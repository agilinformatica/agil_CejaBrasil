using ProtocoloAgil.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloAgil.pages
{
    public partial class TotalAulasDoParceiro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
            }
        }

        private void CarregarGrid(int pParceiro, DateTime startDate, DateTime endDate)
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
                                Sum(Case when AdiPresenca <> '.' then 1 else 0 end) AS Faltas,
                                Sum(Case when AdiPresenca = 'J' then 1 else 0 end) AS Justif
                            FROM View_ControlePresenca
                            WHERE (View_ControlePresenca.AdiDataAula Between @startDate And @endDate) 
                              AND (ParCodigo = @parceiro  )
                            GROUP BY View_ControlePresenca.Apr_Codigo, View_ControlePresenca.Apr_Nome, View_ControlePresenca.ParUniDescricao, Year([AdiDataAula]),  Month([AdiDataAula])
                            ORDER BY Year([AdiDataAula]), Month([AdiDataAula]), View_ControlePresenca.Apr_Nome", connection);
                var parceirop = new SqlParameter("parceiro", pParceiro);
                var beginDatep = new SqlParameter("startDate", startDate);
                var endDatep = new SqlParameter("endDate", endDate);
                command.Parameters.Add(parceirop);
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
                dt.PrimaryKey = new[] { dt.Columns["Nome"] };
                var results = command.ExecuteReader();
                while (results.Read())
                {
                    string codigo = results.GetInt32(0).ToString();
                    string name = results.GetString(1);
                    string parceiro = results.GetString(2);
                    string date = results.GetInt32(3).ToString() + "/" + results.GetInt32(4).ToString() + "<br /> A | P | F | J</center>";
                    string aulas = results.GetInt32(5).ToString();
                    string presencas = results.GetInt32(6).ToString();
                    string faltas = results.GetInt32(7).ToString();
                    string Justif = results.GetInt32(8).ToString();

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
                            //HeaderText = "<center>" + date + "<br /> A | P | F | J</center>"
                            HeaderText = "<center>" + date + "</center>"
                            
                        });
                    }
                    row[date] = "<center><b><span style='color: blue;'>" + aulas + "</span> | <span style='color: green;'>" + presencas + "</span> | <span style='color: red;'>" + faltas + "</span> | <span style='color: black;'>" + Justif + "</span></b></center>";
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
                    PanelLegenda.Visible = true;
                    btn_Excel.Visible = true;
                }
                else
                {
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

                //Pega o nome do parceiro logado. É inserido na sessão após o login.
                string nomeParceiro = Session["Nome"].ToString();

                string p2 = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                var filePath = p2 + "/images/logo_fundacao_732.jpg";


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
                Response.Write("<p align='center'><b> Total Por Período De " + tb_data_inicial.Text + " à " + tb_data_final.Text + " <b/><p/>");
                Response.Write("<br/>");
                Response.Write("Parceiro: " + nomeParceiro);
                Response.Write("<br/>");
                Response.Write("<b>Legenda: <br /> <span style='color: blue'>Aulas</span> &nbsp;|&nbsp; <span style='color: green'>Presenças</span> &nbsp;|&nbsp; <span style='color: red'>Faltas</span>  &nbsp;|&nbsp;  <span style='color: black'>Faltas Justificadas</span> </b>");
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
            string FileName = String.Format("TotalAulas-{0}_{1}.xls",
            DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());
            ExportToExcel((DataTable)Session["DataTable"], FileName);
        }
    }
}