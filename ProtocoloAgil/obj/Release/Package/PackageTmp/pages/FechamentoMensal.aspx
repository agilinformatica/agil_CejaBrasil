<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="FechamentoMensal.aspx.cs" Inherits="ProtocoloAgil.pages.FechamentoMensal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ConfirmaUpdate() {
            var hidden = document.getElementById("<%# HFConfirmSave.ClientID %>");
            if (confirm("Deseja realmente fechar este mês?") == true)
                hidden.value = "true";
            else
                hidden.value = "false";
        }
    </script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Configurações > <span>Fechamento Mensal</span></p>
    </div>
    <div class="controls">
        <div style="float: left;">
            <asp:Button ID="btn_list" runat="server" CssClass="btn_controls" Text="Fechamento"
                OnClick="btn_list_Click" />
            <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Estatísticas / Relatórios"
                OnClick="btn_gerar_Click" />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
         <div class="formatoTela_02">
            <div class="text_titulo">
                <h1>
                    Fechamento Mensal
                </h1>
            </div>
            <br />
            <div class="controls FundoPainel">
                <div style="float: right; margin-right: 30px">
                    <span class="fonteTab">Ano Referência:</span> &nbsp;
                    <asp:TextBox ID="TBAnoRef" runat="server" CssClass="fonteTexto" MaxLength="4" onkeyup="formataInteiro(this,event);" Height="13px" Width="60px"></asp:TextBox>
                    &nbsp; &nbsp; <span class="fonteTab">Mês Referência:</span> &nbsp;
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
                    &nbsp;
                    <asp:Button ID="btn_pesquisa" runat="server" CssClass="btn_novo" 
                        Text="Fechar Mês" OnClientClick="javascript:ConfirmaUpdate();" 
                        onclick="btn_pesquisa_Click" />
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1"  AllowPaging="True" AutoGenerateColumns="False" PageSize="15"
                        OnPageIndexChanging="GridView_PageIndexChanging"  runat="server" OnDataBound="GridView_DataBound"
                        CssClass="list_results_Menor">
                        <Columns>
                             <asp:BoundField DataField="Apr_Codigo" HeaderText="Matricula" />
                            <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" />
                            <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" />
                            <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                            <asp:BoundField DataField="FechTaxa" HeaderText="Taxa" />
                            <asp:BoundField DataField="FechBolsa" HeaderText="Bolsa" />
                            <asp:BoundField DataField="FechPagamento" HeaderText="Resp. Pagamento" />
                        </Columns>
                        <HeaderStyle CssClass="List_results" />
                         <PagerSettings FirstPageText="" LastPageText="" Mode="NumericFirstLast" NextPageText="Próximo"
                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                        <PagerStyle CssClass="nav_results" HorizontalAlign="Center" />
                    </asp:GridView>
                     <asp:HiddenField ID="HFConfirmSave" runat="server" />
                    <asp:HiddenField ID="HFRowCount" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
         </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
           <table class="FundoPainel centralizar" 
                       style="width: 98%; border: solid 1px #787878;">
                       <tr>
                           <td class="espaco" colspan="5">
                           </td>
                           <td rowspan="4" style="width: 15%;">
                               &nbsp;
                               <asp:Button ID="Button2" runat="server" CssClass="btn_novo" 
                                   OnClick="btnpesquisa_Click" Text="Pesquisar" />
                           </td>
                       </tr>
                       <tr>
                           <td style="width: 10%;">
                               &nbsp;
                           </td>
                           <td class="Tam08 fonteTab" style="text-align: right;">
                               Ano: &nbsp;&nbsp;
                           </td>
                           <td class="Tam15 fonteTab">
                               <asp:TextBox ID="TBAno_pesquisa" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="4"
                                   onkeyup="formataInteiro(this,event);" Width="60px"></asp:TextBox>
                           </td>
                           <td class="Tam15 fonteTab" style="text-align: right;">
                               Parceiro(Opcional): &nbsp;&nbsp;
                           </td>
                           <td class="Tam40 fonteTab">
                               <asp:DropDownList ID="DDparceiro_pesquisa" runat="server" CssClass="fonteTexto" DataTextField="ParNomeFantasia" DataValueField="ParCodigo" 
                                   Height="18px" OnDataBound="IndiceZero" ViewStateMode="Enabled" Width="85%">
                               </asp:DropDownList>
                           </td>
                       </tr>
                       <tr>
                           <td style="width: 5%;">
                           </td>
                           <td class="Tam08 fonteTab" style="text-align: right;">
                               Mês: &nbsp;&nbsp;
                           </td>
                           <td class="Tam15 fonteTab">
                               <asp:DropDownList ID="DDmes_pesquisa" runat="server" CssClass="fonteTexto" 
                                   Height="18px" Width="120px">
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
                           <td class="Tam05 fonteTab" style="text-align: right;">
                               &nbsp;
                           </td>
                           <td class="Tam25 fonteTab">
                               &nbsp;
                           </td>
                       </tr>
                       <tr>
                           <td class="espaco" colspan="6">
                               &nbsp;
                           </td>
                       </tr>
                   </table>
             <div class="controls">
        <div style="float: right;">
            <asp:Button ID="btn_analitico" runat="server" CssClass="btn_controls" Text="Analítico" onclick="btn_analitico_Click" />
            <asp:Button ID="btn_Sintetico" runat="server" CssClass="btn_controls" Text="Sintético" onclick="btn_Sintetico_Click" />
             <asp:Button ID="btn_Sintetico_esp" runat="server" CssClass="btn_controls" Text="Por Parceiro" onclick="btn_Sintetico_esp_Click" />
            <asp:Button ID="btn_imprimir" runat="server" CssClass="btn_controls" 
                Text="Imprimir" onclick="btn_imprimir_Click" />
        </div>
    </div>
      
           <asp:MultiView ID="MultiView2" runat="server">
               <asp:View ID="View3" runat="server">

                 <div class="text_titulo">
                <h1>
                    Estatísticas e Relatórios - Pesquisa Analítico
                </h1>
            </div>
            <br/>
                <div class="formatoTela_02">
              
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                        <br/>
                        &nbsp;&nbsp;&nbsp; <asp:literal runat="server" ID="Lit_dados"  /> 
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" PageSize="15"
                                AutoGenerateColumns="False" CssClass="list_results_Menor" 
                                OnDataBound="GridView_DataBound" OnPageIndexChanging="GridView_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="Apr_Codigo" HeaderText="Matricula" />
                                    <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" />
                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" />
                                    <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                                    <asp:BoundField DataField="FechTaxa" HeaderText="Taxa (R$)" 
                                        DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="FechBolsa" HeaderText="Bolsa (R$)" 
                                        DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="FechPagamento" HeaderText="Resp. Pagamento" />
                                    <asp:BoundField DataField="TurNome" HeaderText="Turma" />
                                    <asp:BoundField DataField="AreaDescricao" HeaderText="Área" />
                                </Columns>
                                <HeaderStyle CssClass="List_results" />
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" 
                                    LastPageImageUrl="~/images/seta_ultimo.jpg" LastPageText="" 
                                    Mode="NumericFirstLast" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
               </asp:View>
                  <asp:View ID="View4" runat="server">

                 <div class="text_titulo">
                <h1>
                    Estatísticas e Relatórios - Pesquisa Sintético
                </h1>
            </div>
            <br/>
                <div class="formatoTela_02">
                
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                        <br/>
                        &nbsp;&nbsp;&nbsp;  
                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" style="width: 500px; margin: 0 auto;"
                                AutoGenerateColumns="False" CssClass="list_results_Menor" 
                                OnDataBound="GridView_DataBound" OnPageIndexChanging="GridView_PageIndexChanging">
                                <Columns>
                                 
                                    <asp:BoundField DataField="FechPagamento" HeaderText="Responsável" />
                                    <asp:BoundField DataField="QTD" HeaderText="Nº de Alunos" />
                                    <asp:BoundField DataField="TotalTaxa" DataFormatString="{0:c}" HeaderText="Total Taxa" />
                                    <asp:BoundField DataField="TotalBolsa"  DataFormatString="{0:c}" HeaderText="Total Bolsa" />
                                </Columns>
                                <HeaderStyle CssClass="List_results" />
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" 
                                    LastPageImageUrl="~/images/seta_ultimo.jpg" LastPageText="" 
                                    Mode="NumericFirstLast" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
               </asp:View>
                    <asp:View ID="View5" runat="server">

                 <div class="text_titulo">
                <h1>
                   Estatísticas e Relatórios - Pesquisa Sintético por Parceiro
                </h1>
            </div>
            <br/>
                <div class="formatoTela_02">
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                        <br/>
                          <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="list_results_Menor" 
                                OnDataBound="GridView_DataBound" OnPageIndexChanging="GridView_PageIndexChanging" style="width: 600px; margin: 0 auto;">
                                <Columns>
                                    <asp:BoundField DataField="ParNomeFantasia" HeaderText="Parceiro" />
                                    <asp:BoundField DataField="FechPagamento" HeaderText="Responsável" />
                                    <asp:BoundField DataField="QTD" HeaderText="Nº de Alunos" />
                                    <asp:BoundField DataField="TotalTaxa" DataFormatString="{0:c}" HeaderText="Total Taxa" />
                                    <asp:BoundField DataField="TotalBolsa" DataFormatString="{0:c}" HeaderText="Total Bolsa" />
                                </Columns>
                                <HeaderStyle CssClass="List_results" />
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" 
                                    LastPageImageUrl="~/images/seta_ultimo.jpg" LastPageText="" 
                                    Mode="NumericFirstLast" NextPageText="Próximo" PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" HorizontalAlign="Center" />
                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
               </asp:View>
                <asp:View ID="View6" runat="server">
                  <div class="centralizar">
                <iframe src="Visualizador.aspx" id="Iframe3" class="VisualFrame" name="Iframe1">
                </iframe>
            </div>
               </asp:View>
          </asp:MultiView>
        </asp:View>
    </asp:MultiView>
</asp:Content>
