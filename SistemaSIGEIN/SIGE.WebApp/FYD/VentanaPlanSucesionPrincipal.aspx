<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaPlanSucesionPrincipal.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaPlanSucesionPrincipal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script type="text/javascript">

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

        function OpenEmpleadoSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "winSeleccion", "Selección de empleado")
        }

        function openPerfilPuestoSucedidoWindow() {

            var idPuesto = '<%# vIdPuesto %>';
            OpenSelectionWindow("VentanaPerfilPuestos.aspx?Puestos=" + idPuesto, "winPerfil", "Perfil del puesto")
        }

        function openPerfilPuestoEmpleado(vIdPuesto) {
            OpenSelectionWindow("../Administracion/VentanaVistaDescriptivo.aspx?PuestoId=" + vIdPuesto, "winEditarPuesto", "Vista previa descripción del puesto");
        }

        function openAnalisisSucesionWindow() {

            var vEmpleadosSucesores = [];
            var vEmpleadosJson = "";
            var idEmpleadoSeleccionado = "<%# vIdEmpleado %>";
            var masterTable = $find("<%= rgSucesion.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();

            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vEmpleado = {
                        ID_EMPLEADO: selectedItem.getDataKeyValue("ID_EMPLEADO"),
                        ID_PERIODO: selectedItem.getDataKeyValue("ID_PERIODO_COMPETENCIAS")
                    }

                    vEmpleadosSucesores.push(vEmpleado);
                }

                vEmpleadosJson = JSON.stringify(vEmpleadosSucesores);
                OpenSelectionWindow("VentanaPlanSucesion.aspx?idEmpleado=" + idEmpleadoSeleccionado + "&sucesores=" + vEmpleadosJson, "winAnalisis", "Plan de sucesión - Análisis de sucesores");
            }
            else {
                radalert("Selecciona un empleado.", 400, 150, "Aviso");
            }
        }

        function OpenPlanSucesionWindow(pIdEmpleado) {
            var idPuestoSuceder = '<%# vIdPuesto %>';
            var idEmpleadoSuceder = '<%# vIdEmpleado %>';
            OpenSelectionWindow("VentanaEvaluacionCompetenciasPS.aspx?idEmpleadoSucesor=" + pIdEmpleado + "&PuestoSuceder=" + idPuestoSuceder + "&IdEmpleadoSuceder=" + idEmpleadoSuceder, "winPerfil", "Plan sucesión - Evaluación de competencias")
        }

        function openComparacionperfilPuestosWindow(vIdEmpleado) {
            var idPuesto = <%# vIdPuesto %>
            OpenSelectionWindow("VentanaPerfilPuestos.aspx?idEmpleado=" + vIdEmpleado + "&Puestos=" + idPuesto, "winPerfil", "Perfil del puesto")
        }


        function OpenReporteIndividualWindow(vIdPeriodo, vIdEmpleado) {

            if (vIdPeriodo != null & vIdEmpleado != 0) {
                OpenSelectionWindow("ReporteIndividual.aspx?IdPeriodo=" + vIdPeriodo + "&IdEmpleado=" + vIdEmpleado, "winPerfil", "Reporte General Individual");
            }
        }


        function OpenEvaluacionDesempenoWindow(pIdPeriodo, pIdEvaluado) {
            if (pIdPeriodo != 0 & pIdEvaluado != 0) {
                OpenSelectionWindow("../EO/VentanaReporteCumplimientoPersonal.aspx?idPeriodo=" + pIdPeriodo + "&idEvaluado=" + pIdEvaluado, "winPerfil", "Reporte evaluación del desempeño");
            }
        }

        function OpenTableroControlWindow(pIdEmpleado, pIdDesempeno, pIdCompetencia, pIdPuesto) {
            if (pIdEmpleado != 0) {
                OpenSelectionWindow("../TC/VentanaConsultaTablero.aspx?idEmpleado=" + pIdEmpleado + "&idDesempeno=" + pIdDesempeno + "&idCompetencia=" + pIdCompetencia + "&idPuesto=" + pIdPuesto, "winPerfil", "Tablero de control");
            }
        }

        function OpenAvanceProgramaWindow(vIdPrograma, vIdEmpleado) {

            if (vIdPrograma != null) {
                if (vIdPrograma != -1) {
                    OpenSelectionWindow("VentanaAvanceProgramaCapacitacion.aspx?IdPrograma=" + vIdPrograma + "&IdEmpleado=" + vIdEmpleado + "&clOrigen=SUCESION", "winPerfil", "Avance programa de capacitación");
                }
            }
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                console.info(pDato);

                var arr = "";

                for (var i = 0; i < pDato.length; i++) {
                    arr = arr + pDato[i].idEmpleado + ",";
                }

                arr = arr.substr(0, arr.length - 1);

                var ajaxManager = $find('<%= ramAgregarEmpleados.ClientID%>');
                ajaxManager.ajaxRequest(arr);

            }
        }


    </script>

    <style>
        .ALTO {
            background-color: green;
            border-radius: 5px;
            border: 1px solid black;
            width: 100%;
            padding: 10px;
        }

        .MEDIO {
            background-color: gold;
            border-radius: 5px;
            border: 1px solid black;
            width: 100%;
            padding: 10px;
        }

        .BAJO {
            background-color: red;
            border-radius: 5px;
            border: 1px solid black;
            width: 100%;
            padding: 10px;
        }

        .OTRO {
            background-color: gray;
            border-radius: 5px;
            border: 1px solid black;
            padding: 10px;
            width: 100%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpEmpleados" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramAgregarEmpleados" runat="server" OnAjaxRequest="ramAgregarEmpleados_AjaxRequest" DefaultLoadingPanelID="ralpEmpleados">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramAgregarEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSucesion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSucesion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="rsSucesion" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpSucesion" runat="server">
            <label class="labelTitulo">Plan de sucesión</label>
            <%--<div style="clear: both; height: 10px;"></div>--%>
            <div style="height: calc(100% - 70px); width: 50%; float: left;">
                <div style="height: calc(100% - 30px);">
                    <table class="ctrlTableForm ctrlTableContext">
                        <tr>
                            <td>
                                <label>Empleado:</label>
                            </td>
                            <td>
                                <span id="txtNbEmpleado" runat="server" style="width: 250px"></span>
                            </td>
                            <td rowspan="3">
                                <div style="background: #fafafa; right: 0px; border: 1px solid lightgray; border-radius: 10px; padding: 5px;">
                                    <telerik:RadBinaryImage ID="rbiFotoSuceder" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Perfil del puesto:</label>
                            </td>
                            <td>
                                <a href="javascript:openPerfilPuestoSucedidoWindow()" id="txtNbPuesto" runat="server" style="width: 250px"></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Antigüedad:</label>
                            </td>
                            <td>
                                <span id="txtNbAntiguedad" runat="server" style="width: 250px"></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" ID="btnEmpleados" Text="Agregar candidatos" AutoPostBack="false" OnClientClicked="OpenEmpleadoSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" ID="btnTablaComparativa" Text="Tabla Comparativa" AutoPostBack="false" OnClientClicked="openAnalisisSucesionWindow"></telerik:RadButton>
                </div>
                <%--<div class="ctrlBasico">
                        <telerik:RadTextBox ID="txtNombreEmpleado" runat="server" Width="250px" ReadOnly="true"></telerik:RadTextBox>
                        <div style="clear:both; height:10px;"></div>
                        <telerik:RadTextBox ID="txtDepartamento" runat="server" Width="250px" ReadOnly="true"></telerik:RadTextBox>
                        <telerik:RadButton runat="server" ID="btnPerfilPuesto" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="openPerfilPuestoSucedidoWindow" />
                        <div style="clear:both; height:10px;"></div>
                        <telerik:RadTextBox ID="txtAntiguedad" runat="server" Width="250px" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>
            </div>
            <div style="height: calc(100% - 70px); width: 50%; float: left;">
                <telerik:RadGrid runat="server" ID="rgSucesion" Height="100%" HeaderStyle-Font-Bold="true" OnNeedDataSource="rgSucesion_NeedDataSource" OnItemDataBound="rgSucesion_ItemDataBound" AutoGenerateColumns="false" AllowMultiRowSelection="true" MasterTableView-NoMasterRecordsText="No existen sucesores recomendados.">
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                    <MasterTableView DataKeyNames="ID_EMPLEADO, ID_PERIODO_COMPETENCIAS, ID_PUESTO,FE_INICIO_DESEMPEÑO" ClientDataKeyNames="ID_EMPLEADO, ID_PERIODO_COMPETENCIAS, ID_PUESTO">
                        <ColumnGroups>
                            <telerik:GridColumnGroup HeaderText="Evaluación de competencias" HeaderStyle-HorizontalAlign="Center" Name="Competencias"></telerik:GridColumnGroup>
                            <telerik:GridColumnGroup HeaderText="Evaluación de desempeño" HeaderStyle-HorizontalAlign="Center" Name="Desempeño"></telerik:GridColumnGroup>
                        </ColumnGroups>
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="FG_SELECCION">
                                <HeaderStyle Width="40px" />
                            </telerik:GridClientSelectColumn>
                            <%--                            <telerik:GridBoundColumn UniqueName="NB_EMPLEADO" HeaderText="Empleado" DataField="NB_EMPLEADO" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn UniqueName="NB_PUESTO" DataTextField="NB_PUESTO" HeaderText="Puesto" DataNavigateUrlFields="ID_PUESTO" DataNavigateUrlFormatString="javascript:openPerfilPuestoEmpleado({0})" Visible="false"></telerik:GridHyperLinkColumn>
                            <telerik:GridBoundColumn UniqueName="NB_ANTIGUEDAD" HeaderText="Antigüedad" DataField="NB_ANTIGUEDAD" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FE_INICIO_COMPETENCIAS" HeaderText="Inicio" DataField="FE_INICIO_COMPETENCIAS" ColumnGroupName="Competencias" DataFormatString="{0:dd/mm/yyyy}" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn UniqueName="HL_COMPETENCIAS" DataTextField="PR_COMPETENCIAS" HeaderText="% Compatibilidad" ColumnGroupName="Competencias" DataTextFormatString="{0:N2}" DataNavigateUrlFields="ID_PERIODO_COMPETENCIAS, ID_EMPLEADO" DataNavigateUrlFormatString="javascript:OpenReporteIndividualWindow({0},{1})" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridBoundColumn UniqueName="FE_INICIO_DESEMPEÑO" HeaderText="Inicio" DataField="FE_INICIO_DESEMPEÑO" ColumnGroupName="Desempeño" DataFormatString="{0:dd/mm/yyyy}" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn UniqueName="HL_DESEMPEÑO" HeaderText="% Desempeño" DataTextField="PR_DESEMPEÑO" ColumnGroupName="Desempeño" DataTextFormatString="{0:N2}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn UniqueName="HL_PROGRAMA" HeaderText="Plan de carrera" DataTextField="NB_PROGRAMA" DataNavigateUrlFields="ID_PROGRAMA" DataNavigateUrlFormatString="javascript:OpenAvanceProgramaWindow({0})" Visible="false"></telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn UniqueName="HL_TABLERO" DataTextField="CL_TABLERO_CONTROL" Visible="false"></telerik:GridHyperLinkColumn>--%>
                            <telerik:GridTemplateColumn UniqueName="TC_DATOS_GENERALES" HeaderText="Sucesores">
                                <ItemTemplate>
                                    <div>
                                        <div class="ctrlBasico" style="width: 100%;">
                                            <table style="width: 100%;" border="0">
                                                <tr>
                                                    <td rowspan="8">
                                                        <div class="<%# Eval("CL_POTENCIAL_SUCESOR") %>">
                                                            <br />
                                                            &nbsp&nbsp&nbsp
                                                        </div>
                                                    </td>
                                                    <td style="width: 65%;">
                                                        <label style="padding-left: 10px;" title="<%# Eval("CL_EMPLEADO") %>"><%# Eval("NB_EMPLEADO") %></label></td>
                                                    <td style="width: 35%;">
                                                        <label style="color: red;"><%#Eval("CL_TIPO_EMPLEADO") %> </label>
                                                    </td>
                                                    <td rowspan="8">
                                                        <div style="background: #fafafa; right: 0px; border: 1px solid lightgray; border-radius: 10px; padding: 5px;">
                                                            <telerik:RadBinaryImage ID="rbiFotoEmpleado" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" DataValue='<%# Eval("FI_FOTOGRAFIA") %>' />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <a style="padding-left: 10px;" title="<%# Eval("CL_PUESTO") %>" href="javascript:openPerfilPuestoEmpleado(<%# Eval("ID_PUESTO") %>)"><%# Eval("NB_PUESTO") %> </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <label style="padding-left: 10px;">Antigüedad: </label>
                                                        <%# Eval("NB_ANTIGUEDAD") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" id="tdEvalComp" runat="server">
                                                        <%--<label style="padding-left:10px;">Evalación de competencias: </label> <%# Eval("FE_INICIO_COMPETENCIAS") %> &nbsp <a href="javascript:OpenReporteIndividualWindow(<%# Eval("ID_PERIODO_COMPETENCIAS") %>,<%# Eval("ID_EMPLEADO") %>)"> <%#Eval("PR_COMPETENCIAS") %>% </a>--%>
                                                        <%-- <label style="padding-left:10px;" > <a href="javascript:OpenReporteIndividualWindow(<%# Eval("ID_PERIODO_COMPETENCIAS") %>,<%# Eval("ID_EMPLEADO") %>)"> Evaluación de competencias: </a></label><asp:label runat="server" style="font-weight:normal;" ForeColor='<%# Eval("FE_INICIO_COMPETENCIAS").Equals("Sin realizar")? System.Drawing.Color.Red : System.Drawing.Color.Black %>'> &nbsp<%# Eval("FE_INICIO_COMPETENCIAS") %> </asp:label><asp:label ID="Label1" runat="server" style="font-weight:bold;" ForeColor='<%# Eval("CL_POTENCIAL_SUCESOR").Equals("ALTO")? System.Drawing.Color.Green : Eval("CL_POTENCIAL_SUCESOR").Equals("MEDIO")? System.Drawing.Color.Gold : Eval("CL_POTENCIAL_SUCESOR").Equals("BAJO")? System.Drawing.Color.Red :  System.Drawing.Color.Black %>'>&nbsp  <%#Eval("PR_COMPETENCIAS") %></asp:label> --%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" id="tdEvalDes" runat="server">
                                                        <%--<label style="padding-left:10px;">Evalación de desempeño: </label> <%# DateTime.Parse(Eval("FE_INICIO_DESEMPEÑO").ToString()).ToShortDateString() %> &nbsp <a><%#Eval("PR_DESEMPEÑO") %>% </a></td>--%>
                                                        <%-- <label style="padding-left:10px;"> <a href="javascript:OpenEvaluacionDesempenoWindow(<%# Eval("ID_PERIODO_DESEMPEÑO") %> ,<%# Eval("ID_EVALUADO") %>)"><%# Eval("FE_INICIO_DESEMPEÑO").Equals("Sin realizar")? "Evaluación de desempeño:":"<u>Evaluación de desempeño:</u>" %></a></label><asp:label runat="server" style="font-weight:normal;" ForeColor='<%# Eval("FE_INICIO_DESEMPEÑO").Equals("Sin realizar")? System.Drawing.Color.Red : System.Drawing.Color.Black %>'> &nbsp  <%#  Eval("FE_INICIO_DESEMPEÑO") %> &nbsp <%#Eval("PR_DESEMPEÑO") %> </asp:label>--%>
                                                    </td>
                                                </tr>
                                                    <tr>
                                                    <td colspan="2">
                                                      <a style="padding-left: 10px;" href="javascript:openComparacionperfilPuestosWindow(<%# Eval("ID_EMPLEADO") %>)">Perfil del puesto</a>
                                                    </td>
                                                </tr>
                              <%--                  <tr>
                                                    <td colspan="2" id="tdPrograma" runat="server">--%>
                                                        <%--<label style="padding-left:10px;">Plan de carrera: </label> <a href="javascript:OpenAvanceProgramaWindow(<%# Eval("ID_PROGRAMA") %>)"><%# Eval("NB_PROGRAMA") %> </a>--%>
                                                        <%-- <label style="padding-left:10px; "><a href="javascript:OpenAvanceProgramaWindow(<%# Eval("ID_PROGRAMA") %>)"> Plan de carrera: </a></label><asp:label runat="server" style="font-weight:normal;" ForeColor='<%# Eval("NB_PROGRAMA").Equals("Sin realizar")? System.Drawing.Color.Red : System.Drawing.Color.Black %>'> &nbsp <%# Eval("NB_PROGRAMA") %> </asp:label>--%>
                                           <%--         </td>
                                                </tr>--%>
                                                <tr>
                                                    <td colspan="2"><a style="padding-left: 10px;" href="javascript:OpenTableroControlWindow(<%# Eval("ID_EMPLEADO") %>, <%# Eval("ID_PERIODO_DESEMPEÑO") %>, <%# Eval("ID_PERIODO_COMPETENCIAS") %> , <%# Eval("ID_PUESTO") %>)"><u><%# Eval("CL_TABLERO_CONTROL") %></u> </a></td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div style="clear: both; height: 10px;"></div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="500" MinWidth="500" Height="100%">
                    <div style="padding: 5px; text-align: justify;">
                        Esta página te permite definir a los posibles sucesores de la persona seleccionada. Para facilitarte esta decisión te presentamos la siguiente propuesta. Es posible agregar más candidatos al análisis. Una vez que hayas concluido la revisión selecciona a la(s) persona(s) que consideres más adecuadas. Da clic en tabla comparativa cuando hayas terminado la selección de posibles sucesores. 
                    </div>
                </telerik:RadSlidingPane>
                     <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="350px" Title="Código de color" Height="100%">
                                    <div style="padding: 10px; text-align: justify;">
                                        <telerik:RadGrid ID="grdCodigoColores"
                                            runat="server"
                                            Height="300"
                                            Width="300"
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
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <Windows>
            <%--  <telerik:RadWindow ID="winCamposAdicionales" runat="server" Title="Seleccionar" Height="650px" Width="850px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winPeriodo" runat="server" Title="Agregar/Editar periodo" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winPerfil" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winAnalisis" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
               <telerik:RadWindow ID="winMatrizCuestionarios" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEditarEmpleado" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEditarPuesto" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
              <telerik:RadWindow ID="winCumplimientoGlobal" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinClimaLaboral" runat="server" Width="1000px" Height="650px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
            <telerik:RadWindow ID="WinTabuladores" runat="server" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" ReloadOnShow="false" Animation="Fade" Width="1350px" Height="665px"></telerik:RadWindow>
            <telerik:RadWindow ID="winEmpleado" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winDescriptivo" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwReporte" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="WinTableroControl" runat="server" Width="750px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winConsultatablero" runat="server" Width="760px" Height="590px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>--%>
        
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
