<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ConsultasGenerales.aspx.cs" Inherits="SIGE.WebApp.FYD.ConsultasGenerales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        function OpenEmpleadosSelectionWindow() {
            OpenSelectionWindow("/Comunes/SeleccionEmpleado.aspx?m=FORMACION", "winSeleccion", "Selección de evaluados");
        }

        function OpenPeriodoSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPeriodo.aspx?", "winSeleccion", "Selección de período")
        }

        function OpenPuestoSelectionWindow() {
            OpenSelectionWindow("/Comunes/SeleccionPuesto.aspx?m=FORMACION", "winSeleccion", "Selección de puestos");
        }

        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("/Comunes/SeleccionArea.aspx?m=FORMACION", "winSeleccion", "Selección de áreas/departamento");
        }

        function OpenReporteGlobalWindow() {

            var vIdReporteGlobal = '<%= vIdReporteGlobal %>';
            var vFgGrid = $find('<%# chkFormato.ClientID %>').get_checked();
            var vFgFoto = $find('<%# chkIncluir.ClientID %>').get_checked();

            OpenSelectionWindow("../FYD/ReporteGlobal.aspx?IdReporteGlobal=" + vIdReporteGlobal + "&Fgfoto=" + vFgFoto + "&FgGrid=" + vFgGrid , "rwReporte", "Consulta General - Evaluación de competencias");
        }

        function OpenReporteComparativoWindow(pIdPeriodo) {

            var vIdReporteComparativo = '<%= vIdReporteComparativo %>';
            var vFgFoto = $find('<%# chkFoto.ClientID %>').get_checked();

            OpenSelectionWindow("../FYD/ReporteComparativo.aspx?IdReporteComparativo=" + vIdReporteComparativo + "&Fgfoto=" + vFgFoto, "rwReporte", "Consulta General comparativa - Evaluación de competencias");
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
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

        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {
                    case "EMPLEADO":
                        InsertEvaluado(EncapsularSeleccion("EMPLEADO", pDato));
                        break;
                    case "PUESTO":
                        InsertEvaluado(EncapsularSeleccion("PUESTO", pDato));
                        break;
                    case "DEPARTAMENTO":
                        InsertEvaluado(EncapsularSeleccion("DEPARTAMENTO", pDato));
                        break;
                    case "CAMPOADICIONAL":
                        //InsertCamposAdicionales(pDato);
                        break;
                    case "PERIODO":
                        InsertEvaluado(EncapsularSeleccion("PERIODO", pDato));
                }
            }
        }

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertEvaluado(pDato) {
            var ajaxManager = $find('<%= ramConsultasGenerales.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function OpenReporteGlobal() {
            OpenReporteGlobalWindow();
        }

        function OpenReporteComparativo() {
            OpenReporteComparativoWindow();
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpConsultasGenerales" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramConsultasGenerales" runat="server" OnAjaxRequest="ramConsultasGenerales_AjaxRequest" DefaultLoadingPanelID="ralpConsultasGenerales">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPersonas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPersonas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramConsultasGenerales">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPersonas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramConsultasGenerales">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPeriodosComparar" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="clear: both; height: 10px;"></div>
    <%--<div class="ctrlBasico">
        <label>Periodo:</label>
    </div>

    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtClavePeriodo" Width="50px" Enabled="false"></telerik:RadTextBox>
    </div>

    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtPeriodo" Width="300px" Enabled="false"></telerik:RadTextBox>
    </div>

    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtTiposEvaluacion" Width="500px" Enabled="false"></telerik:RadTextBox>
    </div>--%>

<%--    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Período:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtClavePeriodo" runat="server" style="min-width: 100px;"></div>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtTiposEvaluacion" runat="server" width="170" maxlength="1000" enabled="false"></div>
                </td>
            </tr>
        </table>
    </div>

    <div style="clear: both; height: 10px;"></div>--%>

    <telerik:RadTabStrip runat="server" ID="rtsConsultasGenerales" SelectedIndex="0" Width="100%" MultiPageID="rmpConsultasGenerales">
        <Tabs>
            <telerik:RadTab Text="Contexto"></telerik:RadTab>
            <telerik:RadTab Text="Reporte global"></telerik:RadTab>
            <telerik:RadTab Text="Selección de períodos a comparar"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 60px); width: 100%;">
        <telerik:RadMultiPage runat="server" ID="rmpConsultasGenerales" SelectedIndex="0" Width="100%" Height="100%">
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


            <telerik:RadPageView runat="server" ID="rpvReporteGeneral">
                <div style="height: calc(100% - 50px);">
                    <telerik:RadGrid ID="grdPersonas" runat="server" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true" AllowMultiRowSelection="true"
                        OnNeedDataSource="grdPersonas_NeedDataSource" OnItemCommand="grdPersonas_ItemCommand" OnItemCreated="grdPersonas_ItemCreated" OnItemDataBound="grdPersonas_ItemDataBound">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EMPLEADO, CL_PUESTO, CL_EMPLEADO, CL_DEPARTAMENTO, NB_EMPLEADO_COMPLETO" ClientDataKeyNames="ID_EMPLEADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" FilterControlWidth="90%" HeaderText="Nombre" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="80%" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="80%" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO">
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

                <%--                <div style="height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorPersona" runat="server" Text="Filtrar desde inventario" AutoPostBack="false" OnClientClicked="OpenEmpleadosSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorPuesto" runat="server" Text="Filtrar desde descriptivos de puesto" AutoPostBack="false" OnClientClicked="OpenPuestoSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorArea" runat="server" Text="Filtrar desde áreas" AutoPostBack="false" OnClientClicked="OpenAreaSelectionWindow"></telerik:RadButton>
                </div>--%>
                <div style="clear: both; height: 5px;"></div>
                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadCheckBox runat="server" ID="chkIncluir" Text="Incluir fotografía del evaluado" AutoPostBack="false"></telerik:RadCheckBox>
                    </div>
                    <div class="ctrlBasico" style="padding-left: 5px;">
                        <telerik:RadCheckBox runat="server" ID="chkFormato" Text="Formato de GRID" AutoPostBack="false" Checked="true"></telerik:RadCheckBox>
                    </div>

                    <div class="ctrlBasico" style="padding-left: 5px;">
                        <telerik:RadButton runat="server" ID="btnReporte" Text="Reporte" AutoPostBack="false" OnClientClicked="OpenReporteGlobal"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView runat="server" ID="rpvReporteComparativa">

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
                             <%--   <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>--%>
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
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" ID="btnAgregarPeriodosComparacion" Text="Seleccionar períodos" AutoPostBack="false" OnClientClicked="OpenPeriodoSelectionWindow"></telerik:RadButton>
                </div>


                <%--                <div style="width: 400px; height: 600px; float: left;">
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


                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadCheckBox runat="server" ID="chkFoto" Text="Incluir fotografía del evaluado" AutoPostBack="false"></telerik:RadCheckBox>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnReporteComparativa" Text="Consulta" AutoPostBack="false" OnClientClicked="OpenReporteComparativo"></telerik:RadButton>
                    </div>
                </div>

            </telerik:RadPageView>

        </telerik:RadMultiPage>
    </div>

    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEmpleado" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="rwReporteComparativo" runat="server" Behaviors="Close" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="winDescriptivo" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
