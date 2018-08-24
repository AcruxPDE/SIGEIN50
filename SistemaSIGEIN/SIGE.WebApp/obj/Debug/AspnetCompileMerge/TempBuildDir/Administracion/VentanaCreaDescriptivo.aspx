<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCreaDescriptivo.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCreaDescriptivo" %>

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

        function OnCloseWindows() {
            GetRadWindow().close();
        }

        function AbrirDescriptivo(pIdPuesto) {
            var vURL = "VentanaDescriptivoPuesto.aspx?PuestoId=" + pIdPuesto;
            var vTitulo = "Agregar descripción del puesto";
            OpenSelectionWindow(vURL, "winDescriptivo", vTitulo);
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function DeshabilitarControl() {
            var btnMasDatos = $find("<%= btnMasDatos.ClientID %>");
            btnMasDatos.set_enabled(false);
        }

        function HabilitarControl() {
            var btnMasDatos = $find("<%= btnMasDatos.ClientID %>");
                    btnMasDatos.set_enabled(true);
                }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 20px)">
        <div style="height: 15px; clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblClave">*Clave:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClave" runat="server" Width="250" MaxLength="20"></telerik:RadTextBox>
            </div>
        </div>
        <div style="height: 15px; clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblNombre">*Nombre:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNombre" runat="server" Width="250" MaxLength="100"></telerik:RadTextBox>
            </div>
        </div>
        <div style="height: 15px; clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblDisponibleDO">Disponible para DO:</label>
            </div>
            <div class="divControlDerecha">
                <div class="checkContainer">
                    <telerik:RadButton ID="btnDOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpDO" AutoPostBack="false" OnClientClicked="HabilitarControl">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                    <telerik:RadButton ID="btnDOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpDO" AutoPostBack="false" OnClientClicked="DeshabilitarControl">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                </div>
            </div>
        </div>
        <div style="height: 15px; clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblDisponibleNomina">Disponible para Nómina:</label>
            </div>
            <div class="divControlDerecha">
                <div class="checkContainer">
                    <telerik:RadButton ID="btnNOTrue" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNO" AutoPostBack="false">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                    <telerik:RadButton ID="btnNOFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpNO" AutoPostBack="false">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                </div>
            </div>
        </div>
        <div style="height: 15px; clear: both;"></div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="btnMasDatos" runat="server" Text="Más datos" Width="100" AutoPostBack="true" OnClick="btnMasDatos_Click"></telerik:RadButton>
            <telerik:RadButton ID="btnAceptar" runat="server" Text="Guardar y cerrar"  AutoPostBack="true" OnClick="btnAceptar_Click"></telerik:RadButton>
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" Width="100" OnClientClicked="OnCloseWindows"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
