<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ConfiguracionPeriodo.aspx.cs" Inherits="SIGE.WebApp.FYD.ConfiguracionPeriodo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .RadListBox .rlbGroup {
            max-height: 300px;
            overflow: auto;
        }

        fieldset {
            padding-left: 10px;
            padding-right: 10px;
        }

        .AUTOEVALUACION {
            background-color: #CCFFFF !important;
            color: black;
        }

        .SUPERIOR {
            background-color: #CCFF66 !important;
            color: black;
        }

        .SUBORDINADO {
            background-color: #F5C9A6 !important;
            color: black;
        }

        .INTERRELACIONADO {
            background-color: whitesmoke !important;
            color: black;
        }

        .OTRO {
            background-color: #FCE981 !important;
            color: black;
        }

        .errBackground {
            background-color: red !important;
            color: black;
        }
    </style>

       

    <script type="text/javascript">

        //Variables prueba de texto Idioma
        var vOpenCompetenciasSelectionWindow = "Selección de competencias";
        var vOpenEmpleadosSelectionWindow = "Selección de evaluados";
        var vOpenPuestoSelectionWindow = "Selección de puestos";
        var vOpenAreaSelectionWindow = "Selección de áreas/departamento";
        var vOpenOtrosEvaluadoresManualWindow = "Selección otros evaluadores";
        var vOpenOtrosEvaluadoresSelectionWindow = "Selección de evaluadores";
        var vOpenCamposInterrelacionadosSelectionWindow = "Selección de campos para interrelacionados";
        var vOpenCamposAdicionalesSelectionWindow = "Selección de campos para preguntas adicionales";
        var vOpenAgregarCuestionarioSelectionWindow = "Agregar/Editar cuestionario";
        var vOpenAgregarPreguntasAdicionalesWindow = "Agregar pregunta abierta";
        var vOpenEditarPreguntasAdicionalesWindow = "Editar pregunta abierta";
        var vOpenEditarPreguntasAdicionalesWindow_alert = "Selecciona una pregunta.";
        var vConfirmarEliminarPregunta = "¿Estás seguro de eliminar esta pregunta?, este proceso no podrá revertirse. ";
        var vConfirmarEliminarPregunta_window = "Eliminar pregunta";
        var vOpenEnvioCuestionariosWindow = "Enviar evaluaciones";
        var vOpenOtrosPuestosSelectionWindow = "Selección de puestos contra los que se evaluará";
        var vOpenOtrosPuestosSelectionWindow_alert = "Selecciona uno o más evaluados.";
        var vOpenMatrizEvaluadoresWindow = "Cuestionarios para evaluación de competencias 90°, 180° o 360°";
        var vOpenMatrizEvaluadoresWindow_alert = "Selecciona uno o más evaluados.";
        var vOpenPlaneacionMatrizWindow = "Planeación de cuestionarios";
        var vconfirmarEliminarEvaluados_1a = "¿Deseas eliminar a ";
        var vconfirmarEliminarEvaluados_1b = " como evaluado?, este proceso no podrá revertirse";
        var vconfirmarEliminarEvaluados_2a = "¿Deseas eliminar a los ";
        var vconfirmarEliminarEvaluados_2b = " evaluadores seleccionados?, este proceso no podrá revertirse.";
        var vconfirmarEliminarEvaluados_alert = "Selecciona un evaluado.";
        var vDeleteListItems = "No seleccionado";
        var vChangeControlState = "No seleccionado";
        var vOpenWindowAutorizarDocumento = "Ver registro y Autorización";

        //Traducción si es necesaria al momento de cargar
        window.onload = texto;

        function texto() {
            if ('<%= vClIdioma %>' != "ES") {

                vOpenCompetenciasSelectionWindow = '<%= vOpenCompetenciasSelectionWindow %>';
                vOpenEmpleadosSelectionWindow = '<%= vOpenEmpleadosSelectionWindow %>';
                vOpenPuestoSelectionWindow = '<%= vOpenPuestoSelectionWindow%>';
                vOpenAreaSelectionWindow = '<%=vOpenAreaSelectionWindow %>';
                vOpenOtrosEvaluadoresManualWindow = '<%=vOpenOtrosEvaluadoresManualWindow %>';
                vOpenOtrosEvaluadoresSelectionWindow = '<%=vOpenOtrosEvaluadoresSelectionWindow %>';
                vOpenCamposInterrelacionadosSelectionWindow = '<%=vOpenCamposInterrelacionadosSelectionWindow %>';
                vOpenCamposAdicionalesSelectionWindow = '<%=vOpenCamposAdicionalesSelectionWindow %>';
                vOpenAgregarCuestionarioSelectionWindow = '<%=vOpenAgregarCuestionarioSelectionWindow %>';
                vOpenAgregarPreguntasAdicionalesWindow = '<%=vOpenAgregarPreguntasAdicionalesWindow %>';
                vOpenEditarPreguntasAdicionalesWindow = '<%= vOpenEditarPreguntasAdicionalesWindow%>';
                vOpenEditarPreguntasAdicionalesWindow_alert = '<%=vOpenEditarPreguntasAdicionalesWindow_alert %>';
                vConfirmarEliminarPregunta = '<%=vConfirmarEliminarPregunta %>';
                vConfirmarEliminarPregunta_window = '<%=vConfirmarEliminarPregunta_window %>';
                vOpenEnvioCuestionariosWindow = '<%=vOpenEnvioCuestionariosWindow %>';
                vOpenOtrosPuestosSelectionWindow = '<%= vOpenOtrosPuestosSelectionWindow%>';
                vOpenOtrosPuestosSelectionWindow_alert = '<%=vOpenOtrosPuestosSelectionWindow_alert %>';
                vOpenMatrizEvaluadoresWindow = '<%= vOpenMatrizEvaluadoresWindow%>';
                vOpenMatrizEvaluadoresWindow_alert = '<%=vOpenMatrizEvaluadoresWindow_alert %>';
                vOpenPlaneacionMatrizWindow = '<%=vOpenPlaneacionMatrizWindow %>';
                vconfirmarEliminarEvaluados_1a = '<%=vconfirmarEliminarEvaluados_1a %>';
                vconfirmarEliminarEvaluados_1b = '<%=vconfirmarEliminarEvaluados_1b %>';
                vconfirmarEliminarEvaluados_2a = '<%=vconfirmarEliminarEvaluados_2a %>';
                vconfirmarEliminarEvaluados_2b = '<%=vconfirmarEliminarEvaluados_2b %>';
                vconfirmarEliminarEvaluados_alert = '<%=vconfirmarEliminarEvaluados_alert %>';
                vDeleteListItems = '<%= vDeleteListItems%>';
                vChangeControlState = '<%=vChangeControlState %>';
                vOpenWindowAutorizarDocumento = '<%=vOpenWindowAutorizarDocumento %>';
            }
        }


        var cClTipoEvaluacionPorPuesto = 'PUESTOS';
        var cClTipoEvaluacionPorCompetencia = 'COMPETENCIAS';
        var cClTipoEvaluacionPorOtras = 'OTRAS';
        var cClTipoEvaluacionPlanVidaCarrera = "PLANVIDACARRERA";

        function closeWindow() {
            GetRadWindow().close();
        }

        function OpenCompetenciasSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionCompetencia.aspx", "winSeleccion", vOpenCompetenciasSelectionWindow);
        }

        function OpenEmpleadosSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "winSeleccion", vOpenEmpleadosSelectionWindow);
        }

        function OpenPuestoSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPuesto.aspx?mulSel=1", "winSeleccion", vOpenPuestoSelectionWindow);
        }

        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx", "winSeleccion", vOpenAreaSelectionWindow);
        }

        function OpenOtrosEvaluadoresManualWindow() {
            OpenSelectionWindow("VentanaEvaluador.aspx", "winSeleccion", vOpenOtrosEvaluadoresManualWindow);
        }

        function OpenOtrosEvaluadoresSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=OTROEVALUADOR&CLFILTRO=NINGUNO", "winSeleccion", vOpenOtrosEvaluadoresSelectionWindow);
        }

        function OpenCamposInterrelacionadosSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionCampoAdicional.aspx?CatalogoCl=INTERRELACIONADO&TipoFormularioCl=INVENTARIO&SistemaFg=0&mulSel=0", "winSeleccion", vOpenCamposInterrelacionadosSelectionWindow);
        }

        function OpenCamposAdicionalesSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionCampoAdicional.aspx?CatalogoCl=CUESTIONARIO&TipoFormularioCl=CUESTIONARIO&SistemaFg=0", "winSeleccion", vOpenCamposAdicionalesSelectionWindow);
        }

        function OpenAgregarCuestionarioSelectionWindow() {
            var windowProperties = {
                width: 500,
                height: 650
            };

            OpenSelectionWindow("AgregarCuestionario.aspx?m=FORMACION&PeriodoId=<%= vPeriodo.ID_PERIODO %>", "winAgregarCuestionario", vOpenAgregarCuestionarioSelectionWindow, windowProperties);
        }

        function OpenAgregarPreguntasAdicionalesWindow() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 800,
                height: browserWnd.innerHeight - 100
            };
            OpenSelectionWindow("VentanaPeriodoPreguntasAdicionales.aspx?IdPeriodo=<%= vPeriodo.ID_PERIODO %>", "winCamposAdicionales", vOpenAgregarPreguntasAdicionalesWindow, windowProperties);
        }


        function OpenEditarPreguntasAdicionalesWindow() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 800,
                height: browserWnd.innerHeight - 100
            };

            var masterTable = $find("<%= grdCamposAdicionales.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                var vPregunta = selectedItems[0];
                var vIdPregunta = vPregunta.getDataKeyValue("ID_PREGUNTA_ADICIONAL");
                OpenSelectionWindow("VentanaPeriodoPreguntasAdicionales.aspx?IdPeriodo=<%= vPeriodo.ID_PERIODO %>&IdPregunta=" + vIdPregunta, "winCamposAdicionales", vOpenEditarPreguntasAdicionalesWindow, windowProperties);
            } else {
                radalert(vOpenEditarPreguntasAdicionalesWindow_alert, 400, 150, "Aviso");
            }
        }

        function ConfirmarEliminarPregunta(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
            { if (shouldSubmit) { this.click(); } });

            radconfirm(vConfirmarEliminarPregunta, callBackFunction, 400, 170, null, "Aviso");
            args.set_cancel(true);
        }

        function OpenEnvioCuestionariosWindow() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 750,
                height: browserWnd.innerHeight - 40
            };

            OpenSelectionWindow("VentanaEnvioSolicitudes.aspx?IdPeriodo=<%= vPeriodo.ID_PERIODO %>", "winAgregarCuestionario", vOpenEnvioCuestionariosWindow, windowProperties);
        }

        function OpenOtrosPuestosSelectionWindow() {
            var masterTable = $find("<%= grdEvaluados.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                OpenSelectionWindow("../Comunes/SeleccionPuesto.aspx?m=FORMACION&CatalogoCl=OTROSPUESTOS", "winSeleccion", vOpenOtrosPuestosSelectionWindow)
            }
            else {
                radalert(vOpenOtrosPuestosSelectionWindow_alert, 400, 150, "Aviso");
            }
        }

        function OpenMatrizEvaluadoresWindow() {
            var masterTable = $find('<%= grdCuestionarios.ClientID %>').get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                var vEvaluado = selectedItems[0];
                var vIdEvaluado = vEvaluado.getDataKeyValue("ID_EVALUADO");
                OpenSelectionWindow("MatrizCuestionarios.aspx?PeriodoId=<%= vPeriodo.ID_PERIODO %>&EvaluadoId=" + vIdEvaluado, "winEdicionPorEvaluado", vOpenMatrizEvaluadoresWindow)
            } else {
                radalert(vOpenMatrizEvaluadoresWindow_alert, 400, 150, "Aviso");
            }
        }

        function OpenPlaneacionMatrizWindow() {

            var rtsTabs = $find('<%= rtsConfiguracionPeriodo.ClientID %>');
            var vSelectedTab = rtsTabs.get_selectedTab();

            var vValueTab = vSelectedTab.get_value();
            var vEstadoPeriodo = '<%# vPeriodo.CL_ESTADO %>';
            var vFgOtrosEvauadores = '<%# vPeriodo.FG_OTROS_PUESTOS %>';

            var vNoItemsCuestionarios = $find("<%= grdCuestionarios.ClientID %>").get_masterTableView().get_dataItems().length;

            <%--if (vValueTab == 4 && '<%= vFgInterrelacionado%>' == "True" && vNoItemsCuestionarios == 0) {
                radalert("Si deseas filtrar los interrelacionados por algún campo, seleccionalo en la pestaña antes de seleccionar los evaluados.", 400, 170, "Aviso");
            }--%>

            if (vValueTab == 6 & vEstadoPeriodo != 'CERRADO' & vNoItemsCuestionarios == 0) {
                OpenSelectionWindow("MatrizPlaneacionCuestionarios.aspx?IdPeriodo=<%= vPeriodo.ID_PERIODO %>", "winMatrizCuestionarios", vOpenPlaneacionMatrizWindow);
            }

            if (vValueTab == 7 & vNoItemsCuestionarios > 0) {
                $find("<%=grdContrasenaEvaluadores.ClientID%>").get_masterTableView().rebind();
            }
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function FindItemByValue(pList, pValue) {
            for (var i = 0; i < pList.length; i++)
                if (pList == pValue)
                    return i;
            return null;
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {
                    case "COMPETENCIA":
                        InsertCompetencias(pDato);
                        break;
                    case "EMPLEADO":
                        InsertEvaluado(EncapsularSeleccion("EVALUADO", pDato));
                        break;
                    case "PUESTO":
                        InsertEvaluado(EncapsularSeleccion("PUESTO", pDato));
                        break;
                    case "DEPARTAMENTO":
                        InsertEvaluado(EncapsularSeleccion("AREA", pDato));
                        break;
                    case "OTROEVALUADOR":
                        InsertEvaluado(EncapsularSeleccion("OTROEVALUADOR", pDato));
                        break;
                    case "INTERRELACIONADO":
                        InsertCamposInterrelacionados(pDato);
                        break
                    case "OTROSPUESTOS":
                        InsertOtrosPuestosEvaluacion(pDato);
                        break;
                    case "CUESTIONARIO":
                        InsertCamposAdicionales(pDato);
                        break;
                    case "REBIND":
                        InsertEvaluado(EncapsularSeleccion("REBIND", pDato));
                        break;
                    case "CAMPO_ADICIONAL":
                        RecargarCamposAdicionales();
                        break;
                    case "PLANEACION":
                        RecargarCuestionarios();
                        break;

                }
            }
        }

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertEvaluado(pDato) {
            var ajaxManager = $find('<%= ramConfiguracionPeriodo.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function InsertOtrosPuestosEvaluacion(pDato) {
            var vEmpleados = [];
            var masterTable = $find("<%= grdEvaluados.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vEmpleado = {
                        idEmpleado: selectedItem.getDataKeyValue("ID_EMPLEADO"),
                        clEmpleado: masterTable.getCellByColumnUniqueName(selectedItem, "CL_EMPLEADO").innerHTML,
                        nbCorreoElectronico: masterTable.getCellByColumnUniqueName(selectedItem, "CL_CORREO_ELECTRONICO").innerHTML,
                        nbEmpleado: masterTable.getCellByColumnUniqueName(selectedItem, "NB_EMPLEADO_COMPLETO").innerHTML,
                        clTipoCatalogo: "EMPLEADOS"
                    };
                    vEmpleados.push(vEmpleado);
                }
                var vSeleccion = {
                    lstPuestos: pDato,
                    lstEmpleados: vEmpleados
                };
                InsertEvaluado(EncapsularSeleccion("OTROSPUESTOS", vSeleccion));
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
                    vMensaje = vconfirmarEliminarEvaluados_1a + vNombre + vconfirmarEliminarEvaluados_2b;
                }
                else {
                    vNombre = selectedItems.length;
                    vMensaje = vconfirmarEliminarEvaluados_2a + vNombre + vconfirmarEliminarEvaluados_2b;
                }
                var vWindowsProperties = { height: 200 };

                confirmAction(sender, args, vMensaje, vWindowsProperties);
            }
            else {
                radalert(vconfirmarEliminarEvaluados_alert, 400, 150, "Aviso");
                args.set_cancel(true);
            }
        }

        function OrdenarSeleccion(pSeleccion, pNbListaDestino) {
            var vListBox = $find(pNbListaDestino);
            vListBox.trackChanges();

            var items = vListBox.get_items();

            for (var i = 0; i < items.get_count() ; i++) {
                var item = items.getItem(i);
                var itemValue = item.get_value();
                var itemText = item.get_text();
                if (itemValue != "0") {
                    var vFgItemEncontrado = false;
                    for (var j = 0; j < pSeleccion.length; j++)
                        vFgItemEncontrado = vFgItemEncontrado || (pSeleccion[j].idItem == itemValue);
                    if (!vFgItemEncontrado)
                        pSeleccion.push({
                            idItem: itemValue,
                            nbItem: itemText
                        });
                }
            }

            var arrOriginal = [];
            for (var i = 0; i < pSeleccion.length; i++)
                arrOriginal.push(pSeleccion[i].nbItem);

            var arrOrdenados = arrOriginal.slice();

            arrOrdenados.sort();

            var arrItemsOrdenados = [];

            for (var i = 0; i < arrOrdenados.length; i++)
                arrItemsOrdenados.push(pSeleccion[arrOriginal.indexOf(arrOrdenados[i])]);

            items.clear();

            for (var i = 0, len = arrItemsOrdenados.length; i < len; i++)
                ChangeListItem(arrItemsOrdenados[i].idItem, arrItemsOrdenados[i].nbItem, vListBox);

            vListBox.commitChanges();
        }

        function InsertCamposAdicionales(pDato) {
            var arrSeleccion = [];
            for (var i = 0; i < pDato.length; i++)
                arrSeleccion.push({
                    idItem: pDato[i].idDato,
                    nbItem: pDato[i].nbDato
                });
        }

        function InsertCompetencias(pDato) {
            var arrSeleccion = [];
            for (var i = 0; i < pDato.length; i++)
                arrSeleccion.push({
                    idItem: pDato[i].idCompetencia,
                    nbItem: pDato[i].nbCompetencia
                });

            OrdenarSeleccion(arrSeleccion, '<%=lstCompetenciasEspecificas.ClientID %>');
        }

        function InsertCamposInterrelacionados(pDato) {
            var arrSeleccion = [];
            for (var i = 0; i < pDato.length; i++)
                arrSeleccion.push({
                    idItem: pDato[i].idDato,
                    nbItem: pDato[i].nbDato
                });

            OrdenarSeleccion(arrSeleccion, '<%=lstCamposInterrelacionados.ClientID %>');

            var ajaxManager = $find('<%= ramConfiguracionPeriodo.ClientID%>');
            ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "INSERTARCAMPOINTERRRELACIONADO"}));
        }

        function InsertCampoAdicionalItem(pItem, pListBox) {
            ChangeListItem(pItem.idDato, pItem.nbDato, pListBox);
        }

        function InsertCompetenciaItem(pItem, pListBox) {
            ChangeListItem(pItem.idCompetencia, pItem.nbCompetencia, pListBox);
        }

        function ChangeListItem(pIdItem, pNbItem, pListBox) {
            var items = pListBox.get_items();
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(pNbItem);
            item.set_value(pIdItem);
            items.add(item);
            item.set_selected(true);
        }

        function DeleteListItems(pListBox) {
            var vSelectedItems = pListBox.get_selectedItems();

            pListBox.trackChanges();
            if (vSelectedItems)
                vSelectedItems.forEach(function (item) {
                    pListBox.get_items().remove(item);
                });

            if (pListBox.get_items().get_count() == 0) {
                ChangeListItem("0", vDeleteListItems, pListBox);
            }

            pListBox.commitChanges();
        }

        function DeleteCompetencia() {
            DeleteListItems($find("<%=lstCompetenciasEspecificas.ClientID %>"));
       }

       function DeleteCampoInterrelacionado() {
           DeleteListItems($find("<%=lstCamposInterrelacionados.ClientID %>"));
        }

        function ChangeControlState(pCtrlCheckbox, pFgEnabled, pClTipoControl) {
            if (pCtrlCheckbox) {

                switch (pClTipoControl) {
                    case "CHECKBOX":
                        pCtrlCheckbox.set_enabled(pFgEnabled);
                        if (!pFgEnabled)
                            pCtrlCheckbox.set_checked(pFgEnabled);
                        break;
                    case "LISTBOX":
                        if (!pFgEnabled) {
                            pCtrlCheckbox.trackChanges();

                            var items = pCtrlCheckbox.get_items();
                            items.clear();

                            var item = new Telerik.Web.UI.RadListBoxItem();
                            item.set_text(vChangeControlState);
                            item.set_value("0");
                            items.add(item);
                            item.set_selected(true);
                            pCtrlCheckbox.commitChanges();
                        }
                        break;
                    case "RADBUTTON":
                        if (pFgEnabled) {
                            pCtrlCheckbox.checked = true;
                            pCtrlCheckbox.set_checked(true);
                        }
                        else {
                            pCtrlCheckbox.checked = false;
                            pCtrlCheckbox.set_checked(false);
                        }
                        break;
                }

            }
        }

        function ChangeCheckState(pCtrlCheckbox, pFgEnabled) {
            ChangeControlState(pCtrlCheckbox, pFgEnabled, "CHECKBOX");
        }

        function ChangeListState(pCtrlCheckbox, pFgEnabled) {
            ChangeControlState(pCtrlCheckbox, pFgEnabled, "LISTBOX");
        }

        function ChangeButtonState(pCtrlCheckbox, pFgEnabled) {
            ChangeControlState(pCtrlCheckbox, pFgEnabled, "RADBUTTON");
        }

        function ChangeEnabledPorPuesto(pFgEnabled) {
            ChangeCheckState($find('<%= chkFgPuestoActual.ClientID %>'), pFgEnabled);
            ChangeCheckState($find('<%= chkFgOtrosPuestos.ClientID %>'), pFgEnabled);
        }

        function ChangeEnabledPorCompetencias(pFgEnabled) {
            ChangeCheckState($find('<%= chkFgCompetenciasGenericas.ClientID %>'), pFgEnabled);
            ChangeCheckState($find('<%= chkFgCompetenciasEspecificas.ClientID %>'), pFgEnabled);
            ChangeCheckState($find('<%= chkFgCompetenciasInstitucionales.ClientID %>'), pFgEnabled);

        }

        function ChangeEnabledPorOtrasCompetencias(pFgEnabled) {
            ChangeListState($find('<%= lstCompetenciasEspecificas.ClientID %>'), pFgEnabled);
            ChangeCheckState($find('<%= btnEspecificarOtrasCompetencias.ClientID %>'), pFgEnabled);
            ChangeCheckState($find('<%= btnEliminarCompentenciaEspecifica.ClientID %>'), pFgEnabled);
        }

        function ChangeEnablePlanVidaCarrera(pFgEnabled) {
            ChangeButtonState($find('<%= btnPlanVidaCarrera.ClientID %>'), pFgEnabled);
            ChangeCheckState($find('<%= chkFgRutaVertical.ClientID %>'), pFgEnabled);
            ChangeCheckState($find('<%= chkFgRutaVerticalAlternativa.ClientID %>'), pFgEnabled);
            ChangeCheckState($find('<%= chkFgRutaHorizontalAlternativa.ClientID %>'), pFgEnabled);
        }

        function EvaluacionSettings(pClTipoEvaluacion) {
            ChangeEnabledPorPuesto(pClTipoEvaluacion == cClTipoEvaluacionPorPuesto);
            ChangeEnabledPorCompetencias(pClTipoEvaluacion == cClTipoEvaluacionPorCompetencia);
            ChangeEnabledPorOtrasCompetencias(pClTipoEvaluacion == cClTipoEvaluacionPorOtras);
            ChangeEnablePlanVidaCarrera(pClTipoEvaluacion == cClTipoEvaluacionPorPuesto);
        }

        function EvaluacionCarreraSettings(pClTipoEvaluacion) {
            ChangeEnablePlanVidaCarrera(pClTipoEvaluacion == cClTipoEvaluacionPorPuesto);
        }

        function EvaluacionPlanVidaCarreraSettings(pClTipoEvaluacion) {
            ChangeEnabledPorCompetencias(pClTipoEvaluacion == cClTipoEvaluacionPorCompetencia);
            ChangeEnabledPorOtrasCompetencias(pClTipoEvaluacion == cClTipoEvaluacionPorOtras);
            ChangeEnablePlanVidaCarrera(pClTipoEvaluacion == cClTipoEvaluacionPlanVidaCarrera);
        }

        function EvaluacionPuestoSettings(pClTipoEvaluacion) {
            ChangeEnabledPorPuesto(pClTipoEvaluacion == cClTipoEvaluacionPorPuesto);
            ChangeEnabledPorCompetencias(pClTipoEvaluacion == cClTipoEvaluacionPorCompetencia);
            ChangeEnabledPorOtrasCompetencias(pClTipoEvaluacion == cClTipoEvaluacionPorOtras);
        }

        function SeleccionarPorPuesto() {
            EvaluacionPuestoSettings(cClTipoEvaluacionPorPuesto);
        }

        function SeleccionarPorCompetencias() {
            EvaluacionSettings(cClTipoEvaluacionPorCompetencia);
            EvaluacionCarreraSettings(cClTipoEvaluacionPorCompetencia);
        }

        function SeleccionarPorOtras() {
            EvaluacionSettings(cClTipoEvaluacionPorOtras);
            EvaluacionCarreraSettings(cClTipoEvaluacionPorOtras);
        }

        function SeleccionarPlanVidaCarrera() {
            EvaluacionPlanVidaCarreraSettings(cClTipoEvaluacionPlanVidaCarrera);
            ChangeButtonState($find('<%= btnEvaluacionPorEstandar.ClientID %>'), false);
            ChangeButtonState($find('<%= btnEvaluacionPorOtras.ClientID %>'), false);
        }

        function CheckPonderacionEvaluadores(sender, args) {
            var vPrAutoevaluacion = $find('<%= txtPrAutoevaluacion.ClientID %>').get_value();
            var vPrSuperior = $find('<%= txtPrSuperior.ClientID %>').get_value();
            var vPrSubordinado = $find('<%= txtPrSubordinados.ClientID %>').get_value();
            var vPrInterrelacionado = $find('<%= txtPrInterrelacionados.ClientID %>').get_value();
            var vPrOtrosEvaluadores = $find('<%= txtPrOtros.ClientID %>').get_value();
            var vPrTotal = vPrAutoevaluacion + vPrSuperior + vPrSubordinado + vPrInterrelacionado + vPrOtrosEvaluadores;
            ChangeCssError($find('<%= txtPrTotal.ClientID %>'), vPrTotal);
        }

        function CheckPonderacionCompetencias() {
            var vPrGenericas = $find('<%= txtPrGenericas.ClientID %>').get_value();
            var vPrEspecificas = $find('<%= txtPrEspecificas.ClientID %>').get_value();
            var vPrInstitucionales = $find('<%= txtPrInstitucionales.ClientID %>').get_value();
            var vPrTotal = vPrGenericas + vPrEspecificas + vPrInstitucionales;
            ChangeCssError($find('<%= txtPrTotalCompetencias.ClientID %>'), vPrTotal);
        }

        function ChangeCssError(pCtrlPrTotal, pPrTotal) {

            pCtrlPrTotal.set_value(pPrTotal);
            pCtrlPrTotal.get_styles().DisabledStyle[1] = pCtrlPrTotal.get_styles().DisabledStyle[1].replace("errBackground", "");

            if (pPrTotal != 100)
                pCtrlPrTotal.get_styles().DisabledStyle[1] += " errBackground";
            pCtrlPrTotal.updateCssClass();
        }

        function ShowAutorizarForm() {
            var idPeriodo = '<%# vPeriodo.ID_PERIODO %>';
            OpenWindowAutorizarDocumento(idPeriodo);
        }

        function OpenWindowAutorizarDocumento(pIdPeriodo) {
            //var vFgContestados = '<= vFgContestados %>';
            if (pIdPeriodo != null) {
                var vURL = "VentanaDocumentoAutorizar.aspx";
                vURL = vURL + "?IdPeriodo=" + pIdPeriodo + "&FGCONTESTADO=SI";
                vTitulo = vOpenWindowAutorizarDocumento;
                vTipoOperacion = "&TIPO=Agregar";
            }

            //var oWin = window.radopen(vURL + vTipoOperacion, "winAutoriza");
            //oWin.set_title(vTitulo);

            OpenSelectionWindow(vURL, "winAutoriza", vTitulo);
        }


        function RecargarCamposAdicionales() {
            $find("<%=grdCamposAdicionales.ClientID%>").get_masterTableView().rebind();
        }

        function RecargarCuestionarios() {
            $find("<%=grdCuestionarios.ClientID%>").get_masterTableView().rebind();
        }

        function enableCtrlPonderarCompetencias(sender, args) {
            $get("<%=divPonderarCompetencias.ClientID %>").style.display = sender.get_checked() ? "block" : "none";
            var vGenericas = 0;
            var vEspecificas = 0;
            var vInstitucionales = 0;
            var vPrGenericas = $find('<%= txtPrGenericas.ClientID %>');
            var vPrEspecificas = $find('<%= txtPrEspecificas.ClientID %>');
            var vPrInstitucionales = $find('<%= txtPrInstitucionales.ClientID %>');

            if ($get("<%=divPonderarCompetencias.ClientID %>").style.display = sender.get_checked()) {
                if (vPrGenericas._enabled == true)
                    vGenericas = 1;

                if (vPrEspecificas._enabled == true)
                    vEspecificas = 1;

                if (vPrInstitucionales._enabled == true)
                    vInstitucionales = 1;

                var vHabilitados = vGenericas + vEspecificas + vInstitucionales;
                var vPorcentaje = 100 / vHabilitados;

                if (vPorcentaje == 33.333333333333336) {
                    if (vPrGenericas._enabled == true)
                        vPrGenericas.set_value(33.34);
                } else {
                    vPrGenericas.set_value(vPorcentaje);
                }

                if (vPrEspecificas._enabled == true)
                    vPrEspecificas.set_value(vPorcentaje);

                if (vPrInstitucionales._enabled == true)
                    vPrInstitucionales.set_value(vPorcentaje);
            }
            else {
                vPrGenericas.set_value(vGenericas);
                vPrEspecificas.set_value(vEspecificas);
                vPrInstitucionales.set_value(vInstitucionales);
            }
        }

        function enableCtrlPrEvaluadores(sender, args) {
            if (document.getElementById('chkFgPonderarEvaluadoresAuto').checked)
                document.getElementById("chkFgPonderarEvaluadores").disabled = true;
            else
                document.getElementById("chkFgPonderarEvaluadores").disabled = true;
        }


        function enableCtrlPonderarEvaluadores(sender, args) {
            $get("<%=divPonderarEvaluadores.ClientID %>").style.display = sender.get_checked() ? "block" : "none";
            var vAutoevaluacion = 0;
            var vSuperior = 0;
            var vSubordinado = 0;
            var vInterrelacionado = 0;
            var vOtros = 0;

            var vPrAutoevaluacion = $find('<%= txtPrAutoevaluacion.ClientID%>');
            var vPrSuperior = $find('<%= txtPrSuperior.ClientID%>');
            var vPrSubordinado = $find('<%= txtPrSubordinados.ClientID%>');
            var vPrInterrelacionado = $find('<%= txtPrInterrelacionados.ClientID%>');
            var vPrOtros = $find('<%= txtPrOtros.ClientID%>');

            // if ($get("<=divPonderarEvaluadores.ClientID %>").style.display = sender.get_checked()) {

            //if (vPrAutoevaluacion._enabled == true)
            //    vAutoevaluacion = 1;

            //if (vPrSuperior._enabled == true)
            //    vSuperior = 1;

            //if (vPrSubordinado._enabled == true)
            //    vSubordinado = 1;

            //if (vPrInterrelacionado._enabled == true)
            //    vInterrelacionado = 1;

            //if (vPrOtros._enabled == true)
            //    vOtros = 1;

            //    var vHabilitados = vAutoevaluacion + vSuperior + vSubordinado + vInterrelacionado + vOtros;
            //    var vPorcentaje = 100 / vHabilitados;

            //    if (vPrAutoevaluacion._enabled == true)
            //        vPrAutoevaluacion.set_value(vPorcentaje);

            //    if (vPrSuperior._enabled == true)
            //        vPrSuperior.set_value(vPorcentaje);

            //    if (vPrSubordinado._enabled == true)
            //        vPrSubordinado.set_value(vPorcentaje);

            //    if (vPrInterrelacionado._enabled == true)
            //        vPrInterrelacionado.set_value(vPorcentaje);

            //    if (vPrOtros._enabled == true)
            //        vPrOtros.set_value(vPorcentaje);
            //} else {
            //    vPrAutoevaluacion.set_value(vAutoevaluacion);
            //    vPrSuperior.set_value(vSuperior);
            //    vPrSubordinado.set_value(vSubordinado);
            //    vPrInterrelacionado.set_value(vInterrelacionado);
            //    vPrOtros.set_value(vOtros);
            //}
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpConfiguracionPeriodo" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConfiguracionPeriodo" runat="server" OnAjaxRequest="ramConfiguracionPeriodo_AjaxRequest" DefaultLoadingPanelID="ralpConfiguracionPeriodo">
        <AjaxSettings>
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
            <telerik:AjaxSetting AjaxControlID="grdEvaluados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCuestionarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtDsMensajeInicial" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarCampoAdicional" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdCamposAdicionales" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnPlaneacionMatriz" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnMostrarMatriz" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnRegistroAutorizacion" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarCuestionarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCrearCuestionarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtDsMensajeInicial" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarCampoAdicional" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdCamposAdicionales" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdContrasenaEvaluadores" UpdatePanelHeight="100%" />                   
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramConfiguracionPeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluadoresExternos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEvaluacionPorPuesto" UpdatePanelHeight="100%" />
                     <telerik:AjaxUpdatedControl ControlID="btnPlanVidaCarrera" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgPuestoActual" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgOtrosPuestos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgRutaVertical" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgRutaVerticalAlternativa" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgRutaHorizontalAlternativa" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarPuestos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEvaluacionPorEstandar" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgCompetenciasGenericas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgCompetenciasEspecificas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgCompetenciasInstitucionales" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEvaluacionPorOtras" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEspecificarOtrasCompetencias" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarCompentenciaEspecifica" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarEvaluado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEvaluacionPorPuesto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgPuestoActual" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgOtrosPuestos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgRutaVertical" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgRutaVerticalAlternativa" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgRutaHorizontalAlternativa" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarPuestos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEvaluacionPorEstandar" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgCompetenciasGenericas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgCompetenciasEspecificas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="chkFgCompetenciasInstitucionales" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEvaluacionPorOtras" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEspecificarOtrasCompetencias" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarCompentenciaEspecifica" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarOtroEvaluador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluadoresExternos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtNbEvaluadorExterno" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtNbEvaluadorExternoPuesto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtClCorreoElectronico" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEvaluadoresExternos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluadoresExternos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEvaluadoresExternos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluadoresExternos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarConfiguracion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluadoresExternos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarPuestos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="divPonderacionCompetencias" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarConfiguracionCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluadoresExternos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCampoAdicional">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lstCampoAdicional" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdCamposAdicionales" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCamposAdicionales" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarPonderacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPrAutoevaluacion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrSuperior" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrSubordinados" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrInterrelacionados" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrOtros" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrTotal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrGenericas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrEspecificas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrInstitucionales" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrTotalCompetencias" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarPonderacionCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPrAutoevaluacion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrSuperior" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrSubordinados" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrInterrelacionados" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrOtros" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrTotal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrGenericas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrEspecificas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrInstitucionales" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPrTotalCompetencias" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

 <%--   <div style="padding-top: 10px;">--%>
        <%-- <div class="ctrlBasico">
            <label name="lblIdPeriodo">Periodo:</label>
            <telerik:RadTextBox ID="txtIdPeriodo" runat="server" Enabled="false" Width="50"></telerik:RadTextBox>
        </div>
        <div class="ctrlBasico">
            <telerik:RadTextBox ID="txtNbPeriodo" runat="server" Enabled="false" Width="500"></telerik:RadTextBox>
        </div>--%>
<%--        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Período:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtIdPeriodo" runat="server" style="min-width: 100px;"></div>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <div id="txtNbPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                    </td>
                </tr>
            </table>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton ID="chkFgEvaluadorAutoevaluacion" runat="server" ToggleType="CheckBox" AutoPostBack="false" ReadOnly="true" Text="Autoevaluación">
                <ToggleStates>
                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="chkFgEvaluadorSupervisor" runat="server" ToggleType="CheckBox" AutoPostBack="false" ReadOnly="true" Text="Superior">
                <ToggleStates>
                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="chkFgEvaluadorSubordinados" runat="server" ToggleType="CheckBox" AutoPostBack="false" ReadOnly="true" Text="Subordinados">
                <ToggleStates>
                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="chkFgEvaluadorInterrelacionados" runat="server" ToggleType="CheckBox" AutoPostBack="false" Checked="true" ReadOnly="true" Text="Interrelacionados">
                <ToggleStates>
                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="chkFgEvaluadorOtros" runat="server" ToggleType="CheckBox" AutoPostBack="false" ReadOnly="true" Text="Otros">
                <ToggleStates>
                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>--%>

    <div style="clear: both;"><span id="InsertEvaluados" runat="server"></span></div>
    <telerik:RadTabStrip ID="rtsConfiguracionPeriodo" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracionPeriodo" OnClientTabSelected="OpenPlaneacionMatrizWindow">
        <Tabs>
             <telerik:RadTab Text="Contexto" Value="0"></telerik:RadTab>
            <telerik:RadTab Text="Tipo de evaluación" Value="1"></telerik:RadTab>
            <telerik:RadTab Text="Mensaje inicial y Preguntas abiertas" Value="2"></telerik:RadTab>
            <telerik:RadTab Text="Ponderación" Value="3"></telerik:RadTab>
            <telerik:RadTab Text="Selección de evaluados" Value="4"></telerik:RadTab>
            <telerik:RadTab Text="Otros evaluadores" Value="5"></telerik:RadTab>
            <telerik:RadTab Text="Matriz de evaluación" Value="6"></telerik:RadTab>
            <telerik:RadTab Text="Contraseñas" Value="7"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 90px); padding-top: 10px;">
        <telerik:RadMultiPage ID="rmpConfiguracionPeriodo" runat="server" SelectedIndexSelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
            <div style="clear: both; height: 10px;"></div>
                                <div class="ctrlBasico">
                                    <table class="ctrlTableForm" text-align: left;>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbPeriodo" name="lbTabulador" runat="server">Período:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtClPeriodo" runat="server"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbNbPeriodo" name="lbTabulador" runat="server">Descripción:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtDsPeriodo" runat="server"></div>
                                            </td>
                                        </tr>
                                                 <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbEstatus" name="lbTabulador" runat="server">Estatus:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtEstatus" runat="server"></div>
                                            </td>
                                        </tr>
                                                      <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbClTipoEval" name="lbTabulador" runat="server">Tipo de evaluación:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtTipoEvaluacion" runat="server"></div>
                                            </td>
                                        </tr>
                                             <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lblDsNotas" name="lbTabulador" runat="server">Notas:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtNotas" runat="server"></div>
                                            </td>
                                        </tr>
                                        </table>
                                    </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvTipoEvaluacion" runat="server">
                <div class="ctrlBasico" style="height:50px;">
                    <fieldset>
                        <legend>
                            <telerik:RadButton ID="btnEvaluacionPorPuesto" runat="server" ToggleType="Radio" GroupName="grpTipoEvaluacion" OnClientClicked="SeleccionarPorPuesto" AutoPostBack="false" Text="Evaluación por puesto">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </legend>
                        <table class="ctrlTableForm">
<%--                            <tr>
                                <td colspan="2">
                                    <label id="lblPorPuesto" name="lblPorPuesto" runat="server">Por puesto</label></td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgPuestoActual" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando las competencias que requiere su puesto actual (genéricas, específicas e institucionales). Los cuestionarios de evaluación se construirán con base en estas.">
                                        <%--<ToggleStates>
                                            <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgPuestoActual" name="lblFgPuestoActual" runat="server">Puesto actual</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgOtrosPuestos" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando las competencias de los puestos que a continuación selecciones. Los cuestionarios de evaluación se construirán con base en los puestos seleccionados. ">
                                        <%--<ToggleStates>
                                            <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgOtrosPuestos" name="lblFgOtrosPuestos" runat="server">Otros puestos</label></td>
                            </tr>
              <%--              <tr>
                                <td colspan="2">
                                    <label id="lblPlanVidaCarrera" name="lblPlanVidaCarrera" runat="server">Para plan de vida y carrera</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgRutaVertical" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando la ruta vertical natural determinada por el organigrama del puesto que ocupa(n). Los cuestionarios de evaluación se construirán con base en estas.">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgRutaVertical" name="lblFgRutaVertical" runat="server">Ruta vertical</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgRutaVerticalAlternativa" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando la ruta vertical alternativa establecida en el descriptivo del puesto que tiene cada uno de los participantes. Los cuestionarios de evaluación se construirán con base en esto.">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgRutaVerticalAlternativa" name="lblFgRutaVerticalAlternativa" runat="server">Ruta vertical alternativa</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgRutaHorizontalAlternativa" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando la ruta horizontal alternativa establecida en el descriptivo del puesto que tiene cada uno de los participantes. Los cuestionarios de evaluación se construirán con base en esto.">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgRutaHorizontalAlternativa" name="lblFgRutaHorizontalAlternativa" runat="server">Ruta horizontal alternativa</label></td>
                            </tr>--%>
                        </table>
                    </fieldset>
                </div>
                <div class="ctrlBasico">
                    <fieldset>
                        <legend>
                            <telerik:RadButton ID="btnPlanVidaCarrera" runat="server"  ToggleType="Radio"  AutoPostBack="false" OnClientClicked="SeleccionarPlanVidaCarrera" Text="Evaluación para el plan de vida y carrera">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </legend>
                        <table class="ctrlTableForm">
                    <%--                <tr>
                                <td colspan="2">
                                    <label id="lblPlanVidaCarrera" name="lblPlanVidaCarrera" runat="server">Para plan de vida y carrera</label></td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgRutaVertical" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando la ruta vertical natural determinada por el organigrama del puesto que ocupa(n). Los cuestionarios de evaluación se construirán con base en estas.">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgRutaVertical" name="lblFgRutaVertical" runat="server">Ruta vertical</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgRutaVerticalAlternativa" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando la ruta vertical alternativa establecida en el descriptivo del puesto que tiene cada uno de los participantes. Los cuestionarios de evaluación se construirán con base en esto.">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgRutaVerticalAlternativa" name="lblFgRutaVerticalAlternativa" runat="server">Ruta vertical alternativa</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgRutaHorizontalAlternativa" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando la ruta horizontal alternativa establecida en el descriptivo del puesto que tiene cada uno de los participantes. Los cuestionarios de evaluación se construirán con base en esto.">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgRutaHorizontalAlternativa" name="lblFgRutaHorizontalAlternativa" runat="server">Ruta horizontal alternativa</label></td>
                            </tr>
                        </table>
                        </fieldset>
                       </div>
                <div class="ctrlBasico">
                    <fieldset>
                        <legend>
                            <telerik:RadButton ID="btnEvaluacionPorEstandar" runat="server" ToggleType="Radio" OnClientClicked="SeleccionarPorCompetencias" GroupName="grpTipoEvaluacion" AutoPostBack="false" Text="Evaluación por competencias">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </legend>
                        <table class="ctrlTableForm">
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgCompetenciasGenericas" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando las competencias genéricas. Los cuestionarios de evaluación se construirán con base en esto.">
                                        <%--<ToggleStates>
                                            <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgGenericas" name="lblFgGenericas" runat="server">Genéricas</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgCompetenciasEspecificas" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando las competencias del puesto que actualmente ocupa(n). Los cuestionarios de evaluación se construirán con base en esto.">
                                        <%--<ToggleStates>
                                            <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblEspecificas" name="lblEspecificas" runat="server">Específicas</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="chkFgCompetenciasInstitucionales" RenderMode="Lightweight" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Width="30" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando las competencias institucionales. Los cuestionarios de evaluación se construirán con base en esto.">
                                        <%--<ToggleStates>
                                            <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <label id="lblFgInstitucionales" name="lblFgInstitucionales" runat="server">Institucionales</label></td>
                            </tr>
                        </table>

                         <telerik:RadButton ID="btnEvaluacionPorOtras" runat="server" ToggleType="Radio" OnClientClicked="SeleccionarPorOtras" GroupName="grpTipoEvaluacion" AutoPostBack="false" Text="Otras competencias" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando las competencias que hayas elegido, estas competencias aplicarán a todos los participantes. Los cuestionarios de evaluación se construirán con base en esto.">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                       
                        <div class="ctrlBasico" style="padding-top: 10px;">
                            <table class="ctrlTableForm">
                                <tr>
                                    <td>
                                        <telerik:RadListBox ID="lstCompetenciasEspecificas" runat="server" Width="300" SelectionMode="Multiple">
                                            <Items>
                                               <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                                            </Items>
                                        </telerik:RadListBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnEspecificarOtrasCompetencias" CssClass="customizeIcon" Text="+" runat="server" AutoPostBack="false" OnClientClicked="OpenCompetenciasSelectionWindow">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnEliminarCompentenciaEspecifica" runat="server" Text="x" AutoPostBack="false" OnClientClicked="DeleteCompetencia">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </div>
                <%--                <div class="ctrlBasico">
                  <fieldset>
                        <legend>
                            <telerik:RadButton ID="btnEvaluacionPorOtras" runat="server" ToggleType="Radio" OnClientClicked="SeleccionarPorOtras" GroupName="grpTipoEvaluacion" AutoPostBack="false" Text="Evaluación de otras competencias" ToolTip="Si eliges esta opción la evaluación de la(s) persona(s) seleccionada(s) se realizará considerando las competencias que hayas elegido, estas competencias aplicarán a todos los participantes. Los cuestionarios de evaluación se construirán con base en esto.">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </legend>
                        <div class="ctrlBasico" style="padding-top: 10px;">
                            <table class="ctrlTableForm">
                                <tr>
                                    <td>
                                        <telerik:RadListBox ID="lstCompetenciasEspecificas" runat="server" Width="300" SelectionMode="Multiple">
                                            <Items>
                                               <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                                            </Items>
                                        </telerik:RadListBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnEspecificarOtrasCompetencias" CssClass="customizeIcon" Text="+" runat="server" AutoPostBack="false" OnClientClicked="OpenCompetenciasSelectionWindow">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnEliminarCompentenciaEspecifica" runat="server" Text="x" AutoPostBack="false" OnClientClicked="DeleteCompetencia">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>--%>
                <%--        <div class="ctrlBasico" style="display: inline-block;" id="divConfiguracionInterrelacionados" runat="server">
                    <fieldset style="padding-left: 10px; vertical-align: middle;">
                        <legend id="lgInterrelacionado" runat="server">Interrelacionados</legend>
                        <div class="ctrlBasico">
                            <label id="lblLstCamposInterrelacionados" name="lblLstCamposInterrelacionados" runat="server">Campo en común:</label><br />
                            <table class="ctrlTableForm">
                                <tr>
                                    <td>
                                        <telerik:RadListBox ID="lstCamposInterrelacionados" runat="server" Width="300" SelectionMode="Multiple">
                                            <Items>
                                                <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                                            </Items>
                                        </telerik:RadListBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnEspecificarCamposRelacionados" runat="server" OnClientClicked="OpenCamposInterrelacionadosSelectionWindow" AutoPostBack="false" Text="+">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnEliminarCamposRelacionados" runat="server" OnClientClicked="DeleteCampoInterrelacionado" AutoPostBack="false" Text="x"></telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>--%>
                <div style="height: 10px; clear: both;"></div>
                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarConfiguracion" runat="server" Text="Guardar" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
                    </div>
                    <%--<div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarConfiguracionCerrar" runat="server" Text="Guardar y cerrar" OnClick="btnGuardarConfiguracionCerrar_Click"></telerik:RadButton>
                    </div>--%>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvMensajeInicial" runat="server">
                <div>
                    <label id="lblNbMensajeInicial" name="lblNbMensajeInicial" runat="server">Mensaje inicial</label>
                </div>
                <div style="padding-left: 50px;">
                    <label name="lblDsMensajeInicial" id="lblDsMensajeInicial" runat="server">Introduce aquí el texto que deseas que aparezca como mensaje inicial para los evaluadores, el texto {PERSONA_QUE_EVALUA} reemplazará el nombre de cada uno de los evaluadores.</label>
                    <%--<label name="lblDsMensajeInicial">Introduce aquí el texto que deseas que aparezca como mensaje inicial para los evaluadores, para remplazar el nombre del evaluador utiliza el texto {PERSONA_QUE_EVALUA}</label>--%>
                    <telerik:RadEditor Height="100" ToolsWidth="500" EditModes="Design" ID="txtDsMensajeInicial" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                </div>
                <div style="height: 20px;">
                </div>
                <%--  <div>
                    <label name="lblLsComentarios">Comentarios</label>
                </div>
                <div style="padding-left: 50px; vertical-align: bottom;">
                    <div style="display: inline-block; vertical-align: middle;">
                        <div class="ctrlBasico">
                            <table class="ctrlTableForm">
                                <tr>
                                    <td>
                                        <label name="lblClCampoAdicional">Campo:</label>
                                    </td>
                                    <td>
                                        <telerik:RadListBox ID="lstCampoAdicional" Width="300" runat="server">
                                            <Items>
                                                <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                                            </Items>
                                        </telerik:RadListBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnBuscarCampoAdicional" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenCamposAdicionalesSelectionWindow"></telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="display: inline-block; vertical-align: middle;">
                        <div class="ctrlBasico">
                            <fieldset style="padding-left: 10px;">
                                <legend name="lblGrupoCuestionario">Cuestionario </legend>
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
                    <div style="display: inline-block; vertical-align: middle;">
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnAgregarCampoAdicional" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenAgregarPreguntasAdicionalesWindow" OnClick="btnAgregarCampoAdicional_Click"></telerik:RadButton>
                        </div>
                    </div>
                </div>--%>
                <div style="clear: both;"></div>
                <div style="height: calc(100% - 190px);">
                    <telerik:RadGrid ID="grdCamposAdicionales" runat="server" Height="100%"
                        AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" AllowMultiRowSelection="true"
                        OnNeedDataSource="grdCamposAdicionales_NeedDataSource"
                        OnItemDataBound="grdCamposAdicionales_ItemDataBound" OnPreRender="grdCamposAdicionales_PreRender">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView DataKeyNames="ID_PREGUNTA_ADICIONAL,CL_CUESTIONARIO_OBJETIVO" ClientDataKeyNames="ID_PREGUNTA_ADICIONAL" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText="Preguntas abiertas" DataField="NB_PREGUNTA" UniqueName="NB_PREGUNTA">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="150" HeaderText="Cuestionario" DataField="NB_CUESTIONARIO_OBJETIVO" UniqueName="NB_CUESTIONARIO_OBJETIVO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_PREGUNTA" ConfirmTextFormatString="¿Desea eliminar la pregunta {0}?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />--%>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarCampoAdicional" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenAgregarPreguntasAdicionalesWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEditar" runat="server" Text="Editar" AutoPostBack="false" OnClientClicked="OpenEditarPreguntasAdicionalesWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminarPregunta" OnClick="btnEliminar_Click"></telerik:RadButton>
                </div>

                  <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
                    </div>
                </div>

            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvPonderacion" runat="server">
                <div style="height: calc(100% - 20px); width:100%;">
                    <div class="ctrlBasico" style="height: 100%; width:45%;">
                        <div style="height:80px;">
                            <label id="lblNbMensajeEval" name="lblNbMensajeEval" style="text-align:justify;" runat="server">SIGEIN tiene una fórmula que pondera equitativamente la opinión de los evaluadores. Si deseas modificarla haz click en personalizar ponderación. </label>
                        </div>
                        <div style="height: 5px; clear: both;"></div>
                        <div style="height: calc(100%-120px);">
                            <fieldset>
                                <legend id="lgPorEval" runat="server">Por evaluador</legend>
                                <div class="ctrlBasico">
                                    <telerik:RadButton RenderMode="Lightweight" ID="chkFgPonderarEvaluadoresAuto" runat="server" ToggleType="Radio" ButtonType="ToggleButton" Text="Ponderación estandar" AutoPostBack="false" GroupName="btnEvaluadores">
                                    </telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton RenderMode="Lightweight" Enabled="false" ID="chkFgPonderarEvaluadores" runat="server" ToggleType="Radio" ButtonType="ToggleButton" Text="Personalizar ponderación" GroupName="btnEvaluadores" OnClientCheckedChanged="enableCtrlPonderarEvaluadores"
                                        AutoPostBack="false">
                                    </telerik:RadButton>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div style="display: none;" id="divPonderarEvaluadores" runat="server">

                                        <table class="ctrlTableForm">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label id="lblNoAutoevaluacion" name="lblNoAutoevaluacion" runat="server">Autoevaluación:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrAutoevaluacion" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label id="lblNoSuperior" name="lblNoSuperior" runat="server">Jefe inmediato:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrSuperior" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label name="lblNoSubordinados" id="lblNoSubordinados" runat="server">Subordinados:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrSubordinados" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label name="lblNoInterrelacionados" id="lblNoInterrelacionados" runat="server">Interrelacionados:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrInterrelacionados" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label name="lblNoOtros" id="lblNoOtros" runat="server">Otros:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrOtros" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label name="lblNoTotal" id="lblNoTotal" runat="server">Total:</label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrTotal" runat="server" Enabled="false" Width="60" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    
                    <div class="ctrlBasico" style="height: 100%; width: 45%;" >
                        <div style="display: inline-block;" id="divPonderacionCompetencias" runat="server">
                            <div style="height:80px;">
                                <label name="lblNbMensajeComp" id="lblNbMensajeComp" style="text-align:justify;" runat="server">SIGEIN tiene una fórmula que pondera equitativamente las competencias que has seleccionado para realizar el proceso de evaluación. Si deseas modificarla haz click en personalizar ponderación. Nota: esta función esta disponible únicamente para algunas de las opciones de evaluación.</label>
                            </div> 
                            <div style="height: 5px; clear: both;"></div>
                            <div class="ctrlBasico" style="height: calc(100%-60px); width:100%;">
                                <fieldset>
                                <legend id="lgPorCompetencia" runat="server">Por competencia</legend>
                                <div class="ctrlBasico" style="display: inline-block;" id="div1" runat="server">
                                    <telerik:RadButton RenderMode="Lightweight" ID="chkFgPonderarCompetenciasAuto" runat="server" ToggleType="Radio" ButtonType="ToggleButton" Text="Ponderación estandar" AutoPostBack="false" GroupName="btnCompetencia">
                                    </telerik:RadButton>
                                </div>
                                <%--                    <div style="clear: both; height: 10px;"></div>--%>
                                <%-- <div class="ctrlBasico">
                            <telerik:RadButton RenderMode="Lightweight" Enabled="false" ID="chkFgPonderarEvaluadores" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Ponderar evaluación por evaluadores" OnClientCheckedChanged="enableCtrlPonderarEvaluadores"
                                AutoPostBack="false">
                            </telerik:RadButton>
                            <div style="clear: both; height: 10px;"></div>--%>
                                <%--<div style="display: none;" id="divPonderarEvaluadores" runat="server">--%>

                                <%-- <fieldset>
                                    <legend>Por evaluador</legend>
                                    <table class="ctrlTableForm">
                                        <tr>
                                            <td style="text-align: right;">
                                                <label name="lblNoAutoevaluacion">Autoevaluación:</label></td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPrAutoevaluacion" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <label name="lblNoSuperior">Jefe inmediato:</label></td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPrSuperior" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <label name="lblNoSubordinados">Subordinados:</label></td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPrSubordinados" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <label name="lblNoInterrelacionados">Interrelacionados:</label></td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPrInterrelacionados" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <label name="lblNoOtros">Otros:</label></td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPrOtros" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionEvaluadores"></telerik:RadNumericTextBox><span> %</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <label name="lblNoTotal">Total:</label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPrTotal" runat="server" Enabled="false" Width="60" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><span> %</span>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>--%>
                                <%--  </div>--%>
                                <%--   </div>--%>
                                <div class="ctrlBasico" runat="server">
                                    <telerik:RadButton RenderMode="Lightweight" Enabled="false" ID="chkFgPonderacionCompetencia" runat="server" ToggleType="Radio" ButtonType="ToggleButton" Text="Personalizar ponderación" OnClientCheckedChanged="enableCtrlPonderarCompetencias" GroupName="btnCompetencia"
                                        AutoPostBack="false">
                                    </telerik:RadButton>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div style="display: none;" id="divPonderarCompetencias" runat="server">
                                        <%-- <fieldset>
                                    <legend>Por competencia</legend>--%>
                                        <table class="ctrlTableForm">
                                            <tr>
                                                <td>
                                                    <label name="lblPrGenericas" id="lblPrGenericas" runat="server">Genéricas:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrGenericas" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionCompetencias"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                       
                                            <tr>
                                                <td>
                                                    <label name="lblPrEspecificas" id="lblPrEspecificas" runat="server">Específicas:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrEspecificas" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionCompetencias"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                       
                                            <tr>
                                                <td>
                                                    <label name="lblPrInstitucionales" id="lblPrInstitucionales" runat="server">Institucionales:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrInstitucionales" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxValue="100" MinValue="-1" ClientEvents-OnValueChanged="CheckPonderacionCompetencias"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                   <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label name="lblPrTotalCom" id="lblPrTotalCom" runat="server">Total:</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPrTotalCompetencias" runat="server" Enabled="false" Width="60" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><span> %</span>
                                                </td>
                                            </tr>
                                        </table>
                                        <%-- </fieldset>--%>
                                    </div>
                                </div>

                            </fieldset>
                            </div>
                        </div>
                    </div>
                    <div style="height: 10px; clear: both;"></div>
                    <div class="divControlDerecha">
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnGuardarPonderacion" runat="server" Text="Guardar" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
                        </div>
                        <%--<div class="ctrlBasico">
                            <telerik:RadButton ID="btnGuardarPonderacionCerrar" runat="server" Text="Guardar y cerrar" OnClick="btnGuardarConfiguracionCerrar_Click"></telerik:RadButton>
                        </div>--%>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvSeleccionEvaluados" runat="server" Height="100%">
                    <div style="height: calc(100% - 25px); padding-bottom: 10px;">
                <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="RadPane1" Height="100%" runat="server">
                            <telerik:RadGrid ID="grdEvaluados" runat="server" Height="100%"
                                AutoGenerateColumns="false" EnableHeaderContextMenu="true"
                                AllowSorting="true" AllowMultiRowSelection="true"
                                OnNeedDataSource="grdEvaluados_NeedDataSource"
                                OnDeleteCommand="grdEvaluados_DeleteCommand" HeaderStyle-Font-Bold="true"
                                OnDetailTableDataBind="grdEvaluados_DetailTableDataBind"
                                OnItemDataBound="grdEvaluados_ItemDataBound"
                                OnPreRender="grdEvaluados_PreRender">
                                <ClientSettings AllowKeyboardNavigation="true">
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_EMPLEADO,ID_EVALUADO" ClientDataKeyNames="ID_EMPLEADO,ID_EVALUADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" EnableHierarchyExpandAll="true" HierarchyDefaultExpanded="true" Name="Parent">
                                    <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno" DataField="NB_APELLIDO_PATERNO" UniqueName="NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido materno" DataField="NB_APELLIDO_MATERNO" UniqueName="NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave del área/departamento" DataField="CL_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_CL_DEPARTAMENTO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
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
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre de la empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Razón social" DataField="NB_RAZON_SOCIAL" UniqueName="NB_RAZON_SOCIAL"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Estatus" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Activo" DataField="FG_ACTIVO" UniqueName="FG_ACTIVO"></telerik:GridBoundColumn>
                                    </Columns>
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="ID_PUESTO_EVALUADO_PERIODO" ClientDataKeyNames="ID_PUESTO_EVALUADO_PERIODO" Name="gtvPuestosEvaluacion" Width="100%">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="Puesto para evaluación" DataField="NB_PUESTO" UniqueName="NB_PUESTO">
                                                    <HeaderStyle Font-Bold="true" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30"
                                                    ConfirmTextFields="NB_PUESTO" ConfirmTextFormatString="¿Desea eliminar el puesto {0}?" ConfirmDialogWidth="400"
                                                    ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                </MasterTableView>
                            </telerik:RadGrid>
                         </telerik:RadPane>
                       <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="22px">
                           <telerik:RadSlidingZone Visible="false" Enabled="false" ID="RadSlidingZone1" runat="server" SlideDirection="Left" Width="22px" ClickToOpen="true">
                               <telerik:RadSlidingPane ID="rspFiltroInter" Visible="false" Enabled="false" runat="server" Title="Filtro interrelacionados" Width="500px">
                                   <div class="ctrlBasico" style="display: inline-block;" id="divConfiguracionInterrelacionados" runat="server">
                                       <fieldset style="padding-left: 10px; vertical-align: middle;">
                                           <legend id="lgInterrelacionado" runat="server">Interrelacionados</legend>
                                           <div class="ctrlBasico">
                                               <label id="lblLstCamposInterrelacionados" name="lblLstCamposInterrelacionados" runat="server">Campo en común:</label><br />
                                               <table class="ctrlTableForm">
                                                   <tr>
                                                       <td>
                                                           <telerik:RadListBox ID="lstCamposInterrelacionados" runat="server" Width="300" SelectionMode="Multiple">
                                                               <Items>
                                                                   <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                                                               </Items>
                                                           </telerik:RadListBox>
                                                       </td>
                                                       <td>
                                                           <telerik:RadButton ID="btnEspecificarCamposRelacionados" runat="server" OnClientClicked="OpenCamposInterrelacionadosSelectionWindow" AutoPostBack="false" Text="+">
                                                           </telerik:RadButton>
                                                       </td>
                                                       <td>
                                                           <telerik:RadButton ID="btnEliminarCamposRelacionados" runat="server" OnClientClicked="DeleteCampoInterrelacionado" AutoPostBack="false" Text="x"></telerik:RadButton>
                                                       </td>
                                                   </tr>
                                               </table>
                                           </div>
                                       </fieldset>
                                   </div>
                               </telerik:RadSlidingPane>
                           </telerik:RadSlidingZone>
                       </telerik:RadPane>
                   </telerik:RadSplitter>

                        </div>
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
                            <telerik:RadButton ID="btnEliminarEvaluado" runat="server" Text="Eliminar" OnClientClicking="confirmarEliminarEvaluados" OnClick="btnEliminarEvaluado_Click"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnAgregarPuestos" runat="server" Text="Puestos contra los que se evaluará" OnClientClicked="OpenOtrosPuestosSelectionWindow"></telerik:RadButton>
                        </div>
                    <div class="divControlDerecha">
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="RadButton1" runat="server" Text="Guardar" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
                        </div>
                    </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvOtrosEvaluadores" runat="server">
                <div style="height: 100%;">
                    <telerik:RadSplitter ID="rspOtrosEvaluadores" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="rpnGridOtrosEvaluadores" runat="server">
                            <telerik:RadGrid ID="grdEvaluadoresExternos" runat="server" Height="100%" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" AllowSorting="true"
                                OnUpdateCommand="grdEvaluadoresExternos_UpdateCommand"
                                OnDeleteCommand="grdEvaluadoresExternos_DeleteCommand"
                                OnNeedDataSource="grdEvaluadoresExternos_NeedDataSource"
                                OnItemDataBound="grdEvaluadoresExternos_ItemDataBound"
                                OnPreRender="grdEvaluadoresExternos_PreRender">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_EVALUADOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" EditMode="InPlace">
                                    <Columns>
                                        <telerik:GridEditCommandColumn UniqueName="EditEvaluado" ButtonType="ImageButton" EditText="Editar" InsertText="Insertar" UpdateText="Actualizar" HeaderStyle-Width="30"></telerik:GridEditCommandColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Tipo" DataField="CL_TIPO_EVALUADOR" UniqueName="CL_TIPO_EVALUADOR" ReadOnly="true">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="NB_EVALUADOR" HeaderText="Nombre" UniqueName="NB_EVALUADOR" HeaderStyle-Width="400" FilterControlWidth="330">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemTemplate>
                                                <%# Eval("NB_EVALUADOR") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtNbEvaluadorEditForm" runat="server" Text='<%# Eval("NB_EVALUADOR") %>' Width="100%"></telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="NB_PUESTO" HeaderText="Puesto" UniqueName="NB_PUESTO" HeaderStyle-Width="400" FilterControlWidth="330">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemTemplate>
                                                <%# Eval("NB_PUESTO") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtNbPuestoEvaluadorEditForm" runat="server" Text='<%# Eval("NB_PUESTO") %>' Width="100%"></telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="CL_CORREO_EVALUADOR" HeaderText="Correo electrónico" UniqueName="CL_CORREO_EVALUADOR" HeaderStyle-Width="400" FilterControlWidth="330">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemTemplate>
                                                <%# Eval("CL_CORREO_EVALUADOR") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtClCorreoElectronicoEditForm" runat="server" Text='<%# Eval("CL_CORREO_EVALUADOR") %>' Width="100%"></telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="CL_EVALUA_TODOS" HeaderText="Evalúa a todos" UniqueName="CL_EVALUA_TODOS" HeaderStyle-Width="110" FilterControlWidth="40">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemTemplate>
                                                <%# Eval("CL_EVALUA_TODOS") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadButton ID="chkFormFgEvaluaTodos" runat="server" ToggleType="CheckBox" AutoPostBack="false" Checked='<%# Eval("FG_EVALUA_TODOS") %>'>
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="No" CssClass="unchecked" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_EVALUADOR" ConfirmTextFormatString="¿Desea eliminar a {0} como evaluador?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPane>
                        <telerik:RadPane ID="rpnOtroEvaluador" runat="server" Scrolling="None" Width="22px">
                            <telerik:RadSlidingZone ID="rszOtroEvaluador" runat="server" SlideDirection="Left" Width="22px" ClickToOpen="true">
                                <telerik:RadSlidingPane ID="rspOtroEvaluadorManual" runat="server" Title="Externo" Width="400px">
                                    <div style="padding-top: 10px;">
                                        <table class="ctrlTableForm">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label name="lblNbEvaluadorExterno" id="lblNbEvaluadorExterno" runat="server">Nombre:</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNbEvaluadorExterno" runat="server" Width="300"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label name="lblPuestoEvaluadorExterno" id="lblPuestoEvaluadorExterno" runat="server">Puesto:</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNbEvaluadorExternoPuesto" runat="server" Width="300"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <label name="lblClCorreoElectronico" id="lblClCorreoElectronico" runat="server">Correo electrónico:</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtClCorreoElectronico" runat="server" Width="300"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="padding-top: 10px;">
                                        <table class="ctrlTableForm">
                                            <tr>
                                                <td>
                                                    <telerik:RadButton ID="chkFgOtroEvaluadorExternoTodos" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="30">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </td>
                                                <td>
                                                    <label name="lblEvalExt" id="lblEvalExt" runat="server">Evalúa a todos los evaluados</label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadButton ID="btnAgregarOtroEvaluador" runat="server" Text="Agregar" OnClick="btnAgregarOtroEvaluador_Click"></telerik:RadButton>
                                    </div>
                                </telerik:RadSlidingPane>
                                <telerik:RadSlidingPane ID="rspOtroEvaluadorInventario" runat="server" Title="Inventario" Width="300">
                                    <table class="ctrlTableForm">
                                        <tr>
                                            <td>
                                                <telerik:RadButton ID="chkFgOtroEvaluadorTodos" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="30">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td>
                                                <label name="lblEvalInt" id="lblEvalInt" runat="server">Evalúa a todos los evaluados</label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="divControlDerecha">
                                        <telerik:RadButton ID="btnAgregarOtrosEvaluadoresInventario" runat="server" Text="Agregar desde el inventario" AutoPostBack="true" OnClientClicked="OpenOtrosEvaluadoresSelectionWindow"></telerik:RadButton>
                                    </div>
                                </telerik:RadSlidingPane>
                            </telerik:RadSlidingZone>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </div>
                <div style="clear: both;"></div>
                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="RadButton2" runat="server" Text="Guardar" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>
            
            <telerik:RadPageView ID="rpvCuestionarios" runat="server">
                <div style="height: calc(100% - 25px); padding-bottom: 10px;">
                    <telerik:RadSplitter ID="rsCuestionarios" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="rpCuestionarios" runat="server">
                            <telerik:RadGrid ID="grdCuestionarios" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true"
                                OnNeedDataSource="grdCuestionarios_NeedDataSource"
                                OnDeleteCommand="grdCuestionarios_DeleteCommand"
                                OnDetailTableDataBind="grdCuestionarios_DetailTableDataBind"
                                OnItemDataBound="grdCuestionarios_ItemDataBound"
                                OnPreRender="grdCuestionarios_PreRender">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_EVALUADO" ClientDataKeyNames="ID_EVALUADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" HierarchyDefaultExpanded="true" EnableHierarchyExpandAll="true">
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Evaluado" HeaderStyle-Width="100" FilterControlWidth="40" DataField="NB_EVALUADO" UniqueName="NB_EVALUADO">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="350" FilterControlWidth="280" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. de cuestionarios" DataField="NO_CUESTIONARIOS" UniqueName="NO_CUESTIONARIOS" ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="ID_CUESTIONARIO" Name="gtvEvaluadores">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Evaluador" UniqueName="NB_EVALUADOR">
                                                    <HeaderStyle Font-Bold="true" />
                                                    <ItemTemplate>
                                                        <span style="border: 1px solid gray; border-radius: 5px;" class="<%# Eval("CL_ROL_EVALUADOR") %>" title="<%# Eval("NB_ROL_EVALUADOR") %>, Clave evaluador: <%# Eval("CL_EVALUADOR")%>, Clave puesto: <%# Eval("CL_PUESTO")%>">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<%# Eval("NB_EVALUADOR") %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderStyle-Width="350" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO">
                                                    <HeaderStyle Font-Bold="true" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Rol" HeaderStyle-Width="150" UniqueName="NB_ROL_EVALUADOR">
                                                    <HeaderStyle Font-Bold="true" />
                                                    <ItemTemplate>
                                                        <div style="border: 1px solid gray; padding: 3px; text-align: center; min-width: 50px; border-radius: 5px;" class="<%# Eval("CL_ROL_EVALUADOR") %>"><%# Eval("NB_ROL_EVALUADOR") %></div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_EVALUADOR" ConfirmTextFormatString="¿Desea eliminar el cuestionario del evaluador {0}?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPane>

                        <telerik:RadPane ID="rpAyudaCuestionario" runat="server" Scrolling="None" Width="22px">
                            <telerik:RadSlidingZone ID="rszAyudaCuestionario" runat="server" SlideDirection="Left" Width="22px" DockedPaneId="rspAyudaCuestionario" ClickToOpen="true">
                                <telerik:RadSlidingPane ID="rspAyudaCuestionario" RenderMode="Mobile" runat="server" Title="Ayuda" Width="400px">
                                    <div style="text-align: justify; padding: 10px;">
                                        <p id="txtAyuda" runat="server">Utiliza esta página para definir la programación de cuestionarios.<br />
                                        Esta página te permite definir a los evaluadores que recibirán cuestionarios para calificar las competencias de las personas a evaluar en el proceso de Deteccion de Necesidades de Capacitación. Para facilitarte esta decisión te proponemos la siguiente matriz, revisa detenidamente esta propuesta y utiliza, en su caso, el icono para quitar aquellas personas que no deseas que reciban un cuestionario de evaluación. Utiliza el botón para agregar más personas si lo deseas. Una vez que hayas concluido la revisión y selección haz clic en el botón Crear cuestionarios y la matriz se ajustará a tus requerimientos.</p>
                                    </div>
                                </telerik:RadSlidingPane>
                            </telerik:RadSlidingZone>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </div>
                <div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnPlaneacionMatriz" runat="server" Text="Edición de Matriz" AutoPostBack="false" OnClientClicked="OpenPlaneacionMatrizWindow"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnMostrarMatriz" runat="server" Text="Edición por evaluado" AutoPostBack="false" OnClientClicked="OpenMatrizEvaluadoresWindow"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnRegistroAutorizacion" OnClientClicked="ShowAutorizarForm" AutoPostBack="false" runat="server" Text="Ver registro y autorización" Width="200" ToolTip="Da clic si deseas ver el registro de este programa de capacitación."></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnAgregarCuestionario" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenAgregarCuestionarioSelectionWindow" Visible="false"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnCrearCuestionarios" runat="server" Text="Crear cuestionarios" OnClick="btnCrearCuestionarios_Click" Visible="false"></telerik:RadButton>
                    </div>
                    <div class="divControlDerecha">
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="RadButton3" runat="server" Text="Guardar" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
                        </div>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvContasenas" runat="server">
                <div style="height: calc(100% - 25px); padding-bottom: 10px;">
                    <telerik:RadGrid ID="grdContrasenaEvaluadores" AllowMultiRowSelection="true" runat="server" Height="100%" AutoGenerateColumns="false" AllowSorting="true"
                        OnNeedDataSource="grdContrasenaEvaluadores_NeedDataSource"
                        OnItemDataBound="grdContrasenaEvaluadores_ItemDataBound"
                        OnPreRender="grdContrasenaEvaluadores_PreRender">
                        <ClientSettings EnableAlternatingItems="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EVALUADOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                                                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Evaluador" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Contraseña" DataField="CL_TOKEN" UniqueName="CL_TOKEN">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both;"></div>
               <%-- <div class="ctrlBasico">
                    <telerik:RadButton ID="btnReasignarTodasContrasenas" runat="server" Text="Reasignar contraseñas a todos" OnClick="btnReasignarTodasContrasenas_Click"></telerik:RadButton>
                </div>--%>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnReasignarContrasena" runat="server" Text="Reasignar contraseña al evaluador seleccionado" OnClick="btnReasignarContrasena_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEnviarCuestionarios" runat="server" Text="Enviar evaluaciones" Enabled="false" OnClientClicked="OpenEnvioCuestionariosWindow" AutoPostBack="false"></telerik:RadButton>
                </div>
                <div class="divControlesBoton">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarCerrar" runat="server" Text="Guardar y cerrar" OnClick="btnGuardarConfiguracionCerrar_Click"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winAutoriza" runat="server" Height="600" Width="1300" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar área/departamento" Width="800" Height="600" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>

