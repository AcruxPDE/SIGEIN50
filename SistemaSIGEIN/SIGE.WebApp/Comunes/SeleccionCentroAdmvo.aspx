<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionCentroAdmvo.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionCentroAdmvo" %>
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
            var masterTable = $find("<%= grdCentrosAdmvo.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vGrupo = {
                        idCentro: selectedItem.getDataKeyValue("ID_CENTRO_ADMVO"),
                        clCentro: masterTable.getCellByColumnUniqueName(selectedItem, "CL_CENTRO_ADMVO").innerHTML,
                        nbCentro: masterTable.getCellByColumnUniqueName(selectedItem, "NB_CENTRO_ADMVO").innerHTML,
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    if (!existeElemento(vGrupo)) {
                        vCentros.push(vGrupo);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = "Agregados: " + vCentros.length;
                    }
                    }

                    return true;
                }
                else {
                    var currentWnd = GetRadWindow();
                    var browserWnd = window;
                    if (currentWnd)
                        browserWnd = currentWnd.BrowserWindow;
                    browserWnd.radalert("Selecciona un centro operativo.", 400, 150, "Aviso");
                }

                return false;
        }

        function existeElemento(pGrupo) {
            for (var i = 0; i < vCentros.length; i++) {
                var vValue = vCentros[i];
                if (vValue.idCentro == pGrupo.idCentro)
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
            <telerik:AjaxSetting AjaxControlID="grdCentrosAdmvo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCentrosAdmvo" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdCentrosAdmvo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdCentrosAdmvo" />
                    <telerik:AjaxUpdatedControl ControlID="grdCentrosAdmvo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 100%;">
        <telerik:RadSplitter ID="splCentrosAdmvo" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnCentrosAdmvo" runat="server" Height="100%">
                <div style="height: calc(100% - 54px);">
                    <telerik:RadGrid ID="grdCentrosAdmvo"
                        HeaderStyle-Font-Bold="true"
                        runat="server"
                        Height="100%"
                        AllowMultiRowSelection="true"
                        EnableHeaderContextMenu="true"
                        AllowSorting="true"
                        AutoGenerateColumns="false"
                        OnItemDataBound="grdCentrosAdmvo_ItemDataBound"
                        OnNeedDataSource="grdCentrosAdmvo_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_CENTRO_ADMVO" EnableColumnsViewState="false" DataKeyNames="ID_CENTRO_ADMVO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Clave" DataField="CL_CENTRO_ADMVO" UniqueName="CL_CENTRO_ADMVO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="190" HeaderText="Nombre" DataField="NB_CENTRO_ADMVO" UniqueName="NB_CENTRO_ADMVO"></telerik:GridBoundColumn>
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
                            <telerik:RadFilter runat="server" ID="ftrGrdGruposAdmvo" FilterContainerID="grdGruposAdmvo" ShowApplyButton="true" ApplyButtonText="Filtrar">
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
