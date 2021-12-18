<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="CadastroVagas.aspx.cs" Inherits="ProtocoloAgil.pages.CadastroVagas" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50731.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir esta escola?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel8" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro de Vagas</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Vagas" OnClick="listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text=" Nova Vaga" OnClick="Incluir_Click" />
            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="text_titulo" style="float: none;">
                    <h1>Vagas Cadastradas
                    </h1>
                </div>
                <br />
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" Style="width: 95%; margin: auto" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1"
                    OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="ReqId" HeaderText="Cód Vaga" SortExpression="ReqId"></asp:BoundField>
                        <asp:BoundField DataField="AreaDescricao" HeaderText="Área Atuação" SortExpression="AreaDescricao"></asp:BoundField>
                        <asp:BoundField DataField="ParUniDescricao" HeaderText="Empresa" SortExpression="ParUniDescricao" />
                        <asp:BoundField DataField="Situacao" HeaderText="Situacao" SortExpression="Situacao" />
                        <asp:BoundField DataField="ReqSexo" HeaderText="Sexo" SortExpression="ReqSexo" />
                        <asp:BoundField DataField="ReqDataSolitação" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" />
                        <asp:BoundField DataField="ReqAtividades" HeaderText="Atividades" SortExpression="ReqAtividades" />
                        <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                            ShowSelectButton="True" />
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
                <asp:HiddenField ID="HFRowCount" runat="server" />
                <asp:HiddenField ID="HFEscolaRef" runat="server" />
                <asp:HiddenField runat="server" ID="HFConfirma" />
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
                                <asp:DropDownList ID="DDEmpresa" runat="server" CssClass="fonteTexto" DataTextField="ParUniDescricao"
                                    DataValueField="ParUniCodigo" Height="20px" OnSelectedIndexChanged="DDEmpresa_SelectedIndexChanged" AutoPostBack="true" onkeydown="ModifyEnterKeyPressAsTab();"
                                    Width="80%" OnDataBound="IndiceZero"
                                    ViewStateMode="Enabled">
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab Tam15" style="text-align: left;">
                                <asp:TextBox ID="txtDataSolicitacao" runat="server" CssClass="fonteTexto" Height="16px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                    MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                                <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                                    PopupPosition="BottomRight" runat="server" TargetControlID="txtDataSolicitacao">
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
                            <td class="fonteTab Tam05">Substituição
                            </td>

                            <td class="fonteTab Tam15" style="text-align: left;">Substituindo
                            </td>

                        </tr>
                        <tr>
                            <td class="fonteTab">&nbsp;&nbsp;
                                <asp:DropDownList ID="DDSexo" CssClass="fonteTexto" runat="server">
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
                    </table>
                    <table>
                        <tr>
                            <td class="fonteTab Tam15" style="text-align: left;">Área de Atuação
                            </td>
                            <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;&nbsp; Salário
                            </td>
                            <td class="fonteTab Tam15" style="text-align: left;">&nbsp;&nbsp;&nbsp; Horário Trabalho
                            </td>
                            <td class="fonteTab Tam15" style="text-align: left;">Situação
                            </td>
                            <td class="fonteTab Tam15" style="text-align: left;">Data Entrevista
                            </td>
                        </tr>

                        <tr>
                            <td>

                                <asp:Label ID="Label4" runat="server" Font-Size="11pt" ForeColor="Red" Text="*"></asp:Label>
                                <asp:DropDownList ID="DDAreaAtuacao" runat="server" CssClass="fonteTexto" DataTextField="AreaDescricao" DataValueField="AreaCodigo"
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
                                <asp:DropDownList class="fonteTab" AutoPostBack="true" OnTextChanged="DDSituacao_TextChanged" runat="server" ID="DDSituacao">
                                    <asp:ListItem Value="A">Aberta</asp:ListItem>
                                    <asp:ListItem Value="B">Aprovado</asp:ListItem>
                                    <asp:ListItem Value="C">Cancelada</asp:ListItem>
                                    <asp:ListItem Value="E">Encerrado</asp:ListItem>
                                    <asp:ListItem Value="S">Seleção</asp:ListItem>
                                    


                                </asp:DropDownList>
                            </td>

                            <td class="fonteTab Tam15" style="text-align: left;">
                                <asp:TextBox ID="txtDataEntrevista" runat="server" CssClass="fonteTexto" Height="16px"
                                    onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                    MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                                <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1"
                                    PopupPosition="BottomRight" runat="server" TargetControlID="txtDataEntrevista">
                                </cc2:CalendarExtenderPlus>
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
            <asp:View ID="View3" runat="server">
                <div class="centralizar" style="border: none;">
                    <iframe id="IFrame3" class="VisualFrame" style="border: none;" name="IFrame2"
                        src="visualizador.aspx"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:HiddenField runat="server" ID="HFCampos"></asp:HiddenField>
</asp:Content>
