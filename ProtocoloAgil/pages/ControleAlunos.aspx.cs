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
using System.Net;

namespace ProtocoloAgil.pages
{
    public partial class ControleAlunos : Page
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

        void Page_PreInit(Object sender, EventArgs e)
        {
            string tipo = "";
            if (Session["tipo"] != null)
                tipo = Session["tipo"].ToString();
            switch (tipo)
            {
                case "Interno":
                    Session["CurrentPage"] = "aprendiz";
                    MasterPageFile = "~/MPusers.Master";
                    break;
                case "Educador":
                    Session["CurrentPage"] = "notasprofessores";

                    MasterPageFile = "~/MaEducador.Master";
                    break;
                default:
                    Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            if (!IsPostBack)
            {

                CarregarDropSituacaoAprendiz();
                MultiView1.ActiveViewIndex = 0;
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                BindCursos();
                limpaAprendiz();
                divImprimir.Visible = false;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                btn_nova_alocacao.Enabled = false;
                btn_save.Enabled = false;
            }
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = GridView1.SelectedRow;
            Session["matricula"] = row.Cells[0].Text;
            Session["enable_Save"] = Session["tipo"].Equals("Educador") ? "Educador" : "User";
            using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
            {
                var aprendiz = repository.Find(int.Parse(row.Cells[0].Text));
                lb_codigo_aprendiz.Text = aprendiz.Apr_Codigo.ToString();
                lb_nome_aprendiz.Text = aprendiz.Apr_Nome;
            }
            Pn_aprendiz.Visible = true;

            MultiView1.ActiveViewIndex = 2;
            Session["Comando"] = "Alterar";
            GerarDiretorioaluno();
        }

        private void GerarDiretorioaluno()
        {
            var filePath = Server.MapPath(@"\files");
            var directory = new DirectoryInfo(filePath + @"\" + Session["Escola"] + @"\Alunos\" + HFmatricula.Value + @"\");
            if (!directory.Exists)
                directory.Create();
        }


        protected void listar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            BindAprendizes();
            limpaAprendiz();
        }

        private void limpaAprendiz()
        {
            lb_codigo_aprendiz.Text = "";
            lb_nome_aprendiz.Text = "";
            Pn_aprendiz.Visible = false;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            limpaAprendiz();
            MultiView1.ActiveViewIndex = 1;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            Session["Comando"] = "Inserir";
            Session["enable_Save"] = "User";
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindAprendizes();
        }

        protected void CarregarDropSituacaoAprendiz()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from S in db.CA_SituacaoAprendizs
                             select new { S.StaCodigo, S.StaDescricao }).ToList().OrderBy(item => item.StaDescricao);

                DDSituacao.DataSource = query;
                DDSituacao.DataBind();

            }
        }

        public void PreencheMunicipio()
        {
            var valor = DDEstado.SelectedValue;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from M in bd.MA_Municipios
                             where M.MunIEstado == valor
                             select new { M.MunIDescricao }).ToList();

                DDMunicipio.DataSource = query;
                DDMunicipio.DataBind();
            }
        }


        protected void BindAprendizes()
        {
           using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_Aprendiz> lista;

                lista = from i in bd.CA_Aprendizs select i;

                if (!TBNome.Text.Equals(string.Empty))
                {
                    lista = from i in bd.CA_Aprendizs where SqlMethods.Like(i.Apr_Nome, "%" + TBNome.Text + "%") select i;
                    Session["NomeAprendizDigitado"] = TBNome.Text;
                }
                if (!TBCodigo.Text.Equals(string.Empty))
                {
                    lista = from i in bd.CA_Aprendizs where i.Apr_Codigo == int.Parse(TBCodigo.Text) select i;
                    Session["CodDigitado"] = TBCodigo.Text;
                }

                if (!DDSituacao.SelectedValue.Equals(string.Empty))
                {
                    lista = lista.Where(item => item.Apr_Situacao.Equals(DDSituacao.SelectedValue));
                    Session["CodSituacao"] = DDSituacao.SelectedValue;
                }

                if (!txtCpf.Text.Equals(string.Empty))
                {
                    lista = lista.Where(item => item.Apr_CPF.Equals(txtCpf.Text));
                }


                if (!DDMunicipio.SelectedValue.Equals(string.Empty))
                {
                    lista = lista.Where(item => item.Apr_Cidade.Equals(DDMunicipio.SelectedValue));
                }

                var situacao = (from i in bd.CA_SituacaoAprendizs select i).ToList();
                var datasource = lista.ToList().Select(p => new AprendizPesquisa
                {
                    Apr_Codigo = p.Apr_Codigo.ToString(),
                    Apr_Nome = p.Apr_Nome,
                    Apr_Sexo = (p.Apr_Sexo.Equals("M") ? "Masculino" : "Feminino"),
                    Apr_Telefone = Funcoes.FormataTelefone(p.Apr_Telefone),
                    Apr_Email = p.Apr_Email,
                    StaDescricao = p.Apr_Situacao == 0 ? "" : (situacao.Where(x => x.StaCodigo.Equals(p.Apr_Situacao)).First().StaDescricao),
                    Apr_AreaAtuacao = p.Apr_AreaAtuacao == null ? "" : p.Apr_AreaAtuacao.ToString(),
                    Apr_PlanoCurricular = p.Apr_PlanoCurricular == null ? new short() : (short)p.Apr_PlanoCurricular

                }).ToList().OrderBy(p => p.Apr_Nome);
                GridView1.DataSource = datasource.ToList();

                HFRowCount.Value = datasource.Count().ToString();
                GridView1.DataBind();

                if (int.Parse(HFRowCount.Value) > 0)
                {
                    divImprimir.Visible = true;
                }
                else
                {
                    divImprimir.Visible = false;
                }

            }
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var aprendiz = Convert.ToInt32(button.CommandArgument);
            using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(aprendiz);
            }
            GridView1.DataBind();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            IFrame2.Visible = false;
            var codigo = ((ImageButton)sender).CommandArgument;
            Session["PRMT_Aprendiz"] = codigo;



            using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
            {
                var aprendiz = repository.Find(int.Parse(codigo));
                lb_codigo_aprendiz.Text = aprendiz.Apr_Codigo.ToString();
                lb_nome_aprendiz.Text = aprendiz.Apr_Nome;
            }


            MultiView1.ActiveViewIndex = 3;
            RemoveOpcaoContratoDropDown();
        }

        /// <summary>
        /// remove os itens do drop down tipo relatorio
        /// caso tipo seja empresa ou cejabrasil
        /// andreghorst 05/06/2014
        /// </summary>
        void RemoveOpcaoContratoDropDown()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from A in bd.CA_AlocacaoAprendizs
                             where A.ALAAprendiz.Equals(Session["PRMT_Aprendiz"])
                             select new { A.ALApagto }).FirstOrDefault();

                if (query.ALApagto.Equals("E"))
                {
                    if (DDtipo_relatorio.Items.FindByValue("9") == null)    //como retirou, ele tem que incluir...   menu estatico            
                        DDtipo_relatorio.Items.Insert(4, new ListItem("Contrato Parcial", "9"));

                    DDtipo_relatorio.Items.Remove(DDtipo_relatorio.Items.FindByValue("8"));
                    DDtipo_relatorio.DataBind();
                }
                else
                {
                    if (DDtipo_relatorio.Items.FindByValue("8") == null)   //como retirou, ele tem que incluir...   menu estatico   
                        DDtipo_relatorio.Items.Insert(3, new ListItem("Contrato Integral", "8"));

                    DDtipo_relatorio.Items.Remove(DDtipo_relatorio.Items.FindByValue("9"));
                    DDtipo_relatorio.DataBind();
                }
            }
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindAprendizes();
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

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            HFRowCount.Value = e.AffectedRows.ToString();
        }

        protected void IMB_alocacao_Click(object sender, ImageClickEventArgs e)
        {
            Session["AreaAtuacao"] = GridView1.DataKeys[0]["Apr_AreaAtuacao"].ToString();
            var bt = (ImageButton)sender;
            var aprendiz = int.Parse(bt.CommandArgument);

            using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
            {
                var a = repository.Find(aprendiz);

                lb_codigo_aprendiz.Text = a.Apr_Codigo.ToString();
                lb_nome_aprendiz.Text = a.Apr_Nome;
            }


            BindGridView(aprendiz);
            Session["prmt_aprendiz_selecionado"] = aprendiz;
            MultiView1.ActiveViewIndex = 4;
            //Alocação
            Session["Apr_Codigo"] = GridView1.Rows[0].Cells[0].Text.ToString();  //pega a codigo apr final no grid           
            pGerarCronograma.Visible = true;
            PreencherDropDownPlanoCurricular();
            }

        private void BindGridView(int aprendiz)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var list = new List<View_AlocacoesAluno>();
                var dados02 = bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == aprendiz).OrderByDescending(p => p.ALADataInicio);
                list.AddRange(dados02.ToList());
                HFRowCount.Value = list.Count().ToString();
                GridView2.DataSource = list;
                pn_info.Visible = (list.Count().Equals(0));

                GridView2.DataBind();

                if (list.Count().ToString().Equals("0"))
                {
                    tabela1.Visible = false;
                    tabela2.Visible = false;
                }
                else
                {
                    tabela1.Visible = true;
                    tabela2.Visible = true;
                }

            }
        }

        protected void btn_nova_alocacao_Click(object sender, EventArgs e)
        {
            IniciaCampos();
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 5;
        }

        private void IniciaCampos()
        {
            LimpaCampos();
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var aprendiz = Session["prmt_aprendiz_selecionado"].ToString();
                var dados = bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == int.Parse(aprendiz)).OrderByDescending(p => p.ALADataInicio);
                if (dados.Count() > 0)
                {
                    var alocacao = dados.First();
                    LB_matricula.Text = alocacao.ALAAprendiz.ToString();
                    lb_aprendiz.Text = alocacao.Apr_Nome;

                    BindCursos();
                    DD_Turma.Items.Clear();
                    DD_tipo_pagamento.SelectedValue = alocacao.ALApagto == null ? "" : alocacao.ALApagto.Equals("Empresa") ? "E" : "C";
                    DD_parceiro.SelectedValue = alocacao.ParCodigo.ToString();
                    BindUnidades(alocacao.ParCodigo.ToString(), DDunidade_parceiro);
                    DDunidade_parceiro.SelectedValue = alocacao.ALAUnidadeParceiro.ToString();
                    BindUsuarios();
                    BindOrientador(alocacao.ALAUnidadeParceiro, DD_orientador);
                    if (!string.IsNullOrEmpty(alocacao.OriNome))
                    {
                        var orientador = bd.CA_Orientadors.Where(p => p.OriNome == alocacao.OriNome && p.OriUnidadeParceiro == alocacao.ALAUnidadeParceiro).First();
                        DD_orientador.SelectedValue = orientador.OriCodigo.ToString();
                    }

                    DD_area_atuacao.DataBind();
                    if (!string.IsNullOrEmpty(alocacao.AreaDescricao))
                    {
                        var area = bd.CA_AreaAtuacaos.Where(p => p.AreaDescricao == alocacao.AreaDescricao).First();
                        DD_area_atuacao.SelectedValue = area.AreaCodigo.ToString();
                    }

                    BindUsuarios();
                    DDusuario.SelectedValue = alocacao.ALATutor;
                    BindSituacoes();
                    DD_situacao.SelectedValue = alocacao.Apr_Situacao.ToString();

                    if (alocacao.ALAInicioExpediente != null)
                    {
                        TMS_inicio.Hour = ((DateTime)alocacao.ALAInicioExpediente).Hour;
                        TMS_inicio.Minute = ((DateTime)alocacao.ALAInicioExpediente).Minute;
                        TMS_inicio.AmPm = ((DateTime)alocacao.ALAInicioExpediente).Hour < 12 ? TimeSelector.AmPmSpec.AM : TimeSelector.AmPmSpec.PM;
                    }

                    if (alocacao.ALATerminoExpediente != null)
                    {
                        TMS_final.Hour = ((DateTime)alocacao.ALATerminoExpediente).Hour;
                        TMS_final.Minute = ((DateTime)alocacao.ALATerminoExpediente).Minute;
                        TMS_final.AmPm = ((DateTime)alocacao.ALATerminoExpediente).Hour < 12 ? TimeSelector.AmPmSpec.AM : TimeSelector.AmPmSpec.PM;
                    }

                    TBValorBolsa.Text = alocacao.ALAValorBolsa == null ? string.Format("{0:f2}", 0) : string.Format("{0:f2}", alocacao.ALAValorBolsa);
                    TBValorTaxa.Text = alocacao.ALAValorTaxa == null ? string.Format("{0:f2}", 0) : string.Format("{0:f2}", alocacao.ALAValorTaxa);
                    TBValorEncargos.Text = alocacao.ALAValorEncargos == null ? string.Format("{0:f2}", 0) : string.Format("{0:f2}", alocacao.ALAValorEncargos);

                    var aluno = bd.CA_Aprendizs.Where(p => p.Apr_Codigo == int.Parse(aprendiz)).First();

                    TB_inicio_aprendizagem.Text = string.Format("{0:dd/MM/yyyy}", aluno.Apr_InicioAprendizagem);
                    TB_prev_Fim_aprendizagem.Text = string.Format("{0:dd/MM/yyyy}", aluno.Apr_PrevFimAprendizagem);
                    TB_data_desligamento.Text = string.Format("{0:dd/MM/yyyy}", aluno.Apr_PrevFimAprendizagem);

                    ShowAdress(alocacao.ALAUnidadeParceiro);
                }
                else
                {
                    var dados01 = bd.CA_Aprendizs.Where(p => p.Apr_Codigo == int.Parse(aprendiz)).First();
                    LB_matricula.Text = dados01.Apr_Codigo.ToString();
                    lb_aprendiz.Text = dados01.Apr_Nome;
                    TMS_inicio.Hour = DateTime.Now.Hour;
                    TMS_inicio.Minute = DateTime.Now.Minute;
                    TMS_inicio.AmPm = DateTime.Now.Hour < 12 ? TimeSelector.AmPmSpec.AM : TimeSelector.AmPmSpec.PM;
                    TMS_final.Hour = DateTime.Now.Hour;
                    TMS_final.Minute = DateTime.Now.Minute;
                    TMS_final.AmPm = DateTime.Now.Hour < 12 ? TimeSelector.AmPmSpec.AM : TimeSelector.AmPmSpec.PM;
                }

                DD_SituacaoAlocacao.SelectedValue = "A";

            }
        }


        protected void DDcursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDcurso.SelectedValue.Equals(string.Empty)) return;
            BindTurmas(DDcurso.SelectedValue, DD_Turma);
        }

        protected void DD_Turma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DD_Turma.SelectedValue.Equals(string.Empty)) return;
        }

        protected void DD_parceiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DD_parceiro.SelectedValue.Equals(string.Empty)) return;
            using (var reposytory = new Repository<Parceiros>(new Context<Parceiros>()))
            {
                var parceiro = reposytory.Find(int.Parse(DD_parceiro.SelectedValue));
                TBValorBolsa.Text = string.Format("{0:F2}", parceiro.ParBolsa20Horas);
                TBValorTaxa.Text = string.Format("{0:F2}", parceiro.ParTaxa20Horas);
            }
            BindUnidades(DD_parceiro.SelectedValue, DDunidade_parceiro);
        }

        protected void BindUnidades(string curso, DropDownList dropdown)
        {// thassio

            if (DD_parceiro.SelectedValue.Equals(string.Empty)) return;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var list = (from p in bd.CA_ParceirosUnidades
                            where p.ParUniCodigoParceiro == int.Parse(DD_parceiro.SelectedValue)
                            orderby p.ParUniDescricao
                            select new
                            {
                                ParUniDescricao = p.ParUniDescricao + "    -    " + p.ParUniCNPJ.Substring(0, 2) + "." + p.ParUniCNPJ.Substring(2, 3) + "." + p.ParUniCNPJ.Substring(5, 3) + "/" + p.ParUniCNPJ.Substring(8, 4) +
                                    "-" + p.ParUniCNPJ.Substring(12),
                                ParUniCodigo = p.ParUniCodigo
                            });
                dropdown.Items.Clear();
                dropdown.DataSource = list.ToList();
                dropdown.DataBind();
            }
        }

        protected void BindOrientador(int unidade, DropDownList dropdown)
        {
            if (DD_parceiro.SelectedValue.Equals(string.Empty)) return;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var list = bd.CA_Orientadors.Where(p => p.OriUnidadeParceiro == unidade).OrderBy(p => p.OriNome);
                dropdown.Items.Clear();
                dropdown.DataSource = list.ToList();
                dropdown.DataBind();
            }
        }

        protected void BindUsuarios()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_Usuario> list = (from i in bd.CA_Usuarios select i).OrderBy(p => p.UsuNome);
                DDusuario.Items.Clear();
                DDusuario.DataSource = list.ToList();
                DDusuario.DataBind();
            }
        }

        protected void BindTurmas(string curso, DropDownList dropdown)
        {
            if (DDcurso.SelectedValue.Equals(string.Empty)) return;
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_Turma> list = bd.CA_Turmas.Where(p => p.TurCurso.Equals(DDcurso.SelectedValue)).OrderBy(p => p.TurNome);
                dropdown.Items.Clear();
                dropdown.DataSource = list.ToList();
                dropdown.DataBind();
            }
        }

        protected void BindCursos()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_Curso> list = (from i in bd.CA_Cursos select i).OrderBy(p => p.CurDescricao);
                DDcurso.Items.Clear();
                DDcurso.DataSource = list.ToList();
                DDcurso.DataBind();
            }
        }

        public static string FormataCNPJ(string cnpj)
        {
            if (cnpj == null || cnpj.Equals(string.Empty))
                return "";

            if (cnpj.Length < 14)
                return cnpj;

            var aux = cnpj.Substring(0, 2) + "." + cnpj.Substring(2, 3) + "." + cnpj.Substring(5, 3) + "/" + cnpj.Substring(8, 4) +
                      "-" + cnpj.Substring(12);
            return aux;
        }
        protected void BindParceiros()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var list = (from i in bd.CA_Parceiros
                            orderby i.ParNomeFantasia
                            select new
                            {
                                a = i.ParNomeFantasia + "    -    " + i.ParCNPJ.Substring(0, 2) + "." + i.ParCNPJ.Substring(2, 3) + "." + i.ParCNPJ.Substring(5, 3) + "/" + i.ParCNPJ.Substring(8, 4) +
                                    "-" + i.ParCNPJ.Substring(12),
                                b = i.ParCodigo
                            });
                DD_parceiro.Items.Clear();
                DD_parceiro.DataSource = list.ToList();
                DD_parceiro.DataBind();
            }
        }

        protected void BindSituacoes()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_SituacaoAprendiz> list = (from i in bd.CA_SituacaoAprendizs select i).OrderBy(p => p.StaDescricao);
                DD_situacao.Items.Clear();
                DD_situacao.DataSource = list.ToList();
                DD_situacao.DataBind();
            }
        }

        protected void BindMotivos()
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                IQueryable<CA_MotivoDesligamento> list = (from i in bd.CA_MotivoDesligamentos select i).OrderBy(p => p.MotDescricao);
                DD_motivo_desligamento.Items.Clear();
                DD_motivo_desligamento.DataSource = list.ToList();
                DD_motivo_desligamento.DataBind();
            }
        }

        public static string GetData(int id)
        {
            using (var repository = new Repository<Turma>(new Context<Turma>()))
            {
                var meses = repository.Find(id).TurNumeroMeses;
                return meses.ToString();
            }
        }

        protected void DDunidade_parceiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDunidade_parceiro.SelectedValue.Equals(string.Empty)) return;
            BindOrientador(int.Parse(DDunidade_parceiro.SelectedValue), DD_orientador);
            ShowAdress(int.Parse(DDunidade_parceiro.SelectedValue));
        }

        private void ShowAdress(int unidade)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = bd.CA_ParceirosUnidades.Where(p => p.ParUniCodigo == unidade).First();
                var sb = new StringBuilder();
                sb.Append(dados.ParUniEndereco);
                if (dados.ParUniNumeroEndereco != null)
                {
                    sb.Append(",nº " + dados.ParUniNumeroEndereco);
                }
                sb.Append(dados.ParUniComplemento);
                sb.Append(", Bairro: " + (dados.ParUniBairro ?? "Não Informado"));
                sb.Append(", " + dados.ParUniCidade + " / " + dados.ParUniEstado);
                sb.Append(". CEP: " + (dados.ParUniCEP == null ? "Não Informado" : Funcoes.FormataCep(dados.ParUniCEP)));
                sb.Append(". Telefone: " + ((dados.ParUniTelefone == null) || (dados.ParUniTelefone.Length < 8) ? "Não Informado" : Funcoes.FormataTelefone(dados.ParUniTelefone)));
                sb.Append(". Celular: " + ((dados.ParUniCelular == null) || (dados.ParUniCelular.Length < 8) ? "Não Informado" : Funcoes.FormataTelefone(dados.ParUniCelular)));
                sb.Append(". E-mail: " + (dados.ParUniEmail ?? "Não Informado"));
                lb_endereco.Text = sb.ToString();
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {

                var selecionado = Session["prmt_aprendiz_selecionado"].ToString();
                if (!bool.Parse(HFConfirmSave.Value)) return;
                if (DDcurso.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um curso.");
                if (DD_Turma.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma turma.");
                if (DD_tipo_pagamento.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione o responsável pelo pagamento do aprendiz.");
                if (TB_inicio_aprendizagem.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de início de aprendizagem do aprendiz.");
                if (DDunidade_parceiro.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma unidade do parceiro.");
                if (DDusuario.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione um tutor para o aprendiz.");
                if (TBdataInicio.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de início da aprendizagem.");
                if (TBDataPrev.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de término prevista da aprendizagem.");
                DateTime data;
                if (DD_area_atuacao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma área de atuação");
                if (DD_situacao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma situação.");
                if (!DateTime.TryParse(TBdataInicio.Text, out data)) throw new ArgumentException("Data de início inválida.");
                if (!DateTime.TryParse(TBDataPrev.Text, out data)) throw new ArgumentException("Data de término prevista inválida.");
                if (!TBdataTermino.Text.Equals(string.Empty) && !DateTime.TryParse(TBdataTermino.Text, out data)) throw new ArgumentException("Data de término inválida.");

                if (!DDNovaTurma.SelectedValue.Equals(string.Empty) && TbInicioNovaTurma.Text.Equals(string.Empty)) throw new ArgumentException("Para mudança de turma, preencha a Data Mudança de turma.");
                if (!DDNovaTurma.SelectedValue.Equals(string.Empty) && TbMotivoMudancaTurma.Text.Equals(string.Empty)) throw new ArgumentException("Para mudança de turma, preencha o Motivo da Mudança de turma.");

                var horainicio = new DateTime(1900, 1, 1, TMS_inicio.Hour, TMS_inicio.Minute, 0);
                var horafinal = new DateTime(1900, 1, 1, TMS_final.Hour, TMS_final.Minute, 0);


                const string sqiInsert = "INSERT INTO CA_AlocacaoAprendiz(ALAAprendiz,ALATurma,ALAUnidadeParceiro,ALAStatus,ALATutor, " +
                        "ALADataInicio,ALADataPrevTermino,ALAInicioExpediente,ALATerminoExpediente,ALAValorBolsa,ALApagto , " +
                        "ALAValorTaxa,ALAObservacao, ALAOrientador, ALAMotivoDesligamento, ALAUsuarioCadastro, ALAUsuarioDataCadastro, ALAValorEncargos, AlaMesPagto01Uniforme, AlaAnoPagto01Uniforme, AlaMesPagto02Uniforme, AlaAnoPagto02Uniforme ) " +
                        "VALUES (@ALAAprendiz,@ALATurma,@ALAUnidadeParceiro,@ALAStatus,@ALATutor,@ALADataInicio,@ALADataPrevTermino, @ALAInicioExpediente, " +
                        "@ALATerminoExpediente,@ALAValorBolsa,@ALApagto,@ALAValorTaxa,@ALAObservacao,@ALAOrientador,@ALAMotivoDesligamento,@ALAUsuarioCadastro, " +
                        "@ALAUsuarioDataCadastro,@ALAValorEncargos, @AlaMesPagto01Uniforme, @AlaAnoPagto01Uniforme, @AlaMesPagto02Uniforme, @AlaAnoPagto02Uniforme )";

                const string sqlUpdate = @"UPDATE CA_AlocacaoAprendiz SET ALAAprendiz = @ALAAprendiz, ALATurma = @ALATurma, ALApagto = @ALApagto,
                                            ALAUnidadeParceiro =@ALAUnidadeParceiro, ALAStatus = @ALAStatus, ALATutor = @ALATutor, ALADataInicio =@ALADataInicio, 
                                            ALADataPrevTermino = @ALADataPrevTermino, ALADataTermino = @ALADataTermino, ALAInicioExpediente = @ALAInicioExpediente, 
                                            ALATerminoExpediente = @ALATerminoExpediente, ALAValorBolsa = @ALAValorBolsa, ALAValorTaxa = @ALAValorTaxa, 
                                            ALAObservacao = @ALAObservacao, ALAOrientador = @ALAOrientador, ALAMotivoDesligamento = @ALAMotivoDesligamento,
                                            ALAUsuarioAlteracao = @ALAUsuarioAlteracao , ALAUsuarioDataAlteracao = @ALAUsuarioDataAlteracao , ALAValorEncargos = @ALAValorEncargos, 
                                            ALATurmaAnterior = @ALATurmaAnterior, ALAUsuarioMudaTurma = @ALAUsuarioMudaTurma, ALADataMudaTurma = @ALADataMudaTurma, 
                                            ALAMotivoMudaTurma = @ALAMotivoMudaTurma, AlaMesPagto01Uniforme = @AlaMesPagto01Uniforme, AlaAnoPagto01Uniforme = @AlaAnoPagto01Uniforme, AlaMesPagto02Uniforme = @AlaMesPagto02Uniforme, AlaAnoPagto02Uniforme = @AlaAnoPagto02Uniforme
                                            Where ALAOrdem = @codigo";

                var parameters = new List<SqlParameter> { 
                        new SqlParameter("ALAAprendiz", selecionado), 
                        new SqlParameter("ALAStatus", DD_SituacaoAlocacao.SelectedValue), 
                        new SqlParameter("ALATutor", DDusuario.SelectedValue),
                        new SqlParameter("ALADataInicio",TBdataInicio.Text), 
                        new SqlParameter("ALADataPrevTermino", TBDataPrev.Text),
                        new SqlParameter("ALAInicioExpediente", horainicio), 
                        new SqlParameter("ALATerminoExpediente", horafinal),
                        new SqlParameter("ALAValorTaxa",  TBValorTaxa.Text.Replace(",",".") ), 
                        new SqlParameter("ALAValorBolsa", TBValorBolsa.Text.Replace(",",".") ),
                        new SqlParameter("ALAValorEncargos", TBValorEncargos.Text.Replace(",",".") ),
                        new SqlParameter("ALAObservacao", TBObservacao.Text), 
                        new SqlParameter("ALApagto", DD_tipo_pagamento.SelectedValue),
                        new SqlParameter("ALAUnidadeParceiro", DDunidade_parceiro.SelectedValue),
                        !TBdataTermino.Text.Equals(string.Empty) ? new SqlParameter("ALADataTermino", TBdataTermino.Text) : new SqlParameter("ALADataTermino", DBNull.Value),
                        !DD_orientador.SelectedValue.Equals(string.Empty) ? new SqlParameter("ALAOrientador", DD_orientador.SelectedValue) : new SqlParameter("ALAOrientador", DBNull.Value),
                        !DD_motivo_desligamento.SelectedValue.Equals(string.Empty) ? new SqlParameter("ALAMotivoDesligamento", DD_motivo_desligamento.SelectedValue) : new SqlParameter("ALAMotivoDesligamento", DBNull.Value),
                        !DDNovaTurma.SelectedValue.Equals(string.Empty) ? new SqlParameter("ALATurma", DDNovaTurma.SelectedValue) : new SqlParameter("ALATurma", DD_Turma.SelectedValue),
                        !DDNovaTurma.SelectedValue.Equals(string.Empty) ? new SqlParameter("ALATurmaAnterior", DD_Turma.SelectedValue) : new SqlParameter("ALATurmaAnterior", DBNull.Value),
                        !DDNovaTurma.SelectedValue.Equals(string.Empty) ? new SqlParameter("ALAUsuarioMudaTurma", Session["codigo"].ToString()) : new SqlParameter("ALAUsuarioMudaTurma", DBNull.Value),
                        !TbInicioNovaTurma.Text.Equals(string.Empty) ? new SqlParameter("ALADataMudaTurma", TbInicioNovaTurma.Text) : new SqlParameter("ALADataMudaTurma", DBNull.Value),
                        !TbMotivoMudancaTurma.Text.Equals(string.Empty) ? new SqlParameter("ALAMotivoMudaTurma", TbMotivoMudancaTurma.Text) : new SqlParameter("ALAMotivoMudaTurma", DBNull.Value),

                        
                        
                        !DDMesUniforme1.SelectedValue.Equals(string.Empty) ? new SqlParameter("AlaMesPagto01Uniforme", DDMesUniforme1.SelectedValue) : new SqlParameter("AlaMesPagto01Uniforme", DBNull.Value),
                        !txtMesAnoUniforme1.Text.Equals(string.Empty) ? new SqlParameter("AlaAnoPagto01Uniforme", txtMesAnoUniforme1.Text) : new SqlParameter("AlaAnoPagto01Uniforme", DBNull.Value),

                        !DDMesUniforme2.SelectedValue.Equals(string.Empty) ? new SqlParameter("AlaMesPagto02Uniforme", DDMesUniforme2.SelectedValue) : new SqlParameter("AlaMesPagto02Uniforme", DBNull.Value),
                        !txtMesAnoUniforme2.Text.Equals(string.Empty) ? new SqlParameter("AlaAnoPagto02Uniforme", txtMesAnoUniforme2.Text) : new SqlParameter("AlaAnoPagto02Uniforme", DBNull.Value)
                };

               if (Session["comando"].Equals("Alterar"))
                {
                    parameters.Add(new SqlParameter("codigo", Session["PRMT_ordem"].ToString()));
                    parameters.Add(new SqlParameter("ALAUsuarioAlteracao", Session["codigo"].ToString()));
                    parameters.Add(new SqlParameter("ALAUsuarioDataAlteracao", DateTime.Today));
                }
                else
                {
                    using (var repository = new Repository<AprendizUnidade>(new Context<AprendizUnidade>()))
                    {
                        var aprendiz = repository.All().Where(p => p.ALAAprendiz == int.Parse(selecionado) && p.ALATurma == int.Parse(DD_Turma.SelectedValue)
                            && p.ALAUnidadeParceiro == int.Parse(DDunidade_parceiro.SelectedValue));
                        if (aprendiz.Count() > 0) throw new ArgumentException("Já existe uma alocação nesta turma para este aprendiz.");
                    }
                    parameters.Add(new SqlParameter("ALAUsuarioCadastro", Session["codigo"].ToString()));
                    parameters.Add(new SqlParameter("ALAUsuarioDataCadastro", DateTime.Today));
                }

                var con = new Conexao();
                con.Alterar(Session["comando"].Equals("Inserir") ? sqiInsert : sqlUpdate, parameters.ToArray());

                // Adiciona ao aluno uma area de atuação e modifica as datas de início de aprendizagem e as de desligamento.
                using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
                {

                    var aprendiz = repository.Find(int.Parse(selecionado));
                    aprendiz.Apr_AreaAtuacao = int.Parse(DD_area_atuacao.SelectedValue);

                    if (TB_inicio_aprendizagem.Text.Equals(string.Empty)) aprendiz.Apr_InicioAprendizagem = null;
                    else aprendiz.Apr_InicioAprendizagem = DateTime.Parse(TB_inicio_aprendizagem.Text);

                    if (TB_data_desligamento.Text.Equals(string.Empty)) aprendiz.Apr_FimAprendizagem = null;
                    else aprendiz.Apr_FimAprendizagem = DateTime.Parse(TB_data_desligamento.Text);

                    if (TB_prev_Fim_aprendizagem.Text.Equals(string.Empty)) aprendiz.Apr_PrevFimAprendizagem = null;
                    else aprendiz.Apr_PrevFimAprendizagem = DateTime.Parse(TB_prev_Fim_aprendizagem.Text);

                    aprendiz.Apr_Situacao = int.Parse(DD_situacao.SelectedValue);
                    repository.Edit(aprendiz);
                }

                if (!TB_data_desligamento.Text.Equals("") && !TB_data_desligamento.Text.Equals(null))
                {
                    var sql = @"delete CA_AulasDisciplinasAprendiz where AdiCodAprendiz = " + LB_matricula.Text + " and AdiDataAula > '" + TB_data_desligamento.Text + "'";
                    var cone = new Conexao();
                    cone.Alterar(sql);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('Ação realizada com sucesso. Agora você deverá gerar o cronograma do aprendiz')", true);

                GridView1.DataBind();


            }

            catch (SqlException a)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                  "alert('ERRO - Esta alocação ja foi incluída');", true);

            }

            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000122", ex);
            }
        }

        private void CriaPesquisa(int unidade, int aprendiz, DateTime inicio)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var parceiro = bd.CA_ParceirosUnidades.Where(p => p.ParUniCodigo == unidade).First().ParUniCodigoParceiro;
                var pesquisas = bd.CA_Pesquisas.Where(p => p.PesPublicoAlvo == "P");
                using (var repository = new Repository<PesquisaParceiro>(new Context<PesquisaParceiro>()))
                {
                    foreach (var item in pesquisas)
                    {
                        var pesquisa = new PesquisaParceiro { PepPesquisaCodigo = item.PesCodigo, PepParceiroCodigo = parceiro, PepRealizada = "N", PepAprendiz = aprendiz };
                        var data = inicio;

                        if (item.PesCodigo == 2)
                        {
                            //1º data da pesquisa: 2 meses após a data de alocação.
                            DateTime dataref = data.AddMonths(2);
                            pesquisa.PepAno = dataref.Year.ToString();
                            pesquisa.PepMes = dataref.Month;
                            repository.Add(pesquisa);
                        }
                        else
                        {
                            //1º data da pesquisa: 6 meses após a data de alocação.
                            DateTime dataref = data.AddMonths(6);
                            pesquisa.PepAno = dataref.Year.ToString();
                            pesquisa.PepMes = dataref.Month;
                            repository.Add(pesquisa);
                            //2º data da pesquisa: 12 meses após a data de alocação.
                            dataref = dataref.AddMonths(6);
                            pesquisa.PepAno = dataref.Year.ToString();
                            pesquisa.PepMes = dataref.Month;
                            repository.Add(pesquisa);
                        }
                    }
                }
            }
        }

        private void PreencheCampos()
        {
            LimpaCampos();
            using (var reposytory = new Repository<AprendizUnidade>(new Context<AprendizUnidade>()))
            {
                var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
                var alocacao = reposytory.All().Where(p => p.ALAOrdem == int.Parse(Session["PRMT_ordem"].ToString())).First();
                var dados = bd.View_AlocacoesAlunos.Where(p => p.ALAOrdem.Equals(alocacao.ALAOrdem)).First();
                var aprendiz = bd.CA_Aprendizs.Where(p => p.Apr_Codigo == alocacao.ALAAprendiz).First();
                LB_matricula.Text = dados.ALAAprendiz.ToString();
                lb_aprendiz.Text = dados.Apr_Nome;
                TMS_inicio.Hour = alocacao.ALAInicioExpediente == null ? 8 : ((DateTime)alocacao.ALAInicioExpediente).Hour;
                TMS_inicio.Minute = alocacao.ALAInicioExpediente == null ? 0 : ((DateTime)alocacao.ALAInicioExpediente).Minute;
                TMS_inicio.AmPm = alocacao.ALAInicioExpediente == null || ((DateTime)alocacao.ALAInicioExpediente).Hour < 12 ? TimeSelector.AmPmSpec.AM : TimeSelector.AmPmSpec.PM;
                TMS_final.Hour = alocacao.ALATerminoExpediente == null ? 8 : ((DateTime)alocacao.ALATerminoExpediente).Hour;
                TMS_final.Minute = alocacao.ALATerminoExpediente == null ? 0 : ((DateTime)alocacao.ALATerminoExpediente).Minute;
                TMS_final.AmPm = alocacao.ALAInicioExpediente == null || ((DateTime)alocacao.ALATerminoExpediente).Hour < 12 ? TimeSelector.AmPmSpec.AM : TimeSelector.AmPmSpec.PM;
                DDcurso.SelectedValue = dados.CurCodigo;
                BindTurmas(dados.CurCodigo, DD_Turma);
                DD_Turma.SelectedValue = dados.ALATurma.ToString();
                DD_area_atuacao.DataBind();
                DD_area_atuacao.SelectedValue = aprendiz.Apr_AreaAtuacao == null ? "" : aprendiz.Apr_AreaAtuacao.ToString();
                DD_parceiro.SelectedValue = dados.ParCodigo.ToString();
                BindUnidades(DD_parceiro.SelectedValue, DDunidade_parceiro);
                DDunidade_parceiro.SelectedValue = dados.ALAUnidadeParceiro.ToString();
                BindOrientador(int.Parse(DDunidade_parceiro.SelectedValue), DD_orientador);
                DD_orientador.SelectedValue = alocacao.ALAOrientador == null ? "" : alocacao.ALAOrientador.ToString();
                BindMotivos();
                DD_motivo_desligamento.SelectedValue = alocacao.ALAMotivoDesligamento == null ? "" : alocacao.ALAMotivoDesligamento.ToString();
                BindSituacoes();
                DD_situacao.SelectedValue = aprendiz.Apr_Situacao.ToString();
                DD_SituacaoAlocacao.SelectedValue = alocacao.ALAStatus;
                BindUsuarios();
                DDusuario.SelectedValue = alocacao.ALATutor;
                TBValorBolsa.Text = string.Format("{0:F2}", alocacao.ALAValorBolsa);
                TBValorTaxa.Text = string.Format("{0:F2}", alocacao.ALAValorTaxa);
                TBValorEncargos.Text = string.Format("{0:F2}", alocacao.ALAValorEncargos);
                TBdataInicio.Text = string.Format("{0:dd/MM/yyy}", alocacao.ALADataInicio);
                TBDataPrev.Text = string.Format("{0:dd/MM/yyy}", alocacao.ALADataPrevTermino);
                if (alocacao.ALADataTermino != null && !alocacao.ALADataTermino.Equals(string.Empty))
                    TBdataTermino.Text = string.Format("{0:dd/MM/yyy}", alocacao.ALADataTermino);
                TBObservacao.Text = alocacao.ALAObservacao;
                DD_tipo_pagamento.SelectedValue = alocacao.ALApagto;
                TB_inicio_aprendizagem.Text = string.Format("{0:dd/MM/yyy}", aprendiz.Apr_InicioAprendizagem);
                TB_data_desligamento.Text = string.Format("{0:dd/MM/yyy}", aprendiz.Apr_FimAprendizagem);
                TB_prev_Fim_aprendizagem.Text = string.Format("{0:dd/MM/yyy}", aprendiz.Apr_PrevFimAprendizagem);

                TBMudadoPor.Text = alocacao.ALAUsuarioMudaTurma == null ? "" : alocacao.ALAUsuarioMudaTurma.ToString();
                BindTurmas(dados.CurCodigo, TBTurmaAnterior);
                TBTurmaAnterior.SelectedValue = alocacao.ALATurmaAnterior == null ? (alocacao.ALATurma == null ? "" : alocacao.ALATurma.ToString()) : alocacao.ALATurmaAnterior.ToString();
                TBDataTurmaAnterior.Text = alocacao.ALADataMudaTurma == null ? "" : alocacao.ALADataMudaTurma.ToString();
                TBMotivoMudanca.Text = alocacao.ALAMotivoMudaTurma == null ? "" : alocacao.ALAMotivoMudaTurma.ToString();

                //Uniforme
                DDMesUniforme1.SelectedValue = alocacao.AlaMesPagto01Uniforme == null ? "" : alocacao.AlaMesPagto01Uniforme.ToString();
                txtMesAnoUniforme1.Text = alocacao.AlaAnoPagto01Uniforme == null ? "" : alocacao.AlaAnoPagto01Uniforme.ToString();
                DDMesUniforme2.SelectedValue = alocacao.AlaMesPagto02Uniforme == null ? "" : alocacao.AlaMesPagto02Uniforme.ToString();
                txtMesAnoUniforme2.Text = alocacao.AlaAnoPagto02Uniforme == null ? "" : alocacao.AlaAnoPagto02Uniforme.ToString();

                if (!dados.ALATurma.Equals(string.Empty))
                {
                    BindTurmas(dados.CurCodigo, DDNovaTurma);
                    TableMudaTurma.Visible = true;
                }
                else
                {
                    TableMudaTurma.Visible = false;
                }

                ShowAdress(alocacao.ALAUnidadeParceiro);

            }
        }

        protected void IMB_alterar_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            Session["comando"] = "Alterar";
            Session["PRMT_ordem"] = bt.CommandArgument;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 5;


        }

        private void LimpaCampos()
        {
            LB_matricula.Text = string.Empty;
            lb_aprendiz.Text = string.Empty;
            TMS_inicio.Hour = DateTime.Now.Hour;
            TMS_inicio.Minute = DateTime.Now.Minute;
            TMS_final.Hour = DateTime.Now.Hour;
            TMS_final.Minute = DateTime.Now.Minute;

            BindCursos();
            DD_Turma.Items.Clear();
            DDunidade_parceiro.Items.Clear();
            BindParceiros();
            DD_parceiro.SelectedValue = string.Empty;
            BindMotivos();

            lb_endereco.Text = string.Empty;

            BindSituacoes();
            BindUsuarios();
            TBValorBolsa.Text = "0,00";
            TBValorTaxa.Text = "0,00";
            TBValorEncargos.Text = "0,00";
            TBdataInicio.Text = string.Empty;
            TBDataPrev.Text = string.Empty;
            TBdataTermino.Text = string.Empty;
            TBObservacao.Text = string.Empty;
            DD_tipo_pagamento.SelectedValue = string.Empty;
            TB_inicio_aprendizagem.Text = string.Empty;
            TB_data_desligamento.Text = string.Empty;
        }

        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            BindGridView(int.Parse(lb_codigo_aprendiz.Text));
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

            try
            {
                int i = 0;
                if (DDtipo_relatorio.SelectedValue.Equals("")) throw new ArgumentException("Selecione um tipo de relatório.");
                var tipo = DDtipo_relatorio.SelectedValue;
                switch (tipo)
                {
                    case "1": Session["id"] = "53"; break;
                    case "2": Session["id"] = "21"; break;
                    case "3": Session["id"] = "18"; break;
                    case "4": Session["id"] = "23"; break;
                    case "5": Session["id"] = "13"; break;
                    case "6": Session["id"] = "14"; break;

                    case "7": Session["id"] = "42";

                        if (TBdataInicio.Text.Equals(string.Empty)) throw new ArgumentException("Informe a Data da Aula.");
                        Session["data_lista"] = TBdataInicio.Text;
                        break;

                    //case "8": ExibeContrato(1); break; // Integral
                    //case "9": ExibeContrato(2); break; // Parcial
                    case "8": Session["id"] = "86";
                        IFrame2.Visible = true;
                        IFrame2.DataBind();
                        break; // Integral

                    case "9": Session["id"] = "87";

                        IFrame2.Visible = true;
                        IFrame2.DataBind();
                        break; // Parcial

                    case "10":
                        Session["id"] = "54";
                        IFrame2.Visible = true;
                        IFrame2.DataBind();

                        break;
                    case "11": ExibeContrato(3); break;
                    case "12":

                        if (tb_numero.Text.Equals(string.Empty)) throw new ArgumentException("Informe o número de uniformes cedidos ao aprendiz.");
                        Session["prmt_num_uniforme"] = tb_numero.Text;
                        ExibeContrato(4); break;

                    case "13": ExibeContrato(5); break;
                    case "14": ExibeContrato(6); break;


                    case "15":

                        var con = new Conexao();
                        var sql = "select Apr_Situacao from CA_Aprendiz where Apr_Situacao = 5 and Apr_Codigo = " + Session["CodAprendiz"] + "";
                        var result = con.Consultar(sql);
                        i++;
                        Session["id"] = "84";
                        break;

                    case "16":
                        con = new Conexao();
                        sql = "select Apr_AreaAtuacao from CA_Aprendiz where Apr_Codigo = " + Session["CodAprendiz"] + " and Apr_Situacao = 5";
                        result = con.Consultar(sql);
                        i = 0;
                        while (result.Read())
                        {
                            i++;
                            Session["id"] = "85";
                            BuscaFrequenciaConteudoTeorico(Convert.ToInt32(Session["CodAprendiz"].ToString()));

                            if (result[0].ToString().Equals("1"))
                            {
                                Session["NomeRelatorio"] = "RPVersoCertificadoSit1";
                            }
                            else
                            {
                                Session["NomeRelatorio"] = "RPVersoCertificadoSit4";
                            }


                        }
                        if (i == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('ERRO - Este aprendiz ainda não está apto a emitir o verso do certificado')", true);
                        }

                        break;

                }

                if ((tipo != "8") && (tipo != "9") && (tipo != "11") && (tipo != "12") && (tipo != "13") && (tipo != "14") && (tipo != "10") && (tipo != "15") && (tipo != "16") && (tipo != "10"))
                {
                    IFrame2.Visible = true;
                    IFrame2.DataBind();
                }

                if (((tipo == "15") || (tipo == "16")) && i > 0)
                {
                    IFrame2.Visible = true;
                    IFrame2.DataBind();
                }

            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000122", ex);
            }

        }


        private void BuscaFrequenciaConteudoTeorico(int codigo)
        {
            try
            {



                var sql = @"SELECT 
                            Sum(Case when [AdidataAula]<GETDATE() And [Adipresenca]='.'then 1 else 0 end) As Presenca,

                            Sum(Case when [AdidataAula]<GETDATE() And [Adipresenca]='.'then 1 else 0 end) + Sum(Case when ([AdidataAula]<getDate() And [Adipresenca]='F')then 1 else 0 end ) + Sum(Case when ([AdidataAula]<getDate() And [Adipresenca]='J')then 1 else 0 end )   AS Soma


                            FROM CA_AulasDisciplinasAprendiz INNER JOIN CA_Aprendiz ON CA_AulasDisciplinasAprendiz.AdiCodAprendiz = CA_Aprendiz.Apr_Codigo
                            WHERE (((CA_AulasDisciplinasAprendiz.AdiDataAula)<[Apr_PrevFimAprendizagem]))
                            GROUP BY CA_AulasDisciplinasAprendiz.AdiCodAprendiz
                            HAVING (((CA_AulasDisciplinasAprendiz.AdiCodAprendiz) = " + codigo + "));";

                var con = new Conexao();
                SqlDataReader consulta = con.Consultar(sql);

                var list = new List<String>();
                while (consulta.Read())
                {

                    String presenca = consulta.GetSqlValue(0).ToString();
                    String soma = consulta.GetSqlValue(1).ToString();

                    Session["FrequenciaCoteudoTeorico"] = Convert.ToInt32(Convert.ToDouble(presenca) / Convert.ToDouble(soma) * 100);
                }

            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000156", ex);
            }
        }




        protected void ExibeContrato(int tipo)
        {
            var popup = "popup('Contrato.aspx?" +
                      "id=" + Criptografia.Encrypt(tipo.ToString(), GetConfig.Key()) +
                      "&meta=" + Criptografia.Encrypt(Session["PRMT_Aprendiz"].ToString(), GetConfig.Key()) + "', '700','800');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), popup, true);
        }

        protected void DDtipo_relatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_numero.Visible = DDtipo_relatorio.SelectedValue.Equals("12");
            tb_numero.Visible = DDtipo_relatorio.SelectedValue.Equals("12");
            tb_numero.Text = string.Empty;

            IFrame2.Visible = false;
            IFrame2.DataBind();
        }

        //=====================================================================================INSERE CRONOGRAMA
        //Andreghorst 19032012
        //criado nova rotina para gerar e listar Cronograma 
        #region Cronograma

        /// <summary>
        /// funcao que pega TurCodigo, DataInicio e DataFinal para poder 
        /// incluir cronograma aprendiz no banco de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Session["TurCodigo"] = GridView2.DataKeys[index]["TurCodigo"].ToString();
            Session["DataInicio"] = GridView2.Rows[index].Cells[1].Text.ToString();  //pega a data inicial no grid
            Session["DataFinal"] = GridView2.Rows[index].Cells[2].Text.ToString();  //pega a data final no grid
            MultiView1.ActiveViewIndex = 6; //view 8                     

        }

      
        protected void PreencherDropDownPlanoCurricular()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var datasource = from A in db.CA_Aprendizs
                                 join P in db.CA_PlanoCurriculars on A.Apr_PlanoCurricular equals P.PlcCodigoPlano
                                 join D in db.CA_Disciplinas on P.PlcDisciplina equals D.DisCodigo
                                 where A.Apr_Codigo.Equals(Session["Apr_Codigo"])
                                 select new { D.DisCodigo, D.DisDescricao };
                DD_Plano_Curricular.DataSource = datasource;
                DD_Plano_Curricular.DataBind();
                DD_Plano_Curricular.Items.Insert(0, "Selecione..");
                DD_Plano_Curricular.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// retorna se o dia é feriado de acordo com os dados inseridos na tabela CA_Feriados
        /// </summary>
        /// <param name="feriado"></param>
        /// <returns></returns>
        protected bool VerificaFeriado(DateTime feriado)
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var datasource = from F in db.CA_Feriados
                                 where F.FerData.Date.Equals(feriado)
                                 select new { F.FerData };
                if (datasource.Any())
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Funcao para pegar o codigo do candidato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //thassio
            int index = Convert.ToInt32(e.CommandArgument);
            Session["CodAprendiz"] = index.ToString();
            Session["AreaAtuacao"] = GridView1.DataKeys[0]["Apr_AreaAtuacao"].ToString();
            Session["PlanoCurricular"] = GridView1.DataKeys[0]["Apr_PlanoCurricular"].ToString();
            if (index < 0)
            {
                using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
                {
                    var aprendiz = repository.Find(index);

                    lb_codigo_aprendiz.Text = aprendiz.Apr_Codigo.ToString();
                    lb_nome_aprendiz.Text = aprendiz.Apr_Nome;
                }

            }
            Pn_aprendiz.Visible = true;
        }


        /// <summary>
        /// Retorna dados a serem inseridos no banco e também manipulação da ordem de inserção
        /// </summary>
        protected DataTable TabelaInsercaoIntrodutorio()
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("TurCodigo", typeof(int));
            dt.Columns.Add("DisCodigo", typeof(int));
            dt.Columns.Add("PlcCargaHoraria", typeof(int));
            dt.Columns.Add("PlcOrdemDisciplina", typeof(int));
            dt.Columns.Add("PlcNumeroAulas", typeof(int));
            dt.Columns.Add("EnsNumeroPeriodos", typeof(string));
            dt.Columns.Add("TurDiaSemana", typeof(string));
            dt.Columns.Add("EducCodigo", typeof(int));

            var db = new DC_ProtocoloAgilDataContext(GetConfig.Config());

            string turcodigo = Session["TurCodigo"].ToString();
            string codAprendiz = Session["CodAprendiz"].ToString();

            var sql = (from A in db.CA_Aprendizs
                       join AL in db.CA_AlocacaoAprendizs on A.Apr_Codigo equals AL.ALAAprendiz
                       join T in db.CA_Turmas on AL.ALATurma equals T.TurCodigo
                       join P in db.CA_PlanoCurriculars on T.TurPlanoCurricular equals P.PlcCodigoPlano
                       where
                       T.TurCurso.Equals("001")
                       && T.TurCodigo.Equals(Session["TurCodigo"])
                       && A.Apr_Codigo.Equals(Session["CodAprendiz"]
                       )
                       orderby P.PlcOrdemDisciplina ascending
                       select new { T.TurCodigo, P.PlcDisciplina, P.PlcCargaHoraria, P.PlcOrdemDisciplina, P.PlcNumeroAulas, T.TurDiaSemana, P.EducCodigo }).ToList();

            foreach (var row in sql)
            {
                DataRow dr = dt.NewRow();
                dr[0] = row.TurCodigo;
                dr[1] = row.PlcDisciplina;
                dr[2] = row.PlcCargaHoraria;
                dr[3] = row.PlcOrdemDisciplina;
                dr[4] = row.PlcNumeroAulas;
                dr[5] = 1;
                dr[6] = row.TurDiaSemana;
                dr[7] = row.EducCodigo; ;
                dt.Rows.Add(dr);
            }
            return dt;
        }


        /// <summary>
        /// Retorna dados a serem inseridos no banco e também manipulação da ordem de inserção
        /// </summary>
        protected DataTable TabelaInsercao()
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("TurCodigo", typeof(int));
            dt.Columns.Add("DisCodigo", typeof(int));
            dt.Columns.Add("PlcCargaHoraria", typeof(int));
            dt.Columns.Add("PlcOrdemDisciplina", typeof(int));
            dt.Columns.Add("PlcNumeroAulas", typeof(int));
            dt.Columns.Add("EnsNumeroPeriodos", typeof(string));
            dt.Columns.Add("TurDiaSemana", typeof(string));
            dt.Columns.Add("EducCodigo", typeof(int));

            var db = new DC_ProtocoloAgilDataContext(GetConfig.Config());

            string turcodigo = Session["TurCodigo"].ToString();
            string codAprendiz = Session["CodAprendiz"].ToString();

            var sql = (from A in db.CA_Aprendizs
                       join P in db.CA_PlanoCurriculars on A.Apr_PlanoCurricular equals P.PlcCodigoPlano
                       join AL in db.CA_AlocacaoAprendizs on A.Apr_Codigo equals AL.ALAAprendiz
                       join T in db.CA_Turmas on AL.ALATurma equals T.TurCodigo
                       where
                       T.TurCurso.Equals("002")
                       && T.TurCodigo.Equals(Session["TurCodigo"])
                       && A.Apr_Codigo.Equals(Session["CodAprendiz"]
                       )
                       orderby P.PlcOrdemDisciplina ascending
                       select new { T.TurCodigo, P.PlcDisciplina, P.PlcCargaHoraria, P.PlcOrdemDisciplina, P.PlcNumeroAulas, T.TurDiaSemana, P.EducCodigo }).ToList();

            foreach (var row in sql)
            {
                DataRow dr = dt.NewRow();
                dr[0] = row.TurCodigo;
                dr[1] = row.PlcDisciplina;
                dr[2] = row.PlcCargaHoraria;
                dr[3] = row.PlcOrdemDisciplina;
                dr[4] = row.PlcNumeroAulas;
                dr[5] = 1;
                dr[6] = row.TurDiaSemana;
                dr[7] = row.EducCodigo; ;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Insere cronograma introdutorio, este cronograma é inserido diariamente.
        /// Existe a possibilidade de ser inserido de 7 em 7 dias caso dt.Rows[0][5].ToString().Equals("0"))
        /// seja igual a 1
        /// </summary>
        protected void InsereCronogramaIntroducao()
        {
            // testes
            string curCodigo = "";
            int b = 0;
            bool aprovado = false;

            for (b = 0; b < GridView2.Rows.Count; b++)
            {
                curCodigo = GridView2.DataKeys[b]["CurCodigo"].ToString();
                if (curCodigo.Equals("001"))
                {
                    aprovado = true;
                    break;
                }
            }

            if (!aprovado)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ERRO - Não é possível gerar Introdutório')", true);
                return;
            }


            //thassio
            Session["TurCodigo"] = GridView2.DataKeys[b]["TurCodigo"].ToString();
            DateTime data = Convert.ToDateTime(GridView2.Rows[b].Cells[1].Text.ToString());
            DateTime dataFinal = Convert.ToDateTime(GridView2.Rows[b].Cells[2].Text.ToString());
            DataTable dt = TabelaInsercaoIntrodutorio();
            for (int j = 0; j < dt.Rows.Count && data <= dataFinal; j++)
            {
                for (int i = 0; data <= dataFinal; i++) //and data menor que data final...
                {
                    if (data.DayOfWeek.Equals(DayOfWeek.Saturday))   //sabado
                    {
                        data = data.AddDays(1);
                    }
                    if (data.DayOfWeek.Equals(DayOfWeek.Sunday)) //domingo
                    {
                        data = data.AddDays(1);
                    }
                    if (VerificaFeriado(data.Date).Equals(true))
                    {
                        data = data.AddDays(1);
                        i = i - 1;
                    }
                    else
                    {
                        using (var repository = new Repository<AulasDisciplinasAprendiz>(new Context<AulasDisciplinasAprendiz>()))
                        {

                            var ap = new AulasDisciplinasAprendiz();

                            ap.AdiCargaHoraria = Convert.ToInt32(dt.Rows[j][2].ToString()); //carga horaria
                            ap.AdiCodAprendiz = Convert.ToInt32(Session["CodAprendiz"].ToString()); //codigo aprendiz                       
                            //ap.AdiDisciplina = Convert.ToInt32(dt.Rows[j][1].ToString()); // codigo Disciplina
                            ap.AdiDisciplina = 27; // codigo Disciplina
                            ap.AdiEducador = Convert.ToInt32(dt.Rows[j][7].ToString()); // codigo educador
                            ap.AdiTurma = Convert.ToInt32(dt.Rows[j][0].ToString()); ; //codigo turma
                            ap.AdiPresenca = ".";
                            ap.AdiDataAula = data;
                            repository.Add(ap);
                        }
                        data = data.AddDays(1);
                    }

                }
            }
        }

        /// <summary>
        /// Calcula o dia inicial que o aprendiz irá começar sua aula.
        /// </summary>
        /// <returns></returns>
        protected DateTime retornaDataInicio()
        {
            string a12 = GridView2.DataKeys[0]["TurCodigo"].ToString(); //simulada DataKeys[1]
            DateTime data = Convert.ToDateTime(GridView2.Rows[0].Cells[1].Text.ToString());
            DataTable dt = new DataTable();

            dt.Clear();
            dt = TabelaInsercao();
            DayOfWeek dia;
            Enum.TryParse((dt.Rows[0][6].ToString()), out dia);

            if (dt.Rows[0][6].ToString().Equals("2")) //segunda
            {
                dia = DayOfWeek.Monday;
            }
            if (dt.Rows[0][6].ToString().Equals("3")) //terça
            {
                dia = DayOfWeek.Tuesday;
            }
            if (dt.Rows[0][6].ToString().Equals("4")) //quarta
            {
                dia = DayOfWeek.Wednesday;
            }
            if (dt.Rows[0][6].ToString().Equals("5")) //quinta
            {
                dia = DayOfWeek.Thursday;
            }
            if (dt.Rows[0][6].ToString().Equals("6")) //sexta
            {
                dia = DayOfWeek.Friday;
            }
            if (dt.Rows[0][6].ToString().Equals("7")) //sexta
            {
                dia = DayOfWeek.Saturday;
            }
            for (int i = 0; !data.DayOfWeek.Equals(dia); i++)
                data = data.AddDays(1);
            return data;
        }


        /// <summary>
        /// funcao abaixo insere as diciplinas a partir do dropdown selecionado
        /// por exemplo: selecionado Disciplina: Sistemas e value 3
        /// ele ira inserir a disciplina value 3 adiante...
        /// </summary>
        protected void InsereCronogramaOutrasdisciplinas()
        {
            // thassio
            bool verificaUltima = false;

            Session["TurCodigo"] = GridView2.DataKeys[0]["TurCodigo"].ToString(); //simulada DataKeys[1]            
            string a = GridView2.DataKeys[0]["TurCodigo"].ToString(); //simulada DataKeys[1]
            DateTime data = retornaDataInicio();
            DateTime dataFinal = Convert.ToDateTime(GridView2.Rows[0].Cells[2].Text.ToString());
            DataTable dt = TabelaInsercao();
            int DiscilpinaInicial = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == DD_Plano_Curricular.SelectedValue)
                {
                    DiscilpinaInicial = i;
                    break;
                }
            }
            int contador = DiscilpinaInicial;
            for (; contador < dt.Rows.Count && data < dataFinal; contador++)
            {
                int numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());
                for (int i = 0; i < numAulas && data < dataFinal; i++) //and data menor que data final...
                {
                    verificaUltima = false;

                    if (data.DayOfWeek.Equals(DayOfWeek.Saturday))   //sabado
                    {
                        data = data.AddDays(1);
                    }
                    if (data.DayOfWeek.Equals(DayOfWeek.Sunday)) //domingo
                    {
                        data = data.AddDays(1);
                    }
                    if (VerificaFeriado(data).Equals(true))
                    {

                        data = data.AddDays(7);
                        verificaUltima = true;
                        i = i - 1;
                    }
                    else
                    {
                        using (var repository = new Repository<AulasDisciplinasAprendiz>(new Context<AulasDisciplinasAprendiz>()))
                        {

                            var ap = new AulasDisciplinasAprendiz();
                            ap.AdiCargaHoraria = Convert.ToInt32(dt.Rows[contador][2].ToString()); //carga horaria
                            ap.AdiCodAprendiz = Convert.ToInt32(Session["CodAprendiz"].ToString()); //codigo aprendiz
                            ap.AdiDisciplina = Convert.ToInt32(dt.Rows[contador][1].ToString()); // codigo Disciplina
                            //ap.AdiEducador = Convert.ToInt32(DD_EDUCADORES.SelectedValue); // codigo turma
                            ap.AdiEducador = Convert.ToInt32(dt.Rows[contador][7].ToString()); // codigo educador
                            ap.AdiTurma = Convert.ToInt32(dt.Rows[contador][0].ToString()); ; //codigo turma
                            ap.AdiPresenca = ".";
                            ap.AdiDataAula = data;
                            repository.Add(ap);
                        }

                        if (verificaUltima == false)
                        {
                            data = data.AddDays(7);

                        }
                    }
                    //verificaUltima = true;
                }
            }
            InsereCronogramaOutras(dt, data, DiscilpinaInicial, dataFinal);
        }

        /// <summary>
        /// função que insere as disciplinas abaixo da que foi selecionada, leia a funcao acima
        /// como na funcao anterior foi selecionada a disciplina 3 e inserido 3,4,5,6.... até N
        /// esta funcao irá inserir da primeira até a 3 ou seja, insere 1 e 2
        /// /// 
        /// </summary>
        /// <param name="dt">datatable com as informacoes necessarias para insercao</param>
        /// <param name="data">data a ser inserida para cursar a disciplina</param>
        /// <param name="DiscilpinaInicial"> a disciplina maxima que nao deve ser inserica (3)</param>
        /// <param name="dataFinal">a data final de insercao deve ser menor que esta</param>
        /// 
        /// melhoria no código, a funcao acima pode ser recursiva, evitando o trecho abaixo que é ela mesma, chamando apenas parametos diferentes
        /// 
        protected void InsereCronogramaOutras(DataTable dt, DateTime data, int DiscilpinaInicial, DateTime dataFinal)
        {
            if (VerificaFeriado(data).Equals(true))
            {
                data = data.AddDays(7);
            }

            // DiscilpinaInicial--; //nao faz a ultima;
            for (int contador = 0; contador < DiscilpinaInicial; contador++)
            {
                int numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());
                for (int i = 0; i < numAulas && data < dataFinal; i++) //and data menor que data final...
                {
                    if (data.DayOfWeek.Equals(DayOfWeek.Saturday))   //sabado
                    {
                        data = data.AddDays(1);
                    }
                    if (data.DayOfWeek.Equals(DayOfWeek.Sunday)) //domingo
                    {
                        data = data.AddDays(1);
                    }
                    if (VerificaFeriado(data.Date).Equals(true))
                    {
                        data = data.AddDays(7);
                        i = i - 1;
                    }
                    else
                    {
                        using (var repository = new Repository<AulasDisciplinasAprendiz>(new Context<AulasDisciplinasAprendiz>()))
                        {

                            var ap = new AulasDisciplinasAprendiz();
                            ap.AdiCargaHoraria = Convert.ToInt32(dt.Rows[contador][2].ToString()); //carga horaria
                            ap.AdiCodAprendiz = Convert.ToInt32(Session["CodAprendiz"].ToString()); //codigo aprendiz
                            ap.AdiDisciplina = Convert.ToInt32(dt.Rows[contador][1].ToString()); // codigo Disciplina
                            //ap.AdiEducador = Convert.ToInt32(DD_EDUCADORES.SelectedValue); // codigo turma
                            ap.AdiEducador = Convert.ToInt32(dt.Rows[contador][7].ToString()); // codigo educador
                            ap.AdiTurma = Convert.ToInt32(dt.Rows[contador][0].ToString()); ; //codigo turma
                            ap.AdiPresenca = ".";
                            ap.AdiDataAula = data;
                            repository.Add(ap);
                        }
                        data = data.AddDays(7);

                    }

                }
            }
        }

        /// <summary>
        /// Caso os dados já tenham sido inseridos, a funcao abaixo retorna true e assim 
        /// o usuário nnão consegue inseri-los novamete.
        /// </summary>
        /// <returns></returns>
        protected bool ValidaInsercao()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                string b = Session["CodAprendiz"].ToString();
                var datasource = from F in db.CA_AulasDisciplinasAprendizs
                                 where F.AdiCodAprendiz.Equals(Session["CodAprendiz"])
                                 select new { F.AdiEducador };

                if (datasource.ToList().Any())
                    return true;
                else
                    return false;
            }
        }

        protected void btCadastrarCronograma_Click(object sender, EventArgs e)
        {

            string curCodigo = "";
            int i = 0;
            bool aprovado = false;

            for (i = 0; i < GridView2.Rows.Count; i++)
            {
                curCodigo = GridView2.DataKeys[i]["CurCodigo"].ToString();
                if (curCodigo.Equals("001"))
                {
                    aprovado = true;
                    break;
                }
            }

            if (!aprovado)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Não é possível gerar Introdutório')", true);
                return;
            }


            if (GridView2.Rows[i].Cells[2].Text.ToString().Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Data previsão de término não preenchido. Cronograma não gerado')", true);
                return;
            }

            //thassio
            if (ValidaDados())
            {
                InsereCronogramaIntroducao();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cronograma gerado com sucesso.')", true);

            }
        }

        protected void btnGerarOutras_Click(object sender, EventArgs e)
        {
            if (ValidaDados())
            {
                InsereCronogramaOutrasdisciplinas();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cronograma gerado com sucesso.')", true);
            }
        }



        protected bool ValidaDados()
        {
            return true;
        }

        #endregion

        //===================================================================================== LISTAR CRONOGRAMA
        #region LISTAR CRONOGRAMA

        protected void btListarCronograma_Click(object sender, EventArgs e)
        {
            try //campos do grid nao preenchidos dão pau... LB_LABEL
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                Session["TurCodigo"] = GridView2.DataKeys[index]["TurCodigo"].ToString();
                string turma = GridView2.DataKeys[index]["TurCodigo"].ToString();
                MultiView1.ActiveViewIndex = 7; //view 9

                if (GridView2.DataKeys[index]["CurCodigo"].ToString().Equals("001"))
                {
                    ListarCronogramaIntrodutorio();
                }
                else
                {
                    ListarCronograma();
                }

                //declarado campos no grid como datakeys para posterior uso dos mesmos.
                if (gvListarCronograma.Rows.Count > 0)
                {
                    LB_APR_CRONO.Text = gvListarCronograma.DataKeys[0]["Nome"].ToString();
                    LB_APR_Hinicio.Text = Convert.ToDateTime(gvListarCronograma.DataKeys[0]["Inicio"].ToString()).ToShortTimeString();
                    LB_APR_HTermino.Text = Convert.ToDateTime(gvListarCronograma.DataKeys[0]["Termino"].ToString()).ToShortTimeString();
                    LB_Turma.Text = gvListarCronograma.DataKeys[0]["Turma"].ToString();
                    LB_Educador.Text = gvListarCronograma.DataKeys[0]["Educador"].ToString();
                }
            }
            catch
            {
                throw;
            }
        }

        void ListarCronogramaIntrodutorio()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = from Au in db.CA_AulasDisciplinasAprendizs
                            join A in db.CA_Aprendizs on Au.AdiCodAprendiz equals A.Apr_Codigo
                            join T in db.CA_Turmas on Au.AdiTurma equals T.TurCodigo
                            join D in db.CA_Disciplinas on Au.AdiDisciplina equals D.DisCodigo
                            join E in db.CA_Educadores on Au.AdiEducador equals E.EducCodigo
                            join DF in db.CA_AulasDisciplinasTurmaProfs on T.TurCodigo equals DF.ADPTurma
                            where Au.AdiCodAprendiz.Equals(Session["CodAprendiz"])
                            && Au.AdiTurma.Equals(Session["TurCodigo"]) && D.DisCodigo.Equals(DF.ADPDisciplina) && E.EducCodigo.Equals(DF.ADPprofessor)
                            && Au.AdiDisciplina.Equals(DF.ADPDisciplina) && Au.AdiTurma.Equals(DF.ADPTurma) && Au.AdiEducador.Equals(DF.ADPprofessor)
                            // && Au.AdiDataAula.Equals(DF.ADPDataAula)
                            orderby Au.AdiDataAula, DF.ADPOrdemAula
                            select new { Nome = A.Apr_Nome, Inicio = T.TurInicio, Termino = T.Termino, Disciplina = D.DisDescricao, Educador = E.EducNome, Data = Au.AdiDataAula.ToShortDateString(), Turma = T.TurNome, OrdemAula = DF.ADPOrdemAula };
                gvListarCronograma.DataSource = query;
                gvListarCronograma.DataBind();
            }
        }

        void ListarCronograma()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = from Au in db.CA_AulasDisciplinasAprendizs
                            join A in db.CA_Aprendizs on Au.AdiCodAprendiz equals A.Apr_Codigo
                            join T in db.CA_Turmas on Au.AdiTurma equals T.TurCodigo
                            join D in db.CA_Disciplinas on Au.AdiDisciplina equals D.DisCodigo
                            join E in db.CA_Educadores on Au.AdiEducador equals E.EducCodigo
                            //join DF in db.CA_AulasDisciplinasTurmaProfs on T.TurCodigo equals DF.ADPTurma
                            where Au.AdiCodAprendiz.Equals(Session["CodAprendiz"])
                            //&& Au.AdiTurma.Equals(Session["TurCodigo"]) && D.DisCodigo.Equals(DF.ADPDisciplina) && E.EducCodigo.Equals(DF.ADPprofessor)
                            //&& Au.AdiDisciplina.Equals(DF.ADPDisciplina) && Au.AdiTurma.Equals(DF.ADPTurma) && Au.AdiEducador.Equals(DF.ADPprofessor)
                            // && Au.AdiDataAula.Equals(DF.ADPDataAula)
                            orderby Au.AdiDataAula
                            select new { Nome = A.Apr_Nome, Inicio = T.TurInicio, Termino = T.Termino, Disciplina = D.DisDescricao, Educador = E.EducNome, Data = Au.AdiDataAula.ToShortDateString(), Turma = T.TurNome, OrdemAula = 1 };
                gvListarCronograma.DataSource = query;
                gvListarCronograma.DataBind();
            }
        }



        protected void gvListarCronograma_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListarCronograma.PageIndex = e.NewPageIndex;
            ListarCronograma();
        }

        protected void btVoltarListaCrono_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
        }
        #endregion

        protected void TB_inicio_aprendizagem_TextChanged(object sender, EventArgs e)
        {
            TB_prev_Fim_aprendizagem.Text = Convert.ToDateTime(TB_inicio_aprendizagem.Text).AddMonths(11).ToShortDateString();
        }

        protected void TBdataInicio_TextChanged(object sender, EventArgs e)
        {
            //if (DDcurso.SelectedValue == "001")
            //{
            //    DateTime today = Convert.ToDateTime(TBdataInicio.Text).AddDays(19);


            //   // DateTime endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            //    TBDataPrev.Text = today.ToShortDateString();
            //    //TBDataPrev.Text = Convert.ToDateTime(TBdataInicio.Text).AddMonths(2).AddDays(-1).ToShortDateString();
            //}
            //else
            //{
            //    TBDataPrev.Text = Convert.ToDateTime(TBdataInicio.Text).AddMonths(11).ToShortDateString();
            //}

            //var sql = "Select TurNumeroMeses from CA_turmas where TurCodigo = " + DD_Turma.SelectedValue + "";
            //var con = new Conexao();
            //var result = con.Consultar(sql);
            //int numeroDias = 0;

            //while (result.Read())
            //{
            //    numeroDias = int.Parse(result["TurNumeroMeses"].ToString()) * 7;
            //}

            //DateTime today = Convert.ToDateTime(TBdataInicio.Text).AddDays(numeroDias);
            //TBDataPrev.Text = today.ToShortDateString();
        }

        protected void btRegerarCronograma_Click(object sender, EventArgs e)
        {

            var Aprendiz = Convert.ToInt32(Session["prmt_aprendiz_selecionado"].ToString());

            if (Convert.ToBoolean(HFConfirmaRegerar.Value))
            {
                using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var deleteCronograma = from details in db.CA_AulasDisciplinasAprendizs
                                           where details.AdiCodAprendiz == Aprendiz
                                           select details;

                    foreach (var detail in deleteCronograma)
                    {
                        var sql = "delete CA_AulasDisciplinasAprendiz where AdiCodAprendiz = " + Aprendiz + "";
                        var con = new Conexao();
                        con.Alterar(sql);
                        //  db.CA_AulasDisciplinasAprendizs.DeleteOnSubmit(detail);
                    }

                    try
                    {
                        //if (DD_Plano_Curricular.SelectedValue.Equals("Selecione.."))
                        //{
                        //    throw new ArgumentException("Selecione o Plano curricular.");
                        //}
                        //else
                        //{
                        //    db.SubmitChanges();
                        //    InsereCronogramaIntroducao();
                        //    InsereCronogramaOutrasdisciplinas();
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Novo cronograma gerado com sucesso.')", true);
                        //}
                    }
                    catch (ArgumentException ex)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                 "alert('" + ex.Message + "')", true);
                    }
                    catch (Exception ex)
                    {
                        Funcoes.TrataExcessao("000122", ex);
                    }
                }
            }
        }




        protected void btnPrimeiroEncontro_Click(object sender, EventArgs e)
        {
            if (ValidaCodigo003(1, 3) == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('ERRO - Este aprendiz não possui alocação para encontros.')", true);
            }
        }


        private bool ValidaCodigo003(int tipoEncontro, int disciplina)
        {

            var aprendiz = Session["prmt_aprendiz_selecionado"];
            var sql = "select * from View_AlocacoesAlunos where TurCurso = '003' and AlaAprendiz = " + aprendiz + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                InsereCronogramaPrimeiroEncontroESegundoEncontro(result, tipoEncontro, disciplina);
                return true;

            }
            return false;
        }

        protected void btnSegundoEncontro_Click(object sender, EventArgs e)
        {
            if (ValidaCodigo003(2, 4) == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('ERRO - Este aprendiz não possui alocação para encontros.')", true);
            }
        }

        protected void IndiceZeroSituacao(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            DDSituacao.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

            MultiView1.ActiveViewIndex = 8;
            Session["id"] = 89;


        }

        protected void btnGerarCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                string curCodigo = "";
                int b = 0;
                bool aprovado = false;

                for (b = 0; b < GridView2.Rows.Count; b++)
                {
                    curCodigo = GridView2.DataKeys[b]["CurCodigo"].ToString();
                    if (curCodigo.Equals("002"))
                    {
                        aprovado = true;
                        break;
                    }
                }

                if (!aprovado)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ERRO - Não é possível gerar Simultaneidade')", true);
                    return;
                }

                DateTime dataFinal = Convert.ToDateTime(GridView2.Rows[b].Cells[2].Text.ToString());
                DateTime dataInicial = Convert.ToDateTime(GridView2.Rows[b].Cells[1].Text.ToString());

                var turma = GridView2.DataKeys[b]["TurCodigo"].ToString();
                var numaula = GridView2.DataKeys[b]["TurNumeroMeses"].ToString();


                var sql = "Select * from CA_AulasDisciplinasTurmaProf where ADPTurma = " + turma + " and ADPDataAula >= '" + dataInicial + "' and ADPDataAula <= '" + dataFinal + "' order by ADPDataAula";
                var conexao = new Conexao();
                var aulas = 0;
                var result = conexao.Consultar(sql);
                while (result.Read() && (aulas < Convert.ToInt32(numaula)))
                {
                    var conexaoInsere = new Conexao();
                    string insere = "insert into CA_AulasDisciplinasAprendiz values (" + Session["CodAprendiz"] + ", " + turma + ", " + result["ADPDisciplina"] + ", " + result["ADPProfessor"] + ", '" + Convert.ToDateTime(result["ADPDataAula"]) + "', 2, '.', null, '.','','', '', 0)";
                    conexaoInsere.Alterar(insere);
                    aulas++;
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Cronograma gerado com sucesso')", true);
            }
            catch (Exception a)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Este cronograma já foi gerado')", true);
            }

        }

        protected void btnCapacitacao_Click(object sender, ImageClickEventArgs e)
        {

            var bt = (ImageButton)sender;
            int aprendiz = int.Parse(bt.CommandArgument);

            using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
            {
                var a = repository.Find(aprendiz);

                lb_codigo_aprendiz.Text = a.Apr_Codigo.ToString();
                lb_nome_aprendiz.Text = a.Apr_Nome;
            }

            Session["codAprendiz"] = aprendiz;

            MultiView1.ActiveViewIndex = 10;
            CarregaGridCapacitacao(aprendiz);


        }

        private void CarregaGridCapacitacao(int aprendiz)
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from C in db.CA_CapacitacaoAprendizs
                             join U in db.CA_Unidades on C.CapUnidade equals U.UniCodigo
                             join T in db.CA_Turmas on C.CapTurma equals T.TurCodigo
                             where C.CapAprendiz == aprendiz
                             select new { C.CapAprendiz, C.CapDataInicio, C.CapDataPrevTermino, C.CapDataTermino, C.CapObservacoes, C.CapSequencia, C.CapStatus, C.CapTurma, C.CapUnidade, U.UniNome, T.TurNome }).ToList().OrderBy(item => item.CapAprendiz);

                gridCapacitacoes.DataSource = query.ToList();
                gridCapacitacoes.DataBind();


            }
        }

        private void BuscaTurmaCapicitacao()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from T in db.CA_Turmas
                             where T.TurCurso.Equals("003")
                             select new { T.TurCodigo, T.TurNome }).ToList().OrderBy(item => item.TurNome);

                DDTurmaCapacitacao.DataSource = query;
                DDTurmaCapacitacao.DataBind();
            }
        }
        private void BuscaUnidadeCapicitacao()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from U in db.CA_Unidades
                             //where T.TurCurso.Equals("003")
                             select new { U.UniCodigo, U.UniNome }).ToList().OrderBy(item => item.UniNome);

                DDUnidadeCapacitacao.DataSource = query;
                DDUnidadeCapacitacao.DataBind();
            }
        }

        protected void btnSalvarCapacitacao_Click(object sender, EventArgs e)
        {
            SalvarCapacitacao();
        }

        private void SalvarCapacitacao()
        {

            try
            {
                if (DDTurmaCapacitacao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma Turma");
                if (DDUnidadeCapacitacao.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Selecione uma Unidade");
                if (txtDataInicioCapacitacao.Text.Equals(string.Empty)) throw new ArgumentException("Data Início obrigatório");
                if (txtDataPrevisaoCapacitacao.Text.Equals(string.Empty)) throw new ArgumentException("Data de Previsão obrigatório");

                var dataInicio = new object();
                var dataPrevisaoTermino = new object(); ;
                var dataTermino = new object(); ;

                if (txtDataInicioCapacitacao.Text.Equals(string.Empty))
                {
                    dataInicio = "";
                }
                else
                {
                    dataInicio = DateTime.Parse(txtDataInicioCapacitacao.Text);
                }

                if (txtDataPrevisaoCapacitacao.Text.Equals(string.Empty))
                {
                    dataPrevisaoTermino = "";
                }
                else
                {
                    dataPrevisaoTermino = DateTime.Parse(txtDataPrevisaoCapacitacao.Text);
                }

                if (txtTerminoCapacitacao.Text.Equals(string.Empty))
                {
                    dataTermino = "";
                }
                else
                {
                    dataTermino = DateTime.Parse(txtTerminoCapacitacao.Text);
                }

                if (btnSalvarCapacitacao.Text.Equals("Salvar"))
                {
                    var sql = "insert into CA_CapacitacaoAprendiz values (" + Session["codAprendiz"] + ", " + DDTurmaCapacitacao.SelectedValue + ", '" + DDStatusCapacitacao.SelectedValue + "', " + DDUnidadeCapacitacao.SelectedValue + ", '" + dataInicio + "', '" + dataPrevisaoTermino + "', '" + dataTermino + "', '" + txtObservacoesCapacitacao.Text + "' )";
                    var con = new Conexao();
                    con.Alterar(sql);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                 "alert('Ação realizada com sucesso.')", true);
                }
                else
                {
                    var sql = "update CA_CapacitacaoAprendiz set CapTurma = " + DDTurmaCapacitacao.SelectedValue + ", CapStatus = '" + DDStatusCapacitacao.SelectedValue + "', CapUnidade = " + DDUnidadeCapacitacao.SelectedValue + ", CapDataInicio = '" + dataInicio + "', CapDataPrevTermino = '" + dataPrevisaoTermino + "', CapDataTermino = '" + dataTermino + "', CapObservacoes = '" + txtObservacoesCapacitacao.Text + "' where capAprendiz = " + Session["codAprendizAlterar"] + "";
                    var con = new Conexao();
                    con.Alterar(sql);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                 "alert('Alterado com sucesso.')", true);
                }

                CarregaGridCapacitacao(int.Parse(Session["codAprendiz"].ToString()));

            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('" + ex.Message + "')", true);
            }
            catch (SqlException sql)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('ERRO - Capacitação já inserida para o aprendiz: " + lb_nome_aprendiz.Text + "')", true);
            }
        }



        protected void btnNovaCapacitacao_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 9;
            BuscaTurmaCapicitacao();
            BuscaUnidadeCapicitacao();
            LimpaDadosCapacitacao();
            btnSalvarCapacitacao.Text = "Salvar";
        }

        protected void btnLimparCapacitacoes_Click(object sender, EventArgs e)
        {
            LimpaDadosCapacitacao();
        }

        private void LimpaDadosCapacitacao()
        {
            txtDataInicioCapacitacao.Text = "";
            txtDataPrevisaoCapacitacao.Text = "";
            txtTerminoCapacitacao.Text = "";
            txtObservacoesCapacitacao.Text = "";
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 10;
            CarregaGridCapacitacao(int.Parse(Session["codAprendiz"].ToString()));

        }

        protected void btnAlterarCapacitacao_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            int aprendiz = int.Parse(bt.CommandArgument);

            LimpaDadosCapacitacao();
            BuscaTurmaCapicitacao();
            BuscaUnidadeCapicitacao();
            MultiView1.ActiveViewIndex = 9;


            var sql = "Select *, U.UniCodigo, T.TurCodigo from CA_CapacitacaoAprendiz C join CA_Unidades U on C.CapUnidade = U.UniCodigo join CA_Turmas T on C.CapTurma = T.TurCodigo where C.CapAprendiz = " + aprendiz + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                DDTurmaCapacitacao.SelectedValue = result["capTurma"].ToString();
                DDUnidadeCapacitacao.SelectedValue = result["capUnidade"].ToString();
                DDStatusCapacitacao.SelectedValue = result["capStatus"].ToString();
                txtDataInicioCapacitacao.Text = string.Format("{0:dd/MM/yyyy}", result["CapDataInicio"]);
                txtDataPrevisaoCapacitacao.Text = string.Format("{0:dd/MM/yyyy}", result["CapDataPrevTermino"]);
                txtTerminoCapacitacao.Text = string.Format("{0:dd/MM/yyyy}", result["CapDataTermino"]);
                txtObservacoesCapacitacao.Text = result["capObservacoes"].ToString();
            }
            btnSalvarCapacitacao.Text = "Alterar";
            Session["codAprendizAlterar"] = aprendiz;

        }

        protected void btnGeraCronogramaCapacitacao_Click(object sender, ImageClickEventArgs e)
        {
            if (HFConfirmaCronograma.Value.Equals("false"))
            {
                return;
            }

            string turma = "";
            DateTime dataAula = new DateTime();
            DateTime dataAulaPrevisao = new DateTime();
            string presenca = ".";

            var bt = (ImageButton)sender;
            int aprendiz = int.Parse(bt.CommandArgument);

            var sql = "select * from CA_CapacitacaoAprendiz where CapAprendiz = " + aprendiz + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                turma = result["capTurma"].ToString();
                dataAula = (DateTime)result["CapDataInicio"];
                dataAulaPrevisao = (DateTime)result["CapDataPrevTermino"];
            }

            con = new Conexao();
            for (int i = 0; dataAula <= dataAulaPrevisao; i++)
            {

                if (dataAula.DayOfWeek.Equals(DayOfWeek.Saturday))   //sabado
                {
                    dataAula = dataAula.AddDays(2);
                }


                if (!VerificaFeriado(dataAula))
                {
                    try
                    {
                        sql = "insert into CA_AulasCapacitacaoAprendiz values (" + aprendiz + ", " + turma + ", '" + dataAula + "', '" + presenca + "', '', '')";
                        con.Alterar(sql);
                    }
                    catch (SqlException a)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                            "alert('ERRO - Cronograma já gerado anteriormente.');", true);
                        return;
                    }

                }
                dataAula = dataAula.AddDays(1);
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
          "alert('Cronograma gerado com sucesso.');", true);


        }

        protected void btnGerarPerfil_Click(object sender, ImageClickEventArgs e)
        {
            if (HFConfirmaCronograma.Value.Equals("false"))
            {
                return;
            }
            else
            {

                var bt = (ImageButton)sender;
                int aprendiz = int.Parse(bt.CommandArgument);
                var turma = WebUtility.HtmlDecode(gridCapacitacoes.Rows[0].Cells[2].Text.ToString());
                var codTurma = "";

                var sql = "Select TurCodigo from CA_Turmas where TurNome = '" + turma + "'";
                var con = new Conexao();
                var result = con.Consultar(sql);
                while (result.Read())
                {
                    codTurma = result["TurCodigo"].ToString();
                }

                string notaGeral = "0";
                string notaPortugues = "0";
                string notaMatematica = "0";
                string notaTecnicasADM = "0";
                string notaDigitacao = "0";
                string notaRelacoesHumanas = "0";
                string notaCiencias = "0";
                string notaPluralidade = "0";
                string notaInformatica = "0";
                string notaCaracteristicasGerais = "";
                string notaWord = "0";
                string notaExcel = "0";
                string notaInternet = "0";

                try
                {
                    sql = "insert into CA_AvaliacaoAprendizCapacitacao values (" + aprendiz + ", " + codTurma + ", " + notaGeral + ", " + notaPortugues + ", " + notaMatematica + ", " + notaTecnicasADM + ", " + notaDigitacao + ", " + notaRelacoesHumanas + ", " + notaCiencias + ", " + notaPluralidade + ", " + notaInformatica + ", '" + notaCaracteristicasGerais + "', " + notaWord + ", " + notaExcel + ", " + notaInternet + ")";
                    con = new Conexao();
                    con.Alterar(sql);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                  "alert('Perfil cadastrado com sucesso.');", true);
                }
                catch (SqlException s)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                  "alert('ERRO - Já existe um perfil cadastrado.');", true);
                }
            }
        }

        protected void btnAtualizaPerfil_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 11;
            var turma = WebUtility.HtmlDecode(gridCapacitacoes.Rows[0].Cells[2].Text.ToString());
            var aprendiz = WebUtility.HtmlDecode(gridCapacitacoes.Rows[0].Cells[0].Text.ToString());
            CarregaAvalicaoAprendizCapacitacao(aprendiz, turma);
        }

        protected void btnAtualizarPerfil_Click(object sender, EventArgs e)
        {
            var turma = WebUtility.HtmlDecode(gridCapacitacoes.Rows[0].Cells[2].Text.ToString());
            var aprendiz = WebUtility.HtmlDecode(gridCapacitacoes.Rows[0].Cells[0].Text.ToString());

            var sql = "Select TurCodigo from CA_Turmas where TurNome = '" + turma + "'";
            var con = new Conexao();
            var result = con.Consultar(sql);
            while (result.Read())
            {
                turma = result["TurCodigo"].ToString();
            }

            string notaGeral = txtGeral.Text;
            string notaPortugues = txtPortugues.Text;
            string notaMatematica = txtMatematica.Text;
            string notaTecnicasADM = txtTecnicaADM.Text;
            string notaDigitacao = txtDigitacao.Text;
            string notaRelacoesHumanas = txtRelacoesHumanas.Text;
            string notaCiencias = txtCiencias.Text;
            string notaPluralidade = txtPluralidade.Text;
            string notaInformatica = txtInformatica.Text;
            string notaCaracteristicasGerais = txtCaracteristicasGerais.Text;
            string notaWord = txtWord.Text;
            string notaExcel = txtExcel.Text;
            string notaInternet = txtInternet.Text;

            if (!notaGeral.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaGeral) < 0 || Convert.ToInt32(notaGeral) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaPortugues.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaPortugues) < 0 || Convert.ToInt32(notaPortugues) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaMatematica.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaMatematica) < 0 || Convert.ToInt32(notaMatematica) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }


            if (!notaTecnicasADM.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaTecnicasADM) < 0 || Convert.ToInt32(notaTecnicasADM) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaDigitacao.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaDigitacao) < 0 || Convert.ToInt32(notaDigitacao) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaRelacoesHumanas.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaRelacoesHumanas) < 0 || Convert.ToInt32(notaRelacoesHumanas) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaCiencias.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaCiencias) < 0 || Convert.ToInt32(notaCiencias) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaPluralidade.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaPluralidade) < 0 || Convert.ToInt32(notaPluralidade) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaInformatica.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaInformatica) < 0 || Convert.ToInt32(notaInformatica) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaWord.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaWord) < 0 || Convert.ToInt32(notaWord) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaExcel.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaExcel) < 0 || Convert.ToInt32(notaExcel) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            if (!notaInternet.Equals(string.Empty))
            {
                if (Convert.ToInt32(notaInternet) < 0 || Convert.ToInt32(notaInternet) > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Todas as notas devem estar entre 0 e 10');", true);
                    return;
                }
            }

            sql = "update CA_AvaliacaoAprendizCapacitacao set NcaNotaGeral = " + notaGeral + ", NcaPortugues = " + notaPortugues + ", NcaMatematica = " + notaMatematica + ", NcaTecnicasAdm = " + notaTecnicasADM + ", NcaDigitacao = " + notaDigitacao + ", NcaCiencias = " + notaCiencias + ", NcaPluralidade = " + notaPluralidade + ", NcaInformatica = " + notaInformatica + ",  NcaCaracteristicasGerais = '" + notaCaracteristicasGerais + "', NcaWord = " + notaWord + ", NcaExcel = " + notaExcel + ", NcaInternet = " + notaInternet + " where NcaAprendiz = " + aprendiz + " and NcaTurma = " + turma + "";
            con = new Conexao();
            con.Alterar(sql);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('Perfil atualizado com sucesso.');", true);
        }

        private void CarregaAvalicaoAprendizCapacitacao(string aprendiz, string turma)
        {
            var sql = "Select TurCodigo from CA_Turmas where TurNome = '" + turma + "'";
            var con = new Conexao();
            var result = con.Consultar(sql);
            while (result.Read())
            {
                turma = result["TurCodigo"].ToString();
            }

            sql = "Select * from CA_AvaliacaoAprendizCapacitacao where NcaAprendiz = " + aprendiz + "  and NcaTurma ='" + turma + "'";
            con = new Conexao();
            result = con.Consultar(sql);

            int i = 0;

            while (result.Read())
            {
                i++;
                txtGeral.Text = result["NcaNotaGeral"].ToString();
                txtPortugues.Text = result["NcaPortugues"].ToString();
                txtMatematica.Text = result["NcaMatematica"].ToString();
                txtTecnicaADM.Text = result["NcaTecnicasAdm"].ToString();
                txtDigitacao.Text = result["NcaDigitacao"].ToString();
                txtRelacoesHumanas.Text = result["NcaRelacoesHumanas"].ToString();
                txtCiencias.Text = result["NcaCiencias"].ToString();
                txtPluralidade.Text = result["NcaPluralidade"].ToString();
                txtInformatica.Text = result["NcaInformatica"].ToString();
                txtCaracteristicasGerais.Text = result["NcaCaracteristicasGerais"].ToString();
                txtWord.Text = result["NcaWord"].ToString();
                txtExcel.Text = result["NcaExcel"].ToString();
                txtInternet.Text = result["NcaInternet"].ToString();
            }

            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                        "alert('ERRO - Não existe perfil gerado.');", true);
                MultiView1.ActiveViewIndex = 10;
            }
        }

        protected void btnVoltarPerfil_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 10;
        }

        protected void DDEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencheMunicipio();
        }

        protected void IndiceZeroUF(object sender, EventArgs e)
        {
            var indice0 = new ListItem("--", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btnGerarCronogramaMudancaTurma_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //if (txtDataCronograma.Text.Equals(string.Empty))
            //    //{
            //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
            //    //         "alert('Data Início Cronograma é obrigatório.');", true);
            //    //    return;
            //    //}

            //    string curCodigo = "";
            //    int b = 0;
            //    bool aprovado = false;

            //    for (b = 0; b < GridView2.Rows.Count; b++)
            //    {
            //        curCodigo = GridView2.DataKeys[b]["CurCodigo"].ToString();
            //        if (curCodigo.Equals("002"))
            //        {
            //            aprovado = true;
            //            break;
            //        }
            //    }

            //    if (!aprovado)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Não é possível gerar Simultaneidade')", true);
            //        return;
            //    }

            //    //string data = txtDataCronograma.Text;

            //    //if (data.Equals(string.Empty))
            //    //{
            //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
            //    //     "alert('Data Início Cronograma é obrigatório.');", true);
            //    //    return;
            //    //}
            //    //else
            //    //{
            //    //    //var sql2 = "delete CA_AulasDisciplinasAprendiz where AdiDataAula >= '" + Convert.ToDateTime(txtDataCronograma.Text) + "' and AdiCodAprendiz = " + Session["CodAprendiz"] + "";
            //    //    //var con = new Conexao();
            //    //    //con.Alterar(sql2);
            //    //}


            //    DateTime dataFinal = Convert.ToDateTime(GridView2.Rows[b].Cells[2].Text.ToString());

            //    var turma = GridView2.DataKeys[b]["TurCodigo"].ToString();

            //    //var sql = "Select * from CA_AulasDisciplinasTurmaProf where ADPTurma = " + turma + " and ADPDataAula >= '" + Convert.ToDateTime(txtDataCronograma.Text) + "' and ADPDataAula <= '" + dataFinal + "' order by ADPDataAula";
            //    var conexao = new Conexao();
            //    var result = conexao.Consultar(sql);
            //    while (result.Read())
            //    {
            //        var conexaoInsere = new Conexao();
            //        string insere = "insert into CA_AulasDisciplinasAprendiz values (" + Session["CodAprendiz"] + ", " + turma + ", " + result["ADPDisciplina"] + ", " + result["ADPProfessor"] + ", '" + Convert.ToDateTime(result["ADPDataAula"]) + "', 2, '.', null, '.', '', '', '')";
            //        conexaoInsere.Alterar(insere);
            //    }

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            //                                 "alert('Cronograma gerado com sucesso')", true);
            //}
            //catch (Exception a)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            //                                 "alert('Este cronograma já foi gerado')", true);
            //}
        }

        protected void btnGerarCronogramaIntensivo_Click(object sender, EventArgs e)
        {
            try
            {
                string curCodigo = "";
                int b = 0;
                bool aprovado = false;

                for (b = 0; b < GridView2.Rows.Count; b++)
                {
                    curCodigo = GridView2.DataKeys[b]["CurCodigo"].ToString();
                    if (curCodigo.Equals("005") || curCodigo.Equals("007"))
                    {
                        aprovado = true;
                        break;
                    }
                }

                if (!aprovado)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ERRO - Não é possível gerar Cronograma Intensivo')", true);
                    return;
                }

                DateTime dataInicial = Convert.ToDateTime(GridView2.Rows[b].Cells[1].Text.ToString());
                DateTime dataFinal = Convert.ToDateTime(GridView2.Rows[b].Cells[2].Text.ToString());


                string dataI = GridView2.Rows[b].Cells[1].Text.ToString();
                string dataF = GridView2.Rows[b].Cells[2].Text.ToString();
                var numaula = GridView2.DataKeys[b]["TurNumeroMeses"].ToString();


                var turma = GridView2.DataKeys[b]["TurCodigo"].ToString();

                var sql = "Select * from CA_AulasDisciplinasTurmaProf where ADPTurma = " + turma + " and ADPDataAula >= '" + dataInicial + "' and ADPDataAula <= '" + dataFinal + "' order by ADPDataAula";
                var conexao = new Conexao();
                var aulas = 0;
                var result = conexao.Consultar(sql);
                while (result.Read() && (aulas < Convert.ToInt32(numaula)))
                {
                    var conexaoInsere = new Conexao();
                    string insere = "insert into CA_AulasDisciplinasAprendiz values (" + Session["CodAprendiz"] + ", " + turma + ", " + result["ADPDisciplina"] + ", " + result["ADPProfessor"] + ", '" + Convert.ToDateTime(result["ADPDataAula"]) + "', 2, '.', null, '.','','', '',0)";
                    conexaoInsere.Alterar(insere);
                    aulas++;
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Cronograma gerado com sucesso')", true);
            }
            catch (Exception a)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Este cronograma já foi gerado')", true);
            }
        }


        private bool ValidaCodigo005(int tipoEncontro, int disciplina)
        {

            var aprendiz = Session["prmt_aprendiz_selecionado"];
            var sql = "select * from View_AlocacoesAlunos where TurCurso = '005' and AlaAprendiz = " + aprendiz + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                InsereCronogramaPrimeiroEncontroESegundoEncontro(result, tipoEncontro, disciplina);
                return true;

            }
            return false;
        }

        /// <summary>
        /// funcao abaixo insere as diciplinas a partir do dropdown selecionado
        /// por exemplo: selecionado Disciplina: Sistemas e value 3
        /// ele ira inserir a disciplina value 3 adiante...
        /// </summary>
        protected void InsereCronogramaPrimeiroEncontroESegundoEncontro(SqlDataReader dados, int tipoEncontro, int disciplina)
        {
            // thassio

            string diaSemana = dados["TurDiaSemana"].ToString();

            if (diaSemana.Equals("2"))
            {
                diaSemana = "Monday";
            }
            else if (diaSemana.Equals("3"))
            {
                diaSemana = "Tuesday";
            }
            else if (diaSemana.Equals("4"))
            {
                diaSemana = "Wednesday";
            }
            else if (diaSemana.Equals("5"))
            {
                diaSemana = "Thursday";
            }
            else if (diaSemana.Equals("6"))
            {
                diaSemana = "Friday";
            }

            var con = new Conexao();
            var sql = "Select * from CA_DatasEncontros where DteData between '" + dados["AlaDataInicio"] + "' and '" + dados["AlaDataPrevTermino"] + "' and DteTipoEncontro = " + tipoEncontro + "";
            var result = con.Consultar(sql);

            while (result.Read())
            {
                DateTime dataEncontro = (DateTime)result["DteData"];
                if (dataEncontro.DayOfWeek.ToString().Equals(diaSemana))
                {
                    sql = "insert into CA_AulasDisciplinasAprendiz values (" + dados["ALAAprendiz"] + " , " + dados["ALATurma"] + ", " + disciplina + ", 1, '" + result["DteData"] + "', 6, '.', '','.','','', '')";
                    con = new Conexao();
                    con.Alterar(sql);
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                        "alert('Encontro cadastrado com sucesso.')", true);


      
        }

        protected void btnCalendarioAprendiz_Click(object sender, EventArgs e)
        {
            var lb = (Button)sender;
            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
            var dados = from i in bd.CA_AutorizacaoUsuarios
                        join m in bd.CA_Usuarios on i.AutFUsuario equals m.UsuTipo
                        where m.UsuCodigo.Equals(Session["codigo"].ToString())
                        select new { i.AutFTipoAut, i.AutFFuncao };
            if (dados.Count().Equals(0))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                    "alert('ERRO - Função não habilitada para seu perfil de usuário.')", true);
                return;
            }

            Session["matriculaCalendario"] = LB_matricula.Text;
            var tipoacesso = Criptografia.Encrypt(dados.First().AutFTipoAut, GetConfig.Key());
            Response.Redirect("CalendarioAprendiz.aspx?acs=" + tipoacesso + "&id=" + lb_codigo_aprendiz.Text);
        }

        protected void btnRegerarCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                var sqlExclui = "delete CA_AulasDisciplinasAprendiz where AdiCodAprendiz = " + Session["CodAprendiz"] + "  ";
                var conExclui = new Conexao();
                conExclui.Consultar(sqlExclui);


                string curCodigo = "";
                int b = 0;
                bool aprovado = false;

                for (b = 0; b < GridView2.Rows.Count; b++)
                {
                    curCodigo = GridView2.DataKeys[b]["CurCodigo"].ToString();
                    if (curCodigo.Equals("002"))
                    {
                        aprovado = true;
                        break;
                    }
                }

                if (!aprovado)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ERRO - Não é possível gerar Simultaneidade')", true);
                    return;
                }

                DateTime dataFinal = Convert.ToDateTime(GridView2.Rows[b].Cells[2].Text.ToString());
                DateTime dataInicial = Convert.ToDateTime(GridView2.Rows[b].Cells[1].Text.ToString());

                var turma = GridView2.DataKeys[b]["TurCodigo"].ToString();
                var numaula = GridView2.DataKeys[b]["TurNumeroMeses"].ToString();


                var sql = "Select * from CA_AulasDisciplinasTurmaProf where ADPTurma = " + turma + " and ADPDataAula >= '" + dataInicial + "' and ADPDataAula <= '" + dataFinal + "' order by ADPDataAula";
                var conexao = new Conexao();
                var aulas = 0;
                var result = conexao.Consultar(sql);
                while (result.Read() && (aulas < Convert.ToInt32(numaula)))
                {
                    var conexaoInsere = new Conexao();
                    string insere = "insert into CA_AulasDisciplinasAprendiz values (" + Session["CodAprendiz"] + ", " + turma + ", " + result["ADPDisciplina"] + ", " + result["ADPProfessor"] + ", '" + Convert.ToDateTime(result["ADPDataAula"]) + "', 2, '.', null, '.','','', '', 0)";
                    conexaoInsere.Alterar(insere);
                    aulas++;
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Cronograma REGERADO com sucesso')", true);
            }
            catch (Exception a)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Este cronograma já foi gerado')", true);
            }

        }

    }





}