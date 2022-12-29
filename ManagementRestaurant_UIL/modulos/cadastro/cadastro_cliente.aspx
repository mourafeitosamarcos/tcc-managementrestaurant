<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="cadastro_cliente.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.cadastro.cadastro_cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style2 { width: 1050px; }

        .auto-style3 { height: 26px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Clientes</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="4">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>Tipo de cliente:</td>
                    <td>
                        <asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                            <asp:ListItem Value="1">Físico</asp:ListItem>
                            <asp:ListItem Value="2">Jurídico</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnCadastro" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNome" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtNome" runat="server" Width="185px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Telefone:</td>
                                    <td>
                                        <asp:TextBox ID="txtTelefone" runat="server" onkeyup="formataTelefone(this,event)" MaxLength="14" Width="125px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="185px"></asp:TextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">
                                        <asp:Label ID="lblDocumento" runat="server"></asp:Label>
                                    </td>
                                    <td class="auto-style3" colspan="2">
                                        <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Visible="False" Width="125px"></asp:TextBox>
                                        <asp:TextBox ID="txtCnpj" runat="server" MaxLength="18" onkeyup="formataCNPJ(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Visible="False" Width="150px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7" colspan="4" rowspan="2">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>CEP:</td>
                                    <td>
                                        <asp:TextBox ID="txtCep" runat="server" onkeyup="formataCEP(this,event)" MaxLength="9" Width="90px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="Buscar" OnClick="btnBuscar_Click" Text="    Buscar" />
                                    </td>
                                    <td>Endereço:</td>
                                    <td>
                                        <asp:TextBox ID="txtEndereco" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Número:</td>
                                    <td>
                                        <asp:TextBox ID="txtNumero" runat="server" Width="50px" onkeyup="formataInteiro(this,event)" MaxLength="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bairro:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtBairro" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Cidade:</td>
                                    <td>
                                        <asp:TextBox ID="txtCidade" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Estado:</td>
                                    <td>
                                        <asp:TextBox ID="txtEstado" runat="server" Width="30px" onkeyup="formataTexto(this,event)" MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Aplicar" OnClick="btnCadastrar_Click" style="text-align: right" Text="    Cadastrar" />
                                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" />
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