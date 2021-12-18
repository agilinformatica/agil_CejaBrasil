using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class CadastroParceirosUnidades : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            if (!IsPostBack)
            {
                BindGridView();
                BindParceiros(DD_tipo);
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTsalva.Enabled = false;
            }
        }

        private void BindGridView()
        {
            if (DD_tipo.SelectedValue.Equals(string.Empty))
            {
                GridView1.DataBind();
                Panel1.Visible = false;
                return;
            }
            using (var repository = new Repository<ParceirosUnidade>(new Context<ParceirosUnidade>()))
            {
                var datasource = new List<ParceirosUnidade>();
                datasource.AddRange(repository.All().Where(p => p.ParUniCodigoParceiro.Equals(Convert.ToInt32(DD_tipo.SelectedValue))).OrderBy(p => p.ParUniDescricao));
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
                Panel1.Visible = GridView1.Rows.Count == 0;
            }
        }

        private void BindMunicipios()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = from i in bd.MA_Municipios select new {MunIEstado = i.MunIEstado};
                var datasource = dados.Distinct().OrderBy(p => p.MunIEstado);
                DD_estado_Nat.DataSource = datasource;
                DD_estado_Nat.DataBind();
            }
        }

        public void BindParceiros(DropDownList dropDown)
        {
            using (var repository = new Repository<Parceiros>(new Context<Parceiros>()))
            {
                var datasource = new List<Parceiros>();
                datasource.AddRange(repository.All().OrderBy(p =>p.ParNomeFantasia));
                dropDown.DataSource = datasource;
                dropDown.DataBind();
            }
        }

        private void PreencheCampos( string[] chaves)
        {
            BindMunicipios();
            BindParceiros(DDparceiro);
            using (var repository = new Repository<ParceirosUnidade>(new Context<ParceirosUnidade>()))
            {
                var parceiro = repository.Find(Convert.ToInt32(chaves[0]), Convert.ToInt32(chaves[1]));
                DDparceiro.SelectedValue = parceiro.ParUniCodigoParceiro.ToString();
                TBCodigo.Text = parceiro.ParUniCodigo.ToString();
                TBtelefone.Text = Funcoes.FormataTelefone(parceiro.ParUniTelefone);
                TBcgc.Text = Funcoes.FormataCNPJ(parceiro.ParUniCNPJ);
                TBDescricao.Text = parceiro.ParUniDescricao;
                TBCelular.Text = Funcoes.FormataTelefoneSaoPaulo(parceiro.ParUniCelular);
                TBrepresentante.Text = parceiro.ParUniNomeContato;
                TBTaxaVinte.Text = string.Format("{0:F}", parceiro.ParUniTaxa20Horas);
                TBBolsaVinte.Text = string.Format("{0:F}", parceiro.ParUniBolsa20Horas);
                TBTaxaTrinta.Text = string.Format("{0:F}", parceiro.ParUniTaxa30Horas);
                TBBolsaTrinta.Text = string.Format("{0:F}", parceiro.ParUniBolsa30Horas);
                TBEndereco.Text = parceiro.ParUniEndereco;
                TB_Numero_endereco.Text = parceiro.ParUniNumeroEndereco;
                TB_complemento.Text = parceiro.ParUniComplemento;
                TB_Bairro.Text = parceiro.ParUniBairro;

                DD_estado_Nat.SelectedValue = parceiro.ParUniEstado;
                //TBcidade.Text = parceiro.ParUniCidade;
                //PreencheMunicipio();
               txtMunicipioEndereco.Text = parceiro.ParUniCidade;

                
               // TBCepEndAlu.Text = Funcoes.FormataCep(parceiro.ParUniCEP);
                TBCepEndAlu.Text = parceiro.ParUniCEP;
                TB_email_resp.Text = parceiro.ParUniEmail;
            }
            DDparceiro.Enabled = false;
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
            TBtelefone.Text = string.Empty;
            TBcgc.Text = string.Empty;
            TBDescricao.Text = string.Empty;
            TBCelular.Text = string.Empty;
            TBrepresentante.Text = string.Empty;
            TBTaxaVinte.Text = string.Empty;
            TBBolsaVinte.Text = string.Empty;
            TBTaxaTrinta.Text = string.Empty;
            TBBolsaTrinta.Text = string.Empty;
            TBEndereco.Text = string.Empty;
            TB_Numero_endereco.Text = string.Empty;
            TB_complemento.Text = string.Empty;
            TB_Bairro.Text = string.Empty;
            //TBcidade.Text = string.Empty;
            txtMunicipioEndereco.Text = "";

            DD_estado_Nat.SelectedValue = string.Empty;
            TBCepEndAlu.Text = string.Empty;
            TB_email_resp.Text = string.Empty;
        }


        protected void BTsalva_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBDescricao.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome da unidade.");
                if (DDparceiro.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um parceiro para a unidade.");
                if (!TBCepEndAlu.Text.Equals(string.Empty) && Funcoes.Retirasimbolo(TBCepEndAlu.Text).Length != 8) throw new ArgumentException("CEP incorreto.");
                if (!TB_email_resp.Text.Equals(string.Empty) && !Funcoes.ValidaEmail(TB_email_resp.Text)) throw new ArgumentException("E-mail inválido.");
                if (!TBtelefone.Text.Equals(string.Empty) && !Funcoes.ValidaTelefone(TBtelefone.Text)) throw new ArgumentException("Telefone inválido.");
                //if (!TBCelular.Text.Equals(string.Empty)) throw new ArgumentException("Celular inválido.");

                using (var repository = new Repository<ParceirosUnidade>(new Context<ParceirosUnidade>()))
                {

                    var parceiro = Session["comando"].Equals("Inserir") ? new ParceirosUnidade() : repository.Find(int.Parse(Session["Alteracodigo01"].ToString()), int.Parse(Session["Alteracodigo02"].ToString()));
                    parceiro.ParUniCodigoParceiro = Convert.ToInt32(DDparceiro.SelectedValue);
                    parceiro.ParUniTelefone = Funcoes.Retirasimbolo(TBtelefone.Text);
                    parceiro.ParUniCNPJ = Funcoes.Retirasimbolo(TBcgc.Text);
                    parceiro.ParUniDescricao = TBDescricao.Text;
                    parceiro.ParUniCelular = Funcoes.Retirasimbolo(TBCelular.Text);
                    parceiro.ParUniNomeContato = TBrepresentante.Text;
                    parceiro.ParUniTaxa20Horas = TBTaxaVinte.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TBTaxaVinte.Text);
                    parceiro.ParUniBolsa20Horas = TBBolsaVinte.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TBBolsaVinte.Text);
                    parceiro.ParUniTaxa30Horas = TBTaxaTrinta.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TBTaxaTrinta.Text);
                    parceiro.ParUniBolsa30Horas = TBBolsaTrinta.Text.Equals(string.Empty) ? 0 : Convert.ToSingle(TBBolsaTrinta.Text);
                    parceiro.ParUniEndereco = TBEndereco.Text;
                    parceiro.ParUniNumeroEndereco = TB_Numero_endereco.Text; 
                    parceiro.ParUniComplemento = TB_complemento.Text;
                    parceiro.ParUniBairro = TB_Bairro.Text;
                    //parceiro.ParUniCidade = TBcidade.Text;
                    
                    parceiro.ParUniEstado = DD_estado_Nat.SelectedValue;
                    
                    parceiro.ParUniCidade = txtMunicipioEndereco.Text;

                    parceiro.ParUniCEP = Funcoes.Retirasimbolo(TBCepEndAlu.Text);
                    parceiro.ParUniEmail = TB_email_resp.Text;

                    if(Session["comando"].Equals("Inserir"))
                    {
                        parceiro.ParUniDataCadastro = DateTime.Today;
                        parceiro.ParUniUsuarioCadastro = Session["codigo"].ToString();
                    }
                    else
                    {
                        parceiro.ParUniDataAlteracao = DateTime.Today;
                        parceiro.ParUniUsuarioAlteracao = Session["codigo"].ToString();
                    }

                    if (Session["comando"].Equals("Inserir")) repository.Add(parceiro);
                    else  repository.Edit(parceiro);
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
                Funcoes.TrataExcessao("000200", ex);
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
            BindGridView();
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
            BindParceiros(DDparceiro);
            Limpadados();
            TBCodigo.Enabled = false;
            DDparceiro.Enabled = true;
            Session["comando"] = "Inserir";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gvr = ((GridView) sender).SelectedRow;
            Session["comando"] = "Alterar";
            var chaves = new string[] { WebUtility.HtmlDecode(gvr.Cells[0].Text), DD_tipo.SelectedValue};
            Session["Alteracodigo01"] =  WebUtility.HtmlDecode(gvr.Cells[0].Text);
            Session["Alteracodigo02"] = DD_tipo.SelectedValue;
            PreencheCampos(chaves);
            MultiView1.ActiveViewIndex = 1;
        }


        protected void DD_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton) sender;
            var gvr = (GridViewRow) bt.Parent.Parent;
            var unidade = int.Parse(gvr.Cells[0].Text);
            using (var repository = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                try
                {
                    if (Convert.ToBoolean(HFConfirma.Value))
                    {
                        var dados = repository.CA_AlocacaoAprendizs.Where(p => p.ALAUnidadeParceiro == unidade);
                        if (dados.Count() > 0) throw new ArgumentException("Não é possível excluir esta unidade. Há aprendizes vinculados à ela.");

                        var linha = repository.CA_ParceirosUnidades.Where(p => p.ParUniCodigo == unidade).First();
                        repository.CA_ParceirosUnidades.DeleteOnSubmit(linha);
                        repository.SubmitChanges();
                    }
                    BindGridView();
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void DD_estado_Nat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PreencheMunicipio();
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

        protected void TBCepEndAlu_TextChanged(object sender, EventArgs e)
        {
            var sql = "Select * from Ca_CodigosCep where Cep_Codigo = " + Funcoes.Retirasimbolo(TBCepEndAlu.Text) + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                TBEndereco.Text = result["Cep_Logradouro"].ToString();
                TB_Bairro.Text = result["Cep_Bairro"].ToString();
                DD_estado_Nat.SelectedValue = result["Cep_UF"].ToString();
                txtMunicipioEndereco.Text = result["Cep_Cidade"].ToString();
                // DDMunicipioEndereco.SelectedValue = "SAO PAULO";
            }

        }
    }
}