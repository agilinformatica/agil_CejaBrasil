using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class CronogramaAprendizes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "academicoalunos";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                HFmatricula.Value = Session["codigo"].ToString();
                PreencheCampos();
                BinfGridView();
            }
        }

        private void BinfGridView()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var datasource = bd.View_CA_CronogramaAulas
                                 .Join(bd.CA_DisciplinasAprendizs, p => p.DpOrdem, x => x.DiaDisciplinaProf, (p, x) => new { p, x })
                                 .Where(m => m.x.DiaCodAprendiz == int.Parse(HFmatricula.Value))
                                 .Select( dados => new { dados.p.Disciplina, dados.p.Professor, dados.p.ADPDataAula });
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count().ToString();
                GridView1.DataBind();
            }
            
        }

        private void PreencheCampos()
        {
            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
            var dados = from i in bd.View_Resultado_Finals
                        where i.Apr_Codigo == int.Parse(HFmatricula.Value)
                        select new { i.Apr_Nome, i.Turma, i.CurDescricao, i.ParNomeFantasia, i.DiaNumeroFaltas };
            var aluno = dados.First();

            LBAprendiz_Conceito.Text = aluno.Apr_Nome;
            LBCodigo_Parceiro.Text = aluno.ParNomeFantasia;
            LBCurso_Conceito.Text = aluno.CurDescricao;
            LBTurma_Conceito.Text = aluno.Turma;
        }



        protected void btn_adicionar_Click(object sender, EventArgs e)
        {
            Session["id"] = 38;
            Session["PRMT_User"] = HFmatricula.Value;
            MultiView1.ActiveViewIndex = 1;
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BinfGridView();
        }



        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            
        }

    }
}