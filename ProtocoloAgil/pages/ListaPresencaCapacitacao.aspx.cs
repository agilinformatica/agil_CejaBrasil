using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class ListaPresencaCapacitacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["id"] = 75;
          
            Session["CurrentPage"] = "pedagogico";            
            var scriptManager = ScriptManager.GetCurrent(Page);      
            if (!IsPostBack)
            {              
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                preencheDropDown();           
            }
          
        }

        void preencheDropDown()
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = (from Au in db.CA_AulasCapacitacaoAprendizs
                            join A in db.CA_Aprendizs on Au.AcpAprendiz equals A.Apr_Codigo
                            join T in db.CA_Turmas on Au.AcpTurma equals T.TurCodigo
                            //join D in db.CA_Disciplinas on Au.AdiDisciplina equals D.DisCodigo
                            //join E in db.CA_Educadores on Au.AdiEducador equals E.EducCodigo
                            where T.TurCurso.Equals("003")
                            orderby T.TurNome ascending
                            select new { T.TurCodigo, T.TurNome}).Distinct().OrderBy(t=>t.TurNome);

                DD_TURMA.DataSource = query;
                DD_TURMA.DataBind();
                DD_TURMA.Items.Insert(0, "Selecione..");
                DD_TURMA.SelectedIndex = 0;
            }
        }

        protected void btnGerarRel_Click(object sender, EventArgs e)
        {
            if (ValidaDados().Equals(true))
            {
                Session["id"] = 96; //Relatorio Lista de Presença
                ///Session["DataRel"] = Convert.ToDateTime(DD_Data.SelectedValue).ToShortDateString();
                Session["DataRel"] = DD_Data.SelectedValue;
                Session["TurCodigo"] = DD_TURMA.SelectedValue;
                Panel2.Visible = true;



                var sql = "Select TurDiaSemana from CA_Turmas where TurCodigo = " + DD_TURMA.SelectedValue + "";
                var con = new Conexao();
                var result = con.Consultar(sql);
                string diaSemana = "";
         
                while (result.Read())
                {

                    diaSemana = result["TurDiaSemana"].ToString();
                }
              
                switch (diaSemana)
                {
                    case "1":
                        diaSemana = "Domingo";
                        break;
                    case "2":
                        diaSemana = "Segunda-Feira";
                        break;
                    case "3":
                        diaSemana = "Terça-Feira";
                        break;
                    case "4":
                        diaSemana = "Quarta-Feira";
                        break;
                    case "5":
                        diaSemana = "Quinta-Feira";
                        break;
                    case "6":
                        diaSemana = "Sexta-Feira";
                        break;
                    case "7":
                        diaSemana = "Sábado";
                        break;
                }

              
                Session["DiaSemana"] = diaSemana;





                var sql2 = "Select UniNome, UniEndereco + ' N°' + UniNumeroEndereco +  ','+ ' Bairro: ' + UniBairro + ', ' + UniCidade + ' - ' + UniEstado as endereco, UniCEP, UniTelefone from CA_Unidades U join CA_Turmas T on U.UniCodigo = T.TurUnidade where T.TurCodigo = " + DD_TURMA.SelectedValue + "";
                var con2 = new Conexao();
                var result2 = con2.Consultar(sql2);
                while (result2.Read())
                {
                    Session["Polo"] = result2["UniNome"];
                    Session["Endereco"] = result2["endereco"];
                    Session["Cep"] = result2["UniCep"];
                    Session["Telefone"] = result2["UniTelefone"];
                }

            }
        }

        protected void DD_TURMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from Au in db.CA_AulasCapacitacaoAprendizs                           
                             where Au.AcpTurma.Equals(DD_TURMA.SelectedValue)
                             && Au.AcpDataAula > DateTime.Now.AddDays(-30)
                             && Au.AcpDataAula < DateTime.Now.AddDays(30)

                             select new { Au.AcpDataAula }).Distinct().OrderBy(item => item.AcpDataAula);
                DD_Data.DataSource = query;
                DD_Data.DataBind();
                DD_Data.Items.Insert(0, "Selecione..");
                DD_Data.SelectedIndex = 0;
            }

        }

        protected bool ValidaDados()
        {
            if (DD_TURMA.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                      "alert('Selecione a turma.')", true);
                return false;
            }
            else if (DD_Data.SelectedValue.Equals("Selecione.."))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                      "alert('Selecione a data.')", true);
                return false;
            }
            else
                return true;
        }




    }
}