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

namespace ProtocoloAgil.pages
{
    public partial class CadastroEscola : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            if (!IsPostBack)
            {
                BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTsalva.Enabled = false;
            }
        }


        private void BindGridView( int tipo)
        {
            using (var repository = new Repository<Escolas>(new Context<Escolas>()))
            {
                var datasource = new List<Escolas>();
                datasource.AddRange( tipo == 1 ? repository.All().OrderBy(p=>p.EscNome)
                    : repository.All().Where(p => p.EscNome.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.EscNome)); 
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }

        private void PreencheCampos(string codigo)
        {
            // ----------------------------------preenche campos ------------------------------------>
            using (var repository = new Repository<Escolas>(new Context<Escolas>()))
            {
                var escola = repository.Find(Convert.ToInt32(codigo));
                BindMunicipios();     
                TBCodEscola.Text = escola.EscCodigo.ToString();
                TBnomeEsc.Text = escola.EscNome;
                //TBcidade.Text = escola.EscCidade;
                DD_estado_Nat.SelectedValue = escola.EscEstado;
                PreencheMunicipio();
                DDMunicipio.SelectedValue = escola.EscCidade.ToUpper();

                TBEndereco.Text = escola.EscEndereco;
                TBCep.Text = escola.EscCEP == null ? "" : Funcoes.FormataCep(escola.EscCEP);
                
                TBrepresentante.Text = escola.EscDiretor;
                TB_Numero_endereco.Text = escola.EscNumeroEndereco;
                TB_Bairro.Text = escola.EscBairro;
                TB_complemento.Text = escola.EscComplemento;
                TBtelefone.Text = escola.EscTelefone == null ? "" : Funcoes.FormataTelefone(escola.EscTelefone);
                TBEmail.Text = escola.EscEmail;
                TBrepresentante.Text = escola.EscDiretor;
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
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        private void BindMunicipios()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = from i in bd.MA_Municipios select new { MunIEstado = i.MunIEstado };
                var datasource = dados.Distinct().OrderBy(p => p.MunIEstado);
                DD_estado_Nat.DataSource = datasource;
                DD_estado_Nat.DataBind();
            }
        }

        private void Limpadados()
        {
            TBCodEscola.Text = string.Empty;
            TBnomeEsc.Text = string.Empty;
          //  TBcidade.Text = string.Empty;
            DDMunicipio.SelectedValue = "";
            TBEndereco.Text = string.Empty;
            TBCep.Text = string.Empty;
            TBtelefone.Text = string.Empty;
            BindMunicipios();
            TBrepresentante.Text = string.Empty;
            TB_Numero_endereco.Text = string.Empty;
            TB_Bairro.Text = string.Empty;
            TBEmail.Text = string.Empty;
            TBrepresentante.Text = string.Empty;
        }


        protected void BTsalva_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["comando"].Equals("Inserir") && TBCodEscola.Equals(string.Empty)) throw new ArgumentException("Digite o código da escola.");
                if (TBnomeEsc.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome da escola.");
                if (TBEndereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o endereço da escola.");
                if (TB_Numero_endereco.Text.Equals(string.Empty)) throw new ArgumentException("Digite o número do endereço da escola.");
             //   if (TBcidade.Text.Equals(string.Empty)) throw new ArgumentException("Digite a cidade da escola.");
                if (DDMunicipio.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Escolha cidade da escola.");
                if (TB_Bairro.Text.Equals(string.Empty)) throw new ArgumentException("Digite o bairro da escola.");
                if (!TBCep.Text.Equals(string.Empty) && Funcoes.Retirasimbolo(TBCep.Text).Length != 8) throw new ArgumentException("CEP incorreto.");

                if (!TBtelefone.Text.Equals(""))
                    if (!Funcoes.ValidaTelefone(TBtelefone.Text)) throw new ArgumentException("Telefone inválido.");

                using (var repository = new Repository<Escolas>(new Context<Escolas>()))
                {
                    var unidade = Session["comando"].Equals("Inserir") ? new Escolas() : repository.Find(Convert.ToInt32(HFEscolaRef.Value));
                    unidade.EscCodigo =  Session["comando"].Equals("Inserir") ? 0 : Convert.ToInt32(TBCodEscola.Text);
                    unidade.EscNome = TBnomeEsc.Text;
                    unidade.EscTelefone = Funcoes.Retirasimbolo(TBtelefone.Text);
                //    unidade.EscCidade = TBcidade.Text;
                    unidade.EscCidade = DDMunicipio.SelectedValue;

                    unidade.EscEstado = DD_estado_Nat.SelectedValue;

                    unidade.EscEndereco = TBEndereco.Text;
                    unidade.EscNumeroEndereco = TB_Numero_endereco.Text;
                    unidade.EscComplemento = TB_complemento.Text;
                    unidade.EscBairro = TB_Bairro.Text;
                    unidade.EscCEP = Funcoes.Retirasimbolo(TBCep.Text);
                    unidade.EscDiretor = TBrepresentante.Text;
                    unidade.EscEmail = TBEmail.Text;

                    if (Session["comando"].Equals("Inserir")) repository.Add(unidade);
                    else repository.Edit(unidade);
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
                Funcoes.TrataExcessao("000200", ex);
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
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
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
            MultiView1.ActiveViewIndex = 1;
            Limpadados();
            TBCodEscola.ReadOnly = false;
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
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
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
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 1;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void DD_estado_Nat_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencheMunicipio();
        }

        public void PreencheMunicipio()
        {
            var valor = DD_estado_Nat.SelectedValue;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from M in bd.MA_Municipios
                             where M.MunIEstado == valor
                             select new { M.MunIDescricao }).ToList();

                DDMunicipio.DataSource = query;
                DDMunicipio.DataBind();
            }
        }
    }
}