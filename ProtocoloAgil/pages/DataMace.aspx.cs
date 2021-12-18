using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;
using System.IO;
using ProtocoloAgil.Base.Models;
using System.Web;

namespace ProtocoloAgil.pages
{
    public partial class DataMace : Page
    {


        [Serializable]
        public struct Arquivos
        {
            
            public string Nome_Arquivo { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PainelArquivo.Visible = false;
            GetFiles();

            Session["CurrentPage"] = "configuracoes";

            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterPostBackControl(gridArquivo);
            }

            if (!IsPostBack)
            {
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }
        }


        private void GetFiles()
        {
          
              

                var filePath = Server.MapPath(@"/files/DataMace/");
               
                var dir = new DirectoryInfo(filePath);
                if (dir.Exists)
                {
                    ViewState.Add("Caminho", filePath);
                    var files = dir.GetFiles();
                    files.ToList();
                   // var lista = files.Where(i => i.Name.Split('_')[0].Equals(protocolo)).ToList();
                    //var lista = req.DocDirEspecial.Equals("S") ? files.Where(i => i.Name.Equals(protocolo.ToString())).ToList() : files.Where(i => i.Name.Split('_')[0].Equals(protocolo)).ToList();
                    var datasrc = files.Select(fileInfo => new Arquivos {Nome_Arquivo = fileInfo.Name }).ToList();
                    gridArquivo.DataSource = datasrc;
                    gridArquivo.DataBind();
                }
            
        }

        public void baixarArquivo(FileInfo arquivo) {
            

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + arquivo.Name + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", arquivo.Length.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.WriteFile(arquivo.FullName);
            arquivo = null;
            
        }
        protected void gridArquivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = gridArquivo.SelectedRow;
            var name = HttpUtility.HtmlDecode(row.Cells[0].Text);

            var fInfo = new FileInfo(ViewState["Caminho"] + name);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fInfo.Name + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", fInfo.Length.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.WriteFile(fInfo.FullName);
            fInfo = null;

        }
       

      
        protected void btnGerar_Click(object sender, EventArgs e)
        {
            GerarArquivo();
        }


        public void GerarArquivo()
        {
            try{

                if (txtDataInicioPesquisa.Text.Equals(string.Empty))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                     "alert('Data início é obrigatória')", true);
                    return;
                }
                if (txtDataFinalPesquisa.Text.Equals(string.Empty))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                  "alert('Data término é obrigatório')", true);
                    return;
                }

                //Cria variavel com uma nova instância com informações de tal arquivo
               


                CriaPasta();
                GetFiles();
            }
            catch(Exception e){

            }
        }


        private void CriaPasta()
        {


            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var filePath = Server.MapPath(@"/files/DataMace");
                var dir = new DirectoryInfo(filePath);
                


                if (dir.Exists)
                {
                    BindUsuarios();
                }
                else
                {
                    dir.Create();
                    BindUsuarios();
                }
                
            }
        }

        protected void BindUsuarios()
        {
            // teste
            var filePath = Server.MapPath(@"/files/DataMace/DataMace_"+ ValidaDataHora(DateTime.Now)+".txt");
            //string path = @"D:\__PROJETOS_ATUALIZADOS\Git\aprendiz_Nurap\ProtocoloAgil\files\DataMace\teste.txt";


            if (!File.Exists(filePath))
            {
                var arq =File.CreateText(filePath);
                arq.Close();
               
            }

            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                //var selected = from u in bd.CA_Aprendizs
                //               join a in bd.CA_AlocacaoAprendizs on u.Apr_Codigo equals a.ALAAprendiz
                //               join t in bd.CA_Turmas on a.ALATurma equals t.TurCodigo
                //               where u.Apr_DataContrato > Convert.ToDateTime(txtDataInicioPesquisa.Text) && u.Apr_DataContrato < Convert.ToDateTime(txtDataFinalPesquisa.Text)

                var selected = from u in bd.View_DataMaces
                             //  where u.Apr_DataContrato > Convert.ToDateTime(txtDataInicioPesquisa.Text) && u.Apr_DataContrato < Convert.ToDateTime(txtDataFinalPesquisa.Text)
                                

                             // where u.Apr_Codigo == 4
                               select u;

                IQueryable<View_DataMace> lista;
                lista = selected;
                int quantidade = 0;
                int i = 0;
               // string[] valores;
                string[] valores = new string[lista.Count()];
                foreach (View_DataMace aprendiz in lista)
                {



                    

                    ////Tipo de Registro
                    //valores += AdicionaEspaco("", 0, 1);
                    ////Tipo de Movimento
                    //valores += AdicionaEspaco("", 0, 1);
                    ////Número do cargo
                    //valores += AdicionaZero("", 0, 5);
                    ////Cargo
                    //valores += AdicionaEspaco("", 0, 20);
                    ////CBO
                    //valores += AdicionaZero("", 0, 6);





                    ////Nível
                    //valores += AdicionaEspaco("", 0, 1);
                    ////Grupo Salarial
                    //valores += AdicionaZero("", 0, 3);
                    ////Faixa Salarial
                    //valores += AdicionaZero("", 0, 1);
                    ////Pontos
                    //valores += AdicionaZero("", 0, 7);
                    ////Centro de Custo
                    //valores += AdicionaEspaco("", 0, 12);







                    ////Diretoria
                    //valores += AdicionaZero("", 0, 2);
                    ////Depto
                    //valores += AdicionaZero("", 0, 5);
                    ////Setor
                    //valores += AdicionaZero("", 0, 5);
                    ////Seção
                    //valores += AdicionaZero("", 0, 5);
                    ////Grupo de Cargo
                    //valores += AdicionaEspaco("", 0, 15);




                    ////MDO
                    //valores += AdicionaEspaco("", 0, 1);
                    ////Instrução
                    //valores += AdicionaEspaco("", 0, 1);








                    ////Tipo de Registro
                    //valores += AdicionaEspaco("", 0, 1);
                    ////Tipo de Movimento
                    //valores += AdicionaEspaco("", 0, 1);
                    ////Transf. de Empresa
                    //valores += AdicionaEspaco("", 0, 3);
                    ////Transf. de Registro
                    //valores += AdicionaZero("", 0, 6);
                    ////Para Empresa
                    //valores += AdicionaEspaco("", 0, 3);









                    ////Para Registro
                    //valores += AdicionaZero("", 0, 6);
                    ////Data Transferencia
                    //valores += AdicionaZero("", 0, 8);


                    //  #################### COMEÇANDO ####################//

                    valores[i] += "F";
                    valores[i] += "I";
                    valores[i] += "001";

                           //Número Registro do Trabalhador 
                    valores[i] += AdicionaZero(aprendiz.Apr_Codigo.ToString(), aprendiz.Apr_Codigo.ToString().Length, 6);

                     

                    //Digito Controle
                    valores[i] += AdicionaZero("", 0, 1);
                    //Número Diretoria
                    valores[i] += AdicionaZero("", 0, 2);
                    //Número Depto.
                    valores[i] += AdicionaZero(aprendiz.ALAUnidadeParceiro.ToString() ?? "", aprendiz.ALAUnidadeParceiro.ToString().Length, 5);
                    //Número setor
                    valores[i] += AdicionaZero("", 0, 5);

                        //Número Seção
                    valores[i] += AdicionaZero("", 0, 5);
                        //Nome Trabalhador
                    valores[i] += AdicionaEspaco(aprendiz.Apr_Nome ?? "", aprendiz.Apr_Nome.Length, 30);
                        //Apelido Trab.
                    valores[i] += aprendiz.Apr_Nome.Substring(0, 10);
                        //Endereço Trab.
                    valores[i] += AdicionaEspaco(aprendiz.Apr_Endereço, aprendiz.Apr_Endereço.Length, 30);

                    //====================== Página 2 ========================//



                        
                        //Complemento
                    valores[i] += AdicionaEspaco(aprendiz.Apr_Complemento ?? "", aprendiz.Apr_Complemento == null ? 0 : aprendiz.Apr_Complemento.Length, 20);
                        //Bairro
                    valores[i] += AdicionaEspaco(aprendiz.Apr_Bairro ?? "", aprendiz.Apr_Bairro.Length, 20);
                        //Cidade
                    valores[i] += AdicionaEspaco(aprendiz.Apr_Cidade ?? "", aprendiz.Apr_Cidade == null ? 0 : aprendiz.Apr_Cidade.Length, 20);
                        //Estado
                    valores[i] += AdicionaEspaco(aprendiz.Apr_UF ?? "", aprendiz.Apr_UF == null ? 0 : aprendiz.Apr_UF.Length, 2);
                        //CEP
                    valores[i] += AdicionaZero(aprendiz.Apr_CEP ?? "", aprendiz.Apr_CEP == null ? 0 : aprendiz.Apr_CEP.Length, 8);


                        //Custo gerencial
                    valores[i] += AdicionaEspaco("", 0, 12);
                        //Custo Contábil
                    valores[i] += AdicionaEspaco("", 0, 12);
                        //Locação
                    valores[i] += AdicionaEspaco("", 0, 5);
                        //Turno
                    valores[i] += AdicionaEspaco("", 0, 1);
                        //MDO
                    valores[i] += "D";
                        //Telefone
                    valores[i] += AdicionaEspaco(aprendiz.Apr_Telefone ?? "", aprendiz.Apr_Telefone == null ? 0 : aprendiz.Apr_Telefone.Length, 18);
                        //Data de Nascimento 
                    valores[i] += AdicionaZero(ValidaDataSemBarra(Convert.ToDateTime(aprendiz.Apr_DataDeNascimento)), ValidaDataSemBarra(Convert.ToDateTime(aprendiz.Apr_DataDeNascimento)).Length, 8);
                        //Sexo
                    valores[i] += AdicionaEspaco(aprendiz.Apr_Sexo ?? "", aprendiz.Apr_Sexo == null ? 0 : aprendiz.Apr_Sexo.Length, 1);
                        //Estado Civil
                    valores[i] += AdicionaEspaco(aprendiz.AprEstadoCivil ?? "", aprendiz.Apr_UF == null ? 0 : aprendiz.Apr_UF.Length, 1);


                        //Naturalidade
                    valores[i] += AdicionaEspaco(aprendiz.Apr_Naturalidade ?? "", aprendiz.Apr_Naturalidade == null ? 0 : aprendiz.Apr_Naturalidade.Length, 20);
                        //Naturalidade Estado
                    valores[i] += AdicionaEspaco(aprendiz.Apr_UF_Ident ?? "", aprendiz.Apr_UF_Ident == null ? 0 : aprendiz.Apr_UF_Ident.Length, 2);
                        //Cart. Prof. Número
                    valores[i] += AdicionaZero(aprendiz.Apr_CarteiraDeTrabalho ?? "", aprendiz.Apr_CarteiraDeTrabalho == null ? 0 : aprendiz.Apr_CarteiraDeTrabalho.Length, 7);
                        //Cart. Prof. Série
                    valores[i] += AdicionaZero(aprendiz.Apr_Serie_Cartrab ?? "", aprendiz.Apr_Serie_Cartrab == null ? 0 : aprendiz.Apr_Serie_Cartrab.Length, 5);
                        //Cart. Prof. UF
                    valores[i] += AdicionaEspaco(aprendiz.Apr_UF_CartTrab ?? "", aprendiz.Apr_UF_CartTrab == null ? 0 : aprendiz.Apr_UF_CartTrab.Length, 2);
                        //Cart. Prof. Data
                    valores[i] += AdicionaZero(ValidaDataSemBarra(Convert.ToDateTime(aprendiz.Apr_DataEmissão_CartTrab)), ValidaDataSemBarra(Convert.ToDateTime(aprendiz.Apr_DataEmissão_CartTrab)).Length, 8);
                        //CT - NIT
                    valores[i] += "N";
                        //CPF
                    valores[i] += AdicionaZero(aprendiz.Apr_CPF ?? "", aprendiz.Apr_CPF == null ? 0 : aprendiz.Apr_CPF.Length, 11);
                        //PIS/Pasep
                    valores[i] += AdicionaZero(aprendiz.Apr_PIS ?? "", aprendiz.Apr_PIS == null ? 0 : aprendiz.Apr_PIS.Length, 11);
                        //Pis Data
                    valores[i] += AdicionaZero("", 0, 8);
                        //RG numero
                    valores[i] += AdicionaEspaco(aprendiz.Apr_CarteiraDeIdentidade ?? "", aprendiz.Apr_CarteiraDeIdentidade == null ? 0 : aprendiz.Apr_CarteiraDeIdentidade.Length, 15);
                        //RG órgão emissor
                    valores[i] += AdicionaEspaco(aprendiz.Apr_OrgaoEmissor_Ident ?? "", aprendiz.Apr_OrgaoEmissor_Ident == null ? 0 : aprendiz.Apr_OrgaoEmissor_Ident.Length, 5);
                        //RG UF
                    valores[i] += AdicionaEspaco(aprendiz.Apr_UF_Ident ?? "", aprendiz.Apr_UF_Ident == null ? 0 : aprendiz.Apr_UF_Ident.Length, 2);
                        //RG Data
                    valores[i] += AdicionaEspaco(ValidaDataSemBarra(Convert.ToDateTime(aprendiz.Apr_DataEmissão_Ident)), ValidaDataSemBarra(Convert.ToDateTime(aprendiz.Apr_DataEmissão_Ident)).Length, 8);
                        //Reservista n°
                    valores[i] += AdicionaEspaco(aprendiz.Apr_CertReservista ?? "", aprendiz.Apr_CertReservista == null ? 0 : aprendiz.Apr_CertReservista.Length, 12);
                        //Reservista série
                    valores[i] += AdicionaEspaco("", 0, 5);
                        //Reservista categoria
                    valores[i] += AdicionaEspaco("", 0, 15);
                        //Habil. Profissional
                    valores[i] += AdicionaEspaco("", 0, 20);
                        //Nome Cons.Regional
                    valores[i] += AdicionaEspaco("", 0, 30);
                        //Sigla Cons.Regional
                    valores[i] += AdicionaEspaco("", 0, 5);
                        //Número do Cons.Reg.
                    valores[i] += AdicionaEspaco("", 0, 15);
                        //Região do Cons.Reg.
                    valores[i] += AdicionaEspaco("", 0, 5);
                        //Cons.Reg.Dt Venc.
                    valores[i] += AdicionaZero("", 0, 8);
                        //Número Tit.Eleitor
                    valores[i] += AdicionaZero("", 0, 12);
                        //Zona Eleitoral
                    valores[i] += AdicionaZero("", 0, 3);
                        //Seção Eleitoral
                    valores[i] += AdicionaZero("", 0, 4);
                        //Data Emissão
                    valores[i] += AdicionaZero("", 0, 8);
                        //Nacionalidade
                    valores[i] += "10";
                        //Número Reg.Estrang
                    valores[i] += AdicionaEspaco("", 0, 12);
                        //Data de Chegada
                    valores[i] += AdicionaZero("", 0, 8);
                        //Data Validade RNE
                    valores[i] += AdicionaZero("", 0, 8);
                        //Data Cart.Prof.
                    valores[i] += AdicionaZero("", 0, 8);
                        //Tipo de Visto
                    valores[i] += AdicionaZero("", 0, 1);
                        //Número CNH
                    valores[i] += AdicionaZero("", 0, 11);
                        //Vencto CNH
                    valores[i] += AdicionaZero("", 0, 8);
                        //Categoria CNH
                    valores[i] += AdicionaEspaco("", 0, 4);
                        //Número do CI
                    valores[i] += AdicionaZero("", 0, 11);
                        //Classe do CI
                    valores[i] += AdicionaEspaco("", 0, 2);
                        //Data Exame Médico
                    valores[i] += AdicionaZero("", 0, 8);
                        //Vínculo Empregat.
                    valores[i] += AdicionaZero("", 0, 2);

                    // ================== PÁGINA 3 ========================//

                        //Horas Base
                    valores[i] += AdicionaZero("", 0, 5);     ///valores += AdicionaZero("", 0, 3);
                        //Horas Semanais
                    valores[i] += AdicionaZero("", 0, 3);      ////valores += AdicionaZero("", 0, 2);
                        //Tabela Horas Padrão
                    valores[i] += AdicionaZero("", 0, 3);
                        //Código Horário
                    valores[i] += AdicionaZero("", 0, 5);
                        //Data de Admissão
                    valores[i] += AdicionaZero("", 0, 8);
                        //Tipo de Admissão
                    valores[i] += AdicionaEspaco("", 0, 1);



                        //Banco Pagador
                    valores[i] += AdicionaZero("", 0, 5);
                        //Banco Agência
                    valores[i] += AdicionaEspaco("", 0, 5);
                        //Conta Correnta.
                    valores[i] += AdicionaEspaco("", 0, 12);
                        //CBO.
                    valores[i] += AdicionaZero(aprendiz.Apr_CBO ?? "", aprendiz.Apr_CBO == null ? 0 : aprendiz.Apr_CBO.Length, 6);
                        //Número da Chapa.
                    valores[i] += AdicionaZero("", 0, 6);
                        //Número da Chapeira.
                    valores[i] += AdicionaZero("", 0, 2);
                        //Opção do IRRF.
                    valores[i] += AdicionaEspaco("S", 0, 1);




                        //Opção de Previdência
                    valores[i] += AdicionaEspaco("S", 0, 1);
                        //Aposentado
                    valores[i] += AdicionaEspaco("N", 0, 1);
                        //Opção RAIS
                    valores[i] += AdicionaEspaco("S", 0, 1);
                        //Opção do DRT
                    valores[i] += AdicionaZero("", 0, 6);
                        //Tipo de Trabalhador
                    valores[i] += AdicionaEspaco("N", 0, 1);
                        //Grau de instrução
                    valores[i] += AdicionaZero("", 0, 1);





                        //Grupo do Cargo
                    valores[i] += AdicionaEspaco("", 0, 15);
                        //Código do cargo
                    valores[i] += AdicionaZero("", 0, 5);
                        //Nome do cargo
                    valores[i] += AdicionaEspaco("", 0, 20);
                        //Nível
                    valores[i] += AdicionaEspaco("", 0, 1);
                        //DataCargo
                    valores[i] += AdicionaZero("", 0, 6);
                        //Grau salarial
                    valores[i] += AdicionaEspaco("", 0, 2);
                        //Tipo de Salário.
                    valores[i] += AdicionaZero("1", 0, 1);






                        //Grupo salarial
                        valores[i] += AdicionaZero("", 0, 3);
                        //Faixa salarial
                        valores[i] += AdicionaZero("", 0, 1);
                        //Salário mês Atual
                        valores[i] += AdicionaZero("", 0, 11);   // valores += AdicionaZero("", 0, 9);
                        //Opção Adiantamento
                        valores[i] += AdicionaEspaco("", 0, 1);
                        //Porc. Adiantamento
                        valores[i] += AdicionaZero("", 0, 4);       ///valores += AdicionaZero("", 0, 2);
                        //Data de opção
                        valores[i] += AdicionaZero("", 0, 8);
                        //Banco de FGTS
                        valores[i] += AdicionaZero("", 0, 5);
                        //Saldo FGTS
                        valores[i] += AdicionaZero("", 0, 13);       ///valores += AdicionaZero("", 0, 11);
                        //Conta FGTS
                        valores[i] += AdicionaZero("", 0, 11);
                        //Opção FGTS
                        valores[i] += AdicionaEspaco("", 0, 1);
                        //FGTS Taxa
                        valores[i] += AdicionaZero("", 0, 1);
                        //N° do Sindicato
                        valores[i] += AdicionaZero("", 0, 5);
                        //Imposto Sindical.
                        valores[i] += AdicionaEspaco("", 0, 1);
                        //Opção de Sócio.
                        valores[i] += AdicionaEspaco("N", 0, 1);



                    // ================== PÁGINA 4 ========================//



                        //Matricula sindical
                        valores[i] += AdicionaZero("", 0, 7);
                        //Transferência
                        valores[i] += AdicionaZero("", 0, 1);
                        //Tabela seguro de vida
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tabela ass.medica 1
                        valores[i] += AdicionaZero("", 0, 3);
                        //Qtde. Assist. Med. 1
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tabela Assist. Med. 2
                        valores[i] += AdicionaZero("", 0, 3);
                        //Qtde. Assist. Med. 2
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tabela de refeição
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tabela de cesta Bas.
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tabela de anuidade
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 1
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 2
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 3
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 4
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 5
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 6
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 7
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 8
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 9
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tab. usuário 10
                        valores[i] += AdicionaZero("", 0, 3);
                        //Porcentual Pericul.
                        valores[i] += AdicionaZero("", 0, 5);      ///valores += AdicionaZero("", 0, 3);


                    // 


                        //Cód Condução 1.
                        valores[i] += AdicionaZero("", 0, 3);
                        //Quant. Diária 1
                        valores[i] += AdicionaZero("", 0, 1);
                        //Cod. Condução 2
                        valores[i] += AdicionaZero("", 0, 3);
                        //Quant. Diária 2
                        valores[i] += AdicionaZero("", 0, 1);
                        //Cod. condução 3
                        valores[i] += AdicionaZero("", 0, 3);


                        //Quant. Diária 3
                        valores[i] += AdicionaZero("", 0, 1);
                        //Cod. Condução 4
                        valores[i] += AdicionaZero("", 0, 3);
                        //Quant. Diária 4.
                        valores[i] += AdicionaZero("", 0, 1);
                        //Cód. Condução 5.
                        valores[i] += AdicionaZero("", 0, 3);
                        //Quant. Diária 5.
                        valores[i] += AdicionaZero("", 0, 1);


                        //Cod Condução 6
                        valores[i] += AdicionaZero("", 0, 3);
                        //Quant. Diária 6
                        valores[i] += AdicionaZero("", 0, 1);
                        //Dias Trabalhados VT
                        valores[i] += AdicionaZero("", 0, 3);
                        //Calendário VT
                        valores[i] += AdicionaZero("", 0, 3);
                        //Isenção
                        valores[i] += AdicionaEspaco("", 0, 1);
                        //Perc. Limite Desc.
                        valores[i] += AdicionaZero("", 0, 3);     // campo com decimais.... lembrar



                        //Valor Limite Desc.
                        valores[i] += AdicionaZero("", 0, 11);      ///valores += AdicionaZero("", 0, 9);    // campo com decimais.... lembrar
                        //Unidade Entrega VT.
                        valores[i] += AdicionaZero("", 0, 3);
                        //Tabela GFIP.
                        valores[i] += AdicionaZero("", 0, 3);
                        //Raça cor
                        valores[i] += AdicionaZero("", 0, 1);
                        //Código Rescisão
                        valores[i] += AdicionaZero("", 0, 2);



                        //Data Rescisão
                        valores[i] += AdicionaZero("", 0, 8);
                        //Cod. Afast
                        valores[i] += AdicionaZero("", 0, 1);
                        //Data Afast
                        valores[i] += AdicionaZero("", 0, 8);
                        //Data Retorno
                        valores[i] += AdicionaZero("", 0, 8);

                        //using (StreamWriter sw = File.CreateText(path))
                        //{
                        //    sw.WriteLine(valores);
                            
                        //}


                        
                        //File.WriteAllText(filePath, valores);

                        i++;
                  
                }

                if (valores.Count() == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                  "alert('Não foi encontrado registro para a geração do arquivo.')", true);
                }
                else
                {
                    var arquivo = new FileInfo(filePath);
                    //File.WriteAllLines(@"D:\__PROJETOS_ATUALIZADOS\Git\aprendiz_Nurap\ProtocoloAgil\files\DataMace\teste.txt", valores);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                  "alert('Arquivo "+arquivo.Name+" gerado com sucesso.')", true);
                    

                    
                    if (IsFileLocked(arquivo) == false)
                    {
                        File.WriteAllLines(filePath, valores);
                        IsFileLocked(arquivo);
                      //  baixarArquivo(arquivo);
                    }
                    
                }
                
                

            }

        }



        public virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }



        public string AdicionaZero(string campo , int quantidadeCaracteres, int quantidadeExigidaNoCampo)
        {
            if (quantidadeCaracteres > quantidadeExigidaNoCampo)
            {
                campo = campo.Substring(0, quantidadeExigidaNoCampo);
                return campo;
            }
            else
            {
                if (campo.ToString().Length.ToString().Equals(quantidadeExigidaNoCampo.ToString()))
                {
                    return campo;
                }
                else
                {
                    campo = campo.PadLeft((quantidadeExigidaNoCampo - quantidadeCaracteres) + campo.ToString().Length, '0');
                    return campo;
                }
            }
            
        }

        public string AdicionaEspaco(string campo, int quantidadeCaracteres, int quantidadeExigidaNoCampo)
        {
            if (quantidadeCaracteres > quantidadeExigidaNoCampo)
            {
                campo = campo.Substring(0, quantidadeExigidaNoCampo);
                return campo;
            }
            else
            {
                if (campo.ToString().Length.ToString().Equals(quantidadeExigidaNoCampo.ToString()))
                {
                    return campo;
                }
                else
                {
                    campo = campo.PadRight((quantidadeExigidaNoCampo - quantidadeCaracteres) + campo.ToString().Length, ' ');
                    return campo;
                }
            }
        }


        private string ValidaDataSemBarra(DateTime data)
        {
            var datastring = string.Format("{0:dd/MM/yyy}", data);
            if (datastring.Equals("01/01/1900")) return "";
            return datastring.Replace("/","");
        }

        private string ValidaDataComBarra(DateTime data)
        {
            var datastring = string.Format("{0:dd/MM/yyy}", data);
            if (datastring.Equals("01/01/1900")) return "";
            return datastring;
        }

        private string ValidaDataHora(DateTime data)
        {
            var datastring = string.Format("{0:ddMMyy_Hmm}", data);
            if (datastring.Equals("01/01/1900")) return "";
            return datastring;
        }
        
    }
}