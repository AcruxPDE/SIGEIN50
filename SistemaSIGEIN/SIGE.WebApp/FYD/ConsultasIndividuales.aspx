<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ConsultasIndividuales.aspx.cs" Inherits="SIGE.WebApp.FYD.ConsultasIndividuales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        var vIdEvaluado = 0;
        var vIdEmpleado = 0;

        function ObtenerEvaluado() {

            var masterTable = $find("<%= rgEvaluados.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();

            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {

                    selectedItem = selectedItems[i];
                    vIdEvaluado = selectedItem.getDataKeyValue("ID_EVALUADO");
                    vIdEmpleado = selectedItem.getDataKeyValue("ID_EMPLEADO");
                }
            }
        }

        function abrirPlan() {
            ObtenerEvaluado();
            if ((vIdEmpleado != 0)) {
                var win = window.open("PlanVidaCarrera.aspx?idEmpleado=" + vIdEmpleado, '_self', true);
                win.focus();
            }
            else { radalert("No has seleccionado un empleado.", 400, 150, "Aviso"); }
        }

        function OpenReporteIndividualWindow() {

            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vIdReporteIndividual = '<%= vIdReporteIndividual %>';

            ObtenerEvaluado();

            if (vIdPeriodo != null & vIdEvaluado != 0) {
                OpenSelectionWindow("ReporteIndividual.aspx?IdPeriodo=" + vIdPeriodo + "&IdEvaluado=" + vIdEvaluado + "&IdReporteIndividual=" + vIdReporteIndividual + "&ClTipoReporte=INDIVIADUAL", "rwReporte", "Consulta Individual - Evaluación de competencias");
            } else {
                radalert("No has seleccionado un evaluado.", 400, 150, "Aviso");
            }
        }

        function GetWindowProperties() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;


            return {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            //var currentWnd = GetRadWindow();
            //var browserWnd = window;
            //if (currentWnd)
            //    browserWnd = currentWnd.BrowserWindow;

            var windowProperties = GetWindowProperties();

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenPeriodoSelectionWindow() {

            ObtenerEvaluado();

            if (vIdEmpleado != 0) {
                OpenSelectionWindow("../Comunes/SeleccionPeriodo.aspx?IdEmpleado=" + vIdEmpleado, "winSeleccion", "Selección de período")
            }
            else {
                radalert("No has seleccionado un empleado.", 400, 150, "Aviso");
            }
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var vJsonPeriodo = JSON.stringify({ clTipo: "PERIODO", oSeleccion: pDato });
                var ajaxManager = $find('<%= ramConsultasIndividuales.ClientID%>');
                ajaxManager.ajaxRequest(vJsonPeriodo);

            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <telerik:RadAjaxLoadingPanel ID="ralpConsultasIndividuales" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramConsultasIndividuales" runat="server" OnAjaxRequest="ramConsultasIndividuales_AjaxRequest" DefaultLoadingPanelID="ralpConsultasIndividuales">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="ramConsultasIndividuales">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPeriodosComparar" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

<%--    <div style="clear: both; height: 10px;"></div>--%>

   <%-- <div class="ctrlBasico">
        <label>Periodo:</label>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtNoPeriodo" AutoPostBack="false" Width="100px" Enabled="false"></telerik:RadTextBox>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtNbPeriodo" AutoPostBack="false" Width="300px" Enabled="false"></telerik:RadTextBox>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtTiposEvaluacion" Width="500px" Enabled="false"></telerik:RadTextBox>
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
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTiposEvaluacion" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>--%>


    <div style="clear: both; height: 10px;"></div>

    <telerik:RadTabStrip runat="server" ID="rtsIndividual" MultiPageID="rmpIndividual" SelectedIndex="0" Width="100%" >
        <Tabs>
            <telerik:RadTab runat="server" Text="Contexto"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Seleccionar evaluado"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Selección de períodos a comparar"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 60px); overflow: auto;">
        <telerik:RadMultiPage runat="server" ID="rmpIndividual" Height="100%" Width="100%" SelectedIndex="0">
             <telerik:RadPageView ID="rpvContexto" runat="server">  

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

            <telerik:RadPageView runat="server" ID="rpvEmpleados">
                <div style="height: calc(100% - 50px); overflow: auto;">
                <telerik:RadGrid runat="server" ID="rgEvaluados" OnNeedDataSource="rgEvaluados_NeedDataSource" OnItemCreated="rgEvaluados_ItemCreated" Width="100%" Height="100%">
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                     <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_EVALUADO,ID_EMPLEADO, CL_PUESTO, CL_EMPLEADO, NB_EMPLEADO_COMPLETO" ClientDataKeyNames="ID_EVALUADO,ID_EMPLEADO" AutoGenerateColumns="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="CL_EMPLEADO" DataField="CL_EMPLEADO" HeaderText="No. de Empleado" FilterControlWidth="50%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">                                
                                <ItemStyle Width="10%" />
                                <HeaderStyle Width="10%" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_EMPLEADO" DataField="NB_EMPLEADO_COMPLETO" HeaderText="Nombre" FilterControlWidth="50%"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                <ItemStyle Width="40%" />
                                <HeaderStyle Width="40%" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO"  HeaderText="Puesto" FilterControlWidth="50%"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                <ItemStyle Width="40%" />
                                <HeaderStyle Width="40%" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                    </div>
                  <div style="clear: both; height: 10px;"></div>

    <div class="divControlDerecha">
        <telerik:RadButton runat="server" ID="btnIndividual" Text="Consulta" AutoPostBack="false" OnClientClicked="OpenReporteIndividualWindow"></telerik:RadButton>
    </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="rpvPeriodos">

                <%--                <div style="clear: both; height: 10px;"></div>

                <div style="width: 400px; height: 600px; float: left;">
                    <label style="width: 100%;">Periodos disponibles: </label>
                    <telerik:RadListBox runat="server" ID="rlbPeriodosDisponibles" TransferMode="Move" AllowTransfer="true" TransferToID="rlbPeriodosComparar" DataValueField="ID_PERIODO" DataKeyField="ID_PERIODO" DataTextField="NB_PERIODO" Height="600px" Width="400px" OnTransferred="rlbPeriodosDisponibles_Transferred" AutoPostBackOnTransfer="true" SelectionMode="Multiple">
                        <ButtonSettings AreaWidth="35px" />
                    </telerik:RadListBox>
                </div>

                <div style="width: 400px; height: 600px; float: left;">
                    <label style="width: 100%;">Periodos a comparar:</label>
                    <telerik:RadListBox runat="server" ID="rlbPeriodosComparar" DataValueField="ID_PERIODO" DataKeyField="ID_PERIODO" DataTextField="NB_PERIODO" Height="600px" Width="400px" AutoPostBackOnTransfer="true" SelectionMode="Multiple">
                        <ButtonSettings AreaWidth="35px" />
                    </telerik:RadListBox>
                </div>--%>

                <div style="height: calc(100% - 50px);">
                    <telerik:RadGrid runat="server" ID="rgPeriodosComparar" Height="100%" AutoGenerateColumns="false" OnNeedDataSource="rgPeriodosComparar_NeedDataSource" OnItemCommand="rgPeriodosComparar_ItemCommand" OnItemDataBound="rgPeriodosComparar_ItemDataBound">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_PERIODO" ClientDataKeyNames="ID_PERIODO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" Display="false" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="80%" HeaderText="Período" DataField="NB_PERIODO" UniqueName="NB_PERIODO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="80%" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                    <ItemStyle Width="35" />
                                    <HeaderStyle Width="35" Font-Bold="true" />
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 5px;"></div>

    <div class="divControlDerecha">
        <telerik:RadButton runat="server" ID="btnConsulta" Text="Consulta" AutoPostBack="false" OnClientClicked="OpenReporteIndividualWindow"></telerik:RadButton>
    </div>
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" ID="btnAgregarPeriodosComparacion" Text="Seleccionar períodos" AutoPostBack="false" OnClientClicked="OpenPeriodoSelectionWindow"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>

    <%--<div class="ctrlBasico">
    <telerik:RadButton runat="server" ID="btnPLan" Text="Plan de vida y carrera" AutoPostBack="false" OnClientClicked="abrirPlan"></telerik:RadButton>
    </div>--%>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
