<%@ Page Title="" Language="C#" MasterPageFile="~/EO/MenuEO.master" AutoEventWireup="true" CodeBehind="ClimaLaboral.aspx.cs" Inherits="SIGE.WebApp.EO.ClimaLaboral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .RadListViewContainer {
            width: 200px;
            border: 1px solid lightgray;
            float: left;
            margin: 5px;
            border-radius: 5px;
        }

            .RadListViewContainer.Selected {
                background-color: #9A0209 !important;
                color: #fff !important;
            }

            .RadListViewContainer > .SelectionOptions {
                overflow: auto;
                background-color: lightgray;
                padding: 10px;
            }

        .labelSubtitulo {
            margin-top: 20px;
            display: block;
            font-size: 1.6em;
        }
    </style>

    <script type="text/javascript">

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
        }

        function onCloseWindow(sender, args) {
            var lista = $find("<%# rlvPeriodos.ClientID%>");
            lista.rebind();
        }

        function AbrirVentanaNuevo() {
            OpenWindow(GetNewWindowProperties());
           // openChildDialog("VentanaPeriodoClimaLaboral.aspx", "WinClimaLaboral", "Agregar periodo")
        }

        function GetNewWindowProperties() {
            //var wnd = GetWindowProperties();
            //wnd.width = 1050;
            //wnd.height = 600;
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var wnd = {
                width: browserWnd.innerWidth - 400,
                height: browserWnd.innerHeight - 20
            };
            wnd.vTitulo = "Agregar período";
            wnd.vURL = "VentanaPeriodoClimaLaboral.aspx";
            wnd.vRadWindowId = "WinClimaLaboral";
            return wnd;
        }

        function GetPeriodoNombre() {
            var listView = $find('<%= rlvPeriodos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["NB_PERIODO"];
            else
                return null;
        }

        function GetPeriodoId() {
            var listView = $find('<%= rlvPeriodos.ClientID %>');
                var selectedIndex = listView.get_selectedIndexes();
                if (selectedIndex.length > 0)
                    return listView.get_clientDataKeyValue()[selectedIndex]["ID_PERIODO"];
                else
                    return null;
            }

            function OpenWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
            }

            function OpenEditPeriodoWindow() {
                if ('<%= vEditar %>' == "True") {
                    OpenPeriodoWindow(GetPeriodoId());
                }
                else {
                    radalert("No tiene los permisos necesarios para llevar a cabo esta función.", 450, 200, "");
                }
            }


            function OpenPeriodoWindow(pIdPeriodo) {
                OpenWindow(GetPeriodoWindowProperties(pIdPeriodo));
            }

            function confirmarEliminar(sender, args) {
                if ('<%= vEliminar %>' == "True") {
                var listView = $find('<%= rlvPeriodos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0) {
                var vNombre = listView.get_clientDataKeyValue()[selectedIndex]["CL_PERIODO"];
                var vWindowsProperties = {
                    height: 200
                };
                confirmAction(sender, args, "¿Deseas eliminar el período " + vNombre + '?, este proceso no podrá revertirse');
            }
            else {
                radalert("Selecciona un período.", 400, 150);
                args.set_cancel(true);
            }
                }
                else {
                    radalert("No tiene los permisos necesarios para llevar a cabo esta función.", 450, 200, "");
                    args.set_cancel(true);
                }
        }

        function GetPeriodoWindowProperties(pIdPeriodo) {
            //var wnd = GetWindowProperties();
            //wnd.width = 1050;
            //wnd.height = 580;
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var wnd = {
                width: browserWnd.innerWidth - 400,
                height: browserWnd.innerHeight - 20
            };
            wnd.vTitulo = "Agregar período";
            wnd.vRadWindowId = "WinClimaLaboral";
            wnd.vURL = "VentanaPeriodoClimaLaboral.aspx";
            if (pIdPeriodo != null) {
                wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                wnd.vTitulo = "Editar período";
            }
            return wnd;
        }

        function OpenConfiguracionWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetConfiguracionWindowProperties(vIdPeriodo));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        function GetConfiguracionWindowProperties(pIdPeriodo) {
            //var wnd = GetWindowProperties();
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var wnd = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            wnd.vTitulo = "Configuración del período";
            wnd.vURL = "VentanaConfigurarClima.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "WinConfigurar";
            return wnd;
        }

        function OpenCopiaPeriodoWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetCopiaWindowProperties(vIdPeriodo));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        function GetCopiaWindowProperties(pIdPeriodo) {
            //var wnd = GetWindowProperties();
            //wnd.width = 1050;
            //wnd.height = 580;
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var wnd = {
                width: browserWnd.innerWidth - 400,
                height: browserWnd.innerHeight - 20
            };
            wnd.vTitulo = "Copiar período";
            wnd.vURL = "VentanaPeriodoClimaLaboral.aspx?PeriodoId=" + pIdPeriodo + "&Tipo=COPIA";
            wnd.vRadWindowId = "WinClimaLaboral";
            return wnd;
        }

        function OpenEnvioSolicitudesWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetEnvioSolicitudesWindowProperties(vIdPeriodo));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        function GetEnvioSolicitudesWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.width = 1050;
            //wnd.height = 580;
            wnd.vTitulo = "Enviar cuestionarios";
            wnd.vURL = "EnvioDeSolicitudesClima.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "WinClimaLaboral";
            return wnd;
        }

        function OpenControlAvanceWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetControlAvanceWindowProperties(vIdPeriodo));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        function GetControlAvanceWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Control de avance";
            wnd.vURL = "VentanaControlAvance.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "WinClimaLaboral";
            return wnd;
        }

        function OpenVistaPreviaWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null) {

                //OpenWindow(GetVistaPreviaWindowProperties(vIdPeriodo));
                var win = window.open("VistaPreviaCuestionario.aspx?PeriodoId=" + vIdPeriodo, '_blank');
                win.focus();
            }
            else {
                radalert("Selecciona un período.", 400, 150);
            }
        }

        function GetVistaPreviaWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Vista previa de impresión de cuestionario";
            wnd.vURL = "VistaPreviaCuestionario.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "WinClimaLaboral";
            return wnd;
        }

        function OpenContestarPreviaWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetContestarWindowProperties(vIdPeriodo));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        function GetContestarWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Contestar cuestionarios";
            wnd.vURL = "VentanaContestarCuestionario.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "WinClimaLaboral";
            return wnd;
        }

        //Reportes

        function OpenReportesWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetReportesProperties(vIdPeriodo, "INDICE"));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        function OpenDistribucionWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetReportesProperties(vIdPeriodo, "DISTRIBUCION"));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        function OpenPAbiertasWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetReportesProperties(vIdPeriodo, "PREGUNTAS"));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        function OpenGlobalWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetReportesProperties(vIdPeriodo, "GENERAL"));
            else
                radalert("Selecciona un período.", 400, 150);
        }

        //

        function GetReportesProperties(pIdPeriodo, pClDestino) {
            var wnd = GetWindowProperties();
            if (pClDestino == "INDICE")
                wnd.vTitulo = "Consulta Índice de satisfacción - Clima laboral";
            if (pClDestino == "DISTRIBUCION")
                wnd.vTitulo = "Consulta Distribución de resultados - Clima laboral";
            if (pClDestino == "PREGUNTAS")
                wnd.vTitulo = "Consulta Preguntas abiertas - Clima laboral";
            if (pClDestino == "GENERAL")
                wnd.vTitulo = "Consulta Resultado global - Clima laboral";

                wnd.vURL = "VentanaReportes.aspx?PeriodoId=" + pIdPeriodo + "&ClDestino=" + pClDestino;
            wnd.vRadWindowId = "WinClimaLaboral";
            return wnd;
        }

        function ConfirmarCerrar(sender, args) {
            var vIdPeriodo = GetPeriodoId();
            var vNbPeriodo = GetPeriodoNombre();

            if (vIdPeriodo != null) {

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('¿Deseas cerrar el período ' + vNbPeriodo + ' ?', callBackFunction, 400, 170, null, "Cerrar Período");
                args.set_cancel(true);
            }
            else {
                radalert("Seleccione un período.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function ConfirmarReactivar(sender, args) {
            var vIdPeriodo = GetPeriodoId();
            var vNbPeriodo = GetPeriodoNombre();

            if (vIdPeriodo != null) {

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('¿Deseas reactivar el período ' + vNbPeriodo + ' ?', callBackFunction, 400, 170, null, "Reactivar Período");
                args.set_cancel(true);
            }
            else {
                radalert("Seleccione un período.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].accion) {
                    case "ACTUALIZARLISTA":
                        var ajaxManager = $find('<%= ramOrganigrama.ClientID%>');
                        ajaxManager.ajaxRequest(JSON.stringify({ clTipo: "ACTUALIZARLISTA" }));
                        break;
                    case "ACTUALIZAR":
                        var list = $find('<%= rlvPeriodos.ClientID %>');
                        list.rebind();
                        break;
                }
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpPeriodosClima" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramOrganigrama" runat="server" OnAjaxRequest="ramOrganigrama_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rlvPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodosClima"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="ramOrganigrama">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodosClima"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbAscendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodosClima"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbDescendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodosClima"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rfFiltros" LoadingPanelID="ralpPeriodosClima"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodosClima"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rlvPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnContestar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="txtClPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtDsPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtNotas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtClEstatus" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtTipo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtUsuarioMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtFechaMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodosClima" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodosClima"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnContestar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReactivar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnContestar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="rsClimaLaboral" BorderSize="0" Width="100%" Height="100%" runat="server">
        <telerik:RadPane ID="rpClimaLaboral" runat="server">
            <label class="labelTitulo">Clima laboral</label>
            <div class="ctrlBasico">
                <telerik:RadListView ID="rlvPeriodos" runat="server" DataKeyNames="ID_PERIODO,CL_ESTADO_PERIODO, CL_PERIODO" ClientDataKeyNames="ID_PERIODO,NB_PERIODO, CL_PERIODO" OnNeedDataSource="rlvPeriodos_NeedDataSource" OnItemCommand="rlvPeriodos_ItemCommand" AllowPaging="true" ItemPlaceholderID="ProductsHolder">
                    <LayoutTemplate>
                        <div style="overflow: auto; width: 650px;">
                            <div style="overflow: auto; overflow-y: auto; max-height: 400px;">
                                <asp:Panel ID="ProductsHolder" runat="server"></asp:Panel>
                            </div>
                            <div style="clear: both;"></div>
                            <telerik:RadDataPager ID="rdpPeriodos" runat="server" PagedControlID="rlvPeriodos" PageSize="6" Width="630" >
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric" PageButtonCount="5"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerGoToPageField CurrentPageText="Página:" TotalPageText="de" SubmitButtonText="Ir"></telerik:RadDataPagerGoToPageField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="RadListViewContainer">
                            <div style="padding: 10px;">
                                <div>
                                    <label>Clave:</label>
                                    <%# Eval("CL_PERIODO") %>
                                </div>
                                <div>
                                    <label>Estatus:</label>
                                    <%# Eval("CL_ESTADO_PERIODO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label>Descripción:</label>
                                    <%# Eval("DS_PERIODO") %>
                                </div>
                            </div>
                            <div class="SelectionOptions">
                                <telerik:RadButton ID="btnSeleccionar" runat="server" CommandName="Select" Text="Seleccionar" Width="100%"></telerik:RadButton>
                            </div>
                        </div>
                    </ItemTemplate>
                    <SelectedItemTemplate>
                        <div class="RadListViewContainer Selected">
                            <div style="padding: 10px;">
                                <div>
                                    <label style="color: white;">Clave:</label>
                                    <%# Eval("CL_PERIODO") %>
                                </div>
                                <div>
                                    <label style="color: white;">Estatus:</label>
                                    <%# Eval("CL_ESTADO_PERIODO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label style="color: white;">Descripción:</label>
                                    <%# Eval("DS_PERIODO") %>
                                </div>
                            </div>
                            <div class="SelectionOptions">
                                <%--<telerik:RadButton ID="btnSeleccionar" runat="server" CommandName="Select" Text="Seleccionar"></telerik:RadButton>--%>
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnModificar" runat="server" Text="Editar" Width="100%" OnClientClicked="OpenEditPeriodoWindow"></telerik:RadButton>
                                </div>
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100%" OnClientClicking="confirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </SelectedItemTemplate>
                    <EmptyDataTemplate>
                        <div class="RadListViewContainer" style="overflow: auto; text-align: center; width: 660px; height: 100px;">
                            No hay períodos disponibles
                        </div>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
            <div class="ctrlBasico" style="text-align: left">
                <%--<div>
                    <div class="divControlIzquierda">
                        <label id="lblDsDescripción">Descripción:</label>
                    </div>
                    <div class="divControlIzquierda">
                        <telerik:RadTextBox ID="txtDsDescripcion" Enabled="false" InputType="Text" Width="400" Height="30" runat="server"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="height: 42px;"></div>
                <div style="height: 32px;">
                    <div class="divControlIzquierda">
                        <label id="lblDsEstado">Estado:</label>
                    </div>
                    <div class="divControlIzquierda">
                        <telerik:RadTextBox ID="txtDsEstado" Enabled="false" InputType="Text" Width="120" Height="30" runat="server"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="height: 10px;"></div>
                <div>
                    <div class="divControlIzquierda">
                        <label id="lblDsNotas">Notas:</label>
                    </div>
                    <div class="divControlIzquierda">
                        <telerik:RadEditor Height="100" Width="400" ToolsWidth="400" EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                    </div>
                </div>--%>
                 <telerik:RadTabStrip runat="server" ID="rtsPeriodos" MultiPageID="rmpPeriodos" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Gestionar"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Detalles"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: 10px;"></div>
                <telerik:RadMultiPage runat="server" ID="rmpPeriodos" SelectedIndex="0" Height="100%" Width="100%">
                    <telerik:RadPageView ID="rpvPeriodos" runat="server">
                <div style="height: 10px;"></div>
                <div style="text-align: center">
                    <label class="labelTitulo">Administrar</label>
                    <telerik:RadButton ID="rdAgregar" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="AbrirVentanaNuevo"></telerik:RadButton>
                    <telerik:RadButton ID="btnConfigurar" runat="server" Text="Configurar" AutoPostBack="false" OnClientClicked="OpenConfiguracionWindow"></telerik:RadButton>
                    <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClicking="ConfirmarCerrar" OnClick="btnCerrar_Click"></telerik:RadButton>
                    <telerik:RadButton ID="btnReactivar" runat="server" Text="Re abrir" OnClientClicking="ConfirmarReactivar" OnClick="btnReactivar_Click"></telerik:RadButton>
                    <telerik:RadButton ID="btnCopiar" runat="server" Text="Copiar de" AutoPostBack="false" OnClientClicked="OpenCopiaPeriodoWindow"></telerik:RadButton>
                </div>
                <div style="height: 10px;"></div>
                <div style="text-align: center">
                    <label class="labelTitulo">Cuestionarios</label>                 
                    <telerik:RadButton ID="btnVistaPreviaCuestionario" runat="server"   Text="Vista previa" AutoPostBack="false" OnClientClicked="OpenVistaPreviaWindow"></telerik:RadButton>
                      <telerik:RadButton ID="btnEnviarSolicitudes" runat="server"  Text="Enviar cuestionarios" AutoPostBack="false" OnClientClicked="OpenEnvioSolicitudesWindow"></telerik:RadButton>
                    <telerik:RadButton ID="btnContestar" runat="server" Text="Contestar"  AutoPostBack="false" OnClientClicked="OpenContestarPreviaWindow"></telerik:RadButton>
                      <telerik:RadButton ID="btnControlAvance" runat="server" Text="Control de avance" AutoPostBack="false" OnClientClicked="OpenControlAvanceWindow"></telerik:RadButton>
                </div>
                <div style="height: 10px;"></div>
                <div style="text-align: center">
                    <label class="labelTitulo">Consultas</label> 
                    <telerik:RadButton ID="btnReportes" runat="server" Text="Índice de satisfacción" AutoPostBack="false" OnClientClicked="OpenReportesWindow"></telerik:RadButton>
                     <telerik:RadButton ID="btnRDistribucion" runat="server" Text="Distribución de resultados" AutoPostBack="false" OnClientClicked="OpenDistribucionWindow"></telerik:RadButton>
                                             <div style="height: 10px;"></div>
                     <telerik:RadButton ID="btnPreguntasAbiertas" runat="server" Text="Preguntas abiertas" AutoPostBack="false" OnClientClicked="OpenPAbiertasWindow"></telerik:RadButton>
                     <telerik:RadButton ID="btnGlobal" runat="server" Text="Resultado global" AutoPostBack="false" OnClientClicked="OpenGlobalWindow"></telerik:RadButton>
                </div>
                <div style="height: 10px;"></div>
                        </telerik:RadPageView>

                       <telerik:RadPageView ID="rpvInformacion" runat="server">
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="lblEvento" name="lblEvento" runat="server">Período:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClPeriodo" Enabled="false" runat="server" Width="400px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label1" name="lblEvento" runat="server">Descripción:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtDsPeriodo" Enabled="false" runat="server" Width="400px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>


                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label4" name="lblEvento" runat="server">Notas:</label>
                            <div class="divControlDerecha">
                             <div id="txtNotasContenedor"  runat="server" style="width:400px; height:100px; text-align:justify; border: 1px solid #ccc; border-radius:5px; background: #ccc; "><p id="txtNotas" runat="server" style="padding:10px;"></p></div>
                            </div>
                        </div>


                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label2" name="lblEvento" runat="server">Estatus:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClEstatus" Enabled="false" runat="server" Width="200px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label3" name="lblEvento" runat="server">Tipo de cuestionario:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtTipo" Enabled="false" runat="server" Width="400px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label5" name="lblCurso" runat="server">Último usuario que modifica:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtUsuarioMod" Enabled="false" runat="server" Width="140px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label6" name="lblCurso" runat="server">Última fecha de modificación:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtFechaMod" Enabled="false" runat="server" Width="140px" MaxLength="1000" Height="35px" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                    </telerik:RadPageView>

                    </telerik:RadMultiPage>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyudaClimaLaboral" runat="server" Scrolling="None" Width="20px">
            <telerik:RadSlidingZone ID="rszAyudaClimaLaboral" runat="server" ClickToOpen="true" SlideDirection="Left" ExpandedPaneId="rsClimaLaboral" Width="20px">
                <telerik:RadSlidingPane ID="rspAyudaClimaLaboral" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="100%">
                    <div style="padding: 20px; text-align: justify;">
                        <p>
                            Esta opción te permite dar mantenimiento a los períodos para el módulo de Clima Laboral, 
                                   generando nuevos períodos y cerrando o re-abriendo períodos existentes.
                        </p>
                        <p>Nota: Si se decide re-crear un período, toda la información previamente generada -como programación y cuestionarios- será eliminada.</p>
                    </div>
                </telerik:RadSlidingPane>
                <telerik:RadSlidingPane ID="rspOrdenarFiltrar" runat="server" Title="Ordenar y filtrar" Width="450px" RenderMode="Mobile" Height="100%">
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <fieldset>
                            <legend>Ordenar por:</legend>
                            <telerik:RadComboBox ID="cmbOrdenamiento" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Clave del período" Value="CL_PERIODO" />
                                    <telerik:RadComboBoxItem Text="Nombre del período" Value="NB_PERIODO" />
                                    <telerik:RadComboBoxItem Text="Estatus" Value="CL_ESTADO_PERIODO" />
                                    <telerik:RadComboBoxItem Text="Fecha de inicio del evento" Value="FE_INICIO" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadButton ID="rbAscendente" runat="server" ButtonType="ToggleButton" ToggleType="Radio" Text="Ascendente" GroupName="ordenamiento" OnCheckedChanged="rbAscendente_CheckedChanged"></telerik:RadButton>
                            <telerik:RadButton ID="rbDescendente" runat="server" ButtonType="ToggleButton" ToggleType="Radio" Text="Descendente" GroupName="ordenamiento" OnCheckedChanged="rbDescendente_CheckedChanged"></telerik:RadButton>
                        </fieldset>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <fieldset>
                            <legend>Filtrar por:</legend>
                            <telerik:RadFilter runat="server" ID="rfFiltros" FilterContainerID="rlvPeriodos" ExpressionPreviewPosition="Top" ApplyButtonText="Filtrar">
                            </telerik:RadFilter>
                        </fieldset>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="WinClimaLaboral" runat="server" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
            <telerik:RadWindow ID="WinCues" runat="server" Width="1050px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
            <telerik:RadWindow ID="WinConfigurar" runat="server" Width="1000px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinCuestionario" runat="server" Width="1050px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="WinPreguntas" runat="server" Width="450px" Height="500px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="WinConsultaPersonal" runat="server" Width="1050px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade"> </telerik:RadWindow>
            <telerik:RadWindow ID="WinFiltrosSeleccion" runat="server" Width="1000px" Height="420px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
