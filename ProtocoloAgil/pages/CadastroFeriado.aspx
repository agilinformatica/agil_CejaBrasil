<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.CadastroFeriado"
    CodeBehind="CadastroFeriado.aspx.cs" %>

<%@ Register assembly="CalendarExtenderPlus" namespace="AjaxControlToolkitPlus" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
    function GetConfirm() {
        var hf = document.getElementById("<%# HFConfirma.ClientID %>");
        if (confirm("Deseja realmente excluir este recesso/feriado?") == true)
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
                Configurações > <span>Cadastro de Feriados</span></p>
        </div>
        <div class="controls">

         <div style="float: left;"> 
            <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar" onclick="btn_listar_Click"  />
            <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" onclick="btn_novo_Click"  />
            <asp:Button ID="btn_texto" runat="server" CssClass="btn_controls" Text="Arquivo de Texto" onclick="btn_texto_Click" />
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
                            <asp:BoundField DataField="Data" HeaderText="Data" 
                                InsertVisible="False" ReadOnly="True" SortExpression="Data" 
                                DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nome" HeaderText="Descrição" 
                                SortExpression="Nome">
                            <HeaderStyle Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unidade" HeaderText="Unidade" 
                                SortExpression="Unidade">
                            <HeaderStyle Width="40%" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" 
                                SelectImageUrl="~/Images/icon_edit.png" ShowSelectButton="True" >
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                             <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMBexcluir"  CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "Codigo")%>' 
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
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
         <asp:View ID="View2" runat="server">
          <asp:Panel ID="Panel4" runat="server" CssClass="centralizar" Height="400px" width="700px">
         <br />
           <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <table class="Table FundoPainel">
                    <tr>
                        <td class="cortitulo corfonte titulo" colspan="6" style="font-size: large;">
                            <asp:Label ID="LBtituloAlt" runat="server" Text="Cadastro de Feriados"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td class="espaco" colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                     <td class="Tam05" style="text-align: left">
                              &nbsp;
                            </td>
                         <td class="Tam15 fonteTab">
                             &nbsp;&nbsp; Data :</td>
                        <td class="Tam18" style="text-align: left">
                            <asp:TextBox ID="TBDataRef" runat="server" CssClass="fonteTexto" MaxLength="10" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                                onkeyup="formataData(this,value);" style=" width: 100px; height: 13px;"> </asp:TextBox> 
                            <cc1:CalendarExtenderPlus ID="TBDataRef_CalendarExtenderPlus" runat="server" 
                                TargetControlID="TBDataRef">
                            </cc1:CalendarExtenderPlus>
                            &nbsp;
                            </td>
                             <td class="Tam20 fonteTab">
                                 &nbsp;&nbsp;</td>
                        <td class="Tam18" style="text-align: left">
                              &nbsp;
                            </td>
                         <td class="Tam05" >
                            &nbsp;</td>
                    </tr>

                         <tr>
                             <td class="Tam05" style="text-align: left">
                                 &nbsp;</td>
                             <td class="Tam10 fonteTab">
                                 &nbsp;&nbsp; Nome do Feriado:</td>
                             <td class="Tam18" style="text-align: left" colspan="2">
                                 <asp:TextBox ID="TB_nome_feriado" runat="server" CssClass="fonteTexto" 
                                     MaxLength="50" style=" width: 80%; height: 13px;"> </asp:TextBox>
                             </td>
                             <td class="Tam18" style="text-align: left">
                                 &nbsp;</td>
                             <td class="Tam05">
                                 &nbsp;</td>
                         </tr>

                    <tr>
                     <td class="Tam05" style="text-align: left">
                              &nbsp;
                            </td>
                        <td class="Tam10 fonteTab" colspan="5">
                            &nbsp;&nbsp; Unidade(s) onde o feriado é válido: </td>
                        </tr>
                         <tr>
                             <td class="Tam05" style="text-align: left">
                                 &nbsp;</td>
                             <td class="Tam10 fonteTab" colspan="5">
                                &nbsp;&nbsp; <asp:DropDownList ID="DDunidade" runat="server" BorderStyle="Groove" 
                                     BorderWidth="1px" CssClass="fonteTexto" DataSourceID="SDS_Unidades" 
                                     DataTextField="UniNome" DataValueField="UniCodigo" Height="20px" 
                                     OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();" Width="75%">
                                 </asp:DropDownList>
                             </td>
                         </tr>
                        <tr>
                   
                          <td class="Tam05">
                              &nbsp; </td>
                            <td class="Tam10">
                                &nbsp;
                            </td>
                            <td class="Tam60" colspan="3" style="text-align: left">
                                &nbsp;&nbsp;</td>
                                 <td class="Tam05" >
                            &nbsp;</td>
                    </tr>

                    <tr>
                        <td class="espaco" colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
            </asp:UpdatePanel>
                 <br />
                 <div class="controls" style="margin: 0 auto; text-align: center;">
                        <asp:Button ID="BTaltera" runat="server" OnClick="BTaltera_Click" CssClass="btn_novo" Text="Confirmar" /> &nbsp;
                         <asp:Button ID="btn_voltar" runat="server"  CssClass="btn_novo" Text="Voltar" 
                            onclick="btn_listar_Click" />
                      
                </div>
                             </asp:Panel> 
                                     <asp:SqlDataSource ID="SDS_Unidades" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                            SelectCommand="SELECT UniCodigo, UniNome FROM CA_Unidades WHERE UniCodigo <>99 order by UniNome">
                        </asp:SqlDataSource>
          </asp:View>
    </asp:MultiView>
   </div>
</asp:Content>