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

        function onCloseWindows() {
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

        function OcultarDiv() {
            var div = document.getElementById("dvPuestoNomina");
            div.style.display = "none";
        }

        function HabilitarBotonesNomina() {
            var btnNoDoTrue = $find("<%= btnNODOTrue.ClientID %>");
            var btnNoDoFalse = $find("<%= btnNODOFalse.ClientID %>");
            var cmbRazonSocial = $find("<%= cmbRazonSocial.ClientID %>");
            document.getElementById('<%= txtClEmpleadoNomina.ClientID %>').disabled = false;
            cmbRazonSocial.enable();

            btnNoDoTrue.set_enabled(true);
            btnNoDoFalse.set_enabled(true)
        }

        function DeshabilitarBotonesNomina() {
            var btnNoDoTrue = $find("<%= btnNODOTrue.ClientID %>");
            var btnNoDoFalse = $find("<%= btnNODOFalse.ClientID %>");
            var cmbRazonSocial = $find("<%= cmbRazonSocial.ClientID %>");
            document.getElementById('<%= txtClEmpleadoNomina.ClientID %>').disabled = true;
            cmbRazonSocial.disable();
            btnNoDoTrue.set_enabled(false);
            btnNoDoFalse.set_enabled(false)
        }

        function HabilitarBotonesDo() {

            var btnBonoTrue = $find("<%= btnBonoTrue.ClientID %>");
            var btnBonoFalse = $find("<%= btnBonoFalse.ClientID %>");
            var btnTabuladorTrue = $find("<%= btnTabuladorTrue.ClientID %>");
            var btnTabuladorFalse = $find("<%= btnTabuladorFalse.ClientID %>");
            var btnInventarioTrue = $find("<%= btnInventarioTrue.ClientID %>");
            var btnInventarioFalse = $find("<%= btnInventarioFalse.ClientID %>");
            var btnBuscarPuesto = $find("<%= btnBuscarPuesto.ClientID %>");
            var btnCapturarAlcance = $find("<%= btnCapturarAlcance.ClientID %>");
            document.getElementById('<%= txtSueldo.ClientID %>').disabled = false;
            var div = document.getElementById("dvPuestoNomina");
            div.style.display = "none";
            var btnNoDoTrue = $find("<%= btnNODOTrue.ClientID %>");
            var btnNoDoFalse = $find("<%= btnNODOFalse.ClientID %>");
            btnBuscarPuesto.set_enabled(true);
            btnCapturarAlcance.set_enabled(true);
            btnBonoTrue.set_enabled(true);
            btnBonoFalse.set_enabled(true);
            btnTabuladorTrue.set_enabled(true);
            btnTabuladorFalse.set_enabled(true);
            btnInventarioTrue.set_enabled(true);
            btnInventarioFalse.set_enabled(true);
            btnNoDoTrue.set_enabled(true);
            btnNoDoFalse.set_enabled(true);

        }

        function DeshabilitarBotonesDo() {
            var btnBonoTrue = $find("<%= btnBonoTrue.ClientID %>");
            var btnBonoFalse = $find("<%= btnBonoFalse.ClientID %>");
            var btnTabuladorTrue = $find("<%= btnTabuladorTrue.ClientID %>");
            var btnTabuladorFalse = $find("<%= btnTabuladorFalse.ClientID %>");
            var btnInventarioTrue = $find("<%= btnInventarioTrue.ClientID %>");
            var btnInventarioFalse = $find("<%= btnInventarioFalse.ClientID %>");
            var btnBuscarPuesto = $find("<%= btnBuscarPuesto.ClientID %>");
            var btnCapturarAlcance = $find("<%= btnCapturarAlcance.ClientID %>");
            document.getElementById('<%= txtSueldo.ClientID %>').disabled = true;
            var div = document.getElementById("dvPuestoNomina");
            div.style.display = "block";
            var btnNoDoTrue = $find("<%= btnNODOTrue.ClientID %>");
            var btnNoDoFalse = $find("<%= btnNODOFalse.ClientID %>");
            btnBuscarPuesto.set_enabled(false);
            btnCapturarAlcance.set_enabled(false);
            btnBonoTrue.set_enabled(false);
            btnBonoFalse.set_enabled(false);
            btnTabuladorTrue.set_enabled(false);
            btnTabuladorFalse.set_enabled(false);
            btnInventarioTrue.set_enabled(false);
            btnInventarioFalse.set_enabled(false);
            btnNoDoTrue.set_enabled(false);
            btnNoDoFalse.set_enabled(false)
        }

        function MostrarDiv() {
            var div = document.getElementById("dvPuestoNomina");
            div.style.display = "block";
        }

        function AbrirInventario(pIdEmpleado) {
            var vUrl = "Empleado.aspx?EmpleadoNoDoID=" + pIdEmpleado;
            var vTitulo = "Agregar Empleado";
            var vVentana = "winEmpleadoGeneral";
            OpenSelectionWindow(vUrl, vVentana, vTitulo);
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

        function CopyValue() {
            var radNumericTextBox1 = $find("<%= txtMnSueldoMensual.ClientID %>");
            var radNumericTextBox2 = $find("<%= txtSueldo.ClientID %>");
            radNumericTextBox2.set_value(radNumericTextBox1.get_value());
        }

        function CopyValueBase() {
            var radNumericTextBox1 = $find("<%= txtMnSueldoBase.ClientID %>");
            var radNumericTextBox2 = $find("<%= txtSueldo.ClientID %>");
            radNumericTextBox2.set_value(radNumericTextBox1.get_value());
        }

        function CopyValueDiario() {
            var radNumericTextBox1 = $find("<%= txtMnSueldoDiario.ClientID %>");
            var radNumericTextBox2 = $find("<%= txtSueldo.ClientID %>");
            radNumericTextBox2.set_value(radNumericTextBox1.get_value());
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



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 10px; clear: both;"></div>
    <telerik:RadTabStrip ID="rtsInventario" runat="server" SelectedIndex="0" MultiPageID="rmpInventario">
        <Tabs>
            <telerik:RadTab Text="Datos editables" Visible="false"></telerik:RadTab>
            <telerik:RadTab Text="Datos comunes no editables" Visible="false"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 60px); overflow: auto;">
        <telerik:RadMultiPage ID="rmpInventario" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvDatosRditables" runat="server">
                <div style="height: 10px; clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                        <telerik:RadLabel ID="lblClEmpleado" runat="server" Text="*No. de empleado:"></telerik:RadLabel>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtClEmpleado" runat="server" Width="250" MaxLength="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                        <telerik:RadLabel ID="lblNombre" runat="server" Text="*Nombre:"></telerik:RadLabel>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNombre" runat="server" Width="250"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                        <telerik:RadLabel ID="lblPaterno" runat="server" Text="*Apellido paterno:"></telerik:RadLabel>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtPaterno" runat="server" Width="250"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                        <telerik:RadLabel ID="lblMaterno" runat="server" Text="Apellido materno:"></telerik:RadLabel>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtMaterno" runat="server" Width="250"></telerik:RadTextBox>
                    </div>
                </div>
                <div id="dvPuestoNoDo" runat="server">
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Disponible en nómina:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <div class="checkContainer">
                                <telerik:RadButton ID="btnNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNO" AutoPostBack="false" OnClientClicked="HabilitarBotonesNomina">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNO" AutoPostBack="false" OnClientClicked="DeshabilitarBotonesNomina">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                            <telerik:RadLabel ID="lblDO" runat="server" Text="Disponible en DO:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <div class="checkContainer">
                                <telerik:RadButton ID="btnDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpDO" AutoPostBack="false" OnClientClicked="HabilitarBotonesDo">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpDO" AutoPostBack="false" OnClientClicked="DeshabilitarBotonesDo">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                            <telerik:RadLabel ID="lblNominaDo" runat="server" Text="Puesto DO = Nómina:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <div class="checkContainer">
                                <telerik:RadButton ID="btnNODOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNODO" AutoPostBack="false" OnClientClicked="OcultarDiv">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnNODOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNODO" AutoPostBack="false" OnClientClicked="MostrarDiv">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div id="dvPuestoDO" runat="server">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                            <telerik:RadLabel ID="lblPuestoDO" runat="server" Text="*Puesto DO:"></telerik:RadLabel>
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
                </div>
                <div id="dvPuestoNomina" style="display: none;">
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                            <telerik:RadLabel ID="lblPuestoNomina" runat="server" Text="Puesto nómina:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadListBox ID="lstPuestoNomina" Width="250" runat="server">
                                <Items>
                                    <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                                </Items>
                            </telerik:RadListBox>
                            <telerik:RadButton ID="btnPuestoNomina" runat="server" Text="B" AutoPostBack="false" OnClientClicked="onWindowsPuestoNomina"></telerik:RadButton>
                        </div>
                    </div>
                </div>
                <div id="dvRazonClave" runat="server">
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                            <telerik:RadLabel ID="lblRazonSocial" runat="server" Text="Razón social:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadComboBox ID="cmbRazonSocial" runat="server" Width="250" MarkFirstMatch="true" EmptyMessage="Selecciona" AutoPostBack="false"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                            <telerik:RadLabel ID="lblEmpleadoNomina" runat="server" Text="No. de empleado nómina:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtClEmpleadoNomina" runat="server" Width="250"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div style="padding-left: 195px;">
                        <div class="ctrlBasico" style="width: 630px;">
                            <div class="divBarraTitulo">
                                <label style="float: left; width: 200px;">Sueldo mensual</label>
                                <label style="float: left; width: 200px;">Sueldo diario</label>
                                <label style="float: left; width: 200px;">Base de cotización</label>
                            </div>
                            <div style="padding: 5px">
                                <div class="ctrlBasico" style="text-align: left; width: 100%;">
                                    <div class="ctrlBasico" style="width: 200px;">
                                        <telerik:RadButton ID="btnSueldoMensual" runat="server" ButtonType="ToggleButton" ToggleType="Radio" AutoPostBack="false" GroupName="sueldoDo" OnClientClicked="CopyValue"></telerik:RadButton>
                                        <telerik:RadNumericTextBox ID="txtMnSueldoMensual" MinValue="0" Type="Currency" runat="server" Width="150"></telerik:RadNumericTextBox>
                                    </div>
                                    <div class="ctrlBasico" style="width: 200px;">
                                        <telerik:RadButton ID="btnSueldoDiario" runat="server" ButtonType="ToggleButton" ToggleType="Radio" AutoPostBack="false" GroupName="sueldoDo" OnClientClicked="CopyValueDiario"></telerik:RadButton>
                                        <telerik:RadNumericTextBox ID="txtMnSueldoDiario" MinValue="0" Type="Currency" runat="server" Width="150"></telerik:RadNumericTextBox>
                                    </div>
                                    <div class="ctrlBasico" style="width: 200px;">
                                        <telerik:RadButton ID="btnBaseCotizacion" runat="server" ButtonType="ToggleButton" ToggleType="Radio" AutoPostBack="false" GroupName="sueldoDo" OnClientClicked="CopyValueBase"></telerik:RadButton>
                                        <telerik:RadNumericTextBox ID="txtMnSueldoBase" MinValue="0" Type="Currency" runat="server" Width="150" NumberFormat-DecimalDigits="4"></telerik:RadNumericTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="dvSueldosDo" runat="server">
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                            <telerik:RadLabel ID="lblSueldo" runat="server" Text="Sueldo DO:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox ID="txtSueldo" runat="server" Width="250" MinValue="0" Type="Currency" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="divControlIzquierda" style="text-align: right; width: 180px;">
                        <telerik:RadLabel ID="lblVisibilidad" runat="server" Text="Sueldo visible para DO:"></telerik:RadLabel>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <telerik:RadLabel ID="lblInventario" runat="server" Text="Inventario:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <div class="checkContainer">
                                <telerik:RadButton ID="btnInventarioTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpInventario" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnInventarioFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpInventario" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <telerik:RadLabel ID="lblTabulador" runat="server" Text="Tabulador:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <div class="checkContainer">
                                <telerik:RadButton ID="btnTabuladorTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpTabulador" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnTabuladorFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpTabulador" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <telerik:RadLabel ID="lblBono" runat="server" Text="Bono:"></telerik:RadLabel>
                        </div>
                        <div class="divControlDerecha">
                            <div class="checkContainer">
                                <telerik:RadButton ID="btnBonoTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpBono" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnBonoFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpBono" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                    </div>
                </div>
            </telerik:RadPageView>
            <%--    <telerik:RadPageView ID="rpvDatosNoEditables" runat="server">
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="RadLabel3" runat="server" Text="Jefe inmediato:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtJefeDirecto" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblRFC" runat="server" Text="RFC:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtRfc" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCurp" runat="server" Text="CURP:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCurp" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblNss" runat="server" Text="No. de seguro social:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtNss" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblLugarNacimiento" runat="server" Text="Lugar de nacimiento:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtLugarNacimiento" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblNacionalidad" runat="server" Text="Nacionalidad:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtNacionalidad" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblFeNacimiento" runat="server" Text="Fecha de nacimiento:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtFeNacimiento" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblGenero" runat="server" Text="Género:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtGenero" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblEstadoCivil" runat="server" Text="Estado civil:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtEstadoCivil" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCP" runat="server" Text="C.P.:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCp" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblEstado" runat="server" Text="Estado:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtEstado" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblMunicipio" runat="server" Text="Municipio:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtMunicipio" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblColonia" runat="server" Text="Colonia:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtColonia" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCalle" runat="server" Text="Calle:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCalle" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblNoExt" runat="server" Text="*No. Exterior"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtNoExterior" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lbl" runat="server" Text="*No. Interior:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtNoInterior" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblTelefono" runat="server" Text="Teléfonos:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadComboBox ID="cmbTelefonos" runat="server" Width="300" MarkFirstMatch="true" AutoPostBack="false"></telerik:RadComboBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblEmail" runat="server" Text="Correo electrónico:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtEmail" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCartillaMilitar" runat="server" Text="Cartilla militar:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCartillaMilitar" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblDatosFamiliares" runat="server" Text="Datos familiares:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtDatosFamiliares" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblFormacion" runat="server" Text="Formación académica:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtFormacion" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblIdiomas" runat="server" Text="Idiomas:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadComboBox ID="cmbIdiomas" runat="server" Width="300" MarkFirstMatch="true" AutoPostBack="false"></telerik:RadComboBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblProfesional" runat="server" Text="Experiencia profesional:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtProfesional" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblInteres" runat="server" Text="Área de interes:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadComboBox ID="cmbAreaInteres" runat="server" Width="300" MarkFirstMatch="true" AutoPostBack="false"></telerik:RadComboBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCompetencias" runat="server" Text="Competencias:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadComboBox ID="cmbCompetencias" runat="server" Width="300" MarkFirstMatch="true" AutoPostBack="false"></telerik:RadComboBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblEmpresa" runat="server" Text="Empresa:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtEmpresa" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblSueldoMensual" runat="server" Text="Sueldo mensual:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtSueldoMensual" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblFolioSolicitud" runat="server" Text="Folio de solicitud:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtFolioSolicitud" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblRedesSociales" runat="server" Text="Redes sociales:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadComboBox ID="cmbRedesSociales" runat="server" Width="300" MarkFirstMatch="true" AutoPostBack="false"></telerik:RadComboBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCentroOp" runat="server" Text="Centro operativo:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCentroOp" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCentroAdm" runat="server" Text="Centro administrativo:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCentroAdm" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblPrestaciones" runat="server" Text="Paquete de prestaciones:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtPrestaciones" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblDispersionGasolina" runat="server" Text="Formatos de dispersión gasolina:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtDispersionGasolina" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblDispersionNomina" runat="server" Text="Formatos de dispersión nómina:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtDispersionNomina" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblDispersionVales" runat="server" Text="Formatos de dispersión vales:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtDispersionVales" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblFeIngreso" runat="server" Text="Fecha de ingreso:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtFeIngreso" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblFePlanta" runat="server" Text="Fecha de planta:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtFePlanta" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblFeAntiguedad" runat="server" Text="Fecha de antigüedad:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtFeAntiguedad" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblFeBaja" runat="server" Text="Fecha de baja:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtFeBaja" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblTipoNomina" runat="server" Text="Tipo nomina:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtTipoNomina" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblFormaPago" runat="server" Text="Forma de pago:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtFormaPago" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCotizacionFactor" runat="server" Text="Factor salario base de cotización:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtBaseCotizacion" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCotizacionFija" runat="server" Text="Salario base cotización fijo:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCotizacionFija" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCotizacionVariable" runat="server" Text="Salario base de cotización variable:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCotizacionVariable" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCotizacionMaximo" runat="server" Text="Salario base de cotización máximo:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCotizacionMaximo" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCotizaIMSS" runat="server" Text="Cotiza IMSS"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCotizaImss" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblClBancaria" runat="server" Text="Clabe bancaria:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtClBancaria" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCuentaPago" runat="server" Text="Cuenta de pago:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCtaPago" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblClPago" runat="server" Text="Clabe de pago:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtClPago" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCtaVales" runat="server" Text="Cuenta de vales de despensa:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtCtaVales" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblUltPrimaVacacional" runat="server" Text="Fecha de última prima vacacional:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtUltPrimaVacacional" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblUltAguinaldo" runat="server" Text="Fecha de último aguinaldo:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtUltAguinaldo" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblTipoSAT" runat="server" Text="Tipo de contrato SAT:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtTipoSAT" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblJornadaSAT" runat="server" Text="Tipo de jornada SAT:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtJornadaSAT" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblRegPatronal" runat="server" Text="Registro patronal"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtRegPatronal" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblUnidadFamiliar" runat="server" Text="Unidad médico familiar:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtUnidadFamiliar" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblEstadoNacimiento" runat="server" Text="Estado de nacimiento:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtEstadoNacimiento" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblTipoTrabajador" runat="server" Text="Tipo de trabajador:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtTipoTrabajador" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblJornada" runat="server" Text="Jornada:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtJornada" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblUbicacion" runat="server" Text="Ubicación:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtUbicacion" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblTipoSalario" runat="server" Text="Tipo de salario:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtTipoSalario" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico" style="text-align: right; width: 300px;">
                    <telerik:RadLabel ID="lblCtoAccidente" runat="server" Text="Contacto en caso de accidentes:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtContactoaccidente" runat="server" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
            </telerik:RadPageView>--%>
        </telerik:RadMultiPage>
    </div>
    <div style="height: 10px; clear: both;"></div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnCapturarAlcance" runat="server" Text="Más datos" OnClick="btnCapturarAlcance_Click" AutoPostBack="true"></telerik:RadButton>
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar y cerrar" AutoPostBack="true" OnClick="btnGuardar_Click"></telerik:RadButton>
        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="onCloseWindows"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
