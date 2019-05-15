<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaEstiloDePensamientoManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaEstiloDePensamientoManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style id="MyCss" type="text/css">
                 
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

        .ctrlBasico {
            float: left;
            padding: 5px;
        }

        .lblBasico {
            padding-right:10px;
            width:30px;
            text-align: right;
        }
        .divBasico {
            padding-right:20px;
        }
 
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            vPruebaEstatus = "";
           
            function close_window(sender, args) {
                if (vPruebaEstatus != "TERMINADA") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var btn = $find("<%=btnTerminar.ClientID%>");
                            btn.click();
                        }
                    });
                    var text = "¿Estas seguro que deseas terminar tu prueba?";
                    radconfirm(text, callBackFunction, 400, 150, null, "");
                    args.set_cancel(true);
                }
                else {
                    //window.close();
                    GetRadWindow().close();
                }
            }

            function WinClose(sender, args) {
                vPruebaEstatus = "TERMINADA";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Cuando esté listo para pasar a la siguiente prueba, por favor haga clic en el botón 'Siguiente' más abajo <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                //window.close();
                GetRadWindow().close();
            }
            //EN PROCESO PARA LA DETECCION DEL 3 EN LOS SEGMENTOS
            function valueChanged(sender, args) {

                //var vOldValue = args.get_oldValue();
                //var vNewValue = args.get_newValue();

                //var vId = sender.get_id();
                //var vClassName = document.getElementById(vId).className;
                //var x = document.getElementsByClassName(vClassName);

                //var i = 0;
                //for (i = 0; i < x.length; i++) {
                //    if (x[i].value == vNewValue) {
                //        x[i].value = vOldValue;
                //        break;
                //    }
                //}
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

        </script>
    </telerik:RadCodeBlock>
    <label name="" id="lbltitulo" class="labelTitulo">Estilo de pensamiento</label>

    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
    <%--        <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px;">
                            <label id="Label1" runat="server">El siguiente cuestionario está diseñado para conocer tus preferencias en el estilo de pensamiento, aprendizaje y comunicación.  Esta no es una prueba de inteligencia, no hay respuestas buenas o malas. Por favor contesta todas las preguntas utilizando la siguiente escala:<br />
                            5 - Me gusta mucho o me describe muy bien
                            <br />
                            4 - Me gusta o me describe relativamente bien
                            <br />
                            3 - Me gusta o me describe en forma regular
                            <br />
                            2 - Me disgusta o no me describe
                            <br />
                            1 - Me disgusta o no se aplica a mi
                            <br />
                            Por favor no dejes preguntas sin contestar.</label> 																														
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>--%>
            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <div class="ctrlBasico" style="width: 100%; padding-left:10px;">
                    <div class="ctrlBasico" style="width: 100%;">
                        <label name="" style="font-weight: bold;">Grupo 1</label>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">1:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg1Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">2:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg1Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>

                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">3:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg1Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">4:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg1Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">5:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg2Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">6:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg2Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">7:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg2Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">8:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg2Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">9:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg3Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">10</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg3Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">11:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg3Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">12:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg3Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">13:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg4Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">14:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg4Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">15:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg4Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">16:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg4Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">17:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg5Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">18:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg5Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">19:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg5Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">20:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="radTxtPreg5Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <label name="" style="font-weight: bold;">Grupo 2</label>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">1:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg6Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">2:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg6Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">3:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg6Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">4:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg6Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">5:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg7Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">6:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg7Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">7:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg7Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">8:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg7Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">9:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg8Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">10:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg8Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">11:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg8Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">12:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg8Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">13:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg9Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">14:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg9Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">15:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg9Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">16:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg9Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">17:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg10Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">18:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg10Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">19:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg10Resp3" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">20:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo2" runat="server" ID="radTxtPreg10Resp4" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
               
                    <div class="ctrlBasico" style="width: 100%;">
                        <label name="" style="font-weight: bold;">Grupo 3</label>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">1:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg11Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">2:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg11Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">3:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg12Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">4:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg12Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">5:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg13Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">6:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg13Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">7:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg14Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">8:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg14Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">9:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg15Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">10:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg15Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">11:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg16Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">12:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg16Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">13:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg17Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">14:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg17Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">15:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg18Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">16:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg18Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">17:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg19Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">18:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg19Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico" style="width: 100%;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">19:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg20Resp1" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">20:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo3" runat="server" ID="radTxtPreg20Resp2" Width="70px" MinValue="0" MaxValue="5"  NumberFormat-DecimalDigits="0" Style="text-align: center" AllowOutOfRangeAutoCorrect="false">
                                <ClientEvents OnValueChanging="valueChanged" />
                                
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    
    <div class="DivBtnTerminarDerecha">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
