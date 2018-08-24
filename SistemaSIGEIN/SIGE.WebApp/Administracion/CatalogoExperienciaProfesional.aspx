<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoExperienciaProfesional.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoExperienciaProfesional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            var idExperienciaProfesional = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdCatExperProf.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idExperienciaProfesional = SelectDataItem.getDataKeyValue("ID_AREA_INTERES");
                }
            }

            function onCloseWindow(oWnd, args) {

                idExperienciaProfesional = "";
                $find("<%=grdCatExperProf.ClientID%>").get_masterTableView().rebind();

            }

            function ShowPopupEditarAreaInteres() {
                obtenerIdFila();

                if (idExperienciaProfesional != "") {
                    var oWnd = radopen("VentanaCatExperProf.aspx?&ID=" + idExperienciaProfesional + "&TIPO=Editar", "RWPopupmodalCatalogoGenericoEditar");
                    oWnd.set_title("Editar experiencia profesional");
                } else {
                    radalert("No has seleccionado un registro", 400, 150, "");
                }
            }

            function ShowPopupAgregarAreaInteres() {
                var oWnd = radopen("VentanaCatExperProf.aspx?&TIPO=Agregar", "RWPopupmodalCatalogoGenericoEditar");
                oWnd.set_title("Agregar experiencia profesional");
            }

            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdCatExperProf.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_AREA_INTERES");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar la experiencia profesional ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione una experiencia profesional.", 400, 150, "");
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
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCatExperProf" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCatExperProf" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCatExperProf" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCatExperProf">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCatExperProf" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <label class="labelTitulo">Experiencia profesional</label>
    <div style="height: calc(100% - 100px);">

        <telerik:RadGrid ID="grdCatExperProf" ShowHeader="true" runat="server" AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" Width="600px" GridLines="None"
            Height="100%"
            AllowFilteringByColumn="true"
            HeaderStyle-Font-Bold="true"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnNeedDataSource="grdCatExperProf_NeedDataSource"
            OnItemDataBound="grdCatExperProf_ItemDataBound"
            OnItemCommand="OnItemCommand">
            <GroupingSettings CaseSensitive="False" />
            <ExportSettings FileName="Catalogo Experiencias Profesionales" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_AREA_INTERES" DataKeyNames="ID_AREA_INTERES" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_AREA_INTERES" UniqueName="CL_AREA_INTERES" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_AREA_INTERES" UniqueName="NB_AREA_INTERES" HeaderStyle-Width="400" FilterControlWidth="330"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Activo" DataField="NB_ACTIVO" UniqueName="Activo" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupAgregarAreaInteres" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupEditarAreaInteres" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminar_click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWPopupmodalCatalogoGenericoEditar" runat="server" Title="Catálogo Experiencia profesional" Height="280"
                Width="595" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
