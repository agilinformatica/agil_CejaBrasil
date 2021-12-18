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
    public partial class CadastroBairro : Page
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
                var dados = from i in bd.CA_Bairros join m in bd.CA_Regioes on i.RegBairro equals m.CodRegiao select new { Bairro = i.DescBairro, Regiao = m.DescRegiao };
                var datasource =  new ArrayList();
                switch (tipo)
                {
                    case 1: datasource.AddRange(dados.OrderBy(p => p.Bairro).ToList()); break;
                    case 2: datasource.AddRange(dados.Where(p => p.Bairro.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.Bairro).ToList()); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }


        private void BindRegioes()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.CA_Regioes.ToList().OrderBy(p => p.DescRegiao).ToList();
                DD_regiao.DataSource = dados.ToList();
                DD_regiao.DataBind();
            }
        }


        private void PreencheCampos()
        {
            LimpaCampos();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var bairro = from i in bd.CA_Bairros join m in bd.CA_Regioes on i.RegBairro equals m.CodRegiao
                             where i.DescBairro.Equals(Session["Alteracodigo"].ToString()) 
                             select new { Bairro = i.DescBairro, Regiao = m.CodRegiao };

                if (bairro.Count() == 0) return;
                var dados = bairro.First();
                TBBairro_nome.Text = dados.Bairro;
                DD_regiao.SelectedValue = dados.Regiao.ToString();
            }
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBBairro_nome.Text.Equals(string.Empty)) throw new ArgumentException("Informe o nome do bairro.");
                if (DD_regiao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe a região do bairro.");

                string sqlinsert = "INSERT INTO CA_Bairros VALUES('" + TBBairro_nome.Text + "'," + DD_regiao.SelectedValue + ")";
                string sqlupdate = "UPDATE CA_Bairros  SET  DescBairro = '" + TBBairro_nome.Text + "', RegBairro =" + DD_regiao.SelectedValue + " where DescBairro = '" + Session["Alteracodigo"] + "' ";

              //  var parameters = new List<SqlParameter> { new SqlParameter("DescBairro", TBBairro_nome.Text), new SqlParameter("RegBairro", int.Parse(DD_regiao.SelectedValue)) };
                var con = new Conexao();

                if (Session["comando"].Equals("Inserir"))
                {
                    con.Alterar(sqlinsert);
                }
                else {
                    con.Alterar(sqlupdate);
                }
                
                // con.Alterar(Session["comando"].Equals("Inserir") ?sqlinsert : sqlupdate,parameters.ToArray() );
  
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Ação realizada com sucesso.')", true);
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
            TBBairro_nome.Text = string.Empty;
            BindRegioes();
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
            Session["id"] = "69";
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
                    var dados =  from i in bd.CA_Bairros join m in bd.CA_Regioes on i.RegBairro equals m.CodRegiao select new { Bairro = i.DescBairro, Regiao = m.DescRegiao };
                    foreach (var item in dados)
                    {
                        var linha = item.Bairro + "; " + item.Regiao + "; ";
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Bairros.txt");
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
            var bairro = button.CommandArgument;
            using (var repository = new Repository<Bairros>(new Context<Bairros>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(bairro);
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