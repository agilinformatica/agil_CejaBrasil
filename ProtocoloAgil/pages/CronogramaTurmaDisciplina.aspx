<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="CronogramaTurmaDisciplina.aspx.cs" Inherits="ProtocoloAgil.pages.CronogramaTurmaDisciplina" %>

<%@ Register Assembly="CalendarExtenderPlus" Namespace="AjaxControlToolkitPlus" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            width: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Pedagógico > <span>Geração de Cronograma da Turma/Disciplina</span>
        </p>
    </div>

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">

            
            <br />
            <table class="FundoPainel centralizar" style="width: 85%; border: 1px #7f7f7f solid">
                <tr>
                    <td class="titulo cortitulo corfonte" colspan="7" style="font-size: large;">Geração de Cronograma da Turma/Disciplina</td>
                </tr>
                <tr>
                    <td class="espaco"></td>
                    <td class="espaco"></td>
                    <td class="espaco" colspan="2"></td>
                    <td class="espaco" colspan="2"></td>

                </tr>
                <tr>
                    <td class="Tam10 fonteTab" style="text-align: right;">Turma: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab">
                        <asp:DropDownList ID="DDTurma" runat="server" AppendDataBoundItems="True" CssClass="fonteTexto" DataTextField="TurNome" DataValueField="TurCodigo" Height="18px" ViewStateMode="Enabled" Width="80%">
                        </asp:DropDownList>
                        &nbsp; </td>
                    <td class="Tam10 fonteTab" style="text-align: right;">Disciplina: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab">
                        <asp:DropDownList ID="DDDisciplina" runat="server" AppendDataBoundItems="True" CssClass="fonteTexto" DataTextField="DisDescricao" DataValueField="DisCodigo" Height="18px" ViewStateMode="Enabled" Width="90%">
                        </asp:DropDownList>
                        &nbsp; </td>

                    <td class="Tam10 fonteTab" style="text-align: right;">Professores: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab">
                        <asp:DropDownList ID="DDProfessores" runat="server" AppendDataBoundItems="True" CssClass="fonteTexto" DataTextField="EducNome" DataValueField="EducCodigo" Height="18px" ViewStateMode="Enabled">
                        </asp:DropDownList>
                        &nbsp; </td>
                </tr>
                <tr>
                    <td class="Tam15 fonteTab" style="text-align: right;" >Quantidade: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab">
                        <asp:TextBox ID="txtQuantidade" runat="server" onkeyup="formataInteiro(this, event);" onkeydown="ModifyEnterKeyPressAsTab();" CssClass="fonteTexto" Height="15px"
                            Width="128px" MaxLength="8"></asp:TextBox>
                    </td>
                    <td class="Tam10 fonteTab" >Data Início: &nbsp;&nbsp; </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="10" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="TBdata_inicio_CalendarExtenderPlus" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataInicio">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td class="Tam10 fonteTab" style="text-align: right;">Sequência: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab">
                        <asp:DropDownList ID="DDSequencia" runat="server" CssClass="fonteTexto" Height="18px" ViewStateMode="Enabled" Width="40%">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Button ID="btnGerar" runat="server" OnClick="btnGerar_Click" CssClass="btn_novo" Text="Gerar Cronograma" />
                        &nbsp;
                        <asp:Button ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click" CssClass="btn_novo" Text="Pesquisar" />
                    </td>
                </tr>
                <tr>
                    <td class="espaco" colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="espaco" colspan="7">&nbsp;</td>
                </tr>
            </table>
        </asp:View>


        <asp:View ID="View2" runat="server">
            <br />
            <asp:Button ID="btnVoltar"  runat="server" OnClick="btnVoltar_Click" CssClass="btn_novo" Text="Voltar" />
            <asp:Button ID="btnImprimir"  runat="server" OnClick="btnImprimir_Click" CssClass="btn_novo" Text="Imprimir" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list_results"
                        AutoGenerateColumns="False"
                        Style="width: 70%; margin: 0 auto"
                        OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="ADPDataAula" HeaderText="Data Aula"
                                InsertVisible="False" ReadOnly="True" SortExpression="ADPDataAula"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ADPOrdemAula" HeaderText="Sequência" SortExpression="ADPOrdemAula">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina" SortExpression="DisDescricao">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField DataField="EducNome" HeaderText="Professor" SortExpression="EducNome">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TurObservacoes" HeaderText="Turma" SortExpression="TurObservacoes">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle Height="25px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" BackColor="#999999" />
                        <FooterStyle Width="30px" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Height="20px" BorderStyle="Groove" BorderWidth="1px" />
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
                    <asp:HiddenField runat="server" ID="HFConfirma" />
                    <asp:HiddenField ID="HFRowCount" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>


         <asp:View runat="server" ID="View3">
        <div class="controls">
            <div style="float:right;margin-right:30px; ">
                <asp:Button runat="server" ID="btn_voltar" CssClass="btn_novo" Text="Voltar" 
                    onclick="btnVoltar2_Click" />
            </div>
        </div>
            <div class="centralizar">
                <iframe runat="server" id="Iframe1" src="visualizador.aspx" class="VisualFrame">
                </iframe>
            </div>
        </asp:View>

    </asp:MultiView>



</asp:Content>
