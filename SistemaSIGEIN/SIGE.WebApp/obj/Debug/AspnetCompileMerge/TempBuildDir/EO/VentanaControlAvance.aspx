<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaControlAvance.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaControlAvance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        function OpenCuestionarioConfidencial() {

            var vMasterTable = $find('<%= grdEvaluadorCuestionarios.ClientID %>').get_masterTableView();
            var vSeleccionados = vMasterTable.get_selectedItems();
            if (vSeleccionados.length > 0) {
                var vCuestionario = vSeleccionados[0];
                var vIdEvaluador = vCuestionario.getDataKeyValue("ID_EVALUADOR");
                var vIdPeriodo = '<%= vIdPeriodo %>';

                var vURL = "Cuestionarios/CuestionarioClimaLaboralConfidencial.aspx?ID_EVALUADOR=" + vIdEvaluador + "&ID_PERIODO=" + vIdPeriodo + "&FG_HABILITADO=False";
                var vTitulo = "Cuestionario";

                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 20,
                    height: browserWnd.innerHeight - 20
                };

                var wnd = openChildDialog(vURL, "WinCuestionario", vTitulo, windowProperties);
            }
            else {
                radalert("Selecciona un evaluado.", 400, 150);
            }

        }


        function ConfirmarEliminar(sender, args) {
            var vMasterTable = $find('<%= grdEvaluadorCuestionarios.ClientID %>').get_masterTableView();
            var vSeleccionados = vMasterTable.get_selectedItems();
            if (vSeleccionados.length > 0) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
            { if (shouldSubmit) { this.click(); } });

            radconfirm('¿Estás seguro de eliminar las respuestas?, este proceso no podra revertirse.', callBackFunction, 460, 180, null, "Eliminar respuestas");
            args.set_cancel(true);
            }
            else {
                radalert("Selecciona un evaluado.", 400, 150);
                args.set_cancel(true);
            }
        }


        function OpenCuestionario() {

            var vMasterTable = $find('<%= grdEvaluadorCuestionarios.ClientID %>').get_masterTableView();
            var vSeleccionados = vMasterTable.get_selectedItems();
            if (vSeleccionados.length > 0) {
                var vCuestionario = vSeleccionados[0];
                var vIdEvaluador = vCuestionario.getDataKeyValue("ID_EVALUADOR");
                var vIdPeriodo = '<%= vIdPeriodo %>';

                var vURL = "Cuestionarios/CuestionarioClimaLaboral.aspx?ID_EVALUADOR=" + vIdEvaluador + "&ID_PERIODO=" + vIdPeriodo + "&FG_HABILITADO=False";
                var vTitulo = "Cuestionario";

                    var currentWnd = GetRadWindow();
                    var browserWnd = window;
                    if (currentWnd)
                        browserWnd = currentWnd.BrowserWindow;

                    var windowProperties = {
                        width: browserWnd.innerWidth - 20,
                        height: browserWnd.innerHeight - 20
                    };

                    var wnd = openChildDialog(vURL, "WinCuestionario", vTitulo, windowProperties);
            }
            else {
                radalert("Selecciona un evaluado.", 400, 150);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadTabStrip ID="rtsControlAvance" runat="server" SelectedIndex="0" MultiPageID="rmpControlAvance">
        <Tabs>
            <telerik:RadTab Text="Contexto"></telerik:RadTab>
            <telerik:RadTab Text="Gráfica"></telerik:RadTab>
            <telerik:RadTab Text="Detalle"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 60px);">
       <div style="clear: both; height: 10px;"></div>
        <telerik:RadMultiPage ID="rmpControlAvance" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                <%-- <div class="ctrlBasico">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnPredefinido" runat="server" ToggleType="CheckBox" AutoPostBack="false" ReadOnly="true" Width="260" Text="Cuestionario predefinido de SIGEIN">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>
                              
                             
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnCopia" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="260" ReadOnly="true" Text="Cuestionario copia de otro periodo">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>
                            
                         
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnVacios" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="260" ReadOnly="true" Text="Cuestionario creado desde cero">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>
                                        </tr>
                                </table>
                            </div>--%>
                                <div class="ctrlBasico">
                                    <table class="ctrlTableForm" text-align: left;>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbPeriodo" name="lbTabulador" runat="server">Período:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtClPeriodo" runat="server"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbNbPeriodo" name="lbTabulador" runat="server">Descripción:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtDsPeriodo" runat="server"></div>
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label2" name="lbNotas" runat="server">Notas:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtNotas" runat="server"></div>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="LbEstado" name="lbTabulador" runat="server">Estado:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtEstatus" runat="server"></div>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label1" name="lbNotas" runat="server">Tipo de cuestionario:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtTipo" runat="server"></div>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="LbTipoCuestionario" name="lbNotas" runat="server">Origen de cuestionario:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="lbCuestionario" runat="server"></div>
                                            </td>
                                        </tr>
                                      <%--  <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbedad" name="LbFiltros" runat="server" visible="false">Edad:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtEdad" runat="server" visible="false"></div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbGenero" name="LbFiltros" runat="server" visible="false">Género:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtGenero" runat="server" visible="false"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbAntiguedad" name="LbFiltros" runat="server" visible="false">Antigüedad:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtAntiguedad" runat="server" visible="false"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbDepartamento" name="LbFiltros" runat="server" visible="false">Área:</label>
                                            </td>
                                            <td colspan="2" class="ctrlTableDataContext">
                                                <telerik:RadTextBox ID="rlDepartamento" Visible="false" ReadOnly="true" runat="server" Width="200" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbAdscripciones" name="LbFiltros" runat="server" visible="false">Campos adicionales:</label>
                                            </td>
                                            <td rowspan="3" class="ctrlTableDataContext" visible="false">
                                                <telerik:RadTextBox ID="rlAdicionales" Visible="false" ReadOnly="true" runat="server" Width="200" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
                                            </td>

                                        </tr>--%>
                                    </table>
                                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvGrafica" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <table style="width: 100%; height: 95%">
                    <tr>
                        <td style="text-align: right; width: 30%">
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Cuestionarios contestados" Width="200" Enabled="false"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtRespondidos" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            <div style="height: 10px;"></div>
                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="Cuestionarios por contestar" Width="200" Enabled="false"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtPorResponder" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            <div style="height: 10px;"></div>
                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Total de cuestionarios" Width="200" Enabled="false"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtTotalCuestionarios" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 70%;">
                            <telerik:RadHtmlChart runat="server" ID="hcCuestionarios" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                <ChartTitle Text="Cuestionarios">
                                    <Appearance Align="Center" Position="Top">
                                    </Appearance>
                                </ChartTitle>
                                <Legend>
                                    <Appearance Position="Bottom" Visible="true">
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
                        </td>
                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvDetalle" runat="server">
                <div style="height: calc(100% - 40px); padding-bottom: 10px;">
                    <telerik:RadGrid ID="grdEvaluadorCuestionarios" HeaderStyle-Font-Bold="true" runat="server" Height="100%" Width="100%" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdEvaluadorCuestionarios_NeedDataSource" OnItemDataBound="grdEvaluadorCuestionarios_ItemDataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EVALUADOR" ClientDataKeyNames="ID_EVALUADOR,FG_CONTESTADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <ColumnGroups>
                                <telerik:GridColumnGroup Name="Evaluadores" HeaderText="Evaluadores"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Larger" />
                            </ColumnGroups>
                            <Columns>
                                <telerik:GridBoundColumn ColumnGroupName="Evaluadores" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" FilterControlWidth="80" HeaderStyle-Width="120" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ColumnGroupName="Evaluadores" HeaderText="Nombre completo" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR" FilterControlWidth="170" HeaderStyle-Width="250" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ColumnGroupName="Evaluadores" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" FilterControlWidth="170" HeaderStyle-Width="250" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ColumnGroupName="Evaluadores" HeaderText="Contestado" FilterControlWidth="40" HeaderStyle-Width="90" DataField="NB_CONTESTADO" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                            <div class="ctrlBasico">
<<<<<<< HEAD
                                 <telerik:RadButton runat="server" ID="btnVerCuestionario" Text="Ver cuestionario" AutoPostBack="false" OnClientClicked="OpenCuestionario"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btnVerCuestionarioConfidencial" Visible="false" Text="Ver cuestionario" OnClientClicked="OpenCuestionarioConfidencial"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btnEliminarRespuestas" Text="Eliminar respuestas" AutoPostBack="true" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminarRespuestas_Click"></telerik:RadButton>
                               
=======
                                                                 <telerik:RadButton runat="server" ID="btnEliminarRespuestas" Text="Eliminar respuestas" AutoPostBack="true" OnClick="btnEliminarRespuestas_Click"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btnVerCuestionario" Text="Ver cuestionario" OnClientClicked="OpenCuestionario"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btnVerCuestionarioConfidencial" Visible="false" Text="Ver cuestionario" OnClientClicked="OpenCuestionarioConfidencial"></telerik:RadButton>
>>>>>>> DEV
                            </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
          <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
