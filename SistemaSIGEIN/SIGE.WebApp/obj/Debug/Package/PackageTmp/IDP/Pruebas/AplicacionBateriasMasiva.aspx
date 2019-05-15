<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="AplicacionBateriasMasiva.aspx.cs" Inherits="SIGE.WebApp.IDP.Pruebas.AplicacionBateriasMasiva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OpenBuscarCandidato() {
            var windowProperties = {
                width: 900,
                height: document.documentElement.clientHeight - 20
            };
            openChildDialog("VentanaObtenerFolioSolicitud.aspx", "winSeleccion", "Recuperación de folio de solicitud de empleo", windowProperties);
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var textbox = $find('<%= txtFolio.ClientID %>');
                textbox.set_value(pDato[0].clSolicitud);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 40px); clear: both; padding: 20px;">
        <div style="height: 20px; clear: both;"></div>
        <label class="labelTitulo" style="color: #C6DB95 !important;">Aplicación de pruebas al candidato - Bienvenid@</label>
        <div style="clear: both;"></div>
        <fieldset style="margin-left: 5px; margin-right: 5px;">
            <legend>
                <label style="font-size: 21px;">Bienvenido al módulo de evaluación por competencias</label>
            </legend>
            <div id="txtMensaje" runat="server" style="width: 98%; text-align: justify; margin-left: 5px; margin-right: 5px;">
                El número de folio fue generado automáticamente al llenar la solicitud de empleo o fue proporcionado por tu asesor.
            Si tienes dudas, por favor consúltanos, estamos para servirte.
            </div>
            <br />
            <br />
            <div class="ctrlBasico" style="width: 100%;">
                <div class="divControlIzquierda" style="width: 48%;">
                    <label id="lbTexto1">Favor de teclar tu folio de solicitud de empleo:</label>
                </div>
                <div class="divControlDerecha" style="width: 50%;">
                    <telerik:RadTextBox ID="txtFolio" runat="server" Width="200" MaxLength="10"></telerik:RadTextBox>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div style="text-align: center;">
                ¿Olvidaste tu número de folio?  haz clic en el botón "Buscar" para obtener este número por medio de tu nombre y apellido.
            </div>
        </fieldset>
        <div style="clear: both; height: 5px;"></div>
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClicked="OpenBuscarCandidato"></telerik:RadButton>
        </div>
        <div class="divControlDerecha" style="margin-right: 5px;">
            <telerik:RadButton runat="server" ID="btnIniciarPrueba" Text="Siguiente" AutoPostBack="true" OnClick="btnIniciarPrueba_Click"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
