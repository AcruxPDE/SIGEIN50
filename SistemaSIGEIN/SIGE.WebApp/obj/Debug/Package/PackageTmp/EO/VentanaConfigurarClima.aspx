<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaConfigurarClima.aspx.cs" Inherits="SIGE.WebApp.EO.ConfiguracionClima" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

        function onCloseWindowE(oWnd, args) {
            $find("<%=grdEmpleadosSeleccionados.ClientID%>").get_masterTableView().rebind();
        }

        function onCloseWindowC(oWnd, args) {
            $find("<%=grdEmpleadosContrasenias.ClientID%>").get_masterTableView().rebind();
        }

        function onCloseWindowP(oWnd, args) {
            $find("<%=grdPreguntasCuestionario.ClientID%>").get_masterTableView().rebind();
        }

        function onCloseWindowPA(oWnd, args) {
            $find("<%=rgPreguntas.ClientID%>").get_masterTableView().rebind();
        }

        function AbrirVentanaCuestionario() {
            openChildDialog("VentanaCuestionario.aspx?&ID_PERIODO=<%=vIdPeriodo%>", "WinCuestionario", "Agregar Pregunta")
        }

        function AbrirVentanaPreguntaAbierta() {
            openChildDialog("VentanaPreguntaAbierta.aspx?&ID_PERIODO=<%= vIdPeriodo%>", "WinPreguntas", "Agregar pregunta abierta");
        }

        function OpenEmpleadosSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "WinClimaLaboral", "Selección de evaluados");
        }

        function OpenPuestoSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPuesto.aspx?mulSel=1", "WinClimaLaboral", "Selección de puestos");
        }

        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx", "WinClimaLaboral", "Selección de áreas/departamentos");
        }

        function OpenFiltrosSelectionWindow() {
            openChildDialog("VentanaFiltrosSeleccion.aspx?ID_PERIODO=<%= vIdPeriodo%>", "WinFiltrosSeleccion", "Selección por filtros");
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

        function OpenEnvioSolicitudesWindow() {
            var vIdPeriodo = '<%= vIdPeriodo %>';
            if (vIdPeriodo != null)
                OpenWindow(GetEnvioSolicitudesWindowProperties(vIdPeriodo));
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function GetEnvioSolicitudesWindowProperties(pIdPeriodo) {

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            windowProperties.width = 1050;
            windowProperties.vTitulo = "Enviar cuestionarios";
            windowProperties.vURL = "EnvioDeSolicitudesClima.aspx?PeriodoId=" + pIdPeriodo;
            windowProperties.vRadWindowId = "WinClimaLaboral";
            return windowProperties;
        }


        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {
                    case "EMPLEADO":
                        InsertEvaluado(EncapsularSeleccion("EVALUADO", pDato));
                        break;
                    case "PUESTO":
                        InsertEvaluado(EncapsularSeleccion("PUESTO", pDato));
                        break;
                    case "DEPARTAMENTO":
                        InsertEvaluado(EncapsularSeleccion("AREA", pDato));
                        break;
                    case "CUESTIONARIO":
                        $find("<%=grdPreguntasCuestionario.ClientID%>").get_masterTableView().rebind();
                        break;
                    case "PREGUNTA_ABIERTA":
                        $find("<%=rgPreguntas.ClientID%>").get_masterTableView().rebind();
                        break;
                    case "FILTROS":
                        if (pDato[0].fgAplicados == "True" && pDato[0].fgEvaluados == "True") {
                            var btnSelPersona = $find("<%=btnSeleccionar.ClientID%>");
                            btnSelPersona.set_enabled(false);
                            var btnSelPuesto = $find("<%=btnSeleccionarPuesto.ClientID%>");
                            btnSelPuesto.set_enabled(false);
                            var btnSelArea = $find("<%=btmSleccionarArea.ClientID%>");
                            btnSelArea.set_enabled(false);
                            var btnEliminarEvaluador = $find("<%=btnEliminarEvaluador.ClientID%>");
                            btnEliminarEvaluador.set_enabled(false);
                            dvMenEval.style.visibility = true;
                        }
                        break;
                    case "INDICE_GENERO":
                        var listGenero = $find("<%=rlbGenero.ClientID %>");
                        //  InsertGenero(listGenero, pDato[0]);
                        InsertGenero(pDato, listGenero);
                        break;
                    case "INDICE_DEPARTAMENTO":
                        var vListBox = $find("<%=rlbDepartamento.ClientID %>");
                        InsertDepartamentos(pDato, vListBox);
                        break;
                    case "ADSCRIPCION":
                        var vListBox = $find("<%= rlbAdicionales.ClientID%>");
                        InsertAdicionales(pDato, vListBox);
                        break;
                }
            }
        }

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertEvaluado(pDato) {
            var ajaxManager = $find('<%= ramConfiguracionPeriodoClima.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        var idPregunta = "";

        function obtenerIdFila() {
            var grid = $find("<%=grdPreguntasCuestionario.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                console.info(SelectDataItem);
                idPregunta = SelectDataItem.getDataKeyValue("ID_PREGUNTA");
            }
        }

        function obtenerFilaPrAbiertas() {
            var grid = $find("<%=rgPreguntas.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                console.info(SelectDataItem);
                idPregunta = SelectDataItem.getDataKeyValue("ID_PREGUNTA");
            }
        }

        function EditarPregunta(sender, args) {
            obtenerIdFila();
            if (idPregunta != "") {
                openChildDialog("VentanaCuestionario.aspx?&ID_PREGUNTA=" + idPregunta + "&ID_PERIODO=" + <%=vIdPeriodo%> + "", "WinCuestionario",
                       "Nuevo/Editar pregunta cuestionario")
            } else {
                radalert("Selecciona una pregunta.", 400, 150, "Error");
                args.set_cancel(true);
            }
        }

        function ConfirmValidez(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
            { if (shouldSubmit) { this.click(); } });

            radconfirm('¿Estás seguro de eliminar la validez del cuestionario? Una vez realizado ya no podrá revertirse. ', callBackFunction, 400, 170, null, "Eliminar validez");
            args.set_cancel(true);
        }

        function EditarPreguntaAbierta(sender, args) {
            obtenerFilaPrAbiertas();
            if (idPregunta != "") {
                openChildDialog("VentanaPreguntaAbierta.aspx?ID_PREGUNTA=" + idPregunta + "&ID_PERIODO=" + <%=vIdPeriodo%> + "", "WinPreguntas", "Editar pregunta abierta")
        } else {
            radalert("Selecciona una pregunta.", 400, 150, "Error");
            args.set_cancel(true);
        }
    }

    function OpenSelectionDepartamento() {
        openChildDialog("../Comunes/SeleccionArea.aspx?CatalogoCl=INDICE_DEPARTAMENTO", "WinCuestionario", "Selección de área/departamento");
    }
    function OpenSelectionGenero() {
        openChildDialog("../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=2" + "&CatalogoCl=INDICE_GENERO", "WinCuestionario", "Selección de género");
    }

    function OpenSelectionAdicionales() {
        openChildDialog("../Comunes/SeleccionAdscripciones.aspx?MultiSeleccion=1" + "&ClLista=IS", "WinConsultaPersonal", "Selección de campos adicionales");
    }


    function DeleteDepartamento() {
        var vListBox = $find("<%=rlbDepartamento.ClientID %>");
            Delete(vListBox);
        }

        function DeleteAdicionales() {
            var vListAdscripcion = $find("<%= rlbAdicionales.ClientID %>");
        Delete(vListAdscripcion);
    }

    function DeleteGenero() {
        var vListBox = $find("<%=rlbGenero.ClientID %>");
        Delete(vListBox);
    }

        function InsertGenero(vSelectedData, list) {
            var arrSeleccion = [];
            for (var i = 0; i < vSelectedData.length; i++)
                arrSeleccion.push({
                    idItem: vSelectedData[i].clCatalogoValor,
                    nbItem: vSelectedData[i].nbCatalogoValor
                });

            OrdenarSeleccion(arrSeleccion, list);
        //if (list != undefined) {
        //    list.trackChanges();
        //    var items = list.get_items();
        //    items.clear();
        //    var item = new Telerik.Web.UI.RadListBoxItem();
        //    item.set_text(vSelectedData.nbCatalogoValor);
        //    item.set_value(vSelectedData.clCatalogoValor);
        //    item.set_selected(true);
        //    items.add(item);
        //    list.commitChanges();
        //}
    }

    function InsertDepartamentos(pDato, pListBox) {
        var arrSeleccion = [];
        for (var i = 0; i < pDato.length; i++)
            arrSeleccion.push({
                idItem: pDato[i].idArea,
                nbItem: pDato[i].nbArea
            });

        OrdenarSeleccion(arrSeleccion, pListBox);
    }

    function InsertAdicionales(pDato, pListBox) {
        var arrSeleccion = [];
        for (var i = 0; i < pDato.length; i++)
            arrSeleccion.push({
                idItem: pDato[i].idCampo,
                nbItem: pDato[i].nbValor
            });
        OrdenarSeleccion(arrSeleccion, pListBox);
    }

    function OrdenarSeleccion(pSeleccion, vListBox) {
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

    function ChangeListItem(pIdItem, pNbItem, pListBox) {
        var items = pListBox.get_items();
        var item = new Telerik.Web.UI.RadListBoxItem();
        item.set_text(pNbItem);
        item.set_value(pIdItem);
        items.add(item);
        item.set_selected(true);
    }

    function Delete(vListBox) {
        var vSelectedItems = vListBox.get_selectedItems();
        vListBox.trackChanges();
        if (vSelectedItems)
            vSelectedItems.forEach(function (item) {
                vListBox.get_items().remove(item);
            });
        vListBox.commitChanges();
    }

    function ConfirmarAsignar(sender, args) {
        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
        { if (shouldSubmit) { this.click(); } });

        radconfirm('¿Deseas asignar los cuestionarios a los evaluadores?, este proceso no te permitirá rediseñar el cuestionario.', callBackFunction, 400, 170, null, "Asignar cuestionarios");
        args.set_cancel(true);
    }

    function ConfirmarAsignarPreguntas(sender, args) {
        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
        { if (shouldSubmit) { this.click(); } });

        radconfirm('¿Deseas asignar las preguntas al cuestionario?, este proceso no te permitirá rediseñar las preguntas.', callBackFunction, 400, 170, null, "Asignar cuestionarios");
        args.set_cancel(true);
    }


    function ConfirmaElimina(sender, args) {
        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
        { if (shouldSubmit) { this.click(); } });

        radconfirm('¿Estás seguro de eliminar esta pregunta?, este proceso no podrá revertirse. ', callBackFunction, 400, 170, null, "Eliminar pregunta abierta");
        args.set_cancel(true);
    }

    function ConfirmEliminaPregunta(sender, args) {
        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
        { if (shouldSubmit) { this.click(); } });

        radconfirm('¿Estás seguro de eliminar esta pregunta?, este proceso no podrá revertirse. ', callBackFunction, 400, 170, null, "Eliminar pregunta");
        args.set_cancel(true);
    }


    function ConfirmActualizaMnesaje(sender, args) {
        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
        { if (shouldSubmit) { this.click(); } });

        radconfirm('¿Estás seguro de actualizar el mensaje envío cuestionarios?, Recuerda que este mensaje es único para todos los períodos. Para más información ve a la sección de ayuda.', callBackFunction, 460, 180, null, "Actualizar mensaje");
        args.set_cancel(true);
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpConfiguracionPeriodo" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConfiguracionPeriodoClima" runat="server" OnAjaxRequest="ramConfiguracionPeriodoClima_AjaxRequest" DefaultLoadingPanelID="ralpConfiguracionPeriodo">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConfiguracionPeriodoClima">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosContrasenias" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReasignarTodasContrasenas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosContrasenias" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReasignarContrasena">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosContrasenias" UpdatePanelHeight="100%" />
                </UpdatedControls>               
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnGuardar" UpdatePanelHeight="100%" />
                </UpdatedControls>
                               </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosContrasenias" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarPregunta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPreguntasCuestionario" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarAbierta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPreguntas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarAbierta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPreguntas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditarAbierta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPreguntas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
           <%-- <telerik:AjaxSetting AjaxControlID="btnGuardarAbierta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnEditarAbierta" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarAbierta" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarAbierta" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnEliminarEvaluador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosContrasenias" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCrearCuestionarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnEditar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarPregunta" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                      <telerik:AjaxUpdatedControl ControlID="btnEditarAbierta" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarAbierta" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"  />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarAbierta" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                     <telerik:AjaxUpdatedControl ControlID="btnSeleccionar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                      <telerik:AjaxUpdatedControl ControlID="btnEliminarEvaluador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btmSleccionarArea" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnCrearCuestionarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnEnvioCuestionarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="rtsConfiguracionClima" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
                    <Tabs>
                        <telerik:RadTab Text="Contexto"></telerik:RadTab>
                        <telerik:RadTab Text="Selección de evaluadores"></telerik:RadTab>
                        <telerik:RadTab Text="Parámetros adicionales"></telerik:RadTab>
                        <telerik:RadTab Text="Preguntas abiertas"></telerik:RadTab>
                        <telerik:RadTab Text="Cuestionario"></telerik:RadTab>
                        <telerik:RadTab Text="Mensajes"></telerik:RadTab>
                        <telerik:RadTab Text="Contraseñas"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: 10px;"></div>
                <div style="height: calc(100% - 60px);">
                    <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="rpvContexto" runat="server">
                             <%--  <div class="ctrlBasico">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnPredefinido" runat="server" ToggleType="CheckBox" AutoPostBack="false" ReadOnly="true" Width="260" Text="Cuestionario predefinido de SIGEIN">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>              
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnCopia" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="260" ReadOnly="true" Text="Cuestionario copia de otro periodo">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>
                            
                         
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnVacios" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="260" ReadOnly="true" Text="Cuestionario creado desde cero">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>
                                        </tr>
                                </table>
                            </div>--%>
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
                                                <label id="Label2" name="lbNotas" runat="server">Notas:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtNotas" runat="server"></div>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="LbEstado" name="lbTabulador" runat="server">Estado:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtEstatus" runat="server"></div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label6" name="lbNotas" runat="server">Tipo de cuestionario:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtTipo" runat="server"></div>
                                            </td>
                                        </tr>
                                            <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="LbTipoCuestionario" name="lbNotas" runat="server">Origen de cuestionario:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="lbCuestionario" runat="server"></div>
                                            </td>
                                        </tr>
                                   <%--     <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbedad" name="LbFiltros" runat="server" visible="false">Edad:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtEdad" runat="server" visible="false"></div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbGenero" name="LbFiltros" runat="server" visible="false">Género:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtGenero" runat="server" visible="false"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbAntiguedad" name="LbFiltros" runat="server" visible="false">Antigüedad:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtAntiguedad" runat="server" visible="false"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbDepartamento" name="LbFiltros" runat="server" visible="false">Área:</label>
                                            </td>
                                            <td colspan="2" class="ctrlTableDataContext">
                                                <telerik:RadTextBox ID="rlDepartamento" Visible="false" ReadOnly="true" runat="server" Width="200" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbAdscripciones" name="LbFiltros" runat="server" visible="false">Campos adicionales:</label>
                                            </td>
                                            <td rowspan="3" class="ctrlTableDataContext" visible="false">
                                                <telerik:RadTextBox ID="rlAdicionales" Visible="false" ReadOnly="true" runat="server" Width="200" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
                                            </td>

                                        </tr>--%>
                                    </table>
                                </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvSeleccionEvaluados" runat="server">
                            <div style="height: calc(100% - 40px);" class="ctrlBasico">
                                <telerik:RadGrid ID="grdEmpleadosSeleccionados" runat="server" HeaderStyle-Font-Bold="true" AllowMultiRowSelection="true" OnNeedDataSource="grdEmpleadosSeleccionados_NeedDataSource" AutoGenerateColumns="false" Height="100%" OnItemDataBound="grdEmpleadosSeleccionados_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                        <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView DataKeyNames="ID_EVALUADOR" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="40" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="100" HeaderText="Nombre completo" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="100" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="100" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_AREA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both;"></div>
                           <%-- <div style="color: red;" runat="server" id="dvMenEval" visible="false">Evaluadores seleccionados confidencialmente mediante filtros de selección.</div>
                            <div style="color: red;" runat="server" id="dvMensa2" visible="false">Evaluadores seleccionados confidencialmente mediante filtros de selección.</div>--%>
                            <telerik:RadButton ID="btnSeleccionar" runat="server" Text="Seleccionar por persona" OnClientClicked="OpenEmpleadosSelectionWindow"></telerik:RadButton>
                            <telerik:RadButton ID="btnSeleccionarPuesto" runat="server" Text="Seleccionar por puesto" OnClientClicked="OpenPuestoSelectionWindow"></telerik:RadButton>
                            <telerik:RadButton ID="btmSleccionarArea" runat="server" Text="Seleccionar por área/departamento" OnClientClicked="OpenAreaSelectionWindow"></telerik:RadButton>
                            <telerik:RadButton ID="btnEliminarEvaluador" runat="server" Text="Eliminar" OnClick="btnEliminarEvaluador_Click"></telerik:RadButton>
                            <div class="divControlesBoton">
                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnGuardarEvaluados" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCampos" runat="server">
                            <div style="clear: both;"></div>
                            <div style="height: calc(100% - 60px);">
                                <div style="clear: both; height: 20px"></div>
                                <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <%--<telerik:RadButton RenderMode="Lightweight" ID="chkarea" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            AutoPostBack="false">
                                        </telerik:RadButton>--%>
                                        <label id="lblDepartamentoDistribucion" name="lblDepartamento" runat="server">Área/Departamento:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadListBox ID="rlbDepartamento" Width="200px" Height="100px" runat="server" ValidationGroup="vgDepartamentoDis"></telerik:RadListBox>
                                        <telerik:RadButton ID="btnDistribucionSeleccionarDep" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamentoDis" OnClientClicked="OpenSelectionDepartamento"></telerik:RadButton>
                                        <telerik:RadButton ID="btnDistribucionEliminaDep" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamentoDis" OnClientClicked="DeleteDepartamento"></telerik:RadButton>
                                    </div>
                                </div>
                                <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <%--              <telerik:RadButton RenderMode="Lightweight" ID="chkgenero" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            AutoPostBack="false">
                                        </telerik:RadButton>--%>
                                        <label id="Label3" name="lblGenero" runat="server">Género:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadListBox ID="rlbGenero" Width="200px" Height="100px" runat="server" ValidationGroup="vgGeneroDis"></telerik:RadListBox>
                                        <telerik:RadButton ID="btnDistribucionSeleccionarGen" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionGenero"></telerik:RadButton>
                                        <telerik:RadButton ID="btnDistribucionEliminaGen" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgGeneroDis" OnClientClicked="DeleteGenero"></telerik:RadButton>
                                    </div>
                                </div>
                                <div style="height: 10px; clear: both;"></div>
                                <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <%--        <telerik:RadButton RenderMode="Lightweight" ID="chkadicionales" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            AutoPostBack="false">
                                        </telerik:RadButton>--%>
                                        <label id="Label1" name="lbAdscripciones" runat="server">Campos adicionales:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadListBox ID="rlbAdicionales" Width="200" Height="100px" runat="server" ValidationGroup="vgAdicionales"></telerik:RadListBox>
                                        <telerik:RadButton ID="btnBDist" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="OpenSelectionAdicionales"></telerik:RadButton>
                                        <telerik:RadButton ID="btnXDist" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="DeleteAdicionales"></telerik:RadButton>
                                    </div>
                                </div>
                                <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <telerik:RadButton RenderMode="Lightweight" ID="rbEdad" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            AutoPostBack="false">
                                        </telerik:RadButton>
                                        <label id="Label4" name="lblEdad" runat="server">Edad:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadNumericTextBox runat="server" ID="rntEdadInicial" NumberFormat-DecimalDigits="0" Value="18" Name="rnEdadInicial" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                        a
                                        <telerik:RadNumericTextBox runat="server" ID="rntEdadFinal" NumberFormat-DecimalDigits="0" Value="65" Name="rnEdadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                        años.
                                    </div>
                                </div>
                                <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <telerik:RadButton RenderMode="Lightweight" ID="rbAntiguedad" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            AutoPostBack="false">
                                        </telerik:RadButton>
                                        <label id="Label5" name="lblEdad" runat="server">Antigüedad:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadNumericTextBox runat="server" ID="rntAntiguedadInicial" NumberFormat-DecimalDigits="0" Value="0" Name="rnAntiguedadInicial" Width="60px" MinValue="0" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                        a
                                        <telerik:RadNumericTextBox runat="server" ID="rntAntiguedadFinal" NumberFormat-DecimalDigits="0" Value="30" Name="rnAtiguedadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                        meses.
                                    </div>
                                </div>
                            </div>
                            <div style="clear: both; height: 10px"></div>
                            <div class="divControlDerecha" style="padding-right: 30px;">
                                <telerik:RadButton ID="btnAplicar" runat="server" name="btnAplicar" AutoPostBack="true" Text="Guardar" Width="100" OnClick="btnAplicar_Click"></telerik:RadButton>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvPreguntasAbiertas" runat="server">
                            <div style="height: calc(100% - 50px);">
                                <telerik:RadSplitter runat="server" ID="spHelp1" Width="100%" Height="100%" BorderSize="0">
                                    <telerik:RadPane ID="rpHelp1" runat="server">
                                        <div style="clear: both;"></div>
                                        <telerik:RadGrid ID="rgPreguntas" HeaderStyle-Font-Bold="true" runat="server" AllowMultiRowSelection="true" AutoGenerateColumns="false" Height="100%" OnNeedDataSource="rgPreguntas_NeedDataSource" AllowSorting="true" OnItemDataBound="rgPreguntas_ItemDataBound">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="true" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView DataKeyNames="ID_PREGUNTA" ClientDataKeyNames="ID_PREGUNTA, ID_PREGUNTA" AllowPaging="true">
                                                <Columns>
                                                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" HeaderText="Pregunta" DataField="NB_PREGUNTA" UniqueName="NB_PREGUNTA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="300" HeaderText="Descripción" DataField="DS_PREGUNTA" UniqueName="DS_PREGUNTA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </telerik:RadPane>
                                    <telerik:RadPane ID="rpHelpAyuda1" runat="server" Width="30">
                                        <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                            <telerik:RadSlidingPane ID="rspMensaje" runat="server" Title="Ayuda" Width="300" MinWidth="500" Height="100%">
                                                <div style="padding: 10px; text-align: justify;">
                                
                                                        Las preguntas abiertas ayudan a solicitar respuestas libres al evaluador. Al permitir texto libre en las respuestas, estas pueden ser variadas 
                                                         ya que reflejan la opinión personal de cada uno.<br />
                                                        <br />

                                                        Aquí puedes crearlas añadiendo la pregunta y una descripción de la misma. Dicha descripción aparecerá como tooltip en cada pregunta. 
                                                        <br />
                                                        <br />
                                                        Las preguntas abiertas se podrán editar y eliminar. Al crear los cuestionarios automáticamente se agregarán al cuestionario de cada evaluador.                   
                                                </div>
                                            </telerik:RadSlidingPane>
                                        </telerik:RadSlidingZone>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </div>
                            <div style="height: 10px; clear: both;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnAgregarAbierta" runat="server" Text="Agregar" OnClientClicked="AbrirVentanaPreguntaAbierta"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnEditarAbierta" runat="server" Text="Editar" OnClientClicking="EditarPreguntaAbierta"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnEliminarAbierta" runat="server" OnClientClicking="ConfirmaElimina" OnClick="btnEliminarAbierta_Click" Text="Eliminar"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <label style="color: red;" runat="server" id="dvPreguntasAbiertas" visible="false">No se pueden modificar las preguntas abiertas por que ya fueron asignadas al cuestionario.</label>
                            </div>
                            <div class="divControlesBoton">
                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnGuardarAbierta" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                                </div>
                            </div>
                           <%-- <div class="ctrlBasico">
                                <telerik:RadButton ID="btnGuardarAbierta" runat="server" Text="Asignar preguntas a cuestionario" ToolTip="Esta opción te permite asignar las preguntas abiertas al cuestionario, una vez realizado este proceso no se podrán diseñar preguntas abiertas." OnClientClicking="ConfirmarAsignarPreguntas" OnClick="btnGuardarAbierta_Click"></telerik:RadButton>
                            </div>--%>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCuestionario" runat="server">
                            <div style="height: calc(100% - 50px);">
                                <telerik:RadSplitter runat="server" ID="rsHelp2" Width="100%" Height="100%" BorderSize="0">
                                    <telerik:RadPane ID="rpHelp2" runat="server">
                                        <div style="clear: both;"></div>
                                        <telerik:RadGrid ID="grdPreguntasCuestionario" HeaderStyle-Font-Bold="true" runat="server" AllowMultiRowSelection="true" AutoGenerateColumns="false" Height="100%" OnNeedDataSource="grdPreguntasCuestionario_NeedDataSource" AllowSorting="true" OnItemDataBound="grdPreguntasCuestionario_ItemDataBound">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="true" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView DataKeyNames="ID_PREGUNTA" ClientDataKeyNames="ID_PREGUNTA" AllowPaging="true">
                                                <Columns>
                                                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" HeaderText="Dimensión" DataField="NB_DIMENSION" UniqueName="NB_DIMENSION" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="110" HeaderText="Tema" DataField="NB_TEMA" UniqueName="NB_TEMA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" HeaderText="Pregunta" DataField="NB_PREGUNTA" UniqueName="NB_PREGUNTA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="50" HeaderText="Secuencia" DataField="NO_SECUENCIA" UniqueName="NO_SECUENCIA" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </telerik:RadPane>
                                    <telerik:RadPane ID="rpHelpAyuda2" runat="server" Width="30">
                                        <telerik:RadSlidingZone ID="RadSlidingZone2" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                            <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Title="Ayuda" Width="300" MinWidth="500" Height="100%">
                                                <div style="padding: 10px; text-align: justify;">
                               
                                                        En esta parte puedes crear tu cuestionario, agregar preguntas, editarlas o eliminarlas.
                                                        <br />   
                                                        Recuerda que si tu cuestionario es predefinido de Sigein no podrás editar ni eliminar la validez de las preguntas, sólo de aquellas que se hayan agregado como nuevas.  
                                                        <br />
                                                        Si deseas eliminar la validez del cuestionario completo solo deberás seleccionar el botón "Eliminar validez".                              
                                                        <br />
                                                        <br />
                                                        Una vez creados los cuestionarios ya no será posible realizar ninguna modificación.               

                                                </div>
                                            </telerik:RadSlidingPane>
                                        </telerik:RadSlidingZone>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </div>
                            <div style="height: 10px; clear: both;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnAgregarPregunta" runat="server" Text="Agregar" OnClientClicked="AbrirVentanaCuestionario"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnEditar" runat="server" Text="Editar" OnClientClicking="EditarPregunta"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnEliminar" runat="server" OnClientClicking="ConfirmEliminaPregunta" OnClick="btnEliminar_Click" Text="Eliminar"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnValidez" runat="server" OnClick="btnValidez_Click" OnClientClicking="ConfirmValidez" Text="Eliminar validez"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCrearCuestionarios" runat="server" Text="Crear cuestionarios" ToolTip="Esta opción te permite asignar los cuestionarios a los evaluadores seleccionados, una vez realizado este proceso no se podrá rediseñar el cuestionario." OnClientClicking="ConfirmarAsignar" OnClick="btnCrearCuestionarios_Click"></telerik:RadButton>
                            </div>
                            <div class="divControlesBoton">
                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnGuardarCuestionarios" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <label style="color: red;" runat="server" id="divMensajeCuestionarios">No se puede modificar las preguntas por que ya existen cuestionarios creados.</label>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvMensaje" runat="server">
                            <div style="height: calc(100% - 50px);">
                                <telerik:RadSplitter runat="server" ID="RadSplitter1" Width="100%" Height="100%" BorderSize="0">
                                    <telerik:RadPane ID="RadPane1" runat="server">
                                        <div class="ctrlBasico" style="padding: 10px; text-align: justify; height:100%; width:50%;">
                                            <fieldset>
                                                <legend>
                                                    <label>Mensaje envio cuestionarios:</label></legend>
                                                <telerik:RadEditor ID="lMensaje" runat="server" Width="100%" Height="100%" EditModes="Design" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                                            </fieldset>
                                           <%--<div style="height: 10px; clear: both;"></div>
                                            <div class="divControlDerecha">
                                                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" AutoPostBack="true" OnClick="btnGuardar_Click"></telerik:RadButton>
                                            </div>--%>
                                        </div>
                                        <div class="ctrlBasico" style="padding: 10px; text-align: justify; height:100%; width:50%;">
                                            <fieldset>
                                                <legend>
                                                    <label>Instrucciones cuestionario impreso:</label></legend>
                                                <telerik:RadEditor ID="reInstrucciones" runat="server" Width="100%" Height="100%" EditModes="Design" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                                            </fieldset>
                                             <%-- <div style="height: 10px; clear: both;"></div>
                                             <div class="divControlDerecha">
                                                <telerik:RadButton ID="btnInstrucciones" runat="server" Text="Guardar" AutoPostBack="true" OnClick="btnInstrucciones_Click"></telerik:RadButton>
                                            </div>--%>
                                        </div>
                                    </telerik:RadPane>
                                    <telerik:RadPane ID="RadPane2" runat="server" Width="30">
                                        <telerik:RadSlidingZone ID="RadSlidingZone3" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                            <telerik:RadSlidingPane ID="RadSlidingPane2" runat="server" Title="Ayuda" Width="300" MinWidth="500" Height="100%">
                                                <div style="padding: 10px; text-align: justify;">
      
                                                        En esta parte puedes configurar el mensaje para el cuestionario impreso del período y el mensaje de envío de los cuestionarios de clima laboral.<br />
                                                        <br />
                                                        En el mensaje de envío de cuestionarios no deberán eliminarse o modificarse las palabras entre corchetes ([EVALUADOR], [CONTRASENA]), así como la palabra "aquí" ya que estos se remplazan por los datos de cada evaluador. 
                                                </div>
                                            </telerik:RadSlidingPane>
                                        </telerik:RadSlidingZone>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </div>
                            <div style="height: 10px; clear: both;"></div>
                            <div class="divControlDerecha" style="padding-right:30px;">
                                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" UseSubmitBehavior="false" AutoPostBack="true" OnClick="btnGuardar_Click"></telerik:RadButton>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvEmpleadoContrasenia" runat="server">
                            <div style="clear: both;"></div>
                            <div style="height: calc(100% - 30px);" class="ctrlBasico">
                                <telerik:RadGrid ID="grdEmpleadosContrasenias" runat="server" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" AllowMultiRowSelection="true" Height="100%" OnNeedDataSource="grdEmpleadosContrasenias_NeedDataSource" OnItemDataBound="grdEmpleadosContrasenias_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                        <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView DataKeyNames="ID_EVALUADOR" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre completo" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Contraseña" DataField="CL_TOKEN" UniqueName="CL_TOKEN" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both;"></div>
                            <%-- <telerik:RadButton ID="btnReasignarTodasContrasenas" runat="server" Text="Reasignar a todos" OnClick="btnReasignarTodasContrasenas_Click"></telerik:RadButton>--%>
                            <telerik:RadButton ID="btnReasignarContrasena" runat="server" Text="Reasignar contraseña al evaluador seleccionado" OnClick="btnReasignarContrasena_Click"></telerik:RadButton>
                            <telerik:RadButton ID="btnEnvioCuestionarios" runat="server" Text="Enviar cuestionarios" Enabled="false" OnClientClicking="OpenEnvioSolicitudesWindow"></telerik:RadButton>
                            <div class="divControlesBoton">
                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnGuardarCerrar" Text="Guardar y Cerrar" OnClick="btnGuardarCerrar_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
          <%--  </telerik:RadPane>
            <telerik:RadPane ID="RadPane2" runat="server" Width="30">
                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="rspMensaje" runat="server" Title="Ayuda" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                            <fieldset>
                                <legend>
                                    <label>Preguntas abiertas:</label>
                                </legend>
                                Las preguntas abiertas ayudan a solicitar respuestas libres al evaluador. Al permitir texto libre en las respuestas, estas pueden ser variadas 
                                ya que reflejan la opinión personal de cada uno.<br />
                                <br />
                                En la pestaña "Preguntas abiertas" puedes crearlas añadiendo la pregunta y una descripción de la misma. Dicha descripción aparecerá como tooltip en cada pregunta. 
                                Las preguntas abiertas se podrán editar y eliminar. Al terminar de diseñarlas deberás dar click al botón "Asignar preguntas" mismas que 
                                se agregaran al cuestionario original y se asignaran a los evaluadores.
                                <br />
                                <br />
                                Después de dicha acción ya no se podrán editar, eliminar o crear nuevas preguntas. 
                                (Recuerda que para asignar las preguntas abiertas deberás haber asignado el cuestionario anteriormente).
                                
                            </fieldset>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>--%>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
