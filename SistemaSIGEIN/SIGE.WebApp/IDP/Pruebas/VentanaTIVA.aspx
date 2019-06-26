<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaTIVA.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaTIVA" %>

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

        Table td .subrayado {
            max-width: 600px;
            border-bottom: 1px dotted gray;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPruebas" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
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
            var c = "";
            var a = new Array();
            var vPruebaEstatus = "";
            window.onload = function (sender, args) {
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var ajaxManager = $find("<%=RadAjaxManager1.ClientID%>");
                            ajaxManager.ajaxRequest(null);
                            var segundos = '<%=this.vTiempoPrueba%>';
                            if (segundos <= 0) {
                                // var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no se lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
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

                            //window.close();
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "<label><b>Instrucciones:</b><br/>Lee con atención las siguientes preguntas, elige entre las posibles respuestas aquella que más se aplica a tu forma de actuar. Cada pregunta cuenta con un tiempo límite para ser contestada, por lo que te sugerimos no detenerte demasiado y seleccionar tu respuesta con rapidez. <br /><br /> Este test no tiene preguntas correctas o incorrectas, por lo que puedes sentirte libre de contestar de manera totalmente honesta y objetiva. Sin embargo, este test cuenta con un sistema para invalidarlo si no está de acuerdo a los demás resultados de tu batería, te suplicamos ser honesto en tus respuestas.</label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "TIVA");
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
                    radconfirm(text, callBackFunction, 400, 150, null, "");
                    args.set_cancel(true);

                }
                else {
                    // window.close();
                    window.location = "Default.aspx?ty=sig";
                }
            }

            function WinClose(sender, args) {
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

            function addGrupoContestado(valor) {
                if (a.indexOf(valor) == -1 || a.length == 0) {
                    a.push(valor);
                    console.info(a);
                }
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
                        GrupoNoContestado.style.borderWidth = '1px';
                        var flag = false;
                        break;
                    }
                }
                return flag;
            }


            function OpenReport() {
                var vURL = "ReporteadorPruebasIDP.aspx";
                var vTitulo = "Impresión TIVA";

                var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClToken %>';



                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=TIVA";
                var win = window.open(vURL, '_blank');
                win.focus();
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
      <label style="font-size: 21px;">TIVA</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

            <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="150">
                        <table>
                            <tr>
                                <td style="width: 10px;">&nbsp;</td>
                                <td style="background-color: white; padding: 5px;">
                                    <label class="JustificarTexto">
                                        Lee con atención las siguientes preguntas, elige entre las posibles respuestas aquella que 
                          más se aplica a tu forma de actuar. Cada pregunta cuenta con un tiempo límite para ser contestada, 
                          por lo que te sugerimos no detenerte demasiado y seleccionar tu respuesta con rapidez.
                                    <br />
                                        <br />
                                        Este test no tiene preguntas correctas o incorrectas, por lo que puedes sentirte libre de contestar 
                          de manera totalmente honesta y objetiva. Sin embargo, este test cuenta con un sistema para invalidarlo si
                           no está de acuerdo a los demás resultados de tu batería, te suplicamos ser honesto en tus respuestas.</label>
                                </td>
                            </tr>
                        </table>


                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <table style="width: 96%; margin-left: 2%; margin-right: 2%;">
                    <thead>
                        <tr>
                            <td width="100%"></td>
                        </tr>
                    </thead>
                    <tbody>
                        <%--1--%>

                        <tr>
                            <td>
                                <label id="Label4" name="nbPersuasivo" runat="server" style="font-weight: bold;">1. Me he dado cuenta que entre lo que pienso que debo hacer y lo que hago, generalmente pasa que:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="a. Pocas veces hay diferencias." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="b. Siempre hago lo que pienso que debo hacer." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="c. No todo lo que pienso es igual a lo que hago." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="d. A la hora de hacer lo que tenía pensado, generalmente decido hacer otra cosa." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--2--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label1" name="nbPersuasivo" runat="server">2. Supón que eres el jefe de un equipo. Recibes el crédito por algún trabajo realizado por alguno de tus subordinados, tú:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                        Text="a.	Agradeces el reconocimiento pero en ese momento aclaras que ese trabajo lo realizó alguien de tu equipo y le das a él el reconocimiento. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                        Text="b.	Insistes en que los trabajos tengan el nombre de las personas responsables de ellos. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                        Text="c.	Es normal. Como jefe, te llevas el crédito cuando las cosas salen bien y se te culpa cuando salen mal. Es la esencia del puesto." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                        Text="d.	Te llevas el crédito ante los demás pero hacia el interior del equipo te aseguras de reconocer a la persona responsable." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--3--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label2" name="nbPersuasivo" runat="server">3.	Debo estar puntual en una reunión de trabajo, pero al llegar al lugar con el tiempo justo, no hay espacio disponible de estacionamiento. Los únicos lugares son los reservados para personas con alguna discapacidad y veo que uno de mis compañeros se estaciona ahí, yo:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                        Text="a.	Busco otro lugar aunque esto signifique llegar un poco tarde a la reunión." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                        Text="b.	Le señalo a mi compañero que ese lugar debe respetarse y busco otra opción de estacionamiento." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                        Text="c.	El trabajo es primero, así que me estaciono en el lugar. De todas formas, hay más lugares reservados." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                        Text="d.	Sé que llegaré un poco tarde pero busco un espacio más lejano y aviso al responsable del estacionamiento sobre el mal uso que mi compañero hizo de éste." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--4--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label3" name="nbPersuasivo" runat="server">4.	Imagina que estás en el cine y te encuentras dinero en el mostrador de la dulcería, decides: </label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                        Text="a.	Espero a que alguien lo reclame y se lo entrego." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                        Text="b.	Espero un tiempo razonable para que lo reclamen y si esto no sucede, lo considero como mío." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                        Text="c.	Acudo a la gerencia del cine y lo entrego para que lo devuelvan a su propietario." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                        Text="d.	No lo entrego para que la persona aprenda a tener más cuidado." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--5--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label5" name="nbPersuasivo" runat="server">5.	La política de la empresa es no contratar familiares, pero un hermano mío necesita trabajo y hay una vacante en un departamento que no tiene relación con el mío, por lo que es poco probable que se detecte el parentesco, yo: </label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                        Text="a.	Hago todo lo posible porque obtenga el trabajo. Hay que ver por el bien de la familia. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                        Text="b.	Informo a mi hermano de la vacante y dejo que él haga el proceso por su cuenta sin involucrarme. Tendrá igual oportunidad que cualquiera." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                        Text="c.	No informo a mi hermano de la vacante debido a las políticas." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                        Text="d.	Hablo con mis jefes para exponerles la situación y pedirles su aprobación antes de tomar cualquier decisión al respecto. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--6--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label6" name="nbPersuasivo" runat="server">6.	Supón que de forma precipitada has divulgado algo malo sobre una persona y después te enteras que esto no era cierto, entonces tú:  </label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                        Text="a.	Te acercas a la persona, le dices lo que pasó, te disculpas y aclaras el malentendido con aquellos con quienes compartiste el chisme.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                        Text="b.	No haces nada al respecto y te propones no volver a hablar a la ligera la próxima vez." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                        Text="c.	Les mencionas a las personas con las que compartiste el chisme que al parecer la información era sólo un rumor." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                        Text="d.	Decides que si el afectado te reclama, le dirás el nombre de la persona que te proporcionó la información.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--7--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label7" name="nbPersuasivo" runat="server">7.	Soy un excelente trabajador en una fábrica, en ausencia de mi jefe inmediato:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                        Text="a.	Sigo cumpliendo con mi horario como si mi jefe estuviera presente." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                        Text="b.	Solamente me retrasaría un poco ante una emergencia. No quiero perder mi premio de puntualidad." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                        Text="c.	Sé que para mi jefe lo fundamental son los resultados, por lo que cuido éstos y  no tanto el horario." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                        Text="d.	Es razonable ser un poco más flexible en mi horario, si no hay mucho trabajo.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--8--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label8" name="nbPersuasivo" runat="server">8.	Llegas al banco a realizar un movimiento y te encuentras con una enorme fila. El gerente del banco es amigo tuyo. Tú tienes prisa por regresar a tu trabajo, entonces: </label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                        Text="a.	Vas directo con tu amigo y le pides que realice tu movimiento." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                        Text="b.	Vas a saludarlo y haces todo lo posible para que se ofrezca a realizar el movimiento por ti." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                        Text="c.	Te formas en la fila y esperas tu turno, aunque eso signifique perder más tiempo en el banco." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                        Text="d.	Tomas tu lugar en la fila y sólo que tu amigo te vea y lo ofrezca, aceptas que realice tu movimiento." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--9--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label9" name="nbPersuasivo" runat="server">9.	A lo largo de mi vida he comprobado que mi opinión sobre la frase “el fin justifica los medios” es: </label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                        Text="a.	Desafortunadamente aplica bien." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                        Text="b.	Estoy totalmente en desacuerdo con esta frase." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                        Text="c.	A veces cierta, pero no necesariamente correcta." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                        Text="d.	La mayoría dirá que incorrecta, pero todos la aplican en algún momento de su vida.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--10--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label10" name="nbPersuasivo" runat="server">10.	Un buen político debería:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                        Text="a.	Recurrir a toda clase de métodos para lograr un puesto de poder, siempre y cuando su objetivo final sea ayudar a la gente con su trabajo. 	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                        Text="b.	Conducirse con rectitud aunque esto pueda implicar no llegar al poder. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                        Text="c.	Cumplir de la mejor manera posible sin involucrarse demasiado. La política es un trabajo como cualquier otro." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                        Text="d.	Adaptarse al entorno cediendo en aquellas cosas poco importantes, mientras esto le permita lograr beneficios mayores. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--11--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label11" name="nbPersuasivo" runat="server">11.	Vas a vender un producto que tiene ventajas y desventajas. Ante el cliente tú:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                        Text="a.	Explicas las ventajas y desventajas reales del producto sin exagerar. Si te ganas la credibilidad del cliente, a la larga haces más ventas. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                        Text="b.	Presentas tu producto como el mejor ya que estás dando la cara por él. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                        Text="c.	Resaltas las ventajas del producto pero contestas con sinceridad cuando el cliente te pregunta sobre sus desventajas." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                        Text="d.	Hablas solamente sobre las ventajas del producto, tú obligación como vendedor es lograr ventas." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--12--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label12" name="nbPersuasivo" runat="server">12.	Considero que las personas que se dedican a pedir limosna como negocio:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                        Text="a.	Son personas que engañan a la gente y merecen un castigo.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                        Text="b.	Son un mal necesario. En la actualidad hay muchas personas que tienen que recurrir a eso por la falta de oportunidades.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                        Text="c.	Son muy ingeniosos. Se requiere cierta habilidad y tiempo para que algo así funcione.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                        Text="d.	Abusan de la buena voluntad de las personas por no buscar otra manera de ganarse la vida.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--13--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label13" name="nbPersuasivo" runat="server">13.	Soy el encargado de compras dentro de mi empresa, por lo que esto se presta a que los proveedores me hagan ofrecimientos todo el tiempo para verse favorecidos,  yo:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                        Text="a.	Me apego a las políticas de la empresa rechazando cualquier ofrecimiento por parte de los proveedores." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                        Text="b.	Mantengo una relación cordial con los proveedores recibiendo algunas de sus atenciones pero tomando las decisiones correctas para la empresa." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                        Text="c.	Es conocido que los proveedores no se atreverían a insinuarme algún arreglo personal." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                        Text="d.	Trato de que los intereses de la empresa se vean favorecidos por aquellos proveedores con los que tengo una mejor relación, a final de cuentas ellos me dan a mí  un mejor servicio." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--14--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label14" name="nbPersuasivo" runat="server">14.	Todos hemos llegado a mentir.  En mi caso: </label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                        Text="a.	Cuando he tenido que mentir, después me siento muy decepcionado de mí mismo.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                        Text="b.	Miento sólo cuando es necesario." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                        Text="c.	Considero que la mentira es inmoral y me siento muy mal cuando miento." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                        Text="d.	Cuando miento,  siempre es para lograr un fin mejor." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--15--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label15" name="nbPersuasivo" runat="server">15.	Descubres un defecto técnico en un producto que ya ha sido entregado al cliente, tú:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                        Text="a.	Sin mencionar el defecto, le das consejos para evitar, en la medida de lo posible, que éste se presente. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                        Text="b.	Dejas las cosas tal y como están, el cliente parece estar usando el producto sin problemas. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                        Text="c.	Le cuentas lo que sucede y reemplazas el producto gratuitamente. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                        Text="d.	Te anticipas a posibles reclamaciones y proteges a la empresa recabando firma de conformidad del cliente mientras el producto no presenta problemas. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--16--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label16" name="nbPersuasivo" runat="server">16.	Te enteras que un familiar acaba de  obtener un alto puesto dentro de cierta organización, tú:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                        Text="a.	Aprovechas su posición para solicitarle su ayuda y conseguir un buen puesto también." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                        Text="b.	Le envías tu currículum con la esperanza de que te tome en cuenta para alguna oportunidad.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                        Text="c.	Lo felicitas por su posición y le comentas que, siempre y cuando convenga a sus intereses, tú estarías interesado en colaborar con él." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                        Text="d.	No haces nada al respecto. Él conoce tus habilidades, si te necesita, te llamará. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--17--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label17" name="nbPersuasivo" runat="server">17.	Hay elecciones municipales y yo me encuentro fuera de mi ciudad visitando a unos amigos:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                        Text="a.	Hago todo lo posible por regresar a votar, ojalá llegue antes de que cierren las casillas." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                        Text="b.	Tomo mis precauciones y regreso a tiempo para emitir mi voto." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                        Text="c.	No creo que mi voto haga la diferencia, por lo que prefiero quedarme donde estoy y no presentarme." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                        Text="d.	Si me es posible, regreso a mi ciudad para votar." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>

                        <%--18--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label18" name="nbPersuasivo" runat="server">18.	Me encuentro de paso por una ciudad que no es la mía y estoy por recibir una infracción que me ocasionará un retraso.</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                        Text="a.	Trato de llegar a un acuerdo económico con el oficial, a fin de cuentas es como si realizara el pago, pero sin perder mi tiempo de viaje." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                        Text="b.	No me queda más remedio que seguir el trámite como se indica, aunque mi viaje se retrase." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                        Text="c.	Como no me retiraron ningún documento, evito pagar la infracción, de todas maneras, no es en mi ciudad.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                        Text="d.	Solamente doy algo, si el oficial me lo pide." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>



                        <%--19--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label19" name="nbPersuasivo" runat="server">19.	Te das cuenta que uno de tus compañeros de trabajo usó el auto de la empresa para unas vacaciones de fin de semana, tú:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                        Text="a.	Tengo un compromiso con la empresa que tengo que respetar, por lo que le informo a mi supervisor. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                        Text="b.	Doy una oportunidad más, si se repite, lo denunciaré. " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg19Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                        Text="c.	Lo denuncio de manera anónima.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg19Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                        Text="d.	Procuro hablar con él en privado y hacerle notar su falta." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>


                        <%--20--%>
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-weight: bold;" id="Label20" name="nbPersuasivo" runat="server">20.	Voy caminando por la calle y observo que una persona tira basura delante de mí, yo:</label>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                        Text="a.	Lo alcanzo para decirle que no es correcto tirar basura y le indico dónde se encuentra el basurero.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                        Text="b.	Recojo su basura y la tiro en el basurero más cercano.	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg20Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                        Text="c.	De haber una autoridad cerca, le aviso de la falta cometida por esta persona." Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                <div style="clear: both;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPreg20Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                        Text="d.	Sigo mi camino. Siempre habrá personas como éstas y no está en mis manos solucionarlo. 	" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor">
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
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminarPrueba" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminarBateria" runat="server" Text="Eliminar batería" AutoPostBack="true" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminarBateria_Click"></telerik:RadButton>
         </div>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>

