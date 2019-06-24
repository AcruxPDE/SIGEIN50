<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="MatrizPlaneacionCuestionarios.aspx.cs" Inherits="SIGE.WebApp.FYD.MatrizPlaneacionCuestionarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script src="../Assets/js/jquery.min.js"></script>

    <style>
        fieldset {
            height: calc(100% - 20px);
        }

        .AUTOEVALUACION {
            background-color: #CCFFFF !important;
            color: black;
            border: 1px solid black;
            min-width: 50px;
            border-radius: 5px;
            max-width: 300px;
            padding-left: 5px;
            padding-right: 5px;
        }

        .SUPERIOR {
            background-color: #CCFF66 !important;
            color: black;
            border: 1px solid black;
            min-width: 50px;
            border-radius: 5px;
            max-width: 300px;
            padding-left: 5px;
            padding-right: 5px;
        }

        .SUBORDINADO {
            background-color: #F5C9A6 !important;
            color: black;
            border: 1px solid black;
            min-width: 50px;
            border-radius: 5px;
            max-width: 300px;
            padding-left: 5px;
            padding-right: 5px;
        }

        .INTERRELACIONADO {
            background-color: whitesmoke !important;
            color: black;
            border: 1px solid black;
            min-width: 50px;
            max-width: 300px;
            border-radius: 5px;
            padding-left: 5px;
            padding-right: 5px;
        }

        .OTRO {
            background-color: #FCE981 !important;
            color: black;
            border: 1px solid black;
            min-width: 50px;
            max-width: 300px;
            border-radius: 5px;
            padding-left: 5px;
        }

        .checkc {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 100%;
        }

        .rgExpand,
        .rgCollapse {
            display: none !important;
        }
    </style>

    <script type="text/javascript" id="MyScript">

        var vOnClientBeforeClose_confirm = "¿Estás seguro que quieres salir de la pantalla? Si no se ha guardado, se perderá la selección de evaluadores.";
        var vOnClientBeforeClose_title = "Cerrar";
        var vOpenWindowAutorizarDocumento_title = "Registro y Autorización";
        var vOpenNuevoCuestionarioWindow_title = "Agregar evaluadores";
        var vconfirmarCrearCuestionarios_confirm = "¿Deseas continuar? Una vez hecho esto no podrás solicitar autorizaciones ni volver a la matriz.";

        if ('<%=vClIdioma%>' != "ES") {
            vOnClientBeforeClose_confirm = '<%=vOnClientBeforeClose_confirm%>';
            vOnClientBeforeClose_title = '<%=vOnClientBeforeClose_title%>';
            vOpenWindowAutorizarDocumento_title = '<%=vOpenWindowAutorizarDocumento_title%>';
            vOpenNuevoCuestionarioWindow_title = '<%=vOpenNuevoCuestionarioWindow_title%>';
            vconfirmarCrearCuestionarios_confirm = '<%=vconfirmarCrearCuestionarios_confirm%>';

        }



        function closeWindow() {
            GetRadWindow().close();
        }


        function OnClientBeforeClose(sender, args) {

            function confirmCallback(arg) {
                if (arg) {
                    closeWindow();
                }
            }

            radconfirm(vOnClientBeforeClose_confirm, confirmCallback, 400, 170, null, vOnClientBeforeClose_title);
        }


        function ShowAutorizarForm() {
            var idPeriodo = '<%# vIdPeriodo %>';
            OpenWindowAutorizarDocumento(idPeriodo);
        }

        function OpenWindowAutorizarDocumento(pIdPeriodo) {
            if (pIdPeriodo != null) {
                var vURL = "VentanaDocumentoAutorizar.aspx";
                vURL = vURL + "?IdPeriodo=" + pIdPeriodo;
                vTitulo = vOpenWindowAutorizarDocumento_title;
                vTipoOperacion = "&TIPO=Agregar";
            }

            OpenSelectionWindow(vURL, "winAutoriza", vTitulo);
        }



        function generateDataForParent() {
            var vCampos = [];

            var vCampo = {
                clTipoCatalogo: "PLANEACION"
            };
            vCampos.push(vCampo);

            sendDataToParent(vCampos);
        }

        function Autoevaluacion() {
            var vFgAuto = $find('<%= chkAutoevaluacion.ClientID %>').get_checked();

            if (vFgAuto) {
                SeleccionaTodos(".AE");
            }
            else {
                DeseleccionaTodos(".AE");
            }
        }

        function Superior() {
            var vFgSup = $find('<%= chkSuperior.ClientID %>').get_checked();

            if (vFgSup) {
                SeleccionaTodos(".SP");
            }
            else {
                DeseleccionaTodos(".SP");
            }
        }

        function Subordinado() {
            var vFgSub = $find('<%= chkSubordinado.ClientID %>').get_checked();

              if (vFgSub) {
                  SeleccionaTodos(".SB");
              }
              else {
                  DeseleccionaTodos(".SB");
              }
          }

          function Inter() {
              var vFgInter = $find('<%= chkInter.ClientID %>').get_checked();

            if (vFgInter) {
                SeleccionaTodos(".IN");
            }
            else {
                DeseleccionaTodos(".IN");
            }
        }

        function Otros() {
            var vFgOtros = $find('<%= chkOtro.ClientID %>').get_checked();

            if (vFgOtros) {
                SeleccionaTodos(".OT");
            }
            else {
                DeseleccionaTodos(".OT");
            }
        }

        function SeleccionaTodos(pRol) {
            $(pRol).prop('checked', true);
        }

        function DeseleccionaTodos(pRol) {
            $(pRol).prop('checked', false);
        }


        function OpenNuevoCuestionarioWindow() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 850,
                height: browserWnd.innerHeight - 60
            };

            OpenSelectionWindow("AgregarCuestionario.aspx?PeriodoId=<%= vIdPeriodo %>&AccionCerrarCl=REBIND&FgCrearCuestionarios=false", "winAgregarCuestionario", vOpenNuevoCuestionarioWindow_title, windowProperties);
        }

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


        function useDataFromChild(pDato) {
            if (pDato != null) {
            //     InsertEvaluadores();
                var ajaxManager = $find('<%= ramPlaneacion.ClientID%>');
                ajaxManager.ajaxRequest();
            }
        }

        function EncapsularSeleccion(pLstSeleccion) {
            return JSON.stringify(pLstSeleccion);
        }

        function ConfirmaCrearCuestionarios() {
            var ajaxManager = $find('<%= ramPlaneacion.ClientID%>');
            ajaxManager.ajaxRequest();
        }

        function confirmarCrearCuestionarios(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

            radconfirm(vconfirmarCrearCuestionarios_confirm, callBackFunction, 400, 170, null, "Aviso");
            args.set_cancel(true);
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpMetrizPlaneacion" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramPlaneacion" runat="server" DefaultLoadingPanelID="ralpMetrizPlaneacion" OnAjaxRequest="ramPLaneacion_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramPlaneacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCapacitacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarEmpleado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCuestionarios" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnRegistroAutorizacion" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnCrearCuestionarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="clear: both; height: 10px;"></div>

    <div class="ctrlBasico">
        <telerik:RadCheckBox runat="server" ID="chkAutoevaluacion" Text="Autoevaluación" AutoPostBack="false" OnClientCheckedChanged="Autoevaluacion"></telerik:RadCheckBox>
    </div>

    <div class="ctrlBasico">
        <telerik:RadCheckBox runat="server" ID="chkSuperior" Text="Superior" AutoPostBack="false" OnClientCheckedChanged="Superior"></telerik:RadCheckBox>
    </div>

    <div class="ctrlBasico">
        <telerik:RadCheckBox runat="server" ID="chkSubordinado" Text="Subordinado" AutoPostBack="false" OnClientCheckedChanged="Subordinado"></telerik:RadCheckBox>
    </div>

    <div class="ctrlBasico">
        <telerik:RadCheckBox runat="server" ID="chkInter" Text="Interrelacionados" AutoPostBack="false" OnClientCheckedChanged="Inter"></telerik:RadCheckBox>
    </div>

    <div class="ctrlBasico">
        <telerik:RadCheckBox runat="server" ID="chkOtro" Text="Otros" AutoPostBack="false" OnClientCheckedChanged="Otros"></telerik:RadCheckBox>
    </div>

    <div style="clear: both;"></div>

    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdCuestionarios" runat="server" Height="100%" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true" MasterTableView-TableLayout ="Fixed"
            OnNeedDataSource="grdCuestionarios_NeedDataSource"
            OnItemDataBound="grdCuestionarios_ItemDataBound"
            OnDeleteCommand="grdCuestionarios_DeleteCommand"
            OnDetailTableDataBind="grdCuestionarios_DetailTableDataBind"
            OnPreRender="grdCuestionarios_PreRender">
            <ClientSettings>
                <Scrolling UseStaticHeaders="false" AllowScroll="true"  />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_EVALUADO,ID_EMPLEADO" ClientDataKeyNames="ID_EVALUADO" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" HierarchyDefaultExpanded="true" EnableHierarchyExpandAll="false" Width="100%" TableLayout="Fixed" >
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Evaluado" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EVALUADO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="350" FilterControlWidth="230" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="350" FilterControlWidth="230" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                </Columns>
                <DetailTables>
                    <telerik:GridTableView DataKeyNames="ID_EVALUADO" Name="gtvEvaluadores">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="Autoevaluación" DataField="AUTOEVALUACION" UniqueName="AUTOEVALUACION">
                                <HeaderStyle Width="310px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Superior" DataField="SUPERIOR" UniqueName="SUPERIOR">
                                <HeaderStyle Width="310px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Subordinado" DataField="SUBORDINADO" UniqueName="SUBORDINADO">
                                <HeaderStyle Width="310px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Interrelacionados" DataField="INTERRELACIONADO" UniqueName="INTERRELACIONADO">
                                <HeaderStyle Width="310px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Otros" DataField="OTROS" UniqueName="OTROS">
                                <HeaderStyle Width="310px" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear:both; height:10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" ID="btnAgregarEmpleado" Text="Agregar evaluadores" AutoPostBack="false" OnClientClicked="OpenNuevoCuestionarioWindow" OnClick="btnAgregarEmpleado_Click"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click" ></telerik:RadButton>
    </div>
      <div class="ctrlBasico">
       <telerik:RadButton ID="btnRegistroAutorizacion" AutoPostBack="false" Enabled="true" OnClientClicked="ShowAutorizarForm" runat="server" Text="Registro y autorización" Width="200" ToolTip="Da clic si deseas registrar este programa de capacitación y/o deseas realizar un proceso de autorización."></telerik:RadButton>
     </div>
    <div class="ctrlBasico">
        <%--<telerik:RadButton runat="server" ID="btnCrearCuestionarios" Text="Crear cuestionarios" OnClick="btnCrearCuestionarios_Click"></telerik:RadButton>--%>
        <telerik:RadButton runat="server" ID="btnCrearCuestionarios" Text="Crear cuestionarios" OnClientClicking="confirmarCrearCuestionarios" Enabled="true" OnClick="btnCrearCuestionarios_Click"></telerik:RadButton>
    </div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnCerrar" Text="Cerrar" AutoPostBack="false" OnClientClicked="OnClientBeforeClose"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
