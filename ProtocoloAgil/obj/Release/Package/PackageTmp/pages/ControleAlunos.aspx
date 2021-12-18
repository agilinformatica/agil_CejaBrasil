<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#" MasterPageFile="~/MPusers.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ControleAlunos.aspx.cs" Inherits="ProtocoloAgil.pages.ControleAlunos" %>

<%@ Register Assembly="CalendarExtenderPlus" Namespace="AjaxControlToolkitPlus" TagPrefix="cc2" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/pesquisa.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/spin.js" type="text/javascript"></script>
    <script type="text/javascript">

        function popup(url, width, height) {
            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "Contrato", "status=no, scrollbar=yes, toolbar=no,menubar=no, width= " + width + ",height=  " + height + ",resizable=yes,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja excluir o Cadastro do Jovem ?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function GetConfirmAlocacao() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir esta alocação do Jovem?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function GetConfirmRegeracao() {
            var hf = document.getElementById("<%# HFConfirmaRegerar.ClientID %>");
            if (confirm("Esta opção irá excluir o cronograma existente?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function GetConfirmCronogramaCapacitacao() {
            var hf = document.getElementById("<%# HFConfirmaCronograma.ClientID %>");
            if (confirm("Esta opção irá gerar um cronograma para esse Jovem, deseja continuar?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function GetConfirmGerarPerfil() {
            var hf = document.getElementById("<%# HFConfirmaCronograma.ClientID %>");
            if (confirm("Esta opção irá gerar um perfil para esse Jovem, deseja continuar?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }

        function ConfirmaUpdate() {
            var hidden = document.getElementById("<%# HFConfirmSave.ClientID %>");
            if (confirm("Deseja realmente salvar este registro?") == true)
                hidden.value = "true";
            else
                hidden.value = "false";
        }


        function CalculaDataTermino(dataref) {
            var hidden = document.getElementById("<%# HFNumeroMeses.ClientID %>");
            if (hidden == null || hidden.value == "") return false;
            var previsao = document.getElementById("<%# TBDataPrev.ClientID %>");

            var dados = dataref.value.split('/');
            var data = new Date(dados[2], eval(dados[1]) - 1, dados[0]);
            data.setMonth(data.getMonth() + eval(hidden.value));
            var pad = "00";
            var dia = pad.substring(0, (pad.length - eval(data.getDate().toString().length))) + data.getDate();

            var mes = pad.substring(0, (pad.length - eval(data.getMonth() + (eval(hidden.value) < 12 ? 1 : 0)).toString().length))


             + (data.getMonth() + (eval(hidden.value) < 12 ? 1 : 0));
            var dataprev = dia + "/" + mes + "/" + data.getFullYear();
            previsao.value = dataprev;
            previsao.value = dataprev;
            return true;
        }

        //function popup(url, width, height) {
        //    $("#lightbox").css("display", "inline");
        //    var x = (screen.width - eval(width)) / 2;
        //    var y = (screen.height - eval(height)) / 2;
        //    var newwindow = window.open(url, "ControleAlunos", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
        //    if (window.focus) { newwindow.focus(); }
        //}

        function popup02(url, width, height) {

            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "ControleAlunos", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

    </script>

    <style>
        .tamanho01 {
            height: 107px;
            margin-bottom: 10px;
            margin-top: 20px;
        }

        .Centro {
            padding: 5px;
        }

        .auto-style6 {
            width: 60%;
        }

        .auto-style8 {
            width: 957px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel4" runat="server" CssClass=" Table centralizar" Height="103px" Width="973px">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Jovens > <span>Cadastro de Jovens</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Lista" OnClick="listar_Click" />
                <% var exibir = Session["tipo"];
                   if ((string)exibir == "Interno")
                   { %>
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo Jovem" OnClick="Button1_Click" />
                <% } %>
            </div>
            <div style="float: right; margin-right: 80px;">
                <asp:Panel runat="server" ID="Pn_aprendiz" Visible="false">
                    <span class="fonteTab">Matrícula: </span>
                    <asp:Label runat="server" CssClass="fonteTexto" ID="lb_codigo_aprendiz" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <span class="fonteTab">Nome: </span>
                    <asp:Label runat="server" CssClass="fonteTexto" ID="lb_nome_aprendiz" />
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
    <div id="lightbox">
    </div>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <div class="formatoTela_02">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnpesquisa">
                    <div class="text_titulo">
                        <h1>Pesquisa de Jovens</h1>
                    </div>
                    <br />

                    <div style="top: 120px; text-align: right; width: 957px;" runat="server" id="divImprimir" visible="false">
                        <asp:Button ID="btnImprimir" runat="server" CssClass="btn_novo" Text="Imprimir Lista de Aprendizes"
                            OnClick="btnImprimir_Click" />
                    </div>

                    <div class="FundoPainel">

                        <table>
                            <tr>
                                <td>
                                    <span class="fonteTab"><strong>Nome:</strong></span>
                                </td>
                                <td><span class="fonteTab"><strong>Situação:</strong></span>
                                </td>
                                <td><span class="fonteTab"><strong>Matricula:</strong></span>
                                </td>
                                <td>
                                    <span class="fonteTab"><strong>CPF:</strong></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" Height="20px" Width="350px"
                                        onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBCodigo').value = '';"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList CssClass="fonteTexto" runat="server"
                                        ID="DDSituacao" DataValueField="StaCodigo" Width="250px" DataTextField="StaDescricao" OnDataBound="IndiceZeroSituacao">
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:TextBox
                                        ID="TBCodigo" runat="server" Height="20px" Width="100px" onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBNome').value= '';"
                                        onkeyup="formataInteiro(this,event);"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCpf" runat="server" MaxLength="11"
                                        Height="16px" Width="100px"
                                        onkeyup="formataInteiro(this,event);"></asp:TextBox>
                                </td>


                                <td>
                                    <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar"
                                        OnClick="btnpesquisa_Click" /></td>
                            </tr>

                            <tr>
                                <td>
                                    <span class="fonteTab"><strong>Estado:</strong></span>
                                </td>
                                <td>
                                    <span class="fonteTab"><strong>Município:</strong></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="DDEstado" OnSelectedIndexChanged="DDEstado_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="fonteTexto" DataSourceID="SqlDataSource1"
                                        DataTextField="MunIEstado" DataValueField="MunIEstado" Height="20px" onkeydown="ModifyEnterKeyPressAsTab();"
                                        Width="80%" OnDataBound="IndiceZeroUF" ViewStateMode="Enabled">
                                    </asp:DropDownList>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                                        SelectCommand="SELECT [MunIEstado] FROM [MA_Municipios]  Group BY [MunIEstado]ORDER BY [MunIEstado] "
                                        OnSelected="SqlDataSource1_Selected"></asp:SqlDataSource>
                                </td>
                                <td>
                                    <asp:DropDownList CssClass="fonteTexto" runat="server"
                                        ID="DDMunicipio" DataValueField="MunIDescricao" Width="250px" DataTextField="MunIDescricao">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                    </div>
                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                OnDataBound="GridView_DataBound" CaptionAlign="Top" CssClass="grid_Aluno" HorizontalAlign="Center"
                                OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                PageSize="15" OnRowCommand="GridView1_RowCommand" DataKeyNames="Apr_AreaAtuacao,Apr_PlanoCurricular" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Apr_Codigo" HeaderText="Código" InsertVisible="False"
                                        ReadOnly="True" SortExpression="Apr_Codigo">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Telefone" HeaderText="Telefone" SortExpression="Apr_Telefone"
                                        NullDisplayText="(__) ____-____">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Sexo" HeaderText="Sexo" SortExpression="Apr_Sexo">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StaDescricao" HeaderText="Situação" SortExpression="StaDescricao">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apr_Email" HeaderText="E-mail" SortExpression="Apr_Email"
                                        NullDisplayText="Não Informado">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Image" HeaderText="Detalhes" SelectImageUrl="~/images/icon_edit.png"
                                        SelectText="Alterar" ShowSelectButton="True">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Alocações">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMB_alocacao" ImageUrl="~/images/detalhes_icone.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo")%>'
                                                runat="server" OnClick="IMB_alocacao_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Capacitações">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnCapacitacao" ImageUrl="~/images/iconeAlterar.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo")%>'
                                                runat="server" OnClick="btnCapacitacao_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Imprimir">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IMB_print" ImageUrl="~/images/cs_print.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo")%>'
                                                runat="server" OnClick="ImageButton1_Click" Style="height: 20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Apr_AreaAtuacao" HeaderText="AreaAtuacao" Visible="False" />
                                    <asp:BoundField DataField="Apr_PlanoCurricular" HeaderText="PlanoCurricular" Visible="False" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="Grid_Aluno" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                            <br />
                            <br />
                            <asp:SqlDataSource ID="Alunos" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                                OnSelected="SqlDataSource1_Selected" SelectCommand="select Apr_Codigo, Apr_Nome, Apr_Telefone, Apr_Sexo = (case when Apr_Sexo = 'M' then 'Masculino' else 'Feminino' end),StaDescricao,
                                            Apr_Email from dbo.CA_Aprendiz left join dbo.CA_SituacaoAprendiz on  StaCodigo =  Apr_Situacao
                                            WHERE ((Apr_Nome LIKE @AluNome + '%')  OR  (Apr_Codigo = @AluMatricula)) ORDER BY Apr_Codigo">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="TBNome" Name="AluNome" PropertyName="Text" DefaultValue="0" />
                                    <asp:ControlParameter ControlID="TBCodigo" DefaultValue="0" Name="AluMatricula" PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:HiddenField runat="server" ID="HFmatricula" />
                            <asp:HiddenField ID="HFRowCount" runat="server" />
                            <asp:HiddenField runat="server" ID="HFConfirma" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="centralizar">
                <iframe id="IFrame1" height="870px" width="98%" style="border: none;" name="IFrame1"
                    src="CadastroAprendiz.aspx"></iframe>
            </div>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="centralizar" style="border: none;">
                <iframe id="IFrame3" height="870px" width="98%" style="border: none;" name="IFrame2"
                    src="CadastroAprendiz.aspx"></iframe>
            </div>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <div class="controls">
                <div style="float: right; margin-right: 30px;">
                    <asp:Button ID="btnvoltarRelat" runat="server" CssClass="btn_novo" Text="Voltar"
                        OnClick="listar_Click" />
                </div>
                <div style="float: right; margin-right: 10px;">
                    <asp:Button ID="Button2" runat="server" CssClass="btn_search" Text="Emitir" OnClick="Button1_Click1" OnClientClick="javascript:CreateWheel('yes');" />
                </div>
                <div style="float: right; margin-right: 10px;">
                    <span class="fonteTab">Relatório:</span>  &nbsp;&nbsp;
                            <asp:DropDownList ID="DDtipo_relatorio" runat="server" AutoPostBack="true" CssClass="fonteTexto"
                                Height="18px" Width="180px" OnSelectedIndexChanged="DDtipo_relatorio_SelectedIndexChanged">
                                <asp:ListItem Value=""> Selecione</asp:ListItem>
                                <asp:ListItem Value="3"> Ficha Cadastral</asp:ListItem>
                                <asp:ListItem Value="4"> Ficha Funcional</asp:ListItem>
                                <asp:ListItem Value="8"> Contrato Integral</asp:ListItem>
                                <asp:ListItem Value="9"> Contrato Parcial</asp:ListItem>
                                <asp:ListItem Value="10"> Declaração de Matrícula</asp:ListItem>
                               
                            </asp:DropDownList>


                    <asp:Label ID="lb_numero" runat="server" Visible="false" class="fonteTab">Informe o número de uniformes recebidos:</asp:Label>
                    &nbsp;&nbsp;
                                 <asp:TextBox ID="tb_numero" runat="server" MaxLength="2" Visible="false"
                                     CssClass="fonteTexto" Height="13px" onkeyup="formataInteiro(this,event);" Width="25px"></asp:TextBox>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="centralizar formatoTela_02" style="border: none;">
                        <iframe runat="server" id="IFrame2" src="Visualizador.aspx" visible="false" class="VisualFrame"
                            width="900px" style="border: none;"></iframe>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DDtipo_relatorio" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View runat="server" ID="View6">
            <div class="formatoTela_02">

                <% var exibir = Session["tipo"];
                   if ((string)exibir == "Interno")
                   { %>
                <div class="controls">
                    <div style="float: right;">
                        <asp:Button runat="server" ID="btn_nova_alocacao" CssClass="btn_controls" Text="Nova Alocação"
                            OnClick="btn_nova_alocacao_Click" />
                    </div>
                </div>
                <% } %>

                <asp:Panel ID="pn_info" runat="server" CssClass="centralizar" Height="200px" Visible="false"
                    Width="500px">
                    <div class="text_titulo" style="margin-top: 60px;">
                        <h1>Não existem alocações cadastradas para o aprendiz.</h1>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" CssClass="centralizar" ID="pn_alocacao" Width="98%">
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" Style="margin: 0 auto; width: 98%;"
                        AutoGenerateColumns="False" CssClass="list_results" OnDataBound="GridView_DataBound"
                        OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="TurCodigo,CurCodigo,TurNumeroMeses" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="TurNome" HeaderText="Turma" />
                            <asp:BoundField DataField="ALADataInicio" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Início" />
                            <asp:BoundField DataField="ALADataPrevTermino" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="Prev Término" />
                            <asp:BoundField DataField="ALADataTermino" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Término" />
                            <asp:BoundField DataField="ParNomeFantasia" HeaderText="Empresa" />
                            <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                            <asp:BoundField DataField="StaDescricao" HeaderText="Situação do Aluno" />
                            <asp:BoundField DataField="Situacao" HeaderText="Status" />
                            <asp:TemplateField HeaderText="Detalhes">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMB_alterar" ImageUrl="~/images/icon_edit.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ALAOrdem") %>'
                                        runat="server" OnClick="IMB_alterar_Click" Style="height: 16px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gerar Cronograma" ShowHeader="False" Visible="False">
                                <ItemTemplate>
                                    <asp:Button ID="btGerarCronograma" runat="server" OnClick="Button3_Click" Text="Gerar" />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Listar Cronograma">
                                <ItemTemplate>
                                    <asp:Button ID="btListarCronograma" runat="server" OnClick="btListarCronograma_Click" Text="Listar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TurCodigo" HeaderText="TurCodigo" Visible="False" />
                            <asp:BoundField DataField="CurCodigo" HeaderText="CurCodigo" Visible="False" />
                            <asp:BoundField DataField="TurNumeroMeses" HeaderText="TurNumeroMeses" Visible="False" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg"
                            LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                        <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                    <asp:Panel ID="pGerarCronograma" Visible="false" runat="server">
                        <table width="100%" runat="server" id="tabela1">
                            <tr>
                                <td class="Tam08 fonteTab">
                                    Disciplina Inicial</td>
                                <td class="Tam08 fonteTab">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="fonteTab">
                                    <asp:DropDownList ID="DD_Plano_Curricular" runat="server" CssClass="fonteTexto" DataTextField="DisDescricao" DataValueField="DisCodigo" Height="16px" Width="209px">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <%--<tr>
                                <td class="auto-style8">
                                    <asp:TextBox ID="txtDataCronograma" Visible="false" runat="server" CssClass="fonteTexto centralizar" Height="18px"
                                        MaxLength="10"
                                        onkeyup="formataData(this,event);" Width="10%"></asp:TextBox>
                                    <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server"
                                        Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataCronograma">
                                    </cc2:CalendarExtenderPlus>
                                </td>
                                <td class="fonteTab">&nbsp;</td>
                            </tr>--%>

                        </table>
                        <table width="100%" runat="server" id="tabela2">
                            <tr>
                                <td colspan="5">
                                    <asp:Button ID="btCadastrarCronograma" Visible="false" runat="server" CssClass="btn_novo" OnClick="btCadastrarCronograma_Click" OnClientClick="javascript:CreateWheel('yes');" Text="Gerar Introdutório" Width="123px" />
                                    <asp:Button ID="btnGerarOutras"  runat="server" CssClass="btn_novo" OnClick="btnGerarOutras_Click" OnClientClick="javascript:CreateWheel('yes');" Text="Gerar Simultaneidade" Width="160px" />
                                    <asp:Button ID="btnGerarCronograma" Visible="false" runat="server" CssClass="btn_novo" OnClick="btnGerarCronograma_Click" OnClientClick="javascript:CreateWheel('yes');" Text="Gerar Cronograma" Width="160px" />
                                    <asp:Button ID="btnRegerarCronograma" Visible="false" runat="server" CssClass="btn_novo" BackColor="Red" BorderColor="Red" OnClick="btnRegerarCronograma_Click" OnClientClick="javascript:GetConfirmaCronograma();" Text="REGERAR cronograma" Width="160px" />
                                    <asp:Button ID="btnGerarCronogramaMudancaTurma" Visible="false" runat="server" CssClass="btn_novo" OnClick="btnGerarCronogramaMudancaTurma_Click" OnClientClick="javascript:CreateWheel('yes');" Text="Gerar Cronograma Mudança de Turma" Width="240px" />
                                    <asp:Button ID="btnGerarCronogramaIntensivo" Visible="false" runat="server" CssClass="btn_novo" OnClick="btnGerarCronogramaIntensivo_Click" OnClientClick="javascript:CreateWheel('yes');" Text="Gerar Cronograma Intensivo" Width="180px" />
                                    <asp:Button ID="btnSegundoEncontro" Visible="false" runat="server" CssClass="btn_novo" OnClick="btnSegundoEncontro_Click" OnClientClick="javascript:CreateWheel('yes');" Text="Gerar Segundo Encontro" Width="160px" />
                                    <asp:Button ID="btnCalendarioAprendiz" runat="server" CssClass="btn_novo" OnClick="btnCalendarioAprendiz_Click" Text="Calendário Aprendiz" Width="190px" />
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField runat="server" ID="HFConfirmaRegerar" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </asp:View>
        <asp:View ID="View7" runat="server">
            <asp:Panel ID="Panel5" runat="server" CssClass="centralizar" Width="90%">
                <div class="cadastro_pesquisa tamanho01">
                    <div class="titulo cortitulo corfonte" style="font-size: large;">
                        Alocação de Aprendizes
                    </div>
                    <div class="linha_cadastro">
                        <span class="fonteTab">Matrícula:</span> &nbsp;
                        <asp:Label runat="server" CssClass="fonteTexto" ID="LB_matricula" />
                    </div>
                    <div class="linha_cadastro">
                        <span class="fonteTab">Aprendiz:</span> &nbsp;
                        <asp:Label runat="server" CssClass="fonteTexto" ID="lb_aprendiz" />
                    </div>
                </div>
                <table class="Table FundoPainel centralizar">
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam40 fonteTab" colspan="2">Curso:
                        </td>
                        <td class="Tam20 fonteTab ">Turma :
                        </td>
                        <td class=" Tam20 fonteTab">Tipo de Pagamento
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam25 fonteTab" colspan="2">
                            <asp:DropDownList ID="DDcurso" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                CssClass="fonteTexto" DataTextField="CurDescricao" DataValueField="CurCodigo"
                                Height="18px" OnDataBound="IndiceZero" OnSelectedIndexChanged="DDcursos_SelectedIndexChanged"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td class="Tam10 fonteTab ">
                            <asp:DropDownList ID="DD_Turma" runat="server" AutoPostBack="true" CssClass="fonteTexto"
                                DataTextField="TurNome" DataValueField="TurCodigo" Height="18px" OnDataBound="IndiceZero"
                                OnSelectedIndexChanged="DD_Turma_SelectedIndexChanged" Width="85%">
                            </asp:DropDownList>
                        </td>
                        <td class="fonteTab">
                            <asp:DropDownList ID="DD_tipo_pagamento" runat="server" CssClass="fonteTexto" Height="18px"
                                Width="85%">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="E">Sem Vinculo</asp:ListItem>
                                <asp:ListItem Value="C">Com Vinculo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam15 fonteTab">Parceiro:
                        </td>
                        <td class="Tam35 fonteTab"></td>
                        <td class="fonteTab" colspan="2">Unidade:
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05"></td>
                        <td class="Tam25 fonteTab" colspan="2">
                            <asp:DropDownList ID="DD_parceiro" runat="server" AutoPostBack="true" CssClass="fonteTexto"
                                DataTextField="a" DataValueField="b" Height="18px" OnDataBound="IndiceZero"
                                OnSelectedIndexChanged="DD_parceiro_SelectedIndexChanged" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td class="fonteTab" colspan="2">
                            <asp:DropDownList ID="DDunidade_parceiro" runat="server" CssClass="fonteTexto" DataTextField="ParUniDescricao"
                                DataValueField="ParUniCodigo" Height="18px" OnDataBound="IndiceZero" AutoPostBack="true"
                                OnSelectedIndexChanged="DDunidade_parceiro_SelectedIndexChanged" Width="92%">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;</td>
                        <td class="Tam25 fonteTab" colspan="2">Endereço da Unidade</td>
                        <td class="fonteTab" colspan="2">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;</td>
                        <td class="Tam25 fonteTab" colspan="4">
                            <asp:Label ID="lb_endereco" runat="server" BackColor="White" Width="95%"
                                Height="40px" BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab" colspan="2">Acompanhamento na Empresa
                        </td>

                        <td class="fonteTab" colspan="2">Área de Atuação
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab" colspan="2">
                            <asp:DropDownList ID="DDusuario" runat="server" AutoPostBack="true" CssClass="fonteTexto"
                                DataTextField="UsuNome" DataValueField="UsuCodigo" Height="18px" OnDataBound="IndiceZero"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td class="fonteTab" colspan="2">
                            <asp:DropDownList ID="DD_area_atuacao" runat="server" CssClass="fonteTexto" DataSourceID="SDS_area_atuacao"
                                DataTextField="AreaDescricao" DataValueField="AreaCodigo" Height="18px" OnDataBound="IndiceZero"
                                Width="80%">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab">Supervisor
                        </td>
                        <td class="Tam25 fonteTab">&nbsp;
                        </td>
                        <td class="Tam12 fonteTab">Início Expediente:
                        </td>
                        <td class="Tam15 fonteTab">Término Expediente:
                        </td>

                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab" colspan="2">
                            <asp:DropDownList ID="DD_orientador" runat="server" AutoPostBack="true" CssClass="fonteTexto"
                                DataTextField="OriNome" DataValueField="OriCodigo" Height="18px" OnDataBound="IndiceZero"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td class="Tam10 fonteTab ">
                            <MKB:TimeSelector ID="TMS_inicio" runat="server" CssClass="fonteTexto" DisplaySeconds="False"
                                Height="18px" MinuteIncrement="10" SelectedTimeFormat="TwentyFour">
                            </MKB:TimeSelector>
                        </td>
                        <td class="fonteTab">
                            <MKB:TimeSelector ID="TMS_final" runat="server" CssClass="fonteTexto" DisplaySeconds="False"
                                Height="18px" MinuteIncrement="10" SelectedTimeFormat="TwentyFour">
                            </MKB:TimeSelector>
                        </td>

                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam15 fonteTab">Data Início:
                        </td>
                        <td class="Tam15 fonteTab">Previsão Término:
                        </td>
                        <td class="Tam15 fonteTab">Data Término:
                        </td>
                        <td class="fonteTab Tam15">Status:
                        </td>

                        <td class="Tam05 fonteTab"></td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam15 fonteTab">
                            <asp:TextBox ID="TBdataInicio" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="10"
                                onkeyup="formataData(this,event);" Width="70%" AutoPostBack="True" OnTextChanged="TBdataInicio_TextChanged"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="TBCalendario_CalendarExtenderPlus0" runat="server"
                                Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="TBdataInicio">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam15 fonteTab">
                            <asp:TextBox ID="TBDataPrev" runat="server" CssClass="fonteTexto" Height="18px" MaxLength="10"
                                onblur="javascript:if( this.value !=''  &amp;&amp;   !VerificaData(this.value)) this.value ='';"
                                onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus6" runat="server" Format="dd/MM/yyyy"
                                PopupPosition="BottomRight" TargetControlID="TBDataPrev">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam15 fonteTab">
                            <asp:TextBox ID="TBdataTermino" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="10" onblur="javascript:if( this.value !=''  &amp;&amp;   !VerificaData(this.value)) this.value ='';"
                                onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus7" runat="server" Format="dd/MM/yyyy"
                                PopupPosition="BottomRight" TargetControlID="TBdataTermino">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="fonteTab">
                            <asp:DropDownList ID="DD_SituacaoAlocacao" runat="server" CssClass="fonteTexto" Height="18px"
                                Width="85%">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="A">Ativo</asp:ListItem>
                                <asp:ListItem Value="I">Inativo</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="Tam05 fonteTab"></td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="fonteTab Tam10">Salário:
                        </td>
                        <td class="fonteTab Tam10">Contribuição:
                        </td>
                        <td class="fonteTab Tam10">Valor Outros:
                        </td>



                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="fonteTab">
                            <asp:TextBox ID="TBValorBolsa" runat="server" CssClass="fonteTexto" Height="18px"
                                onkeyup="formataValor(this,event);" Width="110px"></asp:TextBox>
                        </td>
                        <td class="fonteTab">
                            <asp:TextBox ID="TBValorTaxa" runat="server" CssClass="fonteTexto" Height="18px"
                                onkeyup="formataValor(this,event);" Width="110px"></asp:TextBox>
                        </td>
                        <td class="fonteTab">
                            <asp:TextBox ID="TBValorEncargos" runat="server" CssClass="fonteTexto" Height="18px"
                                onkeyup="formataValor(this,event);" Width="110px"></asp:TextBox>


                        </td>

                    </tr>
                    <tr> 
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="fonteTab Tam10">Mês/Ano uniforme 1
                        </td>
                         <td class="fonteTab Tam10">Mês/Ano uniforme 2
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="fonteTab">
                            <asp:DropDownList ID="DDMesUniforme1" runat="server" CssClass="fonteTexto" Height="18px"
                                Width="80px">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                                <asp:ListItem Value="3">Março</asp:ListItem>
                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                <asp:ListItem Value="5">Maio</asp:ListItem>
                                <asp:ListItem Value="6">Junho</asp:ListItem>
                                <asp:ListItem Value="7">Julho</asp:ListItem>
                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                <asp:ListItem Value="9">Setembro</asp:ListItem>
                                <asp:ListItem Value="10">Outubro</asp:ListItem>
                                <asp:ListItem Value="11">Novembro</asp:ListItem>
                                <asp:ListItem Value="12">Dezembro</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtMesAnoUniforme1" runat="server" CssClass="fonteTexto" MaxLength="4" Height="18px"
                                onkeyup="formataInteiro(this,value);" Width="40px"></asp:TextBox>
                        </td>

                         <td class="fonteTab">
                            <asp:DropDownList ID="DDMesUniforme2" runat="server" CssClass="fonteTexto" Height="18px"
                                Width="80px">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                                <asp:ListItem Value="3">Março</asp:ListItem>
                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                <asp:ListItem Value="5">Maio</asp:ListItem>
                                <asp:ListItem Value="6">Junho</asp:ListItem>
                                <asp:ListItem Value="7">Julho</asp:ListItem>
                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                <asp:ListItem Value="9">Setembro</asp:ListItem>
                                <asp:ListItem Value="10">Outubro</asp:ListItem>
                                <asp:ListItem Value="11">Novembro</asp:ListItem>
                                <asp:ListItem Value="12">Dezembro</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtMesAnoUniforme2" runat="server" CssClass="fonteTexto" MaxLength="4" Height="18px"
                                onkeyup="formataInteiro(this,value);" Width="40px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam10 fonteTab">Observações:
                        </td>
                        <td class="Tam60 fonteTab" colspan="3">&nbsp;
                        </td>
                        <td class="Tam05">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="fonteTab" colspan="4">
                            <asp:TextBox ID="TBObservacao" runat="server" CssClass="fonteTexto" Height="40px"
                                onkeyup="javascript:IsMaxLength(this,255);" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </td>
                        <td class="Tam05">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco" colspan="6">&nbsp;
                        </td>
                    </tr>
                </table>
                <table class="Table FundoPainel centralizar">
                    <tr>
                        <td class="titulo cortitulo corfonte" colspan="6" style="font-size: large;">Situação do Aprendiz
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab">Situação:
                        </td>
                        <td class="Tam30 fonteTab">&nbsp;
                        </td>
                        <td class="Tam40 fonteTab">Motivo de desligamento:
                        </td>
                        <td class="Tam05"></td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;</td>
                        <td class=" Tam10 fonteTab" colspan="2">
                            <asp:DropDownList ID="DD_situacao" runat="server" CssClass="fonteTexto" DataTextField="StaDescricao"
                                DataValueField="StaCodigo" Height="18px" OnDataBound="IndiceZero" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td class="Tam10 fonteTab">
                            <asp:DropDownList ID="DD_motivo_desligamento" runat="server" CssClass="fonteTexto"
                                DataTextField="MotDescricao" DataValueField="MotCodigo" Height="18px" OnDataBound="IndiceZero"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;</td>
                        <td class="Tam30 fonteTab">Início Aprendizagem:
                        </td>
                        <td class="Tam30 fonteTab">Prev. Fim Aprendizagem:
                        </td>
                        <td class="Tam15 fonteTab">Data Desligamento:
                        </td>
                        <td class="Tam30 fonteTab">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab"></td>
                    </tr>
                    <tr>
                        <td class=" Tam05">&nbsp;</td>
                        <td class="Tam15 fonteTab">
                            <asp:TextBox ID="TB_inicio_aprendizagem" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="10" onblur="javascript:if( this.value !=''  &amp;&amp;   !VerificaData(this.value)) this.value ='';"
                                onkeyup="formataData(this,event);" Width="70%" AutoPostBack="True" OnTextChanged="TB_inicio_aprendizagem_TextChanged"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus3" runat="server" Format="dd/MM/yyyy"
                                PopupPosition="BottomRight" TargetControlID="TB_inicio_aprendizagem">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam15 fonteTab">
                            <asp:TextBox ID="TB_prev_Fim_aprendizagem" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="10" onblur="javascript:if( this.value !=''  &amp;&amp;   !VerificaData(this.value)) this.value ='';"
                                onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus1" runat="server" Format="dd/MM/yyyy"
                                PopupPosition="BottomRight" TargetControlID="TB_prev_Fim_aprendizagem">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam15 fonteTab">
                            <asp:TextBox ID="TB_data_desligamento" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="10" onblur="javascript:if( this.value !=''  &amp;&amp;   !VerificaData(this.value)) this.value ='';"
                                onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus8" runat="server" Format="dd/MM/yyyy"
                                PopupPosition="BottomRight" TargetControlID="TB_data_desligamento">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="fonteTab">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab"></td>
                    </tr>
                    <tr>
                        <td class="espaco" colspan="6">&nbsp;
                        </td>
                    </tr>
                </table>
                <table class="Table FundoPainel centralizar" id="Table1" runat="server">
                    <tr>
                        <td class="titulo cortitulo corfonte" colspan="6" style="font-size: large;">Turma Anterior
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab">Turma:
                        </td>
                        <td class="Tam12 fonteTab">Data Mundança:
                        </td>
                        <td class="Tam40 fonteTab">Mudado por:
                        </td>
                        <td class="Tam05"></td>
                    </tr>
                    <tr>
                        <td class="Tam05"></td>
                        <td class=" fonteTab">
                            <asp:DropDownList ID="TBTurmaAnterior" runat="server" CssClass="fonteTexto" AutoPostBack="true"
                                DataTextField="TurNome" DataValueField="TurCodigo" Height="16px" OnDataBound="IndiceZero"
                                Width="85%" Enabled="false">
                            </asp:DropDownList>
                            <%--<asp:TextBox runat="server" ID="TBTurmaAnterior" CssClass="fonteTexto" Height="13px" Width="70%"  Enabled="false"></asp:TextBox>--%>
                        </td>
                        <td class="Tam10 fonteTab">&nbsp;
                            <asp:TextBox runat="server" ID="TBDataTurmaAnterior" CssClass="fonteTexto" Height="13px" Width="70%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="Tam10 fonteTab">
                            <asp:TextBox ID="TBMudadoPor" runat="server" CssClass="fonteTexto" Height="13px" Width="70%" MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab" colspan="2">Motivo da Mudança:
                        </td>
                        <td class="Tam40"></td>
                        <td class="Tam05"></td>
                    </tr>
                    <tr>
                        <td class="Tam05"></td>
                        <td class=" fonteTab" colspan="3">
                            <asp:TextBox runat="server" ID="TBMotivoMudanca" CssClass="fonteTexto" Height="13px" Width="87%" Enabled="false"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                </table>

                <table class="Table FundoPainel centralizar" id="TableMudaTurma" runat="server" visible="false">
                    <tr>
                        <td class="titulo cortitulo corfonte" colspan="6" style="font-size: large;">Mudança de Turma
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam08 fonteTab">Nova Turma:
                        </td>
                        <td class="Tam12 fonteTab">Data Mundança:
                        </td>
                        <td class="Tam40 fonteTab">Motivo de Mudança:
                        </td>
                        <td class="Tam05"></td>
                    </tr>
                    <tr>
                        <td class="Tam05"></td>
                        <td class=" fonteTab">
                            <asp:DropDownList ID="DDNovaTurma" runat="server" CssClass="fonteTexto" AutoPostBack="true"
                                DataTextField="TurNome" DataValueField="TurCodigo" Height="16px" OnDataBound="IndiceZero"
                                Width="85%">
                            </asp:DropDownList>
                        </td>
                        <td class="Tam10 fonteTab">&nbsp;
                            <asp:TextBox ID="TbInicioNovaTurma" runat="server" MaxLength="10" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                CssClass="fonteTexto" Height="13px" onkeyup="formataData(this,event);" Width="70%"></asp:TextBox>
                            <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus4"
                                PopupPosition="BottomRight" runat="server" TargetControlID="TbInicioNovaTurma">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="Tam10 fonteTab">
                            <asp:TextBox ID="TbMotivoMudancaTurma" runat="server" CssClass="fonteTexto" Height="13px" Width="80%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                </table>

                <br />

                <% var exibir = Session["tipo"];
                   if ((string)exibir == "Interno")
                   { %>
                <div class="controls">
                    <div class="centralizar">
                        <asp:Button ID="btn_save" runat="server" CssClass="btn_novo" OnClick="btn_save_Click"
                            OnClientClick="javascript:ConfirmaUpdate();" Text="Confirmar" />
                        &nbsp;
                        <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar"
                            OnClick="btn_voltar_Click" />
                    </div>
                </div>
                <% } %>
                <br />
                <br />
            </asp:Panel>
            <asp:SqlDataSource ID="SDS_area_atuacao" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                SelectCommand="SELECT * FROM [CA_AreaAtuacao]"></asp:SqlDataSource>
            <asp:HiddenField ID="HFConfirmSave" runat="server" />
            <asp:HiddenField ID="HFNumeroMeses" runat="server" />
            <br />
        </asp:View>
        <!--andre adicionando novas views -->
        <asp:View ID="View8" runat="server">
            <div class="centralizar" style="border: none;">

                <div class="cadastro_pesquisa tamanho01">
                    <div class="titulo cortitulo corfonte" style="font-size: large;">
                        Gerar cronograma
                    </div>
                    <div class="linha_cadastro">
                    </div>
                </div>

            </div>
        </asp:View>
        <br />
        <br />
        <asp:View ID="View9" runat="server">
            <div class="centralizar" style="border: none;">

                <div class="titulo cortitulo corfonte" style="font-size: large;">
                    Gerar cronograma
                </div>
                <table align="center" class="auto-style6">
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <div class="linha_cadastro">
                                <span class="fonteTab">Aprendiz:</span> &nbsp;
                                <asp:Label ID="LB_APR_CRONO" runat="server" CssClass="fonteTexto" />
                                &nbsp;<span class="fonteTab">Turma:</span>
                                <asp:Label ID="LB_Turma" runat="server" CssClass="fonteTexto" />
                                <%--&nbsp;<span  class="fonteTab">Educador:--%>
                                <asp:Label ID="LB_Educador" Visible="false" runat="server" CssClass="fonteTexto" />
                                <%--</span>--%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="linha_cadastro">
                                <span class="fonteTab">Início:</span> &nbsp;
                                <asp:Label ID="LB_APR_Hinicio" runat="server" CssClass="fonteTexto" />
                                &nbsp;<span class="fonteTab">Término:</span> &nbsp;<asp:Label ID="LB_APR_HTermino" runat="server" CssClass="fonteTexto" />
                                &nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvListarCronograma" runat="server" Width="100%" AllowPaging="True" CssClass="list_results" OnPageIndexChanging="gvListarCronograma_PageIndexChanging" PageSize="15" AutoGenerateColumns="False" DataKeyNames="Nome,Inicio,Termino,Turma,Educador" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Data" HeaderText="Data" />
                                    <asp:BoundField DataField="OrdemAula" HeaderText="Sequência" />
                                    <asp:BoundField DataField="Disciplina" HeaderText="Disciplina">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Educador" HeaderText="Educador" />
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" Visible="False" />
                                    <asp:BoundField DataField="Inicio" HeaderText="Inicio" Visible="False" />
                                    <asp:BoundField DataField="Termino" HeaderText="Termino" Visible="False" />
                                    <asp:BoundField DataField="Turma" HeaderText="Turma" Visible="False" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:Button ID="btVoltarListaCrono" runat="server" CssClass="btn_novo" OnClick="btVoltarListaCrono_Click" Text="Voltar" Width="86px" />
                            <br />
                        </td>
                    </tr>
                </table>
                <br />

            </div>
        </asp:View>
        <asp:View ID="View10" runat="server">
            <div class="centralizar">
                <iframe id="IFrame4" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
            </div>
        </asp:View>
        <asp:View ID="ViewCapacitacoes" runat="server">

            <div class="controls">
                <div style="float: right;">
                    <asp:Button ID="btnVoltar" runat="server" CssClass="btn_novo" OnClick="btnVoltar_Click" Text="Voltar" Width="86px" />
                </div>
            </div>
            <table class="Table FundoPainel centralizar" id="Table2" runat="server">



                <tr>
                    <td class="titulo cortitulo corfonte" colspan="6" style="font-size: large;">Capacitações
                    </td>
                </tr>
                <tr>

                    <td class="fonteTab Tam10">Turma:
                    </td>
                    <td class="fonteTab">Unidade
                    </td>
                    <td class="fonteTab">Status:
                    </td>
                    <td class="fonteTab">Data Início:
                    </td>
                    <td class="fonteTab">Data Prev. Término:
                    </td>
                    <td class="fonteTab">Data Término:
                    </td>

                </tr>
                <tr>
                    <td class="fonteTab">
                        <asp:DropDownList ID="DDTurmaCapacitacao" runat="server" CssClass="fonteTexto"
                            DataTextField="TurNome" DataValueField="TurCodigo" Height="16px"
                            Width="85%">
                        </asp:DropDownList>

                    </td>
                    <td class="fonteTab">
                        <asp:DropDownList ID="DDUnidadeCapacitacao" runat="server" CssClass="fonteTexto"
                            DataTextField="UniNome" DataValueField="UniCodigo" Height="16px"
                            Width="85%">
                        </asp:DropDownList>
                        <%--<asp:TextBox runat="server" ID="TBTurmaAnterior" CssClass="fonteTexto" Height="13px" Width="70%"  Enabled="false"></asp:TextBox>--%>
                    </td>
                    <td class="fonteTab">
                        <asp:DropDownList ID="DDStatusCapacitacao" runat="server" CssClass="fonteTexto"
                            Height="16px"
                            Width="85%">
                            <asp:ListItem Value="A"> Ativo</asp:ListItem>
                            <asp:ListItem Value="I"> Inativo</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox runat="server" ID="TBTurmaAnterior" CssClass="fonteTexto" Height="13px" Width="70%"  Enabled="false"></asp:TextBox>--%>
                    </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="txtDataInicioCapacitacao" runat="server" CssClass="fonteTexto" Height="18px"
                            MaxLength="10"
                            onkeyup="formataData(this,event);" Width="60%"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus5" runat="server"
                            Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataInicioCapacitacao">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="txtDataPrevisaoCapacitacao" runat="server" CssClass="fonteTexto" Height="18px"
                            MaxLength="10"
                            onkeyup="formataData(this,event);" Width="60%"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus10" runat="server"
                            Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataPrevisaoCapacitacao">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="txtTerminoCapacitacao" runat="server" CssClass="fonteTexto" Height="18px"
                            MaxLength="10"
                            onkeyup="formataData(this,event);" Width="60%"></asp:TextBox>
                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus9" runat="server"
                            Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtTerminoCapacitacao">
                        </cc2:CalendarExtenderPlus>
                    </td>

                </tr>
                <tr>

                    <td class="fonteTab" colspan="6">Observações:
                    </td>

                </tr>
                <tr>

                    <td class="fonteTab" colspan="6">
                        <asp:TextBox runat="server" ID="txtObservacoesCapacitacao" TextMode="MultiLine" Rows="3" CssClass="fonteTexto" Height="43px" Width="87%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Button ID="btnSalvarCapacitacao" runat="server" CssClass="btn_novo" Text="Salvar"
                            OnClick="btnSalvarCapacitacao_Click" />
                        <asp:Button ID="btnLimparCapacitacoes" runat="server" CssClass="btn_novo" Text="Limpar"
                            OnClick="btnLimparCapacitacoes_Click" />
                    </td>
                </tr>
            </table>

        </asp:View>

        <asp:View ID="ViewGridCapacitacoes" runat="server">
            <div class="controls">
                <div style="float: right;">
                    <asp:Button ID="btnNovaCapacitacao" runat="server" CssClass="btn_novo" Text="Nova Capacitação"
                        OnClick="btnNovaCapacitacao_Click" />
                </div>
            </div>
            <asp:GridView ID="gridCapacitacoes" runat="server" Width="98%" AllowPaging="True" CssClass="list_results" PageSize="15" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="CapAprendiz" HeaderText="Cod Aprendiz" />
                    <asp:BoundField DataField="UniNome" HeaderText="Unidade" />
                    <asp:BoundField DataField="TurNome" HeaderText="Turma" />
                    <asp:BoundField DataField="CapStatus" HeaderText="Status" />
                    <asp:BoundField DataField="CapDataInicio" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Início" />
                    <asp:BoundField DataField="CapDataPrevTermino" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Prev. Término" />
                    <asp:BoundField DataField="CapDataTermino" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Término" />
                    <asp:TemplateField HeaderText="Alterar">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAlterarCapacitacao" ImageUrl="~/images/icon_edit.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CapAprendiz")%>'
                                runat="server" OnClick="btnAlterarCapacitacao_Click" Style="height: 16px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gerar Cronograma">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnGeraCronogramaCapacitacao" ImageUrl="~/images/detalhes_icone.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CapAprendiz")%>'
                                runat="server" OnClick="btnGeraCronogramaCapacitacao_Click" OnClientClick="javascript: GetConfirmCronogramaCapacitacao(); CreateWheel('yes');" Style="height: 16px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gerar Perfil">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnGerarPerfil" ImageUrl="~/images/icon_reset.jpg" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CapAprendiz")%>'
                                runat="server" OnClick="btnGerarPerfil_Click" OnClientClick="javascript: GetConfirmGerarPerfil(); CreateWheel('yes');" Style="height: 16px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Atualizar Perfil">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAtualizaPerfil" ImageUrl="~/images/icon_edit.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CapAprendiz")%>'
                                runat="server" OnClick="btnAtualizaPerfil_Click" Style="height: 16px" />
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:HiddenField runat="server" ID="HFConfirmaCronograma" />
        </asp:View>

        <asp:View ID="ViewAtualizarPerfil" runat="server">


            <asp:Panel ID="Panel2" runat="server" CssClass="centralizar" Width="90%">

                <div class="titulo cortitulo corfonte" style="font-size: large;">
                    Atualizar Perfil
                </div>

                <table class="Table FundoPainel centralizar">
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">Geral:
                        </td>
                        <td class="Tam05 fonteTab ">Português:
                        </td>
                        <td class=" Tam05 fonteTab">Matemática:
                        </td>
                        <td class=" Tam05 fonteTab">Técnicas ADM:
                        </td>
                        <td class=" Tam05 fonteTab">Digitação:
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtGeral" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtPortugues" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtMatematica" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtTecnicaADM" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtDigitacao" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">Relações Humanas:
                        </td>
                        <td class="Tam05 fonteTab ">Ciências:
                        </td>
                        <td class=" Tam05 fonteTab">Pluralidade:
                        </td>
                        <td class=" Tam05 fonteTab">Informática:
                        </td>

                    </tr>

                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtRelacoesHumanas" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtCiencias" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtPluralidade" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtInformatica" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">Word
                        </td>
                        <td class="Tam05 fonteTab">Excel
                        </td>
                        <td class="Tam05 fonteTab">Internet
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtWord" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtExcel" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtInternet" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab" colspan="6">Características Gerais
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab" colspan="6">
                            <asp:TextBox ID="txtCaracteristicasGerais" runat="server" TextMode="MultiLine" Rows="3" CssClass="fonteTexto" Height="43px"
                                Width="70%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td colspan="6">
                            <asp:RegularExpressionValidator ValidationExpression="[\s\S]{0,1000}" ID="MenuLabelVal" runat="server" ErrorMessage="Características gerais deve conter no máximo 1000 caracteres." ControlToValidate="txtCaracteristicasGerais" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Button ID="btnAtualizarPerfil" runat="server" CssClass="btn_novo" Text="Salvar"
                                OnClick="btnAtualizarPerfil_Click" />
                            <asp:Button ID="btnVoltarPerfil" runat="server" CssClass="btn_novo" Text="Voltar"
                                OnClick="btnVoltarPerfil_Click" />
                        </td>
                    </tr>
                </table>



                <br />
            </asp:Panel>


        </asp:View>

    </asp:MultiView>
</asp:Content>
