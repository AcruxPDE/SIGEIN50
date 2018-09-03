<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="ProgramasCapacitacion.aspx.cs" Inherits="SIGE.WebApp.FYD.ProgramasCapacitacion" %>

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
                background-color: #FF7400 !important;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
               <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="rlvProgramas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtClPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rlvProgramas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtDsPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtNotas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtClEstatus" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtTipo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtUsuarioMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtFechaMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rlvProgramas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtClPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rlvProgramas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtDsPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtNotas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtClEstatus" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtTipo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtUsuarioMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtFechaMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rfFiltros" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvProgramas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbAscendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvProgramas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbDescendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvProgramas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--            <telerik:AjaxSetting AjaxControlID="grdProgramaCapacitacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProgramaCapacitacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProgramaCapacitacion" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProgramaCapacitacion" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCopiar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProgramaCapacitacion" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProgramaCapacitacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRegistroAutorizacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProgramaCapacitacion" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAvance">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProgramaCapacitacion" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            var vIdPrograma = "";
            var vClEstatus = "";
            var vClAutorizacion = "";
            var vNbPrograma = "";

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

            function closeWindow(oWnd, args) {
                vIdPrograma = "";
                vClEstatus = "";
                vNbPrograma = "";
                vClAutorizacion = "";
                var lista = $find("<%=rlvProgramas.ClientID%>");
                lista.rebind();
            }

            function GetProgramaId() {

                var listView = $find('<%= rlvProgramas.ClientID %>');
                var selectedIndex = listView.get_selectedIndexes();
                if (selectedIndex.length > 0) {
                    vIdPrograma = listView.get_clientDataKeyValue()[selectedIndex]["ID_PROGRAMA"];
                    vClEstatus = listView.get_clientDataKeyValue()[selectedIndex]["CL_ESTADO"];
                    vClAutorizacion = listView.get_clientDataKeyValue()[selectedIndex]["CL_AUTORIZACION"];
                    vNbPrograma = listView.get_clientDataKeyValue()[selectedIndex]["NB_PROGRAMA"];
                }
                else {
                    vIdPrograma = "";
                    vClEstatus = "";
                    vClAutorizacion = "";
                    vNbPrograma = "";
                }



                //var selectedItem = $find("<=rlvProgramas.ClientID %>").get_masterTableView().get_selectedItems()[0];

                //if (selectedItem != undefined) {
                //    vIdPrograma = selectedItem.getDataKeyValue("ID_PROGRAMA");
                //    vClEstatus = selectedItem.getDataKeyValue("CL_ESTADO");
                //    vClAutorizacion = selectedItem.getDataKeyValue("CL_AUTORIZACION");
                //    vNbPrograma = selectedItem.getDataKeyValue("NB_PROGRAMA");
                //}
                //else {
                //    vIdPrograma = "";
                //    vClEstatus = "";
                //    vClAutorizacion = "";
                //    vNbPrograma = "";
                //}
            }

            function GetWindowProperties() {
                return {
                    width: document.documentElement.clientWidth - 30,
                    height: document.documentElement.clientHeight - 20
                };
            }

            function OpenRadWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
            }

            function GetAvanceProgramaProperties(pIdPrograma) {
                var wnd = GetWindowProperties();
                wnd.vURL = "VentanaAvanceProgramaCapacitacion.aspx?IdPrograma=" + pIdPrograma;
                wnd.vTitulo = "Avance programa de capacitación";
                wnd.vRadWindowId = "winPrograma";
                return wnd;
            }

            function GetAutorizacionProgramaProperties(pIdPrograma) {
                var wnd = GetWindowProperties();
                wnd.vURL = "VentanaDocumentoAutorizar.aspx?IdPrograma=" + pIdPrograma + "&TIPO=Agregar";
                wnd.vTitulo = "Registro y autorización";
                wnd.vRadWindowId = "winAutoriza";
                return wnd;
            }

            function GetConfigurarPropierties(pIdPrograma, pFgEsCopia) {
                var wnd = GetWindowProperties();
                wnd.vURL = "VentanaProgramasCapacitacion.aspx";
                wnd.vURL = wnd.vURL + "?ID=" + pIdPrograma;
                wnd.vTitulo = "Configurar programa de capacitación";
                wnd.vRadWindowId = "winPrograma";
                var vTipoOperacion = "&TIPO=Editar";

                wnd.vURL = wnd.vURL + vTipoOperacion;

                return wnd;
            }

            function GetProgramaPropierties(pIdPrograma, pFgEsCopia) {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 700,
                    height: browserWnd.innerHeight - 50
                };

                var wnd = windowProperties;
                // wnd.vURL = "VentanaProgramasCapacitacion.aspx";
                wnd.vURL = "VentanaAgregarPrograma.aspx";
                wnd.vTitulo = "Agregar programa de capacitación";
                wnd.vRadWindowId = "winPrograma";
                var vTipoOperacion = "?TIPO=Agregar";

                if (pIdPrograma != null & !pFgEsCopia) {
                    wnd.vURL = wnd.vURL + "?ID=" + pIdPrograma;
                    wnd.vTitulo = "Editar programa de capacitación";
                    vTipoOperacion = "&TIPO=Editar";
                }

                if (pIdPrograma != null & pFgEsCopia) {
                    wnd.vURL = wnd.vURL + "?ID=" + pIdPrograma;
                    vTipoOperacion = "&TIPO=Copy";
                }

                wnd.vURL = wnd.vURL + vTipoOperacion;

                return wnd;
            }

            function OpenAvanceProgramaWindow() {
                GetProgramaId();

                if (vIdPrograma != "")
                    OpenRadWindow(GetAvanceProgramaProperties(vIdPrograma));
                else
                    radalert("Selecciona un programa.", 400, 150);
            }

            function OpenAutorizacionProgramaWindow() {
                GetProgramaId();
                if (vIdPrograma != "") {
                    OpenRadWindow(GetAutorizacionProgramaProperties(vIdPrograma));
                }
                else {
                    radalert("Selecciona un programa.", 400, 150);
                }
            }

            function OpenNuevoProgramaWindow() {
                OpenRadWindow(GetProgramaPropierties(null, false));
            }

            function OpenConfigurarProgramaWindow() {
                GetProgramaId();
                if (vIdPrograma != "") {
                    OpenRadWindow(GetConfigurarPropierties(vIdPrograma, false));
                }
                else {
                    radalert("Selecciona un programa.", 400, 150);
                }
            }

            function OpenEditarProgramaWindow() {
                if ('<%= vEditar %>' == "True") {
                GetProgramaId();
                if (vIdPrograma != "") {
                    OpenRadWindow(GetProgramaPropierties(vIdPrograma, false));
                }
                else {
                    radalert("Selecciona un programa.", 400, 150);
                }
                }
                else {
                    radalert("No tiene los permisos necesarios para llevar a cabo esta función.", 450, 200, "");
                }
            }

            function OpenCopyProgramaWindow() {
                GetProgramaId();

                if (vIdPrograma != "") {
                    OpenRadWindow(GetProgramaPropierties(vIdPrograma, true));
                }
                else {
                    radalert("Selecciona un programa.", 400, 150);
                }
            }

            function ConfirmarEliminar(sender, args) {
                if ('<%= vEliminar %>' == "True") {

                    GetProgramaId();

                    if (vIdPrograma != "") {
                        //CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_PROGRAMA");

                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar el programa de capacitación ' + vNbPrograma + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);

                    } else {
                        radalert("Seleccione un programa de capacitación.", 400, 150, "");
                        args.set_cancel(true);
                    }
                }
                else {
                    radalert("No tiene los permisos necesarios para llevar a cabo esta función.", 450, 200, "");
                    args.set_cancel(true);
                }
            }

            function ConfirmarTerminar(sender, args) {
                GetProgramaId();

                if (vIdPrograma != "") {

                    if (vClEstatus == "Terminado") {
                        radalert("El programa de capacitación ya está terminado.", 400, 150, "");
                        args.set_cancel(true);
                    }
                    else if (vClAutorizacion == "Por Autorizar") {
                        radalert("El programa de capacitación no puede cambiar a terminado.", 400, 150, "");
                        args.set_cancel(true);
                    }
                    else {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas terminar el programa de capacitación ' + vNbPrograma + '?', callBackFunction, 400, 170, null, "Terminar programa");
                        args.set_cancel(true);
                    }

                } else {
                    radalert("Selecciona un programa de capacitación.", 400, 150, "");
                    args.set_cancel(true);
                }
            }

            function useDataFromChild(pData) {
                if (pData != null) {
                    var tipo = pData[0].clTipoCatalogo;
                    switch (tipo) {
                        case "ACTUALIZARLISTA":
                            var ajaxManager = $find('<%= RadAjaxManager1.ClientID%>');
                        ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "ACTUALIZARLISTA" }));
                        break;
                    case "ACTUALIZAR":
                        var ajaxManager = $find('<%= RadAjaxManager1.ClientID%>');
                        ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "ACTUALIZAR" }));
                        break;
                }
            }
        }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadSplitter ID="rsProgramas" BorderSize="0" Width="100%" Height="100%" runat="server">
        <telerik:RadPane ID="rpProgramas" runat="server">
            <label class="labelTitulo">Programas de capacitación</label>
            <div class="ctrlBasico">
                <telerik:RadListView ID="rlvProgramas" runat="server" DataKeyNames="ID_PROGRAMA" ClientDataKeyNames="ID_PROGRAMA,CL_ESTADO,NB_PROGRAMA, CL_AUTORIZACION"
                    OnNeedDataSource="rlvProgramas_NeedDataSource" OnItemCommand="rlvProgramas_ItemCommand" AllowPaging="true" ItemPlaceholderID="ProductsHolder">
                    <LayoutTemplate>
                        <div style="overflow: auto; width: 700px;">
                            <div style="overflow: auto; overflow-y: auto; max-height: 450px;">
                                <asp:Panel ID="ProductsHolder" runat="server"></asp:Panel>
                            </div>
                            <div style="clear: both;"></div>
                            <telerik:RadDataPager ID="rdpPrpgramas" runat="server" PagedControlID="rlvProgramas" PageSize="6" Width="630">
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
                                    <label>Clave:</label>
                                    <%# Eval("CL_PROGRAMA") %>
                                </div>
                                <div>
                                    <label>Estatus:</label>
                                    <%# Eval("CL_ESTADO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label>Descripción:</label>
                                    <%# Eval("NB_PROGRAMA") %>
                                </div>
                            </div>
                            <div class="SelectionOptions">
                                <telerik:RadButton ID="btnSeleccionar" runat="server" CommandName="Select" Text="Seleccionar" Width="100%"></telerik:RadButton>
                            </div>
                        </div>
                    </ItemTemplate>
                    <SelectedItemTemplate>
                        <div class="RadListViewContainer Selected">
                            <div style="padding: 10px;">
                                <div style="overflow: auto; overflow-y: auto; height: 35px;">
                                    <label style="color: white;">Clave:</label>
                                    <%# Eval("CL_PROGRAMA") %>
                                </div>
                                <div>
                                    <label style="color: white;">Estatus:</label>
                                    <%# Eval("CL_ESTADO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label style="color: white;">Descripción:</label>
                                    <%# Eval("NB_PROGRAMA") %>
                                </div>
                            </div>
                            <div class="SelectionOptions">
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnModificar" runat="server" Text="Editar" OnClientClicked="OpenEditarProgramaWindow" AutoPostBack="false" Width="100%"></telerik:RadButton>
                                </div>
                                <div style="width: 50%; float: left; padding-left: 5px;">
                                    <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100%" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </SelectedItemTemplate>
                    <EmptyDataTemplate>
                        <div class="RadListViewContainer" style="overflow: auto; text-align: center; width: 660px; height: 100px;">
                            No hay programas disponibles
                        </div>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
            <%--
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid
            ID="grdProgramaCapacitacion"
            ShowHeader="true"
            runat="server"
            HeaderStyle-Font-Bold="true"
            AllowPaging="true"
            AllowSorting="true"
            GroupPanelPosition="Top"
            Height="100%"
            AllowFilteringByColumn="true"
            OnNeedDataSource="grdProgramaCapacitacion_NeedDataSource"
            OnItemDataBound="grdProgramaCapacitacion_ItemDataBound">
            <GroupingSettings CaseSensitive="false" />
            <ExportSettings FileName="Programas de capacitación" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_PROGRAMA,CL_ESTADO,NB_PROGRAMA, CL_AUTORIZACION" DataKeyNames="ID_PROGRAMA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_PROGRAMA" UniqueName="CL_PROGRAMA" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_PROGRAMA" UniqueName="NB_PROGRAMA" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Tipo de programa" DataField="CL_TIPO_PROGRAMA" UniqueName="CL_TIPO_PROGRAMA" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus autorización" DataField="CL_AUTORIZACION" UniqueName="CL_AUTORIZACION" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Participantes" DataField="NO_PARTICIPANTES" UniqueName="NO_PARTICIPANTES" HeaderStyle-Width="150" FilterControlWidth="80" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Competencias" DataField="NO_COMPETENCIAS" UniqueName="NO_COMPETENCIAS" HeaderStyle-Width="150" FilterControlWidth="80" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave documento" DataField="CL_DOCUMENTO" UniqueName="CL_DOCUMENTO" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Versión" DataField="VERSION" UniqueName="VERSION" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha Creación" DataField="FE_CREACION" UniqueName="FE_CREACION" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>--%>
            <%--    <div style="clear: both; height: 10px;"></div>--%>
            <div class="ctrlBasico" style="text-align: center">
                <telerik:RadTabStrip runat="server" ID="rtsProgramas" MultiPageID="rmpProgramas" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Gestionar"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Detalles"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: 10px;"></div>
                <telerik:RadMultiPage runat="server" ID="rmpProgramas" SelectedIndex="0" Height="100%" Width="100%">
                    <telerik:RadPageView ID="rpvGestionar" runat="server">
                        <div>
                            <label class="labelTitulo">Administrar</label>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnNuevo" OnClientClicked="OpenNuevoProgramaWindow" AutoPostBack="false" runat="server" Text="Agregar" ToolTip="Esta opción te permite crear un programa de capacitación a partir de 0. Podrás seleccionar libremente participantes y competencias en las que deseas capacitar:"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnEditar" OnClientClicked="OpenConfigurarProgramaWindow" AutoPostBack="false" runat="server" Text="Configurar"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCopiar" OnClientClicked="OpenCopyProgramaWindow" AutoPostBack="false" runat="server" Text="Copiar" ToolTip="Esta opción te permite crear un programa de capacitación copiándolo de otro programa que hayas diseñado con anterioridad. Selecciona el periodo deseado:  "></telerik:RadButton>
                                <%--<telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminar_Click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>--%>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnTerminarPrograma" runat="server" Text="Terminar" OnClientClicking="ConfirmarTerminar" OnClick="btnTerminarPrograma_Click"></telerik:RadButton>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <%--  <div>
                            <label class="labelTitulo">Programas</label>
                            <telerik:RadButton ID="btnRegistroAutorizacion" OnClientClicked="OpenAutorizacionProgramaWindow" AutoPostBack="false" runat="server" Text="Registro y autorización" Width="200" ToolTip="Da clic si deseas registrar este programa de capacitación y/o deseas realizar un proceso de autorización."></telerik:RadButton>
                        </div>--%>
                        <div>
                            <label class="labelTitulo">Consultas</label>
                            <telerik:RadButton ID="btnAvance" AutoPostBack="false" runat="server" OnClientClicked="OpenAvanceProgramaWindow" Text="Avance programa de capacitación"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvInformacion" runat="server">
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="lblEvento" name="lblEvento" runat="server">Programa:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClPeriodo" Enabled="false" runat="server" Width="350px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label1" name="lblEvento" runat="server">Descripción:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtDsPeriodo" Enabled="false" runat="server" Width="350px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label4" name="lblEvento" runat="server">Notas:</label>
                            </div>
                            <div class="divControlDerecha">
                                <div id="txtNotasContenedor" runat="server" style="width: 350px; height: 100px; text-align: justify; border: 1px solid #ccc; border-radius: 5px; background: #ccc;">
                                    <p id="txtNotas" runat="server" style="padding: 10px;"></p>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label2" name="lblEvento" runat="server">Estatus:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClEstatus" Enabled="false" runat="server" Width="200px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label3" name="lblEvento" runat="server">Tipo:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtTipo" Enabled="false" runat="server" Width="350px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label5" name="lblCurso" runat="server">Último usuario que modifica:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtUsuarioMod" Enabled="false" runat="server" Width="105px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label6" name="lblCurso" runat="server">Última fecha de modificación:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtFechaMod" Enabled="false" runat="server" Width="105px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpOrdenarFiltrar" runat="server" Scrolling="None" Width="20px">
            <telerik:RadSlidingZone ID="rszOrdenarFiltrar" runat="server" SlideDirection="Left" ExpandedPaneId="rsProgramas" Width="20px">
                <telerik:RadSlidingPane ID="rspOrdenarFiltrar" runat="server" Title="Ordenar y filtrar" Width="450px" RenderMode="Mobile" Height="100%">
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <fieldset>
                            <legend>Ordenar por:</legend>
                            <telerik:RadComboBox ID="cmbOrdenamiento" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Clave del programa" Value="CL_PROGRAMA" />
                                    <telerik:RadComboBoxItem Text="Nombre del programa" Value="NB_PROGRAMA" />
                                    <telerik:RadComboBoxItem Text="Estatus" Value="CL_ESTADO" />
                                    <telerik:RadComboBoxItem Text="Fecha de creación" Value="FE_CREACION" />
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
                                    <telerik:RadFilterTextFieldEditor DataType="System.Int32" DisplayName="Id" FieldName="ID_PROGRAMA" DefaultFilterFunction="Contains" ToolTip="Numero del programa" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Clave" FieldName="CL_PROGRAMA" DefaultFilterFunction="Contains" ToolTip="Clave del programa" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Nombre" FieldName="NB_PROGRAMA" DefaultFilterFunction="Contains" ToolTip="Nombre del programa" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.DateTime" DisplayName="F. Creación" FieldName="FE_CREACION" DefaultFilterFunction="GreaterThanOrEqualTo" ToolTip="Fecha creación" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Estatus" FieldName="CL_ESTADO" DefaultFilterFunction="Contains" ToolTip="Estatus del programa" />
                                </FieldEditors>
                                <Localization FilterFunctionBetween="Entre" FilterFunctionContains="Contiene" FilterFunctionDoesNotContain="No contiene" FilterFunctionEndsWith="Termina con" FilterFunctionEqualTo="Igual a" FilterFunctionGreaterThan="Mayor a" FilterFunctionGreaterThanOrEqualTo="Mayor o igual a" FilterFunctionIsEmpty="Es vacio" FilterFunctionIsNull="Es nulo" FilterFunctionLessThan="Menor que" FilterFunctionLessThanOrEqualTo="Menor o igual a" FilterFunctionNotBetween="No esta entre" FilterFunctionNotEqualTo="No es igual a" FilterFunctionNotIsEmpty="No es vacio" FilterFunctionNotIsNull="No esta nulo" FilterFunctionStartsWith="Inicia con" GroupOperationAnd="y" GroupOperationNotAnd="y no" GroupOperationNotOr="o no" GroupOperationOr="o" />
                            </telerik:RadFilter>
                        </fieldset>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <div style="clear: both"></div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winSeleccion" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winAutoriza" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="closeWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winPrograma" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
