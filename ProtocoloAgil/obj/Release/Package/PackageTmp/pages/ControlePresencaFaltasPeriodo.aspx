<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="ControlePresencaFaltasPeriodo.aspx.cs" Inherits="ProtocoloAgil.pages.ControlePresencaFaltasPeriodo" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Pedagógico > <span>Controle de Faltas por Parceiros e Período</span>
        </p>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="centralizar" style="width: 98%; margin: 0 auto;">
                <div class="text_titulo">
                    <h1>Controle de Faltas por Parceiros e Período</h1>
                </div>
                <br />
                <div class="controls FundoPainel">
                    <table class="centralizar  FundoPainel" style="width: 95%">
                        <tr>
                            <td style="text-align: right"><span class="fonteTab">Período:&nbsp;</span>
                                <asp:TextBox runat="server" ID="tb_data_inicial" Width="80px" CssClass="fonteTexto"
                                    MaxLength="10" Height="13px" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                    onkeyup="formataData(this,event);" />
                                <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus1"
                                    PopupPosition="BottomRight" runat="server" TargetControlID="tb_data_inicial">
                                </cc2:CalendarExtenderPlus>
                                <span class="fonteTab">&nbsp;à&nbsp;</span>
                                <asp:TextBox runat="server" ID="tb_data_final" Width="80px" CssClass="fonteTexto"
                                    MaxLength="10" Height="13px" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                    onkeyup="formataData(this,event);" />
                                <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus2"
                                    PopupPosition="BottomRight" runat="server" TargetControlID="tb_data_final">
                                </cc2:CalendarExtenderPlus>
                            </td>
                            <td>
                                <asp:Button ID="btn_pesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar" OnClick="btn_pesquisa_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pn_info" runat="server" CssClass="centralizar" Height="100px" Visible="false"
                        Width="500px">
                        <div class="text_titulo" style="margin-top: 30px;">
                            <h1>Não existem presenças para essa Parceiro/Período.</h1>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" CssClass="centralizar" ID="pn_grid" Width="98%">
                        <asp:GridView ID="GridView1" runat="server"  Style="margin: 0 auto; width: 100%; text-align: left;"
                            CssClass="list_results_Menor" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="List_results_menor" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </asp:Panel>
                    <br />
                    <center>
                        <asp:Button ID="btn_Excel" runat="server" CssClass="btn_novo" Text="Exportar Excel" OnClick="btn_Excel_Click" Visible="false" />
                        <asp:Button ID="btn_imprimir" runat="server" CssClass="btn_novo" Text="Imprimir" Visible="false" />
                    </center>
                    <br />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_Excel" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <center>
                <iframe runat="server" id="Iframe1" class="VisualFrame" src="visualizador.aspx" />
            </center>
        </asp:View>
    </asp:MultiView>
</asp:Content>
