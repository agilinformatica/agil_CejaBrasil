using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class DadosProfessores : Page
    {
        readonly DC_ProtocoloAgilDataContext bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"].Equals("Professor"))
            {
                BloqueiaInfo();
            }

            if (!IsPostBack)
            {

               // BindVinculoEmpregaticio();
                BindEscolaridade();
                if (Session["matricula"] != null) ExibeDados();
                LBnome.Visible = false;
                LBdataNasc.Visible = false;
                if (!Session["Comando"].Equals("Inserir")) IniciaCampos();
            }
        }

        private void BloqueiaInfo()
        {
            TBnomeProf.Enabled = false;
            TBcodigoProf.Enabled = false;
            TBdataNascProf.Enabled = false;
            DDestCivilProf.Enabled = false;
            DDsexoProf.Enabled = false;
            TBdataEnt.Enabled = false;
            TBdataSaida.Enabled = false;
            DDsitProf.Enabled = false;
            DDTipoProf.Enabled = false;
            TBCPFProf.Enabled = false;
            TBRGProf.Enabled = false;
            TBnumTituloProf.Enabled = false;
            TBzonaTitProf.Enabled = false;
            TBsecaoTitProf.Enabled = false;
            TBnumCTPSresp.Enabled = false;
            TBserieCTPSResp.Enabled = false;
            TBmunEleitoral.Enabled = false;
            TBobservacao.Enabled = false;
            TB_senha_acesso.Enabled = false;
        }

        private void IniciaCampos()
        {
            //var conn = new Conexao(id_escola);
            //var data = conn.consultar("SELECT * FROM MA_Escolas WHERE Esccodigo = " + id_escola + "");
            ////DDescolaProf.SelectedValue = id_escola.ToString();
            //DDsitProf.SelectedValue = "A";
            return;
        }


        /*----------------------------Exibe dados do Profno------------------------------------------------>*/

        protected void ExibeDados() 
        {
            using (var repository = new Repository<Educadores>(new Context<Educadores>()))
            {
                var professor = repository.Find(int.Parse(Session["matricula"].ToString()));
                TBnomeProf.Text = professor.EducNome;
                TBcodigoProf.Text = professor.EducCodigo.ToString();
                DDestCivilProf.SelectedValue = professor.EducEstadoCivil ?? "";
                TBdataNascProf.Text = professor.EducDataNascimento == null ? "" : string.Format("{0:dd/MM/yyyy}",professor.EducDataNascimento);
                DDsexoProf.SelectedValue = professor.EducSexo ?? "";
                DDTipoProf.SelectedValue = professor.EducTipoAdmissao ?? "";
                TBdataEnt.Text = professor.EducDataEntrada == null ? "" : string.Format("{0:dd/MM/yyyy}", professor.EducDataEntrada);
                TBdataSaida.Text = professor.EducDataSaida == null ? "" : string.Format("{0:dd/MM/yyyy}", professor.EducDataSaida);
                DDsitProf.SelectedValue = professor.EducSituacao ?? "";
                TBCPFProf.Text = professor.EducCPF == null ? "" : Funcoes.FormataCPF(professor.EducCPF);
                TBRGProf.Text = professor.EducIdentidade;
                TBnumTituloProf.Text = professor.EducTitulodeEleitor;
                TBnaturalidade.Text = professor.EducNaturalidade;
                DDestado.DataBind();
                DDestado.SelectedValue = professor.EducUFNaturalidade;
                TBzonaTitProf.Text = professor.EducZonaEleitoral;
                TBsecaoTitProf.Text = professor.EducSecaoEleitoral;
                TBmunEleitoral.Text = professor.EducMunEleitoral;
                TBnumeroEnd.Text = professor.EducNumeroEndereco;
                TBruaEndProf.Text = professor.EducEndereco;
                TBcidadeEndProf.Text = professor.EducCidade;
                DDufEndProf.DataBind();
                DDufEndProf.SelectedValue = professor.EducUF ?? "";
                TBcomplementoEndProf.Text = professor.EducComplemento;
                TBBairroProf.Text = professor.EducBairro;
                TBCepEndProf.Text = professor.EducCEP == null ? "" : Funcoes.FormataCep(professor.EducCEP);
                TBtelefoneEndProf.Text = professor.EducTelefone == null ? "" : Funcoes.FormataTelefone(professor.EducTelefone);
                TBcelularEndProf.Text = professor.EducTelefoneCelular == null ? "" : Funcoes.FormataTelefoneSaoPaulo(professor.EducTelefoneCelular);
                TBemail.Text = professor.EducEMail;
                TBobservacao.Text = professor.EducObservacoes;
                TBnomePai.Text = professor.EducNomedoPai;
                TBnomeMae.Text = professor.EducNomedaMae;
                TBnumCTPSresp.Text = professor.EducCartprofissional;
                TBserieCTPSResp.Text = professor.EducSerieCartProfissional;
                DDEscolaridade.SelectedValue = professor.EducGrauInstrucao.ToString();
                TB_senha_acesso.Text = professor.EducSenha;
            }
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


        [WebMethodAttribute, System.Web.Script.Services.ScriptMethodAttribute]
        public static int GetNextValue(int current, string tag)
        {
            return current + 1;
        }

        [WebMethodAttribute, System.Web.Script.Services.ScriptMethodAttribute]
        public static int GetPreviousValue(int current, string tag)
        {
            return current - 1;
        }

        #region GetBairro

        [WebMethod]
        public static string[] GetMunicipio(String prefixText, int count)
        {

            var values = new List<string>();
            string commandText = "SELECT TOP " + count.ToString() + " MunIDescricao FROM MA_Municipios WHERE MunIDescricao LIKE '%" + prefixText + "%'";
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

        protected void GravaDados()
        {
            if (TBCPFProf.Text.Equals(string.Empty)) throw new ArgumentException("Digite o número do CPF.");
            if (HFcpf.Value.Equals("0")) throw new ArgumentException("CPF  inválido. Confira o número do CPF");

            LBnome.Visible = TBnomeProf.Text.Equals(string.Empty);
            LBdataNasc.Visible = TBdataNascProf.Text.Equals(string.Empty);

            if (TBnomeProf.Text.Equals(string.Empty)) throw new ArgumentException("Informe o nome do educador.");
            if (TBnomeProf.Text.Split(' ').Count() < 2) throw new ArgumentException("Nome do educador deve conter pelo menos nome e sobrenome.");
            if (TBdataNascProf.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de nascimento do educador.");

            if (Session["Comando"].Equals("Inserir"))
            {
                var cpf = from i in bd.CA_Educadores where i.EducCPF.Equals(Funcoes.Retirasimbolo(TBCPFProf.Text)) select i;
                if (cpf.Count() > 0) throw new ArgumentException(" Este número de CPF já está cadastrado para um professor.");
            }

            DateTime data;
            if (!TBdataNascProf.Text.Equals(string.Empty))
                if (!DateTime.TryParse(TBdataNascProf.Text, out data)) throw new ArgumentException(" Data de Nascimento inválida.");
            if (!TBdataEnt.Text.Equals(string.Empty))
                if (!DateTime.TryParse(TBdataEnt.Text, out data)) throw new ArgumentException(" Data de entrada inválida.");
            if (!TBdataSaida.Text.Equals(string.Empty))
                if (!DateTime.TryParse(TBdataSaida.Text, out data)) throw new ArgumentException(" Data de saída inválida.");
            if (!TBtelefoneEndProf.Text.Equals(""))
                if (!Funcoes.ValidaTelefone(TBtelefoneEndProf.Text)) throw new ArgumentException("Telefone inválido.");
            //if (!TBcelularEndProf.Text.Equals(""))
            //    if (!Funcoes.ValidaTelefone(TBcelularEndProf.Text)) throw new ArgumentException("Telefone celular inválido.");
            if (!TBemail.Text.Equals(""))
                if (!Funcoes.ValidaEmail(TBemail.Text)) throw new ArgumentException("E-mail inválido.");

            var query01 = new StringBuilder();
            var query02 = new StringBuilder();

            query01.Append("INSERT INTO CA_Educadores (EducNome,EducSexo ,EducEndereco ,EducNumeroEndereco ,EducComplemento,EducBairro ,EducCidade,EducUF,EducCEP,EducTelefone,EducTelefoneCelular ,EducEMail,EducDataNascimento,EducNaturalidade  " +
          ",EducUFNaturalidade,EducEstadoCivil,EducNomedoPai,EducNomedaMae,EducSituacao,EducDataEntrada,EducDataSaida ,EducTipoAdmissao,EducIdentidade,EducExpedIdentidade,EducCPF,EducTitulodeEleitor,EducZonaEleitoral,EducSecaoEleitoral  " +
          ",EducMunEleitoral,EducSenha ,EducCartprofissional,EducSerieCartProfissional,EducObservacoes, EducGrauInstrucao)VALUES  (@EducNome,@EducSexo, @EducEndereco, @EducNumeroEndereco, @EducComplemento, @EducBairro, @EducCidade, @EducUF,  " +
         " @EducCEP, @EducTelefone,@EducTelefoneCelular, @EducEMail, @EducDataNascimento, @EducNaturalidade, @EducUFNaturalidade ,@EducEstadoCivil, @EducNomedoPai, @EducNomedaMae, @EducSituacao, @EducDataEntrada,@EducDataSaida,@EducTipoAdmissao,   " +
          " @EducIdentidade,@EducExpedIdentidade,@EducCPF, @EducTitulodeEleitor,@EducZonaEleitoral , @EducSecaoEleitoral,@EducMunEleitoral, @EducSenha,@EducCartprofissional, @EducSerieCartProfissional, @EducObservacoes,@EducGrauInstrucao)");

            query02.Append("UPDATE CA_Educadores SET EducNome = @EducNome, EducSexo = @EducSexo,EducEndereco = @EducEndereco,EducNumeroEndereco = @EducNumeroEndereco  " +
          ",EducComplemento = @EducComplemento,EducBairro = @EducBairro,EducCidade = @EducCidade,EducUF = @EducUF ,EducCEP = @EducCEP,EducTelefone = @EducTelefone,EducTelefoneCelular = @EducTelefoneCelular,EducEMail = @EducEMail  " +
          " ,EducDataNascimento = @EducDataNascimento,EducNaturalidade = @EducNaturalidade,EducUFNaturalidade = @EducUFNaturalidade ,EducEstadoCivil = @EducEstadoCivil,EducNomedoPai = @EducNomedoPai,EducNomedaMae = @EducNomedaMae,EducSituacao = @EducSituacao  " +
          " ,EducDataEntrada = @EducDataEntrada,EducDataSaida = @EducDataSaida,EducTipoAdmissao = @EducTipoAdmissao ,EducIdentidade = @EducIdentidade,EducExpedIdentidade = @EducExpedIdentidade,EducCPF = @EducCPF  " +
          " ,EducTitulodeEleitor = @EducTitulodeEleitor,EducZonaEleitoral = @EducZonaEleitoral,EducSecaoEleitoral = @EducSecaoEleitoral  " +
          " ,EducMunEleitoral = @EducMunEleitoral,EducCartprofissional = @EducCartprofissional,EducSerieCartProfissional = @EducSerieCartProfissional  " +
          " ,EducObservacoes = @EducObservacoes, EducGrauInstrucao = @EducGrauInstrucao, EducSenha = @EducSenha WHERE EducCodigo = @codigo");

            var paramaters = new List<SqlParameter>();
            paramaters.Add(TBnomeProf.Text.Equals(string.Empty) ? new SqlParameter("EducNome", DBNull.Value) : new SqlParameter("EducNome", TBnomeProf.Text));
            paramaters.Add(DDestCivilProf.SelectedValue.Equals(string.Empty) ? new SqlParameter("EducEstadoCivil", DBNull.Value) : new SqlParameter("EducEstadoCivil", DDestCivilProf.SelectedValue));
            paramaters.Add(TBdataNascProf.Text.Equals(string.Empty) ? new SqlParameter("EducDataNascimento", DBNull.Value) : new SqlParameter("EducDataNascimento", TBdataNascProf.Text));
            paramaters.Add(DDsexoProf.Text.Equals(string.Empty) ? new SqlParameter("EducSexo", DBNull.Value) : new SqlParameter("EducSexo", DDsexoProf.Text));
            paramaters.Add(DDTipoProf.SelectedValue.Equals(string.Empty) ? new SqlParameter("EducTipoAdmissao", DBNull.Value) : new SqlParameter("EducTipoAdmissao", DDTipoProf.SelectedValue));
            paramaters.Add(TBdataEnt.Text.Equals(string.Empty) ? new SqlParameter("EducDataEntrada", DBNull.Value) : new SqlParameter("EducDataEntrada", TBdataEnt.Text));
            paramaters.Add(TBdataSaida.Text.Equals(string.Empty) ? new SqlParameter("EducDataSaida", DBNull.Value) : new SqlParameter("EducDataSaida", TBdataSaida.Text));
            paramaters.Add(DDsitProf.SelectedValue.Equals(string.Empty) ? new SqlParameter("EducSituacao", DBNull.Value) : new SqlParameter("EducSituacao", DDsitProf.SelectedValue));
            paramaters.Add(TBCPFProf.Text.Equals(string.Empty) ? new SqlParameter("EducCPF", DBNull.Value) : new SqlParameter("EducCPF",Funcoes.Retirasimbolo(TBCPFProf.Text)));
            paramaters.Add(TBRGProf.Text.Equals(string.Empty) ? new SqlParameter("EducIdentidade", DBNull.Value) : new SqlParameter("EducIdentidade", Funcoes.Retirasimbolo(TBRGProf.Text)));
            paramaters.Add(TBnumTituloProf.Text.Equals(string.Empty) ? new SqlParameter("EducTitulodeEleitor", DBNull.Value) : new SqlParameter("EducTitulodeEleitor", Funcoes.Retirasimbolo(TBnumTituloProf.Text)));
            paramaters.Add(TBnaturalidade.Text.Equals(string.Empty) ? new SqlParameter("EducNaturalidade", DBNull.Value) : new SqlParameter("EducNaturalidade", TBnaturalidade.Text));
            paramaters.Add(DDestado.SelectedValue.Equals(string.Empty) ? new SqlParameter("EducUFNaturalidade", DBNull.Value) : new SqlParameter("EducUFNaturalidade", DDestado.SelectedValue));
            paramaters.Add(TBzonaTitProf.Text.Equals(string.Empty) ? new SqlParameter("EducZonaEleitoral", DBNull.Value) : new SqlParameter("EducZonaEleitoral", TBzonaTitProf.Text));
            paramaters.Add(TBsecaoTitProf.Text.Equals(string.Empty) ? new SqlParameter("EducSecaoEleitoral", DBNull.Value) : new SqlParameter("EducSecaoEleitoral", TBsecaoTitProf.Text));
            paramaters.Add(TBmunEleitoral.Text.Equals(string.Empty) ? new SqlParameter("EducMunEleitoral", DBNull.Value) : new SqlParameter("EducMunEleitoral", TBmunEleitoral.Text));
            paramaters.Add(TBnumeroEnd.Text.Equals(string.Empty) ? new SqlParameter("EducNumeroEndereco", DBNull.Value) : new SqlParameter("EducNumeroEndereco", TBnumeroEnd.Text));
            paramaters.Add(TBruaEndProf.Text.Equals(string.Empty) ? new SqlParameter("EducEndereco", DBNull.Value) : new SqlParameter("EducEndereco", TBruaEndProf.Text));
            paramaters.Add(TBcidadeEndProf.Text.Equals(string.Empty) ? new SqlParameter("EducCidade", DBNull.Value) : new SqlParameter("EducCidade", TBcidadeEndProf.Text));
            paramaters.Add(DDufEndProf.SelectedValue.Equals(string.Empty) ? new SqlParameter("EducUF", DBNull.Value) : new SqlParameter("EducUF", DDufEndProf.SelectedValue));
            paramaters.Add(TBcomplementoEndProf.Text.Equals(string.Empty) ? new SqlParameter("EducComplemento", DBNull.Value) : new SqlParameter("EducComplemento", TBcomplementoEndProf.Text));
            paramaters.Add(TBBairroProf.Text.Equals(string.Empty) ? new SqlParameter("EducBairro", DBNull.Value) : new SqlParameter("EducBairro", TBBairroProf.Text));
            paramaters.Add(TBCepEndProf.Text.Equals(string.Empty) ? new SqlParameter("EducCEP", DBNull.Value) : new SqlParameter("EducCEP", Funcoes.Retirasimbolo(TBCepEndProf.Text)));
            paramaters.Add(TBtelefoneEndProf.Text.Equals(string.Empty) ? new SqlParameter("EducTelefone", DBNull.Value) : new SqlParameter("EducTelefone", Funcoes.Retirasimbolo(TBtelefoneEndProf.Text)));
            paramaters.Add(TBcelularEndProf.Text.Equals(string.Empty) ? new SqlParameter("EducTelefoneCelular", DBNull.Value) : new SqlParameter("EducTelefoneCelular", Funcoes.Retirasimbolo(TBcelularEndProf.Text)));
            paramaters.Add(TBemail.Text.Equals(string.Empty) ? new SqlParameter("EducEMail", DBNull.Value) : new SqlParameter("EducEMail", TBemail.Text));
            paramaters.Add(TBobservacao.Text.Equals(string.Empty) ? new SqlParameter("EducObservacoes", DBNull.Value) : new SqlParameter("EducObservacoes", TBobservacao.Text));
            paramaters.Add(TBnomePai.Text.Equals(string.Empty) ? new SqlParameter("EducNomedoPai", DBNull.Value) : new SqlParameter("EducNomedoPai", TBnomePai.Text));
            paramaters.Add(TBnomeMae.Text.Equals(string.Empty) ? new SqlParameter("EducNomedaMae", DBNull.Value) : new SqlParameter("EducNomedaMae", TBnomeMae.Text));
            paramaters.Add(TBnumCTPSresp.Text.Equals(string.Empty) ? new SqlParameter("EducCartprofissional", DBNull.Value) : new SqlParameter("EducCartprofissional", TBnumCTPSresp.Text));
            paramaters.Add(TBserieCTPSResp.Text.Equals(string.Empty) ? new SqlParameter("EducSerieCartProfissional", DBNull.Value) : new SqlParameter("EducSerieCartProfissional", TBserieCTPSResp.Text));
            paramaters.Add(TBExped_identidade.Text.Equals(string.Empty) ? new SqlParameter("EducExpedIdentidade", DBNull.Value) : new SqlParameter("EducExpedIdentidade", TBExped_identidade.Text));
            paramaters.Add(DDEscolaridade.SelectedValue.Equals(string.Empty) ? new SqlParameter("EducGrauInstrucao", DBNull.Value) : new SqlParameter("EducGrauInstrucao", DDEscolaridade.SelectedValue));

            if (!Session["Comando"].Equals("Inserir"))  paramaters.Add(new SqlParameter("codigo", TBcodigoProf.Text));
            paramaters.Add(Session["Comando"].Equals("Inserir")
                               ? new SqlParameter("EducSenha", "1")
                               : new SqlParameter("EducSenha", TB_senha_acesso.Text));
            var con = new Conexao();
             con.Alterar(  Session["comando"].Equals("Inserir") ?  query01.ToString(): query02.ToString(), paramaters.ToArray() );            
        }


        protected void BTsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravaDados();
                Alert.Show("Ação realizada com sucesso.");
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000060",ex);
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
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

        private void BindVinculoEmpregaticio()
        {
            using (var repository = new Repository<TipoEmprego>(new Context<TipoEmprego>()))
            {
                var datasource = new List<TipoEmprego>();
                datasource.AddRange(  repository.All().OrderBy(p=>p.Vin_Descricao)); 
                DDTipoProf.Items.Clear();
                DDTipoProf.DataSource = datasource;
                DDTipoProf.DataBind();
            }
        }

        private void BindEscolaridade()
        {
            using (var repository = new Repository<Escolaridade>(new Context<Escolaridade>()))
            {
                var datasource = new List<Escolaridade>();
                datasource.AddRange(repository.All().OrderBy(p => p.GreDescricao));
                DDEscolaridade.Items.Clear();
                DDEscolaridade.DataSource = datasource;
                DDEscolaridade.DataBind();
            }
        }

        protected void TBCepEndAlu_TextChanged(object sender, EventArgs e)
        {
            var sql = "Select * from Ca_CodigosCep where Cep_Codigo = " + Funcoes.Retirasimbolo(TBCepEndProf.Text) + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                TBruaEndProf.Text = result["Cep_Logradouro"].ToString();
                TBBairroProf.Text = result["Cep_Bairro"].ToString();
                DDufEndProf.SelectedValue = result["Cep_UF"].ToString();
                TBcidadeEndProf.Text = result["Cep_Cidade"].ToString();
                // DDMunicipioEndereco.SelectedValue = "SAO PAULO";
            }

        }
    }
}