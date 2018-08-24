<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="PlanCarreraSucesion.aspx.cs" Inherits="SIGE.WebApp.FYD.PlanCarreraSucesion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var idEmpleado = "";
        var arr = [1, 2, 3];

        function obtenerIdFila() {
            var grid = $find("<%=grdEmpleados.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                idEmpleado = row.getDataKeyValue("M_EMPLEADO_ID_EMPLEADO");
            }
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

        function abrirPlan() {
            obtenerIdFila();
            if ((idEmpleado != "")) {
                OpenSelectionWindow("VentanaPlanVidaCarreraPrincipal.aspx?idEmpleado=" + idEmpleado, 'rwReporte', "Plan de vida y carrera");
               // win.focus();
            }
            else { radalert("No has seleccionado un empleado.", 400, 150, ""); }
        }

        function abrirSucesion() {

            obtenerIdFila();
            if (idEmpleado != "") {
                OpenSelectionWindow("VentanaPlanSucesionPrincipal.aspx?idEmpleado=" + idEmpleado + "&Prioridades=" + arr, 'rwReporte', "Plan de sucesión");
              //  win.focus();
            }
            else {
                radalert("No has seleccionado un empleado.", 400, 150, "");
            }
        }

        function guardarPrioridades(sender, args) {
            var chkAlto = $find("<%=chkAlto.ClientID %>");
            var chkMedio = $find("<%=chkMedio.ClientID %>");
            var chkBajo = $find("<%=chkBajo.ClientID %>");

            if (sender == chkAlto) {
                if (chkAlto.get_checked()) {
                    arr.push(1);
                }
                else {
                    arr.splice(arr.indexOf(1), 1);
                }
            }

            if (sender == chkMedio) {
                if (chkMedio.get_checked()) {
                    arr.push(2);
                }
                else {
                    arr.splice(arr.indexOf(2), 1);
                }
            }

            if (sender == chkBajo) {
                if (chkBajo.get_checked()) {
                    arr.push(3);
                }
                else {
                    arr.splice(arr.indexOf(3), 1);
                }
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Plan de vida y carrera / Plan de sucesión</label>
    <div style="height: calc(100% - 160px);">
        <telerik:RadSplitter ID="splEmpleados" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridEmpleados" runat="server">
                <telerik:RadGrid ID="grdEmpleados" runat="server" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true" OnNeedDataSource="grdEmpleados_NeedDataSource" OnItemDataBound="grdEmpleados_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="M_EMPLEADO_ID_EMPLEADO" EnableColumnsViewState="false" DataKeyNames="M_EMPLEADO_ID_EMPLEADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="No. de empleado" DataField="M_EMPLEADO_CL_EMPLEADO" UniqueName="M_EMPLEADO_CL_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre completo" DataField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre" DataField="M_EMPLEADO_NB_EMPLEADO" UniqueName="M_EMPLEADO_NB_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno" DataField="M_EMPLEADO_NB_APELLIDO_PATERNO" UniqueName="M_EMPLEADO_NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido materno" DataField="M_EMPLEADO_NB_APELLIDO_MATERNO" UniqueName="M_EMPLEADO_NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Puesto" DataField="M_PUESTO_NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave del área" DataField="M_DEPARTAMENTO_CL_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_CL_DEPARTAMENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área" DataField="M_DEPARTAMENTO_NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género" DataField="M_EMPLEADO_CL_GENERO" UniqueName="M_EMPLEADO_CL_GENERO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado civil" DataField="M_EMPLEADO_CL_ESTADO_CIVIL" UniqueName="M_EMPLEADO_CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre del cónyuge" DataField="M_EMPLEADO_NB_CONYUGUE" UniqueName="M_EMPLEADO_NB_CONYUGUE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="RFC" DataField="M_EMPLEADO_CL_RFC" UniqueName="M_EMPLEADO_CL_RFC"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="CURP" DataField="M_EMPLEADO_CL_CURP" UniqueName="M_EMPLEADO_CL_CURP"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="NSS" DataField="M_EMPLEADO_CL_NSS" UniqueName="M_EMPLEADO_CL_NSS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Tipo sanguíneo" DataField="M_EMPLEADO_CL_TIPO_SANGUINEO" UniqueName="M_EMPLEADO_CL_TIPO_SANGUINEO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nacionalidad" DataField="M_EMPLEADO_CL_NACIONALIDAD" UniqueName="M_EMPLEADO_CL_NACIONALIDAD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="País" DataField="M_EMPLEADO_NB_PAIS" UniqueName="M_EMPLEADO_NB_PAIS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Entidad federativa" DataField="M_EMPLEADO_NB_ESTADO" UniqueName="M_EMPLEADO_NB_ESTADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Municipio" DataField="M_EMPLEADO_NB_MUNICIPIO" UniqueName="M_EMPLEADO_NB_MUNICIPIO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Colonia" DataField="M_EMPLEADO_NB_COLONIA" UniqueName="M_EMPLEADO_NB_COLONIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calle" DataField="M_EMPLEADO_NB_CALLE" UniqueName="M_EMPLEADO_NB_CALLE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Exterior" DataField="M_EMPLEADO_NO_EXTERIOR" UniqueName="M_EMPLEADO_NO_EXTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Interior" DataField="M_EMPLEADO_NO_INTERIOR" UniqueName="M_EMPLEADO_NO_INTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Código postal" DataField="M_EMPLEADO_CL_CODIGO_POSTAL" UniqueName="M_EMPLEADO_CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="M_EMPLEADO_NB_EMPRESA" UniqueName="M_EMPLEADO_NB_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Correo electrónico" DataField="M_EMPLEADO_CL_CORREO_ELECTRONICO" UniqueName="M_EMPLEADO_CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Natalicio" DataField="M_EMPLEADO_FE_NACIMIENTO" UniqueName="M_EMPLEADO_FE_NACIMIENTO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Lugar de nacimiento" DataField="M_EMPLEADO_DS_LUGAR_NACIMIENTO" UniqueName="M_EMPLEADO_DS_LUGAR_NACIMIENTO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de alta" DataField="M_EMPLEADO_FE_ALTA" UniqueName="M_EMPLEADO_FE_ALTA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de baja" DataField="M_EMPLEADO_FE_BAJA" UniqueName="M_EMPLEADO_FE_BAJA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo" DataField="M_EMPLEADO_MN_SUELDO" UniqueName="M_EMPLEADO_MN_SUELDO" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo variable" DataField="M_EMPLEADO_MN_SUELDO_VARIABLE" UniqueName="M_EMPLEADO_MN_SUELDO_VARIABLE" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Composición del sueldo" DataField="M_EMPLEADO_DS_SUELDO_COMPOSICION" UniqueName="M_EMPLEADO_DS_SUELDO_COMPOSICION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave de la empresa" DataField="C_EMPRESA_CL_EMPRESA" UniqueName="C_EMPRESA_CL_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre de la empresa" DataField="C_EMPRESA_NB_EMPRESA" UniqueName="C_EMPRESA_NB_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Razón social" DataField="C_EMPRESA_NB_RAZON_SOCIAL" UniqueName="C_EMPRESA_NB_RAZON_SOCIAL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Estado" DataField="M_EMPLEADO_CL_ESTADO_EMPLEADO" UniqueName="M_EMPLEADO_CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Activo" DataField="M_EMPLEADO_FG_ACTIVO" UniqueName="M_EMPLEADO_FG_ACTIVO"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="RSPAdvSearch" runat="server" Title="Búsqueda avanzada" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 20px;">
                            <telerik:RadFilter runat="server" ID="ftGrdEmpleados" FilterContainerID="grdEmpleados" ShowApplyButton="true" Height="100">
                                <ContextMenu Height="100" EnableAutoScroll="false">
                                    <DefaultGroupSettings Height="100" />
                                </ContextMenu>
                            </telerik:RadFilter>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div style="clear: both; height: 20px;"></div>

    <div class="ctrlBasico">
        <fieldset>
            <legend>
                <label>Plan de vida y carrera</label>
            </legend>
            <table>
                <tr>
                    <td>
                        <div style="margin-left: 5px; margin-bottom: 5px; margin-right: 5px">
                            <telerik:RadButton ID="btnPlanVidaCarrera" runat="server" Text="Plan de vida y carrera" OnClientClicked="abrirPlan" AutoPostBack="false"></telerik:RadButton>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>

    <div class="ctrlBasico">
        <fieldset>
            <legend>
                <label>Plan de sucesión</label>
            </legend>

            <table style="width: 100%;">
                <tr>
                    <td>
                        <div style="margin-left: 5px; margin-bottom: auto; margin-top:auto; margin-right: 5px; float: left;">
                            <telerik:RadCheckBox ID="chkAlto" runat="server" Text="Sucesores con alto potencial" Checked="true" AutoPostBack="false" OnClientCheckedChanged="guardarPrioridades" ToolTip="Esta opción te presenta personas que han participado en procesos de evaluación de competencias y que tienen una compatibilidad alta con el puesto a suceder."></telerik:RadCheckBox>
                        </div>
                    </td>
                    <td>
                        <div style="height: 20px; width: 20px; border-radius: 5px; background: green; margin-top:auto; margin-bottom:auto; margin-left:5px; margin-right:5px; border: 1px solid black;">&nbsp;</div>
                    </td>
                    <td>
                        <div style="margin-left: 5px; margin-bottom: auto; margin-right: 5px; margin-top:auto; float: left;">
                            <telerik:RadCheckBox ID="chkMedio" runat="server" Text="Sucesores con potencial medio" Checked="true" AutoPostBack="false" OnClientCheckedChanged="guardarPrioridades" ToolTip="Esta opción te presenta personas que han participado en procesos de evaluación de competencias y que tienen una compatibilidad media con el puesto a suceder o a personas que tienen potencial pero que tienen evaluaciones parciales o carecen de evaluación."></telerik:RadCheckBox>
                        </div>
                    </td>
                    <td>
                        <div style="height: 20px; width: 20px; border-radius: 5px; background: yellow; margin-left: 5px; margin-right:5px; margin-bottom: auto; margin-top: auto; border: 1px solid black;">&nbsp;</div>
                    </td>
                    <td>
                        <div style="margin-left: 5px; margin-bottom: auto; margin-right: 5px; margin-top: auto; float: left;">
                            <telerik:RadCheckBox ID="chkBajo" runat="server" Text="Sucesores con bajo potencial" Checked="true" AutoPostBack="false" OnClientCheckedChanged="guardarPrioridades" ToolTip="Esta opción te presenta personas que han participado en procesos de evaluación de competencias y que tienen una compatibilidad baja con el puesto a suceder o a personas que tienen evaluaciones parciales o carecen de evaluación. "></telerik:RadCheckBox>
                        </div>
                    </td>
                    <td>
                        <div style="height: 20px; width: 20px; border-radius: 5px; background: red; margin-left: 5px; margin-top: auto; margin-right:5px; margin-bottom:auto; border: 1px solid black;">&nbsp;</div>
                    </td>
                    <td>
                        <div style="margin-left: 5px; margin-bottom: 5px; margin-right: 5px">
                            <telerik:RadButton ID="btnPlanSucesion" runat="server" Text="Plan de sucesión" OnClientClicked="abrirSucesion" AutoPostBack="false"></telerik:RadButton>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>


    <%--  <div class="ctrlBasico" style="width: 99%;">
        <telerik:RadButton ID="btnPlanVidaCarrera" runat="server" Text="Plan de vida y carrera" OnClientClicked="abrirPlan" AutoPostBack="false"></telerik:RadButton>
    </div>--%>

    <%--   <div style="clear: both; height: 5px;"></div>

    <div class="ctrlBasico" style="width: 99%;">
        <div class="ctrlBasico">
            <telerik:RadCheckBox ID="chkAlto" runat="server" Text="Sucesores con alto potencial" Checked="true" AutoPostBack="false" OnClientCheckedChanged="guardarPrioridades" ToolTip="Esta opción te presenta personas que han participado en procesos de evaluación de competencias y que tienen una compatibilidad alta con el puesto a suceder."></telerik:RadCheckBox>
        </div>
        <div class="ctrlBasico">
            <telerik:RadCheckBox ID="chkMedio" runat="server" Text="Sucesores con potencial medio" Checked="true" AutoPostBack="false" OnClientCheckedChanged="guardarPrioridades" ToolTip="Esta opción te presenta personas que han participado en procesos de evaluación de competencias y que tienen una compatibilidad media con el puesto a suceder o a personas que tienen potencial pero que tienen evaluaciones parciales o carecen de evaluación."></telerik:RadCheckBox>
        </div>
        <div class="ctrlBasico">
            <telerik:RadCheckBox ID="chkBajo" runat="server" Text="Sucesores con bajo potencial" Checked="true" AutoPostBack="false" OnClientCheckedChanged="guardarPrioridades"   ToolTip="Esta opción te presenta personas que han participado en procesos de evaluación de competencias y que tienen una compatibilidad baja con el puesto a suceder o a personas que tienen evaluaciones parciales o carecen de evaluación. "></telerik:RadCheckBox>
        </div>
    </div>

    <div style="clear: both; height: 5px;"></div>

    <div class="ctrlBasico" style="width: 99%;">
        <telerik:RadButton ID="btnPlanSucesion" runat="server" Text="Plan de sucesión" OnClientClicked="abrirSucesion" AutoPostBack="false"></telerik:RadButton>
    </div>--%>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
       <Windows>
             <telerik:RadWindow ID="winCamposAdicionales" runat="server" Title="Seleccionar" Height="650px" Width="850px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
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
            <telerik:RadWindow ID="WinTableroControl" runat="server" Width="750px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winConsultatablero" runat="server" Width="760px" Height="590px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>

             <telerik:RadWindow ID="rwReporte" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
       </Windows>
    </telerik:RadWindowManager>
</asp:Content>
