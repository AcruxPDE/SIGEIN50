<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ConsultaTabuladorSueldos.aspx.cs" Inherits="SIGE.WebApp.MPC.ConsultaTabuladorSueldos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .RadGrid1Class {
            background-color: #FFFFFF;
        }

        .RadGrid2Class {
            background-color: #E6E6FA;
        }
    </style>
    <script type="text/javascript">

        function GetWindowProperties() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            return {
                width: browserWnd.innerWidth - 30,
                height: browserWnd.innerHeight - 20
            };
        }

        function OpenTabuladorEmpleadoSueldo() {
            var vPropierties = GetWindowProperties();
            openChildDialog("SeleccionTabuladorEmpleado.aspx?&IdTabulador=" + <%=vIdTabulador%> + "&vClTipoSeleccion=CONSULTAS" + "&CatalogoCl=TABULADOR_SUELDOS", "winSeleccion", "Selección de empleados", vPropierties);
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vDatosSeleccionados = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "TABULADOR_SUELDOS":
                        for (var i = 0; i < pDato.length; ++i) {
                            arr.push(pDato[i].idEmpleado);
                        }
                        var datos = JSON.stringify({ clTipo: vDatosSeleccionados.clTipoCatalogo, arrEmpleados: arr });
                        InsertEmpleado(datos);
                        break;
                }
            }
        }

        function InsertEmpleado(pDato) {
            var ajaxManager = $find('<%= ramConsultas.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function OpenImprimirReporte(pIdTabulador, pNivelMercado) {
            var vPropierties = GetWindowProperties();
            vPropierties.width = 1100;
            openChildDialog("ReporteTabuladorSueldos.aspx?ID=" + pIdTabulador + "&pNivelMercado=" + pNivelMercado, "winImprimir", "Imprimir consulta", vPropierties);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConsultas" runat="server" OnAjaxRequest="ramConsultas_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConsultas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgComparativos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConsultas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdEmpleados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ScatterChartGraficaAnalisis" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgEmpleadosTabuladorSueldos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdComparacionInventarioPersonal" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionarEmpleado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdEmpleados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionarTodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdEmpleados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEmpleadosTabuladorSueldos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEmpleadosTabuladorSueldos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdComparacionInventarioPersonal" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgdTabuladorMaestro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsTabuladorSueldos" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorSueldos">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Definición de criterios"></telerik:RadTab>
            <telerik:RadTab Text="Tabulador de sueldos"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 50px);">
<%--        <telerik:RadSplitter ID="rsConsultas" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpConsultas" runat="server" Width="100%" Height="100%">--%>
        <telerik:RadMultiPage ID="rmpTabuladorSueldos" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server" Height="100%">
                <div style="height: 10px;"></div>
                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Versión:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtClaveTabulador" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Descipción:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNbTabulador" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Notas:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtDescripción" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Fecha:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtFecha" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Vigencia:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtVigencia" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de puestos:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtPuestos" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvCriteriosTabuladorSueldos" runat="server" Height="100%">
                <telerik:RadSplitter ID="rsConsultas" runat="server" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="rpConsultas" runat="server" Width="100%" Height="100%">
                        <div style="height: 10px; clear: both;"></div>
                        <div class="ctrlBasico">
                            <label id="Label4" name="lbRangoNivel" runat="server">Rango de nivel:</label>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadNumericTextBox runat="server" ID="rntComienzaNivel" NumberFormat-DecimalDigits="0" Name="rnComienza" Width="140px" MinValue="1" ShowSpinButtons="true" Value="1" OnTextChanged="rntComienzaNivel_TextChanged" AutoPostBack="true"></telerik:RadNumericTextBox>
                            <telerik:RadNumericTextBox runat="server" ID="rntTerminaSueldo" NumberFormat-DecimalDigits="0" Name="rnTermina" Width="140px" MinValue="1" ShowSpinButtons="true" Value="100" OnTextChanged="rntComienzaNivel_TextChanged" AutoPostBack="true"></telerik:RadNumericTextBox>
                        </div>
                        <div class="ctrlBasico">
                            <label id="Label5"
                                name="lbCuartil"
                                runat="server">
                                Nivel del mercado:</label>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadComboBox Filter="Contains" runat="server" ID="rcbMercadoTabuladorSueldos" Width="190" MarkFirstMatch="true" AutoPostBack="true" EmptyMessage="Seleccione..." OnSelectedIndexChanged="rcbMercadoTabuladorSueldos_SelectedIndexChanged"
                                DropDownWidth="190">
                            </telerik:RadComboBox>
                        </div>
                        <div style="clear: both;"></div>
                        <div style="height: calc(100% - 110px); overflow: auto;">
                            <telerik:RadGrid ID="rgEmpleadosTabuladorSueldos"
                                runat="server"
                                AllowSorting="true"
                                Height="100%"
                                AutoGenerateColumns="false"
                                HeaderStyle-Font-Bold="true"
                                OnItemCommand="rgEmpleadosTabuladorSueldos_ItemCommand"
                                OnNeedDataSource="rgEmpleadosTabuladorSueldos_NeedDataSource"
                                OnItemDataBound="rgEmpleadosTabuladorSueldos_ItemDataBound">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_TABULADOR_EMPLEADO">
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                            <ItemStyle Width="35" />
                                            <HeaderStyle Width="35" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="height: 10px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnEmpleadoCriterio" runat="server" name="btnSeleccionarEmpleado" OnClientClicked="OpenTabuladorEmpleadoSueldo" AutoPostBack="false" Text="Seleccionar mediante filtros"></telerik:RadButton>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyuda" runat="server" Width="20px" Height="100%">
                        <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas" ClickToOpen="true">
                            <telerik:RadSlidingPane ID="rsbAyuda" runat="server" CollapseMode="Forward" EnableResize="false" Width="325px" Title="Ayuda" Height="100%">
                                <div id="divTabuladorMaestro" runat="server">
                                    <p style="text-align: justify; padding: 10px 10px 10px 10px;">
                                        El reporte muestra por defecto a todos los empleados y el nivel del mercado configurados en el tabulador anteriormente. 
                                        <br />
                                        La pestaña "Definición de criterios" permite definir tu búsqueda para el reporte solicitado.
                                        <br />
                                        En caso de elegir "Seleccionar mediante filtros", se abrirá una ventana en la cual puedes seleccionar los empleados mediante los filtros deseados
                                        <br />
                                        Nota: En algunos casos las opciones pueden ser mutualmente exclusivas.
                                    </p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvTabuladorSueldos" runat="server" Height="100%">
                <div style="height: 10px;"></div>
                <div style="height: calc(100% - 60px);">
                    <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="RadPane1" runat="server" Width="100%" Height="100%">
                            <telerik:RadGrid ID="rgdComparacionInventarioPersonal"
                                runat="server" Height="100%"
                                AutoGenerateColumns="true"
                                OnItemCreated="rgdComparacionInventarioPersonal_ItemCreated"
                                OnNeedDataSource="rgdComparacionInventarioPersonal_NeedDataSource"
                                HeaderStyle-Font-Bold="true"
                                OnColumnCreated="rgdComparacionInventarioPersonal_ColumnCreated"
                                AllowMultiRowSelection="true"
                                OnItemDataBound="rgdComparacionInventarioPersonal_ItemDataBound"
                                AllowPaging="false">
                                <ClientSettings EnablePostBackOnRowClick="false">
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView ClientDataKeyNames="ID_TABULADOR_EMPLEADO, NO_NIVEL" DataKeyNames="ID_TABULADOR_EMPLEADO, NO_NIVEL" EnableColumnsViewState="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup HeaderText="Tabulador medio" Name="TABMEDIO" HeaderStyle-HorizontalAlign="Center">
                                        </telerik:GridColumnGroup>
                                    </ColumnGroups>
                                    <Columns>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPane>
                        <telerik:RadPane ID="RadPane2" runat="server" Width="20px" Height="100%">
                            <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas" ClickToOpen="true">
                                <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" CollapseMode="Forward" EnableResize="false" Width="325px" Title="Ayuda" Height="100%">
                                    <div id="div1" runat="server">
                                        <p style="text-align: justify; padding: 10px 10px 10px 10px;">
                                            El reporte muestra por defecto a todos los empleados y el nivel del mercado configurados en el tabulador anteriormente. 
                                            <br />
                                            La pestaña "Definición de criterios" permite definir tu búsqueda para el reporte solicitado.<br />
                                            En caso de elegir "Seleccionar mediante filtros", se abrirá una ventana en la cual puedes seleccionar los empleados mediante los filtros deseados
                                            <br />
                                            Nota: En algunos casos las opciones pueden ser mutualmente exclusivas.
                                        </p>
                                    </div>
                                </telerik:RadSlidingPane>
                                <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="450px" Title="Código de color" Height="100%">
                                    <div style="padding: 10px; text-align: justify;">
                                        <telerik:RadGrid ID="grdCodigoColores"
                                            runat="server"
                                            Height="215"
                                            Width="400"
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
                <div style="height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnImprimir" runat="server" AutoPostBack="true" Text="Imprimir" OnClick="btnImprimir_Click"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
<%--            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="20px" Height="100%">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="rsbAyuda" runat="server" CollapseMode="Forward" EnableResize="false" Width="325px" Title="Ayuda" Height="100%">
                        <div id="divTabuladorMaestro" runat="server">
                            <p style="text-align: justify; padding: 10px 10px 10px 10px;">
                                El reporte muestra por defecto a todos los empleados y el nivel del mercado configurados en el tabulador anteriormente. 
                            <br />
                                <br />
                                La pestaña "Definición de criterios" permite refinar tu búsqueda para el reporte solicitado.<br />
                                <br />
                                En caso de elegir "Seleccionar mediante filtros", se abrirá una ventana en la cual puedes seleccionar los empleados mediante los filtros deseados
                         <br />
                                <br />
                                Nota: En algunos casos las opciones pueden ser mutualmente exclusivas.
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                    <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="450px" Title="Semáforo" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                            <telerik:RadGrid ID="grdCodigoColores"
                                runat="server"
                                Height="215"
                                Width="400"
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
        </telerik:RadSplitter>--%>
    </div>
</asp:Content>
