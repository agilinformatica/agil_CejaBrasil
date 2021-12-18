using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.IO;

namespace ProtocoloAgil.pages
{
    public partial class ListaCursantes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";


          var manager = new ScriptManager();
          manager.RegisterAsyncPostBackControl(btn_print);


            if(!IsPostBack)
            {
                BindCursos();
                MultiView1.ActiveViewIndex = 0;
            }
        }

        protected void BindCursos()
        {
            using (var reposytory = new Repository<Curso>(new Context<Curso>()))
            {
                var list = new List<Curso>();
                list.AddRange(reposytory.All().OrderBy(p => p.CurDescricao));
                DDcursoDiario.DataSource = list.ToList();
                DDcursoDiario.DataBind();
            }
        }

        protected void BindTurmas(string curso,DropDownList dropdown)
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

        protected void BindDisciplinas()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = from i in bd.View_CA_DisciplinasProfessores
                            where i.TurCodigo.Equals(DDturma_pesquisa.SelectedValue) 
                            select new Disciplina{DisCodigo = i.DPDisciplina,DisDescricao = i.DisDescricao};
                var list = new List<Disciplina>();
                list.AddRange(dados.Distinct());
                DDdisciplina_pesquisa.Items.Clear();
                DDdisciplina_pesquisa.DataSource = list;
                DDdisciplina_pesquisa.DataBind();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

     
        protected void DDcursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList) sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            BindTurmas(drop.SelectedValue, DDturma_pesquisa);
            DDdisciplina_pesquisa.Items.Clear();
            dd_data_inicio.Items.Clear();
        }

        protected void DDturmaDiario_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            BindDisciplinas();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        private void BindGridView()
        {
              try
              {
                    if (DDcursoDiario.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um curso");
                    if (DDturma_pesquisa.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma turma");
                    if (!DDdisciplina_pesquisa.SelectedValue.Equals(string.Empty) 
                        && dd_data_inicio.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma data de início para a disciplina.");

                  const string sql01 = "SELECT DISTINCT Apr_Codigo, Apr_Nome,Apr_Sexo,ParNomeFantasia, ParUniDescricao,AreaDescricao, StaAbreviatura,Apr_Telefone, Apr_Email FROM CA_Aprendiz " +
                      "INNER JOIN CA_AlocacaoAprendiz on CA_Aprendiz.Apr_Codigo = CA_AlocacaoAprendiz.ALAAprendiz INNER JOIN CA_ParceirosUnidade on dbo.CA_ParceirosUnidade.ParUniCodigo = dbo.CA_AlocacaoAprendiz.ALAUnidadeParceiro " +
                      "INNER JOIN CA_Parceiros ON dbo.CA_Parceiros.ParCodigo = dbo.CA_ParceirosUnidade.ParUniCodigoParceiro INNER JOIN CA_DisciplinasAprendiz ON CA_Aprendiz.Apr_Codigo = CA_DisciplinasAprendiz.DiaCodAprendiz " +
                      "INNER JOIN CA_SituacaoAprendiz ON  CA_Aprendiz.Apr_Situacao = CA_SituacaoAprendiz.StaCodigo INNER JOIN dbo.CA_DisciplinasTurmaProf ON CA_DisciplinasAprendiz.DiaDisciplinaprof = CA_DisciplinasTurmaProf.DpOrdem " +
                      "INNER JOIN dbo.CA_Turmas on DPTurma = TurCodigo INNER JOIN dbo.CA_AreaAtuacao on  Apr_AreaAtuacao = AreaCodigo WHERE dbo.CA_DisciplinasTurmaProf.DPDisciplina = 60 AND CA_SituacaoAprendiz.StaCodigo = 6 order by  Apr_Nome";

                  const string sql02 = "SELECT  Apr_Codigo, Apr_Nome,Apr_Sexo,ParNomeFantasia, ParUniDescricao,AreaDescricao, StaAbreviatura,Apr_Telefone, Apr_Email " +
                      "FROM CA_Aprendiz INNER JOIN CA_AlocacaoAprendiz on CA_Aprendiz.Apr_Codigo = CA_AlocacaoAprendiz.ALAAprendiz INNER JOIN CA_ParceirosUnidade on dbo.CA_ParceirosUnidade.ParUniCodigo = dbo.CA_AlocacaoAprendiz.ALAUnidadeParceiro " +
                      "INNER JOIN CA_Parceiros ON dbo.CA_Parceiros.ParCodigo = dbo.CA_ParceirosUnidade.ParUniCodigoParceiro INNER JOIN  CA_SituacaoAprendiz ON  CA_Aprendiz.Apr_Situacao = CA_SituacaoAprendiz.StaCodigo " +
                      "INNER JOIN dbo.CA_Turmas on ALATurma = TurCodigo  INNER JOIN dbo.CA_AreaAtuacao on  Apr_AreaAtuacao = AreaCodigo " +
                      "WHERE  CA_AlocacaoAprendiz.ALATurma = @turma AND  CA_Aprendiz.Apr_Situacao = 6 And  CA_AlocacaoAprendiz.ALAStatus = 'A' order by  Apr_Nome";

                  const string sql03 = "SELECT Apr_Codigo, Apr_Nome,Apr_Sexo,ParNomeFantasia, ParUniDescricao,AreaDescricao, StaAbreviatura,Apr_Telefone, Apr_Email FROM CA_Aprendiz " +
                      "INNER JOIN CA_AlocacaoAprendiz on CA_Aprendiz.Apr_Codigo = CA_AlocacaoAprendiz.ALAAprendiz INNER JOIN CA_ParceirosUnidade on dbo.CA_ParceirosUnidade.ParUniCodigo = dbo.CA_AlocacaoAprendiz.ALAUnidadeParceiro " +
                      "INNER JOIN CA_Parceiros ON dbo.CA_Parceiros.ParCodigo = dbo.CA_ParceirosUnidade.ParUniCodigoParceiro INNER JOIN CA_DisciplinasAprendiz on  CA_Aprendiz.Apr_Codigo = CA_DisciplinasAprendiz.DiaCodAprendiz INNER JOIN " +
                      "CA_SituacaoAprendiz ON  CA_Aprendiz.Apr_Situacao = CA_SituacaoAprendiz.StaCodigo INNER JOIN CA_DisciplinasTurmaProf ON   CA_DisciplinasAprendiz.DiaDisciplinaProf = CA_DisciplinasTurmaProf.DpOrdem " +
                      "INNER JOIN dbo.CA_Turmas on DPTurma = TurCodigo INNER JOIN dbo.CA_AreaAtuacao on  Apr_AreaAtuacao = AreaCodigo WHERE DPDisciplina = @disciplina AND DPTurma = ALATurma  AND  DPDataInicio = @data AND TurCodigo = @turma AND CA_SituacaoAprendiz.StaCodigo = 6 order by  Apr_Nome";

                    SDS_Alunos.SelectParameters.Clear();
                    if (!dd_data_inicio.SelectedValue.Equals(string.Empty))
                    {
                        SDS_Alunos.SelectCommand = sql03;  
                        SDS_Alunos.SelectParameters.Add(new ControlParameter("data", "dd_data_inicio", "SelectedValue"));
                        SDS_Alunos.SelectParameters.Add(new ControlParameter("disciplina", "DDdisciplina_pesquisa", "SelectedValue"));
                        SDS_Alunos.SelectParameters.Add(new ControlParameter("turma", "DDturma_pesquisa", "SelectedValue"));
                    }

                    else if (!DDdisciplina_pesquisa.SelectedValue.Equals(string.Empty) && dd_data_inicio.SelectedValue.Equals(string.Empty))
                    {
                        SDS_Alunos.SelectCommand = sql01;
                        SDS_Alunos.SelectParameters.Add(new ControlParameter("disciplina", "DDdisciplina_pesquisa", "SelectedValue"));
                    }
                    else 
                    {
                        SDS_Alunos.SelectCommand = sql02;
                        SDS_Alunos.SelectParameters.Add(new ControlParameter("turma", "DDturma_pesquisa", "SelectedValue"));
                    }

                    GridView1.DataSource = SDS_Alunos;
                    GridView1.DataBind();
              }
              catch (ArgumentException ex)
              {
                  ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
              }
              catch (Exception ex)
              {
                  Funcoes.TrataExcessao("000121", ex);
              }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindGridView();
            MultiView1.ActiveViewIndex = 0;
            Pn_aprendiz.Visible = false;
        }

        protected void btn_gerar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
        
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void DDciclo_pesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            BindDisciplinas();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = GridView1.SelectedRow;
            HFdataSelecionada.Value = WebUtility.HtmlDecode(row.Cells[0].Text);

            Session["comando"] = "Alterar";
            MultiView1.ActiveViewIndex = 2;
        }

     
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            var br = (ImageButton)sender;
            Session["PRMT_Turma"] = DDturma_pesquisa.SelectedValue;
            Session["PRMT_Aprendiz"] = br.CommandArgument;
            
            Session["PRMT_disciplina"] = DDdisciplina_pesquisa.SelectedValue;
            OpcoesDocumentos(2);
           if( !DDdisciplina_pesquisa.SelectedValue.Equals(string.Empty) &&  !dd_data_inicio.SelectedValue.Equals(string.Empty))
           {
               using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
               {
                   var ordem = bd.CA_DisciplinasTurmaProfs.Where( p => p.DPDisciplina.Equals(int.Parse(DDdisciplina_pesquisa.SelectedValue)) &&
                                p.DPDataInicio == DateTime.Parse(dd_data_inicio.SelectedValue)  && p.DPTurma == int.Parse(DDturma_pesquisa.SelectedValue));
                   Session["PRMT_Ordem"] = ordem.First().DpOrdem;
               }
           }
            MultiView1.ActiveViewIndex = 1;
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            try
            {
                if (DDtipo_relatorio.SelectedValue.Equals("")) throw new ArgumentException("Selecione um tipo de relatório.");
                var tipo = DDtipo_relatorio.SelectedValue;
                switch (tipo)
                {
                    case "1": Session["id"] = "53"; break;
                    case "2": Session["id"] = "21"; break;
                    case "3": Session["id"] = "18"; break;
                    case "4": Session["id"] = "23"; break;
                    case "5": Session["id"] = "13"; break;
                    case "6": Session["id"] = "14"; break;
                    case "7": Session["id"] = "42";
                        if (TBdataInicio.Text.Equals(string.Empty)) throw new ArgumentException("Informe a Data da Aula.");
                        Session["data_lista"] = TBdataInicio.Text;
                        break;
                    case "8": ExibeContrato(1); break;
                    case "9": ExibeContrato(2); break;
                    case "10": Session["id"] = "54"; break;
                    case "11": ExibeContrato(3); break;
                    case "12":
                        if (tb_numero.Text.Equals(string.Empty)) throw new ArgumentException("Informe o número de uniformes cedidos ao aprendiz.");
                        Session["prmt_num_uniforme"] = tb_numero.Text;
                        ExibeContrato(4); break;
                    case "13": ExibeContrato(5); break;
                    case "14": ExibeContrato(6); break;
                    case "15": Session["id"] = "66"; break;
                }

                if ((tipo != "8") && (tipo != "9") && (tipo != "11") && (tipo != "12") && (tipo != "13") && (tipo != "14"))
                {
                    IFrame1.Visible = true;
                    IFrame1.DataBind();
                }
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000122", ex);
            }
          
        }

  
        protected void OpcoesDocumentos(int tipo)
        {
            var list = new List<ListItem>{ new ListItem("Selecione", string.Empty) };
            if(tipo == 1)
            {
                list.Add(new ListItem("Lista de Alunos", "1"));
                list.Add(new ListItem("Lista de Alunos Sintética", "15"));
                if (!DDdisciplina_pesquisa.SelectedValue.Equals(""))
                {
                    list.Add(new ListItem("Diário", "2"));
                    list.Add(new ListItem("Resultado Final", "5"));
                    list.Add(new ListItem("Parecer Técnico", "6"));
                    list.Add(new ListItem("Lista de Presença", "7"));
                }
            }
            else
            {
                list.Add(new ListItem("Ficha Cadastral", "3"));
                list.Add(new ListItem("Ficha Funcional", "4"));
                list.Add(new ListItem("Contrato Fundação", "8"));
                list.Add(new ListItem("Contrato Empresa", "9"));
                list.Add(new ListItem("Declaração de Matrícula", "10"));
                list.Add(new ListItem("Termo de Compromisso", "11"));
                list.Add(new ListItem("Recebimento de Uniformes", "12"));
                list.Add(new ListItem("Aquisição Vale Transporte", "13"));
                list.Add(new ListItem("Declaração Vale Transporte", "14"));
            }
            DDtipo_relatorio.Items.Clear();
            DDtipo_relatorio.SelectedValue = "";
            DDtipo_relatorio.Items.AddRange(list.ToArray());
            IFrame1.Visible = false;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void DDtipo_relatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            IFrame1.Visible = false;
            IFrame1.DataBind();
            LB_data_aula.Visible = DDtipo_relatorio.SelectedValue.Equals("7");
            TBdataInicio.Visible = DDtipo_relatorio.SelectedValue.Equals("7");

            lb_numero.Visible = DDtipo_relatorio.SelectedValue.Equals("12");
            tb_numero.Visible = DDtipo_relatorio.SelectedValue.Equals("12");
            tb_numero.Text = string.Empty;
            TBdataInicio.Text = string.Empty;
        }

        protected void DDdisciplina_pesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataInicio();
        }

        private void BindDataInicio()
        {
            if(DDdisciplina_pesquisa.SelectedValue.Equals(string.Empty)) return;

            using (var repository = new Repository<DisciplinasProf>(new Context<DisciplinasProf>()))
            {
                var list = repository.All().Where(p => p.DPDisciplina == int.Parse(DDdisciplina_pesquisa.SelectedValue) && p.DPTurma == int.Parse(DDturma_pesquisa.Text)).Select
                        (p => p.DPDataInicio).Select(p => new { DiaDataInicio = string.Format("{0:dd/MM/yyyy}",p)}).Distinct();
                dd_data_inicio.DataSource = list.ToList();
                dd_data_inicio.DataBind();
            }
        }

        protected void ExibeContrato( int tipo)
        {
            var popup = "popup('Contrato.aspx?" +
                      "id=" + Criptografia.Encrypt(tipo.ToString(), GetConfig.Key()) +
                      "&meta=" + Criptografia.Encrypt(Session["PRMT_Aprendiz"].ToString(), GetConfig.Key()) + "', '700','800');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), popup, true);
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.Rows.Count == 0) throw new ArgumentException("Realize uma pesquisa para acessar esta área.");
                var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
                Session["PRMT_Turma"] = DDturma_pesquisa.SelectedValue;
                Session["PRMT_data"] = dd_data_inicio.SelectedValue;
                Session["PRMT_disciplina"] = DDdisciplina_pesquisa.SelectedValue;

                if (!DDdisciplina_pesquisa.SelectedValue.Equals(string.Empty) && !dd_data_inicio.SelectedValue.Equals(string.Empty))
                {
                    Session["PRMT_professor"] = bd.CA_DisciplinasTurmaProfs.Where(p => p.DPDisciplina == int.Parse(DDdisciplina_pesquisa.SelectedValue) &&
                                 p.DPTurma == int.Parse(DDturma_pesquisa.SelectedValue) && p.DPDataInicio == DateTime.Parse(dd_data_inicio.SelectedValue)).First().DPProf;
                    Session["PRMT_Ordem"] = bd.CA_DisciplinasTurmaProfs.Where(p => p.DPDisciplina == int.Parse(DDdisciplina_pesquisa.SelectedValue) &&
                                p.DPTurma == int.Parse(DDturma_pesquisa.SelectedValue) && p.DPDataInicio == DateTime.Parse(dd_data_inicio.SelectedValue)).First().DpOrdem;
                }

                OpcoesDocumentos(1);


                LB_data_aula.Visible = DDtipo_relatorio.SelectedValue.Equals("7");
                TBdataInicio.Visible = DDtipo_relatorio.SelectedValue.Equals("7");
                TBdataInicio.Text = string.Empty;

                MultiView1.ActiveViewIndex = 1;
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('" + ex.Message + "')", true);
            }
        }

        protected void BCarometro_Click(object sender, EventArgs e)
        {
            preencheDropDownTurma();          
            MultiView1.ActiveViewIndex = 2;
        }


        void preencheDropDownTurma()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = (from Au in db.CA_AulasDisciplinasAprendizs
                             join A in db.CA_Aprendizs on Au.AdiCodAprendiz equals A.Apr_Codigo
                             join T in db.CA_Turmas on Au.AdiTurma equals T.TurCodigo
                             join D in db.CA_Disciplinas on Au.AdiDisciplina equals D.DisCodigo
                             join E in db.CA_Educadores on Au.AdiEducador equals E.EducCodigo
                             orderby T.TurNome ascending
                             select new { T.TurCodigo, T.TurNome }).Distinct().OrderBy(t => t.TurNome);

                DD_TURMA.DataSource = query;
                DD_TURMA.DataBind();
                DD_TURMA.Items.Insert(0, "Selecione..");
                DD_TURMA.SelectedIndex = 0;
            }
        }

        protected void BGerarCarometro_Click(object sender, EventArgs e)
        {           
            Session["id"] = 80; //Relatorio Lista de Presença  
            Session["TurCodigo"] = DD_TURMA.SelectedValue;
            PCarometro.Visible = true;
        }



       

        protected void btnDetalhesAprendiz_Click(object sender, ImageClickEventArgs e)
        {
            var br = (ImageButton)sender;
            int codAprendiz = int.Parse(br.CommandArgument.ToString());

            Session["matricula"] = codAprendiz;

            Session["PRMT_Aprendiz"] = br.CommandArgument;
            Session["enable_Save"] = Session["tipo"].Equals("Educador") ? "Educador" : "User";

            using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
            {
                var aprendiz = repository.Find(codAprendiz);
                lb_codigo_aprendiz.Text = aprendiz.Apr_Codigo.ToString();
                lb_nome_aprendiz.Text = aprendiz.Apr_Nome;
            }
            Pn_aprendiz.Visible = true;


            MultiView1.ActiveViewIndex = 3;
            Session["Comando"] = "Alterar";
        }
    }
}