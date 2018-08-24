<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="VentanaIncrementoTabulador.aspx.cs" Inherits="SIGE.WebApp.MPC.VentanaIncrementoTabulador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function onCloseWindow(oWnd, args) {
            GetRadWindow().close();
        }

        function OnCloseUpdate() {
            var vTabuladores = [];
            var vTabulador = {
                clTipoCatalogo: "UPDATE"
            };
            vTabuladores.push(vTabulador);
            sendDataToParent(vTabuladores);
        }

        function ConfirmarIncremento(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
            { if (shouldSubmit) { this.click(); } });

            radconfirm('Recuerda que para afectar al inventario de personal deberás seleccionar dicha opción en la pantalla anterior.', callBackFunction, 400, 170, null, "Aviso");
            args.set_cancel(true);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rbMonto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtPrIncremento" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtMontoIncremento" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rbPorcentaje">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtPrIncremento" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtMontoIncremento" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 10px;"></div>
    <telerik:RadSplitter ID="rsIncrementoGeneral" Width="100%" Height="100%" BorderSize="0" runat="server">
        <telerik:RadPane ID="rpIncrementoGeneral" runat="server">
            <div class="ctrlBasico" style="width:100%">
                <table class="ctrlTableForm" style="width:100%">
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Tabulador:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtClTabulador" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Descripción:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtDescripción" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Fecha:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtFecha" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Vigencia:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtVigencia" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 10px; clear: both;"></div>
            <fieldset>
                <legend>
                    <telerik:RadLabel runat="server" ID="lblDsMontoIncremento" Enabled="false" Text="Monto del incremento"></telerik:RadLabel>
                </legend>
                <div style="margin-left: 10px;">
                    <div class="ctrlBasico" style="margin-left:10px;">
                        <telerik:RadButton runat="server" ID="rbPorcentaje" Text="%" AutoPostBack="true" ToggleType="Radio" Width="150px" GroupName="TipoBono">
                            <ToggleStates>
                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                     <div class="ctrlBasico" style="margin-left:10px;">
                        <telerik:RadButton runat="server" ID="rbMonto" Text="$" AutoPostBack="true" ToggleType="Radio" Width="150px" GroupName="TipoBono">
                            <ToggleStates>
                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div style="height: 5px; clear: both;"></div>
                    <div class="ctrlBasico"  style="margin-left:100px;">
                        <telerik:RadNumericTextBox ID="txtMontoIncremento" InputType="Number" Width="150px" runat="server" AutoPostBack="false" NumberFormat-DecimalDigits="2">
                            <EnabledStyle HorizontalAlign="Right" />
                        </telerik:RadNumericTextBox>
                    </div>
                </div>
            </fieldset>
            <div style="height: 20px; clear: both;"></div>
            <div class="divControlDerecha">
                <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" OnClientClicked="onCloseWindow"></telerik:RadButton>
            </div>
            <div class="divControlDerecha">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClientClicking="ConfirmarIncremento" OnClick="btnGuardar_Click"></telerik:RadButton>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyudaIncrementoGeneral" runat="server" Scrolling="None" Width="22px">
            <telerik:RadSlidingZone ID="rszAyudaDefinicionCriterios" runat="server" SlideDirection="Left" ExpandedPaneId="rsDefinicionCriterios" Width="22px">
                <telerik:RadSlidingPane ID="rspAyudaPlaneacionIncrementos" runat="server" Title="Ayuda" Width="250px" RenderMode="Mobile" Height="100%">
                    <div style="padding: 10px; text-align: justify;">
                        <p>
                            Definición de incremento general:
                                <br />
                            Aquí podrás definir un incremento general para todos los empleados seleccionados en la pantalla anterior.
                                <br />
                            Puedes definir un incremento porcentual basado en el sueldo nominal actual o un incremento en monto el cual se sumará al sueldo nominal actual.
                                <br />
                            Recuerda que para afectar al inventario de personal deberás seleccionar dicha opción en la pantalla anterior.
                        </p>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
