using System;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;

namespace ProtocoloAgil.pages
{
    public partial class CadastroOpcao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOpcoes();
                btn_next_final.Visible = false;
                Session["comando"] = "Incluir";
            }
        }


        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void BindOpcoes()
        {
            var questao = Criptografia.Decrypt(Request.QueryString["meta"], GetConfig.Key());
            var sqlquery = "SELECT OpcCodigo,OpcTexto,  OpcOrdemExibicao from dbo.CA_Opcoes where  OpcQuestao = " + questao;
            var datasource = new SqlDataSource { ID = "SDS_pesquisas", ConnectionString = GetConfig.Config(), SelectCommand = sqlquery };
            datasource.Selected += SqlDataSource1_Selected;
            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }


        protected void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_numero.Text.Equals(string.Empty)) throw new ArgumentException("Informe o identificador da opção.");
                if (tb_nome_opcao.Text.Equals(string.Empty)) throw new ArgumentException("Informe o texto da opção.");
                using (var repository = new Repository<Opcao>(new Context<Opcao>()))
                {
                    var questao =  Criptografia.Decrypt(Request.QueryString["meta"], GetConfig.Key());
                    if (!Session["comando"].Equals("Alterar"))
                    {
                        var cadastrado = repository.All().Where(p => p.OpcOrdemExibicao == int.Parse(tb_numero.Text)
                                                                     && p.OpcQuestao == int.Parse(questao));
                        if (cadastrado.Count() > 0)
                            throw new ArgumentException("Uma opção com este identificador já foi cadastrada.");
                    }

                    var opcao = (Session["comando"].Equals("Alterar"))
                                    ? repository.Find(int.Parse(Session["Alteracodigo"].ToString()), int.Parse(questao))
                                    : new Opcao();

                    opcao.OpcOrdemExibicao = int.Parse(tb_numero.Text);
                    opcao.OpcTexto = tb_nome_opcao.Text;
                    opcao.OpcQuestao = int.Parse(questao);
                    opcao.OpcNota = short.Parse(tb_nota.Text);

                    if (Session["comando"].Equals("Alterar")) repository.Edit(opcao);
                    else repository.Add(opcao);
                }
                BindOpcoes();
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000020", ex);
            }
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            using (var repository = new Repository<Opcao>(new Context<Opcao>()))
            {
                var bt = (ImageButton)sender;
                var codigoQuestao = bt.CommandArgument;
                var questao = Criptografia.Decrypt(Request.QueryString["meta"], GetConfig.Key());
                if (bool.Parse(HFConfirma.Value))
                    repository.Remove(int.Parse(codigoQuestao), int.Parse(questao));
                BindOpcoes();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView) sender).SelectedRow;
            using (var repository = new Repository<Opcao>(new Context<Opcao>()))
            {
                var questao = Criptografia.Decrypt(Request.QueryString["meta"], GetConfig.Key());
                var cadastrado = repository.All().Where(p => p.OpcOrdemExibicao == int.Parse(row.Cells[0].Text)
                                                             && p.OpcQuestao == int.Parse(questao)).First();

                tb_numero.Text = cadastrado.OpcOrdemExibicao.ToString();
                tb_nome_opcao.Text = cadastrado.OpcTexto;
                tb_nota.Text = cadastrado.OpcNota.ToString();

                Session["Alteracodigo"] = cadastrado.OpcCodigo;
                Session["comando"] = "Alterar";
                btn_next_final.Visible = true;
            }
        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            var target = Criptografia.Decrypt(Request.QueryString["target"], GetConfig.Key());
            var url = target + "?acs=" + Request.QueryString["acs"];
            using (var repository = new Repository<Questao>(new Context<Questao>()))
            {
                var questao = Criptografia.Decrypt(Request.QueryString["meta"], GetConfig.Key());
                var pesquisa = repository.All().Where(p => p.QueCodigo == int.Parse(questao)).First().QuePesquisa;
                Session["option"] = Criptografia.Decrypt(Request.QueryString["id"], GetConfig.Key());
                Session["code_delivered"] = Criptografia.Encrypt(pesquisa.ToString(), GetConfig.Key());
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "javascript:opener.change('" + url + "'); this.window.close();", true);
            }
        }

        protected void btn_next_final_Click1(object sender, EventArgs e)
        {
            LimpaCampos();
            Session["comando"] = "Incluir";
        }

        private void LimpaCampos()
        {
            tb_numero.Text = string.Empty;
            tb_nome_opcao.Text = string.Empty;
        }
    }
}