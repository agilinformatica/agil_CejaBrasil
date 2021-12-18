<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    MasterPageFile="~/MaAluno.master" AutoEventWireup="true" CodeBehind="MeusDados.aspx.cs"
    Inherits="ProtocoloAgil.pages.MeusDados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <% var exibe = Session["tipo"];
           if ((string)exibe == "Aluno")
           { %>
        <p style="text-align: left;">
            Secretaria > <span>Meus Dados</span></p>
        <%
           }
           else if ((string)exibe == "Educador")
           {
        %>
        <p style="text-align: left;">
            Geral > <span>Meus Dados</span></p>
        <% } %>
    </div>
    <div class="centralizar">
        <iframe id="IFrame1" runat="server" style="border: none; width: 98%; height:870px;" name="IFrame1">
        </iframe>
    </div>
</asp:Content>
