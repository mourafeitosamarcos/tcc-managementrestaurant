<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="controle_ponto.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.operacional.controle_ponto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style2 { width: 1050px; }

        .auto-style3 { height: 18px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Controle de ponto</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="2">&nbsp;</td>
                </tr>
                <tr>
                <td>
                    <asp:Panel ID="pnCadastro" runat="server" Visible="False">
                        <table class="auto-style2">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hfCpf" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEntrada" runat="server" Text="Hora de entrada:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEntrada" runat="server" Enabled="False" Width="80px" style="text-align: center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblSaida" runat="server" Text="Hora de saida:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSaida" runat="server" Enabled="False" Width="80px" style="text-align: center"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3" colspan="4"></td>
                            </tr>
                            <tr>
                                <td class="auto-style13" colspan="4">
                                    <asp:Button ID="btnAplicar" runat="server" CssClass="Aplicar" OnClick="btnAplicar_Click" Text="    Aplicar" />
                                    &nbsp;&nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>