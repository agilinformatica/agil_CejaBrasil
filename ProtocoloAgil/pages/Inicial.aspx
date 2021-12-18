<%@ Page Title="Gestão de Jovem Aprendiz " Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" Inherits="ProtocoloAgil.pages.Inicial" Codebehind="Inicial.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style2
        {
            text-align: center;
            margin:0 auto;
        }
        .style4
        {
            font-family: "Century Gothic";
            font-size: x-large;
            color: #3BBC48;
        }
        .style3
        {
            font-family: "Century Gothic";
            color: #23702B;
        }
        
        .style5
        {
            font-family: "Tahoma";
            font-size: x-large;
            color: #FF0000;
        }
        .clearboth
        {
            font-weight: bold;
        }
        
        .message
        {
            text-align:left;
            width: 100%;
            margin: 5px;
        }
        
        
        .message_text
        {
             font:10pt Verdana;
             color: #003399;
             font-weight: bold;
             text-indent: 30px;
        }
        
        .obs
        {
            font:11pt  Verdana;
            color: #660000;
            font-weight: bold;
        }
        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="style2" Height="400px"  Width="600px">
    <strong>
  
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <asp:Label ID="LBsaudacao" runat="server" CssClass="style4"  Font-Size="20px"></asp:Label>
    </strong>
    <br class="style3" />
     <span class="style5">Seja Bem Vindo ao Sistema de Gestão do Programa Jovem Aprendiz </span>
<%--          <span class="style5">FELIZ NATAL E UM 2016 REPLETO DE COISAS BOAS COM MUITA SAÚDE</span>--%>
     <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <div class="message">
        <asp:Literal ID="lt_mensagem_aluno" runat="server"></asp:Literal>
    </div>


    </asp:Panel>
</asp:Content>

