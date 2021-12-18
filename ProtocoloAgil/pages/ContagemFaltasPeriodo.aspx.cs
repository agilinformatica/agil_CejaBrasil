using MenorAprendizWeb.Base;
using ProtocoloAgil.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class ContagemFaltasPeriodo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            }
        }
        protected void btn_pesquisa_Click(object sender, EventArgs e)
        {
            if (tb_data_inicial.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Informe a data inicial para pesquisar.')", true);
                return;
            }
            if (tb_data_final.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Informe a data final para pesquisar.')", true);
                return;
            }

            //Session["Parceiro"] = DD_Parceiro.SelectedValue;
            Session["DataInicial"] = tb_data_inicial.Text;
            Session["DataFinal"] = tb_data_final.Text;

            CarregarGrid(Convert.ToDateTime(tb_data_inicial.Text), Convert.ToDateTime(tb_data_final.Text));

        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CarregarGrid(Convert.ToDateTime(Session["DataInicial"]), Convert.ToDateTime(Session["DataFinal"]));
        }



        private void CarregarGrid(DateTime dataInicial, DateTime dataFinal)
        {

            Session["dataInicial"] = dataInicial;
            Session["dataFinal"] = dataFinal;

            var sql = @"SELECT View_CA_Contagem_Faltas_Aprendiz.ParDescricao, View_CA_Contagem_Faltas_Aprendiz.ParUniDescricao, View_CA_Contagem_Faltas_Aprendiz.Apr_Codigo, View_CA_Contagem_Faltas_Aprendiz.Apr_Nome, SUM(CASE WHEN [Faltou]= 1 THEN 1 ELSE 0 END) AS FaltaDias, Sum(CASE WHEN [Faltou]=0 THEN [Faltou] ELSE 0 END) AS FaltaHoras, View_CA_Contagem_Faltas_Aprendiz.ParUniCNPJ,View_CA_Contagem_Faltas_Aprendiz.Apr_NumSistExterno FROM View_CA_Contagem_Faltas_Aprendiz WHERE (((View_CA_Contagem_Faltas_Aprendiz.AdiDataAula) Between '" + dataInicial + "' And '" + dataFinal + "') AND ((View_CA_Contagem_Faltas_Aprendiz.Faltou)<>0)) GROUP BY View_CA_Contagem_Faltas_Aprendiz.ParDescricao, View_CA_Contagem_Faltas_Aprendiz.ParUniDescricao, View_CA_Contagem_Faltas_Aprendiz.Apr_Codigo,View_CA_Contagem_Faltas_Aprendiz.Apr_NumSistExterno,View_CA_Contagem_Faltas_Aprendiz.Apr_Nome, View_CA_Contagem_Faltas_Aprendiz.ParUniCNPJ ORDER BY View_CA_Contagem_Faltas_Aprendiz.ParDescricao, View_CA_Contagem_Faltas_Aprendiz.ParUniDescricao, View_CA_Contagem_Faltas_Aprendiz.Apr_Nome;";


            var dsDadosGrid3 = new SqlDataSource
            {
                ID = "ODSescola",
                ConnectionString = GetConfig.Config(),
                SelectCommand = sql
            };

            GridView1.DataSource = dsDadosGrid3;
            GridView1.DataBind();



        }





        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            Session["id"] = 83;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnImprimir_Click(object sender, System.EventArgs e)
        {
            Session["id"] = 95;
            Session["dataInicialImpressao"] = tb_data_inicial.Text;
            Session["dataFinalImpressao"] = tb_data_final.Text;
            MultiView1.ActiveViewIndex = 1;

        }


    }
}