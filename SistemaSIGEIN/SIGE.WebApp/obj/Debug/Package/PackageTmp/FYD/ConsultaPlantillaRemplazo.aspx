<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="ConsultaPlantillaRemplazo.aspx.cs" Inherits="SIGE.WebApp.FYD.ConsultaPlantillaRemplazo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Assets/js/jquery.min.js"></script>
    <script type="text/javascript">

        var vIdPuestoPlantilla = "";

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 30,
                height: document.documentElement.clientHeight - 20
            };
        }

        function OpenPuestoSelectionReportePlantillaWindow() {
            OpenSelectionWindow("/Comunes/SeleccionPuesto.aspx?m=FORMACION&CatalogoCl=PLANTILLA&vClTipoSeleccion=PUESTO_OBJETIVO", "winSeleccion", "Selección de puestos");
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = GetWindowProperties();

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function useDataFromChild(pPuestos) {
            if (pPuestos != null) {
                var vPuestoSeleccionado = pPuestos[0];

                if (vPuestoSeleccionado.clTipoCatalogo == "PLANTILLA") {
                    var list = $find("<%=rlbPuestoPlantillas.ClientID %>");
                    list.trackChanges();

                    var items = list.get_items();
                    items.clear();

                    var item = new Telerik.Web.UI.RadListBoxItem();
                    item.set_text(vPuestoSeleccionado.nbPuesto);
                    item.set_value(vPuestoSeleccionado.idPuesto);
                    vIdPuestoPlantilla = vPuestoSeleccionado.idPuesto;
                    items.add(item);

                    list.commitChanges();
                }
            }
        }

        function OpenPlantillasWindow() {

            var vListBox = $find('<%= rlbPuestoPlantillas.ClientID %>');
            var item = vListBox.get_items().getItem(0);
            var itemValue = item.get_value();
            var itemText = item.get_text();
            if (itemValue != "0") {
                if (vIdPuestoPlantilla != "") {
                    OpenWindow(GetPlantillasWindowProperties(vIdPuestoPlantilla));
                }
            }
            else {
                radalert("Selecciona el puesto a consultar.", 400, 150);
            }
        }

        function OpenWindow(pWindowProperties) {
            if (pWindowProperties)
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function GetPlantillasWindowProperties(pIdPuesto) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Reporte Plantillas de reemplazo";
            wnd.vRadWindowId = "winReporte";
            wnd.vURL = "VentanaReportePlantillasRemplazo.aspx?IdPuesto=" + pIdPuesto;

            return wnd;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Plantillas de Reemplazo</label>
    <div style="height: 10px; clear: both;"></div>
    <div style="height: calc(100% - 100px); width: 100%;">
        <telerik:RadSplitter ID="rsReportes" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpReportes" runat="server">
                <div style="clear: both; height: 15px;"></div>
                <div style="height: calc(100% - 600px);">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="width: 250px;">
                            <label>Puesto a consultar:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadListBox runat="server" ID="rlbPuestoPlantillas" Width="300px" AutoPostBack="false">
                                <Items>
                                    <telerik:RadListBoxItem Value="0" Text="No Seleccionado" />
                                </Items>
                            </telerik:RadListBox>
                        </div>
                    </div>
                    <telerik:RadButton runat="server" ID="btnBuscarPuestoPlantilla" Text="B"
                        AutoPostBack="false" OnClientClicked="OpenPuestoSelectionReportePlantillaWindow">
                    </telerik:RadButton>
                    <div style="clear: both; height: 10px;"></div>
                    <telerik:RadButton runat="server" ID="btnPlantillas" Text="Emitir" AutoPostBack="false" OnClientClicked="OpenPlantillasWindow"></telerik:RadButton>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px" Height="50px">
                <telerik:RadSlidingZone ID="rszAyuda" SlideDirection="Left" runat="server" ExpandedPaneId="rspAyuda" Width="22px">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="270px" RenderMode="Mobile" Height="100%">
                        <div id="divPlantilla" runat="server" style="display: block; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            Plantillas de remplazo
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <Windows>
            <telerik:RadWindow ID="winReporte" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winDetalle" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
