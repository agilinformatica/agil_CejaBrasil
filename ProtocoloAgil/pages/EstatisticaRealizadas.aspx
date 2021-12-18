<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="EstatisticaRealizadas.aspx.cs" Inherits="ProtocoloAgil.pages.EstatisticaRealizadas" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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


        .hiddencol {
            display: none;
        }

        .foto {
            width: 14%;
            height: 160px;
            margin-left: 1%;
            margin-top: 5px;
        }

        .fotoLabel {
            margin-left: 20%;
            margin-top: -80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Estatísticas > <span>Avaliações Realizadas</span></p>
    </div>
    <div class="controls">
        <div style="float: left;">
            <asp:Button ID="btn_listar" CssClass="btn_controls" runat="server"
                Text="Listar" OnClick="btn_listar_Click" />
            <asp:Button ID="btn_contador" CssClass="btn_controls" runat="server"
                Text="Realizadas por Usuario" OnClick="btn_contador_Click" />

        </div>
        <div style="float: right;">
            <asp:Button ID="btn_print" runat="server" CssClass="btn_novo" Text="Imprimir" OnClick="btn_print_Click" />
        </div>
    </div>


    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="text_titulo">
                <h1>Avaliações de Ensino Aprendizagem Realizadas no Período

                </h1>
            </div>
            <br />
            <table class="FundoPainel centralizar"
                style="width: 98%; border: solid 1px #787878;">
                <tr>
                    <td class="espaco" colspan="7"></td>
                    <td rowspan="3" style="width: 15%;">&nbsp;
                        <asp:Button ID="btn_pesquisa" runat="server" CssClass="btn_novo" OnClick="btnpesquisa_Click"
                            Text="Pesquisar" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">&nbsp;
                    </td>
                    <td class="Tam35 fonteTab">Turma
                   

                    <asp:SqlDataSource ID="SDSturmas" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                        SelectCommand="SELECT TurNome,TurCodigo from CA_Turmas"></asp:SqlDataSource>

                        <asp:DropDownList ID="DDturmas" runat="server" CssClass="fonteTexto" Height="20px"
                            OnDataBound="IndiceZero" Width="150px"  DataSourceID="SDSturmas"
                            DataTextField="TurNome" DataValueField="TurCodigo"
                            ViewStateMode="Enabled">
                        </asp:DropDownList>
                    </td>
                    <td class="Tam10 fonteTab" style="text-align: right;">Data Início: &nbsp;&nbsp;
                    </td>
                    <td class="Tam15 fonteTab">
                        <asp:TextBox ID="TBdataInicio" runat="server" MaxLength="10" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus2" PopupPosition="BottomRight"
                            runat="server" TargetControlID="TBdataInicio">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td class="Tam10 fonteTab" style="text-align: right;">Data Final: &nbsp;&nbsp;
                    </td>
                    <td class="Tam15 fonteTab">
                        <asp:TextBox ID="TBdataTermino" runat="server" MaxLength="10" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus1" PopupPosition="BottomRight"
                            runat="server" TargetControlID="TBdataTermino">
                        </cc2:CalendarExtenderPlus>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" class="espaco">&nbsp;
                    </td>
                </tr>
            </table>
            <div class="centralizar">
                <div class="formatoTela_02">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" DataKeyNames="Apr_Nome,ParUniDescricao" AutoGenerateColumns="False" runat="server" CssClass="list_results_Menor"
                                PageSize="15" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="PepCodigo" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemStyle CssClass="hiddencol" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Aprendiz">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PesNome" HeaderText="Pesquisa">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepMes" HeaderText="Mes">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepAno" HeaderText="Ano">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepDataRealizada" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Realização">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PepTutor" HeaderText="Realizado por">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Pontos">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Notafinal").ToString().Equals("0")  ? 
                                            string.Format("{0:F0}", (float.Parse(DataBinder.Eval(Container.DataItem, "MediaNotas").ToString())))  : DataBinder.Eval(Container.DataItem, "Notafinal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Conceito">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblConceito" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visualizar">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMB_visualizar" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PepCodigo") %>' Height="23px" ImageUrl="~/Images/lupa.png" OnClick="IMB_visualizar_Click" Width="23px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Imprimir" HeaderText="Avaliações" ImageUrl="~/images/cs_print.gif" Text="Button">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle CssClass="List_results_menor" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                    LastPageText="" NextPageText="Próximo"
                                    PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                                <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
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
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="controls">
                <div style="float: right; margin-right: 30px">
                    <asp:Button ID="btn_voltar" runat="server" Text="Voltar" CssClass="btn_novo"
                        OnClick="btn_listar_Click" />
                </div>
            </div>
            <div class="text_titulo">
                <h1>
                    <asp:Label runat="server" ID="Lb_Nome_pesquisa" /></h1>
            </div>
            <br />
            <div class="controls FundoPainel" style="height: 165px; width: 800px; margin: 0 auto; border: solid 1px #484848;">

                <div class="foto">
                    <asp:ImageButton runat="server" ID="IMG_foto_aprendiz" Width="100%" Height="100%"
                        Style="margin: auto; z-index: 1; border: none;" />


                </div>


                <div class="fotoLabel">
                    <span class="fonteTab">Empresa: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_empresa" /><br />
                    <span class="fonteTab">Aprendiz: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Aprendiz" /><br />
                    <%--                    <span class="fonteTab">Orientador: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Orientador" /><br />
                    <span class="fonteTab">Horário de Aprendizagem: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Horario" />--%>

                    <span class="fonteTab">Data: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Data" /><br />
                    <span class="fonteTab">Usuário: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Responsavel" /><br />
                </div>
            </div>
            <asp:Panel runat="server" ViewStateMode="Enabled" Width="800px" Style="margin: 0 auto;" ID="Pn_Pesquisa">
            </asp:Panel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="formatoTela_02">

                <div class="text_titulo">
                    <h1>Pesquisas Realizadas por Usuario</h1>
                </div>
                <br />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" AutoGenerateColumns="False" runat="server" CssClass="list_results"
                            PageSize="15" AllowPaging="True" Style="margin: 0 auto; width: 60%"
                            OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField HeaderText="Usuario" DataField="UsuNome">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Pesquisa" DataField="PesNome" />
                                <asp:BoundField HeaderText="Quantidade" DataField="QTD">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                                LastPageText="" NextPageText="Próximo"
                                PreviousPageText="Anterior" />
                            <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
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
        </asp:View>
        <asp:View ID="View4" runat="server">
            <div class="controls">
                <div style="float: right;">
                    <asp:Button ID="btn_Vontar" runat="server" CssClass="btn_novo" Text="Voltar"
                        OnClick="btn_listar_Click" />
                </div>
            </div>
            <div class="centralizar">
                <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
