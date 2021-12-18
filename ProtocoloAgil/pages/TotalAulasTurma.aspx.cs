using ProtocoloAgil.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloAgil.pages
{
    public partial class TotalAulasTurma : System.Web.UI.Page
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

        private void CarregarGrid(int turma, DateTime startDate, DateTime endDate)
        {
            List<string> dates = new List<string>();
            List<DataControlField> fields = new List<DataControlField>();

            using (SqlConnection connection = new SqlConnection(GetConfig.Config()))
            {
                connection.Open();
                SqlCommand command =
                    new SqlCommand(
                       @"SELECT View_ControlePresenca.Apr_Codigo, 
                                View_ControlePresenca.Apr_Nome, 
                                View_ControlePresenca.ParUniDescricao,  
                                Month([AdiDataAula]) AS Mesref,
                                Year([AdiDataAula]) AS AnoRef,
                                Sum(Case when AdiPresenca <> 'x' then 1 else 0 end) AS Aulas, 
                                Sum(Case when AdiPresenca = '.' then 1 else 0 end) AS Presencas, 
                                Sum(Case when AdiPresenca <> '.' then 1 else 0 end) AS Faltas
                            FROM View_ControlePresenca
                            WHERE (View_ControlePresenca.AdiDataAula Between @startDate And @endDate) 
                              AND (View_ControlePresenca.AdiTurma = @turma )
                            GROUP BY View_ControlePresenca.Apr_Codigo, View_ControlePresenca.Apr_Nome, View_ControlePresenca.ParUniDescricao, Year([AdiDataAula]),  Month([AdiDataAula])
                            ORDER BY Year([AdiDataAula]), Month([AdiDataAula]), View_ControlePresenca.Apr_Nome", connection);
                var turmap = new SqlParameter("turma", turma);
                var beginDatep = new SqlParameter("startDate", startDate);
                var endDatep = new SqlParameter("endDate", endDate);
                command.Parameters.Add(turmap);
                command.Parameters.Add(beginDatep);
                command.Parameters.Add(endDatep);

                var dt = new DataTable("matricula");
                dt.Clear();

                dt.Columns.Add("Matricula");
                fields.Add(new BoundField()
                {
                    DataField = "Matricula",
                    HeaderText = "Matricula"
                });
                dt.Columns.Add("Nome");
                fields.Add(new BoundField()
                {
                    DataField = "Nome",
                    HeaderText = "Nome"
                });
                dt.Columns.Add("Parceiro/Unidade");
                fields.Add(new BoundField()
                {
                    DataField = "Parceiro/Unidade",
                    HeaderText = "Parceiro/Unidade"
                });
                dt.PrimaryKey = new[] { dt.Columns["nome"] };
                var results = command.ExecuteReader();
                while (results.Read())
                {
                    string codigo = results.GetInt32(0).ToString();
                    string name = results.GetString(1);
                    string parceiro = results.GetString(2);
                    string date = results.GetInt32(3).ToString() + "/" + results.GetInt32(4).ToString();
                    string aulas = results.GetInt32(5).ToString();
                    string presencas = results.GetInt32(6).ToString();
                    string faltas = results.GetInt32(7).ToString();
                    
                    DataRow row = dt.Rows.Find(name);
                    if (row == null)
                    {
                        row = dt.NewRow();
                        row["Nome"] = name;
                        row["Matricula"] = codigo;
                        row["Parceiro/Unidade"] = parceiro;
                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    }
                    if (!dates.Contains(date))
                    {
                        dates.Add(date);
                        dt.Columns.Add(date);
                        fields.Add(new TemplateField()
                        {
                            ItemTemplate = new HtmlTemplate(date),
                            HeaderText = "<center>" + date + "<br /> A | P | F </center>"
                        });
                    }
                    row[date] = "<center><b><span style='color: blue;'>" + aulas + "</span> | <span style='color: green;'>" + presencas + "</span> | <span style='color: red;'>" + faltas + "</span></b></center>";
                    dt.AcceptChanges();
                }
                //Trocar aqui para ele ser data source do report la ao inves de ser do grid
                //Ai basta trocar essas duas linhas aqui de baixo
                //para colocar mais uma coluna como por exemplo a matricula
                Session["DataTable"] = dt;
                GridView1.DataSource = dt;
                GridView1.AutoGenerateColumns = false;
                foreach (var field in fields)
                {
                    GridView1.Columns.Add(field);
                }
                GridView1.DataBind();

                if (GridView1.Rows.Count > 0)
                {
                    GridView1.Visible = true;
                    pn_info.Visible = false;
                    PanelLegenda.Visible = true;
                    btn_Excel.Visible = true;
                }
                else
                {
                    GridView1.Visible = false;
                    pn_info.Visible = true;
                    PanelLegenda.Visible = false;
                    btn_Excel.Visible = false;
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString(), true);
        }

        protected void btn_pesquisa_Click(object sender, EventArgs e)
        {
            if (DD_Turma.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Informe a turma que deseja pesquisar.')", true);
                return;
            }
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

            CarregarGrid(Convert.ToInt32(DD_Turma.SelectedValue), Convert.ToDateTime(tb_data_inicial.Text), Convert.ToDateTime(tb_data_final.Text));
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public void ExportToExcel(DataTable dt, string FileName)
        {
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
            string FileName = String.Format("TotalAulasTurma-{0}_{1}.xls",
            DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());
            ExportToExcel((DataTable)Session["DataTable"], FileName);
        }
    }

    public class HtmlTemplate : ITemplate
    {
        private string _collumName;
        public HtmlTemplate(string collumName)
        {
            _collumName = collumName;
        }

        public void InstantiateIn(Control container)
        {
            Literal l = new Literal();
            l.Mode = LiteralMode.PassThrough;
            l.DataBinding += new EventHandler(this.BindData);
            container.Controls.Add(l);
        }

        public void BindData(object sender, EventArgs e)
        {
            Literal l = (Literal)sender;
            GridViewRow row = (GridViewRow)l.NamingContainer;
            l.Text = DataBinder.Eval(row.DataItem, _collumName).ToString();
        }
    }
}