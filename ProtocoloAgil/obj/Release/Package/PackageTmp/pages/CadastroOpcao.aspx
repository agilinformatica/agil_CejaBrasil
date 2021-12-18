<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroOpcao.aspx.cs"
    Inherits="ProtocoloAgil.pages.CadastroOpcao" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50731.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("HFConfirma");
            if (confirm("Deseja realmente excluir esta opção ?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
    <link href="../Styles/pesquisa.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/cdl_bh.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Mascara.js" type="text/javascript"></script>
    <style>
        html
        {
            background: #fff;
        }
        
        .controls{
	        width:96%; height:30px; border:solid 1px #dfdfdf; margin:10px 1% 10px 1%; padding:10px 1% 10px 1%;
	        clear: both;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ScriptMode="Release">
    </asp:ToolkitScriptManager>
    <div class="centralizar" style="width: 100%;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="centralizar" style="width: 96%; margin: 0 2% 0 2%">
                    <div class="cadastro_pesquisa" style="height: 160px; margin-bottom: 15px;">
                        <div class="titulo cortitulo corfonte" style="height: 20px; font-size: large;">
                            Cadastro de Questões
                        </div>
                        <div class="linha_cadastro">
                            <span class="fonteTab">Identificador: </span>
                            <br />
                            <asp:TextBox ID="tb_numero" Height="13px" Width="50px" MaxLength="5" onkeyup="formataInteiro(this,event);"
                                CssClass="fonteTexto" runat="server"></asp:TextBox>
                        </div>
                          <div class="linha_cadastro">
                            <span class="fonteTab">Nota:</span>
                            <br />
                            <asp:TextBox ID="tb_nota" runat="server" Height="13px"  Width="50px" MaxLength="2" onkeyup="formataInteiro(this,event);"
                                CssClass="fonteTexto" runat="server"></asp:TextBox>
                            <br />
                        </div>
                        <div class="linha_cadastro">
                            <span class="fonteTab">Descrição:</span>
                            <br />
                            <asp:TextBox ID="tb_nome_opcao" runat="server" CssClass="fonteTexto" Height="13px"
                                MaxLength="80" Width="60%"></asp:TextBox>
                            <br />
                        </div>
                      
                        <div class="linha_next" style="margin-top: -80px; width: 180px; float: right;">
                         <asp:Button ID="btn_next_final" runat="server" CssClass="btn_novo" Text="Nova" onclick="btn_next_final_Click1" />
                            <asp:Button ID="btn_confirmar" runat="server" CssClass="btn_novo" Text="Confirmar" OnClick="btn_next_Click" />
                        </div>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Style="width: 90%; margin: 0 auto;" CaptionAlign="Top" CssClass="list_results"
                        HorizontalAlign="Center" OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField HeaderText="Código" DataField="OpcOrdemExibicao">
                                <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nome" DataField="OpcTexto">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                ShowSelectButton="True">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OpcCodigo")%>'
                                        OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"
                                        runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                        <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:HiddenField ID="HFRowCount" runat="server" />
                            <div class="controls">
            <div class="centralizar">
                <asp:Button ID="btn_close" runat="server" CssClass="btn_novo" Text="Fechar" OnClick="btn_close_Click" />
            </div>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField runat="server" ID="HFConfirma" />
        <br />

    </div>
    </form>
</body>
</html>
