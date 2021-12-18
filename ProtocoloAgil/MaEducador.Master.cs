using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil
{
    public partial class MaEducador : MasterPage
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

            if (Session["matricula"] == null || Session["matricula"].Equals(string.Empty))
            {
                Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
            }

            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
            var escola = from i in bd.CA_Unidades
                         where i.UniCodigo.Equals(GetConfig.Escola())
                         select new {i.UniNome, i.UniEndereco, i.UniEstado, i.UniCidade, i.UniNumeroEndereco,
                             i.UniComplemento, i.UniTelefone, i.UniEnderecoWeb};

            var unidade = escola.First();
            LBnomeEscola.Text = unidade.UniNome;
            LBenderecoEscola.Text = unidade.UniEndereco + ", nº " + unidade.UniNumeroEndereco + " - " + unidade.UniComplemento + " - " +
                  unidade.UniCidade + " - " + unidade.UniEstado +
             "Tel.: " + " (" + unidade.UniTelefone.Substring(0, 2) + ") " + unidade.UniTelefone.Substring(2, 4) + "-" + unidade.UniTelefone.Substring(6);
            LNKendWeb.Attributes.Add("href", unidade.UniEnderecoWeb);
            LBEndWeb.Text = unidade.UniEnderecoWeb;

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            var tipoacesso = Criptografia.Encrypt("A", GetConfig.Key());
            var lb = (LinkButton) sender;
            switch (lb.ID)
            {
                case "LK_Cadastro_Categoria": Response.Redirect("MeusDados.aspx?acs=" + tipoacesso); break;
                case "LK_Lanca_diario": Response.Redirect("DisciplinasDiario.aspx?acs=" + tipoacesso); break;
                case "LK_AreaEnsino": Response.Redirect("AreaEnsino.aspx?acs=" + tipoacesso); break;
                //case "LK_Lanca_notas": Response.Redirect("NotasFaltas.aspx?acs=" + tipoacesso); break;
                case "LK_Lanca_notas": Response.Redirect("LancamentoFaltasEducadores.aspx?acs=" + tipoacesso); break;
                case "LK_Gestao_arquivos": Response.Redirect("Arquivos.aspx?acs=" + tipoacesso); break;
                case "LK_Aniversariantes": Response.Redirect("AniversariantesPeriodo.aspx?acs=" + tipoacesso); break;
                case "LK_controle_alunos": Response.Redirect("ControleAlunos.aspx?acs=" + tipoacesso); break;
                case "LK_AvaliacoesDisponiveisProfessor": Response.Redirect("AvaliacaoOrientadorProfessor.aspx?acs=" + tipoacesso); break;

                case "LK_LancamentoFaltasCapacitacao": Response.Redirect("LancamentoFaltasEducadoresCapacitacao.aspx?acs=" + tipoacesso); break;

            }
        }
    }
}