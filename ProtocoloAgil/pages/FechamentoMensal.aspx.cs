using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class FechamentoMensal : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["CurrentPage"] = "estatisticas";
            if (!IsPostBack)
            {
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = 0;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_pesquisa.Enabled = false;
            }
        }

        protected void btn_list_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            GridView1.DataBind();
            TBAnoRef.Text = string.Empty;
            DDmeses.SelectedValue = string.Empty;
        }

        protected void btn_gerar_Click(object sender, EventArgs e)
        {
            BindParceiros(DDparceiro_pesquisa);
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btn_pesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bool.Parse(HFConfirmSave.Value)) return;
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    //verificar se o mês já foi fechado. 
                    var dados = bd.CA_Fechamentos.Where(p => p.FechMesFechamento == int.Parse(DDmeses.SelectedValue) && p.FechAnoFechamento.Equals(TBAnoRef.Text));
                    if (dados.Count() > 0) throw new ArgumentException("Mês já foi fechado.");

                    var registros = from i in bd.CA_AlocacaoAprendizs
                                    join p in bd.CA_Aprendizs on i.ALAAprendiz equals p.Apr_Codigo
                                    join m in bd.CA_ParceirosUnidades on i.ALAUnidadeParceiro equals m.ParUniCodigo
                                    where i.ALAStatus.Equals("A") && p.Apr_Situacao == 6                                   
                                    && i.ALADataInicio <= DateTime.Now && i.ALADataPrevTermino <= DateTime.Now
                                    && Convert.ToDateTime(i.ALADataInicio).Month.Equals(Convert.ToInt32(DDmeses.SelectedValue))
                                    && Convert.ToDateTime(i.ALADataInicio).Year.Equals(Convert.ToInt32(TBAnoRef.Text))                        
                                    select new { p.Apr_Codigo, m.ParUniCodigo, i.ALAValorBolsa, i.ALAValorTaxa, i.ALApagto, i.ALATurma, p.Apr_AreaAtuacao };
                                    ///ALADataInicio <= GETDATE() and  ALADataPrevTermino <= GETDATE()
                    //inserir os registros na tabela
                    using (var repository = new Repository<Fechamento>(new Context<Fechamento>()))
                    {
                        foreach (var registro in registros)
                        {
                            var fechamento = new Fechamento {FechAprendiz = registro.Apr_Codigo,FechUnidade = registro.ParUniCodigo,FechPagamento = registro.ALApagto,
                                FechBolsa = (float)registro.ALAValorBolsa,FechTaxa = (float)registro.ALAValorTaxa,FechMesFechamento = int.Parse(DDmeses.SelectedValue),
                                FechAnoFechamento = TBAnoRef.Text,FechTurma = registro.ALATurma,FechAreaAtuacao = (int)registro.Apr_AreaAtuacao};
                            repository.Add(fechamento);
                        }

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                       "alert('Mês de " + DDmeses.SelectedItem.Text + " de " + TBAnoRef.Text + " fechado com sucesso.')", true);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('" + ex.Message + "')", true);
            }
            finally
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var registros = bd.View_CA_Fechamento_Mensals.Where(p => p.FechAnoFechamento == TBAnoRef.Text
                         && p.FechMesFechamento == int.Parse(DDmeses.SelectedValue)).OrderBy(p => p.ParNomeFantasia).OrderBy(p => p.Apr_Nome);

                HFRowCount.Value = registros.Count().ToString();
                GridView1.DataSource = registros;
                GridView1.DataBind();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }


        private void BindGridViewAnalitico()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var datasource = new List<View_CA_Fechamento_Mensal>();
                if (DDparceiro_pesquisa.SelectedValue.Equals(string.Empty))
                    datasource.AddRange(bd.View_CA_Fechamento_Mensals.Where(p => p.FechAnoFechamento == TBAno_pesquisa.Text
                        && p.FechMesFechamento == int.Parse(DDmes_pesquisa.SelectedValue)).OrderBy(p => p.ParNomeFantasia).OrderBy(p => p.Apr_Nome));
                else
                    datasource.AddRange(bd.View_CA_Fechamento_Mensals.Where(p => p.FechAnoFechamento == TBAno_pesquisa.Text
                        && p.FechMesFechamento == int.Parse(DDmes_pesquisa.SelectedValue) && p.ParCodigo == int.Parse(DDparceiro_pesquisa.SelectedValue)).OrderBy(p => p.ParNomeFantasia).OrderBy(p => p.Apr_Nome));

                HFRowCount.Value = datasource.Count().ToString();
                GridView2.DataSource = datasource;
                GridView2.DataBind();

                var totaltaxa = datasource.Select(p => p.FechTaxa).Sum();
                var totalBolsa = datasource.Select(p => p.FechBolsa).Sum();
                Lit_dados.Text = "<span class='fonteTab'> Total Aprendizes: </span> &nbsp; <span class='fonteTexto'> " + datasource.Count + " </span> &nbsp;&nbsp; " +
                   "<span class='fonteTab'> Total Taxa: </span> &nbsp; <span class='fonteTexto'> " + string.Format("{0:c}", totaltaxa) + " </span> &nbsp;&nbsp; " +
                   "<span class='fonteTab'> Total Bolsa: </span> &nbsp; <span class='fonteTexto'> " + string.Format("{0:c}", totalBolsa) + " </span> &nbsp;&nbsp; ";
            }
        }


        protected void BindSinteticoGeral()
        {
            string sql = "select FechPagamento = case when FechPagamento = 'C' then 'CejaBrasil' when FechPagamento = 'E' then 'Empresa'  else 'Não Definido' end, " +
                          "count(FechPagamento) as QTD, sum(FechTaxa) as TotalTaxa, sum(FechBolsa) as TotalBolsa from  CA_Fechamento " +
                          "where FechMesFechamento = " + DDmes_pesquisa.SelectedValue + " And FechAnoFechamento = '" + TBAno_pesquisa.Text + "' group by FechPagamento";

            string sql02 = "select FechPagamento = case when FechPagamento = 'C' then 'CejaBrasil' when FechPagamento = 'E'then 'Empresa'  else 'Não Definido' end, " +
                "count(FechPagamento) as QTD, sum(FechTaxa) as TotalTaxa, sum(FechBolsa) as TotalBolsa from  CA_Fechamento " +
                "inner join CA_ParceirosUnidade on ParUniCodigo = FechUnidade inner join CA_Parceiros on ParUniCodigoParceiro = ParCodigo " +
                "where FechMesFechamento = " + DDmes_pesquisa.SelectedValue + " And FechAnoFechamento = '" + TBAno_pesquisa.Text + "' AND ParCodigo = " + DDparceiro_pesquisa.SelectedValue + "  group by FechPagamento";

            var datasource = new SqlDataSource { ID = "ODStota", ConnectionString = GetConfig.Config(), SelectCommand = DDparceiro_pesquisa.SelectedValue.Equals(string.Empty) ? sql : sql02 };
            datasource.Selected += SqlDataSource1_Selected;
            GridView3.DataSource = datasource;
            GridView3.DataBind();
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }


        protected void BindSinteticoEspecifico()
        {

            var sql = "select ParNomeFantasia, FechPagamento = case when FechPagamento = 'C' then 'CejaBrasil' when FechPagamento = 'E' then 'Empresa'   " +
                "else 'Não Definido' end, count(FechPagamento) as QTD, sum(FechTaxa) as TotalTaxa, sum(FechBolsa) as TotalBolsa  " +
                "from  CA_Fechamento inner join CA_ParceirosUnidade on ParUniCodigo = FechUnidade " +
                "inner join CA_Parceiros on ParUniCodigoParceiro = ParCodigo " +
                "where FechMesFechamento = " + DDmes_pesquisa.SelectedValue + "  And FechAnoFechamento = '" + TBAno_pesquisa.Text + "' group by FechPagamento,ParNomeFantasia";

            var sql02 = "select ParNomeFantasia, FechPagamento = case when FechPagamento = 'C' then 'CejaBrasil' when FechPagamento = 'E' then 'Empresa'   " +
                "else 'Não Definido' end, count(FechPagamento) as QTD, sum(FechTaxa) as TotalTaxa, sum(FechBolsa) as TotalBolsa  " +
                "from  CA_Fechamento inner join CA_ParceirosUnidade on ParUniCodigo = FechUnidade " +
                "inner join CA_Parceiros on ParUniCodigoParceiro = ParCodigo " +
                "where FechMesFechamento = " + DDmes_pesquisa.SelectedValue + "  And FechAnoFechamento = '" + TBAno_pesquisa.Text + "' " +
                "and ParCodigo = " + DDparceiro_pesquisa.SelectedValue + "  group by FechPagamento,ParNomeFantasia";

            var datasource = new SqlDataSource { ID = "ODStota", ConnectionString = GetConfig.Config(), SelectCommand = DDparceiro_pesquisa.SelectedValue.Equals(string.Empty) ? sql : sql02 };
            datasource.Selected += SqlDataSource1_Selected;
            GridView4.DataSource = datasource;
            GridView4.DataBind();
        }


        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var grid = (GridView)sender;
            grid.PageIndex = e.NewPageIndex;
            switch (grid.ID)
            {
                case "GridView1": BindGridView(); break;
                case "GridView2": BindGridViewAnalitico(); break;
                case "GridView4": BindSinteticoEspecifico(); break;
            }
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
             try
            {
                if (DDmes_pesquisa.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o mês.");
                if (TBAno_pesquisa.Text.Equals(string.Empty)) throw new ArgumentException("Informe o ano. ");

                BindGridViewAnalitico();
            MultiView1.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = 0;
                  }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                       "alert('" + ex.Message + "')", true);
            }
        }

        protected void btn_analitico_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = 0;
        }


        protected void BindParceiros(DropDownList drop)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_Parceiro> list = (from i in bd.CA_Parceiros select i).OrderBy(p => p.ParNomeFantasia);
                drop.Items.Clear();
                drop.DataSource = list.ToList();
                drop.DataBind();
            }
        }

        protected void btn_Sintetico_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDmes_pesquisa.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o mês.");
                if (TBAno_pesquisa.Text.Equals(string.Empty)) throw new ArgumentException("Informe o ano. ");

                BindSinteticoGeral();
                MultiView1.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 1;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                       "alert('" + ex.Message + "')", true);
            }
        }


        protected void btn_Sintetico_esp_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDmes_pesquisa.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o mês.");
                if (TBAno_pesquisa.Text.Equals(string.Empty)) throw new ArgumentException("Informe o ano. ");
                BindSinteticoEspecifico();
                MultiView1.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 2;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                       "alert('" + ex.Message + "')", true);
            }
        }

        protected void btn_pesquisasintetico_par_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDmes_pesquisa.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o mês.");
                if (TBAno_pesquisa.Text.Equals(string.Empty)) throw new ArgumentException("Informe o ano. ");

                BindSinteticoEspecifico();
                MultiView1.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 2;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                       "alert('" + ex.Message + "')", true);
            }
        }

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            Session["PRMT_Mes"] = DDmes_pesquisa.SelectedValue;
            Session["PRMT_Ano"] = TBAno_pesquisa.Text;
            Session["PRMT_Parceiro_nome"] = DDparceiro_pesquisa.SelectedItem.Text;
            Session["PRMT_Parceiro"] = DDparceiro_pesquisa.SelectedValue;

            var index =  MultiView2.ActiveViewIndex;
            switch (index)
            {
                case 0: Session["id"] = 50; break;
                case 1: Session["id"] = 51; break;
                case 2: Session["id"] = 52; break;
            }
            MultiView1.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = 3;
        }
    }
}