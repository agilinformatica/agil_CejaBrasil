<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="AprendizesParceiro.aspx.cs" Inherits="ProtocoloAgil.pages.AprendizesParceiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/cdl_bh.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Estatísticas > <span>Aprendizes por Parceiro</span></p>
    </div>
    <div class="formatoTela_02">
        <div class="text_titulo">
            <h1>
                Lista de Aprendizes Ativos por Parceiro</h1>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="btn_alocados_sintetico" runat="server" CssClass="btn_controls" Text="Sintético"
                    OnClick="btn_alocados_sintetico_Click" />
                <asp:Button ID="btn_alocados_Analitico" runat="server" CssClass="btn_controls" Text="Analítico"
                    OnClick="btn_alocados_Analitico_Click" />
                <asp:Button ID="Button1" runat="server" CssClass="btn_controls" Text="Imprimir" OnClick="Button1_Click" />
            </div>
        </div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="centralizar" style="border: none;">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" Style="width: 70%;
                                margin: auto" OnDataBound="GridView_DataBound" DataSourceID="SDS_Aprendizes_Alocados" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="ParCodigo" HeaderText="Código" InsertVisible="False" ReadOnly="True" SortExpression="ParCodigo">
                                        <HeaderStyle Width="12%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Empresa" SortExpression="ParNomeFantasia">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTD" HeaderText="QTD" ReadOnly="True" SortExpression="QTD">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Quantitativo">
                                        <HeaderStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMB_Quantitativo" runat="server" ImageUrl="~/images/icon_edit.png" OnClick="IMB_Quantitativo_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Analítico">
                                        <HeaderStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMB_Analítico" runat="server" ImageUrl="~/images/detalhes_icone.gif" OnClick="IMB_Analítico_Click" />
                                        </ItemTemplate>
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
                                <sortedascendingcellstyle backcolor="#E9E7E2" />
                                <sortedascendingheaderstyle backcolor="#506C8C" />
                                <sorteddescendingcellstyle backcolor="#FFFDF8" />
                                <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="SDS_Aprendizes_Alocados" runat="server" OnSelected="SqlDataSource1_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="SELECT CA_Parceiros.ParCodigo, CA_Parceiros.ParNomeFantasia, Count(CA_AlocacaoAprendiz.ALAAprendiz) AS QTD
FROM ((CA_AlocacaoAprendiz INNER JOIN CA_ParceirosUnidade ON CA_AlocacaoAprendiz.ALAUnidadeParceiro = CA_ParceirosUnidade.ParUniCodigo) INNER JOIN CA_Parceiros ON CA_ParceirosUnidade.ParUniCodigoParceiro = CA_Parceiros.ParCodigo) INNER JOIN CA_Turmas ON CA_AlocacaoAprendiz.ALATurma = CA_Turmas.TurCodigo
WHERE (((CA_AlocacaoAprendiz.ALAStatus)='A') AND ((CA_Turmas.TurCurso)='002'))
GROUP BY CA_Parceiros.ParCodigo, CA_Parceiros.ParNomeFantasia
ORDER BY CA_Parceiros.ParNomeFantasia">
                </asp:SqlDataSource>
                <asp:HiddenField ID="HFRowCount" runat="server" />
                <br />
                <br />
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                         <div class="controls">
                            <div style="float: left; margin-left: 25px;">
                               <span class="fonteTab">Empresa:</span>&nbsp;<asp:Label ID="lb_nome" runat="server" CssClass="fonteTexto" Text="Todos."></asp:Label>
                            </div>
                         </div>
                        <div class="centralizar">
                            <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CaptionAlign="Top" CssClass="grid_cronograma" HorizontalAlign="Center" PageSize="15"
                                OnDataBound="GridView_DataBound" Style="margin: auto" onpageindexchanging="GridView6_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" SortExpression="ParNomeFantasia">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" SortExpression="ParUniDescricao" >
                                    <HeaderStyle Width="15%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_InicioAprendizagem" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderText="Início" SortExpression="ALADataInicio" />
                                    <asp:BoundField DataField="Apr_PrevFimAprendizagem" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderText="Prev. Termino" SortExpression="ALADataTermino" />
                                    <asp:BoundField DataField="ALAValorBolsa" DataFormatString="{0:f2}" HeaderText="Bolsa(R$)"
                                        SortExpression="ALAValorBolsa" />
                                    <asp:BoundField DataField="ALAValorTaxa" DataFormatString="{0:f2}" HeaderText="Taxa(R$)"
                                        SortExpression="ALAValorTaxa" />
                                    <asp:BoundField DataField="ALApagto" HeaderText="Pagamento" ReadOnly="True" SortExpression="ALApagto" />
                                    <asp:BoundField DataField="StaDescricao" HeaderText="Situação" SortExpression="StaDescricao" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="Grid_cronograma" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <sortedascendingcellstyle backcolor="#E9E7E2" />
                                <sortedascendingheaderstyle backcolor="#506C8C" />
                                <sorteddescendingcellstyle backcolor="#FFFDF8" />
                                <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hf_parceiro"/>
                         <br/>
                         <br/>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="text_titulo">
                    <h1>
                        Aprendizes por Unidade. Empresa:
                        <asp:Label ID="LBNomeEmpesa" runat="server" Text="" />
                    </h1>
                </div>
                <br />
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CaptionAlign="Top" CssClass="list_results" DataSourceID="SDS_Unidades_parceiro"
                    HorizontalAlign="Center" OnDataBound="GridView_DataBound" Style="width: 50%;
                    margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade da Empesa" SortExpression="ParUniDescricao" />
                        <asp:BoundField DataField="QTD" HeaderText="QTD" ReadOnly="True" SortExpression="QTD" />
                        <asp:BoundField DataField="Percentual" HeaderText="Percentual" ReadOnly="True" SortExpression="Percentual" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                        LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                    <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <sortedascendingcellstyle backcolor="#E9E7E2" />
                    <sortedascendingheaderstyle backcolor="#506C8C" />
                    <sorteddescendingcellstyle backcolor="#FFFDF8" />
                    <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SDS_Unidades_parceiro" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                    OnSelected="SqlDataSource1_Selected" SelectCommand="Select count(ALAAprendiz) as QTD, convert( numeric(10,2),(( convert( numeric(10,2),count(ALAAprendiz) *100))/ 
                                convert( numeric(10,2),( select count(ALAUnidadeParceiro) 
                                from dbo.CA_AlocacaoAprendiz inner join dbo.CA_ParceirosUnidade  on  ALAUnidadeParceiro = CA_ParceirosUnidade.ParUniCodigo 
                                 INNER JOIN CA_Turmas ON CA_AlocacaoAprendiz.ALATurma = CA_Turmas.TurCodigo            
                                where  CA_ParceirosUnidade.ParUniCodigoParceiro = a.ParUniCodigoParceiro and ALAStatus='A' AND ((CA_Turmas.TurCurso)='002')))))as Percentual,  
                                a.ParUniDescricao from  dbo.CA_AlocacaoAprendiz  inner join dbo.CA_ParceirosUnidade a on  ALAUnidadeParceiro = a.ParUniCodigo 
                                where a.ParUniCodigoParceiro = @codigo AND ALAStatus='A' and (ALATurma < 15 or ALATurma = 37) group by a.ParUniDescricao,ParUniCodigoParceiro order by count(ALAAprendiz) desc">
                    <SelectParameters>
                        <asp:SessionParameter Name="codigo" SessionField="PRMT_Empresa" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <br />
            </asp:View>
            <asp:View runat="server" ID="View4">
                <div class="centralizar">
                    <iframe runat="server" id="Iframe1" src="visualizador.aspx" class="VisualFrame">
                    </iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
