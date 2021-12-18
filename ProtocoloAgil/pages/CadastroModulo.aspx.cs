using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class CadastroModulo : Page
    {

        protected struct Modulos
        {
            public string CurDescricao { get; set; }
            public int PlanCodigo { get; set; }
            public string PlanDescricao { get; set; }
                
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(btn_texto);
            if (!IsPostBack)
            {
                BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
            }
        }

        protected void BindCursos()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_Curso> list = (from i in bd.CA_Cursos select i).OrderBy(p => p.CurDescricao);
                DDcurso.Items.Clear();
                DDcurso.DataSource = list.ToList();
                DDcurso.DataBind();
            }
        }


        private void BindGridView(int tipo)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var datasource = new List<Modulos>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(bd.CA_Planos.Join(bd.CA_Cursos, p => p.PlanCurso, m => m.CurCodigo, (p, m) => new {p, m}).Select(
                            dados => new Modulos { CurDescricao = dados.m.CurDescricao, PlanCodigo = dados.p.PlanCodigo, PlanDescricao = dados.p.PlanDescricao })
                            .OrderBy(p => p.PlanDescricao)); break;
                    case 2: datasource.AddRange(bd.CA_Planos.Join(bd.CA_Cursos, p => p.PlanCurso, m => m.CurCodigo, (p, m) => new { p, m })
                            .Select(dados => new Modulos { CurDescricao = dados.m.CurDescricao, PlanCodigo = dados.p.PlanCodigo, PlanDescricao = dados.p.PlanDescricao })
                            .Where(p => p.PlanDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.PlanDescricao)); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome da Área de Atuação.");
                using (var repository = new Repository<Planos>(new Context<Planos>()))
                {
                    var modulo = (Session["comando"].Equals("Inserir")) ? new Planos() : repository.Find(int.Parse(Session["AlrteraCodigo"].ToString()));
                    modulo.PlanCodigo = ((Session["comando"].Equals("Inserir")) ? 0 : int.Parse(TBcodigo.Text));
                    modulo.PlanDescricao = TBNome.Text;
                    modulo.PlanCurso = DDcurso.SelectedValue;
                    if (Session["comando"].Equals("Inserir")) repository.Add(modulo);
                    else repository.Edit(modulo);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação Realizada com Sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000020", ex);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView)sender).SelectedRow;
            Session["AlrteraCodigo"] = WebUtility.HtmlDecode(row.Cells[0].Text);
            Session["comando"] = "Alterar";
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void PreencheCampos()
        {
            LimpaCampos();
            BindCursos();
            using (var repository = new Repository<Planos>(new Context<Planos>()))
            {
                var plano = repository.Find(Convert.ToInt32(Session["AlrteraCodigo"].ToString()));
                TBcodigo.Text = plano.PlanCodigo.ToString();
                TBNome.Text = plano.PlanDescricao;
                DDcurso.SelectedValue = plano.PlanCurso;
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btn_novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 1;
        }

        private void LimpaCampos()
        {
            BindCursos();
            TBcodigo.Text = string.Empty;
            TBNome.Text = string.Empty;
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
           BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_texto_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            try
            {
                using (var bd =new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var dados = from i in bd.CA_Cursos join m in bd.CA_Planos on i.CurCodigo equals m.PlanCurso
                                select new {i.CurDescricao, m.PlanCodigo, m.PlanDescricao};
                    foreach (var item in dados)
                    {
                        var linha = item.PlanCodigo + "; " + item.PlanDescricao + "; " + item.CurDescricao;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Modulos.txt");
                }
            }
            catch (IOException ex)
            {
                 Funcoes.TrataExcessao("000021", ex);
            }
        }

        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 2;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var modulo = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<Planos>(new Context<Planos>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(modulo);
            }
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }
    }
}