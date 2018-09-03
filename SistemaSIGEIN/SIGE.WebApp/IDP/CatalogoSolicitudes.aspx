<%@ Page Title="" Language="C#"  MasterPageFile="MenuIDP.master" AutoEventWireup="true" CodeBehind="CatalogoSolicitudes.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoSolicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

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


            var idSolicitud = "";
            var idBateria = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdSolicitudes.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idSolicitud = SelectDataItem.getDataKeyValue("ID_SOLICITUD");
                    idBateria = SelectDataItem.getDataKeyValue("ID_BATERIA");
                }
            }

            function openConsultas() {
                var vURL = "ConsultasPersonales.aspx";
                var vTitulo = "Consultas personales";

                vURL = vURL + "?pIdBateria=" + pIdBateria;
                var wnd = openChildDialog(vURL, "rwConsultas", vTitulo);
            }

            function onCloseWindow(oWnd, args) {
                idSolicitud = "";
                $find("<%=grdSolicitudes.ClientID%>").get_masterTableView().rebind();
            }

            function showPopupConsultas() {
                obtenerIdFila();

                if ((idBateria != "")) {
                    openConsultas();
                }
                else {
                    radalert("No has seleccionado un registro.", 400, 150, "");
                }
            }

            function ShowPopupEditarSolicitudes() {
                obtenerIdFila();

                if ((idSolicitud != "")) {
                    this.click();
                }
                else {
                    radalert("No has seleccionado un registro.", 400, 150, "");
                }
            }

            function ShowPopupAgregarSolicitudes() {
                var oWnd = radopen("NuevaSolicitud.aspx", "RWPopupmodalCatalogoGenericoEditar");
             
            }

            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdSolicitudes.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "CL_SOLICITUD");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar la solicitud ' + CELL_NOMBRE.innerHTML + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 150, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione una solicitud.", 400, 150, "");
                    args.set_cancel(true);
                }
            }


            function AvancedSearchOpen(sender, args)
               {
                openChildDialog("../Comunes/SeleccionSolicitud.aspx", "winSeleccionSolicitudes", "Selección de solicitudes")
               }
                   
        </script>
    </telerik:RadCodeBlock>

    <label class="labelTitulo">Cátalogo Solicitudes</label>
    <div style="height: calc(100% - 140px);">

        <telerik:RadGrid
            ID="grdSolicitudes"
            ShowHeader="true"
            runat="server"
            AllowPaging="true"
            AllowSorting="true"
            GroupPanelPosition="Top"
            Width="1230px"
            GridLines="None"
            Height="100%"
            AllowFilteringByColumn="true"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnNeedDataSource="grdSolicitudes_NeedDataSource"
            OnItemDataBound="grdSolicitudes_ItemDataBound"
            OnItemCommand="OnItemCommand">
            <GroupingSettings CaseSensitive="False" />


            <ExportSettings FileName="CatalogoSolicitudes" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>

            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />

            <MasterTableView ClientDataKeyNames="ID_SOLICITUD,ID_BATERIA" DataKeyNames="ID_SOLICITUD,ID_BATERIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_CANDIDATO_COMPLETO" UniqueName="NB_CANDIDATO_COMPLETO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Status" DataField="CL_SOLICITUD_ESTATUS" UniqueName="CL_SOLICITUD_ESTATUS" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Requisición" DataField="NO_REQUISICION" UniqueName="NO_REQUISICION" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>--%>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClick="btnAgregar_Click" AutoPostBack="true" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" nClientClicking="ShowPopupEditarSolicitudes" AutoPostBack="true" runat="server" Text="Editar" Width="100" OnClick="btnEditar_Click"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminar_click" OnClientClicking="ConfirmarEliminar" AutoPostBack="true"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnProceso" runat="server" Text="Inicia proceso de selección" Width="200" OnClick="btnProceso_Click" AutoPostBack="true"></telerik:RadButton>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnBusquedaAvanzada" AutoPostBack="false" runat="server" Text="Búsqueda avanzada" Width="200"  OnClientClicked="AvancedSearchOpen"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnCartera" AutoPostBack="false" runat="server" Text="Actualización de cartera" Width="200"  OnClientClicked="ConfirmarEliminar"  ></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnConsultas" AutoPostBack="false" runat="server" Text="Consultas personales" Width="200"  OnClientClicked="showPopupConsultas"  ></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow 
                ID="RWPopupmodalCatalogoGenericoEditar"
                 runat="server" 
                Title="Catálogo de solicitudes." 
                Height="260"
                Width="595" 
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
                 ID="winSeleccionSolicitudes" 
                 runat="server" 
                 Title="Seleccionar splicitud" 
                 Height="600px" 
                 Width="1100px" 
                 ReloadOnShow="true" 
                 VisibleStatusbar="false" 
                 ShowContentDuringLoad="false" 
                 Modal="true" 
                 Behaviors="Close">
             </telerik:RadWindow>

             <telerik:RadWindow 
                 ID="rwConsultas" 
                 runat="server" 
                 Title="Consultas Personales" 
                 Height="600px"
                 Width="1100px"
                 ReloadOnShow="true"
                 VisibleStatusbar="false"
                 ShowContentDuringLoad="false"
                 Modal="true"
                 Behaviors="Close">
             </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
