<%@ Page Title="" Language="C#" MasterPageFile="~/MaEducador.Master" AutoEventWireup="true"
    CodeBehind="Arquivos.aspx.cs" Inherits="ProtocoloAgil.pages.Arquivos" %>

<%@ MasterType VirtualPath="~/MaEducador.Master" %>
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
            <asp:Button ID="Baixar" runat="server" CssClass="btn_search" Text="Gerenciar Arquivos"
                ToolTip="Pesquisar" OnClick="Baixar_Click" />
            <asp:Button ID="Enviar" runat="server" CssClass="btn_search" Text="Recebidos" ToolTip="Pesquisar"
                OnClick="Enviar_Click" />
        </div>
    </div>
    <asp:MultiView runat="server" ID="MultiView1">
        <asp:View runat="server" ID="View1">
        <div class="formatoTela_02">
            <asp:UpdatePanel ID="updArquivo" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnEnviar" />
                </Triggers>
                <ContentTemplate>
                    <div class="controls">
                        <div class="fonteTab" style="float: right; margin-left: 20px;" visible="false">
                            <asp:Label runat="server" ID="Label1" Text="Turma:" />&nbsp;&nbsp;
                            <asp:DropDownList ID="DDturmas" runat="server" CssClass="fonteTexto" Height="20px"
                                OnDataBound="IndiceZero" Width="150px" AutoPostBack="True" DataSourceID="SDSturmas"
                                DataTextField="TurNome" DataValueField="TurCodigo" OnSelectedIndexChanged="DDturmas_SelectedIndexChanged"
                                ViewStateMode="Enabled">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:Label runat="server" ID="LBTituloCampo" Text="Disciplina:" />&nbsp;&nbsp;
                            <asp:DropDownList ID="DDDisNome" runat="server" CssClass="fonteTexto" Height="20px"
                                OnDataBound="IndiceZero" Width="350px" DataSourceID="SDSdisciplinas" DataTextField="DisDescricao"
                                DataValueField="DPDisciplina" ViewStateMode="Enabled" OnSelectedIndexChanged="DDDisNome_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:Button ID="BTsearch" runat="server" CssClass="btn_search" Text="Pesquisar" ToolTip="Pesquisar"
                                OnClick="BTsearch_Click" />
                        </div>
                    </div>
                    <div class="centralizar" style="width: 800px;">
                        <span class="fonteTab"  style="float: left;"> <b>Importante:</b> Recomendamos que os arquivos enviados aos alunos não ultrapassem o limite de 5MB. </span>
                    </div>
                    <br/>
                     <br/>
                    <div class="centralizar">
                            <span class="fonteTab">Selecione o arquivo: </span>
                            <asp:TextBox runat="server" ID="tb_Caminho_arquivo" CssClass="fonteTexto" Width="300px" Height="15px"></asp:TextBox>
                            <div class="upload-wrapper">
                                 <asp:FileUpload ID="fupArquivo" SkinID="fup" CssClass="upload-file"  runat="server"  onchange="javascript:NomeArquivo(this,event);"/>
                                 <img class="upload-button" alt="" title="" src="../images/lupa.png"/>
                            </div>

                      
                            <asp:Button ID="btnEnviar" runat="server" CssClass="btn_novo" style="margin-left: 30px;" Text="Enviar Arquivo" ToolTip="envia o arquivo para turma."
                                OnClick="btnSend_Click" /></div>
                        
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updArquivoUpload" UpdateMode="Conditional" runat="server">
                <ContentTemplate>

                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
                        Width="800px" CssClass="list_results" Style="margin: auto" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        OnRowCommand="GridView1_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Nome">
                                <HeaderStyle HorizontalAlign="Center" Width="85%" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Download" SelectText="" ShowSelectButton="True"
                                SelectImageUrl="~/images/download.jpg">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:ButtonField ButtonType="Image" CommandName="Deletar" ImageUrl="~/images/icon_remove.png"
                                Text="Button" HeaderText="Excluir">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>
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
              </div>
        </asp:View>
        <asp:View runat="server" ID="View2">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="list_results"
                HorizontalAlign="Center" OnRowCommand="GridView2_RowCommand" Style="margin: auto"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="800px" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Matricula" HeaderText="Matrícula">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>

                        <asp:BoundField DataField="Nome" HeaderText="Nome">
                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Turma" HeaderText="Turma">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Arquivo" HeaderText="Nome do Arquivo">
                        <HeaderStyle HorizontalAlign="Center" Width="35%" />
                    </asp:BoundField>
                    <asp:CommandField ButtonType="Image" HeaderText="Download" SelectImageUrl="~/images/download.jpg"
                        SelectText="" ShowSelectButton="True">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                    <asp:ButtonField ButtonType="Image" CommandName="Deletar" HeaderText="Excluir" ImageUrl="~/images/icon_remove.png"
                        Text="Button">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
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
        </asp:View>
    </asp:MultiView>
    <asp:SqlDataSource ID="SDSdisciplinas" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="SELECT  DisDescricao,DPDisciplina from dbo.View_CA_DisciplinasProfessores WHERE  (EducCodigo = @professor) AND (TurCodigo = @sala)">
        <SelectParameters>
            <asp:SessionParameter Name="professor" SessionField="codigo" />
            <asp:ControlParameter ControlID="DDturmas" Name="sala" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSturmas" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="SELECT TurNome,TurCodigo from dbo.View_CA_DisciplinasProfessores WHERE  (EducCodigo = @professor)">
        <SelectParameters>
            <asp:SessionParameter Name="professor" SessionField="codigo" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
