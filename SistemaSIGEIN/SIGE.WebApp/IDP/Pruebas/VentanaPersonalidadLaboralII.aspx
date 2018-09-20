<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaPersonalidadLaboralII.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaPersonalidadLaboral2" %>

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

        Table td {
            padding: 3px 0px 3px 0px;
        }

            Table td .subrayado {
                max-width: 600px;
                border-bottom: 1px dotted gray;
            }

            Table td .maximo {
                max-width: 600px;
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
            var vPruebaEstatus = "";
            var a = new Array();
            var c = "";
            window.onload = function (sender, args) {
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var segundos = '<%=this.vTiempoPrueba%>';
                            if (segundos <= 0) {
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

                            // window.close();
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "<label><b>Instrucciones:</b><br />Este no es un test con respuestas correctas o incorrectas. Es un cuestionario que te permite describir tu estilo de vida principal y secundario, con el fin de identificar los modos productivos y antiproductivos, en que utilizas tus fuerzas. Encontraras en el cuestionario enunciados descriptivos cada uno seguido por cuatro terminaciones posibles.  En los espacios en blanco a la derecha de cada terminación, coloca los números 4, 3, 2 y 1 de acuerdo a cuál es la terminación que <u>más se te asemeja</u> (4) y cuál es la que sientes que <u>se te asemeja menos</u> (1).<br /><br />" +
                         "Por favor observa este ejemplo:<br /><br /> Casi siempre soy: <br /><br />" +
                         "Benévolo, afable y util&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  (4)  <br />" +
                         "Productivo y lleno de planes&nbsp;(2) <br />" +
                         "Económico y cuidadoso&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(1)  <br />" +
                         "Encantador y popular&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  (3)  <br /><br />" +
                         "No uses 4, 3, 2 y 1 más de una vez. <br />" +
                        "Si encuentras que algunos enunciados del cuestionario tienen 2 o más terminaciones que se te asemejen de igual manera o que son igualmente distintas a como tú sientes que es, colócalas en orden, aunque le resulte difícil. Cada terminación <u>deberás</u> clasificarla como 4, 3, 2 ó 1.</label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Personalidad laboral II");
                }
            };

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
                    radconfirm(text, callBackFunction, 400, 160, null, "");
                    args.set_cancel(true);
                }
                else {
                    // window.close();
                    window.location = "Default.aspx?ty=sig";
                }
            }

            function OpenReport() {
                var vURL = "ReporteadorPruebasIDP.aspx";
                var vTitulo = "Impresión Técnica PC";

                var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClToken %>';



                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=LABORAL2";
                var win = window.open(vURL, '_blank');
                win.focus();
            }

            function WinClose(sender, args) {
                vPruebaEstatus = "Terminado";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                // window.close();
                window.location = "Default.aspx?ty=sig";
            }

            function validarCampos(sender, args) {

                var vNewValue = sender._displayText;
                var vId = sender.get_id();
                var vClassName = sender._textBoxElement.className;

                var vtxtSelected = document.getElementById(vId);
                var res = vClassName.replace("riEnabled", "").replace("riHover", "");
                var x = document.getElementsByClassName(res);

                if (vtxtSelected.value != "") {
                    var i = 0;
                    for (i = 0; i < x.length; i++) {
                        if ((x[i].value == vNewValue && x[i].id != vId) && vNewValue != undefined
                            || vtxtSelected.value == "0" || vtxtSelected.value > 4) {
                            vtxtSelected.focus();
                            vtxtSelected.style.borderColor = 'red';
                            vtxtSelected.style.borderWidth = '1px';
                            vtxtSelected.value = "";
                        }
                    }
                }
            }

            function ValidarContendorPreguntas(sender, args) {
                var flag = true;
                var GNoContestadas = new Array();
                var vContenedor = document.getElementsByClassName("Contenedor");
                jsonObj = [];
                var i = 0;
                for (i = 0; i < vContenedor.length; i++) {
                    var vClassName = vContenedor.className;
                    if (vContenedor[i].value == "") {
                        var GrupoNoContestado = document.getElementById(vContenedor[i].id);
                        GrupoNoContestado.focus();
                        GrupoNoContestado.style.borderWidth = '1px';
                        var flag = false;
                        radalert("Hay preguntas que no han sido contestadas. Revise la prueba por favor.", 400, 150, "");
                        break;
                    } else {

                        item = {}
                        item["valor"] = vContenedor[i].value;

                        jsonObj.push(item);
                    }
                }
                return flag;
            }

            //function ConfirmarEliminarRespuestas(sender, args) {
            //    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
            //        if (shouldSubmit) {
            //            this.click();
            //        }
            //    });
            //    radconfirm("Este proceso borrará las respuestas de la prueba, ¿Deseas continuar?", callBackFunction, 400, 150, null, "Eliminar respuestas");
            //    args.set_cancel(true);
            //}

        </script>
    </telerik:RadCodeBlock>

    <label style="font-size: 21px;">Personalidad laboral II</label>

    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

            <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="370">
                        <table>
                            <tr>
                                <td style="width: 10px;">&nbsp;</td>
                                <td style="background-color: white; padding: 5px;">
                                    <label>
                                        Este no es un test con respuestas correctas o incorrectas. Es un cuestionario 
                    que te permite describir tu estilo de vida principal y secundario, con 
                    el fin de identificar los modos productivos y antiproductivos, en que  
                    utilizas tus fuerzas. Encontraras en el cuestionario enunciados descriptivos 
                    cada uno seguido por cuatro terminaciones posibles.&nbsp; En los espacios en blanco a 
                    la derecha de cada terminación, coloca los números 4, 3, 2 y 1 de acuerdo a cuál 
                    es la terminación que <span style="text-decoration: underline; font-weight: bold;">más se te asemeja</span> (4) y cuál es la que sientes que 
                    <span style="text-decoration: underline; font-weight: bold;">se te asemeja 
                    menos</span> (1).<br />
                                        <br />
                                        Por favor observa este ejemplo:<br />
                                        <br />
                                        Casi siempre soy:<br />
                                        <br />

                                        <table>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Benévolo, afable y util</td>
                                                <td>(4)</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Productivo y lleno de planes</td>
                                                <td>(2)</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Económico y cuidadoso</td>
                                                <td>(1)</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Encantador y popular</td>
                                                <td>(3)</td>
                                            </tr>
                                        </table>

                                        <br />
                                        No uses 4, 3, 2 y 1 más de una vez.<br />
                                        <br />
                                        Si encuentras que algunos enunciados del cuestionario tienen 2 o más 
                    terminaciones que se te asemejen de igual manera o que son igualmente distintas 
                    a como tú sientes que es, colócalas en orden, aunque le 
                    resulte difícil. Cada terminación <span style="text-decoration: underline; font-weight: bold;">deberás</span> clasificarla como 4, 3, 2 ó 1.</label></td>
                            </tr>
                        </table>

                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>

            </telerik:RadPane>

            <telerik:RadPane ID="radPanelPreguntas" runat="server">

                <table style="width: 98%; height: 400px; margin-left: 2%">
                    <thead>
                        <tr>
                            <td width="700px"></td>
                            <td width="140px"></td>
                        </tr>
                    </thead>

                    <tbody>

                        <!-- Pregunta 1 -->

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta1" id="lblpregunta1" style="font-weight: bold">1. Me siento más contento conmigo mismo cuando:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg1Resp1" id="lblPregResp1">Actúo con idealismo y optimismo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo1 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg1Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg1Resp2" id="lblPregResp2">Veo una oportunidad de liderazgo y voy tras ella</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo1 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg1Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg1Resp3" id="lblPreg1Resp3">Busco mi propio interés y dejo a los demás buscar el suyo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo1 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg1Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg1Resp4" id="lblPreg1Resp4">Me adapto al grupo en el cual me encuentro</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo1 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg1Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>

                        <!-- Pregunta 2 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta2" id="lblpregunta2" style="font-weight: bold">2. Soy sumamente apto para tratar a otros:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg2Resp1" id="lblPreg2Resp1">Respetuoso, cortés y con admiración</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo2 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg2Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg2Resp2" id="lblPreg2Resp2">Activo, enérgico y con seguridad en mí mismo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo2 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg2Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg2Resp3" id="lblPreg2Resp3">Cuidadoso, reservado, con tranquilidad</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo2 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg2Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg2Resp4" id="lblPreg2Resp4">Con simpatía, social y amistosamente</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo2 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg2Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>

                        <!-- Pregunta 3 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta3" id="lblpregunta3" style="font-weight: bold">3. Hago sentir a los otros:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg3Resp1" id="lblPreg3Resp1">Bien considerados, capaces y dignos de que les pida consejo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo3 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg3Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg3Resp2" id="lblPreg3Resp2">Interesados y entusiasmados por asociarse conmigo y deseosos de tenerme cerca</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo3 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg3Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg3Resp3" id="lblPreg3Resp3">Tratados con justicia, respetados y apreciando la consideración que les profeso</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo3 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg3Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg3Resp4" id="lblPreg3Resp4">Complacidos, impresionados conmigo y deseosos de tenerme cerca</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo3 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg3Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 4 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta4" id="lblpregunta4" style="font-weight: bold">4. En un desacuerdo con otra persona me va mejor si:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg4Resp1" id="lblPreg4Resp1">Me fío del sentido de la justicia</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo4 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg4Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg4Resp2" id="lblPreg4Resp2">Trato de manejarla por medio de mi astucia y superioridad táctica</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo4 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg4Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg4Resp3" id="lblPreg4Resp3">Permanezco tranquilo, metódico e impasible</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo4 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg4Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg4Resp4" id="lblPreg4Resp4">Soy flexible y me adapto a la otra persona</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo4 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg4Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 5 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta5" id="lblpregunta5" style="font-weight: bold">5. En mis relaciones con los demás puedo:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg5Resp1" id="lblPreg5Resp1">Volverme confidencial y dar confianza aún a aquellos que no parecen buscarla</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo5 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg5Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg5Resp2" id="lblPreg5Resp2">Volverme agresivo y aprovecharme de los otros antes de que se den cuenta de que no he sido muy considerado con ello</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo5 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg5Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg5Resp3" id="lblPreg5Resp3">Volverme suspicaz y prudente, y tratarlos con demasiada reserva</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo5 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg5Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg5Resp4" id="lblPreg5Resp4">Volverme demasiado amistoso y hallarme en medio de gente aún cuando no he sido especialmente invitado</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo5 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg5Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 6 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta6" id="lblpregunta6" style="font-weight: bold">6. Impresiono a los demás como:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg6Resp1" id="lblPreg6Resp1">Una persona ingenua que tiene poca iniciativa y confianza en sí misma</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo6 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg6Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg6Resp2" id="lblPreg6Resp2">Un agudo "corredor de bolsa" que siempre trata de sacar el mayor partido posible de la ganga</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo6 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg6Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg6Resp3" id="lblPreg6Resp3">Un individuo obstinado que es frío hacia los demás</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo6 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg6Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg6Resp4" id="lblPreg6Resp4">Una persona indefinida que nunca toma verdadera posición personal</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo6 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg6Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 7 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta7" id="lblpregunta7" style="font-weight: bold">7. Siento que puedo persuadir a la gente siendo:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg7Resp1" id="lblPreg7Resp1">Modesto e idealista</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo7 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg7Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg7Resp2" id="lblPreg7Resp2">Convincente y seguro de mí mismo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo7 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg7Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg7Resp3" id="lblPreg7Resp3">Paciente y práctico</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo7 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg7Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg7Resp4" id="lblPreg7Resp4">Entretenido y animado</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo7 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg7Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 8 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta8" id="lblpregunta8" style="font-weight: bold">8. En mis relaciones con los demás soy sumamente apto para ser:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg8Resp1" id="lblPreg8Resp1">Creíble, confiable y de apoyo para otras personas</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo8 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg8Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg8Resp2" id="lblPreg8Resp2">Rápido para desarrollar ideas útiles y organizar a los demás para que las lleven a cabo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo8 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg8Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg8Resp3" id="lblPreg8Resp3">Práctico, lógico y cuidadoso en saber con quién estoy tratando</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo8 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg8Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg8Resp4" id="lblPreg8Resp4">Interesado en saber todo acerca de ellos y ansioso por ajustarme a lo que esperan de mí</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo8 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg8Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 9 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta9" id="lblpregunta9" style="font-weight: bold">9. Siento suma satisfacción cuando los demás me ven como:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg9Resp1" id="lblPreg9Resp1">Un amigo leal y de confianza</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo9 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg9Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg9Resp2" id="lblPreg9Resp2">Una persona que puede tomar ideas y ponerlas en práctica</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo9 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg9Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg9Resp3" id="lblPreg9Resp3">Una persona prática y que piensa por sí misma</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo9 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg9Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg9Resp4" id="lblPreg9Resp4">Una persona digna de atención y significativa</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo9 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg9Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 10 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta10" id="lblpregunta10" style="font-weight: bold">10. Si no obtengo lo que quiero de una persona tiendo a:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg10Resp1" id="lblPreg10Resp1">Rendirme de buena gana y justificar la inhabilidad de la otra persona para hacer lo mismo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo10 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg10Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg10Resp2" id="lblPreg10Resp2">Reclamar mis derechos y tratar de persuadirla para que lo haga de todas maneras</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo10 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg10Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg10Resp3" id="lblPreg10Resp3">Sentirme indiferente y encontrar otra manera de conseguir lo que quiero</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo10 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg10Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg10Resp4" id="lblPreg10Resp4">Tomarlo en broma y ser flexible acerca del problema</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo10 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg10Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>



                        <!-- Pregunta 11 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta11" id="lblpregunta11" style="font-weight: bold">11. Ante el fracaso siento que lo mejor es:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg11Resp1" id="lblPreg11Resp1">Acudir a otros y confiar en su ayuda</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo11 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg11Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg11Resp2" id="lblPreg11Resp2">Pelear por mis derechos y tomar lo que realmente me merezco</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo11 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg11Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg11Resp3" id="lblPreg11Resp3">Mantener lo que ya tengo y desatenderme de los demás</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo11 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg11Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg11Resp4" id="lblPreg11Resp4">Conservar la fachada y tratar de venderme al mejor precio posible</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo11 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg11Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 12 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta12" id="lblpregunta12" style="font-weight: bold">12. Temo que a veces los demás puedan verme como:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg12Resp1" id="lblPreg12Resp1">Sometido e impresionable</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo12 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg12Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg12Resp2" id="lblPreg12Resp2">Agresivo y arrogante</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo12 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg12Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg12Resp3" id="lblPreg12Resp3">Frío y obstinado</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo12 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg12Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg12Resp4" id="lblPreg12Resp4">Superficial y en busca de atención</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo12 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg12Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 13 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta13" id="lblpregunta13" style="font-weight: bold">13. Siento que el mejor modo de triunfar en la vida es:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg13Resp1" id="lblPreg13Resp1">Ser una persona digna de recompensa y confiar en quienes tienen autoridad para reconocer mi valor</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo13 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg13Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg13Resp2" id="lblPreg13Resp2">Trabajar para establecer un derecho a avanzar y luego reclamarlo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo13 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg13Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg13Resp3" id="lblPreg13Resp3">Preservar lo que ya tengo y construir sobre ello</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo13 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg13Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg13Resp4" id="lblPreg13Resp4">Desarrollar una personalidad triunfante que llame la atención de los demás</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo13 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg13Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 14 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta14" id="lblpregunta14" style="font-weight: bold">14. Resolviendo el problema de trabajar con una persona difícil:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg14Resp1" id="lblPreg14Resp1">Averiguo con otros cómo han resuelto el problema y sigo sus consejos</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo14 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg14Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg14Resp2" id="lblPreg14Resp2">Llego a un acuerdo con la persona y sigo junto a ella del mejor modo posible</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo14 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg14Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg14Resp3" id="lblPreg14Resp3">Decido por mí mismo lo que es correcto y mantengo mis propias convicciones</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo14 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg14Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg14Resp4" id="lblPreg14Resp4">Me modifico, busco la manera de adaptarme a la otra persona y hacer la relación más armónica</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo14 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg14Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 15 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta15" id="lblpregunta15" style="font-weight: bold">15. Impresiono a los demás como:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg15Resp1" id="lblPreg15Resp1">Una persona confiada que aprecia ayuda y consejo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo15 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg15Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg15Resp2" id="lblPreg15Resp2">Una persona con confianza en sí misma, que toma la iniciativa y hace actuar a la gente</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo15 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg15Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg15Resp3" id="lblPreg15Resp3">Una persona que establece y trata con los demás de una manera conservadora</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo15 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg15Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg15Resp4" id="lblPreg15Resp4">Una persona entusiasta que puede congeniar con casi todo el mundo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo15 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg15Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 16 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta16" id="lblpregunta16" style="font-weight: bold">16. Siento que en el último análisis es mejor:</label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg16Resp1" id="lblPreg16Resp1">Simplemente aceptar la derrota y buscar lo que deseo en alguna otra parte</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo16 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg16Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg16Resp2" id="lblPreg16Resp2">Empeñarme en una lucha de estrategias antes que perder y no obtener nada</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo16 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg16Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg16Resp3" id="lblPreg16Resp3">Ser suspicaz y posesivo antes que renunciar a lo que ya tengo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo16 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg16Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg16Resp4" id="lblPreg16Resp4">Transigir y continuar por el momento</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo16 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg16Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 17 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta17" id="lblpregunta17" style="font-weight: bold">17. A veces puedo ser:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg17Resp1" id="lblPreg17Resp1">Fácilmente influenciable y falto de seguridad</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo17 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg17Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg17Resp2" id="lblPreg17Resp2">Agresivo, ambicioso y arrogante</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo17 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg17Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg17Resp3" id="lblPreg17Resp3">Desconfiado, frío y crítico</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo17 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg17Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg17Resp4" id="lblPreg17Resp4">Pueril y dado a tratar de ser la estrella del espectáculo</label></td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo17 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg17Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>


                        <!-- Pregunta 18 -->
                        <tr>
                            <td colspan="2">&nbsp</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <label name="lblpregunta18" id="lblpregunta18" style="font-weight: bold">18. A veces puedo hacer que los demás se sientan:</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg18Resp1" id="lblPreg18Resp1">Superiores y condescendientes conmigo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo18 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg18Resp1" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg18Resp2" id="lblPreg18Resp2">Utilizados por mí y enojados conmigo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo18 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg18Resp2" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg18Resp3" id="lblPreg18Resp3">Injustamente tratados y fríos hacia mí</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo18 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg18Resp3" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="subrayado">
                                <label name="lblPreg18Resp4" id="lblPreg18Resp4">Impacientes e indiferentes hacia mí</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox CssClass="PreguntaNo18 Contenedor" AllowOutOfRangeAutoCorrect="false" MaxLength="1" runat="server" ID="radTxtPreg18Resp4" Width="100px" MinValue="1" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                    <ClientEvents OnBlur="validarCampos" />
                                </telerik:RadNumericTextBox>
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

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCorregir" runat="server" Visible="false" OnClick="btnCorregir_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnImpresionPrueba" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false" Visible="false"></telerik:RadButton>
        </div>

<%--         <div class="ctrlBasico">
                  <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" Visible="false" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminar_Click"></telerik:RadButton>
             </div>--%>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
