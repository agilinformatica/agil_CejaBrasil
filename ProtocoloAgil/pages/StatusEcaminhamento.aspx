<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.StatusEcaminhamento"
    Culture="auto" UICulture="auto" CodeBehind="StatusEcaminhamento.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este Status Encaminhamento?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel8" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de Status Encaminhamento</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Status Encaminhamento"
                    OnClick="listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text=" Novo Status Encaminhamento" OnClick="Incluir_Click" />
            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="text_titulo" style="float: none;">
                    <h1>Status Encaminhamento Cadastrados
                    </h1>
                </div>
                <br />
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" OnDataBound="GridView_DataBound"
                    Style="width: 95%; margin: auto" DataKeyNames="Ste_Codigo" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Ste_Codigo" HeaderText="Código"
                            SortExpression="Ste_Codigo" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="Ste_Descricao" HeaderText="Descrição"
                            SortExpression="Ste_Descricao"></asp:BoundField>
                        <asp:CommandField ButtonType="Image" HeaderText="Alterar"
                            SelectImageUrl="~/images/icon_edit.png" ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Ste_Codigo")%>'
                                    OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                        LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                    <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:HiddenField ID="HFRowCount" runat="server" />
                <asp:HiddenField ID="HFEscolaRef" runat="server" />
                <asp:HiddenField runat="server" ID="HFConfirma" />
                <br />
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div style="width: 90%; margin: 0 auto;">
                    <br />
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="cortitulo titulo corfonte" colspan="4">Dados Status Encaminhamento</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;&nbsp; Código Status Encaminhamento</td>
                            <td class="fonteTab Tam40" style="text-align: left;">Descrição Status Encaminhamento</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;
                                <asp:TextBox ID="txtCodigoStatus" runat="server" Height="13px" MaxLength="2"
                                    Width="50%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam40" style="text-align: left;">
                                <asp:Label ID="Label3" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                                <asp:TextBox ID="txtStatusEncaminhamento" runat="server" Height="13px" MaxLength="80"
                                    Width="90%" CssClass="fonteTexto"></asp:TextBox>
                            </td>

                        </tr>
                    </table>

                    <table class="Table FundoPainel">
                    </table>
                    <div class="fonteTexto" style="float: left;">
                        <p>
                            <b>Obs.:</b> Os campos com (<asp:Label ID="Label10" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>) indicam dados obrigatórios.
                        </p>
                    </div>
                    <div class="controls">
                        <div class="centralizar">
                            <asp:Button ID="BTsalva" runat="server" CssClass="btn_novo" OnClick="BTsalva_Click" Text="Salvar" />
                            &nbsp;
                        <asp:Button ID="BTLimpa" runat="server" Text="Limpar" OnClick="BTLimpa_Click" CssClass="btn_novo" />
                        </div>
                    </div>
                    <br />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:HiddenField runat="server" ID="HFCampos"></asp:HiddenField>
</asp:Content>
