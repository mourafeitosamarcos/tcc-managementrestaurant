<%@ Page Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="lista_inativos.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.lista_inativos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">



        .auto-style2 { width: 1050px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Pesquisa de Funcionários Inativos</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="6">&nbsp;</td>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td>Parametro:</td>
                    <td>
                        <asp:TextBox ID="txtPesquisa" runat="server" style="text-align: left" Width="185px"></asp:TextBox>
                    </td>
                    <td>Pesquisar por:</td>
                    <td>
                        <asp:DropDownList ID="ddlColuna" runat="server">
                            <asp:ListItem Value="Fun_Nome">Nome</asp:ListItem>
                            <asp:ListItem Value="Cargo_Nome">Cargo</asp:ListItem>
                            <asp:ListItem Value="Fun_CPF">CPF</asp:ListItem>
                            <asp:ListItem Value="Fun_RG">RG</asp:ListItem>
                            <asp:ListItem Value="Fun_Cep">Cep</asp:ListItem>
                            <asp:ListItem Value="Fun_Rua">Endereço</asp:ListItem>
                            <asp:ListItem Value="Admissao_Data">Data de Admissão</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Buscar" OnClick="btnPesquisar_Click" style="text-align: right" Text="    Buscar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Panel ID="pnFuncionarios" runat="server" Height="200px" ScrollBars="Auto">
                        <asp:GridView ID="grdFuncionarios" runat="server" AutoGenerateColumns="False" OnRowCommand="grdFuncionarios_RowCommand" style="margin-top: 24px; text-align: center;" Width="1025px">
                            <Columns>
                                <asp:TemplateField HeaderText="Nome Funcionário">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbtNome" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cargo">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtCargo" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CPF">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbtCPF" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RG">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtRG" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CEP">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtCEP" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Endereço">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbtEndereco" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bairro">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtBairro" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data de Admissão">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtDtAdm" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Habilitar" HeaderText="Habilitar Conta" ImageUrl="../../imgs/base/habilitar.gif" />
                                 <asp:TemplateField HeaderText="Visualizar">
                                    <ItemTemplate>
                                            <asp:ImageButton ID="btnVisualizar" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" ImageUrl="~/imgs/base/visualizar.gif" style="text-align: right" />
                                            <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnAtivos" runat="server" CssClass="Ativo" PostBackUrl="~/modulos/alteracao/lista_ativos.aspx" style="text-align: right" Text="    Ativos" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Limpar" Text="    Limpar" OnClick="btnLimpar_Click" />
                        <asp:Button ID="btnVoltar" runat="server" CssClass="Voltar" Text="    Voltar" PostBackUrl="~/modulos/home/home.aspx" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>