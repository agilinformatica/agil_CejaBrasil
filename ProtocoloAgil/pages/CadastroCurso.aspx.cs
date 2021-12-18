using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class CadastroCurso : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if ( Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
                BTinsert.Enabled = false;
            }

            if (IsPostBack) return;
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void BindGridView(int tipo)
        {
            using (var repository = new Repository<Curso>(new Context<Curso>()))
            {
                var datasource = new List<Curso>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p=>p.CurDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.CurDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.CurDescricao)); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }

        private void PreencheCampos()
        {
            using (var repository = new Repository<Curso>(new Context<Curso>()))
            {
                var curso = repository.Find(Session["Alteracodigo"].ToString());
                TBCodigo_curso.Text = curso.CurCodigo;
                DD_frequencia_aula.SelectedValue = curso.EnsNumeroPeriodos.ToString();
                TBNome.Text = curso.CurDescricao;
                TB_Abreviatura.Text = curso.CurAbreviatura;
                TB_carga_horaria.Text = curso.CurCargaHoraria.ToString();
            }          
        }


        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBCodigo_curso.Text.Equals(string.Empty)) throw new ArgumentException("Informe o código do curso.");
                if (TB_Abreviatura.Text.Equals(string.Empty)) throw new ArgumentException("Informe a abreviatura do curso.");
                if (TB_carga_horaria.Text.Equals(string.Empty)) throw new ArgumentException("Informe a carga horária do curso.");
                if (DD_frequencia_aula.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe a frequencia das aulas do curso.");
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome do curso.");

                using (var repository = new Repository<Curso>(new Context<Curso>()))
                {
                    var curso = (Session["comando"].Equals("Inserir")) ? new Curso() : repository.Find(Session["Alteracodigo"].ToString());
                    curso.CurCodigo = TBCodigo_curso.Text;
                    curso.CurDescricao = TBNome.Text;
                    curso.CurCargaHoraria = Convert.ToInt32(TB_carga_horaria.Text);
                    curso.CurAbreviatura = TB_Abreviatura.Text;
                    curso.EnsNumeroPeriodos = Convert.ToByte(DD_frequencia_aula.SelectedValue);
                    if (Session["comando"].Equals("Inserir")) repository.Add(curso);
                    else repository.Edit(curso);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Ação realizada com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('"+ ex.Message+"')", true);
                return;
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000115", ex);
            }
        }


        protected void BTlimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        private void LimpaCampos()
        {
            TBCodigo_curso.Text = string.Empty;
            TBNome.Text = string.Empty;
            TB_carga_horaria.Text = string.Empty;
            TB_Abreviatura.Text = string.Empty;
            DD_frequencia_aula.SelectedValue = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            LBtituloAlt.Text = "Cadastro de Área de Ensino";
            BTinsert.Text = "Salvar";
            Session["comando"] = "Inserir";
            LimpaCampos();
            TBCodigo_curso.Enabled = true;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "5";
            MultiView1.ActiveViewIndex = 2;
        }

        protected void texto_Click(object sender, EventArgs e)
        {
            var filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            var cn = new Conexao();
            var dr = cn.Consultar("SELECT * FROM MA_AreaEnsino ORDER BY EnsDescricao");
            try
            {
                while (dr.Read())
                {
                    var linha = dr["EnsCodigo"] + " " + dr["EnsDescricao"];
                    write.Escreve(linha);
                }
                // download do arquivo de texto
                string fileName = filePath + @"/temp.txt";
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=Lista_de_Profissoes.txt");
                Response.WriteFile(fileName);
                Response.Flush();
                Response.Close();
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
           BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var curso = button.CommandArgument;
            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());

            if(bd.CA_Turmas.Where(p =>p.TurCurso == curso).Count() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                          "alert('ERRO - O curso está associado à uma turma. impossível excluir.')", true);
                return;
            }

            using (var repository = new Repository<Curso>(new Context<Curso>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(curso);
            }
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }
    }
}