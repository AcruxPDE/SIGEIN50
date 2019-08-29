<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="DescriptivoPuestos.aspx.cs" Inherits="SIGE.WebApp.Administracion.DescriptivoPuestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            function ShowInsertForm() {
                OpenWindow(null);
                return false;
            }

            function GetWindowProperties() {
                return {
                    width: document.documentElement.clientWidth - 20,//750,
                    height: document.documentElement.clientHeight - 20//15
                };
            }

            function ShowInsertNomina() {

                var wnd = GetWindowProperties();
                wnd.vTitulo = "Agregar descriptivo de puesto";
                wnd.vURL = "VentanaDescriptivoPuesto.aspx";
                wnd.vRadWindowId = "winNuevoDescriptivo";
                
                OpenNewWindow(wnd);
            }

            function OpenNewWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
            }

            function ShowEditNomina() {

                var selectedItem = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    var IdPuesto = selectedItem.getDataKeyValue("ID_PUESTO_DO")

                    var wnd = GetWindowProperties();
                    wnd.vTitulo = "Editar descriptivo de puesto";
                    wnd.vURL = "VentanaDescriptivoPuesto.aspx?PuestoId=" + IdPuesto;
                    wnd.vRadWindowId = "winNuevoDescriptivo";

                    OpenNewWindow(wnd);

                }
                else
                    radalert("Selecciona un puesto.", 400, 150, "Aviso");

            }

            function ShowConsultar() {
                var selectedItem = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    var IdPuesto = selectedItem.getDataKeyValue("ID_PUESTO_DO")
                    var Ventana = 'CONSULTA';

                    var wnd = GetWindowProperties();
                    wnd.vTitulo = "Consultar descriptivo de puesto";
                    wnd.vURL = "VentanaDescriptivoPuesto.aspx?PuestoId=" + IdPuesto + "&" + "Ventana=" + Ventana;
                    wnd.vRadWindowId = "winNuevoDescriptivo";

                    OpenNewWindow(wnd);

                }
                else
                    radalert("Selecciona un puesto.", 400, 150, "Aviso");
            }

            function ConfirmarEliminarNomina(sender, args) {
                var MasterTable = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];

                if (row != null) {
                    if (selectedRows != "") {
                        CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_PUESTO");
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });
                        radconfirm("¿Deseas eliminar el puesto " + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 200, null, "Aviso");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione un Puesto.", 400, 150, "Aviso");
                    args.set_cancel(true);
                }
            }

            function ShowEditForm() {
                var selectedItem = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindow(selectedItem.getDataKeyValue("ID_PUESTO"), null);
                else
                    radalert("Selecciona un puesto.", 400, 150, "Aviso");
            }

            function RebindGrid() {
                $find("<%=grdDescriptivo.ClientID%>").get_masterTableView().rebind();
            }

            function ShowCopyForm() {
                var selectedItem = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindow(selectedItem.getDataKeyValue("ID_PUESTO_DO"), "C");
                else
                    radalert("Selecciona un puesto.", 400, 150, "Aviso");
            }

            function ShowPreviewForm() {
                var selectedItem = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenPreview(selectedItem.getDataKeyValue("ID_PUESTO_DO"));
                else
                    radalert("Selecciona un puesto.", 400, 150, "Aviso");
            }

            function OpenWindow(pIdPuesto, pIsCopy) {
                var vURL = "VentanaDescriptivoPuesto.aspx";
                var vTitulo = "Agregar descripción del puesto";
                if (pIdPuesto != null) {
                    vURL = vURL + "?PuestoId=" + pIdPuesto

                    vTitulo = "Editar descripción del puesto";

                    if (pIsCopy != null) {
                        vURL = vURL + "&pIsCopy=C";
                        vTitulo = "Copiar descripción del puesto";
                    }

                }
                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };
                openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
            }

            function OpenPreview(pIdPuesto) {
                var vURL = "VentanaVistaDescriptivo.aspx";
                var vTitulo = "Consulta descriptivo de puesto";

                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                vURL = vURL + "?PuestoId=" + pIdPuesto;
                var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
            }

            function onCloseWindow(oWnd, args) {
                $find("<%=grdDescriptivo.ClientID%>").get_masterTableView().rebind();
            }

            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView();
                    var selectedRows = MasterTable.get_selectedItems();
                    var row = selectedRows[0];

                    if (row != null) {
                        if (selectedRows != "") {
                            CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_PUESTO");
                            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                            { if (shouldSubmit) { this.click(); } });
                            radconfirm("¿Deseas eliminar el puesto " + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 200, null, "Aviso");
                            args.set_cancel(true);
                        }
                    } else {
                        radalert("Seleccione un Puesto.", 400, 150, "Aviso");
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

                function ShowFactoresForm() {
                    var selectedItem = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenFactoresWindow(selectedItem.getDataKeyValue("ID_PUESTO"), null);
                else
                    radalert("Selecciona un puesto.", 400, 150, "Aviso");
            }

            function OpenFactoresWindow(pIdPuesto) {
                var vURL = "VentanaDescriptivoFactores.aspx";
                var vTitulo = "Definición de factores para consulta global";

                vURL = vURL + "?PuestoId=" + pIdPuesto


                var windowProperties = {
                    width: 850,
                    height: 430
                };
                openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
            }

            //function pruebatabla() {
            //    var win = window.open("PruebaTableroControl.aspx", '_blank');
            //    win.focus();
            //}

            function ShowReporteDescriptivoPuesto() {
                var selectedItem = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenReport(selectedItem.getDataKeyValue("ID_PUESTO_DO"), null);
                else
                    radalert("Selecciona un puesto.", 400, 150, "Aviso");
            }

            function OpenReport(pIdPuesto) {
                var vURL = "VentanaVistaPreviaDescriptivo.aspx";
                var vTitulo = "Consulta descriptivo de puesto";

                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                vURL = vURL + "?pIdPuesto=" + pIdPuesto;
                var win = window.open(vURL, '_blank');
                win.focus();
                //var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
            }


            function ValidaFunciones() {
                var selectedItem = $find("<%=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    var vFgActivoDo = selectedItem.getDataKeyValue("FG_DO");
                    var btnCopiarde = $find("<%= btnCopiarde.ClientID %>");
                    var btnVistaPrevia = $find("<%= btnVistaPrevia.ClientID %>");
                    var btnReporte = $find("<%= btnReporte.ClientID %>");

                    if (vFgActivoDo == "False") {
                        btnCopiarde.set_enabled(false);
                        btnVistaPrevia.set_enabled(false);
                        btnReporte.set_enabled(false);
                    }
                    else {

                        if ('<%= VistaPrevia %>' == "True")
                            btnVistaPrevia.set_enabled(true);
                        if ('<%= CopiarDe %>' == "True")
                            btnCopiarde.set_enabled(true);
                        if ('<%= vReporte %>' == "True")
                            btnReporte.set_enabled(true);
                    }
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDescriptivo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDescriptivo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDescriptivo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDescriptivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDescriptivo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Descriptivos de puestos</label>
    <div style="height: calc(100% - 100px);">
                <telerik:RadSplitter ID="splPuestos" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridPuestos" runat="server">
        <telerik:RadGrid ID="grdDescriptivo" runat="server" AllowPaging="true" AllowSorting="true" GroupPanelPosition="Top" Height="100%" AllowFilteringByColumn="true"
            OnNeedDataSource="grdDescriptivo_NeedDataSource" HeaderStyle-Font-Bold="true" OnItemDataBound="grdDescriptivo_ItemDataBound">
            <GroupingSettings CaseSensitive="False" />
            <ExportSettings FileName="DescriptivoPuesto" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                 <ClientEvents OnRowSelected="ValidaFunciones" />
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_PUESTO, FG_DO, ID_PUESTO_DO" DataKeyNames="ID_PUESTO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10" CommandItemDisplay="Top">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Descripción" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                      <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Disponible" DataField="NB_ACTIVO_NOMINA" UniqueName="NB_ACTIVO_NOMINA" HeaderStyle-Width="80" FilterControlWidth="20"></telerik:GridBoundColumn>--%>
                    <%-- <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="60" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION_PUESTO" UniqueName="FE_MODIFICACION_PUESTO" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
                </telerik:RadPane>
                    </telerik:RadSplitter>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnAgregarNomina"  runat="server" Text="Agregar"  OnClientClicked="ShowInsertNomina" AutoPostBack="false"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditarNomina"  OnClientClicked="ShowEditNomina" AutoPostBack="false" runat="server" Text="Editar" ></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminarNomina"  runat="server" Text="Eliminar"  AutoPostBack="true" OnClick="btnEliminarNomina_Click" OnClientClicking="ConfirmarEliminarNomina"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnVistaPrevia"  OnClientClicked="ShowPreviewForm" AutoPostBack="false" runat="server" Text="Consultar" ></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnCopiarde"  OnClientClicked="ShowCopyForm" AutoPostBack="false" runat="server" Text="Copiar" ToolTip="Este botón te permite copiar la información de un descriptivo de puesto seleccionado para crear uno nuevo. Sólo debes indicar una nueva clave para el nuevo descriptivo de puesto." ></telerik:RadButton>
    </div>    
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnReporte"  OnClientClicked="ShowReporteDescriptivoPuesto" AutoPostBack="false" runat="server" Text="Imprimir"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server" Animation="Fade" >
        <Windows>
            <telerik:RadWindow ID="winDescriptivo" runat="server" Title="Agregar/Editar Puestos" Height="600px" Width="500px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="None" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winVistaPrevia" runat="server" Title="Consulta puesto" Height="800px" Width="800px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionPuestos" runat="server" Title="Seleccionar Jefe inmediato" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winOrganigrama" runat="server" Title="Organigrama Puesto" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winNuevoDescriptivo" runat="server" Behaviors="Close" Modal="true" Width="490" Height="350" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
