using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil
{
    public partial class MaParceiro : MasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActiveTab.Value = "0";
            }

            if (Session["codigo"] == null || Session["codigo"].Equals(string.Empty))
            {
                Funcoes.TrataExcessao("000000", new Exception("../UserLogin.aspx"));
            }

            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
            var parceiro = from i in bd.CA_Parceiros
                           where i.ParCodigo.Equals(int.Parse(Session["codigo"].ToString()))
                           select new { i.ParDescricao, i.ParEndereco, i.ParEstado, i.ParCidade,
                               i.ParNumeroEndereco, i.ParComplemento, i.ParTelefone };

            var unidade = parceiro.First();
            LBnomeEscola.Text = unidade.ParDescricao;
            LBenderecoEscola.Text = unidade.ParEndereco + ", nº " + unidade.ParNumeroEndereco + " - " +
                
                (string.IsNullOrEmpty(unidade.ParComplemento )?  "" :    unidade.ParComplemento + " - " ) +
                
                  unidade.ParCidade + " - " + unidade.ParEstado +
                  ". Tel.: " + (string.IsNullOrEmpty(unidade.ParTelefone) ? "NA" : Funcoes.FormataTelefone(unidade.ParTelefone));

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            var lb = (LinkButton) sender;
            Session["funcao"] = lb.ID;
            switch (lb.ID)
            {
                case "LK_Aprendizes_Alocados": Response.Redirect("ControleAlocados.aspx"); break;
                //case "LK_NotasPeriodo": Response.Redirect("NotasPeriodoParceiro.aspx"); break;
                case "LK_Contato_parceiro": Response.Redirect("EnvioEmail.aspx"); break;
                case "LK_Controle_Por_Parceiro": Response.Redirect("ControlePresencaDoParceiro.aspx"); break;
                case "LK_Total_Por_Parceiro": Response.Redirect("TotalAulasDoParceiro.aspx"); break;
                case "LK_CadastroVagaEmpresa": Response.Redirect("CadastroVagasEmpresa.aspx"); break;
                case "LK_AvaliacaoDesempenho": Response.Redirect("AvaliacaoOrientadorEmpresa02.aspx"); break;
              
            }
        }
    }
}