<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visualiza_relatorio2.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.relatorios.VisualizaRelatorio2" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 145px;
        }
        .auto-style4 {
            font-size: xx-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color:#ffffff">
        <rsweb:ReportViewer ID="rptRelatorios" runat="server" Height="485px" Width="900px">
        </rsweb:ReportViewer>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    </form>
</body>
</html>
