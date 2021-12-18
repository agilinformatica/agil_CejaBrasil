using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;

namespace ProtocoloAgil.pages
{
    public partial class CadastroGrauEscolaridade : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(btn_texto);
            if (!IsPostBack)
            {
                BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
               Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
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

         private void BindGridView(int tipo)
         {
            using (var repository = new Repository<Escolaridade>(new Context<Escolaridade>()))
            {
                var datasource = new List<Escolaridade>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.GreDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.GreDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.GreDescricao)); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o Nome/Descrição do grau de escolaridade.");
                using (var repository = new Repository<Escolaridade>(new Context<Escolaridade>()))
                {
                    var escolaridade = Session["comando"].Equals("Inserir") ? new Escolaridade() : repository.Find(Convert.ToInt32(Session["AlteraCodigo"]));
                    escolaridade.GreCodigo = Session["comando"].Equals("Inserir") ? 0 : Convert.ToInt32(Session["AlteraCodigo"]);
                    escolaridade.GreDescricao = TBNome.Text;
                    if (Session["comando"].Equals("Inserir")) repository.Add(escolaridade);
                    else repository.Edit(escolaridade);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação Realizada com Sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000020", ex);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {  
            
            var row = ((GridView) sender).SelectedRow;
            Session["AlteraCodigo"] = WebUtility.HtmlDecode(row.Cells[0].Text);
            PreencheCampos();
            TBcodigo.Enabled = false;
            Session["comando"] = "Alterar";
            MultiView1.ActiveViewIndex = 1;
        }

        private void PreencheCampos()
        {
            using (var repository = new Repository<Escolaridade>(new Context<Escolaridade>()))
            {
                var escolaridade = repository.Find(Convert.ToInt32(Session["AlteraCodigo"] ));
                TBNome.Text = escolaridade.GreDescricao;
                TBcodigo.Text = escolaridade.GreCodigo.ToString();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btn_novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            TBcodigo.Enabled = false;
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 1;
        }

        private void LimpaCampos()
        {
            TBcodigo.Text = string.Empty;
            TBNome.Text = string.Empty;
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_texto_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            var cn = new Conexao();
            var dr = cn.Consultar("select  GreCodigo , GreDescricao from dbo.CA_GrauEscolaridade");
            try
            {
                while (dr.Read())
                {
                    string linha = dr["GreCodigo"] + ";" + dr["GreDescricao"];
                    write.Escreve(linha);
                }
                // download do arquivo de texto
                string fileName = filePath + @"/temp.txt";
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=Tabela_de_Grau_de_Escolaridade.txt");
                Response.WriteFile(fileName);
                Response.Flush();
                Response.Close();
            }
            catch (IOException ex)
            {
                 Funcoes.TrataExcessao("000021", ex);
            }
        }

        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 4;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var aprendiz = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<Escolaridade>(new Context<Escolaridade>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(aprendiz);
            }
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }


    }
}