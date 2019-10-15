<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaDeCompromisos.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaDeCompromisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript" id="5">

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
        function AbrirVentanaAgregar() {
            var vURL = "VentanaAgregarCompromiso.aspx";
            var vTitulo = "Agregar Compromiso ";
            var oWin = window.radopen(vURL, "winAgregarCompromiso", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
            oWin.set_title(vTitulo);
            //openChildDialog("VentanaAgregarComunicado.aspx", "winAgregarComunicado", "Nuevo Comunicado")
        }

        function recargarEvaluados() {
            $find("<%=grdMisCompromisos.ClientID%>").get_masterTableView().rebind();
        }

        function recargarMetas() {
            <%--$find("<%=grdMisCompromisoSolicitados.ClientID%>").get_masterTableView().rebind();--%>
        }

        function onCloseWindow(oWnd, args) {
            var idEvaluadoMeta = "";
            var idE = "";
           <%-- $find("<%=grdMisCompromisoSolicitados.ClientID%>").get_masterTableView().rebind();--%>
        }

        function GetIdEvaluado() {
           <%-- var grid = $find("<%=grdMisCompromisoSolicitados.ClientID %>");--%>
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

        function ConfirmarEliminarInactivas(sender, args) {
            if ('<%= vClTipoMetas %>' == "DESCRIPTIVO")
                vMensaje = "¿Los indicadores inactivos serán eliminados, estas seguro que deseas continuar?, este proceso no podrá revertirse";
            else
                vMensaje = "¿Las metas inactivas serán eliminadas, estas seguro que deseas continuar?, este proceso no podrá revertirse";

            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

            radconfirm(vMensaje, callBackFunction, 400, 170, null, "Aviso");
            args.set_cancel(true);

            //var vWindowsProperties = { height: 200 };
            //confirmAction(sender, args, vMensaje, vWindowsProperties);
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
            var masterTable = $find("<%= grdMisCompromisos.ClientID %>").get_masterTableView();
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

            if (eventArgs.get_tableView().get_name() == "gtvMisCompromisosSolicitados") {
                if (tableView.get_selectedItems().length == 1) {
                    tableView.clearSelectedItems();
                }
            }
            if (eventArgs.get_tableView().get_name() == "gtvMisCompromisos") {
                if (tableView.get_selectedItems().length == 1) {
                    tableView.clearSelectedItems();
                }
            }

            if (eventArgs.get_tableView().get_name() == "gtvMisTareas") {
                if (tableView.get_selectedItems().length == 1) {
                    tableView.clearSelectedItems();
                }
            }

            if (eventArgs.get_tableView().get_name() == "gtvMisReportes") {
                if (tableView.get_selectedItems().length == 1) {
                    tableView.clearSelectedItems();
                }
            }
        }

        function obtenerIdMeta() {
            <%--var grid = $find("<%=grdMisCompromisoSolicitados.ClientID%>");--%>
            var MasterTable = grid.get_masterTableView();
            var childSelectedRows = null;
            for (var j = 0; j < grid.get_detailTables().length; j++) {
                if (childSelectedRows == null) {
                    if (grid.get_detailTables()[j].get_selectedItems().length > 0) {
                        childSelectedRows = grid.get_detailTables()[j].get_selectedItems();
                    }
                }
            }

            if (childSelectedRows != null) {
                var childSelIDs = new Array(childSelectedRows);
                if (grid.get_detailTables().length > 0) {
                    for (var i = 0; i < childSelectedRows.length; i++) {
                        var row = childSelectedRows[i];
                        idE = row.getDataKeyValue("ID_EVALUADO_META");
                        idEvaluadoMeta = row.getDataKeyValue("ID_EVALUADO");
                        noMeta = row.getDataKeyValue("NO_META");
                        vFgEvaluar = row.getDataKeyValue("FG_EVALUAR");
                    }
                }
            }
        }

        function OpenModificarMetasWindow() {
            idE = "";
            idEvaluadoMeta = "";
            noMeta = "";
            vFgEvaluar = false;
            GetIdEvaluado();
            obtenerIdMeta();
            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vTextoVentana = "Editar";
            if ('<%= vClTipoMetas %>' == "DESCRIPTIVO")
                vTextoVentana = "Editar";

            if (idE != null & idE != "") {
                if (vFgEvaluar != "False")
                    OpenSelectionWindowC("VentanaMetasDesempeno.aspx?IdEvaluado=" + idEvaluadoMeta + "&IdPeriodo=" + vIdPeriodo + "&NoMeta=" + idE + "&Meta=" + noMeta + "&Accion=Editar", "WinMetas", vTextoVentana);
                else {
                    if ('<%= vClTipoMetas %>' == "DESCRIPTIVO")
                        radalert("No se puede editar un indicador inactivo.", 400, 150, "Aviso");
                    else
                        radalert("No se puede editar una meta inactiva.", 400, 150, "Aviso");
                }
            }
            else {
                if ('<%= vClTipoMetas %>' == "DESCRIPTIVO")
                    radalert("Selecciona un indicador.", 400, 150, "Aviso");
                else
                    radalert("Selecciona una meta.", 400, 150, "Aviso");
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
            var ajaxManager = $find('<%= ramConfiguracionPeriodo.ClientID%>');
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
            } else {
               <%-- $find("<%=grdMisCompromisoSolicitados.ClientID%>").get_masterTableView().rebind();--%>
            }
        }

        function confirmarEliminarEvaluados(sender, args) {
            var masterTable = $find("<%= grdMisCompromisos.ClientID %>").get_masterTableView();
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

            var rtsTabs = $find('<%= rtsConfiguracionPeriodoDesempeno.ClientID %>');
            var vSelectedTab = rtsTabs.get_selectedTab();

            var vValueTab = vSelectedTab.get_value();

            if (vValueTab == 4) {
                var ajaxManager = $find('<%= ramConfiguracionPeriodo.ClientID%>');
                ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "VERIFICACONFIGURACION" }));
            }

           <%-- if (vValueTab == 1 && '<%= vFgBajas%>' == "True")
                radalert("Existen evaluados o evaluadores dados de baja en el período.", 400, 150, "Aviso");--%>
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpConfiguracionPeriodo" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConfiguracionPeriodo" runat="server" DefaultLoadingPanelID="ralpConfiguracionPeriodo" OnAjaxRequest="ramConfiguracionPeriodo_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConfiguracionPeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisoSolicitados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdMisReportes" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rgMisTareas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEnvioSolicitudes" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />    
                    <telerik:AjaxUpdatedControl ControlID="btnReasignarContrasena" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"/>    
                    <telerik:AjaxUpdatedControl ControlID="lbMensaje" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />                  
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorPersona">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdMisCompromisos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnActivarMetas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisoSolicitados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDesactivarMetas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisoSolicitados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCopiarMetas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisoSolicitados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCalcularTodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMisTareas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtMontoMisTareas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReasignarPonderacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCalcularSeleccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMisTareas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReasignarTodasContrasenas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisReportes" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting> 
            <telerik:AjaxSetting AjaxControlID="btnReasignarContrasena">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMisReportes" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbSi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMisTareas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbPorcentaje" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbMonto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtMontoMisTareas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbIndividualDependiente" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbIndividualIndependiente" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbGrupal" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnCalcularTodos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnCalcularSeleccion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMisTareas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbPorcentaje" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbMonto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtMontoMisTareas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbIndividualDependiente" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbIndividualIndependiente" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbGrupal" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnCalcularTodos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnCalcularSeleccion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbMetasDescriptivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbMetasDescriptivo" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbMetasCero" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisoSolicitados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbMetasCero">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbMetasDescriptivo" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbMetasCero" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdMisCompromisoSolicitados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarMeta" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnModificarMetas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsConfiguracionPeriodoDesempeno" runat="server" SelectedIndex="0" MultiPageID="ID_MISCOMPROMISOS">
        <Tabs>
            <telerik:RadTab runat="server" Text="Mis Compromisos"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Mis Compromisos Solicitados"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Mis Tareas"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Mis Reportes"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 55px); width: 100%;">
            <%--<div style="height: 10px;"></div>--%>
        <telerik:RadMultiPage ID="ID_MISCOMPROMISOS" runat="server" SelectedIndex="0" Height="100%">
            
            <telerik:RadPageView ID="rpvMis_Compromisos" runat="server">
                <telerik:RadSplitter runat="server" ID="RadSplitter1" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="RadPane1" runat="server">
                        <div style="height: calc(100% - 65px); width: 100%;">
                            <telerik:RadGrid ID="grdMisCompromisos" runat="server" Height="100%"
                                AutoGenerateColumns="false"
                                EnableHeaderContextMenu="true"
                                AllowSorting="true"
                                OnItemCommand="grdMisCompromisos_ItemCommand"
                                AllowMultiRowSelection="true"
                                OnNeedDataSource="grdSeleccionMisCompromisos_NeedDataSource"
                                HeaderStyle-Font-Bold="true"
                                OnItemDataBound="grdMisCompromisos_ItemDataBound"
                                OnDetailTableDataBind="grdMisCompromisos_DetailTableDataBind" ShowFooter="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames=""
                                    AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true"
                                    EnableHeaderContextFilterMenu="true" ClientDataKeyNames="ID_COMPROMISO" EnableHierarchyExpandAll="true" HierarchyDefaultExpanded="false">
                                    <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="50" HeaderText="Titulo" DataField="CL_COMPROMISO"  UniqueName="CL_COMPROMISO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderStyle-Font-Bold="true" HeaderText="Descripción" DataField ="NB_COMPROMISO" UniqueName="NB_COMPROMISO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  DataFormatString="{0:dd/MM/yyyy}" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Solicitado por" DataField="SOLICITADO_POR" UniqueName="SOLICITADO_POR"></telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha Entrega" DataField="FE_ENTREGA" UniqueName="FE_ENTREGA"></telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estatus" DataField="NB_ESTATUS_COMPROMISO" UniqueName="NB_ESTATUS_COMPROMISO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Prioridad" DataField="NB_PRIORIDAD" UniqueName="NB_PRIORIDAD"></telerik:GridBoundColumn>
                                  
                                    </Columns>
                           
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccionPorPersona" runat="server" Text="Genarar Nuevo Compromiso" AutoPostBack="false" OnClientClicked="AbrirVentanaAgregar"></telerik:RadButton>
                        </div>
                  
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnEliminarEvaluado" runat="server" Text="Eliminar" OnClientClicking="confirmarEliminarEvaluados" OnClick="btnEliminarEvaluado_Click"></telerik:RadButton>
                        </div>
                        <div class="divControlesBoton">
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnGuardarEvaluado" Text="Guardar" OnClick="btnGuardarEvaluado_Click"></telerik:RadButton>
                            </div>
                        </div>
                    </telerik:RadPane>
             
                </telerik:RadSplitter>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvMisCompromisoSolicitados" runat="server">
                <telerik:RadSplitter runat="server" ID="spHelp1" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="rpHelp1" runat="server">
                        <div style="height: calc(100% - 65px); width: 100%;">
                            <telerik:RadGrid ID="grdMisCompromisoSolicitados" runat="server"
                                Height="100%" AutoGenerateColumns="false"  EnableHierarchyExpandAll="true" MasterTableView-HierarchyDefaultExpanded="true"
                                EnableHeaderContextMenu="true" AllowSorting="true"
                                AllowMultiRowSelection="true"
                                OnNeedDataSource="grdMisCompromisoSolicitados_NeedDataSource"
                                OnDetailTableDataBind="grdMisCompromisoSolicitados_DetailTableDataBind"
                                OnItemDataBound="grdMisCompromisoSolicitados_ItemDataBound"
                                OnItemCommand="grdMisCompromisoSolicitados_ItemCommand" HeaderStyle-Font-Bold="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" SaveScrollPosition="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="" ClientDataKeyNames="ID_COMPROMISO" AllowPaging="true"
                                    AllowFilteringByColumn="true" AllowSorting="true" Name="Mis Compromisos Solicitados"
                                    ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                    <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="50" HeaderText="Título" DataField="CL_COMPROMISO"  UniqueName="CL_COMPROMISO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderStyle-Font-Bold="true" HeaderText="Descripción" DataField="NB_COMPROMISO" UniqueName="NB_COMPROMISO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Asignado a" DataField="ASIGNADO_A" UniqueName="ASIGNADO_A"></telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha Entrega" DataField="FE_ENTREGA" UniqueName="FE_ENTREGA"></telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estatús" DataField="NB_ESTATUS_COMPROMISO" UniqueName="NB_ESTATUS_COMPROMISO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Prioridad" DataField="NB_PRIORIDAD" UniqueName="NB_PRIORIDAD"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="Calificación"  HeaderStyle-Font-Bold="true" DataField="NB_CALIFICACION" UniqueName="NB_CALIFICACION"></telerik:GridBoundColumn>
                                    </Columns>
                     
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                       
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnActivarMetas"  runat="server" AutoPostBack="true" Text="Generar Nuevo Compromiso"  OnClientClicked="AbrirVentanaAgregar"></telerik:RadButton>
                        </div>
                       
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnCopiarMetas" runat="server" OnClick="btnCopiarMetas_Click" Text="Eliminar"></telerik:RadButton>
                        </div>
                        <div class="divControlesBoton">
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnGuardarMetas" Text="Guardar" OnClientClicking="ConfirmarEliminarInactivas" OnClick="btnGuardarMetas_Click"></telerik:RadButton>
                            </div>
                        </div>
                    </telerik:RadPane>
                
                </telerik:RadSplitter>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvMisTareas" runat="server">
                 <telerik:RadSplitter runat="server" ID="RadSplitter2" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="RadPane3" runat="server">
                        <div style="height: calc(100% - 65px); width: 100%;">
                        <div style="clear: both; height: 5px;"></div>
                <div style="height: calc(80% - 80px); width: 100%;">
                    <telerik:RadGrid ID="grdMisTareas" runat="server"
                                Height="100%" AutoGenerateColumns="false"  EnableHierarchyExpandAll="true" MasterTableView-HierarchyDefaultExpanded="true"
                                EnableHeaderContextMenu="true" AllowSorting="true"
                                AllowMultiRowSelection="true"
                                OnNeedDataSource="grdMisTareas_NeedDataSource"
                                OnDetailTableDataBind="grdMisTareas_DetailTableDataBind"
                                OnItemDataBound="grdMisTareas_ItemDataBound"
                                OnItemCommand="grdMisTareas_ItemCommand" HeaderStyle-Font-Bold="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" SaveScrollPosition="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="" ClientDataKeyNames="ID_COMPROMISO" AllowPaging="true"
                                    AllowFilteringByColumn="true" AllowSorting="true" Name="Mis Compromisos Solicitados"
                                    ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>                                
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="Titulo" DataField="CL_COMPROMISO"  UniqueName="CL_COMPROMISO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="130" HeaderText="Descripción" DataField="NB_COMPROMISO"  UniqueName="NB_COMPROMISO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Fecha Entrega" DataField="FE_ENTREGA"  UniqueName="FE_ENTREGA" HeaderStyle-Font-Bold="true"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Estatus" DataField="NB_ESTATUS_COMPROMISO" UniqueName="NB_ESTATUS_COMPROMISO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Prioridad" DataField="NB_PRIORIDAD" UniqueName="NB_PRIORIDAD" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                </div>
                        
                <div class="pos">
                    <telerik:RadButton runat="server"  Text="Generar Nuevo Compromiso" OnClientClicked="AbrirVentanaAgregar" AutoPostBack="true" ></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server"   Text="Eliminar" ></telerik:RadButton>
                </div>
                <div class="divControlesBoton">
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server"  ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                    </div>
                </div>

                 </telerik:RadPane>
                   
                </telerik:RadSplitter>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvMisReportes_" runat="server">
                <div style="height: calc(100% - 45px); padding-bottom: 10px;">
                    <telerik:RadGrid ID="grdMisReportes" runat="server" Height="100%" AllowMultiRowSelection="true" AutoGenerateColumns="false" AllowSorting="true"
                        OnNeedDataSource="grdMisReportes_NeedDataSource" HeaderStyle-Font-Bold="true" 
                        OnItemDataBound="grdMisReportes_ItemDataBound" 
                        OnDetailTableDataBind="grdMisReportes_DetailTableDataBind">
                        <ClientSettings EnableAlternatingItems="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" SaveScrollPosition="true" />
                            <Selecting AllowRowSelect="true"  />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="" ClientDataKeyNames="ID_COMPROMISO" AllowSorting="true" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                         
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" Visible="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Periodo de" DataField="PERIODO_DEL" UniqueName="PERIODO_DEL" HeaderStyle-Font-Bold="true"></telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" Visible="true"  CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Al" DataField="PERIODO_AL" UniqueName="PERIODO_AL" HeaderStyle-Font-Bold="true"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Evaluado" DataField="EVALUADO" UniqueName="EVALUADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Calificación Global" DataField="CALIFICACION_GLOBAL" UniqueName="CALIFICACION_GLOBAL" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Título del Compromiso" DataField="CL_COMPROMISO" UniqueName="CL_COMPROMISO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calificación" DataField="NB_CALIFICACION" UniqueName="NB_CALIFICACION" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            </Columns> 
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both;"></div>
                <label id="lbMensaje" runat="server" visible="false" style="color: red;">.</label>
                <div style="clear: both;"></div>
              <%--  <div class="ctrlBasico">
                    <telerik:RadButton ID="btnReasignarTodasContrasenas" runat="server" Text="Reasignar contraseñas a todos" OnClick="btnReasignarTodasContrasenas_Click"></telerik:RadButton>
                </div>--%>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnReasignarContrasena" runat="server" Text="Generar Nuevo Compromiso" OnClick="btnReasignarContrasena_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEnvioSolicitudes" runat="server" Text="Eliminar" OnClientClicking="OpenEnvioSolicitudes"></telerik:RadButton>
                </div>
                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnGuardarCerrar" Text="Guardar" OnClick="btnGuardarCerrar_Click"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
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
            <telerik:RadWindow ID="winAgregarCompromiso" runat="server" Title="Agregar compromiso" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup">
            </telerik:RadWindow><telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccion" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
        
</asp:Content>