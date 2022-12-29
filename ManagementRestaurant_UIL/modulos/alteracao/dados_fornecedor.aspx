<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="dados_fornecedor.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.dados_fornecedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style2 { width: 1050px; }

        .auto-style4 { height: 26px; }

        .auto-style5 { height: 18px; }

        .auto-style3 { height: 36px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Atualização de Fornecedores</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="4">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>Nome do&nbsp;Fornecedor:</td>
                    <td>
                        <asp:DropDownList ID="ddlFornecedor" runat="server" AutoPostBack="True" onload="ddlFornecedor_Load" OnSelectedIndexChanged="ddlFornecedor_SelectedIndexChanged" OnTextChanged="ddlFornecedor_SelectedIndexChanged" style="margin-left: 3px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnAlteracao" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style4">Razão social:</td>
                                    <td class="auto-style4" colspan="2">
                                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="185px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4">Telefone:</td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtTelefone" runat="server" Enabled="False" MaxLength="14" Width="125px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtEmail" runat="server" Enabled="False" MaxLength="50" Width="185px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style4">CNPJ:</td>
                                    <td class="auto-style4" colspan="6">
                                        <asp:TextBox ID="txtCnpj" runat="server" Enabled="False" MaxLength="18" onkeyup="formataCNPJ(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" colspan="7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>CEP:</td>
                                    <td>
                                        <asp:TextBox ID="txtCep" runat="server" Enabled="False" Height="22px" MaxLength="9" onkeyup="formataCEP(this,event)" Width="90px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="Buscar" OnClick="btnBuscar_Click" Text="    Buscar" />
                                    </td>
                                    <td>Endereço:</td>
                                    <td>
                                        <asp:TextBox ID="txtEndereco" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;Número:</td>
                                    <td>
                                        <asp:TextBox ID="txtNumero" runat="server" Enabled="False" MaxLength="5" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bairro:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtBairro" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>Cidade:</td>
                                    <td>
                                        <asp:TextBox ID="txtCidade" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>Estado:</td>
                                    <td>
                                        <asp:TextBox ID="txtEstado" runat="server" Enabled="False" MaxLength="2" onkeyup="formataTexto(this,event)" Width="30px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Complemento:</td>
                                    <td colspan="6">
                                        <asp:TextBox ID="txtComplemento" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" colspan="7">&nbsp;<asp:Button ID="btnAlterar" runat="server" CssClass="Aplicar" OnClick="btnAlterar_Click" style="text-align: right" Text="    Alterar " Visible="False" />
                                        &nbsp;<asp:Button ID="btnConcluirAlteracao" runat="server" CssClass="Aplicar" OnClick="btnConcluirAlteracao_Click" style="text-align: right" Text="    Concluir" Visible="False" />
                                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" Text="    Voltar" PostBackUrl="~/modulos/home/home.aspx" />
                                        &nbsp;</td>
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