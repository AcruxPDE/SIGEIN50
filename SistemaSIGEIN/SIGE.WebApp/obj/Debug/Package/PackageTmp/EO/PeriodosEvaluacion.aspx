<%@ Page Title="" Language="C#" MasterPageFile="~/EO/MenuEO.master" AutoEventWireup="true" CodeBehind="PeriodosEvaluacion.aspx.cs" Inherits="SIGE.WebApp.EO.PeriodosEvaluacion" %>

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
                background-color: #FF7400 !important;
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
        function onCloseWindow(sender, args) {
            var lista = $find("<%# rlvPeriodos.ClientID %>");
            lista.rebind();
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
        }

        function GetControlAvanceWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Control de avance";
            wnd.vURL = "/EO/ControlAvance.aspx?idPeriodo=" + pIdPeriodo;
            wnd.vRadWindowId = "winPeriodo";
            return wnd;
        }
        <%-- 
        //function GetConfiguracionWindowProperties(pIdPeriodo) {
        //    var wnd = GetWindowProperties();
        //    wnd.vTitulo = "Configuración del periodo";
        //    wnd.vURL = "/FYD/ConfiguracionPeriodo.aspx?PeriodoId=" + pIdPeriodo;
        //    wnd.vRadWindowId = "winPeriodo";
        //    return wnd;
        //}

         function GetPeriodoWindowProperties(pIdPeriodo, pTipoTarea) {
            var wnd = GetWindowProperties();
            wnd.width = 750;
            wnd.height = 650;
            wnd.vTitulo = "Agregar periodo";
            wnd.vRadWindowId = "winPeriodo";
            wnd.vURL = "/FYD/PeriodoEvaluacion.aspx";
            if (pIdPeriodo != null) {
                if (pTipoTarea != "COPIA") {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    wnd.vTitulo = "Editar periodo";
                }
                else {
                    wnd.vURL += String.format("?PeriodoId={0}&TipoTarea={1}", pIdPeriodo, pTipoTarea);
                    wnd.vTitulo = "Copiar periodo";
                }
            }
            return wnd;
        }
       --%>

        function GetContestarCuestionariosWindowProperties(pIdPeriodo, pNbPeriodo, pEstadoPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Contestar cuestionarios";
            wnd.vURL = "/EO/ContestarCuestionarios.aspx?PeriodoId=" + pIdPeriodo + "&PeriodoNb=" + pNbPeriodo + "&EstadoPeriodo=" + pEstadoPeriodo;
            wnd.vRadWindowId = "winPeriodo";
            return wnd;
        }
        <%--
        function GetNecesidadCapacitacionWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Necesidades de capacitación";
            wnd.vURL = "/FYD/NecesidadesCapacitacion2.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "winPeriodo";
            return wnd;
        }

        function GetConsultaGeneralWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Consulta General";
            wnd.vURL = "/FYD/ConsultasGenerales.aspx?IdPeriodo=" + pIdPeriodo;
            wnd.vRadWindowId = "rwReportes";
            return wnd;
        }

        function GetConsultaIndividualWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Consulta Individual";
            wnd.vURL = "/FYD/ConsultasIndividuales.aspx?IdPeriodo=" + pIdPeriodo;
            wnd.vRadWindowId = "rwReportes";
            return wnd;
        }
        --%>
        function GetEnvioSolicitudesWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.width = 900;
            wnd.height = 700;
            wnd.vTitulo = "Envío de Cuestionarios";
            wnd.vURL = "/EO/VentanaEnvioSolicitudesCuestionario.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "rwReportes";
            return wnd;
        }
        
        function GetPeriodoId() {
            var listView = $find('<%= rlvPeriodos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["ID_PERIODO"];
            else
                return null;
        }

        function GetPeriodoNombre() {
            var listView = $find('<%= rlvPeriodos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["NB_PERIODO"];
            else
                return null;
        }
        
        function GetPeriodoEstado() {
            var listView = $find('<%= rlvPeriodos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["CL_ESTADO_PERIODO"];
            else
                return null;
        }
        
        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }
        
        function OpenControlAvanceWindow(sender, args) {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetControlAvanceWindowProperties(vIdPeriodo));
            else
                radalert("Selecciona un periodo.", 400, 150);
        }
        <%--
        function OpenNecesidadCapacitacionWindow(sender, args) {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetNecesidadCapacitacionWindowProperties(vIdPeriodo));
            else
                radalert("Selecciona un periodo.", 400, 150);
        }

        function OpenConfiguracionWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null)
                OpenWindow(GetConfiguracionWindowProperties(vIdPeriodo));
            else
                radalert("Selecciona un periodo.", 400, 150);
        }

        function OpenConsultaGeneralWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null) {
                OpenWindow(GetConsultaGeneralWindowProperties(vIdPeriodo));
            }
            else {
                radalert("Selecciona un periodo.", 400, 150);
            }
        }

        function OpenConsultaIndividualWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null) {
                OpenWindow(GetConsultaIndividualWindowProperties(vIdPeriodo));
            }
            else {
                radalert("Selecciona un periodo.", 400, 150);
            }
        }
        --%>
        function OpenEnvioSolicitudesWindow() {
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null) {
                OpenWindow(GetEnvioSolicitudesWindowProperties(vIdPeriodo));
            }
            else {
                radalert("Selecciona un periodo.", 400, 150);
            }
        }
        <%--
        function OpenInsertPeriodoWindow() {
            OpenPeriodoWindow(null, "INSERTA");
        }

        function OpenEditPeriodoWindow() {
            OpenPeriodoWindow(GetPeriodoId(), "EDITA");
        }

        function AbrirCopiarPeriodo(pIdPeriodo) {
            if (pIdPeriodo != null) {
                OpenPeriodoWindow(pIdPeriodo, "COPIA")
            }
        }

        function OpenPeriodoWindow(pIdPeriodo, pTipoTarea) {
            OpenWindow(GetPeriodoWindowProperties(pIdPeriodo, pTipoTarea));
        }--%>

        function OpenContestarCuestionariosWindow(sender, args) {
            var vIdPeriodo = GetPeriodoId();
            var vNbPeriodo = GetPeriodoNombre();
            var vEstadoPeriodo = GetPeriodoEstado();
            if (vIdPeriodo != null) {
                OpenWindow(GetContestarCuestionariosWindowProperties(vIdPeriodo, vNbPeriodo, vEstadoPeriodo));
            } else {
                radalert("Selecciona un periodo.", 400, 150);
            }
        }

       <%-- function ConfirmarCerrar(sender, args) {
            var vIdPeriodo = GetPeriodoId();
            var vNbPeriodo = GetPeriodoNombre();

            if (vIdPeriodo != null) {

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('¿Deseas cerrar el periodo ' + vNbPeriodo + ' ?', callBackFunction, 400, 170, null, "Cerrar Periodo");
                args.set_cancel(true);
            }
            else {
                radalert("Seleccione un periodo.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function ConfirmarReactivar(sender, args) {
            var vIdPeriodo = GetPeriodoId();
            var vNbPeriodo = GetPeriodoNombre();

            if (vIdPeriodo != null) {

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('¿Deseas reactivar el periodo ' + vNbPeriodo + ' ?', callBackFunction, 400, 170, null, "Reactivar Periodo");
                args.set_cancel(true);
            }
            else {
                radalert("Seleccione un periodo.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function ConfirmarEliminar(sender, args) {
            var vIdPeriodo = GetPeriodoId();
            var vNbPeriodo = GetPeriodoNombre();

            if (vIdPeriodo != null) {

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('¿Deseas eliminar el periodo ' + vNbPeriodo + '? Esta opción eliminará todos los datos relacionados con el periodo y no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Periodo");
                args.set_cancel(true);
            }
            else {
                radalert("Seleccione un periodo.", 400, 150, "");
                args.set_cancel(true);
            }
        }--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpPeriodosEvaluacion" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramOrganigrama" runat="server" OnAjaxRequest="ramOrganigrama_AjaxRequest" DefaultLoadingPanelID="ralpPeriodosEvaluacion">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rlvPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadButton6" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" />
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadButton6" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReactivar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" />
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadButton6" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfigurar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbAscendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbDescendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rfFiltros"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
<%--            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="rsPeriodosEvaluacion" BorderSize="0" Width="100%" Height="100%" runat="server">
        <telerik:RadPane ID="rpPeriodosEvaluacion" runat="server">
            <label class="labelTitulo">Evaluación de competencias</label>
            <div class="ctrlBasico">
                <telerik:RadListView ID="rlvPeriodos" runat="server" DataKeyNames="ID_PERIODO,CL_ESTADO_PERIODO" ClientDataKeyNames="ID_PERIODO,NB_PERIODO, CL_ESTADO_PERIODO"
                    OnNeedDataSource="rlvPeriodos_NeedDataSource" OnItemCommand="rlvPeriodos_ItemCommand" AllowPaging="true" ItemPlaceholderID="ProductsHolder">

                    <LayoutTemplate>
                        <div style="overflow: auto; width: 700px;">
                            <div style="overflow: auto; overflow-y: auto; max-height: 450px;">
                                <asp:Panel ID="ProductsHolder" runat="server"></asp:Panel>
                            </div>
                            <div style="clear: both;"></div>
                            <telerik:RadDataPager ID="rdpPeriodos" runat="server" PagedControlID="rlvPeriodos" PageSize="6" Width="630">
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
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="font-size: 1.6em; width: 75px; text-align: center;"><%# Eval("ID_PERIODO") %></div>
                                            </td>
                                            <td>
                                                <div style="overflow: auto; overflow-y: auto; overflow-x: auto; height: 35px"><%# Eval("CL_PERIODO") %> </div>
                                            </td>
                                        </tr>
                                        <%--  <tr>
                                            <td><%# ((DateTime)Eval("FE_INICIO")).ToString("MMMM yyyy") %></td>
                                        </tr>--%>
                                    </table>
                                </div>
                                <div>
                                    <%# Eval("CL_ESTADO_PERIODO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
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
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="font-size: 1.6em; width: 75px; text-align: center;"><%# Eval("ID_PERIODO") %></div>
                                            </td>
                                            <td>
                                                <div style="overflow: auto; overflow-y: auto; overflow-x: auto; height: 35px"><%# Eval("CL_PERIODO") %> </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <%# Eval("CL_ESTADO_PERIODO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <%# Eval("DS_PERIODO") %>
                                </div>
                            </div>
 <%--                           <div class="SelectionOptions">
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnModificar" runat="server" Text="Editar" OnClientClicked="OpenEditPeriodoWindow" Width="100%"></telerik:RadButton>
                                </div>
                                <div style="width: 50%; float: left; padding-left: 5px;">
                                    <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100%" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
                                </div>
                            </div>--%>
                        </div>
                    </SelectedItemTemplate>
                    <EmptyDataTemplate>
                        <div class="RadListViewContainer" style="overflow: auto; text-align: center; width: 660px; height: 100px;">
                            No hay períodos disponibles
                        </div>
                    </EmptyDataTemplate>

                </telerik:RadListView>
            </div>
            <div class="ctrlBasico" style="text-align: center">
<%--                <div>
                    <label class="labelTitulo">Administrar</label>
                    <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenInsertPeriodoWindow"></telerik:RadButton>
                    &nbsp;&nbsp;
            <telerik:RadButton ID="btnConfigurar" runat="server" Text="Configurar" AutoPostBack="false" OnClientClicked="OpenConfiguracionWindow"></telerik:RadButton>
                    &nbsp;&nbsp;
            <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar periodo" OnClientClicking="ConfirmarCerrar" OnClick="btnCerrar_Click"></telerik:RadButton>
                    &nbsp;&nbsp;
            <telerik:RadButton ID="btnReactivar" runat="server" Text="Reactivar" OnClientClicking="ConfirmarReactivar" OnClick="btnReactivar_Click"></telerik:RadButton>
                </div>
                <div style="height: 20px;"></div>--%>
                <div>
                    <label class="labelTitulo">Cuestionarios</label>
                    <telerik:RadButton ID="btnEnviarSolicitudes" runat="server" Text="Envío de cuestionarios" AutoPostBack="false" OnClientClicked="OpenEnvioSolicitudesWindow"></telerik:RadButton>
                    &nbsp;&nbsp;
            <telerik:RadButton ID="btnControlAvance" runat="server" Text="Control de avance" AutoPostBack="false" OnClientClicked="OpenControlAvanceWindow"></telerik:RadButton>
                    &nbsp;&nbsp;
            <telerik:RadButton ID="btnContestarCuestionarios" runat="server" Text="Contestar" AutoPostBack="false" OnClientClicked="OpenContestarCuestionariosWindow"></telerik:RadButton>
                </div>
<%--                <div style="height: 20px;"></div>
                <div>
                    <label class="labelTitulo">Consultas</label>
                    <telerik:RadButton ID="btnIndividuales" runat="server" Text="Individuales" AutoPostBack="false" OnClientClicked="OpenConsultaIndividualWindow"></telerik:RadButton>
                    &nbsp;&nbsp;
            <telerik:RadButton ID="btnGenerales" runat="server" Text="Generales" AutoPostBack="false" OnClientClicked="OpenConsultaGeneralWindow"></telerik:RadButton>
                    &nbsp;&nbsp;
            <telerik:RadButton ID="btnNecesidadesCapacitacion" runat="server" Text="Necesidades de capacitación" AutoPostBack="false" OnClientClicked="OpenNecesidadCapacitacionWindow"></telerik:RadButton>
                </div>--%>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpOrdenarFiltrar" runat="server" Scrolling="None" Width="20px">
            <telerik:RadSlidingZone ID="rszOrdenarFiltrar" runat="server" SlideDirection="Left" ExpandedPaneId="rsPeriodosEvaluacion" Width="20px">
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
                            <telerik:RadFilter runat="server" ID="rfFiltros" ExpressionPreviewPosition="Top" ApplyButtonText="Filtrar" OnApplyExpressions="rfFiltros_ApplyExpressions">
                                <FieldEditors>
                                    <telerik:RadFilterTextFieldEditor DataType="System.Int32" DisplayName="No." FieldName="ID_PERIODO" DefaultFilterFunction="Contains" ToolTip="Numero del periodo" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Clave" FieldName="CL_PERIODO" DefaultFilterFunction="Contains" ToolTip="Clave del periodo" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Nombre" FieldName="NB_PERIODO" DefaultFilterFunction="Contains" ToolTip="Nombre del periodo" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Descripción" FieldName="DS_PERIODO" DefaultFilterFunction="Contains" ToolTip="Descripción del periodo" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.DateTime" DisplayName="F. Inicial" FieldName="FE_INICIO" DefaultFilterFunction="GreaterThanOrEqualTo" ToolTip="Fecha Inicial" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Estatus" FieldName="CL_ESTADO_PERIODO" DefaultFilterFunction="Contains" ToolTip="Estatus del periodo" />
                                </FieldEditors>
                            </telerik:RadFilter>
                        </fieldset>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <div style="clear: both;"></div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <Windows>
            <telerik:RadWindow ID="winPeriodo" runat="server" Title="Agregar/Editar período" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winAgregarCuestionario" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winMatrizCuestionarios" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwReportes" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwReporte" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEmpleado" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winDescriptivo" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winAutoriza" runat="server" Height="600" Width="1300" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winCamposAdicionales" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEdicionPorEvaluado" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winContestarCuestionarios" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winReporteJerarquico" runat="server" Title="Reporte jerárquico" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
