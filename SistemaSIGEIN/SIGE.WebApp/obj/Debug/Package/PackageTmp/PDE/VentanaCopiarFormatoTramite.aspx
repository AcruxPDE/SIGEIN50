<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaCopiarFormatoTramite.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaCopiarFormatoTramite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function closeWindow() {
            var
            oWnd = GetRadWindow();

            oWnd.close();

           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <telerik:RadSplitter ID="rsEdicionTramites" BorderSize="0" Width="100%" Height="100%" runat="server">
            <telerik:RadPane ID="rpEdicionTramites" runat="server">

                <div style="clear: both; height: 10px;"></div>
                <label id="lblNombre">Nombre</label>

                <div style="clear: both; height: 10px;"></div>
                <telerik:RadTextBox ID="txtNombre" Enabled="true" InputType="Text" Width="100%" Height="30" runat="server"></telerik:RadTextBox>
                <div style="clear: both; height: 10px;"></div>
                <label id="Label1">Descripcion</label>

                <div style="clear: both; height: 10px;"></div>

                <telerik:RadTextBox ID="txtDescripcion" Enabled="true" InputType="Text" Width="100%" Height="80" runat="server" TextMode="MultiLine"></telerik:RadTextBox>

            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Aceptar" Width="100" OnClick="btnGuardar_Click"></telerik:RadButton>

    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
