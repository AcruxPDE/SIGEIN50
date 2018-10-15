<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="NecesidadesCapacitacion2.aspx.cs" Inherits="SIGE.WebApp.FYD.NecesidadesCapacitacion2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script src="../Assets/js/jquery.min.js"></script>
    <script type="text/javascript">

        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx", "winSeleccion", "Selección de área/departamento")
        }

        function OpenProgramaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionProgramaCapacitacion.aspx", "winSeleccion", "Selección de programa")
        }

        function closeWindow() {
            GetRadWindow().close();
        }

        function GetRadWindow() {
            var oWindow = null;
            if
                (window.radWindow) oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
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
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties);
        }

        function useDataFromChild(pDato) {
            var vLstDato = {
                idItem: "",
                nbItem: ""
            };

            if (pDato != null) {
                console.info(pDato);
                var vDatosSeleccionados = pDato[0];

                if (vDatosSeleccionados.clTipoCatalogo == "PROGRAMA") {
                    vLstDato.idItem = vDatosSeleccionados.idPrograma;
                    vLstDato.nbItem = vDatosSeleccionados.nbPrograma;
                    //list = $find('<=lstProgramas.ClientID %>');
                    // ChangeListItem(vLstDato.idItem, vLstDato.nbItem, list);

                }
                else {
                    vLstDato.idItem = vDatosSeleccionados.idArea;
                    vLstDato.nbItem = vDatosSeleccionados.nbArea;
                    //  list = $find('<=lstDepartamento.ClientID %>');
                    ChangeListItem(vLstDato.idItem, vLstDato.nbItem, list);
                }

            }
        }

        function ChangeListItem(pIdItem, pNbItem, pList) {
            var vListBox = pList;
            vListBox.trackChanges();

            var items = vListBox.get_items();
            items.clear();

            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(pNbItem);
            item.set_value(pIdItem);
            items.add(item);
            item.set_selected(true);
            vListBox.commitChanges();
        }

        function CleanAreasSelection(sender, args) {
            //var list = $find('<=lstDepartamento.ClientID %>');
            ChangeListItem("", "Todos", list);
        }

        function ValidarNuevoProgramaCapacitacion(sender, args) {

            //var txtProgClave = $find('<=txtClavePrograma.ClientID %>').get_value();
            //var txtProgNombre = $find('<= txtNombrePrograma.ClientID %>').get_value();

            if (txtProgClave != "" & txtProgNombre != "") {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });
            }
            else {
                if (txtProgClave == "") {
                    radalert("Indique la clave del programa.", 400, 150, "Error");
                    args.set_cancel(true);
                    return;
                }

                if (txtProgNombre == "") {
                    radalert("Indique el nombre del programa.", 400, 150, "Error");
                    args.set_cancel(true);
                    return;
                }
            }
        }

        function ValidarActualizacionProgramaCapacitacion(sender, args) {

            //    var vListBox = $find('<=lstDepartamento.ClientID %>');
            var items = vListBox.get_items();
            var item = items[0];
            var vIdPrograma = item.get_value();


            if (vIdPrograma != "0") {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });
            }
            else {
                radalert("Seleccione un programa existente", 400, 150, "Error");
                args.set_cancel(true);
                return;
            }
        }

        function SeleccionaCompetencia(pChkCompetencia) {
            //alert(pChkCompetencia.value);

            var vIdCompetencia = pChkCompetencia.value;
            var vIdEmpleado;
            var vIdCheckBox = "";
            var vIdCheckBoxJQ = "";

            var grid = $find('<%= grdCapacitacion.ClientID %>');
            var masterTableView = grid.get_masterTableView();
            var vFgIncluirNa = $find('<%= chkIncluirNa.ClientID %>').get_checked();
            var vFgIncluirNoNecesarias = $find('<%= chkIncluirNoNecesarias.ClientID %>').get_checked();
            var columns = masterTableView.get_columns();

            // alerts "ID" and "Name"
            for (var i = 3; i < columns.length; i++) {
                //alert(columns[i].get_uniqueName());

                var uniqueName = columns[i].get_uniqueName();
                var vIdEmpleadoRemplace = uniqueName.replace("E", "");
                vIdEmpleado = vIdEmpleadoRemplace;
                vIdCheckBox = "C" + vIdCompetencia + "E" + vIdEmpleado;
                vIdCheckBoxJQ = "#C" + vIdCompetencia + "E" + vIdEmpleado;

                var chk = $(vIdCheckBoxJQ); //document.getElementsByName(vIdCheckBox)[0];

                if (chk != undefined) {
                    //chk.checked = pChkCompetencia.checked;

                    if (pChkCompetencia.checked) {
                        //chk.prop('checked', true);
                        if (chk.attr('class') == 'Datos') {
                            chk.prop('checked', true);
                        }

                        if (vFgIncluirNa & chk.attr('class') == 'NA') {
                            chk.prop('checked', true);
                        }

                        if (vFgIncluirNoNecesarias & chk.attr('class') == 'NoNecesaria') {
                            chk.prop('checked', true);
                        }
                    }
                    else {
                        //chk.prop('checked', false);
                        if (chk.attr('class') == 'Datos') {
                            chk.prop('checked', false);
                        }

                        if (vFgIncluirNa & chk.attr('class') == 'NA') {
                            chk.prop('checked', false);
                        }

                        if (vFgIncluirNoNecesarias & chk.attr('class') == 'NoNecesaria') {
                            chk.prop('checked', false);
                        }
                    }
                }
            }
        }

        function SeleccionaEmpleado(pCheckEmpleado) {

            var vIdCompetencia;
            var vIdEmpleado = pCheckEmpleado.value;
            var vIdCheckBox = "";
            var vIdCheckBoxJQ = "";

            var masterTable = $find('<%= grdCapacitacion.ClientID %>').get_masterTableView();
            var vFgIncluirNa = $find('<%= chkIncluirNa.ClientID %>').get_checked();
            var vFgIncluirNoNecesarias = $find('<%= chkIncluirNoNecesarias.ClientID %>').get_checked();
            var vLstItems = masterTable.get_dataItems();

            for (i = 0; i < vLstItems.length; i++) {
                var item = vLstItems[i];

                vIdCompetencia = item.getDataKeyValue("ID_COMPETENCIA");
                vIdCheckBox = "C" + vIdCompetencia + "E" + vIdEmpleado;
                vIdCheckBoxJQ = "#C" + vIdCompetencia + "E" + vIdEmpleado;

                var chk = $(vIdCheckBoxJQ); //document.getElementsByName(vIdCheckBox)[0];

                if (chk != undefined) {

                    if (pCheckEmpleado.checked) {

                        if (chk.attr('class') == 'Datos') {
                            chk.prop('checked', true);
                        }

                        if (vFgIncluirNa & chk.attr('class') == 'NA') {
                            chk.prop('checked', true);
                        }

                        if (vFgIncluirNoNecesarias & chk.attr('class') == 'NoNecesaria') {
                            chk.prop('checked', true);
                        }

                    }
                    else {
                        if (chk.attr('class') == 'Datos') {
                            chk.prop('checked', false);
                        }

                        if (vFgIncluirNa & chk.attr('class') == 'NA') {
                            chk.prop('checked', false);
                        }

                        if (vFgIncluirNoNecesarias & chk.attr('class') == 'NoNecesaria') {
                            chk.prop('checked', false);
                        }
                    }
                }
            }
        }


        function SeleccionaTodos() {
            var vFgIncluirNa = $find('<%= chkIncluirNa.ClientID %>').get_checked();
            var vFgIncluirNoNecesarias = $find('<%= chkIncluirNoNecesarias.ClientID %>').get_checked();

            $(".Datos").prop('checked', true);

            if (vFgIncluirNa) {
                $(".NA").prop('checked', true);
            }

            if (vFgIncluirNoNecesarias) {
                $(".NoNecesaria").prop('checked', true);
            }
        }

        function DeseleccionaTodos() {
            var vFgIncluirNa = $find('<%= chkIncluirNa.ClientID %>').get_checked();
            var vFgIncluirNoNecesarias = $find('<%= chkIncluirNoNecesarias.ClientID %>').get_checked();

            $(".Datos").prop('checked', false);

            if (vFgIncluirNa) {
                $(".NA").prop('checked', false);
            }

            if (vFgIncluirNoNecesarias) {
                $(".NoNecesaria").prop('checked', false);
            }
        }

        // funcion para agregar las competencias a un programa de capacitación
        $(function () {
            $(".btnGuardarPrograma").click(function () {

                var vChecks = [];
                var vFgIncluirNa = $find('<%= chkIncluirNa.ClientID %>').get_checked();
                var vFgIncluirNoNecesarias = $find('<%= chkIncluirNoNecesarias.ClientID %>').get_checked();
                //var txtProgClave = $find('<=txtClavePrograma.ClientID %>').get_value();
                //var txtProgNombre = $find('<= txtNombrePrograma.ClientID %>').get_value();

                if (txtProgClave != "" & txtProgNombre != "") {

                    $(".Datos").each(function () {

                        if (this.checked) {
                            var vCheck = {
                                control: this.value
                            };

                            vChecks.push(vCheck);
                        }
                    })

                    if (vFgIncluirNa) {
                        $(".NA").each(function () {

                            if (this.checked) {
                                var vCheck = {
                                    control: this.value
                                };

                                vChecks.push(vCheck);
                            }
                        })
                    }

                    if (vFgIncluirNoNecesarias) {
                        $(".NoNecesaria").each(function () {

                            if (this.checked) {
                                var vCheck = {
                                    control: this.value
                                };

                                vChecks.push(vCheck);
                            }
                        })
                    }


                    InsertEvaluado(EncapsularSeleccion("INSERTAR", 0, txtProgClave, txtProgNombre, vChecks));

                }
                else {
                    if (txtProgClave == "") {
                        radalert("Indique la clave del programa.", 400, 150, "Error");
                        return;
                    }

                    if (txtProgNombre == "") {
                        radalert("Indique el nombre del programa.", 400, 150, "Error");
                        return;
                    }
                }
            })
        })


        $(function () {
            $(".btnAgregarPrograma").click(function () {

                var vChecks = [];
                var vFgIncluirNa = $find('<%= chkIncluirNa.ClientID %>').get_checked();
                var vFgIncluirNoNecesarias = $find('<%= chkIncluirNoNecesarias.ClientID %>').get_checked();
                //var vListBox = $find('<lstProgramas.ClientID %>');

                var items = vListBox.get_items();
                var item = vListBox.getItem(0);
                var vIdPrograma = item.get_value();

                if (vIdPrograma != "0") {

                    $(".Datos").each(function () {

                        if (this.checked) {
                            var vCheck = {
                                control: this.value
                            };
                            vChecks.push(vCheck);
                        }
                    })

                    if (vFgIncluirNa) {
                        $(".NA").each(function () {

                            if (this.checked) {
                                var vCheck = {
                                    control: this.value
                                };

                                vChecks.push(vCheck);
                            }
                        })
                    }

                    if (vFgIncluirNoNecesarias) {
                        $(".NoNecesaria").each(function () {

                            if (this.checked) {
                                var vCheck = {
                                    control: this.value
                                };

                                vChecks.push(vCheck);
                            }
                        })
                    }

                    InsertEvaluado(EncapsularSeleccion("ACTUALIZAR", vIdPrograma, "", "", vChecks));
                }
                else {
                    radalert("Seleccione un programa existente", 400, 150, "Error");
                    return;
                }
            })
        })


        //funcion general para todos los modulos, se tomara como base estas funciones de abajo


        function EncapsularSeleccion(pClTipoAccion, pIdPrograma, pClPrograma, pNbPrograma, pLstSeleccion) {
            //{ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion }
            return JSON.stringify({ clTipoAccion: pClTipoAccion, idPrograma: pIdPrograma, clPrograma: pClPrograma, nbPrograma: pNbPrograma, oSeleccion: pLstSeleccion });
        }

        function InsertEvaluado(pDato) {
            var ajaxManager = $find('<%= ramNecesidades.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

    </script>

    <style>
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
            width: 80%;
            padding: 1px;
        }

        td.color {
            width: 10%;
            padding: 1px;
        }

        td.check {
            width: 10%;
            padding: 1px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpNecesidades" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramNecesidades" runat="server" DefaultLoadingPanelID="ralpNecesidades" OnAjaxRequest="ramNecesidades_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnConsultar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdCapacitacion"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--            <telerik:AjaxSetting AjaxControlID="btnGuardarPrograma">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdCapacitacion" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnAgregarPrograma">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdCapacitacion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
        <telerik:RadTabStrip runat="server" ID="rtsNecesidades" SelectedIndex="0" Width="100%" MultiPageID="rmpNecesidades">
        <Tabs>
            <telerik:RadTab Text="Contexto"></telerik:RadTab>
            <telerik:RadTab Text="Necesidades de capacitación"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
        <div style="height: calc(100% - 60px); width: 100%;">
        <telerik:RadMultiPage runat="server" ID="rmpNecesidades" SelectedIndex="0" Width="100%" Height="100%">
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


            <telerik:RadPageView runat="server" ID="rpvNecesidades">

    <telerik:RadSplitter ID="rsPlantilla" Width="100%" Height="100%" BorderSize="0" runat="server">
        <telerik:RadPane ID="rpGridEvaluados" runat="server" Height="100%" ShowContentDuringLoad="false">
            <div style="clear: both; height: 10px;"></div>

           <%-- <div class="ctrlBasico">
                <label style="margin-left: 10px;">Periodo:</label>
            </div>

            <div class="ctrlBasico">
                <telerik:RadTextBox runat="server" Width="80px" ID="txtPeriodo" Text="10" EnabledStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:RadTextBox>
            </div>--%>
            <%-- <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtPeriodo" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNombrePeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
         </div>--%>

            <div class="ctrlBasico">
                <%--<telerik:RadTextBox runat="server" Width="300px" ID="txtNombrePeriodo" ReadOnly="true"></telerik:RadTextBox>--%>
                <telerik:RadButton runat="server" ID="btnbuscaPeriodo" Text="B" Width="35px" AutoPostBack="false" Visible="false"></telerik:RadButton>
                <telerik:RadButton runat="server" ID="btnEliminaPeriodo" Text="X" Width="35px" AutoPostBack="false" Visible="false"></telerik:RadButton>
            </div>

         <%--   <div class="ctrlBasico">
                <label>Departamento:</label>
            </div>

            <div class="ctrlBasico">
                <telerik:RadListBox ID="lstDepartamento" runat="server" Width="350px">
                    <Items>
                        <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                    </Items>
                </telerik:RadListBox>
                <telerik:RadButton runat="server" ID="btnBuscarDepartamento" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenAreaSelectionWindow" />
                <telerik:RadButton runat="server" ID="btnCleanDepartamento" Text="X" Width="35px" AutoPostBack="false" OnClientClicked="CleanAreasSelection" />
            </div>--%>

            <div style="clear: both; height: 5px;"></div>

            <div class="ctrlBasico">
                <label>Prioridad de capacitación:</label>
            </div>

            <div class="ctrlBasico">
                <%--<telerik:RadCheckBox runat="server" ID="chkAlta" Text="Alta" Checked="true" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <telerik:RadButton runat="server" ID="chkAlta" RenderMode="Lightweight" Text="Alta" EnableViewState="true" ButtonType="ToggleButton" ToggleType="CheckBox" OnCheckedChanged="chkAlta_CheckedChanged" AutoPostBack="false" Checked="true"></telerik:RadButton>
            </div>

            <div class="ctrlBasico">
                <%--<telerik:RadCheckBox runat="server" ID="chkIntermedia" Text="Intermedia" Checked="true" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <telerik:RadButton runat="server" ID="chkIntermedia" RenderMode="Lightweight" EnableViewState="true" Text="Intermedia" ButtonType="ToggleButton" ToggleType="CheckBox" AutoPostBack="false" OnCheckedChanged="chkIntermedia_CheckedChanged" Checked="true"></telerik:RadButton>
            </div>

            <div class="ctrlBasico">
                <%--<telerik:RadCheckBox runat="server" ID="chkNoNecesaria" Text="No necesaria" Checked="true" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <telerik:RadButton runat="server" ID="chkNoNecesaria" RenderMode="Lightweight" EnableViewState="true" Text="No necesaria" ButtonType="ToggleButton" ToggleType="CheckBox" AutoPostBack="false" OnCheckedChanged="chkNoNecesaria_CheckedChanged" Checked="false"></telerik:RadButton>
            </div>

            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnConsultar" Text="Consultar" AutoPostBack="true" OnClick="btnConsultar_Click"></telerik:RadButton>
            </div>

            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="chkIncluirNa" RenderMode="Lightweight" Text="Incluir N/A" ButtonType="ToggleButton" ToggleType="CheckBox" AutoPostBack="false" Checked="false" Visible="false"></telerik:RadButton>
            </div>

            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="chkIncluirNoNecesarias" RenderMode="Lightweight" Text="Incluir No necesarias" ButtonType="ToggleButton" ToggleType="CheckBox" AutoPostBack="false" Checked="false" Visible="false"></telerik:RadButton>
            </div>

            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnSeleccionarTodos" Text="Seleccionar Todos" AutoPostBack="false" OnClientClicked="SeleccionaTodos" Visible="false"></telerik:RadButton>
            </div>
            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnQuitarSeleccion" Text="Deseleccionar Todos" AutoPostBack="false" OnClientClicked="DeseleccionaTodos" Visible="false"></telerik:RadButton>
            </div>
            <div style="clear: both;"></div>
            <div style="height: calc(100% - 130px); width: 100%;">
                <telerik:RadGrid ID="grdCapacitacion" runat="server" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="true" OnItemCreated="grdCapacitacion_ItemCreated"
                    OnNeedDataSource="grdCapacitacion_NeedDataSource" OnColumnCreated="grdCapacitacion_ColumnCreated" AllowMultiRowSelection="true" AllowPaging="false">
                    <ClientSettings EnablePostBackOnRowClick="false" Scrolling-FrozenColumnsCount="3">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView ClientDataKeyNames="ID_COMPETENCIA"  DataKeyNames="ID_COMPETENCIA" EnableColumnsViewState="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" >
                        <Columns></Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div style="clear: both; height: 5px;"></div>
            <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnFormatoExcel" Text="Formato Excel" OnClick="btnFormatoExcel_Click"></telerik:RadButton>
            </div>
        </telerik:RadPane>
          <telerik:RadPane ID="RadPane2" runat="server" Width="20px" Height="100%">
                            <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas" ClickToOpen="true">
                        <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="300px" Title="Código de color" Height="100%">
                                    <div style="padding: 10px; text-align: justify;">
                                        <telerik:RadGrid ID="grdCodigoColores"
                                            runat="server"
                                            Height="300"
                                            Width="250"
                                            AllowSorting="true"
                                            AllowFilteringByColumn="true"
                                            HeaderStyle-Font-Bold="true"
                                            ShowHeader="true"
                                            OnNeedDataSource="grdCodigoColores_NeedDataSource">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="true" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                                    AddNewRecordText="Insertar" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Color" HeaderStyle-Width="60" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <div style="margin: auto; width: 25px; border: 1px solid gray; background: <%# Eval("COLOR")%>; border-radius: 5px;">&nbsp;&nbsp;</div>
                                                            &nbsp;
                                                        </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </telerik:RadSlidingPane>
                                </telerik:RadSlidingZone>
                              </telerik:RadPane>

       <%-- <telerik:RadPane ID="rpAyuda" runat="server" Width="22px" ShowContentDuringLoad="false">
            <telerik:RadSlidingZone ID="rszPrograma" runat="server" SlideDirection="Left" Width="22px">
                <telerik:RadSlidingPane ID="rspNuevoPrograma" runat="server" Title="Generar programa" Width="340px" RenderMode="Mobile" Height="200">
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <label>Clave:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadTextBox runat="server" ID="txtClavePrograma" Width="150px" AutoPostBack="false"></telerik:RadTextBox>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="ctrlBasico">
                        <label>Nombre:</label>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadTextBox runat="server" ID="txtNombrePrograma" Width="300px" AutoPostBack="false"></telerik:RadTextBox>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnGuardarPrograma" Text="Guardar" AutoPostBack="false" CssClass="btnGuardarPrograma" ToolTip="Esta opción te permite crear un programa de capacitación a partir de los resultados obtenidos en un período de Detección de Necesidades de Capacitación (DNC). Selecciona el periodo deseado:"></telerik:RadButton>--%>
                        <%--<telerik:RadButton runat="server" ID="btnGuardarPrograma" Text="Guardar" AutoPostBack="false" class="btnGuardarPrograma" OnClientClicking="ValidarNuevoProgramaCapacitacion" OnClick="btnGuardarPrograma_Click" ToolTip="Esta opción te permite crear un programa de capacitación a partir de los resultados obtenidos en un periodo de Detección de Necesidades de Capacitación (DNC). Selecciona el periodo deseado:"></telerik:RadButton> --%>
                  <%--  </div>
                </telerik:RadSlidingPane>
                <telerik:RadSlidingPane ID="rspActualizaPrograma" runat="server" Title="Actualizar programa" Width="315">
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadListBox ID="lstProgramas" runat="server" Width="250px">
                            <Items>
                                <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                            </Items>
                        </telerik:RadListBox>
                        <telerik:RadButton runat="server" ID="btnBuscarProgramas" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenProgramaSelectionWindow" />
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnAgregarPrograma" CssClass="btnAgregarPrograma" Text="Agregar" AutoPostBack="false"></telerik:RadButton>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>--%>
    </telerik:RadSplitter>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
            </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
