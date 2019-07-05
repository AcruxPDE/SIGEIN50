<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="CuestionariosInterno.aspx.cs" Inherits="SIGE.WebApp.FYD.EvaluacionCompetencia.CuestionariosInterno" %>
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

        function OpenMatrizEvaluadoresWindow() {
            var masterTable = $find('<%= grdEvaluados.ClientID %>').get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                //if (vEstadoPeriodo != "CERRADO" && vEstadoPeriodo != "Cerrado") {
                var vEvaluado = selectedItems[0];
                var vIdEvaluadoEvaluador = vEvaluado.getDataKeyValue("ID_EVALUADO_EVALUADOR");
                OpenSelectionWindow("EvaluacionCompetencia/Evaluar.aspx?ID_EVALUADOR=<%= pIdEvaluador %>&ID_EVALUADO_EVALUADOR=" + vIdEvaluadoEvaluador, "winMatrizCuestionarios", "Evaluación")
                //} else {
                //    radalert("Periodo cerrado.", 400, 150);
                //}
            }
            else {
                radalert("Selecciona un evaluado.", 400, 150, "Aviso");
                }
        }

            function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 40,
                    height: browserWnd.innerHeight - 40
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

    <div style="height: calc(100% - 40px);">
        <telerik:RadSplitter ID="rsPlantilla" Width="100%" Height="100%" BorderSize="0" runat="server">

            <telerik:RadPane ID="rpGridEvaluados" runat="server" Height="100%" ShowContentDuringLoad="false">
              <%--  <table class="ctrlTableForm">
                    <tr>
                        <td>
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Periodo:"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtNoPeriodo" Width="80" ReadOnly="true"></telerik:RadTextBox>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtNbPeriodo" Width="500" ReadOnly="true"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>--%>

                 <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
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
                    <telerik:RadGrid ID="grdEvaluados" runat="server" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="false" AllowSorting="true" OnNeedDataSource="grdEvaluados_NeedDataSource" OnItemDataBound="grdEvaluados_ItemDataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                            <ClientEvents OnGridCreated="calcularBar" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView EnableColumnsViewState="false" DataKeyNames="ID_EVALUADO_EVALUADOR,FG_EVALUADO" ClientDataKeyNames="ID_EVALUADO_EVALUADOR,FG_EVALUADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Persona a evaluar" HeaderStyle-Width="100px" DataField="NB_EVALUADO" UniqueName="NB_EVALUADO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Puesto" HeaderStyle-Width="100px" DataField="NB_PUESTO" UniqueName="NB_PUESTO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Evaluar con rol" HeaderStyle-Width="100px" DataField="CL_ROL_EVALUADOR" UniqueName="CL_ROL_EVALUADOR" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Evaluado" HeaderStyle-Width="100px" DataField="FG_EVALUADO" UniqueName="FG_EVALUADO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
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

<%--                    <telerik:RadSlidingPane ID="rspInstrucciones" runat="server" Title="Instrucciones de llenado" Width="315">
                        <div style="padding: 10px;" id="instrucciones" runat="server"></div>
                    </telerik:RadSlidingPane>--%>

                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div>
        <div class="ctrlBasico">
        <telerik:RadButton ID="btnEvaluar" runat="server" Text="Evaluar" AutoPostBack="false" OnClientClicked="OpenMatrizEvaluadoresWindow"></telerik:RadButton>
        <telerik:RadButton ID="btnSalir" runat="server" Text="Salir" AutoPostBack="true" Visible="false" OnClick="btnSalir_Click"></telerik:RadButton>
        </div>
         <div class="divControlDerecha">
            <asp:HiddenField ID="total" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="evalu" runat="server"></asp:HiddenField>
             </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server">
        <Windows>
            <telerik:RadWindow ID="winMatrizCuestionarios" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>


