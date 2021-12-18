using System;
using ProtocoloAgil.Base;

namespace ProtocoloAgil.pages
{
    public partial class MeusDados : System.Web.UI.Page
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
                    MasterPageFile = "~/MaAluno.master";
                    Session["CurrentPage"] = "secretariaalunos";
                    break;
                case "Parceiro":
                    MasterPageFile = "~/MMaParceiro.Master";
                    Session["CurrentPage"] = "configuracoes";
                    break;
                case "Educador":
                    MasterPageFile = "~/MaEducador.Master";
                    Session["CurrentPage"] = "geralprofessores";
                    break;
                default:
                    Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
                    break;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Comando"] = "Alterar";


            switch (Session["tipo"].ToString())
            {
                case "Aluno":
                    IFrame1.Attributes["src"] = "CadastroAprendiz.aspx";
                    Session["enable_Save"] = "Aluno"; 
                     IFrame1.Attributes["height"] = "700px";
                    //lb_breadcrumb.Text = "Secretaria >";
                    break;
                case "Parceiro":
                    IFrame1.Attributes["src"] = "DadosProfessores.aspx";
                    break;
                case "Educador":
                    IFrame1.Attributes["src"] = "DadosProfessores.aspx";
                    IFrame1.Attributes["height"] = "665px";
                    //lb_breadcrumb.Text = "Geral >";
                    break;
            }
        }
    }
}