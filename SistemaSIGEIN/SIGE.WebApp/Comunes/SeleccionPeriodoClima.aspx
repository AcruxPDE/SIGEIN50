<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="SeleccionPeriodoClima.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionPeriodoClima" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vPeriodos = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vPeriodos;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdPeriodos.ClientID %>").get_masterTableView();
             var selectedItems = masterTable.get_selectedItems();
             if (selectedItems.length > 0) {
                 for (i = 0; i < selectedItems.length; i++) {
                     selectedItem = selectedItems[i];
                     var vPeriodo = {
                         idPeriodo: selectedItem.getDataKeyValue("ID_PERIODO"),
                         clPeriodo: masterTable.getCellByColumnUniqueName(selectedItem, "CL_PERIODO").innerHTML,
                         nbPeriodo: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PERIODO").innerHTML,
                         clTipoCatalogo: "PERIODO"
                     };

                     vPeriodos.push(vPeriodo);
                     var vLabel = document.getElementsByName('lblAgregar')[0];
                     vLabel.innerText = "Agregados: " + vPeriodos.length;
                 }

                 return true;
             }
             else {
                 var currentWnd = GetRadWindow();
                 var browserWnd = window;
                 if (currentWnd)
                     browserWnd = currentWnd.BrowserWindow;
                 browserWnd.radalert("Selecciona un periodo.", 400, 150);
             }

             return false;
         }

         function cancelarSeleccion() {
             sendDataToParent(null);
         }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPeriodos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdPeriodos" />
                    <telerik:AjaxUpdatedControl ControlID="grdPeriodos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="splPuesto" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpnGridArea" runat="server">

            <div style="height: calc(100% - 54px);">
                <telerik:RadGrid ID="grdPeriodos" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdPeriodos_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdPeriodos_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_PERIODO" DataKeyNames="ID_PERIODO" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="60" HeaderText="Periodo" DataField="NB_PERIODO" UniqueName="NB_PERIODO"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="divControlDerecha" style="padding-top: 10px;">
                <div class="ctrlBasico">
                    <label runat="server" id="lblAgregar" name="lblAgregar" style="padding-top: 10px; font-weight: lighter;"></label>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregar" runat="server" name="btnAgregar" Text="Agregar" AutoPostBack="false" OnClientClicked="addSelection"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Seleccionar" AutoPostBack="false" OnClientClicked="generateDataForParent"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnCancelar" runat="server" name="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
                </div>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpnBusqueda" runat="server" Width="30">
            <telerik:RadSlidingZone ID="slzBusqueda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                <telerik:RadSlidingPane ID="slpBusqueda" runat="server" Title="Búsqueda avanzada" Width="500" MinWidth="500">
                    <div style="padding: 20px;">
                        <telerik:RadFilter runat="server" ID="ftrGrdPeriodos" FilterContainerID="grdPeriodo" ShowApplyButton="true" Height="100">
                            <ContextMenu Height="300" EnableAutoScroll="true">
                                <DefaultGroupSettings Height="300" />
                            </ContextMenu>
                        </telerik:RadFilter>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
