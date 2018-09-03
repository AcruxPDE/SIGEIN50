<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionGrupo.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionGrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vGrupos = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vGrupos;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= rgGrupos.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vGrupo = {
                        ID_GRUPO: selectedItem.getDataKeyValue("ID_GRUPO"),
                        CL_GRUPO: masterTable.getCellByColumnUniqueName(selectedItem, "CL_GRUPO").innerHTML,
                        NB_GRUPO: masterTable.getCellByColumnUniqueName(selectedItem, "NB_GRUPO").innerHTML,
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    vGrupos.push(vGrupo);
                    var vLabel = document.getElementsByName('lblAgregar')[0];
                    vLabel.innerText = "Agregados: " + vGrupos.length;
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

        function cancelarSeleccion() {
            sendDataToParent(null);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramGrupos" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgGrupos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 55px);">
        <telerik:RadGrid
            ID="rgGrupos"
            runat="server"
            Width="100%"
            Height="100%"
            AllowPaging="true"
            AutoGenerateColumns="false"
            HeaderStyle-Font-Bold="true"
            EnableHeaderContextMenu="true"
            AllowMultiRowSelection="false"
            OnNeedDataSource="rgGrupos_NeedDataSource"
            OnItemDataBound="rgGrupos_ItemDataBound">
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView DataKeyNames="ID_GRUPO" ClientDataKeyNames="ID_GRUPO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn UniqueName="CL_GRUPO" DataField="CL_GRUPO" HeaderText="Clave" AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="120" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_GRUPO" DataField="NB_GRUPO" HeaderText="Grupo" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="CL_USUARIO_MODIFICA" DataField="CL_USUARIO_MODIFICA" HeaderText="Último usuario que modifica" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="50" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="FE_MODIFICACION" DataField="FE_MODIFICACION" HeaderText="Última fecha de modificación" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="50" CurrentFilterFunction="EqualTo" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
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
