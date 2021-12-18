<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    MasterPageFile="~/MPUsers.master" AutoEventWireup="true" CodeBehind="AniversariantesCliente.aspx.cs"
    Inherits="ProtocoloAgil.pages.AniversariantesCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Aprendizes > <span>Cliente Aniversariantes</span>
        </p>
    </div>
    <div class="controls">
        <div style="float: left">
            <asp:Button runat="server" ID="btnListar" CssClass="btn_controls" Text="Listar" OnClick="btnListar_Click" />
            <asp:Button runat="server" ID="btnImprimir" CssClass="btn_controls" Text="Imprimir"
                OnClick="btnImprimir_Click" Visible="false" />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="text_titulo">
                <h1>Pesquisa de Clientes Aniversariantes</h1>
            </div>
            <br />
            <table class="FundoPainel centralizar" style="width: 98%; border: solid 1px #787878;">
                <tr>
                    <td class="espaco" colspan="5"></td>
                    <td rowspan="4" style="width: 15%;">&nbsp;
                        <asp:Button ID="Button2" runat="server" CssClass="btn_novo" OnClick="btnpesquisa_Click"
                            Text="Pesquisar" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">&nbsp;
                    </td>

                    <td class="Tam08 fonteTab" style="text-align: right;">Mês: &nbsp;&nbsp;
                    </td>
                    <td class="Tam30 fonteTab">
                        <asp:DropDownList ID="DDmeses" runat="server" CssClass="fonteTexto" Height="18px"
                            Width="120px">
                            <asp:ListItem Value="">Selecione</asp:ListItem>
                            <asp:ListItem Value="01">Janeiro</asp:ListItem>
                            <asp:ListItem Value="02">Fevereiro</asp:ListItem>
                            <asp:ListItem Value="03">Março</asp:ListItem>
                            <asp:ListItem Value="04">Abril</asp:ListItem>
                            <asp:ListItem Value="05">Maio</asp:ListItem>
                            <asp:ListItem Value="06">Junho</asp:ListItem>
                            <asp:ListItem Value="07">Julho</asp:ListItem>
                            <asp:ListItem Value="08">Agosto</asp:ListItem>
                            <asp:ListItem Value="09">Setembro</asp:ListItem>
                            <asp:ListItem Value="10">Outubro</asp:ListItem>
                            <asp:ListItem Value="11">Novembro</asp:ListItem>
                            <asp:ListItem Value="12">Dezembro</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td colspan="6" class="espaco">&nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <div class="formatoTela_02">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="centralizar">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CaptionAlign="Top" CssClass="list_results" EmptyDataText="Não existe registros cadastrados"  EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-BackColor="Silver" Style="margin: auto" HorizontalAlign="Center"
                                PageSize="15" Width="95%" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>

                                    <asp:BoundField DataField="CacCodigo" HeaderText="Código" SortExpression="CacCodigo"
                                        InsertVisible="False" ReadOnly="True">
                                        <HeaderStyle Width="7%" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="CacNome" HeaderText="Cliente" SortExpression="CacNome">
                                        <HeaderStyle Width="40%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="CacDataNascimento" HeaderText="Dia/Mês"
                                        SortExpression="CacDataNascimento" DataFormatString="{0:dd/MM}">
                                        <HeaderStyle Width="13%" />
                                    </asp:BoundField>
                                
                                    <asp:BoundField DataField="CacEmail" HeaderText="Email" SortExpression="CacEmail">
                                        <HeaderStyle Width="30%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>


                                    <asp:TemplateField HeaderText="Enviar Cartão">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEnviarCartao" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CacCodigo")%>'
                                                OnClick="btnEnviarCartao_Click" ImageUrl="~/images/icon_edit.png" runat="server" />
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
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <br />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="centralizar" style="border: none;">
                <iframe runat="server" id="IFrame3" height="680px" width="900px" style="border: none;"
                    name="IFrame3" src="visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
