<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="AplicacionPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.AplicacionPruebas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPruebas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPruebas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEnviar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPruebas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            var idPrueba = "";
            var vclTokenExterno = "";
            var vFlBateria = "";
            var tipoPrueba = "";
            var clTipoPrueba = "";
            var idCandidato = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdPruebas.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    vFlBateria = row.getDataKeyValue("ID_BATERIA");
                    vclTokenExterno = row.getDataKeyValue("CL_TOKEN");
                    idCandidato = row.getDataKeyValue("ID_CANDIDATO");
                }
                else
                {
                    vclTokenExterno = "";
                    vFlBateria = "";
                    idCandidato = "";
                }
            }


            function OnLoadPage()
            {
                var btnEnviar = $find('<%= btnEnviar.ClientID %>');
                var btnAplicacion = $find('<%= btnAplicacion.ClientID %>');
                var btnCapturaManual = $find('<%= btnCapturaManual.ClientID %>');
                var btnResultados = $find('<%= btnResultados.ClientID %>');
                var btnRevisar = $find('<%= btnRevisar.ClientID %>');
                var btnConsultas = $find('<%= btnConsultas.ClientID %>');
                var btnEliminar = $find('<%= btnEliminar.ClientID %>');
                var btnEditar = $find('<%= btnEditar.ClientID %>');
                var btnCorregir = $find('<%= btnCorregir.ClientID %>');

                btnEnviar.set_enabled(false);
                btnAplicacion.set_enabled(false);
                btnCapturaManual.set_enabled(false);
                btnResultados.set_enabled(false);
                btnRevisar.set_enabled(false);
                btnConsultas.set_enabled(false);
                btnEliminar.set_enabled(false);
                btnEditar.set_enabled(false);
                btnCorregir.set_enabled(false);
            }

            function onCloseWindow(oWnd, args) {
                var idPrueba = "";
                var vclTokenExterno = "";
                $find("<%=grdPruebas.ClientID%>").get_masterTableView().rebind();
                OnLoadPage();
            }

            function IniciarPrueba() {
                // este método no se esta usando, pero no se borrará, por si se necesita en otro momento.
                obtenerIdFila();
                if ((vFlBateria != "")) {
                    var win = window.open("Pruebas/Default.aspx?ID=" + vFlBateria + "&T=" + vclTokenExterno + "&ty=sig", '_self', true);
                    win.focus();
                }
                else { radalert("No has seleccionado una batería de pruebas.", 400, 150, ""); }
            }

            function obtenerIdFilaManual() {
                var grid = $find("<%=grdPruebas.ClientID%>");
                var MasterTable = $find("<%=grdPruebas.ClientID%>").get_masterTableView();
                for (var j = 0; j < grid.get_detailTables().length; j++) {
                    var childSelectedRows = grid.get_detailTables()[j].get_selectedItems();
                }
                var childSelIDs = new Array(childSelectedRows);
                if (grid.get_detailTables().length > 0) {
                    for (var i = 0; i < childSelectedRows.length; i++) {
                        var row = childSelectedRows[i];
                        idPrueba = row.getDataKeyValue("ID_PRUEBA");
                        clTipoPrueba = row.getDataKeyValue("CL_PRUEBA");
                        //clTipoPrueba = grid.get_detailTables()[0].getCellByColumnUniqueName(row, "CL_PRUEBA").innerHTML;
                        vclTokenExterno = row.getDataKeyValue("CL_TOKEN_EXTERNO");
                        tipoPrueba = row.getDataKeyValue("NB_TIPO_PRUEBA");
                    }
                }
            }

            function IniciarPruebaManual() {
                idPrueba = "";
                clTipoPrueba = "";
                vclTokenExterno = "";
                tipoPrueba = "";
                obtenerIdFilaManual();
                if ((idPrueba != "")) {

                    //  if (clTipoPrueba == "LABORAL-1" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    if (clTipoPrueba == "LABORAL-1" ) {
                        var win = radopen("VentanaPersonalidadLabIManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        // win.focus();
                    }
                    //  else if (clTipoPrueba == "INTERES" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "INTERES" ) {
                        var win = radopen("VentanaInteresesPersonalesManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }
                    //else if (clTipoPrueba == "LABORAL-2" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "LABORAL-2") {
                        var win = radopen("VentanaPersonalidadLaboral2Manual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }
                    // else if (clTipoPrueba == "PENSAMIENTO" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "PENSAMIENTO") {
                        var win = radopen("VentanaEstiloDePensamientoManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }

                    //else if (clTipoPrueba == "APTITUD-1" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "APTITUD-1") {
                        var win = radopen("VentanaAptitudMentalIManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }
                    //else if (clTipoPrueba == "APTITUD-2" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "APTITUD-2") {
                        var win = radopen("VentanaAptitudMentalIIManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }
                    //else if (clTipoPrueba == "TIVA" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "TIVA") {
                        var win = radopen("VentanaTIVAManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }

                    //  else if (clTipoPrueba == "ORTOGRAFIA-1" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "ORTOGRAFIA-1") {
                        var win = radopen("VentanaOrtografiaManual.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&CLAVE=" + clTipoPrueba, 'RWpruebas');
                        //win.focus();
                    }

                    //else if (clTipoPrueba == "ORTOGRAFIA-2" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "ORTOGRAFIA-2") {
                        var win = radopen("VentanaOrtografiaManual.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&CLAVE=" + clTipoPrueba, 'RWpruebas');
                        //win.focus();
                    }

                    //else if (clTipoPrueba == "ORTOGRAFIA-3" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "ORTOGRAFIA-3" ) {
                        var win = radopen("VentanaOrtografiaManual.aspx?ID=" + idPrueba + "&CLAVE=" + clTipoPrueba + "&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }

                    // else if (clTipoPrueba == "TECNICAPC" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                        else if (clTipoPrueba == "TECNICAPC") {
                        var win = radopen("VentanaTecnicaPCManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }

                    //  else if (clTipoPrueba == "REDACCION" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "REDACCION") {
                        var win = radopen("VentanaRedaccionManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, "RWpruebas");
                        win.set_title("Captura manual");
                    }

                    //else if (clTipoPrueba == "INGLES" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                    else if (clTipoPrueba == "INGLES") {
                        var win = radopen("VentanaInglesManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //win.focus();
                    }
                    else if (clTipoPrueba == "ADAPTACION" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                        var win = radopen("VentanaAdaptacionMedioManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //var win = radopen("VentanaIntegracionMedioManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        //TODO:  JULIO TAV 28 JULIO 2017.  Está linea comentada es la nueva de adaptacion al medio manual Se comenta por indicaciones de Martin Sanchez. Descomentar a su criterio.
                        win.set_title("Captura manual");
                    }
                    else if (clTipoPrueba == "ENTREVISTA" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                        var win = radopen("VentanaResultadosEntrevista.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, 'RWpruebas');
                        win.set_title("Captura manual");
                    }
                    else { alert("La prueba ha sido capturada desde la aplicación."); }
                }
                else { radalert("No has seleccionado una prueba.", 400, 150, ""); }
            }

            function RevisarPrueba() {
                //obtenerIdFila();
                idPrueba = "";
                clTipoPrueba = "";
                vclTokenExterno = "";
                tipoPrueba = "";
                estado = "";
                obtenerIdFilaManual();
                if ((idPrueba != "")) {

                    //( if ((clTipoPrueba == "LABORAL-1" && tipoPrueba == "APLICACION") ||(clTipoPrueba == "LABORAL-1" && tipoPrueba == null)) {
                    if (clTipoPrueba == "LABORAL-1" ) {
                        var win = window.open("Pruebas/VentanaPersonalidadLaboralI.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }
                    //else if ((clTipoPrueba == "INTERES" && tipoPrueba == "APLICACION") || (clTipoPrueba == "INTERES" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "INTERES" ) {
                        var win = window.open("Pruebas/VentanaInteresesPersonales.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }
                    //else if ((clTipoPrueba == "LABORAL-2" && tipoPrueba == "APLICACION") ||(clTipoPrueba == "LABORAL-2" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "LABORAL-2") {
                        var win = window.open("Pruebas/VentanaPersonalidadLaboralII.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }
                  //  else if ((clTipoPrueba == "PENSAMIENTO" && tipoPrueba == "APLICACION") || (clTipoPrueba == "PENSAMIENTO" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "PENSAMIENTO" ) {
                        var win = window.open("Pruebas/VentanaEstiloDePensamiento.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }

                    //else if ((clTipoPrueba == "APTITUD-1" && tipoPrueba == "APLICACION") || (clTipoPrueba == "APTITUD-1" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "APTITUD-1") {
                        var win = window.open("Pruebas/VentanaAptitudMentalI.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }
                    //else if ((clTipoPrueba == "APTITUD-2" && tipoPrueba == "APLICACION") || (clTipoPrueba == "APTITUD-2" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "APTITUD-2") {
                        var win = window.open("Pruebas/VentanaAptitudMentalII.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }
                    // else if ((clTipoPrueba == "TIVA" && tipoPrueba == "APLICACION") || (clTipoPrueba == "TIVA" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "TIVA") {
                        var win = window.open("Pruebas/VentanaTIVA.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }

                    //else if ((clTipoPrueba == "ORTOGRAFIA-1" && tipoPrueba == "APLICACION") || (clTipoPrueba == "ORTOGRAFIA-1" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "ORTOGRAFIA-1") {
                        var win = window.open("Pruebas/VentanaOrtografiaI.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }

                    //else if ((clTipoPrueba == "ORTOGRAFIA-2" && tipoPrueba == "APLICACION") || (clTipoPrueba == "ORTOGRAFIA-2" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "ORTOGRAFIA-2") {
                        var win = window.open("Pruebas/VentanaOrtografiaII.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }

                    //else if ((clTipoPrueba == "ORTOGRAFIA-3" && tipoPrueba == "APLICACION") || (clTipoPrueba == "ORTOGRAFIA-3" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "ORTOGRAFIA-3") {
                        var win = window.open("Pruebas/VentanaOrtografiaIII.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }

                    //else if ((clTipoPrueba == "TECNICAPC" && tipoPrueba == "APLICACION") || (clTipoPrueba == "TECNICAPC" && tipoPrueba == null)) {
                    else if (clTipoPrueba == "TECNICAPC") {
                        var win = window.open("Pruebas/VentanaTecnicaPC.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }

                    // else if ((clTipoPrueba == "REDACCION" && tipoPrueba == "APLICACION") || (clTipoPrueba == "REDACCION" && tipoPrueba == null)) {
                      else if (clTipoPrueba == "REDACCION" ) {
                        var win = window.open("Pruebas/VentanaRedaccion.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", "_blank");
                        win.focus();
                    }

                    //else if ((clTipoPrueba == "INGLES" && tipoPrueba == "APLICACION") || (clTipoPrueba == "INGLES" && tipoPrueba == null)) {
                      else if ((clTipoPrueba == "INGLES" )) {
                        var win = window.open("Pruebas/VentanaIngles.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                      }

                      else if (clTipoPrueba == "ENTREVISTA" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                          var win = radopen("VentanaResultadosEntrevista.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno + "&&CL=REVISION", 'RWpruebas');
                      }

                    else if ((clTipoPrueba == "ADAPTACION" && tipoPrueba == "MANUAL") || (clTipoPrueba == "ADAPTACION" && tipoPrueba == null)) {
                        var win = window.open("VentanaIntegracionMedioManual.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=REV", '_blank');
                        win.focus();
                    }
                    else { alert("La prueba fue capturada manualmente."); }
                }
                else { radalert("No se ha seleccionado una prueba.", 400, 150, ""); }
            }


            function CorregirRespuestas() {
                //obtenerIdFila();
                idPrueba = "";
                clTipoPrueba = "";
                vclTokenExterno = "";
                tipoPrueba = "";
                estado = "";
                //var windowProperties = {};
                //windowProperties.width = document.documentElement.clientWidth - 10;
                //windowProperties.height = document.documentElement.clientHeight - 10;
                obtenerIdFilaManual();
                if ((idPrueba != "")) {
                    if (clTipoPrueba == "LABORAL-1") {
                        var win = window.open("Pruebas/VentanaPersonalidadLaboralI.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "INTERES") {
                        var win = window.open("Pruebas/VentanaInteresesPersonales.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "LABORAL-2") {
                        var win = window.open("Pruebas/VentanaPersonalidadLaboralII.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "PENSAMIENTO") {
                        var win = window.open("Pruebas/VentanaEstiloDePensamiento.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "APTITUD-1") {
                        var win = window.open("Pruebas/VentanaAptitudMentalI.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "APTITUD-2") {
                        var win = window.open("Pruebas/VentanaAptitudMentalII.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "TIVA") {
                        var win = window.open("Pruebas/VentanaTIVA.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "ORTOGRAFIA-1") {
                        var win = window.open("Pruebas/VentanaOrtografiaI.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "ORTOGRAFIA-2") {
                        var win = window.open("Pruebas/VentanaOrtografiaII.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "ORTOGRAFIA-3") {
                        var win = window.open("Pruebas/VentanaOrtografiaIII.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "TECNICAPC") {
                        var win = window.open("Pruebas/VentanaTecnicaPC.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba == "REDACCION") {
                        var win = window.open("Pruebas/VentanaRedaccion.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", "_blank");
                        win.focus();
                    }
                    else if ((clTipoPrueba == "INGLES")) {
                        var win = window.open("Pruebas/VentanaIngles.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba == "ENTREVISTA" && (tipoPrueba == "MANUAL" || tipoPrueba == null)) {
                        openChildDialog("VentanaResultadosEntrevista.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno + "&&CL=EDIT", 'RWpruebas');
                    }

                    else if ((clTipoPrueba == "ADAPTACION" && tipoPrueba == "MANUAL") || (clTipoPrueba == "ADAPTACION" && tipoPrueba == null)) {
                        var win = window.open("VentanaIntegracionMedioManual.aspx?ID=" + idPrueba + "&T=" + vclTokenExterno + "&MOD=EDIT", '_blank');
                        win.focus();
                    }
                    else { alert("La prueba fue capturada manualmente."); }
                }
                else { radalert("No se ha seleccionado una prueba.", 400, 150, ""); }
            }

            function VerResultados() {
                obtenerIdFila();
                if ((vFlBateria != "")) {
                    var win = window.open("ResultadosPruebas.aspx?ID=" + vFlBateria + "&&T=" + vclTokenExterno, '_blank');
                    win.focus();
                }
                else { radalert("No se ha seleccionado una batería.", 400, 150, ""); }
            }

            //function sustitucionBaremos() {

            //    var win = window.open("SustitucionBaremos.aspx", '_blank');
            //    win.focus();
            //}

            function RowSelecting(sender, eventArgs) {
                var tableView = eventArgs.get_tableView();

                if (eventArgs.get_tableView().get_name() == "PruebaDetails") {
                    if (tableView.get_selectedItems().length == 1) {
                        tableView.clearSelectedItems();
                    }
                }

                var btnEnviar = $find('<%= btnEnviar.ClientID %>');
                var btnAplicacion = $find('<%= btnAplicacion.ClientID %>');
                var btnCapturaManual = $find('<%= btnCapturaManual.ClientID %>');
                var btnResultados = $find('<%= btnResultados.ClientID %>');
                var btnRevisar = $find('<%= btnRevisar.ClientID %>');
                var btnConsultas = $find('<%= btnConsultas.ClientID %>');
                var btnEliminar = $find('<%= btnEliminar.ClientID %>');
                var btnEditar = $find('<%= btnEditar.ClientID %>');
                var btnCorregir = $find('<%= btnCorregir.ClientID %>');

                var vTablaDestino = eventArgs.get_tableView().get_name();

                if (vTablaDestino == "PruebaDetails") {
                    
                    var id = eventArgs.getDataKeyValue("ID_PRUEBA");
                    document.getElementById("<%:hfSelectedDetailRow.ClientID%>").value = id;

                    btnEnviar.set_enabled(false);
                    btnAplicacion.set_enabled(false);
                    btnCapturaManual.set_enabled(true);
                    btnResultados.set_enabled(false);
                    btnRevisar.set_enabled(true);
                    btnConsultas.set_enabled(false);
                    btnEliminar.set_enabled(false);
                    btnEditar.set_enabled(false);
                    btnCorregir.set_enabled(true);

                }

                if (eventArgs.get_tableView().get_name() == "Baterias") {

                    var id = eventArgs.getDataKeyValue("ID_BATERIA");
                    document.getElementById("<%:hfSelectedRow.ClientID%>").value = id;

                    btnEnviar.set_enabled(true);
                    btnAplicacion.set_enabled(true);
                    btnCapturaManual.set_enabled(false);
                    btnResultados.set_enabled(true);
                    btnRevisar.set_enabled(false);
                    btnConsultas.set_enabled(true);
                    btnEliminar.set_enabled(true);
                    btnEditar.set_enabled(true);
                    btnCorregir.set_enabled(false);

                }

            }

            function RowDeselected(sender, eventArgs) {
                var tableView = eventArgs.get_tableView();

                if (eventArgs.get_tableView().get_name() == "PruebaDetails") {
                    if (tableView.get_selectedItems().length == 1) {
                        tableView.clearSelectedItems();
                    }
                }

                var vTablaDestino = eventArgs.get_tableView().get_name();

                if (eventArgs.get_tableView().get_name() == "Baterias") {                    
                    document.getElementById("<%:hfSelectedRow.ClientID%>").value = "";
                }

                if (vTablaDestino == "PruebaDetails") {
                    document.getElementById("<%:hfSelectedDetailRow.ClientID%>").value = "";
                }
            }

            function gridCreated(sender, eventArgs) {
                var masterTable = sender.get_masterTableView();
                var selectColumn = masterTable.getColumnByUniqueName("Select");
                var headerCheckBox = $(selectColumn.get_element()).find("[type=checkbox]")[0];

                if (headerCheckBox) {
                    headerCheckBox.checked = masterTable.get_selectedItems().length ==
                        masterTable.get_dataItems().length;
                }
            }

            function rowCreated(sender, args) {
                var id = 0;
                var tableView = args.get_tableView();

                

                var vTablaDestino = args.get_tableView().get_name();

                if (vTablaDestino == "Baterias") {
                    var IdBateria = document.getElementById("<%:hfSelectedRow.ClientID%>").value;
                    id = args.getDataKeyValue("ID_BATERIA");

                    if (id === IdBateria) {
                        args.get_gridDataItem().set_selected(true);
                    }
                 }

                 if (vTablaDestino == "PruebaDetails") {
                     var IdPrueba = document.getElementById("<%:hfSelectedDetailRow.ClientID%>").value;
                     id = args.getDataKeyValue("ID_PRUEBA");

                     if (id === IdPrueba) {
                         args.get_gridDataItem().set_selected(true);
                     }
                }



            }

            function showPopupConsultas() {
                obtenerIdFila();
                console.info(vFlBateria);
                if ((vFlBateria != "")) {
                    openConsultas();
                }
                else {
                    radalert("No se ha seleccionado una batería.", 400, 150, "");
                }
            }

            function openConsultas() {
                var vURL = "ConsultasPersonales.aspx";
                var vTitulo = "Consultas personales";

                vURL = vURL + "?pIdBateria=" + vFlBateria;
                var windowProperties = {};
                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                var wnd = openChildDialog(vURL, "rwConsultas", vTitulo, windowProperties);
                vFlBateria = "";
            }

            function BienvenidaBateria()
            {
                obtenerIdFila();
                if ((vFlBateria != "")) {
                    //var win = window.open("Pruebas/PruebaBienvenida.aspx?ID=" + vFlBateria + "&T=" + vclTokenExterno + "&ty=sig", '_self', true);
                    var win = window.open("Pruebas/PruebaBienvenida.aspx?ID=" + vFlBateria + "&T=" + vclTokenExterno + "&idCandidato=" + idCandidato, '_self', true);
                    win.focus();
                }
                else {
                    radalert("No has seleccionado una batería de pruebas.", 400, 150, "");
                }
            }
            function OpenWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
            }

            function OpenEditarPruebasWindow(sender, args) {
                obtenerIdFila();
                if (vFlBateria != "") {
                    OpenWindow(EditarPruebasWindowProperties(vFlBateria));
                }
                else {
                    radalert("Selecciona una batería", 400, 150, "Error");
                }
            }

            function OpenAgregarPruebasWindow(sender, args) {
                OpenWindow(GetPruebasWindowProperties());
            }
            function GetWindowProperties() {
                return {
                    width: document.documentElement.clientWidth - 300,
                    height: document.documentElement.clientHeight - 20
                };
            }

            function GetPruebasWindowProperties() {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Agregar pruebas";
                wnd.vRadWindowId = "winPruebas";
                wnd.vURL = "/IDP/AgregarPruebas.aspx";
                return wnd;
            }

            function EditarPruebasWindowProperties(vFlBateria) {
                var wnd = GetWindowProperties();
                    wnd.vTitulo = "Editar bateria";
                    wnd.vRadWindowId = "winPruebas";
                    wnd.vURL = "/IDP/AgregarPruebas.aspx?pIdBateria=" + vFlBateria;
                    return wnd;
            }


            function OpenConfirmDelete(sender, args) {
                obtenerIdFila();
                if (vFlBateria != "") {
                            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                                if (shouldSubmit) {
                                    this.click();
                                }
                            });
                            radconfirm("¿Deseas eliminar la batería?, este proceso no podrá revertirse.", callBackFunction, 400, 170, null, "Eliminar batería");
                            args.set_cancel(true);
                    } else {
                        radalert("Selecciona una batería", 400, 150, "Error");
                        args.set_cancel(true);
                    }
            }
            
            window.onload = OnLoadPage;

        </script>
    </telerik:RadCodeBlock>

    <label class="labelTitulo">Evaluación de candidatos</label>

    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splSolicitudes" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Vertical">
            <telerik:RadPane ID="rpnGridPruebas" runat="server">
                <telerik:RadGrid
                    ID="grdPruebas"
                    runat="server"
                    AutoGenerateColumns="false"
                    EnableHeaderContextMenu="true"
                    Height="100%"
                    AllowSorting="true"
                    HeaderStyle-Font-Bold="true"
                    OnNeedDataSource="grdPruebas_NeedDataSource"
                    OnDetailTableDataBind="grdPruebas_DetailTableDataBind"
                    OnItemCommand="grdPruebas_ItemCommand"
                    AllowMultiRowSelection="true" RetainExpandStateOnRebind="true">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowSelecting="RowSelecting" OnRowDeselected="RowDeselected" OnRowCreated="rowCreated" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_BATERIA,CL_TOKEN,ID_CANDIDATO, FE_TERMINO" Name="Baterias"
                        EnableColumnsViewState="false" DataKeyNames="ID_BATERIA,CL_TOKEN,ID_CANDIDATO, FE_TERMINO" AllowPaging="true"
                        AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" PageSize="20">
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="ID_PRUEBA"
                                ClientDataKeyNames="ID_PRUEBA,CL_TOKEN_EXTERNO,NB_TIPO_PRUEBA,CL_PRUEBA, FE_TERMINO, ID_BATERIA"  Name="PruebaDetails" Width="100%">
                                <Columns>
                                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35" UniqueName="Select"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Tipo prueba" DataField="CL_PRUEBA" UniqueName="CL_PRUEBA"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre prueba" DataField="NB_PRUEBA" UniqueName="NB_PRUEBA"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="50" HeaderText="Estatus" DataField="CL_ESTADO" UniqueName="CL_ESTADO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="50" HeaderText="Tiempo de prueba" DataField="NO_TIEMPO_PRUEBA" UniqueName="NO_TIEMPO_PRUEBA" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Fecha de aplic." DataField="FE_TERMINO" UniqueName="FE_TERMINO" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" Display="true" HeaderStyle-Width="120" FilterControlWidth="50" HeaderText="Tipo" DataField="NB_TIPO_PRUEBA" UniqueName="NB_TIPO_PRUEBA"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="90" FilterControlWidth="30" HeaderText="Aplica" DataField="FG_ASIGNADA" UniqueName="FG_ASIGNADA"></telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn UniqueName="ColumnPruebaEliminar" Text="Eliminar respuestas" CommandName="DeletePrueba" ConfirmText="Este proceso borrará las respuestas de la prueba seleccionada ¿Desea continuar?">
                                        <ItemStyle Width="50px" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Folio de batería" DataField="FL_BATERIA" UniqueName="FL_BATERIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" Display="false" DataField="CL_TOKEN" UniqueName="CL_TOKEN"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ColumnGroupName="Correo" Visible="true" Display="true" UniqueName="CL_CORREO_ELECTRONICO" DataField="CL_CORREO_ELECTRONICO" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Correo">
                                <ItemTemplate>
                                    <telerik:RadTextBox runat="server" ID="txtCorreo" Width="100%" Text='<%# Bind("CL_CORREO_ELECTRONICO") %>' AutoPostBack="false"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Correo" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Estatus" DataField="ESTATUS" UniqueName="ESTATUS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Fecha de aplic." DataField="FE_TERMINO" UniqueName="FE_TERMINO" DataFormatString="{0:dd/MM/yyyy HH:mm}"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="Enviado" DataField="FG_ENVIO_CORREO" UniqueName="FG_ENVIO_CORREO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                            <telerik:GridButtonColumn UniqueName="ColumnBateriaEliminar" Text="Eliminar respuestas" CommandName="DeleteBateria" ConfirmText="Este proceso borrará las respuestas de todas las pruebas de la batería ¿Desea continuar?">
                                <ItemStyle Width="120px" />
                                        <HeaderStyle Width="120px" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Crear batería" ID="btnAddTest" OnClientClicked="OpenAgregarPruebasWindow" AutoPostBack="false" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Enviar correo a batería seleccionada" ID="btnEnviar" OnClick="btnEnviar_Click" AutoPostBack="true" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Aplicación" ID="btnAplicacion" OnClientClicked="BienvenidaBateria" AutoPostBack="false" />
    </div>
    <div class="ctrlBasico">
<telerik:RadButton runat="server" Text="Captura manual" ID="btnCapturaManual"  AutoPostBack="false"  OnClientClicked="IniciarPruebaManual"  />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Resultados" ID="btnResultados" OnClientClicked="VerResultados" AutoPostBack="false" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Revisar prueba" ID="btnRevisar" OnClientClicked="RevisarPrueba" AutoPostBack="false" />
    </div>

  <%--  <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Sustitución de baremos" ID="btnBaremos" OnClientClicked="sustitucionBaremos" AutoPostBack="false"></telerik:RadButton>
    </div>--%>

    <div class="ctrlBasico">
        <telerik:RadButton ID="btnConsultas" AutoPostBack="false" runat="server" Text="Consultas personales"  OnClientClicked="showPopupConsultas"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" AutoPostBack="false" runat="server" Text="Editar batería" OnClientClicked="OpenEditarPruebasWindow"></telerik:RadButton>
    </div>
     <div class="ctrlBasico">
        <telerik:RadButton ID="btnCorregir" AutoPostBack="false" runat="server" Text="Corregir prueba"  OnClientClicking="CorregirRespuestas"></telerik:RadButton>
    </div>
     <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" AutoPostBack="true" runat="server" Text="Eliminar batería"  OnClientClicking="OpenConfirmDelete" OnClick="btnEliminar_Click"></telerik:RadButton>
    </div>
    <%--<div class="ctrlBasico">
        <telerik:RadButton ID="btnAplicacion2" AutoPostBack="false" runat="server" Text="Prueba aplicación" Width="200px" OnClientClicked="BienvenidaBateria"></telerik:RadButton>
    </div>--%>
    <!-- Fin Secciones de niveles -->

    <asp:HiddenField runat="server" ID="hfSelectedRow" />
    <asp:HiddenField runat="server" ID="hfSelectedDetailRow" />

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            
            <telerik:RadWindow ID="RWpruebasCorrecion"
                runat="server"
                Title="Pruebas"
                Left="5%"
                ReloadOnShow="true"
                ShowContentDuringLoad="false"
                VisibleStatusbar="false"
                VisibleTitlebar="true"
                Behaviors="Close"
                Modal="true"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RWpruebas"
                runat="server"
                Title="Pruebas"
                Height="600"
                Width="1000"
                Left="5%"
                ReloadOnShow="true"
                ShowContentDuringLoad="false"
                VisibleStatusbar="false"
                VisibleTitlebar="true"
                Behaviors="Close"
                Modal="true"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>

            <telerik:RadWindow
                ID="rwConsultas"
                runat="server"
                Title="Consultas Personales"
                Height="600px"
                Width="1100px"
                ReloadOnShow="false"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="Close">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPruebas" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Modal="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionCandidato" runat="server" Title="Seleccionar empleado" Height="600px" Width="1100px"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false"  Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%">
    </telerik:RadWindowManager>
</asp:Content>
