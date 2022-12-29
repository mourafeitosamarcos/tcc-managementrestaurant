<%@ Page Language="C#" MasterPageFile="/master/principal.Master" AutoEventWireup="true" CodeBehind="dados_pedido.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.vendas.dados_pedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


        .auto-style2 { width: 1050px; }

        .auto-style9 { }

        .auto-style10 { height: 88px; }

        .auto-style15 { height: 18px; }

        .auto-style17 { width: 4px; }

        .auto-style18 { text-align: left; }

        .auto-style19 { text-align: center; }

        .auto-style22 {
            height: 18px;
            text-align: left;
        }

        .auto-style24 {
            height: 18px;
            width: 190px;
        }

        .auto-style27 { width: 190px; }

        .auto-style28 { height: 17px; }

        .auto-style29 {
            height: 17px;
            width: 190px;
        }

        .auto-style30 {
            height: 17px;
            text-align: left;
        }

        .auto-style31 {
            height: 20px;
            width: 95px;
        }

        .auto-style32 {
            height: 20px;
            width: 190px;
        }

        .auto-style33 {
            height: 18px;
            width: 95px;
        }

        .auto-style34 {
            height: 17px;
            width: 95px;
        }

        .auto-style35 { width: 95px; }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Dados do Pedido</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="8" class="auto-style17">&nbsp;</td>
                    <td colspan="6" class="auto-style15"></td>
                </tr>
                <tr>
                    <td class="auto-style33">
                        <asp:Label ID="lbltNome" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="185px"></asp:TextBox>
                    </td>
                    <td class="auto-style15">Telefone:</td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtTelefone" runat="server" Enabled="False" MaxLength="14" onkeyup="formataTelefone(this,event)" Width="125px"></asp:TextBox>
                    </td>
                    <td class="auto-style22">Valor</td>
                    <td class="auto-style15">
                        <asp:HiddenField ID="hfTipo" runat="server" />
                        <asp:TextBox ID="txtPValor" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15" colspan="6"></td>
                </tr>
                <tr>
                    <td class="auto-style34">CEP:</td>
                    <td class="auto-style29">
                        <asp:TextBox ID="txtCEP" runat="server" Enabled="False" MaxLength="9" onkeyup="formataCEP(this,event)" Width="90px"></asp:TextBox>
                    </td>
                    <td class="auto-style28">Endereço:</td>
                    <td class="auto-style30">
                        <asp:TextBox ID="txtEndereco" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                    </td>
                    <td class="auto-style28">Número:</td>
                    <td class="auto-style28">
                        <asp:TextBox ID="txtNumero" runat="server" Enabled="False" MaxLength="5" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style34">Bairro:</td>
                    <td class="auto-style29">
                        <asp:TextBox ID="txtBairro" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                    </td>
                    <td class="auto-style28">Cidade:</td>
                    <td class="auto-style30">
                        <asp:TextBox ID="txtCidade" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                    </td>
                    <td class="auto-style28">Estado:</td>
                    <td class="auto-style28">
                        <asp:TextBox ID="txtEstado" runat="server" Enabled="False" MaxLength="2" onkeyup="formataTexto(this,event)" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9" colspan="6">&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style31">Nº Pedido</td>
                    <td class="auto-style32">
                        <asp:TextBox ID="txtNPedido" runat="server" Enabled="False" Width="50px"></asp:TextBox>
                    </td>
                    <td class="auto-style10" colspan="4" rowspan="2">
                        <asp:Panel ID="Pgridpedidos" runat="server" Height="100px" Width="750px" ScrollBars="Auto">
                            <div class="auto-style19">
                                <asp:GridView ID="grdPratos" runat="server" AutoGenerateColumns="False" Height="34px" Width="721px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Prato">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrato" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantidade">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtd" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style35">Entregador:</td>
                    <td class="auto-style27">
                        <asp:DropDownList ID="ddlEntregadores" runat="server" OnLoad="ddlEntregadores_Load" Width="125px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" class="auto-style18">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style18" colspan="7">&nbsp;&nbsp;<asp:Button ID="btnGeraOE" runat="server" CssClass="Imprimir" OnClick="btnGeraOE_Click" style="text-align: right" Text="    Imprimir" />
                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" PostBackUrl="~/modulos/home/home.aspx" Text="    Voltar" />
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>