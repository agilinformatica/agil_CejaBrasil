<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.CadastroParceiros"
    Culture="auto" UICulture="auto" CodeBehind="CadastroParceiros.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CalendarExtenderPlus" Namespace="AjaxControlToolkitPlus" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ChangeCode(drop) {
            var hidden = document.getElementById('<%# HFChanged.ClientID %>');
            hidden.value = drop.options[drop.selectedIndex].value;
        }


        function GetConfirm() {
            b
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este Parceiro?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function popup(url, width, height) {
            $("#lightbox").css("display", "inline");
            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "ControleAlunos", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }



    </script>

    <style type="text/css">
        .auto-style1 {
            height: 2%;
        }

        .auto-style2 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            font-weight: bold;
            color: #5E5E5E;
            margin-left: 0px;
            width: 54%;
        }

        .auto-style3 {
            width: 54%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel8" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de Parceiros</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Parceiros" OnClick="listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text=" Novo Parceiro" OnClick="Incluir_Click" />
                <asp:Button ID="relatorio" runat="server" CssClass="btn_controls" Text="Relatório" OnClick="relatorio_Click" />
                <asp:Button ID="texto" runat="server" CssClass="btn_controls" Text="Arquivo de texto" OnClick="texto_Click" />
            </div>
        </div>
        <table>
            <tr>
                <td class="fonteTab" style="text-align: left;">Nome ou Nome Fantasia</td>
                <td class="fonteTab" style="text-align: left;">CNPJ
                </td>
                <td class="fonteTab" style="text-align: left;">Cidade
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="pesquisa" runat="server" CssClass="search_controls" />
                </td>
                <td>
                    <asp:TextBox ID="txtCnpj" onkeyup="formataCGC(this,event);" runat="server" CssClass="search_controls" />
                </td>
                <td>
                    <asp:TextBox ID="txtCidade" runat="server" CssClass="search_controls" />
                </td>
                <td>
                    <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                        OnClick="btnpesquisa_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">

                <div class="text_titulo" style="float: none;">
                    <h1>Parceiros Cadastrados
                    </h1>
                </div>
                <br />
                <asp:Panel ID="Panel1" runat="server" Width="500px" Height="300px" CssClass="centralizar"
                    Visible="false">
                    <div class="text_titulo" style="margin-top: 120px">
                        <h1>Não existem parceiros cadastrados.</h1>
                    </div>
                </asp:Panel>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" OnDataBound="GridView_DataBound"
                    Style="width: 97%; margin: auto" DataKeyNames="ParCodigo" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                    OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField ControlStyle-Width="5%" DataField="ParCodigo" HeaderText="Código" ReadOnly="True" SortExpression="ParCodigo">
                            <ControlStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ParNomeFantasia" HeaderText="Nome Fantasia" SortExpression="ParNomeFantasia">
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ParDescricao" HeaderText="Parceiro" SortExpression="ParDescricao" />
                        <asp:BoundField DataField="ParEndereco" HeaderText="Endereço" SortExpression="ParEndereco" />
                        <asp:BoundField DataField="ParNumeroEndereco" HeaderText="Nº" SortExpression="ParNumeroEndereco" />
                        <asp:BoundField DataField="ParBairro" HeaderText="Bairro" SortExpression="ParBairro" />
                        <asp:BoundField DataField="ParCidade" HeaderText="Cidade" SortExpression="ParCidade" />
                        <asp:BoundField DataField="ParEstado" HeaderText="Estado" SortExpression="ParEstado" />
                        <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png" ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ID="IMBexcluir" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ParCodigo")%>' ImageUrl="~/images/icon_remove.png" OnClick="IMBexcluir_Click" OnClientClick="javascript:GetConfirm();" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Convênio">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnConvenio" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ParCodigo")%>' ImageUrl="~/images/cs_print.gif" OnClick="btnConvenio_Click" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                        LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                    <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SDS_EScolas" runat="server" OnSelected="SqlDataSource1_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="SELECT UniCodigo, UniNome, UniEndereco, UniNumeroEndereco, UniBairro, UniCidade, 
                              UniEstado FROM CA_Unidades WHERE (UniCodigo &lt; &gt; 999) ORDER BY UniNome"></asp:SqlDataSource>
                <asp:HiddenField ID="HFRowCount" runat="server" />
                <asp:HiddenField ID="HFEscolaRef" runat="server" />
                <asp:HiddenField ID="HFConfirma" runat="server" />
                <br />

            </asp:View>
            <asp:View ID="View2" runat="server">
                <div style="width: 800px; height: 816px; margin: 0 auto;">
                    <br />
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="cortitulo titulo corfonte" style="font-size: medium" colspan="4">Dados do Parceiro
                            </td>
                        </tr>
                        <tr>
                            <td class="espaco" colspan="4">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="fonteTab" style="text-align: left;">Código
                            </td>
                            <td class="auto-style2" style="text-align: left;">Nome (Razão Social)
                            </td>
                            <td class="fonteTab" style="text-align: left;">Nome Fantasia</td>
                            <td class="auto-style1" style="text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam10" style="text-align: left;">
                                <asp:TextBox ID="TBCodigo" runat="server" Height="13px"
                                    MaxLength="4" onkeyup="formataInteiro(this,event);" ReadOnly="True" Width="60%"
                                    CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab">
                                <asp:TextBox ID="TBDescricao" runat="server" Height="13px" MaxLength="80" Width="97%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab" colspan="1">
                                <asp:TextBox ID="TBnomeParceiro" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="80" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="fonteTab Tam30" style="text-align: left;">Situação</td>
                            <td class="fonteTab" style="text-align: left;">Num. CNPJ </td>
                            <td class="fonteTab" style="text-align: left;">Cota aprendiz </td>
                            <td class="fonteTab" style="text-align: left;">Convênio </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:DropDownList ID="DDSituacao" runat="server" CssClass="fonteTexto" Style="height: 18px; width: 75px">
                                    <asp:ListItem Value="A">Ativo</asp:ListItem>
                                    <asp:ListItem Value="I">Inativo</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab" style="text-align: left;">
                                <asp:TextBox ID="TBcgc" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="18" onkeyup="formataCGC(this,event);" Width="93%"></asp:TextBox>
                            </td>
                            <td class="fonteTab" style="text-align: left;">
                                <asp:TextBox ID="txtCotaAprendiz" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="8" Width="93%"></asp:TextBox>
                            </td>
                            <td class="fonteTab" style="text-align: left;">
                                <asp:TextBox ID="txtConvenio" runat="server" onkeyup="formataInteiro(this, event);" CssClass="fonteTexto" Height="13px" MaxLength="18" Width="93%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab" style="text-align: left;">Cep</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TBCepEndAlu" runat="server" AutoPostBack="true" CssClass="fonteTexto" Height="13px"
                                    OnTextChanged="TBCepEndAlu_TextChanged" Width="95%"
                                    MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="fonteTab Tam30" colspan="2">Endereço
                            </td>
                            <td class="fonteTab Tam08" style="text-align: left;">Nº</td>
                            <td class="fonteTab Tam15">Complemento </td>
                            <td class="fonteTab Tam20">Bairro </td>
                            <td class="fonteTab Tam20">Cidade </td>
                            <td class="fonteTab Tam08" style="text-align: left;">UF </td>
                        </tr>
                        <tr>
                            <td class="fonteTab" colspan="2">
                                <asp:TextBox ID="TBruaEndAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="60" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam05" style="text-align: left;">
                                <asp:TextBox ID="TB_Numero_endereco" runat="server"
                                    MaxLength="6" CssClass="fonteTexto" Height="13px" Width="70%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam10">
                                <asp:TextBox ID="TB_complemento" runat="server"
                                    MaxLength="20" CssClass="fonteTexto" Height="13px" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam12">
                                <asp:TextBox ID="txtBairro" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="60" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam15">
                                <asp:TextBox ID="txtMunicipioEndereco" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="60" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam05" style="text-align: left;">
                                <asp:DropDownList ID="DD_estado_Nat" runat="server" AutoPostBack="true" CssClass="fonteTexto" DataTextField="MunIEstado"
                                    DataValueField="MunIEstado" OnSelectedIndexChanged="DD_estado_Nat_SelectedIndexChanged" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                    Width="80%" OnDataBound="IndiceZeroUF"
                                    ViewStateMode="Enabled">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <table class="Table FundoPainel">
                        <tr>
                            <td class="fonteTab" style="text-align: left;">Telefone </td>
                            <td class="fonteTab" style="text-align: left;">Celular </td>

                            <td class="auto-style1" style="text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="fonteTab" style="text-align: left;">
                                <asp:TextBox ID="TBtelefone" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="14" onkeyup="formataTelefone(this,event);" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab" style="text-align: left;">
                                <asp:TextBox ID="TBCelular" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="15" onkeyup="formataTelefoneSaoPaulo(this,event);" Width="90%"></asp:TextBox>
                            </td>

                            <td class="auto-style1" style="text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="fonteTab" style="text-align: left;">Inscrição Estadual</td>
                            <td class="fonteTab" style="text-align: left;">Inscrição Municipal</td>
                            <td class="fonteTab" style="text-align: left;">&nbsp;</td>
                            <td class="auto-style1" style="text-align: left;"></td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam30" style="text-align: left;">
                                <asp:TextBox ID="TBInscricao" runat="server" Height="13px"
                                    MaxLength="20" Width="90%" CssClass="fonteTexto"></asp:TextBox>
                            </td>

                            <td class="fonteTab Tam30">
                                <asp:TextBox ID="TBInscricaoMunicipal" runat="server" Height="13px"
                                    Width="95%" MaxLength="20" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam30">&nbsp;</td>
                        </tr>
                    </table>

                    <table class="Table FundoPainel">
                        <tr>
                            <td class="fonteTab Tam30" style="text-align: left;">Relacionamento Empresarial</td>
                            <td class="fonteTab Tam15">Taxa</td>
                            <td class="fonteTab Tam15">Data Contrato</td>

                        </tr>
                        <tr>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:DropDownList ID="dd_responsavel_fundacao" runat="server" CssClass="fonteTexto" DataTextField="UsuNome" DataValueField="UsuCodigo" OnDataBound="IndiceZero" Style="height: 18px; width: 95%;">
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab Tam12">
                                <asp:TextBox ID="TBBolsaTrinta" runat="server" Height="13px"
                                    Width="70%" CssClass="fonteTexto" onkeyup="formataValor(this,event);"></asp:TextBox>
                            </td>

                            <%--<td class="fonteTab Tam15">
                                    <asp:TextBox ID="txtDataContrato" runat="server" CssClass="fonteTexto centralizar" Height="16px"
                                        MaxLength="10"
                                        onkeyup="formataData(this,event);" Width="60%"></asp:TextBox>
                                    <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server"
                                        Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataContrato">
                                    </cc2:CalendarExtenderPlus>
                                </td>--%>


                            <td class="fonteTab Tam15" style="text-align: left;">
                                <asp:TextBox ID="txtDataContrato" runat="server" CssClass="fonteTexto" Height="16px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                    MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                                <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                                    PopupPosition="BottomRight" runat="server" TargetControlID="txtDataContrato">
                                </cc2:CalendarExtenderPlus>
                            </td>


                        </tr>
                        <tr>
                            <td class="fonteTab Tam30" style="text-align: left;">Representante &nbsp;Legal</td>
                            <td class="fonteTab Tam30">E-mail Representante &nbsp;Legal</td>
                            <td class="fonteTab ">Cargo Representate Legal</td>
                        </tr>
                        <tr>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:TextBox ID="TBrepresentante" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="TBEmailRepresentanteLegal" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="TBCargoRepresentanteLegal" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="40" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam30">Data de Aniversário Representante Legal</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDataAniversarioRepresentanteLegal" runat="server" CssClass="fonteTexto centralizar" Height="18px"
                                    MaxLength="10"
                                    onkeyup="formataData(this,event);" Width="50%"></asp:TextBox>
                                <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server"
                                    Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataAniversarioRepresentanteLegal">
                                </cc2:CalendarExtenderPlus>
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam30" style="text-align: left;">Gestor do Programa</td>
                            <td class="fonteTab Tam30">E-mail Gestor do Programa</td>
                            <td class="fonteTab ">Data de Aniversário Gestor do Programa</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam30" style="text-align: left;">
                                <asp:TextBox ID="TBGestorPrograma" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam30">
                                <asp:TextBox ID="TBEmailGestorPrograma" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>

                            <td>
                                <asp:TextBox ID="txtDataAniversarioGestorPrograma" runat="server" CssClass="fonteTexto centralizar" Height="18px"
                                    MaxLength="10"
                                    onkeyup="formataData(this,event);" Width="90%"></asp:TextBox>
                                <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus1" runat="server"
                                    Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataAniversarioGestorPrograma">
                                </cc2:CalendarExtenderPlus>
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam30" style="text-align: left;">Gestor Financeiro</td>
                            <td class="fonteTab Tam30">E-mail Gestor Financeiro</td>
                            <td class="fonteTab ">Data de Aniversário Gestor Financeiro</td>
                        </tr>
                        <tr>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:TextBox ID="TBGestorFinanceiro" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="TBEmailGestorFinanceiro" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataAniversarioGestorFinanceiro" runat="server" CssClass="fonteTexto centralizar" Height="18px"
                                    MaxLength="10"
                                    onkeyup="formataData(this,event);" Width="90%"></asp:TextBox>
                                <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus3" runat="server"
                                    Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataAniversarioGestorFinanceiro">
                                </cc2:CalendarExtenderPlus>
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab2" style="text-align: left;">Gestor de RH</td>
                            <td class="fonteTab">E-mail Gestor de RH</td>
                            <td class="fonteTab"> Data Aniversário do Gestor RH</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam30" style="text-align: left;">
                                <asp:TextBox ID="TBGestorRH" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam30">
                                <asp:TextBox ID="TBEmailGestorRH" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>
                             <td>
                                <asp:TextBox ID="txtDataAniversarioGestorRH" runat="server" CssClass="fonteTexto centralizar" Height="18px"
                                    MaxLength="10"
                                    onkeyup="formataData(this,event);" Width="90%"></asp:TextBox>
                                <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus4" runat="server"
                                    Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataAniversarioGestorRH">
                                </cc2:CalendarExtenderPlus>
                            </td>
                        </tr>
                    </table>


                    <table class="Table FundoPainel">
                        <tr>

                            <td class="fonteTab Tam40" style="text-align: left;">E-Mail Institucional 
                            </td>
                            <td class="fonteTab Tam40">Site </td>
                            <td class="fonteTab Tam40">Ramo de Atuação 
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam20">
                                <asp:TextBox ID="TB_email_resp" runat="server" Height="13px"
                                    MaxLength="80" Width="95%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam20">
                                <asp:TextBox ID="TB_site_escola" runat="server" Height="13px"
                                    MaxLength="80" Width="92%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab">
                                <asp:DropDownList ID="DDramoAtuacao" runat="server" DataTextField="RatDescricao"
                                    DataValueField="RatCodigo" CssClass="fonteTexto" OnDataBound="IndiceZero" Style="height: 18px; width: 95%;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab" colspan="3">Benefícios</td>
                        </tr>
                        <tr>
                            <td class="fonteTab" colspan="3">
                                <asp:TextBox ID="txtBeneficios" TextMode="MultiLine" Rows="3" runat="server" Height="43px"
                                    Width="92%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab" colspan="3">Observações</td>
                        </tr>
                        <tr>
                            <td class="fonteTab" colspan="3">
                                <asp:TextBox ID="txtObservacao" TextMode="MultiLine" Rows="3" runat="server" Height="43px"
                                    Width="92%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="controls">
                        <div class="centralizar">
                            <asp:Button ID="BTsalva" runat="server" CssClass="btn_novo" OnClick="BTsalva_Click"
                                Text="Confirmar" OnClientClick="javascript:CreateWheel('yes');" />
                            &nbsp;
                        <asp:Button ID="BTLimpa" runat="server" Text="Limpar" OnClick="BTLimpa_Click" CssClass="btn_novo" />
                        </div>
                    </div>
                    <br />
                    <asp:HiddenField ID="HFCurrent" runat="server" />
                    <asp:HiddenField ID="HFChanged" runat="server" />
                </div>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="centralizar">
                    <iframe id="IFrame1" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:HiddenField runat="server" ID="HFCampos"></asp:HiddenField>
</asp:Content>
