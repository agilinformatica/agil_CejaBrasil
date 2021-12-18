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
    public partial class CadastroConceito : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void BindGridView(int tipo)
        {
            using (var repository = new Repository<Conceitos>(new Context<Conceitos>()))
            {
                var datasource = new List<Conceitos>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.ConCodigo)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.ConCodigo.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.ConCodigo)); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }

        private void PreencheCampos()
        {
            using (var repository = new Repository<Conceitos>(new Context<Conceitos>()))
            {
                var conceito = repository.Find(Session["Alteracodigo"].ToString());
                if(conceito == null) return;
                TBCodigo.Text = conceito.ConCodigo;
                TB_Nota.Text = string.Format("{0:F2}", conceito.ConNota);
                TBPercentual.Text = string.Format("{0:F2}", conceito.ConPercentual);
                DDaprova.SelectedValue = conceito.ConAprova;
            }
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBCodigo.Text.Equals(string.Empty)) throw new ArgumentException("Informe o código do conceito.");
                if (TB_Nota.Text.Equals(string.Empty)) throw new ArgumentException("Informe a nota do conceito.");
                if (TBPercentual.Text.Equals(string.Empty)) throw new ArgumentException("Informe o percentual do conceito.");
                if (DDaprova.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe se o conceito aprova ou não.");

                using (var repository = new Repository<Conceitos>(new Context<Conceitos>()))
                {
                    var conceito = (Session["comando"].Equals("Inserir")) ? new Conceitos() : repository.Find(Session["Alteracodigo"].ToString());
                    conceito.ConCodigo = TBCodigo.Text;
                    conceito.ConNota = float.Parse(TB_Nota.Text);
                    conceito.ConPercentual = float.Parse(TBPercentual.Text);
                    conceito.ConAprova = DDaprova.SelectedValue;
                    if (Session["comando"].Equals("Inserir")) repository.Add(conceito);
                    else repository.Edit(conceito);
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
            TBCodigo.Text = string.Empty;
            TB_Nota.Text = string.Empty;
            TBPercentual.Text = string.Empty;
            DDaprova.SelectedValue = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            LBtituloAlt.Text = "Cadastro de Conceito";
            BTinsert.Text = "Salvar";
            Session["comando"] = "Inserir";
            LimpaCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "20";
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
                using (var repository = new Repository<Conceitos>(new Context<Conceitos>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.ConCodigo + "; " + string.Format("{0:F2}", item.ConNota) + "; " +
                            string.Format("{0:F2}", item.ConPercentual) + "; " + (item.ConAprova =="S" ? "Sim" : "Não");
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Conceitos.txt");
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

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var conceito = button.CommandArgument;
            using (var repository = new Repository<Conceitos>(new Context<Conceitos>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(conceito);
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