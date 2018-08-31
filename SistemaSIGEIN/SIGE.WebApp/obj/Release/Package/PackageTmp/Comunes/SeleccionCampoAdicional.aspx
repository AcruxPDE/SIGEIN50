<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionCampoAdicional.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionCampoAdicional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
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
            var masterTable = $find("<%= grdCamposAdicionales.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vDato = {
                        idDato: selectedItem.getDataKeyValue("ID_CAMPO_FORMULARIO"),
                        clDato: selectedItem.getDataKeyValue("CL_CAMPO_FORMULARIO"),
                        nbDato: masterTable.getCellByColumnUniqueName(selectedItem, "NB_CAMPO_FORMULARIO").innerHTML,
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    vDatos.push(vDato);
                    var vLabel = document.getElementsByName('lblAgregar')[0];
                    vLabel.innerText = "Agregados: " + vDatos.length;
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un campo.", 400, 150);
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
        <telerik:RadGrid ID="grdCamposAdicionales" runat="server" Height="100%" AllowMultiRowSelection="true"
            OnNeedDataSource="grdCamposAdicionales_NeedDataSource" HeaderStyle-Font-Bold="true" OnItemDataBound="grdCamposAdicionales_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_CAMPO_FORMULARIO,CL_CAMPO_FORMULARIO,CL_FORMULARIO,FG_CLONABLE,FG_SISTEMA" DataKeyNames="ID_CAMPO_FORMULARIO,CL_CAMPO_FORMULARIO,CL_FORMULARIO,FG_CLONABLE,FG_SISTEMA" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                    <%--HASTA QUE TENGAMOS TIEMPO DE IMPLEMENTAR ESTE TIPO DE FILTRO--%>
                    <%--                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Módulo" DataField="CL_FORMULARIO" UniqueName="CL_FORMULARIO">
                        <FilterTemplate>
                            <telerik:RadComboBox ID="APCombo" Width="130" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("CL_FORMULARIO").CurrentFilterValue %>'
                                runat="server" OnClientSelectedIndexChanged="APComboIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Text="TODOS" Value="" />
                                    <telerik:RadComboBoxItem Text="SOLICITUD" Value="SOLICITUD" />
                                    <telerik:RadComboBoxItem Text="INVENTARIO" Value="INVENTARIO" />
                                    <telerik:RadComboBoxItem Text="DESCRIPTIVO" Value="DESCRIPTIVO" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                <script type="text/javascript">
                                    function APComboIndexChanged(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        tableView.filter("CL_FORMULARIO", args.get_item().get_value(), "EqualTo");
                                    }
                                </script>
                            </telerik:RadScriptBlock>
                        </FilterTemplate>
                    </telerik:GridBoundColumn>--%>
                    <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave" DataField="CL_CAMPO_FORMULARIO" UniqueName="CL_CAMPO_FORMULARIO"></telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre" DataField="NB_CAMPO_FORMULARIO" UniqueName="NB_CAMPO_FORMULARIO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="150" HeaderText="Tooltip" DataField="NB_TOOLTIP" UniqueName="NB_TOOLTIP"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="80" HeaderText="Tipo campo" DataField="NB_TIPO_CAMPO" UniqueName="NB_TIPO_CAMPO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="20" HeaderText="Activo" DataField="FG_ACTIVO_CL" UniqueName="FG_ACTIVO_CL"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="20" HeaderText="Sistema" DataField="FG_SISTEMA_CL" UniqueName="FG_SISTEMA_CL"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="20" HeaderText="Copiable" DataField="FG_CLONABLE_CL" UniqueName="FG_CLONABLE_CL"></telerik:GridBoundColumn>
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
            <telerik:RadButton ID="btnCancelar" runat="server" name="" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
        </div>
    </div>
</asp:Content>
