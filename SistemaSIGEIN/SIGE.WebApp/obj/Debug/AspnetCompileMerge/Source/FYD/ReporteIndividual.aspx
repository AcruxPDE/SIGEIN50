<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ReporteIndividual.aspx.cs" Inherits="SIGE.WebApp.FYD.ReporteIndividual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .PotencialBajo {
            background-color: red;
        }

        .PotencialIntermedio {
            background-color: gold;
        }

        .PotencialAlto {
            background-color: green;
        }

        .PotencialNC {
            background-color: lightgray;
        }

        .PivotGeneral td.rpgColumnHeaderZone {
            width: calc(80px * <%= vNoPuestos %>) !important;
        }

        .PivotRadial td.rpgColumnHeaderZone {
            width: calc(100px * <%= vNoPuestos %>) !important;
        }

        .PivotComparativo td.rpgColumnHeaderZone {
            width: calc(100px * <%= vNoPeriodos %>) !important;
        }


        .divRojo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: red;
        }

        .divAmarillo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gold;
        }

        .divVerde {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: green;
        }

         .divNa {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gray;
        }

        
        table.tablaColor {
            width: 100%;
        }

        td.porcentaje {
            width: 80%;
            padding: 1px;
        }

        td.color {
            width: 10%;
            padding: 1px;
        }




    </style>

    <script>
        function OpenDescriptivoActual() {
            OpenDescriptivo(<%= vIdPuesto %>);
        }

        function OpenInventario() {
            var vURL = "../Administracion/Empleado.aspx";
            var vTitulo = "Editar Empleado";
            vURL = vURL + "?EmpleadoId=<%= vIdEmpleado %>";

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "winEmpleado", vTitulo, windowProperties);
        }

        function OpenDescriptivo(pIdPuesto) {
            var vURL = "../Administracion/VentanaVistaDescriptivo.aspx";
            var vTitulo = "Vista previa descripción del puesto";

            if (pIdPuesto != null)
                vURL = vURL + "?PuestoId=" + pIdPuesto;
            else
                vURL = vURL + "?PuestoId=<%= vIdPuesto %>";

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 100%; width: 100%; padding-top: 10px;">
        <telerik:RadTabStrip runat="server" ID="rtsReportes" MultiPageID="rmpReportes" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab runat="server" Text="Contexto"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Reporte General"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Gráfica Radial"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Gráfica de Puestos"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Reporte comparativo entre periodos"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Preguntas adicionales"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: 10px;"></div>
        <div style="height: calc(100% - 52px);">
            <telerik:RadMultiPage runat="server" ID="rmpReportes" SelectedIndex="0" Height="100%" Width="100%">
                <telerik:RadPageView ID="rpvContexto" runat="server">
                    <div class="ctrlBasico">
                        <table class="ctrlTableForm ctrlTableContext">
                            <tr>
                                <td>
                                    <label>Periodo:</label>
                                </td>
                                <td>
                                    <span id="txtNoPeriodo" runat="server" style="width: 300px;"></span>

                                </td>
                                <td>
                                    <span id="txtNbPeriodo" runat="server" style="width: 300px;"></span>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Evaluado: </label>
                                </td>
                                <td>
                                    <a href="#" id="txtClEvaluado" onclick="OpenInventario()" runat="server" style="width: 300px;"></a>

                                </td>
                                <td>
                                    <a href="#" id="txtNbEvaluado" onclick="OpenInventario()" runat="server" style="width: 300px;"></a>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Puesto: </label>
                                </td>
                                <td>
                                    <a href="#" id="txtClPuesto" onclick="OpenDescriptivo()" runat="server" style="width: 300px;"></a>
                                </td>
                                <td>
                                    <a href="#" id="txtNbPuesto" onclick="OpenDescriptivo()" runat="server" style="width: 300px;"></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Tipo de evaluación:</label></td>
                                <td colspan="2">
                                    <ul id="ulTipoEvaluacion" runat="server" style="margin-bottom: 0px;"></ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="ctrlBasico">
                        <table class="ctrlTableForm ctrlTableContext">
                            <tr>
                                <td>
                                    <label>Puestos evaluados:</label></td>
                                <td colspan="2">
                                    <telerik:RadGrid ID="grdPuestosEvaluados" HeaderStyle-Font-Bold="true" runat="server" AutoGenerateColumns="false" ShowHeader="false">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridHyperLinkColumn DataTextField="NB_PUESTO" DataNavigateUrlFields="ID_PUESTO" DataNavigateUrlFormatString="javascript:OpenDescriptivo({0})"></telerik:GridHyperLinkColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both;"></div>
                    <div>
                        <table class="ctrlTableForm ctrlTableContext">
                            <tr>
                                <td>
                                    <label>Notas:</label></td>
                                <td colspan="2">
                                    <div id="txtDsNotas" runat="server" style="min-width: 100px;"></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <label runat="server" id="lblAdvertencia" visible="false" style="color: red;"></label>
                </telerik:RadPageView>

                <telerik:RadPageView ID="rpvGeneralIndividual" runat="server">
                    <div style="height: 100%;">

                        <telerik:RadGrid ID="grdGeneralIndividual" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AutoGenerateColumns="true" OnItemCreated="grdGeneralIndividual_ItemCreated"
                            OnNeedDataSource="grdGeneralIndividual_NeedDataSource" ShowFooter="true" OnColumnCreated="grdGeneralIndividual_ColumnCreated" AllowMultiRowSelection="true" AllowPaging="false">
                            <ClientSettings EnablePostBackOnRowClick="false" Scrolling-FrozenColumnsCount="3">
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView ClientDataKeyNames="ID_COMPETENCIA" DataKeyNames="ID_COMPETENCIA" EnableColumnsViewState="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                <Columns>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <%-- <telerik:RadPivotGrid runat="server" ID="pgIndividualGeneral" OnNeedDataSource="pgIndividualGeneral_NeedDataSource" RowTableLayout="Tabular" OnCellDataBound="pgIndividualGeneral_CellDataBound" CssClass="PivotGeneral"
                            ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" ShowFilterHeaderZone="false" AllowFiltering="false" AllowSorting="true" Height="100%" TotalsSettings-GrandTotalText="Total" AllowNaturalSort="true">
                            <ClientSettings>
                                <Scrolling AllowVerticalScroll="true" />
                                <Resizing AllowColumnResize="false" />
                            </ClientSettings>
                            <ColumnHeaderCellStyle Width="50px" />
                            <TotalsSettings ColumnGrandTotalsPosition="None" ColumnsSubTotalsPosition="None" RowsSubTotalsPosition="None" />
                            <Fields>
                                <telerik:PivotGridRowField Caption="Número" DataField="NO_ORDEN" CellStyle-Width="0">
                                    <CellTemplate>&nbsp;</CellTemplate>
                                </telerik:PivotGridRowField>
                                <telerik:PivotGridRowField Caption="Color" DataField="CL_COLOR" CellStyle-Width="50">
                                    <CellTemplate>
                                        <table style="height: 100%;">
                                            <tr>
                                                <td style="border-width: 0px; padding: 0px;">
                                                    <div style="height: 100%; border-radius: 5px; background: <%# Container.DataItem.ToString() %>;">&nbsp;</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </CellTemplate>
                                </telerik:PivotGridRowField>
                                <telerik:PivotGridRowField Caption="Factor" DataField="NB_COMPETENCIA" CellStyle-Width="200px"></telerik:PivotGridRowField>
                                <telerik:PivotGridRowField Caption="Descripción" DataField="DS_COMPETENCIA"></telerik:PivotGridRowField>
                                <telerik:PivotGridColumnField Caption="Puesto" DataField="ID_PUESTO"></telerik:PivotGridColumnField>
                                <telerik:PivotGridAggregateField Aggregate="Average" DataField="NO_TOTAL_TIPO_EVALUACION" CellStyle-Width="80px">
                                    <CellTemplate>
                                        <table style="border: 0px solid white !important; width: 100%">
                                            <tr>
                                                <td style="border-width: 0px; padding: 1px;">
                                                    <div runat="server" id="divPromedio" style="width: 95%; margin-bottom: auto; margin-top: auto; float: left; text-align: right;">
                                                        <%# String.Format("{0:N2}", Container.DataItem) %>%
                                                    </div>
                                                    <div runat="server" id="divNa" style="width: 95%; margin-bottom: auto; margin-top: auto; float: left; text-align: right;">
                                                        N/A
                                                    </div>
                                                </td>
                                                <td style="border-width: 0px; padding: 1px; width: 20px;">
                                                    <div runat="server" id="divColorComparacion" style="float: right; height: 80%; width: 15px; border-radius: 5px;">
                                                        &nbsp;
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </CellTemplate>
                                </telerik:PivotGridAggregateField>
                            </Fields>
                        </telerik:RadPivotGrid>--%>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvReporte360" runat="server" Height="100%">
                    <div style="height: calc(100% - 5px);">
                        <div style="width: 500px; height: 100%; max-height: 600px; float: left;">
                            <telerik:RadHtmlChart runat="server" ID="rhcPenslamiento" Height="100%" Transitions="true">
                                <Legend>
                                    <Appearance Position="Top" Visible="true"></Appearance>
                                </Legend>
                                <PlotArea>
                                    <XAxis Color="Black" MajorTickType="Outside" MinorTickType="Outside" Reversed="false" StartAngle="180" DataLabelsField="NB_COMPETENCIA">
                                        <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Step="1" Skip="0">
                                            <ClientTemplate>
                                                        #= value #
                                            </ClientTemplate>
                                        </LabelsAppearance>
                                        <MajorGridLines Width="1"></MajorGridLines>
                                        <MinorGridLines Visible="false"></MinorGridLines>
                                    </XAxis>

                                    <YAxis Color="black" MajorTickSize="1" MajorTickType="Outside" MaxValue="6" MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false" Step="1">
                                        <LabelsAppearance RotationAngle="0" Skip="0" Step="1"></LabelsAppearance>
                                        <MajorGridLines Width="1" />
                                        <MinorGridLines Visible="true" />
                                    </YAxis>
                                </PlotArea>
                            </telerik:RadHtmlChart>
                        </div>
                        <div style="height: 100%; float: left; width: calc(100% - 500px)">
                            <telerik:RadPivotGrid runat="server" ID="pgReporte360" OnNeedDataSource="pgReporte360_NeedDataSource" RowTableLayout="Tabular" OnCellDataBound="pgReporte360_CellDataBound" EmptyValue="0" CssClass="PivotRadial"
                                ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" ShowFilterHeaderZone="false" AllowFiltering="false" AllowSorting="true" Height="100%" Width="100%">
                                <ClientSettings>
                                    <Scrolling AllowVerticalScroll="true" />
                                    <Resizing AllowColumnResize="false" />
                                </ClientSettings>
                                <TotalsSettings ColumnGrandTotalsPosition="None" ColumnsSubTotalsPosition="None" RowGrandTotalsPosition="None" RowsSubTotalsPosition="None" />
                                <SortExpressions>
                                    <telerik:PivotGridSortExpression FieldName="NO_ORDEN_CONSECUTIVO" SortOrder="Ascending" />
                                </SortExpressions>
                                <Fields>
                                    <telerik:PivotGridRowField Caption="Número" DataField="NO_ORDEN_CONSECUTIVO" CellStyle-Width="50"></telerik:PivotGridRowField>
                                    <telerik:PivotGridRowField Caption="Color" DataField="CL_COLOR" CellStyle-Width="50">
                                        <CellTemplate>
                                            <div style="height: 100%; border-radius: 5px; background: <%# Container.DataItem.ToString() %>;">
                                                <br />
                                            </div>
                                        </CellTemplate>
                                    </telerik:PivotGridRowField>
                                    <telerik:PivotGridRowField Caption="Factor" DataField="NB_COMPETENCIA" CellStyle-Width="200"></telerik:PivotGridRowField>
                                    <telerik:PivotGridColumnField Caption="" DataField="CL_PUESTO" SortOrder="None"></telerik:PivotGridColumnField>
                                    <telerik:PivotGridAggregateField DataField="NO_VALOR_COMPETENCIA" Aggregate="Average">
                                        <CellTemplate>
                                            <div style="text-align: right;"><%# Container.DataItem %> </div>
                                        </CellTemplate>
                                    </telerik:PivotGridAggregateField>
                                </Fields>
                            </telerik:RadPivotGrid>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvReportePuestos" runat="server">
                    <div style="height: calc(100% - 5px); width: 100%;">
                        <div style="width: 500px; height: 100%; max-height: 600px; float: left;">
                            <telerik:RadHtmlChart runat="server" ID="rhcPuestos" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <Legend>
                                    <Appearance BackgroundColor="Transparent" Position="Top"></Appearance>
                                </Legend>
                                <PlotArea>
                                    <Appearance>
                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                    </Appearance>
                                    <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside" Reversed="false">
                                        <LabelsAppearance DataFormatString="{0}" RotationAngle="45" Skip="0" Step="1"></LabelsAppearance>
                                        <TitleAppearance Position="Center" RotationAngle="0"></TitleAppearance>
                                    </XAxis>
                                    <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                        MaxValue="6" MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false" Step="1">
                                        <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1"></LabelsAppearance>
                                    </YAxis>
                                </PlotArea>
                            </telerik:RadHtmlChart>
                        </div>
                        <div style="height: 100%; float: left; width: calc(100% - 500px)">
                            <telerik:RadPivotGrid runat="server" ID="pgReportePuestos" OnNeedDataSource="pgReportePuestos_NeedDataSource" RowTableLayout="Tabular" OnCellDataBound="pgReportePuestos_CellDataBound" EmptyValue="0" CssClass="PivotRadial"
                                ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" ShowFilterHeaderZone="false" AllowFiltering="false" AllowSorting="true" Height="100%" Width="100%">
                                <ClientSettings>
                                    <Scrolling AllowVerticalScroll="true" />
                                    <Resizing AllowColumnResize="false" />
                                </ClientSettings>
                                <TotalsSettings ColumnGrandTotalsPosition="None" ColumnsSubTotalsPosition="None" RowGrandTotalsPosition="None" RowsSubTotalsPosition="None" />
                                <SortExpressions>
                                    <telerik:PivotGridSortExpression FieldName="NO_ORDEN_CONSECUTIVO" SortOrder="Ascending" />
                                </SortExpressions>
                                <Fields>
                                    <telerik:PivotGridRowField Caption="Número" DataField="NO_ORDEN_CONSECUTIVO" CellStyle-Width="50"></telerik:PivotGridRowField>
                                    <telerik:PivotGridRowField Caption="Color" DataField="CL_COLOR" CellStyle-Width="50">
                                        <CellTemplate>
                                            <div style="height: 100%; border-radius: 5px; background: <%# Container.DataItem.ToString() %>;">
                                                <br />
                                            </div>
                                        </CellTemplate>
                                    </telerik:PivotGridRowField>
                                    <telerik:PivotGridRowField Caption="Factor" DataField="NB_COMPETENCIA" CellStyle-Width="200"></telerik:PivotGridRowField>
                                    <telerik:PivotGridColumnField Caption="" DataField="CL_PUESTO" SortOrder="None"></telerik:PivotGridColumnField>
                                    <telerik:PivotGridAggregateField DataField="NO_VALOR_COMPETENCIA" Aggregate="Average">
                                        <CellTemplate>
                                            <div style="text-align: right;"><%# Container.DataItem %> </div>
                                        </CellTemplate>
                                    </telerik:PivotGridAggregateField>
                                </Fields>
                            </telerik:RadPivotGrid>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvReporteComparativo" runat="server">
                    <div style="height: 100%;">
                        <telerik:RadGrid ID="rgComparativo" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AutoGenerateColumns="false"
                            ShowFooter="true" AllowMultiRowSelection="true" AllowPaging="false" OnItemDataBound="rgComparativo_ItemDataBound">
                            <ClientSettings EnablePostBackOnRowClick="false" Scrolling-FrozenColumnsCount="2">
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView ClientDataKeyNames="ID_COMPETENCIA" DataKeyNames="ID_COMPETENCIA" EnableColumnsViewState="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="CL_COLOR" UniqueName="CL_COLOR" ItemStyle-HorizontalAlign="Center" HeaderText="">
                                        <ItemStyle Width="20px" Height="15px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="30px" Height="20px" />
                                        <ItemTemplate>
                                            <div class="ctrlBasico" style="height: 60px; width: 20px; float: left; background: <%# Eval("CL_COLOR") %>; border-radius: 5px;"></div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" HeaderText="" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <%--              <telerik:RadPivotGrid runat="server" ID="rpgComparativo" OnNeedDataSource="rpgComparativo_NeedDataSource" RowTableLayout="Tabular" ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowFilterHeaderZone="false" AllowFiltering="false" AllowSorting="true" CssClass="PivotComparativo"
                        ShowColumnHeaderZone="false" Height="100%" Width="100%"   OnCellDataBound="rpgComparativo_CellDataBound"   TotalsSettings-GrandTotalText="Total" NoRecordsText="No seleccionó periodos para comparar">
                        <ClientSettings>
                            <Scrolling AllowVerticalScroll="true" />
                            <Resizing AllowColumnResize="false" />
                        </ClientSettings>
                        <TotalsSettings ColumnsSubTotalsPosition="None" RowsSubTotalsPosition="None" ColumnGrandTotalsPosition="None"  />
                        <Fields>
                            <telerik:PivotGridRowField Caption="Número" DataField="NO_LINEA" CellStyle-Width="0">
                                <CellTemplate>&nbsp;</CellTemplate>
                            </telerik:PivotGridRowField>
                            <telerik:PivotGridRowField Caption="Color" DataField="CL_COLOR" CellStyle-Width="25">
                                <CellTemplate>
                                    <div style="height: 100%; border-radius: 5px; background: <%# Container.DataItem.ToString() %>;">
                                        &nbsp;
                                    </div>
                                </CellTemplate>
                            </telerik:PivotGridRowField>
                            <telerik:PivotGridRowField Caption="Competencia" DataField="NB_COMPETENCIA" CellStyle-Width="350"></telerik:PivotGridRowField>
                            <telerik:PivotGridColumnField DataField="CL_PERIODO" Caption="Periodos"></telerik:PivotGridColumnField>
                            <telerik:PivotGridAggregateField DataField="ID_PERIODO" UniqueName="ID_PERIODO">
                                <CellTemplate >
                                    <div id="dvResultado" runat="server"></div>--%>
                    <%--    <table style="border: 0px solid white !important; width: 100%">
                                        <tr>
                                            <td style="border-width: 0px; padding: 1px;">
                                                <div runat="server" id="divPromedioC" style="width: 100%; margin-bottom: auto; margin-top: auto; float: left; text-align: right;">
                                                    <%# String.Format("{0:N2}", Container.DataItem) %>%
                                                </div>
                                                <div runat="server" id="divNaC" style="width: 95%; margin-bottom: auto; margin-top: auto; float: left; text-align: right;">
                                                    N/A
                                                </div>
                                            </td>
                                            <td style="border-width: 0px; padding: 1px; width: 20px;">
                                                <div runat="server" id="divColorComparacionC" style="float: right; height: 80%; width: 15px; border-radius: 5px;">
                                                    &nbsp;
                                                </div>
                                            </td>
                                        </tr>
                                    </table>--%>
                    <%--                           </CellTemplate>
                                <RowGrandTotalCellTemplate >
                                </RowGrandTotalCellTemplate>
                            </telerik:PivotGridAggregateField>
                        </Fields>
                    </telerik:RadPivotGrid>--%>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvAdicionales" runat="server">
                    <div style="height: calc(100% - 60px);">
                        <telerik:RadGrid
                            ID="rgResultadosPreguntas"
                            runat="server"
                            Height="100%"
                            AutoGenerateColumns="false"
                            EnableHeaderContextMenu="false"
                            AllowSorting="true"
                            AllowMultiRowSelection="false"
                            OnNeedDataSource="rgResultadosPreguntas_NeedDataSource"
                            OnDetailTableDataBind="rgResultadosPreguntas_DetailTableDataBind"
                            OnItemCommand="rgResultadosPreguntas_ItemCommand">
                            <ClientSettings EnableAlternatingItems="false">
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="false" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView DataKeyNames="ID_CAMPO_PREGUNTA" HierarchyDefaultExpanded="true" EnableHierarchyExpandAll="true" ClientDataKeyNames="ID_CAMPO_PREGUNTA" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="false">
                                <DetailTables>
                                    <telerik:GridTableView DataKeyNames="ID_CAMPO"
                                        ClientDataKeyNames="ID_CAMPO" Name="PruebaDetails" Width="100%">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" HeaderText="Respuesta" DataField="NB_VALOR" UniqueName="NB_VALOR" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        </Columns>
                                    </telerik:GridTableView>
                                </DetailTables>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="NB_PREGUNTA" HeaderStyle-Width="250" HeaderText="Preguntas" Visible="true" Display="" HeaderStyle-Font-Bold="true" UniqueName="NB_PREGUNTA" ItemStyle-BackColor="#FF7400" ItemStyle-ForeColor="White">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>
</asp:Content>
