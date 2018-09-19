<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaPlanSucesion.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaPlanSucesion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .PotencialBajo {
            background-color: red;
            text-align: center;
            color: white;
        }

        .PotencialIntermedio {
            background-color: gold;
            text-align: center;
        }

        .PotencialAlto {
            background-color: green;
            text-align: center;
        }

        .PotencialPuestoBajo {
            background-color: red;
            text-align: center;
            color: red;
        }

        .PotencialPuestoAlto {
            background-color: green;
            text-align: center;
            color: green;
        }

        .PotencialNC {
            background-color: lightgray;
            text-align: center;
        }

        .PivotCompetencias td.rpgColumnHeaderZone {
            width: calc(70px * <%= vNoEmpleados %>) !important;
        

    </style>

    <script type="text/javascript">

        function OpenEmpleadoWindow() {

            var idEmpleado = '<%# vIdEmpleado %>';

            OpenSelectionWindow("../Administracion/Empleado.aspx?EmpleadoId=" + idEmpleado + "&pFgHabilitaBotones=False", "winEditarEmpleado", "Ver empleado");
        }

        function OpenPuestoWindow() {

            var idPuesto = '<%# vIdPuesto %>';

            OpenSelectionWindow("../Administracion/VentanaVistaDescriptivo.aspx?PuestoId=" + idPuesto, "winEditarPuesto", "Vista previa descripción del puesto");
        }

        function OpenEmpleadoSucesorWindow(idEmpleado) {

            OpenSelectionWindow("../Administracion/Empleado.aspx?EmpleadoId=" + idEmpleado + "&pFgHabilitaBotones=False", "winEditarEmpleado", "Ver empleado");
        }

        function OpenPuestoSucesorWindow(idPuesto) {

            OpenSelectionWindow("../Administracion/VentanaVistaDescriptivo.aspx?PuestoId=" + idPuesto, "winEditarPuesto", "Vista previa descripción del puesto");
        }

        function lstPuestosDoubleClickItem(sender, args) {
            var item = args.get_item();
            var idPuestoSeleccionado = item.get_value();

            OpenSelectionWindow("../Administracion/VentanaVistaDescriptivo.aspx?PuestoId=" + idPuestoSeleccionado, "winEditarPuesto", "Editar puesto");
        }

        function OpenConfiguracionWindow(vIdPeriodo) {
            if (vIdPeriodo != null)
                OpenWindow(GetConfiguracionWindowProperties(vIdPeriodo));
        }

        function GetConfiguracionWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Configuración del período";
            wnd.vURL = "ConfiguracionPeriodo.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "winPeriodo";
            return wnd;
        }

        function TryParseInt(str, defaultValue) {
            if (/^\d+$/g.test(str) === true)
                return parseInt(str);

            return defaultValue;
        }
        
        function useDataFromChild(pDato) {
            if (pDato != null) {
                //SI EL pDato ES UN ENTERO ENTONCES SE CREO EL PERIODO
                var idCreado = TryParseInt(pDato, 0);
                if (idCreado !== 0) {
                    OpenConfiguracionWindow(idCreado);
                }
            }
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenInventario(pIdEmpleado) {
            var vURL = "../Administracion/Empleado.aspx";
            var vTitulo = "Ver Empleado";

            vURL = vURL + "?EmpleadoId=" + pIdEmpleado;

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {};
            windowProperties.width = document.documentElement.clientWidth - 20;
            windowProperties.height = document.documentElement.clientHeight - 20;

            openChildDialog(vURL, "winEditarEmpleado", vTitulo, windowProperties);
        }

        function OpenInsertPeriodoWindow() {
            OpenPeriodoWindow(null);
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function OpenPeriodoWindow(pIdPeriodo) {
            OpenWindow(GetPeriodoWindowProperties(pIdPeriodo));
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
        }

        function GetPeriodoWindowProperties(pIdPeriodo) {

            var wnd = GetWindowProperties();

            wnd.width = 750;
            wnd.height = 600;
            wnd.vTitulo = "Agregar período";
            wnd.vRadWindowId = "winPeriodo";
            
            var txtPuestoEval = document.getElementById("<%= txtPuesto.ClientID %>");
            var nombrePeriodo = "Evaluación plan de Sucesión " + txtPuestoEval.innerHTML;
            var idPuesto = '<%= vIdPuesto %>';
            var idsEmpleados = '<%= vXmlEmpleados %>';
            wnd.vURL = "PeriodoEvaluacion.aspx?evaluadoPS=" + nombrePeriodo + "&idPuestoPS=" + idPuesto + "&idsEmpleadosPS=" + idsEmpleados;
            return wnd;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="height: 10px;"></div>


    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="mpPlanVidaCarrera" SelectedIndex="0" AutoPostBack="false">

        <Tabs>
            <telerik:RadTab Text="Datos Generales"></telerik:RadTab>
            <telerik:RadTab Text="Datos de competencias"></telerik:RadTab>
        </Tabs>

    </telerik:RadTabStrip>

    <div style="height: calc(100% - 75px);">

        <telerik:RadMultiPage runat="server" ID="mpPlanVidaCarrera" SelectedIndex="0" Height="100%" BorderWidth="0">

            <telerik:RadPageView ID="pvGeneral" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div style="overflow: auto;">

                  <%--  <div class="ctrlBasico">
                        <label>Empleado a suplir:</label>
                        <telerik:RadTextBox ID="txtEmpleado" runat="server" Width="350px"></telerik:RadTextBox>
                        <telerik:RadButton runat="server" ID="btnEditarEmpleado" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenEmpleadoWindow" />
                    </div>--%>
                    <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Empleado a suplir:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtEmpleado" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>
                 <div class="ctrlBasico">
                       <telerik:RadButton runat="server" ID="btnEditarEmpleado" Text="V" Width="35px" AutoPostBack="false" OnClientClicked="OpenEmpleadoWindow" />
                     </div>


                  <%--  <div class="ctrlBasico">
                        <label>Puesto a suplir:</label>
                        <telerik:RadTextBox ID="txtPuesto" runat="server" Width="350px"></telerik:RadTextBox>
                        <telerik:RadButton runat="server" ID="btnEditarPuesto" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenPuestoWindow" />
                    </div>--%>
                      <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Puesto a suplir:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtPuesto" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>
                 <div class="ctrlBasico">
                       <telerik:RadButton runat="server" ID="btnEditarPuesto" Text="V" Width="35px" AutoPostBack="false" OnClientClicked="OpenPuestoWindow" />

                     </div>

                         <div class="ctrlBasico" style="text-align: center">
                    <telerik:RadButton ID="btnAgregar" runat="server" Text="Crear período" AutoPostBack="false" OnClientClicked="OpenInsertPeriodoWindow"></telerik:RadButton>
    </div>

                    <div style="clear: both; height: 10px;"></div>

                    <div class="ctrlBasico">
                        <fieldset>
                            <legend>
                                <label>Candidatos a sucesión:</label></legend>

                            <div style="padding: 5px">
                                <div class="ctrlBasico">
                                    <telerik:RadGrid runat="server" ID="rgSucesores" AutoGenerateColumns="false" OnNeedDataSource="rgSucesores_NeedDataSource" OnItemDataBound="rgSucesores_ItemDataBound">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridHyperLinkColumn UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO" DataTextField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" DataNavigateUrlFields="M_EMPLEADO_ID_EMPLEADO" HeaderText="Empleado" DataNavigateUrlFormatString="javascript:OpenEmpleadoSucesorWindow({0})"></telerik:GridHyperLinkColumn>
                                                <telerik:GridHyperLinkColumn UniqueName="M_PUESTO_NB_PUESTO" DataTextField="M_PUESTO_NB_PUESTO" DataNavigateUrlFields="M_PUESTO_ID_PUESTO" HeaderText="Puesto" DataNavigateUrlFormatString="javascript:OpenPuestoSucesorWindow({0})"></telerik:GridHyperLinkColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </fieldset>
                    </div>



                    <div class="ctrlBasico" style="float: right;">

                        <table style="width: 200px;">
                            <tr>
                                <td>
                                    <label>Alta:</label>
                                </td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: red;"></div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>Intermedia:</label></td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: gold;"></div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>No necesaria:</label></td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: green;"></div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>No calificada:</label></td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: lightgray;">
                                        <label style="text-align: center; width: 100%;">N/C</label>
                                    </div>
                                </td>
                            </tr>

                        </table>

                    </div>

                </div>

                <telerik:RadGrid runat="server" ID="grdCompetencias" ShowHeader="false" OnNeedDataSource="grdCompetencias_NeedDataSource" AutoGenerateColumns="false">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn ItemStyle-Width="150" DataField="Key" UniqueName="Value"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ItemStyle-Width="500" DataField="Value" UniqueName="Value"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

            </telerik:RadPageView>

            <telerik:RadPageView ID="pvCompetencias" runat="server">

                <telerik:RadPivotGrid runat="server" ID="pgCompetencias" OnNeedDataSource="pgCompetencias_NeedDataSource" RowTableLayout="Tabular" TotalsSettings-GrandTotalText="Promedio" OnCellDataBound="pgCompetencias_CellDataBound" CssClass="PivotCompetencias"
                    ShowDataHeaderZone="false" ShowRowHeaderZone="true"  RowHeaderCellStyle-BackColor="White"  ShowFilterHeaderZone="false" AllowFiltering="false" AllowSorting="false" ShowColumnHeaderZone="false" Height="100%" Width="100%">
                    <ColumnHeaderCellStyle Width="50px" />
                    <RowHeaderCellStyle Height="50px" Width="800px" />
                    <ClientSettings>
                        <Scrolling AllowVerticalScroll="true" />
                        <Resizing AllowColumnResize="false" />
                    </ClientSettings>
                    <SortExpressions>
                        <telerik:PivotGridSortExpression FieldName="NO_ORDEN" SortOrder="Ascending" />
                    </SortExpressions>
                    <TotalsSettings ColumnsSubTotalsPosition="None" RowGrandTotalsPosition="Last" RowsSubTotalsPosition="None" ColumnGrandTotalsPosition="None" />
                    <Fields>
                        <telerik:PivotGridRowField Caption=" " DataField="ID_COMPETENCIA" CellStyle-Width="0" CellStyle-Font-Size="0"></telerik:PivotGridRowField>
                        <telerik:PivotGridRowField Caption=" "  CellStyle-Font-Size="0"  DataField="CL_COLOR" >
                            <CellStyle Width="30px"/>
                            <CellTemplate>
                                <div style="height:100%; border-radius: 5px; background: <%# Container.DataItem.ToString() %>;">
                                    <br />
                                    <br />
                                </div>
                            </CellTemplate>
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField Caption="Factor"  DataField="NB_COMPETENCIA">
                            <CellStyle Width="230px" />
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField Caption="Descripción"  DataField="DS_COMPETENCIA" CellStyle-Width="470"></telerik:PivotGridRowField>

                        <telerik:PivotGridColumnField Caption="Empleado" DataField="CL_EMPLEADO" CellStyle-Width="100">
                            <CellTemplate>
                                <div style="text-align: center;">
                                    <%# Container.DataItem %>
                                </div>
                            </CellTemplate>
                        </telerik:PivotGridColumnField>
                        <telerik:PivotGridAggregateField Aggregate="Average" DataField="PR_NO_COMPATIBILIDAD" CellStyle-Width="100">

                            <CellTemplate>
                                <%--<div style="text-align: center;">
                                    <%# Container.DataItem %>
                                </div>--%>
                                <table style="width: 100%;" border="0">
                                    <tr>
                                        <td style="width:90%; border-width: 0px;">
                                            <div runat="server" id="divPromedio" style=" margin-bottom: auto; width:100%; margin-top: auto; float: left; text-align: right;">
                                                <%# Container.DataItem %>
                                            </div>
                                              <div runat="server" id="divNa" style=" margin-bottom: auto; width:100%; margin-top: auto; float: left; text-align: right;">
                                                N/A
                                            </div>
                                            <div runat="server" id="divNc" style="margin-bottom: auto; width:100%; margin-top: auto; float: left; text-align: right;">
                                                N/C
                                            </div>
                                        </td>
                                        <td style="width:10%;border-width: 0px;">
                                            <div runat="server" id="divColorComparacion" style="float: left; height: 80%; width: 10px; border-radius: 5px;">
                                                <br />
                                                <br />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </CellTemplate>
                        </telerik:PivotGridAggregateField>
                    </Fields>
                </telerik:RadPivotGrid>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadToolTipManager runat="server" ID="rtmInfoEmpleados" Position="MiddleLeft"
        RelativeTo="Element" Width="100px" Height="100px" Animation="Resize" HideEvent="LeaveTargetAndToolTip" OnAjaxUpdate="RadToolTipManager1_AjaxUpdate"
        RenderInPageRoot="true" AnimationDuration="300">
    </telerik:RadToolTipManager>

</asp:Content>
