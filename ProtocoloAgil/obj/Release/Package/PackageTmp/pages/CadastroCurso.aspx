<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" EnableEventValidation="false"
    Inherits="ProtocoloAgil.pages.CadastroCurso" CodeBehind="CadastroCurso.aspx.cs" %>

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
            if (confirm("Deseja realmente excluir este curso?") == true)
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
                Pedagógico > <span>Cadastro de Cursos</span></p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="listar_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="Novo_Click" />
                
                <asp:Button ID="relatorio" runat="server" CssClass="btn_controls" Text="Relatório" Visible="false"
                    OnClick="relatorio_Click" />
                     <asp:Button ID="texto" runat="server" CssClass="btn_controls" Text="Arquivo de texto" Visible="false"
                    OnClick="texto_Click" />
               
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
         <br/>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CaptionAlign="Top" CssClass="list_results"
                        Height="16px" Style="width: 75%; margin: auto" HorizontalAlign="Center" ondatabound="GridView_DataBound"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
                        onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="CurCodigo" HeaderText="Codigo" 
                                SortExpression="CurCodigo">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CurDescricao" HeaderText="Curso" 
                                SortExpression="CurDescricao">
                                <HeaderStyle Width="35%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CurCargaHoraria" HeaderText="Carga Horária" 
                                SortExpression="CurCargaHoraria" >
                            <HeaderStyle Width="20%" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                                SelectText="Alterar" ShowSelectButton="True">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMBexcluir"  CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "CurCodigo")%>' 
                                    OnClientClick="javascript:GetConfirm();" onclick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"  runat="server" />
                                </ItemTemplate>
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
                     <asp:HiddenField ID="HFRowCount" runat="server" />
                     <asp:HiddenField runat="server" ID="HFConfirma" />
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
                            <asp:Label ID="LBtituloAlt" runat="server" Text="Cadastro de Cursos"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td class="espaco" colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                         <td class="Tam15 fonteTab">
                             &nbsp;&nbsp;Código:</td>
                        <td class="Tam18" style="text-align: left">
                            <asp:TextBox ID="TBCodigo_curso" runat="server" CssClass="fonteTexto"
                                 Height="13px" MaxLength="6" Width="60%"></asp:TextBox> &nbsp;
                            </td>
                             <td class="Tam25 fonteTab">
                            &nbsp;&nbsp; Carga Horária:</td>
                        <td class="Tam18" style="text-align: left">
                              <asp:TextBox ID="TB_carga_horaria" runat="server" CssClass="fonteTexto" onkeyup="javascript:formataInteiro(this,event);"
                                 Height="13px" MaxLength="6" Width="60%"></asp:TextBox> &nbsp;
                            </td>
                         <td class="Tam40" style="text-align: left">
                            &nbsp;&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="Tam15 fonteTab">
                            &nbsp;&nbsp; Abreviatura:</td>
                        <td class="Tam18" style="text-align: left">
                              <asp:TextBox ID="TB_Abreviatura" runat="server" CssClass="fonteTexto"
                                 Height="13px" MaxLength="12" Width="60%"></asp:TextBox> &nbsp;
                            </td>
                             <td class="fonteTab" >
                            &nbsp;&nbsp; Frequência das Aulas:</td>
                      
                        <td class="codigo"  style="text-align: left">
                            <asp:DropDownList ID="DD_frequencia_aula" runat="server" CssClass="fonteTexto" Height="20px" Width="90%">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="0">Diário</asp:ListItem>
                                <asp:ListItem Value="1">Semanal</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td class="Tam40"  style="text-align: left">
                            &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                     <td class="Tam15 fonteTab">
                            &nbsp;&nbsp; Nome: 
                      </td>
                        <td  colspan="4" style="text-align: left;">
                            <asp:TextBox ID="TBNome" runat="server" Height="13px" MaxLength="80" CssClass="fonteTexto"
                                 Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                   
                          <td class="Tam20">
                              &nbsp; </td>
                            <td class="Tam18">
                                &nbsp;
                            </td>
                            <td class="Tam60" colspan="3" style="text-align: left">
                                &nbsp;&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="espaco" colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <div class="controls" style="text-align: center;">
                    <asp:Button ID="BTinsert" runat="server" OnClick="BTaltera_Click" Text="Confirmar" CssClass="btn_novo" />
                    &nbsp;&nbsp;
                        <asp:Button ID="BTLimpar" runat="server" OnClick="BTlimpar_Click" Text="Limpar" CssClass="btn_novo" />
                </div>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="centralizar">
                <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1">
                </iframe>
            </div>
        </asp:View>
    </asp:MultiView>
        </div>
</asp:Content>
