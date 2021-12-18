using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class AlteraSenha : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["reset"].Equals("1"))
            {
                LBantiga.Visible = false;
                TBantiga.Visible = false;
                TBantiga.Text = "1";
            }
        }
        protected void BTconfirma_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = Session["codigo"] == null ? "" : Session["codigo"].ToString();
                if (TBantiga.Text.Equals(string.Empty)) throw new ArgumentException("Digite a senha antiga. ");
                if (TBsenha.Text.Equals(string.Empty)) throw new ArgumentException("Digite a senha corretamente. ");
                if (TBconf.Text.Equals(string.Empty)) throw new ArgumentException("Digite a confirmação de senha. corretamente. ");
                if (!TBsenha.Text.Equals(TBconf.Text)) throw new ArgumentException("Senhas não são iguais. ");
                if (TBsenha.Text.Equals(TBantiga.Text)) throw new ArgumentException("A nova senha não pode ser igual à antiga. ");

                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    if (Funcoes.ValidaSenha(TBsenha.Text)) throw new ArgumentException(
                            "Nova senha possui caracteres não permitidos. Crie uma senha que contenha apenas letras e números.");
                    var tipo = Criptografia.Decrypt(Request.QueryString["id"], GetConfig.Config());
                    switch (tipo)
                    {
                        case "Interno":
                            var dados = from i in bd.CA_Usuarios
                                        where i.UsuCodigo.Equals(codigo) &&
                                                  i.UsuSenha.Equals(TBantiga.Text) select i;
                            if (dados.Count() == 0 || !dados.First().UsuSenha.Equals(TBantiga.Text)) throw new ArgumentException("Senha antiga não condiz com a senha cadastrada.");

                            if (codigo.Equals(""))
                                throw new ArgumentException("Matricula do usuário não pode ser definida.");
                            using (var repository = new Repository<Usuarios>(new Context<Usuarios>()))
                            {
                                var alterado = repository.Find(codigo);
                                alterado.UsuSenha = TBsenha.Text;
                                repository.Edit(alterado);
                            }
                            Sucesso();

                            break;
                        case "Aluno":
                                var senhaantiga = from i in bd.CA_Aprendizs where i.Apr_Codigo.Equals(Funcoes.Retirasimbolo(codigo)) &&
                                                  i.Apr_senha.Equals(TBantiga.Text) select i;
                                if (senhaantiga.Count() == 0 || !senhaantiga.First().Apr_senha.Equals(TBantiga.Text)) throw new ArgumentException("Senha antiga não condiz com a senha cadastrada.");

                                if (codigo.Equals("")) throw new ArgumentException("Matricula do aluno não pode ser definida.");
                                using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
                                {
                                    var alterado = repository.Find(int.Parse(codigo));
                                    alterado.Apr_senha = TBsenha.Text;
                                    repository.Edit(alterado);
                                }
                                Sucesso();
                            break;

                        case "Educador":
                            var senhaEducador = from i in bd.CA_Educadores where i.EducCodigo.Equals(Funcoes.Retirasimbolo(codigo)) &&
                                                i.EducSenha.Equals(TBantiga.Text)
                                                select i;
                            if (senhaEducador.Count() == 0 || !senhaEducador.First().EducSenha.Equals(TBantiga.Text)) throw new ArgumentException("Senha antiga não condiz com a senha cadastrada.");

                            if (codigo.Equals("")) throw new ArgumentException("Matricula do aluno não pode ser definida.");
                            using (var repository = new Repository<Educadores>(new Context<Educadores>()))
                            {
                                var alterado = repository.Find(int.Parse(codigo));
                                alterado.EducSenha = TBsenha.Text;
                                repository.Edit(alterado);
                            }
                            Sucesso();
                            break;

                        case "Parceiro":
                            var senhaParceiro = from i in bd.CA_Parceiros where i.ParCodigo.Equals(Funcoes.Retirasimbolo(codigo)) &&
                                                    i.ParSenha.Equals(TBantiga.Text) select i;
                            if (senhaParceiro.Count() == 0 || !senhaParceiro.First().ParSenha.Equals(TBantiga.Text)) throw new ArgumentException("Senha antiga não condiz com a senha cadastrada.");

                            if (codigo.Equals("")) throw new ArgumentException("Matricula do aluno não pode ser definida.");
                            using (var repository = new Repository<Parceiros>(new Context<Parceiros>()))
                            {
                                var alterado = repository.Find(int.Parse(codigo));
                                alterado.ParSenha = TBsenha.Text;
                                repository.Edit(alterado);
                            }
                            Sucesso();
                            break;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Alert.Show(ex.Message);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000009", ex);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Default.aspx", false);
        }

        private void Sucesso()
        {
            Alert.Show("Senha alterada com sucesso!");
        }
    }
}