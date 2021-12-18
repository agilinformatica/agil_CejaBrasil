using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class PlanoCurricular : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            if (!IsPostBack)
            {
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
            }
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            GridView1.DataBind();
            DD_curso.SelectedValue = TB_codigoCurso.Text;
        }

        protected void btn_novo_Click(object sender, EventArgs e)
        {
            Session["comando"] = "Inserir";
            LimpaDados();
            MultiView1.ActiveViewIndex = 1;
            dd_plano_curricular.Enabled = true;
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void DD_curso_SelectedIndexChanged(object sender, EventArgs e)
        {
            TB_codigoCurso.Text = DD_curso.SelectedValue;
            BindPlanos(DD_plano, TB_codigoCurso.Text);
        }

        private void BindPlanos( DropDownList dropDown, string curso)
        {
            if(DD_curso.SelectedValue.Equals(string.Empty)) return;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.CA_Planos.Where(p => p.PlanCurso == curso).OrderBy(x => x.PlanDescricao);
                dropDown.Items.Clear();
                dropDown.DataSource = dados.ToList();
                dropDown.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = GridView1.SelectedRow;
            var disciplina = Regex.Split(row.Cells[2].Text, " - ")[0];
            Session["PRMT_disciplina"] = disciplina;
            PreencheCampos();
        }

        private void PreencheCampos()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = from i in bd.CA_PlanoCurriculars
                            join m in bd.CA_Planos on i.PlcCodigoPlano equals m.PlanCodigo
                            where m.PlanCodigo.Equals(WebUtility.HtmlDecode(DD_plano.SelectedValue))
                                  && m.PlanCurso.Equals(WebUtility.HtmlDecode(DD_curso.SelectedValue))
                                  && i.PlcDisciplina.Equals(Convert.ToInt32(Session["PRMT_disciplina"]))
                            select new {i, m};
                if (dados.Count() == 0) return;
                var plano = dados.First();
                DD_tipo_avaliacao.SelectedValue = plano.i.PlcTipoAvaliacao ?? "";
                TB_aula_semanal.Text = plano.i.PlcNumeroAulas.ToString();
                TB_carga_horaria.Text = plano.i.PlcCargaHoraria.ToString();
                DD_curso.DataBind();
                dd_plano_curricular.Enabled = false;
                DD_disciplina.DataBind();
                BindPlanos(dd_plano_curricular, plano.m.PlanCurso);
                dd_plano_curricular.SelectedValue = plano.i.PlcCodigoPlano.ToString();
                TB_orderm_disciplina.Text = plano.i.PlcOrdemDisciplina.ToString();
                DD_disciplina.SelectedValue = plano.i.PlcDisciplina.ToString();
                DD_gera_cronograma.SelectedValue = plano.i.PlcGeraCronograma;             
                DD_Educador.SelectedValue = plano.i.EducCodigo.ToString();

                //DD_Educador.SelectedValue = plano.i.EducCodigo.ToString();
                Session["Alteracodigo"] = plano.i.PlcCodigoPlano;
                Session["AlteraDisciplina"] = Convert.ToInt32(Session["PRMT_disciplina"]);
                Session["AlteraCurso"] = plano.m.PlanCurso;
                Session["comando"] = "Alterar";
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }


        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (dd_plano_curricular.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe o código do plano curricular.");
                if (DD_disciplina.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Escolha uma disciplina.");
                if (DD_Educador.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Escolha um Educador.");
                if (TB_aula_semanal.Text.Equals(string.Empty)) throw new ArgumentException("Informe a quantidade de aulas semanais.");
                if (TB_carga_horaria.Text.Equals(string.Empty)) throw new ArgumentException("Informe a carga horária.");
                if (DD_tipo_avaliacao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um tipo de avaliação.");
                if (TB_orderm_disciplina.Text.Equals(string.Empty)) throw new ArgumentException("Informe a ordem que a disiciplina será ministrada .");
                if (DD_gera_cronograma.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Defina se o cronograma da disciplina deve ser gerado automaticamente.");

                const string sqlinsert = " INSERT INTO CA_PlanoCurricular(PlcCodigoPlano, PlcDisciplina,PlcCargaHoraria,PlcTipoAvaliacao,PlcNumeroAulas, PlcGeraCronograma,PlcOrdemDisciplina,EducCodigo) " +
                    "VALUES(@PlcCodigoPlano,@PlcDisciplina,@PlcCargaHoraria,@PlcTipoAvaliacao,@PlcNumeroAulas,@PlcGeraCronograma,@PlcOrdemDisciplina,@EducCodigo) ";

                const string sqlupdate = " UPDATE  CA_PlanoCurricular SET PlcCodigoPlano = @PlcCodigoPlano, PlcDisciplina = @PlcDisciplina,  PlcCargaHoraria = @PlcCargaHoraria, " +
                "PlcTipoAvaliacao = @PlcTipoAvaliacao, PlcNumeroAulas = @PlcNumeroAulas, PlcGeraCronograma = @PlcGeraCronograma, PlcOrdemDisciplina = @PlcOrdemDisciplina, EducCodigo = @EducCodigo WHERE PlcCodigoPlano = @Codigo AND PlcDisciplina = @Disciplina ";

                var paramaters = new List<SqlParameter>{
                DD_disciplina.SelectedValue.Equals(string.Empty)? new SqlParameter("PlcDisciplina", DBNull.Value) : new SqlParameter("PlcDisciplina", DD_disciplina.SelectedValue),
                DD_Educador.SelectedValue.Equals(string.Empty)? new SqlParameter("EducCodigo", DBNull.Value) : new SqlParameter("EducCodigo", DD_Educador.SelectedValue),
                DD_tipo_avaliacao.SelectedValue.Equals(string.Empty) ? new SqlParameter("PlcDisciplina", DBNull.Value) : new SqlParameter("PlcTipoAvaliacao", DD_tipo_avaliacao.SelectedValue),
                TB_aula_semanal.Text.Equals(string.Empty) ? new SqlParameter("PlcNumeroAulas", DBNull.Value) : new SqlParameter("PlcNumeroAulas", TB_aula_semanal.Text),
                TB_carga_horaria.Text.Equals(string.Empty) ? new SqlParameter("PlcCargaHoraria", DBNull.Value) : new SqlParameter("PlcCargaHoraria", TB_carga_horaria.Text),
                new SqlParameter("PlcGeraCronograma", DD_gera_cronograma.SelectedValue ),
                new SqlParameter("PlcOrdemDisciplina", TB_orderm_disciplina.Text),
                new SqlParameter("PlcCodigoPlano", dd_plano_curricular.SelectedValue) };

                if (Session["comando"].Equals("Alterar"))
                {
                    paramaters.Add(new SqlParameter("Codigo", Session["Alteracodigo"]));
                    paramaters.Add(new SqlParameter("Disciplina", Session["AlteraDisciplina"]));
                }

                var con = new Conexao();
                con.Alterar(Session["comando"].Equals("Alterar") ? sqlupdate : sqlinsert, paramaters.ToArray());
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                              "alert('Ação realizada com sucesso.')", true);
            }
            catch ( ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                              "alert('" + ex.Message + "')", true);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000050", ex);
            }
        }

        private void LimpaDados()
        {
            dd_plano_curricular.DataBind();
            DD_disciplina.DataBind(); 
            TB_aula_semanal.Text = string.Empty;
            TB_carga_horaria.Text = string.Empty;
            DD_tipo_avaliacao.Text = string.Empty;
        }

        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 9;
            if (!DD_curso.SelectedValue.Equals(string.Empty))
                Session["parameter_curso"] = DD_curso.SelectedValue;
            Session["parameter_plano"] = DD_plano.SelectedValue;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var curso = button.CommandArgument.Split('_')[0];
            var disciplina = button.CommandArgument.Split('_')[1];
            var com = new Conexao();
            const string sql = "Delete From dbo.CA_PlanoCurricular WHERE  PlcCodigoPlano = @PlcCodigoPlano AND PlcDisciplina = @PlcDisciplina";
            var  parameters = new SqlParameter []{ new SqlParameter("PlcCodigoPlano", curso), new SqlParameter("PlcDisciplina", disciplina) };

                if (Convert.ToBoolean(HFConfirma.Value))
                    com.Alterar(sql,parameters.ToArray());
            GridView1.DataBind();
        }
    }
}