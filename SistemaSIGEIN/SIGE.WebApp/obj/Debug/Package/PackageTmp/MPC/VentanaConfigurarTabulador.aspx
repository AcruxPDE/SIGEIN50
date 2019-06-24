<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="VentanaConfigurarTabulador.aspx.cs" Inherits="SIGE.WebApp.MPC.VentanaConfigurarTabulador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function onCloseWindowC(oWnd, args) {
                $find("<%=grdCompetenciasSeleccionadas.ClientID%>").get_masterTableView().rebind();
            }

            function onCloseWindowE(oWnd, args) {
                $find("<%=grdEmpleadosSeleccionados.ClientID%>").get_masterTableView().rebind();
            }

            function closeWindow() {
                var pDatos = [{
                    clTipoCatalogo: "ACTUALIZAR"

                }];
                cerrarVentana(pDatos);
            }

            function cerrarVentana(recargarGrid) {
                sendDataToParent(recargarGrid);
            }


            function OpenPuestosSelectionWindow() {
                var vPropierties = GetWindowProperties();
                openChildDialog("../Comunes/SeleccionPuesto.aspx?&vClTipoPuesto=<%=vClTipoPuesto%>" + "&vClTipoSeleccion=MC_PUESTO&mulSel=1", "winSeleccion", "Selección de puestos", vPropierties)
            }

            function OpenSelectionDepartamento() {
                var vPropierties = GetWindowProperties();
                openChildDialog("../Comunes/SeleccionArea.aspx?vClTipoPuesto=<%=vClTipoPuesto%>" + "&vClTipoSeleccion=MC_PUESTO", "winSeleccion", "Selección de áreas/departamentos", vPropierties)
            }

            function OpenEmployeeSelectionWindow() {
                var vPropierties = GetWindowProperties();
                openChildDialog("../Comunes/SeleccionEmpleado.aspx?&vClTipoPuesto=<%=vClTipoPuesto%>" + "&vClTipoSeleccion=MC_PUESTO", "winSeleccion", "Selección de empleados", vPropierties)
            }

            function OpenSelectionCompetencia() {
                var vPropierties = GetWindowProperties();
                openChildDialog("../Comunes/SeleccionCompetencia.aspx?vClTipoCompetencia=GEN", "winSeleccion", "Selección de competencias genéricas", vPropierties)
            }

            function OpenSelectionCompetenciaEspecificas() {
                var vPropierties = GetWindowProperties();
                openChildDialog("../Comunes/SeleccionCompetencia.aspx?vClTipoCompetencia=ESP", "winSeleccion", "Selección de competencias específicas", vPropierties)
            }

            function OpenSelectionFactores() {
                var vPropierties = GetWindowProperties();
                openChildDialog("../Comunes/SelectorFactoresEval.aspx", "winSeleccion", "Selección de factores de evaluación", vPropierties)
            }

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

            function AbrirVentana() {
                var vPropierties = GetWindowProperties();
                openChildDialog("VentanaTabuladorCompetencia.aspx?&IDtabulador=" + <%=vIdTabulador%> + "", "winEditarCompetencia", "Nuevo/Editar Factor valuación")
            }

            function useDataFromChild(pDato) {
                if (pDato != null) {
                    var vDatosSeleccionados = pDato[0];
                    var arr = [];
                    switch (vDatosSeleccionados.clTipoCatalogo) {
                        case "EMPLEADO":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idEmpleado);
                            }
                            var datos = JSON.stringify(arr);
                            __doPostBack(vDatosSeleccionados.clTipoCatalogo, datos);
                            $find("<%=grdEmpleadosSeleccionados.ClientID%>").get_masterTableView().rebind();
                            break;
                        case "COMPETENCIA":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idCompetencia);
                            }
                            var datos = JSON.stringify(arr);
                            __doPostBack(vDatosSeleccionados.clTipoCatalogo, datos);
                            $find("<%=grdCompetenciasSeleccionadas.ClientID%>").get_masterTableView().rebind();
                            break;
                        case "FACTOR":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idFactor);
                            }
                            var datos = JSON.stringify(arr);
                            __doPostBack(vDatosSeleccionados.clTipoCatalogo, datos);
                            $find("<%=grdCompetenciasSeleccionadas.ClientID%>").get_masterTableView().rebind();
                            break;
                        case "PUESTO":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idPuesto);
                            }
                            var datos = JSON.stringify(arr);
                            __doPostBack(vDatosSeleccionados.clTipoCatalogo, datos);
                            break;
                        case "DEPARTAMENTO":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idArea);
                            }
                            var datos = JSON.stringify(arr);
                            __doPostBack(vDatosSeleccionados.clTipoCatalogo, datos);
                            break;
                        case "TabCompetencia":
                            $find("<%=grdCompetenciasSeleccionadas.ClientID%>").get_masterTableView().rebind();
                            break;
                    }
                }
            }

            var idFactor = "";
            function obtenerIdFila() {
                var grid = $find("<%=grdCompetenciasSeleccionadas.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    console.info(SelectDataItem);
                    idFactor = SelectDataItem.getDataKeyValue("ID_TABULADOR_FACTOR");
                    vNombre = MasterTable.getCellByColumnUniqueName(SelectDataItem, "NB_COMPETENCIA").innerHTML;
                }
            }
            function EditarCompetencia() {
                obtenerIdFila();
                if (idFactor != "") {
                    var selectedItem = $find("<%=grdCompetenciasSeleccionadas.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("ID_COMPETENCIA");
                    if (selectedItem == null) {
                        openChildDialog("VentanaTabuladorCompetencia.aspx?&IDFactor=" + idFactor + "&IDtabulador=" + <%=vIdTabulador%> + "", "winEditarCompetencia",
                            "Nuevo/Editar Competencia")
                    }
                    else {
                        radalert("La competencia no se puede editar.", 400, 150, "Aviso");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Selecciona una competencia.", 400, 150, "Aviso");
                    args.set_cancel(true);
                }

            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEmpleadosSeleccionados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCompetenciasSeleccionadas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCompetenciasSeleccionadas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarCompetencias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCompetenciasSeleccionadas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCompetenciasSeleccionadas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnEditarCompetencias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <%--  <div class="ctrlBasico">
        <label id="lbTabulador"
            name="lbTabulador"
            runat="server">
            Tabulador:</label>
        <telerik:RadTextBox ID="txtClTabulador"
            runat="server"
            Width="150px"
            MaxLength="800"
            Enabled="false">
        </telerik:RadTextBox>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox ID="txtNbTabulador"
            runat="server"
            Width="260px"
            MaxLength="800"
            Enabled="false">
        </telerik:RadTextBox>
    </div>
    <div class="ctrlBasico">
        <label id="lbFecha"
            name="lbFecha"
            runat="server">
            Fecha:</label>
        <telerik:RadDatePicker ID="rdpCreacion" Enabled="false" runat="server" Width="140px">
        </telerik:RadDatePicker>
    </div>
    <div class="ctrlBasico">
        <label id="lbVigencia"
            name="lbVigencia"
            runat="server">
            Vigencia:</label>
        <telerik:RadDatePicker ID="rdpVigencia" Enabled="false" runat="server" Width="140px">
        </telerik:RadDatePicker>
    </div>--%>
    <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" MultiPageID="rmpConfiguracion">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Selección de empleados" SelectedIndex="1"></telerik:RadTab>
            <telerik:RadTab Text="Valores de valuación" SelectedIndex="2"></telerik:RadTab>
            <%-- <telerik:RadTab Text="Nivel de mercado" SelectedIndex="3"></telerik:RadTab>--%>
            <telerik:RadTab Text="Parámetros" SelectedIndex="3"></telerik:RadTab>
            <telerik:RadTab Text="Niveles" SelectedIndex="4"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 60px);">
        <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Versión:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtClTabulador" runat="server" style="min-width: 100px;"></div>
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
            <telerik:RadPageView ID="rpvSeleccionEmpleados" runat="server">
                <telerik:RadSplitter ID="rsSeleccionEmpleados" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpSeleccionEmpleados" runat="server">
                        <div style="clear: both; height: 10px;"></div>
                        <div style="height: calc(100% - 70px);">
                            <telerik:RadGrid ID="grdEmpleadosSeleccionados"
                                runat="server"
                                Height="100%"
                                Width="950"
                                AllowSorting="true"
                                AllowFilteringByColumn="true"
                                ShowHeader="true"
                                HeaderStyle-Font-Bold="true"
                                OnNeedDataSource="grdEmpleadosSeleccionados_NeedDataSource"
                                AllowMultiRowSelection="true"
                                OnItemDataBound="grdEmpleadosSeleccionados_ItemDataBound">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_TABULADOR_EMPLEADO" ClientDataKeyNames="ID_TABULADOR_EMPLEADO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                        AddNewRecordText="Insertar" />
                                    <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="260" FilterControlWidth="240" HeaderText="Nombre completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccionarEmpleados" runat="server" name="btnSeleccionarEmpleados" AutoPostBack="false" Text="Seleccionar por persona" OnClientClicked="OpenEmployeeSelectionWindow"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccionarPuestos" runat="server" name="btnSeleccionarPuestos" AutoPostBack="false" Text="Seleccionar por puesto" OnClientClicked="OpenPuestosSelectionWindow"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccionarAreas" runat="server" name="btnSeleccionarAreas" AutoPostBack="false" Text="Seleccionar por área/departamento" OnClientClicked="OpenSelectionDepartamento"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnEliminarEmpleados" OnClick="btnEliminarEmpleados_Click" runat="server" name="btnEliminarEmpleados" AutoPostBack="true" Text="Eliminar"></telerik:RadButton>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyudaSeleccionEmpleados" runat="server" Scrolling="None" Width="22px">
                        <telerik:RadSlidingZone ID="rszSeleccionEmpleados" runat="server" SlideDirection="Left" ExpandedPaneId="rsSeleccionEmpleados" Width="22px" ClickToOpen="true">
                            <telerik:RadSlidingPane ID="rspAyudaSeleccionEmpleados" runat="server" Title="Ayuda" Width="250px" RenderMode="Mobile" Height="100%">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>
                                        Utiliza esta página para seleccionar al grupo que participará en el proceso. Si seleccionas por área/departamento, automáticamente todos los empleados que pertenecen a dicha área/departamento serán seleccionados; lo mismo sucede si seleccionas por puesto.
                                    <br />
                                        <br />
                                        Estos son los sueldos que se asignarán al tabulador . Si deseas hacer una modificación deberás hacerla en el inventario de personal y generar el tabulador nuevamente.
                                    </p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvFactoresValuacion" runat="server">
                <telerik:RadSplitter ID="rsFactoresValuacion" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpFactoresValuacion" runat="server">
                        <div style="clear: both; height: 10px;"></div>
                        <div style="height: calc(100% - 70px);">
                            <telerik:RadGrid ID="grdCompetenciasSeleccionadas"
                                runat="server"
                                Height="100%"
                                Width="950"
                                AllowSorting="true"
                                AllowFilteringByColumn="true"
                                HeaderStyle-Font-Bold="true"
                                ShowHeader="true"
                                OnNeedDataSource="grdCompetenciasSeleccionadas_NeedDataSource"
                                OnSelectedIndexChanged="grdCompetenciasSeleccionadas_SelectedIndexChanged"
                                AllowMultiRowSelection="true"
                                OnItemDataBound="grdCompetenciasSeleccionadas_ItemDataBound">
                                <ClientSettings EnablePostBackOnRowClick="true">
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_TABULADOR_FACTOR, ID_COMPETENCIA, CL_TIPO_COMPETENCIA" ClientDataKeyNames="ID_TABULADOR_FACTOR, ID_COMPETENCIA, CL_TIPO_COMPETENCIA" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                        AddNewRecordText="Insertar" />
                                    <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Nombre"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="DS_COMPETENCIA" UniqueName="DS_COMPETENCIA" CurrentFilterFunction="Contains" HeaderStyle-Width="260" FilterControlWidth="240" HeaderText="Descripción"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccionarCompetencia" runat="server" name="btnSeleccionarCompetencia" AutoPostBack="false" Text="Genéricas" Width="100" OnClientClicked="OpenSelectionCompetencia"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSelecionarCompetenciaEspecifica" runat="server" name="btnSeleccionarCompetenciaEspecifica" AutoPostBack="false" Text="Específicas" Width="100" OnClientClicked="OpenSelectionCompetenciaEspecificas"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccionarFactores" runat="server" name="btnSeleccionarFactores" AutoPostBack="false" Text="Factores" Width="100" OnClientClicked="OpenSelectionFactores"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnAgregarCompetencia" runat="server" OnClientClicked="AbrirVentana" name="btnAgregarCompetencia" AutoPostBack="false" Text="Factores Adicionales" Width="160"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnEditarCompetencias" Visible="false" runat="server" OnClientClicking="EditarCompetencia" name="btnEditarCompetencias" AutoPostBack="false" Text="Editar" Width="100"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnEliminarCompetencias" OnClick="btnEliminarCompetencias_Click" runat="server" name="btnEliminarCompetencias" AutoPostBack="true" Text="Eliminar" Width="100"></telerik:RadButton>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyudaFactoresValuacion" runat="server" Scrolling="None" Width="22px">
                        <telerik:RadSlidingZone ID="rszFactoresValuacion" runat="server" SlideDirection="Left" ExpandedPaneId="rsFactoresValuacion" Width="22px" ClickToOpen="true">
                            <telerik:RadSlidingPane ID="rspAyudaFactoresValuacion" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="200">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>Determina los factores de valuación con los que deseas realizar el tabulador de sueldos. Te recomendamos no elegir más de 4 factores.</p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            <%--<telerik:RadPageView ID="rpvConsultasComparativas" runat="server">
                <telerik:RadSplitter ID="rsConsultasComparativas" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpConsultasComparativas" runat="server">
                        <div style="clear: both; height: 10px"></div>
                        <div class="ctrlBasico">
                            <label id="lbNivelMercado"
                                name="lbNivelMercado"
                                runat="server">
                                Nivel de mercado</label>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbNivelMercado" Width="190" MarkFirstMatch="true" AutoPostBack="false" DropDownWidth="190" EmptyMessage="Seleccione...">
                            </telerik:RadComboBox>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyudaConsultasComparativas" runat="server" Scrolling="None" Width="22px">
                        <telerik:RadSlidingZone ID="rszConsultasComparativas" runat="server" SlideDirection="Left" ExpandedPaneId="rsConsultasComparativas" Width="22px">
                            <telerik:RadSlidingPane ID="rspAyudaConsultasComparativas" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="200">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>Para fines comparativos el sistema utiliza uno de los niveles de los cuartiles del mercado salarial, esta decisión puede conservarse grabando de manera preseterminada tu selección.</p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>--%>
            <telerik:RadPageView ID="rpvNivelesParametros" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <label id="lbNivelMercado"
                        name="lbNivelMercado"
                        runat="server">
                        Nivel de mercado:</label>
                    <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbNivelMercado" ToolTip="Para fines comparativos el sistema utiliza uno de los niveles de los cuartiles del mercado salarial, esta decisión puede conservarse grabando de manera preseterminada tu selección." Width="190" MarkFirstMatch="true" AutoPostBack="false" DropDownWidth="190" EmptyMessage="Seleccione...">
                    </telerik:RadComboBox>
                    *Nivel de mercado con el que se presentaran las consultas por defecto.
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <label id="lbNivelesACrear"
                        name="lbNivelesACrear"
                        runat="server">
                        Número de niveles a crear:</label>
                    <%--  <telerik:RadTextBox ID="txtNivelesACrear"
                        runat="server"
                        Width="80px" >
                    </telerik:RadTextBox>--%>
                    <telerik:RadNumericTextBox runat="server" ID="rntNivelesACrear"
                        Width="80px" MinValue="0" ShowSpinButtons="true">
                    </telerik:RadNumericTextBox>
                </div>
                <div class="ctrlBasico">
                    <label runat="server" id="lblAdvertencia" visible="false" style="color: red;"></label>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <label id="lbNumeroCategorias"
                        name="lbNumeroCategorias"
                        runat="server">
                        Número de categorias dentro de cada nivel:</label>
                    <%--<telerik:RadTextBox ID="txtNumeroCategorias"
                        runat="server"
                        Width="80px" ToolTip="Categorías a crear dentro del mismo nivel, ejemplo A, B, C para tres categorías." >
                    </telerik:RadTextBox>--%>
                    <telerik:RadNumericTextBox runat="server" ID="rntNumeroCategorias"
                        Width="80px" MinValue="0" ShowSpinButtons="true" ToolTip="Categorías a crear dentro del mismo nivel, ejemplo A, B, C para tres categorías.">
                    </telerik:RadNumericTextBox>
                </div>
                <div class="ctrlBasico">
                    <label id="lbProgresion"
                        name="lbProgresion"
                        runat="server">
                        Progresión:</label>
                    <%--<telerik:RadTextBox ID="txtProgresion"
                        runat="server"
                        Width="80px" ToolTip="Porcentaje de incremento entre una categoría y otra dentro del mismo nivel.">
                    </telerik:RadTextBox>--%>
                    <telerik:RadNumericTextBox runat="server" ID="rntProgresion"
                        Width="80px" MinValue="0" ShowSpinButtons="true" ToolTip="Porcentaje de incremento entre una categoría y otra dentro del mismo nivel.">
                    </telerik:RadNumericTextBox>
                </div>
                <div style="clear: both; height: 10px"></div>
                <div class="ctrlBasico">
                    <label id="lbPorcentaje"
                        name="lbPorcentaje"
                        runat="server">
                        Porcentaje de incremento inflacional para el siguiente año:</label>
                    <%-- <telerik:RadTextBox ID="txtPorcentaje"
                        runat="server"
                        Width="80px" ToolTip="Porcentaje para determinar el sueldo por categoría y nivel para determinar el sueldo del siguiente año.">
                    </telerik:RadTextBox>--%>
                    <telerik:RadNumericTextBox runat="server" ID="rntPorcentaje"
                        Width="80px" MinValue="0" ShowSpinButtons="true" ToolTip="Porcentaje para determinar el sueldo por categoría y nivel para determinar el sueldo del siguiente año.">
                    </telerik:RadNumericTextBox>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <label id="lbCuartilInflacional"
                        name="lbCuartilInflacional"
                        runat="server">
                        Percentil del tabulador maestro para incremento inflacional:</label>
                    <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbCuartilInflacional" Width="190" MarkFirstMatch="true" AutoPostBack="false" EmptyMessage="Seleccione..."
                        DropDownWidth="190">
                    </telerik:RadComboBox>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <label id="lbCuartilSueldo"
                        name="lbCuartilSueldo"
                        runat="server">
                        Percentil del tabulador maestro para incrementos de sueldo:</label>
                    <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbCuartilIncremento" Width="190" MarkFirstMatch="true" AutoPostBack="false" EmptyMessage="Seleccione..."
                        DropDownWidth="190">
                    </telerik:RadComboBox>
                </div>
                <%-- <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <label id="lbSueldo"
                        name="lbSueldo"
                        runat="server">
                        Sueldo o salario a comparar en las consultas:</label>
                    <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbSueldo" Width="190" MarkFirstMatch="true" AutoPostBack="false" EmptyMessage="Seleccione..."
                        DropDownWidth="190" ToolTip="Solo si cuentas con el módulo de nómina.">
                        <Items>
                            <telerik:RadComboBoxItem Text="Salario nominal" Value="" />
                            <telerik:RadComboBoxItem Text="Salario integrado" Value="" />
                            <telerik:RadComboBoxItem Text="Último salario neto" Value="" />
                        </Items>
                    </telerik:RadComboBox>
                </div>--%>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <label id="lbVariacionVerde"
                        name="lbVariacionVerde"
                        runat="server">
                        Variación para semáforo verde:</label>
                    <%-- <telerik:RadTextBox ID="txtVariacionVerde"
                        runat="server"
                        Width="80px"
                        Text="10">
                    </telerik:RadTextBox>--%>
                    <telerik:RadNumericTextBox runat="server" ID="rntVariacionVerde"
                        Width="80px" MinValue="0" ShowSpinButtons="true" Value="10">
                    </telerik:RadNumericTextBox>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <label id="lbVariacionAmarillo"
                        name="lbVariacionAmarillo"
                        runat="server">
                        Variación para semáforo amarillo:</label>
                    <%-- <telerik:RadTextBox ID="txtVariacionAmarillo"
                        runat="server"
                        Width="80px"
                        Text="20">
                    </telerik:RadTextBox>--%>
                    <telerik:RadNumericTextBox runat="server" ID="rntVariacionAmarillo"
                        Width="80px" MinValue="0" ShowSpinButtons="true" Value="20">
                    </telerik:RadNumericTextBox>
                </div>
                <div style="height: 10px; clear: both"></div>
                          <div class="divControlDerecha">
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardarConfiguracion" runat="server" name="btnGuardarConfiguracion" AutoPostBack="true" Text="Guardar" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
                </div>
                              </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvNiveles" runat="server">
                <div style="height: calc(100% - 50px);">
                    <telerik:RadGrid ID="grdNiveles"
                        runat="server"
                        Width="850px"
                        Height="100%"
                        AllowSorting="true"
                        AllowFilteringByColumn="true"
                        ShowHeader="true"
                        HeaderStyle-Font-Bold="true"
                        OnItemDataBound="grdNiveles_ItemDataBound"
                        OnNeedDataSource="grdNiveles_NeedDataSource">
                        <GroupingSettings CaseSensitive="false" />
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="ID_TABULADOR_NIVEL" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Clave" HeaderStyle-Width="100" FilterControlWidth="60" AutoPostBackOnFilter="false" DataField="CL_TABULADOR_NIVEL" UniqueName="CL_TABULADOR_NIVEL">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtClNivel" runat="server" Enabled="false" Width="90px" MaxLength="800" Text='<%#Eval("CL_TABULADOR_NIVEL") %>'></telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Nombre" HeaderStyle-Width="180" FilterControlWidth="150" AutoPostBackOnFilter="false" DataField="NB_TABULADOR_NIVEL" UniqueName="NB_TABULADOR_NIVEL">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtNbNivel" runat="server" Width="220px" MaxLength="800" Text='<%#Eval("NB_TABULADOR_NIVEL") %>'></telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="70" FilterControlWidth="40" HeaderText="Nivel" DataField="NO_NIVEL" UniqueName="NO_NIVEL" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Orden" HeaderStyle-Width="70" FilterControlWidth="50" AutoPostBackOnFilter="false" DataField="NO_ORDEN" UniqueName="NO_ORDEN">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox runat="server" ID="txnOrden" Name="txnOrden" Width="60px" MinValue="1" ShowSpinButtons="true" Text='<%#Eval("NO_ORDEN") %>' NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Progresión" HeaderStyle-Width="70" FilterControlWidth="50" AutoPostBackOnFilter="false" DataField="PR_PROGRESION" UniqueName="PR_PROGRESION">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox runat="server" ID="txnProgresion" Name="txtProgresion" Width="60px" MinValue="0" ShowSpinButtons="true" Text='<%#Eval("PR_PROGRESION") %>' NumberFormat-DecimalDigits="0" ToolTip="Indica el porcentaje para el incremento entre las posibilidades por nivel."></telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="divControlDerecha">
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardar" AutoPostBack="true" OnClick="btnGuardar_Click" runat="server" Text="Guardar y cerrar" ></telerik:RadButton>
                </div>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <%--<div style="clear: both"></div>
           <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardarConfiguracion" runat="server" name="btnGuardarConfiguracion" AutoPostBack="true" Text="Guardar" Width="100" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
        </div>--%>
        <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
    </div>
</asp:Content>
