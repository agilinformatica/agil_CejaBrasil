using System;
using System.Linq;
using System.Text;
using ProtocoloAgil.Base;
using Page = System.Web.UI.Page;
using MenorAprendizWeb.Base;
using System.Net;
using System.IO;
using System.Web;
using ProtocoloAgil.Base.Models;


namespace ProtocoloAgil.pages
{
    public partial class VagaPopup : Page
    {
        public string Arquivo;

        protected void Page_Load(object sender, EventArgs e)
        {
            var reqId = Criptografia.Decrypt(Request.QueryString["reqId"], GetConfig.Key());
            var empresa = Criptografia.Decrypt(Request.QueryString["empresa"], GetConfig.Key());

            PreencheCampos(reqId);

        }


        private void PreencheCampos(string codigo)
        {
            // ----------------------------------preenche campos ------------------------------------>
            using (var repository = new Repository<RequisicoesVagas>(new Context<RequisicoesVagas>()))
            {
                var vaga = repository.Find(Convert.ToInt32(codigo));

                var sb = new StringBuilder();


                var sql = "select ParUniDescricao from CA_ParceirosUnidade where ParUniCodigo = " + vaga.ReqEmpresa + "";
                var con = new Conexao();
                var result = con.Consultar(sql);
                string empresa = "";
                string areaAtuacao = "";

                while (result.Read())
                {
                    empresa = result["ParUniDescricao"].ToString();
                }

                sql = "select AreaDescricao from CA_AreaAtuacao where AreaCodigo = " + vaga.ReqAreaAtuacao + "";
                con = new Conexao();
                result = con.Consultar(sql);

                while (result.Read())
                {

                    areaAtuacao = result["AreaDescricao"].ToString();
                }


                string dados = @"<b> Código da vaga: </b> " + vaga.ReqId.ToString() +
                    "<br/> <b> Empresa: </b> " + empresa +
                    "<br/> <b> Data Solicitação: </b> " + string.Format("{0:dd/MM/yyyy}", vaga.ReqDataSolitação) + "" +
                    "<br/> <b> Quantidade: </b>" + vaga.ReqQuantidade.ToString() + "" +
                    "<br/> <b> Sexo: </b>" + vaga.ReqSexo + "" +
                    "<br/> <b> Horário Entrevista: </b>" + vaga.ReqHorarioEntrevista + "" +
                    "<br/> <b> Substituição: </b>" + vaga.ReqSubstituicao + "" +
                    "<br/> <b> Características Pessoais: </b>" + vaga.ReqCaracteristicasPessoais + "" +
                    "<br/> <b> Área de Atuação: </b>" + areaAtuacao + "" +
                    "<br/> <b> Salário: </b>" + vaga.ReqSalario.ToString() + "" +
                    "<br/> <b> Horário Trabalho: </b>" + vaga.ReqHorarioTrabalho + "" +
                    "<br/> <b> Habilidades: </b>" + vaga.ReqHabilidades + "" +
                    "<br/> <b> Atividades: </b>" + vaga.ReqAtividades + "" +
                    "<br/> <b> Habilidades: </b>" + vaga.ReqHabilidades + "" +
                    "<br/> <b> Benefícios: </b>" + vaga.ReqBeneficios + "" +
                    "<br/> <b> Contato Entrevista: </b>" + vaga.ReqContaoEntrevista + "" +
                    "<br/> <b> Observações: </b>" + vaga.ReqObservacoes + "" +
                    "<br/> <b> Observações Instituição: </b>" + vaga.ReqObservacoesInst + "";


                sb.Append("<br/>");
                sb.Append("<hr/><h5>Dados Sobre a Vaga</h5><hr/><br/>");
                sb.Append("<div style='text-align: justify'>");


                sb.Append(dados);

                lit_Texto_Contrato.Text = sb.ToString();
                lit_Texto_Contrato.DataBind();


                //txtCodigoVaga.Text = vaga.ReqId.ToString();
                //DDEmpresa.SelectedValue = vaga.ReqEmpresa.ToString();
                //txtDataSolicitacao.Text = string.Format("{0:dd/MM/yyyy}", vaga.ReqDataSolitação);
                //txtQuantidade.Text = vaga.ReqQuantidade.ToString();
                //DDSexo.SelectedValue = vaga.ReqSexo;
                //txtHorarioEntrevista.Text = vaga.ReqHorarioEntrevista;
                //DDSubstituicao.SelectedValue = vaga.ReqSubstituicao;
                //txtCaracteristicaPessoal.Text = vaga.ReqCaracteristicasPessoais;
                //DDAreaAtuacao.SelectedValue = vaga.ReqAreaAtuacao.ToString();
                //txtHabilidades.Text = vaga.ReqHabilidades;
                //txtAtividades.Text = vaga.ReqAtividades;
                //txtCartaoEntrevista.Text = vaga.ReqContaoEntrevista;
                //txtObservacao.Text = vaga.ReqObservacoes;
                //txtObservacaoInst.Text = vaga.ReqObservacoesInst;
                //txtBenefício.Text = vaga.ReqObservacoes;
                //txtSalario.Text = vaga.ReqSalario.ToString();
                //txtHorarioTrabalho.Text = vaga.ReqHorarioTrabalho == null || vaga.ReqHorarioTrabalho.Equals(string.Empty) ? "" : vaga.ReqHorarioTrabalho.ToString();

            }
        }




        private void GeraContratoFundacao(int aprendiz) //CONTRATO CEDUC
        {
            // Arquivo = Convert.ToString(aprendiz) + "_CEDUC";
            var sb = new StringBuilder();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.View_Contrato_Apredizs.Where(p => p.Apr_Codigo == aprendiz && p.TurCurso == "002").First();
                var introdutorio = bd.View_Contrato_Apredizs.Where(p => p.Apr_Codigo == aprendiz && p.TurCurso == "001").First();


                string item3 = "";
                string titulo = "";
                string aprRealiza = "";
                string peloPresente = "";
                if (dados.AreaDescricao == "Atendente Lanchonete")
                {
                    titulo = "CONTRATO DE APRENDIZAGEM DE SERVIÇOS EM ATENDIMENTO DE LANCHONETE ";
                    item3 = "3) Do Curso de Aprendizagem em Atendimento em Lanchonete ";
                    aprRealiza = "O APRENDIZ realizará curso de formação profissional de Aprendizagem de Serviços em Atendimento de Lanchonete na ocupação de Aprendiz de Atendente de Lanchonete.";
                    peloPresente = "Pelo presente instrumento particular de contrato, a CONTRATANTE admite o APRENDIZ, comprometendo-se a propiciar-lhe formação profissional, através do Curso de Aprendizagem de Serviços em Atendimento de Lanchonete, conforme programa de aprendizagem desenvolvido sob a orientação da ANUENTE/INTERVENIENTE, o qual se regerá pela legislação pertinente à Aprendizagem de  Serviços em Atendimento de Lanchonete e pelas seguintes cláusulas e condições: ";
                }
                else if (dados.AreaDescricao == "Promotor de Vendas")
                {
                    titulo = "CONTRATO DE APRENDIZAGEM NO COMÉRCIO EM PROMOÇÃO DE VENDAS";
                    item3 = "3) Do Curso de Aprendizagem No Comércio em Promoção de Vendas";
                    aprRealiza = "O APRENDIZ realizará curso de formação profissional de Aprendizagem no Comércio em Promoção de Vendas na ocupação de Aprendiz de Promotor de Vendas.";
                    peloPresente = "Pelo presente instrumento particular de contrato, a CONTRATANTE admite o APRENDIZ, comprometendo-se a propiciar-lhe formação profissional, através de Curso de Aprendizagem no Comércio em Promoção de Vendas, conforme programa de aprendizagem desenvolvido sob a orientação da ANUENTE/INTERVENIENTE, o qual se regerá pela legislação pertinente à Aprendizagem no Comércio em Promoção de Vendas e pelas seguintes cláusulas e condições:";
                }
                else //serviços administrativos
                {
                    titulo = "CONTRATO DE APRENDIZAGEM EM SERVIÇOS ADMINISTRATIVOS ";
                    item3 = "3) Do curso de Aprendizagem em Auxiliar Administrativo";
                    aprRealiza = "O APRENDIZ realizará curso de formação profissional de Aprendizagem em Serviços Administrativos na ocupação de Aprendiz de Serviços Administrativos.";
                    peloPresente = "Pelo presente instrumento particular de contrato, a CONTRATANTE admite o APRENDIZ, comprometendo-se a propiciar-lhe formação profissional, através de Curso de Aprendizagem em Serviços Administrativos, conforme programa de aprendizagem desenvolvido sob sua própria orientação, o qual se regerá pela legislação pertinente à Aprendizagem em  Serviços Administrativos e pelas seguintes cláusulas e condições: ";
                }

                sb.Append("<FONT FACE='arial'>");
                sb.Append("<hr/><h5>" + titulo + "</h5><hr/><br/>"); //##
                sb.Append("<b>1) Qualificação das Partes </b><br/>");

                //CONTRATANTE
                sb.Append("<b>CONTRATANTE:</b>   <br/><br/>");
                sb.Append("<b>Denominação Social: </b>" + dados.UniNome + "<br/><br/>");
                sb.Append("<b>CNPJ:</b> " + Funcoes.FormataCNPJ(dados.UniCGC) + " <b>Endereço:</b> " + dados.UniEndereco + ", Nº " + dados.UniNumeroEndereco + "<br/><br/>");
                sb.Append("<b>Bairro:</b> " + dados.UniBairro + " <b>Cidade:</b> " + dados.UniCidade + " <b>Estado:</b> " + dados.UniEstado + "<br/><br/>");
                sb.Append("<b>CEP:</b> " + Funcoes.FormataCep(dados.UniCEP) + "  <b>Telefone:</b> " + Funcoes.FormataTelefone(dados.UniTelefone) + "<br/><br/>");
                sb.Append("<b>Representante:</b> " + dados.UniRepresentanteLegal + " <b>Cargo:</b> " + dados.UniRepresentanteCargo + "<br/><br/>");
                //APRENDIZ
                sb.Append("<b>APRENDIZ:</b><br/><br/>");
                sb.Append("<b>Nome:</b> " + dados.Apr_Nome + " <b>Data Nascimento:</b> " + Convert.ToDateTime(dados.Apr_DataDeNascimento).ToString("dd/MM/yyyy") + " <b>CPF:</b> " + Funcoes.FormataCPF(dados.Apr_CPF) + "<br/><br/>");
                sb.Append("<b>CTPS:</b> " + dados.Apr_CarteiraDeTrabalho + " <b>Série:</b> " + dados.Apr_Serie_Cartrab + " <b>PIS:</b> " + dados.Apr_PIS + "<br/><br/>");
                sb.Append("<b>RG:</b> " + dados.Apr_CarteiraDeIdentidade + " <b>Endereço:</b> " + dados.Apr_Endereço + ", Nº " + dados.Apr_NumeroEndereco + ", " + dados.Apr_Complemento + "<br/><br/>");
                sb.Append("<b>Bairro:</b> " + dados.Apr_Bairro + " <b>Cidade:</b> " + dados.Apr_Cidade + " <b>Estado:</b> " + dados.Apr_UF + "<br/><br/>");
                sb.Append("<b>CEP:</b> " + dados.Apr_CEP + " <b>Telefone:</b> " + Funcoes.FormataTelefone(dados.Apr_Telefone) + "<br/><br/>");
                sb.Append("<b>Representante:</b> " + dados.Apr_Responsavel + " <b>CPF:</b>" + Funcoes.FormataCPF(dados.Apr_Resp_CPF) + "<br/><br/>");
                //ANUENTE
                sb.Append("<b>ANUENTE/INTERVENIENTE:</b>   <br/><br/>");
                sb.Append("<b>Denominação Social: </b>" + dados.ParUniDescricao + "<br/><br/>");
                sb.Append("<b>CNPJ:</b> " + Funcoes.FormataCNPJ(dados.ParUniCNPJ) + " <b>Endereço:</b> " + dados.ParUniEndereco + ", Nº " + dados.ParUniNumeroEndereco + ", " + dados.ParUniComplemento + "<br/><br/>");
                sb.Append("<b>Bairro:</b> " + dados.ParUniBairro + " <b>Cidade:</b> " + dados.ParUniCidade + " <b>Estado:</b> " + dados.ParUniEstado + "<br/><br/>");
                sb.Append("<b>CEP:</b> " + Funcoes.FormataCep(dados.ParUniCEP) + " <b>Telefone:</b> " + Funcoes.FormataTelefone(dados.ParUniTelefone) + "<br/><br/>");
                sb.Append("<b>Representante:</b> " + dados.ParNomeContato + " <b>Cargo:</b> " + dados.ParCargoRepresentanteLegal + "<br/><br/>");

                //sb.Append("<b>Representante:</b> " ++ " <b>Cargo:</b> " + "<br/><br/>");
                //2
                sb.Append("<b>2) Do Prazo de Vigência</b><br/><br/>");

                sb.Append("O presente contrato vigorará de " + Convert.ToDateTime(dados.Apr_InicioAprendizagem).ToString("dd/MM/yyyy") + " até " + Convert.ToDateTime(dados.Apr_PrevFimAprendizagem).ToString("dd/MM/yyyy") + "<br/><br/>");
                //3
                sb.Append("<b>" + item3 + "</b><br/><br/>"); //##

                sb.Append(aprRealiza); //##
                //pag 2 - contrato aprendizagem
                sb.Append("<div style='page-break-after: always'></div>"); //QUEBRAR PAGINA
                sb.Append("<br/><hr/><h5>" + titulo + "</h5><hr/><br/>"); //##
                sb.Append("<div style='text-align: justify'>");
                sb.Append(peloPresente + "<br/><br/> "); //##

                sb.Append("<b>Cláusula Primeira: DAS PARTES</b><br/><br/>");
                sb.Append("As partes envolvidas na celebração deste contrato estão qualificadas no item 1 da página 1 deste contrato.<br/><br/>");
                //Clausula SEGUNDA
                sb.Append("<b>Cláusula Segunda: DAS OBRIGAÇÕES DA CONTRATANTE</b><br/><br/>");
                sb.Append("A CONTRATANTE, na condição de empregadora, compromete-se a:<br/><br/>");
                sb.Append("<p>1- Remunerar o APRENDIZ com o salário mínimo hora, salvo condição mais favorável, nos termos do artigo 428, § 2º da Consolidação das Leis do Trabalho - CLT;</p>");
                sb.Append("<p>1.1- A remuneração abrange tanto as atividades realizadas na empresa quanto as atividades de formação técnico-profissional ministradas pela CONTRATANTE");
                //sb.Append("<p>1.1- A remuneração abrange tanto as atividades realizadas na empresa quanto as atividades de formação técnico-profissional ministradas pela ANUENTE/INTERVENIENTE;</p>");
                sb.Append("<p>2- Registrar na Carteira de Trabalho e Previdência Social do APRENDIZ a vigência do presente Contrato de Aprendizagem, de acordo com o artigo 4º do Decreto nº. 5.598, de 1º de dezembro de 2005;</p>");
                sb.Append("<p>3- Garantir ao APRENDIZ todos os direitos trabalhistas e previdenciários que lhe for devido; ");
                sb.Append("<p>4- Recolher o FGTS, com alíquota de 2% sobre a remuneração, nos termos do artigo 15, § 7º da Lei nº. 8036, de 11 de maio de 1990, com a redação dada pela Lei nº. 10.097, de 19 de dezembro de 2000;</p>");
                sb.Append("<p>5- Elaborar programa de aprendizagem, com carga horária total de  1280 horas, sendo 400 horas subdivididas em módulos de formação humana, "
                          + "técnico específico e aprendizagem sócio-comunitária e carga horária prática profissional de 880 horas, garantindo a formação profissional de qualidade ao "
                          + "APRENDIZ matriculado em seus cursos, compreendendo atividades teóricas e práticas, metodicamente organizadas em tarefas de complexidade progressiva; </p>");
                sb.Append("<p>6- Acompanhar o desenvolvimento do programa de aprendizagem e manter mecanismos de controle da freqüência e aproveitamento do APRENDIZ nas atividades teóricas "
                          + "e práticas, de forma a garantir que as atividades práticas estejam contextualizadas no programa de aprendizagem previamente elaborado;</p>");
                sb.Append("<p>7- Acompanhar a freqüência do APRENDIZ na escola formal, salvo quando concluído o ensino médio;</p>");
                sb.Append("<p>8- Comunicar ao Ministério do Trabalho e Emprego, para a adoção das medidas cabíveis, as irregularidades trabalhistas praticadas pela CONTRATANTE contra o APRENDIZ.</p><br/>");

                //Clausula TERCEIRA
                sb.Append("<b>Cláusula Terceira: DAS OBRIGAÇÕES DO APRENDIZ</b><br/><br/>");
                sb.Append("O APRENDIZ compromete-se a: <br/>");
                sb.Append("<p>1- Participar regularmente das aulas e demais atos escolares da CONTRATANTE, bem como a cumprir seu Regimento;</p>");
                sb.Append("<p>2- Freqüentar a escola regular, salvo quando concluído o ensino médio, devendo apresentar, bimestralmente, o respectivo comprovante de freqüência, emitido pela instituição de ensino;</p>");
                ////=================== data e hora a ser colocada      

                sb.Append("<p>3- Cumprir, com exatidão, a jornada de trabalho de 04 (quatro) horas por dia da seguinte forma: de " + Convert.ToDateTime(dados.ALAInicioExpediente).ToShortTimeString() + " às " + Convert.ToDateTime(dados.ALATerminoExpediente).ToShortTimeString() + " horas na ANUENTE/INTERVENIENTE , de " + Convert.ToDateTime(dados.TurInicio).ToShortTimeString() + " às " + Convert.ToDateTime(dados.Termino).ToShortTimeString() + " horas na CONTRATANTE às " + dados.DiaDaSemana + ", sendo:</p>");
                sb.Append("<p>3.1- No período compreendido entre " + Convert.ToDateTime(introdutorio.ALADataInicio).ToShortDateString() + " a " + Convert.ToDateTime(introdutorio.ALADataPrevTermino).ToShortDateString() + ", a jornada de trabalho será cumprida exclusiva e integralmente na CONTRATANTE;</p>");
                // sb.Append("<p>3.1- No período compreendido entre " + Convert.ToDateTime(dados.ALADataInicio).ToShortDateString() + " a " + Convert.ToDateTime(dados.ALADataPrevTermino).ToShortDateString() + ", a jornada de trabalho será cumprida exclusiva e integralmente na CONTRATANTE;</p>");
                sb.Append("<p>3.2- Após o período referido no item 3.1, a jornada de trabalho será cumprida na CONTRATANTE, durante um dia da semana, e na ANUENTE/INTERVENIENTE, em quatro dias da semana e nunca aos finais de semana;</p>");
                sb.Append("<p>3.3- Ao longo da vigência do presente contrato, para todos os fins, e conforme carga horária constante do programa de aprendizagem, o APRENDIZ também terá que cumprir a jornada de trabalho em um sábado letivo por ano na CONTRATANTE;</p>");
                sb.Append("<p>3.4- Cumprir a carga horária de 16 horas de aprendizagem sócio-comunitária que será distribuída durante toda a vigência do contrato;</p>");
                sb.Append("<p>4- Apresentar-se à empresa ANUENTE/INTERVENIENTE para prestar serviços em seu estabelecimento, nos horários e dias previamente ajustados, obedecendo sempre à jornada semanal estipulada no presente contrato;</p>");
                sb.Append("<p>5- É da responsabilidade do aprendiz frequentar e participar de sua formação teórica e prática. A ausência nas atividades ocasionará desligamento automático do Programa de Aprendizagem;</p>");
                sb.Append("<p>6- Exibir à empresa ANUENTE/INTERVENIENTE, sempre que solicitado, documentação emitida pela CONTRATANTE, que comprove sua freqüência às atividades teóricas e o resultado de seu aproveitamento;</p>");
                sb.Append("<p>7- Obedecer às normas e regulamentos vigentes na empresa CONTRATANTE e na ANUENTE/INTERVENIENTE;</p>");
                sb.Append("<p>8- O APRENDIZ declara ciência que é proibida a utilização de aparelho celular ou equipamentos similares no curso durante as atividades, inclusive nas aulas práticas e teóricas.</p><br/>");

                //Clausula QUARTA
                sb.Append("<b>Cláusula Quarta: DAS OBRIGAÇÕES DA ANUENTE / INTERVENIENTE</b><br/><br/>");
                sb.Append("A ANUENTE/INTERVENIENTE compromete-se a:<br/>");
                sb.Append("<p>1-	Propiciar a prática profissional de 1280 horas ao APRENDIZ, conforme programa elaborado pela CONTRATANTE;</p>");
                sb.Append("<p>2- Disponibilizar espaço adequado para a aprendizagem prática do APRENDIZ;</p>");
                sb.Append("<p>3- Definir, em conjunto com o supervisor pedagógico da CONTRATANTE, as funções que o APRENDIZ irá desempenhar durante a aprendizagem prática;</p>");
                sb.Append("<p>4- Informar, à CONTRATANTE, eventuais faltas injustificáveis para fins do respectivo desconto na remuneração do mesmo;</p>");
                sb.Append("<p>5- Indicar um profissional que será o responsável pela coordenação do APRENDIZ na empresa, denominado supervisor;</p>");
                sb.Append("<p>6- Participar, quando solicitado, dos encontros de avaliação do desempenho profissional do APRENDIZ na empresa;</p>");
                sb.Append("<p>7- Emitir relatórios técnicos sobre o desempenho do APRENDIZ na empresa;</p>");
                sb.Append("<p>8- Mediante emissão e envio de boleto bancário por parte da CONTRATANTE, a ANUENTE/INTERVENIENTE deverá providenciar o pagamento da quantia mensal de "
                            + "R$" + dados.ValorCeduc + " (" + Funcoes.ValorPorExtenso((float)dados.ValorCeduc) + "), acrescida do valor do vale-transporte, relativa aos custos referentes à contratação e formação do "
                            + "APRENDIZ, conforme planilha financeira anexa, que integra o presente instrumento para todos os efeitos. O citado pagamento poderá ser feito sem acréscimos legais "
                            + "até o dia 10 (dez) de cada mês que antecede o pagamento do salário do APRENDIZ. O valor previsto nesta cláusula sofrerá reajuste, a qualquer tempo, sempre que "
                            + "o valor do salário mínimo oficial vigente for revisado, e anualmente, de acordo com a variação do INPC ou outro índice oficial que venha substituí-lo;</p><br/>");
                sb.Append("<p>9- O custo do vale transporte será sempre cobrado com base em uma estimativa no número de dias e quantidade e valor do(s) vale(s)-transporte(s) necessário(s) "
                            + "ao deslocamento do APRENDIZ no mês vigente. Excepcionalmente na primeira cobrança, a CONTRATANTE cobrará os custos de vale transporte referentes ao mês vigente da data de "
                            + "vencimento, bem como do custo de vale transporte do mês anterior;</p>");
                sb.Append("<p>10- Em caso de atraso por parte da ANUENTE/INTERVENIENTE no pagamento e repasse do custo mensal à CONTRATANTE, na forma e prazo descritos nos itens 9 e 10 "
                            + "desta cláusula, serão devidos juros de mora de 1% (um por cento ao mês), calculados pro rata die e multa de 2% (dois por cento) sobre o valor da nota fiscal.</p>");

                //Clausula QUINTA
                sb.Append("<b>Cláusula Quinta: DO PRAZO DE VIGÊNCIA</b><br/>");
                sb.Append("O presente contrato vigorará de acordo com a legislação aplicável pelo prazo definido no item 2 da página 1 deste contrato.<br/><br/>");
                //Clausula SEXTA
                sb.Append("<b>Cláusula Sexta: DA RESCISÃO</b><br/><br/>");
                // sb.Append("<div class='texto_bloco'>");
                sb.Append("O presente contrato será automaticamente rescindido quando for atingido seu termo fixado na Cláusula Quinta ou, quando o APRENDIZ completar 24 anos, "
                + "prevalecendo o evento de primeira ocorrência ou ainda, antecipadamente, na hipótese de desempenho insuficiente ou inadaptação do aprendiz; "
                + "falta disciplinar grave; ausência injustificada à escola que implique em perda do ano letivo; ou a pedido do aprendiz, nos termos do artigo 433 da "
                + "Consolidação das Leis do Trabalho – CLT.<br/><br/>");
                sb.Append("E por estarem justos e contratados, assinam o presente instrumento em três vias de igual teor e forma, na presença de duas testemunhas.<br/>");
                //  sb.Append("</div>");
                //sb.Append("<div style='page-break-after: always'></div>"); //QUEBRAR PAGINA

                sb.Append("<br/>Belo Horizonte, " + Convert.ToDateTime(dados.Apr_DataContrato).Day.ToString() + " de " + Convert.ToDateTime(dados.Apr_DataContrato).ToString("MMMM") + " de " + Convert.ToDateTime(dados.Apr_DataContrato).Year.ToString() + "<br/><br/><br/>");
                //ASSINATURAS
                sb.Append("CONTRATANTE:_________________________________________________________________________<br/>");
                sb.Append("<div style='text-align: center'>" + dados.UniNome + "</div><br/>");
                sb.Append("<div style='text-align: center'>" + dados.UniRepresentanteLegal + "</div><br/><br/>");

                sb.Append("APRENDIZ:____________________________________________________________________________<br/>");
                sb.Append("<div style='text-align: center'>" + dados.Apr_Nome + "</div> <br/><br/><br/>");
                sb.Append("<div style='text-align: LEFT'>RESP. P/ APRENDIZ:___________________________________________________________________</div><br/>");
                sb.Append("<div style='text-align: center'>" + dados.Apr_Responsavel + "</div> <br/><br/>");


                sb.Append("ANUENTE/INTERVENIENTE:________________________________________________________<br/>");
                sb.Append("<div style='text-align: center'>" + dados.ParDescricao + "</div><br/>");
                sb.Append("<div style='text-align: center'>" + dados.ParNomeContato + "</div>");

                sb.Append("<p style='text-align: Left'><img src='../images/Assinaturas/AssinaturaTestemunhas.jpg' /></p>");
                //sb.Append("<b>Testemunhas:</b> <br/>");
                //sb.Append("<p style='text-align: center'><img src='../images/Assinaturas/IsabelaCC.jpg' style='height: 40px; width: 200px' /></p>");
                //sb.Append("TESTEMUNHA 1:_________________________________________________________________");
                //sb.Append("<p style='text-align: center'>Isabela Carvalho Campos Sá</p>");
                //sb.Append("<br/>");
                //sb.Append("<p style='text-align: center'><img src='../images/Assinaturas/SaraLopes.jpg' style='height: 40px; width: 200px' /></p>");
                //sb.Append("TESTEMUNHA 2:_________________________________________________________________<br/>");
                //sb.Append("<p style='text-align: center'>Sara Lopes Fonseca</p>");               
                sb.Append("</div>");
                /////////////////TABELA DE CUSTOS
                sb.Append("<div style='page-break-after: always'></div>"); //QUEBRAR PAGINA
                sb.Append("<hr><h5> Planilha de Custo </h5>");
                sb.Append("<h5> Programa de Aprendizagem </h5><hr/>");
                sb.Append("<br/><br/><br/>");
                sb.Append(@"
                <table border='1' cellpadding='0' cellspacing='0' style='width: 633px' width='633'>
				<colgroup>
					<col />
					<col />
					<col />
				</colgroup>
				<tbody>
					<tr height='40'>
						<td height='40' style='height: 40px; width: 344px'>
							<strong>DESCRI&Ccedil;&Atilde;O</strong></td>
						<td style='width: 240px'>
							<strong>LEI BRASILEIRA QUE OBRIGA OS PAGAMENTOS</strong></td>
						<td style='width: 49px'>
							<strong>VALOR</strong></td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							<strong>Sal&aacute;rio contratual mensal/ 4 HORAS trabalhadas</strong></td>
						<td>
							&nbsp;</td>
						<td align='right'>
							<strong>343,89</strong></td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							&nbsp;</td>
						<td>
							&nbsp;</td>
						<td>
							&nbsp;</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Encargos da previd&ecirc;ncia social (INSS)</td>
						<td>
							Lei 8.212/91</td>
						<td align='right'>
							92,16</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Fundo de garantia por tempo de servi&ccedil;o(FGTS)</td>
						<td>
							Lei 8.036/90&nbsp; e art. CLT</td>
						<td align='right'>
							6,88</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							50% s/ Fundo de garantia por tempo de servi&ccedil;o(FGTS)</td>
						<td>
							Lei 8.036/90&nbsp; e art. CLT</td>
						<td align='right'>
							3,44</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Programa de Integra&ccedil;&acirc;o Social (PIS)</td>
						<td>
							Medida Provis&oacute;ria N&ordm; 2.037</td>
						<td align='right'>
							3,44</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							D&eacute;cimo terceiro (um doze avos)</td>
						<td>
							Lei 4.749/65</td>
						<td align='right'>
							28,66</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Encargos da previd&ecirc;ncia social (INSS) s/ 13&ordm;</td>
						<td>
							Lei 8.212/91</td>
						<td align='right'>
							7,68</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Fundo de garantia (FGTS) s/ 13&ordm;</td>
						<td>
							Lei 8.036/90&nbsp; e art. CLT</td>
						<td align='right'>
							0,57</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							50% fundo garantia do m&ecirc;s s/ 13&ordm;</td>
						<td>
							Lei 8.036/90&nbsp; e art. CLT</td>
						<td align='right'>
							0,29</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Programa de Integra&ccedil;&acirc;o Social (PIS) s/ 13&ordm;</td>
						<td>
							Medida Provis&oacute;ria N&ordm; 2.037</td>
						<td align='right'>
							0,29</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							F&eacute;rias (um doze avos) + adicional de um ter&ccedil;o</td>
						<td>
							Art.130 da CLT e Constitui&ccedil;&atilde;o Federal</td>
						<td align='right'>
							38,21</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Encargos da previd&ecirc;ncia social (INSS) s/ f&eacute;rias</td>
						<td>
							Lei 8.212/91</td>
						<td align='right'>
							10,24</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Fundo de garantia (FGTS) s/ f&eacute;rias</td>
						<td>
							Lei 8.036/90&nbsp; e art. CLT</td>
						<td align='right'>
							0,76</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							50% fundo garantia do m&ecirc;s s/ f&eacute;rias</td>
						<td>
							Lei 8.036/90&nbsp; e art. CLT</td>
						<td align='right'>
							0,38</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Programa de Integra&ccedil;&acirc;o Social (PIS) s/ f&eacute;rias</td>
						<td>
							Medida Provis&oacute;ria N&ordm; 2.037</td>
						<td align='right'>
							0,38</td>
					</tr>
					<tr height='20'>
						<td style='height: 20px; background-color: #CCCCCC'>
							Vale Transporte</td>
						<td style='background-color: #cccccc'  >
							&nbsp;</td>
						<td style='background-color: #cccccc'  >
							***</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px' >
							<strong>Custo total sal&aacute;rio no m&ecirc;s</strong></td>
						<td>
							&nbsp;</td>
						<td align='right'>
							<strong>537,27</strong></td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px'>
							Mensalidade paga ao CejaBrasil</td>
						<td>
							&nbsp;</td>
						<td align='right'>
							241,52</td>
					</tr>
					<tr height='20'>
						<td height='20' style='height: 20px; background-color: #CCCCCC'>
							<strong>Custo total por jovem aprendiz</strong></td>
						<td style='background-color: #cccccc'>
							&nbsp;</td>
						<td align='right' style='background-color: #cccccc'>
							<strong>778,79</strong></td>
					</tr>
				</tbody>
			</table>       
                    ");
                //FIM TABELA      
                sb.Append("<br/>*** Nesta planilha não está incluído o valor do Vale Transporte");
                sb.Append("</FONT>");
                lit_Texto_Contrato.Text = sb.ToString();
                lit_Texto_Contrato.DataBind();
            }
        }

        private void GeraContratoEmpresa(int aprendiz) //CONTRATO EMPRESA
        {
            //   Arquivo = Convert.ToString(aprendiz) + "_EMPRESA";
            var sb = new StringBuilder();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.View_Contrato_Apredizs.Where(p => p.Apr_Codigo == aprendiz && p.TurCurso == "002").First();
                var introdutorio = bd.View_Contrato_Apredizs.Where(p => p.Apr_Codigo == aprendiz && p.TurCurso == "001").First();


                string item3 = "";
                string titulo = "";
                string aprRealiza = "";
                string peloPresente = "";
                if (dados.AreaDescricao == "Atendente Lanchonete" || dados.AreaDescricao == "Atendente")
                {
                    titulo = "CONTRATO DE APRENDIZAGEM DE SERVIÇOS EM ATENDIMENTO DE LANCHONETE ";
                    item3 = "3) Do Curso de Aprendizagem em Atendimento em Lanchonete ";
                    aprRealiza = "O APRENDIZ realizará curso de formação profissional de Aprendizagem de Serviços em Atendimento de Lanchonete na ocupação de Aprendiz de Atendente de Lanchonete.";
                    peloPresente = "Pelo presente instrumento particular de contrato, a CONTRATANTE admite o APRENDIZ, comprometendo-se a propiciar-lhe formação profissional, através do Curso de Aprendizagem de Serviços em Atendimento de Lanchonete, conforme programa de aprendizagem desenvolvido sob a orientação da ANUENTE/INTERVENIENTE, o qual se regerá pela legislação pertinente à Aprendizagem de  Serviços em Atendimento de Lanchonete e pelas seguintes cláusulas e condições: ";
                }
                else if (dados.AreaDescricao == "Promotor de Vendas")
                {
                    titulo = "CONTRATO DE APRENDIZAGEM NO COMÉRCIO EM PROMOÇÃO DE VENDAS";
                    item3 = "3) Do Curso de Aprendizagem No Comércio em Promoção de Vendas";
                    aprRealiza = "O APRENDIZ realizará curso de formação profissional de Aprendizagem no Comércio em Promoção de Vendas na ocupação de Aprendiz de Promotor de Vendas.";
                    peloPresente = "Pelo presente instrumento particular de contrato, a CONTRATANTE admite o APRENDIZ, comprometendo-se a propiciar-lhe formação profissional, através de Curso de Aprendizagem no Comércio em Promoção de Vendas, conforme programa de aprendizagem desenvolvido sob a orientação da ANUENTE/INTERVENIENTE, o qual se regerá pela legislação pertinente à Aprendizagem no Comércio em Promoção de Vendas e pelas seguintes cláusulas e condições:";
                }
                else //serviços administrativos
                {
                    titulo = "CONTRATO DE APRENDIZAGEM EM SERVIÇOS ADMINISTRATIVOS ";
                    item3 = "3) Do curso de Aprendizagem em Auxiliar Administrativo";
                    aprRealiza = "O APRENDIZ realizará curso de formação profissional de Aprendizagem em Serviços Administrativos na ocupação de Aprendiz de Serviços Administrativos.";
                    peloPresente = "Pelo presente instrumento particular de contrato, a CONTRATANTE admite o APRENDIZ, comprometendo-se a propiciar-lhe formação profissional, através de Curso de Aprendizagem em Serviços Administrativos, conforme programa de aprendizagem desenvolvido sob sua própria orientação, o qual se regerá pela legislação pertinente à Aprendizagem em  Serviços Administrativos e pelas seguintes cláusulas e condições: ";
                }

                sb.Append("<FONT FACE='arial'>");
                sb.Append("<hr/><h5>" + titulo + "</h5><hr/><br/>"); //##
                sb.Append("<b>1) Qualificação das Partes </b><br/>");

                //CONTRATANTE
                sb.Append("<b>CONTRATANTE:</b>   <br/><br/>");
                sb.Append("<b>Denominação Social: </b>" + dados.ParUniDescricao + "<br/><br/>");
                sb.Append("<b>CNPJ:</b> " + Funcoes.FormataCNPJ(dados.ParUniCNPJ) + " <b>Endereço:</b> " + dados.ParUniEndereco + ", Nº " + dados.ParUniNumeroEndereco + ", " + dados.ParUniComplemento + "<br/><br/>");
                sb.Append("<b>Bairro:</b> " + dados.ParUniBairro + " <b>Cidade:</b> " + dados.ParUniCidade + " <b>Estado:</b> " + dados.ParUniEstado + "<br/><br/>");
                sb.Append("<b>CEP:</b> " + Funcoes.FormataCep(dados.ParUniCEP) + " <b>Telefone:</b> " + Funcoes.FormataTelefone(dados.ParUniTelefone) + "<br/><br/>");
                sb.Append("<b>Representante:</b> " + dados.ParNomeContato + "  <b>Cargo:</b> " + dados.ParCargoRepresentanteLegal + "<br/><br/>");
                //APRENDIZ
                sb.Append("<b>APRENDIZ:</b><br/><br/>");
                sb.Append("<b>Nome:</b> " + dados.Apr_Nome + " <b>Data Nascimento:</b> " + Convert.ToDateTime(dados.Apr_DataDeNascimento).ToString("dd/MM/yyyy") + " <b>CPF:</b> " + Funcoes.FormataCPF(dados.Apr_CPF) + "<br/><br/>");
                sb.Append("<b>CTPS:</b> " + dados.Apr_CarteiraDeTrabalho + " <b>Série:</b> " + dados.Apr_Serie_Cartrab + " <b>PIS:</b> " + dados.Apr_PIS + "<br/><br/>");
                sb.Append("<b>RG:</b> " + dados.Apr_CarteiraDeIdentidade + " <b>Endereço:</b> " + dados.Apr_Endereço + ", Nº " + dados.Apr_NumeroEndereco + ", " + dados.Apr_Complemento + "<br/><br/>");
                //sb.Append("<b>RG:</b> " + dados.Apr_CarteiraDeIdentidade + " <b>Endereço:</b> " + dados.Apr_Endereço + ", Nº " + dados.Apr_NumeroEndereco + "<br/><br/>");
                sb.Append("<b>Bairro:</b> " + dados.Apr_Bairro + " <b>Cidade:</b> " + dados.Apr_Cidade + "<br/><br/>");
                sb.Append("<b>CEP:</b> " + Funcoes.FormataCep(dados.Apr_CEP) + " <b>Estado:</b> " + dados.Apr_UF + " <b>Telefone:</b> " + Funcoes.FormataTelefone(dados.Apr_Telefone) + "<br/><br/>");
                sb.Append("<b>Responsável Legal:</b> " + dados.Apr_Responsavel + " <b>CPF:</b> " + Funcoes.FormataCPF(dados.Apr_Resp_CPF) + "<br/><br/>");
                //ANUENTE   
                sb.Append("<b>ANUENTE/INTERVENIENTE:</b>   <br/><br/>");
                sb.Append("<b>Denominação Social: </b>" + dados.UniNome + "<br/><br/>");
                sb.Append("<b>CNPJ:</b> " + Funcoes.FormataCNPJ(dados.UniCGC) + " <b>Endereço:</b> " + dados.UniEndereco + ", Nº " + dados.UniNumeroEndereco + "<br/><br/>");
                sb.Append("<b>Bairro:</b> " + dados.UniBairro + " <b>Cidade:</b> " + dados.UniCidade + " <b>Estado:</b> " + dados.UniEstado + "<br/><br/>");
                sb.Append("<b>CEP:</b> " + Funcoes.FormataCep(dados.UniCEP) + "  <b>Telefone:</b> " + Funcoes.FormataTelefone(dados.UniTelefone) + "<br/><br/>");
                sb.Append("<b>Representante:</b> " + dados.UniRepresentanteLegal + " <b>Cargo:</b> " + dados.UniRepresentanteCargo + "<br/><br/>");

                //2
                sb.Append("<b>2) Do Prazo de Vigência</b><br/><br/>");
                sb.Append("<div style='text-align: justify'>");

                sb.Append("O presente contrato vigorará de " + Convert.ToDateTime(dados.Apr_InicioAprendizagem).ToString("dd/MM/yyyy") + " até " + Convert.ToDateTime(dados.Apr_PrevFimAprendizagem).ToString("dd/MM/yyyy") + "<br/><br/>");
                //3
                sb.Append("<b>" + item3 + "</b><br/><br/>"); //##

                sb.Append(aprRealiza); //##
                //pag 2 - contrato aprendizagem
                sb.Append("<div style='page-break-after: always'></div>"); //QUEBRAR PAGINA
                sb.Append("<br/><hr/><h5>" + titulo + "</h5><hr/><br/>"); //##
                // sb.Append("<div class='texto_bloco'>");

                sb.Append(peloPresente + "<br/><br/> "); //##

                sb.Append("<b>Cláusula Primeira: DAS PARTES</b><br/><br/>");
                sb.Append("As partes envolvidas na celebração deste contrato estão qualificadas no item 1 da página 1 deste contrato.<br/><br/>");
                //Clausula SEGUNDA
                sb.Append("<b>Cláusula Segunda: DAS OBRIGAÇÕES DA CONTRATANTE</b><br/><br/>");
                sb.Append("A CONTRATANTE, na condição de empregadora, compromete-se a:<br/><br/>");
                sb.Append("<p>1- Remunerar o APRENDIZ com o salário mínimo hora, salvo condição mais favorável, nos termos do artigo 428, § 2º da Consolidação das Leis do Trabalho - CLT;</p>");
                sb.Append("<p>1.1- A remuneração abrange tanto as atividades realizadas na empresa quanto as atividades de formação técnico-profissional ministradas pela ANUENTE/INTERVENIENTE;</p>");
                sb.Append("<p>2- Registrar na Carteira de Trabalho e Previdência Social do APRENDIZ a vigência do presente Contrato de Aprendizagem, de acordo com o artigo 4º do Decreto nº. 5.598, de 1º de dezembro de 2005;</p>");
                sb.Append("<p>3- Garantir ao APRENDIZ todos os direitos trabalhistas e previdenciários que lhe for devido; ");
                sb.Append("<p>4- Recolher o FGTS, com alíquota de 2% sobre a remuneração, nos termos do artigo 15, § 7º da Lei nº. 8036, de 11 de maio de 1990, com a redação dada pela Lei nº. 10.097, de 19 de dezembro de 2000;</p>");
                sb.Append("<p>5- Propiciar a prática profissional de 1280 horas ao APRENDIZ, conforme programa elaborado pela ANUENTE/INTERVENIENTE;</p>");
                sb.Append("<p>6- Descontar, da remuneração do aprendiz, as faltas injustificadas às atividades a serem exercidas no espaço físico da ANUENTE/INTERVENIENTE;</p>");
                sb.Append("<p>7- Mediante emissão e envio de boleto bancário por parte da ANUENTE/INTERVENIENTE, a CONTRATANTE deverá providenciar o pagamento da quantia mensal de R$" + dados.ALAValorTaxa + " (" + Funcoes.ValorPorExtenso((float)dados.ALAValorTaxa) + "), "
                              + "relativa aos custos referentes à contratação e formação do APRENDIZ. O citado pagamento poderá ser feito sem acréscimos legais até o dia 10 (dez) de cada mês.  O valor previsto nesta "
                              + "cláusula sofrerá reajuste anualmente, de acordo com a variação do INPC ou outro índice oficial que venha substituí-lo, ou a qualquer tempo, desde que as partes estejam de acordo;</p>");
                sb.Append("<p>8- Em caso de atraso por parte da CONTRATANTE no pagamento e repasse do custo mensal à ANUNENTE/INTERVENIENTE, na forma e prazo descritos no item 8 desta cláusula, serão devidos juros "
                              + "de mora de 1% (um por cento ao mês), calculados pro rata die e multa de 2% (dois por cento) sobre o valor da nota fiscal;</p>");
                sb.Append("<p>9- Repassar à ANUENTE/INTERVENIENTE, nas hipóteses de rescisão contratual com o aprendiz, o “Termo de Rescisão do Contrato de Trabalho”, “Comunicado de Rescisão”, cópia do pedido de "
                              + "demissão do aprendiz (quando for o caso), bem como informar o último dia de trabalho e o motivo da rescisão. </p><br/><br/>");

                //Clausula TERCEIRA
                sb.Append("<div style='page-break-after: always'></div>"); //QUEBRAR PAGINA
                sb.Append("<b>Cláusula Terceira: DAS OBRIGAÇÕES DO APRENDIZ</b><br/><br/>");
                sb.Append("O APRENDIZ compromete-se a: <br/><br/>");
                sb.Append("<p>1- Participar regularmente das aulas e demais atos escolares da ANUENTE/INTERVENIENTE, bem como a cumprir seu Regimento;</p>");
                sb.Append("<p>2- Freqüentar a escola regular, salvo quando concluído o ensino médio, devendo apresentar, bimestralmente, o respectivo comprovante de freqüência, emitido pela instituição de ensino;</p>");
                //=============data a ser colocada   
                sb.Append("<p>3- Cumprir, com exatidão, a jornada de trabalho de 04 (quatro) horas por dia da seguinte forma: de " + Convert.ToDateTime(dados.ALAInicioExpediente).ToShortTimeString() + " às " + Convert.ToDateTime(dados.ALATerminoExpediente).ToShortTimeString() + " horas na CONTRATANTE, de " + Convert.ToDateTime(dados.TurInicio).ToShortTimeString() + " às " + Convert.ToDateTime(dados.Termino).ToShortTimeString() + " horas na ANUENTE/INTERVENIENTE às " + dados.DiaDaSemana + ", sendo:</p>");
                sb.Append("<p>3.1- No período compreendido entre " + Convert.ToDateTime(introdutorio.ALADataInicio).ToShortDateString() + " a " + Convert.ToDateTime(introdutorio.ALADataPrevTermino).ToShortDateString() + ", a jornada de trabalho será cumprida exclusiva e integralmente na ANUENTE/INTERVENIENTE;</p>");
                // sb.Append("<p>3.1- No período compreendido entre " + Convert.ToDateTime(dados.ALADataInicio).ToShortDateString() + " a " + Convert.ToDateTime(dados.ALADataPrevTermino).ToShortDateString() + ", a jornada de trabalho será cumprida exclusiva e integralmente na ANUENTE/INTERVENIENTE;</p>");
                sb.Append("<p>3.2- Após o período referido no item 3.1, a jornada de trabalho será cumprida na ANUENTE/INTERVENIENTE, durante um dia da semana, e na CONTRATANTE, em quatro dias da semana e nunca aos finais de semana;</p>");
                sb.Append("<p>3.3- Ao longo da vigência do presente contrato, para todos os fins, e conforme carga horária constante do programa de aprendizagem, o APRENDIZ também terá que cumprir jornada de trabalho em um sábado, por ano, específico na ANUENTE/INTERVENIENTE;</p>");
                sb.Append("<p>3.4- Cumprir a carga horária de 16 horas de aprendizagem sócio-comunitária que será distribuída durante toda a vigência do contrato;</p>");
                sb.Append("<p>4- Apresentar-se à CONTRATANTE para prestar serviços em seu estabelecimento, nos horários e dias previamente ajustados, obedecendo sempre à jornada semanal estipulada no presente contrato;</p>");
                sb.Append("<p>5- É da responsabilidade do aprendiz frequentar e participar de sua formação teórica e prática. A ausência nas atividades ocasionará desligamento automático do Programa de Aprendizagem;</p>");
                sb.Append("<p>6- Exibir à CONTRATANTE, sempre que solicitado, documentação emitida pela ANUENTE/INTERVENIENTE, que comprove sua freqüência às atividades teóricas e o resultado de seu aproveitamento;</p>");
                sb.Append("<p>7- Obedecer às normas e regulamentos vigentes na empresa CONTRATANTE e na ANUENTE/INTERVENIENTE;</p>");
                sb.Append("<p>8- O APRENDIZ declara ciência que é proibida a utilização de aparelho celular ou equipamentos similares no curso durante as atividades, inclusive nas aulas práticas e teóricas.</p>");
                sb.Append("<b>Parágrafo Único</b>: Nos termos do artigo 432 da Consolidação das Leis do Trabalho – CLT são vedadas a prorrogação e a compensação de jornada.<br/>");
                //Clausula QUARTA
                sb.Append("<b><br/>Cláusula Quarta: DAS OBRIGAÇÕES DA ANUENTE / INTERVENIENTE</b><br/><br/>");
                sb.Append("A ANUENTE/INTERVENIENTE compromete-se a:<br/>");
                sb.Append("<p>1- Elaborar programa de aprendizagem, com carga horária total de 1280 horas, sendo 400 horas subdivididas em módulos de formação humana, técnico específico e "
                + "aprendizagem sócio-comunitária e carga horária prática profissional de 880 horas, garantindo a formação profissional de qualidade ao APRENDIZ matriculado em seus "
                + "cursos, compreendendo atividades teóricas e práticas, metodicamente organizadas em tarefas de complexidade progressiva;<br/></p>");
                sb.Append("<p>2- Acompanhar o desenvolvimento do programa de aprendizagem e manter mecanismos de controle da freqüência e aproveitamento do APRENDIZ nas atividades teóricas "
                + "e práticas, de forma a garantir que as atividades práticas estejam contextualizadas no programa de aprendizagem previamente elaborado;</p>");
                sb.Append("<p>3- Acompanhar a freqüência do APRENDIZ na escola formal, salvo quando concluído o ensino médio; </p>");
                sb.Append("<p>4- Comunicar ao Ministério do Trabalho e Emprego, para a adoção das medidas cabíveis, as irregularidades trabalhistas praticadas pela CONTRATANTE contra o APRENDIZ.</p><br/>");
                //Clausula QUINTA
                sb.Append("<b>Cláusula Quinta: DO PRAZO DE VIGÊNCIA</b><br/>");
                sb.Append("O presente contrato vigorará de acordo com a legislação aplicável pelo prazo definido no item 2 da página 1 deste contrato.<br/><br/>");
                //Clausula SEXTA
                sb.Append("<b>Cláusula Sexta: DA RESCISÃO</b><br/><br/>");
                sb.Append("O presente contrato será automaticamente rescindido quando for atingido seu termo fixado na Cláusula Quinta ou, quando o APRENDIZ completar 24 anos, "
                + "prevalecendo o evento de primeira ocorrência ou ainda, antecipadamente, na hipótese de desempenho insuficiente ou inadaptação do aprendiz; "
                + "falta disciplinar grave; ausência injustificada à escola que implique em perda do ano letivo; ou a pedido do aprendiz, nos termos do artigo 433 da "
                + "Consolidação das Leis do Trabalho – CLT.<br/><br/>");
                sb.Append("E por estarem justos e contratados, assinam o presente instrumento em três vias de igual teor e forma, na presença de duas testemunhas.<br/>");
                //sb.Append("</div>");
                //sb.Append("<div style='page-break-after: always'></div>"); //QUEBRAR PAGINA
                //sb.Append("<br/><br/>Belo Horizonte, " + DateTime.Now.Day.ToString() + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.Year + "<br/><br/>");
                sb.Append("<br/><br/>Belo Horizonte, " + Convert.ToDateTime(dados.Apr_DataContrato).Day.ToString() + " de " + Convert.ToDateTime(dados.Apr_DataContrato).ToString("MMMM") + " de " + Convert.ToDateTime(dados.Apr_DataContrato).Year.ToString() + "<br/><br/><br/>");
                //ASSINATURAS


                sb.Append("CONTRATANTE:_________________________________________________________________________<br/>");
                sb.Append("<div style='text-align: center'>" + dados.ParDescricao + "</div><br/>");
                sb.Append("<div style='text-align: center'>" + dados.ParNomeContato + "</div><br/><br/><br/>");

                sb.Append("APRENDIZ:____________________________________________________________________________<br/>");
                sb.Append("<div style='text-align: center'>" + dados.Apr_Nome + "</div><br/><br/><br/>");

                sb.Append("<div style='text-align: LEFT'>RESP. P/ APRENDIZ:___________________________________________________________________</div><br/>");
                sb.Append("<div style='text-align: center'>" + dados.Apr_Responsavel + "</div> <br/><br/><br/>");

                sb.Append("ANUENTE/INTERVENIENTE:_______________________________________________________<br/>");
                sb.Append("<div style='text-align: center'>" + dados.UniNome + "</div><br/>");
                sb.Append("<div style='text-align: center'>" + dados.UniRepresentanteLegal + "</div><br/><br/><br/>");

                sb.Append("<p style='text-align: Left'><img src='../images/Assinaturas/AssinaturaTestemunhas.jpg' /></p>");
                //sb.Append("<b>Testemunhas:</b> <br/>");
                //sb.Append("<p style='text-align: center'><img src='../images/Assinaturas/IsabelaCC.jpg' style='height: 50px; width: 200px' /></p>");
                //sb.Append("TESTEMUNHA 1:_________________________________________________________________<br/>");
                //sb.Append("<p style='text-align: center'>Isabela Carvalho Campos Sá</p>");
                //sb.Append("<br/>");
                //sb.Append("<p style='text-align: center'><img src='../images/Assinaturas/SaraLopes.jpg' style='height: 50px; width: 200px' /></p>");
                //sb.Append("TESTEMUNHA 2:_________________________________________________________________<br/>");
                //sb.Append("<p style='text-align: center'>Sara Lopes Fonseca</p>");        

                sb.Append("</FONT>");
                lit_Texto_Contrato.Text = sb.ToString();
                lit_Texto_Contrato.DataBind();
            }
        }

        private string InsereEspaco(int qnt)
        {
            var space = "";
            for (int i = 0; i < qnt; i++)
                space += "&nbsp;";
            return space;
        }

        private void GeraTermoCompromisso(int aprendiz)
        {
            var sb = new StringBuilder();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.View_Contrato_Apredizs.Where(p => p.Apr_Codigo == aprendiz).First();
                sb.Append("<img alt='' src='../Images/logo_fundacao_bw.png' />");
                sb.Append("<br/><h6>Termo de Compromisso Adolescente Aprendiz</h6><br>");
                sb.Append("<div class='texto_bloco'><p>Este termo visa firmar o compromisso do aprendiz " + dados.Apr_Nome + " e de seu responsável " + dados.Apr_Responsavel + ", com o programa Educação e Trabalho – " +
                          "Aprendizagem Profissional, em conformidade com a Lei do Aprendiz 10.097/00. Sendo assim, fica então estabelecido que:</p></div)");
                sb.Append("<div class='texto_bloco'><h1> 1-Aprendiz: </h1> ");
                sb.Append("<p> <ul class='list'>  " +
                          "<li>Em caso de falta na Aprendizagem prática, o Orientador e a Fundação CDL deverão ser informados no mesmo dia, comunicando o motivo da ausência do adolescente Aprendiz. O atestado que justificará a falta deverá ser entregue ao orientador e a Fundação CDL no prazo máximo de dois dias úteis.</li>" +
                          "<li>O Adolescente Aprendiz deverá estar freqüente no mínimo em 75% (setenta e cinco por cento) da aprendizagem teórica durante a semana das " + string.Format("{0:HH:mm}", dados.ALAInicioExpediente) + " horas ás " + string.Format("{0:HH:mm}", dados.ALATerminoExpediente) + " horas previamente agendados pela fundação CDL Pró-criança .Caso precise se ausentar,deverá apresentar o documento justificando sua falta no prazo máximo de 2 dias úteis á fundação CDL </li>" +
                          "<li>Em ambos os casos citados anteriormente, sem o atestado, a falta não será abonada e o Adolescente Aprendiz não receberá a renumeração e os benefícios referentes ao dia de aprendizagem prática e/ou teórica.</li>" +
                          "<li>Em casos de faltas injustificadas na aprendizagem prática e teórica, o aprendiz receberá advertência, podendo ser desligado do programa em sua reincidência.</li>" +
                          "<li>Em caso de faltas consecutivas sem justificativa legal (trinta dias), o adolescente será desligado do programa por abandono de emprego.</li>" +
                          "<li>O adolescente aprendiz se compromete a comparecer em reuniões e demais atividades sempre que agendadas pela fundação CDL;</li>" +
                          "<li>O Adolescente aprendiz se compromete a freqüentar as aulas escolares, zelando pela sua aprovação escolar e pelo seu bom comportamento.</li>" +
                          "<li>Em casos de atrasos na aprendizagem prática e /ou teórica, o aprendiz receberá advertência e na sua reincidência sofrerá penalidades mais severas.</li>" +
                          "<li>A utilização do uniforme no ambiente da atividade prática e /ou teórica é obrigatória, a sua falta em alguma das ocasiões, acarretará em advertência.</li>" +
                          "<li>O adolescente aprendiz está ciente de que ao acumular três advertências levará uma suspensão. Após a suspensão, o próximo ato que contrarie as normas deste termo acarretará em desligamento do programa.</li>" +
                          "<li>O Adolescente aprendiz afirma estar em perfeitas condições de saúde e sem suspeita de gravidez.</li>" +
                          "<li>O adolescente aprendiz está ciente que na presença de descendentes deverá comunicar a Fundação CDL para que providências quanto à bolsa família seja tomada.</li>" +
                          "<li>O aprendiz juntamente com seu responsável legal (caso o Aprendiz seja menor de 18 anos),autoriza(m) a utilização,por quaisquer meios,do seu nome,da sua imagem e da sua voz para fins de pesquisa,seja para divulgação em qualquer meio de comunicação ,por tempo inderteminado sem acarretar nenhum ônus , seja financeiro ou material ,para ambas as partes.</li>" +
                          "</ul></p></div><br/>");
                sb.Append("<div class='texto_bloco'><h1>2-Responsável Legal:</h1> ");
                sb.Append("<p> O responsável pelo Adolescente Aprendiz fica ciente que é de sua responsabilidade a formação cidadã jovem, ficando incumbido de encaminhar e acompanhar no que dia respeito ao comparecimento no Programa Educação e Trabalho como um todo, zelando por sua pontualidade e desenvolvimento. " +
                           "O responsável pelo adolescente aprendiz fica ciente que é de sua co-responsabilidade zelar pelo seu bom desempenho e freqüência na escola de Ensino Regular. </p>");
                sb.Append("<p> O responsável pelo Adolescente Aprendiz, declara sob as penas da lei, que todos os dados e informações contidas no documento comprobatório de renda são a expressão da verdade e que tem ciência que pode sofrer penalidades por prática de fraude;</p>  ");
                sb.Append("<p> O responsável legal pelo adolescente aprendiz está ciente que deverá comparecer a Fundação CDL sempre que solicitado.</p>  ");
                sb.Append("<p>É de ciência de todos que o não cumprimento dos termos acima, pode acarretar o desligamento do Adolescente Aprendiz do referido Programa.</p> <br/>  ");
                sb.Append("<p> Sem mais, estando todos cientes e de comum acordo, subscreve-se:</p> </div> ");
                sb.Append("<div class='texto_bloco' style='text-align: left;'><p><br/><br/> ________________________________________<br/>" +
                          "Adolescente Aprendiz <br/>RG: <br/><br/><br/> ________________________________________<br/>Responsável <br/>RG: <br/><br/><br/> " +
                          "________________________________________<br/> Fundação CDL Pró-Criança </p><br/></div> ");
                sb.Append("<div class='texto_bloco' ><p style='text-align: right;'> Belo Horizonte, " + string.Format("{0:D}", DateTime.Now) + "</p></div>");
                lit_Texto_Contrato.Text = sb.ToString();
                lit_Texto_Contrato.DataBind();
            }
        }

        private void GeraRecebimentoUniforme(int aprendiz)
        {
            var sb = new StringBuilder();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var dados = bd.View_Contrato_Apredizs.Where(p => p.Apr_Codigo == aprendiz).First();
                sb.Append("<img alt='' src='../Images/pet.jpg' height=80px' width='100px' />");
                sb.Append("<br/><h6>Declaração de Recebimento de Uniforme</h6><br><br/><br/><br/>");
                sb.Append("<div class='texto_bloco'><p>O adolescente, " + dados.Apr_Nome + " confirma o recebimento de " + Session["prmt_num_uniforme"] + " (" + Funcoes.NumeroPorExtenso(int.Parse(Session["prmt_num_uniforme"].ToString())) + ")  uniformes e compromete-se " +
                          "em usá-los diariamente no momento de sua atividade prática e teórica, podendo este ser substituído a cada 06 meses de aprendizagem. " +
                          "A não utilização do uniforme pode acarretar desligamento do programa</p></div)" +
                          "<br/><br/><br/><br/><br/><br/><br/><br/>");
                sb.Append("<div class='texto_bloco' ><p style='text-align: left;'> Belo Horizonte, " + string.Format("{0:dd/MM/yyyy}", DateTime.Now) + "</p></div>");
                sb.Append("<div class='assinaturas' ><p><br/><br/> ________________________________________<br/>" +
                          "Assinatura <br/><br/><br/><br/> ________________________________________<br/>Assinatura Responsável<br/></p></div>");

                lit_Texto_Contrato.Text = sb.ToString();
                lit_Texto_Contrato.DataBind();
            }
        }


        private void GeraValeTransporte(int aprendiz)
        {
            var sb = new StringBuilder();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var dados = bd.View_Contrato_Apredizs.Where(p => p.Apr_Codigo == aprendiz).First();
                var itinerarios = bd.CA_Vale_Transportes.Where(p => p.VtpAprendiz == aprendiz).ToList();

                sb.Append("<br/><h6 style='text-align: left;'>Declaração e Termo de Compromisso para Aquisição de Vale-transporte</h6><br><br/><br/><br/>");
                sb.Append("<div class='texto_bloco'><p>Eu, " + dados.Apr_Nome + " residente no endereco  " + dados.Apr_Endereço + ", nº" + dados.Apr_NumeroEndereco + " - " + dados.Apr_Complemento + ", Bairro " + dados.Apr_Bairro + ", " + "na cidade de " + dados.Apr_Cidade + "/" + dados.Apr_UF + ", necessito " +
                          "diariamente de   " + (itinerarios.Where(p => p.VtpItinerario == 1 || p.VtpItinerario == 2).Sum(p => p.VtpQuantidade)) + " vales-transporte para meu deslocamento residência-trabalho-residência e " +
                (itinerarios.Where(p => p.VtpItinerario == 3 || p.VtpItinerario == 4).Sum(p => p.VtpQuantidade)) + " vales-transporte para meu deslocamento residência-curso-residência conforme: <p><br/>");
                foreach (var vtpItinerario in itinerarios.Select(p => p.VtpItinerario).Distinct())
                {
                    var nome = bd.CA_Itinerarios.Where(p => p.ItnCodigo == vtpItinerario).First().ItnNome;
                    sb.Append(" <h1 style='margin-left: 40px'> " + nome + " </h1> ");
                    int cont = 1;
                    foreach (var item in itinerarios.Where(p => p.VtpItinerario == vtpItinerario).ToList())
                    {
                        sb.Append("<p> " + cont + ") Quantidade: " + item.VtpQuantidade + ".  Tarifa: " + string.Format("{0:c}", item.VtpTarifa) + "  </p>");
                        cont++;
                    }
                }

                sb.Append("<br/><br/><br/><p>Assumo o compromisso de utilizar o vale transporte exclusivamente para o deslocamento descrito, e afirmo ter conhecimento do artigo 7º," +
                          "do parágrafo 3º, do Decreto º 95.247, de 17/11/1987, de que constitui falta grave o uso indevido do transporte ou a falsidade desta " +
                          "declaração, estando sujeito a ser desligado do programa. </p>");

                sb.Append("<p>Comprometo -me a atualizar anualmente as informações ou a qualquer tempo quando ocorrer mudança residencial ou no(s) meio(s) de transporte. </p>");

                sb.Append("<div class='texto_bloco' ><p style='text-align: left;'> Belo Horizonte, " + string.Format("{0:D}", DateTime.Now) + "</p></div>");
                sb.Append("<div class='assinaturas' ><p><br/><br/> ________________________________________<br/>" +
                          "Assinatura <br/><br/><br/><br/> ________________________________________<br/>Assinatura Responsável<br/></p></div>");

                lit_Texto_Contrato.Text = sb.ToString();
                lit_Texto_Contrato.DataBind();
            }
        }


        private void GeraValeTransporteFicha(int aprendiz)
        {
            var sb = new StringBuilder();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var dados = bd.View_Contrato_Apredizs.Where(p => p.Apr_Codigo == aprendiz).First();
                sb.Append("<br/><h6 style='text-align: left;'>Declaração e Termo de Compromisso para Aquisição de Vale-transporte</h6><br><br/>");
                sb.Append("<div class='texto_bloco'><p>Eu, " + dados.Apr_Nome + " residente no endereco  " + dados.Apr_Endereço + ", nº" + dados.Apr_NumeroEndereco + " - " + dados.Apr_Complemento + ", Bairro " + dados.Apr_Bairro + ", " + "na cidade de " + dados.Apr_Cidade + "/" + dados.Apr_UF + ", necessito " +
                          "diariamente de ................ vales-transporte para meu deslocamento residência-trabalho-residência e ............... vales-transporte para meu deslocamento residência-curso-residência conforme: <p><br/>");

                sb.Append(" <h1 style='margin-left: 40px'> Itinerário Casa/Trabalho </h1> ");
                sb.Append("<p> 1) Quantidade: .................  Tarifa: ................ </p>");
                sb.Append("<p> 2) Quantidade: .................  Tarifa: ................ </p>");
                sb.Append("<p> 3) Quantidade: .................  Tarifa: ................ </p>");

                sb.Append(" <h1 style='margin-left: 40px'> Itinerário Trabalho/Casa </h1> ");
                sb.Append("<p> 1) Quantidade: .................  Tarifa: ................ </p>");
                sb.Append("<p> 2) Quantidade: .................  Tarifa: ................ </p>");
                sb.Append("<p> 3) Quantidade: .................  Tarifa: ................ </p>");

                sb.Append(" <h1 style='margin-left: 40px'> Itinerário Casa/Curso(Fundação CDL) </h1> ");
                sb.Append("<p> 1) Quantidade: .................  Tarifa: ................ </p>");
                sb.Append("<p> 2) Quantidade: .................  Tarifa: ................ </p>");
                sb.Append("<p> 3) Quantidade: .................  Tarifa: ................ </p>");

                sb.Append(" <h1 style='margin-left: 40px'> Itinerário Curso(Fundação CDL)/Casa </h1> ");
                sb.Append("<p> 1) Quantidade: .................  Tarifa: ................ </p>");
                sb.Append("<p> 2) Quantidade: .................  Tarifa: ................ </p>");
                sb.Append("<p> 3) Quantidade: .................  Tarifa: ................ </p>");


                sb.Append("<br/><p>Assumo o compromisso de utilizar o vale transporte exclusivamente para o deslocamento descrito, e afirmo ter conhecimento do artigo 7º," +
                          "do parágrafo 3º, do Decreto º 95.247, de 17/11/1987, de que constitui falta grave o uso indevido do transporte ou a falsidade desta " +
                          "declaração, estando sujeito a ser desligado do programa. </p>");

                sb.Append("<p>Comprometo -me a atualizar anualmente as informações ou a qualquer tempo quando ocorrer mudança residencial ou no(s) meio(s) de transporte. </p>");

                sb.Append("<div class='texto_bloco' ><p style='text-align: left;'> Belo Horizonte, " + string.Format("{0:D}", DateTime.Now) + "</p></div>");
                sb.Append("<div class='assinaturas' ><p><br/><br/> ________________________________________<br/>" +
                          "Assinatura <br/><br/><br/><br/> ________________________________________<br/>Assinatura Responsável<br/></p></div>");

                lit_Texto_Contrato.Text = sb.ToString();
                lit_Texto_Contrato.DataBind();
            }
        }


        /// <summary>
        /// Andreghorst
        /// Cliente solicitou o salvamento de pdf, porém poderá ser salvo em html caso
        /// ele nao queira usar o pdf da impressora..
        /// </summary>
        void SalvarHtml()
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string sHtml = "";
            HttpWebRequest request;
            HttpWebResponse response = null;
            Stream stream = null;
            request = (HttpWebRequest)WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.Default);
            sHtml = sr.ReadToEnd();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string getpath = path + @"\" + Arquivo + ".html";
            File.WriteAllText(getpath, sHtml, Encoding.UTF8);
            if (stream != null) stream.Close();
            if (response != null) response.Close();
        }



    }
}