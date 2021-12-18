<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="EstatisticaAprendizes.aspx.cs" Inherits="ProtocoloAgil.pages.EstatisticaAprendizes" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Estatísticas > <span>Estatística Geral de Jovens</span>
        </p>
    </div>
    <div class="controls">
        <div style="float: left;">
            <asp:Button ID="bt_ativos_turma" runat="server" CssClass="btn_controls" Text="Ativos por Turma" OnClick="bt_ativos_turma_Click" />
            <asp:Button ID="bt_ativos_area" runat="server" CssClass="btn_controls" Text="Ativos por Área" OnClick="bt_ativos_area_Click" />
            <asp:Button ID="btnAtivosPorCidade" runat="server" CssClass="btn_controls" Text="Ativos por Cidade" OnClick="btnAtivosPorCidade_Click" />
            <asp:Button ID="bt_desligados_periodo" runat="server" CssClass="btn_controls" Text="Deslig. no Período" OnClick="bt_desligados_periodo_Click" />
            <asp:Button ID="btn_motivo_desligamento" runat="server" CssClass="btn_controls" Text="Deslig. por Motivo" OnClick="btn_motivo_desligamento_Click" />
            <asp:Button ID="bt_alocados_periodo" runat="server" CssClass="btn_controls" Text="Aloc. no Período" OnClick="bt_alocados_periodo_Click" />
            <asp:Button ID="btnAtivosUnidade" runat="server" CssClass="btn_controls" Text="Ativos por Unidade" OnClick="btnAtivosUnidade_Click" />
            <asp:Button ID="btn_pagamento" runat="server" CssClass="btn_controls"
                Text="Tipo de Pagamento" OnClick="btn_pagamento_Click" />
        </div>
    </div>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">

                <div class="text_titulo">
                    <h1>Total de Aprendizes Ativos por Turma
                    </h1>
                </div>
                <br />
                <br />
                <div class="controls">
                    <span class="fonteTab">Data de referência:</span>
                    <asp:TextBox ID="txtDataReferenciaPorTurma" runat="server" BorderStyle="Groove" BorderWidth="1px"
                        onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                        MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                        Width="105px"></asp:TextBox>

                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus7"
                        PopupPosition="BottomRight" runat="server" TargetControlID="txtDataReferenciaPorTurma">
                    </cc2:CalendarExtenderPlus>
                    <asp:Button ID="Button1" runat="server" CssClass="btn_novo" Text="Pesquisar"
                        OnClick="btnAtivosPorTurma_Click" />


                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="painelAtivoPorTurma" Visible="false">
                            <div class="centralizar" style="width: 50%">
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="8" CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center"
                                    Style="width: 85%; margin: auto" OnDataBound="GridView1_DataBound" DataSourceID="SDS_AprendizesAtivos" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome" />
                                        <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                        <HeaderStyle Width="20%" />
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

                            <div class="controls">
                                <div style="float: right;">
                                    <asp:Button ID="btn_print_Ativos" runat="server" CssClass="btn_add" Text="Imprimir"
                                        OnClick="btn_print_Ativos_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="SDS_AprendizesAtivos" runat="server" OnSelected="SqlDataSource3_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                                            SelectCommand="Select TurNome, count(ALAAprendiz) as QTD from 
                                                            dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Turmas ON ALATurma = TurCodigo
                                                            inner join CA_Aprendiz on CA_AlocacaoAprendiz.ALAAprendiz = CA_Aprendiz.Apr_Codigo  
                                                            WHERE Apr_Situacao = 6 and ALADataInicio  &lt;= @dataReferenciaPorTurma and  ALADataPrevTermino &gt;= @dataReferenciaPorTurma
                                                            and TurCurso = '002'
                                                            GROUP BY TurNome
                                                            order by count(ALAAprendiz) desc">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtDataReferenciaPorTurma" Name="dataReferenciaPorTurma" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="HFRowCount" runat="server" />
                <br />
            </asp:View>
            <asp:View ID="View4" runat="server">
                <div class="text_titulo">
                    <h1>Total de Aprendizes Ativos por Área de Atuação
                    </h1>
                </div>
                <br />

                <br />
                <div class="controls">
                    <span class="fonteTab">Data de referência:</span>
                    <asp:TextBox ID="txtDataReferenciaPorAreaAtuacao" runat="server" BorderStyle="Groove" BorderWidth="1px"
                        onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                        MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                        Width="105px"></asp:TextBox>
                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus6"
                        PopupPosition="BottomRight" runat="server" TargetControlID="txtDataReferenciaPorAreaAtuacao">
                    </cc2:CalendarExtenderPlus>
                    <asp:Button ID="btnAtivosPorTurma" runat="server" CssClass="btn_novo" Text="Pesquisar"
                        OnClick="btnAtivosArea_Click" />
                </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="painelAtivosPorArea" Visible="false">
                            <div class="controls">
                                <div style="float: right;">
                                    <asp:Button ID="btn_print_Ativos_area" runat="server" CssClass="btn_add"
                                        Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                </div>
                            </div>
                            <div class="pn_left" style="border: none;">
                                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="12" CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center"
                                    Style="width: 85%; margin: auto" OnDataBound="GridView_Total_DataBound" DataSourceID="SDS_AprendizesArea" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="AreaDescricao" HeaderText="Área de Atuação" SortExpression="AreaDescricao" />
                                        <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                        <HeaderStyle Width="20%" />
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
                            <div class="pn_right" style="border: none;">
                                <span class="fonteTab" style="float: left; height: 15px;">Selecione o tipo de Gráfico:</span>
                                <asp:RadioButtonList ID="RB_grafico_ativo_Area" RepeatDirection="Horizontal" AutoPostBack="true"
                                    CssClass="fonteTexto" Style="float: left; margin-left: 10px;" runat="server"
                                    OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="true"> Colunas </asp:ListItem>
                                    <asp:ListItem Value="2"> Pizza(%) </asp:ListItem>
                                </asp:RadioButtonList>
                                <br />
                                <div class="centralizar">
                                    <asp:Chart ID="Chart2" runat="server" DataSourceID="SDS_AprendizesArea" Height="280px"
                                        Width="380px">
                                        <Series>
                                            <asp:Series BackGradientStyle="DiagonalLeft" ChartArea="ChartArea1" Font="Tahoma, 9pt, style=Bold"
                                                IsValueShownAsLabel="True" IsVisibleInLegend="false" IsXValueIndexed="True" LabelBackColor="Transparent"
                                                Legend="Legend1" Name="Series1" Palette="BrightPastel" XValueMember="AreaDescricao"
                                                YValueMembers="QTD">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <Area3DStyle Enable3D="True" LightStyle="Realistic" />
                                            </asp:ChartArea>
                                        </ChartAreas>
                                        <Legends>
                                            <asp:Legend Docking="Bottom" Name="Legend1">
                                            </asp:Legend>
                                        </Legends>
                                    </asp:Chart>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="SDS_AprendizesArea" runat="server" OnSelected="SqlDataSource1_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="SELECT CA_AreaAtuacao.AreaDescricao, Count(CA_Aprendiz.[Apr_Codigo]) AS QTD
                                            FROM ((CA_Aprendiz INNER JOIN CA_AlocacaoAprendiz ON CA_Aprendiz.Apr_Codigo = CA_AlocacaoAprendiz.ALAAprendiz) 
                                            INNER JOIN CA_Turmas ON CA_AlocacaoAprendiz.ALATurma = CA_Turmas.TurCodigo) 
                                            INNER JOIN CA_AreaAtuacao ON CA_Aprendiz.Apr_AreaAtuacao = CA_AreaAtuacao.AreaCodigo  
                                            WHERE Apr_Situacao = 6 and ALADataInicio &lt;= @dataReferencia and  ALADataPrevTermino &gt;= @dataReferencia
                                            and TurCurso = '002'
                                            GROUP BY AreaDescricao 
                                            order by count(Apr_Codigo) desc">

                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtDataReferenciaPorAreaAtuacao" Name="dataReferencia" PropertyName="Text" />
                    </SelectParameters>

                </asp:SqlDataSource>
                <br />
            </asp:View>
            <asp:View runat="server" ID="View2">
                <div class="text_titulo">
                    <h1>Total de Aprendizes Desligados no Período</h1>
                </div>
                <div class="controls">
                    <div style="float: left;">
                        <asp:Button ID="btn_desligados_sintetico" runat="server" CssClass="btn_controls"
                            Text="Sintético" OnClick="btn_desligados_sintetico_Click" />
                        <asp:Button ID="btn_desligados_analitico" runat="server" CssClass="btn_controls"
                            Text="Analítico" OnClick="btn_desligados_analitico_Click" />
                        <asp:Button ID="btn_desligados_genero" runat="server" CssClass="btn_controls" Text="Por Gênero"
                            OnClick="Button1_Click1" />
                        <asp:Button ID="btn_desligados_idade" runat="server" CssClass="btn_controls" Text="Por Idade"
                            OnClick="btn_desligados_idade_Click" />
                    </div>
                    <div style="float: right;">
                        <span class="fonteTab">Data de Início:</span> &nbsp;&nbsp;
                        <asp:TextBox ID="TBdataInicio" runat="server" BorderStyle="Groove" BorderWidth="1px"
                            onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                            Width="105px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                            PopupPosition="BottomRight" runat="server" TargetControlID="TBdataInicio">
                        </cc2:CalendarExtenderPlus>
                        &nbsp;&nbsp; <span class="fonteTab">Data de Término:</span> &nbsp;&nbsp;
                        <asp:TextBox ID="TBdataTermino" runat="server" BorderStyle="Groove" BorderWidth="1px"
                            onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                            Width="105px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1" PopupPosition="BottomRight"
                            runat="server" TargetControlID="TBdataTermino">
                        </cc2:CalendarExtenderPlus>
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_pesquisa_desligados" runat="server" CssClass="btn_novo" Text="Pesquisar"
                            OnClick="btn_pesquisa_desligados_Click" />
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel05">
                    <ContentTemplate>
                        <asp:MultiView ID="MultiView2" runat="server">
                            <asp:View ID="View7" runat="server">
                                <div class="controls">
                                    <div style="float: right;">
                                        <asp:Button ID="btn_print_desligados_situacao" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="pn_left" style="border: none;">
                                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CaptionAlign="Top" CssClass="list_results" DataSourceID="SDS_AprendizesDesligados"
                                                HorizontalAlign="Center" OnDataBound="GridView_Total_DataBound" PageSize="8" Style="width: 85%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="StaDescricao" HeaderText="Situação do Aprendiz" SortExpression="StaDescricao" />
                                                    <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                                    <HeaderStyle Width="20%" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </div>
                                        <div class="pn_right" style="border: none;">
                                            <asp:Panel runat="server" ID="PN_Siuacao_Desligados" Visible="false">
                                                <span class="fonteTab" style="float: left;">Selecione o tipo de Gráfico:</span>
                                                <asp:RadioButtonList ID="RB_grafico_Desliados_sintetico" runat="server" AutoPostBack="true"
                                                    CssClass="fonteTexto" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" Style="float: left; margin-left: 10px;">
                                                    <asp:ListItem Selected="true" Value="1"> Colunas </asp:ListItem>
                                                    <asp:ListItem Value="2"> Pizza(%) </asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <div class="centralizar">
                                                    <asp:Chart ID="Chart3" runat="server" DataSourceID="SDS_AprendizesDesligados" Height="280px"
                                                        Width="380px">
                                                        <Series>
                                                            <asp:Series BackGradientStyle="DiagonalLeft" ChartArea="ChartArea1" Font="Tahoma, 9pt, style=Bold"
                                                                IsValueShownAsLabel="True" IsVisibleInLegend="false" IsXValueIndexed="True" LabelBackColor="Transparent"
                                                                Legend="Legend1" Name="Series1" Palette="BrightPastel" XValueMember="StaDescricao"
                                                                YValueMembers="QTD">
                                                            </asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <Area3DStyle Enable3D="True" LightStyle="Realistic" />
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                        <Legends>
                                                            <asp:Legend Docking="Bottom" Name="Legend1">
                                                            </asp:Legend>
                                                        </Legends>
                                                    </asp:Chart>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:SqlDataSource ID="SDS_AprendizesDesligados" runat="server" OnSelected="SqlDataSource1_Selected"
                                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                                                        Select StaDescricao, count(ALAAprendiz) as QTD from dbo.CA_AlocacaoAprendiz 
                                                        INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo 
                                                        INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo
                                                        INNER JOIN CA_Turmas ON ALATurma = TurCodigo 
                                                        WHERE Apr_FimAprendizagem BETWEEN @dataInicio AND @datatermino
                                                        And TurCurso = '002'
                                                        GROUP BY StaDescricao  order by count(ALAAprendiz) desc">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TBdataInicio" Name="dataInicio" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="TBdataTermino" Name="datatermino" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <br />
                            </asp:View>
                            <asp:View ID="View6" runat="server">
                                <div class="controls">
                                    <div style="float: right;">
                                        <asp:Button ID="btn_print_desligados_analitico" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="centralizar">
                                            <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CaptionAlign="Top" CssClass="list_results_Menor" DataSourceID="SDS_Desligados_analitico"
                                                HorizontalAlign="Center" OnDataBound="GridView_DataBound" Style="width: 98%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome">
                                                        <HeaderStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ParDescricao" HeaderText="Parceiro"
                                                        SortExpression="ParDescricao" NullDisplayText="-----">
                                                        <HeaderStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade"
                                                        SortExpression="ParUniDescricao" NullDisplayText="-----" />
                                                    <asp:BoundField DataField="Apr_InicioAprendizagem" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data de Início"
                                                        SortExpression="ALADataInicio" />
                                                    <asp:BoundField DataField="Apr_FimAprendizagem" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Termino"
                                                        SortExpression="ALADataTermino" />
                                                    <asp:BoundField DataField="ALAValorBolsa" DataFormatString="{0:f2}" HeaderText="Bolsa(R$)"
                                                        SortExpression="ALAValorBolsa" />
                                                    <asp:BoundField DataField="ALAValorTaxa" DataFormatString="{0:f2}" HeaderText="Taxa(R$)"
                                                        SortExpression="ALAValorTaxa" />
                                                    <asp:BoundField DataField="ALApagto" HeaderText="Pagamento" ReadOnly="True" SortExpression="ALApagto" />
                                                    <asp:BoundField DataField="StaDescricao" HeaderText="Situação" SortExpression="StaDescricao" />
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SDS_Desligados_analitico" runat="server" OnSelected="SqlDataSource1_Selected"
                                                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                                                SELECT CA_Aprendiz.Apr_Codigo, CA_Aprendiz.Apr_Nome, CA_Aprendiz.Apr_FimAprendizagem,   CA_Aprendiz.Apr_InicioAprendizagem,
                                                max(alocacao.ALAUnidadeParceiro) AS PrimeiroDeALAUnidadeParceiro, CA_Parceiros.ParDescricao, CA_ParceirosUnidade.ParUniDescricao, alocacao.ALAValorBolsa, alocacao.ALAValorTaxa,
                                                (select TurNome from  CA_Turmas where TurCodigo = max(alocacao.ALATurma))  AS TurNome, ALApagto = case when alocacao.ALApagto = 'E' then 'Empresa' when alocacao.ALApagto = 'C' then 'CejaBrasil' end , CA_Aprendiz.Apr_Situacao,
                                                CA_SituacaoAprendiz.StaDescricao 
                                                FROM CA_Aprendiz INNER JOIN CA_AlocacaoAprendiz alocacao ON CA_Aprendiz.Apr_Codigo = alocacao.ALAAprendiz
                                                INNER JOIN CA_ParceirosUnidade ON alocacao.ALAUnidadeParceiro = CA_ParceirosUnidade.ParUniCodigo
                                                INNER JOIN CA_Parceiros ON CA_ParceirosUnidade.ParUniCodigoParceiro = CA_Parceiros.ParCodigo
                                                INNER JOIN CA_SituacaoAprendiz ON CA_Aprendiz.Apr_Situacao = CA_SituacaoAprendiz.StaCodigo
                                                INNER JOIN dbo.CA_Turmas ON TurCurso = '002'
                                                where ALADataInicio &lt;= GETDATE() and  ALADataPrevTermino &lt;= GETDATE()  
                                                GROUP BY CA_Aprendiz.Apr_Codigo, CA_Aprendiz.Apr_Nome, CA_Aprendiz.Apr_FimAprendizagem, CA_Parceiros.ParDescricao, 
                                                CA_ParceirosUnidade.ParUniDescricao, alocacao.ALAValorBolsa, alocacao.ALAValorTaxa, alocacao.ALApagto, 
                                                CA_Aprendiz.Apr_Situacao, CA_SituacaoAprendiz.StaDescricao, CA_Aprendiz.Apr_InicioAprendizagem
                                                HAVING CA_Aprendiz.Apr_FimAprendizagem Between @dataInicio And @datatermino And Apr_Situacao &lt;&gt; 6 ORDER BY CA_Aprendiz.Apr_Nome">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="TBdataInicio" Name="dataInicio" PropertyName="Text" />
                                                    <asp:ControlParameter ControlID="TBdataTermino" Name="datatermino" PropertyName="Text" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <br />
                                            <br />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:View>
                            <asp:View ID="view12" runat="server">
                                <div class="controls">
                                    <div style="float: right;">
                                        <asp:Button ID="btn_print_desligados_genero" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <div class="pn_left" style="border: none;">
                                            <asp:GridView ID="GridView8" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CaptionAlign="Top" CssClass="list_results" DataSourceID="SDS_desligados_genero"
                                                HorizontalAlign="Center" OnDataBound="GridView_Total_DataBound" PageSize="8" Style="width: 95%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="sexo" HeaderText="Gênero" SortExpression="MotDescricao">
                                                    <HeaderStyle Width="40%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                                    <HeaderStyle Width="20%" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </div>
                                        <div class="pn_right" style="border: none;">
                                            <span class="fonteTab" style="float: left;">Selecione o tipo de Gráfico:</span>&nbsp;
                                            <asp:RadioButtonList ID="RB_grafico_desligados_genero" RepeatDirection="Horizontal"
                                                AutoPostBack="true" CssClass="fonteTexto" Style="float: left; margin-left: 10px;"
                                                runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="true"> Colunas </asp:ListItem>
                                                <asp:ListItem Value="2"> Pizza(%) </asp:ListItem>
                                            </asp:RadioButtonList>
                                            <br />
                                            <div class="centralizar">
                                                <asp:Chart ID="Chart5" runat="server" DataSourceID="SDS_desligados_genero" Height="280px"
                                                    Width="380px">
                                                    <Series>
                                                        <asp:Series BackGradientStyle="DiagonalLeft" ChartArea="ChartArea1" Font="Tahoma, 9pt, style=Bold"
                                                            IsValueShownAsLabel="True" IsVisibleInLegend="false" IsXValueIndexed="True" LabelBackColor="Transparent"
                                                            Legend="Legend1" Name="Series1" Palette="BrightPastel" XValueMember="sexo" YValueMembers="QTD">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="ChartArea1">
                                                            <Area3DStyle Enable3D="True" LightStyle="Realistic" />
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                    <Legends>
                                                        <asp:Legend Docking="Bottom" Name="Legend1">
                                                        </asp:Legend>
                                                    </Legends>
                                                </asp:Chart>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:SqlDataSource ID="SDS_desligados_genero" runat="server" OnSelected="SqlDataSource1_Selected"
                                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                                    Select (Case when Apr_sexo = 'M' then 'Masculino' when Apr_sexo = 'F' then 'Feminino' else 'ND' end) as sexo , count(Apr_sexo) as QTD
                                    from dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo 
                                    INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo
                                    INNER JOIN CA_Turmas ON ALATurma = TurCodigo 
                                    WHERE TurCurso = '002'
                                    AND Apr_FimAprendizagem BETWEEN @dataInicio AND @datatermino
                                    group by Apr_sexo">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TBdataInicio" Name="dataInicio" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="TBdataTermino" Name="datatermino" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </asp:View>
                            <asp:View ID="view13" runat="server">
                                <div class="controls">
                                    <div style="float: right;">
                                        <asp:Button ID="btn_print_desligados_idade" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <div class="centralizar" style="width: 50%;">
                                            <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled"
                                                CaptionAlign="Top" CssClass="list_results" DataSourceID="SDS_Desligados_Idade"
                                                HorizontalAlign="Center" PageSize="8" Style="width: 95%; margin: auto"
                                                OnDataBound="GridView9_DataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="Idade" HeaderText="Idade" SortExpression="MotDescricao">
                                                    <HeaderStyle Width="40%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                                    <HeaderStyle Width="20%" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:SqlDataSource ID="SDS_Desligados_Idade" runat="server" OnSelected="SqlDataSource2_Selected"
                                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                                    SELECT Idade, COUNT(Idade) as QTD from  View_Idade_Alocacao 
                                      WHERE Apr_FimAprendizagem BETWEEN @dataInicio AND @datatermino 
                                     group by Idade 
                                    order by COUNT(Idade)desc">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TBdataInicio" Name="dataInicio" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="TBdataTermino" Name="datatermino" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </asp:View>

                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View runat="server" ID="View8">
                <div class="text_titulo">
                    <h1>Total de Aprendizes Alocados no Período</h1>
                </div>
                <div class="controls">
                    <div style="float: left;">
                        <asp:Button ID="btn_alocados_sintetico" runat="server" CssClass="btn_controls" Text="Sintético"
                            OnClick="btn_alocados_sintetico_Click" />
                        <asp:Button ID="btn_alocados_Analitico" runat="server" CssClass="btn_controls" Text="Analítico"
                            OnClick="btn_alocados_Analitico_Click" />
                        <asp:Button ID="btn_alocados_Genero" runat="server" CssClass="btn_controls" Text="Por Gênero"
                            OnClick="btn_alocados_Genero_Click" />
                        <asp:Button ID="btn_alocados_idade" runat="server" CssClass="btn_controls" Text="Por Idade"
                            OnClick="btn_alocados_idade_Click" />
                    </div>
                    <div style="float: right;">
                        <span class="fonteTab">Data de Início:</span> &nbsp;&nbsp;
                        <asp:TextBox ID="TBdataInicio_alocados" runat="server" BorderStyle="Groove" BorderWidth="1px"
                            onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            CssClass="fonteTexto" Height="13px" MaxLength="10" onkeyup="formataData(this,event);"
                            Width="105px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="TBCalendario_CalendarExtenderPlus0" runat="server"
                            Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="TBdataInicio_alocados">
                        </cc2:CalendarExtenderPlus>
                        &nbsp;&nbsp; <span class="fonteTab">Data de Término:</span> &nbsp;&nbsp;
                        <asp:TextBox ID="TBdataTermino_alocados" runat="server" BorderStyle="Groove" BorderWidth="1px"
                            onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            CssClass="fonteTexto" Height="13px" MaxLength="10" onkeyup="formataData(this,event);"
                            Width="105px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server" Format="dd/MM/yyyy"
                            PopupPosition="BottomRight" TargetControlID="TBdataTermino_alocados">
                        </cc2:CalendarExtenderPlus>
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_pesquisa_alocados" runat="server" CssClass="btn_novo" Text="Pesquisar"
                            OnClick="btn_pesquisa_alocados_Click" />
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel10">
                    <ContentTemplate>
                        <asp:MultiView ID="MultiView3" runat="server">
                            <asp:View ID="View9" runat="server">
                                <div class="controls">
                                    <div style="float: right;">
                                        <asp:Button ID="btn_print_Alocados_parceiro" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="pn_left" style="border: none;">
                                            <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CaptionAlign="Top" CssClass="list_results" DataSourceID="SDS_Aprendizes_Alocados"
                                                HorizontalAlign="Center" OnDataBound="GridView9_DataBound" PageSize="8" Style="width: 85%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" SortExpression="ParNomeFantasia" />
                                                    <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                                    <HeaderStyle Width="20%" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </div>
                                        <div class="pn_right" style="border: none;">
                                            <asp:Panel runat="server" ID="pn_alocados_periodo" Visible="false">
                                                <span class="fonteTab" style="float: left;">Selecione o tipo de Gráfico:</span>
                                                <asp:RadioButtonList ID="RB_grafico_alocados_periodo" runat="server" AutoPostBack="true"
                                                    CssClass="fonteTexto" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" Style="float: left; margin-left: 10px;">
                                                    <asp:ListItem Selected="true" Value="1"> Colunas </asp:ListItem>
                                                    <asp:ListItem Value="2"> Pizza(%) </asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <div class="centralizar">
                                                    <asp:Chart ID="Chart4" runat="server" DataSourceID="SDS_Aprendizes_Alocados" Height="250px"
                                                        Width="380px">
                                                        <Series>
                                                            <asp:Series BackGradientStyle="DiagonalLeft" ChartArea="ChartArea1" Font="Tahoma, 9pt, style=Bold"
                                                                IsValueShownAsLabel="True" IsVisibleInLegend="false" IsXValueIndexed="True" LabelBackColor="Transparent"
                                                                Legend="Legend1" Name="Series1" Palette="BrightPastel" XValueMember="ParNomeFantasia"
                                                                YValueMembers="QTD">
                                                            </asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <Area3DStyle Enable3D="True" LightStyle="Realistic" />
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                        <Legends>
                                                            <asp:Legend Docking="Bottom" Name="Legend1">
                                                            </asp:Legend>
                                                        </Legends>
                                                    </asp:Chart>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:SqlDataSource ID="SDS_Aprendizes_Alocados" runat="server" OnSelected="SqlDataSource2_Selected"
                                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                                    Select ParNomeFantasia, count(ALAAprendiz) as QTD from dbo.CA_AlocacaoAprendiz 
                                    INNER JOIN CA_ParceirosUnidade ON ALAUnidadeParceiro = ParUniCodigo
                                    INNER JOIN CA_Parceiros ON  ParUniCodigoParceiro = ParCodigo 
                                    INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo 
                                    INNER JOIN CA_Turmas ON ALATurma = TurCodigo 
                                    WHERE ALAStatus = 'A' 
                                    AND Apr_InicioAprendizagem BETWEEN @dataInicio_alocados 
                                    AND @datatermino_alocados 
                                    and TurCurso = '002'
                                    GROUP BY ParNomeFantasia 
                                    order by count(ALAAprendiz) desc
                                                    ">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TBdataInicio_alocados" Name="dataInicio_alocados" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="TBdataTermino_alocados" Name="datatermino_alocados" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <br />
                                <br />
                            </asp:View>
                            <asp:View ID="View10" runat="server">
                                <div class="controls">
                                    <div style="float: right;">
                                        <asp:Button ID="btn_print_Alocados_analitico" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <div class="centralizar">
                                            <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CaptionAlign="Top" CssClass="list_results_Menor" DataSourceID="SDS_Alocados_analitico"
                                                HorizontalAlign="Center" OnDataBound="GridView_DataBound" Style="width: 98%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" SortExpression="ParNomeFantasia" NullDisplayText="-----">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" NullDisplayText="-----"
                                                        SortExpression="ParUniDescricao">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ALADataInicio" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data de Início"
                                                        SortExpression="ALADataInicio" />
                                                    <asp:BoundField DataField="ALADataTermino" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Termino"
                                                        SortExpression="ALADataTermino" />
                                                    <asp:BoundField DataField="ALAValorBolsa" DataFormatString="{0:f2}" HeaderText="Bolsa(R$)"
                                                        SortExpression="ALAValorBolsa" />
                                                    <asp:BoundField DataField="ALAValorTaxa" DataFormatString="{0:f2}" HeaderText="Taxa(R$)"
                                                        SortExpression="ALAValorTaxa" />
                                                    <asp:BoundField DataField="ALApagto" HeaderText="Pagamento" ReadOnly="True" SortExpression="ALApagto" />
                                                    <asp:BoundField DataField="StaDescricao" HeaderText="Situação" SortExpression="StaDescricao" />
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SDS_Alocados_analitico" runat="server" OnSelected="SqlDataSource1_Selected"
                                                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                                                Select Apr_Nome,TurNome,ParNomeFantasia,ParUniDescricao, ALADataInicio, ALADataPrevTermino,ALADataTermino, ALAValorBolsa, ALAValorTaxa,
                                                ALApagto = case when ALApagto = 'E' then 'Empresa' when ALApagto = 'C' then 'CejaBrasil' end
                                                ,StaDescricao 
                                                from dbo.CA_AlocacaoAprendiz 
                                                INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo 
                                                INNER JOIN CA_Turmas ON ALATurma = TurCodigo 
                                                INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo
                                                LEFT JOIN CA_ParceirosUnidade ON ALAUnidadeParceiro = ParUniCodigo 
                                                LEFT JOIN CA_Parceiros ON  ParUniCodigoParceiro = ParCodigo
                                                WHERE ALAStatus = 'A' AND Apr_InicioAprendizagem 
                                                BETWEEN @dataInicio AND @datatermino
                                                and TurCurso = '002'

                                                ">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="TBdataInicio_alocados" Name="dataInicio" PropertyName="Text" />
                                                    <asp:ControlParameter ControlID="TBdataTermino_alocados" Name="datatermino" PropertyName="Text" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:View>
                            <asp:View ID="view14" runat="server">
                                <div class="controls">
                                    <div style="float: right;">
                                        <asp:Button ID="btn_print_Alocados_Genero" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <div class="pn_left" style="border: none;">
                                            <asp:GridView ID="GridView10" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CaptionAlign="Top" CssClass="list_results" DataSourceID="SDS_Alocados_genero"
                                                HorizontalAlign="Center" OnDataBound="GridView_Total_DataBound" PageSize="8" Style="width: 95%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="sexo" HeaderText="Gênero" SortExpression="MotDescricao">
                                                    <HeaderStyle Width="40%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                                    <HeaderStyle Width="20%" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </div>
                                        <div class="pn_right" style="border: none;">
                                            <span class="fonteTab" style="float: left;">Selecione o tipo de Gráfico:</span>&nbsp;
                                            <asp:RadioButtonList ID="RB_grafico_alocados_genero" RepeatDirection="Horizontal"
                                                AutoPostBack="true" CssClass="fonteTexto" Style="float: left; margin-left: 10px;"
                                                runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="true"> Colunas </asp:ListItem>
                                                <asp:ListItem Value="2"> Pizza(%) </asp:ListItem>
                                            </asp:RadioButtonList>
                                            <br />
                                            <div class="centralizar">
                                                <asp:Chart ID="Chart7" runat="server" DataSourceID="SDS_Alocados_genero" Height="280px"
                                                    Width="380px">
                                                    <Series>
                                                        <asp:Series BackGradientStyle="DiagonalLeft" ChartArea="ChartArea1" Font="Tahoma, 9pt, style=Bold"
                                                            IsValueShownAsLabel="True" IsVisibleInLegend="false" IsXValueIndexed="True" LabelBackColor="Transparent"
                                                            Legend="Legend1" Name="Series1" Palette="BrightPastel" XValueMember="sexo" YValueMembers="QTD">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="ChartArea1">
                                                            <Area3DStyle Enable3D="True" LightStyle="Realistic" />
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                    <Legends>
                                                        <asp:Legend Docking="Bottom" Name="Legend1">
                                                        </asp:Legend>
                                                    </Legends>
                                                </asp:Chart>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:SqlDataSource ID="SDS_Alocados_genero" runat="server" OnSelected="SqlDataSource1_Selected"
                                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                                    Select (Case when Apr_sexo = 'M' then 'Masculino' when Apr_sexo = 'F' then 'Feminino' else 'ND' end) as sexo ,
                                    count(Apr_sexo) as QTD
                                    from dbo.CA_AlocacaoAprendiz 
                                                INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo 
                                                INNER JOIN CA_Turmas ON ALATurma = TurCodigo 
                                                INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo
                                    WHERE ALAStatus = 'A' AND Apr_InicioAprendizagem BETWEEN @dataInicio AND @datatermino
                                    and TurCurso = '002'
                                    group by Apr_sexo
                                    ">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TBdataInicio_alocados" Name="dataInicio" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="TBdataTermino_alocados" Name="datatermino" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </asp:View>
                            <asp:View ID="view15" runat="server">
                                <div class="controls">
                                    <div style="float: right;">
                                        <asp:Button ID="btn_print_Alocados_Idade" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                    <ContentTemplate>
                                        <div class="centralizar" style="width: 50%;">
                                            <asp:GridView ID="GridView11" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CaptionAlign="Top" CssClass="list_results" DataSourceID="SDS_Alocados_Idade"
                                                HorizontalAlign="Center" OnDataBound="GridView9_DataBound" PageSize="8" Style="width: 95%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="Idade" HeaderText="Idade" SortExpression="MotDescricao">
                                                    <HeaderStyle Width="40%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                                    <HeaderStyle Width="20%" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:SqlDataSource ID="SDS_Alocados_Idade" runat="server" OnSelected="SqlDataSource2_Selected"
                                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                                    SELECT Idade, COUNT(Idade) as QTD from  View_Idade_Alocacao 
WHERE ALAStatus = 'A' AND Apr_InicioAprendizagem BETWEEN @dataInicio_alocados AND @datatermino_alocados 
group by Idade order by COUNT(Idade) desc">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TBdataInicio_alocados" Name="dataInicio_alocados" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="TBdataTermino_alocados" Name="datatermino_alocados" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View runat="server" ID="View5">
                <div class="text_titulo">
                    <h1>Total de Aprendizes Desligados no Período por Motivo</h1>
                </div>
                <div class="controls">
                    <div style="float: left;">
                        <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar"
                            OnClick="btn_listar_Click" />
                        <asp:Button ID="btn_imprime_mot" runat="server" CssClass="btn_controls" Text="Imprimir"
                            OnClick="btn_print_Ativos_Click" />
                    </div>
                    <div style="float: right;">
                        <span class="fonteTab">Data de Início:</span> &nbsp;&nbsp;
                        <asp:TextBox ID="tb_data_ini_mot" runat="server" BorderStyle="Groove" BorderWidth="1px"
                            MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                            onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            Width="105px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus3" PopupPosition="BottomRight"
                            runat="server" TargetControlID="tb_data_ini_mot">
                        </cc2:CalendarExtenderPlus>
                        &nbsp;&nbsp; <span class="fonteTab">Data de Término:</span> &nbsp;&nbsp;
                        <asp:TextBox ID="tb_data_fim_mot" runat="server" BorderStyle="Groove" BorderWidth="1px"
                            onblur="javascript:if(this.value !=''  && !VerificaData(this.value)) this.value ='';"
                            MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                            Width="105px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus4" PopupPosition="BottomRight"
                            runat="server" TargetControlID="tb_data_fim_mot">
                        </cc2:CalendarExtenderPlus>
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_pesquisa_mot" runat="server" CssClass="btn_novo" Text="Pesquisar"
                            OnClick="btn_pesquisa_mot_Click" />
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:MultiView ID="MultiView4" runat="server">
                            <asp:View ID="View11" runat="server">
                                <div class="centralizar" style="width: 800px;">
                                    <asp:GridView ID="GridView7" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CaptionAlign="Top" CssClass="list_results" DataSourceID="Sds_motivos_desl" HorizontalAlign="Center"
                                        OnDataBound="GridView_DataBound" PageSize="8" Style="width: 95%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="StaDescricao" HeaderText="Situação" SortExpression="StaDescricao" />
                                            <asp:BoundField DataField="MotDescricao" HeaderText="Motivo do Desligamento" SortExpression="MotDescricao">
                                            <HeaderStyle Width="40%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                            LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                        <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="Sds_motivos_desl" runat="server" OnSelected="SqlDataSource1_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                    Select StaDescricao, MotDescricao, Count(MotCodigo) As QTD  from dbo.CA_AlocacaoAprendiz 
                    INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo 
                    INNER JOIN dbo.CA_SituacaoAprendiz ON Apr_Situacao = StaCodigo 
                    inner join dbo.CA_MotivoDesligamento on MotCodigo = ALAMotivoDesligamento 
                    INNER JOIN CA_Turmas ON ALATurma = TurCodigo 
                    WHERE Apr_FimAprendizagem BETWEEN @dataInicio AND @datatermino 
                    GROUP BY StaDescricao,MotDescricao  
                    order by count(MotCodigo) desc">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="tb_data_ini_mot" Name="dataInicio" PropertyName="Text" />
                        <asp:ControlParameter ControlID="tb_data_fim_mot" Name="datatermino" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
            </asp:View>
            <asp:View runat="server" ID="View3">
                <div class="centralizar">
                    <iframe runat="server" id="Iframe1" src="visualizador.aspx" class="VisualFrame"></iframe>
                </div>
            </asp:View>









            <!--     teste -->

            <asp:View ID="View16" runat="server">
                <div class="text_titulo">
                    <h1>Total de Aprendizes por Tipo de Pagamento</h1>
                </div>

                <div class="controls">
                    <div style="float: right;">
                        <asp:Button ID="btn_print_Tipo_Pagamento" runat="server" CssClass="btn_add" Text="Imprimir" OnClick="btn_print_Ativos_Click" />
                    </div>
                </div>

                <br />
                <div class="controls">
                    <span class="fonteTab">Data de referência:</span>
                    <asp:TextBox ID="txtDataReferenciaTipoPagamento" runat="server" BorderStyle="Groove" BorderWidth="1px"
                        onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                        MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                        Width="105px"></asp:TextBox>
                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus5"
                        PopupPosition="BottomRight" runat="server" TargetControlID="txtDataReferenciaTipoPagamento">
                    </cc2:CalendarExtenderPlus>
                    <asp:Button ID="btnTipoPagamento" runat="server" CssClass="btn_novo" Text="Pesquisar"
                        OnClick="btnTipoPagamento_Click" />
                </div>
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="painelTipoPagamento" Visible="false">
                            <div class="pn_left" style="border: none;">
                                <asp:GridView ID="GridView12" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CaptionAlign="Top" CssClass="list_results" DataSourceID="SDS_resp_Pag"
                                    HorizontalAlign="Center" OnDataBound="GridView_Total_DataBound" PageSize="8" Style="width: 85%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="Responsavel" HeaderText="Responsavel" ReadOnly="True" SortExpression="Responsavel" />
                                        <asp:BoundField DataField="QTD" HeaderText="QTD" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                        LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                    <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                            <div class="pn_right" style="border: none;">
                                <asp:Panel runat="server" ID="Panel1">
                                    <span class="fonteTab" style="float: left;">Selecione o tipo de Gráfico:</span>
                                    <asp:RadioButtonList ID="rb_grafico_pagamento" runat="server" AutoPostBack="true"
                                        CssClass="fonteTexto" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                        RepeatDirection="Horizontal" Style="float: left; margin-left: 10px;">
                                        <asp:ListItem Selected="true" Value="1"> Colunas </asp:ListItem>
                                        <asp:ListItem Value="2"> Pizza(%) </asp:ListItem>
                                    </asp:RadioButtonList>
                                    <br />
                                    <div class="centralizar">
                                        <asp:Chart ID="Chart1" runat="server" DataSourceID="SDS_resp_Pag" Height="280px"
                                            Width="380px">
                                            <Series>
                                                <asp:Series BackGradientStyle="DiagonalLeft" ChartArea="ChartArea1" Font="Tahoma, 9pt, style=Bold"
                                                    IsValueShownAsLabel="True" IsVisibleInLegend="false"
                                                    IsXValueIndexed="True" LabelBackColor="Transparent"
                                                    Legend="Legend1" Name="Series1" Palette="BrightPastel" XValueMember="Responsavel"
                                                    YValueMembers="QTD">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <Area3DStyle Enable3D="True" LightStyle="Realistic" />
                                                </asp:ChartArea>
                                            </ChartAreas>
                                            <Legends>
                                                <asp:Legend Docking="Bottom" Name="Legend1">
                                                </asp:Legend>
                                            </Legends>
                                        </asp:Chart>
                                    </div>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="SDS_resp_Pag" runat="server" OnSelected="SqlDataSource1_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="select  (case when ALApagto ='C' then 'CejaBrasil' when ALApagto ='E' then 'Empresa' end  ) as Responsavel,
                                                                                                                count(ALApagto) as QTD 
                                                                                                                from dbo.CA_AlocacaoAprendiz 
                                                                                                                INNER JOIN dbo.CA_Aprendiz ON ALAAprendiz = Apr_Codigo 
                                                                                                                INNER JOIN CA_Turmas ON ALATurma = TurCodigo 
                                                                                                                where Apr_Situacao = 6 and ALADataInicio  &lt;= @dataReferenciaTipoPagamento and  ALADataPrevTermino &gt;= @dataReferenciaTipoPagamento 
                                                                                                                And TurCurso = '002'
                                                                                                                group by ALApagto">
                     <SelectParameters>
                        <asp:ControlParameter ControlID="txtDataReferenciaTipoPagamento" Name="dataReferenciaTipoPagamento" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
            </asp:View>





















            <asp:View runat="server" ID="View17">
                <div class="text_titulo">
                    <h1>Ativos Por Unidade</h1>
                </div>
               
                <br />
                <br />

                 <div class="controls">
                    <span class="fonteTab">Data de referência:</span>
                    <asp:TextBox ID="txtAtivosPorUnidade" runat="server" BorderStyle="Groove" BorderWidth="1px"
                        onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                        MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                        Width="105px"></asp:TextBox>

                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus9"
                        PopupPosition="BottomRight" runat="server" TargetControlID="txtAtivosPorUnidade">
                    </cc2:CalendarExtenderPlus>
                    <asp:Button ID="Button2" runat="server" CssClass="btn_novo" Text="Pesquisar"
                        OnClick="btnPesquisarAtivosUnidade_Click" />


                </div>
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                       <asp:Panel runat="server" ID="painelAtivosUnidade" Visible="false">
                                <div class="centralizar" style="width: 800px;">
                                    <asp:GridView ID="gridAtivosPorUnidade" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CaptionAlign="Top" CssClass="list_results" DataSourceID="AtivosUnidade" HorizontalAlign="Center"
                                         PageSize="8" Style="width: 60%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="UniNome" HeaderText="Unidade" SortExpression="UniNome">
                                            <HeaderStyle Width="40%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" SortExpression="QTD">
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                            LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                        <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                           </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="AtivosUnidade" runat="server" OnSelected="SqlDataSource1_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                    select count(ALAAprendiz) as QTD, UniNome from CA_AlocacaoAprendiz 
                    inner join CA_Aprendiz on CA_AlocacaoAprendiz.ALAAprendiz = CA_Aprendiz.Apr_Codigo 
                    INNER JOIN dbo.CA_Turmas ON ALATurma = TurCodigo 
                    Inner join CA_Unidades on TurUnidade = UniCodigo
                    WHERE Apr_Situacao = 6 and ALADataInicio  &lt;= @dataReferenciaPorUnidade and  ALADataPrevTermino &gt;= @dataReferenciaPorUnidade
                    and TurCurso = '002'
                    GROUP BY  UniNome
                    order by UniNome">
                     <SelectParameters>
                        <asp:ControlParameter ControlID="txtAtivosPorUnidade" Name="dataReferenciaPorUnidade" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
            </asp:View>












             <asp:View ID="View18" runat="server">

                <div class="text_titulo">
                    <h1>Total de Aprendizes Por Cidade
                    </h1>
                </div>
                <br />
                <br />

                 <div class="controls">
                    <span class="fonteTab">Data de referência:</span>
                    <asp:TextBox ID="txtxAtivosPorCidade" runat="server" BorderStyle="Groove" BorderWidth="1px"
                        onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                        MaxLength="10" CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);"
                        Width="105px"></asp:TextBox>

                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus8"
                        PopupPosition="BottomRight" runat="server" TargetControlID="txtxAtivosPorCidade">
                    </cc2:CalendarExtenderPlus>
                    <asp:Button ID="btnAtivosPorCidadePesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar"
                        OnClick="btnAtivosPorCidadePesquisa_Click1" />


                </div>
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="painelPorcidade" Visible="false">
                            <div class="centralizar" style="width: 50%">
                                <asp:GridView ID="gridPorCidade" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="8" CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center"
                                    Style="width: 85%; margin: auto" OnDataBound="GridView1_DataBound" DataSourceID="SDS_AprendizesAtivosPorCidade" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="Apr_Cidade" HeaderText="Cidade" ItemStyle-HorizontalAlign="Left" SortExpression="Apr_Cidade">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QTD" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="QTD">
                                        <HeaderStyle Width="20%" />
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
                            <div class="controls">
                                <div style="float: right;">
                                    <asp:Button ID="Button3" runat="server" CssClass="btn_add" Text="Imprimir"
                                        OnClick="btn_print_Ativos_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="SDS_AprendizesAtivosPorCidade" runat="server" OnSelected="SqlDataSource3_Selected"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                                            SelectCommand="Select Apr_Cidade, count(Apr_Cidade) as QTD from 
                                                            dbo.CA_AlocacaoAprendiz INNER JOIN dbo.CA_Turmas ON ALATurma = TurCodigo
                                                            inner join CA_Aprendiz on CA_AlocacaoAprendiz.ALAAprendiz = CA_Aprendiz.Apr_Codigo  
                                                             WHERE Apr_Situacao = 6 and ALADataInicio  &lt;= @dataReferenciaPorCidade and  ALADataPrevTermino &gt;= @dataReferenciaPorCidade
                                                            and TurCurso = '002'
                                                            GROUP BY Apr_Cidade
                                                            order by count(Apr_Cidade) desc">

                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtxAtivosPorCidade" Name="dataReferenciaPorCidade" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <br />
            </asp:View>



























        </asp:MultiView>
    </div>
</asp:Content>
