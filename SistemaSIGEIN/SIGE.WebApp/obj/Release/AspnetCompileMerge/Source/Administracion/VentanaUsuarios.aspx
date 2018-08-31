<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaUsuarios.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }

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

        function OpenEmployeeSelectionWindow() {
            openChildDialog("../Comunes/SeleccionEmpleado.aspx?mulSel=0", "winSeleccionEmpleados", "Selección de empleados")
        }

        function useDataFromChild(pEmpleados) {
         
            if (pEmpleados != null) {
                var vEmpleadoSeleccionado = pEmpleados[0];
                console.info(vEmpleadoSeleccionado);
                var list = $find("<%=lstEmpleado.ClientID %>");
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(vEmpleadoSeleccionado.nbEmpleado);
                item.set_value(vEmpleadoSeleccionado.idEmpleado);
                items.add(item);

                list.commitChanges();

                $find("<%= txtNbUsuario.ClientID %>").set_value(vEmpleadoSeleccionado.nbEmpleado);
                $find("<%= txtNbCorreoElectronico.ClientID %>").set_value(vEmpleadoSeleccionado.nbCorreoElectronico);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <br />
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblClUsuario">Usuario:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClUsuario" name="txtClUsuario" runat="server"></telerik:RadTextBox><br />
            <asp:RequiredFieldValidator ID="reqtxtClUsuario" runat="server" Display="Dynamic" ControlToValidate="txtClUsuario" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblRol">Empleado:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadListBox ID="lstEmpleado" Width="290" runat="server" OnClientItemDoubleClicking="OpenEmployeeSelectionWindow"></telerik:RadListBox>
            <telerik:RadButton ID="btnBuscarEmpleado" runat="server" Text="B" OnClientClicked="OpenEmployeeSelectionWindow" AutoPostBack="false"></telerik:RadButton>
        </div>
    </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblNbUsuario">Nombre:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNbUsuario" name="txtNbUsuario" runat="server" Width="300"></telerik:RadTextBox><br />
            <asp:RequiredFieldValidator ID="reqTxtNbUsuario" runat="server" Display="Dynamic" ControlToValidate="txtNbUsuario" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblNbUsuario">Correo electrónico:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNbCorreoElectronico" name="txtNbCorreoElectronico" runat="server" Width="300"></telerik:RadTextBox><br />
            <asp:RequiredFieldValidator ID="reqTxtNbCorreoElectronico" runat="server" Display="Dynamic" ControlToValidate="txtNbCorreoElectronico" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="ctrlBasico" id="ctrlPasswordChange" runat="server">
        <div class="divControlIzquierda">
            <label name="lblPasswordChange">Cambiar password:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="chkPasswordChange" runat="server" ToggleType="CheckBox" name="chkActivo" Checked="true" AutoPostBack="false" OnClientCheckedChanged="enableCtrlPassword">
                <ToggleStates>
                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>
    <div id="ctrlPassword" runat="server">
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblPassword">Contraseña:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNbPassword" runat="server" TextMode="Password" onkeyup="checkPasswordMatch();"></telerik:RadTextBox>
            </div>
        </div>
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
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblRol">Rol:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadComboBox ID="cmbRol" runat="server"></telerik:RadComboBox>
        </div>
    </div>
     <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblTipoMultiempresa">Empresa:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadComboBox ID="cmbTipoMultiempresa" runat="server" EmptyMessage="Seleccione..." >
            </telerik:RadComboBox>
        </div>
    </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label name="lblNombre">Activo:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" name="chkActivo" Checked="true" AutoPostBack="false">
                <ToggleStates>
                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
