<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaEnvioSolicitudesReplica.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaEnvioSolicitudesReplica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OnCloseWindow() {
            GetRadWindow().Close();
        }

        function OpenSelectionWindows(pURL, pVentana, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var WindowsProperties = {
                width: browserWnd.innerWidth - 850,
                height: browserWnd.innerHeight - 200
            };
            openChildDialog(pURL, pVentana, pTitle, WindowsProperties);
        }

        function OpenSelectionWindowsFinal(pURL, pVentana, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var WindowsProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pVentana, pTitle, WindowsProperties);
        }

        function OpenSendMessage(pIdPeriodo) {
            if (pIdPeriodo != null) {
                OpenSelectionWindows("../EO/VentanaConfiguraFechaEnvioReplicas.aspx?PeriodoId=" + pIdPeriodo, "WinPeriodoDesempeno", "Asigna fechas de envio solicitudes");
            }
        }

        function OpenSendMessageFinal(pIdPeriodo) {
            if (pIdPeriodo != null) {
                OpenSelectionWindowsFinal("../EO/VentanaSolicitudesReplicas.aspx?PeriodoId=" + pIdPeriodo, "WinPeriodoDesempeno", "Envío de solicitudes");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnDiasPrevios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDiasPrevios" LoadingPanelID="RadAjaxLoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="txtDiasPosteriores" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDiasPosteriores">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDiasPrevios" LoadingPanelID="RadAjaxLoadingPanel1"/>
                     <telerik:AjaxUpdatedControl ControlID="txtDiasPosteriores" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCierrePeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDiasPrevios" LoadingPanelID="RadAjaxLoadingPanel1"/>
                     <telerik:AjaxUpdatedControl ControlID="txtDiasPosteriores" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCreaPeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDiasPrevios" LoadingPanelID="RadAjaxLoadingPanel1"/>
                     <telerik:AjaxUpdatedControl ControlID="txtDiasPosteriores" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFechaEnvio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDiasPrevios" LoadingPanelID="RadAjaxLoadingPanel1"/>
                     <telerik:AjaxUpdatedControl ControlID="txtDiasPosteriores" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100%-60px);">
        <div style="clear: both; height: 10px;"></div>
        <label class="labelTitulo">Envío de solicitudes de réplica </label>
        <label id="lbEnvioSolReplicas" runat="server"></label>
        <div style="clear: both; height: 10px;"></div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnDiasPrevios" AutoPostBack="true" runat="server" Checked="true" ButtonType="ToggleButton" ToggleType="Radio" GroupName="EnvioSolicitud" Text="Número de días previos al cierre del período. (Aplica tanto del original como de cada réplica)"></telerik:RadButton>
            <div class="ctrlBasico" style="margin-left:25px;">
            <telerik:RadTextBox ID="txtDiasPrevios" runat="server" Width="150" InputType="Number" MaxLength="12" Visible="false"></telerik:RadTextBox>
            </div>
            <telerik:RadButton ID="btnDiasPosteriores" AutoPostBack="true" runat="server" ButtonType="ToggleButton" ToggleType="Radio" GroupName="EnvioSolicitud" Text="Número de días posteriores al cierre del período. (Aplica tanto del original como de cada réplica)"></telerik:RadButton>
             <div class="ctrlBasico" style="margin-left:25px;">
             <telerik:RadTextBox ID="txtDiasPosteriores" runat="server" Width="150" InputType="Number" MaxLength="12" Visible="false"></telerik:RadTextBox>
             </div>
            <telerik:RadButton ID="btnCierrePeriodo" AutoPostBack="true" runat="server" ButtonType="ToggleButton" ToggleType="Radio" GroupName="EnvioSolicitud" Text="El día del cierre del período. (Aplica tanto del original como de cada réplica)"></telerik:RadButton>
            <telerik:RadButton ID="btnCreaPeriodo" AutoPostBack="true" runat="server" ButtonType="ToggleButton" ToggleType="Radio" GroupName="EnvioSolicitud" Text="El día de la creación del período. (Que corresponde a la fecha específica para el período original y cada réplica)"></telerik:RadButton>
            <telerik:RadButton ID="btnFechaEnvio" AutoPostBack="true" runat="server" ButtonType="ToggleButton" ToggleType="Radio" GroupName="EnvioSolicitud" Text="Selecciona una fecha en específico. (Para cada período tanto el original como cada réplica)"></telerik:RadButton>
        </div>
        <div style="clear:both; height:10px"></div>
        <div class="divControlDerecha">
        <telerik:RadButton ID="btnAceptar" runat="server" Width="100" Text="Aceptar" AutoPostBack="true" OnClick="btnAceptar_Click"></telerik:RadButton>
             <telerik:RadButton ID="btnCancelar" runat="server" Width="100" Text="Cancelar" AutoPostBack="false" OnClientClicking="OnCloseWindow"></telerik:RadButton>
        </div>
    </div>
     <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
