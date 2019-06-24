<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionCatalogos.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionCatalogos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vCatalogos = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vCatalogos;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= rgCatalogos.ClientID %>").get_masterTableView();
             var selectedItems = masterTable.get_selectedItems();
             if (selectedItems.length > 0) {
                 for (i = 0; i < selectedItems.length; i++) {
                     selectedItem = selectedItems[i];
                     var vCatalogo = {
                         idCatalogo: selectedItem.getDataKeyValue("ID_CATALOGO_VALOR"),
                         clCatalogoValor: masterTable.getCellByColumnUniqueName(selectedItem, "CL_CATALOGO_VALOR").innerHTML,
                         nbCatalogoValor: masterTable.getCellByColumnUniqueName(selectedItem, "NB_CATALOGO_VALOR").innerHTML,
                         dsCatalogoValor: masterTable.getCellByColumnUniqueName(selectedItem, "DS_CATALOGO_VALOR").innerHTML,
                         clTipoCatalogo: "<%= vClCatalogo %>"
                     };
                     if (!existeElemento(vCatalogo)) {
                         vCatalogos.push(vCatalogo);
                         var vLabel = document.getElementsByName('lblAgregar')[0];
                         vLabel.innerText = "Agregados: " + vCatalogos.length;
                     }
                 }
                 return true;
             }
             else {
                 var currentWnd = GetRadWindow();
                 var browserWnd = window;
                 if (currentWnd)
                     browserWnd = currentWnd.BrowserWindow;
                 browserWnd.radalert("Selecciona un catalogo.", 400, 150, "Aviso");
             }

             return false;
        }

        function existeElemento(pCatalogo) {
            for (var i = 0; i < vCatalogos.length; i++) {
                var vValue = vCatalogos[i];
                if (vValue.idCatalogo == pCatalogo.idCatalogo)
                    return true;
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
            <telerik:AjaxSetting AjaxControlID="rgCatalogos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCatalogos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrCatalogos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrCatalogos" />
                    <telerik:AjaxUpdatedControl ControlID="rgCatalogos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 100%;">
        <telerik:RadSplitter ID="splCatalogos" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnCatalogos" runat="server">
                <div style="height: calc(100% - 54px);">
                    <telerik:RadGrid ID="rgCatalogos" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnNeedDataSource="rgCatalogos_NeedDataSource" AllowMultiRowSelection="true" OnItemDataBound="rgCatalogos_ItemDataBound">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView ClientDataKeyNames="ID_CATALOGO_LISTA, ID_CATALOGO_VALOR" EnableColumnsViewState="false" DataKeyNames="ID_CATALOGO_VALOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" AutoGenerateColumns="false" PageSize="10">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_CATALOGO_VALOR" UniqueName="CL_CATALOGO_VALOR" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_CATALOGO_VALOR" UniqueName="NB_CATALOGO_VALOR" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Descripción" DataField="DS_CATALOGO_VALOR" UniqueName="DS_CATALOGO_VALOR" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>

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
                        <telerik:RadButton ID="btnCancelar" runat="server" name="" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="slpAdvSearch" runat="server" Title="Búsqueda avanzada" Width="500" MinWidth="500">
                        <div style="padding: 20px;">
                            <telerik:RadFilter runat="server" ID="ftrCatalogos" FilterContainerID="rgCatalogos" ShowApplyButton="true" Height="100">
                                <ContextMenu Height="300" EnableAutoScroll="true">
                                    <DefaultGroupSettings Height="300" />
                                </ContextMenu>
                            </telerik:RadFilter>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
</asp:Content>
