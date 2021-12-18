<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="ControleAcesso.aspx.cs" Inherits="ProtocoloAgil.pages.ControleAcesso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Configurações > <span>Designar Funções</span></p>
    </div>
    <div class="formatoTela_01">

        <div class="controls">
            <div style="float: left; margin-left: 10px;">
                <span class="fonteTab">Perfil Usuário: </span>&nbsp; 
                   &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DD_tipo" DataTextField="PerfDescricao" AutoPostBack="true"  DataValueField="PerfCodigo" runat="server" CssClass="fonteTexto" Height="18px" Width="60%">
                            </asp:DropDownList>
            </div>
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="centralizar" style="width: 90%; height: 500px;">
                    <div style="float: left; width: 47%; margin-left: 20px; vertical-align: top;">
                        <div class="text_titulo" style="margin: 0 auto;">
                            <h1>
                                       Funções Não Autorizadas</h1>
                            <br />
                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                                       AutoGenerateColumns="False" CssClass="list_results" DataKeyNames="funscodigo" 
                                       DataSourceID="SDSfuncoes" HorizontalAlign="Center" 
                                       ondatabound="GridView_DataBound" 
                                       OnSelectedIndexChanged="GridView3_SelectedIndexChanged" 
                                       Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="funscodigo" HeaderText="Código" 
                                               InsertVisible="False" ReadOnly="True" SortExpression="funscodigo">
                                    <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="funsdescricao" HeaderText="Função" 
                                               SortExpression="funsdescricao">
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Som. Leitura">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Image" HeaderText="Adicionar" 
                                               SelectImageUrl="~/images/icon_edit.png" ShowSelectButton="True">
                                    <HeaderStyle Width="15%" />
                                    </asp:CommandField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" 
                                           LastPageImageUrl="~/images/seta_ultimo.jpg" LastPageText="" NextPageText="Próximo" 
                                    PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div style="float: right; width: 47%; margin-right: 20px; vertical-align: top;">
                        <div class="text_titulo">
                            <h1>
                                       Permissões do Usuário</h1>
                            <br />
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                                       AutoGenerateColumns="False" CssClass="list_results" DataKeyNames="funscodigo" 
                                       DataSourceID="autorizadas" HorizontalAlign="Center" 
                                       ondatabound="GridView_DataBound" 
                                       OnSelectedIndexChanged="GridView2_SelectedIndexChanged" 
                                       Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="funscodigo" HeaderText="Código" 
                                               InsertVisible="False" ReadOnly="True" SortExpression="funscodigo">
                                    <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="funsdescricao" HeaderText="Função" 
                                               SortExpression="funsdescricao">
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AutFTipoAut" HeaderText="Som. Leitura" 
                                        SortExpression="AutFTipoAut" >
                                         <HeaderStyle Width="20%" />
                                          </asp:BoundField>
                                    <asp:CommandField ButtonType="Image" HeaderText="Remover" 
                                               SelectImageUrl="~/images/icon_remove.png" ShowSelectButton="True">
                                    <HeaderStyle Width="15%" />
                                    </asp:CommandField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" 
                                           LastPageImageUrl="~/images/seta_ultimo.jpg" LastPageText="" NextPageText="Próximo" 
                                    PreviousPageText="Anterior" />
                                <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource ID="SDSfuncoes" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                           onselected="SqlDataSource1_Selected" 
                    SelectCommand="select FunSDescricao,FunSCodigo, FunSNomeForm FROM CA_funcoesSistema
                            where FunSNomeForm NOT IN  (
                            select FunSNomeForm from CA_AutorizacaoUsuario inner join CA_funcoesSistema on 
                            AutFFuncao = FunSNomeForm
                            WHERE CA_AutorizacaoUsuario.AutFUsuario = @codigo)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DD_tipo" Name="codigo" 
                                   PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SDSfuncoesGeral" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                           onselected="SqlDataSource1_Selected" SelectCommand="
                        SELECT FunSCodigo, FunSDescricao, FunSNomeForm
                            FROM  CA_funcoesSistema WHERE (FunSNomeForm NOT IN
                            (SELECT f.FunSNomeForm
                            FROM CA_funcoesSistema AS f INNER JOIN
                            CA_AutorizacaoUsuario ON f.FunSNomeForm = CA_AutorizacaoUsuario.AutFFuncao
                            )) AND (FunSNomeForm LIKE 'L%')"></asp:SqlDataSource>
                <asp:SqlDataSource ID="autorizadas" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>" 
                           onselected="SqlDataSource1_Selected" 
                    SelectCommand="
                         SELECT CA_funcoesSistema.FunSCodigo, CA_funcoesSistema.FunSDescricao, CA_funcoesSistema.FunSNomeForm, 
                        CA_AutorizacaoUsuario.AutFUsuario,(case when CA_AutorizacaoUsuario.AutFTipoAut = 'A' then 'Não' when CA_AutorizacaoUsuario.AutFTipoAut ='S' then 'Sim' end) as AutFTipoAut
                        FROM CA_funcoesSistema INNER JOIN CA_AutorizacaoUsuario ON CA_funcoesSistema.FunSNomeForm = 
                        CA_AutorizacaoUsuario.AutFFuncao
                        WHERE (CA_AutorizacaoUsuario.AutFUsuario = @codigo )">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DD_tipo" Name="codigo" 
                                   PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="HFRowCount" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DD_tipo" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

        </div>
</asp:Content>
