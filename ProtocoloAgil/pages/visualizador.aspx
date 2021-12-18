<%@ Page Language="C#" AutoEventWireup="true" Inherits="ProtocoloAgil.pages.Visualizador"
    CodeBehind="Visualizador.aspx.cs" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <title></title>
         <style>
        .print
        {
            position: absolute;
            left: 90%;
            top: 10px;
            z-index: 1000;
        }
        
        .image
        {
            width: 20px;
            height: 20px;
            padding-top: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="print">
        <asp:ImageButton CssClass="image" runat="server" ID="IMB_Print" ImageUrl="../images/cs_print.gif"
            OnClick="IMB_Print_Click" Visible="False" />
    </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="875px">
        </rsweb:ReportViewer>
        <br />
    <br />
    </form>
  
</body>

</html>
