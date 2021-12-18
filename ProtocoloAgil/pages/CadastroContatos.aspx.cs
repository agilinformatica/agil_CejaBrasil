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
    public partial class CadastroContatos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            //if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
                BTinsert.Enabled = false;

              
            }

            if (IsPostBack) return;
            CarregaUsuario();
            CarregaCliente();

            DDCliente.SelectedValue = Request.QueryString["id"];
            txtClientePesquisa.Text = DDCliente.SelectedItem.ToString();



            BindGridView(txtClientePesquisa.Equals(string.Empty) ? 1 : 2);
            //Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTinsert.Text = "Alterar";
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            //TBCodigo_indice.Enabled = false;
            CarregaUsuario();
            CarregaCliente();
          // txtClientePesquisa.Text = Request.QueryString["id"];
            DDCliente.SelectedValue = Request.QueryString["id"];
            CarregaPosicao();
            CarregaTipoContato();
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void PreencheCampos()
        {
            using (var repository = new Repository<CA_Contatos>(new Context<CA_Contatos>()))
            {
                var contatos = repository.Find(int.Parse(Session["Alteracodigo"].ToString()));

                txtCodContato.Text = contatos.CocID.ToString();
                DDCliente.SelectedValue =  contatos.CocCliente.ToString();
                DDTipoContato.SelectedValue = contatos.CocTipo.ToString();
                txtDataContato.Text =  string.Format("{0:dd/MM/yyyy}", contatos.CocDataContato) ;
                DDPosicao.SelectedValue = contatos.CocCodigoFechamento == 0 ? "" : contatos.CocCodigoFechamento.ToString();
                txtDescricaoContato.Text = contatos.CocDescricaocontato;
                txtDataFechamento.Text =  string.Format("{0:dd/MM/yyyy}", contatos.CocDatafechamento) ;
                DDUsuarioFechamento.SelectedValue = contatos.CocUsuarioContato ;
                txtResultadoContato.Text = contatos.CocResultadoContato;
                txtAtendente.Text = contatos.CocRespContato;

                txtDataRetorno.Text = string.Format("{0:dd/MM/yyyy}", contatos.CocPrevisaoRetorno);

            }
        }

        private void BindGridView(int tipo)
        {

            var where = "";

            if (!txtClientePesquisa.Text.Equals(string.Empty))
            {
                where += " and CocCliente = '" + Request.QueryString["id"] + "'";
            }


            var sql = "Select *, T.Tco_Descricao, U.UsuNome  from CA_Contatos C left join CA_Usuarios U on C.CocUsuarioContato = U.UsuCodigo left join CA_TiposContatos T on C.CocTipo = T.Tco_Codigo where 1 = 1 " + where + "";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }



        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {

               // if (txtNomeCliente.Equals(string.Empty)) throw new ArgumentException("Digite o nome do cliente.");
               // if (txtIdentificacao.Text.Equals(string.Empty)) throw new ArgumentException("Digite a identificação.");

                using (var repository = new Repository<CA_Contatos>(new Context<CA_Contatos>()))
                {
                    var contatos = (Session["comando"].Equals("Inserir")) ? new CA_Contatos() : repository.Find(Convert.ToInt16(Session["Alteracodigo"].ToString()));
                    contatos.CocID = (short)((Session["comando"].Equals("Inserir")) ? 0 : Convert.ToInt16(txtCodContato.Text));
                    contatos.CocCliente = int.Parse(DDCliente.SelectedValue);
                    contatos.CocTipo = int.Parse(DDTipoContato.SelectedValue);
                    contatos.CocDataContato = txtDataContato.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataContato.Text);
                    contatos.CocCodigoFechamento = DDPosicao.SelectedValue.Equals(string.Empty) ? 0 : int.Parse(DDPosicao.SelectedValue);
                    contatos.CocDescricaocontato = txtDescricaoContato.Text;
                    contatos.CocDatafechamento = txtDataFechamento.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataFechamento.Text);
                    contatos.CocUsuarioContato = DDUsuarioFechamento.SelectedValue;
                    contatos.CocResultadoContato = txtResultadoContato.Text;
                    contatos.CocRespContato = txtAtendente.Text;

                    contatos.CocPrevisaoRetorno = txtDataRetorno.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataRetorno.Text);


                    if (Session["comando"].Equals("Inserir")) repository.Add(contatos);
                    else repository.Edit(contatos);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Ação realizada com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('" + ex.Message + "')", true);
                return;
            }
            catch (Exception ex)
            {
              //  Funcoes.TrataExcessao("000115", ex);
            }
        }

        protected void BTlimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void LimpaCampos()
        {
            txtCodContato.Text = string.Empty;
           // DDCliente.SelectedValue =  string.Empty;
            DDTipoContato.SelectedValue = string.Empty;
            txtDataContato.Text = string.Empty;
            DDPosicao.SelectedValue = string.Empty;
            txtDescricaoContato.Text = string.Empty; ;
            txtDataFechamento.Text = string.Empty;
            DDUsuarioFechamento.SelectedValue = string.Empty; ;
            txtResultadoContato.Text = string.Empty;
            txtAtendente.Text = string.Empty;
            txtDataRetorno.Text = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            BindGridView(txtClientePesquisa.Text.Equals(string.Empty) ? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            CarregaTipoContato();
            CarregaCliente();
           // txtClientePesquisa.Text = Request.QueryString["id"];
            DDCliente.SelectedValue = Request.QueryString["id"];
            DDCliente.Enabled = false;
            CarregaUsuario();
            CarregaPosicao();
            BTinsert.Text = "Salvar";
            Session["comando"] = "Inserir";
            LimpaCampos();

            // TBCodigo_indice.Enabled = false;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "5";
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
                using (var repository = new Repository<IndicesSociais>(new Context<IndicesSociais>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.IndCodigo + "; " + item.IndDescricao + "; " + string.Format("{0:c}", item.IndValorMaximo)
                            + "; " + string.Format("{0:c}", item.IndValorMaximo);
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Índices Sociais.txt");
                }
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView(txtClientePesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var curso = Convert.ToInt16(button.CommandArgument);
            using (var repository = new Repository<CA_CadastroClientes>(new Context<CA_CadastroClientes>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(curso);
            }
            BindGridView(txtClientePesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(txtClientePesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }


        public void CarregaPosicao()
        {
            // CARREGAR GRID SEM LINQ
            var sql = "SELECT * FROM CA_fechamentosContatos ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDPosicao.DataSource = datasource;
            DDPosicao.DataBind();
        }


       

        public void CarregaUsuario()
        {
            // CARREGAR GRID SEM LINQ
            var sql = "SELECT * FROM Ca_Usuarios ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDUsuarioFechamento.DataSource = datasource;
            DDUsuarioFechamento.DataBind();
        }

        public void CarregaCliente()
        {
            // CARREGAR GRID SEM LINQ
            var sql = "select CacCodigo, CacNome from CA_CadastroClientes ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };
            DDCliente.DataSource = datasource;
            DDCliente.DataBind();

            //DDClientePesquisa.DataSource = datasource;
            //DDClientePesquisa.DataBind();
        }

        public void CarregaTipoContato()
        {
            // CARREGAR GRID SEM LINQ
            var sql = "select Tco_Codigo, Tco_Descricao from CA_TiposContatos ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };
            DDTipoContato.DataSource = datasource;
            DDTipoContato.DataBind();
        }

        protected void DDCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sql = "Select CacAtendente from CA_CadastroClientes where CacCodigo = "+DDCliente.SelectedValue+"";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                txtAtendente.Text = result["CacAtendente"].ToString();
            }
        }


        protected void btnVoltarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroClientes.aspx?id=" + txtClientePesquisa.Text + "");
        }
    }
}