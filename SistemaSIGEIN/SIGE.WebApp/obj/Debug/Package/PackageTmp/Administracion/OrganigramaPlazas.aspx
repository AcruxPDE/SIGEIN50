<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="OrganigramaPlazas.aspx.cs" Inherits="SIGE.WebApp.Administracion.OrganigramaPlazas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Assets/css/cssOrgChart.css" />
    <script type="text/javascript" src="/Assets/js/appOrgChart.js"></script>
    <script type="text/javascript">
        function OpenPlazasSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPlaza.aspx", "winSeleccion", "Selección de plaza")
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
                vLstDato.idItem = vDatosSeleccionados.idPlaza;
                vLstDato.nbItem = vDatosSeleccionados.nbPlaza;
                ChangePlazaItem(vLstDato.idItem, vLstDato.nbItem);
            }
        }

        function SelectPlazaOrigen(sender, args) {
            var item = args.get_item();
            if (item)
                ChangePlazaItem(item.get_value(), item.get_text());
        }

        function CleanPlazasSelection(sender, args) {
            ChangePlazaItem("", "Todos");
        }

        function ChangePlazaItem(pIdItem, pNbItem) {
            var vListBox = $find("<%=lstPlaza.ClientID %>");
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
            ajaxManager.ajaxRequest("seleccionPlaza"); //Making ajax request with the argument        
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpOrganigrama" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramOrganigrama" runat="server" OnAjaxRequest="ramOrganigrama_AjaxRequest">
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
    <div>
        <div class="ctrlBasico">
            <label name="lblIdPuesto">A partir de la plaza:</label>
            <telerik:RadListBox ID="lstPlaza" Width="300" runat="server" OnClientItemDoubleClicking="OpenPlazasSelectionWindow">
                <Items>
                    <telerik:RadListBoxItem Text="Todos" Value="" />
                </Items>
            </telerik:RadListBox>
            <telerik:RadButton ID="btnBuscarPuesto" runat="server" Text="B" OnClientClicked="OpenPlazasSelectionWindow" AutoPostBack="false"></telerik:RadButton>
            <telerik:RadButton ID="btnLimpiarPuesto" runat="server" Text="X" OnClientClicked="CleanPlazasSelection" AutoPostBack="false"></telerik:RadButton>
        </div>
        <div class="ctrlBasico"></div>
        <div class="ctrlBasico">
            <label name="lblIdEmpresa">Empresa:</label><telerik:RadComboBox ID="cmbEmpresas" runat="server" OnSelectedIndexChanged="cmbEmpresas_SelectedIndexChanged"></telerik:RadComboBox>
        </div>
        <div class="ctrlBasico"></div>
        <div class="ctrlBasico">
            <telerik:RadCheckBox ID="chkMostrarEmpleados" runat="server" Text="Mostrar empleados" OnClick="chkMostrarEmpleados_Click"></telerik:RadCheckBox>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div style="height: calc(100% - 40px); overflow: auto;">
        <div style="position: relative;">
            <span style="position: absolute; display:none;">
                <label name="lblNbAscendencia">Ascendencia:</label><br />
                <label name="lblMensaje" runat="server" id="lblMensaje" style="">Selecciona un elemento de la lista para mostrar el organigrama.</label>
                <telerik:RadListBox ID="lstAscendencia" runat="server" OnClientItemDoubleClicked="SelectPlazaOrigen"></telerik:RadListBox>
            </span>
            <telerik:RadOrgChart ID="rocPlazas" runat="server" DisableDefaultImage="true" Height="100%" OnNodeDataBound="rocPlazas_NodeDataBound">
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
    </telerik:RadCodeBlock>
</asp:Content>
