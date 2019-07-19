<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ControlAvance.aspx.cs" Inherits="SIGE.WebApp.FYD.ControlAvance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script type="text/javascript">
        function OpenEnvioCuestionariosWindow() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 750,
                height: browserWnd.innerHeight - 40
            };

            OpenSelectionWindow("VentanaEnvioSolicitudes.aspx?IdPeriodo=<%= vIdPeriodo %>&FgRevisaSeleccion=true", "winAgregarCuestionario", "Envío de Cuestionarios", windowProperties);
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function TabSelected() {
            var vTabSelected = $find("<%= RadTabStrip1.ClientID %>").get_selectedIndex();
            console.info(vTabSelected);

            var btnVerCuestionario = document.getElementById('<%= btnVistaPrevia.ClientID %>');

            switch (vTabSelected) {

                case 0:
                    btnVerCuestionario.style.display = "none";
                    break;
                case 1:
                    btnVerCuestionario.style.display = "block";
                    break;
            }
        }

        function OpenVerCuestionario() {
            var masterTable = $find("<%= GridPorEvaluador.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {

                var vEvaluado = selectedItems[0];
                var IdEvaluador = vEvaluado.getDataKeyValue("ID_EVALUADOR");
                var IdEvaluadoEvaluador = vEvaluado.getDataKeyValue("ID_EVALUADO_EVALUADOR");

                OpenSelectionWindow("VentanaVerCuestionario.aspx?ID_EVALUADOR=" + IdEvaluador + "&ID_EVALUADO_EVALUADOR=" + IdEvaluadoEvaluador, "winContestarCuestionarios", "Vista cuestionario")
            }
            else {
                radalert("Selecciona un evaluado.", 400, 150, "Aviso");
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpControlAvance" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramControlAvance" runat="server" DefaultLoadingPanelID="ralpControlAvance">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GridPorEvaluado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridPorEvaluado" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GridPorEvaluador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridPorEvaluador" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
        <telerik:RadTabStrip ID="rtsControlAvance" runat="server" SelectedIndex="0" MultiPageID="rmpControlAvance">
        <Tabs>
             <telerik:RadTab Text="Contexto" Value="0"></telerik:RadTab>
            <telerik:RadTab Text="Control avance" Value="1"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
     <div style="height: calc(100% - 50px);">
     <telerik:RadMultiPage ID="rmpControlAvance" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">  

    <div style="clear: both; height: 10px;"></div>

    <%--<div class="ctrlBasico">
        <telerik:RadLabel runat="server" Text="Periodo:"></telerik:RadLabel>
        <telerik:RadTextBox runat="server" ID="txtNoPeriodo" Width="80px" ReadOnly="true"></telerik:RadTextBox>
        <telerik:RadTextBox runat="server" ID="txtNbPeriodo" Width="300px" ReadOnly="true"></telerik:RadTextBox>
    </div>--%>

<%--     <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNoPeriodo" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNbPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
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
                                                <label id="Label1" name="lbTabulador" runat="server">Estatus:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtEstatus" runat="server"></div>
                                            </td>
                                        </tr>
                                                      <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label2" name="lbTabulador" runat="server">Tipo de evaluación:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtTipoEvaluacion" runat="server"></div>
                                            </td>
                                        </tr>
                                             <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label3" name="lbTabulador" runat="server">Notas:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtNotas" runat="server"></div>
                                            </td>
                                        </tr>
                                        </table>
                                    </div>
</telerik:RadPageView>
       <telerik:RadPageView ID="rpvControlAvance" runat="server">  
    <div style="clear: both;"></div>
    <div style="height: calc(100% - 20px);">
        <telerik:RadSplitter ID="RadSplitter1" runat="server" Orientation="Vertical" Height="100%" Width="100%">
            <telerik:RadPane ID="RadSlidingPane1" runat="server" Width="490">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right">
                            <telerik:RadLabel runat="server" Text="Faltantes" Width="150"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtPorEvaluar" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            <div style="height: 10px;"></div>
                            <telerik:RadLabel runat="server" Text="Completos" Width="150"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtEvaluadas" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            <div style="height: 10px;"></div>
                            <telerik:RadLabel runat="server" Text="Total de evaluados" Width="150"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtTotalEvaluados" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            <telerik:RadHtmlChart runat="server" ID="hcEvaluado" Transitions="true" Height="250px">
                                <ChartTitle Text="Evaluados"></ChartTitle>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <Legend>
                                    <Appearance BackgroundColor="Transparent" Position="Bottom"></Appearance>
                                </Legend>
                            </telerik:RadHtmlChart>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="border-bottom: 1px solid lightgray"></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Cuestionarios respondidos" Width="190px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtRespondidos" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            <div style="height: 10px;"></div>
                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="Cuestionarios por responder" Width="190px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtPorResponder" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            <div style="height: 10px;"></div>
                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Total de cuestionarios" Width="190px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox runat="server" ID="txtTotalCuestionarios" Width="70px" ReadOnly="true">
                                <HoveredStyle HorizontalAlign="Right" />
                                <ReadOnlyStyle HorizontalAlign="Right" />
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            <telerik:RadHtmlChart runat="server" ID="hcCuestionarios" Transitions="true" Height="250">
                                <ChartTitle Text="Cuestionarios"></ChartTitle>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <Legend>
                                    <Appearance BackgroundColor="Transparent" Position="Bottom">
                                    </Appearance>
                                </Legend>
                            </telerik:RadHtmlChart>
                        </td>

                    </tr>
                </table>

            </telerik:RadPane>

            <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Both"></telerik:RadSplitBar>

            <telerik:RadPane ID="RadSlidingPane2" runat="server">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="mpControlAvance" SelectedIndex="0" OnClientTabSelected="TabSelected">
                    <Tabs>
                        <telerik:RadTab TabIndex="0" Text="Por Evaluado"></telerik:RadTab>
                        <telerik:RadTab TabIndex="1" Text="Por Evaluador"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 100px);">
                    <telerik:RadMultiPage ID="mpControlAvance" runat="server" SelectedIndex="0" Height="100%" Width="100%">
                        <telerik:RadPageView ID="RadPageView1" runat="server">

                            <telerik:RadGrid runat="server" ID="GridPorEvaluado" EnableHeaderContextMenu="true" AllowSorting="true" OnNeedDataSource="GridPorEvaluado_NeedDataSource" Width="100%" Height="100%" OnItemDataBound="GridPorEvaluado_ItemDataBound">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AutoGenerateColumns="false" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Evaluado" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NB_EVALUADO" DataField="NB_EVALUADO" ItemStyle-Width="160px">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Puesto" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NB_PUESTO" DataField="NB_PUESTO" ItemStyle-Width="100px">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Área/Departamento" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NB_DEPARTAMENTO" DataField="NB_DEPARTAMENTO" ItemStyle-Width="100px">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Porcentaje" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="PR_TERMINO" DataField="PR_TERMINO" FilterControlWidth="20px" DataFormatString="{0}%">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" Width="20px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Terminado" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="CL_TERMINADO" DataField="CL_TERMINADO" ItemStyle-Width="20px" FilterControlWidth="20px">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">

                            <telerik:RadGrid runat="server" ID="GridPorEvaluador" AllowSorting="true" OnNeedDataSource="GridPorEvaluador_NeedDataSource" Width="100%" Height="100%" OnItemDataBound="GridPorEvaluador_ItemDataBound">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle AlwaysVisible="true" />
                                <MasterTableView AutoGenerateColumns="false" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" ClientDataKeyNames="ID_EVALUADOR, ID_EVALUADO_EVALUADOR" EnableHeaderContextFilterMenu="true">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Evaluador" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NB_EVALUADOR" DataField="NB_EVALUADOR" FilterControlWidth="70%">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Rol" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="CL_ROL_EVALUADOR" DataField="CL_ROL_EVALUADOR" FilterControlWidth="60" HeaderStyle-Width="150">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Evaluado" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NB_EVALUADO" DataField="NB_EVALUADO" FilterControlWidth="200" HeaderStyle-Width="280">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Terminado" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="CL_TERMINADO" DataField="CL_TERMINADO" FilterControlWidth="20" HeaderStyle-Width="80">
                                            <HeaderStyle Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
                <div style="clear:both; height:5px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" ID="btnEnvioCuestionarios" Text="Envío de cuestionarios" AutoPostBack="false" OnClientClicked="OpenEnvioCuestionariosWindow"></telerik:RadButton>
                </div>
                 <div class="ctrlBasico">
                    <telerik:RadButton runat="server" ID="btnVistaPrevia" Text="Ver cuestionario" AutoPostBack="false"  OnClientClicked="OpenVerCuestionario"></telerik:RadButton>
                </div>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>
           </telerik:RadPageView>
         </telerik:RadMultiPage>
         </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
