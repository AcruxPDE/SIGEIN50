<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="CatalogoRequisiciones.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoRequisiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script id="MyScript" type="text/javascript">
        var idRequisicion = "";
        var idPuesto = "";

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


        function obtenerIdFila() {
            var grid = $find('<%=grdRequisicion.ClientID %>');
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idRequisicion = SelectDataItem.getDataKeyValue("ID_REQUISICION");
                idPuesto = SelectDataItem.getDataKeyValue("ID_PUESTO");
            }
            else {
                idRequisicion = "";
                idPuesto = "";
            }
        }

        function onCloseWindow(oWnd, args) {
            idRequisicion = "";
            idPuesto = "";
            $find("<%=grdRequisicion.ClientID%>").get_masterTableView().rebind();
        }
        function closeWindow(oWnd, args) {
            idRequisicion = "";
            idPuesto = "";
            $find("<%=grdRequisicion.ClientID%>").get_masterTableView().rebind();
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 250,
                height: document.documentElement.clientHeight - 20
            };
        }

        function GetRequisicionWindowProperties(pIdRequisicion) {
            var wnd = GetWindowProperties();
            wnd.width = 1150; //document.documentElement.clientWidth - 500;
            wnd.height = document.documentElement.clientHeight - 20; //580;
            wnd.vTitulo = "Agregar requisición";
            wnd.vRadWindowId = "winRequisicion";
            wnd.vURL = "VentanaCatalogoRequisiciones.aspx?TIPO=Agregar";
            if (pIdRequisicion != null) {
                wnd.vURL += String.format("&RequisicionId={0}", pIdRequisicion);
                wnd.vTitulo = "Editar requisición";
            }
            return wnd;
        }

        function GetCandidatoIdoneaoWindowProperties(pIdRequisicion) {
            var wnd = GetWindowProperties();
            //wnd.width = 1100;
            //wnd.height = 650;
            wnd.vTitulo = "Buscar candidato";
            wnd.vURL = "VentanaCandidatoIdoneo.aspx?RequisicionId=" + pIdRequisicion;
            wnd.vRadWindowId = "winCandidatoIdoneo";
            return wnd;
        }

        function OpenCandidatoIdoneoWindow(sender, args) {
            obtenerIdFila();

            if (idRequisicion != "") {
                OpenWindow(GetCandidatoIdoneaoWindowProperties(idRequisicion));
            }
            else {
                radalert("Selecciona una requisición.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function OpenNuevaRequisicionWindow(sender, args) {
            OpenWindow(GetRequisicionWindowProperties(null));
        }

        function OpenEditRequisicionWindow(sender, args) {
            obtenerIdFila();

            if (idRequisicion != "") {
                OpenWindow(GetRequisicionWindowProperties(idRequisicion));
            }
            else {
                radalert("Selecciona una requisición.", 400, 150, "");
            }
        }

        function ConfirmarEliminar(sender, args) {
            var MasterTable = $find("<%=grdRequisicion.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            var row = selectedRows[0];

            if (row != null) {
                CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NO_REQUISICION");
                if (selectedRows != "") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar la requisición ' + CELL_NOMBRE.innerHTML + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 150, null, "Eliminar Registro");
                    args.set_cancel(true);
                }
            } else {
                radalert("Seleccione una requisición.", 400, 150, "");
                args.set_cancel(true);
            }

        }

        function NotificarRequisicion(sender, args) {
            var MasterTable = $find("<%=grdRequisicion.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            var row = selectedRows[0];

            if (row != null) {
                CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NO_REQUISICION");
                if (selectedRows != "") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });
                }
            } else {
                radalert("Seleccione una requisición.", 400, 150, "");
                args.set_cancel(true);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpRequisiciones" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramRequisiciones" runat="server" DefaultLoadingPanelID="ralpRequisiciones">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdRequisicion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequisicion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequisicion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequisicion"  UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequisicion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Requisiciones</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdRequisicion" ShowHeader="true" runat="server" AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" Width="100%" GridLines="None"
            Height="100%" EnableHierarchyExpandAll="true"
            AllowFilteringByColumn="true"
            HeaderStyle-Font-Bold="true"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnItemDataBound="grdRequisicion_ItemDataBound"
            OnNeedDataSource="grdRequisicion_NeedDataSource" OnDetailTableDataBind="grdRequisicion_DetailTableDataBind">
            <GroupingSettings CaseSensitive="False" />
           <%-- <ExportSettings FileName="CatalogoRequisiciones" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>--%>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_REQUISICION, ID_AUTORIZA, ID_PUESTO, CL_ESTATUS_PUESTO, NB_PUESTO, FL_REQUISICION, CL_TOKEN, CL_ESTATUS_REQUISICION, CL_CAUSA" DataKeyNames="ID_REQUISICION, ID_AUTORIZA, ID_PUESTO, CL_ESTATUS_PUESTO, NB_PUESTO, FL_REQUISICION, CL_TOKEN, CL_ESTATUS_REQUISICION, CL_CAUSA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms" NoDetailRecordsText="No existen candidatos asociados a esta requisición.">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <DetailTables>
                    <telerik:GridTableView DataKeyNames="ID_REQUISICION" ClientDataKeyNames="ID_REQUISICION" AllowFilteringByColumn="false" AutoGenerateColumns="false" AllowPaging="false" NoMasterRecordsText="No existen candidatos asociados a esta requisición." NoDetailRecordsText="No existen candidatos asociados a esta requisición." >
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="100" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200"  HeaderText="Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" HeaderText="Estatus solicitud" DataField="CL_SOLICITUD_ESTATUS" UniqueName="CL_SOLICITUD_ESTATUS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" HeaderText="Estatus requisición" DataField="CL_ESTADO" UniqueName="CL_ESTADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" HeaderText="F. Inicio proceso" DataField="FE_INICIO_PROCESO" UniqueName="FE_INICIO_PROCESO" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" HeaderText="F. Fin proceso" DataField="FE_TERMINO_PROCESO" UniqueName="FE_TERMINO_PROCESO" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de requisición" DataField="NO_REQUISICION" UniqueName="NO_REQUISICION" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Causa" DataField="NB_CAUSA" UniqueName="NB_CAUSA" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="300" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave puesto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Status" DataField="CL_ESTATUS_REQUISICION" UniqueName="CL_ESTATUS_REQUISICION" HeaderStyle-Width="130" FilterControlWidth="60"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de Autorización" DataField="FE_AUTORIZA_REQUISICION" UniqueName="FE_AUTORIZA_REQUISICION" HeaderStyle-Width="150" FilterControlWidth="90" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Empleado a suplir" DataField="NB_EMPLEADO_SUPLENTE" UniqueName="NB_EMPLEADO_SUPLENTE" HeaderStyle-Width="250" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION_REQUISICION" UniqueName="FE_MODIFICACION_REQUISICION" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="OpenNuevaRequisicionWindow" AutoPostBack="false" runat="server" Text="Agregar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="OpenEditRequisicionWindow" AutoPostBack="false" runat="server" Text="Editar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton ID="btnBuscarCandidato" runat="server" Text="Buscar candidato" AutoPostBack="false" OnClientClicking="OpenCandidatoIdoneoWindow"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="winNotificarRequisicion" runat="server"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winRequisicion" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Modal="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winAutorizarRequisicion" runat="server" Title="Notificación de autorización de requisición"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winCandidatoIdoneo" runat="server" Title="Candidato Idóneo" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winAgregarCandidato" runat="server" Title="Agregar candidato"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionEmpleados" runat="server" Title="Seleccionar empleado"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSolicitud" runat="server" Title="Solicitud" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionCandidato" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <%-- Ventanas de proceso de selección --%>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
             <telerik:RadWindow ID="winAnalisisCompetenicas" runat="server" Title="Analisis de competencias"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winProcesoSeleccion" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEntrevista" runat="server" Behaviors="Close, Reload" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winPerfil" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winDescriptivo" runat="server" Title="Agregar/Editar Puestos"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winVistaPrevia" runat="server" Title="Vista previa"  ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionPuestos" runat="server" Title="Seleccionar Jefe inmediato" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
