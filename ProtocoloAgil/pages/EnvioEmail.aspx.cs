using System;
using System.Linq;
using System.Web.UI;
using ProtocoloAgil.Base;
using System.Net;
using System.Net.Mail;
using System.IO;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class EnvioEmail : Page
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
                case "Parceiro":
                    Session["CurrentPage"] = "aprendiz";
                    MasterPageFile = "~/MaParceiro.Master";
                    break;
                default: Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
                    break;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Envio de E-mail - Programa Jovem Aprendiz";
            Session["CurrentPage"] = "secretariaalunos";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterPostBackControl(btn_Enviar);
            }

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var tipo = Session["tipo"];
                if (tipo.Equals("Aluno"))
                {
                    btn_Enviar.Click += btn_Envia_Aprendiz_Click;
                    var dados = (from i in bd.CA_Aprendizs
                                 join p in bd.CA_AlocacaoAprendizs on i.Apr_Codigo equals p.ALAAprendiz
                                 join n in bd.CA_Turmas on p.ALATurma equals n.TurCodigo
                                 join m in bd.CA_Unidades on n.TurUnidade equals m.UniCodigo
                                 where i.Apr_Codigo.Equals(int.Parse(Session["matricula"].ToString()))
                                 select new { i.Apr_Email, i.Apr_Nome, m.UniEmailPadraoEnvio, m.UniNome }).First();

                    if (!Funcoes.ValidaEmail(dados.UniEmailPadraoEnvio))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                                "alert('Sua unidade não dispõe desta forma de contato.')", true);
                        btn_Enviar.Enabled = false;
                        return;
                    }

                    var infoAprendiz = new[] { dados.Apr_Email, dados.Apr_Nome, dados.UniEmailPadraoEnvio, dados.UniNome };
                    ViewState.Add("InfoAprendiz", infoAprendiz);
                }
                else
                {
                    var dados = (from i in bd.CA_Parceiros where i.ParCodigo.Equals(int.Parse(Session["codigo"].ToString()))
                                 select new { i.ParEmail, i.ParNomeFantasia }).First();

                    var infoParceiro = new[] { dados.ParEmail, dados.ParNomeFantasia};
                    ViewState.Add("InfoAprendiz", infoParceiro);
                    btn_Enviar.Click += btn_Envia_Parceiro_Click;
                }
            }
        }

        protected void btn_Envia_Aprendiz_Click(object sender, EventArgs e)
        {
            var messageText = ftb_texto.Text;
            var dados = (string[])ViewState["InfoAprendiz"];

            var cliente = new SmtpClient("mail.agilsistemas.com", 587);
            var remetente = new MailAddress("comercial@agilsistemas.com", "Suporte Programa PET -Jovem Aprendiz");
            var credenciais = new NetworkCredential("eli@agilsistemas.com", "mestre3415");
            var destinatario = new MailAddress(dados[2], dados[3]);
            var mensagem = new MailMessage(remetente, destinatario)
            {
                IsBodyHtml = true,
                Body = "<html><body><span style='font: 12px #5E5E5E Arial, Helvetica, sans-serif; font-weight: bold;'> Aluno: " + dados[1] + "</span><br/> " +
                    "<span style='font: 12px #5E5E5E Arial, Helvetica, sans-serif; font-weight: bold;'> E-mail: " + dados[0] + " </span><br/> <br/><br/> " +
                    messageText + "</body> </html>",
                Subject = TB_assunto.Text
            };

            if (fupArquivo.HasFile)
            {
                var filePath = Server.MapPath(@"/files/Temp/");
                var dir = new DirectoryInfo(filePath);
                if (!dir.Exists)
                    dir.Create();
                fupArquivo.SaveAs(filePath + fupArquivo.FileName);
                var arquivo = new Attachment(filePath + fupArquivo.FileName);
                mensagem.Attachments.Add(arquivo);
            }
            cliente.Credentials = credenciais;

            try
            {
                cliente.Send(mensagem);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert(' Email enviado com Sucesso.')", true);
            }
            catch (Exception erro)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('Exceção:" + erro.Message + "')", true);
            }
        }


        protected void btn_Envia_Parceiro_Click(object sender, EventArgs e)
        {
            var messageText = ftb_texto.Text;
            var dados = (string[])ViewState["InfoAprendiz"];
            var destinatario = new MailAddress("secretaria@cvr.org.br", "Acompanhamento Apendiz");

            var cliente = new SmtpClient("mail.agilsistemas.com", 587);
            var remetente = new MailAddress("comercial@agilsistemas.com", "Suporte Programa PET -Jovem Aprendiz");
            var credenciais = new NetworkCredential("eli@agilsistemas.com", "mestre3415");

            var mensagem = new MailMessage(remetente, destinatario)
            {
                IsBodyHtml = true,
                Body =
                    "<html><body><span style='font: 12px #5E5E5E Arial, Helvetica, sans-serif; font-weight: bold;'> Parceiro: " + dados[1] + "</span><br/> " +
                    "<span style='font: 12px #5E5E5E Arial, Helvetica, sans-serif; font-weight: bold;'> E-mail: " + dados[0] + " </span><br/> <br/><br/> " +
                    messageText + "</body> </html>",
                Subject = TB_assunto.Text
            };

            if (fupArquivo.HasFile)
            {
                var filePath = Server.MapPath(@"/files/Temp/");
                var dir = new DirectoryInfo(filePath);
                if (!dir.Exists)
                    dir.Create();
                fupArquivo.SaveAs(filePath + fupArquivo.FileName);
                var arquivo = new Attachment(filePath + fupArquivo.FileName);
                mensagem.Attachments.Add(arquivo);
            }

            cliente.Credentials = credenciais;

            try
            {
                cliente.Send(mensagem);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert(' Email enviado com Sucesso.')", true);
            }
            catch (Exception erro)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('Exceção:" + erro.Message + "')", true);
            }
        }
    }
}