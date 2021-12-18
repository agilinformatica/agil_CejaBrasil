using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using System.Net;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MenorAprendizWeb.Base;
using System.Text;

namespace ProtocoloAgil.pages
{
    public partial class CadastroVagas : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            if (!IsPostBack)
            {
                //  BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);

                CarregaVagas();

                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTsalva.Enabled = false;
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
            }
        }

        private void CarregaVagas()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from V in db.CA_RequisicoesVagas
                             join A in db.CA_AreaAtuacaos on V.ReqAreaAtuacao equals A.AreaCodigo
                             join E in db.CA_ParceirosUnidades on V.ReqEmpresa equals E.ParUniCodigo
                             // where situacoes.Contains(S.StaCodigo)
                             select new { A.AreaDescricao, E.ParUniDescricao, V.ReqDataSolitação, V.ReqAtividades, V.ReqId, V.ReqSexo, Situacao = V.ReqSituacao.Equals("A") ? "Ativo" : "Encerrado"  }).ToList().OrderBy(item => item.ParUniDescricao);

                GridView1.DataSource = query.ToList();
                GridView1.DataBind();

            }
        }

        private void PreencheCampos(string codigo)
        {
            // ----------------------------------preenche campos ------------------------------------>
            using (var repository = new Repository<RequisicoesVagas>(new Context<RequisicoesVagas>()))
            {
                var vaga = repository.Find(Convert.ToInt32(codigo));

                txtCodigoVaga.Text = vaga.ReqId.ToString();
                DDEmpresa.SelectedValue = vaga.ReqEmpresa.ToString();
                ShowAdressEmpresa(int.Parse(DDEmpresa.SelectedValue));
                txtDataSolicitacao.Text = string.Format("{0:dd/MM/yyyy}", vaga.ReqDataSolitação);

                txtDataEntrevista.Text = string.Format("{0:dd/MM/yyyy}", vaga.ReqDataEntrevista);

                txtQuantidade.Text = vaga.ReqQuantidade.ToString();
                DDSexo.SelectedValue = vaga.ReqSexo;
                txtHorarioEntrevista.Text = vaga.ReqHorarioEntrevista;
                DDSubstituicao.SelectedValue = vaga.ReqSubstituicao;
                txtCaracteristicaPessoal.Text = vaga.ReqCaracteristicasPessoais;
                DDAreaAtuacao.SelectedValue = vaga.ReqAreaAtuacao.ToString();
                txtHabilidades.Text = vaga.ReqHabilidades;
                txtAtividades.Text = vaga.ReqAtividades;
                txtCartaoEntrevista.Text = vaga.ReqContaoEntrevista;
                txtObservacao.Text = vaga.ReqObservacoes;
                txtObservacaoInst.Text = vaga.ReqObservacoesInst;
                txtBenefício.Text = vaga.ReqObservacoes;
                txtSalario.Text = vaga.ReqSalario.ToString();
                txtHorarioTrabalho.Text = vaga.ReqHorarioTrabalho == null || vaga.ReqHorarioTrabalho.Equals(string.Empty) ? "" : vaga.ReqHorarioTrabalho.ToString();
                DDSituacao.SelectedValue = vaga.ReqSituacao;
            }
        }

        protected void BTLimpa_Click(object sender, EventArgs e)
        {
            Limpadados();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            CarregaVagas();
        }



        private void Limpadados()
        {
            txtAtividades.Text = "";
            txtCaracteristicaPessoal.Text = "";
            txtCartaoEntrevista.Text = "";
            txtCodigoVaga.Text = "";
            txtDataSolicitacao.Text = "";
            txtDataEntrevista.Text = "";
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


        protected void BTsalva_Click(object sender, EventArgs e)
        {
            try
            {
                //  if (Session["comando"].Equals("Inserir") && TBCodEscola.Equals(string.Empty)) throw new ArgumentException("Digite o código da escola.");
                if (DDAreaAtuacao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Área de Atuação obrigatória");
                if (DDEmpresa.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Empresa Obrigatório.");
                //   if (TB_Numero_endereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o número do endereço da escola.");
                ////   if (TBcidade.Text.Equals(string.Empty)) throw new ArgumentException("Digite a cidade da escola.");
                //  // if (DDMunicipio.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Escolha cidade da escola.");
                //   if (TB_Bairro.Text.Equals(string.Empty)) throw new ArgumentException("Digite o bairro da escola.");
                //   if (!TBCep.Text.Equals(string.Empty) && Funcoes.Retirasimbolo(TBCep.Text).Length != 8) throw new ArgumentException("CEP incorreto.");

                //   if (!TBtelefone.Text.Equals(""))
                //       if (!Funcoes.ValidaTelefone(TBtelefone.Text)) throw new ArgumentException("Telefone inválido.");

                using (var repository = new Repository<RequisicoesVagas>(new Context<RequisicoesVagas>()))
                {
                    var vaga = new RequisicoesVagas();

                    // vaga.ReqId = 1;
                    vaga.ReqId = txtCodigoVaga.Text.Equals(string.Empty) ? new int() : int.Parse(txtCodigoVaga.Text);
                    vaga.ReqEmpresa = int.Parse(DDEmpresa.SelectedValue);
                    vaga.ReqDataSolitação = txtDataSolicitacao.Text == string.Empty ? new DateTime() : Convert.ToDateTime(txtDataSolicitacao.Text);
                    vaga.ReqDataEntrevista = txtDataEntrevista.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataEntrevista.Text);

                    vaga.ReqQuantidade = txtQuantidade.Text == string.Empty ? new short() : short.Parse(txtQuantidade.Text);
                    vaga.ReqSexo = DDSexo.SelectedValue;
                    vaga.ReqHorarioEntrevista = txtHorarioEntrevista.Text;
                    vaga.ReqSubstituicao = DDSubstituicao.SelectedValue;
                    vaga.ReqCaracteristicasPessoais = txtCaracteristicaPessoal.Text;
                    vaga.ReqAreaAtuacao = DDAreaAtuacao.SelectedValue == string.Empty ? new int() : int.Parse(DDAreaAtuacao.SelectedValue);
                    vaga.ReqHabilidades = txtHabilidades.Text;
                    vaga.ReqAtividades = txtAtividades.Text;
                    vaga.ReqContaoEntrevista = txtCartaoEntrevista.Text;
                    vaga.ReqObservacoes = txtObservacao.Text;
                    vaga.ReqSituacao = "A";
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
                        if (DDSituacao.SelectedValue.Equals("E"))
                        {
                            var sql = "update CA_Encaminhamentos set Enc_Status = 'R' where Enc_Requisicao = " + txtCodigoVaga.Text + " and Enc_Status = 'E'";
                            var con = new Conexao();
                            con.Alterar(sql);
                        }
                      

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

        #region GetCompletionList

        [WebMethod]
        public static string[] GetCompletionList(String prefixText, int count)
        {
            var values = new List<string>();
            string commandText =
                "SELECT TOP " + count.ToString()
                + " MunIDescricao FROM MA_Municipios WHERE MunIDescricao LIKE '" + prefixText + "%'";
            var comm = new SqlCommand(commandText, new SqlConnection(GetConfig.Config())) { CommandType = CommandType.Text };
            var suggestions = new List<string>();
            var dtSuggestions = new DataTable();
            comm.Connection.Open();
            var adptr = new SqlDataAdapter(comm);
            adptr.Fill(dtSuggestions);
            if (dtSuggestions.Rows != null && dtSuggestions.Rows.Count > 0)
            {
                suggestions.AddRange(from DataRow dr in dtSuggestions.Rows select dr["MunIDescricao"].ToString());
                values = suggestions;
            }
            return values.ToArray();
        }

        #endregion

        protected void listar_Click(object sender, EventArgs e)
        {
            CarregaVagas();
            MultiView1.ActiveViewIndex = 0;
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void Incluir_Click(object sender, EventArgs e)
        {

            CarregaAreaAtuacao();
            CarregaEmpresa();
            Limpadados();
            txtDataSolicitacao.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            MultiView1.ActiveViewIndex = 1;
            ///  TBCodEscola.ReadOnly = false;
            Session["comando"] = "Inserir";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gvr = ((GridView)sender).SelectedRow;
            Session["comando"] = "Alterar";
            HFEscolaRef.Value = WebUtility.HtmlDecode(gvr.Cells[0].Text);
            PreencheCampos(WebUtility.HtmlDecode(gvr.Cells[0].Text));
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var escola = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<Escolas>(new Context<Escolas>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(escola);
            }
            CarregaVagas();
        }

        protected void IndiceZeroUF(object sender, EventArgs e)
        {
            var indice0 = new ListItem("--", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CarregaVagas();
        }

        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 1;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            var gvr = ((GridView)sender).SelectedRow;
            Session["comando"] = "Alterar";
            HFEscolaRef.Value = WebUtility.HtmlDecode(gvr.Cells[0].Text);
            CarregaAreaAtuacao();
            CarregaEmpresa();
            PreencheCampos(WebUtility.HtmlDecode(gvr.Cells[0].Text));
            MultiView1.ActiveViewIndex = 1;
        }

        protected void DDSituacao_TextChanged(object sender, EventArgs e)
        {
            if (DDSituacao.SelectedValue.Equals("E"))
            {
                string codVaga = txtCodigoVaga.Text;
                var sql = "select * from CA_Encaminhamentos where Enc_Requisicao = " + codVaga + " and Enc_Status = 'E'";
                var con = new Conexao();
                var result = con.Consultar(sql);
                bool valida = false;

                while (result.Read())
                {
                    valida = true;
                    // sql = "update CA_Encaminhamentos set = 'R' where Enc_Requisicao = " + codVaga + " ";
                    //con = new Conexao();
                    //con.Alterar(sql);
                }

                if (valida)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                     "alert('As vagas que estão encaminhadas serão passadas para reprovado com o encerramento dessa vaga.');", true);
                    //  DDSituacao.SelectedValue = "A";
                }
            }
        }

        protected void DDEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAdressEmpresa(int.Parse(DDEmpresa.SelectedValue));
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