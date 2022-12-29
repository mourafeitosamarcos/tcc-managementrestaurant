<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.home.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 60%;
        }

        .auto-style5 {
            width: 100%;
        }
        .auto-style6 {
            width: 230px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">
                <table class="auto-style5">
                    <tr>
                        <td class="auto-style6">
                            <asp:Panel ID="pnCorpoGridProdutos" runat="server" Visible="False">
                                <table class="auto-style1">
                                    <tr>
                                        <td style="background-color: #E46D18">
                                            <asp:Panel ID="pnCabecaGridProdutos" runat="server" BorderColor="Black" BorderWidth="2px" style="text-align: center">
                                                <strong>INGREDIENTES NO ESTOQUE</strong></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnScroll" runat="server" Height="220px" ScrollBars="Vertical">
                                                <asp:UpdatePanel ID="upnGridProdutos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grdIngredientes" runat="server" AutoGenerateColumns="False" Height="150px" Width="245px">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Ingrediente">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIngrediente" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantidade">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
                                                        </asp:Timer>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <ajaxToolkit:DropShadowExtender ID="pnCorpoGridProdutos_DropShadowExtender" runat="server" Enabled="True" Rounded="True" TargetControlID="pnCorpoGridProdutos">
                            </ajaxToolkit:DropShadowExtender>
                            <ajaxToolkit:DragPanelExtender ID="pnCorpoGridProdutos_DragPanelExtender" runat="server" DragHandleID="pnCabecaGridProdutos" Enabled="True" TargetControlID="pnCorpoGridProdutos">
                            </ajaxToolkit:DragPanelExtender>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>