<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/MPusers.Master"
    Inherits="ProtocoloAgil.pages.MultiviewUsuarios" CodeBehind="MultiviewUsuarios.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
     function change(href) {
            window.location.href = href;
        }

        function popup(url,width,height) {
            $("#lightbox").css("display", "inline");
            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "Cadastro", "status=1, scrollbar=0, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este usuário ?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Configurações > <span>Cadastro de Usuários</span></p>
    </div>
    <div class="controls">
        <div style="float: left;">
            <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
            <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="Novo_Click" />
            <asp:Button ID="relatorio" runat="server" CssClass="btn_controls" Text="Relatório" Visible="false"
                OnClick="relatorio_Click" />
            <asp:Button ID="texto" runat="server" CssClass="btn_controls" Text="Arquivo de texto" Visible="false"
                OnClick="texto_Click" />
        </div>
        <div style="float: right;">
            <asp:TextBox ID="pesquisa" runat="server" CssClass="search_controls" />
            <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                OnClick="btnpesquisa_Click" />
        </div>
    </div>
    <div class="formatoTela_02">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CaptionAlign="Top"   ondatabound="GridView_DataBound" 
                        HorizontalAlign="Center" OnRowCommand="GridView1_RowCommand" Style="width: 70%;
                        margin: auto" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                       CssClass="list_results" 
                        onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="UsuCodigo" HeaderText="Código" SortExpression="UsuCodigo">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UsuNome" HeaderText="Nome de usuario" SortExpression="UsuNome">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                SelectText="Alterar" ShowSelectButton="True">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CommandField>
                            <asp:ButtonField CommandName="Reset" HeaderText="Resetar Senha" Text="Resetar" ButtonType="Image"
                                ImageUrl="~/images/icon_reset.jpg">
                                <HeaderStyle Width="13%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:ButtonField>
                              <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UsuCodigo")%>'
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
                    <br />
                    <br />
                     <asp:HiddenField runat="server" ID="HFConfirma" />
                    <asp:HiddenField ID="HFRowCount" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div id="lightbox"></div>
            <asp:Panel ID="PNinsere" runat="server" CssClass="centralizar"  Width="75%">
                <br />
                <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo titulo corfonte" colspan="6" style="font-size: large;">
                            <asp:Label ID="LBtituloAlt" runat="server" Text="Cadastro/Alteração de Usuários"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam15 espaco">
                        </td>
                        <td  class="Tam25 espaco">
                        </td>
                        <td  class="Tam10 espaco">
                        </td>
                        <td class="Tam18 espaco" colspan="2">
                         </td>
                         <td class="espaco"></td>
                    </tr>
                    <tr>
                        <td class="fonteTab">
                           &nbsp;&nbsp; Unidade:</td>
                        <td class="fonteTab">
                                     &nbsp;&nbsp;<asp:TextBox ID="TB_codigo_campus" runat="server" BorderStyle="Groove" 
                                         BorderWidth="1px" CssClass="fonteTexto" ForeColor="Black" Height="13px" 
                                         MaxLength="8" Width="70%"></asp:TextBox>

                                   &nbsp;  <asp:ImageButton ID="ImageButton1" runat="server" Width="23px" Height="23px" 
                                ImageUrl="~/images/lupa.png" onclick="ImageButton1_Click" />
                        </td>
                        <td class="fonteTab" colspan="4">
                            &nbsp;<asp:Label ID="LB_campus" runat="server" CssClass="fonteTab" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="fonteTab">
                           &nbsp;&nbsp; Código: 
                        </td>
                        <td class="fonteTab">
                            &nbsp;&nbsp;<asp:TextBox ID="TBCodigo" runat="server" CssClass="fonteTexto" Height="13px"
                                BorderStyle="Groove" BorderWidth="1px" MaxLength="8" Width="70%" ForeColor="Black" 
                                ></asp:TextBox>
                        </td>
                        <td class="fonteTab" colspan="2">
                             &nbsp;&nbsp; Tipo Usuário:</td>
                        <td class="fonteTab" colspan="2">
                            <asp:DropDownList ID="DD_tipo" DataTextField="PerfDescricao" DataValueField="PerfCodigo" runat="server" CssClass="fonteTexto" Height="18px" Width="60%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">
                          &nbsp;&nbsp; Nome:
                        </td>
                        <td class="fonteTab" colspan="5">
                            &nbsp;&nbsp;<asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" Height="13px"
                                BorderStyle="Groove" BorderWidth="1px" MaxLength="90" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">
                          &nbsp;&nbsp; E-mail:</td>
                        <td class="fonteTab" colspan="5">
                            &nbsp;&nbsp;<asp:TextBox ID="TB_email" runat="server" BorderStyle="Groove" BorderWidth="1px" 
                                CssClass="fonteTexto" Height="13px" MaxLength="60" Width="70%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">
                            &nbsp;&nbsp; Senha:
                        </td>
                        <td class="fonteTab" colspan="2">
                            &nbsp;&nbsp;<asp:TextBox ID="TBSenha1" runat="server" CssClass="fonteTexto" Height="13px" 
                                BorderStyle="Groove" BorderWidth="1px"  TextMode="Password" MaxLength="16"  Width="128px"></asp:TextBox>
                        </td>
                        <td class="fonteTab" colspan="2">
                            Confirmar senha:</td>
                        <td class="fonteTab">
                            <asp:TextBox ID="TBSenha2" runat="server" 
                                BorderStyle="Groove" BorderWidth="1px" TextMode="Password" CssClass="fonteTexto" Height="13px" 
                                MaxLength="16" Width="128px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab2">
                            &nbsp;
                        </td>
                        <td class="fonteTab" colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco" colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                </table>
               
                <div class="fonteTab">
                * Caso seja necessário alterar a senha do usuário, utilize a alteração de senha do login ou reset de senha na opção Listar.
                </div>
                <br/>
                <div class="controls">
                    <asp:Button ID="BTinsert" runat="server" OnClick="BTaltera_Click" Text="Confirmar" CssClass="btn_novo"
                        type="reset" />
                    &nbsp;
                    <asp:Button ID="BTLimpar" runat="server" CssClass="btn_novo" OnClick="BTLimpar_Click" Text="Limpar" type="reset" />
                    <asp:SqlDataSource ID="SDSEscolas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                        SelectCommand="Select  EscCodigo,EscNome from MA_Escolas">
                    </asp:SqlDataSource>
                </div>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="centralizar">
                <iframe id="I1" class="VisualFrame" name="I1" src="visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>
</asp:Content>