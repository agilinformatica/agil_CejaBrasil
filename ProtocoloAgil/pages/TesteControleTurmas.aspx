<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TesteControleTurmas.aspx.cs" Inherits="ProtocoloAgil.pages.TesteControleTurmas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100px;
            height: 42px;
        }
        .auto-style2 {
            width: 250px;
            height: 42px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ScriptMode="Release">
        </asp:ToolkitScriptManager>
    <div>
        <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
            <div style="margin:10px 0px 30px 0px; align-content:center;"> 
                <asp:Button ID="btnNovo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="btnNovo_Click" />
                <asp:Button ID="btnAtualizar" runat="server" CssClass="btn_controls" Text="Atualizar" OnClick="btnAtualizar_Click" />
                <asp:Button ID="btnRemover" runat="server" CssClass="btn_controls" Text="Remover" OnClick="btnRemover_Click" />
            </div>
        </asp:Panel>
        <div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewLista" runat="server">
                        <asp:GridView  ID="grdLista" runat="server" AllowPaging="True" float="left" AllowSorting="True" AutoGenerateColumns="False" 
                            PageSize="12" Style="width: 60%; margin: 0 auto" HorizontalAlign="Center" BorderWidth="1px"
                            CellPadding="4" OnRowCommand="grdLista_RowCommand" BackColor="White" BorderColor="#3366CC" BorderStyle="None">
                            <Columns>
                                <asp:BoundField DataField="TurCodigo" HeaderText="Cód" >
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TurCurso" HeaderText="Curso" >
                                    <ItemStyle Width="150px" HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TurNome" HeaderText="Nome" >
                                    <ItemStyle Width="280px" HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TurDiaSemana" HeaderText="Dia" >
                                    <ItemStyle Width="150px" HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TurInicio" HeaderText="Inicio" >
                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Termino" HeaderText="Termino" >
                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Button" CommandName="EDITAR" Text="  " HeaderText="Editar">
                                    <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                </asp:ButtonField>
                            </Columns>

                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" ForeColor="#003399" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />

                        </asp:GridView>
            </asp:View>
            <asp:View ID="ViewNovo" runat="server">
                <table runat="server">
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Curso" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtCurso" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Nome" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtNome" runat="server" Width="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Dia da Semana" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtDia" runat="server" Width="240px" TextMode="Number"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Inicio" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtDataIni" runat="server" Width="240px" TextMode="DateTime"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Término" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtDataTer" runat="server" Width="240px" TextMode="DateTime"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px; align-content:center;">
                            <asp:Button ID="btnSalvarNovo" text="Salvar" runat="server" Width="80%" OnClick="btnSalvarNovo_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewEditar" runat="server">
                <table runat="server">
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Curso" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtEditCurso" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Nome" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtEditNome" runat="server" Width="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label runat="server" Text="Dia da Semana" Width="100%"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtEditDia" runat="server" Width="240px" TextMode="Number"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Inicio" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtEditInicio" runat="server" Width="240px" TextMode="DateTime"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;">
                            <asp:Label runat="server" Text="Término" Width="100%"></asp:Label>
                        </td>
                        <td style="width:250px;">
                            <asp:TextBox ID="txtEditTermino" runat="server" Width="240px" TextMode="DateTime"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px; align-content:center;">
                            <asp:Button ID="btnSalvarEdit" text="Salvar" runat="server" Width="80%" OnClick="btnSalvarEdit_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
        </div>
    </div>
    </form>
</body>
</html>
