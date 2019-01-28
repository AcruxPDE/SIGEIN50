<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaPersonalidadLaboralI.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaPersonalidadLaboral1" %>

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
            width: 200px;
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
        @media print {
      body, html, #wrapper {
          width: 100%;
      }
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
                                //var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
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
                            }
                        }
                        else {

                            // window.close();
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "<label><b>Instrucciones:</b><br/>Las palabras descriptivas siguientes se encuentran agrupadas en series de cuatro.</br>" +
                   " Examina las palabras de cada serie y marca la opción bajo la columna M que mejor  te describa.</br>" +
                   " Marca la opción bajo la columna L que menos te describa.</br>" +
                   " Asegúrate de marcar solamente una palabra bajo M y solamente una palabra bajo L en cada serie.</label>";
                    //radconfirm(JustificarTexto(text), callBackFunction, 400, 280, null, "Personalidad laboral I");
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Personalidad laboral I");
                }
            };


            function close_window(sender, args) {
                if (vPruebaEstatus != "TERMINADA") {
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
                    // window.close();
                    window.location = "Default.aspx?ty=sig";
                }
            }

            function confirm_edit(sender, args) {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            if (ValidarContendorPreguntas()) {
                                var btn = $find("<%=btnTerminar.ClientID%>");
                                btn.click();
                            }
                        }
                    });
                    var text = "¿Estás seguro que deseas editar los resultados de esta prueba?";
                    radconfirm(text, callBackFunction, 400, 150, null, "");
                    args.set_cancel(true);
                }
            

            function WinClose(sender, args) {
                vPruebaEstatus = "TERMINADA";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                //var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                var oWnd = radalert("El tiempo de la aplicación de la prueba ha concluido y has dejado espacios en blanco, por lo que la prueba será invalidada. Te sugerimos contactar al ejecutivo de selección para más opciones, ya que el sistema no te permitirá regresar a la aplicación.", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                //window.close();
                window.location = "Default.aspx?ty=sig";
            }

            function Close() {
                window.top.location.href = window.top.location.href;
                //window.close();
            }

            function addGrupoContestado(valor) {
                if (a.indexOf(valor) == -1 || a.length == 0) {
                    a.push(valor);
                    //console.info(a);
                }
            }
            //EN PROCESO PARA LA DETECCION DEL 3 EN LOS SEGMENTOS
            function valueChanged(sender, args) {
                var vId = "";
                vId = sender.get_id();
                var vClassName = sender._cssClass;
                addGrupoContestado(sender._groupName);
                var x = document.getElementsByClassName(vClassName);

                var i = 0;
                for (i = 0; i < x.length; i++) {
                    if (x[i].control.get_id() != vId) {
                        if (x[i].control._checked) {
                            //console.info(x[i].control);
                            x[i].control.set_checked(false);
                            // console.info(x[i]);
                            // x[i].control.removeAttribute('checked');
                            // radalert("No puede seleccionar la misma posición en M y L.", 400, 150, "");
                        }
                    }
                }
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
                        GrupoNoContestado.style.borderWidth = '1px';
                        var flag = false;
                        break;
                    }
                }
                return flag;
            }            
            function PrintPane() {
                //window.print();
                var panel = $find("<%=rpnGridSolicitudes.ClientID %>");
                var arrExtStylsheetFiles = getTelerikCssLinks();

                panel.Print(arrExtStylsheetFiles);
            }

            function getTelerikCssLinks() {
                var result = new Array();

                var links = document.getElementsByTagName("LINK"); //get all link elements on the page

                for (var i = 0; i < links.length; i++) {
                    if (links[i].getAttribute("class") == "Telerik_stylesheet")//check if the link element is a Telerik Stylesheet
                        result.push(links[i].getAttribute("href", 2)); //add link href attribute to the result
                }
                var cssFileAbsPath = 'http://localhost:7192/print.css';
                result.push(cssFileAbsPath);
                return result;
            }

            function OpenReport() {
                var vURL = "ReporteadorPruebasIDP.aspx";
                var vTitulo = "Impresión personalidad laboral 1";

                var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClToken %>';



                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=LABORAL1";
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
    <label style="font-size: 21px;">Personalidad laboral I</label>
<%--    <div id ="divCandidato" runat="server" visible="false" class="divControlDerecha" >
         <div class="ctrlBasico" style="margin-left: 20px;">
                    <div class="divControlIzquierda" style="padding-top:8px;">
                        <label id="Label8" name="lblcandidato" runat="server"  style="font-size: 19px;">Candidato:</label>
                    </div>
                    <div class="divControlIzquierda" style="clear: both;"></div>
                    <div>
                        <telerik:RadTextBox ID="txtCandidato" runat="server" Width="350px" MaxLength="100" ReadOnly="true" 
                            Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
    </div>--%>
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

            <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px; text-align: justify;">
                            <label id="Label190" runat="server">Las palabras descriptivas siguientes se encuentran agrupadas en series de cuatro.</label>
                            <br />
                            <label id="Label191" runat="server" >Examina las palabras de cada serie y marca la opción bajo la columna M que mejor  te describa.</label>
                            <br />
                            <label id="Label192" runat="server" >Marca la opción bajo la columna L que menos te describa.</label>
                            <br />
                            <label id="Label193" runat="server" >Asegúrate de marcar solamente una palabra bajo M y solamente una palabra bajo L en cada serie.</label>
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>


            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <table style="width: 90%; height: 100%; margin-left: 3%;">
                    <thead>
                        <tr>
                            <td width="140px"></td>
                            <td width="40px"></td>
                            <td width="40px"></td>
                            <td></td>
                        </tr>
                    </thead>
                    <tbody>
                        <%--1--%>
                        <tr>
                            <td class="DescripcionStyle">1</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--         <td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label runat="server" title="El que induce u obliga a los demás a moverse en un sentido mediante razones">
                                    Persuasivo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta1 Contenedor" ID="btnPersuasivo1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta1 Contenedor" ID="btnPersuasivo2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--   <td>
                                <label id="lblPersuasivo" name="nbPersuasivo" runat="server">El que induce u obliga a los demás a moverse en un sentido mediante razones</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label runat="server" title="El Brioso, galán que actúa con soltura, urbanismo y cortesía">Gentil</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta2 Contenedor" ID="btnGentil1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta2 Contenedor" ID="btnGentil2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label1" name="nbPersuasivo" runat="server">El brioso, galán que actúa con soltura, urbanismo y cortesía</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label96" runat="server" title="El sumiso o el que se rinde. No se interpreta como la percepción cristiana que consiste en el conocimiento de nuestra bajeza">Humilde</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta3 Contenedor" ID="btnHumilde1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta3 Contenedor" ID="btnHumilde2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label2" name="" runat="server">El sumiso o el que se rinde. No se interpreta como la percepción cristiana que consiste en el conocimiento de nuestra bajeza</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label97" runat="server" title="El espontáneo, el natural, el nuevo, el singular y extraño">Original</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta4 Contenedor" ID="btnOriginal1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta4 Contenedor" ID="btnOriginal2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label3" name="" runat="server">El espontáneo, el natural, el nuevo, el singular y extraño</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--2--%>
                        <tr>
                            <td class="DescripcionStyle">2</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label98" runat="server" title="El que demuestra empuje e iniciativa en una forma positiva. No el propenso a ofender o a faltar el derecho al otro">Agresivo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta5 Contenedor" ID="btnAgresivo" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta5 Contenedor" ID="btnAgresivo2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label92" name="nbPersuasivo" runat="server">El que demuestra empuje e iniciativa en una forma positiva. No el propenso a ofender o a faltar el derecho al otro</label>
                            </td>--%>
                        </tr>



                        <tr>

                            <td>
                                <label id="Label99" runat="server" title="El más popular, el que genera mayor atracción en el grupo">Alma de la fiesta</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta6 Contenedor" ID="btnAlmafiesta" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta6 Contenedor" ID="btnAlmafiesta2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--    <td>
                                <label id="Label93" name="nbPersuasivo" runat="server">El más popular, el que genera mayor atracción en el grupo</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label100" runat="server" title="El amante de la comodidad, que busca los caminos fáciles, el oportunista">Comodino</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta7 Contenedor" ID="btnComodino" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta7 Contenedor" ID="btnComodino2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--    <td>
                                <label id="Label94" name="" runat="server">El amante de la comodidad, que busca los caminos fáciles, el oportunista</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label101" runat="server" title="El miedoso, cobarde o irresoluto">Temeroso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta8 Contenedor" ID="btnTemeroso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta8 Contenedor" ID="btnTemeroso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label95" name="" runat="server">El miedoso, cobarde o irresoluto</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--3--%>


                        <tr>
                            <td class="DescripcionStyle">3</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--  <td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label102" runat="server" title="El que complace y da gusto a los demás">Agradable</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta9 Contenedor" ID="btnAgradable" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta9 Contenedor" ID="btnAgradable2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label4" name="nbPersuasivo" runat="server">El que complace y da gusto a los demás</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label103" runat="server" title="El que reconoce el poder de Dios sobre todas las cosas">Temeroso de Dios</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta10 Contenedor" ID="btnTemerosoDios" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta10 Contenedor" ID="btnTemerosoDios2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label5" name="nbPersuasivo" runat="server">El que reconoce el poder de Dios sobre todas las cosas</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label104" runat="server" title="El terco y porfiado con un inquebrantable fuerza de voluntad para realizar algún objetivo">Tenaz</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta11 Contenedor" ID="btnTenaz" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta11 Contenedor" ID="btnTenaz2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label6" name="" runat="server">El terco y porfiado con un inquebrantable fuerza de voluntad para realizar algún objetivo</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label105" runat="server" title="El que inclina a una persona a otra con su voluntad">Atractivo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta12 Contenedor" ID="btnAtractivo" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta12 Contenedor" ID="btnAtractivo2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label7" name="" runat="server">El que inclina a una persona a otra con su voluntad</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>



                        <%--4--%>

                        <tr>
                            <td class="DescripcionStyle">4</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--        <td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        
                        <tr>

                            <td>
                                <label id="Label107" runat="server" title="El reservado, el que actúa con astucia, con precaución y que tiene en cierta forma maña para engañar">Cauteloso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta14 Contenedor" ID="btnCauteloso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta14 Contenedor" ID="btnCauteloso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label9" name="nbPersuasivo" runat="server">El reservado, el que actúa con astucia, con precaución y que tiene en cierta forma maña para engañar</label>
                            </td>--%>
                        </tr>
                        <tr>


                            <td>
                                <label id="Label106" runat="server" title="El que toma resoluciones, el que define, el valeroso y osado">Determinado</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta13 Contenedor" ID="btnDeterminado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta13 Contenedor" ID="btnDeterminado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--                            <td>
                                <label id="Label8" name="nbPersuasivo" runat="server">El que toma resoluciones, el que define, el valeroso y osado</label>
                            </td>--%>
                        </tr>



                        <tr>

                            <td>
                                <label id="Label108" runat="server" title="El que sabe convencer y obligar a otro con razones a que reconozca una cosa o mude de opinión">Convincente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta15 Contenedor" ID="btnConvincente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta15 Contenedor" ID="btnConvincente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <%--/td>
                            <td>
                                <label id="Label10" name="" runat="server">El que sabe convencer y obligar a otro con razones a que reconozca una cosa o mude de opinión</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label109" runat="server" title="El que todo se lo cree, que es amable, dócil y bondadoso">Bonachón</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta16 Contenedor" ID="btnBonachon" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta16 Contenedor" ID="btnBonachon2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label11" name="" runat="server">El que todo se lo cree, que es amable, dócil y bondadoso</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--5--%>

                        <tr>
                            <td class="DescripcionStyle">5</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label110" runat="server" title="Es el suave, apacible, fácil a la enseñanza, obediente que se deja labrar con facilidad">Dócil</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta17 Contenedor" ID="btnDocil" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta17 Contenedor" ID="btnDocil2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label12" name="nbPersuasivo" runat="server">Es el suave, apacible, fácil a la enseñanza, obediente que se deja labrar con facilidad</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label111" runat="server" title="El que insolenta fácilmente faltando al respeto a los demás">Atrevido</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta18 Contenedor" ID="btnAtrevido" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta18 Contenedor" ID="btnAtrevido2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label13" name="nbPersuasivo" runat="server">El que insolenta fácilmente faltando al respeto a los demás</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label112" runat="server" title="El que guarda fidelidad, persona fiel">Leal</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta19 Contenedor" ID="btnleal" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta19 Contenedor" ID="btnleal2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label14" name="" runat="server">El que guarda fidelidad, persona fiel</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label113" runat="server" title="El que hace muy viva y grata su propia impresión">Encantador</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta20 Contenedor" ID="btnEncantador" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta20 Contenedor" ID="btnEncantador2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--   <td>
                                <label id="Label15" name="" runat="server">El que hace muy viva y grata su propia impresión</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--6--%>


                        <tr>
                            <td class="DescripcionStyle">6</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label114" runat="server" title="El hábil, el despejado">Dispuesto</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta21 Contenedor" ID="btnDispuesto" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta21 Contenedor" ID="btnDispuesto2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label16" name="nbPersuasivo" runat="server">El hábil, el despejado</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label115" runat="server" title="El que tiene un fuerte impulso de la voluntad hacia la posesión o disfrute de una cosa">Deseoso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta22 Contenedor" ID="btnDeseoso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta22 Contenedor" ID="btnDeseoso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label17" name="nbPersuasivo" runat="server">El que tiene un fuerte impulso de la voluntad hacia la posesión o disfrute de una cosa</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label116" runat="server" title="El que obra de acuerdo con sus principios. El que mantiene una proporción consigo mismo">Consecuente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta23 Contenedor" ID="btnConsecuente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta23 Contenedor" ID="btnConsecuente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label18" name="" runat="server">El que obra de acuerdo con sus principios. El que mantiene una proporción consigo mismo</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label117" runat="server" title="El que siente exaltación y fogosidad">Entusiasta</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta24 Contenedor" ID="btnEntusiasta" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta24 Contenedor" ID="btnEntusiasta2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label19" name="" runat="server">El que siente exaltación y fogosidad</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--7--%>


                        <tr>
                            <td class="DescripcionStyle">7</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label118" runat="server" title="Determinación inquebrantable para lograr algo">Fuerza de voluntad</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta25 Contenedor" ID="btnFuerza" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta25 Contenedor" ID="btnFuerza2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label20" name="nbPersuasivo" runat="server">Determinación inquebrantable para lograr algo</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label119" runat="server" title="Cualidad para escuchar y digerir otro punto de vista diferente">Mente abierta</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta26 Contenedor" ID="btnMenteAbierta" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta26 Contenedor" ID="btnMenteAbierta2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label21" name="nbPersuasivo" runat="server">Cualidad para escuchar y digerir otro punto de vista diferente</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label120" runat="server" title="El que accede a lo que otro desea">Complaciente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta27 Contenedor" ID="btnComplaciente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta27 Contenedor" ID="btnComplaciente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label22" name="" runat="server">El que accede a lo que otro desea</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label121" runat="server" title="El que demuestra valor, energía, esfuerzo, intención y voluntad">Animoso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta28 Contenedor" ID="btnAnimoso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta28 Contenedor" ID="btnAnimoso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <%--  </td>
                            <td>
                                <label id="Label23" name="" runat="server">El que demuestra valor, energía, esfuerzo, intención y voluntad</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--8--%>

                        <tr>
                            <td class="DescripcionStyle">8</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label122" runat="server" title="El vano y orgulloso hasta el punto de la arrogancia. No el que espera con firmeza o que confía en sí mismo">Confiado</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta29 Contenedor" ID="btnConfiado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta29 Contenedor" ID="btnConfiado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label24" name="nbPersuasivo" runat="server">El vano y orgulloso hasta el punto de la arrogancia. No el que espera con firmeza o que confía en sí mismo</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label123" runat="server" title="El que tiene analogía o inclinación de sentimientos hacia otros">Simpatizador</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta30 Contenedor" ID="btnSimpatizador" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta30 Contenedor" ID="btnSimpatizador2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <%--</td>
                            <td>
                                <label id="Label25" name="nbPersuasivo" runat="server">El que tiene analogía o inclinación de sentimientos hacia otros</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label124" runat="server" title="El que reconoce y respeta las opiniones, prácticas y comportamiento de otros sin importar estar de acuerdo con ellos. No el que sufre con paciencia. No el que deja pasar cosas que no son lícitas">Tolerante</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta31 Contenedor" ID="btnTolerante" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta31 Contenedor" ID="btnTolerante2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label26" name="" runat="server">El que reconoce y respeta las opiniones, prácticas y comportamiento de otros sin importar estar de acuerdo con ellos. No el que sufre con paciencia. No el que deja pasar cosas que no son lícitas</label>
                            </td>--%>
                        </tr>

                        <tr>


                            <td>
                                <label id="Label125" runat="server" title="El que responde en la mayoría de las veces de una manera positiva. No el que sostiene o ratifica lo dicho">Afirmativo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta32 Contenedor" ID="btnAfirmativo" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta32 Contenedor" ID="btnAfirmativo2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label27" name="" runat="server">El que responde en la mayoría de las veces de una manera positiva. No el que sostiene o ratifica lo dicho</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--9--%>


                        <tr>
                            <td class="DescripcionStyle">9</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label126" runat="server" title="El calmado, sereno, imparcial, inalterable, paciente, sufrido, que tiene siempre el mismo ánimo">Ecuánime</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta33 Contenedor" ID="btnEcuanime" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta33 Contenedor" ID="btnEcuanime2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label28" name="nbPersuasivo" runat="server">El calmado, sereno, imparcial, inalterable, paciente, sufrido, que tiene siempre el mismo ánimo</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label127" runat="server" title="El puntual y exacto">Preciso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta34 Contenedor" ID="btnPreciso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta34 Contenedor" ID="btnPreciso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label29" name="nbPersuasivo" runat="server">El puntual y exacto</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label128" runat="server" title="El ansioso, al que le falta sentido de seguridad">Nervioso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta35 Contenedor" ID="btnNervioso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta35 Contenedor" ID="btnNervioso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--    <td>
                                <label id="Label30" name="" runat="server">El ansioso, al que le falta sentido de seguridad</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label129" runat="server" title="El apacible, el alegre y festivo">Jovial</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta36 Contenedor" ID="btnJovial" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta36 Contenedor" ID="btnJovial2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label31" name="" runat="server">El apacible, el alegre y festivo</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--10--%>



                        <tr>
                            <td class="DescripcionStyle">10</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label130" runat="server" title="El acostumbrado a la obediencia">Disciplinado</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta37 Contenedor" ID="btnDisciplinado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta37 Contenedor" ID="btnDisciplinado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label32" name="nbPersuasivo" runat="server">El acostumbrado a la obediencia</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label131" runat="server" title="El que obra con magnanimidad y nobleza, liberal, dadivoso, franco">Generoso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta38 Contenedor" ID="btnGeneroso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta38 Contenedor" ID="btnGeneroso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label33" name="nbPersuasivo" runat="server">El que obra con magnanimidad y nobleza, liberal, dadivoso, franco</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label132" runat="server" title="El que refleja valor y esfuerzo, energía con voluntad inquebrantable">Animoso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta39 Contenedor" ID="btnAnimado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta39 Contenedor" ID="btnAnimado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label34" name="" runat="server">El que refleja valor y esfuerzo, energía con voluntad inquebrantable</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label133" runat="server" title="El que supera obstáculos, el que es constante y se mantiene en un propósito">Persistente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta40 Contenedor" ID="btnPersistente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta40 Contenedor" ID="btnPersistente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--   <td>
                                <label id="Label35" name="" runat="server">El que supera obstáculos, el que es constante y se mantiene en un propósito</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--11--%>


                        <tr>
                            <td class="DescripcionStyle">11</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label134" runat="server" title="El que contiende o disputa con otro por una causa de superación común, por perfección o por posición de propiedades">Competitivo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta41 Contenedor" ID="btnCompetitivo" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta41 Contenedor" ID="btnCompetitivo2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label36" name="nbPersuasivo" runat="server">El que contiende o disputa con otro por una causa de superación común, por perfección o por posición de propiedades</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label135" runat="server" title="El que sabe infundir alegría, contento de ánimo">Alegre</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta42 Contenedor" ID="btnAlegre" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta42 Contenedor" ID="btnAlegre2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label37" name="nbPersuasivo" runat="server">El que sabe infundir alegría, contento de ánimo</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label136" runat="server" title="El que habla con reflexión y que guarda consideración">Considerado</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta43 Contenedor" ID="btnConsiderado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta43 Contenedor" ID="btnConsiderado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label38" name="" runat="server">El que habla con reflexión y que guarda consideración</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label137" runat="server" title="Que tiene amistad y buena correspondencia. El que no tiene fricciones con otras personas">Armonioso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta44 Contenedor" ID="btnArmonioso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta44 Contenedor" ID="btnArmonioso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label39" name="" runat="server">Que tiene amistad y buena correspondencia. El que no tiene fricciones con otras personas</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--12--%>


                        <tr>
                            <td class="DescripcionStyle">12</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label138" runat="server" title="Que es digno de ser admirado">Admirable</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta45 Contenedor" ID="btnAdmirable" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta45 Contenedor" ID="btnAdmirable2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label40" name="nbPersuasivo" runat="server">Que es digno de ser admirado</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label139" runat="server" title="Lleno de carácter apacible">Bondadoso</label>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta46 Contenedor" ID="btnBondadoso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta46 Contenedor" ID="btnBondadoso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label41" name="nbPersuasivo" runat="server">Lleno de carácter apacible</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label140" runat="server" title="El que se conforma, se sujeta, el condescendiente. El que acepta estar bajo la sumisión de otro">Resignado</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta47 Contenedor" ID="btnResignado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta47 Contenedor" ID="btnResignado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label42" name="" runat="server">El que se conforma, se sujeta, el condescendiente. El que acepta estar bajo la sumisión de otro</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label141" runat="server" title="No cambia fácilmente su punto de vista">Carácter firme</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta48 Contenedor" ID="btnCaracter" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta48 Contenedor" ID="btnCaracter2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label43" name="" runat="server">No cambia fácilmente su punto de vista</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--13--%>


                        <tr>
                            <td class="DescripcionStyle">13</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label142" runat="server" title="El que cumple la voluntad de quien manda">Obediente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta49 Contenedor" ID="btnObediente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta49 Contenedor" ID="btnObediente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label44" name="nbPersuasivo" runat="server">El que cumple la voluntad de quien manda</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label143" runat="server" title="El que fácilmente se agravia o se ofende. Demasiado delicado en su trato">Quisquilloso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta50 Contenedor" ID="btnQuisquilloso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta50 Contenedor" ID="btnQuisquilloso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label45" name="nbPersuasivo" runat="server">El que fácilmente se agravia o se ofende. Demasiado delicado en su trato</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label144" runat="server" title="Que no se deja vencer con ruegos y dádivas">Inconquistable</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta51 Contenedor" ID="btnInconquistable" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta51 Contenedor" ID="btnInconquistable2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label46" name="" runat="server">Que no se deja vencer con ruegos y dádivas</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label145" runat="server" title="El que tiene buen sentido del humor">Juguetón</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta52 Contenedor" ID="btnJugueton" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta52 Contenedor" ID="btnJugueton2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label47" name="" runat="server">El que tiene buen sentido del humor</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--14--%>


                        <tr>
                            <td class="DescripcionStyle">14</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label146" runat="server" title="El que guarda reverencia, el que es obediente, que guarda consideración para con los demás">Respetuoso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta53 Contenedor" ID="btnRespetuoso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta53 Contenedor" ID="btnRespetuoso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--   <td>
                                <label id="Label48" name="nbPersuasivo" runat="server">El que guarda reverencia, el que es obediente, que guarda consideración para con los demás</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label147" runat="server" title="Que muestra imaginación instintiva y empuje, que se dedica a resolver cosas difíciles">Emprendedor</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta54 Contenedor" ID="btnEmprendedor" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta54 Contenedor" ID="btnEmprendedor2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--   <td>
                                <label id="Label49" name="nbPersuasivo" runat="server">Que muestra imaginación instintiva y empuje, que se dedica a resolver cosas difíciles</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label148" runat="server" title="El que ve y juzga las cosas bajo su aspecto más favorable">Optimista</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta55 Contenedor" ID="btnOptimista" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta55 Contenedor" ID="btnOptimista2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label50" name="" runat="server">El que ve y juzga las cosas bajo su aspecto más favorable</label>
                            </td--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label149" runat="server" title="El ayudador sin trabas">Servicial</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta56 Contenedor" ID="btnServicial" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta56 Contenedor" ID="btnServicial2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label51" name="" runat="server">El ayudador sin trabas</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--15--%>


                        <tr>
                            <td class="DescripcionStyle">15</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label150" runat="server" title="El que manifiesta coraje o valor">Valiente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta57 Contenedor" ID="btnValiente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta57 Contenedor" ID="btnValiente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--    <td>
                                <label id="Label52" name="nbPersuasivo" runat="server">El que manifiesta coraje o valor</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label151" runat="server" title="El que anima la mente o las emociones de otros">Inspirador</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta58 Contenedor" ID="btnInspirador" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta58 Contenedor" ID="btnInspirador2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label53" name="nbPersuasivo" runat="server">El que anima la mente o las emociones de otros</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label152" runat="server" title="El que permite por sí mismo estar sujeto a otro">Sumiso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta59 Contenedor" ID="btnSumiso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta59 Contenedor" ID="btnSumiso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--      <td>
                                <label id="Label54" name="" runat="server">El que permite por sí mismo estar sujeto a otro</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label153" runat="server" title="El lento para actuar y decidir">Tímido</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta60 Contenedor" ID="btnTimido" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta60 Contenedor" ID="btnTimido2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label55" name="" runat="server">El lento para actuar y decidir</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--16--%>


                        <tr>
                            <td class="DescripcionStyle">16</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label154" runat="server" title="El que se ajusta fácilmente a condiciones nuevas o diferentes">Adaptable</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta61 Contenedor" ID="btnAdaptable" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta61 Contenedor" ID="btnAdaptable2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label56" name="nbPersuasivo" runat="server">El que se ajusta fácilmente a condiciones nuevas o diferentes</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label155" runat="server" title="Dado a argüir o proponer razones para, por o contra algo">Disputador</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta62 Contenedor" ID="btnDisputador" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta62 Contenedor" ID="btnDisputador2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label57" name="nbPersuasivo" runat="server">Dado a argüir o proponer razones para, por o contra algo</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label156" runat="server" title="El que aparece en forma sistemática. No estar involucrado">Indiferente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta63 Contenedor" ID="btnIndiferente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta63 Contenedor" ID="btnIndiferente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--                            <td>
                                <label id="Label58" name="" runat="server">El que aparece en forma sistemática. No estar involucrado</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label157" runat="server" title="El que a todo el mundo le cae bien">Sangre liviana</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta64 Contenedor" ID="btnSangreliviana" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta64 Contenedor" ID="btnSangreliviana2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label59" name="" runat="server">El que a todo el mundo le cae bien</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--17--%>


                        <tr>
                            <td class="DescripcionStyle">17</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label158" runat="server" title="El que busca y goza la compañía de otros">Amiguero</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta65 Contenedor" ID="btnAmiguero" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta65 Contenedor" ID="btnAmiguero2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label60" name="nbPersuasivo" runat="server">El que busca y goza la compañía de otros</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label159" runat="server" title="Capaz de soportar la aflicción o el retraso con calma. El que sabe esperar el momento">Paciente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta66 Contenedor" ID="btnPaciente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta66 Contenedor" ID="btnPaciente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label61" name="nbPersuasivo" runat="server">Capaz de soportar la aflicción o el retraso con calma. El que sabe esperar el momento</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label160" runat="server" title="El que confía en sus propias capacidades y recursos">Confianza en sí mismo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta67 Contenedor" ID="btnConfianza" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta67 Contenedor" ID="btnConfianza2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label62" name="" runat="server">El que confía en sus propias capacidades y recursos</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label161" runat="server" title="El que solo habla para comunicar lo más sobresaliente">Mesurado para hablar</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta68 Contenedor" ID="btnMesurado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta68 Contenedor" ID="btnMesurado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label63" name="" runat="server">El que solo habla para comunicar lo más sobresaliente</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>

                        <%--18--%>


                        <tr>
                            <td class="DescripcionStyle">18</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label162" runat="server" title="El satisfecho con lo que tiene">Conforme</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta69 Contenedor" ID="btnConforme" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta69 Contenedor" ID="btnConforme2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label64" name="nbPersuasivo" runat="server">El satisfecho con lo que tiene</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label163" runat="server" title="Persona a la cual la confianza es puesta">Confiable</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta70 Contenedor" ID="btnConfiable" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta70 Contenedor" ID="btnConfiable2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--   <td>
                                <label id="Label65" name="nbPersuasivo" runat="server">Persona a la cual la confianza es puesta</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label164" runat="server" title="El que tiene calma y serenidad">Pacífico</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta71 Contenedor" ID="btnPacific" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta71 Contenedor" ID="btnPacific2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label66" name="" runat="server">El que tiene calma y serenidad</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label165" runat="server" title="Persona caracterizada por estar relacionada con pensamientos prácticos, reales, no ficticios">Positivo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta72 Contenedor" ID="btnPositivo" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta72 Contenedor" ID="btnPositivo2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label67" name="" runat="server">Persona caracterizada por estar relacionada con pensamientos prácticos, reales, no ficticios</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>
                        <%--19--%>


                        <tr>
                            <td class="DescripcionStyle">19</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label166" runat="server" title="El que toma caminos riesgosos, que actúa sin escrúpulos y que busca destacar">Aventurero</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta73 Contenedor" ID="btnAventurero" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta73 Contenedor" ID="btnAventurero2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--                            <td>
                                <label id="Label68" name="nbPersuasivo" runat="server">El que toma caminos riesgosos, que actúa sin escrúpulos y que busca destacar</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label167" runat="server" title="Apto para recibir cualquier tipo de información">Receptivo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta74 Contenedor" ID="btnPerceptivo" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta74 Contenedor" ID="btnPerceptivo2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--    <td>
                                <label id="Label69" name="nbPersuasivo" runat="server">Apto para recibir cualquier tipo de información</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label168" runat="server" title="El caluroso y sincero">Cordial</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta75 Contenedor" ID="btnCordial" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta75 Contenedor" ID="btnCordial2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--      <td>
                                <label id="Label70" name="" runat="server">El caluroso y sincero</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label169" runat="server" title="El que no es excesivo o extremoso y que se mantiene con puntos de vista medios entre los extremos radicales">Moderado</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta76 Contenedor" ID="btnModerado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta76 Contenedor" ID="btnModerado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label71" name="" runat="server">El que no es excesivo o extremoso y que se mantiene con puntos de vista medios entre los extremos radicales</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>
                        <%--20--%>


                        <tr>
                            <td class="DescripcionStyle">20</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label170" runat="server" title="El que perdona fácilmente">Indulgente</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta77 Contenedor" ID="btnIndulgente" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta77 Contenedor" ID="btnIndulgente2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <%-- </td>
                            <td>
                                <label id="Label72" name="nbPersuasivo" runat="server">El que perdona fácilmente</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label171" runat="server" title="El que cultiva o percibe en una forma superior la belleza">Esteta</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta78 Contenedor" ID="btnEsteta" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta78 Contenedor" ID="btnEsteta2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label73" name="nbPersuasivo" runat="server">El que cultiva o percibe en una forma superior la belleza</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label172" runat="server" title="El robusto y fuerte">Vigoroso</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta79 Contenedor" ID="btnVigoroso" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta79 Contenedor" ID="btnVigoroso2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label74" name="" runat="server">El robusto y fuerte</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label173" runat="server" title="El que busca y necesita del compañerismo de otros">Sociable</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta80 Contenedor" ID="btnSociable" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta80 Contenedor" ID="btnSociable2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--                            <td>
                                <label id="Label75" name="" runat="server">El que busca y necesita del compañerismo de otros</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>
                        <%--21--%>

                        <tr>
                            <td class="DescripcionStyle">21</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>


                            <td>
                                <label id="Label174" runat="server" title="Que tiene inclinación para hablar mucho">Parlanchín</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta81 Contenedor" ID="btnParlanchin" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta81 Contenedor" ID="btnParlanchin2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label76" name="nbPersuasivo" runat="server">Que tiene inclinación para hablar mucho</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label175" runat="server" title="El que ejerce una influencia regulante entre otros">Controlado</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta82 Contenedor" ID="btnControlado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta82 Contenedor" ID="btnControlado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label77" name="nbPersuasivo" runat="server">El que ejerce una influencia regulante entre otros</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label176" runat="server" title="El que se mantiene en una trayectoria de uso general">Convencional</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta83 Contenedor" ID="btnConvencional" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta83 Contenedor" ID="btnConvencional2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label78" name="" runat="server">El que se mantiene en una trayectoria de uso general</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label177" runat="server" title="El resuelto, determinado o incuestionable">Decisivo</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta84 Contenedor" ID="btnDecisivo" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta84 Contenedor" ID="btnDecisivo2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label79" name="" runat="server">El resuelto, determinado o incuestionable</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>
                        <%--22--%>

                        <tr>


                            <td class="DescripcionStyle">22</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label178" runat="server" title="El que se limita, restringe o controla ante los demás">Cohibido</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta85 Contenedor" ID="btnCohibido" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta85 Contenedor" ID="btnCohibido2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label80" name="nbPersuasivo" runat="server">El que se limita, restringe o controla ante los demás</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label179" runat="server" title="Matemático, preciso, calculador">Exacto</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta86 Contenedor" ID="btnExacto" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta86 Contenedor" ID="btnExacto2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <%--label id="Label81" name="nbPersuasivo" runat="server">
                                    Matemático, preciso, calculador
                                </label>--%>
                            </td>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label180" runat="server" title="El libre, dadivoso">Franco</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta87 Contenedor" ID="btnFranco" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta87 Contenedor" ID="btnFranco2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label82" name="" runat="server">El libre, dadivoso</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label181" runat="server" title="Sociable, amigable">Buen compañero</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta88 Contenedor" ID="btnBuencompaniero" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta88 Contenedor" ID="btnBuencompaniero2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label83" name="" runat="server">Sociable, amigable</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>
                        <%--23--%>


                        <tr>
                            <td class="DescripcionStyle">23</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label182" runat="server" title="El que se caracteriza por el buen tacto en su trato con la gente">Diplomático</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta89 Contenedor" ID="btnDiplomatico" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta89 Contenedor" ID="btnDiplomatico2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label84" name="nbPersuasivo" runat="server">El que se caracteriza por el buen tacto en su trato con la gente</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label183" runat="server" title="El arrogante e insolente. No el osado y atrevido">Audaz</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta90 Contenedor" ID="btnAudaz" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta90 Contenedor" ID="btnAudaz2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--   <td>
                                <label id="Label85" name="nbPersuasivo" runat="server">El arrogante e insolente. No el osado y atrevido</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label184" runat="server" title="El elegante en su manera de ser">Refinado</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta91 Contenedor" ID="btnRefinado" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta91 Contenedor" ID="btnRefinado2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--           <td>
                                <label id="Label86" name="" runat="server">El elegante en su manera de ser</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label185" runat="server" title="El que se conforma con los requerimientos del otro. No el presumido o el fatuo">Satisfecho</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta92 Contenedor" ID="btnSatisfecho" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta92 Contenedor" ID="btnSatisfecho2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--  <td>
                                <label id="Label87" name="" runat="server">El que se conforma con los requerimientos del otro. No el presumido o el fatuo</label>
                            </td>--%>
                        </tr>

                        <tr>
                            <td>
                                <div style="clear: both; height: 20px;"></div>
                            </td>
                        </tr>
                        <%--24--%>

                        <tr>
                            <td class="DescripcionStyle">24</td>
                            <td class="CenterDiv">M</td>
                            <td class="CenterDiv">L</td>
                            <%--<td class="DescripcionStyle">Descripción</td>--%>
                        </tr>
                        <tr>

                            <td>
                                <label id="Label186" runat="server" title="Emprendedor, de índole bulliciosa">Inquieto</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta93 Contenedor" ID="btnInquieto" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta93 Contenedor" ID="btnInquieto2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--<td>
                                <label id="Label88" name="nbPersuasivo" runat="server">Emprendedor, de índole bulliciosa</label>
                            </td>--%>
                        </tr>


                        <tr>

                            <td>
                                <label id="Label187" runat="server" title="Muy conocido o extendido en una colectividad">Popular</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta94 Contenedor" ID="btnPopular" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta94 Contenedor" ID="btnPopular2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%-- <td>
                                <label id="Label89" name="nbPersuasivo" runat="server">Muy conocido o extendido en una colectividad</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label188" runat="server" title="Considerado y amable con las personas que viven cerca">Buen vecino</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta95 Contenedor" ID="btnBuenvecino" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta95 Contenedor" ID="btnBuenvecino2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--       <td>
                                <label id="Label90" name="" runat="server">Considerado y amable con las personas que viven cerca</label>
                            </td>--%>
                        </tr>

                        <tr>

                            <td>
                                <label id="Label189" runat="server" title="Manifestación externa de los sentimientos y fervor religiosos">Devoto</label></td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta96 Contenedor" ID="btnDevoto" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24M"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <td>
                                <div class="CenterDiv">
                                    <telerik:RadButton CssClass="GrupoPregunta96 Contenedor" ID="btnDevoto2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24L"
                                        Text="" Skin="Metro" OnClientClicking="valueChanged">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                            <%--                            <td>
                                <label id="Label91" name="" runat="server">Manifestación externa de los sentimientos y fervor religiosos</label>
                            </td>--%>
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

        <div class="ctrlBasico" >
            <telerik:RadButton ID="btnImpresionPrueba" Visible="false" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false"></telerik:RadButton>
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
