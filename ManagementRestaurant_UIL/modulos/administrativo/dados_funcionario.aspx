<%@ Page Title="" Language="C#" MasterPageFile="~/master/master.Master" AutoEventWireup="true" CodeBehind="dados_funcionario.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.dados_funcionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style1 { font-size: small; }

        .auto-style2 { width: 100%; }

        .auto-style8 { height: 26px; }

        .auto-style5 { height: 17px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Alteração de Cadastro</legend>
            <table class="auto-style2">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
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
                    <td>&nbsp;</td>
                    <td colspan="4">
                        <asp:Panel ID="pnCadastro" runat="server">
                            <table class="auto-style2">
                                <tr>
                                    <td>Nome:</td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtNome" runat="server" Width="185px" onkeyup="formataTexto(this,event)" MaxLength="50" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>Telefone:</td>
                                    <td>
                                        <asp:TextBox ID="txtTelefone" runat="server" onkeyup="formataTelefone(this,event)" MaxLength="14" Width="125px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>RG:</td>
                                    <td>
                                        <asp:TextBox ID="txtRg" runat="server" MaxLength="9" onkeyup="formataInteiro(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="90px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8">CPF:</td>
                                    <td class="auto-style8" colspan="4">
                                        <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="auto-style8">CNH:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtCnh" runat="server" MaxLength="11" onkeyup="formataInteiro(this,event)" Width="125px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="auto-style8">&nbsp;</td>
                                    <td class="auto-style8">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>N. Carteira:</td>
                                    <td>
                                        <asp:TextBox ID="txtN_Ctrabalho" runat="server" Width="80px" onkeyup="formataInteiro(this,event)" MaxLength="6" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>N. Série:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtN_Ctrabalho2" runat="server" Width="80px" MaxLength="8" ToolTip="Preencha o número de série no seguinte formato: 0000-AA" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>CEP:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtCep" runat="server" onkeyup="formataCEP(this,event)" MaxLength="9" Width="90px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>Endereço:</td>
                                    <td>
                                        <asp:TextBox ID="txtEndereco" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>Número:</td>
                                    <td>
                                        <asp:TextBox ID="txtNumero" runat="server" Width="50px" onkeyup="formataInteiro(this,event)" MaxLength="5" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bairro:</td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtBairro" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>Cidade:</td>
                                    <td>
                                        <asp:TextBox ID="txtCidade" runat="server" Width="150px" onkeyup="formataTexto(this,event)" MaxLength="50" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>Estado:</td>
                                    <td>
                                        <asp:TextBox ID="txtEstado" runat="server" Width="30px" onkeyup="formataTexto(this,event)" MaxLength="2" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Cargo:</td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="ddlCargo" runat="server" OnLoad="ddlCargo_Load" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <br />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">
                        <asp:Button ID="btnBack" runat="server" CssClass="Botoes" OnClick="BtnBack_Click" style="text-align: right" Text="Voltar" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="Botoes" OnClick="btnCancelar_Click" Text="Cancelar" Visible="False" />
                        <asp:Button ID="btnAlterar" runat="server" CssClass="Botoes" OnClick="btnAlterar_Click" style="text-align: right" Text="Alterar " />
                        <asp:Button ID="btnConcluirAlteracao" runat="server" CssClass="Botoes" OnClick="btnConcluirAlteracao_Click" style="height: 26px; text-align: right;" Text="Concluir" Visible="False" />
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>