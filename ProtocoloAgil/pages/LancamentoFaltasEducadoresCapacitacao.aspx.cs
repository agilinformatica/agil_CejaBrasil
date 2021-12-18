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
    public partial class LancamentoFaltasEducadoresCapacitacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["CurrentPage"] = "geralprofessores";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (!IsPostBack)
            {
                multi.ActiveViewIndex = 0;
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                preencheDropDown();
                // CarregarDropDisciplina();
            }
        }

      

        /// <summary> andreghorst
        /// percorre o gridview chama a funcao para atualizar a frequencia do aluno caso o valor de sua frequencia seja diferente de .(ponto)
        /// ponto é definido como presença
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
      

      
       

        protected bool ValidaDados()
        {
            if (DD_TURMA.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                      "alert('Selecione a turma.')", true);
                return false;
            }

           

            if (DD_Data.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                      "alert('Selecione a data.')", true);
                return false;
            }
            else
                return true;
        }

        void preencheDropDown()
        {
            Panel2.Visible = true;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                Panel2.Visible = false;
                //var query = (from Au in db.CA_AulasDisciplinasAprendizs
                //             join A in db.CA_Aprendizs on Au.AdiCodAprendiz equals A.Apr_Codigo
                //             join T in db.CA_Turmas on Au.AdiTurma equals T.TurCodigo
                //             join D in db.CA_Disciplinas on Au.AdiDisciplina equals D.DisCodigo
                //             join E in db.CA_Educadores on Au.AdiEducador equals E.EducCodigo
                //             where E.EducCodigo.Equals(Session["codigo"])
                //             orderby T.TurNome
                //             select new { T.TurCodigo, T.TurNome }).Distinct().OrderBy(t => t.TurNome);
                

                var query = (from T in db.CA_Turmas
                             where T.TurEducadorResponsavel.Equals(Session["codigo"]) && T.TurCurso.Equals("003") 
                             orderby T.TurNome
                             select new { T.TurCodigo, T.TurNome }).Distinct().OrderBy(t => t.TurNome);

                DD_TURMA.DataSource = query;
                DD_TURMA.DataBind();
                DD_TURMA.Items.Insert(0, "Selecione..");
                DD_TURMA.SelectedIndex = 0;


              

            }
        }

        /// <summary>
        /// preenche drop para top 15 turmas onde a sua data seja 3 dias menor que a data atual e 10 dias maior que a data atual 
        /// esse procedimento é feito para que nao seja carregado muitos dados no dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DD_TURMA_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                //var query = (from Au in db.CA_AulasDisciplinasAprendizs
                //             join A in db.CA_Aprendizs on Au.AdiCodAprendiz equals A.Apr_Codigo
                //             join T in db.CA_Turmas on Au.AdiTurma equals T.TurCodigo
                //             join D in db.CA_Disciplinas on Au.AdiDisciplina equals D.DisCodigo
                //             join E in db.CA_Educadores on Au.AdiEducador equals E.EducCodigo
                //             where Au.AdiTurma.Equals(DD_TURMA.SelectedValue)
                //             && Au.AdiDataAula.Equals(Convert.ToDateTime(DD_Data.SelectedValue))
                //             && D.DisCodigo.Equals(DDDisciplina.SelectedValue)
                //             && E.EducCodigo.Equals(Session["codigo"])
                //             orderby A.Apr_Nome
                //             select new { CodAprendiz = A.Apr_Codigo, Nome = A.Apr_Nome, CodTurma = T.TurCodigo, Turma = T.TurNome, Data = Au.AdiDataAula, Presenca = Au.AdiPresenca }).Distinct().OrderBy(t => t.Nome);

                var query = (from Au in db.CA_AulasCapacitacaoAprendizs
                             where Au.AcpTurma.Equals(DD_TURMA.SelectedValue)
                               && Au.AcpDataAula > DateTime.Now.AddDays(-35)
                             && Au.AcpDataAula < DateTime.Now.AddDays(15)
                             orderby Au.AcpTurma
                             select new { Au.AcpDataAula }).Distinct().OrderBy(t => t.AcpDataAula);


                DD_Data.DataSource = query.ToList();
                DD_Data.DataBind();
                DD_Data.Items.Insert(0, "Selecione..");
                DD_Data.SelectedIndex = 0;
            }

            
        }


       
        protected void DD_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DD_Data.SelectedValue.Equals("Selecione.."))
            {
                gvAlunos.Visible = false;
                btSalvar.Visible = false;
            }
            else
            {
                PreencherGridAluno();
                gvAlunos.Visible = true;
                btSalvar.Visible = true;
            }
            
        }

        protected void PreencherGridAluno()
        {
            Panel2.Visible = true;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                 var query = (from F in db.View_CA_FaltasCapacitacaos
                             where F.AcpTurma.Equals(DD_TURMA.SelectedValue)
                             && F.AcpDataAula.Equals(Convert.ToDateTime(DD_Data.SelectedValue))
                             orderby F.Apr_Nome
                              select new { CodAprendiz = F.AcpAprendiz, Nome = F.Apr_Nome, CodTurma = F.AcpTurma, Data = F.AcpDataAula, Presenca = F.AcpPresenca }).Distinct().OrderBy(t => t.Nome);

                gvAlunos.DataSource = query.ToList();
                gvAlunos.DataBind();
            }
        }

        protected void btSalvar_Click(object sender, EventArgs e)
        {
            if (ValidaDados().Equals(true))
            {
                for (int i = 0; i < gvAlunos.Rows.Count; i++)
                {
                    var textbox = (TextBox)gvAlunos.Rows[i].Cells[4].FindControl("TBPresenca");

                    if (textbox.Text.Equals(string.Empty))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                         "alert('Não pode conter campos em branco. Favor seguir a legenda acima');", true);
                        return;
                    }

                    if (!textbox.Text.Equals("."))
                    {
                        textbox.Text = textbox.Text.ToUpper().Equals("P") ? "." : textbox.Text;
                        AtualizaDados(i, textbox.Text);
                    }


                }

                PreencherGridAluno();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Dados atualizados com sucesso.')", true);
            }
        }


        /// <summary> andreghorst 02/04/2014
        /// Atualiza a presença do aprendiz, passando como parametro i e tipo falta onde i é equivalente a posição linha do grideview
        /// e tipo falta é a letra equivalente a falta do aluno, podendo ter os seguintes valores:
        /// F - Falta.   J - Falta Justificada.   L -Licença Maternidade.   E - Licença Exército.   D - Desligado.
        /// estes valores de entrada são conferidos por uma função javascript
        /// </summary>
        /// <param name="i"></param>
        /// <param name="tipoFalta"></param>
        protected void AtualizaDados(int i, string tipoFalta)
        {
            try
            {

                var sql = "update View_CA_FaltasCapacitacao set AcpPresenca = '" + tipoFalta.ToUpper() + "', AcpUsuario = '" + Session["codigo"] + "', AcpDataAlteracao = '"+DateTime.Now+"'  where AcpAprendiz = " + gvAlunos.DataKeys[i]["CodAprendiz"].ToString() + " and AcpdataAula = '" + Convert.ToDateTime(gvAlunos.DataKeys[i]["Data"].ToString()) + "' and AcpTurma = " + gvAlunos.DataKeys[i]["CodTurma"].ToString() + "";
                var con = new Conexao();
                con.Alterar(sql);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000180", ex); //Erro ao alterar presença do aprendiz
            }
        }

        /// <summary> andreghorst 02/04/2014
        /// Atualiza a presença do aprendiz, passando como parametro i e tipo falta onde i é equivalente a posição linha do grideview
        /// e tipo falta é a letra equivalente a falta do aluno, podendo ter os seguintes valores:
        /// F - Falta.   J - Falta Justificada.   L -Licença Maternidade.   E - Licença Exército.   D - Desligado.
        /// estes valores de entrada são conferidos por uma função javascript
        /// </summary>
        /// <param name="i"></param>
        /// <param name="tipoFalta"></param>
        protected void AtualizaDadosPorLinha(string matricula, string turma, DateTime data, string presente)
        {
            try
            {
                var sql = "update View_CA_FaltasCapacitacao set AcpPresenca = '" + presente.ToUpper() + "'  where AcpAprendiz = " + matricula+ " and AcpdataAula = '" + data + "' and AcpTurma = " + turma + "";
                var con = new Conexao();
                con.Alterar(sql);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000180", ex); //Erro ao alterar presença do aprendiz
            }
        }


        protected void TBPresenca_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gridRow = ((GridViewRow)((TextBox)sender).NamingContainer);

            GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
            TextBox txtPresenca = row.FindControl("TBPresenca") as TextBox;
           


          
            Label lblSalvo = row.FindControl("lblSalvo") as Label;
          
            lblSalvo.Visible = true;
          
            //Access TextBox1 here.
            string presente = "";
            string matricula = "";
            string aluno = "";
            DateTime data = Convert.ToDateTime(gvAlunos.DataKeys[row.RowIndex]["Data"].ToString());
            string turma = gvAlunos.DataKeys[row.RowIndex]["CodTurma"].ToString();
            presente = txtPresenca.Text;
            matricula = gvAlunos.DataKeys[row.RowIndex]["CodAprendiz"].ToString();
            aluno = gridRow.Cells[1].Text;

            AtualizaDadosPorLinha(matricula, turma, data, presente );
            lblSalvo.Text = "Atualizado com sucesso";

        }

    }
}