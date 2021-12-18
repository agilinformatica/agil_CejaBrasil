using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class StatusEcaminhamento : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["CurrentPage"] = "configuracoes";

            if (!IsPostBack)
            {
                BindGridView();
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTsalva.Enabled = false;
            }
        }

      


        private void BindGridView()
        {
            using (var repository = new Repository<CAStatusEncaminhamento>(new Context<CAStatusEncaminhamento>()))
            {
                var datasource = new List<CAStatusEncaminhamento>();
                datasource.AddRange(repository.All().Where(p => p.Ste_Codigo != "999").OrderBy(p => p.Ste_Descricao));
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }


        private void PreencheCampos( string codigo)
        {
            // ----------------------------------preenche campos ------------------------------------>
            using (var repository = new Repository<CAStatusEncaminhamento>(new Context<CAStatusEncaminhamento>()))
            {
                var status = repository.Find(codigo);
                txtCodigoStatus.Text = status.Ste_Codigo.ToString();
                txtStatusEncaminhamento.Text = status.Ste_Descricao;

            }
        }

        protected void BTLimpa_Click(object sender, EventArgs e)
        {
            Limpadados();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        private void Limpadados()
        {
            //txtCodigoInstituicao.Text = string.Empty;
            txtStatusEncaminhamento.Text = string.Empty;
          
           



        }


        protected void BTsalva_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Session["comando"].Equals("Inserir") && TBCodEscola.Equals(string.Empty)) throw new ArgumentException("Digite o código do Campus.");
                if (txtStatusEncaminhamento.Text.Equals(string.Empty)) throw new ArgumentException("Digite a Descrição do Status de Encaminhamento.");
                //if (txtEndereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o Endereço.");
                //if (txtNumeroEndereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o Número do Endereço.");
                ////if (TBcidade.Text.Equals(string.Empty)) throw new ArgumentException("Digite a cidade do Campus.");
                //if (DDMunicipio.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Escolha a Cidade.");

                //if (txtBairro.Text.Equals(string.Empty)) throw new ArgumentException("Digite o Bairro.");




                using (var repository = new Repository<CAStatusEncaminhamento>(new Context<CAStatusEncaminhamento>()))
                {
                    var unidade = Session["comando"].Equals("Inserir") ? new CAStatusEncaminhamento() : repository.Find(HFEscolaRef.Value);
                    CAStatusEncaminhamento status = new CAStatusEncaminhamento();

                    status.Ste_Codigo = Session["comando"].Equals("Inserir") ? txtCodigoStatus.Text : txtCodigoStatus.Text;
                    status.Ste_Descricao = txtStatusEncaminhamento.Text;
                   
                    //instituicao.IpaUsuarioAlteracao = Session["codigo"].ToString();
                    //instituicao.IpaUsuarioCadastro = Session["codigo"].ToString();
                    

                   
                   
                   
                   

                    if (Session["comando"].Equals("Inserir"))
                    {
                    var con = new Conexao();
                    var sql = "insert into CA_StatusEncaminhamento values('" + status.Ste_Codigo + "', '" + status.Ste_Descricao + "')";
                    //var sql = "insert into CA_InstituicoesParceiras values('" + txtDescricaoInstituicaoParceira.Text + "', '" + txtEndereco.Text + "', '" + txtEndereco.Text + "', '" + txtComplemento.Text + "', '" + txtBairro.Text + "', '" + DDMunicipio.SelectedValue + "', '" + DD_estado_Nat.SelectedValue + "', '" + cep + "', '" + txtEmail.Text + "', '" + telefone + "', '" + celular + "', '" + txtNomeContato.Text + "', '" + DateTime.Now + "', '" + DateTime.Now + "', '" + usuarioAlteracao + "', '" + usuarioCadastro + "', '" + senha + "')";
                    con.Alterar(sql);
                    }
                    else
                    {
                        var con = new Conexao();
                        var sql = "update CA_StatusEncaminhamento set Ste_Codigo = '" + status.Ste_Codigo + "', Ste_Descricao = '" + status.Ste_Descricao + "' where Ste_Codigo = '" + HFEscolaRef.Value + "'";     
                        con.Alterar(sql);
                    }
                   

                 //   if (Session["comando"].Equals("Inserir")) repository.Add(unidade);
                 //   else repository.Edit(unidade);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação realizada com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000200", ex);
            }
        }

        #region GetCompletionList

        [WebMethod]
        public static string[] GetCompletionList(String prefixText, int count)
        {
            var values = new List<string>();
            string commandText =
                "SELECT TOP " + count.ToString()
                + " MunIDescricao FROM MA_Municipios WHERE MunIDescricao LIKE '" + prefixText + "%'";
            var comm = new SqlCommand(commandText, new SqlConnection(GetConfig.Config())) { CommandType = CommandType.Text };
            var suggestions = new List<string>();
            var dtSuggestions = new DataTable();
            comm.Connection.Open();
            var adptr = new SqlDataAdapter(comm);
            adptr.Fill(dtSuggestions);
            if (dtSuggestions.Rows != null && dtSuggestions.Rows.Count > 0)
            {
                suggestions.AddRange(from DataRow dr in dtSuggestions.Rows select dr["MunIDescricao"].ToString());
                values = suggestions;
            }
            return values.ToArray();
        }

        #endregion

        protected void listar_Click(object sender, EventArgs e)
        {
            BindGridView();
            MultiView1.ActiveViewIndex = 0;
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void IndiceZeroUF(object sender, EventArgs e)
        {
            var indice0 = new ListItem("--", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void Incluir_Click(object sender, EventArgs e)
        {
            txtCodigoStatus.Text = string.Empty;
            
            MultiView1.ActiveViewIndex = 1;
            Limpadados();
          //  TBCodEscola.ReadOnly = false;
            Session["comando"] = "Inserir";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gvr = ((GridView) sender).SelectedRow;
            Session["comando"] = "Alterar";
            HFEscolaRef.Value = WebUtility.HtmlDecode(gvr.Cells[0].Text);
          
            PreencheCampos(WebUtility.HtmlDecode(gvr.Cells[0].Text));
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var codStatus = button.CommandArgument;
            using (var repository = new Repository<CAStatusEncaminhamento>(new Context<CAStatusEncaminhamento>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(codStatus);
            }
            BindGridView();
        }

       

       
    }
}