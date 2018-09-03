<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaCapturarResultados.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaCapturarResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function onCloseWindow(oWnd, args) {
            $find("<%=grdEvaluados.ClientID%>").get_masterTableView().rebind();

        }

        function useDataFromChild(pDato) {
            $find("<%=grdEvaluados.ClientID%>").get_masterTableView().rebind();
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

        function OpenCapturaResultado() {
            var selectedItem = $find("<%=grdEvaluados.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vClEstado = selectedItem.getDataKeyValue("CL_ESTADO_EMPLEADO");
                if (vClEstado == "ALTA") {
                    var pIdPeriodo = '<%=vIdPeriodo%>';
                    var pIdEvaluado = selectedItem.getDataKeyValue("ID_EVALUADO");
                    OpenSelectionWindow("VentanaManualCapturaResultados.aspx?idPeriodo=" + pIdPeriodo + "&idEvaluado=" + pIdEvaluado, "rwCaptura", "Captura de resultados")
                }
                else 
                    radalert("Evaluado dado de baja.", 400, 150);
            }
            else 
                radalert("Selecciona a un evaluado.", 400, 150);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadTabStrip ID="rtsCapturaResultados" runat="server" SelectedIndex="0" MultiPageID="rmpCapturaResultados">
        <Tabs>
            <telerik:RadTab Text="Contexto"></telerik:RadTab>
            <telerik:RadTab Text="Captura de resultados"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 100px); width: 100%;">
            <div style="clear: both; height: 10px;"></div>
        <telerik:RadMultiPage ID="rmpCapturaResultados" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
                <div class="divControlIzquierda" style="width: 60%; text-align: left;">
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
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvEnvio" runat="server">

                <div style="height: calc(100% - 20px); width: 100%;">
                    <telerik:RadGrid ID="grdEvaluados" runat="server" Height="100%"
                        AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" EnableHeaderContextMenu="true" AllowSorting="true"
                        AllowMultiRowSelection="false" OnNeedDataSource="grdEvaluados_NeedDataSource" OnItemDataBound="grdEvaluados_ItemDataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EVALUADO, CL_ESTADO_EMPLEADO" ClientDataKeyNames="ID_EVALUADO, CL_ESTADO_EMPLEADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="30" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="60" HeaderText="Estatus" DataField="ESTATUS" UniqueName="ESTATUS" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

                <div style="clear: both; height: 10px;"></div>

    
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnCapturar" runat="server" Text="Capturar resultados" AutoPostBack="false" OnClientClicking="OpenCapturaResultado"></telerik:RadButton>
                    </div>

            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>

    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>

</asp:Content>
