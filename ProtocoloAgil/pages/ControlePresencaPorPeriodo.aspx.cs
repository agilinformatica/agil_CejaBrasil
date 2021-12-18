using ProtocoloAgil.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloAgil.pages
{
    public partial class ControlePresencaPorPeriodo : System.Web.UI.Page
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

            Session["TurmaRelatorio"] = DD_Turma.SelectedValue;
            Session["DataInicialRelatorio"] = tb_data_inicial.Text;
            Session["DataFinalRelatorio"] = tb_data_final.Text;

            Session["Turma"] = DD_Turma.SelectedItem;
            Session["DataInicial"] = tb_data_inicial.Text;
            Session["DataFinal"] = tb_data_final.Text;

            CarregarGrid(Convert.ToInt32(DD_Turma.SelectedValue), Convert.ToDateTime(tb_data_inicial.Text), Convert.ToDateTime(tb_data_final.Text));
            CarregarGrid2(Convert.ToInt32(DD_Turma.SelectedValue), Convert.ToDateTime(tb_data_inicial.Text), Convert.ToDateTime(tb_data_final.Text));
           
        }



        private string InverteDataDiaMes(string data)
        {
            string[] array = data.Split('/');
            data = array[1] + "/" + array[0] + "/" + array[2];
            return data;
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
            CarregarGrid(Convert.ToInt32(Session["Turma"]), Convert.ToDateTime(Session["DataInicial"]), Convert.ToDateTime(Session["DataFinal"]));
        }

        protected void SDSControlePresenca_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            var rows = e.AffectedRows;
            pn_info.Visible = (rows == 0);
            GridView1.Visible = (rows != 0);
        }

        private void CarregarGrid(int turma, DateTime startDate, DateTime endDate)
        {
            List<string> dates = new List<string>();
            using (SqlConnection connection = new SqlConnection(GetConfig.Config()))
            {
                connection.Open();
                SqlCommand command =
                    new SqlCommand(
                        @"Select [Apr_Nome],
                             AdiDataAula,
                            [AdiPresenca],
                            Apr_Codigo,
                            CASE [ParDescricao] WHEN [ParUniDescricao] THEN [ParDescricao] ELSE ([ParDescricao] + '/' + [ParUniDescricao]) END AS Parceiro,
                            DisAbreviatura   
                            from View_ControlePresenca 
                            Where AdiTurma = @turma 
                                AND AdiDataAula between @startDate and @endDate 
                            Order By Apr_Nome, AdiDataAula",
                        connection);
                var turmap = new SqlParameter("turma", turma);
                var beginDatep = new SqlParameter("startDate", startDate);
                var endDatep = new SqlParameter("endDate", endDate);
                command.Parameters.Add(turmap);
                command.Parameters.Add(beginDatep);
                command.Parameters.Add(endDatep);
                var dt = new DataTable("matricula");
                dt.Clear();
                dt.Columns.Add("Matricula");
                dt.Columns.Add("Nome");
                dt.Columns.Add("Parceiro/Unidade");
                dt.PrimaryKey = new[] { dt.Columns["Nome"] };
                var results = command.ExecuteReader();
                while (results.Read())
                {
                    string name = results.GetString(0);
                    string codigo = results.GetInt32(3).ToString();
                    string parceiro = results.GetString(4);
                    var date = results.GetDateTime(1).ToString("dd/MM") + " " + results.GetString(5) ;
                    string presenca = results.GetString(2);
                    if (!dates.Contains(date))
                    {
                        dates.Add(date);
                        dt.Columns.Add(date);
                    }
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
                    row[date] = presenca;
                    dt.AcceptChanges();
                }
                //Trocar aqui para ele ser data source do report la ao inves de ser do grid
                //Ai basta trocar essas duas linhas aqui de baixo
                //para colocar mais uma coluna como por exemplo a matricula
                Session["DataTable"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (GridView1.Rows.Count > 0) {
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


        private void CarregarGrid2(int turma, DateTime startDate, DateTime endDate)
        {
            List<string> dates = new List<string>();
            using (SqlConnection connection = new SqlConnection(GetConfig.Config()))
            {
                connection.Open();
                SqlCommand command =
                    new SqlCommand(
                        @"Select [Apr_Nome],
                            AdiDataAula,
                            [AdiPresenca],
                            Apr_Codigo,
                            CASE [ParDescricao] WHEN [ParUniDescricao] THEN [ParDescricao] ELSE ([ParDescricao] + '/' + [ParUniDescricao]) END AS Parceiro   
                            from View_ControlePresenca 
                            Where AdiTurma = @turma 
                                AND AdiDataAula between @startDate and @endDate 
                            Order By Apr_Nome, AdiDataAula",
                        connection);
                var turmap = new SqlParameter("turma", turma);
                var beginDatep = new SqlParameter("startDate", startDate);
                var endDatep = new SqlParameter("endDate", endDate);
                command.Parameters.Add(turmap);
                command.Parameters.Add(beginDatep);
                command.Parameters.Add(endDatep);
                var dt = new DataTable("matricula");
                dt.Clear();
                dt.Columns.Add("Matricula");
                dt.Columns.Add("Nome");
                dt.Columns.Add("Parceiro/Unidade");
                dt.PrimaryKey = new[] { dt.Columns["Nome"] };
                var results = command.ExecuteReader();
                while (results.Read())
                {
                    string name = results.GetString(0);
                    string codigo = results.GetInt32(3).ToString();
                    string parceiro = results.GetString(4);
                    var date = results.GetDateTime(1).ToString("dd-MM");
                    string presenca = "M:________________T:________________";
                    if (!dates.Contains(date))
                    {
                        dates.Add(date);
                        dt.Columns.Add(date);
                    }
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
                    row[date] = presenca;
                    dt.AcceptChanges();
                }
                //Trocar aqui para ele ser data source do report la ao inves de ser do grid
                //Ai basta trocar essas duas linhas aqui de baixo
                //para colocar mais uma coluna como por exemplo a matricula
                Session["DataTable2"] = dt;
                GridView2.DataSource = dt;
                GridView2.DataBind();

                if (GridView2.Rows.Count > 0)
                {
                    GridView2.Visible = false;
                    pn_info.Visible = false;
                    btn_Excel.Visible = true;
                    //btnListaPresencaMensal.Visible = true;
                }
                
            }
        }



        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            Session["id"] = 82;
            MultiView1.ActiveViewIndex = 1;
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


                Response.ContentType = "application/vnd.ms-excel 8.0";
                Response.Charset = "";
                Response.ContentEncoding = System.Text.Encoding.Default;
                

                

                this.EnableViewState = false;

                // Obtém o caminho do diretório correto
                string p2 = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                var filePath = p2 + "/images/logo_fundacao_732.jpg";
                var filePath2 = Server.MapPath(@"/images/logo_fundacao_732.jpg");

                var con = new Conexao();
                var sql = "Select UniNome, UniEndereco, UniNumeroEndereco, UniCEP, UniCidade, UniEstado, UniBairro from CA_Unidades U join CA_Turmas T On T.TurUnidade = U.UniCodigo where T.turCodigo = "+DD_Turma.SelectedValue+"";
                var result = con.Consultar(sql);
                var endereco = "";
                while (result.Read()) {

                    endereco = result["Uninome"].ToString() + "<br/> " + result["UniEndereco"].ToString() + ", " + result["UniNumeroEndereco"].ToString() + ", " + result["UniBairro"].ToString() + ", " + result["UniCidade"].ToString() + ", " + result["UniEstado"].ToString();
                }

                Response.Write("<b>" + endereco + "<b/>");
             //   Response.Write("<img src='" + filePath2 + "'/>");
                Response.Write("<br/>");
                Response.Write("<br/>");
                Response.Write("<br/>");
                Response.Write("<p align='center'><b> Controle de Presença de Turma por Período De " + tb_data_inicial.Text + " à " + tb_data_final.Text + " <b/><p/>");
                Response.Write("<br/>");
                Response.Write("<p align='left'><b> Legenda: F - Falta,   J - Falta Justificada,   L - Licença Maternidade,   S - Serviço Militar,   D - Desligado,  . - Presença<b/><p/>");
                Response.Write("<br/>");
                Response.Write("<br/>");
                
                dgGrid.RenderControl(hw);
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");

                Response.Write(tw.ToString());
                Response.End();
            }
        }

        public void ExportToExcelPresencaMensal(DataTable dt, string FileName)
        {
            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                 hw.AddStyleAttribute("font-size", "5pt");
                



                Response.ContentType = "application/vnd.ms-excel 8.0";
                Response.Charset = "";
                Response.ContentEncoding = System.Text.Encoding.Default;




                this.EnableViewState = false;

                // Obtém o caminho do diretório correto
                string p2 = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                var filePath = p2 + "/images/logo_fundacao_732.jpg";
                var filePath2 = Server.MapPath(@"/images/logo_fundacao_732.jpg");

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
                Response.Write("<p align='center'><b> Controle de Presença de Turma por Período De " + tb_data_inicial.Text + " à " + tb_data_final.Text + " <b/><p/>");
                Response.Write("<br/>");
                Response.Write("<p align='left'><b> Legenda: F - Falta,   J - Falta Justificada,   L - Licença Maternidade,   S - Serviço Militar,   D - Desligado,  . - Presença<b/><p/>");
                Response.Write("<br/>");
                Response.Write("<br/>");

                dgGrid.RenderControl(hw);
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");

                Response.Write(tw.ToString());
                Response.End();
            }
        }



        protected void btn_Excel_Click(object sender, EventArgs e)
        {
            string FileName = String.Format("ControlePresencaTurma-{0}_{1}.xls",
            DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());
            ExportToExcel((DataTable)Session["DataTable"], FileName);
        }
        protected void btnListaPresencaMensal_Click(object sender, EventArgs e)
        {

            var sql = "Select TurDiaSemana from CA_Turmas where TurCodigo = " + Session["TurmaRelatorio"] + "";
            var con = new Conexao();
            var result = con.Consultar(sql);
            string diaSemana = "";
            string unidade = "";
            while(result.Read()){

                diaSemana = result["TurDiaSemana"].ToString();
            }


           

            while (result.Read()) {

                unidade = result["Unidade"].ToString().Equals("") ? " " : result["Unidade"].ToString();
            }

            if (unidade.Equals(""))
            {
                unidade = " ";
            }

            switch (diaSemana)
            {
                case "1":
                    diaSemana = "Domingo";
                break;
                case "2":
                diaSemana = "Segunda-Feira";
                break;
                case "3":
                diaSemana = "Terça-Feira";
                break;
                case "4":
                diaSemana = "Quarta-Feira";
                break;
                case "5":
                diaSemana = "Quinta-Feira";
                break;
                case "6":
                diaSemana = "Sexta-Feira";
                break;
                case "7":
                diaSemana = "Sábado";
                break;
            }

            Session["NomeUnidade"] = unidade;
            Session["DiaSemana"] = diaSemana;



            Session["id"] = 88;
            MultiView1.ActiveViewIndex = 1;
        }

       

        class ControlePresenca
        {
            
            string nome;
            string codigo;
            string parceiro;
            List<string> data;
            string presenca;

            public string getNome(string nome)
            {
                return nome;
            }

            public void setNome(string nome)
            {
                this.nome = nome;
            }

            public string getCodigo(string codigo)
            {
                return codigo;
            }

            public void setCodigo(string codigo)
            {
                this.codigo = codigo;
            }

            public string getParceiro(string parceiro)
            {
                return parceiro;
            }

            public void setParceiro(string parceiro)
            {
                this.parceiro = parceiro;
            }


            public List<string> getData(List<string> data)
            {
                return data;
            }

            public void setData(List<string> data)
            {
                this.data = data;
            }

            public string getPresenca(string presenca)
            {
                return presenca;
            }

            public void setPresenca(string presenca)
            {
                this.presenca = presenca;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }


        

    }
}