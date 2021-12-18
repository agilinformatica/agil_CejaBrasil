using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class MultiviewUsuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if (!IsPostBack)
            {
                AcessoUsuarios();
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
                if (Session["option"] != null)
                {
                    using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                    {
                        switch (Session["option"].ToString())
                        {
                            case "1":
                                var escola =  Criptografia.Decrypt(Session["code_delivered"].ToString(), GetConfig.Key()).Split('_').ToList();
                                string text = escola.Aggregate("", (current, codigo) => current + (codigo + ", "));
                                TB_codigo_campus.Text = text.Substring(0, text.Length - 2);

                                LB_campus.Text = escola.Count == 1
                                    ? (from i in bd.CA_Unidades where i.UniCodigo.Equals(escola.First()) select i.UniNome).First()
                                    :"Vários Selecionados";
                                CarregaOpcoes();
                                MultiView1.ActiveViewIndex = 1;
                            break;
                        }
                    }

                    if (Session["comando"] != null && Session["comando"].Equals("Alterar")
                        && Session["Alteracodigo"] != null && !Session["Alteracodigo"].ToString().Equals(string.Empty))
                    {
                        var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
                        var usuario = (from i in bd.CA_Usuarios where i.UsuCodigo.Equals(Session["Alteracodigo"]) select i).First();
                        TBNome.Text = usuario.UsuNome;
                        TBCodigo.Text = usuario.UsuCodigo;
                        TB_email.Text = usuario.UsuEmail;
                        DD_tipo.SelectedValue = usuario.UsuTipo ?? "";
                    }
                    else
                    {
                        //ativa o cadastro de senha. 
                        TBSenha1.Enabled = true;
                        TBSenha2.Enabled = true;
                    }
                }
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
                BTinsert.Enabled = false;
            }
        }

        private void AcessoUsuarios()
        {
            using (var repository = new Repository<Usuarios>(new Context<Usuarios>()))
            {
                var tipo = repository.Find(Session["codigo"].ToString()).UsuTipo;
                if (tipo.Equals("G"))
                {
                    BindGridView(pesquisa.Text.Equals(string.Empty) ? 3 : 4);
                }
                else
                {
                    BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = GridView1.SelectedRow;
            AcessoUsuarios();
            Session["comando"] = "Alterar";
            TBCodigo.Enabled = false;
            Session["Alteracodigo"] = WebUtility.HtmlDecode(row.Cells[0].Text);
            MultiView1.ActiveViewIndex = 1;
            PreencheCampos();
        }

        private void PreencheCampos()
        {
            CarregaOpcoes();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                //carrega os dados do usuário
                var usuario = (from i in bd.CA_Usuarios where i.UsuCodigo.Equals(Session["Alteracodigo"]) select i).First();

                if(!usuario.UsuTipo.Equals("U") && DD_tipo.Items.Count <= 2)
                {

                    MultiView1.ActiveViewIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('Seu perfil não permite realizar alterações neste usuário.')", true);
                    return;
                }
                TBCodigo.Text = usuario.UsuCodigo;
                TBNome.Text = usuario.UsuNome;
                TB_email.Text = usuario.UsuEmail;
                DD_tipo.SelectedValue = usuario.UsuTipo;

                //Carrega quais escolas pode acessar
                var permissoes = from i in bd.CA_UsuarioUnidades
                                 where i.UnicodigoUsuario.Equals(Session["Alteracodigo"])
                                 select i.UniCodigoUnidade;
                string text = "";
                if (permissoes.Count() > 0)
                {
                    text = Enumerable.Aggregate(permissoes, text, (current, codigo) => current + (codigo + ", "));
                }
                TB_codigo_campus.Text = permissoes.Count() == 0 ? "" : text.Substring(0, text.Length - 2);

                LB_campus.Text = permissoes.Count() == 1
                    ? (from i in bd.CA_Unidades where i.UniCodigo.Equals(permissoes.First()) select i.UniNome).First()
                    : (permissoes.Count() == 0 ? "" : "Vários Selecionados");
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Page")) return;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];

            switch (e.CommandName)
            {   
                case "Reset":
                    var cn = new Conexao();
                    try
                    {
                        cn.Alterar("UPDATE CA_Usuarios SET  UsuSenha ='1' WHERE UsuCodigo='" + row.Cells[0].Text + "' ");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Senha Resetada. Troque a senha no proximo acesso.')", true);
                    }
                    catch (Exception ex)
                    {
                        Funcoes.TrataExcessao("000117", ex);
                    }
                    break;
            }
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBCodigo.Text.Equals(string.Empty)) throw new ArgumentException("Campo Código Não pode ser Vazio.");
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Campo Nome Não pode ser Vazio.");
                if (TB_email.Text.Equals(string.Empty)) throw new ArgumentException("Digite uma e-mail para o Usuário.");
                if (TB_codigo_campus.Text.Equals(string.Empty)) throw new ArgumentException("Campus não pode ser Vazio.");
                if (!Funcoes.ValidaEmail(TB_email.Text)) throw new ArgumentException("E-mail inválido.");
                if (Session["comando"].Equals("Inserir"))
                {
                    if (TBSenha1.Text.Equals(string.Empty)) throw new ArgumentException("Digite uma senha para o Usuário.");
                    if (TBSenha1.Text != TBSenha2.Text) throw new ArgumentException("Senha e confirmação de senha devem ser iguais.");
                }

                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var usuariorepetido = (from i in bd.CA_Usuarios where i.UsuCodigo.Equals(TBCodigo.Text) select i);
                    if (usuariorepetido.Count() > 0 && Session["comando"].Equals("Inserir")) throw new Exception("Codigo já cadastrado. Por Favor, escolha outro código.");
                }

                using (var repository = new Repository<Usuarios>(new Context<Usuarios>()))
                {
                    var usuario = Session["comando"].Equals("Inserir") ? new Usuarios() : repository.Find(Session["Alteracodigo"].ToString());
                    usuario.UsuCodigo = TBCodigo.Text;
                    usuario.UsuNome = TBNome.Text;
                    usuario.UsuEmail = TB_email.Text;
                    usuario.UsuTipo = DD_tipo.SelectedValue;
                    if (Session["comando"].Equals("Inserir"))
                    {
                        usuario.UsuSenha = TBSenha1.Text;
                        repository.Add(usuario);
                    }
                    else
                    {
                        repository.Edit(usuario);
                    }
                }
                AdicionarPermissao();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                "alert('Ação realizada com sucesso.')", true);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000118", ex);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('" + ex.Message + ".')", true);
            }
        }

        private void AdicionarPermissao()
        {
            /* Deleta todas as permissões do usuário e reinsere as que estiverem no textbox, 
               evitando que o sistema cadastre a mesma permissão mais de uma vez */
            try
            {
                var con = new Conexao();
                var codigos = TB_codigo_campus.Text.Split(',');
                var sql = "Delete from  CA_UsuarioUnidade where UnicodigoUsuario= '" + TBCodigo.Text + "'";
                con.Alterar(sql);
                foreach (var codigo in codigos)
                {
                    var sql2 = "INSERT INTO CA_UsuarioUnidade VALUES (" + codigo + ", '" + TBCodigo.Text + "')";
                     con.Alterar(sql2);
                }
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000180", ex);
            }
        }


        protected void BTLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void LimpaCampos()
        {
            TBNome.Text = string.Empty;
            TBCodigo.Text = string.Empty;
            TBSenha1.Text = string.Empty;
            TBSenha2.Text = string.Empty;
            TB_codigo_campus.Text = string.Empty;
            TB_email.Text = string.Empty;
            //DD_tipo.SelectedValue = string.Empty;
            LB_campus.Text = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            AcessoUsuarios();
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            Session["comando"] = "Inserir";
            TBCodigo.Enabled = true;

            //ativa o cadastro de senha. 
            TBSenha1.Enabled = true;
            TBSenha2.Enabled = true;

            LimpaCampos();
            CarregaOpcoes();
            MultiView1.ActiveViewIndex = 1;
        }

        private void CarregaOpcoes()
        {
            var itens = new List<ListItem>();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var tipo = (from i in bd.CA_PerfilUsuarios select new {i.PerfCodigo, i.PerfDescricao });

                DD_tipo.DataSource = tipo.ToList();
                DD_tipo.DataBind();


                //switch (tipo)
                //{
                //    case "A" :
                //        itens.Add(new ListItem("Selecione", ""));
                //        itens.Add(new ListItem("Administrador","A"));
                //        itens.Add(new ListItem("Recrutamento", "R"));
                //        itens.Add(new ListItem("Pedagogico", "P"));
                //        itens.Add(new ListItem("Usuário", "U"));
                //        break;
                //    case "R":
                //        itens.Add(new ListItem("Selecione", ""));
                //         itens.Add(new ListItem("Usuário", "U"));
                //        break;

                //    case "P": case "U":
                //        itens.Add(new ListItem("Selecione", ""));
                //        BTinsert.Enabled = false;
                //        break;
                //}
                //DD_tipo.Items.Clear();

                //foreach (var item in itens)
                //{
                //    DD_tipo.Items.Add(item);
                //}
                //DD_tipo.DataBind();
            }
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "7";
            MultiView1.ActiveViewIndex = 2;
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void texto_Click(object sender, EventArgs e)
        {
            String filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            var cn = new Conexao();
            var dr = cn.Consultar("SELECT * FROM MA_Usuarios ORDER BY UsuNome");
            try
            {
                while (dr.Read())
                {
                    var linha = dr["Usunome"] + ";" + dr["UsuCodigo"] + ";" + dr["UsuSenha"] + ";" + dr["UsuTipo"];
                    write.Escreve(linha);
                }

                // download do arquivo de texto
                var fileName = filePath + @"/temp.txt";
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=Lista_de_Usuarios.txt");
                Response.WriteFile(fileName);
                Response.Flush();
                Response.Close();
            }
            catch (IOException)
            {
                Response.Redirect("ErrorPage.aspx?Erro=000119", false); 
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            AcessoUsuarios();
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            var url = Regex.Split(Request.Url.ToString(), ".aspx");
            var name = Regex.Split(url.First(), "pages/").Last(); 
            var tipo = "popup('popup_escolas.aspx?" +
                      "id=" + Criptografia.Encrypt("1", GetConfig.Key()) +
                      "&meta=" + Criptografia.Encrypt(TBCodigo.Text, GetConfig.Key()) +
                        "&target=" + Criptografia.Encrypt(name + ".aspx", GetConfig.Key()) +
                      "&acs=" + Criptografia.Encrypt(Session["tipoacesso"].ToString(), GetConfig.Key()) + "', '600',450);";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), tipo, true);
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var aprendiz = button.CommandArgument;
            using (var repository = new Repository<Usuarios>(new Context<Usuarios>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(aprendiz);
            }
           AcessoUsuarios();
        }

        private void BindGridView(int type)
        {
            /* Regras: 
             * Administradores podem acessar todos os usuários. 
             * Gestores: Apenas os Usuários.
             * Suporte: Sem acesso à esta tela.
             * Usuários: Sem acesso à esta tela.
             */

            using (var repository = new Repository<Usuarios>(new Context<Usuarios>()))
            {
                var datasource = new List<Usuarios>();
                switch (type)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.UsuNome)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.UsuNome.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.UsuNome)); break;
                    case 3: datasource.AddRange(repository.All().Where(m => m.UsuTipo.Equals("U")).OrderBy(p => p.UsuNome)); break;
                    case 4: datasource.AddRange(repository.All().Where(m => m.UsuTipo.Equals("U"))
                        .Where(p => p.UsuNome.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.UsuNome)); break;
                }
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataSource = datasource;
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
           AcessoUsuarios();
        }
    }
}