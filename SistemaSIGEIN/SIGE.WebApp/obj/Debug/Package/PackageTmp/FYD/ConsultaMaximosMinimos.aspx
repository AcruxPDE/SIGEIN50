<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="ConsultaMaximosMinimos.aspx.cs" Inherits="SIGE.WebApp.FYD.ConsultaMaximosMinimos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Assets/js/jquery.min.js"></script>
    <script type="text/javascript">

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 30,
                height: document.documentElement.clientHeight - 20
            };
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = GetWindowProperties();

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }



        function OpenPuestoSelectionReporteMaximoMinimoWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPuesto.aspx?CatalogoCl=PLANTILLA&vClTipoSeleccion=PUESTO_OBJETIVO", "winSeleccion", "Selección de puestos");
        }

        function useDataFromChild(pPuestos) {
            if (pPuestos != null) {
                var vPuestoSeleccionado = pPuestos[0];

                if (vPuestoSeleccionado.clTipoCatalogo == "PLANTILLA") {
                    var list = $find("<%=rlbPuesto.ClientID %>");
                    list.trackChanges();

                    var items = list.get_items();
                    items.clear();

                    var item = new Telerik.Web.UI.RadListBoxItem();
                    item.set_text(vPuestoSeleccionado.nbPuesto);
                    item.set_value(vPuestoSeleccionado.idPuesto);

                    items.add(item);

                    list.commitChanges();
                }
            }
        }

        function OpenMaximosMinimosWindow(pIdReporte) {
            OpenWindow(GetMaximosMinimosWindowProperties(pIdReporte));
        }

        function OpenWindow(pWindowProperties) {
            if (pWindowProperties)
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function GetMaximosMinimosWindowProperties(pIdReporte) {
            var vListBox = $find('<%= rlbPuesto.ClientID %>');
         var item = vListBox.get_items().getItem(0);
         var itemValue = item.get_value();
         var itemText = item.get_text();
         if (itemValue != "0") {
             var wnd = GetWindowProperties();
             wnd.vTitulo = "Reporte Máximos y mínimos";
             wnd.vRadWindowId = "winReporte";
             wnd.vURL = "VentanaReporteMaximosMinimos.aspx?IdReporte=" + pIdReporte;

             return wnd;
         }
         else {
             radalert("Selecciona el puesto objetivo.", 400, 150);
         }
     }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpConsultas" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConsultas" runat="server" DefaultLoadingPanelID="ralpConsultas">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnMaximosMinimos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnMaximosMinimos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Reporte de Máximos y Mínimos</label>
    <div style="height: 10px; clear: both;"></div>
    <div style="height: calc(100% - 100px); width: 100%;">
        <telerik:RadSplitter ID="rsReportes" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpReportes" runat="server" Height="80%">
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="width: 250px;">
                        <label>Puesto objetivo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox runat="server" ID="rlbPuesto" Width="300px" AutoPostBack="false">
                            <Items>
                                <telerik:RadListBoxItem Value="0" Text="No Seleccionado" />
                            </Items>
                        </telerik:RadListBox>
                        <telerik:RadButton runat="server" ID="btnBuscar" Text="B" AutoPostBack="false"
                            OnClientClicked="OpenPuestoSelectionReporteMaximoMinimoWindow">
                        </telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="width: 250px;">
                        <label>Días de curso:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtDiasCurso" Width="100px" Text="15"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="width: 250px;">
                        <label>Rotación promedio mensual (RPM):</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtRotacion" Width="100px" Text="50"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="width: 250px;">
                        <label>Porcentaje de no aprobados (PC):</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtPorcentaje" Width="100px" Text="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="width: 250px;">
                        <label>Punto de reorden:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtReorden" Width="100px" Text="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <telerik:RadButton runat="server" ID="btnMaximosMinimos" Text="Emitir" OnClick="btnMaximosMinimos_Click"></telerik:RadButton>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px" Height="50px">
                <telerik:RadSlidingZone ID="rszAyuda" SlideDirection="Left" runat="server" ExpandedPaneId="rspAyuda" Width="22px" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="270px" RenderMode="Mobile" Height="100%">
                        <div id="divMaximo" runat="server" style="display: block; padding-left: 10px; padding-right: 10px; padding-top: 20px; text-align: justify;">
                            <p>
                                El reporte de máximos y mínimos permite establecer control sobre los inventarios de personas capacitadas y listas para desempeñar un puesto vacante y diferente al que hoy desempeñan. Este reporte debe de usarse para puesto cruciales para la organización.
                                 Selecciona el puesto que vas a controlar y especifica los datos que se solicitan a continuación. La validez, confiabilidad y actualización de estos datos será lo que te permitirá tener un reporte fidedigno y confiable.
                                																
                            </p>
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
