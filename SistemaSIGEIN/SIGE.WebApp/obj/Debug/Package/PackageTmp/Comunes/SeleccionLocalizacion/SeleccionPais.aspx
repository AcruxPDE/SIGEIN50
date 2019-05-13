<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionPais.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionLocalizacion.SeleccionPais" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function generateDataForParent() {
            var info = null;
            var vEmpleados = [];
            var masterTable = $find("<%= grdPaises.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    var vEmpleado = {};
                    selectedItem = selectedItems[i];
                    vEmpleado.idEmpleado = selectedItem.getDataKeyValue("M_EMPLEADO_ID_EMPLEADO");
                    vEmpleado.clEmpleado = masterTable.getCellByColumnUniqueName(selectedItem, "M_EMPLEADO_CL_EMPLEADO").innerHTML;
                    vEmpleado.nbCorreoElectronico = masterTable.getCellByColumnUniqueName(selectedItem, "M_EMPLEADO_CL_CORREO_ELECTRONICO").innerHTML;
                    vEmpleado.nbEmpleado = masterTable.getCellByColumnUniqueName(selectedItem, "M_EMPLEADO_NB_EMPLEADO_COMPLETO").innerHTML;
                    vEmpleados.push(vEmpleado);
                }
                sendDataToParent(vEmpleados);
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un empleado.", 400, 150);
            }
        }

        function cancelarSeleccion() {
            sendDataToParent(null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 54px);">
        <telerik:RadGrid ID="grdPaises" runat="server" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdPaises_NeedDataSource">
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_PAIS,CL_PAIS" DataKeyNames="ID_PAIS,CL_PAIS" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_PAIS" UniqueName="CL_PAIS"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="País" DataField="NB_PAIS" UniqueName="NB_PAIS"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
