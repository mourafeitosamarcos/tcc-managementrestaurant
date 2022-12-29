<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="dados_cliente.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.dados_cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


        .auto-style2 { width: 1050px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Atualização de Clientes</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="8">&nbsp;</td>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNome" runat="server"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="185px"></asp:TextBox>
                    </td>
                    <td>Telefone:</td>
                    <td>
                        <asp:TextBox ID="txtTelefone" runat="server" Enabled="False" MaxLength="14" onkeyup="formataTelefone(this,event)" Width="125px"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:HiddenField ID="hfTipo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoCod" runat="server" Text="CPF:"></asp:Label>
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtCPF" runat="server" Enabled="False" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Visible="false" Width="125px"></asp:TextBox>
                        <asp:TextBox ID="txtCNPJ" runat="server" Enabled="False" MaxLength="18" onkeyup="formataCNPJ(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Visible="false" Width="125px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>CEP:</td>
                    <td>
                        <asp:TextBox ID="txtCep" runat="server" Enabled="False" MaxLength="9" onkeyup="formataCEP(this,event)" Width="90px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="Buscar" Enabled="False" OnClick="btnBuscar_Click" Text="    Buscar" />
                    </td>
                    <td>Endereço:</td>
                    <td>
                        <asp:TextBox ID="txtEndereco" runat="server" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px" Enabled="False"></asp:TextBox>
                    </td>
                    <td>Número:</td>
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
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <asp:Button ID="btnAlterar" runat="server" CssClass="Aplicar" OnClick="btnAlterar_Click" style="text-align: right" Text="    Alterar " />
                        &nbsp;<asp:Button ID="btnConcluirAlteracao" runat="server" CssClass="Aplicar" OnClick="btnConcluirAlteracao_Click" style="text-align: right" Text="    Concluir" Visible="False" />
                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" Text="    Voltar" PostBackUrl="~/modulos/home/home.aspx" />
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>