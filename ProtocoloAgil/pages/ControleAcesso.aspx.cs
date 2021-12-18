using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class ControleAcesso : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            if (!IsPostBack)
            {
                CarregaOpcoes();
            }
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }


        private void CarregaOpcoes()
        {
            var itens = new List<ListItem>();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var tipo = (from i in bd.CA_PerfilUsuarios select new { i.PerfCodigo, i.PerfDescricao });

                DD_tipo.DataSource = tipo.ToList();
                DD_tipo.DataBind();

            }
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var row = ((GridView)sender).SelectedRow;
                var tipo = "A";
                var usuario = DD_tipo.SelectedValue;
                var dados = from i in bd.CA_funcoesSistemas
                            where i.FunSDescricao.Equals(Server.HtmlDecode(row.Cells[1].Text))
                            select i.FunSNomeForm;
                try
                {
                    if (dados.Count() != 0)
                        foreach (var dado in dados)
                        {
                            var cb = (CheckBox)GridView3.SelectedRow.FindControl("CheckBox1");
                            if (cb.Checked)
                                tipo = "S";
                            var con = new Conexao();
                            con.Alterar("INSERT INTO CA_AutorizacaoUsuario(AutFUsuario,AutFFuncao,AutFTipoAut) " +
                                        "VALUES ('" + usuario + "','" + dado + "','" + tipo + "') ");

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                                "alert('Autorização adicionada para o perfil " +
                                                                DD_tipo.SelectedItem.Text + ".')", true);
                            GridView3.DataBind();
                            GridView2.DataBind();
                        }
                }
                catch (SqlException ex)
                {
                    Funcoes.TrataExcessao("000019", ex);
                }
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var row = ((GridView)sender).SelectedRow;
                var usuario = DD_tipo.SelectedValue;
                var dados = from i in bd.CA_funcoesSistemas
                            where i.FunSDescricao.Equals(Server.HtmlDecode(row.Cells[1].Text))
                            select i.FunSNomeForm;
                try
                {
                    if (dados.Count() != 0)
                        foreach (var dado in dados)
                        {
                            var con = new Conexao();
                            con.Alterar("DELETE FROM CA_AutorizacaoUsuario WHERE (AutFUsuario = '" + usuario +
                                        "') and  (AutFFuncao = '" + dado + "') ");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                                "alert('Autorização removida para o perfil " +
                                                                DD_tipo.SelectedItem.Text + ".')", true);
                            GridView3.DataBind();
                            GridView2.DataBind();
                        }
                }
                catch (SqlException ex)
                {
                    Funcoes.TrataExcessao("000020", ex);
                }
            }
        }
    }
}