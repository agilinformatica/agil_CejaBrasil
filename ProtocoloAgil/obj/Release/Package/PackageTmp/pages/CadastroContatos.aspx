<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" EnableEventValidation="false"
    Inherits="ProtocoloAgil.pages.CadastroContatos" CodeBehind="CadastroContatos.aspx.cs" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function setSessionVariable(valueToSetTo) {
            window.__doPostBack('SetSessionVariable', valueToSetTo);
        }

        function change(href) {
            window.location.href = href;
        }

        function popup(url) {
            $("#lightbox").css("display", "inline");


            var x = (screen.width - 600) / 2;
            var y = (screen.height - 450) / 2;
            var newwindow;
            newwindow = window.open(url, "Cadastro", "status=no, scrollbar=1, width=600,height= 450,resizable = 1,top=" + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este contato?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="Table">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de contatos</span>
            </p>
        </div>
        <div class="linha">
            <div style="float: left;">
                <asp:Button ID="btnVoltarCliente" runat="server" CssClass="btn_controls" Text="Voltar" OnClick="btnVoltarCliente_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="Novo_Click" />


            </div>
            <div style="float: left;">
                <table>
                    <tr>
                      
                        <td>
                            <span class="fonteTab">Cliente</span>
                        </td>
                    </tr>
                    <tr>
                       
                        <td>
                              <asp:TextBox ID="txtClientePesquisa" Enabled="false" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="100" Width="90%"></asp:TextBox>
                        </td>
                        <td>

                            <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                                OnClick="btnpesquisa_Click" />
                        </td>
                    </tr>
                </table>

        </div>
        <br />
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="list_results" Height="16px"
                            Style="width: 75%; margin: auto" HorizontalAlign="Center"
                            OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                            OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="CocID" HeaderText="Cod" SortExpression="CocID">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                 <asp:BoundField DataField="Tco_Descricao" HeaderText="Tipo" SortExpression="CocTipo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>


                                <asp:BoundField DataField="CocDataContato" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Contato" SortExpression="CocDataContato">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                  <asp:BoundField DataField="UsuNome" HeaderText="Usuário" SortExpression="UsuNome">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CocDescricaocontato" HeaderText="Descrição" SortExpression="CocDescricaocontato">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>


                                

                                <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                    SelectText="Alterar" ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
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


                <br />
                <br />
                <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo corfonte titulo" colspan="5" style="font-size: large;">Cadastro do Contato
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">Cod contato</td>
                        <td class="fonteTab">Cliente</td>
                    </tr>
                    <tr>
                        <td>
                             <asp:TextBox ID="txtCodContato" Enabled="false" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="100" Width="90%"></asp:TextBox>
                        </td>
                         <td>
                             <asp:DropDownList ID="DDCliente" runat="server" CssClass="fonteTexto"
                                DataTextField="CacNome" Enabled="false" DataValueField="CacCodigo" AutoPostBack="true" OnSelectedIndexChanged="DDCliente_SelectedIndexChanged" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="55%"  ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">Atendente</td>
                        <td class="fonteTab">Tipo Contato</td>
                    </tr>
                    <tr>
                       <td>
                           <asp:TextBox ID="txtAtendente" Enabled="false" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="100"  Width="90px"></asp:TextBox>
                       </td>
                        <td>
                             <asp:DropDownList ID="DDTipoContato" runat="server" CssClass="fonteTexto"
                                DataTextField="Tco_Descricao" DataValueField="Tco_Codigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="55%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                       
                    </tr>
                    <tr>
                        <td class="fonteTab">Data Contato</td>
                        <td class="fonteTab">Data Fechamento</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDataContato" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus3"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataContato">
                            </cc2:CalendarExtenderPlus>

                        </td>

                         <td>
                            <asp:TextBox ID="txtDataFechamento" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataFechamento">
                            </cc2:CalendarExtenderPlus>

                        </td>
                    </tr>

                    <tr>
                        <td class="fonteTab">Posição</td>
                       <td class="fonteTab">Usuário Fechamento</td>
                    </tr>
                    <tr>

                          <td>
                             <asp:DropDownList ID="DDPosicao" runat="server" CssClass="fonteTexto"
                                DataTextField="FechDescricao" DataValueField="FechCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="85%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>

                          <td>
                             <asp:DropDownList ID="DDUsuarioFechamento" runat="server" CssClass="fonteTexto"
                                DataTextField="UsuNome" DataValueField="UsuCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="55%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="fonteTab">Data retorno</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDataRetorno" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus2"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataRetorno">
                            </cc2:CalendarExtenderPlus>

                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">Descrição do contato</td>
                    </tr>
                    <tr>
                         <td colspan="2">
                            <asp:TextBox ID="txtDescricaoContato" TextMode="MultiLine"  runat="server" CssClass="fonteTexto"
                                Height="43px" MaxLength="100" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td class="fonteTab">Resultado Contato</td>
                    </tr>
                    <tr>
                         <td colspan="2">
                            <asp:TextBox ID="txtResultadoContato" TextMode="MultiLine"  runat="server" CssClass="fonteTexto"
                                Height="43px" MaxLength="100" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                <br />
                <div class="controls" style="text-align: center;">
                    <asp:Button ID="BTinsert" runat="server" OnClick="BTaltera_Click" Text="Salvar" CssClass="btn_novo" />
                    &nbsp;&nbsp;
                        
                </div>

            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="centralizar">
                    <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
