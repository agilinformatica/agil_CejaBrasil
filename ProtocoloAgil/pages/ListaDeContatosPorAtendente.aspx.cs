﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.Drawing;


namespace ProtocoloAgil.pages
{
    public partial class ListaDeContatosPorAtendente : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            //if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
             

              
            }

            if (IsPostBack) return;

            CarregaUsuario();
            BindGridView();
            Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            //TBCodigo_indice.Enabled = false;
         
            MultiView1.ActiveViewIndex = 1;
        }

     
        private void BindGridView()
        {

            var where = "";

            //if (!txtDataInicio.Text.Equals(string.Empty) && !txtDataTermino.Text.Equals(string.Empty))
            //{
            //    where += " and cocDataContato >= '" + txtDataInicio.Text + "' and cocDataContato <= '" + txtDataTermino.Text + "'";
            //}

            if (!DDAtendentePesquisa.SelectedValue.Equals(string.Empty))
            {
                where += " and CacAtendente = '" + DDAtendentePesquisa.SelectedValue + "'";
            }

            if (DDFechamento.SelectedValue.Equals("A"))
            {
                where += " and CocDataFechamento  is null";
            }


            var sql = "Select cocDataContato, T.Tco_Descricao, F.FechDescricao, C.CocUsuarioContato, C.CocDescricaoContato, CC.CacNome, S.StcCodigo   from CA_contatos C   left join CA_TiposContatos T on C.CocTipo = T.Tco_Codigo  left join CA_fechamentosContatos F on C.CocCodigoFechamento = F.FechCodigo left join CA_CadastroClientes CC on C.CocCliente = CC.CacCodigo left join CA_StatusCliente S on CC.CacStatus = S.StcCodigo where 1 = 1 " + where + "";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }


        public void CarregaUsuario()
        {
            // CARREGAR GRID SEM LINQ
            var sql = "SELECT * FROM Ca_Usuarios ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };


            DDAtendentePesquisa.DataSource = datasource;
            DDAtendentePesquisa.DataBind();
        }

    
        protected void listar_Click(object sender, EventArgs e)
        {
            
           // BindGridView(DDClientePesquisa.SelectedValue.Equals(string.Empty) ? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

      
     

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            //if (!txtDataInicio.Text.Equals(string.Empty) && !txtDataTermino.Text.Equals(string.Empty))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
            //           "alert('As datas de início e término são obrigatórias');", true);
            //    return;
            //}

          
            BindGridView();
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[8].Text.Equals("1"))
                {
                    e.Row.Cells[7].BackColor = Color.FromName("red");
                }

                if (e.Row.Cells[8].Text.Equals("2"))
                {
                    e.Row.Cells[7].BackColor = Color.FromName("green");
                }

                if (e.Row.Cells[8].Text.Equals("3"))
                {
                    e.Row.Cells[7].BackColor = Color.FromName("blue");
                }

                if (e.Row.Cells[8].Text.Equals("4"))
                {
                    e.Row.Cells[7].BackColor = Color.FromName("yellow");
                }

                if (e.Row.Cells[8].Text.Equals("5"))
                {
                    e.Row.Cells[7].BackColor = Color.FromName("black");
                }
                if (e.Row.Cells[8].Text.Equals("6"))
                {
                    e.Row.Cells[7].BackColor = Color.FromName("gray");
                }

            }
        }
     
    }
}