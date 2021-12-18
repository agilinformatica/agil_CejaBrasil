using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MenorAprendizWeb.Base.Engine;
using MenorAprendizWeb.Base.ViewModel;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.Data.Entity;
using MKB.TimePicker;
using System.Globalization;
using System.Web;

namespace ProtocoloAgil.pages
{
    public partial class CadastroAprendiz : Page
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();

        }

        [Serializable]
        public struct Arquivos
        {
            public string Nome_Arquivo { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.Now);

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(btnsalvar);

            if (!IsPostBack)
            {

                btnsalvar.Visible = true;
                CarregarDropTurmaSimultaneidade();
                CarregarDropTurmaCCI();
                CarregarDropTurmaEncontro();
                CarregarDropAreaAtuacao();
                CarregarDropPlanoCurricular();
                CarregarDropUnidade();
                // CarregarDropBairro();
                CarregarDropInstituicaoParceira();



                pn_info.Visible = false;
                pn_alocacao.Visible = true;

                MultiView1.ActiveViewIndex = 0;


                if (Session["Comando"].Equals("PesquisaCandidato"))
                {
                    btnsalvar.Visible = false;
                    btn_adicionar.Visible = false;
                    img_add_photo.Visible = false;
                    Button1.Visible = false;
                    btnDocumentacao.Visible = false;
                    ExibeDados();
                }
                else
                {



                    if (Session["enable_Save"].Equals("Aluno") || Session["enable_Save"].Equals("Parceiro") || Session["enable_Save"].Equals("Educador"))
                    {
                        btnsalvar.Visible = false;
                        btn_adicionar.Visible = false;
                        img_add_photo.Visible = false;
                        Button1.Visible = false;
                        btn_familia.Visible = false;
                        btnDocumentacao.Visible = false;
                    }

                    if (Session["enable_Save"].Equals("Parceiro"))
                    {
                        Button1.Visible = false;
                        btn_notas_faltas.Visible = false;
                        //btn_vale_transporte.Visible = false;
                        //btn_vale_transporte.Visible = false;
                        btn_pesquisa.Visible = false;
                        btn_familia.Visible = false;

                        GridView4.Columns[4].Visible = false;
                        GridView4.Columns[5].Visible = false;
                        GridView4.Columns[6].Visible = false;
                        GridView4.Columns[7].Visible = false;
                    }



                    if (!Session["Comando"].Equals("Inserir"))
                    {
                        ExibeDados();
                    }
                    else
                    {
                        DDufEndAlu.SelectedValue = "MG";
                        DD_estado_Nat.SelectedValue = "MG";
                        DDufRGAlu.SelectedValue = "MG";
                        //DD_uf_ctps.SelectedValue = "SP";

                        CarregarDropCidadeInicial();
                        // DDMunicipioEndereco.SelectedValue = "SAO PAULO";
                        DDMunicipio.SelectedValue = "CONTAGEM";

                        TBpaisAlu.Text = "Brasileira";

                        img_add_photo.Visible = false;
                        IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                        BindUsuarios();
                        btn_familia.Enabled = false;
                        btn_alocacao.Enabled = false;
                        // btn_vale_transporte.Enabled = false;
                        btn_notas_faltas.Enabled = false;
                        btn_pesquisa.Enabled = false;
                    }

                    if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
                    {
                        btnsalvar.Visible = false;
                        // btn_novo_itinerario.Visible = false;
                        Button3.Visible = false;
                    }


                    if (Session["option"] != null)
                    {
                        var tipo = Session["option"].ToString();
                        switch (tipo)
                        {
                            case "1":
                                //andreghorst 07/04/2014 - código comentado abaixo estava dando pau e eu refiz esta parte                           
                                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                                {
                                    var escola = from E in bd.CA_Escolas
                                                 orderby (E.EscNome)
                                                 select new { E.EscCodigo, E.EscNome };
                                    DDcoleOrigem.Items.Clear();
                                    DDcoleOrigem.SelectedIndex = -1;
                                    DDcoleOrigem.SelectedValue = null;
                                    DDcoleOrigem.ClearSelection();
                                    DDcoleOrigem.DataSourceID = null;
                                    DDcoleOrigem.DataSource = null;
                                    DDcoleOrigem.DataSource = escola;
                                    DDcoleOrigem.DataValueField = "EscCodigo";
                                    DDcoleOrigem.DataTextField = "EscNome";
                                    DDcoleOrigem.DataBind();
                                }
                                //DDcoleOrigem.DataBind();
                                //var escola = Session["code_delivered"] == null ? "" : Criptografia.Decrypt(Session["code_delivered"].ToString(), GetConfig.Key());
                                //DDcoleOrigem.SelectedValue = escola;
                                break;
                            case "2":
                                //andreghorst 07/04/2014 - código comentado abaixo estava dando pau e eu refiz esta parte
                                // using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                                // {
                                //     var profissao = from P in bd.CA_Profissoes orderby(P.ProfDescricao)
                                //                select new {P.ProfCodigo,P.ProfDescricao};
                                //DDprofissao_resp.Items.Clear();
                                //DDprofissao_resp.SelectedIndex = -1;
                                //DDprofissao_resp.SelectedValue = null;
                                //DDprofissao_resp.ClearSelection();
                                //DDprofissao_resp.DataSourceID = null;
                                //DDprofissao_resp.DataSource = null;
                                //DDprofissao_resp.DataSource = profissao;
                                //DDprofissao_resp.DataValueField = "ProfCodigo";
                                //DDprofissao_resp.DataTextField = "ProfDescricao";
                                //DDprofissao_resp.DataBind();                            
                                // }   
                                //var profissao = Session["code_delivered"] == null ? "" : Criptografia.Decrypt(Session["code_delivered"].ToString(), GetConfig.Key());
                                //var profissao = Session["code_delivered"] == null ? "" : Session["code_delivered"].ToString();
                                //DDprofissao_resp.SelectedValue = profissao;
                                MultiView1.ActiveViewIndex = 1;
                                break;
                        }
                        Session.Remove("option");
                        Session.Remove("code_delivered");
                    }
                }
            }

        }

        //protected void CarregarDropBairro()
        //{
        //    using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
        //    {
        //        var query = (from B in db.CA_Bairros
        //                     select new { B.DescBairro });

        //        DDBairro.DataSource = query;
        //        DDBairro.DataBind();

        //    }
        //}


        protected void CarregarDropCidadeInicial()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from M in db.MA_Municipios
                             where M.MunIEstado.Equals("MG")
                             select new { M.MunIDescricao });
                // DDMunicipioEndereco.DataSource = query;
                // DDMunicipioEndereco.DataBind();

                DDMunicipio.DataSource = query;
                DDMunicipio.DataBind();

            }
        }

        protected void CarregarDropTurmaSimultaneidade()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from T in db.CA_Turmas
                             where T.TurCurso.Equals("002")
                             select new { T.TurCodigo, T.TurNome });

                DDTurma.DataSource = query;
                DDTurma.DataBind();


            }
        }

        protected void CarregarDropTurmaCCI()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from T in db.CA_Turmas
                             where (T.TurCurso.Equals("005") || T.TurCurso.Equals("007"))
                             select new { T.TurCodigo, T.TurNome });

                DDTurmaCCI.DataSource = query;
                DDTurmaCCI.DataBind();
            }
        }


        protected void CarregarDropTurmaEncontro()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from T in db.CA_Turmas
                             where T.TurCurso.Equals("003") || T.TurCurso.Equals("004")
                             select new { T.TurCodigo, T.TurNome });

                DDTurmaEnc.DataSource = query;
                DDTurmaEnc.DataBind();
            }
        }

        protected void CarregarDropInstituicaoParceira()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from I in db.CA_InstituicoesParceiras
                             select new { I.IpaCodigo, I.IpaDescricao });

                DDInstituicaoParceira.DataSource = query;
                DDInstituicaoParceira.DataBind();
            }
        }

        protected void CarregarDropAreaAtuacao()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from AA in db.CA_AreaAtuacaos
                             //where AA.AreaCodigo == 6
                             select new { AA.AreaCodigo, AA.AreaDescricao });

                DDAreaAtuacao.DataSource = query;
                DDAreaAtuacao.DataBind();
                DDAreaAtuacao.SelectedValue = "6";
            }
        }

        protected void CarregarDropPlanoCurricular()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from PC in db.CA_Planos
                             // where PC.PlanCodigo == 6
                             select new { PC.PlanCodigo, PC.PlanDescricao });

                DDPlanoCurricular.DataSource = query;
                DDPlanoCurricular.DataBind();
                DDPlanoCurricular.SelectedValue = "6";
                DDPlanoIntensivo.DataSource = query;
                DDPlanoIntensivo.DataBind();
                DDPlanoIntensivo.SelectedValue = "6";
            }
        }

        protected void CarregarDropUnidade()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from U in db.CA_Unidades
                             select new { U.UniCodigo, U.UniNome });

                DDUnidade.DataSource = query;
                DDUnidade.DataBind();
            }
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }


        protected void BindUsuarios()
        {
            using (var reposytory = new Repository<Usuarios>(new Context<Usuarios>()))
            {
                var list = new List<Usuarios>();
                list.AddRange(reposytory.All().OrderBy(p => p.UsuNome));
                DDusuario.Items.Clear();
                DDusuario.DataSource = list.ToList();
                DDusuario.DataBind();
            }
        }

        protected void ExibeDados()
        {
            try
            {
                using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
                {
                    BindUsuarios();
                    var aprendiz = repository.Find(Convert.ToInt32(Session["matricula"]));
                    TBcodigoAlu.Text = aprendiz.Apr_Codigo.ToString();
                    TBnomeAlu.Text = aprendiz.Apr_Nome;
                    TBdataNascAlu.Text = aprendiz.Apr_DataDeNascimento == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataDeNascimento));
                    DDsexoAlu.SelectedValue = aprendiz.Apr_Sexo ?? "";
                    DD_situacao_Aprendiz.DataBind();
                    DD_situacao_Aprendiz.SelectedValue = aprendiz.Apr_Situacao.ToString();
                    TB_Data_Cadastro.Text = aprendiz.Apr_DataCadastro == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataCadastro));
                    DDestCivilAlu.SelectedValue = aprendiz.AprEstadoCivil ?? "";
                    TBpaisAlu.Text = aprendiz.Apr_Nacionalidade;

                    DD_estado_Nat.DataBind();
                    DD_estado_Nat.SelectedValue = aprendiz.Apr_UF_Nat ?? "";

                    //TBcidadeAlu.Text = aprendiz.Apr_Naturalidade;
                    if (!DD_estado_Nat.SelectedValue.Equals(string.Empty))
                    {
                        PreencheMunicipio();
                        DDMunicipio.SelectedValue = aprendiz.Apr_Naturalidade;
                    }

                    DDcoleOrigem.DataBind();
                    DDcoleOrigem.SelectedValue = aprendiz.AprCodEscola.ToString();
                    DDTurma.SelectedValue = aprendiz.Apr_Turma.ToString();
                    DDAreaAtuacao.SelectedValue = aprendiz.Apr_AreaAtuacao.ToString();
                    DDPlanoCurricular.SelectedValue = aprendiz.Apr_PlanoCurricular.ToString();

                    // Dados Novos Thassio
                    txtInicioEmpresa.Text = aprendiz.Apr_DataInicioEmpresa == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataInicioEmpresa));
                    txtNumeroExterno.Text = aprendiz.Apr_NumSistExterno == null ? "" : aprendiz.Apr_NumSistExterno;
                    DDEstudaAtualmente.SelectedValue = aprendiz.Apr_Estudante;
                    DDDeficiencia.SelectedValue = aprendiz.Apr_Deficiencia;
                    DDTurno.SelectedValue = aprendiz.Apr_TurnoEscolar;
                    txtCBO.Text = aprendiz.Apr_CBO == null ? "" : aprendiz.Apr_CBO;

                    //endereco escola
                    if (aprendiz.AprCodEscola != null)
                    {
                        ShowAdress(aprendiz.AprCodEscola);
                    }

                    if (aprendiz.Apr_DataDeNascimento != null)
                    {
                        LBidade.Text = CalculaIdade((DateTime)aprendiz.Apr_DataDeNascimento);
                    }


                    var inicio = aprendiz.Apr_Escola_HInicio == null ? DateTime.Today : (Convert.ToDateTime(aprendiz.Apr_Escola_HInicio));
                    var final = aprendiz.Apr_Escola_HTermino == null ? DateTime.Today : (Convert.ToDateTime(aprendiz.Apr_Escola_HTermino));

                    //Andreghorst 17/03/2014
                    //inicio
                    // TSinicio.AmPm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    //componente proprietario nao faz 24 horas, sendo assim é necessário a função acima para que o mesmo
                    //converta as horas adequadamente    
                    //if (Convert.ToInt32(inicio.Hour) < 12 && aprendiz.Apr_Escola_HInicio != null)
                    //    TSinicio.AmPm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;

                    //if (Convert.ToInt32(final.Hour) < 12 && aprendiz.Apr_Escola_HTermino != null)
                    //    TSfim.AmPm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;

                    //if (aprendiz.Apr_Escola_HInicio == null)
                    //{

                    //    TSinicio.Hour = 0;
                    //    TSinicio.Minute = 0;
                    //}
                    //else
                    //{
                    //    TSinicio.Hour = inicio.Hour;
                    //    TSinicio.Minute = inicio.Minute;
                    //}


                    //if (aprendiz.Apr_Escola_HTermino == null)
                    //{
                    //    //TSfim.SetTime(dt.Hour, dt.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
                    //    TSfim.Hour = 0;
                    //    TSfim.Minute = 0;
                    //}
                    //else
                    //{
                    //    TSfim.Hour = final.Hour;
                    //    TSfim.Minute = final.Minute;
                    //}
                    //fim 

                    TBnomePai.Text = aprendiz.Apr_NomePai;
                    TBnomeMae.Text = aprendiz.Apr_NomeMae;
                    TBRGalu.Text = aprendiz.Apr_CarteiraDeIdentidade;
                    TBdataEmissRGAlu.Text = aprendiz.Apr_DataEmissão_Ident == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataEmissão_Ident));
                    TBorgExpRGAlu.Text = aprendiz.Apr_OrgaoEmissor_Ident;
                    DDufRGAlu.DataBind();
                    DDufRGAlu.SelectedValue = aprendiz.Apr_UF_Ident;
                    TBCPFalu.Text = aprendiz.Apr_CPF == null ? "" : Funcoes.FormataCPF(aprendiz.Apr_CPF);
                    TB_numero_pis.Text = aprendiz.Apr_PIS;
                    TBnumCTPSresp.Text = aprendiz.Apr_CarteiraDeTrabalho;
                    TBserieCTPSResp.Text = aprendiz.Apr_Serie_Cartrab;
                    //TB_DataEmissao_ctps.Text = aprendiz.Apr_DataEmissão_CartTrab == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataEmissão_CartTrab));
                    //TB_numero_pne.Text = aprendiz.Apr_PNE;
                    TBruaEndAlu.Text = aprendiz.Apr_Endereço;
                    TBnumEndAlu.Text = aprendiz.Apr_NumeroEndereco;
                    TBcomplementoEndAlu.Text = aprendiz.Apr_Complemento;
                    TB_cert_reservista.Text = aprendiz.Apr_CertReservista;


                    txtInicioFerias.Text = aprendiz.Apr_DataPrevInicFerias == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataPrevInicFerias));
                    txtTerminoFerias.Text = aprendiz.Apr_DataPrevTermFerias == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataPrevTermFerias));


                    //TBNumeroMTB.Text = aprendiz.Apr_NumeroMTB;

                    txtBairro.Text = aprendiz.Apr_Bairro;
                    // DDBairro.SelectedValue = aprendiz.Apr_Bairro;

                    DDufEndAlu.DataBind();
                    DDufEndAlu.SelectedValue = aprendiz.Apr_UF;
                    //TBcidadeEndalu.Text = aprendiz.Apr_Cidade;
                    // PreencheMunicipioEndereco();
                    txtMunicipioEndereco.Text = aprendiz.Apr_Cidade;
                    //thassio

                    DDUnidade.SelectedValue = aprendiz.Apr_Unidade.ToString();

                    DDInstituicaoParceira.SelectedValue = aprendiz.Apr_InstParceira.ToString();

                    txtMesesContrato.Text = aprendiz.Apr_MesesContrato.ToString();
                    txtHorasDiarias.Text = aprendiz.Apr_HorasDiarias.ToString();
                    DDTurmaCCI.SelectedValue = aprendiz.Apr_TurmaCCI.ToString();
                    DDTurmaEnc.SelectedValue = aprendiz.Apr_TurmaENC.ToString();

                    //DD_uf_ctps.DataBind();
                    //DD_uf_ctps.SelectedValue = aprendiz.Apr_UF_CartTrab;

                    TBCepEndAlu.Text = aprendiz.Apr_CEP == null ? "" : Funcoes.FormataCep(aprendiz.Apr_CEP);
                    TBtelefoneEndAlu.Text = aprendiz.Apr_Telefone == null ? "" : Funcoes.FormataTelefone(aprendiz.Apr_Telefone);
                    TBcelularEndAlu.Text = aprendiz.Apr_Celular == null ? "" : Funcoes.FormataTelefoneSaoPaulo(aprendiz.Apr_Celular);
                    TBemail.Text = aprendiz.Apr_Email;
                    TB_observacao.Text = aprendiz.Apr_Observacoes;
                    DD_escolaridade_aprendiz.DataBind();
                    DD_escolaridade_aprendiz.SelectedValue = aprendiz.Apr_Escolaridade.ToString();
                    DDusuario.SelectedValue = aprendiz.Apr_Tutor;
                    TB_DataExpiracao.Text = aprendiz.Apr_ValidadeMensagem == null ? "" : string.Format("{0:dd/MM/yyyy}", aprendiz.Apr_ValidadeMensagem);
                    TB_mensagem.Text = aprendiz.Apr_Mensagem;
                    TB_senha_acesso.Text = aprendiz.Apr_senha;



                    // ----------------------- Dados bancários -------------------------------
                    //DDbanco.SelectedValue = aprendiz.AprBanco ?? "";
                    //DDTipoPoupanca.SelectedValue = aprendiz.AprTipoConta ?? "";
                    //TBAgencia.Text = aprendiz.AprAgencia;
                    //TBNumeroConta.Text = aprendiz.AprContaBancaria;

                    // ----------------------- Dados do Responsável -------------------------------
                    TB_cpf_resp.Text = aprendiz.Apr_Resp_CPF == null ? "" : Funcoes.FormataCPF(aprendiz.Apr_Resp_CPF);
                    TB_nome_resp.Text = aprendiz.Apr_Responsavel;
                    TB_tel_resp.Text = aprendiz.Apr_Telefone_Resp == null ? "" : Funcoes.FormataTelefone(aprendiz.Apr_Telefone_Resp);
                    TB_cel_resp.Text = aprendiz.Apr_Celular_Resp == null ? "" : Funcoes.FormataTelefoneSaoPaulo(aprendiz.Apr_Celular_Resp);
                    TB_recado_nome.Text = aprendiz.Apr_Tipo_Contato;
                    TB_recado_tel.Text = aprendiz.Apr_Telefone_Contato == null ? "" : Funcoes.FormataTelefone(aprendiz.Apr_Telefone_Contato);
                    TB_observacao_resp.Text = aprendiz.Apr_Resp_Observacoes;

                    // ----------------------- Dados sócio-Econômicos -------------------------------
                    DDBeneficio.SelectedValue = aprendiz.Apr_beneficio ?? "";
                    TB_bolsa_familia.Text = aprendiz.Apr_BolsaFamilia == null ? "" : string.Format("{0:F}", aprendiz.Apr_BolsaFamilia);
                    TB_pensao.Text = aprendiz.Apr_pensao == null ? "" : string.Format("{0:F}", aprendiz.Apr_pensao);
                    TB_valor_outros.Text = aprendiz.Apr_outros == null ? "" : string.Format("{0:F}", aprendiz.Apr_outros);
                    DDTipoResidencia.SelectedValue = aprendiz.Apr_SituacaoResidencia ?? "";
                    TBValorAluguel.Text = aprendiz.Apr_aluguel == null ? "" : string.Format("{0:F}", aprendiz.Apr_aluguel);
                    DDcarteiraAssinada.SelectedValue = aprendiz.Apr_CarteiraAssinada ?? "";
                    TB_Empresa.Text = aprendiz.Apr_Empresa;
                    TB_Cargo.Text = aprendiz.Apr_Cargo;
                    TB_permanencia.Text = aprendiz.Apr_TempoPermanencia;
                    DDmedicamentos.SelectedValue = aprendiz.Apr_UsaMedicamento ?? "";
                    TB_medicamento.Text = aprendiz.Apr_NomeMedicamento;
                    TB_finalidade_medicamento.Text = aprendiz.Apr_TipoMedicamento;
                    DDAlergia.SelectedValue = aprendiz.Apr_Alergia;
                    TB_tipo_alergia.Text = aprendiz.Apr_TipoAlergia;
                    DDdoenca.SelectedValue = aprendiz.Apr_Doenca ?? "";
                    TB_tipo_doenca.Text = aprendiz.Apr_NomeDoenca;
                    TBNumeroFamiliares.Text = aprendiz.Apr_numeroFamiliares.ToString();
                    tb_renda_percapta.Text = CalculaRenda(aprendiz.Apr_Codigo);
                    //                   Lb_indicador.Text = CalculaIndice(tb_renda_percapta.Text);
                    DD_grau_Parentesco.DataBind();
                    DD_grau_Parentesco.SelectedValue = aprendiz.Apr_Resp_Parentesco.ToString();

                    TB_inicio_aprendizagem.Text = string.Format("{0:dd/MM/yyy}", aprendiz.Apr_InicioAprendizagem);
                    TB_data_desligamento.Text = string.Format("{0:dd/MM/yyy}", aprendiz.Apr_FimAprendizagem);
                    TB_prev_Fim_aprendizagem.Text = string.Format("{0:dd/MM/yyy}", aprendiz.Apr_PrevFimAprendizagem);

                    DDRecebeBeneficio.SelectedValue = aprendiz.Apr_RecebeBeneficio ?? "";

                    TBDataContrato.Text = aprendiz.Apr_DataContrato == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataContrato));

                    CarregaFoto();
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000126", ex);
            }
        }



        private string CalculaIndice(string valor)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var indices = bd.CA_IndicesSociais;
                var renda = float.Parse(valor.Replace("R$ ", ""));
                string indiceTexto = indices.Where(p => renda >= p.IndValorMinimo && renda <= p.IndValorMaximo).First().IndDescricao;
                return indiceTexto;
            }
        }

        private string CalculaRenda(int aluno)
        {

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var familiares = bd.View_CA_Familiares.Where(p => p.Fam_AperendizId == aluno);

                var soma = familiares.Select(p => p.Fam_Renda).Sum();
                if (!TBNumeroFamiliares.Text.Equals(string.Empty))
                {
                    var renda = soma / Convert.ToInt32(TBNumeroFamiliares.Text);
                    return string.Format(" R$ {0:f2}", renda ?? 0);
                }
                else
                    return "R$ 0,00"; // não há familiares com renda, sendo assim retorna zero.
            }


        }

        private string ValidaData(DateTime data)
        {
            var datastring = string.Format("{0:dd/MM/yyy}", data);
            if (datastring.Equals("01/01/1900")) return "";
            return datastring;
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


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            try
            {

                if (TBnomeAlu.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome do aprendiz.");

                if (DDUnidade.SelectedValue.Equals(string.Empty))
                {
                    throw new ArgumentException("Unidade é obrigatória");
                }
                if (TBnomeAlu.Text.Split(' ').Count() < 2) throw new ArgumentException("Nome do estudante deve conter pelo menos nome e sobrenome.");
                if (DDsexoAlu.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o sexo do aprendiz.");
                if (DDAreaAtuacao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione a área de atuação do aprendiz.");
                if (DDPlanoCurricular.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o plano curricular do aprendiz.");
                if (DD_situacao_Aprendiz.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione a situação do aprendiz.");
                if (TBdataNascAlu.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de nascimento do aprendiz.");
                DateTime data;
                if (!DateTime.TryParse(TBdataNascAlu.Text, out data)) throw new ArgumentException(" Data de Nascimento inválida.");

                if (!TBtelefoneEndAlu.Text.Equals(""))
                {

                }
              //  if (!Funcoes.ValidaTelefone(TBtelefoneEndAlu.Text)) throw new ArgumentException("Telefone inválido.");
                if (!TBcelularEndAlu.Text.Equals(""))
                {

                }

                // if (!Funcoes.ValidaTelefone(TBcelularEndAlu.Text)) throw new ArgumentException("Telefone celular inválido.");
                if (!TBemail.Text.Equals(""))
                    if (!Funcoes.ValidaEmail(TBemail.Text)) throw new ArgumentException("E-mail inválido.");
                if (MultiView1.ActiveViewIndex.Equals(1)) //view dos dados socio economocis
                    if (TBNumeroFamiliares.Text.Equals(string.Empty)) throw new ArgumentException("Digite o número de familiares.");

                if (TBCPFalu.Text.Equals(string.Empty)) throw new ArgumentException("Digite o CPF.");

                if (Funcoes.Retirasimbolo(TBCPFalu.Text).Length != 11) throw new ArgumentException("CPF deve possuir 11 dígitos");

                //carrega o repositório --- caso seja uma alteração, procura no banco um aprendiz ---
                using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
                {
                    if (!Session["Comando"].Equals("Inserir") && (Session["matricula"] == null || Session["matricula"].Equals(string.Empty)))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                            "javascript:reload();", true);
                        return;
                    }

                    var aprendiz = Session["Comando"].Equals("Inserir") ? new Aprendiz() : repository.Find(Convert.ToInt32(Session["matricula"]));






                    //// nova alteracao Gabriel
                    /*
                                         var apr=  new ViewModelAprendiz();
                                       // aprendiz.AtualizaAprendiz(new ViewModelAprendiz()                  
                                        // ----------------------- Dados do Aluno -------------------------------
                                        apr.Apr_Nome = TBnomeAlu.Text;
                                        apr.Apr_Codigo = Convert.ToInt32(TBcodigoAlu.Text);
                                        apr.Apr_DataDeNascimento = TBdataNascAlu.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(TBdataNascAlu.Text);
                                        apr.Apr_Sexo = DDsexoAlu.SelectedValue;
                                        apr.Apr_Situacao = Convert.ToInt16(DD_situacao_Aprendiz.SelectedValue);
                                        apr.Apr_DataCadastro = TB_Data_Cadastro.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(TB_Data_Cadastro.Text);
                                        apr.AprEstadoCivil = DDestCivilAlu.SelectedValue;
                  
                                        apr.Apr_Nacionalidade = TBpaisAlu.Text;
                                        apr.Apr_Naturalidade = TBcidadeAlu.Text;
                                        apr.Apr_UF_Nat = DD_estado_Nat.SelectedValue;
                   
                                        if (!DDcoleOrigem.SelectedValue.Equals(string.Empty)) apr.AprCodEscola = Convert.ToInt32(DDcoleOrigem.SelectedValue);
                   
                                       apr.Apr_Escola_HInicio = TSinicio.Hour.Equals(0) ? new DateTime(1900, 01, 01) : new DateTime(1900, 1, 1, TSinicio.Hour, TSinicio.Minute, 0);
                                       apr.Apr_Escola_HTermino = TSinicio.Hour.Equals(0) ? new DateTime(1900, 01, 01) : new DateTime(1900, 1, 1, TSfim.Hour, TSfim.Minute, 0);
                 
                                        apr.Apr_NomePai = TBnomePai.Text;
                                        apr.Apr_NomeMae = TBnomeMae.Text;
                                        apr.Apr_CarteiraDeIdentidade = TBRGalu.Text;
                                        apr.Apr_DataEmissão_Ident = TBdataEmissRGAlu.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(TBdataEmissRGAlu.Text);
                                        apr.Apr_OrgaoEmissor_Ident = TBorgExpRGAlu.Text;
                                        apr.Apr_UF_Ident = DDufRGAlu.SelectedValue;
                                        apr.Apr_CPF = Funcoes.Retirasimbolo(TBCPFalu.Text);
                                        apr.Apr_PIS = TB_numero_pis.Text;
                                        apr.Apr_CarteiraDeTrabalho = TBnumCTPSresp.Text;
                                        apr.Apr_Serie_Cartrab = TBserieCTPSResp.Text;
                                        apr.Apr_DataEmissão_CartTrab = TB_DataEmissao_ctps.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(TB_DataEmissao_ctps.Text);
                                        apr.Apr_PNE = TB_numero_pne.Text;
                                        apr.Apr_Endereço = TBruaEndAlu.Text;

                                        apr.Apr_Turma = Convert.ToInt32(DDTurma.SelectedValue);
                                        apr.Apr_CertReservista = TB_cert_reservista.Text;
                                        apr.Apr_NumeroMTB = TBNumeroMTB.Text;

                                        apr.Apr_NumeroEndereco = TBnumEndAlu.Text;
                                        apr.Apr_Complemento = TBcomplementoEndAlu.Text;
                                        apr.Apr_Bairro = TBBairroAlu.Text;
                                        apr.Apr_Cidade = TBcidadeEndalu.Text;
                                        apr.Apr_UF = DDufEndAlu.SelectedValue;
                                        apr.Apr_CEP = Funcoes.Retirasimbolo(TBCepEndAlu.Text);
                                        apr.Apr_Telefone = Funcoes.Retirasimbolo(TBtelefoneEndAlu.Text);
                                        apr.Apr_Celular = Funcoes.Retirasimbolo(TBcelularEndAlu.Text);
                                        apr.Apr_Email = TBemail.Text;
                                        apr.Apr_UF_CartTrab = DD_uf_ctps.SelectedValue;
                                        apr.Apr_Observacoes = TB_observacao.Text;
                                        apr.Apr_Escolaridade = DD_escolaridade_aprendiz.SelectedValue.Equals(string.Empty) ? 0 : Convert.ToInt32(DD_escolaridade_aprendiz.SelectedValue);
                                        apr.Apr_Tutor = DDusuario.SelectedValue;

                                        apr.Apr_senha = Session["Comando"].Equals("Inserir") ? "1" : TB_senha_acesso.Text;
                                        //FIM DADOS ALUNOS ----------------------- Dados do Aluno -------------------------------

                                        // ----------------------- Dados bancários -------------------------------
                                        apr.AprBanco = DDbanco.SelectedValue;
                                        apr.AprTipoConta = DDTipoPoupanca.SelectedValue;
                                        apr.AprAgencia = TBAgencia.Text;
                                        apr.AprContaBancaria = TBNumeroConta.Text;

                                        // ----------------------- Dados do Responsável -------------------------------
                                        apr.Apr_Resp_CPF = Funcoes.Retirasimbolo(TB_cpf_resp.Text);
                                        apr.Apr_Responsavel = TB_nome_resp.Text;
                                        apr.Apr_Telefone_Resp = Funcoes.Retirasimbolo(TB_tel_resp.Text);
                                        apr.Apr_Celular_Resp = Funcoes.Retirasimbolo(TB_cel_resp.Text);
                                        apr.Apr_Tipo_Contato = TB_recado_nome.Text;
                                        apr.Apr_Telefone_Contato = Funcoes.Retirasimbolo(TB_recado_tel.Text);
                                        apr.Apr_Resp_EstadoCivil = DDSituacao_resp.SelectedValue;
                                        apr.Apr_Resp_GrauInstrucao = DDescolaridade_resp.SelectedValue;                  
                                        if (!DDprofissao_resp.SelectedValue.Equals(string.Empty)) apr.Apr_Resp_Profissao = Convert.ToInt32(DDprofissao_resp.SelectedValue);
                                        apr.Apr_Resp_Observacoes = TB_observacao_resp.Text;
                    
                                        // ----------------------- Dados sócio-Econômicos -------------------------------
                                        apr.Apr_beneficio = DDBeneficio.SelectedValue;
                                        apr.Apr_BolsaFamilia = TB_bolsa_familia.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TB_bolsa_familia.Text);
                                        apr.Apr_pensao = TB_pensao.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TB_pensao.Text);
                                        apr.Apr_outros = TB_valor_outros.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TB_valor_outros.Text);
                                        apr.Apr_SituacaoResidencia = DDTipoResidencia.SelectedValue.Equals(string.Empty) ? null : DDTipoResidencia.SelectedValue;
                                        apr.Apr_aluguel = TBValorAluguel.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TBValorAluguel.Text);
                                        apr.Apr_CarteiraAssinada = DDcarteiraAssinada.SelectedValue.Equals(string.Empty) ? null : DDcarteiraAssinada.SelectedValue;
                                        apr.Apr_Empresa = TB_Empresa.Text;
                                        apr.Apr_Cargo = TB_Cargo.Text;
                                        apr.Apr_TempoPermanencia = TB_permanencia.Text;
                                        apr.Apr_UsaMedicamento = DDmedicamentos.SelectedValue.Equals(string.Empty) ? null : DDmedicamentos.SelectedValue;
                                        apr.Apr_NomeMedicamento = TB_medicamento.Text;
                                        apr.Apr_TipoMedicamento = TB_finalidade_medicamento.Text;
                                        apr.Apr_Alergia = DDAlergia.SelectedValue.Equals(string.Empty) ? null : DDAlergia.SelectedValue;
                                        apr.Apr_TipoAlergia = TB_tipo_alergia.Text;
                                        apr.Apr_Doenca = DDdoenca.SelectedValue.Equals(string.Empty) ? null : DDdoenca.SelectedValue;
                                        apr.Apr_NomeDoenca = TB_tipo_doenca.Text;
                                        apr.Apr_numeroFamiliares = (Int16)Convert.ToInt16(TBNumeroFamiliares.Text);
                                        apr.Apr_RecebeBeneficio = DDRecebeBeneficio.SelectedValue.Equals(string.Empty) ? null : DDRecebeBeneficio.SelectedValue;
                                        apr.Apr_InicioAprendizagem = TB_inicio_aprendizagem.Text.Equals(string.Empty) ? (DateTime?)null : DateTime.Parse(TB_inicio_aprendizagem.Text);
                                        apr.Apr_FimAprendizagem = TB_data_desligamento.Text.Equals(string.Empty) ? (DateTime?)null : DateTime.Parse(TB_data_desligamento.Text);
                                        apr.Apr_PrevFimAprendizagem = TB_prev_Fim_aprendizagem.Text.Equals(string.Empty) ? (DateTime?)null : DateTime.Parse(TB_prev_Fim_aprendizagem.Text);
                                        apr.Apr_Mensagem = TB_mensagem.Text.Equals(string.Empty) ? null : TB_mensagem.Text;

                   
                                        var engine = new AprendizEngine(repository);
                                        if (Session["Comando"].Equals("Inserir"))
                                        {
                                            engine.CriarAprendiz(aprendiz);
                                        }
                                        else
                                        {
                                            engine.AtualizarAprendiz(aprendiz);
                                        }


                                        if (Session["Comando"].Equals("Inserir"))
                                        {
                                            var cpf = repository.All().Where(p => p.Apr_CPF == Funcoes.Retirasimbolo(TBCPFalu.Text)).ToList();
                                            if (cpf.Any()) throw new ArgumentException("Já existe um aluno cadastrado com este CPF. Matricula: " + cpf.First().Apr_Codigo + ". Nome: " + cpf.First().Apr_Nome);
                                        }
                     */
                    ///FIM nova alteracao Gabriel

                    //---------------- dados novos thassio ----------------------------------

                    // if (DDEstudaAtualmente.SelectedValue.Equals(string.Empty)) throw new ArgumentException("O campo Estuda atualmente é obrigatório.");
                    // if (txtNumeroExterno.Text.Equals(string.Empty)) throw new ArgumentException("O campo Número Externo é obrigatório.");
                    // if (txtCBO.Text.Equals(string.Empty)) throw new ArgumentException("O campo CBO é obrigatório.");


                    //aprendiz.Apr_DataInicioEmpresa = txtInicioEmpresa.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(txtInicioEmpresa.Text);
                    aprendiz.Apr_DataInicioEmpresa = txtInicioEmpresa.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(txtInicioEmpresa.Text);
                    aprendiz.Apr_NumSistExterno = txtNumeroExterno.Text.Equals(string.Empty) ? "" : txtNumeroExterno.Text;
                    aprendiz.Apr_TurnoEscolar = DDTurno.SelectedValue;
                    aprendiz.Apr_Estudante = DDEstudaAtualmente.SelectedValue;
                    aprendiz.Apr_Deficiencia = DDDeficiencia.SelectedValue;
                    aprendiz.Apr_CBO = txtCBO.Text.Equals(string.Empty) ? "" : txtCBO.Text;

                    // ----------------------- Dados do Aluno -------------------------------

                    aprendiz.Apr_Nome = TBnomeAlu.Text.ToUpper();
                    if (!TBcodigoAlu.Text.Equals(string.Empty)) aprendiz.Apr_Codigo = Convert.ToInt32(TBcodigoAlu.Text);
                    aprendiz.Apr_DataDeNascimento = TBdataNascAlu.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(TBdataNascAlu.Text);
                    if (!DDsexoAlu.SelectedValue.Equals(string.Empty)) aprendiz.Apr_Sexo = DDsexoAlu.SelectedValue;
                    if (!DD_situacao_Aprendiz.SelectedValue.Equals(string.Empty)) aprendiz.Apr_Situacao = Convert.ToInt16(DD_situacao_Aprendiz.SelectedValue);
                    aprendiz.Apr_DataCadastro = TB_Data_Cadastro.Text.Equals(string.Empty) ? DateTime.Today : Convert.ToDateTime(TB_Data_Cadastro.Text);
                    aprendiz.AprEstadoCivil = DDestCivilAlu.SelectedValue;
                    if (!TBpaisAlu.Text.Equals(string.Empty)) aprendiz.Apr_Nacionalidade = TBpaisAlu.Text;

                    // if (!TBcidadeAlu.Text.Equals(string.Empty)) aprendiz.Apr_Naturalidade = TBcidadeAlu.Text;
                    if (!DDMunicipio.SelectedValue.Equals(string.Empty)) aprendiz.Apr_Naturalidade = DDMunicipio.SelectedValue;

                    if (!DD_estado_Nat.SelectedValue.Equals(string.Empty)) aprendiz.Apr_UF_Nat = DD_estado_Nat.SelectedValue;
                    if (!DDcoleOrigem.SelectedValue.Equals(string.Empty)) aprendiz.AprCodEscola = Convert.ToInt32(DDcoleOrigem.SelectedValue);
                    //aprendiz.Apr_Escola_HInicio = TSinicio.Hour.Equals(0) ? new DateTime(1900, 1, 1, TSinicio.Hour, TSinicio.Minute, 0) : new DateTime(1900, 1, 1, TSinicio.Hour, TSinicio.Minute, 0);
                    //aprendiz.Apr_Escola_HTermino = TSfim.Hour.Equals(0) ? new DateTime(1900, 1, 1, TSfim.Hour, TSfim.Minute, 0) : new DateTime(1900, 1, 1, TSfim.Hour, TSfim.Minute, 0);

                    aprendiz.Apr_Escola_HInicio = new DateTime(1900, 1, 1);
                    aprendiz.Apr_Escola_HTermino = new DateTime(1900, 1, 1);

                    if (!TBnomePai.Text.Equals(string.Empty)) aprendiz.Apr_NomePai = TBnomePai.Text.ToUpper();
                    if (!TBnomeMae.Text.Equals(string.Empty)) aprendiz.Apr_NomeMae = TBnomeMae.Text.ToUpper();
                    if (!TBRGalu.Text.Equals(string.Empty)) aprendiz.Apr_CarteiraDeIdentidade = TBRGalu.Text;
                    aprendiz.Apr_DataEmissão_Ident = TBdataEmissRGAlu.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(TBdataEmissRGAlu.Text);
                    if (!TBorgExpRGAlu.Text.Equals(string.Empty)) aprendiz.Apr_OrgaoEmissor_Ident = TBorgExpRGAlu.Text;
                    if (!DDufRGAlu.SelectedValue.Equals(string.Empty)) aprendiz.Apr_UF_Ident = DDufRGAlu.SelectedValue;
                    if (!TBCPFalu.Text.Equals(string.Empty)) aprendiz.Apr_CPF = Funcoes.Retirasimbolo(TBCPFalu.Text);

                    if (!Funcoes.ValidaCPF(TBCPFalu.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('ERRO - CPF não válido');", true);
                        return;
                    }

                    if (!TB_numero_pis.Text.Equals(string.Empty)) aprendiz.Apr_PIS = TB_numero_pis.Text;
                    if (!TBnumCTPSresp.Text.Equals(string.Empty)) aprendiz.Apr_CarteiraDeTrabalho = TBnumCTPSresp.Text;
                    if (!TBserieCTPSResp.Text.Equals(string.Empty)) aprendiz.Apr_Serie_Cartrab = TBserieCTPSResp.Text;
                    //  aprendiz.Apr_DataEmissão_CartTrab = TB_DataEmissao_ctps.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(TB_DataEmissao_ctps.Text);
                    // if (!TB_numero_pne.Text.Equals(string.Empty)) aprendiz.Apr_PNE = TB_numero_pne.Text;
                    if (!TBruaEndAlu.Text.Equals(string.Empty)) aprendiz.Apr_Endereço = TBruaEndAlu.Text;


                    if (!DDTurma.SelectedValue.Equals(string.Empty)) aprendiz.Apr_Turma = Convert.ToInt32(DDTurma.SelectedValue);
                    if (!DDAreaAtuacao.SelectedValue.Equals(string.Empty)) aprendiz.Apr_AreaAtuacao = Convert.ToInt32(DDAreaAtuacao.SelectedValue);
                    if (!DDPlanoCurricular.SelectedValue.Equals(string.Empty)) aprendiz.Apr_PlanoCurricular = Convert.ToInt16(DDPlanoCurricular.SelectedValue);

                    aprendiz.Apr_CertReservista = TB_cert_reservista.Text;
                    //    aprendiz.Apr_NumeroMTB = TBNumeroMTB.Text;

                    if (!TBnumEndAlu.Text.Equals(string.Empty)) aprendiz.Apr_NumeroEndereco = TBnumEndAlu.Text;
                    if (!TBcomplementoEndAlu.Text.Equals(string.Empty)) aprendiz.Apr_Complemento = TBcomplementoEndAlu.Text;
                    if (!txtBairro.Text.Equals(string.Empty)) aprendiz.Apr_Bairro = txtBairro.Text;

                    // if (!DDBairro.SelectedValue.Equals(string.Empty)) aprendiz.Apr_Bairro = DDBairro.SelectedValue;

                    // if (!TBcidadeEndalu.Text.Equals(string.Empty)) aprendiz.Apr_Cidade = TBcidadeEndalu.Text;
                    //if (!DDMunicipioEndereco.SelectedValue.Equals(string.Empty)) aprendiz.Apr_Cidade = DDMunicipioEndereco.SelectedValue;
                    if (!txtMunicipioEndereco.Text.Equals(string.Empty)) aprendiz.Apr_Cidade = txtMunicipioEndereco.Text;

                    if (!DDufEndAlu.SelectedValue.Equals(string.Empty)) aprendiz.Apr_UF = DDufEndAlu.SelectedValue;
                    if (!TBCepEndAlu.Text.Equals(string.Empty)) aprendiz.Apr_CEP = Funcoes.Retirasimbolo(TBCepEndAlu.Text);
                   
                    if (!TBtelefoneEndAlu.Text.Equals(string.Empty)) aprendiz.Apr_Telefone = Funcoes.Retirasimbolo(TBtelefoneEndAlu.Text);

                    if (aprendiz.Apr_Telefone != null)
                    {
                        if (aprendiz.Apr_Telefone.Count() > 10)
                        {
                            throw new ArgumentException("Telefone inválido");
                        }
                    }

                    if (!TBcelularEndAlu.Text.Equals(string.Empty)) aprendiz.Apr_Celular = Funcoes.Retirasimbolo(TBcelularEndAlu.Text);
                    if (!TBemail.Text.Equals(string.Empty)) aprendiz.Apr_Email = TBemail.Text;
                    // if (!DD_uf_ctps.SelectedValue.Equals(string.Empty)) aprendiz.Apr_UF_CartTrab = DD_uf_ctps.SelectedValue;
                    if (!TB_observacao.Text.Equals(string.Empty)) aprendiz.Apr_Observacoes = TB_observacao.Text;
                    aprendiz.Apr_Escolaridade = DD_escolaridade_aprendiz.SelectedValue.Equals(string.Empty) ? 0 : Convert.ToInt32(DD_escolaridade_aprendiz.SelectedValue);
                    aprendiz.Apr_Tutor = DDusuario.SelectedValue;
                    aprendiz.Apr_senha = Session["Comando"].Equals("Inserir") ? "1" : TB_senha_acesso.Text;

                    aprendiz.Apr_DataContrato = TBDataContrato.Text.Equals(string.Empty) ? new DateTime(1900, 01, 01) : Convert.ToDateTime(TBDataContrato.Text);

                    //teste

                    aprendiz.Apr_Unidade = DDUnidade.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DDUnidade.SelectedValue);
                    aprendiz.Apr_InstParceira = DDInstituicaoParceira.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DDInstituicaoParceira.SelectedValue);

                    aprendiz.Apr_TipoAprendizagem = DDTipoAprendizagem.SelectedValue.Equals(string.Empty) ? "" : DDTipoAprendizagem.SelectedValue;
                    aprendiz.Apr_MesesContrato = txtMesesContrato.Text.Equals(string.Empty) ? new short() : short.Parse(txtMesesContrato.Text);
                    aprendiz.Apr_HorasDiarias = txtHorasDiarias.Text.Equals(string.Empty) ? new short() : short.Parse(txtHorasDiarias.Text);
                    aprendiz.Apr_TurmaCCI = DDTurmaCCI.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DDTurmaCCI.SelectedValue);
                    aprendiz.Apr_TurmaENC = DDTurmaEnc.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DDTurmaEnc.SelectedValue);

                    // ----------------------- Dados bancários -------------------------------
                    //aprendiz.AprBanco = DDbanco.SelectedValue;
                    //aprendiz.AprTipoConta = DDTipoPoupanca.SelectedValue;
                    //aprendiz.AprAgencia = TBAgencia.Text;
                    //aprendiz.AprContaBancaria = TBNumeroConta.Text;

                    // ----------------------- Dados do Responsável -------------------------------
                    if (!TB_cpf_resp.Text.Equals(string.Empty)) aprendiz.Apr_Resp_CPF = Funcoes.Retirasimbolo(TB_cpf_resp.Text);
                    if (!TB_nome_resp.Text.Equals(string.Empty)) aprendiz.Apr_Responsavel = TB_nome_resp.Text;
                    if (!TB_tel_resp.Text.Equals(string.Empty)) aprendiz.Apr_Telefone_Resp = Funcoes.Retirasimbolo(TB_tel_resp.Text);
                    if (!TB_cel_resp.Text.Equals(string.Empty)) aprendiz.Apr_Celular_Resp = Funcoes.Retirasimbolo(TB_cel_resp.Text);
                    if (!TB_recado_nome.Text.Equals(string.Empty)) aprendiz.Apr_Tipo_Contato = TB_recado_nome.Text;
                    if (!TB_recado_tel.Text.Equals(string.Empty)) aprendiz.Apr_Telefone_Contato = Funcoes.Retirasimbolo(TB_recado_tel.Text);
                    if (!TB_observacao_resp.Text.Equals(string.Empty)) aprendiz.Apr_Resp_Observacoes = TB_observacao_resp.Text;

                    // ----------------------- Dados sócio-Econômicos -------------------------------


                    if (!DDBeneficio.SelectedValue.Equals(string.Empty)) aprendiz.Apr_beneficio = DDBeneficio.SelectedValue;
                    aprendiz.Apr_BolsaFamilia = TB_bolsa_familia.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TB_bolsa_familia.Text);
                    aprendiz.Apr_pensao = TB_pensao.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TB_pensao.Text);
                    aprendiz.Apr_outros = TB_valor_outros.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TB_valor_outros.Text);
                    aprendiz.Apr_SituacaoResidencia = DDTipoResidencia.SelectedValue.Equals(string.Empty) ? null : DDTipoResidencia.SelectedValue;
                    aprendiz.Apr_aluguel = TBValorAluguel.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TBValorAluguel.Text);
                    aprendiz.Apr_CarteiraAssinada = DDcarteiraAssinada.SelectedValue.Equals(string.Empty) ? null : DDcarteiraAssinada.SelectedValue;
                    aprendiz.Apr_Empresa = TB_Empresa.Text;
                    aprendiz.Apr_Cargo = TB_Cargo.Text;
                    aprendiz.Apr_TempoPermanencia = TB_permanencia.Text;
                    aprendiz.Apr_UsaMedicamento = DDmedicamentos.SelectedValue.Equals(string.Empty) ? null : DDmedicamentos.SelectedValue;
                    aprendiz.Apr_NomeMedicamento = TB_medicamento.Text;
                    aprendiz.Apr_TipoMedicamento = TB_finalidade_medicamento.Text;
                    aprendiz.Apr_Alergia = DDAlergia.SelectedValue.Equals(string.Empty) ? null : DDAlergia.SelectedValue;
                    aprendiz.Apr_TipoAlergia = TB_tipo_alergia.Text;
                    aprendiz.Apr_Doenca = DDdoenca.SelectedValue.Equals(string.Empty) ? null : DDdoenca.SelectedValue;
                    aprendiz.Apr_NomeDoenca = TB_tipo_doenca.Text;
                    if (TBNumeroFamiliares.Text.Equals(string.Empty))
                        aprendiz.Apr_numeroFamiliares = 1; //numeros de familiares começa com 1
                    else
                        aprendiz.Apr_numeroFamiliares = (Int16)Convert.ToInt16(TBNumeroFamiliares.Text);

                    aprendiz.Apr_RecebeBeneficio = DDRecebeBeneficio.SelectedValue.Equals(string.Empty) ? null : DDRecebeBeneficio.SelectedValue;



                    aprendiz.Apr_DataPrevInicFerias = txtInicioFerias.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtInicioFerias.Text);
                    aprendiz.Apr_DataPrevTermFerias = txtTerminoFerias.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtTerminoFerias.Text);

                    aprendiz.Apr_InicioAprendizagem = TB_inicio_aprendizagem.Text.Equals(string.Empty) ? (DateTime?)null : DateTime.Parse(TB_inicio_aprendizagem.Text);
                    aprendiz.Apr_FimAprendizagem = TB_data_desligamento.Text.Equals(string.Empty) ? (DateTime?)null : DateTime.Parse(TB_data_desligamento.Text);
                    aprendiz.Apr_PrevFimAprendizagem = TB_prev_Fim_aprendizagem.Text.Equals(string.Empty) ? (DateTime?)null : DateTime.Parse(TB_prev_Fim_aprendizagem.Text);

                    aprendiz.Apr_Mensagem = TB_mensagem.Text.Equals(string.Empty) ? null : TB_mensagem.Text;
                    if (!TB_DataExpiracao.Text.Equals(string.Empty))
                        aprendiz.Apr_ValidadeMensagem = DateTime.Parse(TB_DataExpiracao.Text);
                    else
                        aprendiz.Apr_ValidadeMensagem = null;

                    if (Session["Comando"].Equals("Inserir"))
                    {
                        aprendiz.Apr_UsuarioCadastro = Session["codigo"].ToString();
                        aprendiz.Apr_UsuarioDataCadastro = DateTime.Today;
                    }
                    else
                    {
                        aprendiz.Apr_UsuarioAlteracao = Session["codigo"].ToString();
                        aprendiz.Apr_UsuarioDataAlteracao = DateTime.Today;
                    }

                    if (!DD_grau_Parentesco.SelectedValue.Equals(string.Empty)) aprendiz.Apr_Resp_Parentesco = int.Parse(DD_grau_Parentesco.SelectedValue);
                    else DD_grau_Parentesco.SelectedValue = null;

                    // ----------------------------------------------------------------------------

                    if (Session["Comando"].Equals("Inserir"))
                        repository.Add(aprendiz);
                    else
                    {
                        if (!aprendiz.Apr_FimAprendizagem.Equals("") && !aprendiz.Apr_FimAprendizagem.Equals(null))
                        {
                            var sql = @"delete CA_AulasDisciplinasAprendiz where AdiCodAprendiz = " + aprendiz.Apr_Codigo + " and AdiDataAula > '" + aprendiz.Apr_FimAprendizagem + "'";
                            var con = new Conexao();
                            con.Alterar(sql);
                        }

                        repository.Edit(aprendiz);
                        ControlaAlocacoes(aprendiz);
                    }
                    Alert.Show("Ação realizada com sucesso.");
                    btnsalvar.Visible = false;
                }
            }
            catch (ArgumentException ex)
            {
                Alert.Show(ex.Message);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000145", ex);
            }
        }



        private void ControlaAlocacoes(Aprendiz aprendiz)
        {
            try
            {
                int situacao;
                if (!int.TryParse(DD_situacao_Aprendiz.SelectedValue, out situacao)) return;

                if (situacao >= 2 && situacao <= 5)
                {
                    using (var repository = new Repository<AprendizUnidade>(new Context<AprendizUnidade>()))
                    {

                        var alocacoes = repository.All().Where(p => p.ALAAprendiz == aprendiz.Apr_Codigo && p.ALAStatus == "A");
                        foreach (var item in alocacoes)
                        {
                            item.ALAStatus = "I";
                            item.ALADataTermino = item.ALADataPrevTermino;
                            repository.Edit(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000146", ex);
            }
        }


        #region GetMunicipios

        [WebMethod]
        public static string[] GetMunicipios(String prefixText, int count)
        {
            var values = new List<string>();
            string commandText = "SELECT TOP " + count.ToString()
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void DDcarteiraAssinada_SelectedIndexChanged(object sender, EventArgs e)
        {
            PNcarteira.Enabled = DDcarteiraAssinada.SelectedValue.Equals("S");
        }

        protected void btn_adicionar_Click(object sender, EventArgs e)
        {
            DDvinculo.SelectedValue = "1";
            DDProfissao_fam.SelectedValue = "1";
            Session["CommandArgument"] = "Inserir";
            LimpaFamilia();
            PNnovo.Visible = true;
            PNFamilia.Visible = false;
        }

        private void BindParentesco()
        {
            using (var repository = new Repository<GrauParentesco>(new Context<GrauParentesco>()))
            {
                var datasource = new List<GrauParentesco>();
                datasource.AddRange(repository.All());
                DD_parentesco.DataSource = datasource.OrderBy(p => p.GpaDescricao);
                DD_parentesco.DataBind();
            }
        }

        private void BindProfissao()
        {
            using (var repository = new Repository<Profissoes>(new Context<Profissoes>()))
            {
                var datasource = new List<Profissoes>();
                datasource.AddRange(repository.All());
                DDProfissao_fam.DataSource = datasource.OrderBy(g => g.ProfDescricao);
                DDProfissao_fam.DataBind();
            }
        }

        private void BindVinculo()
        {
            using (var repository = new Repository<TipoEmprego>(new Context<TipoEmprego>()))
            {
                var datasource = new List<TipoEmprego>();
                datasource.AddRange(repository.All());
                DDvinculo.DataSource = datasource;
                DDvinculo.DataBind();
            }
        }

        private void LimpaFamilia()
        {
            tb_nome_parente.Text = string.Empty;
            TB_data_Nasc_fam.Text = string.Empty;
            TB_renda_mensal.Text = string.Empty;
            BindParentesco();
            BindProfissao();
            BindVinculo();
        }


        private void BindFamilia()
        {
            try
            {
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var datasource = from i in bd.CA_Membro_Familias
                                     join p in bd.CA_Aprendizs on i.Fam_AperendizId equals p.Apr_Codigo
                                     join m in bd.CA_GrauParentescos on i.Fam_GrauParentescoId equals m.GpaCodigo
                                     join n in bd.CA_Profissoes on i.Fam_ProfissaoId equals n.ProfCodigo
                                     join s in bd.CA_Vinculo_Empregaticios on i.Fam_TipoEmpregoId equals s.Vin_Codigo
                                     where p.Apr_Codigo.Equals(Session["matricula"])

                                     select new { i.Fam_Ordem, i.Fam_Nome, i.Fam_DataNascimento, m.GpaDescricao, n.ProfDescricao, s.Vin_Descricao, i.Fam_Renda, i.Fam_Identidade, i.Fam_OrgaoIdentidade };

                    listaconteudo.DataSource = datasource;
                    listaconteudo.DataBind();
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000158", ex);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_nome_parente.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome do familiar.");
                //if (TB_data_Nasc_fam.Text.Equals(string.Empty)) throw new ArgumentException("Digite a data de nascimento do familiar.");
                if (DD_parentesco.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o grau de parentesco do familiar.");
                //if (DDProfissao_fam.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione a profisão do familiar.");
                //  if (DDvinculo.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o tipo de emprego do familiar.");
                if (TB_renda_mensal.Text.Equals(string.Empty)) throw new ArgumentException("Digite a renda mensal do familiar.");

                using (var repository = new Repository<MembroFamilia>(new Context<MembroFamilia>()))
                {
                    var membro = Session["CommandArgument"].Equals("Inserir") ? new MembroFamilia() : repository.Find(Convert.ToInt32(Session["Alteracodigo"]));
                    if (Session["CommandArgument"].Equals("Inserir"))



                        membro.Fam_AperendizId = int.Parse(Session["matricula"].ToString());
                    membro.Fam_Nome = tb_nome_parente.Text;
                    if (!TB_data_Nasc_fam.Text.Equals(string.Empty))
                        membro.Fam_DataNascimento = DateTime.Parse(TB_data_Nasc_fam.Text);
                    else
                        membro.Fam_DataNascimento = null;
                    membro.Fam_GrauParentescoId = int.Parse(DD_parentesco.SelectedValue);
                    membro.Fam_ProfissaoId = DDProfissao_fam.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DDProfissao_fam.SelectedValue);
                    membro.Fam_TipoEmpregoId = DDvinculo.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DDvinculo.SelectedValue);
                    membro.Fam_Renda = TB_renda_mensal.Text.Equals(string.Empty) ? 0 : float.Parse(TB_renda_mensal.Text);

                    membro.Fam_Identidade = txtNumeroIdentidade.Text;
                    membro.Fam_OrgaoIdentidade = txtOrgaoIdentidade.Text;

                    if (Session["CommandArgument"].Equals("Inserir")) repository.Add(membro);
                    else repository.Edit(membro);
                    BindFamilia();
                    tb_renda_percapta.Text = CalculaRenda(int.Parse(Session["matricula"].ToString()));
                    PNnovo.Visible = false;
                    PNFamilia.Visible = true;
                    Alert.Show("Ação realizada com sucesso.");
                }
            }
            catch (ArgumentException ex)
            {
                Alert.Show(ex.Message);
            }
            catch (Exception ex)
            {
                //  Funcoes.TrataExcessao("000125", ex);
            }
        }

        protected void btn_familia_Click(object sender, EventArgs e)
        {
            if (!Session["matricula"].Equals(""))
            {
                MultiView1.ActiveViewIndex = 2;
                BindFamilia();
            }
            else
            {
                Alert.Show("Sua sessão expirou. Faça um novo login e tente novamente!");
                return;
            }
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            if (Session["enable_Save"].Equals("Aluno") || Session["enable_Save"].Equals("Parceiro"))
            {
                Alert.Show("Não Habilitado para seu perfil.");
                return;
            }


            Session["CommandArgument"] = "Alterar";
            LimpaFamilia();
            using (var repository = new Repository<MembroFamilia>(new Context<MembroFamilia>()))
            {
                var bt = (Button)sender;
                var membro = repository.Find(Convert.ToInt32(bt.CommandArgument));
                Session["Alteracodigo"] = bt.CommandArgument;
                tb_nome_parente.Text = membro.Fam_Nome;
                TB_data_Nasc_fam.Text = string.Format("{0:dd/MM/yyyy}", membro.Fam_DataNascimento);
                DD_parentesco.SelectedValue = membro.Fam_GrauParentescoId.ToString();
                DDProfissao_fam.SelectedValue = membro.Fam_ProfissaoId.ToString();
                DDvinculo.SelectedValue = membro.Fam_TipoEmpregoId.ToString();
                TB_renda_mensal.Text = string.Format("{0:F}", membro.Fam_Renda);
                txtNumeroIdentidade.Text = membro.Fam_Identidade;
                txtOrgaoIdentidade.Text = membro.Fam_OrgaoIdentidade;
            }
            PNnovo.Visible = true;
            PNFamilia.Visible = false;
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {

            if (Session["enable_Save"].Equals("Aluno") || Session["enable_Save"].Equals("Parceiro"))
            {
                Alert.Show("Não Habilitado para seu perfil.");
                return;
            }

            using (var repository = new Repository<MembroFamilia>(new Context<MembroFamilia>()))
            {
                var button = (Button)sender;
                var aprendiz = Convert.ToInt32(button.CommandArgument);
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(aprendiz);
                BindFamilia();
                PNnovo.Visible = false;
                PNFamilia.Visible = true;
            }
        }

        protected void btn_altera_email_Click(object sender, EventArgs e)
        {
            try
            {
                using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
                {
                    var aprendiz = repository.Find(Convert.ToInt32(Session["matricula"]));
                    if (Convert.ToBoolean(HFConfirma.Value))
                    {
                        if (!Funcoes.ValidaEmail(TBemail.Text)) throw new ArgumentException("O e-mail informado não é válido.");
                        aprendiz.Apr_Email = TBemail.Text;
                        repository.Edit(aprendiz);
                        Alert.Show("Seu e-mail foi alterado com sucesso.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Alert.Show(ex.Message);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000126", ex);
            }
        }


        //Parte responsável pela exibição dos dados da alocação do aluno.
        private void BindGridView()
        {
            try
            {
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var list = new List<View_AlocacoesAluno>();
                    var dados02 = from i in bd.View_AlocacoesAlunos where i.ALAAprendiz.Equals(int.Parse(Session["matricula"].ToString())) select i;
                    list.AddRange(dados02.ToList());
                    HFRowCount.Value = list.Count().ToString();
                    GridView1.DataSource = list;
                    pn_info.Visible = (list.Count().Equals(0));
                    GridView1.DataBind();
                    BindGridViewFrequencia(int.Parse(Session["matricula"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000156", ex);
            }
        }


        private void BindGridViewNotas()
        {
            try
            {
                using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var query = (from NA in db.View_CA_Notas_do_Aprendizs
                                 join C in db.CA_Conceitos on NA.NdiNota equals C.ConNota
                                 where NA.NdiAprendiz.Equals(Session["matricula"].ToString())

                                 select new { NA.Apr_Nome, NA.DisDescricao, NA.TurNome, NA.NdiNota, NA.NdiDisciplina, C.ConCodigo }



                                ).OrderBy(p => p.DisDescricao);

                    gridNotas.DataSource = query;
                    gridNotas.DataBind();
                    gridNotas.Visible = true;
                }

                var sql = "Select Sum(NdiNota) from View_CA_Notas_do_Aprendiz where NdiAprendiz = " + Session["matricula"].ToString() + "";
                var con = new Conexao();
                var result = con.Consultar(sql);
                string soma = "";
                while (result.Read())
                {

                    soma = result[0].ToString();
                    lblSomaNota.Text = "Total: " + soma;
                }

            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000156", ex);
            }
        }


        private void BindGridViewFrequencia(int codigo)
        {
            try // teste
            {



                var sql = @"SELECT CA_AulasDisciplinasAprendiz.AdiCodAprendiz, 
                            Sum(Case when ([AdidataAula]<getDate() And [Adipresenca]='F')then 1 else 0 end ) AS Faltas, 
                            Sum(Case when ([AdidataAula]<getDate() And [Adipresenca]='J')then 1 else 0 end ) AS FaltasJust, 
                            Sum(Case when ([AdidataAula]>=getDate()) then 1 else 0 end) AS ACursar, 
                            Sum(Case when ([AdiPresenca]<>'') then 1 else 0 end) AS Total,
                            Sum(Case when [AdidataAula]<GETDATE() And [Adipresenca]='.'then 1 else 0 end) As Presenca,

                            Sum(Case when [AdidataAula]<GETDATE() And [Adipresenca]='.'then 1 else 0 end) + Sum(Case when ([AdidataAula]<getDate() And [Adipresenca]='F')then 1 else 0 end ) + Sum(Case when ([AdidataAula]<getDate() And [Adipresenca]='J')then 1 else 0 end )   AS Soma


                            FROM CA_AulasDisciplinasAprendiz INNER JOIN CA_Aprendiz ON CA_AulasDisciplinasAprendiz.AdiCodAprendiz = CA_Aprendiz.Apr_Codigo
                            WHERE (((CA_AulasDisciplinasAprendiz.AdiDataAula)<[Apr_PrevFimAprendizagem]))
                            GROUP BY CA_AulasDisciplinasAprendiz.AdiCodAprendiz
                            HAVING (((CA_AulasDisciplinasAprendiz.AdiCodAprendiz) = " + codigo + "));";

                var con = new Conexao();
                SqlDataReader consulta = con.Consultar(sql);

                var list = new List<String>();
                //  var valores = new CA_AulasDisciplinasAprendiz();

                while (consulta.Read())
                {



                    lblCodAluno.Text = consulta.GetSqlValue(0).ToString();
                    lblFaltas.Text = consulta.GetSqlValue(1).ToString();
                    lblFaltasJustificadas.Text = consulta.GetSqlValue(2).ToString();
                    lblACursar.Text = consulta.GetSqlValue(3).ToString();
                    lblTotal.Text = consulta.GetSqlValue(4).ToString();
                    lblPresenca.Text = consulta.GetSqlValue(6).ToString();

                    String presenca = consulta.GetSqlValue(5).ToString();
                    String soma = consulta.GetSqlValue(6).ToString();

                    var media = Convert.ToDouble(presenca) / Convert.ToDouble(soma) * 100;

                    lblMedia.Text = String.Format("{0:0.00}", media) + "%"; // "123.46"

                    //lblFaltas.Text = consulta.GetSqlValue(1).ToString();
                    //lblFaltasJustificadas.Text = consulta.GetSqlValue(2).ToString();

                }



                //var teste = consulta.GetValue(consulta.GetOrdinal("Presenca"));


            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000156", ex);
            }
        }



        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();

        }

        private void BindListaFaltas()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from P in db.CA_AulasDisciplinasAprendizs
                             join D in db.CA_Disciplinas on P.AdiDisciplina equals D.DisCodigo
                             join E in db.CA_Educadores on P.AdiEducador equals E.EducCodigo
                             join T in db.CA_Turmas on P.AdiTurma equals T.TurCodigo
                             where P.AdiCodAprendiz.Equals(Session["matricula"].ToString()) 
                             && P.AdiPresenca.Equals("F")
                             orderby P.AdiDataAula, D.DisDescricao

                             select new { T.TurNome, P.AdiDataAula, D.DisDescricao, E.EducNome }).ToList();
                gridListaFaltas.DataSource = query;
                gridListaFaltas.DataBind();

                /*
                    SELECT CA_AulasDisciplinasAprendiz.AdiCodAprendiz, CA_Turmas.TurNome, 
                    CA_AulasDisciplinasAprendiz.AdiDataAula, CA_Disciplinas.DisDescricao, 
                    CA_Educadores.EducNome
                    FROM ((CA_AulasDisciplinasAprendiz 
                    INNER JOIN CA_Disciplinas ON CA_AulasDisciplinasAprendiz.AdiDisciplina = CA_Disciplinas.DisCodigo) 
                    INNER JOIN CA_Educadores ON CA_AulasDisciplinasAprendiz.AdiEducador = CA_Educadores.EducCodigo) 
                    INNER JOIN CA_Turmas ON CA_AulasDisciplinasAprendiz.AdiTurma = CA_Turmas.TurCodigo
                    WHERE (((CA_AulasDisciplinasAprendiz.AdiCodAprendiz)=13057) 
                    AND ((CA_AulasDisciplinasAprendiz.AdiPresenca)='F'))
                    ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula, CA_Disciplinas.DisDescricao;
                 */
            }
        }

        protected void btn_alocacao_Click(object sender, EventArgs e)
        {
            if (!Session["matricula"].Equals(""))
            {
                MultiView1.ActiveViewIndex = 3;
                BindGridView();
                BindGridViewNotas();
                //BindAnalitico();
                BindListaFaltas();
            }
            else
            {
                Alert.Show("Sua sessão expirou. Faça um novo login e tente novamente!");
                return;
            }
        }


        //protected void btn_vale_transporte_Click(object sender, EventArgs e)
        //{
        //    if (!Session["matricula"].Equals(""))
        //    {
        //        BindItinerarios();
        //        pn_info_vale.Visible = (GridView3.Rows.Count == 0);
        //        PN_Vale.Visible = false;
        //        MultiView1.ActiveViewIndex = 4;
        //    }
        //    else
        //    {
        //        Alert.Show("Sua sessão expirou. Faça um novo login e tente novamente!");
        //        return;
        //    }
        //}


        protected void btn_notas_faltas_Click(object sender, EventArgs e)
        {
            if (!Session["matricula"].Equals(""))
            {
                gridPesquisa.Visible = false;
                GridView4.DataBind();
                Bindgridview();
                MultiView1.ActiveViewIndex = 5;
            }
            else
            {
                Alert.Show("Sua sessão expirou. Faça um novo login e tente novamente!");
                return;
            }
        }

        protected void btn_pesquisa_Click(object sender, EventArgs e)
        {
            if (!Session["matricula"].Equals(""))
            {
                BindOcorrencias();
                MultiView1.ActiveViewIndex = 6;
            }
            else
            {
                Alert.Show("Sua sessão expirou. Faça um novo login e tente novamente!");
                return;
            }
        }

        //private void BindItinerarios()
        //{
        //    try
        //    {
        //        var sql = "select ItnNome, VtpLinha, VtpTarifa, VtpOrdem, VtpQuantidade from dbo.CA_Vale_Transporte inner join " +
        //            "CA_Itinerarios on VtpItinerario =  ItnCodigo where VtpAprendiz = " + Session["matricula"];

        //        var datasource = new SqlDataSource { SelectCommand = sql, ID = "SDS_vale", ConnectionString = GetConfig.Config() };
        //        datasource.Selected += SqlDataSource1_Selected;
        //        GridView3.DataSource = datasource;
        //        GridView3.DataBind();

        //    }
        //    catch (Exception ex)
        //    {
        //        Funcoes.TrataExcessao("000020", ex);
        //    }
        //}


        private void Bindgridview()
        {
            string sqlgeral = "SELECT  * FROM View_Pesquisas_Realizadas " +
                    "WHERE Apr_Codigo = '" + Session["matricula"] + "'  AND PepRealizada = 'S'  order by ParNomeFantasia, Apr_Nome";

            var datasource = new SqlDataSource { ID = "SDS_Pesquisas", SelectCommand = sqlgeral, ConnectionString = GetConfig.Config() };
            GridView5.DataSource = datasource;
            GridView5.DataBind();
            pn_info_pesquisa.Visible = GridView5.Rows.Count == 0;
        }


        private void BindAnalitico()
        {
            //try
            //{
            //    using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            //    {
            //        var dados = bd.View_controle_frequencias.Where(p => p.Apr_Codigo == int.Parse(Session["matricula"].ToString())).OrderByDescending(p => p.FreqDataPrevEntrega);
            //        GridView2.DataSource = dados.ToList();
            //        HFRowCount.Value = dados.Count().ToString();
            //        GridView2.DataBind();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Funcoes.TrataExcessao("000021", ex);
            //}
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            var url = Regex.Split(Request.Url.ToString(), ".aspx");
            var name = Regex.Split(url.First(), "pages/").Last();
            var tipo = "popup('PopupCadastroEscolas.aspx?" +
                      "id=" + Criptografia.Encrypt("1", GetConfig.Key()) +
                      "&target=" + Criptografia.Encrypt(name + ".aspx", GetConfig.Key()) + "', '840',380);";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), tipo, true);
        }




        //protected void btn_novo_itinerario_Click(object sender, EventArgs e)
        //{
        //    pn_info_vale.Visible = false;
        //    Session["AlteraVale"] = "Incluir";

        //    dd_itinerarios.DataBind();
        //    dd_itinerarios.Enabled = true;
        //    tb_linha_trp.Text = string.Empty;
        //    tb_tarifa_trp.Text = string.Empty;
        //    tb_quantidade.Text = "0";

        //    GridView3.DataBind();
        //    PN_Vale.Visible = true;
        //}


        protected void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                //if (dd_itinerarios.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Escolha um itinerário.");
                //if (tb_linha_trp.Text.Equals(string.Empty)) throw new ArgumentException("Informe a linha (ônibus, metrô, etc) usada neste itinerário.");
                //if (tb_tarifa_trp.Text.Equals(string.Empty)) throw new ArgumentException("Informe a tarifa da linha usada neste itinerário.");

                //const string sqlinsert = "INSERT INTO CA_Vale_Transporte VALUES(@VtpAprendiz,@VtpItinerario, @VtpLinha, @VtpTarifa,@VtpQuantidade )";
                //const string sqlupdate = "UPDATE CA_Vale_Transporte SET VtpLinha = @VtpLinha, VtpTarifa = @VtpTarifa, VtpQuantidade = @VtpQuantidade WHERE VtpOrdem = @codigo";

                //var parameters = new List<SqlParameter> { new SqlParameter("VtpAprendiz", Session["matricula"].ToString()),
                //                             new SqlParameter("VtpItinerario", dd_itinerarios.SelectedValue), new SqlParameter("VtpLinha", tb_linha_trp.Text),
                //                             new SqlParameter("VtpTarifa", float.Parse(tb_tarifa_trp.Text)), new SqlParameter("VtpQuantidade", tb_quantidade.Text) };

                //if (Session["AlteraVale"].Equals("Alterar"))
                //    parameters.Add(new SqlParameter("codigo", int.Parse(Session["AlteraVale_id"].ToString())));

                //var con = new Conexao();
                //con.Alterar(Session["AlteraVale"].Equals("Alterar") ? sqlupdate : sqlinsert, parameters.ToArray());

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                //                      "alert('Ação realizada com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                Alert.Show(ex.Message);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000030", ex);
            }
        }

        //protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var grid = (GridView)sender;
        //    var row = grid.SelectedRow;
        //    var ordem = ((ImageButton)row.Cells[4].FindControl("IMBexcluir")).CommandArgument;
        //    Session["AlteraVale"] = "Alterar";
        //    Session["AlteraVale_id"] = ordem;

        //    using (var repository = new Repository<ValeTransporte>(new Context<ValeTransporte>()))
        //    {
        //        var vale = repository.All().Where(p => p.VtpOrdem == int.Parse(ordem)).First();
        //        dd_itinerarios.SelectedValue = vale.VtpItinerario.ToString();
        //        dd_itinerarios.Enabled = false;
        //        tb_linha_trp.Text = vale.VtpLinha;
        //        tb_tarifa_trp.Text = string.Format("{0:f2}", vale.VtpTarifa);
        //        tb_quantidade.Text = vale.VtpQuantidade.ToString();
        //    }
        //    GridView3.DataBind();
        //    pn_info_vale.Visible = false;
        //    PN_Vale.Visible = true;
        //}


        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            //BindItinerarios();
            //PN_Vale.Visible = false;
        }

        //protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (Session["enable_Save"].Equals("Aluno") || Session["enable_Save"].Equals("Parceiro"))
        //    {
        //        Alert.Show("Não Habilitado para seu perfil.");
        //        return;
        //    }

        //    using (var repository = new Repository<ValeTransporte>(new Context<ValeTransporte>()))
        //    {
        //        var button = (ImageButton)sender;
        //        var ordem = button.CommandArgument;
        //        if (Convert.ToBoolean(HFConfirma.Value))
        //        {
        //            var item = repository.All().Where(p => p.VtpOrdem == int.Parse(ordem)).First();
        //            repository.Remove(item);
        //        }
        //        BindItinerarios();
        //        PN_Vale.Visible = false;
        //    }
        //}


        private void BindOcorrencias()
        {
            try
            {
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var list = new List<LancamentoOcorrencia.OcrAprendiz>();
                    var dados = from i in bd.CA_OcorrenciasAprendizs
                                join p in bd.CA_Ocorrencias on i.OcaCodOcorrencia equals p.OcoCodigo
                                join m in bd.CA_Aprendizs on i.OcaCodAprendiz equals m.Apr_Codigo
                                join n in bd.CA_Usuarios on i.OcaUsuarioocorrencia equals n.UsuCodigo
                                select new LancamentoOcorrencia.OcrAprendiz
                                {
                                    Data = i.OcaDataOcorrencia,
                                    Ordem = i.OcaOrdem,
                                    Matricula = i.OcaCodAprendiz,
                                    Emissor = i.OcaEmissorOcorrencia.Equals("E") ? "Empresa" : "CejaBrasil",
                                    Nome = m.Apr_Nome,
                                    Descricao = p.OcoDescricao,
                                    Responsavel = n.UsuNome,
                                    Observacao = i.OcaObservacoes
                                };

                    list.AddRange(dados.Where(p => p.Matricula == int.Parse(Session["matricula"].ToString())).OrderBy(x => x.Data));
                    GridView6.DataSource = list.ToList();
                    GridView6.DataBind();
                    pn_info_ocorrencia.Visible = list.Count == 0;
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000126", ex);
            }
        }

        protected void DDcoleOrigem_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            ShowAdress(int.Parse(drop.SelectedValue));
        }


        private void ShowAdress(int? unidade)
        {
            if (unidade == 0 || unidade == null) return;

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var escola = bd.CA_Escolas.Where(p => p.EscCodigo == unidade);
                if (escola.Count() == 0)
                {
                    lb_endereco.Text = "Não Encontrado.";
                    return;
                }

                var dados = escola.First();

                var sb = new StringBuilder();
                sb.Append(dados.EscEndereco);
                if (dados.EscNumeroEndereco != null)
                {
                    sb.Append(",nº " + dados.EscNumeroEndereco);
                }
                sb.Append(dados.EscComplemento);
                sb.Append(", Bairro: " + (dados.EscBairro ?? "Não Informado"));
                sb.Append(", " + dados.EscCidade + " / " + dados.EscEstado);
                sb.Append(". CEP: " + (dados.EscCEP == null ? "Não Informado" : Funcoes.FormataCep(dados.EscCEP)));
                sb.Append(". Telefone: " + ((dados.EscTelefone == null) || (dados.EscTelefone.Length < 8) ? "Não Informado" : Funcoes.FormataTelefone(dados.EscTelefone)));
                sb.Append(". Diretor: " + ((dados.EscDiretor == null) || (dados.EscDiretor.Length < 8) ? "Não Informado" : dados.EscDiretor));
                lb_endereco.Text = sb.ToString();
            }
        }

        private void CarregaFoto()
        {
            if (Session["matricula"] == null)
            {
                IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                return;
            }

            var matricula = Session["matricula"].ToString();
            var filePath = Server.MapPath(@"/files/fotos");
            var dir = new DirectoryInfo(filePath);
            if (dir.Exists)
            {
                var files = dir.GetFiles().ToList();
                //var foto = files.Where(p => p .Name.Contains(matricula)).ToList();
                //var foto = files.Where(p => p.Name.Substring(0, 4).Equals(matricula)).ToList();
                var foto = files.Where(p => p.Name.Equals(matricula + ".jpg")).ToList();
                if (foto.Count() > 0)
                {
                    var path = "../files/fotos/" + foto.First().Name;
                    IMG_foto_aprendiz.Attributes.Add("src", path);
                }
                else
                {
                    IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                }
            }
        }

        private string CalculaIdade(DateTime idade)
        {
            var data = DateTime.Today.Subtract(idade);
            return (data.Days / 365).ToString();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fupArquivo.HasFile) throw new ArgumentException("Selecione um arquivo para enviar.");
                if (fupArquivo.FileContent.Length > 10000000) throw new ArgumentException("Arquivo maior que o limite recomendado.");


                var path = Server.MapPath(@"/files/fotos");

                var matricula = Session["matricula"].ToString();
                var dir = new DirectoryInfo(path);
                var files = dir.GetFiles().ToList();
                var foto = files.Where(p => p.Name.Contains(matricula)).ToList();

                //exclui a foto que existe
                if (foto.Count > 0) foto.First().Delete();


                //salva o arquivo.
                var filePath = path + "/" + Session["matricula"] + ".jpg"; //System.Web.HttpUtility.HtmlDecode(fupArquivo.FileName);
                fupArquivo.SaveAs(filePath);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('" + ex.Message + ".')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000200", ex);
            }
            finally
            {
                tb_Caminho_arquivo.Text = string.Empty;
                CarregaFoto();
            }
        }

        protected void IMB_Notas_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            var row = (GridViewRow)bt.Parent.Parent;
            var target = bt.CommandArgument + "\n" + WebUtility.HtmlDecode(row.Cells[0].Text) + "\n" + WebUtility.HtmlDecode(row.Cells[3].Text);
            var tipo = "popup02('popup_faltas.aspx?target=" + Criptografia.Encrypt(target, GetConfig.Key()) + "', '590','450');";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), tipo, true);

        }

        protected void btnCronogramaAluno_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 7;
            BindGridViewCronogramaAluno();
        }

        //Parte responsável pelo preenchimento do grid de Cronograma Aluno
        //11/03/2015
        // Autor: Thassio Santos
        private void BindGridViewCronogramaAluno()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from AA in db.CA_AulasDisciplinasAprendizs

                             join d in db.CA_Disciplinas on AA.AdiDisciplina equals d.DisCodigo
                             join edu in db.CA_Educadores on AA.AdiEducador equals edu.EducCodigo
                             join tur in db.CA_Turmas on AA.AdiTurma equals tur.TurCodigo
                             join apr in db.CA_Aprendizs on AA.AdiCodAprendiz equals apr.Apr_Codigo
                             where AA.AdiCodAprendiz.Equals(Convert.ToInt32(TBcodigoAlu.Text))
                             orderby AA.AdiDataAula

                             select new { AA.AdiCodAprendiz, AA.AdiDataAula, AA.AdiPresenca, AA.AdiObservacoes, apr.Apr_Nome, tur.TurNome, d.DisDescricao, edu.EducNome });

                gridCronogramaAluno.DataSource = query;
                gridCronogramaAluno.DataBind();
            }
        }

        //Método responsável por colorir algumas linhas do grid.
        // Data: 11/03/2015
        // Autor: Thassio Santos
        protected void gridCronogramaAluno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //  colorindo uma linha com base no conteúdo de uma célula

            DateTime dt;
            var colCount = e.Row.Cells.Count;

            // Colorindo a coluna Observações.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!DateTime.TryParse(e.Row.Cells[5].Text, out dt))
                    return;//nao deu certo a conversao
                else if (dt < DateTime.Now.Date && e.Row.Cells[6].Text.Equals("."))
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                }
                else if (dt < DateTime.Now.Date && e.Row.Cells[6].Text != ".")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Blue;
                }
            }
        }

        // Evento de paginação
        protected void gridCronogramaAluno_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCronogramaAluno.PageIndex = e.NewPageIndex;
            BindGridViewCronogramaAluno();
        }






        private void BindGridPesquisas(int pepCod)
        {




            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from PR in db.View_Pesquisas_Realizadas

                             join rp in db.View_Resposta_Pesquisas on PR.PepCodigo equals rp.PepCodigo
                             join p in db.CA_Pesquisas on rp.PepPesquisaCodigo equals p.PesCodigo
                             join aa in db.CA_AlocacaoAprendizs on PR.Apr_Codigo equals aa.ALAAprendiz
                             join t in db.CA_Turmas on aa.ALATurma equals t.TurCodigo
                             where PR.PepCodigo.Equals(pepCod) && t.TurCurso.Equals("002")

                             select new { rp.QueTexto, rp.OpcTexto, rp.OpcNota }

                            );

                gridPesquisa.DataSource = query;
                gridPesquisa.DataBind();
                gridPesquisa.Visible = true;
                Session["pepCodigo"] = pepCod;
            }

        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Demonstrativo"))
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView5.Rows[index];
                var pepCod = row.Cells[0].Text;
                gridPesquisa.PageIndex = 0;
                BindGridPesquisas(Convert.ToInt32(pepCod));

            }

        }

        protected void gridPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPesquisa.PageIndex = e.NewPageIndex;
            BindGridPesquisas(Convert.ToInt32(Session["pepCodigo"].ToString()));
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

        //public void PreencheMunicipioEndereco()
        //{
        //    var valor = DDufEndAlu.SelectedValue;
        //    using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
        //    {
        //        var query = (from M in bd.MA_Municipios
        //                     where M.MunIEstado == valor
        //                     select new { M.MunIDescricao }).ToList();

        //        DDMunicipioEndereco.DataSource = query;
        //        DDMunicipioEndereco.DataBind();
        //    }
        //}



        protected void DDufEndAlu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PreencheMunicipioEndereco();

        }

        public string RetornaNomeUnidade(int codUnidade)
        {
            var sql = "Select UniNome from Ca_Unidades where UniCodigo = " + codUnidade + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                return result["UniNome"].ToString();
            }

            return "";
        }





        protected void btnDocumentacao_Click(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            MultiView1.ActiveViewIndex = 8;
            GetFiles(int.Parse(Session["matricula"].ToString()));

        }

        private void GetFiles(int aluno)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                //var dados = from i in bd.CA_DocumentosAprendizs
                //            //  join p in bd.MA_StatusRequisicaos on i.DAluStatus equals p.SitCodigo
                //            join n in bd.CA_Documentos on i.DAprDocumento equals n.DocCodigo
                //            //join m in bd.MA_Escolas on i.DAluEscola equals m.EscCodigo
                //            where i.DAprSequencia.Equals(sequencia)
                //            select new { i.DAprSequencia, i.DAprDataSolic, i.AluNomeAnexo };
                //var req = dados.First();

                //var protocolo = req.DAluSequencia.ToString().PadLeft(6, '0') + "-" + req.DAluDataSolic.Year;

                //if (dados.Count() == 0) return;

                var filePath = Server.MapPath(@"/files/DocSecretaria/" + aluno + "/");
                ViewState.Add("Caminho", filePath);
                var dir = new DirectoryInfo(filePath);
                if (dir.Exists)
                {
                    var files = dir.GetFiles().ToList();
                    //var lista = files.Where(i => i.Name.Equals("_" + req.AluNomeAnexo)).ToList();
                    //  var lista = files.Where(i => i.Name.Equals(sequencia.ToString() + "_" + req.AluNomeAnexo)).ToList();
                    //var lista = req.DocDirEspecial.Equals("S") ? files.Where(i => i.Name.Equals(protocolo.ToString())).ToList() : files.Where(i => i.Name.Split('_')[0].Equals(protocolo)).ToList();

                    var datasrc = files.Select(fileInfo => new Arquivos { Nome_Arquivo = fileInfo.Name }).ToList();
                    GridView3.DataSource = datasrc;
                    GridView3.DataBind();
                }
            }
        }

     

        protected void btnImportarArquivo_Click1(object sender, EventArgs e)
        {
            if (fileUploadArquivo.FileName.Equals(string.Empty))
            {
                Alert.Show("Nenhum arquivo anexado.");
            }
            else
            {
                SalvaArquivo(fileUploadArquivo);
            }
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = GridView3.SelectedRow;
            var name = HttpUtility.HtmlDecode(row.Cells[0].Text);

            var fInfo = new FileInfo(ViewState["Caminho"] + name);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fInfo.Name + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", fInfo.Length.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.WriteFile(fInfo.FullName);
        }

        protected void TBCepEndAlu_TextChanged(object sender, EventArgs e)
        {
            var sql = "Select * from Ca_CodigosCep where Cep_Codigo = " + Funcoes.Retirasimbolo(TBCepEndAlu.Text) + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                TBruaEndAlu.Text = result["Cep_Logradouro"].ToString();
                txtBairro.Text = result["Cep_Bairro"].ToString();
                DDufEndAlu.SelectedValue = result["Cep_UF"].ToString();
                txtMunicipioEndereco.Text = result["Cep_Cidade"].ToString();
                // DDMunicipioEndereco.SelectedValue = "SAO PAULO";
            }

        }



        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];
                var filePath = Server.MapPath(@"/files") + @"/DocSecretaria/" + Session["matricula"] + @"/" + GridView3.Rows[index].Cells[0].Text;
                try
                {
                    File.Delete(filePath);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Arquivo excluído com sucesso.')", true);
                }
                catch (Exception)
                {
                    Response.Redirect("ErrorPage.aspx");
                }
                //var direct = @"" + ViewState["pasta"] + "/" + DDturmas.SelectedValue + "/" + DDDisNome.SelectedValue;

                var direct =  Server.MapPath(@"/files") + @"/DocSecretaria/" + Session["matricula"] + @"/";
                var dir = new DirectoryInfo(direct);
             



                if (dir.Exists)
                {
                    var files = dir.GetFiles().ToList();
                    //var lista = files.Where(i => i.Name.Equals("_" + req.AluNomeAnexo)).ToList();
                    //  var lista = files.Where(i => i.Name.Equals(sequencia.ToString() + "_" + req.AluNomeAnexo)).ToList();
                    //var lista = req.DocDirEspecial.Equals("S") ? files.Where(i => i.Name.Equals(protocolo.ToString())).ToList() : files.Where(i => i.Name.Split('_')[0].Equals(protocolo)).ToList();

                    var datasrc = files.Select(fileInfo => new Arquivos { Nome_Arquivo = fileInfo.Name }).ToList();
                    GridView3.DataSource = datasrc;
                    GridView3.DataBind();
                }

            }
        }


        protected void SalvaArquivo(FileUpload arquivo)
        {


            if (!arquivo.HasFile) return;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                //var escola = DD_nome_campus.SelectedValue;
                //var dados = from i in bd.MA_DocumentosAlunos
                //            join m in bd.MA_Documentos on i.DAluDocumento equals m.DocCodigo
                //            where i.DAluMatricula.Equals(Session["matricula"]) &&
                //                  i.DAluDocumento.Equals(Session["Doc"]) && i.DAluDataSolic.Equals(DateTime.Today)
                //                  && i.DAluStatus.Equals("P") && i.DAluEscola.Equals(escola)
                //            select new { i, m.DocDirEspecial };

                //if (dados.Count() > 0)
                //{
                //   var requerimento = dados.First();
                //salva o arquivo.
                var filePath = Server.MapPath(@"/files") + @"/DocSecretaria/" + Session["matricula"] + @"/";


                var dir = new DirectoryInfo(filePath);
                if (!dir.Exists)
                    dir.Create();
                //var protocolo = requerimento.i.DAluSequencia.ToString().PadLeft(6, '0') + "-" + requerimento.i.DAluDataSolic.Year;
                var file = new FileInfo(filePath + arquivo.FileName);
                // var file = new FileInfo(filePath + protocolo + "_" + Session["matricula"].ToString() + ".jpg");
                try
                {
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    file.Refresh();
                }
                catch (IOException ex)
                {
                    var user = (string)Session["matricula"];
                    //Funcoes.TrataExcessao("000000", ex, (user ?? "Indefinido"));
                }
                finally
                {
                    arquivo.SaveAs(filePath + arquivo.FileName);
                    Session["file_attached"] = arquivo.FileName;
                    //  arquivo.SaveAs(filePath + protocolo + "_" + Session["matricula"].ToString() + ".jpg");
                    // Session["file_attached"] = Session["matricula"].ToString() + ".jpg";
                    Session["AnexarDoc"] = "Sim";
                    GetFiles(int.Parse(Session["matricula"].ToString()));
                }

            }
        }


    }
}

