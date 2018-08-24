<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaEstiloDePensamiento.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaEstiloDePensamiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPruebas" runat="server">

    <style id="MyCss" type="text/css">
        .CenterDiv {
            text-align: center;
            padding: 2px;
            line-height: 12px;
            font-family: 'Arial Black';
        }

        .DescripcionStyle {
            padding: 2px;
            line-height: 12px;
            font-family: 'Arial Black';
        }

        .DivControlButtons {
            border: 1px solid #CCC;
            text-align: center;
            width: 200px;
            height: 50px;
            padding: 2px;
            position: fixed;
            right: 9px;
            margin-bottom: 5px;
        }

        .DivMoveLeft {
            text-align: right;
            float: left;
            margin-right: 15px;
            margin-left: 15px;
            width: 142px;
        }

        .DivBtnTerminarDerecha {
            float: right;
            width: 100px;
            height: 46px;
            position: absolute;
            right: 10px;
            bottom: 0px;
            margin-bottom: 2px;
        }

        /* LA MODAL QUE SE DESPLAEGARA  EN LAS PRUEBAS: NOTA. SE EXTENDIO ESTA CLASE CSS */
        .TelerikModalOverlay {
            opacity: 1 !important;
            background-color: #000 !important;
        }

        .rotate {
            -webkit-transform: rotate(-90deg);
            -moz-transform: rotate(-90deg);
            -ms-transform: rotate(-90deg);
            -o-transform: rotate(-90deg);
            transform: rotate(-90deg);
            width: 100%;
            height: 100%;
            display: block;
        }

        .ctrlRow {
            float: left;
            padding: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPruebas" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnTerminar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            var vFgEnValidacion = false;
            vPruebaEstatus = "";
            var c = "";
            window.onload = function (sender, args) {
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var segundos = '<%= this.vTiempoPrueba %>';
                            //alert(segundos);
                            if (segundos <= 0 ) {
                                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no se lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
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

                            }
                        }
                        else {
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "<label><b>Instrucciones:</b><br/>El siguiente cuestionario está diseñado para conocer tus preferencias en el estilo de pensamiento, aprendizaje y comunicación.  Esta no es una prueba de inteligencia, no hay respuestas buenas o malas. Por favor contesta todas las preguntas utilizando la siguiente escala:<br /><br />" +
                    "5 - Me gusta mucho o me describe muy bien <br>" +
                    "4 - Me gusta o me describe relativamente bien <br />" +
                    "3 - Me gusta o me describe en forma regular <br />" +
                    "2 - Me disgusta o no me describe <br />" +
                    "1 - Me disgusta o no se aplica a mi <br /><br />" +
                    "Por favor no dejes preguntas sin contestar.</label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Estilo de pensamiento");
                }
            };

            function close_window(sender, args) {

                if (vPruebaEstatus != "TERMINADA") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            if (ValidarContendorPreguntas())
                            {
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

            function WinClose(sender, args) {
                vPruebaEstatus = "TERMINADA";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                //window.close();
                window.location = "Default.aspx?ty=sig";
            }

            //EN PROCESO PARA LA DETECCION DEL 3 EN LOS SEGMENTOS
            function valueChanged(sender, args) {
                    var vNewValue = sender._displayText;
                    var vId = sender.get_id();
                    var vClassName = sender._textBoxElement.className;
                    var vtxtSelected = document.getElementById(vId);
                    var res = vClassName.replace("riEnabled", "");
                    var res = res.replace(" Contenedor", "");
                    var x = document.getElementsByClassName(vClassName);
              

                    var vContenedor = document.getElementsByClassName("Contenedor");
                    //console.info(vContenedor);

                    if (vtxtSelected.value != "") {
                            var i = 0;
                            for (i = 0; i < x.length; i++) {
                                if ((x[i].value == 3 && vNewValue == 3 && x[i].id != vId && res == 'riTextBox  SegmentoNo1') || vNewValue > 5
                                     || vNewValue < 1) {
                                    vtxtSelected.focus();
                                    vtxtSelected.style.borderColor = '#F78181';
                                    vtxtSelected.style.borderWidth = '1px';
                                    break;
                                }
                                else
                                    if (x[i].value == 3 && vNewValue == 3 && x[i].id != vId && res == 'riTextBox  SegmentoNo2') {
                                        vtxtSelected.focus();
                                        vtxtSelected.style.borderColor = '#F78181';
                                        vtxtSelected.style.borderWidth = '1px';
                                        break;
                                    }
                            
                        }
                    }
            }

            function ValidarContendorPreguntas(sender, args) {
                var flag = true;
                var vContenedor = document.getElementsByClassName("Contenedor");

                var i = 0;
                for (i = 0; i < vContenedor.length; i++) {
                    if (vContenedor[i].value == "") {
                        var vId = vContenedor[i].id;
                        var vSelectedItem = document.getElementById(vId);
                        vSelectedItem.focus();
                        vSelectedItem.style.borderColor = '#F78181';
                        vSelectedItem.style.borderWidth = '1px';
                        flag = false;
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
                var ClTipo = '<%= vTipoPrueba %>';

                            var windowProperties = {
                                width: document.documentElement.clientWidth - 20,
                                height: document.documentElement.clientHeight - 20
                            };

                            vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=ESTILO" + "&Type=" + ClTipo;
                            var win = window.open(vURL, '_blank');
                            win.focus();
                            //var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
             }

        </script>
    </telerik:RadCodeBlock>
    <label style="font-size:21px;"> Estilo de pensamiento</label>

    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

            <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px;">
                            <label runat="server"> El siguiente cuestionario está diseñado para conocer tus preferencias en el estilo de pensamiento, aprendizaje y comunicación.  Esta no es una prueba de inteligencia, no hay respuestas buenas o malas. Por favor contesta todas las preguntas utilizando la siguiente escala:</label><br /><br />
                             <label id="Label1" runat="server">5 - Me gusta mucho o me describe muy bien</label>
                            <br />
                             <label id="Label2" runat="server">4 - Me gusta o me describe relativamente bien</label>
                            <br />
                            <label id="Label3" runat="server"> 3 - Me gusta o me describe en forma regular</label>
                            <br />
                             <label id="Label4" runat="server">2 - Me disgusta o no me describe</label>
                            <br />
                             <label id="Label5" runat="server">1 - Me disgusta o no se aplica a mí</label>
                            <br />
                            <br />
                             <label id="Label6" runat="server">Por favor no dejes preguntas sin contestar.</label> 																														
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>


            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <table style="width: 96%; margin-left: 2%; margin-right: 2%;">
                    <thead>
                        <tr>
                            <td width="25%"></td>
                            <td width="25%"></td>
                            <td width="25%"></td>
                            <td width="25%"></td>
                        </tr>
                    </thead>
                    <tbody>

                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="4">
                                <label name="" style="font-weight: bold;">Intereses personales. Segmento 1.</label>
                                <br />
                                <label style="color: #8A0808;">
                                    Nota: La escala 5,4,2,1 se podrá repetir todas las veces que así lo requieras, sin embargo, la calificación 3 (me gusta o me describe de forma regular) no podrá ser utilizada más de una vez en el segmento 1.
                                </label>
                            </td>
                        </tr>
                        <tr>

                            <td colspan="4">
                                <div style="float: left; margin: 2px;">
                                    <label style="font-weight: bold;">Mis pasatiempos:</label>
                                </div>
                            </td>
                        </tr>
                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor"  runat="server" ID="radTxtPreg1Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                       <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Artísticos</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg1Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Sociales</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg1Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Científicos</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg1Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Deportivos</label>
                                </div>
                            </td>
                        </tr>
                        <%--Pregunta 2--%>

                        <tr>

                            <td colspan="4">
                                <div style="float: left; margin: 2px;">
                                    <label style="font-weight: bold;">Materias que me gustaría estudiar:</label>
                                </div>
                            </td>
                        </tr>
                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg2Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Matemáticas</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg2Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Música</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg2Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Filosofía</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg2Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Idiomas</label>
                                </div>
                            </td>
                        </tr>
                        <%--Pregunta 3--%>

                        <tr>

                            <td colspan="4">
                                <div style="float: left; margin: 2px;">
                                    <label style="font-weight: bold;">Gente con la que me gustaría estar:</label>
                                </div>
                            </td>
                        </tr>
                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg3Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Alegres</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg3Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Organizadores</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg3Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Soñadores</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg3Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Prácticos</label>
                                </div>
                            </td>
                        </tr>
                        <%--Pregunta 4--%>


                        <tr>

                            <td colspan="4">
                                <div style="float: left; margin: 2px;">
                                    <label style="font-weight: bold;">Los maestros de los que más aprendí usaban:</label>
                                </div>
                            </td>
                        </tr>
                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg4Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Fórmulas</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg4Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Procedimientos</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg4Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Imágenes</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg4Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Sentimientos</label>
                                </div>
                            </td>
                        </tr>
                        <%--Pregunta 5--%>

                        <tr>

                            <td colspan="4">
                                <div style="float: left; margin: 2px;">
                                    <label style="font-weight: bold;">Los líderes que más respeto son:</label>
                                </div>
                            </td>
                        </tr>
                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg5Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Imaginativos</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg5Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Inspiradores</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg5Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Cuestionadores</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo1 Contenedor" runat="server" ID="radTxtPreg5Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Disciplinados</label>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 60px;"></div>
                            </td>
                        </tr>
                        <%--SEGMENTO 2--%>

                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="4">
                                <label name="" style="font-weight: bold;">Descripción personal (palabras que me describen relativamente bien). Segmento 2.</label>
                                <br />
                                <label style="color: #8A0808;">
                                    Nota: La escala 5,4,2,1 se podrá repetir todas las veces que así lo requieras, sin embargo, la calificación 3 (me gusta o me describe de forma regular) no podrá ser utilizada más de una vez en el segmento 2.
                                </label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>


                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg6Resp1" Width="70px" MaxLength="1" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Creativo</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg6Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Emocional</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg6Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Técnico</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg6Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Comunicativo</label>
                                </div>
                            </td>
                        </tr>
                        <%--Pregunta 2 --%>
            
                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                       <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor"  runat="server" ID="radTxtPreg7Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" style="text-align:center">
                                     <ClientEvents  OnBlur="valueChanged" />
                                </telerik:RadNumericTextBox>
                                   <label style=" width:70px;">Organizado</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                        <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor"  runat="server" ID="radTxtPreg7Resp2" Width="70px" MinValue="1" MaxValue="5"  NumberFormat-DecimalDigits="0" style="text-align:center">
                                     <ClientEvents  OnBlur="valueChanged"  />
                                </telerik:RadNumericTextBox>
                                    <label style=" width:70px;">Conservador</label>
                                </div>
                            </td>
                              <td>                        
                                <div class="ctrlRow">
                                       <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor"  runat="server" ID="radTxtPreg7Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" style="text-align:center">
                                     <ClientEvents  OnBlur="valueChanged"  />
                                </telerik:RadNumericTextBox>
                                      <label style="width:70px;">Controlado</label>
                                </div>
                            </td>

                              <td>                        
                                <div class="ctrlRow">
                                   <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor"  runat="server" ID="radTxtPreg7Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" style="text-align:center">
                                     <ClientEvents  OnBlur="valueChanged"  />
                                </telerik:RadNumericTextBox>
                                    <label style=" width:70px;">Cuantitativo</label>
                                </div>
                            </td>
                        </tr>
                        <%--Pregunta 3--%>

                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg8Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Analítico</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg8Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Intuitivo</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg8Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Científico</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg8Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Visionario</label>
                                </div>
                            </td>
                        </tr>
                        <%--Pregunta 4--%>


                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg9Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Musical</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg9Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Detallista</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg9Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Racional</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg9Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Comprensivo</label>
                                </div>
                            </td>
                        </tr>
                        <%--Pregunta 5--%>

                        <tr class="BorderRadioComponenteHTML">
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg10Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Matemático</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg10Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Original</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg10Resp3" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Conceptual</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo2 Contenedor" runat="server" ID="radTxtPreg10Resp4" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                    <label style="width: 70px;">Afectuoso</label>
                                </div>
                            </td>
                        </tr>
                       
                            <tr>
                            <td>
                                <div style="clear: both; height: 60px;"></div>
                            </td>
                        </tr>
                        
                        <%--SEGMENTO 3--%>

                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="4">
                                <label name="" style="font-weight: bold;">Preferencias personales (en situaciones de trabajo). Segmento 3.</label>
                                <br />
                                <label style="color: #8A0808;">
                                    Nota: En este segmento podrás utilizar toda escala y repetir las veces que así lo requieras. incluyendo el número 3. 
                                </label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>


                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg11Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>
                                      <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Juzgar en hechos más que en sentimientos</label>
                                          </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg11Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                      <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Filosofar sobre temas</label>
                                          </div>
                            </td>

                        </tr>
                        <%--Pregunta 2 --%>
            
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg12Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Ser confiable y que puedan depender de mí</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg12Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Ser bien organizado y sistemático</label>
                                </div>
                            </td>

                        </tr>
                        <%--Pregunta 3 --%>
            

                        
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg13Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Pensar en grande en el futuro</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg13Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Adaptarme a otra gente</label>
                                </div>
                            </td>

                        </tr>
                        <%--Pregunta 4 --%>
            

                        
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg14Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Evaluar situaciones complejas</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg14Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Seguir métodos probados y aprobados</label>
                                </div>
                            </td>

                        </tr>
                        <%--Pregunta 5 --%>
            

                        
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg15Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Usar diagramas para explicar o enseñar</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg15Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Desarrollar nuevos enfoques a problemas</label>
                                </div>
                            </td>

                        </tr>
                        <%--Pregunta 6 --%>
            

                        
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg16Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Trabajo en equipo</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg16Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Explorar teorías o ideas poco comunes</label>
                                </div>
                            </td>

                        </tr>
                        <%--Pregunta 7 --%>
            

                        
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg17Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Ordenar lo caótico</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg17Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Compartir sentimientos con otros</label>
                                </div>
                            </td>

                        </tr>
                        <%--Pregunta 8 --%>
            

                        
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg18Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Diseñar productos o programas originales</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg18Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Resolver esquemas complicados</label>
                                </div>
                            </td>

                        </tr>
                        <%--Pregunta 9 --%>
            

                        
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg19Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Analizar resultados científicamente</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg19Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Divertirme con la gente</label>
                                </div>
                            </td>

                        </tr>
                        <%--Pregunta 10 --%>
            

                        
                        <tr class="BorderRadioComponenteHTML">
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg20Resp1" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Estar intrigado con ideas intrépidas o atrevidas</label>
                                </div>
                            </td>
               
                            <td colspan="2">
                                <div class="ctrlRow">
                                    <telerik:RadNumericTextBox CssClass="SegmentoNo3 Contenedor" runat="server" ID="radTxtPreg20Resp2" Width="70px" MaxLength="1"  NumberFormat-DecimalDigits="0" Style="text-align: center">
                                        <ClientEvents OnBlur="valueChanged" />
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlRow">
                                    <label style="width:162px; height:40px;">Planear el trabajo y trabajar el plan</label>
                                </div>
                            </td>

                        </tr>
            

                    </tbody>
                </table>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="DivMoveLeft" id="cronometro" runat="server">
        <div class="Cronometro">Tiempo restante <span id="time">15:00</span></div>
    </div>
    <div class="divControlesBoton">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
        <telerik:RadButton ID="btnCorregir" runat="server" Visible="false" OnClick="btnCorregir_Click" Text="Corregir" AutoPostBack="true"></telerik:RadButton>
        <telerik:RadButton ID="btnImpresionPrueba" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false" Visible = "false"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
