<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="CadastroEscolas.aspx.cs" Inherits="ProtocoloAgil.pages.CadastroEscola" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50731.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir esta escola?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel8" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de Escolas</span></p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Escolas" OnClick="listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text=" Nova Escola" OnClick="Incluir_Click" />
                <asp:Button ID="btn_relatorio" runat="server" CssClass="btn_controls" Text="Relatório" onclick="btn_relatorio_Click" />
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
                <div class="text_titulo" style="float: none;">
                    <h1>
                        Escolas Cadastradas
                    </h1>
                </div>
                <br />
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" OnDataBound="GridView_DataBound" Style="width: 95%; margin: auto" DataKeyNames="EscCodigo" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                    OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="EscCodigo" HeaderText="Código" SortExpression="EscCodigo"
                            ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="Escnome" HeaderText="Escola" SortExpression="Escnome">
                        </asp:BoundField>
                        <asp:BoundField DataField="EscEndereco" HeaderText="Endereço" SortExpression="EscEndereco" />
                        <asp:BoundField DataField="EscNumeroendereco" HeaderText="Nº" SortExpression="EscNumeroendereco" />
                        <asp:BoundField DataField="EscBairro" HeaderText="Bairro" SortExpression="EscBairro" />
                        <asp:BoundField DataField="Esccidade" HeaderText="Cidade" SortExpression="Esccidade" />
                        <asp:BoundField DataField="EscEstado" HeaderText="Estado" SortExpression="EscEstado" />
                        <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                            ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EscCodigo")%>'
                                    OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"
                                    runat="server" />
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
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div style="width: 80%;  margin: 0 auto;">
                <br />
                <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo titulo corfonte" colspan="4" style="font-size: large">
                            Dados da Escola
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam15" style="text-align: left;">
                            &nbsp;&nbsp;&nbsp; Código da Escola
                        </td>
                        <td class="fonteTab Tam40" style="text-align: left;">
                          &nbsp;&nbsp; Descrição da Escola
                        </td>
                        <td class="fonteTab Tam14" style="text-align: left;">
                            &nbsp;&nbsp; Cep
                        </td>
                        <td class="fonteTab Tam20" style="text-align: left;">
                           &nbsp;&nbsp; Telefone
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam15" style="text-align: left;">
                            &nbsp;&nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TBCodEscola" runat="server" Height="13px" 
                                MaxLength="4" onkeyup="formataInteiro(this,event);" Enabled="false" Width="50%"
                                CssClass="fonteTexto"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam40" style="text-align: left;">
                            <asp:Label ID="LBnome" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TBnomeEsc" runat="server" Height="13px" 
                                MaxLength="50" Width="90%" CssClass="fonteTexto"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam14">
                            <asp:Label ID="Label5" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TBCep" runat="server"  CssClass="fonteTexto"
                                Height="13px" MaxLength="10" onkeyup="formataCEP(this,event);" Width="80%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam20">
                            <asp:Label ID="Label6" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TBtelefone" runat="server" 
                                CssClass="fonteTexto" Height="13px" MaxLength="14" onkeyup="formataTelefone(this,event);"
                                Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="Table FundoPainel">
                    <tr>
                        <td class="fonteTab " style="width: 25%">
                            &nbsp;&nbsp;&nbsp; Endereço
                        </td>
                        <td class="fonteTab Tam08">
                           &nbsp;&nbsp; Nº
                        </td>
                        <td class="fonteTab Tam15">
                           &nbsp;&nbsp; Complemento
                        </td>
                        <td class="fonteTab Tam20">
                          &nbsp;&nbsp; Bairro
                        </td>
                        <td class="fonteTab Tam05" style="text-align: left;">
                          &nbsp;&nbsp; UF
                        </td>
                        <td class="fonteTab Tam20" >
                          &nbsp;&nbsp; Cidade
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab">
                            &nbsp;&nbsp;<asp:Label ID="LBend" runat="server" Font-Size="11pt" ForeColor="Red"
                                Text="*"></asp:Label>
                            <asp:TextBox ID="TBEndereco" runat="server" Height="13px" 
                                Width="90%" CssClass="fonteTexto"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam05">
                            <asp:Label ID="Label2" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TB_Numero_endereco" runat="server" 
                                CssClass="fonteTexto" Height="13px" Width="70%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam12">
                            &nbsp;
                            <asp:TextBox ID="TB_complemento" runat="server" 
                                MaxLength="30" CssClass="fonteTexto" Height="13px" Width="80%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam12">
                            <asp:Label ID="Label3" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="TB_Bairro" runat="server" 
                                CssClass="fonteTexto" Height="13px" Width="85%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam15">
                           <asp:DropDownList ID="DD_estado_Nat" runat="server" AutoPostBack="true" CssClass="fonteTexto" DataTextField="MunIEstado"
                                DataValueField="MunIEstado" OnSelectedIndexChanged="DD_estado_Nat_SelectedIndexChanged" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                 Width="80%" OnDataBound="IndiceZeroUF"
                                ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                        <td class="fonteTab Tam08" style="text-align: center;">
                            
                             <asp:Label ID="Label4" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
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
                        <td class="fonteTab Tam35" style="text-align: left;">
                            &nbsp;&nbsp;&nbsp; Diretor(a)
                        </td>
                        <td class="fonteTab Tam20">
                            &nbsp; &nbsp; E-mail
                        </td>
                        <td class="fonteTab Tam20">
                            &nbsp;
                        </td>
                        <td class="fonteTab Tam20">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam35" style="text-align: left;">
                          &nbsp;&nbsp;&nbsp;  <asp:TextBox ID="TBrepresentante" CssClass="fonteTexto" runat="server" Height="13px"
                                 Width="90%"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam20" colspan="3">
                            &nbsp;
                            <asp:TextBox ID="TBEmail" runat="server" CssClass="fonteTexto" Height="13px" Width="90%"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div class="fonteTexto" style="float: left;">
                    <p>
                       <b>Obs.:</b> Os campos com (<asp:Label ID="Label10" runat="server" Font-Size="11pt" ForeColor="Red"
                            Text="*"></asp:Label>) indicam dados obrigatórios.
                    </p>
                </div>
                <div class="controls">
                    <div class="centralizar">
                        <asp:Button ID="BTsalva" runat="server" CssClass="btn_novo" OnClick="BTsalva_Click"
                            Text="Salvar" />
                        &nbsp;
                        <asp:Button ID="BTLimpa" runat="server" Text="Limpar" OnClick="BTLimpa_Click" CssClass="btn_novo" />
                    </div>
                </div>
                <br />
            </div>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="centralizar" style="border: none;">
                <iframe id="IFrame3" class="VisualFrame" style="border: none;" name="IFrame2"
                    src="visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>
    <asp:HiddenField runat="server" ID="HFCampos"></asp:HiddenField>
</asp:Content>