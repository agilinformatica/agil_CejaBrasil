<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="ControlePesquisa.aspx.cs" Inherits="ProtocoloAgil.pages.ControlePesquisa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/pesquisa.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <script type="text/javascript">
        function change(href) {
            $("#lightbox").css("display", "none");
            window.location.href = href;
        }

        function popup(url, width, height) {
            $("#lightbox").css("display", "inline");
            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "Cadastro Opções", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir esta questão ?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="breadcrumb">
        <p style="text-align: left;">
            Estatísticas > <span> Atribuição de Avaliações </span></p>
    </div>
    <div id="lightbox"></div>
    <div class="controls" >
        <div style="float: left;">
            <asp:Button ID="btn_consulta" runat="server" CssClass="btn_controls" Text="Listar"
                OnClick="btn_consulta_Click" />
            <asp:Button ID="btn_nova_pesquisa" CssClass="btn_controls" runat="server" Text="Nova Pesquisa"
                OnClick="btn_nova_pesquisa_Click" />
            <asp:Button ID="btn_atribuir" CssClass="btn_controls" runat="server" 
                Text="Atribuir Pesquisa" onclick="btn_atribuir_Click" />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="centralizar" style="width: 90%; margin: 0 auto;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" 
                            OnDataBound="GridView_DataBound" onpageindexchanging="GridView1_PageIndexChanging"
                            onselectedindexchanged="GridView1_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField HeaderText="Código" DataField="PesCodigo" />
                                <asp:BoundField HeaderText="Nome" DataField="PesNome" />
                                <asp:BoundField HeaderText="Descrição" DataField="PesDescricao" />
                                <asp:BoundField HeaderText="Numero de Questões" DataField="NumeroQuestoes" />
                                <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                    ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
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
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="formatoTela_02">
                <div id="lightbox">
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:MultiView ID="MultiView2" runat="server">
                            <asp:View ID="View5" runat="server">
                                <div class="centralizar" style="width: 70%; margin: 0 auto;">
                                    <div class="cadastro_pesquisa" style="height: 200px;">
                                        <div class="titulo cortitulo corfonte" style="height: 20px; font-size: large;">
                                            Cadastro de Pesquisa
                                        </div>
                                        <div class="linha_cadastro">
                                            <span class="fonteTab">Nome: </span>
                                            <br />
                                            <asp:TextBox ID="tb_nome_pesquisa" Height="13px" Width="60%" MaxLength="80" CssClass="fonteTexto"
                                                runat="server"></asp:TextBox>
                                        </div>
                                        <div class="linha_cadastro">
                                            <span class="fonteTab">Descrição: </span>
                                            <br />
                                            <asp:TextBox ID="tb_descricao_pesquisa" Height="50px" Width="98%" TextMode="MultiLine"
                                                onkeyup="javascript:IsMaxLength(this, 255);" CssClass="fonteTexto" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="linha_next" style="margin-top: 20px;">
                                            <asp:Button ID="btn_next" runat="server" CssClass="btn_novo" Text="Avançar" OnClick="btn_next_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="View6" runat="server">
                                <div class="centralizar" style="width: 80%; margin: 0 auto;">
                                    <div class="cadastro_pesquisa" style="height: 195px; margin-bottom: 15px;">
                                        <div class="titulo cortitulo corfonte" style="height: 20px; font-size: large;">
                                            Cadastro de Questões
                                        </div>
                                        <div class="linha_cadastro" style="width: 80px; float: left;">
                                            <span class="fonteTab">Número: </span>
                                            <br />
                                            <asp:TextBox ID="tb_numero" Height="13px" Width="50px" MaxLength="5" onkeyup="formataInteiro(this,event);"
                                                CssClass="fonteTexto" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="linha_cadastro" style="width: 250px; float: left;">
                                            <span class="fonteTab">Tipo: </span>
                                            <br />
                                            <asp:DropDownList ID="dd_tipo_questao" CssClass="fonteTexto" Height="18px" Width="200px"
                                                runat="server">
                                                <asp:ListItem Value=""> Selecione</asp:ListItem>
                                                <asp:ListItem Value="M"> Múltipla Escolha</asp:ListItem>
                                                <asp:ListItem Value="A"> Discursiva</asp:ListItem>
                                                <asp:ListItem Value="T"> Mista</asp:ListItem>
                                                <asp:ListItem Value="N"> Nota</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="linha_cadastro" style="clear: both;">
                                            <span class="fonteTab">Questão: </span>
                                            <br />
                                            <asp:TextBox ID="tb_questao" Height="40px" Width="98%" TextMode="MultiLine" onkeyup="javascript:IsMaxLength(this, 255);"
                                                CssClass="fonteTexto" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="linha_next" style="margin-top: 20px;">
                                            <asp:Button ID="btn_next_final" runat="server" CssClass="btn_novo" Text="Nova" 
                                                onclick="btn_next_final_Click1" />
                                            <asp:Button ID="btn_Cadastro_opcao" runat="server" CssClass="btn_novo" Text="Confirmar"
                                                OnClick="btn_Cadastro_opcao_Click" />
                                           
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center"
                                                OnDataBound="GridView_DataBound" 
                                                onselectedindexchanged="GridView2_SelectedIndexChanged" 
                                                onpageindexchanging="GridView2_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Número" DataField="QueOrdemExibicao">
                                                        <HeaderStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Questão" DataField="QueTexto">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                                        ShowSelectButton="True">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="Opções">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IMBopcao" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "QueCodigo")%>'
                                                             Visible='<%# !DataBinder.Eval(Container.DataItem, "QueTipoQustao").Equals("A") && !DataBinder.Eval(Container.DataItem, "QueTipoQustao").Equals("N") %>'
                                                               ImageUrl="~/images/AddButton.jpg" runat="server" Height="17px" Width="18px" onclick="IMBopcao_Click"  />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderStyle Width="10%"></HeaderStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Excluir">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "QueCodigo")%>'
                                                                OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"
                                                                runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderStyle Width="10%"></HeaderStyle>
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
                                           <asp:HiddenField runat="server" ID="HFConfirma" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btn_Cadastro_opcao" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:View>
        <asp:View ID="View3" runat="server">
        <div class="formatoTela_02">
            <div class="centralizar" style="width: 60%; margin: 0 auto;">
                <div class="cadastro_pesquisa" style="height: 150px;">
                    <div class="titulo cortitulo corfonte" style="height: 20px; font-size: large;">
                       Atribuir pesquisa à uma turma
                    </div>
                    <div class="linha_cadastro">
                        <span class="fonteTab">Curso: </span>  &nbsp;
                       
                        <asp:DropDownList ID="DDcursoDiario" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="fonteTexto" 
                        DataTextField="CurDescricao" DataValueField="CurCodigo" Height="20px" OnDataBound="IndiceZero" 
                        OnSelectedIndexChanged="DDcursos_SelectedIndexChanged" Width="80%">
                                    </asp:DropDownList>
                    </div>
                    <div class="linha_cadastro">
                        <span class="fonteTab">Turma: </span> &nbsp;
                       
                         <asp:DropDownList ID="DDturmaDiario" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                       CssClass="fonteTexto" DataTextField="TurNome"
                                        DataValueField="TurCodigo" Height="20px" OnDataBound="IndiceZero" ViewStateMode="Enabled"
                                        Width="45%" OnSelectedIndexChanged="DDturmaDiario_SelectedIndexChanged">
                                    </asp:DropDownList>
                    </div>
                    <div class="linha_next" style="margin: 20px auto 0 auto;text-align:center;">
                        <asp:Button ID="btn_next0" runat="server" CssClass="btn_novo" Text="Atribuir"  OnClientClick="javascript:CreateWheel('yes');"
                            onclick="btn_next0_Click" />
                    </div>
                </div>
            </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
