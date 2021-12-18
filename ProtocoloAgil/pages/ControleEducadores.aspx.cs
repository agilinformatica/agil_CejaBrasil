using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class ControleEducadores : Page
    {
        private int id_escola;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            if (!IsPostBack)
            {
                id_escola = Convert.ToInt16(Session["Escola"]);
                BindGridview(0);
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }

            if ( Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["matricula"] = GridView1.SelectedRow.Cells[0].Text;

            var con = new Conexao(id_escola);
            var dr = con.Consultar("SELECT EducNome FROM CA_Educadores WHERE EducCodigo='" + Session["matricula"] + "'");
            dr.Read();
            MultiView1.ActiveViewIndex = 1;
            Session["Comando"] = "Alterar";
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TBCodigo.Text.Equals(string.Empty) && HFcodigo.Value.Equals("0")) throw new ArgumentException("Confira o CPF.");
                BindGridview(TBNome.Text.Equals(string.Empty) && TBCodigo.Text.Equals(string.Empty)? 0 : TBNome.Text.Equals(string.Empty) ? 2 : 1);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000200", ex);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Session["Comando"] = "Inserir";
            Session["matricula"] = null;

        }

        private void BindGridview(int tipo)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var educadores = new List<CA_Educadore>();
                switch (tipo)
                {
                    case 0: educadores.AddRange( bd.CA_Educadores.OrderBy(p => p.EducNome)); break;
                    case 1: educadores.AddRange(bd.CA_Educadores.Where(p => SqlMethods.Like(p.EducNome,"%" + TBNome.Text + "%")).OrderBy(p => p.EducNome)); break;
                    case 2: educadores.AddRange(bd.CA_Educadores.Where(p => p.EducCPF.Equals(Funcoes.Retirasimbolo(TBCodigo.Text))).OrderBy(p => p.EducNome)); break;
 
                }
                GridView1.DataSource = educadores.ToList();
                HFRowCount.Value = educadores.Count().ToString();
                GridView1.DataBind();
            }

        }

        protected void listar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            GridView1.DataBind();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

    }
}