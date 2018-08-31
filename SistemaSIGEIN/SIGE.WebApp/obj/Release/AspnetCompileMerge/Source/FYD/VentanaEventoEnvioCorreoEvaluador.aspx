<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaEventoEnvioCorreoEvaluador.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaEventoEnvioCorreoEvaluador" %>

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
                <div style="clear: both; width: 15px;"></div>
                <%--<div class="ctrlBasico" style="padding-left: 5px;">
                    <label class="Etiqueta">Clave del evento: </label>
                    <telerik:RadTextBox runat="server" ID="txtEvento" Width="400px" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>

                <div class="ctrlBasico" >
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Clave del evento:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtEvento" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>

               <%-- <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico" style="padding-left: 5px;">
                    <label class="Etiqueta">Clave del curso:</label>
                    <telerik:RadTextBox runat="server" ID="txtCurso" Width="400px" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>

                 <div class="ctrlBasico" >
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Clave del curso:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtCurso" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>

              <%--  <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico" style="padding-left: 5px;">
                    <label class="Etiqueta">Evaluador:</label>
                    <telerik:RadTextBox runat="server" ID="txtEvaluador" Width="400px" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>

                 <div class="ctrlBasico" >
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Evaluador:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtEvaluador" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>

                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico" style="padding-left: 5px;">
                    <label class="Etiqueta">Correo electrónico:</label>
                    <telerik:RadTextBox runat="server" ID="txtCorreo" Width="400px"></telerik:RadTextBox>
                </div>

                <%-- <div class="ctrlBasico" >
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Correo electrónico:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                 <telerik:RadTextBox runat="server" ID="txtCorreo" Width="400px"></telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </div>--%>

                <div style="clear: both; height: 20px;"></div>
                <label>Se enviará la invitación por medio de correo electrónico al evaluador, con el siguiente texto:</label>

                <fieldset id="fsMensaje" runat="server">
                    <legend></legend>
                    <literal id="lMensaje" runat="server"></literal>
                </fieldset>
                <label runat="server" id="lblCaducidad" visible="false" style="color: red;">*** El evento ya ha caducado</label>
            </div>

            <div class="divControlDerecha">
                <telerik:RadButton runat="server" ID="btnAceptar" Text="Aceptar" OnClick="btnAceptar_Click"></telerik:RadButton>
                <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
            </div>


        </telerik:RadPane>
        <telerik:RadPane ID="rpayuda" runat="server" Width="30">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="500" MinWidth="500" Height="100%">
                    <div style="padding: 10px; text-align: justify;">
                        Este proceso envía una solicitud para evaluación de cuestionarios a cada uno de los correos electrónicos de los evaluadores. Un ejemplo del cuerpo del mensaje se muestra en la pantalla, si deseas modificarlo deberás hacerlo en el menú de configuración del sistema. 
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>

    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
