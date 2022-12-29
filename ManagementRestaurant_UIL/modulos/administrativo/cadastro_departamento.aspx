<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="cadastro_departamento.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.cadastro_departamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">


        .auto-style1 { width: 100%; }

        .auto-style2 { width: 1050px; }

        .auto-style4 { height: 17px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Departamentos</legend>
            <table class="auto-style2">
                <tr>
                    <td class="auto-style3" rowspan="7"></td>
                    <td class="auto-style4" colspan="4"></td>
                </tr>
                <tr>
                    <td class="auto-style5">Nome do departamento:</td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" onkeyup="formataTexto(this,event) , maiuscula(this)" Width="150px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>Responsável:</td>
                    <td>
                        <asp:DropDownList ID="ddlResponsavel" runat="server" OnLoad="ddlResponsavel_Load">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5">
                        <asp:RadioButton ID="rbCentroCusto" runat="server" AutoPostBack="True" GroupName="rbG1" OnCheckedChanged="rbCentroCusto_CheckedChanged" Text="Centro de custos" Checked="True" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rbContrato" runat="server" AutoPostBack="True" GroupName="rbG1" OnCheckedChanged="rbContrato_CheckedChanged" Text="Contratos" />
                    </td>
                    <td colspan="2" rowspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5">
                        <asp:DropDownList ID="ddlCentroCusto" runat="server" OnLoad="ddlCentroCusto_Load">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlContrato" runat="server" OnLoad="ddlContrato_Load" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="4"></td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="4">
                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Aplicar" Text="    Aplicar" OnClick="btnCadastrar_Click" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" Text="    Limpar" OnClick="btnLimpar_Click" />
                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>