<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaPreguntaAbierta.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaPreguntaAbierta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function CloseWindow()  {
            var pDatos = [{
            clTipoCatalogo: "PREGUNTA_ABIERTA"

        }];
            cerrarVentana(pDatos);
        }

        function cerrarVentana(recargarGrid) {
            sendDataToParent(recargarGrid);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100%-50px); clear: both">
        <div style="height: 10px; clear: both"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lbNbpregunta">Pregunta:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNbPregunta" runat="server" Width="200" MaxLength="200" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
            </div>
        </div>
         <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lbDsPregunta">Descripción:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtDsPregunta" runat="server" Width="200" MaxLength="300" Height="200" TextMode="MultiLine"></telerik:RadTextBox>
            </div>
        </div>
        <div style="height:10px; clear:both"></div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" Width="100" OnClick="btnAgregar_Click"></telerik:RadButton>
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" Width="100" OnClientClicked="CloseWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
