using MenorAprendizWeb.Base;
using Microsoft.Reporting.WebForms;
using ProtocoloAgil.Base;
using ProtocoloAgil.net.cloudapp.protocoloagil;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using ReportDataSource = Microsoft.Reporting.WebForms.ReportDataSource;

namespace ProtocoloAgil.pages
{
    public partial class Visualizador : Page
    {

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //SuppressExportButton(ReportViewer1, "PDF");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var reportcommand = "";
            var command = "";
            var id = Session["id"].ToString();
            var selectcommand = "";
            var reportDatas = new ReportDataSource();
            var validacao = "";

            ReportViewer1.LocalReport.EnableExternalImages = true;
            string caminhoLogo = "F:\\sites\\agilsist.com.br\\cejabrasil.agilsist.com.br\\images\\logofundacao.png";

            switch (id)
            {
                case "100":


                    string orderBy = "order by ALApagto, Apr_Nome";
                    var parceiro = Session["Parceiro"];
                    var turma = Session["PRMT_Turma"];
                    var aux = "";
                    var mesReverencia = Session["PRMT_MesRef"];
                    var anoReferencia = Session["PRMT_AnoRef"];
                    var mesReferenciaNumeral = Session["mesReferenciaNumeral"];


                    Session["args"] = Session["PRMT_Tipo"] + "_" + Session["PRMT_Nome"] + "_" + Session["PRMT_MesRef"] + "_" + Session["PRMT_AnoRef"];

                    var tipo01 = Session["PRMT_Tipo"].ToString();
                    validacao = "";
                    orderBy = " order by ALApagto, Apr_Nome";

                    if (!Session["PRMT_Parceiro"].Equals(string.Empty))
                    {
                        validacao += " and ParCodigo = '" + Session["PRMT_Parceiro"] + "'";
                    }

                    if (!Session["PRMT_Turma"].Equals("Selecione.."))
                    {
                        validacao += " and TurCodigo = '" + Session["PRMT_Turma"] + "'";
                    }

                    //if (!Session["PRMT_TipoContrato"].Equals(string.Empty))
                    //{
                    //    validacao += " and ALApagto = '" + Session["PRMT_TipoContrato"] + "'";
                    //}

                    if (!Session["TipoContrato"].Equals(string.Empty))
                    {
                        //Expr1 = ALAPagto > na view esse nome foi trocado
                        validacao += " and Expr1 = '" + Session["TipoContrato"] + "'";
                    }

                    string titulo01 = "Geral";
                    switch (tipo01)
                    {
                        case "tab-0":
                            selectcommand = @"select *, (case when AlaMesPagto01Uniforme = " + Convert.ToInt32(mesReferenciaNumeral) + " and AlaAnoPagto01Uniforme = " + Session["PRMT_AnoRef"] + " then UniValorUniforme  else 0 end) As TotalUniforme from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and turCurso = '002' and Apr_Nome  like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND ALADataInicio <= GETDATE() And ALADataPrevTermino >= getdate() ";

                            selectcommand += validacao;
                            selectcommand += orderBy;
                            titulo01 = "Geral";
                            break;
                        case "tab-1":
                            selectcommand = "select *, (case when AlaMesPagto01Uniforme = " + Convert.ToInt32(mesReferenciaNumeral) + " and AlaAnoPagto01Uniforme = " + Session["PRMT_AnoRef"] + " then UniValorUniforme  else 0 end) As TotalUniforme from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and turCurso = '002' and Apr_Nome " +
                                  "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND DATEPART(m, Apr_InicioAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                            " DATEPART(yyyy, Apr_InicioAprendizagem) = " + Session["PRMT_AnoRef"];

                            selectcommand += validacao;
                            selectcommand += orderBy;
                            titulo01 = "Por data de Início";
                            break;
                        case "tab-2":
                            selectcommand = "select *, (case when AlaMesPagto01Uniforme = " + Convert.ToInt32(mesReferenciaNumeral) + " and AlaAnoPagto01Uniforme = " + Session["PRMT_AnoRef"] + " then UniValorUniforme  else 0 end) As TotalUniforme from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and Apr_Situacao = 6 and Apr_Nome " +
                                "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%'  AND  DATEPART(m, Apr_FimAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                          " DATEPART(yyyy, Apr_FimAprendizagem) = " + Session["PRMT_AnoRef"];

                            selectcommand += validacao;
                            selectcommand += orderBy;
                            titulo01 = "Por data de Término";
                            break;
                        case "tab-3":
                            selectcommand = "select *, (case when AlaMesPagto01Uniforme = " + Convert.ToInt32(mesReferenciaNumeral) + " and AlaAnoPagto01Uniforme = " + Session["PRMT_AnoRef"] + " then UniValorUniforme  else 0 end) As TotalUniforme from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and Apr_Situacao = 6 and Apr_Nome " +
                                "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND DATEPART(m, Apr_PrevFimAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                          " DATEPART(yyyy, Apr_PrevFimAprendizagem) = " + Session["PRMT_AnoRef"];

                            selectcommand += validacao;
                            selectcommand += orderBy;
                            titulo01 = "Por Previsão de Término";
                            break;
                    }

                    var conexaoFinanceiro = new Conexao();
                    var resultadoFinanceiro = conexaoFinanceiro.Consultar(selectcommand);
                    string tipoPagamento = "";

                    while (resultadoFinanceiro.Read())
                    {
                        tipoPagamento = resultadoFinanceiro["ALApagto"].ToString();
                    }

                    Session["PRMT_Nome"] = "";
                    Session["PRMT_MesRef"] = "";
                    Session["PRMT_AnoRef"] = "";
                    Session["PRMT_Turma"] = "";
                    Session["PRMT_Parceiro"] = "";
                    Session["PRMT_TipoContrato"] = "";

                    if (tipoPagamento.ToUpper().Equals("CejaBrasil"))
                    {
                        ReportViewer1.LocalReport.ReportPath = "reports/RPListaAtivosFinanceiro.rdlc";
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = "reports/RPListaAtivosFinanceiroEmpresa.rdlc";
                    }

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("mesReverenciaNumeral", mesReferenciaNumeral.ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("mesReverencia", mesReverencia.ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("anoReferencia", anoReferencia.ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("TipoRelatorio", titulo01));


                    break;

                case "1":
                    selectcommand = "SELECT * FROM CA_Escolas order by EscNome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaEscolas.rdlc";
                    break;

                case "2":
                    selectcommand = "SELECT *, C.CurDescricao as CursoDescricao, U.UniNome as NomeUnidade FROM CA_Turmas T join CA_Cursos C on T.TurCurso = C.CurCodigo join CA_Unidades U on U.UniCodigo = T.TurUnidade order by TurNome ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaTurmas.rdlc";
                    break;

                case "3":
                    selectcommand = "SELECT * FROM CA_Profissoes order by ProfDescricao";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaProfissao.rdlc";
                    break;

                case "4":
                    selectcommand = "SELECT * FROM CA_Ocorrencias order by  OcoDescricao";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaOcorrencia.rdlc";
                    break;

                case "5":
                    selectcommand = "SELECT * FROM CA_Disciplinas order by DisDescricao";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaDisciplinas.rdlc";
                    break;

                //case "6":
                //    selectcommand = "SELECT * FROM CA_Vinculo_Empregaticio order by Vin_Descricao";
                //    ReportViewer1.LocalReport.ReportPath = "reports/RPListaVinculo.rdlc";
                //    break;

                case "7":
                    selectcommand = "SELECT * FROM CA_RamosAtividades order by RatDescricao";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaRamoAtividade.rdlc";

                    break;

                case "8":
                    selectcommand = "SELECT * FROM CA_Parceiros order by ParNomeFantasia";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaParceiros.rdlc";
                    break;

                case "9":

                    Session["args"] = Session["parameter_curso"] + "_" + Session["parameter_plano"];

                    if (Session["parameter_curso"] == null)
                        selectcommand = "select * from View_CA_PlanoCurricular order by CurDescricao, DisDescricao";
                    else
                        selectcommand = "select * from View_CA_PlanoCurricular where CurCodigo = '" + Session["parameter_curso"] + "'   AND  PlanCodigo =  '" + Session["parameter_plano"] + "' order by CurDescricao, DisDescricao";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPPlanoCuricular.rdlc";
                    break;

                case "11":
                    selectcommand = "select * from  View_CA_DisciplinasProfessores";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaDisciplinasProf.rdlc";
                    break;

                case "12":
                    Session["args"] = Session["PRMT_Ordem"];

                    selectcommand = "select * from  View_CA_DisciplinasProfessores where DPOrdem = " + Session["PRMT_Ordem"] + " ";
                    reportcommand = "select * from dbo.CA_AulasDisciplinasTurmaProf WHERE ADPDisciplinaProf = " + Session["PRMT_Ordem"] + "";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPConteudoAulas.rdlc";
                    break;

                case "13":
                    Session["args"] = Session["PRMT_Ordem"];

                    selectcommand = "select * from dbo.View_Resultado_Final where DpOrdem =  " + Session["PRMT_Ordem"] + " order by Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPResultadoFinal.rdlc";
                    break;

                case "14":
                    Session["args"] = Session["PRMT_Ordem"];

                    selectcommand = "select * from View_parecer_Tecnico where DpOrdem = " + Session["PRMT_Ordem"] + " ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPParecerTecnico.rdlc";
                    break;

                case "16":
                    Session["args"] = Session["codigo"] + "_" + Session["school_parameter"] + "_" + Session["curso_parameter"] + "_" +
                         Session["documento_parameter"] + "_" + Session["status_parameter"] + "_" + Session["filter_parameter"];

                    selectcommand =
                        " select MA_DocumentosAlunos.DaluSequencia, MA_Escolas.EscNome,MA_Alunos.AluDataNascimento, MA_Escolas.EscCodigo, MA_AreaEnsino.EnsDescricao, MA_Documentos.DocDescricao,MA_Alunos.AluCPF,   MA_Alunos.AluNome, MA_Alunos.AluTelefone,  MA_Alunos.AluCelular, MA_Alunos.AluEmail, MA_Usuarios.UsuNome,  MA_StatusRequisicao.SitDescricao, " +
                        "MA_DocumentosAlunos.DAluParacer, (case when  MA_DocumentosAlunos.DAluStatusParecer ='S' then 'Sim' else 'Não' end) as Entrega, (case when  MA_DocumentosAlunos.DAluCobrarValor ='S' then 'Sim' else 'Não' end) as Cobrar, MA_DocumentosAlunos.DaluDataSolic,  MA_DocumentosAlunos.DaluDataEntrega " +
                        "FROM ((MA_DocumentosAlunos INNER JOIN MA_AreaEnsino ON MA_DocumentosAlunos.DAluCurso = MA_AreaEnsino.EnsCodigo) INNER JOIN MA_Escolas ON MA_DocumentosAlunos.DAluEscola = MA_Escolas.EscCodigo) INNER JOIN MA_Alunos ON MA_DocumentosAlunos.DAluMatricula = MA_Alunos.AluMatricula  " +
                        "INNER JOIN MA_Documentos ON MA_DocumentosAlunos.DAluDocumento = MA_Documentos.DocCodigo   INNER JOIN MA_StatusRequisicao ON MA_DocumentosAlunos.DAluStatus = MA_StatusRequisicao.SitCodigo  LEFT JOIN MA_Usuarios ON MA_DocumentosAlunos.DAluUsuParecer = MA_Usuarios.UsuCodigo " +
                        "inner join MA_UsuarioUnidade on UniCodigoUnidade =  MA_Escolas.EscCodigo where MA_UsuarioUnidade.UnicodigoUsuario = '" + Session["codigo"] + "' AND   MA_Documentos.DocDescricao IS NOT NULL ";

                    if (Session["school_parameter"] != null && !Session["school_parameter"].Equals(string.Empty))
                    {
                        var codigos = Session["school_parameter"].ToString().Split(',');
                        selectcommand += " AND (";
                        for (int i = 0; i < codigos.Length; i++)
                        {
                            selectcommand += (i == 0
                                ? " MA_DocumentosAlunos.DaluEscola = " + codigos[i] + " "
                                : " OR MA_DocumentosAlunos.DaluEscola = " + codigos[i] + " ");
                        }
                        selectcommand += ") ";
                    }

                    if (Session["curso_parameter"] != null && !Session["curso_parameter"].Equals(string.Empty))
                        selectcommand += " AND MA_DocumentosAlunos.DAluCurso = '" + Session["curso_parameter"] + "' ";

                    if (Session["documento_parameter"] != null && !Session["documento_parameter"].Equals(string.Empty))
                        selectcommand += " AND MA_DocumentosAlunos.DAluDocumento = '" + Session["documento_parameter"] + "' ";

                    if (Session["status_parameter"] != null && !Session["status_parameter"].Equals(string.Empty))
                        selectcommand += " AND MA_DocumentosAlunos.DAluStatus = '" + Session["status_parameter"] + "' ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RP_Relatorio_analitico.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("filtro", WebUtility.HtmlDecode(Session["filter_parameter"].ToString())));
                    break;

                case "17":
                    Session["args"] = Session["school_parameter"] + "_" + Session["user_parameter"] + "_" + Session["curso_parameter"] + "_" +
                         Session["documento_parameter"] + "_" + Session["status_parameter"] + "_" + Session["filter_parameter"];

                    selectcommand = " select * from View_MA_RelatorioAnalitico where DocDescricao IS NOT NULL ";
                    if (Session["school_parameter"] != null && !Session["school_parameter"].Equals(string.Empty))
                    {
                        var codigos = Session["school_parameter"].ToString().Split(',');
                        selectcommand += " AND (";
                        for (int i = 0; i < codigos.Length; i++)
                            selectcommand += (i == 0 ? " Esccodigo = " + codigos[i] + " " : " OR Esccodigo = " + codigos[i] + " ");
                        selectcommand += ") ";
                    }

                    if (Session["user_parameter"] != null && !Session["user_parameter"].Equals(string.Empty))
                    {
                        var codigos = Session["user_parameter"].ToString().Split(',');
                        selectcommand += " AND (";
                        for (int i = 0; i < codigos.Length; i++)
                            selectcommand += (i == 0 ? " Usucodigo = '" + codigos[i] + "' " : " OR Usucodigo = '" + codigos[i] + "' ");
                        selectcommand += ") ";
                    }

                    if (Session["curso_parameter"] != null && !Session["curso_parameter"].Equals(string.Empty))
                    {
                        var codigos = Session["curso_parameter"].ToString().Split(',');
                        selectcommand += " AND (";
                        for (int i = 0; i < codigos.Length; i++)
                            selectcommand += (i == 0 ? " DAluCurso = '" + codigos[i] + "' " : " OR DAluCurso = '" + codigos[i] + "' ");
                        selectcommand += ") ";
                    }

                    if (Session["documento_parameter"] != null && !Session["documento_parameter"].Equals(string.Empty))
                    {
                        var codigos = Session["documento_parameter"].ToString().Split(',');
                        selectcommand += " AND (";
                        for (int i = 0; i < codigos.Length; i++)
                            selectcommand += (i == 0 ? " DAluDocumento = '" + codigos[i] + "' " : " OR DAluDocumento = '" + codigos[i] + "' ");
                        selectcommand += ") ";
                    }

                    if (Session["status_parameter"] != null && !Session["status_parameter"].Equals(string.Empty))
                    {
                        var codigos = Session["status_parameter"].ToString().Split(',');
                        selectcommand += " AND (";
                        for (int i = 0; i < codigos.Length; i++)
                            selectcommand += (i == 0 ? " DAluStatus = '" + codigos[i] + "' " : " OR DAluStatus = '" + codigos[i] + "' ");
                        selectcommand += ") ";
                    }

                    ReportViewer1.LocalReport.ReportPath = "reports/RP_Relatorio_analitico02.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("filtro", WebUtility.HtmlDecode(Session["filter_parameter"].ToString())));
                    break;
                case "18":
                    Session["args"] = Session["PRMT_Aprendiz"];

                    selectcommand = "select * from View_Ficha_Aprendiz where Apr_codigo = " + Session["PRMT_Aprendiz"].ToString() + "";
                    reportcommand = "select * from  dbo.View_CA_Familiares where Fam_AperendizId = " + Session["PRMT_Aprendiz"].ToString() + "";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPFichaAluno.rdlc";
                    break;

                case "19":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"] + "_" +
                     Session["Turma_parameter"] + "_" + Session["Disciplina_parameter"];


                    if (Session["Disciplina_parameter"].Equals(string.Empty))
                    {
                        selectcommand = "select * from  View_CA_CronogramaAulas WHERE DPTurma = " +
                                        Session["Turma_parameter"] + " AND ADPDataAula  BETWEEN '" + Session["Data_inicio_parameter"] + "' AND  '" + Session["Data_final_parameter"] + "'";
                    }
                    else
                    {
                        selectcommand = "select * from  View_CA_CronogramaAulas WHERE DPTurma = " + Session["Turma_parameter"] + "  AND DPDisciplina = " +
                                        Session["Disciplina_parameter"] + " AND ADPDataAula  BETWEEN '" + Session["Data_inicio_parameter"] + "' AND  '" + Session["Data_final_parameter"] + "' ";
                    }
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronograma.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("PeriodoRef", "De " + Session["Data_inicio_parameter"] + " até " + Session["Data_final_parameter"]));

                    break;
                case "20":


                    selectcommand = "SELECT * FROM CA_Conceitos";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaConceitos.rdlc";
                    break;

                case "21":
                    Session["args"] = Session["PRMT_Ordem"];

                    selectcommand = "select * from View_CA_DiarioAprendizes where DPOrdem = " + Session["PRMT_Ordem"] + "  order by Apr_Nome";
                    var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
                    var cargahoraria = bd.View_CA_DiarioAprendizes.Where(p => p.DpOrdem == int.Parse(Session["PRMT_Ordem"].ToString())).First().DiaCargaHoraria;

                    switch (cargahoraria)
                    {
                        case 80: ReportViewer1.LocalReport.ReportPath = "reports/RPDiarioescolar20.rdlc"; break;
                        case 40: ReportViewer1.LocalReport.ReportPath = "reports/RPDiarioescolar10.rdlc"; break;
                        case 32: ReportViewer1.LocalReport.ReportPath = "reports/RPDiarioescolar8.rdlc"; break;
                        case 36: ReportViewer1.LocalReport.ReportPath = "reports/RPDiarioescolar9.rdlc"; break;
                        case 24: ReportViewer1.LocalReport.ReportPath = "reports/RPDiarioescolar6.rdlc"; break;
                        case 20: ReportViewer1.LocalReport.ReportPath = "reports/RPDiarioescolar5.rdlc"; break;
                        case 16: ReportViewer1.LocalReport.ReportPath = "reports/RPDiarioescolar4.rdlc"; break;
                        default: ReportViewer1.LocalReport.ReportPath = "reports/RPDiarioescolar20.rdlc"; break;

                    }

                    break;

                case "22":
                    Session["args"] = Session["PRMT_Turma"];

                    selectcommand = "SELECT DISTINCT Apr_Codigo, Apr_Nome,Apr_Sexo,UniNome,AreaDescricao, StaAbreviatura,Apr_Telefone, Apr_Email , " +
                        "CurDescricao,TurNome,PlanDescricao  , Apr_DataDeNascimento from View_Turma_Alunos " +
                        "where DPTurma = " + Session["PRMT_Turma"] + " AND StaCodigo = 6 ORDER BY Apr_Nome ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaAlunosTurma.rdlc";
                    break;

                case "23":
                    Session["args"] = Session["PRMT_Aprendiz"];

                    selectcommand = "Select  Apr_Codigo, Apr_Nome, Apr_NomeMae,Apr_NomePai,Apr_Sexo, TurNome, Apr_Endereço, Apr_Complemento, " +
                                "Apr_Bairro,Apr_Cidade, CurDescricao, Apr_UF, Apr_CEP,Apr_Telefone,Apr_Celular,Apr_NumeroEndereco, " +
                                " DisDescricao,DisAbreviatura,  DiaCargaHoraria, DiaNumeroFaltas, DiaConceito " +
                                "from  CA_Aprendiz  left join  CA_DisciplinasAprendiz  on  Apr_Codigo = DiaCodAprendiz " +
                                "left join CA_DisciplinasTurmaProf on  DpOrdem = DiaDisciplinaProf " +
                                "left join  CA_Disciplinas on  DisCodigo = DPDisciplina left join  CA_Turmas on  TurCodigo = DPTurma " +
                                "left join CA_Cursos on  CurCodigo = TurCurso where  Apr_Codigo =  " + Session["PRMT_Aprendiz"] +
                                " order by  DisDescricao , TurNome";

                  

                    reportcommand = "Select  Apr_Codigo, Apr_Nome, OcaDataOcorrencia, OcoDescricao, (case when  OcoTipo = 'A' then 'Advertência' else 'Ocorrência'end)  " +
                                     "as OcoTipo, OcaObservacoes  from dbo.CA_OcorrenciasAprendiz inner join  CA_Aprendiz on  Apr_Codigo = OcaCodAprendiz " +
                                     "inner join  dbo.CA_Ocorrencias on  OcoCodigo = OcaCodOcorrencia " +
                                     "where  Apr_Codigo = " + Session["PRMT_Aprendiz"] + " order by  OcaDataOcorrencia , OcoDescricao";

                    command = "select  Apr_Codigo, Apr_Nome, ALADataInicio, ALADataPrevTermino, ALADataTermino, ALAStatus, ALAValorBolsa,ALAValorTaxa, " +
                               "UsuNome, ParNomeFantasia, ParUniDescricao from CA_AlocacaoAprendiz inner join CA_ParceirosUnidade on  ALAUnidadeParceiro = ParUniCodigo  " +
                               "inner join CA_Parceiros on ParUniCodigoParceiro =  ParCodigo " +
                               "inner join  CA_Aprendiz on  ALAAprendiz = Apr_Codigo    inner join  CA_Usuarios on  ALATutor = UsuCodigo " +
                                " where  Apr_Codigo = " + Session["PRMT_Aprendiz"] + " order by ParNomeFantasia";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPFichaFuncional.rdlc";
                    break;

                case "24":
                    selectcommand = "Select count(Apr_Codigo) as QTD, convert( numeric(10,2),(( convert( numeric(10,2),count(Apr_Codigo) *100))/ convert( numeric(10,2),( select count(Apr_Situacao) " +
                   "from CA_Aprendiz inner join CA_SituacaoAprendiz on  Apr_Situacao = StaCodigo  ))))as Percentual,   CA_SituacaoAprendiz.StaDescricao " +
                   "from  dbo.CA_Aprendiz a  inner join CA_SituacaoAprendiz on  Apr_Situacao = StaCodigo group by StaDescricao order by count(Apr_Codigo) desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlunosPorSituacao.rdlc";
                    break;


                case "25":
                    selectcommand = "Select ParCodigo, count(ALAAprendiz) as QTD, convert( numeric(10,2),(( convert( numeric(10,2),count(ALAAprendiz) *100))/ convert( numeric(10,2),( select count(ParCodigo) " +
                                    "from dbo.CA_AlocacaoAprendiz inner join dbo.CA_ParceirosUnidade  on  ALAUnidadeParceiro = CA_ParceirosUnidade.ParUniCodigo inner join dbo.CA_Parceiros on  ParCodigo = ParUniCodigoParceiro " +
                                    "))))as Percentual,  ParNomeFantasia from  dbo.CA_AlocacaoAprendiz inner join CA_Turmas T on dbo.CA_AlocacaoAprendiz.ALATurma = T.TurCodigo  " +
                                    "inner join dbo.CA_ParceirosUnidade a on  ALAUnidadeParceiro = ParUniCodigo inner join dbo.CA_Parceiros on  ParCodigo = ParUniCodigoParceiro WHERE ALAStatus='A' and T.TurCurso = '002' group by ParNomeFantasia,ParCodigo,ParUniCodigoParceiro order by ParNomeFantasia";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlunosPorEmpresa.rdlc";
                    break;

                case "26":
                    Session["args"] = Session["PRMT_Empresa"] + "_" + Session["PRMT_nome_empresa"];

                    selectcommand = "Select count(ALAAprendiz) as QTD, convert( numeric(10,2),(( convert( numeric(10,2),count(ALAAprendiz) *100))/ convert( numeric(10,2),( select count(ALAUnidadeParceiro) " +
                                    "from dbo.CA_AlocacaoAprendiz inner join dbo.CA_ParceirosUnidade  on  ALAUnidadeParceiro = CA_ParceirosUnidade.ParUniCodigo where  CA_ParceirosUnidade.ParUniCodigoParceiro = a.ParUniCodigoParceiro AND ALAStatus='A'  ))))as Percentual, " +
                                    "a.ParUniDescricao from  dbo.CA_AlocacaoAprendiz inner join CA_Turmas T on dbo.CA_AlocacaoAprendiz.ALATurma = T.TurCodigo  " +
                                    "inner join dbo.CA_ParceirosUnidade a on  ALAUnidadeParceiro = a.ParUniCodigo where a.ParUniCodigoParceiro = " + Session["PRMT_Empresa"] + " AND ALAStatus='A' and T.TurCurso = '002' group by a.ParUniDescricao,ParUniCodigoParceiro   order by count(ALAAprendiz) desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlunosPorUnidade.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("NomeEmpresa", Session["PRMT_nome_empresa"].ToString()));
                    break;

                case "27":
                    /*Aprendizes ativos por turma */
                    //HFRowCount.Value
                    string a = "Select TurNome, Count(ALAAprendiz) as QTD from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Turmas ON ALATurma = TurCodigo inner join CA_Aprendiz on CA_AlocacaoAprendiz.ALAAprendiz = CA_Aprendiz.Apr_Codigo  WHERE Apr_Situacao = 6 and ALADataInicio  <= '" + Session["DataReferencia"] + "'  and  ALADataPrevTermino <= '" + Session["DataReferencia"] + "' GROUP BY TurNome order by TurNome desc";


                    selectcommand = "Select TurNome, Count(ALAAprendiz) as ALAAprendiz from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Turmas ON ALATurma = TurCodigo inner join CA_Aprendiz on CA_AlocacaoAprendiz.ALAAprendiz = CA_Aprendiz.Apr_Codigo  WHERE Apr_Situacao = 6 and ALADataInicio  <= '" + Session["DataReferencia"] + "'  and  ALADataPrevTermino >= '" + Session["DataReferencia"] + "' GROUP BY TurNome order by Count(ALAAprendiz) desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAprendizesporTurma.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("QTD", Session["QTD"].ToString()));
                    break;

                case "28":
                    /*Aprendizes ativos por área de Atuação */
                    selectcommand = @"Select AreaDescricao, count(Apr_Codigo) as Apr_Codigo from
                                        CA_Aprendiz INNER JOIN dbo.CA_AreaAtuacao ON Apr_AreaAtuacao = AreaCodigo
                                        inner join CA_AlocacaoAprendiz on CA_AlocacaoAprendiz.ALAAprendiz = CA_Aprendiz.Apr_Codigo   
                                        WHERE Apr_Situacao = 6 and ALADataInicio <= '" + Session["DataReferencia"] + "' and  ALADataPrevTermino >= '" + Session["DataReferencia"] + "'  GROUP BY AreaDescricao  order by count(Apr_Codigo) desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAprendizesporArea.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("QTD", Session["QTD"].ToString()));
                    break;

                case "29":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];

                    /*Aprendizes desligados no periodo - Sintético */
                    selectcommand = "Select StaDescricao, count(ALAAprendiz) as QTD from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                                    "INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo WHERE ALAStatus = 'I'  AND Apr_Situacao <> 6  " +
                                    "AND Apr_FimAprendizagem BETWEEN '" + Session["Data_inicio_parameter"] + "' AND '" + Session["Data_final_parameter"] + "' GROUP BY StaDescricao order by count(ALAAprendiz)desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPDesligadosSituacao.rdlc";
                    break;

                case "30":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];
                    /*Aprendizes desligados no periodo - Analítico */

                    selectcommand = "SELECT CA_Aprendiz.Apr_Codigo, CA_Aprendiz.Apr_Nome, CA_Aprendiz.Apr_FimAprendizagem,   CA_Aprendiz.Apr_InicioAprendizagem,max(alocacao.ALAUnidadeParceiro) AS PrimeiroDeALAUnidadeParceiro, CA_Parceiros.ParDescricao, CA_ParceirosUnidade.ParUniDescricao, alocacao.ALAValorBolsa, alocacao.ALAValorTaxa, " +
                                        "(select TurNome from  CA_Turmas where TurCodigo = max(alocacao.ALATurma))  AS TurNome, ALApagto = case when alocacao.ALApagto = 'E' then 'Empresa' when alocacao.ALApagto = 'F' then 'Fundação' end , CA_Aprendiz.Apr_Situacao,CA_SituacaoAprendiz.StaDescricao FROM CA_Aprendiz INNER JOIN CA_AlocacaoAprendiz alocacao ON CA_Aprendiz.Apr_Codigo = alocacao.ALAAprendiz " +
                                        "INNER JOIN CA_ParceirosUnidade ON alocacao.ALAUnidadeParceiro = CA_ParceirosUnidade.ParUniCodigo INNER JOIN CA_Parceiros ON CA_ParceirosUnidade.ParUniCodigoParceiro = CA_Parceiros.ParCodigo " +
                                        "INNER JOIN CA_SituacaoAprendiz ON CA_Aprendiz.Apr_Situacao = CA_SituacaoAprendiz.StaCodigo GROUP BY CA_Aprendiz.Apr_Codigo, CA_Aprendiz.Apr_Nome, CA_Aprendiz.Apr_FimAprendizagem, CA_Parceiros.ParDescricao,  " +
                                        "CA_ParceirosUnidade.ParUniDescricao, alocacao.ALAValorBolsa, alocacao.ALAValorTaxa, alocacao.ALApagto, CA_Aprendiz.Apr_Situacao, CA_SituacaoAprendiz.StaDescricao, CA_Aprendiz.Apr_InicioAprendizagem " +
                                        "HAVING CA_Aprendiz.Apr_FimAprendizagem Between '" + Session["Data_inicio_parameter"] + "' AND '" + Session["Data_final_parameter"] + "' ORDER BY CA_Aprendiz.Apr_Nome ";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPDesligadosAnalitico.rdlc";
                    break;

                case "31":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];
                    /*Aprendizes desligados por sexo no periodo */
                    selectcommand = "Select (Case when Apr_sexo = 'M' then 'Masculino' when Apr_sexo = 'F' then 'Feminino' else 'ND' end) as sexo , " +
                                    "count(Apr_sexo) as QTD from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo " +
                                    "INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo WHERE ALAStatus = 'I' And  Apr_Situacao <> 6 AND Apr_FimAprendizagem " +
                                    "BETWEEN '" + Session["Data_inicio_parameter"] + "' AND '" + Session["Data_final_parameter"] + "' group by Apr_sexo";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPDesligadosGenero.rdlc";
                    break;

                case "32":
                    Session["args"] = Session["MesRef"];

                    selectcommand = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                    "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE  ALAStatus = 'A' AND DATEPART(MONTH,Apr_DataDeNascimento) =  " +
                     Session["MesRef"] + " " + "ORDER BY DATEPART(MONTH,Apr_DataDeNascimento) , DATEPART(Day,Apr_DataDeNascimento) ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAniversariantes.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", Session["MesRef"].ToString()));
                    break;

                case "33":
                    Session["args"] = Session["PRMT_Curso"] + "_" + Session["MesRef"];

                    selectcommand = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                    "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE ALAStatus = 'A' AND  TurCurso = '" + Session["PRMT_Curso"] + "'  ORDER BY DATEPART(MONTH,Apr_DataDeNascimento) , DATEPART(Day,Apr_DataDeNascimento) ";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPAniversariantes.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", Session["MesRef"].ToString()));
                    break;

                case "34":
                    Session["args"] = Session["PRMT_Curso"] + "_" + Session["MesRef"];

                    selectcommand = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                   "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE  ALAStatus = 'A' AND DATEPART(MONTH,Apr_DataDeNascimento) =  " +
                   Session["MesRef"] + "  AND   TurCurso = '" + Session["PRMT_Curso"] + "' ORDER BY DATEPART(MONTH,Apr_DataDeNascimento), DATEPART(Day,Apr_DataDeNascimento) ";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPAniversariantes.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", Session["MesRef"].ToString()));
                    break;

                case "35":
                    Session["args"] = Session["PRMT_Turma"] + "_" + Session["MesRef"];

                    selectcommand = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                   "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE  ALAStatus = 'A' AND  " +
                    "TurCodigo = " + Session["PRMT_Turma"] + " ORDER BY DATEPART(MONTH,Apr_DataDeNascimento), DATEPART(Day,Apr_DataDeNascimento) ";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPAniversariantes.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", Session["MesRef"].ToString()));
                    break;

                case "36":
                    Session["args"] = Session["PRMT_Turma"] + "_" + Session["MesRef"];

                    selectcommand = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                  "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE  ALAStatus = 'A' AND DATEPART(MONTH,Apr_DataDeNascimento) =  " +
                  Session["MesRef"] + "  AND   TurCodigo = " + Session["PRMT_Turma"] + " ORDER BY  DATEPART(MONTH,Apr_DataDeNascimento), DATEPART(Day,Apr_DataDeNascimento)";


                    ReportViewer1.LocalReport.ReportPath = "reports/RPAniversariantes.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", Session["MesRef"].ToString()));
                    break;

                case "37":
                    Session["args"] = Session["PRMT_User"];

                    selectcommand = "select * from dbo.View_Resultado_Final where Apr_Codigo = " + Session["PRMT_User"] + "";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPBoletimAluno.rdlc";
                    break;

                case "38":
                    Session["args"] = Session["PRMT_User"];

                    selectcommand = "Select Apr_Codigo,Apr_Nome, View_CA_CronogramaAulas.Turma, View_CA_CronogramaAulas.Disciplina, " +
                                    "View_CA_CronogramaAulas.Professor, View_CA_CronogramaAulas.ADPDataAula, " +
                                    "View_Resultado_Final.CurDescricao, ParNomeFantasia from  View_CA_CronogramaAulas inner join  " +
                                    "CA_DisciplinasAprendiz on View_CA_CronogramaAulas.DpOrdem = CA_DisciplinasAprendiz.DiaDisciplinaProf " +
                                    "inner join View_Resultado_Final on View_Resultado_Final.DpOrdem = View_CA_CronogramaAulas.DpOrdem AND  Apr_Codigo = CA_DisciplinasAprendiz.DiaCodAprendiz " +
                                    "where Apr_Codigo = " + Session["PRMT_User"] + "";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaAluno.rdlc";
                    break;

                case "39":
                    selectcommand = "Select Apr_Nome,TurNome,ParNomeFantasia,ParUniDescricao, ALADataInicio, ALADataPrevTermino,ALADataTermino, Apr_PrevFimAprendizagem, Apr_InicioAprendizagem, ALAValorBolsa, ALAValorTaxa, " +
                                    "ALApagto = case when ALApagto = 'E' then 'Empresa' when ALApagto = 'F' then 'Fundação' end , StaDescricao , datediff( D, Apr_InicioAprendizagem,Apr_PrevFimAprendizagem) /30 as NumMeses from dbo.CA_AlocacaoAprendiz " +
                                    "INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo  " +
                                    "INNER JOIN CA_Turmas ON ALATurma = TurCodigo LEFT JOIN CA_ParceirosUnidade ON ALAUnidadeParceiro = ParUniCodigo  " +
                                    "LEFT JOIN CA_Parceiros ON  ParUniCodigoParceiro = ParCodigo WHERE ALAStatus = 'A' and CA_Turmas.TurCurso = '002' AND ALADataInicio <= GetDate() order By ParNomeFantasia,Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAprendizesParceiroAnalitico.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("parceiro", "Todos."));
                    break;
                case "40":
                    Session["args"] = Session["PRMT_dataInicio"] + "_" + Session["PRMT_dataFinal"];

                    selectcommand = "select * from dbo.View_CA_CronogramaAulas where ADPDataAula BETWEEN '" + Session["PRMT_dataInicio"] + "' AND '" + Session["PRMT_dataFinal"] + "'";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaIntervalo.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataIni", Session["PRMT_dataInicio"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFim", Session["PRMT_dataFinal"].ToString()));

                    break;

                case "41":
                    Session["args"] = Session["PRMT_dataInicio"] + "_" + Session["PRMT_dataFinal"];

                    selectcommand = "Select StaDescricao, MotDescricao, Count(MotCodigo) As QTD  from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_SituacaoAprendiz ON ALAStatus = StaCodigo " +
                        "inner join dbo.CA_MotivoDesligamento on MotCodigo = ALAMotivoDesligamento WHERE ALAStatus <> 6  " +
                        "AND ALADataTermino BETWEEN '" + Session["PRMT_dataInicio"] + "' AND '" + Session["PRMT_dataFinal"] + "' GROUP BY StaDescricao,MotDescricao  order by count(MotCodigo) desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPDesligadosMotivoIntervalo.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataIni", Session["PRMT_dataInicio"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFim", Session["PRMT_dataFinal"].ToString()));

                    break;


                case "42":
                    Session["args"] = Session["PRMT_Ordem"] + "_" + Session["data_lista"];

                    selectcommand = "SELECT * FROM  View_Turma_Alunos  inner join CA_AlocacaoAprendiz on  " +
                                    "Apr_Codigo = ALAAprendiz AND ALAturma = DPTurma where DPOrdem = " + Session["PRMT_Ordem"] + "  AND StaCodigo = 6 And ALAStatus = 'A' Order by Apr_Nome";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaGenericaAlunos.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("Data_Aula", Session["data_lista"].ToString()));
                    break;

                case "43":
                    Session["args"] = Session["PRMT_Tipo"] + "_" + Session["PRMT_Nome"] + "_" + Session["PRMT_MesRef"] + "_" + Session["PRMT_AnoRef"];

                    var tipo = Session["PRMT_Tipo"].ToString();
                    string titulo = "Geral";
                    var orderby = " order by ALApagto, Apr_Nome";

                    switch (tipo)
                    {
                        //case "tab-0":
                        //    selectcommand = "select * from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                        //          "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND ALADataInicio <= GETDATE() And ALADataPrevTermino >= getdate() order by ALApagto, Apr_Nome";
                        //    titulo = "Geral";
                        //    break;
                        //case "tab-1":
                        //    selectcommand = "select * from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                        //          "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND DATEPART(m, Apr_InicioAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                        //                    " DATEPART(yyyy, Apr_InicioAprendizagem) = " + Session["PRMT_AnoRef"] + " order by ALApagto, Apr_Nome";
                        //    titulo = "Por data de Início";
                        //    break;
                        //case "tab-2":
                        //    selectcommand = "select * from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                        //        "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%'  AND  DATEPART(m, Apr_FimAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                        //                  " DATEPART(yyyy, Apr_FimAprendizagem) = " + Session["PRMT_AnoRef"] + " order by ALApagto, Apr_Nome";
                        //    titulo = "Por data de Término";
                        //    break;
                        //case "tab-3":
                        //    selectcommand = "select * from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                        //        "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND DATEPART(m, Apr_PrevFimAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                        //                  " DATEPART(yyyy, Apr_PrevFimAprendizagem) = " + Session["PRMT_AnoRef"] + " order by ALApagto, Apr_Nome";
                        //    titulo = "Por Previsão de Término";
                        //    break;





                        case "tab-0":
                            selectcommand = "select * from dbo.View_AlocacoesAlunos where TurCurso <> '003' and  ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                                  "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' ";
                            titulo = "Geral";

                            if (Session["parceiroImpressao"] != null)
                            {
                                //selected = selected.Where(p => p.ParCodigo.Equals(DDParceiro.SelectedValue));
                                selectcommand += " and ParCodigo = " + Session["parceiroImpressao"] + " ";
                            }

                            if (Session["TurmaImpressao"] != null)
                            {
                                //selected = selected.Where(p => p.TurCodigo.Equals(DDturma_pesquisa.SelectedValue));
                                selectcommand += " and TurCodigo = " + Session["TurmaImpressao"] + " ";
                            }

                            selectcommand += orderby;

                            break;
                        case "tab-1":
                            selectcommand = "select * from dbo.View_AlocacoesAlunos where TurCurso = '002' and ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                                  "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND DATEPART(m, Apr_InicioAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                            " DATEPART(yyyy, Apr_InicioAprendizagem) = " + Session["PRMT_AnoRef"] + " order by ALApagto, Apr_Nome";
                            titulo = "Por data de Início";
                            break;
                        case "tab-2":
                            selectcommand = "select * from dbo.View_AlocacoesAlunos where TurCurso <> '003' and  ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                                "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%'  AND  DATEPART(m, Apr_FimAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                          " DATEPART(yyyy, Apr_FimAprendizagem) = " + Session["PRMT_AnoRef"] + " order by ALApagto, Apr_Nome";
                            titulo = "Por data de Término";
                            break;
                        case "tab-3":
                            selectcommand = "select * from dbo.View_AlocacoesAlunos where TurCurso = '002' and ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                                "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND DATEPART(m, Apr_PrevFimAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                          " DATEPART(yyyy, Apr_PrevFimAprendizagem) = " + Session["PRMT_AnoRef"] + " order by ALApagto, Apr_Nome";
                            titulo = "Por Previsão de Término";
                            break;
                    }

                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaAtivos.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("TipoRelatorio", titulo));
                    break;

                case "44":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];

                    /*Aprendizes desligados por idade no periodo */
                    selectcommand = "SELECT Idade, COUNT(Idade) as QTD from  View_Idade_Alocacao WHERE ALAStatus = 'I' And  Apr_Situacao <> 6 " +
                                    "AND Apr_FimAprendizagem BETWEEN '" + Session["Data_inicio_parameter"] + "' AND '" + Session["Data_final_parameter"] + "' group by Idade order by COUNT(Idade)desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPDesligadosIdade.rdlc";
                    break;

                case "45":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];

                    /*Aprendizes Alocados por Parceiro no periodo */
                    selectcommand = "Select ParNomeFantasia, count(ALAAprendiz) as QTD from dbo.CA_AlocacaoAprendiz INNER JOIN CA_ParceirosUnidade ON ALAUnidadeParceiro = ParUniCodigo " +
                        "INNER JOIN CA_Parceiros ON  ParUniCodigoParceiro = ParCodigo INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo  WHERE ALAStatus = 'A' " +
                        "AND Apr_InicioAprendizagem BETWEEN '" + Session["Data_inicio_parameter"] + "' AND '" +
                        Session["Data_final_parameter"] + "' GROUP BY ParNomeFantasia order by count(ALAAprendiz) desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlocadosParceiroPeriodo.rdlc";
                    break;

                case "46":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];

                    /*Aprendizes Alocados  no periodo - Analitico */
                    selectcommand = "Select Apr_Nome,TurNome,ParNomeFantasia,ParUniDescricao, ALADataInicio,ALADataPrevTermino,ALADataTermino, ALAValorBolsa,  " +
                                    "ALAValorTaxa,  Expr1 as Situacao,StaDescricao, ALApagto from  dbo.View_AlocacoesAlunos WHERE ALAStatus ='A' And  Apr_Situacao = 6 AND Apr_InicioAprendizagem " +
                                    "BETWEEN '" + Session["Data_inicio_parameter"] + "' AND '" + Session["Data_final_parameter"] + "'";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlocadosAnalitico.rdlc";
                    break;

                case "47":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];

                    /*Aprendizes Alocados  no periodo por gênero */
                    selectcommand = "Select (Case when Apr_sexo = 'M' then 'Masculino' when Apr_sexo = 'F' then 'Feminino' else 'ND' end) as sexo , " +
                                    "count(Apr_sexo) as QTD from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo " +
                                    "INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo WHERE ALAStatus = 'A' And  Apr_Situacao = 6 AND Apr_InicioAprendizagem  " +
                                    "BETWEEN '" + Session["Data_inicio_parameter"] + "' AND '" + Session["Data_final_parameter"] + "' group by Apr_sexo";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlocadosGenero.rdlc";
                    break;

                case "48":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];

                    /*Aprendizes Alocados no periodo por idade */
                    selectcommand = "SELECT Idade, COUNT(Idade) as QTD from  View_Idade_Alocacao WHERE ALAStatus = 'A' AND Apr_InicioAprendizagem " +
                                    "BETWEEN  '" + Session["Data_inicio_parameter"] + "' AND '" + Session["Data_final_parameter"] + "' group by Idade order by COUNT(Idade) desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlocadosIdade.rdlc";
                    break;

                case "49":
                    Session["args"] = Session["Data_inicio_parameter"] + "_" + Session["Data_final_parameter"];

                    selectcommand = "Select StaDescricao, MotDescricao, Count(MotCodigo) As QTD  from dbo.CA_AlocacaoAprendiz " +
                                    "INNER JOIN dbo.CA_SituacaoAprendiz ON ALAStatus = StaCodigo INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo " +
                                     "INNER JOIN dbo.CA_MotivoDesligamento on MotCodigo = ALAMotivoDesligamento WHERE ALAStatus ='I' And  Apr_Situacao <> 6 " +
                                     "AND ALADataTermino BETWEEN '" + Session["Data_inicio_parameter"] + "' AND '" + Session["Data_final_parameter"] + "'  GROUP BY StaDescricao,MotDescricao  order by count(MotCodigo) desc";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPMotivosDesligamento.rdlc";
                    break;

                case "50":
                    Session["args"] = Session["PRMT_Ano"] + "_" + Session["PRMT_Mes"] + "_" + Session["PRMT_Parceiro"] + "_" + Session["PRMT_Parceiro_nome"];

                    if (Session["PRMT_Parceiro"].ToString().Equals(string.Empty))
                        selectcommand = "select * from  View_CA_Fechamento_Mensal where  FechAnoFechamento = '" + Session["PRMT_Ano"] + "' AND FechMesFechamento = '" + Session["PRMT_Mes"] + "' order by Apr_Nome";
                    else
                        selectcommand = "select * from  View_CA_Fechamento_Mensal where  FechAnoFechamento = '" + Session["PRMT_Ano"] + "' AND FechMesFechamento = '" + Session["PRMT_Mes"] + "'  AND ParCodigo = '" + Session["PRMT_Parceiro"] + "' order by Apr_Nome";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPFechamentoAnalitico.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("AnoRef", Session["PRMT_Ano"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", Session["PRMT_Mes"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("ParceiroRef", Session["PRMT_Parceiro_nome"].ToString().Equals("Selecione") ? "Não Informado" : Session["PRMT_Parceiro_nome"].ToString()));
                    break;

                case "51":
                    Session["args"] = Session["PRMT_Parceiro"] + "_" + Session["PRMT_Mes"] + "_" + Session["PRMT_Ano"] + "_" + Session["PRMT_Parceiro_nome"];

                    if (Session["PRMT_Parceiro"].ToString().Equals(string.Empty))
                        selectcommand = "select FechPagamento = case when FechPagamento = 'F' then 'Fundação' when FechPagamento = 'E' then 'Empresa'  else 'Não Definido' end, " +
                          "count(FechPagamento) as QTD, sum(FechTaxa) as TotalTaxa, sum(FechBolsa) as TotalBolsa from  CA_Fechamento " +
                          "where FechMesFechamento = " + Session["PRMT_Mes"] + " And FechAnoFechamento = '" + Session["PRMT_Ano"] + "' group by FechPagamento order by Apr_Nome";
                    else
                        selectcommand = "select FechPagamento = case when FechPagamento = 'F' then 'Fundação' when FechPagamento = 'E'then 'Empresa'  else 'Não Definido' end, " +
                            "count(FechPagamento) as QTD, sum(FechTaxa) as TotalTaxa, sum(FechBolsa) as TotalBolsa from  CA_Fechamento " +
                            "inner join CA_ParceirosUnidade on ParUniCodigo = FechUnidade inner join CA_Parceiros on ParUniCodigoParceiro = ParCodigo " +
                            "where FechMesFechamento = " + Session["PRMT_Mes"] + " And FechAnoFechamento = '" + Session["PRMT_Ano"] + "' AND ParCodigo = " + Session["PRMT_Parceiro"] + "  group by FechPagamento order by Apr_Nome";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPFechamentoSintetico.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("AnoRef", Session["PRMT_Ano"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", Session["PRMT_Mes"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("ParceiroRef", Session["PRMT_Parceiro_nome"].ToString().Equals("Selecione") ? "Não Informado" : Session["PRMT_Parceiro_nome"].ToString()));
                    break;


                case "52":
                    Session["args"] = Session["PRMT_Parceiro"] + "_" + Session["PRMT_Mes"] + "_" + Session["PRMT_Ano"] + "_" + Session["PRMT_Parceiro_nome"];

                    if (Session["PRMT_Parceiro"].ToString().Equals(string.Empty))
                        selectcommand = "select ParNomeFantasia, FechPagamento = case when FechPagamento = 'F' then 'Fundação' when FechPagamento = 'E' then 'Empresa'   " +
                                "else 'Não Definido' end, count(FechPagamento) as QTD, sum(FechTaxa) as TotalTaxa, sum(FechBolsa) as TotalBolsa  " +
                                "from  CA_Fechamento inner join CA_ParceirosUnidade on ParUniCodigo = FechUnidade " +
                                "inner join CA_Parceiros on ParUniCodigoParceiro = ParCodigo " +
                                "where FechMesFechamento = " + Session["PRMT_Mes"] + "  And FechAnoFechamento = '" + Session["PRMT_Ano"] + "' group by FechPagamento,ParNomeFantasia order by  ParNomeFantasia";
                    else
                        selectcommand = "select ParNomeFantasia, FechPagamento = case when FechPagamento = 'F' then 'Fundação' when FechPagamento = 'E' then 'Empresa'   " +
                                "else 'Não Definido' end, count(FechPagamento) as QTD, sum(FechTaxa) as TotalTaxa, sum(FechBolsa) as TotalBolsa  " +
                                "from  CA_Fechamento inner join CA_ParceirosUnidade on ParUniCodigo = FechUnidade " +
                                "inner join CA_Parceiros on ParUniCodigoParceiro = ParCodigo " +
                                "where FechMesFechamento = " + Session["PRMT_Mes"] + "  And FechAnoFechamento = '" + Session["PRMT_Ano"] + "' " +
                                "and ParCodigo = " + Session["PRMT_Parceiro"] + "  group by FechPagamento,ParNomeFantasia order by  ParNomeFantasia";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPFechamentoSinteticoParceiro.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("AnoRef", Session["PRMT_Ano"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", Session["PRMT_Mes"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("ParceiroRef", Session["PRMT_Parceiro_nome"].ToString().Equals("Selecione") ? "Não Informado" : Session["PRMT_Parceiro_nome"].ToString()));
                    break;

                case "53":
                    Session["args"] = Session["PRMT_Turma"];

                    selectcommand = " SELECT Apr_Codigo, Apr_Nome,Apr_Sexo,ParNomeFantasia, ParUniDescricao,AreaDescricao, Apr_Telefone, Apr_Email , CurDescricao,TurNome,PlanDescricao  , Apr_DataDeNascimento FROM CA_Aprendiz INNER JOIN CA_AlocacaoAprendiz on CA_Aprendiz.Apr_Codigo = CA_AlocacaoAprendiz.ALAAprendiz " +
                                    "INNER JOIN CA_ParceirosUnidade on dbo.CA_ParceirosUnidade.ParUniCodigo = dbo.CA_AlocacaoAprendiz.ALAUnidadeParceiro INNER JOIN CA_Parceiros ON dbo.CA_Parceiros.ParCodigo = dbo.CA_ParceirosUnidade.ParUniCodigoParceiro " +
                                    "INNER JOIN dbo.CA_Turmas on ALATurma = TurCodigo INNER JOIN CA_Cursos on TurCurso = CurCodigo INNER JOIN dbo.CA_Planos on TurPlanoCurricular = PlanCodigo INNER JOIN dbo.CA_AreaAtuacao on  Apr_AreaAtuacao = AreaCodigo " +
                                    "WHERE  CA_AlocacaoAprendiz.ALATurma = " + Session["PRMT_Turma"] + " AND  CA_Aprendiz.Apr_Situacao = 6  And  CA_AlocacaoAprendiz.ALAStatus = 'A' order by  Apr_Nome";


                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaAlunosTurma.rdlc";
                    break;


                case "54":
                    Session["args"] = Session["PRMT_Aprendiz"];

                    //selectcommand = "select Apr_Codigo, Apr_Nome, AreaNumeroCadastro,AreaDescricao, AreaCargaHoraria,Apr_InicioAprendizagem, ALAInicioExpediente, ALATerminoExpediente, ParNomeFantasia, ParUniCNPJ, " +
                    //                "ParUniEndereco, ParUniNumeroEndereco, ParUniComplemento, ParUniBairro,ParUniCidade,ParUniEstado,ParUniCEP, OriNome,OriTelefone, ParUniTelefone " +
                    //                "from dbo.CA_Aprendiz inner join dbo.CA_AlocacaoAprendiz on ALAAprendiz = Apr_Codigo inner join dbo.CA_AreaAtuacao on  Apr_AreaAtuacao = AreaCodigo " +
                    //                "inner join dbo.CA_ParceirosUnidade on ALAUnidadeParceiro = ParUniCodigo inner join dbo.CA_Parceiros on  ParCodigo = ParUniCodigoParceiro " +
                    //                "left join dbo.CA_Orientador on  OriCodigo = ALAOrientador where Apr_Codigo = " + Session["PRMT_Aprendiz"] + "";

                    selectcommand = "select * from View_Contrato_Aprediz where Apr_Codigo = " + Session["PRMT_Aprendiz"] + "";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPDeclaracaoMatricula.rdlc";
                    break;
                case "55":
                    Session["args"] = Session["prmt_ano"] + "_" + Session["prmt_mes"] + "_" + Session["prmt_user"] + "_" + Session["prmt_opt"];

                    selectcommand = "SELECT  PepCodigo, Apr_Codigo, PesNome, Apr_Nome, ParCodigo,ParNomeFantasia, ParUniDescricao, ParUniCodigoParceiro, PepRealizada, PepAno, PepMes, " +
                          "(CASE WHEN PepRealizada = 'N' THEN 'Pendente' ELSE 'Realizada' END) AS Situacao, OriNome, OriTelefone, OriEmail " +
                          "FROM  dbo.CA_Pesquisa_Parceiro inner join dbo.CA_ParceirosUnidade ON ParUniCodigo = PepParceiroCodigo " +
                          "inner join CA_Parceiros ON ParCodigo = ParUniCodigoParceiro inner join CA_Aprendiz ON Apr_Codigo = PepAprendiz " +
                          " left join CA_Orientador ON PepOrientador = OriCodigo left join CA_Usuarios on UsuCodigo = ParRespFundacao  inner join CA_Pesquisa on PepPesquisaCodigo = PesCodigo " +
                          "WHERE  PepAno = '" + Session["prmt_ano"] + "' AND  PepMes = " + Session["prmt_mes"] + " ";

                    if (Session["prmt_user"] != null && !string.IsNullOrEmpty(Session["prmt_user"].ToString()))
                        selectcommand += " AND ParRespFundacao= '" + Session["prmt_user"] + "'";

                    switch (Session["prmt_opt"].ToString())
                    {
                        case "1": selectcommand += "AND PepRealizada = 'N'"; break;
                        case "2": selectcommand += "AND PepRealizada = 'S'"; break;
                    }
                    selectcommand += " order by ParNomeFantasia, Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaPesquisa.rdlc";
                    break;

                case "56":
                    Session["args"] = Session["prmt_turma"] + "_" + Session["prmt_mes"] + "_" + Session["prmt_ano"];

                    selectcommand = "Select * from View_controle_frequencia where  ALATurma = " + Session["prmt_turma"] + " and FreqReferencia = '" +
                        Session["prmt_mes"] + "/" + Session["prmt_ano"] + "'";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPControleFrequencia.rdlc";
                    break;

                case "57":
                    Session["args"] = Session["prmt_ordem"];

                    selectcommand = "Select * from View_controle_frequencia where FreqOrdem =  " + Session["prmt_ordem"] + " ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPControleFrequencia.rdlc";
                    break;

                case "58":
                    Session["args"] = Session["prmt_mes"] + "_" + Session["prmt_ano"];

                    var mesref = Session["prmt_mes"] + "/" + Session["prmt_ano"];
                    selectcommand = "SELECT * FROM View_controle_frequencia WHERE FreqReferencia = '" + mesref + "' order by Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPControleFrequenciaAnalitico.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("MesRef", mesref));
                    break;

                case "59":
                    Session["args"] = Session["prmt_data_inicio"] + "_" + Session["prmt_data_fim"];

                    selectcommand = "SELECT  * FROM View_Pesquisas_Realizadas WHERE  PepDataRealizada " +
                                 " BETWEEN '" + Session["prmt_data_inicio"] + "' AND  '" + Session["prmt_data_fim"] + "' AND PepRealizada = 'S'  order by ParNomeFantasia, Apr_Nome";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPPesquisasRealizadas.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataInicio", Session["prmt_data_inicio"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFinal", Session["prmt_data_fim"].ToString()));
                    break;


                case "60":
                    Session["args"] = Session["prmt_data_inicio"] + "_" + Session["prmt_data_fim"];

                    selectcommand = "Select count(PepTutor) as QTD,UsuNome,PesNome  from CA_Pesquisa_Parceiro inner join CA_Pesquisa on PesCodigo =  PepPesquisaCodigo " +
                                    "left join CA_Usuarios on UsuCodigo = PepTutor  WHERE  PepDataRealizada BETWEEN '" + Session["prmt_data_inicio"] + "' AND  '" + Session["prmt_data_fim"] + "' AND PepRealizada = 'S' group by UsuNome,PesNome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPRealizadasporTutor.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataInicio", Session["prmt_data_inicio"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFinal", Session["prmt_data_fim"].ToString()));
                    break;


                case "61":
                    Session["args"] = Session["prmt_mes"] + "_" + Session["prmt_ano"];

                    var mesreferencia = Session["prmt_mes"] + "/" + Session["prmt_ano"];
                    selectcommand = "SELECT * FROM View_controle_frequencia WHERE FreqReferencia = '" + mesreferencia + "' order by Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPControleFrequencia.rdlc";
                    break;

                case "62":
                    Session["args"] = Session["PRMT_Ordem"];

                    selectcommand = "SELECT * FROM View_parecer_Tecnico WHERE DPOrdem = '" + Session["PRMT_Ordem"] + "' ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPParTecPreenchido.rdlc";
                    break;

                case "63":
                    selectcommand = @"select  (case when ALApagto ='C' then 'CejaBrasil' when ALApagto ='E' then 'Empresa' end  ) as ALApagto,
                                    count(ALApagto) as DpOrdem 
                                    from dbo.CA_AlocacaoAprendiz 
                                    inner join CA_Aprendiz on CA_AlocacaoAprendiz.ALAAprendiz = CA_Aprendiz.Apr_Codigo  
                                    where Apr_Situacao = 6 and ALADataInicio  <= '" + Session["dataReferencia"] + "' and  ALADataPrevTermino >='" + Session["dataReferencia"] + "' group by ALApagto";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaTipoPagamento.rdlc";
                    break;

                case "64":
                    Session["args"] = Session["prmnt_disciplina"] + "_" + Session["prmnt_data"] + "_" + Session["prmnt_disciplina_nome"];

                    selectcommand = "select Apr_Codigo, Apr_Nome, Apr_Sexo, TurNome, ParDescricao, ParUniDescricao  from dbo.CA_Aprendiz  inner join dbo.CA_DisciplinasAprendiz on Apr_Codigo = DiaCodAprendiz " +
                            "inner join dbo.CA_DisciplinasTurmaProf on DpOrdem =  DiaDisciplinaProf inner join dbo.CA_AlocacaoAprendiz on ALAAprendiz = Apr_Codigo AND ALATurma = DPTurma " +
                            "inner join dbo.CA_ParceirosUnidade on ParUniCodigo = ALAUnidadeParceiro inner join dbo.CA_Parceiros on ParCodigo = ParUniCodigoParceiro " +
                            "inner join dbo.CA_Turmas on TurCodigo = ALATurma AND TurCodigo = DPTurma where DPDataInicio   = '" + Session["prmnt_data"] + "' AND DiaDestaque = 'S' " +
                            "AND DPDisciplina = " + Session["prmnt_disciplina"] + " order by TurNome, Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPDestaques.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("Disciplina", Session["prmnt_disciplina_nome"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("Data", Session["prmnt_data"].ToString()));
                    break;

                case "65":
                    Session["args"] = Session["prmnt_data_inicio"] + "_" + Session["prmnt_data_Final"] + "_" + Session["prmnt_Parceiro"] + "_" +
                         Session["prmnt_Unidade"] + "_" + Session["prmnt_Tipo_Pagamento"] + "_" + Session["filter_parameter"];

                    selectcommand = "SELECT * FROM View_Notas_Faltas WHERE DPDataInicio BETWEEN '" + Session["prmnt_data_inicio"] + "' AND '" + Session["prmnt_data_Final"] + "' ";
                    if (!string.IsNullOrEmpty(Session["prmnt_Parceiro"].ToString()))
                        selectcommand += " AND ParUniCodigoParceiro = " + Session["prmnt_Parceiro"] + "";

                    if (!string.IsNullOrEmpty(Session["prmnt_Unidade"].ToString()))
                        selectcommand += " AND ParUniCodigo = " + Session["prmnt_Unidade"] + "";

                    if (!string.IsNullOrEmpty(Session["prmnt_Tipo_Pagamento"].ToString()))
                        selectcommand += " AND ALApagto = '" + Session["prmnt_Tipo_Pagamento"] + "' ";

                    selectcommand += " order By ParDescricao, ParUniDescricao, Apr_Nome";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPNotasPeriodo.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("filtro", WebUtility.HtmlDecode(Session["filter_parameter"].ToString())));
                    break;

                case "66":
                    Session["args"] = Session["PRMT_Turma"];

                    selectcommand = " SELECT Apr_Codigo, Apr_Nome, Apr_CPF, Apr_Sexo,ParNomeFantasia, ParUniDescricao,AreaDescricao, Apr_Telefone, Apr_Email , CurDescricao,TurNome,PlanDescricao  , Apr_DataDeNascimento FROM CA_Aprendiz INNER JOIN CA_AlocacaoAprendiz on CA_Aprendiz.Apr_Codigo = CA_AlocacaoAprendiz.ALAAprendiz " +
                                    "INNER JOIN CA_ParceirosUnidade on dbo.CA_ParceirosUnidade.ParUniCodigo = dbo.CA_AlocacaoAprendiz.ALAUnidadeParceiro INNER JOIN CA_Parceiros ON dbo.CA_Parceiros.ParCodigo = dbo.CA_ParceirosUnidade.ParUniCodigoParceiro " +
                                    "INNER JOIN dbo.CA_Turmas on ALATurma = TurCodigo INNER JOIN CA_Cursos on TurCurso = CurCodigo INNER JOIN dbo.CA_Planos on TurPlanoCurricular = PlanCodigo INNER JOIN dbo.CA_AreaAtuacao on  Apr_AreaAtuacao = AreaCodigo " +
                                    "WHERE  CA_AlocacaoAprendiz.ALATurma = " + Session["PRMT_Turma"] + " AND  CA_Aprendiz.Apr_Situacao = 6  And  CA_AlocacaoAprendiz.ALAStatus = 'A' order by  Apr_Nome";


                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaAlunosCPF.rdlc";
                    break;

                case "67":
                    Session["args"] = Session["PRMT_Empresa"] + "_" + Session["PRMT_nome_empresa"];

                    selectcommand = "Select Apr_Nome,TurNome,ParNomeFantasia,ParUniDescricao, ALADataInicio, ALADataPrevTermino,ALADataTermino, Apr_PrevFimAprendizagem, Apr_InicioAprendizagem, ALAValorBolsa, ALAValorTaxa, " +
                                    "ALApagto = case when ALApagto = 'E' then 'Empresa' when ALApagto = 'F' then 'Fundação' end , StaDescricao from dbo.CA_AlocacaoAprendiz " +
                                    "INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo  " +
                                    "INNER JOIN CA_Turmas ON ALATurma = TurCodigo LEFT JOIN CA_ParceirosUnidade ON ALAUnidadeParceiro = ParUniCodigo  " +
                                    "LEFT JOIN CA_Parceiros ON  ParUniCodigoParceiro = ParCodigo WHERE ALAStatus = 'A' and CA_Turmas.TurCurso = '002' AND ALADataInicio <= GetDate() AND ParCodigo = " + Session["PRMT_Empresa"] + " order By ParNomeFantasia,Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAprendizesParceiroAnalitico.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("parceiro", WebUtility.HtmlDecode(Session["PRMT_nome_empresa"].ToString())));
                    break;

                case "68":
                    Session["args"] = Session["prmt_ordem_ocorrencia"] + "_" + Session["PRMT_nome_empresa"];

                    selectcommand = "SELECT OcaOrdem,ALAAprendiz,Apr_Nome,TurNome, ParNomeFantasia,CurDescricao, ParUniDescricao, UsuNome, OcoDescricao, OcaDataOcorrencia,OcaObservacoes " +
                                    "from CA_OcorrenciasAprendiz inner join dbo.View_AlocacoesAlunos on ALAaprendiz = OcaCodAprendiz inner join  CA_Ocorrencias on OcaCodOcorrencia = " +
                                    "OcoCodigo inner join CA_Usuarios on OcaUsuarioocorrencia = UsuCodigo where ALAStatus = 'A' AND OcaOrdem =  " + Session["prmt_ordem_ocorrencia"];

                    string report;
                    using (var repository = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                    {
                        var ordem = int.Parse(Session["prmt_ordem_ocorrencia"].ToString());
                        var dados = repository.CA_OcorrenciasAprendizs.Where(p => p.OcaOrdem == ordem).First();
                        switch (dados.OcaCodOcorrencia)
                        {
                            case 1: report = "reports/RPAdvertencia01.rdlc"; break;
                            case 3: report = "reports/RPAdvertencia02.rdlc"; break;
                            case 4: report = "reports/RPAdvertencia03.rdlc"; break;
                            default: report = "reports/RPOcorrencia.rdlc"; break;
                        }
                    }

                    ReportViewer1.LocalReport.ReportPath = report;
                    break;

                case "69":
                    selectcommand = "SELECT DescBairro,DescRegiao FROM dbo.CA_Bairros INNER JOIN dbo.CA_Regioes on RegBairro = CodRegiao";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPBairros.rdlc";
                    break;

                case "70":
                    selectcommand = "SELECT CodRegiao,DescRegiao FROM dbo.CA_Regioes";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPRegioes.rdlc";
                    break;

                case "71":
                    Session["args"] = Session["tipo_pesquisa"] + "_" + Session["prmnt_data_inicio"] + "_" + Session["prmnt_data_Final"];

                    var tipoPesquisa = Session["tipo_pesquisa"] == null ? "" : Session["tipo_pesquisa"].ToString();
                    selectcommand = "SELECT * FROM View_Advertencias_aprendizes";
                    switch (tipoPesquisa)
                    {
                        default: selectcommand += " where OcaDataOcorrencia  between '" + Session["prmnt_data_inicio"] + "' AND '" + Session["prmnt_data_Final"] + "'"; break;
                        case "2": selectcommand += " where OcaDataEntrega between '" + Session["prmnt_data_inicio"] + "' AND '" + Session["prmnt_data_Final"] + "'"; break;
                        case "3": selectcommand += " where OcaPrevDevolucao between '" + Session["prmnt_data_inicio"] + "' AND '" + Session["prmnt_data_Final"] + "'"; break;
                        case "4": selectcommand += " where OcaDevolucao between '" + Session["prmnt_data_inicio"] + "' AND '" + Session["prmnt_data_Final"] + "'"; break;
                    }
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaOcorrencias.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("Periodo", "De " + Session["prmnt_data_inicio"] + " até " + Session["prmnt_data_Final"] + "."));
                    break;

                case "72":
                    Session["args"] = Session["prmnt_data_inicio"] + "_" + Session["prmnt_data_Final"] + "_" + Session["prmnt_Parceiro"] + "_" +
                         Session["prmnt_Unidade"] + "_" + Session["prmnt_Tipo_Pagamento"] + "_" + Session["filter_parameter"];

                    selectcommand = "SELECT * FROM View_Notas_Faltas WHERE DPDataInicio BETWEEN '" + Session["prmnt_data_inicio"] + "' AND '" + Session["prmnt_data_Final"] + "' ";
                    if (!string.IsNullOrEmpty(Session["prmnt_Parceiro"].ToString()))
                        selectcommand += " AND ParUniCodigoParceiro = " + Session["prmnt_Parceiro"] + "";

                    if (!string.IsNullOrEmpty(Session["prmnt_Unidade"].ToString()))
                        selectcommand += " AND ParUniCodigo = " + Session["prmnt_Unidade"] + "";

                    if (!string.IsNullOrEmpty(Session["prmnt_Tipo_Pagamento"].ToString()))
                        selectcommand += " AND ALApagto = '" + Session["prmnt_Tipo_Pagamento"] + "' ";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPNotasPeriodoParceiro.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("filtro", WebUtility.HtmlDecode(Session["filter_parameter"].ToString())));
                    break;

                case "73":
                    selectcommand = "SELECT * FROM CA_Eventos order by EvnCodigo";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaEventos.rdlc";
                    break;

                case "74":
                    Session["args"] = Session["prmnt_evento"];

                    selectcommand = "select EvnNome, EvnData, Apr_Codigo, Apr_Nome, PrtPresenca  from  CA_Aprendiz inner join CA_Participantes on Apr_Codigo = PrtAprendiz " +
                        "inner join CA_Eventos on PrtCodigoEvento = EvnCodigo where PrtCodigoEvento = " + Session["prmnt_evento"] + " order by Apr_Nome ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAprendizesEvento.rdlc";
                    break;

                case "75":
                    if (Session["DataRel"] != null)
                    {
                        selectcommand = "Select Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, ParUniDescricao, TurNome, UniNome, AdiDataAula  from View_CA_AulasDisciplinasAprendiz where AdiDataAula = '" + Convert.ToDateTime(Session["DataRel"].ToString()) + "' and AdiTurma =  " + Session["TurCodigo"].ToString() + "and Apr_Situacao = 6 group by Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, ParUniDescricao, TurNome, UniNome, AdiDataAula order by Apr_Nome ";
                        ReportViewer1.LocalReport.ReportPath = "reports/RPListaAlunosPresenca.rdlc";
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Data", Session["DataRel"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Turma", Session["TurCodigo"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("DiaSemana", Session["DiaSemana"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Polo", Session["Polo"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Endereco", Session["Endereco"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Cep", Session["Cep"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Telefone", Session["Telefone"].ToString()));

                    }
                    break;

                case "76":

                    var where = "";
                    var dataInicio = "";
                    var dataTermino = "";

                    if (!Session["DataInicial"].Equals("") && !Session["DataInicial"].Equals(""))
                    {
                        where = " and AdiDataAula between  '" + Convert.ToDateTime(Session["DataInicial"].ToString()) + "' and  '" + Convert.ToDateTime(Session["DataFinal"].ToString()) + "' ";
                    }

                    selectcommand = "select * from View_CA_AulasDisciplinasAprendiz where 1 = 1 and AdiTurma = " + Session["TurCodigo"].ToString();
                    selectcommand += where;

                    reportcommand = "select * from CA_Disciplinas";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaTurma.rdlc";


                    if (!Session["DataInicial"].Equals("") && !Session["DataInicial"].Equals(""))
                    {
                        dataInicio = Session["DataInicial"].ToString();
                        dataTermino = Session["DataFinal"].ToString();

                    }
                    else
                    {
                        var sql2 = "select min(AdiDataAula) minimo, MAX(AdiDataAula) maximo from View_CA_AulasDisciplinasAprendiz where AdiTurma = " + Session["TurCodigo"].ToString();
                        var con2 = new Conexao();
                        var result2 = con2.Consultar(sql2);
                        dataInicio = "";
                        dataTermino = "";

                        while (result2.Read())
                        {
                            dataInicio = String.Format("{0:dd/MM/yyyy}", (DateTime)result2["minimo"]);
                            dataTermino = String.Format("{0:dd/MM/yyyy}", (DateTime)result2["maximo"]);

                        }


                    }


                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaTurma.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataInicial", dataInicio));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFinal", dataTermino));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("Turma", Session["Turma"].ToString()));
                    break;

                //teste
                case "200":

                    string where2 = "";

                    selectcommand = "select * from View_CA_AulasDisciplinasAprendiz where 1 = 1  and AdiTurma = " + Session["TurCodigo"].ToString() + "and disCodigo =" + Session["Disciplina"];




                    if (!Session["DataInicial"].Equals("") && !Session["DataInicial"].Equals(""))
                    {
                        dataInicio = Session["DataInicial"].ToString();
                        dataTermino = Session["DataFinal"].ToString();
                        selectcommand += " and AdiDataAula between  '" + Convert.ToDateTime(Session["DataInicial"].ToString()) + "' and  '" + Convert.ToDateTime(Session["DataFinal"].ToString()) + "'";

                    }
                    else
                    {
                        var sql2 = "select min(AdiDataAula) minimo, MAX(AdiDataAula) maximo from View_CA_AulasDisciplinasAprendiz where AdiTurma = " + Session["TurCodigo"].ToString();
                        var con2 = new Conexao();
                        var result2 = con2.Consultar(sql2);
                        dataInicio = "";
                        dataTermino = "";

                        while (result2.Read())
                        {
                            dataInicio = String.Format("{0:dd/MM/yyyy}", (DateTime)result2["minimo"]);
                            dataTermino = String.Format("{0:dd/MM/yyyy}", (DateTime)result2["maximo"]);

                        }


                    }




                    reportcommand = "select * from CA_Disciplinas";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaTurma.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataInicial", dataInicio));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFinal", dataTermino));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("Turma", Session["Turma"].ToString()));
                    break;

                case "77":
                    selectcommand = "select * from View_CA_AulasDisciplinasAprendiz where AdiDataAula between  '" + Convert.ToDateTime(Session["DataInicial"].ToString()) + "' and  '" + Convert.ToDateTime(Session["DataFinal"].ToString()) + "'";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaTurmaPeriodo.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataInicial", Session["DataInicial"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFinal", Session["DataFinal"].ToString()));
                    break;
                case "78":
                    selectcommand = "select * from View_CA_AulasDisciplinasAprendiz where 1 = 1 and  discodigo = " + Session["CodDisciplina"].ToString();




                    if (!Session["DataInicial"].Equals("") && !Session["DataInicial"].Equals(""))
                    {
                        dataInicio = Session["DataInicial"].ToString();
                        dataTermino = Session["DataFinal"].ToString();
                        selectcommand += " and AdiDataAula between  '" + Convert.ToDateTime(Session["DataInicial"].ToString()) + "' and  '" + Convert.ToDateTime(Session["DataFinal"].ToString()) + "'";

                    }
                    else
                    {
                        var sql2 = "select min(AdiDataAula) minimo, MAX(AdiDataAula) maximo from View_CA_AulasDisciplinasAprendiz where AdiDisciplina = " + Session["CodDisciplina"].ToString();
                        var con2 = new Conexao();
                        var result2 = con2.Consultar(sql2);
                        dataInicio = "";
                        dataTermino = "";

                        while (result2.Read())
                        {
                            dataInicio = String.Format("{0:dd/MM/yyyy}", (DateTime)result2["minimo"]);
                            dataTermino = String.Format("{0:dd/MM/yyyy}", (DateTime)result2["maximo"]);

                        }


                    }


                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaDisciplina.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataInicial", dataInicio));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFinal", dataTermino));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("CodDisciplina", Session["CodDisciplina"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("NomeDisciplina", Session["NomeDisciplina"].ToString()));
                    break;
                case "79":
                    selectcommand = "select * from CA_Disciplinas order by DisDescricao";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaDisciplinasCores.rdlc";
                    break;

                case "80": //carômetro
                    selectcommand = "select * from [dbo].[View_Carometro] where TurCodigo = " + Session["TurCodigo"].ToString() + " order by Apr_Nome ";
                    //System.Web.HttpUtility.HtmlDecode(fupArquivo.FileName)
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCarometro.rdlc";
                    break;

                case "81":
                    selectcommand = "SELECT * " +
                                    "  FROM [View_ControlePresenca] " +
                                    "  WHERE [AdiDataAula] = '" + Session["Data"].ToString() + "'";

                    if (!string.IsNullOrEmpty(Session["Turma"].ToString()))
                        selectcommand += " AND [AdiTurma] = '" + Session["Turma"] + "' ";

                    selectcommand += "  ORDER BY [TurNome], Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPControlePresenca.rdlc";
                    break;

                case "82":

                    selectcommand = "SELECT Apr_Codigo, AdiPresenca, AdiDataAula, DisDescricao, Apr_Nome " +
                                    "  FROM [View_ControlePresenca] " +
                                    "  WHERE [AdiDataAula] BETWEEN '" + Session["DataInicial"].ToString() + "' AND '" + Session["DataFinal"].ToString() + "'" +
                                    " AND [TurNome] = '" + Session["Turma"].ToString() + "' " +
                                    " GROUP BY Apr_Codigo, AdiPresenca, AdiDataAula, DisDescricao, Apr_Nome ORDER BY [Apr_Nome] ";

                    reportcommand = "SELECT Apr_Codigo,AdiPresenca, AdiDataAula " +
                                    "FROM [View_ControlePresencaAprendiz]   " +
                                    "  WHERE [AdiDataAula] BETWEEN '" + Session["DataInicial"].ToString() + "' AND '" + Session["DataFinal"].ToString() + "'" +
                                    " AND [TurNome] = '" + Session["Turma"].ToString() + "' " +
                                    "  GROUP BY Apr_Codigo ORDER BY [Apr_Nome] ";

                    ReportViewer1.LocalReport.ReportPath = "reports/RPControlePresencaPorPeriodo.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("Titulo", "Controle de Presença da Turma " + Session["Turma"].ToString() + " no Período de " + Session["DataInicial"].ToString() + " à " + Session["DataFinal"].ToString() + "."));
                    break;

                case "83":
                    selectcommand = "SELECT View_Pesquisas_Realizadas.PepCodigo as PepCodigo, View_Pesquisas_Realizadas.PepObservacao as pepObservacao, CA_Pesquisa.PesNome as PesNome, View_Pesquisas_Realizadas.Apr_Nome as Apr_Nome,  View_Pesquisas_Realizadas.PepDataRealizada as PepDataRealizada, View_Pesquisas_Realizadas.PepTutor as PepTutor, View_Pesquisas_Realizadas.ParNomeFantasia as ParNomeFantasia, View_Pesquisas_Realizadas.ParUniDescricao as ParUniDescricao, View_Pesquisas_Realizadas.Apr_InicioAprendizagem as Apr_InicioAprendizagem, View_Pesquisas_Realizadas.Apr_PrevFimAprendizagem as Apr_PrevFimAprendizagem, View_Resposta_Pesquisa.MesExtenso as MesExtenso, View_Resposta_Pesquisa.PepAno as PepAno, View_Resposta_Pesquisa.QueTexto as QueTexto, View_Resposta_Pesquisa.OpcTexto as OpcTexto, View_Resposta_Pesquisa.OpcNota as OpcNota, View_Pesquisas_Realizadas.NotaFinal as NotaFinal FROM    View_Pesquisas_Realizadas INNER JOIN View_Resposta_Pesquisa ON View_Pesquisas_Realizadas.PepCodigo = View_Resposta_Pesquisa.PepCodigo  INNER JOIN CA_Pesquisa ON View_Resposta_Pesquisa.PepPesquisaCodigo = CA_Pesquisa.PesCodigo  WHERE  View_Pesquisas_Realizadas.PepCodigo = " + Convert.ToInt32(Session["pepCodigo"].ToString()) + "";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAvaliacaoEnsinoAprendizagem.rdlc";


                    break;

                case "84":
                    selectcommand = "select * from View_Contrato_Aprediz where  Apr_Codigo = " + Session["CodAprendiz"] + "";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCertificado.rdlc";


                    break;

                case "85":
                    selectcommand = "SELECT CA_Aprendiz.Apr_Codigo as codigo, CA_Aprendiz.Apr_Nome as nome, CA_Aprendiz.apr_situacao as situacao, CA_Turmas.TurCurso as turmaCurso, CA_Parceiros.ParNomeFantasia as nomeFantasia, Sum (CA_NotasAprendiz.NdiNota)  AS SomaDeNota, Count (CA_NotasAprendiz.NdiAprendiz) * 5 AS ContaDisciplina, Apr_AreaAtuacao as areaAtuacao FROM     CA_Aprendiz INNER JOIN CA_AlocacaoAprendiz ON CA_Aprendiz.Apr_Codigo = CA_AlocacaoAprendiz.ALAAprendiz  INNER JOIN CA_Turmas ON CA_AlocacaoAprendiz.ALATurma = CA_Turmas.TurCodigo  INNER JOIN CA_ParceirosUnidade ON CA_AlocacaoAprendiz.ALAUnidadeParceiro = CA_ParceirosUnidade.ParUniCodigo  INNER JOIN CA_Parceiros ON CA_ParceirosUnidade.ParUniCodigoParceiro = CA_Parceiros.ParCodigo  INNER JOIN CA_NotasAprendiz ON CA_Aprendiz.Apr_Codigo = CA_NotasAprendiz.NdiAprendiz GROUP BY CA_Aprendiz.Apr_Codigo, CA_Aprendiz.Apr_Nome, CA_Aprendiz.apr_situacao, CA_Turmas.TurCurso, CA_Parceiros.ParNomeFantasia, Apr_AreaAtuacao HAVING CA_Aprendiz.Apr_Codigo= " + Session["CodAprendiz"] + " AND CA_Turmas.TurCurso='002' and Apr_Situacao = 5";
                    ReportViewer1.LocalReport.ReportPath = "reports/" + Session["NomeRelatorio"].ToString() + ".rdlc";

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("FrequenciaCoteudoTeorico", Session["FrequenciaCoteudoTeorico"].ToString() + " %"));

                    break;

                case "86": // Integral
                    selectcommand = "select * from View_Contrato_Aprediz where Apr_Codigo = " + Session["CodAprendiz"] + " and (turCurso = '002' or turCurso = '004')";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPContratoIntegral.rdlc";

                    break;
                case "87": //Parcial
                    selectcommand = "select * from View_Contrato_Aprediz where Apr_Codigo = " + Session["CodAprendiz"] + " and (turCurso = '002' or turCurso = '004')";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPContratoParcial.rdlc";
                    break;


                //Session["TurmaRelatorio"] = DD_Turma.SelectedValue;
                //Session["DataInicialRelatorio"] = Convert.ToDateTime(tb_data_inicial.Text);
                //Session["DataFinalRelatorio"] = Convert.ToDateTime(tb_data_final.Text);

                case "88":
                    selectcommand = @"select Apr_Nome,AdiPresenca,Apr_Codigo,AdiTurma, Parceiro,
                                    [1] as data_1,  [2] as data_2, 
                                    [3] as data_3, [4] as data_4,
                                    [5] as data_5
                                    from
                                    (
                                      select Apr_Nome, AdiPresenca, AdiDataAula, Apr_Codigo,AdiTurma, CASE ParDescricao WHEN ParUniDescricao THEN ParDescricao 
                                    ELSE (ParUniDescricao) END AS Parceiro ,
                                        row_number() over(partition by Apr_Nome order by Apr_Nome) rnk
                                      from View_ControlePresenca Where AdiTurma = " + Session["TurmaRelatorio"] + " and AdiDataAula between '" + Session["DataInicialRelatorio"] + "' and '" + Session["DataFinalRelatorio"] + "') d pivot (max(AdiDataAula) for rnk in ([1], [2], [3], [4], [5])) piv;";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaPresencaMensal.rdlc";

                    var sql = "select u.UniNome as Unidade from CA_Unidades u join CA_Turmas t on u.UniCodigo = t.TurUnidade where t.TurCodigo = " + Session["TurmaRelatorio"] + "";
                    var con = new Conexao();
                    var result = con.Consultar(sql);
                    var unidade = "";
                    while (result.Read())
                    {

                        unidade = result["Unidade"].ToString().Equals("") ? " " : result["Unidade"].ToString();
                    }

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DiaSemana", Session["DiaSemana"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("unidade", unidade));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("Turma", Session["Turma"].ToString()));

                    break;

                case "89":



                    //if (!(Session["NomeAprendizDigitado"] == null))
                    //{
                    //    validacao += " and Apr_Nome like('%" + Session["NomeAprendizDigitado"] + "%') ";
                    //}

                    //if (!(Session["CodDigitado"] == null))
                    //{
                    //    validacao += " and Apr_Codigo = " + Session["CodDigitado"] + "";
                    //}

                    //if (!(Session["CodSituacao"] == null))
                    //{
                    //    validacao += " and Apr_Situacao = " + Session["CodSituacao"] + " ";
                    //}

                    //selectcommand = "Select * from View_Ficha_Aprendiz where  1 = 1 " + validacao + " order by Apr_Nome";
                    //ReportViewer1.LocalReport.ReportPath = "reports/RPListaAprendizes.rdlc";
                    break;

                case "90":
                    selectcommand = @"Select A.ADPDataAula as DataAula, 
                                        T.TurObservacoes as Turma, 
                                        E.EducNome as Professor, 
                                        D.DisDescricao as Disciplina, 
                                        A.ADPOrdemAula as Sequencia 
                                        from CA_AulasDisciplinasTurmaProf A
                                        join CA_Turmas T on A.ADPTurma = T.TurCodigo
                                        join CA_Educadores E on A.ADPprofessor = E.EducCodigo
                                        join CA_Disciplinas D on A.ADPDisciplina = D.DisCodigo
                                        where T.TurCodigo = " + Session["TurmaEscolhida"] + " " +
                                        "and A.ADPDataAula > '" + (DateTime)Session["DataInicio"] + "' " +
                                        "order by A.ADPDataAula, A.ADPOrdemAula";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCronogramaTurmaDisciplina.rdlc";
                    break;

                case "91":
                    selectcommand = @"SELECT CA_Ocorrencias.OcoDescricao as descricao, Count(CA_OcorrenciasAprendiz.OcaCodAprendiz) AS QTD FROM CA_OcorrenciasAprendiz INNER JOIN CA_Ocorrencias ON CA_OcorrenciasAprendiz.OcaCodOcorrencia = CA_Ocorrencias.OcoCodigo WHERE (((CA_OcorrenciasAprendiz.OcaDataOcorrencia) Between '" + DateTime.Parse(Session["DataInicio"].ToString()) + "' And '" + DateTime.Parse(Session["DataTermino"].ToString()) + "')) GROUP BY CA_Ocorrencias.OcoDescricao ORDER BY CA_Ocorrencias.OcoDescricao;"; ;
                    ReportViewer1.LocalReport.ReportPath = "reports/RPListaOcorrenciaTotaisPeriodo.rdlc";

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataInicio", Session["DataInicio"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataTermino", Session["DataTermino"].ToString()));
                    break;


                case "92":
                    selectcommand = "SELECT  CA_SituacaoAprendiz.StaDescricao as descricao,  CA_SituacaoAprendiz.StaAbreviatura as abreviatura,  CA_Aprendiz.Apr_Sexo as sexo, Count( CA_Aprendiz.Apr_Codigo) AS QTD FROM  CA_Aprendiz INNER JOIN  CA_SituacaoAprendiz ON  CA_Aprendiz.Apr_Situacao =  CA_SituacaoAprendiz.StaCodigo GROUP BY  CA_SituacaoAprendiz.StaDescricao,  CA_SituacaoAprendiz.StaAbreviatura,  CA_Aprendiz.Apr_Sexo ORDER BY  CA_SituacaoAprendiz.StaDescricao,  CA_Aprendiz.Apr_Sexo;";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlunosPorSituacaoSexo.rdlc";
                    break;

                case "93":
                    selectcommand = "SELECT CA_SituacaoAprendiz.StaDescricao as descricao, CA_SituacaoAprendiz.StaAbreviatura as abreviatura ,DATEDIFF(yy,CA_Aprendiz.Apr_DataDeNascimento, GETDATE()) AS idade , Count(CA_Aprendiz.Apr_Codigo) AS QTD FROM CA_Aprendiz INNER JOIN CA_SituacaoAprendiz ON CA_Aprendiz.Apr_Situacao = CA_SituacaoAprendiz.StaCodigo GROUP BY CA_SituacaoAprendiz.StaDescricao, CA_SituacaoAprendiz.StaAbreviatura,DATEDIFF(yy,CA_Aprendiz.Apr_DataDeNascimento, GETDATE()) ORDER BY CA_SituacaoAprendiz.StaDescricao,DATEDIFF(yy,CA_Aprendiz.Apr_DataDeNascimento, GETDATE())";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAlunosPorSituacaoIdade.rdlc";
                    break;

                case "94":
                    selectcommand = "SELECT View_Pesquisas_Realizadas.PepCodigo as PepCodigo, View_Pesquisas_Realizadas.PepObservacao as pepObservacao, CA_Pesquisa.PesNome as PesNome, View_Pesquisas_Realizadas.Apr_Nome as Apr_Nome, CA_Turmas.TurCurso as TurCurso, View_Pesquisas_Realizadas.PepDataRealizada as PepDataRealizada, View_Pesquisas_Realizadas.PepTutor as PepTutor, CA_Turmas.TurNome as TurNome, View_Pesquisas_Realizadas.ParNomeFantasia as ParNomeFantasia, View_Pesquisas_Realizadas.ParUniDescricao as ParUniDescricao, View_Pesquisas_Realizadas.Apr_InicioAprendizagem as Apr_InicioAprendizagem, View_Pesquisas_Realizadas.Apr_PrevFimAprendizagem as Apr_PrevFimAprendizagem, View_Resposta_Pesquisa.MesExtenso as MesExtenso, View_Resposta_Pesquisa.PepAno as PepAno, View_Resposta_Pesquisa.QueTexto as QueTexto, View_Resposta_Pesquisa.OpcTexto as OpcTexto, View_Resposta_Pesquisa.OpcNota as OpcNota, View_Pesquisas_Realizadas.NotaFinal as NotaFinal FROM View_Pesquisas_Realizadas INNER JOIN View_Resposta_Pesquisa ON View_Pesquisas_Realizadas.PepCodigo = View_Resposta_Pesquisa.PepCodigo  INNER JOIN CA_Pesquisa ON View_Resposta_Pesquisa.PepPesquisaCodigo = CA_Pesquisa.PesCodigo  INNER JOIN CA_AlocacaoAprendiz ON View_Pesquisas_Realizadas.Apr_Codigo = CA_AlocacaoAprendiz.ALAAprendiz  INNER JOIN CA_Turmas ON CA_AlocacaoAprendiz.ALATurma = CA_Turmas.TurCodigo  WHERE  View_Pesquisas_Realizadas.PepCodigo = " + Convert.ToInt32(Session["pepCodigo"].ToString()) + "";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPAvaliacaoEnsinoAprendizagemAlocacao.rdlc";

                    break;

                case "95":

                    DateTime dataInicial = (DateTime)Session["dataInicial"];
                    DateTime dataFinal = (DateTime)Session["dataFinal"];

                    string dataInicialImpressao = Session["dataInicialImpressao"].ToString();
                    string dataFinalImpressao = Session["dataFinalImpressao"].ToString();

                    selectcommand = @"SELECT View_CA_Contagem_Faltas_Aprendiz.ParDescricao, View_CA_Contagem_Faltas_Aprendiz.ParUniDescricao, View_CA_Contagem_Faltas_Aprendiz.Apr_Codigo, View_CA_Contagem_Faltas_Aprendiz.Apr_Nome, SUM(CASE WHEN [Faltou]= 6 THEN 1 ELSE 0 END) AS FaltaDias, Sum(CASE WHEN [Faltou]<>6 THEN [Faltou] ELSE 0 END) AS FaltaHoras, View_CA_Contagem_Faltas_Aprendiz.ParUniCNPJ, View_CA_Contagem_Faltas_Aprendiz.Apr_NumSistExterno FROM View_CA_Contagem_Faltas_Aprendiz WHERE (((View_CA_Contagem_Faltas_Aprendiz.AdiDataAula) Between '" + dataInicial + "' And '" + dataFinal + "') AND ((View_CA_Contagem_Faltas_Aprendiz.Faltou)<>0)) GROUP BY View_CA_Contagem_Faltas_Aprendiz.ParDescricao, View_CA_Contagem_Faltas_Aprendiz.ParUniDescricao, View_CA_Contagem_Faltas_Aprendiz.Apr_Codigo, View_CA_Contagem_Faltas_Aprendiz.Apr_Nome, View_CA_Contagem_Faltas_Aprendiz.ParUniCNPJ, View_CA_Contagem_Faltas_Aprendiz.Apr_NumSistExterno ORDER BY View_CA_Contagem_Faltas_Aprendiz.ParDescricao, View_CA_Contagem_Faltas_Aprendiz.ParUniDescricao, View_CA_Contagem_Faltas_Aprendiz.Apr_Nome;";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPContagemFaltasPeriodo.rdlc";

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataInicial", dataInicialImpressao));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("DataFinal", dataFinalImpressao));

                    break;


                case "96":
                    if (Session["DataRel"] != null)
                    {
                        //  selectcommand = "Select Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, ParUniDescricao, TurNome, UniNome, AdiDataAula  from View_CA_AulasDisciplinasAprendiz where AdiDataAula = '" + Convert.ToDateTime(Session["DataRel"].ToString()) + "' and AdiTurma =  " + Session["TurCodigo"].ToString() + "and Apr_Situacao = 6 group by Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, ParUniDescricao, TurNome, UniNome, AdiDataAula order by Apr_Nome ";

                        selectcommand = "Select * from View_CA_AulasCapacitacao where AcpDataAula  = '" + Convert.ToDateTime(Session["DataRel"].ToString()) + "' and turCodigo = " + Session["TurCodigo"].ToString() + " order by Apr_Nome";


                        ReportViewer1.LocalReport.ReportPath = "reports/RPListaAlunosPresencaCapacitacao.rdlc";
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Data", Session["DataRel"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Turma", Session["TurCodigo"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("DiaSemana", Session["DiaSemana"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Polo", Session["Polo"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Endereco", Session["Endereco"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Cep", Session["Cep"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Telefone", Session["Telefone"].ToString()));

                    }
                    break;

                case "97":
                    if (Session["DataRel"] != null)
                    {
                        //  selectcommand = "Select Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, ParUniDescricao, TurNome, UniNome, AdiDataAula  from View_CA_AulasDisciplinasAprendiz where AdiDataAula = '" + Convert.ToDateTime(Session["DataRel"].ToString()) + "' and AdiTurma =  " + Session["TurCodigo"].ToString() + "and Apr_Situacao = 6 group by Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, ParUniDescricao, TurNome, UniNome, AdiDataAula order by Apr_Nome ";

                        // selectcommand = "Select * from View_CA_AulasCapacitacao where AcpDataAula  = '" + Convert.ToDateTime(Session["DataRel"].ToString()) + "' and turCodigo = " + Session["TurCodigo"].ToString() + " order by Apr_Nome";
                        selectcommand = "Select Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, ParUniDescricao, TurNome, UniNome, AdiDataAula  from View_CA_AulasDisciplinasAprendiz where AdiDataAula = '" + Convert.ToDateTime(Session["DataRel"].ToString()) + "' and AdiTurma =  " + Session["TurCodigo"].ToString() + "and Apr_Situacao = 6 group by Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, ParUniDescricao, TurNome, UniNome, AdiDataAula order by Apr_Nome ";

                        ReportViewer1.LocalReport.ReportPath = "reports/RPListaAlunosPresencaIntrodutorio.rdlc";
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Data", Session["DataRel"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Turma", Session["TurCodigo"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("DiaSemana", Session["DiaSemana"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Polo", Session["Polo"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Endereco", Session["Endereco"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Cep", Session["Cep"].ToString()));
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("Telefone", Session["Telefone"].ToString()));

                    }

                    break;

                case "98":
                    selectcommand = "Select TurNome, Apr_Codigo, Apr_Nome, uniNome, AcpPresenca, AcpDataAula  from View_CA_AulasCapacitacao where AcpDataAula = '" + Convert.ToDateTime(Session["Data"].ToString()) + "'";
                    if (!string.IsNullOrEmpty(Session["Turma"].ToString()))
                        selectcommand += " AND turCodigo = '" + Session["Turma"] + "' ";
                    selectcommand += "  ORDER BY TurNome, Apr_Nome";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPControlePresencaCapacitacao.rdlc";
                    break;


                case "99":
                    Session["args"] = Session["PRMT_Tipo"] + "_" + Session["PRMT_Nome"] + "_" + Session["PRMT_MesRef"] + "_" + Session["PRMT_AnoRef"];

                    orderBy = "order by ALApagto, Apr_Nome";
                    parceiro = Session["Parceiro"];
                    turma = Session["PRMT_Turma"];
                    aux = "";

                    tipo = Session["PRMT_Tipo"].ToString();
                    titulo = "Geral";
                    switch (tipo)
                    {
                        case "tab-0":
                            selectcommand = "select * from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and TurCurso = '002' and Apr_Nome " +
                                  "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' ";

                            aux = selectcommand;

                            //if (Session["PRMT_Nome"] != null && !Session["PRMT_Nome"].Equals(string.Empty))
                            //{

                            //}
                            //else
                            //{
                            if (parceiro != null && (!parceiro.Equals("")))
                            {
                                selectcommand += " and ParCodigo = " + parceiro + "";
                            }

                            if (turma != null && (!turma.Equals("Selecione..")))
                            {
                               // selectcommand = aux;
                                selectcommand += " and TurCodigo = " + turma + "";
                            }

                            // }





                            titulo = "Geral";
                            break;
                        case "tab-1":
                            selectcommand = "select * from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and Apr_Situacao = 6 and Apr_Nome " +
                                  "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND DATEPART(m, Apr_InicioAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                            " DATEPART(yyyy, Apr_InicioAprendizagem) = " + Session["PRMT_AnoRef"] + " ";

                            if (Session["PRMT_Nome"] != null && !Session["PRMT_Nome"].Equals(string.Empty))
                            {

                            }
                            else
                            {
                                if (parceiro != null && (!parceiro.Equals("")))
                                {
                                    selectcommand += " and ParCodigo = " + parceiro + "";
                                }
                                else
                                {
                                    if (turma != null && (!turma.Equals("Selecione..")))
                                    {
                                        selectcommand = aux;
                                        selectcommand += " and TurCodigo = " + turma + "";
                                    }
                                }
                            }

                            titulo = "Por data de Início";
                            break;
                        case "tab-2":
                            selectcommand = "select * from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and Apr_Situacao = 6 and Apr_Nome " +
                                "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%'  AND  DATEPART(m, Apr_FimAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                          " DATEPART(yyyy, Apr_FimAprendizagem) = " + Session["PRMT_AnoRef"] + " ";

                            if (Session["PRMT_Nome"] != null && !Session["PRMT_Nome"].Equals(string.Empty))
                            {

                            }
                            else
                            {
                                if (parceiro != null && (!parceiro.Equals("")))
                                {
                                    selectcommand += " and ParCodigo = " + parceiro + "";
                                }
                                else
                                {
                                    if (turma != null && (!turma.Equals("Selecione..")))
                                    {
                                        selectcommand = aux;
                                        selectcommand += " and TurCodigo = " + turma + "";
                                    }
                                }
                            }
                            titulo = "Por data de Término";
                            break;
                        case "tab-3":
                            selectcommand = "select * from dbo.View_AlocacoesAlunos where  ALAStatus = 'A' and Apr_Situacao = 6 and Apr_Nome " +
                                "like '%'  + '" + Session["PRMT_Nome"] + "' +  '%' AND DATEPART(m, Apr_PrevFimAprendizagem) = " + Session["PRMT_MesRef"] + " AND " +
                                          " DATEPART(yyyy, Apr_PrevFimAprendizagem) = " + Session["PRMT_AnoRef"] + " ";
                            if (Session["PRMT_Nome"] != null && !Session["PRMT_Nome"].Equals(string.Empty))
                            {

                            }
                            else
                            {
                                if (parceiro != null && (!parceiro.Equals("")))
                                {
                                    selectcommand += " and ParCodigo = " + parceiro + "";
                                }
                                else
                                {
                                    if (turma != null && (!turma.Equals("Selecione..")))
                                    {
                                        selectcommand = aux;
                                        selectcommand += " and TurCodigo = " + turma + "";
                                    }
                                }
                            }
                            titulo = "Por Previsão de Término";


                            break;


                    }
                    selectcommand += orderBy;
                    ReportViewer1.LocalReport.ReportPath = "reports/RPFolhaPonto.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("mesExtenso", Session["mesExtenso"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("ano", Session["ano"].ToString()));





                  //  List<DateTime> ListaDatas = (List<DateTime>)Session["ListaDatas"];

                    // ReportDataSource rds = new ReportDataSource { DataSourceId = "data", Name = "data", Value = datas };

                    //var reportViewer = new ReportViewer();
                    //reportDatas = new ReportDataSource("ListaDatas", ListaDatas);
                    //reportDatas.Value = ListaDatas;
                    break;

                case "101":
                    //selectcommand = "Select A.Apr_Nome, A.Apr_CarteiraDeIdentidade, O.OcoDescricao, OA.OcaDataOcorrencia, P.ParUniCidade, P.ParUniDescricao, U.UniCidade from CA_Ocorrencias O join CA_OcorrenciasAprendiz OA on OA.OcaCodOcorrencia = O.OcoCodigo Join CA_Aprendiz A on OA.OcaCodAprendiz = A.Apr_Codigo join CA_ParceirosUnidade P on P.ParUniCodigo = A.Apr_Unidade join CA_Unidades U on U.UniCodigo = A.Apr_Unidade where ocaordem = " + Session["prmt_ordem_ocorrencia"] + "";

                    selectcommand = "select * from View_OcorrenciaAprendiz where OcaOrdem = " + Session["prmt_ordem_ocorrencia"].ToString() + "";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPOcorrenciaDisciplinar.rdlc";
                    break;


                case "102":
                    selectcommand = "Select * from CA_Parceiros where parCodigo = " + Session["CodParceiroImpressao"] + "";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPConvenio.rdlc";
                    break;

                case "103":
                    selectcommand = " select * from View_DadosVagas where ReqId = " + Session["codRequerimento"] + ";";
                    reportcommand = "Select A.Apr_Codigo, A.Apr_Nome, A.Apr_Celular, A.Apr_Email, S.Ste_Descricao from CA_Encaminhamentos E join CA_RequisicoesVagas R on E.Enc_Requisicao = R.ReqId  join CA_StatusEncaminhamento S on E.Enc_Status = S.Ste_Codigo join CA_Aprendiz A on E.Enc_CodigoAprendiz = A.Apr_Codigo  where R.ReqId = '" + Session["codRequerimento"] + "' ";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPDadosDaVaga.rdlc";


                    break;

                case "104":
                    selectcommand = " select * from View_CartaEncaminhamento where Enc_Sequencia = '" + Session["CodFichaEncaminhamento"] + "'";
                    ReportViewer1.LocalReport.ReportPath = "reports/RPCartaEncaminhamento.rdlc";


                    break;
            }

            ReportViewer1.LocalReport.SetParameters(new ReportParameter("logo", caminhoLogo));

            var dSaux = new SqlDataSource { ID = "ODSobra", ConnectionString = GetConfig.Config(), SelectCommand = selectcommand };
            var dSescola = new SqlDataSource { ID = "ODSescola", ConnectionString = GetConfig.Config(), SelectCommand = "SELECT * from CA_Unidades where UniCodigo = " + GetConfig.Escola() };
            var dStotal = new SqlDataSource { ID = "ODStotalportipo", ConnectionString = GetConfig.Config(), SelectCommand = reportcommand };
            var dScommand = new SqlDataSource { ID = "ODStota", ConnectionString = GetConfig.Config(), SelectCommand = command };

            if (id.Equals("53") || id.Equals("66") /*|| id.Equals("18") */|| id.Equals("22") || id.Equals("35") || id.Equals("36"))
            {
                dSescola = new SqlDataSource { ID = "ODSescola", ConnectionString = GetConfig.Config(), SelectCommand = "  select * from CA_Unidades U   join CA_Turmas T on U.UniCodigo = T.TurUnidade   where T.TurCodigo = " + Session["PRMT_Turma"] + "" };
            }


            var datasource = new ReportDataSource("DataSet1", dSaux);
            var data = new ReportDataSource("DataSet2", dSescola);
            var source = new ReportDataSource("DataSet3", dStotal);
            var com = new ReportDataSource("DataSet4", dScommand);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DataSources.Add(data);

            if (id.Equals("99"))
                ReportViewer1.LocalReport.DataSources.Add(reportDatas);


            if (id.Equals("12") || id.Equals("18") || id.Equals("92") || id.Equals("76") || id.Equals("103"))
                ReportViewer1.LocalReport.DataSources.Add(source);


            if (id.Equals("86") || id.Equals("87"))
            {
                ReportViewer1.LocalReport.DataSources.Add(data);
            }



            if (id.Equals("23"))
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(data);
                ReportViewer1.LocalReport.DataSources.Add(source);
                ReportViewer1.LocalReport.DataSources.Add(com);
            }

            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.LocalReport.EnableExternalImages = true;
        }


        private void SuppressExportButton(ReportViewer rv, string optionToSuppress)
        {
            var reList = rv.LocalReport.ListRenderingExtensions();
            foreach (var re in reList)
            {
                if (re.Name.Trim().ToUpper() == optionToSuppress.Trim().ToUpper()) // Hide the option
                {
                    var fieldInfo = re.GetType().GetField("m_isVisible", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (fieldInfo != null)
                        fieldInfo.SetValue(re, false);
                }
            }
        }


        protected void IMB_Print_Click(object sender, ImageClickEventArgs e)
        {
            var service = new Service1();
            var id = Session["id"].ToString();
            var x = Session["args"];
            var args = string.IsNullOrEmpty((string)x) ? "" : x.ToString();

            var path = Server.MapPath(@"/reports");
            var local = ReportViewer1.LocalReport.ReportPath.Split('/')[1];
            var report = new FileStream(path + @"/" + local, FileMode.Open);
            var memoryStream = new MemoryStream();
            report.CopyTo(memoryStream);
            byte[] bytearay = memoryStream.ToArray();


            byte[] bytes = service.GeraPDF("2." + id, args, bytearay);
            report.Close();

            string filePath = Server.MapPath("/files");
            File.Delete(filePath + @"/output.pdf");

            var fs = new FileStream(filePath + @"/output.pdf", FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            string fileName = filePath + @"/output.pdf";
            Funcoes.Download(fileName, ReportViewer1.LocalReport.ReportPath.Split('/').Last().Replace(".rdlc", ".pdf"));
        }
    }
}