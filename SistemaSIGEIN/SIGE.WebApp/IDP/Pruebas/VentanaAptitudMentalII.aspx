<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaAptitudMentalII.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAptitudMental2" %>

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

        .labelPregunta {
            font-weight: bold !important;
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


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            vPruebaEstatus = "";
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
                               // var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                                var oWnd = radalert("El tiempo de la aplicación de la prueba ha concluido. Recuerda que no es posible regresar a ella, si intentas hacerlo a través del botón del navegador, la aplicación no lo permitirá, generando un error y registrando el intento.", 400, 300, "");
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

                                var pane = $find("<%= radPanelPreguntas.ClientID %>");
                                pane.collapse();
                            }
                        }
                        else {
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "<label><b>Instrucciones:</b><br/>Esta prueba  se  compone de diversas preguntas y problemas que tendrás que resolver. Revisa este ejemplo para  contestar el siguiente cuestionario. <br /><br />" +
                        "<b>EJEMPLO 1:</b> ¿Cuál de las cinco palabras nos indica que es una manzana? <br />" +
                        "a) Flor&nbsp;  b) Árbol&nbsp;  c) Legumbre&nbsp; d) Fruto&nbsp;  e) Animal  <br/><br/>" +
                        "La respuesta exacta es  &#8220;Fruto&#8221;. Por eso se debe seleccionar  la letra D, que es la que va delante de &#8220;Fruto&#8221;.<br />" +
                        "Ud. Deberá responder de esta misma forma, es decir, seleccionando la letra que está delante de la respuesta.<br /><br />" +
                        "<b>EJEMPLO 2:</b> ¿Cuál de éstas cosas es redonda?<br />" +
                        "a) Libro &nbsp;  b) Ladrillo &nbsp;  c) Pelota&nbsp; d) Casa &nbsp;  e) Baúl  <br/><br/>" +
                        "La respuesta correcta es PELOTA; así pues, la respuesta correcta es &#8220;c&#8221;.<br /><br />" +
                        "<b>EJEMPLO 3:</b> ¿Cuál de estos números tiene todas sus cifras impares?<br />" +
                        "a) 243 &nbsp; b) 9,871&nbsp;  c) 6,482&nbsp; d) 3,175&nbsp;  e) 19,783  <br/><br/>" +
                        "La respuesta correcta es &#8220;d&#8221;.<br /><br />" +
                        "<b>EJEMPLO 4:</b> El precio de una pastilla es de 20 centavos ¿Cuánto costarán seis pastillas?<br />" +
                        "a) 1.80 pesos&nbsp;  b) 1.20 pesos &nbsp; c) 0.90 pesos &nbsp;d) 1.30 pesos &nbsp; e) 0.75 pesos  <br/><br/>" +
                        "La solución es de 1 peso 20 centavos , así pues la respuesta correcta  es &#8220;b&#8221;.<br /><br />" +
                        "Esta prueba consta de 75 ejercicios, resuelva todos los que pueda.<br />" +
                        "A partir de la señal dada por el examinador, dispondrás de media hora. Trabaja lo más rápida y exactamente que puedas. No se detenga mucho  en una pregunta. Si llegas a una que no comprendas, pasa a la siguiente.</label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Aptitud mental II");

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
                    var pane = $find("<%= radPanelPreguntas.ClientID %>");
                    pane.expand();
                }, 1000);                
            }

            function close_window(sender, args) {
                if (vPruebaEstatus != "Terminado") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                            if (shouldSubmit) {
                            //if (ValidarContendorPreguntas()) {
                                clearInterval(c);//Se agrega para detener el tiempo del reloj antes de guardar resultados 12/04/2018
                                var btn = $find("<%=btnTerminar.ClientID%>");
                                btn.click();
                            //}
                            
                            }
                        });

                        var text = "¿Estás seguro que deseas terminar tu prueba";
                        var contestado = ValidarContendorPreguntas();
                        if (contestado) {
                            text += "?";
                        }
                        else {
                            text += ", aunque hay preguntas sin responder?";
                        }
                        radconfirm(text, callBackFunction, 400, 160, null, "");
                        args.set_cancel(true);
                    }
                    else {
                       // window.close();
                }
            }

            function WinClose(sender, args) {
                vPruebaEstatus = "Terminado";
                var btn = $find("<%=btnTerminar.ClientID%>");
                    btn.click();
                }

                function mensajePruebaTerminada() {
                    //var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no se lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                    var oWnd = radalert("El tiempo de la aplicación de la prueba ha concluido. Recuerda que no es posible regresar a ella, si intentas hacerlo a través del botón del navegador, la aplicación no lo permitirá, generando un error y registrando el intento.", 400, 300, "");
                    oWnd.add_close(WinClose);
                }

                function CloseTest() {
                    window.location = "Default.aspx?ty=sig";
                }

                function Close() {
                    window.top.location.href = window.top.location.href;
                    //window.close();
                }

                function addGrupoContestado(valor) {

                    if (a.indexOf(valor) == -1 || a.length == 0) {
                        a.push(valor);
                    }
                }

                function OpenReport() {
                    var vURL = "ReporteadorPruebasIDP.aspx";
                    var vTitulo = "Impresión Aptitud Mental II";

                    var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClToken %>';



                    var windowProperties = {
                        width: document.documentElement.clientWidth - 20,
                        height: document.documentElement.clientHeight - 20
                    };

                    vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=APTITUD2";
                    var win = window.open(vURL, '_blank');
                    win.focus();
                }

                function valueChanged(sender, args) {
                    addGrupoContestado(sender._groupName);
                }

                function ValidarContendorPreguntas(sender, args) {
                    var flag = true;
                    var GNoContestadas = new Array();
                    var vContenedor = document.getElementsByClassName("Contenedor");

                    var i = 0;
                    for (i = 0; i < vContenedor.length; i++) {
                        if (a.indexOf(vContenedor[i].control._groupName) == -1) {
                            var GrupoNoContestado = document.getElementById(vContenedor[i].id);
                            GrupoNoContestado.focus();
                            GrupoNoContestado.style.borderWidth = '2px';
                            var flag = false;
                            scrollTo(vContenedor[i].id);
                            break;
                        }
                    }
                    return flag;
                }

                function scrollTo(hash) {
                    location.hash = "#" + hash;
                }


                function ConfirmarEliminarRespuestas(sender, args) {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            this.click();
                        }
                    });
                    radconfirm("Este proceso borrará las respuestas de todas las pruebas de la batería ¿Desea continuar?", callBackFunction, 400, 180, null, "Eliminar respuestas batería");
                    args.set_cancel(true);
                }

                function ConfirmarEliminarPrueba(sender, args) {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            this.click();
                        }
                    });
                    radconfirm("Este proceso borrará las respuestas de la prueba seleccionada ¿Desea continuar?", callBackFunction, 400, 180, null, "Eliminar respuestas prueba");
                    args.set_cancel(true);
                }


        </script>
    </telerik:RadCodeBlock>


    <label class="labelPregunta" style="font-size:21px;">Aptitud mental II</label>
    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

            <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="200">
                        <div style="margin: 10px;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="20%"></td>
                                        <td width="20%"></td>
                                        <td width="20%"></td>
                                        <td width="20%"></td>
                                        <td width="20%"></td>
                                    </tr>
                                </thead>

                                <tbody>                                    
                                    <tr>
                                        <td colspan="4"><label  id="Label1" runat="server">Esta prueba  se  compone de diversas preguntas y problemas que tendrás que resolver. Revisa este ejemplo para  contestar el siguiente cuestionario.</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label  class="labelPregunta" id="Label2" runat="server">EJEMPLO 1</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label3" runat="server">¿Cuál de las cinco palabras nos indica que es una manzana?</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><label id="Label4" runat="server">a)</label><br />
                                            <label id="Label5" runat="server">Flor</label></td>
                                        <td><label id="Label6" runat="server">b)</label><br />
                                            <label id="Label7" runat="server">Árbol</label></td>
                                        <td><label id="Label8" runat="server">c)</label><br />
                                            <label id="Label9" runat="server">Legumbre</label></td>
                                        <td><label id="Label10" runat="server">d)</label><br />
                                            <label id="Label11" runat="server">Fruto</label></td>
                                        <td><label id="Label12" runat="server">e)</label><br />
                                            <label id="Label13" runat="server">Animal</label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label14" runat="server">La respuesta exacta es  "fruto". Por eso se debe seleccionar  la letra D, que es la que va delante de "fruto".</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label15" runat="server">Ud. Deberá responder de esta misma forma, es decir, seleccionando la letra que está delante de la respuesta.</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label  class="labelPregunta"  id="Label16" runat="server">EJEMPLO 2</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label17" runat="server">¿Cuál de éstas cosas es redonda?</label></td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td><label id="Label18" runat="server">a)</label><br />
                                            <label id="Label19" runat="server">Libro</label></td>
                                        <td><label id="Label20" runat="server">b)</label><br />
                                            <label id="Label21" runat="server">Ladrillo</label></td>
                                        <td><label id="Label22" runat="server">c)</label><br />
                                            <label id="Label23" runat="server">Pelota</label></td>
                                        <td><label id="Label24" runat="server">d)</label><br />
                                            <label id="Label25" runat="server">Casa</label></td>
                                        <td><label id="Label26" runat="server">e)</label><br />
                                            <label id="Label27" runat="server">Baúl</label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label28" runat="server">La respuesta correcta es "Pelota"; así pues, la respuesta correcta es "c".</label></td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td colspan="4"><label  class="labelPregunta" id="Label44" runat="server">EJEMPLO 3</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label45" runat="server">¿Cuál de estos números tiene todas sus cifras impares?</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><label id="Label46" runat="server">a)</label><br />
                                            <label id="Label47" runat="server">243</label></td>
                                        <td><label id="Label48" runat="server">b)</label><br />
                                            <label id="Label49" runat="server">9,871</label></td>
                                        <td><label id="Label50" runat="server">c)</label><br />
                                            <label id="Label51" runat="server">6,482</label></td>
                                        <td><label id="Label52" runat="server">d)</label><br />
                                            <label id="Label53" runat="server">3,175</label></td>
                                        <td><label id="Label54" runat="server">e)</label><br />
                                            <label id="Label55" runat="server">19,783</label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label56" runat="server">La respuesta correcta es "d".</label></td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td colspan="4"><label  class="labelPregunta" id="Label29" runat="server">EJEMPLO 4</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label30" runat="server">El precio de una pastilla es de 20 centavos ¿Cuánto costarán seis pastillas?</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><label id="Label31" runat="server">a)</label><br />
                                            <label id="Label32" runat="server">1.80 pesos</label></td>
                                        <td><label id="Label33" runat="server">b)</label><br />
                                            <label id="Label34" runat="server">1.20 pesos</label></td>
                                        <td><label id="Label35" runat="server">c)</label><br />
                                            <label id="Label36" runat="server">0.90 pesos</label></td>
                                        <td><label id="Label37" runat="server">d)</label><br />
                                            <label id="Label38" runat="server">1.30 pesos</label></td>
                                        <td><label id="Label39" runat="server">e)</label><br />
                                            <label id="Label40" runat="server">0.75 pesos</label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"><label id="Label41" runat="server">La solución es de 1 peso 20 centavos, así pues la respuesta correcta  es "b".</label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <br />
                                            <label id="Label42" runat="server">Esta prueba consta de 75 ejercicios, resuelva todos los que pueda.</label>
                                            <br />
                                            <br />
                                            <label id="Label43" runat="server">A partir de la señal dada por el examinador, dispondrás de media hora. Trabaja lo más rápida y exactamente que puedas. No se detenga mucho  en una pregunta. Si llegas a una que no comprendas, pasa a la siguiente.</label>
                                        </td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>

            </telerik:RadPane>

            <telerik:RadPane ID="radPanelPreguntas" runat="server">


                <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                    <table style="width: 100%;">
                        <thead>
                            <tr>
                                <td width="20%"></td>
                                <td width="20%"></td>
                                <td width="20%"></td>
                                <td width="20%"></td>
                                <td width="15%"></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">1. ¿Qué expresa mejor lo que es un martillo?</label></td>

                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                            Text="a) cosa" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                            Text="b) mueble" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                            Text="c) arma" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                            Text="d) herramienta" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta1Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                            Text="e) maquina" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>


                            <%--Pregunta 2--%>


                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">2. ¿Cuál de estas cinco palabras significa lo contrario de GANAR?</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                            Text="a) conseguir" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                            Text="b) decaer" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                            Text="c) perder" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                            Text="d) acceder" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta2Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                            Text="e) ensayar" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <%--Pregunta 3--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">3. La hierba es para la vaca lo que el pan es para…</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                            Text="a) la manteca" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                            Text="b) la harina" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                            Text="c) la leche" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                            Text="d) el hombre" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta3Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                            Text="e) la cosecha" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <%--Pregunta 4--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">4. ¿Qué es para el automóvil lo que el carbón es para la locomotora?</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                            Text="a) el humo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                            Text="b) la motocicleta" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                            Text="c) las ruedas" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                            Text="d) la gasolina" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta4Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                            Text="e) la bocina" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <%--Pregunta 5--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        5. Los números que vienen aquí debajo forman una serie y uno de ellos no es correcto. ¿Qué número debiera figurar en su lugar?          
                                        <br />
                                        5   10   15   20   25   30   35   39   45   50


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                            Text="a) 35" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                            Text="b) 40" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                            Text="c) 42" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                            Text="d) 45" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta5Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                            Text="e) 48" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <%--Pregunta 6--%>


                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        6. La mano es para el brazo lo que el pie es para…
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                            Text="a) la pierna" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                            Text="b) el pulgar" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                            Text="c) el dedo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                            Text="d) el puño" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta6Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                            Text="e) la rodilla" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <%--Pregunta 7--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        7. De un muchacho que no hace más que hablar de sus cualidades y de su sabiduría, se dice que…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                            Text="a) miente" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                            Text="b) bromea" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                            Text="c) engañar" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                            Text="d) se divierte" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta7Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                            Text="e) se alaba" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <%--Pregunta 8--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        8. De una persona que tiene deseos de hacer una cosa pero teme el fracaso, se dice que es…


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                            Text="a) seria" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                            Text="b) ansiosa" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                            Text="c) trabajadora" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                            Text="d) enérgica" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta8Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                            Text="e) tímida" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <%--Pregunta 9--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        9. El sombrero es para la cabeza lo que el dedal es para…
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                            Text="a) el dedo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                            Text="b) la aguja" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                            Text="c) el hilo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                            Text="d) la mano" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta9Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                            Text="e) la costura" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 10--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        10. El hijo de la hermana de mi padre es mi…
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                            Text="a) hermano" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                            Text="b) sobrino" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                            Text="c) primo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                            Text="d) tío" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta10Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                            Text="e) nieto" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 11--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        11. ¿Cuál de estas cantidades es la mayor?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                            Text="a) 6.456" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                            Text="b) 8.968" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                            Text="c) 4.265" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                            Text="d) 5.064" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta11Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                            Text="e) 4.108" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 12--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        12. Cuando sabemos que un acontecimiento va a pasar sin ninguna clase de dudas, decimos que es…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                            Text="a) probable" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                            Text="b) seguro" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                            Text="c) dudoso" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                            Text="d) posible" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta12Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                            Text="e) diferido" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 13--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        13. ¿Qué palabra indica el lado opuesto a ESTE?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                            Text="a) Norte" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                            Text="b) Polo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                            Text="c) Ecuador" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                            Text="d) Sur" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta13Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                            Text="e) Oeste" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 14--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        14. ¿Qué palabra indica lo contrario a SOBERBIA?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                            Text="a) Tristeza" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                            Text="b) Humildad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                            Text="c) Pobreza" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                            Text="d) Variedad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta14Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                            Text="e) Altanería" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 15--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        15. ¿Cuál de estas cinco cosas no debería agruparse a las demás?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                            Text="a) Pera" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                            Text="b) Plátano" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                            Text="c) Naranja" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                            Text="d) Pelota" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta15Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                            Text="e) Higo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 16--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        16. ¿Qué definición de éstas expresa más exactamente lo que es un reloj?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                            Text="a) Una cosa redonda que hace tic tac" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                            Text="b) Un aparato que se coloca en las torres" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                            Text="c) Un instrumento redondo con una cadena" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                            Text="d) Un instrumento que mide el tiempo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta16Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                            Text="e) Una cosa redonda que tiene agua y cristal" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 17--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        17. Si una persona, al salir de su casa, anda siete pasos hacia la derecha y después retrocede cuatro hacia la izquierda, ¿a cuántos pasos está de su casa?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                            Text="a) 7" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                            Text="b) 4" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                            Text="c) 11" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                            Text="d) 3" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta17Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                            Text="e) 10" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 18--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        18. Si comparamos el automóvil a una carreta, ¿a qué deberíamos comparar una motocicleta?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                            Text="a) A la carretera" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                            Text="b) Al caballo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                            Text="c) Al tranvía" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                            Text="d) Al tren" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta18Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                            Text="e) A la bicicleta" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 19--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        19. ¿Cuál es la razón por la cual las fachadas de los comercios están muy iluminadas?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                            Text="a) Con el fin de que los transeúntes sepan dónde están" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                            Text="b) Por que se puedan ver bien los artículos expuestos y la gente sienta deseos de comprar" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta19Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                            Text="c) Porque los comercios pagan muy barata la corriente eléctrica" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta19Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                            Text="d) Para aumentar la iluminación de la calle" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta19Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                            Text="e) Por que el ayuntamiento les obliga" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 20--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        20. ¿Cuál de estas cinco cosas tiene más parecido con manzana, melocotón y pera?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                            Text="a) Semilla" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                            Text="b) Árbol" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta20Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                            Text="c) Ciruela" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta20Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                            Text="d) Jugo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta20Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                            Text="e) Mondadura" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 21--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        21. En el abecedario, ¿qué letra sigue a la K?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta21Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                            Text="a) La J" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta21Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                            Text="b) La G" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta21Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                            Text="c) La M" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta21Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                            Text="d) La L" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta21Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                            Text="e) La N" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 22--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        22. El Rey es a la monarquía lo que el Presidente es a…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta22Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                            Text="a) la Presidencia del Consejo de Ministros" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta22Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                            Text="b) el Senado" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta22Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                            Text="c) la República" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta22Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                            Text="d) un monárquico" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta22Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                            Text="e) un republicano" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 23--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">23. La lana es para el borrego lo que las plumas son para…</label>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta23Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                            Text="a) la almohada" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta23Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                            Text="b) el conejo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta23Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                            Text="c) el pájaro" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta23Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                            Text="d) la cabra" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta23Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                            Text="e) la cama" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 24--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        24. ¿Cuál de estas definiciones expresa más exáctamente lo que es un cordero?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta24Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                            Text="a) Un animal terrestre" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta24Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                            Text="b) Un ser que tiene cuatro patas y una cola" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta24Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                            Text="c) Un animal pequeño y avispado" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta24Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                            Text="d) Un borrego joven" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta24Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                            Text="e) Un animalito que come hierba" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 25--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        25. Pesado es a plomo lo que sonoro es a…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta25Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                            Text="a) suave" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta25Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                            Text="b) pequeño" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta25Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                            Text="c) macizo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta25Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                            Text="d) gris" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta25Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                            Text="e) ruido" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 26--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        26. Mejor es a bueno lo que peor es a…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta26Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                            Text="a) muy bueno" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta26Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                            Text="b) mediano" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta26Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                            Text="c) malo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta26Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                            Text="d) nulo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta26Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                            Text="e) superior" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 27--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        27. ¿Cuál de estas cosas tiene más parecido con tenazas, alambre y clavo?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta27Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                            Text="a) Billete" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta27Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                            Text="b) Hueso" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta27Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                            Text="c) Cuerda" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta27Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                            Text="d) Lápiz" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta27Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                            Text="e) Llave" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 28--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        28. Ante el dolor de los demás normalmente sentimos…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta28Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                            Text="a) rabia" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta28Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                            Text="b) piedad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta28Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                            Text="c) desprecio" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta28Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                            Text="d) desdén" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta28Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                            Text="e) añoranza" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 29--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        29. Cuando alguien concibe una nueva máquina, se dice que ha hecho una…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta29Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                            Text="a) exploración" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta29Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                            Text="b) adaptación" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta29Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                            Text="c) renovación" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta29Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                            Text="d) novedad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta29Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                            Text="e) invención" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 30--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        30. ¿Qué es para la abeja lo que las uñas son para el gato?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta30Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                            Text="a) Vuelo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta30Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                            Text="b) Miel" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta30Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                            Text="c) Alas" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta30Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                            Text="d) Cera" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta30Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                            Text="e) Aguijón" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 31--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        31. Uno de los números de esta serie está equivocado. ¿Qué número debería figurar en su lugar?
                                        <br />
                                        1   7   2   7   3   7   4   7   5   7   6   7   8   7 
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta31Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta31"
                                            Text="a) 7" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta31Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta31"
                                            Text="b) 6" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta31Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta31"
                                            Text="c) 5" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta31Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta31"
                                            Text="d) 8" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta31Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta31"
                                            Text="e) 9" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 32--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        32. ¿Cuál es la principal razón por la que se sustituyeron las carretas por automóviles?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta32Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta32"
                                            Text="a) Los caballos cada día eran mas escasos" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta32Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta32"
                                            Text="b) Los caballos se desbocaban fácilmente" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta32Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta32"
                                            Text="c) Los autos permitían ganar tiempo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta32Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta32"
                                            Text="d) Los autos eran mas económicos que las carretas" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta32Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta32"
                                            Text="e) Las reparaciones de los autos eran más baratas que las carretas" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 11--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        33. La cáscara es para la naranja y la vaina es para el chícharo lo que el cascarón es para…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta33Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta33"
                                            Text="a) la manzana" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta33Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta33"
                                            Text="b) el huevo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta33Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta33"
                                            Text="c) el jugo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta33Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta33"
                                            Text="d) el melocotón" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta33Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta33"
                                            Text="e) la gallina" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 34--%>


                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        34. ¿Qué es para el criminal lo que el hospital es para el enfermo?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta34Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta34"
                                            Text="a) Juez" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta34Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta34"
                                            Text="b) Hospicio" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta34Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta34"
                                            Text="c) Doctor" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta34Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta34"
                                            Text="d) Presidio" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta34Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta34"
                                            Text="e) Sentencia" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 35--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        35. Si estos números estuviesen ordenados, ¿por qué letra empezaría el del centro?
                                        <br />
                                        Ocho   Diez   Seis   Nueve   Siete


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta35Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta35"
                                            Text="a) La S" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta35Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta35"
                                            Text="b) La N" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta35Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta35"
                                            Text="c) La O" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta35Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta35"
                                            Text="d) La D" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta35Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta35"
                                            Text="e) La C" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 36--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        36. A 30 centavos la cartulina, ¿cuántas podrán comprarse por 3 pesos?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta36Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta36"
                                            Text="a) 10" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta36Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta36"
                                            Text="b) 5" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta36Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta36"
                                            Text="c) 8" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta36Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta36"
                                            Text="d) 3" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta36Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta36"
                                            Text="e) 25" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 37--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        37. De una cantidad que disminuye se dice que…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta37Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta37"
                                            Text="a) se va" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta37Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta37"
                                            Text="b) decrece" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta37Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta37"
                                            Text="c) se agota" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta37Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta37"
                                            Text="d) muere" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta37Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta37"
                                            Text="e) desaparece" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 38--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        38. Hay un refrán que dice "No todo lo que brilla es oro" y esto significa:

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta38Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta38"
                                            Text="a) Hay oro que no brilla" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta38Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta38"
                                            Text="b) No hay que dejarse llevar por las apariencias" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta38Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta38"
                                            Text="c) El diamante es más brillante que el oro" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta38Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta38"
                                            Text="d) No hay que llevar bisutería que imite al oro" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta38Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta38"
                                            Text="e) Hay gentes a quienes gusta ostentar sus riquezas" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 39--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        39. En una lengua extranjera KOLO quiere decir "niño" y DAAK KOLO "niño bueno". ¿Por qué letra empieza la palabra que significa "bueno" en ese idioma?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta39Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta39"
                                            Text="a) La D" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta39Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta39"
                                            Text="b) La K" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta39Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta39"
                                            Text="c) La M" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta39Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta39"
                                            Text="d) La S" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta39Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta39"
                                            Text="e) La A" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 40--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        40. Este refrán, "Más vale pájaro en mano que ciento volando", quiere decir:

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta40Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta40"
                                            Text="a) Es preferible poseer una pequeña cosa que esperar una grande" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta40Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta40"
                                            Text="b) El corazón fuerte no se deja rendir por la lisonja" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta40Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta40"
                                            Text="c) Ningún hombre suele apartarse de la verdad sin engañarse a sí mismo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta40Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta40"
                                            Text="d) El que está en todas partes no está en ninguna" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta40Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta40"
                                            Text="e) Cuando se tiene una cosa hay que procurar conservarla" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 41--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        41. ¿Cuál de estas cinco cosas es más completa?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta41Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta41"
                                            Text="a) Retoño" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta41Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta41"
                                            Text="b) Hoja" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta41Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta41"
                                            Text="c) Árbol" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta41Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta41"
                                            Text="d) Rama" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta41Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta41"
                                            Text="e) Tronco" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 42--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        42. Si estas palabras estuviesen convenientemente ordenadas para formar una frase, ¿por qué letra empezaría la tercera palabra?
                                        <br />
                                        CON DIME ERES QUIEN DIRE ANDAS Y TE QUIEN																


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta42Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta42"
                                            Text="a) La D" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta42Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta42"
                                            Text="b) La Q" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta42Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta42"
                                            Text="c) La A" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta42Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta42"
                                            Text="d) La C" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta42Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta42"
                                            Text="e) La Y" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 43--%>
                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        43. Si Jorge es mayor que Pedro, y Pedro es mayor que Juan, entonces Jorge es__________ que Juan.

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta43Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta43"
                                            Text="a) mayor" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta43Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta43"
                                            Text="b) mas pequeño" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta43Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta43"
                                            Text="c) iguales" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta43Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta43"
                                            Text="d) no se puede saber" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                            </tr>


                            <%--Pregunta 44--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        44. ¿Cuál de estas palabras es la primera que aparece en un diccionario?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta44Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta44"
                                            Text="a) Raspador" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta44Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta44"
                                            Text="b) Queso" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta44Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta44"
                                            Text="c) Gruta" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta44Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta44"
                                            Text="d) Noche" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta44Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta44"
                                            Text="e) Pintura" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 45--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        45. Si las palabras General, Teniente, Soldado, Coronel, y Sargento estuviesen colocadas indicando el orden jerárquico que significan, ¿por qué letra empezaría la del centro?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta45Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta45"
                                            Text="a) La G" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta45Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta45"
                                            Text="b) La T" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta45Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta45"
                                            Text="c) La S" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta45Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta45"
                                            Text="d) La C" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta45Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta45"
                                            Text="e) La A" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 46--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        46.Hay un refrán que dice "Un grano no hace granero, pero ayuda al compañero", y esto significa:

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta46Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta46"
                                            Text="a) Resuélvete a hacer lo que debes y haz sin fallar lo que te haya resuelto" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta46Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta46"
                                            Text="b) Hay que ganarse la vida a fuerza de amor" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta46Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta46"
                                            Text="c) No se debe menospreciar las cosas pequeñas" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta46Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta46"
                                            Text="d) En la casa del pobre no es necesario granero" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta46Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta46"
                                            Text="e) Las personas deben ayudarse unas a otras" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 47--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        47. Si Juan es mayor que José, y José tiene la misma edad que Carlos, entonces…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta47Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta47"
                                            Text="a) Carlos es mayor que Juan" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta47Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta47"
                                            Text="b) Juan y Carlos tienen la misma edad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta47Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta47"
                                            Text="c) Carlos es más joven que Juan" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta47Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta47"
                                            Text="d) Juan es menor que Carlos" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta47Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta47"
                                            Text="e) José es el menor de los tres" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 48--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        48. Ordenando la frase que viene aquí debajo, ¿por qué letra empieza la última palabra?<br />
                                        A  FALTA  TORTAS  BUENAS  PAN  SON  DE
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta48Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta48"
                                            Text="a) La T" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta48Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta48"
                                            Text="b) La P" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta48Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta48"
                                            Text="c) La D" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta48Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta48"
                                            Text="d) La B" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta48Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta48"
                                            Text="e) La S" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 49--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        49. Si en una caja grande hubiera dos más pequeñas y dentro de cada una de éstas dos hubiera cinco, ¿cuántas habría en total?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta49Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta49"
                                            Text="a) 10" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta49Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta49"
                                            Text="b) 12" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta49Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta49"
                                            Text="c) 13" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta49Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta49"
                                            Text="d) 15" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta49Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta49"
                                            Text="e) 8" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 50--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        50. ¿Qué indica mejor lo que es una mentira?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta50Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta50"
                                            Text="a) Un error" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta50Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta50"
                                            Text="b) Una afirmación voluntariamente falsa" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta50Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta50"
                                            Text="c) Una afirmación involuntariamente falsa" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta50Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta50"
                                            Text="d) Una exageración" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta50Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta50"
                                            Text="e) Una respuesta inexacta" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 51--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        51. En un idioma extranjero SOTO GRON quiere decir "muy caliente" y PASS GRON "muy frío", ¿por qué letra empieza la palabra que significa "muy" en ese idioma?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta51Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta51"
                                            Text="a) La M" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta51Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta51"
                                            Text="b) La Y" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta51Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta51"
                                            Text="c) La S" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta51Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta51"
                                            Text="d) La G" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta51Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta51"
                                            Text="e) La P" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 52--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        52. La palabra que mejor expresa que una cosa o institución se mantiene a lo largo del tiempo es…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta52Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta52"
                                            Text="a) permanente" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta52Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta52"
                                            Text="b) firme" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta52Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta52"
                                            Text="c) estacionaria" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta52Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta52"
                                            Text="d) sólida" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta52Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta52"
                                            Text="e) verdadera" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 53--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        53. Si Pablo es mayor que Luis y si Pablo es más joven que Andrés, entonces…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta53Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta53"
                                            Text="a) Andrés es mayor que Luis" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta53Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta53"
                                            Text="b) Pablo es el más jóven" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta53Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta53"
                                            Text="c) Andrés y Luis tienen la misma edad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta53Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta53"
                                            Text="d) el mayor de todos es Luis" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta53Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta53"
                                            Text="e) Pablo es mayor que Andrés" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 54--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        54. ¿Cuál de estas cosas tienen más parecido con serpiente, vaca y gorrión?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta54Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta54"
                                            Text="a) Árbol" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta54Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta54"
                                            Text="b) Muñeca" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta54Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta54"
                                            Text="c) Borrego" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta54Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta54"
                                            Text="d) Pluma" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta54Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta54"
                                            Text="e) Piel" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 55--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        55. Hay un refrán que dice: "A hierro caliente, batir de repente", y esto significa:

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta55Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta55"
                                            Text="a) El hierro batido en frío, es malo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta55Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta55"
                                            Text="b) No se pueden hacer varias cosas al mismo tiempo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta55Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta55"
                                            Text="c) Hay que saber aprovechar el momento oportuno" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta55Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta55"
                                            Text="d) Los herreros han de trabajar siempre de prisa" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta55Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta55"
                                            Text="e) El trabajo del hierro es cansado" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 56--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        56. Si las palabras que vienen aquí debajo estuviesen ordenadas, ¿por qué letra empezaría la del centro?
                                        <br />
                                        Semana    Año    Hora    Segundo    Día    Mes    Minuto


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta56Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta56"
                                            Text="a) La S" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta56Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta56"
                                            Text="b) La M" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta56Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta56"
                                            Text="c) La H" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta56Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta56"
                                            Text="d) La D" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta56Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta56"
                                            Text="e) La A" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 57--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        57. El capitán es para el barco lo que el alcalde es para…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta57Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta57"
                                            Text="a) el Estado" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta57Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta57"
                                            Text="b) el país" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta57Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta57"
                                            Text="c) la ciudad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta57Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta57"
                                            Text="d) el patrón" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta57Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta57"
                                            Text="e) el juez" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 58--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        58. Uno de los números de la serie que viene aquí debajo está equivocado. ¿Qué número debiera figurar en su lugar?
                                        <br />
                                        2   3   4   3   2   3   4   3   4


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta58Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta58"
                                            Text="a) 1" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta58Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta58"
                                            Text="b) 2" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta58Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta58"
                                            Text="c) 3" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta58Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta58"
                                            Text="d) 6" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta58Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta58"
                                            Text="e) 5" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 59--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        59. Si un pleito se resuelve por mutuas concesiones, se dice que ha habido…

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta59Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta59"
                                            Text="a) promesa" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta59Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta59"
                                            Text="b) debate" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta59Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta59"
                                            Text="c) amnistía" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta59Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta59"
                                            Text="d) proceso" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta59Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta59"
                                            Text="e) avenencia" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 60--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        60. La oración que viene aquí debajo tiene las palabras desordenadas. ¿Qué letra debe marcarse atendiendo a la frase ordenada? 
                                        <br />
                                        FRASE   LA   LETRA   SEÑALE   PRIMERA   ESTA   DE


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta60Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta60"
                                            Text="a) La E" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta60Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta60"
                                            Text="b) La F" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta60Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta60"
                                            Text="c) La L" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta60Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta60"
                                            Text="d) La S" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta60Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta60"
                                            Text="e) La D" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 61--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        61. En la serie de números, que aparece aquí debajo, cuente todos los 5 que están delante de un 7. ¿Cuántos son? 
                                        <br />
                                        7 5 3 5 7 2 3 7 5 6 7 7 2 5 7 3 4 7 7 5 2 0 7 5 7 8 3 7 2 5 1 7 9 6 5 7


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta61Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                            Text="a) 2" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta61Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta61"
                                            Text="b) 3" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta61Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta61"
                                            Text="c) 4" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta61Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta61"
                                            Text="d) 5" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta61Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta61"
                                            Text="e) 6" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 62--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        62. ¿Qué indica mejor lo que es un termómetro?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta62Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta62"
                                            Text="a) Un tubo de cristal graduado que contiene mercurio" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta62Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta62"
                                            Text="b) Un instrumento para medir la fiebre" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta62Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta62"
                                            Text="c) Un aparato muy sensible al calor" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta62Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta62"
                                            Text="d) Un instrumento para medir la temperatura" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta62Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta62"
                                            Text="e) Un objeto que utilizan los médicos" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 63--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        63. ¿Cuál de estas palabras es la primera que aparece en un diccionario?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta63Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta63"
                                            Text="a) Bravo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta63Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta63"
                                            Text="b) Busto" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta63Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta63"
                                            Text="c) Brocha" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta63Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta63"
                                            Text="d) Bujía" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta63Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta63"
                                            Text="e) Bretón" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 64--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        64. Uno de los números de la serie que aparece aquí debajo está equivocado. ¿Qué número debiera figurar en su lugar?
                                        <br />
                                        1   2   4   8   12   32   64   2   4


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta64Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta64"
                                            Text="a) 10" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta64Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta64"
                                            Text="b) 14" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta64Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta64"
                                            Text="c) 16" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta64Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta64"
                                            Text="d) 24" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta64Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta64"
                                            Text="e) 6" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 65--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        65. ¿Cuál de estas palabras significa lo contrario de COMÚN?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta65Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta65"
                                            Text="a) Banal" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta65Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta65"
                                            Text="b) Vivo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta65Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta65"
                                            Text="c) Difícil" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta65Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta65"
                                            Text="d) Raro" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta65Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta65"
                                            Text="e) Interesante" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 66--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        66. ¿Cuál de estas cinco cosas tiene más parecido con Presidente, Almirante y General?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta66Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta66"
                                            Text="a) Navío" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta66Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta66"
                                            Text="b) Ejército" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta66Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta66"
                                            Text="c) Rey" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta66Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta66"
                                            Text="d) República" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta66Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta66"
                                            Text="e) Soldado" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 67--%>


                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        67. Si las palabras que aparecen aquí debajo estuvieran ordenadas, ¿por qué letra empezaría la del centro?
                                        <br />
                                        Adolescente   Niño   Hombre   Viejo   Bebé


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta67Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta67"
                                            Text="a) La A" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta67Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta67"
                                            Text="b) La V" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta67Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta67"
                                            Text="c) La H" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta67Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta67"
                                            Text="d) La B" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta67Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta67"
                                            Text="e) La N" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 68--%>

                            <tr>
                                <td colspan="5">
                                    <label class="labelPregunta">
                                        68. ¿Cuál de estas definiciones indica más exactamente lo que es un caballo?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta68Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta68"
                                            Text="a) Un animal que tiene cola" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta68Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta68"
                                            Text="b) Un ser viviente" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta68Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta68"
                                            Text="c) Una cosa que trabaja" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta68Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta68"
                                            Text="d) Un animal que tira de los carros" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta68Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta68"
                                            Text="e) Un rumiante" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 69--%>

                            <tr>
                                <td colspan="5">
                                    <label class="labelPregunta">
                                        69. En un idioma extranjero, BECO PRAC quiere decir "un poco de pan", KLUP PRAC "un poco de leche" y BECO OTOH KLUP PRAC "un poco de pan y leche". ¿Por qué letra empieza la palabra que significa Y en dicho idioma?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta69Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta69"
                                            Text="a) La K" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta69Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta69"
                                            Text="b) La P" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta69Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta69"
                                            Text="c) La B" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta69Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta69"
                                            Text="d) La O" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta69Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta69"
                                            Text="e) La Y" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 70--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        70. Si las palabras que aparecen aquí debajo estuviesen convenientemente ordenadas para formar una frase, ¿por qué letra empezaría la tercera palabra? 
                                        <br />
                                        MADRUGA   QUIEN   LE   DIOS   A   AYUDA


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta70Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta70"
                                            Text="a) La A" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta70Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta70"
                                            Text="b) La M" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta70Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta70"
                                            Text="c) La Q" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta70Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta70"
                                            Text="d) La D" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta70Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta70"
                                            Text="e) La L" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 71--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        71. ¿Cuál de estas palabras es la última que aparece en un diccionario?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta71Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta71"
                                            Text="a) Juez" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta71Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta71"
                                            Text="b) Nervio" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta71Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta71"
                                            Text="c) Hora" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta71Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta71"
                                            Text="d) Norte" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta71Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta71"
                                            Text="e) Labio" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 72--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        72. Si se ordena la frase que aparece aquí debajo, ¿qué letra cumple lo que se dice en ella?<br />
                                        EN  LETRA  RECUADRO  A  ESCRIBA  LA  EL
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta72Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta72"
                                            Text="a) La A" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta72Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta72"
                                            Text="b) La E" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta72Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta72"
                                            Text="c) La L" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta72Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta72"
                                            Text="d) La R" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta72Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta72"
                                            Text="e) La B" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 73--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        73. Uno de los números de la serie que aparece aquí debajo está equivocado, ¿qué número debiera figurar en su lugar?
                                        <br />
                                        1   2   5   6   9   10   13   14   16   18


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta73Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta73"
                                            Text="a) 14" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta73Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta73"
                                            Text="b) 17" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta73Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta73"
                                            Text="c) 20" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta73Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta73"
                                            Text="d) 15" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta73Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta73"
                                            Text="e) 16" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 74--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        74. Si un ciclista recorre 250 metros en 25 segundos, ¿cuántos metros recorrerá en un quinto de segundo?

                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta74Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta74"
                                            Text="a) 10" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta74Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta74"
                                            Text="b) 5" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta74Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta74"
                                            Text="c) 2" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta74Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta74"
                                            Text="d) 4" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta74Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta74"
                                            Text="e) 25" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--Pregunta 75--%>

                            <tr>
                                <td colspan="5">
                                    <label  class="labelPregunta">
                                        75. Si la frase que aparece aquí debajo estuviera ordenada, ¿qué número cumple lo que en ella se dice?
                                        <br />
                                        Y   SUMA   CUATRO   ESCRIBA   TRES   LA   UNO   DE


                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta75Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta75"
                                            Text="a) 3" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta75Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta75"
                                            Text="b) 4" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta75Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta75"
                                            Text="c) 1" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta75Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta75"
                                            Text="d) 7" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnPregunta75Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta75"
                                            Text="e) 8" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <%--End Test--%>
                        </tbody>
                    </table>
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
            <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCorregir" runat="server" Visible="false" OnClick="btnCorregir_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton Visible="false" ID="btnImpresionPrueba" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false"></telerik:RadButton>   
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminarPrueba" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminarBateria" runat="server" Text="Eliminar batería" AutoPostBack="true" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminarBateria_Click"></telerik:RadButton>
         </div>
    </div>
<%--      <div class="DivBtnTerminarDerecha">
       
    </div>--%>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
