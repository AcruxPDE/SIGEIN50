<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionArea.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        var vaddSelection_label = "Agregados: ";
        var vaddSelection_alert = "Selecciona una área/departamento";

        if ('<%=vClIdioma%>' != "ES") {
            vaddSelection_label = '<%=vaddSelection_label%>';
            vaddSelection_alert = '<%=vaddSelection_alert%>';
        }

        var vAreas = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vAreas;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdArea.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vArea = {
                        idArea: selectedItem.getDataKeyValue("ID_DEPARTAMENTO"),
                        idArea_pde: selectedItem.getDataKeyValue("ID_DEPARTAMENTO_PDE"),
                        clArea: masterTable.getCellByColumnUniqueName(selectedItem, "CL_DEPARTAMENTO").innerHTML,
                        nbArea: masterTable.getCellByColumnUniqueName(selectedItem, "NB_DEPARTAMENTO").innerHTML,
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    if (!existeElemento(vArea)) {
                        vAreas.push(vArea);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = vaddSelection_label + vAreas.length;
                    }
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
<<<<<<< HEAD
                browserWnd.radalert(vaddSelection_alert, 400, 150);
=======
                browserWnd.radalert("Selecciona una área/departamento", 400, 150);
>>>>>>> DEV

            }

            return false;

        }

        function existeElemento(pArea) {
            for (var i = 0; i < vAreas.length; i++) {
                var vValue = vAreas[i];
                if (vValue.idArea == pArea.idArea)
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
            <telerik:AjaxSetting AjaxControlID="grdArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdArea" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdArea" />
                    <telerik:AjaxUpdatedControl ControlID="grdArea" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="splPuesto" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpnGridArea" runat="server">

            <div style="height: calc(100% - 54px);">
                <telerik:RadGrid ID="grdArea" HeaderStyle-Font-Bold="true" OnPreRender="grdArea_PreRender" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdArea_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdArea_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_DEPARTAMENTO, ID_DEPARTAMENTO_PDE" DataKeyNames="ID_DEPARTAMENTO, ID_DEPARTAMENTO_PDE" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_DEPARTAMENTO" UniqueName="CL_DEPARTAMENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="60" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
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
                        <telerik:RadFilter runat="server" ID="ftrGrdArea" FilterContainerID="grdArea" ShowApplyButton="true" Height="100" >
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
