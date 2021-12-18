using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;

namespace ProtocoloAgil.pages
{
    public partial class AniversariantesPeriodo : Page
    {

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
                    Session["CurrentPage"] = "geralprofessores";
                    MasterPageFile = "~/MaEducador.Master";
                    break;

                default:
                    Funcoes.TrataExcessao("000000", new Exception("../Default.aspx"));
                    break;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if (!IsPostBack)
            {
                BindCursos();
                MultiView1.ActiveViewIndex = 0;
                ViewState.Add("list", "");
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }


        protected void BindCursos()
        {
            using (var reposytory = new Repository<Curso>(new Context<Curso>()))
            {
                var list = new List<Curso>();
                list.AddRange(reposytory.All().OrderBy(p => p.CurDescricao));
                DDcursoDiario.DataSource = list.ToList();
                DDcursoDiario.DataBind();
            }
        }

        protected void BindTurmas(string curso, DropDownList dropdown)
        {
            using (var reposytory = new Repository<Turma>(new Context<Turma>()))
            {
                var list = new List<Turma>();
                list.AddRange(reposytory.All().Where(x => x.TurCurso.Equals(curso)).OrderBy(p => p.TurNome));
                dropdown.Items.Clear();
                dropdown.DataSource = list.ToList();
                dropdown.DataBind();
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }


        protected void DDcursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;
            if (drop.SelectedValue.Equals(string.Empty)) return;
            BindTurmas(drop.SelectedValue, DDturma_pesquisa);
        }


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            string sql;

            //Apenas mês selecionado
            if (DDcursoDiario.SelectedValue.Equals(string.Empty) && !DDmeses.SelectedValue.Equals(string.Empty))
            {
                sql = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                    "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE  ALAStatus = 'A' AND DATEPART(MONTH,Apr_DataDeNascimento) =  " +
                    int.Parse(DDmeses.SelectedValue) + " " + "ORDER BY DATEPART(MONTH,Apr_DataDeNascimento) , DATEPART(Day,Apr_DataDeNascimento) ";
                Session["id"] = 32;
            }

            //Apenas curso selecionado
            else if (!DDcursoDiario.SelectedValue.Equals(string.Empty) && DDmeses.SelectedValue.Equals(string.Empty) && DDturma_pesquisa.SelectedValue.Equals(string.Empty))
            {
                sql = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                    "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE ALAStatus = 'A' AND  TurCurso = '" + DDcursoDiario.SelectedValue + "'  ORDER BY DATEPART(MONTH,Apr_DataDeNascimento) , DATEPART(Day,Apr_DataDeNascimento) ";
                Session["id"] = 33;
            }

           //curso e mês selecionado
            else if (!DDcursoDiario.SelectedValue.Equals(string.Empty) && !DDmeses.SelectedValue.Equals(string.Empty) && DDturma_pesquisa.SelectedValue.Equals(string.Empty))
            {
                sql = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                   "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE  ALAStatus = 'A' AND DATEPART(MONTH,Apr_DataDeNascimento) =  " +
                   int.Parse(DDmeses.SelectedValue) + "  AND   TurCurso = '" + DDcursoDiario.SelectedValue + "' ORDER BY DATEPART(MONTH,Apr_DataDeNascimento), DATEPART(Day,Apr_DataDeNascimento) ";
                Session["id"] = 34;
            }
            //curso e turma selecionado
            else if (!DDcursoDiario.SelectedValue.Equals(string.Empty) && DDmeses.SelectedValue.Equals(string.Empty) && !DDturma_pesquisa.SelectedValue.Equals(string.Empty))
            {
                sql = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                   "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE  ALAStatus = 'A' AND  "+
                    "TurCodigo = " + DDturma_pesquisa.SelectedValue + " ORDER BY DATEPART(MONTH,Apr_DataDeNascimento), DATEPART(Day,Apr_DataDeNascimento) ";
                Session["id"] = 35;
            }
            //curso, mês e turma selecionado
            else
            {
                sql = "SELECT Apr_Codigo, Apr_Nome, Apr_DataDeNascimento, TurNome, CurDescricao FROM  dbo.CA_AlocacaoAprendiz INNER JOIN  dbo.CA_Aprendiz ON Apr_Codigo = ALAAprendiz " +
                  "inner join  CA_Turmas on  TurCodigo = ALATurma inner join  CA_Cursos on  TurCurso = CurCodigo WHERE  ALAStatus = 'A' AND DATEPART(MONTH,Apr_DataDeNascimento) =  " +
                  int.Parse(DDmeses.SelectedValue) + "  AND   TurCodigo = " + DDturma_pesquisa.SelectedValue + " ORDER BY  DATEPART(MONTH,Apr_DataDeNascimento), DATEPART(Day,Apr_DataDeNascimento)";
                Session["id"] = 36;
            }

            var dSescola = new SqlDataSource
            {
                ID = "ODSalunos",
                ConnectionString = GetConfig.Config(),
                SelectCommand = sql,
                EnableViewState = true
            };
            GridView1.DataSource = dSescola;
            ViewState["list"] = sql;
            
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var dados = ViewState["list"].ToString();
            var dSescola = new SqlDataSource
            {
                ID = "ODSalunos",
                ConnectionString = GetConfig.Config(),
                SelectCommand = dados,
                EnableViewState = true
            };
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = dSescola;
            GridView1.DataBind();

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Session["PRMT_Curso"] = DDcursoDiario.SelectedValue;
            Session["PRMT_Turma"] = DDturma_pesquisa.SelectedValue;
            Session["MesRef"] = DDmeses.SelectedValue.Equals(string.Empty) ? "0" : DDmeses.SelectedValue;
         
            MultiView1.ActiveViewIndex = 1;
        }

        protected void texto_Click(object sender, EventArgs e)
        {
            var filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            var cn = new Conexao();
            var sql =  ViewState["list"].ToString();
            var dr = cn.Consultar(sql);
            try
            {
                while (dr.Read())
                {
                    var linha =   string.Format("{0:dd/MM}",dr["Apr_DataDeNascimento"])  + "; " + dr["Apr_Codigo"]  + "; " + dr["Apr_Nome"]
                        + "; " + dr["TurNome"] + "; " + dr["CurDescricao"];
                    write.Escreve(linha);
                }
                // download do arquivo de texto
                string fileName = filePath + @"/temp.txt";
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=Lista de Aniversariantes.txt");
                Response.WriteFile(fileName);
                Response.Flush();
                Response.Close();
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
            }
        }
    }
}