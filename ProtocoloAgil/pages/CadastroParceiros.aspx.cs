using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class CadastroParceiros : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["CurrentPage"] = "configuracoes";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if (!IsPostBack)
            {
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTsalva.Enabled = false;
            }
        }

        private void BindGridView(int tipo)
        {

            using (var repository = new Repository<Parceiros>(new Context<Parceiros>()))
            {
                // var datasource = new List<Parceiros>();
                // datasource.AddRange(tipo == 1 ? repository.All().OrderBy(p => p.ParDescricao) :
                //    repository.All().Where(p => p.ParNomeFantasia.ToLower().Contains(pesquisa.Text.ToLower().Trim())).OrderBy(p => p.ParNomeFantasia));




            }

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = (from S in db.CA_Parceiros
                             where txtCidade.Text.Equals(string.Empty) ? 1 == 1 : S.ParCidade.ToLower().Trim() == txtCidade.Text.ToLower().Trim()

                             select S).ToList().OrderBy(item => item.ParDescricao);

                var queryAux = query;
                if (!pesquisa.Text.Equals(string.Empty))
                {
                    query = query.Where(p => p.ParNomeFantasia.ToLower().Trim().Contains(pesquisa.Text.ToLower().Trim())).OrderBy(p => p.ParNomeFantasia);
                    if (query.Count() == 0)
                    {
                        query = queryAux.Where(p => p.ParDescricao.ToLower().Trim().Contains(pesquisa.Text.ToLower().Trim())).OrderBy(p => p.ParDescricao);
                    }
                }

                if (!txtCnpj.Equals(string.Empty))
                {
                    query = query.Where(p => p.ParCNPJ.Contains(Funcoes.Retirasimbolo(txtCnpj.Text))).OrderBy(p => p.ParNomeFantasia);
                }



                HFRowCount.Value = query.Count().ToString();
                GridView1.DataSource = query.ToList();
                GridView1.DataBind();
            }

        }

        private void BindMunicipios()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = from i in bd.MA_Municipios select new { MunIEstado = i.MunIEstado };
                var datasource = dados.Distinct().OrderBy(p => p.MunIEstado);
                //DD_estado_Nat.DataSource = datasource;
                //DD_estado_Nat.DataBind();
                DD_estado_Nat.DataSource = datasource;
                DD_estado_Nat.DataBind();
            }
        }

        private void BindRamosAtividade()
        {
            using (var repository = new Repository<RamoAtividade>(new Context<RamoAtividade>()))
            {
                var datasource = new List<RamoAtividade>();
                datasource.AddRange(repository.All().OrderBy(item => item.RatDescricao));
                DDramoAtuacao.DataSource = datasource;
                DDramoAtuacao.DataBind();
            }
        }

        private void BindUsuarios()
        {
            using (var repository = new Repository<Usuarios>(new Context<Usuarios>()))
            {
                var datasource = new List<Usuarios>();
                datasource.AddRange(repository.All());
                dd_responsavel_fundacao.Items.Clear();
                dd_responsavel_fundacao.DataSource = datasource;
                dd_responsavel_fundacao.DataBind();
            }
        }

        private void PreencheCampos(int codigo)
        {
            BindMunicipios();
            BindRamosAtividade();
            BindUsuarios();

            using (var repository = new Repository<Parceiros>(new Context<Parceiros>()))
            {
                var parceiro = repository.Find(codigo);
                TBCodigo.Text = parceiro.ParCodigo.ToString();
                TBnomeParceiro.Text = parceiro.ParNomeFantasia;
                TBtelefone.Text = Funcoes.FormataTelefone(parceiro.ParTelefone);
                TBcgc.Text = Funcoes.FormataCNPJ(parceiro.ParCNPJ);
                TBDescricao.Text = parceiro.ParDescricao;
                TBCelular.Text = Funcoes.FormataTelefoneSaoPaulo(parceiro.ParCelular);
                TBrepresentante.Text = parceiro.ParNomeContato;
                TBBolsaTrinta.Text = string.Format("{0:F}", parceiro.ParBolsa30Horas);

                txtDataContrato.Text = string.Format("{0:dd/MM/yyyy}", parceiro.ParDataContrato);

                DDramoAtuacao.SelectedValue = parceiro.ParAtividadeId.ToString();
                TBruaEndAlu.Text = parceiro.ParEndereco;
                TB_Numero_endereco.Text = parceiro.ParNumeroEndereco;
                TB_complemento.Text = parceiro.ParComplemento;
                txtBairro.Text = parceiro.ParBairro;
                txtMunicipioEndereco.Text = parceiro.ParCidade;
                //     PreencheMunicipioEdicao(parceiro.ParEstado);
                //     DDMunicipio.SelectedValue = parceiro.ParCidade;

                DD_estado_Nat.SelectedValue = parceiro.ParEstado;
                TBCepEndAlu.Text = parceiro.ParCEP;


                TBCepEndAlu.Text = Funcoes.FormataCep(parceiro.ParCEP);
                TB_email_resp.Text = parceiro.ParEmail;
                TB_site_escola.Text = parceiro.ParSiteEmpresa;
                dd_responsavel_fundacao.SelectedValue = parceiro.ParRespFundacao;
                TBInscricao.Text = parceiro.ParInscricao;
                TBInscricaoMunicipal.Text = parceiro.ParInscricaoMunicipal;



                TBEmailGestorPrograma.Text = parceiro.ParEmailGestorPrograma;
                TBGestorPrograma.Text = parceiro.ParGestorPrograma;
                TBGestorFinanceiro.Text = parceiro.ParGestorFinanceiro;
                TBEmailGestorFinanceiro.Text = parceiro.ParEmailGestorFinanceiro;
                TBGestorRH.Text = parceiro.ParGestorRH;
                TBEmailGestorRH.Text = parceiro.ParEmailGestorRH;
                TBCargoRepresentanteLegal.Text = parceiro.ParCargoRepresentanteLegal;
                TBEmailRepresentanteLegal.Text = parceiro.ParEmailRespresentanteLegal;

                DDSituacao.SelectedValue = parceiro.ParSituacao;
                //txtValeRefeicao.Text = string.Format("{0:F}", parceiro.ParValeRefeicao);
                //txtValeAlimentacao.Text = string.Format("{0:F}", parceiro.ParValeAlimentacao);
                //DDValeTransporte.SelectedValue = parceiro.ParValeTransporte;
                txtBeneficios.Text = parceiro.ParBeneficios;
                txtObservacao.Text = parceiro.ParObservacoes;

                txtConvenio.Text = parceiro.ParConvenio;
                txtCotaAprendiz.Text = parceiro.ParCotaAprendiz == null ? "" : parceiro.ParCotaAprendiz.ToString();


                txtDataAniversarioRepresentanteLegal.Text = string.Format("{0:dd/MM/yyyy}", parceiro.ParDataAnivRepLegal);
                txtDataAniversarioGestorRH.Text = string.Format("{0:dd/MM/yyyy}", parceiro.ParDataAnivGestorRH);
                txtDataAniversarioGestorPrograma.Text = string.Format("{0:dd/MM/yyyy}", parceiro.ParDataAnivGestorProg);
                txtDataAniversarioGestorFinanceiro.Text = string.Format("{0:dd/MM/yyyy}", parceiro.ParDataAnivGestorFinanc);

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

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        private void Limpadados()
        {
            TBCodigo.Text = string.Empty;
            TBnomeParceiro.Text = string.Empty;
            TBtelefone.Text = string.Empty;
            TBcgc.Text = string.Empty;
            TBDescricao.Text = string.Empty;
            TBCelular.Text = string.Empty;
            TBrepresentante.Text = string.Empty;

            txtDataContrato.Text = string.Empty;
            TBBolsaTrinta.Text = string.Empty;
            DDramoAtuacao.SelectedValue = string.Empty;
            TBruaEndAlu.Text = string.Empty;
            TB_Numero_endereco.Text = string.Empty;
            TB_complemento.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCotaAprendiz.Text = string.Empty;
            txtConvenio.Text = string.Empty;

            txtMunicipioEndereco.Text = string.Empty;
            //   DDMunicipio.SelectedValue = "";

            DD_estado_Nat.SelectedValue = string.Empty;
            TBCepEndAlu.Text = string.Empty;
            TB_email_resp.Text = string.Empty;
            TB_site_escola.Text = string.Empty;
            TBInscricao.Text = string.Empty;
            TBInscricaoMunicipal.Text = string.Empty;
            TBEmailGestorPrograma.Text = string.Empty;
            TBGestorPrograma.Text = string.Empty;
            TBGestorFinanceiro.Text = string.Empty;
            TBEmailGestorFinanceiro.Text = string.Empty;
            TBGestorRH.Text = string.Empty;
            TBEmailGestorRH.Text = string.Empty;
            TBCargoRepresentanteLegal.Text = string.Empty;
            TBEmailRepresentanteLegal.Text = string.Empty;
            TBInscricao.Text = string.Empty;
            txtDataAniversarioGestorPrograma.Text = string.Empty;
            txtDataAniversarioGestorRH.Text = string.Empty;
            txtDataAniversarioGestorFinanceiro.Text = string.Empty;
            txtDataAniversarioRepresentanteLegal.Text = string.Empty;
        }

        protected void BTsalva_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBnomeParceiro.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome do parceiro.");
                if (!TBCepEndAlu.Text.Equals(string.Empty) && Funcoes.Retirasimbolo(TBCepEndAlu.Text).Length != 8) throw new ArgumentException("CEP incorreto.");
                if (DDramoAtuacao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma área de Atuação.");
                if (!TBtelefone.Text.Equals(string.Empty) && !Funcoes.ValidaTelefone(TBtelefone.Text)) throw new ArgumentException("Telefone inválido.");
                //if (!TBCelular.Text.Equals(string.Empty) && TBCelular.Text) throw new ArgumentException("Celular inválido.");

                //valida emails, caso seja preenchido 22/04/2014
                if (!TBEmailRepresentanteLegal.Text.Equals(string.Empty) && !Funcoes.ValidaEmail(TBEmailRepresentanteLegal.Text)) throw new ArgumentException("E-mail Representate Legal inválido.");
                if (!TBEmailGestorPrograma.Text.Equals(string.Empty) && !Funcoes.ValidaEmail(TBEmailGestorPrograma.Text)) throw new ArgumentException("E-mail Gestor Programa inválido.");
                if (!TBEmailGestorFinanceiro.Text.Equals(string.Empty) && !Funcoes.ValidaEmail(TBEmailGestorFinanceiro.Text)) throw new ArgumentException("E-mail Gestor Financeiro inválido.");
                if (!TBEmailGestorRH.Text.Equals(string.Empty) && !Funcoes.ValidaEmail(TBEmailGestorRH.Text)) throw new ArgumentException("E-mail Gestor RH inválido.");
                if (!TB_email_resp.Text.Equals(string.Empty) && !Funcoes.ValidaEmail(TB_email_resp.Text)) throw new ArgumentException("E-mail Institucional inválido.");

                using (var repository = new Repository<Parceiros>(new Context<Parceiros>()))
                {

                    var parceiro = Session["comando"].Equals("Inserir") ? new Parceiros() : repository.Find(Convert.ToInt32(Session["Alteracodigo"]));
                    parceiro.ParCodigo = Session["comando"].Equals("Inserir") ? 0 : Convert.ToInt32(TBCodigo.Text);
                    parceiro.ParNomeFantasia = TBnomeParceiro.Text;
                    parceiro.ParTelefone = Funcoes.Retirasimbolo(TBtelefone.Text);
                    parceiro.ParCNPJ = Funcoes.Retirasimbolo(TBcgc.Text);
                    parceiro.ParInscricao = Funcoes.Retirasimbolo(TBInscricao.Text);
                    parceiro.ParInscricaoMunicipal = Funcoes.Retirasimbolo(TBInscricaoMunicipal.Text);
                    parceiro.ParDescricao = TBDescricao.Text;
                    parceiro.ParCelular = Funcoes.Retirasimbolo(TBCelular.Text);
                    parceiro.ParNomeContato = TBrepresentante.Text;
                    parceiro.ParTaxa20Horas = 0;
                    parceiro.ParBolsa20Horas = 0;
                    parceiro.ParTaxa30Horas = 0;
                    parceiro.ParBolsa30Horas = TBBolsaTrinta.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TBBolsaTrinta.Text);
                    parceiro.ParDataContrato = txtDataContrato.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataContrato.Text);
                    parceiro.ParAtividadeId = Convert.ToInt32(DDramoAtuacao.SelectedValue);
                    parceiro.ParEndereco = TBruaEndAlu.Text;
                    parceiro.ParComplemento = TB_complemento.Text;
                    parceiro.ParBairro = txtBairro.Text;

                    parceiro.ParCidade = txtMunicipioEndereco.Text;
                    //  parceiro.ParCidade = DDMunicipio.SelectedValue;

                    parceiro.ParEstado = DD_estado_Nat.SelectedValue;
                    parceiro.ParCEP = Funcoes.Retirasimbolo(TBCepEndAlu.Text);
                    parceiro.ParEmail = TB_email_resp.Text;
                    parceiro.ParSiteEmpresa = TB_site_escola.Text;



                    parceiro.ParEmailGestorPrograma = TBEmailGestorPrograma.Text;
                    parceiro.ParGestorPrograma = TBGestorPrograma.Text;
                    parceiro.ParGestorFinanceiro = TBGestorFinanceiro.Text;
                    parceiro.ParEmailGestorFinanceiro = TBEmailGestorFinanceiro.Text;
                    parceiro.ParGestorRH = TBGestorRH.Text;
                    parceiro.ParEmailGestorRH = TBEmailGestorRH.Text;
                    parceiro.ParCargoRepresentanteLegal = TBCargoRepresentanteLegal.Text;
                    parceiro.ParEmailRespresentanteLegal = TBEmailRepresentanteLegal.Text;

                    parceiro.ParSituacao = DDSituacao.SelectedValue;
                    //parceiro.ParValeRefeicao = txtValeRefeicao.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(txtValeRefeicao.Text);
                    //parceiro.ParValeAlimentacao = txtValeAlimentacao.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(txtValeAlimentacao.Text);
                    //parceiro.ParValeTransporte = DDValeTransporte.SelectedValue;
                    parceiro.ParBeneficios = txtBeneficios.Text;
                    parceiro.ParObservacoes = txtObservacao.Text;
                    parceiro.ParCotaAprendiz = txtCotaAprendiz.Text == string.Empty ? (short?)null : short.Parse(txtCotaAprendiz.Text);
                    parceiro.ParConvenio = txtConvenio.Text;



                    parceiro.ParDataAnivGestorProg = txtDataAniversarioGestorPrograma.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataAniversarioGestorPrograma.Text);
                    parceiro.ParDataAnivGestorRH = txtDataAniversarioGestorRH.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataAniversarioGestorRH.Text);
                    parceiro.ParDataAnivGestorFinanc = txtDataAniversarioGestorFinanceiro.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataAniversarioGestorFinanceiro.Text);
                    parceiro.ParDataAnivRepLegal = txtDataAniversarioRepresentanteLegal.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataAniversarioRepresentanteLegal.Text);


                    if (!Session["comando"].Equals("Inserir"))
                    {
                        HFCurrent.Value = parceiro.ParRespFundacao;
                        HFChanged.Value = dd_responsavel_fundacao.SelectedValue;
                    }
                    parceiro.ParRespFundacao = dd_responsavel_fundacao.SelectedValue;

                    if (Session["comando"].Equals("Inserir"))
                    {
                        parceiro.ParSenha = "1";
                        parceiro.ParDataCadastro = DateTime.Today;
                        parceiro.ParUsuarioCadastro = Session["codigo"].ToString();
                    }
                    else
                    {
                        parceiro.ParDataAlteracao = DateTime.Today;
                        parceiro.ParUsuarioCadastro = Session["codigo"].ToString();
                    }

                    if (Session["comando"].Equals("Inserir"))
                        repository.Add(parceiro);
                    else
                        repository.Edit(parceiro);
                }
                if (!Session["comando"].Equals("Inserir") && (!HFCurrent.Value.Equals(HFChanged.Value))
                    && !string.IsNullOrEmpty(HFCurrent.Value) && !string.IsNullOrEmpty(HFChanged.Value))
                {
                    ChangeResponsavel(HFCurrent.Value, HFChanged.Value);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação Realizada com Sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                //Funcoes.TrataExcessao("000200", ex);
            }
        }

        private void ChangeResponsavel(string current, string changed)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var alocacoes = from i in bd.CA_AlocacaoAprendizs
                                join p in bd.CA_ParceirosUnidades on i.ALAUnidadeParceiro equals p.ParUniCodigo
                                join m in bd.CA_Parceiros on p.ParUniCodigoParceiro equals m.ParCodigo
                                where i.ALAStatus == "A" && i.ALATutor == current
                                select i;

                foreach (var item in alocacoes)
                {
                    item.ALATutor = changed;
                }

                bd.SubmitChanges();
            }
        }

        #region GetCompletionList

        [WebMethod]
        public static string[] GetCompletionList(String prefixText, int count)
        {
            var values = new List<string>();
            string commandText = "SELECT TOP " + count.ToString()
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
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void IndiceZeroUF(object sender, EventArgs e)
        {
            var indice0 = new ListItem("--", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void Incluir_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            BindMunicipios();
            BindRamosAtividade();
            BindUsuarios();
            Limpadados();
            TBCodigo.Enabled = false;
            Session["comando"] = "Inserir";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gvr = ((GridView)sender).SelectedRow;
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = WebUtility.HtmlDecode(gvr.Cells[0].Text);
            PreencheCampos(Convert.ToInt32(WebUtility.HtmlDecode(gvr.Cells[0].Text)));
            MultiView1.ActiveViewIndex = 1;
        }


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "8";
            MultiView1.ActiveViewIndex = 2;
        }

        protected void texto_Click(object sender, EventArgs e)
        {
            var filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            try
            {
                using (var repository = new Repository<Parceiros>(new Context<Parceiros>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.ParCodigo + "; " + item.ParNomeFantasia + "; " + item.ParEndereco + "; " + item.ParNumeroEndereco + "; " + item.ParComplemento
                            + "; " + item.ParBairro + "; " + item.ParCidade + "; " + item.ParEstado + "; " + Funcoes.FormataTelefone(item.ParTelefone);
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Parceiros.txt");
                }
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            var codigo = int.Parse(bt.CommandArgument);
            using (var repository = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                try
                {
                    if (Convert.ToBoolean(HFConfirma.Value))
                    {
                        if (repository.CA_ParceirosUnidades.Where(p => p.ParUniCodigoParceiro == codigo).Count() > 0)
                            throw new ArgumentException("Não é possível excluir este parceiro. O mesmo possui uidades cadastradas.");

                        var parceiro = repository.CA_Parceiros.Where(p => p.ParCodigo == codigo).First();
                        repository.CA_Parceiros.DeleteOnSubmit(parceiro);
                        repository.SubmitChanges();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('Ação Realizada com Sucesso.')", true);
                    BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
                }
                catch (ArgumentException ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('" + ex.Message + "')", true);
                }
                catch (Exception ex)
                {
                    Funcoes.TrataExcessao("000201", ex);
                }
            }
        }

        protected void DD_estado_Nat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   PreencheMunicipio();
        }

        //public void PreencheMunicipio()
        //{
        //    var valor = DD_estado_Nat.SelectedValue;
        //    using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
        //    {
        //        var query = (from M in bd.MA_Municipios
        //                     where M.MunIEstado == valor
        //                     select new { M.MunIDescricao }).ToList();

        //        DDMunicipio.DataSource = query;
        //        DDMunicipio.DataBind();
        //    }
        //}

        //public void PreencheMunicipioEdicao(string estado)
        //{
        //    using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
        //    {
        //        var query = (from M in bd.MA_Municipios
        //                     where M.MunIEstado == estado
        //                     select new { M.MunIDescricao }).ToList();

        //        DDMunicipio.DataSource = query;
        //        DDMunicipio.DataBind();
        //    }
        //}

        protected void TBCepEndAlu_TextChanged(object sender, EventArgs e)
        {
            var sql = "Select * from Ca_CodigosCep where Cep_Codigo = " + Funcoes.Retirasimbolo(TBCepEndAlu.Text) + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                TBruaEndAlu.Text = result["Cep_Logradouro"].ToString();
                txtBairro.Text = result["Cep_Bairro"].ToString();
                DD_estado_Nat.SelectedValue = result["Cep_UF"].ToString();
                txtMunicipioEndereco.Text = result["Cep_Cidade"].ToString();
                // DDMunicipioEndereco.SelectedValue = "SAO PAULO";
            }

        }

        protected void btnConvenio_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            var codigo = int.Parse(bt.CommandArgument);

            Session["id"] = 102;
            Session["CodParceiroImpressao"] = codigo;
            MultiView1.ActiveViewIndex = 2;
        }
    }
}