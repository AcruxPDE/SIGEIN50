<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaAvanceProgramaCapacitacion.aspx.cs" Inherits="SIGE.WebApp.FYD.AvanceProgramaCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style type="text/css">
        .Color0 {
            background: red;
            width: 100%;
        }

        .Color10 {
            background: rgb(157, 223, 99);
            width: 100%;
            height: 70%;
        }

        .Color11 {
            background: rgb(157, 223, 99);
            width: 100%;
            height: 100%;
        }

        .Color12 {
            background: rgb(157, 223, 99);
            width: 100%;
            height: 100%;
        }

        .Color2 {
            background: rgb(169, 208, 245);
            width: 100%;
        }

        .Color3 {
            background: rgb(247, 211, 88);
            width: 100%;
        }

        .Color4 {
            background: Yellow;
            width: 100%;
        }

        .Color5 {
            background: rgb(230, 230, 230);
            width: 100%;
        }

        .CuadroColor11 {
            background: red;
            width: 15px;
            height: 15px;
            float: right;
        }

        .CuadroColor12 {
            background: gold;
            width: 10px;
            height: 15px;
            float: right;
        }

        .triangulo11 {
            float: right;
            width: 0;
            height: 0;
            border-right: 10px solid red;
            border-top: 10px solid red;
            border-left: 10px solid transparent;
            border-bottom: 10px solid transparent;
        }

        .triangulo12 {
            float: right;
            width: 0;
            height: 0;
            border-right: 10px solid yellow;
            border-top: 10px solid yellow;
            border-left: 10px solid transparent;
            border-bottom: 10px solid transparent;
        }
    </style>
    <script>

        function OpenDescriptivo(pIdPuesto) {
            var vURL = "../Administracion/VentanaVistaDescriptivo.aspx";
            var vTitulo = "Vista previa descripción del puesto";

            if (pIdPuesto != null)
                vURL = vURL + "?PuestoId=" + pIdPuesto;

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "winSeleccion", vTitulo, windowProperties);
        }

        function OpenSelector() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;

            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };

            openChildDialog("../Comunes/SeleccionEmpleado.aspx?IdPrograma=" + '<%= pID_PROGRAMA %>' + "&vClTipoSeleccion=FYD_PROGRAMA", "winSeleccion", "Selección de evaluados", windowProperties);
        }

        function OpenInventario(pIdEmpleado) {
            var vURL = "../Administracion/Empleado.aspx";
            var vTitulo = "Ver Empleado";

            if (pIdEmpleado != null)
                vURL = vURL + "?EmpleadoId=" + pIdEmpleado + "&pFgHabilitaBotones=False";

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "winSeleccion", vTitulo, windowProperties);
        }

        function OpenEvento(pIdEvento) {
            var vURL = "VentanaEventoCapacitacion.aspx";
            var vTitulo = "Ver evento";

            if (pIdEvento != null)
                vURL += String.format("?EventoId={0}&clOrigen=AVANCE", pIdEvento);

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "winSeleccion", vTitulo, windowProperties);
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vDatosSeleccionados = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "EMPLEADO":
                        InsertEmpleado(EncapsularSeleccion("EMPLEADO", pDato));
                        break;
                }
            }
        }


        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertEmpleado(pDato) {
            var ajaxManager = $find('<%= ramAvanceProgramaCapacitacion.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }


        function onRequestStart(sender, args) {
            if ($find("<%=btnExportar.ClientID %>")._uniqueID == sender.__EVENTTARGET)
                args.set_enableAjax(false);
        }


        function onResponseEnd(sender, args) {
            if ($find("<%=btnExportar.ClientID %>")._uniqueID == sender.__EVENTTARGET)
                args.set_enableAjax(true);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramAvanceProgramaCapacitacion" runat="server" OnAjaxRequest="ramAvanceProgramaCapacitacion_AjaxRequest">
        <ClientEvents OnRequestStart="onRequestStart" OnResponseEnd="onResponseEnd" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="mpgProgramaCapacitacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mpgProgramaCapacitacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
            <telerik:RadTabStrip ID="tbAvanceProgramaCapacitacion" runat="server" AutoPostBack="true" SelectedIndex="0" MultiPageID="mpgProgramaCapacitacion">
                <Tabs>
                    <telerik:RadTab Text="Contexto" runat="server"></telerik:RadTab>
                    <telerik:RadTab Text="Definición de criterios" runat="server"></telerik:RadTab>
                    <telerik:RadTab Text="Avance" runat="server"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <div style="height: calc(100% - 45px);">
                <telerik:RadMultiPage ID="mpgProgramaCapacitacion" runat="server" SelectedIndex="0" Height="100%" AutoPostBack="false">
                    <telerik:RadPageView ID="rpContexto" runat="server" Height="100%">
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico">
                            <table class="ctrlTableForm">
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Programa:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtPeriodo" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="ctrlTableDataContext">Descripción:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtDesPeriodo" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label id="Label1" name="lblTipoEvaluacion" runat="server">Tipo:</label>
                                    </td>
                                    <td class="ctrlTableDataBorderContext">
                                        <div id="txtTipoEvaluacion" runat="server" width="170" maxlength="1000" enabled="false"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label id="lblNotas" name="lblNotas" runat="server">Notas:</label>
                                    </td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="radEditorNotas" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                      <%--  <div class="ctrlBasico" style="width: 30%">
                            <fieldset>
                                <legend>
                                    <label>Estatus de la competencia</label>
                                </legend>

                                <table class="ctrlTableForm">
                                    <tr>
                                        <td>
                                            <div style="background: #F7D358; width: 50px;">
                                                <br />
                                            </div>
                                        </td>
                                        <td>
                                            <label>Programada</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="background: Yellow; width: 50px;">
                                                <br />
                                            </div>
                                        </td>
                                        <td>
                                            <label>No programada</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="background: Gray; width: 50px;">
                                                <br />
                                            </div>
                                        </td>
                                        <td>
                                            <label>No aplica</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="background: #A9D0F5; width: 50px;">
                                                <br />
                                            </div>
                                        </td>
                                        <td>
                                            <label>En curso</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="background: #9DDF63; width: 50px;">
                                                <br />
                                            </div>
                                        </td>
                                        <td>
                                            <label>Recibida</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="background: Red; width: 50px;">
                                                <br />
                                            </div>
                                        </td>
                                        <td>
                                            <label>No asistió</label></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>--%>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvFiltros" runat="server" Height="100%">
                        <div style="height: calc(100% - 60px); overflow: auto;">
                            <telerik:RadGrid ID="rgPrograma"
                                runat="server"
                                AllowSorting="true"
                                Height="100%"
                                AutoGenerateColumns="false"
                                HeaderStyle-Font-Bold="true"
                                OnItemCommand="rgPrograma_ItemCommand"
                                OnNeedDataSource="rgPrograma_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_EMPLEADO">
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="300" FilterControlWidth="250" HeaderText="Nombre completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="250" FilterControlWidth="200" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                            <ItemStyle Width="35" />
                                            <HeaderStyle Width="35" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="height: 10px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnEmpleadoFiltro" runat="server" OnClientClicked="OpenSelector" AutoPostBack="false" Text="Seleccionar mediante filtros"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvVista2" runat="server" Height="100%">
                        <div style="height: calc(100% - 50px);">
                              <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="radPanelProgramaCapacitacion" runat="server" Height="100%">

                            <telerik:RadGrid ID="rgAvancePrograma"
                                runat="server"
                                AllowSorting="false"
                                Height="100%"
                                HeaderStyle-Font-Size="Small"
                                AutoGenerateColumns="true"
                                HeaderStyle-Font-Bold="true"
                                OnNeedDataSource="rgAvancePrograma_NeedDataSource"
                                OnColumnCreated="rgAvancePrograma_ColumnCreated">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_EMPLEADO">
                                    <Columns>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>



              </telerik:RadPane>
        <telerik:RadPane ID="rpnOpciones" runat="server" Height="50px" Width="22px" Scrolling="None">
            <telerik:RadSlidingZone ID="slzOpciones" runat="server" SlideDirection="Left" ExpandedPaneId="AyudaPrograma" ClickToOpen="true" Width="30px">
                <telerik:RadSlidingPane ID="RSPHelp" runat="server" RenderMode="Mobile" Title="Ayuda" Width="250px" Height="100%">
                    <p style="margin: 10px; text-align: justify;">
                        Esta consulta presenta el avance que tiene el Programa de capacitación seleccionado.
                        <br />
                        <br />
                        También podrás observar
                                en los recuadros en verde la calificación obtenida por cada participante o si no tiene ninguna calificación. Cuando en estos recuadros aparezca un triángulo amarillo será porque
                                el participante tuvo una asistencia menor al 80% y si el triángulo aparece en rojo entonces su participación fué menor al 60%.
                    </p>
                </telerik:RadSlidingPane>
                                       <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="300px" Title="Código de color" Height="100%">
                                    <div style="padding: 10px; text-align: justify;">
                                        <telerik:RadGrid ID="grdCodigoColores"
                                            runat="server"
                                            Height="400"
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
    </telerik:RadSplitter>
                        </div>
                        <div style="height: 5px; clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnExportar" runat="server" Text="Exportar a excel" AutoPostBack="true" OnClick="btnExportar_Click"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
      
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
</asp:Content>
