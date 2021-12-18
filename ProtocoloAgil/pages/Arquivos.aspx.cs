using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class Arquivos : Page
    {   

        public struct ArquivosRecebidos
        {
            public int Matricula { get; set; }
            public string Nome { get; set; }
            public string Turma { get; set; }
            public string Arquivo { get; set; }
        }


        protected string Pathfiles;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            Session["CurrentPage"] = "arquivosprofessores";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterPostBackControl(GridView1);
                scriptManager.RegisterPostBackControl(GridView2);
            }
            if (!IsPostBack)
            {
                CriaPastas();
                MultiView1.ActiveViewIndex = 0;
            }
        }

        private void CriaPastas()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var filePath = Server.MapPath(@"/files/" + GetConfig.Escola() + @"/Material");
                var dir = new DirectoryInfo(filePath);
                if (dir.Exists)
                {
                    var sub = dir.GetDirectories();
                    var cont = sub.Count(file => file.Name.Equals(Session["codigo"] + "p"));
                    if (cont.Equals(0))
                    {
                        dir.CreateSubdirectory(Session["codigo"] + "p");
                    }
                }
                else
                {
                    dir.Create();
                    dir.CreateSubdirectory(Session["codigo"] + "p");
                }

                filePath = Server.MapPath(@"/files/" + GetConfig.Escola() + @"/Material/" + Session["codigo"] + "p");
                ViewState.Add("pasta", filePath);
                var dirprof = new DirectoryInfo(filePath);
                var disciplinas = from i in bd.CA_DisciplinasTurmaProfs
                                  join j in bd.CA_Disciplinas on i.DPDisciplina equals j.DisCodigo
                                  where i.DPProf.Equals(Session["codigo"])
                                  select new { j.DisCodigo, i.DPTurma };
                foreach (var disciplina in disciplinas.ToList().Distinct())
                {
                    var tempDir = new DirectoryInfo(filePath + "/" + disciplina.DPTurma);
                    if (!tempDir.Exists)
                        dirprof.Create();
                    tempDir = new DirectoryInfo(filePath + "/" + disciplina.DPTurma + "/" + disciplina.DisCodigo);
                    if (!tempDir.Exists)
                        tempDir.Create();
                }
            }
        }


        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void DDturmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDDisNome.Items.Clear();
            DDDisNome.DataBind();
        }

        protected void DDDisNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["disciplina"] = DDDisNome.SelectedValue;
        }

        protected void BTsearch_Click(object sender, EventArgs e)
        {
            var filePath = @"" + ViewState["pasta"] + @"/" + DDturmas.SelectedValue + @"/" + DDDisNome.SelectedValue;

            var direct = @"" + ViewState["pasta"] + @"/" + DDturmas.SelectedValue + @"/" + DDDisNome.SelectedValue;

            var dir = new DirectoryInfo(direct);
            if (dir.Exists)
            {
                GridView1.DataSource = dir.GetFiles();
                GridView1.DataBind();
            }

            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filePath = "";
            var gv = (GridView)sender;
            var row = gv.SelectedRow;
            var arquivo = gv.ID.Equals("GridView1")? HttpUtility.HtmlDecode(row.Cells[0].Text) : row.Cells[0].Text + "_" + HttpUtility.HtmlDecode(row.Cells[3].Text);
            switch (gv.ID)
            {
                case "GridView1":
                    filePath = @"" + ViewState["pasta"] + "/" + DDturmas.SelectedValue + "/" + DDDisNome.SelectedValue + "/" + arquivo;
                    break;
                case "GridView2":
                    filePath = Server.MapPath(@"/files/" + GetConfig.Escola() + "/Material/" + Session["codigo"] + "p/Recebidos/" + arquivo);
                    break;
            }
            var fInfo = new FileInfo(filePath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fInfo.Name + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", fInfo.Length.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.WriteFile(fInfo.FullName);

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                var filePath = @"" + ViewState["pasta"] + @"/" + DDturmas.SelectedValue + @"/" + DDDisNome.SelectedValue + @"/" + HttpUtility.HtmlDecode(row.Cells[0].Text);
                try
                {
                    File.Delete(filePath);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Arquivo excluído com sucesso.')", true);
                }
                catch (Exception)
                {
                    Response.Redirect("ErrorPage.aspx");
                }
                var direct = @"" + ViewState["pasta"] + "/" + DDturmas.SelectedValue + "/" + DDDisNome.SelectedValue;
                var dir = new DirectoryInfo(direct);
                if (dir.Exists)
                {
                    GridView1.DataSource = dir.GetFiles();
                    GridView1.DataBind();
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fupArquivo.HasFile) throw new ArgumentException("Selecione um arquivo para enviar.");
                if (DDturmas.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma turma.");
                if (DDDisNome.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma disciplina.");
                if (fupArquivo.FileContent.Length > 10000000) throw new ArgumentException("Arquivo maior que o limite recomendado.");

                //salva o arquivo.
                var filePath = @"" + ViewState["pasta"] + @"/" + DDturmas.SelectedValue + @"/" + DDDisNome.SelectedValue + "/" + HttpUtility.HtmlDecode(fupArquivo.FileName);
                fupArquivo.SaveAs(filePath);

                //atualiza o Gridview.
                var direct = @"" + ViewState["pasta"] + @"/" + DDturmas.SelectedValue + @"/" + DDDisNome.SelectedValue;
                var dir = new DirectoryInfo(direct);
                if (dir.Exists)
                {
                    GridView1.DataSource = dir.GetFiles();
                    GridView1.DataBind();
                }
            }
            catch(ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('"+ ex.Message+".')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000200", ex);
            }
            finally
            {
                tb_Caminho_arquivo.Text = string.Empty;
            }
        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            var filePath = Server.MapPath(@"/files/" + GetConfig.Escola() + @"/Material/" + Session["codigo"] + "p/Recebidos");
            var dir = new DirectoryInfo(filePath);
            if (dir.Exists)
            {
                var datasource = new List<ArquivosRecebidos>();
                var arquivos = dir.GetFiles();
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    datasource.AddRange(from fileInfo in arquivos let matricula = fileInfo.Name.Split('_')[0]
                                        select new ArquivosRecebidos {
                                           Matricula = int.Parse(matricula), Nome = bd.CA_Aprendizs.Where(p => p.Apr_Codigo == int.Parse(matricula))
                                            .Select(p => p.Apr_Nome).First(),
                                            Arquivo = fileInfo.Name.Remove(0, matricula.Length + 1),
                                            Turma = bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == int.Parse(matricula)).Select(p => p.TurNome).First()
                                                   });
                }

                GridView2.DataSource = datasource.ToList();
                GridView2.DataBind();
            }
            MultiView1.ActiveViewIndex = 1;
        }


        protected void Baixar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[index];
                var arquivo = row.Cells[0].Text + "_" + HttpUtility.HtmlDecode(row.Cells[3].Text);

                var filePath = Server.MapPath(@"/files/" + GetConfig.Escola() + "/Material/" + Session["codigo"] + "p/Recebidos/" + arquivo);
                try
                {
                    File.Delete(filePath);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Arquivo excluído com sucesso.')", true);
                }
                catch (Exception ex)
                {
                    Funcoes.TrataExcessao("000201",ex);
                }
                var direct = Server.MapPath(@"/files/" + GetConfig.Escola() + "/Material/" + Session["codigo"] + "p/Recebidos/");
                var dir = new DirectoryInfo(direct);
                if (dir.Exists)
                {
                    GridView2.DataSource = dir.GetFiles();
                    GridView2.DataBind();
                }
            }
        }
    }
}