<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="lancar_notas.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.financeiro.lancar_notas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">


        .auto-style1 { width: 100%; }

        .auto-style2 { width: 1050px; }

        .auto-style4 { }

        .auto-style5 {
            text-align: left;
            width: 205px;
        }

        .auto-style9 { }

        .auto-style10 { }

        .auto-style11 { }

        .auto-style12 { font-size: x-small; }

        .auto-style13 { height: 85px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Lançamento de Notas</legend>
            <table class="auto-style2">
                <tr>
                    <td class="auto-style3" rowspan="10"></td>
                    <td class="auto-style4" colspan="4"></td>
                    <td class="auto-style4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style11">Numero da conta:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtConta" runat="server" MaxLength="50" onkeyup="formataInteiro(this,event)" Width="250px"></asp:TextBox>
                    </td>
                    <td class="auto-style4">Departamento:</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlDepartamento" runat="server" OnLoad="ddlDepartamento_Load" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style11">Data de pagamento:</td>
                    <td class="auto-style4" colspan="3">
                        <asp:TextBox ID="txtPagamento" runat="server" MaxLength="10" onkeyup="formataData(this,event)" Width="90px" ToolTip="Escolha um dia à partir da data atual"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtPagamento_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtPagamento" PopupButtonID="imgPagamento">
                        </ajaxToolkit:CalendarExtender>
                        &nbsp;<asp:ImageButton ID="imgPagamento" runat="server" ImageUrl="~/imgs/base/calendario.gif" />
                    </td>
                    <td class="auto-style4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="4">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="4">
                        Observações:</td>
                    <td class="auto-style10">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13" colspan="4">
                        <asp:TextBox ID="txtObservacao" runat="server" Height="75px" MaxLength="5" TextMode="MultiLine" Width="350px"></asp:TextBox>
                    </td>
                    <td class="auto-style13">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style12" colspan="2">(Voce pode digitar uma observação com até 500 caractéres incluindo espaços)</td>
                    <td class="auto-style12" colspan="2">
                        &nbsp;</td>
                    <td class="auto-style12">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9" colspan="4">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style11" colspan="3">
                        <asp:HiddenField ID="hfOrigemPagamento" runat="server" />
                    </td>
                    <td class="auto-style5">
                        <asp:HiddenField ID="hfTipoPagamento" runat="server" />
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style11" colspan="5">
                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Aplicar" OnClick="btnCadastrar_Click" Text="    Aplicar" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" />
                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>