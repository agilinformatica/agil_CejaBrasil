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
    public partial class InstituicoesParceiras : Page
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
            using (var repository = new Repository<CAInstituicoesParceiras>(new Context<CAInstituicoesParceiras>()))
            {
                var datasource = new List<CAInstituicoesParceiras>();
                datasource.AddRange(repository.All().Where(p => p.IpaCodigo != 999).OrderBy(p => p.IpaDescricao));
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }


        private void PreencheCampos( string codigo)
        {
            // ----------------------------------preenche campos ------------------------------------>
            using (var repository = new Repository<CAInstituicoesParceiras>(new Context<CAInstituicoesParceiras>()))
            {
                var instituicao = repository.Find(Convert.ToInt32(codigo));
                txtCodigoInstituicao.Text = instituicao.IpaCodigo.ToString();
                txtDescricaoInstituicaoParceira.Text = instituicao.IpaDescricao;
                txtTelefone.Text = Funcoes.FormataTelefone(instituicao.IpaTelefone);
                txtCelular.Text = Funcoes.FormataTelefoneSaoPaulo(instituicao.IpaCelular);
                txtEmail.Text = instituicao.IpaEmail;
                txtEndereco.Text = instituicao.IpaEndereco;
                txtNumeroEndereco.Text = instituicao.IpaNumeroEndereco;
                txtComplemento.Text = instituicao.IpaComplemento;
                txtBairro.Text = instituicao.IpaBairro;
                DD_estado_Nat.SelectedValue = instituicao.IpaEstado;
                
                txtCep.Text = Funcoes.FormataCep(instituicao.IpaCEP);
                txtNomeContato.Text = instituicao.IpaNomeContato;

                PreencheMunicipio();
                DDMunicipio.SelectedValue = instituicao.IpaCidade.ToUpper();

                txtSenha.Text = instituicao.IpaSenha;
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
            txtDescricaoInstituicaoParceira.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumeroEndereco.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtBairro.Text = string.Empty;
            DD_estado_Nat.SelectedValue = string.Empty;
            DDMunicipio.SelectedValue = string.Empty;
            txtCep.Text = string.Empty;
            txtNomeContato.Text = string.Empty;
            txtSenha.Text = string.Empty;



        }


        protected void BTsalva_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Session["comando"].Equals("Inserir") && TBCodEscola.Equals(string.Empty)) throw new ArgumentException("Digite o código do Campus.");
                if (txtDescricaoInstituicaoParceira.Text.Equals(string.Empty)) throw new ArgumentException("Digite a Descrição da Instituição Parceira.");
                //if (txtEndereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o Endereço.");
                //if (txtNumeroEndereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o Número do Endereço.");
                ////if (TBcidade.Text.Equals(string.Empty)) throw new ArgumentException("Digite a cidade do Campus.");
                //if (DDMunicipio.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Escolha a Cidade.");

                //if (txtBairro.Text.Equals(string.Empty)) throw new ArgumentException("Digite o Bairro.");
                if (txtCep.Text != string.Empty)
                {
                    if (Funcoes.Retirasimbolo(txtCep.Text).Length != 8) throw new ArgumentException("CEP incorreto.");
                }
                
                if (!txtEmail.Text.Equals(string.Empty) && !Funcoes.ValidaEmail(txtEmail.Text)) throw new ArgumentException("O e-mail informado não é válido.");

                if (!txtTelefone.Text.Equals(""))
                    if (!Funcoes.ValidaTelefone(txtTelefone.Text)) throw new ArgumentException("Telefone inválido.");

                using (var repository = new Repository<CAInstituicoesParceiras>(new Context<CAInstituicoesParceiras>()))
                {
                    var unidade = Session["comando"].Equals("Inserir") ? new CAInstituicoesParceiras() : repository.Find(Convert.ToInt32(HFEscolaRef.Value));
                    CAInstituicoesParceiras instituicao = new CAInstituicoesParceiras();

                    instituicao.IpaCodigo = Session["comando"].Equals("Inserir") ? 0 : Convert.ToInt32(txtCodigoInstituicao.Text);
                    instituicao.IpaDescricao = txtDescricaoInstituicaoParceira.Text;
                    instituicao.IpaEndereco = txtEndereco.Text;
                    instituicao.IpaNumeroEndereco = txtNumeroEndereco.Text;
                    instituicao.IpaComplemento = txtComplemento.Text;
                    instituicao.IpaBairro = txtBairro.Text;
                    instituicao.IpaCidade = DDMunicipio.SelectedValue;
                    instituicao.IpaEstado = DD_estado_Nat.SelectedValue;
                    instituicao.IpaCEP = Funcoes.Retirasimbolo(txtCep.Text);
                    instituicao.IpaEmail = txtEmail.Text;
                    instituicao.IpaTelefone = Funcoes.Retirasimbolo(txtTelefone.Text);
                    instituicao.IpaCelular = Funcoes.Retirasimbolo(txtCelular.Text);
                    instituicao.IpaNomeContato = txtNomeContato.Text;
                    instituicao.IpaSenha = txtSenha.Text;


                    instituicao.IpaDataCadastro = DateTime.Now;
                    instituicao.IpaDataAlteracao = DateTime.Now;
                    instituicao.IpaUsuarioAlteracao = Session["codigo"].ToString();
                    instituicao.IpaUsuarioCadastro = Session["codigo"].ToString();
                    

                   
                    string cep = Funcoes.Retirasimbolo(txtCep.Text);
                    string telefone = Funcoes.Retirasimbolo(txtTelefone.Text);
                    string celular = Funcoes.Retirasimbolo(txtCelular.Text);
                   

                    if (Session["comando"].Equals("Inserir"))
                    {
                    var con = new Conexao();
                    var sql = "insert into CA_InstituicoesParceiras values('" + instituicao.IpaDescricao + "', '" + instituicao.IpaEndereco + "', '" + instituicao.IpaNumeroEndereco + "', '" + instituicao.IpaComplemento + "', '" + instituicao.IpaBairro + "', '" + instituicao.IpaCidade + "', '" + instituicao.IpaEstado + "', '" + instituicao.IpaCEP + "', '" + instituicao.IpaEmail + "', '" + instituicao.IpaTelefone + "', '" + instituicao.IpaCelular + "', '" + instituicao.IpaNomeContato + "', '" + instituicao.IpaDataCadastro + "', '" + string.Empty + "', '" + string.Empty + "', '" + instituicao.IpaUsuarioCadastro + "', '" + instituicao.IpaSenha + "')";
                    //var sql = "insert into CA_InstituicoesParceiras values('" + txtDescricaoInstituicaoParceira.Text + "', '" + txtEndereco.Text + "', '" + txtEndereco.Text + "', '" + txtComplemento.Text + "', '" + txtBairro.Text + "', '" + DDMunicipio.SelectedValue + "', '" + DD_estado_Nat.SelectedValue + "', '" + cep + "', '" + txtEmail.Text + "', '" + telefone + "', '" + celular + "', '" + txtNomeContato.Text + "', '" + DateTime.Now + "', '" + DateTime.Now + "', '" + usuarioAlteracao + "', '" + usuarioCadastro + "', '" + senha + "')";
                    con.Alterar(sql);
                    }
                    else
                    {
                        var con = new Conexao();
                        var sql = "update CA_InstituicoesParceiras set IpaDescricao = '" + instituicao.IpaDescricao + "', IpaEndereco = '" + instituicao.IpaEndereco + "', IpaNumeroEndereco = '"+instituicao.IpaNumeroEndereco+"',  IpaComplemento = '" + instituicao.IpaComplemento + "', IpaBairro = '" + instituicao.IpaBairro + "', IpaCidade = '" + instituicao.IpaCidade + "', IpaEstado = '" + instituicao.IpaEstado + "', IpaCEP = '" + instituicao.IpaCEP + "', IpaEmail = '" + instituicao.IpaEmail + "', IpaTelefone = '" + instituicao.IpaTelefone + "', IpaCelular = '" + instituicao.IpaCelular + "', IpaNomeContato =  '" + instituicao.IpaNomeContato + "', IpaDataAlteracao = '" + instituicao.IpaDataAlteracao + "', IpaUsuarioAlteracao = '" + instituicao.IpaUsuarioAlteracao + "' , IpaSenha = '" + instituicao.IpaSenha + "' where IpaCodigo = " + instituicao.IpaCodigo + "";     
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
            txtCodigoInstituicao.Text = string.Empty;
            BindMunicipios();
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
            BindMunicipios();
            PreencheCampos(WebUtility.HtmlDecode(gvr.Cells[0].Text));
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var aprendiz = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<CAInstituicoesParceiras>(new Context<CAInstituicoesParceiras>()))
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