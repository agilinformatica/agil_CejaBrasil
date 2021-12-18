using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class RealocarAprendizes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "aprendiz";
            Page.Form.DefaultButton = btn_pesquisa.UniqueID;
            if (!IsPostBack)
            {
                BindCursos(DDcurso_origem);
                BindCursos(DDcurso_destino);
                MultiView1.ActiveViewIndex = 0;
            }
        }

        protected void DDcursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            BindTurmas(drop.SelectedValue, DDturma_origem);
        }


        protected void DDcursos_destino_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            BindTurmas(drop.SelectedValue, DDturma_destino);
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;
            drop.Items.Insert(0, new ListItem("Selecione", ""));
        }


        protected void BindTurmas(string curso, DropDownList dropdown)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var list = new List<CA_Turma>();
                list.AddRange(bd.CA_Turmas.Where(x => x.TurCurso.Equals(curso)).OrderBy(p => p.TurNome));
                dropdown.Items.Clear();
                dropdown.DataSource = list.ToList();
                dropdown.DataBind();
            }
        }

        protected void BindCursos(DropDownList drop)
        {
            using (var reposytory = new Repository<Curso>(new Context<Curso>()))
            {
                var list = new List<Curso>();
                list.AddRange(reposytory.All().OrderBy(p => p.CurDescricao));
                DDturma_origem.Items.Clear();
                DDturma_destino.Items.Clear();

                drop.DataSource = list.ToList();
                drop.DataBind();
            }
        }

        protected void btn_pesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDcurso_origem.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um curso de origem.");
                if (DDturma_origem.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma turma de origem.");
                if (DDcurso_destino.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um curso de destino.");
                if (DDturma_destino.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma turma de destino.");
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var apendizes = from i in bd.CA_AlocacaoAprendizs
                                    join m in bd.CA_Aprendizs on i.ALAAprendiz equals m.Apr_Codigo
                                    where i.ALATurma == int.Parse(DDturma_origem.SelectedValue) &&
                                    i.ALAStatus == "A" && m.Apr_Situacao == 6
                                    select new { Codigo = m.Apr_Codigo, Nome = m.Apr_Nome };
                    var dados = apendizes.Distinct().OrderBy(p => p.Nome);

                    lb_Turma.Items.Clear();
                    foreach (var item in dados)
                    {
                        lb_Turma.Items.Add(new ListItem(item.Nome, item.Codigo.ToString()));
                    }
                }
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                      "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {

                Funcoes.TrataExcessao("000850", ex);
            }
        }

        protected void btn_incluir_Click(object sender, EventArgs e)
        {
            var indices = lb_Turma.GetSelectedIndices();
            var itens = indices.Select(index => lb_Turma.Items[index]).ToList();

            foreach (var listItem in itens)
            {
                if (lb_participantes.Items.Contains(listItem)) continue;
                lb_participantes.Items.Add(listItem);
            }

            foreach (var listItem in itens)
            {
                lb_Turma.Items.Remove(listItem);
            }
        }

        protected void btn_retirar_Click(object sender, EventArgs e)
        {
            var indices = lb_participantes.GetSelectedIndices();
            var itens = indices.Select(index => lb_participantes.Items[index]).ToList();

            foreach (var listItem in itens)
            {
                if (lb_Turma.Items.Contains(listItem)) continue;
                lb_Turma.Items.Add(listItem);
            }
            foreach (var listItem in itens)
            {
                lb_participantes.Items.Remove(listItem);
            }
        }


        protected void Bt_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lb_participantes.Items.Count == 0) throw new ArgumentException("Adicione os aprendizes participantes do evento.");
                if (TBDataInicio.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de inicio da nova alocação.");
                if (TBDataFinal.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de término da nova alocação.");

                var naoalocados = new List<ListItem>();
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var participantes = lb_participantes.Items.Cast<ListItem>().ToList();
                    foreach (var aprendiz in participantes)
                    {
                        // encontra a alocação do aprendiz
                        var dados = from i in bd.CA_AlocacaoAprendizs where i.ALATurma == int.Parse(DDturma_origem.SelectedValue) &&
                                                i.ALAStatus == "A" && i.ALAAprendiz == int.Parse(aprendiz.Value) select i;

                        //caso não haja alocação ativa, adiciona na lista de não alocados e pula para o próximo
                        if (dados.Count() == 0)
                        {
                            naoalocados.Add(aprendiz);
                            continue;
                        }

                        //cria uma alocacao a partir da última alocação ativa para a turma destino
                        var alocacao = dados.First();
                        var participante = new CA_AlocacaoAprendiz { ALAAprendiz = alocacao.ALAAprendiz, ALATurma = int.Parse(DDturma_destino.SelectedValue),
                                            ALAUnidadeParceiro = alocacao.ALAUnidadeParceiro, ALAStatus = "A", ALATutor = alocacao.ALATutor,
                                            ALADataInicio = DateTime.Parse(TBDataInicio.Text), ALADataPrevTermino = DateTime.Parse(TBDataFinal.Text),
                                            ALAInicioExpediente = alocacao.ALAInicioExpediente, ALATerminoExpediente = alocacao.ALATerminoExpediente, 
                                            ALAValorBolsa = alocacao.ALAValorBolsa, ALAValorTaxa = alocacao.ALAValorTaxa, ALApagto = alocacao.ALApagto, 
                                            ALAOrientador = alocacao.ALAOrientador, ALAMotivoDesligamento = alocacao.ALAMotivoDesligamento, 
                                            ALAUsuarioCadastro = Session["codigo"].ToString(), ALAUsuarioDataCadastro = DateTime.Today

                                            //ALADataTermino = alocacao.ALADataTermino
                                            // ALAObservacao - nao preenchido
                                            //ALAOrdem - auto numeracao
                                            // ALAUsuarioAlteracao - nao preenchido
                                            //ALAUsuarioDataAlteracao -  nao preenchido
                        };

                        //adiciona no banco e muda a ultima alocação ativa para inativa
                        bd.CA_AlocacaoAprendizs.InsertOnSubmit(participante);
                        alocacao.ALAStatus = "I";

                        //aplica as mudanças
                        bd.SubmitChanges();
                    }
                }

                lb_participantes.Items.Clear();
                string alert = naoalocados.Count >0 ? naoalocados.Aggregate("Não foi possível realocar os aprendizes : ", (current, item) => current + (" " + item.Text + ". ")) 
                    : "Aprendizes realocados com sucesso!";


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('" + alert  + "')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                      "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000860", ex);
            }
        }


        protected void btn_print_Click(object sender, EventArgs e)
        {
            Session["id"] = "74";
            Session["prmnt_evento"] = HFEvento.Value;
            MultiView1.ActiveViewIndex = 3;
        }

        protected void btn_texto_Click(object sender, EventArgs e)
        {
            var filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            try
            {
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var dados = from i in bd.CA_Aprendizs
                                join m in bd.CA_Participantes on i.Apr_Codigo equals m.PrtAprendiz
                                join p in bd.CA_Eventos on m.PrtCodigoEvento equals p.EvnCodigo
                                where m.PrtCodigoEvento == int.Parse(HFEvento.Value)
                                select new { p.EvnNome, p.EvnData, i.Apr_Codigo, i.Apr_Nome, m.PrtPresenca };

                    foreach (var dado in dados.OrderBy(p => p.Apr_Nome))
                    {
                        var linha = dado.EvnNome + " ;" + dado.EvnData + " ;" + dado.Apr_Codigo + " ;" + dado.Apr_Nome + " ;" + (dado.PrtPresenca.Equals("S")? "Sim" : "Não");
                        write.Escreve(linha);
                    }

                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Aprendizes por Evento.txt");
                }
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000852", ex);
            }
        }


        protected void IMBsalvar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var bt = (ImageButton)sender;
                var evento = int.Parse(bt.CommandArgument);
                var row = (GridViewRow)bt.Parent.Parent;
                var drop = (DropDownList)row.Cells[2].FindControl("DDPresenca");

                if (drop.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma opção na coluna \"Presente\".");
                var aprendiz = int.Parse(row.Cells[0].Text);
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var registro = bd.CA_Participantes.Where(p => p.PrtAprendiz == aprendiz && p.PrtCodigoEvento == evento).First();
                    registro.PrtPresenca = drop.SelectedValue;
                    bd.SubmitChanges();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                         "alert('Presença atualizada com sucesso!')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                      "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000853", ex);
            }
        }

    }
}