<%@ Page Title="" Language="C#" MasterPageFile="~/TC/ContextTC.Master" AutoEventWireup="true" CodeBehind="VentanaNuevoTableroControl.aspx.cs" Inherits="SIGE.WebApp.TC.VentanaNuevoTableroControl" %>

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
                color: black !important;
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

        function closeWindow() {
            GetRadWindow().close();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="padding-top: 10px; height: calc(100% - 50px);">
        <telerik:RadSplitter ID="rsEditarPeriodo" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpDatos" runat="server">
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblNbPeriodo">Consulta:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNbPeriodo" name="txtNbPeriodo" runat="server" MaxLength="100" Width="300"></telerik:RadTextBox><br />
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblNbPeriodo">Descripción:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtDsPeriodo" name="txtNbPeriodo" runat="server" MaxLength="200" Width="500"></telerik:RadTextBox><br />
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
                            <label>Criterios de selección:</label></legend>
                        <div style="clear: both;"></div>
                        <table class="ctrlTableForm">
                            <tr>
                                <td style="text-align: right;">
                                    <label name="lblFgAutoevaluacion">Resultado de pruebas</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnResultadoPruebasTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAutoevaluacion" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnResultadoPruebasFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpAutoevaluacion" AutoPostBack="false">
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
                                    <label name="lblFgSupervisor">Evaluación de competencias</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnEvaluacionCompetenciasTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSuperior" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnEvaluacionCompetenciasFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSuperior" AutoPostBack="false">
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
                                    <label name="lblFgSubordinados">Evaluación de desempeño</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnDesempenoTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSubordinados" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnDesempenoFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSubordinados" AutoPostBack="false">
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
                                    <label name="lblFgInterrelacionados">Resultados clima laboral</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnClimaTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpInterrelacionados" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnClimaFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpInterrelacionados" AutoPostBack="false">
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
                                    <label name="lblFgOtros">Situación salarial</label></td>
                                <td>
                                    <div class="checkContainer">
                                        <telerik:RadButton ID="btnSalarialTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpOtros" AutoPostBack="false">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnSalarialFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpOtros" AutoPostBack="false">
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
                            Esta opción te permitirá agregar consultas al tablero de control especificando los criterios de evaluación a aplicar.
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
