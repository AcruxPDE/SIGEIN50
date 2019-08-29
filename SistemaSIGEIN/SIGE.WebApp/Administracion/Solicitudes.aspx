<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="Solicitudes.aspx.cs" Inherits="SIGE.WebApp.Administracion.Solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script id="MyScript" type="text/javascript">

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                       || args.get_eventTarget().indexOf("ExportToWordButton") >= 0
                       || args.get_eventTarget().indexOf("ExportToCsvButton") >= 0
                       || args.get_eventTarget().indexOf("ChangePageSizeLabel") >= 0
                       || args.get_eventTarget().indexOf("PageSizeComboBox") >= 0
                       || args.get_eventTarget().indexOf("SaveChangesButton") >= 0
                       || args.get_eventTarget().indexOf("CancelChangesButton") >= 0
                       || args.get_eventTarget().indexOf("Download") >= 0
                       || (args.get_eventTarget().indexOf("Export") >= 0)
                       || (args.get_eventTarget().indexOf("DownloadPDF") >= 0)
                       || (args.get_eventTarget().indexOf("download_file") >= 0)
                    )
                    args.set_enableAjax(false);
            }

            var idSolicitud = "";
            var idBateria = "";
            var clEstatus = "";
            var clToken = "";
            var idRequisicion = "";
            var idCandidato = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdSolicitudes.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idSolicitud = SelectDataItem.getDataKeyValue("ID_SOLICITUD");
                    idBateria = SelectDataItem.getDataKeyValue("ID_BATERIA");
                    clEstatus = SelectDataItem.getDataKeyValue("K_SOLICITUD_CL_SOLICITUD_ESTATUS");
                    clToken = SelectDataItem.getDataKeyValue("CL_TOKEN");
                    idRequisicion = SelectDataItem.getDataKeyValue("ID_REQUISICION");
                    idCandidato = SelectDataItem.getDataKeyValue("ID_CANDIDATO");
                }
            }

            function openResultadoPruebas() {

                if (idBateria == null & clToken == null) {
                    radalert("El candidato no tiene bateria de pruebas.", 400, 150, "Aviso");
                    return;
                }

                if (idBateria == "" & clToken == "") {
                    radalert("El candidato no tiene bateria de pruebas.", 400, 150, "Aviso");
                    return;
                }

                var win = window.open("ResultadosPruebas.aspx?&ID=" + idBateria + "&T=" + clToken, '_blank');
                win.focus();
            }

            function openConsultas() {
                var vURL = "ConsultasPersonales.aspx";
                var vTitulo = "Consultas personales";
                obtenerIdFila();
                if (idBateria != null & idBateria != "null") {
                    vURL = vURL + "?&pIdBateria=" + idBateria;
                    var windowProperties = {};
                    windowProperties.width = document.documentElement.clientWidth - 20;
                    windowProperties.height = document.documentElement.clientHeight - 20;
                    var wnd = openChildDialog(vURL, "rwConsultas", vTitulo, windowProperties);
                }
                else
                {
                    radalert("El candidato no tiene bateria de pruebas.", 400, 150, "Aviso");
                    return;
                }
            }


            function showPopupConsultas() {
                obtenerIdFila();

                if (idBateria == "" & idSolicitud == "") {
                    radalert("No has seleccionado un registro.", 400, 150, "Aviso");
                }
                else if (idBateria != "" & idSolicitud != "") {
                    openConsultas();
                }
                else if (idBateria == "" & idSolicitud != "") {
                    radalert("Las pruebas del candidato no han sido contestadas.", 400, 150, "Aviso");
                }
            }


            function showPopupResultadoPruebas() {
                obtenerIdFila();

                if ((idBateria != "" & clToken != "")) {
                    openResultadoPruebas();
                } else if (idBateria == "" && idSolicitud != "") {
                    radalert("Las pruebas del candidato no han sido contestadas.", 400, 150, "Aviso");
                }
                else {
                    radalert("No has seleccionado un registro.", 400, 150, "Aviso");
                }
            }

            function onCloseWindow(oWnd, args) {

                idSolicitud = "";
                idBateria = "";
                clEstatus = "";
                clToken = "";
                idRequisicion = "";

                $find("<%=grdSolicitudes.ClientID%>").get_masterTableView().rebind();
            }

            function ShowEditForm() {
                var selectedItem = $find("<%=grdSolicitudes.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindow(selectedItem.getDataKeyValue("ID_SOLICITUD"));
                else
                    radalert("Selecciona una solicitud.", 400, 150, "Aviso");
            }

            function ShowConsultForm() {
                var selectedItem = $find("<%=grdSolicitudes.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindowConsult(selectedItem.getDataKeyValue("ID_SOLICITUD"));
                else
                    radalert("Selecciona una solicitud.", 400, 150, "Aviso");
            }

            function ShowInsertForm() {
                OpenWindow(null);
            }

            function OpenImpresion() {
                idSolicitud = "";
                obtenerIdFila();
                if (idSolicitud != null && idSolicitud != "") {
                    openChildDialog("../IDP/VentanaImprimirSolicitud.aspx?SolicitudId=" + idSolicitud , "winImprimir", "Imprimir solicitud candidato");
                }
                else radalert("Selecciona una solicitud.", 400, 150, "Aviso");
                //var myWindow = window.open("../VentanaImprimirSolicitud.aspx?SolicitudId=" + '<= vIdSolicitudVS >', "MsgWindow", "width=650,height=650");
             }

            function OpenWindow(pIdSolicitud) {
                var vURL = "../IDP/Solicitud/Solicitud.aspx";
                var vTitulo = "Agregar solicitud";
                if (pIdSolicitud != null) {
                    vURL = vURL + "?SolicitudId=" + pIdSolicitud;
                    vTitulo = "Editar solicitud";
                }
                var windowProperties = {};
                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog(vURL, "winSolicitud", vTitulo, windowProperties);
            }

            function OpenWindowConsult(pIdSolicitud) {
                var vURL = "../IDP/Solicitud/Solicitud.aspx" + "?SolicitudId=" + pIdSolicitud + "&" + "Action=" + "Consult";
                var vTitulo = "Consultar solicitud";
                var windowProperties = {};

                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog(vURL, "winSolicitud", vTitulo, windowProperties);
            }

            function ConfirmarEliminar(sender, args) {
                var masterTable = $find("<%=grdSolicitudes.ClientID %>").get_masterTableView();
                var selectedItem = masterTable.get_selectedItems()[0];
                if (selectedItem != undefined) {
                    //var vClSolicitud = masterTable.getCellByColumnUniqueName(selectedItem, "K_SOLICITUD_CL_SOLICITUD").innerHTML;
                    //vClSolicitud = ((vClSolicitud == "&nbsp;") ? "" : " " + vClSolicitud);
                    //confirmAction(sender, args, '¿Deseas eliminar la solicitud' + vClSolicitud + '?, este proceso no podrá revertirse.');

                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                    radconfirm("¿Deseas eliminar las solicitudes seleccionadas?, este proceso no podrá revertirse.", callBackFunction, 400, 170, null, "Aviso");
                    args.set_cancel(true);

                } else {
                    radalert("Seleccione una solicitud.", 400, 200, "Aviso");
                    args.set_cancel(true);
                }
            }

            function generateDataForParent() {
                var info = null;
                var vSolicitudes = [];
                var masterTable = $find("<%= grdSolicitudes.ClientID %>").get_masterTableView();
                var selectedItems = masterTable.get_selectedItems();
                if (selectedItems.length > 0) {
                    for (i = 0; i < selectedItems.length; i++) {
                        var vEmpleado = {};
                        selectedItem = selectedItems[i];
                        vEmpleado.idEmpleado = selectedItem.getDataKeyValue("ID_SOLICITUD");
                        vEmpleado.clEmpleado = masterTable.getCellByColumnUniqueName(selectedItem, "ID_EMPLEADO").innerHTML;
                        vEmpleado.nbCorreoElectronico = masterTable.getCellByColumnUniqueName(selectedItem, "ID_DESCRIPTIVO").innerHTML;
                        vEmpleado.nbEmpleado = masterTable.getCellByColumnUniqueName(selectedItem, "ID_REQUISICION").innerHTML;
                        vSolicitudes.push(vEmpleado);
                    }

                    sendDataToParent(vSolicitudes);
                }
                else {
                    var currentWnd = GetRadWindow();
                    var browserWnd = window;
                    if (currentWnd)
                        browserWnd = currentWnd.BrowserWindow;
                    browserWnd.radalert("Selecciona un empleado.", 400, 150, "Aviso");
                }
            }

            function useDataFromChild(pEmpleados) {
                $find("<%= grdSolicitudes.ClientID%>").get_masterTableView().rebind();
            }

            function cancelarSeleccion() {
                sendDataToParent(null);
            }

            function ConfirmarActualizar(sender, args) {
                var masterTable = $find("<%=grdSolicitudes.ClientID %>").get_masterTableView();
                var selectedItem = masterTable.get_selectedItems()[0];
                if (selectedItem != undefined) {
                   var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                    radconfirm("¿Deseas actualizar las solicitudes seleccionadas?", callBackFunction, 400, 170, null, "Aviso");
                    args.set_cancel(true);

                } else {
                    radalert("Seleccione una solicitud.", 400, 200, "Aviso");
                    args.set_cancel(true);
                }
            }

            function OpenActualizacionCartera() {
                var win = window.open("ActualizacionCartera.aspx", '_self', true);
                win.focus();
            }

            function ShowContratarForm() {

                obtenerIdFila();

                if (idSolicitud == "") {
                    radalert("Selecciona una solicitud.", 400, 150, "Aviso");
                    return;
                }

                if (clEstatus == "Contratado") {
                    radalert("El candidato de la solicitud ya está contratado. Selecciona otra solicitud.", 400, 150, "Aviso");
                    return;
                }

                OpenContratarWindow(idSolicitud);
            }

            function OpenContratarWindow(pIdSolicitud) {
                var vURL = "VentanaContratarCandidato.aspx";
                var vTitulo = "Contratar candidato";
                vURL = vURL + "?SolicitudId=" + pIdSolicitud;

                var windowProperties = {};
                windowProperties.width = 900;
                windowProperties.height = 400;
                openChildDialog(vURL, "winContratar", vTitulo, windowProperties);
            }

            function OpenProcesoSeleccionWindow() {
                var vURL = "VentanaProcesoSeleccionCandidato.aspx";
                var vTitulo = "Proceso de evaluación";
                vURL = vURL + "?IdCandidato=" + idCandidato

                if (idBateria != null) {
                    vURL = vURL + "&IdBateria=" + idBateria;
                }

                if (clToken != null) {
                    vURL = vURL + "&ClToken=" + clToken;
                }

                vURL = vURL + "&SolicitudId=" + idSolicitud;

                var windowProperties = {};
                windowProperties.width = 1000;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog(vURL, "rwListaProcesoSeleccion", vTitulo, windowProperties);
            }

            function OpenProcesoSeleccionWindowRespaldo() {

                var vURL = "ProcesoSeleccion.aspx";
                var vTitulo = "Proceso de evaluación";
                vURL = vURL + "?IdCandidato=" + idCandidato + "&IdRequisicion=" + idRequisicion + "&IdBateria=" + idBateria + "&ClToken=" + clToken;

                var windowProperties = {};
                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog(vURL, "rwListaProcesoSeleccion", vTitulo, windowProperties);
            }

            function ShowProcesoSeleccionForm() {

                obtenerIdFila();

                if (idSolicitud == "") {
                    radalert("Selecciona una solicitud.", 400, 150, "Aviso");
                    return;
                }

                //if (idRequisicion == "") {
                //    radalert("El candidato no ha sido postulado para ninguna requisición", 400, 150);
                //    return;
                //}

                OpenProcesoSeleccionWindow();
            }

        </script>

    </telerik:RadCodeBlock>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdSolicitudes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnConsultar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnConsultar" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <label class="labelTitulo">Solicitudes</label>

    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splSolicitudes" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <telerik:RadGrid
                    ID="grdSolicitudes"
                    runat="server"
                    Height="100%"
                    AutoGenerateColumns="false"
                    EnableHeaderContextMenu="true"
                    AllowSorting="true"
                    HeaderStyle-Font-Bold="true"
                    AllowMultiRowSelection="true"
                    OnItemDataBound="grdSolicitudes_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_SOLICITUD,ID_CANDIDATO,ID_BATERIA,CL_TOKEN,K_SOLICITUD_CL_SOLICITUD_ESTATUS,ID_REQUISICION" EnableColumnsViewState="false" DataKeyNames="ID_SOLICITUD,ID_BATERIA" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35" HeaderText="Sel."></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Folio de solicitud" DataField="K_SOLICITUD_CL_SOLICITUD" UniqueName="K_SOLICITUD_CL_SOLICITUD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre completo" DataField="C_CANDIDATO_NB_EMPLEADO_COMPLETO" UniqueName="C_CANDIDATO_NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="90" HeaderText="Fecha de solicitud" DataField="K_SOLICITUD_FE_SOLICITUD" UniqueName="K_SOLICITUD_FE_SOLICITUD" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" ItemStyle-HorizontalAlign="Center" HeaderText="Estatus de solicitud" DataField="K_SOLICITUD_CL_SOLICITUD_ESTATUS" UniqueName="K_SOLICITUD_CL_SOLICITUD_ESTATUS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="90" HeaderText="No. de empleado" DataField="M_EMPLEADO_CL_EMPLEADO" UniqueName="M_EMPLEADO_CL_EMPLEADO"></telerik:GridBoundColumn>


                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="20" ItemStyle-HorizontalAlign="Center" HeaderText="Estatus" DataField="M_EMPLEADO_CL_ESTADO_EMPLEADO" UniqueName="M_EMPLEADO_CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="No. Requisición" DataField="M_DEPARTAMENTO_NO_REQUISICION" UniqueName="M_DEPARTAMENTO_NO_REQUISICION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="350" FilterControlWidth="280" HeaderText="Descripción de competencias adicionales" DataField="K_SOLICITUD_DS_COMPETENCIAS_ADICIONALES" UniqueName="K_SOLICITUD_DS_COMPETENCIAS_ADICIONALES"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre" DataField="C_CANDIDATO_NB_CANDIDATO" UniqueName="C_CANDIDATO_NB_CANDIDATO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno" DataField="C_CANDIDATO_NB_APELLIDO_PATERNO" UniqueName="C_CANDIDATO_NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Ápellido materno" DataField="C_CANDIDATO_NB_APELLIDO_MATERNO" UniqueName="C_CANDIDATO_NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género" DataField="C_CANDIDATO_CL_GENERO" UniqueName="C_CANDIDATO_CL_GENERO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="RFC" DataField="C_CANDIDATO_CL_RFC" UniqueName="C_CANDIDATO_CL_RFC"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="CURP" DataField="C_CANDIDATO_CL_CURP" UniqueName="C_CANDIDATO_CL_CURP"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado civil" DataField="C_CANDIDATO_CL_ESTADO_CIVIL" UniqueName="C_CANDIDATO_CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre de cónyuge" DataField="C_CANDIDATO_NB_CONYUGUE" UniqueName="C_CANDIDATO_NB_CONYUGUE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="NSS" DataField="C_CANDIDATO_CL_NSS" UniqueName="C_CANDIDATO_CL_NSS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Tipo sanguíneo" DataField="C_CANDIDATO_CL_TIPO_SANGUINEO" UniqueName="C_CANDIDATO_CL_TIPO_SANGUINEO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="País" DataField="C_CANDIDATO_NB_PAIS" UniqueName="C_CANDIDATO_NB_PAIS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Entidad federativa" DataField="C_CANDIDATO_NB_ESTADO" UniqueName="C_CANDIDATO_NB_ESTADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Municipio" DataField="C_CANDIDATO_NB_MUNICIPIO" UniqueName="C_CANDIDATO_NB_MUNICIPIO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Colonia" DataField="C_CANDIDATO_NB_COLONIA" UniqueName="C_CANDIDATO_NB_COLONIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calle" DataField="C_CANDIDATO_NB_CALLE" UniqueName="C_CANDIDATO_NB_CALLE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Exterior" DataField="C_CANDIDATO_NO_INTERIOR" UniqueName="C_CANDIDATO_NO_INTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Interior" DataField="C_CANDIDATO_NO_EXTERIOR" UniqueName="C_CANDIDATO_NO_EXTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Código postal" DataField="C_CANDIDATO_CL_CODIGO_POSTAL" UniqueName="C_CANDIDATO_CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Correo electrónico" DataField="C_CANDIDATO_CL_CORREO_ELECTRONICO" UniqueName="C_CANDIDATO_CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha de nacimiento" DataField="C_CANDIDATO_FE_NACIMIENTO" UniqueName="C_CANDIDATO_FE_NACIMIENTO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="180" FilterControlWidth="130" HeaderText="Lugar de nacimiento" DataField="C_CANDIDATO_DS_LUGAR_NACIMIENTO" UniqueName="C_CANDIDATO_DS_LUGAR_NACIMIENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="80" HeaderText="Nacionalidad" DataField="C_CANDIDATO_CL_NACIONALIDAD" UniqueName="C_CANDIDATO_CL_NACIONALIDAD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo" DataField="Nombre de licencia" UniqueName="C_CANDIDATO_NB_LICENCIA" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="300" FilterControlWidth="250" HeaderText="Descripción de vehículo" DataField="C_CANDIDATO_DS_VEHICULO" UniqueName="C_CANDIDATO_DS_VEHICULO" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Cartilla militar" DataField="C_CANDIDATO_CL_CARTILLA_MILITAR" UniqueName="C_CANDIDATO_CL_CARTILLA_MILITAR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Disponibilidad" DataField="C_CANDIDATO_DS_DISPONIBILIDAD" UniqueName="C_CANDIDATO_DS_DISPONIBILIDAD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Disponibilidad de viaje" DataField="C_CANDIDATO_CL_DISPONIBILIDAD_VIAJE" UniqueName="C_CANDIDATO_CL_DISPONIBILIDAD_VIAJE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="300" FilterControlWidth="250" HeaderText="Comentarios" DataField="C_CANDIDATO_DS_COMENTARIO" UniqueName="C_CANDIDATO_DS_COMENTARIO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="70" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="M_FE_MODIFICA" UniqueName="M_FE_MODIFICA" HeaderStyle-Width="150" FilterControlWidth="100" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                     <telerik:RadSlidingPane ID="slzAyuda" runat="server" Title="Ayuda" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 10px;">
                            <p style="text-align:justify">
                                Actualizar solicitud te permitirá actualizar la cartera electrónica, invitando al candidato para que actualice sus datos o notificándole que su solicitud ha sido eliminada y si es de su interés pueda entrar a la página de la organización para integrar nuevamente su solicitud. Las notificaciones serán enviadas al correo electrónico ingresado en la solicitud del candidato.<br/><br/>
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                    <telerik:RadSlidingPane ID="RSPAdvSearch" runat="server" Title="Búsqueda avanzada" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 10px;">
                            <p style="text-align:justify">
                                 Para especificar la búsqueda selecciona de los campos disponibles el campo que sea de tu interés (por ejemplo “estado civil”), después selecciona el criterio de búsqueda (por ejemplo “igual a”), y en seguida ingresa la cadena con la que quieres efectuar dicha comparación (por ejemplo “soltero”), a continuación da click en “agregar", y al hacer click el sistema te enviará todos aquellos solicitantes que su estado civil sea soltero.<br/><br/>
                                Puedes afinar la selección combinando varios campos, (por ejemplo, “sexo igual a femenino” , “municipio igual a Salamanca”, y “fecha de solicitud entre 01/01/2013 al 01/01/2014”), cada campo añadido limita el espectro posible de resultados, ya que la solicitud debe cumplir con la información indicada (estado civil soltero; sexo femenino; municipio Salamanca; con fecha de solicitud entre el 01/01/2013 al 01/01/2014). Si no cumple con alguna de estas variables no es incluido dentro de los resultados (por ejemplo: si es mujer, vive en Salamanca y su estado civil es casado, no es tomado en cuenta). 
                            </p><br />
                            <telerik:RadFilter runat="server" ID="ftGrdSolicitudes" FilterContainerID="grdSolicitudes" ShowApplyButton="true" Height="100">
                                <ContextMenu Height="100" EnableAutoScroll="false">
                                    <DefaultGroupSettings Height="100" />
                                </ContextMenu>
                            </telerik:RadFilter>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowInsertForm" AutoPostBack="false" runat="server" Text="Agregar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicking="ShowEditForm" AutoPostBack="false" runat="server" Text="Editar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_click" OnClientClicking="ConfirmarEliminar" AutoPostBack="true"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnConsultar" OnClientClicked="ShowConsultForm" runat="server" Text="Consultar" AutoPostBack="true"></telerik:RadButton>
    </div>
     <div class="ctrlBasico">
         <telerik:RadButton ID="btnActualizarCartera" runat="server" Text="Actualizar solicitud" OnClientClicking="ConfirmarActualizar" OnClick="btnActualizarCartera_Click" AutoPostBack="true"></telerik:RadButton>
    </div>
   <div class="ctrlBasico">
         <telerik:RadButton ID="btnImpresion2" runat="server" Text="Imprimir" OnClientClicked="OpenImpresion" AutoPostBack="false"></telerik:RadButton>
    </div>
<%--    <div class="ctrlBasico">
        <telerik:RadButton runat="server" ID="btnProcesoSeleccion" Text="Proceso de evaluación" AutoPostBack="false" OnClientClicked="ShowProcesoSeleccionForm"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnPruebas" runat="server" Text="Resultado de pruebas" AutoPostBack="false" OnClientClicked="showPopupResultadoPruebas"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnConsultas" AutoPostBack="false" runat="server" Text="Consultas personales" OnClientClicked="showPopupConsultas"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnProceso" runat="server" Text="Contratar" OnClientClicked="ShowContratarForm" AutoPostBack="false"></telerik:RadButton>
    </div>--%>
   
    <%--<div class="ctrlBasico">
        <telerik:RadButton ID="btnBusquedaAvanzada" AutoPostBack="false" runat="server" Text="Búsqueda avanzada" Width="200" OnClientClicked=""></telerik:RadButton>
    </div>--%>
    <%--<div class="ctrlBasico">
        <telerik:RadButton ID="btnCartera" AutoPostBack="false" runat="server" Text="Actualización de cartera" Width="200" OnClientClicked="OpenActualizacionCartera"></telerik:RadButton>
    </div>--%>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup" OnClientShow="centerPopUp">
        <Windows>
               <telerik:RadWindow ID="winImprimir"
                runat="server"
                Title="Imprimir"
                Height="630px"
                Width="1100px"
                ReloadOnShow="true"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="Close"
                Animation="Fade">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winSolicitud" runat="server" Title="Solicitud" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winContratar" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winEntrevista" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winReferencia" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwConsultas" runat="server" Title="Consultas Personales" Height="600px" Width="1100px" ReloadOnShow="false" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwResultadosPruebas" runat="server" Title="Resultado de pruebas" Height="600px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwListaProcesoSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" ReloadOnShow="true"></telerik:RadWindow>
            <telerik:RadWindow ID="rwProcesoSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" ReloadOnShow="true" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="rwComentarios" runat="server" Title="Comentarios de entrevistas" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
        <telerik:RadWindowManager ID="rnTemplate" runat="server">
          <AlertTemplate>    
             <div style="height:10px;"></div>
            <div class="rwDialogButtons" style="text-align:center;">
                ¿Deseas notificar por correo electrónico a los candidatos?
                 <div style="height:20px;"></div>
                <input type="button" value="Si" style="width: 80px;" class="rwOkBtn" onclick="__doPostBack('btnEnviar', 'BorrarEnviar');" />
                <input type="button" value="No" style="width: 80px" class="rwCancelBtn" onclick="__doPostBack('btnBorrar', 'Borrar');"  />
            </div>
        </AlertTemplate>
    </telerik:RadWindowManager>
</asp:Content>
