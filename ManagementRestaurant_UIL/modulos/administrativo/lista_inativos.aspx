<%@ Page Title="" Language="C#" MasterPageFile="~/master/master.Master" AutoEventWireup="true" CodeBehind="lista_inativos.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.administrativo.lista_inativos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">




        .auto-style1 { font-size: small; }

        .auto-style2 { width: 72%; }

        .auto-style5 { height: 17px; }

        .auto-style10 { text-align: center; }

        .auto-style12 { width: 75px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; text-align: right; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Pesquisa de Funcionarios Inativos</legend>
            <table class="auto-style2">
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="4" rowspan="3">
                        <asp:Panel ID="pnCadastro" runat="server" Width="1054px">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style10">
                                        &nbsp;</td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td colspan="2">
                                        &nbsp;</td>
                                    <td class="auto-style12">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td colspan="2">&nbsp;</td>
                                    <td class="auto-style12">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">Parametro:</td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtPesquisa" runat="server" style="text-align: left" Width="185px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">Pesquisar por:</td>
                                    <td style="text-align: center">
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
                                    <td class="auto-style12">
                                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Botoes" OnClick="btnPesquisar_Click" style="text-align: right" Text="Buscar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td class="auto-style12">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style10" colspan="5">
                                        <asp:Panel ID="pGridPesquisa" runat="server" ScrollBars="Vertical">
                                            <asp:GridView ID="grdFuncionarios" runat="server" AutoGenerateColumns="False" OnRowCommand="grdFuncionarios_RowCommand" style="margin-top: 24px" Width="1025px">
                                                <Columns>
                                     <asp:TemplateField HeaderText="Nome Funcionário">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtNome" CommandName="Ver" runat="server" CommandArgument='<%#DataBinder. Eval (Container, "RowIndex") %>' Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cargo">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtCargo" runat="server"  CommandName="Ver" CommandArgument='<%#DataBinder.Eval(Container,"RowIndex") %>' Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CPF">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtCPF"  CommandName="Ver" runat="server" CommandArgument='<%#DataBinder.Eval(Container,"RowIndex") %>' Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RG">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtRG" runat="server"  CommandArgument='<%#DataBinder.Eval(Container,"RowIndex") %>' Font-Size="Small"  CommandName="Ver" ></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CEP">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtCEP" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>'  CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Endereço">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtEndereco" runat="server"  Font-Size="Small" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bairro">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtBairro" runat="server" Font-Size="Small" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Data de Admissão">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtDtAdm" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField CommandName="Habilitar"  Text="Habilitar Conta" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10" colspan="2">&nbsp;</td>
                                    <td colspan="2">&nbsp;</td>
                                    <td class="auto-style12">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">
                        <asp:Button ID="btnAtivos" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/administrativo/lista_ativos.aspx" style="text-align: right" Text="Ativos" />
                        <asp:Button ID="btnLimpar" runat="server" CssClass="Botoes" Text="Limpar" PostBackUrl="~/modulos/administrativo/lista_inativos.aspx" />
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />
        <br />
    </asp:Panel>
</asp:Content>