<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="SIGE.WebApp.Administracion.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ShowInsertForm() {
                OpenWindow(null);
                return false;
            }

            function ShowEditForm() {
                var selectedItem = $find("<%=grdRoles.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindow(selectedItem.getDataKeyValue("ID_ROL"));
                else
                    radalert("Selecciona un rol.", 400, 150);
            }

            function OpenWindow(pIdRol) {
                var vURL = "VentanaRoles.aspx";
                var vTitulo = "Agregar Rol";
                if (pIdRol != null) {
                    vURL = vURL + "?RolId=" + pIdRol;
                    vTitulo = "Editar Rol";
                }

                OpenSelectionWindow(vURL, "winRoles", vTitulo);

            }

            function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 200,
                    height: browserWnd.innerHeight - 20
                };
                openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
            }


            function onCloseWindow(oWnd, args) {
                $find("<%= grdRoles.ClientID%>").get_masterTableView().rebind();
            }

            function confirmarEliminar(sender, args) {
                var masterTable = $find("<%= grdRoles.ClientID %>").get_masterTableView();
                var selectedItem = masterTable.get_selectedItems()[0];
                if (selectedItem != undefined) {
                    var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_ROL").innerHTML;

                    var vWindowsProperties = {
                        height: 200
                    };

                    confirmAction(sender, args, "¿Deseas eliminar el rol " + vNombre + "?, este proceso no podrá revertirse");
                }
                else {
                    radalert("Selecciona un rol.", 400, 150);
                    args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
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
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Roles del sistema</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdRoles" HeaderStyle-Font-Bold="true" runat="server" OnNeedDataSource="grdRoles_NeedDataSource" Height="100%" Width="80%" OnItemDataBound="grdRoles_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_ROL" DataKeyNames="ID_ROL" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave" DataField="CL_ROL" UniqueName="CL_ROL"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="180" FilterControlWidth="100" HeaderText="Nombre" DataField="NB_ROL" UniqueName="NB_ROL"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="25" HeaderStyle-Width="95 " HeaderText="Activo" DataField="FG_ACTIVO" UniqueName="FG_ACTIVO">
                            </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="70" DataType="System.DateTime"></telerik:GridBoundColumn>
                        <%--                    HASTA QUE TENGAMOS TIEMPO DE IMPLEMENTAR ESTE TIPO DE FILTRO   
                        <FilterTemplate>
                            <telerik:RadComboBox ID="APCombo" Width="65" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("FG_ACTIVO").CurrentFilterValue %>'
                                runat="server" OnClientSelectedIndexChanged="APComboIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Text="All" Value="" />
                                    <telerik:RadComboBoxItem Text="Sí" Value="Sí" />
                                    <telerik:RadComboBoxItem Text="No" Value="No" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                <script type="text/javascript">
                                    function APComboIndexChanged(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        tableView.filter("FG_ACTIVO", args.get_item().get_value(), "EqualTo");
                                }
                                </script>
                            </telerik:RadScriptBlock>
                        </FilterTemplate>--%>
                
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnAgregar" runat="server" name="btnAgregar" AutoPostBack="false" Text="Agregar" OnClientClicked="ShowInsertForm"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" runat="server" name="btnEditar" AutoPostBack="false" Text="Editar" OnClientClicked="ShowEditForm"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" name="btnEliminar" AutoPostBack="true" Text="Eliminar" OnClientClicking="confirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
    </div>
     <div style="clear: both;"></div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <Windows>
             <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar empleado" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winRoles" runat="server" Title="Agregar/Editar Rol" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
