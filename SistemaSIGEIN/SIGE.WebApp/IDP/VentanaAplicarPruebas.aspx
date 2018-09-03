<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAplicarPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAplicarPruebas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGenerar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenidoPruebas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="radCmbNiveles" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="radCmbPuesto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>         
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            function OpenMensaje() {
                 var arrUrl = window.location.href.split('/');
                //var vUrl = '<= ResolveClientUrl("~/IDP/Pruebas/AplicacionBateriasMasiva.aspx")%>';
                var vUrl = arrUrl[0] + '//' + arrUrl[2] + "/SIGEIN50/IDP/Pruebas/AplicacionBateriasMasiva.aspx";
                radalert("Copia y pega la siguiente URL en un navegador: <br><br>" + vUrl + "<br><br> No olvides que necesitaras el folio de solicitud para ingresar.", 650, 200, "Liga aplicación masiva");
            }


            function confirmarEliminar2(sender, args) {
                var MasterTable = $find("<%=grdCandidatos.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row == null) {
                    radalert("Selecciona a un candidato", 400, 150, "Warning");
                    args.set_cancel(true);
                }
            }

            function closeWindow() {
                GetRadWindow().close();
            }


            function OpenEnviarCorreos() {
                var windowProperties = {
                    width: document.documentElement.clientWidth - 300,
                    height: document.documentElement.clientHeight - 20
                };
                openChildDialog("EnvioCorreosPruebas.aspx?pIdCandidatosPruebas=" + '<%= vIdCandidatosPruebas %>', "winSeleccionCandidato", "Envío de pruebas", windowProperties);
            }

            function OpenAplicarPruebasInterna() {
                var MasterTable = $find("<%=grdCandidatos.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_dataItems();
                var row = selectedRows[0];
                if (row != null) {
                    var pFlBateria = row.getDataKeyValue("ID_BATERIA");
                    var pClToken = row.getDataKeyValue("CL_TOKEN");
                    var pIdCandidato = row.getDataKeyValue("ID_CANDIDATO");

                    window.parent.location = "Pruebas/PruebaBienvenida.aspx?ID=" + pFlBateria + "&T=" + pClToken + "&idCandidato=" + pIdCandidato;
                }
                else
                    radalert("Selecciona a un candidato", 400, 150, "Warning");
            }

        </script>
    </telerik:RadCodeBlock>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdCandidatos" ShowHeader="true" runat="server" AllowPaging="false"
            Width="100%" GridLines="None" Height="100%" HeaderStyle-Font-Bold="true"
            AllowMultiRowSelection="true"
            AllowFilteringByColumn="false" OnItemCommand="grdCandidatos_ItemCommand"
            ClientSettings-EnablePostBackOnRowClick="false" OnNeedDataSource="grdCandidatos_NeedDataSource">
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_CANDIDATO, CL_SOLICITUD,ID_BATERIA,FL_BATERIA, CL_TOKEN" DataKeyNames="ID_CANDIDATO, CL_SOLICITUD,ID_BATERIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                HorizontalAlign="NotSet" EditMode="EditForms">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO" HeaderStyle-Width="400"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Batería" DataField="FL_BATERIA" UniqueName="FL_BATERIA" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                    <telerik:GridButtonColumn UniqueName="BTNELIMINAR" Text="Eliminar respuestas" CommandName="Delete" HeaderStyle-Width="150" ConfirmText="Este proceso borrará las respuestas de todas las pruebas de la batería ¿Desea continuar?">
                        <ItemStyle Width="10%" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="height: 10px; clear: both;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnDelCandidato" runat="server" Text="Eliminar" OnClientClicking="confirmarEliminar2" OnClick="btnDelCandidato_Click"></telerik:RadButton>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Aplicación interna" ToolTip="Selecciona esta opción si deseas que la aplicación sea administrada y monitoreada por el personal de selección." ID="btnAplicacionInterna" AutoPostBack="false" OnClientClicked="OpenAplicarPruebasInterna" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Aplicación externa" ToolTip="Selecciona esta opción si deseas enviar una liga de aplicación por correo electrónico." ID="btnAplicacionExterna" AutoPostBack="true" OnClick="btnAplicacionExterna_Click" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Aplicación masiva" ToolTip="Da clic en esta opción si deseas generar a 2 o más candidatos una liga para la aplicación de pruebas psicométricas." ID="btnAplicacionMasiva" AutoPostBack="false" OnClientClicked="OpenMensaje" />
    </div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_Click" AutoPostBack="false" OnClientClicked="closeWindow" />
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="winSeleccionCandidato" runat="server" Title="Seleccionar empleado" Height="600px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
