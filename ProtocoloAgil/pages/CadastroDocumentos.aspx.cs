using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using System.Collections.Generic;


namespace ProtocoloAgil.pages
{
    public partial class CadastroDocumentos : Page
    {
        private int id_escola;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
          //  Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if (!IsPostBack)
            {
                //Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], ConfigurationBase.Key);
                id_escola = Convert.ToInt16(Session["Escola"]);
                Session["comando"] = "Inserir";
                MultiView1.ActiveViewIndex =0;
                BindGridView();
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
               // BTaltera.Enabled = false;
            }
        }

        private void BindGridView()
        {
            using (var repository = new Repository<CADocumentos>(new Context<CADocumentos>()))
            {
                var datasource = new List<CADocumentos>();
                datasource.AddRange(repository.All().Where(p => p.DocCodigo != "9999999").OrderBy(p => p.DocCodigo));
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                PainelDocumento.Visible = datasource.Count == 0 ? true : false;
                GridView1.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBtituloAlt.Text = "Alteração de Requerimentos";
            btnSalvar.Text = "Alterar";
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            HFDocumento.Value = GridView1.SelectedRow.Cells[0].Text;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        public void PreencheCampos()
        {

            var sql = "Select * from CA_Documentos where DocCodigo = '" + Session["Alteracodigo"] + "'";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                txtCodigo.Text = result["DocCodigo"].ToString();
                txtDescricao.Text = result["DocDescricao"].ToString();
                txtPreco.Text = string.Format("{0:c}", result["DocValor"].ToString());
                
                DDTipo.SelectedValue = result["DocTipo"].ToString();
                cbObrigatorio.Checked = result["DocObrigatorio"].ToString().Equals("S") ? true : false;
                txtDiasEntrega.Text = result["DocDiasEntrega"].ToString();
                DDProtocolo.SelectedValue = result["DocProtocolo"].ToString();
                txtOrientacoes.Text = result["DocOrientacoes"].ToString();
                txtOrientacoesAdicional.Text = result["DocOrientacoes02"].ToString();
                cb_exige_Anexo.Checked = result["DocExigeAnexo"].ToString().Equals("S") ? true : false;
             
            }
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var codDocumento = button.CommandArgument;
            var sql = "Delete CA_Documentos where DocCodigo = '" + codDocumento + "'";
            var con = new Conexao();
            con.Alterar(sql);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('Excluído com sucesso')", true);
            BindGridView();
        }

        protected void GridView1_RowCommand(object sender,GridViewCommandEventArgs e)
        {
              var   index = Convert.ToInt32(e.CommandArgument);
              var   row = GridView1.Rows[index];
                Session["Alteracodigo"] = row.Cells[0].Text;
                HFDocumento.Value = row.Cells[0].Text; 
                Session["Page"] = "Documentos";
           //     Response.Redirect("Excluir.aspx?acs=" + Criptografia.Encrypt(Session["tipoacesso"].ToString(), ConfigurationBase.Key));
            
        }

        private String Retirasimbolo(String aux)
        {
            aux = aux.Replace(",00", "");
            aux = aux.Replace(",", ".");

            char[] trim = { '=', '\\', ';', ':', '$', 'R', '+', '*', '(', ')', '-', '/', ' ' };
            int pos;
            while ((pos = aux.IndexOfAny(trim)) >= 0)
            {
                aux = aux.Remove(pos, 1);
            }
            return aux;
        }

        protected void BTlimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void LimpaCampos()
        {
            txtCodigo.Text = string.Empty;
            txtDiasEntrega.Text = string.Empty;
            txtPreco.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtOrientacoes.Text = string.Empty;
            txtOrientacoesAdicional.Text = string.Empty;
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
            LBtituloAlt.Text = "Cadastro de Requerimentos";
            btnSalvar.Text = "Salvar";
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 2;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void texto_Click(object sender, EventArgs e)
        {
            try
            {
                var filePath = Server.MapPath("/files");
                // Deleta o arquivo existente e cria outro.
                System.IO.File.Delete(filePath + @"/temp.txt");
                var write = new FileManager(filePath + @"/temp.txt");
                var cn = new Conexao(id_escola);
                SqlDataReader dr = cn.Consultar("SELECT * FROM MA_Documentos ORDER BY DocCodigo");
                while (dr.Read())
                {
                    string linha = dr["DocCodigo"] + ";" + dr["DocDescricao"] + ";" +
                                   string.Format("{0:c}", dr["DocValor"]) + ";" + dr["DocDiasEntrega"];
                    write.Escreve(linha);
                }

                // download do arquivo de texto
                string fileName = filePath + @"/temp.txt";
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=Lista_de_Documentos.txt");
                Response.WriteFile(fileName);
                Response.Flush();
                Response.Close();
            }catch(Exception ex)
            {
                var user = (string)Session["codigo"];
                //Funcoes.TrataExcessao("000112", ex, (user ?? "Indefinido"));   
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text.Equals(string.Empty)) {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('Digite o código do documento')", true);
                    return;
                }

                using (var repository = new Repository<CADocumentos>(new Context<CADocumentos>()))
                {
                   // var unidade = Session["comando"].Equals("Inserir");
                    CADocumentos documento = new CADocumentos();

                    documento.DocCodigo = txtCodigo.Text;
                    documento.DocDiasEntrega = txtDiasEntrega.Text.Equals("") ? new short() : short.Parse(txtDiasEntrega.Text);
                    documento.DocExigeAnexo = cb_exige_Anexo.Checked ? "S" : "N";
                    documento.DocObrigatorio = cbObrigatorio.Checked ? "S" : "N";
                    documento.DocOrientacoes = txtOrientacoes.Text;
                    documento.DocOrientacoes02 = txtOrientacoesAdicional.Text;
                    documento.DocProtocolo = DDProtocolo.SelectedValue.Equals(string.Empty) ? "" : DDProtocolo.SelectedValue;
                    documento.DocTipo = DDTipo.SelectedValue.Equals(string.Empty) ? "" : DDTipo.SelectedValue;
                    documento.DocDescricao = txtDescricao.Text;
                    //                documento.DocValor = txtPreco.Text.Equals("") ? 0 : string.Format("{0:c}", txtPreco.Text);
                   // documento.DocValor = txtPreco.Text.Equals("") ? 0 : Retirasimbolo(txtPreco.Text);
                    //  documento.DocValor = float.Parse(txtPreco.Text);
                    if (Session["comando"].Equals("Inserir"))
                    {

                        var sql = "Insert into CA_Documentos values('" + documento.DocCodigo + "', '" + documento.DocDescricao + "', " + Retirasimbolo(txtPreco.Text.Equals(string.Empty) ? "0" : txtPreco.Text) + ", '" + documento.DocTipo + "', '" + documento.DocObrigatorio + "', " + documento.DocDiasEntrega + ", '" + documento.DocProtocolo + "', '" + documento.DocOrientacoes + "', '" + documento.DocExigeAnexo + "', '" + documento.DocOrientacoes02 + "')";
                        var con = new Conexao();
                        con.Alterar(sql);
                    }
                    if (Session["comando"].Equals("Alterar"))
                    {
                        var sql = "update CA_Documentos set DocCodigo = '" + documento.DocCodigo + "', DocDescricao = '" + documento.DocDescricao + "', DocValor = " + Retirasimbolo(txtPreco.Text.Equals(string.Empty) ? "0" : txtPreco.Text) + ", DocTipo =  '" + documento.DocTipo + "', DocObrigatorio = '" + documento.DocObrigatorio + "', DocDiasEntrega = " + documento.DocDiasEntrega + ", DocProtocolo = '" + documento.DocProtocolo + "', DocOrientacoes = '" + documento.DocOrientacoes + "', DocExigeAnexo = '" + documento.DocExigeAnexo + "',  DocOrientacoes02 = '" + documento.DocOrientacoes02 + "' where DocCodigo = '" + HFDocumento.Value + "'";
                       var con = new Conexao();
                       con.Alterar(sql);
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('Ação realizada com sucesso.')", true);
                    BindGridView();
                    MultiView1.ActiveViewIndex = 0;
                }
            }
            catch (SqlException a)
            {
              
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('ERRO - Já existe um documento com este código.')", true);
                
            }
           
        }
    }
}