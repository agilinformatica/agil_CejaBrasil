<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="ControlePresenca.aspx.cs" Inherits="ProtocoloAgil.pages.ControlePresenca" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Pedagógico > <span>Controle de Presença por Data/Turma</span>
        </p>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="centralizar" style="width: 98%; margin: 0 auto;">
                <div class="text_titulo">
                    <h1>Controle de Presença por Data/Turma</h1>
                </div>
                <br />
                <div class="controls FundoPainel">
                    <table class="centralizar  FundoPainel" style="width: 95%">
                        <tr>
                            <td><span class="fonteTab">Turma:&nbsp;</span>
                                <asp:DropDownList ID="DD_Turma" runat="server" CssClass="fonteTab" Height="18px" Style="width: 60%"
                                    DataSourceID="SDS_Turma" OnDataBound="IndiceZero" DataTextField="TurNome"
                                    DataValueField="TurCodigo">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SDS_Turma" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                                    SelectCommand="SELECT [TurCodigo], [TurNome] FROM [CA_Turmas] ORDER BY [TurNome]"></asp:SqlDataSource>
                            </td>
                            <td style="text-align: right"><span class="fonteTab">Data:&nbsp;</span>
                                <asp:TextBox runat="server" ID="tb_data" Width="80px" CssClass="fonteTexto"
                                    MaxLength="10" Height="13px" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                    onkeyup="formataData(this,event);" />
                                <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                                    PopupPosition="BottomRight" runat="server" TargetControlID="tb_data">
                                </cc2:CalendarExtenderPlus>
                            </td>
                            <td>
                                <asp:Button ID="btn_pesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar" OnClick="btn_pesquisa_Click" />
                                <asp:Button ID="btnConteudo" runat="server" CssClass="btn_novo" Text="Conteúdo" OnClick="btnConteudo_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server" Width="500px" Height="300px" CssClass="centralizar" Visible="false">
                        <div class="text_titulo" style="margin-top: 120px">
                            <h1>Nenhum resultado para esta pesquisa.</h1>
                        </div>
                    </asp:Panel>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
                        Style="width: 98%;" CaptionAlign="Top" CssClass="list_results_Menor" HorizontalAlign="Center" DataSourceID="SDSControlePresenca"
                        OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome" />
                            <asp:BoundField DataField="Apr_Codigo" HeaderText="Código" SortExpression="ParCodigo" />
                            <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome" />
                            <asp:BoundField DataField="DisDescricao" HeaderText="Módulo" SortExpression="Apr_Nome" />
                            <asp:BoundField DataField="Parceiro" HeaderText="Parceiro" SortExpression="Parceiro" />
                            <asp:BoundField DataField="AreaDescricao" HeaderText="Area Atuação" SortExpression="AreaAtuacao" />
                            <asp:BoundField DataField="AdiPresenca" HeaderText="Presença" SortExpression="AdiPresenca">
                                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results_menor" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                        <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SDSControlePresenca" runat="server" OnSelected="SDSControlePresenca_Selected"
                        ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                        SelectCommand="SELECT [TurNome]
                                              ,[Apr_Codigo]
                                              ,[Apr_Nome]
                                              ,[DisDescricao]
                                              ,CASE [ParDescricao] WHEN [ParUniDescricao] THEN [ParDescricao] ELSE ([ParDescricao] + '/' + [ParUniDescricao]) END AS Parceiro
                                              ,[AreaDescricao]
                                              ,[AdiPresenca]
                                        FROM [View_ControlePresenca]
                                        WHERE [AdiDataAula] = '@RegAcad'
                                        ORDER BY [TurNome]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="tb_data" Name="Data" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <center>
                        <asp:Button ID="btn_imprimir" runat="server" CssClass="btn_novo" Text="Imprimir" OnClick="btn_imprimir_Click" Visible="false" />
                    </center>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="controls">
                <div style="float: right;">
                    <asp:Button ID="btn_Listar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btn_Listar_Click" />
                </div>
            </div>
            <center>
                <iframe runat="server" id="Iframe1" class="VisualFrame" src="visualizador.aspx" />
            </center>
        </asp:View>












         <asp:View ID="View3" runat="server">
            <div class="centralizar" style="width: 98%; margin: 0 auto;">
                <div class="text_titulo">
                    <h1>Conteúdo</h1>
                </div>
                <br />
               
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Width="500px" Height="300px" CssClass="centralizar" Visible="false">
                        <div class="text_titulo" style="margin-top: 120px">
                            <h1>Nenhum resultado para esta pesquisa.</h1>
                        </div>
                    </asp:Panel>
                    <asp:GridView ID="GridConteudo" DataSourceID="SDSConteudo" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
                        Style="width: 98%;" CaptionAlign="Top" CssClass="list_results_Menor" HorizontalAlign="Center" 
                        OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome" />
                            <asp:BoundField DataField="EducNome" HeaderText="Educador" SortExpression="EducNome" />
                            <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina" SortExpression="DisDescricao" />
                            <asp:BoundField DataField="ADPConteudoLecionado" HeaderText="Conteúdo" SortExpression="ADPConteudoLecionado" />
                            <asp:BoundField DataField="ADPRecursosUsados" HeaderText="Recursos" SortExpression="ADPRecursosUsados" />
                            <asp:BoundField DataField="ADPObservacoes" HeaderText="Observações" SortExpression="ADPObservacoes" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results_menor" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                        <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <br />
                      <center>
                        <asp:Button ID="btnVoltar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btnVoltar_Click"  />
                    </center>


                     <asp:SqlDataSource ID="SDSConteudo" OnSelected="SDSConteudo_Selected" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                        SelectCommand="">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="tb_data" Name="Data" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>


                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>









    </asp:MultiView>
</asp:Content>
