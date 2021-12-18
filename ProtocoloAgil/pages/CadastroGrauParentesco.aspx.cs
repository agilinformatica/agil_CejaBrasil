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
    public partial class CadastroGrauParentesco : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(btn_texto);
            if (!IsPostBack)
            {
                BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
               Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
            }
        }

        private void BindGridView(int type)
        {
            using (var repository = new Repository<GrauParentesco>(new Context<GrauParentesco>()))
            {
                var datasource = new List<GrauParentesco>();
                switch (type)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.GpaDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.GpaDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.GpaDescricao)); break;
                }
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataSource = datasource;
                GridView1.DataBind();
            }
        }


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                var cn = new Conexao();
                var sql = GeraSql();
                cn.Alterar(sql);
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

        private string GeraSql()
        {
            if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o grau de paretesco.");

            string sqlupdate = "UPDATE CA_GrauParentesco SET GpaDescricao = '" + TBNome.Text + "' WHERE  GpaCodigo = '" + Session["AlrteraCodigo_modelo"] + "' ";
            var sqlinsert = "INSERT INTO CA_GrauParentesco( GpaDescricao  ) VALUES( '" + TBNome.Text + "' ) ";

            return Session["comando"].Equals("Alterar") ? sqlupdate : sqlinsert;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var row = ((GridView) sender).SelectedRow;
                var dados = from i in bd.CA_GrauParentescos 
                            where i.GpaCodigo.Equals(WebUtility.HtmlDecode(row.Cells[0].Text))
                            select new { i.GpaCodigo, i.GpaDescricao };
                if(dados.Count() > 0)
                {
                    var parentesco = dados.First();
                    TBcodigo.Text = parentesco.GpaCodigo.ToString();
                    Session["AlrteraCodigo_modelo"] = parentesco.GpaCodigo.ToString();
                    TBcodigo.Enabled = false;
                    TBNome.Text = parentesco.GpaDescricao;
                    Session["comando"] = "Alterar";
                    MultiView1.ActiveViewIndex = 1;
                }
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
            TBcodigo.Enabled = false;
            Session["comando"] = "Novo";
            MultiView1.ActiveViewIndex = 1;
        }

        private void LimpaCampos()
        {
            TBcodigo.Text = string.Empty;
            TBNome.Text = string.Empty;
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
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
                using (var repository = new Repository<GrauParentesco>(new Context<GrauParentesco>()))
                {
                    var itens = repository.All().OrderBy(p => p.GpaDescricao);
                    foreach (var item in itens)
                    {
                        string linha = item.GpaCodigo + ";" + item.GpaDescricao;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Grau de Parentesco.txt");
                }
            }
            catch (IOException ex)
            {
                 Funcoes.TrataExcessao("000021", ex);
            }
        }

        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 4;
            MultiView1.ActiveViewIndex = 2;
        }



        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var escola = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<GrauParentesco>(new Context<GrauParentesco>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(escola);
            }
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }
    }
}