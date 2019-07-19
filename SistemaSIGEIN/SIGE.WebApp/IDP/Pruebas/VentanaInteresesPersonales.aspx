<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaInteresesPersonales.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaInteresesPersonales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPruebas" runat="server">
    <style id="Estilos" type="text/css">
        .divPruebaTitulo {
            margin-left: 50px;
            font-family: 'Arial Black';
            width: 25%;
            float: left;
        }

        .divPruebaTituloFinal {
            margin-left: 50px;
            font-family: 'Arial Black';
            width: 30%;
        }

         .DivMoveLeft {
            text-align: right;
            float: left;
            margin-right: 15px;
            margin-left: 15px;
            width: 142px;
        }


        .divContenido {
            text-align: left;
            width: 100%;
        }

        .labelPrueba {
            font-family: Arial;
            margin-left: 5px;
            width: 50%;
        }

        .TelerikModalOverlay {
            opacity: 1 !important;
            background-color: #000 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPruebas" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" ClientEvents-OnResponseEnd="retorno">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnTerminar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script id="contentScript" type="text/javascript">
            var vPruebaEstatus = "";
            var vFgEnValidacion = false;
            var c = "";
            var a = new Array();
            window.onload = function (sender, args) {
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var ajaxManager = $find("<%=RadAjaxManager1.ClientID%>");
                            ajaxManager.ajaxRequest(null);
                            var segundos = '<%=this.vTiempoPrueba%>';
                            if (segundos <= 0) {
                                // var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no se lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                                var oWnd = radalert("El tiempo de la aplicación de la prueba ha concluido y has dejado espacios en blanco, por lo que la prueba será invalidada. Te sugerimos contactar al ejecutivo de selección para más opciones, ya que el sistema no te permitirá regresar a la aplicación.", 400, 300, "");
                                oWnd.add_close(CloseTest);
                            }
                            else {
                                var display = document.querySelector('#time');
                                var contenedor = document.querySelector('.Cronometro');

                                var vFgCronometro = '<%=MostrarCronometro %>';
                                if (vFgCronometro == "True") {
                                    contenedor.style.display = 'block';
                                }
                                else {
                                    contenedor.style.display = 'none';
                                }

                                c = Cronometro(segundos, display);

                                var pane = $find("<%= rpnInteresesPersonales.ClientID %>");
                                pane.collapse();
                            }
                        }
                        else {

                            window.location = "Default.aspx?ty=Ini";
                        }
                    });

                    var text = "<label><b>Instrucciones:</b><br/>Los valores personales son aquellos intereses, metas y preferencias que guían nuestras vidas " +
                          "y carreras, cada persona debe tomar decisiones difíciles para disfrutar de la vida y evitar " +
                          "conflictos en la carrera, para ayudarte a entender de una mejor manera tus valores y tener " +
                          "mayor felicidad y éxito. Por favor indica tus preferencias personales en cada uno de los 10 " +
                          "grupos de valores enlistados a continuación: 6 el que escogerías primero, 5 el que " +
                          "escogerías en segundo lugar y así sucesivamente hasta que pongas el número 1 al que " +
                          "escogerías en último lugar; debes de poner un número a todos los factores y no es válido " +
                          "poner empates.</label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Intereses personales");
                }
            };

            function retorno(sender, args) {
                var segundos = '<%=this.vTiempoPrueba%>';
                var display = document.querySelector('#time');
                var contenedor = document.querySelector('.Cronometro');


                var vFgCronometro = '<%=MostrarCronometro %>';
                if (vFgCronometro == "True") {
                    contenedor.style.display = 'block';
                }
                else {
                    contenedor.style.display = 'none';
                }


                c = Cronometro(segundos, display);

                setTimeout(function () {
                    var pane = $find("<%= rpnInteresesPersonales.ClientID %>");
                    pane.expand();
                }, 1000);                
            }


            function close_window(sender, args) {
                if (vPruebaEstatus != "Terminado") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            if (ValidarContendorPreguntas()) {
                                clearInterval(c);//Se agrega para detener el tiempo del reloj antes de guardar resultados 12/04/2018
                                var btn = $find("<%=btnTerminar.ClientID%>");
                                btn.click();
                            }
                        }
                    });

                    var text = "¿Estás seguro que deseas terminar tu prueba?";
                    radconfirm(text, callBackFunction, 400, 150, null, "");
                    args.set_cancel(true);
                }
                else {
                    //window.close();
                    window.location = "Default.aspx?ty=sig";
                }
            }

            function WinClose() {
                vPruebaEstatus = "Terminado";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                //var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                var oWnd = radalert("El tiempo de la aplicación de la prueba ha concluido y has dejado espacios en blanco, por lo que la prueba será invalidada. Te sugerimos contactar al ejecutivo de selección para más opciones, ya que el sistema no te permitirá regresar a la aplicación.", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                window.location = "Default.aspx?ty=sig";
            }

            function Close() {
                window.top.location.href = window.top.location.href;
                //window.close();
            }

            function validarCampos(sender, args) {
                if (!vFgEnValidacion) {
                    vFgEnValidacion = true;
                    var vNewValue = sender._displayText;
                    var vId = sender.get_id();
                    var vClassName = sender._textBoxElement.className;//document.getElementById(vId).className;
                    var vtxtSelected = document.getElementById(vId);
                    var res = vClassName.replace("riEnabled", "").replace("riHover", "");
                    var x = document.getElementsByClassName(res);
                    if (vtxtSelected.value != "") {
                        var i = 0;
                        for (i = 0; i < x.length; i++) {
                            if ((x[i].value == vNewValue && x[i].id != vId) || vtxtSelected.value == "0" || vtxtSelected.value > 6) {
                                vtxtSelected.focus();
                                vtxtSelected.style.borderColor = 'red';
                                vtxtSelected.style.borderWidth = '1px';
                                vtxtSelected.value = "";
                                break;
                            }
                        }
                    }
                    vFgEnValidacion = false;
                }
            }

            function removeElementsByClass(className) {
                var elements = document.getElementsByClassName(className);
            }


            function addGrupoContestado(valor) {
                if (a.indexOf(valor) == -1 || a.length == 0) {
                    a.push(valor);
                    console.info(a);
                }
            }

            function ValidarContendorPreguntas(sender, args) {
                var flag = true;
                var GNoContestadas = new Array();
                var vContenedor = document.getElementsByClassName("Contenedor");
                var i = 0;
                for (i = 0; i < vContenedor.length; i++) {
                    if (vContenedor[i].value == "") {
                        var GrupoNoContestado = document.getElementById(vContenedor[i].id);
                        GrupoNoContestado.value = "";
                        GrupoNoContestado.style.border = 'red';
                        GrupoNoContestado.style.borderWidth = '1px';
                        GrupoNoContestado.focus();
                        GrupoNoContestado.get_styles().EnabledStyle[0] += "border-color: Red;";
                        var flag = false;
                        radalert("Hay preguntas que no han sido contestadas. Revisa la prueba por favor.", 400, 150, "");
                        break;
                    }
                }
                return flag;
            }

            function OpenReport() {
                var vURL = "ReporteadorPruebasIDP.aspx";
                var vTitulo = "Impresión intereses personales";

                var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClTokenExterno %>';



                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=INTERES";
                var win = window.open(vURL, '_blank');
                win.focus();
                //var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
            }


            function ConfirmarEliminarRespuestas(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                radconfirm("Este proceso borrará las respuestas de todas las pruebas de la batería ¿Desea continuar?", callBackFunction, 400, 180, null, "Aviso");
                args.set_cancel(true);
            }

            function ConfirmarEliminarPrueba(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                radconfirm("Este proceso borrará las respuestas de la prueba seleccionada ¿Desea continuar?", callBackFunction, 400, 180, null, "Aviso");
                args.set_cancel(true);
            }
        </script>
    </telerik:RadCodeBlock>
    <label style="font-size: 21px;">Intereses personales</label>
    <div style="height: calc(100% - 100px); overflow: auto;">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
            <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="280">
                        <p style="margin: 10px; text-align: justify;">
                            <label runat="server">
                                Los valores personales son aquellos intereses, metas y preferencias que guían nuestras vidas
                            y carreras, cada persona debe tomar decisiones difíciles para disfrutar de la vida y evitar 
                            conflictos en la carrera, para ayudarte a entender de una mejor manera tus valores y tener
                            mayor felicidad y éxito. Por favor indica tus preferencias personales en cada uno de los 10
                            grupos de valores enlistados a continuación: 6 el que escogerías primero, 5 el que 
                            escogerías en segundo lugar y así sucesivamente hasta que pongas el número 1 al que
                            escogerías en último lugar; debes poner un número a todos los factores y no es válido
                            poner empates.</label>
                        </p>
                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>

            </telerik:RadPane>

            <telerik:RadPane ID="rpnInteresesPersonales" runat="server">
                <div class="divPruebaTitulo" style="float: left;">
                    1 - Intereses personales
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk1ans1" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px">
                        </telerik:RadNumericTextBox>
                        <label class="labelPrueba">Liderazgo</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk1ans2" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px">
                        </telerik:RadNumericTextBox>
                        <label class="labelPrueba">Justicia</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk1ans3" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px">
                        </telerik:RadNumericTextBox>
                        <label class="labelPrueba">Cultura</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk1ans4" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px">
                        </telerik:RadNumericTextBox>
                        <label class="labelPrueba">Dinero</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk1ans5" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px">
                        </telerik:RadNumericTextBox>
                        <label class="labelPrueba">Servicio</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk1ans6" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px">
                        </telerik:RadNumericTextBox>
                        <label class="labelPrueba">Conocimientos</label>
                    </div>
                </div>
                <div class="divPruebaTitulo">
                    2 - Motivadores personales
             <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk2ans1" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px">
                        </telerik:RadNumericTextBox>
                        <label class="labelPrueba">Dirigir personas y situaciones</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk2ans2" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px">
                        </telerik:RadNumericTextBox>
                        <label class="labelPrueba">Continuar estudiando</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk2ans3" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Cristalizar sueños</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk2ans4" CssClass="pregtunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Ayudar a otros</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk2ans5" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Incrementar riquezas</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk2ans6" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Participar en el arte</label>
                    </div>
                </div>
                <div class="divPruebaTitulo">
                    3 - Pasatiempos favoritos
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk3ans1" CssClass="pregunta3 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Caritativos</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk3ans2" CssClass="pregunta3 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Estudios</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk3ans3" CssClass="pregunta3 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Política</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk3ans4" CssClass="pregunta3 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Inversiones</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk3ans5" CssClass="pregunta3 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Museos</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk3ans6" CssClass="pregunta3 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Meditación</label>
                    </div>
                </div>
                <div style="clear: both; height: 30px;"></div>
                <div class="divPruebaTitulo">
                    4 - Metas profesionales
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk4ans1" CssClass="pregunta4 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Artísticas</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk4ans2" CssClass="pregunta4 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Científicas</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk4ans3" CssClass="pregunta4 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Negocio propio (empresariales)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk4ans4" CssClass="pregunta4 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Política (status)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk4ans5" CssClass="pregunta4 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Justicia</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk4ans6" CssClass="pregunta4 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Servicio</label>
                    </div>
                </div>
                <div class="divPruebaTitulo">
                    5 - Autodesarrollo
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk5ans1" CssClass="pregunta5 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Crecimiento espiritual</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk5ans2" CssClass="pregunta5 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Relaciones intepersonales</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk5ans3" CssClass="pregunta5 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Habilidades de liderazgo</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk5ans4" CssClass="pregunta5 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Finanzas personales</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk5ans5" CssClass="pregunta5 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Continuación de estudios</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk5ans6" CssClass="pregunta5 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Desarrollo de habilidades artísticas</label>
                    </div>
                </div>
                <div class="divPruebaTitulo">
                    6 - Intereses educacionales
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk6ans1" CssClass="pregunta6 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Ciencias Físicas</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk6ans2" CssClass="pregunta6 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Ciencias políticas</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk6ans3" CssClass="pregunta6 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Teología</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk6ans4" CssClass="pregunta6 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Artes (Estética)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk6ans5" CssClass="pregunta6 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Finanzas (Ciencias económicas)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk6ans6" CssClass="pregunta6 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Sociología (Ciencias sociales)</label>
                    </div>
                </div>
                <div style="clear: both; height: 30px;"></div>
                <div class="divPruebaTitulo">
                    7 - Reputación deseada
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk7ans1" CssClass="pregunta7 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Poderoso (influencia)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk7ans2" CssClass="pregunta7 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Bondadoso (samaritano)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk7ans3" CssClass="pregunta7 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Capitalista (empresario)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk7ans4" CssClass="pregunta7 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Mediador (diplomático)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk7ans5" CssClass="pregunta7 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Artista (artesano)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk7ans6" CssClass="pregunta7 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Intelectual (investigador)</label>
                    </div>
                </div>
                <div class="divPruebaTitulo">
                    8 - Papel en la sociedad
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk8ans1" CssClass="pregunta8 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Filántropo</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk8ans2" CssClass="pregunta8 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Empresario (patrón)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk8ans3" CssClass="pregunta8 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Líder político (gobernante)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk8ans4" CssClass="pregunta8 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Patrocinador de arte</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk8ans5" CssClass="pregunta8 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Líder intelectual</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk8ans6" CssClass="pregunta8 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Consejero espiritual</label>
                    </div>
                </div>
                <div class="divPruebaTitulo">
                    9 - Metas personales
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk9ans1" CssClass="pregunta9 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Humanitarias</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk9ans2" CssClass="pregunta9 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Económicas</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk9ans3" CssClass="pregunta9 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Científicas</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk9ans4" CssClass="pregunta9 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Ser un líder (políticas)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk9ans5" CssClass="pregunta9 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Ser un erudito (creativas)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk9ans6" CssClass="pregunta9 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Reformador social (éticas)</label>
                    </div>
                </div>
                <div style="clear: both; height: 30px;"></div>
                <div class="divPruebaTituloFinal">
                    10 - Vocación
            <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk10ans1" CssClass="pregunta10 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Enseñanza (lectura)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk10ans2" CssClass="pregunta10 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Drama (teatro)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk10ans3" CssClass="pregunta10 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Trabajo social comunitario</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk10ans4" CssClass="pregunta10 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Negocios (ventas)</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk10ans5" CssClass="pregunta10 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Deportes</label>
                    </div>
                    <div style="clear: both; height: 5px;"></div>
                    <div class="divContenido">
                        <telerik:RadNumericTextBox ID="txtAsk10ans6" CssClass="pregunta10 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarCampos" MaxLength="1" MaxValue="6" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="75px"></telerik:RadNumericTextBox>
                        <label class="labelPrueba">Religión</label>
                    </div>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="DivMoveLeft" id="cronometro" runat="server">
        <div class="Cronometro">Tiempo restante <span id="time"></span></div>
    </div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnTerminar" Text="Terminar" AutoPostBack="true" OnClientClicking="close_window" OnClick="btnTerminar_Click" runat="server"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCorregir" runat="server" Visible="false" OnClick="btnCorregir_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnImpresionPrueba" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false" Visible = "false"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminarPrueba" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminarBateria" runat="server" Text="Eliminar batería" AutoPostBack="true" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminarBateria_Click"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
