<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeBehind="AvaliacaoOrientadorExterna.aspx.cs" Inherits="ProtocoloAgil.AvaliacaoOrientadorExterna" %>









<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50731.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/cdl_bh.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Mascara.js" type="text/javascript"></script>
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <link href="../Styles/pesquisa.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <script src="Scripts/spin.js" type="text/javascript"></script>
    <script src="/Scripts/spin.js" type="text/javascript"></script>

    <style type="text/css">
        .auto-style1 {
            width: 50%;
        }

        .auto-style2 {
            height: 20px;
        }

        .auto-style3 {
            height: 20px;
        }

        .auto-style4 {
            height: 20px;
            width: 297px;
        }

        .auto-style5 {
            height: 20px;
            width: 79px;
        }

        .auto-style6 {
            width: 129px;
        }

        .auto-style7 {
            width: 129px;
            height: 20px;
        }

        .auto-style8 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            font-weight: bold;
            color: #5E5E5E;
            margin-left: 0px;
            height: 20px;
        }

        .error {
            text-align: left;
            text-indent: 30px;
        }

        .error_message {
            font: 7.5pt Verdana,sans-serif;
            color: #FF2222;
        }


        .pn_message {
            width: 600px;
            height: 400px;
            margin: 40px auto;
        }

        .Titulo {
            width: 50%;
            height: 40px;
            font-family: Arial Black;
            font-size: 16pt;
            color: red;
            float: left;
            margin-top: 50px;
            margin-left: 20px;
        }

        .Texto {
            width: 55%;
            height: 200px;
            font-family: Arial Black;
            font-size: 13pt;
            color: black;
            float: left;
            margin-top: 20px;
            margin-left: 20px;
            text-indent: 35px;
        }


        .foto {
            width: 14%;
            height: 160px;
            margin-left: 1%;
            margin-top: 5px;
        }

        .fotoLabel {
            margin-left: 20%;
            margin-top: -40px;
            height: 44px;
        }
    </style>



</head>


<body style="background: #ffffff;">
    <form id="form1" runat="server">
        <div id="foo"></div>





        <div runat="server" id="divPesquisa" >
            <div class="text_titulo">
                <h1>
                    <asp:Label runat="server" ID="Lb_Nome_pesquisa" /></h1>
            </div>
            <br />

            <div class="FundoPainel" style="height: 174px; border: solid 1px #484848; width:1000px; position:relative; left:130px; top: 0px;">

                <div class="foto">
                    <asp:ImageButton runat="server" ID="IMG_foto_aprendiz" Width="100%" Height="100%"
                        Style="margin: auto; z-index: 1; border: none;" />

                </div>

                <div class="fotoLabel">

                    <span class="fonteTab">Empresa: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_empresa" /><br />
                    <span class="fonteTab">Aprendiz: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Aprendiz" /><br />
                    <br />
                </div>
            </div>

            <br />
            <asp:Panel runat="server" ViewStateMode="Enabled" Width="800px" Style="margin: 0 auto;" ID="Pn_Pesquisa">
            </asp:Panel>
            <div style="width: 800px; margin: 0 auto;">
                <span class="fonteTab">Observações</span><br />
                <asp:TextBox ID="TB_observacao" runat="server" CssClass="fonteTexto" Height="40px"
                    onkeyup="javascript:IsMaxLength(this, 255);" TextMode="MultiLine" Width="600px"></asp:TextBox>
            </div>

            <br />
            <div class="error">
                <asp:Label CssClass="error_message" runat="server" ID="LB_erro" />
            </div>
            <div class="controls">
                <div class="centralizar">
                    <asp:Button ID="btn_salvar" runat="server" CssClass="btn_novo" Text="Salvar Avaliação"
                        OnClick="btn_salvar_Click" OnClientClick="this.disabled = true; this.value = 'Salvando...';" UseSubmitBehavior="false" />

                </div>
            </div>
        </div>

        <div class="pn_message" id="divSucesso" runat="server" visible="false">
            <div style="float: left;">

                <asp:Image ID="Image1" runat="server" Width="250px" ImageUrl="~/images/logofundacao.png" />
            </div>
            <div class="Titulo">
                <asp:Label runat="server" ID="LB_title" />
            </div>
            <div class="Texto">
                <br />
                <asp:Label runat="server" ID="LBInfo" />
            </div>
        </div>



    </form>

</body>
</html>
