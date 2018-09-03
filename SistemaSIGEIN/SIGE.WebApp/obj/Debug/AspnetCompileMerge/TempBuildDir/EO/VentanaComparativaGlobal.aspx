<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaComparativaGlobal.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaComparativaGlobal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function CloseWindow() {
            GetRadWindow().close();
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

        

        function GetCumplimientoGlobal(pIdPeriodo) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            var wnd = windowProperties;
            wnd.vRadWindowId = "winCumplimientoGlobal";
            wnd.vURL = "VentanaCumplimientoGlobal.aspx";
            if (pIdPeriodo != null) {
                wnd.vURL += String.format("?PeriodoId={0}", pIdPeriodo);
                wnd.vTitulo = "Cumplimiento global";
            }
            return wnd;
        }

        function OpenGlobal(pIdPeriodo) {
            if (pIdPeriodo != null)
                OpenWindow(GetCumplimientoGlobal(pIdPeriodo));
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function OpenCumPersonal(pIdEvaluado, pIdPeriodo) {
            var vURL = "VentanaReporteCumplimientoPersonal.aspx";
            var vTitulo = "Reporte Cumplimiento Personal";
            vURL = vURL + "?idEvaluado=" + pIdEvaluado + "&idPeriodo=" + pIdPeriodo;
            OpenSelectionWindow(vURL, "winEvaluado", "Reporte cumplimiento personal")
        }

        function OpenIndividualComparativo(pIdEvaluado,pIdEmpleado) {
            OpenSelectionWindow("VentanaComparativaIndividual.aspx?ID_EVALUADO=" + pIdEvaluado + "&ID_EMPLEADO=" + pIdEmpleado + "&CL_ORIGEN=GLOBAL", "winEvaluado", "Comparación desempeño personal");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear: both; height: 10px;"></div>
    <telerik:RadTabStrip ID="rtsReporteGlobal" runat="server" SelectedIndex="0" MultiPageID="rmpReporteGlobal">
        <Tabs>
            <telerik:RadTab SelectedIndex="0" Text="Contexto"></telerik:RadTab>
            <telerik:RadTab SelectedIndex="1" Text="Reporte"></telerik:RadTab>
            <telerik:RadTab SelectedIndex="2" Text="Gráfica"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 120px);">
        <telerik:RadMultiPage ID="rmpReporteGlobal" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                <div style="clear: both; height: 10px;"></div>
                   <%-- <div class="divControlDerecha" style="width: 15%; text-align: left; margin-right:3%;">
                        <fieldset>
                            <legend>
                                <label>Estatus de resultados</label>
                            </legend>

                            <table class="ctrlTableForm">
                                <tr>
                                    <td>
                                        <div style="background: #F2F2F2; width: 50px; border:solid 1px;">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>No calificado</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="background: #FF0000; width: 50px; ">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>No alcanzado</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="background: #FFFF00; width: 50px;">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>Mínimo</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="background: #0070C0; width: 50px;">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>Satisfactorio</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="background: #00B050; width: 50px;">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>Sobresaliente</label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>--%>
                <telerik:RadGrid
                    ID="rgContexto"
                    runat="server"
                    Height="90%"
                    Width="100%"
                    AutoGenerateColumns="false"
                    EnableHeaderContextMenu="false"
                    AllowSorting="false"
                    AllowMultiRowSelection="false"
                    HeaderStyle-Font-Bold="true"
                    OnNeedDataSource="rgContexto_NeedDataSource">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="false" AllowScroll="true" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_PERIODO" ClientDataKeyNames="ID_PERIODO" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <ColumnGroups>
                            <telerik:GridColumnGroup Name="Fechas" HeaderText="Duración" HeaderStyle-Font-Bold="true"
                                HeaderStyle-HorizontalAlign="Center" />
                        </ColumnGroups>
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="100" HeaderText="Período" DataField="CL_TIPO_PERIODO" UniqueName="CL_TIPO_PERIODO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="140" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="240" HeaderText="Notas" DataField="XML_DS_NOTAS" UniqueName="XML_DS_NOTAS" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="90" ColumnGroupName="Fechas" HeaderText="De" DataField="FE_INICIO_PERIODO" UniqueName="FE_INICIO_PERIODO" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"></telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="90" ColumnGroupName="Fechas" HeaderText="a" DataField="FE_TERMINO_PERIODO" UniqueName="FE_TERMINO_PERIODO" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"></telerik:GridDateTimeColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvReporte" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div style="height: 97%">
                    <telerik:RadGrid
                        ID="rgGlobalComparativos"
                        runat="server"
                        Height="100%"
                        AutoGenerateColumns="false"
                        EnableHeaderContextMenu="false"
                        AllowSorting="false"
                        AllowMultiRowSelection="false"
                        HeaderStyle-Font-Bold="true"
                        OnNeedDataSource="rgGlobalComparativos_NeedDataSource"
                        ShowFooter="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowPaging="false" ShowHeadersWhenNoRecords="true">
                            <ColumnGroups>
                                <telerik:GridColumnGroup Name="Periodos" HeaderText="Resultados" HeaderStyle-Font-Bold="true"
                                    HeaderStyle-HorizontalAlign="Center" />
                            </ColumnGroups>
                            <Columns>
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
                        <ChartTitle Text="Cumplimiento global de periodos">
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
    <div style="height: 10px; clear: both;"></div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnCancelar" runat="server" AutoPostBack="false" Width="100" Text="Cancelar" OnClientClicked="CloseWindow"></telerik:RadButton>
    </div>
</asp:Content>


