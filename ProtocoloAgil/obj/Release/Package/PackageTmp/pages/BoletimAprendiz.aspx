<%@ Page Title="" Language="C#" MasterPageFile="~/MaAluno.Master" AutoEventWireup="true"
    CodeBehind="BoletimAprendiz.aspx.cs" Inherits="ProtocoloAgil.pages.BoletimAprendiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Acadêmico > <span>Notas e Faltas</span></p>
    </div>
    <div class="text_titulo">
        <h1>
            Demonstrativo de Notas e Faltas do Aprendiz</h1>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="formatoTela_02">
                <div class="controls FundoPainel">
                    <div style="float: left; margin-left: 30px;">
                        <span class="fonteTab">Aprendiz: </span>&nbsp;
                        <asp:Label ID="LBAprendiz_Conceito" runat="server" class="fonteTab" Text="" />
                        <br />
                        <span class="fonteTab">Curso: </span>&nbsp;
                        <asp:Label ID="LBCurso_Conceito" runat="server" class="fonteTab" Text="" />
                    </div>
                    <div style="float: left; margin-left: 150px;">
                        <span class="fonteTab">Turma: </span>&nbsp;
                        <asp:Label ID="LBTurma_Conceito" runat="server" class="fonteTab" Text="" />
                        <br />
                        <span class="fonteTab">Parceiro: </span>&nbsp;
                        <asp:Label ID="LBCodigo_Parceiro" runat="server" class="fonteTab" Text="" />
                    </div>
                    <div style="float: right; margin-right: 30px;">
                        <asp:Button ID="btn_adicionar" runat="server" CssClass="btn_novo" Text="Imprimir" OnClick="btn_adicionar_Click" />
                    </div>
                </div>
                <br />
                <asp:UpdatePanel runat="server" ID="Panel1" CssClass="centralizar">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            Style="margin: auto;" CaptionAlign="Top" CssClass="list_results" DataSourceID="SDSDisciplinas"
                            HorizontalAlign="Center" Width="98%" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina" SortExpression="DisDescricao">
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Professor" HeaderText="Professor" SortExpression="Professor">
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DieCompreensao" HeaderText="Compreensão" SortExpression="DieCompreensao">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DieComLing" HeaderText="Comunicação e Linguagem" SortExpression="DieComLing">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DiePostura" HeaderText="Postura" SortExpression="DiePostura">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DieParticipacao" HeaderText="Participação" SortExpression="DieParticipacao">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaFinal" HeaderText="Resultado" SortExpression="MediaFinal">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DiaNumeroFaltas" HeaderText="Faltas" SortExpression="DiaNumeroFaltas">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                        <asp:HiddenField ID="HFmatricula" runat="server" />
                        <asp:SqlDataSource ID="SDSDisciplinas" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                            SelectCommand="select * from dbo.View_Resultado_Final where Apr_Codigo = @RegAcad">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="HFmatricula" Name="RegAcad" PropertyName="Value" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="centralizar">
                <iframe src="Visualizador.aspx" id="Iframe1" class="VisualFrame" name="Iframe1">
                </iframe>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>