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
    public partial class CadastroProfissao : Page
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
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
            Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = GridView1.SelectedIndex;

            var row = GridView1.Rows[index];
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = row.Cells[0].Text;
            TBCodigo_curso.Visible = true;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void BindGridView( int tipo)
        {
            using (var repository = new Repository<Profissoes>(new Context<Profissoes>()))
            {
                var datasource = new List<Profissoes>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.ProfDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.ProfDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.ProfDescricao)); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }


        private void PreencheCampos()
        {
            using(var repository = new Repository<Profissoes>(new Context<Profissoes>() ))
            {
                var profissao = repository.All().Where(p => p.ProfCodigo.Equals(Convert.ToInt32(Session["Alteracodigo"]))).First();
                TBCodigo_curso.Text = profissao.ProfCodigo.ToString();
                TBNome.Text = profissao.ProfDescricao;
            }
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Session["comando"].Equals("Inserir") && TBCodigo_curso.Text.Equals(string.Empty)) throw new ArgumentException("Informe o código da profissão.");
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome da profissão.");

                using (var repository = new Repository<Profissoes>(new Context<Profissoes>()))
                {
                    var profissao = (Session["comando"].Equals("Inserir")) ? new Profissoes() : repository.Find(Convert.ToInt32(Session["Alteracodigo"]));
                    profissao.ProfCodigo = (Session["comando"].Equals("Inserir")) ? 0 : Convert.ToInt32(Session["Alteracodigo"]);
                    profissao.ProfDescricao = TBNome.Text;
                    if (Session["comando"].Equals("Inserir")) repository.Add(profissao);
                    else  repository.Edit(profissao);
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
            LimpaCampos();
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            Session["comando"] = "Inserir";
            LimpaCampos();
            TBCodigo_curso.Visible = false;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "3";
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
                using (var repository = new Repository<Profissoes>(new Context<Profissoes>()))
                {
                    var dados = repository.All().OrderBy(p => p.ProfDescricao);
                    foreach (var item in dados)
                    {
                        var linha = item.ProfCodigo + "; " + item.ProfDescricao;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Profissoes.txt");
                }
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
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


        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var turma = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<Profissoes>(new Context<Profissoes>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(turma);
            }
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 :2 );
        }
    }
}