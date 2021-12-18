using System;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class PopupCadastroEscolas : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Cadastro de Escola(s) - Requerimento Web";
            if (!IsPostBack)
            {
                BindMunicipios();
            }
        }

        protected void IndiceZeroUF(object sender, EventArgs e)
        {
            var indice0 = new ListItem("--", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
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
                    var unidade =  new Escolas() ;
                    //unidade.EscCodigo = 0 ;
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

                    repository.Add(unidade);
                    
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

        protected void BTLimpa_Click(object sender, EventArgs e)
        {
            var target = Criptografia.Decrypt(Request.QueryString["target"], GetConfig.Key());
            Session["option"] = Criptografia.Decrypt(Request.QueryString["id"], GetConfig.Key());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "javascript:opener.change('" + target + "'); this.window.close();", true);
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
    }
}