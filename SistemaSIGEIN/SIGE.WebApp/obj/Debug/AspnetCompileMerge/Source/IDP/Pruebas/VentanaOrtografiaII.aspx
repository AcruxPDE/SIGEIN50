<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaOrtografiaII.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaOrtografia2" %>

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
            var input = "";
            var c = "";
            window.onload = function (sender, args) {
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var segundos = '<%=vTiempoPrueba%>';
                            if (segundos <= 0) {
                                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
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

                              c =  Cronometro(segundos, display);
                            }
                        }
                        else {

                            // window.close();
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "<label><b>Instrucciones:</b><br/>Algunas de las palabras listadas están escritas con falta de ortografía, señala cuáles son y escríbelas en forma correcta en el espacio en blanco.</label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Ortografía II");
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
                    //window.close();
                    window.location = "Default.aspx?ty=sig";
                }
            }

            function WinClose(sender, args) {
                vPruebaEstatus = "Terminado";
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function mensajePruebaTerminada() {
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                //window.close();
                window.location = "Default.aspx?ty=sig";
            }

            //FUNCION PARA HABILITAR O DESHABILITAR LOS TEXTBOX
            function EventChangedItem(sender, eventArgs) {
                var vClassName = document.getElementById(sender.get_id()).className;//OBTENEMOS EL CLASSNAME DEL RADCOMBOBOX POR DEFECTO
                var cmbPreguntas = document.getElementsByClassName(vClassName);// OBTENEMOS EN UN ARREGLO LOS COMBOS DE LAS 25 PREGUNTAS
                var pregunta = null;  //OBJETO DE LA PREGUNTA A LA QUE SE LE HABILITARA O DESHABILITARA EL TEXTBOX

                var i = 0;//CONTADOR
                for (i = 0; i < cmbPreguntas.length; i++) {
                    if (cmbPreguntas[i].id == sender.get_id()) { //SI EL ID ES EL DEL SENDER ENTONCES DESHABILITAMOS SU PREGUNTA
                        var pregunta = document.getElementById("ctl00_ContentPruebas_txtPregunta" + (i + 1));
                        //console.info(pregunta);
                        var item = eventArgs.get_item();
                        var cmbpositionSelected = item.get_index() + 1;

                        if (cmbpositionSelected == 1) {
                            pregunta.setAttribute("disabled", "disabled");
                            pregunta.value = "";
                        }
                        else
                            if (cmbpositionSelected == 2) {
                                pregunta.removeAttribute("disabled");
                            }
                        break;
                    }
                }

            }

            function ValidarContendorPreguntas(sender, args) {
                var flag = true;
                var GNoContestadas = new Array();
                var vContenedor = document.getElementsByClassName("Contenedor");
                var i = 0;
                for (i = 0; i < vContenedor.length; i++) {
                    if (vContenedor[i].control._selectedItem == null) {
                        var comboBox = document.getElementById(vContenedor[i].id);
                        input = comboBox.control._inputDomElement;
                        var oWind = radalert("No respondiste la pregunta numero " + (i + 1), 400, 150, "", SetFocusControles);
                        flag = false;
                        break;
                    }
                    else {
                        console.info(vContenedor[i].control);
                        if (vContenedor[i].control._selectedIndex == 1) {
                            var vContenedorPreguntas = document.getElementsByClassName("ContenedorPreguntas");
                            var vPregunta = document.getElementById(vContenedorPreguntas[i].id);

                            if (vPregunta.value == "") {
                                input = vPregunta;
                                var oWind = radalert("No respondiste la pregunta numero " + (i + 1), 400, 150, "", SetFocusControles);
                                flag = false;
                                break;
                            }
                        }
                    }
                }
                return flag;
            }

            function SetFocusControles() {
                input.focus();
                var flag = false;
            }


            function OpenReport() {
                var vURL = "ReporteadorPruebasIDP.aspx";
                var vTitulo = "Impresión Ortografía II";

                var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClToken %>';



                            var windowProperties = {
                                width: document.documentElement.clientWidth - 20,
                                height: document.documentElement.clientHeight - 20
                            };

                            vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=ORTOGRAFIAII";
                            var win = window.open(vURL, '_blank');
                            win.focus();
                            //var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
                        }
        </script>
    </telerik:RadCodeBlock>
    <label style="font-size:21px;">Ortografía II</label>
    <div style="height: calc(100% - 80px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

            <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="160">
                        <p style="margin: 10px; text-align: justify;">
                            <label runat="server">Algunas de las palabras listadas están escritas con falta de ortografía,</label>
                            <br />
                            <label id="Label26" runat="server">señala cuáles son y escríbelas en forma correcta en el espacio en blanco </label>
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <table style="width: 95%; margin-left: 3%; margin-right: 2%;" class="BorderRadioComponenteHTML">
                    <thead>
                        <tr>
                            <td width="200px"></td>
                            <td></td>

                        </tr>
                    </thead>
                    <tbody>

                        <tr>
                            <td>

                                <div class="ctrlBasico">
                                    <label id="Label4" name="" runat="server" style="font-weight: bold;">1. Consideración</label>
                                </div>
                            </td>
                            <td>
                                <div style="height: 10px"></div>

                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta1" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta1" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--2--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label1" name="" runat="server" style="font-weight: bold;">2. Exibición</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta2" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta2" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--3--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label2" name="" runat="server" style="font-weight: bold;">3. Mananteal</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta3" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta3" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--4--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label3" name="" runat="server" style="font-weight: bold;">4. Compensación</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta4" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta4" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--5--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label5" name="" runat="server" style="font-weight: bold;">5. Enbotellar</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta5" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta5" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--6--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label6" name="" runat="server" style="font-weight: bold;">6. Lógicamente</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta6" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta6" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--7--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label7" name="" runat="server" style="font-weight: bold;">7. Athlético</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta7" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta7" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--8--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label8" name="" runat="server" style="font-weight: bold;">8. Aguinaldo</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta8" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta8" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--9--%>

                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label9" name="" runat="server" style="font-weight: bold;">9. Ermético</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta9" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta9" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--10--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label10" name="" runat="server" style="font-weight: bold;">10. Exaustivo</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta10" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta10" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--11--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label11" name="" runat="server" style="font-weight: bold;">11. Exelente</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta11" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta11" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--12--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label12" name="" runat="server" style="font-weight: bold;">12.  Circunscribir</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta12" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta12" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--13--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label13" name="" runat="server" style="font-weight: bold;">13. Hayar</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta13" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta13" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--14--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label14" name="" runat="server" style="font-weight: bold;">14. Inecesario</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta14" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta14" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--15--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label25" name="" runat="server" style="font-weight: bold;">15. Prohibido</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta15" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta15" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>


                        <%--16--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label15" name="" runat="server" style="font-weight: bold;">16. Areopuerto</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta16" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta16" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--16--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label16" name="" runat="server" style="font-weight: bold;">17. Legítimo</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta17" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta17" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--17--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label17" name="" runat="server" style="font-weight: bold;">18. Municipio</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta18" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />

                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta18" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>


                        <%--18--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label18" name="" runat="server" style="font-weight: bold;">19. Defectoso</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta19" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta19" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--19--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label19" name="" runat="server" style="font-weight: bold;">20. Eliminar</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta20" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta20" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--20--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label20" name="" runat="server" style="font-weight: bold;">21. Simultanio</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta21" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta21" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--21--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label21" name="" runat="server" style="font-weight: bold;">22. Inchar</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta22" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta22" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--22--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label22" name="" runat="server" style="font-weight: bold;">23. Enpapar</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta23" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta23" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--23--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label23" name="" runat="server" style="font-weight: bold;">24. Inculpar</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta24" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta24" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>

                        <%--24--%>
                        <tr>
                            <td>
                                <div class="ctrlBasico">
                                    <label id="Label24" name="" runat="server" style="font-weight: bold;">25. Análogo</label>
                                </div>
                            </td>
                            <td>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox ID="cmbPregunta25" CssClass="ComboBox Contenedor" runat="server" Height="90px" Width="140px" OnClientSelectedIndexChanged="EventChangedItem">
                                        <DefaultItem Text="Seleccione.." Value="-1" />
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/Exito.png" Text="Correcto" Value="C"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="../../Assets/images/icon_cancel.png" Text="Incorrecto" Value="I"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txtPregunta25" CssClass="ContenedorPreguntas" runat="server" Width="140px"></telerik:RadTextBox>
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
   <div class="divControlDerecha"  style="margin:2px;">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
    </div>
       <div class="divControlDerecha"  style="margin:2px;">
           <telerik:RadButton ID="btnCorregir" runat="server" Visible="false" OnClick="btnCorregir_Click" Text="Corregir" AutoPostBack="true"></telerik:RadButton>
    </div>

    <div class="divControlDerecha"  style="margin:2px;">
        <telerik:RadButton Visible="false" ID="btnImpresionPrueba" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false"></telerik:RadButton>   
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>


