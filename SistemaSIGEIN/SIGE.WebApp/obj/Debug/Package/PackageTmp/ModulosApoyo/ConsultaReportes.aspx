<%@ Page Language="C#" MasterPageFile="~/ModulosApoyo/MenuMA.Master" AutoEventWireup="true" CodeBehind="ConsultaReportes.aspx.cs" Inherits="SIGE.WebApp.ModulosApoyo.ConsultaReportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CargarReporteDiseño() {
            var tree = $find("<%= rtvReportes.ClientID %>");
            var valor = tree.get_selectedNodes()[0].get_value();
            if (valor != null) {
                window.location.href = "DiseñoReportes.aspx?ruta=" + valor;
            } else {
                radalert("Debe seleccionar un reporte.", 400, 150, "Aviso");
            }
            
        }
        function CargarReporte(arbol) {
            var tree = $find("<%= rtvReportes.ClientID %>");           
            var valor = tree.get_selectedNodes()[0].get_value();
            var iframe = document.getElementById('ifConsultaReportes');            
            iframe.src = iframe.src.substring(59, 0) + "?ruta=" + valor + "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float:left;">
        <h3>Lista de Reportes</h3>
        <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="rtvReportes" Skin="Vista" OnClientNodeClicked="function () {CargarReporte(this);}"  >
        </telerik:RadTreeView>
        <br />
        <telerik:RadButton runat="server" ID="btnActaulizarReporte" Text="Actualizar Reporte" OnClientClicked="CargarReporteDiseño" AutoPostBack="false"></telerik:RadButton>
    </div>
    <div style=" width:5px; height:100%; float:left; border:none;"></div>
    <div style=" width:1px; height:100%; float:left; border: 2px solid #A9BCF5;"></div>
    <div style="float:left; overflow:hidden; width:82%; height:100%">
        <iframe id="ifConsultaReportes" style="border:none; width:100%; height:100%;" src="stiConsultaReportes.aspx"></iframe>
    </div>    
</asp:Content>
