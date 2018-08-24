<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionJefeInmediato.aspx.cs" Inherits="SIGE.WebApp.Administracion.SeleccionJefeInmediato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        function generateDataForParent() {
            var info = null;
            var vPuestos = [];
            var masterTable = $find("<%= gridPuestos.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    var vPuesto = {};
                    selectedItem = selectedItems[i];
                    vPuesto.idPuesto = selectedItem.getDataKeyValue("ID_PUESTO");
                    vPuesto.clPuesto = masterTable.getCellByColumnUniqueName(selectedItem, "CL_PUESTO").innerHTML;
                    vPuesto.nbPuesto = masterTable.getCellByColumnUniqueName(selectedItem, "NB_PUESTO").innerHTML;
                    vPuestos.push(vPuesto);
                }
                sendDataToParent(vPuestos);
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un puesto.", 400, 150);
            }
        }

        function cancelarSeleccion() {
            sendDataToParent(null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 0px);">
        <div style="height: calc(100% - 55px);">
            <telerik:RadGrid ID="gridPuestos" runat="server" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true" Height="100%"
                OnNeedDataSource="gridPuestos_NeedDataSource" HeaderStyle-Font-Bold="true" OnItemDataBound="gridPuestos_ItemDataBound">
                <ClientSettings AllowKeyboardNavigation="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <GroupingSettings CaseSensitive="False" />
                <PagerStyle AlwaysVisible="true" />
                <%--
                --%>
                <MasterTableView ClientDataKeyNames="ID_PUESTO" DataKeyNames="ID_PUESTO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20">
                    <Columns>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre corto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Descripción" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div style="clear: both; height: 10px;"></div>
        <div class="divControlDerecha">
            <div class="ctrlBasico">
                <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Seleccionar" AutoPostBack="false" OnClientClicked="generateDataForParent"></telerik:RadButton>
            </div>
            <telerik:RadButton ID="btnCancelar" runat="server" name="" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
        </div>
    </div>
</asp:Content>


