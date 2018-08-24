<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="OcupacionesNacionales.aspx.cs" Inherits="SIGE.WebApp.STPS.OcupacionesNacionales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            //0000000000000000000000000000000000000000000000000000 AREAS OCUPACION 000000000000000000000000000000000000000000
            var idArea = "";
            function obtenerIdFilaArea() {
                var grid = $find("<%=grdAreas.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idArea = SelectDataItem.getDataKeyValue("ID_AREA");
                }
            }

            function onCloseWindow(oWnd, args) {
                idArea = "";
                $find("<%=grdAreas.ClientID%>").get_masterTableView().rebind();

            }

            function ShowPopupEditarAreas() {
                obtenerIdFilaArea();
                if (idArea != "") {
                    var oWnd = radopen("VentanaOcupacionArea.aspx?&ID=" + idArea, "RWPopupmodalCatalogoArea");
                    oWnd.set_title("Editar área");
                } else {
                    radalert("No has seleccionado un registro.", 350, 148, "");
                }
            }

            function ShowPopupAgregarAreas() {
                var oWnd = radopen("VentanaOcupacionArea.aspx", "RWPopupmodalCatalogoArea");
                oWnd.set_title("Agregar área");
            }

            function ConfirmarEliminarAreas(sender, args) {
                var MasterTable = $find("<%=grdAreas.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_AREA");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar el área ' + CELL_NOMBRE.innerHTML + ' y la relación con las ocupaciones?', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione un área.", 400, 150, "");
                    args.set_cancel(true);
                }
            }

            //0000000000000000000000000000000000000000000000000000 SUBAREAS OCUPACION 000000000000000000000000000000000000000000
            var idSubarea = "";
            function obtenerIdFilaSubarea() {
                var grid = $find("<%=grdSubareas.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idSubarea = SelectDataItem.getDataKeyValue("ID_SUBAREA");
                }
            }

            function onCloseWindowSubarea(oWnd, args) {
                idSubarea = "";
                $find("<%=grdSubareas.ClientID%>").get_masterTableView().rebind();
            }

            function ShowPopupEditarSubareas() {
                obtenerIdFilaSubarea();
                if (idSubarea != "") {
                    var oWnd = radopen("VentanaOcupacionSubarea.aspx?&ID=" + idSubarea, "RWPopupmodalCatalogoSubarea");
                    oWnd.set_title("Editar sub-área");
                } else {
                    radalert("No has seleccionado un registro.", 350, 148, "");
                }
            }

            function ShowPopupAgregarSubareas() {
                var oWnd = radopen("VentanaOcupacionSubarea.aspx", "RWPopupmodalCatalogoSubarea");
                oWnd.set_title("Agregar sub-área");
            }

            function ConfirmarEliminarSubareas(sender, args) {
                var MasterTable = $find("<%=grdSubareas.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_SUBAREA");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar la sub-área ' + CELL_NOMBRE.innerHTML + ' y la relación con las ocupaciones?.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione una sub-área.", 400, 150, "");
                    args.set_cancel(true);
                }
            }
            //0000000000000000000000000000000000000000000000000000 MODULOS OCUPACION 0000000000000000000000000000000000000000000
            var idModulo = "";
           
            function obtenerIdFilaModulo() {
                var grid = $find("<%=grdModulos.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idModulo = SelectDataItem.getDataKeyValue("ID_MODULO");
                }
            }

            function onCloseWindowModulo(oWnd, args) {
                idModulo = "";
                $find("<%=grdModulos.ClientID%>").get_masterTableView().rebind();
            }

            function ShowPopupEditarModulo() {
                obtenerIdFilaModulo();
                if (idModulo != "") {
                    var oWnd = radopen("VentanaOcupacionModulo.aspx?&ID=" + idModulo, "RWPopupmodalCatalogoModulo");
                    oWnd.set_title("Editar módulo");
                } else {
                    radalert("No has seleccionado un registro.", 350, 148, "");
                }
            }

            function ShowPopupAgregarModulo() {
                var oWnd = radopen("VentanaOcupacionModulo.aspx", "RWPopupmodalCatalogoModulo");
                oWnd.set_title("Agregar módulo");
            }

            function ConfirmarEliminarModulos(sender, args) {
                var MasterTable = $find("<%=grdModulos.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_MODULO");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar el módulo ' + CELL_NOMBRE.innerHTML + ' y la relación con las ocupaciones?.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione un módulo.", 400, 150, "");
                    args.set_cancel(true);
                }
            }

            //0000000000000000000000000000000000000000000000000000 OCUPACIONES 0000000000000000000000000000000000000000000000000
            var idOcupacion = "";

            function obtenerIdFilaOcupacion() {
                var grid = $find("<%=grdOcupaciones.ClientID %>");
                 var MasterTable = grid.get_masterTableView();
                 var selectedRows = MasterTable.get_selectedItems();

                 if (selectedRows.length != 0) {
                     var row = selectedRows[0];
                     var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                     idOcupacion = SelectDataItem.getDataKeyValue("ID_OCUPACION");
                 }
             }

             function onCloseWindowOcupacion(oWnd, args) {
                 idOcupacion = "";
                 $find("<%=grdOcupaciones.ClientID%>").get_masterTableView().rebind();
            }

            function ShowPopupEditarOcupacion() {
                obtenerIdFilaOcupacion();
                if (idOcupacion != "") {
                    var oWnd = radopen("VentanaOcupacionesNacionales.aspx?&ID=" + idOcupacion, "RWPopupmodalCatalogoOcupacion");
                    oWnd.set_title("Editar ocupación");
                } else {
                    radalert("No has seleccionado un registro.", 350, 148, "");
                }
            }

            function ShowPopupAgregarOcupacion() {
                var oWnd = radopen("VentanaOcupacionesNacionales.aspx", "RWPopupmodalCatalogoOcupacion");
                oWnd.set_title("Agregar ocupación");
            }

            function ConfirmarEliminarOcupaciones(sender, args) {
                var MasterTable = $find("<%=grdOcupaciones.ClientID %>").get_masterTableView();
                 var selectedRows = MasterTable.get_selectedItems();
                 var row = selectedRows[0];
                 if (row != null) {
                     CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_OCUPACION");
                     if (selectedRows != "") {
                         var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                         { if (shouldSubmit) { this.click(); } });

                         radconfirm('¿Deseas eliminar la ocupación ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                         args.set_cancel(true);
                     }
                 } else {
                     radalert("Seleccione una ocupación.", 400, 150, "");
                     args.set_cancel(true);
                 }
             }

            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                        args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                        args.get_eventTarget().indexOf("ExportToCsvButton") >= 0
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

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardarArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditarArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdAreas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnGuardarSubarea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditarSubarea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSubareas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarSubarea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSubareas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdSubareas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSubareas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnGuardarModulo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdModulos" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditarModulo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdModulos" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarModulo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdModulos" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdModulos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdModulos" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnGuardarOcupacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOcupaciones" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditarOcupacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOcupaciones" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarOcupacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOcupaciones" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdOcupaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOcupaciones" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Catálogo Nacional de Ocupaciones</label>

    <telerik:RadTabStrip runat="server" ID="rtsReportes" SelectedIndex="0" MultiPageID="rmpReportes" Width="100%">
        <Tabs>
            <telerik:RadTab runat="server" Text="Áreas"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Sub-áreas"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Módulos"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Ocupaciones"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 100px); width: 100%;">
        <telerik:RadMultiPage runat="server" ID="rmpReportes" SelectedIndex="0" Width="100%" Height="100%">

            <telerik:RadPageView ID="rpvAreas" runat="server">
                <div style="clear: both; height: 15px;"></div>
                <div style="height: calc(100% - 70px);">
                    <telerik:RadGrid ID="grdAreas" ShowHeader="true" runat="server" AllowPaging="true"
                        AllowSorting="true" GroupPanelPosition="Top" Width="730px" GridLines="None"
                        Height="100%"
                        AllowFilteringByColumn="true"
                        HeaderStyle-Font-Bold="true"
                        ClientSettings-EnablePostBackOnRowClick="false"
                        OnNeedDataSource="grdAreas_NeedDataSource"
                        OnItemDataBound="grdAreas_ItemDataBound"
                        OnItemCommand="grdAreas_ItemCommand">
                        <GroupingSettings CaseSensitive="False" />
                        <ExportSettings FileName="Cátalogo de áreas" ExportOnlyData="true" IgnorePaging="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView ClientDataKeyNames="ID_AREA,CL_AREA" DataKeyNames="ID_AREA,CL_AREA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                            CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                                RefreshText="Actualizar" AddNewRecordText="Insertar" />
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_AREA" UniqueName="CL_AREA" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre del área" DataField="NB_AREA" UniqueName="NB_AREA" HeaderStyle-Width="400" FilterControlWidth="330"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardarArea" OnClientClicked="ShowPopupAgregarAreas" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="btnEditarArea" OnClientClicked="ShowPopupEditarAreas" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="btnEliminarArea" runat="server" OnClientClicking="ConfirmarEliminarAreas" Text="Eliminar" Width="100" OnClick="btnEliminarArea_Click"></telerik:RadButton>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvSubareas" runat="server">

                <div style="clear: both; height: 15px;"></div>

                <div style="height: calc(100% - 70px);">

                    <telerik:RadGrid ID="grdSubareas" ShowHeader="true" runat="server" AllowPaging="true"
                        AllowSorting="true" GroupPanelPosition="Top" Width="930px" GridLines="None"
                        Height="100%"
                        AllowFilteringByColumn="true"
                        HeaderStyle-Font-Bold="true"
                        ClientSettings-EnablePostBackOnRowClick="false"
                        OnNeedDataSource="grdSubareas_NeedDataSource"
                        OnItemDataBound="grdSubareas_ItemDataBound"
                        OnItemCommand="grdSubareas_ItemCommand">
                        <GroupingSettings CaseSensitive="False" />
                        <ExportSettings FileName="Cátalogo de sub-áreas" ExportOnlyData="true" IgnorePaging="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView ClientDataKeyNames="ID_SUBAREA,CL_SUBAREA" DataKeyNames="ID_SUBAREA,CL_SUBAREA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                            CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                                RefreshText="Actualizar" AddNewRecordText="Insertar" />
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área" DataField="NB_AREA" UniqueName="NB_AREA" HeaderStyle-Width="350" FilterControlWidth="280"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de sub-área" DataField="CL_SUBAREA" UniqueName="CL_SUBAREA" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Sub-área" DataField="NB_SUBAREA" UniqueName="NB_SUBAREA" HeaderStyle-Width="400" FilterControlWidth="330"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarSubarea" OnClientClicked="ShowPopupAgregarSubareas" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="btnEditarSubarea" OnClientClicked="ShowPopupEditarSubareas" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="btnEliminarSubarea" runat="server" Text="Eliminar" Width="100" OnClientClicking="ConfirmarEliminarSubareas" OnClick="btnEliminarSubarea_Click"></telerik:RadButton>
                </div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvModulos" runat="server">

                <div style="clear: both; height: 15px;"></div>

                <div style="height: calc(100% - 70px);">

                    <telerik:RadGrid ID="grdModulos" ShowHeader="true" runat="server" AllowPaging="true"
                        AllowSorting="true" GroupPanelPosition="Top" Width="1100px" GridLines="None"
                        Height="100%"
                        AllowFilteringByColumn="true"
                        HeaderStyle-Font-Bold="true"
                        ClientSettings-EnablePostBackOnRowClick="false"
                        OnNeedDataSource="grdModulos_NeedDataSource"
                        OnItemDataBound="grdModulos_ItemDataBound"
                        OnItemCommand="grdModulos_ItemCommand">
                        <GroupingSettings CaseSensitive="False" />
                        <ExportSettings FileName="Cátalogo de módulos" ExportOnlyData="true" IgnorePaging="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView ClientDataKeyNames="ID_MODULO,CL_MODULO" DataKeyNames="ID_MODULO,CL_MODULO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                            CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                                RefreshText="Actualizar" AddNewRecordText="Insertar" />
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área" DataField="NB_AREA" UniqueName="NB_AREA" HeaderStyle-Width="300" FilterControlWidth="200"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Sub-área" DataField="NB_SUBAREA" UniqueName="NB_SUBAREA" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de módulo" DataField="CL_MODULO" UniqueName="CL_MODULO" HeaderStyle-Width="150" FilterControlWidth="90"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Módulo" DataField="NB_MODULO" UniqueName="NB_MODULO" HeaderStyle-Width="300" FilterControlWidth="200"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarModulo" OnClientClicked="ShowPopupAgregarModulo" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="btnEditarModulo" OnClientClicked="ShowPopupEditarModulo" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="btnEliminarModulo" runat="server" Text="Eliminar" Width="100" OnClientClicking="ConfirmarEliminarModulos" OnClick="btnEliminarModulo_Click"></telerik:RadButton>
                </div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvOcupaciones" runat="server">

                <div style="clear: both; height: 15px;"></div>

                <div style="height: calc(100% - 70px);">

                    <telerik:RadGrid ID="grdOcupaciones" ShowHeader="true" runat="server" AllowPaging="true"
                        AllowSorting="true" GroupPanelPosition="Top" Width="1300px" GridLines="None"
                        Height="100%"
                        AllowFilteringByColumn="true"
                        HeaderStyle-Font-Bold="true"
                        ClientSettings-EnablePostBackOnRowClick="false"
                        OnNeedDataSource="grdOcupaciones_NeedDataSource"
                        OnItemDataBound="grdOcupaciones_ItemDataBound"
                        OnItemCommand="grdOcupaciones_ItemCommand">
                        <GroupingSettings CaseSensitive="False" />
                        <ExportSettings FileName="Cátalogo de ocupaciones" ExportOnlyData="true" IgnorePaging="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView ClientDataKeyNames="ID_OCUPACION" DataKeyNames="ID_OCUPACION" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                            CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                                RefreshText="Actualizar" AddNewRecordText="Insertar" />
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área" DataField="NB_AREA" UniqueName="NB_AREA" HeaderStyle-Width="300" FilterControlWidth="200"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Sub-área" DataField="NB_SUBAREA" UniqueName="NB_SUBAREA" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Módulo" DataField="NB_MODULO" UniqueName="NB_MODULO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave ocupación" DataField="CL_OCUPACION" UniqueName="CL_OCUPACION" HeaderStyle-Width="150" FilterControlWidth="90"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_OCUPACION" UniqueName="NB_OCUPACION" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarOcupacion" OnClientClicked="ShowPopupAgregarOcupacion" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="btnEditarOcupacion" OnClientClicked="ShowPopupEditarOcupacion" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="btnEliminarOcupacion" runat="server" Text="Eliminar" Width="100" OnClientClicking="ConfirmarEliminarOcupaciones" OnClick="btnEliminarOcupacion_Click"></telerik:RadButton>
                </div>

            </telerik:RadPageView>

        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWPopupmodalCatalogoArea" runat="server" Title="Catálogo de Áreas" Height="270"
                Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RWPopupmodalCatalogoSubarea" runat="server" Title="Catálogo de sub-áreas" Height="330"
                Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindowSubarea">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RWPopupmodalCatalogoModulo" runat="server" Title="Catálogo de módulos" Height="390"
                Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindowModulo">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RWPopupmodalCatalogoOcupacion" runat="server" Title="Catálogo de ocupaciones" Height="450"
                Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindowOcupacion">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
