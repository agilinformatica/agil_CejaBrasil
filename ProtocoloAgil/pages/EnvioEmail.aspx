<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvioEmail.aspx.cs" MasterPageFile="~/MaAluno.Master"
    Inherits="ProtocoloAgil.pages.EnvioEmail" ValidateRequest="false" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .upload-wrapper
        {
            cursor: pointer;
            display: inline-block;
            position: absolute;
            overflow: hidden;
        }
        .upload-file
        {
            cursor: pointer;
            position: absolute;
            filter: alpha(opacity=1);
            -moz-opacity: 0.01;
            opacity: 0.01;
            height: 28px;
            width: 28px;
        }
        .upload-button
        {
            cursor: pointer;
            height: 25px;
            width: 25px;
        }
    </style>
    <script type="text/javascript">
        function NomeArquivo(fuparquivo, event) {
            var files = event.target.files;
            var textbox = document.getElementById('<%= tb_Caminho_arquivo.ClientID %>');
            for (var i = 0, f; f = files[i]; i++) {
                textbox.value = f.name;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="popup02 centralizar" style="width: 100%">
            <br />
            <table class="FundoPainel" style="width: 95%; margin: 0 auto;">
                <tr>
                    <td class="titulo corfonte cortitulo" colspan="4">
                        Contato Secretaria - Envio de E-mail
                    </td>
                </tr>
                <tr>
                    <td class=" Tam05">
                        &nbsp;
                    </td>
                    <td class="fonteTab Tam15">
                        Assunto:
                    </td>
                    <td class="fonteTab" style="width: 60%;">
                        <asp:TextBox ID="TB_assunto" runat="server"  CssClass="fonteTexto"
                             Height="13px" MaxLength="50" Width="95%"></asp:TextBox>
                    </td>
                    <td class="Tam20" rowspan="3">
                        <asp:Button ID="btn_Enviar" runat="server" CssClass="btn_novo" 
                            Text="Enviar" />
                    </td>
                </tr>
                <tr>
                    <td class=" Tam05" style="text-align: center;">
                        &nbsp;
                    </td>
                    <td class="fonteTab Tam15">
                        Anexo:
                    </td>
                    <td class="fonteTab">
                        <asp:TextBox runat="server" ID="tb_Caminho_arquivo" CssClass="fonteTexto" Width="200px"
                            Height="15px"></asp:TextBox>
                        <div class="upload-wrapper">
                            <asp:FileUpload ID="fupArquivo" SkinID="fup" CssClass="upload-file" runat="server"
                                onchange="javascript:NomeArquivo(this,event);" />
                            <img class="upload-button" alt="" title="" src="../images/lupa.png" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class=" Tam05">
                        &nbsp;
                    </td>
                    <td class="fonteTab Tam15">
                    </td>
                    <td class="fonteTab">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <div class="FundoPainel centralizar" style="width: 95%">
                <FTB:FreeTextBox ID="ftb_texto" runat="server" ButtonDownImage="True" Width="100%">
                </FTB:FreeTextBox>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
