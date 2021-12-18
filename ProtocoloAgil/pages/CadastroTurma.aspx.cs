using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class CadastroTurma : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(btn_texto);
            if (!IsPostBack)
            {
                CarregarDropEducador();
                BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
               Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
                
            }
        }


        protected void CarregarDropEducador()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from E in db.CA_Educadores
                             select new { E.EducCodigo, E.EducNome }).ToList().OrderBy(item => item.EducNome);
                DDEducador.DataSource = query;
                DDEducador.DataBind();
            }
        }

        private void BindCursos()
        {
            using (var repository = new Repository<Curso>(new Context<Curso>()))
            {
                var datasource = new List<Curso>();
                datasource.AddRange(repository.All());
                DDcurso.DataSource = datasource;
                DDcurso.DataBind();
            }
        }

        private void BindPlanos(string plano, DropDownList drop)
        {
            using (var repository = new Repository<Planos>(new Context<Planos>()))
            {
                var datasource = new List<Planos>();
                datasource.AddRange(repository.All().Where(p => p.PlanCurso == plano));
                drop.Items.Clear();
                drop.DataSource = datasource;
                drop.DataBind();
            }
        }
        
        private void BindGridView(int tipo)
        {
            using (var repository = new Repository<Turma>(new Context<Turma>()))
            {
                var datasource = new List<Turma>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.TurNome)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.TurNome.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.TurNome)); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome da Turma.");
                if (DDcurso.SelectedValue.Equals(string.Empty)) throw new ArgumentException("selecione um curso para a turma.");
                if (DDdiaSemana.SelectedValue.Equals(string.Empty)) throw new ArgumentException("selecione dia da semana para a aula.");
                if (DDstatus.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe o status da turma.");
                if (TBMesesPrevistos.Text.Equals(string.Empty)) throw new ArgumentException("Informe o número de meses previstos para o término do curso.");
                if (DD_plano_Curricular.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um plano curricular para a turma.");
                if (DDEducador.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um Educador.");

                using (var repository = new Repository<Turma>(new Context<Turma>()))
                {
                    var turma = (Session["comando"].Equals("Inserir")) ? new Turma() : repository.Find(Convert.ToInt32(Session["AlrteraCodigo"].ToString()));
                    turma.TurCodigo = ((Session["comando"].Equals("Inserir")) ? 0 : Convert.ToInt32(TBcodigo.Text));
                    turma.TurNome = TBNome.Text;
                    turma.TurCurso = DDcurso.SelectedValue;
                    turma.TurDiaSemana = DDdiaSemana.SelectedValue;
                    turma.TurStatus = DDstatus.SelectedValue;
                    turma.TurInicio = new DateTime(1900, 1, 1, TS_inicio.Hour, TS_inicio.Minute, 0);
                    turma.Termino = new DateTime(1900, 1, 1, TS_final.Hour, TS_final.Minute, 0);
                    turma.TurObservacoes = TBobservacao.Text;
                    turma.TurUsuario = Session["codigo"].ToString();
                    turma.TurUnidade = int.Parse(DD_unidade_turma.SelectedValue);
                    turma.TurNumeroMeses = int.Parse(TBMesesPrevistos.Text);
                    turma.TurPlanoCurricular = byte.Parse(DD_plano_Curricular.SelectedValue);
                    turma.TurEducadorResponsavel = int.Parse(DDEducador.SelectedValue);

                    if (Session["comando"].Equals("Inserir")) repository.Add(turma);
                    else repository.Edit(turma);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação Realizada com Sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000020", ex);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView)sender).SelectedRow;
            Session["AlrteraCodigo"] = WebUtility.HtmlDecode(row.Cells[0].Text);
            Session["comando"] = "Alterar";
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void PreencheCampos()
        {
            LimpaCampos();
            using (var repository = new Repository<Turma>(new Context<Turma>()))
            {
                var turma = repository.Find(Convert.ToInt32(Session["AlrteraCodigo"].ToString()));
                TBcodigo.Text = turma.TurCodigo.ToString();
                TBNome.Text = turma.TurNome;
                DDcurso.SelectedValue = turma.TurCurso ?? "";
                BindPlanos(DDcurso.SelectedValue, DD_plano_Curricular);
                DD_plano_Curricular.SelectedValue = turma.TurPlanoCurricular == null ? "" : turma.TurPlanoCurricular.ToString();
                DDdiaSemana.SelectedValue = turma.TurDiaSemana ?? "";
                DDstatus.SelectedValue = turma.TurStatus ?? "";
                var inicio = turma.TurInicio == null ? DateTime.Today : (Convert.ToDateTime(turma.TurInicio));
                var final = turma.Termino == null ? DateTime.Today : (Convert.ToDateTime(turma.Termino));

                if (Convert.ToInt32(inicio.Hour) < 12 && turma.TurInicio != null)
                    TS_inicio.AmPm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;

                if (Convert.ToInt32(final.Hour) < 12 && turma.Termino != null)
                    TS_final.AmPm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;

                TS_inicio.Hour = inicio.Hour;
                TS_inicio.Minute = inicio.Minute;
                TS_final.Hour = final.Hour;
                TS_final.Minute = final.Minute;
                TBobservacao.Text = turma.TurObservacoes;
                DD_unidade_turma.DataBind();
                DD_unidade_turma.SelectedValue = turma.TurUnidade.ToString();
                TBMesesPrevistos.Text = turma.TurNumeroMeses.ToString();
                CarregarDropEducador();
                DDEducador.SelectedValue = turma.TurEducadorResponsavel.ToString();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btn_novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 1;
        }

        private void LimpaCampos()
        {
            BindCursos();
            TBcodigo.Text = string.Empty;
            TBNome.Text = string.Empty;
            DDdiaSemana.SelectedValue = string.Empty;
            DDstatus.SelectedValue = string.Empty;
            TBobservacao.Text = string.Empty;
            TBMesesPrevistos.Text = string.Empty;
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
           BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_texto_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            try
            {
                using (var repository = new Repository<Turma>(new Context<Turma>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.TurCodigo + "; " + item.TurCurso + "; " + item.TurNome + "; " + item.TurDiaSemana +
                           "; " + string.Format("{0:HH:mm}", item.TurInicio) + "; " + string.Format("{0:HH:mm}", item.Termino) +
                           "; " + item.TurUsuario + "; " + (item.TurStatus == "A" ?  "Ativo" : "Inativo") +  "; " + item.TurNumeroMeses;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Turmas.txt");
                }
            }
            catch (IOException ex)
            {
                 Funcoes.TrataExcessao("000021", ex);
            }
        }

        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 2;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var turma = Convert.ToInt32(button.CommandArgument);
            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());

            if (bd.View_AlocacoesAlunos.Where(p => p.ALATurma == turma).Count() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                          "alert('ERRO - Esta turma possui alunos alocados. Impossível excluir.')", true);
                return;
            }

            using (var repository = new Repository<Turma>(new Context<Turma>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(turma);
            }
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void DDcurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DDcurso.SelectedValue == string.Empty) return;
            BindPlanos(DDcurso.SelectedValue, DD_plano_Curricular);
        }
    }
}