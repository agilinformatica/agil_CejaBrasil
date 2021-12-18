using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.Globalization;
using System.IO;


namespace ProtocoloAgil.pages
{
    public partial class SolicitaDocumento : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["CurrentPage"] = "academicoalunos";
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data"); // file upload só funciona assim...
                MultiView1.ActiveViewIndex = 0;

            }


        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            trMostraSeTiverAnexo.Visible = false;
            TrParecer.Visible = false;
            TrParecerCaixaTexto.Visible = false;
            TrDataParecer.Visible = false;
            txtJustificativa.Text = string.Empty;
            txtJustificativa.Enabled = true;
            btnSalvar.Visible = true;
            var row = GridView1.SelectedRow;
            Session["codigoDocumento"] = row.Cells[0].Text;
            PreencheDocumento(row.Cells[0].Text);

        }


        private void PreencheDocumento(string codDocumento)
        {
            List<CADocumentos> listaDocumento = new List<CADocumentos>();

            using (var repository = new Repository<CADocumentos>(new Context<CADocumentos>()))
            {
                var datasource = new List<CADocumentos>();
                datasource.AddRange(repository.All().Where(p => p.DocCodigo == codDocumento).OrderBy(p => p.DocCodigo));
                listaDocumento = datasource;



                foreach (CADocumentos lista in listaDocumento)
                {
                    lblValor.Text = string.Format("{0:c}", lista.DocValor);
                    lblNome.Text = lista.DocDescricao;
                    lblDataSolicitacao.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    DateTime dt = DateTime.Now;
                    dt = dt.AddDays(int.Parse(lista.DocDiasEntrega.ToString()));
                    lblDataEntrega.Text = string.Format("{0:dd/MM/yyyy}", dt);

                    if (lista.DocExigeAnexo.Equals("S"))
                    {
                        trAnexo.Visible = true;
                    }
                    else
                    {

                        trAnexo.Visible = false;
                    }

                }
                MultiView1.ActiveViewIndex = 1;
            }
        }

        private void PreencheDocumentoVisualizar(string codDocumento)
        {
            btnSalvar.Visible = false;
            txtJustificativa.Enabled = false;

             List<CADocumentos> listDoc= new List<CADocumentos>();

            using (var repository = new Repository<CADocumentos>(new Context<CADocumentos>()))
            {
                var datasource = new List<CADocumentos>();
                datasource.AddRange(repository.All().Where(p => p.DocCodigo == codDocumento).OrderBy(p => p.DocCodigo));
                listDoc = datasource;

                foreach(CADocumentos lista in listDoc){
                    lblValor.Text = string.Format("{0:c}", lista.DocValor);
                    lblNome.Text = lista.DocDescricao;
                }

            }

            var sql = "select DAprDataSolic, DAprDataEntrega, DAprJustificativa, AluNomeAnexo, DaprParecer, DAprDataParecer from CA_DocumentosAprendiz  where DAprMatricula = " + Session["matricula"] + " and DAprDocumento = " + codDocumento + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                lblDataSolicitacao.Text = string.Format("{0:dd/MM/yyyy}", result["DAprDataSolic"]);

                lblDataEntrega.Text = string.Format("{0:dd/MM/yyyy}", result["DAprDataEntrega"]);
                
                txtJustificativa.Text = result["DAprJustificativa"] == null ? "" : result["DAprJustificativa"].ToString();

                
                if ((result["AluNomeAnexo"] == null || result["AluNomeAnexo"].ToString().Equals(string.Empty)))
                {
                    trMostraSeTiverAnexo.Visible = false;
                }
                else
                {
                    lblArquivoAnexado.Text = result["AluNomeAnexo"].ToString();
                    trMostraSeTiverAnexo.Visible = true;
                }
                if ((result["DAprDataParecer"] == null || result["DAprDataParecer"].ToString().Equals(string.Empty)))
                {
                    TrDataParecer.Visible = false;
                }
                else
                {
                    TrDataParecer.Visible = true;
                    lblDataParecer.Text = string.Format("{0:dd/MM/yyyy}", result["DAprDataParecer"]);
                }

                if ((result["DaprParecer"] == null || result["DaprParecer"].ToString().Equals(string.Empty)))
                {
                    TrParecer.Visible = false;
                    TrParecerCaixaTexto.Visible = false;
                }
                else
                {
                    TrParecerCaixaTexto.Visible = true;
                    TrParecer.Visible = true;
                    lblParecer.Text = result["DaprParecer"].ToString();
                }

                

            }
            trAnexo.Visible = false;              
            MultiView1.ActiveViewIndex = 1;
            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Session["tipo"] = "Envio";
            string valor = "";
            string codigo = Session["codigoDocumento"].ToString();
            var aux = Funcoes.TrataValor(lblValor.Text.Substring(3));
            if (!aux.Equals("") && !aux.Equals(null))
            {
                valor = aux;
            }
            string nomeArquivo = "";
            string dataSol = lblDataSolicitacao.Text;
            string dataent = lblDataEntrega.Text;
            string dataEntrega = lblDataEntrega.Text;


            ////////////////// TESTEEEEEEEEEEEEEEEEEE ////////////////////////////
            //dataSol = "07/02/2015";
            //dataent = "07/02/2015";
            //dataEntrega = "07/02/2015";

            try
            {
                if (trAnexo.Visible)
                {
                    if (fupArquivo.FileName.Equals(string.Empty))
                    {
                        throw new ArgumentException("Arquivo anexo obrigatório.");
                    }
                    nomeArquivo = fupArquivo.FileName;

                }

                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var dados = from i in bd.CA_DocumentosAprendizs
                                where i.DAprMatricula.Equals(Session["matricula"]) && i.DAprDocumento.Equals(codigo)
                                      && i.DAprStatus == "S" && i.DAprDataSolic.Equals(DateTime.Today)
                                select i;
                    if (dados.Count() > 0) throw new ArgumentException("Documento já solicitado. Aguarde o Parecer da secretaria.");
                }

               



                var con = new Conexao();
                var sql =
                    @"INSERT INTO CA_DocumentosAprendiz(DAprMatricula, DAprDocumento, DAprDataSolic, DAprDataEntrega, DAprUsuario, DAprObservacao, DAprStatus, DAprDocAnexo, DAprJustificativa, AluNomeAnexo, DAprPrevEntrega) VALUES (" + Session["matricula"] + ", '" + codigo + "','" + dataSol + "', '" + dataent + "','WEB'," + "'Incluido via WEB'" + " , 'S', '.', '" + txtJustificativa.Text + "','" + nomeArquivo + "', '" + dataEntrega + "')";
                con.Alterar(sql);

                if (trAnexo.Visible)
                {
                    nomeArquivo = fupArquivo.FileName;
                    SalvaArquivo(fupArquivo);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação realizada com sucesso.')", true);

                MultiView1.ActiveViewIndex = 0;

            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                   "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000132", ex);
            }
        }

        protected void SalvaArquivo(FileUpload arquivo)
        {
            HFmatricula.Value = Session["matricula"].ToString();

            if (!arquivo.HasFile) return;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                //var escola = DD_nome_campus.SelectedValue;
                //var dados = from i in bd.MA_DocumentosAlunos
                //            join m in bd.MA_Documentos on i.DAluDocumento equals m.DocCodigo
                //            where i.DAluMatricula.Equals(Session["matricula"]) &&
                //                  i.DAluDocumento.Equals(Session["Doc"]) && i.DAluDataSolic.Equals(DateTime.Today)
                //                  && i.DAluStatus.Equals("P") && i.DAluEscola.Equals(escola)
                //            select new { i, m.DocDirEspecial };

                //if (dados.Count() > 0)
                //{
                //   var requerimento = dados.First();
                //salva o arquivo.
                var filePath = Server.MapPath(@"/files") + @"/Documentos/Alunos/" + Session["matricula"] + @"/";

                var dir = new DirectoryInfo(filePath);
                if (!dir.Exists)
                    dir.Create();
                //var protocolo = requerimento.i.DAluSequencia.ToString().PadLeft(6, '0') + "-" + requerimento.i.DAluDataSolic.Year;
                var file = new FileInfo(filePath + "_" + arquivo.FileName);
                // var file = new FileInfo(filePath + protocolo + "_" + Session["matricula"].ToString() + ".jpg");
                try
                {
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    file.Refresh();
                }
                catch (IOException ex)
                {
                    var user = (string)Session["matricula"];
                    //Funcoes.TrataExcessao("000000", ex, (user ?? "Indefinido"));
                }
                finally
                {
                    arquivo.SaveAs(filePath + "_" + arquivo.FileName);
                    Session["file_attached"] = arquivo.FileName;
                    //  arquivo.SaveAs(filePath + protocolo + "_" + Session["matricula"].ToString() + ".jpg");
                    // Session["file_attached"] = Session["matricula"].ToString() + ".jpg";
                    Session["AnexarDoc"] = "Sim";
                }

            }
        }


        protected void listar_Click(object sender, EventArgs e)
        {
            HFmatricula.Value = Session["matricula"].ToString();
            GridView2.DataBind();
            MultiView1.ActiveViewIndex = 2;
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var index = Convert.ToInt32(e.CommandArgument);
            var row = GridView2.Rows[index];
            if (e.CommandName.Equals("Documento"))
            {
                string valor = row.Cells[2].Text;
                string codocumento = row.Cells[0].Text;

                Session["codigoDocumento"] = codocumento;
                PreencheDocumentoVisualizar(codocumento);
                
            }
        }

        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }
}
     