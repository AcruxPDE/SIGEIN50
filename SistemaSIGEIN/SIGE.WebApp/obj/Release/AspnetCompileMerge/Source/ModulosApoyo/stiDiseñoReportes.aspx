<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stiDiseñoReportes.aspx.cs" Inherits="SIGE.WebApp.ModulosApoyo.stiDiseñoReportes" %>
<%@ Register assembly="Stimulsoft.Report.WebDesign, Version=2016.1.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" namespace="Stimulsoft.Report.Web" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:StiWebDesigner ID="StiDiseñoReportes" runat="server" Width="500px" Height="200px" OnPreInit="StiDiseñoReportes_PreInit" OnSaveReport="StiDiseñoReportes_SaveReport" />
    </div>
    </form>
</body>
</html>
