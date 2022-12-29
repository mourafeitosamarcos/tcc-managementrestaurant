<%@ Page Title="..:: Sistema de Gestão de Restaurantes ::.." Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="dados_centro_custo.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.dados_centro_custo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">

        .auto-style2 { width: 1050px; }

        .auto-style1 { width: 100%; }

        .auto-style4 { height: 17px; }
        .auto-style5 {
            width: 77px;
        }
        .auto-style8 {
            height: 17px;
            width: 79px;
        }
        .auto-style9 {
            width: 276px;
        }
        .auto-style11 {
            text-align: right;
            background-image: url('../../imgs/base/voltar.gif');
            background-repeat: no-repeat;
            background-position: left;
            border: 1px solid #ebebeb;
            margin-left: 800px;
        }
        .auto-style12 {
            width: 233px;
        }
        .auto-style13 {
            height: 17px;
            width: 222px;
        }
        .auto-style14 {
            width: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Centro de Custos</legend>
            <table class="auto-style2">
                <tr>
                    <td class="auto-style14"></td>
                    <td class="auto-style9" >&nbsp;Centro de custo:</td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddlCC" runat="server" Height="19px" Width="85px" OnLoad="ddlCC_Load" OnSelectedIndexChanged="ddlCC_SelectedIndexChanged" OnTextChanged="ddlCC_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td >
                        <asp:Button ID="btnVoltar" runat="server" CssClass="auto-style11" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" Width="68px" />
                    </td>
                    <td class="auto-style4">&nbsp;</td>
                </tr>
                </table>
            <asp:Panel ID="pnCC" runat="server" Visible="false" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
            
<table>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style13">Codigo do centro de custo:</td>
                    <td>
                        <asp:TextBox ID="txtCodigo" onkeyup="formataInteiro(this,event)" runat="server" MaxLength="6" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="auto-style12">&nbsp;</td>
                    <td>Nome:</td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" onkeyup="formataTexto(this,event) , maiuscula(this)" MaxLength="50" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style4" colspan="5">&nbsp;</td>
                </tr>
               <tr>
                    <td  colspan="4">
                        <asp:Button ID="btnConcluir" runat="server" CssClass="Aplicar" Text="    Aplicar" OnClick="btnConcluir_Click" Visible="False" />
                        &nbsp; &nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="Limpar" Text="    Cancelar" OnClick="btnCancelar_Click" />
                        &nbsp;<asp:Button ID="btnAlterar" runat="server" CssClass="Aplicar" OnClick="btnAlterar_Click" style="text-align: right" Text="    Editar" />
                    </td>
                </tr>
            </table>
           </asp:Panel>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>