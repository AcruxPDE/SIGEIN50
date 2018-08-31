<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaPlaza.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaPlaza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
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

        function OpenPlazaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPlaza.aspx?mulSel=0", "winSeleccion", "Selección de plaza")
        }

        function OpenPuestosSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPuesto.aspx?mulSel=0", "winSeleccion", "Selección de puesto")
        }

        function OpenEmployeeSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?mulSel=0", "winSeleccion", "Selección de empleado")
        }

        function CleanEmployeeSelection(sender, args) {
            ChangeListItem("", "Todos", $find("<%=lstEmpleado.ClientID %>"));
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                console.info(pDato);
                var vDatosSeleccionados = pDato[0];
                var vLstDato = {
                    idItem: "",
                    nbItem: ""
                };

                var list;
                switch (vDatosSeleccionados.clTipoCatalogo) {
                    case "EMPLEADO":
                        list = $find("<%=lstEmpleado.ClientID %>");
                        vLstDato.idItem = vDatosSeleccionados.idEmpleado;
                        vLstDato.nbItem = vDatosSeleccionados.nbEmpleado;
                        break;
                    case "PUESTO":
                        list = $find("<%=lstPuesto.ClientID %>");
                        vLstDato.idItem = vDatosSeleccionados.idPuesto;
                        vLstDato.nbItem = vDatosSeleccionados.nbPuesto;
                        break;
                    case "PLAZA":
                        list = $find("<%=lstPlazaJefe.ClientID %>");
                        vLstDato.idItem = vDatosSeleccionados.idPlaza;
                        vLstDato.nbItem = vDatosSeleccionados.nbPlaza;
                        break;
                }

                if (list)
                    ChangeListItem(vLstDato.idItem, vLstDato.nbItem, list);
            }
        }

        function ChangeListItem(pIdItem, pNbItem, pList) {
            var vListBox = pList;
            vListBox.trackChanges();

            var items = vListBox.get_items();
            items.clear();

            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(pNbItem);
            item.set_value(pIdItem);
            items.add(item);
            item.set_selected(true);
            vListBox.commitChanges();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 10px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblClUsuario">Clave:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClPlaza" name="txtClPlaza" runat="server"></telerik:RadTextBox><br />
            <asp:RequiredFieldValidator ID="reqtxtClPlaza" runat="server" Display="Dynamic" ControlToValidate="txtClPlaza" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblClUsuario">Nombre:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNbPlaza" name="txtNbPlaza" runat="server"></telerik:RadTextBox><br />
            <asp:RequiredFieldValidator ID="reqtxtNbPlaza" runat="server" Display="Dynamic" ControlToValidate="txtNbPlaza" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblRol">Empleado:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadListBox ID="lstEmpleado" Width="300" runat="server" OnClientItemDoubleClicking="OpenEmployeeSelectionWindow">
                <Items>
                    <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                </Items>
            </telerik:RadListBox>
            <telerik:RadButton ID="btnBuscarEmpleado" runat="server" Text="B" OnClientClicked="OpenEmployeeSelectionWindow" AutoPostBack="false"></telerik:RadButton>
            <telerik:RadButton ID="btnLimpiarEmpleado" runat="server" Text="X" OnClientClicked="CleanEmployeeSelection" AutoPostBack="false"></telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblRol">Puesto:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadListBox ID="lstPuesto" Width="300" runat="server" OnClientItemDoubleClicking="OpenPuestosSelectionWindow">
                <Items>
                    <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                </Items>
            </telerik:RadListBox>
            <telerik:RadButton ID="btnBuscarPuesto" runat="server" Text="B" OnClientClicked="OpenPuestosSelectionWindow" AutoPostBack="false"></telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblRol">Plaza jefe:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadListBox ID="lstPlazaJefe" Width="300" runat="server" OnClientItemDoubleClicking="OpenPlazaSelectionWindow">
                <Items>
                    <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                </Items>
            </telerik:RadListBox>
            <telerik:RadButton ID="btnlstPlazaJefe" runat="server" Text="B" OnClientClicked="OpenPlazaSelectionWindow" AutoPostBack="false"></telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblNombre">Empresa:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadComboBox ID="cmbEmpresa" runat="server"></telerik:RadComboBox>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblNombre">Activo:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" name="chkActivo" Checked="true" AutoPostBack="false">
                <ToggleStates>
                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
