using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class EstatisticaAprendizes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "estatisticas";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                txtDataReferenciaPorTurma.Text = DateTime.Now.ToShortDateString();
            }
                
            //if (Session["AtivosArea"] != null)
            //{
            //    if (Session["AtivosArea"].Equals("S"))
            //    {
            //       // txtDataReferencia.Text = DateTime.Now.ToShortDateString();
            //        MultiView1.ActiveViewIndex = 1;
            //        RB_grafico_ativo_Area.SelectedValue = "1";
            //        //MultiView1.ActiveViewIndex = 7;
            //    }
            //}
        }


        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView) sender, HFRowCount.Value);
        }


        protected void GridView_Total_DataBound(object sender, EventArgs e)
        {
            SetFooterRowTotal((GridView) sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
            Session["QTD"] = e.AffectedRows.ToString();
        }


        protected void SqlDataSource3_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            e.Command.Connection.Open();
            var dados = e.Command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            int cont = 0;
            
            while (dados.Read())
            {
                cont += int.Parse(dados["QTD"].ToString());
            }
            HFRowCount.Value = cont.ToString();
            Session["QTD"] = cont;
        }


        protected void SqlDataSource2_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            var datasource = (SqlDataSourceView)sender;
             HFRowCount.Value = BindIdade(datasource.SelectCommand) ;
        }

        protected void bt_lista_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void bt_grafico_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["id"] = 24;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radioButtonList = (RadioButtonList) sender;
            var opt = radioButtonList.SelectedValue;

            Chart chart = radioButtonList.ID.Equals("RB_grafico_ativo_Area") ? Chart2
                        : radioButtonList.ID.Equals("RB_grafico_Desliados_sintetico") ? Chart3
                        : radioButtonList.ID.Equals("RB_grafico_alocados_periodo") ? Chart4
                        : radioButtonList.ID.Equals("RB_grafico_desligados_genero") ? Chart5 
                        : radioButtonList.ID.Equals("rb_grafico_pagamento") ? Chart1 : Chart7;
            chart.Series["Series1"].ChartType = opt.Equals("1") ? SeriesChartType.Column : SeriesChartType.Pie;
            chart.Series["Series1"].IsVisibleInLegend = !opt.Equals("1");

            chart.Series[0].Label = opt.Equals("1") ? "#VALY" : "#PERCENT";
            chart.Series[0].LegendText = "#AXISLABEL";
        }

        protected void bt_ativos_turma_Click(object sender, EventArgs e)
        {
            if (txtDataReferenciaPorTurma.Text.Count() < 1) {
                txtDataReferenciaPorTurma.Text = DateTime.Now.ToShortDateString();
            }
            MultiView1.ActiveViewIndex = 0;
        }
        // pesquisar
        protected void btnAtivosPorTurma_Click(object sender, EventArgs e)
        {
             MultiView1.ActiveViewIndex = 0;
            painelAtivoPorTurma.Visible = true;
            
        }


        protected void bt_ativos_area_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            if (txtDataReferenciaPorAreaAtuacao.Text.Count() < 1)
              txtDataReferenciaPorAreaAtuacao.Text = DateTime.Now.ToShortDateString();
        }

        // pesquisar
        protected void btnAtivosArea_Click(object sender, EventArgs e)
        {
            
            RB_grafico_ativo_Area.SelectedValue = "1";
            painelAtivosPorArea.Visible = true;
        }
        
       
        protected void bt_desligados_periodo_Click(object sender, EventArgs e)
        {
            TBdataInicio.Text = string.Empty;
            TBdataTermino.Text = string.Empty;
            RB_grafico_ativo_Area.SelectedValue = "1";
            PN_Siuacao_Desligados.Visible = false;
            GridView3.DataBind();
            GridView4.DataBind();
            MultiView1.ActiveViewIndex = 2;
            MultiView2.ActiveViewIndex = 0;
        }

        protected void btn_pesquisa_desligados_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBdataInicio.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de início da pesquisa.");
                if (TBdataTermino.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de término da pesquisa.");

                GridView3.DataBind();
                GridView4.DataBind();
                PN_Siuacao_Desligados.Visible = true;
                RB_grafico_Desliados_sintetico.SelectedValue = "1";
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000122", ex);
            }
        }

        protected void btn_desligados_sintetico_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            MultiView2.ActiveViewIndex = 0;
            GridView3.DataBind();
            RB_grafico_Desliados_sintetico.SelectedValue = "1";
            Chart3.DataBind();
        }

        protected void btn_desligados_analitico_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            MultiView2.ActiveViewIndex = 1;
            GridView4.DataBind();
        }

        protected void bt_alocados_periodo_Click(object sender, EventArgs e)
        {
            pn_alocados_periodo.Visible = false;
            TBdataInicio_alocados.Text= string.Empty;
            TBdataTermino_alocados.Text = string.Empty;
            GridView5.DataBind();
            GridView6.DataBind();
            RB_grafico_alocados_periodo.SelectedValue = "1";
            MultiView1.ActiveViewIndex = 3;
            MultiView3.ActiveViewIndex = 0;
        }

        protected void btn_alocados_sintetico_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            MultiView3.ActiveViewIndex = 0;
            GridView5.DataBind();
            RB_grafico_alocados_periodo.SelectedValue = "1";
            Chart4.DataBind();
        }

        protected void btn_alocados_Analitico_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            MultiView3.ActiveViewIndex = 1;
            GridView6.DataBind();
            SDS_Alocados_analitico.DataBind();
        }

        protected void btn_pesquisa_alocados_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBdataInicio_alocados.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de início da pesquisa.");
                if (TBdataTermino_alocados.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de término da pesquisa.");

                GridView5.DataBind();
                GridView6.DataBind();
                pn_alocados_periodo.Visible = true;
                RB_grafico_alocados_periodo.SelectedValue = "1";
                Chart4.DataBind();
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000122", ex);
            }
        }

        protected void btn_pesquisa_mot_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_data_ini_mot.Text.Equals(string.Empty))  throw new ArgumentException("Informe a data de início da pesquisa.");
                if (tb_data_fim_mot.Text.Equals(string.Empty))  throw new ArgumentException("Informe a data de término da pesquisa.");

                GridView7.DataBind();
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000122", ex);
            }
        }

        protected void btn_motivo_desligamento_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            MultiView4.ActiveViewIndex = 0;
        }

        protected void btn_imprime_mot_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_data_ini_mot.Text.Equals(string.Empty))  throw new ArgumentException("Informe a data de início da pesquisa.");
                if (tb_data_fim_mot.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de término da pesquisa.");
                Session["id"] = 41;
                Session["PRMT_dataInicio"] = tb_data_ini_mot.Text;
                Session["PRMT_dataFinal"] = tb_data_fim_mot.Text;
                MultiView1.ActiveViewIndex = 4;
                MultiView4.ActiveViewIndex = 1;
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000122", ex);
            }
        }


        protected void btn_listar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            MultiView4.ActiveViewIndex = 0;
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            MultiView2.ActiveViewIndex = 2;
            GridView8.DataBind();
            RB_grafico_desligados_genero.SelectedValue = "1";
            Chart5.DataBind();
        }

        protected void btn_desligados_idade_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            MultiView2.ActiveViewIndex = 3;
            GridView9.DataBind();
        }

        protected void btn_alocados_Genero_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            MultiView3.ActiveViewIndex = 2;
            GridView10.DataBind();
            RB_grafico_alocados_genero.SelectedValue = "1";
            Chart7.DataBind();
        }

        protected void btn_alocados_idade_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            MultiView3.ActiveViewIndex = 3;
            GridView11.DataBind();
        }

        protected void btn_print_Ativos_Click(object sender, EventArgs e)
        {
            var bt = (Button) sender;
            switch (bt.ID)
            {
                case "btn_print_Ativos":
                    Session["id"] = 27;
                    Session["DataReferencia"] = txtDataReferenciaPorTurma.Text;
                    break;
                case "btn_print_Ativos_area":
                    Session["DataReferencia"] = txtDataReferenciaPorTurma.Text;
                    Session["id"] = 28;
                    break;
                case "btn_print_desligados_situacao":
                    Session["Data_inicio_parameter"] = TBdataInicio.Text;
                    Session["Data_final_parameter"] = TBdataTermino.Text;
                    Session["id"] = 29;
                    break;

                case "btn_print_desligados_analitico":
                    Session["Data_inicio_parameter"] = TBdataInicio.Text;
                    Session["Data_final_parameter"] = TBdataTermino.Text;
                    Session["id"] = 30;
                    break;

                case "btn_print_desligados_genero":
                    Session["Data_inicio_parameter"] = TBdataInicio.Text;
                    Session["Data_final_parameter"] = TBdataTermino.Text;
                    Session["id"] = 31;
                    break;

                case "btn_print_desligados_idade":
                    Session["Data_inicio_parameter"] = TBdataInicio.Text;
                    Session["Data_final_parameter"] = TBdataTermino.Text;
                    Session["id"] = 44;
                    break;

                case "btn_print_Alocados_parceiro":
                    Session["Data_inicio_parameter"] = TBdataInicio_alocados.Text;
                    Session["Data_final_parameter"] = TBdataTermino_alocados.Text;
                    Session["id"] = 45;
                    break;

                case "btn_print_Alocados_analitico":
                    Session["Data_inicio_parameter"] = TBdataInicio_alocados.Text;
                    Session["Data_final_parameter"] = TBdataTermino_alocados.Text;
                    Session["id"] = 46;
                    break;

                case "btn_print_Alocados_Genero":
                    Session["Data_inicio_parameter"] = TBdataInicio_alocados.Text;
                    Session["Data_final_parameter"] = TBdataTermino_alocados.Text;
                    Session["id"] = 47;
                    break;

                case "btn_print_Alocados_Idade":
                    Session["Data_inicio_parameter"] = TBdataInicio_alocados.Text;
                    Session["Data_final_parameter"] = TBdataTermino_alocados.Text;
                    Session["id"] = 48;
                    break;

                case "btn_imprime_mot":
                    Session["Data_inicio_parameter"] = tb_data_ini_mot.Text;
                    Session["Data_final_parameter"] = tb_data_fim_mot.Text;
                    Session["id"] = 49;
                    break;

                case "btn_print_Tipo_Pagamento":
                     Session["id"] = 63;
                     Session["DataReferencia"] = txtDataReferenciaPorTurma.Text;
                    break;
            }

            MultiView1.ActiveViewIndex = 5;
            MultiView2.ActiveViewIndex = 0;
            MultiView3.ActiveViewIndex = 0;
            MultiView4.ActiveViewIndex = 0;
        }


        public static void SetFooterRowTotal(GridView gvr, string rowcont)
        {
            var rows = gvr.Rows;
            int sum = rows.Cast<GridViewRow>().Sum(row => int.Parse(row.Cells[1].Text));
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
            footer.Cells[0].Text = string.Format("Total: {0}", (sum == 0 ? "0" : sum.ToString()));
        }


        protected void GridView9_DataBound(object sender, EventArgs e)
        {
            var gvr = (GridView) sender;
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
            footer.Cells[0].Text = string.Format("Total: {0}", HFRowCount.Value);
            gvr.AllowPaging = true;
        }



        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            var gvr = (GridView)sender;
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
            footer.Cells[0].Text = string.Format("Total: {0}", HFRowCount.Value);
            gvr.AllowPaging = true;
        }


          
        private string BindIdade(string selectCommand)
        {
            var parameters = new List<SqlParameter> {new SqlParameter("@dataInicio", TBdataInicio.Text),new SqlParameter("@datatermino", TBdataTermino.Text),
                                     new SqlParameter("@dataInicio_alocados", TBdataInicio_alocados.Text),new SqlParameter("@datatermino_alocados", TBdataTermino_alocados.Text)};

            var con = new Conexao();
            var dr = con.Consultar(selectCommand, parameters.ToArray());
            int soma = 0;
            while (dr.Read())
            {
                soma += int.Parse(dr["QTD"].ToString());
            }
            return soma.ToString();
        }

        protected void btn_pagamento_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 6;
            if (txtDataReferenciaTipoPagamento.Text.Count() < 1)
            {
                txtDataReferenciaTipoPagamento.Text = DateTime.Now.ToShortDateString();
            }
            
        }

        protected void btnTipoPagamento_Click(object sender, EventArgs e)
        {

            painelTipoPagamento.Visible = true;
            GridView12.DataBind();
            rb_grafico_pagamento.SelectedValue = "1";
            Chart1.DataBind();
            MultiView1.ActiveViewIndex = 6;
        }

        protected void btnAtivosUnidade_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 7;
        }

        protected void btnPesquisarAtivosUnidade_Click(object sender, EventArgs e)
        {
            if (txtAtivosPorUnidade.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de refrência.");
        //    if (txtDataTerminoAtivosPorUnidade.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de término da pesquisa.");
            painelAtivosUnidade.Visible = true;
            gridAtivosPorUnidade.DataBind();
        }

        protected void btnAtivosPorCidade_Click(object sender, EventArgs e)
        {
           
            MultiView1.ActiveViewIndex = 8;
        }

      

        protected void btnAtivosPorCidadePesquisa_Click1(object sender, EventArgs e)
        {
            painelPorcidade.Visible = true;
            gridPorCidade.DataBind();
        }

      
    }
}