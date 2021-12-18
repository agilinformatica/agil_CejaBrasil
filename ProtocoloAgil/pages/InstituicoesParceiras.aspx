<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.InstituicoesParceiras"
    Culture="auto" UICulture="auto" CodeBehind="InstituicoesParceiras.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir esta Instituição Parceira?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel8" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de Instituições Parceiras</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Instituições Parceiras"
                    OnClick="listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text=" Nova Instituição Parceira" OnClick="Incluir_Click" />
            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="text_titulo" style="float: none;">
                    <h1>Instituições Parceiras Cadastradas
                    </h1>
                </div>
                <br />
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" OnDataBound="GridView_DataBound"
                    Style="width: 95%; margin: auto" DataKeyNames="IpaCodigo" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="IpaCodigo" HeaderText="Código"
                            SortExpression="IpaCodigo" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="IpaDescricao" HeaderText="Instituição Parceira"
                            SortExpression="IpaDescricao"></asp:BoundField>
                        <asp:BoundField DataField="IpaEndereco" HeaderText="Endereço"
                            SortExpression="IpaEndereco" />
                        <asp:BoundField DataField="IpaNumeroEndereco" HeaderText="Nº"
                            SortExpression="IpaNumeroEndereco" />
                        <asp:BoundField DataField="IpaBairro" HeaderText="Bairro"
                            SortExpression="IpaBairro" />
                        <asp:BoundField DataField="IpaCidade" HeaderText="Cidade"
                            SortExpression="IpaCidade" />
                        <asp:BoundField DataField="IpaEstado" HeaderText="Estado"
                            SortExpression="IpaEstado" />
                        <asp:CommandField ButtonType="Image" HeaderText="Alterar"
                            SelectImageUrl="~/images/icon_edit.png" ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IpaCodigo")%>'
                                    OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png" runat="server" />
                            </ItemTemplate>
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
                <asp:HiddenField ID="HFRowCount" runat="server" />
                <asp:HiddenField ID="HFEscolaRef" runat="server" />
                <asp:HiddenField runat="server" ID="HFConfirma" />
                <br />
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div style="width: 90%; margin: 0 auto;">
                    <br />
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="cortitulo titulo corfonte" colspan="4">Dados da Instituição Parceira</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;&nbsp; Código da Instituição Parceira</td>
                            <td class="fonteTab Tam40" style="text-align: left;">Descrição da Instituição Parceira</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;
                                <asp:TextBox ID="txtCodigoInstituicao" runat="server" Height="13px" MaxLength="4" onkeyup="formataInteiro(this,event);"
                                    Enabled="false" Width="50%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam40" style="text-align: left;">
                                <asp:Label ID="Label3" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                                <asp:TextBox ID="txtDescricaoInstituicaoParceira"  runat="server" Height="13px" MaxLength="80"
                                    Width="90%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            
                        </tr>
                    </table>
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="fonteTab Tam25" colspan="2">&nbsp;&nbsp;&nbsp; Telefone</td>
                            <td class="fonteTab Tam10">Celular</td>
                            <td class="fonteTab Tam12">&nbsp;</td>
                            <td class="fonteTab Tam15">E-Mail Responsável</td>
                            <td class="fonteTab Tam05">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam25" colspan="2">&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtTelefone" runat="server" Height="13px"
                                    Width="80%" CssClass="fonteTexto" MaxLength="14" onkeyup="formataTelefone(this,event);"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam10" colspan="2">
                                <asp:TextBox ID="txtCelular" runat="server" Height="13px"
                                    Width="80%" CssClass="fonteTexto" MaxLength="15" onkeyup="formataTelefoneSaoPaulo(this,event);"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="80" Width="100%"></asp:TextBox>
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="fonteTab Tam25">&nbsp;&nbsp;&nbsp; Endereço </td>
                            <td class="fonteTab Tam05">&nbsp;&nbsp; Nº </td>
                            <td class="fonteTab Tam10">&nbsp; Complemento </td>
                            <td class="fonteTab Tam12">&nbsp;&nbsp; Bairro </td>
                            <td class="fonteTab Tam05">&nbsp; UF </td>
                            <td class="fonteTab Tam15">&nbsp;&nbsp; Cidade </td>
                        </tr>
                        <tr>
                            <td class="fonteTab">&nbsp;&nbsp;
                                <asp:TextBox ID="txtEndereco" runat="server" CssClass="fonteTexto"  Height="13px" MaxLength="100" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam05">
                                
                                <asp:TextBox ID="txtNumeroEndereco" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="6" Width="70%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam10">&nbsp;
                                <asp:TextBox ID="txtComplemento" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="30" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam12">
                                
                                <asp:TextBox ID="txtBairro" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="30" Width="88%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam05">
                                <asp:DropDownList ID="DD_estado_Nat" runat="server" AutoPostBack="true" CssClass="fonteTexto" DataTextField="MunIEstado"
                                    DataValueField="MunIEstado" OnSelectedIndexChanged="DD_estado_Nat_SelectedIndexChanged" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                    Width="80%" OnDataBound="IndiceZeroUF"
                                    ViewStateMode="Enabled">
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab Tam10">
                                <asp:DropDownList ID="DDMunicipio" runat="server" CssClass="fonteTexto" DataTextField="MunIDescricao" DataValueField="MunIDescricao"
                                    Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                    Width="85%" OnDataBound="IndiceZeroUF"
                                    ViewStateMode="Enabled">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="fonteTab Tam10">&nbsp;&nbsp;&nbsp;Cep </td>
                            <td class="fonteTab Tam40">&nbsp;Nome Contato</td>
                            <td class="fonteTab Tam40">&nbsp;Senha</td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam15">
                               &nbsp;&nbsp;
                                <asp:TextBox ID="txtCep" runat="server" Height="13px"
                                    Width="100px" CssClass="fonteTexto" MaxLength="10" onkeyup="formataCEP(this,event);"></asp:TextBox>
                            </td>
                             <td >
                                <asp:TextBox ID="txtNomeContato" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam20">&nbsp; 
                                <asp:TextBox ID="txtSenha" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="8" Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="fonteTexto" style="float: left;">
                        <p>
                            <b>Obs.:</b> Os campos com (<asp:Label ID="Label10" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>) indicam dados obrigatórios.
                        </p>
                    </div>
                    <div class="controls">
                        <div class="centralizar">
                            <asp:Button ID="BTsalva" runat="server" CssClass="btn_novo" OnClick="BTsalva_Click" Text="Salvar" />
                            &nbsp;
                        <asp:Button ID="BTLimpa" runat="server" Text="Limpar" OnClick="BTLimpa_Click" CssClass="btn_novo" />
                        </div>
                    </div>
                    <br />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:HiddenField runat="server" ID="HFCampos"></asp:HiddenField>
</asp:Content>
