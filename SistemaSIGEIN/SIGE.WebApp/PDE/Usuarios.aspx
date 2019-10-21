<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="SIGE.WebApp.PDE.Usuarios" %>



<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="5" type="text/javascript">
        var idEvaluadoMeta = "";
        var idE = "";
        var noMeta = "";
        var vIdEvaluado = null;
        var vNoMetaSeleccionada = null;
        var vFgEvaluar = false;

        function closeWindow() {
            GetRadWindow().close();
        }

        function CloseWindowConfig() {
            GetRadWindow().close();
        }

        function recargarEvaluados() {
            $find("<%=grdEvaluados.ClientID%>").get_masterTableView().rebind();
        }

        function GetIdEvaluado() {
            <%--//var grid = $find("<%=grdDisenoMetas.ClientID %>");--%>
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                vIdEvaluado = row.getDataKeyValue("ID_EVALUADO");
            }
            else {
                vIdEvaluado = null;
            }
        }


        function OpenRemplazaBaja(pIdEvaluado, pIdPeriodo) {
        }



        function OpenEnvioSolicitudes() {
            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vClOrigen = '<%= vClOrigenPeriodo %>';
            var vNoReplica = '<%= vNoReplica %>';
            if (vIdPeriodo != null) {
                if (vClOrigen != "REPLICA" && vNoReplica == 0) {
                    OpenWindow(GetEnvioSolicitudesProperties(vIdPeriodo));
                }
                else {
                    OpenWindow(GetEnvioSolicitudesReplicas(vIdPeriodo));
                }
            }
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function GetEnvioSolicitudesProperties(pIdPeriodo) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var wnd = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            wnd.vRadWindowId = "WinEnvioSolicitudes";
            wnd.vURL = "VentanaEnvioSolicitudes.aspx";
            if (pIdPeriodo != null) {
                wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                wnd.vTitulo = "Enviar evaluaciones";
            }
            return wnd;
        }

        function GetEnvioSolicitudesReplicas(pIdPeriodo) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var wnd = {
                width: browserWnd.innerWidth - 800,
                height: browserWnd.innerHeight - 20
            };
            wnd.vRadWindowId = "WinPeriodoReplica";
            wnd.vURL = "VentanaEnvioSolicitudesReplica.aspx";
            if (pIdPeriodo != null) {
                wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                wnd.vTitulo = "Envío de solicitudes";
            }
            return wnd;
        }

        function OpenEmpleadosSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "winSeleccion", "Selección de evaluados");
        }

        function OpenPuestoSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPuesto.aspx?mulSel=1", "winSeleccion", "Selección de evaluados por puestos");
        }

        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx?", "winSeleccion", "Selección de evaluados por áreas/departamento");
        }

        function OpenEmpleadosSelectionWindowEvaluador() {
            var masterTable = $find("<%= grdEvaluados.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=EVALUADOR&mulSel=0&CLFILTRO=NINGUNO", "winSeleccion", "Selección de evaluadores");
            }
            else {
                radalert("Selecciona un evaluado.", 400, 150, "Aviso");
            }
        }

        function OpenAgregarMetasWindow() {
            GetIdEvaluado();
            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vTextoVentana = "Agregar";
            if ('<%= vClTipoMetas %>' == "DESCRIPTIVO")
                vTextoVentana = "Agregar";

            if (vIdEvaluado != null) {
                OpenSelectionWindowC("VentanaMetasDesempeno.aspx?IdEvaluado=" + vIdEvaluado + "&IdPeriodo=" + vIdPeriodo + "&Accion=Agregar", "WinMetas", vTextoVentana);
            }
            else {
                radalert("Selecciona un evaluado.", 400, 150, "Aviso");
            }
        }

        function RowSelecting(sender, eventArgs) {
            var tableView = eventArgs.get_tableView();

            if (eventArgs.get_tableView().get_name() == "gtvMetas") {
                if (tableView.get_selectedItems().length == 1) {
                    tableView.clearSelectedItems();
                }
            }
            if (eventArgs.get_tableView().get_name() == "gtvEvaluadores") {
                if (tableView.get_selectedItems().length == 1) {
                    tableView.clearSelectedItems();
                }
            }
        }





        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 30,
                height: browserWnd.innerHeight - 30
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenSelectionWindowC(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: 1200,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenSelectionDefinedWindow(pURL, pIdWindow, pTitle, pWidth, pHeight) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: pWidth,
                height: pHeight
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertEvaluado(pDato) {
            var ajaxManager = $find('<%= ramUsuarios.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function InsertEvaluador(pDato) {
            var listaEvaluados = pDato;
        }

        function FindItemByValue(pList, pValue) {
            for (var i = 0; i < pList.length; i++)
                if (pList == pValue)
                    return i;
            return null;
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo || vDatosSeleccionados.clTipoCatalogo) {
                    case "EMPLEADO":
                        InsertEvaluado(EncapsularSeleccion("EVALUADO", pDato));
                        break;
                    case "PUESTO":
                        InsertEvaluado(EncapsularSeleccion("PUESTO", pDato));
                        break;
                    case "DEPARTAMENTO":
                        InsertEvaluado(EncapsularSeleccion("AREA", pDato));
                        break;
                    case "EVALUADOR":
                        InsertEvaluado(EncapsularSeleccion("EVALUADOR", pDato));
                        break;
                }
            }
        }

        function confirmarEliminarEvaluados(sender, args) {
            var masterTable = $find("<%= grdEvaluados.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                var vNombre = "";
                var vMensaje = "";
                if (selectedItems.length == 1) {
                    vNombre = masterTable.getCellByColumnUniqueName(selectedItems[0], "NB_EMPLEADO_COMPLETO").innerHTML;
                    vMensaje = "¿Deseas eliminar a " + vNombre + " como evaluado?, este proceso no podrá revertirse";
                }
                else {
                    vNombre = selectedItems.length;
                    vMensaje = "¿Deseas eliminar a los " + vNombre + " evaluadores seleccionados?, este proceso no podrá revertirse";
                }
                //var vWindowsProperties = { height: 200 };

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                radconfirm(vMensaje, callBackFunction, 400, 170, null, "Aviso");
                args.set_cancel(true);

            }
            else {
                radalert("Selecciona un evaluado.", 400, 150, "Aviso");
                args.set_cancel(true);
            }
        }

        function ChangeControlState(pCtrlCheckbox, pFgEnabled, pClTipoControl) {
            if (pCtrlCheckbox) {
                switch (pClTipoControl) {
                    case "CHECKBOX":
                        if (!pFgEnabled)
                            pCtrlCheckbox.set_checked(pFgEnabled);
                        break;
                    case "LISTBOX":
                        var vListBox = pCtrlCheckbox;
                        vListBox.trackChanges();

                        var items = vListBox.get_items();
                        items.clear();

                        var item = new Telerik.Web.UI.RadListBoxItem();
                        item.set_text("No seleccionado");
                        item.set_value("");
                        items.add(item);
                        item.set_selected(true);
                        vListBox.commitChanges();
                        break;
                }
                pCtrlCheckbox.set_enabled(pFgEnabled);
            }
        }

        function ChangeCheckState(pCtrlCheckbox, pFgEnabled) {
            ChangeControlState(pCtrlCheckbox, pFgEnabled, "CHECKBOX");
        }

        function ChangeListState(pCtrlCheckbox, pFgEnabled) {
            ChangeControlState(pCtrlCheckbox, pFgEnabled, "LISTBOX");
        }

        function SeleccionarPorPuesto() {
            EvaluacionSettings(cClTipoEvaluacionPorPuesto);
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
        }

        var sumInput = null;
        var tempValue = 0.0;

        function Load(sender, args) {
            sumInput = sender;
        }

        function Blur(sender, args) {
            sumInput.set_value(tempValue + sender.get_value());

        }

        function Focus(sender, args) {
            tempValue = sumInput.get_value() - sender.get_value();
        }

        function OnClientSelected() {

            var rtsTabs = $find('<%= rtsConfiguracion.ClientID %>');
            var vSelectedTab = rtsTabs.get_selectedTab();

            var vValueTab = vSelectedTab.get_value();

            if (vValueTab == 4) {
                var ajaxManager = $find('<%= ramUsuarios.ClientID%>');
                ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "VERIFICACONFIGURACION" }));
            }


        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server" >

   
    <telerik:RadAjaxLoadingPanel ID="ralpUsuarios" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramUsuarios" runat="server" DefaultLoadingPanelID="ralpUsuarios" OnAjaxRequest="ramConfiguracionPeriodo_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramUsuarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />                
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarInformacionGeneral">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnGuardarInformacionGeneral" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorPersona">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEvaluados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <br />

    <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
        <Tabs>
            <telerik:RadTab Text="Selección de Usuarios"></telerik:RadTab>
            <telerik:RadTab Text="Procesos"></telerik:RadTab>
            <telerik:RadTab Text="Contraseña"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

    <div style="height: calc(100% - 130px); padding-top: 10px;">

        <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">

            <telerik:RadPageView ID="rpvSeleccionUsuarios" runat="server">


                <div style="height: calc(100% - 100px);">

                    <telerik:RadGrid ID="grdEvaluados" runat="server" OnNeedDataSource="grdUsuarios_NeedDataSource" OnItemCommand="grdEvaluados_ItemCommand"
                        AllowMultiRowSelection="true"
                        OnItemDataBound="grdEvaluados_ItemDataBound"
                                OnDetailTableDataBind="grdEvaluados_DetailTableDataBind" ShowFooter="true"
                        Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="True" AllowPaging="true"
                        AllowSorting="true" HeaderStyle-Font-Bold="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EMPLEADO,ID_EVALUADO,CL_EMPLEADO,NB_EMPLEADO_COMPLETO,NB_PUESTO,NB_DEPARTAMENTO,PR_EVALUADO, CL_ESTADO_EMPLEADO"
                                    AllowPaging="false" AllowSorting="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true"
                                    EnableHeaderContextFilterMenu="true" ClientDataKeyNames="ID_EVALUADO" EnableHierarchyExpandAll="true" HierarchyDefaultExpanded="false">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="120" HeaderText="Nombre Completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="170" FilterControlWidth="100" HeaderText="Puesto" DataField="M_PUESTO_NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="100" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="60" HeaderStyle-Width="120" HeaderText="Nombre de la empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="20" HeaderStyle-Width="70 " HeaderText="Estatus" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>

                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorPersona" runat="server" Text="Seleccionar por persona" AutoPostBack="false" OnClientClicked="OpenEmpleadosSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorPuesto" runat="server" Text="Seleccionar por puesto" AutoPostBack="false" OnClientClicked="OpenPuestoSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorArea" runat="server" Text="Seleccionar por área/departamento" AutoPostBack="false" OnClientClicked="OpenAreaSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="RadButton1" runat="server" Text="Eliminar" AutoPostBack="false" ></telerik:RadButton>
                </div>
                <div class="divControlesBoton">
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnGuardarEvaluado" Text="Guardar" AutoPostBack="false"></telerik:RadButton>
                    </div>
                </div>


                

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvProcesos" runat="server">

                <div style="margin-left: 20px;">

                    <label id="lbPeriodo" name="lbTabulador" runat="server">Acceso a:</label>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div1" runat="server">
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="rdComunicados" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" >
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No"  PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisComunicados">Mis comunicados.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div2" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdTramites" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" >
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí"  PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No"  PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisTramites">Mis Trámites.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div3" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdCompromisos" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" >
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí"  PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No"  PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisCompromisos">Mis Compromisos.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div4" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdNomina" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" >
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí"  PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No"  PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisNomina">Mi Nómina.</label>
                        </div>
                    </div>
                </div>

                <div style="clear: both; height: 20px;"></div>

                <%--<div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarProcesos" runat="server" name="btnGuardarProcesos" AutoPostBack="true" Text="Guardar" OnClick="btnGuardarProcesos_Click"></telerik:RadButton>
                    </div>
                </div>--%>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvContraseña" runat="server">
                <div style="clear: both;"></div>

                <div style="height: calc(100% - 100px);">

                    <telerik:RadGrid ID="grdContrasenas" runat="server" 
                        Height="100%" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true"
                        AllowSorting="true" HeaderStyle-Font-Bold="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="CL_USUARIO, FG_ACTIVO,FG_ENVIO" DataKeyNames="CL_USUARIO, FG_ACTIVO,FG_ENVIO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Usuario" DataField="CL_USUARIO" UniqueName="CL_USUARIO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="120" HeaderText="Nombre" DataField="NB_USUARIO" UniqueName="NB_USUARIO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="20" HeaderStyle-Width="70 " HeaderText="Contraseña" DataField="" UniqueName=""></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

            </telerik:RadPageView>

           
        </telerik:RadMultiPage>
    </div>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnExito" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server">
        <Windows>
            <telerik:RadWindow ID="WinMetas" runat="server" ReloadOnShow="true" Animation="Fade"
                ShowContentDuringLoad="false"
                VisibleStatusbar="false"
                VisibleTitlebar="true"
                Behaviors="Close"
                Modal="true"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>