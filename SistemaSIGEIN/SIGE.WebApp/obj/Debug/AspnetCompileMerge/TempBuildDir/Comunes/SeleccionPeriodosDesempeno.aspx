<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="SeleccionPeriodosDesempeno.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionPeriodosDesempeno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        function generateDataForParent() {

            var info = null;
            var vPeriodos = [];
            var masterTable = $find("<%= grdPeriodosDesempeno.ClientID %>").get_masterTableView();
             var selectedItems = masterTable.get_selectedItems();

             if (selectedItems.length > 0) {
                 for (i = 0; i < selectedItems.length; i++) {
                     selectedItem = selectedItems[i];

                     var vPeriodo = {
                         idPeriodo: selectedItem.getDataKeyValue("ID_PERIODO"),
                         clPeriodo: masterTable.getCellByColumnUniqueName(selectedItem, "CL_PERIODO").innerHTML,
                         nbPeriodo: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PERIODO").innerHTML,
                         dsPeriodo: masterTable.getCellByColumnUniqueName(selectedItem, "DS_PERIODO").innerHTML,
                         dsNotas: selectedItem.getDataKeyValue("DS_NOTAS"),
                         feInicio: selectedItem.getDataKeyValue("FE_INICIO"),
                         feTermino: selectedItem.getDataKeyValue("FE_TERMINO"),
                         idEvaluado: selectedItem.getDataKeyValue("ID_EVALUADO"),
                         clOrigen: selectedItem.getDataKeyValue("CL_ORIGEN_CUESTIONARIO"),
                         clTipoCopia: selectedItem.getDataKeyValue("CL_TIPO_COPIA"),
                         clTipoCatalogo: "PERIODODESEMPENO"
                     };

                     vPeriodos.push(vPeriodo);
                 }

                 sendDataToParent(vPeriodos);
             }
             else {
                 var currentWnd = GetRadWindow();
                 var browserWnd = window;
                 if (currentWnd)
                     browserWnd = currentWnd.BrowserWindow;
                 browserWnd.radalert("Selecciona un periodo.", 400, 150);
             }
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
            <telerik:AjaxSetting AjaxControlID="grdPeriodosDesempeno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPeriodosDesempeno" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdPeriodos" />
                    <telerik:AjaxUpdatedControl ControlID="grdPeriodosDesempeno" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="splPuesto" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpnGridArea" runat="server">

            <div style="height: calc(100% - 54px);">
                <telerik:RadGrid ID="grdPeriodosDesempeno" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdPeriodosDesempeno_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdPeriodosDesempeno_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_PERIODO, DS_NOTAS, FE_INICIO, FE_TERMINO, ID_EVALUADO, CL_ORIGEN_CUESTIONARIO, CL_TIPO_COPIA" DataKeyNames="ID_PERIODO, DS_NOTAS, FE_INICIO, FE_TERMINO, ID_EVALUADO, CL_ORIGEN_CUESTIONARIO, CL_TIPO_COPIA" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="80" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="80" HeaderText="Período" DataField="NB_PERIODO" UniqueName="NB_PERIODO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="220" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="divControlDerecha" style="padding-top: 10px;">
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

