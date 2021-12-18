using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace ProtocoloAgil.pages
{
    public partial class AniversariantesCliente : Page
    {

        void Page_PreInit(Object sender, EventArgs e)
        {
            string tipo = "";
            if (Session["tipo"] != null)
                tipo = Session["tipo"].ToString();

            switch (tipo)
            {
                case "Interno":
                    Session["CurrentPage"] = "aprendiz";
                    MasterPageFile = "~/MPusers.Master";
                    break;
                case "Educador":
                    Session["CurrentPage"] = "geralprofessores";
                    MasterPageFile = "~/MaEducador.Master";
                    break;

                default:
                    Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
                    break;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            var scriptManager = ScriptManager.GetCurrent(Page);
            Session["CurrentPage"] = "configuracoes";
           // if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if (!IsPostBack)
            {
                //BindCursos();
                MultiView1.ActiveViewIndex = 0;
                ViewState.Add("list", "");
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }


     

        protected void BindTurmas(string curso, DropDownList dropdown)
        {
            using (var reposytory = new Repository<Turma>(new Context<Turma>()))
            {
                var list = new List<Turma>();
                list.AddRange(reposytory.All().Where(x => x.TurCurso.Equals(curso)).OrderBy(p => p.TurNome));
                dropdown.Items.Clear();
                dropdown.DataSource = list.ToList();
                dropdown.DataBind();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }


      

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {

            if (DDmeses.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('Selecione o mês para pesquisa.');", true);
                return;
            }

            

            // CARREGAR GRID SEM LINQ
            var sql = "select * from CA_CadastroClientes where DATEPART(MONTH,CacDataNascimento)  = '" + int.Parse(DDmeses.SelectedValue) + "';";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var dados = ViewState["list"].ToString();
            var dSescola = new SqlDataSource
            {
                ID = "ODSalunos",
                ConnectionString = GetConfig.Config(),
                SelectCommand = dados,
                EnableViewState = true
            };
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = dSescola;
            GridView1.DataBind();

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            //Session["PRMT_Curso"] = DDcursoDiario.SelectedValue;
            //Session["PRMT_Turma"] = DDturma_pesquisa.SelectedValue;
            //Session["MesRef"] = DDmeses.SelectedValue.Equals(string.Empty) ? "0" : DDmeses.SelectedValue;
         
            MultiView1.ActiveViewIndex = 1;
        }

        protected void texto_Click(object sender, EventArgs e)
        {
            var filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            var cn = new Conexao();
            var sql =  ViewState["list"].ToString();
            var dr = cn.Consultar(sql);
            try
            {
                while (dr.Read())
                {
                    var linha = string.Format("{0:dd/MM}", dr["CacDataNascimento"]) + "; " + dr["CacCodigo"] + "; " + dr["Apr_Nome"]
                        + "; " + dr["TurNome"] + "; " + dr["CurDescricao"];
                    write.Escreve(linha);
                }
                // download do arquivo de texto
                string fileName = filePath + @"/temp.txt";
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=Lista de Aniversariantes.txt");
                Response.WriteFile(fileName);
                Response.Flush();
                Response.Close();
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
            }
        }

        protected void btnEnviarCartao_Click(object sender, ImageClickEventArgs e)
        {
            var email = "";
            var nome = "";
            var button = (ImageButton)sender;
            var codigo = button.CommandArgument;
            var sql = "Select CacEmail, CacNome from CA_CadastroClientes where CacCodigo = '" + codigo + "'";
            var con = new Conexao();
            var result = con.Consultar(sql);
            int i = 0;
            while (result.Read())
            {
                i++;
                email = result["CacEmail"].ToString();
                nome = result["CacNome"].ToString();
            }

            if (i == 0 || email.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('Cliente NÃO possui email para envio de cartão de aniversário.');", true);
                return;
            }


            SendEmail(email, nome);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                 "alert('Um e-mail com o cartão de aniversário foi enviado para o cliente.');javascript:CreateWheel('no');", true);

        }


        public void SendEmail(string email, string nome)
        {

            string body = @"<html>  <body> <table width=""100%""> <tr> <td style=""font-style:arial; color:maroon; font-weight:bold""> Olá "+nome+" <br> <img src=cid:myImageID>  </td> </tr> </table> </body> </html>";


            var cliente = new SmtpClient("mail.agilsistemas.com", 587);
            var remetente = new MailAddress("comercial@agilsistemas.com", "Cartão de Aniversário");
            var destinatario = new MailAddress(email, nome);

            var mensagem = new MailMessage(remetente, destinatario)
            {
                IsBodyHtml = true,
               // Body = texto,
                Subject = "Aprendiz Ágil - Cartão Aniversário."
            };

            var credenciais = new NetworkCredential("eli@agilsistemas.com", "mestre3415");
            cliente.Credentials = credenciais;

            mensagem.IsBodyHtml = true;

          //  string body = "";
            //create Alrternative HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            //Add Image
            //LinkedResource theEmailImage = new LinkedResource("C:\\Domains\\mestreagil.com.br\\ceducbh.agilsist.com.br\\images\\CartaoAniversarioCerto.jpg");
            LinkedResource theEmailImage = new LinkedResource("D:\\CartaoAniversarioCerto.jpg");

            theEmailImage.ContentId = "myImageID";

            //Add the Image to the Alternate view
            htmlView.LinkedResources.Add(theEmailImage);

            //Add view to the Email Message
            mensagem.AlternateViews.Add(htmlView);

            cliente.Send(mensagem);
        }
    }
}