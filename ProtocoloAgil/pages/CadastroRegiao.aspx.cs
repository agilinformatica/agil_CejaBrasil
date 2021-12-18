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
    public partial class CadastroRegiao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if ( Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
                BTinsert.Enabled = false;
            }

            if (IsPostBack) return;
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
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

        private void BindGridView(int tipo)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.CA_Regioes.ToList();
                var datasource =  new ArrayList();
                switch (tipo)
                {
                    case 1: datasource.AddRange(dados.OrderBy(p => p.DescRegiao).ToList()); break;
                    case 2: datasource.AddRange(dados.Where(p => p.DescRegiao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.DescRegiao).ToList()); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }


        private void PreencheCampos()
        {
            LimpaCampos();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var regiao = bd.CA_Regioes.Where(p => p.CodRegiao.Equals(Session["Alteracodigo"].ToString()));

                if (regiao.Count() == 0) return;
                var dados = regiao.First();
                TBCodigo.Text = dados.CodRegiao.ToString();
                TBDescricao.Text = dados.DescRegiao;
            }
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBDescricao.Text.Equals(string.Empty)) throw new ArgumentException("Informe o nome da região.");

                const string sqlinsert = "INSERT INTO dbo.CA_Regioes VALUES(@DescRegiao)";
                const string sqlupdate = "UPDATE dbo.CA_Regioes  SET DescRegiao = @DescRegiao WHERE CodRegiao = @CodRegiao ";

                var parameters = new List<SqlParameter> { 
                                         new SqlParameter("CodRegiao", TBCodigo.Text), 
                                         new SqlParameter("DescRegiao", TBDescricao.Text) };

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
                Funcoes.TrataExcessao("000215", ex);
            }
        }

        protected void BTlimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        private void LimpaCampos()
        {
            TBCodigo.Text = string.Empty;
            TBCodigo.Text = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "70";
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
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var dados = bd.CA_Regioes.ToList();
                    foreach (var item in dados)
                    {
                        var linha = item.CodRegiao + "; " + item.DescRegiao + "; ";
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Região.txt");
                }
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
           BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var regiao = button.CommandArgument;
            using (var repository = new Repository<Regioes>(new Context<Regioes>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(int.Parse(regiao));
            }
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }
    }
}