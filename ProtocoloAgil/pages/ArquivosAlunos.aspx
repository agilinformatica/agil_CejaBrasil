<%@ Page Title="" Language="C#" MasterPageFile="~/MaAluno.Master" AutoEventWireup="true"
    CodeBehind="ArquivosAlunos.aspx.cs" Inherits="ProtocoloAgil.pages.ArquivosAlunos" %>

<%@ MasterType VirtualPath="~/MaAluno.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
.upload-wrapper {cursor:pointer; display:inline-block; position:absolute; overflow:hidden;}
.upload-file {cursor:pointer; position:absolute; filter:alpha(opacity=1); -moz-opacity:0.01; opacity:0.01; height: 28px; width: 28px;  }
.upload-button {cursor:pointer; height:25px; width:25px; }


</style>
<script type="text/javascript">
    function NomeArquivo(fuparquivo, event) {
        var files = event.target.files;
        var textbox = document.getElementById('<%= tb_Caminho_arquivo.ClientID %>');
        for (var i = 0, f; f = files[i]; i++) {
            textbox.value = f.name;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Arquivos > <span>Gerenciamento de Arquivos</span></p>
    </div>
    <div class="controls">
        <div style="float: left">
            <asp:Button ID="Baixar" runat="server" CssClass="btn_search" Text="Baixar Arquivos"
                ToolTip="Pesquisar" OnClick="Baixar_Click" />
            <asp:Button ID="Enviar" runat="server" CssClass="btn_search" Text="Enviar Arquivos"
                ToolTip="Pesquisar" OnClick="Enviar_Click" />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <asp:UpdatePanel ID="updArquivo" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
   
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list_results"
                        DataSourceID="SDSturmasdoaluno" HorizontalAlign="Center" Style="margin: auto"
                        Width="800px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="TurNome" HeaderText="Turma" ReadOnly="True" SortExpression="TurNome">
                                <HeaderStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina" SortExpression="DisDescricao">
                                <HeaderStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EducNome" HeaderText="Professor" SortExpression="EducNome">
                                <HeaderStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Arquivos" SelectImageUrl="~/images/icon_edit.png"
                                ShowSelectButton="True">
                                <HeaderStyle  HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                         </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <br />
                    <br />
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="list_results"
                        HorizontalAlign="Center" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" Style="margin: auto"
                        Width="800px" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Arquivo">
                                <HeaderStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Download" SelectImageUrl="~/images/download.jpg"
                                ShowSelectButton="True">
                                <HeaderStyle  HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                         </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="View2" runat="server">
          <asp:UpdatePanel ID="updArquivo0" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnEnviar" />
                </Triggers>
                <ContentTemplate>
                    <div class="controls">
                        <div class="fonteTab" style="float: right; margin-left: 20px;" visible="false">
                            <asp:Label ID="Label2" runat="server" Text="Turma:" />
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="DDturmas" runat="server" AutoPostBack="True" 
                                CssClass="fonteTexto" DataSourceID="SDSturmas" DataTextField="TurNome" 
                                DataValueField="TurCodigo" Height="20px" OnDataBound="IndiceZero" 
                                OnSelectedIndexChanged="DDturmas_SelectedIndexChanged" ViewStateMode="Enabled" 
                                Width="150px">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:Label ID="LBTituloCampo" runat="server" Text="Disciplina:" />
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="DDDisNome" runat="server" CssClass="fonteTexto" 
                                DataSourceID="SDSdisciplinas" DataTextField="DisDescricao" 
                                DataValueField="EducCodigo" Height="20px" OnDataBound="IndiceZero" ViewStateMode="Enabled" 
                                Width="350px">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            </div>
                    </div>
                    <div class="centralizar" style="width: 800px;">
                        <span class="fonteTab" style="float: left;"><b>Importante:</b> Recomendamos que 
                        os arquivos enviados aos professores não ultrapassem o limite de 5MB. </span>
                    </div>
                    <br/>
                    <br/>
                    <div class="centralizar">
                        <span class="fonteTab">Selecione o arquivo: </span>
                        <asp:TextBox ID="tb_Caminho_arquivo" runat="server" CssClass="fonteTexto" 
                            Height="15px" Width="300px"></asp:TextBox>
                        <div class="upload-wrapper">
                            <asp:FileUpload ID="fupArquivo" runat="server" CssClass="upload-file" 
                                onchange="javascript:NomeArquivo(this,event);" SkinID="fup" />
                            <img class="upload-button" alt="" title="" src="../images/lupa.png"/>
                        </div>
                        <asp:Button ID="btnEnviar" runat="server" CssClass="btn_novo" 
                            OnClick="btnSend_Click" style="margin-left: 30px;" Text="Enviar Arquivo" 
                            ToolTip="envia o arquivo para turma." />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>
    <br />
    <asp:SqlDataSource ID="SDSturmasdoaluno" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="
        SELECT distinct EducNome, DisDescricao,TurNome  from  View_CA_DisciplinasProfessores
inner join dbo.CA_DisciplinasAprendiz on  DiaDisciplinaProf = View_CA_DisciplinasProfessores.DpOrdem 
inner join CA_AlocacaoAprendiz on CA_AlocacaoAprendiz.ALATurma = View_CA_DisciplinasProfessores.TurCodigo
where ALAAprendiz = @matricula and ALAStatus = 'A'">
        <SelectParameters>
            <asp:SessionParameter Name="matricula" SessionField="matricula" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSdisciplinas" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="
        SELECT distinct EducCodigo, DisDescricao from  View_CA_DisciplinasProfessores
inner join dbo.CA_DisciplinasAprendiz on  DiaDisciplinaProf = View_CA_DisciplinasProfessores.DpOrdem 
inner join CA_AlocacaoAprendiz on CA_AlocacaoAprendiz.ALATurma = View_CA_DisciplinasProfessores.TurCodigo
where ALAAprendiz =@matricula and TurCodigo =@sala">
        <SelectParameters>
            <asp:SessionParameter Name="matricula" SessionField="matricula" DefaultValue="" />
            <asp:ControlParameter ControlID="DDturmas" Name="sala" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSturmas" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="SELECT distinct TurNome, TurCodigo from  View_CA_DisciplinasProfessores
inner join dbo.CA_DisciplinasAprendiz on  DiaDisciplinaProf = View_CA_DisciplinasProfessores.DpOrdem 
inner join CA_AlocacaoAprendiz on CA_AlocacaoAprendiz.ALATurma = View_CA_DisciplinasProfessores.TurCodigo
where ALAAprendiz = @matricula">
        <SelectParameters>
            <asp:SessionParameter Name="matricula" SessionField="matricula" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
