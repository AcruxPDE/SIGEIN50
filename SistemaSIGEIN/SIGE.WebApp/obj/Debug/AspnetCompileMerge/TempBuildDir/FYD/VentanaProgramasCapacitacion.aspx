<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaProgramasCapacitacion.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaProgramasCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script src="../Assets/js/jquery.min.js"></script>
    <script id="MyScript" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }

        function onCloseWindow(oWnd, args) {

            $find("<%=grdCompetencias.ClientID%>").get_masterTableView().rebind();
            $find("<%=grdParticipantes.ClientID%>").get_masterTableView().rebind();
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenEmployeeSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "winSeleccion", "Selección de empleado");
        }

        function OpenSelectionCompetencia() {
            OpenSelectionWindow("../Comunes/SeleccionCompetencia.aspx", "winSeleccion", "Selección de competencias");
        }

        function CleanEmployeeSelection(sender, args) {
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                //console.info(pDato);
                var vDatosSeleccionados = pDato[0];
                var arr = [];
                var datos = new Array();

                switch (vDatosSeleccionados.clTipoCatalogo) {
                    case "EMPLEADO":
                        for (var i = 0; i < pDato.length; ++i) {
                            arr.push(pDato[i].idEmpleado);
                        }
                        datos = arr;
                        break;
                    case "COMPETENCIA":
                        for (var i = 0; i < pDato.length; ++i) {
                            arr.push(pDato[i].idCompetencia);
                        }
                        datos = arr;
                        break;
                }
                var ajaxManager = $find("<%= ramProgramasCapacitacion.ClientID %>");
                ajaxManager.ajaxRequest(JSON.stringify(datos) + "_" + vDatosSeleccionados.clTipoCatalogo); //Making ajax request with the argument 
            }
        }

        function ConfirmarEliminarCompetencia(sender, args) {
            var MasterTable = $find("<%=grdCompetencias.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            var row = selectedRows[0];

            if (row != null) {
                CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_COMPETENCIA");
                if (selectedRows != "") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar la competencia ' + CELL_NOMBRE.innerHTML + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                    args.set_cancel(true);
                }
            } else {
                radalert("Seleccione una competencia.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function OpenRadWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function GetAutorizacionProgramaProperties(pIdPrograma) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            var wnd = windowProperties;
            wnd.vURL = "VentanaDocumentoAutorizar.aspx?IdPrograma=" + pIdPrograma + "&TIPO=Agregar";
            wnd.vTitulo = "Registro y Autorización";
            wnd.vRadWindowId = "winAutoriza";
            return wnd;
        }

        function OpenAutorizacionProgramaWindow() {
            var vIdPrograma = '<%= vIdPrograma %>';
            if (vIdPrograma != "") {
                OpenRadWindow(GetAutorizacionProgramaProperties(vIdPrograma));
            }
            else {
                radalert("Selecciona un programa.", 400, 150);
            }
        }


        function ConfirmarEliminarEmpleado(sender, args) {
            var MasterTable = $find("<%=grdParticipantes.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            var row = selectedRows[0];

            if (row != null) {
                CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_EMPLEADO");
                if (selectedRows != "") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar el empleado ' + CELL_NOMBRE.innerHTML + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                    args.set_cancel(true);
                }
            } else {
                radalert("Seleccione un empleado.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function ConfirmarEliminarPrograma(sender, args) {
            var MasterTable = $find("<%=grdCategorias.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            var row = selectedRows[0];

            if (row != null) {
                CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_EMPLEADO");
                CELL_COMPETENCIA = MasterTable.getCellByColumnUniqueName(row, "NB_COMPETENCIA");
                if (selectedRows != "") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar la competencia(s) seleccionadas ' + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                    args.set_cancel(true);
                }
            } else {
                radalert("Seleccione una competencia.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        $(function () {
            $(".btnAceptarMatriz").click(function () {
                var vChecks = [];

                $(".Datos").each(function () {

                    if (!this.checked) {
                        var vCheck = {
                            control: this.value
                        };

                        vChecks.push(vCheck);
                    }
                })

                $(".NA").each(function () {
                    if (!this.checked) {
                        var vCheck = {
                            control: this.value
                        };

                        vChecks.push(vCheck);
                    }
                })

                $(".NoNecesaria").each(function () {
                    if (!this.checked) {
                        var vCheck = {
                            control: this.value
                        };
                        vChecks.push(vCheck);
                    }
                })

                InsertEvaluado(EncapsularSeleccion(vChecks));
            })
        })


        function InsertEvaluado(pDato) {
            var ajaxManager = $find('<%= ramProgramasCapacitacion.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function EncapsularSeleccion(pLstSeleccion) {
            return JSON.stringify({ clTipoAccion: "MATRIZ", oSeleccion: pLstSeleccion });
        }

    </script>

    <style type="text/css">
        body {
            overflow: hidden;
        }

        .divNecesario {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: red;
        }

        .divIntermedio {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gold;
        }

        .divBajo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: transparent;
        }

        .divNa {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gray;
        }

        .divPorcentaje {
            text-align: right;
            width: 100%;
            float: left;
            padding-right: 2px;
        }

        .divCheckbox {
            float: right;
        }

        table.tabladnc {
            width: 100%;
        }

        td.porcentaje {
            width: 60%;
            padding: 1px;
        }

        td.color {
            width: 15%;
            padding: 1px;
        }

        td.check {
            width: 25%;
            padding: 1px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramProgramasCapacitacion" runat="server" OnAjaxRequest="ramProgramasCapacitacion_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramProgramasCapacitacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdParticipantes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdCompetencias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdParticipantes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdParticipantes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCompetencias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarParticipantes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdParticipantes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCompetencias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCompetencias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCompetencias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCombinaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCategorias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCombinacionesMatriz">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCapacitacionMatriz" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnQuitarSeleccionados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCategorias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 100px);">
        <div style="clear: both; height: 2px;"></div>
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Programa:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtClProgCapacitacion" runat="server" style="min-width: 100px;"></div>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <div id="txtNbProgCapacitacion" runat="server" width="170" maxlength="1000"></div>
                    </td>
                </tr>
            </table>


            <%--  <div class="divControlIzquierda">
                <label id="lblClavePrograma" name="lblClavePrograma" runat="server">Clave:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClProgCapacitacion" runat="server" Width="200px" Enabled="false" MaxLength="1000"></telerik:RadTextBox>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClProgCapacitacion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            </div>--%>
        </div>
        <%--   <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblNombrePrograma" name="lblNombrePrograma" runat="server">Nombre:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNbProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbProgCapacitacion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            </div>
        </div>--%>
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Tipo:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtTipoProgCapacitacion" runat="server" style="min-width: 100px;"></div>
                    </td>
                </tr>
            </table>

            <%--           <div class="divControlIzquierda">
                <label id="Label4" name="lblNombrePrograma" runat="server">Tipo:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtTipoProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
            </div>--%>
        </div>
        <div class="ctrlBasico" style="display: none;">
            <div style="width: 50px; margin-right: 15px; float: left;">
                <label id="lblEstadoPrograma" name="lblEstadoPrograma" runat="server">Estado:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtEstadoProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
            </div>
        </div>
        <div class="ctrlBasico" style="display: none;">
            <div class="ctrlBasico">
                <label id="Label1" name="lblEstadoPrograma" runat="server">Notas:</label>
            </div>

            <div class="ctrlBasico">
                <telerik:RadEditor Height="150px" Width="400px" ToolsWidth="400px" EditModes="Design" ID="radEditorNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/BasicTools.xml"></telerik:RadEditor>
            </div>
        </div>
        <telerik:RadTabStrip ID="tbProgramaCapacitacion" runat="server" SelectedIndex="0" MultiPageID="mpgProgramaCapacitacion">
            <Tabs>
                <%--                <telerik:RadTab Text="Contexto" Visible="true"></telerik:RadTab>--%>
                <telerik:RadTab Text="Competencias y participantes" Visible="true"></telerik:RadTab>
                <telerik:RadTab Text="Programa capacitación" Visible="true"></telerik:RadTab>
                <telerik:RadTab Text="Programa capacitación" Visible="true"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="mpgProgramaCapacitacion" runat="server" SelectedIndex="0" Height="92%">
            <%--            <telerik:RadPageView ID="RPView1" runat="server">
                <div style="height: 10px;"></div>
                <div style="float: left;">
                    <div class="ctrlBasico">
                        <div style="width: 50px; margin-right: 15px; float: left;">
                            <label id="lblClavePrograma" name="lblClavePrograma" runat="server">Clave:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtClProgCapacitacion" runat="server" Width="200px" Enabled="false" MaxLength="1000"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClProgCapacitacion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div style="width: 50px; margin-right: 15px; float: left;">
                            <label id="lblNombrePrograma" name="lblNombrePrograma" runat="server">Nombre:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtNbProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1"  runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbProgCapacitacion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div style="width: 50px; margin-right: 15px; float: left;">
                            <label id="Label4" name="lblNombrePrograma" runat="server">Tipo:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtTipoProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div style="width: 50px; margin-right: 15px; float: left;">
                            <label id="lblEstadoPrograma" name="lblEstadoPrograma" runat="server">Estado:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtEstadoProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
                        </div>
                    </div>
                </div>
                <div style="float: left;">
                    <div class="ctrlBasico">

                        <div class="ctrlBasico">
                            <label id="Label1" name="lblEstadoPrograma" runat="server">Notas:</label>
                        </div>

                        <div class="ctrlBasico">
                            <telerik:RadEditor Height="150px" Width="400px" ToolsWidth="400px" EditModes="Design" ID="radEditorNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/BasicTools.xml"></telerik:RadEditor>
                        </div>
                    </div>
                </div>
            </telerik:RadPageView>--%>
            <telerik:RadPageView ID="rpCompetenciasParticipantes" runat="server" Height="100%">
                <div style="float: left; width: 50%; height: calc(100% - 30px);">
                    <label id="Label2" name="lblCompetencias" runat="server">Competencias:</label>
                    <div style="height: calc(100% - 70px); margin-right: 5px;">
                        <telerik:RadGrid
                            ID="grdCompetencias"
                            ShowHeader="true"
                            runat="server"
                            Width="100%"
                            Height="100%"
                            HeaderStyle-Font-Bold="true"
                            OnNeedDataSource="grdCompetencias_NeedDataSource"
                            AllowMultiRowSelection="true"
                            AutoGenerateColumns="false"
                            AllowSorting="true">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <PagerStyle AlwaysVisible="true" />
                            <MasterTableView ClientDataKeyNames="ID_COMPETENCIA" AllowFilteringByColumn="true" DataKeyNames="ID_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10">
                                <Columns>
                                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Categoría" DataField="NB_CLASIFICACION" UniqueName="NB_CLASIFICACION" HeaderStyle-Width="120" FilterControlWidth="30"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_COMPETENCIA" UniqueName="CL_COMPETENCIA" HeaderStyle-Width="90" FilterControlWidth="10"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Competencia" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" FilterControlWidth="130"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnAgregarCompetencia" AutoPostBack="false" runat="server" Text="Agregar" Width="100" OnClientClicked="OpenSelectionCompetencia" ToolTip="Da clic si deseas incorporar nuevas competencias a este programa de capacitación. "></telerik:RadButton>
                        <telerik:RadButton ID="btnEliminarCompetencia" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminarCompetencia_Click" OnClientClicking="ConfirmarEliminarCompetencia"></telerik:RadButton>
                    </div>
                </div>
                <div style="float: right; width: 50%; height: calc(100% - 30px);">
                    <label id="Label3" name="lblParticipantes" runat="server">Participantes:</label>
                    <div style="height: calc(100% - 70px); margin-left: 5px;">
                        <telerik:RadGrid
                            ID="grdParticipantes"
                            ShowHeader="true"
                            runat="server"
                            Width="100%"
                            Height="100%"
                            HeaderStyle-Font-Bold="true"
                            GroupPanelPosition="Top"
                            OnNeedDataSource="grdParticipantes_NeedDataSource"
                            AllowMultiRowSelection="true">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <PagerStyle AlwaysVisible="true" />
                            <MasterTableView ClientDataKeyNames="ID_EMPLEADO" AllowFilteringByColumn="true" DataKeyNames="ID_EMPLEADO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10">
                                <Columns>
                                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO" HeaderStyle-Width="200" FilterControlWidth="120"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnAgregarParticipantes" AutoPostBack="false" runat="server" Text="Agregar" Width="100" OnClientClicked="OpenEmployeeSelectionWindow"></telerik:RadButton>
                        <telerik:RadButton ID="btnEliminarParticipantes" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminarEmpleado_Click" OnClientClicking="ConfirmarEliminarEmpleado"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lblPrioridad" name="lblPrioridad" runat="server">Prioridad:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadComboBox ID="cmbPrioridades" runat="server" Width="200px" EmptyMessage="Selecciona"></telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarCombinaciones" OnClick="btnAgregarCombinaciones_Click" runat="server" Text="Agregar combinaciones seleccionadas al programa de capacitación" Width="100%"></telerik:RadButton>
                    <telerik:RadButton ID="btnAgregarCombinacionesMatriz" OnClick="btnAgregarCombinaciones_Click" runat="server" Text="Agregar combinaciones seleccionadas al programa de capacitación" Width="100%"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <%--            <telerik:RadPageView ID="rpCompetenciasParticipantes" runat="server" Height="100%">
                <table style="width: 100%; height:100%;">
                    <tbody>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label2" name="lblCompetencias" runat="server">Competencias:</label>

                                    <div style="clear: both;"></div>
                                    <telerik:RadGrid
                                        ID="grdCompetencias"
                                        ShowHeader="true"
                                        runat="server"
                                        Width="100%"
                                        Height="420px"
                                        OnNeedDataSource="grdCompetencias_NeedDataSource"
                                        AllowMultiRowSelection="true"
                                        AutoGenerateColumns="false"
                                        AllowSorting="true">
                                        <ClientSettings>
                                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView ClientDataKeyNames="ID_COMPETENCIA" AllowFilteringByColumn="true" DataKeyNames="ID_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20">
                                            <Columns>
                                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Categoría" DataField="NB_CLASIFICACION" UniqueName="NB_CLASIFICACION" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_COMPETENCIA" UniqueName="CL_COMPETENCIA" HeaderStyle-Width="90" FilterControlWidth="30"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Competencia" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" HeaderStyle-Width="150" FilterControlWidth="130"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnAgregarCompetencia" AutoPostBack="false" runat="server" Text="Agregar" Width="100" OnClientClicked="OpenSelectionCompetencia" ToolTip="Da clic si deseas incorporar nuevas competencias a este programa de capacitación. "></telerik:RadButton>
                                    <telerik:RadButton ID="btnEliminarCompetencia" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminarCompetencia_Click" OnClientClicking="ConfirmarEliminarCompetencia"></telerik:RadButton>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlBasico">
                                    <div style="clear: both; height: 20px;">
                                        <label id="Label3" name="lblParticipantes" runat="server">Participantes:</label>
                                    </div>
                                    <div style="clear: both;"></div>
                                 
                                    <telerik:RadGrid
                                        ID="grdParticipantes"
                                        ShowHeader="true"
                                        runat="server"
                                        Width="100%"
                                        Height="420px"
                                        GroupPanelPosition="Top"
                                        OnNeedDataSource="grdParticipantes_NeedDataSource"
                                        AllowMultiRowSelection="true">
                                        <ClientSettings>
                                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>

                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView ClientDataKeyNames="ID_EMPLEADO" AllowFilteringByColumn="true" DataKeyNames="ID_EMPLEADO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20">
                                            <Columns>
                                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO" HeaderStyle-Width="200" FilterControlWidth="120"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                   
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnAgregarParticipantes" AutoPostBack="false" runat="server" Text="Agregar" Width="100" OnClientClicked="OpenEmployeeSelectionWindow"></telerik:RadButton>
                                    <telerik:RadButton ID="btnEliminarParticipantes" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminarEmpleado_Click" OnClientClicking="ConfirmarEliminarEmpleado"></telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnAgregarCombinaciones" OnClick="btnAgregarCombinaciones_Click" runat="server" Text="Agregar combinaciones seleccionadas al programa de capacitación" Width="100%"></telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <div style="float: left; width: 120px;">
                                        <label id="lblPrioridad" name="lblPrioridad" runat="server">Prioridad:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadComboBox ID="cmbPrioridades" runat="server" Width="200px" EmptyMessage="Selecciona"></telerik:RadComboBox>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </telerik:RadPageView>--%>
            <telerik:RadPageView ID="RPView2" runat="server">
                <div style="clear: both; height: 5px;"></div>
                <div style="height: calc(100% - 50px);">
                    <telerik:RadGrid
                        ID="grdCategorias"
                        ShowHeader="true"
                        runat="server"
                        Height="100%"
                        HeaderStyle-Font-Bold="true"
                        GroupPanelPosition="Top"
                        AllowMultiRowSelection="true" OnNeedDataSource="grdCategorias_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_PROGRAMA_EMPLEADO_COMPETENCIA" AllowFilteringByColumn="true" DataKeyNames="ID_PROGRAMA_EMPLEADO_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Categoría" DataField="CL_TIPO_COMPETENCIA" UniqueName="CL_TIPO_COMPETENCIA" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clasificación" DataField="NB_CLASIFICACION_COMPETENCIA" UniqueName="NB_CLASIFICACION_COMPETENCIA" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Competencia" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="C. Empleado" DataField="CL_EVALUADO" UniqueName="CL_EVALUADO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_EVALUADO" UniqueName="NB_EVALUADO" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="C. Puesto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Resultado" DataField="PR_RESULTADO" UniqueName="PR_RESULTADO" HeaderStyle-Width="100" FilterControlWidth="30" DataFormatString="{0}%">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Prioridad" DataField="CL_PRIORIDAD" UniqueName="CL_PRIORIDAD" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnQuitarSeleccionados" OnClick="btnQuitarSeleccionados_Click" OnClientClicking="ConfirmarEliminarPrograma" runat="server" Text="Quitar los seleccionados del Programa de capacitación" Width="100%"></telerik:RadButton>
                        <telerik:RadButton ID="btnAutoriza" OnClientClicked="OpenAutorizacionProgramaWindow" AutoPostBack="false" runat="server" Text="Registro y autorización" Width="200" ToolTip="Da clic si deseas registrar este programa de capacitación y/o deseas realizar un proceso de autorización."></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvMatriz" runat="server">
                <div style="height: calc(100% - 50px);">
                    <telerik:RadGrid ID="grdCapacitacionMatriz" runat="server" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="true"
                        OnNeedDataSource="grdCapacitacionMatriz_NeedDataSource" OnColumnCreated="grdCapacitacionMatriz_ColumnCreated" AllowMultiRowSelection="true" AllowPaging="false">
                        <ClientSettings EnablePostBackOnRowClick="false" Scrolling-FrozenColumnsCount="4">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="ID_EMPLEADO" DataKeyNames="ID_EMPLEADO" EnableColumnsViewState="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns></Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnRegistroAutorizacion" OnClientClicked="OpenAutorizacionProgramaWindow" AutoPostBack="false" runat="server" Text="Registro y autorización" Width="200" ToolTip="Da clic si deseas registrar este programa de capacitación y/o deseas realizar un proceso de autorización."></telerik:RadButton>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnAceptar" OnClick="btnAceptar_Click" runat="server" Text="Guardar" ToolTip="Aceptar" CssClass="ctrlBasico"></telerik:RadButton>
        <telerik:RadButton ID="btnAceptarMatriz" runat="server" Text="Guardar" ToolTip="Aceptar" CssClass="btnAceptarMatriz" AutoPostBack="false"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
