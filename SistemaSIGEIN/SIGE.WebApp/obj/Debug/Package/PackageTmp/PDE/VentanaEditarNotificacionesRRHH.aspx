<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaEditarNotificacionesRRHH.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaEditarNotificacionesRRHH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function closeWindow() {
            GetRadWindow().close();
        }
    

        function GetRadWindow() {
            var oWindow = null;
            if
                (window.radWindow) oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function OnClientLoad(editor) {
            //set the width and height of the RadEditor
            editor.setSize("740", "200");
        }
    </script>
    <style>
        .Instrucciones {
            padding-left: 5px;
        }

        .btnRRHH {
            width: 1200px;
            padding-left: 980px;
            padding-top: 30px;
        }

        .btnAdjuntar {
            background: white;
        }

        .Uploadfiles {
            float: right;
        }

        html {
            overflow: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramEditarNotificacionesRRHH" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCancelar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rsEdicionTramites" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rsEdicionTramites">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rsEdicionTramites" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 60px);">

        <telerik:RadSplitter ID="rsEdicionTramites" BorderSize="0" Width="100%" Height="100%" runat="server">
            <telerik:RadPane ID="rpEdicionTramites" runat="server">
                <div style="padding-top: 20px;">
                    <label class="labelTitulo">Edición de mensajes de instrucciones:</label>
                </div>
                <div style="float: left; margin-left: 50px; margin-top: 20px; margin-right: 50px;">
                    <div style="height:33.3%">
                    <div class="ctrlBasico" style="width:30%" >
                        <label id="lblInstruccionesRN" runat="server"><strong>Instrucciones para registrar notificaciones</strong> </label>
                    </div>
                    <div class="ctrlBasico" style="padding-right: 10px;width:70%">
                        <telerik:RadEditor OnClientLoad="OnClientLoad" Width="100%"  EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                    </div>
                    </div>
                    <div style="clear: both; height: 20px;"></div>
                    <div style="height:33.3%">
                    <div class="ctrlBasico" style="width:30%">
                        <label id="Label1" runat="server"><strong>Instrucciones para administrar notificaciones</strong> </label>
                    </div>
                    <div class="ctrlBasico" style="padding-right: 10px;width:70%">
                        <telerik:RadEditor OnClientLoad="OnClientLoad" Width="100%"  EditModes="Design" ID="txtDsNotas2" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                    </div>
                    </div>
                    <div style="clear: both; height: 20px;"></div>
                    <div style="height:33.3%">
                    <div class="ctrlBasico" style="width:30%">
                        <label id="Label2" runat="server"><strong>Instrucciones para agregar comunicados</strong> </label>
                    </div>
                    <div class="ctrlBasico" style="padding-right: 10px;width:70%">
                        <telerik:RadEditor OnClientLoad="OnClientLoad" Width="100%" EditModes="Design" ID="txtDsNotas3" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                    </div>
                  </div>
                </div>


            </telerik:RadPane>

            <telerik:RadPane ID="rpAyudaEdicionDeTramites" runat="server" Scrolling="None" Width="20px">

                <telerik:RadSlidingZone ID="rszAyudaEdicionDeTramites" runat="server" SlideDirection="Left" ExpandedPaneId="rsEdicionDeTramites" Width="20px">
                    <telerik:RadSlidingPane ID="rspAyudaEdicionDeTramites" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="90%">
                        <div style="text-align: justify;">
                            <p>Ésta página te permite modificar el mensaje de instrucciones para notificación a RRHH.</p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Width="100" OnClick="btnGuardar_Click"></telerik:RadButton>
        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" Width="100" AutoPostBack="true" OnClientClicking="closeWindow"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>

</asp:Content>
