using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class PopupEscolas : Page
    {
        [Serializable]
        private struct Item
        {
            public string Codigo { get; set; }
            public string Descricao { get; set; }
        }

        private readonly List<Item> Lista = new List<Item>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Seleção de Escola(s) - Requerimento Web";
            if (!IsPostBack)
            {
                ViewState.Add("Lista", Lista.ToArray());
                GridviewDataBind();
                Session["selecionados"] = "";
            }
     }


        protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
        {
            GridView1.AllowPaging = false;
            GridviewDataBind();

            foreach (GridViewRow row in GridView1.Rows)
            {
                var cb = (CheckBox)row.FindControl("CheckBox2");
                cb.Checked = ((CheckBox)sender).Checked;
                var itens = Session["selecionados"].ToString().Split('_').Where(p => !p.Equals(string.Empty)).ToList();
                if (((CheckBox)sender).Checked)
                    Session["selecionados"] += row.Cells[0].Text + "_";
                else
                {
                    itens.Remove(row.Cells[0].Text);
                    Session["selecionados"]  = itens.Aggregate("", (current, item) => current + (item + "_"));
                }
            }

            GridView1.AllowPaging = true;
        }

        protected void btn_adicionar_Click(object sender, EventArgs e)
        {
            var dados = Session["selecionados"].ToString();
            var selected = dados.Length == 0 ? "" : dados.Substring(0, dados.Length - 1);
            Session["selecionados"] = "";
            var tipo = Criptografia.Decrypt(Request.QueryString["id"], GetConfig.Key());
            string url="";


            var meta = Criptografia.Decrypt(Request.QueryString["meta"], GetConfig.Key());
            var target = Criptografia.Decrypt(Request.QueryString["target"], GetConfig.Key());
            var entries = Regex.Split(meta, "\n");
            switch (tipo)
            {
                case "1":
                    entries[0] = selected;
                    url = target + "?acs=" + Request.QueryString["acs"];
                    break;
                case "10":
                    url = "EstatisticasUnidade.aspx?acs=" + Request.QueryString["acs"];
                    break;
                case "11":
                    url = "RelatorioSinteticoControle.aspx?acs=" + Request.QueryString["acs"];
                    break;
                case "12":
                    url = "RelatorioAnaliticoControle.aspx?acs=" + Request.QueryString["acs"];
                    break;

                case "13":
                    url =  target + "?acs=" + Request.QueryString["acs"];
                    entries[0] = selected;
                    break;

                case "14":
                    url = target + "?acs=" +Request.QueryString["acs"];
                    entries[1] = selected;
                    break;

                case "15":
                    url = target + "?acs=" + Request.QueryString["acs"];
                    entries[2] = selected;
                    break;
                case "16":
                    url = target + "?acs=" + Request.QueryString["acs"];
                    entries[3] = selected;
                    break;

                case "17":
                    url = target + "?acs=" + Request.QueryString["acs"];
                    entries[4] = selected;
                    break;

                case "19":
                    url = target + "?acs=" + Request.QueryString["acs"];
                    entries[6] = selected;
                    break;

            }
            Session["option"] = tipo;
            var delivered = entries.Aggregate("", (current, entry) => current + (entry + "\n"));
            var data = delivered.Substring(0, delivered.Length - 1);
            Session["code_delivered"] = Criptografia.Encrypt(data, GetConfig.Key());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "javascript:opener.change('" + url + "'); this.window.close();", true);
        }


        protected void GridviewDataBind()
        {
            Lista.Clear();
            var list = new List<Item>();

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var tipo = Criptografia.Decrypt(Request.QueryString["id"], GetConfig.Key());
                if (tipo != null)
                {
                    var meta = Criptografia.Decrypt(Request.QueryString["meta"], GetConfig.Key());
                    var entries = Regex.Split(meta, "\n");
                    switch (tipo)
                    {
                        case "1":
                            Lista.AddRange(from i in bd.CA_Unidades
                                           select new Item {Codigo = i.UniCodigo.ToString(), Descricao = i.UniNome});
                            break;

                        case "13":
                            Lista.AddRange(from i in bd.CA_Unidades
                                           join p in bd.CA_UsuarioUnidades on i.UniCodigo equals p.UniCodigoUnidade
                                           where p.UnicodigoUsuario.Equals(entries[5]) && !i.UniCodigo.Equals(1)
                                           select new Item {Codigo = i.UniCodigo.ToString(), Descricao = i.UniNome});
                            break;
                    }
                    GridView1.DataSource =  tipo.Equals("14") ?  list : Lista;
                    GridView1.DataBind();
                    ViewState["Lista"] = Lista.ToArray();

                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridviewDataBind();
            GridView1.DataBind();
            var selected = Session["selecionados"].ToString();
            if(selected.Equals("")) return;
            string[] lista = selected.Length > 5 ? selected.Split('_') : new string[]{selected};

            foreach (var item in lista)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    var cb = (CheckBox)row.FindControl("CheckBox2");
                    if (row.Cells[0].Text.Equals(item))
                    {
                        cb.Checked = true;
                        break;
                    }
                }
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var cb = ((CheckBox) sender);
            var row =  (GridViewRow) cb.Parent.Parent;

            if(cb.Checked)
                Session["selecionados"] += row.Cells[0].Text + "_";
            else
            {
                var itens = Session["selecionados"].ToString().Split('_').Where(p => !p.Equals(string.Empty)).ToList();
                itens.Remove(row.Cells[0].Text);
                Session["selecionados"] = itens.Aggregate("", (current, item) => current + (item + "_"));
            }
        }
    }
}