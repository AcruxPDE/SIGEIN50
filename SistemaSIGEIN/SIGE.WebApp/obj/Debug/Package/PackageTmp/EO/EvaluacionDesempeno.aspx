﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EO/MenuEO.master" AutoEventWireup="true" CodeBehind="EvaluacionDesempeno.aspx.cs" Inherits="SIGE.WebApp.EO.DesempenoInicio" %>

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
        function onCloseWindow(sender, args) {
            var lista = $find("<%# rlvPeriodos.ClientID %>");
            lista.rebind();
        }

        function GetPeriodoId() {
            var listView = $find('<%= rlvPeriodos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["ID_PERIODO"];
            else
                return null;
        }

        function GetClOrigenCuestionatio() {
            var listView = $find('<%= rlvPeriodos.ClientID%>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0) 
                return listView.get_clientDataKeyValue()[selectedIndex]["CL_ORIGEN_CUESTIONARIO"];
                else
                return null;
        }

        function GetNoReplica() {
            var listView = $find('<%= rlvPeriodos.ClientID%>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["NO_REPLICA"];
            else
                return null;
        }

       /* function GetClCopia() {
            var listView = $find('<= rlvPeriodos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["CL_TIPO_COPIA"];
            else
                return null;
        }*/
        

            function GetPeriodoNombre() {

                var listView = $find('<%= rlvPeriodos.ClientID %>');
                var selectedIndex = listView.get_selectedIndexes();
                if (selectedIndex.length > 0)
                    return listView.get_clientDataKeyValue()[selectedIndex]["NB_PERIODO"];
                else
                    return null;
            }

            function GetWindowProperties() {
                return {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };
            }

            function OpenWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
            }

            function GetPeriodoWindowProperties(pIdPeriodo) {
                //var wnd = GetWindowProperties();
                //wnd.width = 750;
                //wnd.height = 640;
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                var wnd = {
                    width: browserWnd.innerWidth - 600,
                    height: browserWnd.innerHeight - 20
                };
                wnd.vRadWindowId = "WinPeriodoDesempeno";
                wnd.vURL = "VentanaPeriodoDesempeno.aspx";
                wnd.vTitulo = "Agregar período";
                if (pIdPeriodo != null) {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    wnd.vTitulo = "Editar período";
                }
                return wnd;
            }

            function GetCopiaWindowProperties(pIdPeriodo) {
                //var wnd = GetWindowProperties();
                //wnd.width = 760;
                //wnd.height = 640;
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                var wnd = {
                    width: browserWnd.innerWidth - 600,
                    height: browserWnd.innerHeight - 10
                };
                wnd.vTitulo = "Copiar período";
                wnd.vURL = "VentanaPeriodoDesempeno.aspx?PeriodoId=" + pIdPeriodo + "&Tipo=COPIA";
                wnd.vRadWindowId = "WinPeriodoDesempeno";
                return wnd;
            }

            function OpenCopiaPeriodoWindow(pIdPeriodo) {
                //var vIdPeriodo = GetPeriodoId();
                if (pIdPeriodo != null)
                    OpenWindow(GetCopiaWindowProperties(pIdPeriodo));
                else
                    radalert("Selecciona un período.", 400, 150);
            }

            function GetReplicaWindowProperties(pIdPeriodo) {
                //var wnd = GetWindowProperties();
                //wnd.width = 800;
                //wnd.height = 570;
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                var wnd = {
                    width: browserWnd.innerWidth - 400,
                    height: browserWnd.innerHeight - 20
                };
                wnd.vTitulo = "Replicar período";
                wnd.vURL = "VentanaPeriodoDesempenoReplica.aspx?PeriodoId=" + pIdPeriodo + "&Tipo=REPLICA";
                wnd.vRadWindowId = "WinPeriodoReplica";
                return wnd;
            }

            function OpenReplicaPeriodoWindow(IdPeriodo) {
                if (IdPeriodo != null)
                    OpenWindow(GetReplicaWindowProperties(IdPeriodo));
            }

            function GetConfiguracionPeriodoProperties(pIdPeriodo) {
                var wnd = GetWindowProperties();
                //wnd.width = 910;
                //wnd.height = 680;
                wnd.vRadWindowId = "WinPeriodoConfiguracion";
                wnd.vURL = "VentanaConfigurarDesempeno.aspx";
                if (pIdPeriodo != null) {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    wnd.vTitulo = "Configurar período";
                }
                return wnd;
            }

            function GetCapturarResultadosPeriodoProperties(pIdPeriodo) {
                var wnd = GetWindowProperties();
                //wnd.width = 910;
                //wnd.height = 680;
                wnd.vRadWindowId = "WinCapturarResultados";
                wnd.vURL = "VentanaCapturarResultados.aspx";
                if (pIdPeriodo != null) {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    wnd.vTitulo = "Captura de evaluaciones";
                }
                return wnd;
            }

            function GetCumplimientoPersonal(pIdPeriodo) {
                var wnd = GetWindowProperties();
                //wnd.width = 910;
                //wnd.height = 680;
                wnd.vRadWindowId = "winCumplimientoPersonal";
                wnd.vURL = "VentanaCumplimientoPersonal.aspx";
                if (pIdPeriodo != null) {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    wnd.vTitulo = "Consulta Individual - Evaluación del desempeño";
                }
                return wnd;
            }

            function GetCumplimientoGlobal(pIdPeriodo) {
                var wnd = GetWindowProperties();
                wnd.vRadWindowId = "winCumplimientoGlobal";
                wnd.vURL = "VentanaCumplimientoGlobal.aspx";
                if (pIdPeriodo != null) {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    //+String.format("&TipoCopia={0}", pClCopia);
                    wnd.vTitulo = "Consulta General - Evaluación del desempeño";
                }
                return wnd;
            }

            //function GetCumplimientoGlobalConsecuente(pIdPeriodo, pClCopia) {
            //    var wnd = GetWindowProperties();
            //    wnd.vRadWindowId = "winCumplimientoGlobal";
            //    wnd.vURL = "VentanaCumplimientoGlobalConsecuente.aspx";
            //    if (pIdPeriodo != null) {
            //        wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
            //        wnd.vTitulo = "Cumplimiento global periodos consecuentes";
            //    }
            //    return wnd;
            //}

            function OpenEnvioSolicitudes() {
                var vIdPeriodo = GetPeriodoId();
                var vClOrigen = GetClOrigenCuestionatio();
                var vNoReplica = GetNoReplica();
                if (vIdPeriodo != null) {
                    if (vClOrigen != "REPLICA" && vNoReplica == 0) {
                        OpenWindow(GetEnvioSolicitudesProperties(vIdPeriodo));
                    }
                    else {
                        OpenWindow(GetEnvioSolicitudesReplicas(vIdPeriodo));
                    }
                }
                else {
                    radalert("Selecciona un período.", 400, 150);
                }
            }

            function GetEnvioSolicitudesProperties(pIdPeriodo) {
                var wnd = GetWindowProperties();
                wnd.vRadWindowId = "WinEnvioSolicitudes";
                wnd.vURL = "VentanaEnvioSolicitudes.aspx";
                if (pIdPeriodo != null) {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    wnd.vTitulo = "Envío de solicitudes";
                }
                return wnd;
            }

            function GetEnvioSolicitudesReplicas(pIdPeriodo) {
                //var wnd = GetWindowProperties();
                //wnd.width = 450;
                //wnd.height = 550;
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                var wnd = {
                    width: browserWnd.innerWidth - 800,
                    height: browserWnd.innerHeight - 20
                };
                wnd.vRadWindowId = "WinPeriodoReplica";
                wnd.vURL = "VentanaEnvioSolicitudesReplica.aspx";
                if (pIdPeriodo != null) {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    wnd.vTitulo = "Envío de solicitudes";
                }
                return wnd;
            }

            function OpenConfigurarPeriodoWindow() {
                var vIdPeriodo = GetPeriodoId();
                if (vIdPeriodo != null)
                    OpenWindow(GetConfiguracionPeriodoProperties(vIdPeriodo));
                else
                    radalert("Selecciona un período.", 400, 150);
            }

            function OpenInsertPeriodoWindow() {
                //OpenPeriodoWindow(null);
                OpenWindow(GetPeriodoWindowProperties(null));
            }

            function OpenEditPeriodoWindow() {
                var vIdPeriodo = GetPeriodoId();

                if (vIdPeriodo != null) {
                    OpenWindow(GetPeriodoWindowProperties(vIdPeriodo));
                }
                else {
                    radalert("Selecciona un período.", 400, 150);
                }
            }

            function ConfirmarCerrar(sender, args) {
                var vIdPeriodo = GetPeriodoId();
                var vNbPeriodo = GetPeriodoNombre();
                if (vIdPeriodo != null) {

                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas cerrar el período ' + vNbPeriodo + ' ?', callBackFunction, 400, 170, null, "Cerrar período");
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

                    radconfirm('¿Deseas reactivar el período ' + vNbPeriodo + ' ?', callBackFunction, 400, 170, null, "Reactivar período");
                    args.set_cancel(true);
                }
                else {
                    radalert("Seleccione un período.", 400, 150, "");
                    args.set_cancel(true);
                }
            }


            function confirmarEliminar(sender, args) {
                var vNbPeriodo = GetPeriodoNombre();
                var listView = $find('<%= rlvPeriodos.ClientID %>');
                var selectedIndex = listView.get_selectedIndexes();
                if (selectedIndex.length == 1) {
                    var vWindowsProperties = {
                        height: 200
                    };
                    confirmAction(sender, args, "¿Deseas eliminar el período " + vNbPeriodo + "?, este proceso no podrá revertirse");
                }
                else {
                    radalert("Selecciona un período.", 400, 150);
                    args.set_cancel(true);
                }
            }

            function OpenCapturarResultados() {
                var vIdPeriodo = GetPeriodoId();
                if (vIdPeriodo != null)
                    OpenWindow(GetCapturarResultadosPeriodoProperties(vIdPeriodo));
                else
                    radalert("Selecciona un período.", 400, 150);
            }

            function OpenCumplimientoPersonal() {
                var vIdPeriodo = GetPeriodoId();
                if (vIdPeriodo != null)
                    OpenWindow(GetCumplimientoPersonal(vIdPeriodo));
                else
                    radalert("Selecciona un período.", 400, 150);
            }

            function OpenCumplimientoGlobal() {
                var vIdPeriodo = GetPeriodoId();
               // var vClCopia = GetClCopia();
                if (vIdPeriodo != null)
                   // if (vClCopia != "CONSECUENTE")
                        OpenWindow(GetCumplimientoGlobal(vIdPeriodo));
                    //else 
                      //  OpenWindow(GetCumplimientoGlobalConsecuente(vIdPeriodo, vClCopia));
                else
                    radalert("Selecciona un período.", 400, 150);
            }

            function OpenReporteBono() {
                var vIdPeriodo = GetPeriodoId();
                if (vIdPeriodo != null)
                    OpenWindow(GetReporteBono(vIdPeriodo));
                else
                    radalert("Selecciona un período.", 400, 150);
            }

            function GetReporteBono(pIdPeriodo) {
                var wnd = GetWindowProperties();
                //wnd.width = 910;
                //wnd.height = 680;
                wnd.vRadWindowId = "WinReporteBono";
                wnd.vURL = "VentanaReporteBono.aspx";
                if (pIdPeriodo != null) {
                    wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                    wnd.vTitulo = "Consulta Bono - Evaluación del desempeño";
                }
                return wnd;
            }

            function GetControlAvance(pIdPeriodo) {
                var wnd = GetWindowProperties();
                wnd.vRadWindowId = "WinReporteBono";
                wnd.vURL = "VentanaControlAvanceDesempeno.aspx";
                wnd.vURL += String.format("?IdPeriodo={0}", pIdPeriodo);
                wnd.vTitulo = "Control de avance";
                return wnd;
            }

            function OpenControlAvance() {
                var vIdPeriodo = GetPeriodoId();
                if (vIdPeriodo != null)
                    OpenWindow(GetControlAvance(vIdPeriodo));
                else
                    radalert("Selecciona un período.", 400, 150);
            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpPeriodosEvaluacion" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramOrganigrama" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rlvPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodosEvaluacion"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rlvPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnConfiguracion" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnContrasenia" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnMetas" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnDefinirBono" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCapturaEvaluaciones" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                      <telerik:AjaxUpdatedControl ControlID="txtClPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtDsPeriodo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtClEstatus" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtTipo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtUsuarioMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="txtFechaMod" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfiguracion" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnContrasenia" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnMetas" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnDefinirBono" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCapturaEvaluaciones" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReabrir">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnConfiguracion" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCerrar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnContrasenia" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnMetas" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnDefinirBono" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnEnviarSolicitudes" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnCapturaEvaluaciones" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Evaluación del desempeño</label>
    <div class="ctrlBasico">
        <telerik:RadListView ID="rlvPeriodos" runat="server" DataKeyNames="ID_PERIODO, CL_ESTADO_PERIODO" ClientDataKeyNames="ID_PERIODO,NB_PERIODO,CL_ORIGEN_CUESTIONARIO, CL_ESTADO_PERIODO, CL_TIPO_COPIA, NO_REPLICA"
            OnNeedDataSource="rlvPeriodos_NeedDataSource" OnItemCommand="rlvPeriodos_ItemCommand" AllowPaging="true" ItemPlaceholderID="ProductsHolder">
            <LayoutTemplate>
                <div style="overflow: auto; width: 660px;">
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

                        <div  style="overflow: auto; overflow-y: auto; height: 45px;">
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

                        <div style="overflow: auto; overflow-y: auto; height: 45px;">
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
                    No hay periodos disponibles
                </div>
            </EmptyDataTemplate>
        </telerik:RadListView>
    </div>
    <div class="ctrlBasico" style="text-align: center">

                    <telerik:RadTabStrip runat="server" ID="rtsPeriodos" MultiPageID="rmpPeriodos" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Gestionar"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Detalles"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: 10px;"></div>
          <telerik:RadMultiPage runat="server" ID="rmpPeriodos" SelectedIndex="0" Height="100%" Width="100%">
                    <telerik:RadPageView ID="rpvPeriodos" runat="server">
        <div style="height: 5px;"></div>
        <div class="ctrlBasico" style="text-align: center">

            <div>
                <label class="labelTitulo">Administrar</label>
                <telerik:RadButton ID="rdAgregar" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenInsertPeriodoWindow"></telerik:RadButton>
                <telerik:RadButton ID="btnConfiguracion" runat="server" Text="Configurar" AutoPostBack="false" OnClientClicked="OpenConfigurarPeriodoWindow"></telerik:RadButton>
                <telerik:RadButton ID="btnReabrir" runat="server" Text="Re-abrir" OnClick="btnReactivar_Click" OnClientClicking="ConfirmarReactivar"></telerik:RadButton>
                <telerik:RadButton ID="btnCerrar" runat="server" OnClientClicking="ConfirmarCerrar" OnClick="btnCerrar_Click" Text="Cerrar periodo"></telerik:RadButton>
                <telerik:RadButton ID="btnCopiar" runat="server" Text="Copiar de" OnClick="btnCopiar_Click" AutoPostBack="true"></telerik:RadButton>
            </div>
            <div style="height: 10px"></div>
            <div>
                <telerik:RadButton ID="btnReplicar" runat="server" AutoPostBack="true" OnClick="btnReplicar_Click" Text="Replicar periodos"></telerik:RadButton>                
            </div>
            <div style="height: 10px;"></div>
            <div>
                <label class="labelTitulo">Cuestionarios</label>
                <telerik:RadButton ID="btnEnviarSolicitudes" runat="server" Text="Enviar solicitudes" OnClientClicking="OpenEnvioSolicitudes" AutoPostBack="true"></telerik:RadButton>
                <telerik:RadButton ID="btnControlAvance" runat="server" Text="Control de avance" AutoPostBack="false" OnClientClicked="OpenControlAvance"></telerik:RadButton>
                <telerik:RadButton ID="btnCapturaEvaluaciones" runat="server" Text="Captura de evaluaciones" OnClientClicking="OpenCapturarResultados" AutoPostBack="false"></telerik:RadButton>
            </div>
            <div style="height: 10px;"></div>
            <div>
                <label class="labelTitulo">Consultas</label>
                <telerik:RadButton ID="btnCumplimientoPersonal" runat="server" Text="Cumplimiento personal" OnClientClicking="OpenCumplimientoPersonal" AutoPostBack="false"></telerik:RadButton>
                <telerik:RadButton ID="btnCumplimientoGlobal" runat="server" Text="Cumplimiento global" OnClientClicking="OpenCumplimientoGlobal" AutoPostBack="false"></telerik:RadButton>
                <telerik:RadButton ID="btnBono" runat="server" Text="Bono" AutoPostBack="false" OnClientClicking="OpenReporteBono"></telerik:RadButton>
            </div>
        </div>
        <div style="height: 10px;"></div>
</telerik:RadPageView>
               <telerik:RadPageView ID="rpvInformacion" runat="server">
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="lblEvento" name="lblEvento" runat="server">Periodo:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClPeriodo" Enabled="false" runat="server" Width="400px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label1" name="lblEvento" runat="server">Descripción:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtDsPeriodo" Enabled="false" runat="server" Width="400px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label2" name="lblEvento" runat="server">Estatus:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClEstatus" Enabled="false" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label3" name="lblEvento" runat="server">Tipo de metas:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtTipo" Enabled="false" runat="server" Width="400px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label5" name="lblCurso" runat="server">Último usuario que modifica:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtUsuarioMod" Enabled="false" runat="server" Width="140px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label6" name="lblCurso" runat="server">Última fecha de modificación:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtFechaMod" Enabled="false" runat="server" Width="140px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                    </telerik:RadPageView>

              </telerik:RadMultiPage>
    </div>
    <div style="clear: both;"></div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="winCumplimientoConsecuente" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinPeriodoDesempeno" runat="server" Animation="Fade" VisibleStatusbar="false" ShowContentDuringLoad="true" Behaviors="Close" Modal="true" ReloadOnShow="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="WinEnvioSolicitudReplica" runat="server" Animation="Fade" VisibleStatusbar="false" ShowContentDuringLoad="true" Behaviors="Close" Modal="true" ReloadOnShow="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="WinPeriodoReplica" runat="server" Animation="Fade" VisibleStatusbar="false" ShowContentDuringLoad="true" Behaviors="Close" Modal="true" ReloadOnShow="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="WinPeriodoConfiguracion" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinCapturarResultados" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinEnvioSolicitudes" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="rwCaptura" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winEvaluado" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="rwAdjuntarArchivos" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winCumplimientoPersonal" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winReporteCumplimientoPersonal" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winCumplimientoGlobal" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinReporteBono" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winBonos" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinMetas" runat="server" Animation="Fade" VisibleStatusbar="false"
                Behaviors="Close" Modal="true" ReloadOnShow="true" ShowContentDuringLoad="false">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>