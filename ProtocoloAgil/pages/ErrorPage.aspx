<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="ProtocoloAgil.pages.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../Styles/style.css" />
    <style>
        .Titulo
        {
            width: 50%; 
            height: 40px; 
            font-family: Arial Black; 
            font-size: 16pt;
            color: red; 
            float:left; 
            margin-top: 50px;
            margin-left:20px;
        }
        
        .Texto
        {
            width: 55%; 
            height: 200px; 
            font-family: Arial Black; 
            font-size: 13pt;
            color: black; 
            float: left; 
            margin-top: 20px;
            margin-left:20px;
        }
        
        .btn_novo{
	        height:30px;
	        font-size:11px;
	        font-weight:bold;
	        background:#4b8efb;
	        color:#fff;
	        border: solid 1px #dfdfdf;
	        padding:8px 8px 8px 8px;
	    }

    </style>
</head>
<body>

    <form id="form1" runat="server">

    <div id="all_login">
    <div class="content">
        <div style="float: left;">
          <asp:Image ID="Image1" runat="server" Width="250px" ImageUrl="~/images/logofundacao.png" />
        </div>
        <div  class="Titulo">
        <asp:Label runat="server" id="LB_title" > O sistema se comportou de forma inesperada. </asp:Label>
        </div>
          <div  class="Texto"><br/>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label runat="server" id="LBInfo">  Um e-mail contendo informações sobre o ocorrido 
        foi enviado ao setor de suporte. Clique abaixo para voltar à tela inicial:
        </asp:Label>
        </div> 
         <div style="float: right;margin-right:10px;">
            <asp:Button ID="Button1" runat="server" CssClass="btn_novo" 
                 Text="Ir para Tela Inicial" onclick="Button1_Click" />
        </div>     
     </div>

        <div class="footer">
        	<div class="system_logo">
            	<a href="http://www.mestreagilweb.com.br" target="_blank">Mestre Ágil</a>
            </div>
            <div class="text_footer">
            	<p>Mestre Ágil - Gestão Acadêmica e Financeira  |  http://www.mestreagilweb.com.br</p>
				<p>Av. Álvares Cabral, 381, Conjunto 2104 - Lourdes - Belo Horizonte - MG  |  (31) 3889-0095  |  (31) 8498-6303</p>
            </div>
        </div>
    </div>

    </form>

</body>
</html>
