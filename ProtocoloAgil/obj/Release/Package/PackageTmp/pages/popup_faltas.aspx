<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup_faltas.aspx.cs" Inherits="ProtocoloAgil.pages.popup_faltas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/cdl_bh.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Mascara.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="content">
    <div class="popup" style="width: 500px; ">
             <div class="text_titulo">
             <h1> Cronograma da Disciplina  </h1>
             </div>
             <br/>
             <div class="fonteTab">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Label runat="server" ID="LB_disciplina"></asp:Label>
             </div>
             <br/>
                     <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CaptionAlign="Top" CssClass="list_results"   PageSize="8" Style="width: 50%; margin: auto" 
                        HorizontalAlign="Center" 
                 onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" >
                         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="Codigo" HeaderText="Data" >
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Descricao" HeaderText="Frequência" >
                                <HeaderStyle Width="50%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>
                         <EditRowStyle BackColor="#999999" />
                         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" 
                            LastPageImageUrl="~/images/seta_ultimo.jpg" />
                        <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                         <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                         <SortedAscendingCellStyle BackColor="#E9E7E2" />
                         <SortedAscendingHeaderStyle BackColor="#506C8C" />
                         <SortedDescendingCellStyle BackColor="#FFFDF8" />
                         <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>    
                    <br/>
                      <br/>
    </div>
     </div>
    </form>
</body>
</html>
