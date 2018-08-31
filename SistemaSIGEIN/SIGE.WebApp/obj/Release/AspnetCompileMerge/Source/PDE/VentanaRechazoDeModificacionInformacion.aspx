<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaRechazoDeModificacionInformacion.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaRechazoDeModificacionInformacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function cerrarpagina() {

            GetRadWindow().close();

        }

        function closeWindow() {

            GetRadWindow().close();

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <telerik:RadSplitter ID="rsEdicionTramites" BorderSize="0" Width="100%" Height="100%" runat="server">
            <telerik:RadPane ID="rpEdicionTramites" runat="server">

                <div style="clear: both; height: 10px;"></div>
                <label id="lblNombre">Motivo del rechazo</label>

                <div style="clear: both; height: 10px;"></div>

                <telerik:RadTextBox ID="txtDescripcion" Enabled="true" InputType="Text" Width="95%" Height="150" runat="server" TextMode="MultiLine"></telerik:RadTextBox>

            </telerik:RadPane>

            <telerik:RadPane ID="rpAyudaEdicionDeTramites" runat="server" Scrolling="None" Width="20px">

                <telerik:RadSlidingZone ID="rszAyudaEdicionDeTramites" runat="server" SlideDirection="Left" ExpandedPaneId="rsEdicionDeTramites" Width="20px">
                    <telerik:RadSlidingPane ID="rspAyudaEdicionDeTramites" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="90%">
                        <div style="text-align: justify;">
                            <p>Por favor introduce un comentario de la causa del rechazo del cambio.</p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnAceptar" runat="server" Text="Aceptar" Width="100" OnClick="btnAceptar_Click"></telerik:RadButton>

    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
