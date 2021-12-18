using System;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;

namespace ProtocoloAgil.pages
{
    public partial class AprendizesParceiro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "estatisticas";
            if (!IsPostBack)
                MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_alocados_sintetico_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            GridView1.DataBind();
        }

        protected void btn_alocados_Analitico_Click(object sender, EventArgs e)
        {
            BindGridView(1, null);
            lb_nome.Text = "Todos.";
            hf_parceiro.Value = "";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if(MultiView1.ActiveViewIndex == 3) return;
            Session["activeview"] = MultiView1.ActiveViewIndex;
            Session["id"] = MultiView1.ActiveViewIndex == 0 ? 25 : MultiView1.ActiveViewIndex == 1 ?  39: 26;
            int i;
            if( int.TryParse(hf_parceiro.Value,out i) )
            {
                Session["PRMT_Empresa"] = hf_parceiro.Value;
                Session["PRMT_nome_empresa"] = WebUtility.HtmlDecode(lb_nome.Text);
                Session["id"] = 67;
            }
            MultiView1.ActiveViewIndex = 3;
        }

        protected void IMB_Quantitativo_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton) sender;
            var row = (GridViewRow) bt.Parent.Parent;
            Session["PRMT_Empresa"] = row.Cells[0].Text;
            LBNomeEmpesa.Text = WebUtility.HtmlDecode(row.Cells[1].Text);
            Session["PRMT_nome_empresa"] = WebUtility.HtmlDecode(row.Cells[1].Text);
            GridView2.DataBind();
            MultiView1.ActiveViewIndex = 2;
        }

        protected void IMB_Analítico_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            var row = (GridViewRow)bt.Parent.Parent;
            Session["PRMT_Empresa"] = row.Cells[0].Text;
            hf_parceiro.Value = row.Cells[0].Text;
            Session["PRMT_nome_empresa"] = WebUtility.HtmlDecode(row.Cells[1].Text);
            lb_nome.Text = WebUtility.HtmlDecode(row.Cells[1].Text);
            MultiView1.ActiveViewIndex = 1;
            BindGridView(2, int.Parse(hf_parceiro.Value));
        }
        
        private void BindGridView(int tipo, int? parceiro)
        {
            const string sql = "Select Apr_Nome,TurNome,ParNomeFantasia,ParUniDescricao, Apr_PrevFimAprendizagem, Apr_InicioAprendizagem, ALADataPrevTermino,ALADataTermino, ALAValorBolsa, ALAValorTaxa, " +
                "ALApagto = case when ALApagto = 'E' then 'Empresa' when ALApagto = 'F' then 'Fundação' end ,StaDescricao from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo INNER JOIN CA_Turmas ON ALATurma = TurCodigo " +
                "INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo INNER JOIN CA_ParceirosUnidade ON ALAUnidadeParceiro = ParUniCodigo INNER JOIN CA_Parceiros ON  ParUniCodigoParceiro = ParCodigo WHERE ALAStatus = 'A' AND TurCurso = '002' AND ALADataInicio <= GetDate()  " +
                "order By ParNomeFantasia,Apr_Nome";

             var sql2 = "Select Apr_Nome,TurNome,ParNomeFantasia,ParUniDescricao, Apr_PrevFimAprendizagem, Apr_InicioAprendizagem, ALADataPrevTermino,ALADataTermino, ALAValorBolsa, ALAValorTaxa, " +
                "ALApagto = case when ALApagto = 'E' then 'Empresa' when ALApagto = 'F' then 'Fundação' end ,StaDescricao from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo INNER JOIN CA_Turmas ON ALATurma = TurCodigo " +
                "INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo INNER JOIN CA_ParceirosUnidade ON ALAUnidadeParceiro = ParUniCodigo INNER JOIN CA_Parceiros ON  ParUniCodigoParceiro = ParCodigo WHERE ALAStatus = 'A'  AND TurCurso = '002' AND ALADataInicio <= GetDate()  " +
                "AND ParCodigo = " + parceiro + " order By ParNomeFantasia,Apr_Nome";

            var datasource = new SqlDataSource() { ID = "SDS_alocados", ConnectionString = GetConfig.Config(), SelectCommand = tipo == 1 ? sql : sql2 };
            datasource.Selected += SqlDataSource1_Selected;
            GridView6.DataSource = datasource;
            GridView6.DataBind();

        }

        protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView6.PageIndex = e.NewPageIndex;
            if( lb_nome.Text.Equals("Todos."))
                BindGridView(1, null);
            else
                 BindGridView(2, int.Parse(hf_parceiro.Value));
        }
    }
}