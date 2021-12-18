using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class popup_faltas : Page
    {
        [Serializable]
        private struct Item
        {
            public string Codigo { get; set; }
            public string Descricao { get; set; }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "PET - Jovem Aprendiz";
            if (!IsPostBack)
            {
                GridviewDataBind();
                Session["selecionados"] = "";
            }
        }


        protected void GridviewDataBind()
        {
            var lista = new List<Item>();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var meta = Criptografia.Decrypt(Request.QueryString["target"], GetConfig.Key());
                var entries = Regex.Split(meta, "\n");

                LB_disciplina.Text = "Disciplina: " + entries[2] + ".<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Professor: " + entries[3];

                var dados = from i in bd.View_CA_DiarioAprendizes where i.Apr_Codigo == int.Parse(entries[1]) 
                           && i.DpOrdem == int.Parse(entries[0]) select i;


                if(dados.Count() == 0) return;
                var aulas = dados.First();

                if (aulas.DATA01 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA01), Descricao = aulas.DiaAula1 });
                if (aulas.DATA02 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA02), Descricao = aulas.DiaAula2 });
                if (aulas.DATA03 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA03), Descricao = aulas.DiaAula3 });
                if (aulas.DATA04 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA04), Descricao = aulas.DiaAula4 });
                if (aulas.DATA05 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA05), Descricao = aulas.DiaAula5 });
                if (aulas.DATA06 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA06), Descricao = aulas.DiaAula6 });
                if (aulas.DATA07 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA07), Descricao = aulas.DiaAula7 });
                if (aulas.DATA08 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA08), Descricao = aulas.DiaAula8 });
                if (aulas.DATA09 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA09), Descricao = aulas.DiaAula9 });
                if (aulas.DATA10 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA10), Descricao = aulas.DiaAula10 });
                if (aulas.DATA11 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA11), Descricao = aulas.DiaAula11 });
                if (aulas.DATA12 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA12), Descricao = aulas.DiaAula12 });
                if (aulas.DATA13 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA13), Descricao = aulas.DiaAula13 });
                if (aulas.DATA14 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA14), Descricao = aulas.DiaAula14 });
                if (aulas.DATA15 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA15), Descricao = aulas.DiaAula15 });
                if (aulas.DATA16 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA16), Descricao = aulas.DiaAula16 });
                if (aulas.DATA17 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA17), Descricao = aulas.DiaAula17 });
                if (aulas.DATA18 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA18), Descricao = aulas.DiaAula18 });
                if (aulas.DATA19 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA19), Descricao = aulas.DiaAula19 });
                if (aulas.DATA20 != null) lista.Add(new Item { Codigo = string.Format("{0:dd/MM/yyyy}", aulas.DATA20), Descricao = aulas.DiaAula20 });

                GridView1.DataSource = lista;
                GridView1.DataBind();

            }
        }

        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridviewDataBind();
        }
    }
}