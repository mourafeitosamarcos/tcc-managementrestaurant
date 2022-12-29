<%@ Page Title="" Language="C#" MasterPageFile="~/master/master.Master" AutoEventWireup="true" CodeBehind="menu_administrativo.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.menu_administrativo" %>
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
                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo_menu" Text="Controle administrativo"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <img alt="" src="../../imgs/base/admin_menu.png" /></td>
                <td>
                    <ul>
                        <font color="#ff6a00">Controle de funcionários</font>
                        <br />
                        <li><a href="cadastro_funcionario.aspx">Cadastro de funcionários</a>
                            <br />
                        </li>
                        <li><a href="ferias_funcionario.aspx">Lançamento de ferias</a>
                            <br />
                        </li>
                        <li><a href="controle_ponto.aspx">Controle de ponto</a></li>
                    </li>
                        <li><a href="lista_ativos.aspx">Alteração de dados</a>
                        <br />

                    </ul>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>