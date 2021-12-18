using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class DisciplinasDiario : Page
    {
        readonly DC_ProtocoloAgilDataContext bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Session["CurrentPage"] = "notasprofessores";
            HFmatricula.Value = Session["codigo"].ToString();
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = GridView1.SelectedRow;
            HFordem.Value = WebUtility.HtmlDecode(row.Cells[0].Text);

            var dados = from i in bd.View_CA_DisciplinasProfessores where  i.DpOrdem.Equals(HFordem.Value) 
                        select new { i.CurDescricao, i.TurNome, i.DisDescricao, i.DPDataInicio};

            if(dados.Count() > 0)
            {
                var dado = dados.First();
                LBCursoCont.Text = dado.CurDescricao;
                LBTurmaCont.Text = dado.TurNome;
                LBDisciplinaCont.Text = dado.DisDescricao;
                LBCodigoCont.Text = string.Format("{0:dd/MM/yyy}", dado.DPDataInicio);
            }
            LimpaCampos();
            BindDatas();
            //ExibeConteudo();
            MultiView1.ActiveViewIndex = 1;
        }

        protected void BindDatas()
        {
            using (var repository = new Repository<AulasProfessores>(new Context<AulasProfessores>()))
            {
                var aulas = new List<AulasProfessores>();
                var aulast = repository.FindAulas(int.Parse(HFordem.Value));
               // aulast.Where(p => p.ADPStatus.Equals("A")).OrderBy(p => p.ADPDataAula)
                aulas.AddRange(aulast.OrderBy(p => p.ADPDataAula));

                DDDatasConteudo.DataSource = aulas;
                DDDatasConteudo.DataBind();
            }
        }


        //private void ExibeConteudo()
        //{
        //    var dados = from i in bd.CA_AulasDisciplinasTurmaProfs join p in bd.CA_DisciplinasTurmaProfs on i.ADPDisciplinaProf equals p.DpOrdem
        //                where p.DpOrdem.Equals(HFordem.Value) select new {i.ADPConteudoLecionado, i.ADPObservacoes, i.ADPRecursosUsados, i.ADPDataAula};
        //    listaconteudo.DataSource = dados;
        //    listaconteudo.DataBind();
        //}


        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        private void LimpaCampos()
        {
            LBDataCont.Text = string.Empty;
            TBConteudo.Text = string.Empty;
            TBRecurso.Text = string.Empty;
            TBobservacao.Text = string.Empty;
        }

        protected void btnvoltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void btn_nucleocomum_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
        }

        protected void DDDatasConteudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var repository = new Repository<AulasProfessores>(new Context<AulasProfessores>()))
            {
                var aulast = repository.FindAulas(int.Parse(HFordem.Value));
                var dados = aulast.Where(p => p.ADPDataAula.Equals(DateTime.Parse(DDDatasConteudo.SelectedValue)));

                var aula = dados.First();
                LBDataCont.Text = string.Format("{0:dd/MM/yyyy}", aula.ADPDataAula);
                TBConteudo.Text = string.IsNullOrWhiteSpace(aula.ADPConteudoLecionado) ? "" : aula.ADPConteudoLecionado;
                TBRecurso.Text = string.IsNullOrWhiteSpace(aula.ADPRecursosUsados) ? "" : aula.ADPRecursosUsados;
                TBobservacao.Text = string.IsNullOrWhiteSpace(aula.ADPObservacoes) ? "" : aula.ADPObservacoes; 
                repository.Edit(aula);
            }
        }

        protected void BTLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        protected void BTinsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDDatasConteudo.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione a data da aula.");
                if (string.IsNullOrEmpty(TBConteudo.Text)) throw new ArgumentException("Digite o conteúdo ensinado na aula.");
                DateTime data;
                DateTime.TryParse(DDDatasConteudo.SelectedValue, out data);
                if (data > DateTime.Today) throw new ArgumentException("Não é permitido lançar aulas futuras.");

                  using (var repository = new Repository<AulasProfessores>(new Context<AulasProfessores>()))
                  {
                      var aulast = repository.FindAulas(int.Parse(HFordem.Value));
                      var dados = aulast.Where(p => p.ADPDataAula.Equals(DateTime.Parse(DDDatasConteudo.SelectedValue)));

                      var aula = dados.First();
                      aula.ADPConteudoLecionado = TBConteudo.Text;
                      aula.ADPRecursosUsados = TBRecurso.Text;
                      aula.ADPObservacoes = TBobservacao.Text;
                      aula.ADPStatus = TBConteudo.Text.Equals(string.Empty) ? "A" : "R";
                      repository.Edit(aula);

                      //ExibeConteudo();
                      BindDatas();
                      LimpaCampos();
                  }
                  ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                     "alert('aula salva com sucesso')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('" + ex.Message + "')", true);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (!e.CommandName.Equals("Imprimir")) return;

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            Session["PRMT_Ordem"] = WebUtility.HtmlDecode(row.Cells[0].Text);
            Session["id"] = 12;
            MultiView1.ActiveViewIndex = 2;
        }
    }
}