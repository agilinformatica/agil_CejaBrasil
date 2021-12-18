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
    public partial class SituacaoAprendiz : Page
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
            using (var repository = new Repository<Situacao>(new Context<Situacao>()))
            {
                var datasource = new List<Situacao>();
                switch (type)
                {
                    case 1 : datasource.AddRange(repository.All().OrderBy(p => p.StaDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.StaDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.StaDescricao)); break;
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
               using(var repository = new Repository<Situacao>( new Context<Situacao>()))
               {
                   var situacao = Session["comando"].Equals("Inserir") ? new Situacao() : repository.Find(Convert.ToInt32(Session["AlrteraCodigo"]));
                   situacao.StaCodigo = Session["comando"].Equals("Inserir") ? 0 : Convert.ToInt32(Session["AlrteraCodigo"]);
                   situacao.StaAbreviatura = TBAbreviatura.Text;
                   situacao.StaDescricao = TBNome.Text;
                   if (Session["comando"].Equals("Inserir")) repository.Add(situacao);
                   else  repository.Edit(situacao);
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
            using (var repository = new Repository<Situacao>(new Context<Situacao>()))
            {
                var situacao = repository.Find(Convert.ToInt32(Session["AlrteraCodigo"]));
                TBcodigo.Text = situacao.StaCodigo.ToString();
                TBcodigo.Enabled = false;
                TBNome.Text = situacao.StaDescricao;
                TBAbreviatura.Text = situacao.StaAbreviatura;
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
            TBAbreviatura.Text = string.Empty;
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
                using (var repository = new Repository<Situacao>(new Context<Situacao>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.StaCodigo + "; " + item.StaDescricao;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Status do Jovem.txt");
                }
            }
            catch (IOException)
            {
                Response.Redirect("ErrorPage.aspx?Erro=000097");
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
            var aprendiz = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<Situacao>(new Context<Situacao>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(aprendiz);
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