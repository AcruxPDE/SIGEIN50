<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/ContextoPrueba.Master" AutoEventWireup="true" CodeBehind="PruebaBienvenida.aspx.cs" Inherits="SIGE.WebApp.IDP.Pruebas.PruebaBienvenida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script type="text/javascript">
        function IniciarPrueba() {

            var vFlBateria = '<%# vFlBateria %>';
            var vClTokenExterno = '<%# vClTokenExterno %>';

            var win = window.open("Default.aspx?ID=" + vFlBateria + "&T=" + vClTokenExterno + "&ty=sig", '_self', true);
            win.focus();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Folio de solicitud:</label></td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtFolioSolicitud" runat="server"></div>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Candidato:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtNombreCandidato" runat="server"></div>
                </td>
            </tr>
        </table>
    </div>
    <div style="clear: both;"></div>

    <fieldset style="margin-left: 5px; margin-right: 5px;">
        <legend>
            <label style="font-size:21px;">Bienvenido</label>
        </legend>

        <div id="txtMensajeBienvenida" runat="server" style="width: 98%; text-align: justify; margin-left: 5px; margin-right: 5px;"></div>

    </fieldset>

    <div style="clear: both; height: 5px;"></div>

    <div class="divControlDerecha" style="margin-right: 5px;">
        <telerik:RadButton runat="server" ID="btnIniciarPrueba" Text="Iniciar pruebas" AutoPostBack="false" OnClientClicked="IniciarPrueba"></telerik:RadButton>
    </div>


    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
