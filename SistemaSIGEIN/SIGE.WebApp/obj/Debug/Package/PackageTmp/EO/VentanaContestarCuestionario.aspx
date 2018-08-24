<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaContestarCuestionario.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaContestarCuestionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

        var idEvaluador = "";
        var vContestado = "";

        function obtenerIdFila() {
            var grid = $find("<%=grdEvaluadorCuestionarios.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idEvaluador = SelectDataItem.getDataKeyValue("ID_EVALUADOR");
                vContestado = SelectDataItem.getDataKeyValue("FG_CONTESTADO");
            }
        }

        function OpenCuestionarioConfidencial() {
            OpenWindow(GetConfidencialWindowProperties());
        }

        function GetConfidencialWindowProperties() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Cuestionario";
            wnd.vURL = "Cuestionarios/CuestionarioClimaLaboralConfidencial.aspx?ID_PERIODO=" + <%=vIdPeriodo%> +"";
            wnd.vRadWindowId = "WinCuestionario";
            return wnd;
        }

        function OpenCuestionario(sender, args) {
            obtenerIdFila();
            if (idEvaluador != "") {
                if (vContestado == "False") {
                    OpenWindow(GetCuestionarioWindowProperties(idEvaluador));
                }
                else {
                    radalert("El cuestionario ya ha sido contestado.", 400, 150, "Error");
                    args.set_cancel(true);
                }
            }
            else
                radalert("Selecciona un evaluador.", 400, 150);
            args.set_cancel(true);
        }

        function GetCuestionarioWindowProperties(pidEvaluador) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Cuestionario";
            wnd.vURL = "Cuestionarios/CuestionarioClimaLaboral.aspx?ID_EVALUADOR=" + pidEvaluador + "&ID_PERIODO=" + <%=vIdPeriodo%> +"";
            wnd.vRadWindowId = "WinCuestionario";
            return wnd;
        }

        function GetWindowProperties() {

            var currentWnd = GetRadWindow();
            var browserWnd = window;

            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            return windowProperties;
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function useDataFromChild(pDato) {
            $find('<%= ramCuestinariosClima.ClientID %>').ajaxRequest();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpCuestionariosClima" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramCuestinariosClima" runat="server" OnAjaxRequest="ramCuestinariosClima_AjaxRequest" DefaultLoadingPanelID="ralpCuestionariosClima">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramCuestinariosClima">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdEvaluadorCuestionarios"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEvaluadorCuestionarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdEvaluadorCuestionarios"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="clear: both; height: 10px;"></div>
    <telerik:RadTabStrip ID="rtsControlAvance" runat="server" SelectedIndex="0" MultiPageID="rmpControlAvance">
        <Tabs>
            <telerik:RadTab Text="Contexto"></telerik:RadTab>
            <telerik:RadTab Text="Cuestionarios"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 120px); padding-bottom: 10px;">
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
                <div style="clear: both; height: 10px;"></div>
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
                                                <label id="txtNotas" runat="server"></label>
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
                                     <%--   <tr>
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
            <telerik:RadPageView ID="rpvCuestionarios" runat="server">
                <telerik:RadGrid ID="grdEvaluadorCuestionarios" runat="server" Height="100%" Width="100%" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" AllowSorting="true" OnNeedDataSource="grdEvaluadorCuestionarios_NeedDataSource" OnItemDataBound="grdEvaluadorCuestionarios_ItemDataBound">
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
                            <telerik:GridBoundColumn ColumnGroupName="Evaluadores" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" FilterControlWidth="100" HeaderStyle-Width="150" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ColumnGroupName="Evaluadores" HeaderText="Nombre completo" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR" FilterControlWidth="170" HeaderStyle-Width="250" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ColumnGroupName="Evaluadores" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" FilterControlWidth="170" HeaderStyle-Width="250" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ColumnGroupName="Evaluadores" HeaderText="Contestado" FilterControlWidth="70" HeaderStyle-Width="120" DataField="NB_CONTESTADO" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnContestarCuestionarios" runat="server" Text="Cuestionario" OnClientClicking="OpenCuestionario"></telerik:RadButton>
                    <telerik:RadButton ID="btnContestarConfidencial" Visible="false" runat="server" Text="Cuestionario" OnClientClicked="OpenCuestionarioConfidencial"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
