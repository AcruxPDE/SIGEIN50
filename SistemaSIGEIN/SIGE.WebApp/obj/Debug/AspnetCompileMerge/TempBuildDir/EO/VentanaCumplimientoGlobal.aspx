<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaCumplimientoGlobal.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaCumplimientoGlobal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>
        function OpenCumPersonal(pIdEvaluado, pIdPeriodo) {
            var vURL = "../EO/VentanaReporteCumplimientoPersonal.aspx";
            var vTitulo = "Reporte Cumplimiento Personal";
            vURL = vURL + "?idEvaluado=" + pIdEvaluado + "&idPeriodo=" + pIdPeriodo;
            OpenSelectionWindow(vURL, "winEvaluado", "Consulta General - Evaluación del desempeño")
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
            if (pDato != null) {
                var vJsonPeriodo = JSON.stringify({ clTipo: "PERIODODESEMPENO", oSeleccion: pDato });
                var ajaxManager = $find('<%= ramReportes.ClientID%>');
                ajaxManager.ajaxRequest(vJsonPeriodo);
            }
        }

        function OpenWindowPeriodos() {
            var vIdPeriodo = ('<%= vIdPeriodo%>');
            OpenSelectionWindow("/Comunes/SeleccionPeriodosDesempeno.aspx?ID_PERIODO=" + vIdPeriodo + "&CL_TIPO=Global", "winSeleccion", "Seleccion de periodos a comparar");
        }

        function OpenWindowComparar() {
            OpenSelectionWindow("/EO/VentanaComparativaGlobal.aspx?", "winBonos", "Consulta General comparativa - Evaluación del desempeño");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramReportes" runat="server" OnAjaxRequest="ramReportes_AjaxRequest" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramReportes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgComparativos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 30px);">
        <div style="clear: both; height: 10px;"></div>
        <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
            <Tabs>
                <telerik:RadTab Text="Contexto"></telerik:RadTab>
                <telerik:RadTab Text="Reporte"></telerik:RadTab>
                <telerik:RadTab Text="Gráfica"></telerik:RadTab>
                <telerik:RadTab Text="Comparar"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: calc(100% - 50px);">
            <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="rpvContexto" runat="server">
                    <div style="clear: both; height: 20px;"></div>
                    <div style="height: calc(100% - 50px);">
                        <div class="ctrlBasico">
                            <table class="ctrlTableForm">
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Período:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtClPeriodo" runat="server"></div>
                                    </td>
                                </tr>
                                <%-- <tr>
                            <td class="ctrlTableDataContext">
                                <label>Nombre del periodo:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNbPeriodo" runat="server"></div>
                            </td>
                        </tr>--%>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Descripción:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtPeriodos" runat="server"></div>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Notas:</label></td>
                                    <td class="ctrlTableDataBorderContext">
                                        <div id="txtNotas" runat="server" width="170" maxlength="1000" enabled="false"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Fecha:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtFechas" runat="server"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Tipo de periodo:</label></td>
                                    <td class="ctrlTableDataBorderContext">
                                        <div id="txtTipoPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Tipo de bono:</label></td>
                                    <td class="ctrlTableDataBorderContext">
                                        <div id="txtTipoBono" runat="server" width="170" maxlength="1000" enabled="false"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Tipo de metas:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtTipoMetas" runat="server"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Tipo de capturista:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtTipoCapturista" runat="server"></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvReporte" runat="server">
                    <div style="clear: both; height: 40px;"></div>
                    <div style="height: calc(100% - 30px);">
                        <telerik:RadGrid ID="rgEvaluados"
                            runat="server"
                            Height="100%"
                            Width="100%"
                            AllowSorting="true"
                            ShowFooter="true"
                            HeaderStyle-Font-Bold="true"
                            AllowMultiRowSelection="true"
                            OnNeedDataSource="rgEvaluados_NeedDataSource">
                            <ClientSettings EnablePostBackOnRowClick="false">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <MasterTableView DataKeyNames="ID_EVALUADO, ID_PERIODO" ClientDataKeyNames="ID_EVALUADO, ID_PERIODO" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true" AllowPaging="false" AllowFilteringByColumn="true">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="300" HeaderText="Puesto" DataField="NB_PUESTO_PERIODO" UniqueName="NB_PUESTO_PERIODO" CurrentFilterFunction="Contains" FilterControlWidth="220" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn HeaderStyle-Width="300" UniqueName="NB_EVALUADO" HeaderText="Nombre completo" DataTextField="NB_EVALUADO" DataNavigateUrlFields="ID_EVALUADO, ID_PERIODO" DataNavigateUrlFormatString="javascript:OpenCumPersonal({0},{1})" CurrentFilterFunction="Contains" FilterControlWidth="220" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="90" HeaderText="Valor cumplido" DataField="PR_CUMPLIMIENTO_EVALUADO" UniqueName="PR_CUMPLIMIENTO_EVALUADO" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" CurrentFilterFunction="Contains" FilterControlWidth="50" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="90" HeaderText="Valor ponderado" DataField="PR_EVALUADO" UniqueName="PR_EVALUADO" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" CurrentFilterFunction="Contains" FilterControlWidth="50" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn CurrentFilterFunction="Contains" HeaderStyle-Width="100" HeaderText="Aporte a cumplimiento general" DataField="C_GENERAL" UniqueName="C_GENERAL" Aggregate="Sum" FooterAggregateFormatString="Cumplimiento general: {0:N2}%" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" FilterControlWidth="60" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvGrafica" runat="server" Height="100%">
                    <div style="height: calc(100% - 50px);">
                        <telerik:RadHtmlChart
                            runat="server"
                            ID="rhcGraficaGlobal"
                            Width="100%"
                            Height="100%"
                            Transitions="true"
                            Skin="Silk">
                            <ChartTitle Text="Cumplimiento global del periodo">
                                <Appearance Align="Center" Position="Top">
                                </Appearance>
                            </ChartTitle>
                            <Legend>
                                <Appearance Position="Right" Visible="true">
                                </Appearance>
                            </Legend>
                            <PlotArea>
                            </PlotArea>
                        </telerik:RadHtmlChart>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvComparar" runat="server" Height="100%">
                    <div style="height: calc(100% - 50px);">
                        <div style="height: 10px; clear: both;"></div>
                        <telerik:RadGrid
                            ID="rgComparativos"
                            runat="server"
                            Height="100%"
                            AutoGenerateColumns="false"
                            EnableHeaderContextMenu="true"
                            AllowSorting="true"
                            HeaderStyle-Font-Bold="true"
                            AllowMultiRowSelection="false"
                            OnNeedDataSource="rgComparativos_NeedDataSource"
                            OnItemDataBound="rgComparativos_ItemDataBound"
                            OnItemCommand="rgComparativos_ItemCommand">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView DataKeyNames="idPeriodo" ClientDataKeyNames="idPeriodo" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Clave" DataField="clPeriodo" UniqueName="clPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Período" DataField="nbPeriodo" UniqueName="nbPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Descripción" DataField="dsPeriodo" UniqueName="dsPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="90" HeaderText="Tipo de período" DataField="clOrigen" UniqueName="clOrigen" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" HeaderStyle-Width="5%" ButtonType="ImageButton"></telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div style="height: 10px; clear: both;"></div>
                        <div class="divControlIzquierda">
                            <telerik:RadButton ID="btnSeleccionar" runat="server" AutoPostBack="false" Width="200" Text="Seleccionar periodos" OnClientClicked="OpenWindowPeriodos"></telerik:RadButton>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="btnComparar" runat="server" AutoPostBack="false" Width="100" Text="Comparar" OnClientClicked="OpenWindowComparar"></telerik:RadButton>
                        </div>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>
</asp:Content>
