<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="entrada_estoque.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.cadastro.entrada_estoque" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">

        .auto-style1 { width: 100%; }

        .auto-style2 { width: 1079px; }

        .auto-style3 { height: 17px; }

        .auto-style4 { height: 26px; }

        .auto-style5 { height: 18px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Entrada de produtos no estoque</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="4">&nbsp;</td>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td>Codigo do produto:</td>
                    <td>
                        <asp:TextBox ID="txtCodProduto" runat="server" MaxLength="4" onkeyup="formataInteiro(this,event)" Width="35px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="Buscar" Text="    Buscar" OnClick="btnBuscar_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnProduto" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style4">Nota Fiscal:</td>
                                    <td class="auto-style4" colspan="3">
                                        <asp:TextBox ID="txtNotaF" onkeyup="formataInteiro(this,event)" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td>Nome do produto:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtProduto" runat="server" Enabled="False" onkeyup="formataTexto(this,event)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkAvulso" runat="server" AutoPostBack="True" OnCheckedChanged="chkAvulso_CheckedChanged" Text="Compra avulsa" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fornecedor:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlFornecedor" runat="server" OnLoad="ddlFornecedor_Load">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Lote:</td>
                                    <td>
                                        <asp:TextBox ID="txtLote" onkeyup="formataInteiro(this,event)" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Validade:</td>
                                    <td>
                                        <asp:TextBox ID="txtValidade" runat="server" MaxLength="10" onkeyup="formataData(this,event)" Width="80px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtValidade_CalendarExtender" runat="server" Enabled="True" PopupButtonID="imgValidade" TargetControlID="txtValidade">
                                        </ajaxToolkit:CalendarExtender>
                                        &nbsp;<asp:ImageButton ID="imgValidade" runat="server" ImageUrl="~/imgs/base/calendario.gif" />
                                    </td>
                                    <td>Quantidade:</td>
                                    <td>
                                        <asp:TextBox ID="txtQuantidade" runat="server" MaxLength="5" onkeyup="formataDouble(this,event)" ToolTip="Caso o produto seja medido por peso, informe o peso total da carga" Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="btnAplicar" runat="server" CssClass="Aplicar" Text="    Aplicar" OnClick="btnAplicar_Click" />
                                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" Text="    Limpar" OnClick="btnLimpar_Click" />
                                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>