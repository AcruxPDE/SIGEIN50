<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="InventarioDePersonal.aspx.cs" Inherits="SIGE.WebApp.Administracion.InventarioDePersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">

            var vIdEmpleado = "";
            var vNbEmpleado = "";
            var vClEstatus = "";
            var vClDisponibiliada = "";

            function obtenerFila() {
                var selectedItem = $find("<%=grdEmpleados.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    vIdEmpleado = selectedItem.getDataKeyValue("ID_EMPLEADO");
                    vNbEmpleado = selectedItem.getDataKeyValue("NB_EMPLEADO_COMPLETO");
                    vClEstatus = selectedItem.getDataKeyValue("CL_ESTADO_EMPLEADO");
                    vClDisponibiliada = selectedItem.getDataKeyValue("NB_NOMINA_DO");
                }
                else {
                    vIdEmpleado = "";
                    vNbEmpleado = "";
                    vClEstatus = "";
                    vClDisponibiliada = "";
                }
            }


            function obtenerFilaNominaDo() {
                var selectedItem = $find("<%=grdEmpleados.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    vIdEmpleado = selectedItem.getDataKeyValue("ID_EMPLEADO");
                    vNbEmpleado = selectedItem.getDataKeyValue("NB_EMPLEADO_COMPLETO");
                    vClEstatus = selectedItem.getDataKeyValue("CL_ESTADO_EMPLEADO");
                    vClDisponibiliada = selectedItem.getDataKeyValue("NB_NOMINA_DO");
                }
                else {
                    vIdEmpleado = "";
                    vNbEmpleado = "";
                    vClEstatus = "";
                    vClDisponibiliada = "";
                }
            }

            function GetWindowProperties() {
                return {
                    width: document.documentElement.clientWidth - 20,//750,
                    height: document.documentElement.clientHeight - 20//15
                };
            }

            function GetWindowBajaProperties() {
                return {
                    width: document.documentElement.clientWidth - 700,
                    height: document.documentElement.clientHeight - 15
                };
            }

            function GetWindowPropertiesNomina() {
                return {
                    width: document.documentElement.clientWidth - 580,//750,
                    height: document.documentElement.clientHeight - 150//15
                };
            }

            function GetWindowPropertiesNominaConsulta() {
                return {
                    width: document.documentElement.clientWidth - 20, //750,
                    height: document.documentElement.clientHeight - 20 //15
                };
            }

            function ShowInsertInventario() {
                OpenNewWindow(GetInventarioWindowProperties());
            }

            function GetInventarioWindowProperties() {
                var wnd = GetWindowPropertiesNomina();
                wnd.vTitulo = "Inventario de personal";
                wnd.vURL = "VentanaInventarioPersonalNomina.aspx";
                wnd.vRadWindowId = "winEmpleadoInventario";
                return wnd;
            }

            function ShowInsertForm() {
                //obtenerFila();
                vIdEmpleado = "";
                OpenNewWindow(GetEmpleadoWindowProperties());
            }

            function ShowEditForm() {
                obtenerFila();

                if (vIdEmpleado != "")
                    OpenNewWindow(GetEmpleadoWindowProperties());
                else
                    radalert("Selecciona un empleado.", 400, 150, "Aviso");
            }

            function ShowEditInventario() {
                obtenerFilaNominaDo();

                if (vIdEmpleado != "")
                    OpenNewWindow(GetInventarioEditWindowProperties());
                else
                    radalert("Selecciona un empleado.", 400, 150, "Aviso");
            }

            function ShowConsultarEmpleado() {
                obtenerFilaNominaDo();
                if (vIdEmpleado != "")
                    OpenNewWindow(GetInventarioConsultaWindowProperties());
                else
                    radalert("Selecciona un empleado.", 400, 150, "Aviso");
            }

            function GetInventarioEditWindowProperties() {
                var wnd = GetWindowPropertiesNomina();
                wnd.vTitulo = "Editar empleado";
                wnd.vURL = "VentanaInventarioPersonalNomina.aspx?pIdEmpleado=" + vIdEmpleado;
                wnd.vRadWindowId = "winEmpleadoInventario";

                return wnd;
            }

            function GetInventarioConsultaWindowProperties() {
                var wnd = GetWindowPropertiesNominaConsulta();
                var vConsulta = 'CONSULTA';
                wnd.vTitulo = "Consulta empleado";
                wnd.vURL = "Empleado.aspx?EmpleadoId=" + vIdEmpleado + "&" + "Ventana=" + vConsulta;
                wnd.vRadWindowId = "winEmpleadoInventario";

                return wnd;
            }

            //function OpenEmpleadoWindow() {
            //    var vURL = "Empleado.aspx";
            //    var vTitulo = "Agregar Empleado";
            //    if (vIdEmpleado != "") {
            //        vURL = vURL + "?EmpleadoId=" + vIdEmpleado;
            //        vTitulo = "Editar Empleado";
            //    }
            //    var windowProperties = {};
            //    windowProperties.width = document.documentElement.clientWidth - 20;
            //    windowProperties.height = document.documentElement.clientHeight - 20;
            //    openChildDialog(vURL, "winEmpleado", vTitulo, windowProperties);
            //}

            function onCloseWindow(oWnd, args) {
                $find("<%=grdEmpleados.ClientID%>").get_masterTableView().rebind();
            }

            function ConfirmarEliminar(sender, args) {
                obtenerFila();
                if (vIdEmpleado != "") {

                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar el empleado ' + vNbEmpleado + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Aviso");
                    args.set_cancel(true);

                } else {
                    radalert("Seleccione un empleado.", 400, 150, "Aviso");
                    args.set_cancel(true);
                }
            }

            function ConfirmarEliminarInventario(sender, args) {
                obtenerFila();
                if (vIdEmpleado != "") {

                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar el empleado ' + vNbEmpleado + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                    args.set_cancel(true);

                } else {
                    radalert("Seleccione un empleado.", 400, 150, "Aviso");
                    args.set_cancel(true);
                }
            }


            function ConfirmarCancelar(sender, args) {
                var MasterTable = $find("<%=grdEmpleados.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_EMPLEADO_COMPLETO");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                            if (shouldSubmit) {
                                this.click();
                            }
                        });
                        radconfirm('¿Deseas cancelar la baja de "' + CELL_NOMBRE.innerHTML + '"?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Cancelar baja");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Selecciona un empleado", 400, 150, "Aviso");
                    args.set_cancel(true);
                }
            }

            function GetDarBajaWindowProperties() {
                var wnd = GetWindowBajaProperties();

                //wnd.width = 750;
                //7wnd.height = 15;
                wnd.vURL = "../EO/VentanaDarBajaEmpleado.aspx?ID=" + vIdEmpleado + "&m=General";
                wnd.vTitulo = "Dar de baja a un empleado";
                wnd.vRadWindowId = "WinDarBaja";
                return wnd;
            }

            function GetEmpleadoWindowProperties() {
                var wnd = GetWindowProperties();
                wnd.vTitulo = "Agregar empleado";
                wnd.vURL = "Empleado.aspx";
                wnd.vRadWindowId = "winEmpleado";

                if (vIdEmpleado != "") {
                    wnd.vURL = wnd.vURL + "?EmpleadoNoDoID=" + vIdEmpleado;
                    wnd.vTitulo = "Editar empleado";
                }

                return wnd;
            }

            function OpenWindowDarBaja() {
                obtenerFila();
                if (vIdEmpleado != "" & vClEstatus != "BAJA") {
                    OpenNewWindow(GetDarBajaWindowProperties());
                }
                else if (vIdEmpleado == "") {
                    radalert("Selecciona un empleado.", 400, 150, "Aviso");
                }
                else if (vClEstatus == "BAJA") {
                    radalert("El empleado ya está dado de baja.", 400, 150, "Aviso");
                }
            }

            function GetReingresoWindowProperties() {
                var wnd = GetWindowBajaProperties();

                wnd.width = 650;
                wnd.height = 380;
                wnd.vURL = "VentanaReingresoEmpleado.aspx?EmpleadoID=" + vIdEmpleado + "&m=General";
                wnd.vTitulo = "Reingreso de empleado";
                wnd.vRadWindowId = "winReingreso";
                return wnd;
            }

            function OpenWindowReingreso() {

                obtenerFila();

                if (vIdEmpleado != "" & vClEstatus != "ALTA") {
                    OpenNewWindow(GetReingresoWindowProperties());
                }
                else if (vIdEmpleado == "") {
                    radalert("Selecciona un empleado.", 400, 150, "Aviso");
                }
                else if (vClEstatus == "ALTA") {
                    radalert("El empleado no está dado de baja", 400, 150, "Aviso");
                }
            }

            function OpenNewWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
            }

            function ValidaFunciones() {
                obtenerFila();
                var selectedItem = $find("<%=grdEmpleados.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    var vFgActivoDo = selectedItem.getDataKeyValue("FG_ACTIVO_DO");
                    var btnDarBaja = $find("<%= btnDarDeBaja.ClientID %>");
                    var btnDarAlta = $find("<%= btnDarAlta.ClientID %>");
                    //var btnReingreso = $find("<= btnReingresoEmpleado.ClientID %>");

                    if (vFgActivoDo == "False") {
                        btnDarBaja.set_enabled(false);
                        btnDarAlta.set_enabled(false);
                        //btnCancelarBaja.set_enabled(false);
                        //btnReingreso.set_enabled(false);
                    }
                    else {

                        if ('<%= vDarBaja %>' == "True" && vClEstatus == "ALTA" && vClDisponibiliada != "NOM")
                            btnDarBaja.set_enabled(true);
                        else
                            btnDarBaja.set_enabled(false);
                        if ('<%= vDarAlta %>' == "True" && vClEstatus == "BAJA" && vClDisponibiliada != "NOM")
                            btnDarAlta.set_enabled(true);
                        else
                            btnDarAlta.set_enabled(false);
                        //if ('<= vReingreso %>' == "True")
                        //btnReingreso.set_enabled(true);
                    }
                }
            }

            function OpenWindowDarAlta() {
                var MasterTable = $find("<%=grdEmpleados.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_EMPLEADO_COMPLETO");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                            if (shouldSubmit) {
                                this.click();
                            }
                        });
                        radconfirm('¿Deseas cancelar la baja de "' + CELL_NOMBRE.innerHTML + '"?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Cancelar baja");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Selecciona un empleado", 400, 150, "Aviso");
                    args.set_cancel(true);
                }
            }

            function CancelarBaja() {
                var ajaxManager = $find('<%= ramInventario.ClientID %>');
                ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "CANCELARBAJA" }));
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpInventario" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramInventario" runat="server" DefaultLoadingPanelID="ralpInventario" OnAjaxRequest="ramInventario_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramInventario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEliminarNomina">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnDarAlta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnImportarEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnImportarEmpleados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnModificacionLayout">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnModificacionLayout" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnConsultar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnConsultar" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <label class="labelTitulo">Inventario de personal</label>
    
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splEmpleados" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridEmpleados" runat="server">
                <telerik:RadGrid ID="grdEmpleados" runat="server" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true"
                    AllowSorting="true" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdEmpleados_NeedDataSource" OnItemCommand="grdEmpleados_ItemCommand" OnItemDataBound="grdEmpleados_ItemDataBound">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings>
                        <ClientEvents OnRowSelected="ValidaFunciones" />
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <ExportSettings ExportOnlyData="true" FileName="InventarioDePersonal" Excel-Format="Xlsx" IgnorePaging="true">
                    </ExportSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <MasterTableView ClientDataKeyNames="ID_EMPLEADO_NOMINA_DO, ID_EMPLEADO, CL_EMPLEADO, NB_EMPLEADO_COMPLETO, CL_ESTADO_EMPLEADO, FG_DO, NB_NOMINA_DO"
                        EnableColumnsViewState="false" DataKeyNames="ID_EMPLEADO, CL_EMPLEADO, CL_ESTADO_EMPLEADO" AllowPaging="true" AllowFilteringByColumn="true"
                        ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="170" FilterControlWidth="80" HeaderText="Área / departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="180" FilterControlWidth="110" HeaderText="Nombre de la empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="20" HeaderText="Disponible" DataField="NB_NOMINA_DO" UniqueName="NB_NOMINA_DO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estatus" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="60" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_APP_MODIFICA" UniqueName="CL_USUARIO_APP_MODIFICA"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICA" UniqueName="FE_MODIFICA" HeaderStyle-Width="150" FilterControlWidth="100" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="RSPAdvSearch" runat="server" Title="Búsqueda avanzada" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 20px;">
                            <telerik:RadFilter runat="server" ID="ftGrdEmpleados" FilterContainerID="grdEmpleados" ShowApplyButton="true" Height="100">
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
        <telerik:RadButton ID="btnAgregarInventario" Visible="true" runat="server" Text="Agregar" OnClientClicked="ShowInsertInventario" AutoPostBack="false"></telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditarInventario" Visible="true" runat="server" Text="Editar" OnClientClicked="ShowEditInventario" AutoPostBack="false"></telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" Visible="true" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton ID="btnConsultar" Visible="true" runat="server" Text="Consultar" OnClientClicked="ShowConsultarEmpleado"></telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton ID="btnDarDeBaja" Visible="true" runat="server" Text="Dar de baja" AutoPostBack="false" OnClientClicked="OpenWindowDarBaja"></telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton ID="btnDarAlta" Visible="true" runat="server" Text="Dar de alta" AutoPostBack="true" OnClick="btnDarAlta_Click"></telerik:RadButton>
    </div>

    <div class=" divControlDerecha"  style="margin-right: 20px">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnImportarEmpleados" Visible="true" runat="server" Text="Importar empleados" AutoPostBack="true" ></telerik:RadButton>
        </div>

        <div class="ctrlBasico" >
            <telerik:RadButton ID="btnModificacionLayout" Visible="true" runat="server" Text="Modificación por layout" AutoPostBack="true" ></telerik:RadButton>
        </div>
    </div>
       
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup" Animation="Fade">
        <Windows>
            <telerik:RadWindow ID="rwComentarios" runat="server" Title="Comentarios de entrevistas" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEntrevista" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winReferencia" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="rwProcesoSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" ReloadOnShow="true" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="rwListaProcesoSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" ReloadOnShow="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winSolicitud" runat="server" Title="Solicitud" Behaviors="None" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winEmpleado" runat="server" Title="Empleado" Behaviors="None" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEmpleadoGeneral" runat="server" Title="Empleado" Behaviors="None" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="WinDarBaja" runat="server" Width="900px" Height="400px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="WinSeleccionCausa" runat="server" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade"></telerik:RadWindow>
            <telerik:RadWindow ID="rwConsultas" runat="server" Title="Consultas Personales" Height="600px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winPrograma" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="WinCuestionario" runat="server" Width="1050px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winReporteCumplimientoPersonal" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winReingreso" runat="server" Width="900px" Height="400px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winEmpleadoInventario" runat="server" Title="Empleado" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="rnTemplate" runat="server">
        <AlertTemplate>
            <div style="height: 10px;"></div>
            <div class="rwDialogButtons" style="text-align: center;">
                <img src="../Assets/images/Exito.png" />
                <label>¿Qué tipo de alta es?:</label>
                <div style="height: 20px;"></div>
                <input type="button" value="Cancelar baja" style="width: 120px;" class="rwOkBtn" onclick="CancelarBaja(); $find('{0}').close(true);" />
                <input type="button" value="Reingreso" style="width: 80px" class="rwCancelBtn" onclick="OpenWindowReingreso(); $find('{0}').close(true);" />
                <input type="button" value="Cancelar" style="width: 80px" class="rwCancelBtn" onclick="$find('{0}').close(true);" />
            </div>
        </AlertTemplate>
    </telerik:RadWindowManager>
</asp:Content>
