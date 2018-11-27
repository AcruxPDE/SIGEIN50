<%@ Page Title="" Language="C#" MasterPageFile="~/AppSIGE.Master" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="SIGE.WebApp.Logon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .windowclass {
            z-index: 5000 !important;
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
                $get("PasswordRepeatedIndicator").innerHTML = "Coinciden";
                $get("PasswordRepeatedIndicator").className = "Base L5";
            }
            else {
                $get("PasswordRepeatedIndicator").innerHTML = "No coinciden";
                $get("PasswordRepeatedIndicator").className = "Base L1";
            }
        }

        function OpenWindow(pIdSolicitud) {
            var myUrl = '<%= ResolveClientUrl("IDP/Solicitud/Solicitud.aspx") %>';
            var vURL = myUrl;
            var vTitulo = "Agregar Solicitud";
            if (pIdSolicitud != null) {
                vURL = vURL + "?SolicitudId=" + pIdSolicitud;
                vTitulo = "Editar Solicitud";
            }
            var windowProperties = {};
            windowProperties.width = document.documentElement.clientWidth - 20;
            windowProperties.height = document.documentElement.clientHeight - 20;
            openChildDialog(vURL, "winSolicitud", vTitulo, windowProperties);
        }


        function OpenWindowPlantilla(pIdPlantilla) {
            var myUrl = '<%= ResolveClientUrl("IDP/Solicitud/Solicitud.aspx") %>';
            var vURL = myUrl;
            var vTitulo = "Agregar Solicitud";
            if (pIdPlantilla != null) {
                vURL = vURL + "?PlantillaId=" + pIdPlantilla;
            }
            var windowProperties = {};
            windowProperties.width = document.documentElement.clientWidth - 20;
            windowProperties.height = document.documentElement.clientHeight - 20;
            openChildDialog(vURL, "winSolicitud", vTitulo, windowProperties);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <div class="row">
            <div class="col-md-12 col-xs-12" style="position: absolute; top: 0px; left: 0%;">
                <center>
                    <img src="Assets/images/TituloBannerSigein.png" class="visible-md visible-lg visible-sm" alt="Banner" />
                    <img src="Assets/images/Logo.png" alt="Logo" class="visible-xs" />
                </center>
            </div>
        </div>

        <div class="row" id="ContentLogin" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="tituloLogin" data-modulo="login" class="TextoDom" data-html="*">Inicia sesión para continuar</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="LogoLogin" style="text-align: center; padding: 10px;">
                                        <%--<img class="profile-img" src="Assets/images/LoginUsuario.png" />--%>
                                        <telerik:RadBinaryImage ID="rbiLogoOrganizacion1" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-10  col-md-offset-1 ">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-user"></i>
                                            </span>
                                            <input runat="server" class="form-control TextoDom" data-modulo="login" id="txtUsuario" placeholder="Usuario" name="txtUsuario" type="text" autofocus enableviewstate="false">
                                        </div>
                                        <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px">
                                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="valusuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-key"></i>
                                            </span>
                                            <input runat="server" class="form-control TextoDom" data-modulo="login" placeholder="Contraseña" name="txtPassword" id="txtPassword" type="password" enableviewstate="false">
                                        </div>
                                        <span class="col-md-12 col-sm-12 col-xs-12" style="padding: 0px">
                                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="Valpassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>

                                    <div class="form-group">
                                        <div class="input-group">
                                            <telerik:RadButton ID="btnRecuperarPassword" Width="220" runat="server" Text="Recuperar contraseña" CausesValidation="false" ButtonType="LinkButton" OnClick="btnRecuperarPassword_Click"></telerik:RadButton>
                                            <%--<input type="button" data-modulo="login" name="recuperapassword" id="recuperapassword" value="Recuperar contraseña" class="btn btn-link TextoDom" onclick="RecuperaPassword(); return false" />--%>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Button Text="Ingresar" ID="btnLogin" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                   <div id="dvSolicitudes" runat="server"  class="panel-footer" style="text-align: center; ">
                        <%--<i class="fa fa-file-text"></i>--%>
                      <%-- <a href="#" onclick="OpenWindow(null);" style="margin: 20px;">Solicitud de empleo</a>--%>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="ContentPasswordRecovery" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong1" data-modulo="login" class="TextoDom" data-html="*">Recupera tu contraseña</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div2" style="text-align: center;">
                                        <%--<img class="profile-img" src="Assets/images/LoginUsuario.png" />--%>
                                        <telerik:RadBinaryImage ID="rbiLogoOrganizacion2" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <telerik:RadTabStrip ID="rtsRecuperarPassword" runat="server" SelectedIndex="0" CausesValidation="false">
                                    <Tabs>
                                        <telerik:RadTab Text="Cuenta"></telerik:RadTab>
                                        <telerik:RadTab Text="Correo electrónico"></telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <br />
                                <telerik:RadTextBox ID="txtRecuperarCuenta" runat="server" Width="100%" EnableViewState="false"></telerik:RadTextBox>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px">
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="reqtxtRecuperarCuenta" runat="server" ControlToValidate="txtRecuperarCuenta" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </span>
                                <br />
                                <telerik:RadButton ID="btnEnviarPorCuenta" runat="server" Text="Recuperar contraseña" Width="100%" OnClick="btnEnviarPorCuenta_Click"></telerik:RadButton>
                                <br />
                                <br />
                                <telerik:RadButton ID="btnIntroducirCodigo" runat="server" Text="Introducir código" Width="100%" OnClick="btnIntroducirCodigo_Click" CausesValidation="false"></telerik:RadButton>
                            </div>
                        </fieldset>
                    </div>
                    <div id="dvRecuperaPass" runat="server" class="panel-footer" style="text-align: center;">
                       <%-- <i class="fa fa-file-text"></i><!-- aqui-->
                        <a href="#" onclick="OpenWindow(null);" style="margin: 20px;">Solicitud de empleo</a>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="ContentCodigoConfirmacion" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong2" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div3" style="text-align: center;">
                                        <%--<img class="profile-img" src="Assets/images/LoginUsuario.png" />--%>
                                        <telerik:RadBinaryImage ID="rbiLogoOrganizacion3" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 100px; padding-right: 10px;">Código:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtCodigo" runat="server" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px">
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="reqtxtCodigo" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtNbPassword" runat="server" TextMode="Password" onkeyup="checkPasswordMatch();"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span id="PasswordRepeatedIndicator" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Confirmación:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtNbPasswordConfirm" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false" onkeyup="checkPasswordMatch();"></telerik:RadTextBox>
                                </div>
                                <br />
                                <br />
                                <telerik:RadButton ID="btnConfirmarCodigo" runat="server" Text="Cambiar contraseña" Width="100%" OnClick="btnConfirmarCodigo_Click"></telerik:RadButton>
                            </div>
                        </fieldset>
                    </div>
                    <div id="dvConfirmaCodigo" runat="server" class="panel-footer" style="text-align: center;">
                        <%--<i class="fa fa-file-text"></i> <!-- aqui-->
                        <a href="#" onclick="OpenWindow(null);" style="margin: 20px;">Solicitud de empleo</a>--%>
                    </div>
                </div>
            </div>
        </div>
        <%--Inicio Autorizar programa de capacitación--%>
        <div class="row" id="ContentAutorizaDocumentos" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong3" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div4" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Período:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtProgramaCapacitacion" runat="server" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px">
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Autorizador:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtAutorizador" runat="server"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span id="Span1" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtClave" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClave" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <telerik:RadButton ID="btnSiguiente" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>
                            </div>
                        </fieldset>
                    </div>
<%--                    <div class="panel-footer" style="text-align: center;">
                        <i class="fa fa-file-text"></i> <!-- aqui-->
                        <a href="#" onclick="OpenWindow(null);" style="margin: 20px;">Solicitud de empleo</a>
                    </div>--%>
                </div>
            </div>
        </div>

        <%--Fin Autorizar programa de capacitación--%>

        <%-- Inicio evaluacion de resultados de eventos --%>

        <div class="row" id="ContentEvaluacionResultados" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong4" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div5" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="rbiEvaluacion" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Evento:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtEvento" runat="server" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Evaluador:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtEvaluador" runat="server"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span id="Span2" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtContraseñaEvaluacion" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContraseñaEvaluacion" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <%--<telerik:RadButton ID="btnAbrirEvaluacion" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>--%>
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnAbrirEvaluacion" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAbrirEvaluacion_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <%--<div class="panel-footer" style="text-align: center;">
                        <i class="fa fa-file-text"></i>
                        <a href="#" onclick="OpenWindow(null);" style="margin: 20px;">Solicitud de empleo</a>
                    </div>--%>
                </div>
            </div>
        </div>

        <%-- Fin evaluación de resultados de eventos --%>

        <%-- Inicio cuestionarios de eventos --%>

        <div class="row" id="ContentCuestionarios" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong5" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div6" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage2" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Período:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtPeriodoCapacitacion" runat="server" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Evaluador:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtEvaluadorCuestionario" runat="server"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span id="Span3" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtContraseñaCuestionario" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContraseñaCuestionario" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <%--<telerik:RadButton ID="btnAbrirEvaluacion" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>--%>
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnAbrirCuestionario" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAbrirCuestionario_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <%--<div class="panel-footer" style="text-align: center;">
                        <i class="fa fa-file-text"></i>
                        <a href="#" onclick="OpenWindow(null);" style="margin: 20px;">Solicitud de empleo</a>
                    </div>--%>
                </div>
            </div>

        </div>

        <%-- Fin evaluación de resultados de eventos --%>

        <%-- Inicio de cuestionarios de clima laboral  --%>

        <div class="row" id="ContentClimaLaboral" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong6" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div7" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage3" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Período:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtPeriodoClima" runat="server" EnableViewState="false" Enabled="false"></telerik:RadTextBox>
                                </div>
                                <br />
<%--                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Evaluador:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtEvaluadorClima" runat="server"></telerik:RadTextBox>
                                </div>
                                <br />--%>
                                <span id="Span4" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtContraseniaClima" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtContraseniaClima" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <%--<telerik:RadButton ID="btnAbrirEvaluacion" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>--%>
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnAbrirCuestionarioClima" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAbrirCuestionarioClima_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>

        </div>
        <%--  Fin clima laboral--%>

        <%-- Inicio de cuestionarios de evaluación desempeño  --%>

        <div class="row" id="ContentEvaluacionDesempeno" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong11" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div12" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage8" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Período:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtPeriodoDesempeno" runat="server" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Evaluador:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtEvaluadorDesempeno" runat="server"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span id="Span8" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtContrasenaDesempeno" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtContrasenaDesempeno" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <%--<telerik:RadButton ID="btnAbrirEvaluacion" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>--%>
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnAbrirCuestionarioDesempeno" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAbrirCuestionarioDesempeno_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>

        </div>
        <%--  Fin evaluación desempeño--%>
         <%-- Inicio de cuestionario independiente --%>

        <div class="row" id="ContentCuestionario" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong11" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div12" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage10" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Período:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtPeriodoCuestionarioInd" runat="server" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Evaluador:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtEvaluadorCuestionarioInd" runat="server"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span id="Span8" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtPassCuestionarioInd" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtContrasenaDesempeno" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <%--<telerik:RadButton ID="btnAbrirEvaluacion" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>--%>
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnAbrirCuestionarioIndependiente" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAbrirCuestionarioIndependiente_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>

        </div>
        <%--  Fin cuestionario independiente --%>
        <div class="row" id="ContentCartera" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong7" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div8" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage4" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtContrasenaCartera" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContrasenaCartera" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <%--<telerik:RadButton ID="btnAbrirEvaluacion" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>--%>
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnCartera" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnCartera_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="ContentEntrevista" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong8" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div9" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage5" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Entrevistador:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtEntrevistador" runat="server" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <span id="Span5" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtContraseñaEntrevista" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtContraseñaCuestionario" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <%--<telerik:RadButton ID="btnAbrirEvaluacion" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>--%>
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnAbrirEntrevista" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAbrirEntrevista_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <%--<div class="panel-footer" style="text-align: center;">
                        <i class="fa fa-file-text"></i>
                        <a href="#" onclick="OpenWindow(null);" style="margin: 20px;">Solicitud de empleo</a>
                    </div>--%>
                </div>
            </div>

        </div>
        <!--Requisiciones-->
        <div class="row" id="ContentRequisiciones" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong9" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div10" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage6" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Requisición:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtNotificacion" runat="server" ReadOnly="true" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <label style="width: 110px; padding-right: 10px;">Puesto:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtPuesto" runat="server" ReadOnly="true" EnableViewState="false"></telerik:RadTextBox>
                                </div>

                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <span id="Span6" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtContraseñaNotificacion" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtContraseñaNotificacion" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <%--<telerik:RadButton ID="btnAbrirEvaluacion" runat="server" Text="Siguiente" Width="100%" OnClick="btnSiguiente_Click"></telerik:RadButton>--%>
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnAbrirRequisicion" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAbrirRequisicion_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <%--<div class="panel-footer" style="text-align: center;">
                        <i class="fa fa-file-text"></i>
                        <a href="#" onclick="OpenWindow(null);" style="margin: 20px;">Solicitud de empleo</a>
                    </div>--%>
                </div>
            </div>

        </div>

        <!--AutorizarRequisición -->
        <div class="row" id="ContentAutorizaRequisicion" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong10" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div11" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage7" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Requisición:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="rtbAutRequisicion" runat="server" ReadOnly="true" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <label style="width: 110px; padding-right: 10px;">Puesto:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="rtbPuesto" runat="server" ReadOnly="true" EnableViewState="false"></telerik:RadTextBox>
                                </div>

                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <span id="Span7" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="rtbAutoContrasena" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator9" runat="server" ControlToValidate="rtbAutoContrasena" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="IngresarAutorizar" runat="server" CssClass="btn btn-primary btn-block" OnClick="IngresarAutorizar_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>

        </div>
        <!-- Fin de Autorizar Requisición -->

        <!--Autorizar requisicion y puesto -->
        <div class="row" id="ContentAutorizaPuestoRequisicion" runat="server">
            <div class="CentrarLogin">
                <div class="panel panel-default panelNoBottomMargin">
                    <div class="panel-heading" style="text-align: center;">
                        <strong id="Strong12" data-modulo="login" class="TextoDom" data-html="*">Introduce tu código</strong>
                    </div>
                    <div class="panel-body">
                        <fieldset class="fieldSetNoBorder">
                            <div class="row">
                                <div class="center-block">
                                    <div id="Div13" style="text-align: center;">
                                        <telerik:RadBinaryImage ID="RadBinaryImage9" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px;">
                                <label style="width: 110px; padding-right: 10px;">Requisición:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtAPRequisicion" runat="server" ReadOnly="true" EnableViewState="false"></telerik:RadTextBox>
                                </div>
                                <br />
                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <label style="width: 110px; padding-right: 10px;">Puesto:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtAPPuesto" runat="server" ReadOnly="true" EnableViewState="false"></telerik:RadTextBox>
                                </div>

                                <span class="col-md-12 col-sm-12 col-xs-12 " style="padding: 0px"></span>
                                <br />
                                <span id="Span9" class="Base L0">&nbsp;</span>
                                <br />
                                <label style="width: 100px; padding-right: 10px;">Contraseña:</label>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtAPContraseña" runat="server" TextMode="Password" PasswordStrengthSettings-ShowIndicator="false"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator11" runat="server" ControlToValidate="rtbAutoContrasena" ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <asp:Button Text="Ingresar" ID="btnAutorizarReqPuesto" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAutorizarReqPuesto_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>

        </div>
        <!-- Fin de Autorizar Requisición -->

    </div>
    <div>
        <div class="container">
            <div style="text-align: right; position: fixed; bottom: 30px; width: 30%; right: 10px;">
                <img class="zoom" src="Assets/images/Mexico.png" style="cursor: pointer; width: 40px;" onclick="CambiarLenguaje('ES')" />
                <img class="zoom" src="Assets/images/EstadosUnidos.png" style="cursor: pointer; width: 40px;" onclick="CambiarLenguaje('EN')" />
            </div>
        </div>
    </div>
    <div class="PiedePagina">
        <div class="container">
            <div class="col-md-4 col-xs-12">
                <a href="http://www.sigein.com.mx" style="color: #FFF !important" target="_blank">www.sigein.com.mx</a>
            </div>
            <div class="col-md-4 col-xs-12">
                <span id="lblEmpresa" runat="server"></span>
            </div>
            <div class="col-md-4 col-xs-12">
                Copyright 2015 SIGEIN ®
            </div>
        </div>
    </div>
    <!-- Ventana Modal -->
    <telerik:RadWindowManager ID="RadWindowManager1" EnableShadow="true" runat="server" OnClientClose="returnDataToParentPopup">
        <Windows>
            <%--            <telerik:RadWindow ID="modalRecuperaPassword"
                runat="server"
                Width="300"
                Height="400"
                Modal="true"
                VisibleStatusbar="false"
                Behaviors="Close"
                Title="Recupera contraseña"
                NavigateUrl="PasswordRecovery.aspx">
            </telerik:RadWindow>--%>
            <telerik:RadWindow ID="winSolicitud" runat="server" Title="Solicitud" Behaviors="Close" Modal="true" VisibleStatusbar="false" Height="600px" Width="600px" OnClientShow="centerPopUp"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="winPrivacidad" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="None"></telerik:RadWindow>


        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
