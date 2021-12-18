using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil
{
    public partial class MPusers : MasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActiveTab.Value = "0";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            var sql = "UPDATE CA_AlocacaoAprendiz SET CA_AlocacaoAprendiz.ALAStatus = 'I' WHERE (((CA_AlocacaoAprendiz.ALADataPrevTermino)<getDate()) AND ((CA_AlocacaoAprendiz.ALAStatus)='A'))";
            var con = new Conexao();
            con.Alterar(sql);

            var sql2 = "delete  CA_AulasDisciplinasAprendiz FROM CA_Aprendiz INNER JOIN CA_AulasDisciplinasAprendiz ON CA_Aprendiz.Apr_Codigo = CA_AulasDisciplinasAprendiz.AdiCodAprendiz WHERE (((CA_AulasDisciplinasAprendiz.AdiDataAula)>[apr_fimAprendizagem])) ";
            con = new Conexao();
            con.Alterar(sql2);

            var sql3 = "UPDATE CA_Aprendiz SET CA_Aprendiz.Apr_Situacao = 5 WHERE (((CA_Aprendiz.Apr_PrevFimAprendizagem)<getDate()) AND ((CA_Aprendiz.Apr_Situacao)=6))";
            con = new Conexao();
            con.Alterar(sql3);

            if (Session["codigo"] == null || Session["codigo"].Equals(string.Empty))
            {
                Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
            }

            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
            var escola = from i in bd.CA_Unidades
                         where i.UniCodigo.Equals(GetConfig.Escola())
                         select new { i.UniNome,i.UniEndereco,i.UniEstado,i.UniCidade,i.UniNumeroEndereco,
                             i.UniComplemento,i.UniTelefone,i.UniEnderecoWeb,i.UniBairro };

            var unidade = escola.First();
            LBnomeEscola.Text = unidade.UniNome;
            LBenderecoEscola.Text = unidade.UniEndereco + ", nº " + unidade.UniNumeroEndereco + " - " + unidade.UniBairro + " - " + unidade.UniCidade + " - " + unidade.UniEstado + 
             "           Telefone: " + " (" + unidade.UniTelefone.Substring(0, 2) + ") " + unidade.UniTelefone.Substring(2, 4) + "-" + unidade.UniTelefone.Substring(6);
            LNKendWeb.Attributes.Add("href", unidade.UniEnderecoWeb);
            LBEndWeb.Text = unidade.UniEnderecoWeb;

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            var tipoacesso = "";
            var lb = (LinkButton)sender;
            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());

            if (!Session["tipoUsuarioLogado"].ToString().Equals("A"))
            {
                var dados = from i in bd.CA_AutorizacaoUsuarios
                            join m in bd.CA_Usuarios on i.AutFUsuario equals m.UsuTipo
                            where m.UsuCodigo.Equals(Session["codigo"].ToString()) && i.AutFFuncao.Equals(lb.ID)
                            select new { i.AutFTipoAut, i.AutFFuncao };
                if (dados.Count().Equals(0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                        "alert('ERRO - Função não habilitada para seu perfil de usuário.')", true);
                    return;
                }
                tipoacesso = Criptografia.Encrypt(dados.First().AutFTipoAut, GetConfig.Key());
            }
            else
            {
                tipoacesso = Criptografia.Encrypt("liberado", GetConfig.Key());
            }

            //var tipoacesso = Criptografia.Encrypt(dados.First().AutFTipoAut, GetConfig.Key());
            Session["funcao"] = lb.ID;
            switch (lb.ID)
            {
                case "LK_Cadastro_Alunos": Response.Redirect("ControleAlunos.aspx?acs=" + tipoacesso); break;
                //case "LK_AreaEnsino": Response.Redirect("AreaEnsino.aspx?acs=" + tipoacesso); break;
                case "LK_CadastroDocumentos": Response.Redirect("CadastroDocumentos.aspx?acs=" + tipoacesso); break;
                case "LK_ProtocolosAbertos": Response.Redirect("GestaoDocumentos.aspx?acs=" + tipoacesso); break;
                case "LK_cadastro_GrauParentesco": Response.Redirect("CadastroGrauParentesco.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Escolas": Response.Redirect("CadastroEscolas.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Curso": Response.Redirect("CadastroCurso.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Educadores": Response.Redirect("ControleEducadores.aspx?acs=" + tipoacesso); break;
                case "LK_HistoricoProtocolo": Response.Redirect("HistoricoRequerimentos.aspx?acs=" + tipoacesso); break;
                case "LK_CadastroUsuarios": Response.Redirect("MultiviewUsuarios.aspx?acs=" + tipoacesso); break;
                case "LK_Estatistica_Geral_controle": Response.Redirect("RelatorioSinteticoControle.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Unidades": Response.Redirect("ParametrosEscolares.aspx?acs=" + tipoacesso); break;
                case "LK_Estatistica_Geral": Response.Redirect("RelatorioSintetico.aspx?acs=" + tipoacesso); break;
                case "LK_Estatistica_Unidade": Response.Redirect("Estatisticas.aspx?acs=" + tipoacesso); break;
                case "LK_Ocorrencias": Response.Redirect("CadastroOcorrencias.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_SituacaoAprendiz": Response.Redirect("SituacaoAprendiz.aspx?acs=" + tipoacesso); break;
                case "lk_Cadastro_Status": Response.Redirect("StatusRequisicao.aspx?acs=" + tipoacesso); break;
                case "LK_Controle_Acesso": Response.Redirect("ControleAcesso.aspx?acs=" + tipoacesso); break;
                case "LK_Perfil_Usuario": Response.Redirect("CadastroPerfilUsuario.aspx?acs=" + tipoacesso); break;
                case "LK_relatorio_analitico_controle": Response.Redirect("RelatorioAnaliticoControle.aspx?acs=" + tipoacesso); break;
                case "LK_relatorio_analitico": Response.Redirect("RelatorioAnalitico.aspx?acs=" + tipoacesso); break;
                case "LK_BKP_fotos": Response.Redirect("GestaoFotos.aspx?acs=" + tipoacesso); break;
                case "LK_Disciplinas": Response.Redirect("CadastroDisciplina.aspx?acs=" + tipoacesso); break;
                case "LK_cadastro_Escolaridade": Response.Redirect("CadastroGrauEscolaridade.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Turmas": Response.Redirect("CadastroTurma.aspx?acs=" + tipoacesso); break;
                case "LK_Plano_curricular": Response.Redirect("PlanoCurricular.aspx?acs=" + tipoacesso); break;
                case "LK_Data_Encontro": Response.Redirect("DataEncontro.aspx?acs=" + tipoacesso); break;
                case "LK_Ramos_Atividade": Response.Redirect("CadastroRamoAtividade.aspx?acs=" + tipoacesso); break;
                case "LK_Contagem_Faltas_Periodo": Response.Redirect("ContagemFaltasPeriodo.aspx?acs=" + tipoacesso); break;

                case "LK_CadastroVagas": Response.Redirect("CadastroVagas.aspx?acs=" + tipoacesso); break;
                case "LK_PesquisaVagas": Response.Redirect("PesquisaVagas.aspx?acs=" + tipoacesso); break;

                case "LK_Total_Por_Parceiro_Faltas": Response.Redirect("ControlePresencaFaltasPeriodo.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Parceiros": Response.Redirect("CadastroParceiros.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Profissoes": Response.Redirect("CadastroProfissao.aspx?acs=" + tipoacesso); break;
                case "LK_unidades_parceiros": Response.Redirect("CadastroParceirosUnidades.aspx?acs=" + tipoacesso); break;
                case "LK_controle_alunos": Response.Redirect("ControleAlunos.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Ciclo": Response.Redirect("CadastroCiclo.aspx?acs=" + tipoacesso); break;
                case "LK_Professor_disciplina": Response.Redirect("ListaPresenca.aspx?acs=" + tipoacesso); break;
                case "LK_ListaPresencaCapacitacao": Response.Redirect("ListaPresencaCapacitacao.aspx?acs=" + tipoacesso); break;
                case "LK_ListaPresencaIntrodutorio": Response.Redirect("ListaPresencaIntrodutorio.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Conceito": Response.Redirect("CadastroConceito.aspx?acs=" + tipoacesso); break;
                case "LK_Area_Atuacao": Response.Redirect("CadastroAreaAtuacao.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_feriado": Response.Redirect("CadastroFeriado.aspx?acs=" + tipoacesso); break;
                case "LK_Modulos_Aprendizagem": Response.Redirect("CadastroModulo.aspx?acs=" + tipoacesso); break;
                case "LK_Motivo_Desligamento": Response.Redirect("CadastroMotDesligamento.aspx?acs=" + tipoacesso); break;
                case "LK_Orientadores": Response.Redirect("CadastroOrientadores.aspx?acs=" + tipoacesso); break;
                case "LK_Fechamento_Mensal": Response.Redirect("FechamentoMensal.aspx?acs=" + tipoacesso); break;
                case "LK_Alocacao_aprendiz": Response.Redirect("AlocacaoAlunos.aspx?acs=" + tipoacesso); break;
                case "LK_Lanca_ocorrencia": Response.Redirect("LancamentoOcorrencia.aspx?acs=" + tipoacesso); break;
                case "LK_Aniversariantes": Response.Redirect("AniversariantesPeriodo.aspx?acs=" + tipoacesso); break;
                case "LK_Lista_Ativos": Response.Redirect("ListaAtivos.aspx?acs=" + tipoacesso); break;
                case "LK_Gera_Cronograma": Response.Redirect("Cronograma.aspx?acs=" + tipoacesso); break;
                case "LK_Gera_CronogramaTurmaDisciplina": Response.Redirect("CronogramaTurmaDisciplina.aspx?acs=" + tipoacesso); break;
                case "LK_Lista_Cursantes": Response.Redirect("ListaCursantes.aspx?acs=" + tipoacesso); break;
                case "LK_AlunosSituacao_Estatistica": Response.Redirect("EstatisticaAlunos.aspx?acs=" + tipoacesso); break;
                case "LK_AlunosGeral_Estatistica": Response.Redirect("EstatisticaAprendizes.aspx?acs=" + tipoacesso); break;
                case "LK_AlunosEmpresa_Estatistica": Response.Redirect("AprendizesParceiro.aspx?acs=" + tipoacesso); break;
                case "LK_Notas_Faltas": Response.Redirect("LancamentoFaltas.aspx?acs=" + tipoacesso); break;
                case "LK_Notas_Faltas_Capacitacao": Response.Redirect("LancamentoFaltasCapacitacao.aspx?acs=" + tipoacesso); break;
                case "LK_Estatistica_pesquisa": Response.Redirect("EstatisticaPesquisa.aspx?acs=" + tipoacesso); break;
                case "Lk_Atribuir_pesquisa": Response.Redirect("ControlePesquisa.aspx?acs=" + tipoacesso); break;
              //  case "LK_Gera_Frequencia": Response.Redirect("ControleFrequencia.aspx?acs=" + tipoacesso); break;
                case "LK_pesquisas_orientador": Response.Redirect("AvaliacaoOrientador.aspx?acs=" + tipoacesso); break;
                case "LK_pesquisas_orientador_Empresa": Response.Redirect("AvaliacaoOrientadorEmpresa.aspx?acs=" + tipoacesso); break;
                case "LK_Realizadas_periodo": Response.Redirect("EstatisticaRealizadas.aspx?acs=" + tipoacesso); break;
                case "LK_CadastroInstituicoesParceiras": Response.Redirect("InstituicoesParceiras.aspx?acs=" + tipoacesso); break;
                case "LK_CadastroStatusEncaminhamento": Response.Redirect("StatusEcaminhamento.aspx?acs=" + tipoacesso); break;
                //case "LK_Destaque_aprendiz": Response.Redirect("Destaques.aspx?acs=" + tipoacesso); break;
               // case "LK_notas_Periodo": Response.Redirect("NotasPeriodo.aspx?acs=" + tipoacesso); break;
                case "LK_Controle_Presenca": Response.Redirect("ControlePresenca.aspx?acs=" + tipoacesso); break;
                case "LK_Controle_Presenca_Capacitacao": Response.Redirect("ControlePresencaCapacitacao.aspx?acs=" + tipoacesso); break;
                case "LK_Controle_Por_Periodo": Response.Redirect("ControlePresencaPorPeriodo.aspx?acs=" + tipoacesso); break;
                case "LK_Controle_Por_Parceiro": Response.Redirect("ControlePresencaPorParceiro.aspx?acs=" + tipoacesso); break;
                case "LK_Total_Por_Turma": Response.Redirect("TotalAulasTurma.aspx?acs=" + tipoacesso); break;
                case "LK_Total_Por_Parceiro": Response.Redirect("TotalAulasParceiro.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Regiao": Response.Redirect("CadastroRegiao.aspx?acs=" + tipoacesso); break;
                case "LK_Cadastro_Bairro": Response.Redirect("CadastroBairro.aspx?acs=" + tipoacesso); break;
               // case "LK_Data_Mace": Response.Redirect("DataMace.aspx?acs=" + tipoacesso); break;
                case "LK_Eventos": Response.Redirect("CadastroEvento.aspx?acs=" + tipoacesso); break;
               // case "LK_eventos_aprendizes": Response.Redirect("EventosAprendizes.aspx?acs=" + tipoacesso); break;
                case "LK_realocacao": Response.Redirect("RealocarAprendizes.aspx?acs=" + tipoacesso); break;
                case "LK_lancamentoNotas": Response.Redirect("LancamentoNotas.aspx?acs=" + tipoacesso); break;
                case "LK_pesquisaCandidatos": Response.Redirect("PesquisaCandidatos.aspx?acs=" + tipoacesso); break;
                case "LK_GestaoDocumentos": Response.Redirect("GestaoDocumentos.aspx?acs=" + tipoacesso); break;

                case "LK_Cliente": Response.Redirect("CadastroClientes.aspx?acs=" + tipoacesso); break;
                case "LK_Contatos": Response.Redirect("CadastroContatos.aspx?acs=" + tipoacesso); break;
                case "LK_ListaContatosAge": Response.Redirect("ListaDeContatosAgendadosPorPeriodo.aspx?acs=" + tipoacesso); break;
                case "LK_RealizadosAge": Response.Redirect("ListaDeContatoRealizadosPorPeriodo.aspx?acs=" + tipoacesso); break;
                case "LK_TipoContato": Response.Redirect("CadastroTiposContato.aspx?acs=" + tipoacesso); break;
                case "LK_FechamentoContato": Response.Redirect("CadastroFechamentoContato.aspx?acs=" + tipoacesso); break;
                case "LK_ContatosAtend": Response.Redirect("ListaDeContatosPorAtendente.aspx?acs=" + tipoacesso); break;
                case "LK_StatusCliente": Response.Redirect("CadastroStatusCliente.aspx?acs=" + tipoacesso); break;
                case "LK_ContatoRetorno": Response.Redirect("ListaDeContatoDataRetorno.aspx?acs=" + tipoacesso); break;

                case "LK_NiverCliente": Response.Redirect("AniversariantesCliente.aspx?acs=" + tipoacesso); break;

                    
                    
                    
            }
        }
    }
}