using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;


namespace ProtocoloAgil.pages
{
    public partial class CadastroRamoAtividade : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if ( Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
                BTinsert.Enabled = false;
            }

            if (IsPostBack) return;
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        private void BindGridView(int tipo)
        {
            if(tipo == 0) return;
            using ( var repository = new Repository<RamoAtividade>(new Context<RamoAtividade>()))
            {
                var datasource = new List<RamoAtividade>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.RatDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.RatDescricao.ToLower().Contains(pesquisa.Text.ToLower().Trim())).OrderBy(p => p.RatDescricao)); break;
                }
                GridView1.DataSource = datasource;    
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void PreencheCampos()
        {
            using (var repository = new Repository<RamoAtividade>(new Context<RamoAtividade>()))
            {
                var ramo = repository.Find(Convert.ToInt32(Session["Alteracodigo"]));
                TBCodigo_curso.Text = ramo.RatCodigo.ToString();
                TBNome.Text = ramo.RatDescricao;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Deletar") return;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            Session["Alteracodigo"] = row.Cells[0].Text;
            Session["Page"] = "RamoAtividade";
            
            Response.Redirect("Excluir.aspx?acs=" + Criptografia.Encrypt(Session["tipoacesso"].ToString(), GetConfig.Key()));
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBCodigo_curso.Text.Equals(string.Empty) && !Session["comando"].Equals("Inserir")) throw new ArgumentException("Informe o código do ramo de atividade.");
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome do ramo de atividade.");
                using (var repository = new Repository<RamoAtividade>(new Context<RamoAtividade>()))
                {
                    var ramo = (Session["comando"].Equals("Inserir")) ? new RamoAtividade() : repository.Find(Convert.ToInt32(Session["Alteracodigo"]));
                    ramo.RatCodigo = (Session["comando"].Equals("Inserir")) ? 0 : Convert.ToInt32(TBCodigo_curso.Text);
                    ramo.RatDescricao = TBNome.Text;
                    if (Session["comando"].Equals("Inserir"))  repository.Add(ramo);
                    else repository.Edit(ramo);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Ação realizada com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('"+ ex.Message+"')", true);
                return;
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000115", ex);
            }
        }

        protected void BTlimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void LimpaCampos()
        {
            TBCodigo_curso.Text = string.Empty;
            TBNome.Text = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            pesquisa.Text = string.Empty;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Session["comando"] = "Inserir";
            LimpaCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "7";
            MultiView1.ActiveViewIndex = 2;
        }

        protected void texto_Click(object sender, EventArgs e)
        {
            var filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            try
            {
                using (var repository = new Repository<RamoAtividade>(new Context<RamoAtividade>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.RatCodigo + "; " + item.RatDescricao;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Ramos de Atividade.txt");
                }
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
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

        protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }
    }
}