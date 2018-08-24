<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionMunicipio.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionLocalizacion.SeleccionMunicipio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function generateDataForParent() {
            var info = null;
            var vDatos = [];
            var masterTable = $find("<%= grdMunicipios.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    var vDato = {};
                    selectedItem = selectedItems[i];
                    vDato.clDato = selectedItem.getDataKeyValue("CL_MUNICIPIO");
                    vDato.nbDato = masterTable.getCellByColumnUniqueName(selectedItem, "NB_MUNICIPIO").innerHTML;
                    vDato.clTipoCatalogo = "MUNICIPIO"
                    vDatos.push(vDato);
                }
                sendDataToParent(vDatos);
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un municipio.", 400, 150);
            }
        }

        function cancelarSeleccion() {
            sendDataToParent(null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadGrid ID="grdMunicipios" runat="server" Height="485" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdMunicipios_NeedDataSource">
        <ClientSettings>
            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="true" />
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView ClientDataKeyNames="ID_MUNICIPIO,CL_MUNICIPIO" DataKeyNames="ID_MUNICIPIO,CL_MUNICIPIO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
            <Columns>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_MUNICIPIO" UniqueName="CL_MUNICIPIO"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Municipio" DataField="NB_MUNICIPIO" UniqueName="NB_MUNICIPIO"></telerik:GridBoundColumn>
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
