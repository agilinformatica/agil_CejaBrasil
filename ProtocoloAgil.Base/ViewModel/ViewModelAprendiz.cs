using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MenorAprendizWeb.Base.ViewModel
{
    public class ViewModelAprendiz
    {
        public int Apr_Codigo { get; set; }
        public string Apr_Nome { get; set; }
        public DateTime? Apr_DataDeNascimento { get; set; }
        public string Apr_Sexo { get; set; }
        public string AprEstadoCivil { get; set; }
        public string Apr_Endereço { get; set; }
        public string Apr_Complemento { get; set; }
        public string Apr_Bairro { get; set; }
        public string Apr_Cidade { get; set; }
        public string Apr_UF { get; set; }
        public string Apr_CEP { get; set; }
        public string Apr_Telefone { get; set; }
        public string Apr_Celular { get; set; }
        public string Apr_Email { get; set; }
        public string Apr_NomeMae { get; set; }
        public string Apr_NomePai { get; set; }
        public string Apr_Naturalidade { get; set; }
        public string Apr_UF_Nat { get; set; }
        public string Apr_CarteiraDeIdentidade { get; set; }
        public DateTime? Apr_DataEmissão_Ident { get; set; }
        public string Apr_UF_Ident { get; set; }
        public string Apr_OrgaoEmissor_Ident { get; set; }
        public string Apr_CPF { get; set; }
        public string Apr_CarteiraDeTrabalho { get; set; }
        public string Apr_Serie_Cartrab { get; set; }
        public DateTime? Apr_DataEmissão_CartTrab { get; set; }
        public string Apr_UF_CartTrab { get; set; }
        public string Apr_PIS { get; set; }
        public string Apr_PNE { get; set; }
        public int? AprCodEscola { get; set; }
        public DateTime? Apr_Escola_HInicio { get; set; }
        public DateTime? Apr_Escola_HTermino { get; set; }
        public DateTime? Apr_DataCadastro { get; set; }
        public int Apr_Situacao { get; set; }
        public string Apr_Responsavel { get; set; }
        public string Apr_Resp_CPF { get; set; }
        public string Apr_Resp_EstadoCivil { get; set; }
        public string Apr_Resp_GrauInstrucao { get; set; }
        public int? Apr_Resp_Profissao { get; set; }
        public string Apr_Telefone_Resp { get; set; }
        public string Apr_Celular_Resp { get; set; }
        public string Apr_Telefone_Contato { get; set; }
        public string Apr_Tipo_Contato { get; set; }
        public string Apr_Observacoes { get; set; }
        public string Apr_Resp_Observacoes { get; set; }
        public string Apr_Nacionalidade { get; set; }
        public string Apr_NumeroEndereco { get; set; }
        public int? Apr_Escolaridade { get; set; }
        public string Apr_TranspLinha01 { get; set; }
        public string Apr_TranspLinha02 { get; set; }
        public string Apr_TranspLinha03 { get; set; }
        public string Apr_beneficio { get; set; }
        public float? Apr_BolsaFamilia { get; set; }
        public float? Apr_pensao { get; set; }
        public float? Apr_outros { get; set; }
        public string Apr_SituacaoResidencia { get; set; }
        public float? Apr_aluguel { get; set; }
        public string Apr_CarteiraAssinada { get; set; }
        public string Apr_Empresa { get; set; }
        public string Apr_Cargo { get; set; }
        public string Apr_TempoPermanencia { get; set; }
        public string Apr_UsaMedicamento { get; set; }
        public string Apr_NomeMedicamento { get; set; }
        public string Apr_TipoMedicamento { get; set; }
        public string Apr_Alergia { get; set; }
        public string Apr_Doenca { get; set; }
        public string Apr_NomeDoenca { get; set; }
        public string Apr_TipoAlergia { get; set; }
        public string Apr_senha { get; set; }
        public string AprBanco { get; set; }
        public string AprTipoConta { get; set; }
        public string AprAgencia { get; set; }
        public string AprContaBancaria { get; set; }
        public string Apr_CertReservista { get; set; }
        public string Apr_NumeroMTB { get; set; }
        public string Apr_Tutor { get; set; }
        public int? Apr_AreaAtuacao { get; set; }
        public string Apr_Mensagem { get; set; }
        public DateTime? Apr_ValidadeMensagem { get; set; }
        public string Apr_UsuarioCadastro { get; set; }
        public DateTime? Apr_UsuarioDataCadastro { get; set; }
        public string Apr_UsuarioAlteracao { get; set; }
        public DateTime? Apr_UsuarioDataAlteracao { get; set; }
        public double? Apr_ValorTransp01 { get; set; }
        public double? Apr_ValorTransp02 { get; set; }
        public double? Apr_ValorTransp03 { get; set; }
        public short? Apr_Quantvt01 { get; set; }
        public short? Apr_Quantvt02 { get; set; }
        public short? Apr_Quantvt03 { get; set; }
        public string Apr_HabilitaPesquisa { get; set; }
        public DateTime? Apr_InicioAprendizagem { get; set; }
        public DateTime? Apr_FimAprendizagem { get; set; }
        public DateTime? Apr_PrevFimAprendizagem { get; set; }
        public int? Apr_Resp_Parentesco { get; set; }
        public short? Apr_numeroFamiliares { get; set; }
        public string Apr_RecebeBeneficio { get; set; }
        public int? Apr_Turma { get; set; }
    }
}
