<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaTIVAManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaTIVAManual" %>

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

            function close_window(sender, args) {
                if (vPruebaEstatus != "Terminado") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
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
                var vNewValue = args.get_newValue();

                if (vNewValue != 'a' && vNewValue != 'b' && vNewValue != 'c' && vNewValue != 'd') {
                    sender.set_value("");
                }
            }

        </script>
    </telerik:RadCodeBlock>
    <label name="" id="lbltitulo" class="labelTitulo">TIVA</label>

    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

           <%-- <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="280">
                        <p style="margin: 10px;">
                           <label id="Label1" runat="server"> Lee con atención las siguientes preguntas, elige entre las posibles respuestas aquella que más se aplica a tu forma de actuar. Cada pregunta cuenta con un tiempo límite para ser contestada, por lo que te sugerimos no detenerte demasiado y seleccionar tu respuesta con rapidez.</label>
                        </p>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="RadButton1" runat="server" Text="Aceptar" AutoPostBack="true"></telerik:RadButton>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>--%>

            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <%-- Contenedor General --%>
                <div class="ctrlBasico" style="width: 100%;">

                    <%-- Contenedor de titulo --%>
                    <div>
                        <label>Preguntas</label>
                    </div>

                    <%-- Primer fila --%>
                    <div class="ctrlBasico" style="width: 90%; margin-left: 30px;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">1:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp1" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">2:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp2" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">3:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp3" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">4:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp4" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">5:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp5" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">6:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp6" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>
                    </div>

                    <%-- Segunda fila --%>
                    <div class="ctrlBasico" style="width: 90%; margin-left: 30px;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">7:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp7" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">8:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp8" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">9:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp9" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">10:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp10" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">11:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp11" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">12:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp12" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>
                    </div>

                    <%-- Tercer fila --%>
                    <div class="ctrlBasico" style="width: 90%; margin-left: 30px;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">13:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp13" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">14:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp14" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">15:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp15" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">16:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp16" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">17:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp17" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">18:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp18" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>
                    </div>


                    <%-- Cuarta fila --%>
                    <div class="ctrlBasico" style="width: 90%; margin-left: 30px;">
                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">19:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp19" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
                        </div>

                        <div class="ctrlBasico divBasico">
                            <label class="lblBasico">20:</label>
                            <telerik:RadTextBox runat="server" ID="txtResp20" Width="70px" Style="text-align: center">
                                <ClientEvents OnValueChanged="valueChanged" />
                            </telerik:RadTextBox>
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
