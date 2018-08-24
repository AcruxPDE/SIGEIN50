<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stiConsultaReportes.aspx.cs" Inherits="SIGE.WebApp.ModulosApoyo.stiConsultaReportes" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2016.1.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <cc1:StiWebViewer ID="StiConsultaReporte" runat="server" Height="100%" Width="100%" 
                            BackColor="White" BorderColor="White" ToolBarBackColor="240, 240, 240" ImagesPath="Images/"
                             RenderMode="AjaxWithCache" ViewMode="WholeReport" />
            </div>
        </form>
    </body>
</html>
