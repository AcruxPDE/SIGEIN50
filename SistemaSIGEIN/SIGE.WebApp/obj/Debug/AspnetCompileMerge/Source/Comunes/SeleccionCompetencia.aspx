<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionCompetencia.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionCompetencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vCompetencias = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vCompetencias;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdCompetencia.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vCompetencia = {
                        idCompetencia: selectedItem.getDataKeyValue("ID_COMPETENCIA"),
                        clCompetencia: masterTable.getCellByColumnUniqueName(selectedItem, "CL_COMPETENCIA").innerHTML,
                        nbCompetencia: masterTable.getCellByColumnUniqueName(selectedItem, "NB_COMPETENCIA").innerHTML,
                        clTipoCompetencia: selectedItem.getDataKeyValue("CL_TIPO_COMPETENCIA"),
                        clTipoCatalogo: "COMPETENCIA"
                    };
                    vCompetencias.push(vCompetencia);
                    var vLabel = document.getElementsByName('lblAgregar')[0];
                    vLabel.innerText = "Agregados: " + vCompetencias.length;
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona una competencia.", 400, 150);
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
            <telerik:AjaxSetting AjaxControlID="grdCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCompetencia" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdCompetencia" />
                    <telerik:AjaxUpdatedControl ControlID="grdCompetencia" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="splCompetencia" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpnGridCompetencia" runat="server">
            <div style="height: calc(100% - 54px);">
                <telerik:RadGrid ID="grdCompetencia" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdCompetencia_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdCompetencia_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_COMPETENCIA,CL_TIPO_COMPETENCIA" DataKeyNames="ID_COMPETENCIA,CL_TIPO_COMPETENCIA" EnableColumnsViewState="false" AllowPaging="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="50" FilterControlWidth="30" HeaderText="Clave" DataField="CL_COMPETENCIA" UniqueName="CL_COMPETENCIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Competencia" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="140" HeaderText="Descripción" DataField="DS_COMPETENCIA" UniqueName="DS_COMPETENCIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Tipo" DataField="NB_TIPO_COMPETENCIA" UniqueName="NB_TIPO_COMPETENCIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clasificación" DataField="NB_CLASIFICACION_COMPETENCIA" UniqueName="NB_CLASIFICACION_COMPETENCIA"></telerik:GridBoundColumn>
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
                        <telerik:RadFilter runat="server" ID="ftrGrdCompetencia" FilterContainerID="grdCompetencia" ShowApplyButton="true" Height="100" ApplyButtonText="Filtrar">
                            <ContextMenu Height="300" EnableAutoScroll="true">
                                <DefaultGroupSettings Height="300" />
                            </ContextMenu>
                            <Localization FilterFunctionBetween="Entre" FilterFunctionContains="Contiene" FilterFunctionDoesNotContain="No contiene" FilterFunctionEndsWith="Termina con" FilterFunctionEqualTo="Igual a" FilterFunctionGreaterThan="Mayor a" FilterFunctionGreaterThanOrEqualTo="Mayor o igual a" FilterFunctionIsEmpty="Es vacio" FilterFunctionIsNull="Es nulo" FilterFunctionLessThan="Menor que" FilterFunctionLessThanOrEqualTo="Menor o igual a" FilterFunctionNotBetween="No esta entre" FilterFunctionNotEqualTo="No es igual a" FilterFunctionNotIsEmpty="No es vacio" FilterFunctionNotIsNull="No esta nulo" FilterFunctionStartsWith="Inicia con" GroupOperationAnd="y" GroupOperationNotAnd="y no" GroupOperationNotOr="o no" GroupOperationOr="o" />
                        </telerik:RadFilter>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
