<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAdaptacionMedioManual.aspx.cs" Inherits="SIGE.WebApp.IDP.Pruebas.VentanaAdaptacionMedio" %>

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

        Table td {
            padding: 3px 0px 3px 0px;
        }

        .imgCell {
            width: 12%;
            text-align: center;
            cursor: pointer;
        }

        .lblCell {
            width: 12%;
            text-align: center;
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
                    <telerik:AjaxUpdatedControl ControlID="lblFin" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="lblFin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblFin" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            var vPruebaEstatus = "";
            var NumFase = 0;
            var ResIndex;
            var blankImg = new Image;
            blankImg.src = "../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg";
            var ResultStr = "";
            var colores = [];
            var TarjetaInicio = 0;
            var TarjetaFin = 7;



            window.onload = function (sender, args) {
                //var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                //    if (shouldSubmit) {
                        GoFase();
                //    }
                //    else {
                //        GetRadWindow().close();
                //    }
                //});
                //var text = "Observa detenidamente estas ocho cartas y elije una a una la que por su color te llame más la atención, sin asociar los colores con otras cosas, tales como autos, ropa, etc. <br> Concéntrece en elegir la que más le atraiga visualmente, repitiendo la operación hasta agotar las cartas.";
                //radconfirm(JustificarTexto(text), callBackFunction, 250, 500, null, "Adaptación al medio");
            };


            function close_window(sender, args) {
                if (vPruebaEstatus != "Terminado") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            saveAnswers();
                        }
                    });

                    var text = "¿Estás seguro que deseas terminar tu prueba?";
                    radconfirm(text, callBackFunction, 400, 160, null, "Aviso");
                    args.set_cancel(true);
                }
                else {
                    GetRadWindow().close();
                }
            }

            function WinClose(sender, args) {
                vPruebaEstatus = "Terminado";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "Aviso");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
              //  GetRadWindow().close();

            }


            function ShuffleCards(segunda) {
                var ImgArr = new Array("Gris", "Azul", "Verde", "Rojo", "Amarillo", "Violeta", "Marron", "Negro");
                var DrawingHat = new Array(7);

                for (i = 0; i <= 7; ++i)
                    DrawingHat[i] = i;

                if (segunda) {
                    for (i = 0; i <= 7; ++i) {
                        var s1 = 0;
                        var s2 = 0;
                        while (s1 == s2) {
                            s1 = RandomInt(8) - 1;
                            s2 = RandomInt(8) - 1;
                        }
                        var T = DrawingHat[s1];
                        DrawingHat[s1] = DrawingHat[s2];
                        DrawingHat[s2] = T;
                    }
                }

                for (i = 0; i <= 7; ++i) {
                    var jIdx = DrawingHat[i];
                    var FName = "../Assets/images/PruebaAdaptacionMedio/Luscher" + ImgArr[jIdx] + jIdx + ".jpg";
                    document.getElementById("img" + (i + TarjetaInicio)).src = FName;
                    document.getElementById("img" + (i + TarjetaInicio)).alt = jIdx;
                    document.getElementById("ContentPlaceHolder1_ContentPlaceHolderContexto_lbl" + (i + TarjetaInicio)).innerHTML = jIdx;
                }

            }


            function ImgClick(pImg) {
                if (document.getElementById(pImg.id).src == blankImg.src)
                    return;
                document.getElementById(pImg.id).src = blankImg.src;
                colores.push(document.getElementById(pImg.id).alt);

                for (i = TarjetaInicio; i <= TarjetaFin; ++i) {
                    if (document.getElementById("ContentPlaceHolder1_ContentPlaceHolderContexto_lbl" + i).innerHTML == document.getElementById(pImg.id).alt) {
                        document.getElementById("ContentPlaceHolder1_ContentPlaceHolderContexto_lbl" + i).innerHTML = "";
                        break;
                    }
                }

                if (colores.length > TarjetaFin) {
                    var text = "Ahora vuelve a efectuar la selección, olvidando que anteriormente ya habías visto estos colores, por lo cual se te pide no tratar de repetir la selección anterior, ni tratar de no repetirla, así como no asociarla a objetos tales como autos, ropa, etc. <br> Concéntrece en elegir la que más le atraiga visualmente, repitiendo la operación hasta agotar las cartas.";
                    radconfirm(JustificarTexto(text), callBackFn, 500, 250, null, "Aviso");
                }
                else if (colores.length > 15) {
                    saveAnswers();
                }
            }

            function callBackFn(arg) {
                var multiPage = $find("<%=mpgActitudMentalI.ClientID %>");
                multiPage.set_selectedIndex(1);
                TarjetaInicio = 10;
                TarjetaFin = 17;
                ShuffleCards(true);
            }

            function saveAnswers() {
                var Respuesta = "";
                for (var i = 0; i < colores.length; i++)
                {
                    Respuesta = Respuesta + colores[i] + ",";
                }
                var hiddenInput = document.getElementById('<%=HiddenField1.ClientID %>');
                hiddenInput.value = Respuesta;
                vPruebaEstatus = "Terminado";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function GoFase() {
                TarjetaInicio = 0;
                ShuffleCards(false);
            }

            // ------------------------------------------------------------------------------
            function RandomInt(num) {
                var result = Math.floor((Math.random() * (num)) + 1);
                return result;
            }
            //------------------------------------------------------------------------
        </script>
    </telerik:RadCodeBlock>

     <label name="" id="lbltitulo" class="labelTitulo">Adaptación al medio</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
       <%--     <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="100">
                        <div style="margin: 10px;">
                            <p><label runat="server">Concéntrese en elegir la que más le atraiga visualmente, repitiendo la operación hasta agotar las cartas.</label> </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>

            </telerik:RadPane>--%>

            <telerik:RadPane ID="radPanelPreguntas" runat="server">

                <telerik:RadTabStrip ID="tbActitudMentalI" runat="server" SelectedIndex="0" MultiPageID="mpgActitudMentalI">
                    <Tabs>
                        <telerik:RadTab Text="1" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="2" Visible="false"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <div style="height: 5px"></div>
                <telerik:RadMultiPage ID="mpgActitudMentalI" runat="server" SelectedIndex="0" Height="200">
                    <telerik:RadPageView ID="RPView1" runat="server">
                        <div style="width: 97%; margin-left: 2%; margin-right: 2%;"   class="BorderRadioComponenteHTML">
                            <b>Primera fase de la prueba</b>
                                <br />
                                Observa detenidamente estas ocho cartas y elije una a una la que por su color te llame más la atención, sin asociar los colores con otras cosas, tales como autos, ropa, etc.<br />
                               Concéntrece en elegir la que más le atraiga visualmente, repitiendo la operación hasta agotar las cartas.
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <div style="width: 97%; margin-left: 2%; margin-right: 2%; background-color: gainsboro; border: solid 2px gray;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <%--<img id="img0" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />--%>
                                                <img id="img0" src="../Assets/images/PruebaAdaptacionMedio/LuscherGris0.jpg" onclick="ImgClick(this);" />
                                            </div>

                                            <label id="lbl0" name="" runat="server" style="text-align: center; width: 100%;">1</label>
                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <%--<img id="img1" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />--%>
                                                <img id="img1" src="../Assets/images/PruebaAdaptacionMedio/LuscherAzul1.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl1" name="" runat="server" style="width: 100%; text-align: center;">2</label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <%--<img id="img2" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />--%>
                                                <img id="img2" src="../Assets/images/PruebaAdaptacionMedio/LuscherVerde2.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl2" name="" runat="server" style="width: 100%; text-align: center;">3</label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <%--<img id="img3" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />--%>
                                                <img id="img3" src="../Assets/images/PruebaAdaptacionMedio/LuscherRojo3.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl3" name="" runat="server" style="width: 100%; text-align: center;">3</label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <%--<img id="img4" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />--%>
                                                <img id="img4" src="../Assets/images/PruebaAdaptacionMedio/LuscherAmarillo4.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl4" name="" runat="server" style="width: 100%; text-align: center;">4</label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <%--<img id="img5" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />--%>
                                                <img id="img5" src="../Assets/images/PruebaAdaptacionMedio/LuscherVioleta5.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl5" name="" runat="server" style="width: 100%; text-align: center;">5</label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <%--<img id="img6" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />--%>
                                                <img id="img6" src="../Assets/images/PruebaAdaptacionMedio/LuscherMarron6.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl6" name="" runat="server" style="width: 100%; text-align: center;">6</label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img7" src="../Assets/images/PruebaAdaptacionMedio/LuscherNegro7.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl7" name="" runat="server" style="width: 100%; text-align: center;">7</label>
                                        </div>
                                    </td>
                                </tr>
                               
                            </table>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView2" runat="server">
                        <div style="width: 97%; margin-left: 2%; margin-right: 2%;" class="BorderRadioComponenteHTML">
                            <b>Segunda fase de la prueba</b>
                                <br />
                                Ahora vuelve a efectuar la selección, olvidando que anteriormente ya habías visto estos colores, por lo cual se te pide no tratar de repetir la selección anterior, ni tratar de no repetirla, así como no asociarla a objetos tales como autos, ropa, etc.
                                <br /> Concéntrece en elegir la que más le atraiga visualmente, repitiendo la operación hasta agotar las cartas.
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <div style="width: 97%; margin-left: 2%; margin-right: 2%; background-color: gainsboro; border: solid 2px gray;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img10" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />
                                            </div>

                                            <label id="lbl10" name="" runat="server" style="text-align: center; width: 100%;"></label>
                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img11" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl11" name="" runat="server" style="width: 100%; text-align: center;"></label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img12" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl12" name="" runat="server" style="width: 100%; text-align: center;"></label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img13" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl13" name="" runat="server" style="width: 100%; text-align: center;"></label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img14" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl14" name="" runat="server" style="width: 100%; text-align: center;"></label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img15" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl15" name="" runat="server" style="width: 100%; text-align: center;"></label>

                                        </div>

                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img16" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl16" name="" runat="server" style="width: 100%; text-align: center;"></label>

                                        </div>


                                        <div class="ctrlBasico">
                                            <div class="imgCell">
                                                <img id="img17" src="../Assets/images/PruebaAdaptacionMedio/LuscherBlank.jpg" onclick="ImgClick(this);" />
                                            </div>
                                            <label id="lbl17" name="" runat="server" style="width: 100%; text-align: center;"></label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="lblFin" name="" runat="server" style="visibility: hidden;"></label>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>

                        <div style="clear: both; height: 10px;"></div>
    <div class="DivBtnTerminarDerecha">
            <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>

