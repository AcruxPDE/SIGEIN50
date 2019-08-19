<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaInventarioPersonalNomina.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaInventarioPersonalNomina" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
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
        var pIdEmpleado = "<%=vIdEmpleado%>";

        function onCloseWindows() {
            GetRadWindow().close();
        }

        function CloseWindowConfig() {
            GetRadWindow().close();
        }

        function onWindowsPlazas() {
            OpenSelectionWindow("../Comunes/SeleccionPlaza.aspx?TipoSeleccionCl=VACANTES&mulSel=1", "winSeleccion", "Selección de plazas")
        }

        function onWindowsPuestoNomina() {
            OpenSelectionWindow("../Comunes/SeleccionPuestoNominaDo.aspx?TipoSeleccionCl=NOMINA&mulSel=1", "winSeleccion", "Selección de puesto")
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var vDatoSeleccionado = pDato[0];
                switch (vDatoSeleccionado.clTipoCatalogo) {
                    case "PLAZA":
                        var vListPuesto = $find("<%=lstPuesto.ClientID %>");
                        SetListBoxItem(vListPuesto, pDato[0].nbPuesto, pDato[0].idPlaza);
                        break;
                    case "PUESTONOMINADO":
                        var vListPuestoNomina = $find("<%=lstPuestoNomina.ClientID %>");
                        SetListBoxItem(vListPuestoNomina, pDato[0].nbPuesto, pDato[0].idPuesto);
                        break;
                    case "CLOSE":
                        onCloseWindows();
                        break;
                    default:
                        break;
                }
            }
        }

        function SetListBoxItem(list, text, value) {
            if (list != undefined) {
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(text);
                item.set_value(value);
                item.set_selected(true);
                items.add(item);

                list.commitChanges();
            }
        }

        function confirmarGuardar(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

            radconfirm('¿Está seguro de guardar los cambios del empleado?', callBackFunction, 400, 170, null, "Aviso");
            args.set_cancel(true);
        }

        function confirmarCancelar(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { CloseWindowConfig(); } });

            radconfirm('¿Está seguro que deseas salir sin guardar los cambios del empleado?', callBackFunction, 400, 170, null, "Aviso");
            args.set_cancel(true);
        }

        function abrirInventario(sender, args) {
            var vUrl = "Empleado.aspx?EmpleadoNoDoID=" + pIdEmpleado;
            var vTitulo = "Agregar Empleado";
            var vVentana = "winEmpleadoGeneral";
            OpenSelectionWindow(vUrl, vVentana, vTitulo);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpInventarioPersonalNomina"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramInventarioPersonalNomina" runat="server" DefaultLoadingPanelID="ralpInventarioPersonalNomina" OnAjaxRequest="ramInventarioPersonalNomina_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnNOTrue">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoDO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoNO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapSueldoNominaDO" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNOFalse">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoDO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoNO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapSueldoNominaDO" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDOTrue">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoDO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoNO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapSueldoNominaDO" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDOFalse">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoDO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoNO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapSueldoNominaDO" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rapPuestoDO">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoDO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoNO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapSueldoNominaDO" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rapPuestoNO">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoDO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoNO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapSueldoNominaDO" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rapSueldoNominaDO">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoDO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapPuestoNO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rapSueldoNominaDO" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSueldoTrue" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnMasDatos" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnMasDatos" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNombre">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnGuardar" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnGuardarCerrar" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnMasDatos" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtPaterno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnGuardar" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnGuardarCerrar" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnMasDatos" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtMaterno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnGuardar" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnGuardarCerrar" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnMasDatos" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="height: 10px; clear: both;"></div>

    <div style="height: calc(100% - 60px); overflow: auto;">
        <div style="height: 10px; clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda" style="text-align: right; width: 180px; margin-top: 6px;">
                <telerik:RadLabel ID="lblClEmpleado" runat="server" Text="*No. de empleado:"></telerik:RadLabel>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClEmpleado" runat="server" Width="250" MaxLength="20"></telerik:RadTextBox>
            </div>
        </div>

        <div style="clear: both;"></div>

        <div class="ctrlBasico">
            <div class="divControlIzquierda" style="text-align: right; width: 180px; margin-top: 6px;">
                <telerik:RadLabel ID="lblNombre" runat="server" Text="*Nombre:"></telerik:RadLabel>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNombre" runat="server" Width="250"></telerik:RadTextBox>
            </div>
        </div>

        <div style="clear: both;"></div>

        <div class="ctrlBasico">
            <div class="divControlIzquierda" style="text-align: right; width: 180px; margin-top: 6px;">
                <telerik:RadLabel ID="lblPaterno" runat="server" Text="*Apellido paterno:"></telerik:RadLabel>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtPaterno" runat="server" Width="250"></telerik:RadTextBox>
            </div>
        </div>

        <div style="clear: both;"></div>

        <div class="ctrlBasico">
            <div class="divControlIzquierda" style="text-align: right; width: 180px; margin-top: 6px;">
                <telerik:RadLabel ID="lblMaterno" runat="server" Text="Apellido materno:"></telerik:RadLabel>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtMaterno" runat="server" Width="250"></telerik:RadTextBox>
            </div>
        </div>

        <div style="clear: both;"></div>

        <telerik:RadAjaxPanel runat="server" ID="dvDispNO">
            <div class="ctrlBasico">
                <div class="divControlIzquierda" style="text-align: right; width: 180px; margin-top: 6px;">
                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Disponible en nómina:"></telerik:RadLabel>
                </div>
                <div class="divControlDerecha">
                    <div class="checkContainer">
                        <telerik:RadButton ID="btnNOTrue" Checked="false" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNO" AutoPostBack="true" OnClick="btnNOTrue_Click">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnNOFalse" Checked="true" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNO" AutoPostBack="true" OnClick="btnNOFalse_Click">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>

        <div style="clear: both;"></div>

        <telerik:RadAjaxPanel ID="dvDispDO" runat="server">
            <div class="ctrlBasico">
                <div class="divControlIzquierda" style="text-align: right; width: 180px; margin-top: 6px;">
                    <telerik:RadLabel ID="lblDO" runat="server" Text="Disponible en DO:"></telerik:RadLabel>
                </div>
                <div class="divControlDerecha">
                    <div class="checkContainer">
                        <telerik:RadButton ID="btnDOTrue" Checked="false" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpDO" AutoPostBack="true" OnClick="btnDOTrue_Click">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnDOFalse" Checked="true" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpDO" AutoPostBack="true" OnClick="btnDOFalse_Click">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>

        <div style="clear: both;"></div>

        <telerik:RadAjaxPanel runat="server" ID="rapPuestoDO" Visible="true">
            <div class="ctrlBasico">
                <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                    <telerik:RadLabel ID="lblPuestoDO" Visible="true" runat="server" Text="* Puesto:"></telerik:RadLabel>
               </div>
                <div class="divControlDerecha">
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="lstPuesto" Width="250" runat="server">
                            <Items>
                                <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                            </Items>
                        </telerik:RadListBox>
                        <telerik:RadButton ID="btnBuscarPuesto" runat="server" Text="B" AutoPostBack="false" OnClientClicked="onWindowsPlazas"></telerik:RadButton>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>

        <div style="clear: both;"></div>

        <telerik:RadAjaxPanel runat="server" ID="rapPuestoNO" Visible="false">
            <div class="ctrlBasico">
                <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                    <telerik:RadLabel ID="lblPuestoNomina" runat="server" Text="* Puesto:"></telerik:RadLabel>
                </div>
                <div class="divControlDerecha">
                    <telerik:RadListBox ID="lstPuestoNomina" OnCallingDataMethods="lstPuestoNomina_CallingDataMethods" Width="250" runat="server">
                        <Items>
                            <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                        </Items>
                    </telerik:RadListBox>
                    <telerik:RadButton ID="btnPuestoNomina" runat="server" Text="B" AutoPostBack="false" OnClientClicked="onWindowsPuestoNomina"></telerik:RadButton>
                </div>
            </div>
        </telerik:RadAjaxPanel>

        <telerik:RadAjaxPanel runat="server" ID="rapSueldoNominaDO" Visible="false">
            <div class="ctrlBasico">
                <div class="divControlIzquierda" style="text-align: right; width: 180px; margin-top: 6px;">
                    <telerik:RadLabel ID="lblSueldoNominaDO" runat="server" Text="Sueldo Nómina = DO:"></telerik:RadLabel>
                </div>
                <div class="divControlDerecha">
                    <div class="checkContainer">
                        <telerik:RadButton ID="btnSueldoTrue" Checked="false" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSueldo" AutoPostBack="true">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnSueldoFalse" Checked="true" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSueldo" AutoPostBack="true">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>

        <div style="height: 10px; clear: both;"></div>
        
        <div class="divControlDerecha" style="padding-right: 30px;">
            <telerik:RadButton ID="btnGuardar" runat="server" name="btnGuardar" AutoPostBack="true" Text="Guardar" Width="100" OnClientClicking="confirmarGuardar" OnClick="btnGuardar_Click"></telerik:RadButton>
            <telerik:RadButton ID="btnGuardarCerrar" runat="server" name="btnGuardarCerrar" AutoPostBack="true" Text="Guardar y Cerrar" UseSubmitBehavior="false" OnClientClicking="confirmarGuardar" OnClick="btnGuardarCerrar_Click" ></telerik:RadButton>
            <telerik:RadButton ID="btnMasDatos" runat="server" name="btnMasDatos" AutoPostBack="true" Text="Más datos" Width="100" Enabled="false" OnClientClicking="abrirInventario" ></telerik:RadButton>
            <telerik:RadButton ID="btnCancelar" runat="server" name="btnCancelar" AutoPostBack="true" Text="Cancelar" Width="100" OnClientClicking="confirmarCancelar" ></telerik:RadButton>
        </div>

    </div>
    <telerik:RadWindowManager ID="rwMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
