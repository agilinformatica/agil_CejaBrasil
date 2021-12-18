<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="PesquisaCandidatos.aspx.cs" Inherits="ProtocoloAgil.pages.PesquisaCandidatos" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50731.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" href="../Styles/jquery-ui.css" rel="stylesheet" />
    <link type="text/css" href="../Styles/jquery.multiselect.css" rel="stylesheet" />
    <link type="text/css" href="../Styles/style2.css" rel="stylesheet" />

    <link type="text/css" href="../Styles/style.css" rel="stylesheet" />
    <script src="../Scripts/Mascara.js" type="text/javascript"></script>



</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.multiselect.js"></script>
    <script src="../Scripts/Mascara.js" type="text/javascript"></script>




    <script type="text/javascript">

        $(document).ready(function () {

            $(".multiselect").multiselect();
        });

        function popup(url, width, height) {
            $("#lightbox").css("display", "inline");
            var x = (screen.width - eval(width)) / 2;
            var y = (screen.height - eval(height)) / 2;
            var newwindow = window.open(url, "Cadastro", "status=no, scrollbar=1, width= " + width + ",height=  " + height + ",resizable = 1,top= " + y + ",left=" + x + "");
            if (window.focus) { newwindow.focus(); }
        }

    </script>

    <style type="text/css">
        .scrollingControlContainer {
            overflow-x: hidden;
            overflow-y: scroll;
            height: 90px;
        }

        .scrollingCheckBoxListRegiao {
            border: 1px #808080 solid;
            height: 100px;
            width: 100%;
        }





        .scrollingCheckBoxListSituacao {
            border: 1px #808080 solid;
            height: 100px;
            width: 100%;
            position: relative;
        }




        .scrollingCheckBoxListEscolaridade {
            border: 1px #808080 solid;
            height: 100px;
            width: 100%;
            position: relative;
        }

        .auto-style1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            font-weight: bold;
            color: #5E5E5E;
            margin-left: 0px;
            width: 24%;
        }

        .auto-style2 {
            width: 24%;
        }

        .posicaoGrid {
            top: -90px;
        }

        .escondeColuna {
            display: none;
        }

        .auto-style4 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            font-weight: bold;
            color: #5E5E5E;
            margin-left: 0px;
            width: 159px;
        }

        .auto-style5 {
            width: 159px;
        }

        .auto-style6 {
            line-height: 8px;
            width: 159px;
        }

        .auto-style7 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            font-weight: bold;
            color: #5E5E5E;
            margin-left: 0px;
            width: 19px;
        }

        .auto-style8 {
            width: 19px;
        }

        .auto-style9 {
            line-height: 8px;
            width: 19px;
        }

        .Table {
            width: 800px;
            border: 1px black solid;
            border-collapse: collapse;
            margin: 0 auto;
            background: #dfdfdf;
        }

            .Table th {
                border: 1px black solid;
                font: 10pt Arial,sans-serif;
                text-align: center;
                font-weight: bold;
                color: #ffffff;
                background: #00599c;
            }

            .Table td {
                border: 1px black solid;
                font: 9pt Arial,sans-serif;
                text-align: left;
                text-align: center;
                font-weight: bold;
                color: #787878;
            }


        .hiddencol {
            display: none;
        }

        .foto {
            width: 14%;
            height: 160px;
            margin-left: 1%;
            margin-top: 5px;
        }

        .fotoLabel {
            margin-left: 20%;
            margin-top: -80px;
        }
    </style>




    <asp:SqlDataSource ID="SDSBairro" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="select RegBairro, DescBairro from CA_Bairros"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SDSEscolaridade" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="select GreCodigo, GreDescricao from CA_GrauEscolaridade order by GreDescricao"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SDSRegiao" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="select CodRegiao, DescRegiao from Ca_Regioes"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SDSParceiro" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="select ParUniCodigo, ParUniDescricao from CA_ParceirosUnidade order by ParUniDescricao"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SDSStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="select Ste_Codigo, Ste_Descricao from CA_StatusEncaminhamento order by Ste_Descricao"></asp:SqlDataSource>

    <asp:SqlDataSource ID="Situacao" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="Select StaCodigo, StaDescricao From CA_SituacaoAprendiz"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SDSMunicipio" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
        SelectCommand="Select MunICodigo, MunIDescricao from MA_Municipios where MunIEstado= 'SP'"></asp:SqlDataSource>






    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">





        <asp:View ID="View1" runat="server">
            <div class="breadcrumb">
                <p style="text-align: left;">
                    Jovens > <span>Pesquisa Candidatos</span>
                </p>
            </div>
            <br />

            <div class="FundoColoridoPesquisa" style="height: 195px; width: 90%; margin: 10px 1% 10px 1%; border: 1px solid #7f7f7f;">
                <table>
                    <tr>
                         <td colspan="2">
                            <span class="fonteTab"><strong>Nome:</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" Height="20px" Width="400px"
                                        onclick="this.value='';document.getElementById('ctl00_ContentPlaceHolder1_TBCodigo').value = '';"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span class="fonteTab"><strong>Status do Jovem:</strong></span>
                        </td>
                        <td>
                            <span class="fonteTab"><strong>Estuda Atualmente:</strong></span>
                        </td>
                        <td>
                            <span class="fonteTab"><strong>Sexo:</strong></span>
                        </td>

                        <td>
                            <span class="fonteTab"><strong>Turno:</strong></span>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">

                            <asp:DropDownList ID="DDSituacao" runat="server" CssClass="fonteTexto"
                                DataTextField="StaDescricao" DataValueField="StaCodigo"
                                Height="18px" OnDataBound="IndiceZero" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="80%">
                            </asp:DropDownList>
                        </td>

                        <td class="auto-style4">
                            <asp:DropDownList ID="DDEstudaAtualmente" runat="server" CssClass="fonteTexto" Height="18px" onkeydown="ModifyEnterKeyPressAsTab();" Width="85px">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">Não</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="auto-style5">
                            <asp:DropDownList ID="DDSexo" runat="server" CssClass="fonteTexto" Height="18px" onkeydown="ModifyEnterKeyPressAsTab();" Width="85px">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="DDTurnoEscolar" runat="server" CssClass="fonteTexto" Height="18px" onkeydown="ModifyEnterKeyPressAsTab();" Width="85px">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="M">Manhã</asp:ListItem>
                                <asp:ListItem Value="T">Tarde</asp:ListItem>
                                <asp:ListItem Value="N">Noite</asp:ListItem>
                            </asp:DropDownList>
                        </td>



                    </tr>
                    <tr>
                        <td colspan="2">
                            <span class="fonteTab"><strong>Município</strong></span>
                        </td>
                        <td colspan="2">
                            <span class="fonteTab"><strong>Idade Entre:</strong></span>

                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">

                            <asp:DropDownList ID="DDMunicipio" runat="server" CssClass="fonteTexto"
                                DataTextField="MunIDescricao" DataValueField="MunIDescricao" OnDataBound="IndiceZero"
                                Height="18px" DataSourceID="SDSMunicipio" onkeydown="ModifyEnterKeyPressAsTab();"
                                Width="80%">
                            </asp:DropDownList>

                        </td>
                        <td colspan="2">



                            <asp:TextBox Width="70px" ID="txtIdadeInicio" runat="server" OnTextChanged="txtIdadeInicio_TextChanged" onkeyup="formataInteiro(this, event);" CssClass="fonteTexto"
                                Height="16px" onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="3"></asp:TextBox>


                            <span class="idadeEntrePesquisa"><strong>a</strong></span>

                            <asp:TextBox ID="txtIdadeTermino" Width="70px" runat="server" CssClass="fonteTexto"
                                Height="16px" onkeydown="ModifyEnterKeyPressAsTab();" onkeyup="formataInteiro(this, event);" MaxLength="3"></asp:TextBox>
                        </td>



                        <td colspan="2">
                            <asp:Button runat="server" CssClass="btn_novo" Text="Pesquisar" ID="btnPesquisar" OnClick="btnPesquisar_Click" />
                        </td>

                    </tr>
                    <tr>
                        <td><span class="fonteTab"><strong>Turma Capacitação:</strong></span></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DDTurmaCapacitacao" runat="server" CssClass="fonteTexto"
                            DataTextField="TurNome" DataValueField="TurCodigo" Height="16px"
                            Width="150px">
                        </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="text_titulo">
                <h1>Pesquisa de Candidatos</h1>
            </div>

            <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gridAprendiz" runat="server" AllowPaging="True" AutoGenerateColumns="False" CaptionAlign="Top" CssClass="grid_Aluno" HorizontalAlign="Center" OnPageIndexChanging="gridAprendiz_PageIndexChanging" DataKeyNames="Apr_Nome" OnRowCommand="gridAprendiz_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="Apr_Codigo" HeaderText="Cód." InsertVisible="False" ReadOnly="True" SortExpression="Apr_Codigo">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" SortExpression="Apr_Nome">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>

                             <asp:BoundField DataField="Apr_Cidade" HeaderText="Cidade" SortExpression="Apr_Cidade">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Apr_Bairro" HeaderText="Bairro" SortExpression="Apr_Bairro">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Apr_Telefone" HeaderText="Telefone" NullDisplayText="(__) ____-____" SortExpression="Apr_Telefone">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Apr_Sexo" HeaderText="Sexo" SortExpression="Apr_Sexo">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StaDescricao" HeaderText="Sit." SortExpression="StaDescricao">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Apr_Idade" HeaderText="Idade" SortExpression="Apr_Idade">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Apr_TurnoEscolar" HeaderText="Turno" SortExpression="Apr_TurnoEscolar">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>

                            
                            <asp:BoundField DataField="Encaminhamento" HeaderText="Enc." SortExpression="Encaminhamento">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" CommandName="Encaminhamentos" HeaderText="Encaminhar" ImageUrl="~/images/icon_reset.jpg" Text="Button">
                                <HeaderStyle HorizontalAlign="Center" Width="2%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>

                            <asp:TemplateField Visible="false" HeaderText="Perfil">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnAtualizaPerfil" ImageUrl="~/images/visualizar.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo")%>'
                                        runat="server" OnClick="btnAtualizaPerfil_Click" Style="height: 16px" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Avaliação">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnAvaliacoes" ImageUrl="~/images/detalhes_icone.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo")%>'
                                        runat="server" OnClick="btnAvaliacoes_Click" Style="height: 16px" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField  HeaderText="Jovem">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnConsultaJovem"  ImageUrl="~/images/visualizar.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Apr_Codigo")%>'
                                        runat="server" OnClick="btnConsultaJovem_Click" Style="height: 16px" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="Grid_Aluno" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageImageUrl="~/images/seta_primeiro.jpg" FirstPageText="" LastPageImageUrl="~/images/seta_ultimo.jpg" LastPageText="" NextPageText="Próximo" PreviousPageText="Anterior" />
                        <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
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


            <div style="width: 14%; height: 160px; margin-top: 13px; float: left; clear: both; background: #E5e5e5">
                <asp:ImageButton runat="server" ID="IMG_foto_aprendiz" Width="100%" Height="100%"
                    Style="margin: auto; z-index: 1; border: none;" />
            </div>


            <table class="FundoPainel Table" style="width: 85%; float: right; clear: both; margin-top: -160px">

                <tr>
                    <th colspan="5" class="cortitulo titulo corfonte" style="font-size: medium; font-weight: bold;">Dados do Aprendiz
                    </th>
                </tr>
                <tr>
                    <td class="fonteTab" style="width: 15%;">Código
                    </td>
                    <td class="fonteTab" colspan="2">Nome do Aprendiz
                    </td>
                    <td class="fonteTab">RG
                    </td>
                </tr>
                <tr>
                    <td class="fonteTab">
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="fonteTexto" Enabled="false"
                            Height="16px" onkeydown="ModifyEnterKeyPressAsTab();" ReadOnly="true" Width="90px"></asp:TextBox>
                    </td>
                    <td class="fonteTab" colspan="2">
                        <asp:TextBox ID="txtNome" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px" MaxLength="60"
                            onblur="if(this.value.length &lt; 10) {alert('Campo Nome do aprendiz vazio ou inválido! Certifique-se que o nome possui mais de 10 caracteres. ');}"
                            onkeydown="ModifyEnterKeyPressAsTab();" Width="90%"></asp:TextBox>
                    </td>
                    <td class="fonteTab">
                        <asp:TextBox ID="txtRG" runat="server" CssClass="fonteTexto" Enabled="false"
                            Height="13px" ReadOnly="true" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fonteTab" style="width: 20%;">CPF:
                    </td>
                    <td class="fonteTab" style="width: 10%;">Data de Nascimento:
                    </td>
                    <td class="fonteTab" style="width: 15%;">Idade
                    </td>
                    <td class="auto-style1">Sexo
                    </td>
                    <td class="fonteTab" style="width: 55%;">E-mail
                    </td>
                </tr>
                <tr style="height: 25px;">
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtCPF" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                            MaxLength="10" Width="90px"></asp:TextBox>

                    </td>

                    <td style="width: 15%;">
                        <asp:TextBox ID="txtDataNascimento" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                            MaxLength="10" Width="90px"></asp:TextBox>
                    </td>

                    <td class="Tam05">
                        <asp:TextBox ID="txtIdade" Enabled="false" runat="server" BackColor="White" Width="30px" Height="16px"
                            BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:TextBox>
                    </td>

                    <td class="auto-style2">
                        <asp:DropDownList ID="DDsexoAlu" Enabled="false" runat="server" CssClass="fonteTexto" Height="18px"
                            onkeydown="ModifyEnterKeyPressAsTab();">
                            <asp:ListItem Value="">Selecione</asp:ListItem>
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                            <asp:ListItem Value="F">Feminino</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtEmail" Enabled="false" runat="server" BackColor="White" Width="246px" Height="16px"
                            BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="fonteTab" style="width: 20%;">Telefone:
                    </td>
                    <td class="fonteTab" style="width: 20%;">Celular:
                    </td>
                    <td class="fonteTab" style="width: 10%;" colspan="2">Endereço:
                    </td>
                    <td class="fonteTab" style="width: 15%;">Bairro
                    </td>


                </tr>
                <tr>

                    <td>
                        <asp:TextBox ID="txtTelefone" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                            onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="14" onkeyup="formataTelefone(this,event);"
                            Width="85%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCelular" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                            onkeydown="ModifyEnterKeyPressAsTab();" MaxLength="14" onkeyup="formataTelefone(this,event);"
                            Width="85%"></asp:TextBox>
                    </td>


                    <td style="width: 15%;" colspan="2">
                        <asp:TextBox ID="txtEndereco" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                            MaxLength="10" Width="95%"></asp:TextBox>
                    </td>

                    <td class="Tam05">
                        <asp:TextBox ID="txtBairro" Enabled="false" runat="server" BackColor="White" Width="95%" Height="16px"
                            BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">N°
                    </td>
                    <td class="fonteTab" style="width: 20%;">Cep:
                    </td>

                    <td class="fonteTab" style="width: 15%;">Série:
                    </td>


                </tr>
                <tr>
                    <td class="Tam05">
                        <asp:TextBox ID="txtNumero" Enabled="false" runat="server" BackColor="White" Width="30px" Height="16px"
                            BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:TextBox>
                    </td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtCep" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                            onkeyup="formataCEP(this,event);" Width="80%"
                            MaxLength="10"></asp:TextBox>

                    </td>

                    <td style="width: 15%;" colspan="2">
                        <asp:TextBox ID="txtSerie" Enabled="false" runat="server" CssClass="fonteTexto" Height="13px"
                            MaxLength="10" Width="95%"></asp:TextBox>
                    </td>
                </tr>



            </table>



            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td colspan="5" style="text-align: right">

                        <asp:Button runat="server" CssClass="btn_novo" Text="Novo Encaminhamento" ID="btnEncaminhamento" OnClick="btnEncaminhamento_Click" />

                        <asp:Button runat="server" CssClass="btn_novo" Text="Voltar" ID="btnVoltar1" OnClick="btnVoltar1_Click" />

                    </td>

                </tr>

                <tr>
                    <th colspan="5" class="cortitulo titulo corfonte" style="font-size: medium; font-weight: bold;">Histórico de Entrevistas
                    </th>
                </tr>



            </table>
            <div class="formatoTela_02">


                <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <br />
                        <asp:GridView ID="gridHistorico" OnRowCommand="gridHistorico_RowCommand" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CaptionAlign="Top" OnPageIndexChanging="gridAprendiz_PageIndexChanging" CssClass="grid_Aluno" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>




                                <asp:BoundField DataField="Enc_Sequencia">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Enc_DataEncaminha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" InsertVisible="False"
                                    ReadOnly="True" SortExpression="Enc_DataEncaminha">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ParUniDescricao" HeaderText="Empresa" InsertVisible="False"
                                    ReadOnly="True" SortExpression="ParUniDescricao">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Ste_Descricao" HeaderText="Status" InsertVisible="False"
                                    ReadOnly="True" SortExpression="Ste_Descricao">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>


                                <asp:BoundField DataField="Enc_Observacoes" HeaderText="Observação" InsertVisible="False"
                                    ReadOnly="True" SortExpression="Enc_Observacoes">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>


                                <asp:ButtonField ButtonType="Image" CommandName="EdicaoHistorico" HeaderText="Editar" ImageUrl="~/images/icon_edit.png" Text="Button">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>

                                  <asp:ButtonField ButtonType="Image" CommandName="ImprimirCarta" HeaderText="Carta Enc." ImageUrl="~/images/cs_print.gif" Text="Button">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>

                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="Grid_Aluno" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </asp:View>


        <asp:View ID="View3" runat="server">

            <table class="FundoPainel Table">
                <tr>
                    <th colspan="5" class="cortitulo titulo corfonte" style="font-size: medium; width: 100%; font-weight: bold;">Novo Encaminhamento
                    </th>
                </tr>

                <tr>
                    <td class="fonteTab">Data:
                    </td>
                    <td class="fonteTab">Empresa:
                    </td>

                    <td class="fonteTab">Status:
                    </td>

                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtData" runat="server" CssClass="fonteTexto" Height="13px"
                            onkeydown="ModifyEnterKeyPressAsTab();" onblur="javascript:if( this.value !=''  &&   !VerificaData(this.value)) this.value ='';"
                            MaxLength="10" onkeyup="formataData(this,event);" Width="90px"></asp:TextBox>
                        <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="TBCalendario_CalendarExtenderPlus"
                            PopupPosition="BottomRight" runat="server" TargetControlID="txtData">
                        </cc2:CalendarExtenderPlus>
                    </td>
                    <td>
                        <asp:DropDownList OnSelectedIndexChanged="DDEmpresa_SelectedIndexChanged" AutoPostBack="true" ID="DDEmpresa" runat="server" CssClass="fonteTexto" OnDataBound="IndiceZero" Width="100%" DataValueField="ParUniCodigo" DataTextField="ParUniDescricao"></asp:DropDownList>
                    </td>

                    <td>
                        <asp:DropDownList ID="DDStatus" Width="100%" Enabled="false" CssClass="fonteTexto" runat="server" DataSourceID="SDSStatus" DataValueField="Ste_Codigo" DataTextField="Ste_Descricao"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fonteTab" colspan="2">Vaga:
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:DropDownList ID="DDVaga" runat="server" AutoPostBack="true" OnTextChanged="DDVaga_TextChanged" CssClass="fonteTexto" Width="60%" DataValueField="ReqId" DataTextField="AreaDescricao"></asp:DropDownList>
                        <asp:Button runat="server" CssClass="btn_novo" Text="Consultar vaga" ID="btnConsultarVaga" OnClick="btnConsultarVaga_Click" />
                    </td>
                </tr>

            </table>
            <table class="FundoPainel Table">
                <tr>
                    <td class="fonteTab" colspan="6">Endereço da Empresa
                    </td>
                </tr>
                <tr>

                    <td class="Tam25 fonteTab" colspan="4">
                        <asp:Label ID="lblEndereco" runat="server" BackColor="White" Width="95%"
                            Height="40px" BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:Label>
                    </td>


                </tr>



                <tr id="trTitulos" runat="server" visible="false">
                    <td class="fonteTab">Salário
                    </td>
                    <td class="fonteTab">Horário Trabalho
                    </td>
                    <td class="fonteTab">Habilidades
                    </td>
                </tr>
                <tr id="trLabel" runat="server" visible="false">
                    <td rowspan="1">
                        <asp:Label ID="lblSalario" runat="server" BackColor="White" Width="50%"
                            Height="13px" BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblHorarioTrabalho" runat="server" BackColor="White" Width="95%"
                            Height="13px" BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:Label>
                    </td>
                    <td class="Tam25 fonteTab" colspan="2">
                        <asp:Label ID="lblHabilidades" runat="server" BackColor="White" Width="95%"
                            Height="40px" BorderWidth="1px" BorderColor="#737373" CssClass="fonteTexto Centro"></asp:Label>
                    </td>
                </tr>








                <tr>
                    <td class="auto-style7">Observação
                    </td>
                </tr>
                <tr>
                    <td class="fonteTab" colspan="4">
                        <asp:TextBox ID="txtObservacao" runat="server" CssClass="fonteTexto" Height="40px"
                            onkeyup="javascript:IsMaxLength(this, 255);" TextMode="MultiLine" Width="97%"></asp:TextBox>
                    </td>
                </tr>

                 <tr>
                    <td class="auto-style7">Feedback
                    </td>
                </tr>
                <tr>
                    <td class="fonteTab" colspan="4">
                        <asp:TextBox ID="txtFeedback" Enabled="false" runat="server" CssClass="fonteTexto" Height="40px"
                            onkeyup="javascript:IsMaxLength(this, 500);" TextMode="MultiLine" Width="97%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9"></td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="6">
                        <asp:Button runat="server" CssClass="btn_novo" Text="Salvar" ID="btnSalvar" OnClick="btnSalvar_Click" />
                        &nbsp
                <asp:Button runat="server" CssClass="btn_novo" Text="Voltar" ID="btnVoltar" OnClick="btnVoltar_Click" />
                    </td>
                </tr>
            </table>



        </asp:View>

        <asp:View ID="View4" runat="server">
            <br />
           
            <asp:Panel ID="painelPerfil" runat="server" CssClass="centralizar" Height="200px" Visible="false"
                Width="500px">
                <div class="text_titulo" style="margin-top: 60px;">
                    <h1>Não existe Perfil para esse aprendiz</h1>
                    <asp:Button runat="server" CssClass="btn_novo" Text="Voltar" ID="Button1" OnClick="btnVoltar1_Click" />
                </div>
            </asp:Panel>
        </asp:View>


        <asp:View ID="ViewAtualizarPerfil" runat="server">


            <asp:Panel ID="Panel2" runat="server" CssClass="centralizar" Width="90%">
                <br />
                <div class="titulo cortitulo corfonte" style="font-size: large;">
                    Perfil
                </div>



                <table class="Table FundoPainel centralizar">
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">Geral:
                        </td>
                        <td class="Tam05 fonteTab ">Português:
                        </td>
                        <td class=" Tam05 fonteTab">Matemática:
                        </td>
                        <td class=" Tam05 fonteTab">Técnicas ADM:
                        </td>
                        <td class=" Tam05 fonteTab">Digitação:
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtGeral" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtPortugues" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtMatematica" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtTecnicaADM" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtDigitacao" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">Relações Humanas:
                        </td>
                        <td class="Tam05 fonteTab ">Ciências:
                        </td>
                        <td class=" Tam05 fonteTab">Pluralidade:
                        </td>
                        <td class=" Tam05 fonteTab">Informática:
                        </td>

                    </tr>

                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtRelacoesHumanas" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtCiencias" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtPluralidade" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtInformatica" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">Word
                        </td>
                        <td class="Tam05 fonteTab">Excel
                        </td>
                        <td class="Tam05 fonteTab">Internet
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtWord" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtExcel" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                        <td class="Tam05 fonteTab">
                            <asp:TextBox ID="txtInternet" ReadOnly="true" onkeyup="formataInteiro(this, event);" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="2" Width="70%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab" colspan="6">Características Gerais
                        </td>
                    </tr>
                    <tr>
                        <td class="Tam05">&nbsp;
                        </td>
                        <td class="Tam05 fonteTab" colspan="6">
                            <asp:TextBox ID="txtCaracteristicasGerais" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="3" CssClass="fonteTexto" Height="43px"
                                Width="70%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Button runat="server" CssClass="btn_novo" Text="Voltar" ID="btnVoltarPerfil" OnClick="btnVoltar1_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </asp:Panel>
        </asp:View>



        <asp:View ID="View5" runat="server">

            <div class="text_titulo">
                <h1>
                    <asp:Label runat="server" ID="Lb_Nome_pesquisa" /></h1>
            </div>
            <br />
            <div class="controls FundoPainel" style="height: 165px; width: 800px; margin: 0 auto; border: solid 1px #484848;">


                <div class="foto">

                    <asp:ImageButton runat="server" ID="fotoAvaliacao" Width="100%" Height="100%"
                        Style="margin: auto; z-index: 1; border: none;" />
                </div>


                <div class="fotoLabel">
                    <span class="fonteTab">Empresa: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_empresa" /><br />
                    <span class="fonteTab">Aprendiz: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Aprendiz" /><br />
                    <%--                    <span class="fonteTab">Orientador: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Orientador" /><br />
                    <span class="fonteTab">Horário de Aprendizagem: </span> <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Horario" />--%>

                    <span class="fonteTab">Data: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Data" /><br />
                    <span class="fonteTab">Usuário: </span>
                    <asp:Label CssClass="fonteTexto" runat="server" ID="LB_Responsavel" /><br />
                </div>
            </div>
            <div style="float: right; margin-right: 85px">
                <asp:Button runat="server" CssClass="btn_novo" Text="Voltar" ID="Button2" OnClick="btnVoltar1_Click" />
            </div>
            <asp:Panel runat="server" ViewStateMode="Enabled" Width="800px" Style="margin: 0 auto;" ID="Pn_Pesquisa">
            </asp:Panel>
        </asp:View>


        <asp:View ID="View6" runat="server">
             <div style="float: right; margin-right: 85px">
                <asp:Button runat="server" CssClass="btn_novo" Text="Voltar" ID="Button3" OnClick="btnVoltar1_Click" />
            </div>
            <div class="centralizar">
                <iframe id="IFrame1" height="870px" width="98%" style="border: none;" name="IFrame1"
                    src="CadastroAprendiz.aspx"></iframe>
            </div>
        </asp:View>


         <asp:View ID="View10" runat="server">
              <div style="float: right; margin-right: 85px">
                <asp:Button runat="server" CssClass="btn_novo" Text="Voltar" ID="Button4" OnClick="btnVoltar_Click" />
            </div>
            <div class="centralizar">
                <iframe id="IFrame4" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>


















</asp:Content>
