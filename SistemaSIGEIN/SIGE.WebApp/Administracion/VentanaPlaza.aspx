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
            var VarIdPlaza = ('<%= vIdPlaza %>');
            var VarIdEmpleado = ('<%= vIdEmpleado %>');
            if (VarIdPlaza == "") {
                OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?mulSel=0", "winSeleccion", "Selección de empleado")
            } else {
                if (VarIdEmpleado != 0) {
                    var currentWnd = GetRadWindow();
                    var browserWnd = window;
                    if (currentWnd)
                        browserWnd = currentWnd.BrowserWindow;
                    browserWnd.radalert("Para asignar esta plaza a otra persona, es importante que se libere desde el inventario de personal.", 400, 180);
                } else { OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?mulSel=0", "winSeleccion", "Selección de empleado") }
            }
        }



        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx?mulSel=0", "winSeleccion", "Selección de área/departamento")
        }

        function OpenGruposWindows() {
            OpenSelectionWindow("../Comunes/SeleccionGrupo.aspx?mulSel=1", "winSeleccion", "Selección de grupos")
        }

        function CleanEmployeeSelection(sender, args) {
            var VarIdPlaza = ('<%= vIdPlaza %>');
            var VarIdEmpleado = ('<%= vIdEmpleado %>');
            if (VarIdPlaza == "") {
                ChangeListItem("", "No Seleccionado", $find("<%=lstEmpleado.ClientID %>"));
            } else {
                if (VarIdEmpleado != 0) {
                    var currentWnd = GetRadWindow();
                    var browserWnd = window;
                    if (currentWnd)
                        browserWnd = currentWnd.BrowserWindow;
                    browserWnd.radalert("Para asignar esta plaza a otra persona, es importante que se libere desde el inventario de personal.", 400, 180);
                } else { ChangeListItem("", "No Seleccionado", $find("<%=lstEmpleado.ClientID %>")); }
            }
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
                    case "DEPARTAMENTO":
                        list = $find("<%= lstArea.ClientID %>");
                        vLstDato.idItem = vDatosSeleccionados.idArea;
                        vLstDato.nbItem = vDatosSeleccionados.nbArea;
                        break;
                    case "GRUPO":
                        InsertarDato(EncapsularDatos("GRUPO", pDato));
                        break;
                }

                if (list)
                    ChangeListItem(vLstDato.idItem, vLstDato.nbItem, list);
            }
        }

        //FUNCTION INSERTAR DATO
        function InsertarDato(pDato) {
            var ajaxManager = $find('<%= RadAjaxManager1.ClientID %>');
            ajaxManager.ajaxRequest(pDato);
        }

        //FUNCION ENCAPSULAR DATO
        function EncapsularDatos(pClTipoDato, pLstDatos) {
            return JSON.stringify({ clTipo: pClTipoDato, oSeleccion: pLstDatos });
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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 50px); overflow: auto;">
        <div style="height: 10px;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblClUsuario">*Clave:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClPlaza" Width="300" name="txtClPlaza" runat="server"></telerik:RadTextBox><br />
                <asp:RequiredFieldValidator ID="reqtxtClPlaza" runat="server" Display="Dynamic" ControlToValidate="txtClPlaza" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblClUsuario">*Nombre:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNbPlaza" Width="300" name="txtNbPlaza" runat="server"></telerik:RadTextBox><br />
                <asp:RequiredFieldValidator ID="reqtxtNbPlaza" runat="server" Display="Dynamic" ControlToValidate="txtNbPlaza" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblRol">Empleado:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadListBox ID="lstEmpleado" Width="300" runat="server" OnClientItemDoubleClicking="OpenEmployeeSelectionWindow" Enabled="true">
                    <Items>
                        <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                    </Items>
                </telerik:RadListBox>
                <telerik:RadButton ID="btnBuscarEmpleado" runat="server" Text="B" OnClientClicked="OpenEmployeeSelectionWindow" AutoPostBack="false" Enabled="true"></telerik:RadButton>
                <telerik:RadButton ID="btnLimpiarEmpleado" runat="server" Text="X" OnClientClicked="CleanEmployeeSelection" AutoPostBack="false" Enabled="true"></telerik:RadButton>
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
                <telerik:RadButton ID="btnBuscarPuesto" runat="server" Text="B" OnClientClicked="OpenPuestosSelectionWindow" AutoPostBack="false" Enabled="true"></telerik:RadButton>
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
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblArea">Área/Departamento:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadListBox ID="lstArea" Width="300" runat="server" OnClientItemDoubleClicking="OpenAreaSelectionWindow">
                    <Items>
                        <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                    </Items>
                </telerik:RadListBox>
                <telerik:RadButton ID="btnArea" runat="server" Text="B" OnClientClicked="OpenAreaSelectionWindow" AutoPostBack="false"></telerik:RadButton>
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
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblMiembro">Miembro de:</label>
            </div>
            <div class="divControlDerecha">
                <div class="ctrlBasico" style="width: 420px;">
                    <telerik:RadGrid
                        ID="rgGrupos"
                        runat="server"
                        Width="420"
                        Height="300"
                        AllowPaging="true"
                        AutoGenerateColumns="false"
                        HeaderStyle-Font-Bold="true"
                        EnableHeaderContextMenu="true"
                        AllowMultiRowSelection="true"
                        OnNeedDataSource="rgGrupos_NeedDataSource">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView DataKeyNames="ID_GRUPO, FG_SISTEMA" ClientDataKeyNames="ID_GRUPO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="CL_GRUPO" DataField="CL_GRUPO" HeaderText="Clave" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="30" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_GRUPO" DataField="NB_GRUPO" HeaderText="Nombre" AutoPostBackOnFilter="true" HeaderStyle-Width="290" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="ctrlBasico" style="float: left">
                    <telerik:RadButton ID="btnAgregar" runat="server" Text="B" AutoPostBack="false" ToolTip="Seleccionar grupos" OnClientClicked="OpenGruposWindows"></telerik:RadButton>
                    <div style="clear: both;"></div>
                    <telerik:RadButton ID="btnEliminar" runat="server" Text="X" AutoPostBack="true" ToolTip="Eliminar grupo" OnClick="btnEliminar_Click"></telerik:RadButton>
                </div>
            </div>
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
