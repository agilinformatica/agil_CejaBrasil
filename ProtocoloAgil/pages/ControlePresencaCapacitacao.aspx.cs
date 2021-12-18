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
    public partial class ControlePresencaCapacitacao : System.Web.UI.Page
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

        private void BindGridView1()
        {
            try
            {
                var sql = "Select TurNome, Apr_Codigo, Apr_Nome, uniNome, acpPresenca  from View_CA_AulasCapacitacao where AcpDataAula = '" + tb_data.Text + "'";
                if (!DD_Turma.SelectedValue.Equals(string.Empty))
                    sql += " and [turCodigo] = '" + DD_Turma.SelectedValue + "'";
                sql += " and (Apr_FimAprendizagem >= '" + tb_data.Text + "'" + " OR Apr_FimAprendizagem Is Null )" ;
                sql += " ORDER BY TurNome, Apr_Nome ";
                SDSControlePresenca.SelectCommand = sql;
                ViewState["sql"] = sql;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000125", ex);
            }
        }

        protected void btn_pesquisa_Click(object sender, EventArgs e)
        {
            if (tb_data.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Informe a data para pesquisar.')", true);
                return;
            }
            BindGridView1();
        }

        protected void SDSControlePresenca_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            var rows = e.AffectedRows;
            Panel1.Visible = (rows == 0);
            GridView1.Visible = (rows != 0);
            btn_imprimir.Visible = (rows != 0);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView1();
        }

        protected void btn_Listar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            GridView1.Visible = true;
        }

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            Session["id"] = 98;
            Session["Data"] = tb_data.Text;
            Session["Turma"] = DD_Turma.SelectedValue;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btnConteudo_Click(object sender, EventArgs e)
        {
            BindGridViewConteudo();
        }


        private void BindGridViewConteudo()
        {
            try
            {
                var sql = "SELECT CA_AulasDisciplinasTurmaProf.ADPTurma, CA_Turmas.TurNome,  CA_Turmas.TurObservacoes,  CA_AulasDisciplinasTurmaProf.ADPDataAula,  CA_AulasDisciplinasTurmaProf.ADPOrdemAula,  CA_AulasDisciplinasTurmaProf.ADPprofessor,  CA_Educadores.EducNome,  CA_AulasDisciplinasTurmaProf.ADPDisciplina,  CA_AulasDisciplinasTurmaProf.ADPConteudoLecionado,  CA_AulasDisciplinasTurmaProf.ADPRecursosUsados,  CA_AulasDisciplinasTurmaProf.ADPObservacoes, D.DisDescricao FROM CA_AulasDisciplinasTurmaProf INNER JOIN CA_Educadores ON CA_AulasDisciplinasTurmaProf.ADPprofessor = CA_Educadores.EducCodigo INNER JOIN CA_Disciplinas ON CA_AulasDisciplinasTurmaProf.ADPDisciplina = CA_Disciplinas.DisCodigo INNER JOIN CA_Turmas ON CA_AulasDisciplinasTurmaProf.ADPTurma = CA_Turmas.TurCodigo Inner Join CA_Disciplinas D on CA_AulasDisciplinasTurmaProf.ADPDisciplina = D.DisCodigo WHERE CA_AulasDisciplinasTurmaProf.ADPDataAula='" + Convert.ToDateTime(tb_data.Text) + "' ";

                if (!DD_Turma.SelectedValue.Equals(string.Empty))
                {
                    sql += " and CA_AulasDisciplinasTurmaProf.ADPTurma = '" + DD_Turma.SelectedValue + "'";
                }
                //sql += " and (Apr_FimAprendizagem >= '" + tb_data.Text + "'" + " OR Apr_FimAprendizagem Is Null )";
                sql += " ORDER BY CA_AulasDisciplinasTurmaProf.ADPTurma, CA_AulasDisciplinasTurmaProf.ADPOrdemAula";
                  
                SDSConteudo.SelectCommand = sql;
                ViewState["sql"] = sql;
                GridConteudo.DataBind();
                GridConteudo.Visible = true;
                MultiView1.ActiveViewIndex = 2;
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000125", ex);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }


        protected void SDSConteudo_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            var rows = e.AffectedRows;
            Panel2.Visible = (rows == 0);
            GridConteudo.Visible = (rows != 0);
        }
    }
}