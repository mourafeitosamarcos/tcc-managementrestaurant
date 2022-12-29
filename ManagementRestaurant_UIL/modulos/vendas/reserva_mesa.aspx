<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="reserva_mesa.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.vendas.reserva_mesa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">

        .auto-style2 { width: 1050px; }

        .auto-style4 { }

        .auto-style5 { height: 39px; }

        .auto-style6 { height: 36px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Reserva de Mesas</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:HiddenField ID="hfCpfFuncionario" runat="server" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>CPF do cliente:</td>
                    <td>
                        <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="Buscar" OnClick="btnBuscar_Click" Text="    Buscar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnReserva" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style4">
                                        Nome:</td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="185px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4">Telefone:</td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtTelefone" runat="server" Enabled="False" MaxLength="14" onkeyup="formataTelefone(this,event)" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" colspan="2">
                                        <br />
                                        &nbsp;</td>
                                    <td class="auto-style4">Hora do agendamento:</td>
                                    <td class="auto-style4">
                                        <asp:DropDownList ID="ddlHora" runat="server">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="08:00">08:00</asp:ListItem>
                                            <asp:ListItem Value="09:30">09:30</asp:ListItem>
                                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                                            <asp:ListItem Value="12:30">12:30</asp:ListItem>
                                            <asp:ListItem Value="14:00">14:00</asp:ListItem>
                                            <asp:ListItem Value="15:30">15:30</asp:ListItem>
                                            <asp:ListItem Value="17:00">17:00</asp:ListItem>
                                            <asp:ListItem Value="18:30">18:30</asp:ListItem>
                                            <asp:ListItem Value="20:00">20:00</asp:ListItem>
                                            <asp:ListItem Value="21:30">21:30</asp:ListItem>
                                            <asp:ListItem>23:00</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style4">Data:</td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtData" runat="server" MaxLength="10" onkeyup="formataData(this,event)" ToolTip="Escolha um dia à partir da data atual" Width="90px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtData_CalendarExtender" runat="server" Enabled="True" PopupButtonID="imgData" TargetControlID="txtData">
                                        </ajaxToolkit:CalendarExtender>
                                        &nbsp;<asp:ImageButton ID="imgData" runat="server" ImageUrl="~/imgs/base/calendario.gif" />
                                    </td>
                                    <td class="auto-style4">
                                        Número da mesa:</td>
                                    <td class="auto-style4">
                                        <asp:DropDownList ID="ddlMesa" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMesa_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" colspan="2">&nbsp;</td>
                                    <td class="auto-style5" colspan="2">
                                        <asp:Label ID="lblStatusMesa" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style6" colspan="4">
                                        <asp:Button ID="btnAplicar" runat="server" CssClass="Aplicar" style="text-align: right" Text="    Aplicar" Enabled="False" OnClick="btnAplicar_Click" />
                                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" style="text-align: right" Text="    Limpar" OnClick="btnLimpar_Click" />
                                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" Text="    Voltar" PostBackUrl="~/modulos/home/home.aspx" />
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