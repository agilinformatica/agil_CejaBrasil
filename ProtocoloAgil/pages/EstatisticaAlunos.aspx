<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="EstatisticaAlunos.aspx.cs" Inherits="ProtocoloAgil.pages.EstatisticaAlunos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div class="breadcrumb">
        <p style="text-align: left;">
            Estatísticas > <span>Jovens por Status</span></p>
    </div>
      <div class="controls">
        <div style="float: left;">
            <asp:Button ID="bt_lista" runat="server" CssClass="btn_controls" Text="Lista" 
                onclick="bt_lista_Click" />
            <asp:Button ID="btnSituacaoSexo" runat="server" CssClass="btn_controls" Text="Situação/Sexo" 
                onclick="btnSituacaoSexo_Click" />
             <asp:Button ID="btnSituacaoIdade" runat="server" CssClass="btn_controls" Text="Situação/Idade" 
                onclick="btnSituacaoIdade_Click" />
            <asp:Button ID="bt_grafico" runat="server" CssClass="btn_controls" 
                Text="Gráfico" onclick="bt_grafico_Click" />
            <asp:Button ID="Button1" runat="server" CssClass="btn_controls" Text="Imprimir" 
                onclick="Button1_Click" />
        </div>
    </div>
    <div class="formatoTela_02">
         <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="text_titulo">
                <h1>
                    Jovens por Status
                </h1>
            </div>
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" Style="width:50%;
                margin: auto" OnDataBound="GridView_DataBound" 
                DataSourceID="SDS_AprendizesSituacao" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                      <asp:BoundField DataField="StaDescricao" HeaderText="Status do Jovem" 
                        SortExpression="StaDescricao">
                    </asp:BoundField>
                     <asp:BoundField DataField="QTD" HeaderText="Quantidade" 
                          SortExpression="QTD" ReadOnly="True">
                      <HeaderStyle Width="20%" />
                    </asp:BoundField>
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
            <asp:SqlDataSource ID="SDS_AprendizesSituacao" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="Select count(Apr_Codigo) as QTD, convert( numeric(10,2),(( convert( numeric(10,2),count(Apr_Codigo) *100))/ convert( numeric(10,2),( select count(Apr_Situacao) 
from CA_Aprendiz inner join CA_SituacaoAprendiz on  Apr_Situacao = StaCodigo  ))))as Percentual,   CA_SituacaoAprendiz.StaDescricao
from  dbo.CA_Aprendiz a  inner join CA_SituacaoAprendiz on  Apr_Situacao = StaCodigo group by StaDescricao order by count(Apr_Codigo) desc"></asp:SqlDataSource>
            <asp:HiddenField ID="HFRowCount" runat="server" />
            <br />
        </asp:View>
        <asp:View runat="server" ID="View2">
         <div class="controls fonteTab">
           Selecione o tipo de Gráfico:&nbsp; 
             <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" AutoPostBack="true"
                 runat="server" onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
              <asp:ListItem  Value="1" Selected="true"> Colunas </asp:ListItem>
              <asp:ListItem  Value="2"> Pizza(%) </asp:ListItem>
             </asp:RadioButtonList>
         </div>
            <div class="centralizar">
            <asp:Chart ID="Chart1" runat="server" DataSourceID="SDS_AprendizesSituacao" 
                    Height="400px" Width="400px">
                <Series>
                    <asp:Series BackGradientStyle="DiagonalLeft" ChartArea="ChartArea1" 
                        Font="Tahoma, 9pt, style=Bold" IsValueShownAsLabel="True" 
                        IsVisibleInLegend="false" IsXValueIndexed="True" LabelBackColor="Transparent" 
                        Legend="Legend1" Name="Series1" Palette="BrightPastel"  XValueMember="StaDescricao" YValueMembers="QTD"  >
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <Area3DStyle Enable3D="True" LightStyle="Realistic" />
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Docking="Bottom" Name="Legend1">
                    </asp:Legend>
                </Legends>
            </asp:Chart>
            </div>
        </asp:View>
         <asp:View runat="server" ID="View3">
             <div class="centralizar">
                <iframe runat="server" id="Iframe1" src="visualizador.aspx" class="VisualFrame">
                </iframe>
            </div>
           </asp:View>




             <asp:View ID="View5" runat="server">
            <div class="text_titulo">
                <h1>
                   Jovens por Situação/Sexo
                </h1>
            </div>
            <br />
            <asp:GridView ID="GridSituacaoSexo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" Style="width:50%;
                margin: auto" OnDataBound="GridSituacaoRegiao_DataBound" 
                DataSourceID="SDS_AprendizesSituacaoSexo" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                      <asp:BoundField DataField="descricao" HeaderText="Status do Jovem" 
                        SortExpression="descricao">
                    </asp:BoundField>
                     <asp:BoundField DataField="abreviatura" HeaderText="Abreviatura" 
                        SortExpression="abreviatura">
                    </asp:BoundField>
                      <asp:BoundField DataField="sexo" HeaderText="Sexo" 
                        SortExpression="sexo">
                    </asp:BoundField>
                     <asp:BoundField DataField="QTD" HeaderText="QTD" 
                          SortExpression="QTD" ReadOnly="True">
                      <HeaderStyle Width="20%" />
                    </asp:BoundField>
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
            <asp:SqlDataSource ID="SDS_AprendizesSituacaoSexo" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                SELECT  CA_SituacaoAprendiz.StaDescricao as descricao,  CA_SituacaoAprendiz.StaAbreviatura as abreviatura,  CA_Aprendiz.Apr_Sexo as sexo, Count( CA_Aprendiz.Apr_Codigo) AS qtd
FROM  CA_Aprendiz INNER JOIN  CA_SituacaoAprendiz ON  CA_Aprendiz.Apr_Situacao =  CA_SituacaoAprendiz.StaCodigo
GROUP BY  CA_SituacaoAprendiz.StaDescricao,  CA_SituacaoAprendiz.StaAbreviatura,  CA_Aprendiz.Apr_Sexo
ORDER BY  CA_SituacaoAprendiz.StaDescricao,  CA_Aprendiz.Apr_Sexo;"></asp:SqlDataSource>
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <br />
        </asp:View>

              <asp:View ID="View6" runat="server">
            <div class="text_titulo">
                <h1>
                    Jovens por Situação/Idade
                </h1>
            </div>
            <br />
            <asp:GridView ID="gridSituacaoIdade" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CaptionAlign="Top" CssClass="list_results" HorizontalAlign="Center" Style="width:50%;
                margin: auto" OnDataBound="GridSituacaoRegiao_DataBound" 
                DataSourceID="SDS_AprendizesSituacaoIdade" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                      <asp:BoundField DataField="descricao" HeaderText="Status do Jovem" 
                        SortExpression="descricao">
                    </asp:BoundField>
                     <asp:BoundField DataField="abreviatura" HeaderText="Abreviatura" 
                        SortExpression="abreviatura">
                    </asp:BoundField>
                      <asp:BoundField DataField="idade" HeaderText="Idade" 
                        SortExpression="sexo">
                    </asp:BoundField>
                     <asp:BoundField DataField="QTD" HeaderText="QTD" 
                          SortExpression="QTD" ReadOnly="True">
                      <HeaderStyle Width="20%" />
                    </asp:BoundField>
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
            <asp:SqlDataSource ID="SDS_AprendizesSituacaoIdade" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="
                SELECT CA_SituacaoAprendiz.StaDescricao as descricao, CA_SituacaoAprendiz.StaAbreviatura as abreviatura ,DATEDIFF(yy,CA_Aprendiz.Apr_DataDeNascimento, GETDATE()) AS idade
, Count(CA_Aprendiz.Apr_Codigo) AS QTD
FROM CA_Aprendiz INNER JOIN CA_SituacaoAprendiz ON CA_Aprendiz.Apr_Situacao = CA_SituacaoAprendiz.StaCodigo
GROUP BY CA_SituacaoAprendiz.StaDescricao, CA_SituacaoAprendiz.StaAbreviatura,DATEDIFF(yy,CA_Aprendiz.Apr_DataDeNascimento, GETDATE())
ORDER BY CA_SituacaoAprendiz.StaDescricao,DATEDIFF(yy,CA_Aprendiz.Apr_DataDeNascimento, GETDATE())"></asp:SqlDataSource>
            <asp:HiddenField ID="HiddenField3" runat="server" />
            <br />
        </asp:View>


        </asp:MultiView>
    </div>
</asp:Content>
