<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="PesquisaVagas.aspx.cs" Inherits="ProtocoloAgil.pages.PesquisaVagas" %>

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
            Configurações > <span>Pesquisa Vagas</span>
        </p>
    </div>
    <div class="cadastro_pesquisa" style="height: 150px; width: 98%; margin: 10px 1% 10px 1%; border: 1px solid #7f7f7f">

        <div class="linha_Vaga" style="width: 100%;">
            <table width="100%">

                <tr>
                    <td>
                        <span class="fonteTab">Empresa: </span>&nbsp;
                    </td>
                    <td>
                        <span class="fonteTab">Área Atuação </span>
                    </td>
                    <td>
                        <span class="fonteTab">Data:</span>
                    </td>
                    <td>
                        <span class="fonteTab">Sexo:</span>
                    </td>

                </tr>
                <tr>
                    <td style="width: 35%">

                        <asp:DropDownList ID="DDEmpresa" runat="server" CssClass="fonteTexto" DataTextField="ParUniDescricao"
                            DataValueField="ParUniCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                            Width="50%" OnDataBound="IndiceZero"
                            ViewStateMode="Enabled">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 35%">
                        <asp:DropDownList ID="DDAreaAtuacao" runat="server" CssClass="fonteTexto" DataTextField="AreaDescricao" DataValueField="AreaCodigo"
                            Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                            Width="70%" OnDataBound="IndiceZero"
                            ViewStateMode="Enabled">
                        </asp:DropDownList>
                    </td>

                    <td>
                        <asp:TextBox ID="txtDataSolicitacao" runat="server" CssClass="fonteTexto" Height="16px"
                            onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                            PopupPosition="BottomRight" runat="server" TargetControlID="txtDataSolicitacao">
                        </cc2:CalendarExtenderPlus>
                    </td>

                    <td class="fonteTab">
                        <asp:DropDownList ID="DDSexo" CssClass="fonteTexto" runat="server">
                            <asp:ListItem Value="">Selecione</asp:ListItem>
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                            <asp:ListItem Value="F">Feminino</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <span class="fonteTab">Situação:</span>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:DropDownList class="fonteTab" runat="server" ID="DDSituacaoVagaPesquisa">
                            <asp:ListItem Value="">Selecione</asp:ListItem>
                            <asp:ListItem Value="A">Aberta</asp:ListItem>
                            <asp:ListItem Value="B">Aprovado</asp:ListItem>
                            <asp:ListItem Value="C">Cancelada</asp:ListItem>
                            <asp:ListItem Value="E">Encerrado</asp:ListItem>
                            <asp:ListItem Value="S">Seleção</asp:ListItem>


                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar"
                            OnClick="btnpesquisa_Click" />
                    </td>
                </tr>

            </table>
        </div>



    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">

            <br />
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView3" runat="server" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="Não existem vagas para esta pesquisa" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="15" CaptionAlign="Top" CssClass="list_results" Style="width: 98%; margin: auto" HorizontalAlign="Center"
                        OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView3_RowCommand"
                        OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="ReqId" HeaderText="Cód Vaga" SortExpression="ReqId"></asp:BoundField>
                            <asp:BoundField DataField="AreaDescricao" HeaderText="Área Atuação" SortExpression="AreaDescricao"></asp:BoundField>
                            <asp:BoundField DataField="ParUniDescricao" HeaderText="Empresa" SortExpression="ParUniDescricao" />
                            <asp:BoundField DataField="ReqSexo" HeaderText="Sexo" SortExpression="ReqSexo" />
                            <asp:BoundField DataField="ReqDataSolitação" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" />
                            <asp:BoundField DataField="ReqAtividades" HeaderText="Atividades" SortExpression="ReqAtividades" />
                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                ShowSelectButton="True" />
                            <asp:ButtonField ButtonType="Image" CommandName="Encaminhamentos" ImageUrl="~/images/visualizar.png"
                                Text="Button" HeaderText="Encaminhamentos">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>

                            <asp:ButtonField ButtonType="Image" CommandName="Imprimir" HeaderText="Imprimir" ImageUrl="~/images/cs_print.gif" Text="Button">
                                <HeaderStyle HorizontalAlign="Center" Width="2%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>


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
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:HiddenField ID="HFRowCount" runat="server" />
            <asp:HiddenField ID="HFSelectedRadio" runat="server" />
            <br />
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





        <asp:View ID="View2" runat="server">
            <div style="width: 80%; margin: 0 auto;">
                <br />
                <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo titulo corfonte" colspan="4" style="font-size: large">Dados da Vaga
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam15">&nbsp;&nbsp;&nbsp; Código da Vaga
                        </td>
                        <td class="fonteTab Tam15">&nbsp;&nbsp; Empresa 
                        </td>
                        <td class="fonteTab Tam15">&nbsp;&nbsp; Data Solicitação
                        </td>
                        <td class="fonteTab Tam15">&nbsp;&nbsp; Quantidade
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;
                            
                                <asp:TextBox ID="txtCodigoVaga" runat="server" Height="13px"
                                    MaxLength="4" onkeyup="formataInteiro(this,event);" Enabled="false" Width="50%"
                                    CssClass="fonteTexto"></asp:TextBox>
                        </td>
                        <td class="fonteTab Tam15">
                            <asp:Label ID="Label1" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:DropDownList ID="DDEmpresaEditar" runat="server" CssClass="fonteTexto" DataTextField="ParUniDescricao"
                                DataValueField="ParUniCodigo" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="80%" OnDataBound="IndiceZero" OnSelectedIndexChanged="DDEmpresaEditar_SelectedIndexChanged" AutoPostBack="true"
                                ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>
                        <td class="fonteTab Tam15" style="text-align: left;">
                            <asp:TextBox ID="txtDataSolicitacaoEditar" runat="server" CssClass="fonteTexto" Height="16px"
                                onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1"
                                PopupPosition="BottomRight" runat="server" TargetControlID="txtDataSolicitacaoEditar">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="fonteTab Tam15">

                            <asp:TextBox ID="txtQuantidade" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="10" onkeyup="formataInteiro(this, event);" Width="80%"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="4" class="fonteTab">Endereço Empresa</td>

                    </tr>
                    <tr>
                        <td colspan="4">

                            <asp:Label ID="lblEnderecoEmpresa" runat="server" BackColor="White" Width="98%"
                                Height="30px" BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:Label>


                        </td>
                    </tr>

                    <tr>
                        <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;&nbsp; Sexo
                        </td>
                        <td class="fonteTab Tam08">&nbsp;&nbsp; Horário Entrevista
                        </td>
                        <td class="fonteTab Tam15">&nbsp;&nbsp; Substituição
                        </td>

                        <td class="fonteTab Tam05" style="text-align: left;">Substituindo
                        </td>

                    </tr>
                    <tr>
                        <td class="fonteTab">&nbsp;&nbsp;
                                <asp:DropDownList ID="DDSexoEditar" CssClass="fonteTexto" runat="server">
                                    <asp:ListItem Value="">Selecione</asp:ListItem>
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                        <td class="fonteTab Tam05">

                            <asp:TextBox ID="txtHorarioEntrevista" MaxLength="50" runat="server"
                                CssClass="fonteTexto" Height="13px" Width="70%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDSubstituicao" CssClass="fonteTexto" runat="server">
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">Não</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="fonteTab Tam15">

                            <asp:TextBox ID="txtSubstituicao" runat="server" CssClass="fonteTexto"
                                Height="13px" MaxLength="80" Width="250px"></asp:TextBox>
                        </td>




                    </tr>
                    <tr>
                        <td class="fonteTab Tam05" style="text-align: left;">&nbsp;&nbsp; Área de Atuação
                        </td>
                        <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;&nbsp; Salário
                        </td>
                        <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;&nbsp; Horário Trabalho
                        </td>
                        <td class="fonteTab Tam15" style="text-align: left;">Situação
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Label ID="Label4" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                            <asp:DropDownList ID="DDAreaAtuacaoEditar" runat="server" CssClass="fonteTexto" DataTextField="AreaDescricao" DataValueField="AreaCodigo"
                                Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="85%" OnDataBound="IndiceZero"
                                ViewStateMode="Enabled">
                            </asp:DropDownList>
                        </td>

                        <td>
                            <asp:TextBox ID="txtSalario" MaxLength="50" runat="server"
                                CssClass="fonteTexto" onkeyup="formataValor(this, event);" Height="13px" Width="70%"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtHorarioTrabalho" runat="server" CssClass="fonteTexto" Height="13px" Width="85%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList class="fonteTab" runat="server" ID="DDSituacao">
                                <asp:ListItem Value="A">Ativo</asp:ListItem>
                                <asp:ListItem Value="E">Encerrado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table class="Table FundoPainel">
                    <tr>
                        <td class="fonteTab Tam20">&nbsp;&nbsp; Características Pessoais
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">

                            <asp:TextBox ID="txtCaracteristicaPessoal" TextMode="MultiLine" Rows="3" runat="server"
                                CssClass="fonteTexto" Height="43px" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam20">&nbsp;&nbsp; Habilidades
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">

                            <asp:TextBox ID="txtHabilidades" TextMode="MultiLine" Rows="3" runat="server"
                                CssClass="fonteTexto" Height="33px" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam20">&nbsp;&nbsp; Atividades 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txtAtividades" CssClass="fonteTexto" TextMode="MultiLine" Rows="3" runat="server" Height="33px"
                                Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam20">&nbsp; &nbsp; Contato Entrevista
                        </td>
                    </tr>


                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txtCartaoEntrevista" runat="server" TextMode="MultiLine" Rows="3" CssClass="fonteTexto" Height="33px" Width="85%"></asp:TextBox>
                        </td>
                    </tr>



                    <tr>
                        <td class="fonteTab Tam20">&nbsp; Benefício
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txtBenefício" TextMode="MultiLine" Rows="3" runat="server" CssClass="fonteTexto" Height="33px" Width="85%"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="fonteTab Tam20">&nbsp; Observação
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txtObservacao" TextMode="MultiLine" Rows="3" runat="server" CssClass="fonteTexto" Height="33px" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fonteTab Tam20">&nbsp; Observação Instituição
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txtObservacaoInst" TextMode="MultiLine" Rows="3" runat="server" CssClass="fonteTexto" Height="33px" Width="85%"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>








                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>




                </table>
                <div class="fonteTexto" style="float: left;">
                    <p>
                        <b>Obs.:</b> Os campos com (<asp:Label ID="Label10" runat="server" Font-Size="11pt" ForeColor="Red"
                            Text="*"></asp:Label>) indicam dados obrigatórios.
                    </p>
                </div>
                <div class="controls">
                    <div class="centralizar">
                        <asp:Button ID="BTsalva" runat="server" CssClass="btn_novo" OnClick="BTsalva_Click"
                            Text="Salvar" />
                        &nbsp;
                        <asp:Button ID="BTLimpa" runat="server" Text="Limpar" OnClick="BTLimpa_Click" CssClass="btn_novo" />
                    </div>
                </div>
                <br />
            </div>
        </asp:View>













        <asp:View ID="View4" runat="server">

            <br />

            <table class="Table FundoPainel">
                <tr>
                    <td class="cortitulo titulo corfonte" colspan="4" style="font-size: large">Dados da Vaga
                    </td>
                </tr>
                <tr>
                    <td class="fonteTab Tam15">Código da Vaga
                    </td>
                    <td class="fonteTab Tam15">Empresa 
                    </td>
                    <td class="fonteTab Tam15">Data Solicitação
                    </td>
                    <td class="fonteTab Tam15">Quantidade
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblCodigoVaga" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblEmpresa" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblDataSolicitacao" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblQuantidade" CssClass="fonteTexto"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="fonteTab Tam15" style="text-align: left;">Sexo
                    </td>
                    <td class="fonteTab Tam08">Horário Entrevista
                    </td>
                    <td class="fonteTab Tam15">Substituição
                    </td>

                    <td class="fonteTab Tam05">Área de Atuação
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblSexo" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblHorarioEntrevista" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSubstituicao" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblAreaAtuacao" CssClass="fonteTexto"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="fonteTab Tam20">Salário
                    </td>
                    <td class="fonteTab Tam20">Horário Trabalho
                    </td>
                    <td class="fonteTab Tam20">Situação
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblSalario" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblHorarioTrabalho" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSituacao" CssClass="fonteTexto"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td class="fonteTab Tam20" colspan="2">Características Pessoais
                    </td>
                    <td class="fonteTab Tam20" colspan="2">Habilidades
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblCaracteriscasPessoais" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblHabilidades" CssClass="fonteTexto"></asp:Label>
                    </td>
                </tr>
                <tr>

                    <td class="fonteTab Tam20" colspan="2">Atividades 
                    </td>
                    <td class="fonteTab Tam20" colspan="2">Contato Entrevista
                    </td>
                </tr>
                <tr>

                    <td colspan="2">
                        <asp:Label runat="server" ID="lblAtividade" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblContatoEntrevista" CssClass="fonteTexto"></asp:Label>
                    </td>
                </tr>











                <tr>
                    <td class="fonteTab Tam20" colspan="2">Benefício
                    </td>
                    <td class="fonteTab Tam20" colspan="2">Observação
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblBeneficio" CssClass="fonteTexto"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblObservacao" CssClass="fonteTexto"></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td class="fonteTab Tam20">Observação Instituição
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Label runat="server" ID="lblObservaoInst" CssClass="fonteTexto"></asp:Label>
                    </td>
                </tr>











            </table>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridEncaminhamentos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="15" CaptionAlign="Top" CssClass="list_results" Style="width: 98%; margin: auto" HorizontalAlign="Center"
                        OnDataBound="GridView_DataBound"
                        OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="CodAprendiz" HeaderText="Cod Aprendiz" SortExpression="CodAprendiz"></asp:BoundField>
                            <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome"></asp:BoundField>
                            <asp:BoundField DataField="Telefone" HeaderText="Telefone" SortExpression="Telefone"
                                NullDisplayText="(__) ____-____">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"></asp:BoundField>
                            <asp:BoundField DataField="SituacaoJovem" HeaderText="Situação" SortExpression="SituacaoJovem"></asp:BoundField>


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
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />

            <div class="centralizar">
                <asp:Button ID="btnVoltar" runat="server" CssClass="btn_novo" OnClick="btnVoltar_Click"
                    Text="Voltar" />

            </div>
        </asp:View>











    </asp:MultiView>
</asp:Content>
