<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    MasterPageFile="~/MPUsers.master" AutoEventWireup="true" CodeBehind="AniversariantesPeriodo.aspx.cs"
    Inherits="ProtocoloAgil.pages.AniversariantesPeriodo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Jovens > <span>Aniversariantes</span></p>
    </div>
    <div class="controls">
        <div style="float: left">
            <asp:Button runat="server" ID="btnListar" CssClass="btn_controls" Text="Listar" OnClick="btnListar_Click" />
              <asp:Button ID="texto" runat="server" CssClass="btn_controls" Text="Arquivo de texto" OnClick="texto_Click" />
            <asp:Button runat="server" ID="btnImprimir" CssClass="btn_controls" Text="Imprimir"
                OnClick="btnImprimir_Click" />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="text_titulo">
                <h1>
                    Pesquisa de Aniversariantes por Curso e Turma</h1>
            </div>
            <br />
            <table class="FundoPainel centralizar" style="width: 98%; border: solid 1px #787878;">
                <tr>
                    <td class="espaco" colspan="5">
                    </td>
                    <td rowspan="4" style="width: 15%;">
                        &nbsp;
                        <asp:Button ID="Button2" runat="server" CssClass="btn_novo" OnClick="btnpesquisa_Click"
                            Text="Pesquisar" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                        &nbsp;
                    </td>
                    <td class="Tam08 fonteTab" style="text-align: right;">
                        Curso: &nbsp;&nbsp;
                    </td>
                    <td class="Tam35 fonteTab">
                        <asp:DropDownList ID="DDcursoDiario" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                             CssClass="fonteTexto" DataTextField="CurDescricao"
                            DataValueField="CurCodigo" Height="18px" OnDataBound="IndiceZero" OnSelectedIndexChanged="DDcursos_SelectedIndexChanged"
                            Width="95%">
                        </asp:DropDownList>
                    </td>
                    <td class="Tam08 fonteTab" style="text-align: right;">
                        Mês: &nbsp;&nbsp;
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
                    <td style="width: 5%;">
                    </td>
                    <td class="Tam08 fonteTab" style="text-align: right;">
                        Turma: &nbsp;&nbsp;
                    </td>
                    <td class="Tam25 fonteTab">
                        <asp:DropDownList ID="DDturma_pesquisa" runat="server" AppendDataBoundItems="True"
                           CssClass="fonteTexto" DataTextField="TurNome"
                            DataValueField="TurCodigo" Height="18px" OnDataBound="IndiceZero" ViewStateMode="Enabled"
                            Width="70%">
                        </asp:DropDownList>
                    </td>
                    <td class="Tam10 fonteTab" style="text-align: right;">
                        &nbsp;
                    </td>
                    <td class="Tam25 fonteTab">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="espaco">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
             <div class="formatoTela_02">
             <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="centralizar">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CaptionAlign="Top" CssClass="list_results" Style="margin: auto" HorizontalAlign="Center"
                            PageSize="15" Width="95%" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                            <asp:BoundField DataField="Apr_DataDeNascimento" HeaderText="Dia/Mês"
                                    SortExpression="Apr_DataDeNascimento" DataFormatString="{0:dd/MM}">
                                    <HeaderStyle Width="13%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apr_Codigo" HeaderText="Matrícula" SortExpression="Apr_Codigo"
                                    InsertVisible="False" ReadOnly="True">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz" SortExpression="Apr_Nome">
                                    <HeaderStyle Width="30%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CurDescricao" HeaderText="Curso" SortExpression="CurDescricao">
                                    <HeaderStyle Width="40%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
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
