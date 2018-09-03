<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaCalificaEvaluadorEvaluado.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaCalificaEvaluadorEvaluado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .labelSubtitulo {
            display: block;
            font-size: 1.6em;
        }

        .RadProgressBar .rpbStateSelected, .RadProgressBar .rpbStateSelected:hover, .RadProgressBar .rpbStateSelected:link, .RadProgressBar .rpbStateSelected:visited {
            background-color: #FF7400 !important;
            border: 0px solid black !important;
            color: black !important;
        }
    </style>
    <script>
        function onCloseWindow(oWnd, args) {
            $find("<%=grdEvaluados.ClientID%>").get_masterTableView().rebind();

        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenCapturaResultado() {
            var selectedItem = $find("<%=grdEvaluados.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined) {
                if (selectedItem.getDataKeyValue("CL_ESTADO_EMPLEADO") == "ALTA") {
                var pIdPeriodo = '<%=vIdPeriodo%>';
                var pIdEvaluado = selectedItem.getDataKeyValue("ID_EVALUADO");
                OpenSelectionWindow("VentanaContextoCapturaResultados.aspx?idPeriodo=" + pIdPeriodo + "&idEvaluado=" + pIdEvaluado, "rwCaptura", "Captura de resultados")
                }
                else
                    radalert("Evaluado dado de baja.", 400, 150);
                }
            else
                radalert("Selecciona a un evaluado.", 400, 150);
        }

        function confirmarTerminar(sender, args) {
           confirmAction(sender, args, "¿Deseas terminar la sesión?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdEvaluados"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEvaluados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdEvaluados"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="height: calc(100% - 45px); padding-left: 15px; padding-top:10px;padding-right:15px;">
        <telerik:RadSplitter ID="rsPlantilla" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpGridEvaluados" runat="server" Height="100%" ShowContentDuringLoad="false">
                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Periodo:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNoPeriodo" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNbPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="clear: both; height: 2px;"></div>
                <label class="labelSubtitulo">Captura de resultados</label>
                <div style="clear: both; height: 3px;"></div>

                <div style="height: calc(100% - 105px);">
                    <telerik:RadGrid ID="grdEvaluados" runat="server"
                        AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" EnableHeaderContextMenu="true" AllowSorting="true"
                        AllowMultiRowSelection="false" OnNeedDataSource="grdEvaluados_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EVALUADO,ESTATUS, CL_ESTADO_EMPLEADO" ClientDataKeyNames="ID_EVALUADO,ESTATUS, CL_ESTADO_EMPLEADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="30" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="80" HeaderText="Estatus" DataField="ESTATUS" UniqueName="ESTATUS" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="22px" ShowContentDuringLoad="false">
                <telerik:RadSlidingZone ID="rszMensajeInicial" runat="server" SlideDirection="Left" DockedPaneId="rspMensajeInicial" Width="22px">
                    <telerik:RadSlidingPane ID="rspMensajeInicial" runat="server" Title="Mensaje Inicial" Width="340px" RenderMode="Mobile" Height="200">
                        <div style="padding: 10px; text-align: justify;" id="mensajeInicial" runat="server">
                            <p>Estimado(a): <label runat="server" id="lblEvaluador"></label></p><br />
                            <p>
                                La lista de evaluados que se muestra, ha sido asignada para que los califiques,
                                cada evaluado cuenta con una serie de metas a las cuales deberás asignarles un
                                valor.<br /><br />
                               Para calificarlas solo selecciona al evaluado y a continuación da clic en el botón capturar resultados.
                               <br /> Para salir de la ventana actual selecciona el botón terminar proceso. Recuerda que puedes volver a ingresar posteriormente para seguir capturando los resultados de los evaluados.
                            </p>
                        </div>
                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div class="ctrlBasico" style="float:right;">
        <telerik:RadButton ID="btnEvaluarMetas" runat="server" Text="Capturar resultados" AutoPostBack="false" OnClientClicking="OpenCapturaResultado"></telerik:RadButton>
        <telerik:RadButton ID="btnTerminar" runat="server" Text="Terminar proceso" OnClientClicking="confirmarTerminar" AutoPostBack="true" OnClick="btnTerminar_Click"></telerik:RadButton>
    </div>
    <div style="clear: both; height: 15px;"></div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server">
        <Windows>
            <telerik:RadWindow ID="rwCaptura" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="rwAdjuntarArchivos" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
