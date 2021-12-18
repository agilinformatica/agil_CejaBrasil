using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ProtocoloAgil.pages
{
    public partial class EstatisticaAlunos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "estatisticas";
            if(!IsPostBack)
                MultiView1.ActiveViewIndex = 0;
        }


        protected void GridView_DataBound(object sender, EventArgs e)
        {

            var gvr = (GridView) sender;
            var total = gvr.Rows.Cast<GridViewRow>().Sum(row => int.Parse(row.Cells[1].Text));


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
            footer.Cells[0].Text = string.Format(" Total: {0} ", total);

  

          
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void bt_lista_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void bt_grafico_Click(object sender, EventArgs e)
        {
            Chart1.DataBind();
            MultiView1.ActiveViewIndex = 1;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (MultiView1.ActiveViewIndex == 0)
            {
                Session["id"] = 24;
            }
            else if (MultiView1.ActiveViewIndex == 3) // sexo
            {
                Session["id"] = 92;
            }
            else if (MultiView1.ActiveViewIndex == 4) // idade
            {
                Session["id"] = 93;
            }
            

            MultiView1.ActiveViewIndex = 2;
        }

        protected void GridSituacaoRegiao_DataBound(object sender, EventArgs e)
        {

            var gvr = (GridView)sender;
            var total = gvr.Rows.Cast<GridViewRow>().Sum(row => int.Parse(row.Cells[3].Text));


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
            footer.Cells[0].Text = string.Format(" Total: {0} ", total);
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            var opt = RadioButtonList1.SelectedValue;
            Chart1.Series["Series1"].ChartType = opt.Equals("1") ? SeriesChartType.Column : SeriesChartType.Pie;
            Chart1.Series["Series1"].IsVisibleInLegend = !opt.Equals("1");
            Chart1.Series[0].YValueMembers = opt.Equals("1") ? "QTD" : "Percentual";
        }

        protected void btnSituacaoSexo_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            GridSituacaoSexo.DataBind();
        }

        protected void btnSituacaoIdade_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            gridSituacaoIdade.DataBind();
        }
    }
}