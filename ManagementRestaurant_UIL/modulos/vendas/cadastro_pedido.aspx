<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="cadastro_pedido.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.vendas.cadastro_pedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">

        .auto-style2 { width: 1050px; }

        .auto-style1 { width: 100%; }

        .auto-style3 { width: 4px; }

        .auto-style5 { height: 152px; }

        .auto-style8 { }

        .auto-style11 {
            height: 29px;
            width: 125px;
        }

        .auto-style12 { height: 29px; }

        .auto-style13 {
            height: 29px;
            text-align: center;
        }

        .auto-style14 {
            height: 29px;
            text-align: center;
            width: 81px;
        }

        .auto-style7 { text-align: center; }

        .auto-style15 { width: 135px; }

        .auto-style16 { width: 133px; }

        .auto-style17 { width: 125px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Pedidos</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="9" class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:HiddenField ID="hfCpf" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Tipo de cliente:</td>
                    <td>
                        <asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                            <asp:ListItem Value="1">Físico</asp:ListItem>
                            <asp:ListItem Value="2">Jurídico</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnClienteF" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td>CPF do cliente:</td>
                                    <td>
                                        <asp:TextBox ID="txtCpfCliente" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                                    </td>
                                    <td>Valor total:</td>
                                    <td>
                                        <asp:TextBox ID="txtValorPrato" runat="server" Enabled="False" MaxLength="6" onkeyup="formataValor(this,event)" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCalcular" runat="server" CssClass="Calcular" Text="    Calcular" OnClick="btnCalcular_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>Realização da compra:</td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlCompra" runat="server">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="1">Pessoal</asp:ListItem>
                                            <asp:ListItem Value="2">Viagem</asp:ListItem>
                                            <asp:ListItem Value="3">Entrega</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnClienteJ" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style16">CNPJ do cliente:</td>
                                    <td>
                                        <asp:TextBox ID="txtCnpjCliente" runat="server" MaxLength="18" onkeyup="formataCNPJ(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>Valor:</td>
                                    <td>
                                        <asp:TextBox ID="txtValorPrato2" runat="server" Enabled="False" MaxLength="6" onkeyup="formataValor(this,event)" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCalcularJ" runat="server" CssClass="Calcular" Text="    Calcular" OnClick="btnCalcularJ_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">Quantidade por dia:</td>
                                    <td>
                                        <asp:TextBox ID="txtQuantidadeDia" runat="server" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>Realização da compra:</td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlCompra2" runat="server">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="2">Viagem</asp:ListItem>
                                            <asp:ListItem Value="3">Entrega</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">Previsão de entrega:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlEntrega" runat="server" OnLoad="ddlEntrega_Load">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style16" colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">Data de inicio:</td>
                                    <td>
                                        <asp:TextBox ID="txtI_Contrato" runat="server" MaxLength="10" onkeyup="formataData(this,event)" ToolTip="Escolha um dia à partir da data atual" Width="90px"></asp:TextBox>
                                        &nbsp;<asp:ImageButton ID="imgI_Contrato" runat="server" ImageUrl="~/imgs/base/calendario.gif" />
                                        <ajaxToolkit:CalendarExtender ID="txtI_Contrato_CalendarExtender" runat="server" Enabled="True" PopupButtonID="imgI_Contrato" TargetControlID="txtI_Contrato">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>Até:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtF_Contrato" runat="server" MaxLength="10" onkeyup="formataData(this,event)" ToolTip="Escolha um dia à partir da data atual" Width="90px"></asp:TextBox>
                                        &nbsp;<asp:ImageButton ID="imgF_Contrato" runat="server" ImageUrl="~/imgs/base/calendario.gif" />
                                        <ajaxToolkit:CalendarExtender ID="txtF_Contrato_CalendarExtender" runat="server" Enabled="True" PopupButtonID="imgF_Contrato" TargetControlID="txtF_Contrato">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">
                        <asp:Panel ID="pnPratos" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style11">Prato:</td>
                                    <td class="auto-style12">
                                        <asp:DropDownList ID="ddlPrato" runat="server" OnLoad="ddlPrato_Load" AutoPostBack="True" OnSelectedIndexChanged="ddlPrato_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style12">Valor:</td>
                                    <td class="auto-style12">
                                        <asp:TextBox ID="txtValor" runat="server" Width="55px" Enabled="False" MaxLength="5"></asp:TextBox>
                                    </td>
                                    <td class="auto-style13" >Quantidade:</td>
                                    <td class="auto-style14">
                                        <asp:TextBox ID="txtQuantidade" runat="server" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style12">
                                        <asp:Button ID="btnItem" runat="server" CssClass="Adicionar" OnClick="btnItem_Click" style="text-align: right" Text="    Adicionar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8" colspan="7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style17">
                                        &nbsp;</td>
                                    <td colspan="5">
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style17">&nbsp;</td>
                                    <td colspan="5">
                                        <asp:Panel ID="pnIngPrato" runat="server" Height="100px" ScrollBars="Auto">
                                            <asp:GridView ID="grdIngPrato" runat="server" AutoGenerateColumns="False" OnRowCommand="grdIngPrato_RowCommand" style="text-align: center" Width="770px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Prato">
                                                        <ItemTemplate>
                                                            <div class="auto-style7">
                                                                <asp:Label ID="lblPrato" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantidade">
                                                        <ItemTemplate>
                                                            <div class="auto-style7">
                                                                <asp:Label ID="lblQuantidade" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValor" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField ButtonType="Image" CommandName="Apagar" HeaderText="Apagar" ImageUrl="~/imgs/base/desabilitar.gif" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style17">&nbsp;</td>
                                    <td colspan="5">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style17">
                                        <asp:Button ID="btnAplicar" runat="server" CssClass="Aplicar" OnClick="btnAplicar_Click" Text="    Aplicar" Visible="False" />
                                    </td>
                                    <td colspan="6">
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" Visible="False" />
                                        <asp:Button ID="btnVoltar" runat="server" CssClass="VoltarEati" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style17">
                                        <asp:Button ID="btnAplicarExterno" runat="server" CssClass="Aplicar" OnClick="btnAplicarExterno_Click" Text="    Aplicar" Visible="False" />
                                    </td>
                                    <td colspan="6">
                                        <asp:Button ID="btnLimparExterno" runat="server" CssClass="Limpar" OnClick="btnLimpar_Click" Text="    Limpar" Visible="False" />
                                        <asp:Button ID="btnVoltar2" runat="server" CssClass="VoltarEati" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" Visible="False" />
                                    </td>
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