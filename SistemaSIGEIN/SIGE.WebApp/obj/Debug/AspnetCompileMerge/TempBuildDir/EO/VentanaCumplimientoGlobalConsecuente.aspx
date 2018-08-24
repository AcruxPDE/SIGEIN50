<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaCumplimientoGlobalConsecuente.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaCumplimientoGlobalConsecuente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function OpenCumPersonal(pIdEvaluado, pIdPeriodo) {
            var vURL = "../EO/VentanaReporteCumplimientoPersonal.aspx";
            var vTitulo = "Reporte Cumplimiento Personal";
            vURL = vURL + "?idEvaluado=" + pIdEvaluado + "&idPeriodo=" + pIdPeriodo;
            OpenSelectionWindow(vURL, "winEvaluado", "Reporte cumplimiento personal")
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


        function ShowInsertFormConsecuente(IdPeriodoConsecuente) {
            OpenSelectionWindow("../EO/VentanaCumplimientoGlobal.aspx?PeriodoId=" + IdPeriodoConsecuente, "winCumplimientoConsecuente", "Cumplimiento Global")
        }

        function ShowInsertForm(IdPeriodo) {
            OpenSelectionWindow("../EO/VentanaCumplimientoGlobal.aspx?PeriodoId=" + IdPeriodo, "winCumplimientoConsecuente", "Cumplimiento Global")
        }

        function OpenPersonalConsecuente(pIdPOriginal, pIdPConsecuente, pIdEvaOriginal, pIdEvaConsecuente) {
            var vURL = "../EO/CumplimientoPersonalConsecuente.aspx";
            var vTitulo = "Reporte Cumplimiento Personal Consecuente";
            vURL = vURL + "?idPeriodoOriginal=" + pIdPOriginal + "&IdPeriodoConsecuente=" + pIdPConsecuente + "&IdEvalOriginal=" + pIdEvaOriginal + "&IdEvalConsecuente=" + pIdEvaConsecuente;
            OpenSelectionWindow(vURL, "winEvaluado", "Reporte cumplimiento personal")
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <div style="clear: both; height: 10px;"></div>
        <telerik:RadTabStrip ID="rtsCumplimiento" runat="server" SelectedIndex="0" MultiPageID="rmpCumplimiento">
            <Tabs>
                <telerik:RadTab Text="Contexto"></telerik:RadTab>
                <telerik:RadTab Text="Reporte"></telerik:RadTab>
                <telerik:RadTab Text="Gráfica"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="rmpCumplimiento" runat="server" SelectedIndex="0" Height="100%" Width="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label></label>
                            </td>
                            <td class="ctrlTableDataContext">
                                <label>Período original</label></td>
                            <td class="ctrlTableDataContext">
                                <label style="margin-left: 20px;">Período consecuente</label></td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <a onclick="return ShowInsertForm('<%= vIdPeriodo %>');">
                                    <div id="txtClPeriodo" runat="server" width="170" maxlength="1000" enabled="false" style="cursor: pointer;"></div>
                                </a>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <a onclick="return ShowInsertFormConsecuente('<%= vIdPeriodoConsecuente %>');">
                                    <div id="txtClConsecuente" runat="server" width="170" maxlength="1000" enabled="false" style="cursor: pointer;"></div>
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Fechas:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtFechaOriginal" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtFechaConsecuente" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Notas:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNotasOriginal" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNotasConsecuente" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de período:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoPeriodoConsecuente" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de bono:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoBono" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoBonoConsecuente" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Responsable de captura de metas:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtCapturaMetas" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtCapturaMetasConsecuentes" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvReporte" runat="server">
                <div style="height: calc(100% - 60px);">
                    <div style="clear: both; height: 10px;"></div>
                    <telerik:RadGrid ID="rgEvaluados"
                        runat="server"
                        Height="100%"
                        Width="100%"
                        AllowSorting="true"
                        ShowFooter="true"
                        AllowMultiRowSelection="false"
                        OnNeedDataSource="rgEvaluados_NeedDataSource">
                        <ClientSettings EnablePostBackOnRowClick="false">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView DataKeyNames="ID_EMPLEADO_ORIGINAL" ClientDataKeyNames="ID_EMPLEADO_ORIGINAL" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true" AllowPaging="false" AllowFilteringByColumn="true">
                            <ColumnGroups>
                                <telerik:GridColumnGroup Name="ValorCumplido" HeaderText="Valor Cumplido"
                                    HeaderStyle-HorizontalAlign="Center" />
                            </ColumnGroups>
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="200" HeaderText="Puesto" DataField="PUESTOS" UniqueName="PUESTOS" CurrentFilterFunction="Contains" FilterControlWidth="180" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridHyperLinkColumn HeaderStyle-Width="250" UniqueName="NB_EVALUADO_ORIGINAL" DataTextField="NB_EVALUADO_ORIGINAL" DataNavigateUrlFields="ID_PERIODO_ORIGINAL, ID_PERIODO_CONSECUENTE, ID_EVALUADO_ORIGINAL, ID_EVALUADO_CONSECUENTE" DataNavigateUrlFormatString="javascript:OpenPersonalConsecuente({0},{1},{2},{3})" HeaderText="Nombre" CurrentFilterFunction="Contains" FilterControlWidth="210" AutoPostBackOnFilter="true"></telerik:GridHyperLinkColumn>
                                <telerik:GridBoundColumn ColumnGroupName="ValorCumplido" CurrentFilterFunction="Contains" HeaderStyle-Width="70" HeaderText="Primer período" DataField="C_GLOBAL_ORIGINAL" UniqueName="C_GLOBAL_ORIGINAL" Aggregate="Sum" FooterAggregateFormatString="Total: {0:N2}%" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" FilterControlWidth="50" AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ColumnGroupName="ValorCumplido" CurrentFilterFunction="Contains" HeaderStyle-Width="70" HeaderText="Período consecuente" DataField="C_GLOBAL_CONSECUENTE" UniqueName="C_GLOBAL_CONSECUENTE" Aggregate="Sum" FooterAggregateFormatString="Total: {0:N2}%" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" FilterControlWidth="50" AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvGrafica" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div style="height: calc(100% - 60px);">
                    <telerik:RadHtmlChart
                        runat="server"
                        ID="rhcGraficaGlobal"
                        Width="100%"
                        Height="100%"
                        Transitions="true"
                        Skin="Silk">
                        <ChartTitle Text="Cumplimiento global de períodos consecuentes">
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
        </telerik:RadMultiPage>
    </div>
</asp:Content>
