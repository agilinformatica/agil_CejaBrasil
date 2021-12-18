using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.IO;

namespace ProtocoloAgil.pages
{
    public partial class EstatisticaRealizadas : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "estatisticas";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBdataInicio.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de Início.");
                if (TBdataTermino.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de Término.");
                Bindgridview();
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

        private void Bindgridview()
        {


            var sql = "select UsuTipo from CA_Usuarios where UsuCodigo = '" + Session["CodInterno"].ToString() + "'";
            var conexao = new Conexao();
            var result = conexao.Consultar(sql);
            var tipo = "";
            while (result.Read())
            {
                tipo = result[0].ToString();
            }

           


            string sqlgeral = "SELECT  * FROM View_Pesquisas_Realizadas " +
                    "WHERE  PepDataRealizada BETWEEN '" + TBdataInicio.Text + "' AND '" + TBdataTermino.Text + "' AND PepRealizada = 'S'  ";

            if (!DDturmas.SelectedValue.Equals(string.Empty))
            {
                sqlgeral += " AND PepTurma = '" + DDturmas.SelectedValue + "'";
            }

            if (tipo.Equals("S"))
            {
                  sqlgeral += " AND PepTutor = '" + Session["CodInterno"].ToString() + "'";
            }

            sqlgeral += " order by ParNomeFantasia, Apr_Nome";

            var datasource = new SqlDataSource { ID = "SDS_Pesquisas", SelectCommand = sqlgeral, ConnectionString = GetConfig.Config() };
            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Bindgridview();
        }

        protected void IMB_visualizar_Click(object sender, ImageClickEventArgs e)
        {


            var btn = (ImageButton)sender;
            GridViewRow row = btn.NamingContainer as GridViewRow;

            var bt = (ImageButton)sender;
            var codigo = bt.CommandArgument;

            var nome = GridView1.DataKeys[row.RowIndex]["Apr_Nome"].ToString();
            var unidade = GridView1.DataKeys[row.RowIndex]["ParUniDescricao"].ToString();

            LB_Aprendiz.Text = nome;
            LB_empresa.Text = unidade;

            using (var repository = new Repository<PesquisaParceiro>(new Context<PesquisaParceiro>()))
            {
                var pesquisa = repository.Find(int.Parse(codigo));
                BindRepeater(pesquisa);
                MultiView1.ActiveViewIndex = 1;
                btn_print.Visible = false;
            }
        }


        private void CarregaFoto(int cod)
        {
            if (cod == null)
            {
                IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                return;
            }

            var matricula = cod;
            var filePath = Server.MapPath(@"/files/fotos");
            var dir = new DirectoryInfo(filePath);
            if (dir.Exists)
            {
                var files = dir.GetFiles().ToList();
                //var foto = files.Where(p => p .Name.Contains(matricula)).ToList();
                //var foto = files.Where(p => p.Name.Substring(0, 4).Equals(matricula)).ToList();
                var foto = files.Where(p => p.Name.Equals(matricula + ".jpg")).ToList();
                if (foto.Count() > 0)
                {
                    var path = "../files/fotos/" + foto.First().Name;
                    IMG_foto_aprendiz.Attributes.Add("src", path);
                }
                else
                {
                    IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                }
            }
        }

        private void BindRepeater(PesquisaParceiro dados)
        {

            CarregaFoto(dados.PepAprendiz);

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                //preenchimento cabeçalho, de acordo com a alocação vigente do apendiz.
                //var aprendiz =
                //    bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == dados.PepAprendiz && p.ALAStatus == "A").First();

                //var aprendiz =
                //    bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == dados.PepAprendiz).OrderByDescending(p => p.ALADataInicio).First();

                //LB_Aprendiz.Text = aprendiz.Apr_Nome;
                //LB_empresa.Text = aprendiz.ParNomeFantasia;
                //LB_Orientador.Text = aprendiz.OriNome;
                //LB_Horario.Text = string.Format("{0:HH:mm}", aprendiz.ALAInicioExpediente) + " às " +
                //                  string.Format("{0:HH:mm}", aprendiz.ALATerminoExpediente);
                LB_Data.Text = string.Format("{0:dd/MM/yyyy}",dados.PepDataRealizada);
                LB_Responsavel.Text = dados.PepTutor;
                // bd.CA_Usuarios.Where(p => p.UsuCodigo == dados.PepTutor).First().UsuNome;

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
                    Pn_Pesquisa.Controls.Add(new Label
                    {
                        ID = "LB_resposta_" + questao.ResOpcaoEscolhida,
                        Text = "<strong>Resposta: </strong>" +
                            (questao.QueTipoQustao == "M" ? questao.OpcTexto : questao.ResRespostaTexto),
                        CssClass = "fonteTexto"
                    });
                    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/>"));

                    if (codigoPesquisa != 3)
                    {
                        Pn_Pesquisa.Controls.Add(new Label
                        {
                            ID = "LB_valor_" + questao.ResOpcaoEscolhida,
                            Text = "<strong>Valor: </strong>" +
                                (questao.QueTipoQustao == "M" ? questao.OpcNota.ToString() : "Não aplicável."),
                            CssClass = "fonteTexto"
                        });
                    }

                    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/><br/>"));
                }

                foreach (var questao in respostas.OrderBy(p => p.QueOrdemExibicao).Distinct())
                {
                    Pn_Pesquisa.Controls.Add(new Label
                    {
                        ID = "LB_valor_" + questao.ResOpcaoEscolhida,
                        Text = "<strong>Observação: </strong>" +
                            (questao.PepObservacao),
                        CssClass = "fonteTexto"
                    });

                    break;
                }

                string conceito;
                float media = 0;

            //    var nota = codigoPesquisa == 2 ? respostas.Sum(p => p.OpcNota) :
            //        respostas.Where(p => !string.IsNullOrEmpty(p.ResRespostaTexto) && Funcoes.IsNumber(p.ResRespostaTexto)).Sum(p => int.Parse(p.ResRespostaTexto));

            //    if (codigoPesquisa == 2)
            //    {
            //        conceito = nota <= 10 ? "Insatisfatório" : (nota >= 11 && nota <= 16) ? "Precisa Melhorar" : (nota >= 17 && nota <= 25)
            //                                   ? "Atingiu as Expectativas" : "Excedeu as Expectativas";
            //    }
            //    else
            //    {
            //        media = (float)nota ;
            //        conceito = media <= 20 ? "Insatisfatório" : (media > 20 && media <= 40) ? "Precisa Melhorar" :
            //            (media > 40 && media <= 60) ? "Regular" : (media > 60 && media <= 80) ? "Atingiu as Expectativas" : "Excedeu as Expectativas";
            //    }


            //    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<table class='Table'><tr>"));
            //    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<th>Nota Final</th>"));
            //    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<th>Conceito</th>"));
            //    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<th>Procedimento</th></tr><tr><td>"));
            //    Pn_Pesquisa.Controls.Add(new Label { ID = "LB_nota_final", Text = (codigoPesquisa == 2 ? nota.ToString() : string.Format("{0:F2}", media)), CssClass = "fonteTexto" });
            //    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("</td><td>"));
            //    Pn_Pesquisa.Controls.Add(new Label { ID = "LB_conceito_final", Text = conceito, ForeColor = nota >= 17 ? Color.Green : Color.Red, CssClass = "fonteTexto" });
            //    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("</td><td>"));
            //    switch (conceito)
            //    {
            //        case "Insatisfatório":
            //            Pn_Pesquisa.Controls.Add(new Label
            //            {
            //                ID = "LB_feedback",
            //                CssClass = "fonteTexto",
            //                Text = "Deverá ser agendada uma visita do Monitor à empresa.<br/> " +
            //                       "O monitor e estagiário de pscologia devem orientar o aprediz pessoalmente."
            //            });
            //            break;
            //        case "Precisa Melhorar":
            //        case "Regular":
            //            Pn_Pesquisa.Controls.Add(new Label
            //            {
            //                ID = "LB_feedback",
            //                CssClass = "fonteTexto",
            //                Text = "O monitor e estagiário de pscologia devem orientar o aprediz pessoalmente. "
            //            });
            //            break;
            //        case "Atingiu as Expectativas":
            //        case "Excedeu as Expectativas":
            //            Pn_Pesquisa.Controls.Add(new Label { ID = "LB_feedback", Text = "Enviar Feedback:" });
            //            var imb = new ImageButton { ID = "IMB_feedback", ImageUrl = "../images/icon_edit.png" };
            //            imb.Click += IMB_feedback_Click;
            //            Pn_Pesquisa.Controls.Add(imb);
            //            break;
            //    }

            //    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("</td></tr></table>"));
            //    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/><br/>"));
            //    ViewState["Pn_Pesquisa"] = dados;
            }
        }

        protected void IMB_feedback_Click(object sender, ImageClickEventArgs e)
        {
        }


        protected void btn_listar_Click(object sender, EventArgs e)
        {

            btn_print.Visible = true;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_contador_Click(object sender, EventArgs e)
        {
            BindGridQTD();
            btn_print.Visible = true;
            MultiView1.ActiveViewIndex = 2;
        }

        private void BindGridQTD()
        {
            string sqlgeral = "Select count(PepTutor) as QTD,UsuNome,PesNome  from CA_Pesquisa_Parceiro "+
                               "inner join CA_Pesquisa on PesCodigo =  PepPesquisaCodigo left join CA_Usuarios on UsuCodigo = PepTutor  " +
                              "WHERE  PepDataRealizada BETWEEN '" + TBdataInicio.Text + "' AND '" + TBdataTermino.Text + "' AND PepRealizada = 'S' group by UsuNome,PesNome";

            var datasource = new SqlDataSource { ID = "SDS_Pesquisas", SelectCommand = sqlgeral, ConnectionString = GetConfig.Config() };
            GridView2.DataSource = datasource;
            GridView2.DataBind();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            BindGridQTD();
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBdataInicio.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de Início.");
                if (TBdataTermino.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de Término.");
                Session["prmt_data_inicio"] = TBdataInicio.Text;
                Session["prmt_data_fim"] = TBdataTermino.Text;
                Session["id"] =  MultiView1.ActiveViewIndex == 0 ?  59 : 60;
                MultiView1.ActiveViewIndex = 3;
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Imprimir"))
            {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[index];
                    var cod = row.Cells[0].Text;
                    Imprimir(cod);
            }
        }

        public void Imprimir(string cod)
        {
            Session["pepCodigo"] = cod;
            string codAprendiz = "";

            var sql = "Select Apr_Codigo from View_Pesquisas_Realizadas where PepCodigo = " + cod + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                codAprendiz = result["Apr_Codigo"].ToString();
            }

             sql = "Select * from CA_AlocacaoAprendiz where ALAAprendiz = "+codAprendiz+"";
             con = new Conexao();
            var result2 = con.Consultar(sql);
            int i = 0;
            while(result2.Read()){
                i++;
            }

            if (i > 0)
            {
                Session["id"] = "94";
            }
            else
            {
                Session["id"] = "83";
            }

            
            MultiView1.ActiveViewIndex = 3;

        }
        // Autor: Thassio Augusto 
        // Responsável por rodar cada linha do meu do grid
        // Data: 04/05/2015
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblConceito = (e.Row.FindControl("lblConceito") as Label);
                var minhaFonte = DataBinder.Eval(e.Row.DataItem, "Notafinal");
                if (Convert.ToInt32(minhaFonte) <= 20)
                {
                    lblConceito.Text = "INSUFICIENTE";
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;

                }
                else if (Convert.ToInt32(minhaFonte) >= 21 && Convert.ToInt32(minhaFonte) <= 30)
                {
                    lblConceito.Text = "REGULAR";
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Yellow;
                }
                else if (Convert.ToInt32(minhaFonte) >= 31 && Convert.ToInt32(minhaFonte) <= 35)
                {
                    lblConceito.Text = "BOM";
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
                }
                else if (Convert.ToInt32(minhaFonte) >= 35 && Convert.ToInt32(minhaFonte) <= 40)
                {
                    lblConceito.Text = "OTIMO";
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Blue;
                }
                else
                {
                    lblConceito.Text = "EXCELENTE";
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Navy;
                }
            }
        }
    }
}
