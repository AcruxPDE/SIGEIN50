<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SIGE.WebApp.Administracion.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .divControlIzquierda {
            width: 200px;
        }

        div.RadUpload .ruBrowse {
            background-position: 0 -23px;
            width: 200px;
        }

        div.RadUpload_Default .ruFileWrap .ruButtonHover {
            background-position: 100% -23px !important;
        }
    </style>
    <script type="text/javascript">
        function checkPasswordMatch() {
            var vPassword1 = $find("<%=txtNbPassword.ClientID %>").get_textBoxValue();
            var vPassword2 = $find("<%=txtNbPasswordConfirm.ClientID %>").get_textBoxValue();

            if (vPassword2 == "") {
                $get("PasswordRepeatedIndicator").innerHTML = "";
                $get("PasswordRepeatedIndicator").className = "Base L0";
            }
            else if (vPassword1 == vPassword2) {
                $get("PasswordRepeatedIndicator").innerHTML = "Coincide";
                $get("PasswordRepeatedIndicator").className = "Base L5";
            }
            else {
                $get("PasswordRepeatedIndicator").innerHTML = "No coincide";
                $get("PasswordRepeatedIndicator").className = "Base L1";
            }
        }

        function enableCtrlPassword(sender, args) {
            $get("<%=ctrlPassword.ClientID %>").style.display = sender.get_checked() ? "block" : "none";
        }

        function ShowInsertForm() {
            OpenWindow("copy");
            return false;
        }

        function ShowEditForm() {
            OpenWindow("edit");
            return false;
        }

        function ShowInsertFieldForm() {
            OpenWindowCampo("add");
            return false;
        }

        function ShowCopyFieldForm() {
            OpenWindowCampo("copy");
            return false;
        }

        function openEnviarEmail() {
            OpenWindowEnviar();
            return false;
        }

        function ShowEditFieldForm() {
            OpenWindowCampo("edit");
            return false;
        }

        function OpenWindowEnviar() {
            var masterTable = $find("<%= grdPlantillas.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vIdPlantilla = selectedItem.getDataKeyValue("ID_PLANTILLA_SOLICITUD");
                var vClTipoPlantilla = selectedItem.getDataKeyValue("CL_FORMULARIO");
                var vURL = "VentanaEnvioSolicitud.aspx?plantillaId=" + vIdPlantilla + "&PlantillaTipoCl=" + vClTipoPlantilla;;
                var vTitulo = "Enviar solicitud";
                var windowProperties = {
                    width: document.documentElement.clientWidth - 100,
                    height: document.documentElement.clientHeight - 20
                };
                openChildDialog(vURL, "winPlantillas", vTitulo, windowProperties);
            }
            else
                radalert("Selecciona un plantilla.", 400, 150);
        }

        function OpenWindowCampo(pClAccion) {
            var vURL = "VentanaCampoFormulario.aspx?AccionCl=" + pClAccion;
            var vTitulo = "Agregar Campo";

            var windowProperties = {
                width: 500,
                height: 530
            };

            if (pClAccion == "add") {
                openChildDialog(vURL, "winCamposFormulario", vTitulo, windowProperties);
            } else {
                var masterTable = $find("<%= grdCamposFormulario.ClientID %>").get_masterTableView();
                var selectedItem = masterTable.get_selectedItems()[0];

                if (selectedItem != undefined) {
                    var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_CAMPO_FORMULARIO").innerHTML;
                    var vIdCampo = selectedItem.getDataKeyValue("ID_CAMPO_FORMULARIO");
                    var vClTipoPlantilla = selectedItem.getDataKeyValue("CL_FORMULARIO");
                    if (vIdCampo != null) {
                        vURL = vURL + "&CampoId=" + vIdCampo + "&PlantillaTipoCl=" + vClTipoPlantilla;
                        var vFgAbrirVentana = true;
                        var vDsMensajeAdvertencia = "";
                        switch (pClAccion) {
                            case "edit":
                                vTitulo = "Editar campo";
                                break;
                            case "copy":
                                vFgAbrirVentana = (selectedItem.getDataKeyValue("FG_CLONABLE").toLowerCase() == "true");
                                vTitulo = "Copiar campo '" + vNombre + "'";
                                vDsMensajeAdvertencia = "El tipo del campo seleccionado no permite que sea copiado.";
                                break;
                        }
                        if (vFgAbrirVentana)
                            openChildDialog(vURL, "winCamposFormulario", vTitulo, windowProperties);
                        else
                            radalert(vDsMensajeAdvertencia, 400, 150);
                    }
                }
                else
                    radalert("Selecciona un campo.", 400, 150);
            }
        }

        function OpenWindow(pClAccion) {
            var masterTable = $find("<%= grdPlantillas.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_PLANTILLA_SOLICITUD").innerHTML;
                var vIdPlantilla = selectedItem.getDataKeyValue("ID_PLANTILLA_SOLICITUD");
                var vClTipoPlantilla = selectedItem.getDataKeyValue("CL_FORMULARIO");
                var vURL = "VentanaPlantillaFormulario.aspx";
                var vTitulo = "Agregar Plantilla";
                if (vIdPlantilla != null) {
                    vURL = vURL + "?PlantillaId=" + vIdPlantilla + "&AccionCl=" + pClAccion + "&PlantillaTipoCl=" + vClTipoPlantilla;
                    switch (pClAccion) {
                        case "edit":
                            vTitulo = "Editar plantilla";
                            break;
                        case "copy":
                            vTitulo = "Copiar plantilla '" + vNombre + "'";
                            break;
                    }
                }
                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };
                openChildDialog(vURL, "winPlantillas", vTitulo, windowProperties);
            }
            else
                radalert("Selecciona un plantilla.", 400, 150);
        }

        function onCloseWindow(oWnd, args) {
            $find("<%= grdPlantillas.ClientID %>").get_masterTableView().rebind();
        }

        function onCloseFieldWindow(oWnd, args) {
            $find("<%= grdCamposFormulario.ClientID %>").get_masterTableView().rebind();
        }

        function confirmarEliminarCampo(sender, args) {
            var masterTable = $find("<%= grdCamposFormulario.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            var vFgAbrirVentana = false;
            var vDsMensajeAdvertencia = "";
            if (selectedItem != undefined) {
                vFgAbrirVentana = (selectedItem.getDataKeyValue("FG_SISTEMA").toLowerCase() == "false");

                if (vFgAbrirVentana) {
                    var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_CAMPO_FORMULARIO").innerHTML;
                    confirmAction(sender, args, "¿Deseas eliminar el campo " + vNombre + "?, este proceso no podrá revertirse");
                } else
                    vDsMensajeAdvertencia = "Los campos de sistema no pueden se eliminados.";
            }
            else
                vDsMensajeAdvertencia = "Selecciona un campo.";

            if (!vFgAbrirVentana) {
                radalert(vDsMensajeAdvertencia, 400, 150);
                args.set_cancel(true);
            }

        }

        function confirmarAccion(sender, args, clAccion) {
            var masterTable = $find("<%= grdPlantillas.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_PLANTILLA_SOLICITUD").innerHTML;
                switch (clAccion) {
                    case "eliminar":
                        confirmAction(sender, args, "¿Deseas eliminar la plantilla " + vNombre + "?, este proceso no podrá revertirse");
                        break;
                    case "general":
                        confirmAction(sender, args, "¿Deseas establecer la plantilla " + vNombre + " por defecto?");
                        break;
                }
            }
            else {
                radalert("Selecciona una plantilla.", 400, 150);
                args.set_cancel(true);
            }
        }

        function confirmarEliminar(sender, args) {
            confirmarAccion(sender, args, "eliminar");
        }

        function confirmarEstablecerGeneral(sender, args) {
            confirmarAccion(sender, args, "general");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="rlpConfiguracion" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPlantillas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPlantillas" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCamposFormulario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCamposFormulario" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarCampo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCamposFormulario" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo" name="lbllocalizacion" id="lbllocalizacion">Configuración</label>
    <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
        <Tabs>
            <telerik:RadTab Text="Información de la organización"></telerik:RadTab>
            <telerik:RadTab Text="Catálogos"></telerik:RadTab>
            <telerik:RadTab Text="Correo electrónico"></telerik:RadTab>
            <telerik:RadTab Text="Control de documentos"></telerik:RadTab>
            <telerik:RadTab Text="Plantillas"></telerik:RadTab>
            <telerik:RadTab Text="Campos"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 130px); padding-top: 10px;">
        <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvAvisoPrivacidad" runat="server">
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblNbOrganizacion">Nombre de la organización</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNbOrganizacion" runat="server" Width="500"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblLogoOrganizacion">Agregar imagen del logotipo de la organización</label>
                    </div>
                    <div class="divControlDerecha">
                        <table>
                            <tr>
                                <td>

                                    <telerik:RadAsyncUpload ID="rauLogoOrganizacion" runat="server" MaxFileInputsCount="1" HideFileInput="true" MultipleFileSelection="Disabled" AllowedFileExtensions=".jpeg,.jpg,.png"
                                        PostbackTriggers="btnActualizarLogoOrganizacion" OnFileUploaded="rauLogoOrganizacion_FileUploaded" Width="300" ToolTip="Haz clic en este botón para seleccionar el logotipo de tu empresa">
                                        <Localization Select="Seleccionar imagen" />
                                    </telerik:RadAsyncUpload>

                                    <telerik:RadButton ID="btnActualizarLogoOrganizacion" runat="server" Text="Cambiar logotipo" Width="140" ToolTip="Haz clic en este botón para cambiar la imagen que hayas seleccionado"></telerik:RadButton>
                                    <br />
                                    <br />
                                    <telerik:RadButton ID="btnEliminarLogoOrganizacion" runat="server" Text="Eliminar logotipo" Width="140" OnClick="btnEliminarLogoOrganizacion_Click" ToolTip="Elimina el logotipo que esta establecido actualmente"></telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadBinaryImage ID="rbiLogoOrganizacion" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="clear: both;"></div>
                  <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                        <label name="lblLogoOrganizacion">Licencia</label>
                    </div>
                       <div class="divControlDerecha">
                    <telerik:RadButton ID="btnActualizar" runat="server" Text="Actualizar" AutoPostBack="true" OnClick="btnActualizar_Click" ></telerik:RadButton>
                   </div>
                            </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvCatalogos" runat="server">
                <telerik:RadSplitter ID="rsConfiguracion" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpConfiguracion" runat="server">

                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Géneros</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoGenero" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Causas de baja</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoCausaVacante" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Estados civiles</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoEstadoCivil" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Tipos de teléfono</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoTipoTelefono" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Parentescos</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoParentesco" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Ocupaciones</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoOcupacion" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Redes sociales</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoRedSocial" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoCausaRequisicion">Causa de requisiciones</label>
                            </div>
                            <div>
                                <div class="divControlDerecha">
                                    <telerik:RadComboBox ID="cmbCatalogoCausasRequisicion" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA"></telerik:RadComboBox>
                                </div>
                            </div>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
                        <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Width="22px">
                            <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Instrucciones" Width="320px" RenderMode="Mobile" Height="200">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>
                                        En esta sección podrás configurar el catálogo correspondiente a las diferentes características relacionadas a un empleado.<br />
                                        <br />
                                        Por ejemplo:
                                        <br />
                                        Para la característica "Géneros", selecciona el catálogo "Género" de la lista desplegable.
                                        <br />
                                        <br />
                                        Al momento de utilizar éste catálogo por ejemplo en el inventario de personal, aparecerán como opciones de "Género":<br />
                                        "Masculino"<br />
                                        "Femenimo"<br />
                                        <br />
                                        En cambio, si seleccionas para la característica "Géneros" el catálogo "Estado Civil" al momento de utilizarlo, aparecerán
                                como opciones:<br />
                                        "Casado"<br />
                                        "Divorciado"<br />
                                        "Soltero"<br />
                                    </p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvCorreoElectronico" runat="server">
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblCatalogoGenero">Servidor</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtMailServer" runat="server" Width="300"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblCatalogoGenero">Puerto</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox ID="txtMailServerPort" runat="server" Width="55" MinValue="1" MaxValue="65535" NumberFormat-DecimalDigits="0" IncrementSettings-Step="1" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblCatalogoGenero">Usar SSL</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadButton ID="chkUseSSL" runat="server" ToggleType="CheckBox" name="chkUseSSL" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblCatalogoGenero">Autenticar</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadButton ID="chkAutenticar" runat="server" ToggleType="CheckBox" name="chkAutenticar" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblCatalogoGenero">Nombre remitente</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtSenderName" runat="server" Width="300"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblCatalogoGenero">Dirección de correo electrónico del remitente</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtSenderMail" runat="server" Width="300"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblCatalogoGenero">Usuario</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtUser" runat="server" Width="300"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico" id="ctrlPasswordChange" runat="server">
                    <div class="divControlIzquierda">
                        <label name="lblPasswordChange">Cambiar password:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadButton ID="chkPasswordChange" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" OnClientCheckedChanged="enableCtrlPassword">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div id="ctrlPassword" runat="server" style="display: none;">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label name="lblPassword">Contraseña:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtNbPassword" runat="server" TextMode="Password" onkeyup="checkPasswordMatch();"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label name="lblPasswordConfirm">Confirmación:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtNbPasswordConfirm" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false" onkeyup="checkPasswordMatch();"></telerik:RadTextBox>
                            <span id="PasswordRepeatedIndicator" class="Base L0">&nbsp;</span>
                        </div>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvControlDocumentos" runat="server">
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblCatalogoGenero">Control de documentos habilitado</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadButton ID="chkControlDocumentos" runat="server" ToggleType="CheckBox" name="chkControlDocumentos" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvPlantillas" runat="server">
                <div style="height: calc(100% - 9px);">
                    <telerik:RadGrid ID="grdPlantillas" runat="server" OnItemDataBound="grdPlantillas_ItemDataBound" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdPlantillas_NeedDataSource" Height="100%" Width="100%" AllowSorting="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_PLANTILLA_SOLICITUD,CL_FORMULARIO" DataKeyNames="ID_PLANTILLA_SOLICITUD,CL_FORMULARIO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Tipo de plantilla" DataField="CL_FORMULARIO" UniqueName="CL_FORMULARIO">
                                    <%--HASTA QUE TENGAMOS TIEMPO DE IMPLEMENTAR ESTE TIPO DE FILTRO--%>
                                    <FilterTemplate>
                                        <telerik:RadComboBox ID="APComboPlantilla" Width="130" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("CL_FORMULARIO").CurrentFilterValue %>'
                                            runat="server" OnClientSelectedIndexChanged="APComboPlantillaIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="TODOS" Value="" />
                                                <telerik:RadComboBoxItem Text="SOLICITUD" Value="SOLICITUD" />
                                                <telerik:RadComboBoxItem Text="INVENTARIO" Value="INVENTARIO" />
                                                <telerik:RadComboBoxItem Text="DESCRIPTIVO" Value="DESCRIPTIVO" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                            <script type="text/javascript">
                                                function APComboPlantillaIndexChanged(sender, args) {
                                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                    tableView.filter("CL_FORMULARIO", args.get_item().get_value(), "EqualTo");
                                                }
                                            </script>
                                        </telerik:RadScriptBlock>
                                    </FilterTemplate>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="240" FilterControlWidth="130" HeaderText="Nombre" DataField="NB_PLANTILLA_SOLICITUD" UniqueName="NB_PLANTILLA_SOLICITUD"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="150" HeaderStyle-Width="210" HeaderText="Descripción" DataField="DS_PLANTILLA_SOLICITUD" UniqueName="DS_PLANTILLA_SOLICITUD"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="20" HeaderText="Por defecto" DataField="FG_GENERAL_CL" UniqueName="FG_GENERAL_CL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Exposición" DataField="CL_EXPOSICION" UniqueName="CL_EXPOSICION"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="70" DataType="System.DateTime"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="height: 9px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEditarPlantilla" runat="server" Text="Editar" AutoPostBack="false" OnClientClicked="ShowEditForm"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarPlantilla" runat="server" Text="Eliminar" AutoPostBack="true" OnClientClicking="confirmarEliminar" OnClick="btnEliminarPlantilla_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnCopiarPlantilla" runat="server" Text="Copiar de..." AutoPostBack="false" OnClientClicked="ShowInsertForm"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEstablecerGeneral" runat="server" Text="Establecer por defecto" AutoPostBack="true" OnClientClicking="confirmarEstablecerGeneral" OnClick="btnEstablecerGeneral_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEnviarSolicitud" runat="server" Text="Enviar solicitud" AutoPostBack="false" OnClientClicking="openEnviarEmail"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvCamposExtras" runat="server">
                <div style="height: calc(100% - 9px);">
                    <telerik:RadGrid ID="grdCamposFormulario" runat="server" OnItemDataBound="grdCamposFormulario_ItemDataBound" OnNeedDataSource="grdCamposAdicionales_NeedDataSource" Height="100%" HeaderStyle-Font-Bold="true" AllowSorting="true" PageSize="50">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_CAMPO_FORMULARIO,CL_FORMULARIO,FG_CLONABLE,FG_SISTEMA" DataKeyNames="ID_CAMPO_FORMULARIO,CL_FORMULARIO,FG_CLONABLE,FG_SISTEMA" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Módulo" DataField="CL_FORMULARIO" UniqueName="CL_FORMULARIO">
                                    <%--HASTA QUE TENGAMOS TIEMPO DE IMPLEMENTAR ESTE TIPO DE FILTRO--%>
                                    <FilterTemplate>
                                        <telerik:RadComboBox ID="APCombo" Width="130" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("CL_FORMULARIO").CurrentFilterValue %>'
                                            runat="server" OnClientSelectedIndexChanged="APComboIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="TODOS" Value="" />
                                                <telerik:RadComboBoxItem Text="Solicitud" Value="SOLICITUD" />
                                                <telerik:RadComboBoxItem Text="Inventario" Value="INVENTARIO" />
                                                <telerik:RadComboBoxItem Text="Descriptivo" Value="DESCRIPTIVO" />
<%--                                                <telerik:RadComboBoxItem Text="Cuestionario" Value="CUESTIONARIO" />--%>
                                                <telerik:RadComboBoxItem Text="Instructor" Value="INSTRUCTOR" />
                                                <telerik:RadComboBoxItem Text="Curso" Value="CURSO" />
                                                <telerik:RadComboBoxItem Text="Evento" Value="EVENTO" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                            <script type="text/javascript">
                                                function APComboIndexChanged(sender, args) {
                                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                    tableView.filter("CL_FORMULARIO", args.get_item().get_value(), "EqualTo");
                                                }
                                            </script>
                                        </telerik:RadScriptBlock>
                                    </FilterTemplate>
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave" DataField="CL_CAMPO_FORMULARIO" UniqueName="CL_CAMPO_FORMULARIO"></telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre" DataField="NB_CAMPO_FORMULARIO" UniqueName="NB_CAMPO_FORMULARIO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="150" HeaderText="Tooltip" DataField="NB_TOOLTIP" UniqueName="NB_TOOLTIP"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="80" HeaderText="Tipo campo" DataField="NB_TIPO_CAMPO" UniqueName="NB_TIPO_CAMPO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="20" HeaderText="Activo" DataField="FG_ACTIVO_CL" UniqueName="FG_ACTIVO_CL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="20" HeaderText="Sistema" DataField="FG_SISTEMA_CL" UniqueName="FG_SISTEMA_CL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="20" HeaderText="Copiable" DataField="FG_CLONABLE_CL" UniqueName="FG_CLONABLE_CL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="70" DataType="System.DateTime"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="height: 9px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarCampo" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="ShowInsertFieldForm"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEditarCampo" runat="server" Text="Editar" AutoPostBack="false" OnClientClicked="ShowEditFieldForm"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarCampo" runat="server" Text="Eliminar" AutoPostBack="true" OnClientClicking="confirmarEliminarCampo" OnClick="btnEliminarCampo_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnCopiarCampo" runat="server" Text="Copiar de..." AutoPostBack="false" OnClientClicked="ShowCopyFieldForm"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winCamposFormulario" runat="server" Title="Campo Formulario" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseFieldWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winPlantillas" runat="server" Title="Solicitud" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <%--<telerik:RadWindow Height="500px" Width="900px" ReloadOnShow="true" ShowContentDuringLoad="false" OnClientClose="onCloseWindow"></telerik:RadWindow>--%>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
