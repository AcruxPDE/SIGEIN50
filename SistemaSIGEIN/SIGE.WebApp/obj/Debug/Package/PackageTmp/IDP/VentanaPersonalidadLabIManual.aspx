<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaPersonalidadLabIManual.aspx.cs" Inherits="SIGE.WebApp.IDP.CapManPersonalidadLabI" %>

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

        /* LA MODAL QUE SE DESPLAEGARA  EN LAS PRUEBAS: NOTA. SE EXTENDIO ESTA CLASE CSS */
        .TelerikModalOverlay {
            opacity: 1 !important;
            background-color: #000 !important;
        }


        .ctrlRow {
            float: left;
            padding: 5px;
        }

        label {
            font-weight: bold !important;
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
            //window.onload = function (sender, args) {
            //    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
            //        if (shouldSubmit) {
            //        }
            //        else {
            //            GetRadWindow().close();
            //        }
            //    });
            //    var text = 'Las palabras descriptivas siguientes se encuentran agrupadas en series de cuatro.' +
            //    '<br />' +
            //    'Examina las palabras de cada serie y marca la opción bajo la columna M que mejor  te describa.' +
            //    '<br />' +
            //    'Marca la opción bajo la columna L que menos te describa.' +
            //    '<br />' +
            //    'Asegúrate de marcar solamente una palabra bajo M y solamente una palabra bajo L en cada serie.';
            //    radconfirm(JustificarTexto(text), callBackFunction, 400, 290, null, "Personalidad laboral I");
            //};

            function close_window(sender, args) {
                if (vPruebaEstatus != "TERMINADA") {
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
                vPruebaEstatus = "TERMINADA";
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


            function ValidarGrupo(sender, args) {
                var vId = sender._clientID;
                var vClassName = sender._element.className;
                vClassName = vClassName.replace("riError", "").replace("riHover", "");
                var x = document.getElementsByClassName(vClassName);
                var contador = 0;
                var i = 0;
                if (x.length >= 2) {
                    if (x[0].value != "" || x[1].value != "") {
                        if (x[0].value == x[1].value) {
                            var NmtxtBox = document.getElementById(vId);
                            NmtxtBox.focus();
                            NmtxtBox.style.borderColor = 'red';
                            NmtxtBox.style.borderWidth = '1px';
                            NmtxtBox.value = "";
                        }
                    }
                }
            }


        </script>
    </telerik:RadCodeBlock>
    <label name="" id="lbltitulo" class="labelTitulo">Personalidad laboral I</label>

    <div style="height: calc(100% - 120px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

<%--            <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px; text-align: justify;">
                            <label id="Label26" runat="server">
                                Las palabras descriptivas siguientes se encuentran agrupadas en series de cuatro.
                            <br />
                                Examina las palabras de cada serie y marca la opción bajo la columna M que mejor  te describa.
                            <br />
                                Marca la opción bajo la columna L que menos te describa.
                            <br />
                                Asegúrate de marcar solamente una palabra bajo M y solamente una palabra bajo L en cada serie.</label>
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>--%>


            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <table style="width: 96%; margin-left: 2%; margin-right: 2%;">
                    <thead>
                        <tr>
                            <td width="10%"></td>
                            <td width="40%"></td>
                            <td width="10%"></td>
                            <td width="40%"></td>
                        </tr>
                    </thead>
                    <tbody>

                        <tr>
                            <td></td>
                            <td>
                                <label name="" runat="server" id="Label2" style="text-align: center;">M / L</label>
                            </td>
                            <td></td>
                            <td>
                                <label name="" runat="server" id="Label5" style="text-align: center;">M / L</label>
                            </td>
                        </tr>
                        <tr>

                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="lblGrupo1" style="width: 70px;">Grupo 01:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta01" runat="server" ID="radTxtPreg1Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta01" runat="server" ID="radTxtPreg1Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label1" style="width: 70px;">Grupo 07:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta07" runat="server" ID="radTxtPreg7Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta07" runat="server" ID="radTxtPreg7Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>
                        <%--Renglon 2--%>

                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label3" style="width: 70px;">Grupo 02:</label>
                                </div>
                            </td>


                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta02" runat="server" ID="radTxtPreg2Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta02" runat="server" ID="radTxtPreg2Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>


                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label4" style="width: 70px;">Grupo 08:</label>
                                </div>
                            </td>
                            <td>


                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta08" runat="server" ID="radTxtPreg8Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta08" runat="server" ID="radTxtPreg8Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>
                        <%--Renglon 2--%>

                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label25" style="width: 70px;">Grupo 03:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta03" runat="server" ID="radTxtPreg3Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta03" runat="server" ID="radTxtPreg3Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                            </td>

                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label6" style="width: 70px;">Grupo 09:</label>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta09" runat="server" ID="radTxtPreg9Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta09" runat="server" ID="radTxtPreg9Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>


                        </tr>

                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label7" style="width: 70px;">Grupo 04:</label>
                                </div>
                            </td>
                            <td>


                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta04" runat="server" ID="radTxtPreg4Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta04" runat="server" ID="radTxtPreg4Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                            </td>

                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label8" style="width: 70px;">Grupo 10:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta10" runat="server" ID="radTxtPreg10Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta10" runat="server" ID="radTxtPreg10Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label9" style="width: 70px;">Grupo 05:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta05" runat="server" ID="radTxtPreg5Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta05" runat="server" ID="radTxtPreg5Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label10" style="width: 70px;">Grupo 11:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta11" runat="server" ID="radTxtPreg11Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta11" runat="server" ID="radTxtPreg11Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label11" style="width: 70px;">Grupo 06:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta06" runat="server" ID="radTxtPreg6Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta06" runat="server" ID="radTxtPreg6Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label12" style="width: 70px;">Grupo 12:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta12" runat="server" ID="radTxtPreg12Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta12" runat="server" ID="radTxtPreg12Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <div style="clear: both; height: 30px;"></div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label13" style="width: 70px;">Grupo 13:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta13" runat="server" ID="radTxtPreg13Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta13" runat="server" ID="radTxtPreg13Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label14" style="width: 70px;">Grupo 19:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta19" runat="server" ID="radTxtPreg19Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta19" runat="server" ID="radTxtPreg19Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label15" style="width: 70px;">Grupo 14:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta14" runat="server" ID="radTxtPreg14Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta14" runat="server" ID="radTxtPreg14Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                            </td>

                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label16" style="width: 70px;">Grupo 20:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta20" runat="server" ID="radTxtPreg20Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta20" runat="server" ID="radTxtPreg20Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label17" style="width: 70px;">Grupo 15:</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta15" runat="server" ID="radTxtPreg15Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta15" runat="server" ID="radTxtPreg15Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label18" style="width: 70px;">Grupo 21:</label>
                                </div>
                            </td>
                            <td>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta21" runat="server" ID="radTxtPreg21Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta21" runat="server" ID="radTxtPreg21Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label19" style="width: 70px;">Grupo 16:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta16" runat="server" ID="radTxtPreg16Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta16" runat="server" ID="radTxtPreg16Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>

                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label20" style="width: 70px;">Grupo 22:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta22" runat="server" ID="radTxtPreg22Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta22" runat="server" ID="radTxtPreg22Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label21" style="width: 70px;">Grupo 17:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta17" runat="server" ID="radTxtPreg17Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta17" runat="server" ID="radTxtPreg17Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>


                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label22" style="width: 70px;">Grupo 23:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta23" runat="server" ID="radTxtPreg23Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta23" runat="server" ID="radTxtPreg23Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label name="" runat="server" id="Label23" style="width: 70px;">Grupo 18:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta18" runat="server" ID="radTxtPreg18Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta18" runat="server" ID="radTxtPreg18Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico" style="margin-left: 5%;">
                                    <label name="" runat="server" id="Label24" style="width: 70px;">Grupo 24:</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta24" runat="server" ID="radTxtPreg24Resp1" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox CssClass="GrpPregunta24" runat="server" ID="radTxtPreg24Resp2" Width="70px" MinValue="0" MaxValue="4" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="ValidarGrupo">
                                    </telerik:RadNumericTextBox>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="DivBtnTerminarDerecha">
            <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
