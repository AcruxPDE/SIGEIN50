<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="CumplimientoPersonalConsecuente.aspx.cs" Inherits="SIGE.WebApp.EO.CumplimientoPersonalConsecuente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function ShowInsertForm(IdEvaluadoMeta) {
            OpenSelectionWindow("../EO/VentanaAdjuntarEvidenciaMetas.aspx?pIdEvaluadoMeta=" + IdEvaluadoMeta, "rwAdjuntarArchivos", "Adjuntar evidencias")
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 10px);">
        <div style="clear: both; height: 10px;"></div>
        <telerik:RadTabStrip ID="rtsReporteCumplimiento" runat="server" SelectedIndex="0" MultiPageID="rmpReporteCumplimiento">
            <Tabs>
                <telerik:RadTab SelectedIndex="0" Text="Contexto"></telerik:RadTab>
                <telerik:RadTab SelectedIndex="1" Text="Reporte"></telerik:RadTab>
                <telerik:RadTab SelectedIndex="2" Text="Gráfica"></telerik:RadTab>
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
                                    <label>Evaluado</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtClEmpleado" runat="server"></div>
                                </td>
                                <td colspan="2" class="ctrlTableDataContext"></td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label></label>
                                </td>
                                <td colspan="2" class="ctrlTableDataContext">
                                    <label>Período original</label></td>
                                <td colspan="2" class="ctrlTableDataContext">
                                    <label>Período consecuente</label></td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Clave del período:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtClPeriodoOriginal" runat="server"></div>
                                </td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtClPeriodoConsecuente" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Nombre del período:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNbPeriodo" runat="server"></div>
                                </td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNbConsecuente" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Puesto:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtPuestoOriginal" runat="server"></div>
                                </td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtPuestoConsecuente" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Período(s):</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtPeriodosOriginal" runat="server"></div>
                                </td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtPeriodosConsecuente" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Fecha(s):</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtFechas" runat="server"></div>
                                </td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtFechasConsecuentes" runat="server"></div>
                                </td>
                            </tr>

                        </table>
                    </div>
                    <div class="divControlIzquierda" style="width: 15%; text-align: left;">
                        <fieldset>
                            <legend>
                                <label>Estatus de la meta</label>
                            </legend>

                            <table class="ctrlTableForm">
                                <tr>
                                    <td>
                                        <div style="background: #F2F2F2; width: 50px;">
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
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvReporte" runat="server">
                    <div style="height: calc(100% - 5px);">
                        <telerik:RadGrid ID="grdCumplimiento" runat="server" Height="100%"
                            AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true"
                            AllowMultiRowSelection="false" HeaderStyle-Font-Bold="true" ShowFooter="True" OnNeedDataSource="grdCumplimiento_NeedDataSource">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView DataKeyNames="ID_EVALUADO_META_ORIGINAL" ClientDataKeyNames="ID_EVALUADO_META_ORIGINAL" AllowPaging="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                <ColumnGroups>
                                    <telerik:GridColumnGroup Name="CumplimientoValor" HeaderText="% Valor"
                                        HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="Cumplimiento" HeaderText="% Cumplimiento"
                                        HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="CumplimientoAlcanzado" HeaderText="Nivel alcanzado"
                                        HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="AdjuntarEvidecias" HeaderText="Adjuntar evidencias"
                                        HeaderStyle-HorizontalAlign="Center" />
                                </ColumnGroups>
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="60" HeaderText="META" DataField="DS_META_ORIGINAL" UniqueName="DS_META_ORIGINAL"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ColumnGroupName="CumplimientoValor" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="40" FilterControlWidth="25" HeaderText="Período original" DataField="PR_EVALUADO_ORIGINAL" UniqueName="PR_EVALUADO_ORIGINAL" DataType="System.Int32" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ColumnGroupName="CumplimientoValor" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="40" FilterControlWidth="25" HeaderText="Período consecuente" DataField="PR_EVALUADO_CONSECUENTE" UniqueName="PR_EVALUADO_CONSECUENTE" DataType="System.Int32" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn ColumnGroupName="CumplimientoAlcanzado" HeaderText="Periodo original" AllowFiltering="true" FilterControlWidth="50px">
                                        <ItemStyle Width="60px" Height="50px" HorizontalAlign="Left" />
                                        <HeaderStyle Width="60px" Height="50px" />
                                        <ItemTemplate>
                                            <div class="ctrlBasico" style="height: 50px; width: 70%; text-align: left; float: left;"><%#Eval("NIVEL_ALZANZADO_ORIGINAL") %></div>
                                            <div class="ctrlBasico" style="height: 50px; width: 15px; float: right; background: <%#Eval("COLOR_NIVEL_ORIGINAL") %>; border-radius: 5px;"></div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ColumnGroupName="CumplimientoAlcanzado" HeaderText="Período consecuente" AllowFiltering="true" FilterControlWidth="50px">
                                        <ItemStyle Width="60px" Height="50px" HorizontalAlign="Left" />
                                        <HeaderStyle Width="60px" Height="50px" />
                                        <ItemTemplate>
                                            <div class="ctrlBasico" style="height: 50px; width: 70%; text-align: left; float: left;"><%#Eval("NIVEL_ALZANZADO_CONSECUENTE") %></div>
                                            <div class="ctrlBasico" style="height: 50px; width: 15px; float: right; background: <%#Eval("COLOR_NIVEL_CONSECUENTE") %>; border-radius: 5px;"></div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn ColumnGroupName="Cumplimiento" FilterControlWidth="30px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="40" HeaderText="Período original" DataField="PR_CUMPLIMIENTO_META_ORIGINAL" UniqueName="PR_CUMPLIMIENTO_META_ORIGINAL" Aggregate="Sum" FooterAggregateFormatString="Porcentaje de cumplimiento: {0:N2}%" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ColumnGroupName="Cumplimiento" FilterControlWidth="30px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="40" HeaderText="Período consecuente" DataField="PR_CUMPLIMIENTO_META_CONSECUENTE" UniqueName="PR_CUMPLIMIENTO_META_CONSECUENTE" Aggregate="Sum" FooterAggregateFormatString="Porcentaje de cumplimiento: {0:N2}%" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn ColumnGroupName="AdjuntarEvidecias" HeaderText="Período original" AllowFiltering="false">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="30px" />
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowInsertForm(<%#Eval("ID_EVALUADO_META_ORIGINAL")%>);">Examinar</a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ColumnGroupName="AdjuntarEvidecias" HeaderText="Período consecuente" AllowFiltering="false">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="30px" />
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowInsertForm(<%#Eval("ID_EVALUADO_META_CONSECUENTE")%>);">Examinar</a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvGarfica" runat="server">
                    <div style="height: 98%;">
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico" style="width: 60%; height: 100%">
                            <telerik:RadHtmlChart runat="server" ID="rhcCumplimientoPersonal" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                <ChartTitle Text="Gráfica cumplimiento personal períodos consecuentes">
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
                        <div class="ctrlBasico" style="width: 40%;">
                            <telerik:RadGrid ID="grdResultados" ShowHeader="true" runat="server" HeaderStyle-Font-Bold="true" GridLines="None" AutoGenerateColumns="false" OnNeedDataSource="grdResultados_NeedDataSource">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView>
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="CumplimientoMetas" HeaderText="Cumplimiento"
                                            HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="60" HeaderText="No. meta" DataField="NO_META_ORIGINAL" UniqueName="NO_META_ORIGINAL"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="240" FilterControlWidth="60" HeaderText="Descripción" DataField="DS_META_ORIGINAL" UniqueName="DS_META_ORIGINAL"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ColumnGroupName="CumplimientoMetas" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Período original" DataField="PR_CUMPLIMIENTO_META_ORIGINAL" UniqueName="PR_CUMPLIMIENTO_META_ORIGINAL" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ColumnGroupName="CumplimientoMetas" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Período consecuente" DataField="PR_CUMPLIMIENTO_META_CONSECUENTE" UniqueName="PR_CUMPLIMIENTO_META_CONSECUENTE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="clear: both"></div>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
        <div style="clear: both;"></div>
    </div>
</asp:Content>
