<%@ Page Title="" Language="C#" MasterPageFile="~/master/master.Master" AutoEventWireup="true" CodeBehind="menu_frota.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.frota.menu_frota" %>
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
                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo_menu" Text="Controle Operacional"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <img alt="" src="../../imgs/base/menu_opera.png" /></td>
                <td>
                    <ul>
                        <font color="#ff6a00">Controle de Frota</font>
                        <li><a href="cadastro_frota.aspx">Cadastro de veículos Internos (funcionários)</a>
                            <br />
                        </li>
                        <li><a href="controle_veiculo.aspx">Controle de entrada e saída</a></li>
                    </ul>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>

</asp:Content>