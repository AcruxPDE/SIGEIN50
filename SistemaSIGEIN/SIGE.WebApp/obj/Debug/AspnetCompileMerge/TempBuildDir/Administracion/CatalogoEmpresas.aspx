<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoEmpresas.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoEmpresas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">


            var idEmpresa = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdCatEmpresas.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idEmpresa = SelectDataItem.getDataKeyValue("ID_EMPRESA");
                }
            }

            function onCloseWindow(oWnd, args) {
                idEmpresa = "";
                $find("<%=grdCatEmpresas.ClientID%>").get_masterTableView().rebind();

            }

            function ShowPopupEditarEmpresas() {

                obtenerIdFila();
                if (idEmpresa != "") {
                    var oWnd = radopen("VentanaCatalogoEmpresas.aspx?&ID=" + idEmpresa + "&TIPO=Editar", "RWPopupmodalCatalogoGenericoEditar");
                    oWnd.set_title("Editar Empresa");
                } else {
                    radalert("No has seleccionado un registro.", 350, 148, "");
                }
            }

            function ShowPopupAgregarEmpresas() {
                var oWnd = radopen("VentanaCatalogoEmpresas.aspx?&TIPO=Agregar", "RWPopupmodalCatalogoGenericoEditar");
                oWnd.set_title("Agregar Empresa");
            }



            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdCatEmpresas.ClientID %>").get_masterTableView();
                 var selectedRows = MasterTable.get_selectedItems();

                 var row = selectedRows[0];

                 if (row != null) {
                     CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_EMPRESA");
                     if (selectedRows != "") {
                         var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                         { if (shouldSubmit) { this.click(); } });

                         radconfirm('¿Deseas eliminar la empresa ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                         args.set_cancel(true);
                     }
                 } else {
                     radalert("Seleccione una empresa.", 400, 150, "");
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
        <ClientEvents OnRequestStart="onRequestStart"/>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCatEmpresas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCatEmpresas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCatEmpresas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCatEmpresas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCatEmpresas" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Empresas</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdCatEmpresas" ShowHeader="true" runat="server" AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" Width="900px" GridLines="None"
            Height="100%"
            AllowFilteringByColumn="true"
            HeaderStyle-Font-Bold="true"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnNeedDataSource="grdCatEmpresas_NeedDataSource"
            OnItemDataBound="grdCatEmpresas_ItemDataBound"
            OnItemCommand="OnItemCommand">
            <GroupingSettings CaseSensitive="False" />
            <ExportSettings FileName="Cátalogo de empresas" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_EMPRESA" DataKeyNames="ID_EMPRESA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn  AutoPostBackOnFilter="true"   CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_EMPRESA" UniqueName="CL_EMPRESA" HeaderStyle-Width="300" FilterControlWidth="230"  ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn  AutoPostBackOnFilter="true"   CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="300" FilterControlWidth="230"  ></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>

            </MasterTableView>
        </telerik:RadGrid>
    </div>



    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupAgregarEmpresas" AutoPostBack="false" runat="server" Text="Agregar" Width="100" ></telerik:RadButton>
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupEditarEmpresas" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminar_click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>



    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWPopupmodalCatalogoGenericoEditar" runat="server" Title="Catálogo Empresas." Height="240"
                Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow" >
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
