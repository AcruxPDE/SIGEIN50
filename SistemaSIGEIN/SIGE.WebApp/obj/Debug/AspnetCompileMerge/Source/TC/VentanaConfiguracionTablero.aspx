<%@ Page Title="" Language="C#" MasterPageFile="~/TC/ContextTC.Master" AutoEventWireup="true" CodeBehind="VentanaConfiguracionTablero.aspx.cs" Inherits="SIGE.WebApp.TC.VentanaConfiguracionTablero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

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

        function OpenEmpleadosSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "winSeleccion", "Selección de evaluados");
        }

        function OpenPuestoSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPuesto.aspx", "winSeleccion", "Selección de puestos");
        }

        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx", "winSeleccion", "Selección de áreas/departamentos");
        }

        function confirmarEliminarEvaluados(sender, args) {
            var masterTable = $find("<%= grdEvaluados.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {

                var vNombre = "";
                var vMensaje = "";
                if (selectedItems.length == 1) {
                    vNombre = masterTable.getCellByColumnUniqueName(selectedItems[0], "NB_EMPLEADO_COMPLETO").innerHTML;
                    vMensaje = "¿Deseas eliminar a " + vNombre + " como evaluado?, este proceso no podrá revertirse";
                }
                else {
                    vNombre = selectedItems.length;
                    vMensaje = "¿Deseas eliminar los " + vNombre + " evaluadores seleccionados?, este proceso no podrá revertirse";
                }
                var vWindowsProperties = { height: 200 };

                confirmAction(sender, args, vMensaje, vWindowsProperties);
            }
            else {
                radalert("Selecciona un evaluado.", 400, 150);
                args.set_cancel(true);
            }
        }

        function confirmarEliminarPeriodosEvaluacion(sender, args) {
            var masterTable = $find("<%= grdCompetencia.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {

                var vNombre = "";
                var vMensaje = "";
                if (selectedItems.length == 1) {
                    vNombre = masterTable.getCellByColumnUniqueName(selectedItems[0], "NB_PERIODO").innerHTML;
                    vMensaje = "¿Deseas eliminar el período " + vNombre + " de la lista?, este proceso no podrá revertirse";
                }
                else {
                    vNombre = selectedItems.length;
                    vMensaje = "¿Deseas eliminar los " + vNombre + " períodos seleccionados?, este proceso no podrá revertirse";
                }
                var vWindowsProperties = { height: 200 };

                confirmAction(sender, args, vMensaje, vWindowsProperties);
            }
            else {
                radalert("Selecciona un período de evaluación.", 400, 150);
                args.set_cancel(true);
            }
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {

                    case "EMPLEADO":
                        InsertarDatos(EncapsularSeleccion("EVALUADO", pDato));
                        break;
                    case "PUESTO":
                        InsertarDatos(EncapsularSeleccion("PUESTO", pDato));
                        break;
                    case "DEPARTAMENTO":
                        InsertarDatos(EncapsularSeleccion("AREA", pDato));
                        break;
                    case "FD_EVALUACION":
                        InsertarDatos(EncapsularSeleccion("FD_EVALUACION", pDato));
                        break;
                    case "REBIND":
                        InsertarDatos(EncapsularSeleccion("REBIND", pDato));
                        break;
                    case "EO_DESEMPENO":
                        InsertarDatos(EncapsularSeleccion("EO_DESEMPENO", pDato));
                        break;
                    case "EO_CLIMA":
                        InsertarDatos(EncapsularSeleccion("EO_CLIMA", pDato));
                        break;
                    case "TABULADOR":
                        InsertarDatos(EncapsularSeleccion("TABULADOR", pDato));
                        break;
                }
            }
        }

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertarDatos(pDato) {
            var ajaxManager = $find('<%= ramConfiguracionTablero.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function OpenPeriodoFormacionSelectionWindow() {
            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vClTipo = "FD_EVALUACION";

            OpenSelectionWindow("../Comunes/SeleccionPeriodoParaTablero.aspx?IdPeriodoTablero=" + vIdPeriodo + "&ClTipoPeriodo=" + vClTipo, "winSeleccion", "Selección de período")
        }

        function OpenPeriodoDesempenoSelectionWindow() {
            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vClTipo = "EO_DESEMPENO";

            OpenSelectionWindow("../Comunes/SeleccionPeriodoParaTablero.aspx?IdPeriodoTablero=" + vIdPeriodo + "&ClTipoPeriodo=" + vClTipo, "winSeleccion", "Selección de período")
        }

        function OpenPeriodoClimaSelectionWindow() {
            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vClTipo = "EO_CLIMA";

            OpenSelectionWindow("../Comunes/SeleccionPeriodoParaTablero.aspx?IdPeriodoTablero=" + vIdPeriodo + "&ClTipoPeriodo=" + vClTipo, "winSeleccion", "Selección de período")
        }

        function OpenTabuladorSelectionWindow() {
            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vClTipo = "TABULADOR";

            OpenSelectionWindow("../Comunes/SeleccionPeriodoParaTablero.aspx?IdPeriodoTablero=" + vIdPeriodo + "&ClTipoPeriodo=" + vClTipo, "winSeleccion", "Selección de Tabulador")
        }

        function confirmarEliminarPeriodosClima(sender, args) {
            var masterTable = $find("<%= grdClima.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                var vNombre = "";
                var vMensaje = "";
                if (selectedItems.length == 1) {
                    vNombre = masterTable.getCellByColumnUniqueName(selectedItems[0], "NB_PERIODO").innerHTML;
                    vMensaje = "¿Deseas eliminar el período " + vNombre + " de la lista?, este proceso no podrá revertirse";
                }
                else {
                    vNombre = selectedItems.length;
                    vMensaje = "¿Deseas eliminar los " + vNombre + " períodos seleccionados?, este proceso no podrá revertirse";
                }
                var vWindowsProperties = { height: 200 };

                confirmAction(sender, args, vMensaje, vWindowsProperties);
            }
            else {
                radalert("Selecciona un periodo de clima laboral.", 400, 150);
                args.set_cancel(true);
            }
        }

        function confirmarEliminarPeriodosDesempeno(sender, args) {
            var masterTable = $find("<%= grdDesempeno.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                var vNombre = "";
                var vMensaje = "";
                if (selectedItems.length == 1) {
                    vNombre = masterTable.getCellByColumnUniqueName(selectedItems[0], "NB_PERIODO").innerHTML;
                    vMensaje = "¿Deseas eliminar el período " + vNombre + " de la lista?, este proceso no podrá revertirse";
                }
                else {
                    vNombre = selectedItems.length;
                    vMensaje = "¿Deseas eliminar los " + vNombre + " períodos seleccionados?, este proceso no podrá revertirse";
                }
                var vWindowsProperties = { height: 200 };

                confirmAction(sender, args, vMensaje, vWindowsProperties);
            }
            else {
                radalert("Selecciona un periodo de desempeño", 400, 150);
                args.set_cancel(true);
            }
        }

        function confirmarEliminarTabulador(sender, args) {
            var masterTable = $find("<%= grdSalarial.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                var vNombre = "";
                var vMensaje = "";
                if (selectedItems.length == 1) {
                    vNombre = masterTable.getCellByColumnUniqueName(selectedItems[0], "NB_PERIODO").innerHTML;
                    vMensaje = "¿Deseas eliminar el tabulador " + vNombre + " de la lista?, este proceso no podrá revertirse";
                }
                else {
                    vNombre = selectedItems.length;
                    vMensaje = "¿Deseas eliminar los " + vNombre + " tabuladores seleccionados?, este proceso no podrá revertirse";
                }
                var vWindowsProperties = { height: 200 };

                confirmAction(sender, args, vMensaje, vWindowsProperties);
            }
            else {
                radalert("Selecciona un tabulador.", 400, 150);
                args.set_cancel(true);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpConfiguracionTablero" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConfiguracionTablero" runat="server" OnAjaxRequest="ramConfiguracionTablero_AjaxRequest" DefaultLoadingPanelID="ralpConfiguracionTablero">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConfiguracionTablero">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdCompetencia" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdDesempeno" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdClima" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdSalarial" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarEvaluado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarPeriodoFyd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCompetencia" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarPeriodoDesempeno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDesempeno" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarPeriodoClima">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdClima" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarTabulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSalarial" LoadingPanelID="ralpConfiguracionTablero" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Tablero de control:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtIdPeriodo" runat="server"></div>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtNbPeriodo" runat="server"></div>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadTabStrip ID="rtsConfiguracionPeriodo" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracionTablero">
        <Tabs>
            <telerik:RadTab Text="Selección de evaluados" Value="0"></telerik:RadTab>
            <telerik:RadTab Text="Resultado de pruebas" Value="1"></telerik:RadTab>
            <telerik:RadTab Text="Evaluación de competencias" Value="2"></telerik:RadTab>
            <telerik:RadTab Text="Evaluación de desempeño" Value="3"></telerik:RadTab>
            <telerik:RadTab Text="Resultados clima laboral" Value="4"></telerik:RadTab>
            <telerik:RadTab Text="Situación salarial" Value="5"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 100px); padding-top: 10px;">
        <telerik:RadMultiPage ID="rmpConfiguracionTablero" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvSeleccionEvaluados" runat="server">
                <div style="height: calc(100% - 45px); padding-bottom: 10px;">
                    <telerik:RadGrid ID="grdEvaluados" runat="server" Height="100%"
                        AutoGenerateColumns="false" EnableHeaderContextMenu="true"
                        AllowSorting="true" AllowMultiRowSelection="true" HeaderStyle-Font-Bold="true"
                        OnNeedDataSource="grdEvaluados_NeedDataSource">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EMPLEADO,ID_EVALUADO" ClientDataKeyNames="ID_EMPLEADO,ID_EVALUADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" EnableHierarchyExpandAll="true" HierarchyDefaultExpanded="true" Name="Parent">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno" DataField="NB_APELLIDO_PATERNO" UniqueName="NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido materno" DataField="NB_APELLIDO_MATERNO" UniqueName="NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave del área/departamento" DataField="CL_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_CL_DEPARTAMENTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género" DataField="CL_GENERO" UniqueName="CL_GENERO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado civil" DataField="CL_ESTADO_CIVIL" UniqueName="CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre del cónyuge" DataField="NB_CONYUGUE" UniqueName="NB_CONYUGUE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="RFC" DataField="CL_RFC" UniqueName="CL_RFC"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="CURP" DataField="CL_CURP" UniqueName="CL_CURP"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="NSS" DataField="CL_NSS" UniqueName="CL_NSS"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Tipo sanguíneo" DataField="CL_TIPO_SANGUINEO" UniqueName="CL_TIPO_SANGUINEO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nacionalidad" DataField="CL_NACIONALIDAD" UniqueName="CL_NACIONALIDAD"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="País" DataField="NB_PAIS" UniqueName="NB_PAIS"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Entidad federativa" DataField="NB_ESTADO" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Municipio" DataField="NB_MUNICIPIO" UniqueName="NB_MUNICIPIO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Colonia" DataField="NB_COLONIA" UniqueName="NB_COLONIA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calle" DataField="NB_CALLE" UniqueName="NB_CALLE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Exterior" DataField="NO_EXTERIOR" UniqueName="NO_EXTERIOR"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Interior" DataField="NO_INTERIOR" UniqueName="NO_INTERIOR"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Código postal" DataField="CL_CODIGO_POSTAL" UniqueName="CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Correo electrónico" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Natalicio" DataField="FE_NACIMIENTO" UniqueName="FE_NACIMIENTO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Lugar de nacimiento" DataField="DS_LUGAR_NACIMIENTO" UniqueName="DS_LUGAR_NACIMIENTO"></telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de alta" DataField="FE_ALTA" UniqueName="FE_ALTA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de baja" DataField="FE_BAJA" UniqueName="FE_BAJA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo" DataField="MN_SUELDO" UniqueName="MN_SUELDO" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo variable" DataField="MN_SUELDO_VARIABLE" UniqueName="MN_SUELDO_VARIABLE" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Composición del sueldo" DataField="DS_SUELDO_COMPOSICION" UniqueName="DS_SUELDO_COMPOSICION"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave de la empresa" DataField="CL_EMPRESA" UniqueName="CL_EMPRESA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre de la empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Razón social" DataField="NB_RAZON_SOCIAL" UniqueName="NB_RAZON_SOCIAL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Estado" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Activo" DataField="FG_ACTIVO" UniqueName="FG_ACTIVO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorPersona" runat="server" Text="Agregar desde inventario" AutoPostBack="false" OnClientClicked="OpenEmpleadosSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorPuesto" runat="server" Text="Agregar desde descriptivos de puesto" AutoPostBack="false" OnClientClicked="OpenPuestoSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorArea" runat="server" Text="Agregar desde áreas/departamentos" AutoPostBack="false" OnClientClicked="OpenAreaSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarEvaluado" runat="server" Text="Eliminar" OnClientClicking="confirmarEliminarEvaluados" OnClick="btnEliminarEvaluado_Click"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvPruebas" runat="server">
                <div style="height: calc(100% - 45px); padding-bottom: 10px;">
                    <telerik:RadGrid ID="grdCandidatos" runat="server" Height="100%"
                        AutoGenerateColumns="false" EnableHeaderContextMenu="true"
                        AllowSorting="true" AllowMultiRowSelection="true" HeaderStyle-Font-Bold="true"
                        OnNeedDataSource="grdCandidatos_NeedDataSource">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EVALUADO,ID_EMPLEADO,ID_CANDIDATO,ID_SOLICITUD" ClientDataKeyNames="ID_EVALUADO,ID_EMPLEADO,ID_CANDIDATO,ID_SOLICITUD" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" EnableHierarchyExpandAll="true" HierarchyDefaultExpanded="true" Name="Parent">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="90" HeaderText="Clave de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="60" HeaderText="Fecha de solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre empleado" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave de candidato" DataField="CL_CANDIDATO" UniqueName="CL_CANDIDATO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre candidato" DataField="NB_CANDIDATO_COMPLETO" UniqueName="NB_CANDIDATO_COMPLETO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvCompetencias" runat="server">
                <div style="height: calc(100% - 45px);">
                    <telerik:RadGrid runat="server" ID="grdCompetencia" Height="100%" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdCompetenci_NeedDataSource" AllowMultiRowSelection="true">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_PERIODO,ID_PERIODO_REFERENCIA_TABLERO" ClientDataKeyNames="ID_PERIODO,ID_PERIODO_REFERENCIA_TABLERO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="90" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre" DataField="NB_PERIODO" UniqueName="NB_PERIODO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="220" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarPeriodoFyd" runat="server" Text="Agregar período" AutoPostBack="false" OnClientClicked="OpenPeriodoFormacionSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarPeriodoFyd" runat="server" Text="Eliminar período" OnClientClicking="confirmarEliminarPeriodosEvaluacion" OnClick="btnEliminarPeriodoFyd_Click"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvDesempeno" runat="server">
                <div style="height: calc(100% - 45px);">
                    <telerik:RadGrid runat="server" ID="grdDesempeno" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="false" OnNeedDataSource="grdDesempeno_NeedDataSource" AllowMultiRowSelection="true">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_PERIODO,ID_PERIODO_REFERENCIA_TABLERO" ClientDataKeyNames="ID_PERIODO,ID_PERIODO_REFERENCIA_TABLERO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="90" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre" DataField="NB_PERIODO" UniqueName="NB_PERIODO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="220" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarPeriodoDesempeno" runat="server" Text="Agregar período" AutoPostBack="false" OnClientClicked="OpenPeriodoDesempenoSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarPeriodoDesempeno" runat="server" Text="Eliminar período" OnClientClicking="confirmarEliminarPeriodosDesempeno" OnClick="btnEliminarPeriodoDesempeno_Click"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvClima" runat="server">
                <div style="height: calc(100% - 45px);">
                    <telerik:RadGrid runat="server" ID="grdClima" Height="100%" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdClima_NeedDataSource" AllowMultiRowSelection="true">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_PERIODO,ID_PERIODO_REFERENCIA_TABLERO" ClientDataKeyNames="ID_PERIODO,ID_PERIODO_REFERENCIA_TABLERO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="90" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre" DataField="NB_PERIODO" UniqueName="NB_PERIODO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="220" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarPeriodoClima" runat="server" Text="Agregar período" AutoPostBack="false" OnClientClicked="OpenPeriodoClimaSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarPeriodoClima" runat="server" Text="Eliminar período" OnClientClicking="confirmarEliminarPeriodosClima" OnClick="btnEliminarPeriodoClima_Click"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvSalarial" runat="server">
                <div style="height: calc(100% - 45px);">
                    <telerik:RadGrid runat="server" ID="grdSalarial" Height="100%" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="grdSalarial_NeedDataSource" AllowMultiRowSelection="true">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_PERIODO,ID_PERIODO_REFERENCIA_TABLERO" ClientDataKeyNames="ID_PERIODO,ID_PERIODO_REFERENCIA_TABLERO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="90" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre" DataField="NB_PERIODO" UniqueName="NB_PERIODO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="220" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarTabulador" runat="server" Text="Agregar tabulador" AutoPostBack="false" OnClientClicked="OpenTabuladorSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarTabulador" runat="server" Text="Eliminar tabulador" OnClientClicking="confirmarEliminarTabulador" OnClick="btnEliminarTabulador_Click"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
