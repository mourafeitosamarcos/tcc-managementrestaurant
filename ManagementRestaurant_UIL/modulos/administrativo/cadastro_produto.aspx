<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="cadastro_produto.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.cadastro_produto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style1 { width: 100%; }

        .auto-style2 { width: 1095px; }

        .auto-style3 { }

        .auto-style4 { height: 17px; }

        .auto-style5 { }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de produtos</legend>
            <table class="auto-style2">
                <tr>
                    <td class="auto-style3" rowspan="4"></td>
                    <td class="auto-style4" colspan="4"></td>
                </tr>
                <tr>
                    <td class="auto-style5">Nome do produto:</td>
                    <td>
                        <asp:TextBox ID="txtNome" onkeyup="formataTexto(this,event)" runat="server"></asp:TextBox>
                    </td>
                    <td>Tipo do produto:</td>
                    <td>
                        <asp:DropDownList ID="ddlTipoProduto" runat="server" OnLoad="ddlTipoProduto_Load">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="4"></td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="4">
                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Aplicar" OnClick="btnCadastrar_Click" Text="    Aplicar" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" />
                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>