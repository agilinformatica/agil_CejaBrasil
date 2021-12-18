<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="ListaPresencaCapacitacao.aspx.cs" Inherits="ProtocoloAgil.pages.ListaPresencaCapacitacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
    <style type="text/css">
        .auto-style1 {
            width: 60%;
        }
        .auto-style2 {
            height: 18px;
        }
    </style>
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Lista de Presença Capacitação</span></p>
        </div>
        <div class="List_results">
            <br />
            <table align="center" class="auto-style1">
                <tr>
                    <td align="right" class="auto-style2">Turma:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                    <td align="left" class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Data:</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:DropDownList ID="DD_TURMA" runat="server" style="margin-left: 0px" DataTextField="TurNome" DataValueField="TurCodigo" OnSelectedIndexChanged="DD_TURMA_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        &nbsp;<asp:DropDownList ID="DD_Data" runat="server" DataTextField="AcpDataAula" DataValueField="AcpDataAula" DataTextFormatString="{0:dd/MM/yyyy}">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="left">
                        <asp:Button ID="btnGerarRel" runat="server" CssClass="btn_novo" OnClick="btnGerarRel_Click" Text="Gerar Relatório" Width="122px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel2" runat="server"  Visible="false">
                        <div class="centralizar">
                <iframe id="IFrame1" class="VisualFrame" name="IFrame1" src="Visualizador.aspx">
                </iframe>
            </div>
                         </asp:Panel>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>          
        </div>
    </asp:Panel>
</asp:Content>
