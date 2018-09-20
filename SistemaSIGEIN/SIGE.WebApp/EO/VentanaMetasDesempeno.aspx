<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaMetasDesempeno.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaMetasDesempeno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

        function ActivarDivs() {
            var rbPorcentual = $find('<%= rbPorcentual.ClientID %>');
            var rbMonto = $find('<%= rbMonto.ClientID %>');
            var rbFecha = $find('<%= rbFecha.ClientID %>');
            var rbSiNo = $find('<%= rbSiNo.ClientID %>');
            if (rbPorcentual.get_checked()) {
                document.getElementById("divPorcentual").style.display = "block";
                document.getElementById("divMonto").style.display = "none";
                document.getElementById("divFecha").style.display = "none";
                document.getElementById("divSiNo").style.display = "none";
            }

            if (rbMonto.get_checked()) {
                document.getElementById("divPorcentual").style.display = "none";
                document.getElementById("divMonto").style.display = "block";
                document.getElementById("divFecha").style.display = "none";
                document.getElementById("divSiNo").style.display = "none";
            }

            if (rbFecha.get_checked()) {
                document.getElementById("divPorcentual").style.display = "none";
                document.getElementById("divMonto").style.display = "none";
                document.getElementById("divFecha").style.display = "block";
                document.getElementById("divSiNo").style.display = "none";
            }

            if (rbSiNo.get_checked()) {
                document.getElementById("divPorcentual").style.display = "none";
                document.getElementById("divMonto").style.display = "none";
                document.getElementById("divFecha").style.display = "none";
                document.getElementById("divSiNo").style.display = "block";
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpMeta" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConfiguracionPeriodo" runat="server" DefaultLoadingPanelID="ralpMeta">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rbPorcentual">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPorcentual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divMonto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divFecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divSiNo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbMonto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPorcentual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divMonto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divFecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divSiNo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbFecha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPorcentual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divMonto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divFecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divSiNo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbSiNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPorcentual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divMonto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divFecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divSiNo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbFunciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbIndicador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAnterior">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbFunciones" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="cmbIndicador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtMeta" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbPorcentual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbMonto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbFecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbSiNo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divPorcentual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divMonto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divFecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divSiNo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtMetaActual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtMetaTotal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPonderacion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnMetaSiguiente" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnAnterior" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnMetaSiguiente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbFunciones" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="cmbIndicador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtMeta" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbPorcentual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbMonto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbFecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbSiNo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divPorcentual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divMonto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divFecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divSiNo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtMetaActual" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtMetaTotal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPonderacion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnMetaSiguiente" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnAnterior" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 80px); width: 100%;">
        <div style="clear: both; height: 10px;"></div>
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Evaluado:</label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <div id="txtEvaluado" runat="server" width="170" maxlength="1000" enabled="false"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Es responsable de:</label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <div id="txtResponsabilidad" runat="server" width="170" maxlength="1000" enabled="false"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divControlesBoton">
            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnAnterior" Text="<" OnClick="btnAnterior_Click"></telerik:RadButton>
            </div>
            <div class="ctrlBasico">
                <telerik:RadTextBox runat="server" ID="txtMetaActual" Width="50px"></telerik:RadTextBox>
            </div>
            <div class="ctrlBasico">
                <telerik:RadLabel runat="server" ID="lblDeM" Text="de"></telerik:RadLabel>
            </div>
            <div class="ctrlBasico">
                <telerik:RadTextBox runat="server" ID="txtMetaTotal" Width="50"></telerik:RadTextBox>
            </div>
            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnMetaSiguiente" Text=">" OnClick="btnMetaSiguiente_Click"></telerik:RadButton>
            </div>
        </div>
        <div style="height: calc(100% - 90px); overflow: auto; width: 100%;">
            <telerik:RadSplitter runat="server" ID="spHelp" Width="100%" Height="100%" BorderSize="0">
                <telerik:RadPane ID="RadPane1" runat="server">
                    <label runat="server" id="lblFuncion">Función genérica:</label>
                    <telerik:RadComboBox runat="server" ID="cmbFunciones" Width="99%" MaxLength="600" AutoPostBack="true"
                        EnableLoadOnDemand="true" OnSelectedIndexChanged="cmbFunciones_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    <div style="clear: both; height: 10px;"></div>
                    <label id="lbIndicador" runat="server">Indicador:</label>
                    <telerik:RadComboBox runat="server" ID="cmbIndicador" Width="99%" MaxLength="800" AutoPostBack="false"
                        EnableLoadOnDemand="true">
                    </telerik:RadComboBox>
                    <div style="clear: both; height: 10px;"></div>
                    <%--<div class="ctrlBasico" style="width: 100%">
            <telerik:RadTextBox runat="server" ID="txtIndicador" TextMode="MultiLine" Height="80px" Width="100%"></telerik:RadTextBox>
        </div>--%>
                    <label>Meta</label>
                    <div style="clear: both; height: 2px;"></div>
                    <div class="ctrlBasico" style="width: 100%">
                        <telerik:RadTextBox runat="server" ID="txtMeta" Height="60px" TextMode="MultiLine" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico" style="padding-left: 5px;">
                        <fieldset>
                            <legend>
                                <label>Tipo de Meta</label>
                            </legend>
                            <div class="ctrlBasico" style="padding-left: 5px;">
                                <telerik:RadButton runat="server" ID="rbPorcentual" ToggleType="Radio" Text="Porcentual" ToolTip=" Utiliza esta opción para establecer metas relacionadas con porcentajes de cumplimiento. Ejemplo: porcentaje de material ahorrado, porcentaje de participación en el mercado, etc." GroupName="METAS" OnCheckedChanged="rbPorcentual_CheckedChanged">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="rbMonto" ToggleType="Radio" Text="Cantidad" ToolTip="Utiliza esta opción para establecer metas relacionadas con cifras, cantidades o números. Ejemplo: número de piezas producidos, días de entrega, número de ordenes entregadas a tiempo, etc." GroupName="METAS" OnCheckedChanged="rbMonto_CheckedChanged">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="rbFecha" ToggleType="Radio" ToolTip="Utiliza esta opción para establecer metas relacionadas con fechas (entrega de reportes, realización del evento, avance de proyecto, etc.) Ejemplo: fecha para entrega de reporte, fecha para entrega de presupuesto, etc. (formato DD/MM/AAAA)" Text="Fecha" GroupName="METAS" OnCheckedChanged="rbFecha_CheckedChanged">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="rbSiNo" ToggleType="Radio" Text="Si/No" GroupName="METAS" ToolTip="Utiliza esta opción para establecer metas que solo tengan la posibilidad de ser calificada como cumplida o incumplida. Ejemplo: Logro de certificación del sistema ISO, entrega de documentación con autoridades gubernamentales, etc." OnCheckedChanged="rbSiNo_CheckedChanged">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </fieldset>
                    </div>
                    <div id="divPorcentual" runat="server" style="width: 100%; float: left; display: none;">
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Actual</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="txtPActual" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Mínimo</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="txtPMinimo" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Satisfactoria</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="txtPSatisfactoria" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Sobresaliente</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="txtPSobresaliente" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div id="divMonto" runat="server" style="width: 100%; float: left; display: none;">
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Actual</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="txtMActual" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Mínimo</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="txtMMinima" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Satisfactoria</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="txtMSatisfactoria" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Sobresaliente</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="txtMSobresaliente" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div id="divFecha" runat="server" style="width: 100%; float: left; display: none;">
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Actual</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadDatePicker runat="server" ID="dtpActual" Width="120" DateInput-DateFormat="dd-MM-yyyy"></telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Mínimo</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadDatePicker runat="server" ID="dtpMinimo" Width="120"></telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Satisfactoria</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadDatePicker runat="server" ID="dtpSatisfactoria" Width="120"></telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Sobresaliente</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadDatePicker runat="server" ID="dtpSobresaliente" Width="120"></telerik:RadDatePicker>
                            </div>
                        </div>
                    </div>
                    <div id="divSiNo" runat="server" style="width: 100%; float: left; display: none;">
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Mínimo</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" Enabled="false" Font-Bold="true" MinValue="0" MaxValue="100" Value="0" ID="txtSMinimo" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Satisfactoria</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" Enabled="false" Font-Bold="true" ID="txtSSatis" Value="0" MinValue="0" MaxValue="100" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label>Sobresaliente</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" Enabled="false" Font-Bold="true" ID="txtSMaximo" Value="100" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Ponderación</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" ID="txtPonderacion" MaxValue="100" NumberFormat-DecimalDigits="2" Width="90px"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                </telerik:RadPane>
                <telerik:RadPane ID="RadPane2" runat="server" Width="30">
                    <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                        <telerik:RadSlidingPane ID="rspMensaje" runat="server" Title="Ayuda" Width="300" MinWidth="300" Height="100%">
                            <div style="padding: 10px; text-align: justify;">

                                    Una vez diseñada la meta, puedes usar el botón "Guardar" para crear nuevas metas.
                                    <br />
                                    Este botón te permitirá continuar en la misma ventana para seguir con el proceso de diseño de metas.
                                    <br />
                                    <br />
                                    Por el contrario, si no requieres dar de alta más, selecciona el botón "Guardar y cerrar", guardará tu meta y te sacará de la ventana actual.
                            </div>
                        </telerik:RadSlidingPane>
                    </telerik:RadSlidingZone>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </div>
        <div class="divControlesBoton">
            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuadrar_Click"></telerik:RadButton>
            </div>
            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnAceptar" Text="Guardar y cerrar" OnClick="btnAceptar_Click"></telerik:RadButton>
            </div>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
