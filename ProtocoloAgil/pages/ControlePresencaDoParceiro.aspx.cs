using ProtocoloAgil.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloAgil.pages
{
    public partial class ControlePresencaDoParceiro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
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

            Session["DataInicial"] = tb_data_inicial.Text;
            Session["DataFinal"] = tb_data_final.Text;

            CarregarGrid(Convert.ToInt32(Session["codigo"]), Convert.ToDateTime(tb_data_inicial.Text), Convert.ToDateTime(tb_data_final.Text));

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
            CarregarGrid(Convert.ToInt32(Session["codigo"]), Convert.ToDateTime(Session["DataInicial"]), Convert.ToDateTime(Session["DataFinal"]));
        }

        protected void SDSControlePresenca_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            var rows = e.AffectedRows;
            pn_info.Visible = (rows == 0);
            GridView1.Visible = (rows != 0);
        }


    //     (Case when AdiPresenca = 'F' then 'V' 
    //else (Case when AdiPresenca = 'J' then 'A' 
    //else (Case when AdiPresenca = '.' and AdiDataAula < getdate() then 'E' else 'Z' end) end ) end )  AS controle,
        private void CarregarGrid(int parceiro, DateTime startDate, DateTime endDate)
        {
            List<string> dates = new List<string>();
            using (SqlConnection connection = new SqlConnection(GetConfig.Config()))
            {
                connection.Open();
                SqlCommand command =
                    new SqlCommand(
                        @"Select [Apr_Nome],
                                 AdiDataAula,
                                 (Case when AdiPresenca = 'F' then 'F' 
                                    else (Case when AdiPresenca = 'J' then 'J' 
                                    else (Case when AdiPresenca = '.' and AdiDataAula < getdate() then '.' else '' end) end ) end )  AS controle,
                                 Apr_Codigo,
                                 ParUniDescricao,
                                 TurNome,
                                 DisAbreviatura  
                            from View_ControlePresenca 
                            Where ParCodigo = @parceiro 
                                AND AdiDataAula between @startDate and @endDate 
                            Order By Apr_Nome, AdiDataAula",
                        connection);
                var parceirop = new SqlParameter("parceiro", parceiro);
                var beginDatep = new SqlParameter("startDate", startDate);
                var endDatep = new SqlParameter("endDate", endDate);
                command.Parameters.Add(parceirop);
                command.Parameters.Add(beginDatep);
                command.Parameters.Add(endDatep);
                var dt = new DataTable("matricula");
                dt.Clear();
                dt.Columns.Add("Matricula");
                dt.Columns.Add("Nome");
                dt.Columns.Add("Unidade");
                dt.Columns.Add("Turma");
                dt.PrimaryKey = new[] { dt.Columns["Nome"] };
                var results = command.ExecuteReader();
                while (results.Read())
                {
                    string name = results.GetString(0);
                    string codigo = results.GetInt32(3).ToString();
                    string unidadeParceiro = results.GetString(4);
                    string turma = results.GetString(5);
                    var date = results.GetDateTime(1).ToString("dd/MM") + " " + results.GetString(6);

                    string presenca = results.GetString(2);
                    if (!dates.Contains(date))
                    {
                        date = date.Replace("-", ".");
                        
                        dates.Add(date);
                        dt.Columns.Add(date);
                    }
                    DataRow row = dt.Rows.Find(name);
                    if (row == null)
                    {
                        row = dt.NewRow();
                        row["Nome"] = name;
                        row["Matricula"] = codigo;
                        row["Unidade"] = unidadeParceiro;
                        row["Turma"] = turma;
                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    }
                    row[date] = presenca;

                    
                   
                    dt.AcceptChanges();
                }
                //Trocar aqui para ele ser data source do report la ao inves de ser do grid
                //Ai basta trocar essas duas linhas aqui de baixo
                //para colocar mais uma coluna como por exemplo a matricula
                Session["DataTable"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                if (GridView1.Rows.Count > 0)
                {
                    GridView1.Visible = true;
                    pn_info.Visible = false;
                    btn_Excel.Visible = true;
                }
                else
                {
                    GridView1.Visible = false;
                    pn_info.Visible = true;
                    btn_Excel.Visible = false;
                }
            }
        }

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            Session["id"] = 83;
            MultiView1.ActiveViewIndex = 1;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public void ExportToExcel(DataTable dt, string FileName)
        {
            foreach (DataColumn column in dt.Columns)
            {
              if (column.ColumnName.Contains("-")) {
                    dt.Columns[column.ColumnName].ColumnName = column.ColumnName.Replace("-", "<br/>");
                }
            }

            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.GridLines = GridLines.Both;
                dgGrid.HeaderStyle.Font.Bold = true;
                dgGrid.RenderControl(hw);

                string nomeParceiro = Session["Nome"].ToString();

                string p2 = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                var filePath = p2 + "/images/logo_fundacao_732.jpg";
                var filePath2 = Server.MapPath(@"/images");

                var con = new Conexao();
                var sql = "Select UniNome, UniEndereco, UniNumeroEndereco, UniCEP, UniCidade, UniEstado, UniBairro from CA_Unidades";
                var result = con.Consultar(sql);
                var endereco = "";
                while (result.Read())
                {

                    endereco = result["Uninome"].ToString() + "<br/> " + result["UniEndereco"].ToString() + ", " + result["UniNumeroEndereco"].ToString() + ", " + result["UniBairro"].ToString() + ", " + result["UniCidade"].ToString() + ", " + result["UniEstado"].ToString();
                }

                Response.Write("<b>" + endereco + "<b/>");
                //   Response.Write("<img src='" + filePath2 + "'/>");
                Response.Write("<br/>");
                Response.Write("<br/>");
                Response.Write("<br/>");
                Response.Write("<p align='center'><b >Controle de Presença por Período dos Aprendizes Alocados De " + tb_data_inicial.Text + " à " + tb_data_final.Text + " <b/><p/>");
                Response.Write("<br/>");
                Response.Write("<p align='left'><b> Legenda.: F - Falta,   J - Falta Justificada,   L -Licença Maternidade,   S - Serviço Militar,   D - Desligado,  . - Presença<b/><p/>");
                Response.Write("<br/>");
                Response.Write("Parceiro: " + nomeParceiro);
                Response.Write("<br/>");

                Response.ContentType = "application/vnd.ms-excel 8.0";
                Response.Charset = "";
                Response.ContentEncoding = System.Text.Encoding.Default;

                this.EnableViewState = false;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");

                Response.Write(tw.ToString());
                Response.End();
            }
        }

        protected void btn_Excel_Click(object sender, EventArgs e)
        {
            string FileName = String.Format("ControlePresenca-{0}_{1}.xls",
            DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());
            ExportToExcel((DataTable)Session["DataTable"], FileName);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
                //colorindo uma linha com base no conteúdo de uma célula
            
            //for (int i = 0; i < e.Row.Cells.Count; i++) {
               
               
            
            //    switch (e.Row.Cells[i].Text)
            //    {
            //        case "A":
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
            //            e.Row.Cells[i].ForeColor = System.Drawing.Color.Yellow;
            //            break;
            //        case "V":
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
            //            e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
            //            break;
            //        case "E":
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
            //            e.Row.Cells[i].ForeColor = System.Drawing.Color.Green;
            //            break;
            //        case "Z":
            //            e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
            //            break;
            //    }
            //}
        }

    }
}