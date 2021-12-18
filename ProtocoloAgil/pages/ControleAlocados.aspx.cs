using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base.Models;


namespace ProtocoloAgil.pages
{
    public partial class ControleAlocados : Page
    {
  protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            Session["CurrentPage"] = "aprendiz";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["matricula"] = GridView1.SelectedRow.Cells[0].Text;
            Session["enable_Save"] = "Parceiro";
            MultiView1.ActiveViewIndex = 1;
            Session["Comando"] = "Alterar";
            GerarDiretorioaluno();
        }

        private void GerarDiretorioaluno()
        {
            var filePath = Server.MapPath(@"\files");
            var directory = new DirectoryInfo(filePath + @"\" + Session["Escola"] + @"\Alunos\" + HFmatricula.Value + @"\");
            if (!directory.Exists)
                directory.Create();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            Session["Comando"] = "Inserir";
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
           GridView1.DataBind();
        }


        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton) sender;
            var aprendiz = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(aprendiz);
            }
           GridView1.DataBind();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            var codigo = ((ImageButton) sender).CommandArgument;
            Session["matricula"] = codigo;
            Session["id"] = 18;
            MultiView1.ActiveViewIndex = 3;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
   }
}