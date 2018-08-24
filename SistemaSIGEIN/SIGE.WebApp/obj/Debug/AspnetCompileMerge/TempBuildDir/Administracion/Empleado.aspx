<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="SIGE.WebApp.Administracion.Empleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
<style type="text/css">
       .ruBrowse
       {
           
           width: 150px !important;
       }

               .DivFotoCss {
           background: #fafafa; 
           position: absolute; 
           right: 0px; 
           margin-right: 50px; 
           border: 1px solid lightgray; 
           border-radius: 10px; 
           padding: 5px;
        }

         @media only screen and (max-width: 700px) {
            .DivFotoCss {
               background: #fafafa; 
               position: relative;
               right: 0px; 
               margin-right: 50px; 
               border: 1px solid lightgray; 
               border-radius: 10px; 
               padding: 5px;
               width:170px;
            }
        }

   </style>

    <script id="MyScript" type="text/javascript">

        function OpenResultadosPsicometrias() {

            var vIdBateria = '<%= ObtenerDatosDeReportes("ID_BATERIA") %>';
            var vClToken = '<%= ObtenerDatosDeReportes("CL_TOKEN") %>';

            if ((vIdBateria != "0")) {
                var win = window.open("/IDP/ResultadosPruebas.aspx?ID=" + vIdBateria + "&&T=" + vClToken, '_blank');
                win.focus();
            }
            else { radalert("La persona no tiene una batería de pruebas asociada", 400, 150, ""); }

        }

        function openConsultasPersonales() {
            var vIdBateria = '<%= ObtenerDatosDeReportes("ID_BATERIA") %>';
            var vClToken = '<%= ObtenerDatosDeReportes("CL_TOKEN") %>';

            if (vIdBateria != "0") {

                var vURL = "/IDP/ConsultasPersonales.aspx";
                var vTitulo = "Consultas personales";

                vURL = vURL + "?pIdBateria=" + vIdBateria;

                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 20,
                    height: browserWnd.innerHeight - 20
                };

                var wnd = openChildDialog(vURL, "rwConsultas", vTitulo, windowProperties);
            }
            else {
                radalert("El empleado no tiene una batería de pruebas asociada", 400, 150, "");
            }
        }

        function OpenSolicitud() {
            var vIdSolicitud = '<%= vIdSolicitud%>';
            if (vIdSolicitud != null && vIdSolicitud != "0") {
                var vURL = "/IDP/Solicitud/Solicitud.aspx";
                var vTitulo = "Ver solicitud";
                vURL = vURL + "?SolicitudId=" + vIdSolicitud +"&FG_HABILITADO=False";
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 20,
                    height: browserWnd.innerHeight - 20
                };
                openChildDialog(vURL, "winSolicitud", vTitulo, windowProperties);
            }
        }


        

        function OpenProceso() {
            var vIdBateria = '<%= ObtenerDatosDeReportes("ID_BATERIA") %>';
            var vClToken = '<%= ObtenerDatosDeReportes("CL_TOKEN") %>';
            var vIdSolicitud = '<%= vIdSolicitud%>';
            var vIdCandidato = '<%= vIdCandidato%>';
            if (vIdSolicitud != null && vIdSolicitud != "0") {

                var vURL = "/IDP/VentanaProcesoSeleccionCandidato.aspx";
                var vTitulo = "Proceso de evaluación";
                vURL = vURL + "?IdCandidato=" + vIdCandidato

                if (vIdBateria != null) {
                    vURL = vURL + "&IdBateria=" + vIdBateria;
                }

                if (vClToken != null) {
                    vURL = vURL + "&ClToken=" + vClToken;
                }

                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 300,
                    height: browserWnd.innerHeight - 20
                };

                openChildDialog(vURL, "rwListaProcesoSeleccion", vTitulo, windowProperties);
            }
        }


        function openCuestionarioClima() {
            var vIdEvaluador = '<%= ObtenerDatosDeReportes("ID_EVALUADO_CLIMA") %>';
            var vIdPeriodo = '<%= ObtenerDatosDeReportes("ID_PERIODO_CLIMA") %>';

            if (vIdEvaluador != "0" & vIdPeriodo != "0") {

                var vURL = "/EO/Cuestionarios/CuestionarioClimaLaboral.aspx?ID_EVALUADOR=" + vIdEvaluador + "&ID_PERIODO=" + vIdPeriodo + "&FG_HABILITADO=False";
                var vTitulo = "Cuestionario";

                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 20,
                    height: browserWnd.innerHeight - 20
                };

                var wnd = openChildDialog(vURL, "WinCuestionario", vTitulo, windowProperties);
            }
            else {
                radalert("El empleado no tiene una cuestionario de clima asociado", 400, 150, "");
            }
        }

       

        function openAvanceProgramaCapacitacion(pIdPrograma) {

            var vURL = "/FYD/VentanaAvanceProgramaCapacitacion.aspx?IdPrograma=";
            var vTitulo = "Avance programa de capacitación";

            vURL = vURL + pIdPrograma;

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            var wnd = openChildDialog(vURL, "winPrograma", vTitulo, windowProperties);
        }



        function openReporteCumplimientoPersonalDesempeno(pIdEvaluado, pIdPeriodo) {

            var vURL = "/EO/VentanaReporteCumplimientoPersonal.aspx?idPeriodo=" + pIdPeriodo + "&idEvaluado=" + pIdEvaluado;
            var vTitulo = "Reporte cumplimiento personal";

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            var wnd = openChildDialog(vURL, "winReporteCumplimientoPersonal", vTitulo, windowProperties);
        }


        function closeWindow() {
            GetRadWindow().close();
        }

        function OnClientBeforeClose(sender, args) {

            function confirmCallback(arg) {
                if (arg) {
                    OnCloseUpdate();
                }
            }

            radconfirm("¿Estás seguro que quieres salir de la pantalla? Si no has guardado los cambios se perderán", confirmCallback, 400, 170, null, "Cerrar");
        }

        function OpenSelectionWindow(sender, args) {
            var vLstEstados = $find("<%= ObtenerClientId(mpgPlantilla, "NB_ESTADO") %>");
            var vBtnEstados = $find("<%= ObtenerClientId(mpgPlantilla, "btnNB_ESTADO") %>");
            var vLstMunicipios = $find("<%= ObtenerClientId(mpgPlantilla, "NB_MUNICIPIO") %>");
            var vBtnMunicipios = $find("<%= ObtenerClientId(mpgPlantilla, "btnNB_MUNICIPIO") %>");
            var vLstColonia = $find("<%= ObtenerClientId(mpgPlantilla, "NB_COLONIA") %>");
            var vBtnColonia = $find("<%= ObtenerClientId(mpgPlantilla, "btnNB_COLONIA") %>");
            var vLstCandidato = $find("<%= ObtenerClientId(mpgPlantilla, "ID_CANDIDATO") %>");
            var vBtnCandidato = $find("<%= ObtenerClientId(mpgPlantilla, "btnID_CANDIDATO") %>");
            var vLstPlaza = $find("<%= ObtenerClientId(mpgPlantilla, "ID_PUESTO") %>");
            var vBtnPlaza = $find("<%= ObtenerClientId(mpgPlantilla, "btnID_PUESTO") %>");
            var vLstPlazaJefe = $find("<%= ObtenerClientId(mpgPlantilla, "ID_PLAZA_JEFE") %>");
            var vBtnPlazaJefe = $find("<%= ObtenerClientId(mpgPlantilla, "btnID_PLAZA_JEFE") %>");
            var vBtnCP = $find("<%= ObtenerClientId(mpgPlantilla, "btnCL_CODIGO_POSTAL")%>");
            var vLstCentroOperativo = $find("<%= ObtenerClientId(mpgPlantilla, "CL_CENTRO_OPERATIVO") %>");
            var vBtnCentroOperativo = $find("<%= ObtenerClientId(mpgPlantilla, "btnCL_CENTRO_OPERATIVO") %>");
            var vLstCentroAdministrativo = $find("<%= ObtenerClientId(mpgPlantilla, "CL_CENTRO_ADMINISTRATIVO") %>");
            var vBtnCentroAdministrativo = $find("<%= ObtenerClientId(mpgPlantilla, "btnCL_CENTRO_ADMINISTRATIVO") %>");
            var vLstEstadosNacimiento = $find("<%= ObtenerClientId(mpgPlantilla, "NB_ESTADO_NACIMIENTO") %>");
            var vBtnEstadosNacimiento = $find("<%= ObtenerClientId(mpgPlantilla, "btnNB_ESTADO_NACIMIENTO") %>");

            var windowProperties = {
                width: 600,
                height: 600
            };

            if (sender == vBtnEstados) {
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionEstado.aspx", "winSeleccion", "Selección de estado", windowProperties);
            } else if (sender == vBtnMunicipios) {
                var nbEstado = vLstEstados.get_selectedItem().get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionMunicipio.aspx?NbEstado=" + nbEstado, "winSeleccion", "Selección de municipio", windowProperties);
            } else if (sender == vBtnColonia) {
                var nbEstado = vLstEstados.get_selectedItem().get_value();
                var nbMunicipio = vLstMunicipios.get_selectedItem().get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionColonia.aspx?NbEstado=" + nbEstado + "&NbMunicipio=" + nbMunicipio, "winSeleccion", "Selección de colonia", windowProperties);
            } else if (sender == vBtnCandidato) {
                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog("../Comunes/SeleccionCandidato.aspx?vClTipoSeleccion=SIN_RELACION", "winSeleccion", "Selección de solicitud", windowProperties);
            } else if (sender == vBtnPlaza) {
                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog("../Comunes/SeleccionPlaza.aspx?TipoSeleccionCl=VACANTES", "winSeleccion", "Selección de la plaza a ocupar", windowProperties);        
                //} else if (sender == vBtnPlazaJefe) {
            //    var idPlaza = vLstPlaza.get_selectedItem().get_value();
            //    windowProperties.width = document.documentElement.clientWidth - 20;
            //    windowProperties.height = document.documentElement.clientHeight - 20;
            //    openChildDialog("../Comunes/SeleccionPlaza.aspx?CatalogoCl=PLAZAJEFE&TipoSeleccionCl=JEFE&PlazaId=" + idPlaza, "winSeleccion", "Selección del jefe inmediato", windowProperties);
            } else if (sender == vBtnCP) {
                windowProperties.width = document.documentElement.clientWidth - 100;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionCP.aspx?CatalogoCl=CODIGOPOSTAL", "winSeleccion", "Selección de código postal", windowProperties);
            } else if (sender == vBtnCentroOperativo) {
                windowProperties.width = document.documentElement.clientWidth - 100;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog("../Comunes/SeleccionCentroOptvo.aspx?CatalogoCl=CENTROOPERATIVO&mulSel=0", "winSeleccion", "Selección de centro operativo", windowProperties);
            } else if (sender == vBtnCentroAdministrativo) {
                windowProperties.width = document.documentElement.clientWidth - 100;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog("../Comunes/SeleccionCentroAdmvo.aspx?CatalogoCl=CENTROADMINISTRATIVO&mulSel=0", "winSeleccion", "Selección de centro administrativo", windowProperties);
            } else if (sender == vBtnEstadosNacimiento) {
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionEstado.aspx?CatalogoCl=ESTADONACIMIENTO", "winSeleccion", "Selección de estado", windowProperties);
            }
        }

        //function centrarVentana(sender, args) {
        //    sender.moveTo(1, 1);
        //}

        function OnCloseUpdate() {
            var vEmpleados = [];
            var vEmpleado = {
                clTipoCatalogo: "CLOSE"
            };
            vEmpleados.push(vEmpleado);
            sendDataToParent(vEmpleados);
        }

        function LoadValue(sender, args) {
            var items = sender.get_items();
            for (var i = 0; i < items.get_count() ; i++) {
                var item = items.getItem(i);
                var valor = item.get_value();
                var texto = item.get_text();
            }
            SetListBoxItem(sender, texto, valor);
        }

        function SetListBoxItem(list, text, value) {
            if (list != undefined & (text != undefined & text != "&nbsp; - &nbsp;")) {
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


        function useDataFromChild(pData) {
            if (pData != null) {
                var vSelectedData = pData[0];
                var list;
                var oData = {
                    nbDato: vSelectedData.nbDato,
                    clDato: vSelectedData.clDato,
                    nbEstado: vSelectedData.nbEstado,
                    nbMunicipio: vSelectedData.nbMunicipio,
                    nbColonia: vSelectedData.nbColonia,
                    nbCP: vSelectedData.nbCP
                };

                switch (vSelectedData.clTipoCatalogo) {
                    case "ESTADO":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_ESTADO") %>");
                        oData.clDato = vSelectedData.nbDato;
                        oData.nbDato = vSelectedData.nbDato;
                        break;
                    case "MUNICIPIO":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_MUNICIPIO") %>");
                        oData.clDato = vSelectedData.nbDato;
                        oData.nbDato = vSelectedData.nbDato;
                        break;
                    case "COLONIA":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_COLONIA") %>");
                        oData.clDato = vSelectedData.nbDato;
                        oData.nbDato = vSelectedData.nbDato;
                        break;
                    case "CANDIDATO":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "ID_CANDIDATO") %>");
                        oData.clDato = vSelectedData.idCandidato;
                        oData.nbDato = vSelectedData.clsolicitud;
                        break;
                    case "PLAZA":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "ID_PUESTO") %>");
                        oData.clDato = vSelectedData.idPlaza;
                        oData.nbDato = vSelectedData.nbPuesto + " (" + vSelectedData.clPlaza + ")";
                        //SetListBoxItem(list, oData.nbDato, oData.clDato);
                        //list = $find("<= ObtenerClientId(mpgPlantilla, "ID_PLAZA_JEFE") %>");
                        //oData.clDato = vSelectedData.idPlazaSuperior;
                        //oData.nbDato = vSelectedData.nbEmpleadoSuperior + " - " + vSelectedData.nbPuestoSuperior;
                        break;
                    //case "PLAZAJEFE":
                    //    list = $find("<= ObtenerClientId(mpgPlantilla, "ID_PLAZA_JEFE") %>");
                    //    oData.clDato = vSelectedData.idPlaza;
                    //    oData.nbDato = vSelectedData.nbEmpleado + " - " + vSelectedData.nbPuesto;
                    //    break;
                    case "CODIGOPOSTAL":
                        boxt = $find("<%= ObtenerClientId(mpgPlantilla, "CL_CODIGO_POSTAL") %>");
                        boxt.set_value(vSelectedData.nbCP);
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_ESTADO") %>");
                        oData.nbEstado = vSelectedData.nbEstado;
                        SetListBoxItem(list, oData.nbEstado);
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_MUNICIPIO") %>");
                        oData.nbMunicipio = vSelectedData.nbMunicipio;
                        SetListBoxItem(list, oData.nbMunicipio);
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_COLONIA") %>");
                        oData.nbDato = vSelectedData.nbColonia;
                        break;
                    case "CENTROOPERATIVO":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "CL_CENTRO_OPERATIVO") %>");
                        oData.clDato = vSelectedData.idCentro;
                        oData.nbDato = vSelectedData.clCentro;
                        break;
                    case "CENTROADMINISTRATIVO":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "CL_CENTRO_ADMINISTRATIVO") %>");
                        oData.clDato = vSelectedData.idCentro;
                        oData.nbDato = vSelectedData.clCentro;
                        break;
                    case "ESTADONACIMIENTO":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_ESTADO_NACIMIENTO") %>");
                         break;
                }

                SetListBoxItem(list, oData.nbDato, oData.clDato);
            }
        }
    </script>

    <style>
            .RadButton.rbSkinnedButton.uncheckedYes > .rbDecorated {
                color: #000 !important;
            }

            .RadButton.rbSkinnedButton.uncheckedYes {
                background-color: #eee !important;
            }


            /*
                .RadButton.rbSkinnedButton.checkedNo {
                    background-color: #eee !important;
                }

                .RadButton.rbSkinnedButton.checkedNo > .rbDecorated {
                    color: #333 !important;
                }
            */

            .RadButton.rbSkinnedButton.uncheckedNo {
                background-color: #eee !important;
            }

            .RadButton.rbSkinnedButton.uncheckedNo > .rbDecorated {
                color: #000 !important;
            }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpInventario" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramInventario" runat="server" DefaultLoadingPanelID="ralpInventario">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnActualizarFotoEmpleado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rauFotoEmpleado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbiFotoEmpleado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarFotoEmpleado" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarFotoEmpleado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rauFotoEmpleado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbiFotoEmpleado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarFotoEmpleado" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 50px); padding: 10px 0px 0px 0px;">
        <telerik:RadSplitter ID="rsSolicitud" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpSolicitud" runat="server">
                <telerik:RadTabStrip ID="tabSolicitud" runat="server" SelectedIndex="0" MultiPageID="mpgPlantilla">
                    <Tabs>
                        <telerik:RadTab Text="Personal"></telerik:RadTab>
                        <telerik:RadTab Text="Familiar"></telerik:RadTab>
                        <telerik:RadTab Text="Académica"></telerik:RadTab>
                        <telerik:RadTab Text="Laboral"></telerik:RadTab>
                        <telerik:RadTab Text="Intereses y competencias"></telerik:RadTab>
                        <telerik:RadTab Text="Adicional"></telerik:RadTab>
                        <telerik:RadTab Text="Documentación"></telerik:RadTab>
                        <telerik:RadTab Text="Reportes"></telerik:RadTab>
                        <telerik:RadTab Text="Nómina" Visible="false"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 45px);">
                    <telerik:RadMultiPage ID="mpgPlantilla" runat="server" SelectedIndex="0" Height="100%" ScrollBars="Auto">
                        <telerik:RadPageView ID="pvwPersonal" runat="server">
                            <div class="DivFotoCss">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadBinaryImage ID="rbiFotoEmpleado" runat="server" Width="128" Height="128"  ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadAsyncUpload ID="rauFotoEmpleado" runat="server" MaxFileInputsCount="1" HideFileInput="true" MultipleFileSelection="Disabled" AllowedFileExtensions=".jpeg,.jpg,.png" PostbackTriggers="btnActualizarFotoEmpleado" OnFileUploaded="rauFotoEmpleado_FileUploaded" Width="150" CssClass="CssBoton"> <Localization Select="Seleccionar" /></telerik:RadAsyncUpload>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadButton ID="btnActualizarFotoEmpleado" runat="server" Text="Adjuntar" Width="150"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadButton ID="btnEliminarFotoEmpleado" runat="server" Text="Eliminar fotografía" Width="150" OnClick="btnEliminarFotoEmpleado_Click"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <label class="labelTitulo">Información personal</label>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pvwFamiliar" runat="server"></telerik:RadPageView>
                        <telerik:RadPageView ID="pvwAcademica" runat="server"></telerik:RadPageView>
                        <telerik:RadPageView ID="pvwLaboral" runat="server"></telerik:RadPageView>
                        <telerik:RadPageView ID="pvwCompetencias" runat="server"></telerik:RadPageView>
                        <telerik:RadPageView ID="pvwAdicional" runat="server">
                            <label class="labelTitulo">Información adicional</label>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pvwDocumentos" runat="server">
                            <label class="labelTitulo">Documentación</label>
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
                            <div class="ctrlBasico">
                                Subir documento:<br />
                                <telerik:RadAsyncUpload ID="rauDocumento" runat="server" MultipleFileSelection="Disabled"><Localization Select="Seleccionar" /></telerik:RadAsyncUpload>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnAgregarDocumento" runat="server" Text="Agregar" OnClick="btnAddDocumento_Click"></telerik:RadButton>
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
                                            <telerik:GridHyperLinkColumn HeaderText="Nombre del documento" DataTextField="NB_DOCUMENTO" DataNavigateUrlFields="ID_ARCHIVO,ID_DOCUMENTO,FE_CREATED_DATE,NB_DOCUMENTO,ID_ITEM" DataNavigateUrlFormatString="/Comunes/ObtenerDocumento.ashx?ArchivoId={0}&ArchivoNb={2:yyyyMMdd}{4}&ArchivoDescargaNb={3}" Target="_blank"></telerik:GridHyperLinkColumn>
                                            <telerik:GridBoundColumn HeaderText="Tipo de documento" HeaderStyle-Width="200" DataField="CL_TIPO_DOCUMENTO" UniqueName="CL_TIPO_DOCUMENTO"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnDelDocumentos" runat="server" Text="Eliminar" OnClick="btnDelDocumentos_Click"></telerik:RadButton>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pvwReportes" runat="server" Visible="false">
                            <label class="labelTitulo">Reportes</label>

                            <div style="clear: both; height: 10px;"></div>

                            <label class="labelTitulo">Integración de personal</label>

                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label>Folio de solicitud</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox runat="server" ID="txtFolioSolicitud" Width="200px" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>

                             <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnSolicitud" Enabled="false" Width="120" Text="Ver solicitud" AutoPostBack="false" OnClientClicked="OpenSolicitud"></telerik:RadButton>
                            </div>

                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnProceso" Enabled="false" Width="220" Text="Ver proceso evaluación" AutoPostBack="false" OnClientClicked="OpenProceso"></telerik:RadButton>
                            </div>

                            <div style="clear: both;"></div>

                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label>Ultima bateria de pruebas:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox runat="server" ID="txtBateriaPruebas" Width="200px" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnResultados" Text="Ver resultados" Width="120" AutoPostBack="false" OnClientClicked="OpenResultadosPsicometrias"></telerik:RadButton>
                            </div>

                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnConsultaPersonales" Width="220" Text="Ver consultas personales" AutoPostBack="false" OnClientClicked="openConsultasPersonales"></telerik:RadButton>
                            </div>
                            <div style="clear: both; height: 10px;"></div>
                            <label class="labelTitulo">Formación y desarollo</label>
                            <label>Programas de capacitación</label>
                            <telerik:RadGrid ID="grdProgramas" runat="server" Height="300px" Width="800px" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="false"
                                AllowSorting="true" OnNeedDataSource="grdProgramas_NeedDataSource" OnDeleteCommand="grdProgramas_DeleteCommand" HeaderStyle-Font-Bold="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ID_PROGRAMA" EnableColumnsViewState="false" DataKeyNames="ID_PROGRAMA" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="150" FilterControlWidth="90" HeaderText="Clave" DataField="CL_PROGRAMA" UniqueName="CL_PROGRAMA"></telerik:GridBoundColumn>
                                        <telerik:GridHyperLinkColumn AutoPostBackOnFilter="true" HeaderText="Programa" DataTextField="NB_PROGRAMA" UniqueName="NB_PROGRAMA" DataNavigateUrlFields="ID_PROGRAMA" DataNavigateUrlFormatString="javascript:openAvanceProgramaCapacitacion({0})"></telerik:GridHyperLinkColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="Creado por" DataField="CL_USUARIO_APP_CREA" UniqueName="CL_USUARIO_APP_CREA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" DataFormatString="{0:d}" FilterControlWidth="25" HeaderText="Fecha de creación" DataField="FE_CREACION" UniqueName="FE_CREACION"></telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_PROGRAMA" ConfirmTextFormatString="¿Desea eliminar al empleado del programa de capacitación {0}?, este proceso no podrá revertirse." ConfirmDialogWidth="450" ConfirmDialogHeight="175" ConfirmDialogType="RadWindow" />
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <div style="clear: both; height: 20px;"></div>
                            <label>Eventos de capacitación</label>
                            <telerik:RadGrid ID="grdEventos" runat="server" Height="300px" Width="1150px" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="false"
                                AllowSorting="true" OnNeedDataSource="grdEventos_NeedDataSource" HeaderStyle-Font-Bold="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ID_EVENTO" ShowFooter="true" EnableColumnsViewState="false" DataKeyNames="ID_EVENTO" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="150" HeaderText="Clave" DataField="CL_EVENTO" UniqueName="CL_EVENTO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="200" HeaderText="Evento" DataField="NB_EVENTO" UniqueName="NB_EVENTO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="Clave curso" DataField="CL_CURSO" UniqueName="CL_CURSO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="200" FilterControlWidth="25" HeaderText="Curso" DataField="NB_CURSO" UniqueName="NB_CURSO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="Costo" DataField="MN_COSTO_DIRECTO" UniqueName="MN_COSTO_DIRECTO" Aggregate="Sum" FooterAggregateFormatString="Total: ${0:N2}" DataFormatString="{0:C2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="F. Inicial" DataField="FE_INICIO" UniqueName="FE_INICIO" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="F. Final" DataField="FE_TERMINO" UniqueName="FE_TERMINO" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="Cumplimiento" DataField="PR_CUMPLIMIENTO" UniqueName="PR_CUMPLIMIENTO" DataFormatString="{0}%">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <div style="clear: both; height: 10px;"></div>
                            <label class="labelTitulo">Evaluación organizacional</label>
                            <div style="clear: both; height: 10px;"></div>
                            <label>Evaluación del desempeño</label>
                            <telerik:RadGrid ID="grdDesempeno" runat="server" Height="300px" Width="700px" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="false"
                                AllowSorting="true" OnNeedDataSource="grdDesempeno_NeedDataSource" HeaderStyle-Font-Bold="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ID_PERIODO,ID_EVALUADO" EnableColumnsViewState="false" DataKeyNames="ID_PERIODO,ID_EVALUADO" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true"
                                    NoMasterRecordsText="Este empleado no ha participado en ningún período de desempeño">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="150" HeaderText="Clave" DataField="CL_PERIODO" UniqueName="CL_PERIODO"></telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="200" HeaderText="Evento" DataField="NB_PERIODO" UniqueName="NB_PERIODO"></telerik:GridBoundColumn>--%>
                                        <telerik:GridHyperLinkColumn AutoPostBackOnFilter="true" HeaderText="Período" DataTextField="NB_PERIODO" UniqueName="NB_PERIODO" DataNavigateUrlFields="ID_EVALUADO,ID_PERIODO" DataNavigateUrlFormatString="javascript:openReporteCumplimientoPersonalDesempeno({0},{1})"></telerik:GridHyperLinkColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="Cumplimiento" DataField="PR_CUMPLIMIENTO_EVALUADO" UniqueName="PR_CUMPLIMIENTO_EVALUADO" DataFormatString="{0}%">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <div style="clear: both; height: 10px;"></div>
                            <label>Clima laboral</label>
                            <div style="clear: both; height: 10px;"></div>
                            <telerik:RadButton runat="server" ID="btnCuestionarioClima" Text="Ver ultimo cuestionario" AutoPostBack="false" OnClientClicked="openCuestionarioClima"></telerik:RadButton>
                            <div style="clear: both; height: 10px;"></div>
                            <label>Rotación</label>
                            <telerik:RadGrid ID="grdRotacion" runat="server" Height="300px" Width="800px" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="false"
                                AllowSorting="true" OnNeedDataSource="grdRotacion_NeedDataSource" HeaderStyle-Font-Bold="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ID_BAJA_EMPLEADO" EnableColumnsViewState="false" DataKeyNames="ID_BAJA_EMPLEADO" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" NoMasterRecordsText="No existen bajas asociadas al empleado">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" HeaderText="Fecha" DataField="FE_BAJA_EFECTIVA" UniqueName="FE_BAJA_EFECTIVA" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" HeaderText="Causa" DataField="NB_MOTIVO" UniqueName="NB_MOTIVO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="300" FilterControlWidth="25" HeaderText="Comentario" DataField="DS_MOTIVO" UniqueName="DS_MOTIVO"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <label class="labelTitulo">Compensación</label>
                            <div style="clear: both; height: 10px;"></div>
                            <telerik:RadGrid ID="grdCompensacion" runat="server" Height="300px" Width="1000px" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="false"
                                AllowSorting="true" OnNeedDataSource="grdCompensacion_NeedDataSource" HeaderStyle-Font-Bold="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ID_BITACORA_SUELDO" EnableColumnsViewState="false" DataKeyNames="ID_BITACORA_SUELDO" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true"
                                    NoMasterRecordsText="Este empleado no esta asociado a un tablero de control">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" HeaderText="Fecha" DataField="FE_CAMBIO" UniqueName="FE_CAMBIO" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" HeaderText="Proceso" DataField="NB_PROCESO" UniqueName="NB_PROCESO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="200" FilterControlWidth="25" HeaderText="Descripción" DataField="DS_PROCESO" UniqueName="DS_PROCESO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="Anterior" DataField="NB_ANTERIOR" UniqueName="NB_ANTERIOR" DataFormatString="${0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="100" FilterControlWidth="25" HeaderText="Actual" DataField="NB_ACTUAL" UniqueName="NB_ACTUAL" DataFormatString="${0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pvwNomina" runat="server">
                            <iframe id="ifNomina" runat="server" frameborder="1"></iframe>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
                <telerik:RadSlidingZone ID="rszAvisoDePrivacidad" runat="server" SlideDirection="Left" Width="22px">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Instrucciones" Width="240px" RenderMode="Mobile" Height="200">
                        <div style="padding: 10px; text-align: justify;">
                            <p>Por favor ingresa los datos solicitados y al terminar haz clic en el botón de Guardar hasta el final de la página.</p>
                            <p>Recuerda que los campos marcados con asterisco (*) son obligatorios. www.sigein.com.mx</p>
                            <br />
                        </div>
                    </telerik:RadSlidingPane>
                    <telerik:RadSlidingPane ID="rspAyudaFoto" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="200">
                        <div style="padding: 10px; text-align: justify;">
                            <p>
                                 Para agregar la fotografía de perfil sigue los siguientes pasos:<br />
                                1.- Presiona el botón "Seleccionar" para seleccionar la fotografía desde tu ordenador.
                                <br />
                                2.- Al seleccionar la imágen, ésta se cargará. Para ponerla como foto de perfil presiona el botón "Adjuntar". Podrás eliminar la fotografía cargada, seleccionando "Remove".<br />
                                3.- A continuación podrás eliminarla con el botón "Eliminar fotografía" o seleccionar una nueva imagen mediante el botón "Select", repitiendo paso 1 y 2.<br />
                                4.- Al finalizar, no olvides guardar los cambios presionando los botones inferiores izquierdos "Guardar". 
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div style="clear: both; height: 5px;"></div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar"  OnClick="btnGuardar_Click"></telerik:RadButton>
        <telerik:RadButton ID="btnGuardarSalir" runat="server" Text="Guardar y cerrar"  OnClick="btnGuardarSalir_Click"></telerik:RadButton>
        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" OnClientClicked="OnClientBeforeClose" AutoPostBack="false"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
        <ConfirmTemplate>
            <div class="rwDialogPopup radconfirm">
                <div class="rwDialogText">
                    {1}
                </div>
                <div>
                    <a onclick="$find('{0}').close(true);" class="rwPopupButton" href="javascript:void(0);">
                        <span class="rwOuterSpan"><span class="rwInnerSpan">Sí</span></span></a>
                    <a onclick="$find('{0}').close(false);" class="rwPopupButton" href="javascript:void(0);">
                        <span class="rwOuterSpan"><span class="rwInnerSpan">No</span></span></a>
                </div>
            </div>
        </ConfirmTemplate>
    </telerik:RadWindowManager>
</asp:Content>
