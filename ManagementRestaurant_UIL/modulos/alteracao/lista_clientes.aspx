<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="lista_clientes.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.lista_clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


        .auto-style2 { width: 1050px; }

        .auto-style10 { text-align: left; }

        .auto-style12 { width: 75px; }

        .auto-style14 { text-align: left; }

        .auto-style15 { width: 3px; }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Pesquisa de Clientes</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="6" class="auto-style15">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>Tipo de cliente:</td>
                    <td>
                        <asp:DropDownList ID="ddltipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltipo_SelectedIndexChanged" OnTextChanged="ddltipo_SelectedIndexChanged" style="text-align: center">
                            <asp:ListItem Text="Selecione..." Value="0" />
                            <asp:ListItem Text="Físico" Value="1" />
                            <asp:ListItem Text="Jurídico" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnCadastro" runat="server" Visible="False" Width="1054px">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style14">Parametro:</td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtPesquisa" runat="server" Width="185px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">Pesquisar por:</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlColuna" runat="server" style="text-align: left">
                                            <asp:ListItem Value="Cli_Nome">Nome</asp:ListItem>
                                            <asp:ListItem Value="Cli_Email">Email</asp:ListItem>
                                            <asp:ListItem Value="Cli_CPF">CPF/CNPJ</asp:ListItem>
                                            <asp:ListItem Value="Cli_RG">RG</asp:ListItem>
                                            <asp:ListItem Value="Cli_Cep">Cep</asp:ListItem>
                                            <asp:ListItem Value="Cli_Rua">Endereço</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style12">
                                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Buscar" OnClick="btnPesquisar_Click" style="text-align: right" Text="    Buscar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10" colspan="5">
                                        <asp:Panel ID="pGridPesquisa" runat="server" HorizontalAlign="Center" ScrollBars="Auto" Height="200px">
                                            <asp:GridView ID="grdClientes" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="grdClientes_RowCommand" style="margin-top: 24px" Width="1025px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nome">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbtNome" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CPF/CNPJ">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbtDoc" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CEP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbtCEP" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Endereço">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbtEndereco" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bairro">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbtBairro" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbtEmail" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Visualizar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnVisualizar" runat="server"  CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'  CommandName="Ver" style="text-align: right" ImageUrl="~/imgs/base/visualizar.gif"  /><br />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" Text="    Limpar" OnClick="btnLimpar_Click" Visible="False" />
                        <asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" Text="    Voltar" PostBackUrl="~/modulos/home/home.aspx" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>