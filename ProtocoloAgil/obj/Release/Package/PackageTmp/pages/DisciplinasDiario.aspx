<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    MasterPageFile="~/MaEducador.Master" AutoEventWireup="true" CodeBehind="DisciplinasDiario.aspx.cs"
    Inherits="ProtocoloAgil.pages.DisciplinasDiario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .fontetopico
        {
            font-family: Arial black;
            font-size: 8pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Notas e Faltas> <span>Diario Eletronico</span></p>
    </div>
     <div class="formatoTela_02">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <asp:Panel runat="server" ID="Panel1" CssClass="centralizar" DefaultButton="">
                <div class="text_titulo">
                    <h1>
                        Lista de Turmas e Disciplinas - Lançamento de Conteúdo Ministrado</h1>
                </div>
                <br />
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    Style="margin: auto;" CaptionAlign="Top" CssClass="list_results" DataSourceID="SDSDisciplinas"
                    HorizontalAlign="Center" OnDataBound="GridView_DataBound" Width="98%" 
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
                    onrowcommand="GridView1_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                          <asp:BoundField DataField="DpOrdem" HeaderText="Cod." SortExpression="DpOrdem">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
       <%--                   <asp:BoundField DataField="DPDisciplina" HeaderText="Cod." SortExpression="DPDisciplina">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="5%" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina" SortExpression="DisDescricao">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DPDataInicio" HeaderText="Data Inicio" SortExpression="DPDataInicio"
                            DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DPDataTermino" HeaderText="Data Termino" SortExpression="DPDataTermino"
                            DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Image" HeaderText="Lançar Conteúdo" 
                            SelectImageUrl="~/images/icon_edit.png" ShowSelectButton="True" >
                             <ItemStyle Width="10%" />
                        </asp:CommandField>
                        <asp:ButtonField ButtonType="Image" CommandName="Imprimir" HeaderText="Imprimir Conteudo"
                            ImageUrl="~/images/icon_edit.png">
                             <ItemStyle Width="12%" />
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
                <asp:HiddenField ID="HFmatricula" runat="server" />
                <asp:SqlDataSource ID="SDSDisciplinas" runat="server" OnSelected="SqlDataSource1_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                    
                    SelectCommand="select * from  View_CA_DisciplinasProfessores where EducCodigo = @RegAcad">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HFmatricula" Name="RegAcad" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View3" runat="server">
           <asp:HiddenField ID="HFordem" runat="server" />
            <asp:HiddenField ID="HFRowCount" runat="server" />
            <div class="controls">
                <div style="float: left; margin-left: 30px;">
                    <span class="fonteTab">Módulo: </span>&nbsp;<asp:Label id="LBCursoCont" runat="server"
                        class="fonteTab" Text="" />
                    <br />
                    <span class="fonteTab">Turma: </span>&nbsp;<asp:Label id="LBTurmaCont" runat="server"
                        class="fonteTab" Text="" />
                </div>
                <div style="float: left; margin-left: 30px;">
                    <span class="fonteTab">Disciplina: </span>&nbsp;<asp:Label id="LBDisciplinaCont" runat="server"
                        class="fonteTab" Text="" />
                    <br />
                    <span class="fonteTab">Data Início: </span>&nbsp;<asp:Label id="LBCodigoCont" runat="server"
                        class="fonteTab" Text="" />
                </div>
                <div style="float: left; margin-left: 60px;">
                    <span class="fonteTab">Data: </span>&nbsp;
                    <br />
                    <asp:DropDownList ID="DDDatasConteudo" runat="server" Width="120px" Height="20px"
                        AutoPostBack="True" CssClass="fonteTab" DataTextField="ADPDataAula"
                        DataValueField="ADPDataAula" OnDataBound="IndiceZero" DataTextFormatString="{0:dd/MM/yyyy}"
                        OnSelectedIndexChanged="DDDatasConteudo_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                </div>
                <div style="float: right; margin-right: 15px;">
                    <asp:Button ID="btnvoltar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btnvoltar_Click" />
                </div>
            </div>
            <br />
            <div style="width: 85%; margin: 0 auto;">
                <table class="Table FundoPainel">
                    <tr>
                        <td class="corfonte cortitulo titulo" colspan="2" style="font-size: large; font-style: normal;">
                            Conteúdo Aula&nbsp;<asp:Label ID="LBDataCont" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class=" espaco" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab" colspan="2">
                            &nbsp; &nbsp; Conteúdo
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab" colspan="2" style="text-align: center;">
                            <asp:TextBox ID="TBConteudo" runat="server" BorderStyle="Groove" BorderWidth="1px" onkeyup="javascript:IsMaxLength(this,255);"
                                CssClass="fonteTexto" Height="30px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab" colspan="2">
                            &nbsp; &nbsp; Recursos
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab" colspan="2" style="text-align: center;">
                            <asp:TextBox ID="TBRecurso" runat="server" BorderStyle="Groove" BorderWidth="1px" onkeyup="javascript:IsMaxLength(this,255);"
                                CssClass="fonteTexto" Height="30px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab" colspan="2">
                            &nbsp; &nbsp; Observação
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab" colspan="2" style="text-align: center;">
                            <asp:TextBox ID="TBobservacao" runat="server" BorderStyle="Groove" BorderWidth="1px" onkeyup="javascript:IsMaxLength(this,255);"
                                CssClass="fonteTexto" Height="30px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="centralizar" colspan="2">
                            <asp:Button ID="BTinsert" runat="server" CssClass="btn_novo" Text="Confirmar" OnClick="BTinsert_Click" />
                            <asp:Button ID="BTLimpar" runat="server" CssClass="btn_novo" Text="Limpar" type="reset"
                                OnClick="BTLimpar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco" colspan="2">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <div class="centralizar">
                <asp:DataList ID="listaconteudo" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                    Width="85%" CssClass="centralizar">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td class="cortitulo corfonte" style="width: 230px;">
                                    <asp:Label ID="Label2" runat="server"><%#  string.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "ADPDataAula"))%></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fonteTab" style="height: 100px; vertical-align: top">
                                    <span class="fontetopico">Conteúdo:</span>&nbsp;
                                    <asp:Label ID="Label3" class="fonteDataList" runat="server"> <%# DataBinder.Eval(Container.DataItem, "ADPConteudoLecionado")%></asp:Label><br />
                                    <span class="fontetopico">Recursos:</span>&nbsp;
                                    <asp:Label ID="Label4" class="fonteDataList" runat="server"><%# DataBinder.Eval(Container.DataItem, "ADPRecursosUsados")%></asp:Label><br />
                                    <span class="fontetopico">Obs.:</span>&nbsp;
                                    <asp:Label ID="Label5" class="fonteDataList" runat="server"><%# DataBinder.Eval(Container.DataItem, "ADPObservacoes")%></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <div class="controls">
                    <div style="float: right; margin-right: 30px;">
                        <asp:Button ID="btnvoltarRelat" runat="server" CssClass="btn_novo" Text="Voltar"
                            OnClick="btnvoltar_Click" />
                    </div>
            </div>
            <div class="centralizar" style="border: none;">
                <iframe runat="server" id="IFrame1" src="Visualizador.aspx" class="VisualFrame" width="900px"
                    style="border: none;" ></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>
</asp:Content>