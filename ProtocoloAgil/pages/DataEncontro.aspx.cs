using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class DataEncontro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            if (!IsPostBack)
            {
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

            if (txtDataInicio.Text.Equals(""))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                       "alert('Data Início é obrigatório')", true);
            }
            else
            {
                string date = Funcoes.ConverteData(txtDataInicio.Text);
                DateTime data = Convert.ToDateTime(date);

                if (!data.DayOfWeek.ToString().Equals("Monday"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                          "alert('O dia escolhido não é uma segunda-feira')", true);
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var con = new Conexao();
                        var sql = "insert into CA_DatasEncontros values ('" + data + "', '" + DDTipoEncontro.SelectedValue + "', '" + txtLocal.Text + "')";
                        con.Alterar(sql);
                        data = data.AddDays(1);
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Encontros cadastrados com sucesso.')", true);

                }
            }
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
            txtDataInicioPesquisa.Text = "";
            txtDataFinalPesquisa.Text = "";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregaEncontros();
        }


        public void carregaEncontros()
        {


            try
            {
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var datasource = from e in bd.CA_DatasEncontros
                                     // join p in bd.CA_ParceirosUnidades on e.Enc_UnidParceiro equals p.ParUniCodigo
                                     // join s in bd.CA_StatusEncaminhamentos on e.Enc_Status equals s.Ste_Codigo
                                     //where e.DteData > Convert.ToDateTime(txtDataInicioPesquisa.Text)
                                     //      || e.DteTipoEncontro.Equals(DDTipoEncontroPesquisa.SelectedValue)

                                     select new { e.DteData, e.DteLocalEncontro, e.DteTipoEncontro };
                    
                    // Adiciona no filtro se a data inicial for digitada
                    if (!txtDataInicioPesquisa.Text.Equals(""))
                    {
                        datasource = datasource.Where(item => item.DteData >= Convert.ToDateTime(txtDataInicioPesquisa.Text));
                    }

                    // Adiciona no filtro se a data final for digitada
                    if (!txtDataFinalPesquisa.Text.Equals(""))
                    {
                        datasource = datasource.Where(item => item.DteData <= Convert.ToDateTime(txtDataFinalPesquisa.Text));
                    }

                    //Adiciona no filtro se o tipo de encontro foi selecionado
                    //if (DDTipoEncontroPesquisa.SelectedValue.Equals("1") || DDTipoEncontroPesquisa.SelectedValue.Equals("2"))
                    //{
                    //    datasource = datasource.Where(item => item.DteTipoEncontro.Equals(DDTipoEncontroPesquisa.SelectedValue));
                    //}


                    gridEncontros.DataSource = datasource;
                    gridEncontros.DataBind();
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000158", ex);
            }
        }

        protected void btn_novo_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            txtDataInicio.Text = "";
            txtLocal.Text = "";
            DDTipoEncontro.SelectedValue = "1";
            DDMes.SelectedValue = "01";

        }

        protected void gridEncontros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEncontros.PageIndex = e.NewPageIndex;
            carregaEncontros();
        }
    }
}