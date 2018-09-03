<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoAreasTematicas.aspx.cs" Inherits="SIGE.WebApp.STPS.CatalogoAreasTematicas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            var idAreaTematica = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdAreasTematicas.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idAreaTematica = SelectDataItem.getDataKeyValue("ID_AREA_TEMATICA");
                }
            }

            function onCloseWindow(oWnd, args) {
                idAreaTematica = "";
                $find("<%=grdAreasTematicas.ClientID%>").get_masterTableView().rebind();

            }
            function ShowPopupEditarAreas() {

                obtenerIdFila();
                if (idAreaTematica != "") {
                    var oWnd = radopen("VentanaCatalogoAreasTematicas.aspx?&ID=" + idAreaTematica, "RWPopupmodalCatalogoGenericoEditar");
                    oWnd.set_title("Editar área temática");
                } else {
                    radalert("No has seleccionado un registro.", 350, 148, "");
                }
            }

            function ShowPopupAgregarAreas() {
                var oWnd = radopen("VentanaCatalogoAreasTematicas.aspx", "RWPopupmodalCatalogoGenericoEditar");
                oWnd.set_title("Agregar área temática");
            }

            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdAreasTematicas.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_AREA_TEMATICA");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar el área temática ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione un área temática.", 400, 150, "");
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
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreasTematicas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreasTematicas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreasTematicas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdAreasTematicas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAreasTematicas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>

    <label class="labelTitulo">Áreas temáticas</label>
    <div style="height: calc(100% - 100px);">

        <telerik:RadGrid ID="grdAreasTematicas" ShowHeader="true" runat="server" AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" Width="1000px" GridLines="None" HeaderStyle-Font-Bold="true"
            Height="100%"
            AllowFilteringByColumn="true"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnNeedDataSource="grdAreasTematicas_NeedDataSource"
            OnItemDataBound="grdAreasTematicas_ItemDataBound"
            OnItemCommand="grdAreasTematicas_ItemCommand">
            <GroupingSettings CaseSensitive="False" />
            <ExportSettings FileName="Cátalogo de áreas temáticas" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_AREA_TEMATICA" DataKeyNames="ID_AREA_TEMATICA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_AREA_TEMATICA" UniqueName="CL_AREA_TEMATICA" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_AREA_TEMATICA" UniqueName="NB_AREA_TEMATICA" HeaderStyle-Width="400" FilterControlWidth="330"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>

            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupAgregarAreas" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupEditarAreas" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminar_Click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWPopupmodalCatalogoGenericoEditar" runat="server" Title="Catálogo de Áreas temáticas." Height="270"
                Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


</asp:Content>
