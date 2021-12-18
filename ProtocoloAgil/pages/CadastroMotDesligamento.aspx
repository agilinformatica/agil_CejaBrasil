<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.CadastroMotDesligamento"
    CodeBehind="CadastroMotDesligamento.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este motivo de desligamento?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Motivos de Desligamento</span></p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="btn_listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="btn_novo_Click" />
                <asp:Button ID="btn_relatorio" runat="server" CssClass="btn_controls" Visible="false" Text="Relatório" OnClick="btn_relatorio_Click" />
                <asp:Button ID="btn_texto" runat="server" CssClass="btn_controls" Text="Arquivo de Texto" OnClick="btn_texto_Click" />
            </div>
            <div style="float: right;">
                <asp:TextBox ID="pesquisa" runat="server" CssClass="search_controls" />
                <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar" OnClick="btnpesquisa_Click" />
            </div>
        </div>
    </asp:Panel>
     <div class="formatoTela_02">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <br/>
                    <br/>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list_results"
                            AutoGenerateColumns="False" OnDataBound="GridView_DataBound"
                            Style="width: 80%; margin: 0 auto;" HorizontalAlign="Center" 
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="MotCodigo" HeaderText="Código" SortExpression="MotCodigo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MotDescricao" HeaderText="Descrição" SortExpression="MotDescricao">
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/Images/icon_edit.png"
                                    ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MotCodigo")%>'
                                            OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"
                                            runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="10%"></HeaderStyle>
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
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                        <asp:HiddenField runat="server" ID="HFConfirma" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:Panel ID="Panel2" runat="server" CssClass="centralizar" Height="300px" Width="700px">
                <br />
                <br />
 
                     <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo corfonte titulo" colspan="6" style="font-size: large;">
                            Cadastro de Motivos de Desligamento
                        </td>
                    </tr>
                      <tr>
                        <td class="espaco" colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                     <td class="Tam05" style="text-align: left">
                              &nbsp;
                            </td>
                         <td class="Tam15 fonteTab">
                             &nbsp;&nbsp; Código:</td>
                        <td class="Tam15" style="text-align: left">
                            <asp:TextBox ID="TBcodigo" runat="server" MaxLength="3"
                                onkeyup="formataInteiro(this,event);" style=" width: 60%; height: 13px;"> </asp:TextBox> 
                            </td>
                             <td class="Tam20 fonteTab">
                                 &nbsp;&nbsp;</td>
                        <td class="Tam18" style="text-align: left">
                              &nbsp;
                            </td>
                         <td class="Tam05" >
                            &nbsp;</td>
                    </tr>

                         <tr>
                             <td class="Tam05" style="text-align: left">
                                 &nbsp;</td>
                             <td class="Tam10 fonteTab">
                                 &nbsp;&nbsp; Descrição :</td>
                             <td class="Tam18" style="text-align: left" colspan="3">
                                 <asp:TextBox ID="TBNome" runat="server" 
                                     MaxLength="50" style=" width: 90%; height: 13px;"></asp:TextBox>
                             </td>
                             <td class="Tam05">
                                 &nbsp;</td>
                         </tr>

                        <tr>
                   
                          <td class="Tam05">
                              &nbsp; </td>
                            <td class="Tam10">
                                &nbsp;
                            </td>
                            <td class="Tam60" colspan="3" style="text-align: left">
                                &nbsp;&nbsp;</td>
                                 <td class="Tam05" >
                            &nbsp;</td>
                    </tr>

                    <tr>
                        <td class="espaco" colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                </table>


                <br />
                <div class="controls" style="margin: 0 auto; text-align: center;">
                    <asp:Button ID="BTaltera" runat="server" OnClick="BTaltera_Click" CssClass="btn_novo"
                        Text="Confirmar" />
                    &nbsp;
                    <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btn_listar_Click" />
                </div>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="centralizar">
                <iframe id="IFrame1" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>
</asp:Content>