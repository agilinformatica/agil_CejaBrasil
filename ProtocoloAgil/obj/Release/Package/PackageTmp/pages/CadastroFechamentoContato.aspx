<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    EnableEventValidation="true" AutoEventWireup="true" MasterPageFile="~/MPusers.Master"
    Inherits="ProtocoloAgil.pages.CadastroFechamentoContato" CodeBehind="CadastroFechamentoContato.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este fechamento contato?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Fechamento Contatos</span></p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar"
                    OnClick="btn_listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="btn_novo_Click" />
                <asp:Button ID="btn_relatorio"  runat="server" CssClass="btn_controls" Visible="false"
                    Text="Relatório" OnClick="btn_relatorio_Click" />
                <asp:Button ID="btn_texto" Visible="false" runat="server" CssClass="btn_controls" Text="Arquivo de Texto"
                    OnClick="btn_texto_Click" />
            </div>
            <div style="float: right;">
                <asp:TextBox ID="pesquisa" runat="server" CssClass="search_controls" />
                <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                    OnClick="btnpesquisa_Click" />
            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <asp:Panel ID="Panel3" runat="server" Height="420px" Width="100%">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list_results"
                                AutoGenerateColumns="False" OnDataBound="GridView_DataBound" Style="width: 60%;
                                margin: 0 auto" HorizontalAlign="Center" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="FechCodigo" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                                        SortExpression="FechCodigo">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechDescricao" HeaderText="Descrição" SortExpression="FechDescricao">
                                        <HeaderStyle Width="50%" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/Images/icon_edit.png"
                                        ShowSelectButton="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FechCodigo")%>'
                                                OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"
                                                runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle Height="25px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" BackColor="#999999" />
                                <FooterStyle Width="30px" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle Height="20px" BorderStyle="Groove" BorderWidth="1px" />
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
                            <asp:HiddenField runat="server" ID="HFConfirma" />
                            <asp:HiddenField ID="HFRowCount" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                            <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:Panel ID="Panel4" runat="server" CssClass="centralizar" Height="300px" Width="700px">
                    <br />
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="Table FundoPainel">
                                <tr>
                                    <td class="cortitulo corfonte titulo" colspan="6" style="font-size: large;">
                                        Cadastro de fechamento contato
                                    </td>
                                </tr>
                                <tr>
                                    <td class="espaco" colspan="6">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam05" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td class="Tam10 fonteTab">
                                        &nbsp;&nbsp; Código:
                                    </td>
                                    <td class="Tam18" style="text-align: left">
                                        <asp:TextBox ID="TBcodigo" runat="server" CssClass="fonteTexto" Enabled="false"
                                            Height="13px" MaxLength="6" Width="60%"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                    <td class="Tam20 fonteTab">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td class="Tam18" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td class="Tam05">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam05" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td class="Tam10 fonteTab">
                                        &nbsp;&nbsp; Descricao:
                                    </td>
                                    <td style="text-align: left" colspan="4">
                                        <asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="80"
                                            Width="75%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam05">
                                        &nbsp;
                                    </td>
                                    <td class="Tam10">
                                        &nbsp;
                                    </td>
                                    <td class="Tam60" colspan="3" style="text-align: left">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td class="Tam05">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="espaco" colspan="6">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="controls" style="margin: 0 auto; text-align: center;">
                        <asp:Button ID="BTaltera" runat="server" OnClick="BTaltera_Click" CssClass="btn_novo"
                            Text="Confirmar" />
                        &nbsp;
                        <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btn_listar_Click" />
                    </div>
                </asp:Panel>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="centralizar">
                    <iframe id="IFrame1" class="VisualFrame" name="IFrame1" src="Visualizador.aspx">
                    </iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
