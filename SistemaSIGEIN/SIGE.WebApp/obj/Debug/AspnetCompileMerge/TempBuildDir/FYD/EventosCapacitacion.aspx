<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="EventosCapacitacion.aspx.cs" Inherits="SIGE.WebApp.FYD.EventosCapacitacion" %>

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
            var lista = $find("<%# rlvEventos.ClientID %>");
            lista.rebind();
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 40,
                height: document.documentElement.clientHeight - 40
            };
        }

        function GetEventoWindowProperties(pIdEvento) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Agregar evento";
            wnd.vRadWindowId = "winEvento";
            wnd.vURL = "VentanaEventoCapacitacion.aspx";
            if (pIdEvento != null) {
                wnd.vURL += String.format("?EventoId={0}", pIdEvento);
                wnd.vTitulo = "Editar evento";
            }
            return wnd;
        }

        function GetCopiaEventoWindowProperties(pIdEvento) {
            var wnd = GetWindowProperties();

            //wnd.vTitulo = "Agregar evento";
            wnd.vRadWindowId = "winEvento";
            wnd.vURL = "VentanaEventoCapacitacion.aspx";
            if (pIdEvento != null) {
                wnd.vURL += String.format("?EventoIdCopia={0}", pIdEvento);
                wnd.vTitulo = "Agregar evento";
            }
            return wnd;
        }

        function GetCalendarioWindowProperties(pIdEvento) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Consulta Calendario Eventos de Capacitación";
            wnd.vRadWindowId = "winEvento";
            wnd.vURL = "VentanaCalendarioCurso.aspx";
            if (pIdEvento != null) {
                wnd.vURL += String.format("?IdEvento={0}", pIdEvento);
            }

            return wnd;
        }

        function GetAsistenciaWindowProperties(pIdEvento) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Captura de asistencia";
            wnd.vRadWindowId = "winEvento";
            wnd.vURL = "VentanaCapturaAsistenciaEvento.aspx";
            if (pIdEvento != null) {
                wnd.vURL += String.format("?IdEvento={0}", pIdEvento);
            }

            return wnd;
        }

        function GetEnvioCorreosParticipantesWindowProperties(pIdEvento) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Envío de Invitación para Evento de Capacitación por Correo Electónico";
            wnd.vRadWindowId = "winEvento";
            wnd.vURL = "VentanaEventoEnvioCorreoParticipante.aspx";
            if (pIdEvento != null) {
                wnd.vURL += String.format("?IdEvento={0}", pIdEvento);
            }

            return wnd;
        }

        function GetEnvioCorreoEvaluadorWindowProperties(pIdEvento) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Evaluación de Resultados";
            wnd.vRadWindowId = "winEvento";
            wnd.vURL = "VentanaEventoEnvioCorreoEvaluador.aspx";
            if (pIdEvento != null) {
                wnd.vURL += String.format("?IdEvento={0}", pIdEvento);
            }

            return wnd;
        }

        function GetEvaluacionResultadosWindowProperties(pIdEvento) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Evaluación del participante";
            wnd.vRadWindowId = "winEvento";
            wnd.vURL = "EvaluacionCompetencia/VentanaEventoEvaluacionResultados.aspx";
            if (pIdEvento != null) {
                wnd.vURL += String.format("?IdEvento={0}", pIdEvento);
            }

            return wnd;
        }

        function GetReporteEvaluacionResultadosWindowProperties(pIdEvento) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Consulta Resultados Eventos de Capacitación";
            wnd.vRadWindowId = "winEvento";
            wnd.vURL = "VentanaEventoReporteParticipacion.aspx";
            if (pIdEvento != null) {
                wnd.vURL += String.format("?IdEvento={0}", pIdEvento);
            }

            return wnd;
        }

        function GetEventoId() {
            var listView = $find('<%= rlvEventos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["ID_EVENTO"];
            else
                return null;
        }
        function GetEventoNombre() {
            var listView = $find('<%= rlvEventos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["NB_EVENTO"];
            else
                return null;
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function OpenInsertEventoWindow() {
            OpenEventoWindow(null);
        }

        function OpenEditEventoWindow() {
            if ('<%= vEditar %>' == "True") {
                OpenEventoWindow(GetEventoId());
            }
            else {
                radalert("No tiene los permisos necesarios para llevar a cabo esta función.", 450, 200, "");
            }
        }

        function OpenEventoWindow(pIdEvento) {
            OpenWindow(GetEventoWindowProperties(pIdEvento));
        }

        function OpenCopyEventoWindow(pIdEvento) {
            var idEvento = GetEventoId();

            if (idEvento != null) {
                OpenWindow(GetCopiaEventoWindowProperties(idEvento));
            }
            else {
                radalert("Selecciona un evento.", 400, 150);
            }
        }

        function OpenCalendarioWindow() {
            var idEvento = GetEventoId();

            if (idEvento != null) {
                OpenWindow(GetCalendarioWindowProperties(idEvento));
            }
            else {
                radalert("Selecciona un evento.", 400, 150);
            }
        }

        function OpenAsistenciaWindow() {
            var idEvento = GetEventoId();

            if (idEvento != null) {
                OpenWindow(GetAsistenciaWindowProperties(idEvento));
            }
            else {
                radalert("Selecciona un evento.", 400, 150);
            }
        }

        function OpenEnvioCorreoParticipantesWindow() {
            var idEvento = GetEventoId();

            if (idEvento != null) {
                OpenWindow(GetEnvioCorreosParticipantesWindowProperties(idEvento));
            }
            else {
                radalert("Selecciona un evento.", 400, 150);
            }
        }

        function OpenEnvioCorreoEvaluadorWindow() {
            var idEvento = GetEventoId();

            if (idEvento != null) {
                OpenWindow(GetEnvioCorreoEvaluadorWindowProperties(idEvento));
            }
            else {
                radalert("Selecciona un evento.", 400, 150);
            }
        }

        function OpenEvaluacionResultadosWindow() {
            var idEvento = GetEventoId();

            if (idEvento != null) {
                OpenWindow(GetEvaluacionResultadosWindowProperties(idEvento));
            }
            else {
                radalert("Selecciona un evento.", 400, 150);
            }
        }

        function OpenReporteEvaluacionWindow() {
            var idEvento = GetEventoId();

            if (idEvento != null) {
                OpenWindow(GetReporteEvaluacionResultadosWindowProperties(idEvento));
            }
            else {
                radalert("Selecciona un evento.", 400, 150);
            }
        }

        function ConfirmarEliminar(sender, args) {
            if ('<%= vEliminar %>' == "True") {
            var idEvento = GetEventoId();
            var nbEvento = "";

            if (idEvento != null) {

                nbEvento = GetEventoNombre();

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });
                radconfirm("¿Deseas eliminar el evento " + nbEvento + '?, este proceso no podrá revertirse.', callBackFunction, 400, 200, null, "Eliminar Registro");
                args.set_cancel(true);

            } else {
                radalert("Seleccione un evento.", 400, 150, "Error");
                args.set_cancel(true);
            }
            }
            else {
                radalert("No tiene los permisos necesarios para llevar a cabo esta función.", 450, 200, "");
                args.set_cancel(true);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpEventos" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramEventos" runat="server" DefaultLoadingPanelID="ralpEventos">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rfFiltros" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvEventos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbAscendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvEventos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbDescendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvEventos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rlvEventos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtNbEvento" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtDescripcion" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtEstado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtCurso" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtFechaMod" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtUsuarioMod" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rlvEventos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarEvento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvEventos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="rsEventosCapacitacion" BorderSize="0" Width="100%" Height="100%" runat="server">
        <telerik:RadPane ID="rpEventosCapacitacion" runat="server">
            <label class="labelTitulo">Eventos de capacitación</label>
            <div class="ctrlBasico">
                <telerik:RadListView ID="rlvEventos" runat="server" DataKeyNames="ID_EVENTO" ClientDataKeyNames="ID_EVENTO,NB_EVENTO" OnNeedDataSource="rlvEventos_NeedDataSource" OnItemCommand="rlvEventos_ItemCommand" AllowPaging="true" ItemPlaceholderID="EventosHolder">
                    <LayoutTemplate>
                        <div style="overflow: auto; width: 660px;">
                            <div style="overflow: auto; overflow-y: auto; max-height: 600px;">
                                <asp:Panel ID="EventosHolder" runat="server"></asp:Panel>
                            </div>
                            <div style="clear: both;"></div>
                            <telerik:RadDataPager ID="rdpEventos" runat="server" PagedControlID="rlvEventos" PageSize="6" Width="630">
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
                            <div style="padding: 10px;" >
                                <div style="overflow: auto; overflow-y: auto; height: 30px;">
                                    <label>Clave:</label>
                                    <%# Eval("CL_EVENTO") %>
                                </div>
                                <div>
                                    <label>Tipo:</label>
                                    <%# Eval("CL_TIPO_CURSO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label>Descripción:</label>
                                    <%# Eval("NB_EVENTO") %>
                                </div>
                                <%-- <div style="text-align: center;">
                                    <h4><# Eval("CL_EVENTO") %></h4>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <# Eval("NB_EVENTO") %>
                                </div>
                                <div>
                                    <# Eval("CL_TIPO_CURSO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <# Eval("NB_CURSO") %>
                                </div>--%>
                            </div>
                            <div class="SelectionOptions">
                                <telerik:RadButton ID="btnSeleccionar" runat="server" CommandName="Select" Text="Seleccionar" Width="100%"></telerik:RadButton>
                            </div>
                        </div>
                    </ItemTemplate>
                    <SelectedItemTemplate>
                        <div class="RadListViewContainer Selected">
                            <div style="padding: 10px;">
                                <div style="overflow: auto; overflow-y: auto; height: 30px;">
                                    <label style="color: white;">Clave:</label>
                                    <%# Eval("CL_EVENTO") %>
                                </div>
                                <div>
                                    <label style="color: white;">Tipo:</label>
                                    <%# Eval("CL_TIPO_CURSO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label style="color: white;">Descripción:</label>
                                    <%# Eval("NB_EVENTO") %>
                                </div>
                                <%--  <div style="text-align: center;">
                                    <h4><# Eval("CL_EVENTO") %></h4>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <# Eval("NB_EVENTO") %>
                                </div>
                                <div>
                                    <# Eval("CL_TIPO_CURSO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <# Eval("NB_CURSO") %>
                                </div>--%>
                            </div>
                            <div class="SelectionOptions">
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnModificar" runat="server" Width="100%" OnClientClicked="OpenEditEventoWindow" Text="Editar"></telerik:RadButton>
                                </div>
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnEliminar" runat="server" Width="100%" Text="Eliminar" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </SelectedItemTemplate>
                    <EmptyDataTemplate>
                        <div class="RadListViewContainer" style="overflow: auto; text-align: center; width: 660px; height: 100px;">
                            No hay eventos disponibles
                        </div>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
            <div class="ctrlBasico" style="text-align: left; padding-left: 15px;">
                <%-- <div class="ctrlBasico">
                    <label style="width: 120px;" id="lblEvento" name="lblEvento" runat="server">Evento:</label>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNbEvento" Enabled="false" runat="server" Width="300px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <label style="width: 120px;" id="lblDescripcion" name="lblDescripcion" runat="server">Descripción:</label>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtDescripcion" Enabled="false" runat="server" Width="300px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <label style="width: 120px;" id="lblEstado" name="lblEstado" runat="server">Estado:</label>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtEstado" Enabled="false" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <label style="width: 120px;" id="lblCurso" name="lblCurso" runat="server">Curso:</label>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtCurso" Enabled="false" runat="server" Width="300px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
                  <div style="clear: both;"></div>
                       <div class="ctrlBasico">
                    <label style="width: 120px;" id="Label1" name="lblCurso" runat="server">Último usuario que modifica:</label>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtUsuarioMod" Enabled="false" runat="server" Width="110px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
                                <div class="ctrlBasico">
                    <label style="width: 120px;" id="Label2" name="lblCurso" runat="server">Última fecha de modificación:</label>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtFechaMod" Enabled="false" runat="server" Width="90px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 10px;"></div>--%>
                <telerik:RadTabStrip runat="server" ID="rtsEventos" MultiPageID="rmpEventos" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Gestionar"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Detalles"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: 10px;"></div>
                <telerik:RadMultiPage runat="server" ID="rmpEventos" SelectedIndex="0" Height="100%" Width="100%">
                    <telerik:RadPageView ID="rpvGestionar" runat="server">
                        <div class="ctrlBasico" style="text-align: center">
                            <div>
                                <label class="labelTitulo" style="text-align: center;">Administrar</label>
                                <telerik:RadButton runat="server" ID="btnAgregarEvento" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenInsertEventoWindow"></telerik:RadButton>
                                &nbsp;&nbsp;
                        <telerik:RadButton ID="btnCopiar" runat="server" Text="Copiar de" AutoPostBack="false" OnClientClicked="OpenCopyEventoWindow"></telerik:RadButton>
                            </div>
                            <div style="clear: both; height: 10px;"></div>
                            <div>
                                <label class="labelTitulo" style="text-align: center;">Eventos</label>
                                <telerik:RadButton ID="btnEnvioCorreo" runat="server" Text="Envío de correos a participantes" AutoPostBack="false" OnClientClicked="OpenEnvioCorreoParticipantesWindow"></telerik:RadButton>
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="btnEnvioCorreoEvaluador" runat="server" Text="Envío de correos a evaluadores" AutoPostBack="false" OnClientClicked="OpenEnvioCorreoEvaluadorWindow"></telerik:RadButton>
                                <div style="clear: both; height: 10px;"></div>
                                <telerik:RadButton ID="btnCapturaAsistencia" runat="server" Text="Captura de asistencia" AutoPostBack="false" OnClientClicked="OpenAsistenciaWindow"></telerik:RadButton>
                                &nbsp;&nbsp;
                        <telerik:RadButton ID="btnEvaluacionResultados" runat="server" Text="Evaluación del participante" AutoPostBack="false" OnClientClicked="OpenEvaluacionResultadosWindow"></telerik:RadButton>
                            </div>
                            <div style="clear: both; height: 10px;"></div>
                            <div>
                                <label class="labelTitulo" style="text-align: center;">Consultas</label>
                                <telerik:RadButton ID="btnCalendario" runat="server" Text="Calendario" AutoPostBack="false" OnClientClicked="OpenCalendarioWindow"></telerik:RadButton>
                                &nbsp;&nbsp;
                         <telerik:RadButton ID="btnListaAsistencia" runat="server" Text="Lista de asistencia" OnClick="btnListaAsistencia_Click"></telerik:RadButton>
                                &nbsp;&nbsp;                   
                        <telerik:RadButton ID="btnReporteResultados" runat="server" Text="Resultados evento" AutoPostBack="false" OnClientClicked="OpenReporteEvaluacionWindow"></telerik:RadButton>
                            </div>
                            <%--                <div style="clear: both; height: 10px;"></div>
                <div>
                    <%--<div class="ctrlBasico">
                        <telerik:RadButton ID="RadButton5" runat="server" Width="250" Text="Evaluación del evento" AutoPostBack="false"></telerik:RadButton>
                    </div>
                </div>--%>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvInformacion" runat="server">
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="lblEvento" name="lblEvento" runat="server">Evento:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtNbEvento" Enabled="false" runat="server" Width="330px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="lblDescripcion" name="lblDescripcion" runat="server">Descripción:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtDescripcion" Enabled="false" runat="server" Width="330px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="lblEstado" name="lblEstado" runat="server">Estatus:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtEstado" Enabled="false" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="lblCurso" name="lblCurso" runat="server">Curso:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtCurso" Enabled="false" runat="server" Width="330px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label1" name="lblCurso" runat="server">Último usuario que modifica:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtUsuarioMod" Enabled="false" runat="server" Width="110px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label2" name="lblCurso" runat="server">Última fecha de modificación:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtFechaMod" Enabled="false" runat="server" Width="90px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpOrdenarFiltrar" runat="server" Scrolling="None" Width="20px">
            <telerik:RadSlidingZone ID="rszOrdenarFiltrar" runat="server" SlideDirection="Left" ExpandedPaneId="rsEventosCapacitacion" Width="20px">
                <telerik:RadSlidingPane ID="rspOrdenarFiltrar" runat="server" Title="Ordenar y filtrar" Width="450px" RenderMode="Mobile" Height="100%">
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <fieldset>
                            <legend>Ordenar por:</legend>
                            <telerik:RadComboBox ID="cmbOrdenamiento" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Clave del evento" Value="CL_EVENTO" />
                                    <telerik:RadComboBoxItem Text="Nombre del evento" Value="NB_EVENTO" />
                                    <telerik:RadComboBoxItem Text="Estatus" Value="CL_ESTADO" />
                                    <telerik:RadComboBoxItem Text="Nombre del curso" Value="NB_CURSO" />
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
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Clave" FieldName="CL_EVENTO" DefaultFilterFunction="Contains" ToolTip="Clave del evento" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Nombre" FieldName="NB_EVENTO" DefaultFilterFunction="Contains" ToolTip="Nombre del evento" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Descripción" FieldName="DS_EVENTO" DefaultFilterFunction="Contains" ToolTip="Descripción del evento" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.DateTime" DisplayName="F. Inicial" FieldName="FE_INICIO" DefaultFilterFunction="GreaterThanOrEqualTo" ToolTip="Fecha Inicial" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.DateTime" DisplayName="F. Final" FieldName="FE_TERMINO" DefaultFilterFunction="LessThanOrEqualTo" ToolTip="Fecha Final" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Curso" FieldName="NB_CURSO" DefaultFilterFunction="Contains" ToolTip="Nombre del curso" />
                                    <telerik:RadFilterTextFieldEditor DataType="System.String" DisplayName="Instructor" FieldName="NB_INSTRUCTOR" DefaultFilterFunction="Contains" ToolTip="Nombre del instructor" />
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
            <telerik:RadWindow ID="winEvento" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
