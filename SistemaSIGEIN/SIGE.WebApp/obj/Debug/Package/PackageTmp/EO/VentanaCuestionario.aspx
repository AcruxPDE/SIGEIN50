<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaCuestionario.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaCuestionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
         <style>
        .RadButton.rbSkinnedButton.uncheckedYes {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedYes > .rbDecorated {
                color: #eee !important;
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
                color: #eee !important;
            }

        .checkContainer {
            border-radius: 5px;
            border: 1px solid lightgray;
            background: #eee;
        }
    </style>
        <script type="text/javascript">

            function closeWindow() {
                var pDatos = [{
                    clTipoCatalogo: "CUESTIONARIO"

                }];
                cerrarVentana(pDatos);
            }

            function cerrarVentana(recargarGrid) {
                sendDataToParent(recargarGrid);
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            <%--<telerik:AjaxSetting AjaxControlID="rbVerificacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPreguntaVerificacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txnSecuenciaVerificacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnVerificacionTrue">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPreguntaVerificacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txnSecuenciaVerificacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnVerificacionFalse">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPreguntaVerificacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txnSecuenciaVerificacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbDimension">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTema" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtAgDimension" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTema">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAgTema" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 50px);">
        <telerik:RadSplitter ID="rsVentanaCuestionario" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpVentanaCuestionario" runat="server">
                <div style="height: 10px;"></div>
                <div style="clear: both;"></div>
                <div id="grpPregunta">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblDimension">Dimensión:</label>
                        </div>
                        <div class="divControlDerecha" style="margin-left: 20px;">
                            <telerik:RadTextBox ID="txtAgDimension" runat="server" ToolTip="Agregar nueva dimensión" EmptyMessage="Agregar"></telerik:RadTextBox>
                        </div>
                        <div class="divControlDerecha" style="margin-left: 20px;">
                            <label id="Label1">ó </label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadComboBox runat="server" ID="cmbDimension" EmptyMessage="Seleccione" OnSelectedIndexChanged="cmbDimension_SelectedIndexChanged" Width="320" AllowCustomText="true" AutoPostBack="true" Filter="Contains"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblNombre">Tema:</label>
                        </div>
                        <div class="divControlDerecha" style="margin-left: 20px;">
                            <telerik:RadTextBox ID="txtAgTema" runat="server" EmptyMessage="Agregar" ToolTip="Agregar nuevo tema"></telerik:RadTextBox>
                        </div>
                        <div class="divControlDerecha" style="margin-left: 20px;">
                            <label id="Label2">ó </label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadComboBox runat="server" ID="cmbTema" EmptyMessage="Seleccione" AutoPostBack="true" OnSelectedIndexChanged="cmbTema_SelectedIndexChanged" Width="320" AllowCustomText="true" Filter="Contains" MaxHeight="400"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblDsPregunta">Pregunta:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtPregunta" runat="server" Width="460" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblSecuencia">Secuencia:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" ID="txnSecuencia" Name="txnSecuenciaVerificacion" Width="60px" MinValue="0" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <label id="lblVerificar" name="lblNotificar" width="260px" runat="server">Habilitar verificación de invalidez:</label>
                </div>
                  <div class="ctrlBasico">
                 <div class="checkContainer">
                      
                        <telerik:RadButton ID="btnVerificacionFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpVerificacionNO" AutoPostBack="true" OnCheckedChanged="btnVerificacionTrue_CheckedChanged">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                       <telerik:RadButton ID="btnVerificacionTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpVerificacionNO" AutoPostBack="true" OnCheckedChanged="btnVerificacionTrue_CheckedChanged">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                      </div>
   <%--             <div class="ctrlBasico">
                    <telerik:RadButton ID="rbVerificacion" runat="server" OnCheckedChanged="rbVerificacion_CheckedChanged" ToggleType="CheckBox" name="rbVerificacion" AutoPostBack="true">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                </div>--%>

                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <telerik:RadLabel runat="server" ID="lblPreguntaVerificacion">Pregunta:</telerik:RadLabel>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtPreguntaVerificacion" runat="server" Width="460" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <telerik:RadLabel runat="server" ID="lblSecuenciaVerificacion">Secuencia:</telerik:RadLabel>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox runat="server" ID="txnSecuenciaVerificacion" Name="txnSecuenciaVerificacion" Width="60px" MinValue="0" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                    </div>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpInstrucciones" runat="server" Scrolling="None" Width="22px">
                <telerik:RadSlidingZone ID="rszInstrucciones" runat="server" SlideDirection="Left" ExpandedPaneId="rsInstrucciones" Width="22px">
                    <telerik:RadSlidingPane ID="rspInstrucciones" runat="server" Title="Instrucciones" Width="380px" RenderMode="Mobile" Height="200">
                        <div style="padding: 10px; text-align: justify;">
                            <p>Es importante que al diseñar tu cuestionario establezcas dimensiones de medición, temas para cada dimensión y preguntas para cada tema de manera tal que puedas filtrar los resultados de acuerdo a sus características.</p>
                            <p style="font-weight: bold;">Definiciones:</p>
                            <ul style="text-align: left;">
                                <li>Dimensiones: Son los temas marco de medición del Clima laboral.</li>
                                <li>Temas: Son las divisiones de las dimensiones y que se refieren a asuntos más concretos del Clima laboral.</li>
                                <li>Preguntas: Son las que aparecen en el cuestionario que aplicarás al personal.</li>
                                <li>Secuencia: Es el orden en el que aparecerán las preguntas en el cuestionario que recibirá el personal que contestará la evaluación.</li>
                            </ul>
                            <p>Tú puedes habilitar o deshabilitar la verificación de invalidez. Si eliges utilizarla es muy importante que por cada tema generado desarrolles 2 preguntas que midan exactamente lo mismo pero redactadas de forma diferente. También te sugerimos que el orden de aparición de cada pregunta de invalidez sea de tal forma que a la persona que llena el cuestionario no le sea sencillo detectar la lógica de la evaluación.</p>
                            <p style="font-weight: bold;">Ejemplo:</p>
                            <div style="clear: both;"></div>
                            <telerik:RadGrid ID="grdEjemplo"
                                runat="server"
                                Height="370"
                                Width="330"
                                AllowSorting="true"
                                AllowFilteringByColumn="true"
                                ShowHeader="true"
                                HeaderStyle-Font-Bold="true"
                                OnNeedDataSource="grdEjemplo_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle AlwaysVisible="true" />
                                <MasterTableView AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                        AddNewRecordText="Insertar" />
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Tema" DataField="TEMA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Liderazgo del jefe inmediato" DataField="LIDERAZGO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Orden en el que aparecerá la pregunta en el cuestionario" DataField="ORDEN"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnAgregar" runat="server" Text="Guardar" OnClick="btnAgregar_Click"></telerik:RadButton>
        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
</asp:Content>
