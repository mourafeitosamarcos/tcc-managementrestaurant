<%@ Page Title="" Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="relatorio_comercial.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.relatorios.RelatorioComercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #masterMenus {
        width: 1000px;
            height: 236px;
        }

         #Administrativo {
            margin-top: 10px;
            margin-left: 20px;
            width: 955px;
        }

        #TituloAdministrativo {
            margin-top: 10px;
            margin-left: 20px;
            width: 300px;
            font-family: Verdana;
            font-size:medium;
            color:#000000;
        }

        #RelAdministrativo1 {
            position:absolute;
            margin-top: 40px;
            margin-left: 20px;
            width: 293px;
            height: 30px;
            font-family: Verdana;
            text-align:center;
            color:#ffffff;
            -moz-border-radius: 7px 7px 7px 7px;
            -webkit-border-radius: 7px 7px 7px 7px;
            border-radius:7px 7px 7px 7px; 
            background: -moz-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #fe984f), color-stop(100%, #ff6a00 ));
            background: -webkit-linear-gradient(top, #fe984f 0%, #000000  100%);
            background: -o-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -ms-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: linear-gradient(to bottom, #fe984f 0%, #ff6a00  100%);
            filter: progid:DXImageTransform.Microsoft.Gradient(StartColorStr='#fe984f', EndColorStr='#ff6a00 ', GradientType=0);
        }

        #RelAdministrativo2 {
            position:absolute;
            margin-top: 40px;
            margin-left: 460px;
            width: 293px;
            height: 30px;
            font-family: Verdana;
            text-align:center;
            color:#ffffff;
            -moz-border-radius: 7px 7px 7px 7px;
            -webkit-border-radius: 7px 7px 7px 7px;
            border-radius:7px 7px 7px 7px; 
            background: -moz-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #fe984f), color-stop(100%, #ff6a00 ));
            background: -webkit-linear-gradient(top, #fe984f 0%, #000000  100%);
            background: -o-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -ms-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: linear-gradient(to bottom, #fe984f 0%, #ff6a00  100%);
            filter: progid:DXImageTransform.Microsoft.Gradient(StartColorStr='#fe984f', EndColorStr='#ff6a00 ', GradientType=0);
        }

        #RelAdministrativo3 {
            position:absolute;
            margin-top: 90px;
            margin-left: 20px;
            width: 293px;
            height: 30px;
            font-family: Verdana;
            text-align:center;
            color:#ffffff;
            -moz-border-radius: 7px 7px 7px 7px;
            -webkit-border-radius: 7px 7px 7px 7px;
            border-radius:7px 7px 7px 7px; 
            background: -moz-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #fe984f), color-stop(100%, #ff6a00 ));
            background: -webkit-linear-gradient(top, #fe984f 0%, #000000  100%);
            background: -o-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -ms-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: linear-gradient(to bottom, #fe984f 0%, #ff6a00  100%);
            filter: progid:DXImageTransform.Microsoft.Gradient(StartColorStr='#fe984f', EndColorStr='#ff6a00 ', GradientType=0);
        }
        
        #RelAdministrativo4 {
            position:absolute;
            margin-top: 90px;
            margin-left: 460px;
            width: 293px;
            height: 30px;
            font-family: Verdana;
            text-align:center;
            color:#ffffff;
            -moz-border-radius: 7px 7px 7px 7px;
            -webkit-border-radius: 7px 7px 7px 7px;
            border-radius:7px 7px 7px 7px; 
            background: -moz-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #fe984f), color-stop(100%, #ff6a00 ));
            background: -webkit-linear-gradient(top, #fe984f 0%, #000000  100%);
            background: -o-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -ms-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: linear-gradient(to bottom, #fe984f 0%, #ff6a00  100%);
            filter: progid:DXImageTransform.Microsoft.Gradient(StartColorStr='#fe984f', EndColorStr='#ff6a00 ', GradientType=0);
        }

        #RelAdministrativo5 {
            position:absolute;
            margin-top: 140px;
            margin-left: 20px;
            width: 293px;
            height: 30px;
            font-family: Verdana;
            text-align:center;
            color:#ffffff;
            -moz-border-radius: 7px 7px 7px 7px;
            -webkit-border-radius: 7px 7px 7px 7px;
            border-radius:7px 7px 7px 7px; 
            background: -moz-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #fe984f), color-stop(100%, #ff6a00 ));
            background: -webkit-linear-gradient(top, #fe984f 0%, #000000  100%);
            background: -o-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -ms-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: linear-gradient(to bottom, #fe984f 0%, #ff6a00  100%);
            filter: progid:DXImageTransform.Microsoft.Gradient(StartColorStr='#fe984f', EndColorStr='#ff6a00 ', GradientType=0);
        }
        
        #RelAdministrativo6 {
            position:absolute;
            margin-top: 140px;
            margin-left: 460px;
            width: 293px;
            height: 30px;
            font-family: Verdana;
            text-align:center;
            color:#ffffff;
            -moz-border-radius: 7px 7px 7px 7px;
            -webkit-border-radius: 7px 7px 7px 7px;
            border-radius:7px 7px 7px 7px; 
            background: -moz-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #fe984f), color-stop(100%, #ff6a00 ));
            background: -webkit-linear-gradient(top, #fe984f 0%, #000000  100%);
            background: -o-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: -ms-linear-gradient(top, #fe984f 0%, #ff6a00  100%);
            background: linear-gradient(to bottom, #fe984f 0%, #ff6a00  100%);
            filter: progid:DXImageTransform.Microsoft.Gradient(StartColorStr='#fe984f', EndColorStr='#ff6a00 ', GradientType=0);
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="masterMenus">
        <div id="Administrativo">
            <div id="TituloAdministrativo"> <em>Relatórios Comerciais</em></div>
            <div id="RelAdministrativo1">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnQuantidadeCli" runat="server" OnClick="lbtnQuantidadeCli_Click">QUANTIDADE DE CLIENTES POR ESTADO</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RelAdministrativo2">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnQtdPedTPCliente" runat="server" OnClick="lbtnQtdPedTPCliente_Click">QUANTIDADE DE PEDIDO - TIPO DE CLIENTE</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <%--<div id="RelAdministrativo3">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lblquantReservas" runat="server" OnClick="lblquantReservas_Click">QUANTIDADE DE RESERVAS POR DIA</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>--%>

<%--            <div id="RelAdministrativo5">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:HyperLink ID="HyperLink4" runat="server">TOTAL DE FUNCIONÁRIOS ADMITIDOS</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="RelAdministrativo6">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:HyperLink ID="HyperLink5" runat="server">TOTAL DE FUNCIONÁRIOS DEMITIDOS</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>--%>
            
        </div>
    </div>
</asp:Content>