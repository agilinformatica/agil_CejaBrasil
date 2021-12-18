using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;


namespace ProtocoloAgil.pages
{
    public partial class LancamentoOcorrencia : Page
    {
        public struct OcrAprendiz
        {
            public DateTime Data { get; set; }
            public int Ordem { get; set; }
            public int Matricula { get; set; }
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public string Tipo { get; set; }
            public string Responsavel { get; set; }
            public string Observacao { get; set; }
            public string Emissor { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "aprendiz";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            }
            else
            {
                string eventTarget = Request["__EVENTTARGET"] ?? string.Empty;
                string eventArgument = Request["__EVENTARGUMENT"] ?? string.Empty;
                if (eventTarget == "SetSessionVariable")
                {
                    using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                    {
                        if (eventArgument.Equals(string.Empty)) return;
                        var nome = eventArgument;
                        var dados = from i in bd.CA_Aprendizs where i.Apr_Nome.Equals(nome) select i;
                        if (dados.Count() == 0) return;
                        var aluno = dados.First();
                        TBcodAprendiz.Text = aluno.Apr_Codigo.ToString();
                    }

                    if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
                    {
                        GridView1.Columns[7].Visible = false;
                        btn_save.Enabled = false;
                    }
                }
            }
        }

        protected void LimpaCampos()
        {
            BindOcorrencia();
            ddEmissor.SelectedValue = "";
            TBnomeAlu.Text = string.Empty;
            TBData.Text = string.Empty;
            TBnomeAlu.Text = string.Empty;
            TBcodAprendiz.Text = string.Empty;
            TBCodOcorrencia.Text = string.Empty;
            TBObservacao.Text = string.Empty;
            tb_notificacao.Text = string.Empty;
            tb_prev_devolucao.Text = string.Empty;
            tb_devolucao.Text = string.Empty;
        }


        public void BindOcorrencia()
        {
            using (var reposytory = new Repository<Ocorrencia>(new Context<Ocorrencia>()))
            {
                var datasource = new List<Ocorrencia>();
                datasource.AddRange(reposytory.All().OrderBy(p => p.OcoDescricao));
                DD_ocorrencia.Items.Clear();
                DD_ocorrencia.DataSource = datasource;
                DD_ocorrencia.DataBind();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }


        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }


        private void BindGridView()
        {
            try
            {
                if (TBCodigo.Text.Equals("") && TBNome.Text.Equals("")) throw new ArgumentException("Digite o nome ou o código do aluno.");
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var list = new ArrayList();
                    int count = 0;
                    if (!TBCodigo.Text.Equals(""))
                    {
                        var dados = from i in bd.CA_Aprendizs
                                    join p in bd.CA_SituacaoAprendizs on i.Apr_Situacao equals p.StaCodigo
                                    where i.Apr_Codigo == int.Parse(TBCodigo.Text)
                                    select new
                                    {
                                        i.Apr_Codigo,
                                        i.Apr_Nome,
                                        i.Apr_Telefone,
                                        Apr_Sexo = i.Apr_Sexo.Equals("M") ? "Masculino" : i.Apr_Sexo.Equals("F") ? "Feminino" : "ND",
                                        p.StaDescricao,
                                        i.Apr_Email
                                    };
                        list.AddRange(dados.ToList());
                        count = dados.Count();
                    }

                    if (!TBNome.Text.Equals(""))
                    {
                        var dados = from i in bd.CA_Aprendizs
                                    join p in bd.CA_SituacaoAprendizs on i.Apr_Situacao equals p.StaCodigo
                                    where SqlMethods.Like(i.Apr_Nome, "%" + TBNome.Text + "%")
                                    select new
                                    {
                                        i.Apr_Codigo,
                                        i.Apr_Nome,
                                        i.Apr_Telefone,
                                        Apr_Sexo = i.Apr_Sexo.Equals("M") ? "Masculino" : i.Apr_Sexo.Equals("F") ? "Feminino" : "ND",
                                        p.StaDescricao,
                                        i.Apr_Email
                                    };
                        list.AddRange(dados.ToList());
                        count = dados.Count();
                    }

                    HFRowCount.Value = count.ToString();
                    GridView2.DataSource = list;
                    GridView2.DataBind();
                }
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000125", ex);
            }
        }


        private void BindGridView2()
        {
            try
            {
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var list = new List<OcrAprendiz>();
                    var dados = from i in bd.CA_OcorrenciasAprendizs
                                join p in bd.CA_Ocorrencias on i.OcaCodOcorrencia equals p.OcoCodigo
                                join m in bd.CA_Aprendizs on i.OcaCodAprendiz equals m.Apr_Codigo
                                join n in bd.CA_Usuarios on i.OcaUsuarioocorrencia equals n.UsuCodigo
                                where m.Apr_Codigo == int.Parse(HFmatricula.Value)
                                select new OcrAprendiz
                                {
                                    Data = i.OcaDataOcorrencia,
                                    Ordem = i.OcaOrdem,
                                    Matricula = i.OcaCodAprendiz,
                                    Emissor = i.OcaEmissorOcorrencia.Equals("E") ? "Empresa" : "CejaBrasil",
                                    Nome = m.Apr_Nome,
                                    Descricao = p.OcoDescricao,
                                    Responsavel = n.UsuNome
                                };
                    list.AddRange(dados.OrderBy(x => x.Data));
                    Panel2.Visible = (list.Count().Equals(0));
                    GridView1.Visible = (!list.Count().Equals(0));
                    HFRowCount.Value = list.Count().ToString();
                    GridView1.DataSource = list;
                    GridView1.DataBind();
                }
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000125", ex);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = int.Parse(Session["active_view"].ToString());
        }

        protected void btn_gerar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = int.Parse(Session["active_view"].ToString());
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var bt = ((ImageButton)sender);
            var ordem = int.Parse(bt.CommandArgument);
            using (var repository = new Repository<OcorrenciaAprendiz>(new Context<OcorrenciaAprendiz>()))
            {
                var aula = repository.All().Where(p => p.OcaOrdem == ordem).First();
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(aula);
            }
            BindGridView2();
        }


        protected void IMBEditar_Click(object sender, ImageClickEventArgs e)
        {
            var bt = ((ImageButton)sender);
            Session["AlteraCodigo"] = bt.CommandArgument;
            Session["comando"] = "Alterar";
            TBnomeAlu.Enabled = false;
            DD_ocorrencia.Enabled = false;
            TBData.Enabled = false;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 2;
            Session["active_view"] = bt.Parent.Parent.Parent.Parent.ID.Equals("GridView1") ? 1 : 4;
        }

        private void PreencheCampos()
        {
            using (var repository = new Repository<OcorrenciaAprendiz>(new Context<OcorrenciaAprendiz>()))
            {
                var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
                BindOcorrencia();

                var ocorrencia = bd.CA_OcorrenciasAprendizs.ToList().Where(p => p.OcaOrdem == int.Parse(Session["AlteraCodigo"].ToString())).ToList();

                //var a = repository.All().Where(p => p.OcaOrdem.Equals(int.Parse(Session["AlteraCodigo"].ToString())));

                //  var ocorrencia = repository.All().Where(p => p.OcaOrdem.Equals(int.Parse(Session["AlteraCodigo"].ToString()))).First();


                HFmatricula.Value = ocorrencia.First().OcaCodAprendiz.ToString();
                //BindDisciplina();
                TBcodAprendiz.Text = ocorrencia.First().OcaCodAprendiz.ToString();
                ddEmissor.SelectedValue = ocorrencia.First().OcaEmissorOcorrencia == null ? "" : ocorrencia.First().OcaEmissorOcorrencia.ToString();
                txtOrdem.Text = ocorrencia.First().OcaOrdem.ToString();
                TBnomeAlu.Text = bd.CA_Aprendizs.Where(p => p.Apr_Codigo == ocorrencia.First().OcaCodAprendiz).First().Apr_Nome;
                TBData.Text = string.Format("{0:dd/MM/yyyy}", ocorrencia.First().OcaDataOcorrencia);
                DD_ocorrencia.SelectedValue = ocorrencia.First().OcaCodOcorrencia.ToString();
                TBCodOcorrencia.Text = ocorrencia.First().OcaCodOcorrencia.ToString();
                TBObservacao.Text = ocorrencia.First().OcaObservacoes;
                tb_notificacao.Text = ocorrencia.First().OcaDataEntrega == null ? "" : string.Format("{0:dd/MM/yyyy}", ocorrencia.First().OcaDataEntrega);
                tb_prev_devolucao.Text = ocorrencia.First().OcaPrevDevolucao == null ? "" : string.Format("{0:dd/MM/yyyy}", ocorrencia.First().OcaPrevDevolucao);
                tb_devolucao.Text = ocorrencia.First().OcaDevolucao == null ? "" : string.Format("{0:dd/MM/yyyy}", ocorrencia.First().OcaDevolucao);

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView2();
        }


        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView3.PageIndex = e.NewPageIndex;
            BindGridView3();
        }


        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddEmissor.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o campo Emissor Ocorrênica.");
                if (DD_ocorrencia.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um tipo de ocorrência.");
                if (TBData.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de lançamento da ocorrência.");
                if (TBObservacao.Text.Equals(string.Empty)) throw new ArgumentException("Informe uma observação para realizar o lançamento da ocorrência.");

                if (Session["Comando"].Equals("Alterar"))
                {
                    salvarOcorrenciaEdicao();
                }
                else
                {
                    //Retirada a pedido do usuário. Data 23/02/2015

                    //using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                    //{
                    //    var existe = bd.CA_OcorrenciasAprendizs.Where(p => p.OcaCodAprendiz == int.Parse(TBcodAprendiz.Text) && p.OcaDataOcorrencia == DateTime.Parse(TBData.Text) && p.OcaCodOcorrencia == int.Parse(DD_ocorrencia.SelectedValue)).Count();
                    //    if (existe > 0) throw new ArgumentException("Já existe uma ocorrência para este aprendiz nesta mesma data e código de ocorrência.");
                    //}
                    salvarOcorrencia();
                }

            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
        }

        public void salvarOcorrencia()
        {
            try
            {
                using (var repository = new Repository<OcorrenciaAprendiz>(new Context<OcorrenciaAprendiz>()))
                {
                    var ocorrencia = Session["comando"].Equals("Inserir") ? new OcorrenciaAprendiz() :
                    repository.All().Where(p => p.OcaOrdem == int.Parse(Session["AlteraCodigo"].ToString())).First();
                    ocorrencia.OcaCodAprendiz = int.Parse(TBcodAprendiz.Text);
                    ocorrencia.OcaCodOcorrencia = int.Parse(TBCodOcorrencia.Text);
                    ocorrencia.OcaDataOcorrencia = DateTime.Parse(TBData.Text);
                    ocorrencia.OcaUsuarioocorrencia = Session["codigo"].ToString();
                    ocorrencia.OcaObservacoes = TBObservacao.Text;

                    if (!ddEmissor.SelectedValue.Equals(string.Empty))
                        ocorrencia.OcaEmissorOcorrencia = ddEmissor.SelectedValue;

                    if (!tb_notificacao.Text.Equals(string.Empty))
                        ocorrencia.OcaDataEntrega = DateTime.Parse(tb_notificacao.Text);

                    if (!tb_prev_devolucao.Text.Equals(string.Empty))
                        ocorrencia.OcaPrevDevolucao = DateTime.Parse(tb_prev_devolucao.Text);

                    if (!tb_devolucao.Text.Equals(string.Empty))
                        ocorrencia.OcaDevolucao = DateTime.Parse(tb_devolucao.Text);

                    if (Session["comando"].Equals("Inserir"))
                    {
                        repository.Add(ocorrencia);
                        Session["comando"] = "Alterar";
                        Session["AlteraCodigo"] = ocorrencia.OcaOrdem;
                    }
                    else
                    {
                        repository.Edit(ocorrencia);
                    }
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000126", ex);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                       "alert('Açao realizada com sucesso.')", true);

                HFmatricula.Value = TBcodAprendiz.Text;
                BindGridView2();
                MultiView1.ActiveViewIndex = 1;
            }
        }

        public void salvarOcorrenciaEdicao()
        {
            try
            {
                using (var repository = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {

                    var codAlter = int.Parse(Session["AlteraCodigo"].ToString());
                    var ocorrencia = repository.CA_OcorrenciasAprendizs.FirstOrDefault(o => o.OcaOrdem == codAlter);

                    ocorrencia.OcaCodAprendiz = int.Parse(TBcodAprendiz.Text);
                    ocorrencia.OcaCodOcorrencia = int.Parse(TBCodOcorrencia.Text);
                    ocorrencia.OcaDataOcorrencia = DateTime.Parse(TBData.Text);
                    ocorrencia.OcaUsuarioocorrencia = Session["codigo"].ToString();
                    ocorrencia.OcaObservacoes = TBObservacao.Text;

                    if (!ddEmissor.SelectedValue.Equals(string.Empty))
                        ocorrencia.OcaEmissorOcorrencia = ddEmissor.SelectedValue;

                    if (!tb_notificacao.Text.Equals(string.Empty))
                        ocorrencia.OcaDataEntrega = DateTime.Parse(tb_notificacao.Text);

                    if (!tb_prev_devolucao.Text.Equals(string.Empty))
                        ocorrencia.OcaPrevDevolucao = DateTime.Parse(tb_prev_devolucao.Text);

                    if (!tb_devolucao.Text.Equals(string.Empty))
                        ocorrencia.OcaDevolucao = DateTime.Parse(tb_devolucao.Text);


                    repository.SubmitChanges();

                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000126", ex);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                       "alert('Açao realizada com sucesso.')", true);

                HFmatricula.Value = TBcodAprendiz.Text;
                BindGridView2();
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void DD_ocorrencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            TBCodOcorrencia.Text = DD_ocorrencia.SelectedValue;
        }

        protected void btn_pesquisar_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView)sender).SelectedRow;
            HFmatricula.Value = row.Cells[0].Text;
            LimpaCampos();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var aprendiz = bd.CA_Aprendizs.Where(p => p.Apr_Codigo == int.Parse(HFmatricula.Value)).First();
                TBnomeAlu.Text = aprendiz.Apr_Nome;
                TBcodAprendiz.Text = aprendiz.Apr_Codigo.ToString();
            }

            Session["comando"] = "Inserir";
            DD_ocorrencia.Enabled = true;
            TBnomeAlu.Enabled = false;
            TBData.Enabled = true;
            Session["active_view"] = 0;
            MultiView1.ActiveViewIndex = 2;
        }

        protected void IMB_alocacao_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            HFmatricula.Value = bt.CommandArgument;
            BindGridView2();
            MultiView1.ActiveViewIndex = 1;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            Session["id"] = "101";
            Session["prmt_ordem_ocorrencia"] = bt.CommandArgument;
            MultiView1.ActiveViewIndex = 3;
            Session["active_view"] = bt.Parent.Parent.Parent.Parent.ID.Equals("GridView1") ? 1 : 4;
        }

        protected void btn_pesquisa_data_Click(object sender, EventArgs e)
        {
            DDOcorrencia.Visible = false;
            lblTipoOcorrencia.Visible = false;
            lblTitulo.Text = "Pesquisa de Ocorrências por Data.";
            MultiView1.ActiveViewIndex = 4;
        }

        protected void btn_pesquisa_Click(object sender, EventArgs e)
        {
            BindGridView3();
        }

        private void BindGridView3()
        {
            try
            {
                var sql = "select OcaDataOcorrencia, OcaDataEntrega, OcaPrevDevolucao, OcaDevolucao, Apr_Nome, OcoDescricao, OcaOrdem, UsuNome, OcaCodAprendiz, OcaOrdem, CASE OcaEmissorOcorrencia WHEN 'C' THEN 'CejaBrasil' ELSE 'EMPRESA' END AS OcaEmissorOcorrencia " +
                    "from CA_OcorrenciasAprendiz inner join CA_Ocorrencias on OcaCodOcorrencia = OcoCodigo inner join  CA_Aprendiz on OcaCodAprendiz = Apr_Codigo " +
                    "inner join CA_Usuarios on OcaUsuarioocorrencia = UsuCodigo where 1 = 1";


                if (!DDOcorrencia.SelectedValue.Equals(string.Empty))
                {
                    sql += " And OcaCodOcorrencia = OcoCodigo And OcoCodigo = " + DDOcorrencia.SelectedValue + "";
                }

                switch (RB_tipo_Pesquisa.SelectedValue)
                {
                    case "1": sql += " And OcaDataOcorrencia BETWEEN '" + TBdataInicio.Text + "' AND '" + TBdataTermino.Text + "' ORDER BY OcaDataOcorrencia DESC "; break;
                    case "2": sql += " And OcaDataEntrega BETWEEN '" + TBdataInicio.Text + "' AND '" + TBdataTermino.Text + "'  ORDER BY OcaDataEntrega DESC "; break;
                    case "3": sql += " And OcaPrevDevolucao BETWEEN '" + TBdataInicio.Text + "' AND '" + TBdataTermino.Text + "'  ORDER BY OcaPrevDevolucao DESC "; break;
                    case "4": sql += " And OcaDevolucao BETWEEN '" + TBdataInicio.Text + "' AND '" + TBdataTermino.Text + "'  ORDER BY OcaDevolucao DESC "; break;
                }



                var datasource = new SqlDataSource { ID = "SDS_Grid3", ConnectionString = GetConfig.Config(), SelectCommand = sql };

                GridView3.DataSource = datasource;
                GridView3.DataBind();
                Panel5.Visible = (GridView3.Rows.Count.Equals(0));
                GridView1.Visible = (!GridView3.Rows.Count.Equals(0));
                HFRowCount.Value = GridView3.Rows.Count.ToString();
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000125", ex);
            }
        }

        protected void btn_cronograma_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBdataInicio.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de início.");
                if (TBdataTermino.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de término.");

                Session["id"] = "71";
                Session["tipo_pesquisa"] = RB_tipo_Pesquisa.SelectedValue;
                Session["prmnt_data_inicio"] = TBdataInicio.Text;
                Session["prmnt_data_Final"] = TBdataTermino.Text;
                MultiView1.ActiveViewIndex = 3;
                Session["active_view"] = 4;
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                          "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000123", ex);
            }
        }

        protected void btnDataTipo_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            DDOcorrencia.Visible = true;
            lblTipoOcorrencia.Visible = true;
            lblTitulo.Text = "Pesquisa de Ocorrências por Data / Tipo de Ocorrência";

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from o in db.CA_Ocorrencias
                             //where situacoes.Contains(S.StaCodigo)
                             select new { o.OcoCodigo, o.OcoDescricao }).ToList().OrderBy(item => item.OcoDescricao);

                DDOcorrencia.DataSource = query;
                DDOcorrencia.DataBind();

            }
        }

        protected void btnTotaisPorPeriodo_Click(object sender, EventArgs e)
        {
            PainelTotalPorPeriodo.Visible = true;
            MultiView1.ActiveViewIndex = 5;




            /*
             SELECT CA_Ocorrencias.OcoDescricao, Count(CA_OcorrenciasAprendiz.OcaCodAprendiz) AS Qtde
            FROM CA_OcorrenciasAprendiz INNER JOIN CA_Ocorrencias ON CA_OcorrenciasAprendiz.OcaCodOcorrencia = CA_Ocorrencias.OcoCodigo
            WHERE (((CA_OcorrenciasAprendiz.OcaDataOcorrencia) Between [d1] And [d2]))
            GROUP BY CA_Ocorrencias.OcoDescricao
            ORDER BY CA_Ocorrencias.OcoDescricao;
             */
        }
        private void BuscaTotaisPorPeriodo()
        {

            var sql = @"SELECT CA_Ocorrencias.OcoDescricao as descricao, Count(CA_OcorrenciasAprendiz.OcaCodAprendiz) AS QTD FROM CA_OcorrenciasAprendiz INNER JOIN CA_Ocorrencias ON CA_OcorrenciasAprendiz.OcaCodOcorrencia = CA_Ocorrencias.OcoCodigo WHERE (((CA_OcorrenciasAprendiz.OcaDataOcorrencia) Between '" + DateTime.Parse(txtDataInicioTotaisPorPeriodo.Text) + "' And '" + DateTime.Parse(txtDataTerminoTotaisPorPeriodo.Text) + "')) GROUP BY CA_Ocorrencias.OcoDescricao ORDER BY CA_Ocorrencias.OcoDescricao;";

            var datasource = new SqlDataSource { ID = "SDS_Grid3", ConnectionString = GetConfig.Config(), SelectCommand = sql };

            gridTotaisPorPeriodo.DataSource = datasource;
            gridTotaisPorPeriodo.DataBind();
            //  PainelTotalPorPeriodo.Visible = (gridTotaisPorPeriodo.Rows.Count.Equals(0));
            //  gridTotaisPorPeriodo.Visible = (!gridTotaisPorPeriodo.Rows.Count.Equals(0));
            HFRowCount.Value = gridTotaisPorPeriodo.Rows.Count.ToString();
            gridTotaisPorPeriodo.Visible = true;
        }



        protected void btnTotaisPorPeriodoPesquisa_Click(object sender, EventArgs e)
        {
            BuscaTotaisPorPeriodo();
        }

        protected void btnImprimirTotaisPorPeriodo_Click(object sender, EventArgs e)
        {
            if (txtDataInicioTotaisPorPeriodo.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de início.");
            if (txtDataTerminoTotaisPorPeriodo.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de término.");

            Session["DataInicio"] = DateTime.Parse(txtDataInicioTotaisPorPeriodo.Text);
            Session["DataTermino"] = DateTime.Parse(txtDataTerminoTotaisPorPeriodo.Text);

            Session["id"] = "91";
            Session["active_view"] = 5;
            MultiView1.ActiveViewIndex = 3;
        }
    }
}