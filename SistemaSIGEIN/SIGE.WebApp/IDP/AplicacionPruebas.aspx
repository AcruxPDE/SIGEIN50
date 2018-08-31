<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="AplicacionPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.AplicacionPruebas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script id="MyScript" type="text/javascript">

        var vIdBateriaEmp = "";
        var idCandidatoEmp = "";
        var idSolicitudEmp = "";
        var idEmpleado = "";

        function returnDataToParentPopupEmp(pDato) {
            vIdBateriaEmp = "";
            idCandidatoEmp = "";
            idSolicitudEmp = "";
            idEmpleado = "";
            $find("<%=rgPruebasEmpleados.ClientID%>").get_masterTableView().rebind();
        }


        function obtenerIdFilaEmpleados() {
            vIdBateriaEmp = "";
            idCandidatoEmp = "";
            var grid = $find("<%=rgPruebasEmpleados.ClientID %>");
             var MasterTable = grid.get_masterTableView();
             var selectedRows = MasterTable.get_selectedItems();
             if (selectedRows.length != 0) {
                 var row = selectedRows[0];
                 vIdBateriaEmp = row.getDataKeyValue("ID_BATERIA");
                 idCandidatoEmp = row.getDataKeyValue("ID_CANDIDATO");
                 idSolicitudEmp = row.getDataKeyValue("ID_SOLICITUD");
                 idEmpleado = row.getDataKeyValue("M_EMPLEADO_ID_EMPLEADO");
             }
             else {
                 radalert("Selecciona un empleado.", 400, 150, "Error");
             }
         }

         function OpenAgregarPruebasEmp() {
             obtenerIdFilaEmpleados();
             var windowProperties = {
                 width: 900,
                 height: document.documentElement.clientHeight - 20
             };
             if (vIdBateriaEmp != "") {
                 if (vIdBateriaEmp != "" && vIdBateriaEmp != null)
                     openChildDialog("VentanaAgregarPruebas.aspx?pIdBateria=" + vIdBateriaEmp, "winPruebasEmp", "Agregar pruebas", windowProperties);
                 else
                     radalert("No existe una batería creada.", 400, 150, "Error");
             }
         }


         function OpenManualEmp() {
             obtenerIdFilaEmpleados();
             var windowProperties = {
                 width: document.documentElement.clientWidth - 20,
                 height: document.documentElement.clientHeight - 20
             };
             if (idCandidatoEmp != "") {
                 if (vIdBateriaEmp != "" && vIdBateriaEmp != null)
                     openChildDialog("VentanaCapturaManualPruebas.aspx?pIdBateria=" + vIdBateriaEmp + "&pIdCandidato=" + idCandidatoEmp, "winPruebasEmp", "Captura manual", windowProperties);
                 else
                     radalert("No existe una batería creada.", 400, 150, "Error");
             }
         }


         function OpenVizualizarEmp() {
             obtenerIdFilaEmpleados();
             var windowProperties = {
                 width: document.documentElement.clientWidth - 20,
                 height: document.documentElement.clientHeight - 20
             };
             if (idCandidatoEmp != "") {
                 if (vIdBateriaEmp != "" && vIdBateriaEmp != null)
                     openChildDialog("VentanaRevisarPruebas.aspx?pIdBateria=" + vIdBateriaEmp + "&pIdCandidato=" + idCandidatoEmp, "winPruebasEmp", "Vizualizar pruebas", windowProperties);
                 else
                     radalert("No existe una batería creada.", 400, 150, "Error");
             }
         }

         function OpenCrearBateriasEmpleado() {
             var pIdCandidatosPruebas = '<%= vIdGeneraBaterias%>';
<<<<<<< HEAD
            openChildDialog("AgregarPruebas.aspx?pIdCandidatosPruebas=" + pIdCandidatosPruebas, "winPruebasEmp", "Crear batería", GetWindowProperties());
=======
            openChildDialog("AgregarPruebas.aspx?pIdCandidatosPruebas=" + pIdCandidatosPruebas, "winPruebasEmp", "Asignar pruebas", GetWindowProperties());
>>>>>>> DEV
        }

        function OpenAplicarPruebasEmp() {
            var pIdCandidatosPruebas = '<%= vIdGeneraBaterias%>';
            openChildDialog("VentanaAplicarPruebas.aspx?pIdCandidatosPruebas=" + pIdCandidatosPruebas, "winPruebasEmp", "Aplicar pruebas", GetWindowProperties());
        }


        function OpenAsignarEmpledo() {
            var vIdRequisicion = '<%= vIdRequisicion %>';
            var vURL = "VentanaAsignarRequisicion.aspx";
            var vTitulo = "Asignar empleado a requisicion";

            obtenerIdFilaEmpleados();

            if (idEmpleado != "" && idEmpleado != null) {
                vURL = vURL + "?idEmpleado=" + idEmpleado

                if (idCandidatoEmp != "" && idCandidatoEmp != null)
                    vURL = vURL + "&IdCandidato=" + idCandidatoEmp

                if (vIdRequisicion != "" && vIdRequisicion != null) {
                    vURL = vURL + "&IdRequisicion=" + vIdRequisicion;
                }

                if (idSolicitudEmp != "" && idSolicitudEmp != null)
                    vURL = vURL + "&SolicitudId=" + idSolicitudEmp;

                var windowProperties = {};
                windowProperties.width = 600;
                windowProperties.height = 250;


                openChildDialog(vURL, "rwListaProcesoSeleccionEmp", vTitulo, windowProperties);
            }
        }


        function OpenProcesoEmp() {
            var vIdRequisicion = '<%= vIdRequisicion %>';
            var vURL = "VentanaProcesoSeleccionCandidato.aspx";
            var vTitulo = "Proceso de evaluación";

            obtenerIdFilaEmpleados();

            if (idEmpleado != "" && idEmpleado != null) {
                vURL = vURL + "?idEmpleado=" + idEmpleado

                if (idCandidatoEmp != "" && idCandidatoEmp != null)
                    vURL = vURL + "&IdCandidato=" + idCandidatoEmp

                if (vIdRequisicion != "" && vIdRequisicion != null)
                    vURL = vURL + "&IdRequisicion=" + vIdRequisicion;


                if (idSolicitudEmp != "" && idSolicitudEmp != null)
                    vURL = vURL + "&SolicitudId=" + idSolicitudEmp;

                var windowProperties = {};
                windowProperties.width = document.documentElement.clientHeight - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;

                openChildDialog(vURL, "rwListaProcesoSeleccionEmp", vTitulo, windowProperties);
            }
        }


        //-----------------------------------------------------------------Solicitudes
        function onCloseWindow(oWnd, args) {
            var vIdBateria = "";
            var idCandidato = "";
            var idSolicitud = "";
            var clToken = "";
            var clEstatus = "";
            $find("<%=grdSolicitudes.ClientID%>").get_masterTableView().rebind();
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 200,
                height: document.documentElement.clientHeight - 20
            };
        }

        var vIdBateria = "";
        var idCandidato = "";
        var idSolicitud = "";
        var clToken = "";
        var clEstatus = "";

        function OpenCrearBaterias() {
            var pIdCandidatosPruebas = '<%= vIdGeneraBaterias%>';
<<<<<<< HEAD
            openChildDialog("AgregarPruebas.aspx?pIdCandidatosPruebas=" + pIdCandidatosPruebas, "winPruebas", "Crear batería", GetWindowProperties());
=======
            openChildDialog("AgregarPruebas.aspx?pIdCandidatosPruebas=" + pIdCandidatosPruebas, "winPruebas", "Asignar pruebas", GetWindowProperties());
>>>>>>> DEV
        }

        function OpenAplicarPruebas() {
            var pIdCandidatosPruebas = '<%= vIdGeneraBaterias%>';
            openChildDialog("VentanaAplicarPruebas.aspx?pIdCandidatosPruebas=" + pIdCandidatosPruebas, "winPruebas", "Aplicar pruebas", GetWindowProperties());
        }

        function useDataFromChild(pDato) {
            var vIdBateria = "";
            var idCandidato = "";
            var idSolicitud = "";
            var clToken = "";
            var clEstatus = "";
            $find("<%=grdSolicitudes.ClientID%>").get_masterTableView().rebind();
        }

        function OpenAplicacionInterna(pFlBateria, pClToken, pIdCandidato) {
            var win = window.open("Pruebas/PruebaBienvenida.aspx?ID=" + pFlBateria + "&T=" + pClToken + "&idCandidato=" + pIdCandidato, '_self', true);
            win.focus();
        }

        function OpenCapturaManual() {
            obtenerIdFilaSolicitudes();
            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            if (idCandidato != "") {
                if (vIdBateria != "" && vIdBateria != null)
                    openChildDialog("VentanaCapturaManualPruebas.aspx?pIdBateria=" + vIdBateria + "&pIdCandidato=" + idCandidato, "winPruebas", "Captura manual", windowProperties);
                else
                    radalert("No existe una batería creada.", 400, 150, "Error");
            }
        }

        function OpenVizualizarPruebas() {
            obtenerIdFilaSolicitudes();
            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            if (idCandidato != "") {
                if (vIdBateria != "" && vIdBateria != null)
                    openChildDialog("VentanaRevisarPruebas.aspx?pIdBateria=" + vIdBateria + "&pIdCandidato=" + idCandidato, "winPruebas", "Vizualizar pruebas", windowProperties);
                else
                    radalert("No existe una batería creada.", 400, 150, "Error");
            }
        }

        function obtenerIdFilaSolicitudes() {
            vIdBateria = "";
            idCandidato = "";
            var grid = $find("<%=grdSolicitudes.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                vIdBateria = row.getDataKeyValue("ID_BATERIA");
                idCandidato = row.getDataKeyValue("ID_CANDIDATO");
            }
            else {
                radalert("Selecciona una solicitud.", 400, 150, "Error");
            }
        }

        function ShowProcesoSeleccionForm() {
            vIdBateria = "";
            idCandidato = "";
            idSolicitud = "";
            clToken = "";
            var grid = $find("<%=grdSolicitudes.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                vIdBateria = row.getDataKeyValue("ID_BATERIA");
                idCandidato = row.getDataKeyValue("ID_CANDIDATO");
                idSolicitud = row.getDataKeyValue("ID_SOLICITUD");
                clToken = row.getDataKeyValue("CL_TOKEN");
            }
            if (idSolicitud == "") {
                radalert("Selecciona una solicitud.", 400, 150);
                return;
            }
            OpenProcesoSeleccionWindow();
        }

        function OpenProcesoSeleccionWindow() {
            var vIdRequisicion = '<%= vIdRequisicion %>';
            var vURL = "VentanaProcesoSeleccionCandidato.aspx";
            var vTitulo = "Proceso de evaluación";
            vURL = vURL + "?IdCandidato=" + idCandidato

            //if (vIdBateria != null) {
            //    vURL = vURL + "&IdBateria=" + vIdBateria;
            //}

            //if (clToken != null) {
            //    vURL = vURL + "&ClToken=" + clToken;
            //}

            if (vIdRequisicion != "") {
                vURL = vURL + "&IdRequisicion=" + vIdRequisicion;
            }

            vURL = vURL + "&SolicitudId=" + idSolicitud;

            var windowProperties = {};
            windowProperties.width = 1000;
            windowProperties.height = document.documentElement.clientHeight - 20;
            openChildDialog(vURL, "rwListaProcesoSeleccion", vTitulo, windowProperties);
        }

        function ShowContratarForm() {
            vIdBateria = "";
            idCandidato = "";
            idSolicitud = "";
            clToken = "";
            clEstatus = "";
            var grid = $find("<%=grdSolicitudes.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                vIdBateria = row.getDataKeyValue("ID_BATERIA");
                idCandidato = row.getDataKeyValue("ID_CANDIDATO");
                idSolicitud = row.getDataKeyValue("ID_SOLICITUD");
                clToken = row.getDataKeyValue("CL_TOKEN");
                clEstatus = row.getDataKeyValue("CL_SOLICITUD_ESTATUS");
            }
            if (idSolicitud == "") {
                radalert("Selecciona una solicitud.", 400, 150);
                return;
            }

            if (clEstatus == "Contratado") {
                radalert("El candidato de la solicitud ya está contratado. Selecciona otra solicitud.", 400, 150);
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
            windowProperties.height = 420;
            openChildDialog(vURL, "winContratar", vTitulo, windowProperties);
        }

        function OpenAgregarPruebas() {
            obtenerIdFilaSolicitudes();
            var windowProperties = {
                width: 900,
                height: document.documentElement.clientHeight - 20
            };
            if (idCandidato != "") {
                if (vIdBateria != "" && vIdBateria != null)
                    openChildDialog("VentanaAgregarPruebas.aspx?pIdBateria=" + vIdBateria, "winPruebas", "Agregar pruebas", windowProperties);
                else
                    radalert("No existe una batería creada.", 400, 150, "Error");
            }
        }

        function OpenAsignarRequisicion() {
            var vIdRequisicion = '<%= vIdRequisicion %>';

                var vURL = "VentanaAsignarRequisicion.aspx";
<<<<<<< HEAD
                var vTitulo = "Asignar candidato a requisicion";
=======
                var vTitulo = "Asignar candidato a requisición";
>>>>>>> DEV

                var grid = $find("<%=grdSolicitudes.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    idCandidato = row.getDataKeyValue("ID_CANDIDATO");
                    idSolicitud = row.getDataKeyValue("ID_SOLICITUD");
                }


                vURL = vURL + "?IdCandidato=" + idCandidato

                if (vIdRequisicion != "") {
                    vURL = vURL + "&IdRequisicion=" + vIdRequisicion;
                }

                vURL = vURL + "&SolicitudId=" + idSolicitud;

                var windowProperties = {};
                windowProperties.width = 600;
                windowProperties.height = 250;

                if (idCandidato != "")
                    openChildDialog(vURL, "rwListaProcesoSeleccion", vTitulo, windowProperties);
                else
                    radalert("Selecciona una solicitud.", 400, 150);
            }


            function RowSelecting(sender, eventArgs) {

                var rgSolicitudes = $find('<%= grdSolicitudes.ClientID %>');
                var rgEmpleados = $find('<%= rgPruebasEmpleados.ClientID %>');

                if (sender == rgSolicitudes) {

                    var btnCrearBateria = $find('<%= btnCrearBateria.ClientID %>');
                     var btnAplicarPruebas = $find('<%= btnAplicarPruebas.ClientID %>');
                     var btnAgrgarPruebas = $find('<%= btnAgrgarPruebas.ClientID %>');
                     var btnCapturaManual = $find('<%= btnCapturaManual.ClientID %>');
                     var btnVizualizarPruebas = $find('<%= btnVizualizarPruebas.ClientID %>');
                     var btnProceso = $find('<%= btnProceso.ClientID %>');
                     var btnAsigRequisicion = $find('<%= btnAsigRequisicion.ClientID %>');
                     var btnContratar = $find('<%= btnContratar.ClientID %>');

                     var tableView = eventArgs.get_tableView();

                     if (tableView.get_selectedItems().length > 0) {
                         if ('<%= vbtnCrearBateria %>' == "True")
                            btnCrearBateria.set_enabled(true);
                        if ('<%= vbtnAplicarPruebas %>' == "True")
                            btnAplicarPruebas.set_enabled(true);
                        btnAgrgarPruebas.set_enabled(false);
                        btnCapturaManual.set_enabled(false);
                        btnVizualizarPruebas.set_enabled(false);
                        btnProceso.set_enabled(false);
                        btnAsigRequisicion.set_enabled(false);
                        btnContratar.set_enabled(false);
                    }
                    else {
                        if ('<%= vbtnCrearBateria %>' == "True")
                            btnCrearBateria.set_enabled(true);
                        if ('<%= vbtnAplicarPruebas %>' == "True")
                            btnAplicarPruebas.set_enabled(true);
                        if ('<%= vbtnAgrgarPruebas %>' == "True")
                            btnAgrgarPruebas.set_enabled(true);
                        if ('<%= vbtnCapturaManual %>' == "True")
                            btnCapturaManual.set_enabled(true);
                        if ('<%= vbtnVizualizarPruebas %>' == "True")
                            btnVizualizarPruebas.set_enabled(true);
                        if ('<%= vbtnProceso %>' == "True")
                            btnProceso.set_enabled(true);
                        if ('<%= vbtnAsigRequisicion %>' == "True")
                            btnAsigRequisicion.set_enabled(true);
                        if ('<%= vbtnContratar %>' == "True")
                            btnContratar.set_enabled(true);
                    }
                }
                else if (sender == rgEmpleados) {

                    var btnCrearBateriaEmp = $find('<%= btnCrearBateriaEmp.ClientID %>');
                    var btnAplicarPruebaEmp = $find('<%= btnAplicarPruebaEmp.ClientID %>');
                    var btnAgregarPruebaEmp = $find('<%= btnAgregarPruebaEmp.ClientID %>');
                    var btnManualEmp = $find('<%= btnManualEmp.ClientID %>');
                    var btnVizualizaEmp = $find('<%= btnVizualizaEmp.ClientID %>');
                    var btnProcesoEvalEmp = $find('<%= btnProcesoEvalEmp.ClientID %>');
                    var btAsignaReqEmp = $find('<%= btAsignaReqEmp.ClientID %>');

                    var tableView = eventArgs.get_tableView();

                    if (tableView.get_selectedItems().length > 0) {
                        if ('<%= vbtnCrearBateria %>' == "True")
                        btnCrearBateriaEmp.set_enabled(true);
                    if ('<%= vbtnAplicarPruebas %>' == "True")
                        btnAplicarPruebaEmp.set_enabled(true);

                    btnAgregarPruebaEmp.set_enabled(false);
                    btnManualEmp.set_enabled(false);
                    btnVizualizaEmp.set_enabled(false);
                    btnProcesoEvalEmp.set_enabled(false);
                    btAsignaReqEmp.set_enabled(false);
                }
                else {
                    if ('<%= vbtnCrearBateria %>' == "True")
                        btnCrearBateriaEmp.set_enabled(true);
                    if ('<%= vbtnAplicarPruebas %>' == "True")
                        btnAplicarPruebaEmp.set_enabled(true);
                    if ('<%= vbtnAgrgarPruebas %>' == "True")
                        btnAgregarPruebaEmp.set_enabled(true);
                    if ('<%= vbtnCapturaManual %>' == "True")
                        btnManualEmp.set_enabled(true);
                    if ('<%= vbtnVizualizarPruebas %>' == "True")
                        btnVizualizaEmp.set_enabled(true);
                    if ('<%= vbtnProceso %>' == "True")
                        btnProcesoEvalEmp.set_enabled(true);
                    if ('<%= vbtnAsigRequisicion %>' == "True")
                        btAsignaReqEmp.set_enabled(true);
                }
            }
    }

    function RowDeselected(sender, eventArgs) {
        var rgSolicitudes = $find('<%= grdSolicitudes.ClientID %>');
        var rgEmpleados = $find('<%= rgPruebasEmpleados.ClientID %>');

        if (sender == rgSolicitudes) {

            var btnCrearBateria = $find('<%= btnCrearBateria.ClientID %>');
            var btnAplicarPruebas = $find('<%= btnAplicarPruebas.ClientID %>');
            var btnAgrgarPruebas = $find('<%= btnAgrgarPruebas.ClientID %>');
            var btnCapturaManual = $find('<%= btnCapturaManual.ClientID %>');
            var btnVizualizarPruebas = $find('<%= btnVizualizarPruebas.ClientID %>');
            var btnProceso = $find('<%= btnProceso.ClientID %>');
            var btnAsigRequisicion = $find('<%= btnAsigRequisicion.ClientID %>');
            var btnContratar = $find('<%= btnContratar.ClientID %>');

            var tableView = eventArgs.get_tableView();

            if (tableView.get_selectedItems().length > 1) {

                if ('<%= vbtnCrearBateria %>' == "True")
                    btnCrearBateria.set_enabled(true);
                if ('<%= vbtnAplicarPruebas %>' == "True")
                    btnAplicarPruebas.set_enabled(true);
                btnAgrgarPruebas.set_enabled(false);
                btnCapturaManual.set_enabled(false);
                btnVizualizarPruebas.set_enabled(false);
                btnProceso.set_enabled(false);
                btnAsigRequisicion.set_enabled(false);
                btnContratar.set_enabled(false);
            }
            else {
                if ('<%= vbtnCrearBateria %>' == "True")
                    btnCrearBateria.set_enabled(true);
                if ('<%= vbtnAplicarPruebas %>' == "True")
                    btnAplicarPruebas.set_enabled(true);
                if ('<%= vbtnAgrgarPruebas %>' == "True")
                    btnAgrgarPruebas.set_enabled(true);
                if ('<%= vbtnCapturaManual %>' == "True")
                    btnCapturaManual.set_enabled(true);
                if ('<%= vbtnVizualizarPruebas %>' == "True")
                    btnVizualizarPruebas.set_enabled(true);
                if ('<%= vbtnProceso %>' == "True")
                    btnProceso.set_enabled(true);
                if ('<%= vbtnAsigRequisicion %>' == "True")
                    btnAsigRequisicion.set_enabled(true);
                if ('<%= vbtnContratar %>' == "True")
                    btnContratar.set_enabled(true);
            }
        }

        else if (sender == rgEmpleados) {

            var btnCrearBateriaEmp = $find('<%= btnCrearBateriaEmp.ClientID %>');
            var btnAplicarPruebaEmp = $find('<%= btnAplicarPruebaEmp.ClientID %>');
            var btnAgregarPruebaEmp = $find('<%= btnAgregarPruebaEmp.ClientID %>');
            var btnManualEmp = $find('<%= btnManualEmp.ClientID %>');
            var btnVizualizaEmp = $find('<%= btnVizualizaEmp.ClientID %>');
            var btnProcesoEvalEmp = $find('<%= btnProcesoEvalEmp.ClientID %>');
            var btAsignaReqEmp = $find('<%= btAsignaReqEmp.ClientID %>');

            var tableView = eventArgs.get_tableView();

            if (tableView.get_selectedItems().length > 1) {
                if ('<%= vbtnCrearBateria %>' == "True")
                        btnCrearBateriaEmp.set_enabled(true);
                    if ('<%= vbtnAplicarPruebas %>' == "True")
                        btnAplicarPruebaEmp.set_enabled(true);

                    btnAgregarPruebaEmp.set_enabled(false);
                    btnManualEmp.set_enabled(false);
                    btnVizualizaEmp.set_enabled(false);
                    btnProcesoEvalEmp.set_enabled(false);
                    btAsignaReqEmp.set_enabled(false);
                }
                else {
                    if ('<%= vbtnCrearBateria %>' == "True")
                        btnCrearBateriaEmp.set_enabled(true);
                    if ('<%= vbtnAplicarPruebas %>' == "True")
                        btnAplicarPruebaEmp.set_enabled(true);
                    if ('<%= vbtnAgrgarPruebas %>' == "True")
                        btnAgregarPruebaEmp.set_enabled(true);
                    if ('<%= vbtnCapturaManual %>' == "True")
                        btnManualEmp.set_enabled(true);
                    if ('<%= vbtnVizualizarPruebas %>' == "True")
                        btnVizualizaEmp.set_enabled(true);
                    if ('<%= vbtnProceso %>' == "True")
                        btnProcesoEvalEmp.set_enabled(true);
                    if ('<%= vbtnAsigRequisicion %>' == "True")
                        btAsignaReqEmp.set_enabled(true);
                }
            }
    }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <label class="labelTitulo">Candidatos</label>
        <div style="clear: both;"></div>
        <telerik:RadTabStrip ID="rtsAplicacionPruebas" runat="server" SelectedIndex="0" MultiPageID="rmpPruebas">
            <Tabs>
                <telerik:RadTab Text="Solicitudes"></telerik:RadTab>
                <telerik:RadTab Text="Empleados"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: calc(100% - 90px);">
                 <div style="clear: both; height: 10px;"></div>
            <telerik:RadMultiPage ID="rmpPruebas" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="rpvPruebasSolicitudes" runat="server">
                    <telerik:RadSplitter ID="splSolicitudes" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Vertical">
                        <telerik:RadPane ID="rpnGridPruebas" runat="server">
                            <div style="height: calc(100% - 60px);">
                                <telerik:RadGrid
                                    ID="grdSolicitudes"
                                    runat="server"
                                    Height="100%"
                                    AutoGenerateColumns="false"
                                    EnableHeaderContextMenu="true"
                                    AllowSorting="true"
                                    HeaderStyle-Font-Bold="true"
                                    AllowMultiRowSelection="true"
                                    OnNeedDataSource="grdSolicitudes_NeedDataSource"
                                    OnItemDataBound="grdSolicitudes_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                        <ClientEvents OnRowSelecting="RowSelecting" OnRowDeselected="RowDeselected" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView ClientDataKeyNames="ID_SOLICITUD,ID_CANDIDATO,ID_BATERIA, CL_TOKEN, CL_SOLICITUD_ESTATUS, ESTATUS" EnableColumnsViewState="false" DataKeyNames="ID_SOLICITUD,ID_BATERIA, ESTATUS, CL_SOLICITUD" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                        <Columns>
                                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35" HeaderText="Sel."></telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre completo" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO"></telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="90" HeaderText="Fecha de solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estatus del proceso" DataField="CL_SOLICITUD_ESTATUS" UniqueName="CL_SOLICITUD_ESTATUS"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estatus batería" DataField="ESTATUS" UniqueName="ESTATUS"></telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha de aplicación" DataField="FE_TERMINO" UniqueName="FE_TERMINO"></telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Enviada" DataField="FG_ENVIO_CORREO" UniqueName="FG_ENVIO_CORREO"></telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha de envío" DataField="FE_ENVIO_CORREO" UniqueName="FE_ENVIO_CORREO"></telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="70" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="M_FE_MODIFICA" UniqueName="M_FE_MODIFICA" HeaderStyle-Width="150" FilterControlWidth="100" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both; height: 10px;"></div>
                            <div class="ctrlBasico">
<<<<<<< HEAD
                                <telerik:RadButton runat="server" Text="Crear batería" ID="btnCrearBateria" AutoPostBack="true" OnClick="btnCrearBateria_Click" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Aplicar pruebas" ID="btnAplicarPruebas" AutoPostBack="true" OnClick="btnAplicarPruebas_Click" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Agregar pruebas" ID="btnAgrgarPruebas" AutoPostBack="false" OnClientClicked="OpenAgregarPruebas" />
                            </div>
=======
                                <telerik:RadButton runat="server" Text="Asignar pruebas" ID="btnCrearBateria" AutoPostBack="true" OnClick="btnCrearBateria_Click" />
                            </div>                          
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Agregar pruebas" ID="btnAgrgarPruebas" AutoPostBack="false" OnClientClicked="OpenAgregarPruebas" />
                            </div>
                             <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ToolTip="Da clic en esta opción para para que selecciones la forma más apta de aplicación de psicometría para tu(s) candidato(s)." Text="Aplicar pruebas" ID="btnAplicarPruebas" AutoPostBack="true" OnClick="btnAplicarPruebas_Click" />
                            </div>
>>>>>>> DEV
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Captura manual" ID="btnCapturaManual" AutoPostBack="false" OnClientClicked="OpenCapturaManual" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Visualizar pruebas" ID="btnVizualizarPruebas" AutoPostBack="false" OnClientClicked="OpenVizualizarPruebas" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Proceso de evaluación" ID="btnProceso" AutoPostBack="false" OnClientClicked="ShowProcesoSeleccionForm" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Asignar a requisición" ID="btnAsigRequisicion" AutoPostBack="false" OnClientClicked="OpenAsignarRequisicion" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Contratar" ID="btnContratar" OnClientClicked="ShowContratarForm" AutoPostBack="false" />
                            </div>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvPruebasEmpleados" runat="server">
                    <telerik:RadSplitter ID="rplPruebasEmpleados" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Vertical">
                        <telerik:RadPane ID="rpPruebasEmpleados" runat="server">
                            <div style="height: calc(100% - 60px);">
                                <telerik:RadGrid
                                    ID="rgPruebasEmpleados"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    EnableHeaderContextMenu="true"
                                    Height="100%"
                                    AllowSorting="true"
                                    HeaderStyle-Font-Bold="true"
                                    OnNeedDataSource="rgPruebasEmpleados_NeedDataSource"
                                    AllowMultiRowSelection="true"
                                    RetainExpandStateOnRebind="true"
                                    OnItemDataBound="rgPruebasEmpleados_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                        <ClientEvents OnRowSelecting="RowSelecting" OnRowDeselected="RowDeselected" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView ClientDataKeyNames="ID_BATERIA,M_EMPLEADO_ID_EMPLEADO, ID_CANDIDATO, ID_SOLICITUD" Name="Baterias"
                                        EnableColumnsViewState="false" DataKeyNames="ID_BATERIA,M_EMPLEADO_ID_EMPLEADO, M_EMPLEADO_CL_EMPLEADO, ID_CANDIDATO, ESTATUS" AllowPaging="true"
                                        AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                        <Columns>
                                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35" HeaderText="Sel."></telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="No. de Empleado" DataField="M_EMPLEADO_CL_EMPLEADO" UniqueName="M_EMPLEADO_CL_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre completo" DataField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="60" HeaderText="Etapa del proceso" DataField="CL_ESTATUS_SOLICITUD" UniqueName="CL_ESTATUS_SOLICITUD"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" ItemStyle-HorizontalAlign="Center" HeaderText="Estatus" DataField="M_EMPLEADO_CL_ESTADO_EMPLEADO" UniqueName="M_EMPLEADO_CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estatus batería" DataField="ESTATUS" UniqueName="ESTATUS"></telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha de aplicación" DataField="FE_TERMINO" UniqueName="FE_TERMINO"></telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Enviada" DataField="FG_ENVIO_CORREO" UniqueName="FG_ENVIO_CORREO"></telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha de envío" DataField="FE_ENVIO_CORREO" UniqueName="FE_ENVIO_CORREO"></telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="70" HeaderText="Último usuario que modifica" DataField="M_EMPLEADO_CL_USUARIO_APP_MODIFICA" UniqueName="M_EMPLEADO_CL_USUARIO_APP_MODIFICA"></telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="M_FE_MODIFICA" UniqueName="M_FE_MODIFICA" HeaderStyle-Width="150" FilterControlWidth="100" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both; height: 10px;"></div>
                            <div class="ctrlBasico">
<<<<<<< HEAD
                                <telerik:RadButton runat="server" Text="Crear batería" ID="btnCrearBateriaEmp" AutoPostBack="true" OnClick="btnCrearBateriaEmp_Click" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Aplicar pruebas" ID="btnAplicarPruebaEmp" AutoPostBack="true" OnClick="btnAplicarPruebaEmp_Click" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Agregar pruebas" ID="btnAgregarPruebaEmp" AutoPostBack="false" OnClientClicked="OpenAgregarPruebasEmp" />
                            </div>
=======
                                <telerik:RadButton runat="server" Text="Asignar pruebas" ID="btnCrearBateriaEmp" AutoPostBack="true" OnClick="btnCrearBateriaEmp_Click" />
                            </div>         
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Agregar pruebas" ID="btnAgregarPruebaEmp" AutoPostBack="false" OnClientClicked="OpenAgregarPruebasEmp" />
                            </div>
                             <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ToolTip="Da clic en esta opción para para que selecciones la forma más apta de aplicación de psicometría para tu(s) empleado(s)." Text="Aplicar pruebas" ID="btnAplicarPruebaEmp" AutoPostBack="true" OnClick="btnAplicarPruebaEmp_Click" />
                            </div>
>>>>>>> DEV
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Captura manual" ID="btnManualEmp" AutoPostBack="false" OnClientClicked="OpenManualEmp" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Visualizar pruebas" ID="btnVizualizaEmp" AutoPostBack="false" OnClientClicked="OpenVizualizarEmp" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Proceso de evaluación" ID="btnProcesoEvalEmp" AutoPostBack="false" OnClientClicked="OpenProcesoEmp" />
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" Text="Asignar a requisición" ID="btAsignaReqEmp" AutoPostBack="false" OnClientClicked="OpenAsignarEmpledo" />
                            </div>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    <asp:HiddenField runat="server" ID="hfSelectedRow" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="RWpruebasCorrecion" runat="server" Title="Pruebas" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Modal="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="RWpruebas" runat="server" Title="Pruebas" Height="600" Width="1000" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Modal="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="rwConsultas" runat="server" Title="Consultas Personales" Height="600px" Width="1100px" ReloadOnShow="false" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winContratar" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="rwListaProcesoSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" ReloadOnShow="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winPruebas" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Modal="true" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionCandidato" runat="server" Title="Seleccionar empleado" Height="600px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwProcesoSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" ReloadOnShow="true" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winAnalisisCompetenicas" runat="server" Title="Analisis de competencias" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winProcesoSeleccion" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEntrevista" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winPerfil" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winDescriptivo" runat="server" Title="Agregar/Editar Puestos" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winVistaPrevia" runat="server" Title="Vista previa" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionPuestos" runat="server" Title="Seleccionar Jefe inmediato" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winPruebasEmp" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Modal="true" OnClientClose="returnDataToParentPopupEmp"></telerik:RadWindow>
            <telerik:RadWindow ID="rwListaProcesoSeleccionEmp" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" ReloadOnShow="true" OnClientClose="returnDataToParentPopupEmp"></telerik:RadWindow>
            <telerik:RadWindow ID="rwComentarios" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
             
             </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%">
    </telerik:RadWindowManager>
</asp:Content>
