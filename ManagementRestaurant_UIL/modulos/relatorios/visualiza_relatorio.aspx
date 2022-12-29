<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visualiza_relatorio.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.relatorios.VisualizaRelatorio" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <style type="text/css">

            .auto-style1 { width: 100%; }

        </style>
    </head>
    <body>
        <form id="form1" runat="server">
    
            <table align="center" class="auto-style1">
                <tr>
                    <td>
    
                        <rsweb:ReportViewer ID="rptPedidos" runat="server" Width="610px" Height="468px">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
    
                           
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
    
                           
                    </td>
                </tr>
            </table>
    
        </form>
    </body>
</html>