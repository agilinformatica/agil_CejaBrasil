using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using ProtocoloAgil.Base;
using System.Web.UI.WebControls;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class ArquivosAlunos : Page
    {
        protected string Pathfiles;
        readonly DC_ProtocoloAgilDataContext bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            Session["CurrentPage"] = "arquivosalunos";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterPostBackControl(GridView2);
                scriptManager.RegisterPostBackControl(btnEnviar);
            }
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
            }
        }

        protected void DDturmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDDisNome.Items.Clear();
            DDDisNome.DataBind();
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }


        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fInfo = new FileInfo(ViewState["Caminho"] +"/" + HttpUtility.HtmlDecode(GridView2.SelectedRow.Cells[0].Text));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition","attachment; filename=\"" + fInfo.Name + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", fInfo.Length.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.WriteFile(fInfo.FullName);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fupArquivo.HasFile) throw new ArgumentException("Selecione um arquivo para enviar.");
                if (DDturmas.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma turma.");
                if (DDDisNome.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma disciplina.");
                if (fupArquivo.FileContent.Length > 10000000) throw new ArgumentException("Arquivo maior que o limite recomendado.");

                var professor = DDDisNome.SelectedValue;
                var filePath = Server.MapPath(@"/files/" + GetConfig.Escola() + "/Material/" + professor + "p/Recebidos");
                var nomeArquivo = Session["matricula"] + "_" + fupArquivo.FileName;
                var dir = new DirectoryInfo(filePath);
                if (dir.Exists)
                    fupArquivo.SaveAs(filePath + "/" + nomeArquivo);
                else
                {
                    dir.Create();
                    fupArquivo.SaveAs(filePath + "/" + nomeArquivo);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Arquivo enviado com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('" + ex.Message + ".')", true);
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

        protected void Baixar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var professor = RetornaProfessor(HttpUtility.HtmlDecode(GridView1.SelectedRow.Cells[2].Text));
            var disciplina = RetornaDisciplina(HttpUtility.HtmlDecode(GridView1.SelectedRow.Cells[1].Text));
            var turma = RetornaTurma(HttpUtility.HtmlDecode(GridView1.SelectedRow.Cells[0].Text));
            var filePath = Server.MapPath(@"/files/" + GetConfig.Escola() + "/Material/" + professor + "p/" + turma  + "/" + disciplina);
            ViewState.Add("Caminho", filePath);
            var dir = new DirectoryInfo(filePath);
            if(dir.Exists)
            {
                GridView2.DataSource = dir.GetFiles();
                GridView2.DataBind();
            }
        }

        private object RetornaTurma(string nome)
        {
            var turma = from i in bd.CA_Turmas where i.TurNome.Equals(nome) select i.TurCodigo;
            return turma.First().ToString();
        }

        private string RetornaProfessor(string nome)
        {
            var prof = from i in bd.CA_Educadores where i.EducNome.Equals(nome) select i.EducCodigo;
            return prof.First().ToString();
        }

        private string RetornaDisciplina(string nome)
        {
            var discip = from i in bd.CA_Disciplinas where i.DisDescricao.Equals(nome) select i.DisCodigo;
            return discip.First().ToString();
        }
   }
}