<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaInventarioPersonalAdmin.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaInventarioPersonalAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
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

            var windowProperties = {
                width: 600,
                height: 600
            };

            if (sender == vBtnEstados) {
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionEstado.aspx", "winSeleccion", "Selección de estado", windowProperties);
            } else if (sender == vBtnMunicipios) {
                var clEstado = vLstEstados.get_selectedItem().get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionMunicipio.aspx?ClEstado=" + clEstado, "winSeleccion", "Selección de municipio", windowProperties);
            } else if (sender == vBtnColonia) {
                var clEstado = vLstEstados.get_selectedItem().get_value();
                var clMunicipio = vLstMunicipios.get_selectedItem().get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionColonia.aspx?ClEstado=" + clEstado + "&ClMunicipio=" + clMunicipio, "winSeleccion", "Selección de colonia", windowProperties);
            } else if (sender == vBtnCandidato) {
                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog("../Comunes/SeleccionSolicitud.aspx", "winSeleccion", "Selección de solicitud", windowProperties);
            } else if (sender == vBtnPlaza) {
                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog("../Comunes/SeleccionPlaza.aspx?TipoSeleccionCl=VACANTES", "winSeleccion", "Selección de la plaza a ocupar", windowProperties);
            } else if (sender == vBtnPlazaJefe) {
                var idPlaza = vLstPlaza.get_selectedItem().get_value();
                windowProperties.width = document.documentElement.clientWidth - 20;
                windowProperties.height = document.documentElement.clientHeight - 20;
                openChildDialog("../Comunes/SeleccionPlaza.aspx?CatalogoCl=PLAZAJEFE&TipoSeleccionCl=JEFE&PlazaId=" + idPlaza, "winSeleccion", "Selección del jefe inmediato", windowProperties);
            }
        }


        function OpenObservacionRechazar() {
            var vURL = "VentanaRechazoDeModificacionInformacion.aspx?IdCambio=" + '<%= vIdCambioVS %>' + "&ClAutorizacion=Rechazada" + "&IdPuesto";
            var vTitulo = "modificación";
            var oWin = window.radopen(vURL, "rwRechazoC", document.documentElement.clientWidth - 90, document.documentElement.clientHeight - 250);
            oWin.set_title(vTitulo);
        }

        //function centrarVentana(sender, args) {
        //    sender.moveTo(1, 1);
        //}

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

        function useDataFromChild(pData) {
            if (pData != null) {
                var vSelectedData = pData[0];
                var list;
                var oData = {
                    nbDato: vSelectedData.nbDato,
                    clDato: vSelectedData.clDato
                };

                switch (vSelectedData.clTipoCatalogo) {
                    case "ESTADO":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_ESTADO") %>");
                        break;
                    case "MUNICIPIO":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_MUNICIPIO") %>");
                        break;
                    case "COLONIA":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "NB_COLONIA") %>");
                        break;
                    case "SOLICITUD":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "ID_CANDIDATO") %>");
                        oData.clDato = vSelectedData.idCandidato;
                        oData.nbDato = vSelectedData.clSolicitud;
                        break;
                    case "PLAZA":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "ID_PUESTO") %>");
                        oData.clDato = vSelectedData.idPlaza;
                        oData.nbDato = vSelectedData.clPlaza;
                        break;
                    case "PLAZAJEFE":
                        list = $find("<%= ObtenerClientId(mpgPlantilla, "ID_PLAZA_JEFE") %>");
                        oData.clDato = vSelectedData.idPlaza;
                        oData.nbDato = vSelectedData.clPlaza;
                        break;
                }

                SetListBoxItem(list, oData.nbDato, oData.clDato);
            }
        }
        function onCloseWindow(oWnd, args) {

            GetRadWindow().close();
        }
        function useDataFromChild() {

        }
    </script>
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
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 45px);">
                    <telerik:RadMultiPage ID="mpgPlantilla" runat="server" SelectedIndex="0" Height="100%" ScrollBars="Auto">
                        <telerik:RadPageView ID="pvwPersonal" runat="server">
                            <div style="background: #fafafa; position: absolute; right: 0px; margin-right: 50px; border: 1px solid lightgray; border-radius: 10px; padding: 5px;">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadBinaryImage ID="rbiFotoEmpleado" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadAsyncUpload ID="rauFotoEmpleado" runat="server" MaxFileInputsCount="1" HideFileInput="true" MultipleFileSelection="Disabled" AllowedFileExtensions=".jpeg,.jpg,.png" PostbackTriggers="btnActualizarFotoEmpleado" OnFileUploaded="rauFotoEmpleado_FileUploaded" Width="150"></telerik:RadAsyncUpload>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadButton ID="btnActualizarFotoEmpleado" runat="server" Text="Cambiar fotografía" Width="150"></telerik:RadButton>
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
                            <div id="lblRechazo" runat="server"  style="padding-left: 10px; height: 20px; Width: 80%; overflow: auto; background-color: skyblue">
                                <label runat="server" class="Notificacion"></label>
                            </div>
                            <div style="clear: both; height: 10px;"></div>
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
                                <telerik:RadAsyncUpload ID="rauDocumento" runat="server" MultipleFileSelection="Disabled"></telerik:RadAsyncUpload>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnAgregarDocumento" runat="server" Text="Agregar" OnClick="btnAddDocumento_Click"></telerik:RadButton>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadGrid ID="grdDocumentos" runat="server" OnNeedDataSource="grdDocumentos_NeedDataSource" Width="580">
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
                        <telerik:RadPageView ID="pvwReportes" runat="server">
                            <label class="labelTitulo">Reportes</label>
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
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <div style="padding-top: 10px;">

        <telerik:RadButton ID="btnGuardar" AutoPostBack="true" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        <telerik:RadButton AutoPostBack="true" ID="btnAutorizar" runat="server" Text="Autorizar" OnClick="btnAutorizar_Click" Width="100"></telerik:RadButton>
        <telerik:RadButton AutoPostBack="false" ID="btnRechazar" runat="server" Text="Rechazar" Width="100" OnClientClicked="OpenObservacionRechazar"></telerik:RadButton>

    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="rwRechazoC"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>

        </Windows>

    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensajeR" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
