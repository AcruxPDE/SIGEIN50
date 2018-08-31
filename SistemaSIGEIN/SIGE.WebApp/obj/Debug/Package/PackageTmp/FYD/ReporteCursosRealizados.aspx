<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="ReporteCursosRealizados.aspx.cs" Inherits="SIGE.WebApp.FYD.ReporteCursosRealizados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../Assets/js/jquery.min.js"></script>
    <script type="text/javascript">

        var vIdPuestoPlantilla = "";

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 30,
                height: document.documentElement.clientHeight - 20
            };
        }

        function OpenPuestoSelectionReporteMaximoMinimoWindow() {
            OpenSelectionWindow("/Comunes/SeleccionPuesto.aspx?m=FORMACION&CatalogoCl=PLANTILLA&vClTipoSeleccion=PUESTO_OBJETIVO", "winSeleccion", "Selección de puestos");
        }

        function OpenPuestoSelectionReportePlantillaWindow() {
            OpenSelectionWindow("/Comunes/SeleccionPuesto.aspx?m=FORMACION&CatalogoCl=PLANTILLA&vClTipoSeleccion=PUESTO_OBJETIVO", "winSeleccion", "Selección de puestos");
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = GetWindowProperties();

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

     

        function GetCursosRealizadosWindowProperties(pIdReporte) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Reporte cursos realizados";
            wnd.vRadWindowId = "winReporte";
            wnd.vURL = "VentanaReporteCursosRealizados.aspx?IdReporte=" + pIdReporte;

            return wnd;
        }

        function GetListaMaterialesWindowProperties(pIdReporte) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Reporte lista materiales";
            wnd.vRadWindowId = "winReporte";
            wnd.vURL = "VentanaReporteListaMateriales.aspx?IdReporte=" + pIdReporte;

            return wnd;
        }

        function GetCostoCapacitacionWindowProperties(pIdReporte) {
            var wnd = GetWindowProperties();

            wnd.vTitulo = "Reporte costo capacitación";
            wnd.vRadWindowId = "winReporte";
            wnd.vURL = "VentanaReporteCostoCapacitacion.aspx?IdReporte=" + pIdReporte;

            return wnd;
        }

      

        function OpenWindow(pWindowProperties) {
            if (pWindowProperties)
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }


       
        function OpenMaximosMinimosWindow(pIdReporte) {
            OpenWindow(GetMaximosMinimosWindowProperties(pIdReporte));
        }

        function OpenCursosRealizadosWindow(pIdReporte) {
            OpenWindow(GetCursosRealizadosWindowProperties(pIdReporte));
        }

        function OpenListaMaterialesWindow(pIdReporte) {
            OpenWindow(GetListaMaterialesWindowProperties(pIdReporte));
        }

        function OpenCostoCapacitacionWindow(pIdReporte) {
            OpenWindow(GetCostoCapacitacionWindowProperties(pIdReporte));
        }

        function HideAll(sender, args) {

            $("#divCursos").hide();
            $("#divInstructores").hide();
            $("#divCompetencias").hide();
            $("#divParticipantes").hide();
            $("#divEventos").hide();
        }


        function ShowGridCursos(sender, args) {

            $("#divCursos").show();
            $("#divInstructores").hide();
            $("#divCompetencias").hide();
            $("#divParticipantes").hide();
            $("#divEventos").hide();
        }

        function ShowGridInstructores(sender, args) {

            $("#divCursos").hide();
            $("#divInstructores").show();
            $("#divCompetencias").hide();
            $("#divParticipantes").hide();
            $("#divEventos").hide();
        }

        function ShowGridCompetencias(sender, args) {

            $("#divCursos").hide();
            $("#divInstructores").hide();
            $("#divCompetencias").show();
            $("#divParticipantes").hide();
            $("#divEventos").hide();
        }

        function ShowGridParticipantes(sender, args) {

            $("#divCursos").hide();
            $("#divInstructores").hide();
            $("#divCompetencias").hide();
            $("#divParticipantes").show();
            $("#divEventos").hide();
        }

        //function ShowGridEventos(sender, args) {

        //    $("#divCursos").hide();
        //    $("#divInstructores").hide();
        //    $("#divCompetencias").hide();
        //    $("#divParticipantes").hide();
        //    $("#divEventos").show();
        //}

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <telerik:RadAjaxLoadingPanel ID="ralpConsultas" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramConsultas" runat="server" DefaultLoadingPanelID="ralpConsultas">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnReporteCursos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnReporteCursos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
     <label class="labelTitulo">Reporte de eventos</label>
      <div style="height: calc(100% - 60px); width: 100%;">
            <telerik:RadSplitter ID="rsReportes" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpReportes" runat="server">
               <div style="float: left; width: 50%; height: 100%;" id="divDerecha">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                        <label>Fecha de inicio:</label>
                            </div>
                        <div class="divControlDerecha">
                        <telerik:RadDatePicker runat="server" ID="dtpInicial" Width="180px"></telerik:RadDatePicker>
                            </div>
                    </div>
                    <div class="ctrlBasico">
                           <div class="divControlIzquierda">
                        <label>Fecha de término:</label>
                               </div>
                          <div class="divControlDerecha">
                        <telerik:RadDatePicker runat="server" ID="dtpTermino" Width="180px"></telerik:RadDatePicker>
                              </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                          <div class="divControlIzquierda">
                        <label>Tipo de curso:</label>
                    </div>
                         <div class="divControlDerecha">
                        <telerik:RadButton runat="server" ID="btnInterno" ToggleType="Radio" GroupName="TipoCurso" Text="Interno" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState />
                                <telerik:RadButtonToggleState CssClass="unchecked" />
                            </ToggleStates>
                        </telerik:RadButton>
                             </div>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnExterno" ToggleType="Radio" GroupName="TipoCurso" Text="Externo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState />
                                <telerik:RadButtonToggleState CssClass="unchecked" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnAmbos" ToggleType="Radio" GroupName="TipoCurso" Text="Ambos" AutoPostBack="false" Checked="true">
                            <ToggleStates>
                                <telerik:RadButtonToggleState />
                                <telerik:RadButtonToggleState CssClass="unchecked" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Cursos:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton runat="server" ID="rbCursosTodos" ToggleType="Radio" GroupName="ca" Text="Todos" Style="margin-right: 10px;" AutoPostBack="false" OnClientClicked="HideAll" Checked="true">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" ID="rbCursosSeleccion" ToggleType="Radio" GroupName="ca" Text="Seleccionados" OnClientClicked="ShowGridCursos" AutoPostBack="false" ToolTip="Haz clic para seleccionar/quitar de la lista, deja la lista en blanco para seleccionar todos.">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Instructores:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton runat="server" ID="rbInsTodos" ToggleType="Radio" GroupName="rp" Text="Todos" Style="margin-right: 10px;" AutoPostBack="false" OnClientClicked="HideAll" Checked="true">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" ID="rbInsSeleccion" ToggleType="Radio" GroupName="rp" Text="Seleccionados" OnClientClicked="ShowGridInstructores" AutoPostBack="false" ToolTip="Haz clic para seleccionar/quitar de la lista, deja la lista en blanco para seleccionar todos.">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Competencias:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton runat="server" ID="rbComTodos" ToggleType="Radio" GroupName="co" Text="Todos" Style="margin-right: 10px;" AutoPostBack="false" OnClientClicked="HideAll" Checked="true">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" ID="rbComSeleccion" ToggleType="Radio" GroupName="co" Text="Seleccionados" AutoPostBack="false" OnClientClicked="ShowGridCompetencias" ToolTip="Haz clic para seleccionar/quitar de la lista, deja la lista en blanco para seleccionar todos.">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Participantes</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton runat="server" ID="rbParTodos" ToggleType="Radio" GroupName="de" Text="Todos" Style="margin-right: 10px;" AutoPostBack="false" OnClientClicked="HideAll" Checked="true">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" ID="rbParSeleccion" ToggleType="Radio" GroupName="de" Text="Seleccionados" OnClientClicked="ShowGridParticipantes" AutoPostBack="false" ToolTip="Haz clic para seleccionar/quitar de la lista, deja la lista en blanco para seleccionar todos.">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>
<%--                    <div style="clear: both; height: 10px;"></div>

                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Eventos de capacitación</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton runat="server" ID="rbEventoTodos" ToggleType="Radio" GroupName="pu" Text="Todos" Style="margin-right: 10px;" AutoPostBack="false" OnClientClicked="HideAll" Checked="true">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" ID="rbEventoSeleccion" ToggleType="Radio" GroupName="pu" Text="Seleccionados" OnClientClicked="ShowGridEventos" AutoPostBack="false" ToolTip="Haga clic para seleccionar/quitar de la lista deje, la lista en blanco para seleccionar todos.">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState />
                                    <telerik:RadButtonToggleState CssClass="unchecked" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>--%>

                    <div style="clear: both; height: 10px;"></div>

                    <div class="ctrlBasico">
                        <div class="ctrlBasico">
                            <telerik:RadButton runat="server" ID="btnReporteCursos" Text="Cursos Realizados" OnClick="btnReporteCursos_Click"></telerik:RadButton>
                        </div>
                     <%--   <div class="ctrlBasico">
                            <telerik:RadButton runat="server" ID="btnReporteMateriales" Text="Lista de materiales" OnClick="btnReporteMateriales_Click"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton runat="server" ID="btnReporteCosto" Text="Costo de capacitación" OnClick="btnReporteCosto_Click"></telerik:RadButton>
                        </div>--%>
                    </div>

                </div>

                <%-- Este div contendra los grids dependiendo de la seleccion que hayan hecho --%>

                <div style="float: left; width: 50%; height: 100%;">

                    <div id="divCursos" style="display: none; height: calc(100% - 1px); width: 100%">
                        <telerik:RadGrid ID="GridCursos" ShowHeader="true" runat="server" HeaderStyle-Font-Bold="true" AllowPaging="false" AllowSorting="false" Width="99%" Height="100%"
                            AllowFilteringByColumn="false" OnNeedDataSource="GridCursos_NeedDataSource" AllowMultiRowSelection="true">
                            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="false">
                                <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID_ENTIDAD" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridClientSelectColumn HeaderText="Selección" UniqueName="FG_SELECCION"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_ENTIDAD" UniqueName="CL_ENTIDAD" Display="true" ReadOnly="true">
                                        <ItemStyle Width="80px" />
                                        <HeaderStyle Width="20%" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn HeaderText="Nombre o Descripción" DataField="NB_ENTIDAD" UniqueName="NB_ENTIDAD" Display="true" ReadOnly="true">
                                        <ItemStyle Width="300px" />
                                        <HeaderStyle Width="65%" />
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                    </div>

                    <div id="divInstructores" style="display: none; height: calc(100% - 1px); width: 100%;">
                        <telerik:RadGrid ID="GridInstructores" ShowHeader="true" runat="server" HeaderStyle-Font-Bold="true" AllowPaging="false" AllowSorting="false" Width="99%" Height="100%"
                            AllowFilteringByColumn="false" OnNeedDataSource="GridInstructores_NeedDataSource" AllowMultiRowSelection="true">
                            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="false">
                                <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID_ENTIDAD" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridClientSelectColumn HeaderText="Selección" UniqueName="FG_SELECCION"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_ENTIDAD" UniqueName="CL_ENTIDAD" Display="true" ReadOnly="true">
                                        <ItemStyle Width="80px" />
                                        <HeaderStyle Width="20%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Nombre o Descripción" DataField="NB_ENTIDAD" UniqueName="NB_ENTIDAD" Display="true" ReadOnly="true">
                                        <ItemStyle Width="300px" />
                                        <HeaderStyle Width="65%" />
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>

                    <div id="divCompetencias" style="display: none; height: calc(100% - 1px); width: 100%;">
                        <telerik:RadGrid ID="GridCompetencias" ShowHeader="true" runat="server" HeaderStyle-Font-Bold="true" AllowPaging="false" AllowSorting="false" Width="99%" Height="100%"
                            AllowFilteringByColumn="false" OnNeedDataSource="GridCompetencias_NeedDataSource" AllowMultiRowSelection="true">
                            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="false">
                                <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID_ENTIDAD" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridClientSelectColumn HeaderText="Selección" UniqueName="FG_SELECCION"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_ENTIDAD" UniqueName="CL_ENTIDAD" Display="true" ReadOnly="true">
                                        <HeaderStyle Width="20%" />
                                        <ItemStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Nombre o Descripción" DataField="NB_ENTIDAD" UniqueName="NB_ENTIDAD" Display="true" ReadOnly="true">
                                        <HeaderStyle Width="65%" />
                                        <ItemStyle Width="300px" />
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>

                    <div id="divParticipantes" style="display: none; height: calc(100% - 1px); width: 100%;">
                        <telerik:RadGrid ID="GridParticipantes" ShowHeader="true" runat="server" HeaderStyle-Font-Bold="true" AllowPaging="false" AllowSorting="false" Width="99%" Height="100%"
                            AllowFilteringByColumn="false" OnNeedDataSource="GridParticipantes_NeedDataSource" AllowMultiRowSelection="true">
                            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="false">
                                <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID_ENTIDAD" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridClientSelectColumn HeaderText="Selección" UniqueName="FG_SELECCION"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_ENTIDAD" UniqueName="CL_ENTIDAD" Display="true" ReadOnly="true">
                                        <HeaderStyle Width="20%" />
                                        <ItemStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Nombre o Descripción" DataField="NB_ENTIDAD" UniqueName="NB_ENTIDAD" Display="true" ReadOnly="true">
                                        <HeaderStyle Width="65%" />
                                        <ItemStyle Width="300px" />
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>

                   <%-- <div id="divEventos" style="display: none; height: calc(100% - 1px); width: 100%;">
                        <telerik:RadGrid ID="GridEventos" ShowHeader="true" runat="server" HeaderStyle-Font-Bold="true" AllowPaging="false" AllowSorting="true" Width="99%" Height="100%"
                            AllowFilteringByColumn="false" OnNeedDataSource="GridEventos_NeedDataSource" AllowMultiRowSelection="true">
                            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="false">
                                <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID_ENTIDAD" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridClientSelectColumn HeaderText="Selección" UniqueName="FG_SELECCION"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_ENTIDAD" UniqueName="CL_ENTIDAD" Display="true" ReadOnly="true">
                                        <ItemStyle Width="80px" />
                                        <HeaderStyle Width="20%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Nombre o Descripción" DataField="NB_ENTIDAD" UniqueName="NB_ENTIDAD" Display="true" ReadOnly="true">
                                        <HeaderStyle Width="65%" />
                                        <ItemStyle Width="300px" />
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>--%>
                </div>
 </telerik:RadPane>
      <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px" Height="50px">
                <telerik:RadSlidingZone ID="rszAyuda" SlideDirection="Left" runat="server" ExpandedPaneId="rspAyuda" Width="22px">
                     <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="270px" RenderMode="Mobile" Height="100%">
                       <div id="divReportes" runat="server" style="display: block; padding-left: 10px; padding-right: 10px; padding-top: 20px; text-align:justify;">
                            <p>
                               Para aplicar filtros de búsqueda al reporte seleccionado utiliza las herramientas de selección mostradas más abajo. En las listas de selección si las dejas sin seleccionar le indicas al sistema que no quieres aplicar filtros; de otra manera seleccionando alguno de los elementos mostrados el sistema asumirá que únicamente deseas la información relativa a dicho elemento.
                              Por ejemplo, si deseas que el reporte genere toda la información sobre cursos, no selecciones elementos de la lista. Si deseas que te muestre la información de sólo un curso, haz clic en él, seleccionando el botón "Seleccionados" y sólo te mostrará información para dichos cursos, los demás serán ignorados.
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
    </telerik:RadSplitter>
   </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <Windows>
            <telerik:RadWindow ID="winReporte" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winDetalle" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
