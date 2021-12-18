using System;
using System.Linq;
using System.Web.UI;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class BoletimAprendiz :Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "academicoalunos";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                HFmatricula.Value = Session["codigo"].ToString();
                PreencheCampos();
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
            Session["id"] = 37;
            Session["PRMT_User"] = HFmatricula.Value;
            MultiView1.ActiveViewIndex = 1;
        }
    }
}