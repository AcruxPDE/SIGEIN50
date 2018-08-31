<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="SeleccionAdscripciones.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionAdscripciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vAdscripciones = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vAdscripciones;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdAdscripcion.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vAdscripcion = {
                        idCampo: selectedItem.getDataKeyValue("CL_CAMPO_CATALOGO"),
                        nbValor: ("[" + masterTable.getCellByColumnUniqueName(selectedItem, "CL_CAMPO_CATALOGO").innerHTML + "]" + masterTable.getCellByColumnUniqueName(selectedItem, "NB_CATALOGO_VALOR").innerHTML),
                        clTipoCatalogo: "<%= vClCatalogo %>",
                        clLista: "<%= vClLista %>"
                    };
                    vAdscripciones.push(vAdscripcion);
                    var vLabel = document.getElementsByName('lblAgregar')[0];
                    vLabel.innerText = "Agregados: " + vAdscripciones.length;
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona una adscripción.", 400, 150);
            }
            return false;
        }

        function cancelarSeleccion() {
            sendDataToParent(null);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdAdscripcion" HeaderStyle-Font-Bold="true" runat="server" AllowMultiRowSelection="true" OnNeedDataSource="grdAdscripcion_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdAdscripcion_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="CL_CAMPO_CATALOGO" DataKeyNames="CL_CAMPO_CATALOGO" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" PageSize="50">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="80" HeaderText="Clave" DataField="CL_CAMPO_CATALOGO" UniqueName="CL_CAMPO_CATALOGO">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Nombre de campo" DataField="NB_CAMPO" UniqueName="NB_CAMPO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Valor de campo" DataField="NB_CATALOGO_VALOR" UniqueName="NB_CATALOGO_VALOR"></telerik:GridBoundColumn>
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
            <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Seleccionar" OnClientClicked="generateDataForParent"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" name="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
        </div>
    </div>
</asp:Content>
