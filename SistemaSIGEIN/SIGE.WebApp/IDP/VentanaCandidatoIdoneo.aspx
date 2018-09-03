<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCandidatoIdoneo.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaCandidatoIdoneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script id="modal" type="text/javascript">

        var idCandidato;
        var idBateria;
        var clToken;
        var idRequisicion;
        var idProcesoSeleccion;
        var clEstadoProceso;
        var idEmpleado;

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }


        function obtenerIdFila() {

            var grid = $find('<%=grdCandidato.ClientID %>');
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idCandidato = SelectDataItem.getDataKeyValue("ID_CANDIDATO");
                idBateria = SelectDataItem.getDataKeyValue("ID_BATERIA");
                clToken = SelectDataItem.getDataKeyValue("CL_TOKEN");
            }
            else {
                idCandidato = "";
                idBateria = "";
                clToken = "";
            }
        }

        function closeWindow() {
            GetRadWindow().close();
        }



        function ShowPopupAnalisisCompetencias() {

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            var vIdRequisicion = '<%# vIdRequisicion %>';
            var vIdBusqueda = '<%# vIdBusquedaCandidato %>';

            obtenerIdFila();

            if (idCandidato == "") {
                radalert("Selecciona un candidato de la lista.", 400, 150);
                return;
            }

            var vUrl = "VentanaAnalisisCompetencias.aspx?IdCandidato=" + idCandidato + "&IdRequisicion=" + vIdRequisicion + "&IdBusqueda=" + vIdBusqueda;

            if (idBateria != null) {
                vUrl = vUrl + "&IdBateria=" + idBateria;
            }

            if (clToken != "" && clToken != null) {
                vUrl = vUrl + "&clToken=" + clToken;
            }

            openChildDialog(vUrl, "winAnalisisCompetenicas", "Análisis de compatibilidad", windowProperties);
        }


        //Eliminar el tooltip del control
        function pageLoad() {
            var datePicker = $find("<%=dtpInicial.ClientID %>");
            datePicker.get_popupButton().title = "";
            var datePicker2 = $find("<%=dtpFinal.ClientID %>");
            datePicker2.get_popupButton().title = "";
        }

 //       function obtenerIdFilaProceso() {
 //           var grid = $find("<=rgProcesos.ClientID %>");
 //    var MasterTable = grid.get_masterTableView();
 //    var selectedRows = MasterTable.get_selectedItems();

 //    if (selectedRows.length != 0) {
 //        var row = selectedRows[0];
 //        var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
 //        idRequisicion = SelectDataItem.getDataKeyValue("ID_REQUISICION");
 //        idProcesoSeleccion = SelectDataItem.getDataKeyValue("ID_PROCESO_SELECCION");
 //        idCandidato = SelectDataItem.getDataKeyValue("ID_CANDIDATO");
 //        idBateria = SelectDataItem.getDataKeyValue("ID_BATERIA");
 //        clToken = SelectDataItem.getDataKeyValue("CL_TOKEN");
 //    }
 //    else {
 //        idProcesoSeleccion = "";
 //        idRequisicion = "";
 //        idCandidato = "";
 //        idBateria = "";
 //        clToken = "";
 //    }
 //}

 function obtenerIdProceso() {
     var grid = $find("<%=grdCandidato.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idRequisicion = SelectDataItem.getDataKeyValue("ID_REQUISICION");
                idProcesoSeleccion = SelectDataItem.getDataKeyValue("ID_PROCESO_SELECCION");
                idCandidato = SelectDataItem.getDataKeyValue("ID_CANDIDATO");
                idBateria = SelectDataItem.getDataKeyValue("ID_BATERIA");
                clToken = SelectDataItem.getDataKeyValue("CL_TOKEN");
            }
            else {
                idProcesoSeleccion = "";
                idRequisicion = "";
                idCandidato = "";
                idBateria = "";
                clToken = "";
            }
        }


        function ShowContinuarProcesoSeleccion() {

            obtenerIdFilaProceso();

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            var vURL = "ProcesoSeleccion.aspx";
            var vTitulo = "Proceso de evaluación";


            vURL = vURL + "?IdProcesoSeleccion=" + idProcesoSeleccion + "&IdCandidato=" + idCandidato;

            if (idBateria != 0 && idBateria != null) {
                vURL = vURL + "&IdBateria=" + idBateria;
            }

            if (clToken != "" && clToken != null) {
                vURL = vURL + "&ClToken=" + clToken;
            }

            if (idRequisicion != null) {
                vURL = vURL + "&IdRequisicion=" + idRequisicion;
            }

            if (idProcesoSeleccion == "") {
                radalert("Selecciona un proceso de evaluación", 400, 150, "");
            }
            else {
                openChildDialog(vURL, "winProcesoSeleccion", vTitulo, windowProperties);
            }
        }


        function ShowVerProcesoSeleccion() {

            obtenerIdFilaProceso();

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            var vURL = "ProcesoSeleccion.aspx";
            var vTitulo = "Proceso de evaluación";


            vURL = vURL + "?IdProcesoSeleccion=" + idProcesoSeleccion + "&IdCandidato=" + idCandidato + "&Tipo=REV";

            if (idBateria != 0 && idBateria != null) {
                vURL = vURL + "&IdBateria=" + idBateria;
            }

            if (clToken != "" && clToken != null) {
                vURL = vURL + "&ClToken=" + clToken;
            }

            if (idRequisicion != null) {
                vURL = vURL + "&IdRequisicion=" + idRequisicion;
            }

            if (idProcesoSeleccion == "") {
                radalert("Selecciona un proceso de evaluación", 400, 150, "");
            }
            else {
                openChildDialog(vURL, "winProcesoSeleccion", vTitulo, windowProperties);
            }
        }


        function ShowContinuarProceso() {
            obtenerIdProceso();
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            var vURL = "ProcesoSeleccion.aspx";
            var vTitulo = "Proceso de evaluación";


            vURL = vURL + "?IdProcesoSeleccion=" + idProcesoSeleccion + "&IdCandidato=" + idCandidato;

            if (idBateria != 0 && idBateria != null) {
                vURL = vURL + "&IdBateria=" + idBateria;
            }

            if (clToken != "" && clToken != null) {
                vURL = vURL + "&ClToken=" + clToken;
            }

            if (idRequisicion != null) {
                vURL = vURL + "&IdRequisicion=" + idRequisicion;
            }

            if (idProcesoSeleccion == "") {
                radalert("Selecciona un proceso de evaluación", 400, 150, "");
            }
            else {
                openChildDialog(vURL, "winProcesoSeleccion", vTitulo, windowProperties);
            }
        }

        function OpenVentanaEvaluacionCandidatos() {
            var grid = $find("<%=grdCandidato.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idEmpleado = SelectDataItem.getDataKeyValue("ID_EMPLEADO");
                idCandidato = SelectDataItem.getDataKeyValue("ID_CANDIDATO");
                idRequisicion = '<%= vIdRequisicion %>';

                var vUrl = "AplicacionPruebas.aspx?m=INTEGRACION&pIdRequisicion=" + idRequisicion;
                if (idEmpleado != "" && idEmpleado != null && idEmpleado != "undefined")
                    vUrl = vUrl + "&pIdEmpleado=" + idEmpleado;
                if (idCandidato != "" && idCandidato != null && idCandidato != "undefined")
                    vUrl = vUrl + "&pIdCandidato=" + idCandidato;

                window.parent.location = vUrl;
            }
            else
                radalert("Selecciona un candidato de la lista.", 400, 150);
        }


        function ShowProcesoTipo() {
            var grid = $find("<%=grdCandidato.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            var vIdRequisicion = '<%# vIdRequisicion %>';

            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idRequisicion = SelectDataItem.getDataKeyValue("ID_REQUISICION");
                idProcesoSeleccion = SelectDataItem.getDataKeyValue("ID_PROCESO_SELECCION");
                clEstadoProceso = SelectDataItem.getDataKeyValue("CL_ESTADO_PROCESO");
                clEstadoProceso = SelectDataItem.getDataKeyValue("CL_ESTADO_PROCESO");
                idEmpleado = SelectDataItem.getDataKeyValue("ID_EMPLEADO");
                idCandidato = SelectDataItem.getDataKeyValue("ID_CANDIDATO");
            }
            else {
                idRequisicion = 0;
                idProcesoSeleccion = 0;
            }

            OpenVentanaEvaluacionCandidatos();

            //if (idProcesoSeleccion != 0 && idRequisicion == vIdRequisicion && clEstadoProceso != "TERMINADO" && clEstadoProceso != "Terminado") {
            //    ShowContinuarProceso();
            //}
            //else {
            //    ShowPopupProcesoSeleccion();
            //}

        }


        function ShowPopupProcesoSeleccion() {

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            //Copiar los parametros  de analisis de competencias
            var vIdRequisicion = '<%# vIdRequisicion %>';

            obtenerIdFila();

            if (idCandidato == "") {
                radalert("Selecciona un candidato de la lista.", 400, 150);
                return;
            }

            var vUrl = "ProcesoSeleccion.aspx?IdCandidato=" + idCandidato + "&IdRequisicion=" + vIdRequisicion;

            if (idBateria != null) {
                vUrl = vUrl + "&IdBateria=" + idBateria;
            }

            if (clToken != "" && clToken != null) {
                vUrl = vUrl + "&clToken=" + clToken;
            }

            openChildDialog(vUrl, "winProcesoSeleccion", "Proceso de evaluación", windowProperties);

        }

        function OpenPreview() {
            var vURL = "../Administracion/VentanaVistaDescriptivo.aspx";
            var vTitulo = "Vista previa descriptivo";
            var vIdPuesto = '<%# vIdPuesto %>';

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            vURL = vURL + "?PuestoId=" + vIdPuesto;
            var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
        }

        function useDataFromChild(pDato) {
            //var radtabstrip = $find('<= rtsBuscarCandidato.ClientID %>');
            //var count = radtabstrip.get_tabs().get_count();
            //var currentindex = radtabstrip.get_selectedIndex();
            //if (currentindex == 1) {
            //    var vGrid = $find("<=grdCandidato.ClientID %>");
            //    var vMasterTable = vGrid.get_masterTableView();
            //    vMasterTable.rebind();

            //}


        }

        //function useDataFromChild(pEmpleados) {
        //    if (pEmpleados != null) {
        //        AgregarCandidato(EncapsularSeleccion("CANDIDATO", pEmpleados));
        //    }
        //}
        //function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
        //    return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        //}
        //function AgregarCandidato(pDato) {
        //    var ajaxManager = $find('<= ramCandidato.ClientID%>');
        //    ajaxManager.ajaxRequest(pDato);
        //}

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpCandidato" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramCandidato" runat="server" OnAjaxRequest="ramCandidato_AjaxRequest" DefaultLoadingPanelID="ralpCandidato">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rlvCandidato">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvCandidato" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramCandidato">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvCandidato" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvCandidato" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsBuscarCandidato" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rmpBuscarCandidato" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftGrdCandidatos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftGrdCandidatos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdCandidato" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkPerfil">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkParametroEdad" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkParametroGenero" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkParametroEstadoCivil" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkParametroEscolaridad" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkParametroCompEsp" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkAreasInteres" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCompetencias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPrMinimo" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCandidato">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccion" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="lbMensaje" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 10px; clear: both;"></div>
    <telerik:RadSplitter ID="rsCandidato" BorderSize="0" Width="100%" Height="99%" runat="server">
        <telerik:RadPane ID="rpIdoneo" runat="server">
            <div class="ctrlBasico">
                <table class="ctrlTableForm">
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Clave de requisición: </label>
                        </td>
                        <td class="ctrlTableDataBorderContext">
                            <span runat="server" id="txtClaveRequisicion" style="width: 300px;"></span>
                        </td>
                        <td class="ctrlTableDataContext">
                            <label>Puesto de la requisición: </label>
                        </td>
                        <td class="ctrlTableDataBorderContext">
                            <a id="txtPuestoEnlace" runat="server" href="javascript:OpenPreview();"></a>
                       </td>
                    </tr>
                </table>
            </div>
            <telerik:RadTabStrip ID="rtsBuscarCandidato" runat="server" SelectedIndex="0" MultiPageID="rmpBuscarCandidato">
                <Tabs>
                    <telerik:RadTab SelectedIndex="0" Text="Criterios de selección"></telerik:RadTab>
                    <telerik:RadTab SelectedIndex="1" Text="Candidatos compatibles"></telerik:RadTab>
                  <%--  <telerik:RadTab SelectedIndex="2" Text="Procesos de evaluación"></telerik:RadTab>--%>
                </Tabs>
            </telerik:RadTabStrip>
            <div style="height: calc(100% - 100px);">
                <telerik:RadMultiPage ID="rmpBuscarCandidato" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="rpvCriteriosBusqueda" runat="server">
                        <div style="height: 10px; clear: both;"></div>
                        <label>Buscar por:</label>
                        <div style="height: 10px; clear: both;"></div>
                        <div class="ctrlBasico" style="width: 60%; ">
                            <fieldset  title="Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud.">
                                <legend>
                                    <label title="Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud.">Fecha de creación de solicitud</label>
                                </legend>
                                <div class="ctrlBasico" title="Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud">
                                    <div class="divControlIzquierda">
                                        <label title="Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud.">Fecha inicial:</label>
                                    </div>
                                    <div class="divControlIzquierda">
                                        <telerik:RadDatePicker runat="server" ID="dtpInicial" AutoPostBack="false" ToolTip="Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud."></telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class="ctrlBasico" title="Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud.">
                                    <div class="divControlIzquierda">
                                        <label title="Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud.">Fecha final:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadDatePicker runat="server" ID="dtpFinal" AutoPostBack="false" ToolTip="Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud."></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="ctrlBasico" style="width: 40%; ">
                            <fieldset  title="Los filtros de búsqueda de origen de candidatos permiten indicar si la búsqueda de los candidatos será desde candidatos externos (desde solicitudes de empleo), candidatos internos (inventario de personal) o ambos. ">
                                <legend>
                                    <label title="Los filtros de búsqueda de origen de candidatos permiten indicar si la búsqueda de los candidatos será desde candidatos externos (desde solicitudes de empleo), candidatos internos (inventario de personal) o ambos. ">Origen candidatos:</label>
                                </legend>
                                <div class="ctrlBasico" title="Los filtros de búsqueda de origen de candidatos permiten indicar si la búsqueda de los candidatos será desde candidatos externos (desde solicitudes de empleo), candidatos internos (inventario de personal) o ambos. ">
                                    <div class="ctrlBasico" style="padding-left:25px;">
                                        <telerik:RadButton ID="btnCandidatos" ToolTip="Desde solicitudes de empleo" runat="server" ToggleType="Radio" ButtonType="ToggleButton" GroupName="btnOrigen" AutoPostBack="false" BorderWidth="0" BackColor="Transparent" RenderMode="Lightweight"></telerik:RadButton>
                                        Externos
                                    </div>
                                    <div class="ctrlBasico" style="padding-left:25px;">
                                        <telerik:RadButton ID="btnEmpleados" ToolTip="Desde inventario de personal" runat="server" ToggleType="Radio" ButtonType="ToggleButton" GroupName="btnOrigen" AutoPostBack="false" BorderWidth="0" BackColor="Transparent" RenderMode="Lightweight"></telerik:RadButton>
                                        Internos
                                    </div>
                                    <div class="ctrlBasico" style="padding-left:25px; ">
                                        <telerik:RadButton ID="btnAmbos" ToolTip="Desde solicitudes de empleo e inventario de personal" runat="server" ToggleType="Radio" ButtonType="ToggleButton" GroupName="btnOrigen" AutoPostBack="false" BorderWidth="0" BackColor="Transparent" RenderMode="Lightweight"></telerik:RadButton>
                                        Ambos
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div style="height: 10px; clear: both;"></div>
                        <div class="ctrlBasico" style="width: 50%;">
                            <fieldset style="height: 260px;">
                                <legend>
                                    <telerik:RadButton Style="margin-left: 10px;" ID="chkPerfil" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" OnCheckedChanged="chkPerfil_CheckedChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                    <label>Perfil</label>
                                </legend>
                                <div style="clear: both; width: 10px;"></div>
                                <div class="ctrlBasico" style="margin-left: 20px;">
                                    <telerik:RadCheckBox runat="server" ID="chkParametroEdad" Text="Edad" AutoPostBack="false"></telerik:RadCheckBox>
                                </div>
                               <div style="clear: both; width: 10px;"></div>
                                <div class="ctrlBasico" style="margin-left: 20px;">
                                    <telerik:RadCheckBox runat="server" ID="chkParametroGenero" Text="Genero" AutoPostBack="false"></telerik:RadCheckBox>
                                </div>
                                <div style="clear: both; width: 10px;"></div>
                                <div class="ctrlBasico" style="margin-left: 20px;">
                                    <telerik:RadCheckBox runat="server" ID="chkParametroEstadoCivil" Text="Estado civil" AutoPostBack="false"></telerik:RadCheckBox>
                                </div>
                                <div style="clear: both; width: 10px;"></div>
                                <div class="ctrlBasico" style="margin-left: 20px;">
                                    <telerik:RadCheckBox runat="server" ID="chkParametroEscolaridad" Text="Escolaridad" AutoPostBack="false"></telerik:RadCheckBox>
                                </div>
                                <div style="clear: both; width: 10px;"></div>
                                <div class="ctrlBasico" style="margin-left: 20px;">
                                    <telerik:RadCheckBox runat="server" ID="chkParametroCompEsp" Text="Competencias especificas" AutoPostBack="false"></telerik:RadCheckBox>
                                </div>
                                <div style="clear: both; width: 10px;"></div>
                                <div class="ctrlBasico" style="margin-left: 20px;">
                                    <telerik:RadCheckBox runat="server" ID="chkAreasInteres" Text="Areas de interes/Experiencia" AutoPostBack="false"></telerik:RadCheckBox>
                                </div>
                            </fieldset>
                        </div>
                        <div class="ctrlBasico" style="width: 50%;">
                            <fieldset style="height: 260px;">
                                <legend>
                                    <telerik:RadButton ID="chkCompetencias" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox" AutoPostBack="true" OnCheckedChanged="chkCompetencias_CheckedChanged" BorderWidth="0" GroupName="RbtnFiltro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                    <label>Competencias genéricas</label>
                                </legend>
                                <label style="margin-left: 10px;">Porcentaje mínimo de compatibilidad: </label>
                                <telerik:RadNumericTextBox runat="server" ID="txtPrMinimo" Width="75px" NumberFormat-DecimalDigits="0" AutoPostBack="true" MaxValue="100" MinValue="1" Value="75">
                                    <EnabledStyle HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
                            </fieldset>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="divControlDerecha">
                            <telerik:RadButton Style="margin-right: 10px;" ID="btnBuscar" runat="server" Text="Buscar" AutoPostBack="true" OnClick="btnBuscar_Click"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvCandidatoSeleccionado" runat="server">
                        <div style="height: 10px; clear: both;"></div>
                        <div style="height: calc(100% - 20px);">
                           <telerik:RadGrid ID="grdCandidato" ShowHeader="true" runat="server" HeaderStyle-Font-Bold="true" AllowPaging="false" AllowSorting="true" GroupPanelPosition="Top" GridLines="None" AllowFilteringByColumn="false" EnableLinqExpressions="false"
                                ClientSettings-EnablePostBackOnRowClick="true" OnNeedDataSource="grdCandidato_NeedDataSource" OnSelectedIndexChanged="grdCandidato_SelectedIndexChanged">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowKeyboardNavigation="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView DataKeyNames="ID_CANDIDATO, ID_BATERIA, CL_TOKEN, ID_REQUISICION, ID_PROCESO_SELECCION, CL_ESTADO_PROCESO, ID_EMPLEADO " ClientDataKeyNames="ID_CANDIDATO, ID_BATERIA, CL_TOKEN, ID_REQUISICION, ID_PROCESO_SELECCION, CL_ESTADO_PROCESO, ID_EMPLEADO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20" CommandItemDisplay="None" HorizontalAlign="NotSet" EditMode="EditForms" NoMasterRecordsText="No existen candidatos compatibles con los parámetros de busqueda">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio de Solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Origen" DataField="CL_ORIGEN" UniqueName="CL_ORIGEN" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus proceso" DataField="CL_ESTADO_PROCESO" UniqueName="CL_ESTADO_PROCESO" HeaderStyle-Width="100"></telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Proceso en otra requisición" DataField="FG_OTRO_PROCESO_SELECCION" UniqueName="FG_OTRO_PROCESO_SELECCION" HeaderStyle-Width="80"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="% de Perfil" DataField="PR_COMPATIBILIDAD_PERFIL" UniqueName="PR_COMPATIBILIDAD_PERFIL" HeaderStyle-Width="70">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="% de Competencias" DataField="PR_COMPATIBILIDAD_COMPETENCIAS" UniqueName="PR_COMPATIBILIDAD_COMPETENCIAS" HeaderStyle-Width="70">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Pais" DataField="CL_PAIS" UniqueName="CL_PAIS" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Estado" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Municipio" DataField="CL_MUNICIPIO" UniqueName="CL_MUNICIPIO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Colonia" DataField="CL_COLONIA" UniqueName="CL_COLONIA" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Fecha de nacimiento" DataField="FE_NACIMIENTO" UniqueName="FE_NACIMIENTO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Lugar de nacimiento" DataField="DS_LUGAR_NACIMIENTO" UniqueName="DS_LUGAR_NACIMIENTO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Nacionalidad" DataField="CL_NACIONALIDAD" UniqueName="CL_NACIONALIDAD" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Disponibilidad para viajar" DataField="CL_DISPONIBILIDAD_VIAJE" UniqueName="CL_DISPONIBILIDAD_VIAJE" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" HeaderText="Disponibilidad de horario" DataField="DS_DISPONIBILIDAD" UniqueName="DS_DISPONIBILIDAD" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <label id="lbMensaje" runat="server" visible="false">*El candidato ya cuenta con un proceso de evaluación terminado. Puedes crear uno nuevo seleccionando "Iniciar otro proceso de evalución".</label>
                        </div>
                        <div style="height: 10px; clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnAnalisis" AutoPostBack="false" runat="server" OnClientClicked="ShowPopupAnalisisCompetencias" Text="Análisis de compatibilidad"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccion" runat="server" Text="Iniciar proceso de evaluación" OnClientClicked="ShowProcesoTipo" AutoPostBack="false"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
<%--                    <telerik:RadPageView ID="rpvProcesos" runat="server">
                        <div style="height: 10px; clear: both;"></div>
                       <div style="height: calc(100% - 20px);">
                            <telerik:RadGrid ID="rgProcesos" ShowHeader="true" runat="server" HeaderStyle-Font-Bold="true" AllowPaging="false" AllowSorting="true" GroupPanelPosition="Top" GridLines="None" AllowFilteringByColumn="false" EnableLinqExpressions="false"
                                ClientSettings-EnablePostBackOnRowClick="true" OnNeedDataSource="rgProcesos_NeedDataSource" OnSelectedIndexChanged="rgProcesos_SelectedIndexChanged">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowKeyboardNavigation="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView DataKeyNames="ID_CANDIDATO, ID_REQUISICION, ID_PROCESO_SELECCION, ID_BATERIA, CL_TOKEN, CL_ESTADO" ClientDataKeyNames="ID_CANDIDATO, ID_REQUISICION, ID_PROCESO_SELECCION, ID_BATERIA, CL_TOKEN, CL_ESTADO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20" CommandItemDisplay="None" HorizontalAlign="NotSet" EditMode="EditForms" NoMasterRecordsText="No existen procesos de evaluación asignados a esta requisición.">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio de Solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus Proceso" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de Inicio" DataFormatString="{0:d}" DataField="FE_INICIO_PROCESO" UniqueName="FE_INICIO_PROCESO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="height: 5px; clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnProceso" runat="server" Text="Continuar proceso de evaluación" OnClientClicked="ShowContinuarProcesoSeleccion" AutoPostBack="false"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnVerProceso" runat="server" Visible="false" Text="Ver proceso de evaluación" OnClientClicked="ShowVerProcesoSeleccion" AutoPostBack="false"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>--%>
                </telerik:RadMultiPage>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpnAyuda" runat="server" Scrolling="None" Width="22px">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Width="22px" ClickToOpen="true">
                <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="300px">
                    <div style="padding: 10px; text-align: justify;">
                        Esta página te permite identificar a los candidatos que cumplen con las características deseadas para el puesto solicitado en la requisición y aquellos que ya cuentan con un proceso de evaluación para dicha requisición. Selecciona los criterios de
                        busqueda y da clic en el botón buscar para que el sistema te muestre los candidato. Cuando tengas el candidato, seleccionalo y da clic en el boton "Iniciar proceso de evaluación" para comenzar
                        el proceso de evaluación para la contratación.
                        <br />
                        <br />
                        Los filtros de búsqueda por fecha de creación de solicitud permiten indicar un rango de fecha dentro del cual tienen que estar creadas las solicitudes para poder ser consideradas como compatibles. Si estos campos se dejan en blanco se consideran todos los candidatos sin importar la fecha de su solicitud.
                        <br />
                        Los filtros de búsqueda de origen de candidatos permiten indicar si la búsqueda de los candidatos será desde candidatos externos (desde solicitudes de empleo), candidatos internos (inventario de personal) o ambos.
                    </div>
                </telerik:RadSlidingPane>
                <telerik:RadSlidingPane ID="rspBusquedaAvanzada" runat="server" Title="Búsqueda avanzada" Width="300" MinWidth="500">
                    <div style="padding: 20px;">
                        <telerik:RadFilter runat="server" ID="ftGrdCandidatos" OnApplyExpressions="ftGrdCandidatos_ApplyExpressions" ShowApplyButton="true" Height="100" OnExpressionItemCreated="ftGrdCandidatos_ExpressionItemCreated">
                            <FieldEditors>
                                <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="No. Solicitud" FieldName="CL_SOLICITUD" DefaultFilterFunction="Contains" ToolTip="Numero de la solicitud" />
                                <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Candidato" FieldName="NB_CANDIDATO" DefaultFilterFunction="Contains" ToolTip="Nombre del candidato" />
                                <telerik:RadFilterDropDownEditor DataType="System.String" DisplayName="Estatus" FieldName="CL_SOLICITUD_ESTATUS" DefaultFilterFunction="EqualTo" DropDownType="RadDropDownList" DataValueField="CL_GENERICA" DataTextField="NB_GENERICO" ToolTip="Estatus actual de la solicitud del candidato" />
                                <telerik:RadFilterDropDownEditor DataType="System.String" DisplayName="Estado" FieldName="CL_ESTADO" DefaultFilterFunction="EqualTo" DropDownType="RadComboBox" DataValueField="CL_GENERICA" DataTextField="NB_GENERICO" ToolTip="Estado en el que radica el candidato" />
                                <telerik:RadFilterDropDownEditor DataType="System.String" DisplayName="Municipio" FieldName="CL_MUNICIPIO" DefaultFilterFunction="EqualTo" DropDownType="RadComboBox" DataValueField="CL_GENERICA" DataTextField="NB_GENERICO" ToolTip="Estado en el que radica el candidato" />
                                <telerik:RadFilterDateFieldEditor DataType="System.DateTime" DisplayName="Fecha de nacimiento" FieldName="FE_NACIMIENTO" DefaultFilterFunction="EqualTo" PickerType="DatePicker" ToolTip="Fecha de nacimiento del candidato" DateFormat="dd/MM/yyyy" />
                                <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Lugar de nacimiento" FieldName="DS_LUGAR_NACIMIENTO" DefaultFilterFunction="Contains" ToolTip="Lugar donde nació el candidato" />
                                <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Nacionalidad" FieldName="CL_NACIONALIDAD" DefaultFilterFunction="Contains" ToolTip="Nacionalidad del candidato" />
                                <telerik:RadFilterBooleanFieldEditor DisplayName="Disponibilidad para viajar" FieldName="CL_DISPONIBILIDAD_VIAJE" DefaultFilterFunction="EqualTo" ToolTip="Si el candidato esta dispuesto a cambiar el lugar de residencia o no" />
                                <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Horario" FieldName="DS_DISPONIBILIDAD" DefaultFilterFunction="Contains" ToolTip="Disponibilidad de horario del candidato" />
                            </FieldEditors>
                        </telerik:RadFilter>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <telerik:RadWindowManager runat="server" ID="rwMensajes"></telerik:RadWindowManager>
</asp:Content>
