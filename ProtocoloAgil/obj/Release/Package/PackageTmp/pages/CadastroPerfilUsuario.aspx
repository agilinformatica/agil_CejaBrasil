<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" EnableEventValidation="false"
    Inherits="ProtocoloAgil.pages.CadastroPerfilUsuario" CodeBehind="CadastroPerfilUsuario.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este Perfil?") == true)
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
                Configurações > <span>Cadastro de Perfil de Usuário</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo Perfil" OnClick="Novo_Click" />
            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="list_results" Height="16px"
                            Style="width: 60%; margin: auto" HorizontalAlign="Center"
                            OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                            OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="PerfCodigo" HeaderText="Código" SortExpression="PerfCodigo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PerfDescricao" HeaderText="Descrição" SortExpression="PerfDescricao">
                                    <HeaderStyle Width="65%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                    SelectText="Alterar" ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PerfCodigo")%>'
                                            OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                        <br />
                        <asp:HiddenField runat="server" ID="HFConfirma" />
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>

            </asp:View>
            <asp:View ID="View2" runat="server">
                <div id="lightbox"></div>
                <asp:Panel ID="PNinsere" runat="server" CssClass="centralizar" Height="300px" Width="500px"
                    DefaultButton="BTinsert">
                    <br />
                    <br />
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="cortitulo corfonte titulo" colspan="4" style="font-size: large;">
                                <asp:Label ID="LBtituloAlt" runat="server" Text="Cadastro de Perfis de Usuário"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="espaco" colspan="4">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="Tam05" style="text-align: left">&nbsp;</td>
                            <td class="Tam08 fonteTab">&nbsp;&nbsp;Código:</td>
                            <td class="Tam50" style="text-align: left">
                                <asp:TextBox ID="TBCodigo" runat="server" CssClass="fonteTexto" Height="13px" Width="20%"></asp:TextBox>
                                &nbsp;
                            </td>

                            <td class="Tam05" style="text-align: left">&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="Tam05" style="text-align: left">&nbsp;</td>
                            <td class="Tam08 fonteTab">&nbsp;&nbsp;Descrição:</td>
                            <td class="Tam18" style="text-align: left">
                                <asp:TextBox ID="TBDescricao" runat="server" CssClass="fonteTexto"
                                    Height="13px" MaxLength="40" Width="90%"></asp:TextBox>
                            </td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="espaco" colspan="4">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="controls" style="text-align: center;">
                        <asp:Button ID="BTinsert" runat="server" OnClick="BTaltera_Click" Text="Confirmar" CssClass="btn_novo" />
                        &nbsp;&nbsp;
                        <asp:Button ID="BTLimpar" runat="server" OnClick="BTlimpar_Click" Text="Limpar" CssClass="btn_novo" />
                    </div>
                </asp:Panel>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="centralizar">
                    <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
