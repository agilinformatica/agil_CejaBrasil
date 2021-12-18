<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.master" Inherits="ProtocoloAgil.pages.Excluir"
    CodeBehind="Excluir.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p style="height: 40px;" />
    &nbsp;<asp:Panel ID="Panel2" runat="server" CssClass="centralizar" Height="400px"
        Width="435px">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table class="Table FundoPainel">
            <tr>
                <td class="titulo corfonte corfundo" colspan="2" style="font-size: large;">
                    Confirmação de exclusão
                </td>
            </tr>
            <tr>
                <td class="Tam50" style="text-align: left">
                    &nbsp;
                </td>
                <td class="Tam50">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="fonteTab2">
                    Código
                </td>
                <td class="fonteTab">
                    <asp:Label ID="LBcodigo" runat="server" ForeColor="#3333FF"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="titulo" colspan="2">
                    <strong>Excluir registro?</strong>
                </td>
            </tr>
            <tr>
                <td class="titulo" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LBinfo" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
        <div class="controls">
            <asp:Button ID="BTconf" runat="server" CssClass="btn_novo" OnClick="BTconf_Click"
                Text="Confirmar" />
            &nbsp;
            <asp:Button ID="BTcancel" runat="server" CssClass="btn_novo" OnClick="BTcancel_Click"
                Text="Voltar/Cancelar" />
        </div>
    </asp:Panel>
</asp:Content>
