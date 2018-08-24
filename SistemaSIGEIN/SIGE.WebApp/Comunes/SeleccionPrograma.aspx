<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionPrograma.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionPrograma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
      <script id="MyScript" type="text/javascript">
          function generateDataForParent() {
              var info = null;
              var vProgramas = [];
              var masterTable = $find("<%= grdPrograma.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vPrograma = {
                        idPrograma: selectedItem.getDataKeyValue("ID_PROGRAMA"),
                        clPrograma: masterTable.getCellByColumnUniqueName(selectedItem, "CL_PROGRAMA").innerHTML,
                        nbPrograma: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PROGRAMA").innerHTML,
                        clTipoCatalogo: "PROGRAMA"
                    };
                    vProgramas.push(vPrograma);
                }
                sendDataToParent(vProgramas);
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un programa.", 400, 150);
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
            <telerik:AjaxSetting AjaxControlID="grdPrograma">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrograma" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdPrograma">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdPrograma" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrograma" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="splPuesto" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpnGridPrograma" runat="server">

            <div style="height: calc(100% - 54px);">
                <telerik:RadGrid ID="grdPrograma" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdPrograma_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_PROGRAMA" DataKeyNames="ID_PROGRAMA" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_PROGRAMA" UniqueName="CL_PROGRAMA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="60" HeaderText="Programa" DataField="NB_PROGRAMA" UniqueName="NB_PROGRAMA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="60" HeaderText="Tipo de programa" DataField="CL_TIPO_PROGRAMA" UniqueName="CL_TIPO_PROGRAMA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="60" HeaderText="Estado" DataField="CL_ESTADO" UniqueName="CL_ESTADO"></telerik:GridBoundColumn>
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
                        <telerik:RadFilter runat="server" ID="ftrGrdPrograma" FilterContainerID="grdPrograma" ShowApplyButton="true" Height="100">
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
