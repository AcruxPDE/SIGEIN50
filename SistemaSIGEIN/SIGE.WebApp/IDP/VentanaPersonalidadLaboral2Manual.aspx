<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaPersonalidadLaboral2Manual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaPersonalidadLaboral2Manual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

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

        .lblBasico {
            padding-right: 10px;
            width: 30px;
            text-align: right;
        }

        .divBasico {
            padding-right: 20px;
        }


        /* LA MODAL QUE SE DESPLAEGARA  EN LAS PRUEBAS: NOTA. SE EXTENDIO ESTA CLASE CSS */
        .TelerikModalOverlay {
            opacity: 1 !important;
            background-color: #000 !important;
        }

        Table td {
            padding: 3px 0px 3px 0px;
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
            var vPruebaEstatus = "";

            /*window.onload = function (sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        var segundos = '<=this.vTiempoPrueba>';
                        if (segundos <= 0) {
                            var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Cuando esté listo para pasar a la siguiente prueba, por favor haga clic en el botón 'Siguiente' más abajo <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                            oWnd.add_close(CloseTest);
                        }
                        else {
                            var display = document.querySelector('#time');
                            Cronometro(segundos, display);
                        }
                    }
                    else {

                        window.close();
                    }
                });
                var text = "Este no es un test con respuestas correctas o incorrectas. Es un cuestionario que te permite describir tu estilo de vida principal y secundario, con el fin de identificar los modos productivos y antiproductivos, en que utilizas tus fuerzas. Encontraras en el cuestionario enunciados descriptivos cada uno seguido por cuatro terminaciones posibles.  En los espacios en blanco a la derecha de cada terminación, coloca los números 4, 3, 2 y 1 de acuerdo a cuál es la terminación que más se te asemeja (4) y cuál es la que sientes que se te asemeja menos (1).";
                radconfirm(text, callBackFunction, 400, 350, null, "Ayuda General");

            };*/

            function close_window(sender, args) {

                if (vPruebaEstatus != "Terminado") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            //window.close();
                            var btn = $find("<%=btnTerminar.ClientID%>");
                            btn.click();
                        }
                    });

                    var text = "¿Estas seguro que deseas terminar tu prueba?";
                    radconfirm(text, callBackFunction, 400, 150, null, "Aviso");
                    args.set_cancel(true);
                }
                else {
                    //window.close();
                    GetRadWindow().close();

                }
            }

            function WinClose(sender, args) {
                vPruebaEstatus = "Terminado";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Cuando esté listo para pasar a la siguiente prueba, por favor haga clic en el botón 'Siguiente' más abajo <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "Aviso");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                //window.close();
                GetRadWindow().close();

            }

            function valueChanged(sender, args) {
            /*    var vOldValue = args.get_oldValue();
                var vNewValue = args.get_newValue();

                var vId = sender.get_id();
                var vClassName = document.getElementById(vId).className;
                var x = document.getElementsByClassName(vClassName);

                var i = 0;
                for (i = 0; i < x.length; i++) {
                    if (x[i].value == vNewValue) {
                        x[i].value = vOldValue;
                        break;
                    }
                }*/
            }


        </script>
    </telerik:RadCodeBlock>

    <label name="" id="lbltitulo" class="labelTitulo">Personalidad laboral II</label>

    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

         <%--   <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="200">

                        <p style="margin: 10px;">
                         <label id="Label1" runat="server">   Este no es un test con respuestas correctas o incorrectas. Es un cuestionario que te permite describir tu estilo de vida principal y secundario, con el fin de identificar los modos productivos y antiproductivos, en que utilizas tus fuerzas. Encontraras en el cuestionario enunciados descriptivos cada uno seguido por cuatro terminaciones posibles.  En los espacios en blanco a la derecha de cada terminación, coloca los números 4, 3, 2 y 1 de acuerdo a cuál es la terminación que más se te asemeja (4) y cuál es la que sientes que se te asemeja menos (1).</label>
                        </p>

                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>

            </telerik:RadPane>--%>

            <telerik:RadPane ID="radPanelPreguntas" runat="server">
                <%-- Contenedor general --%>
                <div class="ctrlBasico" style="width: 100%;">

                    <%-- Contenedor de titulo --%>
                    <div class="ctrlBasico" style="width: 80%; margin-left:20px;">
                        <label>Preguntas</label>
                    </div>

                    <%-- Primer fila --%>
                    <div class="ctrlBasico" style="width: 90%; margin-left:30px;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">A:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespA1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">B:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespB1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">C:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespC1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">D:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespD1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">E:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespE1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">F:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespF1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <%-- Segunda Fila --%>
                     <div class="ctrlBasico" style="width: 90%; margin-left:30px;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">G:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespG1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">H:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespH1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">I:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespI1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">J:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespJ1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">K:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespK1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">L:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespL1" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    
                    <%-- Tercer fila --%>
                    <div class="ctrlBasico" style="width: 90%;margin-left:30px; margin-top:10px;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">a:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespA2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">b:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespB2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">c:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespC2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">d:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespD2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">e:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespE2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">f:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespF2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <%-- Cuarta Fila --%>
                     <div class="ctrlBasico" style="width: 90%; margin-left:30px;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">g:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespG2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">h:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespH2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">i:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespI2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">j:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespJ2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">k:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespK2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
                                <ClientEvents OnValueChanging="valueChanged" />
                            </telerik:RadNumericTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">l:</label>
                            <telerik:RadNumericTextBox CssClass="SegmentoNo1" runat="server" ID="txtRespL2" Width="70px"  MinValue="3" MaxValue="12" NumberFormat-DecimalDigits="0" Style="text-align: center">
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
