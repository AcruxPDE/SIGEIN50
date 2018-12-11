<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaOrtografiaIII.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaOrtografia3" %>

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPruebas" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="btnAgregarPalabra">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lstPalabras" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtPalabra" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnTerminar" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            vPruebaEstatus = "";
            var c = "";
            var input = null;
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
                    radconfirm(text, callBackFunction, 400, 200, null, "");
                    args.set_cancel(true);
                }
                else {
                    //window.close();
                    window.location = "Default.aspx?ty=sig";
                }
            }
            //FUNCION PARA VALIDAR LA PALABRA INGRESADA
            function ValidarPalabraIngresada(sender, args) {
                var vTxtPalabra = $find("<%= txtPalabra.ClientID%>");
                if (vTxtPalabra.get_value() == "") {
                    radalert("Captura una palabra.", 400, 150, "Ortografia III");
                    args.set_cancel(true);
                }
            }

            window.onload = function (sender, args) {

                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var segundos = ' <%=this.vTiempoPrueba%>';
                            if (segundos <= 0) {
                                //var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no se lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
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
                            }
                        }
                        else {

                            // window.close();
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "<label><b>Instrucciones:</b><br/>Lee con atención el siguiente párrafo y escribe correctamente las palabras que deban ser acentuadas.<br/><br/> Añade las palabras con errores a la lista que aparece debajo del párrafo escribiendo la palabra correcta en el cuadro de texto y haciendo clic en el botón ' + '; si deseas eliminar una palabra de la lista haz clic en el botón ' X ' que aparece en la parte derecha del contenedor.</label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 900, 650, null, "Ortografía III");
                }
            };

            function OpenReport() {
                var vURL = "ReporteadorPruebasIDP.aspx";
                var vTitulo = "Impresión Ortografía III";

                var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClToken %>';



                        var windowProperties = {
                            width: document.documentElement.clientWidth - 20,
                            height: document.documentElement.clientHeight - 20
                        };

                        vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=ORTOGRAFIA3";
                        var win = window.open(vURL, '_blank');
                        win.focus();
                    }


            function WinClose(sender, args) {
                vPruebaEstatus = "Terminado";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                //var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                var oWnd = radalert("El tiempo de la aplicación de la prueba ha concluido. Recuerda que no es posible regresar a ella, si intentas hacerlo a través del botón del navegador, la aplicación no lo permitirá, generando un error y registrando el intento.", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                //window.close();
                window.location = "Default.aspx?ty=sig";
            }


            function ValidarContendorPreguntas(sender, args) {
                var flag = true;
                var GNoContestadas = new Array();
                var lbPalabras = $find("<%=lstPalabras.ClientID%>");
                if (lbPalabras.get_items()._array.length == 0) {
                    var vPalabra = $find("<%=txtPalabra.ClientID%>");
                    input = vPalabra;
                    var oWind = radalert("Capture una palabra.", 400, 150, "", SetFocusControles);
                }
                return flag;
            }

            function SetFocusControles() {
                input.focus();
                var flag = false;
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
    <label style="font-size:21px;">Ortografía III</label>

    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
            <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="200">
                        <p style="margin: 10px; text-align: justify;">
                            <label runat="server">Lee con atención el siguiente párrafo y escribe correctamente las palabras que deban ser acentuadas.</label>
                            <br />
                            <label id="Label1" runat="server">Añade las palabras con errores a la lista que aparece debajo del párrafo escribiendo la palabra correcta en el cuadro de texto y haciendo click en el botón "+"; si deseas eliminar una palabra de la lista haz click en el botón "X" que aparece en la parte derecha del contenedor.</label>
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

            <telerik:RadPane ID="radPanelPreguntas" runat="server" Width="100%">

                <div class="ctrlBasico" style="border: 1px solid #ddd; width: 96%; padding: 10px; margin: 1%;">
                    <p>
                        <label id="Label2" runat="server">
                            Un dia, el me pregunto si yo vivia ahi. Me dio la impresion de que hablaba de manera despectiva. Hubiera querido contestarle con orgullo que si, esa era mi casa, pero
solo le dije timidamente que si. No se por que su pregunta me hizo sentir incomoda; pues, aunque la casa no era una mansion, era mia. Habia trabajado mucho y despues de años, fue la unica casita que pude comprar.</label>
                    </p>
                </div>

                <div style="clear: both; height: 20px;"></div>
                <div class="ctrlBasico" style="margin-left: 10px;">
                    <telerik:RadTextBox runat="server" ID="txtPalabra" Width="200px" />
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" Text="+" Width="50px" ID="btnAgregarPalabra" OnClick="btnAgregarPalabra_Click" OnClientClicking="ValidarPalabraIngresada" />
                </div>

                <div style="clear: both;"></div>
                <div class="ctrlBasico" style="margin-left: 10px;">
                    <telerik:RadListBox
                        runat="server"
                        ID="lstPalabras"
                        Height="570px"
                        Width="235px"
                        AllowDelete="true"
                        ButtonSettings-AreaWidth="35px">
                    </telerik:RadListBox>
                </div>

            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>



    <!-- Footer -->
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
            <telerik:RadButton Visible="false" ID="btnImpresionPrueba" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false"></telerik:RadButton>   
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" Visible="true" OnClientClicking="ConfirmarEliminarPrueba" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminarBateria" runat="server" Text="Eliminar batería" AutoPostBack="true" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminarBateria_Click" Visible="true"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
