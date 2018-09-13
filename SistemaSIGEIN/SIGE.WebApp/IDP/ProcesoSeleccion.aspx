<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="ProcesoSeleccion.aspx.cs" Inherits="SIGE.WebApp.IDP.ProcesoSeleccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
      <style>
       .ruBrowse
       {
           
           width: 150px !important;
       }

        .RadButton.rbSkinnedButton.uncheckedYes {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedYes > .rbDecorated {
                color: #eee !important;
            }

        .RadButton.rbSkinnedButton.checkedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.checkedNo > .rbDecorated {
                color: #333 !important;
            }

        .RadButton.rbSkinnedButton.uncheckedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedNo > .rbDecorated {
                color: #eee !important;
            }

        .checkContainer {
            border-radius: 5px;
            border: 1px solid lightgray;
            background: #eee;
        }
    </style>
    <script type="text/javascript">

        var idEntrevista = "";
        var idExperiencia = "";
        var idCandidato = "";

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }


        function onCloseWindow() {

        }

        function TabSelected() {
            var vtabStrip = $find("<%= tbProcesoSeleccion.ClientID%>").get_selectedIndex();
            console.info(vtabStrip);

            var divEstatus
            var divEntrevistas
            var divReferencias
            var divPuestoComp
            var divPruebas
            var divMedico
            var divSocioeconomico
            var divDocumentacion
            var divBitacora

            divEstatus = document.getElementById('<%= divEstatus.ClientID%>');
            divEntrevistas = document.getElementById('<%= divEntrevistas.ClientID%>');
            divReferencias = document.getElementById('<%= divReferencias.ClientID%>');
            divPuestoComp = document.getElementById('<%= divPuestoComp.ClientID%>');
            divPruebas = document.getElementById('<%= divPruebas.ClientID%>');
            divMedico = document.getElementById('<%= divMedico.ClientID%>');
            divSocioeconomico = document.getElementById('<%= divSocioeconomico.ClientID%>');
            divDocumentacion = document.getElementById('<%= divDocumentacion.ClientID%>');
            divBitacora = document.getElementById('<%= divBitacora.ClientID%>');

            switch (vtabStrip) {

                case 0:
                    divEstatus.style.display = 'block';
                    divEntrevistas.style.display = 'none';
                    divReferencias.style.display = 'none';
                    divPuestoComp.style.display = 'none';
                    divPruebas.style.display = 'none';
                    divMedico.style.display = 'none';
                    divSocioeconomico.style.display = 'none';
                    divDocumentacion.style.display = 'none';
                    divBitacora.style.display = 'none';
                    break;
                case 1:
                    divEstatus.style.display = 'none';
                    divEntrevistas.style.display = 'block';
                    divReferencias.style.display = 'none';
                    divPuestoComp.style.display = 'none';
                    divPruebas.style.display = 'none';
                    divMedico.style.display = 'none';
                    divSocioeconomico.style.display = 'none';
                    divDocumentacion.style.display = 'none';
                    divBitacora.style.display = 'none';
                    break;
                case 2:
                    divEstatus.style.display = 'none';
                    divEntrevistas.style.display = 'none';
                    divReferencias.style.display = 'block';
                    divPuestoComp.style.display = 'none';
                    divPruebas.style.display = 'none';
                    divMedico.style.display = 'none';
                    divSocioeconomico.style.display = 'none';
                    divDocumentacion.style.display = 'none';
                    divBitacora.style.display = 'none';
                    break;
                case 3:
                    divEstatus.style.display = 'none';
                    divEntrevistas.style.display = 'none';
                    divReferencias.style.display = 'none';
                    divPuestoComp.style.display = 'block';
                    divPruebas.style.display = 'none';
                    divMedico.style.display = 'none';
                    divSocioeconomico.style.display = 'none';
                    divDocumentacion.style.display = 'none';
                    divBitacora.style.display = 'none';
                    break;
                case 4:
                    divEstatus.style.display = 'none';
                    divEntrevistas.style.display = 'none';
                    divReferencias.style.display = 'none';
                    divPuestoComp.style.display = 'none';
                    divPruebas.style.display = 'block';
                    divMedico.style.display = 'none';
                    divSocioeconomico.style.display = 'none';
                    divDocumentacion.style.display = 'none';
                    divBitacora.style.display = 'none';
                    break;
                case 5:
                    divEstatus.style.display = 'none';
                    divEntrevistas.style.display = 'none';
                    divReferencias.style.display = 'none';
                    divPuestoComp.style.display = 'none';
                    divPruebas.style.display = 'none';
                    divMedico.style.display = 'block';
                    divSocioeconomico.style.display = 'none';
                    divDocumentacion.style.display = 'none';
                    divBitacora.style.display = 'none';
                    break;
                case 6:
                    divEstatus.style.display = 'none';
                    divEntrevistas.style.display = 'none';
                    divReferencias.style.display = 'none';
                    divPuestoComp.style.display = 'none';
                    divPruebas.style.display = 'none';
                    divMedico.style.display = 'none';
                    divSocioeconomico.style.display = 'block';
                    divDocumentacion.style.display = 'none';
                    divBitacora.style.display = 'none';
                    break;
                case 7:
                    divEstatus.style.display = 'none';
                    divEntrevistas.style.display = 'none';
                    divReferencias.style.display = 'none';
                    divPuestoComp.style.display = 'none';
                    divPruebas.style.display = 'none';
                    divMedico.style.display = 'none';
                    divSocioeconomico.style.display = 'none';
                    divDocumentacion.style.display = 'block';
                    divBitacora.style.display = 'none';
                    break;
                case 8:
                    divEstatus.style.display = 'none';
                    divEntrevistas.style.display = 'none';
                    divReferencias.style.display = 'none';
                    divPuestoComp.style.display = 'none';
                    divPruebas.style.display = 'none';
                    divMedico.style.display = 'none';
                    divSocioeconomico.style.display = 'none';
                    divDocumentacion.style.display = 'none';
                    divBitacora.style.display = 'block';
                    break;
            }
        }

        function CopiarDatos(sender, args) {
            confirmAction(sender, args, "Si hay información será remplazada. ¿Desea hacer la copia?");
        }

        function ConfirmarCopiarDatos(sender, args) {

            var grid = $find("<%=grdProcesoSeleccion.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            var vIsProcesoSeleccion;

            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                vIdProcesoSeleccion = SelectDataItem.getDataKeyValue("ID_PROCESO_SELECCION");

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('¿Deseas copiar la información del proceso de evaluación seleccionado al actual?', callBackFunction, 400, 150, null, "Copiar información");
                args.set_cancel(true);

            }
            else {
                radalert("Seleccione el proceso de evaluación del cual se va a copiar la información", 400, 170, "");
                args.set_cancel(true);
            }
        }


        function RecargarGridEntrevistas() {
            idEntrevista = "";
            var vGrid = $find("<%=rgEntrevistas.ClientID %>");
            var vMasterTable = vGrid.get_masterTableView();
            vMasterTable.rebind();
        }

        function closeWindow() {
            GetRadWindow().close();
        }

        function GetWindowProperties() {

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            return windowProperties;
        }

        function GetWindowPropertiesReferencia() {

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: 1000,
                height: 610
            };

            return windowProperties;
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function ObtenerEntrevista() {
            var grid = $find("<%=rgEntrevistas.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idEntrevista = SelectDataItem.getDataKeyValue("ID_ENTREVISTA");
            }
        }

        function ObtenerExperiencia() {
            var grid = $find("<%=grdReferencias.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idExperiencia = SelectDataItem.getDataKeyValue("ID_EXPERIENCIA_LABORAL");
                idCandidato = SelectDataItem.getDataKeyValue("ID_CANDIDATO");
            }
        }

        function useDataFromChild(pDato) {
            var list;
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {
                    case "ENTREVISTA":
                        RecargarGridEntrevistas();
                        break;
                    case "ESTADO":
                        list = $find("<%= rlbEstado.ClientID %>");
                        SetListBoxItem(list, pDato[0].nbDato, pDato[0].clDato);
                        break;
                    case "MUNICIPIO":
                        list = $find("<%= rlbMunicipios.ClientID %>");
                        SetListBoxItem(list, pDato[0].nbDato, pDato[0].clDato);
                        break;
                    case "COLONIA":
                        list = $find("<%= rlbColonia.ClientID %>");
                        SetListBoxItem(list, pDato[0].nbDato, pDato[0].clDato);
                        break;
                    case "REFERENCIA":
                        var vGrid = $find("<%=grdReferencias.ClientID %>");
                        var vMasterTable = vGrid.get_masterTableView();
                        vMasterTable.rebind();
                        break;
                }
            }
        }

        function SetListBoxItem(list, text, value) {
            if (list != undefined) {
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(text);
                item.set_value(value);
                item.set_selected(true);
                items.add(item);

                list.commitChanges();
            }
        }

        function EliminarSeleccionLista(sender, args) {
            var vLstEstados = $find("<%= rlbEstado.ClientID %>");
            var vBtnEstados = $find("<%= btnEliminarEstado.ClientID %>");
            var vLstMunicipios = $find("<%= rlbMunicipios.ClientID %>");
            var vBtnMunicipios = $find("<%= btnEliminaMunicipio.ClientID %>");
            var vLstColonia = $find("<%= rlbColonia.ClientID %>");
            var vBtnColonia = $find("<%= btnEliminaColonia.ClientID %>");

            if (sender == vBtnEstados) {
                SetListBoxItem(vLstEstados, "No Seleccionado", "");
            }

            if (sender == vBtnMunicipios) {
                SetListBoxItem(vLstMunicipios, "No Seleccionado", "");
            }

            if (sender == vBtnColonia) {
                SetListBoxItem(vLstColonia, "No Seleccionado", "");
            }
        }

        function GetEntrevistaWindowProperties(pIdEntrevista) {
            var wnd = GetWindowProperties();
            var idProcesoSeleccion = '<%# vIdProcesoSeleccion %>';

            wnd.width = 700;
            wnd.vTitulo = "Agregar entrevista";
            wnd.vRadWindowId = "winEntrevista";
            wnd.vURL = "VentanaProcesoSeleccionEntrevista.aspx?IdProcesoSeleccion=" + idProcesoSeleccion;
            if (pIdEntrevista != null) {
                wnd.vURL += String.format("&IdEntrevista={0}", pIdEntrevista);
                wnd.vTitulo = "Editar entrevista";
            }
            return wnd;
        }

        function GetReferenciaWindowProperties(pIdExperiencia, pIdCandiadto) {
            var wnd = GetWindowPropertiesReferencia();
            wnd.width = 950;
            wnd.vRadWindowId = "winEntrevista";
            if (pIdExperiencia != "" & pIdCandiadto != "") {
                wnd.vURL = "VentanaProcesoSeleccionReferencias.aspx?IdExperiencia=" + pIdExperiencia + "&idCandidato=" + pIdCandiadto;
                wnd.vTitulo = "Modificar referencia";
            }
            return wnd;
        }

        function OpenNuevaEntrevistaWindow(sender, args) {
            OpenWindow(GetEntrevistaWindowProperties(null));
        }

        function OpenEditarEntrevistaWindow(sender, args) {
            ObtenerEntrevista();

            if (idEntrevista != "")
                OpenWindow(GetEntrevistaWindowProperties(idEntrevista));
            else
                radalert("Selecciona una entrevista.", 400, 150);
        }

        function ShowPopupEditarExperiencia(sender, args) {
            ObtenerExperiencia();

            if (idExperiencia != "" & idCandidato != "")
                OpenWindow(GetReferenciaWindowProperties(idExperiencia, idCandidato));
            else
                radalert("Selecciona un registro.", 400, 150);
        }

        function ConfirmarEliminarEntrevista(sender, args) {

            ObtenerEntrevista();

            if (idEntrevista != "") {
                confirmAction(sender, args, '¿Deseas eliminar la entrevista seleccionada?');
            } else {
                radalert("Seleccione una entrevista.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function LimpiarDatosExamenMedico(sender, args) {

                var chkEnfermedad = $find('<%# chkEMEnfermedadSi.ClientID %>');
                var txtEnfermedad = $find('<%# txtEmEnfermedadComentario.ClientID %>');

                var chkMedicamentos = $find('<%# chkEMMedicamentosSi.ClientID %>');
                var txtMedicamentos = $find('<%# txtEMMedicamentosComentarios.ClientID %>');

                var chkAlergias = $find('<%# chkEMAlergiasSi.ClientID %>');
                var txtAlergias = $find('<%# txtEMAlergiasComentarios.ClientID %>');

                var chkAntecedentes = $find('<%# chkEMAntecedentesSi.ClientID %>');
                var txtAntecedentes = $find('<%# txtEMAntecedentesComentarios.ClientID %>');

                var chkIntervenciones = $find('<%# chkEMCirujiasSi.ClientID %>');
                var txtIntervenciones = $find('<%# txtEMCirujiasComentarios.ClientID %>');

                if (!chkEnfermedad.get_checked() & chkEnfermedad == sender) {
                    txtEnfermedad.set_value("");
                }

                if (!chkMedicamentos.get_checked() & chkMedicamentos == sender) {
                    txtMedicamentos.set_value("");
                }

                if (!chkAlergias.get_checked() & chkAlergias == sender) {
                    txtAlergias.set_value("");
                }

                if (!chkAntecedentes.get_checked() & chkAntecedentes == sender) {
                    txtAntecedentes.set_value("");
                }

                if (!chkIntervenciones.get_checked() & chkIntervenciones == sender) {
                    txtIntervenciones.set_value("");

            }
        }

        function OpenSelectionWindow(sender, args) {
            var vLstEstados = $find("<%= rlbEstado.ClientID %>");
            var vBtnEstados = $find("<%= btnSeleccionarEstado.ClientID %>");
            var vLstMunicipios = $find("<%= rlbMunicipios.ClientID %>");
            var vBtnMunicipios = $find("<%= btnSeleccionaMunicipio.ClientID %>");
            var vLstColonia = $find("<%= rlbColonia.ClientID %>");
            var vBtnColonia = $find("<%= btnBuscarColonia.ClientID %>");

            if (sender == vBtnEstados)
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionEstado.aspx", "winSeleccion", "Selección de estado");

            if (sender == vBtnMunicipios) {
                var clEstado = vLstEstados.get_selectedItem().get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionMunicipio.aspx?ClEstado=" + clEstado, "winSeleccion", "Selección de municipio");
            }

            if (sender == vBtnColonia) {
                var clEstado = vLstEstados.get_selectedItem().get_value();
                var clMunicipio = vLstMunicipios.get_selectedItem().get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionColonia.aspx?ClEstado=" + clEstado + "&ClMunicipio=" + clMunicipio, "winSeleccion", "Selección de colonia");
            }
        }

        function openResultadoPruebas() {
            var IdBateria = '<%=vIdBateria%>';
            if (IdBateria != "") {
                var win = window.open("ResultadosPruebas.aspx?&ID=" + IdBateria + "&T=" + '<%=vClToken%>', '_blank');
                win.focus();
            }
            else {
                radalert("El candidato no tiene pruebas.", 400, 150, "");
                return;
            }
        }

        function OpenComentariosEntrevista() {
            var vidCandidato = '<%=vIdCandidato%> ';
            var idProcesoSeleccion = '<%# vIdProcesoSeleccion %>';
            OpenSelectionWindowEntrevista("VentanaComentariosEntrevista.aspx?IdCandidato=" + vidCandidato + "&IdProcesoSeleccion=" + idProcesoSeleccion, "rwComentarios", "Comentarios de entrevistas")

        }
        function OpenSelectionWindowEntrevista(pURL, pIdWindow, pTitle, pWindowProperties) {
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

        function generateDataForParent() {
            var vCampos = [];

            var vCampo = {
                clTipoCatalogo: "ACTUALIZAR"
            };
            vCampos.push(vCampo);

            sendDataToParent(vCampos);
        }

        function OpenPreview() {
            var vURL = "../Administracion/VentanaVistaDescriptivo.aspx";
            var vTitulo = "Vista previa descriptivo";
            var vIdPuesto = '<%# vIdPuesto %>';

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            if (vIdPuesto != "") {
                vURL = vURL + "?PuestoId=" + vIdPuesto;
                var wnd = openChildDialog(vURL, "rwComentarios", vTitulo, windowProperties);
            }

        }
    </script>

    <style>
        .MostrarCelda {
            border: 1px solid lightgray;
            border-radius: 5px;
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
<%--    <telerik:RadToolTipManager ID="rtmEvaluacion" runat="server" AutoTooltipify="true" BackColor="WhiteSmoke" RelativeTo="Element"
        AutoCloseDelay="60000" Position="TopCenter" Width="300" HideEvent="LeaveTargetAndToolTip">
    </telerik:RadToolTipManager>--%>
    <telerik:RadAjaxLoadingPanel ID="ralpEntrevista" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramEntrevista" runat="server" DefaultLoadingPanelID="ralpEntrevista" OnAjaxRequest="ramEntrevista_AjaxRequest">
     <AjaxSettings>
             <%--  <telerik:AjaxSetting AjaxControlID="ramEntrevista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEntrevistas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEntrevistas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEntrevistas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarEntrevista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEntrevistas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEnvioCorreos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEntrevistas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEnviarTodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEntrevistas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnCopiarDatosProceso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkEMEnfermedadSi" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkEMMedicamentosSi" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkEMAlergiasSi" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkEMAntecedentesSi" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkEMCirujiasSi" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="chkEMAdecuadoSi" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmEnfermedadComentario" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMMedicamentosComentarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMAlergiasComentarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMAntecedentesComentarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMCirujiasComentarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="reObservaciones" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMPeso" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMMasaCorporal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMPulso" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMPresionArterial" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMEmbarazos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMHijos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="cmbEstadoCivil" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESrfc" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESCurp" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESSeguroSocial" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbEstado" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbMunicipios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbColonia" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESCalle" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESNoExt" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESNoInt" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESTelefono" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESTelefonoMovil" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESCodigoPostal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEsTiempoResidencia" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESTipoSanguineo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESIdentificacionFolio" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="cmbESServiciosMedicos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtESServiciosMedicosComentarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLEmpresa" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLDomicilio" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLEstado" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLMunicipio" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLCodigoPostal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLSueldoInicial" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLUltimoSueldo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="cmbDLTipoEmpresa" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rcmbTipoSuledo" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLAntiguedad" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLEspecificarTipoEmpresa" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdDatosFamiliares" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAnt">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvCompetencias" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divColorClas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtNbClasificacion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDsSignificado" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNext">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvCompetencias" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divColorClas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtNbClasificacion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtDsSignificado" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
      <%--  <telerik:AjaxSetting AjaxControlID="rdpFechaDependiente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEdadDependiente" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
           <%-- <telerik:AjaxSetting AjaxControlID="dtpESNacimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEMedad" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDatosFamiliares">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDatosFamiliares" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarEstudio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarESDatosLaborales">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarESDatosEconomicos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarESDatosVivienda">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rdgRegistroSolicitud">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdgRegistroSolicitud" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                 <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                     <telerik:AjaxUpdatedControl ControlID="rauDocumento" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                     <telerik:AjaxUpdatedControl ControlID="cmbTipoDocumento" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
<%--             <telerik:AjaxSetting AjaxControlID="btnDelDocumentos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
          <telerik:AjaxSetting AjaxControlID="mpgProcesoSeleccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mpgProcesoSeleccion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Folio de solicitud: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span runat="server" id="txtFolio" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Nombre del candidato: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span runat="server" id="txtCandidato" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Clave de requisición: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span runat="server" id="txtClaveRequisicion" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Puesto de la requisición: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <%--<span runat="server" id="txtPuestoAplicar" href="javascript:OpenPreview();" style="width: 300px;"></span>--%>
                    <a runat="server" id="txtPuestoAplicar" href="javascript:OpenPreview();" style="width: 300px;"></a>
                </td>
            </tr>
            <tr>
        <td class="ctrlTableDataContext">
                    <label>Fecha de inicio: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span runat="server" id="txtFechaInicio" style="width: 100px;"></span>
                </td>
                 <td class="ctrlTableDataContext" id="lbTerminoc" runat="server" visible="false">
                    <label visible =" true">Fecha de termino: </label>
                </td>
                <td class="ctrlTableDataBorderContext" id="txtFechaTerminoc" visible="false" runat="server">
                    <span runat="server" id="txtFechaTermino" visible="true" style="width: 100px;"></span>
                </td>
            </tr>
        </table>

    </div>

<%--    <div style="clear: both; height: 5px"></div>--%>

    <div style="height: calc(100% - 90px);">
        <telerik:RadSplitter ID="rsSolicitud" Width="100%" Height="100%" BorderSize="0" runat="server">

            <telerik:RadPane ID="rpBotones" runat="server" Width="220px" Height="100%">
                <telerik:RadTabStrip ID="tbProcesoSeleccion" runat="server" Align="Right" SelectedIndex="0" OnClientTabSelected="TabSelected" Width="100%" MultiPageID="mpgProcesoSeleccion" Orientation="VerticalLeft" CssClass="divControlDerecha">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Estatus del participante"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Registro de entrevistas"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Referencias"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Competencias del puesto" Visible="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Aplicación de pruebas" Visible="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Estudio socioeconómico"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Resultados médicos"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Documentación del candidato"></telerik:RadTab>
                      <%--  <telerik:RadTab runat="server" Text="Registro de cambios a la solicitud" Visible="false"></telerik:RadTab>--%>
                    </Tabs>
                </telerik:RadTabStrip>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="rsbResultadoPruebas" runat="server" Width="100%" CollapseMode="Forward" EnableResize="false"></telerik:RadSplitBar>
            <telerik:RadPane ID="rpSolicitud" runat="server">
                <%--<div style="height: calc(100% - 50px); border: 1px solid blue;">--%>
                    <telerik:RadMultiPage ID="mpgProcesoSeleccion" runat="server" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="EstatusParticipante" runat="server">
                            <div class="ctrlBasico" style="height: calc(100% - 10px); width: 60%;">
                                <telerik:RadGrid ID="grdProcesoSeleccion" HeaderStyle-Font-Bold="true" ShowHeader="true" runat="server" AllowPaging="false"
                                    AllowSorting="true" GroupPanelPosition="Top" Width="100%" GridLines="None"
                                    Height="100%" AllowFilteringByColumn="false" ClientSettings-EnablePostBackOnRowClick="false"
                                    OnNeedDataSource="grdProcesoSeleccion_NeedDataSource">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings >
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <MasterTableView ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"  PageSize="20" CommandItemDisplay="None" HorizontalAlign="NotSet" EditMode="EditForms"
                                        ClientDataKeyNames="ID_REQUISICION, ID_PROCESO_SELECCION" DataKeyNames="ID_PROCESO_SELECCION">
                                        <Columns>
                                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35">
                                            <ItemStyle Width="35px" />
                                            </telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de requisición" DataField="NO_REQUISICION" UniqueName="NO_REQUISICION" HeaderStyle-Width="100" FilterControlWidth="50"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto establecido en la requisición" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="200" FilterControlWidth="70"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de inicio" DataField="FE_INICIO_PROCESO" UniqueName="FE_INICIO_PROCESO" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de termino" DataField="FE_TERMINO_PROCESO" UniqueName="FE_TERMINO_PROCESO" HeaderStyle-Width="150" FilterControlWidth="80" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="110" FilterControlWidth="70"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="" DataField="FG_PROCESO_SELECCION_ACTUAL" UniqueName="FG_PROCESO_SELECCION_ACTUAL" HeaderStyle-Width="200" FilterControlWidth="70"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Observaciones" DataField="DS_OBSERVACIONES_TERMINO_PROCESO" UniqueName="DS_OBSERVACIONES_TERMINO_PROCESO" HeaderStyle-Width="200" FilterControlWidth="70"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                
                            </div>

                            <div class="ctrlBasico" style="height: 100%; width: 40%;">

                                <label>Datos a copiar:</label>

                                <div style="height:5px; clear:both;"></div>

                                <div class="ctrlBasico">
                                    <telerik:RadCheckBox runat="server" ID="chkEntrevista" AutoPostBack="false" Text="Entrevistas"></telerik:RadCheckBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadCheckBox runat="server" ID="chkSocioeconomico" AutoPostBack="false" Text="Est. Socioeconomico"></telerik:RadCheckBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadCheckBox runat="server" ID="chkMedico" AutoPostBack="false" Text="Res. Medicos"></telerik:RadCheckBox>
                                </div>

                                <div style="height:5px; clear:both;"></div>

                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnCopiarDatosProceso" Text="Copiar datos de otro proceso de evaluación" OnClientClicking="ConfirmarCopiarDatos" OnClick="btnCopiarDatosProceso_Click"></telerik:RadButton>
                                </div>
                                <div style="height: 10px; clear: both;"></div>
                                <label>Observaciones del proceso:</label>

                                <div style="height: 10px; clear: both;"></div>

                                <telerik:RadEditor Height="200px" Width="100%" ToolsWidth="310px" EditModes="Design" ID="reOBservacionProceso" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/BasicTools.xml"></telerik:RadEditor>

                                <div style="height: 10px; clear: both;"></div>

                                <div class="divControlDerecha">
                                    <telerik:RadButton ID="btnTerminarProceso" runat="server" Text="Terminar proceso de evaluación" UseSubmitBehavior="false" OnClick="btnTerminarProceso_Click"></telerik:RadButton>
                                </div>

                            </div>

                        </telerik:RadPageView>

                        <telerik:RadPageView ID="RegistroEntrevistas" runat="server">

                            <div style="height: calc(100% - 50px);">
                                <telerik:RadGrid runat="server" ID="rgEntrevistas" HeaderStyle-Font-Bold="true"  AutoGenerateColumns="false" Width="100%" Height="100%" OnNeedDataSource="rgEntrevistas_NeedDataSource" ShowHeader="true" AllowFilteringByColumn="true">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="false" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView ClientDataKeyNames="ID_ENTREVISTA" DataKeyNames="ID_ENTREVISTA,FL_ENTREVISTA,CL_TOKEN" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridClientSelectColumn Exportable="false">
                                                <ItemStyle Width="50px" />
                                            </telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="140px" DataField="NB_ENTREVISTA_TIPO" UniqueName="NB_ENTREVISTA_TIPO" HeaderText="Tipo de entrevista">
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="140" DataField="NB_ENTREVISTADOR" UniqueName="NB_ENTREVISTADOR" HeaderText="Entrevistador"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="140" DataField="NB_PUESTO_ENTREVISTADOR" UniqueName="NB_PUESTO_ENTREVISTADOR" HeaderText="Puesto entrevistador"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="140px" DataField="CL_CORREO_ENTREVISTADOR" UniqueName="CL_CORREO_ENTREVISTADOR" HeaderText="Correo del entrevistador">
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                                <div style="height: 10px; clear: both;"></div>

                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnagregarEntrevista" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenNuevaEntrevistaWindow"></telerik:RadButton>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnEditarEntrevista" Text="Editar" AutoPostBack="false" OnClientClicked="OpenEditarEntrevistaWindow"></telerik:RadButton>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnEliminarEntrevista" Text="Eliminar" OnClientClicking="ConfirmarEliminarEntrevista" OnClick="btnEliminarEntrevista_Click"></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnEnvioCorreos" Text="Enviar correos a seleccionados" OnClick="btnEnvioCorreos_Click"></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnEnviarTodos" Text="Enviar correo a todos" OnClick="btnEnviarTodos_Click"></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnComentarios" Text="Ver comentarios" OnClientClicking="OpenComentariosEntrevista"></telerik:RadButton>
                                </div>

                            </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="RPVReferencias" runat="server">
                            <div style="height: calc(100% - 70px);">
                                <telerik:RadGrid runat="server" HeaderStyle-Font-Bold="true"  ID="grdReferencias" AutoGenerateColumns="false"
                                    Width="100%" OnNeedDataSource="grdReferencias_NeedDataSource" ShowHeader="true" Height="100%" AllowFilteringByColumn="true">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="false" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView ClientDataKeyNames="ID_EXPERIENCIA_LABORAL,ID_CANDIDATO" DataKeyNames="ID_EXPERIENCIA_LABORAL,ID_CANDIDATO" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="160" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderText="Empresa o institución" HeaderStyle-Width="240"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="160" DataField="NB_GIRO" UniqueName="NB_GIRO" HeaderText="Giro" HeaderStyle-Width="240"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="70" DataField="FE_INICIO" UniqueName="FE_INICIO" HeaderText="Período de" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="70" DataField="FE_FIN" UniqueName="FE_FIN" HeaderText="Periodo a" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="160" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderText="Puesto" HeaderStyle-Width="240"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="180" DataField="DS_FUNCIONES" UniqueName="DS_FUNCIONES" HeaderText="Actividades" HeaderStyle-Width="260"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="160" DataField="NB_CONTACTO" UniqueName="NB_CONTACTO" HeaderText="Nombre de contacto" HeaderStyle-Width="240"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="160" DataField="NB_PUESTO_CONTACTO" UniqueName="NB_PUESTO_CONTACTO" HeaderText="Puesto de contacto" HeaderStyle-Width="240"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="70" DataField="NO_TELEFONO_CONTACTO" UniqueName="NO_TELEFONO_CONTACTO" HeaderText="Teléfono de contacto" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="160" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO" HeaderText="Correo del contacto" HeaderStyle-Width="240"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="160" DataField="NB_REFERENCIA" UniqueName="NB_REFERENCIA" HeaderText="Nombre de referencia" HeaderStyle-Width="240"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="160" DataField="NB_PUESTO_REFERENCIA" UniqueName="NB_PUESTO_REFERENCIA" HeaderText="Puesto de referencia" HeaderStyle-Width="240"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="70" DataField="CL_INFORMACION_CONFIRMADA" UniqueName="CL_INFORMACION_CONFIRMADA" HeaderText="Información confirmada" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="180" DataField="DS_COMENTARIOS" UniqueName="DS_COMENTARIOS" HeaderText="Comentarios" HeaderStyle-Width="260"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="height: 10px; clear: both;"></div>

                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnAgregarReferencia" OnClientClicked="ShowPopupEditarExperiencia" Text="Editar" AutoPostBack="false"></telerik:RadButton>
                            </div>
                            <%-- <div style="clear: both; height: 10px"></div>
                <div class="ctrlBasico">
                    <table style="width: 100%;">
                        <thead>
                            <tr>
                                <td width="20%">Empleos</td>
                                <td width="20%">Empleo actual o último</td>
                                <td width="20%">Anterior 1</td>
                                <td width="20%">Anterior 2</td>
                                <td width="20%">Anterior 3</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Empresa o Institución:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtEmpresaActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtEmpresa1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtEmpresa2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtEmpresa3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td>Giro:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtGiroActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtGiro1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtGiro2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtGiro3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Periodo (de):</td>

                                <td>
                                    <div class="RadMenu">
                                        <div class="ctrlBasico">
                                            <telerik:RadTextBox runat="server" ID="txtPeriodoInicialActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>

                                </td>
                                <td>
                                    <div class="RadMenu">
                                        <div class="ctrlBasico">
                                            <telerik:RadTextBox runat="server" ID="txtPeriodoInicial1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>

                                </td>
                                <td>
                                    <div class="RadMenu">
                                        <div class="ctrlBasico">
                                            <telerik:RadTextBox runat="server" ID="txtPeriodoInicial2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="RadMenu">
                                        <div class="ctrlBasico">
                                            <telerik:RadTextBox runat="server" ID="txtPeriodoInicial3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td>Periodo (a):</td>
                                <td>
                                    <div class="RadMenu">
                                        <div class="ctrlBasico">
                                            <telerik:RadTextBox runat="server" ID="txtPeriodoFinalActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="RadMenu">
                                        <div class="ctrlBasico">
                                            <telerik:RadTextBox runat="server" ID="txtPeriodoFinal1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>

                                </td>
                                <td>
                                    <div class="RadMenu">
                                        <div class="ctrlBasico">
                                            <telerik:RadTextBox runat="server" ID="txtPeriodoFinal2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="RadMenu">
                                        <div class="ctrlBasico">
                                            <telerik:RadTextBox runat="server" ID="txtPeriodoFinal3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Puesto:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuesto1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuesto2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuesto3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Principales actividades:</td>
                                <td>

                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtActividadesActual" Width="200px" TextMode="MultiLine" Height="80" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtActividades1" Width="200px" TextMode="MultiLine" Height="80" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>

                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtActividades2" Width="200px" TextMode="MultiLine" Height="80" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtActividades3" Width="200px" TextMode="MultiLine" Height="80" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>

                            </tr>

                            <tr>
                                <td>Nombre del jefe inmediato:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtJefeActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtJefe1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtJefe2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtJefe3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Puesto del jefe inmediato:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoJefeActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoJefe1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoJefe2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoJefe3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Telefono del jefe inmediato:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtTelefonoJefeActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtTelefonoJefe1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtTelefonoJefe2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtTelefonoJefe3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Correo electrónico del jefe inmediato:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtCorreoJefeActual" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtCorreoJefe1" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtCorreoJefe2" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtCorreoJefe3" Width="200px" Enabled="false"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>Nombre de la persona que proporcionó las referencias:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtNombreReferenciaActual" Width="200px"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtNombreReferencia1" Width="200px"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtNombreReferencia2" Width="200px"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtNombreReferencia3" Width="200px"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>Puesto de la persona que proporcionó las referencias:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoReferenciaActual" Width="200px"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoReferencia1" Width="200px"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoReferencia2" Width="200px"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtPuestoReferencia3" Width="200px"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>La información que proporcionó el candidato fue confirmada:	</td>
                                <td>

                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="chkInformacionActual" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="chkInformacion1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="chkInformacion2" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="chkInformacion3" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>Comentarios de la persona que proporcionó las referencias:</td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtComentarioActual" Width="200px" Height="80" TextMode="MultiLine"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtComentario1" Width="200px" Height="80" TextMode="MultiLine"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtComentario2" Width="200px" Height="80" TextMode="MultiLine"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox runat="server" ID="txtComentario3" Width="200px" Height="80" TextMode="MultiLine"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>


                        </tbody>
                    </table>
                    <div style="clear: both;"></div>
                    <div class="divControlDerecha">
                        <telerik:RadButton ID="btnGuardarReferencias" runat="server" Width="100px" Text="Guardar" AutoPostBack="true" OnClick="btnGuardarReferencias_Click"></telerik:RadButton>
                    </div>
                </div>--%>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="RPVCompetenciasPuesto" runat="server">
                            <div style="clear: both; height: 5px;"></div>

                            <%-- <table runat="server" id="tblCompetencia" class="ctrlTableForm" style="width: 100%;">
                    <tr>
                        <td style="width: 90px;">
                            <telerik:RadButton ID="btnAnt" runat="server" Text="Anterior" Width="90px" OnClick="btnAnt_Click"></telerik:RadButton>
                        </td>
                        <td style="width: 35px;">
                            <div runat="server" id="divColorClas" style="width: 35px; height: 35px; border-radius: 5px;">
                                <br />
                            </div>
                        </td>
                        <td runat="server" id="tdClasificacion" class="MostrarCelda">
                            <span id="txtNbClasificacion" runat="server" style="width: 300px;"></span>
                        </td>
                        <td runat="server" id="tdSignificado" class="MostrarCelda">
                            <div id="txtDsSignificado" runat="server" style="max-height: 50px; overflow: auto;"></div>
                        </td>
                        <td style="width: 90px;">
                            <telerik:RadButton ID="btnNext" runat="server" Text="Siguiente" Width="90px" OnClick="btnNext_Click"></telerik:RadButton>
                        </td>
                    </tr>
                </table>--%>

                            <%--          <div style="clear: both;"></div>--%>
                            <div style="height: calc(100% - 10px);">
                                <telerik:RadGrid
                                    ID="dgvCompetencias"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    OnNeedDataSource="dgvCompetencias_NeedDataSource"
                                    OnItemCreated="dgvCompetencias_ItemCreated"
                                    OnItemDataBound="dgvCompetencias_DataBound"
                                    HeaderStyle-Font-Bold="true" 
                                    Height="100%">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowHeadersWhenNoRecords="true" Name="Evaluación" DataKeyNames="ID_COMPETENCIA" ShowFooter="true">
                                        <%--<GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Clasificación" FieldName="NB_CLASIFICACION_COMPETENCIA" />
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="NB_CLASIFICACION_COMPETENCIA" SortOrder="Ascending" />
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>--%>
                                        <Columns>
                                            <telerik:GridTemplateColumn DataField="CL_COLOR" UniqueName="CL_COLOR" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="30px" Height="15px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="50px" Height="30px" />
                                                <ItemTemplate>
                                                    <div class="ctrlBasico" style="height: 60px; width: 30px; float: left; background: <%# Eval("CL_COLOR") %>; border-radius: 5px;"></div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="NB_COMPETENCIA" HeaderText="Competencia" UniqueName="NB_COMPETENCIA" ReadOnly="true" HeaderStyle-Width="200">
                                                <ItemTemplate>
                                                    <div><%# Eval("NB_COMPETENCIA") %></div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="FG_VALOR0" UniqueName="FG_VALOR0" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <div style="width: 100%; height: 100%; text-align: center;">
                                                        0<br />
                                                        0%
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <telerik:RadButton runat="server" ID="rbNivel0" Text="0" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR0") %>' Enabled="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="FG_VALOR1" UniqueName="FG_VALOR1" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <div style="width: 100%; height: 100%; text-align: center;">
                                                        1<br />
                                                        20%
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <telerik:RadButton runat="server" ID="rbNivel1" Text="1" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR1") %>' Enabled="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="FG_VALOR2" UniqueName="FG_VALOR2" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <div style="width: 100%; height: 100%; text-align: center;">
                                                        2<br />
                                                        40%
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <telerik:RadButton runat="server" ID="rbNivel2" Text="2" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR2") %>' Enabled="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="FG_VALOR3" UniqueName="FG_VALOR3" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <div style="width: 100%; height: 100%; text-align: center;">
                                                        3<br />
                                                        60%
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <telerik:RadButton runat="server" ID="rbNivel3" Text="3" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR3") %>' Enabled="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="FG_VALOR4" UniqueName="FG_VALOR4" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <div style="width: 100%; height: 100%; text-align: center;">
                                                        4<br />
                                                        80%
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <telerik:RadButton runat="server" ID="rbNivel4" Text="4" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR4") %>' Enabled="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn DataField="FG_VALOR5" UniqueName="FG_VALOR5" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <div style="width: 100%; height: 100%; text-align: center;">
                                                        5<br />
                                                        100%
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <telerik:RadButton runat="server" ID="rbNivel5" Text="5" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR5") %>' Enabled="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="DS_COMPETENCIA" HeaderText="Descripción de la competencia" UniqueName="DS_COMPETENCIA" ReadOnly="true" Visible="true" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NO_VALOR_NIVEL" HeaderText="Nivel solicitado" UniqueName="NO_VALOR_NIVEL" ReadOnly="true" DataFormatString="{0:N0}" HeaderStyle-Width="100">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PR_CUMPLIMIENTO_COMPETENCIA" HeaderText="% De Cumplimiento" UniqueName="PR_CUMPLIMIENTO_COMPETENCIA" ReadOnly="true" Display="true" DataFormatString="{0:N2}" HeaderStyle-Width="110" Aggregate="Avg" FooterAggregateFormatString="Compatibilidad: {0:N2}%" ItemStyle-CssClass="Derecha" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <%--                <div style="text-align: center; align-content: center;">
                    <telerik:RadButton runat="server" ID="RadButton23" Text="<" Width="50" Height="40"></telerik:RadButton
                    <telerik:RadTextBox ID="RadTextBox124" runat="server" Width="200" TextMode="MultiLine" Height="40"></telerik:RadTextBox>
                    <telerik:RadTextBox ID="RadTextBox125" runat="server" Width="800" TextMode="MultiLine" Height="40"></telerik:RadTextBox>
                    <telerik:RadButton runat="server" ID="RadButton24" Text=">" Width="50" Height="40"></telerik:RadButton>
                </div>
                <div style="clear: both; height: 20px;"></div>
                <telerik:RadGrid
                    ID="RadGrid1"
                    ShowHeader="true"
                    runat="server"
                    AllowPaging="true"
                    AllowSorting="true"
                    GroupPanelPosition="Top"
                    Width="100%"
                    GridLines="None"
                    Height="200"
                    AllowFilteringByColumn="true"
                    ClientSettings-EnablePostBackOnRowClick="false">
                    <GroupingSettings CaseSensitive="False" />
                    <ExportSettings
                        FileName="CatalogoAreas"
                        ExportOnlyData="true"
                        IgnorePaging="true">
                        <Excel Format="Xlsx" />
                    </ExportSettings>

                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling
                            AllowScroll="true"
                            UseStaticHeaders="true"
                            SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <MasterTableView
                        ClientDataKeyNames="ID_DEPARTAMENTO"
                        DataKeyNames="ID_DEPARTAMENTO"
                        ShowHeadersWhenNoRecords="true"
                        AutoGenerateColumns="false"
                        PageSize="20"
                        CommandItemDisplay="Top"
                        HorizontalAlign="NotSet"
                        EditMode="EditForms">
                        <CommandItemSettings
                            ShowAddNewRecordButton="false"
                            ShowExportToExcelButton="true"
                            ShowExportToCsvButton="false"
                            RefreshText="Actualizar"
                            AddNewRecordText="Insertar" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Tipo de evento" DataField="CL_DEPARTAMENTO" UniqueName="CL_DEPARTAMENTO" HeaderStyle-Width="150" FilterControlWidth="30"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="350" FilterControlWidth="330"></telerik:GridBoundColumn>
                        </Columns>

                    </MasterTableView>
                </telerik:RadGrid>--%>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="RPVAplicacionPruebas" runat="server">
                            <div style="height: calc(100% - 70px);">
                                <telerik:RadGrid
                                    ID="rgdAplicacionPrueba"
                                    ShowHeader="true"
                                    runat="server"
                                    AllowPaging="true"
                                    AllowSorting="true"
                                    Height="100%"
                                    HeaderStyle-Font-Bold="true" 
                                    AllowFilteringByColumn="true"
                                    OnNeedDataSource="rgdAplicacionPrueba_NeedDataSource"
                                    OnItemDataBound="rgdAplicacionPrueba_ItemDataBound">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowKeyboardNavigation="true">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="false" />
                                    <MasterTableView ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Prueba requerida" DataField="NB_PRUEBA" UniqueName="NB_PRUEBA" HeaderStyle-Width="150" FilterControlWidth="90"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="150" FilterControlWidth="90"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de aplicación" DataField="FE_INICIO" UniqueName="FE_INICIO" HeaderStyle-Width="150" FilterControlWidth="90"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both; height: 10px;"></div>
                             <div class="ctrlBasico">
                                <telerik:RadButton ID="btnResultadosPrueba" runat="server" Text="Resultados" AutoPostBack="false" Width="100px" OnClientClicked="openResultadoPruebas"></telerik:RadButton>
                            </div>
                        </telerik:RadPageView>

                         <telerik:RadPageView ID="RPVEstudiosocioeconomico" runat="server">
                            <div style="clear: both; height: 5px;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCopiarDatos" runat="server" Width="250px" OnClientClicking="CopiarDatos" Text="Copiar datos de solicitud de empleo" OnClick="btnCopiarDatos_Click" AutoPostBack="true"></telerik:RadButton>
                            </div>

                            <div style="clear: both; height: 650px;">
                                <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Height="100%" Width="100%" ExpandMode="FullExpandedItem">
                                    <Items>

                                        <telerik:RadPanelItem Text="Datos personales" Expanded="true">
                                            <ContentTemplate>
                                                <div style="clear: both; height: 10px;"></div>
                                                <table style="width: 100%;" border="0">
                                                    <thead>
                                                        <tr>
                                                            <td width="1%"></td>
                                                            <td width="25%"></td>
                                                            <td width="24%"></td>
                                                            <td width="25%"></td>
                                                            <td width="24%"></td>
                                                            <td width="1%"></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label35" name="lblNbIdioma" runat="server">Fecha de nacimiento:</label>

                                                                    <br />
                                                                    <telerik:RadDatePicker runat="server" ID="dtpESNacimiento" Width="130" OnSelectedDateChanged="dtpESNacimiento_SelectedDateChanged" AutoPostBack="true"></telerik:RadDatePicker>

                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label36" name="lblNbIdioma" runat="server">Edad:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESEdad" runat="server" Width="80" MaxLength="20" ReadOnly="true"></telerik:RadTextBox>

                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label37" name="lblNbIdioma" runat="server">Estado civil:</label>
                                                                    <br />
                                                                    <telerik:RadComboBox
                                                                        Filter="Contains" runat="server" ID="cmbEstadoCivil" Width="180" MarkFirstMatch="true"
                                                                        AutoPostBack="false" EmptyMessage="Seleccione..." DropDownWidth="230">
                                                                    </telerik:RadComboBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label38" name="lblNbIdioma" runat="server">RFC:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESrfc" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>

                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label39" name="lblNbIdioma" runat="server">CURP:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESCurp" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label40" name="lblNbIdioma" runat="server">NSS (Numero de seguro social):</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESSeguroSocial" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <%--<br />--%>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label47" name="lblNbIdioma" runat="server">Estado:</label>
                                                                </div>
                                                                <br />
                                                                <telerik:RadListBox ID="rlbEstado" ReadOnly="true" runat="server" Width="50px" MaxLength="100">
                                                                    <Items>
                                                                        <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                                                                    </Items>
                                                                </telerik:RadListBox>
                                                                <telerik:RadButton runat="server" ID="btnSeleccionarEstado" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionWindow"></telerik:RadButton>
                                                                <telerik:RadButton runat="server" ID="btnEliminarEstado" Text="X" AutoPostBack="false" OnClientClicked="EliminarSeleccionLista"></telerik:RadButton>


                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label48" name="lblNbIdioma" runat="server">Municipio:</label>
                                                                </div>
                                                                <br />
                                                                <telerik:RadListBox ID="rlbMunicipios" ReadOnly="true" runat="server" Width="160" MaxLength="100">
                                                                    <Items>
                                                                        <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                                                                    </Items>
                                                                </telerik:RadListBox>
                                                                <telerik:RadButton runat="server" ID="btnSeleccionaMunicipio" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionWindow"></telerik:RadButton>
                                                                <telerik:RadButton runat="server" ID="btnEliminaMunicipio" Text="X" AutoPostBack="false" OnClientClicked="EliminarSeleccionLista"></telerik:RadButton>

                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>

                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label44" name="lblNbIdioma" runat="server">Colonia:</label>
                                                                </div>
                                                                <br />
                                                                <telerik:RadListBox ID="rlbColonia" ReadOnly="true" runat="server" Width="160px" MaxLength="100">
                                                                    <Items>
                                                                        <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                                                                    </Items>
                                                                </telerik:RadListBox>
                                                                <telerik:RadButton runat="server" ID="btnBuscarColonia" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionWindow"></telerik:RadButton>
                                                                <telerik:RadButton runat="server" ID="btnEliminaColonia" Text="X" AutoPostBack="false" OnClientClicked="EliminarSeleccionLista"></telerik:RadButton>

                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label41" name="lblNbIdioma" runat="server">Calle:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESCalle" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label42" name="lblNbIdioma" runat="server">No.Ext:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESNoExt" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label43" name="lblNbIdioma" runat="server">No.Int:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESNoInt" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>


                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>

                                                            <td></td>
                                                        </tr>


                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label46" name="lblNbIdioma" runat="server">Telefono con LADA:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESTelefono" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>


                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label49" name="lblNbIdioma" runat="server">Teléfono móvil:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESTelefonoMovil" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label45" name="lblNbIdioma" runat="server">CP:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESCodigoPostal" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="ctrlBasico">
                                                                    <label id="Label50" name="lblNbIdioma" runat="server">Tiempo de residencia en domicilio:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtEsTiempoResidencia" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>

                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label51" name="lblNbIdioma" runat="server">Tipo sanguineo:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESTipoSanguineo" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label52" name="lblNbIdioma" runat="server">Tipo de identificación oficial:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox runat="server" ID="txtESIdentificacion" Width="200"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label54" name="lblNbIdioma" runat="server">Folio:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESIdentificacionFolio" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />

                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label110" name="lblNbIdioma" runat="server">Servicios médicos:</label>
                                                                    <br />
                                                                    <telerik:RadComboBox
                                                                        Filter="Contains"
                                                                        runat="server"
                                                                        ID="cmbESServiciosMedicos"
                                                                        Width="200"
                                                                        MarkFirstMatch="true"
                                                                        AutoPostBack="false"
                                                                        EmptyMessage="Seleccione..."
                                                                        DropDownWidth="230">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="ISSSTE" Value="ISSSTE" />
                                                                            <telerik:RadComboBoxItem Text="IMSS" Value="IMSS" />
                                                                            <telerik:RadComboBoxItem Text="Seguro popular" Value="POPULAR" />
                                                                            <telerik:RadComboBoxItem Text="Particular" Value="PARTICULAR" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </div>

                                                            </td>
                                                            <td></td>
                                                        </tr>


                                                        <tr>
                                                            <td></td>

                                                            <td colspan="2">

                                                                <br />
                                                                <div class="ctrlBasico" style="padding-left: 5px;">
                                                                    <label id="Label111" name="lblNbIdioma" runat="server">Especifique:</label>

                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtESServiciosMedicosComentarios" runat="server" Width="300" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                    </tbody>
                                                </table>

                                                <div style="clear: both;"></div>
                                                <div class="divControlDerecha">
                                                    <telerik:RadButton ID="btnGuardarEstudio" runat="server" Width="100px" Text="Guardar" AutoPostBack="true" OnClick="btnGuardarEstudio_Click"></telerik:RadButton>
                                                </div>
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>

                                        <telerik:RadPanelItem Text="Datos laborales" Expanded="false">
                                            <ContentTemplate>

                                                <table style="width: 100%;" border="0">
                                                    <thead>
                                                        <tr>
                                                            <td width="1%"></td>
                                                            <td width="30%"></td>
                                                            <td width="30%"></td>
                                                            <td width="30%"></td>
                                                            <td width="1%"></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <div class="divControlIzquierdaPS" style="padding-top: 5px;">
                                                                    <label id="Label55" name="lblNbIdioma" runat="server">Empresa o institución:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDLEmpresa" runat="server" Width="500" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td colspan="3">
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label56" name="lblNbIdioma" runat="server">Domicilio:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDLDomicilio" runat="server" Width="700" MaxLength="200"></telerik:RadTextBox>
                                                                </div>
                                                            </td>

                                                            <td></td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label58" name="lblNbIdioma" runat="server">Estado:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDLEstado" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>

                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label59" name="lblNbIdioma" runat="server">Municipio:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox runat="server" ID="txtDLMunicipio" Width="200"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label57" name="lblNbIdioma" runat="server">CP:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDLCodigoPostal" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>


                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label61" name="lblNbIdioma" runat="server">Puesto:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDLPuesto" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>

                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label62" name="lblNbIdioma" runat="server">Sueldo inicial:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDLSueldoInicial" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000" MinValue="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label63" name="lblNbIdioma" runat="server">Sueldo final:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDLUltimoSueldo" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000" MinValue="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label64" name="lblNbIdioma" runat="server">Tipo de empresa:</label>
                                                                    <br />
                                                                    <telerik:RadComboBox
                                                                        Filter="Contains"
                                                                        runat="server"
                                                                        ID="cmbDLTipoEmpresa"
                                                                        Width="200"
                                                                        MarkFirstMatch="true"
                                                                        AutoPostBack="false"
                                                                        EmptyMessage="Seleccione..."
                                                                        DropDownWidth="230">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Institución gubernamental" Value="GOBIERNO" />
                                                                            <telerik:RadComboBoxItem Text="Iniciativa privada" Value="PRIVADA" />
                                                                            <telerik:RadComboBoxItem Text="Por su cuenta" Value="PROPRIO" />
                                                                            <telerik:RadComboBoxItem Text="Otros" Value="OTRO" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS" style="padding-top: 5px;">
                                                                    <label id="Label7" name="lblNbIdioma" runat="server">Tipo de sueldo:</label>
                                                                    <br />
                                                                    <telerik:RadComboBox
                                                                        Filter="Contains"
                                                                        runat="server"
                                                                        ID="rcmbTipoSuledo"
                                                                        Width="200"
                                                                        MarkFirstMatch="true"
                                                                        AutoPostBack="false"
                                                                        EmptyMessage="Seleccione..."
                                                                        DropDownWidth="230">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Base" Value="BASE" />
                                                                            <telerik:RadComboBoxItem Text="Confianza" Value="CONFIANZA" />
                                                                            <telerik:RadComboBoxItem Text="Eventual" Value="EVENTUAL" />
                                                                            <telerik:RadComboBoxItem Text="Honorarios" Value="HONORARIOS" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </div>
                                                            </td>

                                                            <td colspan="2">
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label66" name="lblNbIdioma" runat="server">Antigüedad en la empresa:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDLAntiguedad" NumberFormat-DecimalDigits="1" runat="server" Width="100" MaxLength="1000" MinValue="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label65" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDLEspecificarTipoEmpresa" runat="server" Width="350" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>



                                                    </tbody>
                                                </table>

                                                <div style="clear: both;"></div>
                                                <div class="divControlDerecha">
                                                    <telerik:RadButton ID="btnGuardarESDatosLaborales" runat="server" Width="100px" Text="Guardar" AutoPostBack="true" OnClick="btnGuardarESDatosLaborales_Click"></telerik:RadButton>
                                                </div>
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>

                                        <telerik:RadPanelItem Text="Dependientes económicos" Expanded="false">
                                            <ContentTemplate>
                                                <div style="clear: both; height: 10px;"></div>

                                                <div class="ctrlBasico" style="padding-left: 10px;">
                                                    <label id="Label2" name="lblNombre" runat="server">Nombre:</label>
                                                    <br />
                                                    <div class="divControlDerecha">
                                                        <telerik:RadTextBox ID="txtNombreDependiente" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                    </div>
                                                </div>
                                                <div class="ctrlBasico">
                                                    <label id="Label3" name="lblParentesco" runat="server">Parentesco:</label>
                                                    <br />
                                                    <div class="divControlDerecha">
                                                        <telerik:RadComboBox
                                                            Filter="Contains" runat="server" ID="cmbParentesco" Width="200" MarkFirstMatch="true"
                                                            AutoPostBack="false" DropDownWidth="230">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="ctrlBasico">
                                                    <label id="Label4" name="lblacimiento" runat="server">Fecha de nacimiento:</label>
                                                    <br />
                                                    <div class="divControlDerecha">
                                                        <telerik:RadDatePicker runat="server" ID="rdpFechaDependiente" Width="130" OnSelectedDateChanged="rdpFechaDependiente_SelectedDateChanged" AutoPostBack="true"></telerik:RadDatePicker>
                                                        <telerik:RadTextBox ID="txtEdadDependiente" runat="server" Width="80" MaxLength="20" ReadOnly="true" Enabled="false"></telerik:RadTextBox>

                                                    </div>
                                                </div>
                                                <div class="ctrlBasico">
                                                    <label id="Label5" name="lblOcupacion" runat="server">Ocupación:</label>
                                                    <br />
                                                    <div class="divControlDerecha">
                                                        <telerik:RadComboBox
                                                            Filter="Contains" runat="server" ID="cmbOcupacion" Width="200" MarkFirstMatch="true"
                                                            AutoPostBack="false" DropDownWidth="230">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="ctrlBasico">
                                                    <label id="Label6" name="lblEconomico" runat="server">Dependiente económico:</label>
                                                    <br />
                                                    <div class="ctrlBasico">
                                                          <div class="checkContainer">
                                                    <telerik:RadButton ID="chkDependienteSi" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpDependientes"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="chkDependienteNo" runat="server" Checked="true" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpDependientes"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </div>
                                                       <%-- <telerik:RadButton ID="chkDependiente" runat="server" ToggleType="CheckBox" AutoPostBack="false">
                                                            <ToggleStates>
                                                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                            </ToggleStates>
                                                        </telerik:RadButton>--%>
                                                    </div>
                                                </div>
                                                <div class="ctrlBasico" style="padding-left: 5px;">
                                                    <br />
                                                    <telerik:RadButton ID="btnAgregarDependiente" runat="server" Width="100px" Text="Agregar" AutoPostBack="true" OnClick="btnAgregarDependiente_Click"></telerik:RadButton>

                                                </div>
                                                <div style="clear: both; height: 10px;"></div>
                                                <div class="ctrlBasico">
                                                    <telerik:RadGrid ID="grdDatosFamiliares" ShowHeader="true" runat="server" AllowPaging="false"
                                                        AllowSorting="true" Width="100%" Height="300"
                                                        OnNeedDataSource="grdDatosFamiliares_NeedDataSource"
                                                        OnItemCommand="grdDatosFamiliares_ItemCommand" HeaderStyle-Font-Bold="true" >

                                                        <ClientSettings EnablePostBackOnRowClick="false">
                                                            <Selecting AllowRowSelect="true" />
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                                        </ClientSettings>
                                                        <PagerStyle AlwaysVisible="true" />
                                                        <MasterTableView ClientDataKeyNames="ID_DATO_DEPENDIENTE" DataKeyNames="ID_DATO_DEPENDIENTE" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                                                            HorizontalAlign="NotSet" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="false">
                                                            <Columns>
                                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre completo" DataField="NB_PARIENTE" UniqueName="NB_PARIENTE" HeaderStyle-Width="300" FilterControlWidth="80"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Parentesco" DataField="CL_PARENTESCO" UniqueName="CL_PARENTESCO"  HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de nacimiento" DataField="FE_NACIMIENTO" UniqueName="FE_NACIMIENTO" HeaderStyle-Width="100" DataFormatString="{0:d}" FilterControlWidth="60"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Ocupación" DataField="CL_OCUPACION" UniqueName="CL_OCUPACION" HeaderStyle-Width="350" FilterControlWidth="80"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Dependiente económico" DataField="FG_DEPENDIENTE" UniqueName="FG_DEPENDIENTE" HeaderStyle-Width="100" FilterControlWidth="60"></telerik:GridBoundColumn>
                                                                <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                                                    <ItemStyle Width="35" />
                                                                    <HeaderStyle Width="35" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </div>
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>

                                        <telerik:RadPanelItem Text="Datos económicos" Expanded="true">
                                            <ContentTemplate>
                                                <div style="clear: both; height: 10px;"></div>

                                                <table style="width: 100%;" border="0">
                                                    <thead>
                                                        <tr>
                                                            <td width="1%"></td>
                                                            <td width="25%"></td>
                                                            <td width="25%"></td>
                                                            <td width="25%"></td>
                                                            <td width="25%"></td>
                                                            <td width="1%"></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="6">
                                                                <h4>Datos de la propiedad:</h4>
                                                                <div style="clear: both; height: 10px;"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label67" name="lblNbIdioma" runat="server">Tipo de propiedad:</label>
                                                                    <br />
                                                                    <telerik:RadComboBox
                                                                        Filter="Contains"
                                                                        runat="server"
                                                                        ID="cmbDETipoPropiedad"
                                                                        Width="200"
                                                                        MarkFirstMatch="true"
                                                                        AutoPostBack="false"
                                                                        EmptyMessage="Seleccione..."
                                                                        DropDownWidth="230">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Privada" Value="PRIVADA" />
                                                                            <telerik:RadComboBoxItem Text="Ejidal" Value="EJIDAL" />
                                                                            <telerik:RadComboBoxItem Text="Comunal" Value="COMUNAL" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </div>
                                                            </td>


                                                            <td colspan="2">
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label1" name="lblNbIdioma" runat="server">Zona:</label>
                                                                    <br />
                                                                    <telerik:RadComboBox
                                                                        Filter="Contains"
                                                                        runat="server"
                                                                        ID="cmbDEZona"
                                                                        Width="200"
                                                                        MarkFirstMatch="true"
                                                                        AutoPostBack="false"
                                                                        EmptyMessage="Seleccione..."
                                                                        DropDownWidth="230">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Urbana" Value="URBANA" />
                                                                            <telerik:RadComboBoxItem Text="Semiurbana" Value="SEMIURBANA" />
                                                                            <telerik:RadComboBoxItem Text="Rural" Value="RURAL" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <td></td>
                                                        <td colspan="2">
                                                            <br />
                                                            <div class="divControlIzquierdaPS">
                                                                <label id="Label69" name="lblNbIdioma" runat="server">Forma de adquisición:</label>
                                                                <br />
                                                                <telerik:RadComboBox
                                                                    Filter="Contains"
                                                                    runat="server"
                                                                    ID="cmbDEFormaAdquisicion"
                                                                    Width="200"
                                                                    MarkFirstMatch="true"
                                                                    AutoPostBack="false"
                                                                    EmptyMessage="Seleccione..."
                                                                    DropDownWidth="230">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Compraventa" Value="COMPRAVENTA" />
                                                                        <telerik:RadComboBoxItem Text="Herencia" Value="HERENCIA" />
                                                                        <telerik:RadComboBoxItem Text="Legado" Value="LEGADO" />
                                                                        <telerik:RadComboBoxItem Text="Otros" Value="OTROS" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </td>
                                                        <td colspan="2">
                                                            <br />
                                                            <div class="divControlIzquierdaPS">
                                                                <label id="Label68" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                <br />
                                                                <telerik:RadTextBox ID="txtDEFormaAdquisicionEspecificar" runat="server" Width="220" MaxLength="1000"></telerik:RadTextBox>
                                                            </div>
                                                        </td>

                                                        <td></td>
                                                        <td></td>

                                                        <tr>
                                                            <td colspan="6">
                                                                <h4>Egresos mensuales:</h4>
                                                                <div style="clear: both; height: 10px;"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label70" name="lblNbIdioma" runat="server">Alimentación:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEAlimentacion" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label71" name="lblNbIdioma" runat="server">Renta:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDERenta" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label72" name="lblNbIdioma" runat="server">Agua:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEAgua" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label73" name="lblNbIdioma" runat="server">Luz:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDELuz" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label74" name="lblNbIdioma" runat="server">Transporte:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDETransporte" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label75" name="lblNbIdioma" runat="server">Vestido:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEVestido" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label76" name="lblNbIdioma" runat="server">Diversión:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEDiversion" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label77" name="lblNbIdioma" runat="server">Predial:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEPredial" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label78" name="lblNbIdioma" runat="server">Hipotéca:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEHipoteca" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label79" name="lblNbIdioma" runat="server">Gastos Médicos:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEGastosMedicos" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label80" name="lblNbIdioma" runat="server">Gas:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEGas" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label81" name="lblNbIdioma" runat="server">Cable:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDECable" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label82" name="lblNbIdioma" runat="server">Teléfono:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDETelefono" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label83" name="lblNbIdioma" runat="server">Gatos del vehículo:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEGastosVehiculo" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label84" name="lblNbIdioma" runat="server">Otros:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEOtrosGastos" NumberFormat-DecimalDigits="2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label85" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDEEspecificaOtrosGastos" runat="server" Width="220" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="6">
                                                                <h4>Ingresos mensuales:</h4>
                                                                <div style="clear: both; height: 10px;"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td colspan="4">
                                                                <label>En el caso de que el usuario reciba ayuda de algún familiar para el sostenimiento de los gastos familiares, especifique la cantidad y de quién:</label>
                                                                <div style="clear: both; height: 10px;"></div>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label86" name="lblNbIdioma" runat="server">Monto:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEIngreso1" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label87" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDEIngreso1Comentarios" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label90" name="lblNbIdioma" runat="server">Monto:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEIngreso3" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label91" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDEIngreso3Comentarios" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>

                                                        </tr>
                                                   <%--     <td></td>--%>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label88" name="lblNbIdioma" runat="server">Monto:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEIngreso2" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label89" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDEIngreso2Comentarios" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label92" name="lblNbIdioma" runat="server">Monto:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDEIngreso4" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label93" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDEIngreso4Comentarios" runat="server" Width="200" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>

                                                <div style="clear: both; height: 5px;"></div>
                                                <div class="divControlDerecha">
                                                    <telerik:RadButton ID="btnGuardarESDatosEconomicos" runat="server" Width="100px" Text="Guardar" AutoPostBack="true" OnClick="btnGuardarESDatosEconomicos_Click"></telerik:RadButton>
                                                </div>
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>

                                        <telerik:RadPanelItem Text="Datos de la vivienda" Expanded="false">
                                            <ContentTemplate>
                                                <div style="clear: both; height: 10px;"></div>

                                                <table style="width: 100%;" border="0">
                                                    <thead>
                                                        <tr>
                                                            <td width="1%"></td>
                                                            <td width="30%"></td>
                                                            <td width="20%"></td>
                                                            <td width="20%"></td>
                                                            <td width="20%"></td>
                                                            <td width="1%"></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label94" name="lblNbIdioma" runat="server">Tipo de vivienda:</label>
                                                                    <br />
                                                                    <telerik:RadComboBox
                                                                        Filter="Contains"
                                                                        runat="server"
                                                                        ID="cmbDVTipoVivienda"
                                                                        Width="200"
                                                                        MarkFirstMatch="true"
                                                                        AutoPostBack="false"
                                                                        EmptyMessage="Seleccione..."
                                                                        DropDownWidth="230">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Casa" Value="CASA" />
                                                                            <telerik:RadComboBoxItem Text="Departamento" Value="DEPARTAMENTO" />
                                                                            <telerik:RadComboBoxItem Text="Cuarto" Value="CUARTO" />
                                                                            <telerik:RadComboBoxItem Text="Propia" Value="PROPIA" />
                                                                            <telerik:RadComboBoxItem Text="Rentada" Value="RENTADA" />
                                                                            <telerik:RadComboBoxItem Text="Hipotecada" Value="HIPOTECADA" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </div>
                                                                <td colspan="2">
                                                                    <div class="divControlIzquierdaPS">
                                                                        <label id="Label95" name="lblNbIdioma" runat="server">Tipo de construcción:</label>
                                                                        <br />
                                                                        <telerik:RadComboBox
                                                                            Filter="Contains"
                                                                            runat="server"
                                                                            ID="cmbDVTipoConstruccion"
                                                                            Width="200"
                                                                            MarkFirstMatch="true"
                                                                            AutoPostBack="false"
                                                                            EmptyMessage="Seleccione..."
                                                                            DropDownWidth="230">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Concreto" Value="CONCRETO" />
                                                                                <telerik:RadComboBoxItem Text="Tabique" Value="TABIQUE" />
                                                                                <telerik:RadComboBoxItem Text="Lamina" Value="Lamina" />
                                                                                <telerik:RadComboBoxItem Text="Otro" Value="OTRO" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </div>
                                                                </td>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>

                                                            <td></td>

                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label96" name="lblNbIdioma" runat="server">Número de cuartos:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDVNoCuartos" NumberFormat-DecimalDigits="0" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label97" name="lblNbIdioma" runat="server">Número de baños:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDVNoBaños" NumberFormat-DecimalDigits="0" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label98" name="lblNbIdioma" runat="server">Patios:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDVNoPatios" NumberFormat-DecimalDigits="0" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label99" name="lblNbIdioma" runat="server">No. de personas que habitan:</label>
                                                                    <br />
                                                                    <telerik:RadNumericTextBox ID="txtDVNoHabitantes" NumberFormat-DecimalDigits="0" runat="server" Width="100" MaxLength="1000"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>



                                                        <tr>
                                                            <td></td>

                                                            <td colspan="2">
                                                                <br />
                                                                <div class="ctrlBasico">
                                                                    <label id="Label8" name="lblNbIdioma" runat="server">Servicios de la vivienda:</label>
                                                                    <br />
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVAgua" AutoPostBack="false" Text="Agua"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVLuz" AutoPostBack="false" Text="Luz"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVDrenaje" AutoPostBack="false" Text="Drenaje"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVGas" AutoPostBack="false" Text="Gas"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVTelefono" AutoPostBack="false" Text="Telefono"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVOtros" AutoPostBack="false" Text="Otros"></telerik:RadCheckBox>
                                                                </div>
                                                            </td>
                                                            <td colspan="2">
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDVOtrosServiciosComentarios" runat="server" Width="400" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <br />
                                                                <div class="ctrlBasico">
                                                                    <label id="Label9" name="lblNbIdioma" runat="server">Relación de bienes muebles:</label>

                                                                    <br />

                                                                    <telerik:RadCheckBox runat="server" ID="chkDVVehiculo" AutoPostBack="false" Text="Vehículo"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVMotocicleta" AutoPostBack="false" Text="Motocicleta"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVOtrosRelacionBienes" AutoPostBack="false" Text="Otros"></telerik:RadCheckBox>
                                                                </div>
                                                            </td>

                                                            <td colspan="2">
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label102" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDVOtrosRelacionBienesComentarios" runat="server" Width="400" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <%-- <tr>
                                                <td></td>
                                                <td>
                                                    <div class="divControlIzquierda">
                                                        <label id="Label103" name="lblNbIdioma" runat="server">Modelo:</label>
                                                    </div>
                                                    <div class="ctrlBasico">
                                                        <telerik:RadTextBox ID="RadTextBox115" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="divControlIzquierda">
                                                        <label id="Label104" name="lblNbIdioma" runat="server">Tipo:</label>
                                                    </div>
                                                    <div class="ctrlBasico">
                                                        <telerik:RadTextBox ID="RadTextBox116" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="divControlIzquierda">
                                                        <label id="Label105" name="lblNbIdioma" runat="server">Marca:</label>
                                                    </div>
                                                    <div class="ctrlBasico">
                                                        <telerik:RadTextBox ID="RadTextBox117" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="divControlIzquierda">
                                                        <label id="Label106" name="lblNbIdioma" runat="server">Valor:</label>
                                                    </div>
                                                    <div class="ctrlBasico">
                                                        <telerik:RadTextBox ID="RadTextBox118" runat="server" Width="100" MaxLength="1000"></telerik:RadTextBox>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>--%>

                                                        <tr>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label107" name="lblNbIdioma" runat="server">Otros bienes muebles:</label>
                                                                </div>
                                                                <br />
                                                                <div class="ctrlBasico">
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVDvd" Text="DVD" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVRadio" Text="Radio" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVLavadora" Text="Lavadora" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVComputadora" Text="Computadora" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVTelevision" Text="Televisión" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVAlajas" Text="Alajas" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVEstufas" Text="Estufa" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVSala" Text="Sala" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVComedor" Text="Comedor" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVRefrigerador" Text="Refrigerador" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVHornos" Text="Horno" AutoPostBack="false"></telerik:RadCheckBox>
                                                                    <telerik:RadCheckBox runat="server" ID="chkDVBienesRaicesOtros" Text="Otros" AutoPostBack="false"></telerik:RadCheckBox>
                                                                </div>
                                                            </td>

                                                            <td colspan="2">
                                                                <br />
                                                                <div class="divControlIzquierdaPS">
                                                                    <label id="Label108" name="lblNbIdioma" runat="server">Especifique:</label>
                                                                    <br />
                                                                    <telerik:RadTextBox ID="txtDVOtrosBienesComentarios" runat="server" Width="400" MaxLength="1000"></telerik:RadTextBox>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>

                                                <div style="clear: both;"></div>
                                                <div class="divControlDerecha">
                                                    <telerik:RadButton ID="btnGuardarESDatosVivienda" runat="server" Width="100px" Text="Guardar" AutoPostBack="true" OnClick="btnGuardarESDatosVivienda_Click"></telerik:RadButton>
                                                </div>
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>

                                        <%--     <telerik:RadPanelItem Text="Servicios médicos" Expanded="false">
                                <ContentTemplate>
                                    <div style="clear: both; height: 10px;"></div>
                                    
                                </ContentTemplate>
                            </telerik:RadPanelItem>--%>
                                    </Items>
                                </telerik:RadPanelBar>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVResultadosMedicos" runat="server">
                            <div style="clear: both; height: 10px; width: 100%;"></div>
                            <table style="width: 99%;">
                                <thead>
                                    <tr>
                                        <td width="25%"></td>
                                        <td width="25%"></td>
                                        <td width="25%"></td>
                                        <td width="25%"></td>
                                    </tr>
                                </thead>

                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="divControlIzquierda">
                                                <label id="Label14" name="lblNbIdioma" runat="server">Edad:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox ID="txtEMedad" runat="server" Width="60" NumberFormat-DecimalDigits="0" ReadOnly="true"></telerik:RadNumericTextBox>
                                            </div>


                                        </td>
                                      <%--  <td>
                                            <div class="divControlIzquierda">
                                                <label id="Label15" name="lblNbIdioma" runat="server">Talla:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="txtEMtalla" runat="server" Width="60" MaxLength="1000"></telerik:RadTextBox>
                                            </div>

                                        </td>--%>
                                        <td>
                                            <div style="clear: both;" />
                                            <div class="divControlIzquierda">
                                                <label id="Label16" name="lblNbIdioma" runat="server">Peso:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox ID="txtEMPeso" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxLength="1000"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                        <td>

                                            <div style="clear: both;" />
                                            <div class="divControlIzquierda">
                                                <label id="Label17" name="lblNbIdioma" runat="server">Índice de masa corporal:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox ID="txtEMMasaCorporal" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxLength="1000"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                           <td>
                                            <div class="divControlIzquierda">
                                                <label id="Label19" name="lblNbIdioma" runat="server">Pulso:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox ID="txtEMPulso" runat="server" Width="60" NumberFormat-DecimalDigits="2" MaxLength="1000"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                     
                                        <td>
                                            <div class="divControlIzquierda">
                                                <label id="Label20" name="lblNbIdioma" runat="server">Presión arterial:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="txtEMPresionArterial" runat="server" Width="60" MaxLength="1000"></telerik:RadTextBox>
                                            </div>

                                        </td>
                                        <td>
                                            <div class="divControlIzquierda">
                                                <label id="Label21" name="lblNbIdioma" runat="server">Embarazos:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox ID="txtEMEmbarazos" runat="server" NumberFormat-DecimalDigits="0" Width="60" MaxLength="1000"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="divControlIzquierda">
                                                <label id="Label22" name="lblNbIdioma" runat="server">Hijos:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox ID="txtEMHijos" runat="server" Width="60" NumberFormat-DecimalDigits="0" MaxLength="1000"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                            <div class="divControlIzquierda">
                                                <label id="Label23" name="lblNbIdioma" runat="server">Enfermedades actuales:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <div class="checkContainer">
                                                    <telerik:RadButton ID="chkEMEnfermedadSi" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpOtros" OnClientCheckedChanged="LimpiarDatosExamenMedico"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="chkEMEnfermedadNo" runat="server"  ToggleType="Radio" ButtonType="StandardButton" GroupName="grpOtros"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </div>
                                                <%-- <telerik:RadButton ID="chkEMEnfermedad" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" OnClientCheckedChanged="LimpiarDatosExamenMedico">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>--%>
                                            </div>

                                        </td>

                                        <td colspan="3">
                                            <div class="divControlIzquierda">
                                                <label id="Label24" name="lblNbIdioma" runat="server">Especificar:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="txtEmEnfermedadComentario" runat="server" Width="700" MaxLength="1000" TextMode="MultiLine" Height="60"></telerik:RadTextBox>
                                            </div>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td>

                                            <div class="divControlIzquierda">
                                                <label id="Label25" name="lblNbIdioma" runat="server">Medicamentos que toma: </label>
                                            </div>
                                            <div class="ctrlBasico">
                                                      <div class="checkContainer">
                                                    <telerik:RadButton ID="chkEMMedicamentosSi" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpMedicamentos" OnClientCheckedChanged="LimpiarDatosExamenMedico"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="chkEMMedicamentosNo" runat="server"  ToggleType="Radio" ButtonType="StandardButton" GroupName="grpMedicamentos"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </div>
                                            <%--    <telerik:RadButton ID="chkEMMedicamentos" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" OnClientCheckedChanged="LimpiarDatosExamenMedico">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>--%>
                                            </div>

                                        </td>

                                        <td colspan="3">
                                            <div class="divControlIzquierda">
                                                <label id="Label26" name="lblNbIdioma" runat="server">Especificar:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="txtEMMedicamentosComentarios" runat="server" Width="700" MaxLength="1000" TextMode="MultiLine" Height="60"></telerik:RadTextBox>
                                            </div>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td>

                                            <div class="divControlIzquierda">
                                                <label id="Label27" name="lblNbIdioma" runat="server">Alergias: </label>
                                            </div>
                                            <div class="ctrlBasico">
                                                 <div class="checkContainer">
                                                    <telerik:RadButton ID="chkEMAlergiasSi" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAlergias" OnClientCheckedChanged="LimpiarDatosExamenMedico"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="chkEMAlergiasNo" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAlergias"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </div>
                                                <%--<telerik:RadButton ID="chkEMAlergias" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" OnClientCheckedChanged="LimpiarDatosExamenMedico">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>--%>
                                            </div>

                                        </td>

                                        <td colspan="3">
                                            <div class="divControlIzquierda">
                                                <label id="Label28" name="lblNbIdioma" runat="server">Especificar:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="txtEMAlergiasComentarios" runat="server" Width="700" MaxLength="1000" TextMode="MultiLine" Height="60"></telerik:RadTextBox>
                                            </div>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td>

                                            <div class="divControlIzquierda">
                                                <label id="Label29" name="lblNbIdioma" runat="server">Antecedentes familiares (cáncer, hipertensión, diabetes, etc.):</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                 <div class="checkContainer">
                                                    <telerik:RadButton ID="chkEMAntecedentesSi" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAntecedentes" OnClientCheckedChanged="LimpiarDatosExamenMedico"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="chkEMAntecedentesNo" runat="server"  ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAntecedentes"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </div>
                                               <%-- <telerik:RadButton ID="chkEMAntecedentes" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" OnClientCheckedChanged="LimpiarDatosExamenMedico">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>--%>
                                            </div>

                                        </td>

                                        <td colspan="3">
                                            <div class="divControlIzquierda">
                                                <label id="Label30" name="lblNbIdioma" runat="server">Especificar:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="txtEMAntecedentesComentarios" runat="server" Width="700" MaxLength="1000" TextMode="MultiLine" Height="60"></telerik:RadTextBox>
                                            </div>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td>

                                            <div class="divControlIzquierda">
                                                <label id="Label31" name="lblNbIdioma" runat="server">Intervenciones quirúrgicas:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                               <div class="checkContainer">
                                                    <telerik:RadButton ID="chkEMCirujiasSi" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCirugias" OnClientCheckedChanged="LimpiarDatosExamenMedico"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="chkEMCirujiasNo" runat="server"  ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCirugias"  AutoPostBack="false">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </div>
                                               <%-- <telerik:RadButton ID="chkEMCirujias" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" OnClientCheckedChanged="LimpiarDatosExamenMedico">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>--%>
                                            </div>

                                        </td>

                                        <td colspan="3">
                                            <div class="divControlIzquierda">
                                                <label id="Label32" name="lblNbIdioma" runat="server">Especificar:</label>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="txtEMCirujiasComentarios" runat="server" Width="700" MaxLength="1000" TextMode="MultiLine" Height="60"></telerik:RadTextBox>
                                            </div>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td style="height: 200px;">
                                            <div class="divControlDerecha">
                                                <label id="Label33" name="lblFolio">Observaciones:</label>
                                            </div>
                                        </td>
                                        <td colspan="3">
                                            <telerik:RadEditor Height="200px" Width="700" ToolsWidth="310px" EditModes="Design" ID="reObservaciones" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/BasicTools.xml"></telerik:RadEditor>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>

                            <div style="clear: both;"></div>
                            <div class="divControlIzquierda">
                                <label id="Label34" name="lblNbIdioma" runat="server">Adecuado:</label>
                            </div>
                            <div class="ctrlBasico">
                                <div class="checkContainer">
                                    <telerik:RadButton ID="chkEMAdecuadoSi" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAdecuado" OnClientCheckedChanged="LimpiarDatosExamenMedico" AutoPostBack="false">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="chkEMAdecuadoNo" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAdecuado" AutoPostBack="false">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <%-- <telerik:RadButton ID="chkEMAdecuado" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>--%>
                            </div>

                            <div style="clear: both;"></div>
                            <div class="divControlDerecha">
                                <telerik:RadButton ID="btnGuardarExamenMedico" runat="server" Width="100px" Text="Guardar" AutoPostBack="true" OnClick="btnGuardarExamenMedico_Click"></telerik:RadButton>
                            </div>
                        </telerik:RadPageView>

                       

                        <telerik:RadPageView ID="RPVDocumentacionCandidato" runat="server">
                            <div class="ctrlBasico">
                                Tipo documento:<br />
                                <telerik:RadComboBox ID="cmbTipoDocumento" runat="server">
                                    <Items>
                                        <%--<telerik:RadComboBoxItem Text="Fotografía" Value="FOTOGRAFIA" />--%>
                                        <telerik:RadComboBoxItem Text="Imagen" Value="IMAGEN" />
                                        <telerik:RadComboBoxItem Text="Otro" Value="OTRO" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                            <div class="ctrlBasico" style="width: 420px">
                                Subir documento:<br />
                                <telerik:RadAsyncUpload ID="rauDocumento" runat="server" MultipleFileSelection="Disabled"><Localization Select="Seleccionar" /></telerik:RadAsyncUpload>
                               
                            </div>
                            <div class="ctrlBasico" >
                                <br />
                                 <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click"></telerik:RadButton>
                            </div>

                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadGrid ID="grdDocumentos" runat="server" HeaderStyle-Font-Bold="true" Width="580" OnNeedDataSource="grdDocumentos_NeedDataSource">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView ClientDataKeyNames="ID_ARCHIVO,ID_ITEM" DataKeyNames="ID_ARCHIVO,ID_ITEM" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridHyperLinkColumn HeaderText="Nombre del documento" DataTextField="NB_DOCUMENTO" DataNavigateUrlFields="ID_ARCHIVO,ID_DOCUMENTO,FE_CREATED_DATE,NB_DOCUMENTO,ID_ITEM" DataNavigateUrlFormatString="~/Comunes/ObtenerDocumento.ashx?ArchivoId={0}&ArchivoNb={2:yyyyMMdd}{4}&ArchivoDescargaNb={3}" Target="_blank"></telerik:GridHyperLinkColumn>
                                            <telerik:GridBoundColumn HeaderText="Tipo de documento" HeaderStyle-Width="200" DataField="CL_TIPO_DOCUMENTO" UniqueName="CL_TIPO_DOCUMENTO"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnDelDocumentos" runat="server" Text="Eliminar" OnClick="btnDelDocumentos_Click"></telerik:RadButton>
                                <telerik:RadButton ID="btnGuardarDoc" runat="server" Text="Guardar" OnClick="btnGuardarDoc_Click"></telerik:RadButton>

                            </div>


                        </telerik:RadPageView>

                        <telerik:RadPageView ID="RPVRegistroCambiosSolicitud" runat="server">
                            <div style="height: calc(100% - 10px);">
                                <telerik:RadGrid
                                    ID="rdgRegistroSolicitud"
                                    HeaderStyle-Font-Bold="true" 
                                    runat="server"
                                    AllowPaging="true"
                                    AllowSorting="true"
                                    Width="60%"
                                    Height="100%"
                                    AllowFilteringByColumn="true"
                                    OnNeedDataSource="rdgRegistroSolicitud_NeedDataSource">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <MasterTableView ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                                        HorizontalAlign="NotSet" EditMode="EditForms">
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                                            ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Operación" DataField="CL_TIPO_OPERACION" UniqueName="CL_TIPO_OPERACION" HeaderStyle-Width="120" FilterControlWidth="80"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Usuario" DataField="CL_USUARIO" UniqueName="CL_USUARIO" HeaderStyle-Width="120" FilterControlWidth="80"></telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="160" FilterControlWidth="120" HeaderText="Fecha" DataField="FE_REGISTRO" UniqueName="FE_REGISTRO"></telerik:GridDateTimeColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>

                        </telerik:RadPageView>

                    </telerik:RadMultiPage>

                <%--</div>--%>
            </telerik:RadPane>

            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px" Height="50px">
                <telerik:RadSlidingZone ID="rszAvisoDePrivacidad" runat="server" SlideDirection="Left" ExpandedPaneId="rspAyuda" Width="22px" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Foto" Width="180px" RenderMode="Mobile" Height="230">
                        <div class="ctrlBasico" style="padding-left: 20px;">
                            <div style="clear: both; height: 10px"></div>
                            <div style="/*background: #fafafa; right: 0px; border: 1px solid lightgray; border-radius: 10px; padding: 5px; */">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadBinaryImage ID="rbiFotoEmpleado" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </div>
                    </telerik:RadSlidingPane>
                     <telerik:RadSlidingPane ID="rszAyuda" runat="server" Title="Ayuda" Width="270px" RenderMode="Mobile" Height="100%">
                       <div id="divEstatus" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                                Estatus del participante: <br />
                                En esta pestaña puedes observar los diferentes procesos de evaluación con los que cuenta el candidato.
                                El proceso de evaluación actual se presenta en la parte superior junto a la información del candidato. <br />
                                Si deseas copiar datos de otro proceso de evaluación al actual solo selecciona el proceso y a continuación especifica que datos deseas copiar.
                                Puedes copiar los datos de entrevista, estudio socioeconómico o resultados médicos. Da clic en "Copiar datos de otro proceso de evaluación" y de esta manera la información será copiada.	
                                <br />En esta pestaña es donde puedes terminar el proceso de evaluación, recuerda que una vez terminado ya no será posible editarlo.										
                            </p>
                        </div>
                        <div id="divEntrevistas" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                                Registro de entrevistas: <br />
                                En esta pestaña puedes crear o registrar las entrevistas necesarias para el candidato. <br />
                                Con el botón "Agregar" puedes ingresar los datos de la entrevista o generar la entrevista para que alguien más la realice. Al enviarla por correo el entrevistador podrá ingresar los comentarios de entrevista.
                                La opción de "Ver comentarios" te mostrará todos los comentarios de las diversas entrevistas registradas.	
                                																
                            </p>
                        </div>

                        <div id="divReferencias" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                                Referencias:
                                <br />

                                En esta pestaña aparecerán los registros de Experiencia laboral registrados en la solicitud de empleo.
                                Aquí puedes modificar una referencia para indicar los resultados de la verificación de la información al momento de confirmar las referencias laborales.	
											
                            </p>
                        </div>
                        <div id="divPuestoComp" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                                Competencias del puesto:
                                <br />
                               En esta pestaña se muestra la comparación de compatibilidad entre el puesto para el cual aplica el proceso de evaluación (si es que está basado en una requisicion)
                                y los resultados de la última batería de pruebas del candidato.													
                            </p>
                        </div>
                          <div id="divPruebas" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                                Aplicación de pruebas:
                                <br />

                                En esta pestaña se muestran las pruebas de la última batería generadas para el candidato. Aquí puedes consultar los resultados del candidato.
                            </p>
                        </div>
                        <div id="divMedico" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                               Resultados médicos:
                                <br />
                                En esta pestaña puedes capturar los resultados médicos del candidato. La edad se registrara automáticamente al generar el estudio socioeconómico. <br />
                                No olvides guardar tus datos al finalizar esta sección.												
                            </p>
                        </div>
                         <div id="divSocioeconomico" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                               Estudio socieconómico:
                                <br />

                                En esta pestaña puedes registrar el estudio socioeconómico del candidato. Puedes copiar la información básica desde de solicitud de empleo
                                seleccionando dicha opción desde la parte superior.<br /><br /> El estudio socioeconómico cuenta con cinco secciones, Datos personales, Datos laborales, Dependientes económicos,
                                Datos económicos y Datos de la vivienda. En cada sección debes de guardar la información, comenzando por Datos personales.  													
                            </p>
                        </div>
                        <div id="divDocumentacion" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                               Documentación del candidato:
                                <br />
                                En esta pestaña encontraras los documentos del candidato.<br />
                                Para subir un documento selecciona el botón "selec" para cargar el documento y enseguida "Agregar" para agregarlo a la lista. Al finalizar selecciona Guardar para efectuar los cambios.  
                            </p>
                        </div>
                         <div id="divBitacora" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p style="text-align:justify;">
                               Registro de cambios a la solicitud:
                                <br />
                                En esta pestaña se encuentra el registro de los cambios realizados a la solicitud del candidato. 
                                El registro con la operación "Primer registro" corresponde a la fecha de creación de la solicitud.
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
</asp:Content>
