<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    MasterPageFile="~/MPusers.Master"
    Inherits="ProtocoloAgil.pages.CadastroDocumentos" CodeBehind="CadastroDocumentos.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 15%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de Requerimentos</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="Novo_Click" />

                <asp:Button ID="relatorio" runat="server" CssClass="btn_controls" Text="Relatório"
                    OnClick="relatorio_Click" Visible="false" />
                <asp:Button ID="texto" runat="server" CssClass="btn_controls" Text="Arquivo de texto"
                    OnClick="texto_Click" Visible="false" />

            </div>

        </div>
    </asp:Panel>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            
             <asp:Panel ID="PainelDocumento" runat="server" CssClass="centralizar" Height="100px" Visible="false"
                            Width="500px">
                            <div class="text_titulo" style="margin-top: 30px;">
                                <h1>Não existem requerimentos cadastrados.</h1>
                            </div>
                        </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" Height="400px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" CssClass="centralizar">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CaptionAlign="Top"
                            Height="12px" HorizontalAlign="Center"
                            OnRowCommand="GridView1_RowCommand" CssClass="list_results"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnDataBound="GridView_DataBound"
                            Style="width: 80%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="DocCodigo" HeaderText="Código" InsertVisible="False" ReadOnly="True" SortExpression="DocCodigo">
                                <HeaderStyle Width="11%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DocDescricao" HeaderText="Descrição" SortExpression="DocDescricao">
                                <HeaderStyle Width="40%" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DocValor" DataFormatString="{0:c}" HeaderText="Valor" SortExpression="DocValor">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DocDiasEntrega" HeaderText="Dias p/ Entrega" SortExpression="DocDiasEntrega">
                                <HeaderStyle Width="12%" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Editar" SelectImageUrl="~/images/icon_edit.png" SelectText="Alterar" ShowSelectButton="True">
                                <HeaderStyle Width="5%" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBexcluir" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DocCodigo")%>' ImageUrl="~/images/icon_remove.png" OnClick="IMBexcluir_Click" OnClientClick="javascript:GetConfirm();" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" OnSelected="SqlDataSource1_Selected"
                            ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                            SelectCommand="SELECT * FROM [CA_Documentos]  ORDER BY DocDescricao"></asp:SqlDataSource>
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:Panel ID="Panel3" runat="server" CssClass="centralizar" Height="424px" Width="830px">
                <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo titulo corfonte" colspan="6" style="font-size: large;">
                            <asp:Label ID="LBtituloAlt" runat="server"
                                Text="Cadastro de Documentos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class=" espaco" colspan="6"></td>
                    </tr>
                    <tr>
                        <td class=" Tam12 fonteTab2 centralizar">Código:</td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtCodigo" runat="server" class="fonteTexto" Height="13px" BorderStyle="Groove" BorderWidth="1px"
                                MaxLength="3" Width="80%"></asp:TextBox>
                        </td>
                        <td class=" Tam12 fonteTab2 centralizar">Tipo:</td>

                        <td class="auto-style1">

                            <asp:DropDownList ID="DDTipo" runat="server" class="fonteTexto">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="E">Escola</asp:ListItem>
                                <asp:ListItem Value="A">Aluno</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class=" Tam12 fonteTab2 centralizar">Protocolo:</td>
                        <td class="auto-style1" align="left">
                            <asp:DropDownList ID="DDProtocolo" runat="server" class="fonteTexto">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">Não</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class=" Tam12 fonteTab2 centralizar">&nbsp;Valor:
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtPreco" runat="server" BorderStyle="Groove" BorderWidth="1px"
                                class="fonteTexto" Height="13px" onkeyup="formataValor(this,event);"
                                Width="80%"></asp:TextBox>
                        </td>
                        <td class="Tam20 fonteTab">&nbsp;&nbsp;&nbsp; Dias Úteis para Entrega:
                        </td>
                        <td class="fonteTab Tam20">
                            <asp:TextBox ID="txtDiasEntrega" runat="server" BorderStyle="Groove" BorderWidth="1px"
                                class="fonteTexto" Height="13px" MaxLength="4"
                                onkeyup="formataInteiro(this,event);" Width="20%"></asp:TextBox>
                            &nbsp;&nbsp;
                                  <asp:CheckBox ID="cb_exige_Anexo" runat="server" Text="Exige Anexo" />
                        </td>
                        <td class="fonteTab">
                            <asp:CheckBox ID="cbObrigatorio" runat="server" Text="Obrigatório" />
                        </td>

                    </tr>

                    <%-- <tr>
                        <td class=" Tam12 fonteTab2 centralizar">
                                  &nbsp;Ativo:
                              </td>
                        <td style="text-align: left; " class="auto-style1">

                            <asp:DropDownList ID="DDAtivo" runat="server" CssClass="fonteTab"
                                Height="17px" Width="51px">
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                            </asp:DropDownList>

                        </td>

                    </tr>--%>

                    <tr>
                        <td class=" espaco" colspan="6"></td>
                    </tr>
                    <tr>
                        <td class=" Tam12 fonteTab2" style="text-align: center; vertical-align: top">Descrição:</td>
                        <td class=" fonteTab" colspan="5">
                            <asp:TextBox ID="txtDescricao" runat="server" MaxLength="120" class="fonteTexto" BorderStyle="Groove" BorderWidth="1px" TextMode="MultiLine"
                                Width="95%" Height="87px" onkeyup="IsMaxLength(this, 120);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=" Tam12 fonteTab2" style="text-align: center; vertical-align: top">Orientações:</td>
                        <td class=" fonteTab" colspan="5">
                            <asp:TextBox ID="txtOrientacoes" MaxLength="4000" runat="server" class="fonteTexto" BorderStyle="Groove" BorderWidth="1px" TextMode="MultiLine"
                                Width="95%" Height="87px" onkeyup="IsMaxLength(this, 4000);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=" Tam12 fonteTab2" style="text-align: center; vertical-align: top">Orientações Adicionais:</td>
                        <td class=" fonteTab" colspan="5">
                            <asp:TextBox ID="txtOrientacoesAdicional" MaxLength="4000" runat="server" class="fonteTexto" BorderStyle="Groove" BorderWidth="1px" TextMode="MultiLine"
                                Width="95%" Height="87px" onkeyup="IsMaxLength(this, 4000);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:HiddenField ID="HFDocumento" runat="server" />
                            <asp:Button ID="btnSalvar" runat="server" CssClass="btn_novo" Text="Salvar" OnClick="btnSalvar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class=" espaco" colspan="6"></td>
                    </tr>
                </table>

                <br />
                <br />
                <br />

                <br />
            </asp:Panel>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="centralizar">
                <iframe id="I1" class="VisualFrame" name="I1" src="Visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
