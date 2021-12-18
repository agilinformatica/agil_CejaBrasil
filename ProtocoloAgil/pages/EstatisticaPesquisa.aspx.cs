using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.Data;

namespace ProtocoloAgil.pages
{
    public partial class EstatisticaPesquisa : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "estatisticas";
            if (!IsPostBack)
            {
                divPesquisa.Visible = false;
                Session["prmt_opt"] = 0;
                BindUsuarios();
                MultiView1.ActiveViewIndex = 0;
            }
        }


        private void Bindgridview()
        {
            var type = Session["prmt_opt"].ToString();

            divPesquisa.Visible = false;
            if (type.Equals("3"))
            {
                GridGerarNovas.Visible = true;
                GridView1.Visible = false;
            }
            else
            {
                GridGerarNovas.Visible = false;
                GridView1.Visible = true;
            }


            var sqlgeral =
                "SELECT  PepCodigo, Apr_Codigo,Apr_Nome,PesNome, ParCodigo,ParNomeFantasia, ParUniDescricao, ParUniCodigoParceiro, PepRealizada, PepAno,PepMes = CASE WHEN PepMes = 1 THEN 'Janeiro' WHEN PepMes = 2 THEN 'Fevereiro' " +
                "WHEN PepMes = 3 THEN 'Março' WHEN PepMes = 4 THEN 'Abril' WHEN PepMes = 5 THEN 'Maio' WHEN PepMes = 6 THEN 'Junho' WHEN PepMes = 7 THEN 'Julho' WHEN PepMes = 8 THEN 'Agosto' WHEN PepMes = 9 THEN 'Setembro' WHEN PepMes = 10 THEN 'Outubro' " +
                "WHEN PepMes = 11 THEN 'Novembro' WHEN PepMes = 12 THEN 'Dezembro' END,(CASE WHEN PepRealizada = 'N' THEN 'Pendente' ELSE 'Realizada' END) AS Situacao, OriNome, OriTelefone, OriEmail,  UsuCodigo,Usunome " +
                "FROM  dbo.CA_Pesquisa_Parceiro inner join dbo.CA_ParceirosUnidade ON ParUniCodigo = PepParceiroCodigo " +
                "inner join CA_Parceiros ON ParCodigo = ParUniCodigoParceiro inner join CA_Aprendiz ON Apr_Codigo = PepAprendiz inner join CA_Pesquisa on PepPesquisaCodigo = PesCodigo " +
                " left join CA_Orientador ON PepOrientador = OriCodigo left join CA_Usuarios on UsuCodigo = ParRespFundacao " +
                " WHERE  PepAno = '" + TB_ano_ref.Text + "' AND  PepMes = " + DDmeses.SelectedValue + " ";


            if(!dd_responsavel.SelectedValue.Equals(string.Empty))
                sqlgeral += " AND ParRespFundacao= '" +  dd_responsavel.SelectedValue +"'";

            switch (type)
            {
                case "1": sqlgeral += " AND PepRealizada = 'N'"; break;
                case "2": sqlgeral += " AND PepRealizada = 'S'"; break;
            }

            sqlgeral += " order by ParNomeFantasia, Apr_Nome";
            var datasource = new SqlDataSource {ID = "SDS_Pesquisas", SelectCommand = sqlgeral, ConnectionString = GetConfig.Config()};
            GridView1.DataSource = datasource;
            GridView1.Columns[7].Visible = (type == "2");
            GridView1.DataBind();
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                divPesquisa.Visible = false;
                if (DDmeses.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um mês de referência.");
                if (TB_ano_ref.Text.Equals(string.Empty)) throw new ArgumentException("Selecione um ano de referência.");

                if (Session["prmt_opt"].ToString().Equals("3"))
                {
                    GridView1.Visible = false;
                    GridGerarNovas.Visible = true;
                    CarregaGridGerarNovas();
                }
                else
                {
                    GridView1.Visible = true;
                    GridGerarNovas.Visible = false;
                    Bindgridview();
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

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }


        protected void BindUsuarios()
        {
            using( var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.CA_Usuarios.ToList().OrderBy(p => p.UsuNome).ToList();
                dd_responsavel.DataSource = dados;
                dd_responsavel.DataBind();
            }
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            Session["prmt_opt"] = 0;
            Bindgridview();
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            Session["prmt_opt"] = 2;
            Bindgridview();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["prmt_opt"] = 1;
            Bindgridview();
        }


        protected void btnGerarNovas_Click(object sender, EventArgs e)
        {
            Session["prmt_opt"] = 3;

            // teste
            if (DDmeses.SelectedValue.Equals(string.Empty)) {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Selecione um mês de referência.');", true);
                return;
            }

            if (TB_ano_ref.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                            "alert('Selecione um ano de referência.');", true);
                return;
            }
            GridGerarNovas.Visible = true;
            GridView1.Visible = false;
            btnGerar.Visible = true;

            //MultiView1.ActiveViewIndex = 0;
            CarregaGridGerarNovas();
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDmeses.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um mês de referência.");
                if (TB_ano_ref.Text.Equals(string.Empty)) throw new ArgumentException("Selecione um ano de referência.");
                Session["prmt_ano"] = TB_ano_ref.Text;
                Session["prmt_mes"] = DDmeses.SelectedValue;
                Session["prmt_user"] = dd_responsavel.SelectedValue;
                Session["id"] = 55;
                MultiView1.ActiveViewIndex = 1;
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


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Bindgridview();
        }

        protected void btn_Vontar_Click(object sender, EventArgs e)
        {
            Bindgridview();
            MultiView1.ActiveViewIndex = 0;
        }


        protected void IMB_visualizar_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton) sender;
            var codigo = bt.CommandArgument;
            using (var repository = new Repository<PesquisaParceiro>(new Context<PesquisaParceiro>()))
            {
                var pesquisa = repository.Find(int.Parse(codigo));
                BindRepeater(pesquisa);
                MultiView1.ActiveViewIndex = 2;
            }
        }


        private void BindRepeater(PesquisaParceiro dados)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                //preenchimento cabeçalho, de acordo com a alocação vigente do apendiz.
                var aprendiz = bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == dados.PepAprendiz && p.ALAStatus == "A").First();

                LB_Aprendiz.Text = aprendiz.Apr_Nome;
                LB_empresa.Text = aprendiz.ParNomeFantasia;
                //LB_Orientador.Text = aprendiz.OriNome;
                //LB_Horario.Text = string.Format("{0:HH:mm}", aprendiz.ALAInicioExpediente) + " às " +
                //                  string.Format("{0:HH:mm}", aprendiz.ALATerminoExpediente);
                LB_Data.Text = string.Format("{0:dd/MM/yyyy}", dados.PepDataRealizada);
                LB_Responsavel.Text = bd.CA_Usuarios.Where(p => p.UsuCodigo == dados.PepTutor).First().UsuNome;

                /*  O sistema carrega a pesquisa de acordo com o código e gera dinamicamente os controles de acordo com o tipo de questão
                *  os tipos são: M - Multipla escolha, A - Aberta ou T - Multipla escolha com justificativa.
                *  cada controle criado é adicionado ao painel Pn_Pesquisa.
                *  Literais são utilizados para quebra de linha e marcação html.
               */

                var respostas = bd.View_Resposta_Pesquisas.Where(p => p.PepCodigo == dados.PepCodigo).ToList();
                var pesquisa = bd.View_Pequisas.Where(p => p.QuePesquisa == dados.PepPesquisaCodigo);
                Lb_Nome_pesquisa.Text = pesquisa.First().PesNome;
                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/>"));
                var codigoPesquisa = respostas.First().PepPesquisaCodigo;

                foreach (var questao in respostas.OrderBy(p => p.QueOrdemExibicao).Distinct())
                {
                    Pn_Pesquisa.Controls.Add(new Label { ID = "LB_questao_" + questao.QueCodigo, Text = questao.QueTexto, CssClass = "fonteTab" });
                    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/>"));
                    Pn_Pesquisa.Controls.Add(new Label { ID = "LB_resposta_" + questao.ResOpcaoEscolhida, Text = "<strong>Resposta: </strong>" +
                        (questao.QueTipoQustao== "M"? questao.OpcTexto : questao.ResRespostaTexto), CssClass = "fonteTexto" });
                    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/>"));

                    if (codigoPesquisa != 3)
                    {
                       Pn_Pesquisa.Controls.Add(new Label { ID = "LB_valor_" + questao.ResOpcaoEscolhida, Text = "<strong>Valor: </strong>" + 
                         (questao.QueTipoQustao== "M"?  questao.OpcNota.ToString() : "Não aplicável."), CssClass = "fonteTexto" });
                     }
                    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/><br/>"));
                }

                string conceito;
                float media = 0;

                var nota = codigoPesquisa == 2 ? respostas.Sum(p => p.OpcNota) :
                    respostas.Where(p => !string.IsNullOrEmpty(p.ResRespostaTexto) && Funcoes.IsNumber(p.ResRespostaTexto)).Sum(p => int.Parse(p.ResRespostaTexto));

                if (codigoPesquisa == 2)
                {
                    conceito = nota <= 10 ? "Insatisfatório" : (nota >= 11 && nota <= 16) ? "Precisa Melhorar" : (nota >= 17 && nota <= 25)
                                               ? "Atingiu as Expectativas" : "Excedeu as Expectativas";
                }
                else
                {
                     media = (float)nota;
                    conceito = media <= 20 ? "Insatisfatório" : (media > 20 && media <= 40) ? "Precisa Melhorar" :
                        (media > 40 && media <= 60) ? "Regular" : (media > 60 && media <= 80) ? "Atingiu as Expectativas" : "Excedeu as Expectativas";
                }
                 

                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<table class='Table'><tr>"));
                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<th>Nota Final</th>"));
                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<th>Conceito</th>"));
                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<th>Procedimento</th></tr><tr><td>"));
                Pn_Pesquisa.Controls.Add(new Label { ID = "LB_nota_final", Text = (codigoPesquisa == 2 ? nota.ToString() : string.Format("{0:F0}", media)), CssClass = "fonteTexto" });
                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("</td><td>"));
                Pn_Pesquisa.Controls.Add(new Label { ID = "LB_conceito_final", Text = conceito, ForeColor =   nota >= 17 ? Color.Green :  Color.Red, CssClass = "fonteTexto" });
                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("</td><td>"));
                switch (conceito)
                {
                    case "Insatisfatório":
                        Pn_Pesquisa.Controls.Add(new Label { ID = "LB_feedback",   CssClass = "fonteTexto",
                                  Text = "Deverá ser agendada uma visita do Monitor à empresa.<br/> " +
                                         "O monitor e estagiário de pscologia devem orientar o aprediz pessoalmente." });
                        break;
                    case "Precisa Melhorar": case "Regular":
                        Pn_Pesquisa.Controls.Add(new Label { ID = "LB_feedback", CssClass = "fonteTexto", 
                                  Text = "O monitor e estagiário de pscologia devem orientar o aprediz pessoalmente. "});
                        break;
                    case "Atingiu as Expectativas": case "Excedeu as Expectativas":
                        Pn_Pesquisa.Controls.Add(new Label {ID = "LB_feedback", Text = "Enviar Feedback:"});
                        var imb = new ImageButton {ID = "IMB_feedback", ImageUrl = "../images/icon_edit.png"};
                        imb.Click += IMB_feedback_Click;
                        Pn_Pesquisa.Controls.Add(imb);
                        break;
                }

                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("</td></tr></table>"));
                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/><br/>"));
                ViewState["Pn_Pesquisa"] = dados;
            }
        }


        protected void IMB_feedback_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        

        private void CarregaGridGerarNovas()
        {
            string mes = DDmeses.SelectedValue;
            string ano = TB_ano_ref.Text;

            DateTime dataMenor = new DateTime(int.Parse(ano), int.Parse(mes), 01);
            DateTime dataMaior = new DateTime(int.Parse(ano), int.Parse(mes), DateTime.DaysInMonth(int.Parse(ano), int.Parse(mes)));

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from J in db.View_CA_JovensPesquisas
                              where J.ALADataInicio > dataMenor && J.ALADataInicio < dataMaior
                             select new {Nome = J.Apr_Nome, Data = J.ALADataInicio, Codigo = J.Apr_Codigo, UnidadeParceiro = J.ALAUnidadeParceiro, EducadorResponsavel = J.TurEducadorResponsavel, Turma = J.ALATurma  }).ToList().OrderBy(item => item.Nome);

                GridGerarNovas.DataSource = query.ToList();
                GridGerarNovas.DataBind();

            }
        }

        protected void GridGerarNovas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridGerarNovas.PageIndex = e.NewPageIndex;
            CarregaGridGerarNovas();
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            GridGerarNovas.HorizontalAlign = HorizontalAlign.Left;
            divPesquisa.Visible = true;
            CarregarDropPesquisa();
        }

        protected void CarregarDropPesquisa()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from S in db.CA_Pesquisas
                             where S.PesCodigo > 1
                             select new { S.PesCodigo, S.PesDescricao }).ToList().OrderBy(item => item.PesDescricao);
                DDPesquisa.DataSource = query;
                DDPesquisa.DataBind();
            }
        }

        protected void btnGerarDados_Click(object sender, EventArgs e)
        {
            if (DDPesquisa.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                 "alert('Selecione uma pesquisa.');", true);
                return;
            }


            var sql = "Select PesPublicoAlvo from CA_Pesquisa where PesCodigo = "+DDPesquisa.SelectedValue+"";
            var con = new Conexao();
            var result = con.Consultar(sql);
            string pesPublicoAlvo = "";

            string pepTutor = "";
            int pepOrientador = 0;

            while(result.Read())
            {
                pesPublicoAlvo = result["PesPublicoAlvo"].ToString();
            }

            string mes = DDmeses.SelectedValue;
            string ano = TB_ano_ref.Text;

            DateTime dataMenor = new DateTime(int.Parse(ano), int.Parse(mes), 01);
            DateTime dataMaior = new DateTime(int.Parse(ano), int.Parse(mes), DateTime.DaysInMonth(int.Parse(ano), int.Parse(mes)));

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from J in db.View_CA_JovensPesquisas
                             where J.ALADataInicio > dataMenor && J.ALADataInicio < dataMaior
                             select new { Nome = J.Apr_Nome, Data = J.ALADataInicio, Codigo = J.Apr_Codigo, UnidadeParceiro = J.ALAUnidadeParceiro, EducadorResponsavel = J.TurEducadorResponsavel, Turma = J.ALATurma }).ToList().OrderBy(item => item.Nome);

                                
                foreach (var lista in query.ToList())
                {
                    if (pesPublicoAlvo.Equals("E"))
                    {
                        pepTutor = "1";
                        pepOrientador = 1;
                    }
                    else
                    {
                        pepOrientador = int.Parse(lista.EducadorResponsavel.ToString());
                        pepTutor = lista.EducadorResponsavel.ToString();

                    }

                    using (var repository = new Repository<PesquisaParceiro>(new Context<PesquisaParceiro>()))
                    {
                        var pesquisaParceiro = new PesquisaParceiro
                        {
                            PepPesquisaCodigo = int.Parse(DDPesquisa.SelectedValue),
                            PepParceiroCodigo = int.Parse(lista.UnidadeParceiro.ToString()),
                            PepAno = DateTime.Now.Year.ToString(),
                            PepMes = int.Parse(DateTime.Now.Month.ToString()),
                            PepRealizada = "N",
                            PepAprendiz = int.Parse(lista.Codigo.ToString()),
                            PepTurma = int.Parse(lista.Turma.ToString()),
                            PepTutor = pepTutor,
                            PepOrientador = pepOrientador

                        };
                        repository.Add(pesquisaParceiro);
                    }
                }

            }

            GridGerarNovas.HorizontalAlign = HorizontalAlign.Center;
            divPesquisa.Visible = false;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
            "alert('Gerado com sucesso.');", true);
        }
    }
}