<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    MasterPageFile="~/MPusers.master" AutoEventWireup="true" CodeBehind="ControleEducadores.aspx.cs"
    Inherits="ProtocoloAgil.pages.ControleEducadores" %>
<%@ Import Namespace="ProtocoloAgil.Base" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    function valida_cpf() {
        var numcpf = document.getElementById('<%# TBCodigo.ClientID %>');
        var hidden = document.getElementById('<%# HFcodigo.ClientID %>');
        var p = VerificaCPF(numcpf);
        hidden.value = eval(p.toString());
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel4" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Pedagógico > <span>Lista de Monitores</span></p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <asp:Button ID="listar" runat="server" CssClass="btn_controls" Text="Lista" OnClick="listar_Click" />
                <asp:Button ID="Novo" runat="server" CssClass="btn_controls" Text="Novo Monitor"
                    OnClick="Button1_Click" />
            </div>
        </div>
    </asp:Panel>
    <br style="height: 2px;" />
     <div class="formatoTela_02">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
         <div class="formatoTela_01">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="text_titulo">
                    <h1>
                        Pesquisa de Monitores</h1>
                </div>
                <br />
                <div class="controls FundoPainel" style="border:#5E5E5E 1px solid;">
                    <div style="float: left; margin-left: 70px">
                        <strong> <span class="fonteTab">Nome:</span></strong>&nbsp;&nbsp;
                        <asp:TextBox ID="TBNome" runat="server" Height="23px" Width="400px" onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBCodigo').value = '';"></asp:TextBox>
                    </div>
                    <div style="float: right;">
                        <strong> <span class="fonteTab">CPF:</span></strong>&nbsp;&nbsp;<asp:TextBox ID="TBCodigo" runat="server" Height="23px" 
                            Width="125px" onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBNome').value= '';"
                            onkeyup="formataCPF(this,event);" MaxLength="14"  onblur="" ></asp:TextBox>
                        <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_novo" Text="Pesquisar"
                            OnClick="btnpesquisa_Click" />
                    </div>
                </div>
                <br />
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CssClass="list_results"
                    HorizontalAlign="Center" 
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
                    ondatabound="GridView_DataBound" PageSize="12" Style="width: 98%; margin: auto" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="EducCodigo" HeaderText="Código" InsertVisible="False"
                            ReadOnly="True" SortExpression="EducCodigo">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%"  />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EducNome" HeaderText="Nome" 
                            SortExpression="EducNome">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40%"  />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                           <asp:BoundField DataField="EducTelefone" HeaderText="Telefone" />
                        <asp:BoundField DataField="EducCPF" HeaderText="CPF" />
                        <asp:BoundField DataField="EducSexo" HeaderText="Sexo" 
                            SortExpression="EducSexo">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%"  />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EducSituacao" HeaderText="Sit." 
                            SortExpression="EducSituacao">
                            <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Middle"
                                Width="5%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EducEMail" HeaderText="E-mail" 
                            SortExpression="EducEMail">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%"  />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/images/icon_edit.png"
                            SelectText="Alterar" ShowSelectButton="True">
                            <HeaderStyle  HorizontalAlign="Center" VerticalAlign="Middle"
                                Width="5%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                      <asp:HiddenField ID="HFcodigo" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                </Triggers>
            </asp:UpdatePanel>
             </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
           <div class="centralizar">
                <iframe id="I1" width="98%" height="665px" style ="border: none;" name="I1" src="DadosProfessores.aspx"></iframe>
         </div>
        </asp:View>
    </asp:MultiView>
  </div>
</asp:Content>
