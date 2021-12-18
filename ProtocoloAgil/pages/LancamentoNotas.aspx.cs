using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using MKB.TimePicker;
using ProtocoloAgil.Base.Models;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;
using System.Data;
using System.Globalization;
using System.Web;

namespace ProtocoloAgil.pages
{
    public partial class LancamentoNotas : Page
    {
        public struct AprendizPesquisa
        {
            public string Apr_Codigo { get; set; }
            public string Apr_Nome { get; set; }
            public string Apr_Telefone { get; set; }
            public string Apr_Sexo { get; set; }
            public string StaDescricao { get; set; }
            public string Apr_Email { get; set; }
            public string Apr_AreaAtuacao { get; set; }
            public short Apr_PlanoCurricular { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "aprendiz";
           // Page.Form.DefaultButton = btn_pesquisa.UniqueID;
            if (!IsPostBack)
            {
                //BindCursos(DDcurso_origem);
                //BindCursos(DDcurso_destino);
                //MultiView1.ActiveViewIndex = 0;
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindAprendizes();
        }

        protected void BindAprendizes()
        {
            if (TBNome.Text.Equals(string.Empty) && TBCodigo.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('Informe parte do nome ou matricula do Aprendiz para pesquisa.')", true);
                return;
            }

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_Aprendiz> lista;
                if (!TBNome.Text.Equals(string.Empty))
                    lista = from i in bd.CA_Aprendizs where SqlMethods.Like(i.Apr_Nome, "%" + TBNome.Text + "%") select i;

                else lista = from i in bd.CA_Aprendizs where i.Apr_Codigo == int.Parse(TBCodigo.Text) select i;
                var situacao = (from i in bd.CA_SituacaoAprendizs select i).ToList();
                var datasource = lista.ToList().Select(p => new AprendizPesquisa
                {
                    Apr_Codigo = p.Apr_Codigo.ToString(),
                    Apr_Nome = p.Apr_Nome,
                    Apr_Sexo = (p.Apr_Sexo.Equals("M") ? "Masculino" : "Feminino"),
                    Apr_Telefone = Funcoes.FormataTelefone(p.Apr_Telefone),
                    Apr_Email = p.Apr_Email,
                    StaDescricao = p.Apr_Situacao == 0 ? "" : (situacao.Where(x => x.StaCodigo.Equals(p.Apr_Situacao)).First().StaDescricao),
                    Apr_AreaAtuacao = p.Apr_AreaAtuacao.ToString(),
                    Apr_PlanoCurricular = (short)p.Apr_PlanoCurricular

                }).ToList().OrderBy(p => p.Apr_Nome);
                GridView1.DataSource = datasource.ToList();

                HFRowCount.Value = datasource.Count().ToString();
                GridView1.DataBind();

            }
        }

        public void CarregaDadosNotas()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from NA in db.View_CA_Notas_do_Aprendizs

                             where NA.NdiAprendiz.Equals(Session["MatriculaNota"].ToString())

                             select new { NA.Apr_Nome, NA.DisDescricao, NA.TurNome, NA.NdiNota, NA.NdiDisciplina }

                            ).OrderBy(p => p.DisDescricao);

                gridNotas.DataSource = query;
                gridNotas.DataBind();
                gridNotas.Visible = true;
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var linha = gridNotas.Rows.Count;
            var a = new object();
            for (int i = 0; i < linha; i++)
            {
                var idDisciplina = gridNotas.Rows[i].Cells[3].Text;
                DropDownList t = (DropDownList)gridNotas.Rows[i].Cells[4].FindControl("ddNota");
                var idCombo = t.SelectedValue;

                var sql = "update View_CA_Notas_do_Aprendiz set NdiNota = " + idCombo + " where NdiAprendiz  = " + Session["MatriculaNota"].ToString() + " and NdiDisciplina = " + idDisciplina + "";
                var con = new Conexao();
                con.Alterar(sql);
            }
            CarregaDadosNotas();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('Alterado com sucesso')", true);
        }


        protected void duty_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList duty = (DropDownList)gvr.FindControl("DDNota");
            var teste = duty.SelectedItem.Value;
        }

        //Autor: Thassio Augusto
        //Data: 30/04/2015
        //Reponsável por rodar cada linha do grid ja carregado e setar o valor da combo de acordo com o que vem do banco
        protected void gridNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlNota = (e.Row.FindControl("DDNota") as DropDownList);
                var minhaFonte = DataBinder.Eval(e.Row.DataItem, "NdiNota");
                ddlNota.SelectedValue = minhaFonte.ToString();
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            BindAprendizes();
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Notas"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                var matricula = row.Cells[0].Text;
                var nome = row.Cells[1].Text;
                Session["MatriculaNota"] = matricula;
                Session["NomeNota"] = nome;
                txtMatricula.Text = matricula;
                txtNome.Text = HttpUtility.HtmlDecode(nome);
                CarregaDadosNotas();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindAprendizes();
        }
    }
}