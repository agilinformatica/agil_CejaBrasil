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
using MenorAprendizWeb.Base;
using System.Drawing;

namespace ProtocoloAgil.pages
{
    public partial class CadastroStatusCliente : Page
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

        private void BindGridView(int type)
        {
            using (var repository = new Repository<StatusCliente>(new Context<StatusCliente>()))
            {
                var datasource = new List<StatusCliente>();
                switch (type)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.StcCodigo)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.StcDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.StcDescricao)); break;
                }
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataSource = datasource;
                GridView1.DataBind();
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

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                var cn = new Conexao();
                var sql = GeraSql();
                cn.Alterar(sql);
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

        private string GeraSql()
        {
            if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o grau de paretesco.");

            string sqlupdate = "UPDATE CA_StatusCliente SET StcDescricao = '" + TBNome.Text + "' WHERE  StcCodigo = '" + Session["AlrteraCodigo_modelo"] + "' ";
            var sqlinsert = "INSERT INTO CA_StatusCliente( StcDescricao  ) VALUES( '" + TBNome.Text + "' ) ";

            return Session["comando"].Equals("Alterar") ? sqlupdate : sqlinsert;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var row = ((GridView) sender).SelectedRow;
                var dados = from i in bd.CA_StatusClientes 
                            where i.StcCodigo.Equals(WebUtility.HtmlDecode(row.Cells[0].Text))
                            select new { i.StcCodigo, i.StcDescricao };
                if(dados.Count() > 0)
                {
                    var tipo = dados.First();
                    TBcodigo.Text = tipo.StcCodigo.ToString();
                    Session["AlrteraCodigo_modelo"] = tipo.StcCodigo.ToString();
                    TBcodigo.Enabled = false;
                    TBNome.Text = tipo.StcDescricao;
                    Session["comando"] = "Alterar";
                    MultiView1.ActiveViewIndex = 1;
                }
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
            Session["comando"] = "Novo";
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
            try
            {
                using (var repository = new Repository<GrauParentesco>(new Context<GrauParentesco>()))
                {
                    var itens = repository.All().OrderBy(p => p.GpaDescricao);
                    foreach (var item in itens)
                    {
                        string linha = item.GpaCodigo + ";" + item.GpaDescricao;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Grau de Parentesco.txt");
                }
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
            var escola = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<StatusCliente>(new Context<StatusCliente>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(escola);
            }
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text.Equals("1"))
                {
                    e.Row.Cells[2].BackColor = Color.FromName("red");
                }

                if (e.Row.Cells[0].Text.Equals("2"))
                {
                    e.Row.Cells[2].BackColor = Color.FromName("green");
                }

                if (e.Row.Cells[0].Text.Equals("3"))
                {
                    e.Row.Cells[2].BackColor = Color.FromName("blue");
                }

                if (e.Row.Cells[0].Text.Equals("4"))
                {
                    e.Row.Cells[2].BackColor = Color.FromName("yellow");
                }

                if (e.Row.Cells[0].Text.Equals("5"))
                {
                    e.Row.Cells[2].BackColor = Color.FromName("black");
                }
                if (e.Row.Cells[0].Text.Equals("6"))
                {
                    e.Row.Cells[2].BackColor = Color.FromName("gray");
                }

            }
        }
    }
}