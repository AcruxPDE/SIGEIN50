<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SelectorRequisiciones.aspx.cs" Inherits="SIGE.WebApp.Comunes.SelectorRequisiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vRequisiciones = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vRequisiciones;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdRequisiciones.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vRequisicion = {
                        idRequisicion: selectedItem.getDataKeyValue("ID_REQUISICION"),
                        nbRequicision: masterTable.getCellByColumnUniqueName(selectedItem, "NO_REQUISICION").innerHTML,
                        idPuesto: selectedItem.getDataKeyValue("ID_PUESTO"),
                        nbPuesto: masterTable.getCellByColumnUniqueName(selectedItem, "NB_PUESTO").innerHTML,
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
<<<<<<< HEAD
                    if (!existeElemento(vRequisicion)) {
                        vRequisiciones.push(vRequisicion);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = "Agregados: " + vRequisiciones.length;
                    }
=======
                    vRequisiciones.push(vRequisicion);
                    var vLabel = document.getElementsByName('lblAgregar')[0];
                    vLabel.innerText = "Agregados: " + vRequisiciones.length;
>>>>>>> DEV
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona una requisición.", 400, 150);
            }

            return false;
        }

<<<<<<< HEAD
        function existeElemento(pRequisicion) {
            for (var i = 0; i < vRequisiciones.length; i++) {
                var vValue = vRequisiciones[i];
                if (vValue.idRequisicion == pRequisicion.idRequisicion)
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
    <div style="height: calc(100% - 55px);">
        <telerik:RadGrid ID="grdRequisiciones" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AutoGenerateColumns="false" AllowSorting="true"
            OnNeedDataSource="grdRequisiciones_NeedDataSource" OnItemDataBound="grdRequisiciones_ItemDataBound" AllowMultiRowSelection="false">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_REQUISICION, ID_AUTORIZA, CL_ESTATUS_PUESTO, NB_PUESTO, FL_REQUISICION, CL_TOKEN, NO_REQUISICION, CL_ESTATUS_REQUISICION, CL_CAUSA, ID_PUESTO" DataKeyNames="ID_REQUISICION, ID_AUTORIZA, CL_ESTATUS_PUESTO, NO_REQUISICION, NB_PUESTO, FL_REQUISICION, CL_TOKEN, CL_ESTATUS_REQUISICION, CL_CAUSA, ID_PUESTO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de requisición" DataField="NO_REQUISICION" UniqueName="NO_REQUISICION" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Causa" DataField="NB_CAUSA" UniqueName="NB_CAUSA" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:d}"></telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave del puesto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTATUS_REQUISICION" UniqueName="CL_ESTATUS_REQUISICION" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <%-- <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de autorización" DataField="FE_AUTORIZA_REQUISICION" UniqueName="FE_AUTORIZA_REQUISICION" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div class="divControlDerecha" style="padding-top: 10px;">
        <div class="ctrlBasico">
            <label runat="server" id="lblAgregar" name="lblAgregar" style="padding-top: 10px; font-weight: lighter;"></label>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnAgregar" runat="server" name="btnAgregar" Text="Agregar" AutoPostBack="false" OnClientClicked="addSelection" Visible="false"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Seleccionar" AutoPostBack="false" OnClientClicked="generateDataForParent"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" name="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
        </div>
    </div>
</asp:Content>
