<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaListaComunicados.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaListaComunicados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function AbrirVentanaAgregar() {
                var vURL = "VentanaAgregarComunicado.aspx";
                var vTitulo = "Agregar comunicados ";
                var oWin = window.radopen(vURL, "winAgregarComunicado", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
                oWin.set_title(vTitulo);
                //openChildDialog("VentanaAgregarComunicado.aspx", "winAgregarComunicado", "Nuevo Comunicado")
            }


            function AbrirVentanaEditar() {
                var idComunicado = "";
                var vTipoComunicado = "";
                var vAccion = "";
                var grid = $find("<%=grdAdmcomunicados.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idComunicado = SelectDataItem.getDataKeyValue("ID_COMUNICADO");
                    vTipoComunicado = SelectDataItem.getDataKeyValue("TIPO_COMUNICADO");
                    vAccion = SelectDataItem.getDataKeyValue("TIPO_ACCION");
                    //var oWnd = radopen("VentanaAgregarComunicado.aspx?&IdComunicado=" + idComunicado + "&TIPO=Editar", "");
                    //oWnd.set_title("Editar Comunicado");
                    var vURL = "VentanaAgregarComunicado.aspx?&IdComunicado=" + idComunicado + "&TIPO=" +vAccion + "&TipoComunicado=" + vTipoComunicado;
                    var vTitulo = "Editar comunicados ";
                    var oWin = window.radopen(vURL, "winEditarComunicado", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
                    oWin.set_title(vTitulo);
                }
                else {
                    radalert("No has seleccionado un comunicado.", 400, 150, "");
                }
            }

            function confirmarEliminar2(sender, args) {
                var MasterTable = $find("<%=grdAdmcomunicados.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_COMUNICADO");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                            if (shouldSubmit) {
                                this.click();
                            }
                        });
                        radconfirm('¿Deseas eliminar el comunicado "' + CELL_NOMBRE.innerHTML + '"?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Selecciona un comunicado", 400, 150, "Error");
                    args.set_cancel(true);
                }
            }

            function onCloseWindow(oWnd, args) {
                $find("<%=grdAdmcomunicados.ClientID%>").get_masterTableView().rebind();

            }
            function OpenNotificacionWindow(pIdComunicado) {

                var vURL = "VentanaComunicadosLeidos.aspx?&IdComunicado=" + pIdComunicado;
                var vTitulo = "Empleados del comunicado";
                var oWin = window.radopen(vURL, "winEditarComunicado", document.documentElement.clientWidth - 500, document.documentElement.clientHeight - 10);
                oWin.set_title(vTitulo);
            }

            function OpenComentariosWindow(pIdComunicado, modo) {
                var vURL = "VentanaComentariosComunicado.aspx?&IdComunicado=" + pIdComunicado + "&modo=" + modo;
                var vTitulo = "Comentarios del comunicado";
                var oWin = window.radopen(vURL, "winComentarios", document.documentElement.clientWidth - 500, document.documentElement.clientHeight - 10);
                oWin.set_title(vTitulo);
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rbEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAdmcomunicados" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAdmcomunicados" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 20px);">
        <div style="margin-right: 30px; margin-left: 30px;">
            <div style="clear: both; height: 50px;"></div>
            <telerik:RadGrid ID="grdAdmcomunicados" ShowHeader="true" runat="server" AllowPaging="true"
                AllowSorting="true" Width="100%"
                OnNeedDataSource="grdAdmcomunicados_NeedDataSource">
                <ClientSettings EnablePostBackOnRowClick="false">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <PagerStyle AlwaysVisible="true" />
                <MasterTableView ClientDataKeyNames="ID_COMUNICADO, LEIDOS, TOTAL, TIPO_COMUNICADO,FG_PRIVADO, TIPO_ACCION" DataKeyNames="ID_COMUNICADO, FG_PRIVADO, TIPO_ACCION" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                    HorizontalAlign="NotSet" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true">
                    <Columns>
                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Creación" DataField="FE_COMUNICADO" UniqueName="FE_COMUNICADO" HeaderStyle-Width="100" FilterControlWidth="80"></telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Título" DataField="NB_COMUNICADO" UniqueName="NB_COMUNICADO" HeaderStyle-Width="220" FilterControlWidth="150"></telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Visible del:" DataField="FE_VISIBLE_DEL" UniqueName="FE_VISIBLE_DEL" HeaderStyle-Width="100" FilterControlWidth="80"></telerik:GridDateTimeColumn>
                        <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Visible al:" DataField="FE_VISIBLE_AL" UniqueName="FE_VISIBLE_AL" HeaderStyle-Width="100" FilterControlWidth="80"></telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Adjuntos" DataField="NB_ARCHIVO" UniqueName="NB_ARCHIVO" HeaderStyle-Width="90" FilterControlWidth="20"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Modo" DataField="FG_PRIVADO" UniqueName="FG_PRIVADO" HeaderStyle-Width="90" FilterControlWidth="20"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100" AllowFiltering="false">
                            <HeaderTemplate>Leído</HeaderTemplate>
                            <ItemTemplate>
                                <div style="float: left; width: 80%; text-align: center;">
                                    <label style="font-size: 14px; font-weight: lighter;"><%# Eval("LEIDOS") %> de  <%# Eval("TOTAL") %></label>
                                </div>
                                <div style="float: left; cursor: pointer;">
                                    <img src="../Assets/images/StatusPDE.png" onclick="OpenNotificacionWindow(<%# Eval("ID_COMUNICADO") %>)" />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100" AllowFiltering="false">
                            <HeaderTemplate>Comentarios</HeaderTemplate>
                            <ItemTemplate>
                                <div style="float: left; width: 60%; text-align: center;">
                                    <label style="font-size: 14px; font-weight: lighter;"><%# Eval("COMENTARIOS") %></label>
                                </div>
                                <div style="float: left; cursor: pointer;">
                                    <img src="../Assets/images/StatusPDE.png" onclick="OpenComentariosWindow(<%# Eval("ID_COMUNICADO") %>, '<%# Eval("FG_PRIVADO") %>');"/>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div style="clear: both; height: 20px;"></div>
            <div class="ctrlBasico">
                <telerik:RadButton ID="rbAgregar" runat="server" Text="Agregar" Width="120px" AutoPostBack="false" OnClientClicked="AbrirVentanaAgregar">
                </telerik:RadButton>
                <telerik:RadButton ID="rbEditar" runat="server" Text="Editar" Width="120px" AutoPostBack="false" OnClientClicked="AbrirVentanaEditar">
                </telerik:RadButton>
                <telerik:RadButton ID="rbEliminar" runat="server" Text="Eliminar" Width="120px" OnClientClicking="confirmarEliminar2" OnClick="rbEliminar_Click">
                </telerik:RadButton>
            </div>
        </div>
    </div>


    <telerik:RadWindowManager ID="rwmNuevo" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="winAgregarComunicado"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="close"
                Animation="Fade"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
        <Windows>
            <telerik:RadWindow
                ID="winEditarComunicado"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="close"
                Animation="Fade"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
        <Windows>
            <telerik:RadWindow
                ID="winComentarios"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Modal="true"
                Behaviors="close"
                Animation="Fade"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
        <Windows>
            <telerik:RadWindow
                ID="winSeleccion"
                runat="server"
                Title="Seleccionar"
                Width="1200px"
                Height="520px"
                ReloadOnShow="true"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Animation="Fade"
                OnClientClose="returnDataToParentPopup"
                Modal="true"
                Behaviors="Close,Reload">
            </telerik:RadWindow>
            <telerik:RadWindow
                ID="rwVerArchivos"
                runat="server"
                Width="600px"
                Height="600px"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" OnClientClose="onCloseWindow"></telerik:RadWindowManager>

</asp:Content>
