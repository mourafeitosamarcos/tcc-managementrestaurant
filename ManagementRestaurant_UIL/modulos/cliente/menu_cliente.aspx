<%@ Page Title="" Language="C#" MasterPageFile="~/master/master.Master" AutoEventWireup="true" CodeBehind="menu_cliente.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.cliente.menu_cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">



        .auto-style1 { width: 350px; }

        .auto-style2 { width: 256px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="tudo">
        <table style="width: 100%;">
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo_menu" Text="Cadastros"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <img alt="" src="../../imgs/base/cad_menu.png" /></td>
                <td>
                    <ul>
                        <font color="#ff6a00">Controle de Usuários</font>
                        <li><a href="cadastro_cliente.aspx">Cadastro de Clientes</a>
                            <br />
                        </li>
                    </ul>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>