<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="cadastro_prato.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.cadastro_prato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">

        .auto-style2 { width: 1050px; }

        .auto-style6 { }

        .auto-style7 { text-align: center; }

        .auto-style8 { height: 26px; }

        .auto-style9 { height: 18px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Pratos</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="8">&nbsp;</td>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8">Nome do prato:</td>
                    <td colspan="2" class="auto-style8">
                        <asp:TextBox ID="txtNomePrato" onkeyup="formataTexto(this,event)" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                    <td colspan="2" class="auto-style8">Valor:</td>
                    <td colspan="2" class="auto-style8">
                        <asp:TextBox ID="txtValorPrato" onkeyup="formataValor(this,event)" runat="server" Width="70px" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6" colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style6">Ingrediente:</td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlIngrediente" runat="server" OnLoad="ddlIngrediente_Load">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">Quantidade:</td>
                    <td>
                        <asp:TextBox ID="txtQuantidade" onkeyup="formataDouble(this,event)" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnItem" runat="server" OnClick="btnItem_Click" Text="    Adicionar" CssClass="Adicionar" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6" colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7" colspan="2">
                        &nbsp;</td>
                    <td class="auto-style7" colspan="2">
                        <asp:Panel ID="pnIngPrato" runat="server" Height="100px" ScrollBars="Auto">
                            <asp:GridView ID="grdIngPrato" runat="server" AutoGenerateColumns="False" OnRowCommand="grdIngPrato_RowCommand" Width="600px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Ingrediente">
                                        <ItemTemplate>
                                            <div class="auto-style7">
                                                <asp:Label ID="lblIngrediente" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantidade">
                                        <ItemTemplate>
                                            <div class="auto-style7">
                                                <asp:Label ID="lblQuantidade" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Apagar" HeaderText="Apagar" ImageUrl="~/imgs/base/desabilitar.gif" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                    <td class="auto-style7" colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9" colspan="7">
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6" colspan="7">
                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Aplicar" OnClick="btnCadastrar_Click" style="text-align: right" Text="    Aplicar" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" />
                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>