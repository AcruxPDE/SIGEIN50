<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="OrganigramaArea.aspx.cs" Inherits="SIGE.WebApp.Administracion.OrganigramaArea" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/cssOrgChart.css" />
    <script type="text/javascript" src="../Assets/js/appOrgChart.js"></script>
    <script type="text/javascript">
        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx", "winSeleccion", "Selección de área/departamento")
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function useDataFromChild(pDato) {
            var vLstDato = {
                idItem: "",
                nbItem: ""
            };

            if (pDato != null) {
                console.info(pDato);
                var vDatosSeleccionados = pDato[0];
                vLstDato.idItem = vDatosSeleccionados.idArea;
                vLstDato.nbItem = vDatosSeleccionados.nbArea;
                ChangeAreaItem(vLstDato.idItem, vLstDato.nbItem);
            }
        }

        function SelectAreaOrigen(sender, args) {
            var item = args.get_item();
            if (item)
                ChangeAreaItem(item.get_value(), item.get_text());
        }

        function CleanAreaSelection(sender, args) {
            ChangeAreaItem("", "Todas");
        }

        function ChangeAreaItem(pIdItem, pNbItem) {
            var vListBox = $find("<%=lstArea.ClientID %>");
            vListBox.trackChanges();

            var items = vListBox.get_items();
            items.clear();

            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(pNbItem);
            item.set_value(pIdItem);
            items.add(item);
            item.set_selected(true);
            vListBox.commitChanges();

            var ajaxManager = $find("<%= ramOrganigrama.ClientID %>");
            ajaxManager.ajaxRequest("seleccionArea"); //Making ajax request with the argument        
        }
    </script>

    <!-- Load Pako ZLIB library to enable PDF compression -->
    <script src="../Assets/js/pako.min.js"></script>

    <style type="text/css">
        .k-pdf-export.RadOrgChart .rocGroup:before,
        .k-pdf-export.RadOrgChart.rocSimple .rocNode:after,
        .k-pdf-export.RadOrgChart .rocGroup:after,
        .k-pdf-export.RadOrgChart.rocSimple .rocItem:after,
        .k-pdf-export.RadOrgChart.rocSimple .rocItemTemplate:after {
            width: 1px !important;
        }

        .kendo-pdf-hide-pseudo-elements:after,
        .kendo-pdf-hide-pseudo-elements:before {
            display: none !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpOrganigrama" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramOrganigrama" runat="server" OnAjaxRequest="ramOrganigrama_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramOrganigrama">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rocAreas" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lstAscendencia" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" LoadingPanelID="ralpOrganigrama" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkMostrarPuestos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rocAreas" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lstAscendencia" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
     <div style="height: calc(100% - 10px);">
        <telerik:RadSplitter ID="RadSplitter2" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="RadPane2" runat="server">    
    <div>
        <div class="ctrlBasico">
            <label name="lblIdPuesto">A partir del área/departamento:</label>
            <telerik:RadListBox ID="lstArea" Width="300" runat="server" OnClientItemDoubleClicking="OpenAreaSelectionWindow">
                <Items>
                    <telerik:RadListBoxItem Text="Todas" Value="" />
                </Items>
            </telerik:RadListBox>
            <telerik:RadButton ID="btnBuscarArea" runat="server" Text="B" OnClientClicked="OpenAreaSelectionWindow" AutoPostBack="false"></telerik:RadButton>
            <telerik:RadButton ID="btnLimpiarArea" runat="server" Text="X" OnClientClicked="CleanAreaSelection" AutoPostBack="false"></telerik:RadButton>
        </div>
        <div class="ctrlBasico"></div>
        <div class="ctrlBasico">
            <telerik:RadCheckBox ID="chkMostrarPuestos" runat="server" Text="Mostrar puestos" OnClick="chkMostrarPuestos_Click"></telerik:RadCheckBox>
        </div>
        <div class="ctrlBasico"> 
                    <telerik:RadButton ID="btnExportar"  runat="server" OnClientClicked="exportRadOrgChart" Text="Exportar a pdf" AutoPostBack="false" UseSubmitBehavior="false"></telerik:RadButton>
                    <telerik:RadClientExportManager runat="server" ID="RadClientExportManager1">
                    </telerik:RadClientExportManager>
                    </div>
        <div style="clear: both;">
        </div>
    </div>
    <div style="height: calc(100% - 50px); overflow: auto;">
        <div style="position: relative;">
            <span style="position: absolute; display:none;">
                <label name="lblNbAscendencia">Ascendencia:</label><br />
                <label name="lblMensaje" runat="server" id="lblMensaje" style="">Selecciona un elemento de la lista para mostrar el organigrama.</label>
                <telerik:RadListBox ID="lstAscendencia" runat="server" OnClientItemDoubleClicked="SelectAreaOrigen"></telerik:RadListBox>
            </span>
            <telerik:RadOrgChart ID="rocAreas" runat="server" DisableDefaultImage="true" Height="100%" OnNodeDataBound="rocAreas_NodeDataBound">
                <RenderedFields>
                    <ItemFields>
                        <%--<telerik:OrgChartRenderedField DataField="clItem" />--%>
                        <telerik:OrgChartRenderedField DataField="nbItem" />
                    </ItemFields>
                </RenderedFields>
            </telerik:RadOrgChart>
            <telerik:RadContextMenu runat="server" ID="RadContextMenu1" OnClientItemClicked="onClientItemClicked">
                <Items>
                    <telerik:RadMenuItem Text="Editar" Value="Plaza" />
                    <telerik:RadMenuItem Text="Editar" Value="Empleado" />
                </Items>
            </telerik:RadContextMenu>
        </div>
    </div>
     </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup" OnClientShow="centerPopUp">
        <Windows>
            <telerik:RadWindow ID="winPuesto" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close" NavigateUrl="~/Administracion/VentanaDescriptivoPuesto.aspx"></telerik:RadWindow>
            <telerik:RadWindow ID="winArea" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close" NavigateUrl="~/Administracion/CatalogoAreas.aspx"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            //<![CDATA[
            Sys.Application.add_load(function () {
                window.orgChart = $find("<%= rocAreas.ClientID %>");
                window.winNodo = $find("<%= winArea.ClientID%>");
                window.winItem = $find("<%= winPuesto.ClientID%>");
                window.contextMenu = $find("<%= RadContextMenu1.ClientID %>");
                organigrama.initialize();
            });
            //]]>

        </script>
          <script>

              var $ = $telerik.$;

              function exportRadOrgChart() {
                  var v = $find('<%=RadClientExportManager1.ClientID%>');
                  v.exportPDF($(".RadOrgChart"));
              }

        </script>

    </telerik:RadCodeBlock>
</asp:Content>
