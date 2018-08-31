<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="InventarioDePersonal.aspx.cs" Inherits="SIGE.WebApp.Administracion.InventarioDePersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">

            var vIdEmpleado = "";
            var vNbEmpleado = "";
            var vClEstatus = "";


            function obtenerFila() {
                var selectedItem = $find("<%=grdEmpleados.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    vIdEmpleado = selectedItem.getDataKeyValue("ID_EMPLEADO_DO");
                    vNbEmpleado = selectedItem.getDataKeyValue("M_EMPLEADO_NB_EMPLEADO_COMPLETO");
                    vClEstatus = selectedItem.getDataKeyValue("M_EMPLEADO_CL_ESTADO_EMPLEADO");
                }
                else {
                    vIdEmpleado = "";
                    vNbEmpleado = "";
                    vClEstatus = "";
                }
            }


            function obtenerFilaNominaDo() {
                var selectedItem = $find("<%=grdEmpleados.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    vIdEmpleado = selectedItem.getDataKeyValue("M_EMPLEADO_ID_EMPLEADO");
                    vNbEmpleado = selectedItem.getDataKeyValue("M_EMPLEADO_NB_EMPLEADO_COMPLETO");
                    vClEstatus = selectedItem.getDataKeyValue("M_EMPLEADO_CL_ESTADO_EMPLEADO");
                }
                else {
                    vIdEmpleado = "";
                    vNbEmpleado = "";
                    vClEstatus = "";
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
<<<<<<< HEAD
                    width: document.documentElement.clientWidth - 20,//750,
=======
                    width: document.documentElement.clientWidth - 400,//750,
>>>>>>> DEV
                    height: document.documentElement.clientHeight - 20//15
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
                    radalert("Selecciona un empleado.", 400, 150);
            }

            function ShowEditInventario() {
                obtenerFilaNominaDo();

                if (vIdEmpleado != "")
                    OpenNewWindow(GetInventarioEditWindowProperties());
                else
                    radalert("Selecciona un empleado.", 400, 150);
            }

            function GetInventarioEditWindowProperties() {
                var wnd = GetWindowPropertiesNomina();
<<<<<<< HEAD
                wnd.vTitulo = "Editar empleado";
=======
                wnd.vTitulo = "Editar Empleado";
>>>>>>> DEV
                wnd.vURL = "VentanaInventarioPersonalNomina.aspx?pIdEmpleado=" + vIdEmpleado;
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

                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar el empleado ' + vNbEmpleado + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                    args.set_cancel(true);

                } else {
                    radalert("Seleccione un empleado.", 400, 150, "");
                    args.set_cancel(true);
                }
            }

            function ConfirmarEliminarInventario(sender, args) {
                obtenerFila();
                if (vIdEmpleado != "") {

                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar el empleado ' + vNbEmpleado + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                    args.set_cancel(true);

                } else {
                    radalert("Seleccione un empleado.", 400, 150, "");
                    args.set_cancel(true);
                }
            }


            function ConfirmarCancelar(sender, args) {
                var MasterTable = $find("<%=grdEmpleados.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "M_EMPLEADO_NB_EMPLEADO_COMPLETO");
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
                    radalert("Selecciona un empleado", 400, 150, "Error");
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
<<<<<<< HEAD
                    wnd.vTitulo = "Editar empleado";
=======
                    wnd.vTitulo = "Editar Empleado";
>>>>>>> DEV
                }

                return wnd;
            }

            function OpenWindowDarBaja() {
                obtenerFila();
                if (vIdEmpleado != "" & vClEstatus != "BAJA") {
                    OpenNewWindow(GetDarBajaWindowProperties());
                }
                else if (vIdEmpleado == "") {
                    radalert("Selecciona un empleado.", 400, 150);
                }
                else if (vClEstatus == "BAJA") {
                    radalert("El empleado ya está dado de baja.", 400, 150);
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
                    radalert("Selecciona un empleado.", 400, 150);
                }
                else if (vClEstatus == "ALTA") {
                    radalert("El empleado no está dado de baja", 400, 150);
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

                        if ('<%= vDarBaja %>' == "True" && vClEstatus == "ALTA")
                            btnDarBaja.set_enabled(true);
                        else
                            btnDarBaja.set_enabled(false);
                        if ('<%= vDarAlta %>' == "True" && vClEstatus == "BAJA")
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
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "M_EMPLEADO_NB_EMPLEADO_COMPLETO");
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
                    radalert("Selecciona un empleado", 400, 150, "Error");
                    args.set_cancel(true);
                }
            }

<<<<<<< HEAD
            function CancelarBaja() {
                var ajaxManager = $find('<%= ramInventario.ClientID %>');
                ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "CANCELARBAJA" }));
            }

=======
>>>>>>> DEV
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
<<<<<<< HEAD
=======
            <telerik:AjaxSetting AjaxControlID="btnEliminarNomina">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
>>>>>>> DEV
            <telerik:AjaxSetting AjaxControlID="btnDarAlta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
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
                    <MasterTableView ClientDataKeyNames="M_EMPLEADO_ID_EMPLEADO,M_EMPLEADO_CL_EMPLEADO,M_EMPLEADO_NB_EMPLEADO_COMPLETO,M_EMPLEADO_CL_ESTADO_EMPLEADO, FG_ACTIVO_DO, ID_EMPLEADO_DO"
                        EnableColumnsViewState="false" DataKeyNames="M_EMPLEADO_ID_EMPLEADO,M_EMPLEADO_CL_EMPLEADO, M_EMPLEADO_CL_ESTADO_EMPLEADO" AllowPaging="true" AllowFilteringByColumn="true"
                        ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="No. de empleado" DataField="M_EMPLEADO_CL_EMPLEADO" UniqueName="M_EMPLEADO_CL_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre completo" DataField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre" DataField="M_EMPLEADO_NB_EMPLEADO" UniqueName="M_EMPLEADO_NB_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno" DataField="M_EMPLEADO_NB_APELLIDO_PATERNO" UniqueName="M_EMPLEADO_NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido materno" DataField="M_EMPLEADO_NB_APELLIDO_MATERNO" UniqueName="M_EMPLEADO_NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Puesto" DataField="M_PUESTO_NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO"></telerik:GridBoundColumn>
                           <%-- <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave del área/departamento" DataField="M_DEPARTAMENTO_CL_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_CL_DEPARTAMENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/departamento" DataField="M_DEPARTAMENTO_NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género" DataField="M_EMPLEADO_CL_GENERO" UniqueName="M_EMPLEADO_CL_GENERO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado civil" DataField="M_EMPLEADO_CL_ESTADO_CIVIL" UniqueName="M_EMPLEADO_CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre del cónyuge" DataField="M_EMPLEADO_NB_CONYUGUE" UniqueName="M_EMPLEADO_NB_CONYUGUE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="RFC" DataField="M_EMPLEADO_CL_RFC" UniqueName="M_EMPLEADO_CL_RFC"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="CURP" DataField="M_EMPLEADO_CL_CURP" UniqueName="M_EMPLEADO_CL_CURP"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="NSS" DataField="M_EMPLEADO_CL_NSS" UniqueName="M_EMPLEADO_CL_NSS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Tipo sanguíneo" DataField="M_EMPLEADO_CL_TIPO_SANGUINEO" UniqueName="M_EMPLEADO_CL_TIPO_SANGUINEO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nacionalidad" DataField="M_EMPLEADO_CL_NACIONALIDAD" UniqueName="M_EMPLEADO_CL_NACIONALIDAD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="País" DataField="M_EMPLEADO_NB_PAIS" UniqueName="M_EMPLEADO_NB_PAIS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Entidad federativa" DataField="M_EMPLEADO_NB_ESTADO" UniqueName="M_EMPLEADO_NB_ESTADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Municipio" DataField="M_EMPLEADO_NB_MUNICIPIO" UniqueName="M_EMPLEADO_NB_MUNICIPIO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Colonia" DataField="M_EMPLEADO_NB_COLONIA" UniqueName="M_EMPLEADO_NB_COLONIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calle" DataField="M_EMPLEADO_NB_CALLE" UniqueName="M_EMPLEADO_NB_CALLE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Exterior" DataField="M_EMPLEADO_NO_EXTERIOR" UniqueName="M_EMPLEADO_NO_EXTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Interior" DataField="M_EMPLEADO_NO_INTERIOR" UniqueName="M_EMPLEADO_NO_INTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Código postal" DataField="M_EMPLEADO_CL_CODIGO_POSTAL" UniqueName="M_EMPLEADO_CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="M_EMPLEADO_NB_EMPRESA" UniqueName="M_EMPLEADO_NB_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Correo electrónico" DataField="M_EMPLEADO_CL_CORREO_ELECTRONICO" UniqueName="M_EMPLEADO_CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Natalicio" DataField="M_EMPLEADO_FE_NACIMIENTO" UniqueName="M_EMPLEADO_FE_NACIMIENTO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Lugar de nacimiento" DataField="M_EMPLEADO_DS_LUGAR_NACIMIENTO" UniqueName="M_EMPLEADO_DS_LUGAR_NACIMIENTO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de alta" DataField="M_EMPLEADO_FE_ALTA" UniqueName="M_EMPLEADO_FE_ALTA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de baja" DataField="M_EMPLEADO_FE_BAJA" UniqueName="M_EMPLEADO_FE_BAJA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo" DataField="M_EMPLEADO_MN_SUELDO" UniqueName="M_EMPLEADO_MN_SUELDO" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo variable" DataField="M_EMPLEADO_MN_SUELDO_VARIABLE" UniqueName="M_EMPLEADO_MN_SUELDO_VARIABLE" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Composición del sueldo" DataField="M_EMPLEADO_DS_SUELDO_COMPOSICION" UniqueName="M_EMPLEADO_DS_SUELDO_COMPOSICION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave de la empresa" DataField="C_EMPRESA_CL_EMPRESA" UniqueName="C_EMPRESA_CL_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="180" FilterControlWidth="110" HeaderText="Nombre de la empresa" DataField="C_EMPRESA_NB_EMPRESA" UniqueName="C_EMPRESA_NB_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="20" HeaderText="Disponible" DataField="NB_ACTIVO_NOMINA" UniqueName="NB_ACTIVO_NOMINA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Razón social" DataField="C_EMPRESA_NB_RAZON_SOCIAL" UniqueName="C_EMPRESA_NB_RAZON_SOCIAL"></telerik:GridBoundColumn>
<<<<<<< HEAD
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estatus" DataField="M_EMPLEADO_CL_ESTADO_EMPLEADO" UniqueName="M_EMPLEADO_CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
=======
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado" DataField="M_EMPLEADO_CL_ESTADO_EMPLEADO" UniqueName="M_EMPLEADO_CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
>>>>>>> DEV
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Activo" DataField="M_EMPLEADO_FG_ACTIVO" UniqueName="M_EMPLEADO_FG_ACTIVO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="60" HeaderText="Último usuario que modifica" DataField="M_EMPLEADO_CL_USUARIO_APP_MODIFICA" UniqueName="M_EMPLEADO_CL_USUARIO_APP_MODIFICA"></telerik:GridBoundColumn>
                            <%-- <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" DataType="System.DateTime" CurrentFilterFunction="EqualTo"   HeaderStyle-Width="120" FilterControlWidth="100" HeaderText="Última fecha de modificación" DataField="M_FE_MODIFICA" UniqueName="M_FE_MODIFICA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>--%>
                            <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="M_FE_MODIFICA" UniqueName="M_FE_MODIFICA" HeaderStyle-Width="150" FilterControlWidth="100" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="RSPAdvSearch"  runat="server" Title="Búsqueda avanzada" Width="500" MinWidth="500" Height="100%">
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
         <%-- <telerik:RadButton ID="btnAgregar" Visible="true" runat="server" Text="Agregar" OnClientClicked="ShowInsertForm" AutoPostBack="false"></telerik:RadButton>--%>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditarInventario" Visible="true" runat="server" Text="Editar" OnClientClicked="ShowEditInventario" AutoPostBack="false"></telerik:RadButton>
        <%--<telerik:RadButton ID="btnEditar" Visible="true" runat="server" Text="Editar" OnClientClicked="ShowEditForm" AutoPostBack="false"></telerik:RadButton>--%>
    </div>
    <div class="ctrlBasico">
        <%-- <telerik:RadButton ID="btnEliminarInventario" Visible="false" runat="server" Text="Eliminar" OnClick="btnEliminarInventario_Click" OnClientClicking="ConfirmarEliminarInventario"></telerik:RadButton>--%>
        <telerik:RadButton ID="btnEliminar" Visible="true" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnDarDeBaja" Visible="true" runat="server" Text="Dar de baja" AutoPostBack="false" OnClientClicked="OpenWindowDarBaja"></telerik:RadButton>
    </div>
       <div class="ctrlBasico">
        <telerik:RadButton ID="btnDarAlta" Visible="true" runat="server" Text="Dar de alta" AutoPostBack="true" OnClick="btnDarAlta_Click"></telerik:RadButton>
    </div>
    <%--<div class="ctrlBasico">
        <telerik:RadButton ID="btnCancelarBaja" Visible="true" runat="server" Text="Cancelar baja" AutoPostBack="true" OnClientClicking="ConfirmarCancelar" OnClick="btnCancelarBaja_Click"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnReingresoEmpleado" Visible="true" runat="server" Text="Reingreso" AutoPostBack="false" OnClientClicking="OpenWindowReingreso"></telerik:RadButton>
    </div>--%>
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
            <telerik:RadWindow ID="winEmpleadoGeneral" runat="server" Title="Empleado" Behaviors="None" Modal="true" VisibleStatusbar="false" ></telerik:RadWindow>
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
             <div style="height:10px;"></div>
            <div class="rwDialogButtons" style="text-align:center;">
                 <img  src="../Assets/images/Exito.png" />
                <label>¿Qué tipo de alta es?:</label>
                 <div style="height:20px;"></div>
<<<<<<< HEAD
                <%--<input type="button" value="Cancelar baja" style="width: 120px;" class="rwOkBtn" onclick="__doPostBack('btnSubmit', 'cancelarBaja');" />--%>
                <input type="button" value="Cancelar baja" style="width: 120px;" class="rwOkBtn" onclick="CancelarBaja(); $find('{0}').close(true);" />
=======
                <input type="button" value="Cancelar baja" style="width: 120px;" class="rwOkBtn" onclick="__doPostBack('btnSubmit', 'cancelarBaja');" />
>>>>>>> DEV
                <input type="button" value="Reingreso" style="width: 80px" class="rwCancelBtn" onclick="OpenWindowReingreso(); $find('{0}').close(true);"  />
                <input type="button" value="Cancelar" style="width: 80px" class="rwCancelBtn"  onclick="$find('{0}').close(true);" />
            </div>
        </AlertTemplate>
    </telerik:RadWindowManager>
</asp:Content>
