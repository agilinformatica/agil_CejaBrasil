<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="RealocarAprendizes.aspx.cs" Inherits="ProtocoloAgil.pages.RealocarAprendizes" %>
<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../Styles/cdl_bh.css" />
    <link href="../Styles/pesquisa.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Aprendizes > <span>Realocação de Aprendizes</span></p>
    </div>
    <div class="formatoTela_02">
        <asp:MultiView runat="server" ID="MultiView1">
            <asp:View runat="server" ID="View2">
                <div class="conteudo">
                    <div class="cadastro_pesquisa" style="height: 160px; margin: 10px 0 10px 0;">
                        <div class="titulo corfonte cortitulo">
                            Selecione as turmas de origem e destino</div>
                        <div class="linha_cadastro" style="float: left; margin-top:5px;  width: 70%;">
                            <span class="fonteTab" style="display: block; float: left; width: 100px;">Curso:
                            </span>
                            <asp:DropDownList ID="DDcurso_origem" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" CssClass="fonteTexto" DataTextField="CurDescricao" DataValueField="CurCodigo"
                                Height="18px" OnDataBound="IndiceZero" OnSelectedIndexChanged="DDcursos_SelectedIndexChanged"
                                Width="350px">
                            </asp:DropDownList>
                        </div>
                        <div class="linha_cadastro" style="float: left; margin-top:5px;  width: 70%;">
                            <span class="fonteTab" style="display: block; float: left; width: 100px;">Turma Origem:
                            </span>
                            <asp:DropDownList ID="DDturma_origem" runat="server" AppendDataBoundItems="True"
                                CssClass="fonteTexto" DataTextField="TurNome" Style="float: left;" DataValueField="TurCodigo"
                                Height="18px" OnDataBound="IndiceZero" ViewStateMode="Enabled" Width="170px">
                            </asp:DropDownList>
                        </div>
                        <div class="linha_cadastro" style="float: left; margin-top:5px;  width: 70%;">
                            <span class="fonteTab" style="display: block; float: left; width: 100px;">Curso Destino:
                            </span>
                            <asp:DropDownList ID="DDcurso_destino" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" CssClass="fonteTexto" DataTextField="CurDescricao" DataValueField="CurCodigo"
                                Height="18px" OnDataBound="IndiceZero" OnSelectedIndexChanged="DDcursos_destino_SelectedIndexChanged"
                                Width="350px">
                            </asp:DropDownList>
                        </div>
                        <div class="linha_cadastro" style="float: left; margin-top:5px;  width: 70%;">
                            <span class="fonteTab" style="display: block; float: left; width: 100px;">Turma Destino:
                            </span>
                            <asp:DropDownList ID="DDturma_destino" runat="server" AppendDataBoundItems="True"
                                CssClass="fonteTexto" DataTextField="TurNome" DataValueField="TurCodigo" Height="18px"
                                OnDataBound="IndiceZero" ViewStateMode="Enabled" Width="170px">
                            </asp:DropDownList>
                        </div>
                         <div class="linha_cadastro" style="float: left; margin-top:5px; width: 70%;">
                              <span class="fonteTab" style="display: block; float: left; width: 100px;">Data Inicio: </span>
                                <asp:TextBox ID="TBDataInicio" runat="server" style="float: left;" CssClass="fonteTexto" Height="13px" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                        MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                                        PopupPosition="BottomRight" runat="server" TargetControlID="TBDataInicio">
                                    </cc2:CalendarExtenderPlus>

                              <span class="fonteTab" style="display: block;margin-left:40px; float: left; width: 100px;">Data Término: </span>
                                <asp:TextBox ID="TBDataFinal" runat="server" style="float: left;" CssClass="fonteTexto" Height="13px" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                        MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1"
                                        PopupPosition="BottomRight" runat="server" TargetControlID="TBDataFinal">
                                    </cc2:CalendarExtenderPlus>

                         </div>

                        <div class="linha_cadastro" style="float: right; margin-top: -80px; width: 20%;">
                            <asp:Button runat="server" ID="btn_pesquisa" CssClass="btn_novo" Text="Pesquisar"
                                OnClick="btn_pesquisa_Click" />
                        </div>
                    </div>
                    <div class="lista_turma">
                        <div class="text_titulo" style="width: 100%; clear: both;">
                            <h1>
                                Turma de Origem</h1>
                        </div>
                        <asp:ListBox ID="lb_Turma" CssClass="listbox" runat="server" SelectionMode="Multiple">
                        </asp:ListBox>
                    </div>
                    <div class="botoes">
                        <asp:Button ID="btn_incluir" runat="server" Text=">>" ToolTip="incluir selecionados"
                            OnClick="btn_incluir_Click" /><br />
                        <br />
                        <br />
                        <asp:Button ID="btn_retirar" runat="server" Text="<<" ToolTip="excluir selecionados"
                            OnClick="btn_retirar_Click" />
                    </div>
                    <div class="lista_evento">
                        <div class="text_titulo" style="width: 100%; clear: both;">
                            <h1>
                                Turma de Destino</h1>
                        </div>
                        <asp:ListBox ID="lb_participantes" CssClass="listbox" SelectionMode="Multiple" runat="server">
                        </asp:ListBox>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="HFEvento" />
                <div class="controls" style="width: 90%; margin: 0 auto 20px auto;">
                    <div style="text-align: center; margin: 0 auto;">
                        <asp:Button runat="server" ID="Bt_Confirmar" CssClass="btn_novo" Text="Confirmar"
                            OnClick="Bt_Confirmar_Click" />
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>