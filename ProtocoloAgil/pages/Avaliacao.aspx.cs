using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using System.Collections.Generic;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class Avaliacao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                ControlaExibicao(); 
        }


        private void ControlaExibicao()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                //Verifica se está disponível.
                var aprendiz = bd.CA_Aprendizs.Where(p => p.Apr_Codigo == int.Parse(Session["matricula"].ToString())).First();
                if (aprendiz.Apr_HabilitaPesquisa == "N")
                {
                    LB_title.Text = "Esta avaliação não está disponível.";
                    LBInfo.Text = "A secretaria informará o período de preenchimento desta avaliação. Fique atento.";
                    MultiView1.ActiveViewIndex = 1;
                }
                if (aprendiz.Apr_HabilitaPesquisa == "S")
                {
                    MultiView1.ActiveViewIndex = 0;
                    BindRepeater();
                }
            }
        }


        private void BindRepeater()
        {
            using(var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                //preenchimento cabeçalho, de acordo com a alocação vigente do apendiz.
                var aprendiz = bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == int.Parse(Session["matricula"].ToString()) && p.ALAStatus == "A").First();

                LB_Aprendiz.Text = aprendiz.Apr_Nome;
                LB_empresa.Text = aprendiz.ParNomeFantasia;
                LB_Orientador.Text = aprendiz.OriNome;
                LB_Horario.Text = string.Format("{0:HH:mm}", aprendiz.ALAInicioExpediente) + " às " + string.Format("{0:HH:mm}", aprendiz.ALATerminoExpediente);

                /*  O sistema carrega a pesquisa de acordo com o código e gera dinamicamente os controles de acordo com o tipo de questão
                 *  os tipos são: M - Multipla escolha, A - Aberta ou T - Multipla escolha com justificativa.
                 *  cada controle criado é adicionado ao painel Pn_Pesquisa.
                 *  Literais são utilizados para quebra de linha.
                 */

                /* Código totalmente errado. Será criada uma pesquisa para o aprendiz.*/
                Session["PRMT_pesq_parceiro"] = aprendiz.ALAAprendiz;
                var pesquisa = bd.View_Pequisas.Where(p => p.QuePesquisa == 1);
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
                            var textbox = new TextBox { ID = "TB_opcao_" + questao.QueCodigo , Width = 600, Height = 40 ,TextMode = TextBoxMode.MultiLine, CssClass = "fonteTexto" };
                            textbox.Attributes.Add("onkeyup","javascript:IsMaxLength(this, 255);");
                            Pn_Pesquisa.Controls.Add(textbox);
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
            }
        }


        private LiteralControl GetLiteral(string text)
        {
            var literal = new LiteralControl {Text = text};
            return literal;
        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
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
                LB_title.Text = "Sua avaliação foi recebida com sucesso.";
                LBInfo.Text = "Obrigado pela sua participação.";
                MultiView1.ActiveViewIndex = 1;
            }
            catch (ArgumentException ex)
            {
                LB_erro.Text = "Atenção: " + ex.Message;
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000120", ex);
            }
        }

        private IEnumerable<RespostaPesquisa> ValidaFormulario()
        {
            var respostas = new List<RespostaPesquisa>();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                /* Carrega a pesquisa e cria uma lista dos controles presentes no painel Pn_Pesquisa.
                 * para cada questão, avalia o tipo de questão (M,A ou T) e procura o controle que contém a resposta.
                 * Caso o controle não possua nehuma resposta, é avisado ao usuário que aquela questão está pendente.
                 * Se houver, cria-se uma resposta com os dados levantados e esta resposta é adicionada à lista de respostas que será salva no banco de dados. 
                 */

                var list = Pn_Pesquisa.Controls.Cast<Control>().ToList();
                var pesquisa = bd.View_Pequisas.Where(p => p.QuePesquisa == 1);

                foreach ( var questao in pesquisa.OrderBy(p => p.QueOrdemExibicao).Select( p => new {p.QueTexto, p.QueTipoQustao, p.QueCodigo}).Distinct())
                {
                    switch (questao.QueTipoQustao)
                    {
                        case "M":
                            var resposta = list.Where(control => control is RadioButtonList).Where(p => p.ID == "RB_opcao_" + questao.QueCodigo).Cast <RadioButtonList>().First();
                            if (resposta.SelectedValue.Equals(string.Empty)) throw new ArgumentException(" A questão '" +  TrataResposta(questao.QueTexto)  + "' não foi respondida.");
                            respostas.Add(new RespostaPesquisa {ResPesquisaParceiro = int.Parse(Session["PRMT_pesq_parceiro"].ToString()) , ResQuestao = questao.QueCodigo, ResOpcaoEscolhida = int.Parse(resposta.SelectedValue) });
                            break;

                        case "A":
                            var resposta01 = list.Where(control => control is TextBox).Where(p => p.ID == "TB_opcao_" + questao.QueCodigo).Cast<TextBox>().First();
                            if (resposta01.Text.Equals(string.Empty)) throw new ArgumentException(" A questão '" + TrataResposta(questao.QueTexto) + "' não foi respondida.");
                            respostas.Add(new RespostaPesquisa { ResPesquisaParceiro = int.Parse(Session["PRMT_pesq_parceiro"].ToString()), ResQuestao = questao.QueCodigo, 
                                ResOpcaoEscolhida = 0, ResRespostaTexto = resposta01.Text});
                            break;

                        case "T":
                             resposta = list.Where(control => control is RadioButtonList).Where(p => p.ID == "RB_opcao_" + questao.QueCodigo).Cast<RadioButtonList>().First();
                            resposta01 = list.Where(control => control is TextBox).Where(p => p.ID == "TB_opcao_" + questao.QueCodigo).Cast<TextBox>().First();
                            if (resposta.SelectedValue.Equals(string.Empty)) throw new ArgumentException(" A questão '" + TrataResposta(questao.QueTexto) + "' não foi respondida.");
                            respostas.Add(new RespostaPesquisa { ResPesquisaParceiro = int.Parse(Session["PRMT_pesq_parceiro"].ToString()), 
                                ResQuestao = questao.QueCodigo, ResOpcaoEscolhida = int.Parse(resposta.SelectedValue), ResRespostaTexto = resposta01.Text.Equals(string.Empty) ? null : resposta01.Text});
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
    }
}