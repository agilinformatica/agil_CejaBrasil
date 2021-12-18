using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class ListaAtivos : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "aprendiz";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;

            if (HFSelectedRadio.Value != string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "LoadInput()", true);
            }

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                HFSelectedRadio.Value = "tab-0";
                PreencheDropDownTurma();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        void PreencheDropDownTurma()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = (from T in db.CA_Turmas
                             select new { T.TurCodigo, T.TurNome });

                DDturma_pesquisa.DataSource = query;
                DDturma_pesquisa.DataBind();
                DDturma_pesquisa.Items.Insert(0, "Selecione..");
                DDturma_pesquisa.SelectedIndex = 0;
            }
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void bt_lista_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            if (dd_mes_pesquisa.SelectedValue.Equals(string.Empty) && tb_ano_pesquisa.Text.Equals(string.Empty) && HFSelectedRadio.Value != "tab-0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Não é possivel imprimir esta solicitação. Informe mês e ano de referência.')", true);
                return;
            }


           

            if (!DDParceiro.SelectedValue.Equals(string.Empty))
            {
               Session["parceiroImpressao"] =  DDParceiro.SelectedValue;
            }

            if (!DDturma_pesquisa.SelectedValue.Equals(string.Empty) && !DDturma_pesquisa.SelectedValue.Equals("Selecione.."))
            {
               Session["TurmaImpressao"] = DDturma_pesquisa.SelectedValue;
            }


            Session["id"] = 43;
            Session["PRMT_Nome"] = pesquisa.Text.Equals(string.Empty) ? "%" : pesquisa.Text;
            Session["PRMT_MesRef"] = dd_mes_pesquisa.SelectedValue;
            Session["PRMT_AnoRef"] = tb_ano_pesquisa.Text;
            Session["PRMT_Turma"] = DDturma_pesquisa.Text;
            Session["PRMT_Tipo"] = HFSelectedRadio.Value;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                // var datasource = new List<View_AlocacoesAluno>();


                //var selected = from u in bd.View_AlocacoesAlunos
                //               orderby u.Apr_Nome
                //               where u.TurCurso.Equals("002")

                //               select u;

                var selected = from u in bd.View_AlocacoesAlunos
                               orderby u.Apr_Nome
                               where u.TurCurso.Equals("002") 

                               select u;

                //var aprendizes = bd.View_AlocacoesAlunos.Where(item => item.TurCurso.Equals("002") && item.ALAStatus.Equals("A")).OrderBy(p => p.Apr_Nome).ToList();

                var datasource = selected;


                if (!pesquisa.Text.Equals(string.Empty))
                {
                    selected = selected.Where(p => p.Apr_Nome.ToLower().Trim().Contains(pesquisa.Text.ToLower()));
                }

                if (!DDParceiro.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(p => p.ParCodigo.Equals(DDParceiro.SelectedValue));
                }

                if (!DDturma_pesquisa.SelectedValue.Equals(string.Empty) && !DDturma_pesquisa.SelectedValue.Equals("Selecione.."))
                {
                    selected = selected.Where(p => p.TurCodigo.Equals(DDturma_pesquisa.SelectedValue));
                }


                if (DDturma_pesquisa.SelectedValue.Equals("Selecione.."))
                {
                    // datasource.AddRange(aprendizes.Where(p => p.TurCurso.Equals("002")));
                }
                else
                {
                    selected.ToList();
                }



                GridView1.DataSource = selected.Where(item => item.TurCurso != "003");
                HFRowCount.Value = selected.Count().ToString();
                GridView1.DataBind();


                //var ativos = datasource.Where(p => p.ALADataInicio <= DateTime.Today).OrderBy(p => p.Apr_Nome).OrderBy(p => p.ALApagto).ToList();
              //  var ativos = datasource.Where(p => p.ALAStatus == "A" && p.Apr_Situacao == 6).OrderBy(p => p.Apr_Nome).OrderBy(p => p.ALApagto).ToList();

                //geral
                //GridView1.DataSource = ativos;
                //HFRowCount.Value = ativos.Count.ToString();
                //GridView1.DataBind();

                if (GridView1.Rows.Count > 0)
                {
                    btnFolhaPonto.Visible = true;
                }
                else
                {
                    btnFolhaPonto.Visible = false;
                }

                if (!tb_ano_pesquisa.Text.Equals(string.Empty) && !dd_mes_pesquisa.SelectedValue.Equals(string.Empty))
                {
                    //por data de início
                    var inicio = datasource.Where(p => p.Apr_InicioAprendizagem != null && ((DateTime)p.Apr_InicioAprendizagem).Month == int.Parse(dd_mes_pesquisa.SelectedValue) &&
                                 ((DateTime)p.Apr_InicioAprendizagem).Year == int.Parse(tb_ano_pesquisa.Text)).OrderBy(
                                     p => p.Apr_Nome).OrderBy(p => p.ALApagto).ToList();

                    GridView2.DataSource = inicio.Where(item => item.TurCurso == "002"); ;
                    HFRowCount.Value = inicio.Count.ToString();
                    GridView2.DataBind();


                    //por data de término
                    var termino = datasource.Where(p => p.Apr_FimAprendizagem != null && ((DateTime)p.Apr_FimAprendizagem).Month == int.Parse(dd_mes_pesquisa.SelectedValue) &&
                                 ((DateTime)p.Apr_FimAprendizagem).Year == int.Parse(tb_ano_pesquisa.Text)).OrderBy(
                                     p => p.Apr_Nome).OrderBy(p => p.ALApagto).ToList();

                    GridView3.DataSource = termino.Where(item => item.TurCurso != "003"); ;
                    HFRowCount.Value = termino.Count.ToString();
                    GridView3.DataBind();

                    //por previsão de término
                    var previsao = datasource.Where(p => p.Apr_PrevFimAprendizagem != null && ((DateTime)p.Apr_PrevFimAprendizagem).Month == int.Parse(dd_mes_pesquisa.SelectedValue) &&
                                 ((DateTime)p.Apr_PrevFimAprendizagem).Year == int.Parse(tb_ano_pesquisa.Text)).OrderBy(
                                     p => p.Apr_Nome).OrderBy(p => p.ALApagto).ToList();

                    GridView4.DataSource = previsao.Where(item => item.TurCurso == "002"); ;
                    HFRowCount.Value = previsao.Count.ToString();
                    GridView4.DataBind();
                }
                else
                {
                    GridView2.DataBind();
                    GridView3.DataBind();
                    GridView4.DataBind();
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["matricula"] = GridView1.SelectedRow.Cells[0].Text;
            Session["enable_Save"] = "User";

            MultiView1.ActiveViewIndex = 1;
            Session["Comando"] = "Alterar";
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var grid = (GridView)sender;
            grid.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnFolhaPonto_Click(object sender, EventArgs e)
        {
          //  divPonto.Visible = true;
        }

        protected void txtImpressaoFolha_Click(object sender, EventArgs e)
        {

            //if (txtDataInicio.Text.Equals(string.Empty))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            //        "alert('Digite a data de início da folha de ponto.')", true);
            //    return;
            //}


            //if (txtDataFim.Text.Equals(string.Empty))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            //        "alert('Digite a data de término da folha de ponto.')", true);
            //    return;
            //}


            if (DDturma_pesquisa.SelectedValue.Equals("Selecione..") && DDParceiro.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Para emitir a folha de ponto a Turma ou o Parceiro deve ser selecionado.')", true);
                return;
            }


            //var a = txtDataInicio.Text.Split('/');
            //var b = txtDataFim.Text.Split('/');

            //List<DateTime> listaData = new List<DateTime>();
            //DateTime dataInicio = new DateTime(int.Parse(a[2]), int.Parse(a[1]), int.Parse(a[0]));
            //DateTime dataFim = new DateTime(int.Parse(b[2]), int.Parse(b[1]), int.Parse(b[0]));

            //string mesExtenso = RetornaMesExterno(dataInicio);
            //string ano = "";
            //int aux = 0;
            //if (a[1].Equals("12"))
            //{
            //    aux = int.Parse(a[2]) + 1;

            //    ano = a[2] + "/" + aux.ToString();
            //}
            //else
            //{
            //    ano = a[2];
            //}


            //TimeSpan ts = dataFim - dataInicio;

            //listaData.Add(dataInicio);
            //for (int i = 1; i <= ts.Days; i++)
            //{
            //    listaData.Add(dataInicio.AddDays(i));
            //}

           // Session["ListaDatas"] = listaData;

            if (dd_mes_pesquisa.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Selecione o mês de referência.')", true);
                return;
            }


            if (tb_ano_pesquisa.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Selecione o ano.')", true);
                return;
            }



            Session["ano"] = tb_ano_pesquisa.Text;
            Session["mesExtenso"] = dd_mes_pesquisa.SelectedItem;

            Session["id"] = 99;
            Session["PRMT_Nome"] = pesquisa.Text.Equals(string.Empty) ? "%" : pesquisa.Text;
            Session["PRMT_MesRef"] = dd_mes_pesquisa.SelectedValue;
            Session["PRMT_AnoRef"] = tb_ano_pesquisa.Text;
            Session["PRMT_Turma"] = DDturma_pesquisa.SelectedValue;
            Session["PRMT_Tipo"] = HFSelectedRadio.Value;
            Session["Parceiro"] = DDParceiro.SelectedValue;
            MultiView1.ActiveViewIndex = 2;
        }

        public string RetornaMesExterno(DateTime date)
        {
            string mesExterno = "";
            if (date.Month == 1)
            {
                mesExterno = "Janeiro/Fevereiro";
            }
            else if (date.Month == 2)
            {
                mesExterno = "Fevereiro/Março";
            }
            else if (date.Month == 3)
            {
                mesExterno = "Março/Abril";
            }
            else if (date.Month == 4)
            {
                mesExterno = "Abril/Maio";
            }
            else if (date.Month == 5)
            {
                mesExterno = "Maio/Junho";
            }
            else if (date.Month == 6)
            {
                mesExterno = "Junho/Julho";
            }
            else if (date.Month == 7)
            {
                mesExterno = "Julho/Agosto";
            }
            else if (date.Month == 8)
            {
                mesExterno = "Agosto/Setembro";
            }
            else if (date.Month == 9)
            {
                mesExterno = "Setembro/Outubro";
            }
            else if (date.Month == 10)
            {
                mesExterno = "Outubro/Novembro";
            }
            else if (date.Month == 11)
            {
                mesExterno = "Novembro/Dezembro";
            }
            else if (date.Month == 12)
            {
                mesExterno = "Dezembro/Janeiro";
            }
            return mesExterno;
        }

        protected void btnImprimiFinanceiro_Click(object sender, EventArgs e)
        {

            if (dd_mes_pesquisa.SelectedValue.Equals(string.Empty) && tb_ano_pesquisa.Text.Equals(string.Empty) && HFSelectedRadio.Value != "tab-0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Não é possivel imprimir esta solicitação. Informe mês e ano de referência.')", true);
                return;
            }

            if (dd_mes_pesquisa.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Não é possivel imprimir esta solicitação. Informe mês de referência.')", true);
                return;
            }

            if (tb_ano_pesquisa.Text.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Não é possivel imprimir esta solicitação. Informe o ano de referência.')", true);
                return;
            }

            if (DDTipoContrato.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Não é possivel imprimir esta solicitação. Informe o tipo de contrato.')", true);
                return;
            }

            Session["id"] = 100;
            Session["PRMT_Nome"] = pesquisa.Text.Equals(string.Empty) ? "%" : pesquisa.Text;
            Session["mesReferenciaNumeral"] = dd_mes_pesquisa.SelectedValue.Substring(0, 1).Equals("0") ? dd_mes_pesquisa.SelectedValue.Substring(1, 1) : dd_mes_pesquisa.SelectedValue;
            Session["PRMT_MesRef"] = dd_mes_pesquisa.SelectedItem;
            Session["PRMT_AnoRef"] = tb_ano_pesquisa.Text;
            Session["PRMT_Turma"] = DDturma_pesquisa.SelectedValue;
            Session["PRMT_Parceiro"] = DDParceiro.SelectedValue;
            Session["PRMT_Tipo"] = HFSelectedRadio.Value;
            Session["TipoContrato"] = DDTipoContrato.SelectedValue;

            Session["PRMT_TipoContrato"] = DDTipoContrato.SelectedValue == "" ? "" : DDTipoContrato.SelectedValue == "E" ? "Empresa" : "CejaBrasil";
            MultiView1.ActiveViewIndex = 2;

        }
    }
}