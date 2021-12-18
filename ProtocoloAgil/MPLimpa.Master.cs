using System;
using System.Linq;
using MestreNovoWeb;

namespace MestreNovoWeb
{
    public partial class MPLimpa : System.Web.UI.MasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // ActiveTab.Value = "0";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Escola"] == null || Session["Escola"].ToString().Equals(string.Empty))
            //{
            //    Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
            //}

            try
            {
                //using (var bd = new DataClasses1DataContext(GetConfig.config()))
                //{
                //    var escola = (from i in bd.MA_Escolas
                //                  where i.EscCodigo.Equals(Session["Escola"])
                //                  select i).First();
                //    LBnomeEscola.Text = escola.EscNome;
                //    LBenderecoEscola.Text = escola.EscEndereco + ", " + escola.EscCidade + "-" + "" + escola.EscEstado + ". CEP: " + Funcoes.FormataCep(escola.EscCEP) +
                //                    " Tel.: " + Funcoes.FormataTelefone(escola.EscTelefonedaEscola);
                //    LBEndWeb.Text = escola.EscEnderecoWEB;
                //    LNKendWeb.Attributes.Add("href", escola.EscEnderecoWEB);
                //}

            if (DateTime.Now.Hour < 12)
            {
                LBsaudacao.Text = "Bom dia, ";
            }
            else if (DateTime.Now.Hour < 18)
            {
                LBsaudacao.Text = "Boa Tarde, ";
            }
            else
            {
                LBsaudacao.Text = "Boa Noite, ";
            }

            LBusuario.Text = "Convidado";
            LBdata.Text = DateTime.Now.ToString("f");
            }
            catch (Exception)
            {
                Response.Redirect("~/pages/ErrorPage.aspx?Erro=000006", false);
            }
        }
    }
}
