<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaCumplimientoGlobal.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaCumplimientoGlobal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>
        function OpenCumPersonal(pIdEvaluado, pIdPeriodo) {
            var vURL = "VentanaReporteCumplimientoPersonal.aspx";
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

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var vJsonPeriodo = JSON.stringify({ clTipo: "PERIODODESEMPENO", oSeleccion: pDato });
                var ajaxManager = $find('<%= ramReportes.ClientID%>');
                ajaxManager.ajaxRequest(vJsonPeriodo);
            }
        }

        function OpenWindowPeriodos() {
            var vIdPeriodo = ('<%= vIdPeriodo%>');
            OpenSelectionWindow("../Comunes/SeleccionPeriodosDesempeno.aspx?ID_PERIODO=" + vIdPeriodo + "&CL_TIPO=Global", "winSeleccion", "Seleccion de períodos a comparar");
        }

        function OpenWindowComparar() {
            OpenSelectionWindow("VentanaComparativaGlobal.aspx?", "winBonos", "Consulta general comparativa - Evaluación de competencias");
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
        <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
            <Tabs>
                <telerik:RadTab Text="Contexto"></telerik:RadTab>
                <telerik:RadTab Text="Reporte"></telerik:RadTab>
                <telerik:RadTab Text="Gráfica"></telerik:RadTab>
                <telerik:RadTab Text="Selección de períodos a comparar"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: calc(100% - 60px);">
                      <div style="clear: both; height: 10px;"></div>
            <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="rpvContexto" runat="server">         
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
                                        <label>Tipo de período:</label></td>
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
                    <div style="height: calc(100% - 10px);">
                        <telerik:RadSplitter ID="rsSucesion" runat="server" Width="100%" Height="100%" BorderSize="0">
                            <telerik:RadPane ID="rpSucesion" runat="server">
                                <telerik:RadGrid ID="rgEvaluados"
                                    runat="server"
                                    Height="100%"
                                    Width="100%"
                                    AllowSorting="true"
                                    ShowFooter="true"
                                    HeaderStyle-Font-Bold="true"
                                    AllowMultiRowSelection="true"
                                    OnNeedDataSource="rgEvaluados_NeedDataSource"
                                    OnItemDataBound="rgEvaluados_ItemDataBound">
                                    <ClientSettings EnablePostBackOnRowClick="false">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <MasterTableView DataKeyNames="ID_EVALUADO, ID_PERIODO" ClientDataKeyNames="ID_EVALUADO, ID_PERIODO" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true" AllowPaging="false" AllowFilteringByColumn="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="300" HeaderText="Puesto" DataField="NB_PUESTO_PERIODO" UniqueName="NB_PUESTO_PERIODO" FooterStyle-BackColor="#A20804" FooterStyle-BorderColor="#A20804" CurrentFilterFunction="Contains" FilterControlWidth="220" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridHyperLinkColumn HeaderStyle-Width="300" UniqueName="NB_EVALUADO" HeaderText="Nombre completo" DataTextField="NB_EVALUADO" FooterStyle-BackColor="#A20804" FooterStyle-BorderColor="#A20804" DataNavigateUrlFields="ID_EVALUADO, ID_PERIODO" DataNavigateUrlFormatString="javascript:OpenCumPersonal({0},{1})" CurrentFilterFunction="Contains" FilterControlWidth="220" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridHyperLinkColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Valor cumplido" DataField="PR_CUMPLIMIENTO_EVALUADO" UniqueName="PR_CUMPLIMIENTO_EVALUADO" FooterStyle-BackColor="#A20804" FooterStyle-BorderColor="#A20804" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" CurrentFilterFunction="Contains" FilterControlWidth="50" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Valor ponderado" FooterStyle-Font-Bold="true" DataField="PR_EVALUADO" UniqueName="PR_EVALUADO" ItemStyle-HorizontalAlign="Right" FooterStyle-BackColor="#A20804" FooterStyle-BorderColor="#A20804" DataFormatString="{0:N2}%" FooterText="Cumplimiento general:" FooterStyle-HorizontalAlign="Right" FooterStyle-ForeColor="White" CurrentFilterFunction="Contains" FilterControlWidth="60" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn CurrentFilterFunction="Contains" HeaderStyle-Width="100" FooterStyle-Font-Bold="true" HeaderText="Aporte a cumplimiento general" DataField="C_GENERAL" UniqueName="C_GENERAL" Aggregate="Sum" FooterAggregateFormatString="{0:N2}%" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" FilterControlWidth="60" AutoPostBackOnFilter="true" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPane>
                            <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
                                <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                    <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="300px" Title="Código de color" Height="100%">
                                        <div style="padding: 10px; text-align: justify;">
                                            <telerik:RadGrid ID="grdCodigoColores"
                                                runat="server"
                                                Height="350"
                                                Width="250"
                                                AllowSorting="true"
                                                AllowFilteringByColumn="true"
                                                HeaderStyle-Font-Bold="true"
                                                ShowHeader="true"
                                                OnNeedDataSource="grdCodigoColores_NeedDataSource">
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                                </ClientSettings>
                                                <PagerStyle AlwaysVisible="true" />
                                                <GroupingSettings CaseSensitive="false" />
                                                <MasterTableView AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                                        AddNewRecordText="Insertar" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Color" HeaderStyle-Width="60" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <div style="margin: auto; width: 25px; border: 1px solid gray; background: <%# Eval("COLOR")%>; border-radius: 5px;">&nbsp;&nbsp;</div>
                                                                &nbsp;
                                                        </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadSlidingPane>
                                </telerik:RadSlidingZone>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvGrafica" runat="server" Height="100%">
                    <div style="height: calc(100% - 10px);">
                        <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" BorderSize="0">
                            <telerik:RadPane ID="RadPane1" runat="server">
                                <telerik:RadHtmlChart runat="server" ID="rhcGraficaGlobal" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                    <ChartTitle Text="Cumplimiento general del período">
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
                            </telerik:RadPane>
                            <telerik:RadPane ID="RadPane2" runat="server" Width="30">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" CollapseMode="Forward" EnableResize="false" Width="300px" Title="Código de color" Height="100%">
                                        <div style="padding: 10px; text-align: justify;">
                                            <telerik:RadGrid ID="rgColores2"
                                                runat="server"
                                                Height="350"
                                                Width="250"
                                                AllowSorting="true"
                                                AllowFilteringByColumn="true"
                                                HeaderStyle-Font-Bold="true"
                                                ShowHeader="true"
                                                OnNeedDataSource="rgColores2_NeedDataSource">
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                                </ClientSettings>
                                                <PagerStyle AlwaysVisible="true" />
                                                <GroupingSettings CaseSensitive="false" />
                                                <MasterTableView AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                                        AddNewRecordText="Insertar" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Color" HeaderStyle-Width="60" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <div style="margin: auto; width: 25px; border: 1px solid gray; background: <%# Eval("COLOR")%>; border-radius: 5px;">&nbsp;&nbsp;</div>
                                                                &nbsp;
                                                        </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadSlidingPane>
                                </telerik:RadSlidingZone>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                        
                    
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvComparar" runat="server" Height="100%">
                    <div style="height: calc(100% - 50px);">
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
                            <telerik:RadButton ID="btnSeleccionar" runat="server" AutoPostBack="false" Width="200" Text="Seleccionar períodos" OnClientClicked="OpenWindowPeriodos"></telerik:RadButton>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="btnComparar" runat="server" AutoPostBack="false" Width="100" Text="Comparar" OnClientClicked="OpenWindowComparar"></telerik:RadButton>
                        </div>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
</asp:Content>
