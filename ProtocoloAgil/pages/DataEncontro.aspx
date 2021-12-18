<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="DataEncontro.aspx.cs" Inherits="ProtocoloAgil.pages.DataEncontro" %>

<%@ Register Assembly="CalendarExtenderPlus" Namespace="AjaxControlToolkitPlus" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetConfirm() {
            ////  var hf = document.getElementById("<%--<%# HFConfirma.ClientID %>--%>");
            if (confirm("Deseja realmente excluir esta disciplina?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Pedagógico> <span>Gerar Encontros</span>
        </p>
    </div>
    <div class="controls">

        <div style="float: left;">
            <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="btn_listar_Click" />
            <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="btn_novo_Click" />

        </div>
    </div>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server">

            <asp:View ID="View1" runat="server">
                <div style="height: 400px;">
                    <br />
                    <div class="centralizar" style="width: 60%;">
                        <table class="FundoPainel Table centralizar">
                            <tr>
                                <td class="corfonte cortitulo titulo" colspan="4">Gerar Encontros
                                </td>
                            </tr>
                            <tr>
                                <td class="Tam20 fonteTab">Tipo de Encontro</td>
                                <td class="Tam20 fonteTab">Gerar encontros no mês de</td>
                                <td class="Tam20 fonteTab">Data Início</td>
                            </tr>
                            <tr>

                                <td class="fonteTab">
                                    <asp:DropDownList ID="DDTipoEncontro" runat="server"
                                        CssClass="fonteTexto"
                                        Style="height: 18px; width: 35%;">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="fonteTab">
                                    <asp:DropDownList ID="DDMes" runat="server"
                                        CssClass="fonteTexto"
                                        Style="height: 18px; width: 50%;">
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

                                <td class="Tam12 fonteTab">
                                    <asp:TextBox ID="txtDataInicio" runat="server" CssClass="fonteTexto" Height="18px"
                                        MaxLength="10"
                                        onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                                    <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus8" runat="server" Format="dd/MM/yyyy"
                                        PopupPosition="BottomRight" TargetControlID="txtDataInicio">
                                    </cc2:CalendarExtenderPlus>
                                </td>
                            </tr>

                            <tr>
                                <td class="fonteTab">Local
                                </td>
                            </tr>
                            <tr>
                                <td class="fonteTab" colspan="4">
                                    <asp:TextBox ID="txtLocal" runat="server" CssClass="fonteTexto" Height="40px"
                                        MaxLength="255" TextMode="MultiLine" Width="97%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="controls" style="width: 75%; margin: 0 auto; text-align: center;">
                        <asp:Button ID="btnConfirmar" runat="server" OnClick="btnConfirmar_Click" CssClass="btn_novo" Text="Confirmar" />
                    </div>
                </div>




            </asp:View>


            <asp:View ID="View2" runat="server">


                <div class="text_titulo">
                    <h1>Pesquisa de Encontros</h1>
                </div>
                <br />
                <asp:Panel ID="Panel4" runat="server" CssClass="centralizar"  Width="700px">
                    <table class="FundoPainel Table centralizar">
                        <tr>
                            <td class="Tam20 fonteTab">Data Início</td>
                            <td class="Tam20 fonteTab">Data Término</td>
                        </tr>
                        <tr>


                            <td class="Tam12 fonteTab">
                                <asp:TextBox ID="txtDataInicioPesquisa" runat="server" CssClass="fonteTexto" Height="18px"
                                    MaxLength="10"
                                    onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                                <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus1" runat="server" Format="dd/MM/yyyy"
                                    PopupPosition="BottomRight" TargetControlID="txtDataInicioPesquisa">
                                </cc2:CalendarExtenderPlus>

                            </td>

                            <td class="Tam12 fonteTab" style="text-align: left">
                                <asp:TextBox ID="txtDataFinalPesquisa" runat="server" CssClass="fonteTexto" Height="18px"
                                    MaxLength="10"
                                    onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                                <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server" Format="dd/MM/yyyy"
                                    PopupPosition="BottomRight" TargetControlID="txtDataFinalPesquisa">
                                </cc2:CalendarExtenderPlus>

                            </td>

                            <td>
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="btn_novo" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <br />

                <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView Width="60%" ID="gridEncontros" OnPageIndexChanging="gridEncontros_PageIndexChanging"
                            runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CaptionAlign="Top" CssClass="grid_Aluno" Style="width: 60%; margin: auto" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>

                                <asp:BoundField DataField="DteTipoEncontro" HeaderText="Tipo" InsertVisible="False" ReadOnly="True" SortExpression="DteTipoEncontro">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="DteData" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Encontros" InsertVisible="False" ReadOnly="True" SortExpression="DteData">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="DteLocalEncontro" HeaderText="Local" InsertVisible="False" ReadOnly="True" SortExpression="DteLocalEncontro">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80%" />
                                    <ItemStyle HorizontalAlign="Left" Width="80%" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="Grid_Aluno" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg" LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                            <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:View>

            <asp:View ID="View4" runat="server">
                <div class="centralizar">
                    <iframe id="IFrame1" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
