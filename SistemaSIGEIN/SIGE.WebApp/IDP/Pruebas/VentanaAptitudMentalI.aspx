<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaAptitudMentalI.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAptitudMental1" %>

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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"  OnAjaxRequest="RadAjaxManager1_AjaxRequest" ClientEvents-OnResponseEnd="retorno">
        <AjaxSettings>
          <%--  <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="time" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnTerminar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnSiguiente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnSiguiente" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="time">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="time" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
                   <telerik:AjaxSetting AjaxControlID="tbActitudMentalI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbActitudMentalI"   LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="mpgActitudMentalI"   LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="texto"   LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>                    
                     </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mpgActitudMentalI" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="time" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ContentTime" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="btnSiguiente" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            var vPruebaEstatus = "";
            var c = "";
            var a = new Array();
            window.onload = function (sender, args) {

                //alert("Inicio");

                var multiPage = $find("<%=mpgActitudMentalI.ClientID %>");
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var segundos = "";
                                segundos = setInitTime(multiPage.get_selectedIndex() + "");
                                if (segundos <= 0) {
                                    var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido.<br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
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
                            //window.close();
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                        var text = "";
                        var multiPage = $find("<%=mpgActitudMentalI.ClientID %>");
                        text = prueba(multiPage.get_selectedIndex());
                        radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Aptitud mental I");
                }
                else {
                    llenar_GruposContestados();
                    prueba(multiPage.get_selectedIndex());
                }
            };

            function llamaAjaxRequest() {
               clearInterval(c); //Se agrega para detener el tiempo del reloj antes de guardar resultados 12/04/2018
                var ajaxManager = $find('<%= RadAjaxManager1.ClientID %>');
                if (ajaxManager) {
                    ajaxManager.ajaxRequest();
                }
            }

            var actualizarTiempo = function () {
                var multiPage = $find("<%=mpgActitudMentalI.ClientID %>");
                var seccion = multiPage.get_selectedIndex();
                updateTimer(seccion.toString(), "ActualizarTiempo");
                //alert("actualízate!");
            }


            function retorno(sender, args) {
                var text = "";
                var multiPage = $find("<%=mpgActitudMentalI.ClientID %>");
                text = prueba(multiPage.get_selectedIndex());
                var vBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        actualizarTiempo();
                    }
                    else
                    {           
                        window.location = "Default.aspx?ty=Ini";
                    }
                });

                var seccion = multiPage.get_selectedIndex();
                console.info(seccion);
                clearInterval(c);
                if (seccion == -1) {
                    mensajePruebaTerminada2();
                }
                else {
                    var vMOD = "<%= vMOD %>";
                    if (vMOD != "REV" && vMOD != "EDIT") {
                    radconfirm(JustificarTexto(text), vBackFunction, 950, 600, null, "Aptitud mental I");
                   }
                }
                //radconfirm("En el retorno del ajax request", actualizarTiempo, 400, 300, null, "Aptitud mental I");                
            }

            function close_window(sender, args) {
                if (vPruebaEstatus != "Terminado") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            //if (ValidarContendorPreguntas()) {
                                a = [];
                                var btn = $find("<%=btnSiguiente.ClientID%>");
                                        btn.click();
                                  //  }
                            }
                        });

                        var text = "¿Estás seguro que deseas terminar tu prueba?";
                        radconfirm(text, callBackFunction, 400, 160, null, "");
                        args.set_cancel(true);
                    }
                else {
                    var multiPage = $find("<%=mpgActitudMentalI.ClientID %>");
                    updateTimer(multiPage.get_selectedIndex() + 1,"close_window");
                    }
                }

                function WinClose(sender, args) {
                    vPruebaEstatus = "Terminado";
                    var btn = $find("<%=btnSiguiente.ClientID%>");
                        btn.click();
            }

            function mensajePruebaTerminada() {
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function mensajePruebaTerminada2() {
                //var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Cuando esté listo para pasar a la siguiente prueba, por favor haga clic en el botón 'Siguiente' más abajo <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                var text = "¿Estás seguro que deseas terminar tu prueba?";
                radconfirm(text, CloseTest, 400, 160, null, "");
                
            }

            function CloseTest() {
                window.location = "Default.aspx?ty=sig";
            }

            function updateTimer(seccion, sender) {
                var multiPage = $find("<%=mpgActitudMentalI.ClientID %>");
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var position = seccion;
                    //clearInterval(c); //Se quita de aqui y se mueve a llamaAjaxRequest function

                    //console.info(sender);
                   // console.info(seccion);
                    //console.info(position);

                    switch (position) {
                        case "1":
                            var segundos = '<%=this.vSeccionBtime%>';
                            console.info(segundos);
                            prueba(seccion);
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                //multiPage.set_selectedIndex(parseInt(position));
                                console.info("Seccion 1");
                                SetScroll(seccion);

                            }
                            break;
                        case "2":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionCtime%>';
                            console.info(segundos);
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                //multiPage.set_selectedIndex(parseInt(position));
                                console.info("Seccion 2");
                                SetScroll(seccion);
                            }

                            break;
                        case "3":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionDtime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                prueba();
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                                SetScroll(seccion);
                            }
                            break;
                        case "4":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionEtime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                                SetScroll(seccion);
                            }
                            break;
                        case "5":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionFtime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                                SetScroll(seccion);
                            }
                            break;
                        case "6":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionGtime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                                SetScroll(seccion);
                            }
                            break;
                        case "7":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionHtime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                                SetScroll(seccion);
                            }
                            break;
                        case "8":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionItime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                                SetScroll(seccion);
                            }
                            break;
                        case "9":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionJtime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                                SetScroll(seccion);
                            }
                            break;
                        default: break;
                    }
                }
                else {
                    prueba(parseInt(seccion));
                    multiPage.set_selectedIndex(parseInt(seccion));
                }
            }

            function setInitTime(segmento) {
                var seconds = "";

                switch (segmento) {
                    case "0": seconds = parseInt('<%=this.vSeccionAtime%>'); break;
                    case "1": seconds = parseInt('<%=this.vSeccionBtime%>'); break;
                    case "2": seconds = parseInt('<%=this.vSeccionCtime%>'); break;
                    case "3": seconds = parseInt('<%=this.vSeccionDtime%>'); break;
                    case "4": seconds = parseInt('<%=this.vSeccionEtime%>'); break;
                    case "5": seconds = parseInt('<%=this.vSeccionFtime%>'); break;
                    case "6": seconds = parseInt('<%=this.vSeccionGtime%>'); break;
                    case "7": seconds = parseInt('<%=this.vSeccionHtime%>'); break;
                    case "8": seconds = parseInt('<%=this.vSeccionItime%>'); break;
                    case "9": seconds = parseInt('<%=this.vSeccionJtime%>'); break;
                    default: break;
                }
                return seconds;
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

                            vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=APTITUD1";
                            var win = window.open(vURL, '_blank');
                            win.focus();
                            //var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
                        }

            function prueba(posicion) {

                var pos = parseInt(posicion);
                var div = document.getElementById("<%= texto.ClientID %>");
                console.info(div);
                switch (pos) {
                    case 1:
                        div.innerHTML = '<p style="margin: 10px;"><label><b>SERIE II</b><br /><br />' +
                        '<b>Instrucciones:</b><br/>Lee cada cuestión y anota la letra correspondiente a la mejor respuesta tal como lo muestra el ejemplo. <br /><br />' +
                        '<b>Ejemplo:</b><br />' +
                        '¿Por qué compramos relojes? Porque:<br />' +
                        '( ) Nos gusta oirlos sonar<br />' +
                        '( ) Tiene manecillas<br />' +
                        '(•) Nos indican las horas' +
                        '</label></p>';
                        break;
                    case 2:
                        div.innerHTML = ' <p style="margin: 10px;"><label><b>SERIE III</b><br /><br />' +
                        '<b>Instrucciones:</b><br/> Cuando dos palabras signifiquen lo mismo, selecciona la opción &quot;I&quot; de igual. Cuando signifiquen lo opuesto selecciona la letra &quot;O&quot;' +
                        '<br /><br />' +
                        '<b>Ejemplo:</b><br />' +
                        'Tirar-Arrojar <br />' +
                        '(•) I ( ) O <br />' +
                        ' Norte-Sur <br />' +
                        '( ) I (•) O ' +
                        '</label></p>';
                        break;
                    case 3:
                        div.innerHTML = ' <p style="margin: 10px;"><label><b>SERIE IV</b><br /><br />' +
                        '<b>Instrucciones:</b><br/>Anote en la hoja de respuestas las letras correspondientes a las DOS palabras que indican algo que SIEMPRE tiene el sujeto.' +
                        ' ANOTA SOLAMENTE DOS para cada renglón<br />' +
                        '<br />' +
                        '<b>Ejemplo:</b><br />' +
                        'Un hombre tiene siempre:<br />' +
                        '[•] Cuerpo [ ] Gorra [ ] Guantes [•] Boca [ ] Dinero' +
                        '</label></p>';
                        break;
                    case 4:
                        div.innerHTML = '<p style="margin: 10px;"> <label><b>SERIE V</b><br /><br />' +
                        '<b>Instrucciones:</b><br/>Encuentra las respuestas lo más pronto posible y escríbelas en el espacio para captura. <br/> Puedes utilizar una hoja en blanco para hacer las operaciones.</label></p>';
                        break;
                    case 5:
                        div.innerHTML =
                        ' <p style="margin: 10px;"><label><b>SERIE VI</b><br /><br />' +
                        '<b>Instrucciones:</b><br/>Señala &quot;Si&quot; o &quot; No&quot; para cada caso.<br />' +
                        '<br />' +
                        '<b>Ejemplo:</b><br />' +
                        '¿Se hace el carbón de madera? &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;(•) Sí  ( ) No <br /><br />' +
                        '¿Tienen todos los hombres 1.70 de altura? ( ) Sí  (•) No<br />' +
                        '</label></p>';
                        break;
                    case 6:
                        div.innerHTML =
                        '    <p style="margin: 10px;"><label><b>SERIE VII</b><br /><br />' +
                        '<b>Instrucciones:</b><br/>Selecciona la letra correspondiente a la palabra que complete correctamente la oración' +
                        '<br />' +
                        '<br /><b>Ejemplo:</b><br />' +
                        'El OIDO es a OIR como el OJO es a:<br />' +
                        '( ) Mesa (•) Ver ( ) Mano ( ) Jugar<br /><br />' +
                        'El SOMBRERO es a la CABEZA como el ZAPATO es a:<br />' +
                        '( ) Brazo ( ) Abrigo (•) Pie ( ) Pierna  </label></p>';
                        break;
                    case 7:
                        div.innerHTML =
                        '<p style="margin: 10px;"><label><b>SERIE VIII</b><br /><br />' +
                        '<b>Instrucciones:</b><br/>Las palabras de cada una de las oraciones siguientes están mezcladas. Ordena cada una de las oraciones. Si el significado de la oración es VERDADERO selecciona la opcion &quot;V&quot;; si el significado de la oración es FALSO, selecciona la opción &quot;F&quot;. <br />' +
                        '<br />' +
                        '<b>Ejemplo:</b>' +
                        '<table style="margin: 10px;"><tr><td></td><td>V</td><td>F</td></tr><tr><td>oir son los para oidos</td><td>(•)</td><td>( )</td></tr><tr><td>comer para pólvora la buena es</td><td>( )</td><td>(•)</td></tr></table> <br/>' +
                        '</label></p>';
                        break;
                    case 8:
                        div.innerHTML =
                                         ' <p style="margin: 10px;"><label><b>SERIE IX</b><br /><br />' +
                        '<b>Instrucciones:</b><br/>Selecciona la opción con la palabra que no corresponde con las demás del renglón.<br />' +
                        '<br /><b>Ejemplo:</b><br />' +
                        '( ) Bala ( ) Cañón ( ) Pistola ( ) Espada (•) Lápiz<br />' +
                        '( ) Canadá (•) Sonora ( ) China ( ) India ( ) Francia<br /></label></p>';
                        break;
                    case 9:
                        div.innerHTML =
                        '<p style="margin: 10px;"><label><b>SERIE X</b><br /><br />' +
                        '<b>Instrucciones:</b><br/>En cada renglón procura encontrar cómo están hechas las series; después, escribe en los espacios en blanco los DOS números que deban seguir en cada serie<br />' +
                        '<br /><b>Ejemplo:</b><br />' +
                         '<b>5 10 15 20 </b> 25 30 <br />' +
                          '<b>20 18 16 14 12</b> 10 8 <br /> </label></p>';

                        var btn = $find("<%=btnSiguiente.ClientID %>");
                            btn.set_text("Guardar");
                            break;
                        default:
                            break;
                    }

                    return div.innerHTML;

                }

                function ValidarCambiosCampos(sender, args) {
                    var vId = sender.get_id();
                    var vClassName = sender._cssClass;
                    var x = document.getElementsByClassName(vClassName);
                    //console.info(sender);
                   
                    var contador = 0;
                    var i = 0;

                    for (i = 0; i < x.length; i++) {
                        if (x[i].control._checked == true) {
                            contador++;
                            if (contador > 2) {

                                var btn = document.getElementById(vId);
                                btn.control.set_checked(false);
                            }
                            else if(contador == 2)
                            {
                                addGrupoContestado(sender._groupName);
                            }
                        }

                    }
                }

                function addGrupoContestado(valor) {
                    if (a.indexOf(valor) == -1 || a.length == 0) {
                        a.push(valor);
                    }
                }

                function valueChanged(sender, args) {
                    addGrupoContestado(sender._groupName);
                }

                function ValidarContendorPreguntas(sender, args) {
                    var flag = true;
                    var GNoContestadas = new Array();
                    var vPosSeccion = $find("<%=mpgActitudMentalI.ClientID %>").get_selectedIndex();
                    var vContenedor = document.getElementsByClassName("Contenedor" + (vPosSeccion+1));
                    var vcssclass = "Contenedor" + (vPosSeccion + 1);
                    if (vcssclass == "Contenedor4") {
                        var i = 0;
                        for (i = 0; i < vContenedor.length; i++) {
                            if (a.indexOf(vContenedor[i].control._groupName) == -1) {
                                var GrupoNoContestado = document.getElementById(vContenedor[i].id);
                                GrupoNoContestado.focus();
                                GrupoNoContestado.style.borderWidth = '1px';
                                var flag = false;
                                scrollTo(vContenedor[i].id);
                                break;
                            }
                        }
                    }

                    else if (vcssclass == "Contenedor5" || vcssclass == "Contenedor10") {
                        var i = 0;
                        for (i = 0; i < vContenedor.length; i++) {
                            if (vContenedor[i].value == "") {
                                var GrupoNoContestado = document.getElementById(vContenedor[i].id);
                                GrupoNoContestado.focus();
                                GrupoNoContestado.style.borderWidth = '1px';
                                var flag = false;
                                scrollTo(vContenedor[i].id);
                                break;
                            }
                        }
                    }
                    else {
                        var i = 0;
                        for (i = 0; i < vContenedor.length; i++) {
                            if (a.indexOf(vContenedor[i].control._groupName) == -1) {
                                var GrupoNoContestado = document.getElementById(vContenedor[i].id);
                                GrupoNoContestado.focus();
                                GrupoNoContestado.style.borderWidth = '1px';
                                var flag = false;
                                scrollTo(vContenedor[i].id);
                                break;
                            }
                        }
                        if (flag == true) { SetScroll(vPosSeccion); }
                    }
                    return flag;
                }

                function scrollTo(hash) {
                    location.hash = "#" + hash;
                }

                function SetScroll(pos) {
                    var lblAnclar = null;
                    switch (pos + "") {
                        case "0":
                            lblAnclar = document.getElementById("<%= lblSeccion1.ClientID %>");
                            scrollTo(lblAnclar.id);
                            break;
                        case "1":
                            lblAnclar = document.getElementById("<%= lblSeccion2.ClientID %>");
                            scrollTo(lblAnclar.id);
                            lblAnclar.focus();
                            break;
                        case "2":
                            lblAnclar = document.getElementById("<%= lblSeccion3.ClientID %>");
                            scrollTo(lblAnclar.id); break;
                            lblAnclar.focus();
                        case "3":
                            lblAnclar = document.getElementById("<%= lblSeccion4.ClientID %>");
                            scrollTo(lblAnclar.id); break;
                        case "4":
                            lblAnclar = document.getElementById("<%= lblSeccion5.ClientID %>");
                              scrollTo(lblAnclar.id);
                              break;
                          case "5":
                              lblAnclar = document.getElementById("<%= lblSeccion6.ClientID %>");
                            scrollTo(lblAnclar.id);
                            break;
                        case "6":
                            lblAnclar = document.getElementById("<%= lblSeccion7.ClientID %>");
                            scrollTo(lblAnclar.id); break;
                        case "7":
                            lblAnclar = document.getElementById("<%= lblSeccion8.ClientID %>");
                            scrollTo(lblAnclar.id); break;
                        case "8":
                            lblAnclar = document.getElementById("<%= lblSeccion9.ClientID %>");
                            scrollTo(lblAnclar.id); break;
                        case "9":
                            lblAnclar = document.getElementById("<%= lblSeccion10.ClientID %>");
                            scrollTo(lblAnclar.id); break;
                        default: break;
                    }
                }

            function llenar_GruposContestados() {
                a = [];
                var vPosSeccion = $find("<%=mpgActitudMentalI.ClientID %>").get_selectedIndex();
                var vContenedor = document.getElementsByClassName("Contenedor" + (vPosSeccion + 1));
                for (i = 0; i < vContenedor.length; i++) {
                    if (vContenedor[i].control._checked == true) {
                        addGrupoContestado(vContenedor[i].control._groupName);
                    }
                }
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
    <label class="labelPregunta" style="font-size:21px;">Aptitud mental I</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
            <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="200">
                        <div id="texto" runat="server">
                               <p style="margin: 10px; text-align:justify;">
                                <label runat="server"> <b>SERIE I</b> </label>
                                <br />
                                <br />
                                <label><b>Instrucciones:</b></label>
                                <br />
                                <label id="Label18" runat="server">Selecciona la opción correspondiente a la palabra que complete correctamente la oración</label><br /><br />
                                <label id="Label30" runat="server"><b>Ejemplo:</b></label><br />
                                <label id="Label50" runat="server">El iniciador de nuestra guerra de independencia fue:</label><br />
                                <label id="Label70" runat="server">( ) Morelos ( ) Zaragoza ( ) Iturbide (•) Hidalgo</label>																																			
                            </p>
                        </div>
                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>

            </telerik:RadPane>

            <telerik:RadPane ID="radPanelPreguntas" runat="server">

                <telerik:RadTabStrip ID="tbActitudMentalI" AutoPostBack="true" runat="server" SelectedIndex="0" MultiPageID="mpgActitudMentalI" OnTabClick="tbMentalISecciones_TabClick">
                    <Tabs>
                        <telerik:RadTab Text="1" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="2" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="3" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="4" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="5" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="6" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="7" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="8" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="9" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="10" Visible="false"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <div style="height: 5px"></div>
                <telerik:RadMultiPage ID="mpgActitudMentalI" runat="server" SelectedIndex="0" Height="100%" AutoPostBack="false" >
                    <telerik:RadPageView ID="RPView1" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td colspan="4" name="" id="">
                                            <label class="labelPregunta"  id="lblSeccion1" runat="server">1. La gasolina se extrae de:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta1"
                                                    Text="Granos" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta1"
                                                    Text="Petróleo" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta1"
                                                    Text="Trementina" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta1"
                                                    Text="Semillas" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >2. Una tonelada tiene:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta2"
                                                    Text="1,000 kilogramos" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta2"
                                                    Text="2,000 kilogramos" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta2"
                                                    Text="3,000 kilogramos" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta2"
                                                    Text="4,000 kilogramos" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >3. La mayoría de nuestras exportaciones salen de:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta3"
                                                    Text="Mazatlán" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta3"
                                                    Text="Acapulco" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta3"
                                                    Text="Progreso" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta3"
                                                    Text="Veracruz" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >4. El nervio óptico sirve para:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta4"
                                                    Text="Ver" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta4"
                                                    Text="Oír" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta4"
                                                    Text="Probar" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta4"
                                                    Text="Sentir" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >5. El café es una especie de:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta5"
                                                    Text="Corteza" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta5"
                                                    Text="Fruto" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta5"
                                                    Text="Hojas" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta5"
                                                    Text="Raíz" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >6. El jamón es carne de:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta6"
                                                    Text="Carnero" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta6"
                                                    Text="Vaca" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta6"
                                                    Text="Gallina" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta6"
                                                    Text="Cerdo" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >7. La laringe esta en:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta7"
                                                    Text="Abdomen" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta7"
                                                    Text="Cabeza" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta7"
                                                    Text="Garganta" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta7"
                                                    Text="Espalda" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >8. La guillotina causa:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta8"
                                                    Text="Muerte" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta8"
                                                    Text="Enfermedad" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta8"
                                                    Text="Fiebre" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta8"
                                                    Text="Malestar" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >9. La grúa se usa para:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta9"
                                                    Text="Perforar" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta9"
                                                    Text="Cortar" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta9"
                                                    Text="Levantar" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta9"
                                                    Text="Exprimir" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >10. Una figura de seis lados se llama:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta10"
                                                    Text="Pentágono" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta10"
                                                    Text="Paralelogramo" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta10"
                                                    Text="Hexágono" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta10"
                                                    Text="Trapecio" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >11. El kilowatt mide:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta11"
                                                    Text="Lluvia" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta11"
                                                    Text="Viento" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta11"
                                                    Text="Electricidad" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta11"
                                                    Text="Presión" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >12. La pauta se usa en:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta12"
                                                    Text="Agricultura" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta12"
                                                    Text="Música" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta12"
                                                    Text="Fotografía" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta12"
                                                    Text="Estenografía" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >13. Las esmeraldas son:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta13"
                                                    Text="Azules" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta13"
                                                    Text="Verdes" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta13"
                                                    Text="Rojas" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta13"
                                                    Text="Amarillas" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >14. El metro es aproxímadamente igual a:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta14"
                                                    Text="Pie" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta14"
                                                    Text="Pulgada" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta14"
                                                    Text="Yarda" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta14"
                                                    Text="Milla" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >15. Las esponjas se obtienen de:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta15"
                                                    Text="Animales" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta15"
                                                    Text="Yerbas" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta15"
                                                    Text="Bosques" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta15"
                                                    Text="Minas" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >16. Fraude es un término usado en:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta16"
                                                    Text="Medicina" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta16"
                                                    Text="Teología" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta16"
                                                    Text="Leyes" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta16"
                                                    Text="Pedagogía" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView2" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td colspan="4">
                                            <label class="labelPregunta"  id="lblSeccion2" runat="server">1. Si la tierra estuviera más cerca del sol:</label></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta1"
                                                    Text="Las estrellas desaparecerían"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta1"
                                                    Text="Los meses serían más largos"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta1"
                                                    Text="La tierra estaría más caliente"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >2. Los rayos de una rueda están frecuentemente hechos de nogal porque:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta2"
                                                    Text="El nogal es fuerte"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta2"
                                                    Text="Se corta fácilmente"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta2"
                                                    Text="Coge bien la pintura"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >3. Un tren se detiene con más dificultad que un automóvil porque:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta3Resp" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta3"
                                                    Text="Tiene más ruedas"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta3"
                                                    Text="Es más pesado"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta3"
                                                    Text="Sus frenos no son buenos"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >4. El dicho &quot;A golpecitos se derriba un roble&quot; quiere decir:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta4"
                                                    Text="Que los robles son más débiles"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta4"
                                                    Text="Que son mejores los golpes pequeños"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta4"
                                                    Text="Que el esfuerzo constante logra resultados sorprendentes"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >5. El dicho &quot;Una olla vigilada nunca hierve&quot; quiere decir:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta5"
                                                    Text="Que no debemos vigilarla cuando está en el fuego"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta5"
                                                    Text="Que tarda en hervir"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta5"
                                                    Text="Que el tiempo se alarga cuando esperamos algo"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >6. El dicho &quot;Siembra pasto mientras haya sol&quot; quiere decir:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta6"
                                                    Text="Que el pasto se siembra en verano"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta6"
                                                    Text="Que debemos aprovechar nuestras oportunidades"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta6"
                                                    Text="Que el pasto no debe cortarse en la noche"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >7. El dicho &quot;Zapatero a tus zapatos&quot; quiere decir:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta7"
                                                    Text="Que un zapatero no debe abandonar sus zapatos"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta7"
                                                    Text="Que los zapateros no deben estar ociosos"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta7"
                                                    Text="Que debemos trabajar en lo que podemos hacer mejor"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >8. El dicho &quot;La cuña para que apriete tiene que ser del mismo palo&quot; quiere decir:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta8"
                                                    Text="Que el palo sirve para apretar"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta8"
                                                    Text="Que las cuñas siempre son de madera"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta8"
                                                    Text="Que exigen más las personas que nos conocen"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >9. Un acorazado de acero flota porque:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta9"
                                                    Text="La máquina lo hace flotar"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta9"
                                                    Text="Porque tiene grandes espacios huecos"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta9"
                                                    Text="Contiene algo de madera"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >10. Las plumas de las alas ayudan al pájaro a volar porque:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta10"
                                                    Text="Las alas ofrecen una amplia superficie ligera"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta10"
                                                    Text="Mantienen el aire fuera del cuerpo"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta10"
                                                    Text="Disminuye su peso"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
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
                                        <td colspan="4">
                                            <label class="labelPregunta" >11. El dicho &quot;Una golondrina no hace verano&quot quiere decir:</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta11"
                                                    Text="Que las golondrinas regresan"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta11"
                                                    Text="Que un simple dato no es suficiente"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta11"
                                                    Text="Que los pájaros se agregan a nuestros placeres del verano"  Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>


                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView3" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;" id="lblSeccion3" runat="server">1. Salado - dulce</label></div>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta1"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta1"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">2. Alegrarse - regocijarse</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta2"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta2"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">3. Mayor - menor</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta3"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta3"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">4. Sentarse - pararse</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta4"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta4"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">5. Desperdiciar - aprovechar</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta5"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta5"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">6. Conceder - negar</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta6"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>



                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta6"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">7. Tónico - estimulante</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta7"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta7"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">8. Rebajar - denigrar</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta8"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta8"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">9. Prohibir - permitir</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta9"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta9"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">10. Osado - audaz</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta10"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta10"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">11. Arrebatado - prudente</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta11"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta11"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">12. Obtuso - agudo</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta12"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta12"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">13. Inepto - experto</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta13"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta13"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">14. Esquivar - rehuir</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta14"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta14"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">15. Rebelarse - someterse</label>
                                            </div>



                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta15"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>



                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta15"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 16 Observacion BRINCO--%>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">16. Monotonía - variedad</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta16"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta16"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">17. Confortar - consolar</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta17"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta17"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">18. Expeler - retener</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta18"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta18"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">19. Dócil - sumiso</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta19"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta19"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">20. Transitorio - permanente</label>
                                            </div>



                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta20"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta20"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">21. Seguridad - riesgo</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta21Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta21"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta21Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta21"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">22. Aprobar - objetar</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta22Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta22"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta22Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta22"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">23. Expeler - arrojar</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta23Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta23"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta23Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta23"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">24. Engaño - impostura</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta24Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta24"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta24Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta24"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">25. Mitigar - apaciguar</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta25Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta25"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta25Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta25"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">26. Incitar - aplacar</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta26Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta26"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta26Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta26"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">27. Reverencia - veneración</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta27Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta27"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta27Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta27"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">28. Sobriedad - frugalidad</label>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta28Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta28"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta28Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta28"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">29. Aumentar - menguar</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta29Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta29"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta29Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta29"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
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
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  style="width: 180px;">30. Incitar - instigar</label>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta30Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta30"
                                                    Text="I" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta30Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta30"
                                                    Text="O" Skin="Metro"  OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView4" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="lblSeccion4" runat="server" style="font-weight: bold;">1. Un CÍRCULO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta1 Contenedor4" ID="DPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" Skin="Metro"
                                                    Text="Altura" OnClientCheckedChanged="ValidarCambiosCampos"  GroupName="RbtnDPregunta1" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>

                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta1 Contenedor4" ID="DPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent"  GroupName="RbtnDPregunta1"
                                                    Text="Circunferencia" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta1 Contenedor4" ID="DPregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent"  GroupName="RbtnDPregunta1"
                                                    Text="Latitud" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>

                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta1 Contenedor4" ID="DPregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent"  GroupName="RbtnDPregunta1"
                                                    Text="Longitud" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta1 Contenedor4" ID="DPregunta1Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta1"
                                                    Text="Radio" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>



                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label1" runat="server" style="font-weight: bold;">2. Un PÁJARO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta2 Contenedor4" ID="DPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                                    Text="Huesos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta2 Contenedor4" ID="DPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                                    Text="Huevos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta2 Contenedor4" ID="DPregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                                    Text="Pico" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta2 Contenedor4" ID="DPregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                                    Text="Nido" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta2 Contenedor4" ID="DPregunta2Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                                    Text="Canto" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label2" runat="server" style="font-weight: bold;">3. La MÚSICA tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta3 Contenedor4" ID="DPregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                                    Text="Oyente" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta3 Contenedor4" ID="DPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                                    Text="Piano" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta3 Contenedor4" ID="DPregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                                    Text="Ritmo" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta3 Contenedor4" ID="DPregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                                    Text="Sonido" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta3 Contenedor4" ID="DPregunta3Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                                    Text="Violín" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label3" runat="server" style="font-weight: bold;">4. Un BANQUETE tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta4 Contenedor4" ID="DPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                                    Text="Alimentos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta4 Contenedor4" ID="DPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                                    Text="Música" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta4 Contenedor4" ID="DPregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                                    Text="Personas" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta4 Contenedor4" ID="DPregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                                    Text="Discursos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta4 Contenedor4" ID="DPregunta4Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                                    Text="Anfitrión" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label4" runat="server" style="font-weight: bold;">5. Un CABALLO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta5 Contenedor4" ID="DPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                                    Text="Arnés" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta5 Contenedor4" ID="DPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                                    Text="Cascos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta5 Contenedor4" ID="DPregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                                    Text="Herraduras" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta5 Contenedor4" ID="DPregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                                    Text="Establo" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta5 Contenedor4" ID="DPregunta5Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                                    Text="Cola" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label5" runat="server" style="font-weight: bold;">6. Un JUEGO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta6 Contenedor4" ID="DPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                                    Text="Cartas" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta6 Contenedor4" ID="DPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                                    Text="Multas" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta6 Contenedor4" ID="DPregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                                    Text="Jugadores" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta6 Contenedor4" ID="DPregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                                    Text="Castigos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta6 Contenedor4" ID="DPregunta6Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                                    Text="Reglas" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label6" runat="server" style="font-weight: bold;">7. Un OBJETO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta7  Contenedor4" ID="DPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                                    Text="Calor" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta7 Contenedor4" ID="DPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                                    Text="Tamaño" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta7 Contenedor4" ID="DPregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                                    Text="Sabor" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta7 Contenedor4" ID="DPregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                                    Text="Valor" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta7 Contenedor4" ID="DPregunta7Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                                    Text="Peso" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label7" runat="server" style="font-weight: bold;">8. Una CONVERSACIÓN tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta8 Contenedor4" ID="DPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                                    Text="Acuerdos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta8 Contenedor4" ID="DPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                                    Text="Personas" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta8 Contenedor4" ID="DPregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                                    Text="Preguntas" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta8 Contenedor4" ID="DPregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                                    Text="Ingenio" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta8 Contenedor4" ID="DPregunta8Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                                    Text="Palabras" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label8" runat="server" style="font-weight: bold;">9. Una DEUDA implica siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta9 Contenedor4" ID="DPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                                    Text="Acreedor" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta9 Contenedor4" ID="DPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                                    Text="Deudor" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta9 Contenedor4" ID="DPregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                                    Text="Interés" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta9 Contenedor4" ID="DPregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                                    Text="Hipoteca" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta9 Contenedor4" ID="DPregunta9Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                                    Text="Pago" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label9" runat="server" style="font-weight: bold;">10. Un CIUDADANO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta10 Contenedor4" ID="DPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                                    Text="País" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta10 Contenedor4" ID="DPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                                    Text="Ocupación" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta10 Contenedor4" ID="DPregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                                    Text="Derechos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta10 Contenedor4" ID="DPregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                                    Text="Propiedad" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta10 Contenedor4" ID="DPregunta10Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                                    Text="Voto" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label10" runat="server" style="font-weight: bold;">11. Un MUSEO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta11 Contenedor4" ID="DPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                                    Text="Animales" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta11 Contenedor4" ID="DPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                                    Text="Orden" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta11 Contenedor4" ID="DPregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                                    Text="Colecciones" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta11 Contenedor4" ID="DPregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                                    Text="Minerales" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta11 Contenedor4" ID="DPregunta11Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                                    Text="Visitantes" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 12--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label11" runat="server" style="font-weight: bold;">12. Un COMPROMISO implica siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta12 Contenedor4" ID="DPregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                                    Text="Obligación" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta12 Contenedor4" ID="DPregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                                    Text="Acuerdo" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta12 Contenedor4" ID="DPregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                                    Text="Amistad" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta12 Contenedor4" ID="DPregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                                    Text="Respeto" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta12 Contenedor4" ID="DPregunta12Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                                    Text="Satisfacción" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label12" runat="server" style="font-weight: bold;">13. Un BOSQUE tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta13 Contenedor4" ID="DPregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                                    Text="Animales" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta13 Contenedor4" ID="DPregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                                    Text="Flores" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta13 Contenedor4" ID="DPregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                                    Text="Sombra" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta13 Contenedor4" ID="DPregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                                    Text="Maleza" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta13 Contenedor4" ID="DPregunta13Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                                    Text="Árboles" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label13" runat="server" style="font-weight: bold;">14. Los OBSTÁCULOS tienen siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta14 Contenedor4" ID="DPregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                                    Text="Dificultad" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta14 Contenedor4" ID="DPregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                                    Text="Desaliento" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta14 Contenedor4" ID="DPregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                                    Text="Fracaso" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta14 Contenedor4" ID="DPregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                                    Text="Impedimento" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta14 Contenedor4" ID="DPregunta14Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                                    Text="Estímulo" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label14" runat="server" style="font-weight: bold;">15. El ABORRECIMIENTO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta15 Contenedor4" ID="DPregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                                    Text="Aversión" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta15 Contenedor4" ID="DPregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                                    Text="Desagrado" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta15 Contenedor4" ID="DPregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                                    Text="Temor" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta15 Contenedor4" ID="DPregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                                    Text="Ira" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta15 Contenedor4" ID="DPregunta15Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                                    Text="Timidez" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label15" runat="server" style="font-weight: bold;">16. Una REVISTA tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta16 Contenedor4" ID="DPregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                                    Text="Anuncios" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta16 Contenedor4" ID="DPregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                                    Text="Papel" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta16 Contenedor4" ID="DPregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                                    Text="Fotografías" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta16 Contenedor4" ID="DPregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                                    Text="Grabados" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta16 Contenedor4" ID="DPregunta16Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                                    Text="Impresión" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label16" runat="server" style="font-weight: bold;">17. La CONTROVERSIA implica siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta17 Contenedor4" ID="DPregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                                    Text="Argumentos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta17 Contenedor4" ID="DPregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                                    Text="Desacuerdos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta17 Contenedor4" ID="DPregunta17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                                    Text="Aversión" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta17 Contenedor4" ID="DPregunta17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                                    Text="Gritos" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta17 Contenedor4" ID="DPregunta17Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                                    Text="Derrota" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label17" runat="server" style="font-weight: bold;">18. Un BARCO tiene siempre:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta18 Contenedor4" ID="DPregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                                    Text="Maquinaria" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta18 Contenedor4" ID="DPregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                                    Text="Cañones" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta18 Contenedor4" ID="DPregunta18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                                    Text="Quilla" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta18 Contenedor4" ID="DPregunta18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                                    Text="Timón" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton CssClass="GrpPregunta18 Contenedor4" ID="DPregunta18Resp5" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                                    Text="Velas" Skin="Metro" OnClientCheckedChanged="ValidarCambiosCampos">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>
                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView5" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name=""  id="lblSeccion5" runat="server" style="font-weight: bold;">1.   A 2 por 5 centavos, ¿Cuántos lápices pueden comprarse con 50 centavos?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg1Resp1" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label  class="labelPregunta" name="" id="Label19" runat="server" style="font-weight: bold;">2.  ¿Cuántas horas tardará un automóvil en recorrer 660 kilómetros a la velocidad de 60 kilómetros por hora?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg2Resp2" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--PREGUNTA 3--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label  class="labelPregunta" name="" id="Label20" runat="server" style="font-weight: bold;">3. Si un hombre gana $20.00 diarios y gasta $14.00, ¿Cuántos días tardará en ahorrar $300.00?</label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg3Resp3" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 4--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label21" runat="server" style="font-weight: bold;">4. Si dos pasteles cuestan $600.00, ¿Cuántos pesos cuesta la sexta parte de un pastel?</label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg4Resp4" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 5--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label22" runat="server" style="font-weight: bold;">5. ¿Cuántas veces más es 2 x 3 x 4 x 6 que 3 x 4?</label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg5Resp5" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--PREGUNTA 6--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label23" runat="server" style="font-weight: bold;">6. ¿Cuánto es el 16 por ciento de $120?</label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg6Resp6" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 7--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label24" runat="server" style="font-weight: bold;">7. ¿El cuatro por ciento de $1,000.00 es igual al ocho por ciento de qué cantidad?</label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg7Resp7" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--PREGUNTA 8--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label25" runat="server" style="font-weight: bold;">8. La capacidad de un refrigerador rectangular es de 48 metros cúbicos. Si tiene seis metros de largo por cuatro de ancho, ¿Cuál es su altura?</label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg8Resp8" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 9--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label26" runat="server" style="font-weight: bold;">9. Si 7 hombres hacen un pozo de 40 metros en 2 días, ¿Cuántos hombres se necesitan para hacerlo en medio día?</label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg9Resp9" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 10--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label27" runat="server" style="font-weight: bold;">10. A tiene $180.00, B tiene 2/3 de lo que tiene A, y C 1/2 de lo que tiene B. ¿Cuánto tienen todos juntos?</label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg10Resp10" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 11--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label28" runat="server" style="font-weight: bold;">11. Si un hombre corre 100 metros en 10 segundos, ¿Cuántos metros recorrerá como promedio en 1/5 de segundo?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg11Resp11" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 12--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label29" runat="server" style="font-weight: bold;">12. Un hombre gasta 1/4 de su sueldo en casa y alimentos y 4/8 en otros gastos. ¿Qué tanto por ciento de su sueldo ahorra?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadNumericTextBox CssClass="Contenedor5" ID="EtxtPreg12Resp12" runat="server" Width="100" DecimalDigits="1" MinValue="0"></telerik:RadNumericTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--END TEST--%>
                                </tbody>
                            </table>
                        </div>

                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView6" runat="server">

                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name=""  id="lblSeccion6" runat="server" style="font-weight: bold;">1. ¿La higiene es escencial para la salud?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg1Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta1"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg1Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta1"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label31" runat="server" style="font-weight: bold;">2. ¿Los taquígrafos usan el microscopio?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg2Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta2"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg2Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta2"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 3--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"   name="" id="Label32" runat="server" style="font-weight: bold;">3. ¿Los tiranos son justos con sus inferiores?</label>
                                            </div>

                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                          
                                                      <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg3Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta3"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg3Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta3"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 4--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label33" runat="server" style="font-weight: bold;">4. ¿Las personas desamparadas están sujetas con frecuencia a la caridad?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                          <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg4Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta4"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg4Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta4"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 5--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label34" runat="server" style="font-weight: bold;">5. ¿Las personas venerables son por lo común respetadas?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                      <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg5Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta5"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg5Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta5"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 6--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label35" runat="server" style="font-weight: bold;">6. ¿Es el escorbuto un medicamento?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            
                                                     <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg6Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta6"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg6Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta6"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 7--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label36" runat="server" style="font-weight: bold;">7. ¿Es la amonestación una clase de instrumento musical?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                           

                                                      <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg7Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta7"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg7Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta7"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 8--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label37" runat="server" style="font-weight: bold;">8. ¿Son los colores opacos preferidos para las banderas nacionales?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                     <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg8Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta8"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg8Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta8"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 9--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label38" runat="server" style="font-weight: bold;">9. ¿Las cosas misteriosas son a veces pavorosas?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                                     <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg9Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta9"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg9Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta9"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                            

                                        </td>
                                    </tr>
                                    <%--PREGUNTA 10--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label39" runat="server" style="font-weight: bold;">10. ¿Personas conscientes cometen alguna vez errores?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                 <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg10Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta10"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg10Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta10"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 11--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label40" runat="server" style="font-weight: bold;">11. ¿Son carnívoros los carneros?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                                 <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg11Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta11"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg11Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta11"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 12--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label41" runat="server" style="font-weight: bold;">12. ¿Se dan asignaturas a los caballos?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg12Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta12"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg12Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta12"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 13--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label42" runat="server" style="font-weight: bold;">13. ¿Las cartas anónimas llevan alguna vez firma de quien las escribe?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                                <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg13Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta13"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg13Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta13"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 14--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label43" runat="server" style="font-weight: bold;">14. ¿Son discontinuos los sonidos intermitentes?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                   <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg14Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta14"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg14Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta14"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 15--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label44" runat="server" style="font-weight: bold;">15. ¿Las enfermedades estimulan el buen juicio?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg15Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta15"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg15Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta15"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 16--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label45" runat="server" style="font-weight: bold;">16. ¿Son siempre perversos los hechos premeditados?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg16Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta16"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg16Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta16"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 17--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label46" runat="server" style="font-weight: bold;">17. ¿El contacto social tiende a reducir la timidez?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                                   <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg17Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta17"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg17Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta17"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 18--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label47" runat="server" style="font-weight: bold;">18. ¿Son enfermas las personas que tienen mal carácter?</label>
                                            </div>

                                        </td>
                                    </tr>


                                    <tr>
                                        <td>

                                                      <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg18Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta18"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg18Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta18"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 19--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label48" runat="server" style="font-weight: bold;">19. ¿Se caracteriza generalmente el rencor por la persistencia?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                                      <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg19Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta19"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg19Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta19"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 20--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label49" runat="server" style="font-weight: bold;">20. ¿Meticuloso quiere decir lo mismo que cuidadoso?</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                                        <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg20Resp1" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta20"
                                                    Text="Sí" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                                  <div class="ctrlBasico">
                                                <telerik:RadButton ID="FtxtPreg20Resp2" runat="server" ToggleType="Radio"  ButtonType="ToggleButton"  name="chkActivo" AutoPostBack="false" 
                                                     BorderWidth="0" BackColor="transparent" GroupName="RbtnFPregunta20"
                                                    Text="No" Skin="Metro"  OnClientClicking="valueChanged" CssClass ="Contenedor6"  >
                                                 <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--END TEST--%>
                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView7" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name=""  id="lblSeccion7" runat="server" style="font-weight: bold;">1. ABRIGO es a USAR como el PAN es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="Comer" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7" >
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="Hambre" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="Agua" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="Cocinar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label51" runat="server" style="font-weight: bold;">2. SEMANA es a MES como MES es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="Año" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="Hora" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="Minuto" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="Siglo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label52" runat="server" style="font-weight: bold;">3. LEÓN es a ANIMAL como ROSA es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="Olor" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="Hoja" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="Planta" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="Espina" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label53" runat="server" style="font-weight: bold;">4. LIBERTAD es a INDEPENDENCIA como CAUTIVERIO es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="Negro" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="Esclavitud" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="Libre" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="Sufrir" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label54" runat="server" style="font-weight: bold;">5. DECIR es a DIJO como ESTAR es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="Cantar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="Estuvo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="Hablando" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="Cantó" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label55" runat="server" style="font-weight: bold;">6. LUNES es a MARTES como VIERNES es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="Semana" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="Jueves" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="Día" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="Sábado" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label56" runat="server" style="font-weight: bold;">7. PLOMO es a PESADO como CORCHO es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="Botella" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="Peso" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="Ligero" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="Flotar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>


                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label57" runat="server" style="font-weight: bold;">8. ÉXITO es a ALEGRÍA como FRACASO es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="Tristeza" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="Suerte" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="Fracasar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="Trabajo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label58" runat="server" style="font-weight: bold;">9. GATO es a TIGRE como PERRO es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="Lobo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="Ladrido" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="Mordida" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="Agarrar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label59" runat="server" style="font-weight: bold;">10. 4 es a 16 como 5 es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="7" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="45" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="35" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="25" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label60" runat="server" style="font-weight: bold;">11. LLORAR es a REIR como TRISTE es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="Muerte" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="Alegre" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="Mortaja" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="Doctor" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label61" runat="server" style="font-weight: bold;">12. VENENO es a MUERTE como ALIMENTO es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="Comer" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="Pájaro" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="Vida" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="Malo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label62" runat="server" style="font-weight: bold;">13. 1 es a 3 como 9 es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="18" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="27" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="36" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="45" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label63" runat="server" style="font-weight: bold;">14. ALIMENTO es a HAMBRE como AGUA es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="Beber" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="Claro" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="Sed" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="Puro" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label64" runat="server" style="font-weight: bold;">15. AQUÍ es a ALLÍ como ÉSTE es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="Estos" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="Aquel" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="Ese" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="Entonces" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label65" runat="server" style="font-weight: bold;">16. TIGRE es a PELO como TRUCHA es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="Agua" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="Pez" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="Escama" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="Nadar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label66" runat="server" style="font-weight: bold;">17. PERVERTIDO es a DEPRAVADO como INCORRUPTO es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="Patria" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="Honrado" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="Canción" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="Estudio" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label  class="labelPregunta" name="" id="Label67" runat="server" style="font-weight: bold;">18. B es a D como segundo es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="Tercero" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="Último" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="Cuarto" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="Posterior" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label68" runat="server" style="font-weight: bold;">19. ESTADO es a GOBERNADOR como EJÉRCITO es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="Marina" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="Soldado" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg19Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="General" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg19Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="Sargento" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label69" runat="server" style="font-weight: bold;">20. SUJETO es a PREDICADO como NOMBRE es a:</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="Pronombre" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="Adverbio" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg20Resp3300" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="Verbo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="GbtnPreg20Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="Adjetivo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor7">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>
                                </tbody>
                            </table>
                        </div>

                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView8" runat="server">

                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name=""  id="lblSeccion8" runat="server" style="font-weight: bold;">1. con crecen los niños edad la</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label71" runat="server" style="font-weight: bold;">2. buena mar beber el para agua de es</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 3--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label72" runat="server" style="font-weight: bold;">3. lo es paz la guerra opuesto la a</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 4--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label73" runat="server" style="font-weight: bold;">4. caballos automóvil un que caminan los despacio más</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 5--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label74" runat="server" style="font-weight: bold;">5. consejo a veces es buen seguir un difícil</label>
                                            </div>

                                        </td>
                                    </tr>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 6--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label75" runat="server" style="font-weight: bold;">6. cuatrocientas todos páginas contienen libros los</label>
                                            </div>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 7--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label76" runat="server" style="font-weight: bold;">7. crecen las que fresas el más roble</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 8--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label77" runat="server" style="font-weight: bold;">8. verdadera comparada no puede amistad ser</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 9--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label78" runat="server" style="font-weight: bold;">9. envidia la perjudiciales gula son y la</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 10--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label79" runat="server" style="font-weight: bold;">10. nunca acciones premiadas las deben buenas ser</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 11--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label80" runat="server" style="font-weight: bold;">11. exteriores engañan nunca apariencias las nos</label>
                                            </div>


                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 12--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label81" runat="server" style="font-weight: bold;">12. nunca es hombre las que acciones demuestran un lo</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 13--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label82" runat="server" style="font-weight: bold;">13. ciertas siempre muerte de causan clases enfermedades</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 14--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label83" runat="server" style="font-weight: bold;">14. odio indeseables aversión sentimientos el son y la</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 15--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label84" runat="server" style="font-weight: bold;">15. frecuentemente por juzgar podemos acciones hombres nosotros sus a los</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 16--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label85" runat="server" style="font-weight: bold;">16. una es sábana sarapes tan nunca los calientes como</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 17--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label86" runat="server" style="font-weight: bold;">17. nunca que descuidados los tropiezan son</label>
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="V" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="HbtnPreg17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="F" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor8">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--END TEST--%>
                                </tbody>
                            </table>
                        </div>


                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView9" runat="server">

                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name=""  id="lblSeccion9" runat="server" style="font-weight: bold;">1.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                                    Text="Saltar"  Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                                    Text="Correr" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                                    Text="Brincar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                                    Text="Pararse" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg1Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                                    Text="Caminar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label88" runat="server" style="font-weight: bold;">2.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                                    Text="Monarquía" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                                    Text="Comunista" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                                    Text="Demócrata" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                                    Text="Anarquista" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg2Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                                    Text="Católico" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>


                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label89" runat="server" style="font-weight: bold;">3.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                                    Text="Muerte" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                                    Text="Duelo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                                    Text="Paseo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                                    Text="Pobreza" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg3Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                                    Text="Tristeza" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label90" runat="server" style="font-weight: bold;">4.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                                    Text="Carpintero" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                                    Text="Doctor" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                                    Text="Abogado" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                                    Text="Ingeniero" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg4Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                                    Text="Profesor" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label91" runat="server" style="font-weight: bold;">5.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                                    Text="Cama" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                                    Text="Silla" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                                    Text="Plato" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                                    Text="Sofá" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg5Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                                    Text="Mesa" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label92" runat="server" style="font-weight: bold;">6.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                                    Text="Francisco" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                                    Text="Santiago" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                                    Text="Juan" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                                    Text="Sara" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg6Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                                    Text="Guillermo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label93" runat="server" style="font-weight: bold;">7.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                                    Text="Duro" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                                    Text="Áspero" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                                    Text="Liso" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                                    Text="Suave" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg7Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                                    Text="Dulce" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label94" runat="server" style="font-weight: bold;">8.</label>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                                    Text="Digestión" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                                    Text="Oído" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                                    Text="Vista" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                                    Text="Olfato" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg8Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                                    Text="Tacto" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label95" runat="server" style="font-weight: bold;">9.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                                    Text="Automóvil" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                                    Text="Bicicleta" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                                    Text="Guayín" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                                    Text="Telégrafo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg9Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                                    Text="Tren" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label96" runat="server" style="font-weight: bold;">10.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                                    Text="Abajo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                                    Text="Acá" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                                    Text="Reciente" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                                    Text="Arriba" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg10Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                                    Text="Allá" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label97" runat="server" style="font-weight: bold;">11.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                                    Text="Hidalgo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                                    Text="Morelos" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                                    Text="Bravo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                                    Text="Matamoros" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg11Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                                    Text="Bolívar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label98" runat="server" style="font-weight: bold;">12.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                                    Text="Danés" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                                    Text="Galgo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                                    Text="Bulldog" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                                    Text="Pekinés" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg12Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                                    Text="Longhorn" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label99" runat="server" style="font-weight: bold;">13.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                                    Text="Tela" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                                    Text="Algodón" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                                    Text="Lino" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                                    Text="Seda" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg13Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                                    Text="Lana" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label100" runat="server" style="font-weight: bold;">14.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                                    Text="Ira" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                                    Text="Odio" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                                    Text="Alegría" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                                    Text="Piedad" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg14Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                                    Text="Razonamiento" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label101" runat="server" style="font-weight: bold;">15.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                                    Text="Edison" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                                    Text="Franklin" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                                    Text="Marconi" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                                    Text="Fulton" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg15Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                                    Text="Shakespeare" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label102" runat="server" style="font-weight: bold;">16.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                                    Text="Mariposa" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                                    Text="Halcón" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                                    Text="Avestruz" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                                    Text="Petirrojo" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg16Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                                    Text="Golondrina" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label103" runat="server" style="font-weight: bold;">17.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                                    Text="Dar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                                    Text="Prestar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                                    Text="Perder" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                                    Text="Ahorrar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg17Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                                    Text="Derrochar" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label104" runat="server" style="font-weight: bold;">18.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                                    Text="Australia" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                                    Text="Cuba" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                                    Text="Córcega" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                                    Text="Irlanda" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="IbtnPreg18Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                                    Text="España" Skin="Metro" OnClientClicking="valueChanged" CssClass ="Contenedor9">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--END TEST--%>
                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView10" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name=""  id="lblSeccion10" runat="server" style="font-weight: bold;">1.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox  ID="RadNumericTextBox12" Text="8" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox13" Text="7" ReadOnly="true" Enabled="false" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox14" Text="6" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox15" Text="5" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox16" Text="4" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox17" Text="3" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox  CssClass="Contenedor10"  ID="JbtnPreg1Resp1" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox  CssClass="Contenedor10"  ID="JbtnPreg1Resp2" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 2--%>

                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label106" runat="server" style="font-weight: bold;">2.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox  ID="RadNumericTextBox20" Text="3" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox21" Text="8" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox22" Text="13" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox23" Text="18" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox24" Text="23" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox25" Text="28" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox  CssClass="Contenedor10"  ID="JbtnPreg2Resp1" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg2Resp2" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 3--%>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label107" runat="server" style="font-weight: bold;">3.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox28" Text="1" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox29" Text="2" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox30" Text="4" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox31" Text="8" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox32" Text="16" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox33" Text="32" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg3Resp1" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg3Resp2" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 4--%>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label108" runat="server" style="font-weight: bold;">4.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox36" Text="8" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox37" Text="8" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox38" Text="6" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox39" Text="6" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox40" Text="4" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox41" Text="4" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg4Resp1" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg4Resp2" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 5--%>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label  class="labelPregunta"  name="" id="Label109" runat="server" style="font-weight: bold;">5.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox44" Text="11.3/4" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox45" Text="12" ReadOnly="true" Enabled="false" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox46" Text="12.1/4" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox47" Text="12.1/2" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox48" Text="12.3/4" ReadOnly="true" Enabled="false" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>                                            
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg5Resp1" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg5Resp2" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 6--%>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label110" runat="server" style="font-weight: bold;">6.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox52" Text="8" Enabled="false" ReadOnly="true" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox53" Text="9" Enabled="false" ReadOnly="true" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox54" Text="12" Enabled="false" ReadOnly="true" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox55" Text="13" Enabled="false" ReadOnly="true" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox56" Text="16" Enabled="false" ReadOnly="true" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox57" Text="17" Enabled="false" ReadOnly="true" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg6Resp1"  runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg6Resp2" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 7--%>

                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label111" runat="server" style="font-weight: bold;">7.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox60" Text="16" Enabled="false" ReadOnly="true" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox61" Text="8" Enabled="false" ReadOnly="true" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox62" Text="4" Enabled="false" ReadOnly="true" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox63" Text="2" Enabled="false" ReadOnly="true" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox64" Text="1" Enabled="false" ReadOnly="true" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox65" Text="1/2" Enabled="false" ReadOnly="true" runat="server" Width="100"  Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg7Resp1" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg7Resp2" runat="server" Width="100" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 8--%>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label112" runat="server" style="font-weight: bold;">8.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox68" Text="31.3" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox69" Text="40.3" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox70" Text="49.3" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox71" Text="58.3" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox72" Text="67.3" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox73" Text="76.3" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg8Resp1" runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg8Resp2" runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 9--%>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label113" runat="server" style="font-weight: bold;">9.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox76" Text="3" Enabled="false" ReadOnly="true" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox77" Text="5" Enabled="false" ReadOnly="true" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox78" Text="4" Enabled="false" ReadOnly="true" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox79" Text="6" Enabled="false" ReadOnly="true" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox80" Text="5" Enabled="false" ReadOnly="true" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox81" Text="7" Enabled="false" ReadOnly="true" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg9Resp1" runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg9Resp2" runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 10--%>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label114" runat="server" style="font-weight: bold;">10.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox84" Text="7" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox85" Text="11" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox86" Text="15" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox87" Text="16" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox88" Text="20" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadTextBox1" Text="24" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox89" Text="25" Enabled="false" ReadOnly="true" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox92" Text="29" Enabled="false" ReadOnly="true" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg10Resp1" runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg10Resp2" runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--PREGUNTA 11--%>


                                    <tr>
                                        <td>

                                            <div class="ctrlBasico">
                                                <label class="labelPregunta"  name="" id="Label115" runat="server" style="font-weight: bold;">11.</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox93" Text="1/25" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox94" Text="1/5" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox95" Text="1" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox ID="RadNumericTextBox96" Text="5" ReadOnly="true" Enabled="false" runat="server" Width="100" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg11Resp1" runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                            <div class="ctrlBasico">
                                                <telerik:RadTextBox CssClass="Contenedor10"  ID="JbtnPreg11Resp2" runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadTextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--END TEST--%>
                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>

                </telerik:RadMultiPage>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="DivMoveLeft" id="cronometro" runat="server">
        <div class="Cronometro" id="ContentTime">Tiempo restante <span id="time">15:00</span></div>
    </div>

    <div class="divControlDerecha">

        <div class="ctrlBasico">
            <%--<telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Siguiente" AutoPostBack="true" Visible="false"></telerik:RadButton>--%>
            <telerik:RadButton ID="btnSiguiente" runat="server" OnClientClicked="llamaAjaxRequest" Text="Siguiente" AutoPostBack="false"></telerik:RadButton>
            </div> 
            <div class="ctrlBasico">
            <telerik:RadButton ID="btnImpresionPrueba" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false" Visible = "false"></telerik:RadButton>
        </div>
<%--       <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" Visible="false" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>--%>
        <%-- <div class="ctrlBasico">
              <telerik:RadButton ID="btnCorregir" runat="server" Visible="false" OnClick="btnCorregir_Click" Text="Corregir" AutoPostBack="true"></telerik:RadButton>
            <telerik:RadButton ID="RadButton2" runat="server" Visible="false" OnClientClicked="llamaAjaxRequest" Text="Siguiente" AutoPostBack="false"></telerik:RadButton>
        </div>--%>

    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
