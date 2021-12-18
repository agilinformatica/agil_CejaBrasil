using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    struct Feriado
    {
        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public string Unidade { get; set; }
    }


    public partial class CadastroFeriado : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(btn_texto);
            if (!IsPostBack)
            {
                BindGridView();
               Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
            }
        }


        private void BindGridView()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var feriados = bd.CA_Feriados.Where(p => p.FerData.Year == DateTime.Today.Year);
                var datasource = feriados.Select(p => new Feriado { Codigo = p.FerOrdem,   Data = p.FerData, Nome = p.FerDescricao,
                                                          Unidade = p.FerUnidade == 99 ? "Todas as Unidades"
                                                          : bd.CA_Unidades.Where( m => m.UniCodigo == p.FerUnidade).First() .UniNome
                                                      });
                GridView1.DataSource = datasource.ToList();
                HFRowCount.Value = datasource.Count().ToString();
                GridView1.DataBind();
            }
        }


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBDataRef.Text.Equals(string.Empty)) throw new ArgumentException("Informe uma data de feriado.");
                if (TB_nome_feriado.Text.Equals(string.Empty)) throw new ArgumentException("Informe o nome do feriado.");

                const string sqlinsert = "INSERT INTO CA_Feriados VALUES(@FerUnidade,@FerData,@FerDescricao)";
                const string sqlupdate = "UPDATE CA_Feriados SET FerUnidade = @FerUnidade, FerData = @FerData, FerDescricao = @FerDescricao " +
                                "WHERE FerOrdem = @codigo01";

                var parameters = new List<SqlParameter>{ new SqlParameter("FerUnidade", DDunidade.SelectedValue), new SqlParameter("FerData", TBDataRef.Text) ,
                                  new SqlParameter("FerDescricao", TB_nome_feriado.Text) };

                if (!Session["comando"].Equals("Inserir"))
                {
                    parameters.Add(new SqlParameter("codigo01", Session["AlrteraUnidade"].ToString()));
                }

                var con = new Conexao();
                con.Alterar(Session["comando"].Equals("Inserir") ? sqlinsert : sqlupdate,parameters.ToArray());
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
                Funcoes.TrataExcessao("000025", ex);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView)sender).SelectedRow;

            var bt = (ImageButton) row.Cells[3].FindControl("IMBexcluir");
            Session["AlrteraUnidade"] = bt.CommandArgument;
            Session["comando"] = "Alterar";
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void PreencheCampos()
        {
            LimpaCampos();
            using (var repository = new Repository<Feriados>(new Context<Feriados>()))
            {
                var feriado = repository.All().Where(p => p.FerOrdem == int.Parse(Session["AlrteraUnidade"].ToString())).First();
                TBDataRef.Text = feriado.FerData.ToString("dd/MM/yyyy");
                DDunidade.Text = feriado.FerUnidade.ToString();
                TB_nome_feriado.Text = feriado.FerDescricao;
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Todas as Unidades", "99");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btn_novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 1;
        }

        private void LimpaCampos()
        {
            TBDataRef.Text = string.Empty;
            TB_nome_feriado.Text = string.Empty;
            DDunidade.DataBind();
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
           BindGridView();
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
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var dados = from i in bd.CA_Feriados join m in bd.CA_Unidades on i.FerUnidade equals m.UniCodigo
                                where i.FerData.Year == DateTime.Today.Year
                                select new { Data = i.FerData, Unidade = m.UniCodigo == 99 ? "Todas as Unidades" : m.UniNome, Codigo = m.UniCodigo, Nome = i.FerDescricao };
                    foreach (var item in dados)
                    {
                        var linha = item.Data.ToString("dd/MM/yyyy") + "; " + item.Nome + "; " + item.Codigo + "; " + item.Unidade;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Feriados por Unidade.txt");
                }
            }
            catch (IOException ex)
            {
                 Funcoes.TrataExcessao("000021", ex);
            }
        }


        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton) sender;
            var row = (GridViewRow) button.Parent.Parent;
            using (var repository = new Repository<Feriados>(new Context<Feriados>()))
            {
                var feriado = repository.All().Where(p => p.FerData == DateTime.Parse(row.Cells[0].Text) 
                    && p.FerOrdem == int.Parse(button.CommandArgument)).First();
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(feriado);
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