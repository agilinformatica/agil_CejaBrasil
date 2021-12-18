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
    public partial class CadastroOrientadores : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(btn_texto);
            if (!IsPostBack)
            {
                BindParceiros( DD_parceiro);
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
            }
        }


        private void BindGridView()
        {
            using (var repository = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var datasource = new List<CA_Orientador>();
                var unidades = DD_unidades.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DD_unidades.SelectedValue);
                datasource.AddRange(repository.CA_Orientadors.Where(p => p.OriUnidadeParceiro == unidades).OrderBy(p => p.OriNome));

                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataSource = datasource;
                GridView1.DataBind();
            }



            //using (var repository = new Repository<Orientador>(new Context<Orientador>()))
            //{
            //    var datasource = new List<Orientador>();
            //    var unidades = DD_unidades.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DD_unidades.SelectedValue);
            //    datasource.AddRange(repository.All().Where(p => p.OriUnidadeParceiro == unidades).OrderBy(p => p.OriNome)); 

            //    HFRowCount.Value = datasource.Count.ToString();
            //    GridView1.DataSource = datasource;
            //    GridView1.DataBind();
            //}
        }


        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (dd_parceiro_cad.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe o parceiro no qual o orientador trabalha.");
                if (DDunidade_parceiro.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe a unidade do parceiro na qual o orientador trabalha.");

                using (var repository = new Repository<Orientador>(new Context<Orientador>()))
               {
                   var orientador = Session["comando"].Equals("Inserir") ? new Orientador() : repository.Find(Convert.ToInt32(Session["AlrteraCodigo"]));
                   orientador.OriCodigo = Session["comando"].Equals("Inserir") ? 0 : Convert.ToInt32(Session["AlrteraCodigo"]);
                   orientador.OriNome = TBNome.Text;
                   orientador.OriTelefone =  TB_telefone.Text.Equals(string.Empty)?  null : Funcoes.Retirasimbolo(TB_telefone.Text);
                   orientador.OriUnidadeParceiro = int.Parse(DDunidade_parceiro.SelectedValue);
                   if (!tb_email.Text.Equals(string.Empty) && Funcoes.ValidaEmail(tb_email.Text))
                       orientador.OriEmail = tb_email.Text;

                   if (Session["comando"].Equals("Inserir")) repository.Add(orientador);
                   else repository.Edit(orientador);
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
            using (var repository = new Repository<Orientador>(new Context<Orientador>()))
            {
                var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
                var orientador = repository.Find(Convert.ToInt32(Session["AlrteraCodigo"]));
                TBcodigo.Text = orientador.OriCodigo.ToString();
                TBcodigo.Enabled = false;
                TBNome.Text = orientador.OriNome;
                TB_telefone.Text = Funcoes.FormataTelefone(orientador.OriTelefone);
                tb_email.Text = orientador.OriEmail;
                BindParceiros(dd_parceiro_cad);
                dd_parceiro_cad.SelectedValue = bd.CA_ParceirosUnidades.Where(p => p.ParUniCodigo == orientador.OriUnidadeParceiro).First().
                        ParUniCodigoParceiro.ToString();
                BindUnidades(dd_parceiro_cad.SelectedValue, DDunidade_parceiro);
                DDunidade_parceiro.SelectedValue = orientador.OriUnidadeParceiro.ToString();
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
            TB_telefone.Text = string.Empty;
            tb_email.Text = string.Empty;
            BindParceiros(dd_parceiro_cad);
            DDunidade_parceiro.Items.Clear();
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
            BindGridView();
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
                if (DD_unidades.Items.Count == 0 || DD_unidades.SelectedValue.Equals(string.Empty))
                    throw new ArgumentException("Selecione uma unidade do parceiro");

                using (var repository = new Repository<Orientador>(new Context<Orientador>()))
                {
                    var dados = repository.All().Where(p => p.OriUnidadeParceiro == int.Parse(DD_unidades.SelectedValue));
                    foreach (var item in dados)
                    {
                        var linha = item.OriCodigo + "; " + item.OriNome + "; " +  Funcoes.FormataTelefone(item.OriTelefone) +  "; " + item.OriEmail;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Orientadores.txt");
                }
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('" + ex.Message + "')", true);
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
            var orientador = Convert.ToInt32(button.CommandArgument);
            using (var repository = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = repository.CA_AlocacaoAprendizs.Where(p => p.ALAOrientador.Equals(orientador));
                if(dados.Count() == 0)
                {
                    if (Convert.ToBoolean(HFConfirma.Value))
                    {
                        var target = repository.CA_Orientadors.Where(p => p.OriCodigo == orientador).First();
                        repository.CA_Orientadors.DeleteOnSubmit(target);
                        repository.SubmitChanges();
                        BindGridView();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('ERRO - Impossível excluir este orientador. Há aprendizes ligados à ele.')", true);
                }
            }
            
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void DD_curso_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList) sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            BindUnidades(drop.SelectedValue, drop.ID.Equals("DD_parceiro") ? DD_unidades : DDunidade_parceiro);
        }


        protected void BindParceiros( DropDownList drop )
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_Parceiro> list = (from i in bd.CA_Parceiros select i).OrderBy(p => p.ParNomeFantasia);
                drop.Items.Clear();
                drop.DataSource = list.ToList();
                drop.DataBind();
            }
        }


        protected void BindUnidades(string curso, DropDownList dropdown)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var list = bd.CA_ParceirosUnidades.Where(p => p.ParUniCodigoParceiro == int.Parse(curso)).OrderBy(p => p.ParUniDescricao);
                dropdown.Items.Clear();
                dropdown.DataSource = list.ToList();
                dropdown.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BindGridView();
        }



    }
}