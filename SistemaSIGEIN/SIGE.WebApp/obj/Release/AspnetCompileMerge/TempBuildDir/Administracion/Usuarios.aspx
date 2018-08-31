<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="SIGE.WebApp.Administracion.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ShowInsertForm() {
                OpenWindow(null);
                return false;
            }

            function ShowEditForm() {
                var selectedItem = $find("<%=grdUsuarios.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindow(selectedItem.getDataKeyValue("CL_USUARIO"));
                else
                    radalert("Selecciona un usuario.", 400, 150);
            }

            function OpenWindow(pClUsuario) {
                var vURL = "VentanaUsuarios.aspx";
                var vTitulo = "Agregar Usuario";
                if (pClUsuario != null) {
                    vURL = vURL + "?UsuarioId=" + pClUsuario;
                    vTitulo = "Editar Usuario";
                }
                openChildDialog(vURL, "winUsuarios", vTitulo);
            }

            function onCloseWindow(oWnd, args) {
                $find("<%= grdUsuarios.ClientID%>").get_masterTableView().rebind();
            }

            function confirmarEliminar(sender, args) {
                var masterTable = $find("<%= grdUsuarios.ClientID %>").get_masterTableView();
                var selectedItem = masterTable.get_selectedItems()[0];
                if (selectedItem != undefined) {
                    var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "CL_USUARIO").innerHTML;
                    confirmar(sender, args, "¿Deseas eliminar el usuario " + vNombre + "?, este proceso no podrá revertirse");
                }
                else {
                    radalert("Selecciona un usuario.", 400, 150);
                    args.set_cancel(true);
                }
            }

            function confirmar(sender, args, text) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });

                radconfirm(text, callBackFunction, 400, 150, null, "Confirmar");
                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                args.set_cancel(true);
            }

            function useDataFromChild(pEmpleados) {
                $find("<%= grdUsuarios.ClientID%>").get_masterTableView().rebind();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwmAlertas" />
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Usuarios</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdUsuarios" runat="server" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdUsuarios_NeedDataSource" Height="100%" OnItemDataBound="grdUsuarios_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="CL_USUARIO" DataKeyNames="CL_USUARIO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Usuario" DataField="CL_USUARIO" UniqueName="CL_USUARIO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre" DataField="NB_USUARIO" UniqueName="NB_USUARIO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Correo electrónico" DataField="NB_CORREO_ELECTRONICO" UniqueName="NB_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Área" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Rol" DataField="NB_ROL" UniqueName="NB_ROL"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="25" HeaderStyle-Width="95 " HeaderText="Activo" DataField="FG_ACTIVO" UniqueName="FG_ACTIVO">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
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
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="winSeleccionEmpleados" runat="server" Title="Seleccionar empleado" Height="600px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winUsuarios" runat="server" Title="Agregar/Editar Usuarios" Height="600px" Width="500px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
