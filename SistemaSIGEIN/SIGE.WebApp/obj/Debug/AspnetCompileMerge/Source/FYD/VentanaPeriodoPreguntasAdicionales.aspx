<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaPeriodoPreguntasAdicionales.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaPeriodoPreguntasAdicionales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function generateDataForParent() {
            var vCampos = [];

            var vCampo = {
                clTipoCatalogo: "CAMPO_ADICIONAL"
            };
            vCampos.push(vCampo);

            sendDataToParent(vCampos);
        }

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
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpPreguntasAdicionales"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager runat="server" ID="ramAdicionales" DefaultLoadingPanelID="ralpPreguntasAdicionales">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbComboboxCatalogo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbComboboxDefault" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="rsCuestionarios" runat="server" Width="100%" Height="100%" BorderSize="0">

        <telerik:RadPane ID="rpPreguntas" runat="server">
            <div style="height: calc(100% - 35px); padding: 10px 0px 10px 0px;">

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblClave" runat="server">Clave:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtClave" runat="server"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNombre" runat="server">Título del campo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNombre" runat="server" Width="300" Height="50" TextMode="MultiLine" Resize="Vertical"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblTooltip" runat="server">Tooltip:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtTooltip" runat="server" TextMode="MultiLine" Height="100" Width="300" Resize="Vertical"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblTipoControl" runat="server">Tipo control:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadComboBox ID="cmbTipoControl" runat="server" OnClientSelectedIndexChanged="SelectPageView"></telerik:RadComboBox>
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
                                    <label id="lblTextboxLongitud" runat="server">Longitud:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadNumericTextBox ID="txtTextboxLongitud" runat="server" MinValue="0" MaxValue="4000" Value="100" NumberFormat-DecimalDigits="0" CssClass="RightAligned" Width="50"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblTextboxDefault" runat="server">Default:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtTextboxDefault" runat="server" Width="300"></telerik:RadTextBox>
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvDate" runat="server">
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblDateDefault" runat="server">Default:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadDatePicker ID="txtDateDefault" runat="server"></telerik:RadDatePicker>
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCombobox" runat="server">
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblComboboxCatalogo" runat="server">Catálogo:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadComboBox ID="cmbComboboxCatalogo" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="cmbComboboxCatalogo_SelectedIndexChanged"></telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblComboboxDefault" runat="server">Default:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadComboBox ID="cmbComboboxDefault" runat="server" Width="300"></telerik:RadComboBox>
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvMaskbox" runat="server">
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblMaskboxMascara" runat="server">Máscara:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtMaskboxMascara" runat="server"></telerik:RadTextBox>
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvNumericbox" runat="server">
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblNumericboxEnteros" runat="server">Enteros:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadNumericTextBox ID="txtNumericboxEnteros" runat="server" MinValue="0" MaxValue="33" Value="6" NumberFormat-DecimalDigits="0" CssClass="RightAligned" Width="50">
                                        <ClientEvents OnValueChanged="CambiarNumeroEntero" />
                                    </telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblNumericboxDecimales" runat="server">Decimales:</label>
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
                                    <label id="lblNumericboxDefault" runat="server">Default:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadNumericTextBox ID="txtNumericboxDefault" runat="server" Value="0" NumberFormat-DecimalDigits="2" CssClass="RightAligned" Width="150"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCheckbox" runat="server">
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblCheckboxDefault" runat="server">Default:</label>
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

                <div class="ctrlBasico">
                    <fieldset style="padding-left: 10px;">
                        <legend name="lblGrupoCuestionario" id="lblGrupoCuestionario" runat="server">Cuestionario </legend>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnCuestionarioAutoevaluacion" runat="server" ToggleType="Radio" AutoPostBack="false" GroupName="grpTipoCuestionario" Text="De autoevaluación">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnCuestionarioOtros" runat="server" ToggleType="Radio" AutoPostBack="false" GroupName="grpTipoCuestionario" Text="Para evaluar a otros">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnCuestionarioAmbos" runat="server" ToggleType="Radio" AutoPostBack="false" GroupName="grpTipoCuestionario" Text="Ambos">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div>
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ToolTip="Esta opción permite dar de alta campos abiertos para comentarios, podrás crear tantos cambios como necesites."></telerik:RadButton>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyudaCuestionario" runat="server" Scrolling="None" Width="22px">
            <telerik:RadSlidingZone ID="rszAyudaCuestionario" runat="server" SlideDirection="Left" Width="22px">
                <telerik:RadSlidingPane ID="rspAyudaCuestionario" RenderMode="Mobile" runat="server" Title="Ayuda" Width="300px">
                    <div style="text-align: justify; padding: 10px;">
                        <p id="pTextoAyuda" runat="server">
                            Esta opción permite dar de alta campos abiertos para comentarios, podrás crear tantos cambios como necesites.
                            <br />
                            <br />
                            <strong>Clave:</strong>La clave dela pregunta indica un identificador de la pregunta a crear.
                            <br />
                            <strong>Título del campo:</strong> Corresponde a el texto que aparecerá como pregunta.
                            <br />
                            <strong>Tooltip:</strong> Puedes ingresar descripción a la pregunta, esta aparecerá como tooltip.
                            <br />
                            <strong>Tipo de control:</strong> Aquí especificaras el tipo de respuesta que se dará a la pregunta, esta puede ser un campo de texto, un selector de edad o fecha, etc.<br />
                            <strong>Default:</strong> Valor que se mostrara automáticamente al abrir la pregunta.
                            <br />
                            <strong>Cuestionario:</strong> Aquí debes de indicar en que cuestionario aparecerá la pregunta a crear, ya sea para autoevaluación, para los evaluadores o para ambos.
                            <br />
                        </p>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
