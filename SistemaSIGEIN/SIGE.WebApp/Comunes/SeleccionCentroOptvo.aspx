<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionCentroOptvo.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionCentroOptvo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vCentros = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vCentros;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var clTipoCatalogo = "<%= vClCatalogo %>";
                var masterTable = $find("<%= grdCentrosOptvos.ClientID %>").get_masterTableView();
                var selectedItems = masterTable.get_selectedItems();
                if (selectedItems.length > 0) {
                    for (i = 0; i < selectedItems.length; i++) {
                        selectedItem = selectedItems[i];
                        var vGrupo = {
                            idCentro: selectedItem.getDataKeyValue("ID_CENTRO_OPTVO"),
                            clCentro: masterTable.getCellByColumnUniqueName(selectedItem, "CL_CENTRO_OPTVO").innerHTML,
                            nbCentro: masterTable.getCellByColumnUniqueName(selectedItem, "NB_CENTRO_OPTVO").innerHTML,
                            clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    vCentros.push(vGrupo);
                    var vLabel = document.getElementsByName('lblAgregar')[0];
                    vLabel.innerText = "Agregados: " + vCentros.length;
                }

                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un centro operativo.", 400, 150);
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
            <telerik:AjaxSetting AjaxControlID="grdCentrosOptvos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCentrosOptvos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdCentrosOptvos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdCentrosOptvos" />
                    <telerik:AjaxUpdatedControl ControlID="grdCentrosOptvos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 100%;">
        <telerik:RadSplitter ID="splCentrosOptvos" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnCentrosOptvos" runat="server" Height="100%">
                <div style="height: calc(100% - 54px);">
                    <telerik:RadGrid ID="grdCentrosOptvos"
                        HeaderStyle-Font-Bold="true"
                        runat="server"
                        Height="100%"
                        AllowMultiRowSelection="true"
                        EnableHeaderContextMenu="true"
                        AllowSorting="true"
                        AutoGenerateColumns="false"
                        OnItemDataBound="grdCentrosOptvos_ItemDataBound"
                        OnNeedDataSource="grdCentrosOptvos_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_CENTRO_OPTVO" EnableColumnsViewState="false" DataKeyNames="ID_CENTRO_OPTVO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Clave" DataField="CL_CENTRO_OPTVO" UniqueName="CL_CENTRO_OPTVO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="190" HeaderText="Nombre" DataField="NB_CENTRO_OPTVO" UniqueName="NB_CENTRO_OPTVO"></telerik:GridBoundColumn>
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
                            <telerik:RadFilter runat="server" ID="ftrGrdGruposOptvos" FilterContainerID="grdGruposOptvos" ShowApplyButton="true" ApplyButtonText="Filtrar">
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
    </div>
</asp:Content>
