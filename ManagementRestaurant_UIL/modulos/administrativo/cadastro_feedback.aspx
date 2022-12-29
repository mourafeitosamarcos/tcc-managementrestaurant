<%@ Page Language="C#" MasterPageFile="~/master/master_eati.Master" AutoEventWireup="true" CodeBehind="cadastro_feedback.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.cadastro_feedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style2 {
            height: 280px;
            width: 764px;
        }

        .auto-style5 { width: 244px; }

        .auto-style14 { height: 37px; }

        #fdsDadosPessoais { width: 769px; }

        .auto-style15 {
            width: 100%;
        }

        .auto-style17 {
            font-size: x-small;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0; width: 800px;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial; text-align: center;" class="auto-style1">Enviar Feedback</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="4">&nbsp;</td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnCadastro" runat="server" Width="752px" style="text-align: center">
                            <table class="auto-style15">
                                <tr>
                                    <td class="auto-style16" rowspan="7"></td>
                                    <td class="auto-style16">
                                        <asp:HiddenField ID="hfCpf" runat="server" />
                                    </td>
                                    <td class="auto-style16" rowspan="7"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">Assunto:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlAssunto" runat="server">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="1">Dúvida</asp:ListItem>
                                            <asp:ListItem Value="2">Sugestão</asp:ListItem>
                                            <asp:ListItem Value="3">Reclamação</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style16"></td>
                                </tr>
                                <tr>
                                    <td>Mensagem:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtMensagem" runat="server" Height="75px" MaxLength="5" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style17">(Voce pode digitar uma mensagem com até 500 caractéres incluindo espaços)</td>
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