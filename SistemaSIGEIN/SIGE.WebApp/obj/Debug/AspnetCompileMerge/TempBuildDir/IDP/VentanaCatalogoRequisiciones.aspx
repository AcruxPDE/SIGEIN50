<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoRequisiciones.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCatalogoRequisiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="modal" type="text/javascript">

        var fe_requerimiento = "";
        var fe_solicitud = "";
        var vIdPuesto = 0;
        var vFgNuevoPuesto = 0;
        var vFgPuestoNuevoCreado = false;

        function GetRadWindow() {
            var oWindow = null;
            if 
                (window.radWindow) oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;

            return oWindow;
        }

        function closeWindowN(sender, args) {

            var vIdRequisicion = '<%# vIdRequisicion %>';
            var comboCausa = $find("<%# cmbCausas.ClientID %>");

            var cmbSelec = comboCausa.get_selectedItem();

            if (cmbSelec != null)
            var item = comboCausa.get_selectedItem().get_text();

            if (item == "Nuevo puesto" & vIdRequisicion == '') {

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });
                radconfirm('Si cancelas el proceso, se eliminará el nuevo puesto que se haya creado.', callBackFunction, 400, 200, null, "Salir");
                args.set_cancel(true);
            }
            else {
                closeWindow();
            }
        }

        function validacionRevisarPuesto(sender, args) {
            if (vFgPuestoNuevoCreado != true) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                {
                    if (!shouldSubmit) {
                        this.click();
                    }
                    else {
                        openComparacionperfilPuestosWindow();
                    }
                });
                radconfirm('El descriptivo de puesto es la base de la requisición, ¿Deseas revisar el descriptivo antes de guardar la requisición?', callBackFunction, 400, 200, null, "Salir");
                args.set_cancel(true);

            }

        }

        function closeWindow2() {
            GetRadWindow().close();
        }

        function closeWindow() {
            GetRadWindow().close();
        }

        function OnDateSelectedFe_solicitud(sender, eventArgs) {

            var date1 = sender.get_selectedDate();
            if (date1 != null) {
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= Fe_solicitud.ClientID %>");
                datepicker.set_maxDate(date1);
            }
        }

        function OnDateSelectedFe_requerimiento(sender, eventArgs) {
            var date1 = sender.get_selectedDate();
            if (date1 != null) {
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= Fe_Requerimiento.ClientID %>");
                datepicker.set_maxDate(date1);
            }
        }


        function GetPuestoID() {
            var list = $find("<%= rlbPuesto.ClientID %>");
            var items = list.get_items();
            var firstItem = items.getItem(0);

            if (firstItem != null) {
                vIdPuesto = firstItem.get_value();
            }
            else {
                vIdPuesto = 0;
            }
        }

        function OpenNotificateWindow() {

            vFgNuevoPuesto = 2;

            GetPuestoID();

            var vURL = "../IDP/Requisicion/VentanaDescriptivoPuestoRequisicion.aspx?vCrearAutorizar=Crear";
            var vTitulo = "Agregar descripción del puesto";


            if (vIdPuesto != 0) {
                vURL = vURL + "&PuestoId=" + vIdPuesto;
                vTitulo = "Editar descripción del puesto";
            }

            OpenSelectionWindowVistaPrevia(vURL, "winNotificarRequisicion", vTitulo);
        }

        function OpenSeleccionarEmpleadoAutoriza() {
            OpenWindow(GetSeleccionarEmpleadoAutoriza());
            //openChildDialog("../Comunes/SeleccionEmpleado.aspx?mulSel=0", "winSeleccionEmpleados", "Selección de empleados")
        }


        function GetSeleccionarEmpleadoAutoriza() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de empleados";
            wnd.vURL = "../Comunes/SeleccionEmpleado.aspx?mulSel=0";
            wnd.vRadWindowId = "winSeleccionEmpleados";
            return wnd;
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 100,
                height: document.documentElement.clientHeight - 10
            };
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }


        function GetVentanaSeleccionarEmpleadosSuplente() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de empleado a suplir";
            wnd.vURL = "../Comunes/SeleccionEmpleado.aspx?CatalogoCl=SUPLENTE&mulSel=0&CL_ORIGEN=REQUISICION";
            wnd.vRadWindowId = "winSeleccionEmpleados";
            return wnd;
        }

        function VentanaSeleccionarEmpleadosSuplente() {
            OpenWindow(GetVentanaSeleccionarEmpleadosSuplente());
            //openChildDialog("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=SUPLENTE&mulSel=0", "winSeleccionEmpleados", "Selección de empleado a suplir")
        }


        function GetSeleccionarEmpleadoSolicitante() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de solicitante";
            wnd.vURL = "../Comunes/SeleccionEmpleado.aspx?CatalogoCl=SOLICITANTE&mulSel=0";
            wnd.vRadWindowId = "winSeleccionEmpleados";
            return wnd;
        }

        function OpenSeleccionarEmpleadoSolicitante() {
            OpenWindow(GetSeleccionarEmpleadoSolicitante());
            //openChildDialog("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=SOLICITANTE&mulSel=0", "winSeleccionEmpleados", "Selección de solicitante")
        }

        function GetSeleccionarEmpleadoAutorizaPuesto() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de empleado";
            wnd.vURL = "../Comunes/SeleccionEmpleado.aspx?CatalogoCl=AUTO_PUESTO&mulSel=0";
            wnd.vRadWindowId = "winSeleccionEmpleados";
            return wnd;
        }

        function OpenSeleccionarEmpleadoAutorizaPuesto() {
            OpenWindow(GetSeleccionarEmpleadoAutorizaPuesto());
            //openChildDialog("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=AUTO_PUESTO&mulSel=0", "winSeleccionEmpleados", "Selección de empleado")
        }

        function useDataFromChild(pDato) {
            var list;
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {
                    case "NOTIFICACION":
                        var vid_Notificacion = pDato[0].idNotificacion;
                        InsertDato(vid_Notificacion);
                        break;
                    case "EMPLEADO":
                        var vEmpleadoSeleccionado = pDato[0];
                        var vLstEmpleados = $find("<%=lstAutoriza.ClientID %>");
                        var txtPuestoAuto = $find('<%= txtPuestoAutoriza.ClientID %>');
                        var txtCorreoAuto = $find('<%=txtCorreoAutorizaReq.ClientID %>');

                        SetListBoxItem(vLstEmpleados, vEmpleadoSeleccionado.nbEmpleado, vEmpleadoSeleccionado.idEmpleado);
                        txtPuestoAuto.set_value(vEmpleadoSeleccionado.nbPuesto);
                        txtCorreoAuto.set_value(vEmpleadoSeleccionado.nbCorreoElectronico);

                        break;
                    case "PUESTO":
                        var comboCausa = $find("<%# cmbCausas.ClientID %>");

                        var txtSueldo = $find("<%# txtSueldo.ClientID %>");
                        var txtSueldoSugerido = $find("<%# txtSueldoSugerido.ClientID %>");

                        if (comboCausa.get_selectedItem() != null) {

                            var vListPuesto = $find("<%=rlbPuesto.ClientID %>");
                            SetListBoxItem(vListPuesto, pDato[0].nbPuesto, pDato[0].idPuesto);

                            if(vFgNuevoPuesto == 1){
                                var vBtn = $find("<%= btnNuevoPuesto.ClientID %>");
                                vBtn.set_enabled(false);
                            }
                            <%--if (vFgNuevoPuesto == 2) {
                                var vBtn =  $find("<%= radBtnBuscarPuesto.ClientID %>");
                                vBtn.set_enabled(false); 
                            }--%>

                            var item = comboCausa.get_selectedItem().get_text();

                            if (item != "Vacante") {
                                txtSueldo.set_value(pDato[0].sueldo_ultimo);
                            }

                            txtSueldoSugerido.set_value(pDato[0].sueldo_tabulador);

                        }
                        else {
                            radalert("Selecciona una causa", 450, 150, "Seleccionar puesto");
                        }

                        vFgNuevoPuesto = 0;

                        break;
                    case "SUPLENTE":

                        var txtSueldo = $find("<%# txtSueldo.ClientID %>");
                        var txtSueldoSugerido = $find("<%# txtSueldoSugerido.ClientID %>");
                        var comboCausa = $find("<%# cmbCausas.ClientID %>");
                        var vEmpleadoSeleccionado = pDato[0];

                        var btnBuscarPuesto = $find("<%# radBtnBuscarPuesto.ClientID %>");
                        var btnEliminarPuesto = $find("<%# btnEliminarPuestoObjetivo.ClientID %>");

                        //Agregamos el empleado al radlist
                        var vListSuplente = $find("<%=rlbSuplente.ClientID %>");
                        SetListBoxItem(vListSuplente, vEmpleadoSeleccionado.nbEmpleado, vEmpleadoSeleccionado.idEmpleado);

                        //Agregamos el puesto del empleado suplente
                        var vLstPuesto = $find("<%=rlbPuesto.ClientID %>");
                        SetListBoxItem(vLstPuesto, vEmpleadoSeleccionado.nbPuesto, vEmpleadoSeleccionado.idPuesto);

                        //EnviarPuesto(vEmpleadoSeleccionado.idPuesto);

                        var item = comboCausa.get_selectedItem().get_value();

                        //Inhabilitar botones para que no seleccione nada
                        btnBuscarPuesto.set_enabled(false);
                        btnEliminarPuesto.set_enabled(false);


                        //if (item == "Vacante" || item ==) {
                        if (item == "VACANTE" || item == "SUPLENTE") {
                            txtSueldo.set_value(pDato[0].sueldo_ultimo);
                        }

                        if (item == "SUPLENCIA") {
                            txtSueldo.set_value(pDato[0].sueldo_ultimo);
                        }

                        break;
                    case "PUESTO_REQUISICION":

                        var vListPuesto = $find("<%=rlbPuesto.ClientID %>");
                        var btnBuscarPuesto = $find("<%# radBtnBuscarPuesto.ClientID %>");
                        var btnEliminarPuesto = $find("<%# btnEliminarPuestoObjetivo.ClientID %>");
                        var btnVistaPrevia = $find("<%# btnVistaPrevia.ClientID %>");
                        var btnNuevoPuesto = $find("<%# btnNuevoPuesto.ClientID %>");
                        //var divAutorizaPuestoReq = $find("# divAutorizaPuestoReq.ClientID %>");


                        SetListBoxItem(vListPuesto, pDato[0].nbPuesto, pDato[0].idPuesto);

                        radalert("Proceso Exitoso. Recuerde que este descriptivo no aparecerá en el catálogo hasta que se autorice, solo podrá entrar desde está requisición.", 450, 200, "Descriptivo guardado");

                        btnBuscarPuesto.set_enabled(false);
                        btnEliminarPuesto.set_enabled(false);
                        btnVistaPrevia.set_enabled(false);

                        btnNuevoPuesto.set_text("Editar nuevo puesto");

                        document.getElementById("<%= divAutorizaPuestoReq.ClientID %>").style.display = "block";

                        //divAutorizaPuestoReq.setAttribute("display", "block");
                        vFgPuestoNuevoCreado = true;
                        break;

                    case "SOLICITANTE":

                        var vListaSolicitante = $find('<%=rlbSolicitante.ClientID %>');
                        var txtPuestoSol = $find('<%= txtPuestoSolicitante.ClientID %>');
                        var txtCorreoSolicitante = $find('<%= txtCorreoSolicitante.ClientID %>');

                        SetListBoxItem(vListaSolicitante, pDato[0].nbEmpleado, pDato[0].idEmpleado);
                        txtPuestoSol.set_value(pDato[0].nbPuesto);
                        txtCorreoSolicitante.set_value(pDato[0].nbCorreoElectronico);

                        break;

                    case "AUTO_PUESTO":
                        var vTxtAutorizaPuesto = $find('<%=txtPuestoReq.ClientID %>');
                        var txtPuestoAutoPuesto = $find('<%= txtPuestoAutorizaPuesto.ClientID %>');
                        var txtPuestoAutorizaCorreo = $find('<%= txtPuestoAutorizaCorreo.ClientID %>');

                        //SetListBoxItem(vListaAutorizaPuesto, pDato[0].nbEmpleado, pDato[0].idEmpleado);
                        vTxtAutorizaPuesto.set_value(pDato[0].nbEmpleado);
                        txtPuestoAutoPuesto.set_value(pDato[0].nbPuesto);
                        txtPuestoAutorizaCorreo.set_value(pDato[0].nbCorreoElectronico);
                        break;
                }
            }
        }

        function InsertDato(pDato) {
            var ajaxManager = $find('<%= ramRequisiciones.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function EnviarPuesto(pDato) {
            var ajaxManager = $find('<%= ramRequisiciones.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function OpenSelectionWindow() {
            OpenWindow(GetSelectionWindow())
            //openChildDialog("/Comunes/SeleccionPuesto.aspx?mulSel=0", "winSeleccion", "Selección de puesto");
        }

        function GetSelectionWindow() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de puesto";
            wnd.vURL = "../Comunes/SeleccionPuesto.aspx?mulSel=0";
            wnd.vRadWindowId = "winSeleccionEmpleados";
            return wnd;
        }



        function OpenPuestoExistente() {
            vFgNuevoPuesto = 1;
            OpenSelectionWindow();
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

        function CleanSelectionSolicitante(sender, args) {
            var vListSolicitante = $find("<%=rlbSolicitante.ClientID %>");
            var txtPuestoSol = $find('<%= txtPuestoSolicitante.ClientID %>');
            var txtCorreoPuestoSol = $find('<%= txtCorreoSolicitante.ClientID %>');

            SetListBoxItem(vListSolicitante, "No Seleccionado", "");
            txtPuestoSol.set_value("");
            txtCorreoPuestoSol.set_value("");
        }

        function CleanSelectionAutoriza(sender, args) {
            var vListAutoriza = $find("<%=lstAutoriza.ClientID %>");
            var txtPuestoAuto = $find('<%= txtPuestoAutoriza.ClientID %>');
            var txtCorreoAutorizaReq = $find('<%= txtCorreoAutorizaReq.ClientID %>');

            SetListBoxItem(vListAutoriza, "No Seleccionado", "");
            txtPuestoAuto.set_value("");
            txtCorreoAutorizaReq.set_value("");
        }

        function CleanPuestoSelection(sender, args) {
            var vListPuesto = $find("<%=rlbPuesto.ClientID %>");
            SetListBoxItem(vListPuesto, "No Seleccionado", "0");

            var txtSueldo = $find("<%# txtSueldo.ClientID %>");
            txtSueldo.set_value("0.00");

            var txtSueldoSugerido = $find("<%# txtSueldoSugerido.ClientID %>");
            txtSueldoSugerido.set_value("0.00");

            var vBtn = $find("<%= btnNuevoPuesto.ClientID %>");
            vBtn.set_enabled(true);

            var vBtn =  $find("<%= radBtnBuscarPuesto.ClientID %>");
            vBtn.set_enabled(true);

        }

        function CleanSelectionSuplente(sender, args) {
            var vListSuplente = $find("<%=rlbSuplente.ClientID %>");
            var comboCausa = $find("<%# cmbCausas.ClientID %>");
            var vListPuesto = $find("<%# rlbPuesto.ClientID %>");
            SetListBoxItem(vListSuplente, "No Seleccionado", "");
            SetListBoxItem(vListPuesto, "No Seleccionado", "0");
            var txtSueldo = $find('<%# txtSueldo.ClientID %>');
            txtSueldo.set_value("0.00");

            var item = comboCausa.get_selectedItem().get_text();

            if (item == "Vacante") {

                var txtSueldoSugerido = $find('<%# txtSueldoSugerido.ClientID %>');
                txtSueldoSugerido.set_value("0.00");

            }
        }

        function CleanSelectionAutorizaPuesto(sender, args) {
            //var vListSuplente = $find("=rlbPuestoReq.ClientID %>");
            var txtPuestoReq = $find("<%= txtPuestoReq.ClientID %>");
            var txtAutorizaPuesto = $find('<%= txtPuestoAutorizaPuesto.ClientID %>');
            var txtCorreoAutorizaPuesto = $find('<%= txtPuestoAutorizaCorreo.ClientID %>');
            //SetListBoxItem(rlbPuestoReq, "No Seleccionado", "");
            txtPuestoReq.set_value("");
            txtAutorizaPuesto.set_value("");
            txtCorreoAutorizaPuesto.set_value("");
        }


        //Eliminar el tooltip del control
        function pageLoad() {
            var datePicker = $find("<%=rdpAutorizacion.ClientID %>");
            if (datePicker != null)
                datePicker.get_popupButton().title = "";

            var datePicker2 = $find("<%=Fe_Requerimiento.ClientID %>");
            datePicker2.get_popupButton().title = "";
            var datePicker3 = $find("<%=Fe_solicitud.ClientID %>");
            datePicker3.get_popupButton().title = "";
        }



        
        function openComparacionperfilPuestosWindow() {

            GetPuestoID();

            if (vIdPuesto != 0) {

                OpenSelectionWindowVistaPrevia("../Administracion/VentanaDescriptivoPuesto.aspx?PuestoId=" + vIdPuesto, "winPerfil", "Revisar descriptivo de puesto")
            }
            else {
                radalert("Selecciona un puesto para revisar el descriptivo", 450, 150, "Revisar descriptivo");
            }
        }

        function OpenSelectionWindowVistaPrevia(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 60,
                height: browserWnd.innerHeight - 60
            };

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpRequisiciones" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramRequisiciones" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" DefaultLoadingPanelID="ralpRequisiciones">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbCausas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDescripcionCausa" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnNuevoPuesto" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <%--<telerik:AjaxUpdatedControl ControlID="rlbSuplente" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblEmpleadoSuplir" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarSuplente" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEliminaSuplente" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>--%>
                   <%-- <telerik:AjaxUpdatedControl ControlID="dvSueldos" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>--%>
                    <telerik:AjaxUpdatedControl ControlID="radBtnBuscarPuesto" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnVistaPrevia" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarPuestoObjetivo" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="divUltimoSueldo" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="dvSueldoSugerido" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="lblDescripcionCausa" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="lblPuesto" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="lblTiempoCausa" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbPuesto" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtTiempoCausa" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnReenviarAutorizaciones" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divAutorizaPuestoReq" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divEmpleadoSuplir" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtSueldoMin" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtSueldoMax" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbSolicitante" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPuestoSolicitante" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtCorreoSolicitante" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="lstAutoriza" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPuestoAutoriza" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtCorreoAutorizaReq" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtSueldo" UpdatePanelRenderMode="Inline" />

                    <%--<telerik:AjaxUpdatedControl ControlID="txtPuestoReq" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPuestoAutorizaPuesto" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPuestoAutorizaCorreo" UpdatePanelRenderMode="Inline" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNotificarRH">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNotificarAutoriza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarPuestoObjetivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnNuevoPuesto"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldo"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldoSugerido"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnReenviarAutorizaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCorreoAutorizaReq" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rlbPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtClPuesto" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldo" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldoSugerido" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarCatalogo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldo" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldoSugerido" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldoMin" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldoMax" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>

            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReenviarAutorizaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnReenviarAutorizaciones" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <div style="height: calc(100% - 50px);">
        <telerik:RadSplitter ID="rsSolicitud" Width="100%" Height="100%" BorderSize="0" runat="server">

            <telerik:RadPane ID="rpSolicitud" runat="server">

                <div style="clear: both; height: 10px;"></div>

                <div class="ctrlBasico" style="margin-left: 20px;">
                    <div>
                        <label id="Label8" name="lblNbIdioma" runat="server">Clave de requisición:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <telerik:RadTextBox ID="txtNo_requisicion" runat="server" Width="100px" MaxLength="100" ReadOnly="true" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>

                <div class="ctrlBasico" style="margin-left: 20px;">
                    <div>
                        <label id="Label1" name="lblNbIdioma" runat="server">Fecha de la requisición:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <telerik:RadDatePicker ID="Fe_solicitud" Width="150" runat="server" ClientEvents-OnDateSelected="OnDateSelectedFe_solicitud"></telerik:RadDatePicker>
                    </div>

                </div>

                <div class="ctrlBasico" style="margin-left: 20px;">
                    <div>
                        <label id="Label11" name="lblNbIdioma" runat="server">Fecha en la que se requiere:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <telerik:RadDatePicker ID="Fe_Requerimiento" Width="150px" ClientEvents-OnDateSelected="OnDateSelectedFe_requerimiento" runat="server">
                            <Calendar ID="Calendar2" runat="server"></Calendar>
                        </telerik:RadDatePicker>
                    </div>
                </div>

                <div class="ctrlBasico" style="margin-left: 20px;">
                    <div>
                        <label id="lbAutoriza" name="lbAutoriza" runat="server" visible="false">Fecha en la que se autoriza:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <telerik:RadDatePicker ID="rdpAutorizacion" Width="150px" runat="server" Visible="false" Enabled="false">
                            <Calendar ID="Calendar1" runat="server"></Calendar>
                        </telerik:RadDatePicker>
                    </div>
                </div>

                <div style="clear: both; height: 10px;"></div>

                <div class="ctrlBasico" style="margin-left: 20px;">
                    <div>
                        <label id="Label6" name="lblNbIdioma" runat="server">Causas:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <telerik:RadComboBox
                            Filter="Contains" runat="server" ID="cmbCausas" Width="200" MarkFirstMatch="true" AutoPostBack="true" EmptyMessage="Seleccione..." DropDownWidth="330" ValidationGroup="ValcmbCausas" OnSelectedIndexChanged="cmbCausas_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                </div>

                <div class="ctrlBasico" style="margin-left: 20px;">
                    <div>
                        <label id="lblDescripcionCausa" name="lblNbIdioma" runat="server">Causa de otra:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <telerik:RadTextBox ID="txtDescripcionCausa" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>

                <div class="ctrlBasico" style="margin-left: 20px;">
                    <div>
                        <label id="lblTiempoCausa" name="lblNbIdioma" runat="server">Tiempo requerido:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <telerik:RadTextBox ID="txtTiempoCausa" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>

                <div style="clear: both; height: 10px;"></div>

                <div class="ctrlBasico" runat="server" id="divEmpleadoSuplir" style="margin-left: 20px; ">

                    <div>
                        <label id="lblEmpleadoSuplir" name="" runat="server"></label>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <telerik:RadListBox ID="rlbSuplente" runat="server" Width="300" Height="34" OnClientItemDoubleClicking="OpenSelectionWindow" AutoPostBack="true">
                            <Items>
                                <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                            </Items>
                        </telerik:RadListBox>
                        <telerik:RadButton ID="btnBuscarSuplente" OnClientClicked="VentanaSeleccionarEmpleadosSuplente" AutoPostBack="true" runat="server" Text="B"></telerik:RadButton>
                        <telerik:RadButton ID="btnEliminaSuplente" runat="server" Text="X" AutoPostBack="true" OnClientClicked="CleanSelectionSuplente"></telerik:RadButton>
                    </div>
                </div>

                <%--<div style="clear: both; height: 10px;"></div>--%>

                <div class="ctrlBasico" runat="server" id="divPuestoCubrir" style="margin-left: 20px; ">
                    <div>
                        <label id="lblPuesto" name="lblNbIdioma" runat="server">Puesto a cubrir:</label>
                    </div>

                    <div style="clear: both;"></div>

                    <div style="width: 100%;">
                        <telerik:RadListBox ID="rlbPuesto" runat="server" Width="300" Height="34" AutoPostBack="true">
                            <Items>
                                <telerik:RadListBoxItem Text="No Seleccionado" Value="0" />
                            </Items>
                        </telerik:RadListBox>
                        <%--<telerik:RadButton ID="radBtnBuscarPuesto" OnClientClicked="OpenSelectionWindow" AutoPostBack="true" runat="server" Text="B"></telerik:RadButton>--%>
                        <telerik:RadButton ID="radBtnBuscarPuesto" OnClientClicked="OpenPuestoExistente" AutoPostBack="true" runat="server" Text="B"></telerik:RadButton>
                        <telerik:RadButton ID="btnEliminarPuestoObjetivo" runat="server" Text="X" AutoPostBack="true" OnClientClicked="CleanPuestoSelection"></telerik:RadButton>
                        <telerik:RadButton ID="btnVistaPrevia" runat="server" Text="V" AutoPostBack="false" OnClientClicked="openComparacionperfilPuestosWindow"></telerik:RadButton>
                        <telerik:RadButton ValidationGroup="btnNuevoPuesto" Visible="false" ID="btnNuevoPuesto" runat="server" Text="Crear nuevo puesto" OnClientClicked="OpenNotificateWindow" AutoPostBack="false"></telerik:RadButton>
                    </div>
                </div>

                <div style="clear: both; height: 5px;"></div>

                <%-- Sueldos --%>
                <div id="dvSueldos" class="ctrlBasico" runat="server">
                    <fieldset style="width: 500px; margin-left: 20px;" > 
                        <legend>
                            <label>&nbsp</label></legend>
                        <div class="ctrlBasico" runat="server" id="divUltimoSueldo">

                            <div class="divControlIzquierda">
                                <label id="lblUltimoSueldo" name="lblNbIdioma" runat="server">Último sueldo:</label>
                            </div>

                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox DisabledStyle-HorizontalAlign="Right" ID="txtSueldo" runat="server" Width="100px" MaxLength="1000" DataType="Decimal" Enabled="false" Value="0" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                            </div>
                        </div>

                        <div class="ctrlBasico" runat="server" id="dvSueldoSugerido">
                            <div class="divControlIzquierda">
                                <label id="Label2" name="lblNbIdioma" runat="server">Sueldo sugerido:</label>
                            </div>

                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox ID="txtSueldoSugerido" DisabledStyle-HorizontalAlign="Right" runat="server" Width="100px" DataType="Decimal" MaxLength="1000" Enabled="false" Value="0" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                            </div>
                        </div>

                    </fieldset>
                </div>
                <%--<div style="clear: both; height: 10px;"></div>--%>

                <div class="ctrlBasico">
                    <fieldset style="width: 500px; margin-left: 10px;">
                        <legend>
                            <label>Rango sugerido sueldo mensual</label></legend>

                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label3" name="lblNbIdioma" runat="server">Sueldo mínimo:</label>
                            </div>

                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox ID="txtSueldoMin" MinValue="0" DataType="Decimal" runat="server" Width="100px" MaxLength="13" NumberFormat-DecimalDigits="2">
                                    <EnabledStyle HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
                            </div>
                        </div>

                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label5" name="lblNbIdioma" runat="server">Sueldo máximo:</label>
                            </div>

                            <div class="divControlDerecha">

                                <telerik:RadNumericTextBox ID="txtSueldoMax" DisabledStyle-HorizontalAlign="Right" MinValue="0" DataType="Decimal" runat="server" Width="100px" MaxLength="13" NumberFormat-DecimalDigits="2">
                                    <EnabledStyle HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
                            </div>
                        </div>

                    </fieldset>
                </div>

                <%-- Solicitado por --%>
                <div style="clear: both; height: 5px;"></div>

                <fieldset style="width: 1020px; margin-left: 20px;">
                    <legend>
                        <label id="Label15" name="lblNbIdioma" runat="server">Solicitado por:</label></legend>

                    <div class="ctrlBasico" style="padding-left: 20px;">

                        <telerik:RadListBox ID="rlbSolicitante" runat="server" Width="300" Height="34">
                            <Items>
                                <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                            </Items>
                        </telerik:RadListBox>
                        <telerik:RadButton ID="btnSeleccionarSolicitante" runat="server" Text="B" ToolTip="Selecciona persona que solicita" OnClientClicked="OpenSeleccionarEmpleadoSolicitante" AutoPostBack="false"></telerik:RadButton>
                        <telerik:RadButton ID="btnBorrarSeleccionSolicitante" runat="server" Text="X" ToolTip="Eliminar persona que solicita" AutoPostBack="false" OnClientClicked="CleanSelectionSolicitante"></telerik:RadButton>

                        <telerik:RadTextBox runat="server" ID="txtPuestoSolicitante" Width="300px" Enabled="false"></telerik:RadTextBox>
                        <telerik:RadTextBox runat="server" ID="txtCorreoSolicitante" Width="300px"></telerik:RadTextBox>

                    </div>
                </fieldset>

                <div style="clear: both; height: 5px;"></div>

                <%-- Autoriza puesto nuevo --%>

                <div class="ctrlBasico" runat="server" id="divAutorizaPuestoReq">
                    <fieldset style="width: 1020px; margin-left: 20px;">
                        <legend>
                            <label id="Label4" name="lblNbIdioma" runat="server">Autoriza puesto nuevo:</label>
                        </legend>

                        <div class="ctrlBasico" style="padding-left: 20px;">
                            <%--<telerik:RadListBox ID="rlbPuestoReq" runat="server" Width="300" Height="34">
                                <Items>
                                    <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                                </Items>
                            </telerik:RadListBox>--%>
                            <telerik:RadTextBox runat="server" ID="txtPuestoReq" Width="300px"></telerik:RadTextBox>
                            <telerik:RadButton ID="btnAgregarPersonaAutorizaPuesto" runat="server" Text="B" ToolTip="Selecciona una persona para autorizar el puesto creado desde la requisición" OnClientClicked="OpenSeleccionarEmpleadoAutorizaPuesto" AutoPostBack="false"></telerik:RadButton>
                            <telerik:RadButton ID="btnBorrarSeleccionAutorizaPuesto" runat="server" Text="X" ToolTip="Quitar persona" AutoPostBack="false" OnClientClicked="CleanSelectionAutorizaPuesto"></telerik:RadButton>
                            <telerik:RadTextBox runat="server" ID="txtPuestoAutorizaPuesto" Width="300px"></telerik:RadTextBox>
                            <telerik:RadTextBox runat="server" ID="txtPuestoAutorizaCorreo" Width="300px"></telerik:RadTextBox>

                        </div>
                    </fieldset>
                </div>

                <div style="clear: both; height: 5px;"></div>


                <%-- Autoriza --%>

                <fieldset style="width: 1020px; margin-left: 20px;">
                    <legend>
                        <label id="Label10" name="lblNbIdioma" runat="server">Autoriza:</label>
                    </legend>

                    <div class="ctrlBasico" style="padding-left: 20px;">
                        <telerik:RadListBox ID="lstAutoriza" runat="server" Width="300" Height="34">
                            <Items>
                                <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                            </Items>
                        </telerik:RadListBox>
                        <telerik:RadButton ID="btnBuscarEmpleado" runat="server" Text="B" ToolTip="Selecciona una persona para autorizar la requisición" OnClientClicked="OpenSeleccionarEmpleadoAutoriza" AutoPostBack="false"></telerik:RadButton>
                        <telerik:RadButton ID="btnBorrarSeleccionAutoriza" runat="server" Text="X" ToolTip="Quitar persona" AutoPostBack="false" OnClientClicked="CleanSelectionAutoriza"></telerik:RadButton>
                        <telerik:RadTextBox runat="server" ID="txtPuestoAutoriza" Width="300px" Enabled="false"></telerik:RadTextBox>
                        <telerik:RadTextBox runat="server" ID="txtCorreoAutorizaReq" Width="300px" Enabled="true"></telerik:RadTextBox>

                    </div>
                </fieldset>

                <div style="clear: both; height: 5px;"></div>

                <%-- Observaciones --%>

                <fieldset style="width: 1020px; margin-left: 20px;">
                    <legend>
                        <label>Observaciones:</label></legend>
                    <telerik:RadEditor Style="margin-left: 5px; margin-right: 30px;" Height="100" Width="99%" ToolsWidth="500" EditModes="Design" ID="txtObservaciones" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml">
                    </telerik:RadEditor>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="Label17" name="lblNbIdioma" visible="false" runat="server">Vo. Bo. de RR HH:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtVistoBueno" runat="server" Visible="false" Width="300" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
                  </fieldset>

            </telerik:RadPane>

            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
                <telerik:RadSlidingZone ID="slzOpcionesBusqueda" runat="server" Width="18" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="RSPAdvSearchInstructores" runat="server" Title="Ayuda" Width="300" MinWidth="300" Height="100%">
                        <div style="padding: 20px; text-align: justify">
                            Te informamos que si el puesto a cubrir no está en el catálogo de puestos, deberás seleccionar como causa "Nuevo puesto" y crearlo para su autorización.
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>

            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnReenviarAutorizaciones" Text="Reenviar autorizaciones" UseSubmitBehavior="false" OnClick="btnReenviarAutorizaciones_Click" AutoPostBack="true" Visible="false"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardarCatalogo" runat="server" Width="100" Text="Guardar" OnClientClicking="validacionRevisarPuesto" OnClick="btnSave_click" AutoPostBack="true"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelarCatalogo" runat="server" Width="100" Text="Cancelar" AutoPostBack="true" OnClientClicking="closeWindowN" OnClick="btnCancelarCatalogo_Click"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
        <ConfirmTemplate>
            <div class="rwDialogPopup radconfirm">
                <div class="rwDialogText">
                    {1}
                </div>
                <div>
                    <a onclick="$find('{0}').close(true);" class="rwPopupButton" href="javascript:void(0);">
                        <span class="rwOuterSpan"><span class="rwInnerSpan">Sí</span></span></a>
                    <a onclick="$find('{0}').close(false);" class="rwPopupButton" href="javascript:void(0);">
                        <span class="rwOuterSpan"><span class="rwInnerSpan">No</span></span></a>
                </div>
            </div>
        </ConfirmTemplate>
    </telerik:RadWindowManager>
    <asp:HiddenField ID="hfRevisoDescriptivo" runat="server" />
</asp:Content>
