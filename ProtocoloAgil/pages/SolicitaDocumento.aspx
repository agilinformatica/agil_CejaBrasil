<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.SolicitaDocumento"
    Culture="auto" UICulture="auto" CodeBehind="SolicitaDocumento.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="controls">
        <div style="float: left;">
            <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Lista de Solicitados"
                OnClick="listar_Click" />
            <asp:Button ID="btnSolicitar" runat="server" CssClass="btn_controls" Text="Nova Solicitação"
                OnClick="btnSolicitar_Click" />

        </div>
    </div>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="text_titulo" style="float: none;">
                    <h1>Solicitação de Documentos
                    </h1>
                </div>
                <br />

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                    SelectCommand="SELECT * FROM CA_Documentos"></asp:SqlDataSource>

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CaptionAlign="Top"
                    DataSourceID="SqlDataSource2" HorizontalAlign="Center" CssClass="list_results"
                    Style="width: 70%; margin: 0 auto;"
                    AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="DocCodigo" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                            SortExpression="DocCodigo">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DocDescricao" HeaderText="Tipo de Requerimento"
                            SortExpression="DocDescricao">
                            <HeaderStyle Width="42%" />
                            <ItemStyle Width="42%" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DocValor" DataFormatString="{0:c}" HeaderText="Valor"
                            SortExpression="DocValor">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DocDiasEntrega" HeaderText="Dias Úteis para Entrega"
                            SortExpression="DocDiasEntrega">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Image" HeaderText="Solicitar" SelectImageUrl="~/images/icon_edit.png"
                            SelectText="" ShowSelectButton="True">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                        </asp:CommandField>
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
            </asp:View>




















            <asp:View ID="View3" runat="server">
                <div class="centralizar">

                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="centralizar FundoPainel " style="width: 800px;">

                                <tr>
                                    <td colspan="2" class="titulo cortitulo corfonte" style="font-size: large">Solicitação de Documento
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%; text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;">Nome</span>
                                    </td>
                                    <td style="width: 75%; text-align: left;">&nbsp;&nbsp;<asp:Label runat="server" ID="lblNome" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 25%; text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;">Valor</span>
                                    </td>
                                    <td style="width: 75%; text-align: left;">&nbsp;&nbsp;<asp:Label runat="server" ID="lblValor" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 25%; text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;">Data Solicitação</span>
                                    </td>
                                    <td style="width: 75%; text-align: left;">&nbsp;&nbsp;<asp:Label runat="server" ID="lblDataSolicitacao" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                                    </td>

                                   
                                </tr>
                               
                                <tr>
                                    <td style="width: 25%; text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;">Data Prevista Entrega</span>
                                    </td>
                                    <td style="width: 75%; text-align: left;">&nbsp;&nbsp;<asp:Label runat="server" ID="lblDataEntrega" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                                    </td>
                                </tr>
                                 <tr runat="server" id="TrDataParecer" visible="false">
                                     <td  style="width: 25%; text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;">Data Parecer</span>
                                    </td>
                                    <td style="width: 75%; text-align: left;">&nbsp;&nbsp;<asp:Label runat="server" ID="lblDataParecer" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                                    </td>
                                </tr>
                                <tr runat="server" visible="false" id="trMostraSeTiverAnexo">
                                    <td style="width: 25%; text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;">Arquivo Anexado</span>
                                    </td>
                                    <td style="width: 75%; text-align: left;">&nbsp;&nbsp;<asp:Label runat="server" ID="lblArquivoAnexado" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                                    </td>
                                </tr>

                                <tr runat="server" id="TrParecer" visible="false">
                                     <td  style="width: 25%; text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;">Parecer</span>
                                    </td>
                                    
                                </tr>
                                <tr runat="server" id="TrParecerCaixaTexto" visible="false">
                                    <td colspan="2" class="centralizar" style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="lblParecer" runat="server" Height="40px"
                                        BorderStyle="Groove" BorderWidth="1px" Width="95%" CssClass="fonteTexto" onkeyup="javascript:ismaxlength(this,255);"
                                        TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr runat="server" id="trAnexo" visible="false">
                                    <td style="width: 20%; text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;" id="spArquivo1" runat="server">Anexar Arquivo 1:</span> </td>
                                    <td colspan="2" style="width: 75%; text-align: left;">




                                        <asp:FileUpload ID="fupArquivo" runat="server" size="60" SkinID="fup" />




                                    </td>
                                </tr>



                                <tr>
                                    <td colspan="2" class="fonteTab" style="text-align: left;">
                                        <span class="fonteTab" style="margin-left: 25px;">Justificativa (apenas 255 caracteres):</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="centralizar" style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtJustificativa" runat="server" Height="40px"
                                        BorderStyle="Groove" BorderWidth="1px" Width="95%" CssClass="fonteTexto" onkeyup="javascript:ismaxlength(this,255);"
                                        TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" class="espaco">&nbsp;
                                    </td>
                                </tr>


                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" runat="server" CssClass="btn_novo" Text="Solicitar" />
                                    </td>
                                </tr>
                            </table>
                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSalvar" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />





                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </asp:View>






















            <asp:View ID="View2" runat="server">
                <div class="text_titulo" style="float: none;">
                    <h1>Documentos Solicitados
                    </h1>
                </div>
                <br />
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand"
                    CaptionAlign="Top"
                    DataSourceID="SqlDataSource3"
                    HorizontalAlign="Center" Style="margin: 0 auto;"
                    CssClass="list_results"
                    PageSize="20" Width="927px" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>

                        <asp:BoundField DataField="DocCodigo" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                            SortExpression="DocCodigo">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="descricao" HeaderText="Documento" ReadOnly="True" SortExpression="DAprDocumento">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DAprDataSolic" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data da Solicitação"
                            ReadOnly="True" SortExpression="DAprDataSolic">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>




                        <asp:BoundField DataField="DAprStatus" HeaderText="Status" SortExpression="DAprStatus">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DAprStatusParecer" HeaderText="Situação Parecer" ReadOnly="True" SortExpression="DAprStatusParecer">

                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5px" />
                        </asp:BoundField>





                        <asp:ButtonField ButtonType="Image" CommandName="Documento"
                            HeaderText="Pedido" ImageUrl="~/images/iconeAlterar.gif" Text="Button">
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:ButtonField>


 
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
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                    SelectCommand="SELECT DocCodigo, DAprDataSolic,  (case when DAprStatus = 'S' then 'Solicitado'  when DAprStatus = 'D' then 'Deferido'  when DAprStatus = 'I' then 'Indeferido'  end) as DAprStatus , (case when DAprStatusParecer = 'Em Análise' then '' when DAprStatusParecer = 'D' then 'Deferido' when DAprStatusParecer = 'I' then 'Indeferido' when DAprStatusParecer  is null then 'Em Análise' end) as DAprStatusParecer , D.DocDescricao As descricao, D.DocCodigo as DocCodigo FROM CA_DocumentosAprendiz A join CA_Documentos D on D.DocCodigo =  A.DAprDocumento  where DAprMatricula =  @matricula">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HFmatricula" Name="matricula" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="HFmatricula" runat="server" />
                <asp:HiddenField ID="HFEmail" runat="server" />
            </asp:View>















        </asp:MultiView>
    </div>
    <asp:HiddenField runat="server" ID="HFCampos"></asp:HiddenField>
</asp:Content>
