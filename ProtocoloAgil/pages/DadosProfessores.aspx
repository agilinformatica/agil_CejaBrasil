<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DadosProfessores.aspx.cs"
    Inherits="ProtocoloAgil.pages.DadosProfessores" EnableSessionState="True" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50731.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <%--  <link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Styles/cdl_bh.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ModifyEnterKeyPressAsTab() {
            if (window.event && window.event.keyCode == 13) {
                window.event.keyCode = 9;
            }
        }
    </script>
    <script type="text/javascript">
        function valida_cpf() {
            var numcpf = document.getElementById('<%# TBCPFProf.ClientID %>');
            var hidden = document.getElementById('<%# HFcpf.ClientID %>');
            var p = VerificaCPF(numcpf);
            hidden.value = eval(p.toString());
        }
    </script>
    <style>
        html {
            min-height: 100%;
            background: #fff;
        }

        #all_master html {
            width: 100%;
            margin: auto;
            height: 100%;
            background: #0fff;
        }

        .content {
            width: 100%;
            background: #fff;
            min-height: 250px;
            clear: left;
            padding-bottom: 30px;
            margin-top: -10px;
        }

        #all_master .controls {
            width: 96%;
            height: 30px;
            border: solid 1px #dfdfdf;
            margin: 10px 1% 10px 1%;
            padding: 10px 1% 10px 1%;
            clear: both;
        }

        #all_master .btn_controls {
            height: 30px;
            border: solid 1px #dfdfdf;
            background: #f5f5f5;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #00599c;
            font-weight: bold;
            margin-right: 5px;
            padding: 7px 10px 10px 10px;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="all_master">
            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
                EnableScriptLocalization="true" ScriptMode="Release">
            </ajaxToolkit:ToolkitScriptManager>
            <div class="content">
                <% var exibe = Session["tipo"];
                   if ((string)exibe == "Interno")
                   { %>

                <div class="controls">
                    <div style="float: right;">
                        <asp:Button ID="BTsalvar" runat="server" CssClass="btn_novo" Text="Salvar Alterações"
                            OnClick="BTsalvar_Click" />
                    </div>
                </div>
                <br />
                <% }
                   else
                   { %>
                <br />
                <% } %>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <asp:Panel ID="Panel1" runat="server" CssClass="Table" Height="490px" Width="90%"
                            Style="margin: 0 auto">
                            <table class="Table FundoPainel tabela ">
                                <tr>
                                    <td class="titulo corfonte cortitulo" colspan="5" style="font-size: medium;">Dados do Monitor
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam10 fonteTab">&nbsp;&nbsp; Código
                                    </td>
                                    <td class="fonteTab Tam35">Nome do Monitor
                                    </td>
                                    <td class="Tam18 fonteTab">Naturalidade
                                    </td>
                                    <td class="Tam08 fonteTab">UF
                                    </td>
                                    <td class="Tam15 fonteTab">&nbsp; Estado Civil
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fonteTab">&nbsp;&nbsp;
                                    <asp:TextBox ID="TBcodigoProf" runat="server"
                                        CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();" ReadOnly="True"
                                        Width="70%"></asp:TextBox>
                                    </td>
                                    <td class="fonteTab">
                                        <asp:Label ID="LBnome" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                        <asp:TextBox ID="TBnomeProf" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            MaxLength="50" onblur="if(this.value.length < 10) {alert('Campo Nome do Aluno vazio ou inválido! Certifique-se que o nome possui mais de 10 caracteres. ');}"
                                            Width="93%"></asp:TextBox>
                                    </td>
                                    <td class="fonteTab">&nbsp;
                                    <asp:TextBox ID="TBnaturalidade" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="30" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="90%" AutoCompleteType="DisplayName"></asp:TextBox>
                                        <cc1:AutoCompleteExtender BehaviorID="AutoComplete1" ID="AutoCompleteExtender3" runat="server"
                                            TargetControlID="TBnaturalidade" ServiceMethod="GetMunicipio" MinimumPrefixLength="1"
                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td class=" fonteTab">
                                        <asp:DropDownList ID="DDestado" runat="server" CssClass="fonteTexto" DataSourceID="SqlDataSource1"
                                            DataTextField="MunIEstado" DataValueField="MunIEstado" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="90%" OnDataBound="IndiceZeroUF">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="fonteTab">&nbsp;
                                    <asp:DropDownList ID="DDestCivilProf" runat="server"
                                        CssClass="fonteTexto" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();" Width="85%">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="S">Solteiro</asp:ListItem>
                                        <asp:ListItem Value="C">Casado</asp:ListItem>
                                        <asp:ListItem Value="D">Divorciado</asp:ListItem>
                                        <asp:ListItem Value="V">Viúvo</asp:ListItem>
                                        <asp:ListItem Value="A">Amasiado</asp:ListItem>
                                        <asp:ListItem Value="D">Desquitado</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table class="Table FundoPainel tabela ">
                                <tr>
                                    <td class="Tam12 fonteTab">&nbsp;&nbsp; Data Ingresso
                                    </td>
                                    <td class="Tam12 fonteTab">Data Demissão
                                    </td>
                                    <td class="Tam12 fonteTab">Data Nasc.
                                    </td>
                                    <td class="Tam12 fonteTab">Situação
                                    </td>
                                    <td class="Tam20 fonteTab">Escolaridade
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam12">&nbsp;&nbsp;
                                    <asp:TextBox ID="TBdataEnt" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        MaxLength="10" onkeyup="formataData(this,event);"
                                        Width="70%"></asp:TextBox>
                                        <cc2:CalendarExtenderPlus ID="TBdataNascAlu0_CalendarExtenderPlus" runat="server"
                                            Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="TBdataEnt">
                                        </cc2:CalendarExtenderPlus>
                                    </td>
                                    <td class="Tam12">
                                        <asp:TextBox ID="TBdataSaida" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            MaxLength="10" onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                                        <cc2:CalendarExtenderPlus ID="TBdataNascAlu1_CalendarExtenderPlus" runat="server"
                                            Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="TBdataSaida">
                                        </cc2:CalendarExtenderPlus>
                                    </td>
                                    <td class="Tam12">
                                        <asp:Label ID="LBdataNasc" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                        <asp:TextBox ID="TBdataNascProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            MaxLength="10" onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                                        <cc2:CalendarExtenderPlus ID="TBCalendario_CalendarExtenderPlus" runat="server" Format="dd/MM/yyyy"
                                            PopupPosition="BottomRight" TargetControlID="TBdataNascProf">
                                        </cc2:CalendarExtenderPlus>
                                    </td>
                                    <td class="Tam12">
                                        <asp:DropDownList ID="DDsitProf" runat="server"
                                            CssClass="fonteTexto" Height="18px" OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="90%">
                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                            <asp:ListItem Value="A">Ativo</asp:ListItem>
                                            <asp:ListItem Value="D">Demitido</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="Tam20">
                                        <asp:DropDownList ID="DDEscolaridade" runat="server"
                                            CssClass="fonteTexto" Height="18px" OnDataBound="IndiceZero" DataValueField="GreCodigo"
                                            DataTextField="GreDescricao" onkeydown="ModifyEnterKeyPressAsTab();" Width="95%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table class="Table FundoPainel tabela ">
                                <tr>
                                    <td class="Tam25 fonteTab">&nbsp;&nbsp; Vínculo Empregatício
                                    </td>
                                    <% var exibe = Session["tipo"];
                                       if ((string)exibe == "Interno")
                                       { %>
                                    <td class="Tam15 fonteTab">Senha Acesso
                                    </td>
                                    <% } %>
                                    <td class="Tam15 fonteTab">Sexo
                                    </td>
                                    <td class="Tam50 fonteTab">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam18">&nbsp;&nbsp;
                                    <asp:DropDownList ID="DDTipoProf" runat="server"
                                        CssClass="fonteTexto" Height="18px" OnDataBound="IndiceZero" DataValueField="Vin_Codigo"
                                        DataTextField="Vin_Descricao" onkeydown="ModifyEnterKeyPressAsTab();" Width="85%">
                                    </asp:DropDownList>
                                    </td>
                                    <%  exibe = Session["tipo"];
                                        if ((string)exibe == "Interno")
                                        { %>
                                    <td class="Tam15">
                                        <asp:TextBox ID="TB_senha_acesso" runat="server" MaxLength="255"
                                            CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="85%"></asp:TextBox>
                                    </td>
                                    <% } %>
                                    <td class="Tam15">
                                        <asp:DropDownList ID="DDsexoProf" runat="server"
                                            CssClass="fonteTexto" Height="18px" onkeydown="ModifyEnterKeyPressAsTab();" Width="90%">
                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                                            <asp:ListItem Value="F">Feminino</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="Tam50">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="espaco">&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table class="Table FundoPainel tabela ">
                                <tr>
                                    <td class="titulo corfonte cortitulo" colspan="4" style="font-size: medium;">Documentos
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam12 fonteTab">&nbsp;&nbsp; Número CPF
                                    </td>
                                    <td class="Tam12 fonteTab">Núm Identidade
                                    </td>
                                    <td class="Tam12 fonteTab">Orgão Expedidor
                                    </td>
                                    <td class="Tam20 fonteTab">Município Eleitoral
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam12 ">&nbsp;&nbsp;
                                    <asp:TextBox ID="TBCPFProf" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        MaxLength="14" onkeyup="formataCPF(this,event);"
                                        onblur="valida_cpf();" Width="85%"></asp:TextBox>
                                    </td>
                                    <td class="Tam12 ">
                                        <asp:TextBox ID="TBRGProf" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            MaxLength="15" Width="90%"></asp:TextBox>
                                    </td>
                                    <td class="Tam12 ">
                                        <asp:TextBox ID="TBExped_identidade" runat="server" MaxLength="8"
                                            CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="90%"></asp:TextBox>
                                    </td>
                                    <td class="Tam20 fonteTab">
                                        <asp:TextBox ID="TBmunEleitoral" runat="server" MaxLength="30"
                                            CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="85%"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" BehaviorID="AutoComplete1"
                                            CompletionInterval="1000" CompletionSetCount="20" EnableCaching="true" MinimumPrefixLength="1"
                                            ServiceMethod="GetMunicipio" TargetControlID="TBmunEleitoral">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                            </table>
                            <table class="Table FundoPainel tabela ">
                                <tr>
                                    <td class="Tam20 fonteTab">&nbsp;&nbsp; Carteira Profissional
                                    </td>
                                    <td class="Tam10 fonteTab">Série
                                    </td>
                                    <td class="Tam12 fonteTab">Número Titulo
                                    </td>
                                    <td class="Tam10 fonteTab">Zona
                                    </td>
                                    <td class="Tam05 fonteTab">Seção
                                    </td>
                                    <td class="Tam15 fonteTab">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam20 fonteTab">&nbsp;&nbsp;
                                        <asp:TextBox ID="TBnumCTPSresp" runat="server" CssClass="fonteTexto" Height="13px"
                                            MaxLength="20" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="85%"></asp:TextBox>
                                    </td>
                                    <td class="Tam10">
                                        <asp:TextBox ID="TBserieCTPSResp" runat="server" CssClass="fonteTexto" Height="13px"
                                            MaxLength="5" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="85%"></asp:TextBox>
                                    </td>
                                    <td class="Tam12 ">
                                        <asp:TextBox ID="TBnumTituloProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            MaxLength="12" Width="80%"></asp:TextBox>
                                    </td>
                                    <td class="Tam10 ">
                                        <asp:TextBox ID="TBzonaTitProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            MaxLength="4" Width="80%"></asp:TextBox>
                                    </td>
                                    <td class="Tam05 ">
                                        <asp:TextBox ID="TBsecaoTitProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            MaxLength="4" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="80%"></asp:TextBox>
                                    </td>
                                    <td class="Tam15"></td>
                                </tr>
                            </table>
                            <table class="FundoPainel Table">
                                <tr>
                                    <td class="fonteTab">&nbsp;&nbsp; Observação
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fonteTab">&nbsp;&nbsp;
                                    <asp:TextBox ID="TBobservacao" runat="server" CssClass="fonteTexto" Height="40px"
                                        onkeyup="javascript:IsMaxLength(this, 255);" TextMode="MultiLine" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="espaco"></td>
                                </tr>
                            </table>
                            <table class="Table FundoPainel tabela ">
                                <tr>
                                    <td class="titulo corfonte cortitulo" colspan="5" style="font-size: medium;">Endereço
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam15 fonteTab">CEP
                                    </td>
                                    <td class="Tam35 fonteTab">Avenida, Rua, Praça
                                    </td>
                                    <td class="Tam08 fonteTab">Nº
                                    </td>
                                    <td class="Tam18 fonteTab">Complemento
                                    </td>
                                    <td class="Tam25 fonteTab">Bairro
                                    </td>

                                </tr>
                                <tr>
                                    <td class="Tam15 fonteTab">
                                        <asp:TextBox ID="TBCepEndProf" runat="server" AutoPostBack="true" CssClass="fonteTexto" Height="13px"
                                            OnTextChanged="TBCepEndAlu_TextChanged" Width="90%"
                                            MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td class="Tam30 fonteTab">
                                        <asp:TextBox ID="TBruaEndProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="90%"></asp:TextBox>
                                    </td>
                                    <td class="Tam05">
                                        <asp:TextBox ID="TBnumeroEnd" runat="server"
                                            CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();" Width="80%"></asp:TextBox>
                                    </td>
                                    <td class="Tam18">
                                        <asp:TextBox ID="TBcomplementoEndProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="80%"></asp:TextBox>
                                    </td>
                                    <td class="Tam25 fonteTab">
                                        <asp:TextBox ID="TBBairroProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="80%"></asp:TextBox>
                                    </td>

                                </tr>
                            </table>
                            <table class="Table FundoPainel tabela ">
                                <tr>
                                    <td class="Tam30 fonteTab">&nbsp;&nbsp; Municipio
                                    </td>
                                    <td class="Tam08 fonteTab">UF
                                    </td>
                                    <td class="Tam15 fonteTab">Telefone
                                    </td>
                                    <td class="Tam15 fonteTab">Tel. celular
                                    </td>
                                    <td class="Tam30 fonteTab">E- mail
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam30 fonteTab">&nbsp;&nbsp;
                                        <asp:TextBox ID="TBcidadeEndProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="90%" AutoCompleteType="DisplayName"></asp:TextBox>
                                        <cc1:AutoCompleteExtender BehaviorID="AutoComplete1" ID="AutoCompleteExtender1" runat="server"
                                            TargetControlID="TBcidadeEndProf" ServiceMethod="GetMunicipio" MinimumPrefixLength="1"
                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td class="Tam08 fonteTab">
                                        <asp:DropDownList ID="DDufEndProf" runat="server" CssClass="fonteTexto" DataSourceID="SqlDataSource1"
                                            DataTextField="MunIEstado" DataValueField="MunIEstado" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="90%" OnDataBound="IndiceZeroUF">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="Tam15 fonteTab">
                                        <asp:TextBox ID="TBtelefoneEndProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            onkeyup="formataTelefone(this,event);" Width="85%"></asp:TextBox>
                                    </td>
                                    <td class="Tam15 fonteTab">
                                        <asp:TextBox ID="TBcelularEndProf" runat="server" CssClass="fonteTexto" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="15"
                                            onkeyup="formataTelefoneSaoPaulo(this,event);" Width="85%"></asp:TextBox>
                                    </td>
                                    <td class="Tam30 fonteTab">
                                        <asp:TextBox ID="TBemail" runat="server" CssClass="fonteTexto" MaxLength="60" Height="13px"
                                            onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="espaco" colspan="5"></td>
                                </tr>
                            </table>
                            <table class="Table FundoPainel tabela ">
                                <tr>
                                    <td class=" cortitulo corfonte titulo" colspan="2" style="font-size: medium;">Parentesco
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam50 fonteTab">&nbsp;&nbsp; Nome do Pai
                                    </td>
                                    <td class="Tam50 fonteTab">Nome da Mãe
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam50 fonteTab">&nbsp;&nbsp;
                                        <asp:TextBox ID="TBnomePai" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            MaxLength="50" Width="90%"></asp:TextBox>
                                    </td>
                                    <td class="Tam50 fonteTab">
                                        <asp:TextBox ID="TBnomeMae" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            MaxLength="50" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="espaco" colspan="2">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                    SelectCommand="SELECT MunIEstado FROM MA_Municipios  Group BY MunIEstado ORDER BY MunIEstado"></asp:SqlDataSource>
                <asp:HiddenField ID="HFRowCount" runat="server" />
                <asp:HiddenField runat="server" ID="HFcpf" />
            </div>
        </div>
    </form>
</body>
<script src="../Scripts/Mascara.js" type="text/javascript"></script>
</html>
