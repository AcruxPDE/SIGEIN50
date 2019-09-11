<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaOrganigramaPlazas.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaOrganigramaPlazas" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/cssOrgChart.css" />
    <script type="text/javascript" src="../Assets/js/appOrgChart.js"></script>
    <script type="text/javascript">

        function SelectPlazaOrigen(sender, args) {
            var item = args.get_item();
            if (item)
                ChangePlazaItem(item.get_value(), item.get_text());
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpOrganigrama" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramOrganigrama" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramOrganigrama">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rocPlazas" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lstAscendencia" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" LoadingPanelID="ralpOrganigrama" />
                </UpdatedControls> 
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkMostrarEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rocPlazas" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lstAscendencia" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbEmpresas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rocPlazas" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lstAscendencia" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
        <%--        <telerik:RadSplitter ID="RadSplitter2" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="RadPane2" runat="server">
                <div class="ctrlBasico">
                        <label name="lblIdPuesto">Generar organigrama a partir de la plaza:</label>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadListBox ID="lstPlaza" Width="300" runat="server" OnClientItemDoubleClicking="OpenPlazasSelectionWindow">
                                <Items>
                                    <telerik:RadListBoxItem Text="Seleccione" Value="" />
                                </Items>
                            </telerik:RadListBox>
                            <telerik:RadButton ID="btnBuscarPuesto" runat="server" Text="B" OnClientClicked="OpenPlazasSelectionWindow" AutoPostBack="false"></telerik:RadButton>
                            <telerik:RadButton ID="btnLimpiarPuesto" runat="server" Text="X" OnClientClicked="CleanPlazasSelection" AutoPostBack="false"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            Empresa:<telerik:RadComboBox ID="cmbEmpresas" Width="200" runat="server"></telerik:RadComboBox>
                        </div>
                    </div>--%>
        <div style="height: calc(100% - 50px); overflow: auto;">
            <div style="position: relative;">
                <span style="position: absolute; display: none;">
                    <label name="lblNbAscendencia">Ascendencia:</label><br />
                    <label name="lblMensaje" runat="server" id="lblMensaje" style="">Selecciona un elemento de la lista para mostrar el organigrama.</label>
                    <telerik:RadListBox ID="lstAscendencia" runat="server" OnClientItemDoubleClicked="SelectPlazaOrigen"></telerik:RadListBox>
                </span>
                <telerik:RadOrgChart ID="rocPlazas" runat="server" DisableDefaultImage="true" Height="100%" OnNodeDataBound="rocPlazas_NodeDataBound">
                    <RenderedFields>
                        <ItemFields>
                            <telerik:OrgChartRenderedField DataField="clItem" />
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
        <div style="clear: both; height: 10px;"></div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="btnExportar" runat="server" OnClientClicked="exportRadOrgChart" Text="Exportar a pdf" AutoPostBack="false" UseSubmitBehavior="false"></telerik:RadButton>
            <telerik:RadClientExportManager runat="server" ID="RadClientExportManager1"></telerik:RadClientExportManager>
        </div>
        <div class="divControlDerecha" style="padding-right: 10px;">
            <telerik:RadCheckBox ID="chkMostrarEmpleados" runat="server" Text="Mostrar empleados" OnClick="chkMostrarEmpleados_Click"></telerik:RadCheckBox>
        </div>
        <%--            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px" Height="50px">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" ExpandedPaneId="rspAyuda" Width="22px">
                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Title="Ayuda" Width="270px" RenderMode="Mobile" Height="100%">
                        <div style="display: block; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p>
                                Aquí podrás refinar tu búsqueda para la generación del organigrama.
                                <br />
                                En caso de ingresar criterios de búsqueda, éstos serán utilizados para acotar el diagrama. 
                               <br />
                                <br />
                                <b>Nota:</b> Las opciones de Área/Departamento y de Capos extra son mutualmente exclusivas, no pueden ser combinadas entre si.										
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
       </telerik:RadSplitter>--%>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup" OnClientShow="centerPopUp">
        <Windows>
            <telerik:RadWindow ID="winEmpleado" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close" NavigateUrl="~/Administracion/Empleado.aspx"></telerik:RadWindow>
            <telerik:RadWindow ID="winPlaza" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close" NavigateUrl="~/Administracion/VentanaPlaza.aspx"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            //<![CDATA[
            Sys.Application.add_load(function () {
                window.orgChart = $find("<%= rocPlazas.ClientID %>");
                window.winNodo = $find("<%= winPlaza.ClientID%>");
                window.winItem = $find("<%= winEmpleado.ClientID%>");
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
