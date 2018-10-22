﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaComparativaIndividual.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaComparativaIndividual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .cssGrid {
            height: 100%;
        }

    </style>
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

        function ShowInsertForm(IdEvaluadoMeta) {
            OpenSelectionWindow("../EO/VentanaAdjuntarEvidenciaMetas.aspx?pIdEvaluadoMeta=" + IdEvaluadoMeta, "rwAdjuntarArchivos", "Adjuntar evidencias")
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramCumplimiento" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdResultados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdResultados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="clear: both; height: 10px;"></div>
    <telerik:RadTabStrip ID="rtsReporteBono" runat="server" SelectedIndex="0" MultiPageID="rmpReporteIndividual">
        <Tabs>
            <telerik:RadTab SelectedIndex="0" Text="Contexto"></telerik:RadTab>
            <telerik:RadTab SelectedIndex="1" Text="Reporte"></telerik:RadTab>
            <telerik:RadTab SelectedIndex="2" Text="Gráfica"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 120px);">
        <telerik:RadMultiPage ID="rmpReporteIndividual" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>No. de empleado:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtClEmpleado" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Nombre del empleado:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNbEmpleado" runat="server"></div>
                            </td>
                        </tr>
                    </table>
                </div>
              <%--  <div class="divControlDerecha" style="width: 15%; text-align: left; margin-right: 50%;">
                    <fieldset>
                        <legend>
                            <label>Estatus de la meta</label>
                        </legend>
                        <table class="ctrlTableForm">
                            <tr>
                                <td>
                                    <div style="background: #F2F2F2; width: 50px; border:solid 1px;">
                                        <br />
                                    </div>
                                </td>
                                <td>
                                    <label>No calificada</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="background: #FF0000; width: 50px; ">
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
                <div style="clear: both; height: 20px;"></div>
                 <telerik:RadSplitter ID="rsSucesion" runat="server" Width="100%" Height="100%" BorderSize="0">
                            <telerik:RadPane ID="rpSucesion" runat="server">
                <div id="dvReporte" runat="server" style="height: calc(100% - 90px);">
                </div>
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvGrafica" runat="server">
                <div style="height: 100%;">
                    <div style="clear: both; height: 10px;"></div>
                    <div class="ctrlBasico" style="width: 50%; height: 100%">
                        <telerik:RadHtmlChart runat="server" ID="rhcCumplimientoPersonal" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                            <ChartTitle Text="Gráfica cumplimiento personal">
                                <Appearance Align="Center" Position="Top">
                                </Appearance>
                            </ChartTitle>
                            <PlotArea>
                                <Series>
                                    <telerik:ColumnSeries Stacked="false" Gap="1.5" Spacing="0.4">
                                        <Appearance>
                                            <FillStyle BackgroundColor="#d5a2bb"></FillStyle>
                                        </Appearance>
                                        <LabelsAppearance Position="OutsideEnd"></LabelsAppearance>
                                        <TooltipsAppearance Color="White"></TooltipsAppearance>
                                    </telerik:ColumnSeries>
                                </Series>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <YAxis MinValue="0" MaxValue="100" Step="10"></YAxis>
                               
                            </PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                            </Appearance>
                            <Legend>
                                <Appearance BackgroundColor="Transparent" Position="Bottom">
                                </Appearance>
                            </Legend>
                        </telerik:RadHtmlChart>
                    </div>
                    <div class="ctrlBasico" style="width: 50%; height: 100%; padding-top: 50px;">
                        <telerik:RadGrid
                            ID="vGridResultados"
                            runat="server"
                            Width="100%"
                            Height="100%"
                            AutoGenerateColumns="false"
                            HeaderStyle-Font-Bold="true"
                            OnNeedDataSource="vGridResultados_NeedDataSource"
                            AllowMultiRowSelection="false" AllowPaging="false">
                            <ClientSettings EnablePostBackOnRowClick="false">
                                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView EnableColumnsViewState="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" ShowFooter="true" EnableHeaderContextFilterMenu="false">
                                <Columns></Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <div style="height: 10px; clear: both;"></div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnCancelar" runat="server" AutoPostBack="false" Width="100" Text="Cancelar" OnClientClicked="CloseWindow"></telerik:RadButton>
    </div>
</asp:Content>

