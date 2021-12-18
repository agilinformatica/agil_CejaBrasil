using System;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using ProtocoloAgil.Base;

namespace ProtocoloAgil.pages
{
    public partial class ErrorPage : Page
    {
        private bool Enviado;
        protected void Page_Load(object sender, EventArgs e)
        {
              EnviaComprovante();
        }

        private void EnviaComprovante()
        {
            var codigo = Criptografia.Decrypt(Request.QueryString["Erro"], GetConfig.Key());
            var trace = Criptografia.Decrypt(Request.QueryString["Text01"], GetConfig.Key());
            var messageText = Criptografia.Decrypt(Request.QueryString["Text02"], GetConfig.Key());

            if (codigo == "000144" || codigo == "000005" || codigo == "000008" || codigo == "000001")
            {
                LBInfo.Text = "Aguarde alguns minutos e tente novamente.  Clique abaixo para voltar à tela inicial:";
                return;
            }

            if (codigo == "000000")
            {
                LB_title.Text = "Sua sessão expirou.";
                LBInfo.Text = "Inicie uma nova sessão realizando o login na tela de acesso.  Clique abaixo para ser redirecionado:";
                return;
            }

            const string email = "comercial@agilsistemas.com";
            string nome = Session["codigo"] == null ? "Indefinido" : Session["codigo"].ToString();


            var cliente = new SmtpClient("mail.agilsistemas.com", 587);
            var remetente = new MailAddress("comercial@agilsistemas.com", "Suporte Programa PET - Jovem Aprendiz");
            var credenciais = new NetworkCredential("eli@agilsistemas.com", "mestre3415");


            //var cliente = new SmtpClient("smtp.mestreagilweb.com.br", 25);
            //var remetente = new MailAddress("eli@mestreagilweb.com.br", "Suporte Mestre Ágil");
            var destinatario = new MailAddress(email, nome);
          
            var mensagem = new MailMessage(remetente, destinatario)
            {
                IsBodyHtml = true,
                Body =
                    "<html><body> <div style='font-name:Calibri;font-size:12pt;'> Um erro foi disparado durante a sessão do usuário " + nome +
                    ",<br/>" +
                    "Código do erro: " + codigo + "." +
                    "<br/><br/> Sistema : " + HttpContext.Current.Request.Url.Host + "." +
                    "<br/><br/>" +
                      "Mensagem do Erro :" + (messageText.Equals(string.Empty) ? "indefinido" : messageText) + "." +
                     "<br/><br/>" +
                     "Trace :" + (trace.Equals(string.Empty) ? "indefinido" : trace) + "." +
                     "<br/><br/>" +
                    "Atenciosamente,<br/>" +
                    " &nbsp;&nbsp; Sistema de Notificação de Falhas." +
                     "<p>" +
                     "Esta é uma mensagem automática. <strong>Por favor, não responda.</strong></div></body></html>",
                Subject = "Mestre Agil Web - Notificação de Falha."
                
            };
            mensagem.CC.Add(new MailAddress("suporte@agilsistemas.com", "Suporte"));
            //var credenciais = new NetworkCredential("eli@mestreagilweb.com.br", "mestre3415");
                cliente.Credentials = credenciais;

                try
                {
                    if(!Enviado)
                    {
                        cliente.Send(mensagem);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('Falha ao executar comando. Email enviado à divisão de suporte.')", true);
                        Enviado = true;
                    }
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                        "alert('Exceção:" + erro.Message + "')", true);
                }
            }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            var codigo = Criptografia.Decrypt(Request.QueryString["Erro"], GetConfig.Key());
            if (codigo == "000000")
            {
                var messageText = Criptografia.Decrypt(Request.QueryString["Text02"], GetConfig.Key());
                Response.Redirect(messageText, false);
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
    }
}