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


    public partial class CronogramaTurmaDisciplina : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (!IsPostBack)
            {
                CarregarDropTurma();
                CarregarDropDisciplina();
                CarregarDropProfessor();
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }


        }

        protected void CarregarDropTurma()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from T in db.CA_Turmas
                             // where situacoes.Contains(S.StaCodigo)
                             select new { T.TurCodigo, T.TurNome }).ToList().OrderBy(item => item.TurNome);
                DDTurma.DataSource = query;
                DDTurma.DataBind();
            }
        }

        protected void CarregarDropDisciplina()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from D in db.CA_Disciplinas
                             // where situacoes.Contains(S.StaCodigo)
                             select new { D.DisCodigo, D.DisDescricao }).ToList().OrderBy(item => item.DisDescricao);
                DDDisciplina.DataSource = query;
                DDDisciplina.DataBind();
            }
        }

        protected void CarregarDropProfessor()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from E in db.CA_Educadores
                             // where situacoes.Contains(S.StaCodigo)
                             select new { E.EducCodigo, E.EducNome }).ToList().OrderBy(item => item.EducNome);
                DDProfessores.DataSource = query;
                DDProfessores.DataBind();
            }
        }

        void ValidaCampos()
        {
            if (txtDataInicio.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Data de Início é obrigatório.')", true);
                return;
            }
            if (txtQuantidade.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Quantidade é obrigatório.')", true);
                return;
            }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            //ValidaCampos();

            if (txtDataInicio.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Data de Início é obrigatório.')", true);
                return;
            }
            if (txtQuantidade.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Quantidade é obrigatório.')", true);
                return;
            }

            DateTime data = Convert.ToDateTime(txtDataInicio.Text.ToString());
            int diaSemana = RetornaDiaSemanaDaTurma(int.Parse(DDTurma.SelectedValue));
            bool diaCerto = false;
            switch (diaSemana.ToString())
            {
                case "1":
                    if (data.DayOfWeek.Equals(DayOfWeek.Sunday))
                    {
                        diaCerto = true;
                    }
                    break;
                case "2":
                    if (data.DayOfWeek.Equals(DayOfWeek.Monday))
                    {
                        diaCerto = true;
                    }
                    break;
                case "3":
                    if (data.DayOfWeek.Equals(DayOfWeek.Tuesday))
                    {
                        diaCerto = true;
                    }
                    break;
                case "4":
                    if (data.DayOfWeek.Equals(DayOfWeek.Wednesday))
                    {
                        diaCerto = true;
                    }
                    break;
                case "5":
                    if (data.DayOfWeek.Equals(DayOfWeek.Thursday))
                    {
                        diaCerto = true;
                    }
                    break;
                case "6":
                    if (data.DayOfWeek.Equals(DayOfWeek.Friday))
                    {
                        diaCerto = true;
                    }
                    break;
                case "7":
                    if (data.DayOfWeek.Equals(DayOfWeek.Saturday))
                    {
                        diaCerto = true;
                    }
                    break;
            }

            if (diaCerto == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Data escolhida não pertence ao dia da semana da Turma selecionada.')", true);
                return;
            }

            for (int a = 0; a < int.Parse(txtQuantidade.Text); a++)
            {
                if (VerificaFeriado(data.Date).Equals(true))
                {
                    data = data.AddDays(7);
                    a--;
                }
                else
                {

                    var sql = "insert into CA_AulasDisciplinasTurmaProf (ADPTurma, ADPprofessor, ADPDisciplina, ADPDataAula, ADPOrdemAula)  values (" + int.Parse(DDTurma.SelectedValue) + ", " + int.Parse(DDProfessores.SelectedValue) + ", " + int.Parse(DDDisciplina.SelectedValue) + ", '" + data + "', " + int.Parse(DDSequencia.SelectedValue) + ")";
                    var con = new Conexao();
                    con.Alterar(sql);
                    data = data.AddDays(7);
                }
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cronograma da Turma/Disciplina gerado com sucesso.')", true);
            return;
        }

        int RetornaDiaSemanaDaTurma(int codTurma)
        {
            var sql = "select turDiaSemana from CA_Turmas where TurCodigo = " + codTurma + "";
            var con = new Conexao();
            var result = con.Consultar(sql);
            int diaSemana = 0;

            while (result.Read())
            {
                diaSemana = int.Parse(result["turDiaSemana"].ToString());
            }
            return diaSemana;
        }

        /// <summary>
        /// retorna se o dia é feriado de acordo com os dados inseridos na tabela CA_Feriados
        /// </summary>
        /// <param name="feriado"></param>
        /// <returns></returns>
        protected bool VerificaFeriado(DateTime feriado)
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var datasource = from F in db.CA_Feriados
                                 where F.FerData.Date.Equals(feriado)
                                 select new { F.FerData };
                if (datasource.Any())
                    return true;
                else
                    return false;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

           
            BindGridView();
        }

       

        private void BindGridView()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var cronograma = bd.CA_AulasDisciplinasTurmaProfs;
                var selected = from u in bd.CA_AulasDisciplinasTurmaProfs
                               join T in bd.CA_Turmas on u.ADPTurma equals T.TurCodigo
                               join E in bd.CA_Educadores on u.ADPprofessor equals E.EducCodigo
                               join D in bd.CA_Disciplinas on u.ADPDisciplina equals D.DisCodigo
                               where u.ADPDataAula >= Convert.ToDateTime(txtDataInicio.Text)
                               && u.ADPTurma.ToString().Equals(DDTurma.SelectedValue)
                               orderby u.ADPDataAula, u.ADPOrdemAula
                               select new {u.ADPDataAula, T.TurObservacoes, E.EducNome, D.DisDescricao, u.ADPOrdemAula };
                GridView1.DataSource = selected.ToList();
                GridView1.DataBind();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtDataInicio.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Data de Início é obrigatório.')", true);
                return;
            }

            BindGridView();
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Session["DataInicio"] = Convert.ToDateTime(txtDataInicio.Text);
            Session["TurmaEscolhida"] = DDTurma.SelectedValue;
            Session["id"] = 90;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void btnVoltar2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
     
    }
}