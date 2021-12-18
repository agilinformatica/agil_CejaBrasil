<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#"
    AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.CadastroModulo"
    CodeBehind="CadastroModulo.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
    function GetConfirm() {
        var hf = document.getElementById("<%# HFConfirma.ClientID %>");
        if (confirm("Deseja realmente excluir este Plano Curricular?") == true)
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
                Pedagógico > <span>Cadastro de </span>Módulos de Aprendizagem</p>
        </div>
        <div class="controls">

         <div style="float: left;"> 
            <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar" onclick="btn_listar_Click"  />
            <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" onclick="btn_novo_Click"  />
            <asp:Button ID="btn_texto" runat="server" CssClass="btn_controls" Text="Arquivo de Texto" onclick="btn_texto_Click" />
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
        <asp:View ID="View1" runat="server" >
         <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list_results"
                        AutoGenerateColumns="False" ondatabound="GridView_DataBound"
                        Style="width: 80%; margin: 0 auto" HorizontalAlign="Center" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" 
                        onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="PlanCodigo" HeaderText="Código" 
                                InsertVisible="False" ReadOnly="True" SortExpression="PlanCodigo">
                            <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PlanDescricao" HeaderText="Módulo de aprendizagem" 
                                SortExpression="PlanDescricao">
                            <HeaderStyle Width="30%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CurDescricao" HeaderText="Curso" 
                                SortExpression="CurDescricao">
                            <HeaderStyle Width="40%" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" 
                                SelectImageUrl="~/Images/icon_edit.png" ShowSelectButton="True" >
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                             <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMBexcluir"  CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "PlanCodigo")%>' 
                                    OnClientClick="javascript:GetConfirm();" onclick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"  runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="10%" />
                                </asp:TemplateField>
                         </Columns><HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle Height="25px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" BackColor="#999999" />
                        <FooterStyle Width="30px" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Height="20px" BorderStyle="Groove" BorderWidth="1px" />
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
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
         <asp:View ID="View2" runat="server">
          <asp:Panel ID="Panel4" runat="server" Height="400px" width="100%">
         <br />
           <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <table class="Table FundoPainel" style="width: 70%; margin: 0 auto;">
                    <tr>
                        <td class="cortitulo corfonte titulo" style="font-size: large;">
                          Cadastro de Módulos de Aprendizagem</td>
                    </tr>
                 
                  <table class="Table FundoPainel" style="width: 70%; margin: 0 auto;">
                    <tr>
                        <td class="fonteTab Tam05" > &nbsp;</td>
                        <td class="fonteTab Tam08" style="text-align: left;"> &nbsp;</td>
                        <td class=" Tam40 fonteTab" style="text-align: left;">
                            &nbsp;</td>
                               <td class="fonteTab Tam05" > &nbsp;</td>
                   </tr>
                      <tr>
                          <td class="fonteTab Tam05">
                              &nbsp;</td>
                          <td class="fonteTab" style="text-align: left;">
                              Código</td>
                          <td class=" Tam40 fonteTab" style="text-align: left;">
                              &nbsp;&nbsp; Nome do Módulo</td>
                          <td class="fonteTab Tam05">
                              &nbsp;</td>
                      </tr>
                   <tr>
                        <td class="fonteTab Tam05" > &nbsp;</td>
                        <td class="Tam08 fonteTab" style="text-align: left;"> 
                            <asp:TextBox ID="TBcodigo" runat="server" CssClass="fonteTexto" MaxLength="10"  Enabled="false"
                                style=" width: 80px; height: 14px;"> </asp:TextBox>
                        </td>
                        <td class="fonteTab" style="text-align: left;">
                           &nbsp;&nbsp; <asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" MaxLength="80" 
                                style=" width: 90%; height: 14px;"></asp:TextBox>
                        </td>
                             <td class="fonteTab Tam05" > &nbsp;</td>
                   </tr>
                      <tr>
                          <td class="fonteTab Tam05">
                              &nbsp;</td>
                          <td class="fonteTab" style="text-align: left;">
                              Curso</td>
                          <td class="fonteTab" style="text-align: left;">
                              &nbsp;</td>
                          <td class="fonteTab Tam05">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td class="fonteTab Tam05">
                              &nbsp;</td>
                          <td class="fonteTab" style="text-align: left;" colspan="2">
                              <asp:DropDownList ID="DDcurso" runat="server" AppendDataBoundItems="True" 
                                  AutoPostBack="True" BorderStyle="Groove" BorderWidth="1px" 
                                  CssClass="fonteTexto" DataTextField="CurDescricao" DataValueField="CurCodigo" 
                                  Height="18px" OnDataBound="IndiceZero"  Width="95%">
                              </asp:DropDownList>
                          </td>
                          <td class="fonteTab Tam05">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td class="fonteTab Tam05">
                              &nbsp;</td>
                          <td class="fonteTab" style="text-align: left;">
                              &nbsp;</td>
                          <td class="fonteTab" style="text-align: left;">
                              &nbsp;</td>
                          <td class="fonteTab Tam05">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td class="espaco" colspan="4">
                              &nbsp;</td>
                      </tr>
                   </table>
                      </ContentTemplate>
            </asp:UpdatePanel>
                 <br />
                 <div class="controls" style="width: 70%; margin: 0 auto; text-align: center;">
                        <asp:Button ID="BTaltera" runat="server" OnClick="BTaltera_Click" CssClass="btn_novo" Text="Confirmar" /> &nbsp;
                         <asp:Button ID="btn_voltar" runat="server"  CssClass="btn_novo" Text="Voltar" 
                            onclick="btn_listar_Click" />
                      
                </div>
                             </asp:Panel> 
          </asp:View>
    </asp:MultiView>
   </div>
</asp:Content>