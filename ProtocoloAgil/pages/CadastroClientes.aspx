<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" EnableEventValidation="false"
    Inherits="ProtocoloAgil.pages.CadastroClientes" CodeBehind="CadastroClientes.aspx.cs" %>

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
            if (confirm("Deseja realmente excluir este cliente?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

    </script>

    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="Table">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de cliente</span>
            </p>
        </div>
        <div class="linha">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="Novo_Click" />


            </div>
            <div style="float: right;">
                <table>
                    <tr>
                        <td>
                            <span class="fonteTab">Nome</span>
                        </td>
                        <td>
                            <span class="fonteTab">Atendente</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="pesquisa" runat="server" CssClass="search_controls" />

                        </td>
                        <td>
                            <asp:DropDownList ID="DDAtendentePesquisa" runat="server" CssClass="fonteTexto"
                                DataTextField="UsuNome" DataValueField="UsuCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="85%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                        <td>

                            <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                                OnClick="btnpesquisa_Click" />
                        </td>
                    </tr>
                </table>





            </div>
        </div>
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
                            OnDataBound="GridView_DataBound" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                            OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="cacCodigo" HeaderText="Código" SortExpression="cacCodigo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cacNome" HeaderText="Nome" SortExpression="cacNome">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <HeaderStyle Width="60%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="StcDescricao" HeaderText="Status" SortExpression="StcDescricao">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Cor">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="StcCodigo" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"></asp:BoundField>


                                <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                    SelectText="Alterar" ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Cont.">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnContato" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "cacCodigo")%>'
                                            OnClick="btnContato_Click" ImageUrl="~/images/detalhes_icone.gif" runat="server" />
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


                <br />
                <br />
                <table class="FundoPainel" style="margin-left: 20px; width: 95%;">
                    <tr>
                        <td class="cortitulo corfonte titulo" colspan="5" style="font-size: large;">Cadastro do cliente
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">cod. Cliente</td>
                        <td class="fonteTab">Nome</td>
                        <%--  <td class="fonteTab">Identificação</td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCodCliente" Enabled="false" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="5" Width="20%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNomeCliente" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="80" Width="90%"></asp:TextBox>
                        </td>
                        <%--   <td>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="fonteTexto"
                                Height="13px" Width="80%"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <%-- <tr>
                        <td class="fonteTab">Cep</td>
                        <td class="fonteTab">Endereço</td>
                        <td class="fonteTab">Número</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCEP" runat="server" AutoPostBack="true" CssClass="fonteTexto" Height="13px"
                                OnTextChanged="txtCEP_TextChanged" Width="30%"
                                MaxLength="8"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndereco" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="100" Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumeroEndereco" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="6" Width="50%"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <%--   <td class="fonteTab">Bairro</td>--%>
                        <td class="fonteTab">Cidade</td>
                        <td class="fonteTab">Estado</td>

                    </tr>
                    <tr>
                        <%--  <td>
                            <asp:TextBox ID="txtBairro" runat="server" AutoPostBack="true" CssClass="fonteTexto" Height="13px"
                                Width="90%"
                                MaxLength="50"></asp:TextBox>
                        </td>--%>
                        <td>
                            <asp:TextBox ID="txtMunicipio" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDEstado" runat="server" CssClass="fonteTexto"
                                DataTextField="MunIEstado" DataValueField="MunIEstado" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="15%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <%--  <td class="fonteTab">Complemento</td>--%>
                        <td class="fonteTab">E-mail</td>
                    </tr>
                    <tr>
                        <%--<td>
                            <asp:TextBox ID="txtComplemento" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="20" Width="80%"></asp:TextBox>
                        </td>--%>
                        <td colspan="2">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="70%" MaxLength="80"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="fonteTab">Telefone</td>
                        <td class="fonteTab">Celular</td>
                        <%-- <td class="fonteTab">Tipo</td>--%>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox ID="txtTelefone" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="14" onkeyup="formataTelefone(this,event);"
                                Width="50%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCelular" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="15" onkeyup="formataTelefoneSaoPaulo(this,event);"
                                Width="50%"></asp:TextBox>
                        </td>
                        <%-- <td>
                            <asp:DropDownList  runat="server" ID="DDTipo" CssClass="fonteTab">
                                <asp:ListItem  Selected="True" Value="2">Prospect</asp:ListItem>
                                
                            </asp:DropDownList>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="fonteTab">Concorrente</td>
                        <td class="fonteTab">Valor Taxa</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtConcorrente" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="90%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtValorTaxa" onkeyup="formataValor(this,event);" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="40%" MaxLength="80"></asp:TextBox>
                        </td>



                    </tr>

                    <tr>
                        <td class="fonteTab">Quantidade Aprendiz</td>
                        <td class="fonteTab">Responsável RH </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtQuantidadeAprendiz" onkeyup="formataInteiro(this,event);" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="10%" MaxLength="80"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtResponsavelRH" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="90%" MaxLength="80"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="fonteTab">Data Vencimento
                            <br />
                            Contrato Atual</td>
                        <td class="fonteTab">Valor Economia </td>

                    </tr>
                    <tr>
                        <td>

                            <asp:TextBox ID="txtDataVencimentoCOntratoAtual" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus2"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataVencimentoCOntratoAtual">
                            </cc2:CalendarExtenderPlus>
                        </td>

                        <td>
                            <asp:TextBox ID="txtValorEconomia" onkeyup="formataValor(this,event);" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="40%" MaxLength="80"></asp:TextBox>
                        </td>


                    </tr>

                    <tr>
                        <td class="fonteTab">Status</td>
                        <td class="fonteTab">Atendente</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DDStatus" runat="server" CssClass="fonteTexto"
                                DataTextField="StcDescricao" DataValueField="StcCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="85%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>

                        <td>

                            <asp:DropDownList ID="DDAtendente" runat="server" CssClass="fonteTexto"
                                DataTextField="UsuNome" DataValueField="UsuCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="85%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>


                        </td>
                    </tr>

                    <tr>

                        <%--   <td class="fonteTab">Data de nascimento</td>--%>
                    </tr>
                    <tr>

                        <%--  <td>
                            <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus2"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataNascimento">
                            </cc2:CalendarExtenderPlus>

                        </td>--%>
                    </tr>

                    <tr>
                        <td class="fonteTab">Data Cadastro</td>
                        <td class="fonteTab">Usuário Cadastro</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDataCadastro" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataCadastro">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td>

                            <asp:DropDownList ID="DDUsuarioCadastro" runat="server" CssClass="fonteTexto"
                                DataTextField="UsuNome" DataValueField="UsuCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="85%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>

                        </td>
                    </tr>

                    <tr>
                        <td class="fonteTab">Data Alteração</td>
                        <td class="fonteTab">Usuário Alteração</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDataAlteracao" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataAlteracao">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDUsuarioAlteracao" runat="server" CssClass="fonteTexto"
                                DataTextField="UsuNome" DataValueField="UsuCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="85%" OnDataBound="IndiceZero" ViewStateMode="Enabled">
                            </asp:DropDownList>

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
