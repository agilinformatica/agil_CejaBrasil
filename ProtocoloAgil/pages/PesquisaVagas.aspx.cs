using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;
using System.Net;
using ProtocoloAgil.Base.Models;
using System.Text;

namespace ProtocoloAgil.pages
{
    public partial class PesquisaVagas : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;

            if (HFSelectedRadio.Value != string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "LoadInput()", true);
            }

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                HFSelectedRadio.Value = "tab-0";
                CarregaAreaAtuacao();
                CarregaEmpresa();
                //PreencheDropDownTurma();
            }
        }


        private void CarregaEmpresa()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from P in db.CA_ParceirosUnidades
                             //where situacoes.Contains(S.StaCodigo)
                             select new { P.ParUniCodigo, P.ParUniDescricao }).ToList().OrderBy(item => item.ParUniDescricao);

                DDEmpresa.DataSource = query;
                DDEmpresa.DataBind();

                DDEmpresaEditar.DataSource = query;
                DDEmpresaEditar.DataBind();
            }
        }
        private void CarregaAreaAtuacao()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from A in db.CA_AreaAtuacaos
                             //where situacoes.Contains(S.StaCodigo)
                             select new { A.AreaCodigo, A.AreaDescricao }).ToList().OrderBy(item => item.AreaDescricao);

                DDAreaAtuacao.DataSource = query;
                DDAreaAtuacao.DataBind();

                DDAreaAtuacaoEditar.DataSource = query;
                DDAreaAtuacaoEditar.DataBind();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

       
        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void bt_lista_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (dd_mes_pesquisa.SelectedValue.Equals(string.Empty) && tb_ano_pesquisa.Text.Equals(string.Empty) && HFSelectedRadio.Value != "tab-0")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            //        "alert('Não é possivel imprimir esta solicitação. Informe mês e ano de referência.')", true);
            //    return;
            //}

            Session["id"] = 43;
           // Session["PRMT_Nome"] = pesquisa.Text.Equals(string.Empty) ? "%" : pesquisa.Text;
           // Session["PRMT_MesRef"] = dd_mes_pesquisa.SelectedValue;
            //Session["PRMT_AnoRef"] = tb_ano_pesquisa.Text;
            //Session["PRMT_Turma"] = DDturma_pesquisa.Text;
            Session["PRMT_Tipo"] = HFSelectedRadio.Value;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView();
            MultiView1.ActiveViewIndex = 0;
        }

        private void BindGridView()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var selected = from V in bd.CA_RequisicoesVagas
                               join A in bd.CA_AreaAtuacaos on V.ReqAreaAtuacao equals A.AreaCodigo
                               join E in bd.CA_ParceirosUnidades on V.ReqEmpresa equals E.ParUniCodigo
                               where V.ReqSituacao.Equals("A")
                               select new { A.AreaDescricao, E.ParUniDescricao, V.ReqDataSolitação, V.ReqAtividades, V.ReqId, V.ReqSexo, V };
                

                if (!DDAreaAtuacao.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.V.ReqAreaAtuacao.Equals(DDAreaAtuacao.SelectedValue));
                }

                if (!DDEmpresa.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.V.ReqEmpresa.Equals(DDEmpresa.SelectedValue));
                }

                if (!txtDataSolicitacao.Text.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.V.ReqDataSolitação.Equals(Convert.ToDateTime(txtDataSolicitacao.Text)));
                }

                if (!DDSexo.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.V.ReqSexo.Equals(DDSexo.SelectedValue));
                }



                if (!DDSituacaoVagaPesquisa.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.V.ReqSituacao.Equals(DDSituacaoVagaPesquisa.SelectedValue));
                }


                //lista = selected;
                GridView3.DataSource = selected.ToList();
                GridView3.DataBind();

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDSituacao.SelectedValue = "A";
            var gvr = ((GridView)sender).SelectedRow;
            Session["comando"] = "Alterar";
            CarregaAreaAtuacao();
            CarregaEmpresa();
           // HFEscolaRef.Value = WebUtility.HtmlDecode(gvr.Cells[0].Text);
            PreencheCampos(WebUtility.HtmlDecode(gvr.Cells[0].Text));
            MultiView1.ActiveViewIndex = 2;
            Session["Comando"] = "Alterar";
        }

        private void PreencheCampos(string codigo)
        {
            // ----------------------------------preenche campos ------------------------------------>
            using (var repository = new Repository<RequisicoesVagas>(new Context<RequisicoesVagas>()))
            {
                var vaga = repository.Find(Convert.ToInt32(codigo));

                txtCodigoVaga.Text = vaga.ReqId.ToString();
                DDEmpresaEditar.SelectedValue = vaga.ReqEmpresa.ToString();
                ShowAdressEmpresa(int.Parse(DDEmpresaEditar.SelectedValue));
                txtDataSolicitacaoEditar.Text = string.Format("{0:dd/MM/yyyy}", vaga.ReqDataSolitação);
                txtQuantidade.Text = vaga.ReqQuantidade.ToString();
                DDSexoEditar.SelectedValue = vaga.ReqSexo;
                txtHorarioEntrevista.Text = vaga.ReqHorarioEntrevista;
                DDSubstituicao.SelectedValue = vaga.ReqSubstituicao;
                txtCaracteristicaPessoal.Text = vaga.ReqCaracteristicasPessoais;
                DDAreaAtuacaoEditar.SelectedValue = vaga.ReqAreaAtuacao.ToString();
                txtHabilidades.Text = vaga.ReqHabilidades;
                txtAtividades.Text = vaga.ReqAtividades;
                txtCartaoEntrevista.Text = vaga.ReqContaoEntrevista;
                txtObservacao.Text = vaga.ReqObservacoes;
                txtObservacaoInst.Text = vaga.ReqObservacoesInst;
                txtBenefício.Text = vaga.ReqObservacoes;
                txtSalario.Text = vaga.ReqSalario == null || vaga.ReqSalario.Equals(string.Empty) ? "" : vaga.ReqSalario.ToString();
                txtHorarioTrabalho.Text = vaga.ReqHorarioTrabalho == null || vaga.ReqHorarioTrabalho.Equals(string.Empty) ? "" : vaga.ReqHorarioTrabalho.ToString();

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var grid = (GridView)sender;
            grid.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }




        protected void BTsalva_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (DDAreaAtuacaoEditar.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Área de Atuação obrigatória");
                if (DDEmpresaEditar.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Empresa Obrigatório.");
                
                using (var repository = new Repository<RequisicoesVagas>(new Context<RequisicoesVagas>()))
                {
                    var vaga = new RequisicoesVagas();

                    // vaga.ReqId = 1;
                    vaga.ReqId = txtCodigoVaga.Text.Equals(string.Empty) ? new int() : int.Parse(txtCodigoVaga.Text);
                    vaga.ReqEmpresa = int.Parse(DDEmpresaEditar.SelectedValue);
                    vaga.ReqDataSolitação = txtDataSolicitacaoEditar.Text == string.Empty ? new DateTime() : Convert.ToDateTime(txtDataSolicitacaoEditar.Text);
                    vaga.ReqQuantidade = txtQuantidade.Text == string.Empty ? new short() : short.Parse(txtQuantidade.Text);
                    vaga.ReqSexo = DDSexoEditar.SelectedValue;
                    vaga.ReqHorarioEntrevista = txtHorarioEntrevista.Text;
                    vaga.ReqSubstituicao = DDSubstituicao.SelectedValue;
                    vaga.ReqCaracteristicasPessoais = txtCaracteristicaPessoal.Text;
                    vaga.ReqAreaAtuacao = DDAreaAtuacaoEditar.SelectedValue == string.Empty ? new int() : int.Parse(DDAreaAtuacaoEditar.SelectedValue);
                    vaga.ReqHabilidades = txtHabilidades.Text;
                    vaga.ReqAtividades = txtAtividades.Text;
                    vaga.ReqContaoEntrevista = txtCartaoEntrevista.Text;
                    vaga.ReqObservacoes = txtObservacao.Text;
                   
                    vaga.ReqObservacoesInst = txtObservacaoInst.Text;

                    vaga.ReqBeneficios = txtBenefício.Text;
                    vaga.ReqSalario = txtSalario.Text == null || txtSalario.Text.Equals(string.Empty) ? new Single() : Convert.ToSingle(txtSalario.Text);
                    vaga.ReqHorarioTrabalho = txtHorarioTrabalho.Text;

                    vaga.ReqSubstituir = txtSubstituicao.Text;

                    if (txtCodigoVaga.Text.Equals(string.Empty))
                    {
                        vaga.ReqSituacao = "A";
                        repository.Add(vaga);
                        //var sql = "insert into CA_RequisicoesVagas values (" + vaga.ReqEmpresa + ", '" + vaga.ReqDataSolitação + "', " + vaga.ReqQuantidade + ", '" + vaga.ReqSexo + "', '" + vaga.ReqHorarioEntrevista + "', '" + vaga.ReqSubstituicao + "', '"+vaga.ReqCaracteristicasPessoais+"', "+vaga.ReqAreaAtuacao+", '"+vaga.ReqHabilidades+"', '"+vaga.ReqAtividades+"', '"+vaga.ReqContaoEntrevista+"', '"+vaga.ReqObservacoes+"', '"+vaga.ReqSituacao+"', '"+vaga.ReqObservacoesInst+"')";
                        //var con = new Conexao();
                        //con.Alterar(sql);
                    }
                    else
                    {
                        vaga.ReqSituacao = DDSituacao.SelectedValue;
                        repository.Edit(vaga);
                    }
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação realizada com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                // Funcoes.TrataExcessao("000200", ex);
            }
        }
        protected void BTLimpa_Click(object sender, EventArgs e)
        {
            Limpadados();
        }

        private void Limpadados()
        {
            txtAtividades.Text = "";
            txtCaracteristicaPessoal.Text = "";
            txtCartaoEntrevista.Text = "";
            txtCodigoVaga.Text = "";
            txtDataSolicitacao.Text = "";
            txtHabilidades.Text = "";
            txtHorarioEntrevista.Text = "";
            txtObservacao.Text = "";
            txtObservacaoInst.Text = "";
            txtQuantidade.Text = "";
            DDAreaAtuacao.SelectedValue = "";
            DDEmpresa.SelectedValue = "";
            DDSexo.SelectedValue = "";
            txtSalario.Text = "";
            txtBenefício.Text = "";
            txtHorarioTrabalho.Text = "";
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {

           


            if (e.CommandName.Equals("Encaminhamentos"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];
                var codVaga = row.Cells[0].Text.ToString();
                CarregaEncaminhamentos(int.Parse(codVaga));
                MultiView1.ActiveViewIndex = 3;
                PreencheCamposEncaminhamento(codVaga);
            }


            if (e.CommandName.Equals("Imprimir"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];
                int numRequerimento = int.Parse(row.Cells[0].Text);
                Session["codRequerimento"] = (int)numRequerimento;
                Session["id"] = 103;
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void CarregaEncaminhamentos(int codVaga)
        {
       
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from E in db.CA_Encaminhamentos
                             join V in db.CA_RequisicoesVagas on E.Enc_Requisicao equals V.ReqId
                             join S in db.CA_StatusEncaminhamentos on E.Enc_Status equals S.Ste_Codigo
                             where V.ReqId == codVaga
                             select new { Nome = E.CA_Aprendiz.Apr_Nome, CodAprendiz = E.CA_Aprendiz.Apr_Codigo, Telefone = Funcoes.FormataTelefoneSaoPaulo(E.CA_Aprendiz.Apr_Celular), Email = E.CA_Aprendiz.Apr_Email, SituacaoJovem = S.Ste_Descricao }).ToList().OrderBy(item => item.Nome);

                GridEncaminhamentos.DataSource = query.ToList();
                GridEncaminhamentos.DataBind();

            }
        }


        private void PreencheCamposEncaminhamento(string codigo)
        {
            var sql = "";
            var con = new Conexao();
            
            // ----------------------------------preenche campos ------------------------------------>
            using (var repository = new Repository<RequisicoesVagas>(new Context<RequisicoesVagas>()))
            {
                var vaga = repository.Find(Convert.ToInt32(codigo));

                lblCodigoVaga.Text = vaga.ReqId.ToString();

                sql = "Select ParUniDescricao from CA_ParceirosUnidade where  ParUniCodigo = "+vaga.ReqEmpresa.ToString()+"";
                var result = con.Consultar(sql);
                while (result.Read())
                {
                    lblEmpresa.Text = result["ParUniDescricao"].ToString();
                    break;
                }

                lblDataSolicitacao.Text = string.Format("{0:dd/MM/yyyy}", vaga.ReqDataSolitação);
                lblQuantidade.Text = vaga.ReqQuantidade.ToString();
                lblSexo.Text = vaga.ReqSexo.Equals("M") ? "Masculino" : vaga.ReqSexo.Equals("F") ? "Feminino" : "";
                lblHorarioEntrevista.Text = vaga.ReqHorarioEntrevista;
                lblSubstituicao.Text = vaga.ReqSubstituicao;
                lblCaracteriscasPessoais.Text = vaga.ReqCaracteristicasPessoais;

                sql = "select AreaDescricao from CA_AreaAtuacao where AreaCodigo = " + vaga.ReqAreaAtuacao.ToString() + "";
                con = new Conexao();
                var result2 = con.Consultar(sql);
                while(result2.Read()){
                    lblAreaAtuacao.Text = result2["AreaDescricao"].ToString();
                    break;
                }
                //DDAreaAtuacaoEditar.SelectedValue = vaga.ReqAreaAtuacao.ToString();



                lblHabilidades.Text = vaga.ReqHabilidades;
                lblAtividade.Text = vaga.ReqAtividades;
                lblContatoEntrevista.Text = vaga.ReqContaoEntrevista;
                lblObservacao.Text = vaga.ReqObservacoes;
                lblObservaoInst.Text = vaga.ReqObservacoesInst;
                lblBeneficio.Text = vaga.ReqObservacoes;
                lblSalario.Text = vaga.ReqSalario == null || vaga.ReqSalario.Equals(string.Empty) ? "" : vaga.ReqSalario.ToString();
                lblHorarioTrabalho.Text = vaga.ReqHorarioTrabalho == null || vaga.ReqHorarioTrabalho.Equals(string.Empty) ? "" : vaga.ReqHorarioTrabalho.ToString();
                lblSituacao.Text = vaga.ReqSituacao.Equals("A") ? "Ativo" : vaga.ReqSituacao.Equals("E") ? "Encerrado" : "";
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void DDEmpresaEditar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAdressEmpresa(int.Parse(DDEmpresaEditar.SelectedValue));
        }

        private void ShowAdressEmpresa(int? unidade)
        {
            if (unidade == 0 || unidade == null) return;

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var escola = bd.CA_ParceirosUnidades.Where(p => p.ParUniCodigo == unidade);
                if (escola.Count() == 0)
                {
                    lblEnderecoEmpresa.Text = "Não Encontrado.";
                    return;
                }

                var dados = escola.First();

                var sb = new StringBuilder();
                sb.Append(dados.ParUniEndereco);
                if (dados.ParUniNumeroEndereco != null)
                {
                    sb.Append(",nº " + dados.ParUniNumeroEndereco);
                }
                sb.Append(dados.ParUniComplemento);
                sb.Append(", Bairro: " + (dados.ParUniBairro ?? "Não Informado"));
                sb.Append(", " + dados.ParUniCidade + " / " + dados.ParUniEstado);
                sb.Append(". CEP: " + (dados.ParUniCEP == null ? "Não Informado" : Funcoes.FormataCep(dados.ParUniCEP)));
                sb.Append(". Telefone: " + ((dados.ParUniTelefone == null) || (dados.ParUniTelefone.Length < 8) ? "Não Informado" : Funcoes.FormataTelefone(dados.ParUniTelefone)));
                sb.Append(". CNJP: " + ((dados.ParUniCNPJ == null) ? "Não Informado" : Funcoes.FormataCNPJ(dados.ParUniCNPJ)));

                lblEnderecoEmpresa.Text = sb.ToString();
            }
        }
      

    }
}