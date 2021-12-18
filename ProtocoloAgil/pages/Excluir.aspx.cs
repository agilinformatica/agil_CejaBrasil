using System;
using System.Web.UI;
using ProtocoloAgil.Base;

namespace ProtocoloAgil.pages
{
    public partial class Excluir : Page
    {

        void Page_PreInit(Object sender, EventArgs e)
        {
            string tipo = "";
            if (Session["tipo"] != null)
            {
                tipo = Session["tipo"].ToString();
            }

            switch (tipo)
            {
                case "Aluno":
                    MasterPageFile = "~/Senacaluno.Master";
                    break;
                case "Interno":
                    MasterPageFile = "~/MPusers.Master";
                    break;
                default:
                    MasterPageFile = "~/MPusers.Master";
                    break;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            LBcodigo.Text = Session["Alteracodigo"].ToString();
            Session["acs"] = Request.QueryString["acs"] ?? "";
        }

        protected void BTconf_Click(object sender, EventArgs e)
        {
            var sql = string.Empty;
            switch (Session["Page"].ToString())
            {
                case "RamoAtividade":
                    sql = "DELETE FROM CA_RamosAtividades WHERE RatCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "StatusRequisicao":
                    sql = "DELETE FROM dbo.MA_StatusRequisicao WHERE SitCodigo ='" + Session["Alteracodigo"] + "' ";
                    break;
                case "Documentos":
                    sql = "DELETE FROM MA_Documentos WHERE DocCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "Usuarios":
                    sql = "DELETE FROM CA_Usuarios WHERE UsuCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "Ocorrencias":
                    sql = "DELETE FROM dbo.CA_Ocorrencias WHERE OcoCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "SituacaoAprendiz":
                    sql = "DELETE FROM dbo.CA_SituacaoAprendiz WHERE StaCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "GrauParentesco":
                    sql = "DELETE FROM CA_GrauParentesco WHERE GpaCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "CadastroDisciplina":
                    sql = "DELETE FROM CA_Disciplinas WHERE DisCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "GrauEscolaridade":
                    sql = "DELETE FROM CA_GrauEscolaridade WHERE GreCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "CadastroTurma":
                    sql = "DELETE FROM CA_Turmas WHERE TurCodigo ='" + Session["Alteracodigo"] + "'";
                    break;
                case "PlanoCurricular":
                    sql = "DELETE FROM CA_PlanoCurricular where PlcCodigo ='" + Session["Alteracodigo"] + "' " +
                          "AND PlcCurso = '" + Session["AlteraCurso"] + "' AND PlcDisciplina = '" + Session["AlteraDisciplina"] + "'  ";
                    break;
            }

            var cn = new Conexao();
            try
            {
                cn.Alterar(sql);
                LBinfo.Text = "Remoção realizada com sucesso.";
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000072", ex);
            }
        }

        protected void BTcancel_Click(object sender, EventArgs e)
        {
            switch (Session["Page"].ToString())
            {
                case "RamoAtividade": Response.Redirect("CadastroRamoAtividade.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "StatusRequisicao": Response.Redirect("StatusRequisicao.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "Documentos": Response.Redirect("CadastroDocumentos.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "Usuarios": Response.Redirect("MultiviewUsuarios.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "Ocorrencias": Response.Redirect("Ocorrencias.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "SituacaoAprendiz": Response.Redirect("SituacaoAprendiz.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "GrauParentesco": Response.Redirect("GrauParentesco.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "CadastroDisciplina": Response.Redirect("CadastroDisciplina.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "GrauEscolaridade": Response.Redirect("GrauEscolaridade.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "CadastroTurma": Response.Redirect("CadastroTurma.aspx?acs=" + Request.QueryString["acs"], false); break;
                case "PlanoCurricular": Response.Redirect("PlanoCurricular.aspx?acs=" + Request.QueryString["acs"], false); break;
            }
        }
    }
}