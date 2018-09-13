<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaDescriptivoPuesto.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaDescriptivoPuesto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .lblNivel span {
            border: 1px solid gray;
            padding: 2px 7px !important;
            background-color: white;
            border-radius: 14px;
        }

        .rslItemsWrapper {
            z-index: 10 !important;
        }

         .ruBrowse
       {
           
           width: 150px !important;
       }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbAreaO" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="cmbSubarea" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="cmbModulo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="cmbOcupaciones" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgInterrelacionados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgLateral" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgAlternativa" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbAreaO">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbSubarea" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbSubarea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbModulo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbModulo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbOcupaciones" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbOcupaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbOcupaciones" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblClOcupación" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblOcupacionSeleccionada" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarOcupacionPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbOcupaciones" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblClOcupación" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblOcupacionSeleccionada" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="cmbAreaO" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="cmbSubarea" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="cmbModulo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarOcupacionPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarOcupacionPuesto" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditarCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbCompetenciaEspecifica" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarCompetenciaEspecifica" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdNivelCompetenciaEspecifica" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditarIndicador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtIndicadorDesempeno" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarIndicador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFuncionCompetencias" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarIndicadorDesempeno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFuncionCompetencias" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFuncionCompetencias" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCompetenciaEspecifica">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFuncionCompetencias" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbCompetenciaEspecifica">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarCompetenciaEspecifica" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdNivelCompetenciaEspecifica" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdFuncionCompetencias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFuncionCompetencias" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCompentencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFuncionCompetencias" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCarreProfe">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListaCarreraprof" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnagregarPostgrado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListaPostgrados" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radbtnAgregarCarreraTec">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListaCarreraTec" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCompEsp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListacompetenciasEspecificas" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radBtnAgregarExperiencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListaexperiencia" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="divContenido" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgInterrelacionados" LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarLateral">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgLateral" LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarAlternativa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAlternativa" LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--            <telerik:AjaxSetting AjaxControlID="radBtnAgregarPuestoSubordinado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListaPuestosSubordinado" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <%--   <telerik:AjaxSetting AjaxControlID="btnInter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListaPuestosInterrelacionados" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRutaAlter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListaAlternativa" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnLateral">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radListaLateral" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <%--    
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lstJefeInmediato" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtPruebaXml" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
    --%>

    <telerik:RadCodeBlock runat="server" ID="radCodeblock">
        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().close();
            }

            //FUNCION AGREGAR UN POSGRADO 
            function validarPostgrado(sender, args) {
                var vcmbDatos = $find("<%= radcmbPostgrados.ClientID %>");
                if (vcmbDatos._text.length == 0 || vcmbDatos._value.length == 0) {
                    radalert("Selecciona un postgrado.", 400, 150, "Descriptivo de puestos");
                    args.set_cancel(true);
                }
            }


            //FUNCION AGREGAR UN CARRERA PROFESIONAL 
            function validarCarreraProfesional(sender, args) {
                var vcmbDatos = $find("<%= cmbCarreraProf.ClientID %>");
                if (vcmbDatos._text.length == 0 || vcmbDatos._value.length == 0) {
                    radalert("Selecciona una carrera profesional.", 400, 150, "Descriptivo de puestos");
                    args.set_cancel(true);
                }
            }

            //FUNCION AGREGAR UNA CARRERA TECNICA
            function validarCarreraTec(sender, args) {
                var vcmbDatos = $find("<%= cmbCarrTec.ClientID %>");
                if (vcmbDatos._text.length == 0 || vcmbDatos._value.length == 0) {
                    radalert("Selecciona una carrera técnica.", 400, 150, "Descriptivo de puestos");
                    args.set_cancel(true);
                }
            }

            //FUNCION AGREGAR UNA COMPETENCIA
            function validarCompetencia(sender, args) {
                var vcmbDatos = $find("<%= cmbCompetenciaEspecificas.ClientID %>");
                if (vcmbDatos._text.length == 0 || vcmbDatos._value.length == 0) {
                    radalert("Selecciona una competencia.", 400, 150, "Descriptivo de puestos");
                    args.set_cancel(true);
                }
            }

            //FUNCION AREGAR EXPERIENCIA
            function validarExperiencia(sender, args) {

                var vcmbDatos = $find("<%= cmbExperiencias.ClientID %>");
                var vTxtTiempo = $find("<%= txtTiempo.ClientID%>");
                var vBtnRadioRequerida = $find("<%= btnRequerida.ClientID %>");
                var vBtnRadioDeseada = $find("<%= btnDeseada.ClientID %>");
               
                if (vcmbDatos._text.length == 0 || vcmbDatos._value.length == 0 ) {
                    radalert("Selecciona una experiencia.", 400, 150, "Descriptivo de puestos");
                    args.set_cancel(true);
                    return false;
                }

                if (vTxtTiempo.get_value() == "") {
                    radalert("Indica el tiempo de experiencia.", 400, 150, "Descriptivo de puestos");
                    args.set_cancel(true);
                    return false;
                }

                if (!vBtnRadioRequerida.get_checked() & !vBtnRadioDeseada.get_checked()) {
                    radalert("Indica el tipo de experiencia.", 400, 150, "Descriptivo de puestos");
                    args.set_cancel(true);
                    return false;
                }
            }

            //FUNCION QUE SE LLAMARA CUANDO EL FORMULARIO DE EXPERIENCIA SE CIERRRE
            function CerrarFormularioExperiencia() {
                //PROPIEDADES QUE CAMBIAN CUANDO SE CIERRA EL FORMULARIO            
                $get("<%= divContenido.ClientID %>").style.display = "none";
            }

            //MOSTRAR INFORMACION DE CAPTURA  DE EXPERIENCIA NECESARIA  
            function MostrarInformacion() {

                //LIMPIAR LA CAJA DE TEXTO TIEMPO
                var vtxtTiempo = $find("<%= txtTiempo.ClientID %>");
                vtxtTiempo.set_value("");
                //LIMPIAR LOS RADIOS BUTTONS
                var vBtnRequerida = $find("<%= btnRequerida.ClientID%>")
                vBtnRequerida.set_checked(false);
                var vBtnDeseada = $find("<%= btnDeseada.ClientID%>")
                vBtnDeseada.set_checked(false);
                //LIMPIAR EL COMBOBOX
                var vRadCmbExperiencia = $find("<%= cmbExperiencias.ClientID%>")
                vRadCmbExperiencia.clearSelection();

                $get("<%= divContenido.ClientID %>").style.display = "block";
            }

            //EVENTO CANCELAR EN LA LISTA DE EXPERIENCIAS  EN EL DESCRIPTIVO DE PUESTO
            function CancelarAccionExperiencia(sender, args) {
                CerrarFormularioExperiencia();
            }

            //ABRIR VENTANA MODAL INTERRELACIONADOS
            function OpenPuestosInterrelacionadosWindow() {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 30,
                    height: browserWnd.innerHeight - 30
                };

                openChildDialog("../Comunes/SeleccionPuesto.aspx?mulSel=1&CatalogoCl=INTERRELACIONADO", "winSeleccionPuestos", "Selección de puestos interrelacionados", windowProperties);
            }


            //ABRIR VENTANA MODAL ALTERNATIVOS
            function OpenPuestosAlternativosWindow() {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 30,
                    height: browserWnd.innerHeight - 30
                };

                openChildDialog("../Comunes/SeleccionPuesto.aspx?mulSel=1&CatalogoCl=ALTERNATIVO", "winSeleccionPuestos", "Selección de puestos alternativos", windowProperties);
            }


            //ABRIR VENTANA MODAL LATERALES
            function OpenPuestosLateralesWindow() {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 30,
                    height: browserWnd.innerHeight - 30
                };

                openChildDialog("../Comunes/SeleccionPuesto.aspx?mulSel=1&CatalogoCl=LATERAL", "winSeleccionPuestos", "Selección de puestos laterales", windowProperties);
            }

            //ABRIR ORGANIGRAMA
            function OpenVentanaOrganigrama() {

                var IdPuesto = '<%= vIdDescriptivo %>';
                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                if (IdPuesto != null && IdPuesto != "") {
                    openChildDialog("VentanaVerOrganigrama.aspx?pIdPuesto=" + IdPuesto, "winOrganigrama", "Organigrama", windowProperties);
                }
                else {
                    radalert("No se ha definido el puesto y/o su área/departamento.", 400, 150, "Descriptivo de puestos");
                }
            }

            function SetGenericFunctionWindowSettings(sender, args) {
                var oWnd = $find("<%=winFuncionesGenericas.ClientID%>");
                oWnd.SetWidth(document.documentElement.clientWidth - 20);
                oWnd.SetHeight(document.documentElement.clientHeight - 20);
                oWnd.show();
            }

            //RECEPCION DE LA INFORMACION
            function useDataFromChild(pDato) {
                if (pDato != null) {
                    switch (pDato[0].clTipoCatalogo) {
                        case "INTERRELACIONADO":
                            InsertarDato(EncapsularDatos("INTERRELACIONADO", pDato));
                            break;
                        case "LATERAL":
                            InsertarDato(EncapsularDatos("LATERAL", pDato));
                            break;
                        case "ALTERNATIVO":
                            InsertarDato(EncapsularDatos("ALTERNATIVO", pDato));
                    }

                    //    var vPuestoSeleccionado = pPuestos[0];

                    //    var list = $find("<=lstJefeInmediato.ClientID %>");
                    //    list.trackChanges();

                    //    var items = list.get_items();
                    //    items.clear();

                    //    var item = new Telerik.Web.UI.RadListBoxItem();
                    //    item.set_text(vPuestoSeleccionado.nbPuesto);
                    //    item.set_value(vPuestoSeleccionado.idPuesto);
                    //    items.add(item);

                    //    list.commitChanges();
                }
            }

            //FUNCTION INSERTAR DATO
            function InsertarDato(pDato) {
                var ajaxManager = $find('<%= RadAjaxManager1.ClientID %>');
                ajaxManager.ajaxRequest(pDato);
            }

            //FUNCION ENCAPSULAR DATO
            function EncapsularDatos(pClTipoDato, pLstDatos) {
                return JSON.stringify({ clTipo: pClTipoDato, oSeleccion: pLstDatos });
            }

            //FUNCION DE AGREGAR UN PUESTO SUBORDINADO
            function validarPuestoSubordinado(sender, args) {
                //var vcmbDatos = $find("<= cmbPuestosSubordinado.ClientID %>");
                //if (vcmbDatos._text.length == 0) {
                //    radalert("Selecciona un puesto subordinado.", 400, 150, "Descriptivo de puestos");
                //    args.set_cancel(true);
                //}
            }

            //FUNCION DE AGREGAR UN PUESTO INTERRELACIONADO
            function validarPuestoInterrelacionado(sender, args) {
                //var vcmbDatos = $find("<= cmbPuestosInterrelacionados.ClientID %>");
                //if (vcmbDatos._text.length == 0) {
                //    radalert("Selecciona un puesto interrelacionado.", 400, 150, "Descriptivo de puestos");
                //    args.set_cancel(true);
                //}
            }


            //FUNCION DE AGREGAR UNA RUTA ALTERNATIVA
            function validarRutaAlternativa(sender, args) {
                //var vcmbDatos = $find("<= cmbAlternativa.ClientID %>");
                //if (vcmbDatos._text.length == 0) {
                //    radalert("Selecciona una ruta alternativa.", 400, 150, "Descriptivo de puestos");
                //    args.set_cancel(true);
                //}
            }

            //FUNCION DE AGREGAR UNA RUTA LATERAL
            function validarRutaLateral(sender, args) {
                //var vcmbDatos = $find("<= cmbLateral.ClientID %>");
                //if (vcmbDatos._text.length == 0) {
                //    radalert("Selecciona una ruta lateral.", 400, 150, "Descriptivo de puestos");
                //    args.set_cancel(true);
                //}
            }

            function fixEditor(sender, args) {
                $find("<%= txtDetalleFuncion.ClientID %>").onParentNodeChanged();
                $find("<%= txtNotasFuncion.ClientID %>").onParentNodeChanged();
            }

            var GetAllForms = function () {
                var forms = [];
                forms.push({ idControl: "ctrlAgregarCompetenciaEspecifica", idTrigger: "<%= btnAgregarCompetencia.ClientID %>" });
                forms.push({ idControl: "ctrlAgregarCompetenciaEspecifica", idTrigger: "<%= btnEditarCompetencia.ClientID %>" });
                forms.push({ idControl: "ctrlAgregarIndicadorDesempeno", idTrigger: "<%= btnAgregarIndicador.ClientID%>" });
                forms.push({ idControl: "ctrlAgregarIndicadorDesempeno", idTrigger: "<%= btnEditarIndicador.ClientID%>" });
                return forms;
            }

            var ChangeDisplayStyle = function (element, display) {
                $get(element).style.display = display ? "block" : "none";
            }

            var HideForms = function () {
                GetAllForms().forEach(HideForm);
            }

            var HideForm = function (form) {
                ChangeDisplayStyle(form.idControl, false);
            }

            var ShowForm = function (form) {
                ChangeDisplayStyle(form.idControl, true);
            }

            var filterByTriggerId = function (e) {
                return e.idTrigger == this;
            }

            function ShowFormFromButton(sender, args) {
                HideForms();
                GetAllForms().filter(filterByTriggerId, sender.get_id()).forEach(ShowForm);
            }

            function ShowIndicadorDesempenoEditForm(sender, args) {
                var masterTable = $find("<%= grdFuncionCompetencias.ClientID %>").get_masterTableView();

                var dataItems = masterTable.get_dataItems();
                for (var i = 0; i < dataItems.length; i++) {
                    if (dataItems[i].get_nestedViews().length > 0) {
                        var selectedItem = dataItems[i].get_nestedViews()[0].get_selectedItems()[0];
                        if (selectedItem != undefined) {
                            ShowIndicadorDesempenoForm(sender, args, selectedItem, "Selecciona un indicador.");
                            break;
                        }
                        else
                            radalert("Selecciona un indicador.", 400, 150);
                    }
                    else
                        radalert("Selecciona un indicador.", 400, 150);
                }
            }

            function OnCloseUpdate() {
                var vPuestos = [];
                var vPuesto = {
                    clTipoCatalogo: "CLOSE"
                };
                vPuestos.push(vPuesto);
                sendDataToParent(vPuestos);
            }


            function ShowIndicadorDesempenoInsertForm(sender, args) {
                var masterTable = $find("<%= grdFuncionCompetencias.ClientID %>").get_masterTableView();

                var selectedItem = masterTable.get_selectedItems()[0];
                ShowIndicadorDesempenoForm(sender, args, selectedItem, "Selecciona la competencia a la que se le agregará el indicador.");
            }

            function ShowIndicadorDesempenoForm(sender, args, selectedItem, alertMessage) {
                $find("<%= txtIndicadorDesempeno.ClientID %>").set_value("");
                $find("<%= btnAgregarCompetenciaEspecifica.ClientID %>").set_enabled(false);
                if (selectedItem != undefined)
                    ShowFormFromButton(sender, args);
                else
                    radalert(alertMessage, 400, 150);
            }

            function ConfirmFuncionGenericaDelete(sender, args) {
                var masterTable = $find("<%= grdFuncionesGenericas.ClientID %>").get_masterTableView();
                confirmarEliminar(sender, args, masterTable, false);
            }

            function confirmarEliminar(sender, args, masterTable, isMultiSelection) {
                var selectedItems = masterTable.get_selectedItems();
                if (selectedItems != undefined && selectedItems.length > 0) {
                    var length = 1;

                    if (isMultiSelection)
                        selectedItems.length;

                    var confirmMessage = "¿Deseas eliminar este elemento?";

                    for (var i = 0; i < length; i++) {
                        var vNombre = masterTable.getCellByColumnUniqueName(selectedItems[i], "NB_FUNCION_GENERICA").innerHTML;
                        confirmMessage = String.format("¿Deseas eliminar la función genérica {0}?", vNombre);
                    }

                    confirmar(sender, args, confirmMessage);
                }
                else {
                    radalert("Selecciona una función genérica.", 400, 150);
                    args.set_cancel(true);
                }
            }

            function confirmar(sender, args, text, winProperties) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });

                var wnd = radconfirm(text, callBackFunction, 400, 200, null, "Confirmar");
                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                args.set_cancel(true);
            }

            function ConfirmarEliminarOcupacion(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });
                radconfirm('¿Deseas eliminar la ocupación de este puesto?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar área temática");
                args.set_cancel(true);

            }



            function OpenPreview() {
                var vURL = "VentanaVistaDescriptivo.aspx";
                var vTitulo = "Vista previa descriptivo";

                vURL = vURL + "?PuestoId=" + '<%= vIdDescriptivo %>';
                //var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);

                OpenSelectionWindow(vURL, "winVistaPrevia", vTitulo);

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
        </script>
    </telerik:RadCodeBlock>

    <style>
        .rbToggleButton {
            line-height: 15px;
        }

        .RadSlider_Default .rslHorizontal .rslSelectedregion {
            background: transparent;
            border-color: transparent;
        }

        .RadSlider_Default .rslHorizontal .rslTrack {
            background: transparent;
            border-color: transparent;
        }

        .RadSlider_Default .rslHorizontal .rslItem {
            background: transparent;
        }

        .RadSlider_Default .rslHorizontal a.rslHandle {
            background-image: none;
        }
    </style>

    <div style="height: 100%; padding: 10px 10px 0px 10px;">
        <telerik:RadSplitter ID="splEmpleados" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridEmpleados" runat="server">
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNombreCorto" name="lblNombreCorto">* Clave:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtNombreCorto" Width="310px" MaxLength="20" ToolTip="La clave del puesto es única, no puede repetirse y solo debes registrar letras, números y guiones bajos, y preferentemente en mayúsculas. (Ej. PR_004, TS001_22). Puedes registrar hasta 20 caracteres."></telerik:RadTextBox>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lbldescriptivopuesto">* Nombre:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtDescripcionPuesto" Width="310px" MaxLength="100"></telerik:RadTextBox>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblTxtNoPlazas">No. de plazas:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox ID="txtNoPlazas" runat="server" Width="40" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <telerik:RadTabStrip ID="tbdescriptivospuestos" runat="server" SelectedIndex="0" MultiPageID="mpgdescriptivo">
                    <Tabs>
                        <telerik:RadTab Text="Perfil de ingreso"></telerik:RadTab>
                        <telerik:RadTab Text="Organigrama"></telerik:RadTab>
                 <%--       <telerik:RadTab Text="Responsabilidades"></telerik:RadTab>--%>
                        <telerik:RadTab Text="Funciones"></telerik:RadTab>
                        <telerik:RadTab Text="Competencias genéricas"></telerik:RadTab>
                        <telerik:RadTab Text="Campos extra"></telerik:RadTab>
                        <telerik:RadTab Text="STPS"></telerik:RadTab>
                        <telerik:RadTab Text="Documentación"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 130px); overflow: auto; padding: 10px 0px 10px 0px;">
                    <telerik:RadMultiPage ID="mpgdescriptivo" runat="server" SelectedIndex="0" Height="100%" Width="100%">
                        <!-- Inicio del perfil de ingreso -->
                        <telerik:RadPageView ID="pvwperfilIngreso" runat="server">
                            <div>
                                <div class="ctrlBasico">
                                    <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;
                                    <label id="lblRangoedad" name="lblRangoedad">
                                        Rango de edad:</label>&nbsp;
                                    <telerik:RadNumericTextBox ID="txtRangoEdadMin" runat="server" Width="50" MinValue="15" MaxValue="99" Value="18" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                    &nbsp;
                                    <label id="lblRangoI" name="lblRangoI">a</label>&nbsp;
                                    <telerik:RadNumericTextBox ID="txtRangoEdadMax" runat="server" Width="50" MinValue="15" MaxValue="99" Value="65" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>&nbsp;
                                    <label id="lblRangoF" name="lblRangoF">años de edad</label>
                                </div>
                                <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;
                                        <label id="lblGenero" name="lblGenero">Género: </label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadComboBox runat="server" AutoPostBack="false" MarkFirstMatch="true" EmptyMessage="Selecciona" ID="cmbGenero"></telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="ctrlBasico">
                                    <div class="divControlIzquierda" style="width: 150px">
                                        <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;
                                        <label id="lblEstadoCivil">Estado civil: </label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadComboBox runat="server" ID="cmbEdoCivil" MarkFirstMatch="true" AutoPostBack="false" EmptyMessage="Selecciona"></telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div>
                                <label class="labelTitulo" id="lblescolaridad" name="lblescolaridad">
                                    <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;Escolaridad</label>
                            </div>
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">
                                    <div class="divBarraTitulo">
                                        <label style="float: left">Postgrado</label>
                                        <div style="text-align: right;">
                                            <div class="divControlDerecha">
                                                <telerik:RadComboBox runat="server" ID="radcmbPostgrados" MarkFirstMatch="true" Filter="Contains" EmptyMessage="Selecciona" Width="300" MaxHeight="200" DropDownWidth="450"></telerik:RadComboBox>
                                                <telerik:RadButton runat="server" Text="+" Style="width: 34px; padding: 6px 12px; font-size: 24px;" ID="btnagregarPostgrado" AutoPostBack="true" OnClick="btnagregarPostgrado_Click" OnClientClicking="validarPostgrado" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: left; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstPostgrados" Width="100%" AllowDelete="true" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">
                                    <div class="divBarraTitulo">
                                        <label style="float: left">Carrera profesional</label>
                                        <div style="text-align: right;">
                                            <div class="divControlDerecha">
                                                <telerik:RadComboBox runat="server" ID="cmbCarreraProf" MarkFirstMatch="true" Filter="Contains" EmptyMessage="Selecciona" Width="300" MaxHeight="200" DropDownWidth="450"></telerik:RadComboBox>
                                                <telerik:RadButton runat="server" Style="width: 34px; padding: 6px 12px; font-size: 24px;" Text="+" ID="btnAgregarCarreProfe" AutoPostBack="true" OnClick="btnAgregarCarreProfe_Click" OnClientClicking="validarCarreraProfesional" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: left; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstCarreraprof" Width="100%" AllowDelete="true" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">
                                    <div class="divBarraTitulo">
                                        <label style="float: left">Carrera técnica</label>
                                        <div style="text-align: right;">
                                            <div class="divControlDerecha">
                                                <telerik:RadComboBox runat="server" ID="cmbCarrTec" MarkFirstMatch="true" Filter="Contains" EmptyMessage="Selecciona" Width="300" MaxHeight="200" DropDownWidth="450"></telerik:RadComboBox>
                                                <telerik:RadButton runat="server" Style="width: 34px; padding: 6px 12px; font-size: 24px;" Text="+" ID="radbtnAgregarCarreraTec" AutoPostBack="true" OnClick="radbtnAgregarCarreraTec_Click" OnClientClicking="validarCarreraTec" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: left; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstCarreraTec" Width="100%" AllowDelete="true" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div>
                                <label class="labelTitulo" id="lblcompetencias" name="lblcompetencias">
                                    <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp;Competencias</label>
                            </div>
                            <div class="ctrlBasico">
                                <div class="BorderRadioComponenteHTML" style="width: 520px; float: left;">
                                    <div class="divBarraTitulo" title="Considerar que son experiencia y competencias necesarias para ingresar al puesto. Ejemplo: manejo del idioma inglés y experiencia en la traducción de escritos sencillos inglés-español, ó el manejo de algún sistema. Especificar poncentaje de conocimiento.">
                                        <label style="float: left" title="Considerar que son experiencia y competencias necesarias para ingresar al puesto. Ejemplo: manejo del idioma inglés y experiencia en la traducción de escritos sencillos inglés-español, ó el manejo de algún sistema. Especificar poncentaje de conocimiento.">Competencias específicas necesarias</label>
                                        <div style="text-align: right;">
                                            <div class="divControlDerecha">
                                                <telerik:RadComboBox runat="server" ID="cmbCompetenciaEspecificas" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Selecciona" Width="200" MaxHeight="250" DropDownWidth="450"
                                                    ToolTip="Considerar que son experiencia y competencias necesarias para ingresar al puesto. Ejemplo: manejo del idioma inglés y experiencia en la traducción de escritos sencillos inglés-español, ó el manejo de algún sistema. Especificar poncentaje de conocimiento.">
                                                </telerik:RadComboBox>
                                                <telerik:RadButton runat="server" Style="width: 34px; padding: 6px 12px; font-size: 24px;" Text="+" ID="btnCompEsp" AutoPostBack="true" OnClick="btnCompEsp_Click" OnClientClicking="validarCompetencia" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: left; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstCompetenciasEspecificas" Width="100%" Height="100px" AllowDelete="true" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>


             <%--               <div class="ctrlBasico">
                                <div class="BorderRadioComponenteHTML" style="width: 520px; float: left;">
                                    <div class="divBarraTitulo">
                                        <label style="float: left">Competencias requeridas</label>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: right; width: 100%;">
                                            <telerik:RadTextBox runat="server" ID="txtCompetenciasRequeridas" Width="500px" Height="100px" TextMode="MultiLine"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>

                            <div style="clear: both;"></div>
                            <div>
                                <label class="labelTitulo" id="lblExperiencia" name="lblExperiencia">
                                    <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;Experiencia</label>
                            </div>
                            <div class="ctrlBasico">
                                <div class="BorderRadioComponenteHTML" style="width: 700px; float: left;">
                                    <div class="divBarraTitulo">
                                        <label style="float: left">Experiencia necesaria</label>
                                        <div style="text-align: right; position: relative">
                                            <div class="divControlDerecha">
                                                <telerik:RadButton runat="server" Style="width: 34px; padding: 6px 12px;" Text="+" ID="radBtnAgregarMostrar" AutoPostBack="false" OnClientClicked="MostrarInformacion" data-activo="0" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div id="contenidoInfo">
                                            <div id="divContenido" runat="server" style="display: none;">
                                                <div class="ctrlBasico" style="padding-top: 10px; padding-right: 19px; padding-left: 10px;">
                                                    <div>
                                                        <div class="divControlIzquierda">
                                                            <label id="Label1" name="lblExperiencia">Experiencia </label>
                                                        </div>
                                                        <div class="divControlDerecha">
                                                            <telerik:RadComboBox runat="server" ID="cmbExperiencias" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Selecciona" Width="200" DropDownWidth="450" MaxHeight="250"></telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; height: 5px"></div>
                                                    <div>
                                                        <div class="divControlIzquierda">
                                                            <label id="lblTiempo" name="lblTiempo">Tiempo </label>
                                                        </div>
                                                        <div class="divControlIzquierda" style="width: 50px">
                                                            <telerik:RadNumericTextBox runat="server" ID="txtTiempo" Width="50px" NumberFormat-DecimalDigits="0" />
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; height: 5px"></div>
                                                    <div>
                                                        <div class="divControlIzquierda" style="width: 305px">
                                                            <telerik:RadButton ID="btnRequerida" runat="server" ToggleType="Radio" ButtonType="ToggleButton" AutoPostBack="false" GroupName="RadiosExperiencia">
                                                                <ToggleStates>
                                                                    <telerik:RadButtonToggleState Text="Requerida" />
                                                                    <telerik:RadButtonToggleState Text="Requerida" />
                                                                </ToggleStates>
                                                            </telerik:RadButton>
                                                            <telerik:RadButton ID="btnDeseada" runat="server" ToggleType="Radio" ButtonType="ToggleButton" AutoPostBack="false" GroupName="RadiosExperiencia">
                                                                <ToggleStates>
                                                                    <telerik:RadButtonToggleState Text="Deseada" />
                                                                    <telerik:RadButtonToggleState Text="Deseada" />
                                                                </ToggleStates>
                                                            </telerik:RadButton>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; height: 5px"></div>
                                                    <div>
                                                        <div class="divControlIzquierda" style="width: 287px;">
                                                            <telerik:RadButton runat="server" Text="Agregar" ID="btnAgregarExperiencia" AutoPostBack="true" OnClick="radBtnAgregarExperiencia_Click" OnClientClicking="validarExperiencia" />
                                                            <telerik:RadButton runat="server" Text="Cancelar" ID="btnCancelarExperiencia" AutoPostBack="false" OnClientClicked="CancelarAccionExperiencia" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: left; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstExperiencia" Width="100%" AllowDelete="true" OnDeleted="lstExperiencia_Deleted" ButtonSettings-AreaWidth="35px" AutoPostBackOnDelete="true" AllowAutomaticUpdates="True">
                                                <HeaderTemplate>
                                                    <table style="width: 100%">
                                                        <colgroup>
                                                            <col style="width: 50%">
                                                            <col style="width: 20%">
                                                            <col style="width: 30%">
                                                        </colgroup>
                                                        <tr style="text-align: left">
                                                            <td><b>Experiencia</b></td>
                                                            <td><b>Tiempo/años</b></td>
                                                            <td><b>Tipo</b></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table class="ctrlTableForm" style='width: 100%; table-layout: fixed;'>
                                                        <colgroup>
                                                            <col style='width: 50%'>
                                                            <col style='width: 20%'>
                                                            <col style='width: 30%'>
                                                        </colgroup>
                                                        <tr style='text-align: left'>
                                                            <td><%# DataBinder.Eval(Container, "Text") %></td>
                                                            <td><%# DataBinder.Eval(Container.DataItem, "NO_TIEMPO") %></td>
                                                            <td><%# DataBinder.Eval(Container.DataItem, "CL_TIPO_EXPERIENCIA") %></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div>
                                <label class="labelTitulo" id="lblRequerimientos" name="lblRequerimientos" title="Aquí puedes especificar si la persona que ocupará el puesto necesita aportar vehículo, licencia, material de trabajo, laptop, algún certificado de calidad, certificado de instructor interno, etc.">
                                    <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp; Requerimientos / aportaciones adicionales del puesto (equipo, materiales, etc.)</label>
                            </div>
                            <div class="ctrlBasico" title="Aquí puedes especificar si la persona que ocupará el puesto necesita aportar vehículo, licencia, material de trabajo, laptop, algún certificado de calidad, certificado de instructor interno, etc.">
                                <telerik:RadEditor NewLineMode="Br" Height="100px" Width="100%" ToolsWidth="310px" EditModes="Design" ID="radEditorRequerimientos" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"
                                    ToolTip="Aquí puedes especificar si la persona que ocupará el puesto necesita aportar vehículo, licencia, material de trabajo, laptop, algún certificado de calidad, certificado de instructor interno, etc.">
                                </telerik:RadEditor>
                            </div>
                            <div style="clear: both;"></div>
                            <div style="padding-bottom: 5px">
                                <label class="labelTitulo" id="lblobservaciones" name="lblobservaciones" title="Aquí puedes especificar alguna caraterística especial para el puesto. Ejemplo: la persona debe rolar turnos, la persona debe ser cordial, revisar que la persona tenga disponibilidad para viajar." ><span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>&nbsp;Observaciones</label>
                            </div>
                            <div class="ctrlBasico" title="Aquí puedes especificar alguna caraterística especial para el puesto. Ejemplo: la persona debe rolar turnos, la persona debe ser cordial, revisar que la persona tenga disponibilidad para viajar.">
                                <telerik:RadEditor NewLineMode="Br" Height="100px" Width="100%" ToolsWidth="310px" EditModes="Design" ID="radEditorObservaciones" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"
                                    ToolTip="Aquí puedes especificar alguna caraterística especial para el puesto. Ejemplo: la persona debe rolar turnos, la persona debe ser cordial, revisar que la persona tenga disponibilidad para viajar.">
                                </telerik:RadEditor>
                            </div>
                            <div style="clear: both; height: 25px"></div>
                        </telerik:RadPageView>
                        <!-- Fin de Perfil de ingreso -->

                        <!-- Inicio del organigrama -->
                        <telerik:RadPageView ID="pvwOroganigrama" runat="server">
                            <div class="ctrlBasico">
                                <%--<div class="divControlIzquierda" style="width:180px;">--%>
                                <label id="lbltipoPuesto" name="lbltipoPuesto">
                                    <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp; Tipo de puesto:</label>
                                <%--</div>
                                <div class="divControlIzquierda" style="line-height: 33px; width: 90px">--%>
                                <telerik:RadButton ID="btnDirecto" runat="server" Style="margin-left: 20px;" ToggleType="Radio" ButtonType="ToggleButton" AutoPostBack="false" Checked="true" GroupName="Radios"
                                    ToolTip="Los puestos directos son los que intervienen directamente en el proceso de transformación o servicio,enfocados en la razón de ser de la organización. Ejemplos: en organizaciones de manufactura los puestos operativos; en organizaciones de servicio los puestos de atención al cliente.">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Directo" PrimaryIconCssClass="rbToggleRadioChecked" />
                                        <telerik:RadButtonToggleState Text="Directo" PrimaryIconCssClass="rbToggleRadio" />
                                    </ToggleStates>
                                </telerik:RadButton>
                                <%--</div>
                                <div class="divControlIzquierda" style="line-height: 33px; width: 90px">--%>
                                <telerik:RadButton ID="btnIndirecto" runat="server" Style="margin-left: 20px;" ToggleType="Radio" ButtonType="ToggleButton" AutoPostBack="false" GroupName="Radios" ToolTip="Los puestos indirectos son los que apoyan de manera indirecta los procesos centrales que son la razón de ser de la organización. Ejemplo: puestos de servicio como Recursos Humanos, Sistemas, Calidad, etc.">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Indirecto" PrimaryIconCssClass="rbToggleRadioChecked" />
                                        <telerik:RadButtonToggleState Text="Indirecto" PrimaryIconCssClass="rbToggleRadio" />
                                    </ToggleStates>
                                </telerik:RadButton>
                                <%--</div>--%>
                            </div>
                            <%--      <div style="clear: both;"></div>--%>
                            <div class="ctrlBasico">
                                <%--                     <label id="lblarea" name="lblarea">
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp;
                                </label>--%>
                                <%-- <br />
                                <telerik:RadComboBox runat="server" ID="cmbAreas" AutoPostBack="false" MarkFirstMatch="true" EmptyMessage="Selecciona" Width="200" MaxHeight="200" DropDownWidth="450"
                                    ToolTip="Las áreas son los bloques grandes que existen dentro de una organización. Ejemplo: Dirección de Operaciones, Dirección Administrativa, Dirección Comercial. Los departamentos que existen dentro de estas áreas podemos integrarlos por medio de adscripciones.">
                                </telerik:RadComboBox>--%>
                            </div>
                            <%--                 <div class="ctrlBasico">
                                <label id="lblcentroadministrativo" name="lblcentroadministrativo">Centro administrativo: </label>
                                <br />
                                <telerik:RadComboBox runat="server" ID="cmbAdministrativo" AutoPostBack="false" MarkFirstMatch="true" EmptyMessage="Selecciona" Width="200" MaxHeight="200" DropDownWidth="450"></telerik:RadComboBox>
                            </div>
                            <div class="ctrlBasico">
                                <label id="lblcentrooperativo" name="lblcentrooperativo">Centro operativo: </label>
                                <br />
                                <telerik:RadComboBox runat="server" ID="cmbOperativo" AutoPostBack="false" MarkFirstMatch="true" EmptyMessage="Selecciona" Width="200" MaxHeight="200" DropDownWidth="450"></telerik:RadComboBox>
                            </div>--%>
                            <%--                         <div class="ctrlBasico">
                                <label id="lblpuestoJefeInmediato" name="lblpuestoJefeInmediato">
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>&nbsp;Puesto del jefe inmediato:
                                </label>
                                <br />
                                <telerik:RadListBox ID="lstJefeInmediato" runat="server" ToolTip="Aquí debes especificar el puesto del jefe inmediato, debes asignar sólo uno, es a quien se le reporta directamente.">
                                    <Items>
                                        <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                                    </Items>
                                </telerik:RadListBox>
                                <telerik:RadButton runat="server" ID="btnBuscarJefeInmediato" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenPuestosSelectionWindow" />
                            </div>--%>
                            <div style="clear: both;"></div>
                            <div>
                                <label class="labelTitulo" id="lblTituloPuestos" name="lblTituloPuestos">
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>&nbsp;Puestos</label>
                            </div>
                            <div>
                                <label id="lblpuestoJefeInmediato" name="lblpuestoJefeInmediato" title="Aquí se muestran los puestos de los jefes inmediatos de acuerdo a las plazas.">Jefes(s) inmediato(s):</label>
                                <div style="clear: both"></div>
                                <telerik:RadGrid
                                    ID="rgJefesInmediatos"
                                    runat="server"
                                    Width="660"
                                    Height="300"
                                    ToolTip="Aquí se muestran los puestos de los jefes inmediatos de acuerdo a las plazas."
                                    AllowPaging="true"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-Font-Bold="true"
                                    EnableHeaderContextMenu="true"
                                    OnNeedDataSource="rgJefesInmediatos_NeedDataSource">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <MasterTableView DataKeyNames="ID_PUESTO" ClientDataKeyNames="ID_PUESTO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="CL_PUESTO" DataField="CL_PUESTO" HeaderText="Clave puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="90" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Nombre puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="350" FilterControlWidth="290" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="NO_TOTAL_PLAZAS" DataField="NO_TOTAL_PLAZAS" HeaderText="No. plazas" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="40" CurrentFilterFunction="EqualTo" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="height: 10px; clear: both;"></div>
                            <div>
                                <label id="lblSubordinados" name="lblSubordinados" title="Aquí se muestran los puestos que reportan directamente a este puesto de acuerdo a las plazas.">Puestos que supervisa en forma inmediata:</label>
                                <div style="clear: both"></div>
                                <telerik:RadGrid
                                    ID="rgSubordinados"
                                    runat="server"
                                    Width="660"
                                    Height="300"
                                    ToolTip="Aquí se muestran los puestos que reportan directamente a este puesto de acuerdo a las plazas."
                                    AllowPaging="true"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-Font-Bold="true"
                                    EnableHeaderContextMenu="true"
                                    OnNeedDataSource="rgSubordinados_NeedDataSource">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <MasterTableView DataKeyNames="ID_PUESTO" ClientDataKeyNames="ID_PUESTO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="CL_PUESTO" DataField="CL_PUESTO" HeaderText="Clave puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="90" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Nombre puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="330" FilterControlWidth="270" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="NO_TOTAL_PLAZAS" DataField="NO_TOTAL_PLAZAS" HeaderText="No. plazas" AutoPostBackOnFilter="true" HeaderStyle-Width="120" FilterControlWidth="60" CurrentFilterFunction="EqualTo" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <%--                               <div class="BorderRadioComponenteHTML" style="width: 550px; float: left;">
                                    <div class="divBarraTitulo">
                                        <label style="float: left">Puestos que supervisa en forma inmediata</label>
                                        <div style="text-align: right;">
                                            <div class="divControlDerecha" tabindex="Aquí debes especificar los puestos que reportan directamente a este puesto.">
                                                <telerik:RadComboBox runat="server" ID="cmbPuestosSubordinado" MarkFirstMatch="true" Filter="Contains" EmptyMessage="Selecciona" Width="200" MaxHeight="200" DropDownWidth="450" ToolTip="Aquí debes especificar los puestos que reportan directamente a este puesto."></telerik:RadComboBox>
                                                <telerik:RadButton runat="server" Style="width: 34px; padding: 6px 12px; font-size: 24px;" Text="+" ID="radBtnAgregarPuestoSubordinado" AutoPostBack="true" OnClick="radBtnAgregarPuestoSubordinado_Click" OnClientClicking="validarPuestoSubordinado" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: right; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstPuestosSubordinado" Width="100%" AllowDelete="true" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                            <div style="height: 10px; clear: both;"></div>
                            <div class="ctrlBasico">
                                <label id="lblInterelacionados" name="lblInterelacionados" title="Aquí debes especificar aquellos puestos que intervienen en los procesos centrales del puesto analizado, son los responsables de generar entradas o recibir salidas de estos procesos. Te sugerimos que selecciones máximo cinco.">Puestos interrelacionados:</label>
                                <div style="clear: both"></div>
                                <div class="ctrlBasico" style="width: 450px;">
                                    <telerik:RadGrid
                                        ID="rgInterrelacionados"
                                        runat="server"
                                        Width="450"
                                        Height="350"
                                        ToolTip="Aquí debes especificar aquellos puestos que intervienen en los procesos centrales del puesto analizado, son los responsables de generar entradas o recibir salidas de estos procesos. Te sugerimos que selecciones máximo cinco."
                                        AllowPaging="true"
                                        AutoGenerateColumns="false"
                                        HeaderStyle-Font-Bold="true"
                                        EnableHeaderContextMenu="true"
                                        AllowMultiRowSelection="true"
                                        OnNeedDataSource="rgInterrelacionados_NeedDataSource">
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView DataKeyNames="ID_PUESTO, ID_PUESTO_RELACION" ClientDataKeyNames="ID_PUESTO, ID_PUESTO_RELACION" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="CL_PUESTO" DataField="CL_PUESTO" HeaderText="Clave puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="120" FilterControlWidth="60" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Nombre puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="300" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                                <div class="ctrlBasico" style="float: left">
                                    <telerik:RadButton ID="btnAgregar" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenPuestosInterrelacionadosWindow" ToolTip="Seleccionar puestos interrelacionados"></telerik:RadButton>
                                    <div style="clear: both;"></div>
                                    <telerik:RadButton ID="btnEliminar" runat="server" Text="X" AutoPostBack="true" OnClick="btnEliminar_Click" ToolTip="Eliminar puesto interrelacionado"></telerik:RadButton>
                                </div>
                                <%-- <div class="BorderRadioComponenteHTML" style="width: 450px; float: left;">
                                    <div class="divBarraTitulo">
                                        <label style="float: left">Puestos interrelacionados</label>
                                        <div style="text-align: right;">
                                            <div class="divControlDerecha" title="Aquí debes especificar aquellos puestos que intervienen en los procesos centrales del puesto analizado, son los responsables de generar entradas o recibir salidas de estos procesos. Te sugerimos que selecciones máximo cinco.">
                                                <telerik:RadComboBox runat="server" ID="cmbPuestosInterrelacionados" MarkFirstMatch="true" Filter="Contains" EmptyMessage="Selecciona" Width="200" MaxHeight="200" DropDownWidth="450" 
                                                    ToolTip="Aquí debes especificar aquellos puestos que intervienen en los procesos centrales del puesto analizado, son los responsables de generar entradas o recibir salidas de estos procesos. Te sugerimos que selecciones máximo cinco.    "></telerik:RadComboBox>
                                                <telerik:RadButton runat="server" Style="width: 34px; padding: 6px 12px; font-size: 24px;" Text="+" ID="btnInter" AutoPostBack="true" OnClick="btnInter_Click" OnClientClicking="validarPuestoInterrelacionado" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: right; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstPuestosInterrelacionados" Width="100%" AllowDelete="true" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                            <div style="clear: both;"></div>
                            <div>
                                <label class="labelTitulo" id="lblRutas" name="lblRutas"><span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;Rutas</label>
                            </div>
                            <div class="ctrlBasico">
                                <%--<div class="divControlIzquierda" style="width: 190px">--%>
                                <label id="lblPosicionOrganigrama" name="lblPosicionOrganigrama">Posición en el organigrama:</label>
                                <%--</div>
                                <div class="divControlIzquierda" style="line-height: 33px; width: 80px">--%>
                                <telerik:RadButton ID="btnLinea" runat="server" ToggleType="Radio" ButtonType="ToggleButton" AutoPostBack="false" Checked="true" GroupName="RadiosPosicion" ToolTip="Debes seleccionar esta opción si la posición en el organigrama de este puesto es de línea. Ejemplo: Coordinador de Nómina, Gerente de Producción, Operario Especializado, etc.">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Línea" />
                                        <telerik:RadButtonToggleState Text="Línea" />
                                    </ToggleStates>
                                </telerik:RadButton>
                                <%--</div>
                                <div class="divControlIzquierda" style="line-height: 33px; width: 80px">--%>
                                <telerik:RadButton ID="btnStaff" runat="server" ToggleType="Radio" ButtonType="ToggleButton" AutoPostBack="false" GroupName="RadiosPosicion" ToolTip="Debes seleccionar esta opción si la posición en el organigrama de este puesto es staff. Ejemplo: auxiliares, asistentes, apoyo, etc. ">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Staff" />
                                        <telerik:RadButtonToggleState Text="Staff" />
                                    </ToggleStates>
                                </telerik:RadButton>
                                <%--</div>--%>
                                <%--   <telerik:RadButton ID="btnVerOrganigrama" runat="server" AutoPostBack="false" Text="Ver Organigrama" Height="20" Visible="true" OnClientClicked="OpenVentanaOrganigrama"></telerik:RadButton>--%>
                                <label id="lbNivelOrg" name="lbNivelOrg" style="padding-left: 50px;">Nivel dentro del organigrama:</label>
                                <telerik:RadNumericTextBox ID="txtNivelOrg" runat="server" Width="40" MinValue="1" MaxLength="2" NumberFormat-DecimalDigits="0" ToolTip="Aquí puedes indicar el nivel que tendra el puesto dentro del organigrama."></telerik:RadNumericTextBox>
                            </div>
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <%--<div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">--%>
                                <label id="lblRutaAlternativa" name="lblRutaAlternativa" title="La ruta de crecimiento alternativa debe estar conformada por puestos de un nivel orgánico superior pero que no corresponden a su línea ascendente natural es decir a la de su jefe inmediato. Te sugerimos que no selecciones más de tres.">Ruta de crecimiento alternativa:</label>
                                <div style="clear: both"></div>
                                <div class="ctrlBasico" style="width: 450px;">
                                    <telerik:RadGrid
                                        ID="rgAlternativa"
                                        runat="server"
                                        Width="450"
                                        ToolTip="La ruta de crecimiento alternativa debe estar conformada por puestos de un nivel orgánico superior pero que no corresponden a su línea ascendente natural es decir a la de su jefe inmediato. Te sugerimos que no selecciones más de tres."
                                        Height="350"
                                        AllowPaging="true"
                                        AutoGenerateColumns="false"
                                        HeaderStyle-Font-Bold="true"
                                        EnableHeaderContextMenu="true"
                                        AllowMultiRowSelection="true"
                                        OnNeedDataSource="rgAlternativa_NeedDataSource">
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView DataKeyNames="ID_PUESTO, ID_PUESTO_RELACION" ClientDataKeyNames="ID_PUESTO, ID_PUESTO_RELACION" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="CL_PUESTO" DataField="CL_PUESTO" HeaderText="Clave puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="120" FilterControlWidth="60" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Nombre puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="300" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                                <div class="ctrlBasico" style="float: left">
                                    <telerik:RadButton ID="btnAgregarAlternativa" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenPuestosAlternativosWindow" ToolTip="Seleccionar puestos alternativos"></telerik:RadButton>
                                    <div style="clear: both;"></div>
                                    <telerik:RadButton ID="btnEliminarAlternativa" runat="server" Text="X" AutoPostBack="true" OnClick="btnEliminarAlternativa_OnClick" ToolTip="Eliminar puesto alternativo"></telerik:RadButton>
                                </div>
                                <%-- <div class="divBarraTitulo">
                                        <label style="float: left" title="La ruta de crecimiento alternativa debe estar conformada por puestos de un nivel orgánico superior pero que no corresponden a su línea ascendente natural es decir a la de su jefe inmediato. Te sugerimos que no selecciones más de tres.">Ruta de crecimiento alternativa</label>
                                        <div style="text-align: right;">
                                            <div class="divControlDerecha" tabindex="La ruta de crecimiento alternativa debe estar conformada por puestos de un nivel orgánico superior pero que no corresponden a su línea ascendente natural es decir a la de su jefe inmediato. Te sugerimos que no selecciones más de tres.">
                                                <telerik:RadComboBox runat="server" ID="cmbAlternativa" MarkFirstMatch="true" Filter="Contains" EmptyMessage="Selecciona" Width="200" MaxHeight="200" DropDownWidth="400" 
                                                    ToolTip="La ruta de crecimiento alternativa debe estar conformada por puestos de un nivel orgánico superior pero que no corresponden a su línea ascendente natural es decir a la de su jefe inmediato. Te sugerimos que no selecciones más de tres."></telerik:RadComboBox>
                                                <telerik:RadButton runat="server" Text="+" Style="width: 34px; padding: 6px 12px; font-size: 24px;" ID="btnRutaAlter" AutoPostBack="true" OnClick="btnRutaAlter_Click" OnClientClicking="validarRutaAlternativa" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: right; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstAlternativa" Width="100%" AllowDelete="true" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                                        </div>
                                    </div>--%>
                                <%--</div>--%>
                            </div>
                            <div class="ctrlBasico">
                                <%--  <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">--%>
                                <label id="lblRutaLateral" name="lblRutaLateral" title="La ruta de crecimiento lateral debe estar conformada por puestos del mismo nivel orgánico pero que implicarán desarrollar funciones y/o responsabilidades distintas.  Te sugerimos que no selecciones más de tres.">Ruta de crecimiento lateral:</label>
                                <div style="clear: both"></div>
                                <div class="ctrlBasico" style="width: 450px;">
                                    <telerik:RadGrid
                                        ID="rgLateral"
                                        runat="server"
                                        Width="450"
                                        ToolTip="La ruta de crecimiento lateral debe estar conformada por puestos del mismo nivel orgánico pero que implicarán desarrollar funciones y/o responsabilidades distintas.  Te sugerimos que no selecciones más de tres."
                                        Height="350"
                                        AllowPaging="true"
                                        AutoGenerateColumns="false"
                                        HeaderStyle-Font-Bold="true"
                                        EnableHeaderContextMenu="true"
                                        AllowMultiRowSelection="true"
                                        OnNeedDataSource="rgLateral_NeedDataSource">
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView DataKeyNames="ID_PUESTO, ID_PUESTO_RELACION" ClientDataKeyNames="ID_PUESTO, ID_PUESTO_RELACION" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="CL_PUESTO" DataField="CL_PUESTO" HeaderText="Clave puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="120" FilterControlWidth="60" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Nombre puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="300" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                                <div class="ctrlBasico" style="float: left">
                                    <telerik:RadButton ID="btnAgregarLateral" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenPuestosLateralesWindow" ToolTip="Seleccionar puestos laterales"></telerik:RadButton>
                                    <div style="clear: both;"></div>
                                    <telerik:RadButton ID="btnEliminarLateral" runat="server" Text="X" AutoPostBack="true" OnClick="btnEliminarLateral_Click" ToolTip="Eliminar puesto lateral"></telerik:RadButton>
                                </div>
                                <%--                   <div class="divBarraTitulo">
                                        <label style="float: left" title="La ruta de crecimiento lateral debe estar conformada por puestos del mismo nivel orgánico pero que implicarán desarrollar funciones y/o responsabilidades distintas.  Te sugerimos que no selecciones más de tres.">Ruta de crecimiento lateral</label>
                                        <div style="text-align: right;">
                                            <div class="divControlDerecha" title="La ruta de crecimiento lateral debe estar conformada por puestos del mismo nivel orgánico pero que implicarán desarrollar funciones y/o responsabilidades distintas.  Te sugerimos que no selecciones más de tres.">
                                                <telerik:RadComboBox runat="server" ID="cmbLateral" MarkFirstMatch="true" Filter="Contains" EmptyMessage="Selecciona" Width="200" MaxHeight="200" DropDownWidth="400" 
                                                    ToolTip="La ruta de crecimiento lateral debe estar conformada por puestos del mismo nivel orgánico pero que implicarán desarrollar funciones y/o responsabilidades distintas.  Te sugerimos que no selecciones más de tres. "></telerik:RadComboBox>
                                                <telerik:RadButton runat="server" Text="+" Style="width: 34px; padding: 6px 12px; font-size: 24px;" ID="btnLateral" AutoPostBack="true" OnClick="btnLateral_Click" OnClientClicking="validarRutaLateral" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 5px">
                                        <div class="ctrlBasico" style="text-align: right; width: 100%;">
                                            <telerik:RadListBox runat="server" ID="lstLateral" Width="100%" AllowDelete="true" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                                        </div>
                                    </div>--%>
                                <%--         </div>--%>
                            </div>
                        </telerik:RadPageView>
                        <!-- Fin del organigrama -->

                        <!-- Inicio de responsabilidades y funciones genericas -->
                       <%-- <telerik:RadPageView ID="pvwResponsaFuncionalidadesGener" runat="server">
                            <div title="Aquí debes especificar la razón de ser del puesto de manera genérica. Ejemplo: puesto de Gerente de Producción: es responsable de administrar los programas de producción garantizando que el producto terminado cumpla con las especificaciones del cliente y logrando la satisfacción laboral de su equipo de trabajo, respetando y haciendo respetar las políticas, valores y procedimientos de la empresa.">
                                <label class="labelTitulo" id="lblResponsable" name="lblResponsable" title="Aquí debes especificar la razón de ser del puesto de manera genérica. Ejemplo: puesto de Gerente de Producción: es responsable de administrar los programas de producción garantizando que el producto terminado cumpla con las especificaciones del cliente y logrando la satisfacción laboral de su equipo de trabajo, respetando y haciendo respetar las políticas, valores y procedimientos de la empresa.">
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>&nbsp; Es responsable de:</label>
                            </div>
                            <div title="Aquí debes especificar la razón de ser del puesto de manera genérica. Ejemplo: puesto de Gerente de Producción: es responsable de administrar los programas de producción garantizando que el producto terminado cumpla con las especificaciones del cliente y logrando la satisfacción laboral de su equipo de trabajo, respetando y haciendo respetar las políticas, valores y procedimientos de la empresa.">
                                <telerik:RadEditor NewLineMode="Br" Height="150px" Width="99%" EditModes="Design" ID="radEditorResponsable" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"
                                    ToolTip="Aquí debes especificar la razón de ser del puesto de manera genérica. Ejemplo: puesto de Gerente de Producción: es responsable de administrar los programas de producción garantizando que el producto terminado cumpla con las especificaciones del cliente y logrando la satisfacción laboral de su equipo de trabajo, respetando y haciendo respetar las políticas, valores y procedimientos de la empresa.">
                                </telerik:RadEditor>
                            </div>
                            <div title="Aquí debes especificar la autoridad que posee el puesto en relación a la toma de decisiones. Dirigir: puestos directivos, Gestionar: puestos gerenciales, Coordinar: puestos de jefatura y/o coordinación, Supervisar: puestos de supervisión, Operar: puestos operativos y/o asistentes, auxiliares.">
                                <label class="labelTitulo" id="lblAutoridad" name="lblAutoridad" title="Aquí debes especificar la autoridad que posee el puesto en relación a la toma de decisiones. Dirigir: puestos directivos, Gestionar: puestos gerenciales, Coordinar: puestos de jefatura y/o coordinación, Supervisar: puestos de supervisión, Operar: puestos operativos y/o asistentes, auxiliares.">
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;
                                     Autoridad</label>
                            </div>
                            <div title="Aquí debes especificar la autoridad que posee el puesto en relación a la toma de decisiones. Dirigir: puestos directivos, Gestionar: puestos gerenciales, Coordinar: puestos de jefatura y/o coordinación, Supervisar: puestos de supervisión, Operar: puestos operativos y/o asistentes, auxiliares.">
                                <telerik:RadEditor NewLineMode="Br" Height="150px" Width="99%" ToolsWidth="100%" EditModes="Design" ID="radEditorAutoridad" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"
                                    ToolTip="Aquí debes especificar la autoridad que posee el puesto en relación a la toma de decisiones. Dirigir: puestos directivos, Gestionar: puestos gerenciales, Coordinar: puestos de jefatura y/o coordinación, Supervisar: puestos de supervisión, Operar: puestos operativos y/o asistentes, auxiliares.">
                                </telerik:RadEditor>
                            </div>--%>
                            <%--<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false" />--%>
                       <%-- </telerik:RadPageView>--%>
                        <!-- Fin de responsabilidades y funciones genericas -->

                        <telerik:RadPageView ID="pvwFuncionesGenericas" runat="server">
                                                        <div title="Aquí debes especificar la razón de ser del puesto de manera genérica. Ejemplo: puesto de Gerente de Producción: es responsable de administrar los programas de producción garantizando que el producto terminado cumpla con las especificaciones del cliente y logrando la satisfacción laboral de su equipo de trabajo, respetando y haciendo respetar las políticas, valores y procedimientos de la empresa.">
                                <label class="labelTitulo" id="lblResponsable" name="lblResponsable" title="Aquí debes especificar la razón de ser del puesto de manera genérica. Ejemplo: puesto de Gerente de Producción: es responsable de administrar los programas de producción garantizando que el producto terminado cumpla con las especificaciones del cliente y logrando la satisfacción laboral de su equipo de trabajo, respetando y haciendo respetar las políticas, valores y procedimientos de la empresa.">
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                                    <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>&nbsp; Es responsable de:</label>
                            </div>
                            <div title="Aquí debes especificar la razón de ser del puesto de manera genérica. Ejemplo: puesto de Gerente de Producción: es responsable de administrar los programas de producción garantizando que el producto terminado cumpla con las especificaciones del cliente y logrando la satisfacción laboral de su equipo de trabajo, respetando y haciendo respetar las políticas, valores y procedimientos de la empresa.">
                                <telerik:RadEditor NewLineMode="Br" Height="150px" Width="99%" EditModes="Design" ID="radEditorResponsable" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"
                                    ToolTip="Aquí debes especificar la razón de ser del puesto de manera genérica. Ejemplo: puesto de Gerente de Producción: es responsable de administrar los programas de producción garantizando que el producto terminado cumpla con las especificaciones del cliente y logrando la satisfacción laboral de su equipo de trabajo, respetando y haciendo respetar las políticas, valores y procedimientos de la empresa.">
                                </telerik:RadEditor>
                            </div>

                            <div style="height:10px; clear:both;"></div>
                            <div title="Aquí debes especificar la autoridad que posee el puesto en relación a la toma de decisiones. Dirigir: puestos directivos, Gestionar: puestos gerenciales, Coordinar: puestos de jefatura y/o coordinación, Supervisar: puestos de supervisión, Operar: puestos operativos y/o asistentes, auxiliares.">
                                <label class="labelTitulo" id="lblAutoridad" name="lblAutoridad" title="Aquí debes especificar la autoridad que posee el puesto en relación a la toma de decisiones. Dirigir: puestos directivos, Gestionar: puestos gerenciales, Coordinar: puestos de jefatura y/o coordinación, Supervisar: puestos de supervisión, Operar: puestos operativos y/o asistentes, auxiliares.">
                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;
                                     Autoridad</label>
                            </div>
                            <div title="Aquí debes especificar la autoridad que posee el puesto en relación a la toma de decisiones. Dirigir: puestos directivos, Gestionar: puestos gerenciales, Coordinar: puestos de jefatura y/o coordinación, Supervisar: puestos de supervisión, Operar: puestos operativos y/o asistentes, auxiliares.">
                                <telerik:RadEditor NewLineMode="Br" Height="150px" Width="99%" ToolsWidth="100%" EditModes="Design" ID="radEditorAutoridad" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"
                                    ToolTip="Aquí debes especificar la autoridad que posee el puesto en relación a la toma de decisiones. Dirigir: puestos directivos, Gestionar: puestos gerenciales, Coordinar: puestos de jefatura y/o coordinación, Supervisar: puestos de supervisión, Operar: puestos operativos y/o asistentes, auxiliares.">
                                </telerik:RadEditor>
                            </div>
                               <div style="height:10px; clear:both;"></div>
                             <label class="labelTitulo" id="lblFuncionesGenericas" name="lblFuncionesGenericas">
                            <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                            <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>
                            <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp; 
                             Funciones genéricas
                             </label>

                            <div style="height: calc(100% - 80px);">
                                <telerik:RadGrid ID="grdFuncionesGenericas" runat="server" AutoGenerateColumns="false" Height="100%"
                                    OnNeedDataSource="grdFuncionesGenericas_NeedDataSource"
                                    OnItemDataBound="grdFuncionesGenericas_ItemDataBound" HeaderStyle-Font-Bold="true">
                                    <ClientSettings AllowKeyboardNavigation="true">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="ID_ITEM" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Función Genérica" DataField="NB_FUNCION_GENERICA" HeaderStyle-Width="300" UniqueName="NB_FUNCION_GENERICA"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Funciones Específicas" UniqueName="Funciones">
                                                <ItemTemplate>
                                                    <div><%# Eval("DS_DETALLE") %></div>
                                                    <telerik:RadGrid ID="grdCompetencias" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" OnItemCommand="grdCompetencias_ItemCommand">
                                                        <MasterTableView DataKeyNames="ID_COMPETENCIA">
                                                            <Columns>
                                                                <telerik:GridBoundColumn HeaderText="Competencias específicas" DataField="NB_COMPETENCIA" HeaderStyle-Width="300" UniqueName="NB_COMPETENCIA">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderText="Nivel" DataField="NB_NIVEL" UniqueName="NB_NIVEL"></telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Indicadores desempeño - Evidencias" DataField="DS_INDICADORES" UniqueName="DS_INDICADORES">
                                                                    <ItemTemplate>
                                                                        <%# Eval("DS_INDICADORES") %>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="height: 10px;"></div>
                            <div class="ctrlBasico">

                                <telerik:RadButton ID="btnAgregarFuncionGenerica" runat="server" OnClick="btnAgregarFuncionGenerica_Click" Text="Agregar"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnEditarFuncionGenerica" runat="server" OnClick="btnEditarFuncionGenerica_Click" Text="Editar"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnEliminarFuncionGenerica" runat="server" OnClick="btnEliminarFuncionGenerica_Click" OnClientClicking="ConfirmFuncionGenericaDelete" Text="Eliminar"></telerik:RadButton>
                            </div>

                            <div style="clear: both;"></div>

                        </telerik:RadPageView>

                        <!-- Inicio de competencias genericas -->
                        <telerik:RadPageView ID="pvwCompetenciasGenericas" runat="server">
                            <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                            <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                            <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp;
                           
                              <div style="height: calc(100% - 40px);">
                                  <telerik:RadGrid ID="dgvCompetencias" runat="server" ShowStatusBar="True" AutoGenerateColumns="False" AllowPaging="false" AllowSorting="false" Width="100%" Height="100%"
                                      OnNeedDataSource="dgvCompetencias_NeedDataSource" OnDataBound="dgvCompetencias_DataBound" HeaderStyle-Font-Bold="true">
                                      <ClientSettings>
                                          <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                      </ClientSettings>
                                      <MasterTableView AllowMultiColumnSorting="true" ShowHeadersWhenNoRecords="true" Name="Competencias Genéricas"
                                          Width="100%" DataKeyNames="ID_COMPETENCIA, ID_NIVEL0, ID_NIVEL1, ID_NIVEL2, ID_NIVEL3, ID_NIVEL4, ID_NIVEL5">
                                          <Columns>
                                              <telerik:GridTemplateColumn DataField="NB_CLASIFICACION" HeaderText="Clasificación" UniqueName="NB_CLASIFICACION" ReadOnly="true">
                                                  <ItemTemplate>
                                                      <div style="border: 2px solid <%# Eval("CL_CLASIFICACION_COLOR") %>; height: 100%; width: calc(100% + 15px); margin: -8px; padding: 8px;" title="<%# Eval("DS_CLASIFICACION") %>"><%# Eval("NB_CLASIFICACION") %></div>
                                                  </ItemTemplate>
                                              </telerik:GridTemplateColumn>
                                              <telerik:GridTemplateColumn DataField="NB_COMPETENCIA" HeaderText="Competencia" UniqueName="NB_COMPETENCIA" ReadOnly="true">
                                                  <ItemTemplate>
                                                      <div style="border: 2px solid <%# Eval("CL_CLASIFICACION_COLOR") %>; height: 100%; width: calc(100% + 15px); margin: -8px; padding: 8px;" title="<%# Eval("DS_COMPETENCIA") %>"><%# Eval("NB_COMPETENCIA") %></div>
                                                  </ItemTemplate>
                                              </telerik:GridTemplateColumn>
                                              <telerik:GridBoundColumn DataField="ID_NIVEL0" HeaderText="ID_NIVEL0" UniqueName="ID_NIVEL0" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="ID_NIVEL1" HeaderText="ID_NIVEL1" UniqueName="ID_NIVEL1" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="ID_NIVEL2" HeaderText="ID_NIVEL2" UniqueName="ID_NIVEL2" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="ID_NIVEL3" HeaderText="ID_NIVEL3" UniqueName="ID_NIVEL3" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="ID_NIVEL4" HeaderText="ID_NIVEL4" UniqueName="ID_NIVEL4" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="ID_NIVEL5" HeaderText="ID_NIVEL5" UniqueName="ID_NIVEL5" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                                              <telerik:GridTemplateColumn DataField="NO_VALOR_NIVEL" HeaderText="Nivel" UniqueName="NO_NIVEL_DESEADO">
                                                  <HeaderTemplate>
                                                      <div style="padding-left: 40px;">
                                                          <telerik:RadSlider ID="rsEncabezado" AutoPostBack="false" runat="server" AnimationDuration="400" Skin="Default" ShowDecreaseHandle="false"
                                                              ShowIncreaseHandle="false" Value="5" ShowDragHandle="false" Height="60px" ItemType="item" ThumbsInteractionMode="Free" Width="720">
                                                              <Items>
                                                                  <telerik:RadSliderItem Text="Nivel 0<br />0%<br />No lo necesita" Value="0" />
                                                                  <telerik:RadSliderItem Text="Nivel 1<br />20%<br />Lo necesita poco" Value="1" />
                                                                  <telerik:RadSliderItem Text="Nivel 2<br />40%<br />Lo necesita medio bajo" Value="2" />
                                                                  <telerik:RadSliderItem Text="Nivel 3<br />60%<br />Lo necesita medio alto" Value="3" />
                                                                  <telerik:RadSliderItem Text="Nivel 4<br />80%<br />Lo necesita de manera importante" Value="4" />
                                                                  <telerik:RadSliderItem Text="Nivel 5<br />100%<br />Lo necesita de manera imprescindible" Value="5" />
                                                              </Items>
                                                          </telerik:RadSlider>
                                                      </div>
                                                  </HeaderTemplate>
                                                  <ItemTemplate>
                                                      <div style="border: 2px solid <%# Eval("CL_CLASIFICACION_COLOR") %>; height: 100%; width: calc(100% + 15px); margin: -8px; padding: 8px;">
                                                          <telerik:RadSlider ID="rsNivel1"  AutoPostBack="false" runat="server" AnimationDuration="400" TrackMouseWheel="false"
                                                              Value='<%# decimal.Parse(Eval("NO_VALOR_NIVEL").ToString()) %>' CssClass="ItemsSlider"
                                                              Height="22" ItemType="item" ThumbsInteractionMode="Free" Width="800px">
                                                              <Items>
                                                                  <telerik:RadSliderItem Text="0" Value="0" Height="22" CssClass="lblNivel" />
                                                                  <telerik:RadSliderItem Text="1" Value="1" CssClass="lblNivel" />
                                                                  <telerik:RadSliderItem Text="2" Value="2" CssClass="lblNivel" />
                                                                  <telerik:RadSliderItem Text="3" Value="3" CssClass="lblNivel" />
                                                                  <telerik:RadSliderItem Text="4" Value="4" CssClass="lblNivel" />
                                                                  <telerik:RadSliderItem Text="5" Value="5" CssClass="lblNivel" />
                                                              </Items>
                                                          </telerik:RadSlider>
                                                      </div>
                                                  </ItemTemplate>
                                              </telerik:GridTemplateColumn>
                                          </Columns>
                                      </MasterTableView>
                                  </telerik:RadGrid>
                              </div>

                        </telerik:RadPageView>
                        <!-- Fin de competencias genericas -->

                        <telerik:RadPageView ID="pvwCamposExtras" runat="server">
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda" style="width: 200px;">
                                    <label id="lblTipoPrestacion" name="lblTipoPrestacion"><span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span> Tipo de prestación: </label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadComboBox runat="server" ID="cmbTipoPrestaciones" AutoPostBack="false" MarkFirstMatch="true" EmptyMessage="Selecciona" Width="200" Height="100">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="De ley" Value="DE LEY" />
                                            <telerik:RadComboBoxItem Text="Superiores a las de la ley" Value="SUPERIORES" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <div style="padding-bottom: 5px">
                                    <label id="lblPrerstaciones" name="lblPrerstaciones">Prestaciones:</label>
                                </div>
                                <div style="clear: both"></div>
                                <div>
                                    <telerik:RadEditor NewLineMode="Br" Height="150px" Width="99%" EditModes="Design" ID="radEditorPrestaciones" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                                </div>
                            </div>
                        </telerik:RadPageView>

                        <!-- Inicio de ocupaciones -->
                        <telerik:RadPageView ID="pvwOcupaciones" runat="server">
                            <div class="ctrlBasico">
                                <label id="lblAreaO" name="lblAreaO">Área/Departamento: </label>
                                <br />
                                <telerik:RadComboBox ID="cmbAreaO" Skin="Bootstrap" name="cmbAreaO" ToolTip="Campo obligatorio" CssClass="textbox"
                                    runat="server"
                                    DropDownWidth="450"
                                    Width="450px"
                                    MarkFirstMatch="true"
                                    EmptyMessage="Selecciona"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="cmbAreaO_SelectedIndexChanged"
                                    EnableLoadOnDemand="true"
                                    ValidationGroup="vAreaO">
                                </telerik:RadComboBox>
                            </div>
                            <div class="ctrlBasico">
                                <label id="lblSubarea" name="lblSubarea">Sub-área: </label>
                                <br />
                                <telerik:RadComboBox ID="cmbSubarea" Skin="Bootstrap" name="cmbSubarea" ToolTip="Campo obligatorio" CssClass="textbox"
                                    runat="server"
                                    Width="450px"
                                    DropDownWidth="450"
                                    MarkFirstMatch="true"
                                    EmptyMessage="Selecciona"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="cmbSubarea_SelectedIndexChanged"
                                    EnableLoadOnDemand="true"
                                    ValidationGroup="vAreaO">
                                </telerik:RadComboBox>
                            </div>
                            <div style="clear: both; height: 20px;"></div>
                            <div class="ctrlBasico">
                                <label id="lblModulo" name="lblModulo">Módulo: </label>
                                <br />
                                <telerik:RadComboBox ID="cmbModulo" Skin="Bootstrap" name="cmbModulo" ToolTip="Campo obligatorio" CssClass="textbox"
                                    runat="server"
                                    Width="450px"
                                    DropDownWidth="450"
                                    MarkFirstMatch="true"
                                    EmptyMessage="Selecciona"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="cmbModulo_SelectedIndexChanged"
                                    EnableLoadOnDemand="true"
                                    ValidationGroup="vAreaO">
                                </telerik:RadComboBox>
                            </div>
                            <div class="ctrlBasico">
                                <label id="lblOcupacion" name="lblOcupacion">Ocupación:</label>
                                <br />
                                <telerik:RadComboBox ID="cmbOcupaciones" Skin="Bootstrap" name="cmbModulo" ToolTip="Campo obligatorio" CssClass="textbox"
                                    runat="server"
                                    Width="450px"
                                    DropDownWidth="450"
                                    MarkFirstMatch="true"
                                    EmptyMessage="Selecciona"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="cmbOcupaciones_SelectedIndexChanged"
                                    EnableLoadOnDemand="true"
                                    ValidationGroup="vAreaO">
                                </telerik:RadComboBox>
                            </div>
                            <div style="clear: both; height: 30px;"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierdaAT">
                                    <label id="lblCveOcupacion" name="lblCveOcupacion" runat="server">Clave de la ocupación:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadLabel runat="server" ID="lblClOcupación"></telerik:RadLabel>
                                </div>
                            </div>
                            <div style="clear: both; height: 10px;"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierdaAT">
                                    <label id="lblOcupacionS" name="lblOcupacionS" runat="server">Ocupación seleccionada:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadLabel runat="server" ID="lblOcupacionSeleccionada"></telerik:RadLabel>
                                </div>
                            </div>

                            <div style="clear: both;"></div>
                            <div style="float: right; padding-right: 10px; padding-bottom: 10px;">
                                <telerik:RadButton runat="server" ID="btnEliminarOcupacionPuesto" OnClick="btnEliminarOcupacionPuesto_Click"
                                    OnClientClicking="ConfirmarEliminarOcupacion" Text="Eliminar ocupación" Width="170" ToolTip="Eliminar la ocupación del puesto">
                                </telerik:RadButton>
                            </div>
                            <div style="clear: both; height: 5px;"></div>
                            <div style="padding: 10px; text-align: justify; font-size: small;">
                                <p>
                                    "Nota: Es importante que seleccione una ocupación. Esto es para poder relacionar el puesto con la ocupación del 
                                    Catálogo Nacional de Ocupaciones y facilitar la solicitud de constancias de competencias o habilidades a la Secretaría 
                                    del Trabajo y Previsión Social."
                                </p>
                            </div>

                            <div style="clear: both;"></div>
                        </telerik:RadPageView>
                        <!-- Fin de Ocupaciones -->

                        <!-- Inicio de doscumentos -->
                        <telerik:RadPageView ID="pvwDocumentos" runat="server">
                            <label class="labelTitulo">Documentación</label>
                            <div class="ctrlBasico">
                                Tipo documento:<br />
                                <telerik:RadComboBox ID="cmbTipoDocumento" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Imagen" Value="IMAGEN" />
                                        <telerik:RadComboBoxItem Text="Otro" Value="OTRO" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                            <div class="ctrlBasico">
                                Subir documento:<br />
                                <telerik:RadAsyncUpload ID="rauDocumento" runat="server" MultipleFileSelection="Disabled">
                                    <Localization Select="Seleccionar" />
                                </telerik:RadAsyncUpload>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnAgregarDocumento" runat="server" Text="Agregar" OnClick="btnAgregarDocumento_Click"></telerik:RadButton>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadGrid ID="grdDocumentos" runat="server" OnNeedDataSource="grdDocumentos_NeedDataSource" Width="580" HeaderStyle-Font-Bold="true">
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
                            </div>
                        </telerik:RadPageView>

                        <!-- Fin de doscumentos -->

                    </telerik:RadMultiPage>
                </div>
                <div class="divControlDerecha">

                    <%--       <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnVistaPrevia" Text="Vista previa" AutoPostBack="false" OnClientClicked="OpenPreview"></telerik:RadButton>
                    </div>--%>

                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" AutoPostBack="true" OnClick="btnGuardar_Click" ToolTip="Guardar la descripción del puesto." />
                    </div>

                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnGuardarCerrar" Text="Guardar y cerrar" AutoPostBack="true" OnClick="btnGuardarCerrar_Click" ToolTip="Guardar la descripción del puesto y cerrar la ventana." />
                    </div>

                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" ToolTip="Cancela el proceso" AutoPostBack="false" OnClientClicked="OnCloseUpdate" />
                    </div>

                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="slpAdvSearch" runat="server" Title="Control de documentos" Width="500" MinWidth="500">
                        <div style="padding: 20px;">
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblClaveDocumento" name="lblClaveDocumento">Clave del documento:</label>
                                </div>
                                <div class="divControlDerecha" title="Aquí podras integrar la clave del formato que tienes registrada en el Sistema de Gestión de Calidad.">
                                    <telerik:RadTextBox runat="server" ID="txtClaveDocumento" Width="280px"
                                        ToolTip="Aquí podras integrar la clave del formato que tienes registrada en el Sistema de Gestión de Calidad.">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblVersion" name="lblVersion">Versión:</label>
                                </div>
                                <div class="divControlDerecha" tabindex="Aquí debes especificar los cambios que ha tenido el formato. Ejemplo: versión 00, versión 01, versión 02, versión A, versión B, etc.">
                                    <telerik:RadTextBox runat="server" ID="txtVersionDocumento" Width="280px" ToolTip="Aquí debes especificar los cambios que ha tenido el formato. Ejemplo: versión 00, versión 01, versión 02, versión A, versión B, etc. "></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblFechaElaboracion" name="lblFechaElaboracion">Fecha Elaboración:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <%-- <telerik:RadTextBox runat="server" ID="txtFeElabDocumento" Width="280px" InputType="Date" ToolTip="Aquí debes especificar la fecha de creación del descriptivo de puesto."></telerik:RadTextBox>--%>
                                    <telerik:RadDatePicker ID="rdtFeElabDocumento" Width="150px" MinDate="1930/01/01" runat="server" ToolTip="Aquí debes especificar la fecha de creación del descriptivo de puesto."></telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblNombreElaboro" name="lblNombreElaboro">Elaboró:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox runat="server" ID="txtElaboroDocumento" Width="280px" ToolTip="Aquí debes especificar el nombre de la persona que elaboró el descriptivo de puesto, puedes poner entre paréntesis el puesto que ocupa. "></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblFechaRevision" name="lblFechaRevision">Fecha de revisón:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <%--<telerik:RadTextBox runat="server" ID="txtFeRevDocumento" Width="280px" InputType="Date" ToolTip="Aquí debes especificar la fecha con los últimos cambios dentro del descriptivo de puesto."></telerik:RadTextBox>--%>
                                    <telerik:RadDatePicker ID="rdtFeRevDocumento" MinDate="1930/01/01" Width="150px" runat="server" ToolTip="Aquí debes especificar la fecha con los últimos cambios dentro del descriptivo de puesto."></telerik:RadDatePicker>

                                </div>
                            </div>
                            <br />
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblNombreReviso" name="lblNombreReviso">Revisó:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox runat="server" ID="txtRevisoDocumento" Width="280px"></telerik:RadTextBox>
                                </div>
                            </div>
                            <br />
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblFechaAutorizacion" name="lblFechaAutorizacion">Fecha de autorización:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <%--<telerik:RadTextBox runat="server" ID="txtFeAutorizoDocumento" Width="280px" InputType="Date"></telerik:RadTextBox>--%>
                                    <telerik:RadDatePicker ID="rdtFeAutorizoDocumento" MinDate="1930/01/01" Width="150px" runat="server" ToolTip="Aquí debes especificar la fecha de autorización del descriptivo de puesto."></telerik:RadDatePicker>
                                </div>
                            </div>
                            <br />
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblNombreAutorizo" name="lblNombreAutorizo">Autorizó:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox runat="server" ID="txtAutorizoDocumento" Width="280px" ToolTip="Aquí debes especificar el nombre de la persona que autorizó la información que se intregró en el descriptivo de puesto, puedes poner entre paréntesis el puesto que ocupa."></telerik:RadTextBox>
                                </div>
                            </div>
                            <br />
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblControlCambios" name="lblControlCambios">Control de cambios:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox runat="server" ID="txtControlCambios" Width="280px"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindow ID="winFuncionesGenericas" runat="server" Width="1000" Height="530" Behaviors="Close" Title="Función genérica" VisibleStatusbar="false" Modal="true" OnClientShow="fixEditor" VisibleOnPageLoad="false" DestroyOnClose="true">
        <ContentTemplate>
            <div style="width: calc(100% - 2px); padding: 10px; height: 100%">
                <telerik:RadTabStrip ID="rtsFuncionGenerica" runat="server" SelectedIndex="0" MultiPageID="rmpFuncionGenerica">
                    <Tabs>
                        <telerik:RadTab Text="Definición"></telerik:RadTab>
                        <telerik:RadTab Text="Competencias e indicadores"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 90px);">
                    <div style="padding-top: 10px; height: 100%;">
                        <telerik:RadMultiPage ID="rmpFuncionGenerica" runat="server" SelectedIndex="0" Height="100%">
                            <telerik:RadPageView ID="rpvFuncionGenericaDefinicion" runat="server">
                                <div style="padding-bottom: 10px;">
                                    <label id="Label2" name="lblNbFuncion">Descripción de la función:</label>
                                    <telerik:RadTextBox ID="txtNbFuncion" runat="server" Width="600" MaxLength="200" ToolTip="Son los principales QUÉ, responde a la pregunta ¿Qué hace? Son las áreas de resultado. Cada función genérica deberá comenzar con un verbo en infinitivo. Ejemplo: Coordinar, desarrollar, dirigir, realizar, operar, etc."></telerik:RadTextBox>
                                </div>
                                <div style="padding-bottom: 10px;">
                                    <label class="labelTitulo" name="lblDetalleFuncion" title="Las actividades específicas responden a la pregunta ¿CÓMO LO HACE? Respecto a la función genérica, se puede responder a la pregunta definiendo los pasos más importantes a seguir. Cada actividad específica debe comenzar con un verbo en activo. Ejemplo: Verifica, supervisa, coordina, desarrolla, elabora, captura, registra, etc.">Detallar las funciones específicas</label>
                                    <telerik:RadEditor NewLineMode="Br" Height="125px" Width="100%" ToolsWidth="310px" EditModes="Design" ID="txtDetalleFuncion" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml" ToolTip="Las actividades específicas responden a la pregunta ¿CÓMO LO HACE? Respecto a la función genérica, se puede responder a la pregunta definiendo los pasos más importantes a seguir. Cada actividad específica debe comenzar con un verbo en activo. Ejemplo: Verifica, supervisa, coordina, desarrolla, elabora, captura, registra, etc."></telerik:RadEditor>
                                </div>
                                <div>
                                    <label class="labelTitulo" name="lblNotasFuncion">Notas</label>
                                    <telerik:RadEditor NewLineMode="Br" Height="125px" Width="100%" ToolsWidth="310px" EditModes="Design" ID="txtNotasFuncion" runat="server" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvFuncionGenericaCompetencias" runat="server">
                                <label class="labelTitulo" id="lblTituloCompetencias" name="lblTituloCompetencias">Competencias</label>
                                <div id="ctrlAgregarCompetenciaEspecifica" style="display: none; padding: 10px; border: 1px solid lightgray; border-radius: 5px;">
                                    <div class="ctrlBasico">
                                        <label id="lblNbCompetenciaABC" name="lblNbCompetenciaABC" title="Selecciona las competencias específicas que requiere para desempeñar adecuadamente la función y establecer el nivel de competencia. Aquí debes especificar lo que se debe SABER HACER para lograr la función anterior, referido generalmente a un conocimiento  específico del puesto y/o empresa. Ejemplo: Es capaz de desarrollar planes y programas de capacitación, es capaz de formar instructores internos, es capaz de operar la máquina laser, etc.">Competencia específica:</label>
                                        <telerik:RadComboBox ID="cmbCompetenciaEspecifica" Width="350" runat="server" OnSelectedIndexChanged="cmbCompetenciaEspecifica_SelectedIndexChanged" EmptyMessage="Seleccione..." AutoPostBack="true" ToolTip="Selecciona las competencias específicas que requiere para desempeñar adecuadamente la función y establecer el nivel de competencia. Aquí debes especificar lo que se debe SABER HACER para lograr la función anterior, referido generalmente a un conocimiento  específico del puesto y/o empresa. Ejemplo: Es capaz de desarrollar planes y programas de capacitación, es capaz de formar instructores internos, es capaz de operar la máquina laser, etc."></telerik:RadComboBox>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadGrid ID="grdNivelCompetenciaEspecifica" runat="server" OnPreRender="grdNivelCompetenciaEspecifica_PreRender" HeaderStyle-Font-Bold="true">
                                            <ClientSettings Selecting-AllowRowSelect="true"></ClientSettings>
                                            <MasterTableView DataKeyNames="NO_VALOR" AutoGenerateColumns="false">
                                                <Columns>
                                                    <telerik:GridBoundColumn HeaderText="Nivel de la competencia" DataField="NB_NIVEL" UniqueName="NB_NIVEL"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Descripción del nivel" DataField="DS_NIVEL" UniqueName="DS_NIVEL"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnAgregarCompetenciaEspecifica" runat="server" Text="Aceptar" Enabled="false" OnClick="btnAgregarCompetenciaEspecifica_Click" OnClientClicking="HideForms" ></telerik:RadButton>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnCancelarAgregarCompetenciaEspecifica" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="HideForms"></telerik:RadButton>
                                    </div>
                                    <div style="clear: both;"></div>
                                </div>
                                <div id="ctrlAgregarIndicadorDesempeno" style="display: none; padding: 10px; border: 1px solid lightgray; border-radius: 5px;">
                                    <div class="ctrlBasico">
                                        <label id="lblNbIndicadorABC" name="lblNbIndicadorABC" title="¿Cómo se demuestra la competencia anterior?, ¿Cómo se da cuenta el jefe que se está desarrollando de manera efectiva la competencia? Puede haber uno o varios indicadores de desempeño (evidencias) en las competencias. Ejemplo: Programa de producción, Encuesta de satisfacción, Margen de utilidad, Costo, Avance del programa de capacitación, etc." >Indicadores de desepempeño (evidencias):</label>
                                        <telerik:RadTextBox ID="txtIndicadorDesempeno" runat="server" Width="400" ToolTip="¿Cómo se demuestra la competencia anterior?, ¿Cómo se da cuenta el jefe que se está desarrollando de manera efectiva la competencia? Puede haber uno o varios indicadores de desempeño (evidencias) en las competencias. Ejemplo: Programa de producción, Encuesta de satisfacción, Margen de utilidad, Costo, Avance del programa de capacitación, etc. "></telerik:RadTextBox>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnAgregarIndicadorDesempeno" runat="server" Text="Aceptar" OnClick="btnAgregarIndicadorDesempeno_Click" OnClientClicking="HideForms"></telerik:RadButton>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnCancelarAgregarIndicadorDesempeno" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="HideForms"></telerik:RadButton>
                                    </div>
                                    <div style="clear: both;"></div>
                                </div>
                                <div style="clear: both;"></div>
                                <div style="height: calc(100% - 100px);">
                                    <telerik:RadGrid ID="grdFuncionCompetencias" runat="server" AutoGenerateColumns="false" Height="300" ShowHeader="true" EnableHierarchyExpandAll="true"
                                        OnDetailTableDataBind="grdFuncionCompetencias_DetailTableDataBind"
                                        OnNeedDataSource="grdFuncionCompetencias_NeedDataSource"
                                        OnItemCommand="grdFuncionCompetencias_ItemCommand" HeaderStyle-Font-Bold="true">
                                        <ClientSettings>
                                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView Name="mtvCompetencias" DataKeyNames="ID_ITEM" ShowHeader="true" ShowHeadersWhenNoRecords="true">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="Competencia" DataField="NB_COMPETENCIA" HeaderStyle-Width="300" UniqueName="NB_COMPETENCIA"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Nivel" DataField="NB_NIVEL" UniqueName="NB_NIVEL"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Indicadores" DataField="DS_INDICADORES" UniqueName="DS_INDICADORES">
                                                    <ItemTemplate>
                                                        <%# Eval("DS_INDICADORES") %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <DetailTables>
                                                <telerik:GridTableView Name="dgvIndicadores" DataKeyNames="ID_ITEM" runat="server">
                                                    <ParentTableRelation>
                                                        <telerik:GridRelationFields DetailKeyField="ID_PARENT_ITEM" MasterKeyField="ID_ITEM" />
                                                    </ParentTableRelation>
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Indicador" DataField="NB_INDICADOR_DESEMPENO"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </telerik:GridTableView>
                                            </DetailTables>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <div style="clear: both;"></div>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnAgregarCompetencia" runat="server" Text="Agregar competencia" AutoPostBack="false" OnClientClicked="ShowFormFromButton"></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnEditarCompetencia" runat="server" Text="Editar competencia" OnClientClicking="ShowIndicadorDesempenoInsertForm" OnClick="btnEditarCompetencia_Click"></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnEliminarCompetencia" runat="server" Text="Eliminar competencia" OnClick="btnEliminarCompetencia_Click"></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnAgregarIndicador" runat="server" Text="Agregar indicador" AutoPostBack="false" OnClientClicked="ShowIndicadorDesempenoInsertForm" ToolTip="Aquí puedes agregar uno o más indicadores de acuerdo a la función genérica seleccionada."></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnEditarIndicador" runat="server" Text="Editar indicador" OnClientClicking="ShowIndicadorDesempenoEditForm" OnClick="btnEditarIndicador_Click"></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnEliminarIndicador" runat="server" Text="Eliminar indicador" OnClick="btnEliminarIndicador_Click"></telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                        <div style="clear: both;"></div>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <div style="clear: both;"></div>
                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarFuncionGenerica" runat="server" Text="Aceptar" OnClick="btnGuardarFuncionGenerica_Click" ToolTip="Guardar los cambios realizados y regresar a la pantalla anterior."></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnCancelarGuardarFuncionGenerica" runat="server" Text="Cancelar" ToolTip="No guardar modificaciones realizadas y regresar a la pantalla anterior."></telerik:RadButton>
                    </div>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>




