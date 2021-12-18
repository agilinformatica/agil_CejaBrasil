<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    MasterPageFile="~/MaParceiro.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ControleAlocados.aspx.cs" Inherits="ProtocoloAgil.pages.ControleAlocados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel4" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Aprendizes > <span>Aprendizes Alocados</span></p>
        </div>

    </asp:Panel>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="Panel1" runat="server">
                <div class="text_titulo">
                    <h1>Pesquisa de Aprendizes Alocados</h1>
                </div>
                <br />
                <div class="controls FundoPainel">
                    <div  style="float: left; margin-left: 70px">
                       <span class="fonteTab"><strong>Nome:</strong></span> &nbsp;&nbsp;
                        <asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" Height="20px" Width="400px"></asp:TextBox>
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar"
                            OnClick="btnpesquisa_Click" />
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CaptionAlign="Top" CssClass="list_results_Menor" HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged"  PageSize="12" 
                            DataSourceID="Alunos" CellPadding="4" ForeColor="#333333" GridLines="None" >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="Apr_Codigo" HeaderText="Código" InsertVisible="False"
                                    ReadOnly="True" SortExpression="Apr_Codigo">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" 
                                    SortExpression="ParUniDescricao">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StaDescricao" HeaderText="Situação" 
                                    SortExpression="StaDescricao">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ALADataInicio" HeaderText="Data Entrada" 
                                    SortExpression="ALADataInicio" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ALAInicioExpediente" HeaderText="Início Exp." 
                                    SortExpression="ALAInicioExpediente" DataFormatString="{0:HH:mm}">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ALATerminoExpediente" DataFormatString="{0:HH:mm}" 
                                    HeaderText="Fim Exp." >
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Detalhes" SelectImageUrl="~/images/icon_edit.png"
                                    SelectText="Alterar" ShowSelectButton="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                        <asp:SqlDataSource ID="Alunos" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                            OldValuesParameterFormatString="original_{0}" 
                            SelectCommand="Select Apr_Codigo, Apr_Nome,ParUniDescricao, StaDescricao , ALADataInicio, ALAInicioExpediente, ALATerminoExpediente, ALAValorBolsa, ALAValorTaxa
from dbo.CA_AlocacaoAprendiz inner join dbo.CA_Aprendiz on 
Apr_Codigo = ALAAprendiz inner join dbo.CA_ParceirosUnidade on 
ParUniCodigo = ALAUnidadeParceiro inner join dbo.CA_SituacaoAprendiz on Apr_Situacao =StaCodigo
WHERE (Apr_Nome LIKE @AluNome + '%') AND ParUniCodigoParceiro = @codigo AND ALAStatus ='A' ORDER BY Apr_Codigo">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="TBNome" Name="AluNome" PropertyName="Text" DefaultValue="%" />
                                <asp:SessionParameter Name="codigo" SessionField="codigo" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:HiddenField runat="server" ID="HFmatricula" />
                        <asp:HiddenField runat="server" ID="HFConfirma" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <div class="centralizar">
                <iframe id="IFrame1" height="695px" width="99%" style="border: none;" name="IFrame1"
                    src="CadastroAprendiz.aspx"></iframe>
            </div>
        </asp:View>

         <asp:View ID="View6" runat="server">
            <div class="centralizar" style="border: none;">
                <iframe id="IFrame3" class="VisualFrame" style="border: none;" name="IFrame2"
                    src="visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
