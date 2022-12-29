<%@ Page Title="" Language="C#" MasterPageFile="~/master/master.Master" AutoEventWireup="true" CodeBehind="controle_ponto.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.controle_ponto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style2 { width: 1050px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Controle de ponto</legend>
            <table class="auto-style2">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>CPF do funcionário:</td>
                    <td>
                        <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="Botoes" OnClick="btnBuscar_Click" Text="Buscar" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                <td>&nbsp;</td>
                <td>Registro:</td>
                <td>
                    <asp:DropDownList ID="ddlSentido" runat="server">
                        <asp:ListItem Value="0">Selecione...</asp:ListItem>
                        <asp:ListItem Value="1">Entrada</asp:ListItem>
                        <asp:ListItem Value="2">Saida</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="7">
                        <asp:Panel ID="pnCadastro" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style16">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style16"></td>
                                    <td>
                                        <asp:Label ID="lblEntrada" runat="server" Text="Hora de entrada:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEntrada" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSaida" runat="server" Text="Hora de saida:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSaida" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style13" colspan="6">
                                        <asp:Button ID="btnAplicar0" runat="server" CssClass="Botoes" OnClick="btnAplicar_Click" Text="Aplicar" />
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="Botoes" OnClick="btnLimpar_Click" Text="Limpar" />
                                        <asp:Button ID="btnCancelar0" runat="server" CssClass="Botoes" PostBackUrl="~/Modulos/Operacional/menu_operacional.aspx" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style5">
                    </td>
                    <td class="auto-style5" colspan="4"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />
        <br />
    </asp:Panel>
</asp:Content>