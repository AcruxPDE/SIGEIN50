<%@ Page Language="C#" MasterPageFile="MenuMA.Master" AutoEventWireup="true" CodeBehind="DiseñoReportes.aspx.cs" Inherits="SIGE.WebApp.ModulosApoyo.DiseñoReportes" %>

<%@ Register assembly="Stimulsoft.Report.WebDesign, Version=2016.1.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" namespace="Stimulsoft.Report.Web" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%; height:100%">
        <iframe id="ifDiseñoReportes" style="border:none; width:100%; height:100%;" src="stiDiseñoReportes.aspx"></iframe>
    </div>    
    <script type="text/javascript">
        function CargarReporte() {            
            var url = window.location.href;
            if (url.substring(url.length, url.length - 4) == ".mrt") {
                url = url.substring(url.length, 60);
                var iframe = document.getElementById('ifDiseñoReportes');
                iframe.src = iframe.src + "?ruta=" + url + "";
            }
        }
        CargarReporte();

    </script>
</asp:Content>
