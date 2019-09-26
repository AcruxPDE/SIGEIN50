<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionInstructor.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionInstructor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vDatos = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vDatos;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdInstructores.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vDato = {
                        idInstructor: selectedItem.getDataKeyValue("ID_INSTRUCTOR"),
                        clInstructor: masterTable.getCellByColumnUniqueName(selectedItem, "CL_INTRUCTOR").innerHTML,
                        nbInstructor: masterTable.getCellByColumnUniqueName(selectedItem, "NB_INSTRUCTOR").innerHTML,
                        clTipoCatalogo: "INSTRUCTOR"
                    };
                    if (!existeElemento(vDato)) {
                        vDatos.push(vDato);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = "Agregados: " + vDatos.length;
                    }
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un instructor.", 400, 150, "Aviso");
            }

            return false;
        }

        function existeElemento(pDato) {
            for (var i = 0; i < vDatos.length; i++) {
                var vValue = vDatos[i];
                if (vValue.idInstructor == pDato.idInstructor)
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
    <div style="height: calc(100% - 54px);">
        <telerik:RadGrid ID="grdInstructores" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdInstructores_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdInstructores_ItemDataBound">
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_INSTRUCTOR" DataKeyNames="ID_INSTRUCTOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_INTRUCTOR" UniqueName="CL_INTRUCTOR"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="320" HeaderText="Nombre" DataField="NB_NOMBRE_INSTRUCTOR" UniqueName="NB_NOMBRE_INSTRUCTOR"></telerik:GridBoundColumn>
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
</asp:Content>
