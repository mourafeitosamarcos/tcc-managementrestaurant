<%@ Page Language="C#" MasterPageFile="~/master/master_eati.Master" AutoEventWireup="true" CodeBehind="cadastro_eati.aspx.cs" Inherits="ManagementRestaurant_UIL.cadastro_eati" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">

        .auto-style2 {
            height: 280px;
            width: 764px;
        }

        .auto-style3 { width: 113px; }

        .auto-style5 { width: 244px; }

        .auto-style6 { width: 85px; }

        .auto-style7 { width: 153px; }

        .auto-style8 { width: 136px; }

        .auto-style9 { width: 84px; }

        .auto-style10 { width: 8px; }

        .auto-style11 { width: 65px; }

        .auto-style12 { width: 138px; }

        .auto-style13 { width: 305px; }

        .auto-style14 { height: 37px; }

        #fdsDadosPessoais { width: 769px; }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0; width: 800px;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial; text-align: center;" class="auto-style1">Cadastro de Funcionários (EATI)</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="4">&nbsp;</td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnCadastro" runat="server" Width="752px">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style3">Nome:</td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtNome" runat="server" Width="185px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                        &nbsp;</td>
                                    <td class="auto-style12">Telefone:</td>
                                    <td class="auto-style5">
                                        <asp:TextBox ID="txtTelefone" runat="server" onkeyup="formataTelefone(this,event)" MaxLength="14" Width="125px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style6">RG:</td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="txtRg" runat="server" MaxLength="9" onkeyup="formataInteiro(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="90px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">CPF:</td>
                                    <td class="auto-style13" colspan="4">
                                        <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px" EnableViewState="False"></asp:TextBox>
                                    </td>
                                    <td class="auto-style12">CNH:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtCnh" runat="server" MaxLength="11" onkeyup="formataInteiro(this,event)" Width="125px"></asp:TextBox>
                                        &nbsp;</td>
                                    <td class="auto-style8">Email:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" TextMode="Email" Width="185px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">N. Carteira:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtN_Ctrabalho" runat="server" Width="80px" onkeyup="formataInteiro(this,event)" MaxLength="6"></asp:TextBox>
                                    </td>
                                    <td class="auto-style9">N. Série:</td>
                                    <td colspan="6">
                                        <asp:TextBox ID="txtN_Ctrabalho2" runat="server" Width="80px" MaxLength="8" onkeyup="maiuscula(this)" ToolTip="Preencha o número de série no seguinte formato: 0000-AA"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">CEP:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtCep" runat="server" onkeyup="formataCEP(this,event)" MaxLength="9" Width="90px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style11">
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="Buscar" OnClick="btnBuscar_Click" Text="    Buscar" />
                                    </td>
                                    <td class="auto-style12">Endereço:</td>
                                    <td class="auto-style5">
                                        <asp:TextBox ID="txtEndereco" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td class="auto-style6">Número:</td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="txtNumero" runat="server" Width="50px" onkeyup="formataInteiro(this,event)" MaxLength="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">Bairro:</td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtBairro" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td class="auto-style12">Cidade:</td>
                                    <td class="auto-style5">
                                        <asp:TextBox ID="txtCidade" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td class="auto-style6">Estado:</td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="txtEstado" runat="server" Width="30px" onkeyup="formataTexto(this,event) , maiuscula(this)" MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">Cargo:</td>
                                    <td colspan="8">
                                        <asp:DropDownList ID="ddlCargo" runat="server" OnLoad="ddlCargo_Load">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style14">
                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Aplicar" OnClick="btnCadastrar_Click" style="text-align: right" Text="    Aplicar" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" />
                        &nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="VoltarEati" Text="    Fechar" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
    
</asp:Content>