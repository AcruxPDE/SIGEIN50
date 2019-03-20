<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="Consultas.aspx.cs" Inherits="SIGE.WebApp.IDP.Consultas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var idBateria = "";
        var idCandidato = "";
        var clToken = "";
        var idSolicitud = "";

        function obtenerIdFila() {
            var grid = $find("<%=grdCandidatos.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                idBateria = row.getDataKeyValue("ID_BATERIA");
                idCandidato = row.getDataKeyValue("ID_CANDIDATO");
                clToken = row.getDataKeyValue("CL_TOKEN");
                idSolicitud = row.getDataKeyValue("ID_SOLICITUD");
            }
            else {
                idBateria = "";
                idCandidato = "";
                clToken = "";
                idSolicitud = "";
            }
        }

        function OpenResultadosPruebas() {
            obtenerIdFila();
            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            if (idBateria != "") {
                openChildDialog("ResultadosPruebas.aspx?ID=" + idBateria + "&&T=" + clToken, "winConsultas", "Resultados pruebas", windowProperties);
            }
            else { radalert("Selecciona un candidato.", 400, 150, ""); }
        }

        function OpenConsultaResumida() {
            obtenerIdFila();
            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            if (idBateria != "" && idSolicitud != "") {
                openChildDialog("ConsultasPersonales.aspx?pIdBateria=" + idBateria + "&pClTipoConsulta=RESUMIDA", "winConsultas", "Consulta personal resumida", windowProperties);
            }
            else { radalert("Selecciona un candidato.", 400, 150, ""); }
        }

        function OpenConsultaDetallada() {
            obtenerIdFila();
            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            if (idBateria != "" && idSolicitud != "") {
                openChildDialog("ConsultasPersonales.aspx?pIdBateria=" + idBateria + "&pClTipoConsulta=DETALLADA", "winConsultas", "Consulta personal detallada", windowProperties);
            }
            else { radalert("Selecciona un candidato.", 400, 150, ""); }
        }

        function OpenConsultaGlobal() {
            obtenerIdFila();
            var windowProperties = {
                width: 600,
                height: 300
            };
            if (idCandidato != "") {
                openChildDialog("ConsultasComparativas.aspx?pIdCandidato=" + idCandidato + "&pClTipoConsulta=GLOBAL", "winConsultas", "Consulta comparativa global", windowProperties);
            }
            else { radalert("Selecciona un candidato.", 400, 150, ""); }
        }

        function OpenConsultaPuestoPersonas() {
            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            //if ('<= vIdPuestoVsCandidatos >' != null)
            var vCandidatos = [];
            var vCandidatosJson = "";
            var grid = $find("<%=grdCandidatos.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length > 0) {
                for (i = 0; i < selectedRows.length; i++) {
                    selectedItem = selectedRows[i];
                        var vCandidato = {
                            ID_CANDIDATO: selectedItem.getDataKeyValue("ID_CANDIDATO"),
                        }

                        vCandidatos.push(vCandidato);                  
                }

                vCandidatosJson = JSON.stringify(vCandidatos);


                openChildDialog("ConsultasComparativas.aspx?pClTipoConsulta=PSVP&candidatos=" + vCandidatosJson, "winConsultas", "Consulta comparativa Puesto vs N. Personas", windowProperties);
            }
            else { radalert("Selecciona un candidato.", 400, 150, ""); }
        }

        function OpenConsultaPersonaPuestos() {
            obtenerIdFila();
            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            if (idCandidato != "")
                openChildDialog("ConsultasComparativas.aspx?pIdCandidato=" + idCandidato + "&pClTipoConsulta=PVPS", "winConsultas", "Consulta comparativa Personas vs N. Puestos", windowProperties);
            else { radalert("Selecciona un candidato.", 400, 150, ""); }
        }

        function OpenEvaluacionIntegral() {
            obtenerIdFila();
            var vURL = "VentanaProcesoSeleccionCandidato.aspx?pClTipoConsulta=CONSULTAR";
            var vTitulo = "Consulta evaluación integral";

            if (idCandidato != "") {
                vURL = vURL + "&IdCandidato=" + idCandidato

                if (idBateria != "") {
                    vURL = vURL + "&IdBateria=" + idBateria;
                }

                if (clToken != "") {
                    vURL = vURL + "&ClToken=" + clToken;
                }

                vURL = vURL + "&SolicitudId=" + idSolicitud;

                var windowProperties = {};
                windowProperties.width = 1100;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog(vURL, "winConsultas", vTitulo, windowProperties);
            }
            else { radalert("Selecciona un candidato.", 400, 150, ""); }
        }

        function RowSelecting(sender, eventArgs) {

            var btnResultados = $find('<%= btnResultados.ClientID %>');
            var btPersonalResumida = $find('<%= btnPersonalResumida.ClientID %>');
            var btnPersonalDetallada = $find('<%= btnPersonalDetallada.ClientID %>');
            var btnPuestoPersonas = $find('<%= btnPuestoPersonas.ClientID %>');
            var btnPersonaPuestos = $find('<%= btnPersonaPuestos.ClientID %>');
            var btnConsultaGlobal = $find('<%= btnConsultaGlobal.ClientID %>');
            var btnIntegral = $find('<%= btnIntegral.ClientID %>');

            var tableView = eventArgs.get_tableView();

            if (tableView.get_selectedItems().length > 0) {
                btnResultados.set_enabled(false);
                btPersonalResumida.set_enabled(false);
                btnPersonalDetallada.set_enabled(false);

                if ('<%= vbtnPuestoPersonas %>' == "True")
                btnPuestoPersonas.set_enabled(true);

                btnPersonaPuestos.set_enabled(false);
                btnConsultaGlobal.set_enabled(false);
                btnIntegral.set_enabled(false);
            }
            else {
                if ('<%= vbtnResultados %>' == "True")
                    btnResultados.set_enabled(true);
                if ('<%= vbtnPersonalResumida %>' == "True")
                    btPersonalResumida.set_enabled(true);
                if ('<%= vbtnPersonalDetallada %>' == "True")
                    btnPersonalDetallada.set_enabled(true);
                if ('<%= vbtnPuestoPersonas %>' == "True")
                    btnPuestoPersonas.set_enabled(true);
                if ('<%= vbtnPersonaPuestos %>' == "True")
                    btnPersonaPuestos.set_enabled(true);
                if ('<%= vbtnConsultaGlobal %>' == "True")
                    btnConsultaGlobal.set_enabled(true);
                if ('<%= vbtnIntegral %>' == "True")
                btnIntegral.set_enabled(true);
            }
        }

        function RowDeselected(sender, eventArgs) {
            var btnResultados = $find('<%= btnResultados.ClientID %>');
            var btPersonalResumida = $find('<%= btnPersonalResumida.ClientID %>');
            var btnPersonalDetallada = $find('<%= btnPersonalDetallada.ClientID %>');
            var btnPuestoPersonas = $find('<%= btnPuestoPersonas.ClientID %>');
            var btnPersonaPuestos = $find('<%= btnPersonaPuestos.ClientID %>');
            var btnConsultaGlobal = $find('<%= btnConsultaGlobal.ClientID %>');
            var btnIntegral = $find('<%= btnIntegral.ClientID %>');

            var tableView = eventArgs.get_tableView();

            if (tableView.get_selectedItems().length > 0) {
                btnResultados.set_enabled(false);
                btPersonalResumida.set_enabled(false);
                btnPersonalDetallada.set_enabled(false);
                if ('<%= vbtnPuestoPersonas %>' == "True")
                    btnPuestoPersonas.set_enabled(true);

                btnPersonaPuestos.set_enabled(false);
                btnConsultaGlobal.set_enabled(false);
                btnIntegral.set_enabled(false);
            }
            else {
                if ('<%= vbtnResultados %>' == "True")
                    btnResultados.set_enabled(true);
                if ('<%= vbtnPersonalResumida %>' == "True")
                    btPersonalResumida.set_enabled(true);
                if ('<%= vbtnPersonalDetallada %>' == "True")
                    btnPersonalDetallada.set_enabled(true);
                if ('<%= vbtnPuestoPersonas %>' == "True")
                    btnPuestoPersonas.set_enabled(true);
                if ('<%= vbtnPersonaPuestos %>' == "True")
                    btnPersonaPuestos.set_enabled(true);
                if ('<%= vbtnConsultaGlobal %>' == "True")
                    btnConsultaGlobal.set_enabled(true);
                if ('<%= vbtnIntegral %>' == "True")
                    btnIntegral.set_enabled(true);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label id="lbTitulo" class="labelTitulo">Candidatos</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid
            ID="grdCandidatos"
            runat="server"
            Height="100%"
            AutoGenerateColumns="false"
            EnableHeaderContextMenu="true"
            AllowSorting="true"
            HeaderStyle-Font-Bold="true"
            AllowMultiRowSelection="true"
            OnNeedDataSource="grdCandidatos_NeedDataSource"
            OnItemDataBound="grdCandidatos_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowSelecting="RowSelecting" OnRowDeselected="RowDeselected" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_SOLICITUD,ID_CANDIDATO,ID_BATERIA, CL_TOKEN" EnableColumnsViewState="false" DataKeyNames="ID_SOLICITUD,ID_BATERIA" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" Name="Candidatos">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35" HeaderText="Sel."></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="120" FilterControlWidth="50" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Nombre completo" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="90" HeaderText="Fecha de solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD"  DataType="System.DateTime"></telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estatus del proceso" DataField="CL_SOLICITUD_ESTATUS" UniqueName="CL_SOLICITUD_ESTATUS"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="20" HeaderText="Pruebas terminadas" DataField="FG_PRUEBAS_TERMINADAS" UniqueName="FG_PRUEBAS_TERMINADAS"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="20" HeaderText="Entrevista" DataField="FG_ENTREVISTA" UniqueName="FG_ENTREVISTA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="100" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="M_FE_MODIFICA" UniqueName="M_FE_MODIFICA" HeaderStyle-Width="150" FilterControlWidth="100" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Resultados de pruebas" ID="btnResultados" AutoPostBack="false" OnClientClicked="OpenResultadosPruebas" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Personal resumida" ID="btnPersonalResumida" AutoPostBack="false" OnClientClicked="OpenConsultaResumida" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Personal detallada" ID="btnPersonalDetallada" AutoPostBack="false" OnClientClicked="OpenConsultaDetallada" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Puesto vs N. personas" ID="btnPuestoPersonas" AutoPostBack="false" OnClientClicked="OpenConsultaPuestoPersonas" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Persona vs N. puestos" ID="btnPersonaPuestos" AutoPostBack="false" OnClientClicked="OpenConsultaPersonaPuestos" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Consulta global" ID="btnConsultaGlobal" AutoPostBack="false" OnClientClicked="OpenConsultaGlobal" />
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" Text="Evaluación integral" ID="btnIntegral" AutoPostBack="false" OnClientClicked="OpenEvaluacionIntegral" />
    </div>
    <asp:HiddenField runat="server" ID="hfSelectedRow" />
    <telerik:RadWindowManager ID="rwMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="rwComentarios" runat="server" Title="Comentarios de entrevistas" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwProcesoSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" ReloadOnShow="true" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winEntrevista" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winReferencia" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="rwConsulta" runat="server" VisibleStatusbar="false" VisibleTitlebar="true" ShowContentDuringLoad="false" Modal="true" ReloadOnShow="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winReportes" runat="server" VisibleStatusbar="false" VisibleTitlebar="true" ShowContentDuringLoad="false" Modal="true" ReloadOnShow="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winImprimir" runat="server" Title="Imprimir" Height="630px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" Animation="Fade"></telerik:RadWindow>
            <telerik:RadWindow ID="winConsultas" runat="server" VisibleStatusbar="false" VisibleTitlebar="true" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" VisibleStatusbar="false" VisibleTitlebar="true" ShowContentDuringLoad="false" Modal="true" ReloadOnShow="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
