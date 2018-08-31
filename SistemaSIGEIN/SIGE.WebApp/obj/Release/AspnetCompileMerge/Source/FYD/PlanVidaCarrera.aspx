<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="PlanVidaCarrera.aspx.cs" Inherits="SIGE.WebApp.FYD.PlanVidaCarrera" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <link rel="stylesheet" type="text/css" href="/Assets/css/cssOrgChart.css" />

    <style>
        .cssNaturalActual .rocItemTemplate {
            background-color: #A9D0F5 !important;
        }

        .cssNatural .rocItemTemplate {
            background-color: #81BEF7 !important;
        }

        .cssAlternativaActual .rocItemTemplate {
            background-color: #DEFDC4 !important;
        }

        .cssAlternativa .rocItemTemplate {
            background-color: #B0D691 !important;
        }


        .cssHorizontalActual .rocItemTemplate {
            background-color: #FDC4C5 !important;
        }

        .cssHorizontal .rocItemTemplate {
            background-color: #CA8091 !important;
        }
    </style>

    <script type="text/javascript">

        var idPeriodo = "";
        var arr = [];
        var idEmpleado;

        function OpenPeriodoSelectionWindow() {
            idEmpleado = '<%= vIdEmpleado %>';
            OpenSelectionWindow("../Comunes/SeleccionPeriodo.aspx?m=FORMACION&IdEmpleado=" + idEmpleado, "winSeleccion", "Selección de periodo")
        }

        function OpenAnalisisWindow() {
            if (idPeriodo == "") {
                radalert("Selecciona un periodo", 300, 135);
                return;
            }

            if (arr.length == 0) {
                radalert("Selecciona puestos para comparar", 400, 135);
                return;
            }

            idEmpleado = <%= vIdEmpleado %>
            OpenSelectionWindow("VentanaPlanVidaCarrera.aspx?idPeriodo=" + idPeriodo + "&idEmpleado=" + idEmpleado + "&Puestos=" + arr, "winAnalisis", "Plan de Vida y Carrera - Análisis de Alternativas de Crecimiento")
        }

        function useDataFromChild(pDato) {
            var vLstDato = {
                idItem: "",
                nbItem: ""
            };

            if (pDato != null) {
                //SI EL pDato ES UN ENTERO ENTONCES SE CREO EL PERIODO
                var idCreado = TryParseInt(pDato, 0);
                if (idCreado !== 0) {
                    OpenConfiguracionWindow(idCreado);
                }
                else {
                    var vDatosSeleccionados = pDato[0];
                    if (vDatosSeleccionados.clTipoCatalogo == "PERIODO") {
                        vLstDato.idItem = vDatosSeleccionados.idPeriodo;
                        vLstDato.nbItem = vDatosSeleccionados.nbPeriodo;
                        list = $find("<%=lstPeriodos.ClientID %>");
                        idPeriodo = vDatosSeleccionados.idPeriodo;
                        ChangeListItem(vLstDato.idItem, vLstDato.nbItem, list);

                    }
                }
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

        function abrir(a, b) {
            if (b.checked)
                arr.push(a);
            else
                arr.splice(arr.indexOf(a), 1);
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
        }

        function OpenInsertPeriodoWindow() {
            if (arr.length == 0) {
                radalert("Selecciona puestos para comparar", 400, 135);
                return;
            }
            OpenPeriodoWindow(null);
        }

        function OpenPeriodoWindow(pIdPeriodo) {
            OpenWindow(GetPeriodoWindowProperties(pIdPeriodo));
        }

        function GetPeriodoWindowProperties(pIdPeriodo) {

            var wnd = GetWindowProperties();
            wnd.width = 750;
            wnd.height = 650;
            wnd.vTitulo = "Agregar periodo";
            wnd.vRadWindowId = "winPeriodo";
            
            var txtEvaluado = document.getElementById("<%= txtNombreEvaluado.ClientID %>");
            var nombreEvaluado = txtEvaluado.innerHTML;
                idEmpleado = '<%= vIdEmpleado %>';
            wnd.vURL = "/FYD/PeriodoEvaluacion.aspx?evaluadoPVC=" + nombreEvaluado + "&idEvaluadoPVC=" + idEmpleado + "&idsPuestosPVC=" + arr;
            return wnd;
        }

        function onCloseWindow(sender, args) {
            //Redireccionar a configurar el periodo.

        }

        function OpenConfiguracionWindow(vIdPeriodo) {
            if (vIdPeriodo != null)
                OpenWindow(GetConfiguracionWindowProperties(vIdPeriodo));
        }

        function GetConfiguracionWindowProperties(pIdPeriodo) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Configuración del periodo";
            wnd.vURL = "/FYD/ConfiguracionPeriodo.aspx?PeriodoId=" + pIdPeriodo;
            wnd.vRadWindowId = "winPeriodo";
            return wnd;
        }

        function TryParseInt(str, defaultValue) {
            if (/^\d+$/g.test(str) === true)
                return parseInt(str);

            return defaultValue;
        }

    </script>
</asp:Content>--%>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Evaluado:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtClaveEvaluado" runat="server" style="min-width: 100px;"></div>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtNombreEvaluado" runat="server" width="170" maxlength="1000" enabled="false"></div>
                </td>
            </tr>
        </table>
    </div>
    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Puesto:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtClavePuesto" runat="server" style="min-width: 100px;"></div>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtNombrePuesto" runat="server" width="170" maxlength="1000" enabled="false"></div>
                </td>
            </tr>
        </table>
    </div>

    <div class="ctrlBasico">
        <label>Periodo</label>
        <telerik:RadListBox ID="lstPeriodos" runat="server" Width="300px">
            <Items>
                <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
            </Items>
        </telerik:RadListBox>
        <telerik:RadButton runat="server" ID="btnBuscarPeriodo" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenPeriodoSelectionWindow" />
    </div>

    <div style="clear: both; height: 10px;"></div>


    <div style="clear: both;" />
    <div style="height: calc(100% - 50px);">
        <label>Ruta vertical</label>
        <div style="clear: both;" />

        <div class="ctrlBasico" style="width: 100%;">
            <div class="ctrlBasico" style="width: 50%;">
                <label>Natural</label>
                <telerik:RadOrgChart ID="rocNatural" runat="server" DisableDefaultImage="true" Orientation="Vertical" RenderMode="Lightweight" DataTextField="NB_PUESTO" DataFieldID="ID_PUESTO" DataFieldParentID="ID_PUESTO_JEFE"></telerik:RadOrgChart>
            </div>

            <div class="ctrlBasico" style="width: 50%;">
                <label>Alternativa</label>
                <telerik:RadOrgChart ID="rocAlternativa" runat="server" DisableDefaultImage="true" Orientation="Vertical" RenderMode="Lightweight" DataTextField="NB_PUESTO" DataFieldID="ID_PUESTO" DataFieldParentID="ID_PUESTO_JEFE"></telerik:RadOrgChart>
            </div>
        </div>

        <div style="clear: both;" />
        <label>Ruta horizontal</label>

        <div style="clear: both;" />
        <label>Natural</label>

        <telerik:RadOrgChart ID="rocHorizontal" runat="server" DisableDefaultImage="true" Orientation="Horizontal" RenderMode="Lightweight" DataTextField="NB_PUESTO" DataFieldID="ID_PUESTO" DataFieldParentID="ID_PUESTO_JEFE"></telerik:RadOrgChart>

    </div>

    <div style="clear: both;" />
    <div class="ctrlBasico" style="text-align: center">
        <div>
            <telerik:RadButton runat="server" ID="btnAnalisis" AutoPostBack="false" Text="Análisis" OnClientClicked="OpenAnalisisWindow"></telerik:RadButton>
            &nbsp;&nbsp;
                    <telerik:RadButton ID="btnAgregar" runat="server" Text="Crear periodo" AutoPostBack="false" OnClientClicked="OpenInsertPeriodoWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <Windows>
            <telerik:RadWindow ID="winPeriodo" runat="server" Title="Agregar/Editar periodo" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winAnalisis" runat="server" Title="Plan de Vida y Carrera - Análisis de Alternativas de Crecimiento" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEditarEmpleado" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winEditarPuesto" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winMatrizCuestionarios" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Animation="Fade" OnClientClose="returnDataToParentPopup" Modal="true" Behaviors="Close"></telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>--%>
</asp:Content>
