<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="LancamentoOcorrencia.aspx.cs" Inherits="ProtocoloAgil.pages.LancamentoOcorrencia" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50731.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este registro?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function setSessionVariable(obj) {
            var valueToSetTo = obj.value;
            window.__doPostBack('SetSessionVariable', valueToSetTo);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Jovens > <span>Cadastro de Ocorrências</span>
        </p>
    </div>
    <div class="controls">
        <div style="float: left;">
            <asp:Button ID="btn_cronograma" runat="server" CssClass="btn_controls"
                Text="Por Aprendiz" OnClick="btn_cronograma_Click" />
            <asp:Button ID="btn_pesquisa_data" runat="server" CssClass="btn_controls"
                Text="Por Periodo" OnClick="btn_pesquisa_data_Click" />
            <asp:Button ID="btnDataTipo" runat="server" CssClass="btn_controls"
                Text="Por Periodo/ Tipo Ocorrência" OnClick="btnDataTipo_Click" />
            <asp:Button ID="btnTotaisPorPeriodo" runat="server" CssClass="btn_controls"
                Text="Totais Por Período" OnClick="btnTotaisPorPeriodo_Click" />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="formatoTela_02">
                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnpesquisa">
                    <div class="text_titulo">
                        <h1>Pesquisa de Jovens</h1>
                    </div>
                    <br />
                    <div class="controls FundoPainel">
                        <div style="float: left; margin-left: 70px">
                            <span class="fonteTab"><strong>Nome:</strong>&nbsp;&nbsp;</span>
                            <asp:TextBox ID="TBNome" runat="server" Height="20px" onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBCodigo').value = '';"
                                Width="400px"></asp:TextBox>
                        </div>
                        <div style="float: right;">
                            <span class="fonteTab"><strong>Matricula:</strong> &nbsp;&nbsp;</span><asp:TextBox
                                ID="TBCodigo" runat="server" Height="20px" onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBNome').value= '';"
                                onkeyup="formataInteiro(this,event);" Width="100px"></asp:TextBox>
                            &nbsp;
                    <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_novo" OnClick="btn_pesquisar_Click"
                        Text="Pesquisar" />
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                OnDataBound="GridView_DataBound" CaptionAlign="Top" CssClass="grid_Aluno" HorizontalAlign="Center"
                                OnPageIndexChanging="GridView_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                PageSize="15" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Apr_Codigo" HeaderText="Código" InsertVisible="False"
                                        ReadOnly="True" SortExpression="Apr_Codigo">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Jovem" SortExpression="Apr_Nome">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Telefone" HeaderText="Telefone" SortExpression="Apr_Telefone"
                                        NullDisplayText="(__) ____-____">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Sexo" HeaderText="Sexo" SortExpression="Apr_Sexo">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StaDescricao" HeaderText="Status" SortExpression="StaDescricao">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Email" HeaderText="E-mail" SortExpression="Apr_Email"
                                        NullDisplayText="Não Informado">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Ocorrências">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMB_alocacao" ImageUrl="~/images/detalhes_icone.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo")%>'
                                                runat="server" OnClick="IMB_alocacao_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Image" HeaderText="Nova" SelectImageUrl="~/images/icon_edit.png"
                                        SelectText="Alterar" ShowSelectButton="True">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:CommandField>
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
                            <br />
                            <br />
                            <asp:HiddenField runat="server" ID="HFmatricula" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </asp:View>

        <asp:View ID="View1" runat="server">
            <div class="text_titulo">
                <h1>Pesquisa de Ocorrência por Jovem</h1>
            </div>
            <div class="formatoTela_02">
                <br />

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server" Width="500px" Height="300px" CssClass="centralizar" Visible="false">
                            <div class="text_titulo" style="margin-top: 120px">
                                <h1>Nenhum resultado para esta pesquisa.</h1>
                            </div>
                        </asp:Panel>
                        <asp:GridView ID="GridView1" CssClass="list_results" runat="server" OnDataBound="GridView_DataBound"
                            AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nome" HeaderText="Jovem">
                                    <HeaderStyle Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Descricao" HeaderText="Ocorrência">
                                    <HeaderStyle Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Emissor" HeaderText="Emissor">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Responsavel" HeaderText="Responsável">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Alterar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBEditar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Ordem") %>'
                                            OnClick="IMBEditar_Click" ImageUrl="~/images/icon_edit.png" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="5%"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Imprimir">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMB_print" ImageUrl="~/images/cs_print.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Ordem")%>'
                                            runat="server" OnClick="ImageButton1_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Ordem")%>'
                                            OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"
                                            runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="5%"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                                PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                            <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                        <asp:HiddenField ID="HFConfirma" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:View>

        <asp:View ID="View3" runat="server">
            <asp:Panel runat="server" CssClass="centralizar" ID="Panel1" Width="800px">
                <br />
                <br />
                <table class="Table FundoPainel centralizar">
                    <tr>
                        <td class="titulo cortitulo corfonte" colspan="7" style="font-size: large;">Lançamento de Ocorrências
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05"></td>
                        <td class="Tam08 fonteTab">Ordem:
                        </td>
                        <td class="Tam08 fonteTab">Código:
                        </td>
                        <td class="Tam30 fonteTab" colspan="2">Jovem:
                        </td>
                        <td class="Tam30 fonteTab" colspan="2">Emissor Ocorrência:
                        </td>
                        <td class="Tam05"></td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab">
                            <asp:TextBox ID="txtOrdem" runat="server" CssClass="fonteTexto" Height="13px"
                                Enabled="false" Width="40px"></asp:TextBox>
                        </td>
                        <td class="Tam08 fonteTab">
                            <asp:TextBox ID="TBcodAprendiz" runat="server" CssClass="fonteTexto" Height="13px"
                                Enabled="false" Width="70px"></asp:TextBox>
                        </td>
                        <td class="Tam30 fonteTab" colspan="2">
                            <asp:TextBox ID="TBnomeAlu" runat="server" CssClass="fonteTexto" Height="13px" onblur="setSessionVariable(this);"
                                Width="90%"></asp:TextBox>
                            <cc1:AutoCompleteExtender BehaviorID="AutoComplete1" ID="autcompEx" runat="server"
                                TargetControlID="TBnomeAlu" ServiceMethod="GetAlunos" MinimumPrefixLength="1"
                                CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20">
                            </cc1:AutoCompleteExtender>
                        </td>

                        <td class="Tam30 fonteTab" colspan="2">
                            <asp:DropDownList ID="ddEmissor" runat="server" CssClass="fonteTexto" Height="16px"
                                OnSelectedIndexChanged="DD_ocorrencia_SelectedIndexChanged" Width="95%">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="E">Empresa</asp:ListItem>
                                <asp:ListItem Value="C">CejaBrasil</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="Tam05">&nbsp;
                        </td>
                    </tr>
                </table>
                <table class="Table FundoPainel centralizar">
                    <tr>
                        <td class="Tam05"></td>
                        <td class="Tam08 fonteTab">Código:
                        </td>
                        <td class="Tam25 fonteTab">Ocorrência:
                        </td>
                        <td class="Tam10 fonteTab">Data:
                        </td>
                        <td class="Tam10 fonteTab">Notificação:</td>
                        <td class="Tam10 fonteTab">P. Devolução:</td>
                        <td class="Tam10 fonteTab">Devolução:</td>
                        <td class="Tam05 fonteTab">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05"></td>
                        <td class="Tam08 fonteTab">
                            <asp:TextBox ID="TBCodOcorrencia" runat="server" CssClass="fonteTexto"
                                Enabled="false" Height="13px" Width="70px"></asp:TextBox>
                        </td>
                        <td class="Tam25 fonteTab">
                            <asp:DropDownList ID="DD_ocorrencia" runat="server" AutoPostBack="true"
                                CssClass="fonteTexto" DataTextField="OcoDescricao" DataValueField="OcoCodigo"
                                Height="16px" OnDataBound="IndiceZero"
                                OnSelectedIndexChanged="DD_ocorrencia_SelectedIndexChanged" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="Tam10 fonteTab">
                            <asp:TextBox ID="TBData" runat="server" CssClass="fonteTexto" Height="13px" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="80px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server"
                                Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="TBData">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam10 fonteTab">
                            <asp:TextBox ID="tb_notificacao" runat="server" CssClass="fonteTexto" Height="13px" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="80px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus1" runat="server"
                                Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="tb_notificacao">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam10 fonteTab">
                            <asp:TextBox ID="tb_prev_devolucao" runat="server" CssClass="fonteTexto" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                Height="13px" MaxLength="10" onkeyup="formataData(this,event);" Width="80px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus5" runat="server"
                                Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="tb_prev_devolucao">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam10 fonteTab">
                            <asp:TextBox ID="tb_devolucao" runat="server" CssClass="fonteTexto" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                Height="13px" MaxLength="10" onkeyup="formataData(this,event);" Width="80px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus6" runat="server"
                                Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="tb_devolucao">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam05"></td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">Observações:
                        </td>
                        <td class="Tam30 fonteTab" colspan="5">&nbsp;
                        </td>
                        <td class="Tam05">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="fonteTab" colspan="6">
                            <asp:TextBox ID="TBObservacao" runat="server" CssClass="fonteTexto"
                                Height="88px" onkeyup="javascript:IsMaxLength(this,900);" TextMode="MultiLine"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td class="Tam05">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco" colspan="8">&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <div class="controls">
                    <div class="centralizar">
                        <asp:Button ID="btn_save" runat="server" CssClass="btn_novo" Text="Confirmar" OnClick="btn_save_Click" />&nbsp;
                        <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btn_gerar_Click" />
                    </div>
                </div>
                <br />
                <br />
            </asp:Panel>
        </asp:View>

        <asp:View ID="View4" runat="server">
            <div class="controls">
                <div style="float: right; margin-right: 30px;">
                    <asp:Button ID="btnvoltarRelat" runat="server" CssClass="btn_novo" Text="Voltar"
                        OnClick="Button1_Click" />
                </div>
            </div>
            <div class="centralizar" style="border: none;">
                <iframe runat="server" id="IFrame1" src="Visualizador.aspx" class="VisualFrame" width="900px"
                    style="border: none;"></iframe>
            </div>
        </asp:View>

        <asp:View ID="View5" runat="server">
            <div class="formatoTela_02">
                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnpesquisa">
                    <div class="text_titulo">
                        <h1>
                            <asp:Label runat="server" ID="lblTitulo" Text="Pesquisa de Ocorrências por Data"></asp:Label>
                        </h1>
                    </div>
                    <br />
                    <div class="controls FundoPainel" style="height: 50px;">
                        <div style="float: left; margin-left: 70px">
                            &nbsp;&nbsp;<span class="fonteTab"><strong>Data Inicio:</strong>&nbsp;&nbsp;</span>
                            <asp:TextBox ID="TBdataInicio" runat="server" MaxLength="10" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);" Width="100px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus3" PopupPosition="BottomRight"
                                runat="server" TargetControlID="TBdataInicio">
                            </cc2:CalendarExtenderPlus>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                              <span class="fonteTab"><strong>Data Término:</strong>&nbsp;&nbsp;</span>
                            <asp:TextBox ID="TBdataTermino" runat="server" MaxLength="10" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);" Width="100px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus4" PopupPosition="BottomRight"
                                runat="server" TargetControlID="TBdataTermino">
                            </cc2:CalendarExtenderPlus>
                            <br />
                            &nbsp;
                            <asp:Label runat="server" ID="lblTipoOcorrencia" Text="Tipo de Ocorrência" CssClass="fonteTab"></asp:Label>
                            <asp:DropDownList ID="DDOcorrencia" Visible="false" runat="server" CssClass="fonteTexto"
                                DataTextField="OcoDescricao" DataValueField="OcoCodigo"
                                Height="18px" OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="70%">
                            </asp:DropDownList>

                            <asp:RadioButtonList ID="RB_tipo_Pesquisa" CssClass="fonteTab" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Value="1" Selected="true">Data Ocorrência</asp:ListItem>
                                <asp:ListItem Value="2">Data Entrega</asp:ListItem>
                                <asp:ListItem Value="3">Previsão Devolução</asp:ListItem>
                                <asp:ListItem Value="4">Data Devolução</asp:ListItem>
                            </asp:RadioButtonList>

                        </div>
                        <div style="float: right; padding: 10px 20px 0 0;">
                            <asp:Button ID="btn_pesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar" OnClick="btn_pesquisa_Click" />
                            <asp:Button ID="btn_print" runat="server" CssClass="btn_novo" Text="Imprimir"
                                OnClick="btn_print_Click" />
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel5" runat="server" Width="500px" Height="300px" CssClass="centralizar" Visible="false">
                                <div class="text_titulo" style="margin-top: 120px">
                                    <h1>Nenhum resultado para esta pesquisa.</h1>
                                </div>
                            </asp:Panel>
                            <asp:GridView ID="GridView3" CssClass="list_results_Menor" runat="server" OnDataBound="GridView_DataBound"
                                AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView3_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="OcaOrdem" HeaderText="Ordem">
                                        <HeaderStyle Width="18%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz">
                                        <HeaderStyle Width="18%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OcoDescricao" HeaderText="Ocorrência">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OcaEmissorOcorrencia" HeaderText="Emissor">
                                        <HeaderStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UsuNome" HeaderText="Responsável">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OcaDataOcorrencia" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                                        <HeaderStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OcaDataEntrega" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Notificação">
                                        <HeaderStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OcaPrevDevolucao" DataFormatString="{0:dd/MM/yyyy}" HeaderText="P. Devolução">
                                        <HeaderStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OcaDevolucao" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Devolução">
                                        <HeaderStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Alterar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMBEditar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OcaOrdem") %>'
                                                OnClick="IMBEditar_Click" ImageUrl="~/images/icon_edit.png" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle Width="6%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Imprimir">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMB_print" ImageUrl="~/images/cs_print.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OcaOrdem")%>'
                                                runat="server" OnClick="ImageButton1_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OcaOrdem")%>'
                                                OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"
                                                runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle Width="6%"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="List_results_menor" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                                    PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </asp:View>











        <asp:View ID="View6" runat="server">
            <div class="formatoTela_02">
                <asp:Panel ID="PainelTotalPorPeriodo" runat="server" DefaultButton="btnpesquisa">
                    <div class="text_titulo">
                        <h1>
                            <asp:Label runat="server" ID="Label1" Text="Totais Por Período"></asp:Label>
                        </h1>
                    </div>
                    <br />
                    <div class="controls FundoPainel" style="height: 50px;">
                        <div style="float: left; margin-left: 70px">
                            &nbsp;&nbsp;<span class="fonteTab"><strong>Data Inicio:</strong>&nbsp;&nbsp;</span>
                            <asp:TextBox ID="txtDataInicioTotaisPorPeriodo" runat="server" MaxLength="10" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);" Width="100px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus7" PopupPosition="BottomRight"
                                runat="server" TargetControlID="txtDataInicioTotaisPorPeriodo">
                            </cc2:CalendarExtenderPlus>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                              <span class="fonteTab"><strong>Data Término:</strong>&nbsp;&nbsp;</span>
                            <asp:TextBox ID="txtDataTerminoTotaisPorPeriodo" runat="server" MaxLength="10" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);" Width="100px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus8" PopupPosition="BottomRight"
                                runat="server" TargetControlID="txtDataTerminoTotaisPorPeriodo">
                            </cc2:CalendarExtenderPlus>
                            <br />
                            &nbsp;

                        </div>
                        <div style="float: right; padding: 10px 20px 0 0;">
                            <asp:Button ID="btnTotaisPorPeriodoPesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar" OnClick="btnTotaisPorPeriodoPesquisa_Click" />
                            <asp:Button ID="btnImprimirTotaisPorPeriodo" runat="server" CssClass="btn_novo" Text="Imprimir"
                                OnClick="btnImprimirTotaisPorPeriodo_Click" />
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel7" runat="server" Width="500px" Height="300px" CssClass="centralizar" Visible="false">
                                <div class="text_titulo" style="margin-top: 120px">
                                    <h1>Nenhum resultado para esta pesquisa.</h1>
                                </div>
                            </asp:Panel>
                            <div class="centralizar" style="border: none;">
                            <asp:GridView ID="gridTotaisPorPeriodo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" Style="width: 50%;
                                margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="descricao" HeaderText="Descrição" SortExpression="descricao"
                                        InsertVisible="False" ReadOnly="True">
                                        <HeaderStyle Width="90%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTD" HeaderText="Quantidade" SortExpression="QTD">
                                        <ItemStyle HorizontalAlign="Right" />
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
                        </div>
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                            <asp:HiddenField ID="HiddenField4" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </asp:View>



    </asp:MultiView>
</asp:Content>
