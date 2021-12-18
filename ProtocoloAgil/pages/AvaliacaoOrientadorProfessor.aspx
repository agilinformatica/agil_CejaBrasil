<%@ Page Title="" Language="C#" MasterPageFile="~/MaEducador.Master" AutoEventWireup="true"
    CodeBehind="AvaliacaoOrientadorProfessor.aspx.cs" Inherits="ProtocoloAgil.pages.AvaliacaoOrientadorProfessor" %>
<%@ Import Namespace="ProtocoloAgil.Base" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
      
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <script src="Scripts/spin.js" type="text/javascript"></script>
    <script src="/Scripts/spin.js" type="text/javascript"></script>
   <script type="text/javascript" >
       

      
   </script> 

    <style type="text/css">


        .auto-style1 {
            width: 50%;
        }
        .auto-style2 {
            height: 20px;
        }
        .auto-style3 {
            height: 20px;
            }
        .auto-style4 {
            height: 20px;
            width: 297px;
        }
         .auto-style5 {
             height: 20px;
             width: 79px;
         }
         .auto-style6 {
             width: 129px;
         }
         .auto-style7 {
             width: 129px;
             height: 20px;
         }
         .auto-style8 {
             font-family: Arial, Helvetica, sans-serif;
             font-size: 12px;
             text-align: left;
             font-weight: bold;
             color: #5E5E5E;
             margin-left: 0px;
             height: 20px;
         }
        .error
        {
            text-align: left;
            text-indent: 30px;
        }
        
        .error_message
        {
            font: 7.5pt Verdana,sans-serif;
            color: #FF2222;
        }
        
        
        .pn_message
        {
            width: 600px;
            height: 400px;
            margin: 40px auto;
        }
        
          .Titulo
        {
            width: 50%; 
            height: 40px; 
            font-family: Arial Black; 
            font-size: 16pt;
            color: red; 
            float:left; 
            margin-top: 50px;
            margin-left:20px;
        }
        
        .Texto
        {
            width: 55%; 
            height: 200px; 
            font-family: Arial Black; 
            font-size: 13pt;
            color: black; 
            float: left; 
            margin-top: 20px;
            margin-left:20px;
            text-indent: 35px;
        }
        

        .foto { 
                width: 14%; 
            height: 160px; 
             margin-left:1%;
              margin-top: 5px;
            }

        .fotoLabel { 
                
             margin-left:20%;
             margin-top: -40px;
                 }

    </style>

      <link href="../Styles/pesquisa.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="foo"></div>
    <div class="breadcrumb">
        <p style="text-align: left;">
            Configurações > <span>Avaliação do Aprendiz</span></p>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
             
           

    <asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
     <div class="text_titulo">
                <h1> Avaliações Disponíveis Educadores</h1>
            </div>
                   <div class="cadastro_pesquisa" style="height: 70px;width:98%; margin: 30px 1% 10px 1%; border: 1px solid #7f7f7f">
                    <br />

                    <div class="linha_cadastro" style="width: 80%; text-align: right;float:right;">
                        <span class="fonteTab">Matrícula.:&nbsp;</span>
                            <asp:TextBox runat="server" ID="tb_matricula" Width="100px" CssClass="fonteTexto" MaxLength="4"
                                onkeyup="formataInteiro(this,event);" />
                                &nbsp;&nbsp;&nbsp;
                       
                        &nbsp;&nbsp; <span class="fonteTab">Turma:&nbsp;</span>
                        <asp:DropDownList ID="DDTurmaCapacitacao" runat="server" CssClass="fonteTexto"
                            DataTextField="TurNome" DataValueField="TurCodigo" Height="16px"
                            Width="150px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                              <asp:Button ID="btn_pesquisa03" runat="server" CssClass="btn_novo" 
                            Text="Pesquisar" onclick="btn_pesquisa03_Click"  />
                    </div>


                </div>

        <br/>

         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" CssClass="list_results_Menor"
                                PageSize="15" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"  DataKeyNames="ParUniDescricao,Apr_Nome" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PesNome" HeaderText="Pesquisa">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepMes" HeaderText="Mes">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepAno" HeaderText="Ano">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Situacao" HeaderText="Situação">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Codigo" HeaderText="Matricula" />
                                     <asp:TemplateField HeaderText="Responder">  
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMBeditar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PepCodigo")%>'
                                        OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_edit.png" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="List_results_menor" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="HFConfirma" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
    </asp:View>

        <asp:View ID="View2" runat="server">
            <div class="text_titulo">
                <h1> <asp:Label runat="server" ID="Lb_Nome_pesquisa" /></h1>
            </div>
            <br />

            <div class="controls FundoPainel" style="height: 165px; border: solid 1px #484848;">
                                 
                 <div  class="foto">
                     <asp:ImageButton runat="server" ID="IMG_foto_aprendiz" Width="100%" Height="100%"
                    Style="margin: auto; z-index: 1; border: none;" />
          
                     </div>
                    
                   <div class="fotoLabel">
                   
                    <span class="fonteTab">Empresa: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_empresa" /><br />
                    <span class="fonteTab">Aprendiz: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Aprendiz" /><br />
                    <br />
                   </div>   
            </div>

            <br/>
            <asp:Panel runat="server" ViewStateMode="Enabled" Width="800px" Style="margin: 0 auto;" ID="Pn_Pesquisa">
            </asp:Panel>
            <div style="width: 800px; margin: 0 auto;">
                <span class="fonteTab">Observações</span><br />
                <asp:TextBox ID="TB_observacao" runat="server" CssClass="fonteTexto" Height="40px"
                    onkeyup="javascript:IsMaxLength(this, 255);" TextMode="MultiLine" Width="600px"></asp:TextBox>
            </div>
             
            <br/>
            <div class="error">
                <asp:Label CssClass="error_message" runat="server" ID="LB_erro" />
            </div>
            <div class="controls">
                <div class="centralizar">
                    <asp:Button ID="btn_salvar" runat="server" CssClass="btn_novo" Text="Salvar Avaliação"
                        OnClick="btn_salvar_Click" OnClientClick="this.disabled = true; this.value = 'Salvando...';"   UseSubmitBehavior="false"   />
                         <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" 
                        Text="Voltar" onclick="Button1_Click"  />
                </div>
            </div>
        </asp:View>
        <asp:View ID="View3" runat="server">
        <div class="pn_message">
        <div style="float: left;">
            
          <asp:Image ID="Image1" runat="server" Width="250px" ImageUrl="~/images/logofundacao.png" />
        </div>
         <div  class="Titulo">
        <asp:Label runat="server" id="LB_title" /> 
        </div>
          <div  class="Texto"><br/>
        <asp:Label runat="server" id="LBInfo"/> 
        </div> 
          <div style="float: right;margin-right:10px;">
            <asp:Button ID="Button1" runat="server" CssClass="btn_novo" 
                 Text="Voltar" onclick="Button1_Click" />
        </div>  
        </div>
        </asp:View>
    </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>

    
</asp:Content>
