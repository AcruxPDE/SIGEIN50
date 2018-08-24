<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionPlaza.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionPlaza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vPlazas = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vPlazas;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdPlazas.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vPlaza = {
                        idPlaza: selectedItem.getDataKeyValue("ID_PLAZA"),
                        //idPlaza: selectedItem.getDataKeyValue("ID_PLAZA_PDE"),
                        idPlazaSuperior: selectedItem.getDataKeyValue("ID_PLAZA_SUPERIOR"),
                        clPlaza: masterTable.getCellByColumnUniqueName(selectedItem, "CL_PLAZA").innerHTML,
                        clPlazaSuperior: masterTable.getCellByColumnUniqueName(selectedItem, "CL_PLAZA_JEFE").innerHTML,
                        nbPlaza: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PLAZA").innerHTML,
                        nbEmpleado: masterTable.getCellByColumnUniqueName(selectedItem, "NB_EMPLEADO_COMPLETO").innerHTML,
                        nbEmpleadoSuperior: masterTable.getCellByColumnUniqueName(selectedItem, "NB_EMPLEADO_COMPLETO_JEFE").innerHTML,
                        nbPuesto: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PUESTO").innerHTML,
                        nbPuestoSuperior: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PUESTO_JEFE").innerHTML,
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    vPlazas.push(vPlaza);
                    var vLabel = document.getElementsByName('lblAgregar')[0];
                    vLabel.innerText = "Agregados: " + vPlazas.length;
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona una plaza.", 400, 150);
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
        <telerik:RadGrid ID="grdPlazas" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdPlazas_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true" OnItemDataBound="grdPlazas_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_PLAZA, ID_PLAZA_SUPERIOR" ClientDataKeyNames="ID_PLAZA, ID_PLAZA_SUPERIOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave plaza" DataField="CL_PLAZA" UniqueName="CL_PLAZA" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre plaza" DataField="NB_PLAZA" UniqueName="NB_PLAZA" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre empleado" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave puesto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave plaza jefe" DataField="CL_PLAZA_JEFE" UniqueName="CL_PLAZA_JEFE" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre plaza jefe" DataField="NB_PLAZA_JEFE" UniqueName="NB_PLAZA_JEFE" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave jefe" DataField="CL_EMPLEADO_JEFE" UniqueName="CL_EMPLEADO_JEFE" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre jefe" DataField="NB_EMPLEADO_JEFE_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO_JEFE" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave puesto jefe" DataField="CL_PUESTO_JEFE" UniqueName="CL_PUESTO_JEFE" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre puesto jefe" DataField="NB_PUESTO_JEFE" UniqueName="NB_PUESTO_JEFE" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Activo" DataField="CL_ACTIVO" UniqueName="CL_ACTIVO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
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
