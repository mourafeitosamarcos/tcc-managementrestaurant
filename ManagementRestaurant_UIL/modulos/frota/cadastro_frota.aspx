<%@ Page Title="" Language="C#" MasterPageFile="~/master/master.Master" AutoEventWireup="true" CodeBehind="cadastro_frota.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.frota.cadastro_frota" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style2 { width: 1050px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Frota</legend>
            <table class="auto-style2">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
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
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="4">
                        <asp:Panel ID="pnCadastro" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td>Nome:</td>
                                    <td>
                                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" Width="185px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Telefone:</td>
                                    <td>
                                        <asp:TextBox ID="txtTelefone" runat="server" Enabled="False" Width="125px" MaxLength="14"></asp:TextBox>
                                    </td>
                                    <td>CNH:</td>
                                    <td>
                                        <asp:TextBox ID="txtCnh" runat="server" Enabled="False" MaxLength="11" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Placa:</td>
                                    <td>
                                        <asp:TextBox ID="txtPlaca" runat="server" Width="80px" MaxLength="8" ToolTip="Preencha a placa do carro no formato AAA-0000"></asp:TextBox>
                                    </td>
                                    <td>Tipo de veículo:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipo" runat="server">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="Moto">Moto</asp:ListItem>
                                            <asp:ListItem Value="Carro">Carro</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;Marca:</td>
                                    <td>
                                        <asp:TextBox ID="txtMarca" runat="server" MaxLength="20" Width="125px" onkeyup="formataTexto(this,event)" ToolTip="Informe a marca do veículo. Ex: Fiat"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Modelo:</td>
                                    <td>
                                        <asp:TextBox ID="txtModelo" runat="server" MaxLength="30" Width="125px" onkeyup="formataTexto(this,event)" ToolTip="Informe o modelo do veículo. Ex: Fox"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style5"></td>
                                    <td class="auto-style5"></td>
                                    <td class="auto-style5"></td>
                                    <td class="auto-style5"></td>
                                    <td class="auto-style5"></td>
                                    <td class="auto-style5"></td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Botoes" OnClick="btnCadastrar_Click" Text="Cadastrar" />
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="Botoes" OnClick="btnLimpar_Click" Text="Limpar" />
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/frota/menu_frota.aspx" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
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