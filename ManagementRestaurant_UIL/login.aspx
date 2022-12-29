<%@ Page Language="C#" MasterPageFile="~/master/login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ManagementRestaurant_UIL.login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .auto-style1 { width: 100%; }

        .auto-style3 {
            font-size: x-small;
            height: 12px;
        }

        .auto-style11 { color: #000000; }

        .auto-style12 {
            font-size: x-small;
            height: 12px;
            text-align: left;
        }

    .Aplicar {
    background-image: url('imgs/base/aplicar.gif');
    background-position: left;
    background-repeat: no-repeat;
    border: 1px solid #ebebeb;
    height: 25px;
    text-align: right;
    cursor: pointer;
}

    </style>
    <table class="auto-style1">
        <tr>
            <td>
                <asp:Panel ID="pnLogin" runat="server">
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style12">CPF</td>
                            <td class="auto-style12" colspan="2">Senha</td>
                            <td class="auto-style3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnLogar" runat="server" CssClass="BotoesNovoLogin" OnClick="btnLogar_Click" Text="Logar" />
                            </td>
                            <td>
                                <table class="auto-style1">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnEati" runat="server" CssClass="BotaoCadastrar" OnClick="btnEati_Click" Width="91px" Text="Cadastre-se" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="3">
                                <asp:LinkButton ID="btnNovaSenha" runat="server" Text="Esqueci minha senha" OnClick="btnNovaSenha_Click"></asp:LinkButton>
                                
                            </td>
                            <td style="vertical-align: middle">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnSenhaR" runat="server" Visible="False" Width="609px">
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style11" rowspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HiddenField ID="hfToken" runat="server" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            <td class="auto-style11">
                                <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" MaxLength="50" Width="185px"></asp:TextBox>
                            </td>
                            <td rowspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:HiddenField ID="hfRG" runat="server" />
                                <asp:HiddenField ID="hfCNH" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">
                                <asp:Label ID="lblToken" runat="server" Text="Token:" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToken" runat="server" Width="200px" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btnEnviar" runat="server" CssClass="BotoesNovoLogin" OnClick="btnEnviar_Click" Text="Enviar" style="text-align: right" />
                                <asp:Button ID="btnConfirmar2" runat="server" CssClass="BotoesNovoLogin" OnClick="btnConfirmar2_Click" style="text-align: right" Text="Confirmar" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnSenhaN" runat="server" Visible="False" Width="609px">
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style11" rowspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            <td class="auto-style11">Nova senha:</td>
                            <td>
                                <asp:TextBox ID="txtSenhaN" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            </td>
                            <td rowspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style11">Confirme a nova senha:</td>
                            <td>
                                <asp:TextBox ID="txtSenhaC" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btnConfirmar" runat="server" CssClass="BotoesNovoLogin" OnClick="btnConfirmar_Click" Text="Confirmar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>