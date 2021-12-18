<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="LancamentoNotas.aspx.cs" Inherits="ProtocoloAgil.pages.LancamentoNotas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
    <style type="text/css">
        .auto-style1 {
            width: 67%;
        }

        .auto-style2 {
            height: 18px;
        }

         .hiddencol
  {
    display: none;
  }
    </style>

    <script type="text/javascript">

        function popup(url, width, height) {
            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "Contrato", "status=no, scrollbar=yes, toolbar=no,menubar=no, width= " + width + ",height=  " + height + ",resizable=yes,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }


        function popup02(url, width, height) {

            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "ControleAlunos", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <div class="formatoTela_02">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnpesquisa">
                    <div class="text_titulo">
                        <h1>Pesquisa de Jovens</h1>
                    </div>
                    <br />
                    <div class="controls FundoPainel">
                        <div style="float: left; margin-left: 70px">
                            <span class="fonteTab"><strong>Nome:</strong></span> &nbsp;&nbsp;
                            <asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" Height="20px" Width="400px"
                                onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBCodigo').value = '';"></asp:TextBox>
                        </div>
                        <div style="float: right;">
                            <span class="fonteTab"><strong>Matricula:</strong></span>&nbsp;&nbsp;<asp:TextBox
                                ID="TBCodigo" runat="server" Height="20px" Width="100px" onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBNome').value= '';"
                                onkeyup="formataInteiro(this,event);"></asp:TextBox>
                            <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar"
                                OnClick="btnpesquisa_Click" />
                        </div>
                    </div>

                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CaptionAlign="Top" CssClass="grid_Aluno" HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging"
                                PageSize="15" DataKeyNames="Apr_AreaAtuacao,Apr_PlanoCurricular" OnRowCommand="GridView1_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" >
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
                                    


                                     <asp:ButtonField ButtonType="Image" CommandName="Notas"
                                            HeaderText="Lançar Notas" ImageUrl="~/images/lupa.png" Text="Button">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>

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

        <asp:View runat="server" ID="view2">
            <div class="controls FundoPainel">
                <div style="float: left; margin-left: 70px">
                    <span class="fonteTab"><strong>Nome:</strong></span> &nbsp;&nbsp;
                            <asp:TextBox ID="txtNome" ReadOnly="true" runat="server" CssClass="fonteTexto" Height="20px" Width="400px"></asp:TextBox>
                </div>
                <div style="float: right;">
                    <span class="fonteTab"><strong>Matricula:</strong></span>&nbsp;&nbsp;<asp:TextBox
                        ID="txtMatricula" runat="server" ReadOnly="true" Height="20px" Width="100px" onkeyup="formataInteiro(this,event);"></asp:TextBox>
                </div>
            </div>
            <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="float: left;">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="btn_novo" Text="Salvar"
                        OnClick="btnSalvar_Click" OnClientClick="this.disabled = true; this.value = 'Salvando...';"   UseSubmitBehavior="false"/>
                        <asp:Button ID="btnVoltar" runat="server" CssClass="btn_novo" Text="Voltar"
                        OnClick="btnVoltar_Click" />
                        </div>
                    <asp:GridView ID="gridNotas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CaptionAlign="Top" CssClass="grid_Aluno" HorizontalAlign="Center"
                        PageSize="15"  OnSelectedIndexChanged="duty_SelectedIndexChanged" OnRowDataBound="gridNotas_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="DisDescricao" HeaderText="Descrição" InsertVisible="False"
                                ReadOnly="True" SortExpression="DisDescricao">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TurNome" HeaderText="Turma" InsertVisible="False"
                                ReadOnly="True" SortExpression="TurNome">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NdiNota" HeaderText="Nota" InsertVisible="False"
                                ReadOnly="True" SortExpression="NdiNota">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NdiDisciplina" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"> 
                            <HeaderStyle CssClass="hiddencol" />
                            <ItemStyle CssClass="hiddencol" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Conceito">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                <ItemTemplate>
                                    <asp:DropDownList   ID="DDNota" runat="server" CssClass="fonteTexto" DataSourceID="SDSConceito"
                                        DataTextField="ConCodigo" DataValueField="ConNota" Height="20px"  Width="80%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
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

                </ContentTemplate>

            </asp:UpdatePanel>
            <asp:SqlDataSource ID="SDSConceito" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                SelectCommand="Select ConNota, ConCodigo from CA_Conceitos"></asp:SqlDataSource>
        </asp:View>
    </asp:MultiView>
</asp:Content>
