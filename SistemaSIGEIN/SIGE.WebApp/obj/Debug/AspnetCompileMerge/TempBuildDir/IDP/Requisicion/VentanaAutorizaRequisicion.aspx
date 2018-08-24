<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAutorizaRequisicion.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAutorizaRequisicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
       .RadButton.rbSkinnedButton.uncheckedYes {
            background-color: #eee !important;
        }

       .RadButton.rbSkinnedButton.uncheckedYes > .rbDecorated {
                color: #000 !important;
        }

       .RadButton.rbSkinnedButton.checkedNo {
            background-color: #eee !important;
        }

       .RadButton.rbSkinnedButton.checkedNo > .rbDecorated {
                color: #333 !important;
        }

       .RadButton.rbSkinnedButton.uncheckedNo {
            background-color: #eee !important;
        }

       .RadButton.rbSkinnedButton.uncheckedNo > .rbDecorated {
                color: #000 !important;
        }

    </style>

    <script>

        function OnCloseWindow() {
            window.location = "/Logout.aspx"
        }

        function OpenPreview() {
            var vURL = "VentanaVistaDescriptivo.aspx";
            var vTitulo = "Vista previa descriptivo";
            var vIdPuesto = '<%#vIdPuesto%>';

            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };

            vURL = vURL + "?PuestoId=" + vIdPuesto;
            var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
      <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnAutorizarReq" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="btnRechazarReq" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="height: calc(100% - 40px);">
        <div style="clear: both; height: 10px;"></div>
        <div style="padding-left: 40px;">

            <fieldset style="width: 98%">
                <legend>
                    <label>Requisición</label>
                </legend>

                <div style="height: 20px;"></div>

                <table class="ctrlTableForm" style="padding-left: 50px;">
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label id="lbClave" name="lblClave" runat="server">Clave:</label>
                        </td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtClaveRequisicion" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                </table>

                <div style="height: 20px;"></div>
            </fieldset>

            <div style="height: 20px;"></div>

            <fieldset style="width: 98%">
                <legend>
                    <label>Datos de la requisición</label>
                </legend>

                <div style="height: 30px;"></div>

                <div class="ctrlBascio">
                    <table class="ctrlTableForm" runat="server" style="padding-left: 50px;">
                        <tr>
                            <%-- Puesto a cubrir--%>
                            <td class="ctrlTableDataContext">
                                <label id="lblPuesto" name="lblPuesto" runat="server">Puesto a cubrir:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtPuesto" runat="server" style="min-width: 100px;"></div>
                                <a id="txtPUestoEnlace" runat="server" href="javascript:OpenPreview();"></a>
                            </td>

                            <%-- Solicitado por--%>

                            <td class="ctrlTableDataContext">
                                <label id="Label13" name="areas" runat="server">Solicitado por:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="rtbSolicita" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>

                        <tr>
                            <%-- Fecha de solicitud --%>
                            <td class="ctrlTableDataContext">
                                <label id="Label2" name="lblNbIdioma" runat="server">Fecha de solicitud:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="rdpFeSolicitud" runat="server" style="min-width: 100px;"></div>
                            </td>

                            <%-- Fecha de requisicion --%>

                            <td class="ctrlTableDataContext">
                                <label id="Label10" name="lblNbIdioma" runat="server">Fecha de requisición:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="rdpFeRequisicion" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>

                        <tr runat="server" id="rowSuplente">
                            <%-- Empleado a suplir--%>
                            <td class="ctrlTableDataContext">
                                <label id="lblSuplir" name="lblSuplir" runat="server">Empleado a suplir:</label>
                            </td>
                            <td colspan="4" class="ctrlTableDataBorderContext">
                                <div id="txtEmpleadoSuplir" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>

                        <tr>
                            <%-- Sueldo sugerido--%>

                            <td class="ctrlTableDataContext">
                                <label id="lblSueldoSugerido" name="lblSueldoSugerido" runat="server">Sueldo sugerido:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtSueldoSugerido" runat="server" style="min-width: 100px;"></div>
                            </td>

                            <%-- Sueldo actual --%>

                            <td class="ctrlTableDataContext">
                                <label id="Label1" name="lblSuplir" runat="server">Sueldo actual:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtSueldoActual" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>

                        <tr>
                            <%-- Sueldo minimo --%>
                            <td class="ctrlTableDataContext">
                                <label id="lblSueldoMinimo" name="lblSueldoMinimo" runat="server">Sueldo mínimo:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtSueldoMinimo" runat="server" style="min-width: 100px;"></div>
                            </td>

                            <%-- sueldo Maximo --%>
                            <td class="ctrlTableDataContext">
                                <label id="lblSueldoMaximo" name="lblSueldoMaximo" runat="server">Sueldo máximo:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtSueldoMaximo" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>

                        <%-- Causa de la vacante --%>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label id="Label15" name="lblNbIdioma" runat="server">Causa de la requisición:</label>
                            </td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="rtbCausaRequisicion" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                    </table>
                </div>

            </fieldset>

            <%-- Comentarios del puesto --%>

            <div class="ctrlBasico" style="width:100%; padding-right: 20px;" runat="server" id="divComentariosPuesto">
                <fieldset style="width: 100%">
                    <legend>
                        <label>Comentarios del puesto:</label>
                    </legend>
                    <telerik:RadTextBox ID="rtbObservacionesPuesto" runat="server" Width="99%" MaxLength="1000" TextMode="MultiLine" Height="100"></telerik:RadTextBox>
                </fieldset>

                <div style="height: 20px;"></div>
                <div class="divControlDerecha" style="padding-right: 20px;">
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnAutorizar" ButtonType="ToggleButton" Text="Autorizar puesto nuevo" ToggleType="Radio" GroupName="Puesto" AutoPostBack="false">
                            <%--<ToggleStates>
                                <telerik:RadButtonToggleState Text="Autorizar puesto nuevo" CssClass="checkedYes" />
                                <telerik:RadButtonToggleState Text="Autorizar puesto nuevo" CssClass="uncheckedYes" />
                            </ToggleStates>--%>
                        </telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnRechazar" ButtonType="ToggleButton" Text="Rechazar puesto nuevo" ToggleType="Radio" GroupName="Puesto" AutoPostBack="false">
                            <%--<ToggleStates>
                                <telerik:RadButtonToggleState Text="Rechazar puesto nuevo" CssClass="checkedNo" />
                                <telerik:RadButtonToggleState Text="Rechazar puesto nuevo" CssClass="uncheckedNo" />
                            </ToggleStates>--%>
                        </telerik:RadButton>
                    </div>
                </div>
            </div>

            <div style="height: 40px;"></div>

            <div class="ctrlBasico" style="width:100%; padding-right: 20px;" runat="server" id="divComentariosRequisicion">
                <fieldset style="width: 100%">
                    <legend>
                        <label>Comentarios de la requisición:</label>
                    </legend>
                    <telerik:RadTextBox ID="txtObservacionesRequisicion" runat="server" Width="99%" MaxLength="1000" TextMode="MultiLine" Height="100"></telerik:RadTextBox>
                </fieldset>

                <div style="height: 20px;"></div>

                <div class="divControlDerecha" style="padding-right: 20px;">
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnAutorizarReq" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Req" Text="Autorizar requisición" AutoPostBack="false"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnRechazarReq" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Req" Text="Rechazar requisición" AutoPostBack="false"></telerik:RadButton>
                    </div>
                </div>
            </div>

            <div style="height: 20px;"></div>

            <div class="divControlDerecha">
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" ID="btnGuardar" UseSubmitBehavior="false" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                </div>
            </div>

        </div>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winVistaPrevia" runat="server" Title="Vista previa" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensajeError" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
