<%@ Page Title="" Language="C#" MasterPageFile="~/EO/MenuEO.master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SIGE.WebApp.EO.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

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

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ cltipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function OpenSelectionCapturistaWindow(sender, args) {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=CAPTURISTA", "winSeleccion", "Selección de capturistas");
        }

        function OpenSelectionMensajesImportantesWindow(sender, args) {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=IMPORTANTE", "winSeleccion", "Selección de empleado para recepción de mensajes importantes");
        }

        function OpenSelectionMensajesBajaCapturistaWindow(sender, args) {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=BAJACAPTURISTA", "winSeleccion", "Selección de empleado para recepción de mensajes de baja de capturista");
        }

        function OpenSelectionEmpleadosBajaNotificadoWindow(sender, args) {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=BAJANOTIFICADO", "winSeleccion", "Selección de empleado para recepción de mensajes de baja de notificador");
        }

        function OpenSelectionEmpleadosBajaReplicaWindow(sender, args) {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=BAJAREPLICA", "winSeleccion", "Selección de empleado para recepción de mensajes de baja de capturista en réplica");
        }

        function OpenSelectionCampoAdicionalWindow(sender, args) {
            OpenSelectionWindow("../Comunes/SeleccionCampoAdicional.aspx?mulSel=0", "winSeleccion", "Selección de campo adicional");
        }

        function useDataFromChild(pData) {
            if (pData != null) {

                var tipo = pData[0].clTipoCatalogo;
                var texto = "";

                if (tipo == "CAMPOADICIONAL") {
                    var listaCampos = $find("<%# lstBusqueda.ClientID %>");

                texto = pData[0].nbDato;
                SetListBoxItem(listaCampos, texto, pData[0].idDato);
            }
            else {
                InsertaEmpleados(EncapsularSeleccion(tipo, pData));
            }
        }
    }

    function InsertaEmpleados(pDato) {
        var ajaxManager = $find('<%= ramConfiguracion.ClientID%>');
        ajaxManager.ajaxRequest(pDato);
    }

    function EliminarCampoAdicional() {
        var listaCampos = $find("<%# lstBusqueda.ClientID %>");
        SetListBoxItem(listaCampos, "Seleccionar", "-1");

    }

    function SetListBoxItem(list, text, value) {
        if (list != undefined) {
            list.trackChanges();

            var items = list.get_items();
            items.clear();

            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(text);
            item.set_value(value);
            item.set_selected(true);
            items.add(item);

            list.commitChanges();
        }
    }

    </script>
    <style>
        .fieldset {
            border: 1px solid green;
        }

        .legend {
            padding: 0.2em 0.5em;
            border: 1px solid green;
            color: green;
            font-size: 90%;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Configuración</label>
    <div style="height: calc(100% - 90px);">
        <telerik:RadAjaxLoadingPanel ID="ralpConfiguracion" runat="server"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="ramConfiguracion" runat="server" OnAjaxRequest="ramConfiguracion_AjaxRequest" DefaultLoadingPanelID="ralpConfiguracion">
            <AjaxSettings>
<%--                <telerik:AjaxSetting AjaxControlID="btnAgregarCapturistas">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdCapturaResultados"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="grdCapturaResultados">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdCapturaResultados"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
              <%--  <telerik:AjaxSetting AjaxControlID="btnEliminaCapturistas">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdCapturaResultados" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
                <telerik:AjaxSetting AjaxControlID="ramConfiguracion">
                    <UpdatedControls>            
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdRecepcionMensajes" />
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdBajaCapturista" />
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdBajaNotificado" />
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="rgBajaReplica" />
                       <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAgregarImportante" />
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAgregarEncargado" />
                           <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAltaEmpleadoBajaNotificado" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnAgregarImportante">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdRecepcionMensajes" />
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAgregarImportante" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnEliminarImportante">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdRecepcionMensajes" />
                         <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAgregarImportante" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnAgregarEncargado">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="rgBajaReplica" />
                         <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAgregarEncargado" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnEliminarEncargado">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="rgBajaReplica" />
                          <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAgregarEncargado" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnAltaMensajeBajaCapturista">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdBajaCapturista" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnBajaMensajeBajaCapturista">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdBajaCapturista" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnAltaEmpleadoBajaNotificado">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdBajaNotificado" />
                           <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAltaEmpleadoBajaNotificado" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnBajaEmpleadoBajaNotificado">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdBajaNotificado" />
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="btnAltaEmpleadoBajaNotificado" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
            <Tabs>
                <telerik:RadTab Text="Captura de resultados"></telerik:RadTab>
                <telerik:RadTab Text="Notificación de baja en período"></telerik:RadTab>
                <%--<telerik:RadTab Text="Notificaciones de baja de capturista"></telerik:RadTab>--%>
                <telerik:RadTab Text="Notificación de baja de notificado"></telerik:RadTab>
                <telerik:RadTab Text="Notificación de baja en período réplica"></telerik:RadTab>
                <telerik:RadTab Text="Asignación de bono"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: 10px;"></div>
        <div style="height: calc(100% - 10px);">
            <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
                <%--Mensaje captura de resulatdos--%>
                <telerik:RadPageView ID="rpvCapturaResultados" runat="server">
                    <telerik:RadSplitter ID="spHelp" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="rpCapturaResultados" runat="server">
                            <div style="height: calc(100% - 100px);">
                                <%--      <div style="height: 20%;">--%>
                                <label id="lblDsMensajeCaptura">Cuerpo de mensaje para captura de resultados:</label>
                                <div style="height: 10px;"></div>
                                <%--               </div>--%>
                                <telerik:RadEditor
                                    Height="350"
                                    Width="50%"
                                    ToolsWidth="500"
                                    EditModes="Design"
                                    ID="txtMensajeCaptura"
                                    runat="server"
                                    ToolbarMode="Default"
                                    ToolsFile="~/Assets/AdvancedTools.xml">
                                </telerik:RadEditor>
                                <%-- <telerik:RadGrid ID="grdCapturaResultados" runat="server" OnNeedDataSource="grdCapturaResultados_NeedDataSource" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Height="100%" AllowMultiRowSelection="true" OnItemDataBound="grdCapturaResultados_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView DataKeyNames="ID_EMPLEADO, ID_CONFIGURACION_NOTIFICADO" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridClientSelectColumn UniqueName="FG_SELECCION">
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="80" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_AREA"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado" DataField="CL_ESTATUS" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>--%>
                            </div>
                            <%-- <div style="clear: both; height: 5px"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnAgregarCapturistas" Text="Agregar capturistas" AutoPostBack="false" OnClientClicked="OpenSelectionCapturistaWindow"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnEliminaCapturistas" Text="Eliminar capturistas" OnClick="btnEliminaCapturistas_Click"></telerik:RadButton>
                            </div>--%>
                        </telerik:RadPane>
                        <%-- <telerik:RadPane ID="rpayuda" runat="server" Width="30">
                            <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Captura de mensaje" Width="500" MinWidth="500" Height="100%">
                                    <div style="padding: 10px;">                              
                                    </div>
                                </telerik:RadSlidingPane>
                            </telerik:RadSlidingZone>
                        </telerik:RadPane>--%>
                    </telerik:RadSplitter>
                </telerik:RadPageView>
                <%--Persona importante--%>
                <telerik:RadPageView ID="rpvRecepcionMensajes" runat="server">
                    <telerik:RadSplitter ID="rsRecepcionMensajes" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="rpMensajeImportantes" runat="server">
                            <div style="height: calc(100% - 100px);">
                                <label id="Label2">Definie al empleado que recibirá mensajes importantes (si hay evaluados o capturistas dados de baja):</label>
                                <div style="height: 10px;"></div>
                                <div class="ctrlBasico" style="width: 50%">
                                    <telerik:RadGrid ID="grdRecepcionMensajes"
                                        runat="server" HeaderStyle-Font-Bold="true"
                                        OnNeedDataSource="grdRecepcionMensajes_NeedDataSource"
                                        AutoGenerateColumns="false"
                                        Height="350"
                                        Width="100%"
                                        AllowMultiRowSelection="true" OnItemDataBound="grdRecepcionMensajes_ItemDataBound">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView DataKeyNames="ID_EMPLEADO, ID_CONFIGURACION_NOTIFICADO" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                            <Columns>
                                                <telerik:GridClientSelectColumn UniqueName="FG_SELECCION">
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridClientSelectColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_AREA"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado" DataField="CL_ESTATUS" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <div style="clear: both; height: 5px"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton runat="server" ID="btnAgregarImportante" Text="Agregar empleado" AutoPostBack="false" OnClientClicked="OpenSelectionMensajesImportantesWindow"></telerik:RadButton>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton runat="server" ID="btnEliminarImportante" Text="Eliminar empleado" OnClick="btnEliminarImportante_Click"></telerik:RadButton>
                                    </div>
                                </div>
                                <div class="ctrlBasico" style="width: 50%">
                                    <telerik:RadEditor
                                        Height="350"
                                        Width="100%"
                                        ToolsWidth="500"
                                        EditModes="Design"
                                        ID="txtEmpleadoMensaje" 
                                        runat="server" 
                                        ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml">
                                    </telerik:RadEditor>
                                </div>
                            </div>
                        </telerik:RadPane>
                        <%--   <telerik:RadPane ID="prRecepcionMensaje" runat="server" Width="30">
                            <telerik:RadSlidingZone ID="rszRecepcionMensaje" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                <telerik:RadSlidingPane ID="rspRecepcionMensaje" runat="server" Title="Captura de mensaje" Width="500" MinWidth="500" Height="100%">
                                    <div style="padding: 10px;">

                                        <div style="height: 20%;">
                                            <label id="lblDsEmpleadoMensaje">Definie al empleado que recibirá mensajes importantes (si hay evaluados o capturistas dados de baja):</label>
                                        </div>

                                        <telerik:RadEditor Height="700" Width="500" ToolsWidth="500" EditModes="Design" ID="txtEmpleadoMensaje" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                                    </div>
                                </telerik:RadSlidingPane>
                            </telerik:RadSlidingZone>
                        </telerik:RadPane>--%>
                    </telerik:RadSplitter>
                </telerik:RadPageView>
                <%--<telerik:RadPageView ID="rpvBajaCapturista" runat="server">
                    <telerik:RadSplitter ID="rsBajaCapturista" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="rpGrid" runat="server">
                            <div style="height: calc(100% - 100px);">
                                <telerik:RadGrid ID="grdBajaCapturista" runat="server" OnNeedDataSource="grdBajaCapturista_NeedDataSource" AutoGenerateColumns="false" Height="100%" AllowMultiRowSelection="true">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView DataKeyNames="ID_EMPLEADO, ID_CONFIGURACION_NOTIFICADO" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridClientSelectColumn UniqueName="FG_SELECCION">
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="80" HeaderText="Nombre" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área" DataField="NB_DEPARTAMENTO" UniqueName="NB_AREA"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado" DataField="CL_ESTATUS" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both; height: 5px"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnAltaMensajeBajaCapturista" Text="Agregar empleados" AutoPostBack="false" OnClientClicked="OpenSelectionMensajesBajaCapturistaWindow"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnBajaMensajeBajaCapturista" Text="Eliminar empleados" OnClick="btnBajaMensajeBajaCapturista_Click"></telerik:RadButton>
                            </div>
                        </telerik:RadPane>
                        <telerik:RadPane ID="rpMensaje" runat="server" Width="30">
                            <telerik:RadSlidingZone ID="rszBajaCapturista" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                <telerik:RadSlidingPane ID="rspBajaCapturista" runat="server" Title="Captura de mensaje" Width="500" MinWidth="500" Height="100%">
                                    <div style="padding: 10px;">
                                        <div style="height: 20%;">
                                            <label id="lblDsMensajeBajaCapturista">Elige a quién notificar si fue dado de baja el empleado que captura los resultados:</label>
                                        </div>
                                        <telerik:RadEditor Height="700" Width="100%" ToolsWidth="500" EditModes="Design" ID="txtMensajeBajaCapturista" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                                    </div>
                                </telerik:RadSlidingPane>
                            </telerik:RadSlidingZone>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </telerik:RadPageView>--%>
                <%--Persona más importante--%>
                <telerik:RadPageView ID="rpvBajaNotificado" runat="server">
                    <telerik:RadSplitter ID="rsBajaNotificado" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="rpGridBajaNotificado" runat="server">
                            <div style="height: calc(100% - 100px);">
                                <label id="lblDsMensajeBajaNotificado">Elige a quién debe notificarse si el encargado de recibir mensajes fue dado de baja:</label>
                                <div style="height: 10px;"></div>
                                <div class="ctrlBasico" style="width: 50%">
                                    <telerik:RadGrid
                                        ID="grdBajaNotificado"
                                        runat="server"
                                        HeaderStyle-Font-Bold="true"
                                        OnNeedDataSource="grdBajaNotificado_NeedDataSource"
                                        AutoGenerateColumns="false"
                                        Height="350"
                                        Width="100%"
                                        AllowMultiRowSelection="true"
                                        OnItemDataBound="grdBajaNotificado_ItemDataBound">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView DataKeyNames="ID_EMPLEADO, ID_CONFIGURACION_NOTIFICADO" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                            <Columns>
                                                <telerik:GridClientSelectColumn UniqueName="FG_SELECCION">
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridClientSelectColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_AREA"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado" DataField="CL_ESTATUS" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <div style="clear: both; height: 5px"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton runat="server" ID="btnAltaEmpleadoBajaNotificado" Text="Agregar empleado" AutoPostBack="false" OnClientClicked="OpenSelectionEmpleadosBajaNotificadoWindow"></telerik:RadButton>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton runat="server" ID="btnBajaEmpleadoBajaNotificado" Text="Eliminar empleado" OnClick="btnBajaEmpleadoBajaNotificado_Click"></telerik:RadButton>
                                    </div>
                                </div>
                                <div class="ctrlBasico" style="width: 50%">
                                    <telerik:RadEditor
                                        Height="350"
                                        Width="100%" ToolsWidth="500" EditModes="Design" ID="txtMensajeBajaNotificado" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml">
                                    </telerik:RadEditor>
                                </div>
                            </div>
                        </telerik:RadPane>
                        <%--   <telerik:RadPane ID="rpMensajeBajaNotificado" runat="server" Width="30">
                            <telerik:RadSlidingZone ID="rszMensajeBajoNotificado" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                <telerik:RadSlidingPane ID="rspMensajeBajoNotificado" runat="server" Title="Captura de mensaje" Width="500" MinWidth="500" Height="100%">
                                    <div style="padding: 10px;">

                                      <div style="height: 20%;">
                                         
                                        </div>

                                        <telerik:RadEditor Height="700" Width="100%" ToolsWidth="500" EditModes="Design" ID="txtMensajeBajaNotificado" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                                    </div>
                                </telerik:RadSlidingPane>
                            </telerik:RadSlidingZone>
                        </telerik:RadPane>--%>
                    </telerik:RadSplitter>
                </telerik:RadPageView>
                <%--Persona bajas en replicas--%>
                <telerik:RadPageView ID="rpvBajaReplica" runat="server">
                    <telerik:RadSplitter ID="rsBajaReplica" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="rpBajaReplica" runat="server">
                            <div style="height: calc(100% - 100px);">
                                <label id="Label1">Definie al empleado que recibirá mensajes importantes (si hay evaluados o capturistas dados de baja) en periodos réplica: </label>
                                <div style="height: 10px;"></div>
                                <div class="ctrlBasico" style="width: 50%">
                                    <telerik:RadGrid
                                        ID="rgBajaReplica"
                                        runat="server"
                                        HeaderStyle-Font-Bold="true"
                                        OnNeedDataSource="rgBajaReplica_NeedDataSource"
                                        AutoGenerateColumns="false"
                                        Height="350"
                                        Width="100%"
                                        AllowMultiRowSelection="true"
                                        OnItemDataBound="rgBajaReplica_ItemDataBound">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView DataKeyNames="ID_EMPLEADO, ID_CONFIGURACION_NOTIFICADO" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                            <Columns>
                                                <telerik:GridClientSelectColumn UniqueName="FG_SELECCION">
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridClientSelectColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_AREA"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado" DataField="CL_ESTATUS" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <div style="clear: both; height: 5px"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton runat="server" ID="btnAgregarEncargado" Text="Agregar empleado" AutoPostBack="false" OnClientClicked="OpenSelectionEmpleadosBajaReplicaWindow"></telerik:RadButton>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton runat="server" ID="btnEliminarEncargado" Text="Eliminar empleado" OnClick="btnEliminarEncargado_Click"></telerik:RadButton>
                                    </div>
                                </div>
                                <div class="ctrlBasico" style="width: 50%">
                                    <telerik:RadEditor
                                        Height="350"
                                        Width="100%" ToolsWidth="500" EditModes="Design" ID="txtBajaReplica" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml">
                                    </telerik:RadEditor>
                                </div>
                            </div>
                        </telerik:RadPane>
                        <%--   <telerik:RadPane ID="rpMensajeBajaNotificado" runat="server" Width="30">
                            <telerik:RadSlidingZone ID="rszMensajeBajoNotificado" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                <telerik:RadSlidingPane ID="rspMensajeBajoNotificado" runat="server" Title="Captura de mensaje" Width="500" MinWidth="500" Height="100%">
                                    <div style="padding: 10px;">
                                      <div style="height: 20%;">                                        
                                        </div>
                                        <telerik:RadEditor Height="700" Width="100%" ToolsWidth="500" EditModes="Design" ID="txtMensajeBajaNotificado" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                                    </div>
                                </telerik:RadSlidingPane>
                            </telerik:RadSlidingZone>
                        </telerik:RadPane>--%>
                    </telerik:RadSplitter>
                </telerik:RadPageView>
                <%--Criterios de bono--%>
                <telerik:RadPageView ID="rpvAsignacionBono" runat="server">
                    <div style="overflow-y: scroll; height: calc(100% - 70px); padding-right: 10px;">
                        <div style="clear: both; height: 10px;"></div>
                        <fieldset>
                            <legend>
                                <label style="font-weight: bold;">Individual independiente</label></legend>
                            <div style="height: 10px;"></div>
                            <label id="lblDsIndependiente" class="labelConfiguracion">Los integrantes de este grupo, recibirán el bono correspondiente dependiendo de sus resultados individuales e independientemente del resultado que obtenga el grupo. </label>
                            <div style="height: 5px;"></div>
                            <div class="ctrlBasico">
                                <%--<div style="text-align: right; float: left; margin-right: 15px; width: 400px;">--%>
                                <div class="divControlIzquierda" style="width: 400px;">
                                    <label id="lblDsBonoMinimo" class="labelConfiguracion">Minimo nivel de cumplimiento para recibir el bono</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtNivelMinimoII" InputType="Number" Width="50px" Height="30" runat="server"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <%--<div style="text-align: right; float: left; margin-right: 15px; width: 400px;">--%>
                                <div class="divControlIzquierda" style="width: 200px;">
                                    <label id="lblDsBonoMinimoAlcanzable" class="labelConfiguracion">Bono mínimo alcanzable</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtBonoMinimoII" InputType="Number" Width="50px" Height="30" runat="server"></telerik:RadTextBox>
                                </div>
                            </div>
                        </fieldset>
                        <div style="clear: both; height: 10px;"></div>
                        <fieldset>
                            <legend>
                                <label style="font-weight: bold;">Individual dependiente</label></legend>
                            <div style="height: 10px;"></div>
                            <label id="lblBonoResultadosIndividuales" class="labelConfiguracion">Los integrantes de este grupo recibirán el bono correspondiente siempre y cuando el grupo obtenga el mínimo nivel de cumplimiento para recibir bono definido por la empresa. El resultado del bono de cada persona dependerá de los resultados individuales.</label>
                            <div style="height: 5px;"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda" style="width: 400px;">
                                    <label id="lblDsRecibirBonoIndividual" class="labelConfiguracion">Minimo nivel de cumplimiento para recibir el bono</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtNivelMinimoID" InputType="Number" Width="50px" Height="30" runat="server"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda" style="width: 200px;">
                                    <label id="lblDsBonoMinimoIndividual" class="labelConfiguracion">Bono mínimo alcanzable</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtBonoMinimoID" InputType="Number" Width="50px" Height="30" runat="server"></telerik:RadTextBox>
                                </div>
                            </div>
                        </fieldset>
                        <div style="clear: both; height: 10px;"></div>
                        <fieldset>
                            <legend>
                                <label style="font-weight: bold;">Grupal</label></legend>
                            <div style="height: 10px;"></div>
                            <label id="lblDsGrupal" class="labelConfiguracion">El grupo se ganará el bono dependiendo del resultado del grupo, sin excluir a ningún integrante independientemente de sus resultados individuales.</label>
                            <div style="height: 5px;"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda" style="width: 400px;">
                                    <label id="lblDsRecibirBonoGrupal" class="labelConfiguracion">Minimo nivel de cumplimiento para recibir el bono</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtNivelMinimoG" InputType="Number" Width="50px" Height="30" runat="server"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda" style="width: 200px;">
                                    <label id="lblDsBonoMinimoGrupal" class="labelConfiguracion">Bono mínimo alcanzable</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtBonoMinimoG" InputType="Number" Width="50px" Height="30" runat="server"></telerik:RadTextBox>
                                </div>
                            </div>
                        </fieldset>
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico" style="width: 300px;">
                            <label id="lblSueldoAsignacion" class="labelConfiguracion">Sueldo para asignación de bono</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="divControlCenter">
                            <telerik:RadButton ID="rbSueldo" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Campo sueldo fijo" AutoPostBack="false"></telerik:RadButton>
                            <br />
                            <telerik:RadButton ID="rbExtra" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Campos extra" AutoPostBack="false"></telerik:RadButton>
                            <br />
                            <telerik:RadListBox ID="lstBusqueda" Width="300" runat="server">
                                <Items>
                                    <telerik:RadListBoxItem Text="Seleccionar" />
                                </Items>
                            </telerik:RadListBox>
                            <telerik:RadButton ID="rdSeleccionar" CssClass="labelConfiguracion" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionCampoAdicionalWindow"></telerik:RadButton>
                            <telerik:RadButton ID="rdBorrar" runat="server" Text="X" AutoPostBack="false" OnClientClicked="EliminarCampoAdicional"></telerik:RadButton>
                        </div>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server">
        <Windows>
            <telerik:RadWindow ID="winSeleccion" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
