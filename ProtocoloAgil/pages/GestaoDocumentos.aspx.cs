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
using System.IO;
using System.Web;


namespace ProtocoloAgil.pages
{
    public partial class GestaoDocumentos : Page
    {


        [Serializable]
        public struct Arquivos
        {
            
            public string Nome_Arquivo { get; set; }
            
            
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["CurrentPage"] = "aprendiz";

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
            }

            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterPostBackControl(GridView2);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = from i in bd.CA_Documentos
                            join p in bd.CA_DocumentosAprendizs on i.DocCodigo equals p.DAprDocumento
                            join m in bd.CA_Aprendizs on p.DAprMatricula equals m.Apr_Codigo
                            //join a in bd.MA_AreaEnsinos on p.DAluCurso equals a.EnsCodigo
                           // join ca in bd.MA_CursosdoAlunos on p.DAluMatricula equals ca.CalMatricula
                            //join c in bd.MA_Cursos on ca.CalTipo equals c.CurCodigo


                            where// i.DocCodigo.Equals(GridView1.SelectedRow.Cells[2].Text) &&
                                  p.DAprSequencia.Equals(GridView1.SelectedRow.Cells[0].Text) &&
                                  p.DAprStatus.Equals("S")
                            select new
                            {
                                p.DAprMatricula,
                                m.Apr_Nome,
                                p.DAprDocumento,
                                i.DocDescricao,
                                i.DocValor,
                                p.DAprDataSolic,
                                i.DocDiasEntrega,
                                p.DAprSequencia,
                                p.DAprStatusParecer,
                                p.DAprParecer,
                                p.DAprJustificativa,
                                p.DAprObservacao,
                                p.AluNomeAnexo
                                
                                
                            };
                if (dados.Count() == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                        "alert('Não foi possível encontrar dados sobre a solicitação. ')",
                                                        true);
                    return;
                }
                var solicitacao = dados.First();
                var dias = solicitacao.DocDiasEntrega ?? 0;
                TBAlunmo.Text = solicitacao.DAprMatricula + " - " + solicitacao.Apr_Nome;
                TBnome.Text = solicitacao.DAprDocumento + " - " + solicitacao.DocDescricao;
                TBvalor.Text = string.Format("{0:c}", solicitacao.DocValor);
                TBdataSol.Text = solicitacao.DAprDataSolic.ToString("dd/MM/yyyy");
                TBdataEnt.Text = solicitacao.DAprDataSolic.AddDays(dias).ToString("dd/MM/yyyy");
                DDParecer.SelectedValue = solicitacao.DAprStatusParecer ?? "";
                TBparecerTecnico.Text = solicitacao.DAprParecer;
                TBjustificativa.Text = solicitacao.DAprJustificativa;
                TBobservacao.Text = solicitacao.DAprObservacao;
                CodSolicitacao.Value = solicitacao.DAprSequencia.ToString();
               // lblCurso.Text = solicitacao.CurNome;
                codAluno.Value = solicitacao.DAprMatricula.ToString();
                //descricaoCurso.Value = solicitacao.CurNome;
               // codCurso.Value = solicitacao.DAluCurso;
                if (!solicitacao.AluNomeAnexo.Equals(string.Empty))
                {
                    trArquivoBaixar.Visible = true;
                    GetFiles(int.Parse(GridView1.SelectedRow.Cells[1].Text), solicitacao.DAprSequencia);
                }
                else
                {
                    trArquivoBaixar.Visible = false;
                }

            }
            MultiView1.ActiveViewIndex = 1;
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = GridView2.SelectedRow;
            var name = HttpUtility.HtmlDecode(row.Cells[0].Text);

            var fInfo = new FileInfo(ViewState["Caminho"] + name);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fInfo.Name + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", fInfo.Length.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.WriteFile(fInfo.FullName);
        }


        private void GetFiles(int aluno, int sequencia)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var dados = from i in bd.CA_DocumentosAprendizs
                          //  join p in bd.MA_StatusRequisicaos on i.DAluStatus equals p.SitCodigo
                            join n in bd.CA_Documentos on i.DAprDocumento equals n.DocCodigo
                            //join m in bd.MA_Escolas on i.DAluEscola equals m.EscCodigo
                            where i.DAprSequencia.Equals(sequencia)
                            select new { i.DAprSequencia, i.DAprDataSolic, i.AluNomeAnexo };
                var req = dados.First();
              
                //var protocolo = req.DAluSequencia.ToString().PadLeft(6, '0') + "-" + req.DAluDataSolic.Year;
               
                if (dados.Count() == 0) return;

                var filePath = Server.MapPath(@"/files/Documentos/Alunos/" + aluno + "/");
                ViewState.Add("Caminho", filePath);
                var dir = new DirectoryInfo(filePath);
                if (dir.Exists)
                {
                    var files = dir.GetFiles();
                    var lista = files.Where(i => i.Name.Equals("_" + req.AluNomeAnexo)).ToList();
                    //var lista = req.DocDirEspecial.Equals("S") ? files.Where(i => i.Name.Equals(protocolo.ToString())).ToList() : files.Where(i => i.Name.Split('_')[0].Equals(protocolo)).ToList();
                   
                    var datasrc = lista.Select(fileInfo => new Arquivos { Nome_Arquivo = fileInfo.Name}).ToList();
                    GridView2.DataSource = datasrc;
                    GridView2.DataBind();
                }
            }
        }


        protected void BTsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDParecer.SelectedValue.Equals(string.Empty))
                    throw new Exception("Selecione um status para o parecer da secretaria.");

                var sql = "update CA_DocumentosAprendiz set DAprParecer = '" + TBparecerTecnico.Text +
                          "', DAprStatusParecer = '" + DDParecer.SelectedValue + "', DAprUsuParecer = '" +
                          Session["codigo"] +
                          "', " + "DAprDataParecer = '" + DateTime.Today + "', DAprObservacao = '" + TBobservacao.Text +
                          "', DAprStatus ='F'  where DAprSequencia = " +
                          CodSolicitacao.Value + "";

                var con = new Conexao();
                con.Alterar(sql);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('Finalização de solicitação realizada com sucesso.')", true);
                //EnviaComprovante();
                //Session["codDocumento"] = CodSolicitacao.Value;
                //Session["id"] = 74;
                GridView1.DataBind();
                MultiView1.ActiveViewIndex = 0;

            }
            catch (SqlException p)
            {
                Funcoes.TrataExcessao("000000", p);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('" + ex.Message + "')", true);
            }
        }

        protected void Voltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
       
    }
}