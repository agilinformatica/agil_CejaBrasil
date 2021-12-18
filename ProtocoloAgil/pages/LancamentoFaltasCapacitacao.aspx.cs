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
    public partial class LancamentoFaltasCapacitacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (!IsPostBack)
            {
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                preencheDropDown();
               // CarregarDropDisciplina();
            }
        }

        //protected void CarregarDropDisciplina()
        //{
        //    using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
        //    {
        //        var query = (from D in db.CA_Disciplinas
        //                     // where situacoes.Contains(S.StaCodigo)
        //                     select new { D.DisCodigo, D.DisDescricao }).ToList().OrderBy(item => item.DisDescricao);
        //        DDDisciplina.DataSource = query;
        //        DDDisciplina.DataBind();
        //    }
        //}

        /// <summary> andreghorst
        /// percorre o gridview chama a funcao para atualizar a frequencia do aluno caso o valor de sua frequencia seja diferente de .(ponto)
        /// ponto é definido como presença
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGerarRel_Click(object sender, EventArgs e)
        {
            if (ValidaDados().Equals(true))
            {
                for (int i = 0; i < gvAlunos.Rows.Count; i++)
                {
                    var textbox = (TextBox)gvAlunos.Rows[i].Cells[5].FindControl("TBPresenca");
                    if (textbox.Text.Equals(string.Empty))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                         "alert('Não pode conter campos em branco. Favor seguir a legenda acima');", true);
                        return;
                    }

                    if (!textbox.Text.Equals("."))
                    {
                        textbox.Text =  textbox.Text.ToUpper().Equals("P") ? "." : textbox.Text;
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
                //string turma  = gvAlunos.DataKeys[i][""]
                using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var Aprendiz = (from A in db.CA_AulasCapacitacaoAprendizs
                                    where A.AcpAprendiz.Equals(gvAlunos.DataKeys[i]["CodAprendiz"].ToString())
                                  && A.AcpTurma.Equals(gvAlunos.DataKeys[i]["CodTurma"].ToString())
                                  && A.AcpDataAula.Equals(Convert.ToDateTime(gvAlunos.DataKeys[i]["Data"].ToString()))
                                    select A).Single();
                    Aprendiz.AcpPresenca = tipoFalta.ToUpper();
                    Aprendiz.AcpDataAlteracao = DateTime.Now;
                    Aprendiz.AcpUsuario = Session["codigo"].ToString();
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
              //  Funcoes.TrataExcessao("000180", ex); //Erro ao alterar presença do aprendiz
            }
        }

      

        protected bool ValidaDados()
        {
            if (DD_TURMA.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                      "alert('Selecione a turma.')", true);
                return false;
            }

            //if (DDDisciplina.SelectedValue.Equals("Selecione.."))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            //                          "alert('Selecione a disciplina.')", true);
            //    return false;
            //}

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
          
                Panel2.Visible = false;
                using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var query = (from T in db.CA_Turmas
                                 where T.TurCurso.Equals("003")
                                 select new { T.TurCodigo, T.TurNome }).ToList().OrderBy(item => item.TurNome);

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


            Panel2.Visible = false;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from A in db.CA_AulasCapacitacaoAprendizs
                             where A.AcpTurma.Equals(DD_TURMA.SelectedValue)
                             select new { A.AcpDataAula }).Distinct().Take(50);
                DD_Data.DataSource = query.ToList();
                DD_Data.DataBind();
                DD_Data.Items.Insert(0, "Selecione..");
                DD_Data.SelectedIndex = 0;
            }
        }

        protected void DD_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencherGridAluno();
        }

        protected void PreencherGridAluno()
        {
            Panel2.Visible = true;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from AC in db.CA_AulasCapacitacaoAprendizs
                             join T in db.CA_Turmas on AC.AcpTurma equals T.TurCodigo
                             join A in db.CA_Aprendizs on AC.AcpAprendiz equals A.Apr_Codigo
                             where AC.AcpTurma.Equals(DD_TURMA.SelectedValue)
                             && AC.AcpDataAula.Equals(Convert.ToDateTime(DD_Data.SelectedValue))
                           //  && D.DisCodigo.Equals(DDDisciplina.SelectedValue)
                             orderby A.Apr_Nome
                             select new { CodAprendiz = A.Apr_Codigo, Nome = A.Apr_Nome, CodTurma = T.TurCodigo, Turma = T.TurNome, Data = AC.AcpDataAula, Presenca = AC.AcpPresenca}).Distinct().OrderBy(t => t.Nome);
                gvAlunos.DataSource = query.ToList();
                gvAlunos.DataBind();
            }
        }
    }
}