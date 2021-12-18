<%@ Page Language="C#" AutoEventWireup="true" Inherits="ProtocoloAgil.pages.AlteraSenha" Codebehind="AlteraSenha.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="wrap">
    <form id="form1" runat="server">
    <asp:Panel ID="Panel2" runat="server" style="margin: 0 auto;" Width="500px" 
        Height="363px">
        <br/>
        <br/>
        <br/>
        <br/>
        <table class="Table FundoPainel">
            <tr>
                <td  class="corfonte cortitulo titulo" colspan="2" 
                    style="font-family: calibri; font-size: large; color: #FFFFFF;">
                    Alteração de Senha</td>
            </tr>
            <tr>
                <td class="fonteTab">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/images/botao_voltar.gif" onclick="ImageButton1_Click" />
                </td>
                <td class="Tam50">
            &nbsp;</td>
            </tr>
            <tr>
                <td > &nbsp;</td>
                <td class="Tam50"> &nbsp;</td>
            </tr>
            <tr>
                <td class="fonteTab2">
                    <asp:Label ID="LBantiga" runat="server" Text="Senha Antiga"></asp:Label>
                    &nbsp;&nbsp;</td>
                <td class="fonteTab">
                    &nbsp;&nbsp;<asp:TextBox ID="TBantiga" runat="server" CssClass="fonteTexto" Height="15px" 
                        TextMode="Password" Width="128px" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="fonteTab2">
                    <span class="fonteTab" style="text-align: left;color: red;">*</span> Nova Senha&nbsp;&nbsp;</td>
                <td class="fonteTab">
                    &nbsp;&nbsp;<asp:TextBox ID="TBsenha" runat="server" CssClass="fonteTexto" Height="15px" TextMode="Password" 
                        Width="128px" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="fonteTab2">
                    Confirme senha&nbsp;&nbsp;</td>
                <td class="fonteTab">
                    &nbsp;&nbsp;<asp:TextBox ID="TBconf" runat="server" CssClass="fonteTexto" Height="15px" TextMode="Password" 
                        Width="128px" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Tam50">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="titulo" colspan="2">
                    <asp:Button ID="BTconfirma" runat="server" CssClass="btn_novo" onclick="BTconfirma_Click" 
                        Text="Confirmar" />
                </td>
            </tr>
            <tr>
                <td class="espaco" colspan="2">
                    &nbsp;</td>
            </tr>
        </table>
        <br/>
        <br/>
        <div class="controls" >
            <span class="fonteTab" style="text-align: left;color: red;">
                * Senha deve conter no máximo 8 caracteres, sem espaço.
            </span>
        </div>
    </asp:Panel>
        <br /></form>
    
</div>
  
</body>
</html>
