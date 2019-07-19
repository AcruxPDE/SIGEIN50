<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="SelectorFactoresEval.aspx.cs" Inherits="SIGE.WebApp.Comunes.SelectorFactoresEval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vFactores = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vFactores;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdFactores.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vFactor = {
                        idFactor: selectedItem.getDataKeyValue("ID_FACTOR_EVALUACION"),
                        nbFactor: masterTable.getCellByColumnUniqueName(selectedItem, "NB_FACTOR_EVALUACION").innerHTML,

                        clTipoCatalogo: "FACTOR"
                    };
                    if (!existeElemento(vFactor)) {
                        vFactores.push(vFactor);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = "Agregados: " + vFactores.length;
                    }
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un factor.", 400, 150, "Aviso");
            }

            return false;
        }

        function existeElemento(pFactor) {
            for (var i = 0; i < vFactores.length; i++) {
                var vValue = vFactores[i];
                if (vValue.idFactor == pFactor.idFactor)
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
    <telerik:RadSplitter ID="splCompetencia" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpnGridFactores" runat="server">
            <div style="height: calc(100% - 54px);">
                <telerik:RadGrid ID="grdFactores" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" OnNeedDataSource="grdFactores_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnItemDataBound="grdFactores_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_FACTOR_EVALUACION" DataKeyNames="ID_FACTOR_EVALUACION" EnableColumnsViewState="false" AllowPaging="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="70" HeaderText="Factor" DataField="NB_FACTOR_EVALUACION" UniqueName="NB_FACTOR_EVALUACION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="190" HeaderText="Descripción" DataField="DS_FACTOR_EVALUACION" UniqueName="DS_FACTOR_EVALUACION"></telerik:GridBoundColumn>
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
        </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>

