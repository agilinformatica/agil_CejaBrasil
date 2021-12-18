using System;
using System.Linq;
using System.Web.UI;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil
{
    public partial class Default : Page
    {
        private const string Url = "~/pages/Inicial.aspx";
        readonly DC_ProtocoloAgilDataContext bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.Browser.Equals("IE") && Request.Browser.Version.Equals("8.0"))
                Response.Redirect("BrowserNaoSuportado.aspx", false);
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(login);
            if (!IsPostBack)
            {
                Session.Clear();
                Session["matricula"] = "";
                Session["reset"] = "0";
                Session["tipo"] = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DD_perfil.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um perfil para realizar o login.");
                if (TBnome.Text.Equals(string.Empty) && TBnome_matricula.Text.Equals(string.Empty)) throw new ArgumentException("Informe o Login do usuário.");
                if (TBsenha.Text.Equals(string.Empty)) throw new ArgumentException("Selecione um perfil para realizar o login.");

                switch (DD_perfil.SelectedValue)
                {
                    case "U":
                        var interno = from i in bd.CA_Usuarios
                                      where i.UsuCodigo.Equals(TBnome.Text)
                                          && i.UsuSenha.Equals(TBsenha.Text)
                                      select i;
                        if (interno.Count() > 0)
                        {
                            var usuario = interno.First();
                            Session["Nome"] = usuario.UsuNome;
                            Session["codigo"] = usuario.UsuCodigo;
                            Session["CodInterno"] = usuario.UsuCodigo;
                            Session["tipo"] = "Interno";
                            Session["tipoUsuarioLogado"] = usuario.UsuTipo;
                            string senha = usuario.UsuSenha;

                            if (senha.Equals("1"))
                            {
                                var alteraSenha = "~/pages/AlteraSenha.aspx?id=" + Criptografia.Encrypt("Interno", GetConfig.Config());
                                Session["reset"] = "1";
                                Response.Redirect(alteraSenha, false);
                            }
                            else
                            {
                                Response.Redirect(Url, false);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('Login ou senha inválidos.')", true);
                            TBnome.Text = "";
                            TBsenha.Text = "";
                            TBnome.Focus();
                        }

                        break;
                    case "A":
                        var aluno = from i in bd.CA_Aprendizs
                                    where i.Apr_Codigo.Equals(TBnome_matricula.Text)
                                        && i.Apr_senha.Equals(TBsenha.Text)
                                    select i;
                        if (aluno.Count() > 0)
                        {
                            var usuario = aluno.First();
                            Session["Nome"] = usuario.Apr_Nome;
                            Session["codigo"] = usuario.Apr_Codigo;
                            Session["matricula"] = usuario.Apr_Codigo;
                            Session["tipo"] = "Aluno";
                            string senha = usuario.Apr_senha;

                            if (senha.Equals("1"))
                            {
                                var alteraSenha = "~/pages/AlteraSenha.aspx?id=" + Criptografia.Encrypt("Aluno", GetConfig.Config());
                                Session["reset"] = "1";
                                Response.Redirect(alteraSenha, false);
                            }
                            else
                            {
                                Response.Redirect(Url, false);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('Login ou senha inválidos.')", true);
                            TBnome_matricula.Text = "";
                            TBsenha.Text = "";
                            TBnome.Focus();
                        }
                        break;
                    case "E":
                        var professor = from i in bd.CA_Educadores
                                        where i.EducCodigo.Equals(TBnome_matricula.Text)
                                            && i.EducSenha.Equals(TBsenha.Text)
                                        select i;
                        if (professor.Count() > 0)
                        {
                            var usuario = professor.First();
                            if (usuario.EducSituacao == "D")
                            {
                                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('Usuário bloqueado. Entre em cotato com a secretaria para mais informações.')", true);
                                return;
                            }
                            Session["Nome"] = usuario.EducNome;
                            Session["codigo"] = usuario.EducCodigo;
                            Session["matricula"] = usuario.EducCodigo;
                            Session["tipo"] = "Educador";
                            string senha = usuario.EducSenha;

                            if (senha.Equals("1"))
                            {
                                var alteraSenha = "~/pages/AlteraSenha.aspx?id=" + Criptografia.Encrypt("Educador", GetConfig.Config());
                                Session["reset"] = "1";
                                Response.Redirect(alteraSenha, false);
                            }
                            else
                            {
                                Response.Redirect(Url, false);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('Login ou senha inválidos.')", true);
                            TBnome_matricula.Text = "";
                            TBsenha.Text = "";
                            TBnome.Focus();
                        }
                        break;
                    case "P":
                        var parceiro = from i in bd.CA_Parceiros
                                       where i.ParCodigo.Equals(TBnome_matricula.Text)
                                           && i.ParSenha.Equals(TBsenha.Text)
                                       select i;
                        if (parceiro.Count() > 0)
                        {
                            var usuario = parceiro.First();
                            Session["Nome"] = usuario.ParNomeFantasia;
                            Session["codigo"] = usuario.ParCodigo;
                            Session["tipo"] = "Parceiro";
                            string senha = usuario.ParSenha;

                            if (senha.Equals("1"))
                            {
                                var alteraSenha = "~/pages/AlteraSenha.aspx?id=" + Criptografia.Encrypt("Parceiro", GetConfig.Config());
                                Session["reset"] = "1";
                                Response.Redirect(alteraSenha, false);
                            }
                            else
                            {
                                Response.Redirect(Url, false);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('ERRO - Login ou senha inválidos.')", true);
                            TBnome_matricula.Text = "";
                            TBsenha.Text = "";
                            TBnome.Focus();
                        }
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(),
                                          "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000002", ex);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (DD_perfil.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                        "alert('Selecione o perfil do usuário.')", true);
                return;
            }
            if (TBnome.Text.Equals(string.Empty) && TBnome_matricula.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                        "alert('Digite o login do usuário.')", true);
                return;
            }

            
            if (DD_perfil.SelectedValue.Equals("U"))
            {
                Session["codigo"] = TBnome.Text;
                Response.Redirect("~/pages/AlteraSenha.aspx?id=" + Criptografia.Encrypt("Interno", GetConfig.Config()));
            }else if (DD_perfil.SelectedValue.Equals("A"))
            {
                Session["codigo"] = TBnome_matricula.Text;
                Response.Redirect("~/pages/AlteraSenha.aspx?id=" + Criptografia.Encrypt("Aluno", GetConfig.Config()));
            }else if (DD_perfil.SelectedValue.Equals("E"))
            {
                Session["codigo"] = TBnome_matricula.Text;
                Response.Redirect("~/pages/AlteraSenha.aspx?id=" + Criptografia.Encrypt("Educador", GetConfig.Config()));
            }else if (DD_perfil.SelectedValue.Equals("P"))
            {
                Session["codigo"] = TBnome_matricula.Text;
                Response.Redirect("~/pages/AlteraSenha.aspx?id=" + Criptografia.Encrypt("Parceiro", GetConfig.Config()));
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // envia para o usuário suas informações de login e senha. O usuário deve pelo menos saber o login e  possuir e-mail cadastrado.
            try
            {
                string dest;
                string nome;
                string matricula;
                string senha;

                switch (DD_perfil.SelectedValue)
                {
                    case "U":
                        var usuario = (from i in bd.CA_Usuarios where i.UsuCodigo.Equals(TBnome.Text) select i).First();
                        dest = usuario.UsuEmail;
                        nome = usuario.UsuNome;
                        matricula = usuario.UsuCodigo;
                        senha = usuario.UsuSenha;
                        break;
                    case "A":
                        var aluno = (from i in bd.CA_Aprendizs where i.Apr_Codigo.Equals(TBnome_matricula.Text) select i).First();
                        dest = aluno.Apr_Email;
                        nome = aluno.Apr_Nome;
                        matricula = aluno.Apr_Codigo.ToString();
                        senha = aluno.Apr_senha;
                        break;
                    case "E":
                        var educador = (from i in bd.CA_Educadores where i.EducCodigo.Equals(TBnome_matricula.Text) select i).First();
                        dest = educador.EducEMail;
                        nome = educador.EducNome;
                        matricula = educador.EducCodigo.ToString();
                        senha = educador.EducSenha;
                        break;
                    case "P":
                        var parceiro = (from i in bd.CA_Parceiros where i.ParCodigo.Equals(TBnome_matricula.Text) select i).First();
                        dest = parceiro.ParEmail;
                        nome = parceiro.ParNomeContato;
                        matricula = parceiro.ParCodigo.ToString();
                        senha = parceiro.ParSenha;
                        break;
                    default:
                        throw new ArgumentException("Selecione o perfil de usuário.");
                }

                if (TBnome.Text.Equals(string.Empty) && TBnome_matricula.Text.Equals(string.Empty))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                            "alert('Digite o login do usuário.');javascript:CreateWheel('no');", true);
                    return;
                }

                if (dest == null || dest.Equals(string.Empty))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                          "alert(ERRO - 'Usuário não possui e-mail cadastrado. Entre em contato com o Administrador do sistema.');javascript:CreateWheel('no');", true);
                    return;
                }

                var texto = "<html><body><div style='font-name:Calibri;font-size:12pt;'>Caro(a) " + nome + ",<br/>" +
                        "Como solicitado, enviamos ao seu email as informações pertinentes ao acesso ao sistema:" +
                        "<br/><br/>" + "Usuário: " + matricula + ".<br/>" + "Senha: " + senha + ".<br/><br/>" +
                        "Recomendamos que apague este email e mude a senha no próximo acesso.<p>" +
                        "Atenciosamente,<br/>" + " &nbsp;&nbsp;  Gestão de Menor Aprendiz Web." +
                        "<p>" +
                        "Esta é uma mensagem automática. <strong>Por favor, não reponda.</strong></div></body></html>";

                Funcoes.SendEmail(dest, nome, texto);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                     "alert('Um e-mail contendo as informações de acesso foi enviado ao usuário.');javascript:CreateWheel('no');", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000005", ex);
            }
        }

        protected void DD_perfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DD_perfil.SelectedValue)
            {
                case "A":
                case "P":
                case "E":
                    TBnome.Visible = false;
                    TBnome_matricula.Visible = true;
                    break;
                default:
                    TBnome.Visible = true;
                    TBnome_matricula.Visible = false;
                    break;
            }
            TBnome_matricula.Text = string.Empty;
            TBnome.Text = string.Empty;
        }

        protected void btnNovoUsuario_Click(object sender, EventArgs e)
        {
           // Uri myUri = new Uri("CadastroAprendizInicial.aspx?pepCod=10");
            Response.Redirect("CadastroAprendizInicial.aspx");

            //Response.Redirect("CadastroAprendizInicial.aspx?pepCod=10");
        }

        protected void btnTeste_Click(object sender, EventArgs e)
        {

            Response.Redirect("AvaliacaoOrientadorExterna.aspx?pepCodigo=643");
        }
    }
}