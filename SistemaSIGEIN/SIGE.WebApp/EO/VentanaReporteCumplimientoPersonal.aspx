<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaReporteCumplimientoPersonal.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaReporteCumplimientoPersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function ShowInsertForm(IdEvaluadoMeta) {
            OpenSelectionWindow("VentanaAdjuntarEvidenciaMetas.aspx?pIdEvaluadoMeta=" + IdEvaluadoMeta + "&pFgConsulta=SI", "rwAdjuntarArchivos", "Adjuntar evidencias")
        }

        function onCloseWindow(oWnd, args) {
            $find("<%=grdCumplimiento.ClientID%>").get_masterTableView().rebind();
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
            var vIdEvaluado = ('<%= vIdEvaluado%>');
            var vIdPeriodo = ('<%= vIdPeriodo%>');
            OpenSelectionWindow("../Comunes/SeleccionPeriodosDesempeno.aspx?ID_EVALUADO=" + vIdEvaluado + "&ID_PERIODO=" + vIdPeriodo + "&CL_TIPO=Individual", "winSeleccion", "Seleccion de períodos a comparar");
        }

        function OpenWindowComparar() {
            var vIdEvaluado = ('<%= vIdEvaluado%>');
            OpenSelectionWindow("VentanaComparativaIndividual.aspx?ID_EVALUADO=" + vIdEvaluado, "winBonos", "Consulta individual comparativa - Evaluación del desempeño");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramReportes" runat="server" OnAjaxRequest="ramReportes_AjaxRequest" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdCumplimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rhtColumnChart" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramReportes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgComparativos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 10px);">
        <div style="clear: both; height: 10px;"></div>
        <telerik:RadTabStrip ID="rtsReporteCumplimiento" runat="server" SelectedIndex="0" MultiPageID="rmpReporteCumplimiento">
            <Tabs>
                <telerik:RadTab SelectedIndex="0" Text="Contexto"></telerik:RadTab>
                <telerik:RadTab SelectedIndex="1" Text="Reporte"></telerik:RadTab>
                <telerik:RadTab SelectedIndex="2" Text="Gráfica"></telerik:RadTab>
                <telerik:RadTab SelectedIndex="3" Text="Selección de períodos a comparar"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: 10px;"></div>
        <div style="height: calc(100% - 80px);">
            <telerik:RadMultiPage ID="rmpReporteCumplimiento" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="rpvContexto" runat="server">
                    <div class="divControlIzquierda" style="width: 60%; text-align: left;">
                        <table class="ctrlTableForm">
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Período:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtClPeriodo" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Descripción:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNbPeriodo" runat="server"></div>
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
                                    <label>No. de empleado:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNoEmpleado" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Nombre del empleado:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNbEmpleado" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Clave del puesto:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtClPuesto" runat="server"></div>
                                </td>

                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Puesto:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtPuesto" runat="server"></div>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Periodo(s):</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtPeriodos" runat="server"></div>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                <%--    <div class="divControlIzquierda" style="width: 15%; text-align: left;">
                        <fieldset>
                            <legend>
                                <label>Estatus de la meta</label>
                            </legend>

                            <table class="ctrlTableForm">
                                <tr>
                                    <td>
                                        <div style="background: #F2F2F2; width: 50px; border: solid 1px;">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>No calificada</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="background: #FF0000; width: 50px;">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>No alcanzada</label></td>
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
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvReporte" runat="server">
                    <div style="height: calc(100% - 5px);">
                        <telerik:RadSplitter ID="rsSucesion" runat="server" Width="100%" Height="100%" BorderSize="0">
                            <telerik:RadPane ID="rpSucesion" runat="server">
                                <telerik:RadGrid ID="grdCumplimiento" runat="server" Width="100%"
                                    AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true" HeaderStyle-Font-Bold="true"
                                    AllowMultiRowSelection="false" OnNeedDataSource="grdCumplimiento_NeedDataSource" OnItemDataBound="grdCumplimiento_ItemDataBound" ShowFooter="True">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" SaveScrollPosition="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView DataKeyNames="ID_EVALUADO_META" ClientDataKeyNames="ID_EVALUADO_META" AllowPaging="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="NivelCompetencia" HeaderText="Nivel de meta"
                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" />
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="350" FilterControlWidth="250" HeaderText="Descripción" DataField="DS_META" UniqueName="DS_META" HeaderStyle-Font-Bold="true" FooterStyle-BackColor="#A20804"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="35" HeaderText="Ponderación" DataField="PR_EVALUADO" UniqueName="PR_EVALUADO" DataType="System.Int32" FooterStyle-BackColor="#A20804" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" FooterStyle-BorderColor="#A20804" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="30" HeaderText="Actual" DataField="NB_CUMPLIMIENTO_ACTUAL" UniqueName="NB_CUMPLIMIENTO_ACTUAL" FooterStyle-BackColor="#A20804" ColumnGroupName="NivelCompetencia" FooterStyle-BorderColor="#A20804" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="30" HeaderText="Mínima" DataField="NB_CUMPLIMIENTO_MINIMO" UniqueName="NB_CUMPLIMIENTO_MINIMO" FooterStyle-BackColor="#A20804" FooterStyle-BorderColor="#A20804" ColumnGroupName="NivelCompetencia" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="30" HeaderText="Satisfactoria" DataField="NB_CUMPLIMIENTO_SATISFACTORIO" UniqueName="NB_CUMPLIMIENTO_SATISFACTORIO" FooterStyle-BorderColor="#A20804" FooterStyle-BackColor="#A20804" ColumnGroupName="NivelCompetencia" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="30" HeaderText="Sobresaliente" DataField="NB_CUMPLIMIENTO_SOBRESALIENTE" UniqueName="NB_CUMPLIMIENTO_SOBRESALIENTE" FooterStyle-BorderColor="#A20804" FooterStyle-BackColor="#A20804" ColumnGroupName="NivelCompetencia" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="30" HeaderText="Resultado" DataField="NB_RESULTADO" UniqueName="NB_RESULTADO" FooterStyle-BorderColor="#A20804" ItemStyle-HorizontalAlign="Center" FooterStyle-BackColor="#A20804" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Nivel alcanzado" HeaderStyle-HorizontalAlign="Center" AllowFiltering="true" HeaderStyle-Width="100" FilterControlWidth="50px" HeaderStyle-Font-Bold="true" FooterText="Total:" FooterStyle-ForeColor="White" FooterStyle-Font-Bold="true" FooterStyle-BorderColor="#A20804" FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#A20804">
                                                <ItemStyle Width="120px" Height="30px" HorizontalAlign="Left" />
                                                <HeaderStyle Width="120px" Height="30px" />
                                                <ItemTemplate>
                                                    <div class="ctrlBasico" style="height: 30px; width: 70%; text-align: left; float: left;"><%#Eval("NIVEL_ALZANZADO") %></div>
                                                    <div class="ctrlBasico" style="height: 30px; width: 15px; float: right; background: <%#Eval("COLOR_NIVEL") %>; border-radius: 5px;"></div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn FilterControlWidth="40px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100" HeaderText="Cumplimiento" DataField="PR_CUMPLIMIENTO_META" UniqueName="PR_CUMPLIMIENTO_META" Aggregate="Sum" FooterAggregateFormatString="{0:N2}%" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Evidencia" DataField="FG_EVIDENCIA" UniqueName="FG_EVIDENCIA" AllowFiltering="false" HeaderStyle-Font-Bold="true">
                                                <HeaderStyle Width="70" />
                                                <ItemTemplate>
                                                    <div style="width: 90%; text-align: center; cursor: pointer;">
                                                        <img src='<%# Eval("FG_EVIDENCIA").ToString().Equals("True") ? "../Assets/images/Aceptar.png" : "../Assets/images/Cancelar.png"  %>' onclick="return ShowInsertForm(<%#Eval("ID_EVALUADO_META")%>);" title="Selecciona para ver evidencias adjuntas" />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%--<telerik:GridTemplateColumn HeaderText="Adjuntar evidencias" AllowFiltering="false">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="30px" />
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowInsertForm(<%#Eval("ID_EVALUADO_META")%>);">Examinar</a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>
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
                <telerik:RadPageView ID="rpvGarfica" runat="server">
                    <div style="height: 96%;">
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" BorderSize="0">
                            <telerik:RadPane ID="RadPane1" runat="server">
                                <div style="clear: both; height: 10px;"></div>
                                <div class="ctrlBasico" style="width: 100%; height: 80%;">
                                    <telerik:RadHtmlChart runat="server" ID="rhcCumplimientoPersonal" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                        <ChartTitle Text="Gráfica cumplimiento individual">
                                            <Appearance Align="Center" Position="Top">
                                            </Appearance>
                                        </ChartTitle>
                                        <Legend>
                                            <Appearance Position="Right" Visible="true">
                                            </Appearance>
                                        </Legend>
                                        <PlotArea>
                                            <YAxis MinValue="0" MaxValue="100" Step="10"></YAxis>
                                        </PlotArea>
                                    </telerik:RadHtmlChart>
                                </div>
                                <div style="clear: both"></div>
                                <div class="ctrlBasico" style="width: 100%;">
                                    <telerik:RadGrid ID="grdResultados" ShowHeader="true" HeaderStyle-Font-Bold="true" runat="server" GridLines="None" AutoGenerateColumns="false" OnNeedDataSource="grdResultados_NeedDataSource">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="60" HeaderText="No. meta" DataField="NO_META" UniqueName="NO_META"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="240" FilterControlWidth="60" HeaderText="Descripción" DataField="DS_META" UniqueName="DS_META"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Cumplimiento" DataField="PR_CUMPLIMIENTO_META" UniqueName="PR_CUMPLIMIENTO_META" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                                <div style="clear: both"></div>
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
                <telerik:RadPageView ID="RadPageView1" runat="server">
                    <div style="height: 100%;">
                        <telerik:RadGrid
                            ID="rgComparativos"
                            runat="server"
                            Height="90%"
                            AutoGenerateColumns="false"
                            EnableHeaderContextMenu="true"
                            AllowSorting="true"
                            AllowMultiRowSelection="false"
                            HeaderStyle-Font-Bold="true"
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
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="90" HeaderText="Clave" DataField="clPeriodo" UniqueName="clPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="250" HeaderText="Período" DataField="nbPeriodo" UniqueName="nbPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="250" HeaderText="Descripción" DataField="dsPeriodo" UniqueName="dsPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
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
        <div style="clear: both;"></div>
    </div>
</asp:Content>
