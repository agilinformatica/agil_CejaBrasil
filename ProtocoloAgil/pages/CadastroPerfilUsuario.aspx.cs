using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class CadastroPerfilUsuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            var scriptManager = ScriptManager.GetCurrent(Page);
          //  if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if ( Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
                BTinsert.Enabled = false;
            }

            if (IsPostBack) return;
            BindGridView();
            Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = GridView1.SelectedRow;
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = WebUtility.HtmlDecode(row.Cells[0].Text);
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void BindGridView()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.CA_PerfilUsuarios.ToList();
                var datasource =  new ArrayList();
                GridView1.DataSource = dados;
                GridView1.DataBind();
            }
        }


        private void PreencheCampos()
        {
            LimpaCampos();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var perfil = bd.CA_PerfilUsuarios.Where(p => p.PerfCodigo.Equals(Session["Alteracodigo"].ToString()));

                if (perfil.Count() == 0) return;
                var dados = perfil.First();
                TBCodigo.Text = dados.PerfCodigo.ToString();
                TBDescricao.Text = dados.PerfDescricao;
                TBCodigo.Enabled = false;
            }
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBCodigo.Text.Equals(string.Empty)) throw new ArgumentException("Informe código do perfil.");
                if (TBDescricao.Text.Equals(string.Empty)) throw new ArgumentException("Informe a descrição do perfil.");

                const string sqlinsert = "INSERT INTO dbo.CA_PerfilUsuario VALUES(@CodPerfil,@DescPerfil)";
                const string sqlupdate = "UPDATE dbo.CA_PerfilUsuario  SET PerfDescricao = @DescPerfil WHERE PerfCodigo = @CodPerfil ";

                var parameters = new List<SqlParameter> { 
                                         new SqlParameter("CodPerfil", TBCodigo.Text), 
                                         new SqlParameter("DescPerfil", TBDescricao.Text) };

                var con = new Conexao();
                con.Alterar(Session["comando"].Equals("Inserir") ? sqlinsert : sqlupdate,parameters.ToArray() );
  
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Ação realizada com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('"+ ex.Message+"')", true);
                return;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                    "alert('ERRO - Solicitação NÃO relizada. Código já cadastrado');", true);
            }
           
        }

        protected void BTlimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }


        private void LimpaCampos()
        {
            TBCodigo.Text = string.Empty;
            TBDescricao.Text = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            BindGridView();
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            TBCodigo.Enabled = true;
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 1;
        }


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
           BindGridView();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var perfil = button.CommandArgument;
            using (var repository = new Repository<PerfilUsuario>(new Context<PerfilUsuario>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(perfil);
            }
            BindGridView();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}