<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" EnableEventValidation="false"
    Inherits="ProtocoloAgil.pages.CadastroConceito" CodeBehind="CadastroConceito.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este conceito?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Pedagógico > <span>Cadastro de Conceitos</span></p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="Novo_Click" />
                <asp:Button ID="relatorio" runat="server" CssClass="btn_controls" Text="Relatório" OnClick="relatorio_Click" />
                <asp:Button ID="texto" runat="server" CssClass="btn_controls" Text="Arquivo de texto" OnClick="texto_Click" />
               
            </div>
            <div style="float: right;">
                <asp:TextBox ID="pesquisa" runat="server" CssClass="search_controls" />
                <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                    OnClick="btnpesquisa_Click" />
            </div>
        </div>
    </asp:Panel>
     <div class="formatoTela_02">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
         <br/>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CssClass="list_results" Height="16px" 
                        Style="width: 50%; margin: auto" HorizontalAlign="Center" 
                        ondatabound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
                        onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="ConCodigo" HeaderText="Conceito" SortExpression="ConCodigo">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ConNota" HeaderText="Nota" SortExpression="ConNota">
                                <HeaderStyle Width="35%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Aprova">
                                <ItemTemplate>
                                    <asp:Label ID="LBaprova" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ConAprova").Equals("S")? "Sim" : "Não" %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="20%" />
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                SelectText="Alterar" ShowSelectButton="True">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CommandField>
                             <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMBexcluir"  CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "ConCodigo")%>' 
                                    OnClientClick="javascript:GetConfirm();" onclick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"  runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
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
                    <br />
                    <asp:HiddenField runat="server" ID="HFConfirma" />
                    <asp:HiddenField ID="HFRowCount" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
           
        </asp:View>
        <asp:View ID="View2" runat="server">
        <div id="lightbox"></div>
            <asp:Panel ID="PNinsere" runat="server" CssClass="centralizar" Height="300px" Width="500px"
                DefaultButton="BTinsert">
                <br />
                <br />
                <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo corfonte titulo" colspan="7" style="font-size: large;">
                            <asp:Label ID="LBtituloAlt" runat="server" Text="Cadastro de Conceitos"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td class="espaco" colspan="7">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                     <td class="Tam05" style="text-align: left">
                             &nbsp;</td>
                         <td class="Tam10 fonteTab">
                             &nbsp;&nbsp;Código:</td>
                        <td class="codigo" style="text-align: left">
                            <asp:TextBox ID="TBCodigo" runat="server"  CssClass="fonteTexto"
                                Height="13px" MaxLength="2" Width="80%"></asp:TextBox>
                            </td>
                             
                        <td class="Tam05" style="text-align: left">
                              &nbsp;
                        </td>
                         <td class="Tam10 fonteTab" style="text-align: left">
                            Percentual:</td>
                 
                      <td class="Tam18"  style="text-align: left">
                            <asp:TextBox ID="TBPercentual" runat="server" CssClass="fonteTexto" onkeyup="formataValor(this,event);"
                               Height="13px" MaxLength="6" Width="80%"></asp:TextBox>
                        </td>
                         <td class="Tam05" style="text-align: left">
                             &nbsp;</td>
                        </tr>
                   
                    <tr>
                     <td class="Tam05" style="text-align: left">
                             &nbsp;</td>
                        <td class="Tam10 fonteTab">
                            &nbsp;&nbsp; Nota:</td>
                        <td class="Tam18" style="text-align: left">
                              <asp:TextBox ID="TB_Nota" runat="server" CssClass="fonteTexto" onkeyup="formataValor(this,event);"
                                Height="13px" MaxLength="6" Width="80%"></asp:TextBox> &nbsp;
                            </td>
                             <td class="Tam05" style="text-align: left">
                              &nbsp;
                        </td>
                      
                        <td class="Tam10 fonteTab">
                            Aprova:</td>
                         <td class="Tam18 fonteTab" >
                               <asp:DropDownList ID="DDaprova" runat="server" CssClass="fonteTexto"  Height="18px" onkeydown="ModifyEnterKeyPressAsTab();" Width="80%">
                                  <asp:ListItem Value="">Selecione</asp:ListItem>
                                  <asp:ListItem Value="S">Sim</asp:ListItem>
                                  <asp:ListItem Value="N">Não</asp:ListItem>
                              </asp:DropDownList>

                        </td>
                        <td  style="text-align: left">
                            &nbsp;</td>
                        </tr>

                    <tr>
                        <td class="espaco" colspan="7">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <div class="controls" style="text-align: center;">
                    <asp:Button ID="BTinsert" runat="server" OnClick="BTaltera_Click" Text="Salvar" CssClass="btn_novo" />
                    &nbsp;&nbsp;
                        <asp:Button ID="BTLimpar" runat="server" OnClick="BTlimpar_Click" Text="Limpar" CssClass="btn_novo" />
                </div>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="centralizar">
                <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1">
                </iframe>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>
</asp:Content>