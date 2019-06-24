<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAgregarPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAgregarPruebas1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function CloseWindow() {
            GetRadWindow().close();
        }

        function OpenSeleccionaPruebas() {

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: 900,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog("../Comunes/SeleccionPruebas.aspx?pIdBateria=" + '<%= vIdBateria %>', "winSeleccion", "Asignar pruebas", windowProperties);
        }


        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {
                    case "PRUEBAS":
                        InsertPrueba(EncapsularSeleccion(pDato[0].clTipoCatalogo, pDato));
                        break;
                }
            }
        }

        function ConfirmarEliminar(sender, args) {
            var MasterTable = $find("<%=grdPruebas.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            this.click();
                        }
                    });
                    radconfirm("Este proceso borrará la prueba seleccionada y sus posibles respuestas ¿Deseas continuar?", callBackFunction, 400, 180, null, "Aviso");
                    args.set_cancel(true);
                }
                else {
                    radalert("Selecciona una prueba", 400, 150, "Aviso");
                    args.set_cancel(true);
                }
        }


        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertPrueba(pDato) {
            var ajaxManager = $find('<%= RadAjaxManager1.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdPruebas" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
            </UpdatedControls>
        </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnEliminar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdPruebas" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManager>
        <div style="height: calc(100% - 60px);">
            <telerik:RadGrid
                ID="grdPruebas"
                ShowHeader="true"
                runat="server"
                AllowPaging="false"
                GridLines="None"
                Height="100%"
                Width="100%"
                HeaderStyle-Font-Bold="true"
                AllowMultiRowSelection="true"
                AllowFilteringByColumn="false"
                ClientSettings-EnablePostBackOnRowClick="false"
                OnNeedDataSource="grdPruebas_NeedDataSource">
                <ClientSettings AllowKeyboardNavigation="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <PagerStyle AlwaysVisible="true" />
                <MasterTableView ClientDataKeyNames="ID_PRUEBA_PLANTILLA" DataKeyNames="ID_PRUEBA_PLANTILLA, ID_PRUEBA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                    HorizontalAlign="NotSet" EditMode="EditForms">
                    <Columns>
                         <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35" HeaderText="Sel."></telerik:GridClientSelectColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre de la prueba" DataField="NB_PRUEBA" UniqueName="NB_PRUEBA" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="100" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div style="height: 10px; clear: both;"></div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnSeleccionar" runat="server" AutoPostBack="false" Text="Asignar" OnClientClicked="OpenSeleccionaPruebas"></telerik:RadButton>
        </div>
            <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server" AutoPostBack="true" Text="Eliminar" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="btnCancelar" runat="server" AutoPostBack="false" Text="Cancelar" OnClientClicked="CloseWindow"></telerik:RadButton>
        </div>
    <telerik:RadWindowManager ID="rwMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
