using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class ControlePesquisa : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "estatisticas";
            if (!IsPostBack)
            {
              //  BindPesquisas();
                BindCursos();
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 2;
                if (Session["option"] != null)
                {
                    if (Session["option"].ToString().Equals("1"))
                    {
                        var pesquisa = Criptografia.Decrypt(Session["code_delivered"].ToString(), GetConfig.Key());
                        Session["prmt_pesquisa"] = pesquisa;
                        MultiView1.ActiveViewIndex = 1;
                        MultiView2.ActiveViewIndex = 1;
                        BindQuestoes();
                        LimpaCampos();
                        Session["comando"] = "Incluir";
                        btn_next_final.Visible = false;
                    }
                }
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_nova_pesquisa.Enabled = false;
                btn_atribuir.Enabled = false;
                btn_next.Enabled = false;
                btn_next_final.Enabled = false;
                btn_Cadastro_opcao.Enabled = false;
                GridView2.Columns[3].Visible = false;

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

        protected void BindPesquisas()
        {
            const string sqlquery = "SELECT a.PesCodigo, a.PesNome, a.PesDescricao, (select COUNT(CA_Questao.QuePesquisa) from CA_Questao WHERE " +
                           "QuePesquisa =a.PesCodigo ) AS NumeroQuestoes FROM  CA_Pesquisa a";
            var datasource = new SqlDataSource() { ID = "SDS_pesquisas", ConnectionString = GetConfig.Config(), SelectCommand = sqlquery };
            datasource.Selected += SqlDataSource1_Selected;
            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_nome_pesquisa.Text.Equals(string.Empty)) throw new ArgumentException("Informe o nome da pesquisa.");
                using (var repository = new Repository<Pesquisa>(new Context<Pesquisa>()))
                {
                    var pesquisa = new Pesquisa
                    {
                        PesNome = tb_nome_pesquisa.Text,
                        PesDescricao = tb_descricao_pesquisa.Text.Equals(string.Empty) ? null : tb_descricao_pesquisa.Text
                    };
                    repository.Add(pesquisa);
                    Session["prmt_pesquisa"] = repository.All().OrderBy(p => p.PesCodigo).Last().PesCodigo;
                }
                MultiView1.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 1;
                LimpaCampos();
                Session["comando"] = "Incluir";
                btn_next_final.Visible = false;
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000020", ex);
            }
        }

        protected void btn_Cadastro_opcao_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_numero.Text.Equals(string.Empty)) throw new ArgumentException("Informe o número da questão.");
                if (dd_tipo_questao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe o tipo de questão.");
                if (tb_questao.Text.Equals(string.Empty)) throw new ArgumentException("Digite a questão.");
                using (var repository = new Repository<Questao>(new Context<Questao>()))
                {
                    if (!Session["comando"].Equals("Alterar"))
                    {
                        var cadastrado = repository.All().Where(p => p.QueOrdemExibicao == int.Parse(tb_numero.Text)
                            && p.QuePesquisa == int.Parse(Session["prmt_pesquisa"].ToString()));
                        if (cadastrado.Count() > 0) throw new ArgumentException("Uma questão com este número já foi cadastrada.");
                    }

                    var questao = (Session["comando"].Equals("Alterar"))
                                    ? repository.Find(int.Parse(Session["Alteracodigo"].ToString()), int.Parse(Session["prmt_pesquisa"].ToString()))
                                    : new Questao();

                    questao.QueOrdemExibicao = int.Parse(tb_numero.Text);
                    questao.QuePesquisa = int.Parse(Session["prmt_pesquisa"].ToString());
                    questao.QueTexto = tb_questao.Text;
                    questao.QueTipoQustao = dd_tipo_questao.SelectedValue;

                    if (!Session["comando"].Equals("Alterar")) repository.Add(questao);
                    else repository.Edit(questao);
                }
                BindQuestoes();
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000020", ex);
            }
        }

        private void AbreCadastro(int questao)
        {
            var url = Regex.Split(Request.Url.ToString(), ".aspx");
            var name = Regex.Split(url.First(), "pages/").Last();
            var tipo = "popup('CadastroOpcao.aspx?" +
                      "id=" + Criptografia.Encrypt("1", GetConfig.Key()) +
                      "&meta=" + Criptografia.Encrypt(questao.ToString(), GetConfig.Key()) +
                      "&target=" + Criptografia.Encrypt(name + ".aspx", GetConfig.Key()) + "', '700',450);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), tipo, true);
        }

        protected void btn_nova_pesquisa_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = 0;
        }

        protected void btn_consulta_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            BindPesquisas();
        }

        protected void btn_next_final_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            BindPesquisas();
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            using (var repository = new Repository<Questao>(new Context<Questao>()))
            {
                var bt = (ImageButton)sender;
                var codigoQuestao = bt.CommandArgument;

                if (bool.Parse(HFConfirma.Value))
                    repository.Remove(int.Parse(codigoQuestao), int.Parse(Session["prmt_pesquisa"].ToString()));
                BindQuestoes();
            }
        }

        protected void IMBopcao_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            var codigoQuestao = bt.CommandArgument;
            AbreCadastro(int.Parse(codigoQuestao));
        }

        protected void BindQuestoes()
        {
            const string sqlquery = "SELECT QueCodigo, QueTexto, QueTipoQustao, QueOrdemExibicao from CA_Questao where  QuePesquisa = @pesquisa order by QueOrdemExibicao";
            var datasource = new SqlDataSource { ID = "SDS_pesquisas", ConnectionString = GetConfig.Config(), SelectCommand = sqlquery };
            datasource.Selected += SqlDataSource1_Selected;
            datasource.SelectParameters.Add(new SessionParameter { Name = "pesquisa", SessionField = "prmt_pesquisa" });
            GridView2.DataSource = datasource;
            GridView2.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView)sender).SelectedRow;
            Session["prmt_pesquisa"] = row.Cells[0].Text;
            MultiView1.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = 1;
            BindQuestoes();
            LimpaCampos();
            Session["comando"] = "Incluir";
            btn_next_final.Visible = false;
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView)sender).SelectedRow;
            using (var repository = new Repository<Questao>(new Context<Questao>()))
            {
                var questao = repository.All().Where(p => p.QueOrdemExibicao == int.Parse(row.Cells[0].Text) &&
                                 p.QuePesquisa == int.Parse(Session["prmt_pesquisa"].ToString())).First();

                tb_numero.Text = questao.QueOrdemExibicao.ToString();
                dd_tipo_questao.SelectedValue = questao.QueTipoQustao;
                tb_questao.Text = questao.QueTexto;
                Session["comando"] = "Alterar";
                Session["Alteracodigo"] = questao.QueCodigo;
                btn_next_final.Visible = true;
            }
        }

        protected void btn_next_final_Click1(object sender, EventArgs e)
        {
            LimpaCampos();
            Session["comando"] = "Incluir";
        }

        private void LimpaCampos()
        {
            tb_numero.Text = string.Empty;
            dd_tipo_questao.SelectedValue = string.Empty;
            tb_questao.Text = string.Empty;
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            BindQuestoes();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindPesquisas();
        }

        protected void btn_atribuir_Click(object sender, EventArgs e)
        {
            BindCursos();
            MultiView1.ActiveViewIndex = 2;
        }

        protected void DDcursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            BindTurmas(drop.SelectedValue, DDturmaDiario);
        }

        protected void DDturmaDiario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void BindCursos()
        {
            using (var reposytory = new Repository<Curso>(new Context<Curso>()))
            {
                var list = new List<Curso>();
                list.AddRange(reposytory.All().OrderBy(p => p.CurDescricao));
                DDturmaDiario.Items.Clear();
                DDcursoDiario.DataSource = list.ToList();
                DDcursoDiario.DataBind();
            }
        }

        protected void BindTurmas(string curso, DropDownList dropdown)
        {
            using (var reposytory = new Repository<Turma>(new Context<Turma>()))
            {
                var list = new List<Turma>();
                list.AddRange(reposytory.All().Where(x => x.TurCurso.Equals(curso)).OrderBy(p => p.TurNome));
                dropdown.Items.Clear();
                dropdown.DataSource = list.ToList();
                dropdown.DataBind();
            }
        }

        protected void btn_next0_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDcursoDiario.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um curso.");
                if (DDturmaDiario.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma turma.");
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var list = new List<Aprendiz>();
                    var dados = from i in bd.CA_AlocacaoAprendizs join p in bd.CA_Aprendizs on i.ALAAprendiz equals p.Apr_Codigo
                                where i.ALATurma == int.Parse(DDturmaDiario.SelectedValue) && i.ALAStatus == "A"  
                                select new Aprendiz(p) ;
                    list.AddRange(dados);
                    foreach (var aprendiz in list)
                    {
                        using (var aprendizes = new Repository<Aprendiz>(new Context<Aprendiz>()))
                        {
                            aprendiz.Apr_HabilitaPesquisa = "S";
                            aprendizes.Edit(aprendiz);
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('Pesquisa habilitada com sucesso para esta turma.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000020", ex);
            }
        }
    }
}