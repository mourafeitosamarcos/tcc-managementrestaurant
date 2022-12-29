<%@ Page Title="" Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="relatorio_gerencial.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.relatorios.RelatorioGerencial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #masterMenus {
        width: 1000px;
            height: 259px;
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
            height: 257px;
            font-family: Verdana;
            text-align:center;
            color:#ffffff;
            -moz-border-radius: 7px 7px 7px 7px;
            -webkit-border-radius: 7px 7px 7px 7px;
            border-radius:7px 7px 7px 7px;
            top: 36px;
            left: 20px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="masterMenus">
        <div id="Administrativo">
            <div id="TituloAdministrativo"> <em>Relatórios Gerenciais</em></div>
            <div id="RelAdministrativo1">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnProdutoTipo" runat="server" OnClick="lbtnProdutoTipo_Click">QUANTIDADE DE PRODUTO POR TIPO</asp:LinkButton>
                            <%--<a href="" title="Login form"  runat="server"></a>--%>
                               <%--<a href="" title="Login form"  runat="server"></a>--%>
                                
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RelAdministrativo2">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnProdutoEntrada" runat="server" OnClick="lbtnProdutoEntrada_Click">QUANTIDADE DE PRODUTOS ENTRADA</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RelAdministrativo3">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnVeiculosCadastrados" runat="server" OnClick="lbtnVeiculosCadastrados_Click">ENTRADA DE VEICULOS CADASTRADOS</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="RelAdministrativo4">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbnFuncionarios" runat="server" OnClick="lbnFuncionarios_Click">RELATÓRIO GERAL DE FUNCIONÁRIOS</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="RelAdministrativo5">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnDocDepart" runat="server" OnClick="lbtnDocDepart_Click">TOTAL DE DOCUMENTOS POR DEPARTAMENTO</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RelAdministrativo6">
                <table style="width: 293px;height: 30px;">
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imgs/estrutura/turnover.png" OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                </table>
            </div>

           
        </div>
    </div>
    
</asp:Content>
