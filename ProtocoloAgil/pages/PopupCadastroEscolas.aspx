<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupCadastroEscolas.aspx.cs"
    Inherits="ProtocoloAgil.pages.PopupCadastroEscolas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/cdl_bh.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Mascara.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ScriptMode="Release">
        </ajaxToolkit:ToolkitScriptManager> <div id="content">
        <div class="popup03">

            <div style="width: 98%; margin: 0 auto;">
                <br />
               <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo titulo corfonte" colspan="4" style="font-size: large">
                            Dados da Escola
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam15" style="text-align: left;">
                            &nbsp;&nbsp;&nbsp; Código da Escola
                        </td>
                        <td class="fonteTab Tam40" style="text-align: left;">
                          &nbsp;&nbsp; Descrição da Escola
                        </td>
                        <td class="fonteTab Tam14" style="text-align: left;">
                            &nbsp;&nbsp; Cep
                        </td>
                        <td class="fonteTab Tam20" style="text-align: left;">
                           &nbsp;&nbsp; Telefone
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam15" style="text-align: left;">
                            &nbsp;&nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TBCodEscola" runat="server" Height="13px" 
                                MaxLength="4" onkeyup="formataInteiro(this,event);" Enabled="false" Width="50%"
                                CssClass="fonteTexto"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam40" style="text-align: left;">
                            <asp:Label ID="LBnome" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TBnomeEsc" runat="server" Height="13px" 
                                MaxLength="50" Width="90%" CssClass="fonteTexto"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam14">
                            <asp:Label ID="Label5" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TBCep" runat="server"  CssClass="fonteTexto"
                                Height="13px" MaxLength="10" onkeyup="formataCEP(this,event);" Width="80%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam20">
                            <asp:Label ID="Label6" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TBtelefone" runat="server" 
                                CssClass="fonteTexto" Height="13px" MaxLength="14" onkeyup="formataTelefone(this,event);"
                                Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="Table FundoPainel">
                    <tr>
                        <td class="fonteTab " style="width: 25%">
                            &nbsp;&nbsp;&nbsp; Endereço
                        </td>
                        <td class="fonteTab Tam08">
                           &nbsp;&nbsp; Nº
                        </td>
                        <td class="fonteTab Tam15">
                           &nbsp;&nbsp; Complemento
                        </td>
                        <td class="fonteTab Tam20">
                          &nbsp;&nbsp; Bairro
                        </td>
                        <td class="fonteTab Tam05" style="text-align: left;">
                          &nbsp;&nbsp; UF
                        </td>
                        <td class="fonteTab Tam20" >
                          &nbsp;&nbsp; Cidade
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">
                            &nbsp;&nbsp;<asp:Label ID="LBend" runat="server" Font-Size="11pt" ForeColor="Red"
                                Text="*"></asp:Label>
                            <asp:TextBox ID="TBEndereco" runat="server" Height="13px" 
                                Width="90%" CssClass="fonteTexto"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam05">
                            <asp:Label ID="Label2" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TB_Numero_endereco" runat="server" 
                                CssClass="fonteTexto" Height="13px" Width="70%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam12">
                            &nbsp;
                            <asp:TextBox ID="TB_complemento" runat="server" 
                                MaxLength="30" CssClass="fonteTexto" Height="13px" Width="80%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam12">
                            <asp:Label ID="Label3" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TB_Bairro" runat="server" 
                                CssClass="fonteTexto" Height="13px" Width="85%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam15">
                           <asp:DropDownList ID="DD_estado_Nat" runat="server" AutoPostBack="true" CssClass="fonteTexto" DataTextField="MunIEstado"
                                DataValueField="MunIEstado" OnSelectedIndexChanged="DD_estado_Nat_SelectedIndexChanged" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                 Width="80%" OnDataBound="IndiceZeroUF"
                                ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                        <td class="fonteTab Tam08" style="text-align: center;">
                            
                             <asp:Label ID="Label4" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:DropDownList ID="DDMunicipio" runat="server" CssClass="fonteTexto" DataTextField="MunIDescricao" DataValueField="MunIDescricao"
                                 Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                 Width="85%" OnDataBound="IndiceZeroUF"
                                ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table class="Table FundoPainel">
                    <tr>
                        <td class="fonteTab Tam35" style="text-align: left;">
                            &nbsp;&nbsp;&nbsp; Diretor(a)
                        </td>
                        <td class="fonteTab Tam20">
                            &nbsp; &nbsp; E-mail
                        </td>
                        <td class="fonteTab Tam20">
                            &nbsp;
                        </td>
                        <td class="fonteTab Tam20">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam35" style="text-align: left;">
                          &nbsp;&nbsp;&nbsp;  <asp:TextBox ID="TBrepresentante" CssClass="fonteTexto" runat="server" Height="13px"
                                 Width="90%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam20" colspan="3">
                            &nbsp;
                            <asp:TextBox ID="TBEmail" runat="server" CssClass="fonteTexto" Height="13px" Width="90%"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div class="fonteTexto" style="float: left;">
                    <p>
                       <b>Obs.:</b> Os campos com (<asp:Label ID="Label10" runat="server" Font-Size="11pt" ForeColor="Red"
                            Text="*"></asp:Label>) indicam dados obrigatórios.
                    </p>
                </div>
                <div class="controls">
                    <div class="centralizar">
                        <asp:Button ID="BTsalva" runat="server" CssClass="btn_novo" OnClick="BTsalva_Click"
                            Text="Confirmar" />
                            
                        &nbsp; 
                        <asp:Button ID="BTLimpa" runat="server" Text="Fechar" CssClass="btn_novo" 
                            onclick="BTLimpa_Click" />
                        </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
