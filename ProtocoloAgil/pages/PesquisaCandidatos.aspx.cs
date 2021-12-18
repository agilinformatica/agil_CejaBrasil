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
using System.Net;

namespace ProtocoloAgil.pages
{
    public partial class PesquisaCandidatos : Page
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
            public int Apr_Idade { get; set; }
            public string Apr_TurnoEscolar { get; set; }
            public string Apr_EstudaAtualmente { get; set; }

            public int Encaminhamento { get; set; }

            public string Apr_Bairro { get; set; }
            public string Apr_Cidade { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "aprendiz";
            // Page.Form.DefaultButton = btn_pesquisa.UniqueID;
            if (!IsPostBack)
            {
                //BindCursos(DDcurso_origem);
                //BindCursos(DDcurso_destino);
                CarregarDropSituacaoAprendiz();
                BuscaTurmaCapacitacao();
                MultiView1.ActiveViewIndex = 0;
                txtIdadeInicio.Text = "0";
                txtIdadeTermino.Text = "0";
            }
        }


        protected void CarregarDropSituacaoAprendiz()
        {

            var situacoes = new int[5];
            situacoes[0] = 1;
            situacoes[1] = 7;
            situacoes[2] = 8;
            situacoes[3] = 9;
            situacoes[4] = 10;

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from S in db.CA_SituacaoAprendizs
                             where situacoes.Contains(S.StaCodigo)
                             select new { S.StaCodigo, S.StaDescricao }).ToList().OrderBy(item => item.StaDescricao);

                DDSituacao.DataSource = query;
                DDSituacao.DataBind();

            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {


            // var cb = (CheckBoxList)CBSituacaoAprendiz.FindControl("CBSituacaoAprendiz");

            var sql = "";
            var con = new Conexao();
            //   con.Consultar(sql);


            //List<ListItem> selected = new List<ListItem>();
            //foreach (ListItem item in cb.Items)
            //    if (item.Selected)
            //    {
            //        selected.Add(item);
            //    }

            //string[] bairro = new string[selected.Count];

            //for (int i = 0; i < selected.Count; i++) {


            //    bairro[i] += selected[i].ToString();


            //}


            //marcado[0] += "Jardim Felicidade";
            //marcado[1] += "Conjunto Felicidade";
            CarregaDadosAprendiz();
        }



        protected void gridAprendiz_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridAprendiz.PageIndex = e.NewPageIndex;
            CarregaDadosAprendiz();
        }

        public void CarregaDadosAprendiz()
        {



            //bairro[0] = "Jardim Felicidade";
            //bairro[1] = "Conjunto Felicidade";

            //var situacoes = new int[4];
            //situacoes[0] = 1;
            //situacoes[1] = 8;
            //situacoes[2] = 9;
            //situacoes[3] = 10;

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {


                var selected = from u in bd.View_CA_ListaCandidatos
                               where u.Apr_Nome.Equals(string.Empty) ? 1 == 1 : SqlMethods.Like(u.Apr_Nome, "%" + TBNome.Text + "%")

                               select u;


                IQueryable<View_CA_ListaCandidato> lista;





                if (!DDSexo.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.Apr_Sexo == DDSexo.SelectedValue);

                }

                if (!DDSituacao.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.Apr_Situacao == int.Parse(DDSituacao.SelectedValue));

                }



                if (!DDEstudaAtualmente.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.Apr_Estudante.Equals(DDEstudaAtualmente.SelectedValue));
                }

                if (!DDTurnoEscolar.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.Apr_TurnoEscolar.Equals(DDTurnoEscolar.SelectedValue));
                }

                if (!DDMunicipio.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.Apr_Cidade.Equals(DDMunicipio.SelectedValue));
                }

                if (!(txtIdadeInicio.Text.Equals("0") && txtIdadeTermino.Text.Equals("0")))
                {
                    if (!(txtIdadeInicio.Text.Equals("0") && txtIdadeTermino.Text.Equals("0") && txtIdadeInicio.Text.Equals(string.Empty) && txtIdadeTermino.Text.Equals(string.Empty)))
                    {
                        if (int.Parse(txtIdadeInicio.Text) > int.Parse(txtIdadeTermino.Text))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                      "alert('A primeira idade não pode ser maior que a segunda')", true);
                            return;
                        }
                        selected = selected.Where(item => item.Idade >= int.Parse(txtIdadeInicio.Text) && item.Idade <= int.Parse(txtIdadeTermino.Text));
                    }
                }


                if (!DDTurmaCapacitacao.SelectedValue.Equals(string.Empty))
                {
                    selected = selected.Where(item => item.Apr_TurmaENC == int.Parse(DDTurmaCapacitacao.SelectedValue)).Where(item => item.Apr_Situacao == 8 || item.Apr_Situacao == 1);

                }

                selected = selected.OrderBy(item => item.Enc).ThenBy(item => item.Apr_Nome);
                lista = selected;

                var situacao = (from i in bd.CA_SituacaoAprendizs select i).ToList();
                var datasource = lista.ToList().Select(p => new AprendizPesquisa
                {
                    Apr_Codigo = p.Apr_Codigo.ToString(),
                    Apr_Nome = p.Apr_Nome,
                    Apr_Sexo = p.Apr_Sexo,
                    Apr_Telefone = Funcoes.FormataTelefone(p.Apr_Telefone),
                    // Apr_Email = p.Apr_Email,
                    Apr_Cidade = p.Apr_Cidade,
                    Apr_Bairro = p.Apr_Bairro,

                    Apr_Idade = p.Idade == null ? new int() : (int)p.Idade,
                    Apr_TurnoEscolar = p.Apr_TurnoEscolar,
                    Apr_EstudaAtualmente = p.Apr_Estudante,
                    Encaminhamento = (int)p.Enc,

                    StaDescricao = p.Apr_Situacao == 0 ? "" : (situacao.Where(x => x.StaCodigo.Equals(p.Apr_Situacao)).First().StaDescricao).ToString()
                    //Apr_AreaAtuacao = p.Apr_AreaAtuacao.ToString(),
                    // Apr_PlanoCurricular = (short)p.Apr_PlanoCurricular

                }).ToList().OrderBy(p => p.Apr_Nome);
                gridAprendiz.DataSource = datasource.ToList();


                gridAprendiz.DataBind();
                gridAprendiz.Visible = true;
                //MultiView1.ActiveViewIndex = 1;

            }

        }

        protected void gridAprendiz_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cod = "";
            if (!e.CommandName.Equals(string.Empty) && !e.CommandName.Equals("Page"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridAprendiz.Rows[index];
                cod = row.Cells[0].Text;
            }
            if (e.CommandName == "Encaminhamentos")
            {
                ExibeDados(cod);
            }
        }

        protected void gridHistorico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridHistorico.Rows[index];
            string sequencia = row.Cells[0].Text;
            string status = row.Cells[3].Text;


            if (e.CommandName == "EdicaoHistorico")
            {
                //if (!status.Equals("EM PROCESSO SELETIVO"))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                //    "alert('Esta vaga está com status: " + status + ", por isso não é possível editar');", true);
                //}
                //else
                //{
                txtFeedback.Enabled = true;
                buscaDadosHistorico(txtCodigo.Text, int.Parse(sequencia));
                ShowAdress(int.Parse(Session["codEmpresa"].ToString()));
                //  }

            }
            if (e.CommandName == "ImprimirCarta")
            {
                string cod = row.Cells[0].Text;
                Session["CodFichaEncaminhamento"] = cod;
                Session["id"] = "104";
                MultiView1.ActiveViewIndex = 7;
            }

        }



        public void buscaDadosHistorico(string cod, int sequencia)
        {
            // DDStatus.SelectedValue = "SE";
            txtData.Enabled = false;
            DDEmpresa.Enabled = false;
            DDStatus.Enabled = true;


            Session["editar"] = "editar";


            var con = new Conexao();
            var sql = @"select e.Enc_Observacoes, e.Enc_Status, p.ParUniCodigo, e.Enc_DataEncaminha, e.Enc_Sequencia, e.Enc_CodigoAprendiz, e.Enc_Requisicao, e.Enc_Feedback from CA_Encaminhamentos e
                        join CA_ParceirosUnidade p on e.Enc_UnidParceiro = p.ParUniCodigo
                        join CA_StatusEncaminhamento s on e.Enc_Status = s.Ste_Codigo
                        where e.Enc_CodigoAprendiz = " + cod + " and Enc_Sequencia = " + sequencia + "";
            var result = con.Consultar(sql);

            while (result.Read())
            {
                CarregaEmpresa();
                txtObservacao.Text = result[0].ToString();
                DDStatus.SelectedValue = result[1].ToString();
                DDEmpresa.SelectedValue = result[2].ToString();
                txtData.Text = result[3].ToString().Substring(0, 10);
                Session["sequencia"] = result[4].ToString();
                Session["matricula"] = result[5].ToString();

                Session["codEmpresa"] = result[2].ToString();
                CarregaVagas(int.Parse(result[2].ToString()));
                DDVaga.SelectedValue = result[6].ToString();
                txtFeedback.Text = result["Enc_Feedback"].ToString();


            }

            MultiView1.ActiveViewIndex = 2;

        }
        private void CarregaEmpresa()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from P in db.CA_ParceirosUnidades
                             //where situacoes.Contains(S.StaCodigo)
                             select new { P.ParUniCodigo, P.ParUniDescricao }).ToList().OrderBy(item => item.ParUniDescricao);

                DDEmpresa.DataSource = query;
                DDEmpresa.DataBind();


            }
        }


        //protected void BindUsuarios()
        //{
        //    using (var reposytory = new Repository<Usuarios>(new Context<Usuarios>()))
        //    {
        //        var list = new List<Usuarios>();
        //        list.AddRange(reposytory.All().OrderBy(p => p.UsuNome));
        //        DDusuario.Items.Clear();
        //        DDusuario.DataSource = list.ToList();
        //        DDusuario.DataBind();
        //    }
        //}

        private string CalculaIdade(DateTime idade)
        {
            var data = DateTime.Today.Subtract(idade);
            return (data.Days / 365).ToString();
        }
        public void ExibeDados(string cod)
        {
            try
            {

                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {


                }

                using (var repository = new Repository<Aprendiz>(new Context<Aprendiz>()))
                {
                    //  BindUsuarios();
                    Session["matricula"] = cod;
                    CarregaFoto();
                    //CarregaLogo();
                    carregaHistoricoEntrevistas();

                    var aprendiz = repository.Find(Convert.ToInt32(cod));



                    txtCodigo.Text = aprendiz.Apr_Codigo.ToString();
                    txtNome.Text = aprendiz.Apr_Nome == null ? "" : aprendiz.Apr_Nome;
                    txtRG.Text = aprendiz.Apr_CarteiraDeIdentidade ?? ""; ;

                    txtCPF.Text = aprendiz.Apr_CPF == null ? "" : Funcoes.FormataCPF(aprendiz.Apr_CPF);

                    txtDataNascimento.Text = aprendiz.Apr_DataDeNascimento == null ? "" : ValidaData(Convert.ToDateTime(aprendiz.Apr_DataDeNascimento));
                    txtIdade.Text = aprendiz.Apr_DataDeNascimento == null ? "" : CalculaIdade((DateTime)aprendiz.Apr_DataDeNascimento);
                    DDsexoAlu.SelectedValue = aprendiz.Apr_Sexo ?? "";
                    txtEmail.Text = aprendiz.Apr_Email ?? "";

                    txtCep.Text = aprendiz.Apr_CEP == null ? "" : Funcoes.FormataCep(aprendiz.Apr_CEP);
                    txtTelefone.Text = aprendiz.Apr_Telefone == null ? "" : Funcoes.FormataTelefone(aprendiz.Apr_Telefone);
                    txtCelular.Text = aprendiz.Apr_Celular == null ? "" : Funcoes.FormataTelefone(aprendiz.Apr_Celular);

                    txtEndereco.Text = aprendiz.Apr_Endereço ?? "";
                    txtBairro.Text = aprendiz.Apr_Bairro ?? "";
                    txtNumero.Text = aprendiz.Apr_NumeroEndereco ?? "";

                    if (aprendiz.Apr_Escolaridade != null)
                    {
                        var con = new Conexao();
                        var sql = "select GreDescricao from CA_GrauEscolaridade where GreCodigo = " + aprendiz.Apr_Escolaridade;
                        var result = con.Consultar(sql);

                        while (result.Read())
                        {
                            txtSerie.Text = result[0].ToString() ?? "";
                            break;
                        }
                    }


                    MultiView1.ActiveViewIndex = 1;
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000126", ex);
            }
        }


        private void CarregaFoto()
        {
            if (Session["matricula"] == null)
            {
                IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                return;
            }

            var matricula = Session["matricula"].ToString();
            var filePath = Server.MapPath(@"/files/fotos");
            var dir = new DirectoryInfo(filePath);
            if (dir.Exists)
            {
                var files = dir.GetFiles().ToList();
                //var foto = files.Where(p => p .Name.Contains(matricula)).ToList();
                //var foto = files.Where(p => p.Name.Substring(0, 4).Equals(matricula)).ToList();
                var foto = files.Where(p => p.Name.Equals(matricula + ".jpg")).ToList();
                if (foto.Count() > 0)
                {
                    var path = "../files/fotos/" + foto.First().Name;
                    IMG_foto_aprendiz.Attributes.Add("src", path);
                }
                else
                {
                    IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                }
            }
        }

        //private void CarregaLogo()
        //{
        //    var matricula = Session["matricula"].ToString();
        //    var filePath = Server.MapPath(@"/images");
        //    var dir = new DirectoryInfo(filePath);
        //    if (dir.Exists)
        //    {
        //        var files = dir.GetFiles().ToList();
        //        //var foto = files.Where(p => p .Name.Contains(matricula)).ToList();
        //        var path = "../images/logofundacao.png";
        //        IMG_Logo_Nurap.Attributes.Add("src", path);
        //    }
        //}


        private string ValidaData(DateTime data)
        {
            var datastring = string.Format("{0:dd/MM/yyy}", data);
            if (datastring.Equals("01/01/1900")) return "";
            return datastring;
        }

        protected void btnEncaminhamento_Click(object sender, EventArgs e)
        {
            trLabel.Visible = false;
            trTitulos.Visible = false;
            CarregaEmpresa();
            DDVaga.Items.Clear();
            DDStatus.SelectedValue = "SE";
            txtData.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            txtObservacao.Text = "";
            txtData.Enabled = true;
            DDEmpresa.Enabled = true;
            DDStatus.Enabled = false;
            MultiView1.ActiveViewIndex = 2;
            lblEndereco.Text = "";
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDVaga.SelectedValue.Equals(string.Empty))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                  "alert('É necessário selecionar uma empresa que possua vaga.')", true);
                    return;
                }


                string mensagem = ValidaCampos();
                if (mensagem.Equals(""))
                {
                    if (txtData.Enabled == false)
                    {
                        // teste
                        var con = new Conexao();
                        var sql = "Update CA_Encaminhamentos set Enc_Status = '" + DDStatus.SelectedValue + "' , Enc_Observacoes = '" + txtObservacao.Text + "' , Enc_Requisicao = " + DDVaga.SelectedValue + ", Enc_Feedback = '"+txtFeedback.Text+"'  where Enc_CodigoAprendiz = " + Session["matricula"] + " and Enc_Sequencia = " + Session["sequencia"] + "";
                        con.Alterar(sql);
                    }
                    else
                    {
                        var con = new Conexao();
                        var sql = "insert into CA_Encaminhamentos (Enc_CodigoAprendiz, Enc_DataEncaminha , Enc_UnidParceiro, Enc_Status, Enc_Observacoes, Enc_Usuario, Enc_Requisicao) values (" + txtCodigo.Text + ",'" + txtData.Text + "', " + DDEmpresa.SelectedValue + ",'" + DDStatus.SelectedValue + "','" + txtObservacao.Text + "', '" + Session["codigo"].ToString() + "', " + DDVaga.SelectedValue + ", '"+txtFeedback.Text+"')";
                        con.Alterar(sql);
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                   "alert('Ação Realizada com Sucesso.')", true);

                    DDEmpresa.SelectedValue = "";
                    MultiView1.ActiveViewIndex = 1;
                    carregaHistoricoEntrevistas();
                    DDEmpresa.SelectedValue = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                   "alert('" + mensagem + "')", true);
                }
            }
            catch (Exception x)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                               "alert('Já existe um encaminhamento aberto para este aprendiz nesta data.')", true);

            }
        }

        public string ValidaCampos()
        {

            string mensagem = "";
            if (txtObservacao.Text.Equals(""))
            {
                txtObservacao.Text = " ";
            }

            if (DDEmpresa.SelectedValue.Equals(""))
            {
                mensagem = "Empresa é um campo obrigatório";
            }

            if (txtData.Text.Equals(""))
            {
                mensagem = "Data é um campo obrigatório.";

            }

            return mensagem;
        }




        public void carregaHistoricoEntrevistas()
        {


            try
            {
                using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    var datasource = from e in bd.CA_Encaminhamentos
                                     join p in bd.CA_ParceirosUnidades on e.Enc_UnidParceiro equals p.ParUniCodigo
                                     join s in bd.CA_StatusEncaminhamentos on e.Enc_Status equals s.Ste_Codigo
                                     where e.Enc_CodigoAprendiz.Equals(Session["matricula"])

                                     select new { e.Enc_Sequencia, e.Enc_DataEncaminha, p.ParUniDescricao, s.Ste_Descricao, e.Enc_Observacoes };

                    gridHistorico.DataSource = datasource;
                    gridHistorico.DataBind();
                }
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000158", ex);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            //DDEmpresa.SelectedValue = "";
            DDEmpresa.SelectedValue = "";
        }

        protected void btnVoltar1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;

        }



        protected void DDEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DDEmpresa.SelectedValue.Equals(""))
            {
                lblEndereco.Text = "";
            }
            else
            {
                ShowAdress(int.Parse(DDEmpresa.SelectedValue));
                CarregaVagas(int.Parse(DDEmpresa.SelectedValue));
            }

        }

        private void CarregaVagas(int codEmpresa)
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from R in db.CA_RequisicoesVagas
                             join A in db.CA_AreaAtuacaos on R.ReqAreaAtuacao equals A.AreaCodigo
                             //where R.ReqSituacao.Equals("A") && R.ReqEmpresa.Equals(codEmpresa)
                             where R.ReqEmpresa.Equals(codEmpresa)
                             select new { A.AreaDescricao, R.ReqId }).ToList().OrderBy(item => item.ReqId);

                DDVaga.DataSource = query;




                DDVaga.DataBind();
                var indice0 = new ListItem("Selecione", "");
                DDVaga.Items.Insert(0, indice0);

            }
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
                lblEndereco.Text = sb.ToString();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            DDSituacao.Items.Insert(0, indice0); //Adiciona um novo Item
            DDEmpresa.Items.Insert(0, indice0); //Adiciona um novo Item
            DDMunicipio.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void txtIdadeInicio_TextChanged(object sender, EventArgs e)
        {
            txtIdadeTermino.Text = txtIdadeInicio.Text;
        }

        protected void DDVaga_TextChanged(object sender, EventArgs e)
        {
            if (DDVaga.SelectedValue.Equals(string.Empty))
            {
                trLabel.Visible = false;
                trTitulos.Visible = false;
                return;
            }
            else
            {
                trLabel.Visible = true;
                trTitulos.Visible = true;
            }


            var sql = "Select ReqSalario, ReqHorarioTrabalho, ReqHabilidades from CA_RequisicoesVagas where ReqId = " + DDVaga.SelectedValue + "";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                lblSalario.Text = result["ReqSalario"].ToString();
                lblHorarioTrabalho.Text = result["ReqHorarioTrabalho"].ToString();
                lblHabilidades.Text = result["ReqHabilidades"].ToString();
            }

        }

        protected void btnConsultarVaga_Click(object sender, EventArgs e)
        {

            if (DDEmpresa.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
               "alert('Para consultar os dados da vaga selecione a empresa');", true);

                return;
            }

            if (DDVaga.SelectedValue.Equals(string.Empty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
               "alert('Para consultar os dados da vaga selecione a vaga desejada');", true);

                return;
            }

            var popup = "popup('VagaPopup.aspx?" +
                       "reqId=" + Criptografia.Encrypt(DDVaga.SelectedValue, GetConfig.Key()) +
                       "&empresa=" + Criptografia.Encrypt(DDEmpresa.SelectedValue, GetConfig.Key()) + "', '700','500');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), popup, true);
        }

        protected void btnAtualizaPerfil_Click(object sender, ImageClickEventArgs e)
        {
            var bt = (ImageButton)sender;
            string aprendiz = bt.CommandArgument;

            MultiView1.ActiveViewIndex = 3;
            // var turma = WebUtility.HtmlDecode(gridAprendiz.Rows[0].Cells[2].Text.ToString());
            Session["aprendizPerfil"] = WebUtility.HtmlDecode(gridAprendiz.Rows[0].Cells[0].Text.ToString());
            //Session["aprendizPerfil"] = aprendiz;
            //  BuscaTurma();
            CarregaAvalicaoAprendizCapacitacao(aprendiz);
            //CarregaAvalicaoAprendizCapacitacao(aprendiz, turma);
        }

        private void CarregaAvalicaoAprendizCapacitacao(string aprendiz)
        {
            var sql = "Select * from CA_AvaliacaoAprendizCapacitacao where NcaAprendiz = " + aprendiz;
            var con = new Conexao();
            var result = con.Consultar(sql);

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
            if (i > 0)
            {
                MultiView1.ActiveViewIndex = 4;
            }
            else
            {
                painelPerfil.Visible = true;
            }
        }

        //private void BuscaTurma()
        //{
        //    using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
        //    {
        //        var query = (from T in db.CA_Turmas
        //                     where T.TurCurso.Equals("003")
        //                     select new { T.TurCodigo, T.TurNome }).ToList().OrderBy(item => item.TurNome);

        //        DDTurma.DataSource = query;
        //        DDTurma.DataBind();
        //        DDTurma.Items.Insert(0, "Selecione..");
        //        DDTurma.SelectedIndex = 0;
        //    }
        //}

        //protected void DDTurma_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!DDTurma.SelectedValue.Equals("Selecione.."))
        //    {
        //        var aprendiz = Session["aprendizPerfil"].ToString();
        //        var turma = DDTurma.SelectedValue;
        //      //  CarregaAvalicaoAprendizCapacitacao(aprendiz, turma);
        //    }
        //}

        protected void btnVoltarPerfil_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
        }

        protected void btnAvaliacoes_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            GridViewRow row = btn.NamingContainer as GridViewRow;

            var bt = (ImageButton)sender;
            var codigo = bt.CommandArgument;

            var nome = gridAprendiz.DataKeys[row.RowIndex]["Apr_Nome"].ToString();
            //var unidade = GridView1.DataKeys[row.RowIndex]["ParUniDescricao"].ToString();

            LB_Aprendiz.Text = nome;
            //LB_empresa.Text = unidade;
            int pepCodigo = 0;
            string codParceiro = "";
            var sql = "select pepCodigo, ParUniDescricao from CA_Pesquisa_Parceiro PP join CA_ParceirosUnidade P on PP.PepParceiroCodigo = P.ParUniCodigo where PepAprendiz = " + codigo + "";
            var con = new Conexao();
            var result = con.Consultar(sql);
            while (result.Read())
            {

                pepCodigo = int.Parse(result["pepCodigo"].ToString());
                codParceiro = result["ParUniDescricao"].ToString();
                break;
            }
            LB_empresa.Text = codParceiro;
            using (var repository = new Repository<PesquisaParceiro>(new Context<PesquisaParceiro>()))
            {
                var pesquisa = repository.Find(pepCodigo);

                if (pesquisa == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
          "alert('Não existe avaliação para este aprendiz.');", true);
                    return;
                }
                BindRepeater(pesquisa);
                MultiView1.ActiveViewIndex = 5;
                // btn_print.Visible = false;
            }
        }

        private void BindRepeater(PesquisaParceiro dados)
        {


            CarregaFotoAvaliacao(dados.PepAprendiz);

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                //preenchimento cabeçalho, de acordo com a alocação vigente do apendiz.
                //var aprendiz =
                //    bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == dados.PepAprendiz && p.ALAStatus == "A").First();

                //var aprendiz =
                //    bd.View_AlocacoesAlunos.Where(p => p.ALAAprendiz == dados.PepAprendiz).OrderByDescending(p => p.ALADataInicio).First();

                //LB_Aprendiz.Text = aprendiz.Apr_Nome;
                //LB_empresa.Text = aprendiz.ParNomeFantasia;
                //LB_Orientador.Text = aprendiz.OriNome;
                //LB_Horario.Text = string.Format("{0:HH:mm}", aprendiz.ALAInicioExpediente) + " às " +
                //                  string.Format("{0:HH:mm}", aprendiz.ALATerminoExpediente);
                LB_Data.Text = string.Format("{0:dd/MM/yyyy}", dados.PepDataRealizada);
                LB_Responsavel.Text = dados.PepTutor;
                //bd.CA_Usuarios.Where(p => p.UsuCodigo == dados.PepTutor).First().UsuNome;

                /*  O sistema carrega a pesquisa de acordo com o código e gera dinamicamente os controles de acordo com o tipo de questão
                *  os tipos são: M - Multipla escolha, A - Aberta ou T - Multipla escolha com justificativa.
                *  cada controle criado é adicionado ao painel Pn_Pesquisa.
                *  Literais são utilizados para quebra de linha e marcação html.
               */

                var respostas = bd.View_Resposta_Pesquisas.Where(p => p.PepCodigo == dados.PepCodigo).ToList();
                var pesquisa = bd.View_Pequisas.Where(p => p.QuePesquisa == dados.PepPesquisaCodigo);
                Lb_Nome_pesquisa.Text = pesquisa.First().PesNome;
                Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/>"));
                var codigoPesquisa = respostas.First().PepPesquisaCodigo;

                foreach (var questao in respostas.OrderBy(p => p.QueOrdemExibicao).Distinct())
                {
                    Pn_Pesquisa.Controls.Add(new Label { ID = "LB_questao_" + questao.QueCodigo, Text = questao.QueTexto, CssClass = "fonteTab" });
                    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/>"));
                    Pn_Pesquisa.Controls.Add(new Label
                    {
                        ID = "LB_resposta_" + questao.ResOpcaoEscolhida,
                        Text = "<strong>Resposta: </strong>" +
                            (questao.QueTipoQustao == "M" ? questao.OpcTexto : questao.ResRespostaTexto),
                        CssClass = "fonteTexto"
                    });
                    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/>"));

                    if (codigoPesquisa != 3)
                    {
                        Pn_Pesquisa.Controls.Add(new Label
                        {
                            ID = "LB_valor_" + questao.ResOpcaoEscolhida,
                            Text = "<strong>Valor: </strong>" +
                                (questao.QueTipoQustao == "M" ? questao.OpcNota.ToString() : "Não aplicável."),
                            CssClass = "fonteTexto"
                        });
                    }

                    Pn_Pesquisa.Controls.Add(Funcoes.GetLiteral("<br/><br/>"));
                }

                foreach (var questao in respostas.OrderBy(p => p.QueOrdemExibicao).Distinct())
                {
                    Pn_Pesquisa.Controls.Add(new Label
                    {
                        ID = "LB_valor_" + questao.ResOpcaoEscolhida,
                        Text = "<strong>Observação: </strong>" +
                            (questao.PepObservacao),
                        CssClass = "fonteTexto"
                    });

                    break;
                }

                string conceito;
                float media = 0;


            }
        }

        private void CarregaFoto(int cod)
        {
            if (cod == null)
            {
                IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                return;
            }

            var matricula = cod;
            var filePath = Server.MapPath(@"/files/fotos");
            var dir = new DirectoryInfo(filePath);
            if (dir.Exists)
            {
                var files = dir.GetFiles().ToList();
                //var foto = files.Where(p => p .Name.Contains(matricula)).ToList();
                //var foto = files.Where(p => p.Name.Substring(0, 4).Equals(matricula)).ToList();
                var foto = files.Where(p => p.Name.Equals(matricula + ".jpg")).ToList();
                if (foto.Count() > 0)
                {
                    var path = "../files/fotos/" + foto.First().Name;
                    IMG_foto_aprendiz.Attributes.Add("src", path);
                }
                else
                {
                    IMG_foto_aprendiz.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                }
            }
        }

        private void CarregaFotoAvaliacao(int cod)
        {
            if (cod == null)
            {
                fotoAvaliacao.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                return;
            }

            var matricula = cod;
            var filePath = Server.MapPath(@"/files/fotos");
            var dir = new DirectoryInfo(filePath);
            if (dir.Exists)
            {
                var files = dir.GetFiles().ToList();
                //var foto = files.Where(p => p .Name.Contains(matricula)).ToList();
                //var foto = files.Where(p => p.Name.Substring(0, 4).Equals(matricula)).ToList();
                var foto = files.Where(p => p.Name.Equals(matricula + ".jpg")).ToList();
                if (foto.Count() > 0)
                {
                    var path = "../files/fotos/" + foto.First().Name;
                    fotoAvaliacao.Attributes.Add("src", path);
                }
                else
                {
                    fotoAvaliacao.Attributes.Add("src", "../files/fotos/semfoto.jpg");
                }
            }
        }

        private void BuscaTurmaCapacitacao()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from T in db.CA_Turmas
                             where T.TurCurso.Equals("003")
                             select new { T.TurCodigo, T.TurNome }).ToList().OrderBy(item => item.TurNome);

                DDTurmaCapacitacao.DataSource = query;
                DDTurmaCapacitacao.DataBind();

                var indice0 = new ListItem("Selecione", "");
                DDTurmaCapacitacao.Items.Insert(0, indice0); //Adiciona um novo Item
            }
        }

        protected void btnConsultaJovem_Click(object sender, ImageClickEventArgs e)
        {
            //var btn = (ImageButton)sender;
            //GridViewRow row = btn.NamingContainer as GridViewRow;

            var bt = (ImageButton)sender;
            var codigo = bt.CommandArgument;

            Session["Comando"] = "PesquisaCandidato";
            Session["matricula"] = codigo;
            MultiView1.ActiveViewIndex = 6;
        }


    }


}
