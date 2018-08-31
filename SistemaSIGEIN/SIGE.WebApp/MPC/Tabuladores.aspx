<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/MenuMC.master" AutoEventWireup="true" CodeBehind="Tabuladores.aspx.cs" Inherits="SIGE.WebApp.MPC.Tabuladores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .RadListViewContainer {
            width: 200px;
            border: 1px solid lightgray;
            float: left;
            margin: 5px;
            border-radius: 5px;
        }

            .RadListViewContainer.Selected {
                background-color: #0087CF !important;
                color: #fff !important;
            }

            .RadListViewContainer > .SelectionOptions {
                overflow: auto;
                background-color: lightgray;
                padding: 10px;
            }

        .labelSubtitulo {
            margin-top: 20px;
            display: block;
            font-size: 1.6em;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function AbrirVentanaNuevo() {
<<<<<<< HEAD
                openChildDialog("VentanaNuevoTabulador.aspx", "winMenuTabuladores",
                        "Nuevo/Editar tabulador")
=======
                var vPropierties = GetWindowProperties();
                vPropierties.width = 650;
                openChildDialog("VentanaNuevoTabulador.aspx", "winMenuTabuladores", "Nuevo/Editar tabulador", vPropierties)
>>>>>>> DEV
            }

            function onCloseWindow(sender, args) {
                var lista = $find("<%# rlvConsultas.ClientID %>");
                lista.rebind();
            }

            function OpenWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
            }

            function GetWindowProperties() {
                return {
                    width: document.documentElement.clientWidth - 30,
                    height: document.documentElement.clientHeight - 20
                };
            }

            function GetPeriodoId() {
                var listView = $find('<%= rlvConsultas.ClientID %>');
                var selectedIndex = listView.get_selectedIndexes();
                if (selectedIndex.length > 0)
                    return listView.get_clientDataKeyValue()[selectedIndex]["ID_TABULADOR"];
                else
                    return null;
            }

            function GetPeriodoNombre() {
                var listView = $find('<%= rlvConsultas.ClientID %>');
                var selectedIndex = listView.get_selectedIndexes();
                if (selectedIndex.length > 0)
                    return listView.get_clientDataKeyValue()[selectedIndex]["NB_TABULADOR"];
                else
                    return null;
            }


            function ConfirmarEliminar(sender, args) {
                var vPermisoEliminar = '<%= vFgEliminar%>';
                var vIdPeriodo = GetPeriodoId();
                var vNbPeriodo = GetPeriodoNombre();
                if (vPermisoEliminar == "True") {
                    if (vIdPeriodo != null) {

                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar la version ' + vNbPeriodo + ' del tabulador? Esta opción eliminará todos los datos relacionados con la versión y no podrá revertirse.', callBackFunction, 450, 200, null, "Eliminar versión del tabulador");
                        args.set_cancel(true);
                    }
                    else {
                        radalert("Seleccione una versión del tabulador.", 400, 200, "");
                        args.set_cancel(true);
                    }
                }
                else {
                    radalert("No tiene los permisos necesarios para llevar a cabo esta función.", 450, 200, "");
                    args.set_cancel(true);
                }

            }

            //function onCloseWindow(oWnd, args) {
            //    idTabulador = "";
            //    $find("<=rlvConsultas.ClientID%>").get_masterTableView().rebind();
            //}

            //    function confirmarEliminar(sender, args) {
            //var MasterTable = $find("<=grdTabuladores.ClientID %>").get_masterTableView();
            //var selectedRows = MasterTable.get_selectedItems();
            //var row = selectedRows[0];
            //if (row != null) {
            //    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "CL_TABULADOR");
            //    if (selectedRows != "") {
            //        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
            //        { if (shouldSubmit) { this.click(); } });
            //        radconfirm('¿Deseas eliminar el tabulador ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
            //        args.set_cancel(true);
            //    }
            //} else {
            //    radalert("Selecciona un tabulador", 400, 150, "Error");
            //    args.set_cancel(true);
            //}
            //    }

            function cambiarEstado(sender, args) {
                var vIdTabulador = GetPeriodoId();
                var vNbTabulador = GetPeriodoNombre();
                if (vIdTabulador != null) {

                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas cerrar la versión ' + vNbTabulador + ' ?', callBackFunction, 400, 170, null, "Cerrar versión del tabulador");
                    args.set_cancel(true);
                }
                else {
                    radalert("Seleccione una versión.", 400, 150, "");
                    args.set_cancel(true);
                }
            }

            //var MasterTable = $find("<=grdTabuladores.ClientID %>").get_masterTableView();
            //var selectedRows = MasterTable.get_selectedItems();
            //var row = selectedRows[0];
            //if (row != null) {
            //    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "CL_TABULADOR");
            //    if (selectedRows != "") {
            //        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
            //        { if (shouldSubmit) { this.click(); } });
            //        radconfirm('¿Deseas cambiar el estado del tabulador ' + CELL_NOMBRE.innerHTML + ' a Cerrado (Abierto)?', callBackFunction, 400, 170, null, "Cambiar estado");
            //        args.set_cancel(true);
            //    }
            //} else {
            //    radalert("Selecciona un tabulador", 400, 150, "Error");
            //    args.set_cancel(true);
            //}
            //    }

            //  var idTabulador = "";

            // function obtenerIdFila() {
            //   var grid = $find("<=grdTabuladores.ClientID %>");
            //  var MasterTable = grid.get_masterTableView();
            // var selectedRows = MasterTable.get_selectedItems();
            // if (selectedRows.length != 0) {
            //   var row = selectedRows[0];
            //    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
            //    console.info(SelectDataItem);
            //    idTabulador = SelectDataItem.getDataKeyValue("ID_TABULADOR");
            //    vNombre = MasterTable.getCellByColumnUniqueName(SelectDataItem, "CL_TABULADOR").innerHTML;
            //}
            //   }

            function EditarTabulador() {
                // OpenPeriodoWindow(GetPeriodoId(), "EDITA");
                var vPermisoEditar = '<%= vFgEditar%>';
                var vIdTabulador = GetPeriodoId();
                if (vPermisoEditar == "True") {
                    if (vIdTabulador != "" && vIdTabulador != null) {
                        openChildDialog("VentanaNuevoTabulador.aspx?&ID=" + vIdTabulador, "winMenuTabuladores",
                            "Nuevo/Editar tabulador ")
                    }
                }
                else {
                    radalert("No tiene los permisos necesarios para llevar a cabo esta función.", 450, 200, "");
                }
            }

            function OpenPeriodoWindow(pIdPeriodo, pTipoTarea) {
                OpenWindow(GetPeriodoWindowProperties(pIdPeriodo, pTipoTarea));
            }

            function GetPeriodoWindowProperties(pIdPeriodo, pTipoTarea) {
                var wnd = GetWindowProperties();
                wnd.width = 750;
                wnd.height = 550;
                wnd.vTitulo = "Nuevo/Editar tabulador";
                wnd.vRadWindowId = "winMenuTabuladores";
                wnd.vURL = "VentanaNuevoTabulador.aspx";
                if (pIdPeriodo != null) {
                    if (pTipoTarea != "COPIA") {
                        wnd.vURL += String.format("?ID={0}", pIdPeriodo);
                        wnd.vTitulo = "Editar versión del tabulador";
                    }
                    else {
                        wnd.vURL += String.format("?ID={0}&TipoTarea={1}", pIdPeriodo, pTipoTarea);
                        wnd.vTitulo = "Copiar período";
                    }
                }
                return wnd;
            }

            function CopiarTabulador() {
                var vIdTabulador = GetPeriodoId();
                var vNombre = GetPeriodoNombre();
                if (vIdTabulador != null) {
                    openChildDialog("VentanaCopiarTabulador.aspx?&ID=" + vIdTabulador, "winMenuTabuladores", "Copiar tabulador '" + vNombre + "'")
                }
                else
                    radalert("Selecciona un tabulador.", 400, 150);
                //else {
                //    radalert("Seleccione una versión del tabulador.", 450, 200, "");
                //    args.set_cancel(true);
                //}
                //if (vIdTabulador != "") {
                //    openChildDialog("VentanaCopiarTabulador.aspx?&ID=" + vIdTabulador, "winMenuTabuladores",
                //        "Copiar tabulador '" + vNombre + "'")
                //} else {
                //    radalert("Selecciona un tabulador.", 400, 150, "Error");
                //    args.set_cancel(true);
                //}
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            function OpenWindowConfigurar() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetConfiguracionWindowProperties(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConfiguracionWindowProperties(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Configurar tabulador";
                var myUrl = '<%= ResolveClientUrl("VentanaConfigurarTabulador.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladoresConfig";
                return wnd;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////
            function OpenWindowConsultar() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "")
                    OpenWindow(GetConsultarWindowProperties(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowProperties(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Consultas";
                wnd.vURL = "/MPC/Consultas.aspx?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////
            function OpenWindowNiveles() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetNivelesWindowProperties(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetNivelesWindowProperties(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Nivel";
                var myUrl = '<%= ResolveClientUrl("TabuladorNivel.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            function OpenWindowMercado() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetMercadoWindowProperties(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }
            function GetMercadoWindowProperties(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Mercado salarial";
                var myUrl = '<%= ResolveClientUrl("MercadoSalarial.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            function OpenWindowValuacion() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetValuacionWindowProperties(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }
            function GetValuacionWindowProperties(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Valuación puestos";
                var myUrl = '<%= ResolveClientUrl("ValuacionPuestosTabuladores.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            function OpenWindowTabuladorMaestro() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetTabuladorMaestroWindowProperties(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }
            function GetTabuladorMaestroWindowProperties(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Tabulador maestro";
                var myUrl = '<%= ResolveClientUrl("TabuladorMaestro.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            function OpenWindowPlaneacion() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetPlaneacionWindowProperties(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }
            function GetPlaneacionWindowProperties(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Planeación de incrementos";
                var myUrl = '<%= ResolveClientUrl("VentanaPlaneacionIncrementos.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////
            function OpenWindowTablero() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetConsultarWindowPropertiesSueldos(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowPropertiesSueldos(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Consulta tabulador - Tabuladores";
                var myUrl = '<%= ResolveClientUrl("ConsultaTabuladorSueldos.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }


            //////////////////////////////////////////////////////////////////////////////////////////////

            function OpenWindowAnalisis() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetConsultarWindowPropertiesAnalisis(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowPropertiesAnalisis(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Consulta gráfica de análisis - Tabuladores";
                var myUrl = '<%= ResolveClientUrl("ConsultaGraficaAnalisis.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }


            //////////////////////////////////////////////////////////////////////////////////////////////

            function OpenWindowDesviaciones() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "" && vIdTabulador != null)
                    OpenWindow(GetConsultarWindowPropertiesDesviaciones(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowPropertiesDesviaciones(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Consulta análisis de desviaciones - Tabuladores";
                var myUrl = '<%= ResolveClientUrl("ConsultaAnalisisDesviaciones.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////

            function OpenWindowTabuladorMaestroCons() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "")
                    OpenWindow(GetConsultarWindowPropertiesTabMaestro(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowPropertiesTabMaestro(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Análisis de desviaciones";
                var myUrl = '<%= ResolveClientUrl("ConsultaTabuladorMaestro.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////

            function OpenWindowTabuladorMercadoCons() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "")
                    OpenWindow(GetConsultarWindowPropertiesMercadoCons(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowPropertiesMercadoCons(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Mercado salarial";
                var myUrl = '<%= ResolveClientUrl("ConsultaMercadoSalarial.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////

            function OpenWindowIncrementosCons() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "")
                    OpenWindow(GetConsultarWindowPropertiesIncrementosCons(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowPropertiesIncrementosCons(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Planeación de incrementos";
                var myUrl = '<%= ResolveClientUrl("ConsultaIncrementos.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////

            function OpenWindowComparacion() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "")
                    OpenWindow(GetConsultarWindowPropertiesComparacion(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowPropertiesComparacion(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Comparación de sueldo";
                var myUrl = '<%= ResolveClientUrl("ConsultaComparacionSueldo.aspx") %>';
                wnd.vURL = myUrl + "?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////

            function OpenWindowBono() {
                var vIdTabulador = GetPeriodoId();
                if (vIdTabulador != "")
                    OpenWindow(GetConsultarWindowPropertiesBono(vIdTabulador));
                else
                    radalert("Selecciona un tabulador.", 400, 150);
            }

            function GetConsultarWindowPropertiesBono(pIdTabulador) {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Bono";
                wnd.vURL = "/MPC/ConsultaBono.aspx?ID=" + pIdTabulador;
                wnd.vRadWindowId = "WinTabuladores";
                return wnd;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////



            function useDataFromChild(pDato) {
                if (pDato != null) {
                    switch (pDato[0].accion) {
                        case "ACTUALIZARLISTA":
                            var ajaxManager = $find('<%= ramTabulador.ClientID%>');
                            ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "ACTUALIZARLISTA" }));
                            break;
                        case "ACTUALIZAR":
                            var ajaxManager = $find('<%= ramTabulador.ClientID%>');
                            ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "ACTUALIZAR" }));
                            //var list = $find('<= rlvConsultas.ClientID %>');
                            //list.rebind();
                            break;
                    }
                }
            }

            function CloseWindowConfig() {
                var ajaxManager = $find('<%= ramTabulador.ClientID%>');
                ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "CONFIGURACION" }));
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpVersionesTabulador" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramTabulador" runat="server" OnAjaxRequest="ramTabulador_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rlvConsultas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCambiarEstado" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtClPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtDsPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtClEstatus" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtNotas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtTipo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtUsuarioMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtFechaMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnValuar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnIncrementos" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCapturar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCrear" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramTabulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtClPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtDsPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtClEstatus" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtNotas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtTipo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtUsuarioMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtFechaMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />

                     <telerik:AjaxUpdatedControl ControlID="btnCambiarEstado" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnValuar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnIncrementos" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCapturar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCrear" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rlvConsultas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbDescendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rfFiltros"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCambiarEstado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas" />
                    <telerik:AjaxUpdatedControl ControlID="btnCambiarEstado" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnValuar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnIncrementos" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCapturar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCrear" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>

                    <%-- <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReabir">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvConsultas" />
                    <telerik:AjaxUpdatedControl ControlID="btnCambiarEstado" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnValuar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnIncrementos" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCapturar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCrear" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="rsPeriodosEvaluacion" BorderSize="0" Width="100%" Height="100%" runat="server">
        <telerik:RadPane ID="rpPeriodosEvaluacion" runat="server">
            <label class="labelTitulo">Tabuladores</label>
            <div class="ctrlBasico">
                <telerik:RadListView ID="rlvConsultas" runat="server" DataKeyNames="ID_TABULADOR, CL_ESTADO" ClientDataKeyNames="ID_TABULADOR, NB_TABULADOR, CL_ESTADO"
                    OnNeedDataSource="rlvConsultas_NeedDataSource" OnItemCommand="rlvConsultas_ItemCommand" AllowPaging="true" ItemPlaceholderID="ProductsHolder">
                    <LayoutTemplate>
                        <div style="overflow: auto; width: 700px;">
                            <div style="overflow: auto; overflow-y: auto; max-height: 450px;">
                                <asp:Panel ID="ProductsHolder" runat="server"></asp:Panel>
                            </div>
                            <div style="clear: both;"></div>
                            <telerik:RadDataPager ID="rdpConsultas" runat="server" PagedControlID="rlvConsultas" PageSize="6" Width="630">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric" PageButtonCount="5"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerGoToPageField CurrentPageText="Página:" TotalPageText="de" SubmitButtonText="Ir"></telerik:RadDataPagerGoToPageField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="RadListViewContainer">
                            <div style="padding: 10px;">
                                <div style="overflow: auto; overflow-y: auto; height: 35px;">
                                    <label>Versión:</label>
                                    <%# Eval("CL_TABULADOR") %>
                                </div>
                                <div>
                                    <label>Estatus:</label>
                                    <%# Eval("CL_ESTADO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label>Descripción:</label>
                                    <%# Eval("NB_TABULADOR") %>
                                </div>
                            </div>
                            <%--<div>
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="font-size: 1.6em; width: 75px; text-align: center;"><%# Eval("ID_TABULADOR") %></div>
                                            </td>
                                            <td>
                                                <div style="overflow: auto; overflow-y: auto; overflow-x: auto; height: 35px"><%# Eval("CL_TABULADOR") %> </div>
                                            </td>
                                        </tr>
                                        <%--  <tr>
                                            <td><%# ((DateTime)Eval("FE_INICIO")).ToString("MMMM yyyy") %></td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <# Eval("CL_ESTADO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <# Eval("DS_TABULADOR") %>
                                </div>--%>
                            <div class="SelectionOptions">
                                <telerik:RadButton ID="btnSeleccionar" runat="server" CommandName="Select" Text="Seleccionar" Width="100%"></telerik:RadButton>
                            </div>
                        </div>
                    </ItemTemplate>
                    <SelectedItemTemplate>
                        <div class="RadListViewContainer Selected">
                            <div style="padding: 10px;">
                                <div style="overflow: auto; overflow-y: auto; height: 35px;">
                                    <label style="color: white;">Versión:</label>
                                    <%# Eval("CL_TABULADOR") %>
                                </div>
                                <div>
                                    <label style="color: white;">Estatus:</label>
                                    <%# Eval("CL_ESTADO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label style="color: white;">Descripción:</label>
                                    <%# Eval("NB_TABULADOR") %>
                                </div>
                                <%--                  <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="font-size: 1.6em; width: 75px; text-align: center;"><%# Eval("ID_TABULADOR") %></div>
                                            </td>
                                            <td>
                                                <div style="overflow: auto; overflow-y: auto; overflow-x: auto; height: 35px"><%# Eval("CL_TABULADOR") %> </div>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td><%# ((DateTime)Eval("FE_INICIO")).ToString("MMMM yyyy") %></td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <# Eval("CL_ESTADO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <# Eval("DS_TABULADOR") %>
                                </div>--%>
                            </div>
                            <div class="SelectionOptions">
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnModificar" runat="server" Text="Editar" OnClientClicked="EditarTabulador" Width="100%"></telerik:RadButton>
                                </div>
                                <div style="width: 50%; float: left; padding-left: 5px;">
                                    <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100%" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </SelectedItemTemplate>
                    <EmptyDataTemplate>
                        <div class="RadListViewContainer" style="overflow: auto; text-align: center; width: 660px; height: 100px;">
                            No hay versiones disponibles
                        </div>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
            <div class="ctrlBasico" style="text-align: center">
                <telerik:RadTabStrip runat="server" ID="rtsPeriodos" MultiPageID="rmpPeriodos" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Gestionar"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Detalles"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: 10px;"></div>
                <telerik:RadMultiPage runat="server" ID="rmpPeriodos" SelectedIndex="0" Height="100%" Width="100%">
                    <telerik:RadPageView ID="rpvPeriodos" runat="server">
                        <div>
                            <label class="labelTitulo">Administrar</label>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="AbrirVentanaNuevo"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnConfigurar" runat="server" Text="Configurar" AutoPostBack="false" OnClientClicked="OpenWindowConfigurar"></telerik:RadButton>
                                <%--  &nbsp;&nbsp;
                            <telerik:RadButton ID="btnVerNiveles" runat="server" Text="Ver niveles" AutoPostBack="false" OnClientClicked="OpenWindowNiveles"></telerik:RadButton>--%>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCambiarEstado" runat="server" Text="Cerrar" OnClientClicking="cambiarEstado" OnClick="btnCambiarEstado_Click"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnReabrir" runat="server" Text="Re abrir" OnClick="btnReabrir_Click"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCopiar" runat="server" Text="Copiar" AutoPostBack="false" OnClientClicked="CopiarTabulador"></telerik:RadButton>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div>
                            <label class="labelTitulo">Procesos</label>
                            <telerik:RadButton ID="btnValuar" runat="server" Text="Valuar puestos" AutoPostBack="false" OnClientClicking="OpenWindowValuacion"></telerik:RadButton>
                            &nbsp;
                                <telerik:RadButton ID="btnCapturar" runat="server" Text="Integrar mercado salarial" AutoPostBack="false" OnClientClicking="OpenWindowMercado"></telerik:RadButton>
                            <div style="clear: both; height: 10px;"></div>
                            <telerik:RadButton ID="btnCrear" runat="server" Text="Validar tabulador maestro" AutoPostBack="false" OnClientClicking="OpenWindowTabuladorMaestro"></telerik:RadButton>
                            &nbsp;
                            <telerik:RadButton ID="btnIncrementos" runat="server" Text="Planear incrementos" AutoPostBack="false" OnClientClicking="OpenWindowPlaneacion"></telerik:RadButton>
                        </div>
                        <div style="height: 10px; clear: both;"></div>
                        <div>
                            <label class="labelTitulo">Consultas</label>
                            <%-- <telerik:RadButton ID="btnConsultas" runat="server" Text="Consultas" AutoPostBack="false" OnClientClicking="OpenWindowConsultar"></telerik:RadButton>
                             &nbsp;&nbsp;--%>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnConsSueldos" runat="server" Text="Tabulador" AutoPostBack="false" OnClientClicking="OpenWindowTablero"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnGraficaAnalisis" runat="server" Text="Gráfica de análisis" AutoPostBack="false" OnClientClicking="OpenWindowAnalisis"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnDesviaciones" runat="server" Text="Análisis de desviaciones" AutoPostBack="false" OnClientClicking="OpenWindowDesviaciones"></telerik:RadButton>
                            </div>
                            <%-- <div style="height: 10px; clear: both;"></div>
                        <telerik:RadButton ID="btnTabulador" runat="server" Text="Tabulador maestro" AutoPostBack="false"  OnClientClicking="OpenWindowTabuladorMaestroCons"></telerik:RadButton>
                        &nbsp;&nbsp;
                       <telerik:RadButton ID="btnMercado" runat="server" Text="Mercado Salarial" AutoPostBack="false"  OnClientClicking="OpenWindowTabuladorMercadoCons"></telerik:RadButton>
                             &nbsp;&nbsp;
                       <telerik:RadButton ID="btnIncrementos" runat="server" Text="Planeación de incrementos" AutoPostBack="false"  OnClientClicking="OpenWindowIncrementosCons"></telerik:RadButton>
                              <div style="height: 10px; clear: both;"></div>
                        <telerik:RadButton ID="btnComparacion" runat="server" Text="Comparación mercado" AutoPostBack="false"  OnClientClicking="OpenWindowComparacion"></telerik:RadButton>
                        &nbsp;&nbsp;
                       <telerik:RadButton ID="btnBono" runat="server" Text="Bono" AutoPostBack="false" OnClientClicking="OpenWindowBono"></telerik:RadButton>
                        </div>--%>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvInformacion" runat="server">
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="lblEvento" name="lblEvento" runat="server">Versión:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClPeriodo" Enabled="false" runat="server" TextMode="MultiLine" Width="350px" MaxLength="1000" Height="35"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label1" name="lblEvento" runat="server">Descripción:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtDsPeriodo" Enabled="false" runat="server" Width="350px" TextMode="MultiLine" MaxLength="1000" Height="35"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label4" name="lblEvento" runat="server">Notas:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtNotas" Enabled="false" runat="server" Width="350px" TextMode="MultiLine" Height="100px"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label2" name="lblEvento" runat="server">Estatus:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClEstatus" Enabled="false" runat="server" Width="200px" TextMode="MultiLine" MaxLength="1000" Height="35"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label3" name="lblEvento" runat="server">Tipo de puestos:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtTipo" Enabled="false" runat="server" Width="350px" TextMode="MultiLine" MaxLength="1000" Height="35"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label5" name="lblCurso" runat="server">Último usuario que modifica:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtUsuarioMod" Enabled="false" runat="server" Width="105px" TextMode="MultiLine" MaxLength="1000" Height="35"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label6" name="lblCurso" runat="server">Última fecha de modificación:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtFechaMod" Enabled="false" runat="server" Width="105px" TextMode="MultiLine" MaxLength="1000" Height="35"></telerik:RadTextBox>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>

        </telerik:RadPane>
        <telerik:RadPane ID="rpOrdenarFiltrar" runat="server" Scrolling="None" Width="20px">
            <telerik:RadSlidingZone ID="rszOrdenarFiltrar" runat="server" SlideDirection="Left" ClickToOpen="true" ExpandedPaneId="rsVersionesTabulador" Width="20px">
                <telerik:RadSlidingPane ID="rspOrdenarFiltrar" runat="server" Title="Ordenar y filtrar" Width="450px" RenderMode="Mobile" Height="100%">
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <fieldset>
                            <legend>Ordenar por:</legend>
                            <telerik:RadComboBox ID="cmbOrdenamiento" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Clave de la versión" Value="CL_TABULADOR" />
                                    <telerik:RadComboBoxItem Text="Nombre de la versión" Value="NB_TABULADOR" />
                                    <telerik:RadComboBoxItem Text="Estatus" Value="CL_ESTADO" />
                                    <telerik:RadComboBoxItem Text="Fecha última de modificación" Value="FE_ULTIMA_MODIFICACION" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadButton ID="rbAscendente" runat="server" ButtonType="ToggleButton" ToggleType="Radio" Text="Ascendente" GroupName="ordenamiento" OnCheckedChanged="rbAscendente_CheckedChanged"></telerik:RadButton>
                            <telerik:RadButton ID="rbDescendente" runat="server" ButtonType="ToggleButton" ToggleType="Radio" Text="Descendente" GroupName="ordenamiento" OnCheckedChanged="rbDescendente_CheckedChanged"></telerik:RadButton>
                        </fieldset>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <fieldset>
                            <legend>Filtrar por:</legend>
                            <telerik:RadFilter runat="server" ID="rfFiltros" ExpressionPreviewPosition="Top" ApplyButtonText="Filtrar" OnApplyExpressions="rfFiltros_ApplyExpressions">
                                <FieldEditors>
                                    <telerik:RadFilterTextFieldEditor DataType="System.Int32" DisplayName="No." FieldName="ID_TABULADOR" DefaultFilterFunction="Contains" ToolTip="Número de la versión" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Clave" FieldName="CL_TABULADOR" DefaultFilterFunction="Contains" ToolTip="Clave de de la versión" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Nombre" FieldName="NB_TABULADOR" DefaultFilterFunction="Contains" ToolTip="Nombre de la versión" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Descripción" FieldName="DS_TABULADOR" DefaultFilterFunction="Contains" ToolTip="Descripción de de la versión" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.DateTime" DisplayName="F. Modificación" FieldName="FE_ULTIMA_MODIFICACION" DefaultFilterFunction="GreaterThanOrEqualTo" ToolTip="Fecha útima de modificación" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Estatus" FieldName="CL_ESTADO" DefaultFilterFunction="Contains" ToolTip="Estatus de la versión del tabulador" />
                                </FieldEditors>
                            </telerik:RadFilter>
                        </fieldset>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <div style="clear: both;"></div>
    <%--   <div style="height: calc(100% - 180px);">--%>
    <%--        <telerik:RadGrid ID="grdTabuladores"
            runat="server"
            Height="100%"
            AllowSorting="true"
            HeaderStyle-Font-Bold="true"
            OnNeedDataSource="grdTabuladores_NeedDataSource"
            AutoGenerateColumns="false"
            OnSelectedIndexChanged="grdTabuladores_SelectedIndexChanged">
            <ClientSettings EnablePostBackOnRowClick="true">
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_TABULADOR" ClientDataKeyNames="ID_TABULADOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Tabulador" DataField="CL_TABULADOR" UniqueName="CL_TABULADOR"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="260" FilterControlWidth="240" HeaderText="Descripción" DataField="DS_TABULADOR" UniqueName="DS_TABULADOR"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true"  Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Vigencia" DataField="FE_VIGENCIA" UniqueName="FE_VIGENCIA"  DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                     <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Puestos" DataField="CL_TIPO_PUESTO_VW" UniqueName="CL_TIPO_PUESTO_VW"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Estatus" DataField="CL_ESTADO" UniqueName="CL_ESTADO"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Última fecha de modificación" DataField="FE_ULTIMA_MODIFICACION" UniqueName="FE_ULTIMA_MODIFICACION" DataFormatString="{0:d}" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="último usuario que modifica" DataField="CL_USUARIO_APP_MODIFICA" UniqueName="CL_USUARIO_APP_MODIFICA"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>--%>
    <%--    </div>--%>

    <%--    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton
            ID="btnAgregar"
            runat="server"
            name="btnAgregar"
            AutoPostBack="false"
            Text="Agregar"
            Width="100"
            OnClientClicked="AbrirVentanaNuevo">
        </telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton
            ID="btnCopiar"
            runat="server"
            name="btnCopiar"
            AutoPostBack="false"
            Text="Copiar de"
            Width="100"
            OnClientClicking="CopiarTabulador">
        </telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton
            ID="btnEditar"
            runat="server"
            name="btnEditar"
            AutoPostBack="false"
            Text="Editar"
            Width="100"
            OnClientClicking="EditarTabulador">
        </telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton
            ID="btnEliminar"
            runat="server"
            name="btnEliminar"
            AutoPostBack="true"
            Text="Eliminar"
            Width="100"
            OnClick="btnEliminar_Click"
            OnClientClicking="confirmarEliminar">
        </telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton
            ID="btnConfigurar"
            runat="server"
            name="btnConfigurar"
            AutoPostBack="false"
            Text="Configurar"
            Width="100"
            OnClientClicking="OpenWindowConfigurar">
        </telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton
            ID="btnConsultas"
            runat="server"
            name="btnConsultas"
            AutoPostBack="false"
            Text="Consultas"
            Width="100"
            OnClientClicking="OpenWindowConsultar">
        </telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton
            ID="btnCambiarEstado"
            runat="server"
            name="btnCambiarEstado"
            AutoPostBack="true"
            Text="Cambiar estatus"
            Width="150"
            OnClientClicking="cambiarEstado"
            OnClick="btnCambiarEstado_Click">
        </telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton
            ID="btnVerNiveles"
            runat="server"
            name="btnVerNiveles"
            AutoPostBack="false"
            Text="Ver niveles"
            Width="150"
            OnClientClicking="OpenWindowNiveles">
        </telerik:RadButton>
    </div>

    <div style="clear: both; height: 10px;"></div>


    <div style="border-left:solid 0.5px gray; border-bottom:solid 0.5px gray; border-top:solid 0.5px gray; border-radius:5px 5px 5px 5px; width:281px; height:36px; float:left;">
    <div style="margin-left:10px; width:160px; float:left;">
        <telerik:RadButton RenderMode="Lightweight" ID="chbValuacionPuestos" runat="server" ReadOnly="true" ToggleType="CheckBox" ButtonType="ToggleButton"
            AutoPostBack="false">
            <ToggleStates>
                <telerik:RadButtonToggleState Text="Valuación de puestos"></telerik:RadButtonToggleState>
            </ToggleStates>
        </telerik:RadButton>
   </div>

    <div style="width:110px; float:left;">
        <telerik:RadButton
            ID="btnValuar"
            runat="server"
            name="btnValuar"
            AutoPostBack="false"
            Text="Valuar"
            Width="100"
            OnClientClicking="OpenWindowValuacion">
        </telerik:RadButton>
    </div>
        </div>
     <div style="border:solid 0.5px gray; border-radius:5px 5px 5px 5px; width:252px; height:36px; float:left;">
        <div style="margin-left:10px; width:130px; float:left;">
        <telerik:RadButton RenderMode="Lightweight" ID="chbMercadoSalarial" runat="server" ReadOnly="true" ToggleType="CheckBox" ButtonType="ToggleButton"
            AutoPostBack="false">
            <ToggleStates>
                <telerik:RadButtonToggleState Text="Mercado salarial"></telerik:RadButtonToggleState>
            </ToggleStates>
        </telerik:RadButton>
    </div>

    <div style="width:110px; float:left;">
        <telerik:RadButton
            ID="btnCapturar"
            runat="server"
            name="btnCapturar"
            AutoPostBack="false"
            Text="Capturar"
            Width="100"
            OnClientClicking="OpenWindowMercado">
        </telerik:RadButton>
    </div>
       </div>
     <div style="border-bottom:solid 0.5px gray; border-top:solid 0.5px gray; border-radius:5px 5px 5px 5px; width:271px; height:36px; float:left;">
    <div style="margin-left:10px; width:150px; float:left;">
        <telerik:RadButton RenderMode="Lightweight" ID="chbTabuladorMaestro" runat="server" ReadOnly="true" ToggleType="CheckBox" ButtonType="ToggleButton"
            AutoPostBack="false">
            <ToggleStates>
                <telerik:RadButtonToggleState Text="Tabulador maestro"></telerik:RadButtonToggleState>
            </ToggleStates>
        </telerik:RadButton>
    </div>

    <div style="width:110px; float:left;">
        <telerik:RadButton
            ID="btnCrear"
            runat="server"
            name="btnCrear"
            AutoPostBack="false"
            Text="Crear"
            Width="100"
            OnClientClicking="OpenWindowTabuladorMaestro">
        </telerik:RadButton>
    </div>
</div>
     <div style="border:solid 0.5px gray; border-radius:5px 5px 5px 5px; width:302px; height:36px; float:left;">
        <div style="margin-left:10px; width:180px; float:left;">
            <telerik:RadButton RenderMode="Lightweight" ID="chbPlaneacionIncrementos" runat="server" ReadOnly="true" ToggleType="CheckBox" ButtonType="ToggleButton"
                AutoPostBack="false">
                <ToggleStates>
                    <telerik:RadButtonToggleState Text="Planeación incrementos"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>

    <div style="width:110px; float:left;">
        <telerik:RadButton
            ID="RadButton2"
            runat="server"
            name="btnCrear"
            AutoPostBack="false"
            Text="Crear"
            Width="100"
            OnClientClicking="OpenWindowPlaneacion">
        </telerik:RadButton>
    </div>
     </div>--%>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="winMenuTabuladores"
                Width="650px"
                Height="630px"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="close"
                Animation="Fade">
            </telerik:RadWindow>
        </Windows>
        <Windows>
            <telerik:RadWindow
                ID="WinTabuladores"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                Animation="Fade"
                Width="1350px"
                OnClientClose="onCloseWindow"
                Height="665px">
            </telerik:RadWindow>
        </Windows>
        <Windows>
            <telerik:RadWindow
                ID="WinTabuladoresConfig"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                Animation="Fade"
                Width="1350px"
                OnClientClose="CloseWindowConfig"
                Height="665px">
            </telerik:RadWindow>
        </Windows>
        <Windows>
            <telerik:RadWindow ID="winSeleccion"
                runat="server"
                Title="Seleccionar empleado"
                Height="630px"
                Width="1300px"
                ReloadOnShow="true"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="Close"
                Animation="Fade">
            </telerik:RadWindow>
        </Windows>
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
        </Windows>
        <Windows>
            <telerik:RadWindow ID="winIncrementoGeneral"
                runat="server"
                Title="Denifir incremento general"
                Height="450px"
                Width="450px"
                ReloadOnShow="true"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="Close"
                Animation="Fade">
            </telerik:RadWindow>
        </Windows>
        <Windows>
            <telerik:RadWindow ID="winEditarCompetencia"
                runat="server"
                Title="Editar competencia"
                Height="400px"
                Width="800px"
                ReloadOnShow="true"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="Close"
                Animation="Fade">
            </telerik:RadWindow>
        </Windows>
        <Windows>
            <telerik:RadWindow ID="winBonos" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
