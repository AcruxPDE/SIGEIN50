<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ContextHTML.master" CodeBehind="SeleccionCandidato.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionCandidato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vCandidatos = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vCandidatos;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdCandidatos.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vCandidato = {
                        idCandidato: selectedItem.getDataKeyValue("ID_CANDIDATO"),
                        nbCandidato: masterTable.getCellByColumnUniqueName(selectedItem, "NB_CANDIDATO_COMPLETO").innerHTML,
                        clsolicitud: selectedItem.getDataKeyValue("CL_SOLICITUD"),
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    if (!existeElemento(vCandidato)) {
                        vCandidatos.push(vCandidato);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = "Agregados: " + vCandidatos.length;
                    }
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un candidato.", 400, 150);
            }

            return false;
        }

        function existeElemento(pCandidato) {
            for (var i = 0; i < vCandidatos.length; i++) {
                var vValue = vCandidatos[i];
                if (vValue.idCandidato == pCandidato.idCandidato)
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
            <telerik:AjaxSetting AjaxControlID="grdCandidatos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdCandidatos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdCandidatos" />
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 100%;">
        <telerik:RadSplitter ID="splCandidatos" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridCandidatos" runat="server">
                <div style="height: calc(100% - 54px);">
                    <telerik:RadGrid ID="grdCandidatos" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnNeedDataSource="grdCandidatos_NeedDataSource" OnItemDataBound="grdCandidatos_ItemDataBound" AllowMultiRowSelection="true">
                        <GroupingSettings CaseSensitive="False" />
                        <ExportSettings FileName="Candidatos" ExportOnlyData="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView ClientDataKeyNames="ID_CANDIDATO,CL_SOLICITUD" EnableColumnsViewState="false" DataKeyNames="ID_CANDIDATO,CL_SOLICITUD" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" AutoGenerateColumns="false" PageSize="10">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_CANDIDATO_COMPLETO" UniqueName="NB_CANDIDATO_COMPLETO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave del empleado" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Status" DataField="CL_SOLICITUD_ESTATUS" UniqueName="CL_SOLICITUD_ESTATUS" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Requisición	Empresa" DataField="NO_REQUISICION" UniqueName="NO_REQUISICION" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Requisición	Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="170" FilterControlWidth="100"></telerik:GridBoundColumn>--%>
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
                            <telerik:RadFilter runat="server" ID="ftrGrdCandidatos" FilterContainerID="grdCandidatos" ShowApplyButton="true" Height="100">
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
