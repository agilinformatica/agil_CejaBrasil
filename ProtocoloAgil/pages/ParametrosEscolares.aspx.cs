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
    public partial class ParametrosEscolares : Page
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

        private void BindMunicipios()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = from i in bd.MA_Municipios select new { MunIEstado = i.MunIEstado };
                var datasource = dados.Distinct().OrderBy(p => p.MunIEstado);
                DD_estado_Nat.DataSource = datasource;
                DD_estado_Nat.DataBind();
            }
        }


        private void BindGridView()
        {
            using (var repository = new Repository<Unidades>(new Context<Unidades>()))
            {
                var datasource = new List<Unidades>();
                datasource.AddRange(repository.All().Where(p => p.UniCodigo != 999).OrderBy(p => p.UniNome));
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }


        private void PreencheCampos( string codigo)
        {
            // ----------------------------------preenche campos ------------------------------------>
            using (var repository = new Repository<Unidades>(new Context<Unidades>()))
            {
                var escola = repository.Find(Convert.ToInt32(codigo));
                TBCodEscola.Text = escola.UniCodigo.ToString();
                TBnomeEsc.Text = escola.UniNome;
               // TBcidade.Text = escola.UniCidade;
                DD_estado_Nat.SelectedValue = escola.UniEstado;

                PreencheMunicipio();
                DDMunicipio.SelectedValue = escola.UniCidade;

                TBEndereco.Text = escola.UniEndereco;
                TBCep.Text = escola.UniCEP == null ? "" : Funcoes.FormataCep(escola.UniCEP);
                TBcgc.Text = Funcoes.FormataCNPJ(escola.UniCGC);
                TB_Numero_endereco.Text = escola.UniNumeroEndereco;
                TB_Bairro.Text = escola.UniBairro;
                
                TB_email_resp.Text = escola.UniEmailPadraoEnvio;
                TB_site_escola.Text = escola.UniEnderecoWeb;
                TB_complemento.Text = escola.UniComplemento;
                TBtelefone.Text = escola.UniTelefone == null ? "" : Funcoes.FormataTelefone(escola.UniTelefone);
                TB_Representante_Legal.Text = escola.UniRepresentanteLegal;
                TB_Cargo_Representante.Text = escola.UniRepresentanteCargo;


                txt1_3Constitucional.Text = escola.UniUmTerco.ToString();
                Txt_13Salario.Text = escola.Uni13Salario.ToString();
                txtFerias.Text = escola.UniFerias.ToString();
                txtFGTS13Salario.Text = escola.UniFGTS13Sala.ToString();
                txtFGTSFerias.Text = escola.UniFGTSFerias.ToString();
                txtFGTSFolha.Text = escola.UniFGTSFolha.ToString();
                txtPIS13Salario.Text = escola.UniPIS13Sal.ToString();
                txtPISFerias.Text = escola.UniPISFerias.ToString();
                txtPISFolha.Text = escola.UniPISFolha.ToString();
                txtSeguro.Text = escola.Uniseguro.ToString();
                txtxPCMSO.Text = escola.UniPCMSO.ToString();
                txtValorUniforme.Text = escola.UniValorUniforme.ToString();


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
            TBCodEscola.Text = string.Empty;
            TBnomeEsc.Text = string.Empty;
            //TBcidade.Text = string.Empty;
            DDMunicipio.SelectedValue = "";

            TBEndereco.Text = string.Empty;
            TBCep.Text = string.Empty;
            TBtelefone.Text = string.Empty;
            TBcgc.Text = string.Empty;
            DD_estado_Nat.SelectedValue = string.Empty;
            TB_Numero_endereco.Text = string.Empty;
            TB_Bairro.Text = string.Empty;
            TB_email_resp.Text = string.Empty;
            TB_site_escola.Text = string.Empty;
            TB_Representante_Legal.Text = string.Empty;
            TB_Cargo_Representante.Text = string.Empty;

            //ENGARGOS
            txtFGTSFolha.Text = string.Empty;
            txtFGTSFerias.Text = string.Empty;
            txtFGTS13Salario.Text = string.Empty;
            txtPISFolha.Text = string.Empty;
            txtPISFerias.Text = string.Empty;
            txtPIS13Salario.Text = string.Empty;
            txtFerias.Text = string.Empty;
            txt1_3Constitucional.Text = string.Empty;
            Txt_13Salario.Text = string.Empty;
            txtSeguro.Text = string.Empty;
            txtxPCMSO.Text = string.Empty;
            txtValorUniforme.Text = string.Empty;
        }


        protected void BTsalva_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["comando"].Equals("Inserir") && TBCodEscola.Equals(string.Empty)) throw new ArgumentException("Digite o código do Campus.");
                if (TBnomeEsc.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome do Campus.");
                if (TBEndereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o endereço do Campus.");
                if (TB_Numero_endereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o número do endereço do Campus.");
                //if (TBcidade.Text.Equals(string.Empty)) throw new ArgumentException("Digite a cidade do Campus.");
                if (DDMunicipio.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Escolha a cidade do Campus.");

                if (TB_Bairro.Text.Equals(string.Empty)) throw new ArgumentException("Digite o bairro do Campus.");
                if (Funcoes.Retirasimbolo(TBCep.Text).Length != 8) throw new ArgumentException("CEP incorreto.");
                if (!TB_email_resp.Text.Equals(string.Empty) && !Funcoes.ValidaEmail(TB_email_resp.Text)) throw new ArgumentException("O e-mail informado não é válido.");

                if (!TBtelefone.Text.Equals(""))
                    if (!Funcoes.ValidaTelefone(TBtelefone.Text)) throw new ArgumentException("Telefone inválido.");

                using (var repository = new Repository<Unidades>(new Context<Unidades>()))
                {
                    var unidade = Session["comando"].Equals("Inserir") ? new Unidades() : repository.Find(Convert.ToInt32(HFEscolaRef.Value));
                    unidade.UniCodigo =  Session["comando"].Equals("Inserir") ? 0 : Convert.ToInt32(TBCodEscola.Text);
                    unidade.UniNome = TBnomeEsc.Text;
                    unidade.UniTelefone = Funcoes.Retirasimbolo(TBtelefone.Text);

                    unidade.UniEstado = DD_estado_Nat.SelectedValue;
                    //unidade.UniCidade = TBcidade.Text;
                    unidade.UniCidade = DDMunicipio.SelectedValue;


                    unidade.UniCGC = Funcoes.Retirasimbolo(TBcgc.Text);
                    unidade.UniEndereco = TBEndereco.Text;
                    unidade.UniNumeroEndereco = TB_Numero_endereco.Text;
                    unidade.UniComplemento = TB_complemento.Text;
                    unidade.UniBairro = TB_Bairro.Text;
                    unidade.UniCEP = Funcoes.Retirasimbolo(TBCep.Text);
                    unidade.UniEmailPadraoEnvio = TB_email_resp.Text;
                    unidade.UniEnderecoWeb = TB_site_escola.Text;
                    unidade.UniRepresentanteLegal = TB_Representante_Legal.Text;
                    unidade.UniRepresentanteCargo = TB_Cargo_Representante.Text;

                   // double a = 0.2;

                    string UniFGTSFolha = txtFGTSFolha.Text == null || txtFGTSFolha.Text.Equals(string.Empty) ? "0" : txtFGTSFolha.Text.Replace(",", ".");
                    string UniFGTSFerias = txtFGTSFerias.Text == null || txtFGTSFerias.Text.Equals(string.Empty) ? "0" : txtFGTSFerias.Text.Replace(",", ".");
                    string UniFGTS13Sala = txtFGTS13Salario.Text == null || txtFGTS13Salario.Text.Equals(string.Empty) ? "0" : txtFGTS13Salario.Text.Replace(",", ".");
                    string UniPISFolha = txtPISFolha.Text == null || txtPISFolha.Text.Equals(string.Empty) ? "0" : txtPISFolha.Text.Replace(",", ".");
                    string UniPISFerias = txtPISFerias.Text == null || txtPISFerias.Text.Equals(string.Empty) ? "0" : txtPISFerias.Text.Replace(",", ".");
                    string UniPIS13Sal = txtPIS13Salario.Text == null || txtPIS13Salario.Text.Equals(string.Empty) ? "0" : txtPIS13Salario.Text.Replace(",", ".");
                    string UniFerias = txtFerias.Text == null || txtFerias.Text.Equals(string.Empty) ? "0" : txtFerias.Text.Replace(",", ".");
                    string UniUmTerco = txt1_3Constitucional.Text == null || txt1_3Constitucional.Text.Equals(string.Empty) ? "0" : txt1_3Constitucional.Text.Replace(",", ".");
                    string Uni13Slario = Txt_13Salario.Text == null || Txt_13Salario.Text.Equals(string.Empty) ? "0" : Txt_13Salario.Text.Replace(",", ".");
                    string Uniseguro = txtSeguro.Text == null || txtSeguro.Text.Equals(string.Empty) ? "0" : txtSeguro.Text.Replace(",", ".");
                    string UniPCMSO = txtxPCMSO.Text == null || txtxPCMSO.Text.Equals(string.Empty) ? "0" : txtxPCMSO.Text.Replace(",", ".");
                    string UniValorUniforme = txtValorUniforme.Text == null || txtValorUniforme.Text.Equals(string.Empty) ? "0" : txtValorUniforme.Text.Replace(",", "."); 


                    if (Session["comando"].Equals("Inserir"))
                    {
                        var con = new Conexao();
                        var sql = "insert into CA_Unidades values('" + unidade.UniNome + "', '" + unidade.UniEndereco + "', " + unidade.UniNumeroEndereco + ", '" + unidade.UniComplemento + "', '" + unidade.UniBairro + "', '" + unidade.UniCEP + "', '" + unidade.UniCidade + "', '" + unidade.UniEstado + "', '" + unidade.UniTelefone + "', '" + unidade.UniCGC + "', '" + unidade.UniEnderecoWeb + "', '" + unidade.UniEmailPadraoEnvio + "', '" + unidade.UniRepresentanteLegal + "', '" + unidade.UniRepresentanteCargo + "' , " + UniFGTSFolha + ", " + UniFGTSFerias + ", " + UniFGTS13Sala + ", " + UniPISFolha + ", " + UniPISFerias + ", " + UniPIS13Sal + ", " + UniFerias + ", " + UniUmTerco + ", " + Uniseguro + ", " + UniPCMSO + ", " + UniValorUniforme + ", " + Uni13Slario  +")";
                        con.Alterar(sql);
                    }
                    else
                    {
                        var con = new Conexao();
                        var sql = "update CA_Unidades set UniNome = '" + unidade.UniNome + "', UniEndereco = '" + unidade.UniEndereco + "',  UniNumeroEndereco = " + unidade.UniNumeroEndereco + ", UniComplemento = '" + unidade.UniComplemento + "', UniBairro = '" + unidade.UniBairro + "', UniCEP = '" + unidade.UniCEP + "', UniCidade = '" + unidade.UniCidade + "', UniEstado = '" + unidade.UniEstado + "', UniTelefone = '" + unidade.UniTelefone + "', UniCGC = '" + unidade.UniCGC + "', UniEnderecoWeb =  '" + unidade.UniEnderecoWeb + "',  UniEmailPadraoEnvio =  '" + unidade.UniEmailPadraoEnvio + "', UniRepresentanteLegal = '" + unidade.UniRepresentanteLegal + "', UniRepresentanteCargo = '" + unidade.UniRepresentanteCargo + "', UniFGTSFolha = " + UniFGTSFolha + ", UniFGTSFerias = " + UniFGTSFerias + ", UniFGTS13Sala = " + UniFGTS13Sala + ", UniPISFolha = " + UniPISFolha + ", UniPISFerias = " + UniPISFerias + ", UniPIS13Sal = " + UniPIS13Sal + ", UniFerias = " + UniFerias + ", UniUmTerco = " + UniUmTerco + ", Uniseguro = " + Uniseguro + ", UniPCMSO = " + UniPCMSO + " , UniValorUniforme = " + UniValorUniforme + "  , Uni13Salario = " + Uni13Slario + "  where UniCodigo = " + unidade.UniCodigo + " ";     
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
               // Funcoes.TrataExcessao("000200", ex);
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
            BindMunicipios();
            MultiView1.ActiveViewIndex = 1;
            Limpadados();
            TBCodEscola.ReadOnly = false;
            Session["comando"] = "Inserir";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gvr = ((GridView) sender).SelectedRow;
            Session["comando"] = "Alterar";
            HFEscolaRef.Value = WebUtility.HtmlDecode(gvr.Cells[0].Text);
            BindMunicipios();
            PreencheCampos(WebUtility.HtmlDecode(gvr.Cells[0].Text));
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var aprendiz = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<Unidades>(new Context<Unidades>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(aprendiz);
            }
            BindGridView();
        }

        protected void DD_estado_Nat_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencheMunicipio();
        }

        public void PreencheMunicipio()
        {
            var valor = DD_estado_Nat.SelectedValue;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from M in bd.MA_Municipios
                             where M.MunIEstado == valor
                             select new { M.MunIDescricao }).ToList();

                DDMunicipio.DataSource = query;
                DDMunicipio.DataBind();
            }
        }
    }
}