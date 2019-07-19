<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaConfigurarDesempeno.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaConfigurarDesempeno" %>

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

        function recargarEvaluados() {
            $find("<%=grdEvaluados.ClientID%>").get_masterTableView().rebind();
        }

        function recargarMetas() {
            $find("<%=grdDisenoMetas.ClientID%>").get_masterTableView().rebind();
        }

        function onCloseWindow(oWnd, args) {
            var idEvaluadoMeta = "";
            var idE = "";
            $find("<%=grdDisenoMetas.ClientID%>").get_masterTableView().rebind();
        }

        function GetIdEvaluado() {
            var grid = $find("<%=grdDisenoMetas.ClientID %>");
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

        function obtenerIdMeta() {
            var grid = $find("<%=grdDisenoMetas.ClientID%>");
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
                $find("<%=grdDisenoMetas.ClientID%>").get_masterTableView().rebind();
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

            var rtsTabs = $find('<%= rtsConfiguracionPeriodoDesempeno.ClientID %>');
                   var vSelectedTab = rtsTabs.get_selectedTab();

                   var vValueTab = vSelectedTab.get_value();

            if (vValueTab == 4) {
                var ajaxManager = $find('<%= ramConfiguracionPeriodo.ClientID%>');
                ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "VERIFICACONFIGURACION"}));
            }

            if (vValueTab == 1 && '<%= vFgBajas%>' == "True")
                radalert("Existen evaluados o evaluadores dados de baja en el período.", 400, 150, "Aviso");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpConfiguracionPeriodo" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConfiguracionPeriodo" runat="server" DefaultLoadingPanelID="ralpConfiguracionPeriodo" OnAjaxRequest="ramConfiguracionPeriodo_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConfiguracionPeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdDisenoMetas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdContrasenaEvaluadores" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rgBono" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEnvioSolicitudes" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />    
                    <telerik:AjaxUpdatedControl ControlID="btnReasignarContrasena" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"/>    
                    <telerik:AjaxUpdatedControl ControlID="lbMensaje" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />                  
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
            <telerik:AjaxSetting AjaxControlID="btnActivarMetas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDisenoMetas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDesactivarMetas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDisenoMetas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCopiarMetas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDisenoMetas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCalcularTodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgBono" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtMontoBono" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReasignarPonderacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCalcularSeleccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgBono" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReasignarTodasContrasenas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdContrasenaEvaluadores" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReasignarContrasena">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdContrasenaEvaluadores" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbSi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgBono" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbPorcentaje" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbMonto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtMontoBono" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbIndividualDependiente" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbIndividualIndependiente" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbGrupal" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnCalcularTodos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnCalcularSeleccion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgBono" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbPorcentaje" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbMonto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtMontoBono" UpdatePanelHeight="100%" />
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
                    <telerik:AjaxUpdatedControl ControlID="grdDisenoMetas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbMetasCero">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbMetasDescriptivo" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbMetasCero" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdDisenoMetas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarMeta" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnModificarMetas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsConfiguracionPeriodoDesempeno" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion" OnClientTabSelected="OnClientSelected">
        <Tabs>
            <telerik:RadTab Text="Contexto" Value="0"></telerik:RadTab>
            <telerik:RadTab Text="Selección de evaluados" Value="1"></telerik:RadTab>
            <telerik:RadTab Text="Diseñar metas" Value="2"></telerik:RadTab>
            <telerik:RadTab Text="Definir bono" Selected="True" Value="3"></telerik:RadTab>
            <telerik:RadTab Text="Contraseñas" Value="4"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 55px); width: 100%;">
            <div style="height: 10px;"></div>
        <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView runat="server" Width="100%">
                <div class="divControlIzquierda" style="width: 60%; text-align: left;">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtClPeriodo" runat="server"></div>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td class="ctrlTableDataContext">
                                <label>Nombre del periodo:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNbPeriodo" runat="server"></div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Descripción:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtPeriodos" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Notas:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNotas" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Fecha:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtFechas" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de período:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de bono:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoBono" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de metas:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtTipoMetas" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de capturista:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtTipoCapturista" runat="server"></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvSeleccionEvaluados" runat="server">
                <telerik:RadSplitter runat="server" ID="RadSplitter1" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="RadPane1" runat="server">
                        <div style="height: calc(100% - 65px); width: 100%;">
                            <telerik:RadGrid ID="grdEvaluados" runat="server" Height="100%"
                                AutoGenerateColumns="false"
                                EnableHeaderContextMenu="true"
                                AllowSorting="true"
                                OnItemCommand="grdEvaluados_ItemCommand"
                                AllowMultiRowSelection="true"
                                OnNeedDataSource="grdSeleccionEvaluados_NeedDataSource"
                                HeaderStyle-Font-Bold="true"
                                OnItemDataBound="grdEvaluados_ItemDataBound"
                                OnDetailTableDataBind="grdEvaluados_DetailTableDataBind" ShowFooter="true">
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
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="50" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderStyle-Font-Bold="true" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno" DataField="NB_APELLIDO_PATERNO" UniqueName="NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido materno" DataField="NB_APELLIDO_MATERNO" UniqueName="NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderStyle-Font-Bold="true" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave del área/departamento" DataField="CL_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_CL_DEPARTAMENTO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderStyle-Font-Bold="true" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género" DataField="CL_GENERO" UniqueName="CL_GENERO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado civil" DataField="CL_ESTADO_CIVIL" UniqueName="CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre del cónyuge" DataField="NB_CONYUGUE" UniqueName="NB_CONYUGUE"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="RFC" DataField="CL_RFC" UniqueName="CL_RFC"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="CURP" DataField="CL_CURP" UniqueName="CL_CURP"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="NSS" DataField="CL_NSS" UniqueName="CL_NSS"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Tipo sanguíneo" DataField="CL_TIPO_SANGUINEO" UniqueName="CL_TIPO_SANGUINEO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nacionalidad" DataField="CL_NACIONALIDAD" UniqueName="CL_NACIONALIDAD"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="País" DataField="NB_PAIS" UniqueName="NB_PAIS"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Entidad federativa" DataField="NB_ESTADO" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Municipio" DataField="NB_MUNICIPIO" UniqueName="NB_MUNICIPIO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Colonia" DataField="NB_COLONIA" UniqueName="NB_COLONIA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calle" DataField="NB_CALLE" UniqueName="NB_CALLE"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Exterior" DataField="NO_EXTERIOR" UniqueName="NO_EXTERIOR"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Interior" DataField="NO_INTERIOR" UniqueName="NO_INTERIOR"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Código postal" DataField="CL_CODIGO_POSTAL" UniqueName="CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Correo electrónico" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Natalicio" DataField="FE_NACIMIENTO" UniqueName="FE_NACIMIENTO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Lugar de nacimiento" DataField="DS_LUGAR_NACIMIENTO" UniqueName="DS_LUGAR_NACIMIENTO"></telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de alta" DataField="FE_ALTA" UniqueName="FE_ALTA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de baja" DataField="FE_BAJA" UniqueName="FE_BAJA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo" DataField="MN_SUELDO" UniqueName="MN_SUELDO" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo variable" DataField="MN_SUELDO_VARIABLE" UniqueName="MN_SUELDO_VARIABLE" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Composición del sueldo" DataField="DS_SUELDO_COMPOSICION" UniqueName="DS_SUELDO_COMPOSICION"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave de la empresa" DataField="CL_EMPRESA" UniqueName="CL_EMPRESA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre de la empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Razón social" DataField="NB_RAZON_SOCIAL" UniqueName="NB_RAZON_SOCIAL"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="90" FilterControlWidth="30" HeaderText="Estatus" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Activo" DataField="FG_ACTIVO" UniqueName="FG_ACTIVO"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Ponderación" FilterControlWidth="30px" DataType="System.Decimal" HeaderStyle-Font-Bold="true" DataField="PR_EVALUADO" UniqueName="PR_EVALUADO" Aggregate="Sum" FooterAggregateFormatString="Cumplimiento general: {0:N2}%" FooterStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="90px" HorizontalAlign="Left" />
                                            <HeaderStyle Width="90px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtPonderacion" MinValue="0" Text='<%# Eval("PR_EVALUADO")%>' MaxValue="100" NumberFormat-DecimalDigits="2" DataType="Decimal" runat="server" Width="100px" OnTextChanged="txtPonderacion_TextChanged">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="ID_EVALUADO,ID_EVALUADOR" ClientDataKeyNames="ID_EVALUADO,ID_EVALUADOR" Name="gtvEvaluadores"
                                            NoDetailRecordsText="No se han asignado evaluadores">
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="NB_EVALUADOR" DataField="NB_EVALUADOR" HeaderText="Evaluador" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="150" />
                                                    <ItemStyle Width="150" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="150" />
                                                    <ItemStyle Width="150" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CL_CORREO_ELECTRONICO" DataField="CL_CORREO_ELECTRONICO" HeaderText="Correo electrónico" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="150" />
                                                    <ItemStyle Width="150" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CL_ESTADO" DataField="CL_ESTADO" HeaderText="Estatus" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="150" />
                                                    <ItemStyle Width="150" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn CommandName="Delete" UniqueName="Delete" ButtonType="ImageButton" HeaderStyle-Width="30"
                                                    ConfirmTextFormatString="¿Desea eliminar el evaluador?" ConfirmTitle="Confirmar" ConfirmDialogWidth="400" HeaderStyle-Font-Bold="true"
                                                    ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
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
                            <telerik:RadButton ID="btnAgregarEvaluador" Visible="false" runat="server" AutoPostBack="false" OnClientClicked="OpenEmpleadosSelectionWindowEvaluador" Text="Agregar evaluador"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton runat="server" ID="btnReasignarPonderacion" AutoPostBack="true" Text="Reasignar ponderación" OnClick="btnReasignarPonderacion_Click" ToolTip="Selecciona ésta opción si lo que deseas es modificar de manera automática y equitativa el impacto de cada uno de los evaluados."></telerik:RadButton>
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
                    <telerik:RadPane ID="RadPane2" runat="server" Width="30">
                        <telerik:RadSlidingZone ID="RadSlidingZone2" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                            <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Title="Ayuda" Width="300" MinWidth="500" Height="100%">
                                <div style="padding: 10px; text-align: justify;">
                                        Está página te permitirá seleccionar a los evaluados ya sea por persona, puesto o área/departamento. 
                                     <br />
                                        Podrás capturar la ponderación asignada a cada evaluado directamente o asignar una ponderación equitativa para todos, seleccionando el botón "Reasignar ponderación". Recuerda que la suma total debe ser 100%.
                                     <br />
                                        Una vez que hayas seleccionado a los evaluados y asignado una ponderación da clic en guardar para verificar la correcta configuración.  
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvDisenoMetas" runat="server">
                <telerik:RadSplitter runat="server" ID="spHelp1" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="rpHelp1" runat="server">
                        <div style="height: calc(100% - 65px); width: 100%;">
                            <telerik:RadGrid ID="grdDisenoMetas" runat="server"
                                Height="100%" AutoGenerateColumns="false"  EnableHierarchyExpandAll="true" MasterTableView-HierarchyDefaultExpanded="true"
                                EnableHeaderContextMenu="true" AllowSorting="true"
                                AllowMultiRowSelection="true"
                                OnNeedDataSource="grdDisenoMetas_NeedDataSource"
                                OnDetailTableDataBind="grdDisenoMetas_DetailTableDataBind"
                                OnItemDataBound="grdDisenoMetas_ItemDataBound"
                                OnItemCommand="grdDisenoMetas_ItemCommand" HeaderStyle-Font-Bold="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" SaveScrollPosition="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="false" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_EMPLEADO,ID_EVALUADO" ClientDataKeyNames="ID_EVALUADO" AllowPaging="true"
                                    AllowFilteringByColumn="true" AllowSorting="true" Name="Evaluados"
                                    ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                    <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    </Columns>
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="ID_EVALUADO_META,ID_EVALUADO,NO_META, FG_EVALUAR" ClientDataKeyNames="ID_EVALUADO_META,ID_EVALUADO,NO_META, FG_EVALUAR" Name="gtvMetas"
                                            NoDetailRecordsText="No se han asignado metas en el descriptivo de puestos" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                                <telerik:GridTemplateColumn HeaderText="Estatus" DataField="FG_EVALUAR" UniqueName="FG_EVALUAR" AllowFiltering="false" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="50" />
                                                    <ItemTemplate>
                                                        <div style="width: 80%; text-align: center;">
                                                            <img src='<%# Eval("FG_EVALUAR").ToString().Equals("True") ? "../Assets/images/Aceptar.png" : "../Assets/images/Cancelar.png"  %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_INDICADOR" DataField="NB_INDICADOR" HeaderText="Indicador" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="150" />
                                                    <ItemStyle Width="150" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="DS_META" DataField="DS_META" HeaderText="Meta" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="120" />
                                                    <ItemStyle Width="120" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CL_TIPO_META" DataField="CL_TIPO_META" HeaderText="Tipo de meta" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="80" />
                                                    <ItemStyle Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_CUMPLIMIENTO_ACTUAL" DataField="NB_CUMPLIMIENTO_ACTUAL" HeaderText="Actual" DataType="System.String" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="80" />
                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_CUMPLIMIENTO_MINIMO" DataField="NB_CUMPLIMIENTO_MINIMO" HeaderText="Mínimo" DataType="System.String" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="80" />
                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_CUMPLIMIENTO_SATISFACTORIO" DataField="NB_CUMPLIMIENTO_SATISFACTORIO" HeaderText="Satisfactorio" DataType="System.String" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="80" />
                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_CUMPLIMIENTO_SOBRESALIENTE" DataField="NB_CUMPLIMIENTO_SOBRESALIENTE" HeaderText="Sobresaliente" DataType="System.String" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="80" />
                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PR_META" DataField="PR_META" HeaderText="Ponderación" DataType="System.Double" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Width="80" />
                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="Delete" ButtonType="ImageButton" HeaderStyle-Width="40"
                                                    ConfirmText="¿Desea eliminar la meta?"
                                                    ConfirmTextFormatString="¿Desea eliminar la meta?" ConfirmTitle="Aviso" ConfirmDialogWidth="400"
                                                    ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnAgregarMeta" runat="server" name="btnAgregarMeta" AutoPostBack="false" Text="Agregar meta" OnClientClicked="OpenAgregarMetasWindow" Enabled="false" ToolTip="Selecciona esta opción si lo que deseas es dar de alta una meta."></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnActivarMetas" runat="server" AutoPostBack="true" Text="Activar" OnClick="btnActivarMetas_Click"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnDesactivarMetas" runat="server" OnClick="btnDesactivarMetas_Click" Text="Desactivar"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnModificarMetas" runat="server" AutoPostBack="false" Text="Editar" OnClientClicked="OpenModificarMetasWindow" Enabled="false"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnCopiarMetas" runat="server" OnClick="btnCopiarMetas_Click" Text="Copiar"></telerik:RadButton>
                        </div>
                        <div class="divControlesBoton">
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnGuardarMetas" Text="Guardar" OnClientClicking="ConfirmarEliminarInactivas" OnClick="btnGuardarMetas_Click"></telerik:RadButton>
                            </div>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpHelpAyuda1" runat="server" Width="30">
                        <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                            <telerik:RadSlidingPane ID="rspMensaje" runat="server" Title="Ayuda" Width="300" MinWidth="500" Height="100%">
                                <div style="padding: 10px; text-align: justify;">
                                                                            Está página te permitirá diseñar metas de evaluación fácilmente. 
                                     <br />
                                        Copiar metas:
                                <br />
                                        Podrás copiar metas a los empleados que elijas.
                                        Una vez que diseñadas las metas, sus niveles y ponderación, para copiarlas a otros evaluados deberás seleccionar aquellas metas que desees copiar, después selecciona las personas a quienes se les asignaran los elementos a copiar y por último da clic en el botón "Copiar metas"  para realizar el copiado.   

                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvDefinirBono" runat="server">
                 <telerik:RadSplitter runat="server" ID="RadSplitter2" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="RadPane3" runat="server">
                        <div style="height: calc(100% - 65px); width: 100%;">
                <div class="ctrlBasico">
                    <fieldset>
                        <legend>
                            <telerik:RadLabel runat="server" ID="lblDsAsignarBono">¿Desea asignar un bono para este período?</telerik:RadLabel>
                        </legend>
                        <div style="margin-left: 10px;">
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="rbSi" runat="server" ToggleType="Radio" Width="100px"
                                    GroupName="grbAsignacionBonos" Text="Si" OnClick="rbSi_Click">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="rbNo" runat="server" ToggleType="Radio" Width="100px"
                                    GroupName="grbAsignacionBonos" Text="No" OnClick="rbNo_Click">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="ctrlBasico">
                    <fieldset>
                        <legend>
                            <telerik:RadLabel runat="server" ID="lblDsMontoBono" Enabled="false" Text="Monto del bono"></telerik:RadLabel>
                        </legend>
                        <div style="margin-left: 10px;">
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="rbPorcentaje" Text="%" AutoPostBack="false" ToggleType="Radio" Width="100px" GroupName="TipoBono">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="rbMonto" Text="$" AutoPostBack="false" ToggleType="Radio" Width="100px" GroupName="TipoBono">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadNumericTextBox ID="txtMontoBono" InputType="Number" Width="120px" runat="server" AutoPostBack="false" MinValue="0" NumberFormat-DecimalDigits="2">
                                    <EnabledStyle HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCalcularTodos" runat="server" Text="Aplicar a todos" OnClick="btnCalcularTodos_Click"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCalcularSeleccion" runat="server" Text="Aplicar individual" OnClick="btnCalcularSeleccion_Click"></telerik:RadButton>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="ctrlBasico">
                    <fieldset>
                        <legend>
                            <telerik:RadLabel runat="server" ID="RadLabel1" Enabled="false" Text="Tipo de bono"></telerik:RadLabel>
                        </legend>
                        <div style="margin-left: 10px;">
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="rbIndividualIndependiente" runat="server" ToggleType="Radio"
                                    GroupName="grbTipoBono" AutoPostBack="false" Text="Individual independiente" Enabled="false" ToolTip="Los integrantes de este grupo recibirán el bono correspondiente dependiendo de sus resultados individuales e independientemente del resultado que obtenga el grupo.">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="rbIndividualDependiente" runat="server" ToggleType="Radio"
                                    GroupName="grbTipoBono" AutoPostBack="false" Text="Individual dependiente" Enabled="false" ToolTip="Los integrantes de este grupo recibirán el bono correspondiente siempre y cuando el grupo obtenga el mínimo nivel de cumplimiento para recibir bono definido por la empresa. El resultado del bono de cada persona dependerá de los resultados individuales.">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="rbGrupal" runat="server" ToggleType="Radio"
                                    GroupName="grbTipoBono" AutoPostBack="false" Text="Grupal" Enabled="false" ToolTip="El grupo se ganará el bono dependiendo del resultado del grupo, sin excluir a ningún integrante independientemente de sus resultados individuales.">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="ctrlBasico">
                    <fieldset>
                        <legend>
                            <telerik:RadLabel runat="server" ID="rlPeriodo" Enabled="false" Text="Rango de período"></telerik:RadLabel>
                        </legend>
                        <div style="margin-left: 10px;">
                            <div class="ctrlBasico">
                                <telerik:RadTextBox ID="txtRangoPeriodo" runat="server" Width="170" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div style="height: calc(80% - 80px); width: 100%;">
                    <telerik:RadGrid runat="server" ID="rgBono" Height="100%" AutoGenerateColumns="false" OnNeedDataSource="rgBono_NeedDataSource" HeaderStyle-Font-Bold="true" EnableHeaderContextMenu="true" AllowSorting="true" AllowMultiRowSelection="true" OnItemDataBound="rgBono_ItemDataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_BONO_EVALUADO" AllowPaging="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="CL_EVALUADO" HeaderText="No. de empleado" DataField="CL_EVALUADO" FilterControlWidth="10" HeaderStyle-Font-Bold="true">
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_EVALUADO" HeaderText="Nombre completo" DataField="NB_EVALUADO" FilterControlWidth="60%" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_PUESTO" HeaderText="Puesto" DataField="NB_PUESTO" FilterControlWidth="140" HeaderStyle-Font-Bold="true">
                                    <ItemStyle Width="200px" />
                                    <HeaderStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_DEPARTAMENTO" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true">
                                    <ItemStyle Width="200px" />
                                    <HeaderStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="MN_SUELDO" HeaderText="Sueldo" DataField="MN_SUELDO" DataFormatString="{0:C2}" HeaderStyle-Font-Bold="true">
                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="MN_TOPE_BONO" HeaderText="Tope de bono" DataField="MN_TOPE_BONO" FilterControlWidth="60%" DataFormatString="{0:C2}" HeaderStyle-Font-Bold="true">
                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="NO_MONTO_BONO" HeaderText="Bono" DataField="NO_MONTO_BONO" FilterControlWidth="60%" HeaderStyle-Font-Bold="true">
                                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                                    <HeaderStyle Width="150px" />
                                    <ItemTemplate>
                                        <telerik:RadTextBox runat="server" ID="txtMontoBono" NumberFormat-DecimalDigits="2" InputType="Number" HeaderStyle-Font-Bold="true" Width="100%" EnabledStyle-HorizontalAlign="Right" Text='<%# Eval("NO_MONTO_BONO") %>'></telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                            </div>
                <div class="divControlesBoton">
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                    </div>
                </div>

                 </telerik:RadPane>
                    <telerik:RadPane ID="RadPane4" runat="server" Width="30">
                        <telerik:RadSlidingZone ID="RadSlidingZone3" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                            <telerik:RadSlidingPane ID="RadSlidingPane2" runat="server" Title="Ayuda" Width="300" MinWidth="500" Height="100%">
                                <div style="padding: 10px; text-align: justify;">
                                        Define el bono que en este período se aplicará a los evaluados. Dejalo en 0 si no deseas aplicar bono. 
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>

            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvContasenas" runat="server">
                <div style="height: calc(100% - 45px); padding-bottom: 10px;">
                    <telerik:RadGrid ID="grdContrasenaEvaluadores" runat="server" Height="100%" AllowMultiRowSelection="true" AutoGenerateColumns="false" AllowSorting="true"
                        OnNeedDataSource="grdContrasenaEvaluadores_NeedDataSource" HeaderStyle-Font-Bold="true" OnItemDataBound="grdContrasenaEvaluadores_ItemDataBound">
                        <ClientSettings EnableAlternatingItems="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EVALUADOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Evaluador" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Contraseña" DataField="CL_TOKEN" UniqueName="CL_TOKEN" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both;"></div>
                <label id="lbMensaje" runat="server" visible="false" style="color: red;">*El evaluador para este periodo es el Coordinador, no se pueden enviar solicitudes, el coordinador realizará la captura ingresando a través del botón capturar.</label>
                <div style="clear: both;"></div>
              <%--  <div class="ctrlBasico">
                    <telerik:RadButton ID="btnReasignarTodasContrasenas" runat="server" Text="Reasignar contraseñas a todos" OnClick="btnReasignarTodasContrasenas_Click"></telerik:RadButton>
                </div>--%>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnReasignarContrasena" runat="server" Text="Reasignar contraseña al evaluador seleccionado" OnClick="btnReasignarContrasena_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEnvioSolicitudes" runat="server" Text="Enviar evaluaciones" OnClientClicking="OpenEnvioSolicitudes"></telerik:RadButton>
                </div>
                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnGuardarCerrar" Text="Guardar y cerrar" OnClick="btnGuardarCerrar_Click"></telerik:RadButton>
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
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
