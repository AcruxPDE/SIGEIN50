<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaReporteBono.aspx.cs" Inherits="SIGE.WebApp.EO.ReporteBono" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OpenSelectionWindows(pURL, pVentana, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var WindowsProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pVentana, pTitle, WindowsProperties);
        }

        function OpenWindowPeriodos() {
            OpenSelectionWindows("../Comunes/SeleccionPeriodosDesempeno.aspx?CL_TIPO=Bono&ID_PERIODO=" + '<%= vIdPeriodo %>', "winSeleccion", "Seleccion de períodos a comparar");
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var vJsonPeriodo = JSON.stringify({ clTipo: "PERIODODESEMPENO", oSeleccion: pDato });
                var ajaxManager = $find('<%= ramPeriodos.ClientID%>');
                ajaxManager.ajaxRequest(vJsonPeriodo);
            }
        }

        function OpenWindowComparar() {
            OpenSelectionWindows("VentanaComparativaBonos.aspx", "winBonos", "Consulta Bono comparativa - Evaluación de competencias");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpPeriodos" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramPeriodos" runat="server" OnAjaxRequest="ramPeriodos_AjaxRequest" DefaultLoadingPanelID="ralpPeriodos">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgComparativos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadTabStrip ID="rtsReporteBono" runat="server" SelectedIndex="0" MultiPageID="rmpReporteBono">
        <Tabs>
            <telerik:RadTab SelectedIndex="0" Text="Contexto"></telerik:RadTab>
            <telerik:RadTab SelectedIndex="1" Text="Bono"></telerik:RadTab>
            <telerik:RadTab SelectedIndex="2" Text="Comparar"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: 10px;"></div>
    <div style="height: calc(100% - 60px);">
        <telerik:RadMultiPage ID="rmpReporteBono" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                   <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtClPeriodo" runat="server"></div>
                            </td>
                        </tr>
                      <%--  <tr>
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView1" runat="server">
                <div style="width: 100%;">
                    <telerik:RadGrid ID="grdEvaluados" runat="server" HeaderStyle-Font-Bold="true"
                        AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true"
                        AllowMultiRowSelection="false" OnNeedDataSource="grdEvaluados_NeedDataSource" OnItemDataBound="grdEvaluados_ItemDataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EMPLEADO" ClientDataKeyNames="ID_EMPLEADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <ColumnGroups>
                                <telerik:GridColumnGroup Name="Bono" HeaderText="% de desempeño y bono"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" />
                            </ColumnGroups>
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" ColumnGroupName="Bono" HeaderText="Sueldo mensual" DataField="MN_SUELDO" UniqueName="MN_SUELDO" DataType="System.Int32" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" ColumnGroupName="Bono" HeaderText="Bono máximo" DataField="MN_TOPE_BONO" UniqueName="MN_TOPE_BONO" DataType="System.Int32" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" ColumnGroupName="Bono" HeaderText="% de bono" DataField="NO_MONTO_BONO" UniqueName="NO_MONTO_BONO" DataType="System.Double" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" ColumnGroupName="Bono" HeaderText="% desempeño promedio" DataField="PR_CUMPLIMIENTO_EVALUADO" UniqueName="PR_CUMPLIMIENTO_EVALUADO" DataType="System.Double" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" ColumnGroupName="Bono" HeaderText="Total bono" DataField="MN_BONO_TOTAL" UniqueName="MN_BONO_TOTAL" DataType="System.Double" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvComparativo" runat="server">
                <telerik:RadGrid
                    ID="rgComparativos"
                    runat="server"
                    Height="90%"
                    AutoGenerateColumns="false"
                    EnableHeaderContextMenu="true"
                    AllowSorting="true"
                    AllowMultiRowSelection="false" HeaderStyle-Font-Bold="true"
                    OnNeedDataSource="rgComparativos_NeedDataSource"
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
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
