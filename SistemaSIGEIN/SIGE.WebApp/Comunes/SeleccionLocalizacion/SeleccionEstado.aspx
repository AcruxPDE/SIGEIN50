<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionEstado.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionLocalizacion.SeleccionEstado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function generateDataForParent() {
            var info = null;
            var vDatos = [];
            var masterTable = $find("<%= grdEstados.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    var vDato = {};
                    selectedItem = selectedItems[i];
                    vDato.idDato = selectedItem.getDataKeyValue("ID_ESTADO");
                    vDato.clDato = selectedItem.getDataKeyValue("CL_ESTADO");
                    vDato.nbDato = masterTable.getCellByColumnUniqueName(selectedItem, "NB_ESTADO").innerHTML;
                    vDato.clTipoCatalogo = "ESTADO"
                    vDatos.push(vDato);
                }
                sendDataToParent(vDatos);
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un estado.", 400, 150);
            }
        }

        function cancelarSeleccion() {
            sendDataToParent(null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadGrid ID="grdEstados" runat="server" HeaderStyle-Font-Bold="true" Height="485" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdEstados_NeedDataSource">
        <ClientSettings>
            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="true" />
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView ClientDataKeyNames="ID_ESTADO,CL_ESTADO" DataKeyNames="ID_ESTADO,CL_ESTADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
            <Columns>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_ESTADO" UniqueName="CL_ESTADO"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estado" DataField="NB_ESTADO" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <div class="divControlDerecha" style="padding-top: 10px;">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Seleccionar" AutoPostBack="false" OnClientClicked="generateDataForParent"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" name="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
        </div>
    </div>
</asp:Content>
