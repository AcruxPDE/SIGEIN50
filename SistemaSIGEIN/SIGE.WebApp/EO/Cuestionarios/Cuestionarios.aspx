<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="Cuestionarios.aspx.cs" Inherits="SIGE.WebApp.EO.Cuestionarios.Cuestionarios" %>

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
        function OnCommand(sender, args) {

            if (args.get_commandName() == "Evaluar") {
                var itemIndex = args.get_commandArgument();
                var rowID = sender.get_masterTableView().get_dataItems()[itemIndex].getDataKeyValue("ID_EVALUADO_EVALUADOR");
                OpenMatrizEvaluadoresWindow(rowID);
                args.set_cancel(true);
            }
        }

        function closeWindow() {
            sendDataToParent(null);
        }

        function calcularBar(sender, eventArgs) {
            var pb = $find("<%= cpbEvaluados.ClientID %>");

            var count = document.getElementById('<%= total.ClientID%>').value;
            var evaluados = document.getElementById('<%= evalu.ClientID%>').value;

            var marcar = 0;
            if (count != 0)
                marcar = (evaluados * 100) / count;
            pb.set_value(marcar)
        }

        function OpenMatrizEvaluadoresWindow(vIdEvaluadoEvaluador) {
            var masterTable = $find('<%= grdEvaluados.ClientID %>').get_masterTableView();
            OpenSelectionWindow("/EO/Cuestionarios/Evaluar.aspx?ID_EVALUADOR=<%= pIdEvaluador %>&ID_EVALUADO_EVALUADOR=" + vIdEvaluadoEvaluador, "winMatrizCuestionarios", "Evaluación")
        }

        function OpenReporteJerarquico() {
            OpenSelectionWindow("/EO/Cuestionarios/ReporteJerarquico.aspx?ID_EVALUADOR=<%= pIdEvaluador %>", "winReporteJerarquico", "Reporte Jerárquico")
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

        function useDataFromChild(pDato) {
            $find('<%= RadAjaxManager1.ClientID %>').ajaxRequest();
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
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="total"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="evalu"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEvaluados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdEvaluados"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="height: calc(100% - 35px);">
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
                <label class="labelSubtitulo">Progreso de la evaluación</label>

                <telerik:RadProgressBar runat="server" ID="cpbEvaluados" BarType="Percent" Width="99%" MinValue="0"></telerik:RadProgressBar>
                <div style="clear: both; height: 3px;"></div>

                <div style="height: calc(100% - 105px);">
                    <telerik:RadGrid ID="grdEvaluados" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="false" AllowSorting="true" OnNeedDataSource="grdEvaluados_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                            <ClientEvents OnGridCreated="calcularBar" OnCommand="OnCommand" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView EnableColumnsViewState="false" DataKeyNames="ID_EVALUADO_EVALUADOR,FG_EVALUADO" ClientDataKeyNames="ID_EVALUADO_EVALUADOR,FG_EVALUADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Persona a evaluar" HeaderStyle-Width="100px" DataField="NB_EVALUADO" UniqueName="NB_EVALUADO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Puesto" HeaderStyle-Width="100px" DataField="NB_PUESTO" UniqueName="NB_PUESTO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Evaluar con rol" HeaderStyle-Width="100px" DataField="CL_ROL_EVALUADOR" UniqueName="CL_ROL_EVALUADOR" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Evaluado" HeaderStyle-Width="100px" DataField="FG_EVALUADO" UniqueName="FG_EVALUADO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridButtonColumn CommandName="Evaluar" HeaderStyle-Width="80px" DataTextField="DS_TEXTO_BOTON" UniqueName="Evaluar" HeaderText="Evaluar"></telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                </div>
            </telerik:RadPane>

            <telerik:RadPane ID="rpAyuda" runat="server" Width="22px" ShowContentDuringLoad="false">

                <telerik:RadSlidingZone ID="rszMensajeInicial" runat="server" SlideDirection="Left" DockedPaneId="rspMensajeInicial" Width="22px">

                    <telerik:RadSlidingPane ID="rspMensajeInicial" runat="server" Title="Mensaje Inicial" Width="340px" RenderMode="Mobile" Height="200">
                        <div style="padding: 10px; text-align: justify;" id="mensajeInicial" runat="server"></div>
                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div>
        <%-- %><telerik:RadButton ID="btnEvaluar" runat="server" Text="Evaluar" AutoPostBack="false" OnClientClicked="OpenMatrizEvaluadoresWindow"></telerik:RadButton>--%>
        <telerik:RadButton ID="btnReporteJerarquico" runat="server" Text="Reporte jerárquico" AutoPostBack="false" OnClientClicked="OpenReporteJerarquico" ></telerik:RadButton>
        <telerik:RadButton ID="btnSalir" runat="server" Text="Salir" AutoPostBack="true" Visible="false" OnClick="btnSalir_Click"></telerik:RadButton>
        <asp:HiddenField ID="total" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="evalu" runat="server"></asp:HiddenField>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server">
        <Windows>
            <telerik:RadWindow ID="winReporteJerarquico" runat="server" Title="Reporte jerárquico" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winMatrizCuestionarios" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>

