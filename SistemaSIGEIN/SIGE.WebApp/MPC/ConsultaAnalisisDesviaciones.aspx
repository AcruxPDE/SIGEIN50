<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ConsultaAnalisisDesviaciones.aspx.cs" Inherits="SIGE.WebApp.MPC.ConsultaAnalisisDesviaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style type="text/css">
        .RadTabStrip {
            white-space: normal;
        }

        .RadGrid1Class {
            background-color: #F2F2F2;
        }

        .RadGrid2Class {
            background-color: #A9E2F3;
        }
    </style>
    <script id="modal" type="text/javascript">

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vDatosSeleccionados = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "TABULADOR_EMPLEADO":
                        for (var i = 0; i < pDato.length; ++i) {
                            arr.push(pDato[i].idEmpleado);
                        }
                        var datos = JSON.stringify({ clTipo: vDatosSeleccionados.clTipoCatalogo, arrEmpleados: arr });
                        InsertEmpleado(datos);
                        break;
                }
            }
        }

        function OnClientClicked(sender, args) {
            var radtabstrip = $find('<%=rtsTabuladorDesviaciones.ClientID %>');
            var count = radtabstrip.get_tabs().get_count();
            var currentindex = radtabstrip.get_selectedIndex();
            var nextindex = currentindex + 1;
            if (nextindex < count) {
                radtabstrip.set_selectedIndex(nextindex);
            }
        }

        function InsertEmpleado(pDato) {
            var ajaxManager = $find('<%= ramConsultas.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function OpenSelectionTabuladorEmployee() {
            var myUrl = '<%= ResolveClientUrl("SeleccionTabuladorEmpleado.aspx") %>';
            openChildDialog(myUrl + "?&IdTabulador=" + <%=vIdTabulador%> + "&vClTipoSeleccion=CONSULTAS", "winSeleccion", "Selección de empleados");
        }

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

        function OpenImprimirReporte() {
            var pIdTabulador = '<%= vIdTabulador %>';
                var pNivelMercado = '<%= vCuartilComparativo %>';
                openChildDialog("ReporteAnalisisDesviaciones.aspx?ID=" + pIdTabulador + "&pNivelMercado=" + pNivelMercado, "winImprimir", "Imprimir consulta");
        }

        function OpenImprimirGrafica() {
            var selectedItem = $find("<%=rgAnalisisDesviaciones.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vIdTabuladoNivel = selectedItem.getDataKeyValue("ID_TABULADOR_NIVEL");

                var pIdTabulador = '<%= vIdTabulador %>';
                var pNivelMercado = '<%= vCuartilComparativo %>';
                openChildDialog("GraficaAnalisisDesviaciones.aspx?ID=" + pIdTabulador + "&pNivelMercado=" + pNivelMercado + "&pIdTabuladoNivel=" + vIdTabuladoNivel, "winImprimir", "Imprimir consulta");
            }
        }
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConsultas" runat="server" OnAjaxRequest="ramConsultas_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rmpDesviaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rmpDesviaciones" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsTabuladorDesviaciones" runat="server" SelectedIndex="0" MultiPageID="rmpDesviaciones">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Definición de criterios"></telerik:RadTab>
            <telerik:RadTab Text="Análisis de desviaciones"></telerik:RadTab>
            <telerik:RadTab Text="Gráfica de desviaciones" ForeColor="White"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadSplitter ID="rsConsultas" runat="server" Width="100%" Height="95%" BorderSize="0">
        <telerik:RadPane ID="rpConsultas" runat="server" Width="100%" Height="100%">
            <div style="height: calc(100% - 50px); overflow: auto;">
                <telerik:RadMultiPage ID="rmpDesviaciones" runat="server" SelectedIndex="0" Height="100%">
                    <telerik:RadPageView ID="rpvContexto" runat="server">
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
                                        <label>Descripción:</label></td>
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
                    <telerik:RadPageView ID="rpvCriterios" runat="server">
                        <div style="height: 10px;"></div>
                        <div class="ctrlBasico">
                            <label id="Label3" name="lbRangoNivel" runat="server">Rango de nivel:</label>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadNumericTextBox runat="server" ID="txtComienza" NumberFormat-DecimalDigits="0" Name="rnComienza" Width="140px" MinValue="1" ShowSpinButtons="true" Value="1" AutoPostBack="true" OnTextChanged="txtTermina_TextChanged" ></telerik:RadNumericTextBox>
                            <telerik:RadNumericTextBox runat="server" ID="txtTermina" NumberFormat-DecimalDigits="0" Name="rnTermina" Width="140px" MinValue="1" ShowSpinButtons="true" Value="100" AutoPostBack="true" OnTextChanged="txtTermina_TextChanged" ></telerik:RadNumericTextBox>
                        </div>
                        <div class="ctrlBasico">
                            <label id="Label2"
                                name="lbCuartil"
                                runat="server">
                                Nivel del mercado:</label>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadComboBox Filter="Contains" runat="server" ID="rcbNivelMercado" Width="190" MarkFirstMatch="true" AutoPostBack="true" EmptyMessage="Seleccione..."
                                DropDownWidth="190" OnSelectedIndexChanged="rcbNivelMercado_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </div>
                        <div style="height: 10px; clear: both;"></div>
                        <div style="height: calc(100% - 120px);">
                            <telerik:RadGrid ID="rgEmpleadosDesviasion"
                                runat="server"
                                AllowSorting="true"
                                Height="100%"
                                AutoGenerateColumns="false"
                                HeaderStyle-Font-Bold="true"
                                OnItemCommand="rgEmpleadosDesviasion_ItemCommand"
                                OnNeedDataSource="rgEmpleadosDesviasion_NeedDataSource"
                                OnItemDataBound="rgEmpleadosDesviasion_ItemDataBound">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
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
                            <div style="height: 10px; clear: both;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnSeleccionEnpleadosDes" runat="server" name="btnSeleccionarEmpleado" OnClientClicked="OpenSelectionTabuladorEmployee" AutoPostBack="false" Text="Seleccionar mediante filtros"></telerik:RadButton>
                            </div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvDesviaciones" runat="server" Height="100%">
                        <div style="height: 10px;"></div>
                        <div style="height: calc(100% - 65px); overflow: auto;">
                            <telerik:RadGrid ID="rgAnalisisDesviaciones"
                                runat="server"
                                AllowSorting="true"
                                Height="100%"
                                AutoGenerateColumns="false"
                                HeaderStyle-Font-Bold="true"
                                OnSelectedIndexChanged="rgAnalisisDesviaciones_SelectedIndexChanged"
                                OnItemDataBound="rgAnalisisDesviaciones_ItemDataBound">
                                <ClientSettings EnablePostBackOnRowClick="true">
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <HeaderStyle />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="false" AllowPaging="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_TABULADOR_NIVEL" ClientDataKeyNames="ID_TABULADOR_NIVEL">
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Nivel" DataField="NB_TABULADOR_NIVEL" UniqueName="NB_TABULADOR_NIVEL"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="../Assets/images/Icons/25/ArrowEqual.png" DataField="PR_VERDE" UniqueName="PR_VERDE">
                                            <HeaderTemplate>
                                                <span id="Span1" runat="server" style="width: 40px; border: 1px solid gray; background-color: green; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                            </HeaderTemplate>
                                            <ItemTemplate><%# string.Format("{0:N2}", Eval("PR_VERDE"))%>% </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="../Assets/images/Icons/25/ArrowUp.png" DataField="PR_AMARILLO_POS" UniqueName="PR_AMARILLO_POS">
                                            <HeaderTemplate>
                                                <span id="Span2" runat="server" style="width: 40px; height: 50px; border: 1px solid gray; background-color: yellow; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                            </HeaderTemplate>
                                            <ItemTemplate><%#  string.Format("{0:N2}", Eval("PR_AMARILLO_POS"))%>%</ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="../Assets/images/Icons/25/ArrowDown.png" DataField="PR_AMARILLO_NEG" UniqueName="PR_AMARILLO_NEG">
                                            <HeaderTemplate>
                                                <span id="Span3" runat="server" style="width: 40px; border: 1px solid gray; background-color: #ffd700; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                            </HeaderTemplate>
                                            <ItemTemplate><%#  string.Format("{0:N2}", Eval("PR_AMARILLO_NEG"))%>%</ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="../Assets/images/Icons/25/ArrowUp.png" DataField="PR_ROJO_POS" UniqueName="PR_ROJO_POS">
                                            <HeaderTemplate>
                                                <span id="Span4" runat="server" style="width: 40px; border: 1px solid gray; background-color: #ff4500; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                            </HeaderTemplate>
                                            <ItemTemplate><%#  string.Format("{0:N2}", Eval("PR_ROJO_POS"))%>%</ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="../Assets/images/Icons/25/ArrowDown.png" DataField="PR_ROJO_NEG" UniqueName="PR_ROJO_NEG">
                                            <HeaderTemplate>
                                                <span id="Span5" runat="server" style="width: 40px; border: 1px solid gray; background-color: red; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                            </HeaderTemplate>
                                            <ItemTemplate><%#  string.Format("{0:N2}", Eval("PR_ROJO_NEG"))%>%</ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="height: 10px;"></div>

                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnGrafica" runat="server" name="btnGrafica" OnClientClicked="OnClientClicked" Enabled="false" AutoPostBack="false" Text="Gráfica" Width="100"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnImprimir" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenImprimirReporte"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvGraficaDesviaciones" runat="server" Height="100%">
                        <div style="height: calc(100% - 65px);">
                            <telerik:RadHtmlChart runat="server" ID="PieChartGraficaDesviaciones" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                <ChartTitle Text="Gráfica de Desviaciones">
                                    <Appearance Align="Center" Position="Top">
                                    </Appearance>
                                </ChartTitle>
                                <Legend>
                                    <Appearance Position="Right" Visible="true">
                                    </Appearance>
                                </Legend>
                                <PlotArea>
                                    <Series>
                                        <telerik:PieSeries StartAngle="90">
                                            <LabelsAppearance Position="OutsideEnd" DataFormatString="{0} %">
                                            </LabelsAppearance>
                                            <TooltipsAppearance Color="White" DataFormatString="{0} %"></TooltipsAppearance>
                                        </telerik:PieSeries>
                                    </Series>
                                </PlotArea>
                            </telerik:RadHtmlChart>
                        </div>
                        <div style="height: 10px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="RadButton1" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenImprimirGrafica"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyuda" runat="server" Width="20px" Height="90%">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas" ClickToOpen="true">
                <telerik:RadSlidingPane ID="rsbAyuda" runat="server" CollapseMode="Forward" EnableResize="false" Width="325px" Title="Ayuda" Height="100%">
                    <div id="divTabuladorMaestro" runat="server" style="padding: 15px; text-align: justify;">
                        <p>
                                Utiliza la pestaña "Definición de criterios" para refinar tu búsqueda para el reporte solicitado.
                            <br />	
                            <br />																
                                Con excepción de los parámetros de períodos que son mandatorios,																	
                                no seleccionando mediante filtros indicas que dichos criterios																	
                                son irrelevantes y el sistema no lo tomará en cuenta para filtrar los datos.																	
                                En caso de ingresar criterios de búsqueda, éstos serán utilizados para 																	
                                acotar el reporte.	
                                <br />
                                <br />																
                                Nota: en algunos casos las opciones pueden ser mutuamente exclusivas.																		
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
                            <GroupingSettings CaseSensitive="false" />
                            <PagerStyle AlwaysVisible="true" />
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
</asp:Content>
