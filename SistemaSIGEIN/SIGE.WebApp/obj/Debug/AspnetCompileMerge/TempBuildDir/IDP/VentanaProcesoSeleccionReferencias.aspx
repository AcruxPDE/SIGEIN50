<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaProcesoSeleccionReferencias.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaProcesoSeleccionReferencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
      <style>
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

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow() {
            GetRadWindow().close();
            //sendDataToParent();
        }

        function generateDataForParent() {
            var vCampos = [];
            var vCampo = {
                clTipoCatalogo: "REFERENCIA"
            };
            vCampos.push(vCampo);
            sendDataToParent(vCampos);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpEntrevista"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager runat="server" ID="ramEntrevista" DefaultLoadingPanelID="ralpEntrevista">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTipoEntrevista" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="lstEntrevistador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtCorreoEntrevistador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="clear: both; height: 5px;"></div>
    <fieldset>
        <legend style="font-weight:bold; font-size:medium;">Datos generales</legend>
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Empresa o institución: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtEmpresa" style="width: 300px;"></span>
                    </td>
                    <td class="ctrlTableDataContext">
                        <label>Giro de la empresa: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtGiroEmpresa" style="width: 300px;"></span>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Período de: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtInicio" style="width: 300px;"></span>
                    </td>
                    <td class="ctrlTableDataContext">
                        <label>Período a: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtFin" style="width: 300px;"></span>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Puesto desempeñado: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtPuestoDesempenado" style="width: 300px;"></span>
                    </td>
                    <td class="ctrlTableDataContext">
                        <label>Funciones desempeñadas: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtFuncionesDesempenadas" style="width: 300px;"></span>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Nombre del jefe inmediato: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtNombreJefe" style="width: 300px;"></span>
                    </td>
                    <td class="ctrlTableDataContext">
                        <label>Puesto del jefe inmediato: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtPuestoJefe" style="width: 300px;"></span>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Teléfono del jefe inmediato: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtTelefonoJefe" style="width: 300px;"></span>
                    </td>
                    <td class="ctrlTableDataContext">
                        <label>Correo del jefe inmediato: </label>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <span runat="server" id="txtCorreoJefe" style="width: 300px;"></span>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <div style="clear: both;"></div>
    <fieldset>
        <legend style="font-weight:bold; font-size:medium;">Datos de referencia</legend>
    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Nombre de la referencia: </label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtNombreReferencia" Width="250px" runat="server"></telerik:RadTextBox>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Puesto de la referencia: </label>
                </td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtPuestoReferencia" Width="250px"></telerik:RadTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierdaPS">
            <label>El candidato es recomendable: </label>
        </div>
        <div class="divControlDerecha">
            <div class="checkContainer">
                <telerik:RadButton ID="chkInformacionConfirmadaSi" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpReferencias" AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                    </ToggleStates>
                </telerik:RadButton>
                <telerik:RadButton ID="chkInformacionConfirmadaNo" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpReferencias" AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                    </ToggleStates>
                </telerik:RadButton>
            </div>
            <%--<telerik:RadButton ID="chkInformacionConfirmada" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                <ToggleStates>
                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>--%>
        </div>
    </div>
        <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierdaPS">
            <label>Comentarios adicionales: </label>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlDerecha">
            <telerik:RadEditor Height="100px" Width="100%" ToolsWidth="400" EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
        </div>
    </div>
        </fieldset>
    <div style="clear: both; height:5px;"></div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnGuardar" UseSubmitBehavior="false" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" OnClientClicked="generateDataForParent"></telerik:RadButton>
        </div>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
