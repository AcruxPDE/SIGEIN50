<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="AgregarCandidato.aspx.cs" Inherits="SIGE.WebApp.IDP.AgregarCandidato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdCandidatos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">


            var idCandidato = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdCandidatos.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idCandidato = SelectDataItem.getDataKeyValue("ID_CANDIDATO");
                }
            }

            function onCloseWindow(oWnd, args) {
                idDepartamentos = "";
                $find("<%=grdCandidatos.ClientID%>").get_masterTableView().rebind();
            }

            function ShowPopupEditarAreas() {
             
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


     <div style="clear: both; height: 10px;"></div>
   <div class="ctrlBasico" style="height: calc(100% - 80px);">

        <telerik:RadGrid ID="grdCandidatos" ShowHeader="true" runat="server" AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" Width="1200px" GridLines="None"
            Height="100%"
            AllowFilteringByColumn="true"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnNeedDataSource="grdCandidatos_NeedDataSource"
            OnItemDataBound="grdCandidatos_ItemDataBound"
            OnItemCommand="OnItemCommand">
            <GroupingSettings CaseSensitive="False" />

            <ExportSettings FileName="Catalogo de Escolaridades" ExportOnlyData="true" >
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_CANDIDATO" DataKeyNames="ID_CANDIDATO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                   <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio" DataField="NO_FOLIO" UniqueName="NO_FOLIO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de empleado" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Status" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Requisición	Empresa" DataField="NO_REQUISICION" UniqueName="NO_REQUISICION" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Requisición	Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="170" FilterControlWidth="100"></telerik:GridBoundColumn>
                </Columns>

            </MasterTableView>
        </telerik:RadGrid>
    </div>


     <div style="clear: both; height: 10px;"></div>
    <div class="divControlDerecha">
         <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupEditarAreas" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
    </div>
      </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>

</asp:Content>
