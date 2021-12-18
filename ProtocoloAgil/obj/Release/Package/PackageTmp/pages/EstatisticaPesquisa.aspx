<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="EstatisticaPesquisa.aspx.cs" Inherits="ProtocoloAgil.pages.EstatisticaPesquisa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetConfirm(cb) {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente alterar a situação desta pesquisa?") == true) {
                hf.value = "true";
            }
            else {
                hf.value = "false";
                cb.children[0].checked = !cb.children[0].checked;
            }
        }
    </script>

    <style>
        .Table {
            width: 800px;
            border: 1px black solid;
            border-collapse: collapse;
            margin: 0 auto;
            background: #dfdfdf;
        }

            .Table th {
                border: 1px black solid;
                font: 10pt Arial,sans-serif;
                text-align: center;
                font-weight: bold;
                color: #ffffff;
                background: #00599c;
            }

            .Table td {
                border: 1px black solid;
                font: 9pt Arial,sans-serif;
                text-align: left;
                text-align: center;
                font-weight: bold;
                color: #787878;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Estatísticas > <span>Gestão de Avaliações </span>
        </p>
    </div>

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="text_titulo">
                <h1>Avaliações Ensino Aprendizagem no Período</h1>
            </div>
            <br />
            <table class="FundoPainel centralizar" style="width: 98%; border: solid 1px #787878;">
                <tr>
                    <td class="espaco" colspan="9"></td>
                    <td rowspan="3" style="width: 15%;">&nbsp;
                <asp:Button ID="btn_pesquisa" runat="server" CssClass="btn_novo" OnClick="btnpesquisa_Click"
                    Text="Pesquisar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                    <td class="Tam12 fonteTab" style="text-align: right;">Responsável: &nbsp;&nbsp;
                    </td>
                    <td class="Tam25 fonteTab">
                        <asp:DropDownList ID="dd_responsavel" runat="server" DataTextField="UsuNome" DataValueField="UsuCodigo" OnDataBound="IndiceZero" CssClass="fonteTexto" Height="18px" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td class="Tam08 fonteTab" style="text-align: right;">Mês: &nbsp;&nbsp;
                    </td>
                    <td class="Tam12 fonteTab">
                        <asp:DropDownList ID="DDmeses" runat="server" CssClass="fonteTexto" Height="18px"
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
                    </td>
                    <td class="Tam08 fonteTab" style="text-align: right;">Ano: &nbsp;&nbsp;
                    </td>
                    <td class="Tam08 fonteTab">
                        <asp:TextBox ID="TB_ano_ref" runat="server" MaxLength="4" onkeyup="formataInteiro(this,event);"
                            CssClass="fonteTexto" Height="13px" Width="60%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" class="espaco">&nbsp;
                    </td>
                </tr>
            </table>
            <div class="controls">
                <div style="float: left;">
                    <asp:Button ID="btn_geral" runat="server" CssClass="btn_controls" Text="Geral" OnClick="Novo_Click" />
                    <asp:Button ID="btn_realizadas" runat="server" CssClass="btn_controls" Text="Realizadas"
                        OnClick="listar_Click" />
                    <asp:Button ID="btn_pendentes" runat="server" CssClass="btn_controls" Text="Pendentes"
                        OnClick="Button1_Click" />
                    <asp:Button ID="btnGerarNovas" runat="server" CssClass="btn_controls" Text="Gerar Novas"
                        OnClick="btnGerarNovas_Click" />
                </div>
                <div style="float: right;">
                    <asp:Button ID="btn_print" runat="server" CssClass="btn_novo" Text="Imprimir" OnClick="btn_print_Click" />
                    <asp:Button ID="btnGerar" Visible="false" runat="server" CssClass="btn_novo" Text="Gerar" OnClick="btnGerar_Click" />
                </div>
            </div>
            <div class="centralizar">
                <div class="formatoTela_02">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" CssClass="list_results_Menor"
                                PageSize="15" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PesNome" HeaderText="Pesquisa">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepMes" HeaderText="Mes">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepAno" HeaderText="Ano">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Situacao" HeaderText="Situação">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Visualizar">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="IMB_visualizar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PepCodigo") %>'
                                                ImageUrl="~/Images/lupa.png" Width="23px" Height="23px"
                                                OnClick="IMB_visualizar_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle CssClass="List_results_menor" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                                <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="HFConfirma" />

                            <asp:GridView ID="GridGerarNovas" AutoGenerateColumns="False" runat="server" CssClass="list_results_Menor"
                                PageSize="15" HorizontalAlign="Center" Style="width: 57%; margin: auto"
                                AllowPaging="True" OnPageIndexChanging="GridGerarNovas_PageIndexChanging" CellPadding="2" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>

                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle CssClass="List_results_menor" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                    LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                                <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div runat="server" visible="false" id="divPesquisa" style="position: relative; top: 100px; left: 10px">
                      
                            <h5>Escolha a pesquisa para gerar</h5>
                      
                        <asp:DropDownList ID="DDPesquisa" runat="server" CssClass="fonteTexto"
                            DataTextField="PesDescricao" DataValueField="PesCodigo"
                            Height="18px" OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();"
                            Width="30%">
                        </asp:DropDownList>
                        <asp:Button ID="btnGerarDados" runat="server" CssClass="btn_novo" Text="Gerar Dados" OnClick="btnGerarDados_Click" />
                    </div>


                </div>
            </div>









        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="controls">
                <div style="float: right;">
                    <asp:Button ID="btn_Vontar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btn_voltar_Click" />
                </div>
            </div>
            <div class="centralizar">
                <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1"></iframe>
            </div>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="controls">
                <div style="float: right; margin-right: 30px">
                    <asp:Button ID="btn_voltar" runat="server" Text="Voltar" CssClass="btn_novo"
                        OnClick="btn_voltar_Click" />
                </div>
            </div>
            <div class="text_titulo">
                <h1>
                    <asp:Label runat="server" ID="Lb_Nome_pesquisa" /></h1>
            </div>
            <br />
            <div class="controls FundoPainel" style="height: 65px; width: 800px; margin: 0 auto; border: solid 1px #484848;">
                <div style="float: left">
                    <span class="fonteTab">Empresa: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_empresa" /><br />
                    <span class="fonteTab">Aprendiz: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Aprendiz" /><br />
                    <%--                    <span class="fonteTab">Orientador: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Orientador" /><br />--%>
                    <%--                    <span class="fonteTab">Horário de Aprendizagem: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Horario" />--%>
                </div>

                <div style="float: left; margin-left: 20px;">
                    <span class="fonteTab">Data: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Data" /><br />
                    <span class="fonteTab">Responsável: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Responsavel" /><br />
                </div>
            </div>
            <asp:Panel runat="server" ViewStateMode="Enabled" Width="800px" Style="margin: 0 auto;" ID="Pn_Pesquisa">
            </asp:Panel>
        </asp:View>






















    </asp:MultiView>
</asp:Content>
