using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class Cronograma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";  
            Panel2.Visible = false;         
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (!IsPostBack)
            {
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                PreencheDropDownTurma();
                PreencheDropDownDisciplinaTurma();
            }
          

        }



        void PreencheDropDownTurma()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                              
                var query = (from T in db.CA_Turmas 
                             select new { T.TurCodigo, T.TurNome });
                
                DDturma_pesquisa.DataSource = query;
                DDturma_pesquisa.DataBind();
                DDturma_pesquisa.Items.Insert(0, "Selecione..");
                DDturma_pesquisa.SelectedIndex = 0;
            }
        }


        void PreencheDropDownTurmaDisc()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = (from T in db.CA_Turmas
                             select new { T.TurCodigo, T.TurNome });

                DDTurma.DataSource = query;
                DDTurma.DataBind();
                DDTurma.Items.Insert(0, "Selecione..");
                DDTurma.SelectedIndex = 0;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_Intervalo_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }      
     
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (DataInicial.Text.Equals(string.Empty)) {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('Data de início obrigatório.');", true);
                return;
            }



            if (DataFinal.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('Data de término obrigatório.');", true);
                return;
            }

            Session["DataInicial"] = DataInicial.Text;
            Session["Datafinal"] = DataFinal.Text;           
            Session["id"] = 77;
            Panel1.Visible = true;            
        }

        protected void btnCronogramaTurma_Click(object sender, EventArgs e)
        {

            if (DDturma_pesquisa.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('Selecione a Turma.');", true);
                return;
            }

            Session["DataInicial"] = tb_inicio_pesquisa.Text;
            Session["Datafinal"] = tb_final_pesquisa.Text;
            Session["TurCodigo"] = DDturma_pesquisa.SelectedValue;
            Session["Turma"] = DDturma_pesquisa.SelectedItem.Text;

            Session["id"] = 76;
            Panel2.Visible = true;            
        }

        protected void btnCronogramaDisciplina_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            PreencheDropDownDisciplina();
        }

        protected void BtnDiscPesquisar_Click(object sender, EventArgs e)
        {

            //if (TB_DiscDataInicial.Text.Equals(string.Empty))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
            //       "alert('Preencha a data');", true);
            //}
            if (DDDisciplina.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('Disciplina obrigatória.');", true);
                return;
            }
            
            Session["DataInicial"] = TB_DiscDataInicial.Text;
            Session["Datafinal"] = TB_DiscDataFinal.Text;
            Session["CodDisciplina"] = DDDisciplina.SelectedValue;
            Session["NomeDisciplina"] = DDDisciplina.SelectedItem.Text;
            Session["id"] = 78;
            Panel3.Visible = true;     
        }


        void PreencheDropDownDisciplina()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = (from D in db.CA_Disciplinas
                             select new { D.DisCodigo, D.DisDescricao});
              
                DDDisciplina.DataSource = query;
                DDDisciplina.DataBind();
                DDDisciplina.Items.Insert(0, "Selecione..");
                DDDisciplina.SelectedIndex = 0;
            }
        }

        void PreencheDropDownDisciplinaTurma()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = (from D in db.CA_Disciplinas
                             select new { D.DisCodigo, D.DisDescricao });

                DDDisciplinaTurma.DataSource = query;
                DDDisciplinaTurma.DataBind();
                DDDisciplinaTurma.Items.Insert(0, "Selecione..");
                DDDisciplinaTurma.SelectedIndex = 0;
            }
        }


        protected void BtnGerarCores_Click(object sender, EventArgs e)
        {
            Session["id"] = 79;
            Panel4.Visible = true;     
        }

        protected void btnCores_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
        }

        protected void btnDisciplinasTurma_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            PreencheDropDownDisciplinaTurma();
            PreencheDropDownTurmaDisc();
        }

        protected void btnCronogramaTurmaDisciplina_Click(object sender, EventArgs e)
        {

            if (DDTurma.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('Turma obrigatório.');", true);
                return;
            }

            if (DDDisciplinaTurma.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                   "alert('Disciplina obrigatório.');", true);
                return;
            }

            Session["DataInicial"] = txtDataInicio.Text;
            Session["Datafinal"] = txtDataTermino.Text;
            Session["TurCodigo"] = DDTurma.SelectedValue;
            Session["Turma"] = DDTurma.SelectedItem.Text;
            Session["Disciplina"] = DDDisciplinaTurma.SelectedValue;


            Session["id"] = 200;
            Panel5.Visible = true;  
        }




    }
}