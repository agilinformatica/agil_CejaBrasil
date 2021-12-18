using MenorAprendizWeb.Base;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloAgil.Base
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class Funcoes
    {
        public static string TrataValor(String valor)
        {
            if (valor == null || valor.Equals("")) return "";
            int i = 0;
            while (i < valor.Length)
            {
                if (valor[i].Equals('.'))
                {
                    valor = valor.Substring(0, i) + valor.Substring(i + 1);
                }
                i++;
            }
            valor = RetirasimboloValor(valor);
            valor = valor.Replace(",", ".");
            return valor;

        }

        

        public static string ConverteData(string data)
        {
            string dia = null;
            string mes = null;
            string ano = null;


            try
            {
                dia = data.Substring(0, 2);
                mes = data.Substring(3, 2);
                ano = data.Substring(6);
            }
            catch (Exception)
            {
                Console.Write(data);
            }

            data = ano + "/" + mes + "/" + dia;

            return data;
        }


        public static string RetiraEspeciais(String aux)
        {
            if (aux == null) return "";

            aux = aux.Replace("ã", "a");
            aux = aux.Replace("õ", "o");
            aux = aux.Replace("ó", "o");
            aux = aux.Replace("ô", "o");
            aux = aux.Replace("í", "i");
            aux = aux.Replace("ê", "e");
            aux = aux.Replace("é", "e");
            aux = aux.Replace("á", "a");
            aux = aux.Replace("à", "a");
            aux = aux.Replace("â", "a");
            aux = aux.Replace("ú", "u");
            aux = aux.Replace("ü", "u");
            aux = aux.Replace("ç", "c");

            aux = aux.Replace("Ã", "A");
            aux = aux.Replace("Õ", "O");
            aux = aux.Replace("Ô", "O");
            aux = aux.Replace("Ó", "O");
            aux = aux.Replace("Í", "I");
            aux = aux.Replace("Ê", "E");
            aux = aux.Replace("É", "E");
            aux = aux.Replace("À", "A");
            aux = aux.Replace("Á", "A");
            aux = aux.Replace("Â", "A");
            aux = aux.Replace("Ú", "U");
            aux = aux.Replace("Ü", "U");
            aux = aux.Replace("Ç", "C");
            return aux;
        }

        public static String Retirasimbolo(String aux)
        {
            char[] trim = { '=', '\\', ';', '.', ':', ',', '+', '*', '(', ')', '-', '/', ' ','$','R' };
            int pos;
            while ((pos = aux.IndexOfAny(trim)) >= 0)
            {
                aux = aux.Remove(pos, 1);
            }
            return aux;
        }

        public static String RetirasimboloValor(String aux)
        {
            char[] trim = { '=', '\\', ';', '.', ':', '+', '*', '(', ')', '-', '/', ' ', '$', 'R' };
            int pos;
            while ((pos = aux.IndexOfAny(trim)) >= 0)
            {
                aux = aux.Remove(pos, 1);
            }
            return aux;
        }

        public static String RetirasimboloNomes(String aux)
        {
            if (aux == null) return string.Empty;
            char[] trim = { '=', '\\', ';', ':', '+', '*', '(', ')', '-', '/', '$',
                              '&','@','%','!','"','[',']','<','>','{','}','£','²','³','¢',
                          '¬','§','|'};
            int pos;
            while ((pos = aux.IndexOfAny(trim)) >= 0)
            {
                aux = aux.Remove(pos, 1);
            }
            return aux;
        }

        public static string FormataCep(string cep)
        {
            if (cep == null)
                return "";

            if (cep.Length < 8)
                return cep;

            var aux =  cep.Substring(0, 2)  + "." +  cep.Substring(2, 3) + "-" + cep.Substring(5);
            return aux;
        }

        public static string FormataTelefone(string tel)
        {

            if (tel == null || tel.Equals(string.Empty)) return "";
            string telefone ="";
            if ((!tel.Equals("")  && tel.Length == 10)) //telefone com ddd
                telefone = "(" + tel.Substring(0, 2) + ") " + tel.Substring(2, 4) + "-" + tel.Substring(6);
            else
            {
                if (((!tel.Equals("")  && tel.Length == 8))) //telefone 8 digitos sem ddd
                    telefone = tel.Substring(0, 4) + "-" + tel.Substring(4);
                else
                {
                    if (((!tel.Equals("")  && tel.Length == 7))) //telefone antigo 7 digitos sem ddd
                        telefone = tel.Substring(0, 3) + "-" + tel.Substring(3);
                }
            }

            return telefone;
        }

        public static string FormataTelefoneSaoPaulo(string tel)
        {

            if (tel == null || tel.Equals(string.Empty)) return "";
            string telefone = "";
            if ((!tel.Equals("") && tel.Length == 11)) //telefone com ddd
                telefone = "(" + tel.Substring(0, 2) + ") " + tel.Substring(2, 5) + "-" + tel.Substring(7);
            else
            {
                if (((!tel.Equals("") && tel.Length == 8))) //telefone 9 digitos sem ddd
                    telefone = tel.Substring(0, 5) + "-" + tel.Substring(4);
                else
                {
                    if (((!tel.Equals("") && tel.Length == 7))) //telefone antigo 7 digitos sem ddd
                        telefone = tel.Substring(0, 3) + "-" + tel.Substring(3);
                }
            }

            return telefone;
        }

        public static string FormataTelefone(object telef)
        {
            if (telef == null || telef.Equals(string.Empty)) return "";
            var type = telef.GetType();
            var tel = type.Name.Equals("DBNull") ?  string.Empty :(string) telef;
            if (tel.Equals(string.Empty)) return string.Empty;
            string telefone = "";
            if ((!tel.Equals("") && !tel.Equals(null) && tel.Length == 10)) //telefone com ddd
                telefone = "(" + tel.Substring(0, 2) + ") " + tel.Substring(2, 4) + "-" + tel.Substring(6);
            else
            {
                if (((!tel.Equals("") && !tel.Equals(null) && tel.Length == 8))) //telefone 8 digitos sem ddd
                    telefone = tel.Substring(0, 4) + "-" + tel.Substring(4);
                else
                {
                    if (((!tel.Equals("") && !tel.Equals(null) && tel.Length == 8))) //telefone antigo 7 digitos sem ddd
                        telefone = tel.Substring(0, 3) + "-" + tel.Substring(3);
                }
            }

            return telefone;
        }

        public static string FormataCNPJ(string cnpj)
        {
            if (cnpj == null || cnpj.Equals(string.Empty))
                return "";

            if (cnpj.Length < 14)
                return cnpj;

            var aux = cnpj.Substring(0, 2) + "." + cnpj.Substring(2, 3) + "." + cnpj.Substring(5, 3) + "/" + cnpj.Substring(8, 4) +
                      "-" + cnpj.Substring(12);
            return aux;
        }

        public static string TrataApostrofe(string str)
        {
            return str.Replace("'", "´");
        }

        public static string FormataCPF(string cpf)
        {
            if (cpf == null)
                return "";

            if (cpf.Length < 11)
                return cpf;

            var aux = cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." +
                      cpf.Substring(6, 3) + "-" + cpf.Substring(9);
            return aux;
        }

        public static string EncontraMesAnterior(DateTime data)
        {
            int mes = data.Month;
            int ano = data.Year;

            if (mes >= 2 && mes <= 12)
            {
                var mesanterior = mes - 1;
                return mesanterior > 9 ? mesanterior + "/" + ano : "0" + mesanterior + "/" + ano;
            }

            ano = ano - 1;
            return 12 + "/" + ano;
        }

        public static void GravaOcorrencia(string usuario, string funcao, string texto )
        {
            try
            {
                var con = new Conexao();
                var sql = "INSERT INTO MA_Ocorrencias(OcData,OcFuncao,OcUsuario,OcOcorrencia) values " +
                          "( '" + DateTime.Now + "','" + funcao + "', '" + usuario + "', '" + texto + "')";
                con.Alterar(sql);
            }
            catch(SqlException ex)
            {
              Alert.Show(ex.Message);
            }
        }

        public static void SetFooterRow( GridView gvr, string rowcont )
        {
            var iEndRecord = gvr.PageSize * (gvr.PageIndex + 1);
            var iStartsRecods = iEndRecord - gvr.PageSize;
            int rows =  rowcont.Equals(string.Empty) ? 0: Convert.ToInt32(rowcont);
            GridViewRow footer = gvr.FooterRow;
            if (footer == null) return;
            footer.Visible = true;
            footer.BackColor = Color.FromArgb(0, 89, 159);
            footer.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            footer.Visible = true;
            footer.Cells[0].ColumnSpan = gvr.Columns.Count;
            footer.Cells[0].ForeColor = Color.White;
            for (int i = 1; i < gvr.Columns.Count; i++)
            {
                footer.Cells[i].Visible = false;
            }
            footer.Cells[0].Text = string.Format("{0} a {1} resultados de {2} ",
                                       iStartsRecods + 1, (iEndRecord > rows ? rows : iEndRecord),
                                       rows);

        }

        public static bool ValidaSenha(string senha)
        {
            if (senha.Equals(string.Empty)) return false;
            var rg = new Regex(@"[^a-zA-Z0-9 $#%!@~?+-_]");
            return rg.IsMatch(senha);
        }

        public static bool ValidaEmail(string key)
        {
            if (key.Equals(string.Empty)) return false;
            var rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            return rg.IsMatch(key);
        }

           public static bool ValidaTelefone(string key)
        {
            if (key.Equals(string.Empty)) return false;
            //var rg = new Regex(@"^\(\d{2}\)\d{4}-\d{4}$");
            var rg = new Regex(@"\((10)|([1-9][1-9])\) [2-9][0-9]{3}-[0-9]{4}");
            return rg.IsMatch(key);
        }

         
        public static void TrataExcessao(string numero, Exception ex)
        {
            string url = "~/pages/ErrorPage.aspx?" +
                         "Erro=" + Criptografia.Encrypt(numero, GetConfig.Key()) + "&" +
                         "Text01=" + Criptografia.Encrypt(ex.StackTrace ?? "", GetConfig.Key()) + "&" +
                         "Text02=" + Criptografia.Encrypt( ex.Message, GetConfig.Key());

            HttpContext.Current.Response.Redirect(url);
        }

        public static bool VerificaTipo( string codigo)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = from i in bd.CA_Usuarios
                            where i.UsuCodigo.Equals(codigo)
                            select i.UsuTipo;
                if (dados.Count() == 0) return false;
                return (dados.First().Equals("A") || dados.First().Equals("S"));
            }
        } 

        public static void SetCookie( string nome , TimeSpan expiracao, string value)
        {
            DateTime time = DateTime.Now + expiracao;

            var cookie = new HttpCookie(nome) { Value = value, Expires = time };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string nome)
        {
            var cookie = HttpContext.Current.Request.Cookies[nome];
            if (cookie == null || IsExpired(cookie)) throw new Exception("Cookie não Encontrado");
            return  cookie.Value;
        }

        public static bool CookieExists(string nome)
        {
            var cookie = HttpContext.Current.Request.Cookies[nome];
            if (cookie == null) return false;
            return true;
        }



        public static bool IsExpired(HttpCookie cookie)
        {
            if(cookie.Expires < DateTime.Now) return true;
            return false;
        }

        public static void DeleteCookie(string nome)
        {
            var cookie = HttpContext.Current.Request.Cookies[nome];
            if (cookie == null) return;
            cookie.Expires = DateTime.Now.AddMinutes(-20);
            HttpContext.Current.Response.Cookies.Remove(nome); 
        }




        public static void Download(string fileName, string nome)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + nome);
            HttpContext.Current.Response.WriteFile(fileName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
        }


       public static void SendEmail(string email, string nome, string texto )
       {
           var cliente = new SmtpClient("mail.agilsistemas.com", 587);
           var remetente = new MailAddress("comercial@agilsistemas.com", "Suporte Programa PET - Jovem Aprendiz");
           var destinatario = new MailAddress(email, nome);
           var mensagem = new MailMessage(remetente, destinatario)
           {
               IsBodyHtml = true,
               Body = texto,
               Subject = "Mestre Agil Web - Solicitação de informações de acesso."
           };
           var credenciais = new NetworkCredential("eli@agilsistemas.com", "mestre3415");
           cliente.Credentials = credenciais;
           cliente.Send(mensagem);
       }

       public static void SendEmailLinkPesquisa(string email, string nome, string texto)
       {
           var cliente = new SmtpClient("mail.agilsistemas.com", 587);
           var remetente = new MailAddress("aprendiz@agilsistemas.com", "Pesquisa Jovem Aprendiz");
           var destinatario = new MailAddress(email, nome);
           var mensagem = new MailMessage(remetente, destinatario)
           {
               IsBodyHtml = true,
               Body = texto,
               Subject = "Gestão do Programa Jovem Aprendiz - Avaliação de Aprendizagem."
           };
           var credenciais = new NetworkCredential("eli@agilsistemas.com", "mestre3415");
           cliente.Credentials = credenciais;
           cliente.Send(mensagem);
       }

         public static  string ValorPorExtenso(float valor)
         {
             var numero = valor.ToString();
             var num = numero.Split(',');
             string extenso;
             if(num[0] == "1") extenso = "Um real";
             else
             {
                 extenso = NumeroPorExtenso(int.Parse(num[0])) + " reais ";
             }
             if(num.Length > 1)
             {
                 var fracao = num[1].Length == 1 ? num[1].PadRight(2, '0') : num[1].Length > 2 ? num[1].Substring(0, 2) : num[1];
                 if ( !fracao.Equals("00"))
                 {
                     extenso += " e " + NumeroPorExtenso(int.Parse(fracao)) + " centavos";
                 }
             }
             return extenso;
         }


        public static  string NumeroPorExtenso(int valor)
        {
            var numero = valor.ToString();
            if (numero.Length > 4) return "Maior que 10 mil";

            numero = numero.PadLeft(4,'0');
            if (numero == "0000") return "Zero";

            string milhar = "";
            if (numero[0] != '0')
            {
                switch (numero[0])
                {
                    case '1': milhar = "Mil"; break;
                    case '2': milhar = "Dois mil"; break;
                    case '3': milhar = "Três mil"; break;
                    case '4': milhar = "Quatro mil"; break;
                    case '5': milhar = "Cinco mil"; break;
                    case '6': milhar = "Seis mil"; break;
                    case '7': milhar = "Sete mil"; break;
                    case '8': milhar = "Oito mil"; break;
                    case '9': milhar = "Nove mil"; break;
                }
            }

            string centena = "";
            if (numero[1] != '0')
            {
                switch (numero[1])
                {
                    case '1': centena = " cento"; break;
                    case '2': centena = " duzentos"; break;
                    case '3': centena = " trezentos"; break;
                    case '4': centena = " quatrocentos"; break;
                    case '5': centena = " quinhentos"; break;
                    case '6': centena = " seiscentos"; break;
                    case '7': centena = " setecentos"; break;
                    case '8': centena = " oitocentos"; break;
                    case '9': centena = " novecentos"; break;
                }
            }

            string dezena = "";
            if (numero[2] != '0')
            {
                if (numero[2] == '1'){
                    switch (numero.Substring(2))
                    {
                        case "10": dezena = " e dez"; break;
                        case "11": dezena = " e onze"; break;
                        case "12": dezena = " e doze"; break;
                        case "13": dezena = " e treze"; break;
                        case "14": dezena = " e quatorze"; break;
                        case "15": dezena = " e quinze"; break;
                        case "16": dezena = " e desesseis"; break;
                        case "17": dezena = " e dezessete"; break;
                        case "18": dezena = " e dezoito"; break;
                        case "19": dezena = " e dezenove"; break;
                    }
                }
                else
                {
                    switch (numero[2])
                    {

                        case '2': dezena = " e vinte"; break;
                        case '3': dezena = " e trinta"; break;
                        case '4': dezena = " e quarenta"; break;
                        case '5': dezena = " e cinquenta"; break;
                        case '6': dezena = " e sessenta"; break;
                        case '7': dezena = " e setenta"; break;
                        case '8': dezena = " e oitenta"; break;
                        case '9': dezena = " e noventa"; break;
                    }
                }
            }

              string unidade = "";
              if (numero[3] != '0')
              {
                  switch (numero[3])
                  {
                      case '1': unidade = " e um"; break;
                      case '2': unidade = " e dois"; break;
                      case '3': unidade = " e três"; break;
                      case '4': unidade = " e quatro"; break;
                      case '5': unidade = " e cinco"; break;
                      case '6': unidade = " e seis"; break;
                      case '7': unidade = " e sete"; break;
                      case '8': unidade = " e oito"; break;
                      case '9': unidade = " e nove"; break;
                  }
              }

              var extenso = milhar + centena + dezena + unidade;
              if (dezena.Equals(string.Empty) && unidade.Equals(string.Empty))
              {
                  if (numero[1] == '1')
                      extenso = milhar + " e  cem";
                  else
                      extenso = milhar + " e " + centena;
              }

            if (numero[2] == '1')
                extenso = milhar + centena + dezena;

            if (extenso[1] == 'e')
                extenso = extenso.Substring(2);

            return extenso;

        }

        public static bool IsNumber(string resRespostaTexto)
        {
            try
            {
                var x = int.Parse(resRespostaTexto);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static LiteralControl GetLiteral(string text)
        {
            var literal = new LiteralControl { Text = text };
            return literal;
        }

        public static string FormataNumero(string s)
        {
            int i = s.ToString().IndexOf(",");
            if (i == -1)
            {
                s = (s + ",00");
            }
            else
            {
                int j = (s.ToString().Length) - i - 1;
                if (j == 1)
                {
                    s = (s + "0");
                }
                else
                {
                    if (j == 2)
                    {
                        s = (s + "");
                    }
                }
            }
            return s;
        }

        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Equals("00000000000"))
            {
                return false;
            }
            else if (cpf.Equals("11111111111"))
            {
                return false;
            }
            else if (cpf.Equals("22222222222"))
            {
                return false;
            }
            else if (cpf.Equals("33333333333"))
            {
                return false;
            }
            else if (cpf.Equals("44444444444"))
            {
                return false;
            }
            else if (cpf.Equals("55555555555"))
            {
                return false;
            }
            else if (cpf.Equals("66666666666"))
            {
                return false;
            }
            else if (cpf.Equals("77777777777"))
            {
                return false;
            }
            else if (cpf.Equals("88888888888"))
            {
                return false;
            }
            else if (cpf.Equals("99999999999"))
            {
                return false;
            }

            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
 
    }
}