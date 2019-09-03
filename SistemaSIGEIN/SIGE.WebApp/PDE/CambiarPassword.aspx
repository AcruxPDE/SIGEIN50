<%@ Page Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="CambiarPassword.aspx.cs" Inherits="SIGE.WebApp.PDE.CambiarPassword" %>


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


        function OpenEmployeeSelectionWindow() {
            openChildDialog("../Comunes/SeleccionEmpleado.aspx", "winSeleccionEmpleados", "Selección de empleados")
        }

        
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server" SelectedIndex="0">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Visible="false" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="ralpNomina" runat="server"></telerik:RadAjaxLoadingPanel>
      <telerik:RadAjaxManager ID="ramNomina" runat="server" DefaultLoadingPanelID="ralpNomina">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lstEmpleado" />
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>          
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadPageLayout runat="server" ID="rplIDP" GridType="Fluid" ShowGrid="true" HtmlTag="None">
        <telerik:LayoutRow RowType="Generic">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div style="float: left; padding-left: 20px;">
                            <h3>PUNTO DE ENCUENTRO</h3>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-xs-12 col-lg-6 col-md-6 col-sm-6" style="margin-top: 100px;">
                            <div style="padding-left: 20px;">
                                <h3><b style="color: #C6DB95;">Bienvenid@ al Punto de Encuentro</b> </h3>
                                <b><%= vNbUsuario %></b>
                                <br />
                                <br />
                                    <br />
                                Para poder utilizar el sistema es indispensable que cambies la contraseña. Esta pantalla no volverá aparecer una vez cambiada la contraseña.
                                <div id="dvLogo" runat="server" style="padding-left: 20px; padding-top: 20px;">
                                    <telerik:RadBinaryImage ID="rbiLogoOrganizacion1" runat="server" Width="108" Height="108" ResizeMode="Fit" />
                                </div>
                            </div>
                        </div>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:LayoutRow>
    </telerik:RadPageLayout>
     
   <%-- <div style="height: calc(100% - 30px);">--%>
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
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>
    </div>
   
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
