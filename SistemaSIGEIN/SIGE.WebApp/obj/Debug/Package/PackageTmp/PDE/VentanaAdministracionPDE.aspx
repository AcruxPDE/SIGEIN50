<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaAdministracionPDE.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaAdministracionPDE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        function cancelarSeleccion() {
            sendDataToParent(null);
        }
        function closeWindow() {

            GetRadWindow().close();

        }


        function ShowInsertForm() {
            OpenWindow(null);
            return false;
        }

        function ShowEditForm() {
            var selectedItem = $find("<%=grdRoles.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined)
                OpenWindow(selectedItem.getDataKeyValue("ID_ROL"));
            else
                radalert("Selecciona un rol.", 400, 150, "Aviso");
        }

        function OpenWindow(pIdRol) {
            var vURL = "VentanaRoles_PDE.aspx";
            var vTitulo = "Agregar Rol";
            if (pIdRol != null) {
                vURL = vURL + "?RolId=" + pIdRol;
                vTitulo = "Editar Rol";
            }
            var oWin = window.radopen(vURL, "winRoles");
            oWin.set_title(vTitulo);
        }

        function onCloseWindow(oWnd, args) {
            $find("<%= grdRoles.ClientID%>").get_masterTableView().rebind();
            $find("<%=grdEmpleadosSeleccionados.ClientID%>").get_masterTableView().rebind();
            GetRadWindow().close();
        }

        function confirmarEliminar(sender, args) {
            var masterTable = $find("<%= grdRoles.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_ROL").innerHTML;
                
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                radconfirm("¿Deseas eliminar el rol " + vNombre + "?, este proceso no podrá revertirse", callBackFunction, 400, 200, null, "Aviso");
                args.set_cancel(true);

            }
            else {
                radalert("Selecciona un rol.", 400, 150, "Aviso");
                args.set_cancel(true);
            }
        }


        function AbrirVentanaSeleccionPuesto() {
            openChildDialog("../Comunes/SeleccionPuesto.aspx?mulSel=1", "winSeleccion", "Selección por puestos")
        }
        function AbrirVentanaSeleccionUsuarios() {

            openChildDialog("../Comunes/SeleccionUsuario.aspx", "winSeleccion", "Selección de usuarios")
        }
        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {

                    case "PUESTO":
                        InsertarEmpleado(EncapsularSeleccion("PUESTO", pDato));
                        break;

                    case "USUARIO":
                        InsertarEmpleado(EncapsularSeleccion("USUARIO", pDato));
                        break;
                }
            }
        }

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertarEmpleado(pDato) {
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
                    <telerik:AjaxUpdatedControl ControlID="grdPuesto" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdPuesto" />
                    <telerik:AjaxUpdatedControl ControlID="grdPuesto" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPuesto" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRoles" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwmAlertas" />
                    <telerik:AjaxUpdatedControl ControlID="grdRoles" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>

    </telerik:RadAjaxManager>

    <!---->
    <div style="height: calc(100% - 80px);">
        <telerik:RadTabStrip ID="rtsFormatosDescargables" runat="server" MultiPageID="mpFormatosTramites">
            <Tabs>
                <telerik:RadTab TabIndex="0" Text="Configuración de roles"></telerik:RadTab>
                <telerik:RadTab TabIndex="1" Text="Configuración de notificaciones"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: 95%">
            <telerik:RadMultiPage ID="mpFormatosTramites" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="rpvRoles" runat="server" Width="100%" Height="90%">

                    <label class="labelTitulo">Roles del sistema</label>
                    <div style="height: calc(100% - 200px); max-width: 580px;">
                        <telerik:RadGrid ID="grdRoles" runat="server" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdRoles_NeedDataSource" Height="400px">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView ClientDataKeyNames="ID_ROL" DataKeyNames="ID_ROL" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave" DataField="CL_ROL" UniqueName="CL_ROL"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="150" HeaderText="Nombre" DataField="NB_ROL" UniqueName="NB_ROL"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="25" HeaderStyle-Width="95 " HeaderText="Activo" DataField="FG_ACTIVO" UniqueName="FG_ACTIVO">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnAgregar" runat="server" name="btnAgregar" AutoPostBack="false" Text="Agregar" OnClientClicked="ShowInsertForm" Width="100"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnEditar" runat="server" name="btnEditar" AutoPostBack="false" Text="Editar" OnClientClicked="ShowEditForm" Width="100"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnEliminar" runat="server" name="btnEliminar" AutoPostBack="true" Text="Eliminar" OnClientClicking="confirmarEliminar" Width="100" OnClick="btnEliminar_Click"></telerik:RadButton>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvNotificaciones" runat="server" Width="100%" Height="100%">
                    <div style="clear: both; height: 10px;"></div>
                    <div style="height: calc(100% - 70px);">
                        <telerik:RadGrid ID="grdEmpleadosSeleccionados"
                            runat="server"
                            Height="400"
                            Width="100%"
                            AllowSorting="true"
                            ShowHeader="true"
                            AllowMultiRowSelection="true"
                            HeaderStyle-Font-Bold="true"
                            OnNeedDataSource="grdEmpleadosSeleccionados_NeedDataSource"
                            OnItemCommand="grdEmpleadosSeleccionados_ItemCommand">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView DataKeyNames="M_CL_USUARIO,ID_EMPLEADO,ID_PUESTO,NB_PUESTO" ClientDataKeyNames="M_CL_USUARIO,ID_EMPLEADO,ID_PUESTO, NB_PUESTO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="250" HeaderText="NOMBRE" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="250" HeaderText="PUESTO" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn UniqueName="BTNELIMINAR" Text="Eliminar" CommandName="Delete" HeaderStyle-Width="100">
                                        <ItemStyle Width="10%" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div style="clear: both; height: 10px;"></div>

                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnSeleccionarPuestos" runat="server" name="btnSeleccionarPuestos" AutoPostBack="false" Text="Seleccionar por puesto" Width="200" OnClientClicked="AbrirVentanaSeleccionPuesto"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnSeleccionUsuario" runat="server" name="btnSeleccionUsuario" AutoPostBack="false" Text="Seleccionar por usuario externo" Width="220" OnClientClicked="AbrirVentanaSeleccionUsuarios"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Guardar" OnClick="btnSeleccion_Click"></telerik:RadButton>
                    </div>

                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>

    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <Windows>
            <telerik:RadWindow ID="winRoles" runat="server" Title="Agregar/Editar Rol" Height="500px" Width="900px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
