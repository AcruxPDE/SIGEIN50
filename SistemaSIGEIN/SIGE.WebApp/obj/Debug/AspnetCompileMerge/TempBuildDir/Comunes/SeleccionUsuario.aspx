<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="SeleccionUsuario.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vUsuarios = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vUsuarios;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdUsuario.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vUsuario = {
                        idUsuario: selectedItem.getDataKeyValue("CL_USUARIO"),
                        nbUsuario: masterTable.getCellByColumnUniqueName(selectedItem, "NB_USUARIO").innerHTML,
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    if (!existeElemento(vUsuario)) {
                        vUsuarios.push(vUsuario);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = "Agregados: " + vUsuarios.length;
                    }
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un usuario.", 400, 150);
            }

            return false;
        }

        function existeElemento(pUsuario) {
            for (var i = 0; i < vUsuarios.length; i++) {
                var vValue = vUsuarios[i];
                if (vValue.idUsuario == pUsuario.idUsuario)
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
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdUsuario" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdUsuario_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdUsuario_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="CL_USUARIO,NB_USUARIO, FG_ACTIVO,FG_ENVIO" DataKeyNames="CL_USUARIO,NB_USUARIO, FG_ACTIVO,FG_ENVIO" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="90" HeaderText="Usuario" DataField="CL_USUARIO" UniqueName="CL_USUARIO">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="90" HeaderText="Nombre" DataField="NB_USUARIO" UniqueName="NB_USUARIO"></telerik:GridBoundColumn>
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
