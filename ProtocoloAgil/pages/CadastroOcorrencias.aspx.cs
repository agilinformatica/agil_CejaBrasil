using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;

namespace ProtocoloAgil.pages
{
    public partial class CadastroOcorrencias : Page
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
            using (var repository = new Repository<Ocorrencia>(new Context<Ocorrencia>()))
            {
                var datasource = new List<Ocorrencia>();
                switch (type)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.OcoDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.OcoDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.OcoDescricao)); break;
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

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite a descrição da ocorrência.");
                using (var repository = new Repository<Ocorrencia>(new Context<Ocorrencia>()))
                {
                    var ocorrencia = Session["comando"].Equals("Inserir") ? new Ocorrencia() : repository.Find(Convert.ToInt32(Session["AlrteraCodigo"]));
                    ocorrencia.OcoCodigo = Session["comando"].Equals("Inserir") ? 0:  Convert.ToInt32(Session["AlrteraCodigo"]);
                    ocorrencia.OcoDescricao = TBNome.Text;
                    if (Session["comando"].Equals("Inserir")) repository.Add(ocorrencia);
                    else repository.Edit(ocorrencia);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação Realizada com Sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000020", ex);
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
            using (var repository = new Repository<Ocorrencia>(new Context<Ocorrencia>()))
            {
                var situacao = repository.Find(Convert.ToInt32(Session["AlrteraCodigo"]));
                TBcodigo.Text = situacao.OcoCodigo.ToString();
                TBcodigo.Enabled = false;
                TBNome.Text = situacao.OcoDescricao;
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
                using (var repository = new Repository<Ocorrencia>(new Context<Ocorrencia>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.OcoCodigo + "; " + item.OcoDescricao;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Ocorrencias.txt");
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
            try
            {

                var button = (ImageButton)sender;
                var aprendiz = Convert.ToInt32(button.CommandArgument);
                using (var repository = new Repository<Ocorrencia>(new Context<Ocorrencia>()))
                {
                    if (Convert.ToBoolean(HFConfirma.Value))
                        repository.Remove(aprendiz);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                    "alert('Ocorrência excluído com sucesso');", true);
                }
                BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
            }
            catch (Exception c)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                    "alert('ERRO - Não é possível excluir pois já existe ocorrência(as) registradas com esse tipo');", true);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }
    }
}