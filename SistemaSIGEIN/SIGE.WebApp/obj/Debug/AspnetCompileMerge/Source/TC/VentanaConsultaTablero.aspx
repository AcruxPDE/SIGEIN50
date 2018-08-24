<%@ Page Title="" Language="C#" MasterPageFile="~/TC/ContextTC.Master" AutoEventWireup="true" CodeBehind="VentanaConsultaTablero.aspx.cs" Inherits="SIGE.WebApp.TC.VentanaConsultaTablero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
        <style>
            .divRojo {
                height: 100%;
                width: 30px;
                border-radius: 5px;
                background: red;
            }

            .divAmarillo {
                height: 100%;
                width: 30px;
                border-radius: 5px;
                background: gold;
            }

            .divVerde {
                height: 100%;
                width: 30px;
                border-radius: 5px;
                background: green;
            }

            .divNa {
                height: 100%;
                width: 30px;
                border-radius: 5px;
                background: gray;
            }

            table.tablaColor {
                width: 100%;
            }

            td.porcentaje {
                width: 80%;
                height: 40%;
                padding: 1px;
            }

            td.color {
                width: 10%;
                height: 50%;
                padding: 1px;
            }

            .image_resize {
                width: auto;
                height: 150px;
            }

                .image_resize img {
                    max-width: 100%;
                    max-height: 100%;
                }
        </style>
    <script type="text/javascript">

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var windowProperties = GetWindowProperties();
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function openCuestionarioClima(pIdEvaluador, pIdPeriodo) {
            if (pIdEvaluador != "0" & pIdPeriodo != "0") {
                var vURL = "/EO/Cuestionarios/CuestionarioClimaLaboral.aspx?ID_EVALUADOR=" + pIdEvaluador + "&ID_PERIODO=" + pIdPeriodo + "&FG_HABILITADO=False";
                var vTitulo = "Cuestionario";
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                var windowProperties = {
                    width: browserWnd.innerWidth - 20,
                    height: browserWnd.innerHeight - 20
                };
                var wnd = openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
            }
            else {
                radalert("El empleado no tiene una cuestionario de clima asociado", 400, 150, "");
            }
        }


        function ConfirmarGuardar(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
            { if (shouldSubmit) { this.click(); } });

            radconfirm('Una vez guardada la consulta ya no será posible cambiar las ponderaciones y/o los comentarios. ¿Estás seguro que deseas continuar?', callBackFunction, 400, 170, null, "Guardar consulta");
            args.set_cancel(true);
        }


        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
        }

        function CrearConsulta() {
            OpenWindow(GetPeriodoWindowProperties());
        }

        function GetPeriodoWindowProperties() {
            var wnd = GetWindowProperties();
            wnd.width = 750;
            wnd.height = 580;
            wnd.vTitulo = "Nuevo periodo";
            wnd.vRadWindowId = "winDescriptivo";

            var idPuesto = '<%= vIdPuesto %>';
            var idEmpleado = '<%= vIdEmpleado %>';
            wnd.vURL = "/TC/VentanaNuevoTableroControl.aspx?idEmpleado=" + idEmpleado + "&idPuesto=" + idPuesto ;
            return wnd;
        }

        function GetWindowProperties() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            return {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
        }

        function OpenReporteIndividual(pIdEvaluado, pIdPeriodo) {
            var vIdPeriodo = pIdPeriodo;
            var vURL = "../FYD/ReporteIndividual.aspx";
            var vTitulo = "Reporte Individual";

            vURL = vURL + "?IdPeriodo=" + vIdPeriodo
            vURL = vURL + "&IdEvaluado=" + pIdEvaluado

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
        }

        function OpenReporteCumplimientoPersonal(pIdEvaluado, pIdPeriodo) {
            OpenSelectionWindow("../EO/VentanaReporteCumplimientoPersonal.aspx?idPeriodo=" + pIdPeriodo + "&idEvaluado=" + pIdEvaluado, "winCumplimientoGlobal", "Reporte cumplimiento personal")
        }

        function OpenInventario(pIdEmpleado) {
            var vURL = "../Administracion/Empleado.aspx";
            var vTitulo = "Consultar Empleado";
            vURL = vURL + "?EmpleadoId=" + pIdEmpleado + "&pFgHabilitaBotones=False";

            var windowProperties = {};
            windowProperties.width = document.documentElement.clientWidth - 20;
            windowProperties.height = document.documentElement.clientHeight - 20;
            openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
        }

        function OpenDescriptivo(pIdPuesto) {
            var vURL = "../Administracion/VentanaVistaDescriptivo.aspx";
            var vTitulo = "Ver descripción del puesto";
            vURL = vURL + "?PuestoId=" + pIdPuesto
            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
        }

        function GetCumplimientoGlobal(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vRadWindowId = "winCumplimientoGlobal";
            wnd.vURL = "../EO/VentanaCumplimientoGlobal.aspx";
            if (pIdPeriodo != null) {
                wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                wnd.vTitulo = "Cumplimiento global";
            }
            return wnd;
        }

        function GetReportesProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Reportes";
            wnd.vURL = "../EO/VentanaReportes.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "WinClimaLaboral";
            return wnd;
        }

        function GetTabuladorProperties(pIdTabulador) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Consultas";
            wnd.vURL = "../MPC/ConsultaTabuladorSueldos.aspx?ID=" + pIdTabulador + "&pOrigen=TableroControl";
            wnd.vRadWindowId = "WinTabuladores";
            return wnd;
        }

        function GetConsultaGeneral(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Consulta General";
            wnd.vURL = "/FYD/ConsultasGenerales.aspx?IdPeriodo=" + pIdPeriodo;
            wnd.vRadWindowId = "winDescriptivo";
            return wnd;
        }

        function openComparativaPuesto(pIdCandidato, pIdPuesto) {
            if (pIdCandidato != null && pIdPuesto != null)
                OpenSelectionWindow("../IDP/VentanaCandidatoVsPuestos.aspx?pIdPuestoTablero=" + pIdPuesto + "&IdCandidato=" + pIdCandidato, "winDescriptivo", "Comparativa puesto vs candidato")
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function OpenPeriodoFyD(pIdPeriodo) {
            if (pIdPeriodo != null) {
                OpenWindow(GetConsultaGeneral(pIdPeriodo));
            }
        }

        function OpenPeriodoED(pIdPeriodo) {
            if (pIdPeriodo != null)
                OpenWindow(GetCumplimientoGlobal(pIdPeriodo));
        }

        function OpenPeriodoCL(pIdPeriodo) {
            if (pIdPeriodo != null)
                OpenWindow(GetReportesProperties(pIdPeriodo));
        }

        function OpenTabulador(pIdTabulador) {
            if (pIdTabulador != null)
                OpenWindow(GetTabuladorProperties(pIdTabulador));
        }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <telerik:RadAjaxLoadingPanel ID="ralpTableroControl" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramTableroControl" runat="server" DefaultLoadingPanelID="ralpTableroControl">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnRecalcular">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTableroControl" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="btnGuardarPrograma">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTableroControl" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTableroControl" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnGuardar" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rcbComentarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTableroControl" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="rsPlantilla" Width="100%" Height="100%" BorderSize="0" runat="server">
        <telerik:RadPane ID="rpGridEvaluados" runat="server" Height="100%" ShowContentDuringLoad="false">
            <div style="width: 100%; height: calc(100% - 40px);">
                <telerik:RadGrid ID="grdTableroControl" HeaderStyle-Font-Bold="true" 
                                 runat="server" Height="100%" AutoGenerateColumns="true" OnNeedDataSource="grdTableroControl_NeedDataSource" OnColumnCreated="grdTableroControl_ColumnCreated"  RenderMode="Lightweight" >
                    <ClientSettings >
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" SaveScrollPosition="true" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="NB_EMPLEADO_PUESTO, ID_EVALUADO" EnableColumnsViewState="false" OnPreRender="Unnamed_PreRender" DataKeyNames="NB_EMPLEADO_PUESTO, ID_EVALUADO" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <ColumnGroups  >
                             <telerik:GridColumnGroup Name="Pruebas" HeaderStyle-Height="200" HeaderStyle-Font-Bold="true"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="Green" HeaderText="EVALUACIÓN DE PRUEBAS" />
                            <telerik:GridColumnGroup Name="Formacion" HeaderStyle-Height="200" HeaderStyle-Font-Bold="true"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="DarkOrange" HeaderText="EVALUACIÓN DE COMPETENCIAS" />
                             <telerik:GridColumnGroup Name="Desempeno" HeaderStyle-Font-Bold="true"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="200" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="DarkRed" HeaderText="EVALUACIÓN DE DESEMPEÑO"/>
                             <telerik:GridColumnGroup Name="Clima" HeaderStyle-Font-Bold="true"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="200" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="DarkRed" HeaderText="CLIMA LABORAL" />
                             <telerik:GridColumnGroup Name="Tabulador" HeaderStyle-Font-Bold="true"
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="200" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#0066ff" HeaderText="EVALUACIÓN DEL SUELDO" />
                            <telerik:GridColumnGroup Name="Tendencia" HeaderText="Tendencia" HeaderStyle-Font-Bold="true"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="200" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="DarkGray" />
                        </ColumnGroups>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="DS_COMENTARIO" HeaderStyle-BackColor="DarkGray" HeaderStyle-ForeColor="White" DataField="DS_COMENTARIO" HeaderStyle-Width="150" HeaderText="Comentarios">
                                <ItemTemplate>
                                    <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="txtComentarios" Text='<%#Eval ("DS_COMENTARIOS")%>' Width="125" Height="100" Rows="5" TextMode="MultiLine"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            <//div>
            <div style="height: 5px; clear:both;"></div>
            <div class="divControlDerecha">
                <telerik:RadButton runat="server" ID="btnGuardarConsulta" UseSubmitBehavior="false" Text="Guardar" AutoPostBack="false" OnClientClicked="CrearConsulta"  ></telerik:RadButton>
            </div>
            <div class="divControlDerecha">
                <telerik:RadButton runat="server" ID="btnGuardar" UseSubmitBehavior="false" Text="Guardar" AutoPostBack="true" OnClientClicking="ConfirmarGuardar"  OnClick="btnGuardar_Click"></telerik:RadButton>
            </div>
<%--            <div class="divControlDerecha" style="margin-right:20px;">
                <telerik:RadCheckBox runat="server" ID="rcbComentarios" Text="Agregar/Mostrar comentarios" AutoPostBack="true" OnCheckedChanged="rcbComentarios_CheckedChanged"></telerik:RadCheckBox>
            </div>--%>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="22px" ShowContentDuringLoad="false">
                <telerik:RadSlidingZone ID="rszPrograma" runat="server" SlideDirection="Left" Width="22px">
                    <telerik:RadSlidingPane ID="rspNuevoPrograma" runat="server" Title="Configuración de tablero" Width="240px" RenderMode="Mobile" Height="200">
                        <div style="margin-left:10px;">
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label>Resultados de pruebas %:</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadNumericTextBox runat="server" ID="txtIdp" Width="150px" MinValue="0.00" MaxValue="100.00" MaxLength="6" NumberFormat-DecimalDigits="2" AutoPostBack="false"></telerik:RadNumericTextBox>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <label>Evaluación de competencias %</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadNumericTextBox runat="server" ID="txtFyd" Width="150px" MinValue="0.00"  MaxValue="100.00" MaxLength="6" NumberFormat-DecimalDigits="2" AutoPostBack="false"></telerik:RadNumericTextBox>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <label>Evaluación de desempeño %</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadNumericTextBox runat="server" ID="txtDesempeno" MinValue="0.00" Width="150px" MaxLength="6"  MaxValue="100.00" NumberFormat-DecimalDigits="2" AutoPostBack="false"></telerik:RadNumericTextBox>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <label>Clima laboral %</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadNumericTextBox runat="server" ID="txtClima" MinValue="0.00" MaxValue="100.00" MaxLength="6" Width="150px" NumberFormat-DecimalDigits="2"  AutoPostBack="false"></telerik:RadNumericTextBox>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton runat="server" ID="btnGuardarPrograma" Text="Cambiar ponderación" AutoPostBack="true" OnClick="btnGuardarPrograma_Click"></telerik:RadButton>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton runat="server" ID="btnRecalcular" Text="Recalcular" AutoPostBack="true" OnClick="btnRecalcular_Click"  ToolTip="Esta opción te permite calcular las ponderaciones modificadas."></telerik:RadButton>
                        </div>
                            </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
    </telerik:RadSplitter>
     <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>

