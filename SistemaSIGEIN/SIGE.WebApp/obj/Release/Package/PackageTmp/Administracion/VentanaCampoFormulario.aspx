<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCampoFormulario.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCampoFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        var SelectPageView = function (sender, args) {
            //var selectedIndex = $find('<%# cmbTipoControl.ClientID %>').get_selectedIndex();
            //var value = sender.get_value();
            var multiPage = $find('<%= rmpAdicionales.ClientID %>');

            var radPageViewClientId = "";

            switch (sender.get_value()) {
                case "TEXTBOX":
                    radPageViewClientId = '<%= rpvTextbox.ClientID %>';
                    break;
                case "DATEPICKER":
                case "DATEAGE":
                    radPageViewClientId = '<%= rpvDate.ClientID %>';
                    break;
                case "COMBOBOX":
                    radPageViewClientId = '<%= rpvCombobox.ClientID %>';
                    break;
                case "MASKBOX":
                    radPageViewClientId = '<%= rpvMaskbox.ClientID %>';
                    break;
                case "NUMERICBOX":
                    radPageViewClientId = '<%= rpvNumericbox.ClientID %>';
                    break;
                case "CHECKBOX":
                    radPageViewClientId = '<%= rpvCheckbox.ClientID %>';
                    break;
                default:
                    radPageViewClientId = '<%= rpvVacio.ClientID %>';
                    break;
            }
            multiPage.findPageViewByID(radPageViewClientId).set_selected(true);
        }

        function closeWindow() {
            GetRadWindow().close();
        }

        var CambiarNumeroDecimales = function (sender, args) {
            var numericBox = $find('<%= txtNumericboxDefault.ClientID %>');
            numericBox.get_numberFormat().DecimalDigits = args.get_newValue();
            var c = numericBox.get_value();
            numericBox.set_value(++c); // el incremento y decremento es para forzar a que se realice un cambio en el valor del 
            numericBox.set_value(--c); // campo y que el número desplegado muestre correctamente el número de decimales introducido
        }

        var CambiarNumeroEntero = function (sender, args) {
            var numericBox = $find('<%= txtNumericboxDefault.ClientID %>');
            var limiteSuperior = Math.pow(10, args.get_newValue()) - 1;
            var limiteInferior = limiteSuperior * -1;

            numericBox.set_maxValue(limiteSuperior);
            numericBox.set_minValue(limiteInferior);

            var valorCampo = numericBox.get_value();
            if (valorCampo > limiteSuperior)
                numericBox.set_value(limiteSuperior);
            if (valorCampo < limiteInferior)
                numericBox.set_value(limiteInferior);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadToolTipManager ID="rtmEvaluacion" runat="server" AutoTooltipify="true" BackColor="WhiteSmoke" RelativeTo="Element" AutoCloseDelay="60000" Position="TopCenter" Width="300" HideEvent="LeaveTargetAndToolTip"></telerik:RadToolTipManager>
    <telerik:RadAjaxLoadingPanel ID="rlpConfiguracion" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbComboboxCatalogo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbComboboxDefault" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="height: calc(100% - 35px); padding: 10px 0px 10px 0px;">
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblTipoFormulario">Tipo formulario:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadComboBox ID="cmbTipoFormulario" runat="server" ClientDataSourceID="objectDataSourcePlantilla" >
                   
                </telerik:RadComboBox>
                
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblClave">Clave:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClave" runat="server"></telerik:RadTextBox>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblNombre">Nombre:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNombre" runat="server" Width="300"></telerik:RadTextBox>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblTooltip">Tooltip:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtTooltip" runat="server" TextMode="MultiLine" Height="100" Width="300" Resize="Vertical"></telerik:RadTextBox>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblActivo">Activo:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                    </ToggleStates>
                </telerik:RadButton>
            </div>
        </div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblTipoControl">Tipo control:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadComboBox ID="cmbTipoControl" runat="server" OnClientSelectedIndexChanged="SelectPageView" ToolTip="Selecciona el tipo de control que necesitas. Cada uno tiene una configuración específica. Si eliges la opción Lista desplegable recuerda dar de alta primero el catálogo."></telerik:RadComboBox>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div>
            <telerik:RadMultiPage ID="rmpAdicionales" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="rpvVacio" runat="server">
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvTextbox" runat="server">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblTextboxLongitud">Longitud:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox ID="txtTextboxLongitud" runat="server" MinValue="0" MaxValue="4000" Value="100" NumberFormat-DecimalDigits="0" CssClass="RightAligned" Width="50"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblTextboxDefault">Default:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtTextboxDefault" runat="server" Width="300"></telerik:RadTextBox>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvDate" runat="server">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblDateDefault">Default:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadDatePicker ID="txtDateDefault" runat="server"></telerik:RadDatePicker>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvCombobox" runat="server">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblComboboxCatalogo">Catálogo:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadComboBox ID="cmbComboboxCatalogo" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="cmbComboboxCatalogo_SelectedIndexChanged"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblComboboxDefault">Default:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadComboBox ID="cmbComboboxDefault" runat="server" Width="300"></telerik:RadComboBox>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvMaskbox" runat="server">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblMaskboxMascara">Máscara:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtMaskboxMascara" runat="server"></telerik:RadTextBox>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvNumericbox" runat="server">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblNumericboxEnteros">Enteros:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox ID="txtNumericboxEnteros" runat="server" MinValue="0" MaxValue="33" Value="6" NumberFormat-DecimalDigits="0" CssClass="RightAligned" Width="50">
                                <ClientEvents OnValueChanged="CambiarNumeroEntero" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblNumericboxDecimales">Decimales:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox ID="txtNumericboxDecimales" runat="server" MinValue="0" MaxValue="5" Value="2" NumberFormat-DecimalDigits="0" CssClass="RightAligned" Width="50">
                                <ClientEvents OnValueChanged="CambiarNumeroDecimales" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblNumericboxDefault">Default:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox ID="txtNumericboxDefault" runat="server" Value="0" NumberFormat-DecimalDigits="2" CssClass="RightAligned" Width="150"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvCheckbox" runat="server">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblCheckboxDefault">Default:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="chkCheckboxDefault" runat="server" ToggleType="CheckBox" name="chkCheckboxDefault" AutoPostBack="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>
    <div>
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
