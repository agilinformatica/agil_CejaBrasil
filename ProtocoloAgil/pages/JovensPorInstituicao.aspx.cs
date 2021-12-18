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
    public partial class JovensPorInstituicao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "aprendiz";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
          //  if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if ( Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
              //  Novo.Enabled = false;
               // BTinsert.Enabled = false;
            }

            if (IsPostBack) return;

            CarregaInstituicaoParceira();
           // BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        public void CarregaInstituicaoParceira(){

           

            // CARREGAR GRID SEM LINQ
            var sql = "Select * from CA_InstituicoesParceiras where 1 = 1 order by IpaDescricao ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDInstituicaoParceira.DataSource = datasource;
            DDInstituicaoParceira.DataBind();

        }

       

        private void BindGridView()
        {
            var where = "";

            if (!DDInstituicaoParceira.SelectedValue.Equals(""))
            {
                where += " and IpaCodigo = " + DDInstituicaoParceira.SelectedValue + "";
            }

            var sql = "SELECT CA_InstituicoesParceiras.IpaDescricao, CA_Aprendiz.Apr_Nome, CA_Aprendiz.Apr_Codigo, CA_Aprendiz.Apr_InstParceira, CA_Aprendiz.Apr_Situacao, CA_SituacaoAprendiz.StaDescricao FROM (CA_Aprendiz INNER JOIN CA_SituacaoAprendiz ON CA_Aprendiz.Apr_Situacao = CA_SituacaoAprendiz.StaCodigo) INNER JOIN CA_InstituicoesParceiras ON CA_Aprendiz.Apr_InstParceira = CA_InstituicoesParceiras.IpaCodigo where 1 = 1 "+where+" ORDER BY CA_InstituicoesParceiras.IpaDescricao, CA_Aprendiz.Apr_Nome";
          
            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }


      


   

     

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

      
        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "69";
            MultiView1.ActiveViewIndex = 2;
        }

      

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
           BindGridView();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

      
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void BtnImprimir_Click(object sender, EventArgs e)
        {

            Session["Instituicao"] = DDInstituicaoParceira.SelectedValue;
            Session["id"] = "119";
            MultiView1.ActiveViewIndex = 1;
        }
    }
}