<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="ListaAtivos.aspx.cs" Inherits="ProtocoloAgil.pages.ListaAtivos" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/pesquisa.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function GetInput() {
            var radio = document.getElementsByName("tab-group-1");
            var hidden = document.getElementById('<%# HFSelectedRadio.ClientID %>');
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked)
                    hidden.value = radio[i].id;
            }
        }


        function LoadInput() {
            var radio = document.getElementsByName("tab-group-1");
            var hidden = document.getElementById('<%# HFSelectedRadio.ClientID %>');
            for (var i = 0; i < radio.length; i++) {
                if (hidden.value == radio[i].id)
                    radio[i].checked = true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Jovens > <span>Lista de Aprendizes Ativos</span>
        </p>
    </div>
    <div class="cadastro_pesquisa" style="height: 80px; width: 98%; margin: 10px 1% 10px 1%; border: 1px solid #7f7f7f">

        <div class="linha_cadastro" style="width: 70%;">
            <span class="fonteTab">Nome: </span>&nbsp;
            <asp:TextBox ID="pesquisa" runat="server" CssClass="fonteTexto" Width="230px" />
            &nbsp;&nbsp; <span class="fonteTab">Mês Ref.: </span>&nbsp;
            <asp:DropDownList ID="dd_mes_pesquisa" runat="server" CssClass="fonteTexto" Height="18px"
                Width="120px">
                <asp:ListItem Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="01">Janeiro</asp:ListItem>
                <asp:ListItem Value="02">Fevereiro</asp:ListItem>
                <asp:ListItem Value="03">Março</asp:ListItem>
                <asp:ListItem Value="04">Abril</asp:ListItem>
                <asp:ListItem Value="05">Maio</asp:ListItem>
                <asp:ListItem Value="06">Junho</asp:ListItem>
                <asp:ListItem Value="07">Julho</asp:ListItem>
                <asp:ListItem Value="08">Agosto</asp:ListItem>
                <asp:ListItem Value="09">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp; &nbsp; &nbsp; <span class="fonteTab">Ano Ref.:&nbsp;</span>
            <asp:TextBox runat="server" ID="tb_ano_pesquisa" Width="60px" CssClass="fonteTexto"
                MaxLength="4" onkeyup="formataInteiro(this,event);" />
            &nbsp; &nbsp; &nbsp; <span class="fonteTab">Turma:&nbsp;</span>
            <asp:DropDownList ID="DDturma_pesquisa" runat="server" AppendDataBoundItems="True"  CssClass="fonteTexto" DataTextField="TurNome" DataValueField="TurCodigo" Height="20px" ViewStateMode="Enabled" Width="30%">
            </asp:DropDownList>
             &nbsp;&nbsp;
            <span class="fonteTab">Tipo de Contrato:&nbsp;</span>
            <asp:DropDownList runat="server" CssClass="fonteTexto" ID="DDTipoContrato" >
                <asp:ListItem Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="E">Empresa</asp:ListItem>
                <asp:ListItem Value="C">CejaBrasil</asp:ListItem>
            </asp:DropDownList>
            <span class="fonteTab">
                <br />
                Parceiro:&nbsp;</span>
            <asp:DropDownList ID="DDParceiro" runat="server" CssClass="fonteTexto"
                DataSourceID="SDS_Parceiro" DataTextField="ParDescricao" DataValueField="ParCodigo"
                Height="18px" OnDataBound="IndiceZero" Width="60%">
            </asp:DropDownList>
        </div>

        <asp:SqlDataSource ID="SDS_Parceiro" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
            SelectCommand="  select ParCodigo, ParDescricao from CA_Parceiros order by  ParDescricao"></asp:SqlDataSource>

        <div class="linha_cadastro" style="float: right; width: 70%; margin-top: -45px; text-align: right;">
            <br />
            <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar"
                OnClick="btnpesquisa_Click" />
            <asp:Button ID="btn_print_turma" runat="server" CssClass="btn_novo" Text="Imprimir" OnClick="Button1_Click" />
            <asp:Button ID="btnImprimiFinanceiro" runat="server" CssClass="btn_novo" Text="Imprimir Financ." OnClick="btnImprimiFinanceiro_Click" />
            <asp:Button ID="btnFolhaPonto" Visible="false" runat="server" CssClass="btn_novo" Text="Folha de Ponto" OnClick="txtImpressaoFolha_Click" />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="Tabs">
                <div class="tab">
                    <input type="radio" id="tab-0" name="tab-group-1" onclick="GetInput();" checked="checked" />
                    <label for="tab-0">
                        Geral</label>
                    <div class="conteudo_tab">
                        <span class="fonteTab" style="margin-left: 50px;">Lista de aprendizes independente de
                            datas</span>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="15" CaptionAlign="Top" CssClass="grid_alocacao" HorizontalAlign="Center"
                                    OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="ALAAprendiz" HeaderText="Mat." />
                                        <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz" />
                                        <asp:BoundField DataField="AreaDescricao" HeaderText="Área" />
                                        <asp:BoundField DataField="TurNome" HeaderText="Turma" />
                                        <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" />
                                        <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                                        <asp:BoundField DataField="Apr_InicioAprendizagem" HeaderText="Início" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Apr_PrevFimAprendizagem" HeaderText="Prev. Término" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Apr_FimAprendizagem" HeaderText="Término" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="ALAInicioExpediente" HeaderText="Ini. Exped." DataFormatString="{0:HH:mm}" />
                                        <asp:BoundField DataField="ALATerminoExpediente" HeaderText="Térm. Exped." DataFormatString="{0:HH:mm}" />
                                        <asp:BoundField DataField="ALAValorBolsa" HeaderText="Bolsa" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="ALAValorTaxa" HeaderText="Taxa" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="ALApagto" HeaderText="Pagamento" />
                                        <asp:CommandField ButtonType="Image" HeaderText="Detalhes" SelectImageUrl="~/Images/icon_edit.png"
                                            ShowSelectButton="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle CssClass="Grid_alocacao" />
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                        PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                    <PagerStyle CssClass="nav_results" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-1" name="tab-group-1" onclick="GetInput();" />
                    <label for="tab-1">
                        Por data de Início</label>
                    <div class="conteudo_tab">
                        <span class="fonteTab" style="margin-left: 50px;">Lista de aprendizes que iniciam no
                            mês e ano informados.</span>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="15" CaptionAlign="Top" CssClass="grid_alocacao" HorizontalAlign="Center"
                                    OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="ALAAprendiz" HeaderText="Mat." />
                                        <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz" />
                                        <asp:BoundField DataField="AreaDescricao" HeaderText="Área" />
                                        <asp:BoundField DataField="TurNome" HeaderText="Turma" />
                                        <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" />
                                        <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                                        <asp:BoundField DataField="Apr_InicioAprendizagem" HeaderText="Início" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Apr_PrevFimAprendizagem" HeaderText="Prev. Término" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Apr_FimAprendizagem" HeaderText="Término" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="ALAInicioExpediente" HeaderText="Ini. Exped." DataFormatString="{0:HH:mm}" />
                                        <asp:BoundField DataField="ALATerminoExpediente" HeaderText="Térm. Exped." DataFormatString="{0:HH:mm}" />
                                        <asp:BoundField DataField="ALAValorBolsa" HeaderText="Bolsa" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="ALAValorTaxa" HeaderText="Taxa" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="ALApagto" HeaderText="Pagamento" />
                                        <asp:CommandField ButtonType="Image" HeaderText="Detalhes" SelectImageUrl="~/Images/icon_edit.png"
                                            ShowSelectButton="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle CssClass="Grid_alocacao" />
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                        PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                    <PagerStyle CssClass="nav_results" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-2" name="tab-group-1" onclick="GetInput();" />
                    <label for="tab-2">
                        Por data de Término</label>
                    <div class="conteudo_tab">
                        <span class="fonteTab" style="margin-left: 50px;">Lista de aprendizes que finalizaram
                            no mês e ano informados.</span>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="15" CaptionAlign="Top" CssClass="grid_alocacao" HorizontalAlign="Center"
                                    OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="ALAAprendiz" HeaderText="Mat." />
                                        <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz" />
                                        <asp:BoundField DataField="AreaDescricao" HeaderText="Área" />
                                        <asp:BoundField DataField="TurNome" HeaderText="Turma" />
                                        <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" />
                                        <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                                        <asp:BoundField DataField="Apr_InicioAprendizagem" HeaderText="Início" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Apr_PrevFimAprendizagem" HeaderText="Prev. Término" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Apr_FimAprendizagem" HeaderText="Término" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="ALAInicioExpediente" HeaderText="Ini. Exped." DataFormatString="{0:HH:mm}" />
                                        <asp:BoundField DataField="ALATerminoExpediente" HeaderText="Térm. Exped." DataFormatString="{0:HH:mm}" />
                                        <asp:BoundField DataField="ALAValorBolsa" HeaderText="Bolsa" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="ALAValorTaxa" HeaderText="Taxa" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="ALApagto" HeaderText="Pagamento" />
                                        <asp:CommandField ButtonType="Image" HeaderText="Detalhes" SelectImageUrl="~/Images/icon_edit.png"
                                            ShowSelectButton="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle CssClass="Grid_alocacao" />
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                                        PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                    <PagerStyle CssClass="nav_results" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-3" name="tab-group-1" onclick="GetInput();" />
                    <label for="tab-3">
                        Por Previsão de Término</label>
                    <div class="conteudo_tab">
                        <span class="fonteTab" style="margin-left: 50px;">Lista de aprendizes cuja previsão
                            de término pertence ao mês e ano informados.</span>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="15" CaptionAlign="Top" CssClass="grid_alocacao" HorizontalAlign="Center"
                                    OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="ALAAprendiz" HeaderText="Mat." />
                                        <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz" />
                                        <asp:BoundField DataField="AreaDescricao" HeaderText="Área" />
                                        <asp:BoundField DataField="TurNome" HeaderText="Turma" />
                                        <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" />
                                        <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                                        <asp:BoundField DataField="Apr_InicioAprendizagem" HeaderText="Início" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Apr_PrevFimAprendizagem" HeaderText="Prev. Término" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Apr_FimAprendizagem" HeaderText="Término" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="ALAInicioExpediente" HeaderText="Ini. Exped." DataFormatString="{0:HH:mm}" />
                                        <asp:BoundField DataField="ALATerminoExpediente" HeaderText="Térm. Exped." DataFormatString="{0:HH:mm}" />
                                        <asp:BoundField DataField="ALAValorBolsa" HeaderText="Bolsa" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="ALAValorTaxa" HeaderText="Taxa" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="ALApagto" HeaderText="Pagamento" />
                                        <asp:CommandField ButtonType="Image" HeaderText="Detalhes" SelectImageUrl="~/Images/icon_edit.png"
                                            ShowSelectButton="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="Grid_alocacao" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>


                <div  runat="server" id="divPonto" visible="false">

                    <table style="position:absolute; top:-27px; left:620px" >
                        <tr>
                            <td> <span class="fonteTab">Data Início: </span> </td>
                            <td <span class="fonteTab">Data Término: </span></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDataInicio" runat="server" CssClass="fonteTexto" Height="16px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                    MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                                <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                                    PopupPosition="TopLeft" runat="server" TargetControlID="txtDataInicio">
                                </cc2:CalendarExtenderPlus>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataFim" runat="server" CssClass="fonteTexto" Height="16px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                    MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                                <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1"
                                    PopupPosition="TopLeft" runat="server" TargetControlID="txtDataFim">
                                </cc2:CalendarExtenderPlus>
                            </td>
                            <td>
                                <asp:Button ID="txtImpressaoFolha" runat="server" CssClass="btn_novo" Width="100px" Text="Imprimir" OnClick="txtImpressaoFolha_Click" />
                            </td>
                        </tr>
                    </table>





                </div>



            </div>
            <asp:SqlDataSource ID="SDS_AprendizesSituacao" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                SelectCommand="Select count(Apr_Codigo) as QTD, convert( numeric(10,2),(( convert( numeric(10,2),count(Apr_Codigo) *100))/ convert( numeric(10,2),( select count(Apr_Situacao) 
from CA_Aprendiz inner join CA_SituacaoAprendiz on  Apr_Situacao = StaCodigo  ))))as Percentual,   CA_SituacaoAprendiz.StaDescricao
from  dbo.CA_Aprendiz a  inner join CA_SituacaoAprendiz on  Apr_Situacao = StaCodigo group by StaDescricao"></asp:SqlDataSource>
            <asp:HiddenField ID="HFRowCount" runat="server" />
            <asp:HiddenField ID="HFSelectedRadio" runat="server" />
            <br />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="centralizar" style="border: none;">
                <iframe id="IFrame2" height="720px" width="98%" style="border: none;" name="IFrame2"
                    src="CadastroAprendiz.aspx"></iframe>
            </div>
        </asp:View>
        <asp:View runat="server" ID="View3">
            <div class="controls">
                <div style="float: right; margin-right: 30px;">
                    <asp:Button runat="server" ID="btn_voltar" CssClass="btn_novo" Text="Voltar"
                        OnClick="btn_voltar_Click" />
                </div>
            </div>
            <div class="centralizar">
                <iframe runat="server" id="Iframe1" src="visualizador.aspx" class="VisualFrame"></iframe>
            </div>
        </asp:View>





    </asp:MultiView>
</asp:Content>
