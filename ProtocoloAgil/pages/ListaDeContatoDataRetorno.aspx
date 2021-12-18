<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" EnableEventValidation="false"
    Inherits="ProtocoloAgil.pages.ListaDeContatoDataRetorno" CodeBehind="ListaDeContatoDataRetorno.aspx.cs" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function setSessionVariable(valueToSetTo) {
            window.__doPostBack('SetSessionVariable', valueToSetTo);
        }

        function change(href) {
            window.location.href = href;
        }

        function popup(url) {
            $("#lightbox").css("display", "inline");


            var x = (screen.width - 600) / 2;
            var y = (screen.height - 450) / 2;
            var newwindow;
            newwindow = window.open(url, "Cadastro", "status=no, scrollbar=1, width=600,height= 450,resizable = 1,top=" + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este contato?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

    </script>

      <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="Table">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Lista contatos por previsão retorno</span>
            </p>
        </div>
        <div class="linha">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo" Visible="false" />


            </div>
            <div style="float: right;">
                <table>
                    <tr>

                        <td>
                            <span class="fonteTab">Data Início</span>
                        </td>
                        <td>
                            <span class="fonteTab">Data Término</span>
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox ID="txtDataInicio" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataInicio">
                            </cc2:CalendarExtenderPlus>
                        </td>
                           <td>
                            <asp:TextBox ID="txtDataTermino" runat="server" CssClass="fonteTexto" Height="13px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataTermino">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td>

                            <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                                OnClick="btnpesquisa_Click" />
                        </td>
                    </tr>
                </table>





            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                 <div class="text_titulo">
                <h1>
                    Lista de contatos por previsão de retorno

                </h1>
            </div>
            <br />
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="list_results"  OnRowDataBound="GridView1_RowDataBound"
                            Style="width: 75%; margin: auto" HorizontalAlign="Center"  EmptyDataText="Não existe registros cadastrados"  EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-BackColor="Silver"
                            OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                            OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>

                                 <asp:BoundField DataField="CacNome" HeaderText="Cliente" SortExpression="CacNome">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>


                                <asp:BoundField DataField="cocDataContato" HeaderText="Data Contato" DataFormatString="{0:dd/MM/yyyy}" SortExpression="cocDataContato">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Tco_Descricao" HeaderText="Tipo" SortExpression="CocTipo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>


                                <asp:BoundField DataField="CocPrevisaoRetorno" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Previsão Retorno" SortExpression="CocPrevisaoRetorno">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="FechDescricao" HeaderText="Fechamento" SortExpression="FechDescricao">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CocUsuarioContato" HeaderText="Usuário" SortExpression="CocUsuarioContato">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                  <asp:BoundField DataField="CocDescricaoContato" HeaderText="Descrição" SortExpression="CocDescricaoContato">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>


                                  <asp:BoundField DataField="CocResultadoContato" HeaderText="Resultado" SortExpression="CocResultadoContato">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Cor">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>


                                <asp:BoundField DataField="StcCodigo" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"></asp:BoundField>

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
                    <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
