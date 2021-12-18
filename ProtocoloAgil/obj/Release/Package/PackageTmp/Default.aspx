<%@ Page Language="C#" AutoEventWireup="true" Inherits="ProtocoloAgil.Default" CodeBehind="Default.aspx.cs" %>
<%@ Import Namespace="ProtocoloAgil.Base" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestão do Programa Jovem Aprendiz - Login Operacional</title>
       <link type="text/css" rel="stylesheet" href="Styles/cdl_bh.css" />
    <script src="Scripts/Mascara.js" type="text/javascript"></script>
     <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
</head>
<body>
   <div id="header">
    <div id="cabecalho">
        <div class="logo02">
        </div>
        <div class="Nome">
            </div>
              <div id="sair" >
               <div class="userlinks">
            </div>
             </div>
               </div>
    </div>
    <div id="content" style="margin-top: -277px; height:420px;">
       <div id="lightbox" ></div>
        <div class="content_home" >
        <div class="text_titulo" style= "margin-top: 30px;">
        <h1>Gestão do Programa Jovem Aprendiz </h1>
        </div>
        <br/>
        <br/>
    
            <div class="center_login" style="; margin-left: 340px;">
                <form id="form1" name="login" runat="server" >
                <asp:scriptmanager runat="server" id="ScriptManager1" ></asp:scriptmanager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <div class="label_login">
                        Perfil:</div>
                        <div class="field_login">
                            <asp:DropDownList ID="DD_perfil" runat="server" AutoPostBack="true"
                                onselectedindexchanged="DD_perfil_SelectedIndexChanged">
                              <asp:ListItem Value="">Selecione</asp:ListItem>
                              <asp:ListItem Value="U">Usuário Interno</asp:ListItem>
                              <asp:ListItem Value="A">Aprendiz</asp:ListItem>
                              <asp:ListItem Value="E">Educador</asp:ListItem>
                               <asp:ListItem Value="P">Parceiro</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                       
                        <div class="label_login">
                            Login:</div>
                        <div class="field_login">
                            <asp:TextBox ID="TBnome" runat="server"></asp:TextBox>
                              <asp:TextBox ID="TBnome_matricula" Visible="false" onkeyup="formataInteiro(this,event);" runat="server"></asp:TextBox>
                        </div>
                        <div class="label_login">
                            Senha:</div>
                        <div class="field_login">
                            <asp:TextBox ID="TBsenha" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="links_pass">
                            <asp:LinkButton runat="server"  ID="Label1" OnClick="LinkButton2_Click">Deseja alterar sua senha? </asp:LinkButton>
                            <asp:LinkButton runat="server"  ID="LKrelembraSenha"  OnClientClick="javascript:CreateWheel('yes');" OnClick="LinkButton1_Click">Esqueceu sua senha?</asp:LinkButton>
                            <asp:LinkButton runat="server" Visible="false" Font-Bold="true" Font-Size="Large"  ID="btnNovoUsuario"  OnClientClick="javascript:CreateWheel('yes');" OnClick="btnNovoUsuario_Click">Inscrição</asp:LinkButton>
                            <asp:LinkButton runat="server" Visible="false" ID="btnTeste"  OnClientClick="javascript:CreateWheel('yes');" OnClick="btnTeste_Click">teste questionário</asp:LinkButton>
                        </div>
                        <div class="button_login">
                            <asp:Button ID="login" runat="server" OnClick="Button1_Click" Text="Login" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </form>
            </div>
        </div>

         <asp:MultiView ID="MultiView1" runat="server" >
              <asp:View ID="View6" runat="server">
                     <div style="float: right; margin-right: 85px">
                        
                    </div>
                    <div class="centralizar">
                        <iframe id="IFrame1" height="870px" width="98%" style="border: none;" name="IFrame1"
                            src="CadastroAprendizInicial.aspx"></iframe>
                    </div>
                </asp:View>
             </asp:MultiView>



    <div class="footer">
           <div class="content_footer">
             <div class="copyright"> 
                <p>
                        Ceja Brasil - SP </p>
                    <p>
                        Desenvolvido por Agil Sistemas | <a href="http://www.agilsist.com.br"> http://www.agilsist.com.br</a> </p>
             </div>
              </div>
            </div>
        </div>
</body>
<script src=" <%= ResolveUrl("Scripts/spin.js") %>" type="text/javascript"></script>
</html>
