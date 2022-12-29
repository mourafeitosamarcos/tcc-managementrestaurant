<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="cadastro_funcionario.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.cadastro_funcionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style2 { width: 1050px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Funcionários</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="4">&nbsp;</td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnCadastro" runat="server">
                            <table class="auto-style2">
                                <tr>
                                    <td>Nome:</td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtNome" runat="server" Width="185px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Telefone:</td>
                                    <td>
                                        <asp:TextBox ID="txtTelefone" runat="server" onkeyup="formataTelefone(this,event)" MaxLength="14" Width="125px"></asp:TextBox>
                                    </td>
                                    <td>RG:</td>
                                    <td>
                                        <asp:TextBox ID="txtRg" runat="server" MaxLength="9" onkeyup="formataInteiro(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="90px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8">CPF:</td>
                                    <td class="auto-style8" colspan="4">
                                        <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style8">CNH:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtCnh" runat="server" MaxLength="11" onkeyup="formataInteiro(this,event)" Width="125px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style8">Email:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" TextMode="Email" Width="185px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>N. Carteira:</td>
                                    <td>
                                        <asp:TextBox ID="txtN_Ctrabalho" runat="server" Width="80px" onkeyup="formataInteiro(this,event)" MaxLength="6"></asp:TextBox>
                                    </td>
                                    <td>N. Série:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtN_Ctrabalho2" runat="server" Width="80px" MaxLength="8" ToolTip="Preencha o número de série no seguinte formato: 0000-AA"></asp:TextBox>
                                    </td>
                                    <td colspan="4" rowspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>CEP:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtCep" runat="server" onkeyup="formataCEP(this,event)" MaxLength="9" Width="90px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
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
                                    <td colspan="4">
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
                                    <td colspan="9">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Cargo:</td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="ddlCargo" runat="server" OnLoad="ddlCargo_Load">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5">
                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Aplicar" OnClick="btnCadastrar_Click" style="text-align: right" Text="    Aplicar" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" />
                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>