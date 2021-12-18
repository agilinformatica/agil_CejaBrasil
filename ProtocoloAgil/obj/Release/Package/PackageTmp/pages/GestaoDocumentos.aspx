<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.GestaoDocumentos"
    Culture="auto" UICulture="auto" CodeBehind="GestaoDocumentos.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="formatoTela_02">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Aprendiz > <span>Gestão de Requerimentos</span>
            </p>
        </div>

        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">


                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    CaptionAlign="Top" DataSourceID="SqlDataSource2"
                    HorizontalAlign="Center" PageSize="15" CssClass="list_results" Style="width: 95%; margin: 0 auto;"
                    AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>

                        <asp:BoundField DataField="sequencia" HeaderText="sequencia">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>


                        <asp:BoundField DataField="DAprMatricula" HeaderText="Matrícula"
                            SortExpression="DAprMatricula"></asp:BoundField>
                        <asp:BoundField DataField="Apr_Nome" HeaderText="Nome"
                            SortExpression="Apr_Nome"></asp:BoundField>
                        <asp:BoundField DataField="DocCodigo" HeaderText="Código"
                            SortExpression="DocCodigo"></asp:BoundField>
                        <asp:BoundField DataField="DocDescricao" HeaderText="Documento"
                            SortExpression="DocDescricao"></asp:BoundField>
                        <asp:BoundField DataField="DAprDataSolic" HeaderText="Data Solicitação"
                            SortExpression="DAluDataSolic" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                        <asp:BoundField DataField="Situacao" HeaderText="Situação"
                            SortExpression="Situacao" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="Parecer" HeaderText="Parecer"
                            SortExpression="Parecer" />


                        <asp:CommandField ButtonType="Image" HeaderText="Detalhes"
                            SelectImageUrl="~/images/icon_edit.png" ShowSelectButton="True" />
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
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                    SelectCommand="select CA_DocumentosAprendiz.DAprMatricula, CA_Aprendiz.Apr_Nome, CA_Documentos.DocCodigo, CA_Documentos.DocDescricao, 
                                    CA_DocumentosAprendiz.DAprDataSolic,( case when CA_DocumentosAprendiz.DAprStatus = 'S' then 'Solicitado' when CA_DocumentosAprendiz.DAprStatus = 'F' then 'Finalizado'  end) as Situacao,
                                    ( case when CA_DocumentosAprendiz.DAprStatusParecer is null  then 'Em Análise' when CA_DocumentosAprendiz.DAprStatusParecer = 'I' then 'Indeferido' when CA_DocumentosAprendiz.DAprStatusParecer = 'D' then 'Deferido' end) as Parecer, CA_DocumentosAprendiz.DAprSequencia as sequencia
                                    from CA_DocumentosAprendiz inner join  CA_Documentos on CA_DocumentosAprendiz.DAprDocumento = CA_Documentos.DocCodigo
                                    inner join CA_Aprendiz on CA_DocumentosAprendiz.DAprMatricula  =  CA_Aprendiz.Apr_Codigo where CA_Documentos.DocProtocolo = 'S' and DAprStatusParecer is null
                                    order by CA_DocumentosAprendiz.DAprDataSolic DESC"></asp:SqlDataSource>
                <br />
                <asp:HiddenField runat="server" ID="CodSolicitacao" />

            </asp:View>



            <asp:View ID="View3" runat="server">
                <div class="centralizar">

                    <div class="controls">
                        <div style="float: right;">
                            <asp:Button ID="Voltar" OnClick="Voltar_Click" runat="server" CssClass="btn_novo" Text="Voltar" />
                        </div>
                    </div>
                    <br />
                    <table class="centralizar FundoPainel " style="width: 800px;">
                        <tr>
                            <td colspan="4" class="titulo cortitulo corfonte" style="font-size: large">Baixa de Documento</td>
                        </tr>
                        <tr>
                            <td style="width: 25%; text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Aluno</span>
                            </td>
                            <td style="width: 75%; text-align: left;" colspan="3">&nbsp;&nbsp;<asp:Label runat="server" ID="TBAlunmo" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%; text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Documento</span>
                            </td>
                            <td style="width: 75%; text-align: left;" colspan="3">&nbsp;&nbsp;<asp:Label runat="server" ID="TBnome" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 25%; text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Valor</span>
                            </td>
                            <td style="width: 25%; text-align: left;" colspan="3">&nbsp;&nbsp;<asp:Label runat="server" ID="TBvalor" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 25%; text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Data Solicitação</span>
                            </td>
                            <td style="width: 25%; text-align: left;">&nbsp;&nbsp;<asp:Label runat="server" ID="TBdataSol" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                            </td>

                            <td style="width: 25%; text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Data Prevista Entrega</span>
                            </td>
                            <td style="width: 25%; text-align: left;">&nbsp;&nbsp;<asp:Label runat="server" ID="TBdataEnt" CssClass="fonteTexto" ForeColor="Black" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%; text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Parecer</span>
                            </td>
                            <td style="width: 75%; text-align: left;" colspan="3">&nbsp;&nbsp;<asp:DropDownList ID="DDParecer" Style="width: 120px" CssClass="fonteTab" runat="server">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="D">Deferido</asp:ListItem>
                                <asp:ListItem Value="I">Indereferido</asp:ListItem>
                            </asp:DropDownList>

                            </td>
                        </tr>

                        <tr id="trArquivoBaixar" runat="server">
                            <td colspan="5">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="list_results"
                                    HorizontalAlign="Left"
                                    Style="margin: auto" Width="400px" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="Nome_Arquivo" HeaderText="Arquivo">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Image" HeaderText="Download" SelectImageUrl="~/images/download.jpg"
                                            ShowSelectButton="True">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="4" class="fonteTab" style="text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Parecer Técnico</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="centralizar" style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TBparecerTecnico" runat="server" Height="40px"
                                BorderStyle="Groove" BorderWidth="1px" Width="95%" CssClass="fonteTexto"
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="fonteTab" style="text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Justificativa</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="centralizar" style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TBjustificativa" runat="server" Height="40px"
                                BorderStyle="Groove" BorderWidth="1px" Width="95%" CssClass="fonteTexto" ReadOnly="true"
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="4" class="fonteTab" style="text-align: left;">
                                <span class="fonteTab" style="margin-left: 25px;">Observação</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="centralizar" style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TBobservacao" runat="server" Height="40px"
                                BorderStyle="Groove" BorderWidth="1px" Width="95%" CssClass="fonteTexto"
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="espaco">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:HiddenField ID="codAluno" runat="server" />
                    <asp:HiddenField ID="descricaoCurso" runat="server" />

                    <div class="controls">
                        <div class="centralizar">
                            <asp:Button ID="BTsalvar" runat="server" CssClass="btn_novo" Text="Salvar requisição" OnClick="BTsalvar_Click" />
                        </div>

                    </div>
                    <br />

                    <br />

                </div>
            </asp:View>











        </asp:MultiView>
    </div>
    <asp:HiddenField runat="server" ID="HFCampos"></asp:HiddenField>
</asp:Content>
