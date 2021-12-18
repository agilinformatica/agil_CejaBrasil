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

namespace ProtocoloAgil.pages
{
    public partial class CadastroAreaAtuacao : Page
    {
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


        private void BindGridView(int tipo)
        {
            using (var repository = new Repository<AreaAtuacao>(new Context<AreaAtuacao>()))
            {
                var datasource = new List<AreaAtuacao>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.AreaDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.AreaDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.AreaDescricao)); break;
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
                using (var repository = new Repository<AreaAtuacao>(new Context<AreaAtuacao>()))
                {
                    var areaatuacao = (Session["comando"].Equals("Inserir")) ? new AreaAtuacao() : repository.Find(int.Parse(Session["AlrteraCodigo"].ToString()));
                    areaatuacao.AreaCodigo = ((Session["comando"].Equals("Inserir")) ? 0 : int.Parse(TBcodigo.Text));
                    areaatuacao.AreaDescricao = TBNome.Text;
                    if (!TB_carga_horaria.Text.Equals(string.Empty))
                        areaatuacao.AreaCargaHoraria = int.Parse(TB_carga_horaria.Text);
                    if (!TB_numero_cad.Text.Equals(string.Empty))
                        areaatuacao.AreaNumeroCadastro = int.Parse(TB_numero_cad.Text);

                    areaatuacao.AreaCargaPratica = txtCargaPratica.Text.Equals(string.Empty) ? (int?)null : int.Parse(txtCargaPratica.Text);
                    areaatuacao.AreaCargaTeorica = txtCargaTeorica.Text.Equals(string.Empty) ? (int?)null : int.Parse(txtCargaTeorica.Text);


                    if (Session["comando"].Equals("Inserir")) repository.Add(areaatuacao);
                    else repository.Edit(areaatuacao);
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
            using (var repository = new Repository<AreaAtuacao>(new Context<AreaAtuacao>()))
            {
                var area = repository.Find(Convert.ToInt32(Session["AlrteraCodigo"].ToString()));
                TBcodigo.Text = area.AreaCodigo.ToString();
                TBNome.Text = area.AreaDescricao;
                TB_numero_cad.Text = area.AreaNumeroCadastro.ToString();
                TB_carga_horaria.Text = area.AreaCargaHoraria.ToString();
                txtCargaPratica.Text = area.AreaCargaPratica == null ? "" : area.AreaCargaPratica.ToString();
                txtCargaTeorica.Text = area.AreaCargaTeorica == null ? "" : area.AreaCargaTeorica.ToString();
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
                using (var repository = new Repository<AreaAtuacao>(new Context<AreaAtuacao>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.AreaCodigo + "; " + item.AreaDescricao + "; " + item.AreaNumeroCadastro +"; CH = " + item.AreaCargaHoraria; 
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Turmas.txt");
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
            var turma = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<AreaAtuacao>(new Context<AreaAtuacao>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(turma);
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