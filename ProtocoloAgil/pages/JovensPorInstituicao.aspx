<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" EnableEventValidation="false"
    Inherits="ProtocoloAgil.pages.JovensPorInstituicao" CodeBehind="JovensPorInstituicao.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
     
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Aprendiz > <span>Jovens por instituições</span></p>
        </div>
        <div class="controls">
            <div style="float: left;">
                
                <asp:Button ID="BtnImprimir" runat="server" CssClass="btn_controls" OnClick="BtnImprimir_Click" Text="Imprimir" />
               
            </div>
            <div style="float: right;">
                <asp:DropDownList runat="server" Width="80%" CssClass="fonteTab" ID="DDInstituicaoParceira" OnDataBound="IndiceZero" DataValueField="IpaCodigo" DataTextField="IpaDescricao"></asp:DropDownList>
                <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                    OnClick="btnpesquisa_Click" />
            </div>
        </div>
    </asp:Panel>
     <div class="formatoTela_02">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
         <br/>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CssClass="list_results" Height="16px" 
                        Style="width: 60%; margin: auto" HorizontalAlign="Center" 
                        onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                          

                              <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>

                              <asp:BoundField DataField="IpaDescricao" HeaderText="Descrição" SortExpression="IpaDescricao">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>

                              <asp:BoundField DataField="StaDescricao" HeaderText="Situação" SortExpression="StaDescricao">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>

                           
                           


                          
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                        <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <br />
                    <asp:HiddenField runat="server" ID="HFConfirma" />
                    <asp:HiddenField ID="HFRowCount" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
           
        </asp:View>
        
        <asp:View ID="View3" runat="server">
            <div class="centralizar">
                <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1">
                </iframe>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>
</asp:Content>