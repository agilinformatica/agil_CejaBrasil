using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.Globalization;
using System.Drawing;


namespace ProtocoloAgil.pages
{
    public partial class CadastroClientes : Page
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
            CarregaStatus();
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
           // Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTinsert.Text = "Alterar";
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            //TBCodigo_indice.Enabled = false;

           

            txtDataAlteracao.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Today);
            DDUsuarioCadastro.Enabled = false;
            DDUsuarioAlteracao.Enabled = true;
            CarregaUsuario();
            CarregaEstado();
            CarregaStatus();

            DDUsuarioAlteracao.Enabled = false;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void PreencheCampos()
        {
            using (var repository = new Repository<CA_CadastroClientes>(new Context<CA_CadastroClientes>()))
            {
                var cliente = repository.Find(int.Parse(Session["Alteracodigo"].ToString()));

                txtCodCliente.Text = cliente.CacCodigo.ToString();
                txtNomeCliente.Text = cliente.CacNome;
             //   txtIdentificacao.Text = cliente.CacIdentificacao;
             //   txtCEP.Text = cliente.CacCEP;
               // DDTipo.SelectedValue = cliente.CacTipo.ToString();
              //  txtNumeroEndereco.Text = cliente.CacNumeroEndereco;
               // txtComplemento.Text = cliente.CacComplemento;
               // txtEndereco.Text = cliente.CacEndereco;
               // txtBairro.Text = cliente.CacBairro;
                
                txtMunicipio.Text = cliente.CacCidade;
                DDEstado.SelectedValue = cliente.CacEstado;
                txtTelefone.Text = Funcoes.FormataTelefone(cliente.CacTelefone);
                txtCelular.Text = Funcoes.FormataTelefoneSaoPaulo(cliente.CacCelular);
                txtEmail.Text = cliente.CacEmail;
               // txtDataNascimento.Text = string.Format("{0:dd/MM/yyyy}", cliente.CacDataNascimento);
                DDAtendente.SelectedValue = cliente.CacAtendente;
                txtDataCadastro.Text = string.Format("{0:dd/MM/yyyy}", cliente.CacDataCadastro);
                DDUsuarioCadastro.SelectedValue = cliente.CacUsuarioCadastro;
                txtDataAlteracao.Text = string.Format("{0:dd/MM/yyyy}", cliente.CacDataAlteracao);
                DDUsuarioAlteracao.SelectedValue = cliente.CacUsuarioAlteracao;

                txtValorTaxa.Text = cliente.CacValorTaxa == null ? string.Format("{0:f2}", 0) : string.Format("{0:0,0.00}", cliente.CacValorTaxa);
                txtValorEconomia.Text = cliente.CacValorEcomonia == null ? string.Format("{0:f2}", 0) : string.Format("{0:0,0.00}", cliente.CacValorEcomonia);
                txtDataVencimentoCOntratoAtual.Text = string.Format("{0:dd/MM/yyyy}", cliente.CacVencimentoContratoAtual);

                txtConcorrente.Text = cliente.CacConcorrente;
                txtQuantidadeAprendiz.Text = cliente.CacquantidadeAprendiz.ToString();
                DDStatus.SelectedValue = cliente.CacStatus == null ? "" : cliente.CacStatus.ToString();

                txtResponsavelRH.Text = cliente.CacResponsavelRH;

            }
        }

        private void BindGridView(int tipo)
        {

            var where = "";

            if (!DDAtendentePesquisa.SelectedValue.Equals(string.Empty))
            {
                where += " and CacAtendente = '" + DDAtendentePesquisa.SelectedValue + "'";
            }

            if (!pesquisa.Text.Equals(string.Empty))
            {
                where += " and CacNome Like '%" + pesquisa.Text + "%'";
            }


            var sql = "Select *, S.StcCodigo, S.StcDescricao from CA_CadastroClientes C left join CA_StatusCliente S on C.CacStatus = S.StcCodigo where 1 = 1 " + where + "";



            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }



        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtNomeCliente.Equals(string.Empty)) throw new ArgumentException("Digite o nome do cliente.");
                // if (txtIdentificacao.Text.Equals(string.Empty)) throw new ArgumentException("Digite a identificação.");

                using (var repository = new Repository<CA_CadastroClientes>(new Context<CA_CadastroClientes>()))
                {
                    var cliente = (Session["comando"].Equals("Inserir")) ? new CA_CadastroClientes() : repository.Find(Convert.ToInt16(Session["Alteracodigo"].ToString()));
                    cliente.CacCodigo = (short)((Session["comando"].Equals("Inserir")) ? 0 : Convert.ToInt16(txtCodCliente.Text));
                    cliente.CacNome = txtNomeCliente.Text;
                 //   cliente.CacIdentificacao = txtIdentificacao.Text;
                    cliente.CacTipo = 2;
                   // cliente.CacCEP = txtCEP.Text;
                   // cliente.CacNumeroEndereco = txtNumeroEndereco.Text;
                   // cliente.CacComplemento = txtComplemento.Text;
                   // cliente.CacEndereco = txtEndereco.Text;
                   // cliente.CacBairro = txtBairro.Text;
                    cliente.CacCidade = txtMunicipio.Text;
                    cliente.CacEstado = DDEstado.SelectedValue;
                    cliente.CacTelefone = Funcoes.Retirasimbolo(txtTelefone.Text);
                    cliente.CacCelular = Funcoes.Retirasimbolo(txtCelular.Text);
                    cliente.CacEmail = txtEmail.Text;
                   // cliente.CacDataNascimento = txtDataNascimento.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataNascimento.Text);
                    cliente.CacAtendente = DDAtendente.SelectedValue;
                    cliente.CacDataCadastro = txtDataCadastro.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataCadastro.Text);
                    cliente.CacUsuarioCadastro = DDUsuarioCadastro.SelectedValue;
                    cliente.CacDataAlteracao = txtDataAlteracao.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataAlteracao.Text);
                    cliente.CacUsuarioAlteracao = DDUsuarioAlteracao.SelectedValue;

                    cliente.CacValorTaxa = double.Parse(txtValorTaxa.Text);
                    cliente.CacquantidadeAprendiz = short.Parse(txtQuantidadeAprendiz.Text);
                    cliente.CacConcorrente = txtConcorrente.Text;
                    cliente.CacStatus = DDStatus.SelectedValue.Equals(string.Empty) ? (short?)null : short.Parse(DDStatus.SelectedValue);


                    cliente.CacVencimentoContratoAtual = txtDataVencimentoCOntratoAtual.Text.Equals(string.Empty) ? (DateTime?)null : Convert.ToDateTime(txtDataVencimentoCOntratoAtual.Text);
                    cliente.CacValorEcomonia = txtValorEconomia.Text.Equals(string.Empty) ? 0 : double.Parse(txtValorEconomia.Text);

                    cliente.CacResponsavelRH = txtResponsavelRH.Text;

                    if (Session["comando"].Equals("Inserir"))
                    {
                        cliente.CacDataCadastro = DateTime.Today;
                        repository.Add(cliente);
                    }
                    else
                    {
                        cliente.CacDataAlteracao = DateTime.Today;
                        cliente.CacUsuarioAlteracao = Session["CodInterno"].ToString();
                        repository.Edit(cliente);
                    }
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
            //TBCodigo_indice.Text = string.Empty;
            //TBNome_indice.Text = string.Empty;
            //TB_maximo_indice.Text = string.Empty;
            //TB_minimo_indice.Text = string.Empty;
            txtResponsavelRH.Text = string.Empty;
           // txtBairro.Text = string.Empty;
            txtCelular.Text = string.Empty;
           // txtCEP.Text = string.Empty;
            txtCodCliente.Text = string.Empty;
          //  txtComplemento.Text = string.Empty;
            txtDataAlteracao.Text = string.Empty;
            txtDataCadastro.Text = string.Empty;
            txtMunicipio.Text = string.Empty;
          //  txtNumeroEndereco.Text = string.Empty;
            txtTelefone.Text = string.Empty;
         //   txtEndereco.Text = string.Empty;
         //   txtDataNascimento.Text = string.Empty;
            txtNomeCliente.Text = string.Empty;
         //   txtIdentificacao.Text = string.Empty;
            //DDTipo.SelectedValue = "1";
            txtEmail.Text = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            DDUsuarioCadastro.Enabled = true;
            DDUsuarioAlteracao.Enabled = false;

            txtQuantidadeAprendiz.Text = "0";
            txtValorTaxa.Text = "0";

            CarregaStatus();
            CarregaEstado();
            CarregaUsuario();
            LimpaCampos();
            BTinsert.Text = "Salvar";
            Session["comando"] = "Inserir";
            LimpaCampos();
            txtDataCadastro.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Today);
            DDUsuarioCadastro.SelectedValue = Session["CodInterno"].ToString();
            DDUsuarioCadastro.Enabled = false;

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
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void txtCEP_TextChanged(object sender, EventArgs e)
        {
            //var sql = "Select * from Ca_CodigosCep where Cep_Codigo = " + Funcoes.Retirasimbolo(txtCEP.Text) + "";
            //var con = new Conexao();
            //var result = con.Consultar(sql);

            //while (result.Read())
            //{
            // //   txtEndereco.Text = result["Cep_Logradouro"].ToString();
            //  //  txtBairro.Text = result["Cep_Bairro"].ToString();
            //    DDEstado.SelectedValue = result["Cep_UF"].ToString();
            //    txtMunicipio.Text = result["Cep_Cidade"].ToString();
            //    // DDMunicipioEndereco.SelectedValue = "SAO PAULO";
            //}
        }



        public void CarregaEstado()
        {
            // CARREGAR GRID SEM LINQ
            var sql = "SELECT MunIEstado FROM MA_Municipios  Group BY MunIEstado ORDER BY MunIEstado";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDEstado.DataSource = datasource;
            DDEstado.DataBind();
        }

        public void CarregaUsuario()
        {
            // CARREGAR GRID SEM LINQ
            var sql = "SELECT * FROM Ca_Usuarios ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDUsuarioCadastro.DataSource = datasource;
            DDUsuarioCadastro.DataBind();

            DDUsuarioAlteracao.DataSource = datasource;
            DDUsuarioAlteracao.DataBind();

            DDAtendente.DataSource = datasource;
            DDAtendente.DataBind();

            DDAtendentePesquisa.DataSource = datasource;
            DDAtendentePesquisa.DataBind();
        }

        public void CarregaStatus()
        {
            // CARREGAR GRID SEM LINQ
            var sql = "SELECT * FROM CA_StatusCliente ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDStatus.DataSource = datasource;
            DDStatus.DataBind();

         
        }

        protected void btnContato_Click(object sender, ImageClickEventArgs e)
        {

            var button = (ImageButton)sender;
            var codCliente = Convert.ToInt16(button.CommandArgument);


            Response.Redirect("CadastroContatos.aspx?id=" + codCliente + "");

            //using (var repository = new Repository<CA_CadastroClientes>(new Context<CA_CadastroClientes>()))
            //{
            //    if (Convert.ToBoolean(HFConfirma.Value))
            //        repository.Remove(curso);
            //}
            //BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text.Equals("1"))
                {
                    e.Row.Cells[3].BackColor = Color.FromName("red");
                }

                if (e.Row.Cells[4].Text.Equals("2"))
                {
                    e.Row.Cells[3].BackColor = Color.FromName("green");
                }

                if (e.Row.Cells[4].Text.Equals("3"))
                {
                    e.Row.Cells[3].BackColor = Color.FromName("blue");
                }

                if (e.Row.Cells[4].Text.Equals("4"))
                {
                    e.Row.Cells[3].BackColor = Color.FromName("yellow");
                }

                if (e.Row.Cells[4].Text.Equals("5"))
                {
                    e.Row.Cells[3].BackColor = Color.FromName("black");
                }
                if (e.Row.Cells[4].Text.Equals("6"))
                {
                    e.Row.Cells[3].BackColor = Color.FromName("gray");
                }

            }
        }

    }
}