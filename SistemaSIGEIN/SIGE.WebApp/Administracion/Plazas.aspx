<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="Plazas.aspx.cs" Inherits="SIGE.WebApp.Administracion.Plazas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ShowInsertForm() {
                OpenWindow(GetPlazasWindowsPropierties(null));
                return false;
            }

            function ShowEditForm() {
                var selectedItem = $find("<%=grdPlazas.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindow(GetPlazasWindowsPropierties(selectedItem.getDataKeyValue("ID_PLAZA")));
                else
                    radalert("Selecciona una plaza.", 400, 150, "Aviso");
            }

            function GetPlazasWindowsPropierties(pIdPlaza) {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 700,
                    height: browserWnd.innerHeight - 40
                };
                if (pIdPlaza != null)
                {
                    windowProperties.vTitulo = "Editar plaza";
                    windowProperties.vURL = "VentanaPlaza.aspx?PlazaId=" + pIdPlaza;
                    windowProperties.vRadWindowId = "winPlazas";
                }
                else {
                    windowProperties.vTitulo = "Agregar plaza";
                    windowProperties.vURL = "VentanaPlaza.aspx";
                    windowProperties.vRadWindowId = "winPlazas";
                }
                return windowProperties;
            }

            function OpenWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
                //var vURL = "VentanaPlaza.aspx";
                //var vTitulo = "Agregar Plaza";
                //if (pIdRol != null) {
                //    vURL = vURL + "?PlazaId=" + pIdRol;
                //    vTitulo = "Editar Plaza";
                //}
                //var oWin = window.radopen(vURL, "winPlazas");
                //oWin.set_title(vTitulo);
            }

            function onCloseWindow(oWnd, args) {
                $find("<%= grdPlazas.ClientID%>").get_masterTableView().rebind();
            }

            function confirmarEliminar(sender, args) {
                var masterTable = $find("<%= grdPlazas.ClientID %>").get_masterTableView();
                var selectedItem = masterTable.get_selectedItems()[0];

                if (selectedItem != undefined) {
                    var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_PLAZA").innerHTML;
                    var vCL_EMPLEADO = masterTable.getCellByColumnUniqueName(selectedItem, "CL_EMPLEADO").innerHTML;

                    if (vCL_EMPLEADO == "&nbsp;") {
                        var vWindowsProperties = {
                            height: 200
                        };

                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                        radconfirm("¿Deseas eliminar la plaza " + vNombre + "?, este proceso no podrá revertirse", callBackFunction, 400, 170, null, "Aviso");
                        args.set_cancel(true);

                    }
                    else {
                        radalert("La plaza no puede ser eliminada porque tiene relación con el empleado " + vCL_EMPLEADO + ". ", 400, 150, "Aviso");
                        args.set_cancel(true);
                    }                    
                }
                else {
                    radalert("Selecciona un plaza.", 400, 150, "Aviso" );
                    args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Plazas</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdPlazas" runat="server" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
            OnNeedDataSource="grdPlazas_NeedDataSource" OnItemDataBound="grdPlazas_ItemDataBound">
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="Plazas" Excel-Format="Xlsx" IgnorePaging="true"></ExportSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView DataKeyNames="ID_PLAZA, CL_EMPLEADO" ClientDataKeyNames="ID_PLAZA, CL_EMPLEADO" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave plaza" DataField="CL_PLAZA" UniqueName="CL_PLAZA" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre plaza" DataField="NB_PLAZA" UniqueName="NB_PLAZA" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave puesto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área / departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave plaza jefe" DataField="CL_PLAZA_JEFE" UniqueName="CL_PLAZA_JEFE" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre plaza jefe" DataField="NB_PLAZA_JEFE" UniqueName="NB_PLAZA_JEFE" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave jefe" DataField="CL_EMPLEADO_JEFE" UniqueName="CL_EMPLEADO_JEFE" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre jefe" DataField="NB_EMPLEADO_JEFE_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO_JEFE" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave puesto jefe" DataField="CL_PUESTO_JEFE" UniqueName="CL_PUESTO_JEFE" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre puesto jefe" DataField="NB_PUESTO_JEFE" UniqueName="NB_PUESTO_JEFE" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Activo" DataField="CL_ACTIVO" UniqueName="CL_ACTIVO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnAgregar" runat="server" name="btnAgregar" AutoPostBack="false" Text="Agregar" OnClientClicked="ShowInsertForm" ></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" runat="server" name="btnEditar" AutoPostBack="false" Text="Editar" OnClientClicked="ShowEditForm" ></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" name="btnEliminar" AutoPostBack="true" Text="Eliminar" OnClientClicking="confirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <Windows>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar empleado" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winPlazas" runat="server" Title="Agregar/Editar Plaza" VisibleStatusbar="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
