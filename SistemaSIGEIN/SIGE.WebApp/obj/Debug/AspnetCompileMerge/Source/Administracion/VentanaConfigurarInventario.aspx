<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaConfigurarInventario.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaConfigurarInventario" %>

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

        function onCloseWindows() {
            GetRadWindow().close();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <table class="ctrlTableForm" style="width: 100%;">
        <tr>
            <td></td>
            <td style="text-align: center;" colspan="2">
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
    <div style="height: calc(100% - 120px); overflow: auto;">
        <table class="ctrlTableForm" style="width: 100%;">
            <tr>
                <td style="text-align: left; width: 200px;">
                    <label name="lblRFC">RFC</label>
                </td>
                <td style="width: 100px;">
                    <div class="checkContainer" style="width: 82px;">
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
    <div style="height: 15px; clear: both;"></div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Width="100" OnClick="btnGuardar_Click"></telerik:RadButton>
        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" Width="100" OnClientClicked="onCloseWindows"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
