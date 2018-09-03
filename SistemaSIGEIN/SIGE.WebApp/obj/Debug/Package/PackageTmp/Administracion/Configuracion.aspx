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

        function OpenGruposWindows() {
            var vURl = "VentanaGrupos.aspx";
            var vTitulo = "Agregar grupo";
            var vWindowsPropierties = {
                width: document.documentElement.clientWidth - 650,
                height: document.documentElement.clientHeight - 20
            };

            openChildDialog(vURl, "winGrupos", vTitulo, vWindowsPropierties);
        }

        //function OpenEditGruposWindows() {
        //    var masterTable = $find("<= rgGrupos.ClientID %>").get_masterTableView();
        //    var vSelectedItems = masterTable.get_selectedItems()[0];
        //    if (vSelectedItems != undefined) {
        //        var vIdGrupo = vSelectedItems.getDataKeyValue("ID_GRUPO");
        //        var vURl = "VentanaGrupos.aspx?pIdGrupo=" + vIdGrupo;
        //        var vTitulo = "Editar grupo";
        //        var vWindowsPropierties = {
        //            width: document.documentElement.clientWidth - 650,
        //            height: document.documentElement.clientHeight - 20
        //        };

        //        openChildDialog(vURl, "winGrupos", vTitulo, vWindowsPropierties);
        //    }
        //    else
        //        radalert("Selecciona un grupo.", 400, 150);
        //}

        //function OpenConfirmEliminar(sender, args) {
        //    var masterTable = $find("<= rgGrupos.ClientID %>").get_masterTableView();
        //    var selectedItem = masterTable.get_selectedItems()[0];
        //    if (selectedItem != undefined) {
        //        var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_GRUPO").innerHTML;
        //        confirmAction(sender, args, "¿Deseas eliminar el grupo " + vNombre + "?, este proceso no podrá revertirse");
        //    }
        //    else {
        //        radalert("Selecciona un grupo.", 400, 150);
        //    }
        //}

        function OpenWindowEnviar() {
            var masterTable = $find("<%= grdPlantillas.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vIdPlantilla = selectedItem.getDataKeyValue("ID_PLANTILLA_SOLICITUD");
                var vClTipoPlantilla = selectedItem.getDataKeyValue("CL_FORMULARIO");
                var vURL = "VentanaEnvioSolicitud.aspx?plantillaId=" + vIdPlantilla + "&PlantillaTipoCl=" + vClTipoPlantilla;
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

        function OpenDescriptivoWindows() {
            var vURL = "VentanaCreaDescriptivo.aspx";
            var vTitulo = "Descriptivo de puesto";

            openChildDialog(vURL, "winDescriptivo", vTitulo);
        }

        function OpenInventarioWindows() {
            var vURL = "VentanaConfigurarInventario.aspx";
            var vTitulo = "Inventario de personal";

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: 500,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "winInventario", vTitulo, windowProperties);
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


        //function useDataFromChild(pDato) {
        //    $find('<= rgGrupos.ClientID %>').get_masterTableView().rebind();;
        //}


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
            <telerik:AjaxSetting AjaxControlID="btnAgregarGrupo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
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
            <telerik:RadTab Text="Integración con nómina"></telerik:RadTab>
            <%-- <telerik:RadTab Text="Grupos"></telerik:RadTab>--%>
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
                                    <div style="height:10px; clear:both;"></div>
                                    <telerik:RadButton ID="btnActualizarLogoOrganizacion" runat="server" Text="Cambiar logotipo"  ToolTip="Haz clic en este botón para cambiar la imagen que hayas seleccionado"></telerik:RadButton>
                                   <div style="height:10px; clear:both;"></div>
                                    <telerik:RadButton ID="btnEliminarLogoOrganizacion" runat="server" Text="Eliminar logotipo"  OnClick="btnEliminarLogoOrganizacion_Click" ToolTip="Elimina el logotipo que esta establecido actualmente"></telerik:RadButton>
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
                        <telerik:RadButton ID="btnActualizar" runat="server" Text="Actualizar" AutoPostBack="true" OnClick="btnActualizar_Click"></telerik:RadButton>
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
                                <telerik:RadComboBox ID="cmbCatalogoGenero" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA" Width="250"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Causas de baja</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoCausaVacante" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA" Width="250"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Estados civiles</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoEstadoCivil" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA" Width="250"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Tipos de teléfono</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoTipoTelefono" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA" Width="250"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Parentescos</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoParentesco" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA" Width="250"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Ocupaciones</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoOcupacion" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA" Width="250"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoGenero">Redes sociales</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox ID="cmbCatalogoRedSocial" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA" Width="250"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label name="lblCatalogoCausaRequisicion">Causa de requisiciones</label>
                            </div>
       
                                <div class="divControlDerecha">
                                    <telerik:RadComboBox ID="cmbCatalogoCausasRequisicion" runat="server" DataTextField="NB_CATALOGO_LISTA" DataValueField="ID_CATALOGO_LISTA" Width="250"></telerik:RadComboBox>
                                </div>
                 
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
                        <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Width="22px" ClickToOpen="true">
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
                   <telerik:RadSplitter ID="RadSplitter1" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="RadPane1" runat="server">
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
                        </telerik:RadPane>
                       </telerik:RadSplitter>
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
                <div style="height: calc(100% - 10px);">
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
                <div style="height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEditarPlantilla" runat="server" Text="Editar" AutoPostBack="false" OnClientClicked="ShowEditForm"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarPlantilla" runat="server" Text="Eliminar" AutoPostBack="true" OnClientClicking="confirmarEliminar" OnClick="btnEliminarPlantilla_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnCopiarPlantilla" runat="server" Text="Copiar" AutoPostBack="false" OnClientClicked="ShowInsertForm"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEstablecerGeneral" runat="server" Text="Establecer por defecto" AutoPostBack="true" OnClientClicking="confirmarEstablecerGeneral" OnClick="btnEstablecerGeneral_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEnviarSolicitud" runat="server" Text="Enviar solicitud" AutoPostBack="false" OnClientClicking="openEnviarEmail"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvCamposExtras" runat="server">
                <div style="height: calc(100% - 10px);">
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
                <div style="height: 10px;"></div>
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
                    <telerik:RadButton ID="btnCopiarCampo" runat="server" Text="Copiar" AutoPostBack="false" OnClientClicked="ShowCopyFieldForm"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvIntegracionNomina" runat="server">
                <div style="height: calc(100% - 70px);">
                    <div class="ctrlBasico">
                        <table class="ctrlTableForm" style="width: 400px;">
                            <tr>
                                <td></td>
                                <td style="align-content: center;" colspan="2">
                                    <label name="lblEditable">Editable desde</label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;"></td>
                                <td style="text-align: left; width: 82px;">
                                    <label name="lblRFC">Nómina</label>
                                </td>
                                <td style="text-align: left; width: 82px;">
                                    <label name="lblRFC">DO</label>
                                </td>
                            </tr>
                        </table>
                        <div style="height: calc(100% - 150px); width: 400px; overflow: auto; float: left;">
                            <table class="ctrlTableForm" style="width: 100%;">
                                <tr style="padding-bottom:10px;">
                                    <td style="text-align: left; width: 200px; padding-bottom:10px;">
                                        <label name="lblRFC">RFC</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px; ">
                                            <telerik:RadButton ID="btnRFCNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpRFCNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnRFCNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpRFCNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnRFCDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpRFCDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnRFCDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpRFCDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">CURP</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCURPNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCURPNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCURPNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCURPNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCURPDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCURPDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCURPDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCURPDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">No. De seguro social</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNSSNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNSSNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNSSNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNSSNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNSSDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNSSDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNSSDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNSSDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Lugar de nacimiento</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNANOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNANO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNANOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNANO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNADOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNADO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNADOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNADO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblEstadoNacimiento">Estado de nacimiento</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnEstadoNaNoTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpESTADONANO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnEstadoNaNoFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpESTADONANO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnEstadoNaDoTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpESTADONADO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnEstadoNaDoFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpESTADONADO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Nacionalidad</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNacionalidadNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNacionalidadNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNacionalidadNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNacionalidadNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNacionalidadDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNacionalidadDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNacionalidadDOFlase" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNacionalidadDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Fecha de nacimiento</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnFeNacimientoNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpFeNacimientoNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnFeNacimientoNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpFeNacimientoNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnFeNacimientoDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpFeNacimientoDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnFeNacimientoDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpFeNacimientoDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <table class="ctrlTableForm" style="width: 400px;">
                            <tr>
                                <td></td>
                                <td style="align-content: center;" colspan="2">
                                    <label name="lblEditable">Editable desde</label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;"></td>
                                <td style="text-align: left; width: 82px;">
                                    <label name="lblRFC">Nómina</label>
                                </td>
                                <td style="text-align: left; width: 82px;">
                                    <label name="lblRFC">DO</label>
                                </td>
                            </tr>
                        </table>
                        <div style="height: calc(100% - 150px); width: 400px; float: left; padding-left: 30px;">
                            <table class="ctrlTableForm" style="width: 100%;">
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Género</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnGeneroNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpGeneroNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnGeneroNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpGeneroNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnGeneroDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpGeneroDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnGeneroDoFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpGeneroDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Estado civil</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCivilNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCivilNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCivilNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCivilNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCivilDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCivilDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCivilDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCivilDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">C.P.</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCPNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCPNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCPNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCPNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCPDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCPDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCPDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCPDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Estado</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnEstadoNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpEstadoNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnEstadoNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpEstadoNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnEstadoDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpEstadoDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnEstadoDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpEstadoDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Municipio</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnMunicipioNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpMunicipioNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnMunicipioNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpMunicipioNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnMunicipioDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpMunicipioDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnMunicipioDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpMunicipioDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Colonia</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnColoniaNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpColoniaNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnColoniaNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpColoniaNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnColoniaDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpColoniaDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnColoniaDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpColoniaDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Calle</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCalleNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCalleNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCalleNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCalleNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCalleDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCalleDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCalleDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCalleDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <table class="ctrlTableForm" style="width: 400px;">
                            <tr>
                                <td></td>
                                <td style="align-content: center;" colspan="2">
                                    <label name="lblEditable">Editable desde</label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;"></td>
                                <td style="text-align: left; width: 82px;">
                                    <label name="lblRFC">Nómina</label>
                                </td>
                                <td style="text-align: left; width: 82px;">
                                    <label name="lblRFC">DO</label>
                                </td>
                            </tr>
                        </table>
                        <div style="height: calc(100% - 150px); width: 400px; overflow: auto; float: left; padding-left: 30px;">
                            <table class="ctrlTableForm" style="width: 100%;">
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">No. Exterior</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNoExtNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNoExtNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNoExtNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNoExtNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNoExtDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNoExtDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNoExtDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNoExtDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">No. Interior</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNoInteriorNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNoInteriorNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNoInteriorNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNoInteriorNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnNoInteriorDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNoInteriorDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnNoInteriorDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNoInteriorDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Teléfonos</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnTelNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpTelNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnTelNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpTelNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnTelDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpTelDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnTelDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpTelDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Correo electrónico</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnEmailNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpEmailNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnEmailNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpEmailNO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnEmailDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpEmailDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnEmailDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpEmailDO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Centro operativo</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCONOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCONO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCONOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCONO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCODOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCODO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCODOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCODO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 200px;">
                                        <label name="lblRFC">Centro administrativo</label>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCANOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCANO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCANOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCANO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                    <td style="width: 100px;">
                                        <div class="checkContainer" style="width: 82px;">
                                            <telerik:RadButton ID="btnCADOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCADO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCADOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCADO" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    </div>
             <div style="height: 10px; clear: both;"></div>
                    <%--<div class="ctrlBasico">
        <telerik:RadButton ID="RadButton1" runat="server" Text="Guardar" Width="100" ></telerik:RadButton>--%>
                    <%-- <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" Width="100" OnClientClicked="onCloseWindows"></telerik:RadButton>--%>
                    <%-- </div>--%>
            </telerik:RadPageView>
            <%-- <telerik:RadPageView ID="rpvGrupos" runat="server">
                <div style="clear: both;"></div>
                <div style="height: calc(100% - 60px); width: 100%;">
                    <telerik:RadGrid
                        ID="rgGrupos"
                        runat="server"
                        Width="900"
                        Height="100%"
                        AllowPaging="true"
                        AutoGenerateColumns="false"
                        HeaderStyle-Font-Bold="true"
                        EnableHeaderContextMenu="true"
                        AllowMultiRowSelection="false"
                        OnNeedDataSource="rgGrupos_NeedDataSource"
                        OnItemDataBound="rgGrupos_ItemDataBound">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView DataKeyNames="ID_GRUPO" ClientDataKeyNames="ID_GRUPO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="CL_GRUPO" DataField="CL_GRUPO" HeaderText="Clave" AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="120" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_GRUPO" DataField="NB_GRUPO" HeaderText="Grupo" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CL_USUARIO_MODIFICA" DataField="CL_USUARIO_MODIFICA" HeaderText="Último usuario que modifica" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="50" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FE_MODIFICACION" DataField="FE_MODIFICACION" HeaderText="Última fecha de modificación" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="50" CurrentFilterFunction="EqualTo" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="height: 10px; clear: both;"></div>
                <telerik:RadButton ID="btnAgregarGrupo" runat="server" Text="Agregar" Width="100" AutoPostBack="false" OnClientClicked="OpenGruposWindows"></telerik:RadButton>
                <telerik:RadButton ID="btnEditar" runat="server" Text="Editar" Width="100" AutoPostBack="false" OnClientClicked="OpenEditGruposWindows"></telerik:RadButton>
                <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" AutoPostBack="true" OnClientClicking="OpenConfirmEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
                <telerik:RadButton ID="btnAgregarDesc" runat="server" Text="Descriptivo" Width="100" AutoPostBack="false" OnClientClicked="OpenDescriptivoWindows"></telerik:RadButton>
                <telerik:RadButton ID="btnConfigInventario" runat="server" Text="Inventario" Width="100" AutoPostBack="false" OnClientClicked="OpenInventarioWindows"></telerik:RadButton>
            </telerik:RadPageView>--%>
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
            <telerik:RadWindow ID="winGrupos" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winDescriptivo" runat="server" Behaviors="Close" Modal="true" Width="480" Height="400" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winInventario" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
