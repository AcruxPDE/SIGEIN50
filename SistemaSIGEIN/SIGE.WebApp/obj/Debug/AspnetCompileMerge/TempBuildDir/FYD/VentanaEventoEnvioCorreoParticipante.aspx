<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaEventoEnvioCorreoParticipante.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaEventoEnvioCorreo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .Etiqueta {
            width: 200px;
        }
    </style>
    <script type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadSplitter ID="spHelp" runat="server" Width="100%" Height="100%" BorderSize="0">

        <telerik:RadPane ID="rpEnvioCorreo" runat="server">

            <div style="height: calc(100% - 50px);">
              <%--  <div style="clear: both; width: 15px;"></div>--%>
               <%-- <div class="ctrlBasico" style="padding-left: 5px;">
                    <label class="Etiqueta">Evento de Capacitación: </label>
                    <telerik:RadTextBox runat="server" ID="txtNombreEvento" Width="300px" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>
                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Evento de Capacitación:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNombreEvento" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>
              <%--  <div style="clear: both; height: 5px;"></div>--%>
              <%--  <div class="ctrlBasico" style="padding-left: 5px;">
                    <label class="Etiqueta">Lugar:</label>
                    <telerik:RadTextBox runat="server" ID="txtLugar" Width="300px" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>
                  <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Lugar:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtLugar" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>


              <%--  <div style="clear: both; height: 5px;"></div>--%>
                <%--<div class="ctrlBasico" style="padding-left: 5px;">
                    <label class="Etiqueta">Fecha de inicio:</label>
                    <telerik:RadTextBox runat="server" ID="txtFechaInicial" Width="150px" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>

                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Fecha de inicio:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtFechaInicial" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>


<%--                <div style="clear: both; height: 5px;"></div>--%>
               <%-- <div class="ctrlBasico" style="padding-left: 5px;">
                    <label class="Etiqueta">Fecha de término:</label>
                    <telerik:RadTextBox runat="server" ID="txtFechaTermino" Width="150px" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>

                 <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Fecha de término:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtFechaTermino" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>


                <div style="clear: both; height: 20px;"></div>
                <label>Se enviará la invitación por medio de Out-Look a los participantes, con el siguiente texto:</label>

                <fieldset id="fsMensaje" runat="server">
                    <legend></legend>
                    <literal id="lMensaje" runat="server"></literal>
                </fieldset>
                <label runat="server" id="lblCaducidad" visible="false" style="color:red;">*** El evento ya ha caducado</label>
            </div>

            <div class="divControlDerecha">
                <telerik:RadButton runat="server" ID="btnAceptar" Text="Aceptar" OnClick="btnAceptar_Click"></telerik:RadButton>
                <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="500" MinWidth="500" Height="100%">
                    <div style="padding: 10px; text-align: justify;">
                    Este proceso envía una invitación por correo electrónico/Out-look a cada uno de los participantes en el evento de capacitación donde fueron convocados.
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
