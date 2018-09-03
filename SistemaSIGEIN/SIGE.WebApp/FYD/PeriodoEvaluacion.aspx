<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="PeriodoEvaluacion.aspx.cs" Inherits="SIGE.WebApp.FYD.PeriodoEvaluacion" %>

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
        //function closeWindow() {
        //    GetRadWindow().close();
        //}

        function closeWindow() {
            var pDatos = [{
                accion: "ACTUALIZARLISTA"

            }];
            cerrarVentana(pDatos);
        }

        function closeWindowEdit() {
            var pDatos = [{
                accion: "ACTUALIZAR"

            }];
            cerrarVentana(pDatos);
        }

        function cerrarVentana(recargarList) {
            sendDataToParent(recargarList);
        }

        //function closeWindow() {
        //    var pDatos = [{
        //        accion: "ACTUALIZARLISTA"

        //    }];
        //    cerrarVentana(pDatos);
        //}

        //function cerrarVentana(recargarList) {
        //    sendDataToParent(recargarList);
        //}

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="padding-top: 10px; height: calc(100% - 50px);">

        <telerik:RadSplitter ID="rsEditarPeriodo" runat="server" Width="100%" Height="100%" BorderSize="0">

            <telerik:RadPane ID="rpDatos" runat="server">

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblNbPeriodo">Periodo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNbPeriodo" name="txtNbPeriodo" runat="server" MaxLength="100" Width="300"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator ID="reqtxtNbPeriodo" runat="server" Display="Dynamic" ControlToValidate="txtNbPeriodo" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblNbPeriodo">Descripción:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtDsPeriodo" name="txtNbPeriodo" runat="server" MaxLength="200" Width="500"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator ID="reqtxtDsPeriodo" runat="server" Display="Dynamic" ControlToValidate="txtDsPeriodo" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblClEstado">Estado:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtClEstado" name="txtNbPeriodo" runat="server" Enabled="false" Width="100"></telerik:RadTextBox><br />
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblDsNotas">Notas:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadEditor Height="100" Width="500" ToolsWidth="500" EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml">
                        </telerik:RadEditor>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div style="margin-left: 140px;">
                    <fieldset style="width: 300px">
                        <legend>
                            <label>Indica para qué niveles crear cuestionarios</label></legend>
                        <div style="clear: both;"></div>
                        <table class="ctrlTableForm">
                            <tr>
                                <td style="text-align: right;">
                                    <label name="lblFgAutoevaluacion">Autoevaluación:</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnAutoevaluacionTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAutoevaluacion" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnAutoevaluacionFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAutoevaluacion" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <label name="lblFgSupervisor">Supervisor (jefe inmediato):</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnSupervisorTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSuperior" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnSupervisorFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSuperior" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <label name="lblFgSubordinados">Subordinados:</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnSubordinadosTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSubordinados" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnSubordinadosFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSubordinados" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <label name="lblFgInterrelacionados">Interrelacionados:</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnInterrelacionadosTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpInterrelacionados" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnInterrelacionadosFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpInterrelacionados" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <label name="lblFgOtros">Otros:</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnOtrosTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpOtros" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnOtrosFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpOtros" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOtroEvaluador" runat="server" Scrolling="None" Width="22px">
                <telerik:RadSlidingZone ID="rszOtroEvaluador" runat="server" SlideDirection="Left" Width="22px">
                    <telerik:RadSlidingPane ID="rspOtroEvaluadorManual" runat="server" Title="Ayuda" Width="300px">
                        <div style="padding-top: 10px;">
                            Esta opción te permitirá seleccionar a los evaluadores del periodo que has creado. Los evaluadores que selecciones evaluarán al grupo completo de evaluados que has elegido.
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Aceptar" AutoPostBack="true" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
