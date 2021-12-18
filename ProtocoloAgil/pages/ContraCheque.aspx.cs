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
using System.Web;


namespace ProtocoloAgil.pages
{
    public partial class ContraCheque : Page
    {


        [Serializable]
        public struct Arquivo
        {
            public string Nome_Arquivo { get; set; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "secretariaalunos";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (!IsPostBack)
            {
                GetFiles();
            }

            if (scriptManager != null)
            {
                scriptManager.RegisterPostBackControl(GridView2);
            }
        }




        private void GetFiles()
        {
            try
            {
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {

                    var filePath = Server.MapPath(@"/files") + @"/Contra-Cheques/";
                    //var filePath = Server.MapPath(@"/files/Contra-Cheques");
                    ViewState.Add("Caminho", filePath);
                    var dir = new DirectoryInfo(filePath);
                    if (dir.Exists)
                    {
                        var files = dir.GetDirectories();
                        var lista = files.ToList();
                        //var lista = req.DocDirEspecial.Equals("S") ? files.Where(i => i.Name.Equals(protocolo.ToString())).ToList() : files.Where(i => i.Name.Split('_')[0].Equals(protocolo)).ToList();

                        var datasrc = lista.Select(fileInfo => new Arquivo { Nome_Arquivo = fileInfo.Name }).ToList();
                        GridView2.DataSource = datasrc;
                        GridView2.DataBind();

                        MultiView1.ActiveViewIndex = 2;
                    }

                }
            }
            catch (Exception e)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                //   "alert('ERRO - Não existe arquivo para download.');", true);
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

            try {
                var row = GridView2.SelectedRow;
                var name = HttpUtility.HtmlDecode(row.Cells[0].Text);

                var sql = "Select apr_NumSistExterno from CA_aprendiz where apr_codigo = " + Session["codigo"] + "";
                var con = new Conexao();
                var result = con.Consultar(sql);
                string numeroExterno = "";

                while (result.Read())
                {
                    numeroExterno = result["apr_NumSistExterno"].ToString();
                }


                var fInfo = new FileInfo(ViewState["Caminho"] + name + "/" + numeroExterno + ".pdf");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fInfo.Name + "\"");
                HttpContext.Current.Response.AddHeader("Content-Length", fInfo.Length.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.WriteFile(fInfo.FullName);
            }
            catch (Exception x)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                     "alert('ERRO - Não existe download para este período');", true);
            }
            
        }

    }
}