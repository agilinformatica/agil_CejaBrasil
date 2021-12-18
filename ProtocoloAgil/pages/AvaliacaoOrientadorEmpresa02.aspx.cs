using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using System.Collections.Generic;
using MenorAprendizWeb.Base;
using System.IO;

namespace ProtocoloAgil.pages
{
    public partial class AvaliacaoOrientadorEmpresa02 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            Session["CurrentPage"] = "aprendiz";

            btn_salvar.Enabled = true;
            if(!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                BindGridview();
                var dados = new PesquisaParceiro();
                ViewState.Add("Pn_Pesquisa", dados);
                
            }
            else
            {
                var dados = (PesquisaParceiro)ViewState["Pn_Pesquisa"];
                if (dados.PepCodigo != 0)
                    BindRepeater(dados);
            }
        }

        private void BindGridview()
        {

            //WHERE  UsuCodigo  = '" + Session["codigo"] + "'
            var sqlgeral = "SELECT  PepCodigo, Apr_Codigo,Apr_Nome, ParCodigo,ParNomeFantasia, PesNome, ParUniDescricao, ParUniCodigoParceiro, PepRealizada, PepAno, PepMes = CASE WHEN PepMes = 1 THEN 'Janeiro' WHEN PepMes = 2 THEN 'Fevereiro' " +
                            "WHEN PepMes = 3 THEN 'Março' WHEN PepMes = 4 THEN 'Abril' WHEN PepMes = 5 THEN 'Maio' WHEN PepMes = 6 THEN 'Junho' WHEN PepMes = 7 THEN 'Julho' WHEN PepMes = 8 THEN 'Agosto' WHEN PepMes = 9 THEN 'Setembro' WHEN PepMes = 10 THEN 'Outubro' " +
                            "WHEN PepMes = 11 THEN 'Novembro' WHEN PepMes = 12 THEN 'Dezembro' END, (CASE WHEN PepRealizada = 'N' THEN 'Pendente' ELSE 'Realizada' END) AS Situacao, OriNome, OriTelefone, OriEmail, UsuCodigo,Usunome " +
                            "FROM  dbo.CA_Pesquisa_Parceiro inner join dbo.CA_ParceirosUnidade ON ParUniCodigo = PepParceiroCodigo " +
                            "inner join CA_Parceiros ON ParCodigo = ParUniCodigoParceiro inner join CA_Aprendiz ON Apr_Codigo = PepAprendiz inner join CA_Pesquisa on PepPesquisaCodigo = PesCodigo " +
                            "left join CA_Orientador ON PepOrientador = OriCodigo  left join CA_Usuarios on UsuCodigo = ParRespFundacao "+
                            "WHERE PepRealizada = 'N' " +
                            "and ParUniCodigoParceiro = " + Session["codigo"] + " and PesPublicoAlvo = 'E'";

            if (!tb_matricula.Text.Equals(string.Empty))
                sqlgeral += " AND Apr_Codigo = " + tb_matricula.Text;

            if (!tb_ano_ref.Text.Equals(string.Empty))
                sqlgeral += " AND PepAno = " + tb_ano_ref.Text;

            if (!DDmeses_pesquisa.SelectedValue.Equals(string.Empty))
                sqlgeral += " AND PepMes = " + DDmeses_pesquisa.SelectedValue;

           sqlgeral += " AND PepPesquisaCodigo >= 5";

            //Session["CodInterno"]
            //var sql = "select UsuTipo from CA_Usuarios where UsuCodigo = '" + Session["CodInterno"].ToString() + "'";
            //var conexao = new Conexao();
            //var result = conexao.Consultar(sql);
            //var tipo = "";
            //while (result.Read()) {
            //    tipo = result[0].ToString();
            //}

            //if (tipo.Equals("S"))
            //{
            //    sqlgeral += " AND PepTutor = '" + Session["CodInterno"].ToString() + "'";
            //}

         //   sqlgeral += " order by Apr_Nome";
            var datasource = new SqlDataSource { ID = "SDS_Pesquisas", ConnectionString = GetConfig.Config(), SelectCommand = sqlgeral };
            GridView1.DataSource = datasource;
            GridView1.DataBind();

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridview();
        }



        private void CarregaFoto( int cod)
        {
            if (cod == null)
            {
                IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                return;
            }

            var matricula = cod.ToString();
            var filePath = Server.MapPath(@"/files/fotos");
            var dir = new DirectoryInfo(filePath);
            if (dir.Exists)
            {
                var files = dir.GetFiles().ToList();
                //var foto = files.Where(p => p .Name.Contains(matricula)).ToList();
                var foto = files.Where(p => p.Name.Substring(0, 4).Equals(matricula)).ToList();
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

            using(var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                Pn_Pesquisa.Controls.Clear();
                //preenchimento cabeçalho, de acordo com a alocação vigente do apendiz.
                var aprendiz = bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == dados.PepAprendiz && p.ALAUnidadeParceiro ==  dados.PepParceiroCodigo).First();

                LB_Aprendiz.Text = aprendiz.Apr_Nome;
                LB_empresa.Text = aprendiz.ParNomeFantasia;
 //               LB_Orientador.Text = aprendiz.OriNome;
 //               LB_Horario.Text = string.Format("{0:HH:mm}", aprendiz.ALAInicioExpediente) + " às " + string.Format("{0:HH:mm}", aprendiz.ALATerminoExpediente);

                /*  O sistema carrega a pesquisa de acordo com o código e gera dinamicamente os controles de acordo com o tipo de questão
                 *  os tipos são: M - Multipla escolha, A - Aberta ou T - Multipla escolha com justificativa.
                 *  cada controle criado é adicionado ao painel Pn_Pesquisa.
                 *  Literais são utilizados para quebra de linha.
                 */
                var pesquisa = bd.View_Pequisas.Where(p => p.QuePesquisa == dados.PepPesquisaCodigo);
                Lb_Nome_pesquisa.Text = pesquisa.First().PesNome;
                Pn_Pesquisa.Controls.Add(new HiddenField() { ID = "HF_pesquisa", Value = dados.PepCodigo.ToString() });
                foreach (var questao in pesquisa.OrderBy(p => p.QueOrdemExibicao).Select(p => new { p.QueTexto, p.QueTipoQustao, p.QueCodigo }).Distinct())
                {
                    Pn_Pesquisa.Controls.Add( new Label{ID = "LB_questao_" + questao.QueCodigo, Text = questao.QueTexto, CssClass = "fonteTab"});
                    Pn_Pesquisa.Controls.Add(GetLiteral("<br/>"));
                    switch (questao.QueTipoQustao)
                    {
                        case "M":
                            var radio = new RadioButtonList { ID = "RB_opcao_" + questao.QueCodigo, RepeatDirection = RepeatDirection.Vertical, CssClass = "fonteTexto" };
                            foreach (var opt in pesquisa.Where(p => p.OpcQuestao == questao.QueCodigo).Select(p => new { p.OpcTexto, p.OpcCodigo, p.OpcOrdemExibicao }).OrderBy(p => p.OpcOrdemExibicao))
                            {
                                radio.Items.Add( new ListItem {Value = opt.OpcCodigo.ToString(),Text =opt.OpcTexto });
                            }
                            Pn_Pesquisa.Controls.Add(radio);
                            Pn_Pesquisa.Controls.Add(GetLiteral("<br/>"));
                            break;

                        case "A":
                            Pn_Pesquisa.Controls.Add(GetLiteral("<br/>"));
                            var textbox = new TextBox { ID = "TB_opcao_" + questao.QueCodigo , Width = 600, Height = 40 ,  TextMode = TextBoxMode.MultiLine, CssClass = "fonteTexto" };
                            textbox.Attributes.Add("onkeyup","javascript:IsMaxLength(this, 255);");
                            Pn_Pesquisa.Controls.Add(textbox);
                            Pn_Pesquisa.Controls.Add(GetLiteral("<br/><br/>"));
                            break;

                        case "N":
                            var txtbx = new TextBox { ID = "TB_opcao_" + questao.QueCodigo, Width = 60, Height = 13, MaxLength = 2, CssClass = "fonteTexto"  };
                            txtbx.Attributes.Add("onkeyup", "javascript:formataInteiro(this,event);");
                            txtbx.Attributes.Add("onblur", "javascript:if(eval(this.value) >10){alert('Nota não pode ser maior que 10.'); this.value ='';}");
                            Pn_Pesquisa.Controls.Add(txtbx);
                            Pn_Pesquisa.Controls.Add(GetLiteral("<br/><br/>"));
                            break;

                        case "T":
                            var radio01 = new RadioButtonList { ID = "RB_opcao_" + questao.QueCodigo, RepeatDirection = RepeatDirection.Vertical, CssClass = "fonteTexto" };
                            foreach (var opt in pesquisa.Where(p => p.OpcQuestao == questao.QueCodigo).Select(p => new { p.OpcTexto, p.OpcCodigo, p.OpcOrdemExibicao }).OrderBy(p => p.OpcOrdemExibicao))
                            {
                                radio01.Items.Add(new ListItem { Value = opt.OpcCodigo.ToString(), Text = opt.OpcTexto });
                            }
                            
                            Pn_Pesquisa.Controls.Add(radio01);
                            Pn_Pesquisa.Controls.Add(GetLiteral("<br/>"));
                            textbox = new TextBox { ID = "TB_opcao_" + questao.QueCodigo , Width = 600, Height = 40 ,TextMode = TextBoxMode.MultiLine};
                            textbox.Attributes.Add("onkeyup","javascript:IsMaxLength(this, 255);");
                            Pn_Pesquisa.Controls.Add(textbox);
                            Pn_Pesquisa.Controls.Add(GetLiteral("<br/><br/>"));
                            break;
                    }
                }
                ViewState["Pn_Pesquisa"] = dados;
            }
        }


        private LiteralControl GetLiteral(string text)
        {
            var literal = new LiteralControl {Text = text};
            return literal;
        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
            btn_salvar.Enabled = false;
            try
            {
                var respostas = ValidaFormulario();
                using (var repository = new Repository<RespostaPesquisa>(new Context<RespostaPesquisa>()))
                {
                    foreach (var resposta in respostas)
                    {
                        repository.Add(resposta);
                    }
                }

                //Edita a pesquisa do parceiro para finalizá-la .
                using (var repository = new Repository<PesquisaParceiro>(new Context<PesquisaParceiro>()))
                {
                    var pesquisaparceiro = repository.Find(respostas[0].ResPesquisaParceiro);
                    pesquisaparceiro.PepDataRealizada = DateTime.Today;
                    pesquisaparceiro.PepRealizada = "S";
                    pesquisaparceiro.PepTutor = "empresa";
                    pesquisaparceiro.PepObservacao = TB_observacao.Text.Equals(string.Empty) ? null : TB_observacao.Text;
                    repository.Edit(pesquisaparceiro);
                }

                LB_title.Text = "Sua avaliação foi recebida com sucesso.";
                LBInfo.Text = "Obrigado pela sua participação.";
                LB_erro.Text = "";
                MultiView1.ActiveViewIndex = 2;
                ViewState.Remove("Pn_Pesquisa");
                ViewState.Add("Pn_Pesquisa", new PesquisaParceiro());
            }
            catch (ArgumentException ex)
            {
                btn_salvar.Enabled = true;
                LB_erro.Text = "Atenção: " + ex.Message;
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000120", ex);
            }
        }


        private RespostaPesquisa[] ValidaFormulario()
        {
            var respostas = new List<RespostaPesquisa>();
            var pesq = (PesquisaParceiro)ViewState["Pn_Pesquisa"];
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                /* Carrega a pesquisa e cria uma lista dos controles presentes no painel Pn_Pesquisa.
                 * para cada questão, avalia o tipo de questão (M,A ou T) e procura o controle que contém a resposta.
                 * Caso o controle não possua nehuma resposta, é avisado ao usuário que aquela questão está pendente.
                 * Se houver, cria-se uma resposta com os dados levantados e esta resposta é adicionada à lista de respostas que será salva no banco de dados. 
                 */

                var list = Pn_Pesquisa.Controls.Cast<Control>().ToList();
                var pesquisa = bd.View_Pequisas.Where(p => p.QuePesquisa == pesq.PepPesquisaCodigo);
                var codigo = (HiddenField)list.Where(control => control is HiddenField).Where(p => p.ID == "HF_pesquisa").First();


                foreach (var questao in pesquisa.OrderBy(p => p.QueOrdemExibicao).Select(p => new { p.QueTexto, p.QueTipoQustao, p.QueCodigo }).Distinct())
                {
                    switch (questao.QueTipoQustao)
                    {
                        case "M":
                            var resposta = list.Where(control => control is RadioButtonList).Where(p => p.ID == "RB_opcao_" + questao.QueCodigo).Cast<RadioButtonList>().First();
                            if (resposta.SelectedValue.Equals(string.Empty)) throw new ArgumentException(" A questão '" + TrataResposta(questao.QueTexto) + "' não foi respondida.");
                            respostas.Add(new RespostaPesquisa
                            {
                                ResPesquisaParceiro = int.Parse(codigo.Value),
                                ResQuestao = questao.QueCodigo,
                                ResOpcaoEscolhida = int.Parse(resposta.SelectedValue)
                            });
                            break;

                        case "A":
                            var resposta01 = list.Where(control => control is TextBox).Where(p => p.ID == "TB_opcao_" + questao.QueCodigo).Cast<TextBox>().First();
                            if (resposta01.Text.Equals(string.Empty)) throw new ArgumentException(" A questão '" + TrataResposta(questao.QueTexto) + "' não foi respondida.");
                            respostas.Add(new RespostaPesquisa
                            {
                                ResPesquisaParceiro = int.Parse(codigo.Value),
                                ResQuestao = questao.QueCodigo,
                                ResOpcaoEscolhida = 0,
                                ResRespostaTexto = resposta01.Text
                            });
                            break;

                        case "N":
                            var resposta02 = list.Where(control => control is TextBox).Where(p => p.ID == "TB_opcao_" + questao.QueCodigo).Cast<TextBox>().First();
                            if (resposta02.Text.Equals(string.Empty)) throw new ArgumentException(" A nota da questão '" + TrataResposta(questao.QueTexto) + "' não foi informada.");
                            respostas.Add(new RespostaPesquisa
                            {
                                ResPesquisaParceiro = int.Parse(codigo.Value),
                                ResQuestao = questao.QueCodigo,
                                ResOpcaoEscolhida = 0,
                                ResRespostaTexto = resposta02.Text
                            });
                            break;

                        case "T":
                            resposta = list.Where(control => control is RadioButtonList).Where(p => p.ID == "RB_opcao_" + questao.QueCodigo).Cast<RadioButtonList>().First();
                            resposta01 = list.Where(control => control is TextBox).Where(p => p.ID == "TB_opcao_" + questao.QueCodigo).Cast<TextBox>().First();
                            if (resposta.SelectedValue.Equals(string.Empty)) throw new ArgumentException(" A questão '" + TrataResposta(questao.QueTexto) + "' não foi respondida.");
                            respostas.Add(new RespostaPesquisa
                            {
                                ResPesquisaParceiro = int.Parse(codigo.Value),
                                ResQuestao = questao.QueCodigo,
                                ResOpcaoEscolhida = int.Parse(resposta.SelectedValue),
                                ResRespostaTexto = resposta01.Text.Equals(string.Empty) ? null : resposta01.Text
                            });
                            break;
                    }
                }
            }

            return respostas.ToArray();
        }

        private string TrataResposta(string texto)
        {
            texto = texto.Replace("<strong>", "");
            texto = texto.Replace("</strong>", "");
            return texto;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton) sender;
            var codigo = bt.CommandArgument;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.CA_Pesquisa_Parceiros.Where(p => p.PepCodigo == int.Parse(codigo)).Select(pesq => new PesquisaParceiro(pesq)).First();
                BindRepeater(dados);
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindGridview();
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_pesquisa03_Click(object sender, EventArgs e)
        {
            BindGridview();
        }

        protected void btnGerarLink_Click(object sender, ImageClickEventArgs e)
        {
             var bt = (ImageButton) sender;
            var codigo = bt.CommandArgument;

            var sql = "Select A.Apr_nome As Nome from CA_Pesquisa_Parceiro PP join CA_aprendiz A on PP.PepAprendiz = A.Apr_codigo where PP.pepCodigo = " + codigo + "";
            var con = new Conexao();
            var result = con.Consultar(sql);
            string nomeAprendiz = "";

            while (result.Read()) {
                nomeAprendiz = result["Nome"].ToString();
            }


        //    codigo = Criptografia.Encrypt(codigo, GetConfig.Key());

            txtMensagem.Text = "Segue abaixo o link para a avaliação do aprendiz " + nomeAprendiz + ".  http://cefort.agilsist.com.br/AvaliacaoOrientadorExterna.aspx?pepCodigo=" + codigo + "";

            Session["TextoEmail"] = "Segue abaixo o link para a avaliação do aprendiz " + nomeAprendiz + ".  http://cefort.agilsist.com.br/AvaliacaoOrientadorExterna.aspx?pepCodigo=" + bt.CommandArgument + "";

            MultiView1.ActiveViewIndex = 3;
        }

        protected void btnEnviarLink_Click(object sender, EventArgs e)
        {

            if (txtDestinatario.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                  "alert('E-mail do destinatário é obrigatório.');", true);
                return;
            }

            if (Funcoes.ValidaEmail(txtDestinatario.Text))
            {
                Funcoes.SendEmailLinkPesquisa(txtDestinatario.Text, "", Session["TextoEmail"].ToString());
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                  "alert('E-mail Enviado com sucesso.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                    "alert('ERRO - E-mail incorreto.');", true);
            }
        }
    }
}