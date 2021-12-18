<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroAprendizInicial.aspx.cs" MasterPageFile="~/MPLimpa.Master"
    Inherits="ProtocoloAgil.pages.CadastroAprendizInicial" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50731.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/cdl_bh.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Mascara.js" type="text/javascript"></script>
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <link href="../Styles/pesquisa.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">

        function ModifyEnterKeyPressAsTab() {
            if (window.event && window.event.keyCode == 13) {
                window.event.keyCode = 9;
            }
        }


        function reload() {
            window.parent.location = window.parent.location;
        }

        function change(href) {
            window.location.href = href;
        }

        function GetConfirm() {
            var hf = document.getElementById("HFConfirma");
            if (confirm("Deseja realmente excluir este familiar?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function GetConfirmItinerario() {
            var hf = document.getElementById("HFConfirma");
            if (confirm("Deseja realmente excluir este itinerário?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function GetConfirmCPF(message) {
            var hf = document.getElementById("HFConfirma");
            if (confirm(message) == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function GetConfirmEmail() {
            var hf = document.getElementById("HFConfirma");
            if (confirm("Deseja realmente alterar seu e-mail?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function ShowPhoto() {
            $("#Foto").css("display", "inline");
        }

        function popup(url, width, height) {
            $("#lightbox").css("display", "inline");
            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "Cadastro", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }
        function popup02(url, width, height) {

            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "Cadastro", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }
    </script>
    <style type="text/css">
        #all_master {
            width: 95%;
            margin: auto;
            height: 100%;
        }



        .content {
            width: 100%;
            background: #fff;
            min-height: 250px;
            clear: left;
            margin-top: -10px;
            padding-bottom: 30px;
        }

        .text_titulo {
            height: 15px;
            margin: 0 auto;
            float: inherit;
            padding-top: 20px;
        }

            .text_titulo h1 {
                font-family: Arial, Helvetica, sans-serif;
                font-weight: lighter;
                text-align: center;
                font-size: 18px;
                color: #00599c;
                margin: 0 auto;
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

        #content .list_results td.head_list_results {
            background: #f5f5f5;
            color: #00599c;
            font-weight: bold;
        }

        .list_results_Menor {
            width: 98%;
            margin: 10px 1% 10px 1%;
            border-top: solid 1px #dfdfdf;
            border-left: solid 1px #dfdfdf;
        }


            .list_results_Menor p {
                color: #7f7f7f;
                text-decoration: none;
            }


            .list_results_Menor td {
                border-bottom: solid 1px #dfdfdf;
                border-right: solid 1px #dfdfdf;
                padding: 5px;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 7pt;
                color: #474646;
            }

                .list_results_Menor td.head_list_results {
                    background: #f5f5f5;
                    color: #00599c;
                    font-weight: bold;
                }

        .upload-wrapper {
            cursor: pointer;
            display: inline-block;
            position: absolute;
            overflow: hidden;
        }

        .upload-file {
            cursor: pointer;
            position: absolute;
            filter: alpha(opacity=1);
            -moz-opacity: 0.01;
            opacity: 0.01;
            height: 28px;
            width: 28px;
            margin: 20px 0 0 0;
        }

        .upload-button {
            cursor: pointer;
            height: 25px;
            width: 25px;
        }


        .img {
            opacity: 0.4;
            filter: alpha(opacity=40); /* For IE8 and earlier */
        }

            .img:hover {
                opacity: 1.0;
                filter: alpha(opacity=100); /* For IE8 and earlier */
            }

        .hiddencol {
            display: none;
        }

        .style2 {
            text-align: center;
            margin: 0 auto;
        }

        .style4 {
            font-family: "Century Gothic";
            font-size: x-large;
            color: #0066FF;
        }

        .style3 {
            font-family: "Century Gothic";
        }

        .style5 {
            font-family: "Century Gothic";
            color: #0033CC;
        }

        .clearboth {
            font-weight: bold;
        }
    </style>
</asp:Content>











<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">







    <div class="fonteTitulo" style="width: 300px; position: relative; left: 552px; top: -90px">
        Cadastro do Jovem
    </div>


    <div>

        <div>


            <div style="float: right; position: relative; top: -110px">
                <asp:Button ID="btnsalvar" runat="server" CssClass="btn_novo" Text="Salvar" ToolTip="Salvar Alterações"
                    OnClientClick="javascript:CreateWheel('yes');" OnClick="btnpesquisa_Click" />
                <asp:Button ID="btnPaginaInicial" runat="server" CssClass="btn_novo" Text="Site CejaBrasil" ToolTip="Site CejaBrasil"
                    OnClientClick="javascript:CreateWheel('yes');" OnClick="btnPaginaInicial_Click" />
            </div>


            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <div style="width: 100%; margin: 0 auto;">


                        <table class="FundoPainel Table" style="width: 100%; float: right; clear: both; position: relative; top: 00px">
                            <tr>
                                <th colspan="6" class="cortitulo titulo corfonte" style="font-size: medium; font-weight: bold;">Dados do Jovem
                                </th>
                            </tr>
                            <tr>
                                <td class="fonteTab">Código
                                </td>
                                <td class="fonteTab" style="width: 40%">Nome do Jovem
                                </td>
                                <td class="fonteTab">Unidade
                                </td>
                                <td class="fonteTab">Instituição Parceira
                                </td>
                            </tr>
                            <tr>
                                <td class="fonteTab" style="width: 5%">
                                    <asp:TextBox ID="TBcodigoAlu" runat="server" CssClass="fonteTexto" Enabled="false"
                                        Height="16px" onkeydown="ModifyEnterKeyPressAsTab();" ReadOnly="true" Width="40px"></asp:TextBox>
                                </td>
                                <td class="fonteTab">
                                    <asp:TextBox ID="TBnomeAlu" runat="server" CssClass="fonteTexto" Height="16px" MaxLength="60"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="95%"></asp:TextBox>
                                </td>
                                <td class="fonteTab">
                                    <asp:DropDownList ID="DDUnidade" runat="server" CssClass="fonteTexto"
                                        DataTextField="UniNome" DataValueField="UniCodigo"
                                        Height="18px" OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="100%">
                                    </asp:DropDownList>
                                </td>
                                <td class="fonteTab">
                                    <asp:DropDownList ID="DDInstituicaoParceira" runat="server" CssClass="fonteTexto"
                                        DataTextField="IpaDescricao" DataValueField="IpaCodigo"
                                        Height="18px" OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="100%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table class="FundoPainel Table" style="width: 100%; float: right; position: relative; top: 00px">
                            <tr>




                                <td class="fonteTab">Data de Nascimento
                                </td>
                                <td class=" fonteTab">Status do Jovem
                                </td>
                                <td class="fonteTab">Estuda Atualmente
                                </td>

                                <td class="fonteTab">Turno
                                </td>
                                <td class="fonteTab">Escolaridade
                                </td>
                                <td class="fonteTab">Idade
                                </td>
                                <td class="fonteTab">Sexo
                                </td>
                            </tr>
                            <tr>




                                <td class="fonteTab">
                                    <asp:TextBox ID="TBdataNascAlu" runat="server" CssClass="fonteTexto" Height="16px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                        MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                                        PopupPosition="TopRight" runat="server" TargetControlID="TBdataNascAlu">
                                    </cc2:CalendarExtenderPlus>
                                </td>

                                <td class="fonteTab">
                                    <asp:DropDownList ID="DD_situacao_Aprendiz" runat="server" CssClass="fonteTexto"
                                        DataTextField="StaDescricao" DataValueField="StaCodigo"
                                        Height="18px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="75%">
                                    </asp:DropDownList>
                                </td>

                                <td class="fonteTab">
                                    <asp:DropDownList ID="DDEstudaAtualmente" runat="server" CssClass="fonteTexto" Height="18px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="85px">
                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <td class="fonteTab">

                                    <asp:DropDownList CssClass="fonteTexto" runat="server" ID="DDTurno">
                                        <asp:ListItem Value="M">Manhã</asp:ListItem>
                                        <asp:ListItem Value="T"> Tarde</asp:ListItem>
                                        <asp:ListItem Value="N">Noite</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList ID="DD_escolaridade_aprendiz" runat="server" CssClass="fonteTexto"
                                        Height="18px" onkeydown="ModifyEnterKeyPressAsTab();" OnDataBound="IndiceZero"
                                        Width="90%" DataSourceID="SDS_Escolaridade" DataTextField="GreDescricao" DataValueField="GreCodigo">
                                    </asp:DropDownList>
                                </td>

                                <td>
                                    <asp:Label ID="LBidade" runat="server" BackColor="White" Width="30px" Height="16px"
                                        BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:Label>
                                </td>
                                <td class="fonteTab" style="width: 10%;">
                                    <asp:DropDownList ID="DDsexoAlu" runat="server" CssClass="fonteTexto" Height="16px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="85px">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="M">Masculino</asp:ListItem>
                                        <asp:ListItem Value="F">Feminino</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table class="FundoPainel Table" style="width: 100%; float: right; clear: both; position: relative; top: -00px">
                            <tr>




                                <%-- <td class="fonteTab">Início Aula
                                    </td>
                                    <td class="fonteTab">Término Aula
                                    </td>--%>

                                <td class="fonteTab">Escola
                                </td>
                                <td class="fonteTab">&nbsp;&nbsp;Endereço da Escola
                                </td>
                            </tr>
                            <tr>





                                <td style="width: 30%;">
                                    <asp:DropDownList ID="DDcoleOrigem" runat="server" CssClass="fonteTexto" DataSourceID="SDS_Escolas"
                                        AutoPostBack="true" DataTextField="EscNome" DataValueField="EscCodigo" Height="20px"
                                        OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();" Width="90%"
                                        OnSelectedIndexChanged="DDcoleOrigem_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <%  var exibir = Session["enable_Save"];
                                        if ((string)exibir == "User")
                                        { %>
                                    <asp:Button ID="Button3" CssClass="btn_incluir  " runat="server" Text="..." OnClick="Button3_Click" />
                                    <% } %>
                                </td>
                                <td class="Tam25 fonteTab" colspan="2">&nbsp;&nbsp;<asp:Label ID="lb_endereco" runat="server" BackColor="White" Width="98%"
                                    Height="30px" BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:Label>
                                </td>
                            </tr>
                        </table>


                        <table class=" FundoPainel Table" style="width: 100%; float: right; clear: both; position: relative; top: 0px">
                            <tr>
                                <td class="Tam10 fonteTab">&nbsp;&nbsp; Estado Civil
                                </td>
                                <td class="Tam20 fonteTab">&nbsp;&nbsp; Nacionalidade
                                </td>
                                <td class="Tam05 fonteTab">&nbsp;&nbsp; UF
                                </td>
                                <td class="Tam20 fonteTab">&nbsp;&nbsp; Naturalidade
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;
                                    <asp:DropDownList ID="DDestCivilAlu" runat="server" CssClass="fonteTexto" Height="20px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="90%">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="S">Solteiro</asp:ListItem>
                                        <asp:ListItem Value="C">Casado</asp:ListItem>
                                        <asp:ListItem Value="D">Divorciado</asp:ListItem>
                                        <asp:ListItem Value="V">Viúvo</asp:ListItem>
                                        <asp:ListItem Value="A">Amasiado</asp:ListItem>
                                        <asp:ListItem Value="D">Desquitado</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TBpaisAlu" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="30"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="85%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                   <asp:DropDownList ID="DD_estado_Nat" OnSelectedIndexChanged="DD_estado_Nat_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="fonteTexto" DataSourceID="SqlDataSource1"
                                       DataTextField="MunIEstado" DataValueField="MunIEstado" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                       Width="80%" OnDataBound="IndiceZeroUF" ViewStateMode="Enabled">
                                   </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                   
                             <asp:DropDownList ID="DDMunicipio" runat="server" CssClass="fonteTexto" DataTextField="MunIDescricao" DataValueField="MunIDescricao"
                                 Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                 Width="85%" OnDataBound="IndiceZeroUF"
                                 ViewStateMode="Enabled">
                             </asp:DropDownList>
                                </td>

                            </tr>
                        </table>

                        <table class="Table FundoPainel" style="width: 100%; float: right; clear: both; position: relative; top: 00px">
                            <tr>
                                <td class="Tam30 fonteTab">&nbsp;&nbsp; Nome do Pai
                                </td>
                                <td class="Tam30 fonteTab">&nbsp;&nbsp; Nome da Mãe
                                </td>


                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TBnomePai" runat="server" MaxLength="60" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="90%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TBnomeMae" runat="server" MaxLength="60" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="90%"></asp:TextBox>
                                </td>



                                &nbsp;&nbsp;
                                   
                                    
                            </tr>
                        </table>
                        <% var exibe = Session["enable_Save"];
                           if ((string)exibe == "User")
                           { %>
                        <table class="FundoPainel Table" style="width: 100%; float: right; clear: both; position: relative; top: 0px">
                            <tr>
                                <td class="fonteTab">&nbsp;&nbsp; Observação
                                </td>
                            </tr>
                            <tr>
                                <td class="fonteTab">&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_observacao" runat="server" CssClass="fonteTexto" Height="40px"
                                        onkeyup="javascript:IsMaxLength(this, 255);" TextMode="MultiLine" Width="97%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="espaco"></td>
                            </tr>
                        </table>
                        <% } %>














































                        <table class="Table FundoPainel" style="width: 100%; float: right; clear: both; position: relative; top: 00px">
                            <tr>
                                <th class="titulo corfonte cortitulo" style="font-size: medium; font-weight: bold;"
                                    colspan="6">Documentos do Jovem
                                </th>
                            </tr>
                            <tr>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; RG
                                </td>
                                <td class="Tam10 fonteTab">&nbsp;&nbsp; Data Emissão
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; Org. Exp.
                                </td>
                                <td class="Tam08 fonteTab">&nbsp;&nbsp; UF
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; Número CPF
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; Número PIS
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TBRGalu" runat="server" MaxLength="20" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="85%"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                    <asp:TextBox ID="TBdataEmissRGAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                        MaxLength="10" onkeyup="formataData(this,event);" Width="100px"></asp:TextBox>
                                    <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus9" Format="dd/MM/yyyy" PopupPosition="BottomRight"
                                        TargetControlID="TBdataEmissRGAlu" runat="server">
                                    </cc2:CalendarExtenderPlus>
                                </td>
                                <td>
                                    <asp:TextBox ID="TBorgExpRGAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="10" Width="80%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:DropDownList ID="DDufRGAlu" runat="server" CssClass="fonteTexto" DataSourceID="SqlDataSource1"
                                        DataTextField="MunIEstado" DataValueField="MunIEstado" Height="18px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="80%" OnDataBound="IndiceZeroUF">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TBCPFalu" OnTextChanged="TBCPFalu_TextChanged" AutoPostBack="true" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        MaxLength="14" onkeyup="formataCPF(this,event);" Width="85%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_numero_pis" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="11" onkeydown="ModifyEnterKeyPressAsTab();" Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="Table FundoPainel" style="width: 100%; float: right; clear: both; position: relative; top: -00px">
                            <tr>
                                <td class="Tam20 fonteTab">CTPS &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Série
                                </td>


                                <td class="Tam20 fonteTab" style="width: 200px">Certificado de Reservista
                                </td>


                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TBnumCTPSresp" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="8" onkeydown="ModifyEnterKeyPressAsTab();" Width="45%"></asp:TextBox>&nbsp;
                                    <asp:TextBox ID="TBserieCTPSResp" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="6" onkeydown="ModifyEnterKeyPressAsTab();" Width="25%"></asp:TextBox>
                                </td>


                                <td>
                                    <asp:TextBox ID="TB_cert_reservista" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="80" Width="85%"></asp:TextBox>
                                </td>

                            </tr>
                        </table>


                        <table class="Table FundoPainel" style="width: 100%; float: right; clear: both; position: relative; top: 00px">
                            <tr>
                                <th class="titulo corfonte cortitulo" style="font-size: medium; font-weight: bold;"
                                    colspan="6">Endereço
                                </th>
                            </tr>
                            <tr>
                                <td class="Tam10 fonteTab">&nbsp;&nbsp; Cep
                                </td>
                                <td class="Area fonteTab">&nbsp;&nbsp; Nº
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; Complemento
                                </td>
                                <td class="Tam30 fonteTab">&nbsp;&nbsp; Bairro
                                </td>
                                <td class="Tam15 fonteTab">&nbsp;&nbsp; UF
                                </td>
                            </tr>
                            <tr>

                                <td>&nbsp;&nbsp;

                                    <asp:TextBox ID="TBCepEndAlu" runat="server" AutoPostBack="true" CssClass="fonteTexto" Height="13px"
                                        OnTextChanged="TBCepEndAlu_TextChanged" Width="80%"
                                        MaxLength="10"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                    <asp:TextBox ID="TBnumEndAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="6" Width="77%"></asp:TextBox>
                                </td>
                                <td class="Cep2 centralizar">
                                    <asp:TextBox ID="TBcomplementoEndAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="30" Width="90%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <%--<asp:TextBox ID="TBBairroAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="40" Width="85%"></asp:TextBox>
                                        <cc1:AutoCompleteExtender BehaviorID="AutoComplete1" ID="AutoCompleteExtender2" runat="server"
                                            TargetControlID="TBBairroAlu" ServiceMethod="GetBairro" MinimumPrefixLength="1"
                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20">
                                        </cc1:AutoCompleteExtender>
                                    --%>
                                    <asp:TextBox ID="txtBairro" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="60" Width="90%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                        <asp:DropDownList ID="DDufEndAlu" AutoPostBack="true" runat="server" CssClass="fonteTexto" DataSourceID="SqlDataSource1"
                                            DataTextField="MunIEstado" DataValueField="MunIEstado" Height="18px" onkeydown="ModifyEnterKeyPressAsTab();"
                                            Width="80%" OnDataBound="IndiceZeroUF" ViewStateMode="Enabled">
                                        </asp:DropDownList>

                                </td>
                            </tr>
                        </table>
                        <table class="Table FundoPainel" style="width: 100%; float: right; clear: both; position: relative; top: 00px">
                            <tr>

                                <td class="Tam25 fonteTab">&nbsp;&nbsp; Municipio
                                </td>
                                <td class="Tam30 fonteTab">&nbsp;&nbsp;  Avenida, Rua, Praça
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; Telefone
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; Tel. celular
                                </td>
                                <td class="Tam25 fonteTab">&nbsp; E- mail
                                </td>
                            </tr>
                            <tr>

                                <td>&nbsp;
                                     <asp:TextBox ID="txtMunicipioEndereco" runat="server" CssClass="fonteTexto" Height="13px"
                                         onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="60" Width="90%"></asp:TextBox>
                                </td>

                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TBruaEndAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="60" Width="90%"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                    <asp:TextBox ID="TBtelefoneEndAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="14" onkeyup="formataTelefone(this,event);"
                                        Width="85%"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                    <asp:TextBox ID="TBcelularEndAlu" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="15" onkeyup="formataTelefoneSaoPaulo(this,event);"
                                        Width="85%"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                    <asp:TextBox ID="TBemail" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="70%" MaxLength="80"></asp:TextBox>
                                    &nbsp;
                                    <% exibe = Session["enable_Save"];
                                       if ((string)exibe == "Aluno")
                                       { %>
                                    <asp:Button ID="btn_altera_email" runat="server" CssClass="btn_add" OnClientClick="GetConfirmEmail()"
                                        Text="Alterar" OnClick="btn_altera_email_Click" />
                                    <% } %>
                                </td>
                            </tr>
                            <tr>
                                <td class="espaco" colspan="6"></td>
                            </tr>
                        </table>



                        <table class="Table FundoPainel" style="width: 100%; float: right; clear: both; position: relative; top: 00px">
                            <tr>
                                <th class="titulo corfonte cortitulo" style="font-size: medium; font-weight: bold;"
                                    colspan="6">Outras Informações
                                </th>
                            </tr>
                            <tr>
                                <td class="fonteTab">Conhecimento Windows
                                </td>
                                <td class="fonteTab">Conhecimento Word
                                </td>
                                <td class="fonteTab">Conhecimento Excel
                                </td>
                                <td class="fonteTab">Possui filhos?
                                </td>
                                <td class="fonteTab">Quantidade de filhos
                                </td>
                                <td class="fonteTab">Com quem fica?
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="DDWindows" CssClass="fonteTexto">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="A">Alto</asp:ListItem>
                                        <asp:ListItem Value="M">Médio</asp:ListItem>
                                        <asp:ListItem Value="B">Básico</asp:ListItem>
                                        <asp:ListItem Value="N">Nenhum</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="DDWord" CssClass="fonteTexto">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="A">Alto</asp:ListItem>
                                        <asp:ListItem Value="M">Médio</asp:ListItem>
                                        <asp:ListItem Value="B">Básico</asp:ListItem>
                                        <asp:ListItem Value="N">Nenhum</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="DDExcel" CssClass="fonteTexto">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="A">Alto</asp:ListItem>
                                        <asp:ListItem Value="M">Médio</asp:ListItem>
                                        <asp:ListItem Value="B">Básico</asp:ListItem>
                                        <asp:ListItem Value="N">Nenhum</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="DDpossuiFIlhos" CssClass="fonteTexto">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="N">Não</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQuantidadeFilhos" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="2" Width="90%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComQuemFicaFilhos" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="50" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>








                        <table class="FundoPainel Table">
                            <tr>
                                <th class="cortitulo titulo corfonte" style="font-size: medium; font-weight: bold;">Dados Sócio-econômicos
                                </th>
                            </tr>
                        </table>
                        <table class="FundoPainel Table">
                            <tr>
                                <td class="Tam20 fonteTab">Situação da Residência
                                </td>
                                <td class="Tam20 fonteTab">Custo Residência
                                </td>
                                <td class="Tam20 fonteTab">Número<br />
                                    Familiares
                                </td>
                                <td class="Tam10 fonteTab">Benefício
                                </td>
                                <td class="Tam12 fonteTab">Bolsa Família(R$)
                                </td>
                                <td class="Tam12 fonteTab">Pensão(R$)
                                </td>
                                <td class="Tam12 fonteTab">Outros(R$)
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="DDTipoResidencia" runat="server" CssClass="fonteTexto" Height="20px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="90%">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="P">Própria</asp:ListItem>
                                        <asp:ListItem Value="A">Alugada</asp:ListItem>
                                        <asp:ListItem Value="F">Financiada</asp:ListItem>
                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="TBValorAluguel" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" onkeyup="formataValor(this,event);" MaxLength="10"
                                        Width="70%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TBNumeroFamiliares" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="2"
                                        Width="50%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDBeneficio" runat="server" CssClass="fonteTexto" Height="18px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="80%">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_bolsa_familia" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" onkeyup="formataValor(this,event);" MaxLength="10"
                                        Width="85%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_pensao" runat="server" CssClass="fonteTexto" Height="13px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        onkeyup="formataValor(this,event);" MaxLength="10" Width="85%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_valor_outros" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" onkeyup="formataValor(this,event);" MaxLength="10"
                                        Width="85%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Tam50 fonteTab" style="text-align: left;" colspan="3">É você que recebe o benefício?&nbsp;
                                        <asp:DropDownList ID="DDRecebeBeneficio" runat="server" CssClass="fonteTexto" Height="18px"
                                            onkeydown="ModifyEnterKeyPressAsTab();" Width="90px">
                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                        </asp:DropDownList>
                                </td>
                                <td class="Tam50" colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="Tam50 fonteTab" style="text-align: left;" colspan="3">&nbsp;&nbsp; O jovem já trabalhou com carteira assinada ? &nbsp;&nbsp;
                                    <asp:DropDownList ID="DDcarteiraAssinada" runat="server" CssClass="fonteTexto" Height="18px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" AutoPostBack="true" Width="90px" OnSelectedIndexChanged="DDcarteiraAssinada_SelectedIndexChanged">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:DropDownList>
                                </td>


                            </tr>

                            <tr>
                                
                                <td colspan="7" style="text-align:center">
                                    <asp:Button ID="BtnSalavarDeBaixo" runat="server" CssClass="btn_novo" Text="Salvar" ToolTip="Salvar Alterações"
                                        OnClientClick="javascript:CreateWheel('yes');" OnClick="btnpesquisa_Click" />
                                </td>
                            </tr>
                        </table>
























                        <% exibe = Session["enable_Save"];
                           if ((string)exibe == "User")
                           { %>

                        <% } %>
                        <% exibe = Session["enable_Save"];
                           if ((string)exibe == "Aluno")
                           { %>
                        <br />
                        <div class="fonteTab">
                            <span style="color: red">Obs.:</span> Para modificar seu e-mail, reescreva-o e clique
                            no botão "Alterar".
                        </div>
                        <% } %>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                            SelectCommand="SELECT [MunIEstado] FROM [MA_Municipios]  Group BY [MunIEstado]ORDER BY [MunIEstado] "
                            OnSelected="SqlDataSource1_Selected"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SDS_Escolas" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                            SelectCommand="SELECT  EscNome, EscCodigo FROM CA_Escolas order by  EscNome"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SDS_Situacao" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                            SelectCommand="  SELECT  StaDescricao,StaCodigo   FROM CA_SituacaoAprendiz order by  StaDescricao"></asp:SqlDataSource>
                    </div>
                </asp:View>




                <asp:View ID="View3" runat="server">
                    <table class="FundoPainel Table">
                        <tr>
                            <th colspan="6" class="cortitulo titulo corfonte" style="font-size: medium; font-weight: bold;">Dados do Responsável
                            </th>
                        </tr>
                        <tr>
                            <td class="Tam12 fonteTab">&nbsp;&nbsp; Número CPF
                            </td>
                            <td class="Tam25 fonteTab">&nbsp;&nbsp; Nome do Responsável
                            </td>
                            <td class="Tam12 fonteTab">&nbsp;&nbsp; Telefone
                            </td>
                            <td class="Tam12 fonteTab">&nbsp;&nbsp; Tel. celular
                            </td>
                            <td class="Tam15 fonteTab">&nbsp;&nbsp; Nome Contato
                            </td>
                            <td class="Tam15 fonteTab">&nbsp;&nbsp; Tel. Contato
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                                <asp:TextBox ID="TB_cpf_resp" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="14" onkeyup="formataCPF(this,event);"
                                    Width="88%"></asp:TextBox>
                            </td>
                            <td>&nbsp;&nbsp;
                                <asp:TextBox ID="TB_nome_resp" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="60" onblur="if(this.value.length < 10) {alert('Campo Nome do Responsável vazio ou inválido! Certifique-se que o nome possui mais de 10 caracteres. ');}"
                                    Width="90%"></asp:TextBox>
                            </td>
                            <td>&nbsp;
                                <asp:TextBox ID="TB_tel_resp" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="14" onkeyup="formataTelefone(this,event);"
                                    Width="88%"></asp:TextBox>
                            </td>
                            <td>&nbsp;
                                <asp:TextBox ID="TB_cel_resp" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="14" onkeyup="formataTelefone(this,event);"
                                    Width="88%"></asp:TextBox>
                            </td>
                            <td>&nbsp;&nbsp;
                                <asp:TextBox ID="TB_recado_nome" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="30" Width="70%"></asp:TextBox>
                            </td>
                            <td>&nbsp;&nbsp;
                                <asp:TextBox ID="TB_recado_tel" runat="server" CssClass="fonteTexto" Height="13px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="14" onkeyup="formataTelefone(this,event);"
                                    Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="FundoPainel Table">
                        <tr>
                            <td class="Tam25 fonteTab">&nbsp;&nbsp; Grau Parentesco
                            </td>
                            <td class="Tam18 fonteTab">&nbsp;
                            </td>
                            <td class="Tam25 fonteTab">&nbsp;
                            </td>
                            <td class="Tam30 fonteTab">&nbsp;
                            </td>

                        </tr>
                        <tr>
                            <td>&nbsp;
                                <asp:DropDownList ID="DD_grau_Parentesco" runat="server" CssClass="fonteTexto" Height="20px"
                                    OnDataBound="IndiceZero" Width="90%" DataSourceID="SDS_GrauParentesco" DataTextField="GpaDescricao"
                                    DataValueField="GpaCodigo">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;
                                
                            </td>
                            <td>&nbsp;
                                
                            </td>
                            <td>&nbsp;
                                
                            </td>
                        </tr>
                    </table>
                    <% var exibe = Session["enable_Save"];
                       if ((string)exibe == "User")
                       { %>
                    <table class="FundoPainel Table">
                        <tr>
                            <td class="fonteTab">&nbsp;&nbsp; Observação
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab">&nbsp;&nbsp;
                                <asp:TextBox ID="TB_observacao_resp" runat="server" CssClass="fonteTexto" Height="40px"
                                    onkeyup="javascript:IsMaxLength(this, 255);" TextMode="MultiLine" Width="97%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <% } %>
                    <div style="width: 98%; margin: 0 auto;">

                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="PNcarteira" Enabled="False">
                                    <table class="FundoPainel Table">
                                        <tr>
                                            <td class="Tam30 fonteTab" style="text-align: left;" colspan="2"></td>
                                            <td class="Tam40 fonteTab"></td>
                                        </tr>
                                        <tr>
                                            <td class="Tam08 fonteTab" style="text-align: left;">&nbsp;&nbsp; Empresa
                                            </td>
                                            <td class="Tam20 fonteTab">
                                                <asp:TextBox ID="TB_Empresa" runat="server" MaxLength="50" CssClass="fonteTexto"
                                                    Height="13px" Width="90%"></asp:TextBox>
                                            </td>
                                            <td class="Tam40">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Tam08 fonteTab" style="text-align: left;">&nbsp;&nbsp; Cargo
                                            </td>
                                            <td class="Tam20 fonteTab">
                                                <asp:TextBox ID="TB_Cargo" runat="server" MaxLength="50" CssClass="fonteTexto" Height="13px"
                                                    Width="90%"></asp:TextBox>
                                            </td>
                                            <td class="Tam40">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Tam08 fonteTab">&nbsp;&nbsp; Permanência
                                            </td>
                                            <td class="Tam20 fonteTab">
                                                <asp:TextBox ID="TB_permanencia" runat="server" MaxLength="20" CssClass="fonteTexto"
                                                    Height="13px" Width="25%"></asp:TextBox>
                                            </td>
                                            <td class="Tam40">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="espaco" colspan="3">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<table class="FundoPainel Table">
                                <tr>
                                    <td class="cortitulo titulo corfonte" colspan="5" style="font-size: medium; font-weight: bold;">Informações sobre a saúde do aprendiz
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam40 fonteTab" style="text-align: left;" colspan="2">&nbsp;&nbsp;&nbsp;O aprendiz faz uso de Medicamentos ? &nbsp;&nbsp;
                                    <asp:DropDownList ID="DDmedicamentos" runat="server" CssClass="fonteTexto" Height="18px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="90px">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                    <td class=" Tam05 fonteTab">&nbsp;&nbsp;Qual?
                                    </td>
                                    <td class=" Tam20 fonteTab">
                                        <asp:TextBox ID="TB_medicamento" runat="server" CssClass="fonteTexto" Height="13px"
                                            MaxLength="50" Width="85%"></asp:TextBox>
                                    </td>
                                    <td class="Tam25 fonteTab">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam15 fonteTab" style="text-align: left;">&nbsp;&nbsp;&nbsp;Finalidade
                                    </td>
                                    <td class="fonteTab" style="text-align: left;" colspan="4">
                                        <asp:TextBox ID="TB_finalidade_medicamento" runat="server" CssClass="fonteTexto"
                                            Height="13px" MaxLength="50" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam40 fonteTab" style="text-align: left;" colspan="2">&nbsp; &nbsp;&nbsp;O aprendiz tem alguma reação alérgica ? &nbsp;&nbsp;
                                    <asp:DropDownList ID="DDAlergia" runat="server" CssClass="fonteTexto" Height="18px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="80px">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                    <td class=" Tam05 fonteTab">&nbsp;&nbsp;Qual?
                                    </td>
                                    <td class=" fonteTab">
                                        <asp:TextBox ID="TB_tipo_alergia" runat="server" MaxLength="50" CssClass="fonteTexto"
                                            Height="13px" Width="90%"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Tam40 fonteTab" style="text-align: left;" colspan="2">&nbsp;&nbsp;&nbsp;O aprendiz tem algum problema de saúde? &nbsp;&nbsp;<asp:DropDownList
                                        ID="DDdoenca" runat="server" CssClass="fonteTexto" Height="18px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="80px">
                                        <asp:ListItem Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                    <td class=" Tam05 fonteTab">&nbsp;&nbsp;Qual?
                                    </td>
                                    <td class="fonteTab">
                                        <asp:TextBox ID="TB_tipo_doenca" runat="server" MaxLength="50" CssClass="fonteTexto"
                                            Height="13px" Width="90%"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="espaco" colspan="5">&nbsp;
                                    </td>
                                </tr>
                            </table>--%>
                    </div>
                    <asp:SqlDataSource ID="SDS_Escolaridade" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                        SelectCommand="select GreCodigo,GreDescricao from  CA_GrauEscolaridade order by GreDescricao "></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SDS_Profissao" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                        SelectCommand="select ProfCodigo,ProfDescricao from CA_Profissoes order by ProfDescricao"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SDS_GrauParentesco" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                        SelectCommand="select GpaCodigo,GpaDescricao from CA_GrauParentesco order by GpaDescricao "></asp:SqlDataSource>
                </asp:View>
                <asp:View runat="server" ID="View4">
                    <% var exibe = Session["enable_Save"];
                       if ((string)exibe == "User")
                       { %>
                    <div class="controls">
                        <div style="float: right;">
                            <asp:Button ID="btn_adicionar" runat="server" CssClass="btn_novo" Text="Adicionar"
                                ToolTip="Adicionar" OnClick="btn_adicionar_Click" />
                        </div>
                    </div>
                    <% } %>
                    <asp:Panel runat="server" ID="PNnovo" Visible="false">
                        <table class="FundoPainel Table">
                            <tr>
                                <td class="cortitulo titulo corfonte" colspan="5" style="font-size: medium; font-weight: bold;">Cadastro de Familiares
                                </td>
                            </tr>
                            <tr>
                                <td class="fonteTab Tam30">&nbsp;&nbsp; Nome do Familiar
                                </td>
                                <td class="Tam10 fonteTab">&nbsp;&nbsp; Data Nascimento
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; Parentesco
                                </td>
                                <td class="Tam20 fonteTab">&nbsp;&nbsp; Profissão
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="tb_nome_parente" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="50" onblur="if(this.value.length < 10) {alert('Campo Nome do Familiar vazio ou inválido! Certifique-se que o nome possui mais de 10 caracteres. ');}"
                                        Width="90%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_data_Nasc_fam" runat="server" CssClass="fonteTexto" Height="13px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                        MaxLength="10" onkeyup="formataData(this,event);" Width="100px"></asp:TextBox>
                                    <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus3" Format="dd/MM/yyyy" PopupPosition="BottomRight"
                                        TargetControlID="TB_data_Nasc_fam" runat="server">
                                    </cc2:CalendarExtenderPlus>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:DropDownList ID="DD_parentesco" runat="server" CssClass="fonteTexto" Height="20px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" OnDataBound="IndiceZero" Width="90%"
                                        DataTextField="GpaDescricao" DataValueField="GpaCodigo">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:DropDownList ID="DDProfissao_fam" runat="server" CssClass="fonteTexto" Height="20px"
                                        onkeydown="ModifyEnterKeyPressAsTab();" Width="90%"
                                        DataTextField="ProfDescricao" DataValueField="ProfCodigo">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="fonteTab Tam30">&nbsp;&nbsp; Vínculo Empregatício
                                </td>
                                <td class="Tam10 fonteTab">&nbsp;&nbsp; Renda Mensal(R$)
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;&nbsp; Identidade
                                </td>
                                <td class="Tam20 fonteTab">&nbsp;&nbsp; Órgão
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;
                                    <asp:DropDownList ID="DDvinculo" runat="server" CssClass="fonteTexto" DataTextField="Vin_Descricao"
                                        DataValueField="Vin_Codigo" Height="20px"
                                        Width="90%">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_renda_mensal" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="10" onkeydown="ModifyEnterKeyPressAsTab();" onkeyup="formataValor(this,event);"
                                        Width="75%"></asp:TextBox>
                                </td>

                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtNumeroIdentidade" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="8" onkeydown="ModifyEnterKeyPressAsTab();" onkeyup="formataInteiro(this, event);"
                                        Width="75%"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtOrgaoIdentidade" runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="10" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="75%"></asp:TextBox>
                                </td>

                            </tr>

                            <tr>
                                <td class="Tam20 fonteTab centralizar" colspan="5">&nbsp;&nbsp;
                                    <asp:Button ID="Button2" runat="server" CssClass="btn_novo" Text="Confirmar" ToolTip="Adicionar"
                                        OnClick="Button2_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="fonteTab Tam30">&nbsp;
                                </td>
                                <td class="Tam10 fonteTab">&nbsp;
                                </td>
                                <td class="Tam12 fonteTab">&nbsp;
                                </td>
                                <td class="Tam20 fonteTab">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                    <asp:Panel runat="server" ID="PNFamilia">
                        <table class="FundoPainel Table">
                            <tr>
                                <td class="cortitulo titulo corfonte" style="font-size: medium; font-weight: bold;">Informações Familiares
                                </td>
                            </tr>
                        </table>
                        <asp:DataList ID="listaconteudo" runat="server" RepeatColumns="1" RepeatDirection="Vertical"
                            Width="100%" CssClass="centralizar">
                            <ItemTemplate>
                                <table class="FundoPainel Table">
                                    <tr>
                                        <td class="Tam30 fonteTab">&nbsp;&nbsp; Nome do Familiar
                                        </td>
                                        <td class="Tam10 fonteTab">&nbsp;&nbsp; Data Nascimento
                                        </td>
                                        <td class="Tam12 fonteTab">&nbsp;&nbsp; Parentesco
                                        </td>
                                        <td class="Tam20 fonteTab">&nbsp;&nbsp; Profissão
                                        </td>
                                    </tr>
                                    <tr class="fonteTab">
                                        <td>&nbsp;&nbsp;
                                            <asp:TextBox ID="tb_nome_parenteDB" runat="server" CssClass="fonteTexto" Height="13px"
                                                Enabled="false" Width="90%" Text=' <%#  DataBinder.Eval(Container.DataItem, "Fam_Nome")%>'></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                            <asp:TextBox ID="TB_data_Nasc_famDB" runat="server" CssClass="fonteTexto" Height="13px"
                                                Enabled="false" Text='<%# string.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "Fam_DataNascimento"))%>'
                                                Width="75%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                            <asp:TextBox ID="TB_ParentescoDB" runat="server" CssClass="fonteTexto" Height="13px"
                                                Enabled="false" Text='<%# DataBinder.Eval(Container.DataItem, "GpaDescricao")%>'
                                                Width="75%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                            <asp:TextBox ID="TB_profissaoDB" runat="server" CssClass="fonteTexto" Height="13px"
                                                Enabled="false" Text='<%# DataBinder.Eval(Container.DataItem, "ProfDescricao")%>'
                                                Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fonteTab Tam30">&nbsp;&nbsp; Vínculo Empregatício
                                        </td>
                                        <td class="Tam10 fonteTab">&nbsp;&nbsp; Renda Mensal(R$)
                                        </td>
                                        <td class="Tam12 fonteTab">&nbsp;&nbsp; Identidade
                                        </td>
                                        <td class="Tam20 fonteTab">&nbsp;&nbsp; Órgão
                                        </td>
                                    </tr>
                                    <tr class="fonteTab">
                                        <td>&nbsp;&nbsp;
                                            <asp:TextBox ID="TextBox2DB" runat="server" Enabled="false" CssClass="fonteTexto"
                                                Height="13px" Text='<%# DataBinder.Eval(Container.DataItem, "Vin_Descricao")%>'
                                                Width="90%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                            <asp:TextBox ID="TextBox1DB" runat="server" Enabled="false" CssClass="fonteTexto"
                                                Height="13px" Text='<%# string.Format("{0:F}",DataBinder.Eval(Container.DataItem, "Fam_Renda"))%>'
                                                Width="75%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtNumeroIdentidade" Text='<%# DataBinder.Eval(Container.DataItem, "Fam_Identidade")%>' runat="server" CssClass="fonteTexto" Height="13px"
                                        MaxLength="15"
                                        Width="75%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtOrgaoIdentidade" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Fam_OrgaoIdentidade")%>' CssClass="fonteTexto" Height="13px"
                                        MaxLength="10" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="75%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <% var exibe = Session["enable_Save"];
                                               if ((string)exibe == "User")
                                               { %>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btn_editDB" runat="server" CssClass="btn_novo" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Fam_Ordem")%>'
                                                Text="Alterar" ToolTip="Adicionar" OnClick="btn_edit_Click" />
                                            <asp:Button ID="btn_deleteDB" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Fam_Ordem")%>'
                                                OnClientClick="javascript:GetConfirm();" CssClass="btn_novo" Text="Excluir" ToolTip="Adicionar"
                                                OnClick="btn_delete_Click" />
                                            <% } %>
                                        </td>
                                    </tr>
                                    <tr style="clear: both">
                                        <td colspan="5">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <hr />
                            </ItemTemplate>
                        </asp:DataList>
                    </asp:Panel>
                </asp:View>
                <asp:View runat="server" ID="View5">
                    <div class="centralizar" style="width: 98%; margin: 0 auto;">
                        <div class="text_titulo">
                            <h1>Alocações do Jovem
                            </h1>
                        </div>
                        <br />
                    </div>
                    <asp:Panel ID="pn_info" runat="server" CssClass="centralizar" Height="100px" Visible="false"
                        Width="500px">
                        <div class="text_titulo" style="margin-top: 30px;">
                            <h1>Não existem alocações cadastradas para o jovem.</h1>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" CssClass="centralizar" ID="pn_alocacao" Width="98%">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Style="margin: 0 auto; width: 100%;"
                            AutoGenerateColumns="False" CssClass="list_results_Menor" OnDataBound="GridView_DataBound"
                            OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="TurNome" HeaderText="Turma" />
                                <asp:BoundField DataField="ALADataInicio" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Início" />
                                <asp:BoundField DataField="ALADataPrevTermino" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="Prev Término" />
                                <asp:BoundField DataField="ALADataTermino" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Término" />
                                <asp:BoundField DataField="ParNomeFantasia" HeaderText="Empresa" />
                                <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                                <asp:BoundField DataField="StaDescricao" HeaderText="Situação do Aluno" />
                                <asp:BoundField DataField="Situacao" HeaderText="Status" />
                            </Columns>
                            <HeaderStyle CssClass="List_results_menor" />
                            <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo" PreviousPageText="Anterior" />
                            <PagerStyle CssClass="nav_results" HorizontalAlign="Center" />
                        </asp:GridView>

                        <div class="centralizar" style="width: 98%; margin: 0 auto;">
                            <div class="text_titulo">
                                <h1>Frequência
                                </h1>
                            </div>
                            <br />
                            <table width="100%" border="0" cellspacing="1" cellpadding="0" bgcolor="#CCCCCC">

                                <tr class="List_results_menor">
                                    <td>
                                        <asp:Label Text="Código Aluno" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label Text="Cursados" runat="server"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label Text="Faltas" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label Text="Faltas Justificadas" runat="server"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label Text="A Cursar" runat="server"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label Text="Total" runat="server"></asp:Label>

                                    </td>

                                    <td>
                                        <asp:Label Text="% Pres." runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodAluno" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPresenca" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFaltas" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFaltasJustificadas" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblACursar" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotal" Font-Bold="true" runat="server"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lblMedia" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>


                        <div class="centralizar" style="width: 98%; margin: 0 auto;">
                            <div class="text_titulo">
                                <h1>Notas
                                </h1>
                            </div>
                            <br />
                        </div>

                        <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:GridView ID="gridNotas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CaptionAlign="Top" CssClass="grid_Aluno" HorizontalAlign="Center"
                                    PageSize="15">
                                    <Columns>
                                        <asp:BoundField DataField="DisDescricao" HeaderText="Descrição" InsertVisible="False"
                                            ReadOnly="True" SortExpression="DisDescricao">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TurNome" HeaderText="Turma" InsertVisible="False"
                                            ReadOnly="True" SortExpression="TurNome">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NdiNota" HeaderText="Nota" InsertVisible="False"
                                            ReadOnly="True" SortExpression="NdiNota">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ConCodigo" HeaderText="Conceito" InsertVisible="False"
                                            ReadOnly="True" SortExpression="ConCodigo">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>


                                    </Columns>
                                    <HeaderStyle CssClass="Grid_Aluno" />
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                        PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                    <PagerStyle CssClass="nav_results" />
                                </asp:GridView>


                                <table class="centralizar" width="58%" style="align-items: center" bgcolor="#CCCCCC">

                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSomaNota" Text="Total" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <br />

                            </ContentTemplate>

                        </asp:UpdatePanel>





                    </asp:Panel>
                    <div class="centralizar" style="width: 98%; margin: 0 auto;">
                        <div class="text_titulo">
                            <%--<h1>Entrega de Controle de Frequência
                                </h1>--%>
                        </div>
                        <br />
                    </div>
                    <div class="formatoTela_02">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    Style="width: 98%;" CaptionAlign="Top" CssClass="list_results_Menor" HorizontalAlign="Center"
                                    OnDataBound="GridView_DataBound">
                                    <Columns>
                                        <asp:BoundField DataField="FreqReferencia" HeaderText="Referência" SortExpression="FreqReferencia" />
                                        <asp:BoundField DataField="FreqDataPrevEntrega" HeaderText="Previsão Entrega" SortExpression="FreqDataPrevEntrega"
                                            DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Situacao" HeaderText="Situação" SortExpression="Situacao">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FreqDataEntrega" HeaderText="Data Entrega" NullDisplayText="Não Disponível"
                                            SortExpression="FreqDataEntrega" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="List_results_menor" />
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                        PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                    <PagerStyle CssClass="nav_results" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:SqlDataSource ID="SDS_area_atuacao" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                        SelectCommand="SELECT * FROM [CA_AreaAtuacao]"></asp:SqlDataSource>
                    <asp:HiddenField ID="HFRowCount" runat="server" />
                </asp:View>
                <asp:View ID="View7" runat="server">
                    <% var exibe = Session["enable_Save"];
                       if ((string)exibe == "User")
                       { %>
                    <div class="controls">
                        <%--<div style="float: right;">
                                <asp:Button ID="btn_novo_itinerario" runat="server" CssClass="btn_novo" Text="Adicionar Itinerário"
                                    OnClick="btn_novo_itinerario_Click" />
                            </div>--%>
                    </div>
                    <% } %>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <%--<asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    Style="width: 80%;" CaptionAlign="Top" CssClass="list_results_Menor" HorizontalAlign="Center"
                                    OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                    <Columns>
                                       <asp:BoundField DataField="ItnNome" HeaderText="Itinerário" SortExpression="ItnNome" />
                                        <asp:BoundField DataField="VtpLinha" HeaderText="Linha" SortExpression="VtpLinha">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VtpTarifa" HeaderText="Tarifa (R$)" SortExpression="VtpTarifa"
                                            DataFormatString="{0:F2}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VtpQuantidade" HeaderText="Quantidade" SortExpression="VtpQuantidade">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                            ShowSelectButton="True">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="VtpOrdem" HeaderText="Ordem" Visible="False" />
                                       <asp:TemplateField HeaderText="Excluir">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VtpOrdem")%>'
                                                    OnClientClick="javascript:GetConfirmItinerario();" OnClick="IMBexcluir_Click"
                                                    ImageUrl="~/images/icon_remove.png" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="List_results_menor" />
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                        PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                    <PagerStyle CssClass="nav_results" />
                                </asp:GridView>--%>
                            <%-- <asp:Panel ID="pn_info_vale" runat="server" CssClass="centralizar" Height="200px"
                                    Visible="false" Width="500px">
                                    <div class="text_titulo" style="margin-top: 100px;">
                                        <h1>Não existem itinerários cadastrados para este aprendiz.</h1>
                                    </div>
                                </asp:Panel>--%>
                            <%-- <asp:Panel ID="PN_Vale" runat="server" Visible="false">
                                    <div class="centralizar" style="width: 60%; margin: 0 auto;">
                                        <div class="cadastro_pesquisa" style="height: 260px;">
                                            <div class="titulo cortitulo corfonte" style="height: 20px; font-size: large;">
                                                Cadastro de Itinerário
                                            </div>
                                            <div class="linha_cadastro">
                                                <span class="fonteTab">Itinerário: </span>
                                                <br />
                                                <asp:DropDownList Style="margin: 2px 0 0 0" ID="dd_itinerarios" CssClass="fonteTexto"
                                                    OnDataBound="IndiceZero" Height="18px" Width="200px" DataSourceID="SDS_Itinerarios"
                                                    DataTextField="ItnNome" DataValueField="ItnCodigo" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="linha_cadastro">
                                                <span class="fonteTab">Linha: </span>
                                                <br />
                                                <asp:TextBox ID="tb_linha_trp" Height="13px" Width="60px" MaxLength="10" CssClass="fonteTexto"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="linha_cadastro">
                                                <span class="fonteTab">Tarifa: </span>
                                                <br />
                                                <asp:TextBox ID="tb_tarifa_trp" Height="13px" onkeyup="formataValor(this,event);"
                                                    Width="60px" MaxLength="10" CssClass="fonteTexto" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="linha_cadastro">
                                                <span class="fonteTab">Quantidade: </span>
                                                <br />
                                                <asp:TextBox ID="tb_quantidade" Height="13px" onkeyup="formataInteiro(this,event);"
                                                    Width="60px" MaxLength="3" CssClass="fonteTexto" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="linha_cadastro" style="margin-top: 20px;">
                                                <div class="centralizar">
                                                    <asp:Button ID="btn_next" runat="server" CssClass="btn_novo" Text="Confirmar" OnClick="btn_next_Click" />
                                                    <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btn_voltar_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:SqlDataSource ID="SDS_Itinerarios" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                                        SelectCommand="SELECT * FROM CA_Itinerarios"></asp:SqlDataSource>
                                </asp:Panel>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:View>
                <asp:View ID="View6" runat="server">


                    <asp:Panel runat="server" Visible="false">
                        <div class="text_titulo">
                            <h1>Demonstrativo de Notas e Faltas do Jovem</h1>
                        </div>
                        <br />
                        <asp:UpdatePanel runat="server" ID="Panel1" CssClass="centralizar">
                            <ContentTemplate>
                                <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    Style="margin: auto;" CaptionAlign="Top" CssClass="list_results_Menor" DataSourceID="SDSDisciplinas"
                                    HorizontalAlign="Center" Width="98%">
                                    <Columns>
                                        <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina" SortExpression="DisDescricao">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DPDataInicio" HeaderText="Data Inicio" DataFormatString="{0:dd/MM/yyyy}"
                                            SortExpression="DPDataInicio">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DPDataTermino" HeaderText="Data Fim" DataFormatString="{0:dd/MM/yyyy}"
                                            SortExpression="DPDataTermino">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Professor" HeaderText="Educador" SortExpression="Professor">
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DieCompreensao" HeaderText="Compreensão" SortExpression="DieCompreensao">
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DieComLing" HeaderText="Comunic./Linguagem" SortExpression="DieComLing">
                                            <ItemStyle HorizontalAlign="Center" Width="14%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DiePostura" HeaderText="Postura" SortExpression="DiePostura">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DieParticipacao" HeaderText="Participação" SortExpression="DieParticipacao">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MediaFinal" HeaderText="Resultado" SortExpression="MediaFinal">
                                            <HeaderStyle Width="7%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DiaNumeroFaltas" HeaderText="Faltas" SortExpression="DiaNumeroFaltas">
                                            <HeaderStyle Width="7%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Destaque" HeaderText="Destaque" SortExpression="Destaque">
                                            <HeaderStyle Width="7%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Cronograma">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="IMB_Notas" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DpOrdem")  +"\n"+ 
                                            DataBinder.Eval(Container.DataItem, "Apr_Codigo")   %>'
                                                    OnClick="IMB_Notas_Click" ImageUrl="~/images/icon_edit.png" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="6%"></HeaderStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="List_results_menor" />
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                        PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                    <PagerStyle CssClass="nav_results" />
                                </asp:GridView>
                                <div style="margin: 10px 1% 0 1%; text-align: left;">
                                    <span class="fonteTab"><span style="color: #00589c">Legenda Resultado:</span>
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;<b>MB = </b>Muito Bom; &nbsp;&nbsp; <b>B = </b>Bom; &nbsp;&nbsp;
                                    <b>R = </b>Regular; &nbsp;&nbsp; <b>I = </b>Insuficiente; &nbsp;&nbsp; <b>O = </b>
                                        Não Lançado. </span>
                                </div>
                                <asp:SqlDataSource ID="SDSDisciplinas" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                                    SelectCommand="select * from dbo.View_Resultado_Final where Apr_Codigo = @RegAcad">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="RegAcad" SessionField="Matricula" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>



                    <div class="text_titulo">
                        <h1>Avaliações Realizadas</h1>
                    </div>
                    <br />
                    <asp:Panel ID="pn_info_pesquisa" runat="server" CssClass="centralizar" Height="100px"
                        Visible="false" Width="500px">
                        <div class="text_titulo" style="margin-top: 30px;">
                            <h1>Não existem avaliações realizadas para este jovem.</h1>
                        </div>
                    </asp:Panel>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" CssClass="centralizar">
                        <ContentTemplate>
                            <asp:GridView ID="GridView5" AutoGenerateColumns="False" runat="server" CssClass="list_results_Menor"
                                PageSize="10" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView5_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="PepCodigo" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"></asp:BoundField>
                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PesNome" HeaderText="Pesquisa">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepMes" HeaderText="Mes">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepAno" HeaderText="Ano">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepDataRealizada" HeaderText="Data Realização" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UsuNome" HeaderText="Realizado por">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Nota">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Notafinal").ToString().Equals("0")  ? 
                                            string.Format("{0:F1}", (float.Parse(DataBinder.Eval(Container.DataItem, "MediaNotas").ToString())/10))  :DataBinder.Eval(Container.DataItem, "Notafinal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:ButtonField ButtonType="Image" CommandName="Demonstrativo"
                                        HeaderText="Visualizar" ImageUrl="~/images/lupa.png" Text="Button">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>



                                </Columns>
                                <HeaderStyle CssClass="List_results_menor"></HeaderStyle>
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                    LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                                SelectCommand="select * from dbo.View_Resultado_Final where Apr_Codigo = @RegAcad">
                                <SelectParameters>
                                    <asp:SessionParameter Name="RegAcad" SessionField="Matricula" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:GridView ID="gridPesquisa" Visible="false" AutoGenerateColumns="False" runat="server" CssClass="list_results_Menor"
                                PageSize="10" AllowPaging="True" OnPageIndexChanging="gridPesquisa_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="QueTexto" HeaderText="Item">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OpcTexto" HeaderText="Resposta">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OpcNota" HeaderText="Nota">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="List_results_menor"></HeaderStyle>
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                    LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" />
                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </asp:View>
                <asp:View ID="View8" runat="server">
                    <div class="text_titulo">
                        <h1>Ocorrências do Jovem</h1>
                    </div>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pn_info_ocorrencia" runat="server" Width="500px" Height="150px" CssClass="centralizar"
                                Visible="false">
                                <div class="text_titulo" style="margin-top: 50px">
                                    <h1>Ocor/Advertencias.</h1>
                                </div>
                            </asp:Panel>
                            <asp:GridView ID="GridView6" CssClass="list_results_Menor" Width="90%" Style="margin: 10px 5% 0 5%;"
                                runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                                        <HeaderStyle Width="7%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descricao" HeaderText="Ocorrência">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Emissor" HeaderText="Emissor">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Responsavel" HeaderText="Responsável">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Observacao" HeaderText="Observação">
                                        <HeaderStyle Width="30%" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="List_results_menor" />
                                <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                    PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="text_titulo">
                        <h1>Cronograma Aluno</h1>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel2" runat="server" Width="500px" Height="150px" CssClass="centralizar"
                                Visible="false">
                                <div class="text_titulo" style="margin-top: 50px">
                                    <h1>Nenhuma ocorrência encontrada.</h1>
                                </div>
                            </asp:Panel>
                            <asp:GridView ID="gridCronogramaAluno" CssClass="list_results_Menor" Width="90%" Style="margin: 10px 5% 0 5%;"
                                runat="server" OnPageIndexChanging="gridCronogramaAluno_PageIndexChanging" AllowPaging="True" OnRowDataBound="gridCronogramaAluno_RowDataBound" AutoGenerateColumns="False" PageSize="15">
                                <Columns>
                                    <asp:BoundField DataField="AdiCodAprendiz" HeaderText="Cod Jovem">
                                        <HeaderStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Nome Jovem">
                                        <HeaderStyle Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TurNome" HeaderText="Nome da Turma">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina">
                                        <HeaderStyle Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EducNome" HeaderText="Edu. Nome">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AdiDataAula" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Aula">
                                        <HeaderStyle Width="7%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AdiPresenca" HeaderText="Presença">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AdiObservacoes" HeaderText="Observações">
                                        <HeaderStyle Width="3%" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="List_results_menor" />
                                <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                    PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                <PagerStyle CssClass="nav_results" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <img src="../images/IMGcorVerde.png" />
                    Presença
                        <br />
                    <img src="../images/IMGcorVermelha.png" />
                    Falta
                        <br />
                    <img src="../images/IMGcorAzul.png" />
                    A Cursar
                </asp:View>


                <asp:View ID="viewDocumentacao" runat="server">
                    <div class="text_titulo">
                        <h1>Documentos do Aluno</h1>
                    </div>
                    <br />
                    <asp:Panel runat="server" CssClass="centralizar">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnImportarArquivo" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:FileUpload ID="fileUploadArquivo" runat="server" />
                                <asp:Button ID="btnImportarArquivo" runat="server" Text="Carregar arquivo" OnClick="btnImportarArquivo_Click1" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table width="100%">
                            <tr id="trArquivoBaixar" runat="server">
                                <td>
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
                                        HorizontalAlign="Center" Width="65%" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="Nome_Arquivo" HeaderText="Arquivo">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" HeaderText="Download" SelectImageUrl="~/images/download.jpg"
                                                ShowSelectButton="True">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                        </Columns>
                                        <HeaderStyle CssClass="List_results" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:View>
            </asp:MultiView>
            <asp:HiddenField runat="server" ID="HFConfirma" />
        </div>
    </div>


</asp:Content>
