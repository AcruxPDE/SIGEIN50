<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionPuestoNominaDo.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionPuestoNominaDo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vPuestos = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vPuestos;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= rgPuestos.ClientID %>").get_masterTableView();
             var selectedItems = masterTable.get_selectedItems();
             if (selectedItems.length > 0) {
                 for (i = 0; i < selectedItems.length; i++) {
                     selectedItem = selectedItems[i];
                     var vPuesto = {
                         idPuesto: selectedItem.getDataKeyValue("ID_PUESTO"),
                         clPuesto: masterTable.getCellByColumnUniqueName(selectedItem, "CL_PUESTO").innerHTML,
                         nbPuesto: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PUESTO").innerHTML,
                         clTipoCatalogo: "<%= vClCatalogo %>"
<<<<<<< HEAD
                     };
                     if (!existeElemento(vPuesto)) {
                         vPuestos.push(vPuesto);
                         var vLabel = document.getElementsByName('lblAgregar')[0];
                         vLabel.innerText = "Agregados: " + vPuestos.length;
                     }
=======
                    };
                    vPuestos.push(vPuesto);
                    var vLabel = document.getElementsByName('lblAgregar')[0];
                    vLabel.innerText = "Agregados: " + vPuestos.length;
>>>>>>> DEV
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un evaluado.", 400, 150);
            }

            return false;
        }

<<<<<<< HEAD
        function existeElemento(pPuesto) {
            for (var i = 0; i < vPuestos.length; i++) {
                var vValue = vPuestos[i];
                if (vValue.idPuesto == pPuesto.idPuesto)
                    return true;
            }
            return false;
        }

=======
>>>>>>> DEV
        function cancelarSeleccion() {
            sendDataToParent(null);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramPuestos" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgPuestos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPuestos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 55px);">
        <telerik:RadGrid
            ID="rgPuestos"
            runat="server"
            Width="100%"
            Height="100%"
            AllowPaging="true"
            AutoGenerateColumns="false"
            HeaderStyle-Font-Bold="true"
            EnableHeaderContextMenu="true"
            AllowMultiRowSelection="false"
            OnNeedDataSource="rgPuestos_NeedDataSource"
            OnItemDataBound="rgPuestos_ItemDataBound">
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView DataKeyNames="ID_PUESTO" ClientDataKeyNames="ID_PUESTO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn UniqueName="CL_PUESTO" DataField="CL_PUESTO" HeaderText="Clave" AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="120" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="CL_USUARIO_MODIFICA" DataField="CL_USUARIO_MODIFICA" HeaderText="Último usuario que modifica" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="50" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="FE_MODIFICACION_PUESTO" DataField="FE_MODIFICACION_PUESTO" HeaderText="Última fecha de modificación" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="50" CurrentFilterFunction="EqualTo" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
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
    </div>
</asp:Content>
