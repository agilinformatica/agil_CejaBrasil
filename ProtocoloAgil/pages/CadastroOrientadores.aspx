<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.CadastroOrientadores"
    CodeBehind="CadastroOrientadores.aspx.cs" %>
<%@ Import Namespace="ProtocoloAgil.Base" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este orientador?") == true)
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
                Configurações > <span>Cadastro de Orientadores</span></p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="btn_listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="btn_novo_Click" />
                <asp:Button ID="btn_relatorio" runat="server" CssClass="btn_controls" Visible="false" Text="Relatório" OnClick="btn_relatorio_Click" />
                <asp:Button ID="btn_texto" runat="server" CssClass="btn_controls" Text="Arquivo de Texto" OnClick="btn_texto_Click" />
            </div>
        </div>
    </asp:Panel>
     <div class="formatoTela_02">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
        <div style="width: 90%; margin:0 auto;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
                    <br/>
                        <table class="FundoPainel Table centralizar">
                            <tr>
                                  <td class="titulo cortitulo corfonte" colspan="4" style="font-size: medium;">
                                    Parceiros por Unidade - Pesquisa
                                </td>
                            </tr>
                            <tr>
                                <td class="espaco" colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="Tam05 fonteTab">
                                    &nbsp;</td>
                                <td class="Tam30 fonteTab" style="text-align: left;">
                                    &nbsp;&nbsp; Parceiro</td>
                                <td class="Tam40 fonteTab">
                                    &nbsp;&nbsp; Unidade
                                </td>
                                <td class="Tam15" rowspan="2">
                                    &nbsp;
                                    <asp:Button ID="btn_pesquisa" runat="server" CssClass="btn_novo" 
                                        OnClick="Button2_Click" Text="Pesquisar" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Tam05 fonteTab">
                                    &nbsp;
                                </td>
                                <td class="Tam40 fonteTab" style="text-align: left;">
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="DD_parceiro" runat="server" AutoPostBack="true" 
                                        CssClass="fonteTexto"  DataTextField="ParNomeFantasia" 
                                        DataValueField="ParCodigo" OnDataBound="IndiceZero" 
                                        onselectedindexchanged="DD_curso_SelectedIndexChanged" 
                                        style="height: 18px; width:85%;">
                                    </asp:DropDownList>
                                </td>
                                <td class="Tam12 fonteTab">
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="DD_unidades" runat="server" CssClass="fonteTexto" 
                                        DataTextField="ParUniDescricao" DataValueField="ParUniCodigo" 
                                        OnDataBound="IndiceZero" style="height: 18px; width:90%;" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    <br/>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list_results"
                            AutoGenerateColumns="False" OnDataBound="GridView_DataBound"
                            Style="width: 90%; margin: 0 auto;" HorizontalAlign="Center" 
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="OriCodigo" HeaderText="Código" SortExpression="OriCodigo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OriNome" HeaderText="Nome" SortExpression="OriNome">
                                </asp:BoundField>

                                  <asp:TemplateField HeaderText="Telefone">
                                    <ItemTemplate>
                                        <asp:label ID="lb_telefone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OriTelefone") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="15%"></HeaderStyle>
                                </asp:TemplateField>
                                  <asp:BoundField DataField="OriEmail" HeaderText="E-mail" SortExpression="OriEmail">
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/Images/icon_edit.png"
                                    ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Excluir" >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OriCodigo")%>'
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
                        <br/>
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                        <asp:HiddenField runat="server" ID="HFConfirma" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:Panel ID="Panel2" runat="server" Height="300px" Width="100%">
                <br />
                <br />
                <table class="Table FundoPainel" style="width: 90%; margin: 0 auto;">
                    <tr>
                        <td class="cortitulo corfonte titulo" colspan="4" style="font-size: large;">
                            Cadastro de Orientadores
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam10">
                            &nbsp;&nbsp;Código
                        </td>
                        <td class="fonteTab Tam40">
                            &nbsp;&nbsp; Nome</td>
                        <td class="fonteTab Tam15" style="text-align: left;">
                            &nbsp;&nbsp; Telefone</td>
                        <td class="fonteTab" style="text-align: left;">
                           &nbsp;&nbsp; E-mail</td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam10">
                            &nbsp;&nbsp;<asp:TextBox ID="TBcodigo" Style="width: 70%; height: 14px;" MaxLength="3"
                                onkeyup="formataInteiro(this,event);" runat="server"> </asp:TextBox>
                        </td>
                         <td class="fonteTab Tam20">
                         &nbsp;&nbsp;<asp:TextBox ID="TBNome" runat="server" MaxLength="50" CssClass="fonteTexto"
                                 Style="width: 95%; height: 14px;"></asp:TextBox>
                        </td>
                        <td class="fonteTab">
                           &nbsp;&nbsp; <asp:TextBox ID="TB_telefone" runat="server" MaxLength="14" CssClass="fonteTexto"
                                onkeyup="formataTelefone(this,event);" Style="width: 80%; height: 14px;"> </asp:TextBox>
                        </td>
                        <td class="fonteTab">
                           &nbsp;&nbsp; <asp:TextBox ID="tb_email" runat="server" CssClass="fonteTexto" MaxLength="80"  Style="width: 90%; height: 14px;"> </asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    <table class="Table FundoPainel" style="width: 90%; margin: 0 auto;">
                     <tr>
                        <td class="Tam50 fonteTab">
                           &nbsp;&nbsp; Parceiro:
                        </td>
                        <td class="Tam50 fonteTab" >
                           &nbsp;&nbsp; Unidade:
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam50 fonteTab">
                           &nbsp;&nbsp; <asp:DropDownList ID="dd_parceiro_cad" runat="server" CssClass="fonteTexto" AutoPostBack="true"
                                DataTextField="ParNomeFantasia" DataValueField="ParCodigo" Height="16px" OnDataBound="IndiceZero" 
                                Width="87%" OnSelectedIndexChanged="DD_curso_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="Tam50 fonteTab">
                           &nbsp;&nbsp; <asp:DropDownList ID="DDunidade_parceiro" runat="server" CssClass="fonteTexto" DataTextField="ParUniDescricao"
                                DataValueField="ParUniCodigo" Height="16px" OnDataBound="IndiceZero" 
                                Width="90%" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                        &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <div class="controls" style="width: 90%; margin: 0 auto; text-align: center;">
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