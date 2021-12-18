<%@ Page Title="" Language="C#" MasterPageFile="~/MaAluno.Master" AutoEventWireup="true"
    CodeBehind="Avaliacao.aspx.cs" Inherits="ProtocoloAgil.pages.Avaliacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Secretaria > <span>Autoavaliação do Aprendiz</span></p>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="text_titulo">
                <h1>
                    Autoavaliação</h1>
            </div>
            <br />
            <div class="controls FundoPainel" style="height: 65px; border: solid 1px #484848;">
                <div style="float: left">
                    <span class="fonteTab">Empresa: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_empresa" /><br />
                    <span class="fonteTab">Aprendiz: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Aprendiz" /><br />
                    <span class="fonteTab">Orientador: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Orientador" /><br />
                    <span class="fonteTab">Horário de Aprendizagem: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Horario" />
                </div>
            </div>
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
                        OnClick="btn_salvar_Click" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
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
        </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
