using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil
{
    public partial class MaAluno : MasterPage
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
                ActiveTab.Value = "0";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["codigo"] == null || Session["codigo"].Equals(string.Empty))
            //{
            //    Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
            //}

            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
            var escola = from i in bd.CA_Unidades
                         where i.UniCodigo.Equals(GetConfig.Escola())
                         select new { i.UniNome,i.UniEndereco,i.UniEstado,i.UniCidade,i.UniNumeroEndereco,
                             i.UniComplemento,i.UniTelefone,i.UniEnderecoWeb };

            var unidade = escola.First();
            LBnomeEscola.Text = unidade.UniNome;
            LBenderecoEscola.Text = unidade.UniEndereco + ", nº " + unidade.UniNumeroEndereco + " - " + unidade.UniComplemento + " - " +
                  unidade.UniCidade + " - " + unidade.UniEstado +
             "Telefone: " + " (" + unidade.UniTelefone.Substring(0, 2) + ") " + unidade.UniTelefone.Substring(2, 4) + "-" + unidade.UniTelefone.Substring(6);
            LNKendWeb.Attributes.Add("href", unidade.UniEnderecoWeb);
            LBEndWeb.Text = unidade.UniEnderecoWeb;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            var lb = (LinkButton) sender;

            Session["funcao"] = lb.ID;
            switch (lb.ID)
            {
                case "LK_meus_dados": Response.Redirect("MeusDados.aspx"); break;
                case "LK_boletim_aprendiz": Response.Redirect("BoletimAprendiz.aspx"); break;
                case "LK_Gestao_arquivos": Response.Redirect("ArquivosAlunos.aspx"); break;
                case "LK_cronograma": Response.Redirect("CronogramaAprendizes.aspx"); break;
                case "LK_Contato": Response.Redirect("EnvioEmail.aspx"); break;
                case "LK_SolicitaDocumento": Response.Redirect("SolicitaDocumento.aspx"); break;
                    
            }
        }
    }
}