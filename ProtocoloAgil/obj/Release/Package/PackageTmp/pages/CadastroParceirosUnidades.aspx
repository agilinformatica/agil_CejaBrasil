<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.CadastroParceirosUnidades"
    Culture="auto" UICulture="auto" CodeBehind="CadastroParceirosUnidades.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/spin.js" type="text/javascript"></script>

    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir esta unidade do parceiro?") == true)
                hf.value = "true";
            else
                hf.value = "false";


            function popup(url, width, height) {
                $("#lightbox").css("display", "inline");
                var x = (screen.width - eval(width)) / 2;
                var y = (screen.height - eval(height)) / 2;
                var newwindow = window.open(url, "ControleAlunos", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
                if (window.focus) { newwindow.focus(); }
            }



        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 56%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel8" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de Unidades do Parceiro</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Unidades" OnClick="listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text=" Nova Unidade"
                    OnClick="Incluir_Click" />
            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">

                <div class="text_titulo" style="float: none;">
                    <h1>Unidades Cadastradas por Parceiro
                    </h1>
                </div>
                <br />
                <div class="controls FundoPainel" style="border: solid 1px black;">
                    <div style="float: right; margin-left: 20px;">
                        <span class="fonteTab">Nome do Parceiro: </span>&nbsp; 
                   &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DD_tipo" runat="server" CssClass="fonteTexto"
                                AutoPostBack="true" Height="18px" Width="230px" DataTextField="ParNomeFantasia" DataValueField="Parcodigo" OnDataBound="IndiceZero"
                                OnSelectedIndexChanged="DD_tipo_SelectedIndexChanged">
                            </asp:DropDownList>
                    </div>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <asp:Panel ID="Panel1" runat="server" Width="500px" Height="200px" CssClass="centralizar"
                            Visible="false">
                            <div class="text_titulo" style="margin-top: 60px;">
                                <h1>Não existem unidades cadastradas para o parceiro.</h1>
                            </div>
                        </asp:Panel>
                        <div class="formatoTela_02">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" PageSize="8"
                                CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" OnDataBound="GridView_DataBound"
                                Style="width: 97%; margin: auto" DataKeyNames="ParUniCodigo"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="ParUniCodigo" HeaderText="Código" SortExpression="ParUniCodigo"
                                        ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Parceiro"
                                        SortExpression="ParUniDescricao"></asp:BoundField>
                                    <asp:BoundField DataField="ParUniEndereco" HeaderText="Endereço" SortExpression="ParUniEndereco" />
                                    <asp:BoundField DataField="ParUniNumeroEndereco" HeaderText="Nº" SortExpression="ParUniNumeroEndereco" />
                                    <asp:BoundField DataField="ParUniBairro" HeaderText="Bairro" SortExpression="ParUniBairro" />
                                    <asp:BoundField DataField="ParUniCidade" HeaderText="Cidade" SortExpression="ParUniCidade" />
                                    <asp:BoundField DataField="ParUniEstado" HeaderText="Estado" SortExpression="ParUniEstado" />
                                    <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                        ShowSelectButton="True" />
                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ParUniCodigo")%>'
                                                OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle Width="10%" />
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
                            <asp:HiddenField runat="server" ID="HFConfirma" />
                            <asp:HiddenField ID="HFEscolaRef" runat="server" />
                        </div>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:View>
            <asp:View ID="View2" runat="server">
                <div style="width: 80%; height: 400px; margin: 0 auto;">
                    <br />
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="cortitulo titulo corfonte" style="font-size: medium" colspan="4">Dados da Unidade
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam12" style="text-align: left;">&nbsp;&nbsp;&nbsp; Código
                            </td>
                            <td class="fonteTab Tam12" style="text-align: left;">Descrição
                            </td>
                            <td class="fonteTab Tam15" style="text-align: left;">&nbsp;Telefone
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam12" style="text-align: left;">&nbsp;&nbsp;
                            <asp:TextBox ID="TBCodigo" runat="server" Height="13px"
                                MaxLength="4" onkeyup="formataInteiro(this,event);" Enabled="false" Width="50%"
                                CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:TextBox ID="TBDescricao" runat="server" Height="13px"
                                    MaxLength="80" Width="98%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam14">
                                <asp:TextBox ID="TBtelefone" runat="server" Height="13px"
                                    Width="80%" CssClass="fonteTexto" MaxLength="14" onkeyup="formataTelefone(this,event);"></asp:TextBox>
                            </td>
                           
                        </tr>
                        <tr>
                           
                              <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;&nbsp;Num. CNPJ
                            </td>
                             <td  class="fonteTab Tam10" style="text-align: left;">Cep
                            </td>
                        </tr>
                        <tr>

                          
                             <td class="fonteTab Tam10">
                                 &nbsp;&nbsp;
                                <asp:TextBox ID="TBcgc" runat="server" Height="13px"
                                    Width="80%" MaxLength="18" CssClass="fonteTexto" onkeyup="formataCGC(this,event);"></asp:TextBox>
                            </td>
                              <td class="fonteTab Tam15">
                                
                            <asp:TextBox ID="TBCepEndAlu" runat="server" AutoPostBack="true" CssClass="fonteTexto" Height="13px"
                                OnTextChanged="TBCepEndAlu_TextChanged" Width="40%"
                                MaxLength="8"></asp:TextBox>

                            </td>
                        </tr>
                    </table>
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="fonteTab Tam30" colspan="2">&nbsp;&nbsp;&nbsp; Endereço
                            </td>
                            <td class="fonteTab Tam08" style="text-align: left;">Nº</td>
                            <td class="fonteTab Tam12">Complemento </td>
                            <td class="fonteTab Tam15">Bairro </td>
                            <td class="fonteTab Tam08" style="text-align: left;">UF </td>
                            <td class="fonteTab Tam15">Cidade </td>
                        </tr>
                        <tr>
                            <td class="fonteTab" colspan="2">&nbsp;&nbsp;
                            <asp:TextBox ID="TBEndereco" runat="server" Height="13px"
                                MaxLength="100" Width="90%" CssClass="fonteTexto"></asp:TextBox>
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
                                <asp:TextBox ID="TB_Bairro" runat="server"
                                    MaxLength="30" CssClass="fonteTexto" Height="13px" Width="88%"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam8">

                                <asp:DropDownList ID="DD_estado_Nat" runat="server" AutoPostBack="true" CssClass="fonteTexto" DataTextField="MunIEstado"
                                    DataValueField="MunIEstado" OnSelectedIndexChanged="DD_estado_Nat_SelectedIndexChanged" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                    Width="80%" OnDataBound="IndiceZeroUF"
                                    ViewStateMode="Enabled">
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab Tam05" style="text-align: left;">
                                <asp:Label ID="Label4" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                                <asp:TextBox ID="txtMunicipioEndereco" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="60" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <table class="Table FundoPainel">
                        <tr>
                            <td class="fonteTab Tam12">&nbsp;&nbsp; Taxa 22h
                            </td>
                            <td class="fonteTab Tam12">Bolsa 22h
                            </td>
                            <td class="fonteTab Tam12">Taxa 30h
                            </td>
                            <td class="fonteTab Tam12">Bolsa 30h
                            </td>
                            <td class="fonteTab Tam15" style="text-align: left;">&nbsp;Celular
                            </td>
                            <td class="fonteTab Tam30" style="text-align: left;">Representante
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab Tam12">&nbsp;&nbsp;
                            <asp:TextBox ID="TBTaxaVinte" runat="server" Height="13px"
                                Width="70%" CssClass="fonteTexto" onkeyup="formataValor(this,event);"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam12">
                                <asp:TextBox ID="TBBolsaVinte" runat="server" Height="13px"
                                    Width="70%" CssClass="fonteTexto" onkeyup="formataValor(this,event);"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam12">
                                <asp:TextBox ID="TBTaxaTrinta" runat="server" Height="13px"
                                    Width="70%" CssClass="fonteTexto" onkeyup="formataValor(this,event);"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam12">
                                <asp:TextBox ID="TBBolsaTrinta" runat="server" Height="13px"
                                    Width="70%" CssClass="fonteTexto" onkeyup="formataValor(this,event);"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam14">
                                <asp:TextBox ID="TBCelular" runat="server" Height="13px"
                                    Width="80%" CssClass="fonteTexto" MaxLength="15" onkeyup="formataTelefoneSaoPaulo(this,event);"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam30">
                                <asp:TextBox ID="TBrepresentante" runat="server" Height="13px"
                                    Width="95%" MaxLength="50" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="Table FundoPainel">
                        <tr>

                            <td class="fonteTab Tam40">E-Mail
                            </td>
                            <td class="fonteTab Tam40">Nome do Parceiro:</td>
                        </tr>
                        <tr>

                            <td class="fonteTab Tam20">
                                <asp:TextBox ID="TB_email_resp" runat="server" Height="13px"
                                    MaxLength="80" Width="90%" CssClass="fonteTexto"></asp:TextBox>
                            </td>
                            <td class="fonteTab Tam20">

                                <asp:DropDownList ID="DDparceiro" runat="server" CssClass="fonteTexto" OnDataBound="IndiceZero"
                                    DataTextField="ParNomeFantasia" DataValueField="Parcodigo" Height="18px" Width="95%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <div class="controls">
                        <div class="centralizar">
                            <asp:Button ID="BTsalva" runat="server" CssClass="btn_novo" OnClick="BTsalva_Click"
                                Text="Salvar" OnClientClick="javascript:CreateWheel('yes');" />
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
