<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.CadastroTurma"
    CodeBehind="CadastroTurma.aspx.cs" %>

<%@ Register TagPrefix="MKB" Namespace="MKB.TimePicker" Assembly="TimePicker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d25e9f59e49c4d2f" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
        if (confirm("Deseja realmente excluir esta turma?") == true)
            hf.value = "true";
        else
            hf.value = "false";
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Pedagógico > <span>Cadastro de Turmas</span>
            </p>
        </div>
        <div class="controls">

            <div style="float: left;">
                <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="btn_listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="btn_novo_Click" />
                <asp:Button ID="btn_relatorio" runat="server" CssClass="btn_controls" Text="Relatório" OnClick="btn_relatorio_Click" />
                <asp:Button ID="btn_texto" runat="server" CssClass="btn_controls" Text="Arquivo de Texto" OnClick="btn_texto_Click" />
            </div>
            <div style="float: right;">
                <asp:TextBox ID="pesquisa" runat="server" CssClass="search_controls" />
                <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar"
                    OnClick="btnpesquisa_Click" />
            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list_results"
                            AutoGenerateColumns="False" PageSize="12" OnDataBound="GridView_DataBound"
                            Style="width: 60%; margin: 0 auto" HorizontalAlign="Center"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                            OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="TurCodigo" HeaderText="Código" InsertVisible="False" ReadOnly="True" SortExpression="TurCodigo">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TurNome" HeaderText="Turma" SortExpression="TurNome">
                                    <HeaderStyle Width="50%" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/Images/icon_edit.png" ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBexcluir" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TurCodigo")%>' ImageUrl="~/images/icon_remove.png" OnClick="IMBexcluir_Click" OnClientClick="javascript:GetConfirm();" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="10%" />
                                </asp:TemplateField>
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
                            <sortedascendingcellstyle backcolor="#E9E7E2" />
                            <sortedascendingheaderstyle backcolor="#506C8C" />
                            <sorteddescendingcellstyle backcolor="#FFFDF8" />
                            <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                        </asp:GridView>
                        <asp:HiddenField runat="server" ID="HFConfirma" />
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:Panel ID="Panel4" runat="server" Height="400px" Width="100%">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="Table FundoPainel" style="width: 80%; margin: 0 auto;">
                                <tr>
                                    <td class="cortitulo corfonte titulo" style="font-size: large;">Cadastro de Turmas</td>
                                </tr>

                                <table class="Table FundoPainel" style="width: 80%; margin: 0 auto;">
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="fonteTab Tam12" style="text-align: left;">Código</td>
                                        <td class=" Tam30 fonteTab" style="text-align: left;">Nome da Turma</td>
                                        <td class="Tam15 fonteTab" style="text-align: left;">Situação</td>
                                        <td class="Tam15 fonteTab" style="text-align: left;">Dia da Semana</td>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="fonteTab" style="text-align: left;">
                                            <asp:TextBox ID="TBcodigo" runat="server" CssClass="fonteTexto" MaxLength="10" Enabled="false"
                                                Style="width: 90%; height: 14px;"> </asp:TextBox>
                                        </td>
                                        <td class="fonteTab" style="text-align: left;">
                                            <asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" MaxLength="20"
                                                Style="width: 90%; height: 14px;"></asp:TextBox>
                                        </td>
                                        <td class="fonteTab">
                                            <asp:DropDownList ID="DDstatus" runat="server" CssClass="fonteTexto" Height="20px"
                                                OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();" Width="90%">
                                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                                <asp:ListItem Value="A">Ativo</asp:ListItem>
                                                <asp:ListItem Value="I">Inativo</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="fonteTab" style="text-align: left;">
                                            <asp:DropDownList ID="DDdiaSemana" runat="server" CssClass="fonteTexto" Height="20px" OnDataBound="IndiceZero"
                                                onkeydown="ModifyEnterKeyPressAsTab();" Width="90%">
                                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                                <asp:ListItem Value="1">Segunda a Sexta</asp:ListItem>
                                                <asp:ListItem Value="2">Segunda</asp:ListItem>
                                                <asp:ListItem Value="3">Terça</asp:ListItem>
                                                <asp:ListItem Value="4">Quarta</asp:ListItem>
                                                <asp:ListItem Value="5">Quinta</asp:ListItem>
                                                <asp:ListItem Value="6">Sexta</asp:ListItem>
                                                <asp:ListItem Value="7">Sábado</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                    </tr>
                                </table>
                                <table class="Table FundoPainel" style="width: 80%; margin: 0 auto;">
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="fonteTab Tam40" style="text-align: left;">Curso</td>

                                        <td class="Tam35 fonteTab" style="text-align: left;">Plano Curricular</td>
                                    </tr>
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="fonteTab">
                                            <asp:DropDownList ID="DDcurso" runat="server" CssClass="fonteTexto" AutoPostBack="true"
                                                DataTextField="CurDescricao" DataValueField="CurCodigo" Height="20px"
                                                OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();"
                                                Width="95%" OnSelectedIndexChanged="DDcurso_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="fonteTab">
                                            <asp:DropDownList ID="DD_plano_Curricular" runat="server" CssClass="fonteTexto" Height="20px"
                                                OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();" Width="80%" DataTextField="PlanDescricao" DataValueField="PlanCodigo">
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                                <table class="Table FundoPainel" style="width: 80%; margin: 0 auto;">
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="Tam40 fonteTab">Unidade</td>
                                        <td class=" Tam10 fonteTab" style="text-align: left;">Hora Início</td>
                                        <td class="Tam10 fonteTab">Hora Final</td>

                                        <td class="Tam10 fonteTab">Número de Aulas</td>

                                    </tr>
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="fonteTab">
                                            <asp:DropDownList ID="DD_unidade_turma" runat="server" CssClass="fonteTexto" DataTextField="UniNome"
                                                DataValueField="UniCodigo" Height="20px" OnDataBound="IndiceZero"
                                                onkeydown="ModifyEnterKeyPressAsTab();" Width="95%" DataSourceID="SDS_Unidades">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="fonteTab">
                                            <MKB:TimeSelector ID="TS_inicio" runat="server" AmPm="PM" Height="15px" SelectedTimeFormat="TwentyFour"
                                                DisplaySeconds="False" EnableTheming="True" MinuteIncrement="10">
                                            </MKB:TimeSelector>
                                        </td>
                                        <td class="Tam10 fonteTab">
                                            <MKB:TimeSelector ID="TS_final" runat="server" AmPm="PM" Height="15px" SelectedTimeFormat="TwentyFour"
                                                DisplaySeconds="False" EnableTheming="True" MinuteIncrement="10">
                                            </MKB:TimeSelector>
                                        </td>

                                        <td class="Tam15 fonteTab">
                                            <asp:TextBox ID="TBMesesPrevistos" runat="server" CssClass="fonteTexto" onkeyup="formataInteiro(this,event);" MaxLength="2" Style="width: 50px; height: 14px;"> </asp:TextBox>
                                        </td>

                                    </tr>
                                      <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="Tam40 fonteTab">Educador</td>

                                      </tr>
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="DDEducador" runat="server" CssClass="fonteTexto"
                                                DataTextField="EducNome" DataValueField="EducCodigo"
                                                Height="18px" OnDataBound="IndiceZero"
                                                Width="80%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="espaco" colspan="5"></td>
                                    </tr>
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="fonteTab">Observações</td>
                                        <td class="fonteTab" colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="fonteTab Tam05">&nbsp;</td>
                                        <td class="fonteTab" colspan="4">
                                            <asp:TextBox ID="TBobservacao" runat="server" CssClass="fonteTexto" TextMode="MultiLine"
                                                Style="width: 94%; height: 40px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="controls" style="width: 70%; margin: 0 auto; text-align: center;">
                        <asp:Button ID="BTaltera" runat="server" OnClick="BTaltera_Click" CssClass="btn_novo" Text="Confirmar" />
                        &nbsp;
                         <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar"
                             OnClick="btn_listar_Click" />

                    </div>
                </asp:Panel>
                <asp:SqlDataSource ID="SDS_Unidades" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                    SelectCommand="SELECT [UniCodigo], [UniNome] FROM [CA_Unidades]"></asp:SqlDataSource>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="centralizar">
                    <iframe id="IFrame1" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
