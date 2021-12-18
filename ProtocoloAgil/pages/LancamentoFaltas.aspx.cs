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
    public partial class LancamentoFaltas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["CurrentPage"] = "pedagogico";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (!IsPostBack)
            {
                tabelaLancamento.Visible = false;
                multi.ActiveViewIndex = 0;
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                preencheDropDown();
                // CarregarDropDisciplina();
            }
        }

        protected void CarregarDropDisciplina()
        {
            //using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            //{
            //    var query = (from D in db.CA_Disciplinas
            //                 // where situacoes.Contains(S.StaCodigo)
            //                 select new { D.DisCodigo, D.DisDescricao }).ToList().OrderBy(item => item.DisDescricao);
            //    DDDisciplina.DataSource = query;
            //    DDDisciplina.DataBind();
            //}
        }

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

                    
                    var  textboxAtraso = (TextBox)gvAlunos.Rows[i].Cells[6].FindControl("TBAtraso");
                    
                    if (textbox.Text.Equals(string.Empty))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                         "alert('Não pode conter campos em branco. Favor seguir a legenda acima');", true);
                        return;
                    }

                    //if (!textbox.Text.Equals(".") || !textboxAtraso.Text.Equals(string.Empty))
                    //{
                        textbox.Text = textbox.Text.ToUpper().Equals("P") ? "." : textbox.Text;
                        AtualizaDados(i, textbox.Text, textboxAtraso.Text);
                   // }


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
        protected void AtualizaDados(int i, string tipoFalta, string atraso)
        {
            try
            {



                //string turma  = gvAlunos.DataKeys[i][""]
                using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var Aprendiz = (from A in db.CA_AulasDisciplinasAprendizs
                                    join Ap in db.CA_Aprendizs on A.AdiCodAprendiz equals Ap.Apr_Codigo
                                    where A.AdiCodAprendiz.Equals(gvAlunos.DataKeys[i]["CodAprendiz"].ToString())
                                  && A.AdiTurma.Equals(gvAlunos.DataKeys[i]["CodTurma"].ToString())
                                  && A.AdiDataAula.Equals(Convert.ToDateTime(gvAlunos.DataKeys[i]["Data"].ToString()))
                                  && Ap.Apr_Situacao.Equals("6")
                                //  && A.AdiDisciplina.Equals(DDDisciplina.SelectedValue)
                                    select A).Single();
                    Aprendiz.AdiPresenca = tipoFalta.ToUpper();
                    Aprendiz.AdiAtraso = atraso;
                    Aprendiz.AdiDataAlteracao = DateTime.Now;
                    Aprendiz.AdiUsuario = Session["codigo"].ToString();
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000180", ex); //Erro ao alterar presença do aprendiz
            }
        }

        protected void DDDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from Au in db.CA_AulasDisciplinasAprendizs
                             where Au.AdiTurma.Equals(DD_TURMA.SelectedValue)
                             && Au.AdiDataAula > DateTime.Now.AddDays(-35)
                             && Au.AdiDataAula < DateTime.Now.AddDays(15)
                         //    && Au.AdiDisciplina.Equals(DDDisciplina.SelectedValue)
                             select new { Au.AdiDataAula }).Distinct().Take(30);
                DD_Data.DataSource = query;
                DD_Data.DataBind();
                DD_Data.Items.Insert(0, "Selecione..");
                DD_Data.SelectedIndex = 0;


            }
        }

        protected void DDDisciplinaLancamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from Au in db.CA_AulasDisciplinasAprendizs
                             where Au.AdiTurma.Equals(DDTurmaLancamento.SelectedValue)
                             && Au.AdiDataAula > DateTime.Now.AddDays(-35)
                             && Au.AdiDataAula < DateTime.Now.AddDays(15)
                           //  && Au.AdiDisciplina.Equals(DDDisciplinaLancamento.SelectedValue)
                             select new { Au.AdiDataAula }).Distinct().Take(30);


                DDDataLancamento.DataSource = query;
                DDDataLancamento.DataBind();
                DDDataLancamento.Items.Insert(0, "Selecione..");
                DDDataLancamento.SelectedIndex = 0;
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
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                Panel2.Visible = false;
                var query = (from Au in db.CA_AulasDisciplinasAprendizs
                             join A in db.CA_Aprendizs on Au.AdiCodAprendiz equals A.Apr_Codigo
                             join T in db.CA_Turmas on Au.AdiTurma equals T.TurCodigo
                             join D in db.CA_Disciplinas on Au.AdiDisciplina equals D.DisCodigo
                             join E in db.CA_Educadores on Au.AdiEducador equals E.EducCodigo
                             orderby T.TurNome
                             select new { T.TurCodigo, T.TurNome }).Distinct().OrderBy(t => t.TurNome);

                DD_TURMA.DataSource = query;
                DD_TURMA.DataBind();
                DD_TURMA.Items.Insert(0, "Selecione..");
                DD_TURMA.SelectedIndex = 0;


                DDTurmaLancamento.DataSource = query;
                DDTurmaLancamento.DataBind();
                DDTurmaLancamento.Items.Insert(0, "Selecione..");
                DDTurmaLancamento.SelectedIndex = 0;

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
            //using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            //{
            //    var query = (from D in db.CA_Disciplinas
            //                 join A in db.CA_AulasDisciplinasAprendizs on D.DisCodigo equals A.AdiDisciplina
            //                 where A.AdiTurma.Equals(DD_TURMA.SelectedValue)
            //                 select new { D.DisCodigo, D.DisDescricao }).Distinct().ToList().OrderBy(item => item.DisDescricao);
              
            //    DDDisciplina.DataSource = query;
            //    DDDisciplina.DataBind();
            //    DDDisciplina.Items.Insert(0, "Selecione..");
            //    DDDisciplina.SelectedIndex = 0;
            //}





            Panel2.Visible = false;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from Au in db.CA_AulasDisciplinasAprendizs
                             where Au.AdiTurma.Equals(DD_TURMA.SelectedValue)
                             && Au.AdiDataAula > DateTime.Now.AddDays(-35)
                             && Au.AdiDataAula < DateTime.Now.AddDays(15)
                          //   && Au.AdiDisciplina.Equals(DDDisciplina.SelectedValue)
                             select new { Au.AdiDataAula }).Distinct().Take(30);
                DD_Data.DataSource = query;
                DD_Data.DataBind();
                DD_Data.Items.Insert(0, "Selecione..");
                DD_Data.SelectedIndex = 0;
            }
        }


        protected void DDTurmaLancamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            //{
            //    var query = (from D in db.CA_Disciplinas
            //                 join A in db.CA_AulasDisciplinasAprendizs on D.DisCodigo equals A.AdiDisciplina
            //                 where A.AdiTurma.Equals(DDTurmaLancamento.SelectedValue)
            //                 select new { D.DisCodigo, D.DisDescricao }).Distinct().ToList().OrderBy(item => item.DisDescricao);



            //    DDDisciplinaLancamento.DataSource = query;
            //    DDDisciplinaLancamento.DataBind();
            //    DDDisciplinaLancamento.Items.Insert(0, "Selecione..");
            //    DDDisciplinaLancamento.SelectedIndex = 0;
            //}


            Panel2.Visible = false;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from Au in db.CA_AulasDisciplinasAprendizs
                             where Au.AdiTurma.Equals(DDTurmaLancamento.SelectedValue)
                             && Au.AdiDataAula > DateTime.Now.AddDays(-35)
                             && Au.AdiDataAula < DateTime.Now.AddDays(15)
                           //  && Au.AdiDisciplina.Equals(DDDisciplinaLancamento.SelectedValue)
                             select new { Au.AdiDataAula }).Distinct().Take(30);


                DDDataLancamento.DataSource = query;
                DDDataLancamento.DataBind();
                DDDataLancamento.Items.Insert(0, "Selecione..");
                DDDataLancamento.SelectedIndex = 0;
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
                var query = (from Au in db.CA_AulasDisciplinasAprendizs
                             join A in db.CA_Aprendizs on Au.AdiCodAprendiz equals A.Apr_Codigo
                             join T in db.CA_Turmas on Au.AdiTurma equals T.TurCodigo
                             join D in db.CA_Disciplinas on Au.AdiDisciplina equals D.DisCodigo
                             join E in db.CA_Educadores on Au.AdiEducador equals E.EducCodigo
                             where Au.AdiTurma.Equals(DD_TURMA.SelectedValue)
                             && Au.AdiDataAula.Equals(Convert.ToDateTime(DD_Data.SelectedValue))
                             && A.Apr_Situacao.Equals("6")
                        //     && D.DisCodigo.Equals(DDDisciplina.SelectedValue)
                             orderby A.Apr_Nome
                             select new { CodAprendiz = A.Apr_Codigo, Nome = A.Apr_Nome, CodTurma = T.TurCodigo, Turma = T.TurNome, Data = Au.AdiDataAula, Presenca = Au.AdiPresenca, Atraso = Au.AdiAtraso }).Distinct().OrderBy(t => t.Nome);
                gvAlunos.DataSource = query.ToList();
                gvAlunos.DataBind();
            }
        }

        protected void btnLancarConteudo_Click(object sender, EventArgs e)
        {
            multi.ActiveViewIndex = 1;
            preencheDropDown();
        }

        protected void DDDataLancamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabelaLancamento.Visible = true;
            var sql = "select * from  CA_AulasDisciplinasTurmaProf where ADPTurma = " + DDTurmaLancamento.SelectedValue +  " and ADPDataAula = '" + Convert.ToDateTime(DDDataLancamento.SelectedValue) + "'";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while(result.Read()){
                txtConteudoLecionado.Text = result["ADPConteudoLecionado"].ToString();
                txtRecursosUsados.Text = result["ADPRecursosUsados"].ToString();
                txtObservacoes.Text = result["ADPObservacoes"].ToString();

            }
        }

        protected void btnSalvarConteudo_Click(object sender, EventArgs e)
        {
            var sql = "update CA_AulasDisciplinasTurmaProf set ADPConteudoLecionado = '" + txtConteudoLecionado.Text + "', ADPRecursosUsados = '" + txtRecursosUsados.Text + "', ADPObservacoes = '" + txtObservacoes.Text + "', ADPUsuario  = '" + Session["codigo"] + "' , ADPDataAlteracao = '"+DateTime.Now+"' where ADPTurma = " + DDTurmaLancamento.SelectedValue + " and ADPDataAula = '" + Convert.ToDateTime(DDDataLancamento.SelectedValue) + "'";
            var con = new Conexao();
            con.Alterar(sql);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
          "alert('Dados Atualizados com sucesso.');", true);
        }

       
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            multi.ActiveViewIndex = 0;
        }
    }
}