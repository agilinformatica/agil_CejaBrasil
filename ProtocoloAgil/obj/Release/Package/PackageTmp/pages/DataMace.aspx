<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="DataMace.aspx.cs" Inherits="ProtocoloAgil.pages.DataMace" %>

<%@ Register Assembly="CalendarExtenderPlus" Namespace="AjaxControlToolkitPlus" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetConfirm() {
            ////  var hf = document.getElementById("<%--<%# HFConfirma.ClientID %>--%>");
            if (confirm("Deseja realmente excluir esta disciplina?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            font-weight: bold;
            color: #5E5E5E;
            margin-left: 0px;
            height: 2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            > <span>Arquivo Data Mace</span>
        </p>
    </div>
    <div class="controls">

        
    </div>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server">

            


            <asp:View ID="View2" runat="server">

                <div style="position: relative; top: -20px">
                    <div class="text_titulo">
                        <h1>Geração Data Mace</h1>
                    </div>
                    <br />
                    <table class="FundoPainel Table centralizar">
                        <tr>
                            <td class="Tam20 fonteTab">Data Início</td>
                            <td class="Tam20 fonteTab">Data Término</td>
                        </tr>
                        <tr>
                            <td class="Tam12 fonteTab">
                                <asp:TextBox ID="txtDataInicioPesquisa" runat="server" CssClass="fonteTexto" Height="18px"
                                    MaxLength="10"
                                    onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                                <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus1" runat="server" Format="dd/MM/yyyy"
                                    PopupPosition="BottomRight" TargetControlID="txtDataInicioPesquisa">
                                </cc2:CalendarExtenderPlus>

                            </td>

                            <td class="Tam12 fonteTab">
                                <asp:TextBox ID="txtDataFinalPesquisa" runat="server" CssClass="fonteTexto" Height="18px"
                                    MaxLength="10"
                                    onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                                <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server" Format="dd/MM/yyyy"
                                    PopupPosition="BottomRight" TargetControlID="txtDataFinalPesquisa">
                                </cc2:CalendarExtenderPlus>

                            </td>
                            
                            <td>
                                <asp:Button ID="btnGerar" runat="server" CssClass="btn_novo" Text="Gerar" OnClick="btnGerar_Click" />
                            </td>
                        </tr>
                        
                    </table>
                </div>


                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="fonteTab centralizar" 
                                style="width: 800px; text-align: left; padding-bottom: 8px;">
                                Arquivos</div>
                            <asp:Panel ID="PainelArquivo" runat="server" 
                                Style="margin: 0 auto; padding-top: 20px;" Width="800px">
                                <span class="fonteTab" style="color: #00599c;">Não há arquivos gerados.</span>
                            </asp:Panel>
                            <asp:GridView ID="gridArquivo" runat="server" AutoGenerateColumns="False" 
                                CssClass="list_results" OnSelectedIndexChanged="gridArquivo_SelectedIndexChanged"  HorizontalAlign="Center" 
                                Style="margin: auto" 
                                Width="800px" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                   
                                    <asp:BoundField DataField="Nome_Arquivo" HeaderText="Arquivo">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Image" HeaderText="Download" 
                                        SelectImageUrl="~/images/download.jpg" ShowSelectButton="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <br />
                           
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
               
            </asp:View>

           
        </asp:MultiView>
    </div>
</asp:Content>
