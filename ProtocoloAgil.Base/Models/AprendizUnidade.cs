using MenorAprendizWeb.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProtocoloAgil.Base.Models
{
    [Table("CA_Aprendiz")]
    public class Aprendiz
    {
        public Aprendiz()
        {
            AprCodEscola = null;
            Apr_Resp_Profissao = null;
            Apr_DataDeNascimento = null;
            Apr_DataEmissão_Ident = null;
            Apr_DataEmissão_CartTrab = null;
            Apr_Escola_HInicio = null;
            Apr_Escola_HTermino = null;
            Apr_DataCadastro = null;
            Apr_BolsaFamilia = null;
            Apr_pensao = null;
            Apr_outros = null;
            Apr_aluguel = null;
            Apr_Escolaridade = null;
            //  Apr_NumSitExterno = null;


        }

        public Aprendiz(CA_Aprendiz aprendiz)
        {
            Apr_Codigo = aprendiz.Apr_Codigo;
            Apr_Nome = aprendiz.Apr_Nome;
            Apr_DataDeNascimento = aprendiz.Apr_DataDeNascimento;
            Apr_Sexo = aprendiz.Apr_Sexo;
            AprEstadoCivil = aprendiz.AprEstadoCivil;
            Apr_Endereço = aprendiz.Apr_Endereço;
            Apr_Complemento = aprendiz.Apr_Complemento;
            Apr_Bairro = aprendiz.Apr_Bairro;
            Apr_Cidade = aprendiz.Apr_Cidade;
            Apr_UF = aprendiz.Apr_UF;
            Apr_CEP = aprendiz.Apr_CEP;
            Apr_Telefone = aprendiz.Apr_Telefone;
            Apr_Celular = aprendiz.Apr_Celular;
            Apr_Email = aprendiz.Apr_Email;
            Apr_NomeMae = aprendiz.Apr_NomeMae;
            Apr_NomePai = aprendiz.Apr_NomePai;
            Apr_Naturalidade = aprendiz.Apr_Naturalidade;
            Apr_UF_Nat = aprendiz.Apr_UF_Nat;
            Apr_CarteiraDeIdentidade = aprendiz.Apr_CarteiraDeIdentidade;
            Apr_DataEmissão_Ident = aprendiz.Apr_DataEmissão_Ident;
            Apr_UF_Ident = aprendiz.Apr_UF_Ident;
            Apr_OrgaoEmissor_Ident = aprendiz.Apr_OrgaoEmissor_Ident;
            Apr_CPF = aprendiz.Apr_CPF;
            Apr_CarteiraDeTrabalho = aprendiz.Apr_CarteiraDeTrabalho;
            Apr_Serie_Cartrab = aprendiz.Apr_Serie_Cartrab;
            Apr_DataEmissão_CartTrab = aprendiz.Apr_DataEmissão_CartTrab;
            Apr_UF_CartTrab = aprendiz.Apr_UF_CartTrab;
            Apr_PIS = aprendiz.Apr_PIS;
            Apr_PNE = aprendiz.Apr_PNE;
            AprCodEscola = aprendiz.AprCodEscola;
            Apr_Escola_HInicio = aprendiz.Apr_Escola_HInicio;
            Apr_Escola_HTermino = aprendiz.Apr_Escola_HTermino;
            Apr_DataCadastro = aprendiz.Apr_DataCadastro;
            if (aprendiz.Apr_Situacao == null)
                Apr_Situacao = 0;
            else
                // ReSharper disable PossibleInvalidOperationException
                Apr_Situacao = (int)aprendiz.Apr_Situacao;
            // ReSharper restore PossibleInvalidOperationException

            Apr_Responsavel = aprendiz.Apr_Responsavel;
            Apr_Resp_CPF = aprendiz.Apr_Resp_CPF;
            Apr_Resp_EstadoCivil = aprendiz.Apr_Resp_EstadoCivil;
            Apr_Resp_GrauInstrucao = aprendiz.Apr_Resp_GrauInstrucao;
            Apr_Resp_Profissao = aprendiz.Apr_Resp_Profissao;
            Apr_Telefone_Resp = aprendiz.Apr_Telefone_Resp;
            Apr_Celular_Resp = aprendiz.Apr_Celular_Resp;
            Apr_Telefone_Contato = aprendiz.Apr_Telefone_Contato;
            Apr_Tipo_Contato = aprendiz.Apr_Tipo_Contato;
            Apr_Observacoes = aprendiz.Apr_Observacoes;
            Apr_Resp_Observacoes = aprendiz.Apr_Resp_Observacoes;
            Apr_Nacionalidade = aprendiz.Apr_Nacionalidade;
            Apr_NumeroEndereco = aprendiz.Apr_NumeroEndereco;
            Apr_Escolaridade = aprendiz.Apr_Escolaridade;
            Apr_TranspLinha01 = aprendiz.Apr_TranspLinha01;
            Apr_TranspLinha02 = aprendiz.Apr_TranspLinha02;
            Apr_TranspLinha03 = aprendiz.Apr_TranspLinha03;
            Apr_beneficio = aprendiz.Apr_beneficio;
            Apr_BolsaFamilia = aprendiz.Apr_BolsaFamilia;
            Apr_pensao = aprendiz.Apr_pensao;
            Apr_outros = aprendiz.Apr_outros;
            Apr_SituacaoResidencia = aprendiz.Apr_SituacaoResidencia;
            Apr_aluguel = aprendiz.Apr_aluguel;
            Apr_CarteiraAssinada = aprendiz.Apr_CarteiraAssinada;
            Apr_Empresa = aprendiz.Apr_Empresa;
            Apr_Cargo = aprendiz.Apr_Cargo;
            Apr_TempoPermanencia = aprendiz.Apr_TempoPermanencia;
            Apr_UsaMedicamento = aprendiz.Apr_UsaMedicamento;
            Apr_NomeMedicamento = aprendiz.Apr_NomeMedicamento;
            Apr_TipoMedicamento = aprendiz.Apr_TipoMedicamento;
            Apr_Alergia = aprendiz.Apr_Alergia;
            Apr_Doenca = aprendiz.Apr_Doenca;
            Apr_NomeDoenca = aprendiz.Apr_NomeDoenca;
            Apr_TipoAlergia = aprendiz.Apr_TipoAlergia;
            Apr_senha = aprendiz.Apr_senha;
            AprBanco = aprendiz.AprBanco;
            AprTipoConta = aprendiz.AprTipoConta;
            AprAgencia = aprendiz.AprAgencia;
            AprContaBancaria = aprendiz.AprContaBancaria;
            Apr_CertReservista = aprendiz.Apr_CertReservista;
            Apr_NumeroMTB = aprendiz.Apr_NumeroMTB;
            Apr_Tutor = aprendiz.Apr_Tutor;
            Apr_AreaAtuacao = aprendiz.Apr_AreaAtuacao;
            Apr_Mensagem = aprendiz.Apr_Mensagem;
            Apr_ValidadeMensagem = aprendiz.Apr_ValidadeMensagem;
            Apr_UsuarioCadastro = aprendiz.Apr_UsuarioCadastro;
            Apr_UsuarioDataCadastro = aprendiz.Apr_UsuarioDataCadastro;
            Apr_UsuarioAlteracao = aprendiz.Apr_UsuarioAlteracao;
            Apr_UsuarioDataAlteracao = aprendiz.Apr_UsuarioDataAlteracao;
            Apr_ValorTransp01 = aprendiz.Apr_ValorTransp01;
            Apr_ValorTransp02 = aprendiz.Apr_ValorTransp02;
            Apr_ValorTransp03 = aprendiz.Apr_ValorTransp03;
            Apr_Quantvt01 = aprendiz.Apr_Quantvt01;
            Apr_Quantvt02 = aprendiz.Apr_Quantvt02;
            Apr_Quantvt03 = aprendiz.Apr_Quantvt03;
            Apr_HabilitaPesquisa = aprendiz.Apr_HabilitaPesquisa;
            Apr_InicioAprendizagem = aprendiz.Apr_InicioAprendizagem;
            Apr_FimAprendizagem = aprendiz.Apr_FimAprendizagem;
            Apr_PrevFimAprendizagem = aprendiz.Apr_PrevFimAprendizagem;
            Apr_Resp_Parentesco = aprendiz.Apr_Resp_Parentesco;

            Apr_numeroFamiliares = aprendiz.Apr_NumeroFamiliares;
            Apr_RecebeBeneficio = aprendiz.Apr_recebebeneficio;
            Apr_Turma = aprendiz.Apr_AreaAtuacao;
            Apr_DataContrato = aprendiz.Apr_DataContrato;
            Apr_PlanoCurricular = aprendiz.Apr_PlanoCurricular;

            Apr_DataInicioEmpresa = aprendiz.Apr_DataInicioEmpresa;
            Apr_NumSistExterno = aprendiz.Apr_NumSistExterno;
            Apr_TurnoEscolar = aprendiz.Apr_TurnoEscolar;
            Apr_Estudante = aprendiz.Apr_Estudante;
            Apr_Deficiencia = aprendiz.Apr_Deficiencia;
            Apr_CBO = aprendiz.Apr_CBO;
            // Apr_Unidade = aprendiz.Apr_
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Apr_Codigo { get; set; }
        [MaxLength(60, ErrorMessage = " Tamanho máximo : 60 ")]
        public string Apr_Nome { get; set; }
        public DateTime? Apr_DataDeNascimento { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1 ")]
        public string Apr_Sexo { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1 ")]
        public string AprEstadoCivil { get; set; }
        [MaxLength(60, ErrorMessage = " Tamanho máximo : 60 ")]
        public string Apr_Endereço { get; set; }
        [MaxLength(30, ErrorMessage = " Tamanho máximo : 30 ")]
        public string Apr_Complemento { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string Apr_Bairro { get; set; }
        [MaxLength(40, ErrorMessage = " Tamanho máximo : 40")]
        public string Apr_Cidade { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2")]
        public string Apr_UF { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string Apr_CEP { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10")]
        public string Apr_Telefone { get; set; }
        [MaxLength(11, ErrorMessage = " Tamanho máximo : 11")]
        public string Apr_Celular { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80")]
        public string Apr_Email { get; set; }

        [MaxLength(60, ErrorMessage = " Tamanho máximo : 60")]
        public string Apr_NomeMae { get; set; }
        [MaxLength(60, ErrorMessage = " Tamanho máximo : 60")]
        public string Apr_NomePai { get; set; }
        [MaxLength(40, ErrorMessage = " Tamanho máximo : 40")]
        public string Apr_Naturalidade { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2")]
        public string Apr_UF_Nat { get; set; }
        [MaxLength(20, ErrorMessage = " Tamanho máximo : 20")]
        public string Apr_CarteiraDeIdentidade { get; set; }
        public DateTime? Apr_DataEmissão_Ident { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2")]
        public string Apr_UF_Ident { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10")]
        public string Apr_OrgaoEmissor_Ident { get; set; }
        [MaxLength(11, ErrorMessage = " Tamanho máximo : 11")]
        public string Apr_CPF { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string Apr_CarteiraDeTrabalho { get; set; }
        [MaxLength(6, ErrorMessage = " Tamanho máximo : 6")]
        public string Apr_Serie_Cartrab { get; set; }
        public DateTime? Apr_DataEmissão_CartTrab { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2")]
        public string Apr_UF_CartTrab { get; set; }
        [MaxLength(11, ErrorMessage = " Tamanho máximo : 11")]
        public string Apr_PIS { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80")]
        public string Apr_PNE { get; set; }
        public int? AprCodEscola { get; set; }
        public DateTime? Apr_Escola_HInicio { get; set; }
        public DateTime? Apr_Escola_HTermino { get; set; }
        public DateTime? Apr_DataCadastro { get; set; }
        public int Apr_Situacao { get; set; }
        [MaxLength(60, ErrorMessage = " Tamanho máximo : 60")]
        public string Apr_Responsavel { get; set; }
        [MaxLength(11, ErrorMessage = " Tamanho máximo : 11")]
        public string Apr_Resp_CPF { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string Apr_Resp_EstadoCivil { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2")]
        public string Apr_Resp_GrauInstrucao { get; set; }
        public int? Apr_Resp_Profissao { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10")]
        public string Apr_Telefone_Resp { get; set; }
        [MaxLength(11, ErrorMessage = " Tamanho máximo : 10")]
        public string Apr_Celular_Resp { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10")]
        public string Apr_Telefone_Contato { get; set; }
        [MaxLength(30, ErrorMessage = " Tamanho máximo : 30")]
        public string Apr_Tipo_Contato { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255")]
        public string Apr_Observacoes { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255")]
        public string Apr_Resp_Observacoes { get; set; }
        [MaxLength(30, ErrorMessage = " Tamanho máximo : 30")]
        public string Apr_Nacionalidade { get; set; }
        [MaxLength(6, ErrorMessage = " Tamanho máximo : 6")]
        public string Apr_NumeroEndereco { get; set; }

        public int? Apr_Escolaridade { get; set; }
        [MaxLength(5, ErrorMessage = " Tamanho máximo : 5")]
        public string Apr_TranspLinha01 { get; set; }
        [MaxLength(5, ErrorMessage = " Tamanho máximo : 5")]
        public string Apr_TranspLinha02 { get; set; }
        [MaxLength(5, ErrorMessage = " Tamanho máximo : 5")]
        public string Apr_TranspLinha03 { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string Apr_beneficio { get; set; }
        public float? Apr_BolsaFamilia { get; set; }
        public float? Apr_pensao { get; set; }
        public float? Apr_outros { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string Apr_SituacaoResidencia { get; set; }
        public float? Apr_aluguel { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string Apr_CarteiraAssinada { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string Apr_Empresa { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string Apr_Cargo { get; set; }
        [MaxLength(20, ErrorMessage = " Tamanho máximo : 20")]
        public string Apr_TempoPermanencia { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string Apr_UsaMedicamento { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string Apr_NomeMedicamento { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string Apr_TipoMedicamento { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string Apr_Alergia { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string Apr_Doenca { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string Apr_NomeDoenca { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string Apr_TipoAlergia { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string Apr_senha { get; set; }
        [MaxLength(3, ErrorMessage = " Tamanho máximo : 3")]
        public string AprBanco { get; set; }
        [MaxLength(3, ErrorMessage = " Tamanho máximo : 3")]
        public string AprTipoConta { get; set; }
        [MaxLength(5, ErrorMessage = " Tamanho máximo : 5")]
        public string AprAgencia { get; set; }
        [MaxLength(12, ErrorMessage = " Tamanho máximo : 12")]
        public string AprContaBancaria { get; set; }
        [MaxLength(20, ErrorMessage = " Tamanho máximo : 20")]
        public string Apr_CertReservista { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string Apr_NumeroMTB { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10")]
        public string Apr_Tutor { get; set; }
        public int? Apr_AreaAtuacao { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255")]
        public string Apr_Mensagem { get; set; }
        public DateTime? Apr_ValidadeMensagem { get; set; }

        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string Apr_UsuarioCadastro { get; set; }
        public DateTime? Apr_UsuarioDataCadastro { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string Apr_UsuarioAlteracao { get; set; }
        public DateTime? Apr_UsuarioDataAlteracao { get; set; }

        public double? Apr_ValorTransp01 { get; set; }
        public double? Apr_ValorTransp02 { get; set; }
        public double? Apr_ValorTransp03 { get; set; }
        public short? Apr_Quantvt01 { get; set; }
        public short? Apr_Quantvt02 { get; set; }
        public short? Apr_Quantvt03 { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string Apr_HabilitaPesquisa { get; set; }
        public DateTime? Apr_InicioAprendizagem { get; set; }
        public DateTime? Apr_FimAprendizagem { get; set; }
        public DateTime? Apr_PrevFimAprendizagem { get; set; }
        public int? Apr_Resp_Parentesco { get; set; }
        public short? Apr_numeroFamiliares { get; set; }
        public string Apr_RecebeBeneficio { get; set; }
        public int? Apr_Turma { get; set; }
        public DateTime? Apr_DataContrato { get; set; }
        public short? Apr_PlanoCurricular { get; set; }

        public string Apr_NumSistExterno { get; set; }

        public DateTime? Apr_DataInicioEmpresa { get; set; }

        public string Apr_TurnoEscolar { get; set; }
        public string Apr_Estudante { get; set; }
        public string Apr_Deficiencia { get; set; }
        public string Apr_CBO { get; set; }
        public int? Apr_Unidade { get; set; }
        public string Apr_TipoAprendizagem { get; set; }

        public short? Apr_MesesContrato { get; set; }
        public short? Apr_HorasDiarias { get; set; }

        public int? Apr_TurmaCCI { get; set; }
        public int? Apr_TurmaENC { get; set; }

        public int? Apr_InstParceira { get; set; }

        public DateTime? Apr_DataPrevInicFerias { get; set; }
        public DateTime? Apr_DataPrevTermFerias { get; set; }


        public string Apr_ConhecimentoWindows { get; set; }
        public string Apr_ConhecimentoWord { get; set; }
        public string Apr_ConhecimentoExcel { get; set; }
        public string Apr_TemFilhos { get; set; }
        public string Apr_Filhosquemfica { get; set; }
        public int? Apr_QuantidadeFilhos { get; set; }

    }

    [Table("CA_AlocacaoAprendiz")]
    public class AprendizUnidade
    {

        public AprendizUnidade()
        {
            ALAOrientador = null;
            ALAMotivoDesligamento = null;
        }


        [Key, Column(Order = 1)]
        public int ALAAprendiz { get; set; }
        [Key, Column(Order = 2)]
        public int ALATurma { get; set; }
        [Key, Column(Order = 3)]
        public int ALAUnidadeParceiro { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string ALAStatus { get; set; }
        [MaxLength(8, ErrorMessage = "Tamanho maximo = 8")]
        public string ALATutor { get; set; }
        public DateTime? ALADataInicio { get; set; }
        public DateTime? ALADataPrevTermino { get; set; }
        public DateTime? ALADataTermino { get; set; }
        public DateTime? ALAInicioExpediente { get; set; }
        public DateTime? ALATerminoExpediente { get; set; }
        public float? ALAValorBolsa { get; set; }
        public float? ALAValorEncargos { get; set; }
        public float? ALAValorTaxa { get; set; }
        public string ALAObservacao { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ALAOrdem { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string ALApagto { get; set; }
        public int? ALAOrientador { get; set; }
        public int? ALAMotivoDesligamento { get; set; }

        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string ALAUsuarioCadastro { get; set; }
        public DateTime? ALAUsuarioDataCadastro { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string ALAUsuarioAlteracao { get; set; }
        public DateTime? ALAUsuarioDataAlteracao { get; set; }
        public int? ALATurmaAnterior { get; set; }
        public string ALAMotivoMudaTurma { get; set; }
        public string ALAUsuarioMudaTurma { get; set; }
        public DateTime? ALADataMudaTurma { get; set; }

        public short? AlaMesPagto01Uniforme { get; set; }
        public int? AlaAnoPagto01Uniforme { get; set; }
        public short? AlaMesPagto02Uniforme { get; set; }
        public int? AlaAnoPagto02Uniforme { get; set; }


    }

    [Table("CA_DisciplinasAprendiz")]
    public class DesempenhoAprendiz
    {
        [Key, Column(Order = 1)]
        public int DiaCodAprendiz { get; set; }
        [Key, Column(Order = 2)]
        public int DiaDisciplinaProf { get; set; }
        public short? DiaCargaHoraria { get; set; }
        public short? DiaNumeroFaltas { get; set; }
        [MaxLength(2, ErrorMessage = "Tamanho maximo = 2")]
        public string DiaConceito { get; set; }
        [MaxLength(255, ErrorMessage = "Tamanho maximo = 255")]
        public string DiaObservacoes { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula1 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula2 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula3 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula4 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula5 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula6 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula7 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula8 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula9 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula10 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula11 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula12 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula13 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula14 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula15 { get; set; }

        public byte? DieCompreensao { get; set; }
        public byte? DieComLing { get; set; }
        public byte? DiePostura { get; set; }
        public byte? DieParticipacao { get; set; }

        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula16 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula17 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula18 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula19 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaAula20 { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string DiaDestaque { get; set; }
        [MaxLength(255, ErrorMessage = "Tamanho maximo = 255")]
        public string DiaParecerFinal { get; set; }

    }

    [Table("CA_DisciplinasTurmaProf")]
    public class DisciplinasProf
    {
        [Key, Column(Order = 1)]
        public int DPTurma { get; set; }
        [Key, Column(Order = 2)]
        public int DPProf { get; set; }
        [Key, Column(Order = 3)]
        public short DPDisciplina { get; set; }
        [Key, Column(Order = 4)]
        public DateTime DPDataInicio { get; set; }
        public DateTime? DPDataTermino { get; set; }
        [MaxLength(30, ErrorMessage = " Tamanho máximo : 30 ")]
        public string DpSalaFisica { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DpOrdem { get; set; }
        public int? DPProfAux { get; set; }
        public string DPParecer01 { get; set; }
        public string DPParecer02 { get; set; }
        public string DPParecer03 { get; set; }

    }

    [Table("CA_AulasDisciplinasTurmaProf")]
    public class AulasProfessores
    {

        public AulasProfessores()
        {
        }

        public AulasProfessores(CA_AulasDisciplinasTurmaProf aula)
        {
            // ADPCodigo = aula.ADPCodigo;
            //  ADPDisciplinaProf = aula.ADPDisciplinaProf;
            ADPDataAula = aula.ADPDataAula;
            ADPStatus = aula.ADPStatus;
            ADPConteudoLecionado = aula.ADPConteudoLecionado;
            ADPRecursosUsados = aula.ADPRecursosUsados;
            ADPObservacoes = aula.ADPObservacoes;
            ADPOrdemAula = aula.ADPOrdemAula;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ADPCodigo { get; set; }
        public int ADPDisciplinaProf { get; set; }
        public DateTime ADPDataAula { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1 ")]
        public string ADPStatus { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255 ")]
        public string ADPConteudoLecionado { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255 ")]
        public string ADPRecursosUsados { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255 ")]
        public string ADPObservacoes { get; set; }
        public int? ADPOrdemAula { get; set; }
    }

    [Table("CA_AreaAtuacao")]
    public class AreaAtuacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaCodigo { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string AreaDescricao { get; set; }
        public int? AreaNumeroCadastro { get; set; }
        public int? AreaCargaHoraria { get; set; }

        public int? AreaCargaTeorica { get; set; }
        public int? AreaCargaPratica { get; set; }

       
    }

    [Table("CA_Feriados")]
    public class Feriados
    {
        [Key, Column(Order = 1)]
        public int FerUnidade { get; set; }
        [Key, Column(Order = 2)]
        public DateTime FerData { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string FerDescricao { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FerOrdem { get; set; }

    }

    [Table("CA_Planos")]
    public class Planos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanCodigo { get; set; }
        [MaxLength(6, ErrorMessage = " Tamanho máximo : 6 ")]
        public string PlanCurso { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string PlanDescricao { get; set; }
    }

    [Table("CA_SituacaoAprendiz")]
    public class Situacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaCodigo { get; set; }
        [MaxLength(2, ErrorMessage = "Tamanho maximo = 2")]
        public string StaAbreviatura { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string StaDescricao { get; set; }

    }

    [Table("CA_MotivoDesligamento")]
    public class MotivoDesligamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MotCodigo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string MotDescricao { get; set; }

    }

    [Table("CA_Orientador")]
    public class Orientador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OriCodigo { get; set; }
        public int OriUnidadeParceiro { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string OriNome { get; set; }
        [MaxLength(10, ErrorMessage = "Tamanho maximo = 10")]
        public string OriTelefone { get; set; }
        [MaxLength(80, ErrorMessage = "Tamanho maximo = 80")]
        public string OriEmail { get; set; }
    }

    [Table("CA_Fechamento")]
    public class Fechamento
    {
        [Key, Column(Order = 1)]
        public int FechAprendiz { get; set; }
        [Key, Column(Order = 2)]
        public int FechUnidade { get; set; }
        [Key, Column(Order = 3)]
        public int FechMesFechamento { get; set; }
        [Key, Column(Order = 4)]
        [MaxLength(4, ErrorMessage = "Tamanho maximo = 4")]
        public string FechAnoFechamento { get; set; }
        public float FechBolsa { get; set; }
        public float FechTaxa { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string FechPagamento { get; set; }
        public int FechTurma { get; set; }
        public int FechAreaAtuacao { get; set; }
    }

    [Table("CA_IndicesSociais")]
    public class IndicesSociais
    {
        [Key, Column(Order = 1)]
        public int IndCodigo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string IndDescricao { get; set; }
        public double? IndValorMinimo { get; set; }
        public double? IndValorMaximo { get; set; }
    }

    [Table("CA_Resposta_Parceiro")]
    public class RespostaPesquisa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResCodigo { get; set; }
        public int ResPesquisaParceiro { get; set; }
        public int ResQuestao { get; set; }
        public int ResOpcaoEscolhida { get; set; }
        [MaxLength(255, ErrorMessage = "Tamanho maximo = 255")]
        public string ResRespostaTexto { get; set; }

    }

    [Table("CA_Pesquisa")]
    public class Pesquisa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PesCodigo { get; set; }
        [MaxLength(80, ErrorMessage = "Tamanho maximo = 80")]
        public string PesNome { get; set; }
        [MaxLength(255, ErrorMessage = "Tamanho maximo = 255")]
        public string PesDescricao { get; set; }
    }

    [Table("CA_Questao")]
    public class Questao
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QueCodigo { get; set; }
        [Key, Column(Order = 2)]
        public int QuePesquisa { get; set; }
        [MaxLength(255, ErrorMessage = "Tamanho maximo = 255")]
        public string QueTexto { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string QueTipoQustao { get; set; }
        public int QueOrdemExibicao { get; set; }
    }

    [Table("CA_Opcoes")]
    public class Opcao
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OpcCodigo { get; set; }
        [Key, Column(Order = 2)]
        public int OpcQuestao { get; set; }
        [MaxLength(80, ErrorMessage = "Tamanho maximo = 80")]
        public string OpcTexto { get; set; }
        public int OpcOrdemExibicao { get; set; }
        public short? OpcNota { get; set; }
    }

    [Serializable]
    [Table("CA_Pesquisa_Parceiro")]
    public class PesquisaParceiro
    {
        public PesquisaParceiro()
        {
        }

        public PesquisaParceiro(CA_Pesquisa_Parceiro pesquisaParceiro)
        {
            PepCodigo = pesquisaParceiro.PepCodigo;
            PepPesquisaCodigo = pesquisaParceiro.PepPesquisaCodigo;
            PepParceiroCodigo = pesquisaParceiro.PepParceiroCodigo;
            PepAno = pesquisaParceiro.PepAno;
            PepMes = pesquisaParceiro.PepMes;
            PepRealizada = pesquisaParceiro.PepRealizada;
            PepAprendiz = pesquisaParceiro.PepAprendiz == null ? 0 : (int)pesquisaParceiro.PepAprendiz;
            PepDataRealizada = pesquisaParceiro.PepDataRealizada;
            PepTutor = pesquisaParceiro.PepTutor;
            PepObservacao = pesquisaParceiro.PepObservacao;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int PepCodigo { get; set; }
        public int PepPesquisaCodigo { get; set; }
        public int PepParceiroCodigo { get; set; }
        [MaxLength(4, ErrorMessage = "Tamanho maximo = 4")]
        public string PepAno { get; set; }
        public int PepMes { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string PepRealizada { get; set; }
        public int PepAprendiz { get; set; }
        public DateTime? PepDataRealizada { get; set; }
        [MaxLength(8, ErrorMessage = "Tamanho maximo = 8")]
        public string PepTutor { get; set; }
        [MaxLength(255, ErrorMessage = "Tamanho maximo =255")]
        public string PepObservacao { get; set; }

        public int PepTurma { get; set; }
        public int PepOrientador { get; set; }

    }

    [Table("CA_Conceitos")]
    public class Conceitos
    {
        [Key]
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2")]
        public string ConCodigo { get; set; }
        public float? ConNota { get; set; }
        public float? ConPercentual { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1 ")]
        public string ConAprova { get; set; }
    }

    [Table("CA_Cursos")]
    public class Curso
    {
        public Curso()
        {
            CurCodigo = null;
        }

        [Key]
        [MaxLength(6, ErrorMessage = " Tamanho máximo : 6 ")]
        public string CurCodigo { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string CurDescricao { get; set; }
        public int? CurCargaHoraria { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string CurAbreviatura { get; set; }
        public short? EnsNumeroPeriodos { get; set; }
    }

    [Table("CA_Disciplinas")]
    public class Disciplina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short DisCodigo { get; set; }
        [MaxLength(80, ErrorMessage = "Tamanho maximo = 80")]
        public string DisDescricao { get; set; }
        [MaxLength(10, ErrorMessage = "Tamanho maximo = 10")]
        public string DisAbreviatura { get; set; }
        public string DisCor { get; set; }
    }

    [Table("CA_Educadores")]
    public class Educadores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EducCodigo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string EducNome { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string EducSexo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string EducEndereco { get; set; }

        [MaxLength(6, ErrorMessage = "Tamanho maximo = 6")]
        public string EducNumeroEndereco { get; set; }
        [MaxLength(20, ErrorMessage = "Tamanho maximo = 20")]
        public string EducComplemento { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string EducBairro { get; set; }

        [MaxLength(30, ErrorMessage = "Tamanho maximo = 30")]
        public string EducCidade { get; set; }
        [MaxLength(2, ErrorMessage = "Tamanho maximo = 2")]
        public string EducUF { get; set; }
        [MaxLength(9, ErrorMessage = "Tamanho maximo = 9")]
        public string EducCEP { get; set; }

        [MaxLength(10, ErrorMessage = "Tamanho maximo = 9")]
        public string EducTelefone { get; set; }
        [MaxLength(11, ErrorMessage = "Tamanho maximo = 9")]
        public string EducTelefoneCelular { get; set; }
        [MaxLength(60, ErrorMessage = "Tamanho maximo = 60")]
        public string EducEMail { get; set; }
        public DateTime? EducDataNascimento { get; set; }

        [MaxLength(30, ErrorMessage = "Tamanho maximo = 30")]
        public string EducNaturalidade { get; set; }
        [MaxLength(2, ErrorMessage = "Tamanho maximo = 2")]
        public string EducUFNaturalidade { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string EducEstadoCivil { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string EducNomedoPai { get; set; }

        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string EducNomedaMae { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string EducSituacao { get; set; }

        public DateTime? EducDataEntrada { get; set; }
        public DateTime? EducDataSaida { get; set; }

        [MaxLength(25, ErrorMessage = "Tamanho maximo = 25")]
        public string EducTipoAdmissao { get; set; }
        [MaxLength(15, ErrorMessage = "Tamanho maximo = 15")]
        public string EducIdentidade { get; set; }
        [MaxLength(8, ErrorMessage = "Tamanho maximo = 8")]
        public string EducExpedIdentidade { get; set; }
        [MaxLength(11, ErrorMessage = "Tamanho maximo = 11")]
        public string EducCPF { get; set; }
        [MaxLength(15, ErrorMessage = "Tamanho maximo = 15")]
        public string EducTitulodeEleitor { get; set; }
        [MaxLength(4, ErrorMessage = "Tamanho maximo = 4")]
        public string EducZonaEleitoral { get; set; }
        [MaxLength(4, ErrorMessage = "Tamanho maximo = 4")]
        public string EducSecaoEleitoral { get; set; }
        [MaxLength(30, ErrorMessage = "Tamanho maximo = 30")]
        public string EducMunEleitoral { get; set; }
        [MaxLength(8, ErrorMessage = "Tamanho maximo = 8")]
        public string EducSenha { get; set; }
        [MaxLength(20, ErrorMessage = "Tamanho maximo = 20")]
        public string EducCartprofissional { get; set; }
        [MaxLength(5, ErrorMessage = "Tamanho maximo = 5")]
        public string EducSerieCartProfissional { get; set; }
        [MaxLength(255, ErrorMessage = "Tamanho maximo = 255")]
        public string EducObservacoes { get; set; }
        public int? EducGrauInstrucao { get; set; }

    }

    [Table("CA_GrauEscolaridade")]
    public class Escolaridade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GreCodigo { get; set; }
        [MaxLength(30, ErrorMessage = "Tamanho maximo = 30")]
        public string GreDescricao { get; set; }
    }


    [Table("CA_RequisicoesVagas")]
    public class RequisicoesVagas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReqId { get; set; }

        public int ReqEmpresa { get; set; }

        public DateTime ReqDataSolitação { get; set; }

        public short ReqQuantidade { get; set; }

        public string ReqSexo { get; set; }

        public string ReqHorarioEntrevista { get; set; }

        public string ReqSubstituicao { get; set; }

        public string ReqCaracteristicasPessoais { get; set; }

        public int ReqAreaAtuacao { get; set; }

        public string ReqHabilidades { get; set; }

        public string ReqAtividades { get; set; }

        public string ReqContaoEntrevista { get; set; }
        public string ReqObservacoes { get; set; }
        public string ReqSituacao { get; set; }
        public string ReqObservacoesInst { get; set; }
        public string ReqBeneficios { get; set; }
        public string ReqHorarioTrabalho { get; set; }
        public Single ReqSalario { get; set; }
        public string ReqSubstituir { get; set; }

        public DateTime? ReqDataEntrevista { get; set; }
    }

    [Table("CA_Escolas")]
    public class Escolas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EscCodigo { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string EscNome { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string EscEndereco { get; set; }
        [MaxLength(6, ErrorMessage = " Tamanho máximo : 6 ")]
        public string EscNumeroEndereco { get; set; }
        [MaxLength(30, ErrorMessage = " Tamanho máximo : 30 ")]
        public string EscComplemento { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string EscBairro { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8 ")]
        public string EscCEP { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string EscCidade { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2 ")]
        public string EscEstado { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10 ")]
        public string EscTelefone { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string EscDiretor { get; set; }

        [MaxLength(50, ErrorMessage = " Tamanho máximo : 80 ")]
        public string EscEmail { get; set; }
    }

    [Table("CA_GrauParentesco")]
    public class GrauParentesco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GpaCodigo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string GpaDescricao { get; set; }
    }

    [Table("CA_Membro_Familia")]
    public class MembroFamilia
    {
        public MembroFamilia()
        {
            Fam_DataNascimento = new DateTime(1900, 1, 1);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Fam_Ordem { get; set; }
        public int Fam_AperendizId { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string Fam_Nome { get; set; }
        public int Fam_GrauParentescoId { get; set; }
        public DateTime? Fam_DataNascimento { get; set; }
        public int Fam_ProfissaoId { get; set; }
        public int Fam_TipoEmpregoId { get; set; }
        public float Fam_Renda { get; set; }

        public string Fam_Identidade { get; set; }
        public string Fam_OrgaoIdentidade { get; set; }




    }

    [Table("CA_Ocorrencias")]
    public class Ocorrencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OcoCodigo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string OcoDescricao { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string OcoTipo { get; set; }
    }

    [Table("CA_OcorrenciasAprendiz")]
    public class OcorrenciaAprendiz
    {

        [Key, Column(Order = 1)]
        public int OcaCodAprendiz { get; set; }
        [Key, Column(Order = 2)]
        public int OcaCodOcorrencia { get; set; }
        [Key, Column(Order = 3)]
        public DateTime OcaDataOcorrencia { get; set; }

        [MaxLength(8, ErrorMessage = "Tamanho maximo = 8")]
        public string OcaUsuarioocorrencia { get; set; }
        [MaxLength(1000, ErrorMessage = "Tamanho maximo = 1000")]
        public string OcaObservacoes { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OcaOrdem { get; set; }
        public DateTime? OcaDataEntrega { get; set; }

        public DateTime? OcaPrevDevolucao { get; set; }
        public DateTime? OcaDevolucao { get; set; }
        public string OcaEmissorOcorrencia { get; set; }


    }

    [Table("CA_Parceiros")]
    public class Parceiros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParCodigo { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string ParDescricao { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string ParNomeFantasia { get; set; }
        public int ParAtividadeId { get; set; }
        [MaxLength(14, ErrorMessage = " Tamanho máximo : 14 ")]
        public string ParCNPJ { get; set; }
        [MaxLength(100, ErrorMessage = " Tamanho máximo : 100")]
        public string ParInscricao { get; set; }
        [MaxLength(20, ErrorMessage = " Tamanho máximo : 20")]
        public string ParInscricaoMunicipal { get; set; }
        [MaxLength(100, ErrorMessage = " Tamanho máximo : 100")]
        public string ParEndereco { get; set; }
        [MaxLength(6, ErrorMessage = " Tamanho máximo : 6")]
        public string ParNumeroEndereco { get; set; }
        [MaxLength(20, ErrorMessage = " Tamanho máximo : 20")]
        public string ParComplemento { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string ParBairro { get; set; }
        [MaxLength(30, ErrorMessage = " Tamanho máximo : 30")]
        public string ParCidade { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2")]
        public string ParEstado { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80")]
        public string ParEmail { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80")]
        public string ParSiteEmpresa { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]

        public string ParNomeContato { get; set; }
        public float? ParTaxa20Horas { get; set; }
        public float? ParBolsa20Horas { get; set; }
        public float? ParTaxa30Horas { get; set; }
        public float? ParBolsa30Horas { get; set; }

        
        public DateTime? ParDataContrato { get; set; }
        public DateTime? ParDataCadastro { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8 ")]
        public string ParUsuarioCadastro { get; set; }
        public DateTime? ParDataAlteracao { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8 ")]
        public string ParUsuarioAlteracao { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10")]
        public string ParTelefone { get; set; }
        [MaxLength(11, ErrorMessage = " Tamanho máximo : 10")]
        public string ParCelular { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string ParCEP { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string ParSenha { get; set; }

        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string ParRespFundacao { get; set; }

        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string ParGestorPrograma { get; set; }

        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80")]
        public string ParEmailGestorPrograma { get; set; }

        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string ParGestorFinanceiro { get; set; }

        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80")]
        public string ParEmailGestorFinanceiro { get; set; }

        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50")]
        public string ParGestorRH { get; set; }

        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80")]
        public string ParEmailGestorRH { get; set; }

        [MaxLength(80, ErrorMessage = " Tamanho máximo : 30")]
        public string ParCargoRepresentanteLegal { get; set; }

        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80")]
        public string ParEmailRespresentanteLegal { get; set; }


        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string ParSituacao { get; set; }

        public float? ParValeRefeicao { get; set; }
        public float? ParValeAlimentacao { get; set; }

        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string ParValeTransporte { get; set; }

        [MaxLength(500, ErrorMessage = " Tamanho máximo : 500")]
        public string ParBeneficios { get; set; }

        [MaxLength(1000, ErrorMessage = " Tamanho máximo : 1000")]
        public string ParObservacoes { get; set; }


        public string ParConvenio { get; set; }

        public short? ParCotaAprendiz { get; set; }

        public DateTime? ParDataAnivGestorProg { get; set; }
        public DateTime? ParDataAnivGestorRH { get; set; }
        public DateTime? ParDataAnivGestorFinanc { get; set; }
        public DateTime? ParDataAnivRepLegal { get; set; }
    }

    [Table("CA_ParceirosUnidade")]
    public class ParceirosUnidade
    {
        public ParceirosUnidade()
        {
            ParUniCodigo = 0;
            ParUniCodigoParceiro = 0;
            ParUniDataCadastro = null;
            ParUniDataAlteracao = null;
            ParUniTaxa20Horas = null;
            ParUniTaxa20Horas = null;
            ParUniTaxa30Horas = null;
            ParUniBolsa30Horas = null;
        }


        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParUniCodigo { get; set; }
        [Key, Column(Order = 2)]
        public int ParUniCodigoParceiro { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string ParUniDescricao { get; set; }
        [MaxLength(14, ErrorMessage = " Tamanho máximo : 14 ")]
        public string ParUniCNPJ { get; set; }
        [MaxLength(100, ErrorMessage = " Tamanho máximo : 100 ")]
        public string ParUniEndereco { get; set; }
        [MaxLength(6, ErrorMessage = " Tamanho máximo : 6 ")]
        public string ParUniNumeroEndereco { get; set; }
        [MaxLength(20, ErrorMessage = " Tamanho máximo : 20 ")]
        public string ParUniComplemento { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string ParUniBairro { get; set; }
        [MaxLength(30, ErrorMessage = " Tamanho máximo : 30 ")]
        public string ParUniCidade { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2 ")]
        public string ParUniEstado { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string ParUniEmail { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string ParUniNomeContato { get; set; }
        public float? ParUniTaxa20Horas { get; set; }
        public float? ParUniBolsa20Horas { get; set; }
        public float? ParUniTaxa30Horas { get; set; }
        public float? ParUniBolsa30Horas { get; set; }
        public DateTime? ParUniDataCadastro { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8 ")]
        public string ParUniUsuarioCadastro { get; set; }
        public DateTime? ParUniDataAlteracao { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8 ")]
        public string ParUniUsuarioAlteracao { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10")]
        public string ParUniTelefone { get; set; }
        [MaxLength(11, ErrorMessage = " Tamanho máximo : 11")]
        public string ParUniCelular { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8")]
        public string ParUniCEP { get; set; }

        [MaxLength(10, ErrorMessage = " Tamanho máximo : 50")]
        public string ParUniResponsavelLegal { get; set; }

    }

    [Table("CA_Profissoes")]
    public class Profissoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfCodigo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string ProfDescricao { get; set; }
    }

    [Table("CA_RamosAtividades")]
    public class RamoAtividade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatCodigo { get; set; }
        [MaxLength(60, ErrorMessage = " Tamanho máximo : 60 ")]
        public string RatDescricao { get; set; }
    }

    [Table("CA_Vinculo_Empregaticio")]
    public class TipoEmprego
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Vin_Codigo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string Vin_Descricao { get; set; }
    }

    [Table("CA_Turmas")]
    public class Turma
    {
        public Turma()
        {
            TurInicio = null;
            Termino = null;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TurCodigo { get; set; }
        [MaxLength(6, ErrorMessage = "Tamanho maximo = 6")]
        public string TurCurso { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string TurDiaSemana { get; set; }
        [MaxLength(20, ErrorMessage = "Tamanho maximo = 20")]
        public string TurNome { get; set; }

        public DateTime? TurInicio { get; set; }
        public DateTime? Termino { get; set; }

        [MaxLength(255, ErrorMessage = "Tamanho maximo = 255")]
        public string TurObservacoes { get; set; }
        [MaxLength(8, ErrorMessage = "Tamanho maximo = 8")]
        public string TurUsuario { get; set; }
        [MaxLength(1, ErrorMessage = "Tamanho maximo = 1")]
        public string TurStatus { get; set; }
        public int? TurUnidade { get; set; }
        public int? TurNumeroMeses { get; set; }
        public short? TurPlanoCurricular { get; set; }
        public int? TurEducadorResponsavel { get; set; }


    }

    [Table("CA_Unidades")]
    public class Unidades
    {
        //public Unidades()
        //{
        //    UniCodigo = 0;
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UniCodigo { get; set; }
        // [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string UniNome { get; set; }
        [MaxLength(60, ErrorMessage = " Tamanho máximo : 60 ")]
        public string UniEndereco { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string UniNumeroEndereco { get; set; }
        [MaxLength(6, ErrorMessage = " Tamanho máximo : 6 ")]
        public string UniComplemento { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string UniBairro { get; set; }
        [MaxLength(40, ErrorMessage = " Tamanho máximo : 40 ")]
        public string UniCEP { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8 ")]
        public string UniCidade { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string UniEstado { get; set; }
        [MaxLength(2, ErrorMessage = " Tamanho máximo : 2 ")]
        public string UniTelefone { get; set; }
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10 ")]
        public string UniCGC { get; set; }
        [MaxLength(18, ErrorMessage = " Tamanho máximo : 18 ")]
        public string UniEnderecoWeb { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string UniEmailPadraoEnvio { get; set; }

        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string UniRepresentanteLegal { get; set; }

        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string UniRepresentanteCargo { get; set; }

        public Single? UniFGTSFolha { get; set; }
        public Single? UniFGTSFerias { get; set; }
        public Single? UniFGTS13Sala { get; set; }
        public Single? UniPISFolha { get; set; }
        public Single? UniPISFerias { get; set; }
        public Single? UniPIS13Sal { get; set; }
        public Single? UniFerias { get; set; }
        public Single? UniUmTerco { get; set; }
        public Single? Uniseguro { get; set; }
        public Single? UniPCMSO { get; set; }
        public Single? UniValorUniforme { get; set; }
        public Single? Uni13Salario { get; set; }


    }

    [Table("CA_Usuarios")]
    public class Usuarios
    {
        [Key]
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8 ")]
        public string UsuCodigo { get; set; }
        [MaxLength(90, ErrorMessage = " Tamanho máximo : 90 ")]
        public string UsuNome { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 16 ")]
        public string UsuSenha { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo :1 ")]
        public string UsuTipo { get; set; }
        [MaxLength(60, ErrorMessage = " Tamanho máximo : 60 ")]
        public string UsuEmail { get; set; }
    }


    [Table("CA_Frequecia")]
    public class Frequencia
    {
        [Key, Column(Order = 1)]
        public int FreqAprendiz { get; set; }
        [Key, Column(Order = 2)]
        [MaxLength(7, ErrorMessage = " Tamanho máximo : 7 ")]
        public string FreqReferencia { get; set; }
        public DateTime FreqDataPrevEntrega { get; set; }
        public DateTime? FreqDataEntrega { get; set; }
        [MaxLength(8, ErrorMessage = " Tamanho máximo : 8 ")]
        public string FreqUsuarioEntrega { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255 ")]
        public string FreqFaltas { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255 ")]
        public string FreqObservacao { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string FreqSituacao { get; set; }
        public int? FreqNumFaltas { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FreqOrdem { get; set; }

    }


    [Table("CA_Vale_Transporte")]
    public class ValeTransporte
    {
        [Key, Column(Order = 1)]
        public int VtpAprendiz { get; set; }
        [Key, Column(Order = 2)]
        public int VtpItinerario { get; set; }
        [Key, Column(Order = 2)]
        [MaxLength(10, ErrorMessage = " Tamanho máximo : 10 ")]
        public string VtpLinha { get; set; }
        public float VtpTarifa { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VtpOrdem { get; set; }
        public short VtpQuantidade { get; set; }
    }

    [Table("CA_Itinerarios")]
    public class Itinerarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItnCodigo { get; set; }
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string ItnNome { get; set; }
    }

    [Table("CA_Bairros")]
    public class Bairros
    {
        [Key]
        [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string DescBairro { get; set; }
        public int RegBairro { get; set; }
    }

    [Table("CA_Regioes")]
    public class Regioes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodRegiao { get; set; }
        [MaxLength(40, ErrorMessage = " Tamanho máximo : 40 ")]
        public string DescRegiao { get; set; }
    }

    [Table("CA_PerfilUsuario")]
    public class PerfilUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PerfCodigo { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1 ")]
        public string PerfDescricao { get; set; }
    }

    [Table("CA_Eventos")]
    public class Eventos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EvnCodigo { get; set; }
        [MaxLength(80, ErrorMessage = " Tamanho máximo : 80 ")]
        public string EvnNome { get; set; }
        public DateTime EvnData { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255")]
        public string EvnDescricao { get; set; }
    }

    [Table("CA_Participantes")]
    public class Participantes
    {
        [Key, Column(Order = 1)]
        public int PrtCodigoEvento { get; set; }
        [Key, Column(Order = 2)]
        public int PrtAprendiz { get; set; }
        [MaxLength(255, ErrorMessage = " Tamanho máximo : 255")]
        public string PrtObservacao { get; set; }
        [MaxLength(1, ErrorMessage = " Tamanho máximo : 1")]
        public string PrtPresenca { get; set; }
    }

    [Table("CA_AulasDisciplinasAprendiz")]
    public class AulasDisciplinasAprendiz
    {
        [Key, Column(Order = 1)]
        public int AdiCodAprendiz { get; set; }
        [Key, Column(Order = 2)]
        public int AdiTurma { get; set; }
        [Key, Column(Order = 3)]
        public int AdiDisciplina { get; set; }
        [Key, Column(Order = 4)]
        public int AdiEducador { get; set; }
        public DateTime AdiDataAula { get; set; }
        public int AdiCargaHoraria { get; set; }
        public string AdiPresenca { get; set; }
        public string AdiObservacoes { get; set; }
    }





    [Table("CA_InstituicoesParceiras")]
    public class CAInstituicoesParceiras
    {
        public CAInstituicoesParceiras()
        {
            IpaCodigo = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IpaCodigo { get; set; }
        // [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string IpaDescricao { get; set; }

        public string IpaEndereco { get; set; }

        public string IpaNumeroEndereco { get; set; }

        public string IpaComplemento { get; set; }

        public string IpaBairro { get; set; }

        public string IpaCidade { get; set; }

        public string IpaEstado { get; set; }
        public string IpaCEP { get; set; }


        public string IpaEmail { get; set; }


        public string IpaTelefone { get; set; }

        public string IpaCelular { get; set; }

        public string IpaNomeContato { get; set; }

        public DateTime? IpaDataCadastro { get; set; }
        public DateTime? IpaDataAlteracao { get; set; }

        public string IpaUsuarioAlteracao { get; set; }
        public string IpaUsuarioCadastro { get; set; }
        public string IpaSenha { get; set; }



    }


    [Table("CA_StatusEncaminhamento")]
    public class CAStatusEncaminhamento
    {
        public CAStatusEncaminhamento()
        {
            Ste_Codigo = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Ste_Codigo { get; set; }
        // [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public string Ste_Descricao { get; set; }

    }



    [Table("CA_Documentos")]
    public class CADocumentos
    {
        public CADocumentos()
        {
            DocCodigo = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DocCodigo { get; set; }
        public string DocDescricao { get; set; }
        // [MaxLength(50, ErrorMessage = " Tamanho máximo : 50 ")]
        public Single? DocValor { get; set; }

        public string DocTipo { get; set; }
        public string DocObrigatorio { get; set; }
        public short? DocDiasEntrega { get; set; }
        public string DocProtocolo { get; set; }
        public string DocOrientacoes { get; set; }
        public string DocExigeAnexo { get; set; }
        public string DocOrientacoes02 { get; set; }


    }



    [Table("CA_CadastroClientes")]
    public class CA_CadastroClientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CacCodigo { get; set; }
        public string CacNome { get; set; }

        public int CacTipo { get; set; }
        public string CacIdentificacao { get; set; }
        public string CacEndereco { get; set; }
        public string CacNumeroEndereco { get; set; }
        public string CacComplemento { get; set; }
        public string CacBairro { get; set; }
        public string CacCidade { get; set; }
        public string CacEstado { get; set; }
        public string CacCEP { get; set; }
        public string CacTelefone { get; set; }
        public string CacCelular { get; set; }
        public string CacEmail { get; set; }

        public DateTime? CacDataCadastro { get; set; }
        public DateTime? CacDataAlteracao { get; set; }
        public string CacUsuarioAlteracao { get; set; }
        public string CacUsuarioCadastro { get; set; }

        public string CacAtendente { get; set; }

        public DateTime? CacDataNascimento { get; set; }

        public string CacConcorrente { get; set; }

        public short? CacquantidadeAprendiz { get; set; }

        public double? CacValorTaxa { get; set; }

        public short? CacStatus { get; set; }

        public DateTime? CacVencimentoContratoAtual { get; set; }
        public double? CacValorEcomonia { get; set; }

        public string CacResponsavelRH { get; set; }
        
        
            
        
    }


    [Table("CA_Contatos")]
    public class CA_Contatos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CocID { get; set; }
        public int CocCliente { get; set; }
        public int CocTipo { get; set; }
        public DateTime? CocDataContato { get; set; }
        public string CocUsuarioContato { get; set; }
        public string CocRespContato { get; set; }
        public string CocDescricaocontato { get; set; }
        public int CocCodigoFechamento { get; set; }

        public DateTime? CocDatafechamento { get; set; }
        public string CocResultadoContato { get; set; }

        public DateTime? CocPrevisaoRetorno { get; set; }
        

    }


    [Table("CA_TiposContatos")]
    public class TipoContatos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Tco_Codigo { get; set; }
        [MaxLength(80, ErrorMessage = "Tamanho maximo = 80")]
        public string Tco_Descricao { get; set; }
    }

    [Table("CA_StatusCliente")]
    public class StatusCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short? StcCodigo { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho maximo = 50")]
        public string StcDescricao { get; set; }
    }


    [Table("CA_fechamentosContatos")]
    public class FechamentoContato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FechCodigo { get; set; }
        [MaxLength(80, ErrorMessage = "Tamanho maximo = 80")]
        public string FechDescricao { get; set; }
    }
}
