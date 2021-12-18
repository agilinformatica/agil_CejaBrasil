using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ProtocoloAgil.pages
{
    public partial class TesteControleTurmas : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            grdLista.DataSource = carregaGrid();

            grdLista.DataBind();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnSalvarNovo_Click(object sender, EventArgs e)
        {
            try
            {
                string novo = @"INSERT INTO [dbo].[CA_Turmas]
                           ([TurCurso]
                           ,[TurNome]
                           ,[TurDiaSemana]
                           ,[TurInicio]
                           ,[Termino]
                           ,[TurPlanoCurricular])
                            VALUES
                           ('" + txtCurso.Text + @"'
                           ,'" + txtNome.Text + @"'
                           ,'" + txtDia.Text + @"'
                           ,'" + DateTime.Parse(txtDataIni.Text) + @"'
                           ,'" + DateTime.Parse(txtDataTer.Text) + @"'
                           , 1)";

                SqlCommand cmd = new SqlCommand(novo, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException sql)
            {
                Console.WriteLine(sql.Message);
                Console.WriteLine(sql.Errors);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                MultiView1.ActiveViewIndex = 0;
            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {

        }

        protected void grdLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {
                MultiView1.ActiveViewIndex = 2;

                int index = Convert.ToInt32(e.CommandArgument); 
                txtEditCurso.Text = grdLista.Rows[index].Cells[1].Text;
                txtEditNome.Text = grdLista.Rows[index].Cells[2].Text;
                txtEditDia.Text = grdLista.Rows[index].Cells[3].Text;
                txtEditInicio.Text = grdLista.Rows[index].Cells[4].Text;
                txtEditTermino.Text = grdLista.Rows[index].Cells[5].Text;
            }

            
        }


        protected void btnSalvarEdit_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }


        private DataTable carregaGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = @"Data Source=191.234.176.236;
                            Initial Catalog=aprendiz_CejaBrasil;
                            Persist Security Info=True;
                            User ID=SA;
                            Password=Mestreagil3415";
                conn.Open();

                string sqlString = @"SELECT [TurCodigo]
                                       ,[TurCurso]
                                       ,[TurNome]
                                       ,[TurDiaSemana]
                                       ,[TurInicio]
                                       ,[Termino]
                                       FROM [CA_Turmas]";

                SqlCommand command = new SqlCommand(sqlString, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);
            }
            catch (SqlException sqx)
            {
                Console.WriteLine(sqx.Message);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return dt;
            }
            return dt;
        }

        

    }
}