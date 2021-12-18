using System;
using System.Web.UI;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;

namespace ProtocoloAgil.pages
{
    public partial class Inicial : Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            string tipo = "";
            if (Session["tipo"] != null)
                tipo = Session["tipo"].ToString();

            switch (tipo)
            {
                case "Aluno":
                    Session["CurrentPage"] = "secretariaalunos";
                    MasterPageFile = "~/MaAluno.Master";
                    break;
                case "Interno":
                      Session["CurrentPage"] = "configuracoes";
                      MasterPageFile = "~/MPusers.Master";
                    break;
                case "Educador":
                    Session["CurrentPage"] = "geralprofessores";
                    MasterPageFile = "~/MaEducador.Master";
                    break;
                case "Parceiro":
                    Session["CurrentPage"] = "aprendiz";
                    MasterPageFile = "~/MaParceiro.Master";
                    break;
                default:
                    Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (DateTime.Now.Hour < 12)
            {
                LBsaudacao.Text = "Bom dia, " + Session["Nome"] + ".";
            }
            else if (DateTime.Now.Hour < 18)
            {
                LBsaudacao.Text = "Boa Tarde, " + Session["Nome"]+ ".";
            }
            else
            {
                LBsaudacao.Text = "Boa Noite, " + Session["Nome"] + ".";
            }

            if(Session["tipo"].ToString().Equals("Aluno"))
            {
                using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
                {
                    var dados = repository.Find(int.Parse(Session["matricula"].ToString()));

                    if(dados.Apr_ValidadeMensagem >= DateTime.Today)
                    {
                        lt_mensagem_aluno.Text =
                            "<div class='obs'> Mensagem Importante: </div> &nbsp; <div class='message_text'> " + dados.Apr_Mensagem +"  </div>";
                    }
                }
            }
        }
    }
}