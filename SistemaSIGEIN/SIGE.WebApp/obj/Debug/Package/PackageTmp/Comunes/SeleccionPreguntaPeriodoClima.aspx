<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionPreguntaPeriodoClima.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionPreguntaPeriodoClima" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vPreguntas = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vPreguntas;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdPreguntasClima.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];

                    var vPregunta = {
                        idPregunta: selectedItem.getDataKeyValue("ID_PREGUNTA"),
                        nbDimension: masterTable.getCellByColumnUniqueName(selectedItem, "NB_DIMENSION").innerHTML,
                        nbTema: masterTable.getCellByColumnUniqueName(selectedItem, "NB_TEMA").innerHTML,
                        nbPregunta: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PREGUNTAs").innerHTML,
                        clTipoCatalogo: "PREGUNTAS_CLIMA"
                    };
                    if (!existeElemento(vPregunta)) {
                        vPreguntas.push(vPregunta);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = "Agregados: " + vPreguntas.length;
                    }
                }

                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona una pregunta.", 400, 150);
            }

            return false;
        }

        function existeElemento(pPregunta) {
            for (var i = 0; i < vPreguntas.length; i++) {
                var vValue = vPreguntas[i];
                if (vValue.idPregunta == pPregunta.idPregunta)
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
            <telerik:AjaxSetting AjaxControlID="grdPreguntasClima">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPreguntasClima" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrPreguntasClima">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrPreguntasClima" />
                    <telerik:AjaxUpdatedControl ControlID="grdPreguntasClima" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="splPreguntasClima" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpnPreguntasClima" runat="server">
            <div style="height: calc(100% - 54px);">
                <telerik:RadGrid ID="grdPreguntasClima" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdPreguntasClima_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdPreguntasClima_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_PERIODO,NB_TEMA" DataKeyNames="ID_PERIODO , NB_TEMA" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="60" HeaderText="Dimensión" DataField="NB_DIMENSION" UniqueName="NB_DIMENSION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="60" HeaderText="Tema" DataField="NB_TEMA" UniqueName="NB_TEMA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="60" HeaderText="Pregunta" DataField="NB_PREGUNTA" UniqueName="NB_PREGUNTA"></telerik:GridBoundColumn>

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
                        <telerik:RadFilter runat="server" ID="ftrPreguntasClima" FilterContainerID="grdPreguntasClima" ShowApplyButton="true" Height="100">
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
