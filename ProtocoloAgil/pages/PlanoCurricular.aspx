<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true"
    CodeBehind="PlanoCurricular.aspx.cs" Inherits="ProtocoloAgil.pages.PlanoCurricular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     function GetConfirm() {
         var hf = document.getElementById("<%# HFConfirma.ClientID %>");
         if (confirm("Deseja realmente excluir esta disciplina?") == true)
             hf.value = "true";
         else
             hf.value = "false";
     }
 
 </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            font-weight: bold;
            color: #5E5E5E;
            margin-left: 0px;
            height: 2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Pedagógico> <span>Planos Curriculares</span></p>
    </div>
    <div class="controls">

        <div style="float: left;">
            <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="btn_listar_Click" />
            <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="btn_novo_Click" />
            <asp:Button ID="btn_relatorio" runat="server" CssClass="btn_controls" Text="Relatório" OnClick="btn_relatorio_Click" Visible="false" />
        </div>
    </div>
    <div class="formatoTela_02">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
        <br/>
             <div class="centralizar" style="width: 90%;">
                <table class="FundoPainel Table centralizar">
                <tr><td class="corfonte cortitulo titulo" colspan="4"> Plano Curricular - Pesquisa </td></tr>
                 <tr><td class="espaco" colspan="4"> &nbsp; </td></tr>
                    <tr>
                        <td class="Tam10 fonteTab"> &nbsp;&nbsp; Código</td>
                        <td class="Tam40 fonteTab" style="text-align: left;"> &nbsp;&nbsp; Curso</td>
                        <td class="Tam30 fonteTab"> &nbsp;&nbsp; Módulo de Aprendizagem </td>
                        <td class="Tam15" rowspan="2">
                             &nbsp;
                                <asp:Button ID="btn_pesquisa" runat="server" CssClass="btn_novo" 
                                    OnClick="Button2_Click" Text="Pesquisar" />
                        </td>   
                    </tr>
                     <tr>
                         <td class="Tam10 fonteTab">
                            &nbsp;&nbsp; <asp:TextBox ID="TB_codigoCurso" runat="server" Enabled="false" CssClass="fonteTexto"
                                 style="height: 13px; width:85%;"></asp:TextBox>
                         </td>
                         <td class="Tam40 fonteTab" style="text-align: left;">
                            &nbsp;&nbsp; <asp:DropDownList ID="DD_curso" runat="server" DataSourceID="SDS_Cursos" AutoPostBack="true"
                                 DataTextField="CurDescricao" DataValueField="CurCodigo" CssClass="fonteTexto" OnDataBound="IndiceZero"
                                 style="height: 18px; width:85%;" 
                                 onselectedindexchanged="DD_curso_SelectedIndexChanged">
                             </asp:DropDownList>
                         </td>
                         <td class="Tam12 fonteTab"> 
                         &nbsp;&nbsp; <asp:DropDownList ID="DD_plano" runat="server" DataTextField="PlanDescricao" DataValueField="PlanCodigo" 
                         CssClass="fonteTexto" OnDataBound="IndiceZero" style="height: 18px; width:90%;" >
                             </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    </table>
                      <br/>
                       <br/>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="list_results" DataSourceID="SDS_Plano_curricular"   ondatabound="GridView_DataBound"
                        Style="width: 100%; margin: 0 auto" HorizontalAlign="Center" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="PlanCodigo" HeaderText="Código" 
                                InsertVisible="False" ReadOnly="True" SortExpression="PlanCodigo">
                            <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PlanCurso" HeaderText="Curso" 
                                SortExpression="PlanCurso">
                            </asp:BoundField>
                            <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina" 
                                SortExpression="DisDescricao" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PlcCargaHoraria" HeaderText="Carga Horária" 
                                SortExpression="PlcCargaHoraria" />
                            <asp:BoundField DataField="PlcTipoAvaliacao" HeaderText="Tipo Avaliação" 
                                SortExpression="PlcTipoAvaliacao" />
                            <asp:BoundField DataField="PlcNumeroAulas" HeaderText="Número Aulas" 
                                SortExpression="PlcNumeroAulas" />
                                 <asp:BoundField DataField="PlcOrdemDisciplina" HeaderText="Ordem" 
                                SortExpression="PlcOrdemDisciplina" />
                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" 
                                SelectImageUrl="~/Images/icon_edit.png" ShowSelectButton="True" >
                            <HeaderStyle Width="8%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMBexcluir"  
                                    CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "PlanCodigo")   + "_" + DataBinder.Eval(Container.DataItem, "DisCodigo") %>' 
                                    OnClientClick="javascript:GetConfirm();" onclick="IMBexcluir_Click" 
                                        ImageUrl="~/images/icon_remove.png"  runat="server" style="height: 16px" />
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                     <br/>
                      <br/>
                    <asp:SqlDataSource ID="SDS_Plano_curricular" runat="server" OnSelected="SqlDataSource1_Selected"
                        ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" SelectCommand="Select PlanCodigo,PlanCurso, CurDescricao,DisCodigo,
 (REPLICATE('0', 6 - LEN( CAST(DisCodigo AS Varchar))) +  CAST(DisCodigo AS Varchar)  + ' - ' +  DisDescricao) as DisDescricao,
  PlcCargaHoraria, PlcOrdemDisciplina,
 (Case when PlcTipoAvaliacao = 'N' then 'Nota' when PlcTipoAvaliacao = 'C' then 'Conceito' end) 
 as PlcTipoAvaliacao, PlcNumeroAulas from CA_PlanoCurricular INNER JOIN  dbo.CA_Planos on  PlanCodigo = PlcCodigoPlano inner join 
CA_Cursos on  CurCodigo = PlanCurso  INNER JOIN CA_Disciplinas on DisCodigo = PlcDisciplina 
 WHERE PlanCurso = @PlcCurso AND PlanCodigo = @PlcCodigo Order by PlcOrdemDisciplina, PlanCodigo,PlanCurso,CurDescricao">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="TB_codigoCurso" Name="PlcCurso" PropertyName="Text" />
                             <asp:ControlParameter ControlID="DD_plano" Name="PlcCodigo" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                      <asp:HiddenField ID="HFRowCount" runat="server" />
                       <asp:HiddenField runat="server" ID="HFConfirma" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
        </asp:View>
         <asp:View ID="View2" runat="server">
         <div style="height: 400px;">
         <br/>
         <br/>
         <br/>
           <div class="centralizar" style="width: 70%;">
                <table class="FundoPainel Table centralizar">
                <tr>
                    <td class="corfonte cortitulo titulo" colspan="4">
                        Plano Curricular - Cadastro
                    </td>
                    </tr>
                    <tr>
                       <td class="Tam05 fonteTab">&nbsp;</td>
                        <td class="Tam20 fonteTab">
                            &nbsp;&nbsp; Plano Curricular</td>
                        <td class="Tam20 fonteTab" style="text-align: left;">&nbsp;</td>
                        <td class="Tam25 fonteTab" style="text-align: left;">&nbsp;</td>
                    </tr>
                     <tr>
                         <td class="Tam05">
                             </td>
                         <td class="fonteTab" colspan="3">
                            &nbsp;&nbsp;  
                             <asp:DropDownList ID="dd_plano_curricular" runat="server" 
                                 CssClass="fonteTexto" DataSourceID="SDS_planos"   OnDataBound="IndiceZero"
                                 style="height: 18px; width:75%;" DataTextField="PlanDescricao" 
                                 DataValueField="PlanCodigo">
                             </asp:DropDownList>
                         </td>
                    </tr>
                      <tr>
                    <td class="Tam05 fonteTab">&nbsp;</td>
                        <td class="Tam15 fonteTab">
                            &nbsp;&nbsp; Disciplina</td>
                        <td class="Tam15 fonteTab" style="text-align: left;">&nbsp;</td>
                        <td class="Tam25 fonteTab" style="text-align: left;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="Tam05">
                            &nbsp;</td>
                        <td class="fonteTab" colspan="3">
                        &nbsp;&nbsp;  
                            <asp:DropDownList ID="DD_disciplina" runat="server" CssClass="fonteTexto" 
                                DataSourceID="SDS_Disciplinas" DataTextField="DisDescricao" 
                                DataValueField="DisCodigo" OnDataBound="IndiceZero" 
                                style="height: 18px; width:75%;">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05"></td>
                        <td class="auto-style1" colspan="3">&nbsp;&nbsp;&nbsp;Educador</td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;</td>
                        <td class="fonteTab" colspan="3">&nbsp;&nbsp;
                            <asp:DropDownList ID="DD_Educador" runat="server" CssClass="fonteTexto" OnDataBound="IndiceZero" style="height: 18px; width:75%;" DataSourceID="SDS_Educadores" DataTextField="EducNome" DataValueField="EducCodigo">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05 fonteTab">
                            &nbsp;</td>
                        <td class="Tam15 fonteTab">
                            &nbsp;&nbsp;&nbsp; Ordem da Disciplina</td>
                        <td class="Tam15 fonteTab" style="text-align: left;">
                            &nbsp;&nbsp; Tipo de Avaliação
                        </td>
                        <td class="Tam40 fonteTab" style="text-align: left;">
                            &nbsp;&nbsp; Gera Cronograma Automaticamente</td>
                    </tr>
                     <tr>
                       <td class="Tam05 fonteTab">
                           &nbsp;</td>
                         <td class="Tam12 fonteTab">
                             &nbsp;&nbsp;
                             <asp:TextBox ID="TB_orderm_disciplina" runat="server" CssClass="fonteTexto" 
                                 MaxLength="2" style="height: 13px; width:65%;"></asp:TextBox>
                         </td>
                         <td class="Tam12 fonteTab" style="text-align: left;">
                              &nbsp;&nbsp; <asp:DropDownList ID="DD_tipo_avaliacao" runat="server"  CssClass="fonteTexto"
                                 style="height: 18px; width:65%;">
                                 <asp:ListItem Value="">Selecione</asp:ListItem>
                                  <asp:ListItem Value="N">Nota</asp:ListItem>
                                   <asp:ListItem Value="C">Conceito</asp:ListItem>
                             </asp:DropDownList>
                             </td>
                         <td class="Tam40 fonteTab" style="text-align: left;">
                            &nbsp;&nbsp; 
                             <asp:DropDownList ID="DD_gera_cronograma" runat="server" CssClass="fonteTexto" 
                                 style="height: 18px; width:30%;">
                                 <asp:ListItem Value="">Selecione</asp:ListItem>
                                 <asp:ListItem Value="S">Sim</asp:ListItem>
                                 <asp:ListItem Value="N">Não</asp:ListItem>
                             </asp:DropDownList>
                             </td>
                       
                    </tr>
                     <tr>
                        <td class="Tam05 fonteTab">&nbsp;</td>
                         <td class="Tam12 fonteTab">
                             &nbsp;&nbsp; Número de Aulas</td>
                         <td class="Tam12 fonteTab" style="text-align: left;">  &nbsp; Carga Horária</td>
                         <td class="Tam40 fonteTab" style="text-align: left;">&nbsp;&nbsp;</td>
                    </tr>
                     <tr>
                          <td class="Tam05 fonteTab">
                              &nbsp;</td>
                          <td class="Tam12 fonteTab">
                              &nbsp;&nbsp;
                              <asp:TextBox ID="TB_aula_semanal" runat="server" CssClass="fonteTexto" 
                                   style="height: 13px; width:65%;"></asp:TextBox>
                          </td>
                        <td class="Tam12 fonteTab">
                            &nbsp;&nbsp; <asp:TextBox ID="TB_carga_horaria" runat="server" CssClass="fonteTexto"
                                 style="height: 13px; width:65%;"></asp:TextBox>
                         </td>
                         <td class="Tam40 fonteTab" style="text-align: left;">
                            &nbsp;&nbsp;  
                             </td>
                    </tr>
                </table>
            </div>
            <br />
                 <div class="controls" style="width:75%; margin: 0 auto; text-align: center;">
                        <asp:Button ID="BTaltera" runat="server" OnClick="BTaltera_Click" CssClass="btn_novo" Text="Confirmar" />
                         </div>
                                     
                            <asp:SqlDataSource ID="SDS_Cursos" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                                SelectCommand="select CurCodigo, CurDescricao from  CA_Cursos">
                            </asp:SqlDataSource>
                             <asp:SqlDataSource ID="SDS_Disciplinas" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                                SelectCommand="select DisCodigo, DisDescricao from CA_Disciplinas">
                            </asp:SqlDataSource>
                              <asp:SqlDataSource ID="SDS_Educadores" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                                SelectCommand="select EducCodigo, EducNome from CA_Educadores order by EducNome">
                            </asp:SqlDataSource>
                             <asp:SqlDataSource ID="SDS_planos" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                                SelectCommand="select PlanCodigo, PlanDescricao from CA_Planos">
                            </asp:SqlDataSource>
                            </div>
        </asp:View>
         <asp:View ID="View3" runat="server">
            <div class="centralizar">
                <iframe id="IFrame1" class="VisualFrame" name="IFrame1" src="Visualizador.aspx">
                </iframe>
            </div>
        </asp:View>
    </asp:MultiView>
</div>
</asp:Content>