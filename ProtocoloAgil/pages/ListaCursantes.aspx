<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="ListaCursantes.aspx.cs" Inherits="ProtocoloAgil.pages.ListaCursantes" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<%@ Import Namespace="ProtocoloAgil.Base" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <script type="text/javascript">
        function popup(url, width, height) {
            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "Contrato", "status=no, scrollbar=yes, toolbar=no,menubar=no, width= " + width + ",height=  " + height + ",resizable=yes,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            width: 50%;
        }

        .auto-style2 {
            width: 126px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Pedagógico > <span>Aprendizes por Turma</span>
        </p>
    </div>
    <div class="controls">
        <div style="float: left;">
            <asp:Button ID="btn_list" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
            <asp:Button ID="btn_print" runat="server" CssClass="btn_controls" Text="Documentos Turma" OnClick="btn_print_Click" />
            <asp:Button ID="BCarometro" runat="server" CssClass="btn_controls" Text="Carômetro" OnClick="BCarometro_Click" />
        </div>
    </div>

    <asp:Panel runat="server" ID="Pn_aprendiz" Visible="false">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <span class="fonteTab">Matrícula: </span>
        <asp:Label runat="server" CssClass="fonteTexto" ID="lb_codigo_aprendiz" />
        &nbsp;&nbsp;&nbsp;&nbsp;
                <span class="fonteTab">Nome: </span>
        <asp:Label runat="server" CssClass="fonteTexto" ID="lb_nome_aprendiz" />
        <asp:Button ID="Button3" runat="server" CssClass="btn_novo" Text="Voltar"
            OnClick="Button1_Click" />
    </asp:Panel>


    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div class="text_titulo">
                    <h1>Pesquisa de Aprendizes por Curso e Turma</h1>
                </div>
                <br />
                <table class="FundoPainel centralizar" style="width: 98%; border: solid 1px #787878;">
                    <tr>
                        <td class="espaco" colspan="5"></td>
                        <td rowspan="4" style="width: 15%;">&nbsp;
                            <asp:Button ID="Button2" runat="server" CssClass="btn_novo" OnClick="Button2_Click"
                                Text="Pesquisar" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%;">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab" style="text-align: right;">Curso: &nbsp;&nbsp;
                        </td>
                        <td class="Tam35 fonteTab">
                            <asp:DropDownList ID="DDcursoDiario" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                CssClass="fonteTexto" DataTextField="CurDescricao" DataValueField="CurCodigo"
                                Height="18px" OnDataBound="IndiceZero" OnSelectedIndexChanged="DDcursos_SelectedIndexChanged"
                                Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="Tam30 fonteTab">
                            <asp:DropDownList Visible="false" ID="DDdisciplina_pesquisa" runat="server" CssClass="fonteTexto"
                                DataTextField="DisDescricao" DataValueField="DisCodigo" Height="18px" OnDataBound="IndiceZero"
                                Width="95%" AutoPostBack="true" OnSelectedIndexChanged="DDdisciplina_pesquisa_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%;"></td>
                        <td class="Tam08 fonteTab" style="text-align: right;">Turma: &nbsp;&nbsp;
                        </td>
                        <td class="Tam25 fonteTab">
                            <asp:DropDownList ID="DDturma_pesquisa" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" CssClass="fonteTexto" DataTextField="TurNome" DataValueField="TurCodigo"
                                Height="18px" OnDataBound="IndiceZero" OnSelectedIndexChanged="DDturmaDiario_SelectedIndexChanged"
                                ViewStateMode="Enabled" Width="70%">
                            </asp:DropDownList>
                        </td>
                        <td class="Tam25 fonteTab">
                            <asp:DropDownList Visible="false" ID="dd_data_inicio" runat="server" CssClass="fonteTexto" DataTextField="DiaDataInicio"
                                DataValueField="DiaDataInicio" Height="18px" OnDataBound="IndiceZero" Width="50%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="espaco">&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" CssClass="grid_Aluno" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CaptionAlign="Top" HorizontalAlign="Center"
                            PageSize="15" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="Apr_Codigo" HeaderText="Código">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apr_Sexo" HeaderText="Sexo" SortExpression="Apr_Sexo">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" SortExpression="UniNome">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" SortExpression="UniNome">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AreaDescricao" HeaderText="Área de Atuação" SortExpression="AreaDescricao">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apr_Telefone" HeaderText="Telefone" />
                                <asp:BoundField DataField="StaAbreviatura" HeaderText="Sit." SortExpression="StaAbreviatura">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apr_Email" HeaderText="E-mail" SortExpression="Apr_Email"
                                    NullDisplayText="Não Informado.">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Relatórios">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo") %>'
                                            OnClick="ImageButton1_Click" ImageUrl="~/images/cs_print.gif" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Detalhes">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDetalhesAprendiz" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo") %>'
                                            OnClick="btnDetalhesAprendiz_Click" ImageUrl="~/images/icon_edit.png" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="Grid_Aluno" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                        <asp:SqlDataSource ID="SDS_Alunos" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                            SelectCommand=""></asp:SqlDataSource>
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                        <asp:HiddenField ID="HFdataSelecionada" runat="server" />
                        <asp:HiddenField ID="HFConfirma" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div class="controls">
                    <div style="float: right; margin-right: 30px;">
                        <asp:Button ID="btnvoltarRelat" runat="server" CssClass="btn_novo" Text="Voltar"
                            OnClick="Button1_Click" />
                    </div>
                    <div style="float: right; margin-right: 10px;">
                        <asp:Button ID="Button1" runat="server" CssClass="btn_search" Text="Emitir" OnClick="Button1_Click1" />
                    </div>
                    <div style="float: right; margin-right: 10px;">
                        <span class="fonteTab">Relatório:</span> &nbsp;&nbsp;
                        <asp:DropDownList ID="DDtipo_relatorio" runat="server" AutoPostBack="true" CssClass="fonteTexto"
                            Height="18px" Width="180px" OnSelectedIndexChanged="DDtipo_relatorio_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;<asp:Label ID="LB_data_aula" runat="server" Visible="false" class="fonteTab">Data da Aula:</asp:Label>
                        &nbsp;
                        <asp:TextBox ID="TBdataInicio" runat="server" MaxLength="10" Visible="false" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                            PopupPosition="BottomRight" runat="server" TargetControlID="TBdataInicio">
                        </cc2:CalendarExtenderPlus>
                        <asp:Label ID="lb_numero" runat="server" Visible="false" class="fonteTab">Informe o número de uniformes recebidos:</asp:Label>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="tb_numero" runat="server" MaxLength="2" Visible="false" CssClass="fonteTexto"
                            Height="13px" onkeyup="formataInteiro(this,event);" Width="25px"></asp:TextBox>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="centralizar" style="border: none;">
                            <iframe runat="server" id="IFrame1" src="Visualizador.aspx" visible="false" class="VisualFrame"
                                width="900px" style="border: none;"></iframe>
                        </div>
                        <asp:HiddenField ID="HFuniformes" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DDtipo_relatorio" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="Carometro" runat="server">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table align="center" class="auto-style1">
                            <tr>
                                <td class="auto-style2">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="DD_TURMA" runat="server" DataTextField="TurNome" DataValueField="TurCodigo" Style="margin-left: 0px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="BGerarCarometro" runat="server" OnClick="BGerarCarometro_Click" Text="Gerar Carômetro" CssClass="btn_search" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Panel ID="PCarometro" runat="server" Visible="false">
                            <div class="centralizar">
                                <iframe id="IFrameCarometro" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>

            <asp:View ID="View3" runat="server">
                <div class="centralizar">
                    <iframe id="frameAprendiz" height="870px" width="98%" style="border: none;" name="IFrame1"
                        src="CadastroAprendiz.aspx"></iframe>
                </div>
            </asp:View>

        </asp:MultiView>
    </div>
</asp:Content>
