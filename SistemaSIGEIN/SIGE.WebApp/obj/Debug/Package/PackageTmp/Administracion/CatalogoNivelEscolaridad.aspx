<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoNivelEscolaridad.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoNivelEscolaridad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">


            var idEscolaridad = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdNivelEscolaridades.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idEscolaridad = SelectDataItem.getDataKeyValue("ID_NIVEL_ESCOLARIDAD");
                }
            }


            function onCloseWindow(oWnd, args) {
                idEscolaridad = "";

                $find("<%=grdNivelEscolaridades.ClientID%>").get_masterTableView().rebind();
            }



            function ShowPopupEditarEscolaridades() {

                var MasterTable = $find("<%=grdNivelEscolaridades.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (selectedRows != "") {
                    CELL_ID = MasterTable.getCellByColumnUniqueName(row, "ID_NIVEL_ESCOLARIDAD");

                    if ((CELL_ID != null)) {

                        var oWnd = radopen("VentanaCatalogoNivelEscolaridad.aspx?&ID=" + CELL_ID.innerHTML + "&TIPO=Editar", "RWPopupmodalCatalogoGenericoEditar");
                        oWnd.set_title("Editar Nivel Escolaridad");
                    }
                } else {
                    radalert("No has seleccionado un registro.", 400, 150, "");
                }
            }



            function ShowPopupAgregarEscolaridades() {

                var oWnd = radopen("VentanaCatalogoNivelEscolaridad.aspx", "RWPopupmodalCatalogoGenericoEditar");
                oWnd.set_title("Agregar Nivel Escolaridad");

            }



            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdNivelEscolaridades.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "DS_NIVEL_ESCOLARIDAD");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar el nivel de escolaridad ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        //Deseas eliminar ___________?, este proceso no podrá revertirse.
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione un nivel de escolaridad.", 400, 150, "");
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



    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdNivelEscolaridades" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdNivelEscolaridades" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdNivelEscolaridades" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbEscolaridades">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdNivelEscolaridades" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdNivelEscolaridades">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdNivelEscolaridades" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Niveles de escolaridad</label>



    <div id="ContenidoGrid" class="ctrlBasico" style="height: calc(100% - 100px);">

        <telerik:RadGrid ID="grdNivelEscolaridades" ShowHeader="true" runat="server" AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" GridLines="None"
            AllowFilteringByColumn="true" Height="100%" Width="700px"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnNeedDataSource="grdNivelEscolaridades_NeedDataSource"
            OnItemDataBound="grdNivelEscolaridades_ItemDataBound"
            OnItemCommand="OnItemCommand"
            HeaderStyle-Font-Bold="true">


            <GroupingSettings CaseSensitive="False" />
            <ExportSettings FileName="Catalogo de Escolaridades" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />


            <MasterTableView ClientDataKeyNames="ID_NIVEL_ESCOLARIDAD" DataKeyNames="ID_NIVEL_ESCOLARIDAD" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />

                <Columns>

                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderText="Clave" CurrentFilterFunction="Contains" DataField="CL_NIVEL_ESCOLARIDAD" UniqueName="CL_NIVEL_ESCOLARIDAD" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderText="Descripción" CurrentFilterFunction="Contains" DataField="DS_NIVEL_ESCOLARIDAD" UniqueName="DS_NIVEL_ESCOLARIDAD" HeaderStyle-Width="400" FilterControlWidth="330"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderText="Activo" CurrentFilterFunction="Contains" DataField="NB_ACTIVO" UniqueName="Activo" FilterControlWidth="25" HeaderStyle-Width="95"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderText="ID_NIVEL_ESCOLARIDAD" CurrentFilterFunction="Contains" DataField="ID_NIVEL_ESCOLARIDAD" UniqueName="ID_NIVEL_ESCOLARIDAD" HeaderStyle-Width="0" FilterControlWidth="0"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>

            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <br />

    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupAgregarEscolaridades" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupEditarEscolaridades" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" AutoPostBack="true" OnClick="btnEliminar_click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>


    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWPopupmodalCatalogoGenericoEditar" runat="server" Title="Agregar Catálogos de Escolaridades" Height="320"
                Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="grdMensaje" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
