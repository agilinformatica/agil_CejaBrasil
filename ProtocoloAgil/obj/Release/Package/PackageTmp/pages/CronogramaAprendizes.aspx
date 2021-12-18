<%@ Page Title="" Language="C#" MasterPageFile="~/MaAluno.Master" AutoEventWireup="true" CodeBehind="CronogramaAprendizes.aspx.cs" Inherits="ProtocoloAgil.pages.CronogramaAprendizes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Pedagógico > <span> Cronograma de Aulas</span></p>
    </div>
     <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
         <div class="formatoTela_02">
                <div class="controls FundoPainel">
                    <div style="float: left; margin-left: 30px;">
                        <span class="fonteTab">Aprendiz: </span>&nbsp;
                        <asp:Label ID="LBAprendiz_Conceito" runat="server" class="fonteTab" Text="" />
                        <br />
                        <span class="fonteTab">Curso: </span>&nbsp;
                        <asp:Label ID="LBCurso_Conceito" runat="server" class="fonteTab" Text="" />
                    </div>
                    <div style="float: left; margin-left: 150px;">
                        <span class="fonteTab">Turma: </span>&nbsp;
                        <asp:Label ID="LBTurma_Conceito" runat="server" class="fonteTab" Text="" />
                        <br />
                        <span class="fonteTab">Parceiro: </span>&nbsp;
                        <asp:Label ID="LBCodigo_Parceiro" runat="server" class="fonteTab" Text="" />
                    </div>
                    <div style="float: right; margin-right: 30px;">
                        <asp:Button ID="btn_adicionar" runat="server" CssClass="btn_novo" Text="Imprimir" OnClick="btn_adicionar_Click" />
                    </div>
                </div>
                <br />
    <asp:UpdatePanel runat="server">
      <ContentTemplate>
        <asp:GridView ID="GridView1" CssClass="list_results" runat="server" OnDataBound="GridView_DataBound"
                        AllowPaging="True" AutoGenerateColumns="False" 
              onpageindexchanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="Professor" HeaderText="Professor">
                                <HeaderStyle Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Disciplina" HeaderText="Disciplina">
                                <HeaderStyle Width="12%" />
                            </asp:BoundField>
                              <asp:BoundField DataField="ADPDataAula" DataFormatString="{0:dddd}" HeaderText="Dia Semana">
                                <HeaderStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ADPDataAula" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                                <HeaderStyle Width="7%" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                        <PagerStyle CssClass="nav_results" HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                     <asp:HiddenField ID="HFRowCount" runat="server" />
                      <asp:HiddenField ID="HFmatricula" runat="server" />
                       <asp:HiddenField ID="HFcodigo" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
      

      </div>

    </asp:View>
          <asp:View ID="View2" runat="server">


     <div class="controls">
        <div style="float: right;">
            <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar" />
        </div>
    </div>
     <div class="centralizar">
                <iframe src="Visualizador.aspx" id="Iframe1" class="VisualFrame" name="Iframe1">
                </iframe>
            </div>
    </asp:View>
    </asp:MultiView>
</asp:Content>
