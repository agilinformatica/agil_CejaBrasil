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
    public partial class CadastroMotDesligamento : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
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

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }


        private void BindGridView(int type)
        {
            using (var repository = new Repository<MotivoDesligamento>(new Context<MotivoDesligamento>()))
            {
                var datasource = new List<MotivoDesligamento>();
                switch (type)
                {
                    case 1 : datasource.AddRange(repository.All().OrderBy(p => p.MotDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.MotDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.MotDescricao)); break;
                }
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataSource = datasource;
                GridView1.DataBind();
            }
        }


        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                using (var repository = new Repository<MotivoDesligamento>(new Context<MotivoDesligamento>()))
               {
                   var motivo = Session["comando"].Equals("Inserir") ? new MotivoDesligamento() : repository.Find(Convert.ToInt32(Session["AlrteraCodigo"]));
                   motivo.MotCodigo = Session["comando"].Equals("Inserir") ? 0 : Convert.ToInt32(Session["AlrteraCodigo"]);
                   motivo.MotDescricao = TBNome.Text;
                   if (Session["comando"].Equals("Inserir")) repository.Add(motivo);
                   else repository.Edit(motivo);
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
                Funcoes.TrataExcessao("000018", ex);
            }
        }

 
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView)sender).SelectedRow;
            Session["AlrteraCodigo"] = WebUtility.HtmlDecode(row.Cells[0].Text);
            PreencheCampos();
            Session["comando"] = "Alterar";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void PreencheCampos()
        {
            using (var repository = new Repository<MotivoDesligamento>(new Context<MotivoDesligamento>()))
            {
                var situacao = repository.Find(Convert.ToInt32(Session["AlrteraCodigo"]));
                TBcodigo.Text = situacao.MotCodigo.ToString();
                TBcodigo.Enabled = false;
                TBNome.Text = situacao.MotDescricao;
            }
        }

        protected void btn_novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            TBcodigo.Enabled = false;
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
                using (var repository = new Repository<MotivoDesligamento>(new Context<MotivoDesligamento>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.MotCodigo + "; " + item.MotDescricao;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Motivos de Desligamento.txt");
                }
            }
            catch (IOException)
            {
                Response.Redirect("ErrorPage.aspx?Erro=000099");
            }
        }

        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 3;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var motivo = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<MotivoDesligamento>(new Context<MotivoDesligamento>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(motivo);
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