using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class CadastroEvento : Page
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
            LBtituloAlt.Text = "Alteração de Curso";
            BTinsert.Text = "Alterar";
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            TBCodigo_curso.Visible = true;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void BindGridView(int tipo)
        {
            using (var repository = new Repository<Eventos>(new Context<Eventos>()))
            {
                var datasource = new List<Eventos>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.EvnNome)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.EvnNome.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.EvnNome)); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }

        private void PreencheCampos()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var curso = bd.CA_Eventos.Where(p => p.EvnCodigo == int.Parse(Session["Alteracodigo"].ToString())).First();
                TBCodigo_curso.Text = curso.EvnCodigo.ToString();
                TBNome.Text = curso.EvnNome;
                TBData.Text =  string.Format("{0:dd/MM/yyyy}",curso.EvnData);
                TBDescricao.Text = curso.EvnDescricao;
            }

        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Session["comando"].Equals("Inserir") && TBCodigo_curso.Text.Equals(string.Empty)) throw new ArgumentException("Informe o código do evento.");
                if (TBData.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data do evento.");
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome do Evento.");

                using (var repository = new Repository<Eventos>(new Context<Eventos>()))
                {
                    var evento = (Session["comando"].Equals("Inserir")) ? new Eventos() : repository.Find(Convert.ToInt16(Session["Alteracodigo"].ToString()));
                    evento.EvnNome = TBNome.Text;
                    evento.EvnData =   DateTime.Parse(TBData.Text) ;
                    evento.EvnDescricao = TBDescricao.Text;

                    if (Session["comando"].Equals("Inserir")) repository.Add(evento);
                    else repository.Edit(evento);
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
            TBData.Text = string.Empty;
            TBDescricao.Text = string.Empty;
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
            Session["comando"] = "Inserir";
            LimpaCampos();
            TBCodigo_curso.Visible = false;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "73";
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
                using (var repository = new Repository<Eventos>(new Context<Eventos>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.EvnCodigo + "; " + item.EvnNome + "; " + string.Format("{0:dd/MM/yyyy}", item.EvnData);
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Disciplinas.txt");
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
            var evento = int.Parse(button.CommandArgument);
            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
            if (bd.CA_Participantes.Where(p => p.PrtCodigoEvento == evento).Count() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('ERRO - A disciplina está associada à uma aula. impossível excluir.')", true);
                return;
            }

            using (var repository = new Repository<Eventos>(new Context<Eventos>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(evento);
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