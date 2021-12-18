<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="Cronograma.aspx.cs" Inherits="ProtocoloAgil.pages.Cronograma" %>

<%@ Register Assembly="CalendarExtenderPlus" Namespace="AjaxControlToolkitPlus" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 50%;
        }

        .auto-style2 {
            width: 9%;
        }

        .auto-style3 {
            height: 2%;
        }

        .auto-style4 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            font-weight: bold;
            color: #5E5E5E;
            margin-left: 0px;
            height: 2%;
        }

        .auto-style5 {
            width: 9%;
            height: 2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Pedagógico > <span>Cronograma</span>
        </p>
    </div>
    <div class="controls">
        <div style="float: left;">
            <asp:Button ID="btn_cronograma" runat="server" CssClass="btn_controls" Text="Cronogramas" OnClick="Button1_Click" />
            <asp:Button ID="btn_Intervalo" runat="server" CssClass="btn_controls"
                Text="Cronograma por Intervalo" OnClick="btn_Intervalo_Click" />

            <asp:Button ID="btnCronogramaDisciplina" runat="server" CssClass="btn_controls" OnClick="btnCronogramaDisciplina_Click" Text="Cronograma por Disciplina" />
            <asp:Button ID="btnCores" runat="server" CssClass="btn_controls" Text="Cores Disciplinas" OnClick="btnCores_Click" />
            <asp:Button ID="btnDisciplinasTurma" runat="server" CssClass="btn_controls" Text="Cronograma Disciplinas/Turma" OnClick="btnDisciplinasTurma_Click" />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">

            <br />
            <table class="FundoPainel centralizar" style="width: 800px; border: 1px #7f7f7f solid">
                <tr>
                    <td class="titulo cortitulo corfonte" colspan="7" style="font-size: large;">Consulta de Cronograma Turma</td>
                </tr>
                <tr>
                    <td class="espaco"></td>
                    <td class="espaco"></td>
                    <td class="espaco" colspan="2"></td>
                    <td class="espaco" colspan="2"></td>
                    <td rowspan="5" style="width: 15%;">
                        <asp:Button ID="btnCronogramaTurma" runat="server" CssClass="btn_novo" OnClick="btnCronogramaTurma_Click" Text="Pesquisar" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;"></td>
                    <td class="Tam10 fonteTab" style="text-align: right;">Turma: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab" colspan="4">
                        <asp:DropDownList ID="DDturma_pesquisa" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="fonteTexto" DataTextField="TurNome" DataValueField="TurCodigo" Height="18px" ViewStateMode="Enabled" Width="30%">
                        </asp:DropDownList>
                        &nbsp; </td>
                </tr>
                <tr>
                    <td style="width: 5%;">&nbsp;</td>
                    <td class="Tam10 fonteTab" style="text-align: right;">Data Inicial: &nbsp;&nbsp; </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="tb_inicio_pesquisa" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="10" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="TBdata_inicio_CalendarExtenderPlus" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="tb_inicio_pesquisa">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td class="auto-style2" colspan="2">Data Final: </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="tb_final_pesquisa" runat="server" CssClass="fonteTexto" Height="13px" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus1" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="tb_final_pesquisa">
                        </cc2:CalendarExtenderPlus>
                    </td>
                </tr>
                <tr>
                    <td class="espaco" colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="espaco" colspan="7">&nbsp;</td>
                </tr>
            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <div class="centralizar">
                                <iframe id="IFrame2" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="View2" runat="server">
            <br />
            <table class="FundoPainel centralizar" style="width: 800px; border: 1px #7f7f7f solid">
                <tr>
                    <td class="titulo cortitulo corfonte" colspan="6" style="font-size: large;">Cronograma por intervalo</td>
                </tr>
                <tr>
                    <td class="espaco"></td>
                    <td class="espaco" colspan="2"></td>
                    <td class="espaco" colspan="2"></td>
                    <td rowspan="5" style="width: 15%;">&nbsp;
                        <asp:Button ID="Button1" runat="server" CssClass="btn_novo" OnClick="Button2_Click" Text="Pesquisar" />
                    </td>
                </tr>
                <tr>
                    <td class="Tam10 fonteTab" style="text-align: right;">&nbsp;</td>
                    <td class="Tam40 fonteTab" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="Tam10 fonteTab" style="text-align: right;">Data Inicial: &nbsp;&nbsp; </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="DataInicial" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="10" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="DataInicial">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td class="auto-style2" colspan="2">Data Final: </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="DataFinal" runat="server" CssClass="fonteTexto" Height="13px" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus3" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="DataFinal">
                        </cc2:CalendarExtenderPlus>
                    </td>
                </tr>
                <tr>
                    <td class="espaco" colspan="6">&nbsp;</td>
                </tr>
                <tr>
                    <td class="espaco" colspan="6">&nbsp;</td>
                </tr>
            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <div class="centralizar">
                                <iframe id="IFrame1" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="View3" runat="server">

            <br />
            <table class="FundoPainel centralizar" style="width: 800px; border: 1px #7f7f7f solid">
                <tr>
                    <td class="titulo cortitulo corfonte" colspan="7" style="font-size: large;">Consulta de Cronograma Disciplina</td>
                </tr>
                <tr>
                    <td class="espaco"></td>
                    <td class="espaco"></td>
                    <td class="espaco" colspan="2"></td>
                    <td class="espaco" colspan="2"></td>
                    <td rowspan="5" style="width: 15%;">&nbsp;
                        <asp:Button ID="BtnDiscPesquisar" runat="server" CssClass="btn_novo" OnClick="BtnDiscPesquisar_Click" Text="Pesquisar" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;"></td>
                    <td class="Tam10 fonteTab" style="text-align: right;">Disciplina: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab" colspan="4">
                        <asp:DropDownList ID="DDDisciplina" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="fonteTexto" DataTextField="DisDescricao" DataValueField="DisCodigo" Height="18px" ViewStateMode="Enabled" Width="55%">
                        </asp:DropDownList>
                        &nbsp; </td>
                </tr>
                <tr>
                    <td class="Tam05"></td>
                    <td class="auto-style3" style="text-align: right;">Data Inicial: &nbsp;&nbsp; </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TB_DiscDataInicial" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="10" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus4" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="TB_DiscDataInicial">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td class="auto-style5" colspan="2">Data Final: </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TB_DiscDataFinal" runat="server" CssClass="fonteTexto" Height="13px" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus5" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="TB_DiscDataFinal">
                        </cc2:CalendarExtenderPlus>
                    </td>
                </tr>
                <tr>
                    <td class="espaco" colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="espaco" colspan="7">&nbsp;</td>
                </tr>
            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                            <div class="centralizar">
                                <iframe id="IFrame3" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="View4" runat="server">

            <br />
            <table class="FundoPainel centralizar" style="width: 800px; border: 1px #7f7f7f solid">
                <tr>
                    <td class="titulo cortitulo corfonte" colspan="5" style="font-size: large;">Consulta Cores Disciplinas</td>
                </tr>
                <tr>
                    <td class="espaco"></td>
                    <td class="espaco"></td>
                    <td class="espaco"></td>
                    <td class="espaco"></td>
                    <td rowspan="3" style="width: 15%;">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="espaco" colspan="5">
                        <asp:Button ID="BtnGerarCores" runat="server" CssClass="btn_novo" OnClick="BtnGerarCores_Click" Text="Pesquisar" Width="87px" />
                    </td>
                </tr>
                <tr>
                    <td class="espaco" colspan="5">&nbsp;</td>
                </tr>
            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel4" runat="server" Visible="false">
                            <div class="centralizar">
                                <iframe id="IFrame4" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="View5" runat="server">

            <br />
            <table class="FundoPainel centralizar" style="width: 800px; border: 1px #7f7f7f solid">
                <tr>
                    <td class="titulo cortitulo corfonte" colspan="7" style="font-size: large;">Consulta de Cronograma Turma Disciplina</td>
                </tr>
                <tr>
                    <td class="espaco"></td>
                    <td class="espaco"></td>
                    <td class="espaco" colspan="2"></td>
                    <td class="espaco" colspan="2"></td>
                    <td rowspan="5" style="width: 15%;">
                        <asp:Button ID="btnCronogramaTurmaDisciplina" runat="server" CssClass="btn_novo" OnClick="btnCronogramaTurmaDisciplina_Click" Text="Pesquisar" />
                        &nbsp;
                    </td>
                </tr>
                <tr>

                    <td class="Tam10 fonteTab" style="text-align: right;">Turma: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab" colspan="1">
                        <asp:DropDownList ID="DDTurma" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="fonteTexto" DataTextField="TurNome" DataValueField="TurCodigo" Height="18px" ViewStateMode="Enabled" Width="50%">
                        </asp:DropDownList>
                        &nbsp; </td>


                    <td class="Tam10 fonteTab" style="text-align: right;">Disciplina: &nbsp;&nbsp; </td>
                    <td class="Tam40 fonteTab" colspan="4">
                        <asp:DropDownList ID="DDDisciplinaTurma" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="fonteTexto" DataTextField="DisDescricao" DataValueField="DisCodigo" Height="18px" ViewStateMode="Enabled" Width="65%">
                        </asp:DropDownList>
                        &nbsp; </td>
                </tr>
                <tr>
                    
                    <td class="Tam10 fonteTab" style="text-align: right;">Data Inicial: &nbsp;&nbsp; </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="10" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus6" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataInicio">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td class="Tam10 fonteTab" colspan="2">Data Final: </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="txtDataTermino" runat="server" CssClass="fonteTexto" Height="13px" Width="100px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus7" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataTermino">
                        </cc2:CalendarExtenderPlus>
                    </td>
                </tr>
                <tr>
                    <td class="espaco" colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="espaco" colspan="7">&nbsp;</td>
                </tr>
            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel5" runat="server" Visible="false">
                            <div class="centralizar">
                                <iframe id="IFrame2" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>



</asp:Content>
