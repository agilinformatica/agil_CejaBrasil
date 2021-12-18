<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" EnableEventValidation="false"
    Inherits="ProtocoloAgil.pages.CadastroDisciplina" CodeBehind="CadastroDisciplina.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function setSessionVariable(valueToSetTo) {
            window.__doPostBack('SetSessionVariable', valueToSetTo);
        }

        function change(href) {
            window.location.href = href;
        }

        function popup(url) {
            $("#lightbox").css("display", "inline");


            var x = (screen.width - 600) / 2;
            var y = (screen.height - 450) / 2;
            var newwindow;
            newwindow = window.open(url, "Cadastro", "status=no, scrollbar=1, width=600,height= 450,resizable = 1,top=" + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir esta disciplina?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function Color_Changed(sender) {
            sender.get_element().value = "#" + sender.get_selectedColor();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Pedagógico > <span>Cadastro de Discipinas</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="Novo_Click" />
                <asp:Button ID="relatorio" runat="server" CssClass="btn_controls" Text="Relatório" OnClick="relatorio_Click" />
                <asp:Button ID="texto" runat="server" CssClass="btn_controls" Text="Arquivo de texto" OnClick="texto_Click" />

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
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" PageSize="12" CssClass="list_results" Height="16px"
                            Style="width: 75%; margin: auto" HorizontalAlign="Center"
                            OnDataBound="GridView_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                            OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="DisCodigo" HeaderText="Codigo" SortExpression="DisCodigo">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina" SortExpression="DisDescricao">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DisAbreviatura" HeaderText="Abreviatura" SortExpression="DisAbreviatura">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                    SelectText="Alterar" ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DisCodigo")%>'
                                            OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Materiais">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnArquivo" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DisCodigo")%>'
                                            OnClick="btnArquivo_Click" ImageUrl="~/images/download.jpg" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
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
                        <br />
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
                <div id="lightbox"></div>
                <asp:Panel ID="PNinsere" runat="server" CssClass="centralizar" Height="300px" Width="700px"
                    DefaultButton="BTinsert">
                    <br />
                    <br />
                    <table class="Table FundoPainel">
                        <tr>
                            <td class="cortitulo corfonte titulo" colspan="5" style="font-size: large;">
                                <asp:Label ID="LBtituloAlt" runat="server" Text="Cadastro de Disciplinas"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="espaco" colspan="5">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="Tam20 fonteTab">&nbsp;&nbsp;Código:</td>
                            <td class="codigo" style="text-align: left">
                                <asp:TextBox ID="TBCodigo_curso" runat="server" BorderStyle="Groove" CssClass="fonteTexto" Enabled="false"
                                    BorderWidth="1px" Height="13px" MaxLength="6" Width="90%"></asp:TextBox>
                                &nbsp;
                            </td>

                            <td class="codigo" style="text-align: left">&nbsp;
                            </td>
                            <td class="Tam20" style="text-align: left">&nbsp;&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="Tam20 fonteTab">&nbsp;&nbsp; Abreviatura:</td>
                            <td class="Tam18" style="text-align: left">
                                <asp:TextBox ID="TB_Abreviatura" runat="server" BorderStyle="Groove" CssClass="fonteTexto"
                                    BorderWidth="1px" Height="13px" MaxLength="10" Width="90%"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td class="fonteTab">&nbsp;&nbsp;</td>

                            <td class="codigo" style="text-align: left">&nbsp;</td>
                            <td class="Tam40" style="text-align: left">&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="Tam20 fonteTab">&nbsp;&nbsp; Nome: 
                            </td>
                            <td colspan="4" style="text-align: left;">
                                <asp:TextBox ID="TBNome" runat="server" Height="13px" MaxLength="80" CssClass="fonteTexto"
                                    BorderStyle="Groove" BorderWidth="1px" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>

                            <td class="Tam20 fonteTab">&nbsp;&nbsp;&nbsp; Cor:</td>
                            <td class="Tam18">
                                <asp:TextBox ID="TxtCor" runat="server" Width="170px" BorderStyle="Groove" BorderWidth="1px" CssClass="fonteTexto" MaxLength="7" />
                                <asp:Button ID="btnCor" runat="server" Text="Selecione a cor" OnClick="Button1_Click1" CssClass="btn_add" />


                                <ajaxToolkit:ColorPickerExtender ID="TextBox1_ColorPickerExtender" runat="server"
                                    Enabled="True"
                                    TargetControlID="TxtCor"
                                    SampleControlID="preview"
                                    PopupButtonID="btnCor"
                                    PopupPosition="Right"
                                    OnClientColorSelectionChanged="Color_Changed">
                                </ajaxToolkit:ColorPickerExtender>
                                <div id="preview" style="width: 70px; height: 23px; border: 1px solid #000; margin: 0 3px; float: left">
                                </div>

                                &nbsp;
                            </td>
                            <td class="Tam60" colspan="3" style="text-align: left">&nbsp;&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="Tam20">&nbsp;</td>
                            <td class="Tam18">&nbsp;</td>
                            <td class="Tam60" colspan="3" style="text-align: left">&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="espaco" colspan="5">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="controls" style="text-align: center;">
                        <asp:Button ID="BTinsert" runat="server" OnClick="BTaltera_Click" Text="Salvar" CssClass="btn_novo" />
                        &nbsp;&nbsp;
                        <asp:Button ID="BTLimpar" runat="server" OnClick="BTlimpar_Click" Text="Limpar" CssClass="btn_novo" />
                    </div>
                </asp:Panel>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="centralizar">
                    <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1"></iframe>
                </div>
            </asp:View>





            <asp:View runat="server" ID="View4">
                <div class="formatoTela_02">
                    <asp:UpdatePanel ID="updArquivo" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnEnviar" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="controls">
                                <div class="fonteTab" style="float: right; margin-left: 20px;" visible="false">
                                    &nbsp;&nbsp;
                            <asp:Label runat="server" ID="lblDisciplina" Text="Disciplina:" />&nbsp;&nbsp;
                        
                        
                                </div>
                            </div>
                            <div class="centralizar" style="width: 800px;">
                                <span class="fonteTab" style="float: left;"><b>Importante:</b> São permitdos apenas arquivos que não ultrapassem o limite de 5MB. (Obs. Arquivos de vídeo não são permitidos) </span>
                            </div>
                            <br />
                            <br />
                            <div class="centralizar">
                                <span class="fonteTab">Selecione o arquivo: </span>
                                <asp:TextBox runat="server" ID="tb_Caminho_arquivo" CssClass="fonteTexto" Width="300px" Height="15px"></asp:TextBox>
                                <div class="upload-wrapper">
                                    <asp:FileUpload ID="fupArquivo" SkinID="fup" CssClass="upload-file" runat="server" onchange="javascript:NomeArquivo(this,event);" />
                                    <img class="upload-button" alt="" title="" src="../images/lupa.png" />



                                    <asp:Button ID="btnEnviar" runat="server" CssClass="btn_novo" Style="margin-left: 30px;" Text="Enviar Arquivo" ToolTip="envia o arquivo para turma."
                                        OnClick="btnSend_Click" />
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>



                    <asp:UpdatePanel ID="updArquivoUpload" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <br />
                            <br />
                            <asp:GridView ID="GridView2" runat="server" OnRowCommand="GridView2_RowCommand" EmptyDataText="Não existe arquivos enviados" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-CssClass="fonteTexto" HorizontalAlign="Center" AutoGenerateColumns="False"
                                Width="280px" CssClass="list_results" Style="margin: auto" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Nome">
                                        <HeaderStyle HorizontalAlign="Center" Width="85%" />
                                    </asp:BoundField>

                                     <asp:CommandField ButtonType="Image" HeaderText="Download" SelectText="" ShowSelectButton="True"
                                SelectImageUrl="~/images/download.jpg">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>


                                    <asp:ButtonField ButtonType="Image" CommandName="Deletar" HeaderText="Excluir" ImageUrl="~/images/icon_remove.png"
                                        Text="Button">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
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



        </asp:MultiView>
    </div>
</asp:Content>
