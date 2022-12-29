<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="ferias_funcionario.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.ferias_funcionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style2 { width: 1050px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Lançamento de Férias</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="4">&nbsp;</td>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td>CPF do funcionário:</td>
                    <td>
                        <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="Buscar" OnClick="btnBuscar_Click" Text="    Buscar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnCadastro" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td>Nome:</td>
                                    <td>
                                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" Width="185px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Telefone:</td>
                                    <td>
                                        <asp:TextBox ID="txtTelefone" runat="server" Enabled="False" Width="100px" MaxLength="14"></asp:TextBox>
                                    </td>
                                    <td>RG:</td>
                                    <td>
                                        <asp:TextBox ID="txtRg" runat="server" Enabled="False" MaxLength="9" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8">Cargo:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtCargo" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style8">CNH:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtCnh" runat="server" Enabled="False" MaxLength="11" Width="125px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style8">Data de admissão:</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtAdmissao" runat="server" Enabled="False" Width="90px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>N. Carteira de Trabalho:</td>
                                    <td>
                                        <asp:TextBox ID="txtN_Ctrabalho" runat="server" Enabled="False" Width="140px" MaxLength="6"></asp:TextBox>
                                    </td>
                                    <td>Salario hora:</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtS_Hora" runat="server" Enabled="False" Width="70px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>CEP:</td>
                                    <td>
                                        <asp:TextBox ID="txtCep" runat="server" Enabled="False" Width="90px" MaxLength="9"></asp:TextBox>
                                    </td>
                                    <td>Endereço:</td>
                                    <td>
                                        <asp:TextBox ID="txtEndereco" runat="server" Enabled="False" Width="150px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Número:</td>
                                    <td>
                                        <asp:TextBox ID="txtNumero" runat="server" Enabled="False" Width="50px" MaxLength="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bairro:</td>
                                    <td>
                                        <asp:TextBox ID="txtBairro" runat="server" Enabled="False" Width="150px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Cidade:</td>
                                    <td>
                                        <asp:TextBox ID="txtCidade" runat="server" Enabled="False" Width="150px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>Estado:</td>
                                    <td>
                                        <asp:TextBox ID="txtEstado" runat="server" Enabled="False" Width="30px" MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>Proventos:</td>
                                    <td>
                                        <asp:TextBox ID="txtProventos" runat="server" Enabled="False" Width="90px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">&nbsp;</td>
                                </tr>
                            </table>
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style11" colspan="8">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">Início de férias:</td>
                                    <td class="auto-style11">
                                        <asp:TextBox ID="txtI_Ferias" runat="server" MaxLength="10" onkeyup="formataData(this,event)" ToolTip="Escolha um dia à partir da data atual" Width="90px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtI_Ferias_CalendarExtender" runat="server" Enabled="True" PopupButtonID="imgI_Ferias" TargetControlID="txtI_Ferias">
                                        </ajaxToolkit:CalendarExtender>
                                        &nbsp;<asp:ImageButton ID="imgI_Ferias" runat="server" ImageUrl="~/imgs/base/calendario.gif" />
                                    </td>
                                    <td class="auto-style11">Até:</td>
                                    <td class="auto-style11">
                                        <asp:TextBox ID="txtF_Ferias" runat="server" MaxLength="10" onkeyup="formataData(this,event)" ToolTip="Escolha um dia à partir da data atual" Width="90px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtF_Ferias_CalendarExtender" runat="server" Enabled="True" PopupButtonID="imgF_Ferias" TargetControlID="txtF_Ferias">
                                        </ajaxToolkit:CalendarExtender>
                                        &nbsp;<asp:ImageButton ID="imgF_Ferias" runat="server" ImageUrl="~/imgs/base/calendario.gif" />
                                    </td>
                                    <td class="auto-style11" rowspan="3">&nbsp;</td>
                                    <td>Horas trabalhadas por dia:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtHora" runat="server" MaxLength="2" onkeyup="formataInteiro(this,event)" ToolTip="O quantidade máxima de horas trabalhadas não podem ser maiores que 24" Width="30px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11" colspan="4" rowspan="2">
                                        &nbsp;</td>
                                    <td>Dias úteis do período:</td>
                                    <td>
                                        <asp:TextBox ID="txtDias" runat="server" MaxLength="2" onkeyup="formataInteiro(this,event)" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCalcular" runat="server" CssClass="Calcular" OnClick="btnCalcular_Click" Text="    Calcular" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="8" class="auto-style11">
                                        <asp:Button ID="btnAplicar" runat="server" CssClass="Aplicar" OnClick="btnAplicar_Click" Text="    Aplicar" />
                                        &nbsp;<ajaxToolkit:ConfirmButtonExtender ID="btnAplicar_ConfirmButtonExtender" runat="server" ConfirmText="O funcionário não poderá acessar o sistema no período de férias. Deseja continuar?" Enabled="True" TargetControlID="btnAplicar">
                                              </ajaxToolkit:ConfirmButtonExtender>
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" />
                                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset><br />
    </asp:Panel>
</asp:Content>